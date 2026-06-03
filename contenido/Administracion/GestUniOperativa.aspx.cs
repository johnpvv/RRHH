using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class contenido_Administracion_GestUniOperativa : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();

    ClassArticulos art = new ClassArticulos();
    ClassUnidOperativa cu = new ClassUnidOperativa();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet aoDs;
          

            mfNvo();
            Session.Add("lsIdentificador", Request.QueryString["key"].ToString());

            Session.Add("lbNvo", false);

            this.BtnAgregar.Text = "Modificar";

            aoDs = cu.ConsultarID(Session["lsIdentificador"].ToString());

            if (aoDs != null && aoDs.Tables.Count > 0)
            {
                if (aoDs.Tables[0].Rows.Count > 0)
                {
                    this.TxtNombre.Text = aoDs.Tables[0].Rows[0]["descripcion"].ToString();
                    this.TxtCodigo.Text = aoDs.Tables[0].Rows[0]["IDSUP_UNIDAD"].ToString();

                    if (aoDs.Tables[0].Rows[0]["EXTIENDE"].ToString() == "0") this.chk360.Checked = false; else this.chk360.Checked = true;

                    mfCargarGrillaUnidad();
                    mfCargarGrillaArtUnidad();


                }

                this.BtnAgregar.Text = "Agregar";

            }
        }
    }


    private void mfNvo()
    {

        TxtNombre.Text = "";
        TxtCodigo.Text = "";

        this.BtnAgregar.Text = "Agregar";

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
        string asExt = "0";
        if (this.TxtNombre.Text == "")
        {
            mens.mensaje(Page, "Error: Debe de Ingresar Descripcioón..");
            return;
        }

        if(chk360.Checked) asExt = "1";

        string lsRet = cu.ModificarUnidad(Session["lsIdentificador"].ToString(), "", this.TxtNombre.Text, asExt);

        if (lsRet != "")
            mens.mensaje(Page, "Error: " + lsRet);
        else
        {

            mens.mensaje(Page, "Registro Ingresado con Exito.. ");
        }

    }



    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        try
        {
            if(this.rbLista.SelectedValue == "1")
                mfCargarGrillaUnidad();
            else
                mfCargarGrillaArtUnidad();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    private void mfCargarGrillaUnidad()
    {
        DataSet aoDs;

        aoDs = cu.mfCargaListaArt(this.TCodigo.Text, this.TDesc.Text, Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());

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

        aoDs = cu.mfCargaListaArtUnidad(this.TCodigo.Text, this.TDesc.Text, Session["lsIdentificador"].ToString(), Session["codHosp"].ToString());

        gbArtSer.DataSource = aoDs;
        gbArtSer.DataBind();
    }



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
    protected void gdArt_SelectedIndexChanged(object sender, EventArgs e)
    {


        //this.TxtIdMat.Text = gdArt.SelectedRow.Cells[0].Text;
        this.TxtIdMat.Text = gdArt.DataKeys[gdArt.SelectedIndex].Values[0].ToString();
        mfAgregarUnidad();


    }

    private void mfAgregarUnidad()
    {
        string lsRet = "";


        lsRet = cu.mfInsertUnidad(Session["lsIdentificador"].ToString(), this.TxtIdMat.Text, Session["codHosp"].ToString());

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



    protected void gdArt_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdArt.PageIndex = e.NewPageIndex;
        mfCargarGrillaUnidad();
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

    protected void gbArtSer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gbArtSer.PageIndex = e.NewPageIndex;
        mfCargarGrillaArtUnidad();
    }


    protected void gbArtSer_SelectedIndexChanged(object sender, EventArgs e)
    {

        this.TxtIdMatElim.Text = gbArtSer.DataKeys[gbArtSer.SelectedIndex].Values[0].ToString();
        mfElim();


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


}