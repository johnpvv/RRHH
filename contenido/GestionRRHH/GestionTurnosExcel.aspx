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
                mostrarSpinner();
                return true;
            }
            return false;
        }
        function mostrarNombreArchivo(input) {
            var etiqueta = document.getElementById('nombreArchivoSeleccionado');

            if (input.files && input.files.length > 0) {
                etiqueta.innerText = input.files[0].name;
            } else {
                etiqueta.innerText = 'Ningún archivo seleccionado';
            }
        }
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
                Gestión Turnos Excel
            </div>
            <div class="filtros-grid">
                <div class="campo" style="grid-column: 1 / -1;">
                    <label>Elegir archivo Excel a cargar:</label>
                    <div class="archivo-personalizado">
                        <label for="<%= fuExcel.ClientID %>" class="btn-archivo">
                            <span style="font-size: 14px;">📎 Seleccionar archivo</span>
                        </label>
                        <asp:FileUpload
                            ID="fuExcel" runat="server"
                            CssClass="input-archivo-oculto"
                            onchange="mostrarNombreArchivo(this);"
                            accept=".xls,.xlsx" />
                        <span id="nombreArchivoSeleccionado" class="nombre-archivo">Ningún archivo seleccionado            
                        </span>
                    </div>
                </div>
            </div>
            <div class="botones-form">
                <asp:Button
                    ID="btnCargar"
                    runat="server"
                    Text="Procesar Excel"
                    CssClass="BotonPortalVerde"
                    OnClick="btnCargar_Click"
                    OnClientClick="return ConfirmarGuardar();" />
                <asp:Button
                    ID="btnPlantilla"
                    runat="server"
                    Text="Descargar Plantilla Ejemplo"
                    CssClass="BotonPortalAmarillo"
                    OnClick="btnPlantilla_Click" />
                <asp:Button
                    ID="btnVolver"
                    runat="server"
                    Text="Volver"
                    CssClass="BotonPortalGris"
                    OnClick="btnVolver_Click" />
            </div>
            <div style="margin-top: 30px;">
                <asp:Label ID="lblResultado" runat="server" CssClass="textoNormLeft" />
                <br />
                <asp:Label ID="lbMensaje" runat="server" CssClass="textoNormLeft" />
            </div>
            <div class="campo">
                <asp:GridView ID="dgData"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    CssClass="grid-reloj"
                    GridLines="None"
                    CellPadding="6"
                    EmptyDataText="No se ha cargado ningún dato al sistema"
                    EmptyDataRowStyle-CssClass="textoEmpty"
                    DataKeyNames="RUT">
                    <Columns>
                        <asp:BoundField DataField="RUT" HeaderText="RUT" />
                        <asp:BoundField DataField="NOMBRES" HeaderText="Nombres" />
                        <asp:BoundField DataField="CODIGO_TURNO" HeaderText="Código Turno" />
                    </Columns>
                    <HeaderStyle CssClass="GridGralHeader" />
                    <RowStyle CssClass="GridGralRow" />
                    <AlternatingRowStyle CssClass="GridGralAltRow" />
                </asp:GridView>
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
