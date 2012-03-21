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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            GroupList();
            BindData();
        }
    }

    private void BindData()
    {
        int index = ddlGroup.Items.Count > 0 ? int.Parse(ddlGroup.SelectedValue) : 0;
        ListAllManagers(index);
        ListNotManagers(index);
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        BindData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int change = ListManagers.ClientChanges.Count();
        if (change > 0)
        {
            for (int i = 0; i < change; i++)
            {
                SalesmanRepo.UpdateSaleGroup(int.Parse(ListManagers.ClientChanges[i].Item.Value), int.Parse(ddlGroup.SelectedValue));
            }
        }       
    }

    private void ListAllManagers(int gid)
    {
        ListManagers.DataSource = SalesmanRepo.GetSaleGroup(gid, true);
        ListManagers.DataTextField = "FullName";
        ListManagers.DataValueField = "Id";
        ListManagers.DataBind();
    }

    private void ListNotManagers(int gid)
    {
        ListSalemens.DataSource = SalesmanRepo.GetSaleGroup(gid, false);
        ListSalemens.DataTextField = "FullName";
        ListSalemens.DataValueField = "Id";
        ListSalemens.DataBind();
    }    

    private void GroupList()
    {
        ddlGroup.DataSource = GroupRepo.GetAll();
        ddlGroup.DataTextField = "GroupName";
        ddlGroup.DataValueField = "Id";
        ddlGroup.DataBind();
    }
}