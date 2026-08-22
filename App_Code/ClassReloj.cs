using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

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
                    "AND (IDUSRELOJ = @IDUSRELOJ OR IDUSUARIO = @IDUSUARIO)";
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
}
