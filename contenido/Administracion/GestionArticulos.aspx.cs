using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_GestionArticulos : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    string tempo;
    static string Identificador = "0";
    ClassArticulos art = new ClassArticulos();
    ClassUnidOperativa cu = new ClassUnidOperativa();
    ClassHomologa hom = new ClassHomologa();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataSet aoDs;
            //DataSet aoPre;
            //DataSet aoCod;
            //DataSet aoProv;
            //DataSet aoBod;
            //DataSet aoBind; TabPanelHomologa

            string gUsr;
            string asCodSistema;
            String lsGrabar = "";
            modFunciones modfunc = new modFunciones();
            gUsr = Session["user"].ToString();
            asCodSistema = "1";


            lsGrabar = modfunc.fnValidaUsrApp("PANEL_HOMOLOGA", gUsr, asCodSistema);
            if (lsGrabar == "M" || lsGrabar == "L") { this.TabPanelHomologa.Visible = true; }

            mfLlenarDDLUnMedida();
            mfLlenarDDLUnMinima();

            mfNvo();
            Session.Add("lsIdentificador", Request.QueryString["key"].ToString());

            Session.Add("lbNvo", false);
            aoDs = art.ConsultarID(Session["lsIdentificador"].ToString());
            this.BtnAgregar.Text = "Modificar";
           
            this.lbl_Codigo.Visible = false;
            //this.TabPanel1.Visible = false;

            if (aoDs != null && aoDs.Tables.Count > 0)
            {
                if (aoDs.Tables[0].Rows.Count > 0)
                {
                    this.TxtNombre.Text = aoDs.Tables[0].Rows[0]["descripcion"].ToString();
                    this.TxtCodigo.Text = aoDs.Tables[0].Rows[0]["codigo"].ToString();
                    this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["codigo"].ToString();

                    this.ddlUnidad.SelectedValue = aoDs.Tables[0].Rows[0]["IDPRESENTACION"].ToString();
                    this.rblClinico.SelectedValue = aoDs.Tables[0].Rows[0]["CALCULAR"].ToString();
                    this.rblMorbido.SelectedValue = aoDs.Tables[0].Rows[0]["TIPO_RIESGO"].ToString();

                    this.ddlMinima.SelectedValue = aoDs.Tables[0].Rows[0]["IDUNMIN"].ToString();

                    this.TFactor.Text = aoDs.Tables[0].Rows[0]["FACTOR"].ToString().Replace(",",".");
                    // this.TxtExistencia.Text = aoDs.Tables[0].Rows[0]["existencia"].ToString();
                    // this.TxtPromMensual.Text = aoDs.Tables[0].Rows[0]["cpm"].ToString();

                    // if (aoDs.Tables[0].Rows[0]["no_cobrable"].ToString() == "1")
                    //     this.chkcobrable.Checked = false;
                    // else
                    //     this.chkcobrable.Checked = true;

                    //// this.rblClinico.SelectedValue = aoDs.Tables[0].Rows[0]["clinico"].ToString();



                    // this.TxtOC.Text = aoDs.Tables[0].Rows[0]["ultima_oc"].ToString();

                    // this.gvDCA.DataSource = aoDs.Tables["Y"];
                    // this.gvDCA.DataBind();



                    // Existencias de Artículos.

                    this.dgExiArt.DataSource = aoDs.Tables["existencias"];
                    this.dgExiArt.DataBind();


                     //aoPre = art.mfGetDataPrecio(Session["lsIdentificador"].ToString());

                    //this.dgData.DataSource = aoPre;
                    //this.dgData.DataBind();

                    mfCargarGrillaArtUnidad();
                    mfCargarGrillaUnidad();

                    mfCargarGrillaViaArt();
                    mfCargarGrillaVia();

                    mfCargarGrillaHomologaArt();
                    mfCargarGrillaHomologa();

                }

                this.BtnAgregar.Text = "Agregar";

            }
        }
    }

    private void mfLlenarDDLUnMedida()
    {
        DataSet dat = new DataSet();
        ClassUnidad_Medida uni = new ClassUnidad_Medida();

        dat = uni.mfUnidadDDl();

        this.ddlUnidad.DataTextField = "DESCRIPCION";
        this.ddlUnidad.DataValueField = "IDPRESENTACION";
        this.ddlUnidad.DataSource = dat;
        this.ddlUnidad.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Unidad Medida";
        item.Value = "0";
        this.ddlUnidad.Items.Insert(0, item);
    }


    private void mfLlenarDDLUnMinima()
    {
        DataSet dat = new DataSet();
        ClassUnidad_Medida uni = new ClassUnidad_Medida();

        dat = uni.mfUnidadMinimaDDl();

        this.ddlMinima.DataTextField = "DESCRIPCION";
        this.ddlMinima.DataValueField = "IDUNMIN";
        this.ddlMinima.DataSource = dat;
        this.ddlMinima.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Unidad Minima";
        item.Value = "0";
        this.ddlMinima.Items.Insert(0, item);
    }

    private void mfNvo()
    {
        LbTitulo.Text = "";
        TxtNombre.Text = "";
        TxtCodigo.Text = "";
        
        //TxtStCritico.Text = "";
        //TxtExistencia.Text = "";
        //TxtReposicion.Text = "";
        //TxtPromMensual.Text = "";
        //TxtProgramado.Text = "";
        //TxtOC.Text = "";
        //TxtHijo.Text = "";
        //TxtUsos.Text = "";
        //TxtEsterilizacion.Text = "";
        //TxtCargoFijo.Text = "";
        //TxtFarmacia.Text = "";

        rblClinico.SelectedValue = "1";
        

        //chkcobrable.Checked = false;
        //chkImport.Checked = false;
        //chkBcoDroga.Checked = false;

        //TxtObservacion.Text = "";
        this.BtnAgregar.Text = "Agregar";

        // Movimientos
        //this.gvDCA.DataSource = null;
        //this.gvDCA.DataBind();

        // Existencias

        this.dgExiArt.DataSource = null;
        this.dgExiArt.DataBind();

       

        // Nuevo

        Session.Add("lbNvo", true);
        Session.Add("lsIdentificador", "0");
    }

    protected void ImgBtnBack_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/contenido/Administracion/ListaArticulos.aspx?id=1");
    }
    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            //e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('dgData','Select${0}')", e.Row.RowIndex));
        }
    }

    #region existencia
    protected void dgExiArt_SelectedIndexChanged(object sender, EventArgs e)
    {


    }


    protected void dgExiArt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            //e.Row.Attributes.Add("onclick", String.Format("javascript:__doPostBack('dgData','Select${0}')", e.Row.RowIndex));
        }
    }

    protected void dgExiArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    #endregion





    protected void BtnAgregar_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        modFunciones fun = new modFunciones();
        confirmValue = fun.ConfirmValor(confirmValue);
        if (confirmValue == "Yes")
        {

            mfAgregar();
        }
    }

    private void mfAgregar()
    {

        if (this.ddlUnidad.SelectedValue == "0" )
        {
            mens.mensaje(Page, "Debe de Ingresar Presentacion");
            return;
        }


        if (this.ddlMinima.SelectedValue == "0")
        {
            mens.mensaje(Page, "Debe de Ingresar Unidad Minima");
            return;
        }

        if (this.TFactor.Text == "" || Convert.ToDecimal(this.TFactor.Text) <= 0)
        {
            mens.mensaje(Page, "Debe de Ingresar Factor Mayor a Cero");
            return;
        }

        art.lsIDPRESENTACION = this.ddlUnidad.SelectedValue;
        art.lsCALCULAR = this.rblClinico.SelectedValue;
        art.lsMorbido = this.rblMorbido.SelectedValue;
        art.lsMinima = this.ddlMinima.SelectedValue;
        art.lsFactor = this.TFactor.Text.Replace(",",".");

        string lsRet = art.mfGuardar(Session["lsIdentificador"].ToString());

        if (lsRet != "")
            mens.mensaje(Page, "Error: " + lsRet);
        else
        {

            mens.mensaje(Page, "Registro Ingresado con Exito.. ");
        }

    }



    protected void BtnAgregarPrecio_Click(object sender, EventArgs e)
    {

    }

    protected void BtAgrBarra_Click(object sender, EventArgs e)
    {

    }

    protected void BtElimBarra_Click(object sender, EventArgs e)
    {

    }

    protected void BtAgregarBodega_Click(object sender, EventArgs e)
    {

    }

    protected void BtnCrearLote_Click(object sender, EventArgs e)
    {

    }

    protected void BuscarGrupo_Click(object sender, EventArgs e)
    {

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

        aoDs = cu.mfCargaListaUnidad(this.bTxtUnidad.Text, Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());

        gdArt.DataSource = aoDs;
        gdArt.DataBind();
    }

    protected void btn_Limpiar_Click(object sender, EventArgs e)
    {
        string lsRet = "";

        lsRet = cu.mfDeleteUnidad(Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());

        if (lsRet != "")
        {
            mens.mensaje(Page, "Error: Problemas durante la asignación....");
            return;
        }

        mfCargarGrillaArtUnidad();
        mfCargarGrillaUnidad();

    }

    private void mfCargarGrillaArtUnidad()
    {
        DataSet aoDs;

        aoDs = cu.mfCargaArtUnidad(Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());

        gbArtSer.DataSource = aoDs;
        gbArtSer.DataBind();
    }



    #region Unidad

    private void mfElim()
    {
        string lsExiste = "0";
        string lsRet = "";

        //lsExiste = cursos.mfElimAlumn(this.TxtIdMatElim.Text);

        if (lsExiste == "0")
        {
            lsRet = cu.mfElimUnidad(this.TxtIdMatElim.Text);

            if (lsRet != "")
            {

                mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
            }
            else
            {
                mfCargarGrillaArtUnidad();
                mfCargarGrillaUnidad();
            }
        }
        else
        {
            mens.mensaje(Page, "Profesional se Encuentra asignado ");
        }


    }


    private void mfAgregarUnidad()
    {
        string lsRet = "";


        lsRet = cu.mfInsertUnidad(this.TxtIdMat.Text, Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo insertar Unidad Operativa");
        }
        else
        {
            mfCargarGrillaArtUnidad();
            mfCargarGrillaUnidad();
        }
    }



    protected void gbArtSer_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.TxtIdMatElim.Text = grViaAsoc.DataKeys[grViaAsoc.SelectedIndex].Values[0].ToString();
        mfElim();


    }

    protected void gbArtSer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbArtSer.PageIndex = e.NewPageIndex;
        mfCargarGrillaArtUnidad();
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

    protected void gdArt_SelectedIndexChanged(object sender, EventArgs e)
    {


        //this.TxtIdMat.Text = gdArt.SelectedRow.Cells[0].Text;
        this.TxtIdMat.Text = gdArt.DataKeys[gdArt.SelectedIndex].Values[0].ToString();
        mfAgregarUnidad();


    }

    protected void AddUnidad(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.TxtIdMat.Text = cid;
        mfAgregarUnidad();
    }

    protected void ValidaUnidad(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;

        string val = row.Cells[2].Text;

        string lsRet = cu.mfValidaUnidad(cid, val);

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
        }
        else
        {
            mfCargarGrillaArtUnidad();
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

    #endregion

    #region VIA

    private void mfElimVia()
    {
        string lsExiste = "0";
        string lsRet = "";

        //lsExiste = cursos.mfElimAlumn(this.TxtIdMatElim.Text);

        if (lsExiste == "0")
        {
            lsRet = cu.mfElimVia(this.txtTempElimVia.Text);

            if (lsRet != "")
            {

                mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
            }
            else
            {
                mfCargarGrillaViaArt();
                mfCargarGrillaVia();
            }
        }
        else
        {
            mens.mensaje(Page, "Profesional se Encuentra asignado ");
        }


    }

    protected void ElimVia(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.txtTempElimVia.Text = cid;
        mfElimVia();
    }

    private void mfCargarGrillaVia()
    {
        DataSet aoDs;

        aoDs = cu.mfCargarGrillaViaAdm(Session["lsIdentificador"].ToString(), this.TVIA.Text);

        grVia.DataSource = aoDs;
        grVia.DataBind();
    }

    private void mfCargarGrillaViaArt()
    {
        DataSet aoDs;

        aoDs = cu.mfCargaViaAdmArt(Session["lsIdentificador"].ToString());

        grViaAsoc.DataSource = aoDs;
        grViaAsoc.DataBind();
    }

    protected void grVia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grVia.PageIndex = e.NewPageIndex;
        mfCargarGrillaVia();
    }

    protected void grVia_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grVia_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grViaAsoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grViaAsoc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grViaAsoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void AddVia(object sender, ImageClickEventArgs e)
    {
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.txtTempVia.Text = cid;
        mfAgregarVia();
    }

    private void mfAgregarVia()
    {
        string lsRet = "";


        lsRet = cu.mfInsertVia(this.txtTempVia.Text, Session["lsIdentificador"].ToString());

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo insertar Via Administracion");
        }
        else
        {
            mfCargarGrillaViaArt();
            mfCargarGrillaVia();
        }
    }

    protected void btn_ElimVIa_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void BtnVia_Click(object sender, EventArgs e)
    {
        try
        {
            mfCargarGrillaVia();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    #endregion

    #region HOMOLOGA

    private void mfElimHomologa()
    {
        string lsExiste = "0";
        string lsRet = "";

        //lsExiste = cursos.mfElimAlumn(this.TxtIdMatElim.Text);

        if (lsExiste == "0")
        {
            lsRet = hom.mfElimHomol(this.txtTempElimHomol.Text);

            if (lsRet != "")
            {

                mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
            }
            else
            {
                mfCargarGrillaHomologaArt();
                mfCargarGrillaHomologa();
            }
        }
        else
        {
            mens.mensaje(Page, "Profesional se Encuentra asignado ");
        }


    }

    protected void ElimHomologa(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.txtTempElimHomol.Text = cid;
        mfElimHomologa();
    }

    private void mfCargarGrillaHomologa()
    {
        DataSet aoDs;

        aoDs = hom.mfCargarGrillaHomolAdm(Session["lsIdentificador"].ToString(), this.THOMOLOGA.Text);

        grHomol.DataSource = aoDs;
        grHomol.DataBind();
    }


    private void mfCargarGrillaHomologaArt()
    {
        DataSet aoDs;

        aoDs = hom.mfCargaHomologaAdmArt(Session["lsIdentificador"].ToString());

        grHomolArt.DataSource = aoDs;
        grHomolArt.DataBind();
    }

    protected void grHomol_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grVia.PageIndex = e.NewPageIndex;
        mfCargarGrillaHomologa();
    }

    protected void grHomol_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grHomol_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grHomolArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void grHomolArt_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grHomolArt_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void AddHomologa(object sender, ImageClickEventArgs e)
    {
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;
        this.txtTempHomol.Text = cid;
        mfAgregarHomologa();
    }

    private void mfAgregarHomologa()
    {
        string lsRet = "";


        lsRet = hom.mfInsertHomol(this.txtTempHomol.Text, Session["lsIdentificador"].ToString(), Session["user"].ToString());

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo insertar Via Administracion");
        }
        else
        {
            mfCargarGrillaHomologaArt();
            mfCargarGrillaHomologa();
        }
    }


    protected void btn_ElimHomologa_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void BtnHomologa_Click(object sender, EventArgs e)
    {
        try
        {
            mfCargarGrillaHomologa();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    #endregion
}