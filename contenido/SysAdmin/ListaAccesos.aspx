<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaAccesos.aspx.cs" Inherits="contenido_SysAdmin_ListaAccesos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Accesos</title>
    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="bloque">
            <div class="titulo-seccion">
                Gestión de Accesos
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Acceso:</label>
                    <asp:TextBox ID="TxtAcceso"
                        runat="server"
                        CssClass="form-control"
                        Width="350px">
                    </asp:TextBox>
                </div>
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:CheckBox ID="bchkEli"
                        runat="server"
                        Text=" Eliminados"
                        ToolTip="Mostrar accesos eliminados"
                        CssClass="check-portal" />
                </div>
                <div class="campo">
                    <label>&nbsp;</label>
                    <asp:Button ID="btn_Buscar"
                        runat="server"
                        Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btn_Buscar_Click" />
                    </div>
                <div class="campo">
                    <asp:Button ID="btnNuevo"
                        runat="server"
                        Text="Nuevo"
                        CssClass="BotonPortalVerde"
                        OnClick="btnNuevo_Click" />
                </div>
            </div>
        </div>
        <br />
        <div class="campo">
            <asp:GridView ID="dgData"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="idapp"
                GridLines="None"
                Width="100%"
                CssClass="grid-reloj"
                AllowPaging="True"
                PageSize="20"
                AllowSorting="True"
                OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                OnRowDataBound="dgData_RowDataBound"
                OnSorting="dgData_Sorting"
                OnPageIndexChanging="dgData_PageIndexChanging"
                EmptyDataText="No existen accesos registrados."
                EmptyDataRowStyle-CssClass="textoEmpty">
                <Columns>
                    <asp:BoundField
                        DataField="idapp"
                        HeaderText="Id"
                        Visible="False"
                        ReadOnly="True" />
                    <asp:BoundField
                        DataField="codigo"
                        HeaderText="Código"
                        SortExpression="codigo"/>
                    <asp:BoundField
                        DataField="descripcion"
                        HeaderText="Descripción"
                        SortExpression="descripcion" />
                    <asp:BoundField
                        DataField="num_usr"
                        HeaderText="Usuarios"
                        SortExpression="num_usr" />
                    <asp:BoundField
                        DataField="num_rol"
                        HeaderText="Roles"
                        SortExpression="num_rol" />
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAbrir"
                                runat="server"
                                ImageUrl="~/imagenes/check.png"
                                ToolTip="Abrir y editar acceso"
                                CommandName="Select"
                                CausesValidation="false" />
                        </ItemTemplate>
                        <ItemStyle Width="45px" HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="GridGralHeader" />
                <RowStyle CssClass="GridGralRow" />
                <AlternatingRowStyle CssClass="GridGralAltRow" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
