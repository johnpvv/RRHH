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

public partial class contenido_RRHH_GestionPersonas : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    ClassPersona per = new ClassPersona();
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

                if (this.hdIdentificador.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                    this.TxtRut.Enabled = true;
                    this.TxtDv.Enabled = true;
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
                            this.txtFechaNacimiento.Text = aoDs.Tables[0].Rows[0]["FECHA_NACIMIENTO"].ToString();
                            this.TFono1.Text = aoDs.Tables[0].Rows[0]["FONO1"].ToString();
                            this.TFono2.Text = aoDs.Tables[0].Rows[0]["FONO2"].ToString();
                            this.TObsFono1.Text = aoDs.Tables[0].Rows[0]["OBS_FONO1"].ToString();
                            this.TObsFono2.Text = aoDs.Tables[0].Rows[0]["OBS_FONO2"].ToString();
                            this.TMail.Text = aoDs.Tables[0].Rows[0]["EMAIL"].ToString();
                            // this.TMail0.Text = aoDs.Tables[0].Rows[0]["MAIL2_CONT"].ToString();

                            this.ddlPrevision.SelectedValue = aoDs.Tables[0].Rows[0]["IDPREVISION"].ToString();
                            this.ddlRegion.SelectedValue = aoDs.Tables[0].Rows[0]["IDREGION"].ToString();
                            this.ddlComuna.SelectedValue = aoDs.Tables[0].Rows[0]["IDCOMUNA"].ToString();
                            this.observacion.Text = aoDs.Tables[0].Rows[0]["OBSERVACION"].ToString();
                            if (aoDs.Tables[0].Rows[0]["IDESTADO"].ToString() == "1")
                            {
                                this.lbEstado.Text = "VIGENTE";
                                this.btn_habilitar.Text = "Deshabilitar";
                            }
                            else
                            {
                                this.lbEstado.Text = "REVISAR ESTADO";
                            }

                            //this.ddlPrevision.SelectedValue = per.mfIdPrevisionPaciente(this.hdIdentificador.Value);
                            //this.ddlRegion.SelectedValue = per.mfIdRegionPaciente(this.hdIdentificador.Value);
                            //this.ddlComuna.SelectedValue = per.mfIdComunaPaciente(this.hdIdentificador.Value);

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
        ClassPaciente per = new ClassPaciente();

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

        ClassPaciente per = new ClassPaciente();
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

        ClassPaciente per = new ClassPaciente();
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

    private void LlenarPrevision()
    {
        DataSet dat = new DataSet();

        ClassPaciente per = new ClassPaciente();
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


    protected void TxtRut_TextChanged(object sender, EventArgs e)
    {
        /*
         DataSet aoDs;        
        aoDs = per.ConsultaPaciente(TxtRut.Text, "");

        if (aoDs != null && aoDs.Tables.Count > 1) {
            mens.mensaje(Page, "RUT ya existe, por favor verificar");
        }
        */
    }

    #region Persona

    protected void btn_Agregar_Click(object sender, EventArgs e)
    {
        //mfAgregar();
    }

    //private void mfAgregar()
    //{

    //    //Validaciones
    //    if (!ValidarCampos()) { return; }


    //    //Revisar si existe RUT
    //    if (nuevo)
    //    {

    //        DataSet aoDs;
    //        aoDs = per.ConsultaPersona(TxtRut.Text, "");

    //        if (aoDs != null && aoDs.Tables.Count > 0)
    //        {
    //            if (aoDs.Tables[0].Rows.Count > 0)
    //            {
    //                mens.mensaje(Page, "RUT ya existe, por favor verificar");
    //                return;
    //            }
    //        }

    //        if (!ValidaRut(TxtRut.Text, TxtDv.Text))
    //        {
    //            mens.mensaje(Page, "RUT NO VALIDO, por favor verificar");
    //            return;
    //        };
    //    }
    //    else
    //    {

    //        DataSet aoDs;
    //        aoDs = per.ConsultaPaciente(TxtRutT.Text, "");

    //        if (aoDs != null && aoDs.Tables.Count > 0 && (TxtRutT.Text != "" && TxtDvT.Text != ""))
    //        {
    //            if (aoDs.Tables[0].Rows.Count > 0 && (TxtRutT.Text != "" && TxtDvT.Text != ""))
    //            {
    //                mens.mensaje(Page, "RUT ya existe, por favor verificar");
    //                return;
    //            }
    //        }


    //        // Validar Rut y  DV antes de seguir.
    //        if (!ValidaRut(TxtRutT.Text, TxtDvT.Text) && (TxtRutT.Text != "" && TxtDvT.Text != ""))
    //        {
    //            mens.mensaje(Page, "RUT NO VALIDO, por favor verificar");
    //            return;
    //        };
    //    }

    //    // Validar caracteres nombre social
    //    if (this.TxtNombreSocial.Text.Length <= 3 && this.TxtNombreSocial.Text != "")
    //    {
    //        mens.mensaje(Page, "Nombre Social debe tener mas de tres caracteres");
    //        return;
    //    };

    //    per.rut = this.TxtRut.Text;
    //    per.dv = this.TxtDv.Text;
    //    per.rut_n = this.TxtRutT.Text;
    //    per.dv_n = this.TxtDvT.Text;
    //    per.nombre = this.TxtNombre.Text;
    //    per.nombreSocial = this.TxtNombreSocial.Text;
    //    per.paterno = this.TxtPaterno.Text;
    //    per.materno = this.TxtMaterno.Text;

    //    per.direccion = this.TxtDire.Text;

    //    per.idcomuna = this.ddlComuna.SelectedItem.Value;
    //    per.idregion = this.ddlRegion.SelectedItem.Value;
    //    per.idprevision = this.ddlPrevision.SelectedItem.Value;

    //    if (this.chkPro.Checked) per.pro = "1"; else per.pro = "0";

    //    string lsRet = "";
    //    lsRet = per.CrearPaciente(per.rut, per.dv,
    //                                        per.rut_n, per.dv_n,
    //                                        per.idprevision,
    //                                        per.idregion,
    //                                        per.idcomuna,
    //                                        modFunciones.mfLimpiaString(per.nombre),
    //                                        modFunciones.mfLimpiaString(per.nombreSocial),
    //                                        modFunciones.mfLimpiaString(per.paterno),
    //                                        modFunciones.mfLimpiaString(per.materno),
    //                                        modFunciones.mfLimpiaString(per.direccion),
    //                                        TFono1.Text,
    //                                        TFono2.Text,
    //                                        TObsFono1.Text,
    //                                        TObsFono2.Text,
    //                                        TMail.Text,
    //                                        TMail0.Text,
    //                                        nuevo
    //                                       );

    //    if (lsRet != "")
    //        mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
    //    else
    //    {

    //        // if ((Session["lsIdentificador"]).ToString() == "0")
    //        if (this.hdIdentificador.Value == "0")
    //        {
    //            //Session.Add("lsIdentificador", per.rut);
    //            this.hdIdentificador.Value = per.rut;
    //            LbTitulo.Text = per.rut;
    //        }
    //        mens.mensaje(Page, "Registro ingresado con Exito.. ");
    //    }
    //}




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
            Response.Redirect("~/contenido/RRHH/ListaPersonas.aspx?id=0");
        }
        else
        {
            Response.Redirect("~/contenido/RRHH/ListaPersonas.aspx?id=0" + Session["cadena"].ToString());
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

        //string lsRet = per.UpdateEstado(Session["lsIdentificador"].ToString(), asEstado);
        //string lsRet = per.UpdateEstado(this.hdIdentificador.Value, asEstado);

        //if (lsRet != "")
        //    mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
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

    private void mfExistePersona()
    {
        per.ls_rut = this.TxtRut.Text;
        if (Convert.ToInt32(per.mfExistePersona()) > 0)
        {
            mens.mensaje(Page, "RUT ya tiene cuenta asociada.. ");
            this.TxtRut.Text = "";
        }
    }

    #endregion
}