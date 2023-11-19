using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_Web.Controllers {
    public abstract class ExpandedController<T> : Controller where T : class, ISqlTable {
        private const int CacheDuration = 264;

        [ResponseCache(Duration = CacheDuration)]
        public IActionResult ShowTable([FromServices] ValuatingSystemContext valuatingSystemContext) {
            ViewBag.data = valuatingSystemContext.Set<T>().ToList();

            return View();
        }
    }
}
