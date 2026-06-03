using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassFirma
/// </summary>
public class ClassFirma
{

    // Declaración  de Variables
    public string lsBod { get; set; }
    public string lsEst { get; set; }
    public string lsId { get; set; }

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    public ClassFirma()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfBuscar()
    {
        DataSet aoCod;

        string lsSql = "";
        string lsWhere = "where IDCONSTANTE > 0 ";
        string lsOrder = " order by IDCONSTANTE asc ";



        //if (lsBod != "")
        //  lsWhere = lsWhere + " and DESCRIPCION  like '%" + lsBod + "%' ";

        if (lsEst == "3")
            lsWhere = lsWhere + " and isnull(ESTADO,1)   = " + lsEst + " ";
        else
            lsWhere = lsWhere + " and isnull(ESTADO,1)   <> 3 ";


        // Recupera Códigos de barra asociados.
        lsSql = "select IDCONSTANTE, CODIGO, DESCRIPCION as ENCARGADO " +
                "from " + modConstantes.gsDbAB + "m_constantes " + lsWhere + lsOrder;

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfGuardar(string asCode, string asDescr)
    {
        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = "insert into " + modConstantes.gsDbAB + "m_constantes(CODIGO, DESCRIPCION, ESTADO) " +
                "values( '" + asCode + "', '" + asDescr + "' , " + 1 + ")";

        lsRet = bd.EjecutarComando(con, lsSql);



        con.Close();
        return lsRet;
    }

    public string mfExisteId(string asID)
    {
        String lsRet = "0";
        string lsSql = "";

        lsSql = "select count(*) from " + modConstantes.gsDbAB + "m_constantes WHERE codigo = '" + asID + "'";

        con = bd.fnGetConn();
        lsRet = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string updateEstado(string asCode, string estado)
    {

        //Aqui cambie en vez de UPDATE por DELETE. Dejo el codigo para posible reestructuracion de la BD 


        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "m_constantes set estado = " + estado + " " +
                "where idconstante = " + asCode;

        lsRet = bd.EjecutarComando(con, lsSql);
        /**
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = "delete from " + modConstantes.gsDbAB + "m_constantes where IDCONSTANTE = '" + asCode + "'";

        lsRet = bd.EjecutarComando(con, lsSql);
        */

        con.Close();
        return lsRet;
    }

    public DataSet DetalleFirmapopup(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDCONSTANTE, CODIGO, DESCRIPCION " +
                "FROM " + modConstantes.gsDbAB + "M_CONSTANTES " +
                "WHERE IDCONSTANTE = " + lsIdentificador;



        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string editFirma(string asID, string asCode, string asEncargado)
    {
        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = "update " + modConstantes.gsDbAB + "m_constantes set CODIGO = '" + asCode + "' , DESCRIPCION = '" + asEncargado + "' " +
                "where idconstante = " + asID;

        lsRet = bd.EjecutarComando(con, lsSql);
        /**
        string lsRet = "";
        string lsSql = "";

        con = bd.fnGetConn();

        lsSql = "delete from " + modConstantes.gsDbAB + "m_constantes where IDCONSTANTE = '" + asCode + "'";

        lsRet = bd.EjecutarComando(con, lsSql);
        */

        con.Close();
        return lsRet;
    }
}