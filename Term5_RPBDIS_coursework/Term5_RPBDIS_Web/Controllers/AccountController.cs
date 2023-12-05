using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Term5_RPBDIS_Web.Controllers {
    public class AccountController : Controller {
        [Authorize]
        public IActionResult Index() {
            return View();
        }
        public IActionResult Login() {
            return View();
        }

        public IActionResult Register() {
            return View();
        }
    }
}
