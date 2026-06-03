using System.Data;


/// <summary>
/// Descripción breve de ClassRango
/// </summary>
public class ClassRango
{
    // Declaracion de Variables
    public string ls_nomb { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    public ClassRango()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfDDlRango()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDRANGO CODIGO, DESCRIPCION FROM " + modConstantes.gsDbAB + "M_RANGO WHERE IDESTADO <> 3";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}