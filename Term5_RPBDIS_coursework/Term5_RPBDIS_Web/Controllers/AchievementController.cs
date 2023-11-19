using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class AchievementController : Controller {
        public IActionResult ShowTable([FromServices] AchievementService service) {
            ViewBag.data = service.Get("Achievement20");

            return View();
        }
    }
}
