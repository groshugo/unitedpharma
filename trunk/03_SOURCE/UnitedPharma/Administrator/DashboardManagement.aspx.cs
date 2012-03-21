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

public partial class Administrator_DashboardManagement : System.Web.UI.Page
{
    DashboardRepository dRepo = new DashboardRepository();
    SalesmanRepository sRepo = new SalesmanRepository();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mDashboard");
            LoadListSalesmen();
            LoadGridDashboard(); 
        }
    }

    private void LoadListSalesmen()
    {
        cbSalesmen.DataSource = sRepo.GetAll();
        cbSalesmen.DataTextField = "FullName";
        cbSalesmen.DataValueField = "Phone";
        cbSalesmen.DataBind();
    }

    private void LoadGridDashboard()
    {
        string ReceiverPhoneNumber = cbSalesmen.Items.Count > 0 ? cbSalesmen.SelectedValue : "";
        RadGrid1.DataSource = dRepo.GetAllForSalemens(ReceiverPhoneNumber);
        RadGrid1.DataBind();
    }    

    protected void cbSalesmen_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        LoadGridDashboard();
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
        string ReceiverPhoneNumber = cbSalesmen.Items.Count > 0 ? cbSalesmen.SelectedValue : "";
        RadGrid1.DataSource = dRepo.GetAllForSalemens(ReceiverPhoneNumber);
    }
    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            GridPagerItem gridPager = e.Item as GridPagerItem;
            Control numericPagerControl = gridPager.GetNumericPager();
            Control placeHolder = gridPager.FindControl("NumericPagerPlaceHolder");
            placeHolder.Controls.Add(numericPagerControl);
        }
    }
    protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
    {
        GridEditFormItem gdItem = (e.Item as GridEditFormItem);
        var editableItem = ((GridEditableItem)e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var id = (int)editableItem.GetDataKeyValue("ReceiverPhoneNumber");
        try
        {   
            dRepo.Edit(id, (string)values["Title"], (string)values["Content"]);
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

    //protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
    //{
    //    GridEditFormItem gdItem = (e.Item as GridEditFormItem);
    //    var editableItem = ((GridEditableItem)e.Item);
    //    Hashtable values = new Hashtable();
    //    editableItem.ExtractValues(values);
    //    try
    //    {            
    //        dRepo.Add((string)values["Title"], (string)values["Content"], Convert.ToInt32(cbSalesmen.SelectedValue));
    //    }
    //    catch (System.Exception)
    //    {
    //        ShowErrorMessage();
    //    }
    //}

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int)((GridDataItem)e.Item).GetDataKeyValue("Id");
        try
        {
            dRepo.Delete(id);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }

    }
}