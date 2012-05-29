using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.App_Code;
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

    private readonly SalesmanRepository _sRepo = new SalesmanRepository();
    private const string DataTextFieldName = "Fullname";
    private const string DataValueFieldName = "Id";

    Utility U = new Utility();
    protected void Page_Init(object sender, System.EventArgs e)
    {
        GridSalemen.ClientSettings.Scrolling.AllowScroll = true;
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

            ObjLogin adm = (ObjLogin)Session["objLogin"];
            if(adm != null)
            {
                int saleId = adm.Id;
                ListGroup(saleId);
                ListRegion(saleId, GetGroupBySaleId(saleId));

                var salesmen = _sRepo.GetById(saleId);

                if (salesmen == null) poc_pos_container.Visible = false;
                else
                {
                    DeterminePocPos(salesmen);
                }
            }
        }
    }
    private string GetGroupBySaleId(int saleId)
    {
        var GroupList = groupRepo.GetGroupBySaleId(saleId);
        string output = "";
        foreach (var item in GroupList)
        {
            output += item.GroupId + ",";
        }
        if (output != "")
            return output.Substring(0, output.Length - 1);
        else
            return "0";
    }
    private string GetRegionBySaleId(int saleId)
    {
        var GroupList = regionRepo.GetRegionBySaleId(saleId);
        string output = "";
        foreach (var item in GroupList)
        {
            output += item.RegionId + ",";
        }
        if (output != "")
            return output.Substring(0, output.Length - 1);
        else
            return "0";
    }
    private string GetAreaBySaleId(int saleId)
    {
        var GroupList = areaRepo.GetAreaBySaleId(saleId);
        string output = "";
        foreach (var item in GroupList)
        {
            output += item.AreaId + ",";
        }
        if (output != "")
            return output.Substring(0, output.Length - 1);
        else
            return "0";
    }
    private string GetLocalBySaleId(int saleId)
    {
        var GroupList = localRepo.GetLocalBySaleId(saleId);
        string output = "";
        foreach (var item in GroupList)
        {
            output += item.LocalId + ",";
        }
        if (output != "")
            return output.Substring(0, output.Length - 1);
        else
            return "0";
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
    private void ListCustomerType(string localList)
    {
        string sql = "select Distinct CT.Id,TypeName from CustomerType CT left join Customer C on CT.Id=C.CustomerTypeId where C.LocalId in (" + localList + ")";
        ddlCustomerType.DataSource = U.GetList(sql);
        ddlCustomerType.DataTextField = "TypeName";
        ddlCustomerType.DataValueField = "Id";
        ddlCustomerType.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a type", "0");
        ddlCustomerType.Items.Insert(0, item);
    }
    private void ListChannel(string localList)
    {
        string sql = "select Distinct CT.Id,ChannelName from Channel CT left join Customer C on CT.Id=C.ChannelId where C.LocalId in (" + localList + ")";
        ddlChannel.DataSource = U.GetList(sql);
        ddlChannel.DataValueField = "Id";
        ddlChannel.DataTextField = "ChannelName";
        ddlChannel.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a channel", "0");
        ddlChannel.Items.Insert(0, item);
    }
    private void ListGroup(int saleId)
    {
        string sql = "select Id,GroupName from Groups where Id in (" + GetGroupBySaleId(saleId) + ")";
        ddlGroup.DataSource = U.GetList(sql);
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "Id";
        ddlGroup.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a group", "0");
        ddlGroup.Items.Insert(0, item);
    }
    private void ListRegion(int saleId, string groupList)
    {
        DataTable dt = new DataTable();
        string RegionIdList = "";
        if (groupList != "0")
        {
            String sql = "select Id from Region where GroupId in (" + groupList + ")";
            dt = U.GetList(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RegionIdList += dt.Rows[i][0] + ",";
            }
        }
        string para = RegionIdList + GetRegionBySaleId(saleId);
        string sql1 = "select Id,RegionName from Region where Id in (" + para + ")";
        ddlRegion.DataSource = U.GetList(sql1);
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "Id";
        ddlRegion.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a region", "0");
        ddlRegion.Items.Insert(0, item);
        ListArea(saleId, para);
    }
    private void ListArea(int saleId, string regionList)
    {
        DataTable dt = new DataTable();
        string areaList = "";
        if (regionList != "0")
        {
            string sql1 = "select Id from Area where RegionId in (" + regionList + ")";
            dt = U.GetList(sql1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                areaList += dt.Rows[i][0] + ",";
            }

        }
        string para = areaList + GetAreaBySaleId(saleId);
        string sql = "select Id,AreaName from Area where Id in (" + para + ")";
        ddlArea.DataSource = U.GetList(sql);
        ddlArea.DataTextField = "AreaName";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a area", "0");
        ddlArea.Items.Insert(0, item);
        ListLocal(saleId, para);
    }
    private void ListLocal(int saleId, string areaList)
    {
        DataTable dt = new DataTable();
        string localList = "";
        if (areaList != "0")
        {
            string sql1 = "select Id from Area where RegionId in (" + areaList + ")";
            dt = U.GetList(sql1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                localList += dt.Rows[i][0] + ",";
            }

        }
        string para = localList + GetLocalBySaleId(saleId);
        string sql = "select Id,LocalName from Local where Id in (" + para + ")";
        ddlLocal.DataSource = U.GetList(sql);
        ddlLocal.DataTextField = "LocalName";
        ddlLocal.DataValueField = "Id";
        ddlLocal.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a local", "0");
        ddlLocal.Items.Insert(0, item);
        ListCustomerType(para);
        ListChannel(para);
    }
    #endregion

    #region Select item on combobox
    public string GetLocalList(int GroupId, int RegionId, int AreaId, int LocalId)
    {
        string sql1 = string.Empty;
        sql1 += "SELECT l.Id from Groups g left join region r on g.Id=r.GroupId left join area a on r.Id=a.RegionId left join local l on r.Id=l.AreaId where l.Id>0";
        if (GroupId > 0)
            sql1 += " and g.Id=" + GroupId;
        if (RegionId > 0)
            sql1 += " and r.Id=" + RegionId;
        if (AreaId > 0)
            sql1 += " and a.Id=" + AreaId;
        if (LocalId > 0)
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

    private void GetDataToGrid(int salemenId)
    {
        var sb = new StringBuilder();
        var flag = true;

        if(salemenId == -1 || salemenId == 0)
        {
            var adm = (ObjLogin)Session["objLogin"];
            if (adm != null)
                salemenId = adm.Id;
        }

        // salemen
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sb.Append("select distinct s.*, r.RoleName from Salesmen as s left join Role as r on s.RoleId=r.Id where 1=1");
            if (txtFilterName.Text.Trim() != "")
            {
                sb.AppendFormat(" and s.FullName like '%{0}%'", txtFilterName.Text.Trim());
            }   

            if(salemenId > 0)
            {
                sb.AppendFormat(" and s.SalesmenManagerId = {0}", salemenId);
            }

            flag = false;
        }
        else // customer
        {
            sb.Append("select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName");
            sb.Append(" from Customer c left join CustomerType t on c.CustomerTypeId=t.Id");
            sb.Append(" left join Channel ch on c.ChannelId=ch.Id");
            sb.Append(" left join Local l on c.LocalId=l.Id");
            sb.Append(" left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id where 1=1 ");
            
            if (txtFilterName.Text.Trim() != "")
                sb.AppendFormat(" and c.FullName like '%{0}%' ", txtFilterName.Text.Trim());

            if (salemenId > 0)
            {
                sb.AppendFormat(" and c.Localid in (Select LocalId From SalesLocal Where SalesmenId={0})", salemenId);
            }
        }
        
        DataTable dt = U.GetList(sb.ToString());

        if (flag)
        {
            CustomerList.DataSource = dt;
            CustomerList.DataBind();;
        }
        else
        {
            GridSalemen.DataSource = dt;
            GridSalemen.DataBind();
        }
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

            if(LocalId > 0)
            {
                dtFilterCustomers.Merge(GetLocalById(LocalId));
            }
            else
            {
                if(AreaId > 0)
                {
                    dtFilterCustomers.Merge(GetAreaById(AreaId));
                }
                else
                {
                    if(RegionId > 0)
                    {
                        dtFilterCustomers.Merge(GetRegionById(RegionId));
                    }
                    else
                    {
                        if(GroupId > 0)
                            dtFilterCustomers.Merge(getGroupById(GroupId));
                    }
                }
            }
            

            if (GroupId == 0 && RegionId == 0 && AreaId == 0 && LocalId == 0 && customerTypeId == 0 && channelId == 0)
                dtFilterCustomers = FilterByName();

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
                ChannelList += "," + dt2.Rows[i][0];
            }
        }
        return ChannelList;
    }
    // Endget info for Customers
    private DataTable getGroupById(int groupId)
    {
        string sql = string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesGroup as sg left join Salesmen as s on s.Id=sg.SalesmenId " +
                  "left join Role as r on s.RoleId=r.Id where sg.GroupId=" + groupId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            //sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            //sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            //sql += " left join Channel ch on c.ChannelId=ch.Id";
            //sql += " left join Local l on c.LocalId=l.Id left join Area a on l.AreaId=a.Id left join Region r on r.Id=a.RegionId";
            //sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            //sql += " where r.GroupId=" + groupId;
            //if (txtFilterName.Text.Trim() != "")
            //    sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

            var salemenId = -1;
            var adm = Session["objLogin"] as ObjLogin;
            if (adm != null) salemenId = adm.Id;

            var strRegionIdList = UtilitiesHelpers.Instance.SalesRegionList(groupId.ToString(), salemenId);
            if (string.IsNullOrEmpty(strRegionIdList)) return null;

            var strAreaIdList = UtilitiesHelpers.Instance.SalesAreaList(strRegionIdList, salemenId);
            if (string.IsNullOrEmpty(strAreaIdList)) return null;

            var strLocalIdList = UtilitiesHelpers.Instance.SalesLocalList(strAreaIdList, salemenId);
            if (string.IsNullOrEmpty(strLocalIdList)) return null;

            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            if (txtFilterName.Text.Trim() != "")
                sql += " where c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

            sql += " and c.Localid in (" + strLocalIdList.TrimEnd(',') + ")";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable GetRegionById(int regionId)
    {
        string sql = string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesRegion as sr left join Salesmen as s on s.Id = sr.SalesmenId left join Role as r on s.RoleId=r.Id where sr.RegionId=" + regionId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            //sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            //sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            //sql += " left join Channel ch on c.ChannelId=ch.Id";
            //sql += " left join Local l on c.LocalId=l.Id left join Area a on l.AreaId=a.Id";
            //sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            //sql += " where a.RegionId=" + regionId;
            //if (txtFilterName.Text.Trim() != "")
            //    sql += " and c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

            var salemenId = -1;
            var adm = Session["objLogin"] as ObjLogin;
            if (adm != null) salemenId = adm.Id;

            var strAreaIdList = UtilitiesHelpers.Instance.SalesAreaList(regionId.ToString(), salemenId);
            if (string.IsNullOrEmpty(strAreaIdList)) return null;

            var strLocalIdList = UtilitiesHelpers.Instance.SalesLocalList(strAreaIdList, salemenId);
            if (string.IsNullOrEmpty(strLocalIdList)) return null;

            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            if (txtFilterName.Text.Trim() != "")
                sql += " where c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

            sql += " and c.Localid in (" + strLocalIdList.TrimEnd(',') + ")";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable GetAreaById(int AreaId)
    {
        string sql = string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from SalesArea as sa left join Salesmen as s on s.Id = sa.SalesmenId left join Role as r on s.RoleId=r.Id where sa.AreaId=" + AreaId;
            if (txtFilterName.Text.Trim() != "")
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
        }
        else
        {
            var salemenId = -1;
            var adm = Session["objLogin"] as ObjLogin;
            if (adm != null) salemenId = adm.Id;

            var strLocalIdList = UtilitiesHelpers.Instance.SalesLocalList(AreaId.ToString(), salemenId);
            if (string.IsNullOrEmpty(strLocalIdList)) return null;

            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            if (txtFilterName.Text.Trim() != "")
                sql += " where c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

            sql += " and c.Localid in (" + strLocalIdList.TrimEnd(',') + ")";
        }
        DataTable dt = U.GetList(sql);
        return dt;
    }
    private DataTable GetLocalById(int LocalId)
    {
        string sql = string.Empty;
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
        var salemenId = -1;
        var adm = Session["objLogin"] as ObjLogin;
        if (adm != null) salemenId = adm.Id;

        string sql = string.Empty;
        if (int.Parse(ddlSelect.SelectedValue) == 2)
        {
            sql = "select distinct s.*, r.RoleName from Salesmen as s left join Role as r on s.RoleId=r.Id where 1=1";
            if (txtFilterName.Text.Trim() != "")
            {
                sql += " and s.FullName like '%" + txtFilterName.Text.Trim() + "%'";
            }
                

            if(salemenId != -1)
            {
                sql += " and s.SalesmenManagerId = " + salemenId;
            }
        }
        else
        {
            var strGroupIdList = UtilitiesHelpers.Instance.GetGroupBySalemenId(salemenId);
            if (string.IsNullOrEmpty(strGroupIdList)) return null;

            var strRegionIdList = UtilitiesHelpers.Instance.SalesRegionList(strGroupIdList, salemenId);
            if (string.IsNullOrEmpty(strRegionIdList)) return null;

            var strAreaIdList = UtilitiesHelpers.Instance.SalesAreaList(strRegionIdList, salemenId);
            if (string.IsNullOrEmpty(strAreaIdList)) return null;

            var strLocalIdList = UtilitiesHelpers.Instance.SalesLocalList(strAreaIdList, salemenId);
            if (string.IsNullOrEmpty(strLocalIdList)) return null;

            sql = "select c.*, s.FullName as SupervisorName,p.PositionName,s.Phone as supervisorPhone,t.TypeName as CustomerTypeName";
            sql += " from Customer c left join CustomerType t on c.CustomerTypeId=t.Id";
            sql += " left join Channel ch on c.ChannelId=ch.Id";
            sql += " left join Local l on c.LocalId=l.Id";
            sql += " left join CustomerSupervisor s on c.Id=s.CustomerId left join SupervisorPosition p on s.PositionId=p.Id";
            if (txtFilterName.Text.Trim() != "")
                sql += " where c.FullName like '%" + txtFilterName.Text.Trim() + "%'";

            sql += " and c.Localid in (" + strLocalIdList.TrimEnd(',') + ")";
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

        var region = regionRepo.GetRegionByGroupId(int.Parse(e.Value));
        if (region != null)
        {
            ddlRegion.DataSource = region;
            ddlRegion.DataTextField = "RegionName";
            ddlRegion.DataValueField = "Id";
            ddlRegion.DataBind();

            var item = new RadComboBoxItem("Select a region", "0");
            ddlRegion.Items.Insert(0, item);

            ddlArea.Items.Clear();
            ddlLocal.Items.Clear();
        }
    }

    private void ddlRegion_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
        rebindGrid();

        var area = areaRepo.GetAreaByRegionId(int.Parse(e.Value));
        if (area != null)
        {
            ddlArea.DataSource = area;
            ddlArea.DataTextField = "AreaName";
            ddlArea.DataValueField = "Id";
            ddlArea.DataBind();

            var item = new RadComboBoxItem("Select a area", "0");
            ddlArea.Items.Insert(0, item);

            ddlLocal.Items.Clear();
        }
    }

    private void ddlArea_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
        rebindGrid();

        var local = localRepo.GetLocalByAreaId(int.Parse(e.Value));
        if (local != null)
        {
            ddlLocal.DataSource = local;
            ddlLocal.DataTextField = "LocalName";
            ddlLocal.DataValueField = "Id";
            ddlLocal.DataBind();

            var item = new RadComboBoxItem("Select a local", "0");
            ddlLocal.Items.Insert(0, item);
        }
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

    protected void cboTROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var trom = _sRepo.GetSalemenById(int.Parse(e.Value));
            if (trom != null)
            {
                // Load data to TPS
                var tps = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, trom.Id);
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
            var tps = _sRepo.GetSalemenById(int.Parse(e.Value));

            if (tps != null)
            {
                // Load data to TPS
                var tpr = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPR, tps.Id);

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
        GetDataToGrid(Parsers.ToInt(e.Value));
    }

    protected void cboEROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom = _sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom != null)
            {
                // Load data to PSS1
                var pss1 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS1, erom.Id);
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
            var pss1 = _sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss1 != null)
            {
                // Load data to PSR 1
                var psr1 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR1, pss1.Id);

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
        GetDataToGrid(Parsers.ToInt(e.Value));
    }

    protected void cboEROM2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom2 = _sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom2 != null)
            {
                // Load data to PSS2
                var pss2 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS2, erom2.Id);
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
            var pss2 = _sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss2 != null)
            {
                // Load data to PSR 2
                var psr2 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR2, pss2.Id);

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
        GetDataToGrid(Parsers.ToInt(e.Value));
    }

    private void LoadListSalesmenToCombo(ICollection trom, RadComboBox comboBox, string firstItemText)
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

    private void DeterminePocPos(Salesmen salesmen)
    {
        if (salesmen.RoleId.HasValue)
        {
            switch (salesmen.RoleId.Value)
            {
                case (int)SalesmenRole.EROM:
                    cboEROM.Visible = false;
                    litEROM.Visible = false;
                    cboPSS1.Visible = true;
                    cboPSR1.Visible = true;

                    // Load data for PSS1
                    var pss1 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS1, salesmen.Id);
                    LoadListSalesmenToCombo(pss1, cboPSS1, "Select a PSS1");
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR1);

                    litPOC.Visible = true;
                    litPOS.Visible = false;

                    HideComboBox(0);
                    break;

                case (int)SalesmenRole.PSS1:
                    cboEROM.Visible = false;
                    cboPSS1.Visible = false;
                    litEROM.Visible = false;
                    litPSS1.Visible = false;

                    cboPSR1.Visible = true;
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
                    // Load data for PSS1
                    var psr1 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR1, salesmen.Id);
                    LoadListSalesmenToCombo(psr1, cboPSR1, "Select a PSR1");

                    litPOC.Visible = true;
                    litPOS.Visible = false;

                    HideComboBox(0);
                    break;

                case (int)SalesmenRole.PSR1:
                    cboEROM.Visible = false;
                    cboPSS1.Visible = false;
                    litEROM.Visible = false;
                    litPSS1.Visible = false;

                    cboPSR1.Visible = false;
                    litPSR1.Visible = false;
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR1);

                    var itemPsr1 = new RadComboBoxItem(salesmen.FullName, salesmen.Id.ToString());
                    cboPSR1.Items.Insert(0, itemPsr1);

                    litPOC.Visible = true;
                    litPOS.Visible = false;

                    // hide EROM2 and TROM
                    HideComboBox(0);
                    break;

                // EROM 2
                case (int)SalesmenRole.EROM2:
                    cboEROM2.Visible = false;
                    litEROM2.Visible = false;
                    cboPSR2.Visible = true;
                    cboPSR2.Visible = true;

                    // Load data for PSS2
                    var pss2 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS2, salesmen.Id);
                    LoadListSalesmenToCombo(pss2, cboPSS2, "Select a PSS2");
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR2);

                    litPOC.Visible = true;
                    litPOS.Visible = false;

                    HideComboBox(2);
                    break;

                case (int)SalesmenRole.PSS2:
                    cboEROM2.Visible = false;
                    cboPSS2.Visible = false;
                    litEROM2.Visible = false;
                    litPSS2.Visible = false;

                    cboPSR2.Visible = true;
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
                    // Load data for PSR2
                    var psr2 = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR2, salesmen.Id);
                    LoadListSalesmenToCombo(psr2, cboPSR2, "Select a PSR2");

                    litPOC.Visible = true;
                    litPOS.Visible = false;

                    HideComboBox(2);
                    break;

                case (int)SalesmenRole.PSR2:
                    cboEROM2.Visible = false;
                    cboPSS2.Visible = false;
                    litEROM2.Visible = false;
                    litPSS2.Visible = false;

                    cboPSR2.Visible = false;
                    litPSR2.Visible = false;
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR2);

                    litPOC.Visible = true;
                    litPOS.Visible = false;

                    var itemPsr2 = new RadComboBoxItem(salesmen.FullName, salesmen.Id.ToString());
                    cboPSR2.Items.Insert(0, itemPsr2);

                    HideComboBox(2);
                    break;

                // TROM
                case (int)SalesmenRole.TROM:
                    cboTROM.Visible = false;
                    litTROM.Visible = false;

                    cboTPS.Visible = true;
                    // Load data for TPS
                    var tps = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, salesmen.Id);
                    LoadListSalesmenToCombo(tps, cboTPS, "Select a TPS");
                    UtilitiesHelpers.Instance.ClearComboData(cboTPR);

                    litPOC.Visible = false;
                    litPOS.Visible = true;

                    HideComboBox(1);
                    break;

                case (int)SalesmenRole.TPS:
                    cboTROM.Visible = false;
                    cboTPS.Visible = false;
                    litTROM.Visible = false;
                    litTPS.Visible = false;

                    cboTPR.Visible = true;
                    UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
                    // Load data for TPR
                    var tpr = _sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPR, salesmen.Id);
                    LoadListSalesmenToCombo(tpr, cboTPR, "Select a TPR");

                    litPOC.Visible = false;
                    litPOS.Visible = true;

                    HideComboBox(1);
                    break;

                case (int)SalesmenRole.TPR:
                    cboTROM.Visible = false;
                    cboTPS.Visible = false;
                    litTROM.Visible = false;
                    litTPS.Visible = false;

                    cboTPR.Visible = false;
                    litTPR.Visible = false;
                    UtilitiesHelpers.Instance.ClearComboData(cboTPR);

                    var itemTpr = new RadComboBoxItem(salesmen.FullName, salesmen.Id.ToString());
                    cboTPR.Items.Insert(0, itemTpr);

                    litPOC.Visible = false;
                    litPOS.Visible = true;

                    HideComboBox(1);
                    break;
            }
        }
    }

    private void HideComboBox(int salesType)
    {
        switch (salesType)
        {
            case 0: // EROM
                cboEROM2.Visible = false;
                litEROM2.Visible = false;
                cboPSS2.Visible = false;
                litPSS2.Visible = false;
                cboPSR2.Visible = false;
                litPSR2.Visible = false;

                cboTROM.Visible = false;
                cboTPS.Visible = false;
                cboTPR.Visible = false;
                litTROM.Visible = false;
                litTPS.Visible = false;
                litTPR.Visible = false;
                break;
            case 1: // TROM
                cboEROM2.Visible = false;
                cboPSS2.Visible = false;
                cboPSR2.Visible = false;
                litEROM2.Visible = false;
                litPSS2.Visible = false;
                litPSR2.Visible = false;

                cboEROM.Visible = false;
                cboPSS1.Visible = false;
                cboPSR1.Visible = false;
                litEROM.Visible = false;
                litPSS1.Visible = false;
                litPSR1.Visible = false;
                break;
            case 2: // EROM2
                cboTROM.Visible = false;
                cboTPS.Visible = false;
                cboTPR.Visible = false;
                litTROM.Visible = false;
                litTPS.Visible = false;
                litTPR.Visible = false;

                cboEROM.Visible = false;
                cboPSS1.Visible = false;
                cboPSR1.Visible = false;
                litEROM.Visible = false;
                litPSS1.Visible = false;
                litPSR1.Visible = false;
                break;
        }
    }
}