<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionHorarios.aspx.cs" Inherits="contenido_GestionRRHH_GestionHorarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RRHH_GestionHoras</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/all.min.css" rel="stylesheet" />
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" />

    <script type="text/javascript">

        function ConfirmarGuardar() {
            if (confirm('¿Desea guardar los Cambios?')) {
                document.getElementById('divCargando').style.display = 'block';
                return true;
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <table class="TextoLeft">
            <tr>
                <td class="TextoLeft">Gestión
                        <label>
                            Horarios --&gt; Antecedentes --&gt;
                        <asp:Label ID="LbTitulo" runat="server" Text="Label"></asp:Label>
                        </label>
                </td>
                <td style="text-align: right"></td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="700px" Width="960px"
            Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto" ActiveTabIndex="0">

            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                <HeaderTemplate>Información General</HeaderTemplate>
                <ContentTemplate>
                    <table border="0" style="width: 900px;" class="table table-hover table-bordered">
                        <tr>
                            <td>
                                <table style="width: 100%; margin-top: 10px;" border="0">
                                    <tr>
                                        <td colspan="2" style="font-weight: bold; background-color: #EAEAEA; padding: 5px;">Información Básica:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 200px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth"></td>
                                        <td class="TextoLeft"></td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Estado:</td>
                                        <td class="TextoLeft">
                                            <asp:Label ID="lbEstado" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">ID:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtId" runat="server" Width="141px" Required="true" Font-Bold="True" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Descripción:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtDescr"
                                                runat="server"
                                                Width="350px"
                                                Height="80px"
                                                TextMode="MultiLine"
                                                MaxLength="700" Required="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Fecha Creacion:</td>
                                        <td>
                                            <asp:TextBox ID="txtFCrea" runat="server" Width="120px" Enabled="False"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CE2" TargetControlID="txtFCrea"
                                                runat="server" BehaviorID="_content_CE2"></ajaxToolkit:CalendarExtender>
                                            <ajaxToolkit:MaskedEditExtender ID="MEE"
                                                TargetControlID="txtFCrea"
                                                Mask="99/99/9999" runat="server"
                                                MaskType="Date" BehaviorID="_content_MEE" Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""></ajaxToolkit:MaskedEditExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HiddenField ID="hdIdHora" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btn_Agregar"
                                    Text="Grabar"
                                    Cssclass="BotonPortalAzul"
                                    runat="server"
                                    OnClick="btn_Agregar_Click"
                                    OnClientClick="return ConfirmarGuardar();" />&nbsp
                                <asp:Button ID="btn_habilitar"
                                    runat="server"
                                    Cssclass="BotonPortalAmarillo"
                                    Text="Habilitar"
                                    OnClick="btn_habilitar_Click"
                                    OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" />&nbsp
                                <asp:Button ID="btnVolver_1"
                                    runat="server"
                                    Text="Volver"
                                    CssClass="BotonPortalGris"
                                    OnClick="btnVolver_Click" />
                            </td>
                        </tr>
                    </table>
                    <div id="divCargando" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.3); z-index: 9999;">

                        <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%); width: 300px; height: 180px; background: white; border-radius: 10px; text-align: center; padding-top: 30px; box-shadow: 0 0 15px #666;">

                            <img src="../../imagenes/ajax-loader.gif" style="width: 120px; height: 120px;" alt="Cargando..." />
                            <br />
                            <br />
                            <span style="font-size: 16px; font-weight: bold;">Guardando información...
                            </span>

                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>Detalle Horario</HeaderTemplate>
                <ContentTemplate>
                    <table class="GridGral" style="width: 900px; margin: auto;">
                        <tr>
                            <td colspan="2" class="TituloSeccion">
                                Detalle del Horario
                            </td>
                        </tr>
                        <tr>
                            <td class="TextoLeft">&nbsp;</td>
                            <td class="TextoLeft">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textoNormRigth">Intervalo en Minutos:
                            </td>
                            <td class="textoNormLeft">
                                <asp:TextBox ID="txtint" runat="server" Width="60px" CssClass="TextBoxHora"></asp:TextBox>
                                <asp:Button ID="btnCalcular"
                                    runat="server"
                                    Text="Generar Intervalos"
                                    CssClass="BotonPortalVerde"
                                    OnClick="btnCalcular_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TextoLeft">&nbsp;</td>
                            <td class="TextoLeft">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textoNormRigth">Hora de Entrada:
                            </td>
                            <td class="textoNormLeft">
                                <asp:DropDownList ID="ddlHoraEntrada"
                                    runat="server"
                                    Width="120px"
                                    CssClass="GridGralRow"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="CalcularHorario">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="textoNormRigth">Hora de Salida:
                            </td>
                            <td class="textoNormLeft">
                                <asp:DropDownList ID="ddlHoraSalida"
                                    runat="server"
                                    Width="120px"
                                    AutoPostBack="true"
                                    CssClass="GridGralRow"
                                    OnSelectedIndexChanged="CalcularHorario">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="TextoLeft">&nbsp;</td>
                            <td class="TextoLeft">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="textoNormRigth">Duración:
                            </td>
                            <td class="textoNormLeft">
                                <asp:TextBox ID="txtHoras"
                                    runat="server"
                                    Width="60px"
                                    Enabled="false"
                                    CssClass="TextBoxHora" />
                                <span>Horas</span>
                                <asp:TextBox ID="txtMinuto"
                                    runat="server"
                                    Width="60px"
                                    Enabled="false"
                                    CssClass="TextBoxHora" />
                                <span>Minutos</span>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnGuardarDetalle"
                                    runat="server"
                                    Text="Guardar Horario"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnGuardarDetalle_Click" />
                                <asp:Button ID="btnVolver"
                                    runat="server"
                                    Text="Volver"
                                    CssClass="BotonPortalGris"
                                    OnClick="btnVolver_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResultado" runat="server" ForeColor="Red"></asp:Label>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
