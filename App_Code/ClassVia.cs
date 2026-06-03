using System.Data;



/// <summary>
/// Descripción breve de ClassVia
/// </summary>
public class ClassVia
{
    // Declaracion de Variables
    public string ls_nomb { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    public ClassVia()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfDDlVia()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDVIA CODIGO, DESCRIPCION FROM " + modConstantes.gsDbAB + "M_VIA WHERE IDESTADO <> 3";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet mfDDlViaArt(string id)
    {
        // Si corresponde agrega bodega.

        //try
        //{
        //    con = bd.fnGetConn();
        //    //Indico el SP que voy a utilizar
        //    SqlCommand command = new SqlCommand("uspListaVias", con);
        //    command.CommandType = CommandType.StoredProcedure;

        //    SqlDataAdapter adapter = new SqlDataAdapter(command);

        //    //Envió los parámetros que necesito
        //    SqlParameter paramd = new SqlParameter("@idarticulo", SqlDbType.Int);
        //    paramd.Value = id;
        //    command.Parameters.Add(paramd);

        //    command.CommandTimeout = 1200;

        //    DataSet dt = new DataSet();

        //    //Aquí ejecuto el SP y lo lleno en el DataTable
        //    adapter.Fill(dt);
        //    con.Close();

        //    ////Enlazo mis datos obtenidos en el DataTable con el grid
        //    //dataGridView1.DataSource = dt;
        //    return dt;
        //}
        //catch (Exception ex)
        //{
        //    return null;
        //}

        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT A.IDVIA CODIGO, A.DESCRIPCION " +
            "FROM " + modConstantes.gsDbAB + "M_VIA A " +
            "INNER JOIN M_ART_VIA B ON B.IDVIA = A.IDVIA " +
            " WHERE B.IDARTICULO = " + id + " " +
            "and A.IDESTADO <> 3 ";


        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}