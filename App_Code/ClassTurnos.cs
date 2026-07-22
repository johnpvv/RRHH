using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


/// <summary>
/// Descripción breve de ClassTurnos
/// </summary>
public class ClassTurnos
{
    modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    public ClassTurnos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string ls_codigo { get; set; }
    public string ls_turno { get; set; }
    public string ls_descrip { get; set; }

    public DataSet mfBuscarTurnos()
    {
        string lsSql;
        string lsWhe = "";
        DataSet ds;

        if (ls_descrip != "")
        {
            lsWhe += " AND T.DESCRIPCION LIKE '%" + ls_descrip + "%'";
        }

        lsSql =
            "SELECT " +
            "T.IDTURNOS, " +
            "T.DESCRIPCION, " +
            "CASE " +
            "   WHEN T.IDESTADO = 1 THEN 'ACTIVO' " +
            "   WHEN T.IDESTADO = 3 THEN 'INACTIVO' " +
            "   ELSE 'S/E' " +
            "END ESTADO, " +
            "T.F_H_CREACION, " +
            "T.CODIGO " +
            "FROM " + modConstantes.gsDbRH + "M_TURNOS T " +
            "WHERE 1=1 " +
            lsWhe +
            " ORDER BY T.IDTURNOS DESC";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }
}