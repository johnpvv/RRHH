<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionRoles.aspx.cs" Inherits="contenido_SysAdmin_GestionRoles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Selecting GridView Row</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>


    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />


    <style type="text/css">
        .modalbackgbround {
            background-color: Black;
            filter: alpha(opacity=25);
            opacity: 0.8;
            z-index: 10000;
        }

        .Titulo {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font: bold;
            text-align: center;
            background-color: #CCFFFF;
            font-size: 14px;
        }

        .Titulo2 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font: bold;
            text-align: center;
            background-color: #CCFFFF;
            font-size: 12px;
        }

        .TextoRigth {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font: bold;
        }

        .TextoRigthGrilla {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font: bold;
        }


        .TextoLeft {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            margin-left: 0px;
        }

        .TextoLeftGrilla {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
        }

        .TextoCenter {
            text-align: center;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font: bold;
        }

        .TextoCenterGrilla {
            text-align: center;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            font: bold;
        }


        .TextoForm {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font: bold;
        }

        .TextoCheck {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            align: left;
            width: 1197px;
        }




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

        .style4 {
            width: 106px;
        }

        .style12 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            width: 578px;
        }

        .style13 {
            width: 578px;
        }

        .style21 {
            width: 877px;
        }

        .style27 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 313px;
        }

        .style28 {
            width: 313px;
        }

        .style30 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 710px;
        }

        .style31 {
            width: 710px;
        }

        .auto-style1 {
            width: 561px;
        }

        .auto-style44 {
            width: 1008px;
            height: 328px;
        }

        .auto-style45 {
            width: 689px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <p />
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <p />
        <table style="width: 1000px;">
            <tr>
                <td class="auto-style1">Gestión Roles<label>&nbsp; --&gt; Detalle --&gt;
                        <asp:Label ID="LbTitulo" runat="server" Text="Label"></asp:Label>
                </label>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImgBtnBack" runat="server" ImageUrl="~/imagenes/back.png"
                        OnClick="ImgBtnBack_Click" Style="width: 24px; text-align: right" />
                </td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="500px" Width="1000px"
            Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto"
            ActiveTabIndex="0">
            <ajaxToolkit:TabPanel runat="server" ID="TabPanel1" HeaderText="Prueba4" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                <HeaderTemplate>Detalle Rol</HeaderTemplate>
                <ContentTemplate>
                    <table border="1" style="width: 958px">
                        <tr>
                            <td class="auto-style44">
                                <table width="1020" border="0" style="width: 853px">
                                    <tr>
                                        <td class="style1" style="text-align: left">
                                            <label>&#160;</label><asp:Button
                                                ID="Button1" runat="server"
                                                Text="Agregar" OnClick="BtnAgregar_Click" Style="height: 26px" />

                                        </td>
                                        <td class="style2" style="text-align: left">
                                            <asp:Button ID="btnNuevo" runat="server" OnClick="btnNuevo_Click"
                                                Text="Nuevo" /></td>
                                        <td class="style3" style="text-align: left">
                                            <asp:Button ID="btnEliminar" runat="server"
                                                Text="Eliminar" OnClick="btnEliminar_Click" />
                                        </td>
                                        <td class="style4" style="text-align: left">
                                            <asp:Button ID="btnRehabilitar" runat="server"
                                                Text="Rehabilitar" OnClick="btnRehabilitar_Click" />

                                        </td>
                                        <td width="451" style="text-align: left; width: 225px"></td>

                                    </tr>

                                </table>
                                <table border="0" style="width: 849px">
                                    <tr>
                                        <td width="170">&nbsp;</td>
                                        <td class="auto-style2">&nbsp;</td>
                                        <td class="auto-style4">&nbsp;</td>
                                        <td width="144">&nbsp;</td>
                                        <td width="93">&nbsp;</td>
                                        <td width="235">&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">Código:</td>
                                        <td class="auto-style3">
                                            <asp:TextBox ID="TCodigo" runat="server" MaxLength="10"></asp:TextBox>
                                        </td>
                                        <td class="auto-style5">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                        <td class="TextoRigth">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Nombre:</td>
                                        <td colspan="5" class="TextoLeft">
                                            <asp:TextBox ID="TNombre" runat="server" Height="24px" Width="544px" MaxLength="80"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" style="width: 850px">
                                    <tr>
                                        <td class="TextoRigth">Observaciones:</td>
                                        <td class="auto-style7"></td>
                                        <td class="auto-style8"></td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td class="auto-style2">
                                            <asp:TextBox runat="server" MaxLength="400" TextMode="MultiLine" Height="46px" Width="592px" ID="TObser"></asp:TextBox>
                                        </td>
                                        <td class="auto-style4">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>


            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>Accesos</HeaderTemplate>
                <ContentTemplate>
                    <table border="0">
                        <tr>
                            <td width="132">
                                <label>&#160;</label><asp:Button ID="btn_Buscar" runat="server"
                                    Text="Buscar" OnClick="btn_Buscar_Click" />

                            </td>
                            <td width="214">&#160;</td>
                            <td width="195">&nbsp;</td>
                            <td width="451">&nbsp;</td>

                        </tr>

                        <tr>
                            <td width="132">
                                <asp:RadioButtonList ID="rbTipo" runat="server" CssClass="TextoCheck" RepeatDirection="Horizontal" Width="279px">
                                    <asp:ListItem Value="L">Lectura</asp:ListItem>
                                    <asp:ListItem Value="M">Modificación</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="214">&nbsp;</td>
                            <td width="195">&nbsp;</td>
                            <td width="451">&nbsp;</td>
                        </tr>

                    </table>


                    <table border="0" style="width: 925px">
                        <tr>
                            <td class="TextoCenter">Disponibles</td>
                            <td align="left" class="style1">&nbsp;</td>
                            <td class="TextoCenter">Asociados</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="1">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gdArt" runat="server" AutoGenerateColumns="False"
                                                Font-Names="Tahoma" Font-Size="Small" ForeColor="#333333" GridLines="None"
                                                OnRowDataBound="gdArt_RowDataBound"
                                                OnSelectedIndexChanged="gdArt_SelectedIndexChanged" Width="390px"
                                                AllowPaging="True" OnPageIndexChanging="gdArt_PageIndexChanging"
                                                DataKeyNames="idapp" PageSize="50">
                                                <Columns>
                                                    <asp:BoundField DataField="idapp" HeaderText="Id" ReadOnly="True">
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
                                                            <asp:ImageButton ID="btn_Add" runat="server" ImageUrl="~/imagenes/check.png"
                                                                OnClick="AddRol" />

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                                    </asp:TemplateField>

                                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True"
                                                        Visible="False" />
                                                </Columns>
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" class="style1">&nbsp;</td>
                            <td valign="top">
                                <table border="1">
                                    <tr>
                                        <td class="auto-style45">
                                            <asp:GridView ID="gbArtSer" runat="server" AutoGenerateColumns="False"
                                                Font-Names="Tahoma" Font-Size="Small" ForeColor="#333333" GridLines="None"
                                                OnRowDataBound="gbArtSer_RowDataBound"
                                                OnSelectedIndexChanged="gbArtSer_SelectedIndexChanged" Width="494px"
                                                AllowPaging="True" OnPageIndexChanging="gbArtSer_PageIndexChanging"
                                                DataKeyNames="idrolapp" PageSize="50">
                                                <Columns>
                                                    <asp:BoundField DataField="idrolapp" HeaderText="Id" ReadOnly="True">
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
                                                            <asp:ImageButton ID="btn_Elim" runat="server" ImageUrl="~/imagenes/close.png"
                                                                OnClick="ElimRol" />

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                                    </asp:TemplateField>

                                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True"
                                                        Visible="False" />
                                                </Columns>
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="style1">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>
                    Usuarios
                </HeaderTemplate>
                <ContentTemplate>

                    <table border="0" style="width: 811px">
                        <tr>
                            <td width="170">
                                <asp:Button ID="BtBuscarUser" runat="server" Text="Buscar" OnClick="BtBuscarUser_Click" />
                            </td>
                            <td width="147">&nbsp;</td>
                            <td class="auto-style5">&nbsp;</td>
                            <td class="auto-style3">&nbsp;</td>
                            <td width="93">&nbsp;</td>
                            <td width="235">&nbsp;</td>

                        </tr>
                        <tr>
                            <td class="TextoRigth">Nombre:</td>
                            <td colspan="5" class="TextoLeft">
                                <asp:TextBox ID="TNombreUsr" runat="server" Height="24px" Width="307px" MaxLength="80"></asp:TextBox></td>

                        </tr>
                        <tr>
                            <td class="TextoRigth">Rut:</td>
                            <td class="TextoLeft">
                                <asp:TextBox ID="TRut" runat="server"></asp:TextBox></td>
                            <td class="auto-style2">
                                <p>&nbsp;</p>
                            </td>
                            <td class="auto-style4">&#160;</td>
                            <td class="TextoRigth">&nbsp;</td>
                            <td class="TextoLeft">&#160;</td>

                        </tr>

                    </table>

                    <table border="0" style="width: 810px">
                        <tr>
                            <td width="132">
                                <label>&#160;</label></td>
                            <td width="214">&#160;</td>
                            <td width="195">&nbsp;</td>
                            <td width="451">&nbsp;</td>

                        </tr>

                        <tr>
                            <td width="132">
                                <asp:RadioButtonList ID="dbTipoUser" runat="server" CssClass="TextoCheck" RepeatDirection="Horizontal" Width="279px">
                                    <asp:ListItem Value="L" Selected="True">Lectura</asp:ListItem>
                                    <asp:ListItem Value="M">Modificación</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="214">&nbsp;</td>
                            <td width="195">&nbsp;</td>
                            <td width="451">&nbsp;</td>
                        </tr>
                    </table>


                    <table border="0" style="width: 925px">
                        <tr>
                            <td class="TextoCenter">Disponibles</td>
                            <td align="left" class="style1">&nbsp;</td>
                            <td class="TextoCenter">Asociados</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="1">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gbUserDisp" runat="server" AutoGenerateColumns="False"
                                                Font-Names="Tahoma" Font-Size="Small" ForeColor="#333333" GridLines="None"
                                                OnRowDataBound="dvUser_RowDataBound"
                                                OnSelectedIndexChanged="dvUser_SelectedIndexChanged" Width="390px"
                                                AllowPaging="True" OnPageIndexChanging="dvUser_PageIndexChanging"
                                                DataKeyNames="idusuario" PageSize="100">
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
                                                            <asp:ImageButton ID="btn_Add" runat="server" ImageUrl="~/imagenes/check.png"
                                                                OnClick="AddUser" />

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                                    </asp:TemplateField>

                                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True"
                                                        Visible="False" />
                                                </Columns>
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left" class="style1">&nbsp;</td>
                            <td valign="top">
                                <table border="1">
                                    <tr>
                                        <td class="auto-style45">
                                            <asp:GridView ID="gbUser" runat="server" AutoGenerateColumns="False"
                                                Font-Names="Tahoma" Font-Size="Small" ForeColor="#333333" GridLines="None"
                                                OnRowDataBound="gbUserDisp_RowDataBound"
                                                OnSelectedIndexChanged="gbUserDisp_SelectedIndexChanged" Width="494px"
                                                AllowPaging="True" OnPageIndexChanging="gbUserDisp_PageIndexChanging"
                                                DataKeyNames="idusrol" PageSize="50">
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
                                                            <asp:ImageButton ID="btn_Elim" runat="server" ImageUrl="~/imagenes/close.png"
                                                                OnClick="ElimUser" />

                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                                    </asp:TemplateField>

                                                    <asp:CommandField SelectText="Enroll" ShowSelectButton="True"
                                                        Visible="False" />
                                                </Columns>
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="style1">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    </td>
                       </tr>
                    </table>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>

