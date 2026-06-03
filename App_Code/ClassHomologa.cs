using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de ClassHomologa
/// </summary>
public class ClassHomologa
{
    // Declaracion de Variables
    public string ls_nomb { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassHomologa()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string mfInsertHomol(string asIdart, string idartH, string asIdUser)
    {
        String lsRet = "";
        string lsSql = "";


        lsSql = "insert into " + modConstantes.gsDbAB + "M_HOMOLAGA_FARMACO(IDART, IDARTHOM, IDUSCREA) " +
                        "VALUES( " + asIdart + "," + idartH + ", " + asIdUser + ")";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfElimHomol(string asIdentificador)
    {
        String lsRet = "";
        string lsSql = "";

        lsSql = "delete from " + modConstantes.gsDbAB + "M_HOMOLAGA_FARMACO " +
                "where IDHOMFAR = " + asIdentificador + "  ";

        //lsSql = "UPDATE M_USER_UNIDAD SET IDESTADO = 3 " +
        //        "where IDUSERUNID = " + asIdentificador + "  ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public DataSet mfCargarGrillaHomolAdm(string IdArt, string lsDesc)
    {
        DataSet aoSer;
        string lsSql = "";
        string lsWhr = "";
        string lsOrder = "order by  V.CODARTICULO asc ";

        if (lsDesc != "")
        {
            lsWhr = lsWhr + "and V.DESCRIPCION_LARGA like '%" + lsDesc + "%' OR V.CODARTICULO like '%" + lsDesc + "%' ";
        }

        lsSql = "SELECT V.IDARTICULO, V.CODARTICULO CODIGO, V.DESCRIPCION_LARGA DESCRIPCION " +
                    "FROM v_articulos V " +
                    "WHERE V.IDESTADO <> 3 " +
                    "AND V.IDARTICULO NOT IN( " +
                    "SELECT H.IDART " +
                    "FROM M_HOMOLAGA_FARMACO H " +
                    "WHERE H.IDESTADO <> 3 " +
                    "AND H.IDARTHOM = " + IdArt + " " +
                    ")" + lsWhr + " " + lsOrder;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;
    }

    public DataSet mfCargaHomologaAdmArt(string idUser)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT H.IDHOMFAR, V.CODARTICULO CODIGO, V.DESCRIPCION_LARGA DESCRIPCION " +
               "FROM M_HOMOLAGA_FARMACO H " +
               "INNER JOIN v_articulos V ON V.IDARTICULO = H.IDART " +
               "WHERE V.IDESTADO <> 3 " +
               "AND H.IDARTHOM = " + idUser;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public DataSet mfddlHomologaArt(string id, string asIdbod)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT V.IDARTICULO CODIGO, (V.CODARTICULO  + ' / ' + V.DESCRIPCION_LARGA) DESCRIPCION " +
               "FROM M_HOMOLAGA_FARMACO H " +
               "INNER JOIN v_articulos V ON V.IDARTICULO = H.IDART " +
               "inner join M_ART_UNIDAD AU ON AU.IDARTICULO = V.IDARTICULO AND AU.CODUNIOP = " + asIdbod + " " +
               "WHERE V.IDESTADO <> 3 " +
               "AND H.IDARTHOM = " + id;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public DataSet mfddlHomologaArt(string id)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT V.IDARTICULO CODIGO, (V.CODARTICULO  + ' / ' + V.DESCRIPCION_LARGA) DESCRIPCION " +
               "FROM M_HOMOLAGA_FARMACO H " +
               "INNER JOIN v_articulos V ON V.IDARTICULO = H.IDART " +
               "WHERE V.IDESTADO <> 3 " +
               "AND H.IDARTHOM = " + id;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }
    public string mfCountHomolArt(string asId, string asIdart)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select Count(1)  " +
                      "from " + modConstantes.gsDbAB + "M_HOMOLAGA_FARMACO m " +
                      "where " +
                       "m.IDARTHOM = " + asId + " " +
                       "and m.IDART = " + asIdart;


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

    public string mfTieneDesp(string asId)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select CONVERT(DECIMAL(20,0),ISNULL(CANT_DESP,0))  " +
                      "from " + modConstantes.gsDbAB + "[M_ART_RECETA] m " +
                      "where " +
                       "m.IDARTRECETA = " + asId + "";


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

    public string mfCountHomol(string asId)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select Count(1)  " +
                      "from " + modConstantes.gsDbAB + "M_HOMOLAGA_FARMACO m " +
                      "where " +
                       "m.IDARTHOM = " + asId + "";


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

    public string mfAsociarPosologiaHomologa(string asIUsr, string asIdArt, string lsIdentifcador, string lsCant, string lsRango, string lsDuracion,
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
                        "OBS_FARM = '" + lsObservacion + "', " + " IDUSR_MOD = " + asIUsr + ", IDARTICULO = " + asIdArt + ", cant_desp_req = 0 " +
                        "WHERE IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return lsRet;
    }

    public string mfHomologaRecetas(string lsIdArt, string lsIdent)
    {

        string _XML = string.Empty;

        con = bd.fnGetConn();

        SqlCommand Query = new SqlCommand("pu_homologa_farm", con);
        Query.CommandType = CommandType.StoredProcedure;

        Query.Parameters.Add("@IDARTICULO_old", SqlDbType.Int, 4);
        Query.Parameters["@IDARTICULO_old"].Value = lsIdArt;

        Query.Parameters.Add("@IDARTRECETA", SqlDbType.Int, 4);
        Query.Parameters["@IDARTRECETA"].Value = lsIdent;

        Query.Parameters.Add("@XML", SqlDbType.VarChar, 8000);
        Query.Parameters["@XML"].Direction = ParameterDirection.Output;


        Query.CommandTimeout = 600;
        Query.ExecuteNonQuery();
        con.Close();

        _XML = Query.Parameters["@XML"].Value.ToString();


        return _XML;
    }

    public string mfUpHomologarArt(string asIUsr, string asIdArt, string lsIdentifcador, string lsRango, string lsPosologia, string lsFecha, string lsObs, string lsPend, string lsFechaNow)
    {
        string lsRet = "";


        con = bd.fnGetConn();

        string lsSql = "UPDATE M_ART_RECETA SET RANGO_DISP = " + lsRango.Replace(",", ".") + ", POSOLOGIA_DISP = " + lsPosologia.Replace(",", ".") + ", " +
                "FDESPACHO = '" + lsFecha + "', OBS_FARM = '" + lsObs + "', CANT_PEND = " + lsPend.Replace(",", ".") + ", " +
                " cant_desp_req =  " + lsPosologia.Replace(",", ".") + ", FDESPACHO_REAL = '" + lsFechaNow + "', SALDO_SALE = 1, " +
                " IDUSR_MOD = " + asIUsr + ", IDARTICULO = " + asIdArt + " " +
                "WHERE IDARTRECETA = " + lsIdentifcador;

        lsRet = bd.EjecutarComando(con, lsSql);
        //con.Close();


        return lsRet;
    }

    public DataSet mfListaSeguimiento(string id)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT V.CODARTICULO CODIGO,V.DESCRIPCION_LARGA FARMACO, " +
                "A.F_H_CREACION, U.NOMBRE " +
               " FROM[dbo].[M_ART_RECETA_HOMOLGA]  A " +
                "INNER JOIN v_articulos V ON V.IDARTICULO = A.IDARTICULO " +
                "INNER JOIN M_USUARIOS U ON U.IDUSUARIO = A.IDUSR_MOD " +
               " WHERE IDRECETA = " + id;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }
}