<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VistaMarcacionesTrab.aspx.cs" Inherits="contenido_RRHH_VistaMarcacionesTrab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
    <script type="text/javascript">
        function mostrarSpinner() {
            var spinner = document.getElementById('spinnerCarga');
            if (spinner) {
                spinner.style.display = 'flex';
            }
        }
    </script>
    <title>Marcaciones</title>
</head>
<body>
    <form id="form1" runat="server">
        <p class="textoTitLeft">Gestión Marcaciones</p>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>
                <td style="width: 70px;" class="textoNorm">Año:</td>                    
                <td style="width: 100px;">
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100px" CssClass="form-control" 
                        AutopostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px;" class="textoNorm">Mes:</td>                
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlMes" runat="server" Width="150px" CssClass="form-control"
                        AutopostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px;" class="textoNorm">Tipo Vista:</td>                
                <td style="width: 220px;">
                    <asp:DropDownList ID="ddlVistaMarcas" runat="server" CssClass="form-control" onchange="mostrarSpinner();"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlVistaMarcas_SelectedIndexChanged">
                        <asp:ListItem Value="1">Ver marcas en Lista</asp:ListItem>
                        <asp:ListItem Value="2">Agrupar entradas y salidas</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortal"
                        OnClick="btnBuscar_Click"
                        OnClientClick="mostrarSpinner();"/>
                </td>
            </tr>
        </table>
        <div id="divImpresion">
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
                    <asp:BoundField DataField="DIA"
                        HeaderText="Día" ItemStyle-CssClass="textoGrid" />
                    <asp:BoundField DataField="F_H_MARCA"
                        HeaderText="Fecha"
                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="textoGrid" />
                    <asp:BoundField DataField="F_H_MARCA"
                        HeaderText="Hora"
                        DataFormatString="{0:HH:mm:ss}" ItemStyle-CssClass="textoGridBold" />
                    <asp:BoundField DataField="TIPO_MARCA"
                        HeaderText="Marcación" ItemStyle-CssClass="textoGrid" />
                    <asp:BoundField DataField="CENTRO"
                        HeaderText="CENTRO" ItemStyle-CssClass="textoGrid" />
                </Columns>
                <HeaderStyle CssClass="GridGralHeader" />
                <RowStyle CssClass="GridGralRow" />
                <AlternatingRowStyle CssClass="GridGralAltRow" />
            </asp:GridView>
            <asp:GridView ID="dgDataAgrupada"
                runat="server"
                AutoGenerateColumns="False"
                Width="100%"
                CssClass="GridGral"
                GridLines="None"
                CellPadding="6"
                EmptyDataText="Sin Resultados"
                EmptyDataRowStyle-CssClass="textoEmpty">
                <Columns>
                    <asp:BoundField DataField="DIA"
                        HeaderText="Día" ItemStyle-CssClass="textoGrid" />
                    <asp:BoundField DataField="FECHA"
                        HeaderText="Fecha Marcación"
                        DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="textoGrid" />
                    <asp:BoundField DataField="NRO_MARCA"
                        HeaderText="N° Marca" ItemStyle-CssClass="textoGrid" />
                    <asp:BoundField DataField="ENTRADA"
                        HeaderText="Hora Entrada"
                        DataFormatString="{0:HH:mm:ss}" ItemStyle-CssClass="textoGridBold" />
                    <asp:BoundField DataField="SALIDA"
                        HeaderText="Hora Salida"
                        DataFormatString="{0:HH:mm:ss}" ItemStyle-CssClass="textoGridBold" />
                    <asp:BoundField DataField="CENTRO"
                        HeaderText="CENTRO" ItemStyle-CssClass="textoGrid" />
                </Columns>
                <HeaderStyle CssClass="GridGralHeader" />
                <RowStyle CssClass="GridGralRow" />
                <AlternatingRowStyle CssClass="GridGralAltRow" />
            </asp:GridView>
        </div>
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
                    <asp:Button ID="btnImprimir"
                        runat="server"
                        Text="Imprimir Marcas"
                        CssClass="BotonPortalAmarillo"
                        OnClientClick="imprimirGrid(); return false;" />
                    &nbsp;
                    <asp:Button ID="btnVolver"
                        runat="server"
                        Text="Volver"
                        CssClass="BotonPortalGris"
                        OnClick="btnVolver_Click" />
                </td>
            </tr>
        </table>
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function imprimirGrid() {

        var contenido = document.getElementById("divImpresion").innerHTML;

        var nombre = '<%= Session["nombre"] %>';
        var rut = '<%= Session["rut"] %>';
        var periodo = '<%= (ddlMes.SelectedItem + " " + ddlAnio.SelectedValue) %>';

        var ventana = window.open('', '_blank', 'width=900,height=700');

        ventana.document.write('<html>');
        ventana.document.write('<head>');
        ventana.document.write('<title>Marcaciones Trabajador</title>');

        ventana.document.write('<style>');
        ventana.document.write('@page { size: A4; margin: 15mm; }');
        ventana.document.write('body { font-family: Arial; font-size: 12px; }');
        ventana.document.write('.titulo { text-align: center; font-size: 20px; font-weight: bold; margin-bottom: 20px; }');
        ventana.document.write('.datos { margin-bottom: 15px; line-height: 22px; }');
        ventana.document.write('table { width: 100%; border-collapse: collapse; }');
        ventana.document.write('th, td { border: 1px solid #333; padding: 6px; }');
        ventana.document.write('th { font-weight: bold; text-align: center; }');
        ventana.document.write('td { text-align: center; }');
        ventana.document.write('</style>');

        ventana.document.write('</head>');
        ventana.document.write('<body>');

        ventana.document.write('<div class="titulo">MARCACIONES DEL TRABAJADOR</div>');

        ventana.document.write('<div class="datos">');
        ventana.document.write('<b>Funcionario:</b> ' + nombre);
        ventana.document.write('<br>');
        ventana.document.write('<b>RUT:</b> ' + rut);
        ventana.document.write('<br>');
        ventana.document.write('<b>Periodo:</b> ' + periodo);
        ventana.document.write('</div>');

        ventana.document.write(contenido);

        ventana.document.write('</body>');
        ventana.document.write('</html>');

        ventana.document.close();
        ventana.focus();

        ventana.print();
        ventana.close();
    }
</script>
