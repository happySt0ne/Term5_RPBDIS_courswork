using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Term5_RPBDIS_Web.ViewModels;

namespace Term5_RPBDIS_Web.Controllers {
    // TODO: попробуй потом ограничить данные на странице Index. Типа для админа пусть там будет ещё вкладка для управления учетными записями пользователей.
    public class AccountController : Controller {
        private UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager) {
            _userManager = userManager;
        }

        public async Task<IActionResult> UsersList() {
            ViewBag.Users = await _userManager.Users.ToListAsync();

            return View();
        }

        public IActionResult Login() => View();

        [HttpGet]
        public IActionResult Register() => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) {
            var user = new IdentityUser { PhoneNumber = model.PhoneNumber };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model); // Возвращение на страницу регистрации с сообщениями об ошибках
            }

            // Тут менять буду. 
            if (_userManager.Users.Count() == 1) {
                await _userManager.AddToRoleAsync(user, "Admin");
            } else {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return RedirectToAction("Index", "Home"); // Перенаправление на главную страницу после успешной регистрации
        }

    }
}
