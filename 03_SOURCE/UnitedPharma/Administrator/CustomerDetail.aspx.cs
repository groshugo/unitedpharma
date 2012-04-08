using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Administrator_CustomerDetail : System.Web.UI.Page
{
    CustomersRepository CRepo = new CustomersRepository();
    Utility U = new Utility();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        Utility.SetCurrentMenu("mCustomer");
        if (Request.QueryString["ID"] != null)
        {
            int CustomerID = int.Parse(Request.QueryString["ID"]);
            var Select = CRepo.GetCustomersByID(CustomerID);
            lblUpi.Text = Select.UpiCode;
            lblCustomerName.Text = Select.FullName;
            lblPhoneNumber.Text = Select.Phone;
            lblAddress.Text = Select.Address + ", " + Select.Street + ", " + Select.Ward;
            lblCustomerType.Text = Select.CustomerTypeName;
            lblChannel.Text = Select.ChannelName;
            lblDistrict.Text = Select.DistrictName;
            lblLocal.Text = Select.LocalName;
            lblCreateDate.Text = string.Format("{0:d}", Select.CreateDate);
            lblUpdateDate.Text = string.Format("{0:d}", Select.UpdateDate);
            lblStatus.Text = (Select.Status == true) ? "Active" : "Closed";
            chkEnable.Checked = Convert.ToBoolean(Select.IsEnable);

            litNoteOfSalesmen.Text = Select.NoteOfSalesmen;

            LoadSaleManager(Convert.ToInt32(Select.LocalId));
            LoadSupervisorManager(CustomerID);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("CustomersPage.aspx");
    }
    private void LoadSaleManager(int LocalId)
    {
        AreasRepository Arepo = new AreasRepository();
        RegionsRepository RRepo = new RegionsRepository();
        string sqlarea = GetAreaByLocalId(LocalId);
        string SqlRegion = GetRegionByAreaId(sqlarea);
        string SqlGroup = GetGroupByRegionId(SqlRegion);
        string SalesmenIdList = "";
        if (SalesGroupList(SqlGroup) != "")
            SalesmenIdList += SalesGroupList(SqlGroup) + ",";
        if (SalesRegionList(SqlRegion) != "")
            SalesmenIdList += SalesRegionList(SqlRegion) + ",";
        if (SalesAreaList(sqlarea) != "")
            SalesmenIdList += SalesAreaList(sqlarea)+","; 
        if(SalesLocalList(LocalId)!="")
            SalesmenIdList += SalesLocalList(LocalId);
        string sql = "Select s.*, r.RoleName from salesmen s left join role r on s.RoleId=r.Id where s.id in (" + SalesmenIdList + ")";
        SalesManager.DataSource = U.GetList(sql);
        SalesManager.Rebind();        
    }
    private string GetAreaByLocalId(int LocalId)
    {
        string SqlRegion = "select AreaId from Local where Id = " + LocalId + " group by AreaId";
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
    private string GetRegionByAreaId(string AreaIdList)
    {
        string SqlRegion = "select RegionId from Area where Id in (" + AreaIdList + ") group by RegionId";
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
    private string GetGroupByRegionId(string RegionIdList)
    {
        string SqlRegion = "select GroupId from Region where Id in (" + RegionIdList + ") group by GroupId";
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

    private string SalesGroupList(string SqlGroup)
    {
        string SqlRegion = "select SalesmenId from SalesGroup where GroupId in (" + SqlGroup + ") group by SalesmenId";
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
    private string SalesRegionList(string SqlRegion)
    {
        string Sql = "select SalesmenId from SalesRegion where RegionId in (" + SqlRegion + ") group by SalesmenId";
        dt = U.GetList(Sql);
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
    private string SalesAreaList(string SqlArea)
    {
        string Sql = "select SalesmenId from SalesArea where AreaId in (" + SqlArea + ") group by SalesmenId";
        dt = U.GetList(Sql);
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
    private string SalesLocalList(int LocalId)
    {
        string Sql = "select SalesmenId from SalesLocal where LocalId =" + LocalId + " group by SalesmenId";
        dt = U.GetList(Sql);
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
    private void LoadSupervisorManager(int CustomerId)
    {
        CustomerSuperviorRepository CRepo = new CustomerSuperviorRepository();
        SupervisorManager.DataSource = CRepo.GetCustomerSupervisorById(CustomerId);
    }
}