﻿using System;
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
        RadGrid1.DataSource = new List<Salesmen>();
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

    protected void cboTROM_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        if (!string.IsNullOrEmpty(e.Value))
        {
            var trom = sRepo.GetSalemenById(int.Parse(e.Value));
            if (trom != null)
            {
                // Load data to TPS
                var tps = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, trom.Id);
                LoadListSalesmenToCombo(tps, cboTPS, "Select a TPS");
                
                ClearComboData(cboTPR);

                phone = trom.Phone;
            }
        }
        else
        {
            ClearComboData(cboTPS);
            ClearComboData(cboTPR);
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
                var tpr = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.TPS, tps.Id);

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
                var pss1 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS1, erom.Id);
                LoadListSalesmenToCombo(pss1, cboPSS1, "Select a PSS1");

                ClearComboData(cboPSR1);

                phone = erom.Phone;
            }
        }
        else
        {
            ClearComboData(cboPSS1);
            ClearComboData(cboPSR1);
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
                var psr1 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR1, pss1.Id);

                if (psr1 != null)
                {
                    cboPSS1.DataSource = psr1;
                    cboPSS1.DataTextField = DataTextFieldName;
                    cboPSS1.DataValueField = DataValueFieldName;
                    cboPSS1.DataBind();

                    LoadListSalesmenToCombo(psr1, cboPSS1, "Select a PSS1");
                }

                phone = pss1.Phone;
            }
        }
        else
        {
            cboPSS1.DataSource = null;
            cboPSS1.DataBind();
        }

        LoadGridDashboard(phone);
    }
    protected void cboPSR1_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        string phone = string.Empty;
        var pss1 = sRepo.GetSalemenById(int.Parse(e.Value));
        if (pss1 != null)
        {
            phone = pss1.Phone;
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
                var pss2 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSS2, erom2.Id);
                LoadListSalesmenToCombo(pss2, cboPSS2, "Select a PSS2");

                ClearComboData(cboPSR2);

                phone = erom2.Phone;
            }
        }
        else
        {
            ClearComboData(cboPSS2);

            ClearComboData(cboPSR2);
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
                var psr2 = sRepo.GetSalesmenByRoleIdAndManagerId((int)SalesmenRole.PSR2, pss2.Id);

                if (psr2 != null)
                {
                    cboPSR2.DataSource = psr2;
                    cboPSR2.DataTextField = DataTextFieldName;
                    cboPSR2.DataValueField = DataValueFieldName;
                    cboPSR2.DataBind();

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
        var pss2 = sRepo.GetSalemenById(int.Parse(e.Value));
        if (pss2 != null)
        {
            phone = pss2.Phone;
        }

        LoadGridDashboard(phone);
    }

    private void ResetChannelCombo(SalesChannel channel)
    {
        switch (channel)
        {
            case SalesChannel.Erom:
                ClearComboData(cboTPS);
                ClearComboData(cboTPR);
                cboTROM.SelectedIndex = -1;

                ClearComboData(cboPSS2);
                ClearComboData(cboPSR2);
                cboEROM2.SelectedIndex = -1;
                break;
            case SalesChannel.Erom2:
                ClearComboData(cboTPS);
                ClearComboData(cboTPR);
                cboTROM.SelectedIndex = -1;

                ClearComboData(cboPSS1);
                ClearComboData(cboPSR1);
                cboEROM.SelectedIndex = -1;
                break;
            default:
                ClearComboData(cboPSS1);
                ClearComboData(cboPSR1);
                cboEROM.SelectedIndex = -1;

                ClearComboData(cboPSS2);
                ClearComboData(cboPSR2);
                cboEROM2.SelectedIndex = -1;
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
        var salesmens = sRepo.GetSalesmenByRoleId((int)role);
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
    private void ClearComboData(RadComboBox comboBox)
    {
        if(comboBox.Items != null && comboBox.Items.Count >0)
        {
            comboBox.Items.Clear();
        }
    }
}
