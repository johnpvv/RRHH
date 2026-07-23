using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Descripción breve de ClassTurnos
/// </summary>
public class ClassTurnos
{
    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    public ClassTurnos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string ls_codigo { get; set; }
    public string ls_turno { get; set; }
    public string ls_descrip { get; set; }
    public string ls_user { get; set; }

    public DataSet mfBuscarTurnos()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        if (ls_descrip != "")
        {
            lsWhe += " AND T.DESCRIPCION LIKE '%" + ls_descrip + "%'";
        }

        if (ls_codigo != "")
        {
            lsWhe += " AND T.CODIGO = '" + ls_codigo + "'";
        }

        lsSql =
            "SELECT " +
            "T.IDTURNOS, " +
            "T.DESCRIPCION, " +
            "CASE " +
            "   WHEN T.IDESTADO = 1 THEN 'ACTIVO' " +
            "   WHEN T.IDESTADO = 3 THEN 'INACTIVO' " +
            "   ELSE 'S/E' " +
            "END ESTADO, " +
            "T.F_H_CREACION, " +
            "T.CODIGO " +
            "FROM " + modConstantes.gsDbRH + "M_TURNOS T " +
            "WHERE 1=1 " +
            lsWhe +
            " ORDER BY T.IDTURNOS DESC";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }

    public string mfCrearTurnos()
    {
        string lsSql;
        string lsRes = "";

        lsSql = "INSERT INTO " + modConstantes.gsDbRH + "M_TURNOS " +
                "(DESCRIPCION, CODIGO, IDESTADO, F_H_CREACION) " +
                "VALUES (" +
                "'" + ls_descrip + "', " +
                "'" + ls_codigo + "', " +
                "1, " +
                "GETDATE()" +
                ")";

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }
    public DataSet mfBuscarTurnosTrab()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        if (ls_user != "")
        {
            lsWhe += " AND TU.IDUSUARIO = " + ls_user;
        }

        lsSql =
            "SELECT " +
            "TU.IDTURNUS, " +
            "T.IDTURNOS, " +
            "T.DESCRIPCION AS TURNO, " +
            "D.IDDIA, " +
            "D.DESCRIPCION AS DIA, " +
            "H.IDHORA, " +
            "H.DESCRIPCION AS HORARIO, " +
            "H.HORA, " +
            "H.MINUTO, " +
            "H.HORA_INI, " +
            "H.HORA_FIN, " +
            "TU.F_H_CREACION, " +
            "CASE " +
            "   WHEN TU.IDESTADO = 1 THEN 'ACTIVO' " +
            "   WHEN TU.IDESTADO = 3 THEN 'INACTIVO' " +
            "   ELSE 'S/E' " +
            "END AS ESTADO " +
            "FROM " + modConstantes.gsDbRH + "M_TURNO_USUARIOS TU " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_TURNOS T " + "ON T.IDTURNOS = TU.IDTURNOS " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_TURNO_DIA TD " + "ON TD.IDTURNOS = T.IDTURNOS " +
            "INNER JOIN " + modConstantes.gsDbRH + "TG_DIAS D " + "ON D.IDDIA = TD.IDDIA " +
            "INNER JOIN " + modConstantes.gsDbRH + "TG_HORAS H " + "ON H.IDHORA = TD.IDHORA " +
            "WHERE 1=1 " +  lsWhe +
            " AND TU.IDESTADO = 1 " +
            " AND T.IDESTADO = 1 " +
            "ORDER BY D.IDDIA";
        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
}