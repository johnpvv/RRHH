using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class contenido_Administracion_GestDocumentos : System.Web.UI.Page
{
    private string id;
    private String stcadena = String.Empty;
    Mensaje mens = new Mensaje();
    static bool edicion;

    ClassDocumentos doc = new ClassDocumentos();


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

                //lsPer = modfunc.fnValidaUsrApp("MANT_BODEGA", gUsr, asCodSistema);


                //if (lsPer != "M" && lsPer != "L")
                //{
                //    //DMS 20210503
                //    // Response.Redirect("~/contenido/frmerrgen.aspx");
                //}

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
                string cid = row.Cells[1].Text;


                string retorno = doc.mfUpdateEstado(cid , "3");

                if (retorno != "")
                    mens.mensaje(Page, "Error: Problemas al Eliminar Clasificación..");
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
                string cid = row.Cells[1].Text;

                string retorno = doc.mfUpdateEstado(cid, "1");

                if (retorno != "")
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

    protected void BtAgregarPr_Click(object sender, EventArgs e)
    {

        if(this.TDoc.Text == "") { mens.mensaje(Page, "Debe de Ingresar Descripcion"); return; }
        if(this.ddltipodoc.SelectedValue == "0") { mens.mensaje(Page, "Debe de Seleccionar Tipo"); return; }
        //try
        //{


        //string confirmValue = Request.Form["confirm_value"];
        //modFunciones fun = new modFunciones();
        //confirmValue = fun.ConfirmValor(confirmValue);
        //if (confirmValue == "Yes")
        //{

            string strFileName = "";
            string strPathFileName = "";
            string strBase64 = "";
            string lsRet = "";

            strFileName = oFile.PostedFile.FileName;
            strPathFileName = strFileName;

            string[] segments = oFile.FileName.Split('.');
            string fileExt = segments[segments.Length - 1].ToUpper();

            if (fileExt != "PDF" && fileExt != "XLS" && fileExt != "DOC" && fileExt != "XLSX" && fileExt != "DOCX" && fileExt != "PPT" && fileExt != "PPTX" && fileExt != "JPG" && fileExt != "JPEG")
            {
                mens.mensaje(Page, "Se admiten solo archivos pdf, xls, doc, jpg y jpeg");
                return;
            }

            strFileName = Path.GetFileName(strFileName);
            if (oFile.FileName != "")
            {
                String lsArc = "";

                lsArc = Server.MapPath("../") + "Documentos\\" + strFileName;


                if (System.IO.File.Exists(lsArc))
                {
                    System.IO.File.Delete(lsArc);
                }

                this.oFile.PostedFile.SaveAs(lsArc);

                //strBase64 = modFunciones.Encode(strPathFileName);
                strBase64 = modFunciones.Encode(lsArc);

                System.IO.File.Delete(lsArc);
                //mfAdminFactura();
                lsRet = doc.mfIngresarDoc(TDoc.Text, strBase64, this.ddltipodoc.SelectedValue, strFileName, fileExt);

                if (lsRet != "")
                    mens.mensaje(Page, "Error en la carga");
                else
                {
                    mens.mensaje(Page, "Documento cargado con Exito..");
                    this.TDoc.Text = "";
                    this.ddltipodoc.SelectedValue = "0";
                    mfBuscar();
                }

            }
            else
            {
                mens.mensaje(Page, "Seleccione un Archivo para ser cargado");
            }

        //}

        //}
        //catch  
        //{
        //    Response.Redirect("~/contenido/frmerrgen.aspx");
        //}



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
        DataSet uni = new DataSet();

        if (!ckElim.Checked) doc.IdEstado = "1"; else doc.IdEstado = "3";

        doc.Espec = this.TDoc.Text;
        doc.tipo = this.ddltipodoc.SelectedValue;

        uni = doc.mfBuscar();

        if (uni != null && uni.Tables.Count > 0)
        {
            if (uni.Tables[0].Rows.Count > 0)
            {
                this.dgData.DataSource = uni;
                this.dgData.DataBind();
                this.dgData.Caption = "Listado Documentos";
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
        int IDDOC = (int)e.Keys["IDDOC"];

        string DESCRIPCION = (string)e.NewValues["ESPECIFICACION"];

        if (DESCRIPCION == "") { mens.mensaje(Page, "Debe ingresar Decripcion de Unidad Operativa.. "); return; }


        string sal = doc.mfUpdateDesc(IDDOC.ToString(),
                                                DESCRIPCION
                                                );

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

    protected void UploadDoc(object sender, EventArgs e)
    {
        string alTipo = "PDF";
        ImageButton boton = (ImageButton)sender;
        GridViewRow row = (GridViewRow)boton.NamingContainer;
        string cid = row.Cells[1].Text;

        DataSet DBSet = doc.mfCargaDetDoc(cid);

        if (DBSet.Tables.Count > 0)
        {
            if (DBSet.Tables[0].Rows.Count > 0)
            {
                alTipo = DBSet.Tables[0].Rows[0]["TIPO"].ToString();

                Response.ClearContent();
                Response.ClearHeaders();

                if (alTipo == "PDF")
                {
                    Response.ContentType = "application/pdf";
                    //Response.AddHeader("Content-Disposition", "attachment; filename=traslado.pdf");
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + DBSet.Tables[0].Rows[0]["DESCRIPCION"].ToString());
                }
                else if (alTipo == "XLS" || alTipo == "XLSX")
                {
                    Response.ContentType = "application/vnd.ms-excel";

                    Response.AddHeader("Content-Disposition", "attachment;filename=" + DBSet.Tables[0].Rows[0]["DESCRIPCION"].ToString());
                }
                else if (alTipo == "DOC" || alTipo == "DOCX")
                {
                    Response.ContentType = "application/vnd.ms-word";

                    Response.AddHeader("Content-Disposition", "attachment;filename=" + DBSet.Tables[0].Rows[0]["DESCRIPCION"].ToString());
                }
                else if (alTipo == "PPT" || alTipo == "PPTX")
                {
                    Response.ContentType = "application/vnd.ms-powerpoint";

                    Response.AddHeader("Content-Disposition", "attachment;filename=" + DBSet.Tables[0].Rows[0]["DESCRIPCION"].ToString());
                }
                else if (alTipo == "JPG")
                {
                    Response.ContentType = "image/jpeg";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + DBSet.Tables[0].Rows[0]["DESCRIPCION"].ToString());
                }
                else if (alTipo == "JPEG")
                {
                    Response.ContentType = "image/pjpeg";
                    Response.AddHeader("Content-Disposition", "attachment;filename=" + DBSet.Tables[0].Rows[0]["DESCRIPCION"].ToString());
                }
                else
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=traslado.pdf");
                }


                Response.BinaryWrite(Convert.FromBase64String(DBSet.Tables[0].Rows[0]["DOCUMENTO"].ToString()));
                Response.End();
                Response.Flush();
                Response.Clear();
            }
        }

    }



}