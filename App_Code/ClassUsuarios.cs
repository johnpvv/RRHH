using System;
using System.Data;

/// <summary>
/// Clase de los metodos para RRHH, sistema de personas
/// </summary>
public class ClassUsuarios
{
    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassUsuarios()
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
    public string ls_idcomuna { get; set; }
    public string ls_idregion { get; set; }
    public string ls_idprevision { get; set; }
    public string ls_tel1 { get; set; }
    public string ls_tel2 { get; set; }
    public string ls_obs1 { get; set; }
    public string ls_obs2 { get; set; }
    public string ls_mail { get; set; }
    public string ls_fnac { get; set; }
    public string ls_sexo { get; set; }
    public string ls_estciv { get; set; }

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
            "   P.IDUSUARIO, " +
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
            "FROM " + modConstantes.gsDbRH + "M_USUARIOS_P P " +
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
            "P.IDUSUARIO, " +
            "P.RUT, " +
            "P.DV, " +
            "P.NOMBRE, " +
            "ISNULL(P.NOMBRE_SOCIAL,'') NOMBRE_SOCIAL, " +
            "P.AP_PATERNO, " +
            "ISNULL(P.AP_MATERNO,'') AP_MATERNO, " +
            "P.DIRECCION, " +
            "P.FECHA_NACIMIENTO, " +
            "P.SEXO, " +
            "P.EST_CIVIL, " +
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
            "FROM " + modConstantes.gsDbRH + "M_USUARIOS_P P " +
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
            lsSql = "SELECT P.RUT " +
                    "FROM " + modConstantes.gsDbRH + "M_USUARIOS_P P " +
                    "WHERE (P.RUT = " + ls_rut + " ) ";
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
    public string CrearUsuario(bool Nuevo)
    {
        string lsRet = "";
        string lsSql = "";

        if (Nuevo == true)
        {
            lsSql = "INSERT INTO M_USUARIOS_P( " +
                    "Rut, " +
                    "Dv, " +
                    "Idprevision, " +
                    "Idregion,  " +
                    "Idcomuna, " +
                    "Nombre,  " +
                    "Nombre_Social,  " +
                    "AP_Paterno, " +
                    "AP_Materno,  " +                    
                    "FECHA_NACIMIENTO,  " +
                    "SEXO,  " +
                    "FONO1,  " +
                    "FONO2,  " +
                    "OBS_FONO1,  " +
                    "OBS_FONO2,  " +
                    "EMAIL,  " +
                    "OBSERVACION,  " +
                    "IDUSERCREA,  " +
                    "F_H_MOD, " +
                    "Direccion) " +

            "VALUES ( " + ls_rut + ", " +
                    "'" + ls_dv + "', " +
                    ls_idprevision + ", " +
                    ls_idregion + ", " +
                    ls_idcomuna + ", " +
                    "'" + ls_nomb.ToUpper() + "', " +
                    "'" + ls_nomb_soc.ToUpper() + "', " +
                    "'" + ls_pat.ToUpper() + "', " +
                    "'" + ls_mat.ToUpper() + "', " +
                    "'" + ls_fnac + "', " +
                    "'" + ls_sexo + "', " +
                    "'" + ls_tel1 + "', " +
                    "'" + ls_tel2 + "', " +
                    "'" + ls_obs1 + "', " +
                    "'" + ls_obs2 + "', " +
                    "'" + ls_mail + "', " +
                    "'" + ls_obs + "', " +
                    " " + ls_iduser + ", " +
                    "NULL', " +
                    "'" + ls_dir + "')";
        }
        else
        {

            lsSql = "UPDATE M_USUARIOS_P " +
                    "SET	" +
                    "Idprevision =  " + ls_idprevision + ", " +
                    "Idregion	=  " + ls_idregion + ", " +
                    "Idcomuna	=  " + ls_idcomuna + ", " +
                    "Nombre		=  '" + ls_nomb.ToUpper() + "', " +
                    "Nombre_Social	=  '" + ls_nomb_soc.ToUpper() + "', " +
                    "AP_Paterno	=  '" + ls_pat.ToUpper() + "', " +
                    "AP_Materno	=  '" + ls_mat.ToUpper() + "', " +
                    "FECHA_NACIMIENTO	=  '" + ls_fnac + "', " +
                    "SEXO	=  '" + ls_sexo + "', " +
                    "FONO1	=  '" + ls_tel1 + "', " +
                    "FONO2	=  '" + ls_tel2 + "', " +
                    "OBS_FONO1	=  '" + ls_obs1 + "', " +
                    "OBS_FONO2	=  '" + ls_obs2 + "', " +
                    "EMAIL	=  '" + ls_mail + "', " +
                    "OBSERVACION	=  '" + ls_obs + "', " +
                    "Direccion	=  '" + ls_dir + "', " +
                    "IDUSERMOD	=  " + ls_iduser + ", " +
                    "F_H_MOD   =   GETDATE() " +
                    "WHERE RUT = " + ls_rut;
        }
        con = bd.fnGetConnRH();
        lsRet = bd.EjecutarComando(con, lsSql);

        con.Close();
        return lsRet;
    }
}