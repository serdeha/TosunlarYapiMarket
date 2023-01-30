using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TosunlarYapiMarket.Entity.Concrete;
using TosunlarYapiMarket.WebUI.Areas.User.Models;

namespace TosunlarYapiMarket.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GirisYap()
        {
            if (User.Identity!.IsAuthenticated)
            {
                TempData["IsNotSuccess"] = "Sisteme zaten giriş yaptınız.";
                return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GirisYap(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
                    }
                    else
                    {
                        ModelState.AddModelError("","Lütfen bir girdiğiniz bilgilerin doğruluğundan emin olunuz.");
                        return View();
                    }
                }
                ModelState.AddModelError("","Böyle bir kullanıcı bulunamadı.");
                return View();
            }
            return View();
        }

        public async Task CikisYap()
        {
            await _signInManager.SignOutAsync();
        }
    }
}