using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;



/// <summary>
/// Descripción breve de modFunciones
/// </summary>
public class modFunciones
{

    //modFunciones modfun = new modFunciones();
    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    public modFunciones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    //#region Correlativos
    //public string mfgetCorrelativos(string asId)
    //{
    //    string lsSql = "";
    //    string lsUpd = "";
    //    string lsRet = "";
    //    string lsRetUpd = "";

    //    if (asId == "P")
    //    {

    //        con = bd.fnGetConn();
    //        lsSql = "SELECT ISNULL(CORR_PREP,0) + 1 FROM M_CORRELATIVOS";
    //        lsRet = bd.ExecuteScalar(con, lsSql);
    //        lsUpd = "UPDATE M_CORRELATIVOS SET CORR_PREP = " + lsRet;
    //        lsRetUpd = bd.EjecutarComando(con, lsUpd);
    //        con.Close();

    //    }
    //    else if (asId == "D")
    //    {

    //        con = bd.fnGetConn();
    //        lsSql = "SELECT ISNULL(CORR_DISP,0) + 1 FROM M_CORRELATIVOS";
    //        lsRet = bd.ExecuteScalar(con, lsSql);
    //        lsUpd = "UPDATE M_CORRELATIVOS SET CORR_DISP = " + lsRet;
    //        lsRetUpd = bd.EjecutarComando(con, lsUpd);
    //        con.Close();

    //    }
    //    else
    //    {

    //        con = bd.fnGetConn();
    //        lsSql = "SELECT ISNULL(CORR_PEND,0) + 1 FROM M_CORRELATIVOS";
    //        lsRet = bd.ExecuteScalar(con, lsSql);
    //        lsUpd = "UPDATE M_CORRELATIVOS SET CORR_PEND = " + lsRet;
    //        lsRetUpd = bd.EjecutarComando(con, lsUpd);
    //        con.Close();

    //    }

    //    return lsRet;
    //}

    //#endregion

    #region Usuarios

    public String fnValidaUsrApp(String asAplicacion, string gUsr, string asCodSistema)
    {
        string lsRet = "";
        try
        {
            //Si no se indicó aplicación, sale.
            if (asAplicacion == "") return lsRet = "";

            //Recupera Contexto.
            string lsAux;
            string lsErr;
            String asPermiso;


            lsErr = "Debe iniciar sesión de usuario.";

            //Valida sesión Activa.

            lsAux = gUsr.Trim().ToLower();

            if (lsAux == "")
                return lsErr + "(1) ";
            //else if (Int32.Parse(lsAux) > 0 )
            //    return lsErr + "(2) " + lsAux;
            else if (fnLong(lsAux) <= 0)
                return lsErr + " (3) " + lsAux;

            //Determina Permiso del Usuario.

            asPermiso = fnPermisoUsuarioApp(asCodSistema, asAplicacion, gUsr);



            if (lsRet != "") return lsRet;


            lsRet = asPermiso;

            if (asPermiso != "M" && asPermiso != "L")
            {
                lsRet = "Error en Permiso";
                lsRet = "Ret: " + lsRet + ". gSys: " + asCodSistema + ".gUsr: " + gUsr + ".asPermiso: " + asPermiso;
            }
        }
        catch (Exception ex)
        {
            lsRet = ex.Message;
        }

        return lsRet;
    }


    public String fnPermisoUsuarioApp(String asCodSistema, String asAplicacion, String asCodUsuario)
    {
        String Permiso = "";

        Usuarios usr = new Usuarios();

        try
        {
            Permiso = usr.jaxValidarPermisoApp(asCodUsuario, asAplicacion, asCodSistema);
        }
        catch (Exception ex)
        {
            Permiso = ex.Message;
        }


        return Permiso;
    }


    #endregion

    #region Numericos

    public string ConfirmValor(string respuesta)
    {
        string salida = string.Empty;

        if (respuesta.Contains(","))
        {
            salida = respuesta.Substring(0, respuesta.IndexOf(","));
        }
        else
            salida = respuesta;

        return salida;
    }

