﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Term5_RPBDIS_Web.ViewModels;

namespace Term5_RPBDIS_Web.Controllers {
    public class AccountController : Controller {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult AccessDenied(string returnUrl) => RedirectToAction("Index", "Home");

        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UsersList() {
            var users = await _userManager.Users.ToListAsync();
            
            foreach (var user in users) {

                ViewData[$"Roles_{user.Id}"] = await _userManager.GetRolesAsync(user);
            }

            ViewBag.Users = users;
            return View();
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            var result = await _signInManager.PasswordSignInAsync(model.PhoneNumber, model.Password, false, false);
            
            if (result.Succeeded) return RedirectToAction("Index", "Home");
            
            ModelState.AddModelError("", "Неверноый номер телефона или пароль.");
            
            return View(model);
        }

        [HttpGet]
        public IActionResult Register() => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            IdentityUser user = new() { UserName = model.PhoneNumber, PhoneNumber = model.PhoneNumber };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model); 
            }

            model.Rights = model.Rights ?? false;
            
            await _userManager.AddToRoleAsync(user, (bool)model.Rights ? "Admin" : "User");

            return RedirectToAction("Index", "Home");         
        }
    }
}
