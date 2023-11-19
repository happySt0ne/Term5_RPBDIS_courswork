using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class RealEfficiencyController : Controller {
        public IActionResult ShowTable([FromServices] RealEfficiencyService realEfficiencyCacheService) {
            ViewBag.Data = realEfficiencyCacheService.Get("RealEfficiency20");

            return View();
        }
    }
}
