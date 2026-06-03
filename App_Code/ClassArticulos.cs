using System;
using System.Data;


/// <summary>
/// Descripción breve de ClassArticulos
/// </summary>
public class ClassArticulos
{
    // Declaración  de Variables

    public string lsArt { get; set; }
    public string lsCod { get; set; }
    public string lsOnu { get; set; }
    public string lsSed { get; set; }
    public string lsBod { get; set; }
    public string lsRut { get; set; }
    public string lsUni { get; set; }

    public string lsUm { get; set; }
    public string lsCta { get; set; }
    public Boolean lsEli { get; set; }
    public string lsIDPRESENTACION { get; set; }
    public string lsCALCULAR { get; set; }
    public string lsMorbido { get; set; }
    public string lsMinima { get; set; }
    public string lsFactor { get; set; }
    public string lsIdArt { get; set; }

    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;


    public ClassArticulos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }



    public DataSet mfBuscarArtFarm(ClassArticulos Art)
    {
        string lsSql = "";
        DataSet ds;

        if (Art.lsArt == "" && Art.lsCod == "" && Art.lsBod == "0" &&
            Art.lsUm == "0"
            )
        {
            Art.lsArt = "%";
        }


        // Armar SQL
        string lsWhe = "";
        string lsInn = "";

        // Criterios propios de ARTICULO.

        if (Art.lsArt != "")
        {
            lsWhe = lsWhe + "and (m.CODARTICULO + m.descripcion_larga) like '%" + Art.lsArt + "%' ";
        }


        if (Art.lsCod != "")
        {
            lsWhe = lsWhe + " and m.CODARTICULO like '%" + Art.lsCod + "%'";
        }

        if (Art.lsBod != "0")
        {
            lsWhe = lsWhe + " and b.idbodega = " + Art.lsBod + "";
        }


        lsInn = " inner join " + modConstantes.gsDbAB + "M_ART_UNIDAD AU ON AU.IDARTICULO = M.IDARTICULO AND  AU.CODUNIOP = " + lsUni + " " +
                    "  inner join " + modConstantes.gsDbAB + "V_BOD_ART ab on ab.IDARTICULO=m.IDARTICULO " +
                      "  inner join " + modConstantes.gsDbAB + "v_bodegas b on b.idbodega=ab.idbodega ";


        lsSql = "select distinct m.IDARTICULO, b.IDBODEGA, m.IDUNIDAD_MEDIDA, m.CODARTICULO, m.DESCRIPCION_LARGA, " +
                        "m.IDESTADO, isnull(ab.SALDO,0) SALDO, m.DESCRIPCION_TECNICA, m.PRECIO_UNITARIO, m.PRECIO_VENTA, " +
                        "b.DESCRIPCION_LARGA as bodega, m.UN_MED " +
                        "from " + modConstantes.gsDbAB + "V_ARTICULOS m " +
                 lsInn + " " +
               "where  " +
                "m.idestado > 0 " +
                lsWhe + " " +
               "";


        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }

    public string mfGetFactor(string asId)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select m.factor  " +
                      "from " + modConstantes.gsDbAB + "v_articulos m " +
                      "where " +
                       "m.idarticulo = " + asId + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "1";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfCodcArt(string asCodigo)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select m.idarticulo  " +
                      "from " + modConstantes.gsDbAB + "v_articulos m " +
                      "where " +
                       "m.codarticulo = '" + asCodigo + "'";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "0";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }


    public string mfDescArt(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select ltrim(rtrim(m.descripcion_larga)) as descripcion  " +
                      "from " + modConstantes.gsDbAB + "V_ARTICULOS m " +
                      "where " +
                       "m.IDARTICULO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfExisteArtBodega(string asIdArt, string asIdBodega)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select count(1)  " +
                      "from " + modConstantes.gsDbAB + "V_BOD_ART " +
                      "where idarticulo = " + asIdArt + " " +
                       "and idbodega = " + asIdBodega;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfExisteArtBodegaPerif(string asIdArt, string asIdBodega)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select count(1)  " +
                      "from " + modConstantes.gsDbAB + "V_BOD_ART_PER " +
                      "where idarticulo = " + asIdArt + " " +
                       "and IDBODPRERIF = " + asIdBodega;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfSaldoArtBodega(string asIdArt, string asIdBodega)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select isnull(saldo,0)  " +
                      "from " + modConstantes.gsDbAB + "V_BOD_ART_PER " +
                      "where idestado <> 3 " +
                       "and idarticulo = " + asIdArt + " " +
                       "and IDBODPRERIF = " + asIdBodega;


            lsRet = bd.ExecuteScalar(con, lsSql);
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;

        }

        return lsRet;
    }

    public string mfUniMedArt(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select UN_MED  " +
                      "from " + modConstantes.gsDbAB + "V_ARTICULOS " +
                      "where " +
                       "IDARTICULO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "0";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfIdPresentacion(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select IDPRESENTACION  " +
                      "from " + modConstantes.gsDbAB + "V_ARTICULOS " +
                      "where " +
                       "IDARTICULO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "0";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfCalcDosis(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select ISNULL(CALCULAR,1)  " +
                      "from " + modConstantes.gsDbAB + "V_ARTICULOS " +
                      "where " +
                       "IDARTICULO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "1";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "1";

        }

        return lsRet;
    }

    public string mfIdUniMedArt(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select IDUNIDAD_MEDIDA  " +
                      "from " + modConstantes.gsDbAB + "V_ARTICULOS " +
                      "where " +
                       "IDARTICULO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "0";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public string mfIdMinimaArt(string asIdentificador)
    {
        string lsRet = "";

        con = bd.fnGetConn();
        try
        {
            string lsSql;
            lsSql = "select UNI_MIN  " +
                      "from " + modConstantes.gsDbAB + "V_ARTICULOS " +
                      "where " +
                       "IDARTICULO = " + asIdentificador + "";


            lsRet = bd.ExecuteScalar(con, lsSql);

            if (lsRet == "") lsRet = "0";
            con.Close();

        }
        catch (Exception e)
        {
            con.Close();
            lsRet = "0";

        }

        return lsRet;
    }

    public DataSet mfMantBuscar(ClassArticulos Art)
    {
        string lsSql = "";
        DataSet ds;

        if (Art.lsArt == "" && Art.lsCod == "" && Art.lsBod == "0" &&
            Art.lsUm == "0"
            )
        {
            Art.lsArt = "%";
        }


        // Armar SQL
        string lsWhe = "";
        string lsInn = "";

        // Criterios propios de ARTICULO.

        if (Art.lsArt != "")
        {
            lsWhe = lsWhe + "and m.descripcion_larga like '%" + Art.lsArt + "%' ";
        }


        if (Art.lsCod != "")
        {
            lsWhe = lsWhe + " and m.CODARTICULO like '%" + Art.lsCod + "%'";
        }

        lsInn = "  inner join " + modConstantes.gsDbAB + "V_BOD_ART ab on ab.IDARTICULO=m.IDARTICULO " +
                      "  inner join " + modConstantes.gsDbAB + "v_bodegas b on b.idbodega=ab.idbodega " +
                        " LEFT OUTER JOIN " + modConstantes.gsDbAB + "M_ART_VIA AV ON AV.IDARTICULO = m.IDARTICULO " +
                        " LEFT OUTER JOIN " + modConstantes.gsDbAB + "M_VIA VIA ON VIA.IDVIA = AV.IDVIA ";


        lsSql = "select distinct m.IDARTICULO, b.IDBODEGA, m.IDUNIDAD_MEDIDA, m.CODARTICULO, m.DESCRIPCION_LARGA, " +
                        "m.IDESTADO, isnull(ab.SALDO,0) SALDO, m.DESCRIPCION_TECNICA, m.PRECIO_UNITARIO, m.PRECIO_VENTA, " +
                        "b.DESCRIPCION_LARGA as bodega, m.UN_MED, m.UNI_MIN, VIA.DESCRIPCION VIA, " +
                        "CASE WHEN ISNULL(m.TIPO_RIESGO,0) = '0' THEN 'NO' ELSE 'SI' END TIPO_RIESGO, " +
                        "CASE WHEN ISNULL(m.CALCULAR,1) = '1' THEN 'SI' ELSE 'NO' END CALCULAR " +
                        "from " + modConstantes.gsDbAB + "V_ARTICULOS m " +
                 lsInn + " " +
               "where  " +
                "m.idestado > 0 " +
                lsWhe + " " +
               "";


        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }

    public DataSet mfGetDataPrecio(string lsIdentificador)
    {
        DataSet aoPre;


        string lsSql;

        // Recupera Vencimientos.
        lsSql = "select fecha,pant,pnvo,tipo,obs,operador " +
                "from " + modConstantes.gsDbAB + "v_cpr " +
                "where idarticulo = " + lsIdentificador + " " +
                "order by fecha desc";

        con = bd.fnGetConn();
        aoPre = bd.Fill(con, lsSql);
        con.Close();
        return aoPre;

    }

    public DataSet ConsultarID(string asIdentificador)
    {
        DataSet aoDs;
        string lsRet = "";


        con = bd.fnGetConn();

        try
        {
            string lsSql;

            lsSql = "select m.idarticulo    ," +
                    "m.idestado           ," +
                    " m.idbodega           ," +
                    "m.IDUNIDAD_MEDIDA          ," +
                     " m.DESCRIPCION_LARGA ," +
      " 0 as no_cobrable      ," +
          "space(10) as us_cod            ," +
          " 0.00 as us_uso            , " +
           "0.00 as us_est            ," +
            "0 as stock_maximo      ," +
            " 0 as saldo_inicial     ," +
             " 0 as cpm               ," +
               "isNull(m.saldo, 0) as existencia        ," +
                  "isNull(m.precio_unitario, 0) as precio_unit       ," +
                  " isNull(m.precio_unitario, 0) * 1.19 as precio_bruto      ," +
                    " 0 as ultima_oc         ," +
                      "isNull(m.precio_venta, 0) as precio_venta      ," +
                     "  1 as idsede            ," +
                      "  isNull(m.precio_venta, 0) / 1.19 as precio_venta_neto ," +
                        "  ltrim(rtrim(m.codarticulo)) as codigo            ," +
                          " ltrim(rtrim(m.descripcion_larga)) as descripcion       ," +
                             " '' as des_sed           , " +
                            " ltrim(rtrim(ab.NOMB_BOD)) as nom_bod     ," +
                              "  isnull(LOTE, 1) as LOTE        ," +
                                " isnull(IDCUENTA, 0) as IDCUENTA,  " +
                                " isnull(IDPRESENTACION, 0) as IDPRESENTACION,  " +
                                " isnull(IDUNMIN, 0) as IDUNMIN,  " +
                                " isnull(TIPO_RIESGO, 0) as TIPO_RIESGO,  " +
                                " isnull(CALCULAR, 1) as CALCULAR,  " +
                                " isnull(FACTOR, 1) as FACTOR  " +
                      "from " + modConstantes.gsDbAB + "v_articulos m  " +
                      " inner join " + modConstantes.gsDbAB + "V_BOD_ART ab on ab.IDARTICULO=m.IDARTICULO " +
                      "  inner join " + modConstantes.gsDbAB + "v_bodegas b on b.idbodega=ab.idbodega  " +
                      "where " +
                       "m.idarticulo = " + asIdentificador + "";


            aoDs = bd.Fill(con, lsSql);



            //// Recupera cantidades compra.

            //DataSet loDs = new DataSet();
            //DataSet loDs1 = new DataSet();


            //lsSql = "select " +
            //        " ped_ecom + ped_fcom as ped, ped_canj, " +
            //        " lle_ecom + lle_fcom as lle, lle_canj, " +
            //        " can_ecom + can_fcom as cnc, can_canj  " +
            //        "from " + modConstantes.gsDbAB + "m_articulos " +
            //        "where " +
            //        " idarticulo  = " + asIdentificador;


            //loDs = bd.Fill(con, lsSql);


            //if (loDs.Tables[0].Rows.Count <= 0)
            //{
            //    lsSql = "select " +
            //            " 0.00 as ped, 0.00 as ped_canj, " +
            //            " 0.00 as lle, 0.00 as lle_canj, " +
            //            " 0.00 as cnc, 0.00 as can_canj  ";


            //    loDs = bd.Fill(con, lsSql);
            //}

            //lsSql = "select space(20) as tipo , 0.00 as comp , 0.00 as canj";


            //loDs1 = bd.Fill(con, lsSql);


            //loDs1.Tables[0].Rows.Clear();

            //DataRow loRow = loDs1.Tables[0].NewRow();


            //loRow["tipo"] = "Esperada";
            //loRow["comp"] = loDs.Tables[0].Rows[0]["ped"].ToString();
            //loRow["canj"] = loDs.Tables[0].Rows[0]["ped_canj"].ToString();

            //loDs1.Tables[0].Rows.Add(loRow);

            //loRow = loDs1.Tables[0].NewRow();

            //loRow["tipo"] = "Llegada";
            //loRow["comp"] = loDs.Tables[0].Rows[0]["lle"].ToString();
            //loRow["canj"] = loDs.Tables[0].Rows[0]["lle_canj"].ToString();

            //loDs1.Tables[0].Rows.Add(loRow);
            //loRow = loDs1.Tables[0].NewRow();

            //loRow["tipo"] = "Cancelada";
            //loRow["comp"] = loDs.Tables[0].Rows[0]["cnc"].ToString();
            //loRow["canj"] = loDs.Tables[0].Rows[0]["can_canj"].ToString();

            //loDs1.Tables[0].Rows.Add(loRow);
            //loRow = loDs1.Tables[0].NewRow();

            //loRow["tipo"] = "Total";
            //loRow["comp"] = Convert.ToDecimal(loDs.Tables[0].Rows[0]["ped"].ToString()) + Convert.ToDecimal(loDs.Tables[0].Rows[0]["lle"].ToString());
            //loRow["canj"] = Convert.ToDecimal(loDs.Tables[0].Rows[0]["ped_canj"].ToString()) + Convert.ToDecimal(loDs.Tables[0].Rows[0]["lle_canj"].ToString());

            //loDs1.Tables[0].Rows.Add(loRow);

            //loDs1.Tables[0].TableName = "Y";
            //aoDs.Tables.Add(loDs1.Tables[0].Copy());


            //// *** Agrega Existencias ***
            //// Base.
            //DataSet loDsExi;
            //lsSql = "select '' as des_sed , '' as des_srv , '' as des_bod , 0.00 as can_exi , 0 as num_bod ";


            //loDsExi = bd.Fill(con, lsSql);

            //loDsExi.Tables[0].Rows.Clear();


            //// Hospital.
            //DataSet loDsHD;
            //lsSql = "select 1 as des_sed, 'Bodega Central' as des_srv , 'Bodega Central' as des_bod , saldo as can_exi , idbodega as num_bod " +
            //        "from " + modConstantes.gsDbAB + "m_articulos " +
            //        "where idarticulo = " + asIdentificador + " ";


            //loDsHD = bd.Fill(con, lsSql);


            //if (loDsHD.Tables.Count > 0)
            //{
            //    foreach (DataRow drhd in loDsHD.Tables[0].Rows)
            //    {
            //        loRow = loDsExi.Tables[0].NewRow();
            //        loRow["des_sed"] = drhd["des_sed"];
            //        loRow["des_srv"] = drhd["des_srv"];
            //        loRow["des_bod"] = drhd["des_bod"];
            //        loRow["can_exi"] = drhd["can_exi"];
            //        loRow["num_bod"] = drhd["num_bod"];
            //        loDsExi.Tables[0].Rows.Add(loRow);
            //    }
            //}


            ////// Farmacias y Centrales.

            ////DataSet loDsFC;
            ////lsSql = "select des_sed , des_srv , des_bod , can_exi , num_bod " +
            ////        "from " + modConstantes.gsDbAB + "v_exi_hd_fc " +
            ////        "where idmaterial = " + asIdentificador + " " +
            ////        "order by des_srv , des_bod";


            ////loDsFC = bd.Fill(con, lsSql);

            ////if (loDsFC.Tables[0].Rows.Count > 0)
            ////{
            ////    foreach (DataRow drfc in loDsFC.Tables[0].Rows)
            ////    {
            ////        loRow = loDsExi.Tables[0].NewRow();
            ////        loRow["des_sed"] = drfc["des_sed"];
            ////        loRow["des_srv"] = drfc["des_srv"];
            ////        loRow["des_bod"] = drfc["des_bod"];
            ////        loRow["can_exi"] = drfc["can_exi"];
            ////        loRow["num_bod"] = drfc["num_bod"];
            ////        loDsExi.Tables[0].Rows.Add(loRow);
            ////    }
            ////}

            //loDsExi.Tables[0].TableName = "existencias";
            //aoDs.Tables.Add(loDsExi.Tables[0].Copy());



        }
        catch (Exception e)
        {
            con.Close();
            lsRet = e.Message;
            aoDs = null;
        }

        con.Close();
        return aoDs;
    }

    public DataSet ListaCodBar(string lsIdentificador)
    {
        DataSet aoCod;

        string lsSql;

        // Recupera Códigos de barra asociados.
        lsSql = "select idarticodb , ltrim(rtrim(codbarra)) as codbarra, capacidad " +
                "from " + modConstantes.gsDbAB + "m_arti_codb  " +
                "where " +
                "idestado <> 3 and " +
                "idarticulo = " + lsIdentificador + " " +
                "order by codbarra";
        con = bd.fnGetConn();
        aoCod = bd.Fill(con, lsSql);
        con.Close();
        return aoCod;
    }

    public string mfGuardar(string asIdentificador)
    {

        string lsSql = "update " + modConstantes.gsDbRH + "m_articulos SET " +
                            " IDPRESENTACION = " + lsIDPRESENTACION + ", " +
                            " IDUNMIN = " + lsMinima + ", " +
                            " TIPO_RIESGO = " + lsMorbido + ", " +
                            " FACTOR = " + lsFactor + ", " +
                            " CALCULAR = " + lsCALCULAR + " " +
                            "where idarticulo   = " + asIdentificador;
        con = bd.fnGetConn();
        string lsRet = bd.EjecutarComando(con, lsSql);

        con.Close();
        return lsRet;
    }

    #region Homolgar

    public DataSet mfBuscarHomologArtFarm(ClassArticulos Art)
    {
        string lsSql = "";
        DataSet ds;


        // Armar SQL
        string lsWhe = " AND H.IDARTHOM = " + Art.lsIdArt + " ";


        // Criterios propios de ARTICULO.

        if (Art.lsArt != "")
        {
            lsWhe = lsWhe + "and (v.DESCRIPCION_LARGA) like '%" + Art.lsArt + "%' ";
        }


        if (Art.lsCod != "")
        {
            lsWhe = lsWhe + " and v.CODARTICULO like '%" + Art.lsCod + "%'";
        }



        lsSql = "SELECT v.idarticulo, V.CODARTICULO, V.DESCRIPCION_LARGA, H.FACTOR " +
                    "FROM " + modConstantes.gsDbAB + "M_HOMOLAGA_FARMACO H " +
                    "INNER JOIN " + modConstantes.gsDbAB + "v_articulos V ON V.IDARTICULO = H.IDART " +
                    "WHERE H.IDESTADO <> 3 " +
                    lsWhe + " ";


        con = bd.fnGetConn();
        ds = bd.Fill(con, lsSql);
        con.Close();

        return ds;
    }

    public string mfExisteArtHommolgado(string lsIdentificador, string lsIdArt)
    {
        string lsRet = "";
        string lsSql = "";


        con = bd.fnGetConn();

        lsSql = " Select count(1) " +
        "from " + modConstantes.gsDbAB + "M_HOMOLAGA_FARMACO " +
        "where IDART = " + lsIdentificador + " " +
        "and IDARTHOM = " + lsIdArt;

        lsRet = bd.ExecuteScalar(con, lsSql);

        if (lsRet == "") lsRet = "0";

        con.Close();


        return lsRet;

    }

    #endregion

}