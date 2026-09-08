<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionMarcaciones.aspx.cs" Inherits="contenido_GestionRRHH_GestionMarcaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestion Marcas</title>
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
        <div class="bloque">
            <div class="titulo-seccion">
                Filtro de Marcas
            </div>
            <div class="filtros-grid">
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
                    <label>Año:</label>
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100px"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"
                        onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </div>
                <div class="campo">
                    <label>Nombre, ID o RUN a Buscar:</label>
                    <asp:TextBox ID="txtBuscarTrab"
                        runat="server"
                        CssClass="form-control"
                        Width="240px"
                        MaxLength="100" />
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
            </div>
        </div>
        <div class="bloque">
            <div class="titulo-seccion">
                Trabajadores con Marcas
            </div>
            <div class="filtros-grid">
                <asp:Label ID="lblTotalTrab" runat="server" CssClass="contador-grid" Text="0 registro(s)">
                </asp:Label>
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
                    EmptyDataText="No Hay Trabajadores con Marcas en el Período"
                    EmptyDataRowStyle-CssClass="textoEmpty"
                    OnSelectedIndexChanged="dgTrabajadores_SelectedIndexChanged"
                    OnPageIndexChanging="dgTrabajadores_PageIndexChanging"
                    AllowSorting="True"
                    OnSorting="dgTrabajadores_Sorting">
                    <Columns>
                        <asp:BoundField DataField="RUT" HeaderText="RUT" SortExpression="RUT" ItemStyle-CssClass="textoGridBoldLeft" />
                        <asp:BoundField DataField="CODIGO_EMP_RELOJ" HeaderText="ID en Reloj" SortExpression="CODIGO_EMP_RELOJ" />
                        <asp:BoundField DataField="NOMBRE" HeaderText="Trabajador" SortExpression="NOMBRE" />
                        <asp:BoundField DataField="CANT_MARCAS" HeaderText="Marcas" SortExpression="CANT_MARCAS" />
                        <asp:BoundField DataField="CANT_DIAS" HeaderText="Días" SortExpression="CANT_DIAS" />
                        <asp:BoundField DataField="CANT_RELOJES" HeaderText="Relojes" SortExpression="CANT_RELOJES" />
                        <asp:BoundField DataField="ESTADO_RRHH" HeaderText="Estado" SortExpression="ESTADO_RRHH" />
                        <asp:TemplateField HeaderText="Revisar">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="btn_Revisar"
                                    runat="server"
                                    ImageUrl="~/imagenes/check.png"
                                    CommandName="Select"
                                    ToolTip="Revisar marcas" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" CssClass="textoGridBoldLeft" />
                            <ItemStyle Width="60px" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>
        </div>
        <div class="bloque" id="divDetalleMarcas" runat="server" visible="false">
            <div class="bloque-titulo">
                Marcas del Trabajador:
            </div>
            <div class="filtros-grid">
                <asp:Label ID="lblTotalMarcas" runat="server" CssClass="contador-grid" Text="0 registro(s)">
                </asp:Label>
            </div>
            <div class="campo">
                <asp:GridView ID="dgMarcas"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    CssClass="grid-reloj"
                    GridLines="None"
                    EmptyDataText="No Hay Marcas para el Período"
                    EmptyDataRowStyle-CssClass="textoEmpty">
                    <Columns>
                        <asp:BoundField DataField="IDMARCACION" HeaderText="Id" ItemStyle-Font-Size="0px" ItemStyle-Width="0px" HeaderStyle-Font-Size="0px" />
                        <asp:BoundField DataField="F_H_MARCA" HeaderText="Fecha Marca" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="F_H_MARCA" HeaderText="Hora Marca" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="TIPO_MARCA_DESC" HeaderText="Tipo" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="CENTRO" HeaderText="Centro Reloj" />
                        <asp:BoundField DataField="CODIGO_EMP_RELOJ" HeaderText="Código" />
                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observación" />
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                </asp:GridView>
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
