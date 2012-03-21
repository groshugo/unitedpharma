<%@ Page Title="View SMS Detail" Language="C#" MasterPageFile="~/Administrator/SMS.master"
    AutoEventWireup="true" CodeFile="ViewDetailSMS.aspx.cs" Inherits="Administrator_ViewDetailSMS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="float: left; height: 35px;">
        <telerik:RadButton runat="server" ID="btnReply" Text="Reply" Skin="Office2007" ToolTip="Reply"
            OnClick="btnReply_Click">
        </telerik:RadButton>
        <telerik:RadButton runat="server" ID="btnForward" Text="Forward" Skin="Office2007"
            ToolTip="Forward" OnClick="btnForward_Click" Style="margin-left: 5px;">
        </telerik:RadButton>
    </div>
    <div style="float: right; height: 35px; padding-right:10px">
        <telerik:RadButton ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" Skin="Office2007"></telerik:RadButton>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            </div>
        </ItemTemplate>
    </telerik:RadListView>
</asp:Content>
