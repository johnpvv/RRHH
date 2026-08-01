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
                Session.Add("lsIdTurno", Request.QueryString["key"].ToString());
                this.hdIdTurno.Value = Request.QueryString["key"].ToString();
                cargaTurnoSemana();
                //lsGrabar = modfunc.fnValidaUsrApp("BTN_CHK_PAC", gUsr, asCodSistema);
                //if (lsGrabar != "M" && lsGrabar != "L") { this.chkLimpiar.Enabled = false; }

                if (this.hdIdTurno.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                }
                else
                {
                    //Session.Add("cadena", modFunciones.DesEncriptar(Request.QueryString["cadena"].ToString()));
                    Session.Add("lbNvo", false);
                    nuevo = false;
                    this.btn_Agregar.Text = "Actualizar";
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
                                this.btn_habilitar.Text = "Deshabilitar";
                            }
                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["CODIGO"].ToString();
                            mfCargaUser();
                            mfCargaUserDisp();
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
    #region Botones
    protected void btnVolver_Click(object sender, EventArgs e)
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
    protected void btnGuardarDetalle_Click(object sender, EventArgs e)
    {
        string ret = "";
        foreach (GridViewRow row in dgSemana.Rows)
        {
            string idTurnoDia = dgSemana.DataKeys[row.RowIndex]["IDTURNODIA"].ToString();
            string idDia = dgSemana.DataKeys[row.RowIndex]["IDDIA"].ToString();
            CheckBox chkTrabaja = (CheckBox)row.FindControl("chkTrabaja");
            DropDownList ddlHorario = (DropDownList)row.FindControl("ddlHorario");
            bool trabaja = chkTrabaja.Checked;
            int idHorario = 0;
            if (ddlHorario.SelectedValue != "")
                idHorario = Convert.ToInt32(ddlHorario.SelectedValue);
            tur.ls_idturnodia = idTurnoDia;
            tur.ls_idturno = this.hdIdTurno.Value;
            tur.ls_iddia = idDia;
            tur.ls_idhora = idHorario.ToString();
            ret += tur.mfGuardarDiaTurno(trabaja);
        }
        if (ret != "")
        {
            this.lblResultado.Text = " Ha Ocurrido un error: " + ret;
            return;
        }
        mens.mensaje(Page, "Turno Actualizado con Exito.. ");
        this.lblResultado.Text = "Turno Actualizado con Exito..";
    }
    #endregion
    #region Turnos
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
            TextBox txtHr = (TextBox)e.Row.FindControl("txtHr");
            DataRowView dr = (DataRowView)e.Row.DataItem;
            //Llenar horarios
            ddl.DataSource = hor.mfBuscarHoras().Tables[0];
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
                txtHr.Text = dr["HORA"].ToString() + "h, " + dr["MINUTO"].ToString() + "m";
            }
            else
            {
                chk.Checked = false;
                ddl.Enabled = false;
                txtIni.Text = "";
                txtFin.Text = "";
                txtHr.Text = "";
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
            ((TextBox)row.FindControl("txtHr")).Text = "";
        }
    }
    protected void ddlHorario_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddl.NamingContainer;
        TextBox txtIni = (TextBox)row.FindControl("txtIni");
        TextBox txtFin = (TextBox)row.FindControl("txtFin");
        TextBox txtHr = (TextBox)row.FindControl("txtHr");
        if (ddl.SelectedValue == "")
        {
            txtIni.Text = "";
            txtFin.Text = "";
            txtHr.Text = "";
            return;
        }
        hor.ls_hora = ddl.SelectedValue;
        DataSet ds = hor.mfBuscarHoraID();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtIni.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["HORA_INI"]).ToString("HH:mm");
            txtFin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["HORA_FIN"]).ToString("HH:mm");
            txtHr.Text = ds.Tables[0].Rows[0]["HORA"].ToString() + "h, " + ds.Tables[0].Rows[0]["MINUTO"].ToString() + "m";
        }
    }
    #endregion
    #region User Turnos
    protected void AddUser(object sender, EventArgs e)
    {
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        mfAgregarUser(cid);
    }
    protected void BtIngUser_Click(object sender, EventArgs e)
    {
        try
        {
            mfCargaUserDisp();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    private void mfCargaUser()
    {
        DataSet aoDsUser;
        tur.ls_idturno = this.hdIdTurno.Value;
        tur.ls_rut = this.TRut.Text;
        tur.ls_nombre = this.TNombreUsr.Text;
        aoDsUser = tur.mfBuscarUserTurno();
        gbUser.DataSource = aoDsUser;
        gbUser.DataBind();
    }
    private void mfCargaUserDisp()
    {
        DataSet aoDsDisp;
        tur.ls_idturno = this.hdIdTurno.Value;
        tur.ls_rut = this.TRut.Text;
        tur.ls_nombre = this.TNombreUsr.Text;
        aoDsDisp = tur.mfBuscarUserDisp();
        gbUserDisp.DataSource = aoDsDisp;
        gbUserDisp.DataBind();
    }
    private void mfAgregarUser(string asIdentificador)
    {
        string lsRet = "";
        tur.ls_idturno = this.hdIdTurno.Value;
        tur.ls_user = asIdentificador;
        tur.mfAgregarUserTurno();
        if (lsRet != "")
        {
            mens.mensaje(Page, "Error: NO se pudo insertar Profesional");
        }
        else
        {
            mfCargaUser();
            mfCargaUserDisp();
        }
    }
    protected void ElimUser(object sender, EventArgs e)
    {
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        mfElimUser(cid);
    }
    private void mfElimUser(string asIdentificador)
    {
        string lsRet = "";
        
        tur.ls_idturno = hdIdTurno.Value;
        tur.ls_user = asIdentificador;
        tur.ls_iduselim = Session["user"].ToString();
        lsRet= tur.mfQuitarUserTurno();

        if (lsRet != "")
        {
            mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
        }
        else
        {
            mfCargaUser();
            mfCargaUserDisp();
        }

    }
    protected void gbUser_SelectedIndexChanged(object sender, EventArgs e)
    {
        mfAgregarUser(gbUser.DataKeys[gbUser.SelectedIndex].Values[0].ToString());
    }
    protected void gbUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            //e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('dvUser','Select${0}')", e.Row.RowIndex));
        }
    }
    protected void gbUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbUser.PageIndex = e.NewPageIndex;
        mfCargaUser();
    }
    protected void gbUserDisp_SelectedIndexChanged(object sender, EventArgs e)
    {
        mfElimUser(gbUserDisp.DataKeys[gbUserDisp.SelectedIndex].Values[0].ToString());
    }
    protected void gbUserDisp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.            
        }
    }
    protected void gbUserDisp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbUserDisp.PageIndex = e.NewPageIndex;
        mfCargaUserDisp();
    }
    protected void BtBuscarUser_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.rbLista.SelectedValue == "1")
                mfCargaUserDisp();
            else
                mfCargaUser();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    #endregion
}