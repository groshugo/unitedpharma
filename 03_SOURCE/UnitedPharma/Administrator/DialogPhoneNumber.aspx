<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DialogPhoneNumber.aspx.cs" Inherits="Administrator_DialogPhoneNumber" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select phone number</title>
    <style type="text/css">
        html, body, form
        {
            padding: 0;
            margin: 0;
            height: 100%;
            background: #f2f2de;
        }
        
        body
        {
            font: normal 11px Arial, Verdana, Sans-serif;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">

    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/UnitedPharmaService.svc" />
        </Services>
    </telerik:RadScriptManager>    


    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server" Skin="Office2007" />
    
    
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="CustomerList">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="CustomerList" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="GridSalemen">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="GridSalemen" LoadingPanelID="RadAjaxLoadingPanel1" />
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
    <div>
        <fieldset id="fld1" style="clear:both">
            <legend>Select option filter</legend>
            Select an option:&nbsp;
            <telerik:RadComboBox ID="ddlSelect" runat="server" AutoPostBack="true" 
                onselectedindexchanged="ddlSelect_SelectedIndexChanged">
                <Items>
                    <telerik:RadComboBoxItem Value="1" Text="Customers" />
                    <telerik:RadComboBoxItem Value="2" Text="Sales" />
                </Items>
            </telerik:RadComboBox>
            <asp:Panel ID="Panel1" runat="server">
                <table>
                    <tr>
                        <td style="width:115px;">Filter By Name</td>
                        <td colspan="3">
                            <asp:Panel ID="pnFilterName" runat="server" DefaultButton="btnFilterName">
                                <asp:TextBox ID="txtFilterName" runat="server" Width="250px"></asp:TextBox>
                                <telerik:RadButton ID="btnFilterName" runat="server" Text="Apply"></telerik:RadButton>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr id="trCustomerType" runat="server" visible="true">
                        <td>Filter By Customer Type</td>
                        <td><telerik:RadComboBox ID="ddlCustomerType" runat="server" AutoPostBack="true" 
                                Width="253px" onselectedindexchanged="ddlCustomerType_SelectedIndexChanged"></telerik:RadComboBox></td>
                        <td>Filter By Channel</td>
                        <td><telerik:RadComboBox ID="ddlChannel" runat="server" AutoPostBack="true" Width="253px"></telerik:RadComboBox></td>
                    </tr>
                    <tr>
                        <td>Filter By Group</td>
                        <td><telerik:RadComboBox ID="ddlGroup" runat="server" AutoPostBack="true" Width="253px"></telerik:RadComboBox></td>
                        <td>Filter By Region</td>
                        <td><telerik:RadComboBox ID="ddlRegion" runat="server" AutoPostBack="true" Width="253px"></telerik:RadComboBox></td>
                    </tr>
                    <tr>
                        <td>Filter By Area</td>
                        <td><telerik:RadComboBox ID="ddlArea" runat="server" AutoPostBack="true" Width="253px"></telerik:RadComboBox></td>
                        <td>Filter By Local</td>
                        <td><telerik:RadComboBox ID="ddlLocal" runat="server" AutoPostBack="true" Width="253px"></telerik:RadComboBox></td>
                    </tr>
                </table>
                <telerik:RadGrid ID="CustomerList" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="50" OnNeedDataSource="CustomerList_NeedDataSource">
                    <MasterTableView DataKeyNames="Id" TableLayout="Fixed">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                            <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />      
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Full Name" />
                            <telerik:GridBoundColumn DataField="CustomerTypeName" HeaderText="Customer Type" />
                            <telerik:GridBoundColumn DataField="SupervisorName" HeaderText="Supervisor Name" />
                            <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />
                            <telerik:GridBoundColumn DataField="supervisorPhone" HeaderText="Supervisor Phone" />          
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
                </telerik:RadGrid>

                <telerik:RadGrid ID="GridSalemen" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="50" Visible="false" OnNeedDataSource="GridSalemen_NeedDataSource">
                    <MasterTableView DataKeyNames="Id" TableLayout="Fixed">
                        <Columns>
                            <telerik:GridClientSelectColumn UniqueName="CheckboxSelectColumn" FooterText="CheckBoxSelect footer" />
                            <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />      
                            <telerik:GridBoundColumn DataField="FullName" HeaderText="FullName" />
                            <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UpiCode" />  
                            <telerik:GridBoundColumn DataField="RoleName" HeaderText="Role" />        
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="True"></Scrolling>
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevAndNumeric" AlwaysVisible="true" />
                </telerik:RadGrid>
            </asp:Panel>
        </fieldset>
        <div style="margin-top: 4px; padding-right:13px; text-align: right; clear:both">
            <button title="Submit" id="close" onclick="returnToParent(); return false;">Submit</button>
        </div>
    </div>
    <%--<script type="text/javascript">
        document.getElementById("GridSalemen_GridData").removeAttribute('style');
    </script>--%>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            function returnToParent() {
                var selectOption = document.getElementById("<%= ddlSelect.ClientID %>").value;
                var RadGrid1 = (selectOption == "Customers") ? $find("<%=CustomerList.ClientID %>") : $find("<%=GridSalemen.ClientID %>");
                var phoneNumbers = '';
                for (var i = 0; i < RadGrid1.get_masterTableView().get_selectedItems().length; i++) {
                    var MasterTable = RadGrid1.get_masterTableView();
                    var row = MasterTable.get_selectedItems()[i];
                    var cell = MasterTable.getCellByColumnUniqueName(row, "Phone");
                    if (cell.innerHTML != "&nbsp;") {
                        phoneNumbers += cell.innerHTML + ',';
                    }
                    else {
                        continue;
                    }
                }
                var oWnd = GetRadWindow();
                if (oWnd && phoneNumbers) {
                    var selectedPhone = phoneNumbers.slice(0, -1);

                    var searchOptionValue = (selectOption == "Customers") ? 1 : 2;

                    var fullName = document.getElementById("<%= txtFilterName.ClientID %>").value;

                    var customerTypeId = 0;
                    var customerTypeObject = $find("<%= ddlCustomerType.ClientID %>");
                    if (customerTypeObject) {
                        customerTypeId = parseInt(customerTypeObject.get_value());
                        if (!customerTypeId)
                            customerTypeId = 0;
                    }

                    var channelId = 0;
                    var channelObject = $find("<%= ddlChannel.ClientID %>");
                    if (channelObject) {
                        channelId = parseInt(channelObject.get_value());
                        if (!channelId)
                            channelId = 0;
                    }

                    var groupId = 0;
                    var groupObject = $find("<%= ddlGroup.ClientID %>");
                    if (groupObject) {
                        groupId = parseInt(groupObject.get_value());
                        if (!groupId)
                            groupId = 0;
                    }

                    var regionId = 0;
                    var regionObject = $find("<%= ddlRegion.ClientID %>");
                    if (regionObject) {
                        regionId = parseInt(regionObject.get_value());
                        if (!regionId)
                            regionId = 0;
                    }

                    var areaId = 0;
                    var areaObject = $find("<%= ddlArea.ClientID %>");
                    if (areaObject) {
                        areaId = parseInt(areaObject.get_value());
                        if (!areaId)
                            areaId = 0;
                    }

                    var localId = 0;
                    var localObject = $find("<%= ddlLocal.ClientID %>");
                    if (localObject) {
                        localId = parseInt(localObject.get_value());
                        if (!localId)
                            localId = 0;
                    }

                    var promoId = parseInt(getParameterByName("promoId"));

                    // Save to
//                    UnitedPharmaService.AddPromotionBrowseHistory(searchOptionValue, fullName, customerTypeId,
//                        channelId, groupId, regionId, areaId, localId, promoId, selectedPhone, onSuccess, onFail, null)

                    UnitedPharmaService.AddPromotionBrowseHistory(searchOptionValue, fullName, customerTypeId,
                        channelId, groupId, regionId, areaId, localId, promoId, selectedPhone, onSuccess, onFail, null)

                    var listArgs = new Array();
                    listArgs[0] = selectedPhone;
                    listArgs[1] = promoId;
                    oWnd.close(listArgs);
                }
            }

            function onSuccess(result) {
            }

            function onFail(result) {
                alert("Can not update search history");
            }

            function getParameterByName(name) {
                name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                var regexS = "[\\?&]" + name + "=([^&#]*)";
                var regex = new RegExp(regexS);
                var results = regex.exec(window.location.search);
                if (results == null)
                    return "";
                else
                    return decodeURIComponent(results[1].replace(/\+/g, " "));
            }

            -->
        </script>
    </telerik:RadCodeBlock>
    

    </form>
</body>
</html>
