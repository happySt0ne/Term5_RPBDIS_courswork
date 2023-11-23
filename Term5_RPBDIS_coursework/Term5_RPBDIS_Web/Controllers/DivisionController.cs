using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class DivisionController : ExpandedController<Division> {
        public DivisionController(ValuatingSystemContext context) : base(context) { }

        public override IActionResult Create() {
            if (TryGetFromQuery("Name", out string? name) &&
                TryGetFromQuery("Mark", out int? mark) &&
                TryGetFromQuery("StartDate", out DateTime? startDate) &&
                TryGetFromQuery("EndDate", out DateTime? endDate) &&
                TryGetFromQuery("Planned", out int? plannedEfficiency) &&
                TryGetFromQuery("Real", out int? realEfficiency)) {

                GetAdd(name, mark, startDate, endDate, plannedEfficiency, realEfficiency);
            }

            return View();
        }

        public override IActionResult Update() {
            throw new NotImplementedException();
        }
    }
}
