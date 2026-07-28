using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_ListaTurnos : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTurnos tur = new ClassTurnos();
    ClassUsuarios usr = new ClassUsuarios();
    Mensaje mens = new Mensaje();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            CargarTurnos();
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarTurnos();
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
        CrearTurno();
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }
    private void CargarTurnos()
    {
        tur.ls_codigo = this.txtCod.Text;
        tur.ls_descrip = this.txtDescr.Text;
        tur.ls_turno = "";
        dgData.DataSource = tur.mfBuscarTurnos();
        dgData.DataBind();
    }
    private void CrearTurno()
    {
        string ret;
        tur.ls_codigo = this.txtCod.Text;
        tur.ls_descrip = this.txtDescr.Text;
        if (tur.ls_codigo == "" || tur.ls_descrip == "")
        {
            mens.mensaje(Page, "Debe Llenar los Campos para Crear Un Turno Nuevo... ");
        }
        else
        {
            ret = tur.mfCrearTurnos();
            if (ret == "")
            {
                mens.mensaje(Page, "Turno Insertado OK... ");
                this.txtCod.Text = "";
                this.txtDescr.Text = "";
                CargarTurnos();
            }
            else
            {
                mens.mensaje(Page, "Ha Ocurrido un error al Insertar... ");
            }
        }
    }
    protected void dgData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Response.Redirect("~/contenido/GestionRRHH/GestionTurnos.aspx?key=" + index.ToString());
        }
    }
    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = e.Row.RowState.ToString();
        }
    }
}