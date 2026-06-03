using System;
using System.Data;
using System.Web.UI;

/// <summary>
/// Descripción breve de Mensaje
/// </summary>
public class Mensaje
{

    private static String msTabM = "m_msg_gen";
    private String msPkM = "idmsggen";
    private String msTM = modConstantes.gsDbAB + msTabM;
    private String msVM = modConstantes.gsDbAB + "v_lst_msg";

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public Mensaje()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    // btnNuevo.Attributes.Add("onclick", "return confirm('¿desea borrar estos datos?')");

    public void mensaje(Page page, string Texto)
    {
        ClientScriptManager csm = page.ClientScript;

        if (!csm.IsStartupScriptRegistered(page.GetType(), "winPop"))
        {
            csm.RegisterStartupScript(page.GetType(), "Mensaje", "alert('" + Texto + "');", true);
        }
    }

    // Funciones Privadas.
    public DataSet jaxListaMsg(
            String asTipo,
            String asIdentificador,
            String asCodSistema)
    {
        DataSet aoDs;
        try
        {
            String lsSql = "";


            // Recupera datos.
            lsSql =
                "select " +
                    msPkM + " , msg , fecha , usuario , archivo , cvd " +
                "from " + msVM + " " +
                "where " +
                "idestado <> 3                    and " +
                "idsistemas  = " + asCodSistema + " and " +
                "tipo_msg    ='" + asTipo + "'      and " +
                "identif     = " + asIdentificador + " " +
                "order by fecha desc";

            con = bd.fnGetConn();
            aoDs = bd.Fill(con, lsSql);
            con.Close();
        }
        catch
        {
            return null;
        }
        return aoDs;
    }

    public DataSet jaxConsultaCantUlt(String asTipo, String asCodSistema, String asIdentificador)
    {
        DataSet aoDs;

        modFunciones modfun = new modFunciones();
        try
        {
            String lsSql = "";
            String lsAux = "";

            // Recupera datos.
            lsSql =
                "select count(*) " +
                "from " + msVM + " " +
                "where " +
                "idestado <> 3                    and " +
                "idsistema  = " + asCodSistema + " and " +
                "tipo_msg    ='" + asTipo + "'      and " +
                "identif     = " + asIdentificador;

            con = bd.fnGetConn();
            lsAux = bd.ExecuteScalar(con, lsSql);
            con.Close();

            if (modfun.fnLong(lsAux) <= 0)
                return null;

            lsSql =
                "select top 1 msg as mensaje , fecha , usuario as operador , 0 as cantidad " +
                "from " + msVM + " " +
                "where " +
                    "idestado <> 3                    and " +
                    "idsistema  = " + asCodSistema + " and " +
                    "tipo_msg    ='" + asTipo + "'      and " +
                    "identif     = " + asIdentificador + " " +
                "order by fecha desc";

            con = bd.fnGetConn();
            aoDs = bd.Fill(con, lsSql);
            con.Close();


            if (aoDs.Tables.Count == 0) return null;
            aoDs.Tables[0].Rows[0]["cantidad"] = modfun.fnLong(lsAux);

            return aoDs;
        }
        catch
        {
            return null;
        }
    }


    //public DataSet jaxConsultaCantUlt(String asTipo,String asCodSistema,String  asIdentificador)
    //{
    //    DataSet aoDs;
    //    string lsSql = "";
    //    try
    //    {
    //        //' Recupera datos.
    //        lsSql = 
    //            "select count(*) " +
    //            "from " + msVM + " " +
    //            "where " +
    //            "idestado <> 3                    and " +
    //            "idsistemas = " + asCodSistema + " and " +
    //            "tipo_msg    ='" + asTipo + "'      and " +
    //            "identif     = " + asIdentificador;
    //        con = bd.fnGetConn();
    //        string lsAux = bd.ExecuteScalar(con, lsSql);
    //        con.Close();

    //        if(Convert.ToInt32(lsAux) == 0) return null;

    //       aoDs = new DataSet();
    //        lsSql = 
    //            "select top 1 msg as mensaje , fecha , usuario as operador , 0 as cantidad " +
    //            "from " + msVM + " " +
    //            "where " +
    //                "idestado <> 3                    and " +
    //                "idsistemas  = " + asCodSistema + " and " +
    //                "tipo_msg    ='" + asTipo + "'      and " +
    //                "identif     = " + asIdentificador + " " +
    //            "order by fecha desc";

    //        con = bd.fnGetConn();
    //        aoDs = bd.Fill(con, lsSql);
    //        con.Close();

    //        aoDs.Tables[0].Rows[0]["cantidad"] = lsAux;
    //    }
    //    catch(Exception ex)
    //    {
    //       return null;
    //    }

    //    return aoDs;
    //}

}