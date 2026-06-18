using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Descripción breve de ClassReloj
/// </summary>
public class ClassReloj
{
    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassReloj()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string ls_iduser { get; set; }
    public string ls_mes { get; set; }
    public string ls_anio { get; set; }

    public DataSet mfBuscarMarcaciones()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        lsWhe = " AND M.IDUSUARIO = " + ls_iduser;

        if (ls_mes != "")
        {
            lsWhe += " AND MONTH(M.F_H_MARCA) = " + ls_mes;
        }

        if (ls_anio != "")
        {
            lsWhe += " AND YEAR(M.F_H_MARCA) = " + ls_anio;
        }

        lsSql =
            "SELECT " +
            "M.IDMARCACION, " +
            "M.IDRELOJ, " +
            "M.IDUSUARIO, " +
            "M.CODIGO_EMP_RELOJ, " +
            "M.F_H_MARCA, " +
            "CASE " +
            "   WHEN M.TIPO_MARCA = 1 THEN 'ENTRADA' " +
            "   WHEN M.TIPO_MARCA = 2 THEN 'SALIDA' " +
            "   ELSE 'S/T' " +
            "END TIPO_MARCA, " +
            "M.F_H_CARGA " +
            "FROM " + modConstantes.gsDbRH + "M_MARCACIONES M " +
            "WHERE 1=1 " +
            lsWhe +
            " ORDER BY M.F_H_MARCA DESC";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
}
