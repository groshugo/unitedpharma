<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucSalesmenMenu.ascx.cs" Inherits="UserControls_ucSalesmenMenu" %>
<script type="text/javascript">
    $(document).ready(function () {
        var curr = '<%= (Session["currentMenu"] == null) ? "mSms" : Session["currentMenu"].ToString()%>';
        $('#' + curr).addClass('current');
    });           
</script>
<div class="nav_div">
    <ul class="nav_menu">
        <li><a id="mSms" href="../Salemans/Default.aspx">SMS List</a></li>
        <li><a id="mPromotion" href="../Salemans/ShowPromotion.aspx">View Promotions</a></li>
        <li><a id="mCustomer" href="../Salemans/CustomersManagement.aspx">Customer Management</a></li>
        <%--<li><a id="mDashboard" href="#">Dashboard</a></li>
        <li><a id="mAdministrator" href="../Salemans/CustomerManagement.aspx">Administrator</a></li>
        <li><a id="mSalesmen" href="../Salemans/SalesmenPage.aspx">Salesmen</a></li>
        <li><a id="mCustomer" href="../Salemans/CustomersPage.aspx">Customers</a></li>
        <li><a href="#">Report & Chart / Export Data</a></li>--%>
    </ul>
    <div class="clear">
    </div>
</div>
