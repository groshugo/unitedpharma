using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.Text;

public partial class Administrator_DialogPhoneNumber : System.Web.UI.Page
{
    CustomersRepository CustomerRepo = new CustomersRepository();
    CustomerTypeRepository CTypeRepo = new CustomerTypeRepository();
    ChannelRepository ChannelRepo = new ChannelRepository();
    GroupsRepository groupRepo = new GroupsRepository();
    RegionsRepository regionRepo = new RegionsRepository();
    AreasRepository areaRepo = new AreasRepository();
    LocalsRepository localRepo = new LocalsRepository();
    SalesmanRepository SRepo = new SalesmanRepository();
    Utility U = new Utility();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        GridSalemen.ClientSettings.Scrolling.AllowScroll=true;
    }
    protected override void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {
        ddlChannel.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlChannel_SelectedIndexChanged);
        ddlGroup.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlGroup_SelectedIndexChanged);
        ddlRegion.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlRegion_SelectedIndexChanged);
        ddlArea.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlArea_SelectedIndexChanged);
        ddlLocal.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlLocal_SelectedIndexChanged);

        this.btnFilterName.Click += new System.EventHandler(this.btnFilterName_Click);

        this.Load += new System.EventHandler(this.Page_Load);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            ListCustomerType();
            ListChannel();
            ListGroup();
        }
    }

    protected void CustomerList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        this.GetFilterData();
    }
    protected void GridSalemen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        this.GetFilterData();
    }

    #region Load data to comboboxes
    /// <summary>
    /// Load data to comboboxes when page_load
    /// </summary>
    private void ListCustomerType()
    {
        var customertype = CTypeRepo.GetAll();
        ddlCustomerType.DataSource = customertype;
        ddlCustomerType.DataTextField = "TypeName";
        ddlCustomerType.DataValueField = "Id";
        ddlCustomerType.DataBind();
        RadComboBoxItem item=new RadComboBoxItem("Select a type","0");
        ddlCustomerType.Items.Insert(0, item);
    }
    private void ListChannel()
    {
        var channelList = ChannelRepo.GetAll();
        ddlChannel.DataSource = channelList;
        ddlChannel.DataValueField = "Id";
        ddlChannel.DataTextField = "ChannelName";
        ddlChannel.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a channel", "0");
        ddlChannel.Items.Insert(0, item);
    }
    private void ListGroup()
    {
        var group = groupRepo.GetAll();
        ddlGroup.DataSource = group;
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "Id";
        ddlGroup.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a group", "0");
        ddlGroup.Items.Insert(0, item);
    }

    #endregion

   
    #region Select item on combobox
    public string GetLocalList(int GroupId, int RegionId, int AreaId, int LocalId)
    {
        string sql1=string.Empty;
        sql1 += "SELECT l.Id from Groups g left join region r on g.Id=r.GroupId left join area a on r.Id=a.RegionId left join local l on r.Id=l.AreaId where l.Id>0";
        if (GroupId > 0)
            sql1 += " and g.Id=" + GroupId;
        if(RegionId>0)
            sql1 += " and r.Id=" + RegionId;
        if(AreaId>0)
            sql1 += " and a.Id=" + AreaId;
        if(LocalId>0)
            sql1 += " and l.Id=" + LocalId;
        Utility U = new Utility();
        DataTable dt = U.GetList(sql1);
        string result = string.Empty;
        foreach (DataRow r in dt.Rows)
        {
            result += r["Id"].ToString() + ",";
        }
        if (result == "")
            return result;
        else
            return result.Substring(0, result.Length - 1);
    }
    public void GetFilterData()
    {
        int num = int.Parse(this.ddlSelect.SelectedValue);
        int num2 = int.Parse(this.ddlCustomerType.SelectedValue);
        int num3 = int.Parse(this.ddlChannel.SelectedValue);
        int groupId = int.Parse(this.ddlGroup.SelectedValue);
        int regionId = (this.ddlRegion.Items.Count > 0) ? int.Parse(this.ddlRegion.SelectedValue) : 0;
        int areaId = (this.ddlArea.Items.Count > 0) ? int.Parse(this.ddlArea.SelectedValue) : 0;
        int num7 = (this.ddlLocal.Items.Count > 0) ? int.Parse(this.ddlLocal.SelectedValue) : 0;
        string str = string.Empty;
        string str2 = string.Empty;
        string str3 = string.Empty;
        if (num == 1)
        {
            DataTable list = new DataTable();
            if (num2 > 0)
            {
                str = str + string.Format(" and CustomerTypeId={0}", num2);
            }
            if (num3 > 0)
            {
                str2 = str2 + string.Format(" and ChannelId={0}", num3);
            }
            string str4 = string.Empty;
            string str5 = this.txtFilterName.Text.Trim();
            if (!string.IsNullOrEmpty(str5))
            {
                str4 = str4 + string.Format(" and FullName like '%{0}%'", str5);
            }
            if (num7 > 0)
            {
                str3 = str3 + string.Format(" and LocalId = {0}", num7);
            }
            else if (areaId > 0)
            {
                string localIdStringByArea = this.GetLocalIdStringByArea(areaId);
                if (!string.IsNullOrEmpty(localIdStringByArea))
                {
                    str3 = str3 + string.Format(" and LocalId in ({0})", localIdStringByArea);
                }
            }
            else if (regionId > 0)
            {
                string localIdStringByRegion = this.GetLocalIdStringByRegion(regionId);
                if (!string.IsNullOrEmpty(localIdStringByRegion) && (localIdStringByRegion != "0"))
                {
                    str3 = str3 + string.Format(" and LocalId in ({0})", localIdStringByRegion);
                }
            }
            else if (groupId > 0)
            {
                string localIdStringByGroup = this.GetLocalIdStringByGroup(groupId);
                if (!string.IsNullOrEmpty(localIdStringByGroup) && (localIdStringByGroup != "0"))
                {
                    str3 = str3 + string.Format(" and LocalId in ({0})", localIdStringByGroup);
                }
            }
            string sql = "Select c.*, ct.TypeName as CustomerTypeName, cs.FullName as SupervisorName, p.PositionName as PositionName, cs.Phone as supervisorPhone   from Customer c left join CustomerType ct on c.CustomerTypeId = c.Id  left join Channel ch on c.ChannelId = ch.Id  left join CustomerSupervisor cs on c.Id=cs.CustomerId  left join SupervisorPosition p on cs.PositionId=p.Id where IsEnable=1 " + str4 + str + str2 + str3;
            list = this.U.GetList(sql);
            this.CustomerList.DataSource = null;
            this.CustomerList.DataSource = list;
        }
        else
        {
            DataTable table2 = new DataTable();
            if (groupId > 0)
            {
                table2.Merge(this.getGroupById(groupId));
            }
            if (regionId > 0)
            {
                table2.Merge(this.GetRegionById(regionId));
            }
            if (areaId > 0)
            {
                table2.Merge(this.GetAreaById(areaId));
            }
            if (num7 > 0)
            {
                table2.Merge(this.GetLocalById(num7));
            }
            if (((groupId == 0) && (regionId == 0)) && ((areaId == 0) && (num7 == 0)))
            {
                table2 = this.FilterByName();
            }
            this.GridSalemen.DataSource = null;
            this.GridSalemen.DataSource = table2;
        }

    }
    // Get info for Customers
    private DataTable GetCustomerTypeById(int customerTypeId)
    {
        string sql = string.Empty;
        sql = "select distinct c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName ";
        sql += "from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
        sql += " left join Channel ch on c.ChannelId=ch.Id";
        sql += " left join Local l on c.LocalId=l.Id left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";
        sql += "where t.Id=" + customerTypeId;
        if (txtFilterName.Text.Trim() != "")
            sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        return U.GetList(sql);
    }
    private DataTable GetChannelById(int channelId)
    {
        string sql = string.Empty;
        sql = "select distinct c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName ";
        sql += "from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
        sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id ";
        sql += "where c.ChannelId in (" + GetParentChannelId(channelId) + ")";
        if (txtFilterName.Text.Trim() != "")
            sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

        return U.GetList(sql);
    }
    private string GetParentChannelId(int channelId)
    {
        string sql = string.Empty;
        sql = "select Id from Channel where parentchannelId=" + channelId;
        DataTable dt = U.GetList(sql);
        string ChannelList = channelId + ",";
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ChannelList += dt.Rows[i][0] + ",";
        }
        ChannelList = ChannelList.Substring(0, ChannelList.Length - 1);
        if (ChannelRepo.CheckChannelRoot(channelId) == true)
        {
            sql = string.Empty;
            sql = "select Id from Channel where parentchannelId in (" + ChannelList + ")";
            DataTable dt2 = U.GetList(sql);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ChannelList +=  "," + dt2.Rows[i][0];
            }
        }
        return ChannelList;
    }
    // Endget info for Customers
    private DataTable getGroupById(int groupId)
    {
        string sql=string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesGroup as sg left join Salesmen as s on s.Id=sg.SalesmenId left join Role as r on s.RoleId=r.Id where sg.GroupId=" + groupId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id left join Area a on l.AreaId=a.Id left join Region r on r.Id=a.RegionId";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            sql += " where r.GroupId=" + groupId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable GetRegionById(int regionId)
    {
        string sql=string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesRegion as sr left join Salesmen as s on s.Id = sr.SalesmenId left join Role as r on s.RoleId=r.Id where sr.RegionId=" + regionId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id left join Area a on l.AreaId=a.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            sql += " where a.RegionId=" + regionId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable GetAreaById(int AreaId)
    {
        string sql=string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesArea as sa left join Salesmen as s on s.Id = sa.SalesmenId left join Role as r on s.RoleId=r.Id where sa.AreaId=" + AreaId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            sql += " where l.AreaId=" + AreaId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable GetLocalById(int LocalId)
    {
        string sql=string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesLocal as sl left join Salesmen as s on s.Id = sl.SalesmenId left join Role as r on s.RoleId=r.Id where sl.LocalId=" + LocalId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            //sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            sql += " where c.LocalId=" + LocalId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable FilterByName()
    {
        string sql = string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from Salesmen as s left join Role as r on s.RoleId=r.Id";
            if (txtFilterName.Text.Trim() != "")
                sql += " where s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            if (txtFilterName.Text.Trim() != "")
                sql += " where c.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }

    private void   rebindGrid()
    {
        if (int.Parse(ddlSelect.SelectedValue) == 1)
            CustomerList.Rebind();
        else
            GridSalemen.Rebind();
    }
    protected void ddlCustomerType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FilterDateOnForm();
    }
    private void ddlChannel_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        FilterDateOnForm();
    }

    private void ddlGroup_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        this.GetFilterData();
        this.rebindGrid();
        object regionByGroupId = this.regionRepo.GetRegionByGroupId(int.Parse(e.Value));
        if (regionByGroupId != null)
        {
            this.ddlRegion.DataSource = regionByGroupId;
            this.ddlRegion.DataTextField = "RegionName";
            this.ddlRegion.DataValueField = "Id";
            this.ddlRegion.DataBind();

            RadComboBoxItem item = new RadComboBoxItem("Select a region", "0");
            this.ddlRegion.Items.Insert(0, item);
            this.ddlArea.Items.Clear();
            this.ddlLocal.Items.Clear();
        }

    }

    private void ddlRegion_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        this.GetFilterData();
        this.rebindGrid();
        object areaByRegionId = this.areaRepo.GetAreaByRegionId(int.Parse(e.Value));
        if (areaByRegionId != null)
        {
            this.ddlArea.DataSource = areaByRegionId;
            this.ddlArea.DataTextField = "AreaName";
            this.ddlArea.DataValueField = "Id";
            this.ddlArea.DataBind();

            RadComboBoxItem item = new RadComboBoxItem("Select a area", "0");
            this.ddlArea.Items.Insert(0, item);
            this.ddlLocal.Items.Clear();
        }

    }

    private void ddlArea_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        this.GetFilterData();
        this.rebindGrid();
        object localByAreaId = this.localRepo.GetLocalByAreaId(int.Parse(e.Value));
        if (localByAreaId != null)
        {
            this.ddlLocal.DataSource = localByAreaId;
            this.ddlLocal.DataTextField = "LocalName";
            this.ddlLocal.DataValueField = "Id";
            this.ddlLocal.DataBind();
            RadComboBoxItem item = new RadComboBoxItem("Select a local", "0");
            this.ddlLocal.Items.Insert(0, item);
        }

    }

    private void ddlLocal_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        this.GetFilterData();
        this.rebindGrid();
    }

    private void btnFilterName_Click(object sender, EventArgs e)
    {
        FilterDateOnForm();
    }

    private void FilterDateOnForm()
    {
        if (IsCustomerOptionForFilter())
        {
            CustomerList.DataSource = FilterCustomers();
            CustomerList.DataBind();
        }
        else
        {
            // salesmen
            GridSalemen.DataSource = FilterSalesmen();
            GridSalemen.DataBind();
        }
    }

    #endregion
    protected void ddlSelect_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ResetFormWhenOptionChange();

        if (int.Parse(ddlSelect.SelectedValue) == 1)//customer
        {
            trCustomerType.Visible = true;
            CustomerList.Visible = true;
            GridSalemen.Visible = false;

        }
        else//sales
        {
            trCustomerType.Visible = false;
            CustomerList.Visible = false;
            GridSalemen.Visible = true;

            GetFilterData();
            rebindGrid();
        }
    }    

    private void ResetFormWhenOptionChange()
    {
        txtFilterName.Text = string.Empty;
        ddlCustomerType.SelectedIndex = 0;
        ddlChannel.SelectedIndex = 0;
        ddlGroup.SelectedIndex = 0;
        ddlRegion.SelectedIndex = 0;
        ddlArea.SelectedIndex = 0;
        ddlLocal.SelectedIndex = 0;
    }

    private List<vwCustomer> FilterCustomers()
    {
        var fullname = txtFilterName.Text.Trim();

        var customerTypeStringValue = !string.IsNullOrEmpty(ddlCustomerType.SelectedValue) ? ddlCustomerType.SelectedValue : "0";
        var customerTypeId = int.Parse(customerTypeStringValue);

        var channelStringValue = !string.IsNullOrEmpty(ddlChannel.SelectedValue) ? ddlChannel.SelectedValue : "0";
        var channelId = int.Parse(channelStringValue);

        var localStringValue = !string.IsNullOrEmpty(ddlLocal.SelectedValue) ? ddlLocal.SelectedValue : "0";
        var localId = int.Parse(localStringValue);

        return CustomerRepo.FilterCustomersForBrowsePhoneNumber(fullname, customerTypeId, channelId, localId);
    }

    private List<vwSalemen> FilterSalesmen()
    {
        var fullname = txtFilterName.Text.Trim();

        var localId = 0;

        if (ddlLocal.SelectedIndex > 0)
        {
            localId = int.Parse(ddlLocal.SelectedValue);
        }

        return SRepo.FilterSalesmenForBrowsePhoneNumber(fullname, localId);
    }

    private bool IsCustomerOptionForFilter()
    {
        if (ddlSelect.SelectedValue == "1") return true;

        return false;
    }

    private string GetLocalIdStringByArea(int areaId)
    {
        string sql = string.Format("select id from local where AreaId={0}", areaId);
        DataTable list = this.U.GetList(sql);
        StringBuilder builder = new StringBuilder();
        if ((list != null) && (list.Rows.Count > 0))
        {
            foreach (DataRow row in list.Rows)
            {
                builder.AppendFormat("{0},", row["Id"]);
            }
            if (builder.Length > 0)
            {
                string str2 = builder.ToString();
                return str2.Substring(0, str2.Length - 1);
            }
        }
        return string.Empty;
    }

    private string GetLocalIdStringByGroup(int groupId)
    {
        string sql = string.Format("select id from local where AreaId in (select distinct Id from Area where RegionId in (select distinct id from Region where GroupId={0}))", groupId);
        DataTable list = this.U.GetList(sql);
        StringBuilder builder = new StringBuilder();
        if ((list != null) && (list.Rows.Count > 0))
        {
            foreach (DataRow row in list.Rows)
            {
                builder.AppendFormat("{0},", row["Id"]);
            }
            if (builder.Length > 0)
            {
                string str2 = builder.ToString();
                return str2.Substring(0, str2.Length - 1);
            }
        }
        return string.Empty;
    }

    private string GetLocalIdStringByRegion(int regionId)
    {
        string sql = string.Format("select id from local where AreaId in (select Distinct Id from Area where RegionId={0})", regionId);
        DataTable list = this.U.GetList(sql);
        StringBuilder builder = new StringBuilder();
        if ((list != null) && (list.Rows.Count > 0))
        {
            foreach (DataRow row in list.Rows)
            {
                builder.AppendFormat("{0},", row["Id"]);
            }
            if (builder.Length > 0)
            {
                string str2 = builder.ToString();
                return str2.Substring(0, str2.Length - 1);
            }
        }
        return string.Empty;
    }


}