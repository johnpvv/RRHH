<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaHorarios.aspx.cs" Inherits="contenido_Administracion_ListaHorarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Lista Horarios</title>
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
                Gestión de Horarios
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Descripción:</label>
                    <asp:TextBox ID="txtDescr" runat="server"
                        CssClass="form-control"
                        Width="250px" />
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
                    DataKeyNames="IDHORA">
                    <Columns>
                        <asp:BoundField DataField="IDHORA" HeaderText="Id" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" ItemStyle-Width="350px" />
                        <asp:BoundField DataField="DURACION" HeaderText="Duración" ItemStyle-Font-Bold="true" />
                        <asp:BoundField DataField="HORA_INI" DataFormatString="{0:HH:mm}" HeaderText="Hora Inicio" />
                        <asp:BoundField DataField="HORA_FIN" DataFormatString="{0:HH:mm}" HeaderText="Hora Fin" />
                        <asp:BoundField DataField="Estado" HeaderText="Estado" />
                        <asp:BoundField DataField="F_H_CREACION" HeaderText="Fecha Creación" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Editar" HeaderStyle-Width="50px">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar"
                                    runat="server"
                                    ImageUrl="~/imagenes/edit.png"
                                    ToolTip="Editar Horario"
                                    CommandName="Editar"
                                    CommandArgument='<%# Eval("IDHORA") %>'
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
