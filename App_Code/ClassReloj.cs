using System;
using System.Collections.Generic;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
    public string ls_codigo { get; set; }
    public string ls_nombre { get; set; }
    public string ls_iduserreloj { get; set; }
    public string ls_idreloj { get; set; }
    public string ls_iduserweb { get; set; }
    public string ls_unidad { get; set; }
    public string ls_rut { get; set; }

    public DataSet mfBuscarMarcaciones()//metodo inicial, no considerar
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

    public DataSet mfBuscarMarcasReloj()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        lsWhe = " AND UR.IDUSUARIO = " + ls_iduser;

        if (ls_mes != "" && ls_anio != "")
            lsWhe += " AND MR.[MONTH] = " + ls_mes + " AND MR.[YEAR] = " + ls_anio;

        lsSql = "SELECT " +
                "MR.IDMARCARELOJ, " +
                "MR.IDRELOJ, " +
                "MR.ENROLLNUMBER, " +
                "MR.VERIFYMODE, " +
                "MR.INOUTMODE, " +
                "CASE " +
                "WHEN MR.INOUTMODE = 0 THEN 'ENTRADA' " +
                "WHEN MR.INOUTMODE = 1 THEN 'SALIDA' " +
                "ELSE 'Otro' END AS TIPO_MARCA, " +
                "CASE DATEPART(WEEKDAY, DATETIMEFROMPARTS(MR.[YEAR],MR.[MONTH],MR.[DAY],MR.[HOUR],MR.[MINUTE],MR.[SECOND],0)) " +
                "WHEN 1 THEN 'Domingo' " +
                "WHEN 2 THEN 'Lunes' " +
                "WHEN 3 THEN 'Martes' " +
                "WHEN 4 THEN 'Miércoles' " +
                "WHEN 5 THEN 'Jueves' " +
                "WHEN 6 THEN 'Viernes' " +
                "WHEN 7 THEN 'Sábado' " +
                "END DIA, " +
                "DATETIMEFROMPARTS(MR.[YEAR],MR.[MONTH],MR.[DAY],MR.[HOUR],MR.[MINUTE],MR.[SECOND],0) AS F_H_MARCA, " +
                "MR.WORKCODE, " +
                "MR.F_H_CREACION, " +
                "MR.TIPO, " +
                "MR.IDUSR, " +
                "UOP.DESCRIPCION AS CENTRO, " +
                "MR.IDESTADO " +
                "FROM " + modConstantes.gsDbRH + "M_MARCA_RELOJ MR " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_USR_RELOJ UR ON UR.IDUSRELOJ = MR.ENROLLNUMBER " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_RELOJES RE ON RE.IDRELOJ = MR.IDRELOJ " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_UNIDAD_OPERATIVA UOP ON UOP.CODUNIOP = RE.CODUNIOP " +
                "WHERE 1=1 " + lsWhe +
                " ORDER BY MR.[DAY],MR.[HOUR],MR.[MINUTE],MR.[SECOND]";

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
            lsWhe += " AND RE.IP LIKE '%" + ls_ip + "%'";
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
    public DataSet mfBuscarTrabajadoresReloj()
    {
        string lsSql;
        string lsWhere = "";
        DataSet ds;

        lsWhere = " WHERE IDESTADO = 1 ";

        // Buscar por código del reloj
        if (!string.IsNullOrWhiteSpace(ls_codigo))
        {
            lsWhere += " AND IDUSERRELOJ LIKE '%" + ls_codigo.Trim().Replace("'", "''") + "%' ";
        }
        // Buscar por nombre
        if (!string.IsNullOrWhiteSpace(ls_nombre))
        {
            string nombreBuscar = ls_nombre.Trim().Replace("'", "''");
            lsWhere += " AND NOMBRE LIKE '%" + nombreBuscar + "%' ";
        }
        //buscar por reloj
        if (!string.IsNullOrWhiteSpace(ls_idreloj))
        {
            lsWhere += " AND IDRELOJ = " + ls_idreloj.Trim().Replace("'", "''") + " ";
        }
        lsSql =
            "SELECT " +
            "IDUSRPEND, " +
            "IDRELOJ, " +
            "F_H_CREACION, " +
            "IDESTADO, " +
            "IDUSERRELOJ, " +
            "NOMBRE " +
            "FROM " + modConstantes.gsDbRH + "M_USER_RELOJ_PENDIENTE " +
            lsWhere +
            "ORDER BY NOMBRE";
        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    #region Equivalencias
    public DataSet mfBuscaTrabRelojID()
    {
        string lsSql;
        DataSet ds;
        lsSql =
            "SELECT " +
            "IDUSRPEND, " +
            "IDRELOJ, " +
            "F_H_CREACION, " +
            "IDESTADO, " +
            "IDUSERRELOJ, " +
            "NOMBRE " +
            "FROM " + modConstantes.gsDbRH + "M_USER_RELOJ_PENDIENTE " +
            "WHERE IDUSRPEND = '" + ls_iduser + "' ";
        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    public string mfRegistrarEquivalencia()
    {
        string lsSql;
        SqlTransaction tx = null;
        if (string.IsNullOrWhiteSpace(ls_idreloj))
            return "Debe seleccionar el reloj.";
        if (string.IsNullOrWhiteSpace(ls_iduserreloj))
            return "Debe seleccionar un trabajador del reloj.";
        if (string.IsNullOrWhiteSpace(ls_iduser))
            return "Debe seleccionar un trabajador de RRHH.";

        int idReloj;
        int idUserReloj;
        int idUsuario;
        int idUsrWeb;

        if (!int.TryParse(ls_idreloj, out idReloj))
            return "El ID del reloj no es válido.";
        if (!int.TryParse(ls_iduserreloj, out idUserReloj))
            return "El código del trabajador del reloj debe ser numérico.";
        if (!int.TryParse(ls_iduser, out idUsuario))
            return "El ID del usuario RRHH no es válido.";
        if (!int.TryParse(ls_iduserweb, out idUsrWeb))
            return "El ID del usuario RRHH no es válido.";
        try
        {
            con = bd.fnGetConn();
            tx = con.BeginTransaction(IsolationLevel.Serializable);
            lsSql = "SELECT IDUSRELOJ, IDUSUARIO " +
                    "FROM " + modConstantes.gsDbRH + "M_USR_RELOJ WITH (UPDLOCK, HOLDLOCK) " +
                    "WHERE IDRELOJ = @IDRELOJ " +
                    "AND (IDUSRELOJ = @IDUSRELOJ OR IDUSUARIO = @IDUSUARIO) AND IDESTADO <> 3";//revisar si se elimina o solo cambio estado
            int relojExistente = 0;
            int usuarioExistente = 0;
            using (SqlCommand cmd = new SqlCommand(lsSql, con, tx))
            {
                cmd.Parameters.Add("@IDRELOJ", SqlDbType.Int).Value = idReloj;
                cmd.Parameters.Add("@IDUSRELOJ", SqlDbType.Int).Value = idUserReloj;
                cmd.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = idUsuario;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        relojExistente = Convert.ToInt32(dr["IDUSRELOJ"]);
                        usuarioExistente = Convert.ToInt32(dr["IDUSUARIO"]);
                    }
                }
            }
            if (relojExistente != 0)
            {
                tx.Rollback();
                con.Close();
                if (relojExistente == idUserReloj && usuarioExistente == idUsuario)
                    return "Esta equivalencia ya fue registrada anteriormente.";
                if (relojExistente == idUserReloj)
                    return "El trabajador seleccionado del reloj ya está relacionado con otro registro de RRHH.(ID RR.HH.: " + usuarioExistente + ")";
                return "El trabajador de RRHH ya está relacionado con otro código del reloj.(ID RELOJ: " + relojExistente + ")";
            }

            lsSql = "INSERT INTO " + modConstantes.gsDbRH +
                    "M_USR_RELOJ (IDRELOJ, IDUSRELOJ, IDUSUARIO, IDUSR, IDESTADO, FCREACION) " +
                    "VALUES (@IDRELOJ, @IDUSRELOJ, @IDUSUARIO, @IDUSR, 1, GETDATE())";

            using (SqlCommand cmd = new SqlCommand(lsSql, con, tx))
            {
                cmd.Parameters.Add("@IDRELOJ", SqlDbType.Int).Value = idReloj;
                cmd.Parameters.Add("@IDUSRELOJ", SqlDbType.Int).Value = idUserReloj;
                cmd.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = idUsuario;
                cmd.Parameters.Add("@IDUSR", SqlDbType.Int).Value = idUsrWeb;
                cmd.ExecuteNonQuery();
            }
            tx.Commit();
            con.Close();
            return "";
        }
        catch (SqlException ex)
        {
            try
            {
                if (tx != null)
                    tx.Rollback();
            }
            catch { }

            try
            {
                if (con != null)
                    con.Close();
            }
            catch { }

            if (ex.Number == 2601 || ex.Number == 2627)
                return "La equivalencia no se registró porque ya existe una relación para uno de los trabajadores.";

            return "No fue posible registrar la equivalencia: " + ex.Message;
        }
        catch (Exception ex)
        {
            try
            {
                if (tx != null)
                    tx.Rollback();
            }
            catch { }

            try
            {
                if (con != null)
                    con.Close();
            }
            catch { }

            return "No fue posible registrar la equivalencia: " + ex.Message;
        }
    }
    public DataSet mfBuscarRelojesCentro()
    {
        string lsSql;
        DataSet ds;

        lsSql = "SELECT IDRELOJ, DESCRIPCION, IP, PUERTO, SERIE " +
                "FROM " + modConstantes.gsDbRH + "M_RELOJES " +
                "WHERE CODUNIOP = " + ls_unidad + " " +
                "AND IDESTADO = 1 " +
                "ORDER BY DESCRIPCION";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    public DataSet mfBuscarDatosReloj()
    {
        string lsSql;
        DataSet ds;

        lsSql = "SELECT IDRELOJ, DESCRIPCION, IP, PUERTO, SERIE " +
                "FROM " + modConstantes.gsDbRH + "M_RELOJES " +
                "WHERE IDRELOJ = " + ls_idreloj + " " +
                "AND IDESTADO = 1";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public DataSet mfBuscarEquivalencias()
    {
        string lsSql;
        DataSet ds;
        string lsWhe = "";
        if (ls_codigo != "")
            lsWhe += " AND CONVERT(VARCHAR, UR.IDUSRELOJ) LIKE '%" + ls_codigo + "%' ";

        if (ls_nombre != "")
            lsWhe += " AND (UP.NOMBRE LIKE '%" + ls_nombre + "%' " +
                     "OR (U.NOMBRE + ' ' + ISNULL(U.AP_PATERNO,'') + ' ' + ISNULL(U.AP_MATERNO,'')) LIKE '%" + ls_nombre + "%') ";

        if (ls_rut != "")
            lsWhe += " AND (CONVERT(VARCHAR, U.RUT) + '-' + U.DV) LIKE '%" + ls_rut + "%' ";

        lsSql = "SELECT " +
                "UR.IDUSRRELOJ, " +
                "UR.IDRELOJ, " +
                "UR.IDUSRELOJ, " +
                "UR.IDUSUARIO, " +
                "R.DESCRIPCION AS NOMBRE_RELOJ, " +
                "UP.NOMBRE AS NOMBRE_TRAB_RELOJ, " +
                "U.RUT, " +
                "CONVERT(VARCHAR, U.RUT) + '-' + U.DV as RUT_C, " +
                "U.NOMBRE AS NOMBRE_RRHH, " +
                "UR.FCREACION AS FECHA " +
                "FROM " + modConstantes.gsDbRH + "M_USR_RELOJ UR " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_RELOJES R ON R.IDRELOJ = UR.IDRELOJ " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_USUARIOS U ON U.IDUSUARIO = UR.IDUSUARIO " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_USER_RELOJ_PENDIENTE UP ON UP.IDUSERRELOJ = UR.IDUSRELOJ " +
                "WHERE UR.IDESTADO = 1 " +
                "AND UR.IDRELOJ = " + ls_idreloj + " " +
                lsWhe +
                "ORDER BY R.DESCRIPCION, U.NOMBRE";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public string mfDesactivarEquivalencia()
    {
        string lsSql;

        lsSql = "UPDATE " + modConstantes.gsDbRH + "M_USR_RELOJ SET " +//revisar si se elimina o solo cambio estado
                "IDESTADO = 3, " +
                "F_H_ELIM = GETDATE(), " +
                "IDUSELIM = " + ls_iduserweb + " " +
                "WHERE IDUSRRELOJ = " + ls_iduserreloj + " " +
                "AND IDESTADO = 1";

        con = bd.fnGetConn();
        string lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();

        return lsRes;
    }
    public string mfDevuelveIDUserReloj()
    {
        string lsSql;

        lsSql = "SELECT IDUSUARIO " +
                "FROM " + modConstantes.gsDbRH + "M_USR_RELOJ" +
                "WHERE IDUSRELOJ = '" + ls_codigo + "'" +
                " AND IDESTADO <> 3 ";
        con = bd.fnGetConn();
        string lsRes = bd.ExecuteScalar(con, lsSql);
        con.Close();

        return lsRes;
    }
    #endregion
}
