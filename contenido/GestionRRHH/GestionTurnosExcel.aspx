<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionTurnosExcel.aspx.cs" Inherits="contenido_GestionRRHH_GestionTurnosExcel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestion Turnos</title>
    <script type="text/javascript" src="../../js/common.js"></script>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" />

    <script type="text/javascript">

        function ConfirmarGuardar() {
            if (confirm('¿Desea Procesar el Archivo?')) {
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
                <td class="textoNormRigth">&nbsp;</td>
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
                        OnClick="btnCargar_Click"
                        OnClientClick="return ConfirmarGuardar();" />
                    &nbsp;
                    <asp:Button
                        ID="btnPlantilla"
                        runat="server"
                        Text="Descargar Plantilla Ejemplo"
                        CssClass="BotonPortalAmarillo"
                        OnClick="btnPlantilla_Click" />
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
        <asp:Label ID="lbMensaje" runat="server" CssClass="textoNormLeft"></asp:Label>
        <asp:GridView ID="dgData"
            runat="server"
            AutoGenerateColumns="False"
            Width="100%"
            CssClass="GridGral"
            GridLines="None"
            CellPadding="6"
            EmptyDataText="No se ha Cargado Ningún dato al Sistema"
            EmptyDataRowStyle-CssClass="textoEmpty"
            DataKeyNames="RUT">
            <Columns>
                <asp:BoundField DataField="RUT"
                    HeaderText="RUT" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="NOMBRES"
                    HeaderText="NOMBRES" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="CODIGO_TURNO"
                    HeaderText="Código Turno" ItemStyle-CssClass="textoGridBold" />
            </Columns>
            <HeaderStyle CssClass="GridGralHeader" />
            <RowStyle CssClass="GridGralRow" />
            <AlternatingRowStyle CssClass="GridGralAltRow" />
        </asp:GridView>
        <br />
        <div id="divCargando" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.3); z-index: 9999;">
            <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%); width: 300px; height: 180px; background: white; border-radius: 10px; text-align: center; padding-top: 30px; box-shadow: 0 0 15px #666;">
                <img src="../../imagenes/ajax-loader.gif" style="width: 120px; height: 120px;" alt="Cargando..." />
                <br />
                <br />
                <span style="font-size: 16px; font-weight: bold;">Procesando Archivo...
                </span>
            </div>
        </div>
    </form>
</body>
</html>
