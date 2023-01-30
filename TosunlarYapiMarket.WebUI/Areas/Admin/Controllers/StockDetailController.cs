using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Business.Validations;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StockDetailController:Controller
    {
        private IStockDetailService _stockDetailService;
        private readonly UserManager<AppUser> _userManager;

        public StockDetailController(IStockDetailService stockDetailService,UserManager<AppUser> userManager)
        {
            _stockDetailService = stockDetailService;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Index(int? stockId)
        {
            List<StockDetail> stockDetails;
            if (stockId.HasValue)
            {
                stockDetails = await _stockDetailService.GetAllWithFilterAsync(x => x.Id == stockId && x.IsActive && !x.IsDeleted,x=>x.AppUser!)!;
                return View(stockDetails);
            }
            stockDetails = await _stockDetailService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted, x => x.Stocks!,x=>x.AppUser!)!;
            return View(stockDetails);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(StockDetail stockDetail)
        {
            if (ModelState.IsValid)
            {
                StockDetailValidator validator = new StockDetailValidator();
                ValidationResult result = await validator.ValidateAsync(stockDetail);
                if (result.IsValid)
                {
                    AppUser appUser = await _userManager.GetUserAsync(HttpContext.User);
                    stockDetail.AppUserId = appUser.Id;
                    stockDetail.CreatedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    stockDetail.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    await _stockDetailService.AddAsync(stockDetail);
                    return RedirectToAction("Index", "StockDetail");
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                    return View(stockDetail);
                }                
            }
            return View(stockDetail);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int stockDetailId)
        {
            var stockDetail = await _stockDetailService.GetByIdAsync(stockDetailId);
            if(stockDetail != null)
            {
                return View(stockDetail);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(StockDetail stockDetail)
        {
            if (ModelState.IsValid)
            {
                StockDetailValidator validator = new StockDetailValidator();
                ValidationResult result = await validator.ValidateAsync(stockDetail);
                if (result.IsValid)
                {
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    stockDetail.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    stockDetail.ModifiedDate = DateTime.Now;
                    await _stockDetailService.UpdateAsync(stockDetail);
                    TempData["IsSuccess"] = $"{stockDetail.StockDetailName} isimli ürün detayı güncellendi";
                    return RedirectToAction("Index", "StockDetail", new { area = "Admin" });
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error}\n";
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    }
                    TempData["IsNotSuccess"] = $"*{errors}\n ";
                    return View(stockDetail);
                }
            }
            TempData["IsNotSuccess"] = "Ürün detayı güncellenirken bir hata oluştu.";
            return View(stockDetail);
        }
        

        public async Task<JsonResult> Delete(int stockDetailId)
        {
            var stockDetail = await _stockDetailService.GetByIdAsync(stockDetailId);
            if(stockDetail != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                stockDetail.ModifiedDate = DateTime.Now;
                stockDetail.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                await _stockDetailService.DeleteAsync(stockDetail);
                return Json(JsonSerializer.Serialize(stockDetail, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }
            return Json(new { ResultStatus = false });
        }
    }
}
