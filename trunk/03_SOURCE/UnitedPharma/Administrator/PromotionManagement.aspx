<%@ Page Title="Promotion Management" Language="C#" MasterPageFile="~/Administrator/Admin.master" AutoEventWireup="true"
    CodeFile="PromotionManagement.aspx.cs" Inherits="Administrator_PromotionManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Promotion Management
    </h3>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
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
    <div style="padding:5px 0;">
        <telerik:RadButton ID="btnSchedule" runat="server" Text="Promotion Schedule" OnClick="btnSchedule_Click"></telerik:RadButton>
    </div>
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
        Skin="Office2007" OnCreateColumnEditor="RadGrid1_CreateColumnEditor">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                <telerik:GridTemplateColumn HeaderText="Start date">
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# string.Format("{0:d}",Eval("StartDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="txtStartDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarStartDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="End date">
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%# string.Format("{0:d}",Eval("EndDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="txtEndtDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarEndDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2099-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn Visible="False" UniqueName="Content" HeaderText="Content"
                    DataField="Content">
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn ConfirmText="Delete this promotion?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
            </Columns>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
        </MasterTableView>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2007" />
</asp:Content>
