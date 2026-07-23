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
        Response.Redirect("~/contenido/RRHH/VistaDatosTrabajador.aspx");
    }

    protected void btnMarcaciones_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/RRHH/GestionMarcaciones.aspx");
    }

    protected void btnPermisos_Click(object sender, EventArgs e)
    {

    }

    protected void btnTurnos_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/RRHH/VistaTurnosTrab.aspx");
    }
}