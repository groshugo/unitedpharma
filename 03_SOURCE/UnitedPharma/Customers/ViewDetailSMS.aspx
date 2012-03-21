<%@ Page Title="View SMS" Language="C#" MasterPageFile="~/Customers/CustomerMaster.master"
    AutoEventWireup="true" CodeFile="ViewDetailSMS.aspx.cs" Inherits="Customers_ViewDetailSMS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="float: left; height: 35px; width:50%">
                
    </div>
    <div style="float: left; height: 35px; width:50%; text-align:right;">
        <telerik:RadButton runat="server" ID="btnReply" Text="Reply" Skin="Office2007" ToolTip="Reply"
            OnClick="btnReply_Click" Visible="false">
        </telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnBack" Text="Back" Skin="Office2007" ToolTip="Back"
            OnClick="btnBack_Click">
        </telerik:RadButton>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            <!--
            function openWin(id) {
                var oWnd = radopen("DetailPromotion.aspx?id=" + id, "RadWindow2");
            }
            -->
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Office2007" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Behaviors="Close" Width="800" Height="600"
                NavigateUrl="DetailPromotion.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadListView runat="server" ID="lvSMS">
        <ItemTemplate>
            <div class="sms_detail_container">
                <div>
                    <b>From:</b>
                    <%#Eval("SenderName")%>
                    (<%#Eval("SenderPhone")%>)</div>
                <div>
                    <b>To:</b>
                    <%#Eval("ReceiverName")%>
                    (<%#Eval("ReceiverPhone")%>)</div>
                <div>
                    <b>Date:</b><%#Eval("Date")%>
                </div>
                <div>
                    <b>Subject:</b>
                    <%#Eval("Subject")%></div>
                <div class="sms_detail_content">
                    <%#Eval("Content")%>
                </div>
                <div style="position:absolute; top:5px; right:10px">
                    <a href="javascript:void(0);" onclick="openWin(<%# Eval("PromotionId")%>);return false;">View Promotion</a>
                </div>
            </div>
        </ItemTemplate>
    </telerik:RadListView>
</asp:Content>
