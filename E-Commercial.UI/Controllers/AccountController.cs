using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commercial.UI.Entities;
using E_Commercial.UI.Models;
using E_Commercial.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commercial.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<CustomIdentityRole> _roleManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IAccountCookieService _accountCookieService;

        public AccountController(UserManager<CustomIdentityUser> userManager, RoleManager<CustomIdentityRole> roleManager, SignInManager<CustomIdentityUser> signInManager, IAccountCookieService accountCookieService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _accountCookieService = accountCookieService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new LoginAndRegisterViewModel
            {
                RegisterViewModel = new RegisterViewModel()
            };

            return View("LoginAndRegister",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(LoginAndRegisterViewModel loginAndRegisterViewModel)
        {
            ModelState.Remove("LoginViewModel");
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = loginAndRegisterViewModel.RegisterViewModel.UserName,
                    Email = loginAndRegisterViewModel.RegisterViewModel.Email
                };
                IdentityResult userResult = _userManager.CreateAsync(user, loginAndRegisterViewModel.RegisterViewModel.Password).Result;

                if (userResult.Succeeded)
                {
                    // check admin role is exist or not from roles
                    if (!_roleManager.RoleExistsAsync("Admin").Result)
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "Admin"
                        };
                        IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("CouldnotCreateRole", "Could not create role");
                            return View("LoginAndRegister", loginAndRegisterViewModel);
                        }
                    }

                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                    return RedirectToAction("Login", "Account");
                }
                TempData.Add("invalidRegister", "Invalid Information For Register");
                return RedirectToAction("Login", "Account");
            }
            TempData.Add("invalidRegister2", "Invalid Information For Register");
            return View("LoginAndRegister", loginAndRegisterViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginAndRegisterViewModel
            {
                LoginViewModel = new LoginViewModel()
            };

            var loginViewModel = _accountCookieService.GetAccountFromCookie();
            if (loginViewModel != null)
            {
                model.LoginViewModel.UserName = loginViewModel.UserName;
            }

            return View("LoginAndRegister", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginAndRegisterViewModel loginAndRegisterViewModel)
        {
            ModelState.Remove("RegisterViewModel");
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(loginAndRegisterViewModel.LoginViewModel.UserName, loginAndRegisterViewModel.LoginViewModel.Password, loginAndRegisterViewModel.LoginViewModel.RememberMe,false).Result;
                if (result.Succeeded)
                {
                    if (loginAndRegisterViewModel.LoginViewModel.RememberMe)
                    {
                        _accountCookieService.SetAccountToCookie(new LoginViewModel { UserName = loginAndRegisterViewModel.LoginViewModel.UserName });
                    }
                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("Login Failed","Invalid Login...");
            }

            TempData.Add("invalidLogin", "Invalid Information For Login");
            return View("LoginAndRegister", loginAndRegisterViewModel);
        }

        public IActionResult LogOff()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Login", "Account");
        }
    }
}