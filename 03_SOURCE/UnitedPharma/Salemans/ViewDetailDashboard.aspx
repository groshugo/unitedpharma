<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewDetailDashboard.aspx.cs" Inherits="Salemans_ViewDetailDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .BoldText{font-weight:bold;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="background:#AEC7FF; font-size:18px; padding:5px; font-weight:bold"><asp:Label ID="lblTitle" runat="server"></asp:Label></div>
        <div style="padding-top:10px; text-align:justify"><asp:Literal ID="ltrContent" runat="server"></asp:Literal></div>
    </div>
    </form>
</body>
</html>
