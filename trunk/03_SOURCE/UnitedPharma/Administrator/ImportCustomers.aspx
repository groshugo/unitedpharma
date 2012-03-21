<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportCustomers.aspx.cs" Inherits="Administrator_ImportCustomers" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <div style="padding:10px">
            <b>Chọn file excel:</b>
            <asp:FileUpload ID="fuImport" runat="server" Width="330px" size="40" />
            <asp:Button ID="btnImport" runat="server" Text="Import" CausesValidation="false"
                OnClick="btnImport_Click" Skin="Office2007" />
        </div>
        <div style="height:auto; min-height:350px; max-height:500px; overflow:auto">
        <telerik:RadGrid ID="CustomerList" runat="server" Skin="Office2007">
            <MasterTableView>
                        
            </MasterTableView>
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ClientSettings>
                        
            </ClientSettings>
        </telerik:RadGrid>
        </div>
    </div>
    </form>
</body>
</html>
