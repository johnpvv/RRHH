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
                CargarMeses();
                CargarAnios();
                //lsGrabar = modfunc.fnValidaUsrApp("BTN_CHK_PAC", gUsr, asCodSistema);
                //if (lsGrabar != "M" && lsGrabar != "L") { this.chkLimpiar.Enabled = false; }

                if (this.hdIdTurno.Value == "0")
                {
                    Session.Add("lbNvo", true);
                    nuevo = true;
                    this.btn_habilitar.Enabled = false;
                    this.TabPanel2.Enabled = false;
                    this.TabPanel3.Enabled = false;
                    this.TabPanel4.Enabled = false;
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
                            if (aoDs.Tables[0].Rows[0]["FERIADOS"].ToString() == "1")
                            {
                                this.chkFer.Checked = true;
                            }
                            if (aoDs.Tables[0].Rows[0]["TIPO_TURNO"].ToString() == "1")
                            {
                                this.chkTipo.Checked = true;
                                this.TabPanel2.Enabled = false;
                                this.TabPanel2.Visible = false;
                                mfDevuelveFecha();//ssaber que mes y año es el turno para desplegar el grid
                                cargaTurnoMes();
                            }
                            else
                            {
                                this.TabPanel3.Enabled = false;
                                this.TabPanel3.Visible = false;
                                cargaTurnoSemana();
                            }
                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["CODIGO"].ToString();
                            tur.ls_turno = hdIdTurno.Value;
                            mfActivaBtn(tur.mfTieneUsrAsig());//activar/desactivar botones si tiene usuarios asignados el turno
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
    #region General
    private void CargarMeses()
    {
        DataSet ds = tur.mfGenerarMeses();
        ddlMes.DataSource = ds.Tables[0];
        ddlMes.DataTextField = "MES";
        ddlMes.DataValueField = "IDMES";
        ddlMes.DataBind();
        ddlMes.SelectedValue = DateTime.Now.Month.ToString();
    }

    private void CargarAnios()
    {
        ddlAnio.Items.Clear();
        DataSet ds = tur.mfGenerarAnios();
        ddlAnio.DataSource = ds.Tables[0];
        ddlAnio.DataTextField = "ANIO";
        ddlAnio.DataValueField = "ID";
        ddlAnio.DataBind();
        ddlAnio.SelectedValue = DateTime.Now.Year.ToString();

    }
    protected void btn_Agregar_Click(object sender, EventArgs e)
    {
        mfAgregar();
    }
    private void mfAgregar()
    {
        //Validaciones
        if (!ValidarCampos()) { return; }

        string lsRet = "";
        string lsID = "";
        tur.ls_descrip = this.TxtDescr.Text.Trim();
        tur.ls_codigo = this.txtCod.Text.Trim().ToUpper();
        tur.ls_idturno = this.hdIdTurno.Value;

        if (this.chkFer.Checked)
        {
            tur.ls_fer = "1";
        }
        else
        {
            tur.ls_fer = "0";
        }
        if (this.chkTipo.Checked)
        {
            tur.ls_tipo = "1";
            this.TabPanel3.Enabled = true;
        }
        else
        {
            tur.ls_tipo = "0";
            this.TabPanel2.Enabled = true;
        }

        if (nuevo)
        {
            lsID = tur.mfCrearTurnos();
        }
        else
        {
            lsRet = tur.mfUpdateTurnos();
        }

        if (lsID != "")
        {
            this.hdIdTurno.Value = lsID;
            Session.Add("lbNvo", false);
            this.TxtId.Text = lsID;
            this.lbEstado.Text = "ACTIVO";
            this.btn_habilitar.Text = "Deshabilitar";
            this.TabPanel4.Enabled = true;
            mfCargaUser();
            mfCargaUserDisp();
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
        if (this.TxtDescr.Text.Trim() == "")
        {
            mens.mensaje(Page, "Debe Escribir una Descripción del Turno");
            return false;
        }
        if (this.txtCod.Text.Trim() == "")
        {
            mens.mensaje(Page, "Debe Escribir un código válido para el Turno");
            return false;
        }
        tur.ls_codigo = this.txtCod.Text.Trim();
        if (tur.mfDevuelveIDTurno() != "" && tur.mfDevuelveIDTurno() != this.hdIdTurno.Value)
        {
            mens.mensaje(Page, "El código escrito, ya existe, pruebe uno diferente.");
            return false;
        }
        return true;
    }
    private void mfActivaBtn(bool bloquear)
    {
        this.btnGenerarMes.Enabled = !bloquear;
        this.btnAplicarPatron.Enabled = !bloquear;
        this.btnGuardarMes.Enabled = !bloquear;
        this.btn_habilitar.Enabled = !bloquear;
        this.btnGuardarDetalle.Enabled = !bloquear;
        this.ddlPatron.Enabled = !bloquear;
        this.ddlMes.Enabled = !bloquear;
        this.ddlAnio.Enabled = !bloquear;
        this.chkTipo.Enabled = !bloquear;
    }
    #endregion

    #region Botones
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        if (Session["cadena"] == null)
        {
            Response.Redirect("~/contenido/GestionRRHH/ListaTurnos.aspx");
        }
        else
        {
            Response.Redirect("~/contenido/GestionRRHH/ListaTurnos.aspx?" + Session["cadena"].ToString());
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
            this.lblResultado.Text = " <img src='../../../imagenes/close.png'/> Ha Ocurrido un error: " + ret;
            return;
        }
        mens.mensaje(Page, "Turno Actualizado con Exito.. ");
        this.lblResultado.Text = "<img src='../../../imagenes/check.png'/> Turno Actualizado con Exito..";
    }
    #endregion

    #region Turno por dia
    private void CambiarEstadoTurno()
    {
        string asEstado = "2";
        if (this.lbEstado.Text == "VIGENTE")
        {
            asEstado = "3";
        }
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
        hor.ls_idhora = ddl.SelectedValue;
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
        lsRet = tur.mfQuitarUserTurno();

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

    #region Turno Mensual

    public DataTable GenerarMes(int anio, int mes)
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("IDTURNODIA", typeof(int));
        dt.Columns.Add("FECHA", typeof(DateTime));
        dt.Columns.Add("IDDIA", typeof(int));
        dt.Columns.Add("DIA", typeof(string));
        dt.Columns.Add("IDHORA", typeof(int));

        DateTime fechaInicio = new DateTime(anio, mes, 1);
        int diasMes = DateTime.DaysInMonth(anio, mes);

        for (int i = 0; i < diasMes; i++)
        {
            DateTime fecha = fechaInicio.AddDays(i);
            DataRow fila = dt.NewRow();
            fila["IDTURNODIA"] = 0;
            fila["FECHA"] = fecha;
            int idDia;

            if (fecha.DayOfWeek == DayOfWeek.Sunday)
                idDia = 7;
            else
                idDia = (int)fecha.DayOfWeek;
            fila["IDDIA"] = idDia;
            fila["DIA"] = fecha.ToString("dddd").ToUpper();
            fila["IDHORA"] = 0;

            dt.Rows.Add(fila);
        }
        return dt;
    }
    protected void btnGenerarMes_Click(object sender, EventArgs e)
    {
        mfLlenaMes();
    }
    protected void mfLlenaMes()
    {
        int anio;
        int mes;

        if (!int.TryParse(ddlAnio.SelectedValue, out anio))
        {
            mens.mensaje(Page, "Debe ingresar un año válido.");
            return;
        }

        if (anio < 2020 || anio > 2100)
        {
            mens.mensaje(Page, "El año ingresado no es válido.");
            return;
        }

        if (!int.TryParse(ddlMes.SelectedValue, out mes))
        {
            mens.mensaje(Page, "Debe seleccionar un mes.");
            return;
        }
        DataTable dt = GenerarMes(anio, mes);
        dgMes.DataSource = dt;
        dgMes.DataBind();
    }
    private void cargaTurnoMes()
    {
        //lenamos el grid con el mes correspondiente
        mfLlenaMes();

        //pintamos lo de la bd
        tur.ls_turno = this.hdIdTurno.Value;
        DataSet dsDetalle = tur.mfBuscarDetalleTurnoMes();

        foreach (GridViewRow fila in dgMes.Rows)
        {
            HiddenField hdFecha = (HiddenField)fila.FindControl("hdFecha");
            CheckBox chk = (CheckBox)fila.FindControl("chkTrabajaMes");
            DropDownList ddl = (DropDownList)fila.FindControl("ddlHorarioMes");
            TextBox txtIni = (TextBox)fila.FindControl("txtIniMes");
            TextBox txtFin = (TextBox)fila.FindControl("txtFinMes");
            TextBox txtHr = (TextBox)fila.FindControl("txtHrMes");
            DateTime fechaGrid = Convert.ToDateTime(hdFecha.Value);
            foreach (DataRow dr in dsDetalle.Tables[0].Rows)
            {
                DateTime fechaBD = Convert.ToDateTime(dr["FECHA"]);
                if (fechaGrid.Date == fechaBD.Date)
                {
                    chk.Checked = true;
                    ddl.Enabled = true;
                    ddl.SelectedValue = dr["IDHORA"].ToString();
                    txtIni.Text = Convert.ToDateTime(dr["HORA_INI"]).ToString("HH:mm");
                    txtFin.Text = Convert.ToDateTime(dr["HORA_FIN"]).ToString("HH:mm");
                    txtHr.Text = dr["HORA"].ToString() + "h, " + dr["MINUTO"].ToString() + "m";
                    break;
                }
            }
        }
        CalcularTotalMes();//rellena el label con el total de horas a trabajar en el mes
    }
    protected void dgMes_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow)
            return;
        DateTime fecha = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "FECHA"));

        // Identificar fin de semana
        if (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
        {
            e.Row.CssClass = "GridMesFinSemana";
        }

        DropDownList ddlHorario = (DropDownList)e.Row.FindControl("ddlHorarioMes");
        CheckBox chkTrabaja = (CheckBox)e.Row.FindControl("chkTrabajaMes");
        TextBox txtIni = (TextBox)e.Row.FindControl("txtIniMes");
        TextBox txtFin = (TextBox)e.Row.FindControl("txtFinMes");
        if (ddlHorario == null || chkTrabaja == null)
            return;

        // Cargar horarios
        DataSet dsHorarios = hor.mfBuscarHorarios();
        ddlHorario.Items.Clear();
        if (dsHorarios != null && dsHorarios.Tables.Count > 0 && dsHorarios.Tables[0].Rows.Count > 0)
        {
            ddlHorario.DataSource = dsHorarios.Tables[0];
            ddlHorario.DataTextField = "DESCRIPCION";
            ddlHorario.DataValueField = "IDHORA";
            ddlHorario.DataBind();
        }
        ddlHorario.Items.Insert(0, new ListItem("-- Seleccione horario --", "0"));
        // Por defecto no trabaja
        chkTrabaja.Checked = false;
        ddlHorario.Enabled = false;
        if (txtIni != null)
            txtIni.Enabled = false;
        if (txtFin != null)
            txtFin.Enabled = false;
    }

    protected void ddlHorarioMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlHorario = (DropDownList)sender;
        GridViewRow row = (GridViewRow)ddlHorario.NamingContainer;
        TextBox txtIni = (TextBox)row.FindControl("txtIniMes");
        TextBox txtFin = (TextBox)row.FindControl("txtFinMes");
        TextBox txtHr = (TextBox)row.FindControl("txtHrMes");
        CargarHorarioFila(ddlHorario, txtIni, txtFin, txtHr);
        CalcularTotalMes();//rellena el label con el total de horas a trabajar en el mes
    }

    private void CargarHorarioFila(DropDownList ddlHorario, TextBox txtIni, TextBox txtFin, TextBox txtHr)
    {
        if (ddlHorario == null)
            return;

        if (ddlHorario.SelectedValue == "0" || ddlHorario.SelectedValue == "")
        {
            txtIni.Text = "";
            txtFin.Text = "";
            txtHr.Text = "";
            return;
        }
        hor.ls_idhora = ddlHorario.SelectedValue;
        DataSet ds = hor.mfBuscarHoraID();
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtIni.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["HORA_INI"]).ToString("HH:mm");
            txtFin.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["HORA_FIN"]).ToString("HH:mm");
            txtHr.Text = ds.Tables[0].Rows[0]["HORA"].ToString() + "h, " + ds.Tables[0].Rows[0]["MINUTO"].ToString() + "m";
        }
    }
    protected void chkTrabajaMes_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = (CheckBox)sender;
        GridViewRow fila = (GridViewRow)chk.NamingContainer;
        DropDownList ddlHorario = (DropDownList)fila.FindControl("ddlHorarioMes");
        TextBox txtIni = (TextBox)fila.FindControl("txtIniMes");
        TextBox txtFin = (TextBox)fila.FindControl("txtFinMes");
        TextBox txtHr = (TextBox)fila.FindControl("txtHrMes");

        if (chk.Checked)
        {
            ddlHorario.Enabled = true;
        }
        else
        {
            ddlHorario.Enabled = false;
            ddlHorario.SelectedValue = "0";
            txtIni.Text = "";
            txtFin.Text = "";
            txtHr.Text = "";
            CalcularTotalMes();//rellena el label con el total de horas a trabajar en el mes, si se elimina algun dia
        }
        txtIni.Enabled = false;
        txtFin.Enabled = false;
    }
    private void mfAplicPatron(int diasTrabajo, int diasLibre)//revisar, si se hace dinamico segun una tabla tipo????? en duro por ahora
    {
        bool trabajando = true;
        int contador = 0;
        string idHoraBase = "";

        // Obtener horario del primer día si esta seleccionado
        if (dgMes.Rows.Count > 0)
        {
            DropDownList ddlPrimero = (DropDownList)dgMes.Rows[0].FindControl("ddlHorarioMes");
            if (ddlPrimero != null)
            {
                idHoraBase = ddlPrimero.SelectedValue;
            }
        }

        foreach (GridViewRow fila in dgMes.Rows)
        {
            if (fila.RowType != DataControlRowType.DataRow)
                continue;

            CheckBox chk = (CheckBox)fila.FindControl("chkTrabajaMes");
            DropDownList ddl = (DropDownList)fila.FindControl("ddlHorarioMes");
            TextBox txtIni = (TextBox)fila.FindControl("txtIniMes");
            TextBox txtFin = (TextBox)fila.FindControl("txtFinMes");
            TextBox txtHr = (TextBox)fila.FindControl("txtHrMes");
            if (chk == null || ddl == null)
                continue;

            if (trabajando)
            {
                chk.Checked = true;
                ddl.Enabled = true;
                ddl.SelectedValue = idHoraBase;
                CargarHorarioFila(ddl, txtIni, txtFin, txtHr);
                contador++;
                if (contador >= diasTrabajo)
                {
                    contador = 0;
                    trabajando = false;
                }
            }
            else
            {
                chk.Checked = false;
                ddl.Enabled = false;
                ddl.SelectedValue = "0";
                if (txtIni != null)
                    txtIni.Text = "";
                if (txtFin != null)
                    txtFin.Text = "";
                if (txtHr != null)
                    txtHr.Text = "";
                contador++;

                if (contador >= diasLibre)
                {
                    contador = 0;
                    trabajando = true;
                }
            }
        }
        CalcularTotalMes();//rellena el label con el total de horas a trabajar en el mes, al aplicar un patron de turnos
    }
    protected void btnAplicarPatron_Click(object sender, EventArgs e)
    {
        if (ddlPatron.SelectedValue == "0")
        {
            mens.mensaje(Page, "Debe seleccionar un patrón.");
            return;
        }

        string[] patron = ddlPatron.SelectedValue.Split('-');

        int diasTrabajo;
        int diasLibre;

        if (!int.TryParse(patron[0], out diasTrabajo) || !int.TryParse(patron[1], out diasLibre))
        {
            mens.mensaje(Page, "El patrón seleccionado no es válido.");
            return;
        }
        mfAplicPatron(diasTrabajo, diasLibre);//metodo aplica patron para rellenar el grid
    }
    private void CalcularTotalMes()
    {
        int totalMinutos = 0;
        foreach (GridViewRow fila in dgMes.Rows)
        {
            if (fila.RowType != DataControlRowType.DataRow)
            {
                continue;
            }
            CheckBox chk = (CheckBox)fila.FindControl("chkTrabajaMes");
            DropDownList ddl = (DropDownList)fila.FindControl("ddlHorarioMes");

            if (chk == null || ddl == null)
                continue;

            if (chk.Checked && ddl.SelectedValue != "0")
            {
                totalMinutos += ObtenerMinutosHorario(ddl.SelectedValue);
            }
        }
        int totalHoras = totalMinutos / 60;
        int minutos = totalMinutos % 60;
        lblTotalMes.Text = "Total Jornada del mes: " + totalHoras + " horas, " + minutos + " minutos";
    }
    private int ObtenerMinutosHorario(string idHora)
    {
        if (string.IsNullOrEmpty(idHora) || idHora == "0")
            return 0;

        hor.ls_idhora = idHora;
        DataSet ds = hor.mfBuscarHoraID();

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            int horas = Convert.ToInt32(ds.Tables[0].Rows[0]["HORA"]);
            int minutos = Convert.ToInt32(ds.Tables[0].Rows[0]["MINUTO"]);
            return (horas * 60) + minutos;//devuelve el tiempo en minutos
        }
        return 0;
    }

    private string GuardarDetalleMes()
    {
        int idTurno;
        if (!int.TryParse(this.hdIdTurno.Value, out idTurno))
            return "El ID del turno no es válido.";

        DataSet dsDetalle = new DataSet();
        DataTable dt = new DataTable("DETALLE");

        dt.Columns.Add("IDDIA", typeof(int));
        dt.Columns.Add("IDHORA", typeof(int));
        dt.Columns.Add("FECHA", typeof(DateTime));

        foreach (GridViewRow fila in dgMes.Rows)
        {
            if (fila.RowType != DataControlRowType.DataRow)
            {
                continue;
            }
            CheckBox chk = (CheckBox)fila.FindControl("chkTrabajaMes");
            DropDownList ddl = (DropDownList)fila.FindControl("ddlHorarioMes");
            HiddenField hdIdDia = (HiddenField)fila.FindControl("hdIdDia");
            HiddenField hdFecha = (HiddenField)fila.FindControl("hdFecha");

            if (chk == null || ddl == null || hdIdDia == null || hdFecha == null)
                continue;

            if (!chk.Checked)// Si no trabaja, no se agrega
                continue;
            // Si trabaja, debe tener horario
            if (ddl.SelectedValue == "0" || string.IsNullOrEmpty(ddl.SelectedValue))
            {
                return "Existe un día trabajado sin horario seleccionado.";
            }

            int idDia;
            int idHora;
            DateTime fecha;

            if (!int.TryParse(hdIdDia.Value, out idDia))
                return "Existe un día con ID inválido.";
            if (!int.TryParse(ddl.SelectedValue, out idHora))
                return "Existe un horario inválido.";
            if (!DateTime.TryParse(hdFecha.Value, out fecha))
                return "Existe una fecha inválida.";

            DataRow dr = dt.NewRow();
            dr["IDDIA"] = idDia;
            dr["IDHORA"] = idHora;
            dr["FECHA"] = fecha;
            dt.Rows.Add(dr);
        }
        if (dt.Rows.Count == 0)
            return "Debe seleccionar al menos un día trabajado.";

        dsDetalle.Tables.Add(dt);
        tur.ls_idturno = this.hdIdTurno.Value;
        return tur.mfGuardarDetalleMes(dsDetalle);
    }
    protected void btnGuardarDetalleMes_Click(object sender, EventArgs e)
    {
        string lsRet;
        lsRet = GuardarDetalleMes();
        if (lsRet != "")
        {
            mens.mensaje(Page, "Error: " + lsRet);
            this.lblResultadoM.Text = "<img src='../../../imagenes/close.png'/>  Ha Ocurrido un error: " + lsRet;
            return;
        }
        mens.mensaje(Page, "Turno mensual guardado correctamente.");
        this.lblResultadoM.Text = "<img src='../../../imagenes/check.png'/> Turno mensual guardado correctamente...";
    }
    protected void mfDevuelveFecha()
    {
        tur.ls_idturno = this.hdIdTurno.Value;
        DataSet dsFecha = tur.mfDevuelveFechaTurnoMes();

        if (dsFecha != null && dsFecha.Tables.Count > 0 && dsFecha.Tables[0].Rows.Count > 0)
        {
            DateTime fecha = Convert.ToDateTime(dsFecha.Tables[0].Rows[0]["FECHA"]);
            ddlAnio.SelectedValue = fecha.Year.ToString();
            ddlMes.SelectedValue = fecha.Month.ToString();
        }
    }

    #endregion
}