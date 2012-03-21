using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Administrator_ViewDetailSMS : System.Web.UI.Page
{
    SMSObjRepository repo = new SMSObjRepository();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            if (!IsPostBack)
            {
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
                        Response.Redirect("~/Administrator/Default.aspx");
                    }
                }
                catch
                {
                    Response.Redirect("~/Administrator/Default.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("~/Administrator/Default.aspx");
        }
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            Response.Redirect("~/Administrator/SMSReply.aspx?ID=" + Request.QueryString["ID"]);
        }
        else
        {
            Response.Redirect("~/Administrator/Default.aspx");
        }
        
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != null)
        {
            Response.Redirect("~/Administrator/SMSForward.aspx?ID=" + Request.QueryString["ID"]);
        }
        else
        {
            Response.Redirect("~/Administrator/Default.aspx");
        }        
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}