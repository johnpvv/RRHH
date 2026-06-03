using System.Data;

/// <summary>
/// Descripción breve de ClassComuna
/// </summary>
public class ClassComuna
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    public ClassComuna()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfdllComunas(string asRegion)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select  IDCOMUNA,  DESCRIPCION from [dbo].[TG_COMUNA] WHERE IDREGION = " + asRegion;
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}