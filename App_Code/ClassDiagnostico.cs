using System.Data;

/// <summary>
/// Descripción breve de ClassDiagnostico
/// </summary>
public class ClassDiagnostico
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    modFunciones func = new modFunciones();

    public ClassDiagnostico()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfddlDiagnostico()
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDDIAGNOSTICO, DESCRIPCION, IDESTADO " +
                "FROM TG_DIAGNOSTICOS " +
               " WHERE IDESTADO <> 3";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }
}