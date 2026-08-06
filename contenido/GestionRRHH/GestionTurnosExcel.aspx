<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionTurnosExcel.aspx.cs" Inherits="contenido_GestionRRHH_GestionTurnosExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestion Turnos</title>
    <script type="text/javascript" src="../../js/common.js"></script>
    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" />

    <script type="text/javascript">

        function ConfirmarGuardar() {
            if (confirm('¿Desea guardar los Cambios?')) {
                document.getElementById('divCargando').style.display = 'block';
                return true;
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <p class="textoTitLeft">Gestión Turnos Excel</p>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>
                <td class="textoNormRigth">Elegir Archivo a Cargar:
                </td>
                <td>
                    <asp:FileUpload ID="fuExcel" runat="server" CssClass="form-control" />
                </td>
            </tr>
            <tr>
                <td class="textoNormRigth">Descargar Plantilla:
                </td>
                <td></td>
            </tr>
        </table>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>
                <td align="center">
                    <asp:Button
                        ID="btnCargar"
                        runat="server"
                        Text="Procesar Excel"
                        CssClass="BotonPortalVerde"
                        OnClick="btnCargar_Click" />
                    &nbsp;
                    <asp:Button ID="btnVolver"
                        runat="server"
                        Text="Volver"
                        CssClass="BotonPortalGris"
                        OnClick="btnVolver_Click" />
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblResultado" runat="server" CssClass="textoNormLeft"></asp:Label>
        <asp:GridView ID="dgData"
            runat="server"
            AutoGenerateColumns="False"
            Width="100%"
            CssClass="GridGral"
            GridLines="None"
            CellPadding="6"
            EmptyDataText="Sin Resultados"
            EmptyDataRowStyle-CssClass="textoEmpty"
            DataKeyNames="RUT">
            <Columns>
                <asp:BoundField DataField="RUT"
                    HeaderText="RUT" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="NOMBRES"
                    HeaderText="NOMBRES" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="CODIGO"
                    HeaderText="Código Turno" ItemStyle-CssClass="textoGridBold" />
                <asp:BoundField DataField="ESTADO"
                    HeaderText="Estado" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="F_H_CREACION"
                    HeaderText="Fecha Creación"
                    DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-CssClass="textoGrid" />
            </Columns>
            <HeaderStyle CssClass="GridGralHeader" />
            <RowStyle CssClass="GridGralRow" />
            <AlternatingRowStyle CssClass="GridGralAltRow" />
        </asp:GridView>
    </form>
</body>
</html>
