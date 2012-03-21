using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SupervisorCustomerManagement : System.Web.UI.Page
{
    SupervisorManageCustomerRepository repo = new SupervisorManageCustomerRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAllView();
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var Id = (int)editableItem.GetDataKeyValue("Id");

        try
        {
            repo.Edit(Id, Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomer")).SelectedValue), Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlSupervisor")).SelectedValue));

        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }

    }

    private void ShowErrorMessage(string message)
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"" + message + "\")"));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            repo.Add(Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlCustomer")).SelectedValue), Convert.ToInt32(((RadComboBox)gdItem.FindControl("ddlSupervisor")).SelectedValue));

        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var Id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(Id);
        }
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }

    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if ((e.Item is GridEditableItem) && (e.Item.IsInEditMode))
        {
            GridEditableItem edititem = (GridEditableItem)e.Item;
            Hashtable values = new Hashtable();
            edititem.ExtractValues(values);

            using (UPIDataContext db = new UPIDataContext())
            {
                string supervisorIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfSupervisor")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfSupervisor")).Value;
                //Supervisor
                RadComboBox ddlSupervisor = ((RadComboBox)edititem.FindControl("ddlSupervisor"));
                ddlSupervisor.DataSource = db.CustomerSupervisors;
                ddlSupervisor.DataTextField = "FullName";
                ddlSupervisor.DataValueField = "Id";
                ddlSupervisor.DataBind();
                ddlSupervisor.SelectedValue = supervisorIndex;
                

                string custTypeIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfCustomerType")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfCustomerType")).Value;
                string custIndex = String.IsNullOrEmpty(((HiddenField)edititem.FindControl("hdfCustomer")).Value) ? "0" : ((HiddenField)edititem.FindControl("hdfCustomer")).Value;

                //CustomerType                
                RadComboBox ddlCustomerType = ((RadComboBox)edititem.FindControl("ddlCustomerType"));
                ddlCustomerType.DataSource = db.CustomerTypes;
                ddlCustomerType.DataTextField = "TypeName";
                ddlCustomerType.DataValueField = "Id";
                ddlCustomerType.DataBind();
                ddlCustomerType.SelectedValue = custTypeIndex;

                //Customer
                RadComboBox ddlCustomers = ((RadComboBox)edititem.FindControl("ddlCustomer"));
                ddlCustomers.DataSource = (from c in db.Customers where c.CustomerTypeId == (Convert.ToInt32(ddlCustomerType.SelectedValue)) select c);
                ddlCustomers.DataTextField = "FullName";
                ddlCustomers.DataValueField = "Id";
                ddlCustomers.DataBind();
                ddlCustomers.SelectedValue = custIndex;
            }
        }
    }    

    protected void ddlCustomerType_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        using (UPIDataContext db = new UPIDataContext())
        {
            GridEditableItem editedItem = (o as RadComboBox).NamingContainer as GridEditableItem;
            RadComboBox ddlCustomers = editedItem["CustomerColumn"].FindControl("ddlCustomer") as RadComboBox;
            ddlCustomers.DataSource = (from d in db.Customers where d.CustomerTypeId == (Convert.ToInt32(e.Value)) select d);
            ddlCustomers.DataTextField = "FullName";
            ddlCustomers.DataValueField = "Id";
            ddlCustomers.DataBind();
        }
    }
}