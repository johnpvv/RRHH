using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_ListarRelojes : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTrabajadores usr = new ClassTrabajadores();
    Mensaje mens = new Mensaje();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarRelojes();
        }
    }
    private void CargarRelojes()
    {

        //usr.ls_rut = Session["rut"].ToString();
        //rlj.ls_iduser = usr.mfDevuelveID();
        rlj.ls_serie = this.txtSerie.Text;
        rlj.ls_ip = this.txtIp.Text;
        rlj.ls_descrip = this.txtDescr.Text;
        dgData.DataSource = rlj.mfBuscarRelojes();
        dgData.DataBind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarRelojes();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/Administracion/GestionReloj.aspx?key=0");
    }
    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.CssClass = e.Row.RowState.ToString();

            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");
            if (btnEditar != null)
            {
                btnEditar.ToolTip = "Editar reloj";
                btnEditar.AlternateText = "Editar";
            }
        }
    }
    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {
        string id = dgData.SelectedDataKey.Value.ToString();
        Response.Redirect("~/contenido/Administracion/GestionReloj.aspx?key=" + id);
    }
    #region excel
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ClassExcelExportar excel = new ClassExcelExportar();
        rlj.ls_serie = this.txtSerie.Text;
        rlj.ls_ip = this.txtIp.Text;
        rlj.ls_descrip = this.txtDescr.Text;
        DataSet ds = rlj.mfBuscarRelojes();

        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para exportar.");
            return;
        }
        excel.Exportar(ds, "Listado_Relojes_RRHH");
    }
    #endregion
}