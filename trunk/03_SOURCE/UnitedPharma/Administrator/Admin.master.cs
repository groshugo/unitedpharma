using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_Admin : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];            
            if (!IsPostBack)
            {
                LoadTrvFunction(adm.Id);
                CheckPermission(adm.Id);
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }

    private void CheckPermission(int adminId)
    {
        string action = System.IO.Path.GetFileName(Page.Request.FilePath).ToLower();
        if (action != Constant.ADM_MANAGEMENT_PAGE.ToLower())
        {
            AssignFunctionRepository repo = new AssignFunctionRepository();
            if (!repo.CheckPermission(adminId, action))
            {
                Response.Redirect("~/Administrator/AdministratorPanel.aspx");
            }
        }
        
    }

    private void LoadTrvFunction(int AdminId)
    {
        AssignFunctionRepository repo = new AssignFunctionRepository();
        radTrvFunction.DataSource = repo.GetListAssignedFunction(AdminId);
        radTrvFunction.DataBind();
    }

    protected void radTrvFunction_NodeClick(object sender, RadTreeNodeEventArgs e)
    {
        string redirectPage = e.Node.Value;
        if (!String.IsNullOrEmpty(redirectPage))
        {
            Response.Redirect(redirectPage);
        }
    }    
}
