using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class MarkController : Controller {
        public IActionResult ShowTable([FromServices] MarkService markCacheService) {
            ViewBag.Data = markCacheService.Get("Mark20");

            return View();
        }
    }
}
