<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDashboardDetails.aspx.cs"
    Inherits="Administrator_ViewDashboardDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dashboard Details</title>
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
<body style="width: 650px; height: 400px;">
    <form id="form1" runat="server" method="post">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" DecoratedControls="All" runat="server"
        Skin="Office2007" />
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
                var oWnd = GetRadWindow();
                oWnd.close("1");
            }
            function ClosePopup() {
                var oWnd = GetRadWindow();
                oWnd.close();
            }

            -->
        </script>
    </telerik:RadCodeBlock>

    <telerik:RadAjaxLoadingPanel runat="server" Transparency="25" ID="RadAjaxLoadingPanel1"
        CssClass="RadAjax RadAjax_Vista">
        <div class="raDiv">
        </div>
        <div class="raColor raTransp">
        </div>
    </telerik:RadAjaxLoadingPanel>
    <table cellpadding="5" cellspacing="0" style="margin: 10px;">
        <tr>
            <td valign="top">
                Title:
            </td>
            <td>
                <asp:Literal ID="litTitle" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                Content:
            </td>
            <td>
                <asp:Literal ID="litContent" runat="server" />
            </td>
        </tr>
        <tr>
            <td valign="top">
                Attached file:
            </td>
            <td>
                <asp:HyperLink runat="server" ID="AttachedFile"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Sender Phone Number:
            </td>
            <td>
                <asp:Literal runat="server" ID="litSenderPhoneNumber"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Receiver Phone Number:
            </td>
            <td>
                <asp:Literal runat="server" ID="litReceiverPhoneNumber"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <button onclick="ClosePopup()">
                    Close</button><br />
                <asp:Label runat="server" Text="" ID="lblMessage"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
