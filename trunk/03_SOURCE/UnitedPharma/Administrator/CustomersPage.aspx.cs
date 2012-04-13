using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_CustomersPage : System.Web.UI.Page
{
    #region List Repository

    CustomersRepository CustomerRepo = new CustomersRepository();
    ValidationFields checkvalid = new ValidationFields();
    CustomersLogRepository Clog = new CustomersLogRepository();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mCustomer");
            string sql = "select c.*, t.TypeName as CustomerTypeName, ch.ChannelName as ChannelName,d.DistrictName as DistrictName, l.LocalName as LocalName,s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone ";
                sql+="from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
                sql+=" left join Channel ch on c.ChannelId=ch.Id left join District d on c.DistrictId=d.Id";
                sql+=" left join Local l on c.LocalId=l.Id left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";
                sql+="where c.Id in (select CustomerId from CustomerLog where IsApprove=0 group by CustomerId)";
            Utility U = new Utility();
            gridCustomerLog.DataSource = U.GetList(sql);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string upiCode = txtUpiCode.Text.Trim();
        string fullname = txtFullName.Text.Trim();

        if(!string.IsNullOrEmpty(upiCode) || !string.IsNullOrEmpty(fullname))
        {
            CustomerList.DataSource = CustomerRepo.FilterCustomers(upiCode, fullname);
            CustomerList.DataBind();
        }
    }

    protected void CustomerList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        CustomerList.DataSource = CustomerRepo.GetAllViewCustomers();
    }
    //gridCustomerLog

    protected void gridCustomerLog_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        string sql = "select c.*, t.TypeName as CustomerTypeName, ch.ChannelName as ChannelName,d.DistrictName as DistrictName, l.LocalName as LocalName,s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone ";
        sql += "from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
        sql += " left join Channel ch on c.ChannelId=ch.Id left join District d on c.DistrictId=d.Id";
        sql += " left join Local l on c.LocalId=l.Id left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";
        sql += "where c.Id in (select CustomerId from CustomerLog where IsApprove=0 group by CustomerId)";
        Utility U = new Utility();
        gridCustomerLog.DataSource = U.GetList(sql);
    }
    protected void gridCustomerLog_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            Control numericPagerControl = gridPager.GetNumericPager();

            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);
        }
    }
    protected void CustomerList_UpdateCommand(object source, GridCommandEventArgs e)
    {
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var CustomerId = (int)editableItem.GetDataKeyValue("Id");
        var _customer = CustomerRepo.GetCustomerById(CustomerId);
        string phoneNumber=((string)values["Phone"] ==null) ? "" : (string)values["Phone"];
        if (_customer != null)
        {
            try
            {
                if (checkvalid.phoneFormat(phoneNumber))
                {
                    RadComboBox ddlDistricts = gdItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
                    if (int.Parse(ddlDistricts.SelectedValue) > 0)
                    {
                        DateTime CreateDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtCreateDate")).SelectedDate.Value.Date);
                        DateTime UpdateDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtUpdateDate")).SelectedDate.Value.Date);
                        int customerType = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomerType")).SelectedValue);
                        int channel = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlChannels")).SelectedValue);
                        int district = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistricts")).SelectedValue);
                        int local = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlLocation")).SelectedValue);
                        string password = ((string)values["Password"] == null) ? "" : (string)values["Password"];

                        string noteOfSalesmen = ((TextBox)gdItem.FindControl("txtNoteOfSalesmen")).Text;

                        //CustomerRepo.UpdateCustomer(CustomerId, (string)values["UpiCode"], (string)values["FullName"], (string)values["Address"], (string)values["Street"],
                        //    (string)values["Ward"], (string)values["Phone"], (string)values["Password"], customerType, channel, district, local, CreateDate, UpdateDate, (bool)values["Status"]);
                        Clog.InsertCustomer((string)values["UpiCode"], (string)values["FullName"], (string)values["Address"], (string)values["Street"],
                            (string)values["Ward"], phoneNumber, password, customerType, channel, district, local, CreateDate, UpdateDate, 
                            (bool)values["Status"], CustomerId, false, 0, adm.Id, noteOfSalesmen);
                        CustomerRepo.SetEnableOfCustomer(CustomerId, false);
                        Response.Redirect("CustomersPage.aspx");
                    }
                }
                else
                    ShowErrorMessage("Phone number is not valid");
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void CustomerList_InsertCommand(object source, GridCommandEventArgs e)
    {
        ObjLogin adm = (ObjLogin)Session["objLogin"];
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            if (checkvalid.phoneFormat((string)values["Phone"]))
            {
                RadComboBox ddlDistricts = gdItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
                if (int.Parse(ddlDistricts.SelectedValue) > 0)
                {
                    DateTime CreateDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtCreateDate")).SelectedDate.Value.Date);
                    DateTime UpdateDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtUpdateDate")).SelectedDate.Value.Date);
                    int customertype = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomerType")).SelectedValue);
                    int channel = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlChannels")).SelectedValue);
                    int district = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistricts")).SelectedValue);
                    int location = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlLocation")).SelectedValue);
                    int local = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlLocation")).SelectedValue);
                    int CustomerId = CustomerRepo.InsertCustomer((string)values["UpiCode"], (string)values["FullName"], (string)values["Address"], (string)values["Street"], (string)values["Ward"],
                             (string)values["Phone"], (string)values["Password"], customertype, channel, district, location, CreateDate, UpdateDate, (bool)values["Status"],false);

                    Clog.InsertCustomer((string)values["UpiCode"], (string)values["FullName"], (string)values["Address"], (string)values["Street"],
                            (string)values["Ward"], (string)values["Phone"], (string)values["Password"], customertype, channel, district, local, 
                            CreateDate, UpdateDate, (bool)values["Status"], CustomerId, false, 0,adm.Id, string.Empty);
                    Response.Redirect("CustomersPage.aspx");
                }
                else
                {
                    ShowErrorMessage("Select a district");
                }
            }
            else
                ShowErrorMessage("Phone number is not valid");
        }
        catch (System.Exception)
        {
            ShowErrorMessage("Error");
        }
    }

    protected void CustomerList_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var CustomerId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            CustomerRepo.DeleteCustomerById(CustomerId);
            Response.Redirect("CustomersPage.aspx");
        }
        catch (System.Exception)
        {
            ShowErrorMessage("Error");
        }
    }

    protected void CustomerList_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
        {
            GridEditableItem edititem = (GridEditableItem)e.Item;
            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);

           vwCustomer vCustomer = (vwCustomer) e.Item.DataItem;

            using (UPIDataContext db = new UPIDataContext())
            {
                //CustomerType
                RadComboBox ddlCustomerType = ((RadComboBox)edititem.FindControl("ddlCustomerType"));
                ddlCustomerType.DataSource = db.CustomerTypes;
                ddlCustomerType.DataTextField = "TypeName";
                ddlCustomerType.DataValueField = "Id";
                ddlCustomerType.DataBind();
                ddlCustomerType.SelectedValue = (string)values["CustomerTypeId"];

                //Bind data Channel
                RadComboBox ddlChannels = ((RadComboBox)edititem.FindControl("ddlChannels"));
                ddlChannels.DataSource = db.Channels;
                ddlChannels.DataTextField = "ChannelName";
                ddlChannels.DataValueField = "Id";
                ddlChannels.DataBind();
                ddlChannels.SelectedValue = (string)values["ChannelId"];

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
                RadComboBox ddlDistricts = ((RadComboBox)edititem.FindControl("ddlDistricts"));
                ddlDistricts.DataSource = (from d in db.Districts where d.ProvinceId == int.Parse(ddlprovince.SelectedValue) select d);
                ddlDistricts.DataTextField = "DistrictName";
                ddlDistricts.DataValueField = "Id";
                ddlDistricts.DataBind();
                ddlDistricts.SelectedValue = districtIndex;

                string groupIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfGroup")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfGroup")).Value;
                string regionIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfRegion")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfRegion")).Value;
                string areaIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfArea")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfArea")).Value;
                string localIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfLocal")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfLocal")).Value;

                //Group
                RadComboBox ddlGroup = ((RadComboBox)edititem.FindControl("ddlGroup"));
                ddlGroup.DataSource = db.Groups;
                ddlGroup.DataTextField = "GroupName";
                ddlGroup.DataValueField = "Id";
                ddlGroup.DataBind();
                ddlGroup.SelectedValue = groupIndex;

                //Region
                RadComboBox ddlRegion = ((RadComboBox)edititem.FindControl("ddlRegion"));
                ddlRegion.DataSource = (from r in db.Regions where r.GroupId == Convert.ToInt32(ddlGroup.SelectedValue) select r);
                ddlRegion.DataTextField = "RegionName";
                ddlRegion.DataValueField = "Id";
                ddlRegion.DataBind();
                ddlRegion.SelectedValue = regionIndex;

                //Area
                RadComboBox ddlArea = ((RadComboBox)edititem.FindControl("ddlArea"));
                ddlArea.DataSource = (from a in db.Areas where a.RegionId == Convert.ToInt32(ddlRegion.SelectedValue) select a);
                ddlArea.DataTextField = "AreaName";
                ddlArea.DataValueField = "Id";
                ddlArea.DataBind();
                ddlArea.SelectedValue = areaIndex;

                //Location
                RadComboBox ddlLocation = ((RadComboBox)edititem.FindControl("ddlLocation"));
                ddlLocation.DataSource = (from l in db.Locals where l.AreaId == Convert.ToInt32(ddlArea.SelectedValue) select l);
                ddlLocation.DataTextField = "LocalName";
                ddlLocation.DataValueField = "Id";
                ddlLocation.DataBind();
                ddlLocation.SelectedValue = localIndex;

                string CreateDate = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfCreateDate")).Value) ? string.Empty : ((HiddenField)edititem.FindControl("hdfCreateDate")).Value;
                string UpdateDate = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfUpdateDate")).Value) ? string.Empty : ((HiddenField)edititem.FindControl("hdfUpdateDate")).Value;
                RadDatePicker txtCreateDate = ((RadDatePicker)edititem.FindControl("txtCreateDate"));
                txtCreateDate.DbSelectedDate = CreateDate;
                RadDatePicker txtUpdateDate = ((RadDatePicker)edititem.FindControl("txtUpdateDate"));
                txtUpdateDate.DbSelectedDate = UpdateDate;

                // Note of Salesmen
                string noteOfSalesmen = vCustomer.NoteOfSalesmen;
                var txtNoteOfSalesmen = ((TextBox)edititem.FindControl("txtNoteOfSalesmen"));
                txtNoteOfSalesmen.Text = noteOfSalesmen;
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

            RadComboBox ddlDistricts = editedItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
            ddlDistricts.DataSource = (from d in db.Districts where d.ProvinceId == (Convert.ToInt32(ddlprovince.SelectedValue)) select d);
            ddlDistricts.DataTextField = "DistrictName";
            ddlDistricts.DataValueField = "Id";
            ddlDistricts.DataBind();
        }
    }

    protected void ddlprovince_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        using (UPIDataContext db = new UPIDataContext())
        {
            GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
            RadComboBox ddlDistricts = editedItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
            ddlDistricts.DataSource = (from d in db.Districts where d.ProvinceId == (Convert.ToInt32(e.Value)) select d);
            ddlDistricts.DataTextField = "DistrictName";
            ddlDistricts.DataValueField = "Id";
            ddlDistricts.DataBind();
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
        using (UPIDataContext db = new UPIDataContext())
        {
            RadComboBox ddlRegion = editedItem["LocationColumn"].FindControl("ddlRegion") as RadComboBox;
            ddlRegion.DataSource = (from r in db.Regions where r.GroupId == Convert.ToInt32(e.Value) select r);
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataValueField = "Id";
            ddlRegion.DataBind();

            RadComboBox ddlArea = editedItem["LocationColumn"].FindControl("ddlArea") as RadComboBox;
            ddlArea.DataSource = (from a in db.Areas where a.RegionId == Convert.ToInt32(ddlRegion.SelectedValue) select a);
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "Id";
            ddlArea.DataBind();

            RadComboBox ddlLocation = editedItem["LocationColumn"].FindControl("ddlLocation") as RadComboBox;
            ddlLocation.DataSource = (from l in db.Locals where l.AreaId == Convert.ToInt32(ddlArea.SelectedValue) select l);
            ddlLocation.DataTextField = "LocalName";
            ddlLocation.DataValueField = "Id";
            ddlLocation.DataBind();
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
        using (UPIDataContext db = new UPIDataContext())
        {
            RadComboBox ddlArea = editedItem["LocationColumn"].FindControl("ddlArea") as RadComboBox;
            ddlArea.DataSource = (from a in db.Areas where a.RegionId == Convert.ToInt32(e.Value) select a);
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "Id";
            ddlArea.DataBind();

            RadComboBox ddlLocation = editedItem["LocationColumn"].FindControl("ddlLocation") as RadComboBox;
            ddlLocation.DataSource = (from l in db.Locals where l.AreaId == Convert.ToInt32(ddlArea.SelectedValue) select l);
            ddlLocation.DataTextField = "LocalName";
            ddlLocation.DataValueField = "Id";
            ddlLocation.DataBind();
        }
    }

    protected void ddlArea_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        using (UPIDataContext db = new UPIDataContext())
        {
            GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
            RadComboBox ddlLocation = editedItem["LocationColumn"].FindControl("ddlLocation") as RadComboBox;
            ddlLocation.DataSource = (from l in db.Locals where l.AreaId == Convert.ToInt32(e.Value) select l);
            ddlLocation.DataTextField = "LocalName";
            ddlLocation.DataValueField = "Id";
            ddlLocation.DataBind();
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomersPage.aspx");
    }
}