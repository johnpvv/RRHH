using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_GestionReloj : System.Web.UI.Page
{
    ClassReloj reloj = new ClassReloj();
    Mensaje mens = new Mensaje();
    ClassUnidOperativa uop = new ClassUnidOperativa();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarCentros();
            string key = Request.QueryString["key"];
            if (!string.IsNullOrEmpty(key) && key != "0")
            {
                CargarReloj(key);
            }
            else
            {
                ddlEstado.SelectedValue = "1";
            }
            if (Request.QueryString["ok"] == "1")
                mens.mensaje(Page, "Los datos del reloj se guardaron correctamente.");
        }
    }
    private void CargarCentros()
    {
        DataSet ds = uop.mfBuscarCentros();

        ddlCentro.DataSource = ds;
        ddlCentro.DataTextField = "DESCRIPCION";
        ddlCentro.DataValueField = "CODUNIOP";
        ddlCentro.DataBind();

        ddlCentro.Items.Insert(0, new ListItem("Sin asignar", ""));
    }

    private void CargarReloj(string id)
    {
        int idReloj;
        if (!int.TryParse(id, out idReloj) || idReloj <= 0)
        {
            mens.mensaje(Page, "El código del reloj no es válido.");
            return;
        }

        reloj.ls_idreloj = id;
        DataSet ds = reloj.mfBuscarRelojPorId();

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No se encontró el reloj solicitado.");
            return;
        }

        DataRow dr = ds.Tables[0].Rows[0];

        txtDescripcion.Text = dr["DESCRIPCION"].ToString();
        txtIp.Text = dr["IP"].ToString();
        txtPuerto.Text = dr["PUERTO"].ToString();
        txtSerie.Text = dr["SERIE"].ToString();

        string centro = dr["CODUNIOP"] == DBNull.Value ? "" : dr["CODUNIOP"].ToString();
        if (ddlCentro.Items.FindByValue(centro) != null)
            ddlCentro.SelectedValue = centro;

        ddlEstado.SelectedValue = dr["IDESTADO"].ToString();
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        int puerto, serie;

        if (string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
            string.IsNullOrWhiteSpace(txtIp.Text))
        {
            mens.mensaje(Page, "Debe ingresar la descripción y la dirección IP.");
            return;
        }

        if (!int.TryParse(txtPuerto.Text, out puerto) || puerto < 1 || puerto > 65535)
        {
            mens.mensaje(Page, "Ingrese un puerto válido entre 1 y 65535.");
            return;
        }

        if (!int.TryParse(txtSerie.Text, out serie) || serie < 0)
        {
            mens.mensaje(Page, "Ingrese un número de serie válido.");
            return;
        }

        reloj.ls_descrip = txtDescripcion.Text.Trim();
        reloj.ls_ip = txtIp.Text.Trim();
        reloj.ls_puerto = puerto.ToString();
        reloj.ls_serie = serie.ToString();
        reloj.ls_unidad = ddlCentro.SelectedValue;
        reloj.ls_estado = ddlEstado.SelectedValue;

        string key = Request.QueryString["key"];
        string resultado;

        if (string.IsNullOrEmpty(key) || key == "0")
        {
            string idNuevo;
            resultado = reloj.mfInsertarReloj(out idNuevo);

            if (string.IsNullOrEmpty(resultado))
                Response.Redirect("GestionReloj.aspx?key=" + idNuevo + "&ok=1");
        }
        else
        {
            int idReloj;
            if (!int.TryParse(key, out idReloj) || idReloj <= 0)
            {
                mens.mensaje(Page, "El código del reloj no es válido.");
                return;
            }

            reloj.ls_idreloj = key;
            resultado = reloj.mfModificarReloj();

            if (string.IsNullOrEmpty(resultado))
                Response.Redirect("GestionReloj.aspx?key=" + key + "&ok=1");
        }

        if (!string.IsNullOrEmpty(resultado))
            mens.mensaje(Page, "No fue posible guardar el reloj: " + resultado);
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/Administracion/ListarRelojes.aspx");
    }
}