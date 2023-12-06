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
            var users = await _userManager.Users.ToListAsync();
            
            foreach (var user in users) {

                ViewData[$"Roles_{user.Id}"] = await _userManager.GetRolesAsync(user);
            }

            ViewBag.Users = users;
            return View();
        }

        public IActionResult Login() => View();

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
                // TODO: ссылка на регистрацию, ссылка на вход, скрытая вкладка для просмотра учетных записей и CRUD для неё.

                return View(model); 
            }

            model.Rights = model.Rights ?? false;
            
            await _userManager.AddToRoleAsync(user, (bool)model.Rights ? "Admin" : "User");

            return RedirectToAction("Index", "Home");         
        }
    }
}