    public String fnSinComaFinal(String asTexto)
    {
        String lsAux;

        lsAux = (asTexto.Trim());
        if (lsAux != "")
        {

            if (lsAux.Substring(lsAux.Length - 1) == ",")
                lsAux = lsAux.Substring(0, lsAux.Length - 1);

            asTexto = lsAux;
        }
        return asTexto;
    }

    public string fnRandom()
    {
        Random random = new Random();
        int randomNumber = random.Next(0, 100);

        return randomNumber.ToString();
    }


    public Double fnLong(string asValor)
    {
        Double llRet;
        try
        {
            asValor = asValor.Replace("$", "");
            asValor = asValor.Replace(".", "");
            asValor = asValor.Replace("%", "");

            if (!double.TryParse(asValor, out llRet)) return 0;
            llRet = Convert.ToDouble(asValor);

        }
        catch (Exception e)
        {
            llRet = 0;
        }
        return llRet;

    }

    public string fnSwapChar(String lsTxt, String asCharCambiar, String asCharCambio)
    {
        string lsLet = "";
        String lsRet = "";
        int i;


        for (i = 0; i < lsTxt.Length; i++)
        {
            lsLet = lsTxt.Substring(i, 1);

            if (lsLet == asCharCambiar)
                lsRet += asCharCambio;
            else if (Asc(lsLet) == 13 || Asc(lsLet) == 10)
                lsLet = " ";
            else
                lsRet += lsLet;
        }

        return lsRet;
    }

    public String fnNumeroSinComas(String lsTxt)
    {
        return fnSwapChar(lsTxt, ",", ".");
    }

    public string fnIntPositivo(string lsTxt)
    {
        if (Convert.ToInt32(lsTxt) < 0)
            return "0";
        else
            return lsTxt;
    }

    #endregion


    #region Exportar

    public string fnObjRender(GridView aoObj)
    {
        string lsRet = "";

        try
        {
            //StringBuilder SBy = new StringBuilder("StringBuilder");
            //StringWriter SWy = new StringWriter(SBy);
            //HtmlTextWriter htmlTWy = new System.Web.UI.HtmlTextWriter(SWy);

            //if (Object.ReferenceEquals(aoObj.GetType(), GridView))
            lsRet = fnRenderGrilla(aoObj);

        }
        catch (Exception ex)
        {
            lsRet = ex.Message;
        }

        return lsRet;
    }



    //    Public Function fnObjRender( _
    //           ByVal aoObj As Object) As String
    //    Dim lsRet As String = ""
    //    Try
    //        Dim SBy As New System.Text.StringBuilder
    //        Dim SWy As New System.IO.StringWriter(SBy)
    //        Dim htmlTWy As New System.Web.UI.HtmlTextWriter(SWy)
    //        If TypeOf aoObj Is GridView Then
    //            lsRet = fnRenderGrilla(aoObj)
    //        Else
    //            aoObj.RenderControl(htmlTWy)
    //            lsRet = SBy.ToString.Replace(vbCrLf, " ")
    //        End If
    //    Catch ex As Exception
    //        lsRet = ex.Message
    //    End Try
    //    Return lsRet
    //End Function

