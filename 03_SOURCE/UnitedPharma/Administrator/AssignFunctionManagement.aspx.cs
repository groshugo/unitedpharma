using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_AssignFunctionManagement : System.Web.UI.Page
{
    AdministratorRepository Adminrepo = new AdministratorRepository();
    FunctionRepository FuncRepo = new FunctionRepository();
    AssignFunctionRepository AssignRepo = new AssignFunctionRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            AdminList();
            ListNotAssign(int.Parse(ddlUsers.SelectedValue));
            ListAssgin(int.Parse(ddlUsers.SelectedValue));
        }
    }

    protected void ddlUsers_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ListNotAssign(int.Parse(ddlUsers.SelectedValue));
        ListAssgin(int.Parse(ddlUsers.SelectedValue));
    }

    protected void btnUpdateFunction_Click(object sender, EventArgs e)
    {        
        int change = ListBoxFunctionUpdate.ClientChanges.Count();
        if (change > 0)
        {
            for (int i = 0; i < change; i++)
            {
                AssignRepo.UpdateAssignFunction(int.Parse(ListBoxFunctionUpdate.ClientChanges[i].Item.Value), int.Parse(ddlUsers.SelectedValue));
            }
        }       
    }

    private void ListAssgin(int adminId)
    {
        ListBoxFunctionUpdate.DataSource = AssignRepo.GetListAssignedFunction(adminId);
        ListBoxFunctionUpdate.DataTextField = "FunctionName";
        ListBoxFunctionUpdate.DataValueField = "Id";
        ListBoxFunctionUpdate.DataBind();
    }

    private void ListNotAssign(int adminId)
    {
        ListBoxFunctions.DataSource = AssignRepo.GetListNotAssignFunction(adminId);
        ListBoxFunctions.DataTextField = "FunctionName";
        ListBoxFunctions.DataValueField = "Id";
        ListBoxFunctions.DataBind();
    }

    private void UpdateFunction(int functionId, int administratorId)
    {
        AssignRepo.Add(functionId, administratorId);
    }

    private void AdminList()
    {
        ddlUsers.DataSource = Adminrepo.GetAll();
        ddlUsers.DataTextField = "Fullname";
        ddlUsers.DataValueField = "Id";
        ddlUsers.DataBind();
    }
}