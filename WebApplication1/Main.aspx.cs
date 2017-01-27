using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBDCostTrackingSystem.HelperClasses;
using System.Data;
using Microsoft.AspNet.Identity;

namespace NBDCostTrackingSystem
{
    public partial class Manager : System.Web.UI.Page
    {
        // Declare class level variables of entities db
        public EntitiesNBD db = new EntitiesNBD();
        public static List<string[]> dailyWorkReportRows = new List<string[]>();
        public static int dailyWorkReportIDCounter = 1;
        public static string deletedClient;
        public static bool deleteClientFlag = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Build the intiail table for daily work summary
                BuildInitialDailyWorkReportTable();
                DropDownHelper.PopulateEmployeeList(ddlEmployeeList);
                DropDownHelper.PopulateClientList(ddlClientList);
            }
            if (deleteClientFlag)
            {
                NotifyJS.DisplayNotification(this.Page, deletedClient + " was successfully deleted.", "success");
                deleteClientFlag = false;
            }
            //check is user is logged in
            if (User.Identity.IsAuthenticated)
            {
                // Find the control on the master page and assign the username to the label
                Label userNameLabel = (Label)Page.Master.FindControl("lblUserLoggedIn");
                userNameLabel.Text = User.Identity.Name;

                //Fill projects table from DB
                PopulateProjectsTable();

                // Set the date for the create daily work report modal to today by default
                txtCreateDailyWorkReportDateInPanel.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void btnProjectView_Click(Object sender, EventArgs e)
        {
            // Get button that was clicked
            LinkButton edit = (LinkButton)sender;

            // Get id from button
            int id = Convert.ToInt32(edit.Attributes["data-id"]);

            // Store id in session variable
            Session["ProjectID"] = id;

            // Navigate to view page
            Response.Redirect("~/ViewDesignBid.aspx", true);
        }

        protected void btnProjectEdit_Click(Object sender, EventArgs e)
        {
            // Get button that was clicked
            LinkButton edit = (LinkButton)sender;

            // Get id from button
            int id = Convert.ToInt32(edit.Attributes["data-id"]);

            // Store id in session variable
            Session["ProjectID"] = id;

            // Navigate to view page
            Response.Redirect("~/EditDesignBid.aspx", true);
        }

        protected void btnProjectRemove_Click(Object sender, EventArgs e)
        {
            RemoveClientORProject("Project", (LinkButton)sender);
        }

        public void PopulateProjectsTable()
        {
            // Add employees to create team employee list modal
            var query = from project in db.PROJECTs
                        select project;

            // Set up projects table header
            TableHeaderRow th = new TableHeaderRow();
            th.TableSection = TableRowSection.TableHeader;
            th.Font.Bold = true;
            string[] tableHeadings = { "Project Name", "Site", "Bid #", "End Date", "Budget (Actual/Estimated)", "% Marker", "Client", "Manager", "Action" };

            // Add data to cells
            for (int i = 0; i < tableHeadings.Length; i++)
            {
                TableCell td = new TableCell();
                if (i == 5 || i == 6)
                {
                    td.CssClass = "text-center";
                }
                td.Text = tableHeadings[i];
                th.Cells.Add(td);
            }

            // Add the header row to the table
            tblProjects.Rows.Add(th);

            foreach (var p in query)
            {
                // Consruct tablerow object and table cell objects
                TableRow tr = new TableRow();
                TableCell projName = new TableCell();
                TableCell projSite = new TableCell();
                TableCell bidNumber = new TableCell();
                TableCell endDate = new TableCell();
                TableCell budget = new TableCell();
                TableCell percentageMarker = new TableCell();
                TableCell clientApp = new TableCell();
                TableCell managerApp = new TableCell();
                TableCell action = new TableCell();
                clientApp.CssClass = "text-center";
                managerApp.CssClass = "text-center";

                // Construct link buttons
                LinkButton view = new LinkButton();
                LinkButton edit = new LinkButton();
                LinkButton delete = new LinkButton();

                // Code for the view button
                view.CssClass = "btn btn-default btn-xs btnColorOverride";
                view.Attributes.Add("data-toggle", "tooltip");
                view.Attributes.Add("data-id", p.ID.ToString());
                view.Attributes.Add("title", "View");
                view.Attributes.Add("runat", "server");
                view.Text = "<span class='glyphicon glyphicon-eye-open'></span>";
                view.Click += new EventHandler(btnProjectView_Click);

                // Code for the edit button
                edit.CssClass = "btn btn-default btn-xs btnColorOverride";
                edit.Attributes.Add("data-id", p.ID.ToString());
                edit.Attributes.Add("data-toggle", "tooltip");
                edit.Attributes.Add("title", "Edit");
                edit.Attributes.Add("runat", "server");
                edit.Text = "<span class='glyphicon glyphicon-pencil'></span>";
                edit.Click += new EventHandler(btnProjectEdit_Click);

                // Code for the remove button
                delete.CssClass = "btn btn-default btn-xs btnColorOverride";
                delete.Attributes.Add("data-toggle", "tooltip");
                delete.Attributes.Add("data-id", p.ID.ToString());
                delete.Attributes.Add("title", "Remove");
                delete.Attributes.Add("runat", "server");
                delete.Attributes.Add("data-id", p.ID.ToString());
                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                delete.OnClientClick = "return confirm('Are you sure?');";
                delete.Click += new EventHandler(btnProjectRemove_Click);

                // Assign values to cells
                projName.Text = p.projName;
                projSite.Text = p.projSite;
                bidNumber.Text = p.ID.ToString();
                endDate.Text = p.projEstEnd;
                budget.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(p.projActCost), 2)) + " / " + string.Format("{0:C}", Math.Round(Convert.ToDecimal(p.projEstCost), 2));
                decimal percentage = Math.Round((Convert.ToDecimal(p.projActCost) / Convert.ToDecimal(p.projEstCost) * 100));
                percentageMarker.CssClass = "text-center";

                // Test to set css class on values within the danger zone
                if (percentage >= 70)
                {
                    percentage = (percentage <= 0) ? 0 : percentage;
                    percentageMarker.Text = "<span class='text-danger text-center' style='color:red;'>" + percentage + "%</span>";
                }
                else
                {
                    percentageMarker.Text = (percentage <= 0) ? "0%" : percentage + "%";
                }
                if (p.projBidCustAccept == true)
                {
                    //If client has approved:
                    clientApp.Text = "<span class='text-center glyphicon glyphicon-thumbs-up' data-toggle='tooltip' data-container='body' style='color:green;' title='Approved'></span>";
                }
                else if (p.projBidCustAccept == false)
                {
                    //If the client has not approved:
                    clientApp.Text = "<span class='glyphicon glyphicon-thumbs-down'data-toggle='tooltip' data-container='body' style='color:red;' title='Not Approved'></span>";
                }

                if (p.projBidMgmtAccept == true)
                {
                    //If the manager has approved:
                    managerApp.Text = "<span class='glyphicon glyphicon-thumbs-up' data-toggle='tooltip' data-container='body' style='color:green;' title='Approved'></span>";
                }
                else if (p.projBidMgmtAccept == false)
                {
                    //If the manager has not approved:
                    managerApp.Text = "<span class='glyphicon glyphicon-thumbs-down' data-toggle='tooltip' data-container='body' style='color:red;' title='Not Approved'></span>";
                }

                // Add buttons to cell
                action.Controls.Add(view);
                action.Controls.Add(edit);
                action.Controls.Add(delete);

                //Estimate 70% business rule
                if (Convert.ToDouble(p.projActCost) >= (Convert.ToDouble(p.projEstCost) * 0.6))
                {
                    budget.CssClass = "danger";
                }
                else if (Convert.ToDouble(p.projActCost) >= (Convert.ToDouble(p.projEstCost) * 0.4))
                {
                    budget.CssClass = "warning";
                }
                else
                {
                    budget.CssClass = "success";
                }

                // Add cells to table row
                tr.Cells.Add(projName);
                tr.Cells.Add(projSite);
                tr.Cells.Add(bidNumber);
                tr.Cells.Add(endDate);
                tr.Cells.Add(budget);
                tr.Cells.Add(percentageMarker);
                tr.Cells.Add(clientApp);
                tr.Cells.Add(managerApp);
                tr.Cells.Add(action);

                // Add row to table
                tblProjects.Rows.Add(tr);
            }
        }
        private void BuildInitialDailyWorkReportTable()
        {
            // Build header rows for datatable
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("#", typeof(string)));
            dt.Columns.Add(new DataColumn("Project", typeof(string)));
            dt.Columns.Add(new DataColumn("Hours", typeof(string)));
            dt.Columns.Add(new DataColumn("Task", typeof(string)));

            //Store the DataTable in ViewState
            ViewState["DailyWorkReportTable"] = dt;
            gvDailyWorkReportSummary.DataSource = dt;
            gvDailyWorkReportSummary.DataBind();
        }
        private void AddNewRowToDailyWorkReportTable()
        {
            // Initialize count variable
            int rowIndex = 0;
            if (ViewState["DailyWorkReportTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["DailyWorkReportTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count == 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["#"] = dailyWorkReportIDCounter;
                    drCurrentRow["Project"] = ddlDailyWorkReportProjectInPanel.SelectedItem.Text;
                    drCurrentRow["Hours"] = txtHoursWorked.Text;
                    drCurrentRow["Task"] = ddlDailyWorkReportTaskstInPanel.SelectedItem.Text;
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["DailyWorkReportTable"] = dtCurrentTable;
                    gvDailyWorkReportSummary.DataSource = dtCurrentTable;
                    gvDailyWorkReportSummary.DataBind();
                }
                else if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        string project = ddlDailyWorkReportProjectInPanel.SelectedItem.Text;
                        string hours = txtHoursWorked.Text;
                        string task = ddlDailyWorkReportTaskstInPanel.SelectedItem.Text;

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["#"] = dailyWorkReportIDCounter;
                        drCurrentRow["Project"] = project;
                        drCurrentRow["Hours"] = hours;
                        drCurrentRow["Task"] = task;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["DailyWorkReportTable"] = dtCurrentTable;
                    gvDailyWorkReportSummary.DataSource = dtCurrentTable;
                    gvDailyWorkReportSummary.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousDataOnDailyWorkReportTable();
        }
        private void SetPreviousDataOnDailyWorkReportTable()
        {
            int rowIndex = 0;
            if (ViewState["DailyWorkReportTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["DailyWorkReportTable"];
                if (dt.Rows.Count > 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string project = gvDailyWorkReportSummary.Rows[rowIndex].Cells[1].Text;
                        string hours = gvDailyWorkReportSummary.Rows[rowIndex].Cells[2].Text;
                        string task = gvDailyWorkReportSummary.Rows[rowIndex].Cells[3].Text;

                        project = dt.Rows[i]["Project"].ToString();
                        hours = dt.Rows[i]["Hours"].ToString();
                        task = dt.Rows[i]["Task"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        protected void gvDailyWorkReportSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Show grid view hide label
            gvDailyWorkReportSummary.Visible = true;
            lblDailyWorkReportSummary.Visible = false;

            if (gvDailyWorkReportSummary.Rows.Count == 0)
            {
                pnlDailyWorkReportDeleteControls.Visible = false;
            }
            else
            {
                pnlDailyWorkReportDeleteControls.Visible = true;
            }
        }

        protected void btnDailyWorkReportDeleteRow_Click(object sender, EventArgs e)
        {
            // Get ID from drop down list
            string id = ddlDeleteDailyWorkReportFromTable.SelectedItem.Text;

            // Get datatable from view state
            DataTable dtCurrentTable = (DataTable)ViewState["DailyWorkReportTable"];
            if (ddlDeleteDailyWorkReportFromTable.SelectedItem.Text == "Delete All Rows")
            {
                dtCurrentTable.Clear();
                dailyWorkReportRows.Clear();
                ddlDeleteDailyWorkReportFromTable.Items.Clear();
                ddlDeleteDailyWorkReportFromTable.Items.Add("Delete All Rows");
                dailyWorkReportIDCounter = 1;
                lblDailyWorkReportSummary.Visible = true;
                pnlDailyWorkReportDeleteControls.Visible = false;
                btnSaveDailyWorkReport.Visible = false;
            }
            else
            {
                // Delete reference in drop down list
                ddlDeleteDailyWorkReportFromTable.Items.Remove(id);

                // Remove row from datatable
                for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtCurrentTable.Rows[i];
                    if (dr["#"].ToString() == id)
                    {
                        dr.Delete();
                        dailyWorkReportRows.RemoveAt(i);
                    }
                }
                if (dtCurrentTable.Rows.Count == 0)
                {
                    pnlDailyWorkReportDeleteControls.Visible = false;
                    lblDailyWorkReportSummary.Visible = true;
                    btnSaveDailyWorkReport.Visible = false;
                }
            }

            // Rebind the datasource
            gvDailyWorkReportSummary.DataSource = dtCurrentTable;
            gvDailyWorkReportSummary.DataBind();
        }

        protected void btnDailyWorkReportAddNewTask_Click(object sender, EventArgs e)
        {
            try
            {
                // Show save button
                btnSaveDailyWorkReport.Visible = true;
                AddNewRowToDailyWorkReportTable();

                // Assign values to string array for prep in the database
                string[] row = {
                    ddlDailyWorkReportProjectInPanel.SelectedValue,
                    txtHoursWorked.Text,
                    ddlDailyWorkReportTaskstInPanel.SelectedValue,
                };

                // Add to rows for use with db
                dailyWorkReportRows.Add(row);

                // Set focus on hidden label to avoid page jump
                lblFocusHAX.Focus();

                // Add row numbers to drop down
                ddlDeleteDailyWorkReportFromTable.Items.Add(dailyWorkReportIDCounter.ToString());

                // Increment row counter
                dailyWorkReportIDCounter++;
            }
            catch (DataException dx)
            {
                NotifyJS.DisplayNotification(this.Page, dx.InnerException.InnerException.Message, "danger");
            }
            catch (Exception ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }

        protected void btnSaveDailyWorkReport_Click(object sender, EventArgs e)
        {
            // CODE INCOMPLETE IN THIS PART BECAUSE IT WAS NOT A REQUIREMENT BUT WE DID START IT
            // Part 1
            // I can either get the userID that doesn't have an assigned workertype in the system
            // Or I can hardcode in a workertypeID since we are only building this system to work with a Managers level of access
            // And just assign the manager type to the workertype in the labour summary table
            // In reality this should be attached to the user upon registration so it can be pulled from the role in the identity database
            // int userID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.GetUserId());

            // MATERIAL REQUIREMENTS
            for (int i = 0; i < dailyWorkReportRows.Count; i++)
            {
                // Get project ID and hours worked on project
                int projID = Convert.ToInt32(dailyWorkReportRows[i][0]);
                short hours = Convert.ToInt16(dailyWorkReportRows[i][2]);

                // Create material requirements object to hold material data
                LABOUR_SUMMARY ls = new LABOUR_SUMMARY();
                ls.projectID = projID;
                ls.lsHours = hours;

                // Part 2, See part 1 for explanation
                ls.workerTypeID = 8;

                // Add to material requirements table
                db.LABOUR_SUMMARY.Add(ls);

                PROJECT p = db.PROJECTs.Find(projID);
            }
        }

        protected void ddlClientList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add ID's to buttons
            btnClientRemove.Attributes.Add("data-id", ddlClientList.SelectedValue);
            btnClientEdit.Attributes.Add("data-id", ddlClientList.SelectedValue);
            btnClientView.Attributes.Add("data-id", ddlClientList.SelectedValue);
        }

        protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add ID's to buttons
            btnEmployeeRemove.Attributes.Add("data-id", ddlEmployeeList.SelectedValue);
            btnEmployeeEdit.Attributes.Add("data-id", ddlEmployeeList.SelectedValue);
            btnEmployeeView.Attributes.Add("data-id", ddlEmployeeList.SelectedValue);
        }

        protected void ddlDailyWorkReportProjectInPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlDailyWorkReportProjectInPanel.SelectedValue);
            var labourReq = from labour in db.LABOUR_REQUIREMENT
                            join task in db.TASKs on labour.taskID equals task.ID
                            where labour.projectID == id
                            select new { labour, task };

            // Add to materials drop down list
            foreach (var l in labourReq)
            {
                ListItem li = new ListItem();
                li.Text = l.labour.ID + " - " + l.task.taskDesc;
                li.Value = l.labour.ID.ToString();
                ddlDailyWorkReportTaskstInPanel.Items.Add(li);
            }
        }

        protected void btnEmployeeView_Click(object sender, EventArgs e)
        {
            // CODE INCOMPLETE NOT REQUIRED
        }

        protected void btnEmployeeEdit_Click(object sender, EventArgs e)
        {
            // CODE INCOMPLETE NOT REQUIRED
        }

        protected void btnEmployeeRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(btnEmployeeRemove.Attributes["data-id"]);
            try
            {
                //delete an existing record
                WORKER w = db.WORKERs.Find(id);
                db.WORKERs.Remove(w);
                db.SaveChanges();
                // Reset the employeelist
                DropDownHelper.PopulateEmployeeList(ddlEmployeeList);
                NotifyJS.DisplayNotification(this.Page, w.wrkFName + " " + w.wrkLName + " was successfully deleted.", "success");
            }
            catch (DataException dx)
            {
                NotifyJS.DisplayNotification(this.Page, dx.InnerException.InnerException.Message, "danger");
            }
        }

        // Click events for dynamic buttons
        protected void btnClientView_Click(object sender, EventArgs e)
        {
            // Show the modal for client view
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#mdlViewClient').modal('show');", true);

            // Get client ID from button
            int id = Convert.ToInt32(btnClientView.Attributes["data-id"]);

            // Search for client in db
            var client = (from c in db.CLIENTs
                          where c.ID == id
                          select c).SingleOrDefault();

            // Add data to controls
            txtClientName.Text = client.cliName;
            txtClientAddress.Text = client.cliAddress;
            ddlClientCity.Items.Insert(0, new ListItem { Text = client.CITY.city1 });
            ddlClientProv.Items.Insert(0, new ListItem { Text = client.cliProvince });
            txtClientPCode.Text = client.cliPCode;
            txtClientPhone.Text = client.cliPhone;
            txtClientConFName.Text = client.cliConFName;
            txtClientConLName.Text = client.cliConLName;
            txtClientConPosition.Text = client.cliConPosition;
        }

        protected void btnClientEdit_Click(object sender, EventArgs e)
        {
            // Show the modal for client edit
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#mdlCreateClient').modal('show');", true);

            // Get client ID from button
            int id = Convert.ToInt32(btnClientRemove.Attributes["data-id"]);

            // Get controls from master page
            TextBox txtClientName = (TextBox)Page.Master.FindControl("txtClientName");
            TextBox txtClientAddress = (TextBox)Page.Master.FindControl("txtClientAddress");
            DropDownList ddlClientCity = (DropDownList)Page.Master.FindControl("ddlClientCity");
            DropDownList ddlClientProv = (DropDownList)Page.Master.FindControl("ddlClientProv");
            TextBox txtClientPCode = (TextBox)Page.Master.FindControl("txtClientPCode");
            TextBox txtClientPhone = (TextBox)Page.Master.FindControl("txtClientPhone");
            TextBox txtClientConFName = (TextBox)Page.Master.FindControl("txtClientConFName");
            TextBox txtClientConLName = (TextBox)Page.Master.FindControl("txtClientConLName");
            TextBox txtClientConPosition = (TextBox)Page.Master.FindControl("txtClientConPosition");

            //Populate text boxes with existing data
            CLIENT c = db.CLIENTs.Find(id);
            txtClientName.Text = c.cliName;
            txtClientAddress.Text = c.cliAddress;
            ddlClientCity.SelectedItem.Selected = false;
            ddlClientProv.SelectedItem.Selected = false;
            ddlClientCity.Items.FindByValue(c.cityID.ToString()).Selected = true;
            ddlClientProv.Items.FindByText(c.cliProvince.ToString()).Selected = true;
            txtClientPCode.Text = c.cliPCode;
            txtClientPhone.Text = c.cliPhone;
            txtClientConFName.Text = c.cliConFName;
            txtClientConLName.Text = c.cliConLName;
            txtClientConPosition.Text = c.cliConPosition;

            // Switch back to create mode
            ReverseClientMode(false);
        }
        public void ReverseClientMode(bool reverse)
        {
            // Reverse visibility on headers and buttons
            Panel pnlCreateNewClient = (Panel)Page.Master.FindControl("pnlCreateNewClient");
            Panel pnlEditClient = (Panel)Page.Master.FindControl("pnlEditClient");
            Button btnClientCreate = (Button)Page.Master.FindControl("btnClientCreate");
            Button btnSaveClientChanges = (Button)Page.Master.FindControl("btnSaveClientChanges");

            pnlCreateNewClient.Visible = reverse;
            pnlEditClient.Visible = !reverse;
            btnClientCreate.Visible = reverse;
            btnSaveClientChanges.Visible = !reverse;
        }

        protected void btnClientRemove_Click(object sender, EventArgs e)
        {
            RemoveClientORProject("Client", (LinkButton)sender);
        }

        public void RemoveClientORProject(string type, LinkButton button)
        {
            // get id from button parameter
            int id = Convert.ToInt32(button.Attributes["data-id"]);

            // Determine which project to delete by clientID or projID
            if (type == "Client")
            {
                var projects = from p in db.PROJECTs
                               where p.clientID == id
                               select p;

                DeleteClientOrProject(projects, type, id);
            }
            else
            {
                var projects = from p in db.PROJECTs
                               where p.ID == id
                               select p;

                DeleteClientOrProject(projects, type, id);
            }
        }

        public void DeleteClientOrProject(IQueryable<PROJECT> project, string type, int id)
        {
            // Delete all projects and child records associated with passed paramater project
            foreach (var p in project)
            {
                var labourSummary = db.LABOUR_SUMMARY
                    .Where(labID => labID.projectID == p.ID);

                foreach (var l in labourSummary)
                {
                    int lsID = l.ID;
                    LABOUR_SUMMARY ls = db.LABOUR_SUMMARY.Find(lsID);
                    db.LABOUR_SUMMARY.Remove(ls);
                }

                var materialRequirement = db.MATERIAL_REQ
                    .Where(matID => matID.projectID == p.ID);

                foreach (var m in materialRequirement)
                {
                    int mID = m.ID;
                    MATERIAL_REQ mr = db.MATERIAL_REQ.Find(mID);
                    db.MATERIAL_REQ.Remove(mr);
                }

                var labourRequirement = db.LABOUR_REQUIREMENT
                    .Where(lrID => lrID.projectID == p.ID);

                foreach (var l in labourRequirement)
                {
                    int lID = l.ID;
                    LABOUR_REQUIREMENT lr = db.LABOUR_REQUIREMENT.Find(lID);
                    db.LABOUR_REQUIREMENT.Remove(lr);
                }

                var prodTeam = db.PROD_TEAM
                    .Where(ptID => ptID.projectID == p.ID);

                foreach (var pts in prodTeam)
                {
                    int pteamID = pts.ID;
                    PROD_TEAM pt = db.PROD_TEAM.Find(pteamID);
                    db.PROD_TEAM.Remove(pt);

                    var teamMember = db.TEAM_MEMBER
                        .Where(tmID => tmID.teamID == pts.ID);

                    foreach (var ptm in teamMember)
                    {
                        int teamMemberID = ptm.Id;
                        TEAM_MEMBER tm = db.TEAM_MEMBER.Find(teamMemberID);
                        db.TEAM_MEMBER.Remove(tm);
                    }
                }
                db.PROJECTs.Remove(p);
            }
            if (type == "Client")
            {
                try
                {
                    //delete an existing record
                    CLIENT c = db.CLIENTs.Find(id);
                    db.CLIENTs.Remove(c);
                    db.SaveChanges();
                    DropDownHelper.PopulateClientList(ddlClientList);
                    deletedClient = c.cliName;
                    deleteClientFlag = true;
                    Response.Redirect("~/Main.aspx", false);

                }
                catch (DataException dx)
                {
                    NotifyJS.DisplayNotification(this.Page, dx.InnerException.InnerException.Message, "danger");
                }
            }
            else
            {
                try
                {
                    db.SaveChanges();
                    Response.Redirect("~/Main.aspx", false);
                }
                catch (DataException dx)
                {
                    NotifyJS.DisplayNotification(this.Page, dx.InnerException.InnerException.Message, "danger");
                }
            }
        }
    }
}