using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class PlannedEfficiencyController : ExpandedController<PlannedEfficiency> {
        public PlannedEfficiencyController(ValuatingSystemContext context) : base(context) { }

        public override IActionResult Create() {
            if (!TryGetFromQuery("Efficiency", out int? efficiency) ||
                !TryGetFromQuery("StartDate", out DateTime? startDate) ||
                !TryGetFromQuery("EndDate", out DateTime? endDate)) {

                return View();
            }

            if (!IsRecordExist<Date>(x => x.EndDate == endDate && x.StartDate == startDate, out int? dateId)) {
                Date date = new() {
                    StartDate = startDate,
                    EndDate = endDate
                };

                dateId = AddToDb(date);
            }
            
            PlannedEfficiency plannedEfficiency = new() {
                DateId = dateId,
                Efficiecy = efficiency,
            };

            AddToDb(plannedEfficiency);

            return View();
        }
    
        public override IActionResult Update() {
            throw new NotImplementedException();
        }
    }
}
