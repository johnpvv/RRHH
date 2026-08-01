using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ClassHorarios
/// </summary>
public class ClassHorarios
{
    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    public ClassHorarios()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public string ls_user { get; set; }
    public string ls_idhora { get; set; }
    public string ls_descrip { get; set; }
    public string ls_hora { get; set; }
    public string ls_minuto { get; set; }
    public string ls_horaini { get; set; }
    public string ls_horafin { get; set; }
    public string ls_estado { get; set; }

    public DataSet mfBuscarHorarios()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        if (ls_descrip != "")
        {
            lsWhe += " AND H.DESCRIPCION LIKE '%" + ls_descrip + "%'";
        }

        lsSql =
            "SELECT " +
            "H.IDHORA, " +
            "H.DESCRIPCION, " +
            "CONVERT(VARCHAR,H.HORA) + 'h, ' + " +
            "CONVERT(VARCHAR,H.MINUTO) +'m' as DURACION, " +
            "H.HORA_INI, " +
            "H.HORA_FIN, " +
            "H.F_H_CREACION, " +
            "CASE " +
            " WHEN H.IDESTADO = 1 THEN 'ACTIVO' " +
            " WHEN H.IDESTADO = 3 THEN 'INACTIVO' " +
            " ELSE 'S/E' END ESTADO " +
            "FROM " + modConstantes.gsDbRH + "TG_HORAS H " +
            "WHERE 1=1 " +
            lsWhe +
            " ORDER BY H.IDHORA";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public DataSet mfConsultarHorario()
    {
        string lsSql;
        DataSet ds;

        lsSql =
            "SELECT " +
            "H.IDHORA, " +
            "H.DESCRIPCION, " +
            "CONVERT(VARCHAR,H.HORA) + 'h, ' + " +
            "CONVERT(VARCHAR,H.MINUTO) +'m' as DURACION, " +
            "H.HORA_INI, " +
            "H.HORA_FIN, " +
            "H.F_H_CREACION, " +
            "CASE " +
            " WHEN H.IDESTADO = 1 THEN 'ACTIVO' " +
            " WHEN H.IDESTADO = 3 THEN 'INACTIVO' " +
            " ELSE 'S/E' END ESTADO " +
            "FROM " + modConstantes.gsDbRH + "TG_HORAS H " +
            "WHERE IDHORA = " + ls_idhora;

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public DataSet mfBuscarHoras()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        lsSql = "SELECT " +
            "H.IDHORA," +
            "H.DESCRIPCION," +
            "H.HORA," +
            "H.MINUTO," +
            "H.HORA_INI," +
            "H.HORA_FIN " +
            "FROM " + modConstantes.gsDbRH + "TG_HORAS H " +
            "ORDER BY H.IDHORA";
        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    public DataSet mfBuscarHoraID()
    {
        string lsSql;
        DataSet ds;

        lsSql =
            "SELECT " +
            "IDHORA, " +
            "DESCRIPCION, " +
            "HORA," +
            "MINUTO," +
            "HORA_INI, " +
            "HORA_FIN " +
            "FROM " + modConstantes.gsDbRH + "TG_HORAS " +
            "WHERE IDHORA = " + ls_hora;

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public string mfCrearHorario()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "INSERT INTO " + modConstantes.gsDbRH + "TG_HORAS " +
            "(" +
            "DESCRIPCION," +
            "IDESTADO," +
            "F_H_CREACION," +
            "IDUSUARIO " +
            ") VALUES (" +
            "'" + ls_descrip + "'," +
            "1," +
            "GETDATE()," +
            " "+ ls_user +");" +
            "SELECT CAST(SCOPE_IDENTITY() AS INT);";

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }

    public string mfUpdateHorario()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "UPDATE " + modConstantes.gsDbRH + "TG_HORAS SET " +
            "DESCRIPCION = '" + ls_descrip + "', " +
            "IDESTADO = 1, " +
            "F_H_CREACION = GETDATE(), " +
            "IDUSUARIO =  " + ls_user + " " +
            "WHERE IDHORA = " + ls_idhora;
        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }

    public string mfUpdateHorarioDet()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "UPDATE " + modConstantes.gsDbRH + "TG_HORAS SET " +
            "HORA = " + ls_hora + ", " +
            "MINUTO = " + ls_minuto + ", " +
            "HORA_INI = '" + ls_horaini + "', " +
            "HORA_FIN = '" + ls_horafin + "', " +
            "IDUSUARIO =  " + ls_user + " "+
            "WHERE IDHORA = " + ls_idhora;

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();

        return lsRes;
    }
    public string mfEstadoHorario()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "UPDATE " + modConstantes.gsDbRH + "TG_HORAS " +
            "SET IDESTADO = " + ls_estado + ", " +
            "IDUSUARIO =  " + ls_user + " " +
            " WHERE IDHORA = " + ls_idhora;

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }
}