using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class DivisionController : ExpandedController<Division> {
        public DivisionController(ValuatingSystemContext context) : base(context) { }

        [Authorize]
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

        [Authorize]
        public override IActionResult Update() {
            ViewBag.Divisions = _context.Divisions.ToList();
            ViewBag.PlannedEfficiencies = _context.PlannedEfficiencies.ToList();
            ViewBag.RealEfficiencies = _context.RealEfficiencies.ToList();
            ViewBag.Marks = _context.Marks.ToList();

            if (!TryGetFromQuery("Id", out int? id)) return View();

            Division division = _context.Divisions.Find(id);

            if (TryGetFromQuery("Name", out string? name)) {

                division.Name = name;
            }
            if (TryGetFromQuery("MarkId", out int? markId)) {

                division.MarkId = markId;
            }
            if (TryGetFromQuery("PlannedEfficiencyId", out int? plannedEfficiencyId)) {

                division.PlannedEfficiencyId = plannedEfficiencyId;
            }
            if (TryGetFromQuery("RealEfficiencyId", out int? realEfficiencyId)) {

                division.RealEfficiencyId = realEfficiencyId;
            }

            _context.SaveChanges();

            return View();
        }
    }
}
