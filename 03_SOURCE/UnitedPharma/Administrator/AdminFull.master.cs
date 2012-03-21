using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Administrator_AdminFull : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["objLogin"] != null)
        {
            ObjLogin adm = (ObjLogin)Session["objLogin"];            
            if (!IsPostBack)
            {                
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
        if (String.Equals(action, "customerspage.aspx"))
        {
            action = "customermanagement.aspx";
        }
        else if (String.Equals(action, "salesmenpage.aspx"))
        {
            action = "salesmanmanagement.aspx";
        }
        if (action != Constant.ADM_MANAGEMENT_PAGE.ToLower())
        {
            AssignFunctionRepository repo = new AssignFunctionRepository();
            if (!repo.CheckPermission(adminId, action))
            {
                Response.Redirect("~/Administrator/AdministratorPanel.aspx");
            }
        }
        
    }    
}
