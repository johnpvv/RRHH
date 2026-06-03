using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de Reportes
/// </summary>
public class Reportes
{
    public string lb_bod2 { get; set; }
    // Atributos fecha vencimiento

    public string is_fd { get; set; }
    public string is_fh { get; set; }
    public string lsNom { get; set; }

    public string ls_f_d { get; set; }
    public string ls_f_h { get; set; }
    public string ls_uni { get; set; }
    public string ls_num { get; set; }
    public string ls_Codigo { get; set; }
    public string ls_Art { get; set; }
    public string ls_rut { get; set; }
    public string ls_nomb { get; set; }
    public string ls_tipo { get; set; }

    public string ls_idusr { get; set; }

    public string ls_IdServ { get; set; }
    public string ls_IdSubServ { get; set; }
    public string ls_IdCama { get; set; }
    public string ls_All { get; set; }

    public string ls_Esp { get; set; }

    public string ls_Med { get; set; }
    public string ls_Farm { get; set; }
    public string ls_Infec { get; set; }
    public string ls_Salud { get; set; }
    public string ls_Estado { get; set; }

    public string ls_Rut_M { get; set; }
    public string ls_Nomb_M { get; set; }
    public string lb_bod { get; set; }
    public string lb_mes { get; set; }
    public string lb_anio { get; set; }

    // micro
    public string ls_error_api { get; set; }
    public int li_total_despacho_paciente { get; set; }
    public int li_total_despacho_receta { get; set; }
    public int li_total_precritas_receta { get; set; }

    // end Micro

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public Reportes()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    #region Medicos


