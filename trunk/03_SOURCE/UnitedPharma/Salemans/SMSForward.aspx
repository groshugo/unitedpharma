<%@ Page Title="Forward SMS" Language="C#" MasterPageFile="~/Salemans/SalesmenMaster.master" AutoEventWireup="true" CodeFile="SMSForward.aspx.cs" Inherits="Salemans_SMSForward" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" Runat="Server">
    <style>
        .sms_header
        {
            background: url(../Images/nav_bg.png) repeat-x;
        }
    </style>
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            $(document).ready(function () {
                var len = $("#<%=txtForwardContent.ClientID %>").val().length;
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
                if (arg) {
                    var Phone = "";
                    for (var i = 0; i < arg.length; i++) {
                        Phone += arg[i] + ",";
                    }
                    Phone = Phone.substring(0, Phone.length - 1);
                    $("#<% =txtPhoneNumbers.ClientID%>").val(Phone);
                }
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <h3 style="padding-left:10px">Forward SMS</h3>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" Behaviors="Close" OnClientClose="OnClientClose" Width="850" Height="600"
                NavigateUrl="DialogPhoneNumber.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" Skin="Office2007"></telerik:RadFormDecorator>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSendSMS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelForwardSMS" LoadingPanelID="RadAjaxLoadingPanel1" />
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

    <asp:Panel ID="PanelForwardSMS" runat="server">
        <div style="padding-left:10px; padding-top:10px">
            <table>
                <tr>
                    <td>Phone numbers</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPhoneNumbers" runat="server" Width="600px"></asp:TextBox>
                        <button onclick="openWin(); return false;">Browse</button>
                    </td>
                </tr>
                <tr>
                    <td>Title</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Message forward</td>                
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtForwardContent" runat="server" TextMode="MultiLine" MaxLength="150" onkeyup="countChar(this)" 
                            Height="150px" Width="600px" style="max-height:150px; max-width:600px; min-height:150px; min-width:600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSendSMS" runat="server" Text="Send" OnClick="btnSendSMS_Click" />
                        <asp:Button ID="btnAbort" runat="server" Text="Cancel" OnClick="btnAbort_Click" />
                    </td>
                </tr>
            </table>            
            <div style="width:600px">
                <div style="float:left"><h2>Remaining : <span id="charaterleft">150</span> characters</h2></div>
                <div style="float:right"><span>(You can send 150 characters per sms)</span></div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