    public string fnRenderGrilla(System.Web.UI.WebControls.GridView aoDG)
    {
        String lsRet = "";

        try
        {
            DataSet loDs = new DataSet();
            GridView loDg = new GridView();
            DataTable loDt = new DataTable(""); ;
            DataRow loRow;
            BoundField xxx;

            int i = -1;

            Boolean lbAux = false;

            //Recupera columnas con ancho mayor a 0.
            foreach (DataControlField dc in aoDG.Columns)
            {
                i += 1;
                loDt.Columns.Add("f" + i);



                if (dc.HeaderStyle.Width.ToString().ToLower().Trim() == "0px" || dc.ItemStyle.Width.ToString().ToLower().Trim() == "0px")
                {
                    lbAux = true;
                }
                else
                {
                    // Agrega Columna.
                    xxx = new BoundField(); // BoundColumn
                    xxx.ItemStyle.HorizontalAlign = dc.ItemStyle.HorizontalAlign;
                    xxx.DataField = "f" + i;
                    xxx.HeaderText = dc.HeaderText;
                    loDg.Columns.Add(xxx);
                }
            }

            // Si hay filas que ocultar.
            StringBuilder SBy = new StringBuilder("");
            StringWriter SWy = new StringWriter(SBy);

            HtmlTextWriter htmlTWy = new System.Web.UI.HtmlTextWriter(SWy);
            lbAux = true;

            if (lbAux)
            {
                foreach (GridViewRow dr in aoDG.Rows)
                {
                    loRow = loDt.NewRow();
                    int ll = -1;

                    foreach (DataControlField dc1 in aoDG.Columns)
                    {
                        ll += 1;
                        loRow[ll] = aoDG.Page.Server.HtmlDecode(dr.Cells[ll].Text.Trim());
                    }
                    loDt.Rows.Add(loRow);
                }

                loDs.Tables.Add(loDt);
                loDg.AutoGenerateColumns = false;
                loDg.DataSource = loDs;
                loDg.DataMember = loDs.Tables[0].TableName;
                loDg.DataBind();
                loDg.RenderControl(htmlTWy);

            }
            else
            {
                aoDG.RenderControl(htmlTWy);
            }
            lsRet = SBy.ToString().Replace("\n", " ");



        }
        catch (Exception ex)
        {
            lsRet = ex.Message;
        }
        return lsRet;
    }



    #endregion


    #region String

    public bool IsDate(object inValue)
    {
        bool bValid;
        try
        {
            DateTime myDT = DateTime.Parse(inValue.ToString());
            bValid = true;
        }
        catch (Exception e)
        {
            bValid = false;
        }

        return bValid;
    }

    public static string mfLimpiaString(string Dato)
    {
        string salida = Dato.ToLower();

        salida = salida.Replace("'", "");
        salida = salida.Replace("--", "");
        salida = salida.Replace("=", "");
        salida = salida.Replace("<", "");
        salida = salida.Replace(">", "");
        salida = salida.Replace("declare ", "");
        salida = salida.Replace("char(", "");
        salida = salida.Replace("char ", "");
        salida = salida.Replace("set ", "");
        salida = salida.Replace("cast ", "");
        salida = salida.Replace("convert ", "");
        salida = salida.Replace("delete ", "");
        salida = salida.Replace("drop ", "");
        salida = salida.Replace("exec ", "");

        salida = salida.Replace("insert ", "");
        salida = salida.Replace("meta ", "");
        salida = salida.Replace("script ", "");
        salida = salida.Replace("select ", "");
        salida = salida.Replace("truncate ", "");
        salida = salida.Replace("update ", "");

        salida = salida.Replace("alter ", "");
        salida = salida.Replace("begin ", "");
        salida = salida.Replace("checkpoint", "");
        salida = salida.Replace("commit ", "");
        salida = salida.Replace("create ", "");
        salida = salida.Replace("cursor ", "");
        salida = salida.Replace("dbcc", "");
        salida = salida.Replace("deny ", "");
        salida = salida.Replace("escape", "");
        salida = salida.Replace("execute", "");
        salida = salida.Replace(" go ", "");
        salida = salida.Replace("grant ", "");
        salida = salida.Replace("opendatasource", "");
        salida = salida.Replace("openquery", "");
        salida = salida.Replace("openrowset", "");
        salida = salida.Replace("shutdown", "");
        salida = salida.Replace("sp_", "");
        salida = salida.Replace("tran ", "");
        salida = salida.Replace("transaction ", "");

        salida = salida.Replace("xp_", "");
        salida = salida.Replace(";", "");



        return salida;
    }

