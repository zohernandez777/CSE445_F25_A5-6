using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Assignment5_CSE445F25
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Enable extensionless Web Forms routing
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Application["TotalFootballSessions"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["TotalFootballSessions"] = (int)Application["TotalFootballSessions"] + 1;
            Application.UnLock();
        }
    }
}