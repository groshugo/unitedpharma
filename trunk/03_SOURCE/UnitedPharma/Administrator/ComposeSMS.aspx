<%@ Page Title="Compose SMS" Language="C#" MasterPageFile="~/Administrator/SMS.master"
    AutoEventWireup="true" CodeFile="ComposeSMS.aspx.cs" Inherits="Administrator_Compose" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>Compose SMS</title>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentCommand" runat="Server">
    <h3>
        Compose SMS</h3>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .sms_header
        {
            background: url(Images/nav_bg.png) repeat-x;
        }
    </style>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all"
        Skin="Office2007"></telerik:RadFormDecorator>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            $(document).ready(function () {
                var len = $("#<%=txtContent.ClientID %>").val().length;
                if (len > 150) {
                    val.value = val.value.substring(0, 150);
                } else {
                    $('#charaterleft').text(150 - len);
                }
            });
            function countChar(val) {
                var len = val.value.length;
                if (len > 150) {
                    val.value = val.value.substring(0, 150);
                } else {
                    $('#charaterleft').text(150 - len);
                }
            };
            function openWin() {
                var oWnd = radopen("DialogPhoneNumber.aspx", "RadWindow1");
                oWnd.Maximize();
            }
            function OnClientClose(oWnd, args) {
                var arg = args.get_argument();
                var Phone = "";
                for (var i = 0; i < arg.length; i++) {
                    if (CheckExistedPhoneNumber(arg[i]) == false) {
                        Phone += arg[i] + ",";
                    }
                }
                var currentListPhone = $("#<% =txtPhoneNumber.ClientID%>").val();
                currentListPhone = (currentListPhone != "") ? currentListPhone + "," + Phone : Phone;
                currentListPhone = currentListPhone.substring(0, currentListPhone.length - 1);
                $("#<% =txtPhoneNumber.ClientID%>").val(currentListPhone);
                $("#<% =hdfPhoneNumbers.ClientID%>").val(currentListPhone);
                InitiateAsyncRequest(currentListPhone);
                if (currentListPhone != "") {
                    $("#<%= SchedulePhoneNumbers.ClientID %>").show();
                }
                else {
                    $("#<%= SchedulePhoneNumbers.ClientID %>").hide();
                }
            }
            function CheckExistedPhoneNumber(element) {
                var currentListPhone = $("#<% =txtPhoneNumber.ClientID%>").val();
                currentListPhone = currentListPhone.split(',');
                for (var c = 0; c < currentListPhone.length; c++) {
                    if (element == currentListPhone[c]) {
                        return true;
                    }
                }
                return false;
            }
            function InitiateAsyncRequest(argument) {
                var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
                ajaxManager.ajaxRequest(argument);
                return true;
            }

            function HandleScrolling(e) {
                var grid = $find("<%=SchedulePhoneNumbers.ClientID %>");
                var scrollArea = document.getElementById("<%= SchedulePhoneNumbers.ClientID %>" + "_GridData");
                if (IsScrolledToBottom(scrollArea)) {
                    var currentlyDisplayedRecords = grid.get_masterTableView().get_pageSize() * (grid.get_masterTableView().get_currentPageIndex() + 1);
                    if (currentlyDisplayedRecords < 100) {
                        $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("LoadMoreRecords");
                    }
                }
            }
            function IsScrolledToBottom(scrollArea) {
                var currentPosition = scrollArea.scrollTop + scrollArea.clientHeight;
                return currentPosition == scrollArea.scrollHeight;
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose"
                Width="770" Height="605" NavigateUrl="DialogPhoneNumber.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        OnAjaxRequest="RadAjaxManager1_AjaxRequest">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbPhoneNumber">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbCustomerType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlCustomerType">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbChannel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlChannel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlGroup">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlRegion">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlArea">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="rdbLocal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlLocal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadPanel1" LoadingPanelID="RadAjaxLoadingPanel1" />
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
    <asp:Panel ID="RadPanel1" runat="server">
        <div style="width: 820px; position: relative">
            <table>
                <tr>
                    <td>
                        Promotion
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlPromotion" runat="server" AutoPostBack="true" Width="650px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Subject
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtSubject" runat="server" Width="645px">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Content
                    </td>
                    <td>
                        <div>
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="647px" Height="100px"
                                onkeyup="countChar(this)"></asp:TextBox>
                        </div>
                        <div>
                            <div style="float: left">
                                <h2>
                                    Remaining : <span id="charaterleft">150</span> characters</h2>
                                <span>(You can send 150 characters per sms)</span>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        To:
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPhoneNumber" Width="645px" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <button onclick="openWin(); return false;">
                                        Browse</button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <telerik:RadGrid ID="SchedulePhoneNumbers" runat="server" Skin="Office2007" AllowMultiRowSelection="true"
                                    AutoGenerateColumns="false" Width="800px" Visible="false" AllowPaging="true"
                                    PageSize="10">
                                    <PagerStyle Visible="false" />
                                    <MasterTableView DataKeyNames="Phone">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Phone" HeaderText="Phone" />
                                            <telerik:GridBoundColumn DataField="CustomerName" HeaderText="FullName" MaxLength="250" />
                                            <telerik:GridBoundColumn DataField="UpiCode" HeaderText="UpiCode" />
                                            <telerik:GridBoundColumn DataField="FullName" HeaderText="Supervisor" />
                                            <telerik:GridBoundColumn DataField="PositionName" HeaderText="Position" />
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True">
                                        </Scrolling>
                                        <%--<ClientEvents OnScroll="HandleScrolling" />--%>
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div style="float: right; position: absolute; top: 5px; right: 15px">
        <telerik:RadButton ID="btnSendSMS" runat="server" Text="Send SMS" Skin="Office2007">
        </telerik:RadButton>
        <telerik:RadButton ID="btnAbort" runat="server" Text="Abort" Skin="Office2007">
        </telerik:RadButton>
    </div>
    <asp:HiddenField ID="hdfPhoneNumbers" runat="server" />
</asp:Content>
