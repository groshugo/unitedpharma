using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;

public partial class Administrator_GroupManagement : System.Web.UI.Page
{
    GroupsRepository repo = new GroupsRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");
            BindGrid();
        }
    }

    private void BindGrid()
    {
        RadGrid1.DataSource = repo.GetAll();
        RadGrid1.DataBind();
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {
        RadGrid1.DataSource = repo.GetAll();
    }
    protected void RadGrid1_CreateColumnEditor(object sender, GridCreateColumnEditorEventArgs e)
    {
        if (e.Column is GridBoundColumn)
        {
            if (e.Column.UniqueName == "Description")
            {
                e.ColumnEditor = new MultiLineTextBoxColumnEditor();
            }
        }
    }
    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);

        int id = (int)editableItem.GetDataKeyValue("Id");
        try
        {
            repo.Edit(id, (string)values["UpiCode"], (string)values["GroupName"], (string)values["Description"]);
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
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
        Salesmen S = new Salesmen();
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            repo.Add((string)values["UpiCode"], (string)values["GroupName"], (string)values["Description"]);
        }
        catch (System.Exception ex)
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
        catch (System.Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }
}