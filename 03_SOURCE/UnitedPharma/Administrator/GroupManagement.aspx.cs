using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_GroupManagement : System.Web.UI.Page
{
    GroupsRepository repo = new GroupsRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            BindGrid();
        }
    }

    private void BindGrid()
    {
        RadGrid1.DataSource = repo.GetAll();
        RadGrid1.DataBind();
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAll();
    }
    protected void RadGrid1_CreateColumnEditor(object sender, GridCreateColumnEditorEventArgs e)
    {
        if (e.Column is GridBoundColumn)
        {
            if (e.Column.UniqueName == "Description")
            {
                e.ColumnEditor = new MultiLineTextBoxColumnEditor();
            }
        }
    }
    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);

        var id = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            var upiCode = values["UpiCode"] as string;
            var groupName = values["GroupName"] as string;
            var description = values["Description"] as string;

            if (string.IsNullOrEmpty(groupName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || groupName == null || string.IsNullOrEmpty(groupName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var result = repo.Edit(id, upiCode, groupName, description ?? string.Empty);
                    if (!result)
                    {
                        ShowErrorMessage("Group name is unique, please choose another one.");
                        e.Canceled = true;
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_UpdateCommand_can_not_update__please_try_again_later_or_contact_admnistrator_);
            e.Canceled = true;
        }
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);
        
        try
        {
            var upiCode = values["UpiCode"] as string;
            var groupName = values["GroupName"] as string;
            var description = values["Description"] as string;

            if (string.IsNullOrEmpty(groupName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || groupName == null || string.IsNullOrEmpty(groupName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var result = repo.Add(upiCode, groupName, description ?? string.Empty);
                    if (result == -1)
                    {
                        ShowErrorMessage("Group name and UPI Code are unique, please choose another one.");
                        e.Canceled = true;
                    }
                }
            }
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_InsertCommand_can_not_add__please_try_again_later_or_contact_admnistrator_);
            e.Canceled = true;
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(id);
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }
}