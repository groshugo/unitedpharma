<%@ Page Title="Area Management" Language="C#" MasterPageFile="~/Administrator/Admin.master" AutoEventWireup="true"
    CodeFile="AreaManagement.aspx.cs" Inherits="Administrator_AreaManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>
        Area Management
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
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
        Skin="Office2007" OnItemDataBound="RadGrid1_ItemDataBound" OnCreateColumnEditor="RadGrid1_CreateColumnEditor">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms" EditFormSettings-EditColumn-ButtonType="ImageButton">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="AreaName" HeaderText="Area Name" />
                <telerik:GridBoundColumn DataField="Description" HeaderText="Description" UniqueName="Description" />                                
                <telerik:GridTemplateColumn HeaderText="Region" UniqueName="RegionNameColumn">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblRegionName" Text='<%# Eval("RegionName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField runat="server" ID="hdfGroupId" Value='<%# Eval("GroupId") %>' />  
                        Group: <telerik:RadComboBox runat="server" ID="ddlGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        <asp:HiddenField runat="server" ID="hdfRegionId" Value='<%# Eval("RegionId") %>' />                        
                        Region: <telerik:RadComboBox runat="server" ID="ddlRegion">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Delete this Area?" ConfirmDialogType="RadWindow"
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
