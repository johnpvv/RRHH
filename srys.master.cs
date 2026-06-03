using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class srys : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void OnClick(object sender, MenuEventArgs e)
    {

       switch (int.Parse(e.Item.Value))
        {
            case 205:
                Response.Redirect("~/contenido/Gestion/Prueba.aspx");
                break;
            case 301:
                Response.Redirect("~/contenido/PAC/AnalisisPAC.aspx");
                break;
            case 302:
                Response.Redirect("~/contenido/PAC/CargaPAC.aspx");
                break;
            case 303:
                Response.Redirect("~/contenido/PAC/AsociaProdServ.aspx");
                break;
            case 304:
                Response.Redirect("~/contenido/Gestion/GestArticulos.aspx");
                break;
            case 305:
                Response.Redirect("~/contenido/Gestion/ValidaComision.aspx");
                break;
            default:
                break;
        }

    }
}