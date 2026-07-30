<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaHorarios.aspx.cs" Inherits="contenido_Administracion_ListaHorarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Horarios</title>
</head>
<body>
    <form id="form1" runat="server">
        <p class="textoTitLeft">Gestión Horarios</p>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>
                <td style="width: 70px;" class="textoNorm">Descripción:
                </td>
                <td style="width: 120px;">
                    <asp:TextBox ID="txtDescr" runat="server" Width="200px" CssClass="TextBoxGral" />
                </td>
                <td>
                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortal"
                        OnClick="btnBuscar_Click" />
                    &nbsp;
                    <asp:Button ID="btnCrear"
                        runat="server"
                        Text="Agregar Nuevo"
                        CssClass="BotonPortalAmarillo"
                        OnClick="btnCrear_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView ID="dgData"
            runat="server"
            AutoGenerateColumns="False"
            Width="100%"
            CssClass="GridGral"
            GridLines="None"
            CellPadding="6"
            EmptyDataText="Sin Resultados"
            EmptyDataRowStyle-CssClass="textoEmpty"
            OnRowCommand="dgData_RowCommand"
            DataKeyNames="IDHORA">
            <Columns>
                <asp:BoundField DataField="IDHORA"
                    HeaderText="Id" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="DESCRIPCION"
                    HeaderText="Descripción" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="DURACION"
                    HeaderText="Duración" ItemStyle-CssClass="textoGridBold" />
                <asp:BoundField DataField="HORA_INI" DataFormatString="{0:HH:mm}"
                    HeaderText="Hora Inicio" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="HORA_FIN" DataFormatString="{0:HH:mm}"
                    HeaderText="Hora Fin" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="Estado"
                    HeaderText="Estado" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="F_H_CREACION"
                    HeaderText="Fecha Creación"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-CssClass="textoGrid" />
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEditar"
                            runat="server"
                            ImageUrl="~/imagenes/edit.png"
                            ToolTip="Editar Turno"
                            CommandName="Editar"
                            CommandArgument='<%# Eval("IDHORA") %>'
                            Width="22px"
                            Height="22px" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                    <asp:Button ID="btnExportar"
                        runat="server"
                        Text="Exportar Excel"
                        CssClass="BotonPortalVerde"
                        OnClick="btnExportar_Click" />
                    &nbsp;
                    <asp:Button ID="btnVolver"
                        runat="server"
                        Text="Volver"
                        CssClass="BotonPortalGris"
                        OnClick="btnVolver_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
