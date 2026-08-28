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
            CargarSincronizaciones();
        }
    }
    #region Crear SIncronizacion

    private void CargarCentrosAdmin()
    {
        usr.ls_iduser = Session["user"].ToString();
        DataSet ds = usr.mfBuscarCentrosAdmin();

        ddlCentroSincroniza.Items.Clear();
        ddlCentroSincroniza.Items.Add(new ListItem("-- Seleccione centro / unidad --", "0"));

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return;

        ddlCentroSincroniza.DataSource = ds;
        ddlCentroSincroniza.DataTextField = "DESCRIPCION";
        ddlCentroSincroniza.DataValueField = "CODUNIOP";
        ddlCentroSincroniza.DataBind();

        ddlCentroSincroniza.Items.Insert(0, new ListItem("-- Seleccione centro / unidad --", "0"));
    }
    private void InicializarRelojAdmin()
    {
        ddlRelojSincroniza.Items.Clear();
        ddlRelojSincroniza.Items.Add(new ListItem("-- Seleccione reloj --", "0"));
    }
    protected void ddlCentroSincroniza_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarRelojesSincroniza();
    }
    private void CargarRelojesSincroniza()
    {
        rlj.ls_unidad = ddlCentroSincroniza.SelectedValue;

        ddlRelojSincroniza.DataSource = rlj.mfBuscarRelojesCentro();
        ddlRelojSincroniza.DataTextField = "DESCRIPCION";
        ddlRelojSincroniza.DataValueField = "IDRELOJ";
        ddlRelojSincroniza.DataBind();

        ddlRelojSincroniza.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
    }
    protected void btnSincronizar_Click(object sender, EventArgs e)
    {
        DateTime dtInicio;
        DateTime dtFin;

        if (ddlCentroSincroniza.SelectedValue == "0")
        {
            mens.mensaje(Page, "Seleccione un centro o unidad.");
            return;
        }

        if (ddlRelojSincroniza.SelectedValue == "0")
        {
            mens.mensaje(Page, "Seleccione un reloj.");
            return;
        }

        if (!DateTime.TryParse(txtFechaInicio.Text, out dtInicio))
        {
            mens.mensaje(Page, "Ingrese una fecha de inicio válida.");
            return;
        }

        if (!DateTime.TryParse(txtFechaFin.Text, out dtFin))
        {
            mens.mensaje(Page, "Ingrese una fecha de término válida.");
            return;
        }

        if (dtInicio > dtFin)
        {
            mens.mensaje(Page, "La fecha de inicio no puede ser mayor a la fecha de término.");
            return;
        }

        rlj.ls_idreloj = ddlRelojSincroniza.SelectedValue;
        rlj.ls_f_ini = dtInicio.ToString("dd/MM/yyyy");
        rlj.ls_f_fin = dtFin.ToString("dd/MM/yyyy");
        rlj.ls_iduserweb = Session["user"].ToString();

        try
        {
            DataSet ds = rlj.mfSincronizarMarcas();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                if (dr["IDESTADO"].ToString() == "1")
                {
                    mens.mensaje(Page, "Sincronización realizada correctamente. ");
                    this.lblSincr.Text = "<img src='../../../imagenes/check.png'/> Leídas: " + dr["CANT_LEIDA"].ToString() + " | Insertadas: " + dr["CANT_INSERT"].ToString();
                    CargarSincronizaciones();
                }
            }
        }
        catch (Exception ex)
        {
            mens.mensaje(Page, "No fue posible realizar la sincronización: " + ex.Message);
            this.lblSincr.Text = "<img src='../../../imagenes/close.png'/> Ha ocurrido un error en la sincronización: " + ex.Message;
        }
    }
    #endregion

    #region cargar sincronizaciones
    private void CargarSincronizaciones()
    {
        DataSet ds;
        ds = rlj.mfBuscarSincronizaciones();
        dgSincronizacion.DataSource = ds;
        dgSincronizacion.DataBind();
    }
    protected void dgSincronizacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VER")
        {
            int fila = Convert.ToInt32(e.CommandArgument);
            int idSincroniza = Convert.ToInt32(dgSincronizacion.DataKeys[fila].Value);
            rlj.ls_idsincroniza = idSincroniza.ToString();
            DataSet ds = rlj.mfBuscarMarcasSincronizacion();
            dgMarcasSincronizacion.DataSource = ds;
            dgMarcasSincronizacion.DataBind();
            lblIdSincronizacion.Text = " #" + idSincroniza;
            pnlDetalleSincronizacion.Visible = true;
        }
    }
    #endregion
}