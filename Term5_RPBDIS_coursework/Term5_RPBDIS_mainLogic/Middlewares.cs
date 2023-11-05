using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
                var achivementCache = context.RequestServices.GetService<AchievementService>().Get("Achievements20");

                string answer = "" +
                "<HTML>" +
                "<HEAD> <Title>Достижения</title> </head>" +
                "<META http-equiv='Content-Type' content='text/html; charset=utf-8 />'" +
                "<body> " +
                    "<h1>Список достижений</h1>" +
                    "<table border = 1>" +
                        "<th> " +
                            "<td>Код</td>" +
                            "<td>Текст</td>" +
                        "</th>";

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
        public static void ShowDate(IApplicationBuilder app) { }
        public static void ShowDivision(IApplicationBuilder app) { }
        public static void ShowEmployee(IApplicationBuilder app) { }
        public static void ShowMark(IApplicationBuilder app) { }
        public static void ShowPlannedEfficiency(IApplicationBuilder app) { }
        public static void ShowRealEfficiency(IApplicationBuilder app) { }
    }
}