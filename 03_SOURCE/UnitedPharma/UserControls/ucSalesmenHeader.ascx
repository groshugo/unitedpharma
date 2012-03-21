<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSalesmenHeader.ascx.cs"
    Inherits="UserControls_ucSalesmenHeader" %>
<div class="ucHeader">
    <div class="ucHeaderLeft">
        <a href="../Salemans/Default.aspx">
            <img style="border: 0;" alt="Logo Salesmen" src="../Images/Logo/Salesmen_Logo.png" />
        </a>
    </div>
    <div class="ucHeaderRight">
        Welcome back,&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblUsername"></asp:Label>
        <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click">| Logout</asp:LinkButton>
    </div>
    <div class="clear">
    </div>
</div>
