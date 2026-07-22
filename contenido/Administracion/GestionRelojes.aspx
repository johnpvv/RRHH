<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionRelojes.aspx.cs" Inherits="contenido_Administracion_GestionRelojes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <title>Relojes</title>
</head>
<body>
    <form id="form1" runat="server">
        <p class="textoTitLeft">Gestión Relojes</p>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>
                <td style="width: 70px;" class="textoNorm">IP:
                </td>
                <td style="width: 180px;">
                    <asp:TextBox ID="txtIp" runat="server" Width="150px" CssClass="TextBoxGral" />
                </td>
                <td style="width: 70px;" class="textoNorm">Serie:
                </td>
                <td style="width: 120px;">
                    <asp:TextBox ID="txtSerie" runat="server" Width="100px" CssClass="TextBoxGral" />
                </td>
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
            EmptyDataRowStyle-CssClass="textoEmpty">
            <Columns>
                <asp:BoundField DataField="IDRELOJ"
                    HeaderText="Id" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="DESCRIPCION"
                    HeaderText="Descripción" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="IP"
                    HeaderText="Dir. IP" ItemStyle-CssClass="textoGridBold" />
                <asp:BoundField DataField="SERIE"
                    HeaderText="Serie" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="Estado"
                    HeaderText="Estado" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="CENTRO"
                    HeaderText="CENTRO" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="F_H_CREACION"
                    HeaderText="Fecha Creación"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-CssClass="textoGrid" />
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
