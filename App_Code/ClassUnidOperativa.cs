using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de ClassUnidOperativa
/// </summary>
public class ClassUnidOperativa
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    modFunciones func = new modFunciones();

    public static SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

    // Declaración  de Variables
    public string lsanio { get; set; }
    public int IdEstado { get; set; }
    public int IdUnidadOperativa { get; set; }
    public string UnidadSuperior { get; set; }
    public string NombreUnidad { get; set; }



    public ClassUnidOperativa()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet ConsultarID(string lsId)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT U.IDSUP_UNIDAD,  U.DESCRIPCION, U.EXTIENDE " +
                "FROM " + modConstantes.gsDbAB + "[v_unidad_operativa] U " +
                "WHERE U.CODUNIOP = " + lsId + " ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public static string mfGetPadreUnidad(string asCodigo)
    {
        string lsRet = "";
        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;
        modFunciones func = new modFunciones();

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select IDSUP_UNIDAD  " +
                      "from " + modConstantes.gsDbAB + "v_unidad_operativa " +
                      "where " +
                       "CODUNIOP = " + asCodigo + " ";


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

    public string mfGetSeExtiende(string asIndentificador)
    {
        string aoCod;
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select ISNULL(EXTIENDE,0) " +
                "from " + modConstantes.gsDbAB + "[M_UNIDAD_OPERATIVA] " +
                "where CODUNIOP = " + asIndentificador;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }
    public string mfGetIdUnidad(string asCodigo)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select CODUNIOP  " +
                      "from " + modConstantes.gsDbAB + "v_unidad_operativa m " +
                      "where " +
                       "DESCRIPCION = '" + asCodigo + "'";


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

    public string getUnidadOp(string asCodigo)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select DESCRIPCION  " +
                      "from " + modConstantes.gsDbAB + "v_unidad_operativa m " +
                      "where " +
                       "CODUNIOP = '" + asCodigo + "'";


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

    public string getUnidadOpUsr(string asCodigo)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;

            if (asCodigo == "0")
            {

                lsSql = "select DESCRIPCION  " +
                          "from " + modConstantes.gsDbAB + "v_unidad_operativa m ";

            }
            else
            {
                lsSql = "select DESCRIPCION  " +
                     "from " + modConstantes.gsDbAB + "v_unidad_operativa m " +
                     "where " +
                      "CODUNIOP = '" + asCodigo + "'";
            }




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

    public DataSet mfListaUnidad(string lsIdInst)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT CODUNIOP, IDSUP_UNIDAD, CODPADRE, DESCRIPCION, IDESTADO, ID_INST, SOLICITADO " +
                "FROM " + modConstantes.gsDbAB + "m_unidad_operativa " +
                "WHERE IDESTADO <> 3 " +
                "AND ID_INST = " + lsIdInst + " " +
                "order by DESCRIPCION ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfUnidadDDl(string lsIdentificador, string lsIdInst)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT U.CODUNIOP,  DESCRIPCION " +
                "FROM " + modConstantes.gsDbAB + "[v_unidad_operativa] U " +
                "INNER JOIN " + modConstantes.gsDbAB + "[M_USER_UNIDAD] B ON U.CODUNIOP = B.CODUNIOP AND B.IDESTADO <> 3  " +
                "WHERE U.IDESTADO <> 3 " +
                "AND B.IDUSUARIO = " + lsIdentificador + " " +
                "AND B.ID_INST = " + lsIdInst + " " +
                "order by DESCRIPCION ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfDDlUnidadCama()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT CODUNIOP, DESCRIPCION " +
                "FROM M_UNIDAD_OPERATIVA " +
                "WHERE IDESTADO <> 3 " +
                "AND ISNULL(CAMAS,0) = 1";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfDDlSubServicio(string asId)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDSUBUNIDAD, DESCRIPCION " +
                "FROM M_SUB_UNIDAD " +
                "WHERE IDESTADO <> 3 " +
                "AND CODUNIOP = " + asId;

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfDDlCamaUnidad(string asId)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDCAMA, DESCRIPCION " +
                "FROM M_CAMAS " +
                "WHERE IDESTADO <> 3 " +
                "AND IDSUBUNIDAD = " + asId;

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }


    public string mfDesUnidad(string Identificador)
    {
        String lsRet = "";
        string lsSql = "";

        lsSql = "select DESCRIPCION as nombre_unidad  " +
                " from " + modConstantes.gsDbAB + "v_unidad_operativa " +
                " where idestado <> 3 and CODUNIOP = " + Identificador;

        con = bd.fnGetConn();
        lsRet = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRet;
    }

    public DataSet mfUnidadSolDDl(string lsIdInst)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT U.CODUNIOP,  U.DESCRIPCION " +
                "FROM " + modConstantes.gsDbAB + "[v_unidad_operativa] U " +
                "WHERE U.IDESTADO <> 3 " +
                "AND U.SOLICITADO = 2 " +
                "AND U.ID_INST = " + lsIdInst + " " +
                "order by U.DESCRIPCION ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfValidaUnidadDDl(string lsIdentificador, string lsIdInst)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT U.CODUNIOP,  DESCRIPCION " +
                "FROM " + modConstantes.gsDbAB + "[v_unidad_operativa] U " +
                "INNER JOIN " + modConstantes.gsDbAB + "[M_USER_UNIDAD] B ON U.CODUNIOP = B.CODUNIOP AND B.IDESTADO <> 3  " +
                "WHERE U.IDESTADO <> 3 " +
                "AND B.IDUSUARIO = " + lsIdentificador + " " +
                "AND B.ID_INST = " + lsIdInst + " " +
                "AND B.VALIDA = 1 ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }


    #region Usuario

    public DataSet mfCargaListaUnidadUsr(string lsUnidad, string idUser, string idinst)
    {
        DataSet aoSer;
        string lsSql = "";
        string lsWhere = "";


        if (lsUnidad != "")
            lsWhere = lsWhere + " and (rtrim(ltrim(A.DESCRIPCION))) like '%" + lsUnidad + "%' ";


        lsSql = "SELECT A.CODUNIOP, A.IDSUP_UNIDAD, A.CODPADRE, A.DESCRIPCION, A.IDESTADO, A.ID_INST " +
                "FROM " + modConstantes.gsDbAB + "[M_UNIDAD_OPERATIVA] A " +
                "WHERE A.CODUNIOP NOT IN ( " +
                "SELECT B.CODUNIOP FROM " + modConstantes.gsDbAB + "[M_USER_UNIDAD] B  " +
                "where B.IDUSUARIO = " + idUser + " " +
                "AND B.IDESTADO <> 3 " +
                "AND B.ID_INST = " + idinst + " " +
                ") " +
                "AND A.ID_INST = " + idinst + " " +
                lsWhere;

        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }



    public DataSet mfCargaUserUnidad(string idUser, string idInst)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT B.IDUSERUNID,  A.CODUNIOP, A.IDSUP_UNIDAD, A.CODPADRE, A.DESCRIPCION, B.IDESTADO, " +
                " case when b.valida = 1 then 'SI' else 'NO' end valida " +
                "FROM " + modConstantes.gsDbAB + "[M_UNIDAD_OPERATIVA] A " +
                "INNER JOIN  " + modConstantes.gsDbAB + "[M_USER_UNIDAD] B ON A.CODUNIOP = B.CODUNIOP AND " +
                "B.IDESTADO <> 3 AND B.IDUSUARIO = " + idUser + " " +
                "AND B.ID_INST = " + idInst + " " +
                "WHERE A.IDESTADO <> 3 " +
                "AND A.ID_INST = " + idInst;

        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public string mfDeleteUnidadUsr(string asIdUser, string idinst)
    {
        String lsRet = "";
        string lsSql = "";


        lsSql = "delete from " + modConstantes.gsDbAB + "M_USER_UNIDAD " +
                "where IDUSUARIO = " + asIdUser + " " +
                "and ID_INST = " + idinst;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfInsertUnidadUsr(string asIdUnidad, string asIdUser, string idinst)
    {
        String lsRet = "";
        string lsSql = "";


        lsSql = "insert into " + modConstantes.gsDbAB + "M_USER_UNIDAD(IDUSUARIO, CODUNIOP, ID_INST, IDESTADO, F_H_CREACION, VALIDA) " +
                        "VALUES( " + asIdUser + ",  " + asIdUnidad + ",  " + idinst + ", 1, getdate(), 0)";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }




    public string mfElimUnidadUsr(string asIdentificador)
    {
        String lsRet = "";
        string lsSql = "";

        lsSql = "delete from " + modConstantes.gsDbAB + "M_USER_UNIDAD " +
                "where IDUSERUNID = " + asIdentificador + "  ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfValidaUnidadUsr(string asIdentificador, String asValida)
    {
        String lsRet = "";
        string lsSql = "";

        if (asValida == "SI")
            lsSql = "Update " + modConstantes.gsDbAB + "M_USER_UNIDAD set VALIDA = 0 " +
                    "where IDUSERUNID = " + asIdentificador + "  ";
        else
            lsSql = "Update " + modConstantes.gsDbAB + "M_USER_UNIDAD set VALIDA = 1 " +
                "where IDUSERUNID = " + asIdentificador + "  ";


        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }
    #endregion

    #region Articulos

    public DataSet mfCargaListaArtUnidad(string lsCod, string lsUnidad, string id, string idInst)
    {
        DataSet aoSer;
        string lsSql = "";
        string lsWhere = "";

        if (lsCod != "")
            lsWhere = lsWhere + " and (rtrim(ltrim(A.CODARTICULO))) like '%" + lsCod + "%' ";

        if (lsUnidad != "")
            lsWhere = lsWhere + " and (rtrim(ltrim(A.DESCRIPCION_LARGA))) like '%" + lsUnidad + "%' ";

        lsSql = "SELECT B.IDARTUNID,  A.CODARTICULO, A.DESCRIPCION_LARGA " +
                "FROM " + modConstantes.gsDbAB + "[v_articulos] A " +
                "INNER JOIN  " + modConstantes.gsDbAB + "[M_ART_UNIDAD] B ON A.IDARTICULO = B.IDARTICULO AND " +
                "B.IDESTADO <> 3 AND B.CODUNIOP = " + id + " " +
                "AND B.ID_INST = " + idInst + " " +
                "WHERE A.ID_INST = " + idInst + " " +
                lsWhere;

        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public DataSet mfCargaListaArt(string lsCod, string lsUnidad, string idUni, string idinst)
    {
        DataSet aoSer;
        string lsSql = "";
        string lsWhere = "";


        if (lsCod != "")
            lsWhere = lsWhere + " and (rtrim(ltrim(A.CODARTICULO))) like '%" + lsCod + "%' ";

        if (lsUnidad != "")
            lsWhere = lsWhere + " and (rtrim(ltrim(A.DESCRIPCION_LARGA))) like '%" + lsUnidad + "%' ";


        lsSql = "SELECT A.IDARTICULO, A.CODARTICULO, A.DESCRIPCION_LARGA " +
                "FROM " + modConstantes.gsDbAB + "[v_articulos] A " +
                "WHERE A.IDARTICULO NOT IN ( " +
                "SELECT B.IDARTICULO FROM " + modConstantes.gsDbAB + "[M_ART_UNIDAD] B  " +
                "where B.CODUNIOP = " + idUni + " " +
                "AND B.IDESTADO <> 3 " +
                "AND B.ID_INST = " + idinst + " " +
                ") " +
                "AND A.ID_INST = " + idinst + " " +
                lsWhere;

        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public DataSet mfCargaListaUnidad(string lsUnidad, string idUser, string idinst)
    {
        DataSet aoSer;
        string lsSql = "";
        string lsWhere = "";
        string lsOrder = "order by  A.DESCRIPCION asc ";


        if (lsUnidad != "")
            lsWhere = lsWhere + " and (rtrim(ltrim(A.DESCRIPCION))) like '%" + lsUnidad + "%' ";


        lsSql = "SELECT A.CODUNIOP, A.IDSUP_UNIDAD, A.DESCRIPCION, A.ID_INST " +
                "FROM " + modConstantes.gsDbAB + "[v_unidad_operativa] A " +
                "WHERE A.CODUNIOP NOT IN ( " +
                "SELECT B.CODUNIOP FROM " + modConstantes.gsDbAB + "[M_ART_UNIDAD] B  " +
                "where B.IDARTICULO = " + idUser + " " +
                "AND B.IDESTADO <> 3 " +
                "AND B.ID_INST = " + idinst + " " +
                ") " +
                "AND A.ID_INST = " + idinst + " " +
                lsWhere + " " + lsOrder;

        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public DataSet mfCargarGrillaViaAdm(string IdArt, string lsDesc)
    {
        DataSet aoSer;
        string lsSql = "";
        string lsWhr = "";
        string lsOrder = "order by  DESCRIPCION asc ";

        if (lsDesc != "")
        {
            lsWhr = lsWhr + "and DESCRIPCION like '%" + lsDesc + "%' ";
        }

        lsSql = "SELECT IDVIA,DESCRIPCION,IDESTADO  " +
                "FROM [M_VIA] A  " +
                "WHERE A.IDVIA   " +
                "NOT IN (   SELECT B.IDVIA   " +
                "FROM [M_ART_VIA] B     " +
                "where B.IDARTICULO = " + IdArt + " " +
                "AND B.IDESTADO <> 3    )   " +
                "AND IDESTADO <> 3 " + lsWhr + " " + lsOrder;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;
    }

    public DataSet mfCargaViaAdmArt(string idUser)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT A.IDARTVIA, B.DESCRIPCION " +
            "FROM M_ART_VIA A " +
            "JOIN M_VIA B ON B.IDVIA = A.IDVIA " +
            "JOIN v_articulos V ON A.IDARTICULO = V.IDARTICULO " +
            "WHERE V.IDARTICULO = " + idUser;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }



    public DataSet mfCargaArtUnidad(string idUser, string idInst)
    {
        DataSet aoSer;
        string lsSql = "";

        lsSql = "SELECT B.IDARTUNID,  A.CODUNIOP, A.IDSUP_UNIDAD, A.DESCRIPCION, B.IDESTADO " +
                "FROM " + modConstantes.gsDbAB + "[v_unidad_operativa] A " +
                "INNER JOIN  " + modConstantes.gsDbAB + "[M_ART_UNIDAD] B ON A.CODUNIOP = B.CODUNIOP AND " +
                "B.IDESTADO <> 3 AND B.IDARTICULO = " + idUser + " " +
                "AND B.ID_INST = " + idInst + " " +
                "WHERE A.ID_INST = " + idInst;


        con = bd.fnGetConn();
        aoSer = bd.Fill(con, lsSql);
        con.Close();
        return aoSer;

    }

    public string mfDeleteUnidad(string asIdUser, string idinst)
    {
        String lsRet = "";
        string lsSql = "";


        lsSql = "delete from " + modConstantes.gsDbAB + "M_ART_UNIDAD " +
                "where IDARTICULO = " + asIdUser + " " +
                "and ID_INST = " + idinst;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfInsertUnidad(string asIdUnidad, string asIdart, string idinst)
    {
        String lsRet = "";
        string lsSql = "";


        lsSql = "insert into " + modConstantes.gsDbAB + "M_ART_UNIDAD(IDARTICULO, CODUNIOP, ID_INST, IDESTADO, F_H_CREACION) " +
                        "VALUES( " + asIdart + ",  " + asIdUnidad + ",  " + idinst + ", 1, getdate())";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfInsertVia(string asIdUser, string idVia)
    {
        String lsRet = "";
        string lsSql = "";


        lsSql = "insert into " + modConstantes.gsDbAB + "M_ART_VIA(IDARTICULO, IDESTADO, F_H_CREACION, IDVIA) " +
                        "VALUES( " + idVia + ", 1, getdate(), " + asIdUser + ")";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }


    public string mfElimVia(string asIdentificador)
    {
        String lsRet = "";
        string lsSql = "";

        lsSql = "delete from " + modConstantes.gsDbAB + "M_ART_VIA " +
                "where IDARTVIA = " + asIdentificador + "  ";

        //lsSql = "UPDATE M_USER_UNIDAD SET IDESTADO = 3 " +
        //        "where IDUSERUNID = " + asIdentificador + "  ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfElimUnidad(string asIdentificador)
    {
        String lsRet = "";
        string lsSql = "";

        lsSql = "delete from " + modConstantes.gsDbAB + "M_ART_UNIDAD " +
                "where IDARTUNID = " + asIdentificador + "  ";

        //lsSql = "UPDATE M_USER_UNIDAD SET IDESTADO = 3 " +
        //        "where IDUSERUNID = " + asIdentificador + "  ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfValidaUnidad(string asIdentificador, String asValida)
    {
        String lsRet = "";
        string lsSql = "";

        if (asValida == "SI")
            lsSql = "Update " + modConstantes.gsDbAB + "M_USER_UNIDAD set VALIDA = 0 " +
                    "where IDUSERUNID = " + asIdentificador + "  ";
        else
            lsSql = "Update " + modConstantes.gsDbAB + "M_USER_UNIDAD set VALIDA = 1 " +
                "where IDUSERUNID = " + asIdentificador + "  ";

        //lsSql = "UPDATE M_USER_UNIDAD SET IDESTADO = 3 " +
        //        "where IDUSERUNID = " + asIdentificador + "  ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfCountUsr(string asIdUsr, string asIdUnd)
    {
        String lsRet = "0";
        string lsSql = "";

        lsSql = "SELECT COUNT(1) " +
                "FROM [dbo].[M_USER_UNIDAD] " +
                "WHERE IDUSUARIO = " + asIdUsr + " AND IDESTADO <> 3 " +
                "AND CODUNIOP = " + asIdUnd;

        con = bd.fnGetConn();
        lsRet = bd.ExecuteScalar(con, lsSql);
        if (lsRet == "") lsRet = "0";
        con.Close();
        return lsRet;
    }
    #endregion


    public string mfExisteId(string asID)
    {
        String lsRet = "0";
        string lsSql = "";

        lsSql = "select count(*) from " + modConstantes.gsDbAB + "TG_UNIDAD_SUPERIOR WHERE IDSUP_UNIDAD = UPPER('" + asID + "')";

        con = bd.fnGetConn();
        lsRet = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfExisteUOP(string asID)
    {
        String lsRet = "0";
        string lsSql = "";

        lsSql = "select count(*) from " + modConstantes.gsDbAB + "v_unidad_operativa WHERE DESCRIPCION = UPPER('" + asID + "')";

        con = bd.fnGetConn();
        lsRet = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRet;
    }

    //// DMS

    public int Eli_Rest_UnidadOperativa(ClassUnidOperativa OP)
    {
        int valor = 0;
        string nulo = "";
        int cero = 0, uno = 1;
        decimal zero = 0;
        try
        {
            con2.Open();
            SqlCommand comando = con2.CreateCommand();
            comando.CommandText = "dbo.spEliRestUnidadOperativa";
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter par = null;

            par = comando.Parameters.Add("@IdUnidadOperativa", SqlDbType.Int);
            if (OP.IdUnidadOperativa.ToString() != "" && OP.IdUnidadOperativa != 0)
            {
                par.Value = Convert.ToInt32(OP.IdUnidadOperativa);
            }
            else
            {
                par.Value = uno;
            }

            par = comando.Parameters.Add("@IdEstado", SqlDbType.Int);
            if (OP.IdEstado.ToString() != "" && OP.IdEstado != 0)
            {
                par.Value = Convert.ToInt32(OP.IdEstado);
            }
            else
            {
                par.Value = uno;
            }


            SqlDataReader lector = comando.ExecuteReader();

            con2.Close();
            return 1;
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            con2.Close();
            return 0;
        }
        finally
        {
            con2.Close();
        }
    }


    public DataTable busca_uni_op(ClassUnidOperativa OP)
    {
        DataTable dtResultado = new DataTable();
        string nulo = "";
        int cero = 0;
        DateTime fecha_nula = Convert.ToDateTime("1/1/1900");
        try
        {
            con2.Open();
            SqlCommand comando = con2.CreateCommand();
            comando.CommandText = "dbo.spBuscaUnidadOperativa";
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter par = null;

            par = comando.Parameters.Add("@IdEstado", SqlDbType.Int);
            if (OP.IdEstado.ToString() != "" && OP.IdEstado != 0)
            {
                par.Value = Convert.ToInt32(OP.IdEstado);
            }
            else
            {
                par.Value = cero;
            }

            par = comando.Parameters.Add("@UnidadSuperior", SqlDbType.VarChar, 5);
            if (OP.UnidadSuperior != "" && OP.UnidadSuperior != null)
            {
                par.Value = OP.UnidadSuperior;
            }
            else
            {
                par.Value = nulo;
            }

            par = comando.Parameters.Add("@NombreUnidad", SqlDbType.VarChar, 100);
            if (OP.NombreUnidad != "" && OP.NombreUnidad != null)
            {
                par.Value = OP.NombreUnidad;
            }
            else
            {
                par.Value = nulo;
            }


            IDataReader dr = comando.ExecuteReader();

            dtResultado.Load(dr);
            con2.Close();
            return dtResultado;
        }
        catch (SqlException ex)
        {
            string error = ex.Message;
            con2.Close();
            return dtResultado;
        }
        finally
        {
            con2.Close();
        }
    }


    public int InsertUnidadOperativa(ClassUnidOperativa OP)
    {
        int valor = 0;
        string nulo = "";
        int cero = 0, uno = 1;
        decimal zero = 0;
        try
        {
            con2.Open();
            SqlCommand comando = con2.CreateCommand();
            comando.CommandText = "dbo.spGuardaUniOp";
            comando.CommandType = CommandType.StoredProcedure;
            SqlParameter par = null;

            par = comando.Parameters.Add("@UnidadSuperior", SqlDbType.VarChar, 5);
            if (OP.UnidadSuperior != "" && OP.UnidadSuperior != null)
            {
                par.Value = OP.UnidadSuperior;
            }
            else
            {
                par.Value = nulo;
            }

            par = comando.Parameters.Add("@NombreUnidad", SqlDbType.VarChar, 100);
            if (OP.NombreUnidad != "" && OP.NombreUnidad != null)
            {
                par.Value = OP.NombreUnidad;
            }
            else
            {
                par.Value = nulo;
            }

            par = comando.Parameters.Add("@IdEstado", SqlDbType.Int);
            if (OP.IdEstado.ToString() != "" && OP.IdEstado != 0)
            {
                par.Value = Convert.ToInt32(OP.IdEstado);
            }
            else
            {
                par.Value = uno;
            }

            SqlDataReader lector = comando.ExecuteReader();

            con2.Close();
            return 1;
        }
        catch (Exception ex)
        {
            string error = ex.Message;
            con2.Close();
            return 0;
        }
        finally
        {
            con2.Close();
        }
    }

    public string ModificarUnidad(string cod, string codSup, string descripcion, string asExti)
    {
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = "UPDATE " + modConstantes.gsDbAB + "M_UNIDAD_OPERATIVA set DESCRIPCION = '" + descripcion + "', " +
            " EXTIENDE = " + asExti + " " +
                "WHERE CODUNIOP = " + cod;

        lsRet = bd.EjecutarComando(con, lsSql);

        con.Close();
        return lsRet;

    }



}