<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Login"
    Title="United International Pharmacy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/Signin.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#admin").click(function () {
                $("#hdfLoginType").val("1");
                $("#lblLoginTitle").html("Administrator"); 
                $("#light_bg").fadeIn("slow");
                $("#login_box").fadeIn("slow");
                $("#input_login").val("0909313336");
                $("#input_password").val("123");
                $("body").css("overflow", "hidden");
                $("#input_login").focus();
            });
            $("#salemen").click(function () {
                $("#hdfLoginType").val("2");
                $("#lblLoginTitle").html("Salemens");
                $("#light_bg").fadeIn("slow");
                $("#login_box").fadeIn("slow");
                $("#input_login").val("0987435325");
                $("#input_password").val("123");
                $("body").css("overflow", "hidden");
                $("#input_login").focus();
            });
            $("#customer").click(function () {
                $("#hdfLoginType").val("3");
                $("#lbl_header_cust_1").html("Customers");
                $("#light_bg").fadeIn("slow");
                $("#login_cust_1").fadeIn("slow");
                $("#txtCustPhone").val("0123456789");                
                $("body").css("overflow", "hidden");
                $("#txtCustPhone").focus();
            });
            $("#close_cust_1").click(function () {
                $("#hdfLoginType").val("");
                $("#light_bg").fadeOut("slow");
                $("#login_cust_1").fadeOut("slow");
                $("body").css("overflow", "visible");
            });
            $("#close_cust_2").click(function () {
                $("#hdfLoginType").val("");
                $("#hdfCustPhone").val("");
                $("#light_bg").fadeOut("slow");
                $("#login_cust_2").fadeOut("slow");
                $("body").css("overflow", "visible");
            });
            $("#close").click(function () {
                $("#hdfLoginType").val("");
                $("#light_bg").fadeOut("slow");
                $("#login_box").fadeOut("slow");
                $("body").css("overflow", "visible");
            });
            $("#submit_button").click(function () {
                if ($("#input_login").val() == "") {
                    alert('Please enter phone number!');
                    return;
                }
                if ($("#input_password").val() == "") {
                    alert('Please enter your password!');
                    return;
                }

                $("#loginmask").css('display', 'block');
                $.ajax({
                    type: 'POST',
                    url: "API/CheckLogin.ashx?UID=" + $("#input_login").val() + "&PWD=" + $("#input_password").val() + "&TYPE=" + $("#hdfLoginType").val(),
                    success: function (responsetext) {
                        if (responsetext == "-1") {
                            $("#loginmask").css('display', 'none');
                            alert('Phone or password is incorrect');
                        }
                        else {
                            if (responsetext == "1") {
                                location.href = "Administrator";
                            }
                            if (responsetext == "2") {
                                location.href = "Salemans";
                            }
                        }
                    }
                });
            });
            $("#btnCheckCustPhone").click(function () {
                if ($("#txtCustPhone").val() == "") {
                    alert('Please enter phone number!');
                    return;
                }

                $("#loginmask_cust_1").css('display', 'block');
                $.ajax({
                    type: 'POST',
                    url: "API/ValidCustomer.ashx?PHONE=" + $("#txtCustPhone").val() + "&TYPE=1",
                    success: function (responsetext) {
                        if (responsetext == "-1") {
                            $("#loginmask_cust_1").css('display', 'none');
                            alert('Phone is not exist.');
                        }
                        else {
                            if (responsetext == "1") {
                                $("#loginmask_cust_1").css('display', 'none');
                                $("#login_cust_1").fadeOut("fast");                                
                                $("#hdfLoginType").val("3");
                                $("#hdfCustPhone").val($("#txtCustPhone").val());
                                $("#lbl_header_cust_2").html("Customers");
                                $("#login_cust_2").fadeIn("slow");                                
                                $("#txtCustPassword").val("");
                                $("body").css("overflow", "hidden");
                                $("#txtCustPassword").focus();
                            }                            
                        }
                    }
                });
            });
            $("#btnVerifyPassword  ").click(function () {
                if ($("#txtCustPassword").val() == "") {
                    alert('Please enter phone number!');
                    return;
                }

                $("#loginmask_cust_2").css('display', 'block');
                $.ajax({
                    type: 'POST',
                    url: "API/ValidCustomer.ashx?PHONE=" + $("#hdfCustPhone").val() + "&PWD=" + $("#txtCustPassword").val() + "&TYPE=2",
                    success: function (responsetext) {
                        if (responsetext == "-1") {
                            $("#loginmask_cust_2").css('display', 'none');
                            alert('Invalid Password.');
                        }
                        else {
                            if (responsetext == "1") {
                                location.href = "Customers";
                            }
                        }
                    }
                });
            });                     
        });
        function redirect(url) {
            location.href = url;
        }
    </script>
