using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

/// <summary>
/// Descripción: Permite crear archivos excel, y subir archivos al sistema.
/// </summary>
public class ClassExcel
{
    public ClassExcel()
    {
        //Agregar aquí la lógica del constructor
    }

    public string ls_error { get; set; }
    //public DataTable LeerExcel(string archivo);
    //public bool ValidarCabecera(DataTable dt, string[] columnas);
    //public byte[] ExportarExcel(DataTable dt);
    //public byte[] CrearPlantillaTurnos();
    //public byte[] ExportarErrores(DataTable errores);

    public DataTable LeerExcel(string ruta)
    {
        DataTable dt = new DataTable();

        using (SpreadsheetDocument documento = SpreadsheetDocument.Open(ruta, false))
        {

            WorkbookPart wbPart = documento.WorkbookPart;
            Sheet hoja = wbPart.Workbook.Descendants<Sheet>().First();
            WorksheetPart wsPart = (WorksheetPart)wbPart.GetPartById(hoja.Id);

            SheetData datos = wsPart.Worksheet.Elements<SheetData>().First();

            bool primera = true;
            foreach (Row fila in datos.Elements<Row>())
            {
                if (primera)
                {
                    foreach (Cell celda in fila.Elements<Cell>())
                    {
                        dt.Columns.Add(LeerCelda(celda, wbPart));
                    }
                    primera = false;
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    int i = 0;
                    foreach (Cell celda in fila.Elements<Cell>())
                    {
                        dr[i] = LeerCelda(celda, wbPart);
                        i++;
                    }
                    dt.Rows.Add(dr);
                }
            }
        }
        return dt;
    }
    private string LeerCelda(Cell celda, WorkbookPart wbPart)
    {
        if (celda == null)
            return "";

        string valor = celda.InnerText;

        if (celda.DataType != null)
        {
            if (celda.DataType == CellValues.SharedString)
            {
                return wbPart.SharedStringTablePart
                    .SharedStringTable
                    .ChildElements[int.Parse(valor)]
                    .InnerText;
            }
        }
        return valor;
    }
}