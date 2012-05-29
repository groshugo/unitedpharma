using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Resources;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_CustomerSupervisorManagement : System.Web.UI.Page
{
    CustomerSuperviorRepository superviorRepo = new CustomerSuperviorRepository();
    ValidationFields checkvalid = new ValidationFields();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = superviorRepo.GetAllViewSupervior();
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var SuperviorId = (int)editableItem.GetDataKeyValue("Id");
        var Supervior = superviorRepo.GetCustomerSuperviorById(SuperviorId);
        if (Supervior != null)
        {
            try
            {
                var phone = values["Phone"] as string;
                var fullName = values["FullName"] as string;

                if (phone == null || string.IsNullOrEmpty(phone.Trim()) || fullName == null || string.IsNullOrEmpty(fullName.Trim()))
                {
                    ShowErrorMessage("Full name and phone number are required fields");
                    e.Canceled = true;
                }
                else
                {
                    if (checkvalid.phoneFormat(phone))
                    {
                        var result = superviorRepo.UpdateCustomerSupervisor(SuperviorId, fullName, (string)values["Address"], (string)values["Street"],
                            (string)values["Ward"], phone, Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomer")).SelectedValue),
                            Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistrict")).SelectedValue),
                            Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlPosition")).SelectedValue));
                        if (!result)
                        {
                            ShowErrorMessage("Phone number is unique, please choose another one.");
                            e.Canceled = true;
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Phone number is not valid.");
                        e.Canceled = true;
                    }
                }

            }
            catch (System.Exception)
            {
                ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_UpdateCommand_can_not_update__please_try_again_later_or_contact_admnistrator_);
                e.Canceled = true;
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        
        try
        {
            var phone = values["Phone"] as string;
            var fullName = values["FullName"] as string;

            if (phone == null || string.IsNullOrEmpty(phone.Trim()) || fullName == null || string.IsNullOrEmpty(fullName.Trim()))
            {
                ShowErrorMessage("Full name and phone number are required fields");
                e.Canceled = true;
            }
            else
            {
                if (checkvalid.phoneFormat(phone))
                {
                    var result = superviorRepo.InsertCustomerSupervisor((string)values["FullName"], (string)values["Address"], (string)values["Street"], 
                        (string)values["Ward"], phone, Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomer")).SelectedValue), 
                        Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistrict")).SelectedValue), 
                        Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlPosition")).SelectedValue));
                    if (!result)
                    {
                        ShowErrorMessage("Phone number is unique, please choose another one.");
                        e.Canceled = true;
                    }
                }
                else
                {
                    ShowErrorMessage("Phone number is not valid.");
                    e.Canceled = true;
                }
            }

        }
        catch (System.Exception)
        {
            ShowErrorMessage(Pharma.Administrator_Default_RadGrid1_InsertCommand_can_not_add__please_try_again_later_or_contact_admnistrator_);
            e.Canceled = true;
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var SuperviorId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        var Supervior = superviorRepo.GetCustomerSuperviorById(SuperviorId);
        if (Supervior != null)
        {
            try
            {
                superviorRepo.DeleteCustomerSupervisorById(SuperviorId);
            }
            catch (System.Exception)
            {
                ShowErrorMessage("Error");
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

            using (UPIDataContext db = new UPIDataContext())
            {
                //Position
                RadComboBox ddlPosition = ((RadComboBox)edititem.FindControl("ddlPosition"));
                ddlPosition.DataSource = db.SupervisorPositions;
                ddlPosition.DataTextField = "PositionName";
                ddlPosition.DataValueField = "Id";
                ddlPosition.DataBind();
                ddlPosition.SelectedValue = (string)values["PositionId"];

                string sectionIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfSection")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfSection")).Value;
                string provinceIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfProvince")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfProvince")).Value;
                string districtIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfDistrict")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfDistrict")).Value;

                //Section
                RadComboBox ddlSection = ((RadComboBox)edititem.FindControl("ddlSection"));
                ddlSection.DataSource = db.Sections;
                ddlSection.DataTextField = "SectionName";
                ddlSection.DataValueField = "Id";
                ddlSection.DataBind();
                ddlSection.SelectedValue = sectionIndex;

                // Province
                RadComboBox ddlprovince = ((RadComboBox)edititem.FindControl("ddlprovince"));
                ddlprovince.DataSource = (from p in db.Provinces where p.SectionId == int.Parse(ddlSection.SelectedValue) select p);
                ddlprovince.DataTextField = "ProvinceName";
                ddlprovince.DataValueField = "Id";
                ddlprovince.DataBind();
                ddlprovince.SelectedValue = provinceIndex;

                //District                
                RadComboBox ddlDistrict = ((RadComboBox)edititem.FindControl("ddlDistrict"));
                ddlDistrict.DataSource = (from d in db.Districts where d.ProvinceId == int.Parse(ddlprovince.SelectedValue) select d);
                ddlDistrict.DataTextField = "DistrictName";
                ddlDistrict.DataValueField = "Id";
                ddlDistrict.DataBind();
                ddlDistrict.SelectedValue = districtIndex;

                string custTypeIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfCustomerType")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfCustomerType")).Value;
                string custIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfCustomer")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfCustomer")).Value;

                //CustomerType                
                RadComboBox ddlCustomerType = ((RadComboBox)edititem.FindControl("ddlCustomerType"));
                ddlCustomerType.DataSource = db.CustomerTypes;
                ddlCustomerType.DataTextField = "TypeName";
                ddlCustomerType.DataValueField = "Id";
                ddlCustomerType.DataBind();
                ddlCustomerType.SelectedValue = custTypeIndex;

                //Customer
                RadComboBox ddlCustomers = ((RadComboBox)edititem.FindControl("ddlCustomer"));
                ddlCustomers.DataSource = (from c in db.Customers where c.CustomerTypeId == (Convert.ToInt32(ddlCustomerType.SelectedValue)) select c);
                ddlCustomers.DataTextField = "FullName";
                ddlCustomers.DataValueField = "Id";
                ddlCustomers.DataBind();
                ddlCustomers.SelectedValue = custIndex;
            }            
        }
    }

    protected void ddlSection_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        using (UPIDataContext db = new UPIDataContext())
        {
            GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
            RadComboBox ddlprovince = editedItem["DistrictColumn"].FindControl("ddlprovince") as RadComboBox;
            ddlprovince.DataSource = (from p in db.Provinces where p.SectionId == (Convert.ToInt32(e.Value)) select p);
            ddlprovince.DataTextField = "ProvinceName";
            ddlprovince.DataValueField = "Id";
            ddlprovince.DataBind();

            RadComboBox ddlDistrict = editedItem["DistrictColumn"].FindControl("ddlDistrict") as RadComboBox;
            ddlDistrict.DataSource = (from d in db.Districts where d.ProvinceId == (Convert.ToInt32(ddlprovince.SelectedValue)) select d);
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "Id";
            ddlDistrict.DataBind();
        }
    }

    protected void ddlprovince_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        using (UPIDataContext db = new UPIDataContext())
        {
            GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
            RadComboBox ddlDistrict = editedItem["DistrictColumn"].FindControl("ddlDistrict") as RadComboBox;
            ddlDistrict.DataSource = (from d in db.Districts where d.ProvinceId == (Convert.ToInt32(e.Value)) select d);
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "Id";
            ddlDistrict.DataBind();
        }
    }

    protected void ddlCustomerType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        using (UPIDataContext db = new UPIDataContext())
        {
            GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
            RadComboBox ddlCustomers = editedItem["CustomerColumn"].FindControl("ddlCustomer") as RadComboBox;
            ddlCustomers.DataSource = (from d in db.Customers where d.CustomerTypeId == (Convert.ToInt32(e.Value)) select d);
            ddlCustomers.DataTextField = "FullName";
            ddlCustomers.DataValueField = "Id";
            ddlCustomers.DataBind();
        }
    }
}