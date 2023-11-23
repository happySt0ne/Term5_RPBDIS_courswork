using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class MarkController : ExpandedController<Mark> {
        public MarkController(ValuatingSystemContext context) : base(context) { }

        public override IActionResult Create() {
            if (TryGetFromQuery("mark", out int? value)) {

                GetAdd(value);
            }

            return View();
        }

        public override IActionResult Update() {
            throw new NotImplementedException();
        }
    }
}
