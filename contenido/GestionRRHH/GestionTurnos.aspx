<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionTurnos.aspx.cs" Inherits="contenido_GestionRRHH_GestionTurnos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RRHH_GestionTurnos</title>
    <script src="../../js/common.js" type="text/javascript"></script>
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
                            Turnos --&gt; Antecedentes --&gt;
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
                                            <asp:TextBox ID="TxtId" runat="server" Width="141px" Font-Bold="True" Enabled="False"></asp:TextBox>
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
                                                MaxLength="700"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Código:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="txtCod" runat="server" Width="141px" Font-Bold="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Considera Feriados:</td>
                                        <td class="TextoLeft">
                                            <asp:CheckBox ID="chkFer" runat="server" Font-Bold="True"></asp:CheckBox>
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
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btn_Agregar"
                                    Text="Grabar"
                                    class="BotonPortalAzul"
                                    runat="server"
                                    OnClick="btn_Agregar_Click"
                                    OnClientClick="return ConfirmarGuardar();" />&nbsp
                                <asp:Button ID="btn_habilitar"
                                    runat="server"
                                    class="BotonPortalAmarillo"
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
                                        Width="60px"
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
                <ContentTemplate>
                    <table border="0" style="width: 811px">
                        <tr>
                            <td width="170">
                                <asp:Button ID="BtBuscarUser"
                                    runat="server"
                                    Text="Buscar"
                                    OnClick="BtBuscarUser_Click"
                                    CssClass="BotonPortalAzul" />
                            </td>
                            <td width="147">
                                <asp:Button ID="btnVolver_2"
                                    runat="server"
                                    Text="Volver"
                                    CssClass="BotonPortalGris"
                                    OnClick="btnVolver_Click" /></td>
                            <td class="auto-style5">&nbsp;</td>
                            <td class="auto-style3">&nbsp;</td>
                            <td width="93">&nbsp;</td>
                            <td width="235">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                        </tr>
                        <tr>
                            <td class="TextoRigth">Nombre:</td>
                            <td colspan="5" class="TextoLeft">
                                <asp:TextBox ID="TNombreUsr" runat="server" Height="24px" Width="307px" MaxLength="80"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="TextoRigth">Rut:</td>
                            <td class="TextoLeft">
                                <asp:TextBox ID="TRut" runat="server"></asp:TextBox></td>
                            <td class="auto-style2"></td>
                            <td class="auto-style4">
                                <asp:RadioButtonList ID="rbLista" runat="server" CssClass="TextoCheck" RepeatDirection="Horizontal" Width="258px">
                                    <asp:ListItem Selected="True" Value="1">Disponibles</asp:ListItem>
                                    <asp:ListItem Value="2">Asignados</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                        </tr>
                        <tr>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>
                        </tr>
                    </table>
                    <table border="0" style="width: 900px">
                        <tr>
                            <td class="TextoCenter">Disponibles</td>
                            <td align="left" class="style1">&nbsp;</td>
                            <td class="TextoCenter">Asociados</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="1">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gbUserDisp" runat="server" AutoGenerateColumns="False"
                                                Font-Names="Tahoma" Font-Size="Small" ForeColor="#333333"
                                                GridLines="None"
                                                OnRowDataBound="gbUserDisp_RowDataBound"
                                                OnSelectedIndexChanged="gbUserDisp_SelectedIndexChanged"
                                                Width="420px"
                                                CssClass="GridGral"
                                                EmptyDataRowStyle-CssClass="textoEmpty"
                                                EmptyDataText="No Hay Profesionales Disponibles"
                                                AllowPaging="True"
                                                OnPageIndexChanging="gbUserDisp_PageIndexChanging"
                                                DataKeyNames="idusuario"
                                                PageSize="20">
                                                <Columns>
                                                    <asp:BoundField DataField="idusuario" HeaderText="Id" ReadOnly="True">
                                                        <ItemStyle CssClass="TextoCenter" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RUT_C" HeaderText="RUT">
                                                        <ItemStyle CssClass="TextoLeft" Font-Bold="true" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                        <ItemStyle CssClass="TextoLeft" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Add">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btn_Add" runat="server" ImageUrl="~/imagenes/check.png" OnClick="AddUser" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                                </Columns>
                                                <HeaderStyle CssClass="GridGralHeader" />
                                                <RowStyle CssClass="GridGralRow" />
                                                <AlternatingRowStyle CssClass="GridGralAltRow" /> 
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left">&nbsp;</td>
                            <td valign="top">
                                <table border="1">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gbUser" runat="server"
                                                AutoGenerateColumns="False"
                                                Font-Names="Tahoma" Font-Size="Small"
                                                ForeColor="#333333" GridLines="None"
                                                OnRowDataBound="gbUser_RowDataBound"
                                                OnSelectedIndexChanged="gbUser_SelectedIndexChanged"
                                                Width="420px"
                                                AllowPaging="True"
                                                CssClass="GridGral"
                                                EmptyDataRowStyle-CssClass="textoEmpty"
                                                EmptyDataText="No Hay Profesionales Asociados"
                                                OnPageIndexChanging="gbUser_PageIndexChanging"
                                                DataKeyNames="IDTURNUS"
                                                PageSize="20">
                                                <Columns>
                                                    <asp:BoundField DataField="IDTURNUS" HeaderText="Id" ReadOnly="True">
                                                        <ItemStyle CssClass="TextoCenter" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RUT" HeaderText="RUT">
                                                        <ItemStyle CssClass="TextoLeft" Font-Bold="true" Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                        <ItemStyle CssClass="TextoLeft" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Elim">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btn_Elim" runat="server" ImageUrl="~/imagenes/close.png" OnClick="ElimUser" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                                </Columns>
                                                <HeaderStyle CssClass="GridGralHeader" />
                                                <RowStyle CssClass="GridGralRow" />
                                                <AlternatingRowStyle CssClass="GridGralAltRow" />
                                                <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
