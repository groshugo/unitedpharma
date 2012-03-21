<%@ page title="Reply SMS" language="C#" masterpagefile="~/Customers/CustomerMaster.master" autoeventwireup="true" inherits="Customers_SMSReply, App_Web_qxy1wy0m" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <telerik:radcodeblock id="RadCodeBlock1" runat="server">
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
            function checknullcontent() {
                var content = $('#<%=txtReplyContent.ClientID %>');
                var title = $('#<%=txtTitle.ClientID %>');
                title = $.trim(title.val());
                content = $.trim(content.val());
                if (content == "" || title=="") {
                    alert('Title or content not allow null');
                    return false;
                }

            }
            -->
        </script>
    </telerik:radcodeblock>
    <h3 style="padding-left: 10px; color:#000;">
        Reply SMS</h3>
    <telerik:radformdecorator id="FormDecorator1" runat="server" decoratedcontrols="all"
        skin="Office2007"></telerik:radformdecorator>
    <telerik:radajaxmanager runat="server" id="RadAjaxManager1" defaultloadingpanelid="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSendSMS">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelReplySMS" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>
    <telerik:radajaxloadingpanel runat="server" transparency="25" id="RadAjaxLoadingPanel1"
        cssclass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:radajaxloadingpanel>
    <asp:Panel ID="PanelReplySMS" runat="server">
        <div style="padding-left: 10px">
            <table>
                <tr>
                    <td>
                        Receive Phone number
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Width="600px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Title
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Message reply
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtReplyContent" runat="server" TextMode="MultiLine" onkeyup="countChar(this)"
                            Height="150px" Width="600px" Style="max-height: 150px; max-width: 600px; min-height: 150px;
                            min-width: 600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSendSMS" runat="server" Text="Send" OnClientClick="checknullcontent()" OnClick="btnSendSMS_Click" />
                        <asp:Button ID="btnAbort" runat="server" Text="Cancel" OnClick="btnAbort_Click" />
                    </td>
                </tr>
            </table>
            <div>
                <h2>
                    Remaining : <span id="charaterleft">150</span> characters</h2>
                <span>(You can send 150 characters per sms)</span>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
