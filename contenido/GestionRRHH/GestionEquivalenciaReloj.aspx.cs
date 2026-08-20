using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_GestionRRHH_GestionEquivalenciaReloj : System.Web.UI.Page
{
    ClassUsuarios usr = new ClassUsuarios();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region Botones
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {

    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {

    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
    

    protected void btnCargarReloj_Click(object sender, EventArgs e)
    {

    }

    protected void btnBuscarRRHH_Click(object sender, EventArgs e)
    {
        CargarUsuariosRRHH();
    }
    #endregion
    protected void dgReloj_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgRRHH_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

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
    protected void dgRRHH_RowCommand(object sender,GridViewCommandEventArgs e)
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
            lblEstadoEquivalencia.Text = "Reloj: sin selección → RRHH: " + dr["NOMBRE"].ToString();
            // Guardamos que hay un usuario seleccionado
            hdIdUsuario.Value = dr["IDUSUARIO"].ToString();
        }
    }
}