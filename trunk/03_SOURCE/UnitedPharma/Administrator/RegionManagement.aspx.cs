using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_RegionManagement : System.Web.UI.Page
{
    GroupsRepository grepo = new GroupsRepository();
    RegionsRepository rrepo = new RegionsRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = rrepo.GetAllViewRegion();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = rrepo.GetAllViewRegion();
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
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);

        var id = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            rrepo.Edit(id, (string)values["UpiCode"], (string)values["RegionName"], (string)values["Description"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlGroup")).SelectedValue));
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
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
            rrepo.Add((string)values["UpiCode"], (string)values["RegionName"], (string)values["Description"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlGroup")).SelectedValue));
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }
    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            rrepo.Delete(id);
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
        {
            GridEditableItem edititem = (GridEditableItem)e.Item;
            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);
            string gId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfGroupId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfGroupId")).Value;            
            RadComboBox ddlGroup = ((RadComboBox)edititem.FindControl("ddlGroup"));
            ddlGroup.DataSource = grepo.GetAll();
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "Id";
            ddlGroup.DataBind();
            ddlGroup.SelectedValue = gId.ToString();
        }
    }
}