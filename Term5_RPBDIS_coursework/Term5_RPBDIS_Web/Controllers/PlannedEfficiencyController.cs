using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class PlannedEfficiencyController : ExpandedController<PlannedEfficiency> {
        public PlannedEfficiencyController([FromServices] ValuatingSystemContext context) : base(context) {
        }

        public override IActionResult Create() {
            throw new NotImplementedException();
        }

        public override IActionResult Update() {
            throw new NotImplementedException();
        }
    }
}
