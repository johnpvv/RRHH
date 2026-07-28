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

public partial class contenido_GestionRRHH_GestionTurnos : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    Usuarios usr = new Usuarios();
    ClassTurnos tur = new ClassTurnos();
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
        //if (IsPostBack)
        //{
        //    if (Request.Params["__EVENTTARGET"] == "ExisteRutPostBack")
        //    {
        //        mfExistePersona();
        //    }
        //}

        if (!IsPostBack)
        {
            try
            {
                // Captura Datos
                Session.Add("lsIdTurno", Request.QueryString["key"].ToString());
                this.hdIdTurno.Value = Request.QueryString["key"].ToString();
                cargaTurnoSemana();

                //lsGrabar = modfunc.fnValidaUsrApp("BTN_CHK_PAC", gUsr, asCodSistema);
                //if (lsGrabar != "M" && lsGrabar != "L") { this.chkLimpiar.Enabled = false; }

                //LlenarPrevision();
                //LlenarRegion();
                //LlenarComuna();
                //LlenarEstCivil();

                if (this.hdIdTurno.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                    //this.TxtRut.Enabled = true;
                    //this.TxtDv.Enabled = true;
                    //this.btn_habilitar.Enabled = false;
                    //this.TxtRut.Focus();
                }
                else
                {
                    //Session.Add("cadena", modFunciones.DesEncriptar(Request.QueryString["cadena"].ToString()));
                    //Session.Add("lbNvo", false);

                    nuevo = false;
                    //this.TxtRut.Enabled = false;
                    //this.TxtDv.Enabled = false;
                    this.btn_Agregar.Text = "Actualizar";

                    //per.ls_rut = this.hdIdTurno.Value;
                    tur.ls_turno = this.hdIdTurno.Value;
                    tur.ls_codigo = "";
                    tur.ls_descrip = "";

                    aoDs = tur.mfBuscarTurnos();

                    if (aoDs != null && aoDs.Tables.Count > 0)
                    {
                        if (aoDs.Tables[0].Rows.Count > 0)
                        {

                            this.TxtId.Text = aoDs.Tables[0].Rows[0]["IDTURNOS"].ToString();
                            this.TxtDescr.Text = aoDs.Tables[0].Rows[0]["DESCRIPCION"].ToString();
                            this.txtFCrea.Text = aoDs.Tables[0].Rows[0]["F_H_CREACION"].ToString();
                            this.txtCod.Text = aoDs.Tables[0].Rows[0]["CODIGO"].ToString();

                            if (aoDs.Tables[0].Rows[0]["ESTADO"].ToString() == "VIGENTE")
                            {
                                this.lbEstado.Text = "VIGENTE";
                                this.btn_habilitar.Text = "Deshabilitar";
                            }
                            else
                            {
                                this.lbEstado.Text = "REVISAR ESTADO";
                            }

                            //this.ddlPrevision.SelectedValue = per.mfIdPrevisionPaciente(this.hdIdTurno.Value);
                            //this.ddlRegion.SelectedValue = per.mfIdRegionPaciente(this.hdIdTurno.Value);
                            //this.ddlComuna.SelectedValue = per.mfIdComunaPaciente(this.hdIdTurno.Value);

                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["CODIGO"].ToString();

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


        //Revisar si existe RUT
        if (nuevo)
        {
            //per.ls_rut = this.TxtRut.Text;
            //if (Convert.ToInt32(per.mfExistePersona()) > 0)
            //{
            //    mens.mensaje(Page, "RUT ya existe, por favor verificar");
            //    return;
            //}

            //if (!ValidaRut(TxtRut.Text, TxtDv.Text))
            //{
            //    mens.mensaje(Page, "RUT NO VALIDO, por favor verificar");
            //    return;
            //};
        }


        string lsRet = "";
        tur.ls_descrip = this.TxtDescr.Text;
        tur.ls_codigo = this.txtCod.Text;
        tur.ls_turno = this.hdIdTurno.Value;
        lsRet = tur.mfUpdateTurnos();

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Modificar el Registro.");
        else
        {
            //if (this.hdIdTurno.Value == "0")
            //{
            //    this.hdIdTurno.Value = per.ls_rut;
            //    LbTitulo.Text = per.ls_rut + "-" + per.ls_dv;
            //}
            mens.mensaje(Page, "Registro ingresado con Exito.. ");
        }
    }

    public bool ValidarCampos()
    {

        //if (this.ddlPrevision.SelectedIndex == 0)
        //{
        //    mens.mensaje(Page, "Debe seleccionar Prevision");
        //    return false;
        //}

        //if (this.ddlRegion.SelectedIndex == 0)
        //{
        //    mens.mensaje(Page, "Debe seleccionar Region");
        //    return false;
        //}

        //if (this.ddlComuna.SelectedIndex == 0)
        //{
        //    mens.mensaje(Page, "Debe seleccionar Comuna");
        //    return false;
        //}
        return true;
    }

    protected void ImgBtnBack_Click(object sender, ImageClickEventArgs e)
    {

        if (Session["cadena"] == null)
        {
            Response.Redirect("~/contenido/GestionRRHH/ListaTurnos.aspx");
        }
        else
        {
            Response.Redirect("~/contenido/GestionRRHH/ListaTurnoss.aspx?" + Session["cadena"].ToString());
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
                CambiarEstadoTurno();
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }



    private void CambiarEstadoTurno()
    {
        string asEstado = "2";

        if (this.lbEstado.Text == "VIGENTE") asEstado = "3";

        //string lsRet = per.UpdateEstado(Session["lsIdentificador"].ToString(), asEstado);
        //string lsRet = per.UpdateEstado(this.hdIdTurno.Value, asEstado);

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
            mens.mensaje(Page, "Registro Actualizado con Exito.. ");
        }
    }

    private void cargaTurnoSemana()
    {
        tur.ls_turno = this.hdIdTurno.Value;
        dgSemana.DataSource = tur.mfBuscarTurnoDia();
        dgSemana.DataBind();
    }

    protected void dgSemana_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlHorario");
            CheckBox chk = (CheckBox)e.Row.FindControl("chkTrabaja");
            TextBox txtIni = (TextBox)e.Row.FindControl("txtIni");
            TextBox txtFin = (TextBox)e.Row.FindControl("txtFin");
            DataRowView dr = (DataRowView)e.Row.DataItem;

            //Llenar horarios
            ddl.DataSource = tur.mfBuscarHoras().Tables[0];
            ddl.DataTextField = "DESCRIPCION";
            ddl.DataValueField = "IDHORA";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Seleccione--", ""));
            if (dr["IDHORA"] != DBNull.Value)
            {
                chk.Checked = true;
                ddl.SelectedValue = dr["IDHORA"].ToString();
                txtIni.Text = Convert.ToDateTime(dr["HORA_INI"]).ToString("HH:mm");
                txtFin.Text = Convert.ToDateTime(dr["HORA_FIN"]).ToString("HH:mm");
            }
            else
            {
                chk.Checked = false;
                txtIni.Text = "";
                txtFin.Text = "";
            }
        }
    }
    protected void chkTrabaja_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow row = (GridViewRow)chk.NamingContainer;
        DropDownList ddl = (DropDownList)row.FindControl("ddlHorario");
        ddl.Enabled = chk.Checked;
        if (!chk.Checked)
        {
            ddl.SelectedIndex = 0;
            ((TextBox)row.FindControl("txtIni")).Text = "";
            ((TextBox)row.FindControl("txtFin")).Text = "";
        }
    }
    protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;

        TextBox txtIni = (TextBox)row.FindControl("txtIni");
        TextBox txtFin = (TextBox)row.FindControl("txtFin");

        if (ddl.SelectedValue == "")
        {
            txtIni.Text = "";
            txtFin.Text = "";
            return;
        }
        tur.ls_hora = ddl.SelectedValue;
        DataSet ds = tur.mfBuscarHoraID();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtIni.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["HORA_INI"]).ToString("HH:mm");
            txtFin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["HORA_FIN"]).ToString("HH:mm");
        }
    }
}
