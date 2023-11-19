using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using Term5_RPBDIS_library;

namespace Term5_RPBDIS_Web.Controllers {
    public class SearchController : Controller {
        public IActionResult Form1() {
            TryGetCookie("choosingList", out string? table);
            TryGetCookie("column", out string? column);
            TryGetCookie("textForSearch", out string? textForSearch);

            ViewBag.Table = table; 
            ViewBag.Column = column;
            ViewBag.TextForSearch = textForSearch;

            return View();
        }

        public IActionResult Response() {
            bool isChosenTableNotNull = TryGet("choosingList", out string? chosenTable);
            bool isChosenColumnNotNull = TryGet("column", out string? chosenColumn);
            bool isTextForSearchNotNull = TryGet("textForSearch", out string? textForSearch);
            // TODO: Сделать Session.
            // TODO: Сделать круды
            // TODO: также хотелось бы выводить название таблицы и столбца по которому искали, сам всё увидишь.
            if (isChosenTableNotNull && isChosenColumnNotNull && isTextForSearchNotNull) {

                ViewBag.Response = Find(chosenTable, chosenColumn, textForSearch);
            }

            return View();
        }

        private bool TryGet(string key, out string? res) {
            if (!HttpContext.Request.Query.ContainsKey(key)) {
                
                res = null;
                return false;
            }

            return TryGetCookie(key, out res) || TryGetFromServer(key, out res);
        }

        private bool TryGetCookie(string key, out string? res) {
            if (HttpContext.Request.Cookies.ContainsKey(key)) {

                res = HttpContext.Request.Cookies[key];
                return true;
            }

            res = null;
            return false;
        }

        private bool TryGetFromServer(string key, out string? res) {
            res = HttpContext.Request.Query[key];
            HttpContext.Response.Cookies.Append(key, res);
            return true;
        }

        private List<string> Find(string chosenTable, string chosenColumn, string textForSearch) {
            List<string> columnNames = GetColumnNames(chosenTable);
            List<string> answer = new();

            if (columnNames.Contains(chosenColumn)) {

                var table = GetTable(chosenTable);

                foreach (var item in table.Select(chosenColumn)) {

                    if (item.ToString().Contains(textForSearch)) {

                        answer.Add(item.ToString()) ;
                    }
                }

                return answer;
            }

            throw new Exception("Такого столбца нет в выбранной таблице.");
        }

        private List<string> GetColumnNames(string tableName) {
            var fullTableName = $"Term5_RPBDIS_library.models.tables.{tableName}, Term5_RPBDIS_sql_library";
            var dbContext = HttpContext.RequestServices.GetService<ValuatingSystemContext>();

            var modelType = Type.GetType(fullTableName);
            var entityType = dbContext.Model.FindEntityType(modelType);

            return entityType.GetProperties().Select(x => x.Name).ToList();
        }

        private IQueryable GetTable(string tableName) {
            var fullTableName = $"Term5_RPBDIS_library.models.tables.{tableName}, Term5_RPBDIS_sql_library";
            var dbContext = HttpContext.RequestServices.GetService<ValuatingSystemContext>();

            var modelType = Type.GetType(fullTableName);
            return (IQueryable)dbContext.GetType().GetMethod("Set", Type.EmptyTypes).MakeGenericMethod(modelType).Invoke(dbContext, null);
        }
    }
}
