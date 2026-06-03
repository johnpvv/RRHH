using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassPaciente
/// </summary>
public class ClassPaciente
{

    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    private static String msTB = "m_usuarios";
    private static String msTM = modConstantes.gsDbAB + msTB;
    private static String msPkM = "idusuario";
    private static String msTBU = "v_usys";
    private static String msTU = modConstantes.gsDbRH + msTBU;
    private static String msPkU = "rut";
    public string pro;
    public string rut;
    public string rut_n;
    public string idregion;
    public string idcomuna;
    public string idprevision;
    public string dv;
    public string dv_n;
    public string nombre;
    public string nombreSocial;
    public string paterno;
    public string materno;
    public string direccion;

    public string lsElim;

    public string lscheck { get; set; }


    public ClassPaciente()
    {
    }

    public DataSet mfBuscarPaciente(string lsRut, string lsNombre)
    {
        string lsSql = "";
        DataSet ds;

        if (lsNombre == "" && lsRut == "")
        {
            lsNombre = "%";
        }


        // Armar SQL
        string lsWhe = "";


        // Criterios propios de Comite.

        if (lsNombre != "")
        {
            lsWhe = lsWhe + "and P.NOMBRE like '%" + lsNombre + "%' ";
        }



        if (lsRut != "")
        {
            lsWhe = lsWhe + " and P.RUT = '" + lsRut + "'";
        }




        lsSql =
          "	SELECT	P.RUT,  " +
            "P.IDREGION,  " +
            "P.IDCOMUNA,  " +
            "P.IDPREVISION,  " +
            "P.DV,  " +
            "P.NOMBRE,  " +
            "ISNULL(P.NombreSocial,'') NOMBRE_SOCIAL,  " +
            "P.AP_PATERNO,  " +
            "P.AP_MATERNO,  " +
            "P.DIRECCION " +
            "FROM	M_PACIENTE		P  " +
            "WHERE	P.RUT > 0 " + lsWhe + " " +
            "order by P.NOMBRE";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }
    public DataSet mfBuscarNombrePaciente()
    {
        string lsSql = "";
        DataSet ds;

        if (nombre == "" && rut == "")
        {
            nombre = "%";
        }


        // Armar SQL
        string lsWhe = "";


        // Criterios propios de Comite.

        if (nombre != "")
        {
            lsWhe = lsWhe + "and P.NOMBRE like '%" + nombre + "%' ";
        }


        if (paterno != "")
        {
            lsWhe = lsWhe + "and P.AP_PATERNO like '%" + paterno + "%' ";
        }


        if (materno != "")
        {
            lsWhe = lsWhe + "and P.AP_MATERNO like '%" + materno + "%' ";
        }

        if (rut != "")
        {
            lsWhe = lsWhe + " and P.RUT = '" + rut + "'";
        }




        lsSql =
          "	SELECT	P.RUT,  " +
            "P.IDREGION,  " +
            "P.IDCOMUNA,  " +
            "P.IDPREVISION,  " +
            "P.DV,  " +
            "P.NOMBRE,  " +
            "ISNULL(P.NombreSocial,'') NOMBRE_SOCIAL,  " +
            "P.AP_PATERNO,  " +
            "P.AP_MATERNO,  " +
            "P.DIRECCION " +
            "FROM	M_PACIENTE		P  " +
            "WHERE	P.RUT > 0 " + lsWhe + " " +
            "order by P.NOMBRE";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();
        return ds;
    }

    public DataSet ConsultarID(string asRut)
    {
        DataSet aoCod;

        string lsSql;

        //' Recupera registros.
        lsSql =
          "	SELECT	P.RUT, ISNULL(ISPROGRAMA,0) ISPROGRAMA, " +
            "P.IDREGION,  " +
            "P.IDCOMUNA,  " +
            "P.IDPREVISION,  " +
            "P.DV,  " +
            "P.NOMBRE,  " +
            "ISNULL(P.NombreSocial,'') NOMBRE_SOCIAL,  " +
            "P.AP_PATERNO,  " +
            "P.AP_MATERNO,  " +
            "P.DIRECCION, " +
            "FONO1_CONT, FONO2_CONT, OBS1_CONT, OBS2_CONT,  MAIL_CONT, MAIL2_CONT " +
            "FROM	M_PACIENTE		P  " +
            "WHERE	(P.RUT = '" + asRut + "' ) ";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;

    }
    public DataSet ConsultaPacienteBuscar(string rut,
                                  string nombre)
    {

        DataSet aoDF;
        string lsSql;

        lsSql = "uspListaPacientesBuscar '" + rut + "','" + nombre + "'";

        con = bd.fnGetConn();
        aoDF = bd.Fill(con, lsSql);
        con.Close();
        return aoDF;
    }


