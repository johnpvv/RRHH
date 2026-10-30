<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListarRelojes.aspx.cs" Inherits="contenido_Administracion_ListarRelojes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
    <title>Relojes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="bloque">
            <div class="titulo-seccion">Gestión de Relojes</div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Dirección IP:</label>
                    <asp:TextBox ID="txtIp" runat="server" CssClass="form-control" MaxLength="100" TextMode="Search" />
                </div>
                <div class="campo">
                    <label>Serie:</label>
                    <asp:TextBox ID="txtSerie" runat="server" CssClass="form-control" Width="100px" TextMode="Search"/>
                </div>
                <div class="campo">
                    <label>Nombre Reloj:</label>
                    <asp:TextBox ID="txtDescr" runat="server" CssClass="form-control" MaxLength="800" TextMode="Search" />
                </div>
                <div class="campo">
                    <asp:Button ID="btnBuscar" runat="server"
                        Text="Buscar" CssClass="BotonPortalAzul"
                        OnClick="btnBuscar_Click" />
                </div>
                <div class="campo">
                    <asp:Button ID="btnNuevo" runat="server"
                        Text="Nuevo" CssClass="BotonPortalAmarillo"
                        OnClick="btnNuevo_Click" />
                </div>
            </div>
        </div>
        <div class="bloque">
            <div class="bloque-titulo">Listado de Relojes</div>
            <div class="campo">
                <asp:GridView ID="dgData" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="grid-reloj"
                    GridLines="None"
                    DataKeyNames="IDRELOJ"
                    EmptyDataText="No existen relojes para los filtros seleccionados."
                    OnRowDataBound="dgData_RowDataBound"
                    OnSelectedIndexChanged="dgData_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="IDRELOJ" HeaderText="ID" />
                        <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripción" />
                        <asp:BoundField DataField="IP" HeaderText="Dirección IP" />
                        <asp:BoundField DataField="PUERTO" HeaderText="Puerto" />
                        <asp:BoundField DataField="SERIE" HeaderText="Serie" />
                        <asp:BoundField DataField="ESTADO" HeaderText="Estado" />
                        <asp:BoundField DataField="CENTRO" HeaderText="Centro asignado" />
                        <asp:BoundField DataField="F_H_CREACION" HeaderText="Fecha de creación"
                            DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Editar">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEditar" runat="server"
                                    ImageUrl="~/imagenes/edit.png"
                                    ToolTip="Editar reloj"
                                    CommandName="Select"
                                    CausesValidation="false"
                                    Width="20px" Height="20px" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="botones-form">
            <asp:Button ID="btnExportar" runat="server"
                Text="Exportar Excel"
                CssClass="BotonPortalVerde"
                OnClick="btnExportar_Click"
                CausesValidation="false" />
            <asp:Button ID="btnVolver" runat="server"
                Text="Volver"
                CssClass="BotonPortalGris"
                OnClick="btnVolver_Click"
                formnovalidate="formnovalidate" />
        </div>
    </form>
</body>
</html>
