<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaUniOperativa.aspx.cs" Inherits="contenido_Administracion_ListaUniOperativa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Centros</title>
    <script language="text/javascript" src="~/js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function KeyEnter(e) {
            if (e.keyCode == 13) {
                __doPostBack('KeyEnterPostBack', '')
            }
        }
    </script>
    <script type="text/javascript">
        function mostrarSpinner() {
            document.getElementById('spinnerCarga').style.display = 'flex';
            return true;
        }
    </script>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="bloque">
            <div class="titulo-seccion">
                Gestión Centros
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Unidad Superior</label>
                    <asp:TextBox ID="TID" runat="server"
                        CssClass="form-control"
                        MaxLength="5"
                        Width="150px"
                        onkeypress="KeyEnter(event)"
                        Style="text-transform: uppercase;" />
                </div>
                <div class="campo">
                    <label>Unidad Operativa</label>
                    <asp:TextBox ID="TServicio" runat="server"
                        CssClass="form-control"
                        MaxLength="100"
                        onkeypress="KeyEnter(event)"
                        Style="text-transform: uppercase;" />
                </div>
                <div class="campo" style="display: flex; align-items: end; padding-bottom: 5px;">
                    <asp:CheckBox ID="ckElim" runat="server"
                        Text="Eliminados"
                        CssClass="TextoCheck" />
                </div>
                <div class="botones-form">
                    <asp:Button ID="btn_Buscar" runat="server"
                        Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btn_Buscar_Click"
                        OnClientClick="mostrarSpinner();" />
                    <asp:Button ID="btnNuevo" runat="server"
                        Text="Nuevo"
                        CssClass="BotonPortalAmarillo"
                        OnClick="btnNuevo_Click" />
                </div>
            </div>
        </div>
        <div class="bloque">
            <div class="bloque-titulo">
                Listado de Centros
            </div>
            <div class="campo">
                <asp:GridView ID="dgData" runat="server"
                    DataKeyNames="CODUNIOP"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    CssClass="grid-reloj"
                    AllowPaging="True"
                    OnPageIndexChanging="dgData_PageIndexChanging"
                    OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                    PageSize="30">
                    <Columns>
                        <asp:BoundField DataField="CODUNIOP" HeaderText="ID" ReadOnly="true">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IDSUP_UNIDAD" HeaderText="ID Superior">
                            <ItemStyle Width="90px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UNIDAD_SUPERIOR" HeaderText="Unidad Superior"></asp:BoundField>
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Unidad Operativa"></asp:BoundField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar"
                                    runat="server"
                                    ImageUrl="~/imagenes/edit.png"
                                    CommandName="Select"
                                    ToolTip="Editar"
                                    Width="24px"
                                    Height="24px" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Elim.">
                            <ItemTemplate>
                                <asp:ImageButton ID="btn_EliCla"
                                    runat="server"
                                    ImageUrl="~/imagenes/close.png"
                                    OnClick="ElimClasif"
                                    ToolTip="Eliminar"
                                    OnClientClick="mostrarSpinner();" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rehab.">
                            <ItemTemplate>
                                <asp:ImageButton ID="btn_RehaCla"
                                    runat="server"
                                    ImageUrl="~/imagenes/check.png"
                                    OnClick="RehabClasif"
                                    ToolTip="Rehabilitar"
                                    OnClientClick="mostrarSpinner();" />
                            </ItemTemplate>
                            <HeaderStyle Width="60px" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:CommandField
                            ShowSelectButton="true" ButtonType="Link" Visible="false" SelectText="Enroll" />
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
        <div class="filtros-grid">
            <asp:Label ID="lblTotal" runat="server" CssClass="contador-grid" Text="0 registro(s)">
            </asp:Label>
        </div>
        <div class="botones-form">
            <asp:Button ID="btnExportarExcel" runat="server"
                Text="Exportar a Excel"
                CssClass="BotonPortalVerde"
                OnClick="btnExportarExcel_Click" />
            <asp:Button ID="btnVolver" runat="server"
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
