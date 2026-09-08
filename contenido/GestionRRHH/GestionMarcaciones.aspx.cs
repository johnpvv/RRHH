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
            lblTotalTrab.Text = ds.Tables[0].Rows.Count.ToString() + " resultado(s)";
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
            direccionActual = direccionActual == "ASC" ? "DESC": "ASC";
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
    protected void dgTrabajadores_SelectedIndexChanged(object sender,EventArgs e)
    {
        CargarMarcasTrabajador();
    }
    protected void dgTrabajadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgTrabajadores.PageIndex = e.NewPageIndex;
        CargarTrabajadoresMarcas();
    }

    #region grid detalle por trabajador
    private void CargarMarcasTrabajador()
    {
        string idSeleccion =dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        //string idReloj =dgTrabajadores.SelectedDataKey.Values["IDRELOJ"].ToString();
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        rlj.ls_iduser = "";
        rlj.ls_iduserreloj = "";
        //rlj.ls_idreloj = idReloj;

        if (idSeleccion.StartsWith("U:"))
        {
            rlj.ls_iduser = idSeleccion.Substring(2);
        }
        else if (idSeleccion.StartsWith("R:"))
        {
            rlj.ls_iduserreloj = idSeleccion.Substring(2);
        }
        DataSet ds = rlj.mfBuscarMarcasTrabajador();
        dgMarcas.DataSource = ds;
        dgMarcas.DataBind();
        lblTotalMarcas.Text = ds.Tables[0].Rows.Count.ToString() + " resultado(s)";
        divDetalleMarcas.Visible = true;
    }

    #endregion
}