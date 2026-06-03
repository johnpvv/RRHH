using System;

/// <summary>
/// Descripción breve de ClassEstadisticas
/// </summary>
public class ClassEstadisticas
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    // Declaración  de Variables
    public string lsAcceso { get; set; }

    public ClassEstadisticas()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public string mfExisteAuditoria(string asMes, string asAnio, string asBod)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "SELECT COUNT(1) " +
                    "FROM " + modConstantes.gsDbAB + "V_AUDIT_CAB " +
                    "WHERE ANIO =  " + asAnio + " " +
                    "AND MES =  " + asMes + " " +
                    "AND IDBODPRERIF = " + asBod;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "S/N";
        }
        return asNombre;
    }

    public string mfExisteDetalleAuditoria(string asAnio, string asBod)
    {
        String asNombre;
        try
        {
            String lsSql = "";
            //Dim loObj As New jaxData.dbExecute
            lsSql = "SELECT COUNT(1) " +
                    "FROM " + modConstantes.gsDbAB + "V_AUDITORIA_GRAL_1 " +
                    "WHERE ANIO =  " + asAnio + " " +
                    "AND IDBODPRERIF = " + asBod;

            con = bd.fnGetConn();
            asNombre = bd.ExecuteScalar(con, lsSql);
            con.Close();


        }
        catch (Exception ex)
        {
            asNombre = "S/N";
        }
        return asNombre;
    }

    public string mfInsAuditCab(string asMes, string asAnio, string asUsr, string asObs, string asIdBod)
    {
        string lsRet = "";


        string lsSql = "INSERT INTO " + modConstantes.gsDbPer + "M_AUDIT_CAB(MES, ANIO, FECHA_CREACION, ESTADO, USUARIO, OBSERVACION, IDBODPRERIF) " +
                       " VALUES(" + asMes + ", " + asAnio + ", GETDATE(), 1, " + asUsr + ", '" + asObs + "', " + asIdBod + ") ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfDeleteAuditCab(string asMes, string asAnio, string asIdBod)
    {
        string lsRet = "";


        string lsSql = "DELETE FROM " + modConstantes.gsDbPer + "M_AUDIT_CAB " +
                        "WHERE ANIO =  " + asAnio + " " +
                        "AND MES =  " + asMes + " " +
                        "AND IDBODPRERIF = " + asIdBod;

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }
}