<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSalemenDashboard.ascx.cs"
    Inherits="UserControls_ucSalemenDashboard" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadPanelBar runat="server" ID="RadPanelBar1" Height="60px" ExpandMode="FullExpandedItem"
    Skin="Office2007" Style="margin: 0 0 5px 0px;" OnItemClick="RadPanelBar1_ItemClick">
    <Items>
        <telerik:RadPanelItem Text="Dashboard" ImageUrl="../Images/dashboard.png" Expanded="True">            
        </telerik:RadPanelItem>
    </Items>
</telerik:RadPanelBar>
