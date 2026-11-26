using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Reportes_RepMarcaTrabajadores : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    ClassTrabajadores usr = new ClassTrabajadores();
    ClassTurnos tur = new ClassTurnos();
    ClassHorarios hor = new ClassHorarios();
    ClassReloj rlj = new ClassReloj();
    ClassFeriado fer = new ClassFeriado();
    ClassExcelExportar excel = new ClassExcelExportar();
    ClassPDF pdf = new ClassPDF();
    private HashSet<DateTime> feriados = new HashSet<DateTime>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarMeses();
            CargarAnios();
            CargarCentrosAdmin();
        }
    }

    #region General
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
            DateTime fechaVal;
            if (DateTime.TryParse(dr["FECHA"].ToString(), out fechaVal))
                feriados.Add(fechaVal.Date);
        }
    }
    private void CargarCentrosAdmin()
    {
        usr.ls_iduser = Session["user"].ToString();
        DataSet ds = usr.mfBuscarCentrosAdmin();

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            return;

        ddlCentro.DataSource = ds;
        ddlCentro.DataTextField = "DESCRIPCION";
        ddlCentro.DataValueField = "CODUNIOP";
        ddlCentro.DataBind();
        ddlCentro.Items.Insert(0, new ListItem("-- Seleccione centro / unidad --", "0"));
    }

    protected void ddlCentro_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
    }

    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
    }
    #endregion

    #region cargar Marcas total
    private void CargarTrabajadoresMarcas()
    {
        rlj.ls_mes = this.ddlMes.SelectedValue;
        rlj.ls_anio = this.ddlAnio.SelectedValue;
        rlj.ls_nombre = this.txtBuscarTrab.Text.Trim();
        rlj.ls_unidad = this.ddlCentro.SelectedValue;
        // Validar que se haya seleccionado un centro
        if (string.IsNullOrEmpty(ddlCentro.SelectedValue) || ddlCentro.SelectedValue == "0")
        {
            mens.mensaje(Page, "Debe seleccionar un centro antes de buscar las marcaciones.");
            return;
        }
        DataSet ds = rlj.mfBuscarTrabajadoresMarcas();
        if (ds != null && ds.Tables.Count > 0)
        {
            DataView dv = ds.Tables[0].DefaultView;
            dv.Sort = OrdenTrabajadores;
            dgTrabajadores.DataSource = dv;
            dgTrabajadores.DataBind();
            lblTotalTrab.Text = ds.Tables[0].Rows.Count.ToString();
            //divDetalleMarcas.Visible = false;
        }
        else
        {
            dgTrabajadores.DataSource = null;
            dgTrabajadores.DataBind();
            //divDetalleMarcas.Visible = false;
        }
    }
    private string OrdenTrabajadores
    {
        get
        {
            return ViewState["OrdenTrabajadores"] == null
                ? "NOMBRE ASC" :
                ViewState["OrdenTrabajadores"].ToString();
        }
        set
        {
            ViewState["OrdenTrabajadores"] = value;
        }
    }
    protected void dgTrabajadores_Sorting(object sender, GridViewSortEventArgs e)
    {
        string ordenActual = OrdenTrabajadores;
        string campoActual = ordenActual.Split(' ')[0];
        string direccionActual = ordenActual.Split(' ')[1];
        if (campoActual == e.SortExpression)
        {
            direccionActual = direccionActual == "ASC" ? "DESC" : "ASC";
        }
        else
        {
            direccionActual = "ASC";
        }
        OrdenTrabajadores = e.SortExpression + " " + direccionActual;
        CargarTrabajadoresMarcas();
    }
    protected void btnBuscarMarcas_Click(object sender, EventArgs e)
    {
        CargarTrabajadoresMarcas();
    }
    protected void dgTrabajadores_SelectedIndexChanged(object sender, EventArgs e)
    {
        //CargarMarcasTrabajador();
        //CargarCentrosMarca();
    }
    protected void dgTrabajadores_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgTrabajadores.PageIndex = e.NewPageIndex;
        CargarTrabajadoresMarcas();
    }
    #endregion
    #region Exportar Excel
    protected void btnExportarTrab_Click(object sender, EventArgs e)
    {
        rlj.ls_mes = this.ddlMes.SelectedValue;
        rlj.ls_anio = this.ddlAnio.SelectedValue;
        rlj.ls_nombre = this.txtBuscarTrab.Text.Trim();
        rlj.ls_unidad = this.ddlCentro.SelectedValue;
        if (string.IsNullOrEmpty(ddlCentro.SelectedValue) || ddlCentro.SelectedValue == "0")
        {
            mens.mensaje(Page, "Debe seleccionar un centro antes de exportar los Datos.");
            return;
        }
        DataSet ds = rlj.mfBuscarTrabajadoresMarcas();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para exportar.");
            return;
        }

        excel.Exportar(ds, "Trabajadores_marcacion_Gral");
    }


    protected void btnExportarMarcas_Click(object sender, EventArgs e)
    {
        string idSeleccion = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        rlj.ls_iduser = "";
        rlj.ls_iduserreloj = "";
        if (idSeleccion.StartsWith("U:"))
        {
            rlj.ls_iduser = idSeleccion.Substring(2);
        }
        else if (idSeleccion.StartsWith("R:"))
        {
            rlj.ls_iduserreloj = idSeleccion.Substring(2);
        }
        DataSet ds = rlj.mfBuscarMarcasTrabajador();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para exportar.");
            return;
        }
        //excluir columnas
        excel.ColumnasExcluir.Add("IDMARCACION");
        excel.ColumnasExcluir.Add("IDRELOJ");
        excel.ColumnasExcluir.Add("TIPO_MARCA");
        excel.ColumnasExcluir.Add("TIPO_CARGA");
        excel.ColumnasExcluir.Add("HORA");

        //solo formato hora
        excel.ColumnasSoloHora.Add("F_H_MARCA");
        excel.ColumnasSoloFecha.Add("FECHA");

        excel.Exportar(ds, "Trabajador_marcacion_Detalle");
    }

    protected void btnExportarCalcExcel_Click(object sender, EventArgs e)
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (!iduser.StartsWith("U:"))
        {
            mens.mensaje(Page, "El Trabajador no está registrado en sistema WEB RR.HH, no se puede continuar...");
            return;
        }
        rlj.ls_rut = usr.mfDevuelveRut(iduser.Substring(2));
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;

        DataSet ds = rlj.mfCalcularHorasTrabajador();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para exportar.");
            return;
        }

        //excluir columnas
        excel.ColumnasExcluir.Add("IDUSUARIO");
        excel.ColumnasExcluir.Add("IDTURNOS");
        excel.ColumnasExcluir.Add("EXIGE_JORNADA");

        //solo formato hora
        excel.ColumnasSoloHora.Add("HORA_ENTRADA_TURNO");
        excel.ColumnasSoloHora.Add("HORA_SALIDA_TURNO");
        excel.ColumnasSoloHora.Add("ENTRADA_REAL");
        excel.ColumnasSoloHora.Add("SALIDA_REAL");
        excel.ColumnasSoloFecha.Add("FECHA");

        excel.Exportar(ds.Tables[0], "calculo_trab_" + ddlMes.SelectedValue + "_" + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[0].Text));
    }
    #endregion

    #region PDF
    protected void btnExportarPDF_Click(object sender, EventArgs e)
    {
        string idSeleccion = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;
        rlj.ls_iduser = "";
        rlj.ls_iduserreloj = "";
        if (idSeleccion.StartsWith("U:"))
        {
            rlj.ls_iduser = idSeleccion.Substring(2);
        }
        else if (idSeleccion.StartsWith("R:"))
        {
            rlj.ls_iduserreloj = idSeleccion.Substring(2);
        }
        DataSet ds = rlj.mfBuscarMarcasTrabajador();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para generar reporte.");
            return;
        }
        //setear variables para informe
        pdf.Titulo = "Listado Preliminar de Marcaciones por Trabajador";
        pdf.Subtitulo = "Nombre: " + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[2].Text) + "\nRUT: " + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[0].Text) + "\nPeríodo: " + ddlMes.SelectedItem + " " + ddlAnio.SelectedValue;
        pdf.OrientacionHorizontal = false;
        pdf.MostrarFecha = true;
        pdf.MostrarNumeracion = true;

        //columnas a excluir
        pdf.ColumnasExcluir.Add("IDMARCACION");
        pdf.ColumnasExcluir.Add("IDRELOJ");
        pdf.ColumnasExcluir.Add("F_H_MARCA");
        pdf.ColumnasExcluir.Add("TIPO_MARCA");
        pdf.ColumnasExcluir.Add("F_H_CREACION");
        pdf.ColumnasExcluir.Add("TIPO");
        pdf.ColumnasExcluir.Add("IDUSR");
        pdf.ColumnasExcluir.Add("IDESTADO");
        pdf.ColumnasExcluir.Add("TIPO_CARGA");

        //configurar columnas formato fecha hora
        pdf.ColumnasFechaHora.Add("F_H_MARCA");
        pdf.ColumnasFechaHora.Add("HORA");

        //configurar ancho columnas
        pdf.AnchosColumnas.Add("OBSERVACIONES", 25f);
        pdf.AnchosColumnas.Add("FECHA", 14f);
        pdf.AnchosColumnas.Add("HORA", 13f);
        pdf.AnchosColumnas.Add("CENTRO", 30f);
        pdf.AnchosColumnas.Add("DIA", 13f);

        //generar nombre archivo y descargar
        pdf.ExportarDataTable(Response, ds.Tables[0], "Listado_preliminar_Marcas_" + ddlMes.SelectedValue + "_" + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[0].Text));
    }
    protected void btnExportarPDFHoras_Click(object sender, EventArgs e)
    {
        string iduser = dgTrabajadores.SelectedDataKey.Values["ID_SELECCION"].ToString();
        if (!iduser.StartsWith("U:"))
        {
            mens.mensaje(Page, "El Trabajador no está registrado en sistema WEB RR.HH, no se puede continuar...");
            return;
        }
        rlj.ls_rut = usr.mfDevuelveRut(iduser.Substring(2));
        rlj.ls_mes = ddlMes.SelectedValue;
        rlj.ls_anio = ddlAnio.SelectedValue;

        DataSet ds = rlj.mfCalcularHorasTrabajador();
        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No existen datos para generar reporte.");
            return;
        }
        //setear variables para informe
        pdf.Titulo = "Cálculo de Horas por Trabajador";
        pdf.Subtitulo = "Nombre: " + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[2].Text) + "\nRUT: " + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[0].Text) + "\nPeríodo: " + ddlMes.SelectedItem + " " + ddlAnio.SelectedValue;
        pdf.MostrarFecha = true;
        pdf.MostrarNumeracion = true;
        pdf.OrientacionHorizontal = true;

        //columnas a excluir
        pdf.ColumnasExcluir.Add("IDUSUARIO");
        pdf.ColumnasExcluir.Add("RUT");
        pdf.ColumnasExcluir.Add("EXIGE_JORNADA");
        //pdf.ColumnasExcluir.Add("TIENE_TURNO");
        pdf.ColumnasExcluir.Add("MINUTOS_TRABAJADOS");
        pdf.ColumnasExcluir.Add("MINUTOS_ATRASO");
        pdf.ColumnasExcluir.Add("MINUTOS_EXTRAS_DIURNAS");
        pdf.ColumnasExcluir.Add("MINUTOS_EXTRAS_FESTIVAS");
        pdf.ColumnasExcluir.Add("IDTURNOS");

        //segunda tabla
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_TRABAJADOS");
        pdf.ColumnasExcluir.Add("TOTAL_HORAS_TRABAJADAS");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_ATRASO");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_EXTRAS");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_EXTRAS_DIURNAS");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_EXTRAS_FESTIVAS");

        //configurar columnas formato fecha hora
        pdf.ColumnasSoloHora.Add("HORA_ENTRADA_TURNO");
        pdf.ColumnasSoloHora.Add("HORA_SALIDA_TURNO");
        pdf.ColumnasSoloHora.Add("ENTRADA_REAL");
        pdf.ColumnasSoloHora.Add("SALIDA_REAL");

        //configurar ancho columnas
        pdf.AnchosColumnas.Add("DIA", 15f);
        pdf.AnchosColumnas.Add("FECHA", 18f);

        // Tabla adicional: resumen
        pdf.TablasAdicionales.Add(ds.Tables[1]);
        pdf.TitulosTablasAdicionales.Add("Resumen del Período:");

        pdf.InformacionAdicional.Add("Favor verificar bien los datos antes de generar el cálculo final.");

        //generar nombre archivo y descargar
        pdf.ExportarDataTable(Response, ds.Tables[0], "Listado_preliminar_Calculo_" + ddlMes.SelectedValue + "_" + System.Web.HttpUtility.HtmlDecode(dgTrabajadores.SelectedRow.Cells[0].Text));
    }

    protected void btnExportarPDFLista_Click(object sender, EventArgs e)
    {
        generaPDFLista();
    }
    private void generaPDFLista()
    {
        // Limpiar colecciones para evitar acumular datos de exportaciones anteriores
        pdf.TablasAdicionales.Clear();
        pdf.TitulosTablasAdicionales.Clear();
        pdf.ColumnasExcluir.Clear();
        pdf.ColumnasSoloHora.Clear();
        pdf.AnchosColumnas.Clear();
        pdf.InformacionAdicional.Clear();

        List<string> trabajadoresConError = new List<string>();
        bool hayDatos = false;

        // Recorrer trabajadores del GridView
        foreach (GridViewRow fila in dgTrabajadores.Rows)
        {
            string iduser = dgTrabajadores.DataKeys[fila.RowIndex].Values["ID_SELECCION"].ToString();

            // Solo trabajadores registrados en RR.HH.
            if (!iduser.StartsWith("U:"))
                continue;

            rlj.ls_rut = usr.mfDevuelveRut(iduser.Substring(2));
            rlj.ls_mes = ddlMes.SelectedValue;
            rlj.ls_anio = ddlAnio.SelectedValue;

            try
            {
                DataSet ds = rlj.mfCalcularHorasTrabajador();
                if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count == 0)
                    continue;

                string nombre = System.Web.HttpUtility.HtmlDecode(fila.Cells[2].Text);
                string rut = System.Web.HttpUtility.HtmlDecode(fila.Cells[0].Text);
                pdf.TablasAdicionales.Add(ds.Tables[0]);
                pdf.TitulosTablasAdicionales.Add("Detalle diario: " + nombre + " - RUT: " + rut);

                pdf.TablasAdicionales.Add(ds.Tables[1]);
                pdf.TitulosTablasAdicionales.Add("Resumen del Período: " + nombre + " - RUT: " + rut);

                hayDatos = true;
            }
            catch (Exception ex)
            {
                string nombre = System.Web.HttpUtility.HtmlDecode(fila.Cells[2].Text);
                string rut = System.Web.HttpUtility.HtmlDecode(fila.Cells[0].Text);

                trabajadoresConError.Add(nombre + " - " + rut + ": " + ex.Message);
                continue;
            }
        }

        if (!hayDatos)
        {
            mens.mensaje(Page, "No existen datos para generar reporte.");
            return;
        }
        if (trabajadoresConError.Count > 0)
        {
            pdf.InformacionAdicional.Add("TRABAJADORES NO CALCULADOS:");
            foreach (string error in trabajadoresConError)
            {
                pdf.InformacionAdicional.Add(error);
            }
        }
        //setear variables para informe
        pdf.Titulo = "Cálculo Masivo de Horas por Trabajador";
        pdf.Subtitulo = "Período: " + ddlMes.SelectedItem + " " + ddlAnio.SelectedValue;

        // Configuración del informe
        pdf.MostrarFecha = true;
        pdf.MostrarNumeracion = true;
        pdf.OrientacionHorizontal = true;
        pdf.MostrarTablaPrincipal = false;//para ocultar tabla de detalle principal, y dejar listado por trabajador

        // Columnas a excluir
        pdf.ColumnasExcluir.Add("IDUSUARIO");
        pdf.ColumnasExcluir.Add("RUT");
        pdf.ColumnasExcluir.Add("EXIGE_JORNADA");
        pdf.ColumnasExcluir.Add("MINUTOS_TRABAJADOS");
        pdf.ColumnasExcluir.Add("MINUTOS_ATRASO");
        pdf.ColumnasExcluir.Add("MINUTOS_EXTRAS_DIURNAS");
        pdf.ColumnasExcluir.Add("MINUTOS_EXTRAS_FESTIVAS");
        pdf.ColumnasExcluir.Add("IDTURNOS");

        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_TRABAJADOS");
        pdf.ColumnasExcluir.Add("TOTAL_HORAS_TRABAJADAS");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_ATRASO");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_EXTRAS");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_EXTRAS_DIURNAS");
        pdf.ColumnasExcluir.Add("TOTAL_MINUTOS_EXTRAS_FESTIVAS");

        // Formato fecha y hora
        pdf.ColumnasSoloHora.Add("HORA_ENTRADA_TURNO");
        pdf.ColumnasSoloHora.Add("HORA_SALIDA_TURNO");
        pdf.ColumnasSoloHora.Add("ENTRADA_REAL");
        pdf.ColumnasSoloHora.Add("SALIDA_REAL");

        // Ancho columnas
        pdf.AnchosColumnas.Add("DIA", 15f);
        pdf.AnchosColumnas.Add("FECHA", 18f);

        pdf.InformacionAdNeg.Add("Favor verificar bien los datos antes de generar el cálculo final.");

        // Generar PDF
        pdf.ExportarDataTable(Response, pdf.TablasAdicionales[0], "Listado_Calculo_Horas_" + ddlMes.SelectedValue + "_" + ddlAnio.SelectedValue);

    }
    #endregion
}