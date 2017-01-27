using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace NBDCostTrackingSystem.HelperClasses
{
    public static class NotifyJS
    {
        /// <summary>
        /// Shows the notification.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="message">The message.</param>
        /// <param name="style">The style.</param>
        public static void DisplayNotification(this Page page, string message, string style)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "notificationScript",
                "<script type='text/javascript'>  $(document).ready(function () { $.notify('" +
                message + "', '" + style + "'); });</script>");
        }

    }
}

