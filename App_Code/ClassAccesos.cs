using System;
using System.Data;

/// <summary>
/// Descripción breve de ClassAccesos
/// </summary>
public class ClassAccesos
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;
    modFunciones func = new modFunciones();

    // Declaración  de Variables
    public string lsAcceso { get; set; }
    public string lsIdEstado { get; set; }


    public ClassAccesos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataSet mfPermisosDDl()
    {
        DataSet aoCod;

        string lsSql;


        /** lsSql = " SELECT a.IDAPP, a.DESCRIPCION " +
                 "FROM " + modConstantes.gsDbAB + "TG_APPS a " +
                 "INNER JOIN " + modConstantes.gsDbAB + " M_USAPP u ON a.IDAPP = u.IDAPP " +
                 "INNER JOIN  " + modConstantes.gsDbAB + " ON u.IDUSUARIO = s.IDUSUARIO " +
                 "WHERE s.IDUSUARIO = " +
                 "AND B.IDUSUARIO = " + lsIdentificador + " " +
                 "AND B.ID_INST = " + lsIdInst + " " +
                 "order by a.DESCRIPCION ";
        */

        lsSql = " SELECT IDAPP, DESCRIPCION FROM TG_APPS";
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
          "select idapp , " +
          "ltrim(rtrim(codigo))      as codigo , " +
          "ltrim(rtrim(descripcion)) as descripcion , " +
          "ltrim(rtrim(obs))         as obs " +
          "from tg_apps " +
          "where " +
          " idestado <> 3 and " +
          "idapp  = " + lsIdentificador + " ";

        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;

    }

    public DataSet mfBuscar(string asApp, string asHosp, string lsIdEstado, string asCodUsuario, string asMachine)
    {
        string lsSql = "";
        string lsWhe = "";
        string lsEstado = "";
        DataSet aoDs;



        //asApp = fnSwapChar(Trim(asApp))
        if (asApp == "") asApp = "%"; // Return gsErrNoCrit

        if (lsIdEstado == "3")
            lsEstado = " idestado = 3 and ";
        else
            lsEstado = " idestado <> 3 and ";


        lsWhe = lsWhe + " and descripcion like '%" + asApp + "%' ";
        //' Recupera registros.
        lsSql =
          "select idapp , " +
          "ltrim(rtrim(codigo))      as codigo      , " +
          "ltrim(rtrim(descripcion)) as descripcion , " +
          "ltrim(rtrim(obs))         as obs         , " +
          "0 as num_usr , 0 as num_rol " +
          "from tg_apps " +
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
            lsUsX += dr["idapp"] + ",";
        }


        lsUsX = func.fnSinComaFinal(lsUsX);

        //' Agrega cantidad de usuarios.
        lsSql =
          "select idapp,count(*) as cant " +
          "from m_usapp " +
          "where " +
          " idestado <> 3 and " +
          " id_inst = " + asHosp + " and " +
          " idapp in(" + lsUsX + ") " +
          " and idusuario not in(select idusuario from m_usuarios where idestado = 3 ) " +
          "group by idapp";


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
                    int liUsr1 = Convert.ToInt32(dr1["idapp"]);
                    int liCan = Convert.ToInt32(dr1["cant"]);

                    foreach (DataRow dr2 in aoDs.Tables[0].Rows)
                    {
                        int liUsr2 = Convert.ToInt32(dr2["idapp"]);

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
          "select idapp,count(*) as cant " +
          "from m_rolapp " +
          "where " +
          " idestado <> 3 and " +
          " id_inst = " + asHosp + " and " +
          " idapp in(" + lsUsX + ") " +
          " and idrol not in(select idrol from tg_roles where idestado = 3 ) " +
          "group by idapp ";


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
                    int liUsr1 = Convert.ToInt32(dr1["idapp"]);
                    int liCan = Convert.ToInt32(dr1["cant"]);
                    foreach (DataRow dr2 in aoDs.Tables[0].Rows)
                    {
                        int liUsr2 = Convert.ToInt32(dr2["idapp"]);
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

    public string mfGuardar(string asIdentificador, string asCodigo, string asNombre, string asObs, string asHosp, string asCodUsuario, string asMachine, Boolean lbNvo)
    {
        string lsRet = "";
        string lsSql = "";
        string cont = "";

        if (asHosp == "") return "Debe ingresar Código";
        if (asNombre == "") return "Debe ingresar Nombre";





        //' Verifica que no repita registro.
        string lsAux = "";

        if (!lbNvo)
        {
            lsAux = " and idapp <> " + asIdentificador;
        }

        lsSql =
              "select isNull( count(*) , 0 ) " +
              "from tg_apps " +
              "where IDESTADO <> 3 and " +
              "(" +
              " codigo      = '" + asCodigo + "' or " +
              " descripcion = '" + asNombre + "' " +
              ") " + lsAux + " and " +
              " id_inst = " + asHosp + " ";

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
              "insert into tg_apps(" +
                "codigo,descripcion,obs,id_inst,iduscrea) " +
              "values(" +
                "'" + asCodigo.Trim() + "'   , " +
                "'" + asNombre + "'   , " +
                "'" + asObs + "'      , " +
                "" + asHosp + " , " +
                "" + asCodUsuario + " )";

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

        }
        else
        {
            lsSql =
              "update tg_apps SET " +
              "  codigo            = '" + asCodigo.Trim() + "'   , " +
              "  descripcion       = '" + asNombre + "'   , " +
              "  obs               = '" + asObs + "'      , " +
              "  idusmod           = " + asCodUsuario + " , " +
              "  f_h_modif         = getdate()              " +
              "where idapp = " + asIdentificador;

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
        lsSql = "select Max(idapp) " +
                "from tg_apps ";

        con = bd.fnGetConn();
        aoCod = bd.ExecuteScalar(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfEliminar(string asIdentificador)
    {
        string lsRet = "";


        string lsSql = "update tg_apps SET " +
                              "idestado = 3 " +
                              "where  idapp = " + asIdentificador + " ";
        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }

    public string mfRehabilitar(string asIdentificador)
    {
        string lsRet = "";


        string lsSql = "update tg_apps SET " +
                              "idestado = 1 " +
                              "where  idapp = " + asIdentificador + " ";
        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();
        return lsRet;
    }


    public DataSet jaxListaRoles(string asHosp, string asCodApp)
    {

        DataSet aoDsSel;
        string lsSql = "";


        //' Recupera Roles asociados.

        lsSql =
          "select a.idrolapp , a.idrol , a.permiso , ltrim(rtrim(b.descripcion)) as descripcion " +
          "from m_rolapp a " +
          "inner join tg_roles b " +
          "on a.idrol = b.idrol and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idapp = " + asCodApp + " and " +
          "a.id_inst = " + asHosp + " " +
          "order by b.descripcion";

        con = bd.fnGetConn();
        aoDsSel = bd.Fill(con, lsSql);
        con.Close();

        return aoDsSel;
    }

    public DataSet jaxListaRolesDisp(string asHosp, string asCodApp)
    {

        DataSet aoDsSel;
        string lsSql = "";


        //' Recupera Roles asociados.

        lsSql =
          "select a.idrolapp , a.idrol , a.permiso , ltrim(rtrim(b.descripcion)) as descripcion " +
          "from m_rolapp a " +
          "inner join tg_roles b " +
          "on a.idrol = b.idrol and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idapp = " + asCodApp + " and " +
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
            lsNoT += dr["idrol"] + ",";
        }

        lsNoT = func.fnSinComaFinal(lsNoT);

        if (lsNoT != "") lsNoT = " and idrol not in(" + lsNoT + ") ";


        lsSql =
          "select idrol , ltrim(rtrim(descripcion)) as descripcion " +
          "from tg_roles " +
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
          "select a.idusapp , a.idusuario , a.permiso , ltrim(rtrim(b.nombre)) as descripcion " +
          "from m_usapp a " +
          "inner join m_usuarios b " +
          "on a.idusuario = b.idusuario and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idapp = " + asCodApp + " and " +
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
          "select a.idusapp , a.idusuario , a.permiso , ltrim(rtrim(b.nombre)) as descripcion " +
          "from m_usapp a " +
          "inner join m_usuarios b " +
          "on a.idusuario = b.idusuario and " +
          "   b.idestado <> 3 " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idapp = " + asCodApp + " and " +
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



    public string jaxAgregarRol(string asIdentificador, string asRoles, string asPermiso, string asHosp, string asCodUsuario, string asMachine)
    {
        string lsRet = "";
        string lsSql = "";


        if (Convert.ToInt32(asIdentificador) <= 0) return "Debe seleccionar Acceso.";
        if (asRoles == "") return "Debe seleccionar Rol para asociar.";

        asPermiso = asPermiso.Trim().ToUpper();

        if (asPermiso != "M") asPermiso = "L";


        string alSalida = "0";
        //        ' Verifica si la Aplicación está asociada al Rol.
        lsSql =
          "select isNull(a.llID,0) " +
          "from m_rolapp a " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idapp = " + asIdentificador + " and " +
          "a.idrol = " + asRoles + " and " +
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
              "idapp,id_inst,idrol,permiso,iduscrea) " +
              "values(" +
              asIdentificador + " , " +
              asHosp + " , " +
              asRoles + " , " +
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
            "delete from m_rolapp  " +
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

        //            ' Verifica si la Aplicación está asociada al Usuario.
        lsSql =
          "select isNull(a.idusapp,0) " +
          "from m_usapp a " +
          "where " +
          "a.idestado <> 3 and " +
          "a.idusuario = " + asUsuarios + " and " +
          "a.idapp = " + asIdentificador + " and " +
          "a.id_inst = " + asHosp + " ";

        con = bd.fnGetConn();
        asIdusapp = bd.ExecuteScalar(con, lsSql);
        con.Close();

        if (asIdusapp == "") asIdusapp = "0";

        if (Convert.ToInt32(asIdusapp) > 0)
        {
            //                ' Modifica permiso.
            lsSql =
              "update m_usapp SET " +
              " permiso = '" + asPermiso + "' , " +
              " idusmod = " + asCodUsuario + " , " +
              " f_h_modif = getdate() " +
            " where idusapp = " + asIdusapp;

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

            //                ' Audita movimiento.
            //                lsSql = fnABDSqlAudgral(asCodSistema, "modappusr", lsRet, asCodUsuario, asMachine, "Permiso: " & asPermiso)
            //                loColSql.Add(lsSql)

        }
        else
        {
            //                ' Agrega registros de Aplicaciones de Rol.
            lsSql =
              "insert into m_usapp(" +
                "idapp," +
                "id_inst," +
                "idusuario," +
                "permiso," +
                "iduscrea) " +
              "values(" +
                asIdentificador + " , " +
                asHosp + " , " +
                asUsuarios + " , " +
                "'" + asPermiso + "' , " +
                asCodUsuario + " ) ";

            con = bd.fnGetConn();
            lsRet = bd.EjecutarComando(con, lsSql);
            con.Close();

            //                ' Audita movimiento.
            //                lsSql = fnABDSqlAudgral(asCodSistema, "agrappusr", asIdentificador, asCodUsuario, asMachine, "Usr: " & llID & ". Permiso: " & asPermiso)
            //                loColSql.Add(lsSql)

        }


        return lsRet;
    }

    public string jaxEliminarUsuario(string asIdentificador, string asHosp, string asCodUsuario, string asMachine)
    {
        string lsRet = "";
        string lsSql = "";

        //' Elimina registros de Aplicaciones de Usuarios.
        //lsSql =
        //  "update m_usapp SET " +
        //  " idestado = 3         ," +
        //  " f_h_eli  = getdate() , " +
        //  " iduseli  = " + asCodUsuario + " " +
        //  "where idusapp = " + asIdentificador + " ";

        lsSql =
          "delete from m_usapp  " +
          "where idusapp = " + asIdentificador + " ";

        con = bd.fnGetConn();
        lsRet = bd.EjecutarComando(con, lsSql);
        con.Close();

        //            ' Audita movimiento.
        //            lsSql = fnABDSqlAudgral(asCodSistema, "eliappusr", llID, asCodUsuario, asMachine, "")
        //            loColSql.Add(lsSql)



        return lsRet;
    }

}