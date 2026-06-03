using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class contenido_SysAdmin_ListaAccesos : System.Web.UI.Page
{
    ClassAccesos pers = new ClassAccesos();
    Mensaje mens = new Mensaje();

    protected void Page_Load(object sender, EventArgs e)
    {
        String lsPer = "";
        modFunciones modfunc = new modFunciones();
        string gUsr;
        string asCodSistema;

        if (!IsPostBack)
        {
            try
            {
                gUsr = Session["user"].ToString();
                asCodSistema = Session["codHosp"].ToString();

                lsPer = modfunc.fnValidaUsrApp("MANT_ACCESO", gUsr, asCodSistema);


                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }

        }


    }


    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {

        Response.Redirect("~/contenido/SysAdmin/GestionAccesos.aspx?key=" + dgData.DataKeys[dgData.SelectedIndex].Values[0].ToString());
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
        string lsIdEstado = "1";

        //pers.lsAcceso = this.TxtAcceso.Text;


        if (bchkEli.Checked == true)
            lsIdEstado = "3";
        else
            lsIdEstado = "1";

        ds = pers.mfBuscar(this.TxtAcceso.Text, Session["codHosp"].ToString(), lsIdEstado, Session["user"].ToString(), System.Environment.MachineName);


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
                this.dgData.Caption = "Listado Protocolos";
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
        Response.Redirect("~/contenido/SysAdmin/GestionAccesos.aspx?key=0");
    }


    protected void dgData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
    }

}