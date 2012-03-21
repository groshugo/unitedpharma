using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_ProvinceManagement : System.Web.UI.Page
{
    SectionRepository sRepo = new SectionRepository();
    ProvincesRepository pRepo = new ProvincesRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = pRepo.GetAllViewProvince();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = pRepo.GetAllViewProvince();
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
            pRepo.Edit(id, (string)values["ProvinceName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlSection")).SelectedValue));
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
            pRepo.Add((string)values["ProvinceName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlSection")).SelectedValue));
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
            pRepo.Delete(id);
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
            string sId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfSectionId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfSectionId")).Value;
            RadComboBox ddlSection = ((RadComboBox)edititem.FindControl("ddlSection"));
            ddlSection.DataSource = sRepo.GetAll();
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataValueField = "Id";
            ddlSection.DataBind();
            ddlSection.SelectedValue = sId;
        }
    }
}