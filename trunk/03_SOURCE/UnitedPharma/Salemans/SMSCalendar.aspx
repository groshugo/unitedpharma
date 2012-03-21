<%@ Page Title="SMS Calendar" Language="C#" MasterPageFile="~/Salemans/SalesmenMaster.master"
    AutoEventWireup="true" CodeFile="SMSCalendar.aspx.cs" Inherits="Salemans_SMSCalendar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="float: left; height: 35px;">
        <telerik:radbutton runat="server" id="btnCompose" text="Compose" skin="Office2007"
            tooltip="Compose" onclick="btnCompose_Click">
        </telerik:radbutton>
    </div>
    <div style="margin-right: 5px; float: right; height: 35px;">
        <telerik:radbutton runat="server" id="btnFilter" text="Filter" tooltip="Filter" onclick="btnFilter_Click">
        </telerik:radbutton>
        <telerik:radbutton runat="server" id="btnClearFilter" text="Clear Filter" tooltip="Clear Filter"
            onclick="btnClearFilter_Click">
        </telerik:radbutton>
    </div>
    <div style="float: right; height: 35px; width: 358px;">
        Filter:
        <telerik:radcombobox runat="server" id="cbFilterType" skin="Office2007">
            <Items>
                <telerik:RadComboBoxItem Value="0" Text="By From" />
                <telerik:RadComboBoxItem Value="1" Text="By Phone" />
                <telerik:RadComboBoxItem Value="2" Text="By Subject" />
            </Items>
        </telerik:radcombobox>
        <telerik:radtextbox runat="server" id="txtFilterValue" height="18px" width="150"
            skin="Office2007">
        </telerik:radtextbox>
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
            onneeddatasource="RadGrid1_NeedDataSource" allowsorting="true" skin="Office2007"
            gridlines="Horizontal" borderwidth="0" pagesize="20" allowmultirowselection="true">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None"  ItemStyle-Height="30" HeaderStyle-Height="35">
                <Columns>                    
                    <telerik:GridBoundColumn DataField="SenderName" SortExpression="SenderName" HeaderText="From" />
                    <telerik:GridBoundColumn DataField="SenderPhone" SortExpression="SenderPhone" HeaderText="Phone" />
                    <telerik:GridHyperLinkColumn DataTextField="Subject" SortExpression="Subject" HeaderText="Subject" 
                        DataNavigateUrlFormatString="~/Salemans/ViewDetailSMS.aspx?ID={0}&T=1" DataNavigateUrlFields="Id" />
                    <telerik:GridBoundColumn DataFormatString="{0:d}" DataField="Date" DataType="System.DateTime"
                        HeaderText="Received" SortExpression="Date" UniqueName="Date" />                    
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
