using System;

/// <summary>
/// Descripción breve de modConstantes
/// </summary>
public class modConstantes
{
    // Declaracion de Base de Datos

    BaseDatos bd = new BaseDatos();
    System.Data.SqlClient.SqlConnection con = null;

    //private const string alserver = "[10.6.24.11].";
    private const string alserver = "";

    public const string gsDatabasePer = alserver + "DB_BOD_PERIFERICA";
    public const string gsDatabaseRH = alserver + "DB_RRHH";


    public const string gsDbPer = gsDatabasePer + ".dbo.";
    public const string gsDbRH = gsDatabaseRH + ".dbo.";
    public const string gsDatabaseAB = "DB_HCSBA";
    public const string gsDbAB = gsDatabaseAB + ".dbo.";



    public const string gsRutaGest = "http://localhost:50509/Fuentes/contenido/Seguimiento/Gest_Documentos/";

    //public const string gsRutaGestFisica = "C:\\WP\\Proyectos\\Abastecimiento\\Mantenciones\\Consignaciones\\Fuentes\\contenido\\Gestion\\Gest_Documentos\\";

    public const string gsRutaBodFisica = "C:\\WP\\Proyectos\\Abastecimiento\\Mantenciones\\Consignaciones\\Fuentes\\contenido\\Bodega\\BodDoc\\";
    public const string gsRutaBodGest = "http://168.88.162.39/bsiab/contenido/Bodega/BodDoc/";

    public const string gsServidor = "\\\\168.88.162.25\\";

    public const string gsRutaGestFisica = "Seguimiento\\Seg_Documentos\\";
    public const string gsRutaGestFisicaPDF = "Seguimiento\\Seg_Documentos\\";
    public const string gsRutaSegFisica = "Seg_Documentos\\";
    public const string gsRutaSegFisicaImg = "imagenes\\img\\";
    public const string gsRutaSegFisicaImgTimbre = "http://168.88.162.25/sysobj/img/";
    public const string gsRutaSegFisicaImgFirma = "http://168.88.162.25/sysobj/data/imab/";

    public const string gsRutaExcell = "C:\\WP\\Proyectos\\Abastecimiento\\Mantenciones\\Sistema Compras\\Fuentes\\contenido\\Compras\\CompraDoc\\";
    //public const string gsRutaExcell = "C:\\ASP.NET\\carpeta_excel_siab\\";

    public const double gdIva = 1.19;

    public const string gsTitAB = "Sistema RR.HH.";
    public const string gsInst = "Unidad RR.HH.";

    public const string gsSystemAB = "1"; // Abastecimiento.


    public const Double gsFPR = 1.2;

    // Estados

    public const int Guardada = 128;
    public const int Autorizada = 129;
    public const int Anulada = 130;
    public const int EnlazadaOC = 131;

    // Fuentes enFuente

    public const int Normal = 0;
    public const int NormalSubrayado = 1;
    public const int Titulo = 2;
    public const int TituloChico = 3;
    public const int SubTitulo = 4;
    public const int Pequena = 5;
    public const int PequenaError = 6;
    public const int PequenaAzul = 7;
    public const int NormalNegrita = 8;
    public const int TituloChicoSubNeg = 9;
    public const int MuyPequena = 10;
    public const int MuyGrande = 11;
    public const int Grande = 12;
    public const int MuyMuyGrande = 13;
    public const int GrandeMas = 14;


    // Tipo de Font 
    public const int FontNormal = 1;
    public const int FontNegrita = 2;
    public const int FontSubrayado = 3;
    public const int FontNegrSubr = 4;
    public const int FontTitulo = 5;

    public string jaxDescripcion(String asCodigo, String asCodSistema, String msTM)
    {
        string asDesc = "";
        try
        {
            string lsSql = "select descripcion " +
                          "from " + msTM + " " +
                          "where " +
                          " idestado <> 3 and idsistemas = " + asCodSistema + " and codigo = " + "'" + asCodigo + "'";


            con = bd.fnGetConn();
            asDesc = bd.ExecuteScalar(con, lsSql);
            con.Close();
        }
        catch (Exception e)
        {
            return "";
        }

        return asDesc;
    }

    public static string mfEstado(string Idestado)
    {
        string asDesc = "";
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        try
        {
            string lsSql = "select descripcion " +
                          "from " + modConstantes.gsDbAB + "tg_estados " +
                          "where " +
                          " idestado = " + Idestado;


            con = bd.fnGetConn();
            asDesc = bd.ExecuteScalar(con, lsSql);
            con.Close();
        }
        catch (Exception e)
        {
            return "";
        }

        return asDesc;
    }

    public static string mfConstante(string IdCodigo)
    {
        string asDesc = "";
        // Declaracion de Base de Datos

        BaseDatos bd = new BaseDatos();
        System.Data.SqlClient.SqlConnection con = null;


        try
        {
            string lsSql = "select isnull(DESCRIPCION,'') from " + modConstantes.gsDbAB +
                "M_CONSTANTES WHERE CODIGO  = '" + IdCodigo + "'";


            con = bd.fnGetConn();
            asDesc = bd.ExecuteScalar(con, lsSql);
            con.Close();
        }
        catch (Exception e)
        {
            return "";
        }

        return asDesc;
    }

}