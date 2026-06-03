using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassModSys
/// </summary>
public class ClassModSys
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    modFunciones func = new modFunciones();

    // Declaración  de Variables
    public string lsRut { get; set; }
    public string lsDv { get; set; }
    public string lsElim { get; set; }
    public string lsNombre { get; set; }
    public string lsPaterno { get; set; }
    public string lsMaterno { get; set; }

    public string lsEmail { get; set; }
    public string lsTelefono { get; set; }
    public string lsObs { get; set; }
    public string lsHosp { get; set; }

    public string lsBod { get; set; }
    public string lsUniOp { get; set; }
    public string lsEspec { get; set; }

    public string lsMed { get; set; }
    public string lsFarm { get; set; }
    public string lsInfect { get; set; }
    public string lsSal { get; set; }

    public ClassModSys()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    #region Usuarios
    public DataSet mfBuscarUsuario()
    {
        string lsSql = "";
        DataSet ds;

        if (lsNombre == "" && lsRut == "")
        {
            lsNombre = "%";
        }


        // Armar SQL
        string lsWhe = "";


        // Criterios propios de Comite.

        if (lsNombre != "")
        {
            lsWhe = lsWhe + "and REPLACE(LTRIM(RTRIM(nombre)), ' ','') COLLATE Modern_Spanish_CI_AI like '%" + lsNombre + "%' COLLATE Modern_Spanish_CI_AI ";
        }


        if (lsRut != "")
        {
            lsWhe = lsWhe + " and rut like '%" + lsRut + "%'";
        }


        if (lsElim == "3")
        {
            lsWhe = lsWhe + " and idestado = 3 ";
        }
        else
        {
            lsWhe = lsWhe + " and idestado <> 3 ";
        }

        lsSql = "select idusuario, " +
                   "rut ,  " +
                   "LTRIM(RTRIM(nombre))  as nombre ,  " +
                   "LTRIM(RTRIM(email))  as email   " +
                  "from " + modConstantes.gsDbAB + "m_usuarios " +
                  "where  " +
                    "rut > 0 " + lsWhe + " " +
                    "order by nombre";


        //lsSql = "select idusuario , " +
        //           "rut                                ,  " +
        //           "ltrim(rtrim(nombre))   as nombre   ,  " +
        //           "ltrim(rtrim(usr))      as usr      ,  " +
        //           "ltrim(rtrim(email))    as email    ,  " +
        //           "ltrim(rtrim(rut_full)) as rut_full ,  " +
        //           "ltrim(rtrim(obs))      as obs      ,  " +
        //           "0 as num_rol , 0 as num_app  " +
        //          "from " + modConstantes.gsDbAB + "m_usuarios " +
        //          "where  " +
        //            "rut > 0 and  " + lsWhe + " " +
        //            "order by nombre";

        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }


    public DataSet ConsultarID(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select " +
                   "rut                                ,  " +
                   "ltrim(rtrim(nombre))       as nombre   ,  " +
                   "ltrim(rtrim(rut))           as rut ,  " +
                   "ltrim(rtrim(dv))            as dv ,  " +
                   "ltrim(rtrim(OBSERVACION))   as obs      ,  " +
                   "0 as num_rol , 0 as num_app  " +
                  "from " + modConstantes.gsDbAB + "m_usuarios " +
                  "where  " +
                    "rut > 0 and  " +
                    "rut = " + lsIdentificador + " " +
                    "order by nombre";

        //lsSql = "select idusuario , " +
        //           "rut                                ,  " +
        //           "ltrim(rtrim(nombres))   as nombres   ,  " +
        //           "ltrim(rtrim(apelpat))   as apelpat   ,  " +
        //           "ltrim(rtrim(apelmat))   as apelmat   ,  " +
        //           "ltrim(rtrim(usr))       as usr      ,  " +
        //           "ltrim(rtrim(email))     as email    ,  " +
        //           "ltrim(rtrim(rut))       as rut ,  " +
        //           "ltrim(rtrim(dv))        as dv ,  " +
        //           "ltrim(rtrim(obs))       as obs      ,  " +
        //           "0 as num_rol , 0 as num_app  " +
        //          "from v_cns_usr  " +
        //          "where  " +
        //            "idestado <> 3 and  " +
        //            "idusuario = " + lsIdentificador + " " +
        //            "order by nombre";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public DataSet GetUsuarioRut(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select " +
                   "rut                                ,  " +
                   "ltrim(rtrim(nombre))       as nombre   ,  " +
                   "ltrim(rtrim(rut))           as rut ,  " +
                   "ltrim(rtrim(dv))            as dv ,  " +
                   "ltrim(rtrim(email))         as email ,  " +
                   "isnull(telefono,0)          as telefono ,  " +
                   "ltrim(rtrim(OBSERVACION))   as obs      ,  " +
                   "isnull(CODUNIOP,0)         as CODUNIOP      ,  " +
                   "isnull(IDBODEGA,0)         as IDBODEGA      ,  " +
                   "isnull(IDESTADO,0)         as IDESTADO      ,  " +
                   "isnull(MEDICO,0)           as MEDICO      ,  " +
                   "isnull(FARMACIA,0)         as FARMACIA      ,  " +
                   "isnull(INFECTOLOGIA,0)     as INFECTOLOGIA      ,  " +
                   "isnull(SALUD,0)             as SALUD      ,  " +
                   "isnull(ESPECIALIDAD,'S/E')         as ESPECIALIDAD      ,  " +
                   "0 as num_rol , 0 as num_app  " +
                  "from " + modConstantes.gsDbAB + "m_usuarios " +
                  "where  " +
                    "idusuario > 0 and  " +
                    "idusuario = " + lsIdentificador + " " +
                    "order by nombre";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfGuardar(string asIdentificador, Boolean lbNvo)
    {
        string lsRet = "";
        string lsSql;
        con = bd.fnGetConn();


        //if (lbNvo == false)
        if (lsNombre == "") return "Debe ingresar Nombre";
        if (lsRut == "") return "Debe ingresar Rut";
        if (lsDv == "") return "Debe ingresar DV";
        //if (lsUniOp == "0") return "Debe ingresar Unidad Operativa";


        if (lbNvo == true)
        {

            lsSql = "insert into " + modConstantes.gsDbAB + "m_usuarios (rut," +
                                            "id_inst," +
                                            "dv," +
                                            "nombre," +
                                            "telefono, " +
                                            "email, " +
                                            "idestado, " +
                                            "passwd, " +
                                            "IDBODEGA, " +
                                            "ESPECIALIDAD, " +
                                            "MEDICO, " +
                                            "FARMACIA, " +
                                            "INFECTOLOGIA, " +
                                            "SALUD, " +
                                            "observacion) " +
                    "Values(" + lsRut + ", " +
                    " " + lsHosp + "," +
                    " '" + lsDv + "'," +
                    " '" + lsNombre + "', " +
                    " '" + lsTelefono + "', " +
                    " '" + lsEmail + "', " +
                    " 1, " +
                    " '" + modFunciones.Encriptar("1234") + "', " +
                    " " + lsBod + ", " +
                    " '" + lsEspec + "', " +
                    " " + lsMed + ", " +
                    " " + lsFarm + ", " +
                    " " + lsInfect + ", " +
                    " " + lsSal + ", " +
                    " '" + lsObs + "' " +
                    ")";

            lsRet = bd.EjecutarComando(con, lsSql);
        }
        else
        {
            lsSql = "update " + modConstantes.gsDbAB + "m_usuarios set " +
                    "nombre = '" + lsNombre + "', " +
                    "telefono = " + lsTelefono + ", " +
                    "email = '" + lsEmail + "', " +
                    "IDBODEGA = " + lsBod + ", " +
                    "CODUNIOP = " + lsUniOp + ", " +
                    "ESPECIALIDAD = '" + lsEspec + "', " +
                    "MEDICO = " + lsMed + ", " +
                    "FARMACIA = " + lsFarm + ", " +
                    "INFECTOLOGIA = " + lsInfect + ", " +
                    "SALUD = " + lsSal + ", " +
                    "observacion = '" + lsObs + "' " +
                    "where idusuario = " + asIdentificador;

            lsRet = bd.EjecutarComando(con, lsSql);
        }

        return lsRet;

    }



    public String ObtenerIDUsuario(string asRut, string asHops)
    {
        String aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select idusuario " +
                "from " + modConstantes.gsDbAB + "m_usuarios " +
                "where rut = " + asRut + " and id_inst = " + asHops;

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }
    #endregion
}