<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaTurnos.aspx.cs" Inherits="contenido_Administracion_ListaTurnos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Lista Turnos</title>
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
                Gestión de Turnos
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label for="<%= txtCod.ClientID %>">Código:</label>
                    <asp:TextBox ID="txtCod" runat="server"
                        CssClass="form-control" Width="140px" />
                </div>
                <div class="campo">
                    <label for="<%= txtDescr.ClientID %>">Descripción:</label>
                    <asp:TextBox ID="txtDescr" runat="server"
                        CssClass="form-control" Width="250px" />
                </div>
                <div class="campo">
                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btnBuscar_Click"
                        OnClientClick="mostrarSpinner();"/>
                </div>
                <div class="campo">
                    <asp:Button ID="btnCrear"
                        runat="server"
                        Text="Agregar Nuevo"
                        CssClass="BotonPortalAmarillo"
                        OnClick="btnCrear_Click" />
                </div>
            </div>
            <div class="campo">
                <asp:GridView ID="dgData"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    CssClass="grid-reloj"
                    GridLines="None"
                    CellPadding="6"
                    EmptyDataText="Sin Resultados"
                    EmptyDataRowStyle-CssClass="textoEmpty"
                    OnRowCommand="dgData_RowCommand"
                    DataKeyNames="IDTURNOS">
                    <Columns>
                        <asp:BoundField DataField="IDTURNOS" HeaderText="ID" />
                        <asp:BoundField DataField="CODIGO" HeaderText="Código" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción del Turno" />
                        <asp:BoundField DataField="TIPO_TURNO_P" HeaderText="Tipo de Turno" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:BoundField DataField="F_H_CREACION" HeaderText="Fecha Creación" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Editar" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar"
                                    runat="server"
                                    ImageUrl="~/imagenes/edit.png"
                                    ToolTip="Editar Turno"
                                    CommandName="Editar"
                                    CommandArgument='<%# Eval("IDTURNOS") %>'
                                    Width="22px"
                                    Height="22px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="GridGralHeader" />
                    <RowStyle CssClass="GridGralRow" />
                    <AlternatingRowStyle CssClass="GridGralAltRow" />
                </asp:GridView>
            </div>
            <div class="botones-form">
                <asp:Button ID="btnExportar"
                    runat="server"
                    Text="Exportar Excel"
                    CssClass="BotonPortalVerde"
                    OnClick="btnExportar_Click" />
                <asp:Button ID="btnVolver"
                    runat="server"
                    Text="Volver"
                    CssClass="BotonPortalGris"
                    OnClick="btnVolver_Click" />
            </div>
        </div>
    </form>
    <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
        <div class="spinner"></div>
        <div class="spinner-text">
            Cargando Datos, Favor Espere...
        </div>
    </div>
</body>
</html>
