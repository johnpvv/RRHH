<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionHorarios.aspx.cs" Inherits="contenido_GestionRRHH_GestionHorarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RRHH_GestionHoras</title>
    <script src="../../js/common.js" type="text/javascript"></script>
    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/all.min.css" rel="stylesheet" />
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" />
    <script type="text/javascript">
        function ConfirmarGuardar() {
            if (confirm('¿Desea guardar los Cambios?')) {
                document.getElementById('spinnerCarga').style.display = 'block';
                return true;
            }
            return false;
        }
        function ValidarTexto(obj) {
            var strNumero
            strNumero = document.getElementById(obj).value

            strNumero = strNumero.replace(",", ".");
            if (isNaN(strNumero) == true) {
                alert("El valor " + document.getElementById(obj).value + " no es un número.");
                document.getElementById(obj).value = "";
                document.getElementById(obj).focus();
            }
            else {
                document.getElementById(obj).value = strNumero;
            }
        }
    </script>
    <script type="text/javascript">
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
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <div class="bloque">
            <div class="titulo-seccion">
                Gestión Horarios
                <span>/ Antecedentes / Codigo: </span>
                <asp:Label ID="LbTitulo" runat="server" Text="Label" />
            </div>
        </div>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" ActiveTabIndex="0" CssClass="tabs-moderno" ScrollBars="Auto">
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1">
                <HeaderTemplate>
                    Información General
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Información Básica
                        </div>
                        <div class="filtros-grid-center">
                            <div class="campo">
                                <label>Estado:</label>
                                <asp:Label ID="lbEstado" runat="server" Font-Bold="True" />
                            </div>
                            <div class="campo">
                                <label>ID:</label>
                                <asp:TextBox ID="TxtId" runat="server"
                                    CssClass="form-control" Width="100px"
                                    Font-Bold="True" Enabled="False" />
                            </div>
                            <div class="campo">
                                <label>Descripción:</label>
                                <asp:TextBox ID="TxtDescr" runat="server"
                                    CssClass="form-control" Width="450px"
                                    Height="120px" TextMode="MultiLine"
                                    MaxLength="700" />
                            </div>
                            <div class="campo">
                                <label>Fecha Creación:</label>
                                <asp:TextBox ID="txtFCrea" runat="server"
                                    CssClass="form-control"
                                    Width="150px" Enabled="False" />
                                <ajaxToolkit:CalendarExtender ID="CE2"
                                    TargetControlID="txtFCrea" runat="server"
                                    BehaviorID="_content_CE2" />
                                <ajaxToolkit:MaskedEditExtender
                                    ID="MEE" TargetControlID="txtFCrea"
                                    Mask="99/99/9999" runat="server"
                                    MaskType="Date" BehaviorID="_content_MEE"
                                    Century="2000" />
                            </div>
                        </div>
                        <asp:HiddenField ID="hdIdHora" runat="server" />
                        <div class="botones-form">
                            <asp:Button ID="btn_Agregar"
                                Text="Grabar"
                                CssClass="BotonPortalAzul"
                                runat="server"
                                OnClick="btn_Agregar_Click"
                                OnClientClick="return ConfirmarGuardar();" />
                            <asp:Button ID="btn_habilitar"
                                runat="server"
                                CssClass="BotonPortalAmarillo"
                                Text="Habilitar"
                                OnClick="btn_habilitar_Click"
                                OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..');mostrarSpinner();" />

                            <asp:Button ID="btnVolver_1"
                                runat="server"
                                Text="Volver"
                                CssClass="BotonPortalGris"
                                OnClick="btnVolver_Click"
                                CausesValidation="false" />
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    Detalle Horario
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Configuración del Horario
                        </div>
                        <div class="filtros-grid-center">
                            <div style="display: flex; align-items: center; gap: 10px;">
                                <div class="campo">
                                    <label>Intervalo en Minutos:</label>
                                    <asp:TextBox ID="txtint" runat="server"
                                        CssClass="form-control" Width="100px"
                                        MaxLength="3" onblur="ValidarTexto(this.id)" />
                                </div>
                                <div class="campo">
                                    <label>&nbsp;</label>
                                    <asp:Button ID="btnCalcular"
                                        runat="server"
                                        Text="Generar Intervalos"
                                        CssClass="BotonPortalVerde"
                                        OnClick="btnCalcular_Click"
                                        OnClientClick="mostrarSpinner();" />
                                </div>
                            </div>
                            <div class="campo">
                                <label>Hora de Entrada:</label>
                                <asp:DropDownList ID="ddlHoraEntrada"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="150px"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="calcHoras">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Hora de Salida:</label>
                                <asp:DropDownList ID="ddlHoraSalida"
                                    runat="server"
                                    CssClass="form-control"
                                    Width="150px"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="calcHoras">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Duración:</label>
                                <div style="display: flex; align-items: center; gap: 8px;">
                                    <asp:TextBox ID="txtHoras"
                                        runat="server"
                                        Width="65px"
                                        Enabled="false"
                                        CssClass="form-control"
                                        Font-Bold="true"
                                        Font-Size="18px"
                                        ForeColor="Black" />
                                    <span>Horas</span>
                                    <asp:TextBox ID="txtMinuto"
                                        runat="server"
                                        Width="65px"
                                        Enabled="false"
                                        CssClass="form-control"
                                        Font-Bold="true"
                                        Font-Size="18px"
                                        ForeColor="Black" />
                                    <span>Minutos</span>
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblResultado" runat="server" ForeColor="Red" />
                        <div class="botones-form">
                            <asp:Button ID="btnGuardarDetalle"
                                runat="server"
                                Text="Guardar Horario"
                                CssClass="BotonPortalAzul"
                                OnClick="btnGuardarDetalle_Click"
                                OnClientClick="mostrarSpinner();" />
                            <asp:Button ID="btnVolver"
                                runat="server"
                                Text="Volver"
                                CssClass="BotonPortalGris"
                                OnClick="btnVolver_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
</body>
</html>
