using Microsoft.AspNetCore.Mvc;

namespace Term5_RPBDIS_Web.Controllers {
    public class HomeController : Controller {
        public IActionResult Index() => View();

        [Route("/info")]
        [Route("/home/info")]
        public IActionResult Info() {
            ViewBag.RemoteIp = HttpContext.Connection.RemoteIpAddress;
            ViewBag.ClientLanguage = HttpContext.Request.Headers["Accept-Language"];
            ViewBag.ClientBrowser = HttpContext.Request.Headers["User-Agent"];

            return View();
        }
    }
}
