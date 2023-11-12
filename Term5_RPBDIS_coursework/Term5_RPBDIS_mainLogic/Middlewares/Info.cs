using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term5_RPBDIS_mainLogic {
    public static partial class Middlewares {
        public class Info {
            public static void ShowClientInfo(IApplicationBuilder app) {
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
}
