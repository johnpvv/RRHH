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
public partial class contenido_GestionRRHH_GestionHorarios : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    Usuarios usr = new Usuarios();
    ClassTurnos tur = new ClassTurnos();
    ClassHorarios hor = new ClassHorarios();
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

        if (!IsPostBack)
        {
            try
            {
                // Captura Datos
                Session.Add("lsIdHora", Request.QueryString["key"].ToString());
                this.hdIdHora.Value = Request.QueryString["key"].ToString();
                //cargaTurnoSemana();
                //lsGrabar = modfunc.fnValidaUsrApp("BTN_CHK_PAC", gUsr, asCodSistema);
                //if (lsGrabar != "M" && lsGrabar != "L") { this.chkLimpiar.Enabled = false; }

                if (this.hdIdHora.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                    this.btn_habilitar.Enabled = false;
                    this.btn_habilitar.CssClass = "BotonPortalAmarillo";
                }
                else
                {
                    //Session.Add("cadena", modFunciones.DesEncriptar(Request.QueryString["cadena"].ToString()));
                    Session.Add("lbNvo", false);
                    nuevo = false;
                    this.btn_Agregar.Text = "Actualizar";
                    hor.ls_idhora = this.hdIdHora.Value;
                    aoDs = hor.mfConsultarHorario();
                    if (aoDs != null && aoDs.Tables.Count > 0)
                    {
                        if (aoDs.Tables[0].Rows.Count > 0)
                        {
                            this.TxtId.Text = aoDs.Tables[0].Rows[0]["IDHORA"].ToString();
                            this.TxtDescr.Text = aoDs.Tables[0].Rows[0]["DESCRIPCION"].ToString();
                            this.txtFCrea.Text = aoDs.Tables[0].Rows[0]["F_H_CREACION"].ToString();
                            //this.txtCod.Text = aoDs.Tables[0].Rows[0]["CODIGO"].ToString();
                            if (aoDs.Tables[0].Rows[0]["ESTADO"].ToString() == "ACTIVO")
                            {
                                this.lbEstado.Text = "ACTIVO";
                                this.btn_habilitar.Text = "Deshabilitar";
                            }
                            else
                            {
                                this.lbEstado.Text = "INACTIVO";
                                this.btn_habilitar.Text = "Habilitar";
                            }
                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["IDHORA"].ToString();
                            //mfCargaUser();
                            //mfCargaUserDisp();
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
    protected void btn_Agregar_Click(object sender, EventArgs e)
    {
        mfAgregar();
    }
    private void mfAgregar()
    {
        //Validaciones
        if (!ValidarCampos()) { return; }
        //Revisar si existe turno
        string lsRet = "";
        string lsID = "";
        if (nuevo)
        {
            hor.ls_user = Session["user"].ToString();
            hor.ls_descrip = this.TxtDescr.Text;
            lsID = hor.mfCrearHorario();
        }
        else
        {
            hor.ls_user = Session["user"].ToString();
            hor.ls_idhora = this.hdIdHora.Value;
            hor.ls_descrip = this.TxtDescr.Text;
            lsRet = hor.mfUpdateHorario();
        }

        if (lsID != "")
        {
            this.hdIdHora.Value = lsID;
            Session.Add("lbNvo", false);
            this.TxtId.Text = lsID;
            this.lbEstado.Text = "ACTIVO";
            this.btn_habilitar.Text = "Deshabilitar";
        }

        if (lsRet != "" && lsID == "")
        {
            mens.mensaje(Page, "Error: Problemas al Insertar y/o Modificar el Registro.");
        }
        else
        {
            mens.mensaje(Page, "Registro Ingresado y/o Actualizado con Exito.. ");
        }
    }
    public bool ValidarCampos()
    {
        if (this.TxtDescr.Text == "")
        {
            mens.mensaje(Page, "Debe Escribir una Descripción");
            return false;
        }
        return true;
    }
    #region Botones
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        if (Session["cadena"] == null)
        {
            Response.Redirect("~/contenido/GestionRRHH/ListaHorarios.aspx");
        }
        else
        {
            Response.Redirect("~/contenido/GestionRRHH/ListaHorarios.aspx?" + Session["cadena"].ToString());
        }
    }
    protected void btn_habilitar_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            if (confirmValue == "Yes")
            {
                CambiarEstadoHorario();
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    protected void btnGuardarDetalle_Click(object sender, EventArgs e)
    {
        string ret = "";

        hor.ls_user = Session["user"].ToString();
        hor.ls_idhora = this.hdIdHora.Value;
        hor.ls_hora = this.txtHoras.Text;
        hor.ls_minuto = this.txtMinuto.Text;
        hor.ls_horaini = this.ddlHoraEntrada.SelectedValue;
        hor.ls_horafin = this.ddlHoraSalida.SelectedValue;
        ret = hor.mfUpdateHorarioDet();
        if (ret != "")
        {
            this.lblResultado.Text = " Ha Ocurrido un error: " + ret;
            return;
        }
        mens.mensaje(Page, "Horario Actualizado con Exito.. ");
        this.lblResultado.Text = "Horario Actualizado con Exito..";
    }
    protected void btnCalcular_Click(object sender, EventArgs e)
    {
        int intervalo = Convert.ToInt32(this.txtint.Text);
        CargarHoras(intervalo);
    }
    #endregion

    #region Horarios
    private void CambiarEstadoHorario()
    {
        string asEstado = "1";
        if (this.lbEstado.Text == "ACTIVO") asEstado = "3";
        hor.ls_estado = asEstado;
        hor.ls_user = Session["user"].ToString();
        hor.ls_idhora = this.hdIdHora.Value;
        string lsRet = hor.mfEstadoHorario();
        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {
            if (asEstado == "3")
            {
                this.lbEstado.Text = "INACTIVO";
                this.btn_habilitar.Text = "Habilitar";
            }
            else
            {
                this.lbEstado.Text = "ACTIVO";
                this.btn_habilitar.Text = "Deshabilitar";
            }
            mens.mensaje(Page, "Registro Actualizado con Exito.. ");
        }
    }


    protected void CalcularHorario(object sender, EventArgs e)
    {
        DateTime inicio = DateTime.Parse(ddlHoraEntrada.SelectedValue);
        DateTime fin = DateTime.Parse(ddlHoraSalida.SelectedValue);

        if (fin < inicio)
            fin = fin.AddDays(1);

        TimeSpan ts = fin - inicio;

        txtHoras.Text = ts.Hours.ToString();
        txtMinuto.Text = ts.Minutes.ToString("00");
    }

    private void CargarHoras(int intervalo)
    {
        ddlHoraEntrada.Items.Clear();
        ddlHoraSalida.Items.Clear();
        txtHoras.Text = "";
        txtMinuto.Text = "";
        DateTime hora = DateTime.Today;
        while (hora < DateTime.Today.AddDays(1))
        {
            string texto = hora.ToString("HH:mm");
            ddlHoraEntrada.Items.Add(new ListItem(texto, texto));
            ddlHoraSalida.Items.Add(new ListItem(texto, texto));
            hora = hora.AddMinutes(intervalo);
        }
    }

    #endregion
}