using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_AreaManagement : System.Web.UI.Page
{
    AreasRepository aRepo = new AreasRepository();
    RegionsRepository rRepo = new RegionsRepository();
    GroupsRepository gRepo = new GroupsRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = aRepo.GetAllViewArea();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = aRepo.GetAllViewArea();
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
            aRepo.Edit(id, (string)values["UpiCode"], (string)values["AreaName"], (string)values["Description"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlRegion")).SelectedValue));
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
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            aRepo.Add((string)values["UpiCode"], (string)values["AreaName"], (string)values["Description"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlRegion")).SelectedValue));
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
            aRepo.Delete(id);
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
            string rId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfRegionId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfRegionId")).Value;
            string gId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfGroupId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfGroupId")).Value;
            RadComboBox ddlGroup = ((RadComboBox)edititem.FindControl("ddlGroup"));
            ddlGroup.DataSource = gRepo.GetAll();
            ddlGroup.DataTextField = "GroupName";
            ddlGroup.DataValueField = "Id";
            ddlGroup.DataBind();
            ddlGroup.SelectedValue = gId;
            RadComboBox ddlRegion = ((RadComboBox)edititem.FindControl("ddlRegion"));
            ddlRegion.DataSource = rRepo.GetRegionByGroupId(Convert.ToInt32(ddlGroup.SelectedValue));
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataValueField = "Id";
            ddlRegion.DataBind();
            ddlRegion.SelectedValue = rId;
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
        RadComboBox ddlRegion = editedItem["RegionNameColumn"].FindControl("ddlRegion") as RadComboBox;
        ddlRegion.DataSource = rRepo.GetRegionByGroupId(Convert.ToInt32(e.Value));
        ddlRegion.DataBind();        
    }
}