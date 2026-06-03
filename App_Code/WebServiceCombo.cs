using AjaxControlToolkit;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;


/// <summary>
/// Descripción breve de WebServiceCombo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]


public class WebServiceCombo : System.Web.Services.WebService
{

    public WebServiceCombo()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }



    public List<CascadingDropDownNameValue> GetData(string query)
    {

        string ConS = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
        SqlCommand comando = new SqlCommand(query);

        List<CascadingDropDownNameValue> values = new List<CascadingDropDownNameValue>();
        using (SqlConnection con = new SqlConnection(ConS))
        {
            con.Open();
            comando.Connection = con;
            using (SqlDataReader reader = comando.ExecuteReader())
            {
                while (reader.Read())
                {
                    values.Add(new CascadingDropDownNameValue
                    {
                        name = reader[0].ToString(),
                        value = reader[1].ToString()
                    });
                }
                reader.Close();
                con.Close();
                return values;
            }
        }
    }


    #region Lista Grupo

    [WebMethod]
    public CascadingDropDownNameValue[] GetGrupo(string knownCategoryValues)
    {

        string query = "select desc_grupo, idgrupo from " + modConstantes.gsDbAB + "[TG_GRUPOS_COMPRAS] order by 1";
        List<CascadingDropDownNameValue> prov = GetData(query);
        return prov.ToArray();
    }


    [WebMethod]
    public CascadingDropDownNameValue[] GetUserGrupo(string knownCategoryValues)
    {
        string idgrupo = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["idgrupo"];
        string query = string.Format("select nombre, idpersona from " + modConstantes.gsDbAB + "v_persona_grupo where idgrupo = {0}", idgrupo);
        List<CascadingDropDownNameValue> ListPlant = GetData(query);
        return ListPlant.ToArray();
    }

    #endregion


    #region Lista Plantillas

    [WebMethod]
    public CascadingDropDownNameValue[] GetProveedor(string knownCategoryValues)
    {

        string query = "select nombre_prov, rut from " + modConstantes.gsDbAB + "[v_prov_insumos] order by 1";
        List<CascadingDropDownNameValue> prov = GetData(query);
        return prov.ToArray();
    }


    [WebMethod]
    public CascadingDropDownNameValue[] GetListPlantillas(string knownCategoryValues)
    {
        string rut = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)["rut"];
        string query = string.Format("select descripcion, idplanprov from " + modConstantes.gsDbAB + "M_PLANTILLA_PROV where IDESTADO <> 3 AND rut = {0}", rut);
        List<CascadingDropDownNameValue> ListPlant = GetData(query);
        return ListPlant.ToArray();
    }

    #endregion


}
