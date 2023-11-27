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
            ViewBag.Marks = _context.Marks.ToList();

            if (!TryGetFromQuery("Id", out int? id)) return View();
            
            Mark mark = _context.Marks.Find(id);

            if (TryGetFromQuery("Value", out int? value)) { 
            
                mark.Value = value;
            }
            
            _context.SaveChanges();

            return View();
        }
    }
}
