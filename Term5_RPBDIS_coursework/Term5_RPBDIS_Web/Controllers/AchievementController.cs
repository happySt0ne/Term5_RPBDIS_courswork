﻿using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class AchievementController : ExpandedController<Achievement> {
        public AchievementController([FromServices] ValuatingSystemContext context) : base(context) { }

        public override IActionResult Create() {
            if (!TryGetFromQuery("Text", out string? text)) {
                
                return View();
            }

            Achievement achievement = new() {
                Text = text
            };

            AddToDb(achievement);

            return View();
        }

        public override IActionResult Update() {
            throw new NotImplementedException();
        }
    }
}
