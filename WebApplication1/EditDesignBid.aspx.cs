using NBDCostTrackingSystem.HelperClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBDCostTrackingSystem
{
    public partial class EditDesignBid1 : System.Web.UI.Page
    {
        // Declare class level variables
        public EntitiesNBD db = new EntitiesNBD();
        public decimal labourTotal = 0;
        public decimal materialsTotal = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load all data bro
                GetProjectData(Convert.ToInt32(Session["ProjectID"]));                
            }

            // Get all data
            GetLaborData(Convert.ToInt32(Session["ProjectID"]));
            GetMaterialsData(Convert.ToInt32(Session["ProjectID"]));
            GetSummaryData(Convert.ToInt32(Session["ProjectID"]));

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
        }
        protected void GetSummaryData(int id)
        {
            // Get all summary data for project
            decimal bidTotal = 0;
            int projectID = Convert.ToInt32(Session["ProjectID"]);
            var projects = db.PROJECTs
                .Where(t => t.ID == projectID)
                .ToList();

            foreach (var r in projects)
            {
                //Start Summary Section
                bidTotal = labourTotal + materialsTotal;
                headerLabourlTotal.InnerHtml = string.Format("{0:C}", Math.Round(Convert.ToDecimal(labourTotal), 2));
                headerMaterialTotal.InnerHtml = string.Format("{0:C}", Math.Round(Convert.ToDecimal(materialsTotal), 2));
                headerBidTotal.InnerHtml = string.Format("{0:C}", Math.Round(Convert.ToDecimal(bidTotal), 2));
            }
        }

        protected void GetProjectData(int id)
        {
            // Get project information for textboxes
            var p = db.PROJECTs
                       .Where(t => t.ID == id)
                       .ToList()
                       .SingleOrDefault();

            // Get all designers
            var allDesigners = db.WORKERs
                .Where(d => d.wrkTypeID == 2);

            // Populate Designer List
            foreach (var d in allDesigners)
            {
                ListItem li = new ListItem();
                li.Text = d.wrkFName + " " + d.wrkLName;
                li.Value = d.ID.ToString();
                ddlDesigner.Items.Add(li);
            }

            // Get sales associate
            var allSales = db.WORKERs
                .Where(d => d.wrkTypeID == 7);

            // Populate Sales associate List
            foreach (var s in allSales)
            {
                ListItem li = new ListItem();
                li.Text = s.wrkFName + " " + s.wrkLName;
                li.Value = s.ID.ToString();
                ddlSales.Items.Add(li);
            }

            // Fill Textboxes
            mdlTxtClientContact.Text = p.CLIENT.cliConFName + " " + p.CLIENT.cliConLName;
            mdlTxtClientAddress.Text = p.CLIENT.cliAddress;
            mdlTxtClientName.Text = p.CLIENT.cliName;
            mdlTxtClientPhone.Text = p.CLIENT.cliPhone;
            mdlTxtProjectName.Text = p.projName;
            mdlTxtProjectSite.Text = p.projSite;
            mdlTxtProjectBeginDate.Text = p.projEstStart;
            mdlTxtProjectEndDate.Text = p.projEstEnd;
        }

        protected void GetLaborData(int id)
        {
            //START PULL LABOUR TABLE
            // Labour Query
            tblLabourSummary.Rows.Clear();
            TableHeaderRow lth = new TableHeaderRow();
            lth.TableSection = TableRowSection.TableHeader;
            string[] labourHeaderArray = { "#", "Type", "Task", "Hours", "Unit Price", "Extended Price", "Action" };

            for (int i = 0; i < labourHeaderArray.Length; i++)
            {
                TableCell cell = new TableCell();
                cell.Font.Bold = true;
                cell.Text = labourHeaderArray[i];
                lth.Cells.Add(cell);
            }

            // Add table header to the table
            tblLabourSummary.Rows.Add(lth);
            var labourQuery = (from labourReq in db.LABOUR_REQUIREMENT
                               join workerType in db.WORKER_TYPE on labourReq.workerTypeID equals workerType.ID
                               join task in db.TASKs on labourReq.taskID equals task.ID
                               where labourReq.projectID == id
                               select new { labourReq, workerType, task }).ToList();
            int labourCount = 1;

            foreach (var w in labourQuery)
            {
                // Create rows, buttons, and cells
                TableRow tr = new TableRow();
                TableCell labCount = new TableCell();
                TableCell labType = new TableCell();
                TableCell labTask = new TableCell();
                TableCell labHours = new TableCell();
                TableCell labUnPrice = new TableCell();
                TableCell labExPrice = new TableCell();
                TableCell action = new TableCell();
                LinkButton delete = new LinkButton();

                // Code for the remove button
                delete.CssClass = "btn btn-default btn-xs btnColorOverride";
                delete.Attributes.Add("data-toggle", "tooltip");
                delete.Attributes.Add("data-id", w.labourReq.ID.ToString());
                delete.Attributes.Add("title", "Remove");
                delete.Attributes.Add("runat", "server");
                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                delete.OnClientClick = "return confirm('Are you sure?');";
                delete.Click += new EventHandler(DeleteLaborRequirement);
                action.Controls.Add(delete);

                // Apply data to controls
                labCount.Text = labourCount.ToString();
                labType.Text = w.workerType.wrkTypeDesc;
                labTask.Text = w.task.taskDesc;
                labHours.Text = w.labourReq.lreqEstHours.ToString();
                labUnPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.workerType.wrkTypePrice), 2));
                labExPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.workerType.wrkTypePrice * w.labourReq.lreqEstHours), 2));

                // Add cells to row
                tr.Cells.Add(labCount);
                tr.Cells.Add(labType);
                tr.Cells.Add(labTask);
                tr.Cells.Add(labHours);
                tr.Cells.Add(labUnPrice);
                tr.Cells.Add(labExPrice);
                tr.Cells.Add(action);

                //Update Labour Total, add row to table, increment labourcounter
                labourTotal += Convert.ToDecimal(w.workerType.wrkTypePrice * w.labourReq.lreqEstHours);
                tblLabourSummary.Rows.Add(tr);
                labourCount++;
            }
        }

        protected void GetMaterialsData(int id)
        {
            // Clear rows
            tblMaterialsSummary.Rows.Clear();

            // Set up table header
            TableHeaderRow mth = new TableHeaderRow();
            mth.TableSection = TableRowSection.TableHeader;
            string[] materialHeaderArray = { "#", "Type", "Quantity", "Description", "Size", "Unit Price", "Extended Price", "Action" };

            // Populate table header with headings
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
            int materialCount = 1;

            foreach (var w in materialQuery)
            {
                // Create row, cells and controls
                TableRow tr = new TableRow();
                TableCell matCount = new TableCell();
                TableCell matType = new TableCell();
                TableCell matQty = new TableCell();
                TableCell matDesc = new TableCell();
                TableCell matSize = new TableCell();
                TableCell matUnPrice = new TableCell();
                TableCell matExPrice = new TableCell();
                TableCell action = new TableCell();
                LinkButton delete = new LinkButton();

                // Code for the remove button
                delete.CssClass = "btn btn-default btn-xs btnColorOverride";
                delete.Attributes.Add("data-toggle", "tooltip");
                delete.Attributes.Add("data-id", w.ID.ToString());
                delete.Attributes.Add("title", "Remove");
                delete.Attributes.Add("runat", "server");
                delete.Text = "<span class='glyphicon glyphicon-remove'></span>";
                delete.OnClientClick = "return confirm('Are you sure?');";
                delete.Click += new EventHandler(DeleteMaterialRequirement);
                action.Controls.Add(delete);

                // Add data to controls
                matCount.Text = materialCount.ToString();
                matType.Text = w.INVENTORY.MATERIAL.matType;
                matQty.Text = w.mreqEstQty.ToString();
                matDesc.Text = w.INVENTORY.MATERIAL.matDesc;

                //Not all the units have a size amount category. If the first is null just set it to 1
                matSize.Text = (w.INVENTORY.invSizeAmnt == null) ? "1 " + w.INVENTORY.invSizeUnit : w.INVENTORY.invSizeAmnt.ToString() + " " + w.INVENTORY.invSizeUnit;
                matUnPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.INVENTORY.invList), 2));
                matExPrice.Text = string.Format("{0:C}", Math.Round(Convert.ToDecimal(w.INVENTORY.invList * w.mreqEstQty), 2));

                // Add cells to row
                tr.Cells.Add(matCount);
                tr.Cells.Add(matType);
                tr.Cells.Add(matQty);
                tr.Cells.Add(matDesc);
                tr.Cells.Add(matSize);
                tr.Cells.Add(matUnPrice);
                tr.Cells.Add(matExPrice);
                tr.Cells.Add(action);

                // Increase Row Count
                materialCount++;

                // Increase Materials Total
                materialsTotal += Convert.ToDecimal(w.INVENTORY.invList * w.mreqEstQty);

                //Add Row to Table
                tblMaterialsSummary.Rows.Add(tr);
            }
        }

        protected void DeleteLaborRequirement(object sender, EventArgs e)
        {
            // Get button that was clicked
            LinkButton lb = (LinkButton)sender;
            
            // Get the ID of the labour requirement from the button
            int id = Convert.ToInt32(lb.Attributes["data-id"]);

            // Find the requirement
            LABOUR_REQUIREMENT lr = db.LABOUR_REQUIREMENT.Find(id);

            // Remove
            db.LABOUR_REQUIREMENT.Remove(lr);

            try
            {
                db.SaveChanges();
                Response.Redirect("EditDesignBid.aspx", false);
            }
            catch (DataException dx)
            {
                NotifyJS.DisplayNotification(this.Page, dx.InnerException.InnerException.Message, "danger");
            }
        }

        protected void DeleteMaterialRequirement(object sender, EventArgs e)
        {
            // Get the button that was clicked
            LinkButton lb = (LinkButton)sender;

            // Get the material id from the button
            int id = Convert.ToInt32(lb.Attributes["data-id"]);

            // Find the material
            MATERIAL_REQ mr = db.MATERIAL_REQ.Find(id);

            // Remove
            db.MATERIAL_REQ.Remove(mr);

            try
            {
                db.SaveChanges();
                Response.Redirect("EditDesignBid.aspx", false);
            }
            catch (DataException dx)
            {
                NotifyJS.DisplayNotification(this.Page, dx.InnerException.InnerException.Message, "danger");
            }
        }

        protected void btnSaveBid_Click(object sender, EventArgs e)
        {
            // Get project id from session
            int projectID = Convert.ToInt32(Session["ProjectID"]);

            // Find the project
            PROJECT proj = db.PROJECTs.Find(projectID);

            // Modify data
            proj.projName = mdlTxtProjectName.Text;
            proj.projSite = mdlTxtProjectSite.Text;
            proj.projEstStart = mdlTxtProjectBeginDate.Text;
            proj.projEstEnd = mdlTxtProjectEndDate.Text;

            // Change entry state
            db.Entry(proj).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
                Response.Redirect("EditDesignBid.aspx", false);
            }
            catch (DataException ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }


        protected void btnAddLabor_Click(object sender, EventArgs e)
        {
            // Get info from add textbox
            int projectID = Convert.ToInt32(Session["ProjectID"]);
            int workerTypeID = Convert.ToInt32(ddlLaborType.SelectedValue);
            int taskDescriptionID = Convert.ToInt32(ddlLaborTask.SelectedValue);
            short hours = Convert.ToInt16(txtLaborHours.Text);

            // Get production team id
            var labour = db.LABOUR_REQUIREMENT
                .Where(p => p.ID == projectID)
                .SingleOrDefault();

            // Add new labour requirement 
            LABOUR_REQUIREMENT lr = new LABOUR_REQUIREMENT();
            lr.lreqEstHours = hours;
            lr.workerTypeID = workerTypeID;
            lr.taskID = taskDescriptionID;
            lr.projectID = projectID;
            lr.prodTeamID = Convert.ToInt32(labour.prodTeamID);
            db.LABOUR_REQUIREMENT.Add(lr);

            try
            {
                db.SaveChanges();
                Response.Redirect("EditDesignBid.aspx", false);
            }
            catch (DataException ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }

        protected void btnMaterialAdd_Click(object sender, EventArgs e)
        {
            // Get material id from drop down
            int matID = Convert.ToInt32(ddlMaterialDescription.SelectedValue);

            // Search inventory table
            var invQuery = (from inventory in db.INVENTORies
                            where inventory.materialID == matID
                            select inventory).SingleOrDefault();

            // Get id from query
            int invID = invQuery.ID;

            // get quantity from text box
            short matQty = Convert.ToInt16(txtMaterialQuantity.Text);

            // Add new material
            MATERIAL_REQ insertNew = new MATERIAL_REQ();
            insertNew.inventoryID = invID;
            insertNew.projectID = Convert.ToInt32(Session["ProjectID"]);
            insertNew.mreqEstQty = matQty;
            db.MATERIAL_REQ.Add(insertNew);

            // Reset textbox
            txtMaterialQuantity.Text = "";

            try
            {
                db.SaveChanges();
                Response.Redirect("EditDesignBid.aspx", false);
            }
            catch (DataException ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }
    }
}




