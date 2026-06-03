using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Login_VolverLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //this.lblTit.Text = modConstantes.gsTitAB;
            id.InnerText = modConstantes.gsTitAB;
        }
        else
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Write("<script language='javascript'>top.location='../Login.aspx';</script>");
        }
    }

    protected void Ingresar_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Response.Write("<script language='javascript'>top.location='../Login.aspx';</script>");
        //Response.Write("<script language='javascript'>location='../Login.aspx';</script>");
    }
}