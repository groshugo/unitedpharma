using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections;
using System.Drawing;
using System.Globalization;

public partial class Administrator_PromotionManagement : System.Web.UI.Page
{
    PromotionRepository repo = new PromotionRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mAdministrator");                   
            RadGrid1.DataSource = repo.GetAll();
        }
    }

    protected void RadGrid1_CreateColumnEditor(object sender, GridCreateColumnEditorEventArgs e)
    {
        if (e.Column is GridBoundColumn)
        {
            if (e.Column.UniqueName == "Content")
            {
                e.ColumnEditor = new MultiLineTextBoxColumnEditor();
            }
        }
    }

    protected void RadGrid1_PreRender(object sender, System.EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.RadGrid1.EditIndexes.Add(1);
            this.RadGrid1.MasterTableView.Rebind();
        }
    }

    protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
    {       
        RadGrid1.DataSource = repo.GetAll();
    }

    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var PromotionId = (int)editableItem.GetDataKeyValue("Id");
        try
        {

            DateTime startdate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtStartDate")).SelectedDate.Value.Date);
            DateTime endDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtEndtDate")).SelectedDate.Value.Date);
            repo.Update(PromotionId, (string)values["UpiCode"], (string)values["Title"], (string)values["Content"], startdate, endDate);
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
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        try
        {
            DateTime startdate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtStartDate")).SelectedDate.Value.Date);
            DateTime endDate = Convert.ToDateTime(((RadDatePicker)gdItem.FindControl("txtEndtDate")).SelectedDate.Value.Date);
            ObjLogin adm = (ObjLogin)Session["objLogin"];
            repo.Insert((string)values["UpiCode"], (string)values["Title"], (string)values["Content"], startdate, endDate, adm.Id);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }
    }

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var PromotionId = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            repo.Delete(PromotionId);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }

    }

    protected void btnSchedule_Click(object sender, EventArgs e)
    {
        Response.Redirect("SchedulePromotionManagement.aspx");
    }
}