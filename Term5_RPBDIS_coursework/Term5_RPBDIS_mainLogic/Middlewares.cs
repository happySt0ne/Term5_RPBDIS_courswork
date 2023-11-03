using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Term5_RPBDIS_mainLogic {
    public class Middlewares {
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
    }
}