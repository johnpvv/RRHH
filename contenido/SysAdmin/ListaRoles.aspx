<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaRoles.aspx.cs" Inherits="contenido_SysAdmin_ListaRoles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Roles</title>
    <link href="~/css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="bloque">
            <div class="titulo-seccion">
                Gestión Roles
            </div>
            <div class="filtros-grid">
                <div class="campo">
                    <label>Roles:</label>
                    <asp:TextBox ID="txtBNom" runat="server"
                        CssClass="form-control"
                        Width="455px">
                    </asp:TextBox>
                </div>
                <div class="campo">
                    <asp:CheckBox ID="bchkEli" runat="server"
                        Text="Eliminado"
                        ToolTip="Eliminado"
                        CssClass="TextoCheck" />
                </div>

                <div class="campo">
                    <asp:Button ID="btn_Buscar" runat="server"
                        Text="Buscar"
                        CssClass="BotonPortalAzul"
                        OnClick="btn_Buscar_Click" />
                </div>
                <div class="campo">
                    <asp:Button ID="btnNuevo" runat="server"
                        Text="Nuevo"
                        CssClass="BotonPortalVerde"
                        OnClick="btnNuevo_Click" />
                </div>
            </div>
        </div>
        <div class="bloque">
            <div class="campo">
                <asp:GridView ID="dgData" runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="idrol"
                    GridLines="None"
                    CssClass="grid-reloj"
                    OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                    OnRowDataBound="dgData_RowDataBound"
                    OnSorting="dgData_Sorting"
                    AllowPaging="True"
                    OnPageIndexChanging="dgData_PageIndexChanging"
                    AllowSorting="True"
                    PageSize="30">
                    <Columns>
                        <asp:BoundField DataField="idrol"
                            HeaderText="Id"
                            ShowHeader="False"
                            ReadOnly="True"
                            Visible="False">
                            <HeaderStyle CssClass="Titulo2" Width="50px" />
                            <ItemStyle CssClass="TextoCenter" Width="10px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="codigo"
                            HeaderText="Código Rol"
                            SortExpression="codigo">
                            <ItemStyle Width="100px" Font-Bold="true" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion"
                            HeaderText="Descripcion"
                            SortExpression="descripcion">
                            <ItemStyle Width="300px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="num_usr"
                            HeaderText="Nº Usr."
                            SortExpression="num_usr">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="num_rol"
                            HeaderText="Nº App."
                            SortExpression="num_rol">
                            <ItemStyle Width="70px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Acción">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnAbrir"
                                    runat="server"
                                    ImageUrl="~/imagenes/check.png"
                                    ToolTip="Abrir y editar Rol"
                                    CommandName="Select"
                                    CausesValidation="false" />
                            </ItemTemplate>
                            <ItemStyle Width="30px" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="GridGralHeader" />
                    <RowStyle CssClass="GridGralRow" />
                    <AlternatingRowStyle CssClass="GridGralAltRow" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
