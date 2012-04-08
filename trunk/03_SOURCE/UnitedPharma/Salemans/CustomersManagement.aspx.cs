using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;
using System.Data;

public partial class Salemans_CustomersManagement : System.Web.UI.Page
{
    SalesmanRepository repo = new SalesmanRepository();
    ValidationFields checkvalid = new ValidationFields();
    CustomersLogRepository CLRepo = new CustomersLogRepository();
    CustomersRepository CRepo = new CustomersRepository();
    GroupsRepository GRepo = new GroupsRepository();
    RegionsRepository RRepo = new RegionsRepository();
    AreasRepository ARepo = new AreasRepository();
    LocalsRepository LRepo = new LocalsRepository();
    Utility U = new Utility();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            if (!Page.IsPostBack)
            {
                Utility.SetCurrentMenu("mCustomer");
                GroupList();
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    private void GroupList()
    {
        var g = GRepo.GetAll();
        ddlGroup.DataSource = g;
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "Id";
        ddlGroup.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select", "0");
        ddlGroup.Items.Insert(0, item);
    }
    private void RegionList(int GroupId)
    {
        var r = RRepo.GetRegionByGroupId(GroupId);
        ddlRegion.DataSource = r;
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "Id";
        ddlRegion.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select", "0");
        ddlRegion.Items.Insert(0, item);
    }
    private void AreaList(int RegionId)
    {
        var a = ARepo.GetAreaByRegionId(RegionId);
        ddlArea.DataSource = a;
        ddlArea.DataTextField = "AreaName";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select", "0");
        ddlArea.Items.Insert(0, item);
    }
    private void LocalList(int AreaId)
    {
        var l = LRepo.GetLocalByAreaId(AreaId);
        ddlLocal.DataSource = l;
        ddlLocal.DataTextField = "LocalName";
        ddlLocal.DataValueField = "Id";
        ddlLocal.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select", "0");
        ddlLocal.Items.Insert(0, item);
    }
    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        RadGrid1.DataSource = LoadEditedCustomer(sale.Id);
        //RadGrid1.DataSource = repo.GetCustomerContact(sale.Id).Distinct();
    }

