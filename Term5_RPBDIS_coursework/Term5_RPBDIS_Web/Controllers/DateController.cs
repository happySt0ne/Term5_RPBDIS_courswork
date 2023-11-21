using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class DateController : ExpandedController<Date> {
        public DateController([FromServices] ValuatingSystemContext context) : base(context) { }

        public override IActionResult Create() {
            if (!TryGetFromQuery("StartDate", out DateTime? startDate) || 
                !TryGetFromQuery("EndDate", out DateTime? endDate)) {

                return View();
            }

            Date date = new() {
                StartDate = startDate,
                EndDate = endDate
            };

            AddToDb(date);

            return View();
        }

        public override IActionResult Update() {
            throw new NotImplementedException();
        }
    }
}
