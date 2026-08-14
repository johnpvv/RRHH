using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción: Permite crear archivos excel, y subir archivos al sistema.
/// </summary>
public class ClassExcel
{
    ClassUsuarios usr = new ClassUsuarios();
    ClassTurnos tur = new ClassTurnos();
    public ClassExcel()
    {
        //Agregar aquí la lógica del constructor
    }

    public string ls_error { get; set; }

    #region General
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
                // PROCESAR LOS ENCABEZADOS
                if (primera)
                {
                    int cantidadColumnas = ObtenerCantidadColumnas(fila);
                    for (int i = 0; i < cantidadColumnas; i++)
                    {
                        dt.Columns.Add("COL_" + i);
                    }

                    foreach (Cell celda in fila.Elements<Cell>())
                    {
                        int indice = GetIndexColumna(celda.CellReference);
                        if (indice < dt.Columns.Count)
                        {
                            string nombreColumna = LeerCelda(celda, wbPart).Trim();

                            if (string.IsNullOrEmpty(nombreColumna))
                            {
                                nombreColumna = "COL_" + indice;
                            }
                            // Evitar nombres duplicados
                            string nombreOriginal = nombreColumna;
                            int contador = 1;
                            while (dt.Columns.Contains(nombreColumna))
                            {
                                nombreColumna = nombreOriginal + "_" + contador;
                                contador++;
                            }
                            dt.Columns[indice].ColumnName = nombreColumna;
                        }
                    }
                    primera = false;
                }
                else
                {
                    // PROCESAR LS DATOS
                    DataRow dr = dt.NewRow();
                    foreach (Cell celda in fila.Elements<Cell>())
                    {
                        int indice = GetIndexColumna(celda.CellReference);
                        if (indice < dt.Columns.Count)
                        {
                            dr[indice] = LeerCelda(celda, wbPart);
                        }
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
                return wbPart.SharedStringTablePart.SharedStringTable.ChildElements[int.Parse(valor)].InnerText;
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
    private int ObtenerCantidadColumnas(Row fila)
    {
        int mayor = 0;
        foreach (Cell celda in fila.Elements<Cell>())
        {
            int indice = GetIndexColumna(celda.CellReference);
            if (indice > mayor)
                mayor = indice;
        }
        return mayor + 1;
    }
    #endregion

    #region Validar Excel Turnos
    public List<string> ValidarDatosTurnos(DataTable dt)
    {
        List<string> errores = new List<string>();

        if (dt == null || dt.Rows.Count == 0)
        {
            errores.Add("El archivo Excel no contiene registros.");
            return errores;
        }
        dt.Columns.Add("RUT_NUM");//agregar para obtener solo el rut parte numeros                                  
        HashSet<int> rutsProcesados = new HashSet<int>();// Para controlar RUT duplicados dentro del Excel        
        Dictionary<int, int> filaRut = new Dictionary<int, int>();// Para saber en qué fila apareció originalmente
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow fila = dt.Rows[i];
            int filaExcel = i + 2;// +2 porque la fila 1 corresponde al encabezado titulo
            string rut = fila["RUT"] == DBNull.Value ? "" : fila["RUT"].ToString().Trim();
            string idTurno = fila["CODIGO_TURNO"] == DBNull.Value ? "" : fila["CODIGO_TURNO"].ToString().Trim();

            // VALIDAR RUT VACIO, LUEGO SI EXISTE
            if (string.IsNullOrWhiteSpace(rut))
            {
                errores.Add("Fila " + filaExcel + ": RUT vacío.");
            }
            else
            {
                string rutVal = usr.ValidarRut(rut);
                int rutNum;
                if (!int.TryParse(rutVal, out rutNum))
                {
                    errores.Add("Fila " + filaExcel + ": " + rutVal + "");
                }
                else
                {
                    fila["RUT_NUM"] = rutVal;
                    if (rutsProcesados.Contains(rutNum))
                    {
                        int filaOriginal = filaRut[rutNum];
                        errores.Add("Fila " + filaExcel + ": RUT " + rut + " está duplicado, en la fila: " + filaOriginal + ".");
                    }
                    else
                    {
                        rutsProcesados.Add(rutNum);
                        filaRut.Add(rutNum, filaExcel);
                    }
                }
            }

            //VALIDAR TURNO VACIO, LUEGO SI EXISTE
            if (string.IsNullOrWhiteSpace(idTurno))
            {
                errores.Add("Fila " + filaExcel + ": CODIGO_TURNO vacío.");
            }
            else
            {
                int id;
                if (!int.TryParse(idTurno, out id))
                {
                    errores.Add("Fila " + filaExcel + ": CODIGO_TURNO es inválido.");
                }
            }
        }
        return errores;
    }
    public List<string> ValidarDatosBD(DataTable dt)
    {
        List<string> erroresBD = new List<string>();

        if (dt == null || dt.Rows.Count == 0)
        {
            erroresBD.Add("El archivo Excel no contiene registros.");
            return erroresBD;
        }
        dt.Columns.Add("IDUSUARIO");//agregar para obtener el id del user
        dt.Columns.Add("IDTURNOS");//agregar para obtener el id del turno
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow fila = dt.Rows[i];
            int filaExcel = i + 2;
            string rut = fila["RUT_NUM"].ToString().Trim();
            string codTurno = fila["CODIGO_TURNO"].ToString().Trim();
            // VALIDAR RUT SI EXISTE en BD
            usr.ls_rut = rut;
            string rutID = usr.mfDevuelveID();
            if (rutID == "0")
            {
                erroresBD.Add("Fila " + filaExcel + ": el RUT: " + fila["RUT"].ToString() + " no existe en el sistema.");
            }
            else
            {
                fila["IDUSUARIO"] = rutID;
            }
            //VALIDAR TURNO SI EXISTE BD
            tur.ls_codigo = codTurno;
            string idTurno = tur.mfDevuelveIDTurno();
            if (idTurno == "")
            {
                erroresBD.Add("Fila " + filaExcel + ": CODIGO_TURNO no existe, o es inválido.");
            }
            else
            {
                fila["IDTURNOS"] = idTurno;
            }
        }
        return erroresBD;
    }
    #endregion
}