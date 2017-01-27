using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBDCostTrackingSystem
{
    public partial class Employees : System.Web.UI.Page
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
                // Creat table header
                TableHeaderRow th = new TableHeaderRow();
                th.TableSection = TableRowSection.TableHeader;
                string[] headerArray = { "Employee's Name", "Description", "Price", "Action" };
                for (int i = 0; i < headerArray.Length; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Font.Bold = true;
                    cell.Text = headerArray[i];
                    th.Cells.Add(cell);
                }

                // Add table header to the table
                tblEmployee.Rows.Add(th);

                // Add employees to create employee list 
                var query = from worker in db.WORKERs
                            join workerType in db.WORKER_TYPE on worker.wrkTypeID equals workerType.ID
                            select worker;

                foreach (var w in query)
                {
                    // Creat table rows and cells
                    TableRow tr = new TableRow();
                    TableCell wrkName = new TableCell();
                    TableCell wrkTypeDesc = new TableCell();
                    TableCell wrkTypePrice = new TableCell();
                    TableCell action = new TableCell();

                    // Construct link buttons
                    LinkButton edit = new LinkButton();
                    LinkButton delete = new LinkButton();
                    
                    // Code for the edit button
                    edit.ID = "btnAllEmpEdit_" + w.ID;
                    edit.CssClass = "btn btn-default btn-xs btnColorOverride";
                    edit.Attributes.Add("data-toggle", "modal");
                    edit.Attributes.Add("title", "Edit");
                    edit.Attributes.Add("runat", "server");
                    edit.Text = "<span class='glyphicon glyphicon-pencil'></span>";
                    edit.Attributes.Add("data-target", "#mdlCreateEmployee");

                    // Code for the remove button
                    delete.ID = "btnAllEmpRemove_" + w.ID;
                    delete.CssClass = "btn btn-default btn-xs btnColorOverride";
                    delete.Attributes.Add("data-toggle", "tooltip");
                    delete.Attributes.Add("title", "Remove");
                    delete.Attributes.Add("runat", "server");
                    delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                    delete.OnClientClick = "return confirm('Are you sure?');";

                    // Assign data to cells
                    wrkName.Text = w.wrkLName + " " + w.wrkFName;
                    wrkTypeDesc.Text = w.WORKER_TYPE.wrkTypeDesc;
                    wrkTypePrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.WORKER_TYPE.wrkTypePrice), 2));
                   
                    // Add controls to cells
                    action.Controls.Add(edit);
                    action.Controls.Add(delete);

                    // Add cells to row
                    tr.Cells.Add(wrkName);
                    tr.Cells.Add(wrkTypeDesc);
                    tr.Cells.Add(wrkTypePrice);
                    tr.Cells.Add(action);

                    // Add row to table
                    tblEmployee.Rows.Add(tr);
                }
            }
        }
    }
}