using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class UserControls_ucNewTask : System.Web.UI.UserControl
{
    SMSObjRepository SMSRepo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            StringBuilder sb = new StringBuilder();
            bool flag = false;
            int usID = ((ObjLogin)Session["objLogin"]).Id;
            string PhoneNumber = ((ObjLogin)Session["objLogin"]).Phone;
            int SMSUnread = SMSRepo.CountSMSUnread(usID,PhoneNumber);
            sb.Append("<ul class='newtask'>");
            if (SMSUnread > 0)
            {
                sb.Append("<li><a href='Administrator/Default.aspx'>New message (<span>" + SMSUnread + "</span>)</a></li>");
                flag = true;
            }
            sb.Append("</ul>");
            ltrNewTask.Text = sb.ToString();

            if (flag == true)
                ltrNewTask.Visible = true;
            else
                ltrNewTask.Visible = false;
        }
    }
}