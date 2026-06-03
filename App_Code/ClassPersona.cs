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

    public ClassPersona ()
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


    public string CrearPersona (string Rut,
                                string Dv,
                                string Rut_n,
                                string Dv_n,
                                string Idprevision,
                                string Idregion,
                                string Idcomuna,
                                string Nombre,
                                string NombreSocial,
                                string Paterno,
                                string Materno,
                                string Direccion,
                                string Tel1,
                                string Tel2,
                                string Obs1,
                                string Obs2,
                                string Mail1,
                                string Mail2,
                                bool Nuevo
                                )
    {
        string lsRet = "";
        string lsSql = "";
        string lstdel = " ";

        if (Nuevo == true)
        {
            lsSql = "INSERT INTO M_PERSONA( " +
                    "Rut, " +
                    "Dv, " +
                    "Idprevision, " +
                    "Idregion,  " +
                    "Idcomuna, " +
                    "Nombre,  " +
                    "NombreSocial,  " +
                    "AP_Paterno, " +
                    "AP_Materno,  " +
                    "FONO1_CONT,  " +
                    "FONO2_CONT,  " +
                    "OBS1_CONT,  " +
                    "OBS2_CONT,  " +
                    "MAIL_CONT,  " +
                    "MAIL2_CONT,  " +
                    "ISPROGRAMA,  " +
                    "Direccion) " +

            "VALUES ( " + Rut + ", " +
                    "'" + Dv + "', " +
                    Idprevision + ", " +
                    Idregion + ", " +
                    Idcomuna + ", " +
                    "'" + Nombre.ToUpper() + "', " +
                    "'" + NombreSocial.ToUpper() + "', " +
                    "'" + Paterno.ToUpper() + "', " +
                    "'" + Materno.ToUpper() + "', " +
                    "'" + Tel1 + "', " +
                    "'" + Tel2 + "', " +
                    "'" + Obs1 + "', " +
                    "'" + Obs2 + "', " +
                    "'" + Mail1 + "', " +
                    "'" + Mail2 + "', " +
                   // "" + pro + ", " +
                    "'" + Direccion + "')";
        }
        else
        {
            //string strut = "";



            //if(lscheck == "1") lstdel = " delete from M_PACIENTE where rut = " + Rut_n + "; ";

            //if (lscheck == "1") lstdel = " UPDATE M_PACIENTE SET IDESTADO = 3 where rut = " + Rut_n + "; ";
            //if (lscheck == "1") lstdel = " UPDATE M_RECETA SET RUT = " + Rut_n + ", dv ='" + Dv_n + "' where rut = '" + Rut + "'; ";

            //if ((Rut_n != "" && Dv_n != "") && lscheck != "1")
            //    strut = " Rut = " + Rut_n + ", Dv = '" + Dv_n + "', ";

            lsSql = "UPDATE M_PERSONA  " +
                 "SET	" +
                 //strut +
                "Idprevision =  " + Idprevision + ", " +
                "Idregion	=  " + Idregion + ", " +
                "Idcomuna	=  " + Idcomuna + ", " +
                "Nombre		=  '" + Nombre.ToUpper() + "', " +
                "NombreSocial		=  '" + NombreSocial.ToUpper() + "', " +
                "AP_Paterno	=  '" + Paterno.ToUpper() + "', " +
                "AP_Materno	=  '" + Materno.ToUpper() + "', " +
                "FONO1_CONT	=  '" + Tel1 + "', " +
                "FONO2_CONT	=  '" + Tel2 + "', " +
                "OBS1_CONT	=  '" + Obs1 + "', " +
                "OBS2_CONT	=  '" + Obs2 + "', " +
                "MAIL_CONT	=  '" + Mail1 + "', " +
                "MAIL2_CONT	=  '" + Mail2 + "', " +
                //"ISPROGRAMA	=  " + pro + ", " +
                "Direccion	=  '" + Direccion + "' " +
                "WHERE	RUT = " + Rut;
        }

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lstdel + lsSql);

        con.Close();
        return lsRet;
    }
    public DataSet mfBuscarPersona()
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
            "FROM "+modConstantes.gsDbRH + "M_PERSONAS P " +
            "WHERE P.RUT > 0 " +
            lsWhe + " " +
            "ORDER BY P.AP_PATERNO, P.AP_MATERNO, P.NOMBRE";

        con = bd.fnGetConnRH();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }


}