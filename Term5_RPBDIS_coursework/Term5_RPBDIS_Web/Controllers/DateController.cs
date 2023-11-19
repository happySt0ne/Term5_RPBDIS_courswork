using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class DateController : Controller {
        public IActionResult ShowTable([FromServices] DateService dateCacheService) {
            ViewBag.Data = dateCacheService.Get("Date20");

            return View();
        }
    }
}
