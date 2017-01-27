using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NBDCostTrackingSystem.Classes.OWINStartUp))]

namespace NBDCostTrackingSystem.Classes
{
    class OWINStartUp
    {
        public void Configuration(IAppBuilder app)
        {
            

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = "NBDCookie",
                LoginPath = new PathString("/LogIn"),
                LogoutPath = new PathString("/LogIn"),
                ExpireTimeSpan = System.TimeSpan.FromDays(1)
            });
        }
    }
}

