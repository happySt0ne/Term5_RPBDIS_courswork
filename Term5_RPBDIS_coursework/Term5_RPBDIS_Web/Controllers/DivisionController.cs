using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class DivisionController : Controller {
        public IActionResult ShowTable([FromServices] DivisionService divisionCacheService) {
            ViewBag.Data = divisionCacheService.Get("Division20");

            return View();
        }
    }
}
