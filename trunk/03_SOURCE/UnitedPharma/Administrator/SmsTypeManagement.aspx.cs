using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SmsTypeManagement : System.Web.UI.Page
{
    SmsTypeRepository repo = new SmsTypeRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = repo.GetAll();
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
        int id = (int)editableItem.GetDataKeyValue("Id");
        

        try
        {
            var smsName = values["Name"] as string;
            var syntax = values["Syntax"] as string;

            if (string.IsNullOrEmpty(syntax) && string.IsNullOrEmpty(smsName))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (smsName == null || string.IsNullOrEmpty(smsName.Trim())
                    || syntax == null || string.IsNullOrEmpty(syntax.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var result = repo.Edit(id, smsName, syntax);
                    if (!result)
                    {
                        ShowErrorMessage("SMS Type and Syntax are unique, please choose another one.");
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

    private void ShowErrorMessage(string errMsg)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"{0}\")", errMsg));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        SmsType S = new SmsType();
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        
        try
        {
            var smsName = values["Name"] as string;
            var syntax = values["Syntax"] as string;

            if (string.IsNullOrEmpty(syntax) && string.IsNullOrEmpty(smsName))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (smsName == null || string.IsNullOrEmpty(smsName.Trim())
                    || syntax == null || string.IsNullOrEmpty(syntax.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var result = repo.Add(smsName, syntax);
                    if (!result)
                    {
                        ShowErrorMessage("SMS Type and Syntax are unique, please choose another one.");
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
        catch (System.Exception exception)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_DeleteCommand_can_not_delete__please_try_again_later_or_contact_admnistrator_);
        }
    }
}