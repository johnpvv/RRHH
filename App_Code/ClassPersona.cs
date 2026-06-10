using System;
using System.Data;

/// <summary>
/// Clase de los metodos para RRHH, sistema de personas
/// </summary>
public class ClassPersona
{
    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassPersona()
    {

    }

    public int ls_iduser { get; set; }
    public string ls_obs { get; set; }
    public string ls_desc { get; set; }
    public string mfFechaAnt { get; set; }
    public string ls_nomb { get; set; }
    public string ls_nomb_soc { get; set; }
    public string ls_pat { get; set; }
    public string ls_mat { get; set; }
    public string ls_rut { get; set; }
    public string ls_dv { get; set; }
    public string ls_dir { get; set; }
    public string ls_reg { get; set; }
    public string ls_elim { get; set; }


    public DataSet mfBuscarPersonas()
    {
        string lsSql = "";
        string lsWhe = "";
        DataSet ds;

        if (ls_nomb != "")
        {
            lsWhe += " AND P.NOMBRE LIKE '%" + ls_nomb.Trim() + "%'";
        }

        if (ls_pat != "")
        {
            lsWhe += " AND P.AP_PATERNO LIKE '%" + ls_pat.Trim() + "%'";
        }

        if (ls_mat != "")
        {
            lsWhe += " AND P.AP_MATERNO LIKE '%" + ls_mat.Trim() + "%'";
        }

        if (ls_rut != "")
        {
            lsWhe += " AND P.RUT = " + ls_rut;
        }
        //falta agregar estado
        lsSql =
            "SELECT " +
            "   P.IDPERSONA, " +
            "   P.RUT, " +
            "   P.DV, " +
            "   P.NOMBRE, " +
            "   ISNULL(P.NOMBRE_SOCIAL,'') NOMBRE_SOCIAL, " +
            "   P.AP_PATERNO, " +
            "   ISNULL(P.AP_MATERNO,'') AP_MATERNO, " +
            "   P.DIRECCION, " +
            "   P.IDREGION, " +
            "   P.IDCOMUNA, " +
            "   P.IDPREVISION, " +
            "   P.FONO1, " +
            "   P.FONO2, " +
            "   P.EMAIL, " +
            "   P.IDESTADO, " +
            "   P.F_H_CREA " +
            "FROM " + modConstantes.gsDbRH + "M_PERSONAS P " +
            "WHERE P.RUT > 0 " +
            lsWhe + " " +
            "ORDER BY P.AP_PATERNO, P.AP_MATERNO, P.NOMBRE";

        con = bd.fnGetConnRH();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
    public DataSet ConsultarID()
    {
        DataSet aoCod;

        string lsSql;

        //' Recupera registros.
        lsSql =
            "SELECT " +
            "P.IDPERSONA, " +
            "P.RUT, " +
            "P.DV, " +
            "P.NOMBRE, " +
            "ISNULL(P.NOMBRE_SOCIAL,'') NOMBRE_SOCIAL, " +
            "P.AP_PATERNO, " +
            "ISNULL(P.AP_MATERNO,'') AP_MATERNO, " +
            "P.DIRECCION, " +
            "P.FECHA_NACIMIENTO, " +
            "P.IDREGION, " +
            "P.IDCOMUNA, " +
            "P.IDPREVISION, " +
            "P.FONO1, " +
            "P.FONO2, " +
            "P.OBS_FONO1, " +
            "P.OBS_FONO2, " +
            "P.EMAIL, " +
            "P.IDESTADO, " +
            "P.F_H_CREA, " +
            "P.OBSERVACION " +
            "FROM " + modConstantes.gsDbRH + "M_PERSONAS P " +
            "WHERE	(P.RUT = " + ls_rut + " ) ";

        con = bd.fnGetConnRH();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfExistePersona()
    {
        string lsRet = "";
        con = bd.fnGetConnRH();
        try
        {
            string lsSql;
            lsSql = "SELECT P.RUT, " +
                    "FROM " + modConstantes.gsDbRH + "M_PERSONAS P" +
                    "WHERE	(P.RUT = '" + ls_rut + "' ) ";
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