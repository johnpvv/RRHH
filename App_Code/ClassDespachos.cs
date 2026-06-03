using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassDespachos
/// </summary>
public class ClassDespachos
{

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    // Declaración  de Variables

    public string ls_f_d { get; set; }
    public string ls_f_h { get; set; }
    public string ls_em_num { get; set; }
    public string ls_em_req { get; set; }
    public string ls_srv { get; set; }
    public string ls_codigo { get; set; }
    public string ls_obs { get; set; }
    public string ls_anul { get; set; }
    public string ls_bod { get; set; }
    public string ls_tipo { get; set; }
    public string ls_rut { get; set; }

    public ClassDespachos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    public DataSet mfBuscarPerifericas()
    {
        DataSet aoCod;
        string is_crit = "";
        string is_where = "";
        string ls_where = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";
        string lsinner = " ";

        // Genera String de SQL dinámico.


        // Limpia string de WHERE.
        if (is_where != "")
        {
            ls_where = is_where;
            ls_w = " ";
        }
        else
        {
            ls_where = "";
            ls_w = " WHERE ";
        }

        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {
            if (ls_where != "")
                ls_where += " AND ";

            ls_where += " ( D.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   D.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de despacho entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }


        // Si se ingresó Anulado, la agrega al Where.
        if (ls_rut != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.RUT = " + ls_rut + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }


        // Si se ingresó Anulado, la agrega al Where.
        if (ls_anul == "3")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDESTADO = " + ls_anul + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }
        else
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDESTADO <> 3 ) ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }


        // Si se ingresó Receta, la agrega al Where.
        if (ls_em_req != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_em_req + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }

        // Si se ingresó Receta, la agrega al Where.
        if (ls_bod != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( D.IDBODPRERIF = " + ls_bod + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }


        // Si se ingresó OS, la agrega al Where.
        if (ls_em_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( D.NUMOSAL = " + ls_em_num + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }

        string is_group = "";
        //string is_order_by = " ORDER BY DESP.F_H_CREACION ASC, DESP.NUMOSAL ASC";
        string is_order_by = " ORDER BY D.NUMOSAL, R.FOLIO desc";
        string is_select = "";


        // Si se ingresó Codigo., la agrega al Where.


        lsinner = " inner join M_RECETA R ON R.IDRECETA = D.idreqaut ";
        lsinner = lsinner + " inner join TG_ESTADOS ES ON ES.IDESTADO = R.IDESTADO ";
        lsinner = lsinner + " inner join " + modConstantes.gsDbPer + "V_ESTADOS ES1 ON ES1.IDESTADO = D.IDESTADO ";


        is_select = "SELECT D.IDDESPACHO, D.NUMOSAL, CONVERT(VARCHAR(10),D.F_H_CREACION,103) F_H_CREACION, " +
                    "ES.DESCRIPCION ESTADO, ES1.DESCRIPCION ESTADO_D,R.FOLIO, (R.NOMBRE + ' ' + R.APELL_PAT + ' ' + R.APELL_MAT)  NOMBRE, R.RUT " +
                    " ";

        lsTablas = " FROM " + modConstantes.gsDbPer + "M_DESPACHOS D ";

        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                 lsTablas + lsinner +
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
        string is_crit = "";
        string is_where = "";
        string ls_where = "";
        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";
        string lsinner = " ";

        // Genera String de SQL dinámico.


        // Limpia string de WHERE.
        if (is_where != "")
        {
            ls_where = is_where;
            ls_w = " ";
        }
        else
        {
            ls_where = "";
            ls_w = " WHERE ";
        }

        // Si se ingresó Fechas las agrega al WHERE.
        if (ls_f_d != "")
        {
            if (ls_where != "")
                ls_where += " AND ";

            ls_where += " ( D.F_H_CREACION >= cast( '" + ls_f_d + " 00:00:00' as datetime)  and " +
                            "   D.F_H_CREACION <= cast( '" + ls_f_h + " 23:59:00' as datetime) ) ";

            // Agrega criterio a var. aux.
            is_crit += " Fecha de despacho entre: " + ls_f_d + " y " + ls_f_h + " , ";
        }

        // Si se ingresó Anulado, la agrega al Where.
        if (ls_anul == "3")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDESTADO = " + ls_anul + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }
        else
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.IDESTADO <> 3 ) ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }


        // Si se ingresó Receta, la agrega al Where.
        if (ls_em_req != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( R.FOLIO = " + ls_em_req + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }



        // Si se ingresó OS, la agrega al Where.
        if (ls_em_num != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( D.NUMOSAL = " + ls_em_num + ") ";
            // Agrega criterio a var. aux.
            is_crit += " OS igual a: " + ls_bod + " , ";
        }

        string is_group = "";
        //string is_order_by = " ORDER BY DESP.F_H_CREACION ASC, DESP.NUMOSAL ASC";
        string is_order_by = " ORDER BY D.NUMOSAL, R.FOLIO desc";
        string is_select = "";




        lsinner = " inner join M_RECETA R ON R.IDRECETA = D.idreqaut ";
        lsinner = lsinner + " inner join TG_ESTADOS ES ON ES.IDESTADO = R.IDESTADO ";



        is_select = "SELECTD.IDDESPACHO, D.NUMOSAL, CONVERT(VARCHAR(10),D.F_H_CREACION,103) F_H_CREACION, " +
                    "ES.DESCRIPCION ESTADO, R.FOLIO, R.NOMBRE, R.RUT " +
                    " ";

        lsTablas = " FROM " + modConstantes.gsDbPer + "M_DESPACHOS D ";

        ls_where = ls_w + ls_where;
        ls_sql = is_select + ' ' +
                 lsTablas + lsinner +
                 ls_where + ' ' +
                   is_group + ' ' +
                   is_order_by;


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, ls_sql);

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
                "WHERE B.IDESTADO <> 3 " +
                "and C.IDUSUARIO = " + Identificador + " " +
                "and B.id_inst = " + asInst + " " +
                "order by B.DESCRIPCION_LARGA ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet ConsultarIDPerif(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select DESP.iddespacho, " +
                    "DESP.IDESTADO, DESP.IDUSUARIO, " +
                    "DESP.IDTIPODESP,DESP.F_H_CREACION, " +
                    "F_H_CREACION_REAL, " +
                    "DESP.F_H_MODIF, DESP.OBSERV, " +
                    "DESP.COD_DOC,DESP.NUM_DOC, " +
                    "DESP.NUMOSAL,DESP.DESTOSAL, " +
                    "DESP.IVAPCTJE, DESP.MONTO_DESP,DESP.idreqaut, " +
                    "bod.DESCRIPCION_LARGA as BODEGA, desp.IDBODPRERIF, " +
                    "CASE ISNULL(DESP.INTEGRADO_ORDEN,'') " +
                    "WHEN 'A'  THEN 'A INTEGRAR'  " +
                    "WHEN 'I'  THEN 'INTEGRADO'  " +
                    "WHEN 'E'  THEN 'ERROR AL INTEGRAR'  " +
                    "ELSE 'NO INTEGRADO'  " +
                    "End As Cab_orden " +
                    "from " + modConstantes.gsDbPer + "M_DESPACHOS AS DESP " +
                    "inner join " + modConstantes.gsDbPer + "TG_BOD_PERIFERICAS bod on bod.IDBODPRERIF = desp.IDBODPRERIF " +
                    "WHERE DESP.iddespacho = " + lsIdentificador;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }


    public DataSet DetallePerif(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT DET.IDDETDESP, VM.CODARTICULO CODMATERIAL, VM.DESCRIPCION_LARGA NOM_ARTICULO, VM.UNIDAD_MEDIDA DESC_UN_MED, " +
                "isnull(DET.CANT_SOLIC,0) CANT_SOLIC ,  isnull(DET.CANT_DESP,0)  CANT_DESP, isnull(DET.cant_desp_req,0) cant_desp_req, " +
                " 0 as despachar, isnull(DET.cant_dev,0)  cant_dev, (isnull(DET.CANT_DESP,0) - isnull(DET.cant_dev,0)) cant_dif, " +
                " CONVERT(VARCHAR(10),DET.FDESPACHO_D,103) FDESPACHO, CONVERT(DECIMAL(20,0),DET.RANGO_DISP_D) DIAS " +
                "FROM " + modConstantes.gsDbPer + "[M_DETDESP] AS DET   " +
                "INNER JOIN " + modConstantes.gsDbPer + "V_ARTICULOS AS VM ON VM.IDARTICULO = DET.IDMATERIAL   " +
                "INNER JOIN " + modConstantes.gsDbAB + "M_ART_RECETA AR ON AR.IDARTRECETA = DET.IDARTRECETA " +
                 "WHERE DET.IDDESPACHO = " + lsIdentificador + " " +
                 "order by VM.CODARTICULO";



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfUpdateCantAnularDesp(string asIdentificador, string asCant)
    {
        string lsRet = "";
        string lsSql = "UPDATE  " + modConstantes.gsDbPer + "M_DETDESP SET " +
                        " cant_desp_req = " + asCant + " " +
                        " WHERE IDDETDESP = " + asIdentificador;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfUpdateFechaDesp(string asIdentificador, string asFecha)
    {
        string lsRet = "";
        string lsSql = "UPDATE  " + modConstantes.gsDbPer + "M_DESPACHOS SET " +
                        " F_H_CREACION = '" + asFecha + "' " +
                        " WHERE IDDESPACHO = " + asIdentificador;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfUpdateFechaDetRecDesp(string asIdentificador, string asFecha, string asDias)
    {
        string lsRet = "";
        //string lsSql = "UPDATE AR SET AR.FDESPACHO = '" + asFecha + "', AR.ACUM_RANGO = " + asDias + "," +
        //                "AR.RANGO_DISP = CASE WHEN (AR.RANGO - AR.ACUM_RANGO) >= 0 THEN (AR.RANGO - AR.ACUM_RANGO) ELSE 0 END " +
        //               "FROM " + modConstantes.gsDbAB + "M_ART_RECETA AR " +
        //                " WHERE AR.IDARTRECETA = " + asIdentificador;

        string lsSql = " UPDATE AR SET AR.FDESPACHO_D = '" + asFecha + "', AR.RANGO_DISP_D = " + asDias + " " +
                 "FROM " + modConstantes.gsDbPer + "M_DETDESP AR " +
                " WHERE AR.IDDETDESP = " + asIdentificador;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public string mfCountIngresoCantidadDespAnular(string lsIdentificador)
    {
        string cantidad = "0";
        string lsSql;

        // Recupera suma cantidad despachar.

        lsSql = "select count(1) " +
                "from " + modConstantes.gsDbPer + "M_DETDESP " +
                "where IDDESPACHO =  " + lsIdentificador + " " +
                "and isnull(cant_desp_req, 0) > 0";

        con = bd.fnGetConn();
        cantidad = bd.ExecuteScalar(con, lsSql);
        con.Close();


        return cantidad;

    }

    public string mfISDespSinSaldo(string lsIdentificador)
    {
        string cantidad = "0";
        string lsSql;

        // Recupera suma cantidad despachar.

        lsSql = "select count(1) " +
                "from V_PEND_DISP " +
                "where IDDETDESP =  " + lsIdentificador + " ";

        con = bd.fnGetConn();
        cantidad = bd.ExecuteScalar(con, lsSql);
        con.Close();


        return cantidad;

    }

    public string mfMaxIDActa()
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select max(IDACTRECEP) " +
                     "from " + modConstantes.gsDbPer + "M_ACT_RECEP ";

            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "";

        }

        return lsRet;
    }

    public string mfIdArtTecDetDesp(string asId)
    {


        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select IDARTRECETA " +
                     "from " + modConstantes.gsDbPer + "M_DETDESP " +
                     "where IDDETDESP = " + asId;

            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "";

        }

        return lsRet;
    }

    public string mfIdAnulDepActa(string lsIdentificador)
    {
        string lsRet = "";

        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select ISNULL(IDREQAUT,0) " +
                "from " + modConstantes.gsDbPer + "M_ACT_RECEP " +
                "where IDACTRECEP = " + lsIdentificador + " ";

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }
}