using System.Data;

/// <summary>
/// Descripción breve de ClassDocumentos
/// </summary>
public class ClassDocumentos
{

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    modFunciones func = new modFunciones();

    // Declaración  de Variables
    public string lsId { get; set; }
    public string IdEstado { get; set; }
    public string Espec { get; set; }
    public string tipo { get; set; }


    public ClassDocumentos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string mfUpdateEstado(string asIdentificador, string asEstado)
    {
        string lsRet = "";


        string lsSql = "update M_DOCUMENTOS SET " +
                              "idestado = " + asEstado + " " +
                              "where  IDDOC = " + asIdentificador + " ";
        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfUpdateDesc(string asIdentificador, string asDesc)
    {
        string lsRet = "";


        string lsSql = "update M_DOCUMENTOS SET " +
                              "ESPECIFICACION = '" + asDesc + "' " +
                              "where  IDDOC = " + asIdentificador + " ";
        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public DataSet mfBuscar()
    {
        DataSet aoCod;
        string ls_where = "";


        ls_where = "WHERE ( IDDOC > 0 ) ";

        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  F_H_CREACION DESC";
        string is_select = "";

        // Si se ingresó Estado la agrega al Where.
        if (IdEstado != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( IDESTADO = " + IdEstado + " ) ";

        }


        // Si se ingresó Especificacion la agrega al Where.
        if (Espec != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( ESPECIFICACION like '%" + Espec + "%' ) ";

        }

        // Si se ingresó Tipo la agrega al Where.
        if (tipo != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( IDTIPODOC = " + tipo + " ) ";

        }


        // Region Tablas


        // Recupera Códigos de barra asociados.

        is_select = "SELECT IDDOC, F_H_CREACION,  " +
                    "IDESTADO, DOCUMENTO,  " +
                    "CASE WHEN IDTIPODOC = 1 then 'Manual' ELSE 'Certificado' END TIPODOC, " +
                    "DESCRIPCION, TIPO, ESPECIFICACION " +
                    "FROM M_DOCUMENTOS  ";


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

    public DataSet mfBuscarMultimedia()
    {
        DataSet aoCod;
        string ls_where = "";


        ls_where = "WHERE ( IDDOC > 0 and idestado <> 3 ) ";

        string ls_w = "";
        string ls_sql = "";
        string lsTablas = "";

        // Genera String de SQL dinámico.

        string is_group = "";
        string is_order_by = " ORDER BY  F_H_CREACION DESC";
        string is_select = "";



        // Si se ingresó Especificacion la agrega al Where.
        if (Espec != "")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( ESPECIFICACION like '%" + Espec + "%' ) ";

        }

        // Si se ingresó Tipo la agrega al Where.
        if (tipo != "0")
        {
            if (ls_where != "")
                ls_where += " and ";

            ls_where += " ( IDTIPODOC = " + tipo + " ) ";

        }


        // Region Tablas


        // Recupera Códigos de barra asociados.

        is_select = "SELECT IDDOC, F_H_CREACION,  " +
                    "IDESTADO, DOCUMENTO,  " +
                    "CASE WHEN IDTIPODOC = 1 then 'Manual' ELSE 'Certificado' END TIPODOC, " +
                    "DESCRIPCION, TIPO, ESPECIFICACION " +
                    "FROM M_DOCUMENTOS  ";


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

    public string mfIngresarDoc(string asEsp, string asDocumento, string asTipoDoc, string asDesc, string asTipo)
    {
        string lsRet = "";
        string lsSql = "insert into " + modConstantes.gsDbAB + "[M_DOCUMENTOS](" +
                        " ESPECIFICACION, F_H_CREACION, IDESTADO, " +
                        " DOCUMENTO, IDTIPODOC, DESCRIPCION," +
                        " TIPO)" +
                        " values(                   " +
                        " '" + asEsp + "',  " +
                        " getdate() ,               " +
                        " 1,                        " +
                        " '" + asDocumento + "',    " +
                        " '" + asTipoDoc + "',      " +
                        " '" + asDesc.Replace(",", "") + "',         " +
                        " '" + asTipo + "'          " +
                        " )";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;

    }

    public DataSet mfCargaDetDoc(string lsIdentificador)
    {
        DataSet aoBind;
        string lsSql = "";

        lsSql = "SELECT IDDOC, F_H_CREACION, IDESTADO, DOCUMENTO, IDTIPODOC, DESCRIPCION, TIPO, ESPECIFICACION " +
                "FROM " + modConstantes.gsDbAB + "[M_DOCUMENTOS] " +
                "WHERE IDDOC = " + lsIdentificador;

        con = bd.fnGetConn();
        aoBind = bd.Fill(con, lsSql);
        con.Close();
        return aoBind;
    }
}