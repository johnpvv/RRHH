using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;
/// <summary>
/// Descripción: Permite crear archivos PDF parametrizando los tipos nombres, etc y generando el archivo a descargar
/// </summary>
public class ClassPDF
{
    public string Titulo { get; set; }
    public string Subtitulo { get; set; }
    public bool OrientacionHorizontal { get; set; }
    public bool MostrarFecha { get; set; }
    public bool MostrarNumeracion { get; set; }
    public float TamanoFuente { get; set; }
    public float TamanoTitulo { get; set; }
    public float MargenIzquierdo { get; set; }
    public float MargenDerecho { get; set; }
    public float MargenSuperior { get; set; }
    public float MargenInferior { get; set; }
    public string FormatoFecha { get; set; }
    public string RutaLogo { get; set; }
    public string NombreInstitucion { get; set; }
    public List<string> ColumnasFechaHora { get; set; }
    public List<string> ColumnasExcluir { get; set; }
    public List<string> ColumnasSoloHora { get; private set; }
    public Dictionary<string, float> AnchosColumnas { get; set; }
    // Tablas adicionales que se mostrarán después de la tabla principal
    public List<DataTable> TablasAdicionales { get; set; }
    // Títulos para cada tabla adicional
    public List<string> TitulosTablasAdicionales { get; set; }
    // Información adicional en formato texto
    public List<string> InformacionAdicional { get; set; }

