using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;

/// <summary>
/// Descripción: Permite generar un archivo excel nativo con openxml para exportar datos del sistema, pasando datatable o dataSet
/// </summary>
public class ClassExcelExportar
{
    public ClassExcelExportar()
    {
        //Agregar aquí la lógica del constructor
    }

    public void Exportar(DataSet ds, string nombreArchivo)
    {
        if (ds == null || ds.Tables.Count == 0)
            return;

        Exportar(ds.Tables[0], nombreArchivo);
    }

    //public void Exportar(DataTable dt, string nombreArchivo)
    //{
    //    if (dt == null || dt.Columns.Count == 0)
    //        return;

    //    using (MemoryStream ms = new MemoryStream())
    //    {
    //        using (SpreadsheetDocument document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
    //        {
    //            WorkbookPart workbookPart = document.AddWorkbookPart();
    //            workbookPart.Workbook = new Workbook();
    //            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
    //            SheetData sheetData = new SheetData();
    //            worksheetPart.Worksheet = new Worksheet(sheetData);
    //            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
    //            Sheet sheet = new Sheet
    //            {
    //                Name = "Datos",
    //                SheetId = 1,
    //                Id = workbookPart.GetIdOfPart(worksheetPart)
    //            };

    //            sheets.Append(sheet);

    //            // Encabezados
    //            Row headerRow = new Row();
    //            foreach (DataColumn column in dt.Columns)
    //            {
    //                Cell cell = new Cell
    //                {
    //                    DataType = CellValues.InlineString,InlineString = new InlineString(new Text(column.ColumnName))
    //                };

    //                headerRow.Append(cell);
    //            }
    //            sheetData.Append(headerRow);

    //            // Datos
    //            foreach (DataRow row in dt.Rows)
    //            {
    //                Row dataRow = new Row();
    //                foreach (DataColumn column in dt.Columns)
    //                {
    //                    object valor = row[column];
    //                    Cell cell = CrearCelda(valor);
    //                    dataRow.Append(cell);
    //                }
    //                sheetData.Append(dataRow);
    //            }
    //            workbookPart.Workbook.Save();
    //        }
    //        Descargar(ms, nombreArchivo);
    //    }
    //}
    public static void Exportar(DataTable dt, string nombreArchivo)
    {
        if (dt == null || dt.Columns.Count == 0) return;
        using (MemoryStream ms = new MemoryStream())
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                WorkbookStylesPart stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();

                stylesPart.Stylesheet = new Stylesheet(
                    new Fonts(
                        new Font(),
                        new Font(new Bold())
                    ),
                    new Fills(
                        new Fill(new PatternFill { PatternType = PatternValues.None }),
                        new Fill(new PatternFill { PatternType = PatternValues.Gray125 })
                    ),
                    new Borders(new Border()),
                    new CellStyleFormats(new CellFormat()),
                    new CellFormats(
                        new CellFormat(),
                        new CellFormat { FontId = 1 }
                    )
                );
                stylesPart.Stylesheet.Save();
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet
                {
                    Name = "Datos",
                    SheetId = 1,
                    Id = workbookPart.GetIdOfPart(worksheetPart)
                };
                sheets.Append(sheet);
                // Anchos de columnas
                List<int> anchos = new List<int>();

                foreach (DataColumn column in dt.Columns)
                    anchos.Add(column.ColumnName.Length);

                // Encabezado
                Row headerRow = new Row();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    Cell cell = new Cell
                    {
                        DataType = CellValues.InlineString,
                        StyleIndex = 1,
                        InlineString = new InlineString(
                            new Text(dt.Columns[i].ColumnName)
                        )
                    };

                    headerRow.Append(cell);
                }
                sheetData.Append(headerRow);

                // Datos
                foreach (DataRow row in dt.Rows)
                {
                    Row dataRow = new Row();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        object valor = row[i];
                        string texto = valor == DBNull.Value ? "" : valor.ToString();

                        if (texto.Length > anchos[i])
                            anchos[i] = texto.Length;
                        dataRow.Append(CrearCelda(valor));
                    }
                    sheetData.Append(dataRow);
                }
                // Aplicar anchos
                Columns columns = new Columns();
                for (int i = 0; i < anchos.Count; i++)
                {
                    double ancho = Math.Min(anchos[i] + 2, 50);
                    columns.Append(new Column
                    {
                        Min = (uint)(i + 1),
                        Max = (uint)(i + 1),
                        Width = ancho,
                        CustomWidth = true
                    });
                }

                worksheetPart.Worksheet.InsertAt(columns, 0);

                workbookPart.Workbook.Save();
            }
            Descargar(ms, nombreArchivo);
        }
    }
    private static Cell CrearCelda(object valor)
    {
        if (valor == null || valor == DBNull.Value)
        {
            return new Cell
            {
                DataType = CellValues.InlineString,
                InlineString = new InlineString(new Text(""))
            };
        }

        if (valor is DateTime)
        {
            DateTime fecha = (DateTime)valor;

            return new Cell
            {
                DataType = CellValues.InlineString,
                InlineString = new InlineString(
                    new Text(fecha.ToString("dd-MM-yyyy HH:mm:ss"))
                )
            };
        }

        if (valor is int || valor is long || valor is short ||
            valor is decimal || valor is double || valor is float)
        {
            return new Cell
            {
                DataType = CellValues.Number,
                CellValue = new CellValue(
                    Convert.ToString(valor, CultureInfo.InvariantCulture)
                )
            };
        }

        return new Cell
        {
            DataType = CellValues.InlineString,
            InlineString = new InlineString(
                new Text(valor.ToString())
            )
        };
    }

    private static void Descargar(MemoryStream ms, string nombreArchivo)
    {
        HttpResponse response = HttpContext.Current.Response;

        response.Clear();
        response.ClearHeaders();
        response.ClearContent();
        response.Buffer = true;

        response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        response.AddHeader("Content-Disposition", "attachment; filename=\"" + nombreArchivo + ".xlsx\"");
        response.AddHeader("Content-Length", ms.Length.ToString());
        ms.Position = 0;
        ms.CopyTo(response.OutputStream);
        response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}