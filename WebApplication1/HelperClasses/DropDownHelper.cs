using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NBDCostTrackingSystem.HelperClasses
{
    public class DropDownHelper
    {

        public static void PopulateEmployeeList(DropDownList ddl)
        {
            EntitiesNBD db = new EntitiesNBD();
            // Reset employee list
            ddl.Items.Clear();
            ListItem item = new ListItem();
            item.Text = "Select an Employee...";
            item.Value = "-1";
            ddl.Items.Add(item);

            // Reset the drop down list
            var query = from worker in db.WORKERs
                        select worker;

            foreach (var w in query)
            {
                ListItem li = new ListItem();
                li.Text = w.wrkFName + " " + w.wrkLName;
                li.Value = w.ID.ToString();
                ddl.Items.Add(li);
            }
        }

        public static void PopulateClientList(DropDownList ddl)
        {
            EntitiesNBD db = new EntitiesNBD();
            // Reset employee list
            ddl.Items.Clear();
            ListItem item = new ListItem();
            item.Text = "Select a Client...";
            item.Value = "-1";
            ddl.Items.Add(item);

            // Reset the drop down list
            var query = from client in db.CLIENTs
                        select client;

            foreach (var c in query)
            {
                ListItem li = new ListItem();
                li.Text = c.cliName;
                li.Value = c.ID.ToString();
                ddl.Items.Add(li);
            }
        }
    }
}