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
    [Area("Admin")]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;

        public NoteController(INoteService noteService, UserManager<AppUser> userManager, ICustomerService customerService)
        {
            _noteService = noteService;
            _userManager = userManager;
            _customerService = customerService;
        }

        public async Task<IActionResult> Index(int? customerId)
        {
            List<Note> notes;
            if (customerId.HasValue)
            {
                notes = await _noteService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted && x.CustomerId == customerId.Value, x => x.Customer!)!;
                return View(notes);
            }

            notes = await _noteService.GetAllWithFilterAsync(x => x.IsActive && !x.IsDeleted, x => x.Customer!)!;
            return View(notes);
        }

        public async Task<IActionResult> ApprovedNotes()
        {
            return View(await _noteService.GetAllWithFilterAsync(x => x.IsActive == false && !x.IsDeleted, x => x.Customer!)!);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.CustomerList = GetSelectListCustomers();
            return View();
        }

        public async Task<IActionResult> Add(Note note)
        {
            if (ModelState.IsValid)
            {
                NoteValidator validator = new NoteValidator();
                ValidationResult result = await validator.ValidateAsync(note);
                if (result.IsValid)
                {
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    note.CreatedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    note.AppUserId = appUser.Id;
                    await _noteService.AddAsync(note);
                    return RedirectToAction("Index", "Note", new { area = "Admin" });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ViewBag.CustomerList = GetSelectListCustomers();
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                        return View(note);
                    }
                }
            }
            ViewBag.CustomerList = GetSelectListCustomers();
            TempData["IsNotSuccess"] = "Not eklenirken bir hata oluştu!";
            return View(note);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int noteId)
        {
            var note = await _noteService.GetWithFilterAsync(x => x.Id == noteId, x => x.Customer!);
            if (note != null)
            {
                ViewBag.CustomerList = GetSelectListCustomers();
                return View(note);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(Note note)
        {
            if (ModelState.IsValid)
            {
                NoteValidator validator = new NoteValidator();
                ValidationResult result = await validator.ValidateAsync(note);
                if (result.IsValid)
                {
                    var appUser = await _userManager.GetUserAsync(HttpContext.User);
                    note.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                    note.ModifiedDate = DateTime.Now;
                    await _noteService.UpdateAsync(note);
                    TempData["IsSuccess"] = $"{note.NoteTitle} isimli not başarıyla güncellendi.";
                    return RedirectToAction("Index", "Note", new {area = "Admin"});
                }
                else
                {
                    string errors = string.Empty;
                    foreach (var error in result.Errors)
                    {
                        errors += $"{error}\n";
                        ModelState.AddModelError(error.PropertyName,error.ErrorMessage);
                    }

                    TempData["IsNotSuccess"] = $"{errors}\n";
                    return View(note);
                }
            }

            TempData["IsNotSuccess"] = "Not eklenirken bir hata oluştu.";
            return View(note);
        }

        public async Task<JsonResult> Delete(int noteId)
        {
            Note? note = await _noteService.GetByIdAsync(noteId);
            if (note != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                note.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                note.ModifiedDate = DateTime.Now;
                await _noteService.DeleteAsync(note);
                return Json(JsonSerializer.Serialize(note, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }
            else
            {
                return Json(JsonSerializer.Serialize(new {ResultStatus = false},new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }
        }

        public async Task<IActionResult> GetApprovedDetail(int noteId)
        {
            Note? note = await _noteService.GetWithFilterAsync(x => x.Id == noteId, x => x.Customer!);
            if (note != null)
            {
                return PartialView("~/Areas/Admin/Views/Note/_NoteApproveDetailPartialView.cshtml", note);
            }

            return Json(JsonSerializer.Serialize(new {ResultStatus = false}, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public async Task<IActionResult> GetUndoDetail(int noteId)
        {
            Note? note = await _noteService.GetWithFilterAsync(x => x.Id == noteId, x => x.Customer!);
            if (note != null)
            {
                return PartialView("~/Areas/Admin/Views/Note/_NoteUndoDetailPartialView.cshtml", note);
            }

            return Json(JsonSerializer.Serialize(new { ResultStatus = false }, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public async Task<JsonResult> ApproveNote(int noteId)
        {
            Note? note = await _noteService.GetByIdAsync(noteId);
            if (note != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                note.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                note.ModifiedDate = DateTime.Now;
                note.IsActive = false;
                await _noteService.UpdateAsync(note);
                return Json(JsonSerializer.Serialize(note, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(JsonSerializer.Serialize(new {ResultStatus = false}, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        public async Task<JsonResult> UndoNote(int noteId)
        {
            Note? note = await _noteService.GetByIdAsync(noteId);
            if (note != null)
            {
                var appUser = await _userManager.GetUserAsync(HttpContext.User);
                note.ModifiedByName = string.Concat(appUser.FirstName, " ", appUser.LastName);
                note.ModifiedDate = DateTime.Now;
                note.IsActive = true;
                await _noteService.UpdateAsync(note);
                return Json(JsonSerializer.Serialize(note, new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }));
            }

            return Json(JsonSerializer.Serialize(new {ResultStatus = false}, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            }));
        }

        private List<SelectListItem> GetSelectListCustomers()
        {
            var customers = _customerService.GetAll(x => x.IsActive && !x.IsDeleted)!;
            List<SelectListItem> customersSelectList = new List<SelectListItem>();
            foreach (var customer in customers)
            {
                customersSelectList.Add(new SelectListItem
                {
                    Value = customer.Id.ToString(),
                    Text = string.Concat(customer.FirstName, " ", customer.LastName)
                });
            }

            return customersSelectList;
        }
    }
}
