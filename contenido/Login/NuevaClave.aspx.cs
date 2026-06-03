using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Login_NuevaClave : System.Web.UI.Page
{
    private string aluser;
    private string alhosp;

    Mensaje mens = new Mensaje();
    Usuarios usr = new Usuarios();

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.lblTit.Text = modConstantes.gsTitAB;

        if (Request.QueryString["user"] != null && Request.QueryString["hosp"] != null)
        {
            aluser = Request.QueryString["user"].ToString();
            alhosp = Request.QueryString["hosp"].ToString();
        }
        else
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Write("<script language='javascript'>location='../Login.aspx';</script>");
        }


    }

    protected void Ingresar_Click(object sender, EventArgs e)
    {

        mfAgregar();

    }


    private void mfAgregar()
    {
        if (usr.ObtenerClave(aluser, alhosp) != this.Told.Text)
        {
            mens.mensaje(Page, "Clave Actual no corresponde... ");
            return;
        }

        if (this.Told.Text != "" && this.TNew.Text != "" && this.TRNew.Text != "")
        {
            if (this.TNew.Text != this.TRNew.Text)
            {
                mens.mensaje(Page, "Nueva clave y confirmación son distintos... ");
                return;
            }

            string lsRet = usr.mfGuardar(aluser, alhosp, this.TNew.Text);

            if (lsRet != "")
                mens.mensaje(Page, "Error: Al Actualizar clave....");
            else
            {

                //mens.mensaje(Page, "Clave actualizada con Exito.. ");
                Response.Redirect("~/contenido/Login/VolverLogin.aspx");
            }
        }
        else
        {
            mens.mensaje(Page, "Debe de ingresar todos los antecedentes necesarios... ");
            return;
        }

    }


}