</head>
<body>
    <form id="form1">
    <div class="form">
        <div>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3">
                        <h3>
                            United International Pharma - SMS based CRM System</h3>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <img src="Images/splash_01.gif" alt="" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="Images/Customer.gif" alt="" id="customer" style="cursor:pointer" />
                    </td>
                    <td>
                        <img src="Images/Sales.gif" alt="" id="salemen" style="cursor:pointer" />
                    </td>
                    <td>
                        <img src="Images/Administrator.gif" alt="" id="admin" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <img src="Images/splash_08.gif" alt="" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="light_bg">
    </div>
    <div class="login_form" id="login_box">
        <div class="login_form_inside">
            <div id="loginmask" class="login_form_mask" style="display: none;">
            </div>
            <div id="close">
            </div>
            <h2 id="lblLoginTitle">
            </h2>
            <div class="input_block">
                <div class="label">
                    <label for="login">
                        Phone number</label>
                </div>
                <div class="field">
                    <input type="text" tabindex="1" name="login" id="input_login" class="text" value="0987435325" />
                </div>
            </div>
            <div class="input_block">
                <div class="label">
                    <label for="password">
                        Password</label>
                </div>
                <div class="field">
                    <input type="password" tabindex="2" name="password" id="input_password" class="text" />
                </div>
                <div class="clearfix">
                </div>
            </div>
            <div class="group" id="login-links">
                <input type="button" tabindex="3" id="submit_button" class="login float_left" />
            </div>
        </div>
    </div>
    <div class="login_form" id="login_cust_1">
        <div class="login_form_inside">
            <div id="loginmask_cust_1" class="login_form_mask" style="display: none;">
            </div>
            <div id="close_cust_1">
            </div>
            <h2 id="lbl_header_cust_1">
            </h2>
            <div class="input_block">
                <div class="label">
                    <label for="login">
                        Phone number</label>
                </div>
                <div class="field">
                    <input type="text" tabindex="1" name="login" id="txtCustPhone" class="text" value="0909313336" />
                </div>
            </div>
            <div class="group" id="Div4">
                <input type="button" tabindex="3" id="btnCheckCustPhone" class="login float_left"
                    style="margin-top: 0;" />
            </div>
        </div>
    </div>
    <div class="login_form" id="login_cust_2">
        <div class="login_form_inside">
            <div id="loginmask_cust_2" class="login_form_mask" style="display: none;">
            </div>
            <div id="close_cust_2">
            </div>
            <h2 id="lbl_header_cust_2">
            </h2>
            <div class="input_block">
                <div>
                    Please check your phone to receive a password.</div>
                <div class="label">
                    <label for="login">
                        Password</label>
                </div>
                <div class="field">
                    <input type="text" tabindex="1" name="login" id="txtCustPassword" class="text" />
                </div>
            </div>
            <div class="group" id="Div5">
                <input type="button" tabindex="3" id="btnVerifyPassword" class="login float_left"
                    style="margin-top: 0;" />
            </div>
        </div>
    </div>
    <input type="hidden" id="hdfLoginType" />
    <input type="hidden" id="hdfCustPhone" />
    </form>
</body>
</html>
