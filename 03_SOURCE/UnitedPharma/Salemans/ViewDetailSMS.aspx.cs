using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salemans_ViewDetailSMS : System.Web.UI.Page
{
    SMSObjRepository repo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["T"] != null)
                {
                    if (Request.QueryString["T"] == "0")
                    {
                        btnReply.Visible = false;
                        btnForward.Visible = false;
                    }                    
                }
                Utility.SetCurrentMenu("mSms");
                try
                {
                    ObjLogin adm = (ObjLogin)Session["objLogin"];
                    if (repo.CheckOwner(adm.Phone, Convert.ToInt32(Request.QueryString["ID"])))
                    {
                        List<vwSMS> rs = new List<vwSMS>();
                        repo.GetListParentSMS(Convert.ToInt32(Request.QueryString["ID"]), rs);
                        lvSMS.DataSource = rs;
                        lvSMS.DataBind();
                        repo.SetIsRead(Convert.ToInt32(Request.QueryString["ID"]));
                    }
                    else
                    {
                        Response.Redirect("~/Salemans/Default.aspx");
                    }
                }
                catch
                {
                    Response.Redirect("~/Salemans/Default.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/Salemans/Default.aspx");
        }
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            Response.Redirect("~/Salemans/SMSReply.aspx?ID=" + Request.QueryString["ID"]);
        }
        else
        {
            Response.Redirect("~/Salemans/Default.aspx");
        }

    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            Response.Redirect("~/Salemans/SMSForward.aspx?ID=" + Request.QueryString["ID"]);
        }
        else
        {
            Response.Redirect("~/Salemans/Default.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Salemans/Default.aspx");
    }
}