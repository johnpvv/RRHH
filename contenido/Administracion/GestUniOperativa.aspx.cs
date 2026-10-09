using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_GestUniOperativa : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    ClassUnidOperativa cu = new ClassUnidOperativa();
    ClassReloj reloj = new ClassReloj();

    private string IdUnidad
    {
        get
        {
            return Session["lsIdentificador"] == null ? "0" : Session["lsIdentificador"].ToString();
        }
    }
    private bool EsNuevo {
        get {
            return IdUnidad == "0";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string key = Request.QueryString["key"];
            Session["lsIdentificador"] = !string.IsNullOrWhiteSpace(key) && key != "0" ? key : "0";
            Session["lbNvo"] = EsNuevo;

            if (EsNuevo)
            {
                TxtNombre.Text = "";
                TxtCodigo.Text = "";
                chk360.Checked = false;
                BtnAgregar.Text = "Agregar";
                this.TabPanel2.Enabled = false;
            }
            else
            {
                mfCargarCentro();
                BtnAgregar.Text = "Modificar";
                mfCargarRelojes();
            }
        }
    }

    private void mfCargarCentro()
    {
        DataSet ds = cu.ConsultarID(IdUnidad);
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No se encontró el centro solicitado.");
            return;
        }

        DataRow row = ds.Tables[0].Rows[0];
        TxtNombre.Text = row["DESCRIPCION"].ToString();
        TxtCodigo.Text = row["IDSUP_UNIDAD"].ToString();
        chk360.Checked = row["EXTIENDE"].ToString() == "1";
    }

    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(TxtNombre.Text))
        {
            mens.mensaje(Page, "Debe ingresar el nombre del centro.");
            return;
        }

        string extiende = chk360.Checked ? "1" : "0";
        string error;

        if (EsNuevo)
        {
            string idNuevo;
            error = cu.InsertarUnidad(TxtCodigo.Text.Trim(), TxtNombre.Text.Trim(), extiende, out idNuevo);

            if (error != "")
            {
                mens.mensaje(Page, "Error al crear el centro: " + error);
                return;
            }

            Session["lsIdentificador"] = idNuevo;
            Session["lbNvo"] = false;
            BtnAgregar.Text = "Modificar";
            this.TabPanel2.Enabled = true;
            mens.mensaje(Page, "Centro creado correctamente. Ya puede asignar relojes.");
        }
        else
        {
            error = cu.ModificarUnidad(IdUnidad, TxtCodigo.Text.Trim(), TxtNombre.Text.Trim(), extiende);

            if (error != "")
            {
                mens.mensaje(Page, "Error al modificar el centro: " + error);
                return;
            }

            mens.mensaje(Page, "Centro modificado correctamente.");
        }

        mfCargarRelojes();
    }

    private void mfConfigurarReloj()
    {
        reloj.ls_unidad = IdUnidad;
        reloj.ls_idreloj = TCodigo.Text.Trim();
        reloj.ls_descrip = TDesc.Text.Trim();
    }

    private void mfCargarRelojes()
    {
        mfCargarRelojesDisponibles();
        mfCargarRelojesAsignados();
    }

    private void mfCargarRelojesDisponibles()
    {
        mfConfigurarReloj();
        DataSet ds = reloj.mfBuscarRelojesDisponibles();
        gdArt.DataSource = ds;
        gdArt.DataBind();
    }

    private void mfCargarRelojesAsignados()
    {
        mfConfigurarReloj();
        DataSet ds = reloj.mfBuscarRelojesCentro();
        gbArtSer.DataSource = ds;
        gbArtSer.DataBind();
    }

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        gdArt.PageIndex = 0;
        gbArtSer.PageIndex = 0;
        mfCargarRelojes();
    }

    protected void btn_Limpiar_Click(object sender, EventArgs e)
    {
        TCodigo.Text = "";
        TDesc.Text = "";
        gdArt.PageIndex = 0;
        gbArtSer.PageIndex = 0;
        mfCargarRelojes();
    }

    protected void AddUnidad(object sender, EventArgs e)
    {
        if (EsNuevo)
        {
            mens.mensaje(Page, "Primero debe guardar el centro para asignar relojes.");
            return;
        }

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string idReloj = gdArt.DataKeys[row.RowIndex].Value.ToString();

        mfConfigurarReloj();
        reloj.ls_idreloj = idReloj;

        string error = reloj.mfAsignarRelojCentro();
        if (error != "")
        {
            mens.mensaje(Page, "Error al asignar el reloj: " + error);
            return;
        }

        mfCargarRelojes();
    }

    protected void ElimUnidad(object sender, EventArgs e)
    {
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string idReloj = gbArtSer.DataKeys[row.RowIndex].Value.ToString();

        mfConfigurarReloj();
        reloj.ls_idreloj = idReloj;

        string error = reloj.mfQuitarRelojCentro();
        if (error != "")
        {
            mens.mensaje(Page, "Error al quitar el reloj: " + error);
            return;
        }

        mfCargarRelojes();
    }

    protected void gdArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdArt.PageIndex = e.NewPageIndex;
        mfCargarRelojesDisponibles();
    }

    protected void gbArtSer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbArtSer.PageIndex = e.NewPageIndex;
        mfCargarRelojesAsignados();
    }

    protected void gdArt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = e.Row.RowState.ToString();
    }

    protected void gbArtSer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.CssClass = e.Row.RowState.ToString();
    }

    protected void gdArt_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gdArt.SelectedIndex >= 0)
        {
            string idReloj = gdArt.DataKeys[gdArt.SelectedIndex].Value.ToString();
            mfAsignarReloj(idReloj);
        }
    }

    protected void gbArtSer_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gbArtSer.SelectedIndex >= 0)
        {
            string idReloj = gbArtSer.DataKeys[gbArtSer.SelectedIndex].Value.ToString();
            mfQuitarReloj(idReloj);
        }
    }

    private void mfAsignarReloj(string idReloj)
    {
        if (EsNuevo)
        {
            mens.mensaje(Page, "Primero debe guardar el centro para asignar relojes.");
            return;
        }

        mfConfigurarReloj();
        reloj.ls_idreloj = idReloj;
        string error = reloj.mfAsignarRelojCentro();

        if (error != "")
            mens.mensaje(Page, "Error al asignar el reloj: " + error);
        else
            mfCargarRelojes();
    }

    private void mfQuitarReloj(string idReloj)
    {
        mfConfigurarReloj();
        reloj.ls_idreloj = idReloj;
        string error = reloj.mfQuitarRelojCentro();

        if (error != "")
            mens.mensaje(Page, "Error al quitar el reloj: " + error);
        else
            mfCargarRelojes();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/Administracion/ListaUniOperativa.aspx?id=0");
    }
}