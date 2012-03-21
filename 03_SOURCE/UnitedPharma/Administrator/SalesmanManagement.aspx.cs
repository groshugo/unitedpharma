using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SalesmanManagement : System.Web.UI.Page
{
    SalesmanRepository salesRepo = new SalesmanRepository();
    ValidationFields checkValid = new ValidationFields();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
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
                salesRepo.Add((string)values["UpiCode"], (string)values["FullName"], (string)values["Phone"],role,SmsQuota, ExpiredDate);
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
            var RolesList = from r in DbContext.Roles select new { roleId = r.Id, RoleName=r.RoleName };
            if (RolesList.Count() > 0)
            {
                RadComboBox ddlRoles = ((RadComboBox)edititem.FindControl("ddlRoles"));
                ddlRoles.DataSource = RolesList.ToList();
                ddlRoles.DataTextField = "RoleName";
                ddlRoles.DataValueField = "roleId";
                ddlRoles.DataBind();
                ddlRoles.SelectedValue = roleId.ToString();
            }
        }
    }
}