    public String fnGetFec(Object aoCol)
    {
        String lsRet = "";

        try
        {
            if (aoCol == null)
                lsRet = "";
            else
            {
                String lsFec = aoCol.ToString().Trim();

                if (IsDate(lsFec))
                    lsRet = "";
                else
                {

                    DateTime ldFec = DateTime.Parse(lsFec);
                    lsRet = String.Format("dd-MM-yyyy", ldFec);
                }
            }
            return lsRet;
        }
        catch
        {
            return "";
        }
    }


    private int Asc(string s)
    {
        return Encoding.ASCII.GetBytes(s)[0];
    }

    public string fnSetTxtLen(string asTexto, int alLength)
    {
        asTexto = asTexto.Trim();
        if (asTexto.Length > alLength) asTexto = asTexto.Substring(1, alLength);

        return asTexto;
    }


    public Double fnSetLong(String asValor)
    {
        Double ldRet;
        try
        {
            // Quita caracteres inválidos.
            asValor = asValor.Replace("$", "");
            asValor = asValor.Replace(".", "");
            asValor = asValor.Replace("%", "");

            if (!IsNumeric(asValor))
                ldRet = 0;
            else
                ldRet = Convert.ToDouble(asValor);


        }
        catch (Exception e)
        {
            ldRet = 0;
        }

        return ldRet;
    }

    public String fnSetTxtLenEnter(String asTexto, int alLength)
    {
        asTexto = asTexto.Replace("'", "´");
        if (asTexto.Length > alLength) asTexto = asTexto.Substring(1, alLength);
        return asTexto;

    }

    public String fnSetFec(String asFecha)
    {
        try
        {
            int liId1 = 0;
            int liId2 = 0;
            asFecha = asFecha.Trim();

            if (asFecha == "")
            {
                asFecha = "";
                return asFecha;
            }
            asFecha = asFecha.Trim().Replace("/", "-");
            // Una fecha debe tener, al menos 6 caracteres: d-m-yy}
            if (asFecha.Length < 6)
            {
                asFecha = "";
                return asFecha;
            }


            // Una fecha válida debe tener dos guiones.
            liId1 = asFecha.IndexOf("-", 1);
            if (liId1 <= 0) return "";

            liId2 = asFecha.IndexOf("-", liId1 + 1);
            if (liId2 <= 0)
            {
                asFecha = "";
                return asFecha;
            }

            // Valida que sea fecha.
            if (!IsDate(asFecha))
            {
                return "";

            }

            DateTime ldFec = DateTime.ParseExact(asFecha, "dd/MM/yy", null);
            asFecha = ldFec.Day + "-" + ldFec.Month + "-" + ldFec.Year;

            if (!IsDate(asFecha)) return "";
        }
        catch (Exception e)
        {
            asFecha = "";
        }

        return asFecha;
    }

    public bool IsDate(String inputDate)
    {
        bool isDate = true;
        try
        {
            DateTime dt = DateTime.Parse(inputDate);
        }
        catch
        {
            isDate = false;
        }
        return isDate;
    }

    public static System.Boolean IsNumeric(System.Object Expression)
    {
        if (Expression == null || Expression is DateTime)
            return false;

        if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
            return true;

        try
        {
            if (Expression is string)
                Double.Parse(Expression as string);
            else
                Double.Parse(Expression.ToString());
            return true;
        }
        catch
        {
        } // just dismiss errors but return false
        return false;

    }

