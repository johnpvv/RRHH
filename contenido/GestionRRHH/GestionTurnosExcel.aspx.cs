using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_GestionRRHH_GestionTurnosExcel : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    Usuarios usr = new Usuarios();
    ClassTurnos tur = new ClassTurnos();
    ClassHorarios hor = new ClassHorarios();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCargar_Click(object sender, EventArgs e)
    {
        if (!fuExcel.HasFile)
        {
            mens.mensaje(Page, "Debe seleccionar un archivo...");
            return;
        }

        string extension = Path.GetExtension(fuExcel.FileName).ToLower();
        if (extension != ".xlsx")
        {
            mens.mensaje(Page, "Sólo se permiten archivos Excel 2007 en adelante.");
            return;
        }

        string archivo = Server.MapPath("~/TempExcel/");

        if (!Directory.Exists(archivo))
            Directory.CreateDirectory(archivo);

        string ruta = Path.Combine(archivo, Guid.NewGuid().ToString() + extension);
        fuExcel.SaveAs(ruta);
        this.lblResultado.Text = "Archivo cargado: " + fuExcel.FileName;
        ClassExcel excel = new ClassExcel();
        DataTable dt = excel.LeerExcel(ruta);
        Session["CargaExcel"] = dt;
        dgData.DataSource = dt;
        dgData.DataBind();

        File.Delete(ruta);
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
}