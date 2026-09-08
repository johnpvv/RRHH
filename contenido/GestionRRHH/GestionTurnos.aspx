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
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <div class="bloque">
            <div class="bloque-titulo">
                Gestión Turnos --&gt; Antecedentes --&gt;
                <asp:Label ID="LbTitulo" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="100%" Width="100%" ScrollBars="Auto" ActiveTabIndex="0">

            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                <HeaderTemplate>Información General</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Información del Turno       
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>ID:</label>
                                <asp:TextBox ID="TxtId" runat="server"
                                    CssClass="form-control"
                                    Width="120px"
                                    Enabled="False"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Estado:</label>
                                <asp:TextBox ID="lbEstado" runat="server"
                                    CssClass="form-control"
                                    Width="300px"
                                    Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Descripción:</label>
                                <asp:TextBox ID="TxtDescr"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="500px"
                                    Height="70px"
                                    TextMode="MultiLine"
                                    MaxLength="700"></asp:TextBox>
                            </div>
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Código:</label>
                                <asp:TextBox ID="txtCod"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="140px"
                                    Font-Bold="True"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Fecha Creación:</label>
                                <asp:TextBox ID="txtFCrea"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="120px"
                                    Enabled="False"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender
                                    ID="CE2"
                                    TargetControlID="txtFCrea"
                                    runat="server"
                                    BehaviorID="_content_CE2"></ajaxToolkit:CalendarExtender>
                                <ajaxToolkit:MaskedEditExtender
                                    ID="MEE"
                                    TargetControlID="txtFCrea"
                                    Mask="99/99/9999"
                                    runat="server"
                                    MaskType="Date"
                                    BehaviorID="_content_MEE"
                                    Century="2000" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="" CultureTimePlaceholder=""></ajaxToolkit:MaskedEditExtender>
                            </div>
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Considera Feriados:</label>
                                <asp:CheckBox ID="chkFer"
                                    runat="server"
                                    CssClass="TextoCheck"></asp:CheckBox>
                            </div>
                            <div class="campo">
                                <label>Turno Mensual:</label>
                                <asp:CheckBox ID="chkTipo"
                                    runat="server"
                                    CssClass="TextoCheck"></asp:CheckBox>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdIdTurno" runat="server" />
                    </div>
                    <div class="botones-form">
                        <asp:Button ID="btn_Agregar"
                            runat="server"
                            Text="Grabar"
                            CssClass="BotonPortalAzul"
                            OnClick="btn_Agregar_Click"
                            OnClientClick="if (!confirm('¿Desea guardar los cambios..?')) return false; mostrarSpinner();" />
                        <asp:Button ID="btn_habilitar"
                            runat="server"
                            Text="Habilitar"
                            CssClass="BotonPortalAmarillo"
                            OnClick="btn_habilitar_Click"
                            OnClientClick="if (!confirm('¿Desea realizar la actualización de los Datos..?')) return false; mostrarSpinner();" />
                        <asp:Button ID="btnVolver_1"
                            runat="server"
                            Text="Volver"
                            CssClass="BotonPortalGris"
                            OnClick="btnVolver_Click" />
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
                            <asp:TemplateField HeaderText="Día">
                                <ItemTemplate>
                                    <%# Eval("DIA") %>
                                </ItemTemplate>
                                <ItemStyle CssClass="textoGridBold" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trabaja">
                                <ItemTemplate>
                                    <asp:CheckBox
                                        ID="chkTrabaja"
                                        runat="server"
                                        Checked='<%# Eval("IDTURNODIA") != DBNull.Value %>'
                                        AutoPostBack="true"
                                        OnCheckedChanged="chkTrabaja_CheckedChanged" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" CssClass="textoGrid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Descripción">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlHorario" runat="server"
                                        CssClass="GridGralRow"
                                        Width="260px"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlHorario_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle CssClass="textoGrid" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Duración Horas">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHr" runat="server" Width="60px" ReadOnly="true" Enabled="false" CssClass="textoGridBold" />
                                </ItemTemplate>
                                <ItemStyle CssClass="textoGridBold" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Entrada">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtIni" runat="server" Width="50px" ReadOnly="true" Enabled="false" CssClass="textoGridBold" />
                                </ItemTemplate>
                                <ItemStyle CssClass="textoGridBold" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salida">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtFin" runat="server" Width="50px" ReadOnly="true" Enabled="false" CssClass="textoGridBold" />
                                </ItemTemplate>
                                <ItemStyle CssClass="textoGridBold" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridGralHeader" />
                        <RowStyle CssClass="GridGralRow" />
                        <AlternatingRowStyle CssClass="GridGralAltRow" />
                    </asp:GridView>
                    <hr />
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td class="textoNormRigth">
                                <asp:Label ID="lblTotal" runat="server" CssClass="form-control"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnGuardarDetalle"
                                    runat="server"
                                    Text="Guardar Turno"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnGuardarDetalle_Click"
                                    OnClientClick="mostrarSpinner();"/>
                                <asp:Button ID="btnVolver_2"
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

            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="TabMes">
                <HeaderTemplate>Detalle Turno Mensual</HeaderTemplate>
                <ContentTemplate>
                    <table class="table table-bordered" style="width: 100%;">
                        <tr>
                            <td class="TextoRigth" style="width: 50px;">Año:
                            </td>
                            <td class="TextoLeft" style="width: 50px;">
                                <asp:DropDownList ID="ddlAnio" runat="server" Width="80px" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td class="TextoRigth" style="width: 50px;">Mes:
                            </td>
                            <td class="TextoLeft" style="width: 200px;">
                                <asp:DropDownList ID="ddlMes" runat="server" Width="150px" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:Button ID="btnGenerarMes" runat="server" 
                                    CssClass="BotonPortalVerde" OnClientClick="mostrarSpinner();"
                                    OnClick="btnGenerarMes_Click" Text="Cargar Mes" />
                            </td>
                            <td class="TextoRigth" style="width: 80px;">Rellenar:</td>
                            <td class="TextoLeft" style="width: 200px;">
                                <asp:DropDownList ID="ddlPatron" runat="server" CssClass="form-control" Width="180px">
                                    <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                                    <asp:ListItem Value="1-1">1 día / 1 libre</asp:ListItem>
                                    <asp:ListItem Value="2-2">2 días / 2 libres</asp:ListItem>
                                    <asp:ListItem Value="2-3">2 días / 3 libres</asp:ListItem>
                                    <asp:ListItem Value="3-3">3 días / 3 libres</asp:ListItem>
                                    <asp:ListItem Value="4-4">4 días / 4 libres</asp:ListItem>
                                    <asp:ListItem Value="5-2">5 días / 2 libres</asp:ListItem>
                                    <asp:ListItem Value="6-1">6 días / 1 libre</asp:ListItem>
                                    <asp:ListItem Value="7-7">7 días / 7 libres</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btnAplicarPatron" runat="server" 
                                    CssClass="BotonPortalAmarillo" 
                                    OnClientClick="mostrarSpinner();"
                                    OnClick="btnAplicarPatron_Click" Text="Aplicar" />
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="updateMes" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="dgMes"
                                runat="server"
                                AutoGenerateColumns="False"
                                CssClass="GridGral"
                                Width="100%"
                                GridLines="None"
                                OnRowDataBound="dgMes_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Fecha">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdIdTurnoDia" runat="server" Value='<%# Eval("IDTURNODIA") %>' />
                                            <asp:HiddenField ID="hdIdDia" runat="server" Value='<%# Eval("IDDIA") %>' />
                                            <asp:HiddenField ID="hdFecha" runat="server" Value='<%# Eval("FECHA", "{0:yyyy-MM-dd}") %>' />
                                            <%# Eval("FECHA", "{0:dd/MM/yyyy}") %>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="textoGridBold" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="DIA" HeaderText="Día">
                                        <ItemStyle CssClass="textoGridBold" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Trabaja">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkTrabajaMes" runat="server" AutoPostBack="true" OnCheckedChanged="chkTrabajaMes_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Horario">
                                        <ItemTemplate>
                                            <asp:DropDownList
                                                ID="ddlHorarioMes" runat="server" CssClass="GridGralRow" Width="260px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlHorarioMes_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Duración Horas">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtHrMes" runat="server" Width="60px" ReadOnly="true" Enabled="false" CssClass="textoGridBold" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="textoGridBold" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Entrada">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtIniMes" runat="server" Width="60px" ReadOnly="true" Enabled="false" CssClass="textoGridBold" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="textoGridBold" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salida">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFinMes" runat="server" Width="60px" ReadOnly="true" Enabled="false" CssClass="textoGridBold" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="textoGridBold" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridGralHeader" />
                                <RowStyle CssClass="GridGralRow" />
                                <AlternatingRowStyle CssClass="GridGralAltRow" />
                            </asp:GridView>
                            <hr />
                            <table style="width: 100%; margin-bottom: 10px;">
                                <tr>
                                    <td class="textoNormRigth">
                                        <asp:Label ID="lblTotalMes" runat="server" CssClass="form-control"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:Label ID="lblResultadoM" runat="server" ForeColor="Red"></asp:Label>
                    <br />
                    <table style="width: 100%; margin-bottom: 10px;">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnGuardarMes"
                                    runat="server"
                                    Text="Guardar Turno Mensual"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnGuardarDetalleMes_Click"
                                    OnClientClick="mostrarSpinner();"/>
                                <asp:Button ID="btnVolver_3"
                                    runat="server"
                                    Text="Volver"
                                    CssClass="BotonPortalGris"
                                    OnClick="btnVolver_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="TabProf">
                <HeaderTemplate>
                    Personas
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Asociar Personas
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Nombre:</label>
                                <asp:TextBox ID="TNombreUsr" runat="server" MaxLength="80" CssClass="form-control" Width="300px">
                                </asp:TextBox>
                            </div>

                            <div class="campo">
                                <label>RUT:</label>
                                <asp:TextBox ID="TRut" runat="server" CssClass="form-control" Width="160px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Mostrar:</label>
                                <asp:RadioButtonList ID="rbLista"
                                    runat="server"
                                    CssClass="TextoCheck"
                                    RepeatDirection="Horizontal"
                                    Width="280px">
                                    <asp:ListItem Selected="True" Value="1">
                            Disponibles
                                    </asp:ListItem>
                                    <asp:ListItem Value="2">
                            Asignados
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="campo">
                                <label>&nbsp;</label>
                                <asp:Button ID="BtBuscarUser"
                                    runat="server"
                                    Text="Buscar"
                                    OnClick="BtBuscarUser_Click"
                                    OnClientClick="mostrarSpinner();"
                                    CssClass="BotonPortalAzul" />
                            </div>
                            <div class="campo">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnVolver_4"
                                    runat="server"
                                    Text="Volver"
                                    CssClass="BotonPortalGris"
                                    OnClick="btnVolver_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="bloque">
                        <div style="display: flex; gap: 25px; align-items: flex-start;">
                            <div style="flex: 1;">
                                <div class="asignacion-titulo">
                                    Personas Disponibles
                                </div>
                                <asp:GridView ID="gbUserDisp"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    Width="100%"
                                    CssClass="grid-reloj"
                                    GridLines="None"
                                    AllowPaging="True"
                                    PageSize="20"
                                    DataKeyNames="idusuario"
                                    EmptyDataText="No Hay Profesionales Disponibles"
                                    EmptyDataRowStyle-CssClass="textoEmpty"
                                    OnRowDataBound="gbUserDisp_RowDataBound"
                                    OnSelectedIndexChanged="gbUserDisp_SelectedIndexChanged"
                                    OnPageIndexChanging="gbUserDisp_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField
                                            DataField="idusuario"
                                            HeaderText="Id"
                                            ReadOnly="True">
                                            <ItemStyle CssClass="TextoCenter" />
                                        </asp:BoundField>
                                        <asp:BoundField
                                            DataField="RUT_C"
                                            HeaderText="RUT">
                                            <ItemStyle
                                                CssClass="TextoLeft"
                                                Font-Bold="true"
                                                Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField
                                            DataField="NOMBRE"
                                            HeaderText="Nombre">
                                            <ItemStyle CssClass="TextoLeft" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Agregar">
                                            <ItemTemplate>
                                                <asp:ImageButton
                                                    ID="btn_Add"
                                                    runat="server"
                                                    ImageUrl="~/imagenes/check.png"
                                                    OnClick="AddUser"
                                                    OnClientClick="mostrarSpinner();"
                                                    ToolTip="Asociar persona" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle
                                                Width="60px"
                                                HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:CommandField
                                            SelectText="Enroll"
                                            ShowSelectButton="True"
                                            Visible="False" />
                                    </Columns>
                                    <PagerStyle
                                        CssClass="GridPager"
                                        HorizontalAlign="Center" />
                                    <SelectedRowStyle
                                        BackColor="#D1DDF1"
                                        Font-Bold="True"
                                        ForeColor="#333333" />
                                </asp:GridView>
                            </div>
                            <div class="asignacion-separador">
                                <span>→</span>
                            </div>
                            <div style="flex: 1;">
                                <div class="asignacion-titulo">
                                    Personas Asociadas
                                </div>
                                <asp:GridView ID="gbUser"
                                    runat="server"
                                    AutoGenerateColumns="False"
                                    Width="100%"
                                    CssClass="grid-reloj"
                                    GridLines="None"
                                    AllowPaging="True"
                                    PageSize="20"
                                    DataKeyNames="IDTURNUS"
                                    EmptyDataText="No Hay Profesionales Asociados"
                                    EmptyDataRowStyle-CssClass="textoEmpty"
                                    OnRowDataBound="gbUser_RowDataBound"
                                    OnSelectedIndexChanged="gbUser_SelectedIndexChanged"
                                    OnPageIndexChanging="gbUser_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField
                                            DataField="IDTURNUS"
                                            HeaderText="Id"
                                            ReadOnly="True">
                                            <ItemStyle CssClass="TextoCenter" />
                                        </asp:BoundField>
                                        <asp:BoundField
                                            DataField="RUT"
                                            HeaderText="RUT">
                                            <ItemStyle
                                                CssClass="TextoLeft"
                                                Font-Bold="true"
                                                Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField
                                            DataField="NOMBRE"
                                            HeaderText="Nombre">
                                            <ItemStyle CssClass="TextoLeft" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:ImageButton
                                                    ID="btn_Elim"
                                                    runat="server"
                                                    ImageUrl="~/imagenes/close.png"
                                                    OnClick="ElimUser"
                                                    OnClientClick="mostrarSpinner();"
                                                    ToolTip="Eliminar asociación" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" />
                                            <ItemStyle
                                                Width="60px"
                                                HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:CommandField
                                            SelectText="Enroll"
                                            ShowSelectButton="True"
                                            Visible="False" />
                                    </Columns>
                                    <PagerStyle
                                        CssClass="GridPager"
                                        HorizontalAlign="Center" />
                                    <SelectedRowStyle
                                        BackColor="#D1DDF1"
                                        Font-Bold="True"
                                        ForeColor="#333333" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
</body>
</html>
