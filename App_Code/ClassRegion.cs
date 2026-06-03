using System.Data;

/// <summary>
/// Descripción breve de ClassRegion
/// </summary>
public class ClassRegion
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    public ClassRegion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfdllRegiones()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select IDREGION, DESCRIPCION from [dbo].[TG_REGION]";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}