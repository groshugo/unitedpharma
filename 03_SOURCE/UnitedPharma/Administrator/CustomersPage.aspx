<%@ Page Title="Customer Management" Language="C#" MasterPageFile="~/Administrator/AdminFull.master"
    AutoEventWireup="true" CodeFile="CustomersPage.aspx.cs" Inherits="Administrator_CustomersPage" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function openWin() {
                var oWnd = radopen("ImportCustomers.aspx", "RadWindow1");
                oWnd.Maximize();
            }
            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                if (arg == "1") {
                    window.location.reload();
                }
            }
            function viewLog() {
                if ($find("<%=gridCustomerLog.ClientID %>").get_masterTableView().get_selectedItems().length < 1) {
                    alert("Select a customer to view log");
                }
                else {
                    var RadGrid1 = $find("<%=gridCustomerLog.ClientID %>");
                    var MasterTable = RadGrid1.get_masterTableView();
                    var row = MasterTable.get_selectedItems()[0];
                    var keyValues = row.getDataKeyValue("Id");
                    var oWnd = radopen("CustomerLog.aspx?Id=" + keyValues, "RadWindow2");
                    $("#<%= hdfID.ClientID %>").val(keyValues);
                }
            }
            var tableView = null;
            function pageLoad(sender, args) {
                tableView = $find("<%= gridCustomerLog.ClientID %>").get_masterTableView();
            }

            function RadComboBox1_SelectedIndexChanged(sender, args) {
                tableView.set_pageSize(sender.get_value());
            }

            function changePage(argument) {
                tableView.page(argument);
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager2" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose"
                Width="650px" Height="480px" NavigateUrl="ImportCustomers.aspx">
            </telerik:RadWindow>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" OnClientClose="OnClientClose"
                Width="850" Height="600" NavigateUrl="CustomerLog.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <div>
        <div style="float: left; padding: 10px;">
            <h3>
                Customer Management</h3>
            <asp:Panel ID="PanelFilter" runat="server" DefaultButton="btnFilter">
                <table>
                    <tr>
                        <td>
                            UPI Code:
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtUpiCode" runat="server" Width="158px"></asp:TextBox>--%>
                            <telerik:RadTextBox runat="server" ID="txtUpiCode" Width="158px" SkinID="Office2007"/>
                        </td>
                        <td>
                            FullName:
                        </td>
                        <td>
                            <%--<asp:TextBox ID="txtFullName" runat="server" Width="158px"></asp:TextBox>--%>
                            <telerik:RadTextBox runat="server" ID="txtFullName" Width="158px" SkinID="Office2007"/>
                        </td>
                        <td colspan="2">
                            <%--<asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" />--%>
                            <telerik:RadButton ID="btnFilter" runat="server" SkinID="Office2007" Text="Filter" OnClick="btnFilter_Click"></telerik:RadButton>
                            <telerik:RadButton ID="btnClear" runat="server" SkinID="Office2007" Text="Clear" OnClick="btnClear_Click"></telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="float: right; padding: 10px;">
            <button onclick="openWin(); return false;">
                Import customers</button>
        </div>
    </div>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CustomerList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CustomerList" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CustomerList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCustomerLog" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridCustomerLog">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCustomerLog" />
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
    <telerik:RadGrid ID="CustomerList" runat="server" Skin="Office2007" AllowPaging="true"
        AutoGenerateColumns="false" OnNeedDataSource="CustomerList_NeedDataSource" OnUpdateCommand="CustomerList_UpdateCommand"
        OnDeleteCommand="CustomerList_DeleteCommand" OnInsertCommand="CustomerList_InsertCommand"
        OnItemDataBound="CustomerList_ItemDataBound" AllowMultiRowEdit="true" PageSize="50">
        <MasterTableView DataKeyNames="Id" CommandItemDisplay="Top" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms" ClientDataKeyNames="Id">
            <Columns>
                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                <telerik:GridBoundColumn DataField="Password" HeaderText="Password" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Customer Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCustomerType" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlCustomerType">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Channel" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblChannel" Text='<%# Eval("ChannelName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlChannels">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" Visible="false" />
                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" Visible="false" />
                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn" Visible="false">
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
                        <telerik:RadComboBox runat="server" ID="ddlDistricts">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CreateDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("CreateDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfCreateDate" runat="server" Value='<%# String.Format("{0:d}", Eval("CreateDate")) %>' />
                        <telerik:RadDatePicker ID="txtCreateDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarCreateDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="UpdateDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("UpdateDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfUpdateDate" runat="server" Value='<%# String.Format("{0:d}", Eval("UpdateDate")) %>' />
                        <telerik:RadDatePicker ID="txtUpdateDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarUpdateDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationColumn" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("LocalName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfGroup" runat="server" Value='<%# Eval("GroupId") %>' />
                        <asp:HiddenField ID="hdfRegion" runat="server" Value='<%# Eval("RegionId") %>' />
                        <asp:HiddenField ID="hdfArea" runat="server" Value='<%# Eval("AreaId") %>' />
                        <asp:HiddenField ID="hdfLocal" runat="server" Value='<%# Eval("LocalId") %>' />
                        Group:
                        <telerik:RadComboBox ID="ddlGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Region:
                        <telerik:RadComboBox ID="ddlRegion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        <br />
                        Area:&nbsp;&nbsp;&nbsp;
                        <telerik:RadComboBox ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Local: &nbsp;&nbsp;
                        <telerik:RadComboBox runat="server" ID="ddlLocation">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridBoundColumn DataField="SupervisorName" HeaderText="Supervisor Name" />
                <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />
                <telerik:GridBoundColumn DataField="supervisorPhone" HeaderText="Supervisor Phone" />--%>
                <telerik:GridBoundColumn DataField="LocalName" HeaderText="Local" ReadOnly="true" />
                <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status">
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="SupervisorName" HeaderText="Supervisor Name" />
                <telerik:GridTemplateColumn HeaderText="Note Of Salesmen">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtNoteOfSalesmen" TextMode="MultiLine" Rows="4"
                            Columns="50" ></asp:TextBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="View detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="CustomerDetail" runat="server" Text="View detail" NavigateUrl='<%# String.Format("CustomerDetail.aspx?ID={0}",Eval("Id")) %>'></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridButtonColumn ConfirmText="Delete this customer?" ConfirmDialogType="RadWindow"
                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" />
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
    <br />
    <div>
        <div style="float: left; padding: 10px;">
            <h3>
                Edited Customers</h3>
        </div>
        <div style="float: right; padding: 10px;">
            <button onclick="viewLog(); return false;">
                View Log</button>
        </div>
    </div>
    <telerik:RadGrid ID="gridCustomerLog" runat="server" Skin="Office2007" AllowPaging="true"
        OnItemCreated="gridCustomerLog_ItemCreated" AutoGenerateColumns="false" OnNeedDataSource="gridCustomerLog_NeedDataSource"
        PageSize="50">
        <MasterTableView DataKeyNames="Id" InsertItemPageIndexAction="ShowItemOnCurrentPage"
            EditMode="EditForms" ClientDataKeyNames="Id" CommandItemDisplay="Top">
            <CommandItemSettings ShowAddNewRecordButton="false" />
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
                <telerik:GridClientSelectColumn UniqueName="ClientSelectColumn" />
                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                <telerik:GridBoundColumn DataField="Password" HeaderText="Password" Visible="false" />
                <telerik:GridTemplateColumn HeaderText="Customer Type">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCustomerType" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlCustomerType">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Channel" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblChannel" Text='<%# Eval("ChannelName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <telerik:RadComboBox runat="server" ID="ddlChannels">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" Visible="false" />
                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" Visible="false" />
                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn" Visible="false">
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
                        <telerik:RadComboBox runat="server" ID="ddlDistricts">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CreateDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("CreateDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfCreateDate" runat="server" Value='<%# String.Format("{0:d}", Eval("CreateDate")) %>' />
                        <telerik:RadDatePicker ID="txtCreateDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarCreateDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="UpdateDate" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("UpdateDate")) %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfUpdateDate" runat="server" Value='<%# String.Format("{0:d}", Eval("UpdateDate")) %>' />
                        <telerik:RadDatePicker ID="txtUpdateDate" runat="server" DateInput-ReadOnly="true">
                            <Calendar ID="CalendarUpdateDate" runat="server" RangeMinDate="1900-01-01" RangeMaxDate="2050-12-29">
                            </Calendar>
                        </telerik:RadDatePicker>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationColumn" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("LocalName") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:HiddenField ID="hdfGroup" runat="server" Value='<%# Eval("GroupId") %>' />
                        <asp:HiddenField ID="hdfRegion" runat="server" Value='<%# Eval("RegionId") %>' />
                        <asp:HiddenField ID="hdfArea" runat="server" Value='<%# Eval("AreaId") %>' />
                        <asp:HiddenField ID="hdfLocal" runat="server" Value='<%# Eval("LocalId") %>' />
                        Group:
                        <telerik:RadComboBox ID="ddlGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Region:
                        <telerik:RadComboBox ID="ddlRegion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        <br />
                        Area:&nbsp;&nbsp;&nbsp;
                        <telerik:RadComboBox ID="ddlArea" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp;Local: &nbsp;&nbsp;
                        <telerik:RadComboBox runat="server" ID="ddlLocation">
                        </telerik:RadComboBox>
                    </EditItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status"
                    Visible="false">
                </telerik:GridCheckBoxColumn>
                <telerik:GridBoundColumn DataField="SupervisorName" HeaderText="Supervisor Name" />
                <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />
                <telerik:GridBoundColumn DataField="supervisorPhone" HeaderText="Supervisor Phone" />
                <telerik:GridTemplateColumn HeaderText="View detail">
                    <ItemTemplate>
                        <asp:HyperLink ID="CustomerDetail" runat="server" Text="View detail" NavigateUrl='<%# String.Format("CustomerDetail.aspx?ID={0}",Eval("Id")) %>'></asp:HyperLink>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
            </Columns>
            <EditFormSettings>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
            <PagerStyle Mode="NumericPages" PageButtonCount="10" />
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="True" />
        </ClientSettings>
        <PagerStyle Mode="NextPrevAndNumeric" />
    </telerik:RadGrid>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Office2007"
        Width="950px" Height="480px">
    </telerik:RadWindowManager>
    <asp:HiddenField ID="hdfID" runat="server" Value="" />
</asp:Content>
