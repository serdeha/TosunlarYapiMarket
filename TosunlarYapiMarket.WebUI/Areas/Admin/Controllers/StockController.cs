using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Business.Validations;
using TosunlarYapiMarket.Core.Extensions;
using TosunlarYapiMarket.Entity.Concrete;
using TosunlarYapiMarket.WebUI.Areas.Admin.Models;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;
        private readonly IStockDetailService _stockDetailService;
        private readonly UserManager<AppUser> _userManager;

        public StockController(IStockService stockService, IStockDetailService stockDetailService , UserManager<AppUser> userManager)
        {
            _stockService = stockService;
            _userManager = userManager;
            _stockDetailService = stockDetailService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _stockService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted,x=>x.StockDetail!)!);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.StockDetailList = GetStockDetailList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StockAddViewModel stockAddViewModel)
        {
            if (stockAddViewModel.FormFile != null)
            {
                stockAddViewModel.ImageUrl = ImageHelperExtension.UploadImage(stockAddViewModel.FormFile, "/admin/stock/");
            }

            var appUser = await _userManager.GetUserAsync(HttpContext.User);
            Stock stock = new Stock
            {
                Price = stockAddViewModel.Price,
                AppUserId = appUser.Id,
                CreatedByName = string.Concat(appUser.FirstName, " ", appUser.LastName),
                KDV = stockAddViewModel.Kdv,
                StockDetailId = stockAddViewModel.StockDetailId,
                StockName = stockAddViewModel.StockName,
                StockAnyDetail = stockAddViewModel.StockAnyDetail,
                ImageUrl = stockAddViewModel.ImageUrl ??= "defaultStock.png"
            };

            StockValidator validator = new StockValidator();
            ValidationResult result = await validator.ValidateAsync(stock);
            if (result.IsValid)
            {
                await _stockService.AddAsync(stock);
                return RedirectToAction("Index", "Stock", new { area = "Admin" });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                ViewBag.StockDetailList = GetStockDetailList();
                return View(stockAddViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int stockId)
        {
            var stock = await _stockService.GetByIdAsync(stockId);
            if (stock != null)
            {
                stock.StockDetail = await _stockDetailService.GetByIdAsync(stock.StockDetailId);
                ViewBag.StockDetailList = GetStockDetailList();
                return View(stock);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Stock stock)
        {
            StockValidator validator = new StockValidator();
            ValidationResult result = await validator.ValidateAsync(stock);
            if (result.IsValid)
            {
                if (stock.ImageFile != null)
                {
                    if (stock.ImageUrl != null) ImageHelperExtension.DeleteImage(stock.ImageUrl, "/admin/stock/");
                    stock.ImageUrl = ImageHelperExtension.UploadImage(stock.ImageFile, "/admin/stock/");
                }
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                stock.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                stock.ModifiedDate = DateTime.Now;
                await _stockService.UpdateAsync(stock);
                TempData["IsSuccess"] = $"{stock.StockName} isimli ürün başarıyla güncellendi.";
                return RedirectToAction("Index", "Stock", new { area = "Admin" });
            }
            else
            {
                string errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error}\n";
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                TempData["IsNotSuccess"] = $"{errors}";
                ViewBag.StockDetailList = GetStockDetailList();
                return View(stock);
            }
        }

        public async Task<JsonResult> Delete(int stockId)
        {
            Stock? stock = await _stockService.GetByIdAsync(stockId);
            if (stock != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                stock.AppUserId = appUser.Id;
                stock.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                stock.ModifiedDate = DateTime.Now;
                await _stockService.DeleteAsync(stock);
                return Json(JsonSerializer.Serialize(stock, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(new { ResultStatus = false });
        }

        public async Task<IActionResult> GetDetail(int stockId)
        {
            Stock? stock = await _stockService.GetWithFilterAsync(x => x.Id == stockId ,x => x.AppUser!);
            if (stock != null)
            {
                stock.StockDetail = await _stockDetailService.GetByIdAsync(stock.StockDetailId);
                return PartialView("~/Areas/Admin/Views/Stock/_StockDetailPartialView.cshtml", stock);
            }

            return NotFound();
        }

        private List<SelectListItem> GetStockDetailList()
        {
            List<StockDetail> stockDetails = _stockDetailService.GetAllWithFilter(x => x.IsActive && !x.IsDeleted)!;
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            if(stockDetails.Count > 0)
            {
                foreach (var stockDetail in stockDetails)
                {
                    selectListItems.Add(new SelectListItem
                    {
                        Text = stockDetail.StockDetailName,
                        Value = stockDetail.Id.ToString()
                    });
                }
            }
            else
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = "Lütfen bir ürün detayı ekleyiniz..",
                    Value = "-1",
                    Disabled = true
                });
            }
            
            return selectListItems;
        }
    }
}
