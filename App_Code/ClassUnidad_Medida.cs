using System.Data;

/// <summary>
/// Descripción breve de ClassUnidad_Medida
/// </summary>
public class ClassUnidad_Medida
{
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassUnidad_Medida()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfUnidadDDl()
    {
        DataSet aoCod;

        string lsSql;

        lsSql = "SELECT IDPRESENTACION, DESCRIPCION " +
                "FROM [dbo].[M_PRESENTACION] " +
                "WHERE IDESTADO <> 3 " +
                "order by DESCRIPCION";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }


    public DataSet mfUnidadMinimaDDl()
    {
        DataSet aoCod;

        string lsSql;

        lsSql = "SELECT IDUNMIN, DESCRIPCION " +
                "FROM [dbo].[M_UNIDAD_MINIMA] " +
                "WHERE IDESTADO <> 3 " +
                "order by DESCRIPCION";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}