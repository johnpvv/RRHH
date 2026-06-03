using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassPeriodo
/// </summary>
public class ClassPeriodo
{
    // Declaracion de Variables
    public string ls_nomb { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassPeriodo()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfDDlPeriodo()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDPERIODO CODIGO, DESCRIPCION FROM " +
            modConstantes.gsDbAB + "M_PERIODO WHERE IDESTADO <> 3 ORDER BY ORDEN ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfFactorPeriodo(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select FACTOR  " +
                      "from " + modConstantes.gsDbAB + "M_PERIODO " +
                      "where " +
                       "IDPERIODO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "0";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }
}