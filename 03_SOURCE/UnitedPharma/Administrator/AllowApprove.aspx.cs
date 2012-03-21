using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        int change = ListBoxFunctionUpdate.ClientChanges.Count();
        string AdminIDList = string.Empty;
        for (int i = 0; i < change; i++)
        {
            AdminIDList += ListBoxFunctionUpdate.ClientChanges[i].Item.Value + ",";
        }
        AdminIDList = AdminIDList.Substring(0, AdminIDList.Length - 1);
        if (AdminIDList != "")
        {
            string sql = "update Administrator set AllowApprove=1 where Id in (" + AdminIDList + ")";
            Utility utility = new Utility();
            utility.SetList(sql);
        }
        AdminIDList = string.Empty;
        int unchange = ListBoxFunctions.ClientChanges.Count();
        for (int j = 0; j < unchange; j++)
        {
            AdminIDList += ListBoxFunctions.ClientChanges[j].Item.Value + ",";
        }
        AdminIDList = AdminIDList.Substring(0, AdminIDList.Length - 1);
        if (AdminIDList != "")
        {
            string sql = "update Administrator set AllowApprove=0 where Id in (" + AdminIDList + ")";
            Utility utility = new Utility();
            utility.SetList(sql);
        }
    }
}