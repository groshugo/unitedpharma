<%@ Page Title="CustomerSupervisor" Language="C#" MasterPageFile="~/Administrator/Admin.master" AutoEventWireup="true"
    CodeFile="CustomerSupervisorManagement.aspx.cs" Inherits="Administrator_CustomerSupervisorManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        CustomerSupervisor Management
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
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="FullName" />
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" />
                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" />
                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
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
                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfSection" runat="server" Value='<%# Eval("SectionId") %>' />
                        <asp:HiddenField ID="hdfProvince" runat="server" Value='<%# Eval("ProvinceId") %>' />
                        <asp:HiddenField ID="hdfDistrict" runat="server" Value='<%# Eval("DistrictId") %>' />
                        Section:
                        <telerik:RadComboBox ID="ddlSection" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Province:
                        <telerik:RadComboBox ID="ddlprovince" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlprovince_SelectedIndexChanged">
                        </telerik:RadComboBox>                        
                        &nbsp;District:
                        <telerik:RadComboBox runat="server" ID="ddlDistrict">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Customer Position">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblposition" Text='<%# Eval("PositionName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlPosition">
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
