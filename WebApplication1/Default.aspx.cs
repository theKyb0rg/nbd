using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBDCostTrackingSystem.HelperClasses;
using Microsoft.Owin.Security;
using System.Data.Entity.Infrastructure;
using System.Data;

namespace NBDCostTrackingSystem
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtRegisterPassword.Text != txtRegisterConfirmPassword.Text)
            {
                NotifyJS.DisplayNotification(this.Page, "Passwords do not match", "error");
            }
            else
            {
                // Default UserStore constructor uses the default connection string named: DefaultConnection
                var userStore = new UserStore<IdentityUser>();
                var manager = new UserManager<IdentityUser>(userStore);
                var user = new IdentityUser() { UserName = txtRegisterUsername.Text };

                try
                {
                    IdentityResult result = manager.Create(user, txtRegisterPassword.Text);
                    if (result.Succeeded)
                    {
                        // This line of code calls a javascript helper function to display notifications
                        // See HelperClasses.NotifyJS for documentation on how to use it
                        NotifyJS.DisplayNotification(this.Page, user.UserName + " was created successfully!", "success");
                    }
                    else
                    {
                        // This line of code calls a javascript helper function to display notifications
                        // See Project.js DisplayNotification() for details
                        NotifyJS.DisplayNotification(this.Page, result.Errors.FirstOrDefault(), "error");
                    }
                }
                catch (DataException dx)
                {
                    lblDBERROR.Text = dx.InnerException.InnerException.Message;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Default UserStore constructor uses the default connection string named: DefaultConnection
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            IdentityUser user = manager.Find(txtUsername.Text, txtPassword.Text);

            if (user == null)
            {
                // This line of code calls a javascript helper function to display notifications
                // See Project.js DisplayNotification() for details
                NotifyJS.DisplayNotification(this.Page, "Username or password incorrect.", "error");
            }
            else
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(userIdentity);
                Response.Redirect("~/Main.aspx");
            }
        }
    }
}