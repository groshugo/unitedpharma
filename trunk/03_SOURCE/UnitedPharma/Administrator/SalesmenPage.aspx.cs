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
    SalesmanRepository sRepo = new SalesmanRepository();
    ValidationFields checkValid = new ValidationFields();
    GroupsRepository groupRepo = new GroupsRepository();
    RegionsRepository regionRepo = new RegionsRepository();
    AreasRepository areaRepo = new AreasRepository();
    LocalsRepository localRepo = new LocalsRepository();

    private const string DataTextFieldName = "Fullname";
    private const string DataValueFieldName = "Id";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mSalesmen");
            ListGroup();

            // Load TROM to combo
            LoadSalesmenToComboTrom();

            // Load EROM to combo
            LoadSalesmenToComboErom();

            // Load EROM2 to combo
            LoadSalesmenToComboErom2();
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
        RadGrid1.DataSource = GetDataForGridCommand();
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

        var groupJoinString = string.Empty;
        var groupWhereString = string.Empty;
        if (GroupId > 0)
        {
            groupJoinString += " left join salesgroup sg on s.Id=sg.salesmenId ";
            groupWhereString= " and sg.GroupId=" + GroupId + " ";
        }

        var regionJoinString = string.Empty;
        var regionWhereString = string.Empty;
        if (RegionId > 0)
        {
            regionJoinString += " left join salesregion sr on s.Id=sr.SalesmenId ";
            regionWhereString = " and sr.RegionId=" + RegionId + " ";
        }

        var areaJoinString = string.Empty;
        var areaWhereString = string.Empty;
        if (AreaId > 0)
        {
            areaJoinString += " left join salesArea sa on s.Id=sa.salesmenId ";
            areaWhereString = " and sa.AreaId=" + AreaId + " ";
        }

        var localJoinString = string.Empty;
        var localWhereString = string.Empty;
        if (LocalId > 0)
        {
            localJoinString += " left join salesLocal sl on s.Id=sl.SalesmenId ";
            localWhereString = " and sl.LocalId=" + LocalId + " ";
        }

        sql = "select s.*,r.RoleName from salesmen s " + groupJoinString + regionJoinString + areaJoinString +
              localJoinString + " left join Role r on s.RoleId=r.Id where s.Id > 0 " + groupWhereString +
              regionWhereString + areaWhereString + localWhereString;

        if (txtFullName.Text.Trim() != "")
            sql += " and s.FullName like '%" + txtFullName.Text.Trim() + "%'";
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
        var Salesmen = sRepo.GetById(SalesmenId);
        if (Salesmen != null)
        {
            try
            {
                var upiCode = values["UpiCode"] as string;
                var phoneNumber = values["Phone"] as string;
                var fullName = values["FullName"] as string;

                if (phoneNumber == null || string.IsNullOrEmpty(phoneNumber.Trim()) ||
                    upiCode == null || string.IsNullOrEmpty(upiCode.Trim()) ||
                    fullName == null || string.IsNullOrEmpty(fullName.Trim()))
                {
                    ShowErrorMessage("Full name, UPI Code and Phone number are required fields");
                    e.Canceled = true;
                    return;
                }

                if (checkValid.phoneFormat(phoneNumber))
                {
                    var expiredDate = DateTime.Now.AddDays(7);

                    var txtExpiredDate = (RadDatePicker)gdItem.FindControl("txtExpiredDate");
                    if (txtExpiredDate != null)
                    {
                        if (txtExpiredDate.SelectedDate != null) expiredDate = txtExpiredDate.SelectedDate.Value;
                    }

                    var role = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlRoles")).SelectedValue);

                    var smsQuota = 0;
                    var txtSmsQuota = (RadNumericTextBox)gdItem.FindControl("txtSmsQuota");
                    if (txtSmsQuota != null && !string.IsNullOrEmpty(txtSmsQuota.Text.Trim()))
                    {
                        smsQuota = int.Parse(txtSmsQuota.Text);
                    }

                   
                    if (UtilitiesHelpers.Instance.IsRepRole(role))
                    {
                        var groupVal = ((RadComboBox)gdItem.FindControl("ddlGroupAddNew")).SelectedValue;
                        var groupId = Convert.ToInt32(string.IsNullOrEmpty(groupVal) ? "0" : groupVal);

                        var regionVal = ((RadComboBox)gdItem.FindControl("ddlRegionAddNew")).SelectedValue;
                        var regionId = Convert.ToInt32(string.IsNullOrEmpty(regionVal) ? "0" : regionVal);

                        var areaVal = ((RadComboBox)gdItem.FindControl("ddlAreaAddNew")).SelectedValue;
                        var areaId = Convert.ToInt32(string.IsNullOrEmpty(areaVal) ? "0" : areaVal);

                        var localVal = ((RadComboBox)gdItem.FindControl("ddlLocalAddNew")).SelectedValue;
                        var localId = Convert.ToInt32(string.IsNullOrEmpty(localVal) ? "0" : localVal);

                        var result = sRepo.Edit(SalesmenId, upiCode, fullName, phoneNumber, role, smsQuota, expiredDate,
                            groupId, regionId, areaId, localId);
                        if (!result)
                        {
                            ShowErrorMessage("Phone number and UPI Code are unique, please choose another one.");
                            e.Canceled = true;
                        }
                    }
                    else
                    {
                        var result = sRepo.Edit(SalesmenId, upiCode, fullName, phoneNumber, role, smsQuota, expiredDate,
                            0, 0, 0, 0);
                        if (!result)
                        {
                            ShowErrorMessage("Phone number and UPI Code are unique, please choose another one.");
                            e.Canceled = true;
                        }
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
            var upiCode = values["UpiCode"] as string;
            var phoneNumber = values["Phone"] as string;
            var fullName = values["FullName"] as string;

            if(phoneNumber == null || string.IsNullOrEmpty(phoneNumber.Trim()) ||
                upiCode == null || string.IsNullOrEmpty(upiCode.Trim()) ||
                fullName == null || string.IsNullOrEmpty(fullName.Trim()))
            {
                ShowErrorMessage("Full name, UPI Code and Phone number are required fields");
                e.Canceled = true;
                return;
            }

            if (checkValid.phoneFormat(phoneNumber))
            {
                var expiredDate = DateTime.Now.AddDays(7);

                var txtExpiredDate = (RadDatePicker) gdItem.FindControl("txtExpiredDate");
                if(txtExpiredDate != null)
                {
                    if (txtExpiredDate.SelectedDate != null) expiredDate = txtExpiredDate.SelectedDate.Value;
                }
                
                var role = Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlRoles")).SelectedValue);

                var smsQuota = 0;
                var txtSmsQuota = (RadNumericTextBox) gdItem.FindControl("txtSmsQuota");
                if (txtSmsQuota != null && !string.IsNullOrEmpty(txtSmsQuota.Text.Trim()))
                {
                    smsQuota = int.Parse(txtSmsQuota.Text);
                }

                if (UtilitiesHelpers.Instance.IsRepRole(role))
                {
                    var groupVal = ((RadComboBox)gdItem.FindControl("ddlGroupAddNew")).SelectedValue;
                    var groupId = Convert.ToInt32(string.IsNullOrEmpty(groupVal) ? "0" : groupVal);

                    var regionVal = ((RadComboBox)gdItem.FindControl("ddlRegionAddNew")).SelectedValue;
                    var regionId = Convert.ToInt32(string.IsNullOrEmpty(regionVal) ? "0" : regionVal);
                    
                    var areaVal = ((RadComboBox)gdItem.FindControl("ddlAreaAddNew")).SelectedValue;
                    var areaId = Convert.ToInt32(string.IsNullOrEmpty(areaVal) ? "0" : areaVal);
                    
                    var localVal = ((RadComboBox)gdItem.FindControl("ddlLocalAddNew")).SelectedValue;
                    var localId = Convert.ToInt32(string.IsNullOrEmpty(localVal) ? "0" : localVal);

                    var result  = sRepo.Add(upiCode, fullName, phoneNumber, role, smsQuota, expiredDate,
                        groupId, regionId, areaId, localId);
                    if(!result)
                    {
                        ShowErrorMessage("Phone number and UPI Code are unique, please choose another one.");
                        e.Canceled = true;
                    }
                }
                else
                {
                    var result = sRepo.Add(upiCode, fullName, phoneNumber, role, smsQuota, expiredDate,
                        0, 0, 0, 0);
                    if (!result)
                    {
                        ShowErrorMessage("Phone number and UPI Code are unique, please choose another one.");
                        e.Canceled = true;
                    }
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
        //RadGrid1.Rebind();
    }
    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var SalesmenId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        var Salesmen = sRepo.GetById(SalesmenId);
        if (Salesmen != null)
        {
            try
            {
                sRepo.Delete(SalesmenId);
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

            int roleId = 0;

            var vwSalesmen = edititem.DataItem as vwSalemen;
            if (vwSalesmen != null)
            {
                roleId = vwSalesmen.RoleId.HasValue ? vwSalesmen.RoleId.Value : 0;
            }

            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);

            var RolesList = from r in DbContext.Roles select new { roleId = r.Id, RoleName = r.RoleName };
            if (RolesList.Count() > 0)
            {
                RadComboBox ddlRoles = ((RadComboBox)edititem.FindControl("ddlRoles"));
                ddlRoles.DataSource = RolesList.ToList();
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "roleId";
                ddlRoles.DataBind();

                RadComboBoxItem item = new RadComboBoxItem("Select a Role", "0");
                ddlRoles.Items.Insert(0, item);

                ddlRoles.SelectedValue = roleId.ToString();
            }

            if (UtilitiesHelpers.Instance.IsRepRole(roleId))
            {
                // Get group
                var group = groupRepo.GetAll();
                if (group != null && group.Count > 0)
                {
                    var ddlGroupAddNew = ((RadComboBox)edititem.FindControl("ddlGroupAddNew"));
                    if (ddlGroupAddNew != null)
                    {
                        var newGroup = new Group { Id = 0, GroupName = "Select a group" };
                        group.Insert(0, newGroup);
                        ddlGroupAddNew.DataSource = group;
                        ddlGroupAddNew.DataTextField = "GroupName";
                        ddlGroupAddNew.DataValueField = "Id";
                        ddlGroupAddNew.DataBind();

                        //
                        if (vwSalesmen != null)
                        {
                            var salesmenId = vwSalesmen.Id;

                            var sql = string.Format("Select distinct GroupId  FROM [UPI].[dbo].[SalesGroup] where SalesmenId={0}",
                                                    salesmenId);
                            var utils = new Utility();
                            var groupDt = utils.GetList(sql);
                            if (groupDt != null && groupDt.Rows.Count == 1)
                            {
                                var groupId = groupDt.Rows[0]["GroupId"].ToString();
                                ddlGroupAddNew.SelectedValue = groupId;

                                var region = regionRepo.GetRegionByGroupId(int.Parse(groupId));
                                var ddlRegionAddNew = ((RadComboBox)edititem.FindControl("ddlRegionAddNew"));
                                if (region != null && ddlRegionAddNew != null)
                                {
                                    ddlRegionAddNew.DataSource = region;
                                    ddlRegionAddNew.DataTextField = "RegionName";
                                    ddlRegionAddNew.DataValueField = "Id";
                                    ddlRegionAddNew.DataBind();

                                    sql = string.Format("Select distinct RegionId  FROM [Salesregion] where SalesmenId={0}",
                                                    salesmenId);
                                    var regionDt = utils.GetList(sql);
                                    if (regionDt != null && regionDt.Rows.Count >0)
                                    {
                                        var regionId = regionDt.Rows[0]["RegionId"].ToString();
                                        ddlRegionAddNew.SelectedValue = regionId;

                                        var area = areaRepo.GetAreaByRegionId(int.Parse(regionId));
                                        var ddlAreaAddNew = ((RadComboBox)edititem.FindControl("ddlAreaAddNew"));
                                        if (area != null && ddlAreaAddNew != null)
                                        {
                                            ddlAreaAddNew.DataSource = area;
                                            ddlAreaAddNew.DataTextField = "AreaName";
                                            ddlAreaAddNew.DataValueField = "Id";
                                            ddlAreaAddNew.DataBind();

                                            sql = string.Format("Select distinct AreaId  FROM [SalesArea] where SalesmenId={0}",
                                                            salesmenId);
                                            var areaDt = utils.GetList(sql);
                                            if (areaDt != null && areaDt.Rows.Count > 0)
                                            {
                                                var areaId = areaDt.Rows[0]["AreaId"].ToString();
                                                ddlAreaAddNew.SelectedValue = areaId;

                                                var local = localRepo.GetLocalByAreaId(int.Parse(areaId));
                                                var ddlLocalAddNew = ((RadComboBox)edititem.FindControl("ddlLocalAddNew"));
                                                if (local != null && ddlLocalAddNew != null)
                                                {
                                                    ddlLocalAddNew.DataSource = local;
                                                    ddlLocalAddNew.DataTextField = "LocalName";
                                                    ddlLocalAddNew.DataValueField = "Id";
                                                    ddlLocalAddNew.DataBind();

                                                    sql = string.Format("Select distinct LocalId  FROM [SalesLocal] where SalesmenId={0}",
                                                                    salesmenId);
                                                    var localDt = utils.GetList(sql);
                                                    if (localDt != null && localDt.Rows.Count > 0)
                                                    {
                                                        var localId = localDt.Rows[0]["LocalId"].ToString();
                                                        ddlLocalAddNew.SelectedValue = localId;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                                
                            }
                        }
                    }
                }

                
                

            }
        }
    }

    protected void ddlRoles_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        var roleId = int.Parse(e.Value);
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;

        if (editedItem == null) return;

        if (UtilitiesHelpers.Instance.IsRepRole(roleId))
        {
            var ddlGroupAddNew = ((RadComboBox)editedItem.FindControl("ddlGroupAddNew"));

            // Get group
            var group = groupRepo.GetAll();
            if (group != null && group.Count > 0)
            {
                var newGroup = new Group { Id = 0, GroupName = "Select a group" };
                group.Insert(0, newGroup);
                ddlGroupAddNew.DataSource = group;
                ddlGroupAddNew.DataTextField = "GroupName";
                ddlGroupAddNew.DataValueField = "Id";
                ddlGroupAddNew.DataBind();

                CleanComboAddNew(editedItem, false);
            }
        }
        else
        {
            CleanComboAddNew(editedItem, true);
        }
    }

    private static void CleanComboAddNew(GridEditableItem editedItem, bool clearFirstComboFlag)
    {
        if (clearFirstComboFlag)
        {
            var ddlGroupAddNew = ((RadComboBox)editedItem.FindControl("ddlGroupAddNew"));
            UtilitiesHelpers.Instance.ClearComboData(ddlGroupAddNew);
        }

        var ddlRegionAddNew = ((RadComboBox)editedItem.FindControl("ddlRegionAddNew"));
        UtilitiesHelpers.Instance.ClearComboData(ddlRegionAddNew);

        var ddlAreaAddNew = ((RadComboBox)editedItem.FindControl("ddlAreaAddNew"));
        UtilitiesHelpers.Instance.ClearComboData(ddlAreaAddNew);

        var ddlLocalAddNew = ((RadComboBox)editedItem.FindControl("ddlLocalAddNew"));
        UtilitiesHelpers.Instance.ClearComboData(ddlLocalAddNew);
    }

    protected void ddlGroupAddNew_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;

        var regionCombo = ((RadComboBox)editedItem.FindControl("ddlRegionAddNew"));
        if (regionCombo != null)
        {
            if(e.Value != "0")
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
            else
            {
                regionCombo.Items.Clear();
            }
        }

        var areaCombo = ((RadComboBox)editedItem.FindControl("ddlAreaAddNew"));
        if (areaCombo != null)
        {
            areaCombo.Items.Clear();
        }

        var localCombo = ((RadComboBox)editedItem.FindControl("ddlLocalAddNew"));
        if (localCombo != null)
        {
            localCombo.Items.Clear();
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
    protected void ddlGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if(e.Value == "0")
        {
            ddlRegion.Items.Clear();
            ddlArea.Items.Clear();
            ddlLocal.Items.Clear();
        }
        else
        {
            ddlRegion.Enabled = true;
            ddlArea.Enabled = false;
            ddlLocal.Enabled = false;
            ListRegion();
            RadGrid1.MasterTableView.SortExpressions.Clear();
            RadGrid1.MasterTableView.Rebind();
            GetFilterData();    
        }
        
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (e.Value == "0")
        {
            ddlArea.Items.Clear();
            ddlLocal.Items.Clear();
        }
        else
        {
            ddlArea.Enabled = true;
            ddlLocal.Enabled = false;
            ListArea();
            GetFilterData();
        }
    }
    protected void ddlArea_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (e.Value == "0")
        {
            ddlLocal.Items.Clear();
        }
        else
        {
            ddlLocal.Enabled = true;
            ListLocal();
            GetFilterData();
        }
    }
    protected void ddlLocal_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        GetFilterData();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        GetFilterData();
    }

    protected void cboTROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var trom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (trom != null)
            {
                // Load TPS to grid
                var tps = sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, trom.Id);
                LoadListSalesmenToCombo(tps, cboTPS, "Select a TPS");

                LoadSalesmenToGrid(tps);
            }
        }
        else
        {
            ClearComboData(cboTPS);
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
                // Load TPR to grid
                var tpr = sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPR, tps.Id);
                LoadSalesmenToGrid(tpr);
            }
        }
    }

    protected void cboEROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom != null)
            {
                // Load data to TPS
                var pss1 = sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS1, erom.Id);
                LoadListSalesmenToCombo(pss1, cboPSS1, "Select a PSS1");

                LoadSalesmenToGrid(pss1);
            }
        }
        else
        {
            ClearComboData(cboPSS1);
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
                // Load PSR1 to grid
                var psr1 = sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR1, pss1.Id);
                LoadSalesmenToGrid(psr1);
            }
        }
    }

    protected void cboEROM2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom2 = sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom2 != null)
            {
                // Load data to erom2
                var pss2 = sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS2, erom2.Id);
                LoadListSalesmenToCombo(pss2, cboPSS2, "Select a PSS2");

                LoadSalesmenToGrid(pss2);
            }
        }
        else
        {
            ClearComboData(cboPSS2);
        }

        ResetChannelCombo(SalesChannel.Erom);
    }
    protected void cboPSS2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.Value))
        {
            var pss2 = sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss2 != null)
            {
                // Load data to TPS
                var psr2 = sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR2, pss2.Id);
                LoadSalesmenToGrid(psr2);
            }
        }
    }

    private void ResetChannelCombo(SalesChannel channel)
    {
        switch (channel)
        {
            case SalesChannel.Erom:
                ClearComboData(cboTPS);
                cboTROM.SelectedIndex = -1;

                ClearComboData(cboPSS2);
                cboEROM2.SelectedIndex = -1;
                break;
            case SalesChannel.Erom2:
                ClearComboData(cboTPS);
                cboTROM.SelectedIndex = -1;

                ClearComboData(cboPSS1);
                cboEROM.SelectedIndex = -1;
                break;
            default:
                ClearComboData(cboPSS1);
                cboEROM.SelectedIndex = -1;

                ClearComboData(cboPSS2);
                cboEROM2.SelectedIndex = -1;
                break;
        }

    }

    private void LoadSalesmenToComboTrom()
    {
        LoadSalesmenToCombo(SalesmenRole.TROM, cboTROM, "Select a TROM");
    }
    private void LoadSalesmenToComboErom2()
    {
        LoadSalesmenToCombo(SalesmenRole.EROM2, cboEROM2, "Select a EROM2");
    }
    private void LoadSalesmenToComboErom()
    {
        LoadSalesmenToCombo(SalesmenRole.EROM, cboEROM, "Select a EROM");
    }

    private void LoadSalesmenToCombo(SalesmenRole role, RadComboBox cbo, string firstItemText)
    {
        var salesmens = sRepo.GetViewWebSalesmenByRoleId((int)role);
        if (salesmens != null)
        {
            LoadListSalesmenToCombo(salesmens, cbo, firstItemText);
        }
        else
        {
            cbo.Enabled = false;
            cbo.Enabled = false;
            // write log here
        }
    }
    private void LoadListSalesmenToCombo(List<vwSalemen> trom, RadComboBox comboBox, string firstItemText)
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

    private void ClearComboData(RadComboBox comboBox)
    {
        if (comboBox.Items != null && comboBox.Items.Count > 0)
        {
            comboBox.Items.Clear();
        }
    }
    private void LoadSalesmenToGrid(List<vwSalemen> vwSalemens)
    {
        RadGrid1.DataSource = vwSalemens;
        RadGrid1.DataBind();
    }

    private List<vwSalemen> GetDataForGridCommand()
    {
        // POS
        var tromId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboTROM);
        if (tromId > 0)
        {
            var tpsId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboTPS);
            return tpsId > 0 ? sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPR, tpsId)
                : sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, tromId);
        }
        else
        {
            // POC
            var eromId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboEROM);
            if (eromId > 0)
            {
                var pss1Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSS1);
                return pss1Id > 0 ? sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR1, pss1Id)
                    : sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS1, eromId);
            }
            else
            {
                var erom2Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboEROM2);
                if (erom2Id > 0)
                {
                    var pss2Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSS2);
                    return pss2Id > 0 ? sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR2, pss2Id)
                        : sRepo.GetViewWebSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS2, erom2Id);
                }
                return sRepo.GetAllViewSales();
            }
        }
    }
}