<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDashboardDetails.aspx.cs"
    Inherits="Administrator_ViewDashboardDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .BoldText{font-weight:bold;}
    </style>
</head>
<body>
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
    <div style="background: #AEC7FF; font-size: 18px; padding: 5px; font-weight: bold">
        <asp:Literal ID="litTitle" runat="server" /></div>
    <div style="padding-top: 10px; text-align: justify">
        <asp:Literal ID="litContent" runat="server" /></div>
    <table style="margin: 10px;">
        <tr>
            <td valign="top" style="width: 150px;">
                <asp:Literal ID="litAttachedFile" runat="server" Text="Attached file:" />
            </td>
            <td>
                <asp:HyperLink runat="server" ID="AttachedFile"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td valign="top" colspan="2">
                <asp:Label runat="server" Text="" ID="lblMessage"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
