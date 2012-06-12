<%@ Page Title="SMS Outbox" Language="C#" MasterPageFile="~/Customers/CustomerMaster.master"
    AutoEventWireup="true" CodeFile="SMSOutbox.aspx.cs" Inherits="Customers_SMSOutbox" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">    
    <div style="margin-right: 5px; float: right; height: 35px;">
        <telerik:radbutton runat="server" id="btnFilter" text="Filter" skin="Office2007"
            tooltip="Filter" onclick="btnFilter_Click">
        </telerik:radbutton>
        <telerik:radbutton runat="server" id="btnClearFilter" text="Clear Filter" tooltip="Filter"
            onclick="btnClearFilter_Click" skin="Office2007">
        </telerik:radbutton>
    </div>
    <div style="float: right; height: 35px; width: 83%;">
        <div style="float: left; height: 35px; width: 48%;">
            Promotion: 
            <telerik:RadComboBox ID="ddlPromotion" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlPromotion_SelectedIndexChanged"></telerik:RadComboBox>
        </div>
        <div style="float: right; height: 35px; width: 52%;">
            Filter:
            <telerik:RadComboBox runat="server" ID="cbFilterType" Skin="Office2007">
                <Items>
                    <telerik:RadComboBoxItem Value="0" Text="By To" />
                    <telerik:RadComboBoxItem Value="1" Text="By Phone" />
                    <telerik:RadComboBoxItem Value="2" Text="By Subject" />
                </Items>
            </telerik:RadComboBox>
            <telerik:RadTextBox Skin="Office2007" runat="server" Height="16px" Width="150" ID="txtFilterValue"
                Style="padding-bottom: 1px;">
            </telerik:RadTextBox>
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:radwindowmanager id="RadWindowManager1" runat="server">
    </telerik:radwindowmanager>    
    <telerik:radajaxmanager runat="server" id="RadAjaxManager1" defaultloadingpanelid="RadAjaxLoadingPanel1">
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
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel runat="server" id="RadAjaxLoadingPanel1" cssclass="RadAjax RadAjax_Vista"
        transparency="25">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:radajaxloadingpanel>
    <div style="margin: 5px 1px; clear: both">
        <telerik:radgrid runat="server" id="RadGrid1" autogeneratecolumns="false" allowpaging="true"
            onneeddatasource="RadGrid1_NeedDataSource" allowsorting="true" gridlines="Horizontal"
            borderwidth="0" pagesize="20" allowmultirowselection="true" skin="Office2007">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" ItemStyle-Height="30"
                HeaderStyle-Height="35">
                <Columns>                    
                    <telerik:GridBoundColumn DataField="ReceiverName" SortExpression="ReceiverName" HeaderText="To" />
                    <telerik:GridBoundColumn DataField="ReceiverPhone" SortExpression="ReceiverPhone"
                        HeaderText="Phone" />
                    <telerik:GridHyperLinkColumn DataTextField="Subject" SortExpression="Subject" HeaderText="Subject"
                        DataNavigateUrlFormatString="~/Customers/ViewDetailSMS.aspx?ID={0}&T=0" DataNavigateUrlFields="Id" />
                    <telerik:GridBoundColumn DataFormatString="{0:d}" DataField="Date" DataType="System.DateTime"
                        HeaderText="Sent" SortExpression="Date" UniqueName="Date" />                    
                </Columns>
                <EditFormSettings>
                    <EditColumn ButtonType="ImageButton" />
                </EditFormSettings>
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="true">
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
            <PagerStyle Mode="NextPrevAndNumeric" />
        </telerik:radgrid>
    </div>
</asp:Content>
