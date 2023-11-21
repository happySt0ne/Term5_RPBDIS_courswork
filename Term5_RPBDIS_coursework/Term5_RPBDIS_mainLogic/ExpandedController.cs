using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Term5_RPBDIS_library;
using Term5_RPBDIS_sql_library;

namespace Term5_RPBDIS_Web.Controllers {
    public abstract class ExpandedController<T> : Controller where T : class, ISqlTable {
        private const int CacheDuration = 264;
        protected ValuatingSystemContext _context;

        protected ExpandedController([FromServices] ValuatingSystemContext context) {
            _context = context;
        }

        public abstract IActionResult Create();
        public abstract IActionResult Update();

        public IActionResult Delete() {
            if (!TryGetFromQuery("Id", out int? id)) {

                return View();
            }

            DeleteFromDb(id);

            return View();
        }

        [ResponseCache(Duration = CacheDuration)]
        public IActionResult ShowTable() {
            ViewBag.data = _context.Set<T>().ToList();

            return View();
        }

        protected void DeleteFromDb(int? id) {
            T? entity = _context.Set<T>().Find(id);

            if (entity is null) return;

            DeleteFromDb(entity);
        }

        protected void DeleteFromDb(T recordToDelete) {
            _context.Set<T>().Remove(recordToDelete);
            _context.SaveChanges();
        }

        protected int AddToDb<T>(T recordToAdd) where T: class, ISqlTable {
            _context.Set<T>().Add(recordToAdd);
            _context.SaveChanges();

            return recordToAdd.ID;
        }

        /// <param name="key"> Ключ из запроса. </param>
        /// <param name="number"> Переменная, в которую может быть присвоено значение. </param>
        /// <returns>true, если <paramref name="number"/> было успешно присвоено значение.</returns>
        protected bool TryGetFromQuery(string key, out int? number) {
            if (!HttpContext.Request.Query.ContainsKey(key)) {

                number = null;
                return false;
            }

            number = int.Parse(HttpContext.Request.Query[key]);
            return true;
        }

        /// <param name="key"> Ключ из запроса. </param>
        /// <param name="str"> Переменная, в которую может быть присвоено значение. </param>
        /// <returns>true, если <paramref name="str"/> было успешно присвоено значение.</returns>
        protected bool TryGetFromQuery(string key, out string? str) {
            if (!HttpContext.Request.Query.ContainsKey(key)) {

                str = null;
                return false;
            }

            str = HttpContext.Request.Query[key];
            return true;
        }

        /// <param name="key"> Ключ из запроса. </param>
        /// <param name="dateTime"> Переменная, в которую может быть присвоено значение. </param>
        /// <returns>true, если <paramref name="dateTime"/> было успешно присвоено значение.</returns>
        protected bool TryGetFromQuery(string key, out DateTime? dateTime) {
            if (!HttpContext.Request.Query.ContainsKey(key)) {

                dateTime = null;
                return false;
            }

            dateTime = DateTime.Parse(HttpContext.Request.Query[key]);
            return true;
        }
    }
}
