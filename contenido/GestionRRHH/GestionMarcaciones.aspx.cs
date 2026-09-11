using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_GestionRRHH_GestionMarcaciones : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    Usuarios usr = new Usuarios();
    ClassTurnos tur = new ClassTurnos();
    ClassHorarios hor = new ClassHorarios();
    ClassReloj rlj = new ClassReloj();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarMeses();
            CargarAnios();
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
    protected void btnVolver_Click(object sender, EventArgs e)//revisar
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
    }

    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
    }
    private void CargarTrabajadoresMarcas()
    {
        rlj.ls_mes = this.ddlMes.SelectedValue;
        rlj.ls_anio = this.ddlAnio.SelectedValue;
        rlj.ls_nombre = this.txtBuscarTrab.Text.Trim();
        DataSet ds = rlj.mfBuscarTrabajadoresMarcas();
        if (ds != null && ds.Tables.Count > 0)
        {
            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = OrdenTrabajadores;
            dgTrabajadores.DataSource = dv;
            dgTrabajadores.DataBind();
            lblTotalTrab.Text = ds.Tables[0].Rows.Count.ToString();
        }
        else
        {
            dgTrabajadores.DataSource = null;
            dgTrabajadores.DataBind();
        }
    }
    private string OrdenTrabajadores
    {
        get
        {
            return ViewState["OrdenTrabajadores"] == null
                ? "NOMBRE ASC" :
                ViewState["OrdenTrabajadores"].ToString();
        }
        set
        {
            ViewState["OrdenTrabajadores"] = value;
        }
    }
    protected void dgTrabajadores_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenActual = OrdenTrabajadores;
        string campoActual = ordenActual.Split(' ')[0];
        string direccionActual = ordenActual.Split(' ')[1];
        if (campoActual == e.SortExpression)
        {
            direccionActual = direccionActual == "ASC" ? "DESC" : "ASC";
        }
        else
        {
            direccionActual = "ASC";
        }
        OrdenTrabajadores = e.SortExpression + " " + direccionActual;
        CargarTrabajadoresMarcas();
    }
    protected void btnBuscarMarcas_Click(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
        divDetalleMarcas.Visible = false;
    }
    protected void dgTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarcasTrabajador();
        CargarCentrosMarca();
    }
    protected void dgTrabajadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgTrabajadores.PageIndex = e.NewPageIndex;
        CargarTrabajadoresMarcas();
    }

    #region grid detalle por trabajador
    private void CargarCentrosMarca()
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (iduser.StartsWith("U:"))
        {
            rlj.ls_iduser = iduser.Substring(2);
            DataSet ds = rlj.mfBuscarRelojesUsuario();

            ddlCentroMarca.DataSource = ds;
            ddlCentroMarca.DataTextField = "CENTRO";
            ddlCentroMarca.DataValueField = "IDUSRRELOJ";
            ddlCentroMarca.DataBind();
        }
    }
    private void CargarMarcasTrabajador()
    {
        string idSeleccion = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        //string idReloj =dgTrabajadores.SelectedDataKey.Values["IDRELOJ"].ToString();
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        rlj.ls_iduser = "";
        rlj.ls_iduserreloj = "";
        //rlj.ls_idreloj = idReloj;

        if (idSeleccion.StartsWith("U:"))
        {
            rlj.ls_iduser = idSeleccion.Substring(2);
            lblTituloMarcas.Text = dgTrabajadores.SelectedRow.Cells[2].Text;
        }
        else if (idSeleccion.StartsWith("R:"))
        {
            rlj.ls_iduserreloj = idSeleccion.Substring(2);
            lblTituloMarcas.Text = idSeleccion.Substring(2) + " (Sin identificación RRHH)";
        }
        DataSet ds = rlj.mfBuscarMarcasTrabajador();
        dgMarcas.DataSource = ds;
        dgMarcas.DataBind();
        lblTotalMarcas.Text = ds.Tables[0].Rows.Count.ToString() + " resultado(s)";
        divDetalleMarcas.Visible = true;
        int ok = 0, entradaRep = 0, salidaRep = 0, faltantes = 0, revisar = 0;
        foreach (DataRow r in ds.Tables[0].Rows)
        {
            string e = r["ESTADO"].ToString();
            if (e == "OK")
                ok++;
            else if
                (e == "ENTRADA REPETIDA") entradaRep++;
            else if
                (e == "SALIDA REPETIDA") salidaRep++;
            else if
                (e.Contains("FALTANTE")) faltantes++;
            else
                revisar++;
        }
        
        lblResumenMarcas.Text =
            "<span>Resumen Marcas: OK: " + ok + ",</span> &nbsp; " +
            "<span>Repetidas: " + (entradaRep + salidaRep) + ",</span> &nbsp; " +
            "<span>Faltantes: " + faltantes + ",</span> &nbsp; " +
            "<span>Para Revisar: " + revisar + ",</span> &nbsp;" +
            "<span>Total: " + (ok + revisar + faltantes + entradaRep + salidaRep) + "</span>";
    }
    protected void dgMarcas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;

        string estado = DataBinder.Eval(e.Row.DataItem, "ESTADO").ToString();

        if (estado == "OK")
            e.Row.Cells[5].CssClass = "marca-ok";
        else if (estado.Contains("FALTANTE"))
            e.Row.Cells[5].CssClass = "marca-alerta";
        else if (estado.Contains("REPETIDA"))
            e.Row.Cells[5].CssClass = "marca-warning";
    }
    protected void dgMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
        DataKey key = dgMarcas.DataKeys[row.RowIndex];

        string id = e.CommandArgument.ToString();
        string idUserReloj = key.Values["CODIGO_EMP_RELOJ"].ToString();
        string idReloj = key.Values["IDRELOJ"].ToString();

        if (e.CommandName == "EditarMarca")
            mfEditarMarca(id);
        else if (e.CommandName == "EliminarMarca")
            mfAnularMarca(id);
        else if (e.CommandName == "InsertarMarca")
            mfInsertarMarca(e.CommandArgument.ToString(), idUserReloj, idReloj);
    }
    private void mfEditarMarca(string id)
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (!iduser.StartsWith("U:"))
        {
            mens.mensaje(Page, "El Trabajador no está registrado en sistema WEB RR.HH, no se puede continuar...");
            return;
        }
        rlj.ls_idmarca = id;
        DataSet ds = rlj.mfBuscarMarca();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow r = ds.Tables[0].Rows[0];
            hdIdMarcacion.Value = id;
            DateTime f = Convert.ToDateTime(r["F_H_MARCA"]);
            txtFechaMarca.Text = f.ToString("dd/MM/yyyy");
            txtHoraMarca.Text = f.ToString("HH:mm:ss");
            ddlTipoMarca.SelectedValue = r["TIPO_MARCA"].ToString();
            ddlCentroMarca.SelectedValue = r["CENTRO"].ToString();
            txtObsMarca.Text = r["OBSERVACIONES"].ToString();
            lblTituloMarca.Text = "Editar Marcación:";
            pnlEditarMarca.Style["display"] = "flex";
        }
    }
    protected void btnCancelarMarca_Click(object sender, EventArgs e)
    {
        pnlEditarMarca.Style["display"] = "none";
    }
    protected void btnGuardarMarca_Click(object sender, EventArgs e)
    {
        rlj.ls_idmarca = hdIdMarcacion.Value;
        rlj.ls_fecha = txtFechaMarca.Text;
        rlj.ls_hora = txtHoraMarca.Text;
        rlj.ls_tipo = ddlTipoMarca.SelectedValue;
        rlj.ls_obs = txtObsMarca.Text.Trim();
        rlj.ls_iduserreloj = hdIdUserReloj.Value;
        rlj.ls_idreloj = hdIdReloj.Value;
        rlj.ls_iduserweb = Session["user"].ToString();

        if (rlj.ls_obs == "")
        {
            rlj.ls_obs = "Marca Modificada Por Analista";
        }

        if (hdIdMarcacion.Value != "")
        {
            rlj.mfActualizarMarca();
        }
        else
        {
            rlj.mfInsertarMarca();
        }
        
        pnlEditarMarca.Style["display"] = "none";
        CargarTrabajadoresMarcas();
        CargarMarcasTrabajador();
    }
    private void mfAnularMarca(string id)
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (!iduser.StartsWith("U:"))
        {
            mens.mensaje(Page, "El Trabajador no está registrado en sistema WEB RR.HH, no se puede continuar...");
            return;
        }
        rlj.ls_idmarca = id;
        rlj.ls_iduserweb = Session["user"].ToString();
        rlj.mfAnularMarca();
        CargarTrabajadoresMarcas();
        CargarMarcasTrabajador();
    }
    private void mfInsertarMarca(string datos, string idUserReloj, string idReloj)
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (!iduser.StartsWith("U:"))
        {
            mens.mensaje(Page, "El Trabajador no está registrado en sistema WEB RR.HH, no se puede continuar...");
            return;
        }
        string[] p = datos.Split('|');
        DateTime fecha = Convert.ToDateTime(p[0]);

        txtFechaMarca.Text = fecha.ToString("dd/MM/yyyy");
        txtHoraMarca.Text = "";
        ddlTipoMarca.SelectedValue = p[1] == "ENTRADA FALTANTE" ? "0" : "1";//si falta entrada , dejarla precargada
        txtObsMarca.Text = "Marca agregada por regularización RRHH";
        ddlCentroMarca.SelectedValue = "";
        hdIdMarcacion.Value = "";
        hdIdUserReloj.Value = idUserReloj;
        hdIdReloj.Value = idReloj;
        lblTituloMarca.Text = "Agregar Marcación:";
        pnlEditarMarca.Style["display"] = "flex";
    }
    protected void btnNuevaMarca_Click(object sender, EventArgs e)
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (!iduser.StartsWith("U:"))
        {
            mens.mensaje(Page, "El Trabajador no está registrado en sistema WEB RR.HH, no se puede continuar...");
            return;
        }
        hdIdMarcacion.Value = "";
        lblTituloMarca.Text = "Agregar Nueva Marca:";
        txtFechaMarca.Enabled = true;
        txtFechaMarca.Text = "";
        txtHoraMarca.Text = "";
        ddlTipoMarca.SelectedValue = "0";
        txtObsMarca.Text = "Marca agregada por regularización RRHH";
        //ddlCentroMarca.SelectedValue = "";
        pnlEditarMarca.Style["display"] = "flex";
    }
    #endregion
}