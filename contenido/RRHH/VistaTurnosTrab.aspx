<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VistaTurnosTrab.aspx.cs" Inherits="contenido_RRHH_VistaTurnosTrab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Turnos</title>
</head>
<body>
    <form id="form1" runat="server">
        <p class="textoTitLeft">Gestión Turnos</p>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>

                <td style="width: 70px;" class="textoNorm">Mes:
                </td>

                <td style="width: 180px;">
                    <asp:DropDownList ID="ddlMes" runat="server" Width="150px" CssClass="TextBoxGral">
                    </asp:DropDownList>
                </td>

                <td style="width: 70px;" class="textoNorm">Año:
                </td>

                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100px" CssClass="TextBoxGral">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortal"
                        OnClick="btnBuscar_Click" />
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
            EmptyDataText="Sin turnos asignados"
            EmptyDataRowStyle-CssClass="textoEmpty">
            <Columns>
                <asp:BoundField
                    DataField="DIA"
                    HeaderText="Día"
                    ItemStyle-CssClass="textoGrid" />
                <asp:BoundField
                    DataField="TURNO"
                    HeaderText="Turno"
                    ItemStyle-CssClass="textoGrid" />
                <asp:BoundField
                    DataField="HORA_INI"
                    HeaderText="Entrada"
                    DataFormatString="{0:HH:mm}"
                    ItemStyle-CssClass="textoGrid" />
                <asp:BoundField
                    DataField="HORA_FIN"
                    HeaderText="Salida"
                    DataFormatString="{0:HH:mm}"
                    ItemStyle-CssClass="textoGrid" />
                <asp:TemplateField
                    HeaderText="Duración"
                    ItemStyle-CssClass="textoGridBold">
                    <ItemTemplate>
                        <%# Eval("HORA") %> hora(s) <%# Convert.ToInt32(Eval("MINUTO")) > 0 ? " " + Eval("MINUTO") + " min." : "" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField
                    DataField="ESTADO"
                    HeaderText="Estado"
                    ItemStyle-CssClass="textoGrid" />
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
