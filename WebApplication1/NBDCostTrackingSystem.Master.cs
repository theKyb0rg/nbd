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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public EntitiesNBD db = new EntitiesNBD();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Assign username and makeshift email to user settings modal
            txtUserName.Text = Page.User.Identity.Name;
            txtUserEmail.Text = Page.User.Identity.Name + "@nbd.com";
            if (!IsPostBack)
            {
                DropDownHelper.PopulateEmployeeList(ddlCreateTeamEmployees);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Logout functionality
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        protected void btnClientCreate_Click(object sender, EventArgs e)
        {
            // Gather data to create new client
            CLIENT c = new CLIENT();
            c.cliName = txtClientName.Text;
            c.cliAddress = txtClientAddress.Text;
            c.cityID = Convert.ToInt32(ddlClientCity.SelectedValue);
            c.cliProvince = ddlClientProv.SelectedItem.Text;
            c.cliPCode = txtClientPCode.Text;
            c.cliPhone = txtClientPhone.Text;
            c.cliConFName = txtClientConFName.Text;
            c.cliConLName = txtClientConLName.Text;
            c.cliConPosition = txtClientConPosition.Text;
            db.CLIENTs.Add(c);

            try
            {
                db.SaveChanges();
                // Repopulate drop down lists
                DropDownList ddlClientList = (DropDownList)Body.FindControl("ddlClientList");
                DropDownHelper.PopulateClientList(ddlClientList);
                NotifyJS.DisplayNotification(this.Page, c.cliName + " successfully created.", "success");
                ClearClientModal();
            }
            catch (DataException ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }

        protected void btnAddNewEmployee_Click(object sender, EventArgs e)
        {
            // Gather data to create new employee
            WORKER w = new WORKER();
            w.wrkFName = txtEmployeeFName.Text;
            w.wrkLName = txtEmployeeLName.Text;
            w.wrkTypeID = Convert.ToInt32(ddlEmployeePosition.SelectedValue);
            db.WORKERs.Add(w);

            try
            {
                db.SaveChanges();
                // Repopulate drop down lists
                DropDownList ddlEmployeeList = (DropDownList)Body.FindControl("ddlEmployeeList");
                DropDownHelper.PopulateEmployeeList(ddlEmployeeList);
                NotifyJS.DisplayNotification(this.Page, w.wrkFName + " " + w.wrkLName + " successfully created.", "success");
            }
            catch (DataException ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }

        protected void btnSaveClientChanges_Click(object sender, EventArgs e)
        {
            // Find button on body content tag
            LinkButton btnClientRemove = (LinkButton)Body.FindControl("btnClientRemove");

            // Get id from button
            int id = Convert.ToInt32(btnClientRemove.Attributes["data-id"]);

            // Find client data
            CLIENT c = db.CLIENTs.Find(id);
            c.cliName = txtClientName.Text;
            c.cliAddress = txtClientAddress.Text;
            c.cityID = Convert.ToInt32(ddlClientCity.SelectedValue);
            c.cliProvince = ddlClientProv.SelectedItem.Text;
            c.cliPCode = txtClientPCode.Text;
            c.cliPhone = txtClientPhone.Text;
            c.cliConFName = txtClientConFName.Text;
            c.cliConLName = txtClientConLName.Text;
            c.cliConPosition = txtClientConPosition.Text;

            // Set state to modified
            db.Entry(c).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();

                // Repopulate drop down lists
                DropDownList ddlClientList = (DropDownList)Body.FindControl("ddlClientList");
                DropDownHelper.PopulateClientList(ddlClientList);
                NotifyJS.DisplayNotification(this.Page, c.cliName + " successfully modified.", "success");
                ClearClientModal();
                ReverseClientMode(true);
            }
            catch (DataException ex)
            {
                NotifyJS.DisplayNotification(this.Page, ex.InnerException.InnerException.Message, "danger");
            }
        }
        public void ClearClientModal()
        {
            // Clear controls 
            txtClientName.Text = "";
            txtClientAddress.Text = "";
            ddlClientCity.SelectedItem.Selected = false;
            ddlClientProv.SelectedItem.Selected = false;
            ddlClientCity.Items.FindByValue("-1").Selected = true;
            ddlClientProv.Items.FindByValue("-1").Selected = true;
            txtClientPCode.Text = "";
            txtClientPhone.Text = "";
            txtClientConFName.Text = "";
            txtClientConLName.Text = "";
            txtClientConPosition.Text = "";
        }

        public void ReverseClientMode(bool reverse)
        {
            pnlCreateNewClient.Visible = reverse;
            pnlEditClient.Visible = !reverse;
            btnClientCreate.Visible = reverse;
            btnSaveClientChanges.Visible = !reverse;
        }

        protected void btnClientCancel_Click(object sender, EventArgs e)
        {
            ClearClientModal();
            ReverseClientMode(true);
        }
    }
}