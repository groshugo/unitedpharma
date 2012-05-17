﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_DistrictManagement : System.Web.UI.Page
{
    DistrictsRepository dRepo = new DistrictsRepository();
    ProvincesRepository pRepo = new ProvincesRepository();
    SectionRepository sRepo = new SectionRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = dRepo.GetAllViewDistrict();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = dRepo.GetAllViewDistrict();
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
            dRepo.Edit(id, (string)values["DistrictName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlProvince")).SelectedValue));
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
            dRepo.Add((string)values["DistrictName"], Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlProvince")).SelectedValue));
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
            dRepo.Delete(id);
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

            string pId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfProvinceId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfProvinceId")).Value;
            string sId = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfSectionId")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfSectionId")).Value;

            RadComboBox ddlSection = ((RadComboBox)edititem.FindControl("ddlSection"));
            ddlSection.DataSource = sRepo.GetAll();
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataValueField = "Id";
            ddlSection.DataBind();
            ddlSection.SelectedValue = sId;

            RadComboBox ddlProvince = ((RadComboBox)edititem.FindControl("ddlProvince"));
            ddlProvince.DataSource = pRepo.GetProvinceBySectionId(Convert.ToInt32(ddlSection.SelectedValue));
            ddlProvince.DataTextField = "ProvinceName";
            ddlProvince.DataValueField = "Id";
            ddlProvince.DataBind();
            ddlProvince.SelectedValue = pId;
        }
    }

    protected void ddlSection_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
        RadComboBox ddlProvince = editedItem["ProvinceColumn"].FindControl("ddlProvince") as RadComboBox;
        ddlProvince.DataSource = pRepo.GetProvinceBySectionId(Convert.ToInt32(e.Value));
        ddlProvince.DataBind();        
    }
    
}