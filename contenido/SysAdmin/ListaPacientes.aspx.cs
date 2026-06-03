using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_SysAdmin_ListaPacientes : System.Web.UI.Page
{
    // Class objPaciente objPaciente = new Class objPaciente();
    ClassModSys objModSys = new ClassModSys();
    ClassPaciente objPaciente = new ClassPaciente();

    Mensaje mens = new Mensaje();

    private string id;
    private String stcadena = String.Empty;



    protected void Page_Load(object sender, EventArgs e)
    {
        String lsPer = "";
        modFunciones modfunc = new modFunciones();
        string gUsr;
        string asCodSistema;


        if (Request.Params["__EVENTTARGET"] == "KeyEnterPostBack")
        {
            mfBuscar();
        }

        if (!IsPostBack)
        {
            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();

                lsPer = modfunc.fnValidaUsrApp("MANT_PACIENTE", gUsr, asCodSistema);

                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }

                id = Request.QueryString["id"].ToString();
                //id = "0";  //DMS 20210504

                if (id == "0")
                {
                    this.TxtNombre.Text = Request.QueryString["TxtNombre"].ToString();
                    this.TxtRut.Text = Request.QueryString["TxtRut"].ToString();
                    this.bchkEli.Checked = Convert.ToBoolean(Request.QueryString["bchkEli"].ToString());

                    mfBuscar();
                }
                else
                {
                    this.bchkEli.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }

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

    // Funciones y Procedimientos

    private void mfBuscar()
    {

        DataSet ds;

        objPaciente.rut = this.TxtRut.Text;        
        objPaciente.nombre = this.TxtNombre.Text.Trim().Replace(" ", "");
        objPaciente.paterno = this.TxtPaterno.Text.Trim().Replace(" ", "");
        objPaciente.materno = this.TxtMaterno.Text.Trim().Replace(" ", "");


        if (this.bchkEli.Checked) objPaciente.lsElim = "3"; else objPaciente.lsElim = "1";

        ds = objPaciente.mfBuscarNombrePaciente();

        //this.dgData.DataSource = ds;
        //this.dgData.DataBind();
        //this.dgData.Caption = "Listado Usuarios";


        if (ds != null && ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                if (ViewState["SortDirection"] != null)
                {
                    _sortDirection = ViewState["SortDirection"].ToString();
                }

                if (ViewState["SortExpression"] != null)
                {
                    _sortExpression = ViewState["SortExpression"].ToString();
                    dv.Sort = string.Concat(_sortExpression, " ", _sortDirection);
                }

                this.dgData.DataSource = dv;
                this.dgData.DataBind();
                this.dgData.Caption = "Listado Usuarios";
            }
            else
            {
                mens.mensaje(Page, "No se encontraron coincidencias.. ");
                this.dgData.DataSource = null;
                this.dgData.DataBind();
            }
        }
        else
        {
            mens.mensaje(Page, "No se encontraron coincidencias.. ");
            this.dgData.DataSource = null;
            this.dgData.DataBind();
        }
    }


    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cadena = string.Empty;

        stcadena = "";
        stcadena = stcadena + "&TxtNombre=" + TxtNombre.Text + "&bchkEli=" + bchkEli.Checked;
        stcadena = stcadena + "&TxtRut=" + TxtRut.Text;

        cadena = modFunciones.Encriptar(stcadena);
        Response.Redirect("~/contenido/SysAdmin/GestionPacientes.aspx?key=" + dgData.DataKeys[dgData.SelectedIndex].Values[0].ToString() + "&cadena=" + cadena);
    }

    protected void dgData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //add css to GridViewrow based on rowState
            e.Row.CssClass = e.Row.RowState.ToString();
            //Add onclick attribute to select row.
            e.Row.Attributes.Add("ondblclick", String.Format("javascript:__doPostBack('dgData','Select${0}')", e.Row.RowIndex));
        }
    }



    private string _sortDirection;
    private string _sortExpression;
    protected void dgData_Sorting(object sender, GridViewSortEventArgs e)
    {

        if (ViewState["SortDirection"] == null || ViewState["SortExpression"].ToString() != e.SortExpression)
        {
            ViewState["SortDirection"] = "ASC";
            dgData.PageIndex = 0;
        }
        else if (ViewState["SortDirection"].ToString() == "ASC")
        {
            ViewState["SortDirection"] = "DESC";
        }
        else if (ViewState["SortDirection"].ToString() == "DESC")
        {
            ViewState["SortDirection"] = "ASC";
        }

        ViewState["SortExpression"] = e.SortExpression;

        mfBuscar();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/SysAdmin/GestionPacientes.aspx?key=0");
    }


    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
    }
}