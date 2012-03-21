<%@ Page Title="Salemens Management" Language="C#" MasterPageFile="~/Administrator/Admin.master"
    AutoEventWireup="true" CodeFile="SalesmanManagement.aspx.cs" Inherits="Administrator_SalesmanManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Salemens Management
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
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1"
        CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
        Skin="Office2007" OnItemDataBound="RadGrid1_ItemDataBound" PageSize="5">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms" EditFormSettings-EditColumn-ButtonType="ImageButton">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="FullName" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                <telerik:GridTemplateColumn HeaderText="Role">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblRole" Text='<%# Eval("RoleName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlRoles">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SmsQuota">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSmsQuota" Text='<%# Eval("SmsQuota") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtSmsQuota" runat="server" MaxLength="3" Type="Number">
                        </telerik:RadNumericTextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SmsUsed">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSmsUsed" Text='<%# Eval("SmsUsed") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="ExpiredDate">
                    <ItemTemplate>
                        <asp:Label ID="lblExpiredDate" runat="server" Text='<%# String.Format("{0:d}",Eval("ExpiredDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadDatePicker ID="txtExpiredDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarExpiredDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2099-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Delete this saleman?" ConfirmDialogType="RadWindow"
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