    public DataSet mfrptMedRec()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = "";
        string lsOrder = " order by a.IDUSUARIO, b.NOMBRE,FORMAT(FECHA, 'dd-MM-yyyy') ";
        string lsGroup = " group by a.IDUSUARIO, b.rut, b.DV, b.NOMBRE, UN.DESCRIPCION, FORMAT(FECHA, 'dd-MM-yyyy') ,b.idestado ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            lsWhere += " and  ( FECHA >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   FECHA <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {

            lsWhere += " AND ( b.rut = " + ls_rut + " ) ";

        }



        // Si corresponde agrega bodega.
        if (lsNom != "")
            lsWhere += " and b.nombre like '%" + lsNom + "%' ";


        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
        {

            lsWhere += " AND ( A.CODUNIOP = " + ls_IdServ + " ) ";

        }

        // Si se ingresó Servicio la agrega al Where.
        if (ls_Estado != "0")
        {
            if (ls_Estado == "3") lsWhere += " AND ( b.idestado = 3 ) "; else lsWhere += " AND ( b.idestado <> 3 ) ";


        }



        //lsSelect = " select a.IDUSUARIO, b.nombre,FORMAT(FECHA, 'dd-MM-yyyy')  as FECHA, count(*) as cantidad_recetas " +
        //            "from M_RECETA a, m_usuarios b " +
        //            "where a.IDUSUARIO=b.IDUSUARIO " +
        //            "and a.IDUSUARIO not in(1,2) " +
        //            "and a.IDESTADO <> 1 ";

        //lsSelect = " select a.IDUSUARIO IdMed, b.nombre Nombre,FORMAT(FECHA, 'dd-MM-yyyy')  as FECHA, count(*) as Cantidad " +
        //            "from M_RECETA a, m_usuarios b " +
        //            "where a.IDUSUARIO=b.IDUSUARIO " +
        //            "and a.IDESTADO <> 1 ";

        lsSelect = " select a.IDUSUARIO IdMed, (CONVERT(VARCHAR(10),b.rut) + '-' + b.dv) rut, b.nombre Nombre, " +
                     "FORMAT(FECHA, 'dd-MM-yyyy')  as FECHA, UN.DESCRIPCION UNIDAD, " +
                     "count(*) as Cantidad, case when b.idestado = 3 then 'No Vigente' else 'Vigente' end estado " +
                     "from M_RECETA a  " +
                     "INNER JOIN m_usuarios b ON a.IDUSUARIO=b.IDUSUARIO  " +
                     "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = A.CODUNIOP " +
                    " WHERE a.IDESTADO <> 1 ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptDetInfect()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = "";
        string is_crit = "";
        string lsOrder = " ORDER BY  R.F_H_CREACION DESC ";
        string lsGroup = " ";



        if (ls_f_d != "" && ls_All == "NotAll")
        {

            lsWhere += " and  (cast( '" + ls_f_d + " 00:00:00' as datetime) BETWEEN  dateadd(day,-1, R.F_H_INGRESO) and dateadd(day,(select Max(rango) + 1 from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de creación entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }




        //// Si se ingresó Tipo la agrega al Where.
        //if (ls_tipo != "")
        //{
        //    if (lsWhere != "")
        //        lsWhere += " and ";

        //    lsWhere += " ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";
        //    // Agrega criterio a var. aux.
        //    is_crit += " Tipo Solicitud: " + ls_tipo + " , ";
        //}

        // Si se ingresó Numero la agrega al Where.
        if (ls_num != "")
        {

            lsWhere += " AND ( R.FOLIO = " + ls_num + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Numero igual a: " + ls_num + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {

            lsWhere += " AND ( R.rut = " + ls_rut + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {

            lsWhere += " AND ( R.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }


        // Si se ingresó Articulo Solicitud la agrega al Where.
        if (ls_Art != "")
        {

            lsWhere += " AND ( M.DESCRIPCION_LARGA LIKE '%" + ls_Art + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Articulo Solicitud: " + ls_Art + " , ";
        }

        // Si se ingresó Código Solicitud la agrega al Where.
        if (ls_Codigo != "")
        {

            lsWhere += " AND ( M.CODARTICULO = '" + ls_Codigo + "' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Codigo + " , ";
        }

        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
        {

            lsWhere += " AND ( R.IDSERVICIO = " + ls_IdServ + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdServ + " , ";
        }

        // Si se ingresó Sub servicio la agrega al Where.
        if (ls_IdSubServ != "0" && ls_IdSubServ != "")
        {

            lsWhere += " AND ( R.IDSUBSERVICIO = " + ls_IdSubServ + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdSubServ + " , ";
        }

        // Si se ingresó Cama la agrega al Where.
        if (ls_IdCama != "0" && ls_IdCama != "")
        {

            lsWhere += " AND ( R.IDCAMA = " + ls_IdCama + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdCama + " , ";
        }



        lsSelect = "SELECT R.IDRECETA, R.FOLIO, R.F_H_CREACION, " +
                    "R.NOMBRE, R.APELL_PAT, R.APELL_MAT, " +
                    "R.RUT, EST.DESCRIPCION ESTADO, " +
                    "CONVERT(VARCHAR(10),R.F_H_INGRESO,103) F_H_INGRESO,   " +
                    "CONVERT(VARCHAR(10),dateadd(day,(select Max(rango) from M_ART_RECETA where IDRECETA = R.IDRECETA),R.F_H_INGRESO),103) F_H_FIN,  " +
                    "UN.DESCRIPCION UNIDAD, SUN.DESCRIPCION SUBUNIDAD, CA.DESCRIPCION CAMA  " +
                    ", M.CODARTICULO, M.DESCRIPCION_LARGA nom_articulo, " +
                    "(case when ((ISNULL(ART.CANTIDAD,0)-round(ISNULL(ART.CANTIDAD,0),0,1)) > 0)  " +
                    "then convert(varchar(10),convert(decimal(20,2),ISNULL(ART.CANTIDAD,0)))  " +
                    "else convert(varchar(10),convert(decimal(20,0),ISNULL(ART.CANTIDAD,0)))  " +
                    "end + ' ' + M.UNI_MIN + ' ' + ISNULL(PE.DESCRIPCION,'-')) FRECUENCIA, " +
                    "ISNULL(VIA.DESCRIPCION,'-') DVIA, " +
                    "convert(decimal(20,0),ISNULL(ART.RANGO,0)) DIAS_TRAT, " +
                    "OBSERVACIONES, usr.NOMBRE MEDICO " +
                    "FROM M_RECETA R  " +
                    "INNER JOIN M_ART_RECETA ART ON ART.IDRECETA = R.IDRECETA " +
                    "INNER JOIN v_articulos M ON M.IDARTICULO = ART.IDARTICULO " +
                    "left outer join [M_VIA] VIA ON VIA.IDVIA = ART.IDVIA " +
                    "left outer JOIN [M_RANGO] RA ON RA.IDRANGO = ART.IDRANGO  " +
                    "LEFT OUTER JOIN [M_PERIODO] PE ON PE.IDPERIODO = ART.IDPERIODO  " +
                    "INNER JOIN TG_ESTADOS EST ON EST.IDESTADO = R.IDESTADO  " +
                    "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = R.IDSERVICIO  " +
                    "INNER JOIN M_SUB_UNIDAD SUN ON SUN.IDSUBUNIDAD = R.IDSUBSERVICIO  " +
                    "INNER JOIN M_CAMAS CA ON CA.IDCAMA = R.IDCAMA    " +
                    "inner join m_usuarios usr on usr.IDUSUARIO = r.IDUSUARIO " +
                    "WHERE ( r.idestado in (2, 3, 5) and R.FOLIO > 0 ) and ( isnull(R.TIPO,1) = '" + ls_tipo + "' ) ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptUsrFarm()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = "";
        string is_crit = "";
        string lsOrder = " ORDER BY  US.RUT DESC ";
        string lsGroup = " ";
        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {

            lsWhere += " AND ( US.RUT = " + ls_rut + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }
        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {

            lsWhere += " AND ( US.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }
        lsSelect = "SELECT  CONVERT(VARCHAR(10),US.RUT) + '-' + CONVERT(CHAR(1),US.DV) RUT, US.NOMBRE, " +
                    "US.ESPECIALIDAD, US.EMAIL, ISNULL(R.DESCRIPCION,'SIN ROL')AS ROL, ISNULL(AP.DESCRIPCION,'SIN ACCESOS')AS ACCESO, " +
                    "CASE WHEN ISNULL(US.MEDICO,0) = 1 THEN 'SI' ELSE 'NO' END MEDICO, " +
                    "CASE WHEN ISNULL(US.FARMACIA,0) = 1 THEN 'SI' ELSE 'NO' END FARMACIA, " +
                    "CASE WHEN ISNULL(US.INFECTOLOGIA,0) = 1 THEN 'SI' ELSE 'NO' END INFECTOLOGIA, " +
                    "CASE WHEN ISNULL(US.SALUD,0) = 1 THEN 'SI' ELSE 'NO' END SALUD, dbo.fn_receta_30_dias_func(US.IDUSUARIO) MES," +
                    "dbo.fn_receta_90_dias_func(US.IDUSUARIO)  MESES, case when US.idestado = 3 then 'No Vigente' else 'Vigente' end estado " +
                    "FROM M_USUARIOS US " +
                    "LEFT JOIN M_USROL UR ON UR.IDUSUARIO = US.IDUSUARIO AND UR.IDESTADO <> 3" +
                    "LEFT JOIN TG_ROLES R ON R.IDROL = UR.IDROL AND R.IDESTADO <> 3" +
                    "LEFT JOIN M_USAPP UA ON UA.IDUSUARIO = US.IDUSUARIO AND UA.IDESTADO <> 3" +
                    "LEFT JOIN TG_APPS AP ON AP.IDAPP = UA.IDAPP AND UA.IDESTADO <> 3" +
                    "WHERE ISNULL(US.FARMACIA,0) = 1 ";
        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;
        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();
        return lsCodigo;
    }

    public DataSet mfrptRegMed()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = "";
        string is_crit = "";
        string lsOrder = " ORDER BY  US.RUT DESC ";
        string lsGroup = " ";

        // Si se ingresó Medico la agrega al Where.
        if (ls_Med != "0")
        {

            lsWhere += " AND ( ISNULL(US.MEDICO,0) = 1 ) ";
            // Agrega criterio a var. aux.
            is_crit += " Medico igual a: " + ls_Med + " , ";
        }

        // Si se ingresó Farmacia la agrega al Where.
        if (ls_Farm != "0")
        {

            lsWhere += " AND ( ISNULL(US.FARMACIA,0) = 1 ) ";
            // Agrega criterio a var. aux.
            is_crit += " Farmacia igual a: " + ls_Farm + " , ";
        }

        // Si se ingresó Infectologia la agrega al Where.
        if (ls_Infec != "0")
        {

            lsWhere += " AND ( ISNULL(US.INFECTOLOGIA,0) = 1 ) ";
            // Agrega criterio a var. aux.
            is_crit += " INFECTOLOGIA igual a: " + ls_Infec + " , ";
        }

        // Si se ingresó Salud la agrega al Where.
        if (ls_Salud != "0")
        {

            lsWhere += " AND ( ISNULL(US.SALUD,0) = 1 ) ";
            // Agrega criterio a var. aux.
            is_crit += " SALUD igual a: " + ls_Salud + " , ";
        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
        {

            lsWhere += " AND ( US.RUT = " + ls_rut + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Rut igual a: " + ls_rut + " , ";
        }

        // Si se ingresó Nombre la agrega al Where.
        if (ls_nomb != "")
        {

            lsWhere += " AND ( US.NOMBRE like '%" + ls_nomb + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Nombre igual a: " + ls_nomb + " , ";
        }



        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
        {

            lsWhere += " AND ( UU.CODUNIOP = " + ls_IdServ + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdServ + " , ";
        }

        // Si se ingresó Especialidad la agrega al Where.
        if (ls_Esp != "")
        {

            lsWhere += " AND ( US.ESPECIALIDAD like '%" + ls_Esp + "%' ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_Esp + " , ";
        }

        // Si se ingresó Servicio la agrega al Where.
        if (ls_Estado != "0")
        {
            if (ls_Estado == "3") lsWhere += " AND ( US.idestado = 3 ) "; else lsWhere += " AND ( US.idestado <> 3 ) ";


        }


        lsSelect = "SELECT  CONVERT(VARCHAR(10),US.RUT) + '-' + CONVERT(CHAR(1),US.DV) RUT, US.NOMBRE,  " +
                    "US.ESPECIALIDAD, US.EMAIL,  " +
                    "UN.DESCRIPCION UNIDAD, " +
                    "CASE WHEN ISNULL(US.MEDICO,0) = 1 THEN 'SI' ELSE 'NO' END MEDICO, " +
                    "CASE WHEN ISNULL(US.FARMACIA,0) = 1 THEN 'SI' ELSE 'NO' END FARMACIA, " +
                    "CASE WHEN ISNULL(US.INFECTOLOGIA,0) = 1 THEN 'SI' ELSE 'NO' END INFECTOLOGIA, " +
                    "CASE WHEN ISNULL(US.SALUD,0) = 1 THEN 'SI' ELSE 'NO' END SALUD, dbo.fn_receta_mes(US.IDUSUARIO ) MES," +
                    "dbo.fn_Receta30(US.IDUSUARIO )  MESES, case when US.idestado = 3 then 'No Vigente' else 'Vigente' end estado " +
                    "FROM M_USUARIOS US " +
                    "INNER JOIN M_USER_UNIDAD UU ON UU.IDUSUARIO = US.IDUSUARIO " +
                    "INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = UU.CODUNIOP " +
                    "WHERE US.IDUSUARIO > 0 ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptArsenal()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = "";
        string is_crit = "";
        string lsOrder = " ORDER BY  CODARTICULO DESC ";
        string lsGroup = " ";



        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
        {

            lsWhere += " AND ( AU.CODUNIOP = " + ls_IdServ + " ) ";
            // Agrega criterio a var. aux.
            is_crit += " Código Solicitud: " + ls_IdServ + " , ";
        }




        lsSelect = "SELECT V.CODARTICULO, V.DESCRIPCION_LARGA, V.UN_MED, V.UNI_MIN, " +
                    "VIA.DESCRIPCION VIA,  " +
                    "CASE WHEN ISNULL(V.CALCULAR,1) = '1' THEN 'SI' ELSE 'NO' END CALCULAR " +
                    "FROM v_articulos V  " +
                   " INNER JOIN M_ART_UNIDAD AU ON AU.IDARTICULO = V.IDARTICULO " +
                   " INNER JOIN M_ART_VIA AV ON AV.IDARTICULO = V.IDARTICULO " +
                    "INNER JOIN M_VIA VIA ON VIA.IDVIA = AV.IDVIA " +
                    "WHERE  V.IDARTICULO > 0 ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }
    #endregion

    #region Dispensacion 

    public DataSet mfdllAnioEstadistica()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select DISTINCT ANIO CODIGO, ANIO DESCRIPCION from[dbo].[V_AUDITORIA_GRAL_1] ORDER BY 1 DESC ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfEstadisticaAnoMes(string lsMes, string lsAnio, string lsBod)
    {
        string lsSalida = "";

        string lsSQL = "SELECT COUNT(1) " +
                        "FROM V_AUDIT_CAB " +
                        "WHERE ANIO =  " + lsAnio + " " +
                        "AND MES = " + lsMes + " " +
                        "and IDBODPRERIF = " + lsBod;

        con = bd.fnGetConn();
        lsSalida = bd.ExecuteScalar(con, lsSQL);
        con.Close();

        return lsSalida;
    }

    public DataSet mfrptDespPend()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where IDESTADO = 13 ";
        string lsOrder = " order by FULT_DESP desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,FULT_DESP,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,FULT_DESP,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and DESCRIPCION_LARGA like '%" + ls_Art + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_Rut_M != "")
            lsWhere += " AND ( RUT_MED = " + ls_Rut_M + " ) ";


        // Si corresponde Rut Med.
        if (ls_Nomb_M != "")
            lsWhere += " and MEDICO like '%" + ls_Nomb_M + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( rut = " + ls_rut + " ) ";



        // Si corresponde agrega bodega.
        if (lb_bod != "0")
            lsWhere += " AND ( IDBODPRERIF = " + lb_bod + " ) ";


        if (lsNom != "")
            lsWhere += " and nombre like '%" + lsNom + "%' ";


        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND ( CODUNIOP = " + ls_IdServ + " ) ";




        lsSelect = " SELECT FOLIO, rut_pac, " +
                   "  NOMBRE_PAC, FRECUENCIA," +
                    "CODARTICULO, DESCRIPCION_LARGA,  FECHA, " +
                    "RANGO, RANGO_DISP, POSOLOGIA, POSOLOGIA_DISP, " +
                    "CANT_DESP, CANT_PEND, ACUM_RANGO, FULT_DESP, MEDICO, RUT_MED " +
                    "FROM V_RPT_DESP_PEND ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptDespControl()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where b.IDRECETA > 0 AND CANTIDAD > 0 ";
        string lsOrder = " order by b.FOLIO desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,b.FDESPACHO,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,b.FDESPACHO,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( b.CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and b.NOMB_ART like '%" + ls_Art + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_Rut_M != "")
            lsWhere += " AND ( b.RUT_MED_Q = " + ls_Rut_M + " ) ";


        // Si corresponde Rut Med.
        if (ls_Nomb_M != "")
            lsWhere += " and b.MEDICO like '%" + ls_Nomb_M + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( b.RUT_PAC = " + ls_rut + " ) ";



        // Si corresponde agrega bodega.
        // Si se ingresó Servicio la agrega al Where.
        if (lb_bod != "0")
            lsWhere += " AND ( b.IDBODPRERIF = " + lb_bod + " ) ";

        if (lsNom != "")
            lsWhere += " and b.nombre like '%" + lsNom + "%' ";


        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND ( b.CODUNIOP = " + ls_IdServ + " ) ";


        // Si se ingresó Servicio la agrega al Where.
        if (ls_Estado != "0")
        {
            if (ls_Estado == "3") lsWhere += " AND ( b.idestado = 3 ) "; else lsWhere += " AND ( b.idestado <> 3 ) ";


        }


        //lsSelect = " select FOLIO, NUM_REC_MANUAL, NUMOSAL, BODEGA, ESTADO, DIAGNOSTICO,  " +
        //                "MEDICO, (RUT_MED + '-' + DV_MED) RUT_MED, UNIDAD,  " +
        //                "CODARTICULO, NOMB_ART, FRECUENCIA, VIA, OBS_MED,  " +
        //                "FDESPACHO, CANTIDAD, DIAS, FNEXTDESPACHO, CANT_PEND, OBSFARM,  " +
        //                "RUT, (NOMBRE + ' ' + APELL_PAT + ' ' + APELL_MAT)  NOMBRE," +
        //                "FONO1_CONT, OBS1_CONT, FONO2_CONT, OBS2_CONT, MAIL,  " +
        //                "(DIRECCION + ' ' + COMUNA + ' ' + REGION) DIRECCION, NombAdq, RutAdq, FonoAdq " +
        //                "from V_DESP_DISP b ";

        lsSelect = " select FOLIO, NUM_REC_MANUAL, NUMOSAL, BODEGA, ESTADO, DIAGNOSTICO,  " +
                        "MEDICO,  RUT_MED, UNIDAD,  " +
                        "CODARTICULO, NOMB_ART, FRECUENCIA, VIA, OBS_MED,  " +
                        "FDESPACHO, CANTIDAD, DIAS, FNEXTDESPACHO, CANT_PEND, OBSFARM,  " +
                        "RUT, (NOMBRE + ' ' + APELL_PAT + ' ' + APELL_MAT)  NOMBRE," +
                        "FONO1_CONT, OBS1_CONT, FONO2_CONT, OBS2_CONT, MAIL,  " +
                        "(DIRECCION + ', ' + COMUNA + ' ') DIRECCION, NombAdq, RutAdq, FonoAdq " +
                        "from V_DESP_DISP b ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptDepRec()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where b.IDRECETA > 0 AND ( b.idestado <> 3 ) AND CANTIDAD > 0 ";
        string lsOrder = " order by b.FDESPACHO desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,b.FDESPACHO,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,b.FDESPACHO,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( b.CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and b.NOMB_ART like '%" + ls_Art + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_Rut_M != "")
            lsWhere += " AND ( b.RUT_MED_Q = " + ls_Rut_M + " ) ";


        // Si corresponde Rut Med.
        if (ls_Nomb_M != "")
            lsWhere += " and b.MEDICO like '%" + ls_Nomb_M + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( b.RUT_PAC = " + ls_rut + " ) ";



        // Si corresponde agrega bodega.
        if (lb_bod != "0")
            lsWhere += " AND ( b.IDBODPRERIF = " + lb_bod + " ) ";


        if (lsNom != "")
            lsWhere += " and b.nombre like '%" + lsNom + "%' ";


        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND ( b.CODUNIOP = " + ls_IdServ + " ) ";




        lsSelect = " select FOLIO, NUMOSAL, BODEGA, ESTADO, DIAGNOSTICO, NUM_REC_MANUAL, " +
                        "MEDICO, RUT_MED, UNIDAD,  " +
                        "CODARTICULO, NOMB_ART, FRECUENCIA, VIA, OBS_MED,  " +
                        "FDESPACHO, CANTIDAD, DIAS, FNEXTDESPACHO, CANT_PEND, OBSFARM,  " +
                        "RUT, NOMBRE, APELL_PAT, APELL_MAT,  " +
                        "FONO1_CONT, OBS1_CONT, FONO2_CONT, OBS2_CONT, MAIL,  " +
                        "DIRECCION, COMUNA, REGION, NombAdq, RutAdq, FonoAdq " +
                        "from V_DESP_DISP b ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptFarmacoPacientesAgendados()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " WHERE ISNULL(P.ISPROGRAMA,0) <> 0 ";
        string lsOrder = " ORDER BY R.RUT DESC ";
        string lsGroup = " ";


        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( V.CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and V.DESCRIPCION_LARGA like '%" + ls_Art + "%' ";

        lsSelect = " SELECT DISTINCT V.CODARTICULO, V.DESCRIPCION_LARGA NOM_ARTICULO, R.RUT, R.DV, R.NOMBRE, R.APELL_PAT, R.APELL_MAT, BD.DESCRIPCION_LARGA BODEGA " +
                    " FROM M_RECETA R " +
                    " INNER JOIN M_ART_RECETA AR ON AR.IDRECETA = R.IDRECETA " +
                    " INNER JOIN v_articulos V ON V.IDARTICULO = AR.IDARTICULO " +
                    " INNER JOIN M_PACIENTE P ON P.RUT = R.RUT " +
                    "INNER JOIN " + modConstantes.gsDbPer + "TG_BOD_PERIFERICAS BD ON BD.IDBODPRERIF = R.IDBODPRERIF ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptPrecritasRec()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where V.FOLIO > 0 AND V.ESTADO_RECETA <> 3 ";
        string lsOrder = " order by V.FOLIO desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,V.F_H_CREACION,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,V.F_H_CREACION,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( V.CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and V.NOM_ARTICULO like '%" + ls_Art + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_Rut_M != "")
            lsWhere += " AND ( CASE WHEN V.RUT_MED = -1 THEN US.RUT ELSE V.RUT_MED END = " + ls_Rut_M + " ) ";


        // Si corresponde Rut Med.
        if (ls_Nomb_M != "")
            lsWhere += " and CASE WHEN V.RUT_MED = -1 THEN US.NOMBRE ELSE V.NOMB_MED END like '%" + ls_Nomb_M + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( V.RUT = " + ls_rut + " ) ";




        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND ( V.CODUNIOP = " + ls_IdServ + " ) ";




        lsSelect = " select V.FOLIO, NUM_REC_MANUAL, V.F_H_CREACION, V.NOMBRE, V.RUT, V.DV, V.ESTADO, V.APELL_PAT, V.APELL_MAT, " +
                    " UN.DESCRIPCION UNIDAD,  " +
                    " CASE WHEN V.RUT_MED = -1 THEN US.RUT ELSE V.RUT_MED END RUT_MEDICO,   " +
                    " CASE WHEN V.RUT_MED = -1 THEN US.NOMBRE ELSE V.NOMB_MED END NOMBRE_MEDICO,  " +
                    "V.TipoAdq, V.NombAdq, V.RutAdq, V.FonoAdq, V.NombreSocial, V.OBS_FARMACIA, " +
                    "V.DIRECCION_PAC, V.OBSERVACION, V.DIAGNOSTICO, V.F_H_INGRESO, V.CODARTICULO, V.NOM_ARTICULO,  " +
                    "V.FRECUENCIA, V.DESC_UN_MED, V.CALCULAR, V.CANTIDAD, V.DURACION, V.RANGO, V.RANGO_DISP,  " +
                    "V.POSOLOGIA_DISP, V.POSOLOGIA, V.DVIA, V.DRANGO, V.DPRESENT, V.DPERIODO, V.OBSERVACIONES,  " +
                    "V.UNI_MIN, V.OBS_FARM, V.CANT_ENTREGA, V.FDESPACHO, V.PENDIENTE, V.SALDO, V.OBS, V.cant_desp_req,  " +
                    "V.cant_desp_2, V.IDPRESENTACION, V.IDRANGO, V.IDVIA, V.IDPERIODO, V.IDARTICULO, V.IDBODPRERIF,  " +
                    "V.IDESTADO, V.IDARTRECETA, V.IDREGION, V.IDCOMUNA, V.DIRECCION, V.IDREGION_PAC, V.IDCOMUNA_PAC,  " +
                   "V.IDDIAGNOSTICO, V.CODUNIOP, V.IDSERVICIO, V.IDSUBSERVICIO, V.IDCAMA " +
                    "from[dbo].[V_RECETA_DETALLE_REP] V " +
                    " INNER JOIN M_USUARIOS US ON US.IDUSUARIO = V.IDUSUARIO " +
                    " INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = V.CODUNIOP ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptPacientesAgendados()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where RUT > 0 AND ISNULL(ISPROGRAMA, 0 ) <> 0 ";
        string lsOrder = " order by RUT desc ";
        string lsGroup = " ";


        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( RUT = " + ls_rut + " ) ";

        // Si se ingresó Nombre la agrega al Where.
        if (lsNom != "")
            lsWhere += " AND ( NOMBRE like '%" + lsNom + "%' OR APELL_PAT like '%" + lsNom + "%' OR APELL_MAT like '%" + lsNom + "%') ";

        lsSelect = " SELECT (CONVERT(VARCHAR(10),RUT) + '-' + DV) RUT,  DV, NOMBRE, AP_PATERNO, AP_MATERNO,  " +
                    " DIRECCION, Email , Telefono, TelefonoMovil, CO.DESCRIPCION COMUNA, " +
                    " NombreSocial, FONO1_CONT FONO1, FONO2_CONT FONO2, (MAIL_CONT + '@' + MAIL2_CONT) CORREO , " +
                    "(SELECT TOP 1 AR.FULT_DESP " +
                    "   FROM M_RECETA R " +
                    "   INNER JOIN M_ART_RECETA AR ON AR.IDRECETA = R.IDRECETA " +
                    "   WHERE R.IDESTADO IN(2,13) " +
                    "   AND RUT = P.RUT" +
                    "   ORDER BY AR.FULT_DESP ASC) FDESPACHO, " +
                    " (SELECT TOP 1 AR.FDESPACHO " +
                    "   FROM M_RECETA R " +
                    "   INNER JOIN M_ART_RECETA AR ON AR.IDRECETA = R.IDRECETA " +
                    "   WHERE R.IDESTADO IN(2,13) " +
                    "   AND RUT = P.RUT" +
                    "   ORDER BY AR.FDESPACHO ASC) FPROXDESPACHO, " +
                     "   (SELECT CASE WHEN COUNT(1) > 0 THEN 'SI' ELSE 'NO' END " +
                     "     FROM M_RECETA " +
                      "    WHERE IDESTADO = 4 " +
                     "     AND RUT = p.RUT) VIGENTE,  " +
                     "      (SELECT CASE WHEN COUNT(1) > 0 THEN 'SI' ELSE 'NO' END " +
                      "    FROM M_RECETA " +
                     "     WHERE IDESTADO = 13 " +
                      "    AND RUT = p.RUT) DPENDIENTES " +
                    " FROM M_PACIENTE P " +
                    " INNER JOIN[dbo].[TG_COMUNA] CO ON CO.IDCOMUNA = P.IDCOMUNA ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }
    public DataSet mfrptPrecritasAgendadosRec()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where V.FOLIO > 0 AND V.ESTADO_RECETA IN (13,4) ";
        string lsOrder = " order by V.FOLIO desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,V.FDESPACHO,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,V.FDESPACHO,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( V.CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and V.NOM_ARTICULO like '%" + ls_Art + "%' ";



        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( V.RUT = " + ls_rut + " ) ";

        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND (V.CODUNIOP = " + ls_IdServ + " ) ";


        // Si se ingresó Servicio la agrega al Where.
        if (lb_bod != "0")
            lsWhere += " AND ( V.IDBODPRERIF = " + lb_bod + " ) ";




        lsSelect = " select V.FOLIO, V.NUM_REC_MANUAL, V.F_H_INGRESO, V.NOMBRE, (CONVERT(VARCHAR(10),V.RUT) + '-' + V.DV) RUT,  V.DV, V.ESTADO, V.APELL_PAT, V.APELL_MAT, " +
                    " UN.DESCRIPCION UNIDAD, V.BODEGA,  " +
                    " CASE WHEN V.RUT_MED = -1 THEN US.RUT ELSE V.RUT_MED END RUT_MEDICO,   " +
                    " CASE WHEN V.RUT_MED = -1 THEN US.NOMBRE ELSE V.NOMB_MED END NOMBRE_MEDICO, P.FONO1_CONT FONO1, P.FONO2_CONT FONO2, (MAIL_CONT + '@' + MAIL2_CONT) CORREO, " +
                    "V.TipoAdq, V.NombAdq, V.RutAdq, V.FonoAdq, V.NombreSocial, V.OBS_FARMACIA, " +
                    "V.DIRECCION_PAC, V.OBSERVACION, V.DIAGNOSTICO, V.F_H_INGRESO, V.CODARTICULO, V.NOM_ARTICULO,  " +
                    "V.FRECUENCIA, V.DESC_UN_MED, V.CALCULAR, V.CANTIDAD, V.DURACION, V.RANGO, V.RANGO_DISP,  " +
                    "V.POSOLOGIA_DISP, V.POSOLOGIA,  V.PENDIENTE, CASE WHEN (V.POSOLOGIA -  V.PENDIENTE) < 0 THEN 0 ELSE (V.POSOLOGIA -  V.PENDIENTE) END SALDO_RECETA," +
                    " V.DVIA, V.DRANGO, V.DPRESENT, V.DPERIODO, V.OBSERVACIONES, ISNULL(COM.DESCRIPCION,'SC') COMUNA,  " +
                    "V.UNI_MIN, V.OBS_FARM, V.CANT_ENTREGA, V.FDESPACHO, V.FULT_DESP, V.SALDO, V.OBS, V.cant_desp_req,  " +
                    "V.cant_desp_2, V.IDPRESENTACION, V.IDRANGO, V.IDVIA, V.IDPERIODO, V.IDARTICULO, V.IDBODPRERIF,  " +
                    "V.IDESTADO, V.IDARTRECETA, V.IDREGION, V.IDCOMUNA, V.DIRECCION, V.IDREGION_PAC, V.IDCOMUNA_PAC,  " +
                    "V.IDDIAGNOSTICO, V.CODUNIOP, V.IDSERVICIO, V.IDSUBSERVICIO, V.IDCAMA, " +
                    "(SELECT CASE WHEN COUNT(1) > 0 THEN 'SI' ELSE 'NO' END      " +
                    " FROM M_RECETA R  " +
                    " INNER JOIN M_ART_RECETA AR ON AR.IDRECETA = R.IDRECETA   " +
                    " WHERE R.IDESTADO = 4  " +
                    " AND R.RUT =  P.RUT " +
                    " AND AR.IDARTICULO =  V.IDARTICULO  " +
                    " AND(DATEADD(month, -6, '" + ls_f_d + "') <= AR.FDESPACHO  AND AR.FDESPACHO <= DATEADD(month, 12, '" + ls_f_d + "'))) VIGENTE, " +
                    "(SELECT CASE WHEN COUNT(1) > 0 THEN 'SI' ELSE 'NO' END      " +
                    " FROM M_RECETA R  " +
                    " INNER JOIN M_ART_RECETA AR ON AR.IDRECETA = R.IDRECETA AND AR.CANT_PEND > 0 " +
                    " WHERE R.IDESTADO = 13  " +
                    " AND R.RUT =  P.RUT  " +
                    " AND AR.IDARTICULO =  V.IDARTICULO  " +
                    " AND(DATEADD(month, -6, '" + ls_f_d + "') <= AR.FDESPACHO  AND AR.FDESPACHO <= DATEADD(month, 12, '" + ls_f_d + "'))) DPENDIENTES " +
                    "from[dbo].[V_RECETA_DETALLE] V " +
                    " INNER JOIN M_USUARIOS US ON US.IDUSUARIO = V.IDUSUARIO  " +
                    " INNER JOIN M_PACIENTE P ON P.RUT = V.RUT AND ISNULL(P.ISPROGRAMA,0) <> 0  " +
                    "LEFT OUTER JOIN TG_COMUNA COM ON COM.IDCOMUNA = P.IDCOMUNA " +
                    " INNER JOIN M_UNIDAD_OPERATIVA UN ON UN.CODUNIOP = V.CODUNIOP ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }
    public DataSet mfrptDespPaciente()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " where b.IDRECETA > 0 AND ( b.idestado <> 3 ) ";
        string lsOrder = " order by b.FDESPACHO desc ";
        string lsGroup = " ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " and  ( convert( Datetime,b.FDESPACHO,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,b.FDESPACHO,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }

        // Si se ingresó Rut la agrega al Where.
        if (ls_Codigo != "")
            lsWhere += " AND ( b.CODARTICULO = '" + ls_Codigo + "' ) ";


        // Si corresponde Rut Med.
        if (ls_Art != "")
            lsWhere += " and b.NOMB_ART like '%" + ls_Art + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_Rut_M != "")
            lsWhere += " AND ( b.RUT_MED_Q = " + ls_Rut_M + " ) ";


        // Si corresponde Rut Med.
        if (ls_Nomb_M != "")
            lsWhere += " and b.MEDICO like '%" + ls_Nomb_M + "%' ";

        // Si se ingresó Rut la agrega al Where.
        if (ls_rut != "")
            lsWhere += " AND ( b.RUT_PAC = " + ls_rut + " ) ";



        // Si corresponde agrega bodega.
        if (lb_bod != "0")
            lsWhere += " AND ( b.IDBODPRERIF = " + lb_bod + " ) ";


        // Si se ingresó Servicio la agrega al Where.
        if (ls_IdServ != "0")
            lsWhere += " AND ( b.CODUNIOP = " + ls_IdServ + " ) ";




        lsSelect = " select FOLIO, BODEGA, UNIDAD,  " +
                        "CODARTICULO, NOMB_ART, FRECUENCIA,  " +
                        "FDESPACHO, CANTIDAD, DIAS, FNEXTDESPACHO, CANT_PEND " +
                        "from V_DESP_PACIENTE b ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptEstadHora()
    {
        try
        {
            con = bd.fnGetConn();
            //Indico el SP que voy a utilizar
            SqlCommand command = new SqlCommand("pu_rpt_estadistica_dia", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //Envió los parámetros que necesito
            SqlParameter paramb = new SqlParameter("@desde", SqlDbType.Char);
            paramb.Value = ls_f_d;
            command.Parameters.Add(paramb);
            SqlParameter paramd = new SqlParameter("@hasta", SqlDbType.Char);
            paramd.Value = ls_f_h;
            command.Parameters.Add(paramd);
            SqlParameter paramh = new SqlParameter("@bodega", SqlDbType.Int);
            paramh.Value = lb_bod;
            command.Parameters.Add(paramh);
            SqlParameter paramp = new SqlParameter("@idusr", SqlDbType.Int);
            paramp.Value = ls_idusr;
            command.Parameters.Add(paramp);

            command.CommandTimeout = 1200;
            DataSet dt = new DataSet();
            //Aquí ejecuto el SP y lo lleno en el DataTable
            adapter.Fill(dt);
            con.Close();
            ////Enlazo mis datos obtenidos en el DataTable con el grid
            //dataGridView1.DataSource = dt;
            return dt;
        }
        catch (Exception ecr)
        {
            return null;
        }
    }

    public DataSet mfrptEstadoRecetaRendimiento()
    {
        try
        {
            con = bd.fnGetConn();
            //Indico el SP que voy a utilizar
            SqlCommand command = new SqlCommand("pu_rpt_estado_receta_hora", con);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //Envió los parámetros que necesito
            SqlParameter paramb = new SqlParameter("@desde", SqlDbType.Char);
            paramb.Value = ls_f_d;
            command.Parameters.Add(paramb);
            SqlParameter paramd = new SqlParameter("@hasta", SqlDbType.Char);
            paramd.Value = ls_f_h;
            command.Parameters.Add(paramd);
            SqlParameter paramh = new SqlParameter("@bodega", SqlDbType.Int);
            paramh.Value = lb_bod;
            command.Parameters.Add(paramh);
            SqlParameter paramp = new SqlParameter("@idusr", SqlDbType.Int);
            paramp.Value = ls_idusr;
            command.Parameters.Add(paramp);

            command.CommandTimeout = 1200;
            DataSet dt = new DataSet();
            //Aquí ejecuto el SP y lo lleno en el DataTable
            adapter.Fill(dt);
            con.Close();
            ////Enlazo mis datos obtenidos en el DataTable con el grid
            //dataGridView1.DataSource = dt;
            return dt;
        }
        catch (Exception ecr)
        {
            return null;
        }
    }

    public DataSet mfrptEstadisRendimiento()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " ";
        string lsOrder = " order by U.RUT desc ";
        string lsGroup = " GROUP BY U.RUT, U.IDUSUARIO, C.IDBODPRERIF, U.NOMBRE    ";




        lsSelect = " SELECT DISTINCT U.RUT, U.NOMBRE, dbo.fn_Estadisticas_Mov( U.IDUSUARIO, 1, '" + ls_f_d + "', '" + ls_f_h + "' , C.IDBODPRERIF) VALIDA, " +
                   " dbo.fn_Estadisticas_Mov(U.IDUSUARIO, 2, '" + ls_f_d + "', '" + ls_f_h + "', C.IDBODPRERIF) PREPARA, " +
                   " dbo.fn_Estadisticas_Mov(U.IDUSUARIO, 3, '" + ls_f_d + "', '" + ls_f_h + "', C.IDBODPRERIF) PARA_DESPACHO, " +
                   " dbo.fn_Estadisticas_Mov(U.IDUSUARIO, 4, '" + ls_f_d + "', '" + ls_f_h + "', C.IDBODPRERIF) LLAMA, " +
                   " dbo.fn_Estadisticas_Mov(U.IDUSUARIO, 5, '" + ls_f_d + "', '" + ls_f_h + "', C.IDBODPRERIF) PENDIENTE, " +
                   " dbo.fn_Estadisticas_Mov(U.IDUSUARIO, 6, '" + ls_f_d + "', '" + ls_f_h + "', C.IDBODPRERIF) DISPENSA " +
                   " FROM M_USUARIOS U " +
                   " INNER JOIN " + modConstantes.gsDbRH + "M_USUARIOS U1 ON U.RUT = U1.RUT  AND U1.IDESTADO <> 3  " +
                   " INNER JOIN " + modConstantes.gsDbPer + "M_USR_BOD C ON C.IDUSUARIO = U1.IDUSUARIO AND C.IDESTADO <> 3 " +
                   " WHERE U.RUT = U1.RUT " +
                   " AND U.IDESTADO <> 3 " +
                   " AND C.IDBODPRERIF = " + lb_bod + " ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptGastosPoli()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " ";
        string lsOrder = " order by B.BODEGA, B.UNIDAD desc ";
        string lsGroup = " GROUP BY BODEGA, UNIDAD ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {

            //lsWhere += " and  ( b.FDESPACHO >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
            //                "   b.FDESPACHO <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            lsWhere += " WHERE  ( convert( Datetime,b.FDESPACHO,103) >= convert( Datetime, '" + ls_f_d + " 00:00:00',103)  and " +
                            "   convert( Datetime,b.FDESPACHO,103) <= convert( Datetime, '" + ls_f_h + " 23:59:00',103) ) ";

        }


        // Si corresponde agrega bodega.
        if (lb_bod != "0")
            lsWhere += " AND ( b.IDBODPRERIF = " + lb_bod + " ) ";




        lsSelect = " SELECT BODEGA, UNIDAD, CONVERT(DECIMAL(20,0),SUM(CONSUMO)) CONSUMO " +
                   " FROM V_CONSUMO_POLI b ";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }

    public DataSet mfrptEstadisticaMesAnio()
    {
        DataSet lsCodigo;
        string lsSql = "";
        string lsSelect = "";
        string lsWhere = " ";
        string lsOrder = " order by ORDEN_TIPO , TIPO , ORDEN_UNIDAD asc  ";
        string lsGroup = " ";

        lsWhere += " WHERE  IDBODPRERIF = " + lb_bod + " AND ANIO = " + lb_anio + " ";




        lsSelect = " SELECT TIPO, UNIDAD, " +
                    "CRONICO_ENERO, MORBIDO_ENERO,TOTAL_ENERO, " +
                    "CRONICO_FEBRERO, MORBIDO_FEBRERO,TOTAL_FEBRERO, " +
                    "CRONICO_MARZO, MORBIDO_MARZO,TOTAL_MARZO, " +
                    "CRONICO_ABRIL, MORBIDO_ABRIL,TOTAL_ABRIL, " +
                    "CRONICO_MAYO, MORBIDO_MAYO,TOTAL_MAYO, " +
                    "CRONICO_JUNIO, MORBIDO_JUNIO,TOTAL_JUNIO, " +
                    "CRONICO_JULIO, MORBIDO_JULIO,TOTAL_JULIO, " +
                    "CRONICO_AGOST, MORBIDO_AGOST,TOTAL_AGOST, " +
                    "CRONICO_SEPT, MORBIDO_SEPT,TOTAL_SEPT, " +
                    "CRONICO_OCT, MORBIDO_OCT,TOTAL_OCT,  " +
                    "CRONICO_NOV, MORBIDO_NOV,TOTAL_NOV, " +
                    "CRONICO_DIC, MORBIDO_DIC, TOTAL_DIC " +
                    "FROM V_AUDITORIA_GRAL_1";

        lsSql = lsSelect + lsWhere + lsGroup + lsOrder;

        con = bd.fnGetConn();
        lsCodigo = bd.Fill(con, lsSql);
        con.Close();


        return lsCodigo;
    }
    #endregion

    #region Micro
    public DataSet mfrptDepRec(bool esExportacion = false)
    {
        ls_error_api = "";
        DataSet ds = crearDataSetDespRec();

        try
        {
            string urlBase = ConfigurationManager.AppSettings["ApiReportesUrl"];
            if (string.IsNullOrWhiteSpace(urlBase))
            {
                ls_error_api = "No existe configuracion de API para reportes.";
                return ds;
            }

            string urlEndpoint = urlBase.TrimEnd('/') + "/despacho-receta";
            string query = construirQueryDespRec(esExportacion);
            string url = urlEndpoint + (string.IsNullOrWhiteSpace(query) ? "" : "?" + query);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "application/json";
            req.Timeout = obtenerTimeoutApi();
            req.ReadWriteTimeout = req.Timeout;

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string json = sr.ReadToEnd();
                cargarJsonDespRec(ds.Tables[0], json);
            }
        }
        catch (WebException wex)
        {
            ls_error_api = obtenerMensajeErrorApi(wex);
        }
        catch (Exception ex)
        {
            ls_error_api = "Error consultando API de despacho receta: " + ex.Message;
        }

        return ds;
    }

