<%@ WebHandler Language="C#" Class="ValidCustomer" %>

using System;
using System.Web;

public class ValidCustomer : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string phone = context.Request.QueryString["PHONE"];        
        string type = context.Request.QueryString["TYPE"];
        if (String.Equals(type, "1"))
        {
            var repo = new CustomersRepository();
            if (repo.GetCustomerByPhone(phone))
            {
                string password = "12345"; //for test
                //string password = Utility.CreateRandomPassword(5);
                if (repo.SetPassword(phone, password))
                {                    
                    SmsHandler sh = new SmsHandler();
                    sh.SendSMS(phone, Constant.MSG_CUSTOMER_PASSWORD + password);
                    context.Response.Write(1);
                }
                else
                {
                    context.Response.Write(-1);
                }
            }
            else
            {
                context.Response.Write(-1);
            }
        }
        else if (String.Equals(type, "2"))
        {
            string password = context.Request.QueryString["PWD"];            
            var repo = new CustomersRepository();
            ObjLogin cust = repo.CheckLoginSerialize(phone, password);
            if (cust != null)
            {
                context.Session["objLogin"] = cust;
                repo.SetPassword(phone, String.Empty);
                context.Response.Write(1);
            }
            else
            {
                context.Response.Write(-1);
            }            
        }
        else
        {
            context.Response.Write(-1);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}