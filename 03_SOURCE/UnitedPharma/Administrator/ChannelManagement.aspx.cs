using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_ChannelManagement : System.Web.UI.Page
{
    ChannelRepository channelRepo = new ChannelRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = channelRepo.GetAllViewChannel();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = channelRepo.GetAllViewChannel();
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);

        var ChannelId = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            channelRepo.Update(ChannelId, (string)values["UpiCode"], (string)values["ChannelName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlParentChannel")).SelectedValue));
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
            if (((RadComboBox)gdItem.FindControl("ddlParentChannel")).SelectedIndex == 0)
            {
                channelRepo.Insert((string)values["UpiCode"], (string)values["ChannelName"], null);
            }
            else
            {
                channelRepo.Insert((string)values["UpiCode"], (string)values["ChannelName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlParentChannel")).SelectedValue));
            }
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var channelId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            channelRepo.Delete(channelId);
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
            string parentChannel = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfParentId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfParentId")).Value;
            string currChannelId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfcurrentId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfcurrentId")).Value;
            RadComboBox ddlParentChannel = ((RadComboBox)edititem.FindControl("ddlParentChannel"));
            ddlParentChannel.DataSource = channelRepo.GetListParentChannel(Convert.ToInt32(currChannelId));
            ddlParentChannel.DataTextField = "ChannelName";
            ddlParentChannel.DataValueField = "Id";
            ddlParentChannel.DataBind();
            ddlParentChannel.SelectedValue = parentChannel;

        }
    }
}