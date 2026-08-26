using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_GestionRRHH_GestionSincronizacion : System.Web.UI.Page
{
    ClassTrabajadores usr = new ClassTrabajadores();
    ClassReloj rlj = new ClassReloj();
    Mensaje mens = new Mensaje();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarCentrosAdmin();
            InicializarRelojAdmin();
        }
    }
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