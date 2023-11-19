using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class PlannedEfficiencyController : Controller {
        public IActionResult ShowTable([FromServices] PlannedEfficiencyService plannedEfficiencyCacheService) {
            ViewBag.Data = plannedEfficiencyCacheService.Get("PlannedEfficiency20");

            return View();
        }
    }
}
