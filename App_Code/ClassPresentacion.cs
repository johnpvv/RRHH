using System.Data;


/// <summary>
/// Descripción breve de ClassPresentacion
/// </summary>
public class ClassPresentacion
{
    // Declaracion de Variables
    public string ls_nomb { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public ClassPresentacion()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfDDlPresentacion()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDPRESENTACION CODIGO, DESCRIPCION FROM " + modConstantes.gsDbAB + "M_PRESENTACION WHERE IDESTADO <> 3";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}