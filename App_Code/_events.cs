using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;

/// <summary>
/// Descripción breve de _events
/// </summary>
public class _events : PdfPageEventHelper
{
    private iTextSharp.text.Font myfgral = iTextSharp.text.FontFactory.GetFont("HELVETICA", 10);
    private PdfContentByte _content;
    private BaseFont _baseFont = null;
    String msAux = String.Empty;
    String msTipo = String.Empty;
    PdfPTable moTb;
    DataSet moDs;

    public _events(String asTipo, PdfPTable aoTabla, String asAux, DataSet aoDs)
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        msTipo = asTipo;
        moTb = aoTabla;
        moDs = aoDs;
        msAux = asAux;
    }

    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        _baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        _content = writer.DirectContent;
    }

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        if (msTipo == "OC")
        {
            // Asigna página x de y a la tercera celda.
            String lsPagAct = Convert.ToString(writer.PageNumber);

            Phrase loPhr = new Phrase("Página: " + lsPagAct, myfgral);

            PdfPCell cell = new PdfPCell(loPhr);

            moTb.AddCell(cell);

            //moTb.GetRow(0).GetCells(2).Phrase = loPhr;

            moTb.WriteSelectedRows(0, -1, 25, writer.PageSize.Height - document.TopMargin + moTb.TotalHeight - (0), _content);
            // Si corresponde, muestra imagen de clon.
            // Si corresponde agrega imagen que indica compra de 'clones'.
            if (msAux != "")
            {
                Image loImx1 = null;
                loImx1 = Image.GetInstance(msAux);
                loImx1.SetAbsolutePosition(340, document.PageSize.Height - loImx1.Height);
                document.Add(loImx1);
            }
        }
    }

}

