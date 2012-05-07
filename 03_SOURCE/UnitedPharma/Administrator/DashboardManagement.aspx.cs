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
    private DashboardRepository dRepo = new DashboardRepository();
    private SalesmanRepository sRepo = new SalesmanRepository();

    private const string DataTextFieldName = "Fullname";
    private const string DataValueFieldName = "Id";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Utility.SetCurrentMenu("mDashboard");

            // Load TROM to combo
            LoadSalesmenToComboTROM();

            // Load EROM to combo
            LoadSalesmenToComboErom();

            // Load EROM2 to combo
            LoadSalesmenToComboErom2();
        }
    }

    private void LoadGridDashboard(string phone)
    {
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
        // Get all salesmen for the first time
        RadGrid1.DataSource = GetDataForGridCommand();
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
        var editableItem = ((GridEditableItem) e.Item);
        Hashtable values = new Hashtable();
        editableItem.ExtractValues(values);
        var id = (int) editableItem.GetDataKeyValue("Id");
        try
        {
            dRepo.Edit(id, (string) values["Title"], (string) values["Content"]);
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

    protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
    {
        var id = (int) ((GridDataItem) e.Item).GetDataKeyValue("Id");
        try
        {
            dRepo.Delete(id);
        }
        catch (System.Exception)
        {
            ShowErrorMessage();
        }

    }

    protected void cboTROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var trom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (trom != null)
            {
                // Load data to TPS
                var tps = sRepo.GetSalesmenByRoleIdAndManagerId((int) SalesmenRole.TPS, trom.Id);
                LoadListSalesmenToCombo(tps, cboTPS, "Select a TPS");

                UtilitiesHelpers.Instance.ClearComboData(cboTPR);

                phone = trom.Phone;
            }
        }
        else
        {
            UtilitiesHelpers.Instance.ClearComboData(cboTPS);
            UtilitiesHelpers.Instance.ClearComboData(cboTPR);
        }

        LoadGridDashboard(phone);
        ResetChannelCombo(SalesChannel.Trom);
    }

    protected void cboTPS_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var tps = sRepo.GetSalemenById(int.Parse(e.Value));

            if (tps != null)
            {
                // Load data to TPS
                var tpr = sRepo.GetSalesmenByRoleIdAndManagerId((int) SalesmenRole.TPR, tps.Id);

                if (tpr != null)
                {
                    cboTPR.DataSource = tpr;
                    cboTPR.DataTextField = DataTextFieldName;
                    cboTPR.DataValueField = DataValueFieldName;
                    cboTPR.DataBind();

                    LoadListSalesmenToCombo(tpr, cboTPR, "Select a TPR");
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

    protected void cboTPR_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        var tpr = sRepo.GetSalemenById(int.Parse(e.Value));
        if (tpr != null)
        {
            phone = tpr.Phone;
        }

        LoadGridDashboard(phone);
    }

    protected void cboEROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom != null)
            {
                // Load data to PSS1
                var pss1 = sRepo.GetSalesmenByRoleIdAndManagerId((int) SalesmenRole.PSS1, erom.Id);
                LoadListSalesmenToCombo(pss1, cboPSS1, "Select a PSS1");

                UtilitiesHelpers.Instance.ClearComboData(cboPSR1);

                phone = erom.Phone;
            }
        }
        else
        {
            UtilitiesHelpers.Instance.ClearComboData(cboPSS1);
            UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
        }

        LoadGridDashboard(phone);
        ResetChannelCombo(SalesChannel.Erom);
    }

    protected void cboPSS1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var pss1 = sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss1 != null)
            {
                // Load data to PSR 1
                var psr1 = sRepo.GetSalesmenByRoleIdAndManagerId((int) SalesmenRole.PSR1, pss1.Id);

                if (psr1 != null)
                {
                    LoadListSalesmenToCombo(psr1, cboPSR1, "Select a PSS1");
                }

                phone = pss1.Phone;
            }
        }
        else
        {
            cboPSR1.DataSource = null;
            cboPSR1.DataBind();
        }

        LoadGridDashboard(phone);
    }

    protected void cboPSR1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        var ps11 = sRepo.GetSalemenById(int.Parse(e.Value));
        if (ps11 != null)
        {
            phone = ps11.Phone;
        }

        LoadGridDashboard(phone);
    }

    protected void cboEROM2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var erom2 = sRepo.GetSalemenById(int.Parse(e.Value));
            if (erom2 != null)
            {
                // Load data to PSS2
                var pss2 = sRepo.GetSalesmenByRoleIdAndManagerId((int) SalesmenRole.PSS2, erom2.Id);
                LoadListSalesmenToCombo(pss2, cboPSS2, "Select a PSS2");

                UtilitiesHelpers.Instance.ClearComboData(cboPSR2);

                phone = erom2.Phone;
            }
        }
        else
        {
            UtilitiesHelpers.Instance.ClearComboData(cboPSS2);
            UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
        }

        LoadGridDashboard(phone);
        ResetChannelCombo(SalesChannel.Erom2);
    }

    protected void cboPSS2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var pss2 = sRepo.GetSalemenById(int.Parse(e.Value));

            if (pss2 != null)
            {
                // Load data to PSR 2
                var psr2 = sRepo.GetSalesmenByRoleIdAndManagerId((int) SalesmenRole.PSR2, pss2.Id);

                if (psr2 != null)
                {
                    LoadListSalesmenToCombo(psr2, cboPSR2, "Select a PSR2");
                }

                phone = pss2.Phone;
            }
        }
        else
        {
            cboPSR2.DataSource = null;
            cboPSR2.DataBind();
        }

        LoadGridDashboard(phone);
    }

    protected void cboPSR2_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        var psr2 = sRepo.GetSalemenById(int.Parse(e.Value));
        if (psr2 != null)
        {
            phone = psr2.Phone;
        }

        LoadGridDashboard(phone);
    }

    private void ResetChannelCombo(SalesChannel channel)
    {
        switch (channel)
        {
            case SalesChannel.Erom:
                UtilitiesHelpers.Instance.ClearComboData(cboTPS);
                UtilitiesHelpers.Instance.ClearComboData(cboTPR);
                cboTROM.SelectedIndex = 0;

                UtilitiesHelpers.Instance.ClearComboData(cboPSS2);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
                cboEROM2.SelectedIndex = 0;
                break;
            case SalesChannel.Erom2:
                UtilitiesHelpers.Instance.ClearComboData(cboTPS);
                UtilitiesHelpers.Instance.ClearComboData(cboTPR);
                cboTROM.SelectedIndex = 0;

                UtilitiesHelpers.Instance.ClearComboData(cboPSS1);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
                cboEROM.SelectedIndex = 0;
                break;
            default:
                UtilitiesHelpers.Instance.ClearComboData(cboPSS1);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR1);
                cboEROM.SelectedIndex = 0;

                UtilitiesHelpers.Instance.ClearComboData(cboPSS2);
                UtilitiesHelpers.Instance.ClearComboData(cboPSR2);
                cboEROM2.SelectedIndex = 0;
                break;
        }
    }

    private void LoadSalesmenToComboTROM()
    {
        LoadSalesmenToCombo(SalesmenRole.TROM, cboTROM, "Select a TROM");
    }

    private void LoadSalesmenToComboErom2()
    {
        LoadSalesmenToCombo(SalesmenRole.EROM2, cboEROM2, "Select a EROM2");
    }

    private void LoadSalesmenToComboErom()
    {
        LoadSalesmenToCombo(SalesmenRole.EROM, cboEROM, "Select a EROM");
    }

    private void LoadSalesmenToCombo(SalesmenRole role, RadComboBox cbo, string firstItemText)
    {
        var salesmens = sRepo.GetSalesmenByRoleId((int) role);
        if (salesmens == null)
        {
            cbo.Enabled = false;
            cbo.Enabled = false;
            // write log here
        }
        else
        {
            LoadListSalesmenToCombo(salesmens, cbo, firstItemText);
        }
    }

    private void LoadListSalesmenToCombo(List<Salesmen> trom, RadComboBox comboBox, string firstItemText)
    {
        if (trom != null)
        {
            comboBox.DataSource = trom;
            comboBox.DataTextField = DataTextFieldName;
            comboBox.DataValueField = DataValueFieldName;
            comboBox.DataBind();

            if (trom.Count > 0)
            {
                var item = new RadComboBoxItem(firstItemText, "0");
                comboBox.Items.Insert(0, item);
            }
        }

    }

    private List<Dashboard> GetDataForGridCommand()
    {
        // POS
        var tromId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboTROM);
        if(tromId > 0)
        {
            var tpsId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboTPS);
            if(tpsId > 0)
            {
                var tprId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboTPR);
                if(tprId > 0)
                {
                    var tprSalesmen = sRepo.GetSalemenById(tprId);
                    return dRepo.GetAllForSalemens(tprSalesmen.Phone);
                }

                var tpsSalesmen = sRepo.GetSalemenById(tpsId);
                return dRepo.GetAllForSalemens(tpsSalesmen.Phone);
            }

            var tromSalesmen = sRepo.GetSalemenById(tromId);
            return dRepo.GetAllForSalemens(tromSalesmen.Phone);
        }
        else
        {
            // POC
            var eromId = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboEROM);
            if(eromId > 0)
            {
                var pss1Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSS1);
                if (pss1Id > 0)
                {
                    var psr1Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSR1);
                    if (psr1Id > 0)
                    {
                        var psr1Salesmen = sRepo.GetSalemenById(psr1Id);
                        return dRepo.GetAllForSalemens(psr1Salesmen.Phone);
                    }

                    var pss1Salesmen = sRepo.GetSalemenById(pss1Id);
                    return dRepo.GetAllForSalemens(pss1Salesmen.Phone);
                }

                var eromSalesmen = sRepo.GetSalemenById(eromId);
                return dRepo.GetAllForSalemens(eromSalesmen.Phone);
            }
            else
            {
                var erom2Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboEROM2);
                if (erom2Id > 0)
                {
                    var pss2Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSS2);
                    if (pss2Id > 0)
                    {
                        var psr2Id = UtilitiesHelpers.Instance.GetSelectedValueOfCombo(cboPSR2);
                        if (psr2Id > 0)
                        {
                            var psr2Salesmen = sRepo.GetSalemenById(psr2Id);
                            return dRepo.GetAllForSalemens(psr2Salesmen.Phone);
                        }

                        var pss2Salesmen = sRepo.GetSalemenById(pss2Id);
                        return dRepo.GetAllForSalemens(pss2Salesmen.Phone);
                    }

                    var erom2Salesmen = sRepo.GetSalemenById(erom2Id);
                    return dRepo.GetAllForSalemens(erom2Salesmen.Phone);
                }
                return dRepo.GetAll();
            }
        }
    }
}
