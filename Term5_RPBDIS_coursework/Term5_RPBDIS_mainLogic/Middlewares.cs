using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Term5_RPBDIS_library;
using Term5_RPBDIS_library.models.tables;
using Term5_RPBDIS_mainLogic.Services;

namespace Term5_RPBDIS_mainLogic {
    public static class Middlewares {
        public static void GetClientInfo(IApplicationBuilder app) {
            app.Run(async context => {
                string answer = "";
                answer += "remote ip:\n";
                answer += context.Connection.RemoteIpAddress + "\n\n";

                answer += "client language:\n";
                answer += context.Request.Headers["Accept-Language"] + "\n\n";

                answer += "client browser info:\n";
                answer += context.Request.Headers["User-Agent"] + "\n\n";

                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowAchievement(IApplicationBuilder app) {
            app.Run(async context => {
                var achivementCache = context.RequestServices.GetService<AchievementService>()?.Get("Achievements20");

                string answer = "" +
                "<HTML>" + 
                "<HEAD> <Title>Достижения</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список достижений</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Текст</th>" +
                        "</tr>";

                foreach (Achievement achievement in achivementCache) {
                    answer += "<tr>";
                    answer += "<td>" + achievement.ID + "</td>";
                    answer += "<td>" + achievement.Text + "</td>";
                    answer += "</tr>";
                }
                
                answer += "</table> </body> </html>";
                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowDate(IApplicationBuilder app) {
            app.Run(async context => {
                var dateCache = context.RequestServices.GetService<DateService>().Get("Dates20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Даты</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список дат</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Начальная дата</th>" +
                            "<th>Конечная дата</th>" +
                        "</tr>";

                foreach (Date date in dateCache) {
                    answer += "<tr>";
                    answer += "<td>" + date.ID + "</td>";
                    answer += "<td>" + date.StartDate+ "</td>";
                    answer += "<td>" + date.EndDate + "</td>";
                    answer += "</tr>";
                }

                answer += "</table> </body> </html>";
                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowDivision(IApplicationBuilder app) {
            app.Run(async context => {
                var divisionCache = context.RequestServices.GetService<DivisionService>().Get("Division20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Отделы</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список отделов</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Название</th>" +
                            "<th>Оценка</th>" +
                            "<th>Планируемая эффективность</th>" +
                            "<th>Реальная эффективность</th>" +
                            "<th>Начальная дата оценки</th>" +
                            "<th>Конечная дата оценки</th>" +
                        "</tr>";

                foreach (Division division in divisionCache) {
                    answer += "<tr>";
                    answer += "<td>" + division.ID + "</td>";
                    answer += "<td>" + division.Name + "</td>";
                    answer += "<td>" + division.Mark?.Value + "</td>";
                    answer += "<td>" + division.PlannedEfficiency?.Efficiecy + "</td>";
                    answer += "<td>" + division.RealEfficiency?.Efficiecy + "</td>";
                    answer += "<td>" + division.RealEfficiency?.Date?.StartDate + "</td>";
                    answer += "<td>" + division.RealEfficiency?.Date?.EndDate + "</td>";
                    answer += "</tr>";
                }


                answer += "</table> </body> </html>";

                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowEmployee(IApplicationBuilder app) {
            app.Run(async context => {
                var employeesCache = context.RequestServices.GetService<EmployeeService>().Get("Employees20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Рабочие</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список работников</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Имя</th>" +
                            "<th>Название отдела</th>" +
                            "<th>Дата найма</th>" +
                            "<th>Текст достижения</th>" +
                            "<th>Оценка</th>" +
                        "</tr>";

                foreach (Employee employee in employeesCache) {
                    answer += "<tr>";
                    answer += "<td>" + employee.ID + "</td>";
                    answer += "<td>" + employee.Name + "</td>";
                    answer += "<td>" + employee.Division.Name + "</td>";
                    answer += "<td>" + employee.HireDate + "</td>";
                    answer += "<td>" + employee.Achievement.Text + "</td>";
                    answer += "<td>" + employee.Mark.Value + "</td>";
                    answer += "</tr>";
                }

                answer += "</table> </body> </html>";
                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowMark(IApplicationBuilder app) {
            app.Run(async context => {
                var marksCache = context.RequestServices.GetService<MarkService>().Get("Marks20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Оценки</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список оценок</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Значение оценки</th>" +
                        "</tr>";

                foreach (Mark mark in marksCache) {
                    answer += "<tr>";
                    answer += "<td>" + mark.ID + "</td>";
                    answer += "<td>" + mark.Value + "</td>";
                    answer += "</tr>";
                }

                answer += "</table> </body> </html>";
                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowPlannedEfficiency(IApplicationBuilder app) {
            app.Run(async context => {
                var plannedEfficiecyCacheService = context.RequestServices.GetService<PlannedEfficiencyService>();
                var plannedEfficiecyCache = plannedEfficiecyCacheService.Get("PlannedEfficiency20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Планируемая эффективность</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список запланированной эффективности</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Эффективность</th>" +
                            "<th>Начальная дата</th>" +
                            "<th>Конечная дата</th>" +
                        "</tr>";

            foreach (PlannedEfficiency plannedEfficiency in plannedEfficiecyCache) {
                    answer += "<tr>";
                    answer += "<td>" + plannedEfficiency.ID + "</td>";
                    answer += "<td>" + plannedEfficiency.Efficiecy + "</td>";
                    answer += "<td>" + plannedEfficiency.Date.StartDate + "</td>";
                    answer += "<td>" + plannedEfficiency.Date.EndDate + "</td>";
                    answer += "</tr>";
                }

                answer += "</table> </body> </html>";
                await context.Response.WriteAsync(answer);
            });
        }

        public static void ShowRealEfficiency(IApplicationBuilder app) {
            app.Run(async context => {
                var realEfficiecyCacheService = context.RequestServices.GetService<RealEfficiencyService>();
                var realEfficiecyCache = realEfficiecyCacheService.Get("RealEfficiency20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Реальная эффективность</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список реальной эффективности</h1>" +
                    "<table border = 1>" +
                        "<tr> " +
                            "<th>Код</th>" +
                            "<th>Эффективность</th>" +
                            "<th>Начальная дата</th>" +
                            "<th>Конечная дата</th>" +
                        "</tr>";

                foreach (RealEfficiency realEfficiency in realEfficiecyCache) {
                    answer += "<tr>";
                    answer += "<td>" + realEfficiency.ID + "</td>";
                    answer += "<td>" + realEfficiency.Efficiecy + "</td>";
                    answer += "<td>" + realEfficiency.Date.StartDate + "</td>";
                    answer += "<td>" + realEfficiency.Date.EndDate + "</td>";
                    answer += "</tr>";
                }

                answer += "</table> </body> </html>";
                await context.Response.WriteAsync(answer);
            });
        }
    }
}