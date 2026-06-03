using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de ClassReceta
/// </summary>
public class ClassReceta
{
    // Declaración  de Variables

    public string ls_idusr { get; set; }
    public string ls_reg_med { get; set; }
    public string ls_rut_med { get; set; }
    public string ls_dv_med { get; set; }
    public string ls_nomb_med { get; set; }
    public string ls_com_med { get; set; }
    public string ls_direccion { get; set; }
    public string ls_fecha { get; set; }
    public string ls_nomb_pac { get; set; }
    public string ls_nomb_pac_social { get; set; }
    public string ls_pat_pac { get; set; }
    public string ls_mat_pac { get; set; }
    public string ls_rut_pac { get; set; }
    public string ls_dv_pac { get; set; }
    public string ls_dir_pac { get; set; }
    public string ls_reg_pac { get; set; }
    public string ls_com_pac { get; set; }
    public string ls_unidad { get; set; }
    public string ls_num_rec { get; set; }
    public string ls_obs { get; set; }
    public string ls_diag { get; set; }

    // Declaración  de Variables Listas
    public string ls_f_d { get; set; }
    public string ls_f_h { get; set; }
    public string ls_uni { get; set; }
    public string ls_num { get; set; }
    public string ls_Codigo { get; set; }
    public string ls_Art { get; set; }
    public string ls_rut { get; set; }
    public string ls_nomb { get; set; }
    public string ls_tipo { get; set; }

    public string ls_All { get; set; }
    public string ls_Manual { get; set; }

    public string ls_Serv { get; set; }
    public string ls_SubServ { get; set; }
    public string ls_Cama { get; set; }

    public string ls_IdServ { get; set; }
    public string ls_IdSubServ { get; set; }
    public string ls_IdCama { get; set; }
    public string ls_IdTipoDiag { get; set; }
    public string ls_Prioridad { get; set; }
    public string ls_Rapido { get; set; }

