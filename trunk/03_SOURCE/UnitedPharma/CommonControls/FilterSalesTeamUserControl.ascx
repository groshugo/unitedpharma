<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FilterSalesTeamUserControl.ascx.cs"
    Inherits="CommonControls_FilterSalesTeamUserControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<telerik:RadAjaxManagerProxy runat="server" ID="FilterSalesTeamRadAjaxManagerProxy">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="cboTROM">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboTPS" />
                <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                <telerik:AjaxUpdatedControl ControlID="cboEROM" />
                <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
                <telerik:AjaxUpdatedControl ControlID="cboEROM2" />
                <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboTPS">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboTPR">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboEROM">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                <telerik:AjaxUpdatedControl ControlID="cboTPS" />
                <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                <telerik:AjaxUpdatedControl ControlID="cboTROM" />
                <telerik:AjaxUpdatedControl ControlID="cboEROM2" />
                <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboPSS1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboPSR1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboEROM2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                <telerik:AjaxUpdatedControl ControlID="cboTPS" />
                <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                <telerik:AjaxUpdatedControl ControlID="cboTROM" />
                <telerik:AjaxUpdatedControl ControlID="cboEROM" />
                <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboTPS">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="cboPSR2">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManagerProxy>
