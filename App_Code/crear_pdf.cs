using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Windows.Forms;

/// <summary>
/// Descripción breve de crear_pdf
/// </summary>
public class crear_pdf
{

    // Variables
    private int miDoc = 0;
    private Double mdbCmPFac = 28.346456693;

    // Key de búsqueda de datos en texto PDF.
    private String msKP = "";

    // Márgenes para exportar grillas de datos.
    private int miMarL = 30;
    private int miMarR = 15;
    private int miMarTB = 30;

    // Márgenes para documentos.
    private int miMarLD = 90;
    private int miMarRD = 70;
    private int miMarTD = 80;
    private int miMarBD = 50;

    private iTextSharp.text.Font myfGral = iTextSharp.text.FontFactory.GetFont("HELVETICA", 10);
    private iTextSharp.text.Font myfGraB = iTextSharp.text.FontFactory.GetFont("HELVETICA", 10, iTextSharp.text.Font.BOLD);
    private iTextSharp.text.Font myfGraU = iTextSharp.text.FontFactory.GetFont("HELVETICA", 10, iTextSharp.text.Font.UNDERLINE);
    private iTextSharp.text.Font myfGraN = iTextSharp.text.FontFactory.GetFont("HELVETICA", 10, iTextSharp.text.Font.BOLD);
    private iTextSharp.text.Font myfTIT = iTextSharp.text.FontFactory.GetFont("HELVETICA", 13, iTextSharp.text.Font.UNDERLINE);
    private iTextSharp.text.Font myfTICSN = iTextSharp.text.FontFactory.GetFont("HELVETICA", 10, iTextSharp.text.Font.UNDERLINE);



    // Fuentes Documentos.
    private iTextSharp.text.Font mfGN = iTextSharp.text.FontFactory.GetFont("HELVETICA", 12);
    private iTextSharp.text.Font mfGB = iTextSharp.text.FontFactory.GetFont("HELVETICA", 12, iTextSharp.text.Font.BOLD);
    private iTextSharp.text.Font mfGU = iTextSharp.text.FontFactory.GetFont("HELVETICA", 12, iTextSharp.text.Font.UNDERLINE);
    private iTextSharp.text.Font mfGBU = iTextSharp.text.FontFactory.GetFont("HELVETICA", 12, iTextSharp.text.Font.BOLD);
    private iTextSharp.text.Font mfTit = iTextSharp.text.FontFactory.GetFont("HELVETICA", 12, iTextSharp.text.Font.BOLD);


    static string numsal;
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

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

    public PdfPTable prop_cell_juntos(PdfPTable nombre)
    {
        nombre.DefaultCell.Border = Rectangle.NO_BORDER;
        nombre.DefaultCell.BorderWidth = 0;
        nombre.HorizontalAlignment = Element.ALIGN_LEFT;
        nombre.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
        nombre.TotalWidth = 540f;
        nombre.LockedWidth = true;

        return nombre;
    }

