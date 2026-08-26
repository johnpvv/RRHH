using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_GestionRelojes : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTrabajadores usr = new ClassTrabajadores();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarRelojes();
        }
    }
    private void CargarRelojes()
    {

        //usr.ls_rut = Session["rut"].ToString();
        //rlj.ls_iduser = usr.mfDevuelveID();
        rlj.ls_serie = this.txtSerie.Text;
        rlj.ls_ip=this.txtIp.Text;
        rlj.ls_descrip=this.txtDescr.Text;
        dgData.DataSource = rlj.mfBuscarRelojes();
        dgData.DataBind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarRelojes();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }
}