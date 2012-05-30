using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.App_Code;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_CustomersPage : System.Web.UI.Page
{
    #region List Repository

    CustomersRepository CustomerRepo = new CustomersRepository();
    ValidationFields checkvalid = new ValidationFields();
    CustomersLogRepository Clog = new CustomersLogRepository();
    private SalesmanRepository sRepo = new SalesmanRepository();
    private const string DataTextFieldName = "Fullname";
    private const string DataValueFieldName = "Id";

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mCustomer");

            LoadCustomerLog(-1);

            // Load TROM to combo
            LoadSalesmenToComboTROM();

            // Load EROM to combo
            LoadSalesmenToComboErom();

            // Load EROM2 to combo
            LoadSalesmenToComboErom2();

            Session["IsFilterClickedAdminCustomer"] = "0";
        }
    }

    private DataTable GetCustomerLog(int supervisorId)
    {
        var sql =
            "select c.*, t.TypeName as CustomerTypeName, ch.ChannelName as ChannelName,d.DistrictName as DistrictName, " +
            "l.LocalName as LocalName,s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone, cusLog.IsApprove ";
        sql += "from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
        sql += " left join Channel ch on c.ChannelId=ch.Id left join District d on c.DistrictId=d.Id";
        sql +=
            " left join Local l on c.LocalId=l.Id left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";

        sql += supervisorId != -1
                   ? string.Format("join CustomerLog cusLog on cusLog.CustomerId=c.Id where c.Id='{0}'", supervisorId)
                   : string.Format("join CustomerLog cusLog on cusLog.CustomerId=c.Id");

        sql += " order by c.CreateDate desc";

        Utility U = new Utility();
        return U.GetList(sql);
    }

    private void LoadCustomerLog(int supervisorId)
    {
        gridCustomerLog.DataSource = GetCustomerLog(supervisorId);
    }

    private void LoadSalesmenToComboTROM()
    {
        LoadSalesmenToCombo(SalesmenRole.TROM, cboTROM, "Select a TROM");
    }

    private void LoadSalesmenToComboErom()
    {
        LoadSalesmenToCombo(SalesmenRole.EROM, cboEROM, "Select a EROM");
    }

    private void LoadSalesmenToComboErom2()
    {
        LoadSalesmenToCombo(SalesmenRole.EROM2, cboEROM2, "Select a EROM2");
    }

    private void LoadSalesmenToCombo(SalesmenRole role, RadComboBox cbo, string firstItemText)
    {
        var salesmens = sRepo.GetSalesmenByRoleId((int)role);
        if (salesmens == null)
        {
            cbo.Enabled = false;
            cbo.Enabled = false;
            // write log here
        }
        else
        {
            LoadListSalesmenToCombo(salesmens, cbo, firstItemText);
        }
    }

    private void LoadListSalesmenToCombo(List<Salesmen> trom, RadComboBox comboBox, string firstItemText)
    {
        if (trom != null)
        {
            comboBox.DataSource = trom;
            comboBox.DataTextField = DataTextFieldName;
            comboBox.DataValueField = DataValueFieldName;
            comboBox.DataBind();

            if (trom.Count > 0)
            {
                var item = new RadComboBoxItem(firstItemText, "0");
                comboBox.Items.Insert(0, item);
            }
        }

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        string upiCode = txtUpiCode.Text.Trim();
        string fullname = txtFullName.Text.Trim();

        CustomerList.DataSource = CustomerRepo.FilterCustomers(upiCode, fullname);
        CustomerList.DataBind();

        Session["IsFilterClickedAdminCustomer"] = "1";
    }

    protected void CustomerList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        var upiCode = txtUpiCode.Text.Trim();
        var fullname = txtFullName.Text.Trim();

        var isFilterClicked = Session["IsFilterClickedAdminCustomer"] as string;

        if (isFilterClicked != null && isFilterClicked == "1")
        {
            CustomerList.DataSource = CustomerRepo.FilterCustomers(upiCode, fullname);
        }
        else
        {
            var supervisorId = GetSupervisorOfPOC_POS();

            CustomerList.DataSource = supervisorId ==-1 ? CustomerRepo.GetAllViewCustomers() : CustomerRepo.GetAllViewCustomersBySupervisor(supervisorId);
        }
    }

    protected void gridCustomerLog_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        string upiCode = txtUpiCode.Text.Trim();
        string fullname = txtFullName.Text.Trim();

        if (!string.IsNullOrEmpty(upiCode) || !string.IsNullOrEmpty(fullname))
        {
            gridCustomerLog.DataSource = Clog.FilterCustomers(upiCode, fullname);
        }
        else
        {
            var supervisorId = GetSupervisorOfPOC_POS();
            gridCustomerLog.DataSource = GetCustomerLog(supervisorId);
        }
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
        
        var customerId = (int)editableItem.GetDataKeyValue("Id");
        var customer = CustomerRepo.GetCustomerById(customerId);
        
        if (customer != null)
        {
            try
            {
                var upiCode = values["UpiCode"] as string;
                var fullName = values["FullName"] as string;
                var phoneNumber = values["Phone"] as string;

                if (string.IsNullOrEmpty(upiCode) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber))
                {
                    ShowErrorMessage("Please provide UPI Code, Full Name and Phone number");
                    e.Canceled = true;
                    return;
                }

                if (checkvalid.phoneFormat(phoneNumber))
                {

                    var result = CustomerRepo.CheckExistedCustomer(customerId, phoneNumber);
                    if(result)
                    {
                        ShowErrorMessage("Phone number is unique, please choose another one.");
                        e.Canceled = true;
                        return;
                    }

                    RadComboBox ddlDistricts = gdItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
                    if (int.Parse(ddlDistricts.SelectedValue) > 0)
                    {
                        var createDate = DateTime.Now;
                        var updateDate = DateTime.Now;

                        var customerType = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomerType")).SelectedValue);
                        var channel = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlChannels")).SelectedValue);
                        var district = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistricts")).SelectedValue);
                        var local = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlLocation")).SelectedValue);
                        var password = (string)values["Password"] ?? "";

                        var noteOfSalesmen = ((TextBox)gdItem.FindControl("txtNoteOfSalesmen")).Text;

                        Clog.InsertCustomer(upiCode.Trim(), fullName.Trim(), (string)values["Address"], (string)values["Street"],
                            (string)values["Ward"], phoneNumber, password, customerType, channel, district, local, createDate, updateDate,
                            (bool)values["Status"], customerId, false, 0, adm.Id, noteOfSalesmen);
                        CustomerRepo.SetEnableOfCustomer(customerId, false);
                        Response.Redirect("CustomersPage.aspx");
                    }
                }
                else
                {
                    ShowErrorMessage("Phone number is not valid");
                    e.Canceled = true;
                }
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message);
                e.Canceled = true;
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void CustomerList_InsertCommand(object source, GridCommandEventArgs e)
    {
        var adm = (ObjLogin)Session["objLogin"];
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            var upiCode = values["UpiCode"] as string;
            var fullName = values["FullName"] as string;
            var phoneNumber = values["Phone"] as string;

            if(string.IsNullOrEmpty(upiCode) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber))
            {
                ShowErrorMessage("Please provide UPI Code, Full Name and Phone number");
                e.Canceled = true;
                return;
            }

            if (phoneNumber != null && checkvalid.phoneFormat(phoneNumber))
            {
                RadComboBox ddlDistricts = gdItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
                if (int.Parse(ddlDistricts.SelectedValue) > 0)
                {
                    var createDate = DateTime.Now;
                    var updateDate = DateTime.Now;

                    int customertype = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomerType")).SelectedValue);
                    int channel = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlChannels")).SelectedValue);
                    int district = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistricts")).SelectedValue);
                    int location = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlLocation")).SelectedValue);
                    int local = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlLocation")).SelectedValue);

                    var noteOfSalesmen = string.Empty;
                    var txtNoteOfsales = gdItem.FindControl("txtNoteOfSalesmen") as TextBox;
                    if(txtNoteOfsales != null)
                    {
                        noteOfSalesmen = txtNoteOfsales.Text.Trim();
                    }

                    //var supervisorName = values["SupervisorName"] as string;

                    int CustomerId = CustomerRepo.InsertCustomer(upiCode.Trim(), fullName.Trim(), (string)values["Address"], 
                        (string)values["Street"], (string)values["Ward"],
                             phoneNumber, (string)values["Password"], customertype, channel, district, location, createDate, updateDate, (bool)values["Status"], false);

                    if (CustomerId > 0)
                    {
                        Clog.InsertCustomer(upiCode.Trim(), fullName.Trim(), (string)values["Address"], (string)values["Street"],
                               (string)values["Ward"], phoneNumber, (string)values["Password"], customertype, channel, district, local,
                               createDate, updateDate, (bool)values["Status"], CustomerId, false, 0, adm.Id, noteOfSalesmen);
                        Response.Redirect("CustomersPage.aspx");
                    }
                    else
                    {
                        ShowErrorMessage("UPI Code and phone number are unique, please choose another one.");
                        e.Canceled = true;
                    }
                }
                else
                {
                    ShowErrorMessage("Select a district");
                    e.Canceled = true;
                }
            }
            else
            {
                ShowErrorMessage("Phone number is not valid");
                e.Canceled = true;
            }
        }
        catch (System.Exception)
        {
            ShowErrorMessage("Error");
            e.Canceled = true;
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

                // Note of Salesmen
                vwCustomer vCustomer = e.Item.DataItem as vwCustomer;
                if(vCustomer != null)
                {
                    string noteOfSalesmen = vCustomer.NoteOfSalesmen;
                    var txtNoteOfSalesmen = ((TextBox)edititem.FindControl("txtNoteOfSalesmen"));
                    if (txtNoteOfSalesmen != null) txtNoteOfSalesmen.Text = noteOfSalesmen;
                }
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

    protected void gridCustomerLog_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = (GridDataItem) e.Item;
            if (dataItem["IsApprove"].Text.ToLower() == "true")
            {
                dataItem.BackColor = Color.FromArgb(255, 0, 155, 255);
            }
        }
    }

    protected void cboTROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var trom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (trom != null)
            {
                // Load data to TPS
                var tps = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, trom.Id);
                LoadListSalesmenToCombo(tps, cboTPS, "Select a TPS");

                UtilitiesHelpers.Instance.ClearComboData(cboTPR);
            }
        }
        else
        {
            UtilitiesHelpers.Instance.ClearComboData(cboTPS);
            UtilitiesHelpers.Instance.ClearComboData(cboTPR);
        }

        ResetChannelCombo(SalesChannel.Trom);
    }

    protected void cboTPS_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var tps = sRepo.GetSalemenById(int.Parse(e.Value));

            if (tps != null)
            {
                // Load data to TPS
                var tpr = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPR, tps.Id);

                if (tpr != null)
                {
                    cboTPR.DataSource = tpr;
                    cboTPR.DataTextField = DataTextFieldName;
                    cboTPR.DataValueField = DataValueFieldName;
                    cboTPR.DataBind();

                    LoadListSalesmenToCombo(tpr, cboTPR, "Select a TPR");
                }
            }
        }
        else
        {
            cboTPR.DataSource = null;
            cboTPR.DataBind();
        }
    }

    protected void cboTPR_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        var supervisorId = Parsers.ToInt(e.Value);

        FilterCustomerAndCustomerLog(supervisorId);
    }

    private void FilterCustomerAndCustomerLog(int supervisorId)
    {
        CustomerList.DataSource = (supervisorId == -1 || supervisorId == 0)
                                      ? CustomerRepo.GetAllViewCustomers()
                                      : CustomerRepo.GetAllViewCustomersBySupervisor(supervisorId);
        CustomerList.DataBind();

        gridCustomerLog.DataSource = GetCustomerLog(supervisorId);
        gridCustomerLog.DataBind();
    }

    protected void cboEROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom != null)
            {
                // Load data to PSS1
                var pss1 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS1, erom.Id);
                LoadListSalesmenToCombo(pss1, cboPSS1, "Select a PSS1");

                UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
            }
        }
        else
        {
            UtilitiesHelpers.Instance.ClearComboData(cboPSS1);
            UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
        }

        ResetChannelCombo(SalesChannel.Erom);
    }

    protected void cboPSS1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var pss1 = sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss1 != null)
            {
                // Load data to PSR 1
                var psr1 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR1, pss1.Id);

                if (psr1 != null)
                {
                    LoadListSalesmenToCombo(psr1, cboPSR1, "Select a PSR1");
                }
            }
        }
        else
        {
            cboPSR1.DataSource = null;
            cboPSR1.DataBind();
        }
    }

    protected void cboPSR1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        var supervisorId = Parsers.ToInt(e.Value);

        FilterCustomerAndCustomerLog(supervisorId);
    }

    protected void cboEROM2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom2 = sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom2 != null)
            {
                // Load data to PSS2
                var pss2 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS2, erom2.Id);
                LoadListSalesmenToCombo(pss2, cboPSS2, "Select a PSS2");

                UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
            }
        }
        else
        {
            UtilitiesHelpers.Instance.ClearComboData(cboPSS2);
            UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
        }

        ResetChannelCombo(SalesChannel.Erom2);
    }

    protected void cboPSS2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var pss2 = sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss2 != null)
            {
                // Load data to PSR 2
                var psr2 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR2, pss2.Id);

                if (psr2 != null)
                {
                    LoadListSalesmenToCombo(psr2, cboPSR2, "Select a PSR2");
                }

            }
        }
        else
        {
            cboPSR2.DataSource = null;
            cboPSR2.DataBind();
        }
    }

    protected void cboPSR2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        var supervisorId = Parsers.ToInt(e.Value);

        FilterCustomerAndCustomerLog(supervisorId);
    }

    private void ResetChannelCombo(SalesChannel channel)
    {
        switch (channel)
        {
            case SalesChannel.Erom:
                UtilitiesHelpers.Instance.ClearComboData(cboTPS);
                UtilitiesHelpers.Instance.ClearComboData(cboTPR);
                cboTROM.SelectedIndex = 0;

                UtilitiesHelpers.Instance.ClearComboData(cboPSS2);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
                cboEROM2.SelectedIndex = 0;
                break;
            case SalesChannel.Erom2:
                UtilitiesHelpers.Instance.ClearComboData(cboTPS);
                UtilitiesHelpers.Instance.ClearComboData(cboTPR);
                cboTROM.SelectedIndex = 0;

                UtilitiesHelpers.Instance.ClearComboData(cboPSS1);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
                cboEROM.SelectedIndex = 0;
                break;
            default:
                UtilitiesHelpers.Instance.ClearComboData(cboPSS1);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
                cboEROM.SelectedIndex = 0;

                UtilitiesHelpers.Instance.ClearComboData(cboPSS2);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
                cboEROM2.SelectedIndex = 0;
                break;
        }
    }

    private int GetSupervisorOfPOC_POS()
    {
        // POS
        var tprId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboTPR);
        if (tprId > 0)
        {
            return tprId;
        }
        
        // POC
        var psr1Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSR1);
        if (psr1Id > 0)
        {
            return psr1Id;
        }

        var psr2Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSR2);
        if (psr2Id > 0)
        {
            return psr2Id;
        }
        return -1;
    }
}