<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VistaTurnosTrab.aspx.cs" Inherits="contenido_RRHH_VistaTurnosTrab" %>

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
    <title>Turnos</title>
</head>
<body>
    <form id="form1" runat="server">
        <p class="textoTitLeft">Gestión Turnos</p>
        <table style="width: 100%; margin-bottom: 10px;">
            <tr>
                <td style="width: 70px;" class="textoNorm">Año:
                </td>
                <td style="width: 100px;">
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100px" CssClass="form-control" 
                        AutopostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px;" class="textoNorm">Mes:
                </td>
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlMes" runat="server" Width="150px" CssClass="form-control" 
                        AutopostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px;" class="textoNorm">Vista:
                </td>
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlVista"
                        runat="server"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlVista_SelectedIndexChanged"
                        onchange="mostrarSpinner();">
                        <asp:ListItem Value="1">Lista</asp:ListItem>
                        <asp:ListItem Value="2">Calendario</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btnBuscar_Click"
                        OnClientClick="mostrarSpinner();" />
                </td>
            </tr>
        </table>
        <div id="divImpresion">
            <asp:Panel ID="pnlLista" runat="server">
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
                            DataField="FECHA"
                            HeaderText="Fecha"
                            ItemStyle-CssClass="textoGridBold" />
                        <asp:BoundField
                            DataField="TURNO"
                            HeaderText="Turno"
                            ItemStyle-CssClass="textoGrid">
                            <ItemStyle Width="250px" />
                        </asp:BoundField>
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
            </asp:Panel>

        </div>
        <asp:Panel ID="pnlCalendario" runat="server" Visible="false">
            <div id="calendarioImprimir">
                <asp:Literal ID="litCalendario" runat="server"></asp:Literal>
            </div>
        </asp:Panel>
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
                        Text="Imprimir Turnos"
                        CssClass="BotonPortalAmarillo"
                        OnClientClick="return imprimirVista();" />
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
        var fecha = '<%= ddlMes.SelectedItem + " " + ddlAnio.SelectedItem %>';

        var ventana = window.open('', '_blank', 'width=900,height=700');

        ventana.document.write('<html>');
        ventana.document.write('<head>');
        ventana.document.write('<title>Turnos Trabajador</title>');

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

        ventana.document.write('<div class="titulo">TURNOS DEL TRABAJADOR</div>');

        ventana.document.write('<div class="datos">');
        ventana.document.write('<b>Funcionario:</b> ' + nombre);
        ventana.document.write('<br>');
        ventana.document.write('<b>RUT:</b> ' + rut);
        ventana.document.write('<br>');
        ventana.document.write('<b>Periodo:</b> ' + fecha);
        ventana.document.write('</div>');

        ventana.document.write(contenido);

        ventana.document.write('</body>');
        ventana.document.write('</html>');

        ventana.document.close();
        ventana.focus();

        ventana.print();
        ventana.close();
    }
    function imprimirVista() {
        var calendario = document.getElementById("calendarioImprimir");
        if (calendario && calendario.innerHTML.trim() !== "") {
            imprimirCalendario();
            return false;
        }
        imprimirGrid();
        return false;
    }

    function imprimirCalendario() {
        var nombre = '<%= Session["nombre"] %>';
        var rut = '<%= Session["rut"] %>';
        var fecha = '<%= ddlMes.SelectedItem + " " + ddlAnio.SelectedItem %>';

        var contenido = document.getElementById("calendarioImprimir");
        if (!contenido) {
            alert("No se encontró el calendario.");
            return;
        }

        var ventana = window.open('', '_blank', 'width=1200,height=800');
        ventana.document.write('<html>');
        ventana.document.write('<head>');
        ventana.document.write('<title>Calendario de Turnos</title>');
        ventana.document.write('<style>');
        ventana.document.write(`
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .titulo { 
            text-align: center; 
            font-size: 20px;
            font-weight: bold; 
            margin-bottom: 20px;
       }
        .datos { 
            margin-bottom: 15px;
            line-height: 22px; 
       }
        .calendario {
            width: 100%;
            border: 1px solid #ddd;
            border-radius: 8px;
            overflow: hidden;
            background: #fff;
        }

        .calendario-header {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
            background: #f2f4f7;
            border-bottom: 1px solid #ddd;
        }

        .calendario-header div {
            padding: 10px;
            text-align: center;
            font-weight: bold;
            color: #555;
            border-right: 1px solid #ddd;
        }

        .calendario-semana {
            display: grid;
            grid-template-columns: repeat(7, 1fr);
        }

        .calendario-dia,
        .calendario-domingo,
        .calendario-feriado,
        .calendario-domingo-feriado {
            min-height: 115px;
            padding: 8px;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
            box-sizing: border-box;
        }

        .calendario-dia {
            background: #fff;
        }

        .calendario-domingo {
            background: #fce8e8;
        }

        .calendario-feriado {
            background: #fff3cd;
        }

        .calendario-domingo-feriado {
            background: #f5d6d6;
        }

        .calendario-dia-vacio {
            background: #f8f8f8;
        }

        .calendario-numero {
            font-weight: bold;
            font-size: 15px;
            margin-bottom: 6px;
            color: #555;
        }

        .calendario-domingo .calendario-numero {
            color: #c0392b;
        }

        .calendario-feriado .calendario-numero {
            color: #b7791f;
        }

        .calendario-turno {
            font-size: 13px;
            font-weight: bold;
            margin-bottom: 4px;
        }

        .calendario-horario {
            font-size: 12px;
            margin-bottom: 3px;
        }

        .calendario-duracion {
            font-size: 11px;
            color: #777;
        }

        .calendario-libre {
            font-size: 12px;
            color: #999;
            font-style: italic;
        }
        @media print {
            body {
                margin: 10mm;
            }

            .calendario {
                page-break-inside: avoid;
            }
        }
    `);
        ventana.document.write('</style>');
        ventana.document.write('</head>');
        ventana.document.write('<body>');        
        ventana.document.write('<div class="titulo">TURNOS DEL TRABAJADOR</div>');
        ventana.document.write('<div class="datos">');
        ventana.document.write('<b>Funcionario:</b> ' + nombre);
        ventana.document.write('<br>');
        ventana.document.write('<b>RUT:</b> ' + rut);
        ventana.document.write('<br>');
        ventana.document.write('<b>Periodo:</b> ' + fecha);
        ventana.document.write('</div>');
        ventana.document.write(contenido.innerHTML);
        ventana.document.write('</body>');
        ventana.document.write('</html>');
        ventana.document.close();
        ventana.focus();
        setTimeout(function () { ventana.print(); ventana.close(); }, 500);
    }
</script>
