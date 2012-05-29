using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
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
        var gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);

        var id = (int)editableItem.GetDataKeyValue("Id");
        
        try
        {
            var upiCode = values["UpiCode"] as string;
            var regionName = values["RegionName"] as string;
            var description = values["Description"] as string;

            if (string.IsNullOrEmpty(regionName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || regionName == null || string.IsNullOrEmpty(regionName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var cboGroup = gdItem.FindControl("ddlGroup") as RadComboBox;
                    if (cboGroup != null)
                    {
                        var result = rrepo.Edit(id, upiCode, regionName, description ?? string.Empty, int.Parse(cboGroup.SelectedValue));
                        if (!result)
                        {
                            ShowErrorMessage("Region name is unique, please choose another one.");
                            e.Canceled = true;
                        }
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
        var gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);

        try
        {
            var upiCode = values["UpiCode"] as string;
            var regionName = values["RegionName"] as string;
            var description = values["Description"] as string;

            if (string.IsNullOrEmpty(regionName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || regionName == null || string.IsNullOrEmpty(regionName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var cboGroup = gdItem.FindControl("ddlGroup") as RadComboBox;
                    if (cboGroup != null)
                    {
                        var result = rrepo.Add(upiCode, regionName, description ?? string.Empty, int.Parse(cboGroup.SelectedValue));
                        if (result == -1)
                        {
                            ShowErrorMessage("Region name is unique, please choose another one.");
                            e.Canceled = true;
                        }
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