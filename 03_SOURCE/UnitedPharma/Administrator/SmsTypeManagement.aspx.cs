using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_SmsTypeManagement : System.Web.UI.Page
{
    SmsTypeRepository repo = new SmsTypeRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            RadGrid1.DataSource = repo.GetAll();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAll();
    }
    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        int id = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            repo.Edit(id, (string)values["Name"], (string)values["Syntax"]);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }

    }

    private void ShowErrorMessage()
    {
        RadAjaxManager1.ResponseScripts.Add(string.Format("window.radalert(\"Please enter valid data!\")"));
    }

    protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    {
        var editableItem = ((GridEditableItem)e.Item);
        SmsType S = new SmsType();
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            repo.Add((string)values["Name"], (string)values["Syntax"]);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");

        try
        {
            repo.Delete(id);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }

    }
}