using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class RealEfficiencyController : ExpandedController<RealEfficiency> {
        public RealEfficiencyController(ValuatingSystemContext context) : base(context) { }

        [Authorize]
        public override IActionResult Create() {
            if (TryGetFromQuery("Efficiency", out int? efficiency) &&
                TryGetFromQuery("StartDate", out DateTime? startDate) &&
                TryGetFromQuery("EndDate", out DateTime? endDate)) {

                GetAddReal(efficiency, startDate, endDate);
            }

            return View();
        }

        [Authorize]
        public override IActionResult Update() {
            TryGetFromQuery("PageNumber", out int? PageNumber);

            if (!TryGetFromQuery("Id", out int? id)) {

                return RedirectToAction("ShowTable", "RealEfficiency", new { pageNumber = PageNumber });
            } 

            RealEfficiency realEfficiencies = _context.RealEfficiencies.Find(id);

            if (TryGetFromQuery("Date", out int? dateId)) {

                realEfficiencies.Date = _context.Dates.Find(dateId);
            }

            if (TryGetFromQuery("Efficiecy", out int? efficiecy)) {

                realEfficiencies.Efficiecy = efficiecy;
            }

            _context.SaveChanges();

            return RedirectToAction("ShowTable", "RealEfficiency", new { pageNumber = PageNumber });
        }
    }
}
