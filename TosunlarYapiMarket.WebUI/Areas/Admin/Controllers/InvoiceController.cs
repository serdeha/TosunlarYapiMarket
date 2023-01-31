using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Business.Validations;
using TosunlarYapiMarket.Core.Extensions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;
        private readonly IStockService _stockService;
        private readonly IStockBasketService _stockBasketService;
        private readonly IStockDetailService _stockDetailService;
        private readonly IDebtService _debtService;

        private readonly UserManager<AppUser> _userManager;
        private string _addedInvoiceDetail = string.Empty;
        private List<StockBasket>? _stockBaskets;

        public InvoiceController(IInvoiceService invoiceService, ICustomerService custoemrService, IStockService stockService, IStockBasketService stockBasketService, IDebtService debtService, IStockDetailService stockDetailService, UserManager<AppUser> userManager)
        {
            _invoiceService = invoiceService;
            _userManager = userManager;
            _customerService = custoemrService;
            _stockService = stockService;
            _stockBasketService = stockBasketService;
            _debtService = debtService;
            _stockDetailService = stockDetailService;
        }

        public async Task<IActionResult> Index(int? customerId)
        {
            List<Invoice> invoices;
            if (customerId.HasValue)
            {
                var customer = await _customerService.GetByIdAsync(customerId.Value);
                ViewBag.CustomerTitle = string.Concat(customer?.FirstName, " ", customer?.LastName);
                invoices = await _invoiceService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.CustomerId == customerId.Value, x => x.StockBaskets!, x => x.Customer!)!;
            }
            else
            {
                invoices = await _invoiceService.GetAllWithFilterAsync(x => !x.IsDeleted && x.IsActive, x => x.Customer!)!;
            }
            return View(invoices);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.CustomerList = GetSelectListCustomers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                InvoiceValidator validator = new InvoiceValidator();
                ValidationResult result = await validator.ValidateAsync(invoice);
                if (result.IsValid)
                {
                    var appUser = await _userManager?.GetUserAsync(HttpContext.User)!;
                    var customer = await _customerService.GetByIdAsync(invoice.CustomerId);
                    invoice.AppUserId = appUser.Id;
                    await _invoiceService.AddAsync(invoice);
                    _addedInvoiceDetail = $"{string.Concat(customer?.FirstName, " ", customer?.LastName)} müşterinin {invoice.InvoiceCode} numaralı faturası başarıyla eklenmiştir.";
                    return RedirectToAction("AddInvoiceStock", "Invoice", new { area = "Admin", invoiceId = invoice.Id });
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error.ErrorMessage}\n";
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                }
            }
            ViewBag.CustomerList = GetSelectListCustomers();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddInvoiceStock(int? invoiceId)
        {
            if (invoiceId.HasValue)
            {
                var invoice = await _invoiceService.GetByIdAsync(invoiceId.Value);
                if (invoice != null) invoice.Customer = await _customerService.GetByIdAsync(invoice.CustomerId);
                if (!string.IsNullOrEmpty(_addedInvoiceDetail)) TempData["IsSuccess"] = _addedInvoiceDetail;
                return View(invoice);
            }
            return RedirectToAction("Add", "Invoice", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoiceStock(List<int> StockId, List<double> StockAnyDetail, int invoiceId)
        {

            if (StockId != null && StockAnyDetail != null)
            {
                _stockBaskets = new List<StockBasket>();
                decimal AllPrice = 0;
                for (int i = 0; i < StockId.Count; i++)
                {
                    var stock = await _stockService.GetByIdAsync(StockId[i]);
                    if (stock != null && !(StockAnyDetail[i] > stock.StockAnyDetail))
                    {
                        _stockBaskets.Add(new StockBasket
                        {
                            StockId = StockId[i],
                            StockName = stock.StockName,
                            InvoiceId = invoiceId,
                            StockAnyDetail = StockAnyDetail[i],
                            StockPrice = CalculateTotalPrice.Calculate(stock.Price, stock.KDV, Convert.ToDecimal(StockAnyDetail[i]))
                        });
                        AllPrice += _stockBaskets[i].StockPrice;
                        stock.StockAnyDetail -= StockAnyDetail[i];
                        await _stockService.UpdateAsync(stock);
                    }
                    else
                    {
                        TempData["IsNotSuccess"] = "Yeterli stok'u bulunmayan ürün eklediniz. Lütfen kontrol ediniz.";
                        return RedirectToAction("AddInvoiceStock", "Invoice", new { area = "Admin", invoiceId = invoiceId });
                    }
                }

                var invoice = await _invoiceService.GetByIdAsync(invoiceId);
                if (invoice != null)
                {
                    invoice.StockBaskets = _stockBaskets;
                    invoice.TotalPrice += AllPrice;
                    invoice.IsPaid = false;
                    var customer = await _customerService.GetByIdAsync(invoice.CustomerId);
                    var user = await _userManager.GetUserAsync(HttpContext.User);
                    if (customer != null)
                    {
                        customer.CustomerDebt += AllPrice;
                        await _customerService.UpdateAsync(customer);
                        var debt = await _debtService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.InvoiceId == invoice.Id);
                        if(debt == null)
                        {
                            await _debtService.AddAsync(new Debt
                            {
                                AppUserId = user.Id,
                                CustomerId = customer.Id,
                                TotalAmount = AllPrice,
                                PaymentAmount = AllPrice,
                                PaymentDate = DateTime.Now,
                                InvoiceId = invoice.Id
                            });
                        }
                        else
                        {
                            debt.AppUserId = user.Id;
                            debt.TotalAmount += AllPrice;
                            debt.PaymentAmount += AllPrice;
                            debt.PaymentDate = DateTime.Now;
                            debt.InvoiceId = invoice.Id;
                            await _debtService.UpdateAsync(debt);
                        }
                    }
                    await _invoiceService.UpdateAsync(invoice);
                    return RedirectToAction("Index", "Invoice", new { area = "Admin" });
                }
            }
            return NotFound();
        }

        public async Task<JsonResult> Delete(int invoiceId)
        {
            var invoice = await _invoiceService.GetByIdAsync(invoiceId);
            if (invoice != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                invoice.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                invoice.ModifiedDate = DateTime.Now;
                Customer? customer = _customerService.GetById(invoice.CustomerId);
                customer.CustomerDebt -= invoice.DiscountedTotalPrice == 0 ? invoice.TotalPrice : invoice.DiscountedTotalPrice;
                Debt? debt = await _debtService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.InvoiceId == invoice.Id)!;
                if (debt != null) await _debtService.DeleteAsync(debt);
                await _customerService.UpdateAsync(customer);
                await _invoiceService.DeleteAsync(invoice);
                return Json(JsonSerializer.Serialize(invoice, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }
            return Json(new { ResultStatus = false });
        }

        public async Task<IActionResult> ShowDiscountPartial(int invoiceId)
        {
            var invoice = await _invoiceService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.Id == invoiceId);
            if (invoice != null)
            {
                invoice.Customer = await _customerService.GetByIdAsync(invoice.CustomerId);
                return PartialView("~/Areas/Admin/Views/Invoice/_AddDiscountPartialView.cshtml", invoice);
            }

            return Json(JsonSerializer.Serialize(new { ResulStatus = false }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }
        public async Task<IActionResult> AddDiscount(int invoiceId, decimal discountPrice)
        {
            var invoice = await _invoiceService.GetByIdAsync(invoiceId);
            decimal addedPrice = invoice.DiscountedTotalPrice == 0 ? invoice.TotalPrice : invoice.DiscountedTotalPrice;
            if (invoice != null && (addedPrice - discountPrice) > 0)
            {
                invoice.Customer = await _customerService.GetByIdAsync(invoice.CustomerId);
                invoice.DiscountPrice += discountPrice;
                var debt = await _debtService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.InvoiceId == invoice.Id);
                if (invoice.DiscountedTotalPrice > 0 && debt != null)
                {
                    invoice.DiscountedTotalPrice = invoice.DiscountedTotalPrice -= discountPrice;
                    debt.TotalAmount = invoice.DiscountedTotalPrice;
                    debt.PaymentAmount = invoice.DiscountedTotalPrice;
                    await _debtService.UpdateAsync(debt);
                }
                else
                {
                    invoice.DiscountedTotalPrice = invoice.TotalPrice - discountPrice;
                    if(debt != null)
                    {
                        debt.TotalAmount = invoice.DiscountedTotalPrice;
                        debt.PaymentAmount = invoice.DiscountedTotalPrice;
                        await _debtService.UpdateAsync(debt);
                    }
                }
                
                await _invoiceService.UpdateAsync(invoice);
                return Json(JsonSerializer.Serialize(invoice, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(JsonSerializer.Serialize(new { ResultStatus = false }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public async Task<IActionResult> ShowInvoice(int invoiceId)
        {
            var invoice = await _invoiceService.GetWithFilterAsync(x => x.Id == invoiceId, x => x.StockBaskets!, x => x.Customer!);

            if (invoice != null)
            {
                invoice.Customer = await _customerService.GetByIdAsync(invoice.CustomerId);
                invoice.StockBaskets = await _stockBasketService.GetAllWithFilterAsync(x => x.InvoiceId == invoiceId, x => x.Stock!)!;
                return View(invoice);
            }

            return NotFound();
        }

        public async Task<JsonResult> GetInvoiceCode()
        {
            int invCode = 0;
            var invoices = await _invoiceService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted)!;
            if (invoices.Count > 0)
            {
                invCode = invoices.Select(x => x.InvoiceCode).Last() == null ? 0 : Convert.ToInt32(invoices.Select(x => x.InvoiceCode).Last());
                invCode++;
            }
            invCode++;
            return Json(JsonSerializer.Serialize(new { invoiceCode = invCode }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        private List<SelectListItem> GetSelectListCustomers()
        {
            var customers = _customerService.GetAll(x => x.IsActive && !x.IsDeleted)!;
            List<SelectListItem> listCustomers = new List<SelectListItem>();
            foreach (var customer in customers)
            {
                listCustomers.Add(new SelectListItem
                {
                    Text = string.Concat(customer.FirstName, " ", customer.LastName),
                    Value = customer.Id.ToString()
                });
            }

            return listCustomers;
        }

        public async Task<JsonResult> GetSelectListForStocks()
        {
            var stocks = await _stockService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.StockAnyDetail > 0)!;
            List<SelectListItem> listCustomers = new List<SelectListItem>();
            foreach (var stock in stocks)
            {
                stock.StockDetail = await _stockDetailService.GetByIdAsync(stock.StockDetailId);
                listCustomers.Add(new SelectListItem
                {
                    Text = $"{stock.StockName} - Stok Adedi : {Math.Round(stock.StockAnyDetail, 2)} {stock.StockDetail?.StockDetailName}",
                    Value = stock.Id.ToString()
                });
            }

            return Json(JsonSerializer.Serialize(listCustomers), new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });
        }
    }
}
