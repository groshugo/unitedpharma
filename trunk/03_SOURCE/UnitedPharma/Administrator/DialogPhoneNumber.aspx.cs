using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;

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
        ///
        /// SelectedIndexChanged of combobox
        ///
        ddlChannel.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlChannel_SelectedIndexChanged);
        ddlGroup.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlGroup_SelectedIndexChanged);
        ddlRegion.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlRegion_SelectedIndexChanged);
        ddlArea.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlArea_SelectedIndexChanged);
        ddlLocal.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddlLocal_SelectedIndexChanged);
        ///
        /// Button click
        ///
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
            ListRegion();
            ListArea();
            ListLocal();
        }
    }
    private void AllCustomers()
    {
        CustomerList.DataSource = CustomerRepo.GetAllViewCustomers();
    }
    protected void CustomerList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        GetFilterData();
    }
    protected void GridSalemen_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        GetFilterData();
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
    private void ListRegion()
    {
        var region = regionRepo.GetAll();
        ddlRegion.DataSource = region;
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "Id";
        ddlRegion.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a region", "0");
        ddlRegion.Items.Insert(0, item);
    }
    private void ListArea()
    {
        var area = areaRepo.GetAll();
        ddlArea.DataSource = area;
        ddlArea.DataTextField = "AreaName";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a area", "0");
        ddlArea.Items.Insert(0, item);
    }
    private void ListLocal()
    {
        var local = localRepo.GetAll();
        ddlLocal.DataSource = local;
        ddlLocal.DataTextField = "LocalName";
        ddlLocal.DataValueField = "Id";
        ddlLocal.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a local", "0");
        ddlLocal.Items.Insert(0, item);
    }
    #endregion

    /// <summary>
    /// Select item on combobox
    /// </summary>
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
        DataTable dt = new DataTable();
        int selectType = int.Parse(ddlSelect.SelectedValue);
        int customerTypeId = int.Parse(ddlCustomerType.SelectedValue);
        int channelId = int.Parse(ddlChannel.SelectedValue);
        int GroupId = int.Parse(ddlGroup.SelectedValue);
        int RegionId = (ddlRegion.Items.Count > 0) ? int.Parse(ddlRegion.SelectedValue) : 0;
        int AreaId = (ddlArea.Items.Count > 0) ? int.Parse(ddlArea.SelectedValue) : 0;
        int LocalId = (ddlLocal.Items.Count > 0) ? int.Parse(ddlLocal.SelectedValue) : 0;

        string sql = string.Empty;
        if (selectType == 1)//customer
        {
            DataTable dtFilterCustomers = new DataTable();
            if (customerTypeId > 0)
                dtFilterCustomers.Merge(GetCustomerTypeById(customerTypeId));
            if (channelId > 0)
                dtFilterCustomers.Merge(GetChannelById(channelId));
            if (GroupId > 0)
                dtFilterCustomers.Merge(getGroupById(GroupId));
            if (RegionId > 0)
                dtFilterCustomers.Merge(GetRegionById(RegionId));
            if (AreaId > 0)
                dtFilterCustomers.Merge(GetAreaById(AreaId));
            if (LocalId > 0)
                dtFilterCustomers.Merge(GetLocalById(LocalId));
            if (GroupId == 0 && RegionId == 0 && AreaId == 0 && LocalId == 0 && customerTypeId == 0 && channelId == 0)
                dtFilterCustomers=FilterByName();

            CustomerList.DataSource = null;
            CustomerList.DataSource = dtFilterCustomers;
        }
        else//sale
        {
            DataTable dtSaleFilter = new DataTable();
            if (GroupId > 0)
            {
                dtSaleFilter.Merge(getGroupById(GroupId));
            }
            if (RegionId > 0)
            {
                dtSaleFilter.Merge(GetRegionById(RegionId));
            }
            if (AreaId > 0)
                dtSaleFilter.Merge(GetAreaById(AreaId));
            if (LocalId > 0)
                dtSaleFilter.Merge(GetLocalById(LocalId));
            if (GroupId == 0 && RegionId == 0 && AreaId == 0 && LocalId == 0)
                dtSaleFilter = FilterByName();

            GridSalemen.DataSource = null;
            GridSalemen.DataSource = dtSaleFilter;
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

    private void rebindGrid()
    {
        if (int.Parse(ddlSelect.SelectedValue) == 1)
            CustomerList.Rebind();
        else
            GridSalemen.Rebind();
    }
    protected void ddlCustomerType_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {      
        GetFilterData();
        rebindGrid();
    }
    private void ddlChannel_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
         GetFilterData();
         rebindGrid();
    }

    private void ddlGroup_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
        rebindGrid();
    }

    private void ddlRegion_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
        rebindGrid();
    }

    private void ddlArea_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
        rebindGrid();
    }

    private void ddlLocal_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
        rebindGrid();
    }

    private void btnFilterName_Click(object sender, EventArgs e)
    {
        GetFilterData();
        rebindGrid();
    }
    #endregion
    protected void ddlSelect_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
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
}