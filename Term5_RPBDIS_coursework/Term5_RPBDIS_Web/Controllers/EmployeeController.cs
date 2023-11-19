using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_Web.Controllers {
    public class EmployeeController : Controller {
        public IActionResult ShowTable([FromServices] EmployeeService employeeCacheService) {
            ViewBag.Data = employeeCacheService.Get("Employee20");

            return View();
        }
    }
}
