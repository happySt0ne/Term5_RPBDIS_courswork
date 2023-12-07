using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class AchievementController : ExpandedController<Achievement> {
        public AchievementController(ValuatingSystemContext context) : base(context) { }
        [Authorize]
        public override IActionResult Create() {
            if (TryGetFromQuery("Text", out string? text)) {

                GetAdd(text);
            }

            return View();
        }

        [Authorize]
        public override IActionResult Update() {
            ViewBag.Achievements = _context.Achievements.ToList();

            if (!TryGetFromQuery("Id", out int? id)) return View();
            
            Achievement achievement = _context.Achievements.Find(id);
            
            if (TryGetFromQuery("Text", out string? text)) {
                
                achievement.Text = text;
            }
            
            _context.SaveChanges();

            return View();
        }
    }
}
