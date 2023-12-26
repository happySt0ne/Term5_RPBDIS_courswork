using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Term5_RPBDIS_library;

namespace Term5_RPBDIS_Web.Controllers {
    [Authorize(Roles = "Admin")]
    public class RatingController : Controller {
        private ValuatingSystemContext _context;

        public RatingController(ValuatingSystemContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Employees(int? id) { 
            ViewBag.Divisions = await _context.Divisions.ToListAsync();
            ViewBag.Dates = await _context.Dates.ToListAsync();
            ViewBag.Method = "get";

            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Employees() {
            var divisionId = int.Parse(HttpContext.Request.Form["DivisionId"]);
            var dateId = int.Parse(HttpContext.Request.Form["DateId"]);
                
            ViewBag.Method = "post";

            ViewBag.Employees = await _context.Employees
                .Where(e => e.DivisionId == divisionId)
                .Where(e => e.Division.RealEfficiency.DateId == dateId)
                .OrderByDescending(x => x.Mark.Value)
                .ToListAsync();

            return View();
        }
    }
}
