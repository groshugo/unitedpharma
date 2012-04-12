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

    private const string DataTextFieldName = "Fullname";
    private const string DataValueFieldName = "Id";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mDashboard");

            //LoadListSalesmen();

            // Load TROM to combo
            LoadSalesmenToComboTROM();
        }
    }

    //private void LoadListSalesmen()
    //{
    //    cbSalesmen.DataSource = sRepo.GetAll();
    //    cbSalesmen.DataTextField = "FullName";
    //    cbSalesmen.DataValueField = "Phone";
    //    cbSalesmen.DataBind();
    //}

    private void LoadSalesmenToComboTROM()
    {
        var trom = sRepo.GetSalesmenByRoleId((int)SalesmenRole.TROM);
        if (trom == null)
        {
            cboTPS.Enabled = false;
            cboTPR.Enabled = false;
            // write log here
        }
        else
        {
            cboTROM.DataSource = trom;
            cboTROM.DataTextField = DataTextFieldName;
            cboTROM.DataValueField = DataValueFieldName;
            cboTROM.DataBind();

            // Load default value for TPS
            int id = trom[0].Id;
            var tps = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, id);
            if (tps != null)
            {
                cboTPS.DataSource = tps;
                cboTPS.DataTextField = DataTextFieldName;
                cboTPS.DataValueField = DataValueFieldName;
                cboTPS.DataBind();

                if(tps.Count > 0)
                {
                    var item = new RadComboBoxItem("Select a TPS", "0");
                    cboTPS.Items.Insert(0, item);
                }
            }

            // Load data for Dashboard grid
            var phone = trom[0].Phone;
            LoadGridDashboard(phone);
        }
    }

    private void LoadGridDashboard(string phone)
    {
        //RadGrid1.DataSource = null;
        RadGrid1.DataSource = dRepo.GetAllForSalemens(phone);
        RadGrid1.DataBind();
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
        //string ReceiverPhoneNumber = cboTROM.Items.Count > 0 ? cboTROM.SelectedValue : "";
        //RadGrid1.DataSource = dRepo.GetAllForSalemens(ReceiverPhoneNumber);
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

    protected void cboTROM_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var trom = sRepo.GetSalemenById(int.Parse(e.Value));
            if(trom != null)
            {
                // Load data to TPS
                var tps = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, trom.Id);

                if (tps != null)
                {
                    cboTPS.DataSource = tps;
                    cboTPS.DataTextField = DataTextFieldName;
                    cboTPS.DataValueField = DataValueFieldName;
                    cboTPS.DataBind();

                    if (tps.Count > 0)
                    {
                        var item = new RadComboBoxItem("Select a TPS", "0");
                        cboTPS.Items.Insert(0, item);
                    }
                }
                else
                {
                    cboTPR.DataSource = null;
                    cboTPR.DataBind();
                }

                phone = trom.Phone;
            }
        }
        else
        {
            cboTPS.DataSource = null;
            cboTPS.DataBind();

            cboTPR.DataSource = null;
            cboTPR.DataBind();
        }

        LoadGridDashboard(phone);
    }
    protected void cboTPS_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var tps = sRepo.GetSalemenById(int.Parse(e.Value));

            if (tps != null)
            {
                // Load data to TPS
                var tpr = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, tps.Id);

                if (tpr != null)
                {
                    cboTPR.DataSource = tpr;
                    cboTPR.DataTextField = DataTextFieldName;
                    cboTPR.DataValueField = DataValueFieldName;
                    cboTPR.DataBind();

                    if (tpr.Count > 0)
                    {
                        var item = new RadComboBoxItem("Select a TPR", "0");
                        cboTPR.Items.Insert(0, item);
                    }
                }

                phone = tps.Phone;
            }
        }
        else
        {
            cboTPR.DataSource = null;
            cboTPR.DataBind();
        }

        LoadGridDashboard(phone);
    }
    protected void cboTPR_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        var tpr = sRepo.GetSalemenById(int.Parse(e.Value));
        if(tpr != null)
        {
            phone = tpr.Phone;
        }

        LoadGridDashboard(phone);
    }
}