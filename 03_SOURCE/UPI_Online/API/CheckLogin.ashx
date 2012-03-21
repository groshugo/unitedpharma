<%@ WebHandler Language="C#" Class="CheckLogin" %>

using System;
using System.Web;
using System.Linq;

public class CheckLogin : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        string phone = context.Request.QueryString["UID"];
        string password = context.Request.QueryString["PWD"];
        string type = context.Request.QueryString["TYPE"];
        if (String.Equals(type, "1"))
        {
            var repo = new AdministratorRepository();
            ObjLogin adm = repo.CheckLoginSerialize(phone, password);
            if (adm != null)
            {
                context.Session["objLogin"] = adm;                
                context.Response.Write(1);
            }
            else
            {
                context.Response.Write(-1);
            }
        }
        else
        {
            var repo = new SalesmanRepository();
            ObjLogin sal = repo.CheckLoginSerialize(phone);
            if (sal != null)
            {
                context.Session["objLogin"] = sal;
                context.Response.Write(2);
            }
            else
            {
                context.Response.Write(-1);
            }
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