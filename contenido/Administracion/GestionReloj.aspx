<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionReloj.aspx.cs" Inherits="contenido_Administracion_GestionReloj" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Gestión de Relojes</title>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function confirmarGuardar() {
            var form = document.getElementById('form1');
            if (!form.checkValidity()) {
                form.reportValidity();
                return false;
            }
            if (!confirm('¿Desea guardar los datos del reloj?')) return false;
            document.getElementById('spinnerCarga').style.display = 'flex';
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bloque">
            <div class="titulo-seccion">
                <span>Gestión de Relojes</span>
            </div>
            <div class="bloque">
                <div class="bloque-titulo">Datos del reloj</div>
                <div class="filtros-form" style="padding:8px;">
                    <div class="campo">
                        <label>Descripción</label>
                        <asp:TextBox ID="txtDescripcion" runat="server"
                            CssClass="form-control" MaxLength="800"
                            required="required" />
                    </div>
                    <div class="campo">
                        <label>Dirección IP</label>
                        <asp:TextBox ID="txtIp" runat="server"
                            CssClass="form-control" MaxLength="100"
                            required="required" />
                    </div>
                    <div class="campo">
                        <label>Puerto</label>
                        <asp:TextBox ID="txtPuerto" runat="server"
                            CssClass="form-control" TextMode="Number"
                            required="required" Width="200px" />
                    </div>
                    <div class="campo">
                        <label>Número de serie</label>
                        <asp:TextBox ID="txtSerie" runat="server"
                            CssClass="form-control" TextMode="Number"
                            required="required" Width="200px" />
                    </div>
                    <div class="campo" style="grid-column: span 2;">
                        <label>Centro asignado</label>
                        <asp:DropDownList ID="ddlCentro" runat="server"
                            CssClass="form-control" />
                    </div>
                    <div class="campo">
                        <label>Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server"
                            CssClass="form-control">
                            <asp:ListItem Value="1">Activo</asp:ListItem>
                            <asp:ListItem Value="3">Inactivo</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="botones-form">
                    <asp:Button ID="btnGuardar" runat="server"
                        Text="Guardar" CssClass="BotonPortalAzul"
                        OnClick="btnGuardar_Click"
                        OnClientClick="return confirmarGuardar();" />
                    <asp:Button ID="btnCancelar" runat="server"
                        Text="Volver" CssClass="BotonPortalGris"
                        formnovalidate="formnovalidate"
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
