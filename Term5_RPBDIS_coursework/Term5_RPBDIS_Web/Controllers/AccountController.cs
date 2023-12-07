using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Term5_RPBDIS_Web.ViewModels.AccountViewModels;

namespace Term5_RPBDIS_Web.Controllers
{
    public class AccountController : Controller {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult AccessDenied(string returnUrl) => RedirectToAction("Index", "Home");

        [HttpGet]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(RegisterViewModel model) {
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

            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateAccountViewModel model) {
            IdentityUser user = await _userManager.FindByNameAsync(model.OldPhoneNumber);

            if (user is null) {

                ModelState.AddModelError("", "Пользователь не найден.");
                return View(model);
            }

            model.NewRights = model.NewRights ?? false;

            user.PhoneNumber = model.NewPhoneNumber ?? user.PhoneNumber;
            user.UserName = model.NewPhoneNumber ?? user.UserName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) {

                foreach (var error in result.Errors) {

                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }

            if ((bool)model.NewRights) {

                await _userManager.RemoveFromRoleAsync(user, "User");
                await _userManager.AddToRoleAsync(user, "Admin");
            } else {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
                await _userManager.AddToRoleAsync(user, "User");
            }

            return View();
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete() => View();

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(DeleteAccountViewModel model) {
            var user = await _userManager.FindByNameAsync(model.PhoneNumber);
            
            if (user is null) {

                ModelState.AddModelError("", "Пользователь не найден.");
                return View(model);
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) {

                foreach (var error in result.Errors) {

                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
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
            if (!ModelState.IsValid) {

                foreach (var modelState in ViewData.ModelState.Values) {

                    foreach (var error in modelState.Errors) {

                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
                return View(model);
            }

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
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");         
        }
    }
}