    public PdfPTable prop_cell_izq_der(PdfPTable nombre)
    {
        nombre.DefaultCell.Border = Rectangle.RECTANGLE;
        nombre.DefaultCell.BorderWidth = 1;
        nombre.HorizontalAlignment = Element.ALIGN_LEFT;
        nombre.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
        nombre.TotalWidth = 270f;
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


    #region RECETA
    public byte[] CrearPDFRecetaInfectologia(string Identificador)
    {

        string sSql;



        DataSet registro;
        sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
            //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
            " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, " +
            "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),R.F_H_CREACION,103)  F_H_CREACION, " +
                "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + Identificador + ") DIAS_TRAT , " +
                "ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario, " +
                "ISNULL(U2.DESCRIPCION,'S/S') SERVICIO, ISNULL(SU.DESCRIPCION,'S/S') SUBSERVICIO, ISNULL(CM.DESCRIPCION,'S/C') CAMA, " +
                "ISNULL(DIA.DESCRIPCION,'S/D') TIPO_DIAG " +
                "FROM M_RECETA R " +
                "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                "LEFT OUTER JOIN TG_DIAGNOSTICOS DIA ON DIA.IDDIAGNOSTICO = R.IDDIAGNOSTICO " +
                "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                "LEFT OUTER JOIN M_UNIDAD_OPERATIVA U2 ON U2.CODUNIOP = R.IDSERVICIO " +
                "LEFT OUTER JOIN M_SUB_UNIDAD SU ON SU.IDSUBUNIDAD = R.IDSUBSERVICIO " +
                "LEFT OUTER JOIN M_CAMAS CM ON CM.IDCAMA = R.IDCAMA " +
                "where IDRECETA = " + Identificador;

        con = bd.fnGetConn();
        registro = bd.Fill(con, sSql);
        con.Close();


        string sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
        string sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
        string sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
        string sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
        string snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
        string sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
        string sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
        string sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
        string sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
        string sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
        string sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
        string sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
        string sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();

        string sSERVICIO = registro.Tables[0].Rows[0]["SERVICIO"].ToString();
        string sSUBSERVICIO = registro.Tables[0].Rows[0]["SUBSERVICIO"].ToString();
        string sCAMA = registro.Tables[0].Rows[0]["CAMA"].ToString();
        string sTIPO_DIAG = registro.Tables[0].Rows[0]["TIPO_DIAG"].ToString();


        registro.Dispose();



        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));


        Document doc = new Document(PageSize.LETTER, 30f, 1.5f, 30f, 1.5f);


        using (MemoryStream output = new MemoryStream())
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, output);
            //EventoTitulos ev = new EventoTitulos(); wri.PageEvent = ev;
            DateTime fecha_actual = DateTime.Today;
            bool b;
            string asMsg;


            asMsg = sFOLIO;

            EventoTitulosInfectologia ev = new EventoTitulosInfectologia(asMsg);
            wri.PageEvent = ev;

            doc.Open();
            b = doc.AddAuthor("Sistema de Farmacia.");
            b = doc.AddTitle("Receta Electrónica.");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
            iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

            logo.ScaleAbsolute(180, 46);
            linea.ScaleAbsolute(520, 3);

            //doc.Add(logo);

            BaseColor color_negro = new BaseColor(0, 0, 0);

            Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
            Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);
            Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);



            Paragraph ph;
            Paragraph espacio = new Paragraph(" ");


            //Chunk salto = new Chunk
            //("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            //Paragraph salta = new Paragraph();
            //salta.Alignment = Element.ALIGN_LEFT;
            //salta.Add(salto);


            //// Tipo Paciente
            //PdfPTable tablePac = new PdfPTable(3);
            //prop_cell(tablePac);

            //float[] TamColumPac = new float[] { 1.0f, 1.5f, 1.0f};
            //tablePac.SetWidths(TamColumPac);
            //tablePac.HorizontalAlignment = Element.ALIGN_CENTER;

            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
            //tablePac.AddCell(getCellNegrita("PACIENTE AMBULATORIO ", 8, PdfPCell.ALIGN_CENTER, 0));
            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

            //doc.Add(tablePac);

            // Cabecera
            PdfPTable tableCab = new PdfPTable(4);
            prop_cell(tableCab);

            float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableCab.SetWidths(TamColumCab);
            tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////
            if (snombreSocial == "")
            {
                tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sNOMBRE + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));
            }
            else
            {
                tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(snombreSocial, 8, PdfPCell.ALIGN_LEFT, 1));

            }

            tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////

            tableCab.AddCell(getCellNegrita("Fecha Emisión: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sF_H_CREACION, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Servicio", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sSERVICIO, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////

            tableCab.AddCell(getCellNegrita("Sub Servicio: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sSUBSERVICIO, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Cama", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sCAMA, 8, PdfPCell.ALIGN_LEFT, 1));

            doc.Add(tableCab);

            doc.Add(espacio);
            //doc.Add(linea);

            // Detalle Despachos 1
            PdfPTable tableDetCab = new PdfPTable(10);
            prop_cell(tableDetCab);

            float[] TamColumDetCab = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
            tableDetCab.SetWidths(TamColumDetCab);
            tableDetCab.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDetCab.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));


            tableDetCab.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));

            //doc.Add(tableDetCab);

            DataSet registroDet;
            sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES,  ";
            sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
            sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  ";
            sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
            //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
            sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
            sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
            sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD ";
            sSql = sSql + " from M_ART_RECETA D  ";
            sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
            sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
            sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
            sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
            sSql = sSql + "where D.IDRECETA = " + Identificador + " AND ISNULL(D.IDESTADO,1) <> 3 ";
            sSql = sSql + " order by   V.CODARTICULO asc";

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();


            DataTable dt = registroDet.Tables[0];

            int i = 1;
            int j = 1;
            string sCODARTICULO = string.Empty;
            string sNOM_ARTICULO = string.Empty;
            string sDPRESENT = string.Empty;
            string sFORMA = string.Empty;
            string sDRANGO = string.Empty;
            string sDVIA = string.Empty;
            string sCANTIDAD = string.Empty;
            string sDIAS_TRATA = string.Empty;
            string sOBSERVACIONES = string.Empty;
            string sCALCULAR = string.Empty;



            // Detalle Despachos 1
            PdfPTable tableDet = new PdfPTable(10);
            prop_cell(tableDet);

            float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
            tableDet.SetWidths(TamColumDet);
            tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));


            foreach (DataRow row in dt.Rows)
            {

                sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                sDPRESENT = Convert.ToString(row["DPRESENT"]);
                sFORMA = Convert.ToString(row["FORMA"]);
                sDRANGO = Convert.ToString(row["DRANGO"]);
                sDVIA = Convert.ToString(row["DVIA"]);
                sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                sCALCULAR = Convert.ToString(row["CALCULAR"]);
                if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                {
                    sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                }
                else
                {
                    sDIAS_TRATA = "0";
                }
                sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);



                tableDet.AddCell(getCell(i.ToString(), 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCell(sCODARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sNOM_ARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sFORMA, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sDRANGO, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sDVIA, 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCell(sCANTIDAD, 7, PdfPCell.ALIGN_CENTER, 1));


                if (sCALCULAR == "1")
                    tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));
                else
                    tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCell(" ", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCell(sOBSERVACIONES, 7, PdfPCell.ALIGN_LEFT, 1));


                i++;
                doc.Add(tableDet);

                tableDet.Rows.Clear();

                if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
                {
                    doc.NewPage();
                    doc.Add(tableDetCab);
                    j++;
                }
            }


            if (sOBSERVACION != "")
            {
                PdfPTable tableObs = new PdfPTable(1);
                prop_cell(tableObs);
                tableObs.HorizontalAlignment = Element.ALIGN_LEFT;

                tableObs.AddCell(getCellNegrita("Indicaciones:", 8, PdfPCell.ALIGN_LEFT, 0));
                tableObs.AddCell(getCell(sOBSERVACION, 8, PdfPCell.ALIGN_LEFT, 1));
                doc.Add(tableObs);
                doc.Add(espacio);
            }
            else
            {
                doc.Add(espacio);
            }


            doc.Add(espacio);
            doc.Add(espacio);

            Usuarios usr = new Usuarios();
            DataSet aoDs = usr.TraeDatosUsuario(sidusuario);

            if (aoDs != null && aoDs.Tables.Count > 0)
            {
                if (aoDs.Tables[0].Rows.Count > 0)
                {
                    PdfPTable tableFirma = new PdfPTable(2);
                    prop_cell(tableFirma);

                    float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                    tableFirma.SetWidths(TamColumFirma);
                    tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                    tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Dr. " + aoDs.Tables[0].Rows[0]["NOMBRE"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Rut: " + aoDs.Tables[0].Rows[0]["RUT_DV"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                    tableFirma.AddCell(getCell("Especialidad: " + aoDs.Tables[0].Rows[0]["especialidad"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    doc.Add(tableFirma);
                    doc.Add(espacio);
                }
            }

            //doc.Add(espacio);
            //string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

            //ph = new Paragraph(Rayas, font_celdas);
            //doc.Add(ph);
            //string Gerencia = "Información Farmacia";

            //ph = new Paragraph(Gerencia, font_celdas);
            //doc.Add(ph);

            //ph = new Paragraph(Rayas, font_celdas);
            //doc.Add(ph);

            doc.Add(espacio);

            // Farmacia
            PdfPTable tableFarm = new PdfPTable(4);
            prop_cell(tableFarm);

            float[] TamColumFarm = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableFarm.SetWidths(TamColumFarm);
            tableFarm.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            tableFarm.AddCell(getCellNegrita("Tipo Diagnóstico:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sTIPO_DIAG, 8, PdfPCell.ALIGN_LEFT, 1));

            tableFarm.AddCell(getCellNegrita("Diagnóstico:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sDIAGNOSTICO, 8, PdfPCell.ALIGN_LEFT, 1));



            /////////////////////////////////////////////////////
            tableFarm.AddCell(getCellNegrita("Unidad Solicitante:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("Previsión:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sPREVISION, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////
            tableFarm.AddCell(getCellNegrita("Días Tratamiento:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sDIAS_TRAT, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));


            doc.Add(tableFarm);

            doc.Add(espacio);

            // PIE
            PdfPTable tablePie = new PdfPTable(5);
            prop_cell(tablePie);

            float[] TamColumPie = new float[] { 1.0f, 1.5f, 1.0f, 1.5f, 1.5f };
            tablePie.SetWidths(TamColumPie);
            tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            tablePie.AddCell(getCellNegrita("Uso Exclusivo Farmacia", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("RECIBE", 8, PdfPCell.ALIGN_LEFT, 1));


            tablePie.AddCell(getCell("ESCRIBE", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("PREPARA", 8, PdfPCell.ALIGN_LEFT, 1));

            tablePie.AddCell(getCell("ENTREGA", 8, PdfPCell.ALIGN_LEFT, 1));

            doc.Add(tablePie);

            doc.Close();
            return output.ToArray();
        }

    }
    //OK 9
    public byte[] CrearPDFReceta(string Identificador)
    {

        string sSql;



        DataSet registro;
        sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
            //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
            " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, " +
            "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
                "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + Identificador + ") DIAS_TRAT ,ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                "FROM M_RECETA R " +
                "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                "where IDRECETA = " + Identificador;

        con = bd.fnGetConn();
        registro = bd.Fill(con, sSql);
        con.Close();


        string sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
        string sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
        string sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
        string sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
        string snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
        string sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
        string sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
        string sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
        string sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
        string sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
        string sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
        string sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
        string sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();


        registro.Dispose();



        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));


        Document doc = new Document(PageSize.LETTER, 30f, 1.5f, 30f, 1.5f);


        using (MemoryStream output = new MemoryStream())
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, output);
            //EventoTitulos ev = new EventoTitulos(); wri.PageEvent = ev;
            DateTime fecha_actual = DateTime.Today;
            bool b;
            string asMsg;


            asMsg = sFOLIO;

            EventoTitulos ev = new EventoTitulos(asMsg);
            wri.PageEvent = ev;

            doc.Open();
            b = doc.AddAuthor("Sistema de Farmacia.");
            b = doc.AddTitle("Receta Electrónica.");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
            iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

            logo.ScaleAbsolute(180, 46);
            linea.ScaleAbsolute(520, 3);

            //doc.Add(logo);

            BaseColor color_negro = new BaseColor(0, 0, 0);

            Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
            Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);
            Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);



            Paragraph ph;
            Paragraph espacio = new Paragraph(" ");


            //Chunk salto = new Chunk
            //("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            //Paragraph salta = new Paragraph();
            //salta.Alignment = Element.ALIGN_LEFT;
            //salta.Add(salto);


            //// Tipo Paciente
            //PdfPTable tablePac = new PdfPTable(3);
            //prop_cell(tablePac);

            //float[] TamColumPac = new float[] { 1.0f, 1.5f, 1.0f};
            //tablePac.SetWidths(TamColumPac);
            //tablePac.HorizontalAlignment = Element.ALIGN_CENTER;

            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
            //tablePac.AddCell(getCellNegrita("PACIENTE AMBULATORIO ", 8, PdfPCell.ALIGN_CENTER, 0));
            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

            //doc.Add(tablePac);

            // Cabecera
            PdfPTable tableCab = new PdfPTable(4);
            prop_cell(tableCab);

            float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableCab.SetWidths(TamColumCab);
            tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            if (snombreSocial == "")
            {
                tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sNOMBRE, 8, PdfPCell.ALIGN_LEFT, 1));
            }
            else
            {
                tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(snombreSocial + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));

            }

            tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////

            tableCab.AddCell(getCellNegrita("Fecha Emisión: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sF_H_CREACION, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

            doc.Add(tableCab);

            doc.Add(espacio);
            //doc.Add(linea);

            // Detalle Despachos 1
            PdfPTable tableDetCab = new PdfPTable(10);
            prop_cell(tableDetCab);

            float[] TamColumDetCab = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
            tableDetCab.SetWidths(TamColumDetCab);
            tableDetCab.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDetCab.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));


            tableDetCab.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));

            //doc.Add(tableDetCab);

            DataSet registroDet;
            sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES,  ";
            sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
            sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  ";
            sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
            //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
            sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
            sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
            sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD , ISNULL(DIA.DIA,'') DIA  ";
            sSql = sSql + " from M_ART_RECETA D  ";
            sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
            sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
            sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
            sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
            sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
            sSql = sSql + "where D.IDRECETA = " + Identificador + " AND ISNULL(D.IDESTADO,1) <> 3 ";
            sSql = sSql + " order by   V.CODARTICULO asc";

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();


            DataTable dt = registroDet.Tables[0];

            int i = 1;
            int j = 1;
            string sCODARTICULO = string.Empty;
            string sNOM_ARTICULO = string.Empty;
            string sDPRESENT = string.Empty;
            string sFORMA = string.Empty;
            string sDRANGO = string.Empty;
            string sDVIA = string.Empty;
            string sCANTIDAD = string.Empty;
            string sDIAS_TRATA = string.Empty;
            string sOBSERVACIONES = string.Empty;
            string sCALCULAR = string.Empty;
            string sDIA = string.Empty;



            // Detalle Despachos 1
            PdfPTable tableDet = new PdfPTable(10);
            prop_cell(tableDet);

            float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
            tableDet.SetWidths(TamColumDet);
            tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));


            foreach (DataRow row in dt.Rows)
            {

                sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                sDPRESENT = Convert.ToString(row["DPRESENT"]);
                sFORMA = Convert.ToString(row["FORMA"]);
                sDRANGO = Convert.ToString(row["DRANGO"]);
                sDVIA = Convert.ToString(row["DVIA"]);
                sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                sCALCULAR = Convert.ToString(row["CALCULAR"]);
                if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                {
                    sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                }
                else
                {
                    sDIAS_TRATA = "0";
                }
                sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);


                if (Convert.ToString(row["DIA"]) == "")
                    sDIA = "";
                else
                    sDIA = " / " + Convert.ToString(row["DIA"]) + "";

                tableDet.AddCell(getCell(i.ToString(), 6, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCell(sCODARTICULO, 6, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sNOM_ARTICULO, 6, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sFORMA, 6, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sDRANGO, 6, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sDVIA, 6, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCell(sCANTIDAD, 6, PdfPCell.ALIGN_CENTER, 1));


                if (sCALCULAR == "1")
                    tableDet.AddCell(getCell(sDIAS_TRATA + sDIA, 6, PdfPCell.ALIGN_CENTER, 1));
                else
                    tableDet.AddCell(getCell(sDIAS_TRATA, 6, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCell(" ", 6, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCell(sOBSERVACIONES, 6, PdfPCell.ALIGN_LEFT, 1));


                i++;
                doc.Add(tableDet);

                tableDet.Rows.Clear();

                if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
                {
                    doc.NewPage();
                    doc.Add(tableDetCab);
                    j++;
                }
            }


            if (sOBSERVACION != "")
            {
                PdfPTable tableObs = new PdfPTable(1);
                prop_cell(tableObs);
                tableObs.HorizontalAlignment = Element.ALIGN_LEFT;

                tableObs.AddCell(getCellNegrita("Indicaciones:", 8, PdfPCell.ALIGN_LEFT, 0));
                tableObs.AddCell(getCell(sOBSERVACION, 8, PdfPCell.ALIGN_LEFT, 1));
                doc.Add(tableObs);
                doc.Add(espacio);
            }
            else
            {
                doc.Add(espacio);
            }


            doc.Add(espacio);
            doc.Add(espacio);

            Usuarios usr = new Usuarios();
            DataSet aoDs = usr.TraeDatosUsuario(sidusuario);
            ClassReceta rec = new ClassReceta();
            if (aoDs != null && aoDs.Tables.Count > 0 && rec.mfRutNombMedico(Identificador) == "-1")
            {
                if (aoDs.Tables[0].Rows.Count > 0)
                {
                    PdfPTable tableFirma = new PdfPTable(2);
                    prop_cell(tableFirma);

                    float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                    tableFirma.SetWidths(TamColumFirma);
                    tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                    tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Dr. " + aoDs.Tables[0].Rows[0]["NOMBRE"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Rut: " + aoDs.Tables[0].Rows[0]["RUT_DV"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                    tableFirma.AddCell(getCell("Especialidad: " + aoDs.Tables[0].Rows[0]["especialidad"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    doc.Add(tableFirma);
                    doc.Add(espacio);
                }

            }
            else
            {
                PdfPTable tableFirma = new PdfPTable(2);
                prop_cell(tableFirma);

                float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                tableFirma.SetWidths(TamColumFirma);
                tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                tableFirma.AddCell(getCell("Dr. " + rec.mfGetNombMedico(Identificador), 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                tableFirma.AddCell(getCell("Rut: " + rec.mfRutNombMedico(Identificador), 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                tableFirma.AddCell(getCell(" ", 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                doc.Add(tableFirma);
                doc.Add(espacio);
            }

            //doc.Add(espacio);
            //string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

            //ph = new Paragraph(Rayas, font_celdas);
            //doc.Add(ph);
            //string Gerencia = "Información Farmacia";

            //ph = new Paragraph(Gerencia, font_celdas);
            //doc.Add(ph);

            //ph = new Paragraph(Rayas, font_celdas);
            //doc.Add(ph);

            doc.Add(espacio);

            // Farmacia
            PdfPTable tableFarm = new PdfPTable(4);
            prop_cell(tableFarm);

            float[] TamColumFarm = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableFarm.SetWidths(TamColumFarm);
            tableFarm.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            tableFarm.AddCell(getCellNegrita("Diagnóstico:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sDIAGNOSTICO, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));





            /////////////////////////////////////////////////////
            tableFarm.AddCell(getCellNegrita("Unidad Solicitante:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("Previsión:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sPREVISION, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////
            tableFarm.AddCell(getCellNegrita("Días Tratamiento:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sDIAS_TRAT, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));


            doc.Add(tableFarm);

            doc.Add(espacio);

            // PIE
            PdfPTable tablePie = new PdfPTable(5);
            prop_cell(tablePie);

            float[] TamColumPie = new float[] { 1.0f, 1.5f, 1.0f, 1.5f, 1.5f };
            tablePie.SetWidths(TamColumPie);
            tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            tablePie.AddCell(getCellNegrita("Uso Exclusivo Farmacia", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("RECIBE", 8, PdfPCell.ALIGN_LEFT, 1));


            tablePie.AddCell(getCell("ESCRIBE", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("PREPARA", 8, PdfPCell.ALIGN_LEFT, 1));

            tablePie.AddCell(getCell("ENTREGA", 8, PdfPCell.ALIGN_LEFT, 1));

            doc.Add(tablePie);

            doc.Close();
            return output.ToArray();
        }

    }

    public byte[] CrearPDFCopiasReceta(string Identificador)
    {

        string sSql;

        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));


        Document doc = new Document(PageSize.LETTER, 30f, 1.5f, 30f, 1.5f);


        using (MemoryStream output = new MemoryStream())
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, output);
            //EventoTitulos ev = new EventoTitulos(); wri.PageEvent = ev;
            DateTime fecha_actual = DateTime.Today;
            bool b;
            string asMsg;


            DataSet regRecet;
            sSql = "SELECT A.IDRECETA " +
                   " FROM M_RECETA A " +
                   " where (a.IDRECETA = " + Identificador + " or a.IDREC_PADRE = " + Identificador + ")";

            con = bd.fnGetConn();
            regRecet = bd.Fill(con, sSql);
            con.Close();

            doc.Open();

            foreach (DataRow rw in regRecet.Tables[0].Rows)
            {
                String lsIdPR = rw["IDRECETA"].ToString();
                string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");
                string rutalogo = ruta;

                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
                iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 70 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                //imagen.ScaleAbsolute(50f, 50f);

                //document.Add(imagen);

                DataSet registro;
                sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
                //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
                " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, " +
                    "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD,convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
                "(SELECT CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 AND IDRECETA = " + Identificador + ") DIAS_TRAT , " +
                "ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                "FROM M_RECETA R " +
                "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                "where IDRECETA = " + lsIdPR;

                con = bd.fnGetConn();
                registro = bd.Fill(con, sSql);
                con.Close();


                string sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
                string sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
                string sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
                string sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
                string snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
                string sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
                string sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
                string sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
                string sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
                string sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
                string sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
                string sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
                string sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();


                registro.Dispose();

                // Cabecera Derecha
                PdfPTable tableDer = new PdfPTable(1);
                float[] anchosDer = new float[] { 0.50f };
                tableDer.DefaultCell.BorderWidth = 0;
                tableDer.SetWidths(anchosDer);

                tableDer.WidthPercentage = 90;

                //////////////////////////////////////////

                tableDer.AddCell(imagen2);


                // Cabecera Izquierda
                PdfPTable tableIzq = new PdfPTable(1);
                float[] anchosIzq = new float[] { 0.50f };
                tableIzq.DefaultCell.BorderWidth = 0;
                tableIzq.SetWidths(anchosIzq);

                tableIzq.WidthPercentage = 90;

                //////////////////////////////////////////

                tableIzq.AddCell(imagen);

                // Cabecera Titulo
                PdfPTable tableTit = new PdfPTable(2);
                float[] anchosTit = new float[] { 1.50f, 1.0f };
                tableTit.DefaultCell.BorderWidth = 0;
                tableTit.SetWidths(anchosTit);

                tableTit.WidthPercentage = 90;

                //////////////////////////////////////////
                PdfPCell cell = new PdfPCell(new Phrase("RECETA FARMACIA"));
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.Border = PdfPCell.NO_BORDER;

                tableTit.AddCell(cell);
                Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
                cell = new PdfPCell(new Phrase("N° " + sFOLIO, font));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableTit.AddCell(cell);


                //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
                // Cabecera CENTRO
                PdfPTable tableCent = new PdfPTable(1);
                float[] anchos = new float[] { 0.50f };
                tableCent.DefaultCell.BorderWidth = 0;
                tableCent.SetWidths(anchos);

                tableCent.WidthPercentage = 90;

                //////////////////////////////////////////

                cell = new PdfPCell(new Phrase("  "));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableCent.AddCell(cell);

                //////////////////////////////////////////


                tableCent.AddCell(tableTit);

                //////////////////////////////////////////

                cell = new PdfPCell(new Phrase("  "));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableCent.AddCell(cell);

                //////////////////////////////////////////

                cell = new PdfPCell(new Phrase("PACIENTE"));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableCent.AddCell(cell);

                //////////////////////////////////////////

                cell = new PdfPCell(new Phrase("AMBULATORIO"));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableCent.AddCell(cell);


                ///////////////////////////////////////////

                // Hora Hospital

                string Hosp = "Hospital Clínico San Borja Arriaran";

                Chunk Hosp_chuck = new Chunk
                    (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
                Paragraph Hosp_par = new Paragraph();
                Hosp_par.Alignment = Element.ALIGN_RIGHT;
                Hosp_par.Add(Hosp_chuck);


                PdfPTable tableHosp = new PdfPTable(3);
                tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
                tableHosp.DefaultCell.BorderWidth = 0;
                tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;
                tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                tableHosp.TotalWidth = 540f;
                tableHosp.LockedWidth = true;

                float[] TamColumHora = new float[] { 0.15f, 0.50f, 0.15f };
                tableHosp.SetWidths(TamColumHora);
                tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;

                tableHosp.AddCell(tableIzq);
                tableHosp.AddCell(tableCent);
                tableHosp.AddCell(tableDer);
                //tableHosp.AddCell(Hosp_par);


                doc.Add(tableHosp);

                asMsg = sFOLIO;

                // Salto

                Chunk saltoP = new Chunk
                ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
                Paragraph saltaP = new Paragraph();
                saltaP.Alignment = Element.ALIGN_LEFT;
                saltaP.Add(saltoP);


                //Paragraph p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;

                //Chunk c = new Chunk
                //    (asMsg, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

                //p.Add(c);



                //doc.Add(p);
                doc.Add(saltaP);
                //EventoTitulos ev = new EventoTitulos(asMsg);
                //wri.PageEvent = ev;

                b = doc.AddAuthor("Sistema de Farmacia.");
                b = doc.AddTitle("Receta Electrónica.");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
                iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

                logo.ScaleAbsolute(180, 46);
                linea.ScaleAbsolute(520, 3);

                //doc.Add(logo);

                BaseColor color_negro = new BaseColor(0, 0, 0);

                Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
                Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
                Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
                Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
                Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);
                Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);



                Paragraph ph;
                Paragraph espacio = new Paragraph(" ");


                //Chunk salto = new Chunk
                //("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
                //Paragraph salta = new Paragraph();
                //salta.Alignment = Element.ALIGN_LEFT;
                //salta.Add(salto);

                // Tipo Paciente
                //PdfPTable tablePac = new PdfPTable(3);
                //prop_cell(tablePac);

                //float[] TamColumPac = new float[] { 1.0f, 1.5f, 1.0f };
                //tablePac.SetWidths(TamColumPac);
                //tablePac.HorizontalAlignment = Element.ALIGN_CENTER;

                //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
                //tablePac.AddCell(getCellNegrita("PACIENTE AMBULATORIO ", 8, PdfPCell.ALIGN_CENTER, 0));
                //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                //doc.Add(tablePac);

                // Cabecera
                PdfPTable tableCab = new PdfPTable(4);
                prop_cell(tableCab);

                float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
                tableCab.SetWidths(TamColumCab);
                tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


                /////////////////////////////////////////////////////
                if (snombreSocial == "")
                {
                    tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                    tableCab.AddCell(getCell(sNOMBRE, 8, PdfPCell.ALIGN_LEFT, 1));
                }
                else
                {
                    tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                    tableCab.AddCell(getCell(snombreSocial + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));

                }

                tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

                /////////////////////////////////////////////////////

                tableCab.AddCell(getCellNegrita("Fecha Emisión: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sF_H_CREACION, 8, PdfPCell.ALIGN_LEFT, 1));

                tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

                doc.Add(tableCab);

                doc.Add(espacio);
                //doc.Add(linea);

                // Detalle Despachos 1
                PdfPTable tableDetCab = new PdfPTable(10);
                prop_cell(tableDetCab);

                float[] TamColumDetCab = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
                tableDetCab.SetWidths(TamColumDetCab);
                tableDetCab.HorizontalAlignment = Element.ALIGN_LEFT;

                tableDetCab.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));


                tableDetCab.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDetCab.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));

                //doc.Add(tableDetCab);

                DataSet registroDet;
                //sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, D.POSOLOGIA sDIAS_TRATA, D.OBSERVACIONES,  ";
                //sSql = sSql + "ISNULL(V.UN_MED,'-') DPRESENT,  ";
                //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
                //sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,ISNULL(D.DURACION,0) CANTIDAD ";
                //sSql = sSql + " from M_ART_RECETA D  ";
                //sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
                //sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
                //sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
                //sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
                //sSql = sSql + "where D.IDRECETA = " + Identificador + " ";
                //sSql = sSql + " order by   V.CODARTICULO asc";


                sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, D.OBSERVACIONES,  ";
                sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR, ";
                sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  ";
                sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
                //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
                sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
                sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
                sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA  ";
                sSql = sSql + " from M_ART_RECETA D  ";
                sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
                sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
                sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
                sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
                sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
                sSql = sSql + "where D.IDRECETA = " + Identificador + " AND ISNULL(D.IDESTADO,1) <> 3 ";
                sSql = sSql + " order by   V.CODARTICULO asc";

                con = bd.fnGetConn();
                registroDet = bd.Fill(con, sSql);
                con.Close();


                DataTable dt = registroDet.Tables[0];

                int i = 1;
                int j = 1;
                string sCODARTICULO = string.Empty;
                string sNOM_ARTICULO = string.Empty;
                string sDPRESENT = string.Empty;
                string sFORMA = string.Empty;
                string sDRANGO = string.Empty;
                string sDVIA = string.Empty;
                string sCANTIDAD = string.Empty;
                string sDIAS_TRATA = string.Empty;
                string sOBSERVACIONES = string.Empty;
                string sCALCULAR = string.Empty;
                string sDIA = string.Empty;

                // Detalle Despachos 1
                PdfPTable tableDet = new PdfPTable(10);
                prop_cell(tableDet);

                float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
                tableDet.SetWidths(TamColumDet);
                tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

                tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));

                foreach (DataRow row in dt.Rows)
                {

                    sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                    sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                    sDPRESENT = Convert.ToString(row["DPRESENT"]);
                    sFORMA = Convert.ToString(row["FORMA"]);
                    sDRANGO = Convert.ToString(row["DRANGO"]);
                    sDVIA = Convert.ToString(row["DVIA"]);
                    sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                    sCALCULAR = Convert.ToString(row["CALCULAR"]);

                    if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                    {
                        sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                    }
                    else
                    {
                        sDIAS_TRATA = "0";
                    }

                    sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);


                    if (Convert.ToString(row["DIA"]) == "")
                        sDIA = "";
                    else
                        sDIA = " / " + Convert.ToString(row["DIA"]) + "";

                    tableDet.AddCell(getCell(i.ToString(), 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sCODARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sNOM_ARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sFORMA, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sDRANGO, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sDVIA, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sCANTIDAD, 7, PdfPCell.ALIGN_LEFT, 1));


                    if (sCALCULAR == "1")
                        tableDet.AddCell(getCell(sDIAS_TRATA + sDIA, 7, PdfPCell.ALIGN_CENTER, 1));
                    else
                        tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));

                    tableDet.AddCell(getCell(" ", 7, PdfPCell.ALIGN_LEFT, 1));

                    tableDet.AddCell(getCell(sOBSERVACIONES, 7, PdfPCell.ALIGN_LEFT, 1));

                    i++;
                    doc.Add(tableDet);

                    tableDet.Rows.Clear();

                    if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
                    {
                        doc.NewPage();
                        doc.Add(tableDetCab);
                        j++;
                    }
                }


                if (sOBSERVACION != "")
                {
                    PdfPTable tableObs = new PdfPTable(1);
                    prop_cell(tableObs);
                    tableObs.HorizontalAlignment = Element.ALIGN_LEFT;

                    tableObs.AddCell(getCellNegrita("Indicaciones:", 8, PdfPCell.ALIGN_LEFT, 0));
                    tableObs.AddCell(getCell(sOBSERVACION, 8, PdfPCell.ALIGN_LEFT, 1));
                    doc.Add(tableObs);
                    doc.Add(espacio);
                }
                else
                {
                    doc.Add(espacio);
                }


                doc.Add(espacio);
                doc.Add(espacio);

                Usuarios usr = new Usuarios();
                DataSet aoDs = usr.TraeDatosUsuario(sidusuario);
                ClassReceta rec = new ClassReceta();
                if (aoDs != null && aoDs.Tables.Count > 0 && rec.mfRutNombMedico(Identificador) == "-1")
                {
                    if (aoDs.Tables[0].Rows.Count > 0)
                    {
                        PdfPTable tableFirma = new PdfPTable(2);
                        prop_cell(tableFirma);

                        float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                        tableFirma.SetWidths(TamColumFirma);
                        tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                        tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                        tableFirma.AddCell(getCell("Dr. " + aoDs.Tables[0].Rows[0]["NOMBRE"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                        tableFirma.AddCell(getCell("Rut: " + aoDs.Tables[0].Rows[0]["RUT_DV"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                        tableFirma.AddCell(getCell("Especialidad: " + aoDs.Tables[0].Rows[0]["especialidad"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                        doc.Add(tableFirma);
                        doc.Add(espacio);
                    }

                }
                else
                {
                    PdfPTable tableFirma = new PdfPTable(2);
                    prop_cell(tableFirma);

                    float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                    tableFirma.SetWidths(TamColumFirma);
                    tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                    tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Dr. " + rec.mfGetNombMedico(Identificador), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Rut: " + rec.mfRutNombMedico(Identificador), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                    tableFirma.AddCell(getCell(" ", 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    doc.Add(tableFirma);
                    doc.Add(espacio);
                }

                //Usuarios usr = new Usuarios();
                //DataSet aoDs = usr.TraeDatosUsuario(sidusuario);

                //PdfPTable tableFirma = new PdfPTable(2);
                //prop_cell(tableFirma);

                //float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                //tableFirma.SetWidths(TamColumFirma);
                //tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                //tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                //tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                //tableFirma.AddCell(getCell("Dr. " + aoDs.Tables[0].Rows[0]["NOMBRE"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                //tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                //tableFirma.AddCell(getCell("Rut: " + aoDs.Tables[0].Rows[0]["RUT_DV"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                //tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                //tableFirma.AddCell(getCell("Especialidad: " + aoDs.Tables[0].Rows[0]["especialidad"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                //tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                //doc.Add(tableFirma);
                //doc.Add(espacio);

                //doc.Add(espacio);
                //string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

                //ph = new Paragraph(Rayas, font_celdas);
                //doc.Add(ph);
                //string Gerencia = "Información Farmacia";

                //ph = new Paragraph(Gerencia, font_celdas);
                //doc.Add(ph);

                //ph = new Paragraph(Rayas, font_celdas);
                //doc.Add(ph);

                doc.Add(espacio);

                // Farmacia
                PdfPTable tableFarm = new PdfPTable(4);
                prop_cell(tableFarm);

                float[] TamColumFarm = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
                tableFarm.SetWidths(TamColumFarm);
                tableFarm.HorizontalAlignment = Element.ALIGN_LEFT;


                /////////////////////////////////////////////////////

                tableFarm.AddCell(getCellNegrita("Diagnótico:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableFarm.AddCell(getCell(sDIAGNOSTICO, 8, PdfPCell.ALIGN_LEFT, 1));


                tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
                tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));


                /////////////////////////////////////////////////////
                tableFarm.AddCell(getCellNegrita("Unidad Solicitante:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableFarm.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));


                tableFarm.AddCell(getCellNegrita("Previsión:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableFarm.AddCell(getCell(sPREVISION, 8, PdfPCell.ALIGN_LEFT, 1));

                /////////////////////////////////////////////////////
                tableFarm.AddCell(getCellNegrita("Días Tratamiento:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableFarm.AddCell(getCell(sDIAS_TRAT, 8, PdfPCell.ALIGN_LEFT, 1));


                tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
                tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));


                doc.Add(tableFarm);

                // PIE
                PdfPTable tablePie = new PdfPTable(5);
                prop_cell(tablePie);

                float[] TamColumPie = new float[] { 1.0f, 1.5f, 1.0f, 1.5f, 1.5f };
                tablePie.SetWidths(TamColumPie);
                tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


                /////////////////////////////////////////////////////

                tablePie.AddCell(getCellNegrita("Uso Exclusivo Farmacia", 8, PdfPCell.ALIGN_LEFT, 1));
                tablePie.AddCell(getCell("RECIBE", 8, PdfPCell.ALIGN_LEFT, 1));


                tablePie.AddCell(getCell("ESCRIBE", 8, PdfPCell.ALIGN_LEFT, 1));
                tablePie.AddCell(getCell("PREPARA", 8, PdfPCell.ALIGN_LEFT, 1));

                tablePie.AddCell(getCell("ENTREGA", 8, PdfPCell.ALIGN_LEFT, 1));

                doc.Add(tablePie);

                doc.NewPage();
            }


            doc.Close();
            return output.ToArray();
        }

    }

    public byte[] CrearPDFRecetaAll(DataSet loDs, String asCodusuario)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        Document doc = new Document(PageSize.LETTER, miMarL, miMarR, (miMarTB * 3), miMarTB * 1);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);

        // Arma tabla de Encabezado.
        Single[] colDtX = new Single[] { 150, 260, 120 };
        PdfPTable loTbX = new PdfPTable(3);

        loTbX.DefaultCell.Border = PdfPCell.NO_BORDER;
        String lsImCln = String.Empty;


        //EventoTitulosAll ev = new EventoTitulosAll();
        //writer.PageEvent = ev;
        //// Asigna eventos.
        //DataSet aoDsEv = null;
        //_events loEv = new _events("OC", loTbX, lsImCln, aoDsEv);

        //writer.PageEvent = loEv;
        writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, "ownerpass", PdfWriter.AllowPrinting);
        // Agrega metadata al documento.
        doc.AddCreator("walter.pizarro@minsal.gob.cl");
        doc.AddAuthor("Wpizarror®");

        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);
        Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);

        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;

        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");



        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));


        // Abre documento.
        doc.Open();

        foreach (DataRow rw in loDs.Tables[0].Rows)
        {
            DataSet registro;
            string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
                //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
                " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, " +
                "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
                    "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + rw["ID"].ToString() + ") DIAS_TRAT ,ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                    "FROM M_RECETA R " +
                    "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                    "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                    "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                    "where IDRECETA = " + rw["ID"].ToString();

            con = bd.fnGetConn();
            registro = bd.Fill(con, sSql);
            con.Close();


            sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
            sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
            sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
            sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
            snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
            sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
            sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
            sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
            sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
            sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
            sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
            sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
            sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();


            registro.Dispose();


            //////////////////////////////////////////////////////////////////////////////////////////////


            string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");

            //Application.StartupPath;
            string rutaLogo = Application.UserAppDataPath;
            string rutaLogo2 = Application.LocalUserAppDataPath;
            string rutalogo = ruta;

            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
            iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_LEFT;
            float percentage = 0.0f;
            percentage = 70 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            //imagen.ScaleAbsolute(50f, 50f);

            //document.Add(imagen);

            // Cabecera Derecha
            PdfPTable tableDer = new PdfPTable(1);
            float[] anchosDer = new float[] { 0.50f };
            tableDer.DefaultCell.BorderWidth = 0;
            tableDer.SetWidths(anchosDer);

            tableDer.WidthPercentage = 90;

            //////////////////////////////////////////

            tableDer.AddCell(imagen2);


            // Cabecera Izquierda
            PdfPTable tableIzq = new PdfPTable(1);
            float[] anchosIzq = new float[] { 0.50f };
            tableIzq.DefaultCell.BorderWidth = 0;
            tableIzq.SetWidths(anchosIzq);

            tableIzq.WidthPercentage = 90;

            //////////////////////////////////////////

            tableIzq.AddCell(imagen);

            // Cabecera Titulo
            PdfPTable tableTit = new PdfPTable(2);
            float[] anchosTit = new float[] { 1.50f, 1.0f };
            tableTit.DefaultCell.BorderWidth = 0;
            tableTit.SetWidths(anchosTit);

            tableTit.WidthPercentage = 90;

            //////////////////////////////////////////
            PdfPCell cell = new PdfPCell(new Phrase("RECETA FARMACIA"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.Border = PdfPCell.NO_BORDER;

            tableTit.AddCell(cell);
            Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
            cell = new PdfPCell(new Phrase("N° " + sFOLIO, font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableTit.AddCell(cell);


            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
            // Cabecera CENTRO
            PdfPTable tableCab1 = new PdfPTable(1);
            float[] anchos = new float[] { 0.50f };
            tableCab1.DefaultCell.BorderWidth = 0;
            tableCab1.SetWidths(anchos);

            tableCab1.WidthPercentage = 90;

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("  "));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab1.AddCell(cell);

            //////////////////////////////////////////


            tableCab1.AddCell(tableTit);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("  "));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab1.AddCell(cell);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("PACIENTE"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab1.AddCell(cell);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("AMBULATORIO"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab1.AddCell(cell);


            ///////////////////////////////////////////

            // Hora Hospital

            string Hosp = "Hospital Clínico San Borja Arriaran";

            Chunk Hosp_chuck = new Chunk
                (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
            Paragraph Hosp_par = new Paragraph();
            Hosp_par.Alignment = Element.ALIGN_RIGHT;
            Hosp_par.Add(Hosp_chuck);


            PdfPTable tableHosp = new PdfPTable(3);
            tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
            tableHosp.DefaultCell.BorderWidth = 0;
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;
            tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableHosp.TotalWidth = 540f;
            tableHosp.LockedWidth = true;

            float[] TamColumHora = new float[] { 0.15f, 0.50f, 0.15f };
            tableHosp.SetWidths(TamColumHora);
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;

            tableHosp.AddCell(tableIzq);
            tableHosp.AddCell(tableCab1);
            tableHosp.AddCell(tableDer);
            //tableHosp.AddCell(Hosp_par);

            doc.Add(tableHosp);

            // Salto

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            salta.Add(salto);

            //document.Add(salta);
            //Paragraph p = new Paragraph();
            //p.Alignment = Element.ALIGN_CENTER;

            //Chunk c = new Chunk
            //    (lsTitulo, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

            //p.Add(c);


            //document.Add(p);
            doc.Add(salta);







            ///////////////////////////////////////////////////////////////////////////////////////////

            DateTime fecha_actual = DateTime.Today;
            bool b;
            //string asMsg;


            //asMsg = sFOLIO;

            //EventoTitulos ev = new EventoTitulos(asMsg);
            //writer.PageEvent = ev;

            ////doc.Open();
            //b = doc.AddAuthor("Sistema de Farmacia.");
            //b = doc.AddTitle("Receta Electrónica.");
            //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
            //iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

            //logo.ScaleAbsolute(180, 46);
            //linea.ScaleAbsolute(520, 3);

            //doc.Add(logo);





            // Cabecera
            PdfPTable tableCab = new PdfPTable(4);
            prop_cell(tableCab);

            float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableCab.SetWidths(TamColumCab);
            tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            if (snombreSocial == "")
            {
                tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sNOMBRE, 8, PdfPCell.ALIGN_LEFT, 1));
            }
            else
            {
                tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(snombreSocial + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));

            }

            tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////

            tableCab.AddCell(getCellNegrita("Fecha Emisión: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sF_H_CREACION, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

            doc.Add(tableCab);

            doc.Add(espacio);
            //doc.Add(linea);

            // Detalle Despachos 1
            PdfPTable tableDetCab = new PdfPTable(10);
            prop_cell(tableDetCab);

            float[] TamColumDetCab = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
            tableDetCab.SetWidths(TamColumDetCab);
            tableDetCab.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDetCab.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));


            tableDetCab.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDetCab.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));

            //doc.Add(tableDetCab);

            DataSet registroDet;
            sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES,  ";
            sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
            sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  ";
            sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
            //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
            sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
            sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
            sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA ";
            sSql = sSql + " from M_ART_RECETA D  ";
            sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
            sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
            sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
            sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
            sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
            sSql = sSql + "where D.IDRECETA = " + rw["ID"].ToString() + " AND ISNULL(D.IDESTADO,1) <> 3 ";
            sSql = sSql + " order by   V.CODARTICULO asc";

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();


            DataTable dt = registroDet.Tables[0];

            int i = 1;
            int j = 1;


            // Detalle Despachos 1
            PdfPTable tableDet = new PdfPTable(10);
            prop_cell(tableDet);

            float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
            tableDet.SetWidths(TamColumDet);
            tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("C.E.", 8, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));


            foreach (DataRow row in dt.Rows)
            {

                sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                sDPRESENT = Convert.ToString(row["DPRESENT"]);
                sFORMA = Convert.ToString(row["FORMA"]);
                sDRANGO = Convert.ToString(row["DRANGO"]);
                sDVIA = Convert.ToString(row["DVIA"]);
                sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                sCALCULAR = Convert.ToString(row["CALCULAR"]);
                if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                {
                    sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                }
                else
                {
                    sDIAS_TRATA = "0";
                }
                sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

                if (Convert.ToString(row["DIA"]) == "")
                    sDIA = "";
                else
                    sDIA = " / " + Convert.ToString(row["DIA"]) + "";

                tableDet.AddCell(getCell(i.ToString(), 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCell(sCODARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sNOM_ARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sFORMA, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sDRANGO, 7, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sDVIA, 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCell(sCANTIDAD, 7, PdfPCell.ALIGN_CENTER, 1));


                if (sCALCULAR == "1")
                    tableDet.AddCell(getCell(sDIAS_TRATA + sDIA, 7, PdfPCell.ALIGN_CENTER, 1));
                else
                    tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCell(" ", 7, PdfPCell.ALIGN_CENTER, 1));

                tableDet.AddCell(getCell(sOBSERVACIONES, 7, PdfPCell.ALIGN_LEFT, 1));


                i++;
                doc.Add(tableDet);

                tableDet.Rows.Clear();

                if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
                {
                    doc.NewPage();
                    doc.Add(tableDetCab);
                    j++;
                }
            }


            if (sOBSERVACION != "")
            {
                PdfPTable tableObs = new PdfPTable(1);
                prop_cell(tableObs);
                tableObs.HorizontalAlignment = Element.ALIGN_LEFT;

                tableObs.AddCell(getCellNegrita("Indicaciones:", 8, PdfPCell.ALIGN_LEFT, 0));
                tableObs.AddCell(getCell(sOBSERVACION, 8, PdfPCell.ALIGN_LEFT, 1));
                doc.Add(tableObs);
                doc.Add(espacio);
            }
            else
            {
                doc.Add(espacio);
            }


            doc.Add(espacio);
            doc.Add(espacio);

            Usuarios usr = new Usuarios();
            DataSet aoDs = usr.TraeDatosUsuario(sidusuario);
            ClassReceta rec = new ClassReceta();
            if (aoDs != null && aoDs.Tables.Count > 0 && rec.mfRutNombMedico(rw["ID"].ToString()) == "-1")
            {
                if (aoDs.Tables[0].Rows.Count > 0)
                {
                    PdfPTable tableFirma = new PdfPTable(2);
                    prop_cell(tableFirma);

                    float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                    tableFirma.SetWidths(TamColumFirma);
                    tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                    tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Dr. " + aoDs.Tables[0].Rows[0]["NOMBRE"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    tableFirma.AddCell(getCell("Rut: " + aoDs.Tables[0].Rows[0]["RUT_DV"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                    tableFirma.AddCell(getCell("Especialidad: " + aoDs.Tables[0].Rows[0]["especialidad"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
                    tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                    doc.Add(tableFirma);
                    doc.Add(espacio);
                }

            }
            else
            {
                PdfPTable tableFirma = new PdfPTable(2);
                prop_cell(tableFirma);

                float[] TamColumFirma = new float[] { 2.5f, 2.5f };
                tableFirma.SetWidths(TamColumFirma);
                tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


                tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                tableFirma.AddCell(getCell("Dr. " + rec.mfGetNombMedico(rw["ID"].ToString()), 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                tableFirma.AddCell(getCell("Rut: " + rec.mfRutNombMedico(rw["ID"].ToString()), 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


                tableFirma.AddCell(getCell(" ", 8, PdfPCell.ALIGN_LEFT, 0));
                tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

                doc.Add(tableFirma);
                doc.Add(espacio);
            }

            //Usuarios usr = new Usuarios();
            //DataSet aoDs = usr.TraeDatosUsuario(sidusuario);

            //if (aoDs != null && aoDs.Tables.Count > 0)
            //{
            //    if (aoDs.Tables[0].Rows.Count > 0)
            //    {
            //        PdfPTable tableFirma = new PdfPTable(2);
            //        prop_cell(tableFirma);

            //        float[] TamColumFirma = new float[] { 2.5f, 2.5f };
            //        tableFirma.SetWidths(TamColumFirma);
            //        tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


            //        tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
            //        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

            //        tableFirma.AddCell(getCell("Dr. " + aoDs.Tables[0].Rows[0]["NOMBRE"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
            //        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

            //        tableFirma.AddCell(getCell("Rut: " + aoDs.Tables[0].Rows[0]["RUT_DV"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
            //        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


            //        tableFirma.AddCell(getCell("Especialidad: " + aoDs.Tables[0].Rows[0]["especialidad"].ToString(), 8, PdfPCell.ALIGN_LEFT, 0));
            //        tableFirma.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));

            //        doc.Add(tableFirma);
            //        doc.Add(espacio);
            //    }
            //}

            //doc.Add(espacio);
            //string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

            //ph = new Paragraph(Rayas, font_celdas);
            //doc.Add(ph);
            //string Gerencia = "Información Farmacia";

            //ph = new Paragraph(Gerencia, font_celdas);
            //doc.Add(ph);

            //ph = new Paragraph(Rayas, font_celdas);
            //doc.Add(ph);

            doc.Add(espacio);

            // Farmacia
            PdfPTable tableFarm = new PdfPTable(4);
            prop_cell(tableFarm);

            float[] TamColumFarm = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableFarm.SetWidths(TamColumFarm);
            tableFarm.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            tableFarm.AddCell(getCellNegrita("Diagnótico:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sDIAGNOSTICO, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));





            /////////////////////////////////////////////////////
            tableFarm.AddCell(getCellNegrita("Unidad Solicitante:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("Previsión:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sPREVISION, 8, PdfPCell.ALIGN_LEFT, 1));

            /////////////////////////////////////////////////////
            tableFarm.AddCell(getCellNegrita("Días Tratamiento:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell(sDIAS_TRAT, 8, PdfPCell.ALIGN_LEFT, 1));


            tableFarm.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableFarm.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));


            doc.Add(tableFarm);

            doc.Add(espacio);

            // PIE
            PdfPTable tablePie = new PdfPTable(5);
            prop_cell(tablePie);

            float[] TamColumPie = new float[] { 1.0f, 1.5f, 1.0f, 1.5f, 1.5f };
            tablePie.SetWidths(TamColumPie);
            tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


            /////////////////////////////////////////////////////

            tablePie.AddCell(getCellNegrita("Uso Exclusivo Farmacia", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("RECIBE", 8, PdfPCell.ALIGN_LEFT, 1));


            tablePie.AddCell(getCell("ESCRIBE", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("PREPARA", 8, PdfPCell.ALIGN_LEFT, 1));

            tablePie.AddCell(getCell("ENTREGA", 8, PdfPCell.ALIGN_LEFT, 1));

            doc.Add(tablePie);

            doc.NewPage();


        }



        doc.Close();


        return m.ToArray();


    }

    public byte[] CrearPDFEtiquetaInfect(DataSet loDs, String asCodusuario)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        iTextSharp.text.Rectangle rectangulo = new iTextSharp.text.Rectangle(Convert.ToInt32(modConstantes.mfConstante("LARGO")), Convert.ToInt32(modConstantes.mfConstante("ALTO")));
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        Document doc = new Document(rectangulo, Convert.ToInt32(modConstantes.mfConstante("MARG_IZQ")), 0, Convert.ToInt32(modConstantes.mfConstante("MARG_TOP")), 0);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);



        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);


        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string SFMAXFECHA = string.Empty;

        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");


        // Abre documento.
        doc.Open();

        foreach (DataRow rw in loDs.Tables[0].Rows)
        {
            DataSet registro;
            string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
                //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
                " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FMAXFECHA, " +
                "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
                    "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + rw["ID"].ToString() + ") DIAS_TRAT ,ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                    "FROM M_RECETA R " +
                    "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                    "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                    "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                    "where IDRECETA = " + rw["ID"].ToString();

            con = bd.fnGetConn();
            registro = bd.Fill(con, sSql);
            con.Close();


            sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
            sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
            sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
            sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
            snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
            sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
            sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
            sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
            sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
            sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
            sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
            sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
            sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();
            SFMAXFECHA = registro.Tables[0].Rows[0]["FMAXFECHA"].ToString();

            registro.Dispose();


            //////////////////////////////////////////////////////////////////////////////////////////////



            // Salto

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            //salta.Add(salto);
            ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
            doc.Add(ph);

            doc.Add(espacio);


            //ph = new Paragraph("            Nombre: " + sNOMBRE, font_desc);
            //doc.Add(ph);
            //ph = new Paragraph("            Rut: " + sRUT, font_desc);
            //doc.Add(ph);
            //doc.Add(espacio);
            //ph = new Paragraph("            Preparado: " + sF_H_CREACION, font_desc);
            //doc.Add(ph);
            //ph = new Paragraph("            Fecha Retiro: " , font_desc);
            //doc.Add(ph);

            if (snombreSocial == "")
            {
                ph = new Paragraph(sNOMBRE, font_desc);
            }
            else
            {
                ph = new Paragraph(snombreSocial + sAPELLIDOS, font_desc);

            }


            doc.Add(ph);
            ph = new Paragraph(sRUT, font_desc);
            doc.Add(ph);
            doc.Add(espacio);
            ph = new Paragraph("Folio: " + sFOLIO, font_desc);
            doc.Add(ph);
            ph = new Paragraph("Fecha Despacho: " + sF_H_CREACION, font_desc);
            doc.Add(ph);
            ph = new Paragraph("Fecha Próximo Retiro: " + SFMAXFECHA, font_desc);
            doc.Add(ph);

            doc.NewPage();


            DataSet registroDet;
            sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES,  ";
            sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
            sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  ";
            sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
            //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
            sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
            sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
            sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA ";
            sSql = sSql + " from M_ART_RECETA D  ";
            sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
            sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
            sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
            sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
            sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
            sSql = sSql + "where D.IDRECETA = " + rw["ID"].ToString() + " AND ISNULL(D.IDESTADO,1) <> 3 ";
            sSql = sSql + " order by   V.CODARTICULO asc";

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();
            DataTable dt = registroDet.Tables[0];


            foreach (DataRow row in dt.Rows)
            {

                sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                sDPRESENT = Convert.ToString(row["DPRESENT"]);
                sFORMA = Convert.ToString(row["FORMA"]);

                sDRANGO = Convert.ToString(row["DRANGO"]);
                sDVIA = Convert.ToString(row["DVIA"]);
                sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                sCALCULAR = Convert.ToString(row["CALCULAR"]);
                if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                {
                    sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                }
                else
                {
                    sDIAS_TRATA = "0";
                }
                sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

                if (Convert.ToString(row["DIA"]) == "")
                    sDIA = "";
                else
                    sDIA = " / " + Convert.ToString(row["DIA"]) + "";
                ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
                doc.Add(ph);
                if (snombreSocial == "")
                {
                    ph = new Paragraph(sNOMBRE, font_normal);
                }
                else
                {
                    ph = new Paragraph(snombreSocial + sAPELLIDOS, font_normal);

                }
                doc.Add(ph);
                ph = new Paragraph(sRUT, font_celdas);
                doc.Add(ph);

                doc.Add(espacio);

                ph = new Paragraph("Folio: " + sFOLIO + " Código: " + sCODARTICULO, font_celdas);
                doc.Add(ph);
                //ph = new Paragraph(sCODARTICULO, font_celdas);
                //doc.Add(ph);

                ph = new Paragraph(sNOM_ARTICULO, font_desc);
                doc.Add(ph);

                //ph = new Paragraph(sDPRESENT, font_desc);
                //doc.Add(ph);

                ph = new Paragraph(sDRANGO, font_desc);
                doc.Add(ph);


                ph = new Paragraph("Vía: " + sDVIA, font_desc);
                doc.Add(ph);

                ph = new Paragraph("Total: ", font_desc);
                doc.Add(ph);
                //ph = new Paragraph("Indicado por: " + sCANTIDAD + " días.", font_desc);
                //doc.Add(ph);


                //ph = new Paragraph(sOBSERVACIONES, font_desc);
                //doc.Add(ph);
                doc.NewPage();

            }





        }



        doc.Close();


        return m.ToArray();


    }

    public byte[] CrearPDFEtiquetaInfectDisp(DataSet loDs, String asCodusuario)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        iTextSharp.text.Rectangle rectangulo = new iTextSharp.text.Rectangle(Convert.ToInt32(modConstantes.mfConstante("LARGO")), Convert.ToInt32(modConstantes.mfConstante("ALTO")));
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        Document doc = new Document(rectangulo, Convert.ToInt32(modConstantes.mfConstante("MARG_IZQ")), 0, Convert.ToInt32(modConstantes.mfConstante("MARG_TOP")), 0);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);



        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);


        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string SFMAXFECHA = string.Empty;

        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");


        // Abre documento.
        doc.Open();

        foreach (DataRow rw in loDs.Tables[0].Rows)
        {
            DataSet registro;
            string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
                //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
                " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FMAXFECHA, " +
                "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
                    "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + rw["ID"].ToString() + ") DIAS_TRAT ,ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                    "FROM M_RECETA R " +
                    "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                    "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                    "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                    "where IDRECETA = " + rw["ID"].ToString();

            con = bd.fnGetConn();
            registro = bd.Fill(con, sSql);
            con.Close();


            sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
            sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
            sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
            sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
            snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
            sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
            sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
            sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
            sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
            sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
            sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
            sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
            sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();
            SFMAXFECHA = registro.Tables[0].Rows[0]["FMAXFECHA"].ToString();

            registro.Dispose();


            //////////////////////////////////////////////////////////////////////////////////////////////



            // Salto

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            //salta.Add(salto);
            ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
            doc.Add(ph);

            doc.Add(espacio);


            //ph = new Paragraph("            Nombre: " + sNOMBRE, font_desc);
            //doc.Add(ph);
            //ph = new Paragraph("            Rut: " + sRUT, font_desc);
            //doc.Add(ph);
            //doc.Add(espacio);
            //ph = new Paragraph("            Preparado: " + sF_H_CREACION, font_desc);
            //doc.Add(ph);
            //ph = new Paragraph("            Fecha Retiro: " , font_desc);
            //doc.Add(ph);

            if (snombreSocial == "")
            {
                ph = new Paragraph(sNOMBRE, font_desc);
            }
            else
            {
                ph = new Paragraph(snombreSocial + sAPELLIDOS, font_desc);

            }


            doc.Add(ph);
            ph = new Paragraph(sRUT, font_desc);
            doc.Add(ph);
            doc.Add(espacio);
            ph = new Paragraph("Folio: " + sFOLIO, font_desc);
            doc.Add(ph);
            ph = new Paragraph("Fecha Despacho: " + sF_H_CREACION, font_desc);
            doc.Add(ph);
            ph = new Paragraph("Fecha Próximo Retiro: " + SFMAXFECHA, font_desc);
            doc.Add(ph);

            doc.NewPage();


            DataSet registroDet;
            sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES,  ";
            sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
            sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  ";
            sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
            //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
            sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
            sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
            sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA ";
            sSql = sSql + " from M_ART_RECETA D  ";
            sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
            sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
            sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
            sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
            sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
            sSql = sSql + "where D.IDRECETA = " + rw["ID"].ToString() + " AND ISNULL(D.IDESTADO,1) <> 3 and cant_desp_req > 0 ";
            sSql = sSql + " order by   V.CODARTICULO asc";

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();
            DataTable dt = registroDet.Tables[0];


            foreach (DataRow row in dt.Rows)
            {

                sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                sDPRESENT = Convert.ToString(row["DPRESENT"]);
                sFORMA = Convert.ToString(row["FORMA"]);

                sDRANGO = Convert.ToString(row["DRANGO"]);
                sDVIA = Convert.ToString(row["DVIA"]);
                sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                sCALCULAR = Convert.ToString(row["CALCULAR"]);
                if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                {
                    sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                }
                else
                {
                    sDIAS_TRATA = "0";
                }
                sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

                if (Convert.ToString(row["DIA"]) == "")
                    sDIA = "";
                else
                    sDIA = " / " + Convert.ToString(row["DIA"]) + "";
                ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
                doc.Add(ph);
                if (snombreSocial == "")
                {
                    ph = new Paragraph(sNOMBRE, font_normal);
                }
                else
                {
                    ph = new Paragraph(snombreSocial + sAPELLIDOS, font_normal);

                }
                doc.Add(ph);
                ph = new Paragraph(sRUT, font_celdas);
                doc.Add(ph);

                doc.Add(espacio);

                ph = new Paragraph("Folio: " + sFOLIO + " Código: " + sCODARTICULO, font_celdas);
                doc.Add(ph);
                //ph = new Paragraph(sCODARTICULO, font_celdas);
                //doc.Add(ph);

                ph = new Paragraph(sNOM_ARTICULO, font_desc);
                doc.Add(ph);

                //ph = new Paragraph(sDPRESENT, font_desc);
                //doc.Add(ph);

                ph = new Paragraph(sDRANGO, font_desc);
                doc.Add(ph);


                ph = new Paragraph("Vía: " + sDVIA, font_desc);
                doc.Add(ph);

                ph = new Paragraph("Total: ", font_desc);
                doc.Add(ph);
                //ph = new Paragraph("Indicado por: " + sCANTIDAD + " días.", font_desc);
                //doc.Add(ph);


                //ph = new Paragraph(sOBSERVACIONES, font_desc);
                //doc.Add(ph);
                doc.NewPage();

            }





        }



        doc.Close();


        return m.ToArray();


    }

    public byte[] CrearPDFEtiquetas(string Identificador)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        iTextSharp.text.Rectangle rectangulo = new iTextSharp.text.Rectangle(Convert.ToInt32(modConstantes.mfConstante("LARGO")), Convert.ToInt32(modConstantes.mfConstante("ALTO")));
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        Document doc = new Document(rectangulo, Convert.ToInt32(modConstantes.mfConstante("MARG_IZQ")), 0, Convert.ToInt32(modConstantes.mfConstante("MARG_TOP")), 0);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);



        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);


        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string SRANGO_DISP = string.Empty;
        string SPOSOLOGIA_DISP = string.Empty;
        string Scant_desp_req = string.Empty;


        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");


        // Abre documento.
        doc.Open();


        DataSet registro;
        string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
            //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
            " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, " +
            "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
                "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + Identificador + ") DIAS_TRAT ,ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                "FROM M_RECETA R " +
                "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                "where IDRECETA = " + Identificador;

        con = bd.fnGetConn();
        registro = bd.Fill(con, sSql);
        con.Close();


        sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
        sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
        sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
        sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
        snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
        sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
        sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
        sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
        sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
        sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
        sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
        sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
        sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();


        registro.Dispose();


        //////////////////////////////////////////////////////////////////////////////////////////////



        // Salto

        Chunk salto = new Chunk
        ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
        Paragraph salta = new Paragraph();
        salta.Alignment = Element.ALIGN_LEFT;
        //salta.Add(salto);
        ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
        doc.Add(ph);

        doc.Add(espacio);


        //ph = new Paragraph("            Nombre: " + sNOMBRE, font_desc);
        //doc.Add(ph);
        //ph = new Paragraph("            Rut: " + sRUT, font_desc);
        //doc.Add(ph);
        //doc.Add(espacio);
        //ph = new Paragraph("            Preparado: " + sF_H_CREACION, font_desc);
        //doc.Add(ph);
        //ph = new Paragraph("            Fecha Retiro: " , font_desc);
        //doc.Add(ph);

        if (snombreSocial == "")
        {
            ph = new Paragraph(sNOMBRE, font_desc);
        }
        else
        {
            ph = new Paragraph(snombreSocial + sAPELLIDOS, font_desc);

        }


        doc.Add(ph);
        ph = new Paragraph(sRUT, font_desc);
        doc.Add(ph);
        doc.Add(espacio);
        ph = new Paragraph("Folio: " + sFOLIO, font_desc);
        doc.Add(ph);
        ph = new Paragraph("Fecha Despacho: " + sF_H_CREACION, font_desc);
        doc.Add(ph);
        ph = new Paragraph("Fecha Próximo Retiro: ", font_desc);
        doc.Add(ph);

        doc.NewPage();


        DataSet registroDet;
        //sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES, convert(decimal(20,0),ISNULL(D.cant_desp_req,0)) cant_desp_req,   ";
        //sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
        //sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  convert(decimal(20,0),D.POSOLOGIA_DISP) POSOLOGIA_DISP, convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) RANGO_DISP, ";
        //sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
        ////sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
        //sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
        //sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ";
        //sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA ";
        //sSql = sSql + " from M_ART_RECETA D  ";
        //sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
        //sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
        //sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
        //sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
        //sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
        //sSql = sSql + "where D.IDRECETA = " + Identificador + " AND ISNULL(D.IDESTADO,1) <> 3 ";
        //sSql = sSql + " order by   V.CODARTICULO asc";

        string lsSql = "select MAX(IDDESPACHO) IDDESPACHO from " + modConstantes.gsDbPer + "M_DESPACHOS where idreqaut = " + Identificador;
        DataSet registro1;
        con = bd.fnGetConn();
        registro1 = bd.Fill(con, lsSql);

        sSql = "SELECT V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBS_FARM OBSERVACIONES,  " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT,ISNULL(V.UN_MED,'-') FORMA,  " +
                "convert(decimal(20,0),(ISNULL(DD.CANT_DESP,0) - ISNULL(DD.CANT_DEV,0))) CANT_DESP, " +
                " ISNULL(ACUM_RANGO_D,0) sDIAS_TRATA,   " +
                "CONVERT(DECIMAL(20,0),(ISNULL(CANT_PEND_D,0) + ISNULL(DD.CANT_DEV,0))) PENDIENTE,   " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0)  " +
                "then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0)))  " +
                "else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +   ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO,  " +
                "CONVERT(VARCHAR(10),DD.FDESPACHO_D,103) FDESPACHO, ISNULL(VIA.DESCRIPCION,'-') DVIA, " +
                "convert(decimal(20,0),ISNULL(DD.RANGO_DISP_D,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA   " +
                "FROM " + modConstantes.gsDbPer + "M_DESPACHOS DE " +
                "INNER JOIN " + modConstantes.gsDbPer + "M_DETDESP DD ON DD.IDDESPACHO = DE.IDDESPACHO " +
                "INNER JOIN M_ART_RECETA D ON D.IDARTRECETA = DD.IDARTRECETA " +
                "INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO    " +
                "left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA    " +
                "left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO    " +
               " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO    " +
                "LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  " +
                "WHERE DE.IDDESPACHO = " + registro1.Tables[0].Rows[0]["IDDESPACHO"].ToString() + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3  " +
                "order by   V.CODARTICULO asc";


        con = bd.fnGetConn();
        registroDet = bd.Fill(con, sSql);
        con.Close();
        DataTable dt = registroDet.Tables[0];


        foreach (DataRow row in dt.Rows)
        {

            sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
            sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
            sDPRESENT = Convert.ToString(row["DPRESENT"]);
            sFORMA = Convert.ToString(row["FORMA"]);

            sDRANGO = Convert.ToString(row["DRANGO"]);
            sDVIA = Convert.ToString(row["DVIA"]);
            sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
            //sCALCULAR = Convert.ToString(row["CALCULAR"]);
            //SRANGO_DISP = Convert.ToString(row["RANGO_DISP"]);
            //SPOSOLOGIA_DISP = Convert.ToString(row["POSOLOGIA_DISP"]);
            Scant_desp_req = Convert.ToString(row["CANT_DESP"]);


            if (Convert.ToString(row["sDIAS_TRATA"]) != null)
            {
                sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
            }
            else
            {
                sDIAS_TRATA = "0";
            }
            sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

            if (Convert.ToString(row["DIA"]) == "")
                sDIA = "";
            else
                sDIA = " / " + Convert.ToString(row["DIA"]) + "";
            ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
            doc.Add(ph);

            if (snombreSocial == "")
            {
                ph = new Paragraph(sNOMBRE, font_normal);
            }
            else
            {
                ph = new Paragraph(snombreSocial + sAPELLIDOS, font_normal);

            }
            doc.Add(ph);

            ph = new Paragraph(sRUT, font_celdas);
            doc.Add(ph);

            doc.Add(espacio);

            ph = new Paragraph("Folio: " + sFOLIO + " Código: " + sCODARTICULO, font_celdas);
            doc.Add(ph);
            //ph = new Paragraph(sCODARTICULO, font_celdas);
            //doc.Add(ph);

            ph = new Paragraph(sNOM_ARTICULO, font_desc);
            doc.Add(ph);

            //ph = new Paragraph(sDPRESENT, font_desc);
            //doc.Add(ph);

            ph = new Paragraph(sDRANGO, font_desc);
            doc.Add(ph);


            ph = new Paragraph("Vía: " + sDVIA, font_desc);
            doc.Add(ph);

            ph = new Paragraph("Total: " + Scant_desp_req, font_desc);
            doc.Add(ph);
            //ph = new Paragraph("Indicado por: " + sCANTIDAD + " días.", font_desc);
            //doc.Add(ph);


            //ph = new Paragraph(sOBSERVACIONES, font_desc);
            //doc.Add(ph);
            doc.NewPage();

        }


        doc.Close();


        return m.ToArray();


    }


    public byte[] CrearPDFEtiquetaRut(DataSet loDs, String asCodusuario)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        iTextSharp.text.Rectangle rectangulo = new iTextSharp.text.Rectangle(Convert.ToInt32(modConstantes.mfConstante("LARGO")), Convert.ToInt32(modConstantes.mfConstante("ALTO")));
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        Document doc = new Document(rectangulo, Convert.ToInt32(modConstantes.mfConstante("MARG_IZQ")), 0, Convert.ToInt32(modConstantes.mfConstante("MARG_TOP")), 0);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);



        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);


        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string SRANGO_DISP = string.Empty;
        string SPOSOLOGIA_DISP = string.Empty;
        string Scant_desp_req = string.Empty;
        string SFMAXFECHA = string.Empty;


        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");


        // Abre documento.
        doc.Open();

        foreach (DataRow rw in loDs.Tables[0].Rows)
        {
            DataSet registro;
            string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, OBSERVACION, '35' EDAD, " +
                //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
                " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, " +
                "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FMAXFECHA, " +
                    "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + rw["IDRECETA"].ToString() + ") DIAS_TRAT ,ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                    "FROM M_RECETA R " +
                    "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                    "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                    "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                    "where IDRECETA = " + rw["IDRECETA"].ToString();

            con = bd.fnGetConn();
            registro = bd.Fill(con, sSql);
            con.Close();


            sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
            sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
            sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
            sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
            snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
            sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
            sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
            sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
            sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
            sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
            sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
            sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
            sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();
            SFMAXFECHA = registro.Tables[0].Rows[0]["FMAXFECHA"].ToString();

            registro.Dispose();


            //////////////////////////////////////////////////////////////////////////////////////////////



            // Salto

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            //salta.Add(salto);
            ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
            doc.Add(ph);

            doc.Add(espacio);


            //ph = new Paragraph("            Nombre: " + sNOMBRE, font_desc);
            //doc.Add(ph);
            //ph = new Paragraph("            Rut: " + sRUT, font_desc);
            //doc.Add(ph);
            //doc.Add(espacio);
            //ph = new Paragraph("            Preparado: " + sF_H_CREACION, font_desc);
            //doc.Add(ph);
            //ph = new Paragraph("            Fecha Retiro: " , font_desc);
            //doc.Add(ph);

            if (snombreSocial == "")
            {
                ph = new Paragraph(sNOMBRE, font_desc);
            }
            else
            {
                ph = new Paragraph(snombreSocial + sAPELLIDOS, font_desc);

            }


            doc.Add(ph);
            ph = new Paragraph(sRUT, font_desc);
            doc.Add(ph);
            doc.Add(espacio);
            ph = new Paragraph("Folio: " + sFOLIO, font_desc);
            doc.Add(ph);
            ph = new Paragraph("Fecha Despacho: " + sF_H_CREACION, font_desc);
            doc.Add(ph);
            ph = new Paragraph("Fecha Próximo Retiro: " + SFMAXFECHA, font_desc);
            doc.Add(ph);

            doc.NewPage();


            DataSet registroDet;
            sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBSERVACIONES,  ";
            sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
            sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  convert(decimal(20,0),D.POSOLOGIA_DISP) POSOLOGIA_DISP, convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) RANGO_DISP, ";
            sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
            //sSql = sSql + " CONVERT(VARCHAR(10), ISNULL(D.CANTIDAD,0)) + ' ' + ISNULL(V.UN_MED,'-')+ '(S)'   + ' CADA ' + CONVERT(VARCHAR(10),ISNULL(D.RANGO, 0)) +' ' + ISNULL(PE.DESCRIPCION, '-') + '(S)' DRANGO, ";
            sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
            sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, ISNULL(cant_desp_req,0) cant_desp_req,";
            sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,convert(decimal(20,0),ISNULL(D.RANGO,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA ";
            sSql = sSql + " from M_ART_RECETA D  ";
            sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
            sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
            sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
            sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
            sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
            sSql = sSql + "where D.IDRECETA = " + rw["IDRECETA"].ToString() + " AND ISNULL(D.IDESTADO,1) <> 3 AND cant_desp_req > 0 ";
            sSql = sSql + " order by   V.CODARTICULO asc";

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();
            DataTable dt = registroDet.Tables[0];


            foreach (DataRow row in dt.Rows)
            {

                sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                sDPRESENT = Convert.ToString(row["DPRESENT"]);
                sFORMA = Convert.ToString(row["FORMA"]);

                sDRANGO = Convert.ToString(row["DRANGO"]);
                sDVIA = Convert.ToString(row["DVIA"]);
                sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                sCALCULAR = Convert.ToString(row["CALCULAR"]);
                SRANGO_DISP = Convert.ToString(row["RANGO_DISP"]);
                SPOSOLOGIA_DISP = Convert.ToString(row["POSOLOGIA_DISP"]);
                Scant_desp_req = Convert.ToString(row["cant_desp_req"]);

                if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                {
                    sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                }
                else
                {
                    sDIAS_TRATA = "0";
                }
                sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

                if (Convert.ToString(row["DIA"]) == "")
                    sDIA = "";
                else
                    sDIA = " / " + Convert.ToString(row["DIA"]) + "";
                ph = new Paragraph("Farmacia - Hospital Clinico San Borja Arriaran.", font_celdas);
                doc.Add(ph);

                if (snombreSocial == "")
                {
                    ph = new Paragraph(sNOMBRE, font_normal);
                }
                else
                {
                    ph = new Paragraph(snombreSocial + sAPELLIDOS, font_normal);

                }
                doc.Add(ph);



                ph = new Paragraph(sRUT, font_celdas);
                doc.Add(ph);

                doc.Add(espacio);

                ph = new Paragraph("Folio: " + sFOLIO + " Código: " + sCODARTICULO, font_celdas);
                doc.Add(ph);
                //ph = new Paragraph(sCODARTICULO, font_celdas);
                //doc.Add(ph);

                ph = new Paragraph(sNOM_ARTICULO, font_desc);
                doc.Add(ph);

                //ph = new Paragraph(sDPRESENT, font_desc);
                //doc.Add(ph);

                ph = new Paragraph(sDRANGO, font_desc);
                doc.Add(ph);


                ph = new Paragraph("Vía: " + sDVIA, font_desc);
                doc.Add(ph);

                ph = new Paragraph("Total: " + Scant_desp_req, font_desc);
                doc.Add(ph);
                //ph = new Paragraph("Indicado por: " + sCANTIDAD + " días.", font_desc);
                //doc.Add(ph);


                //ph = new Paragraph(sOBSERVACIONES, font_desc);
                //doc.Add(ph);
                doc.NewPage();

            }





        }



        doc.Close();


        return m.ToArray();


    }
    public byte[] CrearPDFComprobantesDesp(string asIdentificador)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        //Document doc = new Document(PageSize.LETTER, miMarL, miMarR, (miMarTB * 3), miMarTB * 1);
        Document doc = new Document(PageSize.LETTER, miMarL, miMarR, (10), miMarTB * 1);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);

        // Arma tabla de Encabezado.
        Single[] colDtX = new Single[] { 150, 260, 120 };
        PdfPTable loTbX = new PdfPTable(3);

        loTbX.DefaultCell.Border = PdfPCell.NO_BORDER;
        String lsImCln = String.Empty;



        //writer.PageEvent = loEv;
        writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, "ownerpass", PdfWriter.AllowPrinting);
        // Agrega metadata al documento.
        doc.AddCreator("walter.pizarro@minsal.gob.cl");
        doc.AddAuthor("Wpizarror®");

        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.BOLD);
        Font font_Fecha = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL);
        Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);

        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string scant_desp_req = string.Empty;
        string sFDESPACHO = string.Empty;
        string sPENDIENTE = string.Empty;
        //string sFMAXFECHA = string.Empty;

        string sTipoAdq = string.Empty;
        string sNombAdq = string.Empty;
        string sRutAdq = string.Empty;
        string sFonoAdq = string.Empty;
        string sNUM_REC_MANUAL = string.Empty;


        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");

        string lsSql = "select idreqaut,CONVERT(VARCHAR(10), F_H_CREACION, 103) F_H_CREACION from " + modConstantes.gsDbPer + "M_DESPACHOS where IDDESPACHO = " + asIdentificador;
        DataSet registro1;
        con = bd.fnGetConn();
        registro1 = bd.Fill(con, lsSql);


        lsSql = " select MAX(FDESPACHO_D)  from " + modConstantes.gsDbPer + "M_DETDESP  where IDDESPACHO = " + asIdentificador;


        string FechaDespM = bd.ExecuteScalar(con, lsSql);
        con.Close();

        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString(registro1.Tables[0].Rows[0]["F_H_CREACION"].ToString(), CultureInfo.CreateSpecificCulture("fr-FR"));


        // Abre documento.
        doc.Open();

        DataSet registro;
        string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, " +
            "OBSERVACION, '35' EDAD, isnull(TipoAdq,1) TipoAdq, isnull(NombAdq,'') NombAdq, isnull(RutAdq,'') RutAdq, isnull(FonoAdq,'') FonoAdq, " +
            //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
            " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, ISNULL(NUM_REC_MANUAL,'')  NUM_REC_MANUAL, " +
            "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, " +
            "'' FMAXFECHA, " +
                "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + registro1.Tables[0].Rows[0]["idreqaut"].ToString() + ") DIAS_TRAT , " +
                "ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                "FROM M_RECETA R " +
                "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                "where IDRECETA = " + registro1.Tables[0].Rows[0]["idreqaut"].ToString();

        con = bd.fnGetConn();
        registro = bd.Fill(con, sSql);
        con.Close();


        sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
        sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
        sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
        sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
        snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
        sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
        sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
        sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
        sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
        sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
        sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
        sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
        sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();
        //sFMAXFECHA = registro.Tables[0].Rows[0]["FMAXFECHA"].ToString();

        sTipoAdq = registro.Tables[0].Rows[0]["TipoAdq"].ToString();
        sNombAdq = registro.Tables[0].Rows[0]["NombAdq"].ToString();
        sRutAdq = registro.Tables[0].Rows[0]["RutAdq"].ToString();
        sFonoAdq = registro.Tables[0].Rows[0]["FonoAdq"].ToString();
        sNUM_REC_MANUAL = registro.Tables[0].Rows[0]["NUM_REC_MANUAL"].ToString();


        registro.Dispose();


        //////////////////////////////////////////////////////////////////////////////////////////////


        string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");

        //Application.StartupPath;
        string rutaLogo = Application.UserAppDataPath;
        string rutaLogo2 = Application.LocalUserAppDataPath;
        string rutalogo = ruta;

        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
        iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
        imagen.BorderWidth = 0;
        imagen.Alignment = Element.ALIGN_LEFT;
        float percentage = 0.0f;
        percentage = 70 / imagen.Width;
        imagen.ScalePercent(percentage * 100);
        //imagen.ScaleAbsolute(50f, 50f);

        //document.Add(imagen);

        // Cabecera Derecha
        PdfPTable tableDer = new PdfPTable(1);
        float[] anchosDer = new float[] { 0.50f };
        tableDer.DefaultCell.BorderWidth = 0;
        tableDer.SetWidths(anchosDer);

        tableDer.WidthPercentage = 90;

        //////////////////////////////////////////

        //tableDer.AddCell(imagen2);
        tableDer.AddCell("");


        // Cabecera Izquierda
        PdfPTable tableIzq = new PdfPTable(1);
        float[] anchosIzq = new float[] { 0.50f };
        tableIzq.DefaultCell.BorderWidth = 0;
        tableIzq.SetWidths(anchosIzq);

        tableIzq.WidthPercentage = 90;

        //////////////////////////////////////////

        //tableIzq.AddCell(imagen);
        tableIzq.AddCell("");

        // Cabecera Titulo
        PdfPTable tableTit = new PdfPTable(1);
        float[] anchosTit = new float[] { 1.50f };
        tableTit.DefaultCell.BorderWidth = 1;
        tableTit.SetWidths(anchosTit);

        tableTit.WidthPercentage = 90;

        //////////////////////////////////////////
        PdfPCell cell = new PdfPCell(new Phrase("COMPROBANTE ENTREGA DE MEDICAMENTOS", font_titulo));
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.Border = PdfPCell.NO_BORDER;

        tableTit.AddCell(cell);

        //Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
        //cell = new PdfPCell(new Phrase("" + sFOLIO, font));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.Border = PdfPCell.NO_BORDER;
        //tableTit.AddCell(cell);


        //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
        // Cabecera CENTRO
        PdfPTable tableCab1 = new PdfPTable(1);
        float[] anchos = new float[] { 0.50f };
        tableCab1.DefaultCell.BorderWidth = 0;
        tableCab1.SetWidths(anchos);

        tableCab1.WidthPercentage = 90;

        //////////////////////////////////////////

        cell = new PdfPCell(new Phrase(" Hospital Clinico San Borja Arriaran "));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.Border = PdfPCell.NO_BORDER;
        tableCab1.AddCell(cell);

        //////////////////////////////////////////


        tableCab1.AddCell(tableTit);

        //////////////////////////////////////////

        //cell = new PdfPCell(new Phrase("  "));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.Border = PdfPCell.NO_BORDER;
        //tableCab1.AddCell(cell);

        ////////////////////////////////////////////

        cell = new PdfPCell(new Phrase("(No válido como receta)", font_desc));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.Border = PdfPCell.NO_BORDER;
        tableCab1.AddCell(cell);

        ////////////////////////////////////////////

        //cell = new PdfPCell(new Phrase("AMBULATORIO"));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.Border = PdfPCell.NO_BORDER;
        //tableCab1.AddCell(cell);


        ///////////////////////////////////////////

        // Hora Hospital

        string Hosp = "Hospital Clínico San Borja Arriaran";

        Chunk Hosp_chuck = new Chunk
            (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
        Paragraph Hosp_par = new Paragraph();
        Hosp_par.Alignment = Element.ALIGN_RIGHT;
        Hosp_par.Add(Hosp_chuck);


        PdfPTable tableHosp = new PdfPTable(3);
        tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
        tableHosp.DefaultCell.BorderWidth = 0;
        tableHosp.HorizontalAlignment = Element.ALIGN_CENTER;
        tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        tableHosp.TotalWidth = 540f;
        tableHosp.LockedWidth = true;

        float[] TamColumHora = new float[] { 0.1f, 5.50f, 0.1f };
        tableHosp.SetWidths(TamColumHora);
        tableHosp.HorizontalAlignment = Element.ALIGN_CENTER;

        tableHosp.AddCell(tableIzq);
        tableHosp.AddCell(tableCab1);
        tableHosp.AddCell(tableDer);
        //tableHosp.AddCell(Hosp_par);

        doc.Add(tableHosp);

        // Salto

        Chunk salto = new Chunk
        ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
        Paragraph salta = new Paragraph();
        salta.Alignment = Element.ALIGN_LEFT;
        salta.Add(salto);

        //document.Add(salta);
        //Paragraph p = new Paragraph();
        //p.Alignment = Element.ALIGN_CENTER;

        //Chunk c = new Chunk
        //    (lsTitulo, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

        //p.Add(c);


        //document.Add(p);
        doc.Add(salta);







        ///////////////////////////////////////////////////////////////////////////////////////////

        DateTime fecha_actual = DateTime.Today;
        bool b;
        //string asMsg;


        //asMsg = sFOLIO;

        //EventoTitulos ev = new EventoTitulos(asMsg);
        //writer.PageEvent = ev;

        ////doc.Open();
        //b = doc.AddAuthor("Sistema de Farmacia.");
        //b = doc.AddTitle("Receta Electrónica.");
        //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
        //iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

        //logo.ScaleAbsolute(180, 46);
        //linea.ScaleAbsolute(520, 3);

        //doc.Add(logo);

        //string FechaDesp = "Fecha Despacho: " + DateTime.Now.ToString("dd/MM/yyyy"); ;
        string FechaDesp = "Fecha Despacho: " + shoy;

        ph = new Paragraph(FechaDesp, font_celdas);
        doc.Add(ph);

        doc.Add(espacio);

        // Cabecera
        PdfPTable tableCab = new PdfPTable(4);
        prop_cell(tableCab);

        float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
        tableCab.SetWidths(TamColumCab);
        tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


        /////////////////////////////////////////////////////

        if (snombreSocial == "")
        {
            tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sNOMBRE, 8, PdfPCell.ALIGN_LEFT, 1));
        }
        else
        {
            tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(snombreSocial + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));

        }

        tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
        tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

        /////////////////////////////////////////////////////

        tableCab.AddCell(getCellNegrita("Servicio: ", 8, PdfPCell.ALIGN_LEFT, 1));
        tableCab.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));

        tableCab.AddCell(getCellNegrita("Folio Receta:", 8, PdfPCell.ALIGN_LEFT, 1));
        tableCab.AddCell(getCell(sFOLIO, 8, PdfPCell.ALIGN_LEFT, 1));

        if (sNUM_REC_MANUAL != "")
        {
            tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Folio Manual:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sNUM_REC_MANUAL, 8, PdfPCell.ALIGN_LEFT, 1));
        }

        doc.Add(tableCab);

        doc.Add(espacio);


        DataSet registroDet;
        sSql = "SELECT V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBS_FARM OBSERVACIONES,  " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT,ISNULL(V.UN_MED,'-') FORMA,  " +
                "convert(decimal(20,0),(ISNULL(DD.CANT_DESP,0) - ISNULL(DD.CANT_DEV,0))) CANT_DESP, " +
                " ISNULL(ACUM_RANGO_D,0) sDIAS_TRATA,   " +
                "CONVERT(DECIMAL(20,0),(ISNULL(CANT_PEND_D,0) + ISNULL(DD.CANT_DEV,0))) PENDIENTE,   " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0)  " +
                "then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0)))  " +
                "else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +   ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO,  " +
                "CONVERT(VARCHAR(10),DD.FDESPACHO_D,103) FDESPACHO, ISNULL(VIA.DESCRIPCION,'-') DVIA, " +
                "convert(decimal(20,0),ISNULL(DD.RANGO_DISP_D,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA   " +
                "FROM " + modConstantes.gsDbPer + "M_DESPACHOS DE " +
                "INNER JOIN " + modConstantes.gsDbPer + "M_DETDESP DD ON DD.IDDESPACHO = DE.IDDESPACHO " +
                "INNER JOIN M_ART_RECETA D ON D.IDARTRECETA = DD.IDARTRECETA " +
                "INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO    " +
                "left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA    " +
                "left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO    " +
               " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO    " +
                "LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  " +
                "WHERE DE.IDDESPACHO = " + asIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3  " +
                "order by   V.CODARTICULO asc";

        con = bd.fnGetConn();
        registroDet = bd.Fill(con, sSql);
        con.Close();


        DataTable dt = registroDet.Tables[0];

        int i = 1;
        int j = 1;


        // Detalle Despachos 1
        PdfPTable tableDet = new PdfPTable(10);
        prop_cell(tableDet);
        float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 0.7f, 1.5f, 1.2f, 1.6f, 1.3f, 1.7f };
        //float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
        tableDet.SetWidths(TamColumDet);
        tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

        tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Vía Admin.", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Cantidad Pendiente", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Cantidad Despachada", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Proximo Despacho", 8, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));


        foreach (DataRow row in dt.Rows)
        {

            sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
            sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
            sDPRESENT = Convert.ToString(row["DPRESENT"]);
            sFORMA = Convert.ToString(row["FORMA"]);
            sDRANGO = Convert.ToString(row["DRANGO"]);
            sDVIA = Convert.ToString(row["DVIA"]);
            sCANTIDAD = Convert.ToString(row["CANTIDAD"]);

            scant_desp_req = Convert.ToString(row["CANT_DESP"]);
            sFDESPACHO = Convert.ToString(row["FDESPACHO"]);
            sPENDIENTE = Convert.ToString(row["PENDIENTE"]);

            if (Convert.ToString(row["sDIAS_TRATA"]) != null)
            {
                sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
            }
            else
            {
                sDIAS_TRATA = "0";
            }
            sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

            if (Convert.ToString(row["DIA"]) == "")
                sDIA = "";
            else
                sDIA = " / " + Convert.ToString(row["DIA"]) + "";

            tableDet.AddCell(getCell(i.ToString(), 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(sCODARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCell(sNOM_ARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
            //tableDet.AddCell(getCell(sFORMA, 7, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCell(sDRANGO, 7, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCell(sDVIA, 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(sCANTIDAD, 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(sPENDIENTE, 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(scant_desp_req, 7, PdfPCell.ALIGN_CENTER, 1));


            //if (sCALCULAR == "1")
            //    tableDet.AddCell(getCell(sDIAS_TRATA + sDIA, 7, PdfPCell.ALIGN_CENTER, 1));
            //else
            //    tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCell(sFDESPACHO, 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCell(sOBSERVACIONES, 7, PdfPCell.ALIGN_LEFT, 1));


            i++;
            doc.Add(tableDet);

            tableDet.Rows.Clear();

            if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
            {
                doc.NewPage();
                doc.Add(tableDet);
                j++;
            }
        }

        byte data = (byte)Convert.ToDateTime(FechaDespM).DayOfWeek;

        string dia = "";
        string NumDia = Convert.ToDateTime(FechaDespM).Day.ToString();
        string NumMes = Convert.ToDateTime(FechaDespM).Month.ToString();
        string NumAnio = Convert.ToDateTime(FechaDespM).Year.ToString();
        string Mes = "";

        if (data == 1) dia = "Lunes";
        else if (data == 2) dia = "Martes";
        else if (data == 3) dia = "Miércoles";
        else if (data == 4) dia = "Jueves";
        else if (data == 5) dia = "Viernes";
        else if (data == 6) dia = "Sabado";
        else if (data == 0) dia = "Domingo";

        if (NumMes == "1") Mes = "Enero";
        else if (NumMes == "2") Mes = "Febrero";
        else if (NumMes == "3") Mes = "Marzo";
        else if (NumMes == "4") Mes = "Abril";
        else if (NumMes == "5") Mes = "Mayo";
        else if (NumMes == "6") Mes = "Junio";
        else if (NumMes == "7") Mes = "Julio";
        else if (NumMes == "8") Mes = "Agosto";
        else if (NumMes == "9") Mes = "Septiembre";
        else if (NumMes == "10") Mes = "Octubre";
        else if (NumMes == "11") Mes = "Noviembre";
        else if (NumMes == "12") Mes = "Diciembre";

        doc.Add(espacio);

        string FechaProxDesp = "Fecha Próximo Despacho: " + dia + ", " + NumDia + " de " + Mes + " " + NumAnio + ", CON RECETA NUEVA.";

        ph = new Paragraph(FechaProxDesp, font_Fecha);
        doc.Add(ph);

        doc.Add(espacio);

        // PIE
        PdfPTable tablePie = new PdfPTable(4);
        prop_cell(tablePie);

        float[] TamColumPie = new float[] { 1.0f, 2.5f, 1.5f, 1.5f };
        tablePie.SetWidths(TamColumPie);
        tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


        /////////////////////////////////////////////////////

        tablePie.AddCell(getCellNegrita("Datos Adquiriente", 8, PdfPCell.ALIGN_LEFT, 1));


        if (sTipoAdq == "1")
        {
            tablePie.AddCell(getCell("Usuario", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));
        }
        else
        {
            tablePie.AddCell(getCell(sNombAdq, 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell(sRutAdq, 8, PdfPCell.ALIGN_LEFT, 1));
        }


        tablePie.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

        doc.Add(tablePie);
        doc.Add(espacio);


        doc.Add(espacio);
        string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

        ph = new Paragraph(Rayas, font_celdas);
        doc.Add(ph);

        string MSG1 = "*Comprobante no válido como receta médica";

        ph = new Paragraph(MSG1, font_celdas);
        doc.Add(ph);

        string MSG2 = "Importante: volver con receta médica o gestionar su versión electrónica en la fecha indicada";

        ph = new Paragraph(MSG2, font_celdas);
        doc.Add(ph);

        string MSG3 = "Horario recepción recetas Lunes a Jueves 7:20 a 17:30h ; viernes 7:20 a 16:30h";

        ph = new Paragraph(MSG3, font_celdas);
        doc.Add(ph);

        string MSG4 = "Teléfono contacto: 225748669 (Ambulatoria);  225748677 (Oncología) ";

        ph = new Paragraph(MSG4, font_celdas);
        doc.Add(ph);

        ph = new Paragraph(Rayas, font_celdas);
        doc.Add(ph);

        doc.Add(espacio);






        doc.Close();


        return m.ToArray();


    }

    public byte[] CrearPDFComprobantesRec(string asIdentificador)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        //Document doc = new Document(PageSize.LETTER, miMarL, miMarR, (miMarTB * 3), miMarTB * 1);
        Document doc = new Document(PageSize.LETTER, miMarL, miMarR, (10), miMarTB * 1);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);

        // Arma tabla de Encabezado.
        Single[] colDtX = new Single[] { 150, 260, 120 };
        PdfPTable loTbX = new PdfPTable(3);

        loTbX.DefaultCell.Border = PdfPCell.NO_BORDER;
        String lsImCln = String.Empty;



        //writer.PageEvent = loEv;
        writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, "ownerpass", PdfWriter.AllowPrinting);
        // Agrega metadata al documento.
        doc.AddCreator("walter.pizarro@minsal.gob.cl");
        doc.AddAuthor("Wpizarror®");

        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.BOLD);
        Font font_Fecha = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL);
        Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);

        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string scant_desp_req = string.Empty;
        string sFDESPACHO = string.Empty;
        string sPENDIENTE = string.Empty;
        string sFMAXFECHA = string.Empty;

        string sTipoAdq = string.Empty;
        string sNombAdq = string.Empty;
        string sRutAdq = string.Empty;
        string sFonoAdq = string.Empty;
        string sNUM_REC_MANUAL = string.Empty;
        string sFULT_DESP = string.Empty;


        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");


        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));

        string lsSql = "select MAX(IDDESPACHO) IDDESPACHO from " + modConstantes.gsDbPer + "M_DESPACHOS where idreqaut = " + asIdentificador;
        DataSet registro1;
        con = bd.fnGetConn();
        registro1 = bd.Fill(con, lsSql);

        // Abre documento.
        doc.Open();

        DataSet registro;
        string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, " +
            "OBSERVACION, '35' EDAD, isnull(TipoAdq,1) TipoAdq, isnull(NombAdq,'') NombAdq, isnull(RutAdq,'') RutAdq, isnull(FonoAdq,'') FonoAdq, " +
            //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
            " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, ISNULL(NUM_REC_MANUAL,'')  NUM_REC_MANUAL, " +
            "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FMAXFECHA, " +
                "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + asIdentificador + ") DIAS_TRAT , " +
                "ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                "FROM M_RECETA R " +
                "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                "where IDRECETA = " + asIdentificador;

        con = bd.fnGetConn();
        registro = bd.Fill(con, sSql);
        con.Close();


        sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
        sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
        sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
        sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
        snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
        sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
        sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
        sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
        sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
        sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
        sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
        sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
        sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();
        sFMAXFECHA = registro.Tables[0].Rows[0]["FMAXFECHA"].ToString();

        sTipoAdq = registro.Tables[0].Rows[0]["TipoAdq"].ToString();
        sNombAdq = registro.Tables[0].Rows[0]["NombAdq"].ToString();
        sRutAdq = registro.Tables[0].Rows[0]["RutAdq"].ToString();
        sFonoAdq = registro.Tables[0].Rows[0]["FonoAdq"].ToString();
        sNUM_REC_MANUAL = registro.Tables[0].Rows[0]["NUM_REC_MANUAL"].ToString();


        registro.Dispose();


        //////////////////////////////////////////////////////////////////////////////////////////////


        string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");

        //Application.StartupPath;
        string rutaLogo = Application.UserAppDataPath;
        string rutaLogo2 = Application.LocalUserAppDataPath;
        string rutalogo = ruta;

        iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
        iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
        imagen.BorderWidth = 0;
        imagen.Alignment = Element.ALIGN_LEFT;
        float percentage = 0.0f;
        percentage = 70 / imagen.Width;
        imagen.ScalePercent(percentage * 100);
        //imagen.ScaleAbsolute(50f, 50f);

        //document.Add(imagen);

        // Cabecera Derecha
        PdfPTable tableDer = new PdfPTable(1);
        float[] anchosDer = new float[] { 0.50f };
        tableDer.DefaultCell.BorderWidth = 0;
        tableDer.SetWidths(anchosDer);

        tableDer.WidthPercentage = 90;

        //////////////////////////////////////////

        //tableDer.AddCell(imagen2);
        tableDer.AddCell("");


        // Cabecera Izquierda
        PdfPTable tableIzq = new PdfPTable(1);
        float[] anchosIzq = new float[] { 0.50f };
        tableIzq.DefaultCell.BorderWidth = 0;
        tableIzq.SetWidths(anchosIzq);

        tableIzq.WidthPercentage = 90;

        //////////////////////////////////////////

        //tableIzq.AddCell(imagen);
        tableIzq.AddCell("");

        // Cabecera Titulo
        PdfPTable tableTit = new PdfPTable(1);
        float[] anchosTit = new float[] { 1.50f };
        tableTit.DefaultCell.BorderWidth = 1;
        tableTit.SetWidths(anchosTit);

        tableTit.WidthPercentage = 90;

        //////////////////////////////////////////
        PdfPCell cell = new PdfPCell(new Phrase("COMPROBANTE ENTREGA DE MEDICAMENTOS", font_titulo));
        cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
        cell.Border = PdfPCell.NO_BORDER;

        tableTit.AddCell(cell);

        //Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
        //cell = new PdfPCell(new Phrase("" + sFOLIO, font));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.Border = PdfPCell.NO_BORDER;
        //tableTit.AddCell(cell);


        //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
        // Cabecera CENTRO
        PdfPTable tableCab1 = new PdfPTable(1);
        float[] anchos = new float[] { 0.50f };
        tableCab1.DefaultCell.BorderWidth = 0;
        tableCab1.SetWidths(anchos);

        tableCab1.WidthPercentage = 90;

        //////////////////////////////////////////

        cell = new PdfPCell(new Phrase(" Hospital Clinico San Borja Arriaran "));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.Border = PdfPCell.NO_BORDER;
        tableCab1.AddCell(cell);

        //////////////////////////////////////////


        tableCab1.AddCell(tableTit);

        //////////////////////////////////////////

        //cell = new PdfPCell(new Phrase("  "));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.Border = PdfPCell.NO_BORDER;
        //tableCab1.AddCell(cell);

        ////////////////////////////////////////////

        cell = new PdfPCell(new Phrase("(No válido como receta)", font_desc));
        cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        cell.Border = PdfPCell.NO_BORDER;
        tableCab1.AddCell(cell);

        ////////////////////////////////////////////

        //cell = new PdfPCell(new Phrase("AMBULATORIO"));
        //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //cell.Border = PdfPCell.NO_BORDER;
        //tableCab1.AddCell(cell);


        ///////////////////////////////////////////

        // Hora Hospital

        string Hosp = "Hospital Clínico San Borja Arriaran";

        Chunk Hosp_chuck = new Chunk
            (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
        Paragraph Hosp_par = new Paragraph();
        Hosp_par.Alignment = Element.ALIGN_RIGHT;
        Hosp_par.Add(Hosp_chuck);


        PdfPTable tableHosp = new PdfPTable(3);
        tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
        tableHosp.DefaultCell.BorderWidth = 0;
        tableHosp.HorizontalAlignment = Element.ALIGN_CENTER;
        tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
        tableHosp.TotalWidth = 540f;
        tableHosp.LockedWidth = true;

        float[] TamColumHora = new float[] { 0.1f, 5.50f, 0.1f };
        tableHosp.SetWidths(TamColumHora);
        tableHosp.HorizontalAlignment = Element.ALIGN_CENTER;

        tableHosp.AddCell(tableIzq);
        tableHosp.AddCell(tableCab1);
        tableHosp.AddCell(tableDer);
        //tableHosp.AddCell(Hosp_par);

        doc.Add(tableHosp);

        // Salto

        Chunk salto = new Chunk
        ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
        Paragraph salta = new Paragraph();
        salta.Alignment = Element.ALIGN_LEFT;
        salta.Add(salto);

        //document.Add(salta);
        //Paragraph p = new Paragraph();
        //p.Alignment = Element.ALIGN_CENTER;

        //Chunk c = new Chunk
        //    (lsTitulo, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

        //p.Add(c);


        //document.Add(p);
        doc.Add(salta);







        ///////////////////////////////////////////////////////////////////////////////////////////

        DateTime fecha_actual = DateTime.Today;
        bool b;
        //string asMsg;


        //asMsg = sFOLIO;

        //EventoTitulos ev = new EventoTitulos(asMsg);
        //writer.PageEvent = ev;

        ////doc.Open();
        //b = doc.AddAuthor("Sistema de Farmacia.");
        //b = doc.AddTitle("Receta Electrónica.");
        //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
        //iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

        //logo.ScaleAbsolute(180, 46);
        //linea.ScaleAbsolute(520, 3);

        //doc.Add(logo);

        //string FechaDesp = "Fecha Despacho: " + DateTime.Now.ToString("dd/MM/yyyy"); ;
        string FechaDesp = "Fecha Despacho: " + shoy;

        ph = new Paragraph(FechaDesp, font_celdas);
        doc.Add(ph);

        doc.Add(espacio);

        // Cabecera
        PdfPTable tableCab = new PdfPTable(4);
        prop_cell(tableCab);

        float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
        tableCab.SetWidths(TamColumCab);
        tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


        /////////////////////////////////////////////////////

        if (snombreSocial == "")
        {
            tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sNOMBRE, 8, PdfPCell.ALIGN_LEFT, 1));
        }
        else
        {
            tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(snombreSocial + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));

        }

        tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
        tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

        /////////////////////////////////////////////////////

        tableCab.AddCell(getCellNegrita("Servicio: ", 8, PdfPCell.ALIGN_LEFT, 1));
        tableCab.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));

        tableCab.AddCell(getCellNegrita("Folio Receta:", 8, PdfPCell.ALIGN_LEFT, 1));
        tableCab.AddCell(getCell(sFOLIO, 8, PdfPCell.ALIGN_LEFT, 1));

        if (sNUM_REC_MANUAL != "")
        {
            tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Folio Manual:", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sNUM_REC_MANUAL, 8, PdfPCell.ALIGN_LEFT, 1));
        }

        doc.Add(tableCab);

        doc.Add(espacio);


        DataSet registroDet;
        sSql = "SELECT V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBS_FARM OBSERVACIONES,  " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT,ISNULL(V.UN_MED,'-') FORMA,  " +
                "convert(decimal(20,0),(ISNULL(DD.CANT_DESP,0) - ISNULL(DD.CANT_DEV,0))) CANT_DESP, " +
                " ISNULL(ACUM_RANGO_D,0) sDIAS_TRATA,   " +
                "CONVERT(DECIMAL(20,0),(ISNULL(CANT_PEND_D,0) + ISNULL(DD.CANT_DEV,0))) PENDIENTE,   " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0)  " +
                "then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0)))  " +
                "else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +   ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO,  " +
                "CONVERT(VARCHAR(10),DD.FDESPACHO_D,103) FDESPACHO, ISNULL(VIA.DESCRIPCION,'-') DVIA, " +
                "convert(decimal(20,0),ISNULL(DD.RANGO_DISP_D,0)) CANTIDAD, ISNULL(DIA.DIA,'') DIA   " +
                "FROM " + modConstantes.gsDbPer + "M_DESPACHOS DE " +
                "INNER JOIN " + modConstantes.gsDbPer + "M_DETDESP DD ON DD.IDDESPACHO = DE.IDDESPACHO " +
                "INNER JOIN M_ART_RECETA D ON D.IDARTRECETA = DD.IDARTRECETA " +
                "INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO    " +
                "left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA    " +
                "left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO    " +
               " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO    " +
                "LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  " +
                "WHERE DE.IDDESPACHO = " + registro1.Tables[0].Rows[0]["IDDESPACHO"].ToString() + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3  " +
                "order by   V.CODARTICULO asc";

        con = bd.fnGetConn();
        registroDet = bd.Fill(con, sSql);
        con.Close();


        DataTable dt = registroDet.Tables[0];

        int i = 1;
        int j = 1;


        // Detalle Despachos 1
        PdfPTable tableDet = new PdfPTable(10);
        prop_cell(tableDet);
        float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 0.7f, 1.5f, 1.2f, 1.6f, 1.3f, 1.7f };
        //float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
        tableDet.SetWidths(TamColumDet);
        tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

        tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Vía Admin.", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Cantidad Pendiente", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Cantidad Despachada", 7, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Proximo Despacho", 8, PdfPCell.ALIGN_CENTER, 1));
        tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));


        foreach (DataRow row in dt.Rows)
        {

            sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
            sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
            sDPRESENT = Convert.ToString(row["DPRESENT"]);
            sFORMA = Convert.ToString(row["FORMA"]);
            sDRANGO = Convert.ToString(row["DRANGO"]);
            sDVIA = Convert.ToString(row["DVIA"]);
            sCANTIDAD = Convert.ToString(row["CANTIDAD"]);

            scant_desp_req = Convert.ToString(row["CANT_DESP"]);
            sFDESPACHO = Convert.ToString(row["FDESPACHO"]);
            sPENDIENTE = Convert.ToString(row["PENDIENTE"]);

            if (Convert.ToString(row["sDIAS_TRATA"]) != null)
            {
                sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
            }
            else
            {
                sDIAS_TRATA = "0";
            }
            sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

            if (Convert.ToString(row["DIA"]) == "")
                sDIA = "";
            else
                sDIA = " / " + Convert.ToString(row["DIA"]) + "";

            tableDet.AddCell(getCell(i.ToString(), 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(sCODARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCell(sNOM_ARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
            //tableDet.AddCell(getCell(sFORMA, 7, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCell(sDRANGO, 7, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCell(sDVIA, 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(sCANTIDAD, 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(sPENDIENTE, 7, PdfPCell.ALIGN_CENTER, 1));
            tableDet.AddCell(getCell(scant_desp_req, 7, PdfPCell.ALIGN_CENTER, 1));


            //if (sCALCULAR == "1")
            //    tableDet.AddCell(getCell(sDIAS_TRATA + sDIA, 7, PdfPCell.ALIGN_CENTER, 1));
            //else
            //    tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCell(sFDESPACHO, 7, PdfPCell.ALIGN_CENTER, 1));

            tableDet.AddCell(getCell(sOBSERVACIONES, 7, PdfPCell.ALIGN_LEFT, 1));


            i++;
            doc.Add(tableDet);

            tableDet.Rows.Clear();

            if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
            {
                doc.NewPage();
                doc.Add(tableDet);
                j++;
            }
        }
        byte data = (byte)Convert.ToDateTime(sFMAXFECHA).DayOfWeek;

        string dia = "";
        string NumDia = Convert.ToDateTime(sFMAXFECHA).Day.ToString();
        string NumMes = Convert.ToDateTime(sFMAXFECHA).Month.ToString();
        string NumAnio = Convert.ToDateTime(sFMAXFECHA).Year.ToString();
        string Mes = "";

        if (data == 1) dia = "Lunes";
        else if (data == 2) dia = "Martes";
        else if (data == 3) dia = "Miércoles";
        else if (data == 4) dia = "Jueves";
        else if (data == 5) dia = "Viernes";
        else if (data == 6) dia = "Sabado";
        else if (data == 0) dia = "Domingo";

        if (NumMes == "1") Mes = "Enero";
        else if (NumMes == "2") Mes = "Febrero";
        else if (NumMes == "3") Mes = "Marzo";
        else if (NumMes == "4") Mes = "Abril";
        else if (NumMes == "5") Mes = "Mayo";
        else if (NumMes == "6") Mes = "Junio";
        else if (NumMes == "7") Mes = "Julio";
        else if (NumMes == "8") Mes = "Agosto";
        else if (NumMes == "9") Mes = "Septiembre";
        else if (NumMes == "10") Mes = "Octubre";
        else if (NumMes == "11") Mes = "Noviembre";
        else if (NumMes == "12") Mes = "Diciembre";

        doc.Add(espacio);

        string FechaProxDesp = "Fecha Próximo Despacho: " + dia + ", " + NumDia + " de " + Mes + " " + NumAnio + ", CON RECETA NUEVA."; ;

        ph = new Paragraph(FechaProxDesp, font_Fecha);
        doc.Add(ph);

        doc.Add(espacio);

        // PIE
        PdfPTable tablePie = new PdfPTable(4);
        prop_cell(tablePie);

        float[] TamColumPie = new float[] { 1.0f, 2.5f, 1.5f, 1.5f };
        tablePie.SetWidths(TamColumPie);
        tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


        /////////////////////////////////////////////////////

        tablePie.AddCell(getCellNegrita("Datos Adquiriente", 8, PdfPCell.ALIGN_LEFT, 1));


        if (sTipoAdq == "1")
        {
            tablePie.AddCell(getCell("Usuario", 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));
        }
        else
        {
            tablePie.AddCell(getCell(sNombAdq, 8, PdfPCell.ALIGN_LEFT, 1));
            tablePie.AddCell(getCell(sRutAdq, 8, PdfPCell.ALIGN_LEFT, 1));
        }


        tablePie.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

        doc.Add(tablePie);
        doc.Add(espacio);


        doc.Add(espacio);
        string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

        ph = new Paragraph(Rayas, font_celdas);
        doc.Add(ph);

        string MSG1 = "*Comprobante no válido como receta médica";

        ph = new Paragraph(MSG1, font_celdas);
        doc.Add(ph);

        string MSG2 = "Importante: volver con receta médica o gestionar su versión electrónica en la fecha indicada";

        ph = new Paragraph(MSG2, font_celdas);
        doc.Add(ph);

        string MSG3 = "Horario recepción recetas Lunes a Jueves 7:20 a 17:30h ; viernes 7:20 a 16:30h";

        ph = new Paragraph(MSG3, font_celdas);
        doc.Add(ph);

        string MSG4 = "Teléfono contacto: 225748669 (Ambulatoria);  225748677 (Oncología) ";

        ph = new Paragraph(MSG4, font_celdas);
        doc.Add(ph);

        ph = new Paragraph(Rayas, font_celdas);
        doc.Add(ph);

        doc.Add(espacio);






        doc.Close();


        return m.ToArray();


    }

    public byte[] CrearPDFComprobantesRut(DataSet loDs, String asCodusuario)
    {
        //OrdenCompra oc = new OrdenCompra();

        string lsRet = string.Empty;

        String asPath = string.Empty;


        miDoc = 1;
        iTextSharp.text.Font myfTb = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, 8);
        //int ix = 0;
        string lsAx = string.Empty;


        // Inicializa Documento.
        System.IO.MemoryStream m = new System.IO.MemoryStream();
        Document doc = new Document(PageSize.LETTER, miMarL, miMarR, (10), miMarTB * 1);
        doc.AddTitle("");
        // Setea contenido.
        PdfWriter writer = PdfWriter.GetInstance(doc, m);

        // Arma tabla de Encabezado.
        Single[] colDtX = new Single[] { 150, 260, 120 };
        PdfPTable loTbX = new PdfPTable(3);

        loTbX.DefaultCell.Border = PdfPCell.NO_BORDER;
        String lsImCln = String.Empty;


        //EventoTitulosAll ev = new EventoTitulosAll();
        //writer.PageEvent = ev;
        //// Asigna eventos.
        //DataSet aoDsEv = null;
        //_events loEv = new _events("OC", loTbX, lsImCln, aoDsEv);

        //writer.PageEvent = loEv;
        writer.SetEncryption(PdfWriter.STRENGTH128BITS, null, "ownerpass", PdfWriter.AllowPrinting);
        // Agrega metadata al documento.
        doc.AddCreator("walter.pizarro@minsal.gob.cl");
        doc.AddAuthor("Wpizarror®");

        BaseColor color_negro = new BaseColor(0, 0, 0);

        Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
        Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
        Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD);
        Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, Font.BOLD);
        Font font_Fecha = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, Font.NORMAL);
        Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);

        string sFOLIO = "";
        string sRUT = "";
        string sNOMBRE = "";
        string sAPELLIDOS = "";
        string snombreSocial = "";
        string sOBSERVACION = "";
        string sEDAD = "";
        string sDIAGNOSTICO = "";
        string sUNIDAD = "";
        string sDIAS_TRAT = "";
        string sPREVISION = "";
        string sidusuario = "";
        string sF_H_CREACION = "";

        string sCODARTICULO = string.Empty;
        string sNOM_ARTICULO = string.Empty;
        string sDPRESENT = string.Empty;
        string sFORMA = string.Empty;
        string sDRANGO = string.Empty;
        string sDVIA = string.Empty;
        string sCANTIDAD = string.Empty;
        string sDIAS_TRATA = string.Empty;
        string sOBSERVACIONES = string.Empty;
        string sCALCULAR = string.Empty;
        string sDIA = string.Empty;
        string scant_desp_req = string.Empty;
        string sFDESPACHO = string.Empty;
        string sPENDIENTE = string.Empty;
        string sFMAXFECHA = string.Empty;

        string sTipoAdq = string.Empty;
        string sNombAdq = string.Empty;
        string sRutAdq = string.Empty;
        string sFonoAdq = string.Empty;
        string sNUM_REC_MANUAL = string.Empty;
        string sFULT_DESP = string.Empty;

        Paragraph ph;
        Paragraph espacio = new Paragraph(" ");



        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));

        try
        {
            // Abre documento.
            doc.Open();

            foreach (DataRow rw in loDs.Tables[0].Rows)
            {
                DataSet registro;
                string sSql = "SELECT FOLIO, CONVERT(varchar(10),R.RUT) + '-' + R.DV RUT, R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT NOMBRE, " +
                    "OBSERVACION, '35' EDAD, isnull(TipoAdq,1) TipoAdq, isnull(NombAdq,'') NombAdq, isnull(RutAdq,'') RutAdq, isnull(FonoAdq,'') FonoAdq, " +
                    //"case when isnull(R.nombreSocial,'') = '' then R.nombre else R.nombreSocial end nombreSocial, " +
                    " isnull(R.nombreSocial,'') nombreSocial, ' ' + R.APELL_PAT + ' ' + R.APELL_MAT APELLIDOS, ISNULL(NUM_REC_MANUAL,'')  NUM_REC_MANUAL, " +
                    "R.DIAGNOSTICO, U.DESCRIPCION UNIDAD, convert(varchar(10),F_H_CREACION,103)  F_H_CREACION, dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FMAXFECHA, " +
                        "(SELECT  CONVERT(DECIMAL(20,0),MAX(RANGO)) FROM M_ART_RECETA WHERE ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + rw["IDRECETA"].ToString() + ") DIAS_TRAT , " +
                        "ISNULL(PR.DESCRIPCION,'S/D') PREVISION, idusuario " +
                        "FROM M_RECETA R " +
                        "INNER JOIN M_UNIDAD_OPERATIVA U ON U.CODUNIOP = R.CODUNIOP " +
                        "LEFT OUTER JOIN M_PACIENTE US ON US.RUT = R.RUT " +
                        "LEFT OUTER JOIN M_PREVISION PR ON PR.IDPREVISION = US.IDPREVISION " +
                        "where IDRECETA = " + rw["IDRECETA"].ToString();



                con = bd.fnGetConn();
                registro = bd.Fill(con, sSql);
                con.Close();


                sFOLIO = registro.Tables[0].Rows[0]["FOLIO"].ToString();
                sRUT = registro.Tables[0].Rows[0]["RUT"].ToString();
                sNOMBRE = registro.Tables[0].Rows[0]["NOMBRE"].ToString();
                sAPELLIDOS = registro.Tables[0].Rows[0]["APELLIDOS"].ToString();
                snombreSocial = registro.Tables[0].Rows[0]["nombreSocial"].ToString();
                sOBSERVACION = registro.Tables[0].Rows[0]["OBSERVACION"].ToString();
                sEDAD = registro.Tables[0].Rows[0]["EDAD"].ToString();
                sDIAGNOSTICO = registro.Tables[0].Rows[0]["DIAGNOSTICO"].ToString();
                sUNIDAD = registro.Tables[0].Rows[0]["UNIDAD"].ToString();
                sDIAS_TRAT = registro.Tables[0].Rows[0]["DIAS_TRAT"].ToString();
                sPREVISION = registro.Tables[0].Rows[0]["PREVISION"].ToString();
                sidusuario = registro.Tables[0].Rows[0]["idusuario"].ToString();
                sF_H_CREACION = registro.Tables[0].Rows[0]["F_H_CREACION"].ToString();
                sFMAXFECHA = registro.Tables[0].Rows[0]["FMAXFECHA"].ToString();

                sTipoAdq = registro.Tables[0].Rows[0]["TipoAdq"].ToString();
                sNombAdq = registro.Tables[0].Rows[0]["NombAdq"].ToString();
                sRutAdq = registro.Tables[0].Rows[0]["RutAdq"].ToString();
                sFonoAdq = registro.Tables[0].Rows[0]["FonoAdq"].ToString();
                sNUM_REC_MANUAL = registro.Tables[0].Rows[0]["NUM_REC_MANUAL"].ToString();


                registro.Dispose();


                //////////////////////////////////////////////////////////////////////////////////////////////


                string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");

                //Application.StartupPath;
                string rutaLogo = Application.UserAppDataPath;
                string rutaLogo2 = Application.LocalUserAppDataPath;
                string rutalogo = ruta;

                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
                iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
                imagen.BorderWidth = 0;
                imagen.Alignment = Element.ALIGN_LEFT;
                float percentage = 0.0f;
                percentage = 70 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                //imagen.ScaleAbsolute(50f, 50f);

                //document.Add(imagen);

                // Cabecera Derecha
                PdfPTable tableDer = new PdfPTable(1);
                float[] anchosDer = new float[] { 0.50f };
                tableDer.DefaultCell.BorderWidth = 0;
                tableDer.SetWidths(anchosDer);

                tableDer.WidthPercentage = 90;

                //////////////////////////////////////////

                //tableDer.AddCell(imagen2);
                tableDer.AddCell("");


                // Cabecera Izquierda
                PdfPTable tableIzq = new PdfPTable(1);
                float[] anchosIzq = new float[] { 0.50f };
                tableIzq.DefaultCell.BorderWidth = 0;
                tableIzq.SetWidths(anchosIzq);

                tableIzq.WidthPercentage = 90;

                //////////////////////////////////////////

                //tableIzq.AddCell(imagen);
                tableIzq.AddCell("");

                // Cabecera Titulo
                PdfPTable tableTit = new PdfPTable(1);
                float[] anchosTit = new float[] { 1.50f };
                tableTit.DefaultCell.BorderWidth = 1;
                tableTit.SetWidths(anchosTit);

                tableTit.WidthPercentage = 90;

                //////////////////////////////////////////
                PdfPCell cell = new PdfPCell(new Phrase("COMPROBANTE ENTREGA DE MEDICAMENTOS", font_titulo));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;

                tableTit.AddCell(cell);

                //Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
                //cell = new PdfPCell(new Phrase("" + sFOLIO, font));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //cell.Border = PdfPCell.NO_BORDER;
                //tableTit.AddCell(cell);


                //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
                // Cabecera CENTRO
                PdfPTable tableCab1 = new PdfPTable(1);
                float[] anchos = new float[] { 0.50f };
                tableCab1.DefaultCell.BorderWidth = 0;
                tableCab1.SetWidths(anchos);

                tableCab1.WidthPercentage = 90;

                //////////////////////////////////////////

                cell = new PdfPCell(new Phrase("Hospital Clinico San Borja Arriaran"));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableCab1.AddCell(cell);

                //////////////////////////////////////////


                tableCab1.AddCell(tableTit);

                //////////////////////////////////////////

                //cell = new PdfPCell(new Phrase("  "));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //cell.Border = PdfPCell.NO_BORDER;
                //tableCab1.AddCell(cell);

                ////////////////////////////////////////////

                cell = new PdfPCell(new Phrase("(NO válido como receta)", font_desc));
                cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                tableCab1.AddCell(cell);

                ////////////////////////////////////////////

                //cell = new PdfPCell(new Phrase("AMBULATORIO"));
                //cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                //cell.Border = PdfPCell.NO_BORDER;
                //tableCab1.AddCell(cell);


                ///////////////////////////////////////////

                // Hora Hospital

                string Hosp = "Hospital Clínico San Borja Arriaran";

                Chunk Hosp_chuck = new Chunk
                    (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
                Paragraph Hosp_par = new Paragraph();
                Hosp_par.Alignment = Element.ALIGN_RIGHT;
                Hosp_par.Add(Hosp_chuck);


                PdfPTable tableHosp = new PdfPTable(3);
                tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
                tableHosp.DefaultCell.BorderWidth = 0;
                tableHosp.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                tableHosp.TotalWidth = 540f;
                tableHosp.LockedWidth = true;

                float[] TamColumHora = new float[] { 0.1f, 5.50f, 0.1f };
                tableHosp.SetWidths(TamColumHora);
                tableHosp.HorizontalAlignment = Element.ALIGN_CENTER;

                tableHosp.AddCell(tableIzq);
                tableHosp.AddCell(tableCab1);
                tableHosp.AddCell(tableDer);
                //tableHosp.AddCell(Hosp_par);

                doc.Add(tableHosp);

                // Salto

                Chunk salto = new Chunk
                ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
                Paragraph salta = new Paragraph();
                salta.Alignment = Element.ALIGN_LEFT;
                salta.Add(salto);

                //document.Add(salta);
                //Paragraph p = new Paragraph();
                //p.Alignment = Element.ALIGN_CENTER;

                //Chunk c = new Chunk
                //    (lsTitulo, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

                //p.Add(c);


                //document.Add(p);
                doc.Add(salta);







                ///////////////////////////////////////////////////////////////////////////////////////////

                DateTime fecha_actual = DateTime.Today;
                bool b;
                //string asMsg;


                //asMsg = sFOLIO;

                //EventoTitulos ev = new EventoTitulos(asMsg);
                //writer.PageEvent = ev;

                ////doc.Open();
                //b = doc.AddAuthor("Sistema de Farmacia.");
                //b = doc.AddTitle("Receta Electrónica.");
                //iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
                //iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

                //logo.ScaleAbsolute(180, 46);
                //linea.ScaleAbsolute(520, 3);

                //doc.Add(logo);


                string FechaDesp = "Fecha Despacho: " + DateTime.Now.ToString("dd/MM/yyyy"); ;

                ph = new Paragraph(FechaDesp, font_celdas);
                doc.Add(ph);

                doc.Add(espacio);

                // Cabecera
                PdfPTable tableCab = new PdfPTable(4);
                prop_cell(tableCab);

                float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
                tableCab.SetWidths(TamColumCab);
                tableCab.HorizontalAlignment = Element.ALIGN_LEFT;


                /////////////////////////////////////////////////////

                if (snombreSocial == "")
                {
                    tableCab.AddCell(getCellNegrita("Nombre Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                    tableCab.AddCell(getCell(sNOMBRE, 8, PdfPCell.ALIGN_LEFT, 1));
                }
                else
                {
                    tableCab.AddCell(getCellNegrita("Nombre Social Paciente: ", 8, PdfPCell.ALIGN_LEFT, 1));
                    tableCab.AddCell(getCell(snombreSocial + sAPELLIDOS, 8, PdfPCell.ALIGN_LEFT, 1));

                }

                tableCab.AddCell(getCellNegrita("Rut:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sRUT, 8, PdfPCell.ALIGN_LEFT, 1));

                /////////////////////////////////////////////////////

                tableCab.AddCell(getCellNegrita("Servicio: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sUNIDAD, 8, PdfPCell.ALIGN_LEFT, 1));

                tableCab.AddCell(getCellNegrita("Folio Receta:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sFOLIO, 8, PdfPCell.ALIGN_LEFT, 1));

                if (sNUM_REC_MANUAL != "")
                {
                    tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
                    tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

                    tableCab.AddCell(getCellNegrita("Folio Manual:", 8, PdfPCell.ALIGN_LEFT, 1));
                    tableCab.AddCell(getCell(sNUM_REC_MANUAL, 8, PdfPCell.ALIGN_LEFT, 1));
                }

                doc.Add(tableCab);

                doc.Add(espacio);
                //doc.Add(linea);

                //// Detalle Despachos 1
                //PdfPTable tableDetCab = new PdfPTable(10);
                //prop_cell(tableDetCab);

                //float[] TamColumDetCab = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 2.0f, 1.5f, 1.2f, 1.6f, 0.7f, 1.7f };
                //tableDetCab.SetWidths(TamColumDetCab);
                //tableDetCab.HorizontalAlignment = Element.ALIGN_LEFT;

                //tableDetCab.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));


                //tableDetCab.AddCell(getCellNegrita("Principio Activo", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Vía Administración", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Cantidad Total", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("C.E.", 7, PdfPCell.ALIGN_CENTER, 1));

                //tableDetCab.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));

                //doc.Add(tableDetCab);

                DataSet registroDet;
                sSql = "select V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO,  D.OBS_FARM OBSERVACIONES,  ";
                sSql = sSql + "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, isnull(v.CALCULAR,1) CALCULAR,  ";
                sSql = sSql + " ISNULL(V.UN_MED,'-') FORMA,  CONVERT(DECIMAL(20,0),isnull(cant_desp_req,0)) cant_desp_req, ";
                sSql = sSql + "case when isnull(v.CALCULAR,1) = 1 then Convert(varchar(100),convert(decimal(20,0),D.POSOLOGIA)) else 'CALCULADA EN FARMACIA' end sDIAS_TRATA,  ";
                sSql = sSql + "  CASE WHEN (ISNULL(D.CANT_PEND,0) > 0 AND ISNULL(D.SALDO_SALE,0) <> 1)  " +
                            " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                            " THEN  CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                            " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END " +
                            " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END PENDIENTE, ";
                sSql = sSql + "CASE WHEN  ISNULL(D.FULT_DESP,'') = '' THEN CONVERT(VARCHAR(10),getdate(),103) " +
                                "else CONVERT(VARCHAR(10),D.FULT_DESP,103) end FULT_DESP, ";
                sSql = sSql + " (case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end +  ";
                sSql = sSql + " ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-'))  DRANGO, CONVERT(VARCHAR(10),D.FDESPACHO,103) FDESPACHO,";
                sSql = sSql + " ISNULL(VIA.DESCRIPCION,'-') DVIA,CASE WHEN ISNULL(D.RANGO_DISP,0) < 0 THEN 0 ELSE convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) END CANTIDAD, ISNULL(DIA.DIA,'') DIA ";
                sSql = sSql + " from M_ART_RECETA D  ";
                sSql = sSql + " INNER JOIN v_articulos V ON V.IDARTICULO = D.IDARTICULO  ";
                sSql = sSql + " left outer join [M_VIA] VIA ON VIA.IDVIA = D.IDVIA  ";
                sSql = sSql + " left outer JOIN [M_RANGO] R ON R.IDRANGO = D.IDRANGO  ";
                sSql = sSql + " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO  ";
                sSql = sSql + " LEFT OUTER JOIN M_DIAS DIA ON DIA.IDDIA = D.POSOLOGIA  ";
                sSql = sSql + "where D.IDRECETA = " + rw["IDRECETA"].ToString() + " AND ISNULL(D.IDESTADO,1) <> 3  ";
                sSql = sSql + " and ((cant_desp_req > 0 AND elib = 0)  OR (CANT_PEND > 0 AND cant_desp_req = 0 AND elib = 0)) ";
                sSql = sSql + " order by   V.CODARTICULO asc ";

                con = bd.fnGetConn();
                registroDet = bd.Fill(con, sSql);
                con.Close();


                DataTable dt = registroDet.Tables[0];

                int i = 1;
                int j = 1;


                // Detalle Despachos 1
                PdfPTable tableDet = new PdfPTable(11);
                prop_cell(tableDet);

                float[] TamColumDet = new float[] { 0.4f, 1.0f, 1.7f, 1.3f, 0.7f, 1.3f, 1.5f, 1.2f, 1.6f, 1.3f, 1.7f };
                tableDet.SetWidths(TamColumDet);
                tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

                tableDet.AddCell(getCellNegrita("N°", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Código", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Forma Farmacéutica", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Frecuencia", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Vía Admin.", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Fech. Desp.", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Días Tratamiento", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Cantidad Pendiente", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Cantidad Despachada", 7, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Proximo Despacho", 8, PdfPCell.ALIGN_CENTER, 1));
                tableDet.AddCell(getCellNegrita("Observaciones", 7, PdfPCell.ALIGN_CENTER, 1));


                foreach (DataRow row in dt.Rows)
                {

                    sCODARTICULO = Convert.ToString(row["CODARTICULO"]);
                    sNOM_ARTICULO = Convert.ToString(row["NOM_ARTICULO"]);
                    sDPRESENT = Convert.ToString(row["DPRESENT"]);
                    sFORMA = Convert.ToString(row["FORMA"]);
                    sDRANGO = Convert.ToString(row["DRANGO"]);
                    sDVIA = Convert.ToString(row["DVIA"]);
                    sCANTIDAD = Convert.ToString(row["CANTIDAD"]);
                    sCALCULAR = Convert.ToString(row["CALCULAR"]);
                    scant_desp_req = Convert.ToString(row["cant_desp_req"]);
                    sFDESPACHO = Convert.ToString(row["FDESPACHO"]);
                    sPENDIENTE = Convert.ToString(row["PENDIENTE"]);
                    sFULT_DESP = Convert.ToString(row["FULT_DESP"]);

                    if (Convert.ToString(row["sDIAS_TRATA"]) != null)
                    {
                        sDIAS_TRATA = Convert.ToString(row["sDIAS_TRATA"]);
                    }
                    else
                    {
                        sDIAS_TRATA = "0";
                    }
                    sOBSERVACIONES = Convert.ToString(row["OBSERVACIONES"]);

                    if (Convert.ToString(row["DIA"]) == "")
                        sDIA = "";
                    else
                        sDIA = " / " + Convert.ToString(row["DIA"]) + "";

                    tableDet.AddCell(getCell(i.ToString(), 7, PdfPCell.ALIGN_CENTER, 1));
                    tableDet.AddCell(getCell(sCODARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sNOM_ARTICULO, 7, PdfPCell.ALIGN_LEFT, 1));
                    //tableDet.AddCell(getCell(sFORMA, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sDRANGO, 7, PdfPCell.ALIGN_LEFT, 1));
                    tableDet.AddCell(getCell(sDVIA, 7, PdfPCell.ALIGN_CENTER, 1));
                    tableDet.AddCell(getCell(sFULT_DESP, 7, PdfPCell.ALIGN_CENTER, 1));
                    tableDet.AddCell(getCell(sCANTIDAD, 7, PdfPCell.ALIGN_CENTER, 1));
                    tableDet.AddCell(getCell(sPENDIENTE, 7, PdfPCell.ALIGN_CENTER, 1));
                    tableDet.AddCell(getCell(scant_desp_req, 7, PdfPCell.ALIGN_CENTER, 1));


                    //if (sCALCULAR == "1")
                    //    tableDet.AddCell(getCell(sDIAS_TRATA + sDIA, 7, PdfPCell.ALIGN_CENTER, 1));
                    //else
                    //    tableDet.AddCell(getCell(sDIAS_TRATA, 7, PdfPCell.ALIGN_CENTER, 1));

                    tableDet.AddCell(getCell(sFDESPACHO, 7, PdfPCell.ALIGN_CENTER, 1));

                    tableDet.AddCell(getCell(sOBSERVACIONES, 7, PdfPCell.ALIGN_LEFT, 1));


                    i++;
                    doc.Add(tableDet);

                    tableDet.Rows.Clear();

                    if (i == Convert.ToInt32(modConstantes.mfConstante("LINEAUNO")) || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
                    {
                        doc.NewPage();
                        doc.Add(tableDet);
                        j++;
                    }
                }

                byte data = (byte)Convert.ToDateTime(sFMAXFECHA).DayOfWeek;

                string dia = "";
                string NumDia = Convert.ToDateTime(sFMAXFECHA).Day.ToString();
                string NumMes = Convert.ToDateTime(sFMAXFECHA).Month.ToString();
                string NumAnio = Convert.ToDateTime(sFMAXFECHA).Year.ToString();
                string Mes = "";

                if (data == 1) dia = "Lunes";
                else if (data == 2) dia = "Martes";
                else if (data == 3) dia = "Miércoles";
                else if (data == 4) dia = "Jueves";
                else if (data == 5) dia = "Viernes";
                else if (data == 6) dia = "Sabado";
                else if (data == 0) dia = "Domingo";

                if (NumMes == "1") Mes = "Enero";
                else if (NumMes == "2") Mes = "Febrero";
                else if (NumMes == "3") Mes = "Marzo";
                else if (NumMes == "4") Mes = "Abril";
                else if (NumMes == "5") Mes = "Mayo";
                else if (NumMes == "6") Mes = "Junio";
                else if (NumMes == "7") Mes = "Julio";
                else if (NumMes == "8") Mes = "Agosto";
                else if (NumMes == "9") Mes = "Septiembre";
                else if (NumMes == "10") Mes = "Octubre";
                else if (NumMes == "11") Mes = "Noviembre";
                else if (NumMes == "12") Mes = "Diciembre";

                doc.Add(espacio);

                string FechaProxDesp = "Fecha Próximo Despacho: " + dia + ", " + NumDia + " de " + Mes + " " + NumAnio + ", CON RECETA NUEVA."; ;

                ph = new Paragraph(FechaProxDesp, font_Fecha);
                doc.Add(ph);

                doc.Add(espacio);

                // PIE
                PdfPTable tablePie = new PdfPTable(4);
                prop_cell(tablePie);

                float[] TamColumPie = new float[] { 1.0f, 2.5f, 1.5f, 1.5f };
                tablePie.SetWidths(TamColumPie);
                tablePie.HorizontalAlignment = Element.ALIGN_LEFT;


                /////////////////////////////////////////////////////

                tablePie.AddCell(getCellNegrita("Datos Adquiriente", 8, PdfPCell.ALIGN_LEFT, 1));


                if (sTipoAdq == "1")
                {
                    tablePie.AddCell(getCell("Usuario", 8, PdfPCell.ALIGN_LEFT, 1));
                    tablePie.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));
                }
                else
                {
                    tablePie.AddCell(getCell(sNombAdq, 8, PdfPCell.ALIGN_LEFT, 1));
                    tablePie.AddCell(getCell(sRutAdq, 8, PdfPCell.ALIGN_LEFT, 1));
                }


                tablePie.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));

                doc.Add(tablePie);
                doc.Add(espacio);


                doc.Add(espacio);
                string Rayas = "-------------------------------------------------------------------------------------------------------------------------------------------";

                ph = new Paragraph(Rayas, font_celdas);
                doc.Add(ph);

                string MSG1 = "*Comprobante no válido como receta médica";

                ph = new Paragraph(MSG1, font_celdas);
                doc.Add(ph);

                string MSG2 = "Importante: volver con receta médica o gestionar su versión electrónica en la fecha indicada";

                ph = new Paragraph(MSG2, font_celdas);
                doc.Add(ph);

                string MSG3 = "Horario recepción recetas Lunes a Jueves 7:20 a 17:30h ; viernes 7:20 a 16:30h";

                ph = new Paragraph(MSG3, font_celdas);
                doc.Add(ph);

                string MSG4 = "Teléfono contacto: 225748669 (Ambulatoria);  225748677 (Oncología) ";

                ph = new Paragraph(MSG4, font_celdas);
                doc.Add(ph);

                ph = new Paragraph(Rayas, font_celdas);
                doc.Add(ph);

                doc.Add(espacio);


                doc.NewPage();


            }



            doc.Close();


            return m.ToArray();

        }
        catch (Exception ex)
        {
            byte[] lsB = null;

            return lsB;
        }

    }





    #endregion

    #region Despachos
    public byte[] CrearPDFActasDespachos(string Identificador)
    {

        string sSql;

        ClassDespachos act = new ClassDespachos();
        DataSet registro;
        sSql = "SELECT isnull(ACT.IDSERVICIO,0) as IDSERVICIO, isnull(serv.DESCRIPCION,'S/D') as DESCRIPCION, convert(char(10),act.F_H_ACTA, 103)  fecha,  " +
               " tip.descripcion tipo, ACT.TIPOACTA, ACT.ORIGEN, " +
                "case when ACT.G_FAC > 0 then ACT.G_FAC else FACTURA end G_FAC,  " +
                "case when ACT.G_FAC > 0 then 'Guía' else 'Otros' end Tipo_DOC,   " +
                "usr.NOMBRE nom_usuario, ISNULL(ACT.IDESTADO,1) IDESTADO,    " +
                "convert(char(10),ACT.F_H_GFAC, 103)  F_H_GFAC, ACT.OBS_ACTA,  ACT.NUM_ACTA , '' TIPCAR, '' NOMCOM,  " +
                "isnull(ACT.OBS_Nula,'') OBS_Nula, BOD.DESCRIPCION_LARGA BODEGA,  " +
                "usr.NOMBRE as nombre_usr, convert(varchar(10),usr.RUT)   + '-' + usr.DV as rut_usr " +
                "FROM  " + modConstantes.gsDbPer + "M_ACT_RECEP AS ACT    " +
                "left outer join " + modConstantes.gsDbPer + "[V_UNIDAD_OPERATIVA] serv on serv.CODUNIOP = ACT.IDSERVICIO    " +
                " INNER JOIN " + modConstantes.gsDbPer + "TG_BOD_PERIFERICAS BOD ON BOD.IDBODPRERIF = ACT.IDBODPRERIF " +
                "inner join " + modConstantes.gsDbPer + "[v_tipo_acta] tip on tip.cod = act.TIPOACTA  " +
                "inner join  " + modConstantes.gsDbPer + "V_USUARIOS usr on usr.IDUSUARIO = act.IDUSUARIO  " +
                "WHERE ACT.IDACTRECEP = " + Identificador;

        con = bd.fnGetConn();
        registro = bd.Fill(con, sSql);
        con.Close();

        string sEstado = registro.Tables[0].Rows[0]["IDESTADO"].ToString();
        string srut_usr = registro.Tables[0].Rows[0]["rut_usr"].ToString();
        string snombre_usr = registro.Tables[0].Rows[0]["nombre_usr"].ToString();

        string sIDSERVICIO = registro.Tables[0].Rows[0]["IDSERVICIO"].ToString();
        string sDESCRIPCION = registro.Tables[0].Rows[0]["DESCRIPCION"].ToString();
        string sfecha = registro.Tables[0].Rows[0]["fecha"].ToString();
        string stipo = registro.Tables[0].Rows[0]["tipo"].ToString();

        string sBODEGA = registro.Tables[0].Rows[0]["BODEGA"].ToString();

        string sobs_nula = registro.Tables[0].Rows[0]["OBS_Nula"].ToString();

        string sG_FAC = registro.Tables[0].Rows[0]["G_FAC"].ToString();
        string sTipo_DOC = registro.Tables[0].Rows[0]["Tipo_DOC"].ToString();
        string snom_usuario = registro.Tables[0].Rows[0]["nom_usuario"].ToString();
        string snombre = registro.Tables[0].Rows[0]["NOMCOM"].ToString();
        string stipcar = registro.Tables[0].Rows[0]["TIPCAR"].ToString();
        string sF_H_GFAC = registro.Tables[0].Rows[0]["F_H_GFAC"].ToString();
        string sOBS_ACTA = registro.Tables[0].Rows[0]["OBS_ACTA"].ToString();

        string sNUM_ACTA = registro.Tables[0].Rows[0]["NUM_ACTA"].ToString();
        string sTIPOACTA = registro.Tables[0].Rows[0]["TIPOACTA"].ToString();
        string sORIGEN = registro.Tables[0].Rows[0]["ORIGEN"].ToString();


        string sidreqaut = act.mfIdAnulDepActa(Identificador);

        DateTime d = Convert.ToDateTime(sfecha);


        registro.Dispose();

        DateTime hoy = DateTime.Now;
        string shoy = hoy.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("fr-FR"));


        Document doc = new Document(PageSize.LETTER, 30f, 1.5f, 30f, 1.5f);

        using (MemoryStream output = new MemoryStream())
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, output);
            DateTime fecha_actual = DateTime.Today;
            bool b;

            string titulo = "";

            if (sTIPOACTA == "B")
                titulo = "ANULACION DESPACHO";
            else
                titulo = "ACTA DE RECEPCIÓN";

            EventoTitulos ev = new EventoTitulos(titulo + " N° " + sNUM_ACTA);
            wri.PageEvent = ev;

            doc.Open();
            b = doc.AddAuthor("Sistema de Abastecimiento.");
            b = doc.AddTitle("Despacho a Servicios.");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/Logo2.jpg"));
            iTextSharp.text.Image linea = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/imagenes/linea.png"));

            logo.ScaleAbsolute(180, 46);
            linea.ScaleAbsolute(520, 3);

            //doc.Add(logo);

            BaseColor color_negro = new BaseColor(0, 0, 0);

            Font font_celdas = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_copia = FontFactory.GetFont(FontFactory.COURIER_BOLD, 40, Font.NORMAL);
            Font font_normal = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_desc = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL);
            Font font_titulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.NORMAL);
            Font font_titulo_tabla = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, Font.BOLD, color_negro);



            Paragraph ph;
            Paragraph espacio = new Paragraph(" ");

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            salta.Add(salto);

            // Cabecera Izquierda
            PdfPTable tableCab = new PdfPTable(4);
            prop_cell(tableCab);

            float[] TamColumCab = new float[] { 1.0f, 1.5f, 1.0f, 1.5f };
            tableCab.SetWidths(TamColumCab);
            tableCab.HorizontalAlignment = Element.ALIGN_LEFT;

            tableCab.AddCell(getCellNegrita("Servicio: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sDESCRIPCION, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Bodega: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sBODEGA, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Fecha: ", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(sfecha, 8, PdfPCell.ALIGN_LEFT, 1));

            tableCab.AddCell(getCellNegrita("Tipo :", 8, PdfPCell.ALIGN_LEFT, 1));
            tableCab.AddCell(getCell(stipo, 8, PdfPCell.ALIGN_LEFT, 1));

            if (sTIPOACTA == "B")
            {
                tableCab.AddCell(getCellNegrita("Origen: ", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell(sORIGEN, 8, PdfPCell.ALIGN_LEFT, 1));

                tableCab.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 1));
                tableCab.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));
            }


            // Contorno
            PdfPTable tableCont = new PdfPTable(1);
            prop_cell_juntos(tableCont);

            float[] TamColumCont = new float[] { 5f };
            tableCont.SetWidths(TamColumCont);
            tableCont.HorizontalAlignment = Element.ALIGN_LEFT;

            if (sTIPOACTA == "B")
                tableCont.AddCell(getCellNegrita("Datos de la Anulación Despacho", 8, PdfPCell.ALIGN_LEFT, 0));
            else
                tableCont.AddCell(getCellNegrita("Datos Acta Recepción", 8, PdfPCell.ALIGN_LEFT, 0));

            tableCont.AddCell(tableCab);


            doc.Add(tableCont);

            doc.Add(espacio);

            // Cabecera Actas

            PdfPTable tableDetCab = new PdfPTable(5);
            prop_cell(tableDetCab);

            //float[] TamColumDetCab = new float[] { 0.2f, 0.5f, 1.9f, 0.5f, 0.5f, 0.7f, 0.7f };
            float[] TamColumDetCab = new float[] { 0.2f, 0.5f, 1.9f, 0.5f, 0.5f };
            tableDetCab.SetWidths(TamColumDetCab);
            tableDetCab.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDetCab.AddCell(getCellNegrita("N°", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDetCab.AddCell(getCellNegrita("Codigo", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDetCab.AddCell(getCellNegrita("Descripcion", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDetCab.AddCell(getCellNegrita("UM", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDetCab.AddCell(getCellNegrita("Cant", 8, PdfPCell.ALIGN_LEFT, 1));
            //tableDetCab.AddCell(getCellNegrita("Valor Unit", 8, PdfPCell.ALIGN_LEFT, 1));
            //tableDetCab.AddCell(getCellNegrita("Valor Total", 8, PdfPCell.ALIGN_LEFT, 1));


            DataSet registroDet;
            sSql = "Select DET.IDDETACTA, VM.CODARTICULO, SUBSTRING(VM.DESCRIPCION_LARGA,0,100) as NOM_ARTICULO, VM.CODIGO AS DESC_UN_MED,  ";
            sSql = sSql + " convert(decimal(20,0),isnull(DET.CANTIDAD,0)) CANTIDAD, convert(decimal(20,2),isnull(DET.PRECIO,0)) PRECIO , ";
            sSql = sSql + " convert(decimal(20,2),(isnull(DET.CANTIDAD,0)* isnull(DET.PRECIO,0))) VALORIZADO ";
            sSql = sSql + " FROM " + modConstantes.gsDbPer + "M_DETACTA AS DET ";
            sSql = sSql + " INNER JOIN " + modConstantes.gsDbPer + "V_ARTICULOS AS VM ON VM.IDARTICULO = DET.IDMATERIAL  ";
            sSql = sSql + " WHERE DET.CANTIDAD > 0 and DET.IDACTRECEP = " + Identificador;

            con = bd.fnGetConn();
            registroDet = bd.Fill(con, sSql);
            con.Close();


            DataTable dt = registroDet.Tables[0];

            int i = 1;
            int j = 1;

            string scodmaterial = string.Empty;
            string sCANT_RECEP = string.Empty;
            string sUnidad = string.Empty;
            string sdescripcion_larga = string.Empty;
            string sPRECIO_UNIT = string.Empty;
            string sTotal = string.Empty;

            decimal acum = 0;

            // Detalle Actas

            PdfPTable tableDet = new PdfPTable(5);
            prop_cell(tableDet);

            //float[] TamColumDet = new float[] { 0.2f, 0.5f, 1.9f, 0.5f, 0.5f, 0.7f, 0.7f };
            float[] TamColumDet = new float[] { 0.2f, 0.5f, 1.9f, 0.5f, 0.5f };
            tableDet.SetWidths(TamColumDet);
            tableDet.HorizontalAlignment = Element.ALIGN_LEFT;

            tableDet.AddCell(getCellNegrita("N°", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCellNegrita("Codigo", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCellNegrita("Descripcion", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCellNegrita("UM", 8, PdfPCell.ALIGN_LEFT, 1));
            tableDet.AddCell(getCellNegrita("Cant", 8, PdfPCell.ALIGN_LEFT, 1));
            //tableDet.AddCell(getCellNegrita("Valor Unit", 8, PdfPCell.ALIGN_LEFT, 1));
            //tableDet.AddCell(getCellNegrita("Valor Total", 8, PdfPCell.ALIGN_LEFT, 1));

            foreach (DataRow row in dt.Rows)
            {

                scodmaterial = Convert.ToString(row["CODARTICULO"]);
                sCANT_RECEP = Convert.ToString(row["CANTIDAD"]);
                sUnidad = Convert.ToString(row["DESC_UN_MED"]);
                sdescripcion_larga = Convert.ToString(row["NOM_ARTICULO"]);
                sPRECIO_UNIT = Convert.ToString(row["PRECIO"]);
                sTotal = Convert.ToString(row["VALORIZADO"]);

                tableDet.AddCell(getCell(i.ToString(), 8, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(scodmaterial, 8, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sdescripcion_larga, 8, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sUnidad, 8, PdfPCell.ALIGN_LEFT, 1));
                tableDet.AddCell(getCell(sCANT_RECEP, 8, PdfPCell.ALIGN_LEFT, 1));
                //tableDet.AddCell(getCell(Convert.ToDecimal(sPRECIO_UNIT).ToString("N2"), 8, PdfPCell.ALIGN_LEFT, 1));
                //tableDet.AddCell(getCell(Convert.ToDecimal(sTotal).ToString("N2"), 8, PdfPCell.ALIGN_LEFT, 1));

                acum = acum + Convert.ToDecimal(sTotal);

                i++;

                doc.Add(tableDet);

                tableDet.Rows.Clear();

                if (i == 37 || i == Convert.ToInt32(modConstantes.mfConstante("LINEAS")) * j)
                {
                    doc.NewPage();
                    doc.Add(tableDetCab);
                    j++;
                }

            }


            //PdfPTable tableDetUni = new PdfPTable(1);
            //prop_cell_juntos(tableDetUni);

            //float[] TamColumDetUni = new float[] { 5f };
            //tableDetUni.SetWidths(TamColumDetUni);
            //tableDetUni.HorizontalAlignment = Element.ALIGN_LEFT;

            //tableDetUni.AddCell(tableDet);

            //doc.Add(tableDetUni);


            // Totales


            //PdfPTable tableBasTot = new PdfPTable(2);
            //prop_cell_izq_der(tableBasTot);

            //float[] TamColumBasTot = new float[] { 1.0f, 1.0f };
            //tableBasTot.SetWidths(TamColumBasTot);
            //tableBasTot.HorizontalAlignment = Element.ALIGN_LEFT;

            //tableBasTot.AddCell(getCellNegrita("Subtotal:", 8, PdfPCell.ALIGN_LEFT, 1));
            //tableBasTot.AddCell(getCell(Convert.ToInt32(acum).ToString("N2"), 8, PdfPCell.ALIGN_LEFT, 1));

            //tableBasTot.AddCell(getCellNegrita("  ", 8, PdfPCell.ALIGN_LEFT, 1));
            //tableBasTot.AddCell(getCell("  ", 8, PdfPCell.ALIGN_LEFT, 1));

            //doc.Add(tableBasTot);





            // Cabecera Derecha
            PdfPTable tableEstado = new PdfPTable(1);
            prop_cell_izq_der(tableEstado);

            float[] TamColumEstado = new float[] { 2.0f };
            tableEstado.SetWidths(TamColumEstado);
            tableEstado.HorizontalAlignment = Element.ALIGN_LEFT;


            tableEstado.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));


            /////////////////////////////////////////
            if (sEstado == "52")
            {

                tableEstado.AddCell(getCell("A C T A  A N U L A D A ", 10, PdfPCell.ALIGN_CENTER, 1));
            }
            else
            {
                tableEstado.AddCell(getCell(" ", 8, PdfPCell.ALIGN_LEFT, 0));
            }

            ///////////////////////////////////////////////////////

            if (sEstado == "52")
            {
                tableEstado.AddCell(getCellNegrita("Obs:", 8, PdfPCell.ALIGN_LEFT, 1));
                tableEstado.AddCell(getCell(sobs_nula, 8, PdfPCell.ALIGN_LEFT, 1));

            }
            else
            {
                tableEstado.AddCell(getCellNegrita("", 8, PdfPCell.ALIGN_LEFT, 0));
                tableEstado.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
            }

            // Contorno
            PdfPTable tableContTotal = new PdfPTable(2);
            prop_cell_juntos(tableContTotal);

            float[] TamColumContTotal = new float[] { 2.0f, 2.0f };
            tableContTotal.SetWidths(TamColumContTotal);
            tableContTotal.HorizontalAlignment = Element.ALIGN_LEFT;


            tableContTotal.AddCell(tableEstado);

            //tableContTotal.AddCell(tableBasTot);

            doc.Add(tableContTotal);


            //////////////////////////////////////////////////////////

            //////////////////////////////////////////////////////////


            doc.Add(espacio);
            doc.Add(espacio);
            doc.Add(espacio);

            //Single[] colPf = new Single[] { 95, 145, 50, 95, 145 };
            //PdfPTable oTbPf = new PdfPTable(5);
            //oTbPf.DefaultCell.Border = PdfPCell.NO_BORDER;
            //oTbPf.SetTotalWidth(colPf);
            //oTbPf.SetTotalWidth(colPf);
            //oTbPf.LockedWidth = true;
            //oTbPf.HorizontalAlignment = Element.ALIGN_LEFT;
            //oTbPf.DefaultCell.Padding = 2;


            //string lsTim1 = "";
            //string lsTim2 = "";


            //oTbPf.AddCell(mfTxtPDF(lsTim1, modConstantes.Normal, 1, "L", true, true, 0, 95, 95, 0, "x"));
            ////oTbPf.AddCell(mfTxtPDF(lsFir1, modConstantes.Normal, 1, "L", true, true, 0, 0, 0, 0, "x"));
            //oTbPf.AddCell(mfTxtPDF("  ", modConstantes.Normal, 1, "L", true, true, 0, 0, 0, 0, "x"));
            //oTbPf.AddCell(mfTxtPDF("  ", modConstantes.Normal, 1, "C"));
            //oTbPf.AddCell(mfTxtPDF(lsTim2, modConstantes.Normal, 1, "L", true, true, 0, 95, 95, 0, "x"));
            ////oTbPf.AddCell(mfTxtPDF(lsFir2, modConstantes.Normal, 1, "L", true, true, 0, 0, 0, 0, "x"));
            //oTbPf.AddCell(mfTxtPDF("  ", modConstantes.Normal, 1, "L", true, true, 0, 0, 0, 0, "x"));
            //// Agrega pié de firma.
            //doc.Add(oTbPf);

            //////////////////////////////////////////////////////////

            PdfPTable tableFirma = new PdfPTable(2);
            prop_cell(tableFirma);

            float[] TamColumFirma = new float[] { 2.5f, 2.5f };
            tableFirma.SetWidths(TamColumFirma);
            tableFirma.HorizontalAlignment = Element.ALIGN_LEFT;


            tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));
            tableFirma.AddCell(getCell("_____________________", 8, PdfPCell.ALIGN_LEFT, 0));

            tableFirma.AddCell(getCell("Recibí Conforme", 8, PdfPCell.ALIGN_LEFT, 0));
            tableFirma.AddCell(getCell("Entregue Conforme", 8, PdfPCell.ALIGN_LEFT, 0));

            tableFirma.AddCell(getCell("Nombre: " + snombre_usr, 8, PdfPCell.ALIGN_LEFT, 0));
            tableFirma.AddCell(getCell("Nombre:", 8, PdfPCell.ALIGN_LEFT, 0));


            tableFirma.AddCell(getCell("Rut: " + srut_usr, 8, PdfPCell.ALIGN_LEFT, 0));
            tableFirma.AddCell(getCell("Rut:", 8, PdfPCell.ALIGN_LEFT, 0));

            doc.Add(tableFirma);
            doc.Add(salta);

            // Observaciones
            PdfPTable tableObs = new PdfPTable(1);
            prop_cell(tableObs);

            float[] TamColumObs = new float[] { 5f };
            tableObs.SetWidths(TamColumObs);
            tableObs.HorizontalAlignment = Element.ALIGN_LEFT;


            if (sTIPOACTA == "B")
            {
                tableObs.AddCell(getCellNegrita("Observaciones Anulación", 8, PdfPCell.ALIGN_LEFT, 0));
                //tableObs.AddCell(getCell(ClassDespachos.mfGetObsAnulDesp(sidreqaut), 8, PdfPCell.ALIGN_LEFT, 1));
                tableObs.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 1));
            }
            else
            {
                tableObs.AddCell(getCellNegrita("Observaciones Acta", 8, PdfPCell.ALIGN_LEFT, 0));
                tableObs.AddCell(getCell(sOBS_ACTA, 8, PdfPCell.ALIGN_LEFT, 1));
            }



            doc.Add(tableObs);

            doc.Close();

            //if (sEstado != "15")
            //{
            //    string lsRet = act.mfCambioEstadoActa(Identificador, "43");
            //}

            return output.ToArray();
        }

    }

    #endregion


    private PdfPCell mfTxtPDF(String asTxt,
                            int aoTipoTexto = 0,
                            int alColsPan = 1,
                            string asAlineacion = "L",
                            Boolean abBorder = false,
                            Boolean abImagen = false,
                            int alPadding = 0,
                            int alImgWid = 0,
                            int alImgHei = 0,
                            int alImgScalePerc = 0,
                            string asImgFirm = ""
                        )
    {


        PdfPCell oColE = new PdfPCell();

        iTextSharp.text.Font obFG = myfGral; // Fuente General
        iTextSharp.text.Font msFGU = myfGraU; // Fuente General Subrayado.
        iTextSharp.text.Font msFGN = myfGraN; // Fuente General Negrita.

        iTextSharp.text.Font msFP = iTextSharp.text.FontFactory.GetFont("HELVETICA", 8); // Fuente Normal, Pequeña.
        iTextSharp.text.Font msFMP = iTextSharp.text.FontFactory.GetFont("HELVETICA", 6); // Fuente Normal, Muy Pequeña.
        iTextSharp.text.Font msFPAZL = iTextSharp.text.FontFactory.GetFont("HELVETICA", "", false, 8, 0, iTextSharp.text.BaseColor.RED); // Fuente Normal, Pequeña, Azul.
        iTextSharp.text.Font msFPE = iTextSharp.text.FontFactory.GetFont("HELVETICA", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.RED);

        iTextSharp.text.Font msFT = myfTIT; // Titulo

        iTextSharp.text.Font msFS = iTextSharp.text.FontFactory.GetFont("HELVETICA", 9); // Sub-Título.
        iTextSharp.text.Font msFTC = iTextSharp.text.FontFactory.GetFont("HELVETICA", 9, iTextSharp.text.Font.BOLD); // Título Chico.

        iTextSharp.text.Font msfTICSN = myfTICSN; // Título Chico Negrita Subrayado.

        iTextSharp.text.Font msFG0 = iTextSharp.text.FontFactory.GetFont("HELVETICA", 14); // Fuente Grande.
        iTextSharp.text.Font msFG1 = iTextSharp.text.FontFactory.GetFont("HELVETICA", 19); // Fuente Grande (MAS)
        iTextSharp.text.Font msFMG = iTextSharp.text.FontFactory.GetFont("HELVETICA", 32); // Fuente Muy Grande.        
        iTextSharp.text.Font msFMMG = iTextSharp.text.FontFactory.GetFont("HELVETICA", 40); // Fuente Muy Muy Grande.

        iTextSharp.text.Font loFont = obFG;

        switch (aoTipoTexto)
        {
            case 0:
                loFont = obFG;
                break;
            case 1:
                loFont = msFGU;
                break;
            case 8:
                loFont = msFGN;
                break;
            case 5:
                loFont = msFP;
                break;
            case 10:
                loFont = msFMP;
                break;
            case 6:
                loFont = msFPE;
                break;
            case 4:
                loFont = msFS;
                break;
            case 2:
                loFont = msFT;
                break;
            case 3:
                loFont = msFTC;
                break;
            case 7:
                loFont = msFPAZL;
                break;
            case 9:
                loFont = msfTICSN;
                break;
            case 11:
                loFont = msFMG;
                break;
            case 12:
                loFont = msFG0;
                break;
            case 14:
                loFont = msFG1;
                break;
            case 13:
                loFont = msFMMG;
                break;

        }



        if (abImagen && asTxt.Trim() != "")
        {
            // Es una imagen

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(asTxt);

            if (alImgWid > 0 && alImgHei > 0)
            {
                img.ScaleToFit(alImgWid, alImgHei);
            }

            if (alImgScalePerc > 0)
            {
                img.ScalePercent(alImgScalePerc);
            }
            oColE.AddElement(img);
        }
        else
        {
            // Es un texto.
            Phrase loFr = new Phrase(asTxt, loFont);
            oColE.Phrase = loFr;
        }


        if (!abBorder)
        {
            oColE.Border = PdfPCell.NO_BORDER;
        }

        if (asImgFirm != "")
        {
            oColE.Border = PdfPCell.BOTTOM_BORDER;
        }

        if (alColsPan > 1)
        {
            oColE.Colspan = alColsPan;
        }

        asAlineacion = asAlineacion.Trim().ToUpper();


        if (asAlineacion == "L") oColE.HorizontalAlignment = Element.ALIGN_LEFT;
        if (asAlineacion == "C") oColE.HorizontalAlignment = Element.ALIGN_CENTER;
        if (asAlineacion == "R") oColE.HorizontalAlignment = Element.ALIGN_RIGHT;
        // Si corresponde, agrega PADDING.
        if (alPadding > 0)
        {
            oColE.Padding = (alPadding);
        }


        return oColE;

    }

    public class EventoTitulos : PdfPageEventHelper
    {
        protected PdfTemplate total;
        protected BaseFont helv;
        protected Font helvN;
        protected BaseFont cour;
        //private bool settingFont = false;
        protected string lsTitulo;
        int totcountPage = 0;
        //PdfTemplate headerTemplate;

        public EventoTitulos(string titulo)
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //

            lsTitulo = titulo;
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {// Se crea el template
            //headerTemplate = writer.DirectContent.CreateTemplate(100, 100);
            //total.BoundingBox = new Rectangle(-20, -20, 100, 100);
            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

            cour = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");

            //Application.StartupPath;
            string rutaLogo = Application.UserAppDataPath;
            string rutaLogo2 = Application.LocalUserAppDataPath;
            string rutalogo = ruta;

            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
            iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_LEFT;
            float percentage = 0.0f;
            percentage = 70 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            //imagen.ScaleAbsolute(50f, 50f);

            //document.Add(imagen);

            // Cabecera Derecha
            PdfPTable tableDer = new PdfPTable(1);
            float[] anchosDer = new float[] { 0.50f };
            tableDer.DefaultCell.BorderWidth = 0;
            tableDer.SetWidths(anchosDer);

            tableDer.WidthPercentage = 90;

            //////////////////////////////////////////

            tableDer.AddCell(imagen2);


            // Cabecera Izquierda
            PdfPTable tableIzq = new PdfPTable(1);
            float[] anchosIzq = new float[] { 0.50f };
            tableIzq.DefaultCell.BorderWidth = 0;
            tableIzq.SetWidths(anchosIzq);

            tableIzq.WidthPercentage = 90;

            //////////////////////////////////////////

            tableIzq.AddCell(imagen);

            // Cabecera Titulo
            PdfPTable tableTit = new PdfPTable(2);
            float[] anchosTit = new float[] { 1.50f, 1.0f };
            tableTit.DefaultCell.BorderWidth = 0;
            tableTit.SetWidths(anchosTit);

            tableTit.WidthPercentage = 90;

            //////////////////////////////////////////
            PdfPCell cell = new PdfPCell(new Phrase("RECETA FARMACIA"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.Border = PdfPCell.NO_BORDER;

            tableTit.AddCell(cell);
            Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
            cell = new PdfPCell(new Phrase("N° " + lsTitulo, font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableTit.AddCell(cell);


            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
            // Cabecera CENTRO
            PdfPTable tableCab = new PdfPTable(1);
            float[] anchos = new float[] { 0.50f };
            tableCab.DefaultCell.BorderWidth = 0;
            tableCab.SetWidths(anchos);

            tableCab.WidthPercentage = 90;

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("  "));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);

            //////////////////////////////////////////


            tableCab.AddCell(tableTit);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("  "));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("PACIENTE"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("AMBULATORIO"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);


            ///////////////////////////////////////////

            // Hora Hospital

            string Hosp = "Hospital Clínico San Borja Arriaran";

            Chunk Hosp_chuck = new Chunk
                (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
            Paragraph Hosp_par = new Paragraph();
            Hosp_par.Alignment = Element.ALIGN_RIGHT;
            Hosp_par.Add(Hosp_chuck);


            PdfPTable tableHosp = new PdfPTable(3);
            tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
            tableHosp.DefaultCell.BorderWidth = 0;
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;
            tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableHosp.TotalWidth = 540f;
            tableHosp.LockedWidth = true;

            float[] TamColumHora = new float[] { 0.15f, 0.50f, 0.15f };
            tableHosp.SetWidths(TamColumHora);
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;

            tableHosp.AddCell(tableIzq);
            tableHosp.AddCell(tableCab);
            tableHosp.AddCell(tableDer);
            //tableHosp.AddCell(Hosp_par);

            document.Add(tableHosp);

            // Salto

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            salta.Add(salto);

            //document.Add(salta);
            //Paragraph p = new Paragraph();
            //p.Alignment = Element.ALIGN_CENTER;

            //Chunk c = new Chunk
            //    (lsTitulo, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

            //p.Add(c);


            //document.Add(p);
            document.Add(salta);

        }



        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            int pageNumber = writer.PageNumber - 1;

            totcountPage = writer.PageNumber;

        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            cb.BeginText();
            cb.SetFontAndSize(helv, 6);
            string sPiePagina = "";


            float textBase = 15; // Este lo pone la informacion en la parte inferior

            //sPiePagina = "PAGINA " + writer.PageNumber + " de " + totcountPage;
            sPiePagina = "SOFYA: PAGINA " + writer.PageNumber;
            cb.SetTextMatrix(document.Left, textBase);
            cb.ShowText(sPiePagina);

            cb.EndText();


            cb.RestoreState();
        }
    }

    public class EventoTitulosInfectologia : PdfPageEventHelper
    {
        protected PdfTemplate total;
        protected BaseFont helv;
        protected Font helvN;
        protected BaseFont cour;
        //private bool settingFont = false;
        protected string lsTitulo;
        int totcountPage = 0;
        //PdfTemplate headerTemplate;

        public EventoTitulosInfectologia(string titulo)
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //

            lsTitulo = titulo;
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {// Se crea el template
            //headerTemplate = writer.DirectContent.CreateTemplate(100, 100);
            //total.BoundingBox = new Rectangle(-20, -20, 100, 100);
            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);

            cour = BaseFont.CreateFont(BaseFont.COURIER_BOLD, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
        }
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            string ruta = HttpContext.Current.Server.MapPath("~/imagenes/");

            //Application.StartupPath;
            string rutaLogo = Application.UserAppDataPath;
            string rutaLogo2 = Application.LocalUserAppDataPath;
            string rutalogo = ruta;

            iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(rutalogo + "logo-HCSBA.jpg");
            iTextSharp.text.Image imagen2 = iTextSharp.text.Image.GetInstance(rutalogo + "logo_hcsba_ministerial.jpg");
            imagen.BorderWidth = 0;
            imagen.Alignment = Element.ALIGN_LEFT;
            float percentage = 0.0f;
            percentage = 70 / imagen.Width;
            imagen.ScalePercent(percentage * 100);
            //imagen.ScaleAbsolute(50f, 50f);

            //document.Add(imagen);

            // Cabecera Derecha
            PdfPTable tableDer = new PdfPTable(1);
            float[] anchosDer = new float[] { 0.50f };
            tableDer.DefaultCell.BorderWidth = 0;
            tableDer.SetWidths(anchosDer);

            tableDer.WidthPercentage = 90;

            //////////////////////////////////////////

            tableDer.AddCell(imagen2);


            // Cabecera Izquierda
            PdfPTable tableIzq = new PdfPTable(1);
            float[] anchosIzq = new float[] { 0.50f };
            tableIzq.DefaultCell.BorderWidth = 0;
            tableIzq.SetWidths(anchosIzq);

            tableIzq.WidthPercentage = 90;

            //////////////////////////////////////////

            tableIzq.AddCell(imagen);

            // Cabecera Titulo
            PdfPTable tableTit = new PdfPTable(2);
            float[] anchosTit = new float[] { 1.50f, 1.0f };
            tableTit.DefaultCell.BorderWidth = 0;
            tableTit.SetWidths(anchosTit);

            tableTit.WidthPercentage = 90;

            //////////////////////////////////////////
            PdfPCell cell = new PdfPCell(new Phrase("PASE INFECTOLOGIA"));
            cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cell.Border = PdfPCell.NO_BORDER;

            tableTit.AddCell(cell);
            Font font = FontFactory.GetFont("HELVETICA", 12, Font.BOLD);
            cell = new PdfPCell(new Phrase("N° " + lsTitulo, font));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableTit.AddCell(cell);


            //tablePac.AddCell(getCell("", 8, PdfPCell.ALIGN_LEFT, 0));
            // Cabecera CENTRO
            PdfPTable tableCab = new PdfPTable(1);
            float[] anchos = new float[] { 0.50f };
            tableCab.DefaultCell.BorderWidth = 0;
            tableCab.SetWidths(anchos);

            tableCab.WidthPercentage = 90;

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("  "));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);

            //////////////////////////////////////////


            tableCab.AddCell(tableTit);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase("  "));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase(""));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);

            //////////////////////////////////////////

            cell = new PdfPCell(new Phrase(""));
            cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            cell.Border = PdfPCell.NO_BORDER;
            tableCab.AddCell(cell);


            ///////////////////////////////////////////

            // Hora Hospital

            string Hosp = "Hospital Clínico San Borja Arriaran";

            Chunk Hosp_chuck = new Chunk
                (Hosp, FontFactory.GetFont("HELVETICA", 10, Font.BOLD, BaseColor.BLACK));
            Paragraph Hosp_par = new Paragraph();
            Hosp_par.Alignment = Element.ALIGN_RIGHT;
            Hosp_par.Add(Hosp_chuck);


            PdfPTable tableHosp = new PdfPTable(3);
            tableHosp.DefaultCell.Border = Rectangle.RECTANGLE;
            tableHosp.DefaultCell.BorderWidth = 0;
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;
            tableHosp.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tableHosp.TotalWidth = 540f;
            tableHosp.LockedWidth = true;

            float[] TamColumHora = new float[] { 0.15f, 0.50f, 0.15f };
            tableHosp.SetWidths(TamColumHora);
            tableHosp.HorizontalAlignment = Element.ALIGN_LEFT;

            tableHosp.AddCell(tableIzq);
            tableHosp.AddCell(tableCab);
            tableHosp.AddCell(tableDer);
            //tableHosp.AddCell(Hosp_par);

            document.Add(tableHosp);

            // Salto

            Chunk salto = new Chunk
            ("\n", FontFactory.GetFont("HELVETICA", 12, Font.NORMAL, BaseColor.BLACK));
            Paragraph salta = new Paragraph();
            salta.Alignment = Element.ALIGN_LEFT;
            salta.Add(salto);

            //document.Add(salta);
            //Paragraph p = new Paragraph();
            //p.Alignment = Element.ALIGN_CENTER;

            //Chunk c = new Chunk
            //    (lsTitulo, FontFactory.GetFont("HELVETICA", 12, Font.BOLD, BaseColor.BLACK));

            //p.Add(c);


            //document.Add(p);
            document.Add(salta);

        }



        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            int pageNumber = writer.PageNumber - 1;

            totcountPage = writer.PageNumber;

        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED);
            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            cb.BeginText();
            cb.SetFontAndSize(helv, 6);
            string sPiePagina = "";


            float textBase = 15; // Este lo pone la informacion en la parte inferior

            //sPiePagina = "PAGINA " + writer.PageNumber + " de " + totcountPage;
            sPiePagina = "SOFYA: PAGINA " + writer.PageNumber;
            cb.SetTextMatrix(document.Left, textBase);
            cb.ShowText(sPiePagina);

            cb.EndText();


            cb.RestoreState();
        }
    }

}