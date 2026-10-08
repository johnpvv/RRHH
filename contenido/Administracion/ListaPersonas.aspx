<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaPersonas.aspx.cs" Inherits="contenido_Administracion_ListaPersonas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>RRHH_ListaPersonas</title>
    <script language="text/javascript" src="~/js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function mostrarSpinner() {
            document.getElementById('spinnerCarga').style.display = 'flex';
            return true;
        }
    </script>
    <link href="../../css/EstiloRRHH.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True" />
        <div class="bloque">
            <div class="titulo-seccion">Gestión Personas</div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>RUT</label>
                    <asp:TextBox ID="TxtRut" runat="server" CssClass="form-control"
                        onkeypress="KeyEnter(event)" MaxLength="9" onblur="IsInteger(this);" Width="200px" />
                </div>

                <div class="campo" style="display: flex; align-items: end; padding-bottom: 5px;">
                    <asp:CheckBox ID="bchkEli" runat="server" Text="Eliminados"
                        CssClass="TextoCheck" ToolTip="Eliminados" />
                </div>
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Nombre</label>
                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control"
                        placeholder="Nombres" onkeypress="KeyEnter(event)" MaxLength="80" Width="300px" />
                </div>
                <div class="campo">
                    <label>Paterno</label>
                    <asp:TextBox ID="TxtPaterno" runat="server" CssClass="form-control"
                        placeholder="Paterno" onkeypress="KeyEnter(event)" MaxLength="80" />
                </div>
                <div class="campo">
                    <label>Materno</label>
                    <asp:TextBox ID="TxtMaterno" runat="server" CssClass="form-control"
                        placeholder="Materno" onkeypress="KeyEnter(event)" MaxLength="80" />
                </div>
                <div class="campo">
                    <asp:Button ID="btn_Buscar" runat="server" Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btn_Buscar_Click"
                        OnClientClick="mostrarSpinner();" />
                </div>
                <div class="campo">
                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo"
                        CssClass="BotonPortalAmarillo"
                        OnClick="btnNuevo_Click"
                        OnClientClick="mostrarSpinner();" />
                </div>
            </div>
        </div>
        <div class="bloque">
            <div class="bloque-titulo">Listado de Personas</div>
            <div class="campo">
                <asp:GridView ID="dgData" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="rut"
                    GridLines="None"
                    CssClass="grid-reloj"
                    OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                    OnRowDataBound="dgData_RowDataBound"
                    OnSorting="dgData_Sorting"
                    OnPageIndexChanging="dgData_PageIndexChanging"
                    AllowPaging="True"
                    AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="rut" HeaderText="RUT" SortExpression="rut">
                            <HeaderStyle Font-Size="0px" Width="0px" />
                            <ItemStyle Font-Size="0px" Width="0px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="rut_c" HeaderText="RUT" SortExpression="rut_c">
                            <ItemStyle Font-Bold="true" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre" HeaderText="Nombres" SortExpression="nombre">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ap_paterno" HeaderText="AP. Paterno" SortExpression="ap_paterno">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ap_materno" HeaderText="AP. Materno" SortExpression="ap_materno">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="direccion">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="email" HeaderText="E-Mail" SortExpression="email">
                            <ItemStyle Width="200px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fono1" HeaderText="Teléfono Principal" SortExpression="fono1">
                            <ItemStyle Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="estado">
                            <ItemStyle Font-Bold="true" Width="120px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Editar">
                            <ItemStyle Width="40px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server"
                                    ImageUrl="../../imagenes/check.png"
                                    ToolTip="Editar"
                                    CommandName="Select"
                                    CommandArgument='<%# Eval("rut") %>'
                                    Width="25px"
                                    Height="25px"
                                    OnClientClick="mostrarSpinner();" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
            <div class="filtros-grid">
                <asp:Label ID="lblTotal" runat="server" CssClass="contador-grid" Text="0 registro(s)">
                </asp:Label>
            </div>
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
