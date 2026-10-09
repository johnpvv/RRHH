<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestUniOperativa.aspx.cs" Inherits="contenido_Administracion_GestUniOperativa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Edición Centros</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" />
        <div class="bloque">
            <div class="titulo-seccion">
                <span>Gestión y Edición Centros</span>
            </div>
            <ajaxToolkit:TabContainer ID="TC_1" runat="server" ActiveTabIndex="0">
                <ajaxToolkit:TabPanel ID="TP_1" runat="server" HeaderText="Detalle">
                    <ContentTemplate>
                        <div class="bloque">
                            <div class="bloque-titulo">Datos del centro</div>
                            <div class="filtros-grid">
                                <div class="campo">
                                    <label>Nombre</label>
                                    <asp:TextBox ID="TxtNombre" runat="server"
                                        CssClass="form-control" MaxLength="80" required="required" Width="500px" />
                                </div>
                                <div class="campo">
                                    <label>Código</label>
                                    <asp:TextBox ID="TxtCodigo" runat="server"
                                        CssClass="form-control" MaxLength="3" Width="100px" />
                                </div>
                                <div class="campo" style="display: flex; align-items: end; padding-bottom: 5px;">
                                    <asp:CheckBox ID="chk360" runat="server" Text="Extiende" />
                                </div>
                                <div class="campo">
                                    <asp:Button ID="BtnAgregar" runat="server"
                                        Text="Agregar / Modificar"
                                        CssClass="BotonPortalAzul"
                                        OnClick="BtnAgregar_Click"
                                        OnClientClick="return confirmarActualizacion();" />
                                </div>
                                <div class="campo">
                                    <asp:Button ID="btnVolver" runat="server"
                                        Text="Volver"
                                        CssClass="BotonPortalGris"
                                        OnClick="btnVolver_Click"
                                        formnovalidate="formnovalidate"/>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Relojes del centro">
                    <ContentTemplate>
                        <div class="bloque">
                            <div class="bloque-titulo">Administración de relojes</div>
                            <div class="filtros-grid">
                                <div class="campo">
                                    <label>Código del reloj</label>
                                    <asp:TextBox ID="TCodigo" runat="server"
                                        CssClass="form-control" MaxLength="80" />
                                </div>
                                <div class="campo">
                                    <label>Descripción</label>
                                    <asp:TextBox ID="TDesc" runat="server"
                                        CssClass="form-control" MaxLength="80" />
                                </div>
                                <div class="campo">
                                    <label>Consultar</label>
                                    <asp:RadioButtonList ID="rbLista" runat="server"
                                        CssClass="TextoCheck" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Disponibles</asp:ListItem>
                                        <asp:ListItem Value="2">Asignados</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="campo">
                                    <asp:Button ID="btn_Buscar" runat="server"
                                        Text="Buscar" CssClass="BotonPortalAzul"
                                        OnClick="btn_Buscar_Click"
                                        OnClientClick="mostrarSpinner();" />
                                </div>
                                <div class="campo">
                                    <asp:Button ID="btn_Limpiar" runat="server"
                                        Text="Limpiar" 
                                        CssClass="BotonPortalGris"
                                        OnClick="btn_Limpiar_Click"
                                        CausesValidation="false"/>
                                </div>
                            </div>
                        </div>
                        <div class="asignacion-grid">
                            <div class="asignacion-columna">
                                <div class="asignacion-titulo">Relojes Disponibles</div>
                                <asp:GridView ID="gdArt" runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="grid-reloj"
                                    GridLines="None"
                                    DataKeyNames="IDRELOJ"
                                    AllowPaging="True"
                                    PageSize="15"
                                    OnRowDataBound="gdArt_RowDataBound"
                                    OnSelectedIndexChanged="gdArt_SelectedIndexChanged"
                                    OnPageIndexChanging="gdArt_PageIndexChanging"
                                    EmptyDataText="No hay relojes disponibles."
                                    EmptyDataRowStyle-CssClass="textoEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="IDRELOJ" HeaderText="ID" ReadOnly="True" />
                                        <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                        <asp:TemplateField HeaderText="Asignar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_Add" runat="server"
                                                    ImageUrl="~/imagenes/check.png"
                                                    ToolTip="Asignar reloj al centro"
                                                    OnClick="AddUnidad"
                                                    OnClientClick="mostrarSpinner();" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CommandField SelectText="Seleccionar"
                                            ShowSelectButton="True" Visible="False" />
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                            <div class="asignacion-separador">
                                <span>→</span>
                            </div>
                            <div class="asignacion-columna">
                                <div class="asignacion-titulo">Relojes Asociados</div>
                                <asp:GridView ID="gbArtSer" runat="server"
                                    AutoGenerateColumns="False"
                                    CssClass="grid-reloj"
                                    GridLines="None"
                                    DataKeyNames="IDRELOJ"
                                    AllowPaging="True"
                                    PageSize="15"
                                    OnRowDataBound="gbArtSer_RowDataBound"
                                    OnSelectedIndexChanged="gbArtSer_SelectedIndexChanged"
                                    OnPageIndexChanging="gbArtSer_PageIndexChanging"
                                    EmptyDataText="No hay relojes asignados a este centro."
                                    EmptyDataRowStyle-CssClass="textoEmpty">
                                    <Columns>
                                        <asp:BoundField DataField="IDRELOJ" HeaderText="ID" ReadOnly="True" />
                                        <asp:BoundField DataField="CODIGO" HeaderText="Código" />
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                                        <asp:TemplateField HeaderText="Quitar">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_Elim" runat="server"
                                                    ImageUrl="~/imagenes/close.png"
                                                    ToolTip="Quitar reloj del centro"
                                                    OnClick="ElimUnidad"
                                                    OnClientClick="mostrarSpinner();" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:CommandField SelectText="Seleccionar"
                                            ShowSelectButton="True" Visible="False" />
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </div>
        <div id="spinnerCarga" class="spinner-overlay" style="display: none;">
            <div class="spinner"></div>
            <div class="spinner-text">
                Cargando Datos, Favor Espere...
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function confirmarActualizacion() {
            if (!document.getElementById('<%= TxtNombre.ClientID %>').checkValidity()) {
                return true;
            }

            if (!confirm('¿Desea realizar la actualización de los datos?')) {
                return false;
            }
            mostrarSpinner();
            return true;
        }
        function mostrarSpinner() {
            var spinner = document.getElementById('spinnerCarga');
            if (spinner) spinner.style.display = 'flex';
        }
    </script>
</body>
</html>