    protected void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        RadGrid2.DataSource = LoadSaleManager(sale.Id, 1, "", "", 0, string.Empty);
    }
    protected void RadGrid2_UpdateCommand(object source, GridCommandEventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var CustomerId = (int)editableItem.GetDataKeyValue("Id");
        var _customer = CRepo.GetCustomerById(CustomerId);
        if (_customer != null)
        {
            try
            {
                RadComboBox ddlDistricts = gdItem["DistrictColumn"].FindControl("ddlDistricts") as RadComboBox;
                if (int.Parse(ddlDistricts.SelectedValue) > 0)
                {
                    int district = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlDistricts")).SelectedValue);
                    TextBox txtFullName = (TextBox)gdItem["FullNameColumn"].FindControl("txtFullName") as TextBox;

                    int customerTypeId = int.Parse(((RadComboBox)gdItem.FindControl("dropdownCustomerType")).SelectedValue);
                    bool customerStatus = ((CheckBox)gdItem.FindControl("chkStatusEdit")).Checked;

                    string noteOfSalesmen = ((TextBox)gdItem.FindControl("txtNoteOfSalesmen")).Text;

                    CLRepo.InsertCustomer(_customer.UpiCode, txtFullName.Text, (string)values["Address"], (string)values["Street"],
                        (string)values["Ward"], _customer.Phone, _customer.Password, customerTypeId, int.Parse(_customer.ChannelId.ToString()),
                        district, int.Parse(_customer.LocalId.ToString()), Convert.ToDateTime(_customer.CreateDate), Convert.ToDateTime(_customer.UpdateDate),
                        customerStatus, CustomerId, false, 0, sale.Id, noteOfSalesmen);
                    CRepo.SetEnableOfCustomer(CustomerId, false);
                    Response.Redirect("CustomersManagement.aspx");
                }
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }
    }

    private DataTable LoadSaleManager(int salemenId, int IsEnable, string FullName, string PhoneNumber, int LocalId, string upiCode)
    {
        AreasRepository Arepo = new AreasRepository();
        RegionsRepository RRepo = new RegionsRepository();

        string strGroupIdList = GetGroupBySalemenId(salemenId);
        string strRegionIdList = SalesRegionList(strGroupIdList, salemenId);
        string strAreaIdList = SalesAreaList(strRegionIdList, salemenId);
        string strLocalIdList = SalesLocalList(strAreaIdList, salemenId);
        string sql = "select c.*, t.TypeName as CustomerTypeName, ch.ChannelName as ChannelName,d.DistrictName as DistrictName, l.LocalName as LocalName,s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,dis.ProvinceId,pro.SectionId ";
        sql += "from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
        sql += " left join Channel ch on c.ChannelId=ch.Id left join District d on c.DistrictId=d.Id";
        sql += " left join Local l on c.LocalId=l.Id left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";
        sql += " left join District dis on c.DistrictId = dis.Id";
        sql += " left join Province pro on dis.ProvinceId = pro.Id";
        //sql += " left join Section sec on pro.SectionId = sec.Id";
        sql += " where c.IsEnable= " + IsEnable;
        if (FullName != "")
            sql += " and c.FullName like '%" + FullName + "%'";
        if (PhoneNumber != "")
            sql += " and c.Phone ='" + PhoneNumber + "'";
        if (LocalId > 0)
            sql += " and c.LocalId=" + LocalId;
        else
        {
            sql += " and c.Localid in (" + strLocalIdList + ")";
        }

        if (!string.IsNullOrEmpty(upiCode))
        {
            sql += string.Format(" and c.UpiCode like '%{0}%'", upiCode);
        }

        return U.GetList(sql);
    }
    private DataTable LoadEditedCustomer(int salemenId)
    {
        AreasRepository Arepo = new AreasRepository();
        RegionsRepository RRepo = new RegionsRepository();

        string strGroupIdList = GetGroupBySalemenId(salemenId);
        string strRegionIdList = SalesRegionList(strGroupIdList, salemenId);
        string strAreaIdList = SalesAreaList(strRegionIdList, salemenId);
        string strLocalIdList = SalesLocalList(strAreaIdList, salemenId);
        string sql = "select c.*, t.TypeName as CustomerTypeName, ch.ChannelName as ChannelName,d.DistrictName as DistrictName, l.LocalName as LocalName,s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone ";
        sql += "from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
        sql += " left join Channel ch on c.ChannelId=ch.Id left join District d on c.DistrictId=d.Id";
        sql += " left join Local l on c.LocalId=l.Id left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";
        sql += "where c.Id in (select CustomerId from CustomerLog where IsApprove=0 and changeBy=" + salemenId + " group by CustomerId)";
        return U.GetList(sql);
    }
    private string GetGroupBySalemenId(int salemenId)
    {
        string SqlGroup = "select GroupId from salesgroup where SalesmenId = " + salemenId + " group by GroupId";
        dt = U.GetList(SqlGroup);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }
    private string GetRegionBySalemenId(int salemenId)
    {
        string SqlRegion = "select RegionId from SalesRegion where salesmenId = " + salemenId + " group by RegionId";
        dt = U.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }
    private string GetAreaBySalemenId(int salemenId)
    {
        string SqlRegion = "select AreaId from SalesArea where salesmenId = " + salemenId + " group by AreaId";
        dt = U.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }
    private string GetLocalBySalemenId(int salemenId)
    {
        string SqlRegion = "select LocalId from SalesLocal where salesmenId = " + salemenId + " group by LocalId";
        dt = U.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }

    private string SalesRegionList(string strGroupIdList, int salemenId)
    {
        string SqlRegion = "select Id from Region where GroupId in (" + strGroupIdList + ") group by Id";
        dt = U.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        string sqlRegionId = GetRegionBySalemenId(salemenId);
        if (sqlRegionId != "")
            result = result + "," + sqlRegionId;
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }
    private string SalesAreaList(string strRegionIdList, int salemenId)
    {
        string SqlRegion = "select Id from Area where RegionId in (" + strRegionIdList + ") group by Id";
        dt = U.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        string sqlRegionId = GetAreaBySalemenId(salemenId);
        if (sqlRegionId != "")
            result = result + "," + sqlRegionId;
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }
    private string SalesLocalList(string strAreaIdList, int salemenId)
    {
        string SqlRegion = "select Id from Local where AreaId in (" + strAreaIdList + ") group by Id";
        dt = U.GetList(SqlRegion);
        string result = string.Empty;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            result += dt.Rows[i][0] + ",";
        }
        string sqlRegionId = GetLocalBySalemenId(salemenId);
        if (sqlRegionId != "")
            result = result + "," + sqlRegionId;
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
        {
            GridEditableItem edititem = (GridEditableItem)e.Item;
            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);

            using (UPIDataContext db = new UPIDataContext())
            {
                var rowData = (DataRowView) e.Item.DataItem;

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

                // Customer Type
                RadComboBox comboCustomerType = ((RadComboBox)edititem.FindControl("dropdownCustomerType"));
                comboCustomerType.DataSource = (from ct in db.CustomerTypes select ct);
                comboCustomerType.DataTextField = "TypeName";
                comboCustomerType.DataValueField = "Id";
                comboCustomerType.DataBind();

                HiddenField hdfCt = ((HiddenField)edititem.FindControl("hdfCustomerTypeId"));
                if (hdfCt != null) comboCustomerType.SelectedValue = hdfCt.Value;

                // Note of Salesmen
                string noteOfSalesmen = rowData["NoteOfSalesmen"].ToString();
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
            var a = (from d in db.Districts where d.ProvinceId == (Convert.ToInt32(e.Value)) select d);
            ddlDistricts.DataSource = (from d in db.Districts where d.ProvinceId == (Convert.ToInt32(e.Value)) select d);
            ddlDistricts.DataTextField = "DistrictName";
            ddlDistricts.DataValueField = "Id";
            ddlDistricts.DataBind();
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (int.Parse(ddlGroup.SelectedValue.ToString()) > 0)
        {
            ObjLogin sale = (ObjLogin)Session["objLogin"];
            ddlRegion.Enabled = true;
            RegionList(int.Parse(ddlGroup.SelectedValue.ToString()));
        }
        else
        {
            ddlRegion.Enabled = false;
        }
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (int.Parse(ddlRegion.SelectedValue.ToString()) > 0)
        {
            AreaList(int.Parse(ddlRegion.SelectedValue.ToString()));
            ddlArea.Enabled = true;
        }
        else
            ddlArea.Enabled = false;
    }
    protected void ddlArea_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (int.Parse(ddlArea.SelectedValue.ToString()) > 0)
        {
            LocalList(int.Parse(ddlArea.SelectedValue.ToString()));
            ddlLocal.Enabled = true;
        }
        else
            ddlLocal.Enabled = false;
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        ObjLogin sale = (ObjLogin)Session["objLogin"];
        string phone = txtPhoneNumber.Text.Trim();
        if (phone != "")
        {
            if (checkvalid.phoneFormat(txtPhoneNumber.Text.Trim()))
                phone = txtPhoneNumber.Text.Trim();
        }
        int LocalId = (ddlLocal.Enabled) ? int.Parse(ddlLocal.SelectedValue) : 0;

        RadGrid2.DataSource = LoadSaleManager(sale.Id, 1, txtFullname.Text.Trim(), phone, LocalId, txtUpiCode.Text.Trim());
        RadGrid2.DataBind();
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomersManagement.aspx");
    }
}