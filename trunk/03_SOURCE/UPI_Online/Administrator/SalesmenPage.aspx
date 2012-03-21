<%@ page title="Salesmen Management" language="C#" masterpagefile="~/Administrator/AdminFull.master" autoeventwireup="true" inherits="Administrator_SalesmenPage, App_Web_wvzyt0rb" %>

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
                    <td><asp:TextBox ID="txtFullName" runat="server" Width="158px"></asp:TextBox></td>
                    <td>Phone</td>
                    <td><asp:TextBox ID="txtPhoneNumber" runat="server" Width="158px"></asp:TextBox></td>
                    <td>Role</td>
                    <td><asp:TextBox ID="txtRoleName" runat="server" Width="158px"></asp:TextBox></td>
                    <td colspan="2" align="center"><asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" /></td>
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
                        <telerik:RadComboBox runat="server" ID="ddlRoles">
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
