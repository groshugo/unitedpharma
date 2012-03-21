using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_AdministratorManagement : System.Web.UI.Page
{
    AdministratorRepository repo = new AdministratorRepository();
    ValidationFields checkValid = new ValidationFields();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = repo.GetAll();
            RadGrid1.DataBind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAll(); ;
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        var id = (int)editableItem.GetDataKeyValue("Id");
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            if (checkValid.phoneFormat((string)values["Phone"]))
                repo.Edit(id, (string)values["UpiCode"], (string)values["Fullname"], (string)values["Password"], (string)values["Phone"]);
            else
                ShowErrorMessage("Phone number not valid");
        }
        catch (Exception ex)
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
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            if (checkValid.phoneFormat((string)values["Phone"]))
                repo.Add((string)values["UpiCode"], (string)values["Fullname"], (string)values["Password"], (string)values["Phone"]);
            else
                ShowErrorMessage("Phone number not valid");
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(id);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }

    }
}