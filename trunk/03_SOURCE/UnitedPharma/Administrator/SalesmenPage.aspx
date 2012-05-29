<%@ Page Title="Salesmen Management" Language="C#" MasterPageFile="~/Administrator/AdminFull.master"
    AutoEventWireup="true" CodeFile="SalesmenPage.aspx.cs" Inherits="Administrator_SalesmenPage" %>

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

            <telerik:AjaxSetting AjaxControlID="cboTROM">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboTPS" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS1" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTPS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cboEROM">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />

                    <telerik:AjaxUpdatedControl ControlID="cboTROM" />
                    <telerik:AjaxUpdatedControl ControlID="cboTPS" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPSS1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cboEROM2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />

                    <telerik:AjaxUpdatedControl ControlID="cboTROM" />
                    <telerik:AjaxUpdatedControl ControlID="cboTPS" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTPS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="ddlRoles">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlGroupAddNew" />
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
    <div style="padding:10px 0">
        <asp:Panel ID="PanelFilter" runat="server" DefaultButton="btnFilter">
            <table>
                <tr>
                    <td>Group</td>
                    <td>
                        <telerik:RadComboBox ID="ddlGroup" runat="server" AutoPostBack="true" 
                            onselectedindexchanged="ddlGroup_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                    <td>Region</td>
                    <td>
                        <telerik:RadCombobox ID="ddlRegion" runat="server" AutoPostBack="true" 
                            Enabled="false" onselectedindexchanged="ddlRegion_SelectedIndexChanged"></telerik:RadCombobox>
                    </td>
                    <td>Area</td>
                    <td>
                        <telerik:RadComboBox ID="ddlArea" runat="server" AutoPostBack="true" 
                            Enabled="false" onselectedindexchanged="ddlArea_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                    <td>Local</td>
                    <td colspan="4">
                        <telerik:RadComboBox ID="ddlLocal" runat="server" AutoPostBack="true" 
                            Enabled="false" onselectedindexchanged="ddlLocal_SelectedIndexChanged"></telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>FullName</td>
                    <td>
                        <telerik:RadTextBox ID="txtFullName" runat="server" Width="158px" Skin="Office2007"/>
                    </td>
                    <td>Phone</td>
                    <td><telerik:RadTextBox ID="txtPhoneNumber" runat="server" Width="158px" Skin="Office2007"/></td>
                    <td>Role</td>
                    <td><telerik:RadTextBox ID="txtRoleName" runat="server" Width="158px" Skin="Office2007"/></td>
                    <td colspan="2" align="center">
                        <%--<asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />--%>
                        <telerik:RadButton runat="server" ID="btnFilter" OnClick="btnFilter_Click" Text="Filter" Skin="Office2007" />
                    </td>
                </tr>
            </table>
            <table style="padding-right: 3px; padding-bottom: 3px;">
            <tr>
                <td>
                    <span style="font-weight: bold">POS:</span>
                </td>
                <td>
                    TROM:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboTROM" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboTROM_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td>
                    TPS:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboTPS" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboTPS_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-weight: bold">POC:</span>
                </td>
                <td>
                    EROM:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboEROM" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboEROM_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td>
                    PSS1:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboPSS1" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboPSS1_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    EROM2:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboEROM2" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboEROM2_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
                <td>
                    PSS2:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboPSS2" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboPSS2_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnNeedDataSource="RadGrid1_NeedDataSource" OnUpdateCommand="RadGrid1_UpdateCommand"
        OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
        Skin="Office2007" OnItemDataBound="RadGrid1_ItemDataBound" PageSize="50">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms" EditFormSettings-EditColumn-ButtonType="ImageButton">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="FullName" FilterControlWidth="120px" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" FilterControlWidth="120px" />
                <telerik:GridTemplateColumn HeaderText="Role">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblRole" Text='<%# Eval("RoleName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlRoles" AutoPostBack="true" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SmsQuota" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSmsQuota" Text='<%# Eval("SmsQuota") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadNumericTextBox ID="txtSmsQuota" runat="server" MaxLength="3" Type="Number">
                        </telerik:RadNumericTextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="SmsUsed" AllowFiltering="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSmsUsed" Text='<%# Eval("SmsUsed") %>'></asp:Label>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="ExpiredDate" FilterControlWidth="120px">
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
                <telerik:GridTemplateColumn HeaderText="Group">
                    <EditItemTemplate>
                        <telerik:RadComboBox ID="ddlGroupAddNew" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlGroupAddNew_SelectedIndexChanged"></telerik:RadComboBox>
                    </EditItemTemplate>                    
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Region" Visible="false">
                    <EditItemTemplate>
                        <telerik:RadComboBox ID="ddlRegionAddNew" runat="server" AutoPostBack="true" 
                        OnSelectedIndexChanged="ddlRegionAddNew_SelectedIndexChanged"></telerik:RadComboBox>
                    </EditItemTemplate>                    
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Area" Visible="false">
                    <EditItemTemplate>
                        <telerik:RadComboBox ID="ddlAreaAddNew" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAreaAddNew_SelectedIndexChanged"></telerik:RadComboBox>
                    </EditItemTemplate>                    
                </telerik:GridTemplateColumn> 
                <telerik:GridTemplateColumn HeaderText="Local" Visible="false">
                    <EditItemTemplate>
                        <telerik:RadComboBox ID="ddlLocalAddNew" runat="server" AutoPostBack="true"></telerik:RadComboBox>
                    </EditItemTemplate>                    
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Are you sure you want to delete this salesman?" ConfirmDialogType="RadWindow"
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
