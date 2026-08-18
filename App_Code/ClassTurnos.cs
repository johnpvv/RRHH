using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public string ls_hora { get; set; }
    public string ls_idturnodia { get; set; }
    public string ls_idturno { get; set; }
    public string ls_iddia { get; set; }
    public string ls_idhora { get; set; }
    public string ls_iduselim { get; set; }
    public string ls_rut { get; set; }
    public string ls_nombre { get; set; }
    public string ls_fer { get; set; }
    public string ls_tipo { get; set; }

    public DataSet mfGenerarMeses()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("MES");

        dt.Columns.Add("IDMES", typeof(int));
        dt.Columns.Add("MES", typeof(string));
        dt.Rows.Add(1, "Enero");
        dt.Rows.Add(2, "Febrero");
        dt.Rows.Add(3, "Marzo");
        dt.Rows.Add(4, "Abril");
        dt.Rows.Add(5, "Mayo");
        dt.Rows.Add(6, "Junio");
        dt.Rows.Add(7, "Julio");
        dt.Rows.Add(8, "Agosto");
        dt.Rows.Add(9, "Septiembre");
        dt.Rows.Add(10, "Octubre");
        dt.Rows.Add(11, "Noviembre");
        dt.Rows.Add(12, "Diciembre");
        ds.Tables.Add(dt);
        return ds;
    }
    public DataSet mfGenerarAnios()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable("ANIO");

        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("ANIO", typeof(string));

        for (int i = DateTime.Now.Year; i >= 2020; i--)
        {
            dt.Rows.Add(i.ToString(), i.ToString());
        }

        ds.Tables.Add(dt);
        return ds;
    }

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

        if (ls_turno != "")
        {
            lsWhe += " AND T.IDTURNOS = '" + ls_turno + "'";
        }

        lsSql =
            "SELECT " +
            "T.IDTURNOS, " +
            "T.DESCRIPCION, " +
            "ISNULL(T.FERIADOS,0) FERIADOS, " +
            "CASE " +
            "   WHEN T.IDESTADO = 1 THEN 'ACTIVO' " +
            "   WHEN T.IDESTADO = 3 THEN 'INACTIVO' " +
            "   ELSE 'S/E' " +
            "END ESTADO, " +
            "T.F_H_CREACION, " +
            "T.CODIGO, " +
            "ISNULL(T.TIPO_TURNO,0) AS TIPO_TURNO " +
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
                "(DESCRIPCION, CODIGO, IDESTADO, F_H_CREACION, FERIADOS, TIPO_TURNO) " +
                "VALUES (" +
                "'" + ls_descrip + "', " +
                "'" + ls_codigo + "', " +
                "1, " +
                "GETDATE()," +
                " " + ls_fer + ", " +
                " " + ls_tipo +");" +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }
    public string mfUpdateTurnos()
    {
        string lsSql;
        string lsRes = "";

        lsSql = "UPDATE " + modConstantes.gsDbRH + "M_TURNOS SET " +
                "DESCRIPCION = '" + ls_descrip + "', " +
                "CODIGO = '" + ls_codigo + "', " +
                "FERIADOS = " + ls_fer + ", " +
                "TIPO_TURNO = " + ls_tipo + " " +
                "WHERE IDTURNOS = " + ls_idturno;

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
            "ISNULL(T.FERIADOS,0) FERIADOS, " +
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
            "WHERE 1=1 " + lsWhe +
            " AND TU.IDESTADO = 1 " +
            " AND T.IDESTADO = 1 " +
            "ORDER BY D.IDDIA";
        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    public DataSet mfBuscarTurnoDia()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        lsSql = "SELECT " +
            "D.IDDIA," +
            "D.DESCRIPCION AS DIA," +
            "ISNULL(TD.IDTURNODIA,0) IDTURNODIA ," +
            "TD.IDTURNOS," +
            "TD.IDHORA IDHORA, " +
            "ISNULL(H.HORA_INI, '') HORA_INI, " +
            "ISNULL(H.HORA_FIN, '') HORA_FIN, " +
            "H.DESCRIPCION, " +
            "H.HORA, " +
            "H.MINUTO " +
            "FROM " + modConstantes.gsDbRH + "TG_DIAS D " +
            "LEFT JOIN " + modConstantes.gsDbRH + "M_TURNO_DIA TD ON TD.IDDIA = D.IDDIA AND TD.IDTURNOS = " + ls_turno + " AND TD.IDESTADO <>3 " +
            "LEFT JOIN " + modConstantes.gsDbRH + "TG_HORAS H ON H.IDHORA = TD.IDHORA " +
            "ORDER BY D.IDDIA";
        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }

    public string mfGuardarDiaTurno(bool trabaja)
    {
        string lsSql = "";
        string lsRes = "";

        if (trabaja)
        {
            if (ls_idturnodia == "" || ls_idturnodia == "0")
            {
                lsSql =
                "INSERT INTO " + modConstantes.gsDbRH + "M_TURNO_DIA " +
                "(IDTURNOS, IDDIA, IDHORA, F_H_CREACION, IDESTADO) " +
                "VALUES (" +
                ls_idturno + "," +
                ls_iddia + "," +
                ls_idhora + "," +
                "GETDATE()," +
                "1)";
            }
            else
            {
                lsSql =
                "UPDATE " + modConstantes.gsDbRH + "M_TURNO_DIA SET " +
                "IDHORA = " + ls_idhora + ", " +
                "F_H_CREACION = GETDATE() " +
                "WHERE IDTURNODIA = " + ls_idturnodia;
            }
        }
        else
        {
            if (ls_idturnodia != "" && ls_idturnodia != "0")
            {
                lsSql =
                "DELETE FROM " + modConstantes.gsDbRH +
                "M_TURNO_DIA " +
                "WHERE IDTURNODIA = " + ls_idturnodia;
            }
        }
        if (lsSql != "")
        {
            con = bd.fnGetConn();
            lsRes = bd.ExecuteScalar(con, lsSql);
            con.Close();
        }
        return lsRes;
    }
    public DataSet mfBuscarUserDisp()
    {
        string lsSql;
        DataSet ds;
        string lsWhe = "";

        if (ls_rut != "")
        {
            lsWhe += " AND P.RUT = '" + ls_rut + "'";
        }

        if (ls_nombre != "")
        {
            lsWhe += " AND (P.NOMBRE LIKE '%" + ls_nombre + "%' OR " +
                "P.AP_PATERNO LIKE '%" + ls_nombre + "%' OR " +
                "P.AP_MATERNO LIKE '%" + ls_nombre + "%')";
        }

        lsSql =
            "SELECT " +
            "P.IDUSUARIO, " +
            "P.RUT, " +
            "P.DV, " +
            "CONVERT(VARCHAR,P.RUT) + '-' + P.DV as RUT_C, " +
            "P.NOMBRE + ' ' + " +
            "ISNULL(P.AP_PATERNO,'') + ' ' + " +
            "ISNULL(P.AP_MATERNO,'') as NOMBRE " +
            "FROM " + modConstantes.gsDbRH + "M_USUARIOS P " +
            "WHERE P.IDESTADO = 1 " +
            "AND NOT EXISTS ( " +
            "   SELECT 1 " +
            "   FROM " + modConstantes.gsDbRH + "M_TURNO_USUARIOS TU " +
            "   WHERE TU.IDUSUARIO = P.IDUSUARIO " +
            //"   AND TU.IDTURNOS = " + ls_idturno +//si se desea solo user de turno especifico
            "   AND TU.IDESTADO = 1 ) " +
            " " + lsWhe +
            "ORDER BY P.NOMBRE, P.AP_PATERNO, P.AP_MATERNO";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public DataSet mfBuscarUserTurno()
    {
        string lsSql;
        DataSet ds;
        string lsWhe = "";

        if (ls_rut != "")
        {
            lsWhe += " AND P.RUT = '" + ls_rut + "'";
        }

        if (ls_nombre != "")
        {
            lsWhe += " AND (P.NOMBRE LIKE '%" + ls_nombre + "%' OR " +
                "P.AP_PATERNO LIKE '%" + ls_nombre + "%' OR " +
                "P.AP_MATERNO LIKE '%" + ls_nombre + "%')";
        }

        lsSql =
            "SELECT " +
            "TU.IDTURNUS, " +
            "P.IDUSUARIO, " +
            "P.RUT, " +
            "P.DV, " +
            "P.NOMBRE + ' ' + " +
            "ISNULL(P.AP_PATERNO,'') + ' ' + " +
            "ISNULL(P.AP_MATERNO,'') as NOMBRE " +
            "FROM " + modConstantes.gsDbRH + "M_TURNO_USUARIOS TU " +
            "INNER JOIN " + modConstantes.gsDbRH + "M_USUARIOS P " +
            "ON TU.IDUSUARIO = P.IDUSUARIO " +
            "WHERE TU.IDTURNOS = " + ls_idturno +
            " " + lsWhe +
            " AND TU.IDESTADO = 1 " +
            "ORDER BY P.NOMBRE, P.AP_PATERNO, P.AP_MATERNO";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    public string mfAgregarUserTurno()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "IF EXISTS ( " +
            "SELECT 1 FROM " + modConstantes.gsDbRH + "M_TURNO_USUARIOS " +
            "WHERE IDUSUARIO = " + ls_user + " AND IDTURNOS = " + ls_idturno +" ) " +
            "BEGIN " +
            "UPDATE " + modConstantes.gsDbRH + "M_TURNO_USUARIOS SET " +
            "IDESTADO = 1, " +
            "F_H_ELIM = NULL, " +
            "IDUSELIM = NULL, " +
            "IDTURNOS = " + ls_idturno + ", " +
            "OBSERVACION = 'Re-Asignado' " +
            "WHERE IDUSUARIO = " + ls_user +
            " END " +
            "ELSE " +
            "BEGIN " +
            "INSERT INTO " + modConstantes.gsDbRH + "M_TURNO_USUARIOS " +
            "(IDTURNOS,IDUSUARIO,F_H_CREACION,IDESTADO) VALUES (" +
            ls_idturno + "," +
            ls_user + "," +
            "GETDATE(),1)" +
            " END";

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }
    public string mfQuitarUserTurno()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "UPDATE " + modConstantes.gsDbRH + "M_TURNO_USUARIOS SET " +
            "IDESTADO = 3, " +
            "F_H_ELIM = GETDATE(), " +
            "IDUSELIM = " + ls_iduselim +
            " WHERE IDTURNOS = " + ls_idturno +
            " AND IDTURNUS = " + ls_user;

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();

        return lsRes;
    }

    public string mfDevuelveIDTurno()
    {
        string lsSql;
        string lsRes = "";

        lsSql =
            "SELECT IDTURNOS " +
            "FROM " + modConstantes.gsDbRH + "M_TURNOS " +
            "WHERE CODIGO = '" + ls_codigo + "' " +
            "AND IDESTADO <> 3 ";

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRes;
    }
    public string mfCargaTurnos(DataTable dt)
    {
        string lsRes = "";
        try
        {
            con = bd.fnGetConn();
            foreach (DataRow fila in dt.Rows)
            {
                ls_user = fila["IDUSUARIO"].ToString();
                ls_idturno = fila["IDTURNOS"].ToString();
                // Eliminar turno activo, e insertar turno nuevo
                lsRes = mfAddUserTurnoExcel();
                if (lsRes != "")
                    return lsRes;
            }
            con.Close();
        }
        catch (Exception ex)
        {
            lsRes = ex.Message;
        }
        return lsRes;
    }
    public string mfAddUserTurnoExcel()
    {
        string lsSql;
        string lsRes;

        lsSql =
            "UPDATE " + modConstantes.gsDbRH + "M_TURNO_USUARIOS SET " +
            "IDESTADO = 3, " +
            "F_H_ELIM = GETDATE(), " +
            "IDUSELIM = " + ls_iduselim + ", " +
            "OBSERVACION = 'Eliminado por carga Excel' " +
            "WHERE IDUSUARIO = " + ls_user + " " +
            "AND IDESTADO = 1; " +

            "INSERT INTO " + modConstantes.gsDbRH + "M_TURNO_USUARIOS " +
            "(IDTURNOS, IDUSUARIO, F_H_CREACION, IDESTADO, OBSERVACION) " +
            "VALUES (" +
            ls_idturno + ", " +
            ls_user + ", " +
            "GETDATE(), " +
            "1, " +
            "'Carga desde Excel'" +
            ");";

        con = bd.fnGetConn();
        lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();

        return lsRes;
    }

    #region turno mes
    public string mfGuardarDetalleMes(DataSet dsDetalle)
    {
        string lsSql = "";
        string lsRes = "";

        if (dsDetalle == null || dsDetalle.Tables.Count == 0 || dsDetalle.Tables[0].Rows.Count == 0)
        {
            return "No existen detalles para guardar.";
        }
        lsSql =
            "DELETE FROM " + modConstantes.gsDbRH + "M_TURNO_DIA " +
            "WHERE IDTURNOS = " + ls_idturno + " " +
            "AND FECHA IS NOT NULL; ";

        foreach (DataRow dr in dsDetalle.Tables[0].Rows)
        {
            int idDia = Convert.ToInt32(dr["IDDIA"]);
            int idHora = Convert.ToInt32(dr["IDHORA"]);
            DateTime fecha = Convert.ToDateTime(dr["FECHA"]);

            lsSql +=
                "INSERT INTO " + modConstantes.gsDbRH + "M_TURNO_DIA " +
                "(IDTURNOS, IDDIA, IDHORA, F_H_CREACION, FECHA) VALUES (" +
                ls_idturno + ", " +
                idDia + ", " +
                idHora + ", " +
                "GETDATE(), " +
                "'" + fecha.ToString("yyyyMMdd") + "'); ";
        }
        con = bd.fnGetConn();
        SqlTransaction tran = con.BeginTransaction();
        try
        {
            SqlCommand cmd = new SqlCommand(lsSql, con, tran);
            cmd.ExecuteNonQuery();
            tran.Commit();
            lsRes = "";
        }
        catch (Exception ex)
        {
            try
            {
                tran.Rollback();
            }
            catch
            {
            }
            lsRes = ex.Message;
        }
        finally
        {
            con.Close();
        }
        return lsRes;
    }



    #endregion
}