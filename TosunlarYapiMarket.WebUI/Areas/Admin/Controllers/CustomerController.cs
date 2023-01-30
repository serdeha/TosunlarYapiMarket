using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TosunlarYapiMarket.Business.Abstract;
using TosunlarYapiMarket.Business.Validations;
using TosunlarYapiMarket.Core.Extensions;
using TosunlarYapiMarket.Entity.Concrete;

namespace TosunlarYapiMarket.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;

        public CustomerController(ICustomerService customerService, UserManager<AppUser> userManager)
        {
            _customerService = customerService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _customerService.GetAllWithFilterAsync(x=>x.IsActive && !x.IsDeleted)!);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Add(Customer customer)
        {
            if (ModelState.IsValid)
            {
                CustomerValidator validator = new CustomerValidator();
                ValidationResult result = await validator.ValidateAsync(customer);
                if (result.IsValid)
                {
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    customer.AppUserId = appUser.Id;
                    customer.CreatedByName = string.Concat(appUser.FirstName, " ", customer.LastName);
                    await _customerService.AddAsync(customer);
                    TempData["IsSuccess"] = $"{string.Concat(customer.FirstName, " ", customer.LastName)} isimli müşteri başarıyla eklendi.";
                    return RedirectToAction("Index", "Customer", new { area = "Admin" });
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        errors += $"*{error}\n\n";
                    }

                    TempData["IsNotSuccess"] = $"{errors}";
                    return View(customer);
                }
            }

            TempData["IsNotSuccess"] = "Bir hata oluştu. Lütfen yöneticiye danışın.";
            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int customerId)
        {
            Customer? customer = await _customerService.GetByIdAsync(customerId);
            if (customer != null)
            {
                return View(customer);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Customer customer)
        {
            if (ModelState.IsValid)
            {
                CustomerValidator validator = new CustomerValidator();
                ValidationResult result = await validator.ValidateAsync(customer);
                if (result.IsValid)
                {
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    customer.ModifiedByName = string.Concat(appUser.FirstName," ",appUser.LastName);
                    customer.ModifiedDate = DateTime.Now;
                    customer.AppUserId = appUser.Id;
                    await _customerService.UpdateAsync(customer);
                    TempData["IsSucess"] = $"{string.Concat(customer.FirstName, " ", customer.LastName)} başarıyla güncellendi.";
                    return RedirectToAction("Index", "Customer", new { area = "Admin" });
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        errors += $"{error.ErrorMessage}\n";
                        TempData["IsNotSuccess"] = $"*{errors}\n";
                        return RedirectToAction("Index", "Customer", new { area = "Admin" });
                    }
                }
            }

            TempData["IsNotSuccess"] = "Müşteri güncellenirken bir hata oluştu.";
            return View(customer);
        }

        public async Task<JsonResult> Delete(int customerId)
        {
            Customer? customer = await _customerService.GetByIdAsync(customerId);
            if (customer != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                customer.ModifiedByName = string.Concat(appUser.FirstName," ",appUser.LastName);
                customer.ModifiedDate = DateTime.Now;
                await _customerService.DeleteAsync(customer);
                return Json(JsonSerializer.Serialize(customer, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(new {ResultStatus = false});
        }

        public async Task<IActionResult> GetDetail(int customerId)
        {
            Customer? customer = await _customerService.GetWithFilterAsync(x=>x.Id == customerId,x=>x.AppUser!,x=>x.Invoices!,x=>x.Debts!,x=>x.Notes!);
            if (customer != null)
            {
                return PartialView("~/Areas/Admin/Views/Customer/_CustomerDetailPartialView.cshtml", customer);
            }

            return Json(JsonSerializer.Serialize(new {ResultStatus = false},new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public JsonResult GetCustomerNo()
        {
            string newCustomerNo = CreateCustomerNoExtension.GetCustomerNo();
            var customers = _customerService.GetAll()?.Select(x => x.CustomerNo);
            if (!customers!.Contains(newCustomerNo))
            {
                return Json(JsonSerializer.Serialize(new { customerNo = newCustomerNo }, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(new { customerNo = false });
        }
    }
}
