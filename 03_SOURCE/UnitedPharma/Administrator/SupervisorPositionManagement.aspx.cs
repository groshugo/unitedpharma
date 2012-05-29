using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SupervisorPositionManagement : System.Web.UI.Page
{
    SupervisorPositionRepository repo = new SupervisorPositionRepository();
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
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);

        var id = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            var posName = values["PositionName"] as string;

            if (posName == null || string.IsNullOrEmpty(posName.Trim()))
            {
                ShowErrorMessage(Pharma.Please_provide_role_name_to_update);
                e.Canceled = true;
            }
            else
            {
                var result = repo.Edit(id, posName.Trim());
                if (!result)
                {
                    ShowErrorMessage("Position Name is unique, please choose another name.");
                    e.Canceled = true;
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
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
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var posName = values["PositionName"] as string;

            if (posName == null || string.IsNullOrEmpty(posName.Trim()))
            {
                ShowErrorMessage(Pharma.Please_provide_name_to_add);
                e.Canceled = true;
            }
            else
            {
                var result = repo.Add(posName.Trim());
                if (!result)
                {
                    ShowErrorMessage("Position Name is unique, please choose another name.");
                    e.Canceled = true;
                }
            }
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
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