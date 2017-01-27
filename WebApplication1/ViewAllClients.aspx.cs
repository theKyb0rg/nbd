using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBDCostTrackingSystem
{
    public partial class AllClients : System.Web.UI.Page
    {
        public static EntitiesNBD db = new EntitiesNBD();
        protected void Page_Load(object sender, EventArgs e)
        {
            //check is user is logged in
            if (User.Identity.IsAuthenticated)
            {
                // Find the control on the master page and assign the username to the label
                Label userNameLabel = (Label)Page.Master.FindControl("lblUserLoggedIn");
                userNameLabel.Text = User.Identity.Name;
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!IsPostBack)
            {
                TableHeaderRow th = new TableHeaderRow();
                th.TableSection = TableRowSection.TableHeader;
                string[] headerArray = { "Client's Name", "Address", "Phone", "Contact Name", "Contact Position", "Action" };
                for (int i = 0; i < headerArray.Length; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Font.Bold = true;
                    cell.Text = headerArray[i];
                    th.Cells.Add(cell);
                }

                // Add table header to the table
                tblClient.Rows.Add(th);

                // Add clients to create client list 
                var query = from client in db.CLIENTs
                            join city in db.CITies on client.cityID equals city.ID
                            select client;

                foreach (var w in query)
                {
                    // Create rows and cells
                    TableRow tr = new TableRow();
                    TableCell cliName = new TableCell();
                    TableCell Address = new TableCell();
                    TableCell cliPhone = new TableCell();
                    TableCell cliConName = new TableCell();
                    TableCell cliConPosition = new TableCell();
                    TableCell action = new TableCell();

                    // Construct link buttons
                    LinkButton edit = new LinkButton();
                    LinkButton delete = new LinkButton();

                    // Code for the edit button
                    edit.ID = "btnAllClientEdit_" + w.ID;
                    edit.CssClass = "btn btn-default btn-xs btnColorOverride";
                    edit.Attributes.Add("data-toggle", "modal");
                    edit.Attributes.Add("title", "Edit");
                    edit.Attributes.Add("runat", "server");
                    edit.Text = "<span class='glyphicon glyphicon-pencil'></span>";
                    edit.Attributes.Add("data-target", "#mdlCreateClient");

                    // Code for the remove button
                    delete.ID = "btnClientRemove_" + w.ID;
                    delete.CssClass = "btn btn-default btn-xs btnColorOverride";
                    delete.Attributes.Add("data-toggle", "tooltip");
                    delete.Attributes.Add("title", "Remove");
                    delete.Attributes.Add("runat", "server");
                    delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                    delete.OnClientClick = "return confirm('Are you sure?');";

                    // Assign controls the data 
                    cliName.Text = w.cliName;
                    Address.Text = w.cliAddress + " " + w.CITY.city1 + ", " + w.cliProvince + " " + w.cliPCode;
                    cliPhone.Text = w.cliPhone;
                    cliConName.Text = w.cliConFName + " " + w.cliConLName;
                    cliConPosition.Text = w.cliConPosition;
                    
                    // Add controls to cells
                    action.Controls.Add(edit);
                    action.Controls.Add(delete);

                    // Add cells to row
                    tr.Cells.Add(cliName);
                    tr.Cells.Add(Address);
                    tr.Cells.Add(cliPhone);
                    tr.Cells.Add(cliConName);
                    tr.Cells.Add(cliConPosition);
                    tr.Cells.Add(action);

                    // Add rows to table
                    tblClient.Rows.Add(tr);
                }
            }
        }
    }
}