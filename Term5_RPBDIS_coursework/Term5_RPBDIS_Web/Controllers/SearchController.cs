using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Dynamic.Core;
using Term5_RPBDIS_library;
using Term5_RPBDIS_mainLogic.sessionStuff;

namespace Term5_RPBDIS_Web.Controllers {
    public class SearchController : Controller {
        public IActionResult Form1() {
            bool isChosenTableNotNull = TryGet("choosingList", out string? chosenTable);
            bool isChosenColumnNotNull = TryGet("column", out string? chosenColumn);
            bool isTextForSearchNotNull = TryGet("textForSearch", out string? textForSearch);

            if (isChosenTableNotNull && isChosenColumnNotNull && isTextForSearchNotNull) {

                ViewBag.Response = Find(chosenTable, chosenColumn, textForSearch);
            }

            ViewBag.Table = chosenTable; 
            ViewBag.Column = chosenColumn;
            ViewBag.TextForSearch = textForSearch;

            return View();
        }
            
        public IActionResult Form2() {
            var searchSession = HttpContext.Session.Get<SearchSession>("searchSession") ?? new SearchSession();

            if (searchSession.isSaved || TryGetFromServer(searchSession)) {

                ViewBag.Response = Find(searchSession.tableName, searchSession.columnName, searchSession.textForSearch);
            }

            return View(searchSession);
        }

        private bool TryGetFromServer(SearchSession session) {
            bool isContainTable = HttpContext.Request.Query.ContainsKey("choosingList");
            bool isContainColumn = HttpContext.Request.Query.ContainsKey("column");
            bool isContainText = HttpContext.Request.Query.ContainsKey("textForSearch");

            if (!isContainColumn || !isContainTable || !isContainText) {
                return false;
            }

            var table = HttpContext.Request.Query["choosingList"];
            var column = HttpContext.Request.Query["column"];
            var textForSearch = HttpContext.Request.Query["textForSearch"];

            session.Save(table, column, textForSearch, HttpContext);
            return true;
        }

        private bool TryGet(string key, out string? res) =>
            TryGetCookie(key, out res) || TryGetFromServer(key, out res);
        

        private bool TryGetCookie(string key, out string? res) {
            if (HttpContext.Request.Cookies.ContainsKey(key)) {

                res = HttpContext.Request.Cookies[key];
                return true;
            }

            res = null;
            return false;
        }

        private bool TryGetFromServer(string key, out string? res) {
            if (!HttpContext.Request.Query.ContainsKey(key)) {

                res = null;
                return false;
            }

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

            answer.Add("Такого столбца нет в выбранной таблице.");
            answer.Add("Вы можете выбрать из:");

            foreach (var a in columnNames) {
                answer.Add(a);
            }

            return answer;
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
