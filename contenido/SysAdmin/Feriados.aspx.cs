using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class contenido_SysAdmin_Feriados : System.Web.UI.Page
{
    private string id;
    private String stcadena = String.Empty;
    Mensaje mens = new Mensaje();
    ClassFeriado cla = new ClassFeriado();
    ClassTurnos tur = new ClassTurnos();
    protected void Page_Load(object sender, EventArgs e)
    {
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
                CargarAnios();
                mfBuscar();
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
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
    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        mfBuscar();
    }
    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        mfBuscar();
    }
    protected void ImBtIngresar_Click(object sender, EventArgs e)
    {
        try
        {
            mfAgregar();
            mens.mensaje(Page, "Agregado Exitosamente.");
        }
        catch (Exception ex)
        {
            Response.Redirect("~/contenido/frmerrgen.aspx");
        }
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
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
            string lsRet = cla.mfDelete(cid);
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
    protected void mfBuscar()
    {
        DataSet ds;
        cla.ls_anio = this.ddlAnio.SelectedValue;
        ds = cla.mfBuscar();
        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.dgData.DataSource = ds;
                this.dgData.DataBind();
                lblTotal.Text = ds.Tables[0].Rows.Count.ToString() + " resultado(s)";
            }
            else
            {
                this.dgData.DataSource = null;
                this.dgData.DataBind();
                lblTotal.Text = "0 resultado(s)";
            }
        }
        else
        {
            this.dgData.DataSource = null;
            this.dgData.DataBind();
            lblTotal.Text = "0 resultado(s)";
        }
    }
    protected void mfAgregar()
    {
        DateTime fecha;

        if (!DateTime.TryParseExact(TFecha.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out fecha))
        {
            mens.mensaje(Page, "La fecha ingresada no es válida.");
            return;
        }
        if (this.TFecha.Text == "")
        {
            mens.mensaje(Page, "Debe de Ingresar Fecha.. ");
            return;
        }
        if (Convert.ToInt32(cla.mfExisteFecha(this.TFecha.Text)) > 0)
        {
            mens.mensaje(Page, "Fecha ya existe.. ");
            return;
        }
        string lsRet = cla.mfGuardar(this.TFecha.Text);
        if (lsRet != "")
            mens.mensaje(Page, "Error: Problemas al Ingresar el Registro.");
        else
        {
            this.TFecha.Text = "";
            mfBuscar();
        }
    }
}