using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_GestionPersonas : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    ClassTrabajadores per = new ClassTrabajadores();
    ClassUnidOperativa cu = new ClassUnidOperativa();
    Usuarios usr = new Usuarios();
    static bool nuevo;

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet aoDs;
        string gUsr;
        string asCodSistema;
        String lsGrabar = "";
        modFunciones modfunc = new modFunciones();
        gUsr = Session["user"].ToString();
        asCodSistema = "1";
        if (IsPostBack)
        {
            if (Request.Params["__EVENTTARGET"] == "ExisteRutPostBack")
            {
                mfExistePersona();
            }
        }
        if (!IsPostBack)
        {
            try
            {
                // Captura Datos
                Session.Add("lsIdentificador", Request.QueryString["key"].ToString());
                this.hdIdentificador.Value = Request.QueryString["key"].ToString();
                //lsGrabar = modfunc.fnValidaUsrApp("BTN_CHK_PAC", gUsr, asCodSistema);
                //if (lsGrabar != "M" && lsGrabar != "L") { this.chkLimpiar.Enabled = false; }
                LlenarPrevision();
                LlenarRegion();
                LlenarComuna();
                LlenarEstCivil();
                LlenarCentros();
                if (this.hdIdentificador.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                    this.TxtRut.Enabled = true;
                    this.TxtDv.Enabled = true;
                    this.btn_habilitar.Enabled = false;
                    this.btn_ReinicioClv.Enabled = false;
                    this.TxtRut.Focus();
                }
                else
                {
                    Session.Add("cadena", modFunciones.DesEncriptar(Request.QueryString["cadena"].ToString()));
                    Session.Add("lbNvo", false);

                    nuevo = false;
                    this.TxtRut.Enabled = false;
                    this.TxtDv.Enabled = false;
                    this.btn_Agregar.Text = "Actualizar";

                    per.ls_rut = this.hdIdentificador.Value;
                    aoDs = per.ConsultarID();

                    if (aoDs != null && aoDs.Tables.Count > 0)
                    {
                        if (aoDs.Tables[0].Rows.Count > 0)
                        {
                            this.TxtRut.Text = aoDs.Tables[0].Rows[0]["RUT"].ToString();
                            this.TxtDv.Text = aoDs.Tables[0].Rows[0]["DV"].ToString();
                            this.TxtNombre.Text = aoDs.Tables[0].Rows[0]["NOMBRE"].ToString();
                            this.TxtNombreSocial.Text = aoDs.Tables[0].Rows[0]["NOMBRE_SOCIAL"].ToString();
                            this.TxtPaterno.Text = aoDs.Tables[0].Rows[0]["AP_PATERNO"].ToString();
                            this.TxtMaterno.Text = aoDs.Tables[0].Rows[0]["AP_MATERNO"].ToString();
                            this.TxtDire.Text = aoDs.Tables[0].Rows[0]["DIRECCION"].ToString();
                            if (aoDs.Tables[0].Rows[0]["FECHA_NACIMIENTO"] != DBNull.Value)
                                this.txtFechaNacimiento.Text = Convert.ToDateTime(aoDs.Tables[0].Rows[0]["FECHA_NACIMIENTO"]).ToString("yyyy-MM-dd");
                            this.ddlEstadoCivil.SelectedValue = aoDs.Tables[0].Rows[0]["EST_CIVIL"].ToString();
                            this.ddlSexo.SelectedValue = aoDs.Tables[0].Rows[0]["SEXO"].ToString();
                            this.TFono1.Text = aoDs.Tables[0].Rows[0]["FONO1"].ToString();
                            this.TFono2.Text = aoDs.Tables[0].Rows[0]["FONO2"].ToString();
                            this.TObsFono1.Text = aoDs.Tables[0].Rows[0]["OBS_FONO1"].ToString();
                            this.TObsFono2.Text = aoDs.Tables[0].Rows[0]["OBS_FONO2"].ToString();
                            this.TMail.Text = aoDs.Tables[0].Rows[0]["EMAIL"].ToString();
                            this.ddlCentro.SelectedValue = aoDs.Tables[0].Rows[0]["IDCENTRO"].ToString();//coduniop
                            this.ddlPrevision.SelectedValue = aoDs.Tables[0].Rows[0]["IDPREVISION"].ToString();
                            this.ddlRegion.SelectedValue = aoDs.Tables[0].Rows[0]["IDREGION"].ToString();
                            this.ddlComuna.SelectedValue = aoDs.Tables[0].Rows[0]["IDCOMUNA"].ToString();
                            this.observacion.Text = aoDs.Tables[0].Rows[0]["OBSERVACION"].ToString();
                            this.hdIdEstado.Value = aoDs.Tables[0].Rows[0]["IDESTADO"].ToString();
                            if (this.hdIdEstado.Value == "1")
                            {
                                this.lbEstado.Text = "VIGENTE";
                                this.btn_habilitar.Text = "Deshabilitar";
                            }
                            else if (this.hdIdEstado.Value == "2")
                            {
                                this.lbEstado.Text = "EDITADO";
                                this.btn_habilitar.Text = "Deshabilitar";
                            }
                            else
                            {
                                this.lbEstado.Text = "NO VIGENTE";
                            }
                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["RUT"].ToString() + "-" + aoDs.Tables[0].Rows[0]["DV"].ToString();
                        }
                    }

                }
            }
            catch (Exception xe)
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaComuna(ddlRegion.SelectedValue.ToString());

        this.ddlComuna.DataTextField = "DESCRIPCION";
        this.ddlComuna.DataValueField = "IDCOMUNA";
        this.ddlComuna.DataSource = dat;
        this.ddlComuna.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Comuna";
        item.Value = "0";
        this.ddlComuna.Items.Insert(0, item);
    }


    private void LlenarComuna()
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaComuna();

        this.ddlComuna.DataTextField = "DESCRIPCION";
        this.ddlComuna.DataValueField = "IDCOMUNA";
        this.ddlComuna.DataSource = dat;
        this.ddlComuna.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Comuna";
        item.Value = "0";
        this.ddlComuna.Items.Insert(0, item);
    }

    private void LlenarRegion()
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaRegion();

        this.ddlRegion.DataTextField = "DESCRIPCION";
        this.ddlRegion.DataValueField = "IDREGION";
        this.ddlRegion.DataSource = dat;
        this.ddlRegion.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Region";
        item.Value = "0";
        this.ddlRegion.Items.Insert(0, item);
    }

    private void LlenarEstCivil()
    {
        DataSet dat = new DataSet();

        dat = per.ConsultaEstCivil();

        this.ddlEstadoCivil.DataTextField = "DESCRIPCION";
        this.ddlEstadoCivil.DataValueField = "IDESTADOCIVIL";
        this.ddlEstadoCivil.DataSource = dat;
        this.ddlEstadoCivil.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Estado";
        item.Value = "0";
        this.ddlEstadoCivil.Items.Insert(0, item);
    }
    private void LlenarPrevision()
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaPrevision();

        this.ddlPrevision.DataTextField = "DESCRIPCION";
        this.ddlPrevision.DataValueField = "IDPREVISION";
        this.ddlPrevision.DataSource = dat;
        this.ddlPrevision.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Unidad";
        item.Value = "0";
        this.ddlPrevision.Items.Insert(0, item);
    }
    private void LlenarCentros()
    {
        DataSet dat = new DataSet();
        dat = cu.mfListaUnidad(Session["serv"].ToString());

        this.ddlCentro.DataTextField = "DESCRIPCION";
        this.ddlCentro.DataValueField = "CODUNIOP";
        this.ddlCentro.DataSource = dat;
        this.ddlCentro.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Unidad";
        item.Value = "0";
        this.ddlCentro.Items.Insert(0, item);
    }


    protected void TxtRut_TextChanged(object sender, EventArgs e)
    {

        string ls_ret;
        per.ls_rut = TxtRut.Text;
        ls_ret = per.mfExistePersona();

        if (ls_ret != null && ls_ret != "")
        {
            mens.mensaje(Page, "RUT ya existe, por favor verificar");
        }

    }

    #region Persona

    protected void btn_Agregar_Click(object sender, EventArgs e)
    {
        mfAgregar();
    }

    private void mfAgregar()
    {
        //Validaciones
        if (!ValidarCampos()) { return; }

        //Revisar si existe RUT
        if (nuevo)
        {
            per.ls_rut = this.TxtRut.Text;
            if (Convert.ToInt32(per.mfExistePersona()) > 0)
            {
                mens.mensaje(Page, "RUT ya existe, por favor verificar");
                return;
            }

            if (!ValidaRut(TxtRut.Text, TxtDv.Text))
            {
                mens.mensaje(Page, "RUT NO VALIDO, por favor verificar");
                return;
            };
        }

        // Validar caracteres nombre social
        if (this.TxtNombreSocial.Text.Length <= 3 && this.TxtNombreSocial.Text != "")
        {
            mens.mensaje(Page, "Nombre Social debe tener mas de tres caracteres");
            return;
        };

        per.ls_rut = this.TxtRut.Text;
        per.ls_dv = this.TxtDv.Text;
        per.ls_nomb = modFunciones.mfLimpiaString(this.TxtNombre.Text);
        per.ls_nomb_soc = modFunciones.mfLimpiaString(this.TxtNombreSocial.Text);
        per.ls_pat = modFunciones.mfLimpiaString(this.TxtPaterno.Text);
        per.ls_mat = modFunciones.mfLimpiaString(this.TxtMaterno.Text);
        per.ls_fnac = Convert.ToDateTime(this.txtFechaNacimiento.Text).ToString("dd/MM/yyyy");
        per.ls_sexo = this.ddlSexo.SelectedValue;
        per.ls_estciv = this.ddlEstadoCivil.SelectedValue;
        per.ls_dir = modFunciones.mfLimpiaString(this.TxtDire.Text);
        per.ls_idcomuna = this.ddlComuna.SelectedItem.Value;
        per.ls_idregion = this.ddlRegion.SelectedItem.Value;
        per.ls_idprevision = this.ddlPrevision.SelectedItem.Value;
        per.ls_tel1 = this.TFono1.Text;
        per.ls_tel2 = this.TFono2.Text;
        per.ls_obs1 = this.TObsFono1.Text;
        per.ls_obs2 = this.TObsFono2.Text;
        per.ls_obs = modFunciones.mfLimpiaString(this.observacion.Text);
        per.ls_mail = this.TMail.Text;
        per.ls_iduser = Session["user"].ToString();
        per.ls_centro = this.ddlCentro.SelectedItem.Value;
        string lsRet = "";
        lsRet = per.CrearUsuario(nuevo);

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {
            if (this.hdIdentificador.Value == "0")
            {
                this.hdIdentificador.Value = per.ls_rut;
                LbTitulo.Text = per.ls_rut + "-" + per.ls_dv;
            }
            mens.mensaje(Page, "Registro ingresado con Exito.. ");
        }
    }

    public bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(this.TxtRut.Text))
        {
            mens.mensaje(Page, "Debe Escribir un RUT Válido");
            return false;
        }
        if (this.ddlPrevision.SelectedIndex == 0)
        {
            mens.mensaje(Page, "Debe seleccionar Prevision");
            return false;
        }

        if (this.ddlRegion.SelectedIndex == 0)
        {
            mens.mensaje(Page, "Debe seleccionar Region");
            return false;
        }

        if (this.ddlComuna.SelectedIndex == 0)
        {
            mens.mensaje(Page, "Debe seleccionar Comuna");
            return false;
        }
        if (string.IsNullOrWhiteSpace(this.txtFechaNacimiento.Text))
        {
            mens.mensaje(Page, "Debe ingresar la fecha de nacimiento...");
            return false;
        }

        DateTime fechaNacimiento;

        if (!DateTime.TryParseExact(this.txtFechaNacimiento.Text, "yyyy-MM-dd",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaNacimiento))
        {
            mens.mensaje(Page, "La fecha de nacimiento no es válida...");
            return false;
        }
        if (fechaNacimiento > DateTime.Today)
        {
            mens.mensaje(Page, "La fecha de nacimiento no puede ser futura...");
            return false;
        }
        return true;
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {

        if (Session["cadena"] == null)
        {
            Response.Redirect("~/contenido/Administracion/ListaPersonas.aspx?id=1");
        }
        else
        {
            Response.Redirect("~/contenido/Administracion/ListaPersonas.aspx?id=0" + Session["cadena"].ToString());
        }
    }

    protected void btn_habilitar_Click(object sender, EventArgs e)
    {
        try
        {
            CambiarEstadoPersona();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    private void CambiarEstadoPersona()
    {
        string asEstado = "2";

        if (this.hdIdEstado.Value == "1" || this.hdIdEstado.Value == "2")
            asEstado = "3";

        per.ls_rut = this.hdIdentificador.Value;
        string id = per.mfDevuelveID();
        per.ls_iduser = id;
        per.ls_idestado = asEstado;
        string lsRet = per.mfUpdateEstado();

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al cambiar el estado...");
        else
        {
            if (asEstado == "3")
            {
                this.lbEstado.Text = "NO VIGENTE";
                this.btn_habilitar.Text = "Habilitar";
            }
            else
            {
                this.lbEstado.Text = "EDITADO";
                this.btn_habilitar.Text = "Deshabilitar";
            }
            mens.mensaje(Page, "Estado Actualizado con Exito...");
        }
    }
    protected void btn_ReinicioClv_Click(object sender, EventArgs e)
    {
        try
        {
            mfReIniciarUsuario();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    private void mfReIniciarUsuario()
    {
        per.ls_rut = this.hdIdentificador.Value;
        string id = per.mfDevuelveID();
        per.ls_iduser = id;
        string lsRet = per.mfReIniciarClave();

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Reiniciar la Clave...");
        else
        {
            mens.mensaje(Page, "Clave Reiniciada con Exito...");
        }
    }
    private void mfExistePersona()
    {
        if (this.TxtRut.Text == "")
        {
            mens.mensaje(Page, "Debe Escribir un RUT Válido");
            return;
        }
        per.ls_rut = this.TxtRut.Text;
        if (Convert.ToInt32(per.mfExistePersona()) > 0)
        {
            mens.mensaje(Page, "RUT ya tiene cuenta asociada.. ");
            this.TxtRut.Text = "";
        }
        else
        {
            this.TxtDv.Text = Digito(Convert.ToInt32(this.TxtRut.Text));
        }
    }
    #endregion
    #region Validador de rut
    public static bool ValidaRut(string rut, string dv)
    {
        return ValidaRut(rut + "-" + dv);
    }

    public static bool ValidaRut(string rut)
    {
        rut = rut.Replace(".", "").ToUpper();
        Regex expresion = new Regex("^([0-9]+-[0-9K])$");
        string dv = rut.Substring(rut.Length - 1, 1);
        if (!expresion.IsMatch(rut))
        {
            return false;
        }
        char[] charCorte = { '-' };
        string[] rutTemp = rut.Split(charCorte);
        if (dv != Digito(int.Parse(rutTemp[0])))
        {
            return false;
        }
        return true;
    }
    public static string Digito(int rut)
    {
        int suma = 0;
        int multiplicador = 1;
        while (rut != 0)
        {
            multiplicador++;
            if (multiplicador == 8)
                multiplicador = 2;
            suma += (rut % 10) * multiplicador;
            rut = rut / 10;
        }
        suma = 11 - (suma % 11);
        if (suma == 11)
        {
            return "0";
        }
        else if (suma == 10)
        {
            return "K";
        }
        else
        {
            return suma.ToString();
        }
    }
    #endregion
}