using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_Default : System.Web.UI.Page
{
    RolesRepository repo = new RolesRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = repo.GetAll();
            RadGrid1.DataBind();
        }
    }


    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAll();
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var id = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            var roleName = values["RoleName"] as string;

            if (roleName == null || string.IsNullOrEmpty(roleName.Trim()))
            {
                ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_UpdateCommand_Please_provide_role_name_to_update);
                e.Canceled = true;
            }
            else
            {
                var result = repo.Edit(id, roleName.Trim());
                if (!result)
                {
                    ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_UpdateCommand_Can_not_update__please_provide_new_name_or_try_again_or_contact_administrator);
                    e.Canceled = true;
                }
            }
        }
        catch (System.Exception ex)
        {
            e.Canceled = true;
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_UpdateCommand_can_not_update__please_try_again_later_or_contact_admnistrator_);
        }

    }

    private void ShowErrorMessage(string errMsg)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"{0}!\")", errMsg));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var roleName = values["RoleName"] as string;

            if (roleName == null || string.IsNullOrEmpty(roleName.Trim()))
            {
                ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_InsertCommand_Please_provide_role_name_to_add);
                e.Canceled = true;
            }
            else
            {
                var result = repo.Add(roleName.Trim());
                if(!result)
                {
                    ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_InsertCommand_Can_not_add_role);
                    e.Canceled = true;
                }
            }
            
        }
        catch (System.Exception)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_InsertCommand_can_not_add__please_try_again_later_or_contact_admnistrator_);
            e.Canceled = true;
        }
    }
    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        int id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(id);
        }
        catch (System.Exception)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_DeleteCommand_can_not_delete__please_try_again_later_or_contact_admnistrator_);
        }

    }
}