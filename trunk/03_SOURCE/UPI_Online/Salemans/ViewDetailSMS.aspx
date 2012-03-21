<%@ page title="View Detail SMS" language="C#" masterpagefile="~/Salemans/SalesmenMaster.master" autoeventwireup="true" inherits="Salemans_ViewDetailSMS, App_Web_z5rbp2l2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCommand" runat="Server">
    <div style="float: left; height: 35px;">
        <telerik:radbutton runat="server" id="btnReply" text="Reply" skin="Office2007" tooltip="Reply"
            onclick="btnReply_Click">
        </telerik:radbutton>
        <telerik:radbutton runat="server" id="btnForward" text="Forward" skin="Office2007"
            tooltip="Forward" onclick="btnForward_Click" style="margin-left: 5px;">
        </telerik:radbutton>
    </div>
    <div style="float: right; height: 35px;">
        <telerik:radbutton runat="server" id="btnBack" text="Back" skin="Office2007"
            tooltip="Back" onclick="btnBack_Click" style="margin-left: 5px;">
        </telerik:radbutton>
    </div>
    <div class="clear">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:radlistview runat="server" id="lvSMS">
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
    </telerik:radlistview>
</asp:Content>
