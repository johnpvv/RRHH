<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionEquivalenciaReloj.aspx.cs" Inherits="contenido_GestionRRHH_GestionEquivalenciaReloj" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Equivalencia Relojes</title>
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
                <HeaderTemplate>Registro Equivalencias</HeaderTemplate>
                <ContentTemplate>
                    <div class="panel-reloj-rrhh">
                        <div class="titulo-seccion">
                            Relacionar trabajador del reloj con RRHH
                        </div>
                        <div class="fila-superior">
                            <div class="bloque">
                                <div class="bloque-titulo">
                                    Conexión y trabajadores del Reloj
                                </div>
                                <div class="form-linea">
                                    <div class="campo">
                                        <label>Centro / Unidad:</label>
                                        <asp:DropDownList ID="ddlCentro"
                                            runat="server"
                                            CssClass="form-control"
                                            AutoPostBack="true"
                                            onchange="mostrarSpinner();"
                                            OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="campo">
                                        <label>Reloj:</label>
                                        <asp:DropDownList ID="ddlReloj"
                                            runat="server"
                                            CssClass="form-control"
                                            AutoPostBack="true"
                                            onchange="mostrarSpinner();"
                                            OnSelectedIndexChanged="ddlReloj_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-linea">
                                    <div class="campo">
                                        <label>IP:</label>
                                        <asp:TextBox ID="txtIP" runat="server" CssClass="form-control" Width="150px" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo">
                                        <label>Puerto:</label>
                                        <asp:TextBox ID="txtPuerto" runat="server" CssClass="form-control" Width="70px" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo">
                                        <label>ID:</label>
                                        <asp:TextBox ID="txtIdReloj" runat="server" CssClass="form-control" Width="50px" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="filtros-grid">
                                    <div class="campo">
                                        <label>Código:</label>
                                        <asp:TextBox ID="txtFiltroCodigoReloj" runat="server" CssClass="form-control" Width="100px">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtFiltroNombreReloj" runat="server" CssClass="form-control" Width="220px">
                                        </asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnCargarReloj"
                                        runat="server"
                                        Text="Buscar en reloj"
                                        CssClass="BotonPortalAzul"
                                        OnClick="btnCargarReloj_Click"
                                        OnClientClick="mostrarSpinner();" />
                                </div>
                            </div>
                            <div class="bloque">
                                <div class="bloque-titulo">
                                    Búsqueda en el sistema RR.HH.
                                </div>
                                <div class="form-linea">
                                    <div class="campo">
                                        <label>RUT:</label>
                                        <asp:TextBox ID="txtRut" runat="server" CssClass="form-control" Width="100px">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="220px">
                                        </asp:TextBox>
                                    </div>
                                    <asp:Button ID="btnBuscarRRHH"
                                        runat="server"
                                        Text="Buscar RRHH"
                                        CssClass="BotonPortalVerde"
                                        OnClick="btnBuscarRRHH_Click"
                                        OnClientClick="mostrarSpinner();" />
                                </div>
                            </div>
                        </div>

                        <div class="fila-grids">
                            <div class="bloque-grid">
                                <div class="bloque-titulo">
                                    1. Trabajadores registrados en el Reloj:
                                </div>
                                <div class="filtros-grid">
                                    <asp:Label ID="lblTotalReloj" runat="server" CssClass="contador-grid" Text="0 registro(s)">
                                    </asp:Label>
                                </div>
                                <div class="contenedor-grid">
                                    <asp:GridView ID="dgReloj"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        CssClass="grid-reloj"
                                        GridLines="None"
                                        OnSelectedIndexChanged="dgReloj_SelectedIndexChanged"
                                        OnRowCommand="dgReloj_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="IDUSRPEND" HeaderText="ID Trabajador">
                                                <HeaderStyle Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IDUSERRELOJ" HeaderText="Código">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                <HeaderStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Selec.">
                                                <ItemTemplate>
                                                    <asp:ImageButton
                                                        ID="btnSeleccionar"
                                                        runat="server"
                                                        ImageUrl="~/imagenes/check.png"
                                                        CommandName="SeleccionarReloj"
                                                        CommandArgument='<%# Eval("IDUSRPEND") %>'
                                                        ToolTip="Seleccionar trabajador"
                                                        Width="24px"
                                                        Height="24px"
                                                        OnClientClick="mostrarSpinner();" />
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdIdUsrReloj" runat="server" />
                                    <asp:HiddenField ID="hdIdReloj" runat="server" />

                                </div>
                            </div>
                            <div class="bloque-grid">
                                <div class="bloque-titulo">
                                    2. Resultados del sistema RR.HH.:
                                </div>
                                <div class="filtros-grid">
                                    <asp:Label ID="lblTotalRRHH" runat="server" CssClass="contador-grid" Text="0 resultado(s)">
                                    </asp:Label>
                                </div>
                                <div class="contenedor-grid">
                                    <asp:GridView ID="dgRRHH"
                                        runat="server"
                                        AutoGenerateColumns="False"
                                        CssClass="grid-reloj"
                                        GridLines="None"
                                        OnSelectedIndexChanged="dgRRHH_SelectedIndexChanged"
                                        OnRowCommand="dgRRHH_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="IDUSUARIO" HeaderText="ID Usuario">
                                                <HeaderStyle Width="40px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RUT_C" HeaderText="RUT">
                                                <HeaderStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE" HeaderText="Nombre">
                                                <HeaderStyle Width="200px" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Selec.">
                                                <ItemTemplate>
                                                    <asp:ImageButton
                                                        ID="btnSeleccionar"
                                                        runat="server"
                                                        ImageUrl="~/imagenes/check.png"
                                                        CommandName="SeleccionarUsuario"
                                                        CommandArgument='<%# Eval("IDUSUARIO") %>'
                                                        ToolTip="Seleccionar usuario"
                                                        Width="24px"
                                                        Height="24px"
                                                        OnClientClick="mostrarSpinner();" />
                                                </ItemTemplate>
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdIdUsuario" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="fila-seleccionados">
                            <div class="bloque-seleccion">
                                <div class="bloque-titulo">
                                    Registro seleccionado del Reloj:
                                </div>
                                <div class="detalle-seleccion">
                                    <div class="campo">
                                        <label>ID Trab.:</label>
                                        <asp:TextBox ID="txtIdTrabSeleccionado" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo">
                                        <label>Código:</label>
                                        <asp:TextBox ID="txtCodigoSeleccionado" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo campo-largo">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreTrabSeleccionado" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="bloque-seleccion">
                                <div class="bloque-titulo">
                                    Registro seleccionado de RR.HH.:
                                </div>
                                <div class="detalle-seleccion">
                                    <div class="campo">
                                        <label>ID Usuario:</label>
                                        <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo">
                                        <label>RUT:</label>
                                        <asp:TextBox ID="txtRutSeleccionado" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                    <div class="campo campo-largo">
                                        <label>Nombre:</label>
                                        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" ReadOnly="true">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="estado-equivalencia">
                            <asp:Label ID="lblEstadoEquivalencia" runat="server" CssClass="estado-texto"
                                Text="Reloj: sin selección → RRHH: sin selección">
                            </asp:Label>
                            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje-form">
                            </asp:Label>
                        </div>

                        <div class="botones-form">
                            <asp:Button ID="btnRegistrar"
                                runat="server"
                                Text="Registrar equivalencia"
                                CssClass="BotonPortalAmarillo"
                                OnClick="btnRegistrar_Click"
                                OnClientClick="if (!confirm('¿Desea registrar la Equivalencia seleccionada?')) return false; mostrarSpinner();" />
                            <asp:Button ID="btnLimpiar"
                                runat="server"
                                Text="Limpiar"
                                CssClass="BotonPortalVerde"
                                OnClick="btnLimpiar_Click"
                                OnClientClick="mostrarSpinner();"/>
                            <asp:Button ID="btnVolver"
                                runat="server"
                                Text="Volver"
                                CssClass="BotonPortalGris"
                                OnClick="btnVolver_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>Administrar Equivalencias</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Equivalencias Registradas           
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Centro / Unidad:</label>
                                <asp:DropDownList ID="ddlCentroAdmin"
                                    runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    Width="300px"
                                    onchange="mostrarSpinner();"
                                    OnSelectedIndexChanged="ddlCentroAdmin_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Reloj:</label>
                                <asp:DropDownList ID="ddlRelojAdmin"
                                    runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    Width="200px"
                                    onchange="mostrarSpinner();"
                                    OnSelectedIndexChanged="ddlRelojAdmin_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="campo">
                                <label>Cód. Trab. Reloj:</label>
                                <asp:TextBox ID="txtFiltroCodigo" runat="server" CssClass="form-control" Width="100px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Nombre:</label>
                                <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" Width="200px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>RUT:</label>
                                <asp:TextBox ID="txtFiltroRut" runat="server" CssClass="form-control" Width="120px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <asp:Button ID="btnBuscarEquivalencias"
                                    runat="server"
                                    Text="Buscar"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnBuscarEquivalencias_Click"
                                    OnClientClick="mostrarSpinner();" />
                            </div>
                        </div>
                        <br />
                        <asp:GridView ID="dgEquivalencias"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="grid-reloj"
                            GridLines="None"
                            OnRowCommand="dgEquivalencias_RowCommand"
                            EmptyDataText="No existen equivalencias con los filtros aplicados."
                            EmptyDataRowStyle-CssClass="bloque-titulo">
                            <Columns>
                                <asp:BoundField DataField="IDUSRELOJ" HeaderText="Cód. Trab. Reloj" />
                                <asp:BoundField DataField="NOMBRE_TRAB_RELOJ" HeaderText="Nombre Trabajador Reloj" />
                                <asp:BoundField DataField="NOMBRE_RELOJ" HeaderText="Nombre Reloj" />
                                <asp:TemplateField HeaderText="(Hacia)">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnHacia"
                                            runat="server"
                                            ImageUrl="~/imagenes/avanzar.png"
                                            ToolTip="Hacia" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="RUT_C" HeaderText="RUT" />
                                <asp:BoundField DataField="NOMBRE_RRHH" HeaderText="Trabajador RRHH" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                                <asp:TemplateField HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminarEquivalencia"
                                            runat="server"
                                            ImageUrl="~/imagenes/close.png"
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Eval("IDUSRRELOJ") %>'
                                            ToolTip="Desactivar equivalencia"
                                            OnClientClick="return confirm('¿Está seguro de desactivar esta equivalencia?');" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
