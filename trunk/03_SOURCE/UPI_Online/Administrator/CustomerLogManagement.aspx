<%@ page title="" language="C#" masterpagefile="~/Administrator/Admin.master" autoeventwireup="true" inherits="Administrator_CustomerLog, App_Web_wvzyt0rb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" Skin="Office2007"></telerik:RadFormDecorator>
    <telerik:RadCodeBlock runat="server" ID="radCodeBlock">
        <script type="text/javascript">
            function GetId() {
                if ($find("<%=DetailLog.ClientID %>").get_masterTableView().get_selectedItems().length < 1) {
                    alert("Select version in log to update");
                }
                else {
                    var firstDataItem = $find("<%= DetailLog.MasterTableView.ClientID %>").get_dataItems()[0];
                    var keyValues = firstDataItem.getDataKeyValue("Id");
                    $("#<%= hdfID.ClientID %>").val(keyValues);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <h3 style="padding-left:10px">Customer Logs</h3>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlListaccount">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbxListAccounts" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="lbxLogs" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="DetailLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="lbxListAccounts">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="lbxLogs" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="DetailLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="lbxLogs">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DetailLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gridCustomers">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DetailLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="gridCustomers" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DetailLog">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DetailLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <%--<AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnApprove">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gridCustomers" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="DetailLog" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>--%>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1"
        CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel ID="PanelLogs" runat="server">
        <table>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="gridCustomers" AutoGenerateColumns="false" Skin="Office2007" AllowMultiRowSelection="false" 
                        OnSelectedIndexChanged="gridCustomers_SelectedIndexChanged" AutoPostback="true" AllowPaging="true" PageSize="10" OnNeedDataSource="gridCustomers_NeedDataSource">
                        <MasterTableView DataKeyNames="Id">
                            <Columns>
                                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                                <telerik:GridBoundColumn DataField="Password" HeaderText="Password" />
                                <telerik:GridTemplateColumn HeaderText="Custome Typer">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCustomerType" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Channel">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblChannel" Text='<%# Eval("ChannelName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" />
                                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" />
                                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="CreateDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("CreateDate")) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="UpdateDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("UpdateDate")) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationColumn">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("LocalName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                    <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status">
                                </telerik:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td>Customer information Edited</td>
            </tr>
            <tr>
                <td>
                    <telerik:RadGrid runat="server" ID="DetailLog" AutoGenerateColumns="false" Skin="Office2007" AllowMultiRowSelection="false" 
                        AllowPaging="false" OnNeedDataSource="DetailLog_NeedDataSource">
                        <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id">
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                                <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UPI Code" />
                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                                <telerik:GridBoundColumn DataField="Password" HeaderText="Password" />
                                <telerik:GridTemplateColumn HeaderText="Custome Typer">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCustomerType" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Channel">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblChannel" Text='<%# Eval("ChannelName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" />
                                <telerik:GridBoundColumn DataField="Street" HeaderText="Street" />
                                <telerik:GridBoundColumn DataField="Ward" HeaderText="Ward" Visible="false" />
                                <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                                <telerik:GridTemplateColumn HeaderText="District" UniqueName="DistrictColumn">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblDistrict" Text='<%# Eval("DistrictName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="CreateDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCreateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("CreateDate")) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="UpdateDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# String.Format("{0:d}", Eval("UpdateDate")) %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Location" UniqueName="LocationColumn">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblLocation" Text='<%# Eval("LocalName") %>'></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                    <telerik:GridCheckBoxColumn DataField="Status" HeaderText="Status" UniqueName="Status">
                                </telerik:GridCheckBoxColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClientClick="GetId()" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <div style="clear:both"></div>
    
                    <asp:HiddenField ID="hdfID" runat="server" Value="" />
</asp:Content>