    public DataSet ConsultaPaciente(string rut,
                                      string nombre)
    {

        DataSet aoDF;
        string lsSql;

        lsSql = "uspListaPacientes '" + rut + "','" + nombre + "'";

        con = bd.fnGetConn();
        aoDF = bd.Fill(con, lsSql);
        con.Close();
        return aoDF;
    }


    public string CrearPaciente(string Rut,
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

        //int nuevecito;

        //if (Nuevo == true)
        //    {nuevecito = 0;}
        //else
        //    {nuevecito = 1;}

        //lsSql = "spCreaPaciente '" + Rut + "','" +
        //                            Dv + "','" +
        //                            Idprevision + "','" +
        //                            Idregion + "','" +
        //                            Idcomuna + "','" +
        //                            Nombre + "','" +
        //                            Paterno + "','" +
        //                            Materno + "','" +
        //                            Direccion + "'," +
        //                            nuevecito;

        if (Nuevo == true)
        {
            lsSql = "INSERT INTO M_PACIENTE( " +
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
                    "" + pro + ", " +
                    "'" + Direccion + "')";
        }
        else
        {
            string strut = "";



            //if(lscheck == "1") lstdel = " delete from M_PACIENTE where rut = " + Rut_n + "; ";

            //if (lscheck == "1") lstdel = " UPDATE M_PACIENTE SET IDESTADO = 3 where rut = " + Rut_n + "; ";
            if (lscheck == "1") lstdel = " UPDATE M_RECETA SET RUT = " + Rut_n + ", dv ='" + Dv_n + "' where rut = '" + Rut + "'; ";

            if ((Rut_n != "" && Dv_n != "") && lscheck != "1")
                strut = " Rut = " + Rut_n + ", Dv = '" + Dv_n + "', ";

            lsSql = "UPDATE M_PACIENTE  " +
                 "SET	" +
                 strut +
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
                "ISPROGRAMA	=  " + pro + ", " +
                "Direccion	=  '" + Direccion + "' " +
                "WHERE	RUT = " + Rut;
        }

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lstdel + lsSql);

