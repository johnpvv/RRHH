using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_Administracion_ListaArticulos : System.Web.UI.Page
{
    private string id;
    private String stcadena = String.Empty;

    Mensaje mens = new Mensaje();
    ClassArticulos art = new ClassArticulos();
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

                lsPer = modfunc.fnValidaUsrApp("MANT_ARTICULOS", gUsr, asCodSistema);


                if (lsPer != "M" && lsPer != "L")
                {
                    Response.Redirect("~/contenido/frmerrgen.aspx");
                }


                id = Request.QueryString["id"].ToString();

                if (id == "0")
                {
                     //Captura de Textos

                    this.bTxtNom.Text = Request.QueryString["bTxtNom"].ToString();
                    this.btxtCod.Text = Request.QueryString["btxtCod"].ToString();

                    mfBuscar();
                }
            }
            catch
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
    }

    private void mfBuscar()
    {
        DataSet ds;


        art.lsArt = bTxtNom.Text;
        art.lsCod = btxtCod.Text;

        ds = art.mfMantBuscar(art);
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

                this.dgData.PageSize = 50;
                this.dgData.DataSource = dv;
                this.dgData.DataBind();
                this.dgData.Caption = "Listado de Artículos";

 
                this.dgData0.DataSource = dv;
                this.dgData0.DataBind();
                this.dgData0.Caption = "Listado de Artículos";
            }
            else
            {
                mens.mensaje(Page, "No se encontraron coincidencias.. ");
                this.dgData.DataSource = null;
                this.dgData.DataBind();
                this.dgData0.DataSource = null;
                this.dgData0.DataBind();
            }
        }
        else
        {
            mens.mensaje(Page, "No se encontraron coincidencias.. ");
            this.dgData.DataSource = null;
            this.dgData.DataBind();
            this.dgData0.DataSource = null;
            this.dgData0.DataBind();
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

    protected void gbExp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dgData.PageIndex = e.NewPageIndex;
        mfBuscar();
    }

    protected void btn_Buscar_Click(object sender, EventArgs e)
    {
        mfBuscar();
    }

    protected void dgData_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cadena = string.Empty;

        stcadena = "";
        stcadena = stcadena + "&bTxtNom=" + bTxtNom.Text
                            + "&btxtCod=" + btxtCod.Text;

        cadena = modFunciones.Encriptar(stcadena);

        Response.Redirect("~/contenido/Administracion/GestionArticulos.aspx?key=" + dgData.SelectedRow.Cells[0].Text + "&cadena=" + cadena);

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
    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/Administracion/GestionArticulos.aspx?key=0&bTxtNom=&btxtCod=&btxtOnu=&btxtBod=0&btxtRut=0&btxtUm=0&btxtCta=0&bchkBsC=false&bchkBPR=false&bchkSPR=false&brbPrg=false&bchkSEST=false&bchkEli=false");
    }

    #region Excel
    protected void btn_Excel_Click(object sender, EventArgs e)
    {
        //ExportarExcell exp = new ExportarExcell();

        //exp.exportaExcel(gbRep);
        modFunciones mod = new modFunciones();
        //ClassBodega bod = new ClassBodega();

        string asTitulo = "Listado Artículos";
        string asSubtitulo = "Listado Artículos: ";
        string asCriterio = "";
        string asPie = "DTI Hospital Clinico San Borja Ariaran.";
        string lsXp = "";
        string asMembrete = "<table><tr><td width=100px align='center'>Ministerio de Salud</td></tr><tr><td width=100px align='center'>Servicio de Salud Metropolitana Central</td></tr><tr><td width=100px align='center'><u>Hospital Clínico San Borja Arriaran</u></td></tr></table>";


        asSubtitulo = "";


        if (asTitulo != "") asTitulo = "" +
         "<br><center><font size='3' face='arial'><b><u>" + asTitulo + "</u></b></font></center><br>";

        if (asSubtitulo != "") asSubtitulo = "<br>" + asSubtitulo + "<br>";

        if (asCriterio != "") asCriterio = "<font size='1' face='arial'><b>Criterios: </b>" + asCriterio + "</font><br><br>";

        if (asPie != "") asPie = "<br><font size='1' face='arial'>" + asPie + "</font><br>";





        lsXp = asMembrete + asTitulo + asSubtitulo + asCriterio + mod.fnObjRender(dgData0) + asPie;


        fnExpXls(lsXp);
        //ExportaExcell();
    }

    private string fnExpXls(string asDataXls)
    {
        string lsRet = "";
        String lsExt = "xls";
        String lsNom = "ListadoArticulos";

        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + lsNom + "." + lsExt + "");
        Response.ContentEncoding = System.Text.Encoding.Default;
        // El encoding correcto es: Windows-1252
        Response.Charset = "UTF-8";
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
        Response.ContentType = "application/vnd." + lsExt;
        Response.Write(asDataXls);
        Response.End();

        return lsRet;
    }
    #endregion


}