<%@ Page Title="Supervisor - Customer" Language="C#" MasterPageFile="~/Administrator/Admin.master" AutoEventWireup="true"
    CodeFile="SupervisorCustomerManagement.aspx.cs" Inherits="Administrator_SupervisorCustomerManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Supervisor - Customer Management
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
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
        Skin="Office2007" OnItemDataBound="RadGrid1_ItemDataBound" PageSize="20">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage">
            <Columns>
                <telerik:GridTemplateColumn HeaderText="Supervisor">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSupervisor" Text='<%# Eval("SupervisorName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfSupervisor" runat="server" Value='<%# Eval("SupervisorId") %>' />
                        <telerik:RadComboBox runat="server" ID="ddlSupervisor">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>                
                <telerik:GridTemplateColumn HeaderText="Customer" UniqueName="CustomerColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCustomer" Text='<%# Eval("CustomerName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfCustomer" runat="server" Value='<%# Eval("CustomerId") %>' />
                        <asp:HiddenField ID="hdfCustomerType" runat="server" Value='<%# Eval("CustomerTypeId") %>' />
                        Customer Type:
                        <telerik:RadComboBox ID="ddlCustomerType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Customer Name:
                        <telerik:RadComboBox runat="server" ID="ddlCustomer" MarkFirstMatch="true" EmptyMessage="Enter Customer Name">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Delete this supervior?" ConfirmDialogType="RadWindow"
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