        con.Close();
        return lsRet;
    }


    public string UpdatePaciente(string Rut,
                            string Idregion,
                            string Idcomuna,
                            string Fono1,
                            string Obs1,
                            string Fono2,
                            string Obs2,
                            string Mail1,
                            string Mail2,
                            string Direccion
                            )
    {
        string lsRet = "";
        string lsSql = "";

        lsSql = "UPDATE M_PACIENTE  " +
             "SET " +
            "Idregion	    =  " + Idregion + ", " +
            "Idcomuna	    =  " + Idcomuna + ", " +
            "FONO1_CONT		=  '" + Fono1 + "', " +
            "FONO2_CONT		=  '" + Fono2 + "', " +
            "OBS1_CONT	    =  '" + Obs1 + "', " +
            "OBS2_CONT	    =  '" + Obs2 + "', " +
            "MAIL_CONT	    =  '" + Mail1 + "', " +
            "MAIL2_CONT	    =  '" + Mail2 + "', " +
            "Direccion	    =  '" + Direccion + "' " +
            "WHERE	RUT = " + Rut;


        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);

        con.Close();
        return lsRet;
    }

    public string EliminarPaciente(string rut)
    {
        string lsRet = "";
        string lsSql = "";

        lsSql = "spEliminaPaciente '" + rut + "'";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);

        con.Close();
        return lsRet;
    }


    public DataSet ConsultaPrevision(string id = "")
    {
        DataSet aoDF;
        string lsSql;

        lsSql = "SELECT IDPREVISION, DESCRIPCION FROM M_PREVISION ";

        con = bd.fnGetConn();
        aoDF = bd.Fill(con, lsSql);
        con.Close();
        return aoDF;
    }


    public DataSet ConsultaRegion()
    {
        DataSet aoDF;
        string lsSql;

        lsSql = "SELECT IDREGION, DESCRIPCION FROM TG_REGION ORDER BY DESCRIPCION ";

        con = bd.fnGetConn();
        aoDF = bd.Fill(con, lsSql);
        con.Close();
        return aoDF;
    }



    public DataSet ConsultaComuna(string idregion = "")
    {
        DataSet aoDF;
        string lsSql;

        // lsSql = "SELECT IDCOMUNA, DESCRIPCION FROM TG_COMUNA WHERE IDREGION = '" +
        //idregion + "' OR '"+ idregion + "'=''";

        lsSql = "uspListaComunas " + idregion;

        con = bd.fnGetConn();
        aoDF = bd.Fill(con, lsSql);
        con.Close();
        return aoDF;
    }

    public string UpdateEstado(string asId, string asIdEstado)
    {
        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "M_PACIENTE set IDESTADO = " + asIdEstado + " " +
                "where RUT = " + asId;

        lsRet = bd.EjecutarComando(con, lsSql);



        con.Close();
        return lsRet;

    }

    public string mfExistePaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.rut  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + " ";


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

    //AQUI FALTA LA BD DE FLORENCE, SOLO PONDRE UN RETURN EN 0 POR EL MOMENTO
    public string mfExistePacienteFlorence(string asRut)
    {
        string lsRet = "";

        //con = bd.fnGetConn();   //AQUI CONECTARSE A BD FLORENCE
        try
        {
            string lsSql;
            lsSql = ""; // QUERY A LA BD DE FLORENCE

            // lsRet = bd.ExecuteScalar(con, lsSql);

            //if (lsRet == "") lsRet = "0";
            //con.Close();

            lsRet = "0";
        }
        catch (Exception e)
        {
            //con.Close();
            lsRet = "0";
        }
        return lsRet;
    }

    public string mfDVPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.dv  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


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
    public string mfNombreSocialPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            //lsSql = "select case when isnull(p.nombreSocial,'') = '' then p.nombre else p.nombreSocial end " +
            //          "from " + modConstantes.gsDbAB + "m_paciente p " +
            //          "where " +
            //           "p.rut = " + asRut + "";
            lsSql = "select isnull(p.nombreSocial,'')  " +
          "from " + modConstantes.gsDbAB + "m_paciente p " +
          "where " +
           "p.rut = " + asRut + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfNombrePaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.nombre  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "S/N";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfPaternoPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.ap_paterno  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "S/P";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfMaternoPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.ap_materno  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "S/M";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfDireccionPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.direccion  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "S/D";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }


    public string mfIdRegionPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.idregion  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


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

    public string mfIdComunaPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.idcomuna  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


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

    public string mfIdPrevisionPaciente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select p.IDPREVISION  " +
                      "from " + modConstantes.gsDbAB + "m_paciente p " +
                      "where " +
                       "p.rut = " + asRut + "";


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


    public string mfGetFono1(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(FONO1_CONT,'') from " + modConstantes.gsDbAB + "m_paciente where rut = " + asIdUsr;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetObsFono1(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(OBS1_CONT,'') from " + modConstantes.gsDbAB + "m_paciente where rut = " + asIdUsr;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetFono2(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(FONO2_CONT,'') from " + modConstantes.gsDbAB + "m_paciente where rut = " + asIdUsr;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetObsFono2(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(OBS2_CONT,'') from " + modConstantes.gsDbAB + "m_paciente where rut = " + asIdUsr;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetMailCont(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(MAIL_CONT,'') from " + modConstantes.gsDbAB + "m_paciente where rut = " + asIdUsr;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetMail2Cont(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(MAIL2_CONT,'') from " + modConstantes.gsDbAB + "m_paciente where rut = " + asIdUsr;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetNombAdq(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select top 1 isnull(NombAdq,'') from " + modConstantes.gsDbAB + "M_RECETA where RutAdq = '" + asIdUsr + "'";

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfGetFonoAdq(string asIdUsr)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select top 1 isnull(FonoAdq,'') from " + modConstantes.gsDbAB + "M_RECETA where RutAdq = '" + asIdUsr + "'";

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "0";
        }
        return asNombre;
    }

    public string mfExisteAdquiriente(string asRut)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select count(1)  " +
                      "from " + modConstantes.gsDbAB + "M_RECETA p " +
                      "where " +
                       "p.RutAdq = '" + asRut + "'";


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