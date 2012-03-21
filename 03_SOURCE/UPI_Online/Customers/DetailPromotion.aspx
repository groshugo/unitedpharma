<%@ page language="C#" autoeventwireup="true" inherits="Customers_DetailPromotion, App_Web_mrrqiiar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View detail Promotion</title>
    <style type="text/css">
        .LeftColumn{width:20%; float:left; font-weight:bold; }
        .RightColumn{width:80%; float:right;}
        .Rowformat{background-color:#AEC7FF; height:25px; padding:5px}
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
            -->
        </script>
    </telerik:RadCodeBlock>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Rowformat">
            <div class="LeftColumn">Start Date:</div>
            <div class="RightColumn"><asp:Literal  runat="server" ID="ltrStartDate"></asp:Literal></div>
        </div>
        <div style="height:1px; width:100%"></div>
        <div class="Rowformat">
            <div class="LeftColumn">End Date:</div>
            <div class="RightColumn"><asp:Literal  runat="server" ID="ltrEndDate"></asp:Literal></div>
        </div>
        <div style="padding-top:10px;">
            <asp:Literal  runat="server" ID="ltrContent"></asp:Literal>
        </div>
    </form>
</body>
</html>
