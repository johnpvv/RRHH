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
    ClassExcel excel = new ClassExcel();

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
            mens.mensaje(Page, "Sólo se permiten archivos Excel 2007 en adelante (.xlsx)");
            return;
        }

        string archivo = Server.MapPath("~/TempExcel/");

        if (!Directory.Exists(archivo))
        {
            Directory.CreateDirectory(archivo);
        }

        string ruta = Path.Combine(archivo, Guid.NewGuid().ToString() + extension);
        fuExcel.SaveAs(ruta);


        DataTable dt = excel.LeerExcel(ruta);

        string[] columnas = { "RUT", "NOMBRES", "CODIGO_TURNO" }; //VALIDAR CONTENIDO COLUMNAS
        string val = excel.ValidarColumnas(dt, columnas);

        if (val != "")
        {
            mens.mensaje(Page, "Hay un Inconveniente con el archivo: " + val);
            this.lblResultado.Text = "Error al cargar el archivo: " + fuExcel.FileName + " (" + val + ")";
            return;
        }

        List<string> erroresExcel = excel.ValidarDatosTurnos(dt);
        if (erroresExcel.Count > 0)
        {
            this.lblResultado.Text = "";
            dgData.DataSource = null;
            dgData.DataBind();
            this.lbMensaje.Text = "Se han encontrado los siguientes errores en el archivo Excel: <br/>";
            foreach (string error in erroresExcel)
            {
                this.lbMensaje.Text += "• " + error + "<br/>";
            }
            return;
        }

        List<string> erroresBD = excel.ValidarDatosBD(dt);
        if (erroresBD.Count > 0)
        {
            this.lblResultado.Text = "";
            dgData.DataSource = null;
            dgData.DataBind();
            this.lbMensaje.Text = "Se han encontrado los siguientes errores en los Datos del Archivo: <br/>";
            foreach (string error in erroresBD)
            {
                this.lbMensaje.Text += "• " + error + "<br/>";
            }
            return;
        }
        tur.ls_iduselim = Session["user"].ToString();
        string lsInsert = tur.mfCargaTurnos(dt);
        if (lsInsert != "")
        {
            mens.mensaje(Page, "Hubo un error al insertar los registros: " + lsInsert);
            this.lblResultado.Text = "Error al cargar el archivo: " + fuExcel.FileName + " (" + lsInsert + ")";
            return;
        }
        dgData.DataSource = dt;
        dgData.DataBind();
        this.lblResultado.Text = "Archivo cargado correctamente: " + fuExcel.FileName;
        this.lbMensaje.Text = "";
        File.Delete(ruta);
    }
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
    protected void btnPlantilla_Click(object sender, EventArgs e)
    {
        string ruta = Server.MapPath("~/contenido/Plantillas/Plantilla_Turnos.xlsx");

        if (!System.IO.File.Exists(ruta))
        {
            mens.mensaje(Page, "No se encontró la plantilla de turnos.");
            return;
        }
        Response.Clear();
        Response.ClearHeaders();
        Response.ClearContent();
        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        Response.AddHeader("Content-Disposition", "attachment; filename=Plantilla_Turnos.xlsx");
        Response.WriteFile(ruta);

        Response.End();
    }
}
