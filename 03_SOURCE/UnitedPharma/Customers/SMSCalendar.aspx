<%@ Page Title="SMS Calendar" Language="C#" MasterPageFile="~/Customers/CustomerMaster.master"
    AutoEventWireup="true" CodeFile="SMSCalendar.aspx.cs" Inherits="Customers_SMSCalendar" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="margin-right: 5px; float: right; height: 35px;">
        <table>
            <tr>
                <td>
                    <telerik:RadButton runat="server" ID="btnFilter" Text="Filter" ToolTip="Filter" OnClick="btnFilter_Click">
                    </telerik:RadButton>
                </td>
                <td>
                    <telerik:RadButton runat="server" ID="btnClearFilter" Text="Clear Filter" ToolTip="Clear Filter"
                        OnClick="btnClearFilter_Click">
                    </telerik:RadButton>
                </td>
            </tr>
        </table>
    </div>
    <div style="float: right; height: 35px; width: 358px;">
        <table>
            <tr>
                <td>
                    Filter:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cbFilterType" Skin="Office2007">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Text="By From" />
                            <telerik:RadComboBoxItem Value="1" Text="By Phone" />
                            <telerik:RadComboBoxItem Value="2" Text="By Subject" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadTextBox runat="server" ID="txtFilterValue" Height="18px" Width="150"
                        Skin="Office2007">
                    </telerik:RadTextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
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
                    <telerik:AjaxUpdatedControl ControlID="cbFilterType" />
                    <telerik:AjaxUpdatedControl ControlID="txtFilterValue" />
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
    <div style="margin: 5px 1px; clear: both">
        <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
            OnNeedDataSource="RadGrid1_NeedDataSource" AllowSorting="true" Skin="Office2007"
            GridLines="Horizontal" BorderWidth="0" PageSize="20" AllowMultiRowSelection="true">
            <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" ItemStyle-Height="30"
                HeaderStyle-Height="35">
                <Columns>
                    <telerik:GridBoundColumn DataField="SenderName" SortExpression="SenderName" HeaderText="From" />
                    <telerik:GridBoundColumn DataField="SenderPhone" SortExpression="SenderPhone" HeaderText="Phone" />
                    <telerik:GridHyperLinkColumn DataTextField="Subject" SortExpression="Subject" HeaderText="Subject"
                        DataNavigateUrlFormatString="~/Customers/ViewDetailSMS.aspx?ID={0}&T=1" DataNavigateUrlFields="Id" />
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
        </telerik:RadGrid>
    </div>
</asp:Content>
