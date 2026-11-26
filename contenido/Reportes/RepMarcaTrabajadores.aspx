<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepMarcaTrabajadores.aspx.cs" Inherits="contenido_Reportes_RepMarcaTrabajadores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reportes Marcas</title>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" />
    <script type="text/javascript">
        function mostrarSpinner() {
            var spinner = document.getElementById('spinnerCarga');
            if (spinner) {
                spinner.style.display = 'flex';
            }
        }

        function guardarScroll() {
            sessionStorage.setItem('scrollMarcas', window.scrollY);
        }
        window.addEventListener('load', function () {
            var p = sessionStorage.getItem('scrollMarcas');
            if (p) {
                window.scrollTo(0, parseInt(p));
                sessionStorage.removeItem('scrollMarcas');
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <div class="bloque">
            <div class="titulo-seccion">
                Filtro de Marcas
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Año:</label>
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100px"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"
                        onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </div>
                <div class="campo">
                    <label>Mes:</label>
                    <asp:DropDownList ID="ddlMes" runat="server" Width="150px"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlMes_SelectedIndexChanged"
                        onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </div>
                <div class="campo">
                    <label>Centro / Unidad:</label>
                    <asp:DropDownList ID="ddlCentro" runat="server" CssClass="form-control"
                        AutoPostBack="true" Width="250px"
                        OnSelectedIndexChanged="ddlCentro_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </div>
                <div class="campo">
                    <label>Nombre, ID o RUN a Buscar:</label>
                    <asp:TextBox ID="txtBuscarTrab"
                        runat="server"
                        CssClass="form-control"
                        Width="200px"
                        MaxLength="100"
                        TextMode="Search" />
                </div>
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:Button ID="btnBuscarMarcas"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btnBuscarMarcas_Click"
                        OnClientClick="mostrarSpinner();" />
                </div>
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:Button ID="btnVolver"
                        runat="server"
                        Text="Volver"
                        CausesValidation="false"
                        CssClass="BotonPortalGris"
                        OnClick="btnVolver_Click" />
                </div>
            </div>
        </div>
        <div class="bloque">
            <div class="titulo-seccion">
                Trabajadores Encontrados:
                (<asp:Label ID="lblTotalTrab" runat="server" Text="0" />)
            </div>
            <div class="campo">
                <asp:GridView ID="dgTrabajadores"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    CssClass="grid-reloj"
                    GridLines="None"
                    AllowPaging="True"
                    PageSize="20"
                    DataKeyNames="ID_SELECCION"
                    EmptyDataText="No se han encontrado registros con los filtros aplicados"
                    EmptyDataRowStyle-CssClass="textoEmpty"
                    OnSelectedIndexChanged="dgTrabajadores_SelectedIndexChanged"
                    OnPageIndexChanging="dgTrabajadores_PageIndexChanging"
                    AllowSorting="True"
                    OnSorting="dgTrabajadores_Sorting">
                    <Columns>
                        <asp:BoundField DataField="RUT_C" HeaderText="R.U.N." SortExpression="RUT" ItemStyle-CssClass="textoGridBoldLeft" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="CODIGO_EMP_RELOJ" HeaderText="ID en Reloj" SortExpression="CODIGO_EMP_RELOJ" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Trabajador" SortExpression="NOMBRE" />
                        <asp:BoundField DataField="CANT_MARCAS" HeaderText="Marcas" SortExpression="CANT_MARCAS" />
                        <asp:BoundField DataField="CANT_DIAS" HeaderText="Días" SortExpression="CANT_DIAS" />
                        <asp:BoundField DataField="CANT_RELOJES" HeaderText="Relojes" SortExpression="CANT_RELOJES" />
                        <asp:BoundField DataField="CANT_PROBLEMAS" HeaderText="Posibles Problemas" SortExpression="CANT_PROBLEMAS" ItemStyle-ForeColor="Blue" ItemStyle-Font-Bold="true" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="CANT_MARCAS_OTRO_CENTRO" HeaderText="Cant. Marcas en Otros Centros" SortExpression="CANT_MARCAS_OTRO_CENTRO" ItemStyle-Font-Bold="true" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="ANOMALIA_CENTRO" HeaderText="Marcas en diferentes Centros" SortExpression="ANOMALIA_CENTRO" ItemStyle-Font-Bold="true" HeaderStyle-Width="100px" />
                        <asp:BoundField DataField="ESTADO_RRHH" HeaderText="Estado" SortExpression="ESTADO_RRHH" />
                        <asp:TemplateField HeaderText="Revisar">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="btn_Revisar"
                                    runat="server"
                                    ImageUrl="~/imagenes/check.png"
                                    CommandName="Select"
                                    ToolTip="Revisar marcas"
                                    OnClientClick="mostrarSpinner();" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" CssClass="textoGridBoldLeft" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>
            <div class="botones-form">
                <asp:Button ID="btnExportarTrab"
                    runat="server"
                    Text="Exportar Excel"
                    CssClass="BotonPortalVerde"
                    OnClick="btnExportarTrab_Click" />
                <asp:Button ID="btnExportarPDFLista"
                    runat="server"
                    Text="Generar PDF Detalle Cálculo"
                    CssClass="BotonPortalAmarillo"
                    OnClick="btnExportarPDFLista_Click" />
            </div>
        </div>
        <br />
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
</body>
</html>
