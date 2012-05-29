using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_CustomerTypeManagement : System.Web.UI.Page
{
    CustomerTypeRepository repo = new CustomerTypeRepository();
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
        var customerTypeId = (int)editableItem.GetDataKeyValue("Id");

        try
        {
            var typeName = values["TypeName"] as string;
            var upiCode = values["UpiCode"] as string;

            if (string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || typeName == null || string.IsNullOrEmpty(typeName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var result = repo.Edit(customerTypeId, upiCode, typeName);
                    if (!result)
                    {
                        ShowErrorMessage("UPI code and Customer Type are unique, please change to another one.");
                        e.Canceled = true;
                    }
                }
            }
        }
        catch (System.Exception)
        {
            ShowErrorMessage(Pharma.can_not_save__try_again_later_or_contact_administrator);
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
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var typeName = values["TypeName"] as string;
            var upiCode = values["UpiCode"] as string;

            if (string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(typeName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || typeName == null || string.IsNullOrEmpty(typeName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var result = repo.Add(upiCode, typeName);
                    if (!result)
                    {
                        ShowErrorMessage("UPI code and Customer Type are unique, please change to another one.");
                        e.Canceled = true;
                    }
                }
            }
        }
        catch (System.Exception)
        {
            ShowErrorMessage(Pharma.can_not_save__try_again_later_or_contact_administrator);
            e.Canceled = true;
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var CustomerTypeId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(CustomerTypeId);
        }
        catch (System.Exception)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_DeleteCommand_can_not_delete__please_try_again_later_or_contact_admnistrator_);
        }

    }
}