<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionSincronizacion.aspx.cs" Inherits="contenido_GestionRRHH_GestionSincronizacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestion Sincronizacion</title>
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
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
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="100%" Width="100%" ScrollBars="Auto" ActiveTabIndex="0">
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1">
                <HeaderTemplate>Gestion Sincronización</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Gestión Sincronización
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Centro / Unidad:</label>
                                <asp:DropDownList ID="ddlCentroSincroniza" runat="server" CssClass="form-control"
                                    AutoPostBack="true" Width="350px" onchange="mostrarSpinner();"
                                    OnSelectedIndexChanged="ddlCentroSincroniza_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Reloj:</label>
                                <asp:DropDownList ID="ddlRelojSincroniza" runat="server" CssClass="form-control" Width="250px">
                                </asp:DropDownList>
                            </div>

                            <div class="campo">
                                <label>Fecha inicio:</label>
                                <asp:TextBox ID="txtFechaInicio" runat="server"
                                    CssClass="form-control"
                                    TextMode="Date"
                                    onchange="actualizarFechaFin();">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Fecha fin:</label>
                                <asp:TextBox ID="txtFechaFin" runat="server"
                                    CssClass="form-control"
                                    TextMode="Date">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSincronizar"
                                    runat="server"
                                    Text="Sincronizar"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnSincronizar_Click"
                                    OnClientClick="if (!confirm('¿Desea realizar la sincronización de las marcas del período seleccionado?')) return false; mostrarSpinner();" />
                            </div>
                        </div>
                        <div class="campo">
                            <div class="bloque-titulo">
                                <asp:Label ID="lblSincr" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="botones-form">
                            <asp:Button ID="btnVolver" runat="server" Text="Volver"
                                CssClass="BotonPortalGris" OnClick="btnVolver_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>Administrar Sincronizaciones</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Administrar Sincronizaciones          
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Año:</label>
                                <asp:DropDownList ID="ddlAnio" runat="server" Width="100px"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Mes:</label>
                                <asp:DropDownList ID="ddlMes" runat="server" Width="150px"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Estado:</label>
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control" Width="140px">
                                    <asp:ListItem Value="">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Activas</asp:ListItem>
                                    <asp:ListItem Value="2">Cerradas</asp:ListItem>
                                    <asp:ListItem Value="3">Anuladas</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnBuscarSincr" runat="server"
                                    Text="Buscar"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnBuscarSincr_Click"
                                    OnClientClick="mostrarSpinner();" />
                            </div>
                            <div class="campo">
                                <asp:Button ID="btn_Volver1" runat="server"
                                    Text="Volver"
                                    CssClass="BotonPortalGris"
                                    OnClick="btnVolver_Click" />
                            </div>
                        </div>
                        <div class="campo">
                            <asp:GridView ID="dgSincronizacion" runat="server"
                                AutoGenerateColumns="False"
                                CssClass="grid-reloj"
                                DataKeyNames="IDSINCRONIZA"
                                GridLines="None"
                                EmptyDataText="No existen Sincronizaciones con los filtros aplicados."
                                EmptyDataRowStyle-CssClass="bloque-titulo"
                                OnRowCommand="dgSincronizacion_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="IDSINCRONIZA" HeaderText="ID" />
                                    <asp:BoundField DataField="RELOJ" HeaderText="Reloj" />
                                    <asp:BoundField DataField="F_H_INICIO" HeaderText="Inicio" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                    <asp:BoundField DataField="F_H_FIN" HeaderText="Fin" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                    <asp:BoundField DataField="CANT_LEIDA" HeaderText="Leídas" />
                                    <asp:BoundField DataField="CANT_INSERT" HeaderText="Nuevas" />
                                    <asp:BoundField DataField="ESTADO" HeaderText="Estado" ItemStyle-Width="150px" />
                                    <asp:TemplateField HeaderText="Acciones:">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnVerDetalle" runat="server"
                                                ImageUrl="~/imagenes/eye-icon.png"
                                                CommandName="VER"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                ToolTip="Ver Detalle"
                                                OnClientClick="mostrarSpinner();" />
                                            &nbsp;
                                            <asp:ImageButton ID="btnEliminar" runat="server"
                                                ImageUrl="~/imagenes/delete.png"
                                                CommandName="ELIMINAR"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                ToolTip="Eliminar Sincronización"
                                                Visible='<%# Convert.ToInt32(Eval("ELIMINAR")) == 1 %>'
                                                OnClientClick="return confirm('¿Está seguro de eliminar esta sincronización?');" />
                                            &nbsp;
                                            <%--<asp:ImageButton ID="btnActualizar" runat="server"
                                                ImageUrl="~/imagenes/refresh.png"
                                                CommandName="ACTUALIZAR"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                ToolTip="Actualizar Sincronización"
                                                Visible='<%# Convert.ToInt32(Eval("ACTUALIZAR")) == 1 %>'
                                                OnClientClick="mostrarSpinner();" />
                                            &nbsp;--%>
                                            <asp:ImageButton ID="btnCerrar" runat="server"
                                                ImageUrl="~/imagenes/lock.png"
                                                CommandName="CERRAR"
                                                CommandArgument='<%# Container.DataItemIndex %>'
                                                ToolTip="Cerrar Periodo"
                                                Visible='<%# Convert.ToInt32(Eval("CERRAR")) == 1 %>'
                                                OnClientClick="return confirm('¿Está seguro de cerrar este periodo? Una vez cerrado no podrá actualizar ni eliminar la sincronización.');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="120px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="filtros-grid">
                            <asp:Label ID="lblTotalSinc" runat="server" CssClass="contador-grid" Text="0 registro(s)">
                            </asp:Label>
                        </div>
                    </div>
                    <div class="bloque">
                        <asp:Panel ID="pnlDetalleSincronizacion" runat="server" Visible="false">
                            <div class="bloque">
                                <div class="titulo-seccion">
                                    Marcas de la sincronización                                
                            <asp:Label ID="lblIdSincronizacion" runat="server">
                            </asp:Label>
                                </div>
                                <asp:Panel ID="pnlBusqueda" runat="server" DefaultButton="btnFiltroDet">
                                    <div class="filtros-grid">
                                        <div class="campo">
                                            <label>Código, RUT o Nombre Trabajador:</label>
                                            <asp:TextBox ID="txtBusq" runat="server" CssClass="form-control">
                                            </asp:TextBox>
                                        </div>
                                        <div class="campo">
                                            <label>&nbsp;</label>
                                            <asp:Button ID="btnFiltroDet" runat="server"
                                                Text="Buscar"
                                                CssClass="BotonPortalVerde"
                                                OnClick="btnFiltroDet_Click"
                                                OnClientClick="mostrarSpinner();" />
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="campo">
                                    <asp:HiddenField ID="hdIdSincronizacion" runat="server" />
                                    <asp:GridView ID="dgMarcasSincronizacion" runat="server"
                                        AutoGenerateColumns="False"
                                        GridLines="None"
                                        EmptyDataText="No existen Sincronizaciones con los filtros aplicados."
                                        EmptyDataRowStyle-CssClass="bloque-titulo"
                                        CssClass="grid-reloj"
                                        AllowPaging="True"
                                        PageSize="50"
                                        OnPageIndexChanging="dgMarcasSincronizacion_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="CODIGO_EMP_RELOJ" HeaderText="Cód. Trab. Reloj" />
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre RR.HH." />
                                            <asp:BoundField DataField="RELOJ" HeaderText="Nombre Reloj" />
                                            <asp:BoundField DataField="F_H_MARCA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Font-Bold="true" />
                                            <asp:BoundField DataField="F_H_MARCA" HeaderText="Hora" DataFormatString="{0:HH:mm:ss}" />
                                            <asp:BoundField DataField="TIPO_MARCA" HeaderText="Tipo" ItemStyle-Font-Bold="true" />
                                            <asp:BoundField DataField="F_H_CARGA" HeaderText="Fecha Carga" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                            <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observaciones" />
                                        </Columns>
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                    </asp:GridView>
                                    <asp:Label ID="lblTotalMarcas" runat="server" CssClass="contador-grid" Text="0 registro(s)">&nbsp;</asp:Label>
                                </div>
                                <div class="botones-form">
                                    <asp:Button ID="btnExportarMarcas"
                                        runat="server"
                                        Text="Exportar Excel"
                                        CssClass="BotonPortalVerde"
                                        OnClick="btnExportarMarcas_Click" />
                                </div>
                        </asp:Panel>
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
<script type="text/javascript">
    function actualizarFechaFin() {
        var inicio = document.getElementById('<%= txtFechaInicio.ClientID %>').value;
        if (!inicio) return;

        var partes = inicio.split('-');
        var anio = parseInt(partes[0]);
        var mes = parseInt(partes[1]);
        var dia = parseInt(partes[2]);

        var ultimoDia = new Date(anio, mes, 0).getDate();

        document.getElementById('<%= txtFechaFin.ClientID %>').value =
            anio + '-' + String(mes).padStart(2, '0') + '-' + String(ultimoDia).padStart(2, '0');
    }
</script>
</html>
