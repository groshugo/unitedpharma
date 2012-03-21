<%@ Page Title="SMS Contact" Language="C#" MasterPageFile="~/Customers/CustomerMaster.master"
    AutoEventWireup="true" CodeFile="SMSContact.aspx.cs" Inherits="Customers_SMSContact" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
        <div><h3>Direct contact</h3></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .sms_header
        {
            background: url(../Images/nav_bg.png) repeat-x;
        }
    </style>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnClearFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista"
        Transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <div style="margin: 5px 1px;">
        <telerik:RadGrid runat="server" ID="RadGrid2" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid2_NeedDataSource" AllowSorting="true" Skin="Office2007"
            PageSize="20" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridBoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Name" HeaderStyle-Width="60%" />
                    <telerik:GridBoundColumn DataField="Phone" SortExpression="Phone" HeaderText="Phone" HeaderStyle-Width="25%" />
                    <telerik:GridBoundColumn DataField="RoleName" SortExpression="RoleName" HeaderText="Role" HeaderStyle-Width="15%" />
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
        <div style="height:13px"></div>
        <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid1_NeedDataSource" AllowSorting="true" Skin="Office2007"
            PageSize="10" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None">
                <Columns>
                    <telerik:GridBoundColumn DataField="FullName" SortExpression="FullName" HeaderText="Name" HeaderStyle-Width="60%" />
                    <telerik:GridBoundColumn DataField="Phone" SortExpression="Phone" HeaderText="Phone" HeaderStyle-Width="25%" />
                    <telerik:GridBoundColumn DataField="RoleName" SortExpression="RoleName" HeaderText="Role" HeaderStyle-Width="15%" />
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:RadGrid>
    </div>
</asp:Content>
