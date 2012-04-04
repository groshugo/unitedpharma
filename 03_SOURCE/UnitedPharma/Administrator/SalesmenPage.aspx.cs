using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;
using System.Data;

public partial class Administrator_SalesmenPage : System.Web.UI.Page
{
    SalesmanRepository salesRepo = new SalesmanRepository();
    ValidationFields checkValid = new ValidationFields();
    GroupsRepository groupRepo = new GroupsRepository();
    RegionsRepository regionRepo = new RegionsRepository();
    AreasRepository areaRepo = new AreasRepository();
    LocalsRepository localRepo = new LocalsRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSalesmen");
            ListGroup();
        }
    }
    private UPIDataContext _dataContext;

    protected UPIDataContext DbContext
    {
        get
        {
            if (_dataContext == null)
            {
                _dataContext = new UPIDataContext();
            }
            return _dataContext;
        }
    }
    public override void Dispose()
    {
        if (_dataContext != null)
        {
            _dataContext.Dispose();
        }
        base.Dispose();
    }
    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = salesRepo.GetAllViewSales();
    }

    #region Load data to combobox
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
        var region = (int.Parse(ddlGroup.SelectedValue) > 0) ? regionRepo.GetRegionByGroupId(int.Parse(ddlGroup.SelectedValue)) : regionRepo.GetAll();
        ddlRegion.DataSource = region;
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "Id";
        ddlRegion.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a region", "0");
        ddlRegion.Items.Insert(0, item);
    }
    private void ListArea()
    {
        var area = (ddlRegion.Items.Count > 0) ? areaRepo.GetAreaByRegionId(int.Parse(ddlRegion.SelectedValue)) : areaRepo.GetAll();
        ddlArea.DataSource = area;
        ddlArea.DataTextField = "AreaName";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a area", "0");
        ddlArea.Items.Insert(0, item);
    }
    private void ListLocal()
    {
        var local = (ddlArea.Items.Count > 0) ? localRepo.GetLocalByAreaId(int.Parse(ddlArea.SelectedValue)) : localRepo.GetAll();
        ddlLocal.DataSource = local;
        ddlLocal.DataTextField = "LocalName";
        ddlLocal.DataValueField = "Id";
        ddlLocal.DataBind();
        RadComboBoxItem item = new RadComboBoxItem("Select a local", "0");
        ddlLocal.Items.Insert(0, item);
    }

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
    public void GetFilterData()
    {
        Utility U = new Utility();
        DataTable dt = new DataTable();
        int GroupId = int.Parse(ddlGroup.SelectedValue);
        int RegionId = (ddlRegion.Items.Count > 0) ? int.Parse(ddlRegion.SelectedValue) : 0;
        int AreaId = (ddlArea.Items.Count > 0) ? int.Parse(ddlArea.SelectedValue) : 0;
        int LocalId = (ddlLocal.Items.Count > 0) ? int.Parse(ddlLocal.SelectedValue) : 0;

        string sql = string.Empty;
        sql = "select s.*,r.RoleName from salesmen s left join salesgroup sg on s.Id=sg.salesmenId left join salesregion sr on s.Id=sr.SalesmenId";
        sql += " left join salesArea sa on s.Id=sa.salesmenId left join salesLocal sl on s.Id=sl.SalesmenId left join Role r on s.RoleId=r.Id where s.Id>0 ";
        if (GroupId > 0)
            sql += " and sg.GroupId=" + GroupId;
        if (RegionId > 0)
            sql += " and sr.RegionId=" + RegionId;
        if (AreaId > 0)
            sql += " and sa.AreaId=" + AreaId;
        if (LocalId > 0)
            sql += " and sl.LocalId=" + LocalId;
        if(txtFullName.Text.Trim()!="")
            sql += " and s.FullName like '%" + txtFullName.Text.Trim()+"%'";
        if (txtPhoneNumber.Text.Trim() != "")
            sql += " and s.Phone like '%" + txtPhoneNumber.Text.Trim() + "%'";
        if (txtRoleName.Text.Trim() != "")
            sql += " and r.RoleName like '%" + txtRoleName.Text.Trim() + "%'";

        dt = U.GetList(sql);
        RadGrid1.DataSource = null;
        RadGrid1.DataSource = dt;
        RadGrid1.Rebind();
    }
    #endregion

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);

        var SalesmenId = (int)editableItem.GetDataKeyValue("Id");
        var Salesmen = salesRepo.GetById(SalesmenId);
        if (Salesmen != null)
        {
            if (checkValid.phoneFormat((string)values["Phone"]))
            {
                DateTime ExpiredDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("ExpiredDate")).SelectedDate.Value.Date);
                salesRepo.Edit(SalesmenId, (string)values["UpiCode"], (string)values["FullName"], (string)values["Phone"],
                    Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlRoles")).SelectedValue), int.Parse(((RadNumericTextBox)gdItem.FindControl("txtSmsQuota")).ToString()), ExpiredDate);
            }
            else
                ShowErrorMessage("Phone number is not valid");
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
            if (checkValid.phoneFormat((string)values["Phone"]))
            {
                DateTime ExpiredDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtExpiredDate")).SelectedDate.Value.Date);
                int role = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlRoles")).SelectedValue);
                int SmsQuota = Convert.ToInt32(((RadNumericTextBox)gdItem.FindControl("txtSmsQuota")).Text);

                string groupVal = ((RadComboBox) gdItem.FindControl("ddlGroupAddNew")).SelectedValue;
                int groupId = Convert.ToInt32(string.IsNullOrEmpty(groupVal) ? "0": groupVal);
                string regionVal = ((RadComboBox)gdItem.FindControl("ddlRegionAddNew")).SelectedValue;
                int regionId = Convert.ToInt32(string.IsNullOrEmpty(regionVal) ? "0" : regionVal);
                string areaVal = ((RadComboBox)gdItem.FindControl("ddlAreaAddNew")).SelectedValue;
                int areaId = Convert.ToInt32(string.IsNullOrEmpty(areaVal) ? "0" : areaVal);
                string localVal = ((RadComboBox)gdItem.FindControl("ddlLocalAddNew")).SelectedValue;
                int localId = Convert.ToInt32(string.IsNullOrEmpty(localVal) ? "0" : localVal);

                salesRepo.Add((string)values["UpiCode"], (string)values["FullName"], (string)values["Phone"], role, SmsQuota, ExpiredDate, 
                    groupId, regionId, areaId, localId);
            }
            else
                ShowErrorMessage("Phone number is not valid");
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
        RadGrid1.Rebind();
    }
    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var SalesmenId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        var Salesmen = salesRepo.GetById(SalesmenId);
        if (Salesmen != null)
        {
            try
            {
                salesRepo.Delete(SalesmenId);
            }
            catch (System.Exception ex)
            {
                ShowErrorMessage(ex.Message);
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
            int roleId = Convert.ToInt32((string)values["roleId"]);
            var RolesList = from r in DbContext.Roles select new { roleId = r.Id, RoleName = r.RoleName };
            if (RolesList.Count() > 0)
            {
                RadComboBox ddlRoles = ((RadComboBox)edititem.FindControl("ddlRoles"));
                ddlRoles.DataSource = RolesList.ToList();
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "roleId";
                ddlRoles.DataBind();
                ddlRoles.SelectedValue = roleId.ToString();
            }

            // Get group
            var group = groupRepo.GetAll();
            if (group != null && group.Count > 0)
            {
                var ddlGroupAddNew = ((RadComboBox)edititem.FindControl("ddlGroupAddNew"));
                if(ddlGroupAddNew != null)
                {
                    var newGroup = new Group {Id = 0, GroupName = "Select a group"};
                    group.Insert(0, newGroup);
                    ddlGroupAddNew.DataSource = group;
                    ddlGroupAddNew.DataTextField = "GroupName";
                    ddlGroupAddNew.DataValueField = "Id";
                    ddlGroupAddNew.DataBind();
                }
            }
        }
    }

    protected void ddlGroupAddNew_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;

        var regionCombo = ((RadComboBox)editedItem.FindControl("ddlRegionAddNew"));
        if(regionCombo != null && e.Value != "0")
        {
            var region = regionRepo.GetRegionByGroupId(int.Parse(e.Value));
            if (region != null)
            {
                regionCombo.DataSource = region;
                regionCombo.DataTextField = "RegionName";
                regionCombo.DataValueField = "Id";
                regionCombo.DataBind();

                RadComboBoxItem item = new RadComboBoxItem("Select a region", "0");
                regionCombo.Items.Insert(0, item);
            }
        }

        var areaCombo = ((RadComboBox)editedItem.FindControl("ddlAreaAddNew"));
        if(areaCombo != null)
        {
            areaCombo.Items.Clear();
            //ResetComboBox(areaCombo);
        }

        var localCombo = ((RadComboBox)editedItem.FindControl("ddlLocalAddNew"));
        if (localCombo != null)
        {
            localCombo.Items.Clear();
            //ResetComboBox(localCombo);
        }
    }

    protected void ddlRegionAddNew_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;

        var areaCombo = ((RadComboBox)editedItem.FindControl("ddlAreaAddNew"));
        if (areaCombo != null && e.Value != "0")
        {
            var area = areaRepo.GetAreaByRegionId(int.Parse(e.Value));
            if (area != null)
            {
                areaCombo.DataSource = area;
                areaCombo.DataTextField = "AreaName";
                areaCombo.DataValueField = "Id";
                areaCombo.DataBind();

                RadComboBoxItem item = new RadComboBoxItem("Select a area", "0");
                areaCombo.Items.Insert(0, item);
            }
        }

        var localCombo = ((RadComboBox)editedItem.FindControl("ddlLocalAddNew"));
        if (localCombo != null)
        {
            localCombo.Items.Clear();
        }
    }

    protected void ddlAreaAddNew_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;

        var localCombo = ((RadComboBox)editedItem.FindControl("ddlLocalAddNew"));
        if (localCombo != null && e.Value != "0")
        {
            var local = localRepo.GetLocalByAreaId(int.Parse(e.Value));
            if (local != null)
            {
                localCombo.DataSource = local;
                localCombo.DataTextField = "LocalName";
                localCombo.DataValueField = "Id";
                localCombo.DataBind();

                RadComboBoxItem item = new RadComboBoxItem("Select a local", "0");
                localCombo.Items.Insert(0, item);
            }
        }
    }

    private void ResetComboBox(RadComboBox radComboBox)
    {
        radComboBox.DataSource = null;
        radComboBox.DataBind();
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ddlRegion.Enabled = true;
        ddlArea.Enabled = false;
        ddlLocal.Enabled = false;
        ListRegion();
        RadGrid1.MasterTableView.SortExpressions.Clear();
        RadGrid1.MasterTableView.Rebind();
        GetFilterData();
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ddlArea.Enabled = true;
        ddlLocal.Enabled = false;
        ListArea();
        GetFilterData();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        ddlLocal.Enabled = true;
        ListLocal();
        GetFilterData();
    }
    protected void ddlLocal_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        GetFilterData();
    }
}