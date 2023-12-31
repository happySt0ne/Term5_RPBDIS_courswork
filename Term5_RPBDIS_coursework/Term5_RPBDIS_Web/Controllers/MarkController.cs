﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;

namespace Term5_RPBDIS_Web.Controllers {
    public class MarkController : ExpandedController<Mark> {
        public MarkController(ValuatingSystemContext context) : base(context) { }

        [Authorize]
        public override IActionResult Create() {
            if (TryGetFromQuery("mark", out int? value)) {

                GetAdd(value);
            }

            return View();
        }

        [Authorize]
        public override IActionResult Update() {
            TryGetFromQuery("PageNumber", out int? PageNumber);

            if (!TryGetFromQuery("Id", out int? id)) {

                return RedirectToAction("ShowTable", "Mark", new { pageNumber = PageNumber });
            } 

            Mark mark = _context.Marks.Find(id);

            if (TryGetFromQuery("Value", out int? value)) {

                mark.Value = value;
            }

            _context.SaveChanges();

            return RedirectToAction("ShowTable", "Mark", new { pageNumber = PageNumber });
        }
    }
}
