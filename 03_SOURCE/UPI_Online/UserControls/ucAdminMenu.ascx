<%@ control language="C#" autoeventwireup="true" inherits="UserControls_ucAdminMenu, App_Web_4qxrpxci" %>
<script type="text/javascript">
    $(document).ready(function () {
        var curr = '<%= (Session["currentMenu"] == null) ? "mSms" : Session["currentMenu"].ToString()%>';
        $('#' + curr).addClass('current');
    });           
</script>
<div class="nav_div">
    <ul class="nav_menu">
        <li><a id="mSms" href="../Administrator/Default.aspx">SMS List</a></li>
        <li><a id="mPromotion" href="../Administrator/SchedulePromotionManagement.aspx">Promotion</a></li>
        <li><a id="mDashboard" href="../Administrator/DashboardManagement.aspx">Dashboard</a></li>
        <li><a id="mAdministrator" href="../Administrator/AdministratorPanel.aspx">Administrator</a></li>
        <li><a id="mSalesmen" href="../Administrator/SalesmenPage.aspx">Salesmen</a></li>
        <li><a id="mCustomer" href="../Administrator/CustomersPage.aspx">Customers</a></li>
        <%--<li><a href="#">Report & Chart / Export Data</a></li>--%>
    </ul>
    <div class="clear">
    </div>
</div>
