<%@ Page Title="Function Management" Language="C#" MasterPageFile="~/Administrator/Admin.master" AutoEventWireup="true" CodeFile="FunctionManagement.aspx.cs" Inherits="Administrator_FunctionManagement" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <h3>
        Function Management
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
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1" CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false"
        AllowPaging="true" OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand" onitemdatabound="RadGrid1_ItemDataBound"
        OnInsertCommand="RadGrid1_InsertCommand" Skin="Office2007" PageSize="20">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" /> 
                <telerik:GridBoundColumn DataField="FunctionName" HeaderText="Function Name" />
                <telerik:GridTemplateColumn HeaderText="Parent Function">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblFunctionName" Text='<%# Eval("ParentFunctionName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlFunctions">
                        </telerik:RadComboBox>
                        <asp:HiddenField ID="hdfcurrentId" runat="server" Value='<%# Eval("Id") %>' />
                        <asp:HiddenField ID="hdfParentId" runat="server" Value='<%# Eval("ParentFunctionId") %>' />
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Action" HeaderText="Action" />
                <telerik:GridButtonColumn ConfirmText="Delete this function?" ConfirmDialogType="RadWindow"
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

