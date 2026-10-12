using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_RRHH_VistaMarcacionesTrab : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTrabajadores usr = new ClassTrabajadores();
    ClassTurnos tur = new ClassTurnos();
    ClassFeriado fer = new ClassFeriado();
    Mensaje mens = new Mensaje();
    private HashSet<DateTime> feriados = new HashSet<DateTime>();
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
        DataSet ds = tur.mfGenerarMeses();
        ddlMes.DataSource = ds.Tables[0];
        ddlMes.DataTextField = "MES";
        ddlMes.DataValueField = "IDMES";
        ddlMes.DataBind();
        ddlMes.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void CargarAnios()
    {
        ddlAnio.Items.Clear();
        DataSet ds = tur.mfGenerarAnios();
        ddlAnio.DataSource = ds.Tables[0];
        ddlAnio.DataTextField = "ANIO";
        ddlAnio.DataValueField = "ID";
        ddlAnio.DataBind();
        ddlAnio.SelectedValue = DateTime.Now.Year.ToString();

    }
    private void CargarFeriados()
    {
        fer.ls_anio = ddlAnio.SelectedValue;
        DataSet ds = fer.mfBuscar();
        feriados.Clear();
        if (ds == null || ds.Tables.Count == 0) return;
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            DateTime fecha;
            if (DateTime.TryParse(dr["FECHA"].ToString(), out fecha))
                feriados.Add(fecha.Date);
        }
    }
    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        DateTime fecha;
        if (!DateTime.TryParse(DataBinder.Eval(e.Row.DataItem, "F_H_MARCA").ToString(), out fecha))
            return;

        if (fecha.DayOfWeek == DayOfWeek.Sunday || feriados.Contains(fecha.Date))
        {
            e.Row.CssClass = "calendario-feriado";
            e.Row.ToolTip = "Feriado";
        }
    }
    protected void dgDataAgrupada_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        DateTime fecha;
        if (!DateTime.TryParse(DataBinder.Eval(e.Row.DataItem, "FECHA").ToString(), out fecha))
            return;

        if (fecha.DayOfWeek == DayOfWeek.Sunday || feriados.Contains(fecha.Date))
        {
            e.Row.CssClass = "calendario-feriado";
            e.Row.ToolTip = "Feriado";
        }
    }
    private void CargarMarcaciones()
    {
        CargarFeriados();//poblar los feriados para destacar los grids
        usr.ls_rut = Session["rut"].ToString();
        rlj.ls_iduser = usr.mfDevuelveID();//en caso de homologar con otro id desde los relojes u otros        
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        if (ddlVistaMarcas.SelectedValue == "1")
        {
            dgData.Visible = true;
            dgDataAgrupada.Visible = false;

            dgData.DataSource = rlj.mfBuscarMarcasReloj();
            dgData.DataBind();
        }
        else
        {
            dgData.Visible = false;
            dgDataAgrupada.Visible = true;

            dgDataAgrupada.DataSource = rlj.mfBuscarMarcasRelojAgrupadas();
            dgDataAgrupada.DataBind();
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarMarcaciones();
    }

    protected void ddlVistaMarcas_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarcaciones();
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarcaciones();
    }

    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMarcaciones();
    }
    #region Exportar
    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ClassExcelExportar excel = new ClassExcelExportar();
        DataSet ds;
        usr.ls_rut = Session["rut"].ToString();
        rlj.ls_iduser = usr.mfDevuelveID();//en caso de homologar con otro id desde los relojes u otros        
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        if (ddlVistaMarcas.SelectedValue == "1")
        {
            ds = rlj.mfBuscarMarcasReloj();
        }
        else
        {
            ds = rlj.mfBuscarMarcasRelojAgrupadas();
        }

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para exportar.");
            return;
        }
        excel.ColumnasExcluir.Add("IDMARCARELOJ");
        excel.ColumnasExcluir.Add("IDRELOJ");
        excel.ColumnasExcluir.Add("VERIFYMODE");
        excel.ColumnasExcluir.Add("INOUTMODE");
        excel.ColumnasExcluir.Add("WORKCODE");
        excel.ColumnasExcluir.Add("IDESTADO");
        excel.ColumnasExcluir.Add("NRO_MARCA");
        excel.ColumnasExcluir.Add("ENROLLNUMBER");
        excel.ColumnasExcluir.Add("VERIFYMODE");
        excel.ColumnasExcluir.Add("F_H_CREACION");
        excel.ColumnasExcluir.Add("TIPO");
        excel.ColumnasExcluir.Add("IDUSR");
        excel.Exportar(ds, "Listado_Marcaciones_" + ddlMes.SelectedValue + "_" + Session["rut"].ToString());
    }
    protected void btnExportarPDF_Click(object sender, EventArgs e)
    {
        DataSet ds;
        ClassPDF pdf = new ClassPDF();
        usr.ls_rut = Session["rut"].ToString();
        rlj.ls_iduser = usr.mfDevuelveID();
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        if (ddlVistaMarcas.SelectedValue == "1")
        {
            ds = rlj.mfBuscarMarcasReloj();
        }
        else
        {
            ds = rlj.mfBuscarMarcasRelojAgrupadas();
        }

        //setear variables para informe
        pdf.Titulo = "Listado de Marcaciones por Trabajador";
        pdf.Subtitulo = Session["nombre"].ToString() + "\n" + Session["rut"].ToString() + "\nPeriodo: " + ddlMes.SelectedItem + " " + ddlAnio.SelectedValue;
        pdf.OrientacionHorizontal = false;
        pdf.MostrarFecha = true;
        pdf.MostrarNumeracion = true;

        //columnas a excluir
        pdf.ColumnasExcluir.Add("IDMARCARELOJ");
        pdf.ColumnasExcluir.Add("IDRELOJ");
        pdf.ColumnasExcluir.Add("ENROLLNUMBER");
        pdf.ColumnasExcluir.Add("VERIFYMODE");
        pdf.ColumnasExcluir.Add("INOUTMODE");
        pdf.ColumnasExcluir.Add("WORKCODE");
        pdf.ColumnasExcluir.Add("F_H_CREACION");
        pdf.ColumnasExcluir.Add("TIPO");
        pdf.ColumnasExcluir.Add("IDUSR");
        pdf.ColumnasExcluir.Add("IDESTADO");
        pdf.ColumnasExcluir.Add("NRO_MARCA");

        //configurar columnas formato fecha hora
        pdf.ColumnasFechaHora.Add("F_H_MARCA");
        pdf.ColumnasFechaHora.Add("ENTRADA");
        pdf.ColumnasFechaHora.Add("SALIDA");

        //configurar ancho columnas
        pdf.AnchosColumnas.Add("FECHA", 13f);
        pdf.AnchosColumnas.Add("DIA", 12f);

        //generar nombre archivo y descargar
        pdf.ExportarDataTable(Response, ds.Tables[0], "Listado_Marcaciones_" + ddlMes.SelectedValue + "_" + Session["rut"].ToString());
    }
    #endregion
}