    /// Encripta una cadena
    public static string Encriptar(string _cadenaAencriptar)
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
        result = Convert.ToBase64String(encryted);
        return result;
    }

    /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
    public static string DesEncriptar(string _cadenaAdesencriptar)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
        //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }


    public string mfConcatenaIN(int[] arreglo, int j)
    {
        string salida = "";

        for (int i = 0; i <= (j - 1); i++)
        {
            if (salida == "")
                salida = arreglo[i].ToString();
            else
                salida = salida + ", " + arreglo[i].ToString();
        }

        return salida;
    }

    #endregion

    #region Ventanas

    public void VentanaDoc(Page page, string asRuta)
    {
        string asScript = "fnPop( 'wp','" + asRuta + "',false)";

        ClientScriptManager csm = page.ClientScript;

        if (!csm.IsStartupScriptRegistered(page.GetType(), "winPop"))
        {
            csm.RegisterStartupScript(page.GetType(), "Mensaje", asScript, true);
        }
    }

    #endregion


    #region Excell


    public static string fnCargaXls2(String asArchivo, String asSql)
    {
        DataSet aoDs = new DataSet();

        String lsCon = "";
        String lsSql = "";
        string lsRet = "";

        try
        {
            lsCon =
              "Provider=Microsoft.Jet.OLEDB.4.0;" +
              "Data Source=" + asArchivo + ";" +
              "Extended Properties=Excel 8.0;";


            //string lsExtension = Path.GetExtension(asArchivo);

            //if (lsExtension == "xls")
            //    lsCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + asArchivo + ";Extended Properties ='Excel 8.0;IMEX=1;HDR=YES;'";
            //else
            //    lsCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + asArchivo + ";Extended Properties ='Excel 12.0;IMEX=1;HDR=YES;'";

            // Crea Sql de Lectura.
            lsSql = asSql;
            // Definimos la conexión OleDb al fichero Excel y la abrimos
            System.Data.OleDb.OleDbConnection oledbConn = new System.Data.OleDb.OleDbConnection(lsCon);
            oledbConn.Open();
            // Creamos un comand para ejecutar la sentencia SELECT.
            System.Data.OleDb.OleDbCommand oledbCmd = new System.Data.OleDb.OleDbCommand(lsSql, oledbConn);
            // Creamos un dataAdapter para leer los datos y asociarlos al DataSet.
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(oledbCmd);

            try
            {
                lsRet = "Bien";
            }
            catch (Exception ex)
            {
                lsRet = ex.Message;
            }
            oledbConn.Close();
        }
        catch (Exception ex)
        {
            lsRet = ex.Message;
        }
        return lsRet;
    }
    public static DataSet fnCargaXls(String asArchivo, String asSql)
    {
        DataSet aoDs = new DataSet();

        String lsCon = "";
        String lsSql = "";

        try
        {
            //lsCon =
            //  "Provider=Microsoft.Jet.OLEDB.4.0;" +
            //  "Data Source=" + asArchivo + ";" +
            //  "Extended Properties=Excel 8.0;";


            string lsExtension = Path.GetExtension(asArchivo);

            if (lsExtension == ".xls")
                lsCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + asArchivo + ";Extended Properties ='Excel 8.0;IMEX=1;HDR=YES;'";
            else
                lsCon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + asArchivo + ";Extended Properties ='Excel 12.0;IMEX=1;HDR=YES;'";

            // Crea Sql de Lectura.

            lsSql = asSql;
            // Definimos la conexión OleDb al fichero Excel y la abrimos
            System.Data.OleDb.OleDbConnection oledbConn = new System.Data.OleDb.OleDbConnection(lsCon);
            oledbConn.Open();

            // Creamos un comand para ejecutar la sentencia SELECT.
            System.Data.OleDb.OleDbCommand oledbCmd = new System.Data.OleDb.OleDbCommand(lsSql, oledbConn);
            // Creamos un dataAdapter para leer los datos y asociarlos al DataSet.
            System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(oledbCmd);

            try
            {
                da.Fill(aoDs);

            }
            catch (Exception ex)
            {
                aoDs = null;


            }
            oledbConn.Close();

        }
        catch (Exception ex)
        {
            aoDs = null;

        }
        return aoDs;
    }


    #endregion

    # region base64
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }



    public static string Encode(string strFileTarget)
    {
        string strEncoded;

        using (FileStream fs = new FileStream(strFileTarget, FileMode.Open, FileAccess.Read))
        {
            byte[] filebytes = new byte[fs.Length];

            fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
            strEncoded = Convert.ToBase64String(filebytes, Base64FormattingOptions.InsertLineBreaks);
        }
        return strEncoded;
    }

    #endregion
}
