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

public partial class contenido_Administracion_GestionEstadistica : System.Web.UI.Page
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
        if (!IsPostBack)
        {

            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();

                lsPer = modfunc.fnValidaUsrApp("ADM_ESTAD", gUsr, asCodSistema);

                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }
                this.hdchk.Value = "0";
                llenarDDLAnio();
                llenarDDLBodega();


            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }


        }

    }

    private void mfCheck(string anio, string bod)
    {
        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("1", anio, bod)) > 0)
        {
            chk1.Checked = true;
            chk1.Enabled = false;
        }
        else
        {
            chk1.Checked = false;
            chk1.Enabled = true;
        }
            

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("2", anio, bod)) > 0)
        {
            chk2.Checked = true;
            chk2.Enabled = false;
        }
        else
        {
            chk2.Checked = false;
            chk2.Enabled = true;
        }
        

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("3", anio, bod)) > 0)
        {
            chk3.Checked = true;
            chk3.Enabled = false;
        }
        else
        {
            chk3.Checked = false;
            chk3.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("4", anio, bod)) > 0)
        {
            chk4.Checked = true;
            chk4.Enabled = false;
        }
        else
        {
            chk4.Checked = false;
            chk4.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("5", anio, bod)) > 0)
        {
            chk5.Checked = true;
            chk5.Enabled = false;
        }
        else
        {
            chk5.Checked = false;
            chk5.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("6", anio, bod)) > 0)
        {
            chk6.Checked = true;
            chk6.Enabled = false;
        }
        else
        {
            chk6.Checked = false;
            chk6.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("7", anio, bod)) > 0)
        {
            chk7.Checked = true;
            chk7.Enabled = false;
        }
        else
        {
            chk7.Checked = false;
            chk7.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("8", anio, bod)) > 0)
        {
            chk8.Checked = true;
            chk8.Enabled = false;
        }
        else
        {
            chk8.Checked = false;
            chk8.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("9", anio, bod)) > 0)
        {
            chk9.Checked = true;
            chk9.Enabled = false;
        }
        else
        {
            chk9.Checked = false;
            chk9.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("10", anio, bod)) > 0)
        {
            chk10.Checked = true;
            chk10.Enabled = false;
        }
        else
        {
            chk10.Checked = false;
            chk10.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("11", anio, bod)) > 0)
        {
            chk11.Checked = true;
            chk11.Enabled = false;
        }
        else
        {
            chk11.Checked = false;
            chk11.Enabled = true;
        }

        if (Convert.ToInt32(rep.mfEstadisticaAnoMes("12", anio, bod)) > 0)
        {
            chk12.Checked = true;
            chk12.Enabled = false;
        }
        else
        {
            chk12.Checked = false;
            chk12.Enabled = true;
        }

    }


    private void mfSetCheck(bool asEstado)
    {

        chk1.Checked = !asEstado;
        chk1.Enabled = asEstado;

        chk2.Checked = !asEstado;
        chk2.Enabled = asEstado;

        chk3.Checked = !asEstado;
        chk3.Enabled = asEstado;

        chk4.Checked = !asEstado;
        chk4.Enabled = asEstado;

        chk5.Checked = !asEstado;
        chk5.Enabled = asEstado;

        chk6.Checked = !asEstado;
        chk6.Enabled = asEstado;

        chk7.Checked = !asEstado;
        chk7.Enabled = asEstado;

        chk8.Checked = !asEstado;
        chk8.Enabled = asEstado;

        chk9.Checked = !asEstado;
        chk9.Enabled = asEstado;

        chk10.Checked = !asEstado;
        chk10.Enabled = asEstado;

        chk11.Checked = !asEstado;
        chk11.Enabled = asEstado;

        chk12.Checked = !asEstado;
        chk12.Enabled = asEstado;


    }
    private void llenarDDLAnio()
    {
        DataSet dat = new DataSet();

        dat = rep.mfdllAnioEstadistica();


        this.ddlAnio.DataTextField = "DESCRIPCION";
        this.ddlAnio.DataValueField = "CODIGO";
        this.ddlAnio.DataSource = dat;
        this.ddlAnio.DataBind();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
        item.Text = "Seleccione Año";
        item.Value = "0";
        this.ddlAnio.Items.Insert(0, item);
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

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {

        try
        {
            // Introducing delay for demonstration.
            mfBuscar();
        }
        catch (Exception ex)
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

    private void mfBuscar()
    {
        // Verifica ingreso de alguna fecha.
        if (this.ddlAnio.SelectedValue == "0" || this.ddlBod.SelectedValue == "0")
        {
            mens.mensaje(Page, "Debe de Ingresar todos los criterios.. ");
            this.lbmensaje.Text = "Debe de Ingresar todos los criterios.. ";
            return;
        }

        

        DataSet ds;

        this.lbmensaje.Text = "";



        rep.lb_anio = this.ddlAnio.SelectedValue;

        rep.lb_bod = this.ddlBod.SelectedValue;

        ds = rep.mfrptEstadisticaMesAnio();


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
                this.dgData.Caption = "Estadisticas";

                this.dgData0.DataSource = dv;
                this.dgData0.DataBind();
                this.dgData0.Caption = "Estadisticas";

                mfCheck(this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue);

            }
            else
            {

                mens.mensaje(Page, "No se encontraron coincidencias.. ");
                this.lbmensaje.Text = "No se encontraron coincidencias.. ";
                this.dgData.DataSource = null;
                this.dgData.DataBind();

                this.dgData0.DataSource = null;
                this.dgData0.DataBind();

                mfSetCheck(true);
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
            mfSetCheck(true);
        }

    }

    protected void btn_Excel_Click(object sender, EventArgs e)
    {
        //ExportarExcell exp = new ExportarExcell();

        //exp.exportaExcel(gbRep);
        modFunciones mod = new modFunciones();
        //ClassBodega bod = new ClassBodega();

        string asTitulo = "Estadisticas";
        string asSubtitulo = "Estadisticas: ";
        string asCriterio = "";
        string asPie = "DTI Hospital Clinico San Borja Ariaran.";
        string lsXp = "";
        string asMembrete = "<table><tr><td width=100px align='center'>Ministerio de Salud</td></tr><tr><td width=100px align='center'>Servicio de Salud Metropolitana Central</td></tr><tr><td width=100px align='center'><u>Hospital Clínico San Borja Arriaran</u></td></tr></table>";


        asSubtitulo = "";


        if (asTitulo != "") asTitulo = "" +
         "<br><center><font size='3' face='arial'><b><u>" + asTitulo + "</u></b></font></center><br>";

        if (asSubtitulo != "") asSubtitulo = "<br>" + asSubtitulo + "<br>";

        if (asCriterio != "") asCriterio = "<font size='1' face='arial'><b>Criterios: </b>" + asCriterio + "</font><br><br>";

        if (asPie != "") asPie = "<br><font size='1' face='arial'>" + asPie + "</font><br>";





        lsXp = asMembrete + asTitulo + asSubtitulo + asCriterio + mod.fnObjRender(dgData0) + asPie;


        fnExpXls(lsXp);
        //ExportaExcell();
    }

    private string fnExpXls(string asDataXls)
    {
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

    protected void pdfBtn(object sender, ImageClickEventArgs e)
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



    protected void BtnCertificar_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            if (confirmValue == "Yes")
            {

                mfCertificar();
            }

            
        }
        catch (Exception ex)
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    private void mfCertificar()
    {
        ClassEstadisticas est = new ClassEstadisticas();

        if (this.ddlAnio.SelectedValue == "0" || this.ddlBod.SelectedValue == "0" || this.hdchk.Value == "0")
        {
            mens.mensaje(Page, "Debe de Ingresar todos los criterios.. ");
            this.lbmensaje.Text = "Debe de Ingresar todos los criterios.. ";
            return;
        }

        if (Convert.ToInt32(est.mfExisteAuditoria(this.hdchk.Value, this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue)) > 0)
        {
            mens.mensaje(Page, "Periodo ya esta Cerrado.. ");
            this.lbmensaje.Text = "Periodo ya esta Cerrado.. ";
            return;
        }


        //if (Convert.ToInt32(est.mfExisteDetalleAuditoria(this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue)) == 0)
        //{
        //    mens.mensaje(Page, "No se ha cargado Estadistica del período.. ");
        //    this.lbmensaje.Text = "No se ha cargado Estadistica del período.. ";
        //    return;
        //}


        string lsResp = est.mfInsAuditCab(this.hdchk.Value, this.ddlAnio.SelectedValue, Session["user"].ToString(), "Inserta Auditoria", this.ddlBod.SelectedValue);

        if (lsResp != "")
        {
            mens.mensaje(Page, "Problemas al Certificar.. ");
            this.lbmensaje.Text = "Problemas al Certificar.. ";
            return;
        }

        mfCheck(this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue);

    }

    protected void chk1_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk1.Checked) this.hdchk.Value = "1"; else this.hdchk.Value = "0";
    }

    protected void chk2_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk2.Checked) this.hdchk.Value = "2"; else this.hdchk.Value = "0";
    }

    protected void chk3_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk3.Checked) this.hdchk.Value = "3"; else this.hdchk.Value = "0";
    }

    protected void chk4_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk4.Checked) this.hdchk.Value = "4"; else this.hdchk.Value = "0";
    }

    protected void chk5_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk5.Checked) this.hdchk.Value = "5"; else this.hdchk.Value = "0";
    }

    protected void chk6_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk6.Checked) this.hdchk.Value = "6"; else this.hdchk.Value = "0";
    }

    protected void chk7_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk7.Checked) this.hdchk.Value = "7"; else this.hdchk.Value = "0";
    }

    protected void chk8_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk8.Checked) this.hdchk.Value = "8"; else this.hdchk.Value = "0";
    }

    protected void chk9_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk9.Checked) this.hdchk.Value = "9"; else this.hdchk.Value = "0";
    }

    protected void chk10_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk10.Checked) this.hdchk.Value = "10"; else this.hdchk.Value = "0";
    }

    protected void chk11_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk11.Checked) this.hdchk.Value = "11"; else this.hdchk.Value = "0";
    }

    protected void chk12_CheckedChanged(object sender, EventArgs e)
    {
        if (this.chk12.Checked) this.hdchk.Value = "12"; else this.hdchk.Value = "0";
    }

    protected void BtnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            if (confirmValue == "Yes")
            {

                mfLimpiar();
            }


        }
        catch (Exception ex)
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    private void mfLimpiar()
    {
        ClassEstadisticas est = new ClassEstadisticas();

        if (this.ddlAnio.SelectedValue == "0" || this.ddlBod.SelectedValue == "0" || this.hdchk.Value == "0")
        {
            mens.mensaje(Page, "Debe de Ingresar todos los criterios.. ");
            this.lbmensaje.Text = "Debe de Ingresar todos los criterios.. ";
            return;
        }

        if (Convert.ToInt32(est.mfExisteAuditoria(this.hdchk.Value, this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue)) == 0)
        {
            mens.mensaje(Page, "Periodo NO esta Cerrado.. ");
            this.lbmensaje.Text = "Periodo NO esta Cerrado.. ";
            return;
        }


        //if (Convert.ToInt32(est.mfExisteDetalleAuditoria( this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue)) == 0)
        //{
        //    mens.mensaje(Page, "No se ha cargado Estadistica del período.. ");
        //    this.lbmensaje.Text = "No se ha cargado Estadistica del período.. ";
        //    return;
        //}


        string lsResp = est.mfDeleteAuditCab(this.hdchk.Value, this.ddlAnio.SelectedValue,  this.ddlBod.SelectedValue);

        if (lsResp != "")
        {
            mens.mensaje(Page, "Problemas al Habilitar Periodo.. ");
            this.lbmensaje.Text = "Problemas al Habilitar Periodo.. ";
            return;
        }

        mfCheck(this.ddlAnio.SelectedValue, this.ddlBod.SelectedValue);

    }



}