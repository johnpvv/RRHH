<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionSincronizacion.aspx.cs" Inherits="contenido_GestionRRHH_GestionSincronizacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Gestion Sincronizacion</title>
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="100%" Width="100%" ScrollBars="Auto" ActiveTabIndex="0">
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1">
                <HeaderTemplate>Gestion Sincronización</HeaderTemplate>
                <ContentTemplate>
                    <div class="bloque">
                        <div class="titulo-seccion">
                            Gestion Sincronizacion           
                        </div>
                        <div class="filtros-grid">
                            <div class="campo">
                                <label>Centro / Unidad:</label>
                                <asp:DropDownList ID="ddlCentroAdmin"
                                    runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlCentroAdmin_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="campo">
                                <label>Reloj:</label>
                                <asp:DropDownList ID="ddlRelojAdmin"
                                    runat="server"
                                    CssClass="form-control"
                                    AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlRelojAdmin_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>

                            <div class="campo">
                                <label>Código reloj:</label>
                                <asp:TextBox ID="txtFiltroCodigo" runat="server" CssClass="form-control" Width="100px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>Nombre:</label>
                                <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" Width="200px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <label>RUT:</label>
                                <asp:TextBox ID="txtFiltroRut" runat="server" CssClass="form-control" Width="120px">
                                </asp:TextBox>
                            </div>
                            <div class="campo">
                                <asp:Button ID="btnBuscarEquivalencias"
                                    runat="server"
                                    Text="Buscar"
                                    CssClass="BotonPortalAzul"
                                    OnClick="btnBuscarEquivalencias_Click" />
                            </div>
                        </div>
                        <br />
                        <asp:GridView ID="dgEquivalencias"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="grid-reloj"
                            GridLines="None"
                            OnRowCommand="dgEquivalencias_RowCommand"
                            EmptyDataText="No existen equivalencias con los filtros aplicados."
                            EmptyDataRowStyle-CssClass="bloque-titulo">
                            <Columns>
                                <asp:BoundField DataField="IDUSRELOJ" HeaderText="Código Reloj" />
                                <asp:BoundField DataField="NOMBRE_TRAB_RELOJ" HeaderText="Nombre Trabajador Reloj" />
                                <asp:BoundField DataField="NOMBRE_RELOJ" HeaderText="Nombre Reloj" />
                                <asp:TemplateField HeaderText="(Hacia)">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnHacia"
                                            runat="server"
                                            ImageUrl="~/imagenes/avanzar.png"
                                            ToolTip="Hacia" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="RUT_C" HeaderText="RUT" />
                                <asp:BoundField DataField="NOMBRE_RRHH" HeaderText="Trabajador RRHH" />
                                <asp:BoundField DataField="FECHA" HeaderText="Fecha Registro" />
                                <asp:TemplateField HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminarEquivalencia"
                                            runat="server"
                                            ImageUrl="~/imagenes/close.png"
                                            CommandName="Eliminar"
                                            CommandArgument='<%# Eval("IDUSRRELOJ") %>'
                                            ToolTip="Desactivar equivalencia"
                                            OnClientClick="return confirm('¿Está seguro de desactivar esta equivalencia?');" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px" HorizontalAlign="center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
