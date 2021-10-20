using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HeadHunter.Models;
using HeadHunter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeadHunter.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    Occupation = model.Occupation,
                    PhoneNumber = model.PhoneNumber
                };
                if ( string.IsNullOrEmpty(model.Avatar))
                {
                    user.Avatar = "https://www.computerhope.com/jargon/g/guest-user.jpg";
                }
                else
                {
                    user.Avatar = model.Avatar;
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.Occupation == Occupation.Работодатель)
                    {
                        await _userManager.AddToRoleAsync(user, "employer");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "jobSeeker");
                    }
                    await _signInManager.SignInAsync(user, false);
                    if (user.Occupation == Occupation.Работодатель)
                    {
                        return RedirectToAction("Index", "Resume");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Vacancy");
                    }
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(model.Email);
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                    user,
                    model.Password,
                    model.RememberMe,
                    false
                    );
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    if(user.Occupation == Occupation.Работодатель)
                    {
                        return RedirectToAction("Index", "Resume");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Vacancy");
                    }
                }
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