    public ClassPDF()//constructor define valores por defecto, John Vaccarella
    {
        Titulo = "";
        Subtitulo = "";
        OrientacionHorizontal = false;
        MostrarFecha = true;
        MostrarNumeracion = true;
        TamanoFuente = 8f;
        TamanoTitulo = 16f;
        MargenIzquierdo = 20f;
        MargenDerecho = 20f;
        MargenSuperior = 25f;
        MargenInferior = 28f;
        FormatoFecha = "dd/MM/yyyy";
        RutaLogo = HttpContext.Current.Server.MapPath("~/imagenes/logo.jpg");
        NombreInstitucion = "I. Municipalidad de Lo Espejo";
        ColumnasExcluir = new List<string>();
        AnchosColumnas = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
        ColumnasFechaHora = new List<string>();
        ColumnasSoloHora = new List<string>();
        TablasAdicionales = new List<DataTable>();
        TitulosTablasAdicionales = new List<string>();
        InformacionAdicional = new List<string>();
    }
    public void ExportarDataTable(HttpResponse response, DataTable datos, string nombreArchivo)
    {
        if (response == null)
            throw new ArgumentNullException("response");
        if (datos == null)
            throw new ArgumentNullException("datos");
        if (string.IsNullOrWhiteSpace(nombreArchivo))
            nombreArchivo = "Reporte.pdf";
        nombreArchivo = Path.GetFileName(nombreArchivo);
        if (!nombreArchivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            nombreArchivo += ".pdf";
        List<DataColumn> columnas = new List<DataColumn>();
        foreach (DataColumn columna in datos.Columns)
        {
            if (!ColumnasExcluir.Contains(columna.ColumnName, StringComparer.OrdinalIgnoreCase))
                columnas.Add(columna);
        }
        if (columnas.Count == 0)
            throw new InvalidOperationException("El reporte no tiene columnas para mostrar.");

        Rectangle pagina = OrientacionHorizontal ? PageSize.A4.Rotate() : PageSize.A4;
        using (MemoryStream ms = new MemoryStream())
        {
            Document documento = new Document(pagina, MargenIzquierdo, MargenDerecho, MargenSuperior, MargenInferior);
            PdfWriter writer = PdfWriter.GetInstance(documento, ms);
            writer.CloseStream = false;
            writer.PageEvent = new EventoPaginaPDF(MostrarNumeracion);
            documento.Open();

            Font fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA, TamanoTitulo, Font.BOLD, BaseColor.BLACK);
            Font fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA, 10f, Font.NORMAL, BaseColor.DARK_GRAY);
            Font fuenteEncabezado = FontFactory.GetFont(FontFactory.HELVETICA, TamanoFuente, Font.BOLD, BaseColor.WHITE);
            Font fuenteCelda = FontFactory.GetFont(FontFactory.HELVETICA, TamanoFuente, Font.NORMAL, BaseColor.BLACK);
            PdfPTable encabezado = new PdfPTable(2);
            encabezado.WidthPercentage = 100f;
            encabezado.SetWidths(new float[] { 18f, 82f });
            encabezado.SpacingAfter = 12f;
            PdfPCell celdaLogo = new PdfPCell();
            celdaLogo.Border = Rectangle.NO_BORDER;
            celdaLogo.VerticalAlignment = Element.ALIGN_MIDDLE;
            celdaLogo.HorizontalAlignment = Element.ALIGN_CENTER;
            celdaLogo.Padding = 4f;
            if (!string.IsNullOrWhiteSpace(RutaLogo) && File.Exists(RutaLogo))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(RutaLogo);
                logo.ScaleToFit(90f, 90f);
                celdaLogo.AddElement(logo);
            }
            encabezado.AddCell(celdaLogo);
            PdfPCell celdaTexto = new PdfPCell();
            celdaTexto.Border = Rectangle.NO_BORDER;
            celdaTexto.VerticalAlignment = Element.ALIGN_MIDDLE;
            celdaTexto.PaddingLeft = 8f;
            if (!string.IsNullOrWhiteSpace(NombreInstitucion))
            {
                Paragraph institucion = new Paragraph(NombreInstitucion, FontFactory.GetFont(FontFactory.HELVETICA, 11f, Font.BOLD));
                institucion.SpacingAfter = 4f;
                celdaTexto.AddElement(institucion);
            }
            if (!string.IsNullOrWhiteSpace(Titulo))
            {
                Paragraph titulo = new Paragraph(Titulo, FontFactory.GetFont(FontFactory.HELVETICA, TamanoTitulo, Font.BOLD));
                titulo.SpacingAfter = 4f;
                celdaTexto.AddElement(titulo);
            }
            if (!string.IsNullOrWhiteSpace(Subtitulo))
            {
                Paragraph subtitulo = new Paragraph(
                    Subtitulo, FontFactory.GetFont(FontFactory.HELVETICA, 9f, Font.NORMAL));
                subtitulo.SpacingAfter = 4f;
                celdaTexto.AddElement(subtitulo);
            }
            if (MostrarFecha)
            {
                Paragraph fecha = new Paragraph("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"), FontFactory.GetFont(FontFactory.HELVETICA, 8f, Font.NORMAL));
                celdaTexto.AddElement(fecha);
            }
            encabezado.AddCell(celdaTexto);
            documento.Add(encabezado);

            // TABLA PRINCIPAL
            AgregarTabla(documento, datos, fuenteEncabezado, fuenteCelda);

            // TABLAS ADICIONALES
            if (TablasAdicionales != null)
            {
                for (int i = 0; i < TablasAdicionales.Count; i++)
                {
                    DataTable tablaAdicional = TablasAdicionales[i];

                    if (tablaAdicional == null)
                        continue;

                    documento.Add(new Paragraph(" "));

                    if (TitulosTablasAdicionales != null &&
                        i < TitulosTablasAdicionales.Count &&
                        !string.IsNullOrWhiteSpace(TitulosTablasAdicionales[i]))
                    {
                        Paragraph tituloAdicional = new Paragraph(TitulosTablasAdicionales[i], fuenteTitulo);

                        tituloAdicional.SpacingBefore = 8f;
                        tituloAdicional.SpacingAfter = 6f;

                        documento.Add(tituloAdicional);
                    }
                    AgregarTabla(documento, tablaAdicional, fuenteEncabezado, fuenteCelda);
                }
            }
            // INFORMACIÓN ADICIONAL
            if (InformacionAdicional != null)
            {
                Font fuenteInfo = FontFactory.GetFont(
                    FontFactory.HELVETICA,
                    9f,
                    Font.NORMAL,
                    BaseColor.BLACK);

                foreach (string informacion in InformacionAdicional)
                {
                    if (!string.IsNullOrWhiteSpace(informacion))
                    {
                        Paragraph parrafo = new Paragraph(informacion, fuenteInfo);
                        parrafo.SpacingBefore = 5f;
                        documento.Add(parrafo);
                    }
                }
            }
            documento.Close();
            response.Clear();
            response.Buffer = true;
            response.ContentType = "application/pdf";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + nombreArchivo + "\"");
            response.BinaryWrite(ms.ToArray());
            response.Flush();
        }
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
    private float[] ObtenerAnchos(List<DataColumn> columnas, DataTable datos)
    {
        float[] anchos = new float[columnas.Count];
        for (int i = 0; i < columnas.Count; i++)
        {
            DataColumn columna = columnas[i];
            if (AnchosColumnas.ContainsKey(columna.ColumnName) &&
                AnchosColumnas[columna.ColumnName] > 0)
            {
                anchos[i] = AnchosColumnas[columna.ColumnName];
                continue;
            }
            int longitud = columna.Caption.Length;
            int limite = Math.Min(datos.Rows.Count, 30);
            for (int j = 0; j < limite; j++)
            {
                string valor = datos.Rows[j][columna] == DBNull.Value
                    ? ""
                    : Convert.ToString(datos.Rows[j][columna]);
                if (valor.Length > longitud)
                    longitud = valor.Length;
            }
            anchos[i] = Math.Max(8f, Math.Min(30f, longitud));
        }
        return anchos;
    }
    private bool EsNumerica(Type tipo)
    {
        tipo = Nullable.GetUnderlyingType(tipo) ?? tipo;
        return tipo == typeof(byte) ||
               tipo == typeof(short) ||
               tipo == typeof(int) ||
               tipo == typeof(long) ||
               tipo == typeof(float) ||
               tipo == typeof(double) ||
               tipo == typeof(decimal);
    }

    //tablas adicionales.
    private void AgregarTabla(Document documento, DataTable datos, Font fuenteEncabezado, Font fuenteCelda, bool alternarFilas = true)
    {
        if (datos == null || datos.Columns.Count == 0)
            return;
        List<DataColumn> columnas = new List<DataColumn>();
        foreach (DataColumn columna in datos.Columns)
        {
            if (!ColumnasExcluir.Contains(
                columna.ColumnName,
                StringComparer.OrdinalIgnoreCase))
            {
                columnas.Add(columna);
            }
        }

        if (columnas.Count == 0)
            return;

        PdfPTable tabla = new PdfPTable(columnas.Count);
        tabla.WidthPercentage = 100f;
        tabla.HeaderRows = 1;
        tabla.SplitRows = true;
        tabla.SplitLate = false;

        tabla.SetWidths(ObtenerAnchos(columnas, datos));

        foreach (DataColumn columna in columnas)
        {
            PdfPCell celda = new PdfPCell(new Phrase(columna.Caption, fuenteEncabezado));

            celda.BackgroundColor = new BaseColor(48, 71, 94);
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;
            celda.Padding = 4f;
            tabla.AddCell(celda);
        }

        for (int i = 0; i < datos.Rows.Count; i++)
        {
            foreach (DataColumn columna in columnas)
            {
                object valor = datos.Rows[i][columna];
                string texto = "";

                if (valor != DBNull.Value && valor != null)
                {
                    if (valor is DateTime)
                    {
                        DateTime fecha = (DateTime)valor;

                        if (ColumnasSoloHora.Contains(
                            columna.ColumnName,
                            StringComparer.OrdinalIgnoreCase))
                        {
                            texto = fecha.ToString("HH:mm:ss");
                        }
                        else
                        {
                            texto = ColumnasFechaHora.Contains(
                                columna.ColumnName,
                                StringComparer.OrdinalIgnoreCase)
                                ? fecha.ToString("dd/MM/yyyy HH:mm:ss")
                                : fecha.ToString(FormatoFecha);
                        }
                    }
                    else if (valor is TimeSpan &&
                        ColumnasSoloHora.Contains(columna.ColumnName, StringComparer.OrdinalIgnoreCase))
                    {
                        texto = ((TimeSpan)valor).ToString(@"hh\:mm\:ss");
                    }
                    else
                    {
                        texto = Convert.ToString(valor, CultureInfo.CurrentCulture);
                    }
                }

                PdfPCell celda = new PdfPCell(new Phrase(texto, fuenteCelda));

                celda.Padding = 4f;
                celda.VerticalAlignment = Element.ALIGN_MIDDLE;

                if (alternarFilas && i % 2 == 1)
                    celda.BackgroundColor = new BaseColor(240, 243, 246);

                celda.HorizontalAlignment = EsNumerica(columna.DataType)
                    ? Element.ALIGN_RIGHT
                    : Element.ALIGN_LEFT;

                tabla.AddCell(celda);
            }
        }

        if (datos.Rows.Count == 0)
        {
            PdfPCell vacio = new PdfPCell(new Phrase("No existen registros para mostrar.", fuenteCelda));

            vacio.Colspan = columnas.Count;
            vacio.HorizontalAlignment = Element.ALIGN_CENTER;
            vacio.Padding = 10f;
            tabla.AddCell(vacio);
        }
        documento.Add(tabla);
    }
    private class EventoPaginaPDF : PdfPageEventHelper
    {
        private readonly bool mostrarNumeracion;
        private BaseFont fuente;
        public EventoPaginaPDF(bool mostrar)
        {
            mostrarNumeracion = mostrar;
        }
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            fuente = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        }
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            if (!mostrarNumeracion)
                return;
            PdfContentByte contenido = writer.DirectContent;
            contenido.BeginText();
            contenido.SetFontAndSize(fuente, 8f);
            contenido.ShowTextAligned(Element.ALIGN_RIGHT, "Página " + writer.PageNumber, document.PageSize.GetRight(30f), document.PageSize.GetBottom(20f), 0f);
            contenido.EndText();
        }
    }
}