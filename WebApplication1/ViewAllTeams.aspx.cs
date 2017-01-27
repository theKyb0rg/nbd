using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBDCostTrackingSystem
{
    public partial class ViewAllTeams : System.Web.UI.Page
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
                string[] headerArray = { "Team Name", "Project", "# of Members", "Action" };
                for (int i = 0; i < headerArray.Length; i++)
                {
                    TableCell cell = new TableCell();
                    cell.Font.Bold = true;
                    cell.Text = headerArray[i];
                    th.Cells.Add(cell);
                }

                // Add table header to the table
                tblTeams.Rows.Add(th);

                // Add clients to create client list 
                var teams = from team in db.PROD_TEAM
                            select team;

                foreach (var w in teams)
                {
                    TableRow tr = new TableRow();
                    TableCell teamName = new TableCell();
                    TableCell projectName = new TableCell();
                    TableCell memberCount = new TableCell();
                    TableCell action = new TableCell();

                    // Construct link buttons
                    LinkButton edit = new LinkButton();
                    LinkButton delete = new LinkButton();

                    // Code for the edit button
                    edit.CssClass = "btn btn-default btn-xs btnColorOverride";
                    edit.Attributes.Add("data-toggle", "modal");
                    edit.Attributes.Add("title", "Edit");
                    edit.Attributes.Add("runat", "server");
                    edit.Text = "<span class='glyphicon glyphicon-pencil'></span>";
                    edit.Attributes.Add("data-target", "#mdlCreateTeam");

                    // Code for the remove button
                    delete.CssClass = "btn btn-default btn-xs btnColorOverride";
                    delete.Attributes.Add("data-toggle", "tooltip");
                    delete.Attributes.Add("title", "Remove");
                    delete.Attributes.Add("runat", "server");
                    delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                    delete.OnClientClick = "return confirm('Are you sure?');";

                    // Add text to cells
                    teamName.Text = w.teamName;
                    projectName.Text = w.PROJECT.projName;
                    memberCount.Text = w.TEAM_MEMBER.Count.ToString();         
                    action.Controls.Add(edit);
                    action.Controls.Add(delete);

                    // Ad cells to rows
                    tr.Cells.Add(teamName);
                    tr.Cells.Add(projectName);
                    tr.Cells.Add(memberCount);
                    tr.Cells.Add(action);

                    // Add row to table
                    tblTeams.Rows.Add(tr);
                }
            }
        }
    }
}