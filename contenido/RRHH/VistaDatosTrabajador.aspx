<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VistaDatosTrabajador.aspx.cs" Inherits="contenido_RRHH_VistaDatosTrabajador" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Datos Personales</title>
    <script type="text/javascript">
        function mostrarSpinner() {
            var spinner = document.getElementById('spinnerCarga');
            if (spinner) {
                spinner.style.display = 'flex';
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="PortalFicha">
            <div class="CardPerfil">
                <div class="titulo-seccion">
                    👤 Información Personal:
                </div>
                <table class="TablaPerfil">
                    <tr>
                        <td class="CampoTitulo">R.U.N.:</td>
                        <td>
                            <asp:Label ID="lblRutCompleto" runat="server" Font-Bold="true"></asp:Label>
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
                            <asp:DropDownList ID="ddlEstadoCivil" runat="server" CssClass="ComboPortal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Previsión:</td>
                        <td>
                            <asp:DropDownList ID="ddlPrevision" runat="server" CssClass="ComboPortal">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Estado Trabajador:</td>
                        <td>
                            <asp:Label ID="lbEstado" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="CardPerfil">
                <div class="titulo-seccion">
                    🏠 Dirección:
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
                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"
                                onchange="mostrarSpinner();">
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
                <div class="titulo-seccion">
                    📞 Información de Contacto:
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
                        <td class="CampoTitulo">Teléfono Principal:</td>
                        <td>
                            <asp:TextBox ID="TFono1" runat="server" CssClass="TextoPortal" MaxLength="9">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="CampoTitulo">Teléfono Secundario:</td>
                        <td>
                            <asp:TextBox ID="TFono2" runat="server" CssClass="TextoPortal" MaxLength="9">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="CardPerfil">
                <div class="titulo-seccion">
                    🏢 Información Laboral:
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
                    OnClick="btnGuardar_Click"
                    OnClientClick="if (!confirm('¿Desea guardar los cambios?')) return false; mostrarSpinner();" />
                &nbsp;
                <asp:Button ID="btnVolver"
                    runat="server"
                    Text="Volver"
                    CssClass="BotonPortalGris"
                    OnClick="btnVolver_Click" />
            </div>
        </div>
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
</body>
</html>
