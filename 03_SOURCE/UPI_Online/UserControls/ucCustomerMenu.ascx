<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ucCustomerMenu, App_Web_4qxrpxci" %>
<script type="text/javascript">
    $(document).ready(function () {
        var curr = '<%= (Session["currentMenu"] == null) ? "mSms" : Session["currentMenu"].ToString()%>';
        $('#' + curr).addClass('current');
    });           
</script>
<div class="nav_div">
    <ul class="nav_menu">
        <li><a id="mSms" href="../Customers/Default.aspx">SMS List</a></li>
        <li><a id="mPromotion" href="../Customers/ShowPromotion.aspx">View Promotion</a></li>
        <%--<li><a id="mImport" href="#">Import/Log System</a></li>
        <li><a id="mPromotion" href="../Customers/PromotionManagement.aspx">Promotion</a></li>
        <li><a id="mDashboard" href="#">Dashboard</a></li>
        <li><a id="mAdministrator" href="../Customers/CustomerManagement.aspx">Administrator</a></li>
        <li><a id="mSalesmen" href="../Customers/SalesmenPage.aspx">Salesmen</a></li>
        <li><a id="mCustomer" href="../Customers/CustomersPage.aspx">Customers</a></li>--%>
        <%--<li><a href="#">Report & Chart / Export Data</a></li>--%>
    </ul>
    <div class="clear">
    </div>
</div>
