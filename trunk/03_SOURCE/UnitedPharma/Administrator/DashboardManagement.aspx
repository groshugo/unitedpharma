<%@ Page Title="Dashboard Management" Language="C#" MasterPageFile="~/Administrator/AdminFull.master"
    AutoEventWireup="true" CodeFile="DashboardManagement.aspx.cs" Inherits="Administrator_DashboardManagement" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function openWin() {
                var oWnd = radopen("CreateDashboard.aspx", "RadWindow1");
            }
            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg == "1") {
                    window.location.reload();
                }

            }
            var tableView = null;
            function pageLoad(sender, args) {
                tableView = $find("<%= RadGrid1.ClientID %>").get_masterTableView();
            }

            function RadComboBox1_SelectedIndexChanged(sender, args) {
                tableView.set_pageSize(sender.get_value());
            }

            function changePage(argument) {
                tableView.page(argument);
            }

            function openWinViewDashboard(dashboardId) {
                var oWnd = radopen("ViewDashboardDetails.aspx?ID=" + dashboardId, "RadWindowDashboardDetails");
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose" Width="850" Height="605" NavigateUrl="CreateDashboard.aspx">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindowDashboardDetails" runat="server" Behaviors="Close" OnClientClose="OnClientClose" Width="600" 
                Height="400" NavigateUrl="ViewDashboardDetails.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <h3>
        Dashboard Management
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
                    <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                    
                    <telerik:AjaxUpdatedControl ControlID="cboEROM" />                    
                    <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSR1" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTPS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTPR">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cboEROM">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />

                    <telerik:AjaxUpdatedControl ControlID="cboTPS" />
                    <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                    <telerik:AjaxUpdatedControl ControlID="cboTROM" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPSS1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPSR1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="cboEROM2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPSS2" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />

                    <telerik:AjaxUpdatedControl ControlID="cboTPS" />
                    <telerik:AjaxUpdatedControl ControlID="cboTPR" />
                    <telerik:AjaxUpdatedControl ControlID="cboTROM" />

                    <telerik:AjaxUpdatedControl ControlID="cboEROM" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSS1" />
                    <telerik:AjaxUpdatedControl ControlID="cboPSR1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboTPS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cboPSR2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cboPSR2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
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

    <div style="float: right; width: 200px; text-align: right">
        <button onclick="openWin(); return false;">
            Add new Dashboard</button>
    </div>

    <div style="margin: 10px 0 10px;">
        View dashboard of salesmen: &nbsp;
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
                <td>
                    TPR:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboTPR" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboTPR_SelectedIndexChanged">
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
                <td>
                    PSR1:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboPSR1" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboPSR1_SelectedIndexChanged">
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
                <td>
                    PSR2:
                </td>
                <td>
                    <telerik:RadComboBox runat="server" ID="cboPSR2" AutoPostBack="true" Skin="Office2007"
                        OnSelectedIndexChanged="cboPSR2_SelectedIndexChanged">
                    </telerik:RadComboBox>
                </td>
            </tr>
        </table>
    </div>
    
    <telerik:RadGrid runat="server" ID="RadGrid1" AutoGenerateColumns="false" AllowPaging="true"
        OnItemCreated="RadGrid1_ItemCreated" OnNeedDataSource="RadGrid1_NeedDataSource"
        OnUpdateCommand="RadGrid1_UpdateCommand" PageSize="50" OnDeleteCommand="RadGrid1_DeleteCommand"
        Skin="Office2007" OnCreateColumnEditor="RadGrid1_CreateColumnEditor">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="None" InsertItemPageIndexAction="ShowItemOnCurrentPage">
            <PagerTemplate>
                <asp:Panel ID="PagerPanel" Style="padding: 6px; height: 20px" runat="server">
                    <div style="float: left">
                        <span style="margin-right: 3px;">Page size:</span>
                        <telerik:RadComboBox ID="RadComboBox1" DataSource="<%# new object[]{10, 20, 50, 200, 500, 1000} %>"
                            Style="margin-right: 20px;" Width="70px" SelectedValue='<%# DataBinder.Eval(Container, "Paging.PageSize") %>'
                            runat="server" OnClientSelectedIndexChanged="RadComboBox1_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    </div>
                    <div style="margin: 0px; float: right;">
                        Displaying page
                        <%# (int)DataBinder.Eval(Container, "Paging.CurrentPageIndex") + 1 %>
                        of
                        <%# DataBinder.Eval(Container, "Paging.PageCount")%>
                        , items
                        <%# (int)DataBinder.Eval(Container, "Paging.FirstIndexInPage") + 1 %>
                        to
                        <%# (int)DataBinder.Eval(Container, "Paging.LastIndexInPage") + 1 %>
                        of
                        <%# DataBinder.Eval(Container, "Paging.DataSourceCount")%>
                    </div>
                    <asp:Panel runat="server" ID="NumericPagerPlaceHolder" />
                </asp:Panel>
            </PagerTemplate>
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" />
                <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                <telerik:GridBoundColumn DataField="CreateDate" HeaderText="Create Date" ReadOnly="true" />
                <telerik:GridBoundColumn DataField="UpdateDate" HeaderText="Update Date" ReadOnly="true" />
                <telerik:GridBoundColumn UniqueName="Content" HeaderText="Content" DataField="Content">
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="">
                    <ItemTemplate>
                        <a href="javascript:void(0);" onclick="openWinViewDashboard(<%# Eval("ID") %>); return false;">View Details</a>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Are you sure you want to delete this Dashboard?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
            </Columns>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
            <PagerStyle Mode="NumericPages" PageButtonCount="10" />
        </MasterTableView>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
</asp:Content>
