using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_GestionRRHH_GestionEquivalenciaReloj : System.Web.UI.Page
{
    ClassUsuarios usr = new ClassUsuarios();
    ClassReloj rlj = new ClassReloj();
    Mensaje mens = new Mensaje();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnRegistrar.Enabled = false;
    }
    #region Botones
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(hdIdUsrReloj.Value))
        {
            mens.mensaje(Page, "Seleccione un trabajador del reloj.");
            return;
        }

        if (string.IsNullOrWhiteSpace(hdIdUsuario.Value))
        {
            mens.mensaje(Page, "Seleccione un trabajador del sistema RRHH.");
            return;
        }

        rlj.ls_idreloj = hdIdReloj.Value;
        rlj.ls_iduserreloj = hdIdUsrReloj.Value;
        rlj.ls_iduser = hdIdUsuario.Value;
        rlj.ls_iduserweb = Session["user"].ToString();
        string lsRet = rlj.mfRegistrarEquivalencia();

        if (lsRet != "")
        {
            mens.mensaje(Page, lsRet);
            this.lblMensaje.Text = lsRet;
            return;
        }

        mens.mensaje(Page, "Equivalencia registrada correctamente.");
        this.lblMensaje.Text = "Equivalencia registrada correctamente.";
        //LimpiarEquivalencia();
        CargarTrabajadoresReloj();
        CargarUsuariosRRHH();

    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {

    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }


    protected void btnCargarReloj_Click(object sender, EventArgs e)
    {
        CargarTrabajadoresReloj();
    }

    protected void btnBuscarRRHH_Click(object sender, EventArgs e)
    {
        CargarUsuariosRRHH();
    }
    #endregion
    protected void dgReloj_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgRRHH_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void mfEstEquivalencia()
    {
        string codigoReloj = txtCodigoSeleccionado.Text.Trim();
        string nombreReloj = txtNombreTrabSeleccionado.Text.Trim();
        string rutRRHH = txtRutSeleccionado.Text.Trim();
        string nombreRRHH = txtNombreUsuario.Text.Trim();
        string reloj;
        string rrhh;
        // RELOJ
        if (string.IsNullOrWhiteSpace(codigoReloj))
        {
            reloj = "Sin Selección";
        }
        else
        {
            reloj = "(" + codigoReloj + ") " + nombreReloj;
        }

        // RRHH
        if (string.IsNullOrWhiteSpace(rutRRHH))
        {
            rrhh = "Sin Selección";
        }
        else
        {
            rrhh = "(" + rutRRHH + ") " + nombreRRHH;
        }

        lblEstadoEquivalencia.Text = "Reloj: " + reloj + " → RRHH: " + rrhh;
        this.lblMensaje.Text = "";
        // Solo permitir registrar cuando ambos existen
        btnRegistrar.Enabled = !string.IsNullOrWhiteSpace(codigoReloj) && !string.IsNullOrWhiteSpace(rutRRHH);
    }
    #region RRHH WEB
    private void CargarUsuariosRRHH()
    {
        usr.ls_rut = txtRut.Text.Trim();
        usr.ls_nomb = txtNombre.Text.Trim();
        DataSet ds = usr.mfBuscarUsuariosRRHH();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            dgRRHH.DataSource = ds;
            dgRRHH.DataBind();
            lblTotalRRHH.Text = ds.Tables[0].Rows.Count.ToString() + " resultado(s)";
        }
        else
        {
            dgRRHH.DataSource = null;
            dgRRHH.DataBind();
            lblTotalRRHH.Text = "0 resultado(s)";
        }
    }
    protected void dgRRHH_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SeleccionarUsuario")
        {
            string idUsuario = e.CommandArgument.ToString();
            CargarUsuarioRRHH(idUsuario);
        }
    }
    private void CargarUsuarioRRHH(string idUsuario)
    {
        usr.ls_iduser = idUsuario;
        DataSet ds = usr.ConsultarIDUser();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtIdUsuario.Text = dr["IDUSUARIO"].ToString();
            txtRutSeleccionado.Text = dr["RUT"].ToString() + "-" + dr["DV"].ToString();
            txtNombreUsuario.Text = dr["NOMBRE"].ToString();
            mfEstEquivalencia();
            // Guardamos que hay un usuario seleccionado
            hdIdUsuario.Value = dr["IDUSUARIO"].ToString();
        }
    }
    #endregion

    #region RRHH Escritorio Walter
    private void CargarTrabajadoresReloj()
    {
        rlj.ls_codigo = this.txtFiltroCodigoReloj.Text.Trim();
        rlj.ls_nombre = this.txtFiltroNombreReloj.Text.Trim();
        DataSet ds = rlj.mfBuscarTrabajadoresReloj();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            dgReloj.DataSource = ds;
            dgReloj.DataBind();

            lblTotalReloj.Text = ds.Tables[0].Rows.Count.ToString() + " trabajador(es)";
        }
        else
        {
            dgReloj.DataSource = null;
            dgReloj.DataBind();
            lblTotalReloj.Text = "0 trabajador(es)";
        }
    }
    protected void dgReloj_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "SeleccionarReloj")
        {
            string idUsrPend = e.CommandArgument.ToString();
            CargarTrabajadorReloj(idUsrPend);
        }
    }
    private void CargarTrabajadorReloj(string idUsuario)
    {
        rlj.ls_iduser = idUsuario;
        DataSet ds = rlj.mfBuscaTrabRelojID();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].Rows[0];
            txtIdTrabSeleccionado.Text = dr["IDUSRPEND"].ToString();
            txtCodigoSeleccionado.Text = dr["IDUSERRELOJ"].ToString();
            txtNombreTrabSeleccionado.Text = dr["NOMBRE"].ToString();
            mfEstEquivalencia();
            // Guardamos que hay un usuario seleccionado
            hdIdUsrReloj.Value = dr["IDUSERRELOJ"].ToString();
            hdIdReloj.Value = dr["IDRELOJ"].ToString();
        }
    }
    #endregion
}