    private DataSet crearDataSetDespRec()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        string[] columnas = {
            "FOLIO", "NUM_REC_MANUAL", "NUMOSAL", "BODEGA", "ESTADO", "DIAGNOSTICO",
            "MEDICO", "RUT_MED", "UNIDAD", "CODARTICULO", "NOMB_ART", "FRECUENCIA",
            "VIA", "OBS_MED", "FDESPACHO", "CANTIDAD", "DIAS", "FNEXTDESPACHO",
            "CANT_PEND", "OBSFARM", "RUT", "NOMBRE", "APELL_PAT", "APELL_MAT",
            "FONO1_CONT", "OBS1_CONT", "FONO2_CONT", "OBS2_CONT", "MAIL", "DIRECCION",
            "COMUNA", "REGION", "NombAdq", "RutAdq", "FonoAdq"
        };

        foreach (string col in columnas)
        {
            dt.Columns.Add(col, typeof(string));
        }

        ds.Tables.Add(dt);
        return ds;
    }

    private string construirQueryDespRec(bool incluirVFull = false)
    {
        List<string> query = new List<string>();

        agregarParametro(query, "fecha_desde", formatearFechaApi(ls_f_d));
        agregarParametro(query, "fecha_hasta", formatearFechaApi(ls_f_h));
        agregarParametro(query, "rut_paciente", ls_rut);
        agregarParametro(query, "rut_medico", ls_Rut_M);
        agregarParametro(query, "nom_medico", ls_Nomb_M);
        agregarParametro(query, "cod_articulo", ls_Codigo);
        agregarParametro(query, "desc_articulo", ls_Art);
        agregarParametro(query, "nombre_paciente", lsNom);

        if (lb_bod != "0") agregarParametro(query, "id_bodega", lb_bod);
        if (ls_IdServ != "0") agregarParametro(query, "id_servicio", ls_IdServ);

        if (incluirVFull) agregarParametro(query, "v_full", "true");

        return string.Join("&", query.ToArray());
    }

    private string formatearFechaApi(string fecha)
    {
        if (string.IsNullOrWhiteSpace(fecha)) return "";

        DateTime dt;
        string[] formatos = new string[] { "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "yyyy/MM/dd", "dd-MM-yyyy" };
        if (DateTime.TryParseExact(fecha.Trim(), formatos, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
            return dt.ToString("yyyy-MM-dd");

        if (DateTime.TryParse(fecha, out dt))
            return dt.ToString("yyyy-MM-dd");

        return fecha.Trim();
    }

    private void agregarParametro(List<string> query, string nombre, string valor)
    {
        if (string.IsNullOrWhiteSpace(valor)) return;
        query.Add(HttpUtility.UrlEncode(nombre) + "=" + HttpUtility.UrlEncode(valor.Trim()));
    }
    private void cargarJsonDespRec(DataTable tabla, string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return;

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = int.MaxValue;

        try
        {
            object data = js.DeserializeObject(json);
            System.Collections.IEnumerable filas = null;

            Dictionary<string, object> respuesta = data as Dictionary<string, object>;
            if (respuesta != null && respuesta.ContainsKey("muestra"))
            {
                filas = respuesta["muestra"] as System.Collections.IEnumerable;
                if (respuesta.ContainsKey("total"))
                {
                    int totalInt = 0;
                    if (int.TryParse(respuesta["total"].ToString(), out totalInt))
                    {
                        li_total_despacho_receta = totalInt;
                    }
                }
            }
            else if (data is System.Collections.IEnumerable && !(data is string))
            {
                filas = data as System.Collections.IEnumerable;
                li_total_despacho_receta = 0;
            }

            if (filas == null) return;

            string[] columnas = {
                "FOLIO", "NUM_REC_MANUAL", "NUMOSAL", "BODEGA", "ESTADO", "DIAGNOSTICO",
                "MEDICO", "RUT_MED", "UNIDAD", "CODARTICULO", "NOMB_ART", "FRECUENCIA",
                "VIA", "OBS_MED", "FDESPACHO", "CANTIDAD", "DIAS", "FNEXTDESPACHO",
                "CANT_PEND", "OBSFARM", "RUT", "NOMBRE", "APELL_PAT", "APELL_MAT",
                "FONO1_CONT", "OBS1_CONT", "FONO2_CONT", "OBS2_CONT", "MAIL", "DIRECCION",
                "COMUNA", "REGION", "NombAdq", "RutAdq", "FonoAdq"
            };

            foreach (object fila in filas)
            {
                Dictionary<string, object> row = fila as Dictionary<string, object>;
                if (row == null) continue;

                DataRow dr = tabla.NewRow();
                foreach (string col in columnas)
                {
                    string apiKey = "";
                    switch (col)
                    {
                        case "FOLIO": apiKey = "folio"; break;
                        case "NUM_REC_MANUAL": apiKey = "numRecManual"; break;
                        case "NUMOSAL": apiKey = "numOsal"; break;
                        case "BODEGA": apiKey = "bodega"; break;
                        case "ESTADO": apiKey = "estado"; break;
                        case "DIAGNOSTICO": apiKey = "diagnostico"; break;
                        case "MEDICO": apiKey = "medico"; break;
                        case "RUT_MED": apiKey = "rutMed"; break;
                        case "UNIDAD": apiKey = "unidad"; break;
                        case "CODARTICULO": apiKey = "codArticulo"; break;
                        case "NOMB_ART": apiKey = "nombArt"; break;
                        case "FRECUENCIA": apiKey = "frecuencia"; break;
                        case "VIA": apiKey = "via"; break;
                        case "OBS_MED": apiKey = "obsMed"; break;
                        case "FDESPACHO": apiKey = "fDespacho"; break;
                        case "CANTIDAD": apiKey = "cantidad"; break;
                        case "DIAS": apiKey = "dias"; break;
                        case "FNEXTDESPACHO": apiKey = "fNextDespacho"; break;
                        case "CANT_PEND": apiKey = "cantPend"; break;
                        case "OBSFARM": apiKey = "obsFarm"; break;
                        case "RUT": apiKey = "rut"; break;
                        case "NOMBRE": apiKey = "nombre"; break;
                        case "APELL_PAT": apiKey = "apellPat"; break;
                        case "APELL_MAT": apiKey = "apellMat"; break;
                        case "FONO1_CONT": apiKey = "fono1Cont"; break;
                        case "OBS1_CONT": apiKey = "obs1Cont"; break;
                        case "FONO2_CONT": apiKey = "fono2Cont"; break;
                        case "OBS2_CONT": apiKey = "obs2Cont"; break;
                        case "MAIL": apiKey = "mail"; break;
                        case "DIRECCION": apiKey = "direccion"; break;
                        case "COMUNA": apiKey = "comuna"; break;
                        case "REGION": apiKey = "region"; break;
                        case "NombAdq": apiKey = "nombAdq"; break;
                        case "RutAdq": apiKey = "rutAdq"; break;
                        case "FonoAdq": apiKey = "fonoAdq"; break;
                    }

                    dr[col] = valorJson(row, apiKey);
                }
                tabla.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error en cargarJsonDespRec: " + ex.Message);
        }
    }

    private int obtenerTimeoutApi()
    {
        int timeout;
        if (int.TryParse(ConfigurationManager.AppSettings["ApiReportesTimeout"], out timeout) && timeout > 0)
            return timeout;

        if (int.TryParse(ConfigurationManager.AppSettings["ApiRecetaTimeout"], out timeout) && timeout > 0)
            return timeout;

        return 30000;
    }

    private string valorJson(Dictionary<string, object> row, string key)
    {
        // Búsqueda exacta primero (más rápida)
        object valor;
        if (row.TryGetValue(key, out valor) && valor != null) return valor.ToString();

        // Fallback case-insensitive (por si la API cambia la capitalización)
        foreach (var kvp in row)
        {
            if (string.Equals(kvp.Key, key, StringComparison.OrdinalIgnoreCase) && kvp.Value != null)
                return kvp.Value.ToString();
        }
        return "";
    }

    private string obtenerMensajeErrorApi(WebException wex)
    {
        try
        {
            HttpWebResponse response = wex.Response as HttpWebResponse;
            if (response == null)
                return "No fue posible conectar con el servicio de reportes.";

            string body = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                body = sr.ReadToEnd();
            }

            if ((int)response.StatusCode == 400)
            {
                string detalle = extraerErrorDesdeJson(body);
                return string.IsNullOrWhiteSpace(detalle)
                    ? "Solicitud invalida al consultar reportes."
                    : detalle;
            }

            if ((int)response.StatusCode >= 500)
                return "El servicio de reportes respondio con un error interno.";

            return "Error al consultar servicio de reportes (HTTP " + (int)response.StatusCode + ").";
        }
        catch
        {
            return "Error al consultar el servicio de reportes.";
        }
    }

    private string extraerErrorDesdeJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return "";

        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Dictionary<string, object> data = js.DeserializeObject(json) as Dictionary<string, object>;
            if (data == null || !data.ContainsKey("error") || data["error"] == null) return "";
            return data["error"].ToString();
        }
        catch
        {
            return "";
        }
    }

    // Rec Dispensadas

    public DataSet mfrptPrecritasRec(bool esExportacion = false)
    {
        ls_error_api = "";
        DataSet ds = crearDataSetPrecritasRec();

        try
        {
            string urlBase = ConfigurationManager.AppSettings["ApiReportesUrl"];
            if (string.IsNullOrWhiteSpace(urlBase))
            {
                ls_error_api = "No existe configuracion de API para reportes.";
                return ds;
            }

            string urlEndpoint = urlBase.TrimEnd('/') + "/recetas-prescritas";
            string query = construirQueryPrecritasRec(esExportacion);
            string url = urlEndpoint + (string.IsNullOrWhiteSpace(query) ? "" : "?" + query);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "application/json";
            req.Timeout = obtenerTimeoutApi();
            req.ReadWriteTimeout = req.Timeout;

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                // Verificar Content-Length si está disponible
                const long maxBytes = 30L * 1024 * 1024; // 30 MB
                if (resp.ContentLength > maxBytes)
                {
                    ls_error_api = "El resultado contiene demasiados datos ("
                        + (resp.ContentLength / 1024 / 1024) + " MB). "
                        + "Reduzca el rango de fechas o agregue filtros adicionales.";
                    return ds;
                }

                // Leer en bloques para detectar respuestas gigantes antes de cargar toda la memoria
                char[] buffer = new char[65536];
                int read;
                var sb = new System.Text.StringBuilder();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    while ((read = sr.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sb.Append(buffer, 0, read);
                        if (sb.Length > maxBytes)
                        {
                            ls_error_api = "El resultado contiene demasiados datos (más de 30 MB). "
                                + "Reduzca el rango de fechas o agregue filtros adicionales.";
                            return ds;
                        }
                    }
                }
                cargarJsonPrecritasRec(ds.Tables[0], sb.ToString());
            }
        }
        catch (OutOfMemoryException)
        {
            ls_error_api = "Memoria insuficiente para procesar el resultado. "
                + "Reduzca el rango de fechas o agregue más filtros (médico, paciente, artículo).";
        }
        catch (WebException wex)
        {
            ls_error_api = obtenerMensajeErrorApi(wex);
        }
        catch (Exception ex)
        {
            ls_error_api = "Error consultando API de recetas prescritas: " + ex.Message;
        }

        return ds;
    }

    private DataSet crearDataSetPrecritasRec()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        string[] columnas = {
            "FOLIO", "NUM_REC_MANUAL", "F_H_CREACION", "NOMBRE", "RUT", "DV", "ESTADO", "APELL_PAT", "APELL_MAT",
            "UNIDAD", "RUT_MEDICO", "NOMBRE_MEDICO", "TipoAdq", "NombAdq", "RutAdq", "FonoAdq", "NombreSocial", "OBS_FARMACIA",
            "DIRECCION_PAC", "OBSERVACION", "DIAGNOSTICO", "F_H_INGRESO", "CODARTICULO", "NOM_ARTICULO",
            "FRECUENCIA", "DESC_UN_MED", "CALCULAR", "CANTIDAD", "DURACION", "RANGO", "RANGO_DISP",
            "POSOLOGIA_DISP", "POSOLOGIA", "PENDIENTE", "SALDO_RECETA", "DVIA", "DRANGO", "DPRESENT", "DPERIODO",
            "OBSERVACIONES", "UNI_MIN", "OBS_FARM", "CANT_ENTREGA", "FDESPACHO", "FULT_DESP", "SALDO", "OBS", "cant_desp_req",
            "cant_desp_2", "IDPRESENTACION", "IDRANGO", "IDVIA", "IDPERIODO", "IDARTICULO", "IDBODPRERIF",
            "IDESTADO", "IDARTRECETA", "IDREGION", "IDCOMUNA", "DIRECCION", "IDREGION_PAC", "IDCOMUNA_PAC",
            "IDDIAGNOSTICO", "CODUNIOP", "IDSERVICIO", "IDSUBSERVICIO", "IDCAMA"
        };

        foreach (string col in columnas)
        {
            dt.Columns.Add(col, typeof(string));
        }

        ds.Tables.Add(dt);
        return ds;
    }

    private string construirQueryPrecritasRec(bool incluirVFull = false)
    {
        List<string> query = new List<string>();

        agregarParametro(query, "fecha_desde", formatearFechaApi(ls_f_d));
        agregarParametro(query, "fecha_hasta", formatearFechaApi(ls_f_h));
        agregarParametro(query, "rut_paciente", ls_rut);
        agregarParametro(query, "rut_medico", ls_Rut_M);
        agregarParametro(query, "nom_medico", ls_Nomb_M);
        agregarParametro(query, "cod_articulo", ls_Codigo);
        agregarParametro(query, "desc_articulo", ls_Art);
        agregarParametro(query, "nombre_paciente", lsNom);

        if (ls_IdServ != "0") agregarParametro(query, "id_servicio", ls_IdServ);

        if (incluirVFull) agregarParametro(query, "v_full", "true");

        return string.Join("&", query.ToArray());
    }

    private void cargarJsonPrecritasRec(DataTable tabla, string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return;

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = int.MaxValue;

        try
        {
            object data = js.DeserializeObject(json);
            System.Collections.IEnumerable filas = null;

            Dictionary<string, object> respuesta = data as Dictionary<string, object>;
            if (respuesta != null && respuesta.ContainsKey("muestra"))
            {
                filas = respuesta["muestra"] as System.Collections.IEnumerable;
                if (respuesta.ContainsKey("total"))
                {
                    int totalInt = 0;
                    if (int.TryParse(respuesta["total"].ToString(), out totalInt))
                    {
                        li_total_precritas_receta = totalInt;
                    }
                }
            }
            else if (data is System.Collections.IEnumerable && !(data is string))
            {
                filas = data as System.Collections.IEnumerable;
                li_total_precritas_receta = 0;
            }

            if (filas == null) return;

            foreach (object fila in filas)
            {
                Dictionary<string, object> row = fila as Dictionary<string, object>;
                if (row == null) continue;

                DataRow dr = tabla.NewRow();
                // Mapeo exhaustivo de campos de la API (camelCase) a la Tabla (PascalCase/Legacy)
                dr["FOLIO"] = valorJson(row, "folio");
                dr["NUM_REC_MANUAL"] = valorJson(row, "numRecManual");
                dr["F_H_CREACION"] = valorJson(row, "fhCreacion");
                dr["NOMBRE"] = valorJson(row, "nombre");
                dr["RUT"] = valorJson(row, "rut");
                dr["DV"] = valorJson(row, "dv");
                dr["ESTADO"] = valorJson(row, "estado");
                dr["APELL_PAT"] = valorJson(row, "apellPat");
                dr["APELL_MAT"] = valorJson(row, "apellMat");
                dr["UNIDAD"] = valorJson(row, "unidad");
                dr["RUT_MEDICO"] = valorJson(row, "rutMedFmt"); // Usamos el formateado del API
                dr["NOMBRE_MEDICO"] = valorJson(row, "medico"); // Nombre resuelto por API
                dr["TipoAdq"] = valorJson(row, "tipoAdq");
                dr["NombAdq"] = valorJson(row, "nombAdq");
                dr["RutAdq"] = valorJson(row, "rutAdq");
                dr["FonoAdq"] = valorJson(row, "fonoAdq");
                dr["NombreSocial"] = valorJson(row, "nombreSocial");
                dr["OBS_FARMACIA"] = valorJson(row, "obsFarmacia");
                dr["DIRECCION_PAC"] = valorJson(row, "direccionPac");
                dr["OBSERVACION"] = valorJson(row, "observacion");
                dr["DIAGNOSTICO"] = valorJson(row, "diagnostico");
                dr["F_H_INGRESO"] = valorJson(row, "fhIngreso");
                dr["CODARTICULO"] = valorJson(row, "codArticulo");
                dr["NOM_ARTICULO"] = valorJson(row, "nomArticulo");
                dr["FRECUENCIA"] = valorJson(row, "frecuencia");
                dr["DESC_UN_MED"] = valorJson(row, "descUnMed");
                dr["CALCULAR"] = valorJson(row, "calcular");
                dr["CANTIDAD"] = valorJson(row, "cantidad");
                dr["DURACION"] = valorJson(row, "duracion");
                dr["RANGO"] = valorJson(row, "rango");
                dr["RANGO_DISP"] = valorJson(row, "rangoDisp");
                dr["POSOLOGIA_DISP"] = valorJson(row, "posologiaDisp");
                dr["POSOLOGIA"] = valorJson(row, "posologia");
                dr["PENDIENTE"] = valorJson(row, "pendiente");

                // Saldo receta calculado en API o local
                double poso = 0, pend = 0;
                double.TryParse(dr["POSOLOGIA"].ToString(), out poso);
                double.TryParse(dr["PENDIENTE"].ToString(), out pend);
                dr["SALDO_RECETA"] = (poso - pend < 0) ? "0" : (poso - pend).ToString();

                dr["DVIA"] = valorJson(row, "dVia");
                dr["DRANGO"] = valorJson(row, "dRango");
                dr["DPRESENT"] = valorJson(row, "dPresent");
                dr["DPERIODO"] = valorJson(row, "dPeriodo");
                dr["OBSERVACIONES"] = valorJson(row, "observaciones");
                dr["UNI_MIN"] = valorJson(row, "uniMin");
                dr["OBS_FARM"] = valorJson(row, "obsFarm");
                dr["CANT_ENTREGA"] = valorJson(row, "cantEntrega");
                dr["FDESPACHO"] = valorJson(row, "fdespacho");
                dr["FULT_DESP"] = valorJson(row, "fultDesp");
                dr["SALDO"] = valorJson(row, "saldo");
                dr["OBS"] = valorJson(row, "obs");
                dr["cant_desp_req"] = valorJson(row, "cantDespReq");
                dr["cant_desp_2"] = valorJson(row, "cantDesp2");
                dr["IDPRESENTACION"] = valorJson(row, "idPresentacion");
                dr["IDRANGO"] = valorJson(row, "idRango");
                dr["IDVIA"] = valorJson(row, "idVia");
                dr["IDPERIODO"] = valorJson(row, "idPeriodo");
                dr["IDARTICULO"] = valorJson(row, "idArticulo");
                dr["IDBODPRERIF"] = valorJson(row, "idBodPrerif");
                dr["IDESTADO"] = valorJson(row, "idEstado");
                dr["IDARTRECETA"] = valorJson(row, "idArtReceta");
                dr["IDREGION"] = valorJson(row, "idRegion");
                dr["IDCOMUNA"] = valorJson(row, "idComuna");
                dr["DIRECCION"] = valorJson(row, "direccion");
                dr["IDREGION_PAC"] = valorJson(row, "idRegionPac");
                dr["IDCOMUNA_PAC"] = valorJson(row, "idComunaPac");
                dr["IDDIAGNOSTICO"] = valorJson(row, "idDiagnostico");
                dr["CODUNIOP"] = valorJson(row, "codUniOp");
                dr["IDSERVICIO"] = valorJson(row, "idServicio");
                dr["IDSUBSERVICIO"] = valorJson(row, "idSubServicio");
                dr["IDCAMA"] = valorJson(row, "idCama");

                tabla.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error en cargarJsonPrecritasRec: " + ex.Message);
        }
    }

    // despacho pac

    public DataSet mfrptDespPaciente(bool esExportacion = false)
    {
        ls_error_api = "";
        DataSet ds = crearDataSetDespPaciente();

        try
        {
            string urlBase = ConfigurationManager.AppSettings["ApiReportesDespachoPacienteUrl"];
            if (string.IsNullOrWhiteSpace(urlBase))
            {
                string apiReportes = ConfigurationManager.AppSettings["ApiReportesUrl"];
                if (!string.IsNullOrWhiteSpace(apiReportes))
                {
                    urlBase = apiReportes.TrimEnd('/') + "/despacho-paciente";
                }
            }

            if (string.IsNullOrWhiteSpace(urlBase))
            {
                ls_error_api = "No existe configuracion de API para despacho paciente.";
                return ds;
            }

            string query = construirQueryDespPaciente(esExportacion);
            string url = urlBase + (string.IsNullOrWhiteSpace(query) ? "" : "?" + query);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "application/json";
            req.Timeout = obtenerTimeoutApi();
            req.ReadWriteTimeout = req.Timeout;

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                string json = sr.ReadToEnd();
                // Debug: Log JSON recibido (primeros 500 chars para no saturar)
                string jsonPreview = json.Length > 500 ? json.Substring(0, 500) + "..." : json;
                System.Diagnostics.Debug.WriteLine("DEBUG mfrptDespPaciente - esExportacion: " + esExportacion);
                System.Diagnostics.Debug.WriteLine("DEBUG JSON response: " + jsonPreview);

                cargarJsonDespPaciente(ds.Tables[0], json, esExportacion);

                System.Diagnostics.Debug.WriteLine("DEBUG Rows loaded: " + ds.Tables[0].Rows.Count);
            }
        }
        catch (WebException wex)
        {
            ls_error_api = obtenerMensajeErrorApi(wex);
        }
        catch (Exception ex)
        {
            ls_error_api = "Error consultando API de despacho paciente: " + ex.Message;
        }

        return ds;
    }

    private void cargarJsonDespPaciente(DataTable tabla, string json, bool conVFull = false)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            System.Diagnostics.Debug.WriteLine("DEBUG cargarJsonDespPaciente - JSON vacío");
            return;
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = int.MaxValue;

        try
        {
            object data = js.DeserializeObject(json);
            System.Diagnostics.Debug.WriteLine("DEBUG cargarJsonDespPaciente - conVFull: " + conVFull);
            System.Diagnostics.Debug.WriteLine("DEBUG data type: " + (data != null ? data.GetType().Name : "null"));

            System.Collections.IEnumerable filas = null;

            // Primero intentamos ver si es una Dictionary con estructura {total, muestra}
            Dictionary<string, object> respuesta = data as Dictionary<string, object>;
            if (respuesta != null && respuesta.ContainsKey("muestra"))
            {
                System.Diagnostics.Debug.WriteLine("DEBUG Detectada estructura {total, muestra}");
                object muestraObj = respuesta["muestra"];
                System.Diagnostics.Debug.WriteLine("DEBUG muestra type: " + (muestraObj != null ? muestraObj.GetType().Name : "null"));
                filas = muestraObj as System.Collections.IEnumerable;

                // Capturar total para uso posterior
                if (respuesta.ContainsKey("total"))
                {
                    object totalObj = respuesta["total"];
                    int totalInt = 0;
                    if (int.TryParse(totalObj.ToString(), out totalInt))
                    {
                        li_total_despacho_paciente = totalInt;
                        System.Diagnostics.Debug.WriteLine("DEBUG Total registros: " + totalInt);
                    }
                }
            }
            else if (data is System.Collections.IEnumerable && !(data is string))
            {
                // Si no es Dictionary con muestra, asumimos que es un array directo
                System.Diagnostics.Debug.WriteLine("DEBUG Detectado array directo");
                filas = data as System.Collections.IEnumerable;
                li_total_despacho_paciente = 0; // No hay total en array directo
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("DEBUG Estructura JSON no reconocida. Tipo: " +
                    (data != null ? data.GetType().FullName : "null"));
            }

            if (filas == null)
            {
                System.Diagnostics.Debug.WriteLine("DEBUG filas es null, retornando");
                return;
            }

            int rowCount = 0;
            foreach (object fila in filas)
            {
                Dictionary<string, object> row = fila as Dictionary<string, object>;
                if (row == null)
                {
                    System.Diagnostics.Debug.WriteLine("DEBUG Fila no es Dictionary, tipo: " + fila.GetType().Name);
                    continue;
                }

                DataRow dr = tabla.NewRow();
                dr["UNIDAD"] = valorJson(row, "unidad");
                dr["BODEGA"] = valorJson(row, "bodega");
                dr["FOLIO"] = valorJson(row, "folio");
                dr["NOMB_ART"] = valorJson(row, "nombArt");
                dr["FRECUENCIA"] = valorJson(row, "frecuencia");
                dr["FDESPACHO"] = valorJson(row, "fDespacho");
                dr["DIAS"] = valorJson(row, "dias");
                dr["CANTIDAD"] = valorJson(row, "cantidad");
                dr["FNEXTDESPACHO"] = valorJson(row, "fNextDespacho");
                dr["CANT_PEND"] = valorJson(row, "cantPend");
                tabla.Rows.Add(dr);
                rowCount++;
            }

            System.Diagnostics.Debug.WriteLine("DEBUG Filas cargadas: " + rowCount);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("DEBUG Error en cargarJsonDespPaciente: " + ex.Message + " | " + ex.StackTrace);
        }
    }

    private string construirQueryDespPaciente(bool incluirVFull = false)
    {
        List<string> query = new List<string>();

        agregarParametro(query, "fecha_desde", formatearFechaApi(ls_f_d));
        agregarParametro(query, "fecha_hasta", formatearFechaApi(ls_f_h));
        agregarParametro(query, "rut_paciente", ls_rut);
        agregarParametro(query, "rut_medico", ls_Rut_M);
        agregarParametro(query, "nom_medico", ls_Nomb_M);
        agregarParametro(query, "cod_articulo", ls_Codigo);
        agregarParametro(query, "desc_articulo", ls_Art);

        if (lb_bod != "0") agregarParametro(query, "id_bodega", lb_bod);
        if (ls_IdServ != "0") agregarParametro(query, "id_servicio", ls_IdServ);

        if (incluirVFull) agregarParametro(query, "v_full", "true");

        return string.Join("&", query.ToArray());
    }

    private DataSet crearDataSetDespPaciente()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        dt.Columns.Add("UNIDAD", typeof(string));
        dt.Columns.Add("BODEGA", typeof(string));
        dt.Columns.Add("FOLIO", typeof(string));
        dt.Columns.Add("NOMB_ART", typeof(string));
        dt.Columns.Add("FRECUENCIA", typeof(string));
        dt.Columns.Add("FDESPACHO", typeof(string));
        dt.Columns.Add("DIAS", typeof(string));
        dt.Columns.Add("CANTIDAD", typeof(string));
        dt.Columns.Add("FNEXTDESPACHO", typeof(string));
        dt.Columns.Add("CANT_PEND", typeof(string));

        ds.Tables.Add(dt);
        return ds;
    }

    // Rec Pendientes

    public DataSet mfrptDespPend(bool esExportacion = false)
    {
        ls_error_api = "";
        DataSet ds = crearDataSetDespPend();

        try
        {
            string urlBase = ConfigurationManager.AppSettings["ApiReportesUrl"];
            if (string.IsNullOrWhiteSpace(urlBase))
            {
                ls_error_api = "No existe configuracion de API para reportes.";
                return ds;
            }

            string urlEndpoint = urlBase.TrimEnd('/') + "/despachos-pendientes";
            string query = construirQueryDespPend(esExportacion);
            string url = urlEndpoint + (string.IsNullOrWhiteSpace(query) ? "" : "?" + query);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.Accept = "application/json";
            req.Timeout = obtenerTimeoutApi();
            req.ReadWriteTimeout = req.Timeout;

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            using (StreamReader sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8))
            {
                string json = sr.ReadToEnd();
                cargarJsonDespPend(ds.Tables[0], json);
            }
        }
        catch (WebException wex)
        {
            ls_error_api = obtenerMensajeErrorApi(wex);
        }
        catch (Exception ex)
        {
            ls_error_api = "Error consultando API de despachos pendientes: " + ex.Message;
        }

        return ds;
    }

    private void cargarJsonDespPend(DataTable tabla, string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return;

        JavaScriptSerializer js = new JavaScriptSerializer();
        js.MaxJsonLength = int.MaxValue;

        try
        {
            object data = js.DeserializeObject(json);
            System.Collections.IEnumerable filas = null;

            Dictionary<string, object> respuesta = data as Dictionary<string, object>;
            if (respuesta != null && respuesta.ContainsKey("muestra"))
            {
                filas = respuesta["muestra"] as System.Collections.IEnumerable;
                if (respuesta.ContainsKey("total"))
                {
                    int totalInt = 0;
                    if (int.TryParse(respuesta["total"].ToString(), out totalInt))
                        li_total_despacho_receta = totalInt;
                }
            }
            else if (data is System.Collections.IEnumerable && !(data is string))
            {
                filas = data as System.Collections.IEnumerable;
                li_total_despacho_receta = 0;
            }

            if (filas == null) return;

            foreach (object fila in filas)
            {
                Dictionary<string, object> row = fila as Dictionary<string, object>;
                if (row == null) continue;

                DataRow dr = tabla.NewRow();
                dr["FOLIO"] = valorJson(row, "folio");
                dr["RUT_PAC"] = valorJson(row, "rutPac");
                dr["NOMBRE_PAC"] = valorJson(row, "nombrePac");
                dr["FRECUENCIA"] = valorJson(row, "frecuencia");
                dr["CODARTICULO"] = valorJson(row, "codArticulo");
                dr["DESCRIPCION_LARGA"] = valorJson(row, "descArticulo");
                dr["FECHA"] = valorJson(row, "fecha");
                dr["RANGO"] = valorJson(row, "rango");
                dr["RANGO_DISP"] = valorJson(row, "rangoDisp");
                dr["POSOLOGIA"] = valorJson(row, "posologia");
                dr["POSOLOGIA_DISP"] = valorJson(row, "posologiaDisp");
                dr["CANT_DESP"] = valorJson(row, "cantDesp");
                dr["CANT_PEND"] = valorJson(row, "cantPend");
                dr["ACUM_RANGO"] = valorJson(row, "acumRango");
                dr["FULT_DESP"] = valorJson(row, "fecha"); // fecha = fultDesp en API
                dr["MEDICO"] = valorJson(row, "medico");
                dr["RUT_MED"] = valorJson(row, "rutMed");
                dr["RUT_MED_FMT"] = valorJson(row, "rutMedFmt");
                dr["UNIDAD"] = valorJson(row, "unidad");
                dr["COD_UNIOP"] = valorJson(row, "codUniOp");
                dr["ID_BOD_PRERIF"] = valorJson(row, "idBodPrerif");
                dr["ID_ESTADO"] = valorJson(row, "idEstado");
                tabla.Rows.Add(dr);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error en cargarJsonDespPend: " + ex.Message);
        }
    }

    private string construirQueryDespPend(bool incluirVFull = false)
    {
        List<string> query = new List<string>();

        agregarParametro(query, "fecha_desde", formatearFechaApi(ls_f_d));
        agregarParametro(query, "fecha_hasta", formatearFechaApi(ls_f_h));
        agregarParametro(query, "rut_paciente", ls_rut);
        agregarParametro(query, "nombre_paciente", lsNom);
        agregarParametro(query, "rut_medico", ls_Rut_M);
        agregarParametro(query, "nom_medico", ls_Nomb_M);
        agregarParametro(query, "cod_articulo", ls_Codigo);
        agregarParametro(query, "desc_articulo", ls_Art);

        if (lb_bod != null && lb_bod != "0") agregarParametro(query, "id_bodega", lb_bod);
        if (ls_IdServ != null && ls_IdServ != "0") agregarParametro(query, "id_servicio", ls_IdServ);

        if (incluirVFull) agregarParametro(query, "v_full", "true");

        return string.Join("&", query.ToArray());
    }

    private DataSet crearDataSetDespPend()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        string[] columnas = {
            "FOLIO", "RUT_PAC", "NOMBRE_PAC", "FRECUENCIA", "CODARTICULO", "DESCRIPCION_LARGA",
            "FECHA", "RANGO", "RANGO_DISP", "POSOLOGIA", "POSOLOGIA_DISP",
            "CANT_DESP", "CANT_PEND", "ACUM_RANGO", "FULT_DESP",
            "MEDICO", "RUT_MED", "RUT_MED_FMT", "UNIDAD", "COD_UNIOP",
            "ID_BOD_PRERIF", "ID_ESTADO"
        };

        foreach (string col in columnas)
            dt.Columns.Add(col, typeof(string));

        ds.Tables.Add(dt);
        return ds;
    }
    #endregion
}