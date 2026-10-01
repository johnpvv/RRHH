<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionRoles.aspx.cs" Inherits="contenido_SysAdmin_GestionRoles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestion Roles</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function mostrarSpinner() {
            var spinner = document.getElementById('spinnerCarga');
            if (spinner) {
                spinner.style.display = 'flex';
            }
        }
    </script>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True" />
        <div class="bloque">
            <div class="bloque-titulo">
                Gestión Roles --&gt; Detalle --&gt;
            <asp:Label ID="LbTitulo" runat="server" Text="Label" />
            </div>
        </div>
        <ajaxToolkit:TabContainer ID="TC_1" runat="server"
            ActiveTabIndex="0"
            CssClass="tabs-rrhh">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>
                    Detalle Rol
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Información del Rol
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Código</label>
                                <asp:TextBox ID="TCodigo" runat="server"
                                    CssClass="form-control"
                                    MaxLength="10" />
                            </div>
                            <div class="campo">
                                <label>Nombre</label>
                                <asp:TextBox ID="TNombre" runat="server"
                                    CssClass="form-control"
                                    MaxLength="80" />
                            </div>
                            <div class="campo" style="grid-column: span 2;">
                                <label>Observaciones</label>
                                <asp:TextBox ID="TObser" runat="server"
                                    CssClass="form-control"
                                    MaxLength="400"
                                    Width="800px" Height="70px"
                                    TextMode="MultiLine"
                                    Rows="3" />
                            </div>
                        </div>
                        <div class="botones-form">
                            <asp:Button ID="Button1" runat="server"
                                Text="Agregar"
                                CssClass="BotonPortalAzul"
                                OnClick="BtnAgregar_Click" />
                            <asp:Button ID="btnNuevo" runat="server"
                                Text="Nuevo"
                                CssClass="BotonPortalVerde"
                                OnClick="btnNuevo_Click" />
                            <asp:Button ID="btnEliminar" runat="server"
                                Text="Eliminar"
                                CssClass="BotonPortalRojo"
                                OnClick="btnEliminar_Click" />
                            <asp:Button ID="btnRehabilitar" runat="server"
                                Text="Rehabilitar"
                                CssClass="BotonPortalAmarillo"
                                OnClick="btnRehabilitar_Click" />
                            <asp:Button ID="btnVolver" runat="server"
                                Text="Volver"
                                CssClass="BotonPortalGris"
                                OnClick="btnVolver_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>
                    Accesos
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Aplicaciones y Accesos
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Tipo de acceso</label>
                                <asp:RadioButtonList ID="rbTipo" runat="server"
                                    CssClass="TextoCheck"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="L">Lectura</asp:ListItem>
                                    <asp:ListItem Value="M">Modificación</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="campo">
                                <asp:Button ID="btn_Buscar" runat="server"
                                    Text="Buscar"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btn_Buscar_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="asignacion-grid">
                        <div class="asignacion-columna">
                            <div class="asignacion-titulo">Disponibles</div>
                            <asp:GridView ID="gdArt" runat="server"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                CssClass="grid-reloj"
                                OnRowDataBound="gdArt_RowDataBound"
                                OnSelectedIndexChanged="gdArt_SelectedIndexChanged"
                                AllowPaging="True"
                                OnPageIndexChanging="gdArt_PageIndexChanging"
                                DataKeyNames="idapp"
                                PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idapp" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigo" HeaderText="Código">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                        <ItemStyle CssClass="TextoLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Agregar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Add"
                                                runat="server"
                                                ImageUrl="~/imagenes/check.png"
                                                OnClick="AddRol"
                                                ToolTip="Agregar" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="asignacion-separador">
                            <span>→</span>
                        </div>
                        <div class="asignacion-columna">
                            <div class="asignacion-titulo">Asociados</div>
                            <asp:GridView ID="gbArtSer" runat="server"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                CssClass="grid-reloj"
                                OnRowDataBound="gbArtSer_RowDataBound"
                                OnSelectedIndexChanged="gbArtSer_SelectedIndexChanged"
                                AllowPaging="True"
                                OnPageIndexChanging="gbArtSer_PageIndexChanging"
                                DataKeyNames="idrolapp"
                                PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idrolapp" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="codigo" HeaderText="Código">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción">
                                        <ItemStyle CssClass="TextoLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Elim"
                                                runat="server"
                                                ImageUrl="~/imagenes/close.png"
                                                OnClick="ElimRol"
                                                ToolTip="Eliminar" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server">
                <HeaderTemplate>
                    Usuarios
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">Asignación de Usuarios</div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Nombre</label>
                                <asp:TextBox ID="TNombreUsr" runat="server"
                                    CssClass="form-control" MaxLength="80" Width="280px" />
                            </div>
                            <div class="campo">
                                <label>Rut</label>
                                <asp:TextBox ID="TRut" runat="server"
                                    CssClass="form-control" Width="120px" />
                            </div>
                            <div class="campo">
                                <label>Tipo de acceso</label>
                                <asp:RadioButtonList ID="dbTipoUser"
                                    runat="server"
                                    CssClass="TextoCheck"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="L" Selected="True">Disponibles</asp:ListItem>
                                    <asp:ListItem Value="M">Asociados</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="botones-form">
                                <asp:Button ID="BtBuscarUser"
                                    runat="server"
                                    Text="Buscar"
                                    CssClass="BotonPortalAzul"
                                    OnClick="BtBuscarUser_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="asignacion-grid">
                        <div class="asignacion-columna">
                            <div class="asignacion-titulo">Disponibles</div>
                            <asp:GridView ID="gbUserDisp"
                                runat="server"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                CssClass="grid-reloj"
                                OnRowDataBound="dvUser_RowDataBound"
                                OnSelectedIndexChanged="dvUser_SelectedIndexChanged"
                                AllowPaging="True"
                                OnPageIndexChanging="dvUser_PageIndexChanging"
                                DataKeyNames="idusuario"
                                PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idusuario" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="descripcion">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="descripcion">
                                        <ItemStyle CssClass="TextoLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Add">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Add"
                                                runat="server"
                                                ImageUrl="~/imagenes/check.png"
                                                OnClick="AddUser"
                                                ToolTip="Agregar" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                </Columns>
                                <PagerStyle
                                    CssClass="GridPager"
                                    HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                        <div class="asignacion-separador">
                            <span>→</span>
                        </div>
                        <div class="asignacion-columna">
                            <div class="asignacion-titulo">Asociados</div>
                            <asp:GridView ID="gbUser"
                                runat="server"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                CssClass="grid-reloj"
                                OnRowDataBound="gbUserDisp_RowDataBound"
                                OnSelectedIndexChanged="gbUserDisp_SelectedIndexChanged"
                                AllowPaging="True"
                                OnPageIndexChanging="gbUserDisp_PageIndexChanging"
                                DataKeyNames="idusrol"
                                PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idusrol" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="descripcion">
                                        <ItemStyle CssClass="TextoCenter" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="descripcion">
                                        <ItemStyle CssClass="TextoLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Elim">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Elim"
                                                runat="server"
                                                ImageUrl="~/imagenes/close.png"
                                                OnClick="ElimUser"
                                                ToolTip="Eliminar" />
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                </Columns>
                                <PagerStyle
                                    CssClass="GridPager"
                                    HorizontalAlign="Center" />
                            </asp:GridView>
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

