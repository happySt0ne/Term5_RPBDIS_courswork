﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Linq.Dynamic.Core;
using Term5_RPBDIS_library;
using Term5_RPBDIS_mainLogic.sessionStuff;

namespace Term5_RPBDIS_Web.Controllers {
    public class SearchController : Controller {

        [HttpGet]
        public IActionResult Form1() {
            if (TryGetCookie("choosingList", out string? chosenTable) &&
                TryGetCookie("column", out string? chosenColumn) &&
                TryGetCookie("textForSearch", out string? textForSearch)) {

                if (!Find(chosenTable, chosenColumn, textForSearch, out List<string> result)) {

                    result.Add(ColumnNotFoundHandler(GetColumnNames(chosenTable)));
                }

                ViewBag.Response = result;

                ViewBag.Table = chosenTable;
                ViewBag.Column = chosenColumn;
                ViewBag.TextForSearch = textForSearch;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Form1(int? id) {
            bool isChosenTableNotNull = TryGetFromServer("choosingList", out string? chosenTable);
            bool isChosenColumnNotNull = TryGetFromServer("column", out string? chosenColumn);
            bool isTextForSearchNotNull = TryGetFromServer("textForSearch", out string? textForSearch);

            if (isChosenTableNotNull && isChosenColumnNotNull && isTextForSearchNotNull) {

                if (!Find(chosenTable, chosenColumn, textForSearch, out List<string> result)) {

                    result.Add(ColumnNotFoundHandler(GetColumnNames(chosenTable)));
                }

                ViewBag.Response = result;

                Response.Cookies.Append("choosingList", chosenTable);
                Response.Cookies.Append("column", chosenColumn);
                Response.Cookies.Append("textForSearch", textForSearch);
            }
            
            ViewBag.Table = chosenTable;
            ViewBag.Column = chosenColumn;
            ViewBag.TextForSearch = textForSearch;


            return View();
        }

        [HttpGet]
        public IActionResult Form2() {
            var searchSession = HttpContext.Session.Get<SearchSession>("searchSession") ?? new SearchSession();
            
            if (searchSession.isSaved) {

                if (!Find(searchSession.tableName,searchSession.columnName,
                          searchSession.textForSearch, out List<string> result)) {

                    result.Add(ColumnNotFoundHandler(GetColumnNames(searchSession.tableName)));
                }

                ViewBag.Response = result;
            }

            return View(searchSession);
        }

        [HttpPost]
        public IActionResult Form2(int? id) {
            var searchSession = HttpContext.Session.Get<SearchSession>("searchSession") ?? new SearchSession();

            if (TryGetFromServer(searchSession)) {

                if (!Find(searchSession.tableName, searchSession.columnName,
                          searchSession.textForSearch, out List<string> result)) {

                    result.Add(ColumnNotFoundHandler(GetColumnNames(searchSession.tableName)));
                }

                ViewBag.Response = result;
            }

            return View(searchSession);
        }

        private bool TryGetFromServer(SearchSession session) {
            bool isContainTable = HttpContext.Request.Form.ContainsKey("choosingList");
            bool isContainColumn = HttpContext.Request.Form.ContainsKey("column");
            bool isContainText = HttpContext.Request.Form.ContainsKey("textForSearch");

            if (!isContainColumn || !isContainTable || !isContainText) return false;
            
            var table = HttpContext.Request.Form["choosingList"];
            var column = HttpContext.Request.Form["column"];
            var textForSearch = HttpContext.Request.Form["textForSearch"];

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
            if (!HttpContext.Request.Form.ContainsKey(key)) {  

                res = null;
                return false;
            }

            res = HttpContext.Request.Form[key];
            HttpContext.Response.Cookies.Append(key, res);
            return true;
        }

        private string GetPropertyValues(List<string> columnNames, object item) {
            string result = "";

            foreach (var column in columnNames) {
                
                result += GetPropertyValue(item, column) + " ";
            }

            return result;
        }

        private string GetPropertyValue(object item, string columnName) =>
            item.GetType().GetProperty(columnName).GetValue(item, null).ToString();

        private bool Find(string chosenTable, string chosenColumn, 
                          string textForSearch, out List<string> result) {
            List<string> columnNames = GetColumnNames(chosenTable);
            result = new();

            if (!columnNames.Contains(chosenColumn)) return false;
            
            var table = GetTable(chosenTable);
                
            foreach (var item in table) {

                if (GetPropertyValue(item, chosenColumn).Contains(textForSearch)) {

                    result.Add(GetPropertyValues(columnNames, item));
                }
            }

            return true;
        }

        private string ColumnNotFoundHandler(List<string> columnNames) {
            string answer = "";

            answer += "Такого столбца нет в выбранной таблице.\n";
            answer += "Вы можете выбрать из:\n";

            columnNames.ForEach(col => answer += $"{col}, ");

            return answer[..^2];
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
            return (IQueryable)dbContext
                .GetType()
                .GetMethod("Set", Type.EmptyTypes)
                .MakeGenericMethod(modelType)
                .Invoke(dbContext, null);
        }
    }
}
