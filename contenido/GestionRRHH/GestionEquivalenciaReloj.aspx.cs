using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_GestionRRHH_GestionEquivalenciaReloj : System.Web.UI.Page
{
    ClassTrabajadores usr = new ClassTrabajadores();
    ClassReloj rlj = new ClassReloj();
    Mensaje mens = new Mensaje();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.btnRegistrar.Enabled = false;
        if (!IsPostBack)
        {
            CargarCentros();
            InicializarReloj();
            CargarCentrosAdmin();
            InicializarRelojAdmin();
        }
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
        LimpiarEquivalencia();
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

    #region varios
    protected void dgReloj_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgRRHH_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void mfEstEquivalencia()//LLENA EL LABEL CON LOS RESULTADOS DE LA SELECCION DE EQUIVLENCIA DE PERSONAS
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
    protected void LimpiarEquivalencia()
    {
        // CENTRO / RELOJ
        ddlCentro.SelectedIndex = 0;
        ddlReloj.Items.Clear();
        ddlReloj.Items.Add(new ListItem("-- Seleccione reloj --", "0"));

        txtIP.Text = "";
        txtPuerto.Text = "";
        txtIdReloj.Text = "";

        // FILTROS RELOJ
        txtFiltroCodigoReloj.Text = "";
        txtFiltroNombreReloj.Text = "";

        // FILTROS ADMINISTRAR
        ddlCentroAdmin.SelectedIndex = 0;
        ddlRelojAdmin.Items.Clear();
        ddlRelojAdmin.Items.Add(new ListItem("-- Seleccione reloj --", "0"));

        txtFiltroCodigo.Text = "";
        txtFiltroNombre.Text = "";
        txtFiltroRut.Text = "";

        // GRID RELOJ
        txtIdTrabSeleccionado.Text = "";
        txtNombreTrabSeleccionado.Text = "";
        txtCodigoSeleccionado.Text = "";
        dgReloj.DataSource = null;
        dgReloj.DataBind();

        // GRID RRHH
        txtIdUsuario.Text = "";
        txtRutSeleccionado.Text = "";
        txtNombreUsuario.Text = "";
        dgRRHH.DataSource = null;
        dgRRHH.DataBind();

        // Administrar EQUIVALENCIAS
        txtFiltroCodigo.Text = "";
        txtFiltroNombre.Text = "";
        txtFiltroRut.Text = "";
        dgEquivalencias.DataSource = null;
        dgEquivalencias.DataBind();

        // SELECCIONES
        hdIdUsuario.Value = "";
        hdIdUsrReloj.Value = "";
        hdIdReloj.Value = "";

        // LABELS
        lblEstadoEquivalencia.Text = "Reloj: Sin Selección → RRHH: Sin Selección";
        lblMensaje.Text = "";
    }
    #endregion

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

    #region RRHH Escritorio Walter Equivalencias
    private void InicializarReloj()
    {
        ddlReloj.Items.Clear();
        ddlReloj.Items.Add(new ListItem("-- Seleccione reloj --", "0"));

        LimpiarDatosReloj();
    }
    private void CargarCentros()
    {
        usr.ls_iduser = Session["user"].ToString();
        DataSet ds = usr.mfBuscarCentrosAdmin();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return;

        ddlCentro.DataSource = ds;
        ddlCentro.DataTextField = "DESCRIPCION";
        ddlCentro.DataValueField = "CODUNIOP";
        ddlCentro.DataBind();
        ddlCentro.Items.Insert(0, new ListItem("-- Seleccione centro / unidad --", "0"));
    }
    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlReloj.Items.Clear();

        if (ddlCentro.SelectedValue == "0")
            return;
        rlj.ls_unidad = ddlCentro.SelectedValue;
        DataSet ds = rlj.mfBuscarRelojesCentro();

        ddlReloj.DataSource = ds;
        ddlReloj.DataTextField = "DESCRIPCION";
        ddlReloj.DataValueField = "IDRELOJ";
        ddlReloj.DataBind();
        ddlReloj.Items.Insert(0, new ListItem("-- Seleccione Reloj--", "0"));
    }

    protected void ddlReloj_SelectedIndexChanged(object sender, EventArgs e)
    {
        LimpiarDatosReloj();

        if (ddlReloj.SelectedValue == "0")
            return;

        rlj.ls_idreloj = ddlReloj.SelectedValue;
        DataSet ds = rlj.mfBuscarDatosReloj();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return;
        DataRow dr = ds.Tables[0].Rows[0];
        txtIP.Text = dr["IP"].ToString();
        txtPuerto.Text = dr["PUERTO"].ToString();
        txtIdReloj.Text = dr["IDRELOJ"].ToString();
        CargarTrabajadoresReloj();
    }
    private void LimpiarDatosReloj()
    {
        txtIP.Text = "";
        txtPuerto.Text = "";
        txtIdReloj.Text = "";
        dgReloj.DataSource = null;
        dgReloj.DataBind();
    }
    private void CargarTrabajadoresReloj()
    {
        rlj.ls_codigo = this.txtFiltroCodigoReloj.Text.Trim();
        rlj.ls_nombre = this.txtFiltroNombreReloj.Text.Trim();
        rlj.ls_idreloj = ddlReloj.SelectedValue;
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

    #region Administrar Equivalencias
    private void CargarEquivalencias()
    {
        if (ddlRelojAdmin.SelectedValue == "0")
            return;
        rlj.ls_idreloj = ddlRelojAdmin.SelectedValue;
        DataSet ds = rlj.mfBuscarEquivalencias();

        dgEquivalencias.DataSource = ds;
        dgEquivalencias.DataBind();
    }
    protected void dgEquivalencias_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Eliminar")
            return;

        rlj.ls_iduserreloj = e.CommandArgument.ToString();
        rlj.ls_iduserweb = Session["user"].ToString();
        string resultado = rlj.mfDesactivarEquivalencia();
        CargarEquivalencias();
    }
    private void CargarCentrosAdmin()
    {
        usr.ls_iduser = Session["user"].ToString();
        DataSet ds = usr.mfBuscarCentrosAdmin();

        ddlCentroAdmin.Items.Clear();
        ddlCentroAdmin.Items.Add(new ListItem("-- Seleccione centro / unidad --", "0"));

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return;

        ddlCentroAdmin.DataSource = ds;
        ddlCentroAdmin.DataTextField = "DESCRIPCION";
        ddlCentroAdmin.DataValueField = "CODUNIOP";
        ddlCentroAdmin.DataBind();

        ddlCentroAdmin.Items.Insert(0, new ListItem("-- Seleccione centro / unidad --", "0"));
    }
    private void InicializarRelojAdmin()
    {
        ddlRelojAdmin.Items.Clear();
        ddlRelojAdmin.Items.Add(new ListItem("-- Seleccione reloj --", "0"));
    }
    protected void ddlCentroAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlRelojAdmin.Items.Clear();
        ddlRelojAdmin.Items.Add(new ListItem("-- Seleccione reloj --", "0"));

        dgEquivalencias.DataSource = null;
        dgEquivalencias.DataBind();

        if (ddlCentroAdmin.SelectedValue == "0")
            return;
        rlj.ls_unidad = ddlCentroAdmin.SelectedValue;
        DataSet ds = rlj.mfBuscarRelojesCentro();

        ddlRelojAdmin.DataSource = ds;
        ddlRelojAdmin.DataTextField = "DESCRIPCION";
        ddlRelojAdmin.DataValueField = "IDRELOJ";
        ddlRelojAdmin.DataBind();

        ddlRelojAdmin.Items.Insert(0, new ListItem("-- Seleccione reloj --", "0"));
    }
    protected void ddlRelojAdmin_SelectedIndexChanged(object sender, EventArgs e)
    {
        dgEquivalencias.DataSource = null;
        dgEquivalencias.DataBind();

        if (ddlRelojAdmin.SelectedValue == "0")
            return;
        CargarEquivalencias();
    }
    protected void btnBuscarEquivalencias_Click(object sender, EventArgs e)
    {
        rlj.ls_idreloj = ddlRelojAdmin.SelectedValue;
        rlj.ls_codigo = txtFiltroCodigo.Text.Trim();
        rlj.ls_nombre = txtFiltroNombre.Text.Trim();
        rlj.ls_rut = txtFiltroRut.Text.Trim();
        CargarEquivalencias();
    }
    #endregion
}

