using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
	public class EmployeeController : ExpandedController<Employee> {
		public EmployeeController(ValuatingSystemContext context) : base(context) { }

        [Authorize]
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

        [Authorize]
        public override IActionResult Update() {
			ViewBag.Employees = _context.Employees.ToList();
			ViewBag.Divisions = _context.Divisions.ToList();
			ViewBag.Achievements = _context.Achievements.ToList();
			ViewBag.Marks = _context.Marks.ToList();

			if (!TryGetFromQuery("Id", out int? id)) return View();


			Employee employee = _context.Employees.Find(id);

			if (TryGetFromQuery("Name", out string? name)) {

				employee.Name = name;
			}
			if (TryGetFromQuery("DivisionId", out int? divisionId)) {

				employee.DivisionId = divisionId;
			}
			if (TryGetFromQuery("HireDate", out DateTime? hireDate)) {

				employee.HireDate = hireDate;
			}
			if (TryGetFromQuery("AchievementId", out int? achievementId)) {

				employee.AchievementId = achievementId;
			}
			if (TryGetFromQuery("MarkId", out int? markId)) {

				employee.MarkId = markId;
			}

			_context.SaveChanges();

			return View();
		}
	}
}
