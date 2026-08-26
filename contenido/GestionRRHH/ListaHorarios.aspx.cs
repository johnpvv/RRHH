using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_ListaHorarios : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTurnos tur = new ClassTurnos();
    ClassHorarios hor = new ClassHorarios();
    ClassTrabajadores usr = new ClassTrabajadores();
    Mensaje mens = new Mensaje();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            CargarHorarios();
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarHorarios();
    }

    protected void btnCrear_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/GestionRRHH/GestionHorarios.aspx?key=0");
    }
    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }
    private void CargarHorarios()
    {
        hor.ls_descrip = this.txtDescr.Text;
        dgData.DataSource = hor.mfBuscarHorarios();
        dgData.DataBind();
    }

    protected void dgData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Editar")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            Response.Redirect("~/contenido/GestionRRHH/GestionHorarios.aspx?key=" + index.ToString());
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