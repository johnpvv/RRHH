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
    //protected void ElimClasif(object sender, EventArgs e)
    //{
    //    if (edicion == true) { return; }
    //    try
    //    {
    //        if (Session["lsGrabar"].ToString() == "NO")
    //        {
    //            mens.mensaje(Page, "NO esta autorizado para eliminar..");
    //        }
    //        else
    //        {
    //            ImageButton boton = (ImageButton)sender;
    //            GridViewRow row = (GridViewRow)boton.NamingContainer;
    //            string cid = row.Cells[0].Text;
    //            if (cid != "" && cid != null)
    //            {
    //                UniOp.IdUnidadOperativa = Convert.ToInt32(cid);
    //            }
    //            UniOp.IdEstado = 3;
    //            int retorno = 0;
    //            retorno = UniOp.Eli_Rest_UnidadOperativa(UniOp);
    //            if (retorno == 0)
    //                mens.mensaje(Page, "Error: Problemas al Eliminar Centro..");
    //            else
    //            {
    //                mfBuscar();
    //                mens.mensaje(Page, "Eliminado Exitosamente.");
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("~/contenido/frmerrgen.aspx");
    //    }
    //}
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
    //protected void ImBtIngresar_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        mfAgregar();
    //    }
    //    catch
    //    {
    //        Response.Redirect("~/contenido/frmerrgen.aspx");
    //    }
    //}
    //private void mfAgregar()//revisar si se elimina o traslada a gestion unidad
    //{
    //    if (this.TServicio.Text == "") { mens.mensaje(Page, "Debe de Ingresar Descripcion de Unidad Operativa.. "); return; }
    //    if (this.TID.Text == "") { mens.mensaje(Page, "Debe de Ingresar Unidad Superior.. "); return; }
    //    if (Convert.ToInt32(UniOp.mfExisteId(this.TID.Text)) == 0)
    //    {
    //        mens.mensaje(Page, "No existe Unidad Superior, favor verifique.. "); return;
    //    }
    //    if (Convert.ToInt32(UniOp.mfExisteUOP(this.TServicio.Text)) > 0) { mens.mensaje(Page, "Debe de Ingresar Nombre Unidad Operativa Única.. "); return; }
    //    UniOp.UnidadSuperior = TID.Text.ToUpper();
    //    UniOp.NombreUnidad = TServicio.Text.ToUpper();
    //    UniOp.IdEstado = 1;
    //    int retorno = 0;
    //    retorno = UniOp.InsertUnidadOperativa(UniOp);
    //    if (retorno == 0)
    //        mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
    //    else
    //    {
    //        this.TServicio.Text = "";
    //        this.TID.Text = "";
    //        mfBuscar();
    //    }
    //}
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
            if (uni.Rows.Count > 0)
            {
                this.dgData.DataSource = uni;
                this.dgData.DataBind();
                this.lblTotal.Text = uni.Rows.Count.ToString() + " Registro/s";
            }
            else
            {
                this.dgData.DataSource = null;
                this.dgData.DataBind();
            }
        }
        else
        {
            this.dgData.DataSource = null;
            this.dgData.DataBind();
        }
    }


    //protected void dgData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    //Validar
    //    int CODUNIOP = (int)e.Keys["CODUNIOP"];
    //    string IDSUP_UNIDAD = (string)e.NewValues["IDSUP_UNIDAD"];
    //    string DESCRIPCION = (string)e.NewValues["DESCRIPCION"];
    //    if (IDSUP_UNIDAD == "") { mens.mensaje(Page, "Debe ingresar Unidad Superior.. "); return; }
    //    if (DESCRIPCION == "") { mens.mensaje(Page, "Debe ingresar Decripcion de Unidad Operativa.. "); return; }
    //    if (Convert.ToInt32(UniOp.mfExisteId(IDSUP_UNIDAD)) == 0)
    //    {
    //        mens.mensaje(Page, "No existe Unidad Superior, favor verifique.. "); return;
    //    }
    //    //ACTUALIZAR
    //    UniOp.UnidadSuperior = IDSUP_UNIDAD;
    //    UniOp.NombreUnidad = DESCRIPCION.ToUpper();
    //    UniOp.IdEstado = 1;
    //    string sal = UniOp.ModificarUnidad(CODUNIOP.ToString(),
    //                                            IDSUP_UNIDAD,
    //                                            DESCRIPCION,
    //                                            "0");
    //    if (sal != "")
    //    {
    //        //infoColor(LblProceso, System.Drawing.Color.OrangeRed, "ERROR AL ACTUALIZAR ARTICULO: " + salida);
    //        mens.mensaje(Page, "ERROR AL ACTUALIZAR REGISTRO !!. Error: " + sal); return;
    //        dgData.EditIndex = -1;
    //        return;
    //    }
    //    dgData.EditIndex = -1;
    //    mfBuscar();
    //    //infoColor(LblProceso, System.Drawing.Color.Green, "REGISTRO ACTUALIZADO EXITOSAMENTE !! ");
    //    mens.mensaje(Page, "REGISTRO ACTUALIZADO EXITOSAMENTE !! "); return;
    //    GridViewRow gvrEdit = dgData.Rows[e.RowIndex];
    //    gvrEdit.BackColor = System.Drawing.Color.LightPink;
    //    edicion = false;
    //}
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