<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ucCustomerHeader, App_Web_4qxrpxci" %>
<div class="ucHeader">
    <div class="ucHeaderLeft">
        <a href="../Customers/Default.aspx">
            <img style="border: 0;" alt="Logo Admin" src="../Images/Logo/Customer_Logo.png" />
        </a>
    </div>
    <div class="ucHeaderRight">
        Welcome back,&nbsp;<asp:Label runat="server" Font-Bold="true" ID="lblUsername"></asp:Label>
        <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click">| Logout</asp:LinkButton>
    </div>
    <div class="clear">
    </div>
</div>
