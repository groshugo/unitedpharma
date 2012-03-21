<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ucSalemenDashboard, App_Web_4qxrpxci" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadPanelBar runat="server" ID="RadPanelBar1" Height="60px" ExpandMode="FullExpandedItem"
    Skin="Office2007" Style="margin: 0 0 5px 0px;" OnItemClick="RadPanelBar1_ItemClick">
    <Items>
        <telerik:RadPanelItem Text="Dashboard" ImageUrl="../Images/dashboard.png" Expanded="True">            
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>
