using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class DateController : ExpandedController<Date> {
        public DateController([FromServices] ValuatingSystemContext context) : base(context) { }

        [Authorize]
        public override IActionResult Create() {
            if (TryGetFromQuery("StartDate", out DateTime? startDate) &&
                TryGetFromQuery("EndDate", out DateTime? endDate)) {

                GetAdd(startDate, endDate);
            }

            return View();
        }

        [Authorize]
        public override IActionResult Update() {
            ViewBag.Dates = _context.Dates.ToList();

            if (!TryGetFromQuery("Id", out int? id)) return View();

            Date date = _context.Dates.Find(id);

            if (TryGetFromQuery("StartDate", out DateTime? startDate)) {

                date.StartDate = startDate;
            }

            if (TryGetFromQuery("EndDate", out DateTime? endDate)) {

                date.EndDate = endDate;
            }

            _context.SaveChanges();

            return View();
        }
    }
}
