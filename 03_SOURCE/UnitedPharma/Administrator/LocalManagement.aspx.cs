using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_LocalManagement : System.Web.UI.Page
{
    GroupsRepository gRepo = new GroupsRepository();
    RegionsRepository rRepo = new RegionsRepository();
    AreasRepository aRepo = new AreasRepository();
    LocalsRepository lRepo = new LocalsRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = lRepo.GetAllViewLocal();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = lRepo.GetAllViewLocal();
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
            lRepo.Edit(id, (string)values["UpiCode"], (string)values["LocalName"], (string)values["Description"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlArea")).SelectedValue));
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
            lRepo.Add((string)values["UpiCode"], (string)values["LocalName"], (string)values["Description"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlArea")).SelectedValue));
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
            lRepo.Delete(id);
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
            string gId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfGroup")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfGroup")).Value;
            string rId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfRegion")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfRegion")).Value;
            string aId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfAreaId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfAreaId")).Value;
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


            RadComboBox ddlArea = ((RadComboBox)edititem.FindControl("ddlArea"));
            ddlArea.DataSource = aRepo.GetAreaByRegionId(Convert.ToInt32(ddlRegion.SelectedValue));
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "Id";
            ddlArea.DataBind();
            ddlArea.SelectedValue = aId;
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;

        RadComboBox ddlRegion = editedItem["AreaColumn"].FindControl("ddlRegion") as RadComboBox;
        ddlRegion.DataSource = rRepo.GetRegionByGroupId(Convert.ToInt32(e.Value));
        ddlRegion.DataBind();

        RadComboBox ddlArea = editedItem["AreaColumn"].FindControl("ddlArea") as RadComboBox;
        ddlArea.DataSource = aRepo.GetAreaByRegionId(Convert.ToInt32(ddlRegion.SelectedValue));
        ddlArea.DataBind();
    }

    protected void ddlRegion_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
        RadComboBox ddlArea = editedItem["AreaColumn"].FindControl("ddlArea") as RadComboBox;
        ddlArea.DataSource = aRepo.GetAreaByRegionId(Convert.ToInt32(e.Value));
        ddlArea.DataBind();
    }
}