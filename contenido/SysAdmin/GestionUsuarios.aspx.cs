using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.IO;

public partial class contenido_SysAdmin_GestionUsuarios : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();

    ClassModSys modsys = new ClassModSys();
    ClassUnidOperativa cu = new ClassUnidOperativa();

    Usuarios usr = new Usuarios();


    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet aoDs;

        if (IsPostBack)
        {
            if (Request.Params["__EVENTTARGET"] == "ExisteRutPostBack")
            {
                mfExisteUsuario();
            }
        }


        if (!IsPostBack)
        {
            try
            {
                // Captura Datos
                Session.Add("lsIdentificador", Request.QueryString["key"].ToString());
                this.hdIdentificador.Value = Request.QueryString["key"].ToString();


                // if (Session["lsIdentificador"].ToString() == "0")

                if (this.hdIdentificador.Value == "0")
                {

                    Session.Add("lbNvo", true);
                    this.btn_habilitar.Enabled = false;
                    this.TxtRut.Focus();
                }
                else
                {
                    Session.Add("cadena", modFunciones.DesEncriptar(Request.QueryString["cadena"].ToString()));
                    Session.Add("lbNvo", false);

                    // Detalle Alumno
                    //aoDs = modsys.GetUsuarioRut(Session["lsIdentificador"].ToString());
                    aoDs = modsys.GetUsuarioRut(this.hdIdentificador.Value);


                    if (aoDs != null && aoDs.Tables.Count > 0)
                    {
                        if (aoDs.Tables[0].Rows.Count > 0)
                        {
                            this.TxtRut.Text = aoDs.Tables[0].Rows[0]["rut"].ToString();
                            this.TxtDv.Text = aoDs.Tables[0].Rows[0]["dv"].ToString();
                            this.TxtNombre.Text = aoDs.Tables[0].Rows[0]["nombre"].ToString();
                            this.TxtAnexo.Text = aoDs.Tables[0].Rows[0]["telefono"].ToString();
                            this.TxtEmail.Text = aoDs.Tables[0].Rows[0]["email"].ToString();
                            //this.ddlSexo.SelectedValue = aoDs.Tables[0].Rows[0]["SEXO"].ToString();
                            this.TxtDesc.Text = aoDs.Tables[0].Rows[0]["obs"].ToString();
                            this.TEspecialidad.Text = aoDs.Tables[0].Rows[0]["ESPECIALIDAD"].ToString();

                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["rut"].ToString() + "-" + aoDs.Tables[0].Rows[0]["dv"].ToString();

                            if (Convert.ToInt32(aoDs.Tables[0].Rows[0]["IDESTADO"].ToString()) == 3)
                            {
                                this.lbEstado.Text = "NO VIGENTE";
                                this.btn_habilitar.Text = "Habilitar";
                            }
                            else
                            {
                                this.lbEstado.Text = "VIGENTE";
                                this.btn_habilitar.Text = "Deshabilitar";
                            }

                            if (aoDs.Tables[0].Rows[0]["MEDICO"].ToString() == "1")
                                this.chkMed.Checked = true;
                            else
                                this.chkMed.Checked = false;

                            if (aoDs.Tables[0].Rows[0]["FARMACIA"].ToString() == "1")
                                this.chkfarm.Checked = true;
                            else
                                this.chkfarm.Checked = false;

                            if (aoDs.Tables[0].Rows[0]["INFECTOLOGIA"].ToString() == "1")
                                this.chkInf.Checked = true;
                            else
                                this.chkInf.Checked = false;

                            if (aoDs.Tables[0].Rows[0]["SALUD"].ToString() == "1")
                                this.chkSal.Checked = true;
                            else
                                this.chkSal.Checked = false;

                            mfCargarGrillaUserUnidad();
                            mfCargarGrillaUnidad();



                        }
                    }

                }
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }

        }

    }

    protected void TxtRut_TextChanged(object sender, EventArgs e)
    {

        mfExisteUsuario();
        // if (usr.AbastRut(TxtRut.Text) != "")
        //{
        //  mens.mensaje(Page, "RUT ya existe, por favor verificar");
        // }

    }


    #region Usuarios
    private void mfExisteUsuario()
    {

        if (Convert.ToInt32(usr.UserIdentificador(this.TxtRut.Text)) > 0)
        {
            mens.mensaje(Page, "Rut ya tiene cuenta asociada.. ");
            this.TxtRut.Text = "";
        }


    }


    protected void btn_Agregar_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            if (confirmValue == "Yes")
            {

                mfAgregar();
            }

        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    private void mfAgregar()
    {

        int largo = this.TxtDesc.Text.Length;

        if (largo <= 900)
        {
            modsys.lsRut = this.TxtRut.Text;
            modsys.lsDv = this.TxtDv.Text;
            modsys.lsNombre = this.TxtNombre.Text;

            modsys.lsEmail = this.TxtEmail.Text;

            if (this.TxtAnexo.Text == "")
                modsys.lsTelefono = "0";
            else
                modsys.lsTelefono = this.TxtAnexo.Text;

            modsys.lsObs = this.TxtDesc.Text;
            modsys.lsHosp = "1";
            modsys.lsUniOp = "0";
            modsys.lsEspec = this.TEspecialidad.Text;
            //modsys.lsBod = this.ddlBodega.SelectedValue;

            if (this.chkMed.Checked) modsys.lsMed = "1"; else modsys.lsMed = "0";

            if (this.chkfarm.Checked) modsys.lsFarm = "1"; else modsys.lsFarm = "0";

            if (this.chkInf.Checked) modsys.lsInfect = "1"; else modsys.lsInfect = "0";

            if (this.chkSal.Checked) modsys.lsSal = "1"; else modsys.lsSal = "0";

            modsys.lsBod = "0";

            //string lsRet = modsys.mfGuardar(Session["lsIdentificador"].ToString(), Convert.ToBoolean(Session["lbNvo"]));
            string lsRet = modsys.mfGuardar(this.hdIdentificador.Value, Convert.ToBoolean(Session["lbNvo"]));

            if (lsRet != "")
                mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
            else
            {
                // if ((Session["lsIdentificador"]).ToString() == "0")

                if (this.hdIdentificador.Value == "0")
                {

                    //Session.Add("lsIdentificador", modsys.ObtenerIDUsuario(modsys.lsRut, modsys.lsHosp));
                    this.hdIdentificador.Value = modsys.ObtenerIDUsuario(modsys.lsRut, modsys.lsHosp);
                    Session.Add("lbNvo", false);
                    LbTitulo.Text = modsys.lsRut;

                }

                mens.mensaje(Page, "Registro ingresado con Exito.. ");
            }
        }
        else
        {
            mens.mensaje(Page, "Largo Observación no puede exceder los 900 caracteres. ");
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

                mfCambiarEstadoUsuario();
            }

        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    protected void btn_ReIniciar_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            if (confirmValue == "Yes")
            {

                mfReIniciarUsuario();
            }

        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }


    private void mfReIniciarUsuario()
    {

        //string lsRet = usr.mfReIniciarClave(Session["lsIdentificador"].ToString());
        string lsRet = usr.mfReIniciarClave(this.hdIdentificador.Value);

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {
            mens.mensaje(Page, "Registro ingresado con Exito.. ");
        }


    }

    private void mfCambiarEstadoUsuario()
    {
        string asEstado = "2";

        if (this.lbEstado.Text == "VIGENTE")
            asEstado = "3";

        //string lsRet = usr.mfUpdateEstado(Session["lsIdentificador"].ToString(), asEstado);
        string lsRet = usr.mfUpdateEstado(this.hdIdentificador.Value, asEstado);

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


    protected void ImgBtnBack_Click(object sender, ImageClickEventArgs e)
    {

        if (Session["cadena"] == null)
        {
            // Response.Redirect("~/contenido/frmerrgen.aspx");
            Response.Redirect("~/contenido/Adquisiciones/ListaFUS.aspx?id=0");
        }
        else
        {
            Response.Redirect("~/contenido/SysAdmin/ListaUsuarios.aspx?id=0" + Session["cadena"].ToString());
        }

    }


    #endregion

    #region UnidadesOperativas

    protected void btn_Limpiar_Click(object sender, EventArgs e)
    {
        string lsRet = "";

        //lsRet = cu.mfDeleteUnidadUsr(Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());
        lsRet = cu.mfDeleteUnidadUsr(this.hdIdentificador.Value, Session["codHosp"].ToString());

        if (lsRet != "")
        {
            mens.mensaje(Page, "Error: Problemas durante la asignación....");
            return;
        }

        mfCargarGrillaUserUnidad();
        mfCargarGrillaUnidad();
    }

    protected void btn_AsigAll_Click(object sender, EventArgs e)
    {
        DataSet aoDs;
        string lsRet = "";


        //lsRet = cu.mfDeleteUnidadUsr(Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());
        lsRet = cu.mfDeleteUnidadUsr(this.hdIdentificador.Value, Session["codHosp"].ToString());

        if (lsRet != "")
        {
            mens.mensaje(Page, "Error: Problemas durante la asignación....");
            return;
        }


        aoDs = cu.mfListaUnidad(Session["codHosp"].ToString());
        // Ciclo para cargar los datos.
        foreach (DataRow dr in aoDs.Tables[0].Rows)
        {

            //cu.mfInsertUnidadUsr(dr["CODUNIOP"].ToString(), Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());
            cu.mfInsertUnidadUsr(dr["CODUNIOP"].ToString(), this.hdIdentificador.Value, Session["codHosp"].ToString());

        }

        mfCargarGrillaUserUnidad();
        mfCargarGrillaUnidad();
    }

    protected void AddUnidad(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.TxtIdMat.Text = cid;
        mfAgregarUnidad();
    }

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        try
        {
            mfCargarGrillaUnidad();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }


    }

    private void mfCargarGrillaUnidad()
    {
        DataSet aoDs;

        //aoDs = cu.mfCargaListaUnidadUsr(this.bTxtUnidad.Text, Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());
        aoDs = cu.mfCargaListaUnidadUsr(this.bTxtUnidad.Text, this.hdIdentificador.Value, Session["codHosp"].ToString());

        gdArt.DataSource = aoDs;
        gdArt.DataBind();
    }

    private void mfCargarGrillaUserUnidad()
    {
        DataSet aoDs;

        //aoDs = cu.mfCargaUserUnidad(Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());
        aoDs = cu.mfCargaUserUnidad(this.hdIdentificador.Value, Session["codHosp"].ToString());

        gbArtSer.DataSource = aoDs;
        gbArtSer.DataBind();
    }


    private void mfAgregarUnidad()
    {
        string lsRet = "";


        //lsRet = cu.mfInsertUnidadUsr(this.TxtIdMat.Text, Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());
        lsRet = cu.mfInsertUnidadUsr(this.TxtIdMat.Text, this.hdIdentificador.Value, Session["codHosp"].ToString());

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo insertar Profesional");
        }
        else
        {
            mfCargarGrillaUserUnidad();
            mfCargarGrillaUnidad();
        }
    }

    protected void ValidaUnidad(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;

        string val = row.Cells[2].Text;

        string lsRet = cu.mfValidaUnidadUsr(cid, val);

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
        }
        else
        {
            mfCargarGrillaUserUnidad();
            //mfCargarGrillaUnidad();
        }
    }


    protected void ElimUnidad(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.TxtIdMatElim.Text = cid;
        mfElim();
    }

    private void mfElim()
    {
        string lsExiste = "0";
        string lsRet = "";

        //lsExiste = cursos.mfElimAlumn(this.TxtIdMatElim.Text);

        if (lsExiste == "0")
        {
            lsRet = cu.mfElimUnidadUsr(this.TxtIdMatElim.Text);

            if (lsRet != "")
            {

                mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
            }
            else
            {
                mfCargarGrillaUserUnidad();
                mfCargarGrillaUnidad();
            }
        }
        else
        {
            mens.mensaje(Page, "Profesional se Encuentra asignado ");
        }


    }

    protected void gdArt_SelectedIndexChanged(object sender, EventArgs e)
    {


        //this.TxtIdMat.Text = gdArt.SelectedRow.Cells[0].Text;
        this.TxtIdMat.Text = gdArt.DataKeys[gdArt.SelectedIndex].Values[0].ToString();
        mfAgregarUnidad();


    }
    protected void gdArt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('gdArt','Select${0}')", e.Row.RowIndex));
        }
    }


    protected void gbArtSer_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.TxtIdMatElim.Text = gbArtSer.DataKeys[gbArtSer.SelectedIndex].Values[0].ToString();
        mfElim();


    }
    protected void gbArtSer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('gbArtSer','Select${0}')", e.Row.RowIndex));
        }
    }

    protected void gdArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdArt.PageIndex = e.NewPageIndex;
        mfCargarGrillaUnidad();
    }
    protected void gbArtSer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbArtSer.PageIndex = e.NewPageIndex;
        mfCargarGrillaUserUnidad();
    }

    #endregion



    protected void btn_Acceso_Click(object sender, EventArgs e)
    {
        try
        {
            string confirmValue = Request.Form["confirm_value"];
            modFunciones fun = new modFunciones();
            confirmValue = fun.ConfirmValor(confirmValue);
            if (confirmValue == "Yes")
            {

                mfLimpiarAcceso();
            }

        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }


    private void mfLimpiarAcceso()
    {

        string lsRet = usr.mfLimpiarAcceso(this.TxtRut.Text);

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {
            mens.mensaje(Page, "Registro ingresado con Exito.. ");
        }


    }
}