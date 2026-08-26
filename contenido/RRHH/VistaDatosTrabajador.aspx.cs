using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contenido_RRHH_VistaDatosTrabajador : System.Web.UI.Page
{
    Mensaje mens = new Mensaje();
    ClassTrabajadores per = new ClassTrabajadores();
    ClassUnidOperativa cu = new ClassUnidOperativa();
    Usuarios usr = new Usuarios();
    static bool nuevo;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet aoDs;
        if (!IsPostBack)
        {
            try
            {
                per.ls_rut = Session["rut"].ToString();
                aoDs = per.ConsultarID();

                if (aoDs != null &&
                    aoDs.Tables.Count > 0 &&
                    aoDs.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = aoDs.Tables[0].Rows[0];

                    // Información Personal
                    lblRutCompleto.Text = dr["RUT"] + "-" + dr["DV"];

                    lblNombreCompleto.Text =
                        dr["NOMBRE"] + " " +
                        dr["AP_PATERNO"] + " " +
                        dr["AP_MATERNO"];

                    lblNombreSocial.Text = dr["NOMBRE_SOCIAL"].ToString();

                    if (!string.IsNullOrEmpty(dr["FECHA_NACIMIENTO"].ToString()))
                    {
                        lblFechaNacimiento.Text =
                            Convert.ToDateTime(dr["FECHA_NACIMIENTO"])
                            .ToString("dd/MM/yyyy");
                    }

                    switch (dr["SEXO"].ToString())
                    {
                        case "M":
                            lblSexo.Text = "Masculino";
                            break;

                        case "F":
                            lblSexo.Text = "Femenino";
                            break;

                        case "O":
                            lblSexo.Text = "Otro";
                            break;

                        default:
                            lblSexo.Text = "";
                            break;
                    }
                    LlenarPrevision();
                    LlenarRegion();
                    LlenarComuna();
                    LlenarEstCivil();

                    // Dirección (editable)
                    TxtDire.Text = dr["DIRECCION"].ToString();
                    ddlRegion.SelectedValue = dr["IDREGION"].ToString();
                    ddlComuna.SelectedValue = dr["IDCOMUNA"].ToString();
                    this.ddlEstadoCivil.SelectedValue = aoDs.Tables[0].Rows[0]["EST_CIVIL"].ToString();
                    this.ddlPrevision.SelectedValue = aoDs.Tables[0].Rows[0]["IDPREVISION"].ToString();

                    // Contacto (editable)
                    TMail.Text = dr["EMAIL"].ToString();
                    TFono1.Text = dr["FONO1"].ToString();
                    TFono2.Text = dr["FONO2"].ToString();

                    // Información Laboral
                    lblUnidad.Text = dr["CENTRO"].ToString();

                    //lblCargo.Text = dr["CARGO"].ToString();

                    //lblTipoContrato.Text = dr["TIPO_CONTRATO"].ToString();

                    //if (!string.IsNullOrEmpty(dr["FECHA_INGRESO"].ToString()))
                    //{
                    //    lblFechaIngreso.Text =
                    //        Convert.ToDateTime(dr["FECHA_INGRESO"])
                    //        .ToString("dd/MM/yyyy");
                    //}

                    //lblJefatura.Text = dr["JEFATURA"].ToString();

                    // Estado
                    if (dr["IDESTADO"].ToString() == "1")
                    {
                        lbEstado.Text = "VIGENTE";
                    }
                    else
                    {
                        lbEstado.Text = "INACTIVO";
                    }

                    //LbTitulo.Text = lblRutCompleto.Text;
                }
            }
            catch (Exception xe)
            {
                Response.Redirect("~/contenido/frmerrgen.aspx");
            }
        }
    }

    protected void ddlRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaComuna(ddlRegion.SelectedValue.ToString());

        this.ddlComuna.DataTextField = "DESCRIPCION";
        this.ddlComuna.DataValueField = "IDCOMUNA";
        this.ddlComuna.DataSource = dat;
        this.ddlComuna.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Comuna";
        item.Value = "0";
        this.ddlComuna.Items.Insert(0, item);
    }


    private void LlenarComuna()
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaComuna();

        this.ddlComuna.DataTextField = "DESCRIPCION";
        this.ddlComuna.DataValueField = "IDCOMUNA";
        this.ddlComuna.DataSource = dat;
        this.ddlComuna.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Comuna";
        item.Value = "0";
        this.ddlComuna.Items.Insert(0, item);
    }

    private void LlenarRegion()
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaRegion();

        this.ddlRegion.DataTextField = "DESCRIPCION";
        this.ddlRegion.DataValueField = "IDREGION";
        this.ddlRegion.DataSource = dat;
        this.ddlRegion.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Region";
        item.Value = "0";
        this.ddlRegion.Items.Insert(0, item);
    }

    private void LlenarEstCivil()
    {
        DataSet dat = new DataSet();

        dat = per.ConsultaEstCivil();

        this.ddlEstadoCivil.DataTextField = "DESCRIPCION";
        this.ddlEstadoCivil.DataValueField = "IDESTADOCIVIL";
        this.ddlEstadoCivil.DataSource = dat;
        this.ddlEstadoCivil.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Estado";
        item.Value = "0";
        this.ddlEstadoCivil.Items.Insert(0, item);


    }
    private void LlenarPrevision()
    {
        DataSet dat = new DataSet();
        dat = per.ConsultaPrevision();

        this.ddlPrevision.DataTextField = "DESCRIPCION";
        this.ddlPrevision.DataValueField = "IDPREVISION";
        this.ddlPrevision.DataSource = dat;
        this.ddlPrevision.DataBind();

        System.Web.UI.WebControls.ListItem item = new ListItem();
        item.Text = "Seleccione Unidad";
        item.Value = "0";
        this.ddlPrevision.Items.Insert(0, item);
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        //falta agregar esta logica - JOHN
    }

    protected void btnVolver_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/contenido/frmblksiab.aspx");
    }
}
