using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_RRHH_VistaMarcacionesTrab : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassUsuarios usr = new ClassUsuarios();
    ClassTurnos tur = new ClassTurnos();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarMeses();
            CargarAnios();
            rlj.ls_mes = ddlMes.SelectedValue;
            rlj.ls_anio = ddlAnio.SelectedValue;
            CargarMarcaciones();
        }
    }
    private void CargarMeses()
    {
        DataSet ds = tur.mfGenerarMeses();
        ddlMes.DataSource = ds.Tables[0];
        ddlMes.DataTextField = "MES";
        ddlMes.DataValueField = "IDMES";
        ddlMes.DataBind();
        ddlMes.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void CargarAnios()
    {
        ddlAnio.Items.Clear();
        DataSet ds = tur.mfGenerarAnios();
        ddlAnio.DataSource = ds.Tables[0];
        ddlAnio.DataTextField = "ANIO";
        ddlAnio.DataValueField = "ID";
        ddlAnio.DataBind();
        ddlAnio.SelectedValue = DateTime.Now.Year.ToString();

    }
    private void CargarMarcaciones()
    {

        usr.ls_rut = Session["rut"].ToString();
        rlj.ls_iduser = usr.mfDevuelveID();//en caso de homologar con otro id desde los relojes u otros
        dgData.DataSource = rlj.mfBuscarMarcaciones();
        dgData.DataBind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        CargarMarcaciones();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }
}