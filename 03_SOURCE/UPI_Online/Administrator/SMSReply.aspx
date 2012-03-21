<%@ page title="Reply SMS" language="C#" masterpagefile="~/Administrator/SMS.master" autoeventwireup="true" inherits="Administrator_SMSReply, App_Web_wvzyt0rb" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" Runat="Server">
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <style>
        .sms_header
        {
            background: url(Images/nav_bg.png) repeat-x;
        }
    </style>
        <script type="text/javascript">
            <!--
            $(document).ready(function () {
                var len = $("#<%=txtReplyContent.ClientID %>").val().length;
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
            -->
        </script>
    </telerik:RadCodeBlock>
    <h3 style="padding-left:10px">Reply SMS</h3>
    <telerik:RadFormDecorator ID="FormDecorator1" runat="server" DecoratedControls="all" Skin="Office2007"></telerik:RadFormDecorator>
    <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSendSMS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelReplySMS" LoadingPanelID="RadAjaxLoadingPanel1" />
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
    <asp:Panel ID="PanelReplySMS" runat="server">
        <div style="padding-left:10px; padding-top:10px;">
            <table>
                <tr>
                    <td>Receive Phone number</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtPhoneNumber" runat="server" Width="600px" ReadOnly="true"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Title</td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtTitle" runat="server" Width="600px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Message reply</td>                
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtReplyContent" runat="server" TextMode="MultiLine" onkeyup="countChar(this)" 
                            Height="150px" Width="600px" style="max-height:150px; max-width:600px; min-height:150px; min-width:600px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSendSMS" runat="server" Text="Send" OnClick="btnSendSMS_Click" />
                        <asp:Button ID="btnAbort" runat="server" Text ="Cancel" OnClick="btnAbort_Click" />
                    </td>
                </tr>
            </table>
            <div>
                <h2>Remaining : <span id="charaterleft">150</span> characters</h2>
                <span>(You can send 150 characters per sms)</span>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>

