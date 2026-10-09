using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class contenido_Administracion_ListaUniOperativa : System.Web.UI.Page
{
    private string id;
    private String stcadena = String.Empty;
    Mensaje mens = new Mensaje();
    static bool edicion;
    ClassUnidOperativa UniOp = new ClassUnidOperativa();
    protected void Page_Load(object sender, EventArgs e)
    {
        String lsPer = "";
        string gUsr;
        string asCodSistema;
        modFunciones modfunc = new modFunciones();
        if (Request.Params["__EVENTTARGET"] == "KeyEnterPostBack")
        {
            mfBuscar();
        }
        if (!IsPostBack)
        {
            //gUsr = Session["user"].ToString();
            //asCodSistema = Session["codHosp"].ToString();
            //Session.Add("lsGrabar", "SI");
            //mfBuscar();
            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();
                Session.Add("lsGrabar", "SI");
                lsPer = modfunc.fnValidaUsrApp("MANT_UNIDAD", gUsr, asCodSistema);
                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }
                id = Request.QueryString["id"].ToString();
                mfBuscar();
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
    }
    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cadena = string.Empty;
        stcadena = "";
        stcadena = stcadena + "&TID=" + TID.Text
                            + "&TServicio=" + TServicio.Text;
        cadena = modFunciones.Encriptar(stcadena);
        Response.Redirect("~/contenido/Administracion/GestUniOperativa.aspx?key=" + dgData.SelectedRow.Cells[0].Text + "&cadena=" + cadena);
    }
    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
        }
    }
    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
    }
    private string _sortDirection;
    private string _sortExpression;
    protected void dgData_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["SortDirection"] == null || ViewState["SortExpression"].ToString() != e.SortExpression)
        {
            ViewState["SortDirection"] = "ASC";
            dgData.PageIndex = 0;
        }
        else if (ViewState["SortDirection"].ToString() == "ASC")
        {
            ViewState["SortDirection"] = "DESC";
        }
        else if (ViewState["SortDirection"].ToString() == "DESC")
        {
            ViewState["SortDirection"] = "ASC";
        }
        ViewState["SortExpression"] = e.SortExpression;
        mfBuscar();
    }
    protected void ElimClasif(object sender, EventArgs e)
    {
        if (edicion) return;
        if (Session["lsGrabar"].ToString() == "NO")
        {
            mens.mensaje(Page, "NO está autorizado para eliminar.");
            return;
        }
        try
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.NamingContainer;
            UniOp.IdUnidadOperativa = Convert.ToInt32(row.Cells[0].Text);
            UniOp.IdEstado = 3;
            if (UniOp.Eli_Rest_UnidadOperativa())
            {
                mfBuscar();
                mens.mensaje(Page, "Centro eliminado exitosamente.");
            }
            else
            {
                mens.mensaje(Page, "Error: Problemas al eliminar Centro.");
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    protected void RehabClasif(object sender, EventArgs e)
    {
        if (edicion) return;
        if (Session["lsGrabar"].ToString() == "NO")
        {
            mens.mensaje(Page, "NO está autorizado para Rehabilitar.");
            return;
        }
        try
        {
            ImageButton boton = (ImageButton)sender;
            GridViewRow row = (GridViewRow)boton.NamingContainer;
            UniOp.IdUnidadOperativa = Convert.ToInt32(row.Cells[0].Text);
            UniOp.IdEstado = 1;
            if (UniOp.Eli_Rest_UnidadOperativa())
            {
                mfBuscar();
                mens.mensaje(Page, "Centro rehabilitado exitosamente.");
            }
            else
            {
                mens.mensaje(Page, "Error: Problemas al Rehabilitar Centro.");
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        try
        {
            mfBuscar();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    private void mfBuscar()
    {
        DataTable uni = new DataTable();
        if (!ckElim.Checked) UniOp.IdEstado = 1; else UniOp.IdEstado = 3;
        UniOp.UnidadSuperior = TID.Text;
        UniOp.NombreUnidad = TServicio.Text;
        uni = UniOp.busca_uni_op(UniOp);
        if (uni != null && uni.Rows.Count > 0)
        {
            DataView dv = uni.DefaultView;
            if (ViewState["SortDirection"] != null)
            {
                _sortDirection = ViewState["SortDirection"].ToString();
            }
            if (ViewState["SortExpression"] != null)
            {
                _sortExpression = ViewState["SortExpression"].ToString();
                dv.Sort = string.Concat(_sortExpression, " ", _sortDirection);
            }
            this.dgData.DataSource = dv;
            this.dgData.DataBind();
            this.lblTotal.Text = uni.Rows.Count.ToString() + " Registro/s";
        }
        else
        {
            this.dgData.DataSource = null;
            this.dgData.DataBind();
            this.lblTotal.Text = "0 Registro/s";
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/Administracion/GestUniOperativa.aspx?key=0");
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
    protected void btnExportarExcel_Click(object sender, EventArgs e)
    {
        ClassExcelExportar excel = new ClassExcelExportar();
        DataTable dt;
        if (!ckElim.Checked) UniOp.IdEstado = 1; else UniOp.IdEstado = 3;
        UniOp.UnidadSuperior = TID.Text;
        UniOp.NombreUnidad = TServicio.Text;
        dt = UniOp.busca_uni_op(UniOp);

        if (dt == null || dt.Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para exportar.");
            return;
        }
        excel.Exportar(dt, "Listado_Centros_RRHH");
    }
}