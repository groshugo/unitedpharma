using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_FunctionManagement : System.Web.UI.Page
{
    FunctionRepository repo = new FunctionRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
        }
    }
    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAllViewFunction();
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);

        var FunctionId = (int)editableItem.GetDataKeyValue("Id");
        var Function = repo.GetById(FunctionId);
        if (Function != null)
        {
            int parentFuncId = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlFunctions")).SelectedValue);
            repo.Edit(FunctionId, (string)values["FunctionName"], parentFuncId, (string)values["Action"]);
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
            if (((RadComboBox)gdItem.FindControl("ddlFunctions")).SelectedIndex == 0)
            {
                repo.Add((string)values["FunctionName"], null, (string)values["Action"]);
            }
            else
            {
                repo.Add((string)values["FunctionName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlFunctions")).SelectedValue), (string)values["Action"]);
            }
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var FunctionId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        var Function = repo.GetById(FunctionId);
        if (Function != null)
        {
            try
            {
                repo.Delete(FunctionId);
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
        {
            GridEditableItem edititem = (GridEditableItem)e.Item;
            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);
            string parentFunctionId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfParentId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfParentId")).Value;
            string currFunctionId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfcurrentId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfcurrentId")).Value;
            RadComboBox ddlParentChannel = ((RadComboBox)edititem.FindControl("ddlFunctions"));
            ddlParentChannel.DataSource = repo.GetListParentFunction(Convert.ToInt32(currFunctionId));
            ddlParentChannel.DataTextField = "FunctionName";
            ddlParentChannel.DataValueField = "Id";
            ddlParentChannel.DataBind();
            ddlParentChannel.SelectedValue = parentFunctionId;
        }
    }
}