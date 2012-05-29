using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_AllowApprove : System.Web.UI.Page
{
    AdministratorRepository ARepo = new AdministratorRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            GetAdminList();
        }
    }

    private void GetAdminList()
    {
        ListBoxFunctions.DataSource = ARepo.GetAdminApprove(false);
        ListBoxFunctions.DataTextField = "Fullname";
        ListBoxFunctions.DataValueField = "Id";
        ListBoxFunctions.DataBind();

        ListBoxFunctionUpdate.DataSource = ARepo.GetAdminApprove(true);
        ListBoxFunctionUpdate.DataTextField = "Fullname";
        ListBoxFunctionUpdate.DataValueField = "Id";
        ListBoxFunctionUpdate.DataBind();
    }
    protected void btnUpdateFunction_Click(object sender, EventArgs e)
    {
        Utility utility = new Utility();

        var sql = "update Administrator set AllowApprove=0";
        utility.SetList(sql);

        var adminIdList = string.Empty;
        // get the current setting for Allow Approve 
        foreach (RadListBoxItem item in ListBoxFunctionUpdate.Items)
        {
            if (item.Value != null) adminIdList += string.Format("{0},", item.Value);
        }

        if (!string.IsNullOrEmpty(adminIdList))
        {
            adminIdList = adminIdList.Substring(0, adminIdList.Length - 1);
            if (adminIdList != "")
            {
                sql = "update Administrator set AllowApprove=1 where Id in (" + adminIdList + ")";
                utility.SetList(sql);
            }
        }
    }
}