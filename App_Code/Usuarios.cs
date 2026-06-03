using System;
using System.Data;

/// <summary>
/// Descripción breve de Usuarios
/// </summary>
public class Usuarios
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

    public Usuarios()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    #region Usuario

    public String fnValidaUsrApp(String asAplicacion, string gUsr, string asCodSistema)
    {
        string lsRet = "";
        modFunciones mod = new modFunciones();
        try
        {

            //Si no se indicó aplicación, sale.
            if (asAplicacion == "") return lsRet = "";

            //Recupera Contexto.
            string lsAux;
            string lsErr;
            String asPermiso;


            lsErr = "Debe iniciar sesión de usuario.";

            //Valida sesión Activa.

            lsAux = gUsr.Trim().ToLower();

            if (lsAux == "")
                return lsErr + "(1) ";
            //else if (Int32.Parse(lsAux) > 0 )
            //    return lsErr + "(2) " + lsAux;
            else if (mod.fnLong(lsAux) <= 0)
                return lsErr + " (3) " + lsAux;

            //Determina Permiso del Usuario.

            asPermiso = fnPermisoUsuarioApp(asCodSistema, asAplicacion, gUsr);



            if (lsRet != "") return lsRet;


            lsRet = asPermiso;

            if (asPermiso != "M" && asPermiso != "L")
            {
                lsRet = "Error en Permiso";
                lsRet = "Ret: " + lsRet + ". gSys: " + asCodSistema + ".gUsr: " + gUsr + ".asPermiso: " + asPermiso;
            }
        }
        catch (Exception ex)
        {
            lsRet = ex.Message;
        }

        return lsRet;
    }

    public String fnPermisoUsuarioApp(String asCodSistema, String asAplicacion, String asCodUsuario)
    {
        String Permiso = "";



        try
        {
            Permiso = jaxValidarPermisoApp(asCodUsuario, asAplicacion, asCodSistema);
        }
        catch (Exception ex)
        {
            Permiso = ex.Message;
        }


        return Permiso;
    }


    public string f_permiso_usuario(string as_funcion, string gl_usuario)
    {
        /*
        FUNCION      : Retorna atributos del usuario activo sobre una función.
        ARGUMENTO    : as_funcion  (string) Función consultada.
        RETORNO      : 'M' Permiso de modificación.
                       'L' Permiso de lectura.
					        'N' Ningún tipo de permiso.
					        'E' La función no existe.
        REVISION     : 04/10/2014 wpizarror.
        */

        string ls_permiso = string.Empty;
        int ll_funcion, ll_rol;

        // Recupera código de función a consultar.

        string lsSql = "select idapp " +
        "from   tg_apps " +
        "where  descripcion = " + as_funcion;

        con = bd.fnGetConn();
        ll_funcion = Convert.ToInt32(bd.ExecuteScalar(con, lsSql));
        con.Close();

        // Si la función no existe...
        if ((ll_funcion == null) || ll_funcion <= 0)
        {
            return "E";
        }

        // Recupera permiso para la función indicada. 
        lsSql = "SELECT permiso " +
          "FROM m_usapp " +
         "WHERE ( idusuario = :gl_usuario ) AND  " +
         "( idapp     = " + ll_funcion + ") AND " +
         "idestado <> 3 ";

        con = bd.fnGetConn();
        ls_permiso = bd.ExecuteScalar(con, lsSql);
        con.Close();


        // Si el usuario no tiene ningún tipo de permiso.
        if ((ls_permiso == null) || ls_permiso == "")
        {
            // Declara cursor para verificar si el usuario tiene permisos a través de un grupo (o rol).

            lsSql = "SELECT m_usrol.idrol " +
            "FROM   m_usrol  " +
            "WHERE  idusuario = " + gl_usuario + " AND " +
            "idestado <> 3";

            con = bd.fnGetConn();
            DataSet aoDat = bd.Fill(con, lsSql);
            con.Close();

            DataTable dt = aoDat.Tables[0];

            foreach (DataRow row in dt.Rows)
            {
                // Recupera permiso para el ROL.INTO  :ls_permiso  
                lsSql = "SELECT  permiso " +
                "FROM  m_rolapp " +
                "WHERE ( idrol = " + Convert.ToString(row["ll_rol"]) + "   ) AND  " +
                "( idapp = " + ll_funcion + " ) AND " +
                "idestado <> 3 ";

                con = bd.fnGetConn();
                ls_permiso = bd.ExecuteScalar(con, lsSql);
                con.Close();

                if (ls_permiso != "")
                    break;

            }
        }

        // Verifica que el permiso no sea nulo.
        if ((ls_permiso == null) || ls_permiso == "")
            ls_permiso = "N";

        ls_permiso = ls_permiso.Trim();

        // Retorna permiso para el usuario.
        return ls_permiso;
    }

    public String jaxValidarPermisoApp(String asCodUsuario, String asApp, String asHosp)
    {
        String asPermiso = "";
        try
        {

            String lsAux, lsSql;
            DataSet ds;
            double lsCodUsuario, lsCodSistema;

            bool abSysAdmin;
            String asAdmApp;
            String asAdmRep;
            String asAdmEst;

            double llApp;
            asApp.ToString().Trim();

            lsCodUsuario = modfun.fnLong(asCodUsuario);
            lsCodSistema = modfun.fnLong(asHosp);


            //Verifica si es un super-usuario.

            bool lbAux = false;

            ds = mfSysAdmin(asCodUsuario, lbAux, asHosp, "", "", "");

            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["admsys"].ToString().Trim().ToLower() == "SI")
                        abSysAdmin = true;
                    else
                        abSysAdmin = false;

                    asAdmApp = ds.Tables[0].Rows[0]["admapp"].ToString();
                    asAdmRep = ds.Tables[0].Rows[0]["admrep"].ToString();
                    asAdmEst = ds.Tables[0].Rows[0]["admest"].ToString();

                    if (abSysAdmin)
                    {
                        asPermiso = "M";
                        return asPermiso;
                    }
                }

            }

            //Recupera ID de Aplicación.
            lsSql = "SELECT " +
                "idapp " +
              "FROM " +
              modConstantes.gsDbAB + "tg_apps " +
              "WHERE " +
                "ltrim(rtrim(Upper(codigo))) = '" + asApp.Trim().ToLower() + "' AND " +
                "idestado  <> 3 and " +
                "id_inst = " + asHosp + " ";

            con = bd.fnGetConn();
            lsAux = bd.ExecuteScalar(con, lsSql);
            con.Close();


            if (modfun.fnLong(lsAux) <= 0)
            {
                return "! No se ha encontrado código de aplicación en la base de datos ¡ ";

            }

            llApp = modfun.fnLong(lsAux);

            //Recupera Permiso de usuario (Tiene precedencia sobre permiso de grupo de usuario).
            lsSql = "SELECT isNull(permiso,'') " +
                "FROM " + modConstantes.gsDbAB + "m_usapp  " +
                "WHERE idapp = " + llApp + " AND " +
                "idestado  <> 3 and " +
                "idusuario = " + asCodUsuario + " and " +
                "id_inst = " + asHosp + " " +
                "ORDER BY permiso DESC";

            con = bd.fnGetConn();
            lsAux = bd.ExecuteScalar(con, lsSql);
            con.Close();


            if (lsAux == "")
            {

                //Recupera Permiso de Rol.
                lsSql = "SELECT isNull(permiso,'') " +
                    "FROM " + modConstantes.gsDbAB + "m_rolapp " +
                    "WHERE " +
                    "idestado <> 3 and " +
                    "idapp = " + llApp + "   and " +
                    "id_inst = " + asHosp + " and " +
                    "idrol IN( " +
                        "SELECT idrol  " +
                        "FROM " + modConstantes.gsDbAB + "m_usrol " +
                        "WHERE " + " " +
                        "idusuario  = " + asCodUsuario + " AND " +
                        "idestado <> 3 AND " +
                        "id_inst  = " + asHosp + " " +
                        "                    ) " +
                    "ORDER BY permiso DESC ";

                //Recupera el mayor permiso, en función que un usuario
                //puede estar asociado a más de un rol
                //con permisos diferentes.

                con = bd.fnGetConn();
                lsAux = bd.ExecuteScalar(con, lsSql);
                con.Close();
            }

            asPermiso = lsAux;

            if (lsAux == "")
            {
                return "Error ";
            }

        }
        catch (Exception e)
        {

            asPermiso = "Error ";
        }

        return asPermiso;

    }



    public String jaxIDUsuario(
           String asRut)
    {

        String asIDUsuario;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select " + msPkM + " from " + msTM + " where idestado <> 3 and rut = " + asRut;

            con = bd.fnGetConn();
            asIDUsuario = bd.ExecuteScalar(con, lsSql);
            con.Close();


            if (modfun.fnLong(asIDUsuario) <= 0)
                asIDUsuario = "";

        }
        catch (Exception ex)
        {
            asIDUsuario = "";
        }
        return asIDUsuario;
    }



    private DataSet mfSysAdmin(String asIdUsuario, Boolean abSysAdmin, String asCodSistema, String asAdmApp, String asAdmRep, String asAdmEst)
    {

        DataSet ds;

        try
        {
            String lsSql = "select idusuario,id_inst,vrf," +
                        "case admsys when 1 then 'SI' else 'NO' end as admsys , " +
                        "case admapp when 1 then 'SI' else 'NO' end as admapp , " +
                        "case admest when 1 then 'SI' else 'NO' end as admest , " +
                        "case admrep when 1 then 'SI' else 'NO' end as admrep   " +
                        "from " + modConstantes.gsDbAB + "m_sys_admin " +
                        "where idestado <> 3 and  " +
                        "idusuario = " + asIdUsuario + " and " +
                        "id_inst  = " + asCodSistema;

            con = bd.fnGetConn();
            ds = bd.Fill(con, lsSql);
            con.Close();
        }
        catch (Exception e)
        {

            ds = null;
        }

        return ds;
    }


    public string IdConstantes(string asIdent)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select descripcion from " + modConstantes.gsDbAB + "tg_constantes where codigo = '" + asIdent.Replace(" ", "") + "' ";

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "S/N";
        }
        return asNombre;
    }

    public string AbastRut(string asIdent)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select rut from " + modConstantes.gsDbAB + "m_usuarios where idusuario = " + asIdent;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "S/N";
        }
        return asNombre;
    }

    public string TraeNombre(string asrut)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select nombre from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "S/N";
        }
        return asNombre;
    }

    public string TraeDV(string asrut)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select dv from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "S/N";
        }
        return asNombre;
    }

    public string mfIntentos(string asrut)
    {
        String asCont;
        try
        {
            string lsSql = "";
            string dia = DateTime.Now.Day.ToString(), mes = DateTime.Now.Month.ToString(), anio = DateTime.Now.Year.ToString();

            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(cont,0) from " + modConstantes.gsDbAB + "m_accesos where rut = " + asrut +
                " and dia = " + dia + " and mes = " + mes + " and anio = " + anio;

            con = bd.fnGetConn();
            asCont = bd.ExecuteScalar(con, lsSql);

            if (asCont == "") asCont = "0";
            con.Close();


        }
        catch (Exception ex)
        {
            asCont = "0";
        }
        return asCont;
    }

    public string mfEsMedico(string asrut)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(MEDICO,0) from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

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


    public string mfEsFarmacia(string asrut)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(FARMACIA,0) from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

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

    public string mfEsInfectologia(string asrut)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(INFECTOLOGIA,0) from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

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

    public string mfEsSalud(string asId)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(SALUD,0) from " + modConstantes.gsDbAB + "m_usuarios where idusuario = " + asId;

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

    public string UserIdentificador(string asrut)
    {
        String asID;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select IDUSUARIO from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

            con = bd.fnGetConn();
            asID = bd.ExecuteScalar(con, lsSql);

            if (asID == "") asID = "0";

            con.Close();


        }
        catch (Exception ex)
        {
            asID = "0";
        }
        return asID;
    }

    public string UserServicio(string asrut)
    {
        String asID;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select ISNULL(CODUNIOP,1) from " + modConstantes.gsDbAB + "m_usuarios where rut = " + asrut;

            con = bd.fnGetConn();
            asID = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asID = "";
        }
        return asID;
    }

    public string mfUserIdServicio(string asIdentificador)
    {
        String asID;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select ISNULL(CODUNIOP,0) from " + modConstantes.gsDbAB + "m_usuarios where IDUSUARIO = " + asIdentificador;

            con = bd.fnGetConn();
            asID = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asID = "";
        }
        return asID;
    }

    public string UseFirma(string asIdentificador)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select ltrim(rtrim(firma)) from db_abastecimiento.dbo.tg_firma_oc where idestado <> 3 and idusuario = " + asIdentificador;

            con = bd.fnGetConn();
            asIdServ = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asIdServ = "";
        }
        return asIdServ;
    }


    public string mfGuardar(string asIdentificador, string asHosp, string asPass)
    {
        string lsRet = "";
        string lsSql;
        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "m_usuarios set " +
                "passwd = '" + modFunciones.Encriptar(asPass) + "', " +
                "new_passwd = 1 " +
                "where RUT = " + asIdentificador + " " +
                "and id_inst = " + asHosp;

        lsRet = bd.EjecutarComando(con, lsSql);


        return lsRet;

    }

    public string mfGuardarMedico(string asIdentificador, string asRut, string asNomb)
    {
        string lsRet = "";
        string lsSql;
        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "m_usuarios set " +
                "RUT_MED = " + asRut + ", " +
                "NOMB_MED = '" + asNomb + "' " +
                "where idusuario = " + asIdentificador + " ";

        lsRet = bd.EjecutarComando(con, lsSql);


        return lsRet;

    }

    public string IsNewPass(string asIdentificador, string asHosp)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(new_passwd,0) from " + modConstantes.gsDbAB + "m_usuarios " +
                    "where idestado <> 3 and rut = " + asIdentificador + " and id_inst = " + asHosp;

            con = bd.fnGetConn();
            asIdServ = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asIdServ = "";
        }
        return asIdServ;
    }

    public string ObtenerClave(string asIdentificador, string asHosp)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "select isnull(passwd,'') from " + modConstantes.gsDbAB + "m_usuarios " +
                    "where idestado <> 3 and rut = " + asIdentificador + " and id_inst = " + asHosp;

            con = bd.fnGetConn();
            asIdServ = modFunciones.DesEncriptar(bd.ExecuteScalar(con, lsSql));
            con.Close();
        }
        catch (Exception ex)
        {
            asIdServ = "";
        }
        return asIdServ;
    }


    public DataSet TraeDatosUsuario(String asIdUsuario)
    {

        DataSet ds;

        try
        {
            String lsSql = "select IDUSUARIO, RUT, ID_INST, IDBODEGA, CODUNIOP, DV,  isnull(NOMBRE,'S/N') NOMBRE, " +
                            "convert(varchar(20), RUT) + '-' + DV as RUT_DV, " +
                        "isnull(TELEFONO,0) TELEFONO, isnull(EMAIL,'S/M') EMAIL, IDESTADO, PASSWD, new_passwd, observacion, ISNULL(ESPECIALIDAD,'S/E')  especialidad " +
                        "from " + modConstantes.gsDbAB + "m_usuarios " +
                        "where " +
                        "idusuario = " + asIdUsuario;

            con = bd.fnGetConn();
            ds = bd.Fill(con, lsSql);
            con.Close();
        }
        catch (Exception e)
        {

            ds = null;
        }

        return ds;
    }

    public string mfUpdateEstado(string asId, string asIdEstado)
    {
        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "m_usuarios set idestado = " + asIdEstado + " " +
                "where idusuario = " + asId;

        lsRet = bd.EjecutarComando(con, lsSql);



        con.Close();
        return lsRet;

    }

    public string mfReIniciarClave(string asId)
    {
        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "m_usuarios set PASSWD = 'MQAyADMANAA=', new_passwd = 0 " +
                "where idusuario = " + asId;

        lsRet = bd.EjecutarComando(con, lsSql);



        con.Close();
        return lsRet;

    }

    public string mfLimpiarAcceso(string asIdentificador)
    {
        string lsRet = "";
        string lsSql;
        con = bd.fnGetConn();

        lsSql = "DELETE FROM M_ACCESOS WHERE RUT = " + asIdentificador;

        lsRet = bd.EjecutarComando(con, lsSql);


        return lsRet;

    }


    #endregion

    #region roles

    public string mfEsValidador(string asIdentificador, string asHosp, string asIdunidad)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "SELECT COUNT(1) " +
                    "FROM " + modConstantes.gsDbAB + "[M_USER_UNIDAD] " +
                    "WHERE VALIDA = 1 " +
                    "AND ID_INST = " + asHosp + " " +
                    "AND IDUSUARIO = " + asIdentificador + " " +
                    "AND CODUNIOP = " + asIdunidad;

            con = bd.fnGetConn();
            asIdServ = bd.ExecuteScalar(con, lsSql);

            if (asIdServ == "") asIdServ = "0";


            con.Close();
        }
        catch (Exception ex)
        {
            asIdServ = "0";
        }
        return asIdServ;
    }


    public string mfEsSDA(string asIdentificador)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "	select count(1)  " +
                    "from " + modConstantes.gsDbAB + "[TG_CARGOS] CAR " +
                    "INNER JOIN " + modConstantes.gsDbAB + "[M_USER_CARGO] USCAR ON USCAR.IDCARGO = CAR.IDCARGO AND USCAR.IDESTADO <> 3 " +
                    "WHERE CAR.IDESTADO <> 3 " +
                    "and CAR.codigo = 'SDA' " +
                    "AND USCAR.IDUSUARIO = " + asIdentificador;

            con = bd.fnGetConn();
            asIdServ = bd.ExecuteScalar(con, lsSql);

            if (asIdServ == "") asIdServ = "0";


            con.Close();
        }
        catch (Exception ex)
        {
            asIdServ = "0";
        }
        return asIdServ;
    }

    public string mfEsSDM(string asIdentificador)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "	select count(1)  " +
                    "from " + modConstantes.gsDbAB + "[TG_CARGOS] CAR " +
                    "INNER JOIN " + modConstantes.gsDbAB + "[M_USER_CARGO] USCAR ON USCAR.IDCARGO = CAR.IDCARGO AND USCAR.IDESTADO <> 3 " +
                    "WHERE CAR.IDESTADO <> 3 " +
                    "and CAR.codigo = 'SDM' " +
                    "AND USCAR.IDUSUARIO = " + asIdentificador;

            con = bd.fnGetConn();
            asIdServ = bd.ExecuteScalar(con, lsSql);

            if (asIdServ == "") asIdServ = "0";

            con.Close();
        }
        catch (Exception ex)
        {
            asIdServ = "0";
        }
        return asIdServ;
    }


    public string mfEsAPC(string asIdentificador)
    {
        String asIdServ;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "	select count(1)  " +
                    "from " + modConstantes.gsDbAB + "[TG_CARGOS] CAR " +
                    "INNER JOIN " + modConstantes.gsDbAB + "[M_USER_CARGO] USCAR ON USCAR.IDCARGO = CAR.IDCARGO AND USCAR.IDESTADO <> 3 " +
                    "WHERE CAR.IDESTADO <> 3 " +
                    "and CAR.codigo = 'APC' " +
                    "AND USCAR.IDUSUARIO = " + asIdentificador;

            con = bd.fnGetConn();
            asIdServ = bd.ExecuteScalar(con, lsSql);

            if (asIdServ == "") asIdServ = "0";

            con.Close();
        }
        catch (Exception ex)
        {
            asIdServ = "0";
        }
        return asIdServ;
    }

    #endregion

    public DataSet menuUser(string asIdUsuario, string asHosp)//Personalizar menu usuario - JVV 12/12/2023
    {
        DataSet ds;
        String lsSql = "";
        //devolver todos los permisos del usuario, ya sea asignado por roles o por acceso - JVV
        try
        {
            lsSql = "SELECT IDAPP, RTRIM(LTRIM(CODIGO)) CODIGO, DESCRIPCION " + //permisos app por acceso - JVV
                "FROM( " +
                "SELECT a.IDAPP, a.CODIGO, a.DESCRIPCION " +
                "FROM " + modConstantes.gsDbAB + "TG_APPS AS a " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_USAPP AS u ON a.IDAPP = u.IDAPP " +
                "WHERE u.IDUSUARIO = " + asIdUsuario + " AND u.idestado <> 3 AND u.ID_INST = " + asHosp + " " +
             "UNION " +
                "SELECT ta.IDAPP, ta.CODIGO, ta.DESCRIPCION " + //permisos app por Roles - JVV
                "FROM " + modConstantes.gsDbAB + "m_usrol ur " +
                "INNER JOIN " + modConstantes.gsDbAB + "TG_ROLES ro ON ur.IDROL = ro.IDROL " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_ROLAPP ra ON ra.IDROL = ur.IDROL " +
                "INNER JOIN " + modConstantes.gsDbAB + "TG_APPS ta ON ta.IDAPP = ra.IDAPP " +
                "WHERE ur.IDUSUARIO = " + asIdUsuario + " AND ra.idestado <> 3 AND ra.ID_INST = " + asHosp + " " +
            ") AS RESULTADO " +
                "ORDER BY IDAPP; ";

            con = bd.fnGetConn();
            ds = bd.Fill(con, lsSql);
            con.Close();
        }
        catch (Exception e)
        {

            ds = null;
        }
        return ds;
    }
}
