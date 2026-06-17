using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_frmblksiab : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnPerfil_Click(object sender, EventArgs e)
    {
        string gUser = Session["rut"].ToString();//boton pruebas
        Response.Redirect("~/contenido/RRHH/GestionPersonas.aspx?key=" +gUser + "&cadena=");
    }
}