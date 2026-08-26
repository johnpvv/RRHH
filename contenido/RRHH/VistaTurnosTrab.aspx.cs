using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_RRHH_VistaTurnosTrab : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTurnos tur = new ClassTurnos();
    ClassTrabajadores usr = new ClassTrabajadores();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarMeses();
            CargarAnios();
            CargarTurnosTrab();
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
    private void CargarTurnosTrab()
    {
        usr.ls_rut = Session["rut"].ToString();
        tur.ls_user = usr.mfDevuelveID();

        DataSet dsTurno = tur.mfBuscarTurnoActivoTrab();

        if (dsTurno == null || dsTurno.Tables.Count == 0 || dsTurno.Tables[0].Rows.Count == 0)
        {
            dgData.DataSource = null;
            dgData.DataBind();
            return;
        }

        DataRow dr = dsTurno.Tables[0].Rows[0];

        string idTurno = dr["IDTURNOS"].ToString();
        string tipoTurno = dr["TIPO"].ToString();

        DataSet ds;

        tur.ls_mes = ddlMes.SelectedValue;
        tur.ls_anio = ddlAnio.SelectedValue;
        tur.ls_idturno = idTurno;

        if (tipoTurno == "1")
        {            
            ds = tur.mfBuscarTurnosTrabMes();
        }
        else
        {
            ds = tur.mfBuscarTurnosTrab();
            this.ddlAnio.Enabled = false;
            this.ddlMes.Enabled = false;
        }

        dgData.DataSource = ds;
        dgData.DataBind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        //rlj.ls_mes = ddlMes.SelectedValue;
        //rlj.ls_anio = ddlAnio.SelectedValue;
        CargarTurnosTrab();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }
}