using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_RRHH_VistaTurnosTrab : System.Web.UI.Page
{
    ClassReloj rlj = new ClassReloj();
    ClassTurnos tur = new ClassTurnos();
    ClassTrabajadores usr = new ClassTrabajadores();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarMeses();
            CargarAnios();
            CargarTurnosTrab();
        }
    }
    private void CargarMeses()
    {
        DataSet ds = tur.mfGenerarMeses();
        ddlMes.DataSource = ds.Tables[0];
        ddlMes.DataTextField = "MES";
        ddlMes.DataValueField = "IDMES";
        ddlMes.DataBind();
        ddlMes.SelectedValue = DateTime.Now.Month.ToString();
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
    private void CargarTurnosTrab()
    {
        usr.ls_rut = Session["rut"].ToString();
        tur.ls_user = usr.mfDevuelveID();

        DataSet dsTurno = tur.mfBuscarTurnoActivoTrab();

        if (dsTurno == null || dsTurno.Tables.Count == 0 || dsTurno.Tables[0].Rows.Count == 0)
        {
            dgData.DataSource = null;
            dgData.DataBind();
            return;
        }

        DataRow dr = dsTurno.Tables[0].Rows[0];

        string idTurno = dr["IDTURNOS"].ToString();
        string tipoTurno = dr["TIPO"].ToString();

        DataSet ds;

        tur.ls_mes = ddlMes.SelectedValue;
        tur.ls_anio = ddlAnio.SelectedValue;
        tur.ls_idturno = idTurno;

        if (tipoTurno == "1")
        {
            ds = tur.mfBuscarTurnosTrabMes();
        }
        else
        {
            ds = tur.mfBuscarTurnosTrab();
            this.ddlAnio.Enabled = false;
            this.ddlMes.Enabled = false;
            this.ddlVista.Enabled = false;
        }

        dgData.DataSource = ds;
        dgData.DataBind();
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        CargarVista();
    }
    protected void ddlVista_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarVista();
    }
    private void CargarVista()
    {
        if (ddlVista.SelectedValue == "1")
        {
            pnlLista.Visible = true;
            pnlCalendario.Visible = false;
            CargarTurnosTrab();
        }
        else
        {
            pnlLista.Visible = false;
            pnlCalendario.Visible = true;
            CargarCalendario();
        }
    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {

    }

    protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarVista();
    }

    protected void ddlAnio_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarVista();
    }
    #region calendario
    private void CargarCalendario()
    {
        usr.ls_rut = Session["rut"].ToString();
        tur.ls_user = usr.mfDevuelveID();
        tur.ls_mes = ddlMes.SelectedValue;
        tur.ls_anio = ddlAnio.SelectedValue;

        DataSet ds = tur.mfBuscarTurnosTrabMes();
        DataTable dt = ds.Tables[0];

        DateTime primerDia = new DateTime(Convert.ToInt32(ddlAnio.SelectedValue), Convert.ToInt32(ddlMes.SelectedValue), 1);
        DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);
        StringBuilder sb = new StringBuilder();
        sb.Append("<div class='calendario'>");
        sb.Append("<div class='calendario-header'>");
        sb.Append("<div>Lunes</div>");
        sb.Append("<div>Martes</div>");
        sb.Append("<div>Miércoles</div>");
        sb.Append("<div>Jueves</div>");
        sb.Append("<div>Viernes</div>");
        sb.Append("<div>Sábado</div>");
        sb.Append("<div>Domingo</div>");
        sb.Append("</div>");

        DateTime fecha = primerDia;
        int diaSemana = ((int)primerDia.DayOfWeek + 6) % 7;
        sb.Append("<div class='calendario-semana'>");
        for (int i = 0; i < diaSemana; i++)
        {
            sb.Append("<div class='calendario-dia calendario-dia-vacio'></div>");
        }

        while (fecha <= ultimoDia)
        {
            if (fecha.DayOfWeek == DayOfWeek.Monday && fecha != primerDia)
            {
                sb.Append("</div>");
                sb.Append("<div class='calendario-semana'>");
            }

            DataRow[] filas = dt.Select("FECHA = '" + fecha.ToString("dd/MM/yyyy") + "'");

            string claseDia = "calendario-dia";

            if (fecha.DayOfWeek == DayOfWeek.Sunday)
                claseDia = "calendario-feriado";

            //if (filas.Length > 0 && Convert.ToInt32(filas[0]["FERIADOS"]) == 1)//para cuando hagamos feriados
            //    claseDia = "calendario-feriado";

            sb.Append("<div class='" + claseDia + "'>");
            sb.Append("<div class='calendario-numero'>");
            sb.Append(fecha.Day);
            sb.Append("</div>");

            if (filas.Length > 0)
            {
                foreach (DataRow dr in filas)
                {
                    sb.Append("<div class='calendario-turno'>");
                    sb.Append(Server.HtmlEncode(dr["TURNO"].ToString()));
                    sb.Append("</div>");

                    sb.Append("<div class='calendario-horario'>");
                    sb.Append(FormatearHora(dr["HORA_INI"]));
                    sb.Append(" - ");
                    sb.Append(FormatearHora(dr["HORA_FIN"]));
                    sb.Append("</div>");

                    sb.Append("<div class='calendario-duracion'>");
                    sb.Append(dr["HORA"]);
                    sb.Append(" hora(s)");

                    if (Convert.ToInt32(dr["MINUTO"]) > 0)
                    {
                        sb.Append(" ");
                        sb.Append(dr["MINUTO"]);
                        sb.Append(" min.");
                    }

                    sb.Append("</div>");
                }
            }
            else
            {
                sb.Append("<div class='calendario-libre'>");
                sb.Append("Sin turno");
                sb.Append("</div>");
            }

            sb.Append("</div>");

            fecha = fecha.AddDays(1);
        }

        int espaciosFinales = 7 - ((int)ultimoDia.DayOfWeek + 6) % 7 - 1;

        for (int i = 0; i < espaciosFinales; i++)
        {
            sb.Append("<div class='calendario-dia calendario-dia-vacio'></div>");
        }

        sb.Append("</div>");
        sb.Append("</div>");

        litCalendario.Text = sb.ToString();
    }
    private string FormatearHora(object valor)
    {
        if (valor == null || valor == DBNull.Value)
            return "";

        DateTime hora;

        if (DateTime.TryParse(valor.ToString(), out hora))
            return hora.ToString("HH:mm");

        return valor.ToString();
    }
    #endregion
}