using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
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
        var gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);

        var channelId = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            var upiCode = values["UpiCode"] as string;
            var channelName = values["ChannelName"] as string;

            if (string.IsNullOrEmpty(channelName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || channelName == null || string.IsNullOrEmpty(channelName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var cboParentChannel = gdItem.FindControl("ddlParentChannel") as RadComboBox;
                    if (cboParentChannel != null)
                    {
                        var result = channelRepo.Update(channelId, upiCode, channelName, int.Parse(cboParentChannel.SelectedValue));
                        if (!result)
                        {
                            ShowErrorMessage("UPI code and Channel name are unique, please change to another one.");
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
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Salesmen S = new Salesmen();
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var upiCode = values["UpiCode"] as string;
            var channelName = values["ChannelName"] as string;

            if (string.IsNullOrEmpty(channelName) && string.IsNullOrEmpty(upiCode))
            {
                ShowErrorMessage(Pharma.Provide_info_to_insert__please);
                e.Canceled = true;
            }
            else
            {
                if (upiCode == null || string.IsNullOrEmpty(upiCode.Trim())
                    || channelName == null || string.IsNullOrEmpty(channelName.Trim()))
                {
                    ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                    e.Canceled = true;
                }
                else
                {
                    var cboParentChannel = gdItem.FindControl("ddlParentChannel") as RadComboBox;
                    if (cboParentChannel != null)
                    {
                       var result =  channelRepo.Insert(upiCode, channelName, int.Parse(cboParentChannel.SelectedValue));
                        if(!result)
                        {
                            ShowErrorMessage("UPI code and Channel name are unique, please change to another one.");
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

            var item = new RadComboBoxItem("Select parent channel", "0");
            ddlParentChannel.Items.Insert(0, item);

        }
    }
}