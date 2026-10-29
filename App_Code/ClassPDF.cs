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
    public Dictionary<string, float> AnchosColumnas { get; set; }
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
        NombreInstitucion = "I.Municipalidad de Lo Espejo";
        ColumnasExcluir = new List<string>();
        AnchosColumnas = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
        ColumnasFechaHora = new List<string>();
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
            PdfPTable tabla = new PdfPTable(columnas.Count);
            tabla.WidthPercentage = 100f;
            tabla.HeaderRows = 1;
            tabla.SpacingBefore = 5f;
            tabla.SpacingAfter = 5f;
            tabla.SplitRows = true;
            tabla.SplitLate = false;
            float[] anchos = ObtenerAnchos(columnas, datos);
            tabla.SetWidths(anchos);
            foreach (DataColumn columna in columnas)
            {
                PdfPCell celda = new PdfPCell(new Phrase(columna.Caption, fuenteEncabezado));

                celda.BackgroundColor = new BaseColor(48, 71, 94);
                celda.HorizontalAlignment = Element.ALIGN_CENTER;
                celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                celda.Padding = 4f;
                celda.NoWrap = false;
                tabla.AddCell(celda);
            }
            for (int i = 0; i < datos.Rows.Count; i++)
            {
                DataRow fila = datos.Rows[i];
                foreach (DataColumn columna in columnas)
                {
                    object valor = fila[columna];
                    string texto = "";
                    if (valor != DBNull.Value && valor != null)
                    {
                        if (valor is DateTime)
                        {
                            texto = ColumnasFechaHora.Contains(columna.ColumnName,
                                StringComparer.OrdinalIgnoreCase)
                                ? ((DateTime)valor).ToString("dd/MM/yyyy HH:mm:ss")
                                : ((DateTime)valor).ToString(FormatoFecha);
                        }
                        else
                        {
                            texto = Convert.ToString(valor, CultureInfo.CurrentCulture);
                        }
                    }
                    PdfPCell celda = new PdfPCell(new Phrase(texto, fuenteCelda));
                    celda.Padding = 4f;
                    celda.VerticalAlignment = Element.ALIGN_MIDDLE;
                    celda.NoWrap = false;
                    if (i % 2 == 1)
                        celda.BackgroundColor = new BaseColor(240, 243, 246);
                    if (EsNumerica(columna.DataType))
                        celda.HorizontalAlignment = Element.ALIGN_RIGHT;
                    else
                        celda.HorizontalAlignment = Element.ALIGN_LEFT;
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