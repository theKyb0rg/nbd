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
    public partial class AddDesignBid : System.Web.UI.Page
    {
        // Declare database
        public static EntitiesNBD db = new EntitiesNBD();

        // Materials summary table
        public static List<string[]> materialRows = new List<string[]>();
        public static List<string[]> laborRows = new List<string[]>();
        public static decimal laborTotal = 0.00m;
        public static decimal materialTotal = 0.00m;
        public static decimal bidAmount = 0.00m;
        public static int materialIDCounter = 1;
        public static int laborIDCounter = 1;
        public static bool materialsTableFlag = false;
        public static bool labourTableFlag = false;

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
            try
            {
                if (!IsPostBack)
                {
                    // Populate 5 drop down lists
                    PopulateDropDownLists();

                    // Build initial materials table
                    BuildInitialMaterialsTable();
                    BuildInitialLaborTable();

                    // Clear the data in the grid views and lists if not post back
                    gvMaterialsSummary.DataSource = new DataTable();
                    gvMaterialsSummary.DataBind();
                    gvLaborSummary.DataSource = new DataTable();
                    gvLaborSummary.DataBind();
                    materialRows.Clear();
                    laborRows.Clear();

                    // Hide the grid view show the label
                    gvLaborSummary.Visible = false;
                    lblLaborSummary.Visible = true;
                    gvMaterialsSummary.Visible = false;
                    lblMaterialsSummary.Visible = true;
                }
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
        // Client Name drop down list
        protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get client id
                int id = Convert.ToInt32(this.ddlClientName.SelectedValue);

                // Get clients with specific id
                var query = from client in db.CLIENTs
                            where client.ID == id
                            select client;

                // Add client to controls
                foreach (var c in query)
                {
                    txtClientContact.Text = c.cliConFName + " " + c.cliConLName;
                    txtClientAddress.Text = c.cliAddress;
                    txtClientPhone.Text = c.cliPhone;
                }
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
        protected void PopulateDropDownLists()
        {
            try
            {
                // Get distinct values from material types
                var materials = (from material in db.MATERIALs
                                 select material.matType).Distinct();

                // Add to materials drop down list
                foreach (var m in materials)
                {
                    ListItem li = new ListItem();
                    li.Text = m;
                    li.Value = m;
                    ddlMaterialType.Items.Add(li);
                }

                // Get sales associates
                var sales = from workers in db.WORKERs
                            join workerType in db.WORKER_TYPE on workers.wrkTypeID equals workerType.ID
                            where workerType.ID == 7
                            select workers;

                // Add to sales drop down list
                foreach (var s in sales)
                {
                    ListItem li = new ListItem();
                    li.Text = s.wrkFName + " " + s.wrkLName;
                    li.Value = s.ID.ToString();
                    ddlNBDSales.Items.Add(li);
                }

                // Get designers
                var designers = from workers in db.WORKERs
                                join workerType in db.WORKER_TYPE on workers.wrkTypeID equals workerType.ID
                                where workerType.ID == 2 || workerType.ID == 5
                                select workers;

                // Add to designers drop down list
                foreach (var d in designers)
                {
                    ListItem li = new ListItem();
                    li.Text = d.wrkFName + " " + d.wrkLName;
                    li.Value = d.ID.ToString();
                    ddlNBDDesigner.Items.Add(li);
                }

                // Add employees to create labor type modal
                var workerTypes = from workers in db.WORKER_TYPE
                                  orderby workers.wrkTypeDesc ascending
                                  select workers;

                foreach (var w in workerTypes)
                {
                    ListItem li = new ListItem();
                    li.Text = w.wrkTypeDesc;
                    li.Value = w.ID.ToString();
                    ddlLaborType.Items.Add(li);
                }

                // Get the list of tasks from the database
                var tasks = from task in db.TASKs
                            orderby task.taskDesc ascending
                            select task;

                foreach (var t in tasks)
                {
                    ListItem li = new ListItem();
                    li.Text = t.taskDesc;
                    li.Value = t.ID.ToString();
                    ddlLaborTask.Items.Add(li);
                }
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

        protected void btnMaterialAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Add new row
                AddNewRowToMaterialsTable();

                // Set visibility on summary label
                lblMaterialsSummary.Visible = false;

                // Assign values to string array and add to materialsRow list
                string[] row = {
                    ddlMaterialType.SelectedItem.Text,
                    txtMaterialQuantity.Text,
                    ddlMaterialDescription.SelectedValue,
                    ddlMaterialSize.SelectedItem.Text,
                    txtMaterialUnitPrice.Text,
                    txtMaterialExtendedPrice.Text
                };

                // Add data for use with db
                materialRows.Add(row);

                // Increase materials totral
                materialTotal += Convert.ToDecimal(txtMaterialExtendedPrice.Text);

                // Update bidamount
                bidAmount = materialTotal + laborTotal;

                // Apply bid amount to control
                txtBidAmount.Text = bidAmount.ToString();

                // Set focus on hidden label to avoid page jump
                lblFocusHAX.Focus();

                // Add id to delete drop down
                ddlDeleteMaterialFromTable.Items.Add(materialIDCounter.ToString());

                // Incrememnt material ID count
                materialIDCounter++;

                //Shows that a row has been added to the table
                materialsTableFlag = true;
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
        protected void btnLaborAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                // Add new row
                AddNewRowToLaborTable();

                // Set the summary label visibility
                lblLaborSummary.Visible = false;

                // Assign values to string array for prep in the database
                string[] row = {
                    ddlLaborType.SelectedItem.Text,
                    txtLaborHours.Text,
                    ddlLaborTask.SelectedValue,
                    txtLaborUnitPrice.Text,
                    txtLaborExtendedPrice.Text,
                    ddlLaborType.SelectedValue
                };
                
                // Add to labor rows for use with db
                laborRows.Add(row);

                // Increase labor total
                laborTotal += Convert.ToDecimal(txtLaborExtendedPrice.Text);

                // Adjust the bid amount
                bidAmount = laborTotal + materialTotal;

                // Display bid amount in textbox
                txtBidAmount.Text = bidAmount.ToString();

                // Set focus on hidden label to avoid page jump
                lblFocusHAX.Focus();

                // Add id to delete control drop down
                ddlDeleteLaborFromTable.Items.Add(laborIDCounter.ToString());

                // Increment labor ID counter
                laborIDCounter++;

                //Shows that a row has been added to the table
                labourTableFlag = true;
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
        protected void btnMaterialClear_Click(object sender, EventArgs e)
        {
            // Clear controls
            txtMaterialQuantity.Text = "0";
            txtMaterialUnitPrice.Text = "0.00";
            txtMaterialExtendedPrice.Text = "0.00";
            ddlMaterialDescription.Items.Clear();
            ddlMaterialDescription.Items.Add("Material type not selected.");
            ddlMaterialSize.Items.Clear();
            ddlMaterialSize.Items.Add("Item not selected.");
            ddlMaterialType.SelectedIndex = -1;
        }
        protected void btnLaborClear_Click(object sender, EventArgs e)
        {
            // Clear controls
            ddlLaborType.Items.Clear();
            ddlLaborType.Items.Add("Select a worker type...");
            ddlLaborTask.Items.Clear();
            ddlLaborTask.Items.Add("Select a task...");
            txtLaborExtendedPrice.Text = "0.00";
            txtLaborHours.Text = "0";
            txtLaborHours.Text = "0.00";
        }

        protected void ddlMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear textboxes
                txtMaterialExtendedPrice.Text = "0.00";
                txtMaterialQuantity.Text = "0";
                txtMaterialUnitPrice.Text = "0.00";

                // Clear the drop down and add back the first item for the postback
                ddlMaterialDescription.Items.Clear();
                ListItem initial = new ListItem();
                initial.Text = "Select an Item...";
                initial.Value = "-1";
                ddlMaterialDescription.Items.Add(initial);

                // Clear the item drop down list
                ddlMaterialSize.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "Item not selected.";
                item.Value = "-1";
                ddlMaterialSize.Items.Add(item);

                // Preload all materials so visual studio doesnt fight me
                var materials = from material in db.MATERIALs
                                select material;

                if (ddlMaterialType.SelectedValue == "Materials")
                {
                    materials = from material in db.MATERIALs
                                where material.matType == "Materials"
                                select material;
                }
                else if (ddlMaterialType.SelectedValue == "Plant")
                {
                    materials = from material in db.MATERIALs
                                where material.matType == "Plant"
                                select material;
                }
                else if (ddlMaterialType.SelectedValue == "Pottery")
                {
                    materials = from material in db.MATERIALs
                                where material.matType == "Pottery"
                                select material;
                }

                // Add to materials drop down list
                foreach (var m in materials)
                {
                    ListItem li = new ListItem();
                    li.Text = m.matDesc;
                    li.Value = m.ID.ToString();
                    ddlMaterialDescription.Items.Add(li);
                }
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
        protected void ddlMaterialDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear the drop down list and append initial value
                ddlMaterialSize.Items.Clear();

                if (ddlMaterialDescription.SelectedValue == "-1")
                {
                    ListItem item = new ListItem();
                    item.Text = "Item not selected.";
                    item.Value = "-1";
                    ddlMaterialSize.Items.Add(item);
                }

                // Clear the textboxes
                txtMaterialExtendedPrice.Text = "0.00";
                txtMaterialQuantity.Text = "0";

                // Get id of material
                int id = Convert.ToInt32(ddlMaterialDescription.SelectedValue);

                // Get the selected inventory item
                var materials = from material in db.MATERIALs
                                join inventory in db.INVENTORies on material.ID equals inventory.materialID
                                where inventory.materialID == id
                                select inventory;

                foreach (var m in materials)
                {
                    ListItem li = new ListItem();
                    li.Text = m.invSizeAmnt + " " + m.invSizeUnit;
                    li.Value = Math.Round(Convert.ToDecimal(m.invList), 2).ToString();
                    ddlMaterialSize.Items.Add(li);
                    txtMaterialUnitPrice.Text = Math.Round(Convert.ToDecimal(m.invList), 2).ToString();
                }

                // Select the last item in the list
                ddlMaterialSize.SelectedIndex = ddlMaterialSize.Items.Count - 1;
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

        protected void ddlLaborType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get id of worker
                int id = Convert.ToInt32(ddlLaborType.SelectedValue);

                // Get the selected cost for the type of worker
                var costs = (from cost in db.WORKER_TYPE
                             where cost.ID == id
                             orderby cost.wrkTypeDesc ascending
                             select cost).SingleOrDefault();

                if (ddlLaborType.SelectedValue == "-1")
                {
                    txtLaborUnitPrice.Text = "0.00";
                    txtLaborHours.Text = "0";
                    txtLaborExtendedPrice.Text = "0.00";
                }
                else
                {
                    txtLaborUnitPrice.Text = Math.Round(Convert.ToDecimal(costs.wrkTypePrice), 2).ToString();
                    txtLaborExtendedPrice.Text = Math.Round(Convert.ToDecimal(txtLaborUnitPrice.Text) * Convert.ToDecimal(txtLaborHours.Text), 2).ToString();
                }
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
        /* ----------------------------------------------------------------------------------------- */
        /* ------------------------ GRID VIEW CODE FOR MATERIALS TABLE ----------------------------- */
        /* ----------------------------------------------------------------------------------------- */
        private void BuildInitialMaterialsTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("#", typeof(string)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Size", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit Price", typeof(string)));
            dt.Columns.Add(new DataColumn("Extended Price", typeof(string)));

            //Store the DataTable in ViewState
            ViewState["MaterialsTable"] = dt;

            gvMaterialsSummary.DataSource = dt;
            gvMaterialsSummary.DataBind();
        }
        private void AddNewRowToMaterialsTable()
        {
            int rowIndex = 0;
            if (ViewState["MaterialsTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["MaterialsTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count == 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["#"] = materialIDCounter;
                    drCurrentRow["Type"] = ddlMaterialType.SelectedItem.Text;
                    drCurrentRow["Quantity"] = txtMaterialQuantity.Text;
                    drCurrentRow["Description"] = ddlMaterialDescription.SelectedItem.Text;
                    drCurrentRow["Size"] = ddlMaterialSize.SelectedItem.Text;
                    drCurrentRow["Unit Price"] = txtMaterialUnitPrice.Text;
                    drCurrentRow["Extended Price"] = txtMaterialExtendedPrice.Text;
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["MaterialsTable"] = dtCurrentTable;

                    gvMaterialsSummary.DataSource = dtCurrentTable;
                    gvMaterialsSummary.DataBind();

                }
                else if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        string type = ddlMaterialType.SelectedItem.Text;
                        string quantity = txtMaterialQuantity.Text;
                        string description = ddlMaterialDescription.SelectedItem.Text;
                        string size = ddlMaterialSize.SelectedItem.Text;
                        string unitPrice = txtMaterialUnitPrice.Text;
                        string extPrice = txtMaterialExtendedPrice.Text;

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["#"] = materialIDCounter;
                        drCurrentRow["Type"] = type;
                        drCurrentRow["Quantity"] = quantity;
                        drCurrentRow["Description"] = description;
                        drCurrentRow["Size"] = size;
                        drCurrentRow["Unit Price"] = unitPrice;
                        drCurrentRow["Extended Price"] = extPrice;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["MaterialsTable"] = dtCurrentTable;
                    gvMaterialsSummary.DataSource = dtCurrentTable;
                    gvMaterialsSummary.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousDataOnMaterialsTable();
        }
        private void SetPreviousDataOnMaterialsTable()
        {
            int rowIndex = 0;
            if (ViewState["MaterialsTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["MaterialsTable"];
                if (dt.Rows.Count > 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string type = gvMaterialsSummary.Rows[rowIndex].Cells[1].Text;
                        string quantity = gvMaterialsSummary.Rows[rowIndex].Cells[2].Text;
                        string description = gvMaterialsSummary.Rows[rowIndex].Cells[3].Text;
                        string size = gvMaterialsSummary.Rows[rowIndex].Cells[4].Text;
                        string unitPrice = gvMaterialsSummary.Rows[rowIndex].Cells[5].Text;
                        string extPrice = gvMaterialsSummary.Rows[rowIndex].Cells[6].Text;

                        type = dt.Rows[i]["Type"].ToString();
                        quantity = dt.Rows[i]["Quantity"].ToString();
                        description = dt.Rows[i]["Description"].ToString();
                        size = dt.Rows[i]["Size"].ToString();
                        unitPrice = dt.Rows[i]["Unit Price"].ToString();
                        extPrice = dt.Rows[i]["Extended Price"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        protected void gvMaterialsSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            gvMaterialsSummary.Visible = true;
            lblMaterialsSummary.Visible = false;
            if (gvMaterialsSummary.Rows.Count == 0)
            {
                pnlDeleteMaterialRowControls.Visible = false;
            }
            else
            {
                pnlDeleteMaterialRowControls.Visible = true;
            }
        }
        /* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */
        /* ^^^^^^^^^^^^^^^^^^^^^^^^^^ GRID VIEW CODE FOR TABLES ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */
        /* ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^ */
        private void BuildInitialLaborTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("#", typeof(string)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));
            dt.Columns.Add(new DataColumn("Hours", typeof(string)));
            dt.Columns.Add(new DataColumn("Task", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit Price", typeof(string)));
            dt.Columns.Add(new DataColumn("Extended Price", typeof(string)));

            //Store the DataTable in ViewState
            ViewState["LaborTable"] = dt;

            gvLaborSummary.DataSource = dt;
            gvLaborSummary.DataBind();
        }
        private void AddNewRowToLaborTable()
        {
            int rowIndex = 0;

            if (ViewState["LaborTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["LaborTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count == 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["#"] = laborIDCounter;
                    drCurrentRow["Type"] = ddlLaborType.SelectedItem.Text;
                    drCurrentRow["Hours"] = txtLaborHours.Text;
                    drCurrentRow["Task"] = ddlLaborTask.SelectedItem.Text;
                    drCurrentRow["Unit Price"] = txtLaborUnitPrice.Text;
                    drCurrentRow["Extended Price"] = txtLaborExtendedPrice.Text;
                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["LaborTable"] = dtCurrentTable;
                    gvLaborSummary.DataSource = dtCurrentTable;
                    gvLaborSummary.DataBind();
                }
                else if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        string type = ddlLaborType.SelectedItem.Text;
                        string hours = txtLaborHours.Text;
                        string task = ddlLaborTask.SelectedItem.Text;
                        string unitPrice = txtLaborUnitPrice.Text;
                        string extPrice = txtLaborExtendedPrice.Text;

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["#"] = laborIDCounter;
                        drCurrentRow["Type"] = type;
                        drCurrentRow["Hours"] = hours;
                        drCurrentRow["Task"] = task;
                        drCurrentRow["Unit Price"] = unitPrice;
                        drCurrentRow["Extended Price"] = extPrice;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["LaborTable"] = dtCurrentTable;
                    gvLaborSummary.DataSource = dtCurrentTable;
                    gvLaborSummary.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousDataOnLaborTable();
        }
        private void SetPreviousDataOnLaborTable()
        {
            int rowIndex = 0;
            if (ViewState["LaborTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["LaborTable"];
                if (dt.Rows.Count > 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string type = gvLaborSummary.Rows[rowIndex].Cells[1].Text;
                        string hours = gvLaborSummary.Rows[rowIndex].Cells[2].Text;
                        string task = gvLaborSummary.Rows[rowIndex].Cells[3].Text;
                        string unitPrice = gvLaborSummary.Rows[rowIndex].Cells[4].Text;
                        string extPrice = gvLaborSummary.Rows[rowIndex].Cells[5].Text;

                        type = dt.Rows[i]["Type"].ToString();
                        hours = dt.Rows[i]["Hours"].ToString();
                        task = dt.Rows[i]["Task"].ToString();
                        unitPrice = dt.Rows[i]["Unit Price"].ToString();
                        extPrice = dt.Rows[i]["Extended Price"].ToString();

                        rowIndex++;
                    }
                }
            }
        }
        protected void gvLaborSummary_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Show grid view hide label
            gvLaborSummary.Visible = true;
            lblLaborSummary.Visible = false;

            if (gvLaborSummary.Rows.Count == 0)
            {
                pnlLaborDeleteControls.Visible = false;
            }
            else
            {
                pnlLaborDeleteControls.Visible = true;
            }
        }
        /* ----------------------------------------------------------------------------------------- */
        /* ------------------------ GRID VIEW CODE FOR LABOR TABLE ----------------------------- */
        /* ----------------------------------------------------------------------------------------- */
        protected void btnDesignBidCreate_Click(object sender, EventArgs e)
        {
            if (labourTableFlag == false || materialsTableFlag == false)
            {
                if (materialsTableFlag == false)
                {
                    NotifyJS.DisplayNotification(this.Page, "You cannot create a Design Bid with no Materials added.", "danger");
                }

                if (labourTableFlag == false)
                {
                    NotifyJS.DisplayNotification(this.Page, "You cannot create a Design Bid with no Labor Requirements.", "danger");
                }
            }
            else
            {
                try
                {
                    // Get the last ID from the projects table
                    var projId = db.PROJECTs.Max(x => x.ID) + 1;

                    // Store project information into project variable
                    PROJECT p = new PROJECT();
                    p.ID = projId;
                    p.projName = txtProjectName.Text;
                    p.projSite = txtProjectSite.Text;
                    p.projBidDate = DateTime.Now;
                    p.projEstStart = txtProjectBeginDate.Text;
                    p.projEstEnd = txtProjectEndDate.Text;
                    p.projActStart = "TBA";
                    p.projActEnd = "TBA";
                    p.projEstCost = (txtBidAmount.Text == string.Empty) ? "0.00" : txtBidAmount.Text;
                    p.projActCost = "0";
                    p.projBidCustAccept = false;
                    p.projBidMgmtAccept = false;
                    p.projCurrentPhase = "D";
                    p.projIsFlagged = false;
                    p.clientID = Convert.ToInt32(ddlClientName.SelectedValue);
                    p.designerID = Convert.ToInt32(ddlNBDDesigner.SelectedValue);

                    // Add to projects table
                    db.PROJECTs.Add(p);

                    // MATERIAL REQUIREMENTS
                    for (int i = 0; i < materialRows.Count; i++)
                    {
                        // Get the appropriate inventory ID for the material
                        int matID = Convert.ToInt32(materialRows[i][2]);
                        var invID = db.INVENTORies.Where(x => x.ID == matID).SingleOrDefault();

                        // Create material requirements object to hold material data
                        MATERIAL_REQ m = new MATERIAL_REQ();
                        m.inventoryID = invID.ID;
                        m.projectID = projId;
                        m.mreqDeliver = null;
                        m.mreqInstall = null;
                        m.mreqEstQty = Convert.ToInt16(materialRows[i][1]);
                        m.mreqActQty = null;

                        // Add to material requirements table
                        db.MATERIAL_REQ.Add(m);
                    }

                    // PRODUCTION TEAM
                    PROD_TEAM team = new PROD_TEAM();
                    team.projectID = projId;
                    team.teamName = "TEST TEAM BRA";
                    team.teamPhaseIn = "B";
                    db.PROD_TEAM.Add(team);

                    // Save changes in order to get the new max prod team id
                    db.SaveChanges();

                    // Get the last production team ID
                    int teamID = db.PROD_TEAM.Max(m => m.ID);

                    //Add Designer
                    TEAM_MEMBER designer = new TEAM_MEMBER();
                    designer.workerID = p.designerID;
                    designer.teamID = teamID;
                    db.TEAM_MEMBER.Add(designer);

                    //Add Sales Associate
                    TEAM_MEMBER sales = new TEAM_MEMBER();
                    sales.workerID = Convert.ToInt32(ddlNBDSales.SelectedValue);
                    sales.teamID = teamID;
                    db.TEAM_MEMBER.Add(sales);

                    // LABOR SUMMARY
                    LABOUR_SUMMARY ls = new LABOUR_SUMMARY();
                    ls.projectID = projId;
                    ls.workerTypeID = Convert.ToInt32(ddlLaborType.SelectedValue);

                    // Total up labor hours and add to object
                    short labourHours = 0;
                    for (int i = 0; i < laborRows.Count; i++)
                    {
                        labourHours += Convert.ToInt16(laborRows[i][1]);
                    }
                    ls.lsHours = labourHours;
                    db.LABOUR_SUMMARY.Add(ls);

                    for (int i = 0; i < laborRows.Count; i++)
                    {
                        LABOUR_REQUIREMENT l = new LABOUR_REQUIREMENT();
                        l.prodTeamID = teamID;
                        l.taskID = Convert.ToInt32(laborRows[i][2]);
                        l.lreqEstDate = null;
                        l.lreqEstHours = Convert.ToInt16(laborRows[i][1]);
                        l.lreqActDate = null;
                        l.lreqActHours = null;
                        l.lreqComments = null;
                        l.projectID = projId;
                        l.workerTypeID = Convert.ToInt32(laborRows[i][5]);
                        db.LABOUR_REQUIREMENT.Add(l);
                    }

                    // Save
                    db.SaveChanges();

                    // Display notification
                    Response.Redirect("~/Main.aspx", false);
                    NotifyJS.DisplayNotification(this.Page, "Design bid " + p.projName + " for " + p.CLIENT.cliName + " successfully added.", "success");
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
        }

        protected void btnDesignBidCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main.aspx");
        }

        protected void txtLaborHours_TextChanged(object sender, EventArgs e)
        {
            txtLaborExtendedPrice.Text = (Convert.ToDecimal(txtLaborHours.Text) * Convert.ToDecimal(txtLaborUnitPrice.Text)).ToString();
            lblFocusHAX.Focus();
        }

        protected void txtMaterialQuantity_TextChanged(object sender, EventArgs e)
        {
            txtMaterialExtendedPrice.Text = (Convert.ToDecimal(txtMaterialQuantity.Text) * Convert.ToDecimal(txtMaterialUnitPrice.Text)).ToString();
            lblFocusHAX.Focus();
        }

        protected void btnMaterialRemove_Click(object sender, EventArgs e)
        {
            // Get ID from drop down list
            string id = ddlDeleteMaterialFromTable.SelectedItem.Text;

            // Get datatable from view state
            DataTable dtCurrentTable = (DataTable)ViewState["MaterialsTable"];
            if (ddlDeleteMaterialFromTable.SelectedItem.Text == "Delete All Rows")
            {
                dtCurrentTable.Clear();
                materialRows.Clear();
                ddlDeleteMaterialFromTable.Items.Clear();
                ddlDeleteMaterialFromTable.Items.Add("Delete All Rows");
                materialTotal = 0;
                bidAmount = materialTotal + laborTotal;
                txtBidAmount.Text = bidAmount.ToString();
                materialIDCounter = 1;
                lblMaterialsSummary.Visible = true;
                pnlDeleteMaterialRowControls.Visible = false;
                materialsTableFlag = false;
            }
            else
            {
                // Delete reference in drop down list
                ddlDeleteMaterialFromTable.Items.Remove(id);

                // Remove row from datatable
                for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtCurrentTable.Rows[i];
                    if (dr["#"].ToString() == id)
                    {
                        dr.Delete();
                        decimal extPrice = Convert.ToDecimal(materialRows[i][5]);
                        materialTotal -= extPrice;
                        bidAmount = materialTotal + laborTotal;
                        materialRows.RemoveAt(i);
                    }
                }
                txtBidAmount.Text = bidAmount.ToString();
                if (dtCurrentTable.Rows.Count == 0)
                {
                    pnlDeleteMaterialRowControls.Visible = false;
                    lblMaterialsSummary.Visible = true;
                    materialsTableFlag = false;
                }
            }

            // Rebind the datasource
            gvMaterialsSummary.DataSource = dtCurrentTable;
            gvMaterialsSummary.DataBind();
        }

        protected void btnLaborDeleteRow_Click(object sender, EventArgs e)
        {
            // Get ID from drop down list
            string id = ddlDeleteLaborFromTable.SelectedItem.Text;

            // Get datatable from view state
            DataTable dtCurrentTable = (DataTable)ViewState["LaborTable"];
            if (ddlDeleteLaborFromTable.SelectedItem.Text == "Delete All Rows")
            {
                dtCurrentTable.Clear();
                laborRows.Clear();
                ddlDeleteLaborFromTable.Items.Clear();
                ddlDeleteLaborFromTable.Items.Add("Delete All Rows");
                laborTotal = 0;
                bidAmount = laborTotal + materialTotal;
                txtBidAmount.Text = bidAmount.ToString();
                laborIDCounter = 1;
                lblLaborSummary.Visible = true;
                pnlLaborDeleteControls.Visible = false;
                labourTableFlag = false;
            }
            else
            {
                // Delete reference in drop down list
                ddlDeleteLaborFromTable.Items.Remove(id);

                // Remove row from datatable
                for (int i = dtCurrentTable.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dtCurrentTable.Rows[i];
                    if (dr["#"].ToString() == id)
                    {
                        dr.Delete();
                        decimal extPrice = Convert.ToDecimal(laborRows[i][4]);
                        laborTotal -= extPrice;
                        bidAmount = laborTotal + materialTotal;
                        laborRows.RemoveAt(i);
                    }
                }
                txtBidAmount.Text = bidAmount.ToString();
                if (dtCurrentTable.Rows.Count == 0)
                {
                    pnlLaborDeleteControls.Visible = false;
                    lblLaborSummary.Visible = true;
                    labourTableFlag = false;
                }
            }

            // Rebind the datasource
            gvLaborSummary.DataSource = dtCurrentTable;
            gvLaborSummary.DataBind();
        }
    }
}