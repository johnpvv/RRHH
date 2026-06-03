<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaUsuarios.aspx.cs" Inherits="contenido_SysAdmin_ListaUsuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Artículos</title>
    <script language="text/javascript" src="~/js/common.js" type="text/javascript"></script>

    <script type="text/javascript">

        function KeyEnter(e) {
            if (e.keyCode == 13) {
                __doPostBack('KeyEnterPostBack', '')
            }

        }
    </script>

    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body, html {
            font-family: Tahoma;
            font-size: small;
        }

        .Normal {
            background-color: #EFF3FB;
            cursor: hand;
        }

            .Normal:Hover, .Alternate:Hover {
                background-color: #D1DDF1;
                cursor: hand;
            }

        .Alternate {
            background-color: White;
            cursor: hand;
        }

        .style3 {
            height: 20px;
        }

        .auto-style1 {
            width: 156px;
        }

        .auto-style2 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 156px;
        }

        .auto-style3 {
            width: 986px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
        </asp:ScriptManager>
        <table border="0" style="width: 69%">
            <tr>
                <td class="TextoLeft">
                    <label>
                        Gestión Usuarios --&gt;</label></td>
                <td></td>
            </tr>
        </table>
        <table width="90%" border="1">
            <tr>
                <td class="auto-style3">
                    <table border="0" style="width: 86%">
                        <tr>
                            <td width="132">
                                <label>
                                    &nbsp;</label><asp:Button ID="btn_Buscar" runat="server" OnClick="btn_Buscar_Click"
                                        Text="Buscar" />
                            </td>
                            <td width="214">
                                <asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click"
                                    Text="Nuevo" />
                            </td>
                            <td width="195">&nbsp;</td>
                            <td width="451">&nbsp;</td>
                        </tr>
                    </table>
                    <table border="0" style="width: 932px">
                        <tr>
                            <td class="auto-style2">Nombre:</td>
                            <td class="TextoLeft">
                                <p>
                                    <asp:TextBox ID="TxtNombre" onkeypress="KeyEnter(event)" runat="server" MaxLength="80" Width="327px"></asp:TextBox>
                                </p>
                            </td>
                            <td class="TextoLeft">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Rut:</td>
                            <td class="TextoLeft">
                                <asp:TextBox ID="TxtRut" runat="server" onkeypress="KeyEnter(event)" MaxLength="9" onblur="IsInteger(this);"></asp:TextBox>
                            </td>
                            <td class="TextoLeft">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">&nbsp;</td>
                            <td class="TextoLeft">
                                <asp:CheckBox ID="bchkEli" runat="server" Text="Eliminado"
                                    ToolTip="Eliminado" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <hr style="width: 90%" />
        <asp:GridView ID="dgData" runat="server"
            AutoGenerateColumns="False" DataKeyNames="idusuario"
            GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged"
            OnRowDataBound="dgData_RowDataBound" Width="1127px" OnSorting="dgData_Sorting"
            AllowPaging="True" OnPageIndexChanging="dgData_PageIndexChanging" Style="margin-right: 0px" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="idusuario" HeaderText="Id" ShowHeader="False"
                    ReadOnly="True" Visible="False">
                    <HeaderStyle CssClass="Titulo2" Width="50px" />
                    <ItemStyle CssClass="TextoCenter" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="rut" HeaderText="Rut" SortExpression="rut">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>

                <asp:BoundField DataField="email" HeaderText="email" SortExpression="email">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>


                <asp:CommandField ShowSelectButton="true" ButtonType="Link" Visible="false" SelectText="Enroll" />
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="black" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>

        <br />
        <br />



    </form>
</body>
</html>
