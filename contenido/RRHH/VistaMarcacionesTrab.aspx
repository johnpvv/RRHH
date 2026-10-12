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
                        AutoPostBack="true" OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px;" class="textoNorm">Mes:</td>
                <td style="width: 120px;">
                    <asp:DropDownList ID="ddlMes" runat="server" Width="150px" CssClass="form-control"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </td>
                <td style="width: 70px;" class="textoNorm">Tipo Vista:</td>
                <td style="width: 250px;">
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
                        OnClientClick="mostrarSpinner();" />
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
            OnRowDataBound="dgData_RowDataBound">
            <Columns>
                <asp:BoundField DataField="DIA"
                    HeaderText="Día" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="F_H_MARCA"
                    HeaderText="Fecha"
                    DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="textoGridBold" />
                <asp:TemplateField HeaderText="Hora Marca" ItemStyle-CssClass="textoGridBoldLeft" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <span class='estado-marca-trabajador <%# Eval("ESTADO_MARCA") %>'
                            title='<%# Eval("ESTADO_MARCA") %>'>&nbsp;</span>
                        <%# Eval("F_H_MARCA","{0:HH:mm:ss}") %>
                        <%# Eval("ESTADO_MARCA").ToString()=="MARCA EDITADA" ? "*":"" %>
                    </ItemTemplate>
                </asp:TemplateField>
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
            EmptyDataRowStyle-CssClass="textoEmpty"
            OnRowDataBound="dgDataAgrupada_RowDataBound">
            <Columns>
                <asp:BoundField DataField="DIA"
                    HeaderText="Día" ItemStyle-CssClass="textoGrid" />
                <asp:BoundField DataField="FECHA"
                    HeaderText="Fecha Marcación"
                    DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="textoGridBold" />
                <asp:BoundField DataField="NRO_MARCA"
                    HeaderText="N° Marca" ItemStyle-CssClass="textoGrid" />
                <asp:TemplateField HeaderText="Hora Entrada" ItemStyle-CssClass="textoGridBoldLeft" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <span class='estado-marca-trabajador <%# Eval("ESTADO_ENTRADA") %>'
                            title='<%# Eval("ESTADO_ENTRADA") %>'>&nbsp;
                        </span>
                        <%# Eval("ENTRADA","{0:HH:mm:ss}") %>
                        <%# Eval("ESTADO_ENTRADA").ToString()=="MARCA EDITADA" ? "*" : "" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CENTRO_ENT" HeaderText="Centro Entrada" ItemStyle-CssClass="textoGridLeft" />
                <asp:TemplateField HeaderText="Hora Salida" ItemStyle-CssClass="textoGridBoldLeft" HeaderStyle-Width="130px">
                    <ItemTemplate>
                        <span class='estado-marca-trabajador <%# Eval("ESTADO_SALIDA") %>'
                            title='<%# Eval("ESTADO_SALIDA") %>'>&nbsp;                                
                        </span>
                        <%# Eval("SALIDA","{0:HH:mm:ss}") %>
                        <%# Eval("ESTADO_SALIDA").ToString()=="MARCA EDITADA" ? "*":"" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CENTRO_SAL" HeaderText="Centro Salida" ItemStyle-CssClass="textoGridLeft" />
            </Columns>
            <HeaderStyle CssClass="GridGralHeader" />
            <RowStyle CssClass="GridGralRow" />
            <AlternatingRowStyle CssClass="GridGralAltRow" />
        </asp:GridView>
        <br />
        <div class="botones-form">
            <asp:Button ID="btnExportar"
                runat="server"
                Text="Exportar Excel"
                CssClass="BotonPortalVerde"
                OnClick="btnExportar_Click" />
            <asp:Button ID="btnImprimirPDF"
                runat="server"
                Text="Generar PDF Marcas"
                CssClass="BotonPortalAmarillo"
                OnClick="btnExportarPDF_Click" />
            <asp:Button ID="btnVolver"
                runat="server"
                Text="Volver"
                CssClass="BotonPortalGris"
                OnClick="btnVolver_Click" />
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
