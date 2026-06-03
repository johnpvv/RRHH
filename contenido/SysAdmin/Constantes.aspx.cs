using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class contenido_SysAdmin_Constantes : System.Web.UI.Page
{
    private string id;
    private String stcadena = String.Empty;
    Mensaje mens = new Mensaje();
    ClassFirma cla = new ClassFirma();


    protected void Page_Load(object sender, EventArgs e)
    {
        ModalPopupExtender_Popup.Hide();
        String lsPer = "";
        //String lsGrabar = "";
        modFunciones modfunc = new modFunciones();
        string gUsr;
        string asCodSistema;

        if (!IsPostBack)
        {
            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();
                Session.Add("lsGrabar", "SI");

                lsPer = modfunc.fnValidaUsrApp("MANT_CONST", gUsr, asCodSistema);


                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }

                //lsGrabar = modfunc.fnValidaUsrApp("ADM_INT_BTN_AGREGAR", gUsr, asCodSistema);
                //if (lsGrabar != "M") { this.ImBtIngresar.Enabled = false; Session.Add("lsGrabar", "NO"); }

                id = Request.QueryString["id"].ToString();

                mfBuscar();
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
    }

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        mfBuscar();
    }

    protected void ImBtIngresar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            mfAgregar();
            mens.mensaje(Page, "Agregado Exitosamente.");
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            //e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('dgData','Select${0}')", e.Row.RowIndex));
        }
    }

    protected void btn_EliCla_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (Session["lsGrabar"].ToString() == "NO")
            //{
              //  mens.mensaje(Page, "NO esta autorizado para eliminar..");
            //}
           // else
          //  {
                ImageButton boton = (ImageButton)sender;
                GridViewRow row = (GridViewRow)boton.NamingContainer;
                string cid = row.Cells[0].Text;
                string lsRet = cla.updateEstado(cid, "3");

                if (lsRet != "")
                    mens.mensaje(Page, "Error: Problema al eliminar firma..");
                else
                {

                    mfBuscar();
                mens.mensaje(Page, "Eliminado Exitosamente.");
            }
           // }
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    protected void btn_RehaCla_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
           // if (Session["lsGrabar"].ToString() == "NO")
            //{
             //   mens.mensaje(Page, "NO esta autorizado para Rehabilitar..");
            //}
            //else
            //{
                ImageButton boton = (ImageButton)sender;
                GridViewRow row = (GridViewRow)boton.NamingContainer;
                string cid = row.Cells[0].Text;
                string lsRet = cla.updateEstado(cid, "1");

                if (lsRet != "")
                    mens.mensaje(Page, "Error: Problemas al Rehabilitar Clasificación..");
                else
                {

                    mfBuscar();
                mens.mensaje(Page, "Recuperado Exitosamente");
            }
            //}
        }
        catch
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

#region editar
protected void btn_Edit_Click(object sender, ImageClickEventArgs e)
    {
        DataSet aoDs;
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;

        string cid = row.Cells[0].Text;
        this.hddIdart.Value = cid;

        aoDs = cla.DetalleFirmapopup(cid);


        if (aoDs != null && aoDs.Tables.Count > 0)
        {
            if (aoDs.Tables[0].Rows.Count > 0)
            {
                this.IdPop.Text = aoDs.Tables[0].Rows[0]["IDCONSTANTE"].ToString();
                this.TextCodigoPop.Text = aoDs.Tables[0].Rows[0]["CODIGO"].ToString();
                this.TextEncargadoPop.Text = aoDs.Tables[0].Rows[0]["DESCRIPCION"].ToString();
               
            }
        }


        UpdatePanelCantidad.Visible = true;
        ModalPopupExtender_Popup.Show();
      
       
    }

    protected void BtnEditarPop_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Session["lsGrabar"].ToString() == "NO")
            //{
            //  mens.mensaje(Page, "NO esta autorizado para eliminar..");
            //}
            // else
            //  {
            //ImageButton boton = (ImageButton)sender;
            // GridViewRow row = (GridViewRow)boton.NamingContainer;
            string idpop = IdPop.Text;
            string cid = TextCodigoPop.Text;
            string enc = TextEncargadoPop.Text;

            string lsRet = cla.editFirma(idpop, cid, enc);

            if (lsRet != "")
                mens.mensaje(Page, "Error: Problema al editar firma..");
            else
            {

                mfBuscar();
                mens.mensaje(Page, "Editado Exitosamente.");
            }
            // }
        }
        catch
        {
            ModalPopupExtender_Popup.Hide();
        }
    }

    protected void BtnCerrarPopup_Click(object sender, EventArgs e)
    {
        ModalPopupExtender_Popup.Hide();
    }

    #endregion



    protected void mfBuscar()
    {
        DataSet ds;

        cla.lsBod = this.Tcodigo.Text;

        if (!ckElim.Checked) cla.lsEst = "1"; else cla.lsEst = "3";

        ds = cla.mfBuscar();


        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {

                this.dgData.DataSource = ds;
                this.dgData.DataBind();
                this.dgData.Caption = "Listado  Firmas";

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

    protected void mfAgregar()
    {
        if (this.Tcodigo.Text == "") { mens.mensaje(Page, "Debe de Codigo de Firma.. "); return; }
        if (this.Tencarado.Text == "") { mens.mensaje(Page, "Debe de Ingresar Encargado.. "); return; }

        if (Convert.ToInt32(cla.mfExisteId(this.Tcodigo.Text)) > 0) { mens.mensaje(Page, "Constante ya existe.. "); return; }

        string lsRet = cla.mfGuardar(this.Tcodigo.Text, this.Tencarado.Text);

        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {

            this.Tcodigo.Text = "";
            this.Tencarado.Text = "";

            mfBuscar();
        }
    }

   
}