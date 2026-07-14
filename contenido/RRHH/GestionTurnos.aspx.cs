using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_RRHH_GestionTurnos : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassUsuarios usr = new ClassUsuarios();
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!IsPostBack)
        {            
            CargarMeses();
            CargarAnios();
            rlj.ls_mes = ddlMes.SelectedValue;
            rlj.ls_anio = ddlAnio.SelectedValue;
            CargarMarcaciones();
        }
    }
    private void CargarMeses()
    {
        if (ddlMes.Items.Count == 0)
        {
            ddlMes.Items.Add(new ListItem("Enero", "1"));
            ddlMes.Items.Add(new ListItem("Febrero", "2"));
            ddlMes.Items.Add(new ListItem("Marzo", "3"));
            ddlMes.Items.Add(new ListItem("Abril", "4"));
            ddlMes.Items.Add(new ListItem("Mayo", "5"));
            ddlMes.Items.Add(new ListItem("Junio", "6"));
            ddlMes.Items.Add(new ListItem("Julio", "7"));
            ddlMes.Items.Add(new ListItem("Agosto", "8"));
            ddlMes.Items.Add(new ListItem("Septiembre", "9"));
            ddlMes.Items.Add(new ListItem("Octubre", "10"));
            ddlMes.Items.Add(new ListItem("Noviembre", "11"));
            ddlMes.Items.Add(new ListItem("Diciembre", "12"));
        }

        ddlMes.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void CargarAnios()
    {
        ddlAnio.Items.Clear();

        for (int i = DateTime.Now.Year; i >= 2020; i--)
        {
            ddlAnio.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
    }
    private void CargarMarcaciones()
    {
        
        usr.ls_rut = Session["rut"].ToString();
        rlj.ls_iduser = usr.mfDevuelveID();
        dgData.DataSource = rlj.mfBuscarMarcaciones();
        dgData.DataBind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }



    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        CargarMarcaciones();
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }
}