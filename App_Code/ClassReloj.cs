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
    public string ls_serie { get; set; }
    public string ls_ip { get; set; }
    public string ls_descrip { get; set; }

    public DataSet mfBuscarMarcaciones()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        lsWhe = " AND UR.IDUSUARIO = " + ls_iduser;

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
            "M.IDSINCRONIZA, " +
            "M.CODIGO_EMP_RELOJ, " +
            "M.F_H_MARCA, " +
            "CASE " +
            "   WHEN M.TIPO_MARCA = 1 THEN 'ENTRADA' " +
            "   WHEN M.TIPO_MARCA = 2 THEN 'SALIDA' " +
            "   ELSE 'S/T' " +
            "END TIPO_MARCA, " +
            "M.F_H_CARGA, " +
            "UOP.DESCRIPCION AS CENTRO " +
            "FROM " + modConstantes.gsDbRH + "M_MARCACIONES M " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_SINCRONIZACION SC ON SC.IDSINCRONIZA = M.IDSINCRONIZA " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_RELOJES RE ON RE.IDRELOJ = SC.IDRELOJ " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_UNIDAD_OPERATIVA UOP ON UOP.CODUNIOP = RE.CODUNIOP " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_USR_RELOJ UR ON UR.IDRELOJ = RE.IDRELOJ AND UR.IDUSRELOJ = M.CODIGO_EMP_RELOJ " +
            "WHERE 1=1 " +
            lsWhe +
            " ORDER BY M.F_H_MARCA DESC";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }

    public DataSet mfBuscarRelojes()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        if (ls_descrip != "")
        {
            lsWhe += " AND RE.DESCRIPCION LIKE '%" + ls_descrip + "%'";
        }

        if (ls_ip != "")
        {
            lsWhe += " AND RE.IP LIKE '%" + ls_ip +"%'";
        }

        if (ls_serie != "")
        {
            lsWhe += " AND SERIE = " + ls_serie;
        }

        lsSql =
            "SELECT " +
            "RE.IDRELOJ, " +
            "RE.DESCRIPCION, " +
            "RE.IP, " +
            "RE.PUERTO, " +
            "RE.SERIE, " +
            "CASE " +
            "   WHEN RE.IDESTADO = 1 THEN 'ACTIVO' " +
            "   WHEN RE.IDESTADO = 3 THEN 'INACTIVO' " +
            "   ELSE 'S/E' " +
            "END ESTADO, " +
            "RE.F_H_CREACION, " +
            "UOP.DESCRIPCION AS CENTRO " +
            "FROM " + modConstantes.gsDbRH + "M_RELOJES RE " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_UNIDAD_OPERATIVA UOP ON UOP.CODUNIOP = RE.CODUNIOP " +
            "WHERE 1=1 " +
            lsWhe +
            " ORDER BY RE.IDRELOJ DESC";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
}
