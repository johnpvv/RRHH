<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionAccesos.aspx.cs" Inherits="contenido_SysAdmin_GestionAccesos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Selecting GridView Row</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <link runat="server" href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True" />
        <div class="bloque">
            <div class="bloque-titulo">
                Gestión Accesos --> Detalle -->
                <asp:Label ID="LbTitulo" runat="server" />
            </div>
        </div>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" ActiveTabIndex="0" Width="100%"
            CssClass="tabs-rrhh">
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1">
                <HeaderTemplate>Detalle Acceso</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">Información del Acceso</div>
                        <div class="form-linea">
                            <div class="campo">
                                <label>Código:</label>
                                <asp:TextBox ID="TCodigo" runat="server" MaxLength="20" CssClass="form-control" Width="140px" />
                            </div>
                            <div class="campo">
                                <label>Nombre:</label>
                                <asp:TextBox ID="TNombre" runat="server" MaxLength="80" CssClass="form-control" Width="420px" />
                            </div>
                            <div class="campo">
                                <label>Observaciones:</label>
                                <asp:TextBox ID="TObser" runat="server" MaxLength="400" TextMode="MultiLine"
                                    CssClass="form-control" Width="800px" Height="70px" />
                            </div>
                        </div>
                        <div class="botones-form">
                            <asp:Button ID="Button1" runat="server" Text="Agregar"
                                OnClick="BtnAgregar_Click" CssClass="BotonPortalAzul" />
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo"
                                OnClick="btnNuevo_Click" CssClass="BotonPortalAmarillo" />
                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
                                OnClick="btnEliminar_Click" CssClass="BotonPortalRojo" />
                            <asp:Button ID="btnRehabilitar" runat="server" Text="Rehabilitar"
                                OnClick="btnRehabilitar_Click" CssClass="BotonPortalVerde" />
                            <asp:Button ID="btnVolver" runat="server" Text="Volver"
                                CssClass="BotonPortalGris" OnClick="btnVolver_Click" />
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>Roles</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">Asignación de Roles</div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Nombre:</label>
                                <asp:TextBox ID="TNombreRol" runat="server" MaxLength="80"
                                    CssClass="form-control" Width="280px" />
                            </div>

                            <div class="campo">
                                <label>Tipo de Búsqueda:</label>
                                <asp:RadioButtonList ID="rbTipo" runat="server" CssClass="TextoCheck"
                                    RepeatDirection="Horizontal" Width="280px">
                                    <asp:ListItem Value="L" Selected="True">Disponibles</asp:ListItem>
                                    <asp:ListItem Value="M">Asociados</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                            <div class="campo">
                                <label>&nbsp;</label>
                                <asp:Button ID="btn_Buscar" runat="server" Text="Buscar"
                                    OnClick="btn_Buscar_Click" CssClass="BotonPortalAzul" />
                            </div>
                        </div>
                    </div>
                    <div class="asignacion-grid">
                        <div class="asignacion-columna">
                            <div class="asignacion-titulo">Disponibles</div>
                            <asp:GridView ID="gdArt" runat="server" AutoGenerateColumns="False"
                                CssClass="grid-reloj" GridLines="None"
                                OnRowDataBound="gdArt_RowDataBound"
                                OnSelectedIndexChanged="gdArt_SelectedIndexChanged"
                                AllowPaging="True" OnPageIndexChanging="gdArt_PageIndexChanging"
                                DataKeyNames="idrol" PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idrol" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Rol">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Agregar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Add" runat="server"
                                                ImageUrl="~/imagenes/check.png"
                                                OnClick="AddRol" ToolTip="Agregar rol" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
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
                            <asp:GridView ID="gbArtSer" runat="server" AutoGenerateColumns="False"
                                CssClass="grid-reloj" GridLines="None"
                                OnRowDataBound="gbArtSer_RowDataBound"
                                OnSelectedIndexChanged="gbArtSer_SelectedIndexChanged"
                                AllowPaging="True" OnPageIndexChanging="gbArtSer_PageIndexChanging"
                                DataKeyNames="idrolapp" PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idrolapp" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Rol">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Elim" runat="server"
                                                ImageUrl="~/imagenes/close.png"
                                                OnClick="ElimRol" ToolTip="Eliminar rol" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server">
                <HeaderTemplate>Usuarios</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">Asignación de Usuarios</div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Nombre:</label>
                                <asp:TextBox ID="TNombreUsr" runat="server" MaxLength="80"
                                    CssClass="form-control" Width="280px" />
                            </div>
                            <div class="campo">
                                <label>RUT:</label>
                                <asp:TextBox ID="TRut" runat="server"
                                    CssClass="form-control" Width="120px" />
                            </div>                            
                            <div class="campo">
                                <label>Tipo de Búsqueda:</label>
                                <asp:RadioButtonList ID="dbTipoUser" runat="server" CssClass="TextoCheck"
                                    RepeatDirection="Horizontal" Width="280px">
                                    <asp:ListItem Value="L" Selected="True">Disponibles</asp:ListItem>
                                    <asp:ListItem Value="M">Asociados</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="campo">
                                <label>&nbsp;</label>
                                <asp:Button ID="BtBuscarUser" runat="server" Text="Buscar"
                                    OnClick="BtBuscarUser_Click" CssClass="BotonPortalAzul" />
                            </div>
                        </div>
                    </div>
                    <div class="asignacion-grid">
                        <div class="asignacion-columna">
                            <div class="asignacion-titulo">Disponibles</div>
                            <asp:GridView ID="gbUserDisp" runat="server" AutoGenerateColumns="False"
                                CssClass="grid-reloj" GridLines="None"
                                OnRowDataBound="dvUser_RowDataBound"
                                OnSelectedIndexChanged="dvUser_SelectedIndexChanged"
                                AllowPaging="True" OnPageIndexChanging="dvUser_PageIndexChanging"
                                DataKeyNames="idusuario" PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idusuario" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="rut" HeaderText="RUN">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Usuario">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Agregar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Add" runat="server"
                                                ImageUrl="~/imagenes/check.png"
                                                OnClick="AddUser" ToolTip="Agregar usuario" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
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
                            <asp:GridView ID="gbUser" runat="server" AutoGenerateColumns="False"
                                CssClass="grid-reloj" GridLines="None"
                                OnRowDataBound="gbUserDisp_RowDataBound"
                                OnSelectedIndexChanged="gbUserDisp_SelectedIndexChanged"
                                AllowPaging="True" OnPageIndexChanging="gbUserDisp_PageIndexChanging"
                                DataKeyNames="idusapp" PageSize="50"
                                EmptyDataText="No existen Resultados."
                                EmptyDataRowStyle-CssClass="textoEmpty">
                                <Columns>
                                    <asp:BoundField DataField="idusapp" HeaderText="Id" ReadOnly="True">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="rut" HeaderText="RUN">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Usuario">
                                        <ItemStyle CssClass="textoGridLeft" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_Elim" runat="server"
                                                ImageUrl="~/imagenes/close.png"
                                                OnClick="ElimUser" ToolTip="Eliminar usuario" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True" Visible="False" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
