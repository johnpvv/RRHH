using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_SysAdmin_GestionRoles : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();

    ClassRoles acs = new ClassRoles();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet aoDs;


        if (!IsPostBack)
        {

            try
            {
                // Captura Datos IdComite
                Session.Add("lsIdentificador", Request.QueryString["key"].ToString());

                mfNvo();

                if (Session["lsIdentificador"].ToString() == "0")
                {

                    Session.Add("lbNvo", true);


                }
                else
                {

                    Session.Add("lbNvo", false);
                    aoDs = acs.ConsultarID(Session["lsIdentificador"].ToString(), Session["codHosp"].ToString(), Session["user"].ToString(), System.Environment.MachineName);


                    if (aoDs != null && aoDs.Tables.Count > 0)
                    {
                        if (aoDs.Tables[0].Rows.Count > 0)
                        {


                            this.TNombre.Text = aoDs.Tables[0].Rows[0]["descripcion"].ToString();
                            this.TCodigo.Text = aoDs.Tables[0].Rows[0]["codigo"].ToString();
                            this.TObser.Text = aoDs.Tables[0].Rows[0]["obs"].ToString();
                            this.LbTitulo.Text = aoDs.Tables[0].Rows[0]["descripcion"].ToString();

                            mfCargaGrp();
                            mfCargaGrpDisp();

                            mfCargaUser();
                            mfCargaUserDisp();



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

    private void mfNvo()
    {
        // Inicializar Formularios

        //ddlProfesor.SelectedValue = "1";
        TNombre.Text = "";
        TCodigo.Text = "";
        TObser.Text = "";



        // Profesionales
        this.gdArt.DataSource = null;
        this.gdArt.DataBind();

        // Temas
        this.gbArtSer.DataSource = null;
        this.gbArtSer.DataBind();



    }


    protected void BtnAgregar_Click(object sender, EventArgs e)
    {

        try
        {
            mfAgregar();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        try
        {
            mfNvo();
            // Nuevo
            Session.Add("lsIdentificador", 0);
            Session.Add("lbNvo", true);
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    protected void ImgBtnBack_Click(object sender, ImageClickEventArgs e)
    {

        Response.Redirect("~/contenido/SysAdmin/ListaRoles.aspx?id=0");

    }

    private void mfAgregar()
    {
        string Obsr = this.TObser.Text;

        int largo = Obsr.Length;

        if (largo <= 900)
        {

            string lsRet = acs.mfGuardar(Session["lsIdentificador"].ToString(), this.TCodigo.Text, this.TNombre.Text, this.TObser.Text, Session["codHosp"].ToString(), Session["user"].ToString(), System.Environment.MachineName, Convert.ToBoolean(Session["lbNvo"]));

            if (lsRet != "")
                mens.mensaje(Page, "Error: " + lsRet);
            else
            {
                if ((Session["lsIdentificador"]).ToString() == "0")
                {
                    string idval = acs.ConsultarMaxID();
                    Session.Add("lsIdentificador", idval);
                }

                Session.Add("lbNvo", false);
                mfCargaGrp();
                mfCargaGrpDisp();

                mfCargaUser();
                mfCargaUserDisp();

                mens.mensaje(Page, "Datos Ingresados Exitosamente.. ");
            }
        }
        else
        {
            mens.mensaje(Page, "Largo Descripción no puede exceder los 400 caracteres. ");
        }

    }


    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["lsIdentificador"].ToString() == "0")
            {
                mens.mensaje(Page, "Debe de seleccionar un Registro.. ");
            }
            else
            {
                string lsRet = acs.mfEliminar(Session["lsIdentificador"].ToString());

                if (lsRet != "")
                    mens.mensaje(Page, "Error: " + lsRet);
                else
                {
                    mens.mensaje(Page, "Registro Eliminado Exitosamente.. ");
                }
            }


        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }


    protected void btnRehabilitar_Click(object sender, EventArgs e)
    {

        try
        {

            if (Session["lsIdentificador"].ToString() == "0")
            {
                mens.mensaje(Page, "Debe de seleccionar un Registro.. ");
            }
            else
            {

                string lsRet = acs.mfRehabilitar(Session["lsIdentificador"].ToString());

                if (lsRet != "")
                    mens.mensaje(Page, "Error: " + lsRet);
                else
                {
                    mens.mensaje(Page, "Registro Rehabilitado Exitosamente.. ");
                }
            }

        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }


    #region Roles

    protected void AddRol(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;

        mfAgregarAplicacion(cid);
    }

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        try
        {
            mfCargaGrpDisp();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }


    }

    private void mfCargaGrp()
    {
        DataSet aoDs;

        aoDs = acs.jaxListaAcceso(Session["codHosp"].ToString(), Session["lsIdentificador"].ToString());

        gbArtSer.DataSource = aoDs;
        gbArtSer.DataBind();
    }

    private void mfCargaGrpDisp()
    {
        DataSet aoDs;

        aoDs = acs.jaxListaAccesoDisp(Session["codHosp"].ToString(), Session["lsIdentificador"].ToString());

        gdArt.DataSource = aoDs;
        gdArt.DataBind();
    }


    private void mfAgregarAplicacion(string asIdentificador)
    {
        string lsRet = "";


        lsRet = acs.jaxAgregarAplicacion(Session["lsIdentificador"].ToString(), asIdentificador, this.rbTipo.SelectedValue, Session["codHosp"].ToString(), Session["user"].ToString(), System.Environment.MachineName);

        if (lsRet != "")
        {

            mens.mensaje(Page, "Error: NO se pudo insertar Profesional");
        }
        else
        {
            mfCargaGrp();
            mfCargaGrpDisp();
        }
    }

    protected void ElimRol(object sender, EventArgs e)
    {

        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[0].Text;

        mfElim(cid);
    }

    private void mfElim(string asIdentificador)
    {
        string lsExiste = "0";
        string lsRet = "";

        //lsExiste = cursos.mfElimAlumn(this.TxtIdMatElim.Text);

        if (lsExiste == "0")
        {
            lsRet = acs.jaxEliminarAplicacion(asIdentificador, Session["codHosp"].ToString(), Session["user"].ToString(), System.Environment.MachineName);

            if (lsRet != "")
            {

                mens.mensaje(Page, "Error: NO se pudo Eliminar Profesional");
            }
            else
            {
                mfCargaGrp();
                mfCargaGrpDisp();
            }
        }
        else
        {
            mens.mensaje(Page, "Profesional se Encuentra asignado ");
        }


    }

    protected void gdArt_SelectedIndexChanged(object sender, EventArgs e)
    {

        mfAgregarAplicacion(gdArt.DataKeys[gdArt.SelectedIndex].Values[0].ToString());


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


        mfElim(gbArtSer.DataKeys[gbArtSer.SelectedIndex].Values[0].ToString());


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
        mfCargaGrp();

    }
    protected void gbArtSer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbArtSer.PageIndex = e.NewPageIndex;
        mfCargaGrpDisp();
    }

    #endregion

    #region User

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
        DataSet aoDs;

        aoDs = acs.jaxListaUsuarios(Session["codHosp"].ToString(), Session["lsIdentificador"].ToString());

        gbUser.DataSource = aoDs;
        gbUser.DataBind();
    }

    private void mfCargaUserDisp()
    {
        DataSet aoDs;

        aoDs = acs.jaxListaUsuariosDisp(Session["codHosp"].ToString(), this.TNombreUsr.Text, this.TRut.Text, Session["lsIdentificador"].ToString());

        gbUserDisp.DataSource = aoDs;
        gbUserDisp.DataBind();
    }


    private void mfAgregarUser(string asIdentificador)
    {
        string lsRet = "";


        lsRet = acs.jaxAgregarUsuario(Session["lsIdentificador"].ToString(), asIdentificador, this.rbTipo.SelectedValue, Session["codHosp"].ToString(), Session["user"].ToString(), System.Environment.MachineName);

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
        string lsExiste = "0";
        string lsRet = "";

        //lsExiste = cursos.mfElimAlumn(this.TxtIdMatElim.Text);

        if (lsExiste == "0")
        {
            lsRet = acs.jaxEliminarUsuario(asIdentificador, Session["codHosp"].ToString(), Session["user"].ToString(), System.Environment.MachineName);

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
        else
        {
            mens.mensaje(Page, "Profesional se Encuentra asignado ");
        }


    }



    protected void dvUser_SelectedIndexChanged(object sender, EventArgs e)
    {

        mfAgregarUser(gbUser.DataKeys[gbUser.SelectedIndex].Values[0].ToString());


    }
    protected void dvUser_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('dvUser','Select${0}')", e.Row.RowIndex));
        }
    }


    protected void dvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbUser.PageIndex = e.NewPageIndex;
        mfCargaUser();

    }

    protected void gbUserDisp_SelectedIndexChanged(object sender, EventArgs e)
    {

        mfElim(gbUserDisp.DataKeys[gbUserDisp.SelectedIndex].Values[0].ToString());


    }
    protected void gbUserDisp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('gbUserDisp','Select${0}')", e.Row.RowIndex));
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
            mfCargaUserDisp();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }
    #endregion


}