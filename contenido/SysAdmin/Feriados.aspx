<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feriados.aspx.cs" Inherits="contenido_SysAdmin_Feriados" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Firmas</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <link runat="server" href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" id="Link1" />
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
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True" />
        <div class="titulo-seccion">Gestión Feriados</div>
        <div class="bloque">
            <div class="filtros-grid">
                <div class="campo">
                    <label>Año:</label>
                    <asp:DropDownList ID="ddlAnio" runat="server" Width="100px"
                        CssClass="form-control"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAnio_SelectedIndexChanged"
                        onchange="mostrarSpinner();">
                    </asp:DropDownList>
                </div>
                <div class="campo">
                    <label>Fecha:</label>
                    <asp:TextBox ID="TFecha" runat="server"
                        CssClass="form-control"
                        Width="120px"
                        MaxLength="10" />
                    <ajaxToolkit:CalendarExtender ID="calFecha" runat="server"
                        TargetControlID="TFecha"
                        Format="dd/MM/yyyy" />
                </div>
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:Button ID="ImBtIngresar" runat="server"
                        Text="+ Agregar"
                        CssClass="BotonPortalAzul"
                        OnClick="ImBtIngresar_Click"
                        OnClientClick="mostrarSpinner();" />
                </div>
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:Button ID="btn_Buscar" runat="server"
                        OnClick="btn_Buscar_Click"
                        CssClass="BotonPortalVerde"
                        OnClientClick="mostrarSpinner();"
                        Text="Buscar" />
                </div>
            </div>
        </div>
        <div class="filaCenter">
            <div class="campo">
                <asp:GridView ID="dgData" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="IDFERIADO"
                    CssClass="grid-reloj"
                    GridLines="None"
                    AllowPaging="True"
                    PageSize="40"
                    Width="40%"
                    OnPageIndexChanging="dgData_PageIndexChanging"
                    EmptyDataText="No Hay Datos para mostrar...">
                    <Columns>
                        <asp:BoundField DataField="IDFERIADO" HeaderText="ID" HeaderStyle-Font-Size="0px" ItemStyle-Font-Size="0px" HeaderStyle-Width="0px" />
                        <asp:BoundField DataField="FECHA" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="textoGridBoldLeft" />
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <asp:ImageButton ID="btn_EliCla" runat="server"
                                    ImageUrl="~/imagenes/close.png"
                                    ToolTip="Eliminar feriado"
                                    CommandArgument='<%# Eval("IDFERIADO") %>'
                                    OnClick="btn_EliCla_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar este feriado?');" />
                            </ItemTemplate>
                            <ItemStyle Width="40px" HorizontalAlign="center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="filtros-grid">
            <asp:Label ID="lblTotal" runat="server" Text="0" CssClass="contador-grid" />
        </div>
        <div class="botones-form">
            <asp:Button ID="btn_Volver" runat="server" Text="Volver"
                CssClass="BotonPortalGris" OnClick="btnVolver_Click" />
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
