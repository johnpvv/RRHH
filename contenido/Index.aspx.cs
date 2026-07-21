using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Index : System.Web.UI.Page
{
    public string usr;
    public string serv;

    protected void Page_Load(object sender, EventArgs e)
    {
        String lsPer = "";
        modFunciones modfunc = new modFunciones();
        string gUsr;
        string asCodSistema;

        if (Session["user"] == null)
        {
            Response.Redirect("~/contenido/Login.aspx");
            return;
        }
        gUsr = Session["user"].ToString();
        asCodSistema = Session["codHosp"].ToString();

        lsPer = modfunc.fnValidaUsrApp("PORTAL_TRAB", gUsr, asCodSistema);
        if (lsPer != "M" && lsPer != "L")
        {
            Session["ModoPortal"] = "ADMIN";
        }
        else
        {            
            Session["ModoPortal"] = "TRABAJADOR";
        }

        if (Session["ModoPortal"].ToString() == "TRABAJADOR")
        {
            theframe.Attributes["cols"] = "*";
            frmMenu.Visible = false;
        }
        else
        {
            theframe.Attributes["cols"] = "240,*";
            frmMenu.Visible = true;
        }
    }
}