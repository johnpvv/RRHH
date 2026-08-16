<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VistaDatosTrabajador.aspx.cs" Inherits="contenido_RRHH_VistaDatosTrabajador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Datos Personales</title>
    <style type="text/css">
        .PortalFicha {
            width: 95%;
            margin: 15px auto;
            font-family: Arial, Helvetica, sans-serif;
        }

        .CardPerfil {
            background: #FFFFFF;
            border: 1px solid #DDE3EA;
            border-radius: 8px;
            margin-bottom: 15px;
            padding: 15px;
            box-shadow: 0px 1px 4px rgba(0,0,0,0.15);
        }

        .CardTitulo {
            font-size: 18px;
            font-weight: bold;
            color: #1F6FB2;
            border-bottom: 1px solid #E5E5E5;
            padding-bottom: 8px;
            margin-bottom: 15px;
        }

        .TablaPerfil {
            width: 100%;
        }
            .TablaPerfil td {
                padding: 8px;
            }

        .CampoTitulo {
            font-weight: bold;
            color: #555555;
            width: 180px;
        }

        .CampoLectura {
            color: #333333;
            background-color: #F8F9FA;
            border: 1px solid #DDDDDD;
            padding: 5px;
        }

        .TextoPortal {
            width: 350px;
            padding: 6px;
            border: 1px solid #CCCCCC;
            border-radius: 4px;
        }

        .ComboPortal {
            width: 250px;
            padding: 5px;
        }

        .BarraBotones {
            text-align: center;
            margin-top: 20px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="PortalFicha">
            <div class="CardPerfil">
                <div class="CardTitulo">
                    👤 Información Personal
                </div>

                <table class="TablaPerfil">

                    <tr>
                        <td class="CampoTitulo">Estado:</td>
                        <td>
                            <asp:Label ID="lbEstado" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">RUT:</td>
                        <td>
                            <asp:Label ID="lblRutCompleto" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Nombre Completo:</td>
                        <td>
                            <asp:Label ID="lblNombreCompleto" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Nombre Social:</td>
                        <td>
                            <asp:Label ID="lblNombreSocial" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Fecha Nacimiento:</td>
                        <td>
                            <asp:Label ID="lblFechaNacimiento" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Sexo:</td>
                        <td>
                            <asp:Label ID="lblSexo" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Estado Civil:</td>
                        <td>
                            <asp:DropDownList ID="ddlEstadoCivil"
                                runat="server"
                                CssClass="ComboPortal">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Previsión:</td>
                        <td>
                            <asp:DropDownList ID="ddlPrevision"
                                runat="server"
                                CssClass="ComboPortal">
                            </asp:DropDownList>
                        </td>
                    </tr>

                </table>

            </div>
            <div class="CardPerfil">

                <div class="CardTitulo">
                    🏠 Dirección
                </div>
                <table class="TablaPerfil">
                    <tr>
                        <td class="CampoTitulo">Dirección:</td>
                        <td>
                            <asp:TextBox ID="TxtDire" runat="server" CssClass="TextoPortal">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Región:</td>
                        <td>
                            <asp:DropDownList ID="ddlRegion"
                                runat="server"
                                CssClass="ComboPortal"
                                AutoPostBack="True"
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td class="CampoTitulo">Comuna:</td>
                        <td>
                            <asp:DropDownList ID="ddlComuna" runat="server" CssClass="ComboPortal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="CardPerfil">
                <div class="CardTitulo">
                    📞 Información de Contacto
                </div>
                <table class="TablaPerfil">
                    <tr>
                        <td class="CampoTitulo">Correo Electrónico:</td>
                        <td>
                            <asp:TextBox ID="TMail" runat="server" CssClass="TextoPortal">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Celular Principal:</td>
                        <td>
                            <asp:TextBox ID="TFono1" runat="server" CssClass="TextoPortal" MaxLength="9">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Celular Secundario:</td>
                        <td>
                            <asp:TextBox ID="TFono2" runat="server" CssClass="TextoPortal" MaxLength="9">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="CardPerfil">
                <div class="CardTitulo">
                    🏢 Información Laboral
                </div>
                <table class="TablaPerfil">
                    <tr>
                        <td class="CampoTitulo">Unidad:</td>
                        <td>
                            <asp:Label ID="lblUnidad" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Cargo:</td>
                        <td>
                            <asp:Label ID="lblCargo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Tipo Contrato:</td>
                        <td>
                            <asp:Label ID="lblTipoContrato" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Fecha Ingreso:</td>
                        <td>
                            <asp:Label ID="lblFechaIngreso" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Jefatura:</td>
                        <td>
                            <asp:Label ID="lblJefatura" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="BarraBotones">
                <asp:Button ID="btnGuardar"
                    runat="server"
                    Text="Guardar Cambios"
                    CssClass="BotonPortalVerde"
                    OnClick="btnGuardar_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="btnVolver"
                    runat="server"
                    Text="Volver"
                    CssClass="BotonPortalGris"
                    OnClick="btnVolver_Click" />

            </div>
        </div>

    </form>
</body>
</html>
