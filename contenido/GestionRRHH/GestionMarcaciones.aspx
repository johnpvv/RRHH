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
    <script>
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
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:Button ID="btnVolver"
                        runat="server"
                        Text="Volver"
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
                    EmptyDataText="No Hay Trabajadores con Marcas en el Período"
                    EmptyDataRowStyle-CssClass="textoEmpty"
                    OnSelectedIndexChanged="dgTrabajadores_SelectedIndexChanged"
                    OnPageIndexChanging="dgTrabajadores_PageIndexChanging"
                    AllowSorting="True"
                    OnSorting="dgTrabajadores_Sorting">
                    <Columns>
                        <asp:BoundField DataField="RUT_C" HeaderText="R.U.N." SortExpression="RUT" ItemStyle-CssClass="textoGridBoldLeft" />
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
            <div class="campo">
                <div class="bloque-titulo">
                    <label>&nbsp;</label>
                </div>
            </div>
        </div>
        <div class="bloque" id="divDetalleMarcas" runat="server" visible="false">
            <div class="bloque-titulo">
                Marcas del Trabajador:
                <label>&nbsp;</label>
                <asp:Label ID="lblTituloMarcas" runat="server" />
            </div>
            <div class="campo">
                <asp:GridView ID="dgMarcas"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    CssClass="grid-reloj"
                    GridLines="None"
                    DataKeyNames="IDMARCACION,CODIGO_EMP_RELOJ,IDRELOJ"
                    EmptyDataText="No Hay Marcas para el Período"
                    EmptyDataRowStyle-CssClass="textoEmpty"
                    OnRowCommand="dgMarcas_RowCommand"
                    OnRowDataBound="dgMarcas_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="IDMARCACION" HeaderText="Id" ItemStyle-Font-Size="0px" ItemStyle-Width="0px" HeaderStyle-Font-Size="0px" />
                        <asp:BoundField DataField="IDRELOJ" HeaderText="Idreloj" ItemStyle-Font-Size="0px" ItemStyle-Width="0px" HeaderStyle-Font-Size="0px" />
                        <asp:BoundField DataField="F_H_MARCA" HeaderText="Fecha Marca" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="F_H_MARCA" HeaderText="Hora Marca" DataFormatString="{0:HH:mm:ss}" />
                        <asp:BoundField DataField="TIPO_MARCA_DESC" HeaderText="Tipo" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="CENTRO" HeaderText="Centro Reloj" HeaderStyle-Width="200px" />
                        <asp:BoundField DataField="CODIGO_EMP_RELOJ" HeaderText="Código Trab." />
                        <asp:BoundField DataField="OBSERVACIONES" HeaderText="Observación" HeaderStyle-Width="200px" />
                        <asp:TemplateField HeaderText="Estado" HeaderStyle-Width="150px">
                            <ItemTemplate>
                                <span class='estado-marca <%# Eval("ESTADO").ToString().Replace(" ","-") %>'>
                                    <%# Eval("ESTADO") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accion" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server" ImageUrl="~/imagenes/edit.png"
                                    CommandName="EditarMarca"
                                    CommandArgument='<%# Eval("IDMARCACION") %>'
                                    ToolTip="Editar"
                                    OnClientClick="guardarScroll();" />
                                <asp:ImageButton ID="btnEliminar" runat="server" ImageUrl="~/imagenes/delete.png"
                                    CommandName="EliminarMarca"
                                    CommandArgument='<%# Eval("IDMARCACION") %>'
                                    ToolTip="Eliminar"
                                    Visible='<%# Eval("ESTADO").ToString().Contains("REPETIDA") %>'
                                    OnClientClick="guardarScroll();return confirm('¿Está seguro de eliminar esta marca?');" />
                                <asp:ImageButton ID="btnInsertar" runat="server" ImageUrl="~/imagenes/add.png"
                                    CommandName="InsertarMarca"
                                    CommandArgument='<%# Eval("FECHA") + "|" + Eval("ESTADO")  %>' ToolTip="Agregar marca"
                                    Visible='<%# Eval("ESTADO").ToString().Contains("FALTANTE") %>'
                                    OnClientClick="guardarScroll();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>
            <div class="botones-form">
                <asp:Button ID="btnNuevaMarca" runat="server"
                    Text="+ Agregar Marcación"
                    CssClass="BotonPortalAmarillo"
                    OnClick="btnNuevaMarca_Click"
                    Width="200px"
                    OnClientClick="guardarScroll();" />
            </div>
            <div class="filtros-grid">
                <asp:Label ID="lblTotalMarcas" runat="server" CssClass="contador-grid" Text="0 registro(s)">
                </asp:Label>
            </div>
            <div class="campo">
                <asp:Label ID="lblResumenMarcas" runat="server" CssClass="resumen-marcas" />
            </div>
        </div>
        <asp:Panel ID="pnlEditarMarca" runat="server" CssClass="modal-marca" Style="display: none;">
            <div class="modal-marca-contenido">
                <div class="modal-marca-titulo">
                    <asp:Label ID="lblTituloMarca" runat="server" />
                </div>
                <div class="filtros-grid">
                    <div class="campo">
                        <label>Fecha:</label><asp:TextBox ID="txtFechaMarca" runat="server" CssClass="form-control" Width="100px" Enabled="false" />
                    </div>
                    <div class="campo">
                        <label>Hora:</label><asp:TextBox ID="txtHoraMarca" runat="server" CssClass="form-control" Width="100px" MaxLength="8" />
                    </div>
                    <div class="campo">
                        <label>Tipo:</label><asp:DropDownList ID="ddlTipoMarca" runat="server" CssClass="form-control" Width="120px">
                            <asp:ListItem Value="0">ENTRADA</asp:ListItem>
                            <asp:ListItem Value="1">SALIDA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="campo">
                        <label>Centro:</label>
                        <%--<asp:Label ID="lblCentroMarca" runat="server" CssClass="textoNormLeft" />--%>
                        <asp:DropDownList ID="ddlCentroMarca" runat="server" CssClass="form-control" />
                    </div>
                    <div class="campo">
                        <label>Observación:</label><asp:TextBox ID="txtObsMarca" runat="server" CssClass="form-control" MaxLength="800" Width="200px" TextMode="MultiLine" />
                    </div>
                </div>
                <asp:HiddenField ID="hdIdMarcacion" runat="server" />
                <asp:HiddenField ID="hdIdUserReloj" runat="server" />
                <asp:HiddenField ID="hdIdReloj" runat="server" />
                <br />
                <div class="botones-form">
                    <asp:Button ID="btnGuardarMarca" runat="server"
                        Text="Guardar"
                        CssClass="BotonPortalAzul"
                        OnClick="btnGuardarMarca_Click"
                        OnClientClick="guardarScroll();" />
                    <asp:Button ID="btnCancelarMarca" runat="server"
                        Text="Cancelar"
                        CssClass="BotonPortalGris"
                        OnClick="btnCancelarMarca_Click"
                        OnClientClick="guardarScroll();" />
                </div>
            </div>
        </asp:Panel>
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
</body>
</html>
