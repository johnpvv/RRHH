<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionTurnos.aspx.cs" Inherits="contenido_GestionRRHH_GestionTurnos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RRHH_GestionTurnos</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />
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
    <style type="text/css">
        .success {
            background-color: #4CAF50;
            color: #FDFEFE;
        }

        .info {
            background-color: #2196F3;
            color: #FDFEFE;
        }

        .labelBlue {
            background-color: #4682B4;
            color: #FDFEFE;
        }

        .warning {
            background-color: #ff9800;
            color: #FDFEFE;
        }

        .warningDark {
            background-color: darkmagenta;
            color: white;
        }

        .goldBlack {
            background-color: gold;
            color: black;
        }

        .danger {
            background-color: #f44336;
            color: #FDFEFE;
        }

        .other {
            background-color: #e7e7e7;
            color: black;
        }

        .auto-style18 {
            width: 787px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <table class="auto-style18">
            <tr>
                <td class="TextoLeft">Gestión
                        <label>
                            Turnos --&gt; Antecedentes --&gt;
                        <asp:Label ID="LbTitulo" runat="server" Text="Label"></asp:Label>
                        </label>
                </td>
                <td style="text-align: right">
                </td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="700px" Width="900px"
            Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto" ActiveTabIndex="0">

            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                <HeaderTemplate>Información General</HeaderTemplate>
                <ContentTemplate>
                    <table border="0" style="width: 860px;" class="table table-hover table-bordered">
                        <tr>
                            <td class="auto-style52">
                                <table border="0" style="width: 860px;">
                                    <tr>
                                        <td class="auto-style57">
                                            <asp:Button ID="btn_Agregar" Height="35px" Text="Grabar" Width="150px"
                                                class="success" runat="server"
                                                OnClick="btn_Agregar_Click"
                                                OnClientClick="return ConfirmarGuardar();" />
                                        </td>

                                        <td class="auto-style58">
                                            <asp:Button ID="btn_habilitar"
                                                runat="server" class="goldBlack"
                                                Text="Habilitar" Width="150px" Height="35px"
                                                OnClick="btn_habilitar_Click"
                                                OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" />
                                        </td>
                                    </tr>
                                </table>
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
                                        <td class="TextoRigth">Código:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="txtCod" runat="server" Width="141px" Required="true" Font-Bold="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Considera Feriados:</td>
                                        <td class="TextoLeft">
                                            <asp:CheckBox ID="chkFer" runat="server" Required="true" Font-Bold="True"></asp:CheckBox>
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
                                            <asp:HiddenField ID="hdIdTurno" runat="server" />
                                        </td>
                                    </tr>
                                </table>
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
                <HeaderTemplate>Detalle Turno Días</HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="dgSemana"
                        runat="server"
                        AutoGenerateColumns="False"
                        CssClass="GridGral"
                        Width="100%"
                        GridLines="None"
                        OnRowDataBound="dgSemana_RowDataBound"
                        DataKeyNames="IDTURNODIA,IDDIA">
                        <Columns>
                            <asp:TemplateField HeaderText="Día" ItemStyle-CssClass="textoGridBold">
                                <ItemTemplate>
                                    <%# Eval("DIA") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trabaja" ItemStyle-CssClass="textoGrid">
                                <ItemTemplate>
                                    <asp:CheckBox
                                        ID="chkTrabaja"
                                        runat="server"
                                        Checked='<%# Eval("IDTURNODIA") != DBNull.Value %>'
                                        AutoPostBack="true"
                                        OnCheckedChanged="chkTrabaja_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción" ItemStyle-CssClass="textoGrid">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlHorario" runat="server"
                                        CssClass="GridGralRow"
                                        Width="240px"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duración Horas" ItemStyle-CssClass="textoGridBold">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHr"
                                        runat="server"
                                        Width="50px"
                                        ReadOnly="true"
                                        Enabled="false"
                                        CssClass="textoGridBold" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entrada" ItemStyle-CssClass="textoGridBold">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIni"
                                        runat="server"
                                        Width="50px"
                                        ReadOnly="true"
                                        Enabled="false"
                                        CssClass="textoGridBold" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salida" ItemStyle-CssClass="textoGridBold">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFin"
                                        runat="server"
                                        Width="50px"
                                        ReadOnly="true"
                                        Enabled="false"
                                        CssClass="textoGridBold" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridGralHeader" />
                        <RowStyle CssClass="GridGralRow" />
                        <AlternatingRowStyle CssClass="GridGralAltRow" />
                    </asp:GridView>
                    <br />
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnGuardarDetalle"
                                    runat="server"
                                    Text="Guardar Turno"
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
            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                <HeaderTemplate>Personas</HeaderTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
