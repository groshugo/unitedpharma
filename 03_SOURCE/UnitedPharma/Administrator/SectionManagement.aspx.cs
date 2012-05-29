using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SectionManagement : System.Web.UI.Page
{
    SectionRepository repo = new SectionRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        var gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);

        var id = (int)editableItem.GetDataKeyValue("Id");
        
        try
        {
            var sectionName = values["SectionName"] as string;

            if (sectionName == null || string.IsNullOrEmpty(sectionName.Trim()))
            {
                ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_UpdateCommand_Please_provide_role_name_to_update);
                e.Canceled = true;
            }
            else
            {
                var result = repo.Edit(id, sectionName.Trim());
                if (!result)
                {
                    ShowErrorMessage("Section name is unique, please choose another one.");
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

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Salesmen S = new Salesmen();
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        
        try
        {
            var sectionName = values["SectionName"] as string;

            if (sectionName == null || string.IsNullOrEmpty(sectionName.Trim()))
            {
                ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_InsertCommand_Please_provide_role_name_to_add);
                e.Canceled = true;
            }
            else
            {
                var result = repo.Add(sectionName.Trim());
                if (!result)
                {
                    ShowErrorMessage("Section name is unique, please choose another one.");
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