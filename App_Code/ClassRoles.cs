using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassRoles
/// </summary>
public class ClassRoles
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    modFunciones func = new modFunciones();

    // Declaración  de Variables
    public string lsRol { get; set; }
    public string lsIdEstado { get; set; }

    public ClassRoles()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfRolDdl(string lsIdentificador, string lsIdInst)
    {


        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "SELECT IDROL, DESCRIPCION " +
                "FROM TG_ROLES " +
                "WHERE IDESTADO <> 3 " +
                // "AND u.IDUSUARIO = " + lsIdentificador + " " +
                //"AND u.ID_INST = " + lsIdInst + " " +
                "order by DESCRIPCION ";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;

    }

    public DataSet ConsultarID(string lsIdentificador, string asCodSistema, string asCodUsuario, string asMachine)
    {
        DataSet aoCod;

        string lsSql;

        //' Recupera registros.
        lsSql =
          "select idrol , " +
          "ltrim(rtrim(codigo))      as codigo , " +
          "ltrim(rtrim(descripcion)) as descripcion , " +
          "ltrim(rtrim(obs))         as obs " +
          "from tg_roles " +
          "where " +
          " idestado <> 3 and " +
          "idrol  = " + lsIdentificador + " ";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;

    }

    public DataSet mfBuscar(string asRol, string asHosp, string lsIdEstado, string asCodUsuario, string asMachine)
    {
        string lsSql = "";
        string lsWhe = " and descripcion like '%" + asRol + "%' ";
        string lsEstado = "";
        DataSet aoDs;



        //asApp = fnSwapChar(Trim(asApp))
        if (asRol == "") lsWhe = ""; // Return gsErrNoCrit

        if (lsIdEstado == "3")
            lsEstado = " idestado = 3 and ";
        else
            lsEstado = " idestado <> 3 and ";


        //' Recupera registros.
        lsSql =
          "select idrol , " +
          "ltrim(rtrim(codigo))      as codigo      , " +
          "ltrim(rtrim(descripcion)) as descripcion , " +
          "ltrim(rtrim(obs))         as obs         , " +
          "0 as num_usr , 0 as num_rol " +
          "from tg_roles " +
          "where " +
          lsEstado +
          " id_inst = " + asHosp + " " +
           " " + lsWhe + " " +
          "order by descripcion";

        con = bd.fnGetConn();
        aoDs = bd.Fill(con, lsSql);
        con.Close();


        if (aoDs.Tables.Count <= 0) return null;
        if (aoDs.Tables[0].Rows.Count <= 0) return null;

        //' Obtiene ID de roles.
        String lsUsX = "";
        foreach (DataRow dr in aoDs.Tables[0].Rows)
        {
            lsUsX += dr["idrol"] + ",";
        }


        lsUsX = func.fnSinComaFinal(lsUsX);

        //' Agrega cantidad de usuarios.
        lsSql =
          "select idrol,count(*) as cant " +
          "from m_usrol " +
          "where " +
          " idestado <> 3 and " +
          " id_inst = " + asHosp + " and " +
          " idrol in(" + lsUsX + ") " +
          " and idusuario not in(select idusuario from m_usuarios where idestado = 3 ) " +
          "group by idrol";


        DataSet loDsU;
        con = bd.fnGetConn();
        loDsU = bd.Fill(con, lsSql);
        con.Close();

        if (aoDs.Tables.Count > 0)
        {
            if (aoDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in loDsU.Tables[0].Rows)
                {
                    int liUsr1 = Convert.ToInt32(dr1["idrol"]);
                    int liCan = Convert.ToInt32(dr1["cant"]);

                    foreach (DataRow dr2 in aoDs.Tables[0].Rows)
                    {
                        int liUsr2 = Convert.ToInt32(dr2["idrol"]);

                        if (liUsr1 == liUsr2)
                        {
                            dr2["num_usr"] = liCan;
                            break;
                        }
                    }
                }
            }

        }




        //' Agrega cantidad de roles.

        lsSql =
          "select idrol,count(*) as cant " +
          "from m_rolapp " +
          "where " +
          " idestado <> 3 and " +
          " id_inst = " + asHosp + " and " +
          " idrol in(" + lsUsX + ") " +
          " and idrol not in(select idrol from tg_apps where idestado = 3 ) " +
          "group by idrol ";


        DataSet loDsA;

        con = bd.fnGetConn();
        loDsA = bd.Fill(con, lsSql);
        con.Close();

        if (loDsA.Tables.Count > 0)
        {
            if (loDsA.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in loDsA.Tables[0].Rows)
                {
                    int liUsr1 = Convert.ToInt32(dr1["idrol"]);
                    int liCan = Convert.ToInt32(dr1["cant"]);
                    foreach (DataRow dr2 in aoDs.Tables[0].Rows)
                    {
                        int liUsr2 = Convert.ToInt32(dr2["idrol"]);
                        if (liUsr1 == liUsr2)
                        {
                            dr2["num_rol"] = liCan;
                            break;
                        }

                    }
                }
            }

        }


        //' Audita consulta.
        //asCodSistema = fnLong(asCodSistema)
        //asCodUsuario = fnLong(asCodUsuario)
        //lsSql = fnABDSqlAudgral(asCodSistema, "cnsApp", 0, asCodUsuario, asMachine, lsSql)
        //lsRet = loObj.jaxEjecutarSql(lsSql)
        return aoDs;
    }

    public string mfGuardar(string asIdentificador, string asCodigo, string asNombre, string asObs, string asIdHosp, string asCodUsuario, string asMachine, Boolean lbNvo)
    {
        string lsRet = "";
        string lsSql = "";
        string cont = "";

        if (asIdHosp == "") return "Debe ingresar Código";
        if (asNombre == "") return "Debe ingresar Nombre";





        //' Verifica que no repita registro.
        string lsAux = "";

        if (!lbNvo)
        {
            lsAux = " and idrol <> " + asIdentificador;
        }

        lsSql =
              "select isNull( count(*) , 0 ) " +
              "from tg_roles " +
              "where IDESTADO <> 3 and " +
              "(" +
              " codigo      = '" + asCodigo + "' or " +
              " descripcion = '" + asNombre + "' " +
              ") " + lsAux + " and " +
              " id_inst = " + asIdHosp + " ";

        con = bd.fnGetConn();
        cont = bd.ExecuteScalar(con, lsSql);
        con.Close();

        if (Convert.ToInt32(cont) > 0)
        {
            return "El Registro ya existe. ";
        }


        if (lbNvo)
        {
            //' Si corresponde, crea nuevo registro.
            lsSql =
              "insert into tg_roles(" +
                "codigo,descripcion,obs,id_inst,iduscrea) " +
              "values(" +
                "'" + asCodigo.Trim() + "'   , " +
                "'" + asNombre + "'   , " +
                "'" + asObs + "'      , " +
                "" + asIdHosp + " , " +
                "" + asCodUsuario + " )";

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

        }
        else
        {
            lsSql =
              "update tg_roles SET " +
              "  codigo            = '" + asCodigo.Trim() + "'   , " +
              "  descripcion       = '" + asNombre + "'   , " +
              "  obs               = '" + asObs + "'      , " +
              "  idusmod           = " + asCodUsuario + " , " +
              "  f_h_modif         = getdate()              " +
              "where idrol = " + asIdentificador;

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

        }

        //If lsRet = "" Then
        //    If lbNvo Then
        //        asIdentificador = loObj.jaxEjecutarEscalar("select max( " & msPkM & " ) from " & msTM & " where iduscrea = " & asCodUsuario)
        //    End If
        //End If


        return lsRet;

    }

    public String ConsultarMaxID()
    {
        String aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select Max(idrol) " +
                "from tg_roles ";

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfEliminar(string asIdentificador)
    {
        string lsRet = "";


        string lsSql = "update tg_roles SET " +
                              "idestado = 3 " +
                              "where  idrol = " + asIdentificador + " ";
        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfRehabilitar(string asIdentificador)
    {
        string lsRet = "";


        string lsSql = "update tg_roles SET " +
                              "idestado = 1 " +
                              "where  idrol = " + asIdentificador + " ";
        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }



    public DataSet jaxListaAcceso(string asHosp, string asCodRol)
    {

        DataSet aoDsSel;
        string lsSql = "";


        //' Recupera Roles asociados.

        lsSql =
          "select a.idrolapp , a.idapp , a.permiso , ltrim(rtrim(b.descripcion)) as descripcion " +
          "from m_rolapp a " +
          "inner join tg_apps b " +
          "on a.idapp = b.idapp and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idrol = " + asCodRol + " and " +
          "a.id_inst = " + asHosp + " " +
          "order by b.descripcion";

        con = bd.fnGetConn();
        aoDsSel = bd.Fill(con, lsSql);
        con.Close();

        return aoDsSel;
    }

    public DataSet jaxListaAccesoDisp(string asHosp, string asCodRol)
    {

        DataSet aoDsSel;
        string lsSql = "";


        //' Recupera Roles asociados.

        lsSql =
          "select a.idrolapp , a.idapp , a.permiso , ltrim(rtrim(b.descripcion)) as descripcion " +
          "from m_rolapp a " +
          "inner join tg_apps b " +
          "on a.idapp = b.idapp and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idrol = " + asCodRol + " and " +
          "a.id_inst = " + asHosp + " " +
          "order by b.descripcion";

        con = bd.fnGetConn();
        aoDsSel = bd.Fill(con, lsSql);
        con.Close();


        //' Recupera roles disponibles.
        DataSet aoDsDisp;
        String lsNoT = "";


        foreach (DataRow dr in aoDsSel.Tables[0].Rows)
        {
            lsNoT += dr["idapp"] + ",";
        }

        if (lsNoT != "") lsNoT = " and idapp not in(" + lsNoT.Substring(0, lsNoT.Length - 1) + ") ";


        lsSql =
          "select idapp , ltrim(rtrim(descripcion)) as descripcion " +
          "from tg_apps " +
          "where idestado <> 3 " + lsNoT + " and " +
          " id_inst = " + asHosp + " " +
          "order by descripcion";

        con = bd.fnGetConn();
        aoDsDisp = bd.Fill(con, lsSql);
        con.Close();


        return aoDsDisp;
    }


    public DataSet jaxListaUsuarios(string asHosp, string asCodApp)
    {

        DataSet aoDsSel;
        string lsSql = "";


        //' Recupera Roles asociados.

        lsSql =
          "select a.idusrol , a.idusuario ,  ltrim(rtrim(b.nombre)) as descripcion " +
          "from m_usrol a " +
          "inner join m_usuarios b " +
          "on a.idusuario = b.idusuario and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idROL = " + asCodApp + " and " +
          "a.id_inst = " + asHosp + " " +
          "order by b.nombre";



        con = bd.fnGetConn();
        aoDsSel = bd.Fill(con, lsSql);
        con.Close();

        return aoDsSel;
    }

    public DataSet jaxListaUsuariosDisp(string asHosp, string asNombre, string asRut, string asCodApp)
    {

        DataSet aoDsSel;
        string lsSql = "";



        //' Recupera Roles asociados.

        lsSql =
          "select a.idusrol , a.idusuario , ltrim(rtrim(b.nombre)) as descripcion " +
          "from m_usrol a " +
          "inner join m_usuarios b " +
          "on a.idusuario = b.idusuario and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idROL = " + asCodApp + " and " +
          "a.id_inst = " + asHosp + " " +
          "order by b.nombre";

        con = bd.fnGetConn();
        aoDsSel = bd.Fill(con, lsSql);
        con.Close();



        //' Recupera roles disponibles.
        DataSet aoDsDisp;
        String lsNoT = "";


        foreach (DataRow dr in aoDsSel.Tables[0].Rows)
        {
            lsNoT += dr["idusuario"] + ",";
        }

        if (lsNoT != "") lsNoT = " and usr.idusuario not in(" + lsNoT.Substring(0, lsNoT.Length - 1) + ") ";

        string ls_where = "";

        // Si se ingresó Fechas las agrega al WHERE.
        if (asNombre != "")
            ls_where += " and usr.nombre like '%" + asNombre + "%'  ";


        // Si se ingresó Fechas las agrega al WHERE.
        if (asRut != "")
            ls_where += " and  usr.rut = " + asRut + "  ";


        lsSql =
               "select " +
               "usr.idusuario , " +
               "ltrim(rtrim(usr.nombre)) as descripcion " +
               "from m_usuarios usr " +
               "where usr.idestado <> 3 " + lsNoT + " " +
               "and id_inst = " + asHosp + " " +
               ls_where + " " +
               "order by usr.nombre";


        con = bd.fnGetConn();
        aoDsDisp = bd.Fill(con, lsSql);
        con.Close();


        return aoDsDisp;
    }



    public string jaxAgregarAplicacion(string asIdentificador, string asAPP, string asPermiso, string asHosp, string asCodUsuario, string asMachine)
    {
        string lsRet = "";
        string lsSql = "";


        if (Convert.ToInt32(asIdentificador) <= 0) return "Debe seleccionar Rol.";
        if (asAPP == "") return "Debe seleccionar Aplicación para asociar.";

        asPermiso = asPermiso.Trim().ToUpper();

        if (asPermiso != "M") asPermiso = "L";


        string alSalida = "0";
        //        ' Verifica si la Aplicación está asociada al Rol.
        lsSql =
          "select isNull(a.idrolapp,0) " +
          "from m_rolapp a " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idrol = " + asIdentificador + " and " +
          "a.idapp = " + asAPP + " and " +
          "a.id_inst = " + asHosp + " ";

        con = bd.fnGetConn();
        alSalida = bd.ExecuteScalar(con, lsSql);
        con.Close();

        if (alSalida.Trim() == "") alSalida = "0";

        if (Convert.ToInt32(alSalida) > 0)
        {
            //' Modifica permiso.
            lsSql =
              "update m_rolapp SET " +
              "permiso = '" + asPermiso + "' , " +
              " idusmod = " + asCodUsuario + " , " +
              " f_h_modif = getdate() " +
            "where idrolapp = " + alSalida;

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

            if (lsRet != "") return "Problemas al ingresar registro... ";



            //            ' Audita movimiento.
            //            lsSql = fnABDSqlAudgral(asCodSistema, "modapprol", lsRet, asCodUsuario, asMachine, "Permiso: " & asPermiso)
            //            loColSql.Add(lsSql)

        }
        else
        {
            //            ' Agrega registros de Aplicaciones de Rol.
            lsSql =
              "insert into m_rolapp(" +
              "idrol,id_inst,idapp,permiso,iduscrea) " +
              "values(" +
              asIdentificador + " , " +
              asHosp + " , " +
              asAPP + " , " +
              "'" + asPermiso + "' , " +
              asCodUsuario + " ) ";

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

            if (lsRet != "") return "Problemas al ingresar registro... ";


        }

        return lsRet;
    }


    public string jaxEliminarAplicacion(string asIdentificador, string asHosp, string asCodUsuario, string asMachine)
    {
        string lsRet = "";
        string lsSql = "";

        //' Elimina registros de Aplicaciones de Rol.
        //lsSql =
        //    "update m_rolapp SET " +
        //    " idestado = 3         ," +
        //    " f_h_eli  = getdate() , " +
        //    " iduseli  = " + asCodUsuario + " " +
        //    "where idrolapp = " + asIdentificador + " ";

        lsSql =
            "delete from  m_rolapp " +
            "where idrolapp = " + asIdentificador + " ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();

        //            ' Audita movimiento.
        //            lsSql = fnABDSqlAudgral(asCodSistema, "eliapprol", llID, asCodUsuario, asMachine, "")
        //            loColSql.Add(lsSql)



        return lsRet;
    }


    public string jaxAgregarUsuario(string asIdentificador, string asUsuarios, string asPermiso, string asHosp, string asCodUsuario, string asMachine)
    {
        string lsRet = "";
        string lsSql = "";
        string asIdusapp = "";


        asPermiso = asPermiso.Trim().ToUpper();
        if (asPermiso != "M") asPermiso = "L";

        //' Verifica si el Rol está asociado al usuario.
        lsSql =
          "select isNull(b.descripcion,'') " +
          "from m_usrol a " +
          "inner join tg_roles b " +
          "on a.idrol = b.idrol " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idrol = " + asIdentificador + " and " +
          "a.idusuario = " + asUsuarios + " and " +
          "a.id_inst = " + asHosp + " ";

        con = bd.fnGetConn();
        asIdusapp = bd.ExecuteScalar(con, lsSql);
        con.Close();

        if (asIdusapp != "") return "El Rol: " + asIdusapp + " ya está asociado al usuario.";

        //                ' Agrega registros de Aplicaciones de Rol.
        lsSql =
          "insert into m_usrol(" +
          "idrol,id_inst,idusuario ,iduscrea ) " +
          "values(" +
          asIdentificador + " , " +
          asHosp + " , " +
          asUsuarios + " , " +
          asCodUsuario + " ) ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();

        //                ' Audita movimiento.
        //                lsSql = fnABDSqlAudgral(asCodSistema, "agrappusr", asIdentificador, asCodUsuario, asMachine, "Usr: " & llID & ". Permiso: " & asPermiso)
        //                loColSql.Add(lsSql)

        return lsRet;
    }

    public string jaxEliminarUsuario(string asIdentificador, string asHosp, string asCodUsuario, string asMachine)
    {
        string lsRet = "";
        string lsSql = "";

        //' Elimina registros de Aplicaciones de Usuarios.
        //lsSql =
        //  "update m_usrol SET " +
        //  " idestado = 3         ," +
        //  " f_h_eli  = getdate() , " +
        //  " iduseli  = " + asCodUsuario + " " +
        //  "where idusrol = " + asIdentificador + " ";

        lsSql =
          "delete from  m_usrol  " +
          "where idusrol = " + asIdentificador + " ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();

        //            ' Audita movimiento.
        //            lsSql = fnABDSqlAudgral(asCodSistema, "eliappusr", llID, asCodUsuario, asMachine, "")
        //            loColSql.Add(lsSql)



        return lsRet;
    }

}