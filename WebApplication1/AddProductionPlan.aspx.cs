using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBDCostTrackingSystem
{
    public partial class AddProductionPlan : System.Web.UI.Page
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
                PopulateProjectsDDL();
            }
        }

        protected void PopulateProjectsDDL()
        {
            // Get all projects
            var projectName = from project in db.PROJECTs
                              select project;

            // Loop through query and add to dropdownlist
            foreach (var n in projectName)
            {
                ListItem li = new ListItem();
                li.Text = n.projName;
                li.Value = n.ID.ToString();
                ddlProject.Items.Add(li);
            }
        }

        protected void setSalesAssociate()
        {
            // Get project ID
            int id = Convert.ToInt32(ddlProject.SelectedValue);

            // Get sales associate attached to this project
            var salesAssoc = (from project in db.PROJECTs
                              join prodTeam in db.PROD_TEAM on project.ID equals prodTeam.projectID
                              join team in db.TEAM_MEMBER on prodTeam.ID equals team.teamID
                              join worker in db.WORKERs on team.workerID equals worker.ID
                              join workerType in db.WORKER_TYPE on worker.wrkTypeID equals workerType.ID
                              where project.ID == id && workerType.ID == 7
                              select worker).SingleOrDefault();

            // Display name in textbox
            txtTeamSalesAssociate.Text = salesAssoc.wrkFName + " " + salesAssoc.wrkLName;
        }

        protected void setLeadDesigner()
        {
            // Get project ID
            int id = Convert.ToInt32(ddlProject.SelectedValue);

            // Get designer attached to this project
            var leadDesigner = (from project in db.PROJECTs
                                join prodTeam in db.PROD_TEAM on project.ID equals prodTeam.projectID
                                join team in db.TEAM_MEMBER on prodTeam.ID equals team.teamID
                                join worker in db.WORKERs on team.workerID equals worker.ID
                                where worker.ID == project.designerID && project.ID == id
                                select project).SingleOrDefault();

            // Display name in textbox
            txtTeamDesigner.Text = leadDesigner.WORKER.wrkFName + " " + leadDesigner.WORKER.wrkLName;
        }

        protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Make the Panels Visible
            pnlLabSum.Visible = true;
            pnlMatSum.Visible = true;

            //Set the Project Site
            int id = Convert.ToInt32(this.ddlProject.SelectedValue);
            var projects = from project in db.PROJECTs
                           where project.ID == id
                           select project;

            foreach (var p in projects)
            {
                txtProjectBeginDate.Text = p.projEstStart;
                txtProjectEndDate.Text = p.projEstEnd;
                txtProjectSite.Text = p.projSite;
            }

            //Set the Sales Associate
            setSalesAssociate();

            //Set the Lead Designer
            setLeadDesigner();

            // START PULL MATERIAL TABLE
            tblMaterialsSummary.Rows.Clear();

            // Set up table headers
            TableHeaderRow mth = new TableHeaderRow();
            mth.TableSection = TableRowSection.TableHeader;
            string[] materialHeaderArray = { "#", "Type", "Quantity", "Description", "Size", "Delivery Date", "Work Date", "Unit Price", "Extended Price" };

            // Add table headings to table header
            for (int i = 0; i < materialHeaderArray.Length; i++)
            {
                TableCell cell = new TableCell();
                cell.Font.Bold = true;
                cell.Text = materialHeaderArray[i];
                mth.Cells.Add(cell);
            }

            // Add table header to the table
            tblMaterialsSummary.Rows.Add(mth);

            // Material Query
            var materialQuery = from materialReq in db.MATERIAL_REQ
                                join inventory in db.INVENTORies on materialReq.inventoryID equals inventory.ID
                                join material in db.MATERIALs on inventory.materialID equals material.ID
                                where materialReq.projectID == id
                                select materialReq;

            // Declare count variable for tracking number of materials in table
            int materialCount = 1;

            foreach (var w in materialQuery)
            {
                // Build rows and cells
                TableRow tr = new TableRow();
                TableCell matCount = new TableCell();
                TableCell matType = new TableCell();
                TableCell matQty = new TableCell();
                TableCell matDesc = new TableCell();
                TableCell matSize = new TableCell();
                TableCell matUnPrice = new TableCell();
                TableCell matExPrice = new TableCell();
                TableCell delDate = new TableCell();
                TableCell wrkDate = new TableCell();
                
                // Append textboxes to certain cells
                TextBox txtdelDate = new TextBox();
                txtdelDate.CssClass = "form-control";
                txtdelDate.Attributes.Add("runat", "server");
                txtdelDate.TextMode = TextBoxMode.Date;
                txtdelDate.ID = "txtDate" + Convert.ToString(materialCount);

                TextBox txtwrkDate = new TextBox();
                txtwrkDate.CssClass = "form-control";
                txtwrkDate.Attributes.Add("runat", "server");
                txtwrkDate.TextMode = TextBoxMode.Date;
                txtwrkDate.ID = "wrkDate" + Convert.ToString(materialCount);

                // Assign data to table cell text field
                matCount.Text = materialCount.ToString();
                matType.Text = w.INVENTORY.MATERIAL.matType;
                matQty.Text = w.mreqEstQty.ToString();
                matDesc.Text = w.INVENTORY.MATERIAL.matDesc;
                delDate.Controls.Add(txtdelDate);
                wrkDate.Controls.Add(txtwrkDate);

                // Not all the units have a size amount category. If the first is null just set it to 1
                matSize.Text = (w.INVENTORY.invSizeAmnt == null) ? "1 " + w.INVENTORY.invSizeUnit : w.INVENTORY.invSizeAmnt.ToString() + " " + w.INVENTORY.invSizeUnit;
                matUnPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.INVENTORY.invList), 2));
                matExPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.INVENTORY.invList * w.mreqEstQty), 2));

                // Add cells to rows
                tr.Cells.Add(matCount);
                tr.Cells.Add(matType);
                tr.Cells.Add(matQty);
                tr.Cells.Add(matDesc);
                tr.Cells.Add(matSize);
                tr.Cells.Add(delDate);
                tr.Cells.Add(wrkDate);
                tr.Cells.Add(matUnPrice);
                tr.Cells.Add(matExPrice);

                //Increse Row Count
                materialCount++;

                //Add Row to Table
                tblMaterialsSummary.Rows.Add(tr);
            }

            // START PULL LABOUR TABLE
            tblLabourSummary.Rows.Clear();

            // Set up table headers
            TableHeaderRow lth = new TableHeaderRow();
            lth.TableSection = TableRowSection.TableHeader;
            string[] labourHeaderArray = { "#", "Worker", "Type", "Task", "Hours", "Work Date", "Unit Price", "Extended Price" };

            // Add table headings to table header
            for (int i = 0; i < labourHeaderArray.Length; i++)
            {
                TableCell cell = new TableCell();
                cell.Font.Bold = true;
                cell.Text = labourHeaderArray[i];
                lth.Cells.Add(cell);
            }

            // Add table header to the table
            tblLabourSummary.Rows.Add(lth);

            // Labour query
            var labourQuery = from labourReq in db.LABOUR_REQUIREMENT
                              join workerType in db.WORKER_TYPE on labourReq.workerTypeID equals workerType.ID
                              join task in db.TASKs on labourReq.taskID equals task.ID
                              where labourReq.projectID == id
                              select new { labourReq, workerType, task };

            // Declare count variable for tracking number of labour req's in table
            int labourCount = 1;

            foreach (var w in labourQuery)
            {
                // Build rows and cells
                TableRow tr = new TableRow();
                TableCell labCount = new TableCell();
                TableCell labType = new TableCell();
                TableCell labTask = new TableCell();
                TableCell labHours = new TableCell();
                TableCell wrkDate = new TableCell();
                TableCell labUnPrice = new TableCell();
                TableCell labExPrice = new TableCell();
                TableCell labWorker = new TableCell();

                // Build drop downs
                DropDownList ddlWorker = new DropDownList();
                ddlWorker.CssClass = "form-control";

                // Get the worker type id from worker
                int workerTypeID = w.workerType.ID;

                // Query workers from prod team
                var workers = from team in db.PROD_TEAM
                              join member in db.TEAM_MEMBER on team.ID equals member.teamID
                              join worker in db.WORKERs on member.workerID equals worker.ID
                              where team.projectID == id && worker.wrkTypeID == workerTypeID
                              select new { worker.ID, worker.wrkFName, worker.wrkLName };

                // Add workers to drop down
                foreach (var wrk in workers)
                {
                    ListItem li = new ListItem();
                    li.Text = wrk.wrkFName + " " + wrk.wrkLName;
                    li.Value = Convert.ToString(wrk.ID);
                    ddlWorker.Items.Add(li);
                }

                // Build textbox
                TextBox txtWrkDate = new TextBox();
                txtWrkDate.CssClass = "form-control";
                txtWrkDate.Attributes.Add("runat", "server");
                txtWrkDate.TextMode = TextBoxMode.Date;
                txtWrkDate.ID = "txtWorker" + Convert.ToString(labourCount);

                // Assign data
                labCount.Text = labourCount.ToString();
                labWorker.Controls.Add(ddlWorker);
                labType.Text = w.workerType.wrkTypeDesc;
                labTask.Text = w.task.taskDesc;
                labHours.Text = w.labourReq.lreqEstHours.ToString();
                labUnPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.workerType.wrkTypePrice), 2));
                labExPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.workerType.wrkTypePrice * w.labourReq.lreqEstHours), 2));
                wrkDate.Controls.Add(txtWrkDate);
                
                // Add cells to row
                tr.Cells.Add(labCount);
                tr.Cells.Add(labWorker);
                tr.Cells.Add(labType);
                tr.Cells.Add(labTask);
                tr.Cells.Add(labHours);
                tr.Cells.Add(wrkDate);
                tr.Cells.Add(labUnPrice);
                tr.Cells.Add(labExPrice);

                // Add row to table
                tblLabourSummary.Rows.Add(tr);
                labourCount++;
            }
        }

        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear drop down
            ddlProject.Items.Clear();

            // Get client id
            int id = Convert.ToInt32(this.ddlClientName.SelectedValue);

            // Query projects with clientID
            var projects = from project in db.PROJECTs
                           where project.clientID == id
                           select project;

            // Declare initial list item and add to drop down
            ListItem def = new ListItem();
            def.Selected = true;
            def.Value = "-1";
            def.Text = "Please Select a Project...";
            ddlProject.Items.Add(def);

            // Add remaining data to drop down
            foreach (var p in projects)
            {
                ListItem li = new ListItem();
                li.Text = p.projName;
                li.Value = Convert.ToString(p.ID);
                ddlProject.Items.Add(li);
            }

            // Add client information to controls
            foreach (var p in projects)
            {
                txtClientAddress.Text = p.CLIENT.cliAddress;
                txtClientContact.Text = p.CLIENT.cliConFName + " " + p.CLIENT.cliConLName;
                txtClientPhone.Text = p.CLIENT.cliPhone;
            }
        }

        protected void btnSave_onClick(object sender, EventArgs e)
        {
            // Pretend the save button works
            Response.Redirect("Main.aspx");
        }
    }
}