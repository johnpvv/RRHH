using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

public partial class contenido_SysAdmin_GestionPacientes : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
        
    //ClassModSys objModSys = new ClassModSys();
    ClassPaciente objPaciente = new ClassPaciente();
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
                mfExistePaciente();
            }

            if(this.TxtRutT.Text != "" && this.chkLimpiar.Checked == false)
            {
                if (Request.Params["__EVENTTARGET"] == "ExisteRutTPostBack")
                {
                    mfExisteTPaciente();
                }

            }

        }


        if (!IsPostBack)
        {
            try
            {
                // Captura Datos
                Session.Add("lsIdentificador", Request.QueryString["key"].ToString());
                this.hdIdentificador.Value = Request.QueryString["key"].ToString();


                lsGrabar = modfunc.fnValidaUsrApp("BTN_CHK_PAC", gUsr, asCodSistema);
                if (lsGrabar != "M" && lsGrabar != "L") { this.chkLimpiar.Enabled = false; }

                LlenarPrevision();
                LlenarRegion();
                LlenarComuna();

                // if (Session["lsIdentificador"].ToString() == "0")
                if (this.hdIdentificador.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                    this.TxtRut.Enabled = true;
                    this.TxtDv.Enabled = true;

                    this.TxtRutT.Enabled = false;
                    this.TxtDvT.Enabled = false;

                    this.btn_habilitar.Enabled = false;                    
                    this.TxtRut.Focus();
                }
                else
                {
                    Session.Add("cadena", modFunciones.DesEncriptar(Request.QueryString["cadena"].ToString()));
                    Session.Add("lbNvo", false);
                    
                    nuevo = false;
                    this.TxtRut.Enabled = false;
                    this.TxtDv.Enabled = false;

                    this.TxtRutT.Enabled = true;
                    this.TxtDvT.Enabled = true;

                    // Detalle Alumno
                    //aoDs = objPaciente.ConsultarID(Session["lsIdentificador"].ToString());
                    aoDs = objPaciente.ConsultarID(this.hdIdentificador.Value);


                    if (aoDs != null && aoDs.Tables.Count > 0)
                    {
                        if (aoDs.Tables[0].Rows.Count > 0)
                        {
                            this.TxtRut.Text     = aoDs.Tables[0].Rows[0]["RUT"].ToString();
                            this.TxtDv.Text      = aoDs.Tables[0].Rows[0]["DV"].ToString();
                            this.TxtNombre.Text  = aoDs.Tables[0].Rows[0]["NOMBRE"].ToString();
                            this.TxtNombreSocial.Text = aoDs.Tables[0].Rows[0]["NOMBRE_SOCIAL"].ToString();
                            this.TxtPaterno.Text = aoDs.Tables[0].Rows[0]["AP_PATERNO"].ToString();
                            this.TxtMaterno.Text = aoDs.Tables[0].Rows[0]["AP_MATERNO"].ToString();
                            this.TxtDire.Text    = aoDs.Tables[0].Rows[0]["DIRECCION"].ToString();

                            this.TFono1.Text = aoDs.Tables[0].Rows[0]["FONO1_CONT"].ToString();
                            this.TFono2.Text = aoDs.Tables[0].Rows[0]["FONO2_CONT"].ToString();
                            this.TObsFono1.Text = aoDs.Tables[0].Rows[0]["OBS1_CONT"].ToString();
                            this.TObsFono2.Text = aoDs.Tables[0].Rows[0]["OBS2_CONT"].ToString();
                            this.TMail.Text = aoDs.Tables[0].Rows[0]["MAIL_CONT"].ToString();
                            this.TMail0.Text = aoDs.Tables[0].Rows[0]["MAIL2_CONT"].ToString();

                            //this.ddlPrevision.SelectedValue = aoDs.Tables[0].Rows[0]["IDPREVISION"].ToString();
                            //this.ddlRegion.SelectedValue    = aoDs.Tables[0].Rows[0]["IDREGION"].ToString();
                            //this.ddlComuna.SelectedValue    = aoDs.Tables[0].Rows[0]["IDCOMUNA"].ToString();


                            //this.ddlPrevision.SelectedValue = objPaciente.mfIdPrevisionPaciente(Session["lsIdentificador"].ToString());
                            //this.ddlRegion.SelectedValue = objPaciente.mfIdRegionPaciente(Session["lsIdentificador"].ToString());
                            //this.ddlComuna.SelectedValue = objPaciente.mfIdComunaPaciente(Session["lsIdentificador"].ToString());

                            this.ddlPrevision.SelectedValue = objPaciente.mfIdPrevisionPaciente(this.hdIdentificador.Value);
                            this.ddlRegion.SelectedValue = objPaciente.mfIdRegionPaciente(this.hdIdentificador.Value);
                            this.ddlComuna.SelectedValue = objPaciente.mfIdComunaPaciente(this.hdIdentificador.Value);

                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["RUT"].ToString() + "-" + aoDs.Tables[0].Rows[0]["DV"].ToString();

                            if(aoDs.Tables[0].Rows[0]["ISPROGRAMA"].ToString() != "0")
                            {
                                this.chkPro.Checked = true;
                            }
                            else
                            {
                                this.chkPro.Checked = false;
                            }

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
        ClassPaciente objPaciente = new ClassPaciente();

        dat = objPaciente.ConsultaComuna(ddlRegion.SelectedValue.ToString());

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

        ClassPaciente objPaciente = new ClassPaciente();
        dat = objPaciente.ConsultaComuna();

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

        ClassPaciente objPaciente = new ClassPaciente();
        dat = objPaciente.ConsultaRegion();

        this.ddlRegion.DataTextField = "DESCRIPCION";
        this.ddlRegion.DataValueField = "IDREGION";
        this.ddlRegion.DataSource = dat;
        this.ddlRegion.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Region";
        item.Value = "0";
        this.ddlRegion.Items.Insert(0, item);
    }

    private void LlenarPrevision()
    {
        DataSet dat = new DataSet();

        ClassPaciente objPaciente = new ClassPaciente();
        dat = objPaciente.ConsultaPrevision();

        this.ddlPrevision.DataTextField = "DESCRIPCION";
        this.ddlPrevision.DataValueField = "IDPREVISION";
        this.ddlPrevision.DataSource = dat;
        this.ddlPrevision.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Unidad";
        item.Value = "0";
        this.ddlPrevision.Items.Insert(0, item);
    }


    protected void TxtRut_TextChanged(object sender, EventArgs e)
    {
        /*
         DataSet aoDs;        
        aoDs = objPaciente.ConsultaPaciente(TxtRut.Text, "");

        if (aoDs != null && aoDs.Tables.Count > 1) {
            mens.mensaje(Page, "RUT ya existe, por favor verificar");
        }
        */
    }

    #region Pacientes

    protected void btn_Agregar_Click(object sender, EventArgs e)
    {
        mfAgregar();
    }

    private void mfAgregar() {

        //Validaciones
        if (!ValidarCampos()) { return; }


        //Revisar si existe RUT
        if (nuevo)
        {

            DataSet aoDs;
            aoDs = objPaciente.ConsultaPaciente(TxtRut.Text, "");

            if (aoDs != null && aoDs.Tables.Count > 0)
            {
                if (aoDs.Tables[0].Rows.Count > 0)
                {
                    mens.mensaje(Page, "RUT ya existe, por favor verificar");
                    return;
                }
            }

            // Validar Rut y  DV antes de seguir.
            if (!ValidaRut(TxtRut.Text, TxtDv.Text))
            {
                mens.mensaje(Page, "RUT NO VALIDO, por favor verificar");
                return;
            };


        }
        else
        {
            if(chkLimpiar.Checked)
                objPaciente.lscheck = "1";
            else
            {
                objPaciente.lscheck = "0";

                DataSet aoDs;
                aoDs = objPaciente.ConsultaPaciente(TxtRutT.Text, "");

                if (aoDs != null && aoDs.Tables.Count > 0 && (TxtRutT.Text != "" && TxtDvT.Text != ""))
                {
                    if (aoDs.Tables[0].Rows.Count > 0 && (TxtRutT.Text != "" && TxtDvT.Text != ""))
                    {
                        mens.mensaje(Page, "RUT ya existe, por favor verificar");
                        return;
                    }
                }

            }
 
            // Validar Rut y  DV antes de seguir.
            if (!ValidaRut(TxtRutT.Text, TxtDvT.Text) && (TxtRutT.Text != "" && TxtDvT.Text != ""))
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

        objPaciente.rut = this.TxtRut.Text;
        objPaciente.dv = this.TxtDv.Text;
        objPaciente.rut_n = this.TxtRutT.Text;
        objPaciente.dv_n = this.TxtDvT.Text;
        objPaciente.nombre = this.TxtNombre.Text;
        objPaciente.nombreSocial = this.TxtNombreSocial.Text;
        objPaciente.paterno = this.TxtPaterno.Text;
        objPaciente.materno = this.TxtMaterno.Text;

        objPaciente.direccion = this.TxtDire.Text;

        objPaciente.idcomuna = this.ddlComuna.SelectedItem.Value;
        objPaciente.idregion = this.ddlRegion.SelectedItem.Value;
        objPaciente.idprevision = this.ddlPrevision.SelectedItem.Value;

        if(this.chkPro.Checked) objPaciente.pro = "1"; else objPaciente.pro = "0";

        string lsRet="";
        lsRet = objPaciente.CrearPaciente(objPaciente.rut, objPaciente.dv,
                                            objPaciente.rut_n, objPaciente.dv_n,
                                            objPaciente.idprevision,
                                            objPaciente.idregion,
                                            objPaciente.idcomuna,
                                            modFunciones.mfLimpiaString(objPaciente.nombre),
                                            modFunciones.mfLimpiaString(objPaciente.nombreSocial),
                                            modFunciones.mfLimpiaString(objPaciente.paterno),
                                            modFunciones.mfLimpiaString(objPaciente.materno),
                                            modFunciones.mfLimpiaString(objPaciente.direccion),
                                            TFono1.Text,
                                            TFono2.Text,
                                            TObsFono1.Text,
                                            TObsFono2.Text,
                                            TMail.Text,
                                            TMail0.Text,
                                            nuevo
                                           );

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {

            // if ((Session["lsIdentificador"]).ToString() == "0")
            if (this.hdIdentificador.Value == "0")
            {
                //Session.Add("lsIdentificador", objPaciente.rut);
                this.hdIdentificador.Value = objPaciente.rut;
                LbTitulo.Text = objPaciente.rut;
            }
            mens.mensaje(Page, "Registro ingresado con Exito.. ");
        }
    }




    public bool ValidarCampos()
    {
        
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
        return true;
    }

    protected void ImgBtnBack_Click(object sender, ImageClickEventArgs e)
    {

        if (Session["cadena"] == null)
        {
            Response.Redirect("~/contenido/SysAdmin/ListaPacientes.aspx?id=0");
        }
        else
        {
            Response.Redirect("~/contenido/SysAdmin/ListaPacientes.aspx?id=0" + Session["cadena"].ToString());
        }
    }


    
    
    //A integrar lo siguiente ???

    protected void btn_habilitar_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            
            if (confirmValue == "Yes")
            {
                CambiarEstadoPaciente();
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    
      

    private void CambiarEstadoPaciente()
    {
        string asEstado = "2";

        if (this.lbEstado.Text == "VIGENTE") asEstado = "3";

        //string lsRet = objPaciente.UpdateEstado(Session["lsIdentificador"].ToString(), asEstado);
        string lsRet = objPaciente.UpdateEstado(this.hdIdentificador.Value, asEstado);

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {
            if (asEstado == "3")
            {
                this.lbEstado.Text = "NO VIGENTE";
                this.btn_habilitar.Text = "Habilitar";
            }
            else
            {
                this.lbEstado.Text = "VIGENTE";
                this.btn_habilitar.Text = "Deshabilitar";
            }
            mens.mensaje(Page, "Registro ingresado con Exito.. ");
        }
    }


    //Validador de rut------------------------------------------
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

    private void mfExistePaciente()
    {

        if (Convert.ToInt32(objPaciente.mfExistePaciente(this.TxtRut.Text)) > 0)
        {
            mens.mensaje(Page, "Rut ya tiene cuenta asociada.. ");
            this.TxtRut.Text = "";
        }


    }

    private void mfExisteTPaciente()
    {

        if (Convert.ToInt32(objPaciente.mfExistePaciente(this.TxtRutT.Text)) > 0)
        {
            mens.mensaje(Page, "Rut ya tiene cuenta asociada.. ");
            this.TxtRutT.Text = "";
        }


    }

    #endregion


}