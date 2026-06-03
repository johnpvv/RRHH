using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_UnidadOperativa : System.Web.UI.Page
{
    private string id;
    private String stcadena = String.Empty;
    Mensaje mens = new Mensaje();
    static bool edicion;

    ClassUnidOperativa UniOp = new ClassUnidOperativa();


    protected void Page_Load(object sender, EventArgs e)
    {
        String lsPer = "";
        string gUsr;
        string asCodSistema;
        modFunciones modfunc = new modFunciones();

        if (!IsPostBack)
        {
            //gUsr = Session["user"].ToString();
            //asCodSistema = Session["codHosp"].ToString();
            //Session.Add("lsGrabar", "SI");
            //mfBuscar();

            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();
                Session.Add("lsGrabar", "SI");

                lsPer = modfunc.fnValidaUsrApp("MANT_BODEGA", gUsr, asCodSistema);


                if (lsPer != "M" && lsPer != "L")
                {
                    //DMS 20210503
                   // Response.Redirect("~/contenido/frmerrgen.aspx");
                }

                //id = Request.QueryString["id"].ToString();

                mfBuscar();
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
    }

    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {   
            e.Row.CssClass = e.Row.RowState.ToString();         
        }
    }

    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
    }


    protected void ElimClasif(object sender, EventArgs e)
    {

        if (edicion == true) { return; }


        try
        {
            if (Session["lsGrabar"].ToString() == "NO")
            {
                mens.mensaje(Page, "NO esta autorizado para eliminar..");
            }
            else
            {
                ImageButton boton = (ImageButton)sender;
                GridViewRow row = (GridViewRow)boton.NamingContainer;
                string cid = row.Cells[0].Text;
                if (cid != "" && cid != null)
                {
                    UniOp.IdUnidadOperativa = Convert.ToInt32(cid);
                }

                //UniOp.UnidadSuperior = TID.Text.ToUpper();
                //UniOp.NombreUnidad = TServicio.Text.ToUpper();
                UniOp.IdEstado = 3;

                int retorno = 0;

                retorno = UniOp.Eli_Rest_UnidadOperativa(UniOp);

                if (retorno == 0)
                    mens.mensaje(Page, "Error: Problemas al Rehabilitar Clasificación..");
                else
                {

                    mfBuscar();
                    mens.mensaje(Page, "Eliminado Exitosamente.");
                }
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }


    }

    protected void RehabClasif(object sender, EventArgs e)
    {
        if (edicion == true) { return; }

        try
        {
            if (Session["lsGrabar"].ToString() == "NO")
            {
                mens.mensaje(Page, "NO esta autorizado para Rehabilitar..");
            }
            else
            {
                ImageButton boton = (ImageButton)sender;
                GridViewRow row = (GridViewRow)boton.NamingContainer;
                string cid = row.Cells[0].Text;
                if (cid != "" && cid != null)
                {
                    UniOp.IdUnidadOperativa = Convert.ToInt32(cid);
                }

                //UniOp.UnidadSuperior = TID.Text.ToUpper();
                //UniOp.NombreUnidad = TServicio.Text.ToUpper();
                UniOp.IdEstado = 1;

                int retorno = 0;

                retorno = UniOp.Eli_Rest_UnidadOperativa(UniOp);

                if (retorno == 0)
                    mens.mensaje(Page, "Error: Problemas al Rehabilitar Clasificación..");
                else
                {

                    mfBuscar();
                    mens.mensaje(Page, "Rehabilitado Exitosamente.");
                }
            }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }


    }

    protected void ImBtIngresar_Click(object sender, ImageClickEventArgs e)
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

    private void mfAgregar()
    {
        if (this.TServicio.Text == "") { mens.mensaje(Page, "Debe de Ingresar Descripcion de Unidad Operativa.. "); return; }
        if (this.TID.Text == "") { mens.mensaje(Page, "Debe de Ingresar Unidad Superior.. "); return; }

        if (Convert.ToInt32(UniOp.mfExisteId(this.TID.Text)) == 0)
        {
            mens.mensaje(Page, "No existe Unidad Superior, favor verifique.. "); return;
        }

        if (Convert.ToInt32(UniOp.mfExisteUOP(this.TServicio.Text)) > 0) { mens.mensaje(Page, "Debe de Ingresar Nombre Unidad Operativa Única.. "); return; }

        UniOp.UnidadSuperior = TID.Text.ToUpper();
        UniOp.NombreUnidad = TServicio.Text.ToUpper();
        UniOp.IdEstado = 1;

        int retorno = 0;

        retorno = UniOp.InsertUnidadOperativa(UniOp);


        if (retorno == 0)
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {

            this.TServicio.Text = "";
            this.TID.Text = "";

            mfBuscar();
        }

    }



    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        try
        {
            mfBuscar();
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    private void mfBuscar()
    {
        DataTable uni = new DataTable();

        if (!ckElim.Checked) UniOp.IdEstado = 1; else UniOp.IdEstado = 3;

        UniOp.UnidadSuperior = TID.Text;
        UniOp.NombreUnidad = TServicio.Text;

        uni = UniOp.busca_uni_op(UniOp);

        if (uni != null && uni.Rows.Count > 0)
        {
            if (uni.Rows.Count > 0)
            {
                this.dgData.DataSource = uni;
                this.dgData.DataBind();
                this.dgData.Caption = "Listado Unidad Operativa";
            }
            else
            {
                this.dgData.DataSource = null;
                this.dgData.DataBind();
            }
        }
        else
        {
            this.dgData.DataSource = null;
            this.dgData.DataBind();
        }
    }

    protected void dgData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        dgData.EditIndex = e.NewEditIndex;
        mfBuscar();
        edicion = true;

        //DESHABILITAR BOTON-------------------

        //btn_RehaCla.enabled = false;

        //-------------------------------------------------------
        //Button lbtn1 = (Button)e.Row.Cells[3].Controls[0] as Button;
        //Button btnFinalizar = (Button)e.Row.Cells[6].Controls[0] as Button;
        //TextBox txt1 = (TextBox)e.Row.FindControl("txt1");
        //TextBox txt2 = (TextBox)e.Row.FindControl("txt2");

        //txt1.Enabled = true;
        //txt2.Enabled = false;
        //btnFinalizar.Enabled = true;

        //-------------------------------------------------------

        //int filaElimar = dgData.CurrentRow.Index;
        //dgData.CurrentCell = null;
        //dgData.Rows[filaElimar].ReadOnly = false;
        //dgData.Rows[filaElimar].DefaultCellStyle.ForeColor = Color.Gray;



    }

    protected void dgData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        dgData.EditIndex = -1;
        mfBuscar();
        edicion = false;
    }

    protected void dgData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        //Validar
        int CODUNIOP        = (int)e.Keys["CODUNIOP"];
        string IDSUP_UNIDAD = (string)e.NewValues["IDSUP_UNIDAD"];
        string DESCRIPCION  = (string)e.NewValues["DESCRIPCION"];


        if (IDSUP_UNIDAD == "") { mens.mensaje(Page, "Debe ingresar Unidad Superior.. "); return; }
        if (DESCRIPCION == "")  { mens.mensaje(Page, "Debe ingresar Decripcion de Unidad Operativa.. "); return; }

        if (Convert.ToInt32(UniOp.mfExisteId(IDSUP_UNIDAD)) == 0)
        {
            mens.mensaje(Page, "No existe Unidad Superior, favor verifique.. "); return;
        }


        //ACTUALIZAR
        UniOp.UnidadSuperior = IDSUP_UNIDAD;
        UniOp.NombreUnidad   = DESCRIPCION.ToUpper();
        UniOp.IdEstado       = 1;

        string sal =  UniOp.ModificarUnidad(    CODUNIOP.ToString(), 
                                                IDSUP_UNIDAD, 
                                                DESCRIPCION,
                                                "0");

        if (sal != "")
        {
            //infoColor(LblProceso, System.Drawing.Color.OrangeRed, "ERROR AL ACTUALIZAR ARTICULO: " + salida);
            mens.mensaje(Page, "ERROR AL ACTUALIZAR REGISTRO !!. Error: " + sal); return;
            dgData.EditIndex = -1;
            return;
        }

        dgData.EditIndex = -1;

        mfBuscar();
        //infoColor(LblProceso, System.Drawing.Color.Green, "REGISTRO ACTUALIZADO EXITOSAMENTE !! ");
        mens.mensaje(Page, "REGISTRO ACTUALIZADO EXITOSAMENTE !! "); return;

        GridViewRow gvrEdit = dgData.Rows[e.RowIndex];
        gvrEdit.BackColor = System.Drawing.Color.LightPink;
        edicion = false;
    }





}