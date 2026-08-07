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
                        int indice = GetIndexColumna(celda.CellReference);
                        while (dt.Columns.Count <= indice)
                            dt.Columns.Add();
                        dt.Columns[indice].ColumnName = LeerCelda(celda, wbPart);
                    }
                    primera = false;
                }
                else
                {
                    DataRow dr = dt.NewRow();                    
                    foreach (Cell celda in fila.Elements<Cell>())
                    {
                        int indice = GetIndexColumna(celda.CellReference);
                        if (indice < dt.Columns.Count)
                            dr[indice] = LeerCelda(celda, wbPart);
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
    private int GetIndexColumna(string refCelda)
    {
        string columna = new string(refCelda.Where(Char.IsLetter).ToArray());
        int indice = 0;
        foreach (char c in columna)
        {
            indice *= 26;
            indice += c - 'A' + 1;
        }
        return indice - 1;
    }
    public string ValidarColumnas(DataTable dt, string[] columnas)
    {
        string mensaje = "";

        foreach (string columna in columnas)
        {
            if (!dt.Columns.Contains(columna))
            {
                mensaje += "Falta la columna: " + columna + ". ";
            }
        }
        return mensaje;
    }
}