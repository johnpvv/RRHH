using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_frmtopsiab : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["nombre"] != null)
        {
            this.lblUsr.Text = Session["nombre"].ToString();
            this.lblTit.Text = modConstantes.gsTitAB;
            this.lblSed.Text = modConstantes.gsInst;
        }
        else
        {
            Response.Write("<script language='javascript'>self.parent.location='Login.aspx';</script>");
        }
    }

    protected void cierrasesion_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
        Response.Write("<script language='javascript'>self.parent.location='Login.aspx';</script>");
    }

    protected void Idhome_Click(object sender, EventArgs e)
    {
        if (Session["user"] != null && Session["serv"] != null)
        {
            string user = Session["user"].ToString();
            //string serv = Session["serv"].ToString();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            //Response.Redirect("~/contenido/frmblksiab.aspx");

            Response.Write("<script type='text/javascript'>locaton.reload()</script>");

            //Response.Redirect("~/contenido/Index1.aspx?user=" + user + "&serv=" + serv);
        }
    }
}