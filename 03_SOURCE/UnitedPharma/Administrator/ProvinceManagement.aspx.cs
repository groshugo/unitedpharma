using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_ProvinceManagement : System.Web.UI.Page
{
    SectionRepository sectionRepo = new SectionRepository();
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
        var gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        var values = new Hashtable();
        editableItem.ExtractValues(values);

        var id = (int)editableItem.GetDataKeyValue("Id");

        try
        {
            var provinceName = values["ProvinceName"] as string;

            if (provinceName == null || string.IsNullOrEmpty(provinceName.Trim()))
            {
                ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                e.Canceled = true;
            }
            else
            {
                var cboSection = gdItem.FindControl("ddlSection") as RadComboBox;
                if (cboSection != null)
                {
                    var result = pRepo.Edit(id, provinceName, int.Parse(cboSection.SelectedValue));
                    if (!result)
                    {
                        ShowErrorMessage("Province name is unique, please choose another one.");
                        e.Canceled = true;
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
            var provinceName = values["ProvinceName"] as string;

            if (provinceName == null || string.IsNullOrEmpty(provinceName.Trim()))
            {
                ShowErrorMessage(Pharma.Provide_full_name_to_save__please);
                e.Canceled = true;
            }
            else
            {
                var cboSection = gdItem.FindControl("ddlSection") as RadComboBox;
                if (cboSection != null)
                {
                    var result = pRepo.Add(provinceName, int.Parse(cboSection.SelectedValue));
                    if (!result)
                    {
                        ShowErrorMessage("Province name is unique, please choose another one.");
                        e.Canceled = true;
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
            ddlSection.DataSource = sectionRepo.GetAll();
            ddlSection.DataTextField = "SectionName";
            ddlSection.DataValueField = "Id";
            ddlSection.DataBind();
            ddlSection.SelectedValue = sId;
        }
    }
}