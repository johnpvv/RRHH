using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Security;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Web.Services;

public partial class contenido_Reportes_RepDespachoPacMicro : System.Web.UI.Page
{
    Reportes rep = new Reportes();

    Mensaje mens = new Mensaje();


    public int carga = 0;
    private string id;
    private String stcadena = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        String lsPer = "";
        modFunciones modfunc = new modFunciones();
        string gUsr;
        string asCodSistema;

        if (Request.Params["__EVENTTARGET"] == "KeyEnterPostBack")
        {
            mfgetPac();
            mfBuscar();
        }

        if (!IsPostBack)
        {

            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();

                lsPer = modfunc.fnValidaUsrApp("RPT_DES_PAC", gUsr, asCodSistema);

                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }

                llenarDDLServicio();
                llenarDDLBodega();
                em_f_desde.Text = "01/01/" + (DateTime.Now.Year - 1).ToString();
                em_f_hasta.Text = DateTime.Now.ToString();

            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }


        }

    }

    private void llenarDDLBodega()
    {
        DataSet dat = new DataSet();
        ClassReceta rec = new ClassReceta();

        dat = rec.mfdllUserBodegas(Session["user"].ToString(), Session["codHosp"].ToString());


        this.ddlBod.DataTextField = "DESCRIPCION";
        this.ddlBod.DataValueField = "CODIGO";
        this.ddlBod.DataSource = dat;
        this.ddlBod.DataBind();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
        item.Text = "Seleccione Bodega";
        item.Value = "0";
        this.ddlBod.Items.Insert(0, item);
    }

    private void llenarDDLServicio()
    {
        DataSet dat = new DataSet();
        ClassUnidOperativa reg = new ClassUnidOperativa();

        dat = reg.mfListaUnidad("1");


        this.ddlServicio.DataTextField = "DESCRIPCION";
        this.ddlServicio.DataValueField = "CODUNIOP";
        this.ddlServicio.DataSource = dat;
        this.ddlServicio.DataBind();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
        item.Text = "Seleccione Servicio";
        item.Value = "0";
        this.ddlServicio.Items.Insert(0, item);
    }

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {

        try
        {
            // Introducing delay for demonstration.
            mfBuscar();
        }
        catch (Exception)
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }


    }

    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            //e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('dgData','Select${0}')", e.Row.RowIndex));
        }
    }


    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
    }


    #region Lista

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

    private bool ValidarReglaRangoConFiltroExtra(string accion)
    {
        bool tieneDesde = !string.IsNullOrWhiteSpace(em_f_desde.Text);
        bool tieneHasta = !string.IsNullOrWhiteSpace(em_f_hasta.Text);

        if (!tieneDesde || !tieneHasta)
            return true;

        DateTime fechaDesde;
        DateTime fechaHasta;
        if (!TryParseFechaFiltro(em_f_desde.Text, out fechaDesde) || !TryParseFechaFiltro(em_f_hasta.Text, out fechaHasta))
            return true;

        if (fechaHasta < fechaDesde)
        {
            mens.mensaje(Page, "La fecha hasta no puede ser menor que la fecha desde.");
            this.lbmensaje.Text = "La fecha hasta no puede ser menor que la fecha desde.";
            return false;
        }

        int maxMeses = ObtenerMaxMesesSinFiltroExtra(accion);
        if (maxMeses < 0)
            maxMeses = 3;

        bool superaRango = fechaHasta > fechaDesde.AddMonths(maxMeses);
        if (!superaRango)
            return true;

        if (TieneFiltroExtraBusqueda())
            return true;

        string mensaje = "Para rangos mayores a " + maxMeses + " meses debe agregar al menos un filtro adicional (rut médico, rut paciente, código, nombre médico, descripción, servicio o bodega).";
        mens.mensaje(Page, mensaje);
        this.lbmensaje.Text = mensaje;
        return false;
    }

    private int ObtenerMaxMesesSinFiltroExtra(string accion)
    {
        int meses;
        string keyAccion = accion == "exportar"
            ? "RepDespachoPac.MaxMesesSinFiltroExtra.Exportar"
            : "RepDespachoPac.MaxMesesSinFiltroExtra.Buscar";

        string valor = ConfigurationManager.AppSettings[keyAccion];
        if (string.IsNullOrWhiteSpace(valor))
            valor = ConfigurationManager.AppSettings["RepDespachoPac.MaxMesesSinFiltroExtra"];

        if (int.TryParse(valor, out meses))
            return meses;

        return 3;
    }

    private bool TieneFiltroExtraBusqueda()
    {
        return !string.IsNullOrWhiteSpace(TRutPac.Text)
            || !string.IsNullOrWhiteSpace(TRutMed.Text)
            || !string.IsNullOrWhiteSpace(TNombMed.Text)
            || !string.IsNullOrWhiteSpace(TCodigo.Text)
            || !string.IsNullOrWhiteSpace(TDesc.Text)
            || this.ddlServicio.SelectedValue != "0"
            || this.ddlBod.SelectedValue != "0";
    }

    private bool TryParseFechaFiltro(string fechaTexto, out DateTime fecha)
    {
        string[] formatos = new string[] { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy" };
        return DateTime.TryParseExact(fechaTexto.Trim(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha)
            || DateTime.TryParse(fechaTexto.Trim(), out fecha);
    }

    private void mfBuscar()
    {
        bool tieneDesde = !string.IsNullOrWhiteSpace(em_f_desde.Text);
        bool tieneHasta = !string.IsNullOrWhiteSpace(em_f_hasta.Text);

        // La API exige que las fechas se envíen juntas o no se envíen.
        if (tieneDesde != tieneHasta)
        {
            mens.mensaje(Page, "Debe ingresar fecha desde y hasta juntas.");
            this.lbmensaje.Text = "Debe ingresar fecha desde y hasta juntas.";
            return;
        }

        // Evita consultas abiertas sin ningun criterio.
        bool sinFiltros = !tieneDesde
            && string.IsNullOrWhiteSpace(TRutPac.Text)
            && string.IsNullOrWhiteSpace(TRutMed.Text)
            && string.IsNullOrWhiteSpace(TNombMed.Text)
            && string.IsNullOrWhiteSpace(TCodigo.Text)
            && string.IsNullOrWhiteSpace(TDesc.Text)
            && this.ddlServicio.SelectedValue == "0"
            && this.ddlBod.SelectedValue == "0";

        if (sinFiltros)
        {
            mens.mensaje(Page, "Debe ingresar al menos un filtro para buscar.");
            this.lbmensaje.Text = "Debe ingresar al menos un filtro para buscar.";
            return;
        }

        if (!ValidarReglaRangoConFiltroExtra("buscar"))
            return;

        DataSet ds;

        this.lbmensaje.Text = "";

        rep.ls_f_d = this.em_f_desde.Text;
        rep.ls_f_h = this.em_f_hasta.Text;

        rep.ls_Codigo = this.TCodigo.Text;
        rep.ls_Art = string.Empty;   // TDesc solo para buscador de maestros

        rep.ls_Rut_M = this.TRutMed.Text;
        rep.ls_Nomb_M = string.Empty;   // TNombMed solo para buscador de maestros

        rep.ls_rut = this.TRutPac.Text;

        rep.ls_IdServ = this.ddlServicio.SelectedValue;
        rep.lb_bod = this.ddlBod.SelectedValue;

        // Búsqueda sin v_full para obtener todos los registros
        ds = rep.mfrptDespPaciente(false);

        if (!string.IsNullOrWhiteSpace(rep.ls_error_api))
        {
            mens.mensaje(Page, rep.ls_error_api);
            this.lbmensaje.Text = rep.ls_error_api;
            this.dgData.DataSource = null;
            this.dgData.DataBind();
            this.dgData0.DataSource = null;
            this.dgData0.DataBind();
            return;
        }


        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                DataView dv = ds.Tables[0].DefaultView;
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
                this.dgData.Caption = "Despachos / Pacientes";

                // Mostrar total de registros
                string totalMsg = rep.li_total_despacho_paciente > 0
                    ? "Total de registros: " + rep.li_total_despacho_paciente + " | Mostrando: " + ds.Tables[0].Rows.Count
                    : "Total de registros: " + ds.Tables[0].Rows.Count;
                this.lbmensaje.Text = totalMsg;

                // Evita cargar una segunda grilla invisible con todos los registros.
                this.dgData0.DataSource = null;
                this.dgData0.DataBind();


            }
            else
            {

                mens.mensaje(Page, "No se encontraron coincidencias.. ");
                this.lbmensaje.Text = "No se encontraron coincidencias.. ";
                this.dgData.DataSource = null;
                this.dgData.DataBind();

                this.dgData0.DataSource = null;
                this.dgData0.DataBind();
            }
        }
        else
        {

            mens.mensaje(Page, "No se encontraron coincidencias.. ");
            this.lbmensaje.Text = "No se encontraron coincidencias.. ";
            this.dgData.DataSource = null;
            this.dgData.DataBind();
            this.dgData0.DataSource = null;
            this.dgData0.DataBind();
        }

    }

    protected void btn_Excel_Click(object sender, EventArgs e)
    {
        bool tieneDesde = !string.IsNullOrWhiteSpace(em_f_desde.Text);
        bool tieneHasta = !string.IsNullOrWhiteSpace(em_f_hasta.Text);

        if (tieneDesde != tieneHasta)
        {
            mens.mensaje(Page, "Debe ingresar fecha desde y hasta juntas.");
            this.lbmensaje.Text = "Debe ingresar fecha desde y hasta juntas.";
            return;
        }

        bool sinFiltros = !tieneDesde
            && string.IsNullOrWhiteSpace(TRutPac.Text)
            && string.IsNullOrWhiteSpace(TRutMed.Text)
            && string.IsNullOrWhiteSpace(TNombMed.Text)
            && string.IsNullOrWhiteSpace(TCodigo.Text)
            && string.IsNullOrWhiteSpace(TDesc.Text)
            && this.ddlServicio.SelectedValue == "0"
            && this.ddlBod.SelectedValue == "0";

        if (sinFiltros)
        {
            mens.mensaje(Page, "Debe ingresar al menos un filtro para exportar.");
            this.lbmensaje.Text = "Debe ingresar al menos un filtro para exportar.";
            return;
        }

        if (!ValidarReglaRangoConFiltroExtra("exportar"))
            return;

        // Reconsulta los datos para exportar sin depender del GridView/HTML render.
        rep.ls_f_d = this.em_f_desde.Text;
        rep.ls_f_h = this.em_f_hasta.Text;
        rep.ls_Codigo = this.TCodigo.Text;
        rep.ls_Art = string.Empty;   // TDesc solo para buscador de maestros
        rep.ls_Rut_M = this.TRutMed.Text;
        rep.ls_Nomb_M = string.Empty;   // TNombMed solo para buscador de maestros
        rep.ls_rut = this.TRutPac.Text;
        rep.ls_IdServ = this.ddlServicio.SelectedValue;
        rep.lb_bod = this.ddlBod.SelectedValue;

        // Exportación con v_full=true para obtener datos con muestra
        DataSet ds = rep.mfrptDespPaciente(true);
        if (!string.IsNullOrWhiteSpace(rep.ls_error_api))
        {
            mens.mensaje(Page, rep.ls_error_api);
            this.lbmensaje.Text = rep.ls_error_api;
            return;
        }

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            mens.mensaje(Page, "No se encontraron datos para exportar.");
            this.lbmensaje.Text = "No se encontraron datos para exportar.";
            return;
        }

        ExportarXlsxDespachosPacientes(ds.Tables[0]);
    }

    private void ExportarXlsxDespachosPacientes(DataTable dt)
    {
        string fileName = "Despachos_Pacientes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

        using (MemoryStream msZip = new MemoryStream())
        {
            using (ZipArchive zip = new ZipArchive(msZip, ZipArchiveMode.Create, true))
            {
                EscribirEntrada(zip, "[Content_Types].xml", ContenidoTypes());
                EscribirEntrada(zip, "_rels/.rels", RelsRaiz());
                EscribirEntrada(zip, "xl/workbook.xml", WorkbookXml());
                EscribirEntrada(zip, "xl/_rels/workbook.xml.rels", WorkbookRels());
                EscribirEntrada(zip, "xl/styles.xml", StylesXml());
                EscribirEntrada(zip, "xl/worksheets/sheet1.xml", SheetXml(dt));
            }

            msZip.Position = 0;

            Response.Clear();
            Response.BufferOutput = false;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.AddHeader("Content-Length", msZip.Length.ToString());

            // Cookie que el cliente detecta para saber que la descarga está lista
            Response.Cookies.Add(new HttpCookie("xlsxReady", "1") { Path = "/" });
            msZip.CopyTo(Response.OutputStream);
            Response.Flush();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    private void EscribirEntrada(ZipArchive zip, string path, string content)
    {
        ZipArchiveEntry entry = zip.CreateEntry(path, CompressionLevel.Fastest);
        using (StreamWriter sw = new StreamWriter(entry.Open(), Encoding.UTF8))
        {
            sw.Write(content);
        }
    }

    private string ContenidoTypes()
    {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<Types xmlns=\"http://schemas.openxmlformats.org/package/2006/content-types\">" +
               "<Default Extension=\"rels\" ContentType=\"application/vnd.openxmlformats-package.relationships+xml\"/>" +
               "<Default Extension=\"xml\" ContentType=\"application/xml\"/>" +
               "<Override PartName=\"/xl/workbook.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml\"/>" +
               "<Override PartName=\"/xl/worksheets/sheet1.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml\"/>" +
               "<Override PartName=\"/xl/styles.xml\" ContentType=\"application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml\"/>" +
               "</Types>";
    }

    private string RelsRaiz()
    {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
               "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument\" Target=\"xl/workbook.xml\"/>" +
               "</Relationships>";
    }

    private string WorkbookXml()
    {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">" +
               "<sheets><sheet name=\"Despachos Pacientes\" sheetId=\"1\" r:id=\"rId1\"/></sheets>" +
               "</workbook>";
    }

    private string WorkbookRels()
    {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
               "<Relationships xmlns=\"http://schemas.openxmlformats.org/package/2006/relationships\">" +
               "<Relationship Id=\"rId1\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet\" Target=\"worksheets/sheet1.xml\"/>" +
               "<Relationship Id=\"rId2\" Type=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles\" Target=\"styles.xml\"/>" +
               "</Relationships>";
    }

    private string StylesXml()
    {
        return "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>" +
               "<styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">" +
               "<fonts count=\"4\">" +
               "<font><sz val=\"10\"/><name val=\"Calibri\"/></font>" +
               "<font><b/><sz val=\"10\"/><color rgb=\"FFFFFFFF\"/><name val=\"Calibri\"/></font>" +
               "<font><sz val=\"10\"/><color rgb=\"FF000000\"/><name val=\"Calibri\"/></font>" +
               "<font><b/><sz val=\"11\"/><color theme=\"1\"/><name val=\"Calibri\"/></font>" +
               "</fonts>" +
               "<fills count=\"3\">" +
               "<fill><patternFill patternType=\"none\"/></fill>" +
               "<fill><patternFill patternType=\"gray125\"/></fill>" +
               "<fill><patternFill patternType=\"solid\"><fgColor rgb=\"FF1F4E78\"/><bgColor rgb=\"FF1F4E78\"/></patternFill></fill>" +
               "</fills>" +
               "<borders count=\"2\">" +
               "<border><left/><right/><top/><bottom/><diagonal/></border>" +
               "<border><left style=\"thin\"><color auto=\"1\"/></left><right style=\"thin\"><color auto=\"1\"/></right><top style=\"thin\"><color auto=\"1\"/></top><bottom style=\"thin\"><color auto=\"1\"/></bottom><diagonal/></border>" +
               "</borders>" +
               "<cellStyleXfs count=\"1\"><xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\"/></cellStyleXfs>" +
               "<cellXfs count=\"5\">" +
               "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\"/>" +
               "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyAlignment=\"1\"><alignment horizontal=\"left\"/></xf>" +
               "<xf numFmtId=\"0\" fontId=\"1\" fillId=\"2\" borderId=\"1\" xfId=\"0\" applyFont=\"1\" applyFill=\"1\" applyBorder=\"1\" applyAlignment=\"1\"><alignment horizontal=\"center\" vertical=\"center\" wrapText=\"1\"/></xf>" +
               "<xf numFmtId=\"0\" fontId=\"0\" fillId=\"0\" borderId=\"1\" xfId=\"0\" applyBorder=\"1\" applyAlignment=\"1\"><alignment vertical=\"top\" wrapText=\"1\"/></xf>" +
               "<xf numFmtId=\"0\" fontId=\"3\" fillId=\"0\" borderId=\"0\" xfId=\"0\" applyFont=\"1\" applyAlignment=\"1\"><alignment horizontal=\"left\"/></xf>" +
               "</cellXfs>" +
               "<cellStyles count=\"1\"><cellStyle name=\"Normal\" xfId=\"0\" builtinId=\"0\"/></cellStyles>" +
               "</styleSheet>";
    }

    private string SheetXml(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.Append("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">");
        sb.Append("<cols>");
        sb.Append("<col min=\"1\" max=\"1\" width=\"20\" customWidth=\"1\"/>");
        sb.Append("<col min=\"2\" max=\"2\" width=\"28\" customWidth=\"1\"/>");
        sb.Append("<col min=\"3\" max=\"3\" width=\"14\" customWidth=\"1\"/>");
        sb.Append("<col min=\"4\" max=\"4\" width=\"42\" customWidth=\"1\"/>");
        sb.Append("<col min=\"5\" max=\"5\" width=\"26\" customWidth=\"1\"/>");
        sb.Append("<col min=\"6\" max=\"6\" width=\"20\" customWidth=\"1\"/>");
        sb.Append("<col min=\"7\" max=\"7\" width=\"16\" customWidth=\"1\"/>");
        sb.Append("<col min=\"8\" max=\"8\" width=\"16\" customWidth=\"1\"/>");
        sb.Append("<col min=\"9\" max=\"9\" width=\"20\" customWidth=\"1\"/>");
        sb.Append("<col min=\"10\" max=\"10\" width=\"16\" customWidth=\"1\"/>");
        sb.Append("</cols>");
        sb.Append("<sheetData>");

        int r = 1;
        List<int> filasCombinadas = new List<int>();

        AppendRow(sb, r, 4, "Ministerio de Salud");
        filasCombinadas.Add(r++);
        AppendRow(sb, r, 4, "Servicio de Salud Metropolitana Central");
        filasCombinadas.Add(r++);
        AppendRow(sb, r, 4, "Hospital Clinico San Borja Arriaran");
        filasCombinadas.Add(r++);

        r++;

        AppendRow(sb, r, 4, "Despachos / Pacientes");
        filasCombinadas.Add(r++);

        r++;

        int filaEncabezado = r;
        AppendRow(sb, r++, 2,
            "Policlinico", "Bodega", "N° Receta", "Medicamento", "Frecuencia",
            "Fecha Entrega", "Dias Tratamiento", "Cant. Despachada", "Fecha Prox.Desp.", "Cant. Pendiente");

        foreach (DataRow dr in dt.Rows)
        {
            AppendRow(sb, r++, 3,
                dr["UNIDAD"].ToString(),
                dr["BODEGA"].ToString(),
                dr["FOLIO"].ToString(),
                dr["NOMB_ART"].ToString(),
                dr["FRECUENCIA"].ToString(),
                dr["FDESPACHO"].ToString(),
                dr["DIAS"].ToString(),
                dr["CANTIDAD"].ToString(),
                dr["FNEXTDESPACHO"].ToString(),
                dr["CANT_PEND"].ToString());
        }

        sb.Append("</sheetData>");

        int ultimaFila = r - 1;
        if (ultimaFila >= filaEncabezado)
        {
            sb.Append("<autoFilter ref=\"A")
              .Append(filaEncabezado)
              .Append(":J")
              .Append(ultimaFila)
              .Append("\"/>");
        }

        sb.Append("<mergeCells count=\"").Append(filasCombinadas.Count).Append("\">");
        foreach (int fila in filasCombinadas)
        {
            sb.Append("<mergeCell ref=\"A")
              .Append(fila)
              .Append(":J")
              .Append(fila)
              .Append("\"/>");
        }
        sb.Append("</mergeCells>");

        sb.Append("</worksheet>");
        return sb.ToString();
    }

    private void AppendRow(StringBuilder sb, int rowIndex, int styleId, params string[] values)
    {
        sb.Append("<row r=\"").Append(rowIndex).Append("\">");
        for (int i = 0; i < values.Length; i++)
        {
            string cellRef = Col(i + 1) + rowIndex;
            sb.Append("<c r=\"").Append(cellRef).Append("\" t=\"inlineStr\"");
            sb.Append(" s=\"").Append(styleId).Append("\"");
            sb.Append("><is><t xml:space=\"preserve\">")
              .Append(EscapeXml(values[i] ?? ""))
              .Append("</t></is></c>");
        }
        sb.Append("</row>");
    }

    private string Col(int index)
    {
        string col = "";
        while (index > 0)
        {
            int rem = (index - 1) % 26;
            col = (char)(65 + rem) + col;
            index = (index - 1) / 26;
        }
        return col;
    }

    private string EscapeXml(string val)
    {
        return SecurityElement.Escape(val) ?? "";
    }

    private string fnExpXls(string asDataXls)
    {
        // Export legacy: mantenido por compatibilidad, actualmente no usado en esta pantalla.
        string lsRet = "";
        String lsExt = "xls";
        String lsNom = "Hoja1";

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + lsNom + "." + lsExt + "");
        Response.ContentEncoding = System.Text.Encoding.Default;
        // El encoding correcto es: Windows-1252
        Response.Charset = "UTF-8";
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.ContentType = "application/vnd." + lsExt;
        Response.Write(asDataXls);
        Response.End();

        return lsRet;
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    #endregion

    #region PDF

    private static void addCell(PdfPTable table, string text, int rowspan)
    {
        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 6, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

        PdfPCell cell = new PdfPCell(new Phrase(text, times));
        cell.Rowspan = rowspan;
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        table.AddCell(cell);
    }

    public PdfPCell getCellNegrita(string text, float size, int alignment, short bordo)
    {
        Font font = FontFactory.GetFont("HELVETICA", size, Font.BOLD);
        // definimos tipo, tama?o y color de fuente
        PdfPCell cell = new PdfPCell(new Phrase(text, font));
        // definimos alineamiento y bordes de celda
        cell.Padding = 3;
        cell.HorizontalAlignment = alignment;
        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
        if ((bordo == 0))
        {
            cell.Border = bordo;
        }
        else
        {
            // cell.Border = (iTextSharp.text.Rectangle.BOTTOM_BORDER _
            //                 Or (iTextSharp.text.Rectangle.TOP_BORDER _
            //                 Or (iTextSharp.text.Rectangle.LEFT_BORDER Or iTextSharp.text.Rectangle.RIGHT_BORDER)))
        }

        return cell;
    }

    public PdfPTable prop_cell(PdfPTable nombre)
    {
        nombre.DefaultCell.Border = Rectangle.RECTANGLE;
        nombre.DefaultCell.BorderWidth = 1;
        nombre.HorizontalAlignment = Element.ALIGN_LEFT;
        nombre.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
        nombre.TotalWidth = 540f;
        nombre.LockedWidth = true;

        return nombre;
    }

    public PdfPCell getCell(string text, float size, int alignment, short bordo)
    {
        Font font = FontFactory.GetFont("HELVETICA", size, Font.NORMAL);
        // definimos tipo, tama?o y color de fuente
        PdfPCell cell = new PdfPCell(new Phrase(text, font));
        // definimos alineamiento y bordes de celda
        cell.Padding = 3;
        cell.HorizontalAlignment = alignment;
        cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
        if ((bordo == 0))
        {
            cell.Border = bordo;
        }
        else
        {
            // cell.Border = (iTextSharp.text.Rectangle.BOTTOM_BORDER _
            //                 Or (iTextSharp.text.Rectangle.TOP_BORDER _
            //                 Or (iTextSharp.text.Rectangle.LEFT_BORDER Or iTextSharp.text.Rectangle.RIGHT_BORDER)))
        }

        return cell;
    }

    protected void pdfBtn(object sender, EventArgs e)
    {

        dgData0.AllowPaging = false;
        dgData0.AllowSorting = false;

        //string myFont = @"C:\Windows\Fonts\arial.ttf";

        //PdfPTable pdftable = new PdfPTable(dgData.HeaderRow.Cells.Count);
        PdfPTable pdftable = new PdfPTable(6);

        BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        pdftable.HorizontalAlignment = 0;
        pdftable.TotalWidth = 500f;
        pdftable.LockedWidth = true;
        //float[] widths = new float[] { 35f, 100f, 20f, 30f, 30f, 40f, 45f };
        float[] widths = new float[] { 35f, 100f, 20f, 20f, 20f, 20f };
        pdftable.SetWidths(widths);


        // foreach (GridViewRow gv in dgData.Rows)
        // {

        Document pdfD = new Document(PageSize.LETTER, 30f, 30f, 20f, 20f);
        PdfWriter.GetInstance(pdfD, Response.OutputStream);
        bool b;
        pdfD.Open();



        for (int i = 0; i < dgData0.PageCount; i++)
        {
            //Set the Page Index.
            dgData0.PageIndex = i;

            //Hide Page as not needed in PDF.
            dgData0.PagerSettings.Visible = false;

            foreach (TableCell headercell in dgData0.HeaderRow.Cells)
            {
                string decode = Server.HtmlDecode(headercell.Text);
                Font font = new Font(bf, 12, iTextSharp.text.Font.NORMAL);
                font.Color = new BaseColor(255, 255, 255);
                PdfPCell pdfPCellCa = new PdfPCell(new Phrase(decode, font));
                pdfPCellCa.BackgroundColor = new BaseColor(85, 85, 85);
                pdftable.HorizontalAlignment = Element.ALIGN_CENTER;
                pdftable.AddCell(pdfPCellCa);
            }
            //  }

            foreach (GridViewRow gv in dgData0.Rows)
            {
                foreach (TableCell tablecell in gv.Cells)
                {
                    string decode = Server.HtmlDecode(tablecell.Text);
                    Font font = new Font(bf, 10, iTextSharp.text.Font.NORMAL);
                    // font.Color = new BaseColor(dgData.RowStyle.ForeColor);

                    PdfPCell pdfPCell = new PdfPCell(new Phrase(decode, font));
                    // pdfPCell.BackgroundColor = new BaseColor(dgData.RowStyle.ForeColor);
                    pdftable.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdftable.AddCell(pdfPCell);
                }
            }



            string imagepath = HttpContext.Current.Server.MapPath("~/imagenes/logo-HCSBA.jpg");
            string imagepath2 = HttpContext.Current.Server.MapPath("~/imagenes/linea.png");






            //doc.Add(new Paragraph("GIF"));
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagepath);

            iTextSharp.text.Image image2 = iTextSharp.text.Image.GetInstance(imagepath2);
            image.ScaleAbsolute(75, 80);
            image2.ScaleAbsolute(520, 3);



            string st = "Ministerio de Salud\n" +
                        "Servicio de Salud Metropolitana Central\n" +
                        "Hospital Clínico San Borja Arriaran";

            PdfPTable tableHosp = new PdfPTable(2);
            tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
            tableHosp.DefaultCell.BorderWidth = 0;
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;
            tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableHosp.TotalWidth = 540f;
            tableHosp.LockedWidth = true;

            float[] TamColumHora = new float[] { 0.08f, 0.50f };
            tableHosp.SetWidths(TamColumHora);
            tableHosp.HorizontalAlignment = Element.ALIGN_RIGHT;

            tableHosp.AddCell(image);

            tableHosp.AddCell(st);



            pdfD.Add(tableHosp);
            pdfD.Add(image2);



        }


        Paragraph para = new Paragraph("Medicos / Bodegas", new Font(Font.FontFamily.HELVETICA, 16));
        para.Alignment = Element.ALIGN_CENTER;
        Paragraph space = new Paragraph("    ", new Font(Font.FontFamily.HELVETICA, 10));
        Paragraph bodega = new Paragraph("Medicos / Bodegas", new Font(Font.FontFamily.HELVETICA, 10));
        bodega.Alignment = Element.ALIGN_CENTER;
        //  pdfD.Add(space);


        // pdfD.Add(space);

        pdfD.Add(para);

        // pdfD.Add(space);
        pdfD.Add(bodega);
        pdfD.Add(space);
        pdfD.Add(pdftable);
        pdfD.Add(space);

        Paragraph foot = new Paragraph("  DTI Hospital Clinico San Borja Ariaran.", new Font(Font.FontFamily.HELVETICA, 10));

        foot.Alignment = Element.ALIGN_LEFT;
        pdfD.Add(foot);
        pdftable.Rows.Clear();
        pdfD.NewPage();




        pdfD.Close();

        Response.ContentType = "application/pdf";
        Response.AppendHeader("content-disposition", "attachment;filename=ArticuloBodegas.pdf");
        Response.Write(pdfD);
        Response.Flush();
        Response.End();

        dgData0.AllowPaging = true;
        dgData0.AllowSorting = true;
    }


    #endregion

    protected void TRutPac_TextChanged(object sender, EventArgs e)
    {
        mfgetPac();
    }

    private void mfgetPac()
    {
        ClassPaciente pac = new ClassPaciente();

        TNombPac.Text = pac.mfNombrePaciente(TRutPac.Text) + " " + pac.mfPaternoPaciente(TRutPac.Text) + " " + pac.mfMaternoPaciente(TRutPac.Text);
        // No dispara búsqueda automática para evitar postbacks pesados durante digitación.
    }

    // ── Proxy de búsqueda de maestros ───────────────────────────────���─────
    [WebMethod]
    public static string BuscarMaestro(string endpoint, string termino)
    {
        string baseUrl = System.Web.Configuration.WebConfigurationManager
                             .AppSettings["ApiBusquedaUrl"]
                         ?? "http://localhost:8080/api/v1/busqueda/";
        string url = baseUrl + endpoint + "?termino=" + Uri.EscapeDataString(termino);
        try
        {
            using (var wc = new WebClient())
            {
                wc.Headers.Add("Accept", "application/json");
                wc.Encoding = Encoding.UTF8;
                return wc.DownloadString(url);
            }
        }
        catch (System.Net.WebException ex)
        {
            int status = ex.Response != null ? (int)((System.Net.HttpWebResponse)ex.Response).StatusCode : 0;
            return "{\"__error\":true,\"status\":" + status + "}";
        }
    }
}