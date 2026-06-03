using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data;
using System.Data.SqlClient;

public partial class contenido_Login : System.Web.UI.Page
{

    Mensaje mens = new Mensaje();
    Usuarios usr = new Usuarios();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            HttpContext.Current.Session.Remove("codHosp");
            HttpContext.Current.Session.Remove("nombre");
            HttpContext.Current.Session.Remove("user");
            HttpContext.Current.Session.Remove("serv");
        }
    }



    protected void Ingresar_Click(object sender, EventArgs e)
    {
        string user;
        string serv;
        string nombre;
        string codHosp = "1";
        string tipo;
        string tablero;
        Boolean salida = true;


        //string clave = modFunciones.DesEncriptar("MgAwADgAMwA=");

        //ClassReceta re = new ClassReceta();



        //tipo = this.rbTipo.SelectedValue;

        if (this.txtUser.Text != "" && this.txtClave.Text != "")
        {
            if (Convert.ToInt32(usr.mfIntentos(this.txtUser.Text)) >= 4)
            {
                mens.mensaje(Page, "Ha superado el Máximo de Intentos diarios.. ");
                return;
            }

            salida = ValidarUsuarioClave(modFunciones.mfLimpiaString(this.txtUser.Text), codHosp, this.txtClave.Text);

            if (salida)
            {

                if (ObtenerNewClave(modFunciones.mfLimpiaString(this.txtUser.Text), codHosp))
                {
                    Response.Redirect("~/contenido/Login/NuevaClave.aspx?user=" + modFunciones.mfLimpiaString(this.txtUser.Text) + "&hosp=" + codHosp);
                    return;
                }
                else
                {

                    //tablero = usr.mfEsMedico(this.txtUser.Text);

                    //if (tipo == "1" && tablero != "1")
                    //{
                    //    mens.mensaje(Page, "Usuario sin acceso a Receta.. ");
                    //    return;
                    //}

                    //tablero = usr.mfEsFarmacia(this.txtUser.Text);

                    //if (tipo == "2" && tablero != "1")
                    //{
                    //    mens.mensaje(Page, "Usuario sin acceso a Farmacia.. ");
                    //    return;
                    //}


                    //tablero = usr.mfEsInfectologia(this.txtUser.Text);

                    //if (tipo == "3" && tablero != "1")
                    //{
                    //    mens.mensaje(Page, "Usuario sin acceso a Infectología.. ");
                    //    return;
                    //}

                    nombre = usr.TraeNombre(modFunciones.mfLimpiaString(this.txtUser.Text));
                    user = usr.UserIdentificador(modFunciones.mfLimpiaString(this.txtUser.Text));
                    serv = usr.UserServicio(modFunciones.mfLimpiaString(this.txtUser.Text));


                    if (nombre == "" || user == "" || serv == "")
                    {
                        mens.mensaje(Page, "Usuario sin registrar, consultar a su administrador.. ");
                    }
                    else
                    {
                        Session.Add("codHosp", codHosp);
                        Session.Add("nombre", nombre);
                        Session.Add("user", user);
                        Session.Add("serv", serv);
                        Session.Add("tipo", "2");
                        //usr.mfLimpiarAcceso(this.txtUser.Text);
                        Response.Redirect("~/contenido/Index.aspx?user=" + user + "&serv=" + serv);
                    }
                }

            }
            else
            {
                mens.mensaje(Page, "Usuario no Registrado o Clave Erronea.. ");
                mfValidaAccesos();
            }
        }
        else
        {
            mens.mensaje(Page, "Debe de ingresar usuario y clave... ");
        }

    }



    private Boolean ValidarUsuarioClave(string aduser, string asHosp, string adclave)
    {
        Boolean Salida = true;
        string lsClave = usr.ObtenerClave(aduser, asHosp);

        if (lsClave != adclave) Salida = false;

        return Salida;

    }

    private Boolean ObtenerNewClave(string aduser, string asHosp)
    {
        Boolean Salida = false;

        if (usr.IsNewPass(aduser, asHosp) == "0") Salida = true;

        return Salida;

    }

    private void mfValidaAccesos()
    {

        try
        {

            string _XML = string.Empty;
            BaseDatos bd = new BaseDatos();
            System.Data.SqlClient.SqlConnection con = null;

            con = bd.fnGetConn();

            SqlCommand Query = new SqlCommand("pu_valida_accesos", con);
            Query.CommandType = CommandType.StoredProcedure;


            Query.Parameters.Add("@Rut", SqlDbType.Int, 4);
            Query.Parameters["@Rut"].Value = this.txtUser.Text;


            Query.Parameters.Add("@asmsg", SqlDbType.VarChar, 8000);
            Query.Parameters["@asmsg"].Direction = ParameterDirection.Output;


            Query.CommandTimeout = 600;
            Query.ExecuteNonQuery();
            con.Close();

            _XML = Query.Parameters["@asmsg"].Value.ToString();


        }
        catch (Exception ex)
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
}