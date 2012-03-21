using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_CreateDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }
    protected void grdSalemen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        //var repo = new SalesmanRepository();
        //grdSalemen.DataSource = repo.GetAllViewSales();
    }
    
    protected void btnCreateDashboard_Click(object sender, EventArgs e)
    {
        if (grdSalemen.SelectedItems.Count > 0)
        {
            try
            {
                foreach (GridItem gi in grdSalemen.SelectedItems)
                {
                    string ReceiverPhoneNumber = gi.OwnerTableView.DataKeyValues[gi.ItemIndex]["Phone"].ToString();
                    var repo = new DashboardRepository();
                    ObjLogin adm = (ObjLogin)Session["objLogin"];
                    repo.Add(txtTitle.Text, txtContent.Text, ReceiverPhoneNumber,adm.Phone);
                }

                lblMessage.Text = "<script type='text/javascript'>returnToParent()</" + "script>";
            }
            catch
            {
                lblMessage.Text = "Create Dashboard Fail";
            }
        }
    }
    protected void chkAddAdmin_CheckedChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    private void LoadData()
    {
        AdministratorRepository ARepo = new AdministratorRepository();
        SalesmanRepository SRepo = new SalesmanRepository();
        var SaleList = SRepo.GetSaleToDashboard();
        if (chkAddAdmin.Checked)
        {
            var listAdmin = ARepo.GetAdminToDashboard();
            foreach (var item in listAdmin)
            {
                SaleList.Add(item);
            }
        }
        grdSalemen.DataSource = SaleList;
        grdSalemen.Rebind();
    }
}