    public string ls_TipoAdq { get; set; }
    public string ls_NombAdq { get; set; }
    public string ls_RutAdq { get; set; }
    public string ls_FonoAdq { get; set; }
    public string ls_Bod { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    modFunciones mod = new modFunciones();

    public ClassReceta()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    #region Cabecera

    public string mfUpdateRutporRut(string asRut_New, string asDv_New, string asRut_Old)
    {
        string lsRet = "";
        string lsSql = "";



        lsSql = "update " + modConstantes.gsDbAB + "M_RECETA set " +
                "rut = '" + asRut_New + "', " +
                "dv = '" + asDv_New + "' " +
                    "where rut = '" + asRut_Old + "' ";


        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }
    public string mfUpdateAdquiriente(string asIdentificador, string TipoAdq, string NombAdq, string RutAdq, string FonoAdq)
    {
        string lsRet = "";
        string lsSql = "";



        lsSql = "update " + modConstantes.gsDbAB + "M_RECETA set " +
                "TipoAdq = " + TipoAdq + ", " +
                "NombAdq = '" + NombAdq + "', " +
                "RutAdq = '" + RutAdq + "', " +
                "FonoAdq = '" + FonoAdq + "' " +
                    "where IDRECETA = " + asIdentificador;


        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfGuardarManual(string asIdentificador, Boolean lbNvo)
    {
        string lsRet = "";
        string lsSql = "";

        if (lbNvo == true)
        {

            lsSql = "INSERT INTO " + modConstantes.gsDbAB + "M_RECETA( IDREGION, IDCOMUNA, " +
                    " RUT_MED , NOMB_MED, DV_MED, NUM_REC_MANUAL, " +
                    "FOLIO, DIRECCION, RUT, DV, " +
                    "NOMBRE, APELL_PAT, APELL_MAT, " +
                    "NombreSocial, " +
                    "IDREGION_PAC, IDCOMUNA_PAC,  " +
                    "DIRECCION_PAC, F_H_CREACION, OBSERVACION, " +
                    "DIAGNOSTICO, CODUNIOP, TIPO, " +
                    "IDDIAGNOSTICO," +
                    "IDSERVICIO, IDSUBSERVICIO, IDCAMA, " +
                    "SERVICIO, SUBSERVICIO, CAMA, idusuario, F_H_MOD," +
                    "TipoAdq, NombAdq, RutAdq, FonoAdq, IDBODPRERIF) " +
                    "VALUES( " + ls_reg_med + "," + ls_com_med + ", " +
                    ls_rut_med + ", '" + ls_nomb_med + "', '" + ls_dv_med + "', '" + ls_num_rec + "', " +
                    "'0', '" + ls_direccion.Replace("'", " ") + "','" + ls_rut_pac + "','" + ls_dv_pac + "','" +
                    ls_nomb_pac.Replace("'", " ") + "', '" + ls_pat_pac.Replace("'", " ") + "', '" + ls_mat_pac.Replace("'", " ") + "', " +
                    "'" + ls_nomb_pac_social + "', " +
                    ls_reg_pac + ", " + ls_com_pac + ", '" +
                    ls_dir_pac.Replace("'", " ") + "', getdate(), '" + ls_obs.Replace("'", " ") + "','" +
                    ls_diag.Replace("'", " ") + "'," + ls_unidad + ", " + ls_tipo + "," +
                    ls_IdTipoDiag + ", " +
                    ls_IdServ + "," + ls_IdSubServ + ", " + ls_IdCama + ",'" +
                    ls_Serv + "','" + ls_SubServ + "','" + ls_Cama + "', " + ls_idusr + ", getdate(), " +
                    ls_TipoAdq + ", '" + ls_NombAdq + "', '" + ls_RutAdq + "', '" + ls_FonoAdq + "', " + ls_Bod + ")";

        }
        else
        {


            lsSql = "update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "TipoAdq = " + ls_TipoAdq + ", " +
                    "NombAdq = '" + ls_NombAdq + "', " +
                    "RutAdq = '" + ls_RutAdq + "', " +
                    "FonoAdq = '" + ls_FonoAdq + "', " +
                    "IDBODPRERIF = " + ls_Bod + ", " +
                        "RUT_MED = " + ls_rut_med + ", " +
                        "DV_MED = '" + ls_dv_med + "', " +
                        "NOMB_MED = '" + ls_nomb_med + "', " +
                        "NUM_REC_MANUAL = '" + ls_num_rec + "', " +
                        "IDREGION = " + ls_reg_med + ", " +
                        "IDCOMUNA = " + ls_com_med + ", " +
                        "CODUNIOP = " + ls_unidad + ", " +
                        "F_H_MOD = GETDATE()," +
                        "DIRECCION = '" + ls_direccion.Replace("'", " ") + "', " +
                        "RUT = '" + ls_rut_pac + "', " +
                        "DV = '" + ls_dv_pac + "', " +
                        "NOMBRE = '" + ls_nomb_pac.Replace("'", " ") + "', " +
                        "NombreSocial = '" + ls_nomb_pac_social.Replace("'", " ") + "', " +
                        "APELL_PAT = '" + ls_pat_pac.Replace("'", " ") + "', " +
                        "APELL_MAT = '" + ls_mat_pac.Replace("'", " ") + "', " +
                        "IDREGION_PAC = " + ls_reg_pac + ", " +
                        "IDCOMUNA_PAC = " + ls_com_pac + ", " +
                        "DIRECCION_PAC = '" + ls_dir_pac + "', " +
                        "OBSERVACION = '" + ls_obs.Replace("'", " ") + "', " +
                        "DIAGNOSTICO = '" + ls_diag.Replace("'", " ") + "', " +
                        "IDDIAGNOSTICO = " + ls_IdTipoDiag + ", " +
                        "IDSERVICIO = " + ls_IdServ + ", " +
                        "IDSUBSERVICIO = " + ls_IdSubServ + ", " +
                        "IDCAMA = " + ls_IdCama + ", " +
                        "SERVICIO = '" + ls_Serv.Replace("'", " ") + "', " +
                        "SUBSERVICIO = '" + ls_IdSubServ.Replace("'", " ") + "', " +
                        "CAMA = '" + ls_Cama.Replace("'", " ") + "' " +
                        "where IDRECETA = " + asIdentificador;

        }

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfGuardar(string asIdentificador, Boolean lbNvo)
    {
        string lsRet = "";
        string lsSql = "";

        if (lbNvo == true)
        {

            lsSql = "INSERT INTO " + modConstantes.gsDbAB + "M_RECETA( IDUSUARIO, IDREGION, IDCOMUNA, " +
                    "FOLIO, DIRECCION, RUT, DV, " +
                    "NOMBRE, APELL_PAT, APELL_MAT, " +
                    "NombreSocial, " +
                    "IDREGION_PAC, IDCOMUNA_PAC,  " +
                    "DIRECCION_PAC, F_H_CREACION, OBSERVACION, " +
                    "DIAGNOSTICO, CODUNIOP, TIPO, " +
                    "IDDIAGNOSTICO," +
                    "IDSERVICIO, IDSUBSERVICIO, IDCAMA, " +
                    "SERVICIO, SUBSERVICIO, CAMA) " +
                    "VALUES(" + ls_idusr + "," + ls_reg_med + "," + ls_com_med + ", " +
                    "'0', '" + ls_direccion.Replace("'", " ") + "','" + ls_rut_pac + "','" + ls_dv_pac + "','" +
                    ls_nomb_pac.Replace("'", " ") + "', '" + ls_pat_pac.Replace("'", " ") + "', '" + ls_mat_pac.Replace("'", " ") + "', " +
                    "'" + ls_nomb_pac_social + "', " +
                    ls_reg_pac + ", " + ls_com_pac + ", '" +
                    ls_dir_pac.Replace("'", " ") + "', getdate(), '" + ls_obs.Replace("'", " ") + "','" +
                    ls_diag.Replace("'", " ") + "'," + ls_unidad + ", " + ls_tipo + "," +
                    ls_IdTipoDiag + ", " +
                    ls_IdServ + "," + ls_IdSubServ + ", " + ls_IdCama + ",'" +
                    ls_Serv + "','" + ls_SubServ + "','" + ls_Cama + "')";

        }
        else
        {


            lsSql = "update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDREGION = " + ls_reg_med + ", " +
                        "IDCOMUNA = " + ls_com_med + ", " +
                        "CODUNIOP = " + ls_unidad + ", " +
                        "DIRECCION = '" + ls_direccion.Replace("'", " ") + "', " +
                        "RUT = '" + ls_rut_pac + "', " +
                        "DV = '" + ls_dv_pac + "', " +
                        "NOMBRE = '" + ls_nomb_pac.Replace("'", " ") + "', " +
                        "NombreSocial = '" + ls_nomb_pac_social.Replace("'", " ") + "', " +
                        "APELL_PAT = '" + ls_pat_pac.Replace("'", " ") + "', " +
                        "APELL_MAT = '" + ls_mat_pac.Replace("'", " ") + "', " +
                        "IDREGION_PAC = " + ls_reg_pac + ", " +
                        "IDCOMUNA_PAC = " + ls_com_pac + ", " +
                        "DIRECCION_PAC = '" + ls_dir_pac + "', " +
                        "OBSERVACION = '" + ls_obs.Replace("'", " ") + "', " +
                        "DIAGNOSTICO = '" + ls_diag.Replace("'", " ") + "', " +
                        "IDDIAGNOSTICO = " + ls_IdTipoDiag + ", " +
                        "IDSERVICIO = " + ls_IdServ + ", " +
                        "IDSUBSERVICIO = " + ls_IdSubServ + ", " +
                        "IDCAMA = " + ls_IdCama + ", " +
                        "SERVICIO = '" + ls_Serv.Replace("'", " ") + "', " +
                        "SUBSERVICIO = '" + ls_IdSubServ.Replace("'", " ") + "', " +
                        "CAMA = '" + ls_Cama.Replace("'", " ") + "' " +
                        "where IDRECETA = " + asIdentificador;

        }

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }


    public string ConsultarMaxID()
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select isnull(max(IDRECETA),0) from " + modConstantes.gsDbAB + "M_RECETA  ";


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string ConsultarMaxID(string asIdusr)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select case when  isnull(max(IDRECETA) ,0) = 0  " +
                    "then(select max(idreceta) from M_RECETA) else max(IDRECETA)  end " +
                    " from " + modConstantes.gsDbAB +
                    "M_RECETA where idusuario =  " + asIdusr;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfGetEstado(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select ISNULL(idestado,1)  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfGetDispensado(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select count(1)  from " + modConstantes.gsDbAB + "M_OBSERVACIONES_RECETA  " +
                    "WHERE IDESTADO_FIN in (4,13) and IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfGetEditarRecta(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select count(1)  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDESTADO in (4, 3, 15) and IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfGetPrioridad(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select ISNULL(FPRIORIDAD,0)  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfGetDiagnostico(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select ISNULL(DIAGNOSTICO,'')  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfGetObservacion(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select ISNULL(OBSERVACION,'')  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfTraeIdUnidOp(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select ISNULL(CODUNIOP,1) " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }



    public string mfGetNombMedico(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select ISNULL(NOMB_MED,'')  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfRutNombMedico(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select ISNULL(RUT_MED,-1)  from " + modConstantes.gsDbAB + "M_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }
    #endregion


    #region articulos


    public string mfUpCantValidar(string lsIdentifcador, string lsRango, string lsPosologia, string lsFecha, string lsObs, string lsPend, string lsFechaNow)
    {
        string lsRet = "";


        con = bd.fnGetConn();
        //string lsSql = "UPDATE M_ART_RECETA SET RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
        //                "FDESPACHO = '" + lsFecha + "', OBS_FARM = '" + lsObs + "', CANT_PEND = " + lsPend.Replace(",", ".") + ", " +
        //                " cant_desp_req =  " + lsPosologia.Replace(",", ".") + ", FDESPACHO_REAL = '" + lsFechaNow + "', " +
        //                "elib = case when " + lsPosologia.Replace(",", ".") + " = 0 and " + lsPend.Replace(",", ".") + " = 0 then 1 else 0 end " +
        //                "WHERE IDARTRECETA = " + lsIdentifcador;

        string lsSql = "UPDATE M_ART_RECETA SET RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
                "FDESPACHO = '" + lsFecha + "', OBS_FARM = '" + lsObs + "', CANT_PEND = " + lsPend.Replace(",", ".") + ", " +
                " cant_desp_req =  " + lsPosologia.Replace(",", ".") + ", FDESPACHO_REAL = '" + lsFechaNow + "', SALDO_SALE = 1 " +
                "WHERE IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return lsRet;
    }


    public string mfUpCantValidar2(string lsIdentifcador, string lsRango, string lsPosologia, string lsFecha, string lsObs, string lsPend, string lsFechaNow)
    {
        string lsRet = "";


        con = bd.fnGetConn();
        //string lsSql = "UPDATE M_ART_RECETA SET RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
        //                "FDESPACHO = '" + lsFecha + "', OBS_FARM = '" + lsObs + "', CANT_PEND = " + lsPend.Replace(",", ".") + ", " +
        //                " cant_desp_req =  " + lsPosologia.Replace(",", ".") + ", FDESPACHO_REAL = '" + lsFechaNow + "', " +
        //                "elib = case when " + lsPosologia.Replace(",", ".") + " = 0 and " + lsPend.Replace(",", ".") + " = 0 then 1 else 0 end " +
        //                "WHERE IDARTRECETA = " + lsIdentifcador;

        string lsSql = "UPDATE M_ART_RECETA SET RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
                "FDESPACHO = '" + lsFecha + "', OBS_FARM = '" + lsObs + "', CANT_PEND = " + lsPend.Replace(",", ".") + ", " +
                " cant_desp_req =  " + lsPosologia.Replace(",", ".") + ", FDESPACHO_REAL = '" + lsFechaNow + "' " +
                "WHERE IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return lsRet;
    }



    public string mfAsociarPosologiaManual(string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                                string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion,
                                string lsObservacion, string lsPosologia, string lsfecha, string lsFechaNow, string lsPend)
    {
        string lsRet = "";
        if (lsObservacion == null)
        {
            lsObservacion = "";
        }


        con = bd.fnGetConn();
        string lsSql = "UPDATE M_ART_RECETA SET CANTIDAD = " + lsCant.Replace(",", ".") + ", RANGO = " + lsRango.Replace(",", ".") + ", DURACION = " + lsDuracion.Replace(",", ".") + ", " +
                        "IDRANGO = " + lsIdRango + ", IDVIA = " + lsIdVia + ", IDPERIODO = " + lsIdPeriodo + ", IDPRESENTACION = "
                        + lsIdPresentacion + ", OBSERVACIONES = '" + lsObservacion + "', POSOLOGIA = " + lsPosologia.Replace(",", ".") + ",  " +
                        "RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
                        "FDESPACHO = '" + lsfecha + "', FDESPACHO_REAL = '" + lsFechaNow + "', CANT_PEND = " + lsPend.Replace(",", ".") + ", " +
                        " cant_desp_req = " + lsPosologia.Replace(",", ".") + ", OBS_FARM = '" + lsObservacion + "' " +
                        "WHERE IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return lsRet;
    }


    public string mfAsociarPosologia(string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                                    string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion,
                                    string lsObservacion, string lsPosologia)
    {
        string lsRet = "";
        if (lsObservacion == null)
        {
            lsObservacion = "";
        }


        con = bd.fnGetConn();
        string lsSql = "UPDATE M_ART_RECETA SET CANTIDAD = " + lsCant.Replace(",", ".") + ", RANGO = " + lsRango.Replace(",", ".") + ", DURACION = " + lsDuracion.Replace(",", ".") + ", " +
                        "IDRANGO = " + lsIdRango + ", IDVIA = " + lsIdVia + ", IDPERIODO = " + lsIdPeriodo + ", IDPRESENTACION = "
                        + lsIdPresentacion + ", OBSERVACIONES = '" + lsObservacion + "', POSOLOGIA = " + lsPosologia.Replace(",", ".") + ",  " +
                        "RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
                        "OBS_FARM = '" + lsObservacion + "' " +
                        "WHERE IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return lsRet;
    }



    public string mfAsociarPosologiaDetalleManualId(string lsIdReceta, string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                        string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion, string lsObservacion,
                        string lsPosologia, string lsFecha, string lsFechaNow, string lsPend)
    {
        string lsRet = "";

        if (lsObservacion == null)
        {
            lsObservacion = "";
        }

        string _XML = string.Empty;

        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_ins_art_receta_manual", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@lsIdReceta", SqlDbType.Int, 4);
        Query.Parameters["@lsIdReceta"].Value = lsIdReceta;

        Query.Parameters.Add("@lsIdentifcador", SqlDbType.Int, 4);
        Query.Parameters["@lsIdentifcador"].Value = lsIdentifcador;

        Query.Parameters.Add("@lsCant", SqlDbType.VarChar, 10);
        Query.Parameters["@lsCant"].Value = lsCant;

        Query.Parameters.Add("@lsRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsRango"].Value = lsRango;

        Query.Parameters.Add("@lsDuracion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsDuracion"].Value = lsDuracion;

        Query.Parameters.Add("@lsIdRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdRango"].Value = lsIdRango;

        Query.Parameters.Add("@lsIdVia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdVia"].Value = lsIdVia;

        Query.Parameters.Add("@lsIdPeriodo", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPeriodo"].Value = lsIdPeriodo;

        Query.Parameters.Add("@lsIdPresentacion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPresentacion"].Value = lsIdPresentacion;

        Query.Parameters.Add("@lsObservacion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsObservacion"].Value = lsObservacion;

        Query.Parameters.Add("@lsPosologia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsPosologia"].Value = lsPosologia;

        Query.Parameters.Add("@lsFecha", SqlDbType.VarChar, 10);
        Query.Parameters["@lsFecha"].Value = lsFecha;

        Query.Parameters.Add("@lsFechaNew", SqlDbType.VarChar, 10);
        Query.Parameters["@lsFechaNew"].Value = lsFechaNow;

        Query.Parameters.Add("@lsPend", SqlDbType.VarChar, 10);
        Query.Parameters["@lsPend"].Value = lsPend;


        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();
        //con = bd.fnGetConn();
        //string lsSql = "INSERT INTO M_ART_RECETA( IDARTICULO, IDRECETA, CANTIDAD, " +
        //                "RANGO, DURACION, IDPRESENTACION,  " +
        //                "IDRANGO, IDVIA, IDPERIODO, OBSERVACIONES, POSOLOGIA) " +
        //                "VALUES(" + lsIdentifcador + ", " + lsIdReceta + ", " + lsCant + 
        //                ", " + lsRango + ", " + lsDuracion + ", " + lsIdPresentacion + 
        //                ", " + lsIdRango + ", " + lsIdVia + ", " + lsIdPeriodo + ", '" + lsObservacion + "', " + lsPosologia.Replace(",",".") + " )";

        //lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return _XML;
    }

    public string mfAsociarPosologiaDetalleManual(string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                        string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion,
                        string lsObservacion, string lsPosologia, string lsFecha, string lsFechaNow, string lsPend)
    {
        //string lsRet = "";
        if (lsObservacion == null)
        {
            lsObservacion = "";
        }

        string _XML = string.Empty;

        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_update_art_receta_manual", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@lsIdentifcador", SqlDbType.Int, 4);
        Query.Parameters["@lsIdentifcador"].Value = lsIdentifcador;

        Query.Parameters.Add("@lsCant", SqlDbType.VarChar, 10);
        Query.Parameters["@lsCant"].Value = lsCant;

        Query.Parameters.Add("@lsRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsRango"].Value = lsRango;

        Query.Parameters.Add("@lsDuracion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsDuracion"].Value = lsDuracion;

        Query.Parameters.Add("@lsIdRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdRango"].Value = lsIdRango;

        Query.Parameters.Add("@lsIdVia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdVia"].Value = lsIdVia;

        Query.Parameters.Add("@lsIdPeriodo", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPeriodo"].Value = lsIdPeriodo;

        Query.Parameters.Add("@lsIdPresentacion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPresentacion"].Value = lsIdPresentacion;

        Query.Parameters.Add("@lsObservacion", SqlDbType.VarChar, 100);
        Query.Parameters["@lsObservacion"].Value = lsObservacion;

        Query.Parameters.Add("@lsPosologia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsPosologia"].Value = lsPosologia;

        Query.Parameters.Add("@lsFecha", SqlDbType.VarChar, 10);
        Query.Parameters["@lsFecha"].Value = lsFecha;

        Query.Parameters.Add("@lsFechaNew", SqlDbType.VarChar, 10);
        Query.Parameters["@lsFechaNew"].Value = lsFechaNow;

        Query.Parameters.Add("@lsPend", SqlDbType.VarChar, 10);
        Query.Parameters["@lsPend"].Value = lsPend;


        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();

        //con = bd.fnGetConn();
        //string lsSql = "UPDATE M_ART_RECETA SET CANTIDAD = " + lsCant.Replace(",", ".") + ", RANGO = " + lsRango.Replace(",", ".") + ", DURACION = " + lsDuracion.Replace(",", ".") + ", " +
        //                "IDRANGO = " + lsIdRango + ", IDVIA = " + lsIdVia + ", IDPERIODO = " + lsIdPeriodo + ", IDPRESENTACION = " + lsIdPresentacion + ", OBSERVACIONES = '" + lsObservacion + "', POSOLOGIA = " + lsPosologia.Replace(",", ".") + "  " +
        //                "WHERE IDARTRECETA = " + lsIdentifcador;

        //lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return _XML;
    }

    public string mfAsociarPosologiaDetalle(string lsIdReceta, string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                            string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion, string lsObservacion,
                            string lsPosologia)
    {
        string lsRet = "";

        if (lsObservacion == null)
        {
            lsObservacion = "";
        }

        string _XML = string.Empty;

        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_ins_art_receta", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@lsIdReceta", SqlDbType.Int, 4);
        Query.Parameters["@lsIdReceta"].Value = lsIdReceta;

        Query.Parameters.Add("@lsIdentifcador", SqlDbType.Int, 4);
        Query.Parameters["@lsIdentifcador"].Value = lsIdentifcador;

        Query.Parameters.Add("@lsCant", SqlDbType.VarChar, 10);
        Query.Parameters["@lsCant"].Value = lsCant;

        Query.Parameters.Add("@lsRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsRango"].Value = lsRango;

        Query.Parameters.Add("@lsDuracion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsDuracion"].Value = lsDuracion;

        Query.Parameters.Add("@lsIdRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdRango"].Value = lsIdRango;

        Query.Parameters.Add("@lsIdVia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdVia"].Value = lsIdVia;

        Query.Parameters.Add("@lsIdPeriodo", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPeriodo"].Value = lsIdPeriodo;

        Query.Parameters.Add("@lsIdPresentacion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPresentacion"].Value = lsIdPresentacion;

        Query.Parameters.Add("@lsObservacion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsObservacion"].Value = lsObservacion;

        Query.Parameters.Add("@lsPosologia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsPosologia"].Value = lsPosologia;


        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();
        //con = bd.fnGetConn();
        //string lsSql = "INSERT INTO M_ART_RECETA( IDARTICULO, IDRECETA, CANTIDAD, " +
        //                "RANGO, DURACION, IDPRESENTACION,  " +
        //                "IDRANGO, IDVIA, IDPERIODO, OBSERVACIONES, POSOLOGIA) " +
        //                "VALUES(" + lsIdentifcador + ", " + lsIdReceta + ", " + lsCant + 
        //                ", " + lsRango + ", " + lsDuracion + ", " + lsIdPresentacion + 
        //                ", " + lsIdRango + ", " + lsIdVia + ", " + lsIdPeriodo + ", '" + lsObservacion + "', " + lsPosologia.Replace(",",".") + " )";

        //lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return _XML;
    }

    public string mfAsociarPosologiaDetalle(string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                            string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion,
                            string lsObservacion, string lsPosologia)
    {
        //string lsRet = "";
        if (lsObservacion == null)
        {
            lsObservacion = "";
        }

        string _XML = string.Empty;

        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_update_art_receta", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@lsIdentifcador", SqlDbType.Int, 4);
        Query.Parameters["@lsIdentifcador"].Value = lsIdentifcador;

        Query.Parameters.Add("@lsCant", SqlDbType.VarChar, 10);
        Query.Parameters["@lsCant"].Value = lsCant;

        Query.Parameters.Add("@lsRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsRango"].Value = lsRango;

        Query.Parameters.Add("@lsDuracion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsDuracion"].Value = lsDuracion;

        Query.Parameters.Add("@lsIdRango", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdRango"].Value = lsIdRango;

        Query.Parameters.Add("@lsIdVia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdVia"].Value = lsIdVia;

        Query.Parameters.Add("@lsIdPeriodo", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPeriodo"].Value = lsIdPeriodo;

        Query.Parameters.Add("@lsIdPresentacion", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdPresentacion"].Value = lsIdPresentacion;

        Query.Parameters.Add("@lsObservacion", SqlDbType.VarChar, 100);
        Query.Parameters["@lsObservacion"].Value = lsObservacion;

        Query.Parameters.Add("@lsPosologia", SqlDbType.VarChar, 10);
        Query.Parameters["@lsPosologia"].Value = lsPosologia;


        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();

        //con = bd.fnGetConn();
        //string lsSql = "UPDATE M_ART_RECETA SET CANTIDAD = " + lsCant.Replace(",", ".") + ", RANGO = " + lsRango.Replace(",", ".") + ", DURACION = " + lsDuracion.Replace(",", ".") + ", " +
        //                "IDRANGO = " + lsIdRango + ", IDVIA = " + lsIdVia + ", IDPERIODO = " + lsIdPeriodo + ", IDPRESENTACION = " + lsIdPresentacion + ", OBSERVACIONES = '" + lsObservacion + "', POSOLOGIA = " + lsPosologia.Replace(",", ".") + "  " +
        //                "WHERE IDARTRECETA = " + lsIdentifcador;

        //lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return _XML;
    }

    public string mfAsociarPosologiaPopManual(string lsIdReceta, string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                            string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion, string lsObservacion,
                            string lsPosologia, string lsFecha, string lsFechaNow, string lsPend)
    {
        string lsRet = "";

        if (lsObservacion == null)
        {
            lsObservacion = "";
        }


        con = bd.fnGetConn();
        string lsSql = "INSERT INTO M_ART_RECETA( IDARTICULO, IDRECETA, CANTIDAD, " +
                        "RANGO, DURACION, IDPRESENTACION,  " +
                        "IDRANGO, IDVIA, IDPERIODO, OBSERVACIONES, POSOLOGIA, " +
                        "RANGO_DISP, POSOLOGIA_DISP,  " +
                        "FDESPACHO, FDESPACHO_REAL, CANT_PEND, cant_desp_req, OBS_FARM) " +
                        "VALUES(" + lsIdentifcador + ", " + lsIdReceta + ", " + lsCant +
                        ", " + lsRango + ", " + lsDuracion + ", " + lsIdPresentacion +
                        ", " + lsIdRango + ", " + lsIdVia + ", " + lsIdPeriodo + ", '" +
                        lsObservacion + "', " + lsPosologia.Replace(",", ".") + " , " +
                        lsRango + ", " + lsPosologia.Replace(",", ".") + ", " +
                        "'" + lsFecha + "', '" + lsFechaNow + "', " + lsPend.Replace(",", ".") + ", " +
                        lsPosologia.Replace(",", ".") + ", '" + lsObservacion + "')";

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;
    }

    public string mfAsociarPosologiaPop(string lsIdReceta, string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
                                string lsIdRango, string lsIdVia, string lsIdPeriodo, string lsIdPresentacion, string lsObservacion,
                                string lsPosologia)
    {
        string lsRet = "";

        if (lsObservacion == null)
        {
            lsObservacion = "";
        }


        con = bd.fnGetConn();
        string lsSql = "INSERT INTO M_ART_RECETA( IDARTICULO, IDRECETA, CANTIDAD, " +
                        "RANGO, DURACION, IDPRESENTACION,  " +
                        "IDRANGO, IDVIA, IDPERIODO, OBSERVACIONES, POSOLOGIA, " +
                        "RANGO_DISP, POSOLOGIA_DISP, OBS_FARM) " +
                        "VALUES(" + lsIdentifcador + ", " + lsIdReceta + ", " + lsCant +
                        ", " + lsRango + ", " + lsDuracion + ", " + lsIdPresentacion +
                        ", " + lsIdRango + ", " + lsIdVia + ", " + lsIdPeriodo + ", '" +
                        lsObservacion + "', " + lsPosologia.Replace(",", ".") + " , " +
                        lsRango + ", " + lsPosologia.Replace(",", ".") + ", '" + lsObservacion + "')";

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;
    }

    public string mfExisteArtActa(string lsIdentificador, string lsIdArt)
    {
        string lsRet = "";

        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select count(1) " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA " +
                "where idreceta = " + lsIdentificador + " " +
                "and idarticulo = " + lsIdArt;

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }

    public string mfTieneActa(string lsIdentificador)
    {
        string lsRet = "";

        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select count(1) " +
                "from " + modConstantes.gsDbPer + "M_DESPACHOS " +
                "where idreqaut = " + lsIdentificador + " " +
                "and IDTIPODESP = 63 ";

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }

    public string mfgetElibDesp(string lsIdentificador)
    {
        string lsRet = "";

        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select count(1) " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA " +
                "where idreceta = " + lsIdentificador + " " +
                "and elib = 0 ";

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }

    public string mfgetIdArt(string lsIdentificador)
    {
        string lsRet = "";

        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select IDARTICULO " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA " +
                "where IDARTRECETA = " + lsIdentificador + " ";

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }

    public string mfgetElibArt(string lsIdentificador)
    {
        string lsRet = "";

        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select isnull(elib,0) " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA " +
                "where IDARTRECETA = " + lsIdentificador + " ";

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }

    public string mfAsignarArt(string lsIdRec, string lsIdmaterial)
    {
        string lsRet = "";

        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " insert into " + modConstantes.gsDbAB + "M_ART_RECETA( " +
                "IDRECETA, " +
                "IDARTICULO " +
                ") " +
                "values ( " +
                lsIdRec + ", " +
                lsIdmaterial +
                ")";

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();

        return lsRet;

    }

    public string mfAsociarCantidad(string lsCantidad, string lsIdentifcador)
    {
        string lsRet = "";


        con = bd.fnGetConn();
        string lsSql = "update " + modConstantes.gsDbAB + "M_ART_RECETA set " +
                        " cantidad =  " + lsCantidad.Replace(",", ".") + " " +

                        "where IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfAsociarIdarticulo(string lsIdReceta, string lsIdentifcador, string lsArt, string lschk)
    {


        string _XML = string.Empty;

        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_homologa_farm", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@lsIdReceta", SqlDbType.Int, 4);
        Query.Parameters["@lsIdReceta"].Value = lsIdReceta;

        Query.Parameters.Add("@IDARTRECETA", SqlDbType.Int, 4);
        Query.Parameters["@IDARTRECETA"].Value = lsIdentifcador;

        Query.Parameters.Add("@lsIdentifcador", SqlDbType.VarChar, 10);
        Query.Parameters["@lsIdentifcador"].Value = lsArt;

        Query.Parameters.Add("@lschk", SqlDbType.VarChar, 10);
        Query.Parameters["@lschk"].Value = lschk;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();

        return _XML;

        //string lsRet = "";


        //con = bd.fnGetConn();
        //string lsSql = "update " + modConstantes.gsDbAB + "M_ART_RECETA set " +
        //                " idarticulo =  " + lsArt + " " +
        //                "where IDARTRECETA = " + lsIdentifcador;

        //lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        //return lsRet;

    }

    public DataSet mfArtRec(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, convert(decimal(20,0),isnull(posologia,0)) posologia, " +
                "CONVERT(VARCHAR(10),ISNULL(FDESPACHO, getdate()),103) FDESPACHO, " +
                "V.UN_MED AS DESC_UN_MED, convert(decimal(20,0),isnull(D.RANGO_DISP,0)) RANGO_DISP, " +
                "convert(decimal(20,0),isnull(posologia_disp,0)) posologia_disp, " +
                "case when (dbo.fn_decimales(ISNULL(D.CANTIDAD,0)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) " +
                "else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end CANTIDAD, " +
                "CONVERT(VARCHAR(10),ISNULL(FDESPACHO_REAL, FDESPACHO),103) FDESPACHO_REAL, " +
                "convert(decimal(20,0),isnull(D.DURACION,0)) DURACION, convert(decimal(20,0),isnull(D.RANGO,0)) RANGO, " +
                "CONVERT(DECIMAL(20,0),ISNULL(D.CANT_DESP,0)) CANT_DESP, " +
                "  CASE WHEN (ISNULL(D.CANT_PEND,0) > 0 AND ISNULL(D.SALDO_SALE,0) <> 1)  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END PENDIENTE, " +
                " ISNULL(D.OBS_FARM,'') OBS_FARM," +
                "isnull(D.IDPRESENTACION,0) IDPRESENTACION, isnull(D.IDRANGO,0) IDRANGO, isnull(D.IDVIA,0) IDVIA, " +
                "isnull(D.IDPERIODO,0) IDPERIODO , OBSERVACIONES, CONVERT(DECIMAL(20,0),ISNULL(BP.SALDO,0)) SALDO " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_RECETA RE ON RE.IDRECETA = D.IDRECETA " +
                "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
                "LEFT OUTER JOIN " + modConstantes.gsDbAB + "V_BOD_ART_PER BP ON BP.IDARTICULO = D.IDARTICULO AND BP.IDBODPRERIF = RE.IDBODPRERIF " +
                "WHERE D.IDARTRECETA = " + lsIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet DetalleArtRecPendiente(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.


        lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end + " +
                "' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA, CONVERT(VARCHAR(10),ISNULL(FDESPACHO, GETDATE()),103) FDESPACHO, " +
                "V.UN_MED AS DESC_UN_MED, " +
                "case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end CANTIDAD, " +
                "convert(decimal(20,0),ISNULL(D.DURACION,0)) DURACION, convert(decimal(20,0),ISNULL(D.RANGO,0)) RANGO, convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) RANGO_DISP," +
                "D.IDPRESENTACION , D.IDRANGO, D.IDVIA, D.IDPERIODO, CONVERT(DECIMAL(20,0),ISNULL(BP.SALDO,0)) SALDO, " +
                "ISNULL(VIA.DESCRIPCION,'-') DVIA, ISNULL(R.DESCRIPCION,'-') DRANGO, isnull(cant_desp_req,0) cant_desp_req, ISNULL(OBS_FARM,'') OBS_FARM," +
                "  CASE WHEN (ISNULL(D.CANT_PEND,0) > 0 AND ISNULL(D.SALDO_SALE,0) <> 1)  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END PENDIENTE, " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, ISNULL(PE.DESCRIPCION,'-') DPERIODO, OBSERVACIONES, " +
                "convert(decimal(20,0),POSOLOGIA) POSOLOGIA, convert(decimal(20,0),POSOLOGIA_DISP) POSOLOGIA_DISP, v.UNI_MIN " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_RECETA RE ON RE.IDRECETA = D.IDRECETA " +
                "LEFT OUTER JOIN " + modConstantes.gsDbAB + "V_BOD_ART_PER BP ON BP.IDARTICULO = D.IDARTICULO AND BP.IDBODPRERIF = RE.IDBODPRERIF " +
                "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
                "  left outer join " + modConstantes.gsDbAB + "[M_VIA] VIA ON VIA.IDVIA = D.IDVIA " +
                "  left outer JOIN " + modConstantes.gsDbAB + "[M_RANGO] R ON R.IDRANGO = D.IDRANGO " +
                "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO " +
                "WHERE D.IDRECETA = " + lsIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet DetalleArtRecDispensar(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.


        lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end + " +
                "' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA, " +
                "V.UN_MED AS DESC_UN_MED, CONVERT(DECIMAL(20,0),ISNULL(BP.SALDO,0)) SALDO, ISNULL(OBS_FARM,'') OBS_FARM, isnull(cant_desp_req,0) cant_desp_req, " +
                "case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end CANTIDAD, " +
                "convert(decimal(20,0),ISNULL(D.DURACION,0)) DURACION, convert(decimal(20,0),ISNULL(D.RANGO,0)) RANGO, convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) RANGO_DISP," +
                "D.IDPRESENTACION , D.IDRANGO, D.IDVIA, D.IDPERIODO, CONVERT(VARCHAR(10),ISNULL(FDESPACHO, GETDATE()),103) FDESPACHO, " +
                "ISNULL(VIA.DESCRIPCION,'-') DVIA, ISNULL(R.DESCRIPCION,'-') DRANGO, " +
                "  CASE WHEN (ISNULL(D.CANT_PEND,0) > 0 AND ISNULL(D.SALDO_SALE,0) <> 1)  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END PENDIENTE, " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, ISNULL(PE.DESCRIPCION,'-') DPERIODO, OBSERVACIONES, " +
                "convert(decimal(20,0),POSOLOGIA) POSOLOGIA, convert(decimal(20,0),POSOLOGIA_DISP) POSOLOGIA_DISP, v.UNI_MIN " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_RECETA RE ON RE.IDRECETA = D.IDRECETA " +
                "LEFT OUTER JOIN " + modConstantes.gsDbAB + "V_BOD_ART_PER BP ON BP.IDARTICULO = D.IDARTICULO AND BP.IDBODPRERIF = RE.IDBODPRERIF " +
                "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
                "  left outer join " + modConstantes.gsDbAB + "[M_VIA] VIA ON VIA.IDVIA = D.IDVIA " +
                "  left outer JOIN " + modConstantes.gsDbAB + "[M_RANGO] R ON R.IDRANGO = D.IDRANGO " +
                "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO " +
                "WHERE D.IDRECETA = " + lsIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet DetalleArtRec(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        //lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, " +
        //        "V.UN_MED AS DESC_UN_MED, ISNULL(D.CANTIDAD,0) CANTIDAD, ISNULL(D.DURACION,0) DURACION, ISNULL(D.RANGO,0) RANGO, " +
        //        "D.IDPRESENTACION , D.IDRANGO, D.IDVIA, D.IDPERIODO, " +
        //        "ISNULL(VIA.DESCRIPCION,'-') DVIA, ISNULL(R.DESCRIPCION,'-') DRANGO, " +
        //        "ISNULL(UN_MED,'-') DPRESENT, ISNULL(PE.DESCRIPCION,'-') DPERIODO, OBSERVACIONES, POSOLOGIA " +
        //        "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
        //        "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
        //        "  left outer join " + modConstantes.gsDbAB + "[M_VIA] VIA ON VIA.IDVIA = D.IDVIA " +
        //        "  left outer JOIN " + modConstantes.gsDbAB + "[M_RANGO] R ON R.IDRANGO = D.IDRANGO " +
        //        "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PRESENTACION] P ON P.IDPRESENTACION = D.IDPRESENTACION " +
        //        "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO " + 
        //        "WHERE D.IDRECETA = " + lsIdentificador;

        lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then " +
                "convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else " +
                "convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end + " +
                "' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA, " +
                "V.UN_MED AS DESC_UN_MED, " +
                "case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then " +
                "convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else " +
                "convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end CANTIDAD, " +
                "convert(decimal(20,0),ISNULL(D.DURACION,0)) DURACION, convert(decimal(20,0),ISNULL(D.RANGO,0)) RANGO, " +
                "convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) RANGO_DISP," +
                "D.IDPRESENTACION , D.IDRANGO, D.IDVIA, D.IDPERIODO, " +
                "ISNULL(VIA.DESCRIPCION,'-') DVIA, ISNULL(R.DESCRIPCION,'-') DRANGO, " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, ISNULL(PE.DESCRIPCION,'-') DPERIODO, " +
                "OBSERVACIONES, CONVERT(VARCHAR(10),ISNULL(D.FDESPACHO,''),103) FDESPACHO, " +
                "convert(decimal(20,0),POSOLOGIA) POSOLOGIA, convert(decimal(20,0),POSOLOGIA_DISP) POSOLOGIA_DISP, v.UNI_MIN " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
                "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
                "  left outer join " + modConstantes.gsDbAB + "[M_VIA] VIA ON VIA.IDVIA = D.IDVIA " +
                "  left outer JOIN " + modConstantes.gsDbAB + "[M_RANGO] R ON R.IDRANGO = D.IDRANGO " +
                "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO " +
                "WHERE D.IDRECETA = " + lsIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfElimAsignarArt(string lsIdentifcador)
    {
        string lsRet = "";

        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " delete from " + modConstantes.gsDbAB + "M_ART_RECETA " +
                        "where IDARTRECETA = " + lsIdentifcador; ;

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfObtenerCantArt(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select count(1) " +
                "from " + modConstantes.gsDbAB + "[M_ART_RECETA] " +
                "where ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfUpdateCantDisp(string lsIdentifcador, string asCant)
    {
        string lsRet = "";

        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "M_ART_RECETA set cant_desp_req = " + asCant.Replace(",", ".") + " " +
                        "where IDARTRECETA = " + lsIdentifcador; ;

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }


    public string mfUpdateCantDisp(string lsIdentifcador, string asCant, string asTipo, string valor, string FReal, string FNow)
    {
        string lsRet = "";

        string lsSql = "";

        con = bd.fnGetConn();

        if (asTipo == "1")
        {

            lsSql = "update " + modConstantes.gsDbAB + "M_ART_RECETA set cant_desp_req = " + asCant.Replace(",", ".") + ", " +
                    "RANGO_DISP = " + valor.Replace(",", ".") + ", POSOLOGIA_DISP = " + asCant.Replace(",", ".") + ", " +
                    "FDESPACHO_REAL = '" + FReal + "', FDESPACHO = '" + FNow + "', " +
                    " CANT_PEND = CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(POSOLOGIA,0) - (ISNULL(CANT_DESP,0) + " + asCant.Replace(",", ".") + "))) >= 0 " +
                    " THEN  CONVERT(DECIMAL(20,0),(ISNULL(POSOLOGIA,0) - (ISNULL(CANT_DESP,0) + " + asCant.Replace(",", ".") + "))) " +
                    " ELSE 0 END " +
                    "where IDARTRECETA = " + lsIdentifcador;
        }
        else
        {

            lsSql = "update " + modConstantes.gsDbAB + "M_ART_RECETA set cant_desp_req = " + asCant.Replace(",", ".") + ", " +
                    "POSOLOGIA_DISP = " + asCant.Replace(",", ".") + ", CANT_PEND = 0, RANGO_DISP = RANGO - ISNULL(ACUM_RANGO,0), " +
                    "FDESPACHO_REAL = '" + FReal + "', FDESPACHO = '" + FNow + "' " +
                            "where IDARTRECETA = " + lsIdentifcador;
        }

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    #endregion

    #region dispensacion


    public DataSet mfDispPendSaldo()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where IDRECETA > 0 AND ( idestado <> 3 ) ";
        string lsOrder = " order by FOLIO desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,FDESPACHO,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,FDESPACHO,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and ARTICULO like '%" + ls_Art + "%' ";


        // Si corresponde agrega bodega.
        if (ls_Bod != "0")
            lsWhere += " AND ( IDBODEGA = " + ls_Bod + " ) ";



        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND ( CODUNIOP = " + ls_IdServ + " ) ";




        lsSelect = " select FOLIO, RMANUAL, " +
                    " NUMOSAL, FDESPACHO, " +
                    " CODARTICULO,ARTICULO, " +
                    " CANTIDAD, UNIDAD, SALDO, " +
                    " CODUNIOP, IDBODEGA, IDDETDESP " +
                        "from V_PEND_DISP  ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public string mfGetMaxFechaDesp(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            lsSql = "select MAX(FDESPACHO)  from " + modConstantes.gsDbAB + "M_ART_RECETA  " +
                    "WHERE IDRECETA = " + asId;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public DataSet mfdllListaUserBodegas(string Identificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT U2.IDUSUARIO CODIGO,  U1.NOMBRE DESCRIPCION  " +
                "FROM " + modConstantes.gsDbPer + "[TG_BOD_PERIFERICAS] B " +
                "INNER JOIN " + modConstantes.gsDbPer + "M_USR_BOD C ON C.IDBODPRERIF = B.IDBODPRERIF AND C.IDESTADO <> 3 " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_USUARIOS U1 ON U1.IDUSUARIO = C.IDUSUARIO " +
                "INNER JOIN M_USUARIOS U2 ON U2.RUT = U1.RUT " +
                "WHERE U1.IDESTADO <> 3 " +
                "and C.IDBODPRERIF = " + Identificador + " " +
                "order by U1.NOMBRE ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
    public DataSet mfdllUserBodegas(string Identificador, string asInst)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT B.IDBODPRERIF CODIGO,  B.DESCRIPCION_LARGA DESCRIPCION " +
                "FROM " + modConstantes.gsDbPer + "[TG_BOD_PERIFERICAS] B " +
                "INNER JOIN " + modConstantes.gsDbPer + "M_USR_BOD C ON C.IDBODPRERIF = B.IDBODPRERIF AND C.IDESTADO <> 3 " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_USUARIOS U1 ON U1.IDUSUARIO = C.IDUSUARIO " +
                "INNER JOIN M_USUARIOS U2 ON U2.RUT = U1.RUT " +
                "WHERE B.IDESTADO <> 3 " +
                "and U2.IDUSUARIO = " + Identificador + " " +
                "and B.id_inst = " + asInst + " " +
                "order by B.DESCRIPCION_LARGA ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfdllListaUserBodegas(string Identificador, string asInst)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT U1.IDUSUARIO CODIGO,  U1.NOMBRE DESCRIPCION " +
                "FROM " + modConstantes.gsDbPer + "M_USR_BOD C  " +
                "INNER JOIN " + modConstantes.gsDbRH + "M_USUARIOS U1 ON U1.IDUSUARIO = C.IDUSUARIO " +
                "WHERE C.IDESTADO <> 3 " +
                "and C.IDBODPRERIF = " + Identificador + " " +
                "order by U1.NOMBRE ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfCargaCertificados(string lsIdentificador)
    {
        DataSet aoBind;
        string lsSql = "";

        lsSql = "SELECT D.IDDESPACHO, D.NUMOSAL, CONVERT(varchar(10),D.F_H_CREACION,103) FECHA , V.DESCRIPCION ESTADO " +
                    "FROM " + modConstantes.gsDbPer + "M_DESPACHOS D " +
                    "INNER JOIN " + modConstantes.gsDbPer + "V_ESTADOS V ON V.IDESTADO = D.IDESTADO " +
                    "WHERE D.idreqaut = " + lsIdentificador + " " +
                    "AND D.IDTIPODESP = 63 ";

        con = bd.fnGetConn();
        aoBind = bd.Fill(con, lsSql);
        con.Close();
        return aoBind;
    }

    public DataSet mfValidaSaldo(string asIdentificador, string asIbBod, string asEstado)
    {
        DataSet aoCod;

        string lsSql;


        lsSql = "SELECT V.CODARTICULO, CONVERT(DECIMAL(20,0),BP.SALDO) SALDO, CONVERT(DECIMAL(20,0),SUM(d.cant_desp_req)) SUMA " +
                    "FROM M_RECETA R " +
                    "INNER JOIN M_ART_RECETA D ON R.IDRECETA = D.IDRECETA and d.cant_desp_req > 0 and D.IDESTADO <> 3 " +
                    "INNER JOIN " + modConstantes.gsDbPer + "M_BOD_ART BP ON BP.IDARTICULO = D.IDARTICULO AND BP.IDBODPRERIF = R.IDBODPRERIF " +
                    "INNER JOIN v_articulos V ON V.IDARTICULO = BP.IDARTICULO " +
                    "WHERE R.RUT = '" + asIdentificador + "' " +
                    "and r.IDESTADO = " + asEstado + " " +
                    "and R.IDBODPRERIF = " + asIbBod + " " +
                    "GROUP BY V.CODARTICULO, BP.SALDO " +
                    "HAVING SUM(d.cant_desp_req) > BP.SALDO ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfValidaCantDispRut(string asIdentificador, string asIbBod, string asEstado)
    {
        DataSet aoCod;

        string lsSql;


        lsSql = "SELECT R.FOLIO,  SUM(ISNULL(DT.cant_desp_req,0)) cant_desp_req " +
                "FROM M_RECETA R " +
                "INNER JOIN M_ART_RECETA DT ON DT.IDRECETA = R.IDRECETA " +
                "WHERE RUT =  '" + asIdentificador + "' " +
                "and R.IDESTADO =  " + asEstado + " " +
                "and R.IDBODPRERIF =  " + asIbBod + " " +
                "GROUP BY R.FOLIO " +
                "HAVING SUM(ISNULL(DT.cant_desp_req, 0)) <= 0 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfGetAcumDias(string lsIdentificador)
    {
        string cantidad = "0";
        string lsSql;

        // Recupera suma cantidad despachar.

        lsSql = "  SELECT  Convert(decimal(20,0),isnull(sum(ACUM_RANGO),0)) " +
                    "FROM  " + modConstantes.gsDbAB + "M_ART_RECETA " +
                    "where IDARTRECETA = " + lsIdentificador + " ";



        con = bd.fnGetConn();
        cantidad = bd.ExecuteScalar(con, lsSql);
        con.Close();


        return cantidad;

    }


    public string mfTraeDias(string uno, string dos, string tres)
    {
        Int32 dias = 0;
        if (Convert.ToInt32(tres.Replace(",", ".")) == 0)
            dias = Convert.ToInt32(uno.Replace(",", "."));
        else
            dias = (Convert.ToInt32(uno.Replace(",", ".")) * Convert.ToInt32(dos.Replace(",", "."))) / Convert.ToInt32(tres.Replace(",", "."));

        return dias.ToString();
    }

    public string mfGuardaFechas(string asIdentificador)
    {
        DataSet aoCod;
        string fecha;
        string fecha_now;
        string lsSQL = "select IDARTRECETA from M_ART_RECETA where IDRECETA = " + asIdentificador + " AND elib = 0 ";
        string salida = "";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSQL);
        con.Close();

        foreach (DataRow row in aoCod.Tables[0].Rows)
        {
            lsSQL = "  SELECT [dbo].[fn_get_fecha_entrega](" + Convert.ToString(row["IDARTRECETA"]) + ")";

            con = bd.fnGetConn();
            fecha_now = bd.ExecuteScalar(con, lsSQL);
            con.Close();

            fecha = mfFechaDespacho(fecha_now);

            salida = mfUpdateFechas(Convert.ToString(row["IDARTRECETA"]), fecha_now, fecha);
            if (salida != "") salida = salida + salida;
        }

        return salida;

    }

    public string mfUpdateFechas(string asIdentficador, string asFechaNow, string lsFecha)
    {
        string lsSql = "";

        lsSql = " update " + modConstantes.gsDbAB + "M_ART_RECETA set " +
                "FDESPACHO_REAL = '" + asFechaNow + "', " +
                "FDESPACHO =  '" + lsFecha + "' " +
                "WHERE IDARTRECETA =  " + asIdentficador;


        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfFechaDespacho(string fecha_now)
    {

        int i = 0;
        string cont = "0";
        string cont_aux = "0";

        //string fecha_now = "";
        string lsSql;
        byte data;
        byte data_aux = 9;


        // Recupera suma cantidad despachar.

        //lsSql = "  SELECT [dbo].[fn_get_fecha_entrega](" + lsIdentificador + ")";

        con = bd.fnGetConn();
        //fecha_now = bd.ExecuteScalar(con, lsSql);

        while (i <= 6)
        {
            data = (byte)Convert.ToDateTime(fecha_now).DayOfWeek;

            lsSql = " select count(1) " +
                    "from m_feriados " +
                    "where convert(varchar(10),feriado,103) = convert(varchar(10),'" + fecha_now + "',103)";

            cont = bd.ExecuteScalar(con, lsSql);

            if (cont == "0" && data != 0 && data != 6) break;


            if (data == 0)
            {
                fecha_now = Convert.ToDateTime(fecha_now).AddDays(1).ToString("dd/MM/yyyy");
            }
            else if (data == 6)
            {
                fecha_now = Convert.ToDateTime(fecha_now).AddDays(-1).ToString("dd/MM/yyyy");
            }
            else
            {

                if (data_aux == 0)
                {
                    fecha_now = Convert.ToDateTime(fecha_now).AddDays(1).ToString("dd/MM/yyyy");
                }
                else if (data_aux == 6)
                {
                    fecha_now = Convert.ToDateTime(fecha_now).AddDays(-1).ToString("dd/MM/yyyy");
                }
                else
                {
                    if (data_aux > data)
                        fecha_now = Convert.ToDateTime(fecha_now).AddDays(-1).ToString("dd/MM/yyyy");
                    else
                        fecha_now = Convert.ToDateTime(fecha_now).AddDays(1).ToString("dd/MM/yyyy");
                }

            }

            data_aux = data;
            i++;
        }

        con.Close();

        return fecha_now;

    }

    //public string mfFechaDespacho(string fecha_now)
    //{

    //    int i = 0;
    //    string cont = "0";
    //    //string fecha_now = "";
    //    string lsSql;
    //    byte data;

    //    // Recupera suma cantidad despachar.

    //    //lsSql = "  SELECT [dbo].[fn_get_fecha_entrega](" + lsIdentificador + ")";

    //    con = bd.fnGetConn();
    //    //fecha_now = bd.ExecuteScalar(con, lsSql);

    //    while (i <= 6)
    //    {
    //        data = (byte)Convert.ToDateTime(fecha_now).DayOfWeek;

    //        lsSql = " select count(1) " +
    //                "from m_feriados " +
    //                "where convert(varchar(10),feriado,103) = convert(varchar(10),'" + fecha_now + "',103)";

    //        cont = bd.ExecuteScalar(con, lsSql);

    //        if (cont == "0" && data != 0 && data != 6) break;


    //        if (data == 0)
    //        {
    //            fecha_now = Convert.ToDateTime(fecha_now).AddDays(-2).ToString("dd/MM/yyyy");
    //        }
    //        else if (data == 6)
    //        {
    //            fecha_now = Convert.ToDateTime(fecha_now).AddDays(-1).ToString("dd/MM/yyyy");
    //        }
    //        else
    //        {
    //            fecha_now = Convert.ToDateTime(fecha_now).AddDays(-1).ToString("dd/MM/yyyy");
    //        }

    //        i++;
    //    }

    //    con.Close();

    //    return fecha_now;

    //}


    public string mfValidaCantValida(string lsIdentificador)
    {
        string cantidad = "0";
        string lsSql;

        // Recupera suma cantidad despachar.

        lsSql = "  SELECT  isnull(sum(cant_desp_req),0) " +
                    "FROM  " + modConstantes.gsDbAB + "M_ART_RECETA " +
                    "where idreceta = " + lsIdentificador + " ";



        con = bd.fnGetConn();
        cantidad = bd.ExecuteScalar(con, lsSql);
        con.Close();


        return cantidad;

    }

    public string mfAsociarCantidadValida(string lsCantidad, string lsIdentifcador)
    {
        string lsRet = "";


        con = bd.fnGetConn();
        string lsSql = "update " + modConstantes.gsDbAB + "M_ART_RECETA set " +
                        "cant_desp_req =   " + lsCantidad.Replace(",", ".") + " " +
                        "where IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public DataSet ListadoRecDesp(string asID)
    {
        DataSet aoCod;

        string lsSql;
        string lsOrder = " order by DE.F_H_CREACION DESC "; ;

        string lsWhere = " ";

        //if (ls_f_d != "")
        //{

        //    lsWhere += " and  ( DE.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
        //                    "   DE.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";
        //}

        if (ls_tipo != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";

        }

        //// Si se ingresó Numero la agrega al Where.
        //if (ls_num != "")
        //{
        //    if (lsWhere != "")
        //        lsWhere += " and ";

        //    lsWhere += " ( R.FOLIO = " + ls_num + " ) ";

        //}

        //// Si se ingresó Articulo Solicitud la agrega al Where.
        //if (ls_Art != "")
        //{
        //    if (lsWhere != "")
        //        lsWhere += " and ";

        //    lsWhere += " ( v.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";

        //}

        //// Si se ingresó Código Solicitud la agrega al Where.
        //if (ls_Codigo != "")
        //{
        //    if (lsWhere != "")
        //        lsWhere += " and ";

        //    lsWhere += " ( v.CODARTICULO = '" + ls_Codigo + "' ) ";

        //}

        lsSql = "select M.IDARTRECETA ,R.FOLIO,CONVERT(VARCHAR(10),r.F_H_CREACION, 103) F_H_CREACION, V.DESCRIPCION_LARGA, " +
                " (case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0)))   " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0)))   " +
                " end + ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA,  " +
                " case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0))) " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0))) end CANTIDAD, " +
                "  EST.DESCRIPCION ESTADO, UN.DESCRIPCION UNIDAD, " +
                " CONVERT(VARCHAR(10),ISNULL(M.FULT_DESP,''),103) FENTREGA, convert(decimal(20,0),ISNULL(DP.RANGO_DISP_D,0) )  DIAS_TRATA, " +
                " convert(decimal(20,0),POSOLOGIA_DISP) CANT_TOTAL, CONVERT(VARCHAR(10),ISNULL(DP.FDESPACHO_D,''),103) FPDESPACHO, " +
                "  CASE WHEN ISNULL(M.CANT_PEND,0) > 0  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(M.cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(M.cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(M.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(M.CANT_PEND,0)) END PENDIENTE, " +
                    " CONVERT(DECIMAL(20,0),(dp.CANT_DESP - DP.CANT_DEV))  CANT_DESP, CONVERT(VARCHAR(10),DE.F_H_CREACION, 103) F_H_DESP " +
                " from M_ART_RECETA M  " +
                " INNER JOIN M_RECETA R ON R.IDRECETA = M.IDRECETA  " +
                " INNER JOIN v_articulos V ON V.IDARTICULO = M.IDARTICULO  " +
                " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = M.IDPERIODO " +
                " INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
                " INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
                " INNER JOIN " + modConstantes.gsDbPer + "M_DESPACHOS DE ON DE.idreqaut = R.IDRECETA AND IDTIPODESP = 63 " +
                " INNER JOIN " + modConstantes.gsDbPer + "M_DETDESP DP ON DP.IDDESPACHO = DE.IDDESPACHO AND DP.IDMATERIAL = M.IDARTICULO " +
                " WHERE RUT =  '" + asID + "' " +
                " AND ISNULL(M.IDESTADO,1) <> 3 " +
                //" and dp.CANT_DESP > 0 " +
                " AND (dp.CANT_DESP - DP.CANT_DEV) > 0 " +
                " AND (DE.F_H_CREACION >= DATEADD(DAY, -120, getdate()) and DE.F_H_CREACION <= DATEADD(DAY, 30, getdate())) " +
                " and R.IDESTADO IN (4,13) " + lsWhere + " " + lsOrder;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet ListadoMediRecDesp()
    {
        DataSet aoCod;

        string lsSql;
        string lsOrder = " order by DE.F_H_CREACION DESC "; ;

        string lsWhere = " ";

        if (ls_f_d != "")
        {

            lsWhere += " and  ( DE.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   DE.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";
        }

        if (ls_tipo != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";

        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( R.FOLIO = " + ls_num + " ) ";

        }

        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( v.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";

        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( v.CODARTICULO = '" + ls_Codigo + "' ) ";

        }

        lsSql = "select M.IDARTRECETA ,R.FOLIO,CONVERT(VARCHAR(10),r.F_H_CREACION, 103) F_H_CREACION, V.DESCRIPCION_LARGA, " +
                " (case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0)))   " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0)))   " +
                " end + ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA,  " +
                " case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0))) " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0))) end CANTIDAD, " +
                "  EST.DESCRIPCION ESTADO, UN.DESCRIPCION UNIDAD, " +
                " CONVERT(VARCHAR(10),ISNULL(M.FULT_DESP,''),103) FENTREGA, convert(decimal(20,0),ISNULL(DP.RANGO_DISP_D,0) )  DIAS_TRATA, " +
                " convert(decimal(20,0),POSOLOGIA_DISP) CANT_TOTAL, CONVERT(VARCHAR(10),ISNULL(DP.FDESPACHO_D,''),103) FPDESPACHO, " +
                //"  CASE WHEN ISNULL(M.CANT_PEND,0) = 0  " +
                //" THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(M.cant_desp_req,0)))) >= 0 " +
                //" THEN  CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(M.cant_desp_req,0)))) " +
                //" ELSE 0 END " +
                //" ELSE CONVERT(DECIMAL(20,0),M.CANT_PEND) END PENDIENTE, " +
                "  CASE WHEN ISNULL(M.CANT_PEND,0) > 0  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(M.cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(M.cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(M.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(M.CANT_PEND,0)) END PENDIENTE, " +
                    " CONVERT(DECIMAL(20,0),(dp.CANT_DESP - DP.CANT_DEV))  CANT_DESP, CONVERT(VARCHAR(10),DE.F_H_CREACION, 103) F_H_DESP " +
                " from M_ART_RECETA M  " +
                " INNER JOIN M_RECETA R ON R.IDRECETA = M.IDRECETA  " +
                " INNER JOIN v_articulos V ON V.IDARTICULO = M.IDARTICULO  " +
                " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = M.IDPERIODO " +
                " INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
                " INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
                " INNER JOIN " + modConstantes.gsDbPer + "M_DESPACHOS DE ON DE.idreqaut = R.IDRECETA AND IDTIPODESP = 63 " +
                " INNER JOIN " + modConstantes.gsDbPer + "M_DETDESP DP ON DP.IDDESPACHO = DE.IDDESPACHO AND DP.IDMATERIAL = M.IDARTICULO " +
                " WHERE RUT =  '" + ls_rut + "' " +
                " AND ISNULL(M.IDESTADO,1) <> 3  " +
                //" and dp.CANT_DESP > 0 " +
                " AND (dp.CANT_DESP - DP.CANT_DEV) > 0 " +
                " and DE.IDESTADO not IN (3,8) " + lsWhere + " " + lsOrder;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet ListadoMediRecValidar(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;
        string lsOrder = "order by r.F_H_CREACION, R.FOLIO DESC "; ;

        string lsWhere = " ";

        if (ls_tipo != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";

        }

        lsSql = "select M.IDARTRECETA ,R.FOLIO,CONVERT(VARCHAR(10),r.F_H_CREACION, 103) F_H_CREACION, V.DESCRIPCION_LARGA, " +
                " (case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0)))   " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0)))   " +
                " end + ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA,  " +
                " case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0))) " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0))) end CANTIDAD, " +
                "  EST.DESCRIPCION ESTADO, UN.DESCRIPCION UNIDAD, " +
                " CONVERT(VARCHAR(10),ISNULL(M.FULT_DESP,''),103) FENTREGA, convert(decimal(20,0),ISNULL(M.RANGO_DISP,0))  DIAS_TRATA, " +
                " convert(decimal(20,0),POSOLOGIA_DISP) CANT_TOTAL, CONVERT(VARCHAR(10),ISNULL(M.FDESPACHO,''),103) FPDESPACHO, " +
                //"  CASE WHEN ISNULL(M.CANT_PEND,0) = 0  " +
                //" THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                //" THEN  CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                //" ELSE 0 END " +
                //" ELSE CONVERT(DECIMAL(20,0),M.CANT_PEND) END PENDIENTE, " +
                "  CASE WHEN ISNULL(M.CANT_PEND,0) > 0  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(M.POSOLOGIA,0) - (ISNULL(M.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(M.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(M.CANT_PEND,0)) END PENDIENTE, " +
                " ISNULL(M.POSOLOGIA,0) POSOLOGIA, ISNULL(M.RANGO,0) RANGO, ISNULL(M.CANT_DESP,0)  CANT_DESP, ISNULL(M.ACUM_RANGO,0) ACUM_RANGO " +
                " from M_ART_RECETA M  " +
                " INNER JOIN M_RECETA R ON R.IDRECETA = M.IDRECETA  " +
                " INNER JOIN v_articulos V ON V.IDARTICULO = M.IDARTICULO  " +
                " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = M.IDPERIODO " +
                " INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
                " INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
                " WHERE RUT =  '" + lsIdentificador + "' " +
                " AND ISNULL(M.IDESTADO,1) <> 3 " +
                " AND ISNULL(M.CANT_DESP,0) > 0 " +
                " and R.IDESTADO IN (4 , 13) " + lsWhere + " " +
                " AND (R.F_H_CREACION >= DATEADD(DAY, -120, getdate()) and R.F_H_CREACION <= DATEADD(DAY, 30, getdate())) " +
                "AND EXISTS (SELECT OB.IDRECETA " +
                "FROM M_OBSERVACIONES_RECETA OB " +
                "WHERE OB.IDRECETA = R.IDRECETA " +
                ")" + lsOrder;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet DetalleArtRecValidar(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.

        lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) " +
                "else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end + " +
                "' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA, " +
                "V.UN_MED AS DESC_UN_MED, ISNULL(V.CALCULAR,2) CALCULAR, " +
                "case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) " +
                "else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end CANTIDAD, " +
                "convert(decimal(20,0),ISNULL(D.DURACION,0)) DURACION, convert(decimal(20,0),ISNULL(D.RANGO,0)) RANGO, " +
                "CASE WHEN ISNULL(D.RANGO_DISP,0) < 0 THEN 0 ELSE convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) END RANGO_DISP, " +
                "convert(decimal(20,0),POSOLOGIA_DISP) POSOLOGIA_DISP," +
                "D.IDPRESENTACION , D.IDRANGO, D.IDVIA, D.IDPERIODO, " +
                "ISNULL(VIA.DESCRIPCION,'-') DVIA, ISNULL(R.DESCRIPCION,'-') DRANGO, " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, ISNULL(PE.DESCRIPCION,'-') DPERIODO, OBSERVACIONES, " +
                "convert(decimal(20,0),POSOLOGIA) POSOLOGIA, v.UNI_MIN, ISNULL(OBS_FARM,'') OBS_FARM," +
                " 0 CANT_ENTREGA, CONVERT(VARCHAR(10),D.FDESPACHO,103) FDESPACHO, " +
                "  CASE WHEN (ISNULL(D.CANT_PEND,0) > 0   AND ISNULL(D.SALDO_SALE,0) <> 1) " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END PENDIENTE, " +
                " CONVERT(DECIMAL(20,0),ISNULL(BP.SALDO,0)) SALDO, D.OBSERVACIONES OBS,  CONVERT(DECIMAL(20,0),isnull(cant_desp_req,0)) cant_desp_req, " +
                " CASE WHEN ISNULL(V.CALCULAR,2) = 1 THEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA_DISP,0))) ELSE CONVERT(DECIMAL(20,0),isnull(cant_desp_req,0)) END cant_desp_2 " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
                "INNER JOIN M_RECETA RE ON RE.IDRECETA = D.IDRECETA " +
                "LEFT OUTER JOIN " + modConstantes.gsDbAB + "V_BOD_ART_PER BP ON BP.IDARTICULO = D.IDARTICULO AND RE.IDBODPRERIF = BP.IDBODPRERIF " +
                "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
                "  left outer join " + modConstantes.gsDbAB + "[M_VIA] VIA ON VIA.IDVIA = D.IDVIA " +
                "  left outer JOIN " + modConstantes.gsDbAB + "[M_RANGO] R ON R.IDRANGO = D.IDRANGO " +
                "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO " +
                "WHERE D.IDRECETA = " + lsIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet DetalleArtRecPreparar(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.

        lsSql = "select D.IDARTRECETA, V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, " +
                "(case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end + " +
                "' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA, " +
                "V.UN_MED AS DESC_UN_MED, " +
                "case when ((ISNULL(D.CANTIDAD,0)-round(ISNULL(D.CANTIDAD,0),0,1)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(D.CANTIDAD,0))) else convert(varchar(10),convert(decimal(20,0),ISNULL(D.CANTIDAD,0))) end CANTIDAD, " +
                "convert(decimal(20,0),ISNULL(D.DURACION,0)) DURACION, convert(decimal(20,0),ISNULL(D.RANGO,0)) RANGO, convert(decimal(20,0),ISNULL(D.RANGO_DISP,0)) RANGO_DISP," +
                "D.IDPRESENTACION , D.IDRANGO, D.IDVIA, D.IDPERIODO, " +
                "ISNULL(VIA.DESCRIPCION,'-') DVIA, ISNULL(R.DESCRIPCION,'-') DRANGO, " +
                "case when v.UNI_MIN = 'S/M' then ISNULL(V.UN_MED,'-') else ISNULL(V.UNI_MIN,'-') end DPRESENT, ISNULL(PE.DESCRIPCION,'-') DPERIODO, OBSERVACIONES, " +
                "convert(decimal(20,0),POSOLOGIA ) POSOLOGIA, convert(decimal(20,0),POSOLOGIA_DISP ) POSOLOGIA_DISP, v.UNI_MIN, " +
                " 0 CANT_ENTREGA, CONVERT(VARCHAR(10),ISNULL(FDESPACHO, GETDATE()),103) FDESPACHO,  " +
                "  CASE WHEN (ISNULL(D.CANT_PEND,0) > 0 AND ISNULL(D.SALDO_SALE,0) <> 1)  " +
                " THEN CASE WHEN CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) > 0 " +
                " THEN  CONVERT(DECIMAL(20,0),(ISNULL(D.POSOLOGIA,0) - (ISNULL(D.CANT_DESP,0) + isnull(cant_desp_req,0)))) " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END " +
                " ELSE CONVERT(DECIMAL(20,0),ISNULL(D.CANT_PEND,0)) END PENDIENTE, " +
                " CONVERT(DECIMAL(20,0),ISNULL(BP.SALDO,0)) SALDO, ISNULL(OBS_FARM,'') OBS, isnull(cant_desp_req,0) cant_desp_req " +
                "from " + modConstantes.gsDbAB + "M_ART_RECETA D " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_RECETA RE ON RE.IDRECETA = D.IDRECETA " +
                "LEFT OUTER JOIN " + modConstantes.gsDbAB + "V_BOD_ART_PER BP ON BP.IDARTICULO = D.IDARTICULO AND BP.IDBODPRERIF = RE.IDBODPRERIF " +
                "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = D.IDARTICULO " +
                "  left outer join " + modConstantes.gsDbAB + "[M_VIA] VIA ON VIA.IDVIA = D.IDVIA " +
                "  left outer JOIN " + modConstantes.gsDbAB + "[M_RANGO] R ON R.IDRANGO = D.IDRANGO " +
                "  LEFT OUTER JOIN " + modConstantes.gsDbAB + "[M_PERIODO] PE ON PE.IDPERIODO = D.IDPERIODO " +
                "WHERE D.IDRECETA = " + lsIdentificador + " " +
                "AND ISNULL(D.IDESTADO,1) <> 3 ";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarFarmaciaValidar()
    {
        DataSet aoCod;
        string ls_where = "";


        //if (ls_All == "NotAll")

        //    ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0 ) ";
        //else
        //    ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0 ) ";

        ls_where = "WHERE ( r.idestado in (2,13) and R.FOLIO > 0 ) ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_CREACION DESC ";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }



        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }

        if (ls_pat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_PAT like '%" + ls_pat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_pat_pac + " , ";
        }

        if (ls_mat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_MAT like '%" + ls_mat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_mat_pac + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        //is_select = "SELECT R.IDRECETA, R.FOLIO, CONVERT(VARCHAR(10),R.F_H_CREACION, 103) F_H_CREACION, (R.NOMBRE + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE," + 
        //    " R.RUT, EST.DESCRIPCION ESTADO, " +
        //    "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  R.F_H_MOD, " +
        //    "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
        //    "UN.DESCRIPCION UNIDAD, (select convert(decimal(20,0),Max(rango)) from M_ART_RECETA where IDRECETA = R.IDRECETA) DIAS, " +
        //    " CASE WHEN R.IDESTADO = 13 THEN CONVERT(VARCHAR(10), R.FULT_DESP,103) ELSE  CONVERT(VARCHAR(10), GETDATE(),103) END FULT_DESP, " +
        //    "dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FPDESPACHO " +
        //    "FROM M_RECETA R " +
        //    "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
        //    "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO ";


        is_select = "SELECT R.IDRECETA, R.FOLIO, CONVERT(VARCHAR(10),R.F_H_CREACION, 103) F_H_CREACION, " +
                "(R.NOMBRE + ISNULL(' (' + P.NombreSocial + ') ','') + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE," +
            " R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  R.F_H_MOD, " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "UN.DESCRIPCION UNIDAD, (select convert(decimal(20,0),Max(rango)) from M_ART_RECETA where IDRECETA = R.IDRECETA) DIAS, " +
            " CASE WHEN R.IDESTADO = 13 THEN CONVERT(VARCHAR(10), R.FULT_DESP,103) ELSE  CONVERT(VARCHAR(10), GETDATE(),103) END FULT_DESP, " +
            //"dbo.fn_get_Max_fecha_entrega(R.IDRECETA) FPDESPACHO " +
            "case when r.idestado = 2  " +
                "then  " +
                "( " +
                "CONVERT(VARCHAR(10),DATEADD(day, (select 	MAX(ISNULL(RANGO_DISP,0)) " +
                        "from M_ART_RECETA " +
                        "where IDRECETA = R.IDRECETA), getdate()),103) " +
                ") " +
                "else " +
                "(SELECT  CONVERT(VARCHAR(10),MAX(FDESPACHO),103) FROM M_ART_RECETA WHERE IDRECETA = R.IDRECETA AND elib = 0) " +
                "end FPDESPACHO " +
            "FROM M_RECETA R " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "LEFT OUTER JOIN M_PACIENTE P ON P.RUT = R.RUT ";
        //"LEFT OUTER JOIN M_PACIENTE P ON CONVERT(VARCHAR(10),P.RUT) = CONVERT(VARCHAR(10),R.RUT) ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarFarmaciaPreparar()
    {
        DataSet aoCod;
        string ls_where = "";


        //if (ls_All == "NotAll")

        //    ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0 ) ";
        //else
        //    ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0 ) ";

        ls_where = "WHERE ( r.idestado in (7,8) and R.FOLIO > 0 and R.IDBODPRERIF = " + ls_Bod + ") ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = "";

        if (ls_Prioridad == "0")
            is_order_by = " ORDER BY  R.F_H_MOD DESC ";
        else
            is_order_by = " ORDER BY FPRIORIDAD DESC";

        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE. 

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_MOD >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_MOD <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }


        // Si se ingresó Rapido
        if (ls_Rapido == "1")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " dbo.fn_Cant_Art(R.IDRECETA) <= 2 ";
            // Agrega criterio a var. aux.
            is_crit += " Cantidad articulos: ";
        }

        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }

        if (ls_pat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_PAT like '%" + ls_pat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_pat_pac + " , ";
        }

        if (ls_mat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_MAT like '%" + ls_mat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_mat_pac + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        //is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, (R.NOMBRE + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
        //    "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,R.F_H_VALIDA  F_H_MOD, ISNULL(R.FPRIORIDAD,0)  FPRIORIDAD, " +
        //    "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
        //    "UN.DESCRIPCION UNIDAD, isnull(usr.NOMBRE,'')  user_prepara, ISNULL(R.OBS_FARMACIA,'') OBSERVACION, dbo.fn_Cant_Art(R.IDRECETA) CANT " +
        //    "FROM M_RECETA R " +
        //    "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
        //    "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
        //    "left outer join M_USUARIOS usr on usr.IDUSUARIO = r.user_prepara " ;

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, (R.NOMBRE + ISNULL(' (' + P.NombreSocial + ') ','') + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE, " +
                "R.RUT, EST.DESCRIPCION ESTADO, " +
        "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,R.F_H_VALIDA  F_H_MOD, ISNULL(R.FPRIORIDAD,0)  FPRIORIDAD, " +
        "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
        "UN.DESCRIPCION UNIDAD, isnull(usr.NOMBRE,'')  user_prepara, ISNULL(R.OBS_FARMACIA,'') OBSERVACION, " +
        "( " +
        "    select count(1) " +
        "	        FROM M_ART_RECETA " +
        "	        WHERE IDRECETA = R.IDRECETA " +
            "   ) CANT " +
        "FROM M_RECETA R " +
        "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
        "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
        "left outer JOIN M_PACIENTE P ON P.RUT = R.RUT " +
        "left outer join M_USUARIOS usr on usr.IDUSUARIO = r.user_prepara ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarFarmaciaDispensar()
    {
        DataSet aoCod;
        string ls_where = "";


        //if (ls_All == "NotAll")

        //    ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0 ) ";
        //else
        //    ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0 ) ";

        ls_where = "WHERE ( r.idestado in (6, 10) and R.FOLIO > 0 and R.IDBODPRERIF = " + ls_Bod + ") ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = "";

        if (ls_Prioridad == "0")
            is_order_by = " ORDER BY  R.F_H_MOD DESC";
        else
            is_order_by = " ORDER BY FPRIORIDAD DESC";

        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_MOD >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_MOD <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }



        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }

        if (ls_pat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_PAT like '%" + ls_pat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_pat_pac + " , ";
        }

        if (ls_mat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_MAT like '%" + ls_mat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_mat_pac + " , ";
        }

        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, (R.NOMBRE + ISNULL(' (' + P.NombreSocial + ') ','') + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE, " +
                "R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  R.F_H_MOD, ISNULL(R.FPRIORIDAD,0)  FPRIORIDAD, R.F_H_PREPARA, " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "UN.DESCRIPCION UNIDAD, isnull(usr.NOMBRE,'')  user_llama, ISNULL(R.OBS_FARMACIA,'') OBSERVACION " +
            "FROM M_RECETA R " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "left outer JOIN M_PACIENTE P ON P.RUT = R.RUT " +
            "left outer join M_USUARIOS usr on usr.IDUSUARIO = r.user_llama ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarFarmaciaPendiente()
    {
        DataSet aoCod;
        string ls_where = "";


        //if (ls_All == "NotAll")

        //    ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0 ) ";
        //else
        //    ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0 ) ";

        ls_where = "WHERE ( r.idestado in (9, 12) and R.FOLIO > 0 and R.IDBODPRERIF = " + ls_Bod + ") ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_MOD DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_MOD >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_MOD <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }



        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }

        if (ls_pat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_PAT like '%" + ls_pat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_pat_pac + " , ";
        }

        if (ls_mat_pac != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.APELL_MAT like '%" + ls_mat_pac + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_mat_pac + " , ";
        }

        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, (R.NOMBRE + ISNULL(' (' + P.NombreSocial + ') ','') + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE, " +
                "R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  R.F_H_MOD, R.F_H_DISPENSA, " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "UN.DESCRIPCION UNIDAD, isnull(usr.NOMBRE,'')  user_llama, ISNULL(R.OBS_FARMACIA,'') OBSERVACION " +
            "FROM M_RECETA R " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "left outer JOIN M_PACIENTE P ON P.RUT = R.RUT " +
            "left outer join M_USUARIOS usr on usr.IDUSUARIO = r.user_llama ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }
    #endregion

    #region Obervaciones
    public DataSet mfCargaObs(string lsIdentificador)
    {
        DataSet aoBind;
        string lsSql = "";

        lsSql = "select A.[IDOBSREC], A.[IDRECETA], A.OBSERVACION, A.F_H_CREACION, A.IDUSER, A.IDESTADO_INI, A.IDESTADO_FIN, " +
                "B.DESCRIPCION ESTADO_INI, C.DESCRIPCION ESTADO_FIN, D.NOMBRE " +
                "from [dbo].[M_OBSERVACIONES_RECETA] A " +
                "INNER JOIN [dbo].[TG_ESTADOS] B ON A.IDESTADO_INI = B.IDESTADO " +
                "INNER JOIN [dbo].[TG_ESTADOS] C ON A.IDESTADO_FIN = C.IDESTADO " +
                "INNER JOIN [dbo].[M_USUARIOS] D ON D.IDUSUARIO = A.IDUSER " +
                "where A.IDRECETA = " + lsIdentificador + " ORDER BY A.[IDOBSREC] ASC";

        con = bd.fnGetConn();
        aoBind = bd.Fill(con, lsSql);
        con.Close();
        return aoBind;
    }

    public string mfInsObsRecRut(string asRut, string asObs, string asUser, string asIni, string asFin, string asIdBodega)
    {
        string lsRet = "";
        string lsSql = "insert into " + modConstantes.gsDbAB + "M_OBSERVACIONES_RECETA(" +
                        " IDRECETA, OBSERVACION, F_H_CREACION, IDUSER, " +
                        " IDESTADO_INI, IDESTADO_FIN" +
                        " )" +
                        " SELECT IDRECETA,     " +
                         " '" + asObs + "',          " +
                        " getdate() ,               " +
                        " " + asUser + ",           " +
                        " '" + asIni + "',          " +
                        " '" + asFin + "'           " +
                        " FROM  M_RECETA " +
                        "WHERE rut =  '" + asRut + "' and idestado = " + asIni + " and IDBODPRERIF = " + asIdBodega;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfInsObsRec(string asIdentificador, string asObs, string asUser, string asIni, string asFin)
    {
        string lsRet = "";
        string lsSql = "insert into " + modConstantes.gsDbAB + "M_OBSERVACIONES_RECETA(" +
                        " IDRECETA, OBSERVACION, F_H_CREACION, IDUSER, " +
                        " IDESTADO_INI, IDESTADO_FIN" +
                        " )" +
                        " values(                   " +
                        " " + asIdentificador + ",  " +
                        " '" + asObs + "',          " +
                        " getdate() ,               " +
                        " " + asUser + ",           " +
                        " '" + asIni + "',          " +
                        " '" + asFin + "'           " +
                        " )";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfUpdateUsrValRec(string asIdentificador, string asUser)
    {
        string lsRet = "";
        string lsSql = "UPDATE " + modConstantes.gsDbAB + "M_RECETA set " +
                        "F_H_PREPARA = NULL," +
                        "user_prepara = NULL," +
                        "USER_LLAMA = NULL," +
                        "F_H_LLAMA = NULL," +
                        "user_valida =  " + asUser + ", " +
                        "F_H_VALIDA =  GETDATE() " +
                        " WHERE IDRECETA = " + asIdentificador;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfUpdateUsrPrepRecRut(string asRut, string asUser, string lsEstado, string lsObs, string lsEstadoEst, string asIdBod)
    {
        string lsSql = "";

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                "IDESTADO = " + lsEstado + ", " +
                "user_prepara =  " + asUser + ", " +
                " OBS_FARMACIA = '" + lsObs + "', " +
                " F_H_PREPARA = getdate() " +
                "WHERE RUT =  '" + asRut + "' AND IDESTADO = " + lsEstadoEst + " AND IDBODPRERIF = " + asIdBod;


        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfUpdateUsrPrepRec(string asIdentificador, string asUser, string lsEstado, string lsObs)
    {
        string lsSql = "";

        if (lsEstado == "2" || lsEstado == "11" || lsEstado == "8")
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "F_H_MOD = GETDATE(), " +
                    "user_prepara =  NULL, " +
                    " OBS_FARMACIA = '" + lsObs + "', " +
                    " F_H_PREPARA = NULL " +
                    "WHERE IDRECETA =  " + asIdentificador;
        }
        else
        {
            //string corr = mod.mfgetCorrelativos("D");

            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "user_prepara =  " + asUser + ", " +
                        //" CORR_DISP = " + corr + ", " +
                        "F_H_PREPARA =  GETDATE() " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }


        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }
    public string mfUpdateUsrDispRecRut(string asRut, string asUser, string lsEstado, string lsObs, string lsEstadoEst, string asIdBod)
    {
        string lsSql = "";

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "F_H_MOD = GETDATE(), " +
                    "OBS_FARMACIA = '" + lsObs + "', " +
                    "USER_DISPENSA =  " + asUser + ", " +
                    "F_H_DISPENSA =  GETDATE() " +
                    "WHERE RUT =  '" + asRut + "' AND IDESTADO = " + lsEstadoEst + " AND IDBODPRERIF = " + asIdBod;




        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }
    public string mfUpdateUsrDispRec(string asIdentificador, string asUser, string lsEstado, string lsObs)
    {
        string lsSql = "";

        if (lsEstado == "2" || lsEstado == "11")
        {

            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "USER_DESPACHA =  NULL, " +
                        "F_H_DESPACHA =  NULL, " +
                        "USER_LLAMA =  NULL, " +
                        "F_H_LLAMA =  NULL, " +
                        "user_prepara =  NULL, " +
                        "F_H_PREPARA =  NULL, " +
                        "USER_DISPENSA =  NULL, " +
                        "F_H_DISPENSA =  NULL " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }
        else if (lsEstado == "6")
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "USER_LLAMA =  NULL, " +
                        "F_H_LLAMA =  NULL " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }
        else
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "USER_DISPENSA =  " + asUser + ", " +
                        "F_H_DISPENSA =  GETDATE() " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }

        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfUpdateUsrPendRec(string asIdentificador, string asUser, string lsEstado, string lsObs)
    {
        string lsSql = "";

        if (lsEstado == "2" || lsEstado == "11")
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "USER_DESPACHA =  NULL, " +
                        "F_H_DESPACHA =  NULL, " +
                        "USER_LLAMA =  NULL, " +
                        "F_H_LLAMA =  NULL, " +
                        "user_prepara =  NULL, " +
                        "F_H_PREPARA =  NULL, " +
                        "USER_DISPENSA =  NULL, " +
                        "F_H_DISPENSA =  NULL, " +
                        "USER_PENDIENTE =  NULL, " +
                        "F_H_PENDIENTE =  NULL " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }
        else if (lsEstado == "9")
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "USER_LLAMA =  NULL, " +
                        "F_H_LLAMA =  NULL " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }
        else
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "F_H_MOD = GETDATE(), " +
                        "OBS_FARMACIA = '" + lsObs + "', " +
                        "USER_DISPENSA =  " + asUser + ", " +
                        "F_H_DISPENSA =  GETDATE() " +
                        "WHERE IDRECETA =  " + asIdentificador;
        }



        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }




    public string mfUpdateUsrPendRecRut(string asRut, string asUser, string lsEstado, string lsObs, string lsEstadoEst, string asIdBod)
    {
        string lsSql = "";


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "F_H_MOD = GETDATE(), " +
                    "OBS_FARMACIA = '" + lsObs + "', " +
                    "USER_DISPENSA =  " + asUser + ", " +
                    "F_H_DISPENSA =  GETDATE() " +
                    "WHERE RUT =  '" + asRut + "' AND IDESTADO = " + lsEstadoEst + " AND IDBODPRERIF = " + asIdBod;




        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    #endregion

    #region Listado

    public string mfEliminarReceta()
    {


        string lsRet = "";

        string _XML = string.Empty;
        string _llNOS = string.Empty;
        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_terminado_pase", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();

        if (_XML == "")
        {

            lsRet = "0";
        }
        else
        {
            lsRet = "-1";
        }

        return lsRet;
    }

    public DataSet mfBuscarInfectologiaFarmacia()
    {
        DataSet aoCod;
        string ls_where = "";


        if (ls_All == "NotAll")

            ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0 ) ";
        else
            ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0 ) ";




        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_CREACION DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }



        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDSERVICIO = " + ls_IdServ + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdServ + " , ";
        }

        // Si se ingresó Sub servicio la agrega al Where.
        if (ls_IdSubServ != "0" && ls_IdSubServ != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDSUBSERVICIO = " + ls_IdSubServ + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdSubServ + " , ";
        }

        // Si se ingresó Cama la agrega al Where.
        if (ls_IdCama != "0" && ls_IdCama != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDCAMA = " + ls_IdCama + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdCama + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "UN.DESCRIPCION UNIDAD, SUN.DESCRIPCION SUBUNIDAD, CA.DESCRIPCION CAMA " +
            "FROM M_RECETA R " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.IDSERVICIO " +
            "INNER JOIN M_SUB_UNIDAD SUN ON SUN.IDSUBUNIDAD = R.IDSUBSERVICIO " +
            "INNER JOIN M_CAMAS CA ON CA.IDCAMA = R.IDCAMA ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarDispFarmacia()
    {
        DataSet aoCod;
        string ls_where = "";


        //if (ls_All == "NotAll")

        //    ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0  ) ";
        //else
        //    ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0  ) ";

        ls_where = "WHERE ( R.FOLIO > 0  ) ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_CREACION DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }

        // Si se ingresó ALL
        if (ls_All == "NotAll")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( r.IDESTADO not in (2,4, 3) ) ";
            // Agrega criterio a var. aux.
            is_crit += " estado: " + ls_All + " , ";
        }

        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Policlinico la agrega al Where.
        if (ls_unidad != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.CODUNIOP = '" + ls_unidad + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " CODUNIOP igual a: " + ls_unidad + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Manual != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( ISNULL(R.RUT_MED,0) <> 0 ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }


        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  R.F_H_MOD, " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "UN.DESCRIPCION UNIDAD " +
            "FROM M_RECETA R " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarFarmacia()
    {
        DataSet aoCod;
        string ls_where = "";


        if (ls_All == "NotAll")

            ls_where = "WHERE ( r.idestado = 2 and R.FOLIO > 0 ) ";
        else
            ls_where = "WHERE ( r.IDUSUARIO > 0  and R.FOLIO > 0 ) ";




        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_CREACION DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.

        if (ls_tipo == "1")
        {
            if (ls_f_d != "")
            {

                ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                                "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }
        else
        {
            if (ls_f_d != "" && ls_All == "NotAll")
            {

                ls_where += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

                // Agrega criterio a var. aux.
                is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
            }
        }



        // Si se ingresó Tipo la agrega al Where.
        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Policlinico la agrega al Where.
        if (ls_unidad != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.CODUNIOP = '" + ls_unidad + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " CODUNIOP igual a: " + ls_unidad + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, (R.NOMBRE + ' ' + APELL_PAT + ' ' + APELL_MAT) NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  R.F_H_MOD, (select convert(decimal(20,0),Max(rango)) from M_ART_RECETA where IDRECETA = R.IDRECETA) DIAS, " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "UN.DESCRIPCION UNIDAD " +
            "FROM M_RECETA R " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscar()
    {
        DataSet aoCod;
        string ls_where = "";


        //if (ls_All == "NotAll")

        //    ls_where = "WHERE ( r.IDUSUARIO = " + ls_idusr + " and r.idestado = 2) ";
        //else
        //    ls_where = "WHERE ( r.IDUSUARIO = " + ls_idusr + " and r.idestado <> 1) ";

        if (ls_All == "NotAll")

            ls_where = "WHERE ( r.idestado = 2) ";
        else
            ls_where = "WHERE ( r.idestado <> 1) ";




        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY R.FOLIO DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }


        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            if (ls_tipo == "2")
                ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            else
                ls_where += " ( r.IDUSUARIO = " + ls_idusr + " and isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";

            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "USR.NOMBRE MEDICO " +
            "FROM M_RECETA R " +
            "INNER JOIN M_USUARIOS USR ON USR.IDUSUARIO = R.IDUSUARIO " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;


        //SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO ,
        //dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_CREACION) 
        //FROM M_RECETA R 
        //INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO   
        //WHERE ( r.idestado = 2 and R.FOLIO > 0 )  
        //and  ( cast( '01/01/2021 00:00:00' as datetime) >=   R.F_H_INGRESO
        //and    cast( '01/01/2021 00:00:00' as datetime) <= dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_CREACION)  )  
        //and  ( isnull(R.TIPO,1) = '2' )    
        //ORDER BY  R.F_H_CREACION DESC


    }

    public DataSet mfBuscarManual()
    {
        DataSet aoCod;
        string ls_where = " WHERE ISNULL(RUT_MED,-1) <> -1 and FOLIO > 0 ";

        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY R.FOLIO DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }


        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }



        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "NOMB_MED MEDICO " +
            "FROM M_RECETA R " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;


    }

    public DataSet mfBuscarRecetaInfectologia()
    {
        DataSet aoCod;
        string ls_where = "";


        ls_where = "WHERE ( r.idestado <> 1 ) ";
        //ls_where = "WHERE ( r.idestado not in (1, 3) ) ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_CREACION DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }

        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }


        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,  " +
            "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA  " +
            "where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN, " +
            "USR.NOMBRE MEDICO " +
            "FROM M_RECETA R " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "INNER JOIN M_USUARIOS USR ON USR.IDUSUARIO = R.IDUSUARIO ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }

    public DataSet mfBuscarReceta()
    {
        DataSet aoCod;
        string ls_where = "";


        ls_where = "WHERE ( r.idestado not in (1, 3, 11)  AND ISNULL(R.RUT_MED,0) = 0 ) ";


        string is_crit = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  R.F_H_CREACION DESC";
        string is_select = "";

        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            ls_where += " and  ( R.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   R.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }

        if (ls_tipo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        }


        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.rut = '" + ls_rut + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Region Tablas

        // Si se ingresó Contacto Solicitud la agrega al Where.
        if (ls_Codigo != "" || ls_Art != "")
        {

            lsTablas += "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA AND ISNULL(ART.IDESTADO,1) <> 3 " +
                        "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO ";

        }

        // Recupera Códigos de barra asociados.

        is_select = "SELECT R.IDRECETA, R.FOLIO, CONVERT(VARCHAR(10),R.F_H_CREACION,103) F_H_CREACION, R.NOMBRE, R.RUT, EST.DESCRIPCION ESTADO, " +
            "UN.DESCRIPCION UNIDAD, CASE WHEN ISNULL(R.RUT_MED,-1) = -1 THEN US.NOMBRE ELSE R.NOMB_MED END MEDICO, R.F_H_MOD  " +
            "FROM M_RECETA R " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP  = R.CODUNIOP " +
            "INNER JOIN M_USUARIOS US ON US.IDUSUARIO = R.IDUSUARIO ";


        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                    lsTablas + ' ' +
                    ls_where + ' ' +
                    is_group + ' ' +
                    is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

        con.Close();
        return aoCod;
    }
    public DataSet ConsultarID(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, R.NOMBRE, R.RUT, ISNULL(R.DV,'') DV, R.IDESTADO, EST.DESCRIPCION ESTADO, " +
            "ISNULL(R.APELL_PAT,'') APELL_PAT, ISNULL(R.APELL_MAT,'') APELL_MAT, ISNULL(RUT_MED,-1) RUT_MED, " +
            " isnull(TipoAdq,1) TipoAdq, isnull(NombAdq,'') NombAdq, isnull(RutAdq,'') RutAdq, isnull(FonoAdq,'') FonoAdq, " +
            " ISNULL(R.NombreSocial,'')  NombreSocial, OBS_FARMACIA, ISNULL(R.IDBODPRERIF,0) IDBODPRERIF," +
            "R.IDREGION, R.IDCOMUNA, R.DIRECCION, R.IDREGION_PAC, R.IDCOMUNA_PAC, " +
            "isnull(R.IDDIAGNOSTICO,0) IDDIAGNOSTICO, isnull(R.IDBODPRERIF,0) IDBODPRERIF , " +
            "R.DIRECCION_PAC, R.OBSERVACION, R.DIAGNOSTICO, isnull(R.CODUNIOP,1) CODUNIOP, " +
            " ISNULL(IDSERVICIO,0) IDSERVICIO, ISNULL(IDSUBSERVICIO,0) IDSUBSERVICIO, ISNULL(IDCAMA,0) IDCAMA, CONVERT(VARCHAR(10),F_H_INGRESO,103) F_H_INGRESO " +
            "FROM M_RECETA R " +
            "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
            "WHERE R.IDRECETA = " + lsIdentificador;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet ListadoMediRec(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;
        string lsOrder = "order by r.F_H_CREACION, R.FOLIO DESC "; ;

        string lsWhere = " ";

        if (ls_tipo != "")
        {
            if (lsWhere != "")
                lsWhere += " and ";

            lsWhere += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";

        }

        lsSql = "select M.IDARTRECETA ,R.FOLIO, r.F_H_CREACION, V.DESCRIPCION_LARGA, " +
                " (case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0)))   " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0)))   " +
                " end + ' ' + v.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA,  " +
                " case when ((m.cantidad - FLOOR(m.cantidad)) > 0) then convert(varchar(10),convert(decimal(20,2),ISNULL(M.CANTIDAD,0))) " +
                " else convert(varchar(10),convert(decimal(20,0),ISNULL(M.CANTIDAD,0))) end CANTIDAD, " +
                "  EST.DESCRIPCION ESTADO, UN.DESCRIPCION UNIDAD " +
                " from M_ART_RECETA M  " +
                " INNER JOIN M_RECETA R ON R.IDRECETA = M.IDRECETA  " +
                " INNER JOIN v_articulos V ON V.IDARTICULO = M.IDARTICULO  " +
                " LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = M.IDPERIODO " +
                " INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO " +
                " INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.CODUNIOP " +
                " WHERE RUT =  '" + lsIdentificador + "' " +
                " AND ISNULL(M.IDESTADO,1) <> 3 " +
                " and R.IDESTADO NOT IN (1,3,5) " + lsWhere + " " +
                " AND (R.F_H_CREACION >= DATEADD(DAY, -30, getdate()) and R.F_H_CREACION <= DATEADD(DAY, 30, getdate())) " +
                "AND EXISTS (SELECT OB.IDRECETA " +
                "FROM M_OBSERVACIONES_RECETA OB " +
                "WHERE OB.IDRECETA = R.IDRECETA " +
                ")" + lsOrder;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    #endregion

    #region Folio

    public string mfValidarRecetaManual(string asID, string asObs, string fecha)
    {


        string lsRet = "";

        string _XML = string.Empty;
        string _llNOS = string.Empty;
        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_validar_receta_manual", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@Idreceta", SqlDbType.Int, 4);
        Query.Parameters["@Idreceta"].Value = asID;

        Query.Parameters.Add("@obs", SqlDbType.VarChar, 800);
        Query.Parameters["@obs"].Value = asObs;

        Query.Parameters.Add("@fecha", SqlDbType.VarChar, 800);
        Query.Parameters["@fecha"].Value = fecha;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;

        Query.Parameters.Add("@asmsg", SqlDbType.VarChar, 8000);
        Query.Parameters["@asmsg"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();
        _llNOS = Query.Parameters["@asmsg"].Value.ToString();

        if (_XML == "")
        {

            lsRet = _llNOS;
        }
        else
        {
            lsRet = "-1";
        }

        return lsRet;
    }
    public string mfValidarReceta(string asID, string asCopias, string asObs, string fecha)
    {


        string lsRet = "";

        string _XML = string.Empty;
        string _llNOS = string.Empty;
        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_validar_receta", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@Idreceta", SqlDbType.Int, 4);
        Query.Parameters["@Idreceta"].Value = asID;

        Query.Parameters.Add("@copias", SqlDbType.Int, 4);
        Query.Parameters["@copias"].Value = asCopias;

        Query.Parameters.Add("@obs", SqlDbType.VarChar, 800);
        Query.Parameters["@obs"].Value = asObs;

        Query.Parameters.Add("@fecha", SqlDbType.VarChar, 800);
        Query.Parameters["@fecha"].Value = fecha;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;

        Query.Parameters.Add("@asmsg", SqlDbType.VarChar, 8000);
        Query.Parameters["@asmsg"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();
        _llNOS = Query.Parameters["@asmsg"].Value.ToString();

        if (_XML == "")
        {

            lsRet = _llNOS;
        }
        else
        {
            lsRet = "-1";
        }

        return lsRet;
    }

    public string mfValidarPaseInfectologia(string asID, string asCopias, string asObs)
    {


        string lsRet = "";

        string _XML = string.Empty;
        string _llNOS = string.Empty;
        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_validar_pase", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@Idreceta", SqlDbType.Int, 4);
        Query.Parameters["@Idreceta"].Value = asID;

        Query.Parameters.Add("@copias", SqlDbType.Int, 4);
        Query.Parameters["@copias"].Value = asCopias;

        Query.Parameters.Add("@obs", SqlDbType.VarChar, 800);
        Query.Parameters["@obs"].Value = asObs;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;

        Query.Parameters.Add("@asmsg", SqlDbType.VarChar, 8000);
        Query.Parameters["@asmsg"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();
        _llNOS = Query.Parameters["@asmsg"].Value.ToString();

        if (_XML == "")
        {

            lsRet = _llNOS;
        }
        else
        {
            lsRet = "-1";
        }

        return lsRet;
    }

    #endregion

    #region Farmacia

    public DataSet mfTraeReceta(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.


        lsSql = "select IDRECETA " +
                "from " + modConstantes.gsDbAB + "M_RECETA  " +
                "WHERE IDRECETA = " + lsIdentificador;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet ListaRecetasDispensacion(string lsIdentificador, string asEstado, string asIdBod)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.


        lsSql = "select IDRECETA " +
                "from " + modConstantes.gsDbAB + "M_RECETA  " +
                "WHERE RUT = '" + lsIdentificador + "' " +
                "AND IDBODPRERIF = " + asIdBod + " " +
                "AND IDESTADO =  " + asEstado;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerIdUsrPreparar(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select isnull(user_prepara,0) " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where idreceta = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerIdUsrLlama(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select isnull(USER_LLAMA,0) " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where idreceta = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerCantPreparar(string asIndentificador, string asEstado, string asIdBod)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select count(1) " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where RUT = '" + asIndentificador + "' AND IDESTADO =  " + asEstado + " and IDBODPRERIF = " + asIdBod;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerUnidad(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select isnull(CODUNIOP,1)  CODUNIOP " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerDiagnosticoPROA(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select isnull(IDDIAGNOSTICO,0)  IDDIAGNOSTICO " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerDiagnostico(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select isnull(DIAGNOSTICO,'')  DIAGNOSTICO " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerIndicaciones(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select isnull(observacion,'')  observacion " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerEstado(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select idestado " +
                "from " + modConstantes.gsDbAB + "[M_RECETA] " +
                "where IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfObtenerDiasTratamiento(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        //lsSql = "select top 1 CONVERT(DECIMAL(20,0),(RANGO)) " +
        //        "from " + modConstantes.gsDbAB + "[M_ART_RECETA] " +
        //        "where ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + asIndentificador;4

        lsSql = "select CONVERT(DECIMAL(20,0),MAX(RANGO)) " +
                "from " + modConstantes.gsDbAB + "[M_ART_RECETA] " +
                "where ISNULL(IDESTADO,1) <> 3 And IDRECETA = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfUpdateEstadoFus(string lsIDFUS, string lsEstado, string lsObs)
    {
        string lsRet = "";
        string lsSql = "";
        string lsIns = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "F_H_MOD = GETDATE(), " +
                    "OBS_FARMACIA = '" + lsObs + "' " + lsIns + " " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdatefechaDesp(string lsIDFUS, string lsFecha)
    {
        string lsRet = "";
        string lsSql = "";
        //string corr = mod.mfgetCorrelativos("P");
        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_ART_RECETA set " +
                    "FDESPACHO = '" + lsFecha + "' " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdatefechaDespRec(string lsIDFUS, string lsFecha)
    {
        string lsRet = "";
        string lsSql = "";
        //string corr = mod.mfgetCorrelativos("P");
        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_ART_RECETA set " +
                    "F_DESPACHO_REC = '" + lsFecha + "' " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdateValida(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";
        //string corr = mod.mfgetCorrelativos("P");
        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "user_valida =  " + lsUser + ", " +
                    " OBS_FARMACIA = '" + lsObs + "', " +
                    " F_H_MOD = GETDATE(), " +
                    //" CORR_PREP = " + corr + ", " +
                    " F_H_VALIDA = GETDATE() " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdatePreparaRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdbod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                "IDESTADO = " + lsEstado + ", " +
                "user_prepara =  " + lsUser + ", " +
                " F_H_MOD = GETDATE(), " +
                " F_H_PREPARA = GETDATE() " +
                "WHERE rut =  '" + lsRut + "' and idestado = 8 and IDBODPRERIF = " + asIdbod;



        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdatePrepara(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        if (lsEstado == "2" || lsEstado == "11")
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "user_prepara =  NULL, " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_PREPARA = NULL " +
                    "WHERE IDRECETA =  " + lsIDFUS;
        }
        else
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "user_prepara =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_PREPARA = GETDATE() " +
                    "WHERE IDRECETA =  " + lsIDFUS + " ";
        }


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdateDespacharRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdBod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_DESPACHA =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_DESPACHA = GETDATE() " +
                    "WHERE RUT =  '" + lsRut + "' AND IDESTADO = 7 AND IDBODPRERIF = " + asIdBod;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdateDespachar(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_DESPACHA =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_DESPACHA = GETDATE() " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdateLlamarRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdBod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_LLAMA =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_LLAMA = GETDATE() " +
                    "WHERE RUT =  '" + lsRut + "' AND IDESTADO = 6 AND IDBODPRERIF = " + asIdBod;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdateLlamarDisRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdBod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_LLAMA =  NULL, " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_LLAMA = NULL, " +
                    " USER_PENDIENTE =  " + lsUser + ", " +
                    " F_H_PENDIENTE = GETDATE() " +
                    "WHERE RUT =  '" + lsRut + "' AND IDESTADO = 10 and IDBODPRERIF = " + asIdBod;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdateLlamarPendRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdBod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_LLAMA =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_LLAMA = GETDATE() " +
                    "WHERE RUT =  '" + lsRut + "' AND IDESTADO = 9 and IDBODPRERIF = " + asIdBod;



        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdateLlamar(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        if (lsEstado == "9")
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        " F_H_MOD = GETDATE(), " +
                        "USER_LLAMA =  NULL, " +
                        " F_H_LLAMA = NULL, " +
                        "USER_PENDIENTE =  " + lsUser + ", " +
                        " F_H_PENDIENTE = GETDATE() " +
                        "WHERE IDRECETA =  " + lsIDFUS;
        }
        else
        {
            lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                        "IDESTADO = " + lsEstado + ", " +
                        "USER_LLAMA =  " + lsUser + ", " +
                        " F_H_MOD = GETDATE(), " +
                        " F_H_LLAMA = GETDATE() " +
                        "WHERE IDRECETA =  " + lsIDFUS;
        }





        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdateEntregar(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_ENTREGA =  " + lsUser + ", " +
                    " OBS_FARMACIA = '" + lsObs + "', " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_ENTREGA = GETDATE() " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdatePendienteRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdBod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_PENDIENTE =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_PENDIENTE = GETDATE() " +
                    "WHERE RUT =  '" + lsRut + "' AND IDESTADO = 10 AND IDBODPRERIF = " + asIdBod;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdatePendiente(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_PENDIENTE =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_PENDIENTE = GETDATE() " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdatePendienteDesp(string lsIDFUS, string lsEstado, string lsObs, string lsUser)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_DISPENSA =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_DISPENSA = GETDATE() " +
                    "WHERE IDRECETA =  " + lsIDFUS;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    public string mfUpdatePendienteDespRut(string lsRut, string lsEstado, string lsObs, string lsUser, string asIdBod)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "IDESTADO = " + lsEstado + ", " +
                    "USER_DISPENSA =  " + lsUser + ", " +
                    " F_H_MOD = GETDATE(), " +
                    " F_H_DISPENSA = GETDATE() " +
                    "WHERE RUT =  '" + lsRut + "' AND IDESTADO = 12 AND IDBODPRERIF = " + asIdBod;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }


    public string mfUpdatePacienteRecetas(string lsId, string lsRut, string lsDV, string lsNomb, string lsSocial, string lsPat, string lsMat, string lsDir, string lsReg, string lsCom)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();


        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "RUT = '" + lsRut + "', " +
                    "DV = '" + lsDV + "', " +
                    "NOMBRE = '" + lsNomb + "', " +
                    "NombreSocial = '" + lsSocial + "', " +
                    "APELL_PAT = '" + lsPat + "'," +
                    "APELL_MAT = '" + lsMat + "'," +
                    "IDREGION_PAC = " + lsReg + "," +
                    "IDCOMUNA_PAC = " + lsCom + ", " +
                    "DIRECCION_PAC = '" + lsDir + "' " +
                    "WHERE IDRECETA = " + lsId + " ";


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    public string mfUpdateCabReceta(string asID, string asId, string asFono1, string asFono2,
                                    string asObs1, string asObs2, string asMail, string asMail2,
                                    string asTipo, string asNomb, string asRut,
                                    string asFono, string asDir, string asPrioridad,
                                    string asRegion, string asComuna, string asIdBod,
                                    string asFechaDesp)
    {

        string _XML = string.Empty;
        //string _llNOS = string.Empty;
        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_mod_cab_receta", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@lsIdReceta", SqlDbType.Int, 4);
        Query.Parameters["@lsIdReceta"].Value = asID;

        Query.Parameters.Add("@lsIdentificador", SqlDbType.Int, 4);
        Query.Parameters["@lsIdentificador"].Value = asId;

        Query.Parameters.Add("@FONO1", SqlDbType.VarChar, 800);
        Query.Parameters["@FONO1"].Value = asFono1;

        Query.Parameters.Add("@FONO2", SqlDbType.VarChar, 800);
        Query.Parameters["@FONO2"].Value = asFono2;

        Query.Parameters.Add("@OBS1", SqlDbType.VarChar, 800);
        Query.Parameters["@OBS1"].Value = asObs1;

        Query.Parameters.Add("@OBS2", SqlDbType.VarChar, 800);
        Query.Parameters["@OBS2"].Value = asObs2;

        Query.Parameters.Add("@MAIL", SqlDbType.VarChar, 800);
        Query.Parameters["@MAIL"].Value = asMail;

        Query.Parameters.Add("@MAIL2", SqlDbType.VarChar, 800);
        Query.Parameters["@MAIL2"].Value = asMail2;

        Query.Parameters.Add("@Tipo", SqlDbType.Int, 4);
        Query.Parameters["@Tipo"].Value = asTipo;

        Query.Parameters.Add("@Nomb", SqlDbType.VarChar, 800);
        Query.Parameters["@Nomb"].Value = asNomb;


        Query.Parameters.Add("@Rut", SqlDbType.VarChar, 800);
        Query.Parameters["@Rut"].Value = asRut;

        Query.Parameters.Add("@Fono", SqlDbType.VarChar, 800);
        Query.Parameters["@Fono"].Value = asFono;

        Query.Parameters.Add("@Dir", SqlDbType.VarChar, 800);
        Query.Parameters["@Dir"].Value = asDir;

        Query.Parameters.Add("@Prioridad", SqlDbType.Int, 4);
        Query.Parameters["@Prioridad"].Value = asPrioridad;

        Query.Parameters.Add("@Region", SqlDbType.Int, 4);
        Query.Parameters["@Region"].Value = asRegion;

        Query.Parameters.Add("@Comuna", SqlDbType.Int, 4);
        Query.Parameters["@Comuna"].Value = asComuna;

        Query.Parameters.Add("@IdBod", SqlDbType.Int, 4);
        Query.Parameters["@IdBod"].Value = asIdBod;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;



        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();

        return _XML;

    }

    public string mfUpdateFechaInfec(string lsID, string lsfecha)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = " update " + modConstantes.gsDbAB + "M_RECETA set " +
                    "F_H_INGRESO = '" + lsfecha + "', idestado = 2 " +
                    "WHERE IDRECETA =  " + lsID;


        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }

    #endregion

    #region posologia

    public bool mfRangoDias(string Id)
    {
        bool salida = true;

        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            //lsSql = "SELECT COUNT(1) " +
            //        "FROM M_RECETA A " +
            //        "WHERE CODUNIOP IN ( " +
            //        "SELECT CODUNIOP " +
            //        "FROM M_UNIDAD_OPERATIVA " +
            //        "WHERE CODUNIOP IN (" + modConstantes.mfConstante("RANG_DIAS") + ") " +
            //        ") " +
            //        "AND IDRECETA = " + Id;

            lsSql = "SELECT COUNT(1) " +
                "FROM M_UNIDAD_OPERATIVA " +
                "WHERE ISNULL(EXTIENDE,0) <> 0 " +
                "AND CODUNIOP = " + Id;


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "0") salida = false;
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return salida;
    }

    public string mfCalcularPosologia(string hdIdMat, string TCalc0, string ddlPeriodo, string TPresent, string TRango, string TDias, string TxtPosologia)
    {
        ClassArticulos art = new ClassArticulos();

        double dosis = 0;


        if (TCalc0 == "2" && (ddlPeriodo != "12" && ddlPeriodo != "14"))
        {

            dosis = 0;

        }
        else
        {
            if (ddlPeriodo != "10" && ddlPeriodo != "11" && ddlPeriodo != "12" && ddlPeriodo != "13"
                && ddlPeriodo != "14" && ddlPeriodo != "15" && ddlPeriodo != "16" && ddlPeriodo != "17"
                 && ddlPeriodo != "18" && ddlPeriodo != "19" && ddlPeriodo != "20" && ddlPeriodo != "21"
                 && ddlPeriodo != "22" && ddlPeriodo != "23" && ddlPeriodo != "24")
            {
                ClassPeriodo per = new ClassPeriodo();

                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                    dosis = double.Parse(TPresent.Replace(".", ",")) * double.Parse(per.mfFactorPeriodo(ddlPeriodo)) * double.Parse(TRango);


            }
            else if (ddlPeriodo == "10" || ddlPeriodo == "15")
            {
                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                    dosis = double.Parse(TPresent.Replace(".", ",")) * (double.Parse(TRango));
            }
            else if (ddlPeriodo == "11" || ddlPeriodo == "18" || ddlPeriodo == "19" ||
                    ddlPeriodo == "20" || ddlPeriodo == "21" || ddlPeriodo == "22" ||
                    ddlPeriodo == "23" || ddlPeriodo == "24")
            {
                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                {
                    int div = 1;

                    if (ddlPeriodo == "18" || ddlPeriodo == "19" || ddlPeriodo == "22")
                        div = 3;
                    else if (ddlPeriodo == "21")
                        div = 2;
                    else if (ddlPeriodo == "23")
                        div = 4;
                    else if (ddlPeriodo == "24")
                        div = 5;
                    else
                        div = 1;

                    dosis = (double.Parse(TPresent.Replace(".", ",")) * (double.Parse(TRango) / 7)) * div;
                }


            }
            else if (ddlPeriodo == "12" || ddlPeriodo == "14")
            {
                if (TxtPosologia == "")
                    dosis = 0;
                else
                    dosis = double.Parse(TxtPosologia.Replace(".", ","));

            }
            else if (ddlPeriodo == "13" || ddlPeriodo == "16" || ddlPeriodo == "17")
            {
                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                {
                    double valor = 1;

                    if (ddlPeriodo == "16") valor = 0.5;
                    if (ddlPeriodo == "17") valor = 0.333;

                    if (TDias == "1")
                    {
                        if (TRango == "30" || TRango == "60" || TRango == "90")
                        {

                            if (ddlPeriodo == "16")
                            {
                                if (TRango == "30") valor = 0.5;
                                if (TRango == "60") valor = 1;
                                if (TRango == "90") valor = 1.5;


                            }
                            else if (ddlPeriodo == "17")
                            {
                                if (TRango == "30") valor = 1;
                                if (TRango == "60") valor = 1;
                                if (TRango == "90") valor = 1;


                            }
                            else
                            {
                                if (TRango == "30") valor = 1;
                                if (TRango == "60") valor = 2;
                                if (TRango == "90") valor = 3;
                            }


                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                        else
                        {
                            TRango = "30";
                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                    }
                    else if (TDias == "2")
                    {
                        if (TRango == "30" || TRango == "60" || TRango == "90" || TRango == "180" || TRango == "360")
                        {

                            if (ddlPeriodo == "16")
                            {
                                if (TRango == "30") valor = 0.5;
                                if (TRango == "60") valor = 1;
                                if (TRango == "90") valor = 1.5;
                                if (TRango == "180") valor = 3;
                                if (TRango == "360") valor = 6;


                            }
                            else if (ddlPeriodo == "17")
                            {
                                if (TRango == "30") valor = 1;
                                if (TRango == "60") valor = 1;
                                if (TRango == "90") valor = 1;
                                if (TRango == "180") valor = 2;
                                if (TRango == "360") valor = 4;


                            }
                            else
                            {

                                if (TRango == "30") valor = 1;
                                if (TRango == "60") valor = 2;
                                if (TRango == "90") valor = 3;
                                if (TRango == "180") valor = 6;
                                if (TRango == "360") valor = 12;
                            }

                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                        else
                        {
                            TRango = "30";
                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                    }


                }

            }



        }

        decimal dosisV = Convert.ToDecimal(dosis) / Convert.ToDecimal(art.mfGetFactor(hdIdMat));
        return TxtPosologia = Math.Ceiling(dosisV).ToString();

    }

    public string mfCalcularPosologia2(string hdIdMat, string TCalc0, string ddlPeriodo, string TPresent, string TRango, string TDias)
    {
        ClassArticulos art = new ClassArticulos();

        double dosis = 0;
        string TxtPosologia = "0";

        if (TCalc0 == "2" && (ddlPeriodo != "12" && ddlPeriodo != "14"))
        {

            dosis = 0;

        }
        else
        {
            if (ddlPeriodo != "10"
                 && ddlPeriodo != "11"
                 && ddlPeriodo != "12"
                 && ddlPeriodo != "13"
                 && ddlPeriodo != "14")
            {
                ClassPeriodo per = new ClassPeriodo();

                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                    dosis = double.Parse(TPresent.Replace(".", ",")) * double.Parse(per.mfFactorPeriodo(ddlPeriodo)) * double.Parse(TRango);


            }
            else if (ddlPeriodo == "10")
            {
                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                    dosis = double.Parse(TPresent.Replace(".", ",")) * (double.Parse(TRango));
            }
            else if (ddlPeriodo == "11")
            {
                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                    dosis = double.Parse(TPresent.Replace(".", ",")) * (double.Parse(TRango) / 7);

            }
            else if (ddlPeriodo == "12" || ddlPeriodo == "14")
            {
                if (TxtPosologia == "")
                    dosis = 0;
                else
                    dosis = double.Parse(TxtPosologia.Replace(".", ","));

            }
            else if (ddlPeriodo == "13")
            {
                if (TPresent == "" || TRango == "")
                {
                    dosis = 0;
                }
                else
                {
                    int valor = 1;

                    if (TDias == "1")
                    {
                        if (TRango == "30" || TRango == "60" || TRango == "90")
                        {


                            if (TRango == "30") valor = 1;
                            if (TRango == "60") valor = 2;
                            if (TRango == "90") valor = 3;

                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                        else
                        {
                            TRango = "30";
                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                    }
                    else if (TDias == "2")
                    {
                        if (TRango == "30" || TRango == "60" || TRango == "90" || TRango == "180" || TRango == "360")
                        {


                            if (TRango == "30") valor = 1;
                            if (TRango == "60") valor = 2;
                            if (TRango == "90") valor = 3;

                            if (TRango == "180") valor = 6;

                            if (TRango == "360") valor = 12;

                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                        else
                        {
                            TRango = "30";
                            dosis = double.Parse(TPresent.Replace(".", ",")) * valor;
                        }
                    }


                }

            }


        }

        decimal dosisV = Convert.ToDecimal(dosis) / Convert.ToDecimal(art.mfGetFactor(hdIdMat));
        return TxtPosologia = Math.Ceiling(dosisV).ToString();

    }
    #endregion

    #region DOC
    public string mfIngresarDoc(string asIdentificador, string asDocumento, string asDesc, string asTipo, string asTipoDoc)
    {
        string lsRet = "";
        string lsSql = "insert into " + modConstantes.gsDbAB + "M_RECETA_DOC(" +
                        " IDRECETA, F_H_CREACION, IDESTADO, " +
                        " DOCUMENTO, DESCRIPCION," +
                        " TIPO)" +
                        " values(                   " +
                        " " + asIdentificador + ",  " +
                        " getdate() ,               " +
                        " 1,                        " +
                        " '" + asDocumento + "',    " +
                        " '" + asDesc.Replace(",", "") + "',         " +
                        " '" + asTipo + "' " +
                        " )";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public DataSet mfCargaDoc(string lsIdentificador)
    {
        DataSet aoBind;
        string lsSql = "";

        lsSql = "select IDRECDOC,cd.DESCRIPCION, CD.F_H_CREACION, 'RECETA ORIGINAL' DESC_TIPODOC " +
                "from " + modConstantes.gsDbAB + "[M_RECETA_DOC] cd  " +
                "where CD.[IDRECETA] = " + lsIdentificador + " " +
                "order by CD.F_H_CREACION desc ";

        con = bd.fnGetConn();
        aoBind = bd.Fill(con, lsSql);
        con.Close();
        return aoBind;
    }

    public DataSet mfCargaDetDoc(string lsIdentificador)
    {
        DataSet aoBind;
        string lsSql = "";

        lsSql = "SELECT IDRECETA, F_H_CREACION, IDESTADO, DOCUMENTO, DESCRIPCION, TIPO " +
                "FROM " + modConstantes.gsDbAB + "[M_RECETA_DOC] " +
                "WHERE IDRECDOC = " + lsIdentificador;

        con = bd.fnGetConn();
        aoBind = bd.Fill(con, lsSql);
        con.Close();
        return aoBind;
    }

    public string mfEliminarDoc(string asIdentificador)
    {
        string lsRet = "";


        string lsSql = "delete from " + modConstantes.gsDbAB + "[M_RECETA_DOC] " +
                " where IDRECDOC = " + asIdentificador;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();


        return lsRet;

    }
    #endregion

}