<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionPersonas.aspx.cs" Inherits="contenido_Administracion_GestionPersonas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestion Personas</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function ExisteRut() {
            __doPostBack('ExisteRutPostBack', '')
        }
        function mostrarSpinner() {
            var form = document.getElementById('form1');

            if (!form.checkValidity())
                return false;

            document.getElementById('spinnerCarga').style.display = 'flex';
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <asp:Label ID="LbTitulo" runat="server" Text="Label" Visible="false"></asp:Label>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="100%" Width="100%"
            Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto"
            ActiveTabIndex="0">
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1">
                <HeaderTemplate>Información Personal</HeaderTemplate>
                <ContentTemplate>
                    <div class="formulario-persona">
                        <div class="bloque">
                            <div class="titulo-seccion">Información Básica</div>
                        </div>
                        <div class="filtros-form">
                            <div class="campo">
                                <label>RUT:</label>
                                <div style="display: flex; align-items: center; gap: 6px;">
                                    <asp:TextBox ID="TxtRut" runat="server"
                                        CssClass="form-control"
                                        MaxLength="9"
                                        onblur="IsInteger(this);ExisteRut();"
                                        Required="true" BackColor="LightSkyBlue"></asp:TextBox>
                                    <span>-</span>
                                    <asp:TextBox ID="TxtDv" runat="server"
                                        CssClass="form-control"
                                        MaxLength="1"
                                        Required="true" BackColor="LightSkyBlue"
                                        Style="max-width: 50px;"></asp:TextBox>
                                </div>
                        </div>
                        <div class="campo">
                            <label>Estado:</label>
                            <asp:TextBox ID="lbEstado" runat="server"
                                CssClass="form-control"
                                Font-Bold="True"
                                Text="Nuevo"
                                Enabled="false" BackColor="#ffffcc">
                            </asp:TextBox>
                        </div>
                        </div>
                        <div class="filtros-form">
                            <div class="campo">
                                <label>Nombres:</label>
                                <asp:TextBox ID="TxtNombre" runat="server"
                                    CssClass="form-control"
                                    MaxLength="80"
                                    Required="true"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Nombre Social:</label>
                                <asp:TextBox ID="TxtNombreSocial" runat="server"
                                    CssClass="form-control"
                                    MaxLength="80"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Apellido Paterno:</label>
                                <asp:TextBox ID="TxtPaterno" runat="server"
                                    CssClass="form-control"
                                    MaxLength="80"
                                    Required="true"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Apellido Materno:</label>
                                <asp:TextBox ID="TxtMaterno" runat="server"
                                    CssClass="form-control"
                                    MaxLength="80"
                                    Required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="filtros-form">
                            <div class="campo">
                                <label>Fecha Nacimiento:</label>
                                <asp:TextBox ID="txtFechaNacimiento" runat="server"
                                    CssClass="form-control" TextMode="Date" Required="true"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Sexo:</label>
                                <asp:DropDownList ID="ddlSexo" runat="server"
                                    CssClass="form-control">
                                    <asp:ListItem>Seleccione</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                    <asp:ListItem Value="O">Otro</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Estado Civil:</label>
                                <asp:DropDownList ID="ddlEstadoCivil" runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Previsión:</label>
                                <asp:DropDownList ID="ddlPrevision" runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2"
                                    runat="server"
                                    PromptCssClass="info"
                                    QueryPattern="Contains"
                                    PromptText="Escriba Prevision"
                                    TargetControlID="ddlPrevision"
                                    BehaviorID="_content_ListSearchExtender2" />
                            </div>
                        </div>
                        <div class="filtros-form">
                            <div class="campo">
                                <label>Dirección:</label>
                                <asp:TextBox ID="TxtDire" runat="server"
                                    CssClass="form-control"
                                    Required="true"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Región:</label>
                                <asp:DropDownList ID="ddlRegion" runat="server"
                                    CssClass="form-control"
                                    OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1"
                                    runat="server"
                                    TargetControlID="ddlRegion"
                                    PromptCssClass="info"
                                    QueryPattern="Contains"
                                    PromptText="Escriba Region"
                                    BehaviorID="_content_ListSearchExtender1" />
                            </div>
                            <div class="campo">
                                <label>Comuna:</label>
                                <asp:DropDownList ID="ddlComuna" runat="server"
                                    CssClass="form-control">
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3"
                                    runat="server"
                                    TargetControlID="ddlComuna"
                                    PromptCssClass="info"
                                    QueryPattern="Contains"
                                    PromptText="Escriba Comuna"
                                    BehaviorID="_content_ListSearchExtender3" />
                            </div>
                        </div>
                        <div class="filtros-form">
                            <div class="campo" style="grid-column: span 2;">
                                <label>Centro:</label>
                                <asp:DropDownList ID="ddlCentro" runat="server"
                                    CssClass="form-control" Required="true">
                                </asp:DropDownList>
                                <ajaxToolkit:ListSearchExtender ID="ListSearchExtender4"
                                    runat="server"
                                    TargetControlID="ddlCentro"
                                    PromptCssClass="info"
                                    QueryPattern="Contains"
                                    PromptText="Escriba Centro"
                                    BehaviorID="_content_ListSearchExtender4" />
                            </div>
                            <div class="campo">
                                <label>E-Mail:</label>
                                <asp:TextBox ID="TMail" runat="server"
                                    CssClass="form-control"
                                    MaxLength="100" Required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="filtros-form">
                            <div class="campo">
                                <label>Fono 1 (+56):</label>
                                <asp:TextBox ID="TFono1" runat="server"
                                    CssClass="form-control"
                                    MaxLength="9"
                                    onblur="IsInteger(this)"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Observación Fono 1:</label>
                                <asp:TextBox ID="TObsFono1" runat="server"
                                    CssClass="form-control"
                                    MaxLength="100"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Fono 2 (+56):</label>
                                <asp:TextBox ID="TFono2" runat="server"
                                    CssClass="form-control"
                                    MaxLength="9"
                                    onblur="IsInteger(this)"></asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Observación Fono 2:</label>
                                <asp:TextBox ID="TObsFono2" runat="server"
                                    CssClass="form-control"
                                    MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="botones-form">
                            <div class="campo">
                                <label>Observaciones:</label>
                                <asp:TextBox ID="observacion" runat="server"
                                    CssClass="form-control"
                                    TextMode="MultiLine"
                                    MaxLength="500"
                                    Style="height: 80px; width: 800px;"></asp:TextBox>
                            </div>
                        </div>
                        <div class="botones-form">
                            <asp:Button ID="btn_Agregar" runat="server"
                                Text="Grabar"
                                CssClass="BotonPortalAzul"
                                OnClick="btn_Agregar_Click"
                                OnClientClick="if (!confirm('¿Desea realizar la actualización de los datos?')) return false; mostrarSpinner();" />
                            <asp:Button ID="btn_habilitar" runat="server"
                                Text="Habilitar"
                                CssClass="BotonPortalAmarillo"
                                OnClick="btn_habilitar_Click"
                                OnClientClick="if (!confirm('¿Desea realizar la actualización de los datos?')) return false; mostrarSpinner();" />
                            <asp:Button ID="btn_ReinicioClv"
                                runat="server"
                                Text="Reiniciar Clave"
                                CssClass="BotonPortalVerde"
                                OnClick="btn_ReinicioClv_Click"
                                formnovalidate="formnovalidate"
                                OnClientClick="if (!confirm('¿Desea reiniciar la Clave del usuario?')) return false; mostrarSpinner();" />
                            <asp:Button ID="btnVolver"
                                runat="server"
                                Text="Volver"
                                CssClass="BotonPortalGris"
                                OnClick="btnVolver_Click"
                                formnovalidate="formnovalidate" />
                        </div>
                        <asp:HiddenField ID="hdIdentificador" runat="server" />
                        <asp:HiddenField ID="hdIdEstado" runat="server" />
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
