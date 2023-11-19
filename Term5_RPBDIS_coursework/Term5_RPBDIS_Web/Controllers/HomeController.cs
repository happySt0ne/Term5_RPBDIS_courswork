using Microsoft.AspNetCore.Mvc;

namespace Term5_RPBDIS_Web.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
