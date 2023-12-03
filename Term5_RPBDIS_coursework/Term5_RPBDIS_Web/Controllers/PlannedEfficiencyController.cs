using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class PlannedEfficiencyController : ExpandedController<PlannedEfficiency> {
        public PlannedEfficiencyController(ValuatingSystemContext context) : base(context) { }

        public override IActionResult Create() {
            if (TryGetFromQuery("Efficiency", out int? efficiency) &&
                TryGetFromQuery("StartDate", out DateTime? startDate) &&
                TryGetFromQuery("EndDate", out DateTime? endDate)) {

                

                GetAddPlanned(efficiency, startDate, endDate);
            }

            return View();
        }
    
        public override IActionResult Update() {
            ViewBag.PlannedEfficiencies = _context.PlannedEfficiencies.ToList();
            ViewBag.Dates = _context.Dates.ToList();

            if (!TryGetFromQuery("Id", out int? id)) return View();

            PlannedEfficiency plannedEfficiencies = _context.PlannedEfficiencies.Find(id);

            if (TryGetFromQuery("Date", out int? dateId)) {

                plannedEfficiencies.Date = _context.Dates.Find(dateId);
            }

            if (TryGetFromQuery("Efficiecy", out int? efficiecy)) {

                plannedEfficiencies.Efficiecy = efficiecy;
            }

            _context.SaveChanges();

            return View();
        }
    }
}
