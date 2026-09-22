using System;
using System.Data;
/// <summary>
/// Descripción breve de ClassFeriado
/// </summary>
public class ClassFeriado
{
    // Declaración  de Variables
    public string lsEst { get; set; }
    public string lsId { get; set; }
    public string ls_anio { get; set; }

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    public ClassFeriado()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public DataSet mfBuscar()
    {
        DataSet aoCod;
        string lsSql = "";
        // Recupera Códigos de barra asociados.
        lsSql = 
            "SELECT IDFERIADO, FERIADO FECHA FROM " + modConstantes.gsDbRH + "M_FERIADOS " +
            "WHERE YEAR(FERIADO)=" + ls_anio + " " +
            "ORDER BY FERIADO DESC ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
    public string mfGuardar(string asFecha)
    {
        string lsRet = "";
        string lsSql = "";
        con = bd.fnGetConn();
        lsSql = "insert into " + modConstantes.gsDbRH + "M_FERIADOS(FERIADO) " +
                "values( '" + asFecha + "')";
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }
    public string mfDelete(string asId)
    {
        string lsRet = "";
        string lsSql = "";
        con = bd.fnGetConn();
        lsSql = "delete from " + modConstantes.gsDbRH + "M_FERIADOS " +
                "where IDFERIADO = " + asId + " ";
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }
    public string mfExisteFecha(string asFecha)
    {
        String lsRet = "0";
        string lsSql = "";
        lsSql = "select count(*) from " + modConstantes.gsDbRH + "M_FERIADOS " +
            "WHERE CONVERT(VARCHAR(10),FERIADO,103) = CONVERT(VARCHAR(10),'" + asFecha + "',103)";
        con = bd.fnGetConn();
        lsRet = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return lsRet;
    }
}