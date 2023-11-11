﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Term5_RPBDIS_library;

namespace Term5_RPBDIS_mainLogic {
    public static partial class Middlewares {
        public static class Search {
            public static void ShowForm1(IApplicationBuilder app) {
                app.Run(async context => {
                    string chosenTable = context.Request.Query["choosingList"];
                    string column = context.Request.Query["column"];
                    string textForSearch = context.Request.Query["textForSearch"];

                    string answ =
                    $"<html> <head>Поиск по таблицам</head>" +
                    $"<META http-equiv='Content-Type' content='text/html; charset=utf-8' /><body> " +
                    $"<form action = /searchform1> " +

                    $"<label for='choosingList'>Выберите таблицу для поиска:<br></label>" +
                    $"<select name='choosingList' id='choosingList' required >" +
                    $"<option value='Achievement'>Достижения</option>" +
                    $"<option value='Date'>Даты</option>" +
                    $"<option value='Division'>Отделы</option>" +
                    $"<option value='Employee'>Работники</option>" +
                    $"<option value='Term5_RPBDIS_libraryMark'>Оценки</option>" +
                    $"<option value='PlannedEfficiecy'>Планируемая эффективность</option>" +
                    $"<option value='RealEfficiecy'>Реальная эффективность</option>" +
                    $"</select><br> " +

                    $"<label for='column'>Введите название столбца для поиска по таблице:<br></label>" +
                    $"<input type='text' name='column' id='column' required /> <br>" +

                    $"<label for='textForSearch'>Введите фрагмент для поиска: <br></label>" +
                    $"<input type='text' name='textForSearch' id='textForSearch' required /> <br>" +

                    $"<input type='submit' value='найти' >" +
                    $"</form> </body> </html>";

                    if (chosenTable is not null) {
                        // TODO: теперь нужно отрефакторить. Все миддлвары нужно раскинуть по своим классам доступа.
                        answ += Find(chosenTable, column, textForSearch, context);
                    }

                    await context.Response.WriteAsync(answ);
                });
            }

            public static void ShowForm2(IApplicationBuilder app) {
                app.Run(async context => {
                    string chosenTable = context.Request.Query["choosingList"];
                    string column = context.Request.Query["column"];
                    string textForSearch = context.Request.Query["textForSearch"];

                    string answ =
                    $"<html> <head>Поиск по таблицам</head>" +
                    $"<META http-equiv='Content-Type' content='text/html; charset=utf-8' /><body> " +
                    $"<form action = /searchform1> " +

                    $"<label for='choosingList'>Выберите таблицу для поиска:<br></label>" +
                    $"<select name='choosingList' id='choosingList' required >" +
                    $"<option value='Achievement'>Достижения</option>" +
                    $"<option value='Date'>Даты</option>" +
                    $"<option value='Division'>Отделы</option>" +
                    $"<option value='Employee'>Работники</option>" +
                    $"<option value='Term5_RPBDIS_libraryMark'>Оценки</option>" +
                    $"<option value='PlannedEfficiecy'>Планируемая эффективность</option>" +
                    $"<option value='RealEfficiecy'>Реальная эффективность</option>" +
                    $"</select><br> " +

                    $"<label for='column'>Введите название столбца для поиска по таблице:<br></label>" +
                    $"<input type='text' name='column' id='column' required /> <br>" +

                    $"<label for='textForSearch'>Введите фрагмент для поиска: <br></label>" +
                    $"<input type='text' name='textForSearch' id='textForSearch' required /> <br>" +

                    $"<input type='submit' value='найти' >" +
                    $"</form> </body> </html>";

                    if (chosenTable is not null) {
                        // TODO: теперь нужно отрефакторить. Все миддлвары нужно раскинуть по своим классам доступа.
                        answ += Find(chosenTable, column, textForSearch, context);
                    }

                    await context.Response.WriteAsync(answ);
                });
            }

            private static string Find(string chosenTable, string chosenColumn, string textForSearch, HttpContext context) {
                string answ = "";
                List<string> columnNames = GetColumnNames(chosenTable, context);

                if (columnNames.Contains(chosenColumn)) {

                    var table = GetTable(chosenTable, context);

                    foreach (var item in table.Select(chosenColumn)) {

                        if (item.ToString().Contains(textForSearch)) {

                            answ += $"<li>{item}</li>";
                        }
                    }

                    return answ;
                }

                answ += "<p style='color:red;'>Такого столбца нет в выбранной таблице.</p>";
                answ += "Вы можете выбрать из:";

                foreach (var a in columnNames) {

                    answ += $"<li>{a}</li>";
                }

                return answ;
            }

            private static IQueryable GetTable(string tableName, HttpContext context) {
                var fullTableName = $"Term5_RPBDIS_library.models.tables.{tableName}, Term5_RPBDIS_sql_library";
                var dbContext = context.RequestServices.GetService<ValuatingSystemContext>();

                var modelType = Type.GetType(fullTableName);
                return (IQueryable)dbContext.GetType().GetMethod("Set", Type.EmptyTypes).MakeGenericMethod(modelType).Invoke(dbContext, null);
            }

            private static List<string> GetColumnNames(string tableName, HttpContext context) {
                var fullTableName = $"Term5_RPBDIS_library.models.tables.{tableName}, Term5_RPBDIS_sql_library";
                var dbContext = context.RequestServices.GetService<ValuatingSystemContext>();

                var modelType = Type.GetType(fullTableName);
                var entityType = dbContext.Model.FindEntityType(modelType);

                return entityType.GetProperties().Select(x => x.Name).ToList();
            }
        }
    }
}