using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Business.Validations;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.Controllers
{
    [Area(("Admin"))]
    public class DebtController : Controller
    {
        private readonly IDebtService _debtService;
        private readonly IInvoiceService _invoiceService;
        private readonly ICustomerService _customerService;
        private readonly IPayOffDebtService _payoffDebtService;
        private readonly UserManager<AppUser> _userManager;

        public DebtController(IDebtService debtService, IInvoiceService invoiceService, ICustomerService customerService, UserManager<AppUser> userManager, IPayOffDebtService payoffDebtService)
        {
            _debtService = debtService;
            _customerService = customerService;
            _userManager = userManager;
            _payoffDebtService = payoffDebtService;
            _invoiceService = invoiceService;
        }

        public async Task<IActionResult> Index(int? customerId)
        {
            List<Debt> debts;
            if (customerId.HasValue)
            {
                var customer = await _customerService.GetByIdAsync(customerId.Value);
                ViewBag.CustomerTitle = string.Concat(customer?.FirstName, " ", customer?.LastName);
                debts = await _debtService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.PaymentAmount > 0 && x.CustomerId == customerId.Value, x => x.Customer!)!;
            }
            else
            {
                debts = await _debtService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.PaymentAmount > 0, x => x.Customer!)!;
            }
            return View(debts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.CustomerList = GetSelectListCustomers();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Debt debt)
        {
            if (ModelState.IsValid)
            {
                DebtValidator validator = new DebtValidator();
                ValidationResult result = await validator.ValidateAsync(debt);
                if (result.IsValid)
                {
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    var customer = await _customerService.GetByIdAsync(debt.CustomerId);
                    debt.AppUserId = appUser.Id;
                    debt.CreatedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    debt.TotalAmount = debt.PaymentAmount;
                    if (customer != null) customer.CustomerDebt = debt.TotalAmount;
                    await _debtService.AddAsync(debt);
                    TempData["IsSuccess"] = $"{string.Concat(customer?.FirstName, " ", customer?.LastName)} isimli müşterinin {debt.PaymentAmount} ₺ borcu eklendi.";
                    return RedirectToAction("Index", "Debt", new { area = "Admin" });
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error.ErrorMessage}\n";
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    ViewBag.CustomerList = GetSelectListCustomers();
                    TempData["IsNotSuccess"] = $"*{errors}\n ";
                    return View(debt);
                }
            }
            ViewBag.CustomerList = GetSelectListCustomers();
            TempData["IsNotSuccess"] = "Borç eklenirken bir hata oluştu.";
            return View(debt);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int debtId)
        {
            var debt = await _debtService.GetWithFilterAsync(x => x.Id == debtId, x => x.Customer!);
            if (debt != null)
            {
                ViewBag.CustomerList = GetSelectListCustomers();
                return View(debt);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Debt debt)
        {
            if (ModelState.IsValid)
            {
                DebtValidator validator = new DebtValidator();
                ValidationResult result = await validator.ValidateAsync(debt);
                if (result.IsValid)
                {
                    var customer = await _customerService.GetByIdAsync(debt.CustomerId);
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    debt.AppUserId = appUser.Id;
                    debt.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    debt.ModifiedDate = DateTime.Now;
                    debt.TotalAmount = debt.PaymentAmount;
                    await _debtService.UpdateAsync(debt);
                    TempData["IsSuccess"] = $"{string.Concat(customer?.FirstName, " ", customer?.LastName)} isimli müşterinin borcu, {debt.PaymentAmount} ₺ olarak güncellendi.";
                    return RedirectToAction("Index", "Debt", new { area = "Admin" });
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error}\n";
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    ViewBag.CustomerList = GetSelectListCustomers();
                    TempData["IsNotSuccess"] = $"*{errors}\n ";
                    return View(debt);
                }
            }
            ViewBag.CustomerList = GetSelectListCustomers();
            TempData["IsNotSuccess"] = "Borç güncellenirken bir hata oluştu.";
            return View(debt);
        }

        public async Task<IActionResult> Delete(int debtId)
        {
            var debt = await _debtService.GetByIdAsync(debtId);
            if (debt != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                debt.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                debt.ModifiedDate = DateTime.Now;
                await _debtService.DeleteAsync(debt);
                return Json(JsonSerializer.Serialize(debt, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(new { ResultStatus = false });
        }

        public async Task<IActionResult> PayDebtPartial(int debtId)
        {
            var debt = await _debtService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.Id == debtId, x => x.Customer!);
            if (debt != null)
            {
                debt.Customer = await _customerService.GetByIdAsync(debt.CustomerId);
                return PartialView("~/Areas/Admin/Views/Debt/_PayDebtPartialView.cshtml", debt);
            }

            return Json(JsonSerializer.Serialize(new { ResulStatus = false }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public async Task<IActionResult> PayOffDebtPartial(int debtId, string customer)
        {
            List<PayOffDebt> payOffDebts = await _payoffDebtService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.DebtId == debtId, x => x.Debt!)!;
            ViewBag.CustomerName = customer;
            return PartialView("~/Areas/Admin/Views/Debt/_PayOffDebtPartialView.cshtml", payOffDebts);
        }

        public async Task<JsonResult> PayDebt(int debtId, decimal amountPaid)
        {
            Debt? debt = await _debtService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.Id == debtId, x => x.Customer!);
            if (debt != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                var customer = await _customerService.GetByIdAsync(debt.CustomerId);
                ViewBag.CustorName = string.Concat(customer?.FirstName, " ", customer?.LastName);
                debt.PaymentDate = DateTime.Now;
                debt.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                debt.ModifiedDate = DateTime.Now;


                if (amountPaid <= debt.PaymentAmount && amountPaid >= 0)
                {
                    debt.PaymentAmount = debt.PaymentAmount - amountPaid;
                    if (customer != null)
                    {
                        customer.CustomerDebt -= amountPaid;
                        await _customerService.UpdateAsync(customer);
                    }
                    await _debtService.UpdateAsync(debt);

                    if (debt.PaymentAmount == 0 && debt.InvoiceId != null)
                    {
                        Invoice? invoce = await _invoiceService.GetWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.Id == debt.InvoiceId)!;
                        if (invoce != null)
                        {
                            invoce.IsPaid = true;
                            await _invoiceService.UpdateAsync(invoce);
                        }
                    }

                    await _payoffDebtService.AddAsync(new PayOffDebt
                    {
                        DebtId = debtId,
                        AmountPaid = amountPaid,
                        CreatedByName = string.Concat(appUser?.FirstName, " ", appUser?.LastName),
                        ModifiedByName = $"Ödeme Yapan : {string.Concat(customer?.FirstName, " ", customer?.LastName)}",
                        IsActive = true,
                        IsDeleted = false,
                        PaidDate = DateTime.Now,
                    });
                    debt.Customer = customer;
                    return Json(JsonSerializer.Serialize(debt, new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve
                    }));
                }
                return Json(JsonSerializer.Serialize(new { ResultStatus = "Error" }, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(JsonSerializer.Serialize(new { ResultStatus = false }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public async Task<IActionResult> PaidDebts(int? customerId)
        {
            List<Debt> debts;
            if (customerId.HasValue)
            {
                var customer = await _customerService.GetByIdAsync(customerId.Value);
                ViewBag.CustorName = string.Concat(customer?.FirstName, " ", customer?.LastName);
                debts = await _debtService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.PaymentAmount <= 0 && x.CustomerId == customerId.Value, x => x.Customer!)!;
            }
            else
            {
                debts = await _debtService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.PaymentAmount <= 0, x => x.Customer!)!;
            }

            return View(debts);
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
    }
}
