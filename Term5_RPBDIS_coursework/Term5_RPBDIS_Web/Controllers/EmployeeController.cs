using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
	public class EmployeeController : ExpandedController<Employee> {
		public EmployeeController([FromServices] ValuatingSystemContext context) : base(context) {
		}

		public override IActionResult Create() {
			if (TryGetFromQuery("Name", out string? name) &&
				TryGetFromQuery("HireDate", out DateTime? hireDate) &&
				TryGetFromQuery("Achievement", out string? achievement) &&
				TryGetFromQuery("Mark", out int? mark) &&
				TryGetFromQuery("DivisionMark", out int? divisionMark) &&
				TryGetFromQuery("DivisionName", out string? divisionName) &&
				TryGetFromQuery("StartDate", out DateTime? startDate) &&
				TryGetFromQuery("EndDate", out DateTime? endDate) &&
				TryGetFromQuery("Planned", out int? planned) && 
				TryGetFromQuery("Real", out int? real)) {

				GetAdd(name, hireDate, achievement, mark, divisionMark, divisionName, startDate, endDate, planned, real);
			}

			return View();
		}

		public override IActionResult Update() {
			throw new NotImplementedException();
		}
	}
}
