using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SalesmenLocalManagement : System.Web.UI.Page
{
    SalesmanRepository SalesmanRepo = new SalesmanRepository();
    GroupsRepository GroupRepo = new GroupsRepository();
    RegionsRepository RegionRepo = new RegionsRepository();
    AreasRepository AreaRepo = new AreasRepository();
    LocalsRepository LocalRepo = new LocalsRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            GroupList();
            RegionList();
            AreaList();
            LocalList();
            BindData();
        }
    }        

    private void GroupList()
    {
        ddlGroup.DataSource = GroupRepo.GetAll();
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "Id";
        ddlGroup.DataBind();
    }

    private void RegionList()
    {
        int index = ddlGroup.Items.Count > 0 ? int.Parse(ddlGroup.SelectedValue) : 0;
        ddlRegion.DataSource = RegionRepo.GetRegionByGroupId(index);
        ddlRegion.DataTextField = "RegionName";
        ddlRegion.DataValueField = "Id";
        ddlRegion.DataBind();
        AreaList();
    }

    private void AreaList()
    {
        int index = ddlRegion.Items.Count > 0 ? int.Parse(ddlRegion.SelectedValue) : 0;
        ddlArea.DataSource = AreaRepo.GetAreaByRegionId(index);
        ddlArea.DataTextField = "AreaName";
        ddlArea.DataValueField = "Id";
        ddlArea.DataBind();
        LocalList();
    }

    private void LocalList()
    {
        int index = ddlArea.Items.Count > 0 ? int.Parse(ddlArea.SelectedValue) : 0;
        ddlLocal.DataSource = LocalRepo.GetLocalByAreaId(index);
        ddlLocal.DataTextField = "LocalName";
        ddlLocal.DataValueField = "Id";
        ddlLocal.DataBind();
        BindData();
    }

    private void BindData()
    {
        int index = ddlLocal.Items.Count > 0 ? int.Parse(ddlLocal.SelectedValue) : 0;
        ListAllManagers(index);
        ListNotManagers(index);
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RegionList();        
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {        
        AreaList();
    }
    protected void ddlArea_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LocalList();
    }
    protected void ddlLocal_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlLocal.Items.Count > 0)
        {
            int change = ListManagers.ClientChanges.Count();
            if (change > 0)
            {
                for (int i = 0; i < change; i++)
                {
                    SalesmanRepo.UpdateSaleLocal(int.Parse(ListManagers.ClientChanges[i].Item.Value), int.Parse(ddlLocal.SelectedValue));
                }
            }
        }
        else
        {
            ShowMessage("Please choose a Local");
        }
    }

    private void ShowMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }

    private void ListAllManagers(int lid)
    {
        ListManagers.DataSource = SalesmanRepo.GetSaleLocal(lid, true);
        ListManagers.DataTextField = "FullName";
        ListManagers.DataValueField = "Id";
        ListManagers.DataBind();
    }

    private void ListNotManagers(int lid)
    {
        ListSalemens.DataSource = SalesmanRepo.GetSaleLocal(lid, false);
        ListSalemens.DataTextField = "FullName";
        ListSalemens.DataValueField = "Id";
        ListSalemens.DataBind();
    }    

    
}