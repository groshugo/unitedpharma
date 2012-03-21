using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SalesmenGroupManagement : System.Web.UI.Page
{
    SalesmanRepository SalesmanRepo = new SalesmanRepository();
    GroupsRepository GroupRepo = new GroupsRepository();
    RegionsRepository RegionRepo = new RegionsRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            GroupList();
            RegionList();
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
        BindData();
    }

    private void BindData()
    {
        int index = ddlRegion.Items.Count > 0 ? int.Parse(ddlRegion.SelectedValue) : 0;
        ListAllManagers(index);
        ListNotManagers(index);
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        RegionList();        
    }
    protected void ddlRegion_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {        
        BindData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ddlRegion.Items.Count > 0)
        {
            int change = ListManagers.ClientChanges.Count();
            if (change > 0)
            {
                for (int i = 0; i < change; i++)
                {
                    SalesmanRepo.UpdateSaleRegion(int.Parse(ListManagers.ClientChanges[i].Item.Value), int.Parse(ddlRegion.SelectedValue));
                }
            }
        }
        else
        {
            ShowMessage("Please choose a Region");
        }
    }

    private void ShowMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "!\")"));
    }

    private void ListAllManagers(int rid)
    {
        ListManagers.DataSource = SalesmanRepo.GetSaleRegion(rid, true);
        ListManagers.DataTextField = "FullName";
        ListManagers.DataValueField = "Id";
        ListManagers.DataBind();
    }

    private void ListNotManagers(int rid)
    {
        ListSalemens.DataSource = SalesmanRepo.GetSaleRegion(rid, false);
        ListSalemens.DataTextField = "FullName";
        ListSalemens.DataValueField = "Id";
        ListSalemens.DataBind();
    }    

    
}