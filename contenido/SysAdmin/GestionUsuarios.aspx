<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionUsuarios.aspx.cs" Inherits="contenido_SysAdmin_GestionUsuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Selecting GridView Row</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>


    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        function ExisteRut() {
            __doPostBack('ExisteRutPostBack', '')
        }

    </script>
    <style type="text/css">
        .autocomplete_completionListElement {
            visibility: hidden;
            margin: 0px;
            background-color: inherit;
            color: windowtext;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            overflow: auto;
            height: 200px;
            text-align: left;
            font-size: 10px;
            font-family: Verdana;
            list-style-type: none;
        }

        .autocomplete_highlightedListItem {
            background-color: #ffff99;
            color: black;
            padding: 1px;
            cursor: pointer;
        }

        .autocomplete_listItem {
            background-color: window;
            color: windowtext;
            padding: 1px;
        }

        .PromptCSS {
            color: black;
            font-family: Verdana;
            font-size: 12px;
            font-weight: bold;
            background-color: AliceBlue;
            height: 25px;
        }

        .listado {
            margin: 0px;
            border: buttonshadow;
            border-width: 1px;
            border-style: solid;
            text-align: left;
            font-size: 11px;
            font-family: Verdana;
        }
    </style>

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

        .auto-style52 {
            width: 1010px;
        }

        .auto-style55 {
            width: 108px;
        }

        .auto-style56 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 108px;
        }

        .auto-style57 {
            width: 79px;
        }

        .auto-style58 {
            width: 249px;
        }

        .auto-style59 {
            width: 190px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <p />
        <table style="width: 594px;">
            <tr>
                <td class="auto-style1">Gestión
                        <label>
                            Usuarios --&gt; Antecedentes --&gt;
                        <asp:Label ID="LbTitulo" runat="server" Text="Label"></asp:Label>
                        </label>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImgBtnBack" runat="server" ImageUrl="~/imagenes/back.png"
                        OnClick="ImgBtnBack_Click" Style="width: 24px; text-align: right" />
                </td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="500px" Width="995px"
            Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto"
            ActiveTabIndex="0">

            <ajaxToolkit:TabPanel runat="server" ID="TabPanel4" HeaderText="Prueba4" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                <HeaderTemplate>Detalle Usuarios</HeaderTemplate>
                <ContentTemplate>
                    <table border="1" style="width: 942px">
                        <tr>
                            <td class="auto-style52">

                                <table border="0" style="width: 951px">
                                    <tr>
                                        <td class="auto-style57">
                                            <asp:Button ID="btn_Agregar" runat="server" Text="Agregar" OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" OnClick="btn_Agregar_Click" />
                                        </td>
                                        <td class="auto-style58">
                                            <asp:Button ID="btn_habilitar" runat="server" OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" Text="Habilitar" OnClick="btn_habilitar_Click" Width="154px" />
                                        </td>
                                        <td class="auto-style59">
                                            <asp:Button ID="btn_ReIniciar" runat="server" OnClick="btn_ReIniciar_Click" OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" Text="ReIniciar Clave" Width="136px" />
                                        </td>
                                        <td width="75">
                                            <asp:Button ID="btn_Acceso" runat="server" OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" Text="Limpiar Acceso" Width="136px" OnClick="btn_Acceso_Click" />
                                        </td>
                                        <td width="75">&nbsp;</td>
                                    </tr>
                                </table>

                                <table border="0" style="width: 951px">
                                    <tr>
                                        <td>Accesos:</td>
                                        <td>
                                            <asp:CheckBox ID="chkMed" runat="server" />Medico
                                <asp:CheckBox ID="chkfarm" runat="server" />Farmacia
                                <asp:CheckBox ID="chkInf" runat="server" />Infectologia
                                <asp:CheckBox ID="chkSal" runat="server" Visible="False" />
                                        </td>
                                        <td></td>
                                    </tr>
                                </table>

                                <table border="0" style="width: 951px">
                                    <tr>
                                        <td class="auto-style56">Estado:</td>
                                        <td class="TextoLeft">
                                            <asp:Label ID="lbEstado" runat="server"></asp:Label>

                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style56">Rut:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtRut" runat="server" MaxLength="9" onblur="IsInteger(this);" Width="141px" OnTextChanged="TxtRut_TextChanged"></asp:TextBox>
                                            &nbsp;&nbsp;
                                    <asp:TextBox ID="TxtDv" runat="server" MaxLength="1" Width="23px" Required="true"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style56">Nombre:</td>
                                        <td colspan="4" class="TextoLeft">
                                            <asp:TextBox ID="TxtNombre" runat="server" Width="499px" MaxLength="80" Required="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style56">Anexo:</td>
                                        <td class="TextoLeft">

                                            <asp:TextBox ID="TxtAnexo" runat="server" Width="141px"></asp:TextBox>

                                        </td>
                                        <td class="TextoRigth">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style56">Email:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtEmail" type="email" runat="server" Width="334px" Required="true"></asp:TextBox>
                                        </td>
                                        <td class="TextoRigth">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style56">Especialidad:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TEspecialidad" runat="server" MaxLength="80" Required="true" Width="499px"></asp:TextBox>
                                        </td>
                                        <td class="TextoRigth">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                        <td class="TextoLeft">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style56">Descripción:</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style55">&nbsp;</td>
                                        <td class="TextoLeft" colspan="3">
                                            <asp:TextBox ID="TxtDesc" runat="server" Height="46px" Width="592px"
                                                MaxLength="400" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style55">&nbsp;</td>
                                        <td>
                                            <asp:HiddenField ID="hdIdentificador" runat="server" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>

                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>Unidades Operativas</HeaderTemplate>
                <ContentTemplate>
                    <table width="1020" border="1" style="width: 929px; height: 214px">
                        <tr>
                            <td class="auto-style34">

                                <table border="0" style="width: 810px">
                                    <tr>
                                        <td width="132">
                                            <label>&#160;</label><asp:Button ID="btn_Buscar" runat="server"
                                                Text="Buscar" OnClick="btn_Buscar_Click" />

                                        </td>
                                        <td width="214">
                                            <asp:Button ID="btn_AsigAll" runat="server" Text="Asignar Todas" OnClick="btn_AsigAll_Click" />
                                        </td>
                                        <td width="195">
                                            <asp:Button ID="btn_Limpiar" runat="server" Text="Limpiar" OnClick="btn_Limpiar_Click" />
                                        </td>
                                        <td width="451">
                                            <asp:TextBox ID="TxtIdMatElim" runat="server" ReadOnly="True" Visible="False" Width="58px"></asp:TextBox>
                                            <asp:TextBox ID="TxtIdMat" runat="server" ReadOnly="True" Visible="False" Width="58px"></asp:TextBox>
                                        </td>

                                    </tr>

                                </table>
                                <table border="0" style="width: 811px">
                                    <tr>
                                        <td width="170">&nbsp;</td>
                                        <td width="147">&nbsp;</td>
                                        <td class="auto-style5">&nbsp;</td>
                                        <td class="auto-style3">&nbsp;</td>
                                        <td width="93">&nbsp;</td>
                                        <td width="235">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Unidad:</td>
                                        <td colspan="5" class="TextoLeft">
                                            <asp:TextBox ID="bTxtUnidad" runat="server" Height="24px" Width="307px" MaxLength="80"></asp:TextBox></td>
                                    </tr>

                                </table>


                                <table border="0" style="width: 925px">
                                    <tr>
                                        <td class="TextoCenter">Unidades</td>
                                        <td align="left" class="style1">&nbsp;</td>
                                        <td class="TextoCenter">Unidades Asignadas</td>
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
                                                            DataKeyNames="CODUNIOP">
                                                            <Columns>
                                                                <asp:BoundField DataField="CODUNIOP" HeaderText="Id" ReadOnly="True">
                                                                    <ItemStyle CssClass="TextoCenter" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="UNIDAD">
                                                                    <ItemStyle CssClass="TextoCenterutr" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="IDESTADO" HeaderText="IDESTADO">
                                                                    <ItemStyle CssClass="TextoLeft" />
                                                                </asp:BoundField>

                                                                <asp:TemplateField HeaderText="Add">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btn_Add" runat="server" ImageUrl="~/imagenes/check.png"
                                                                            OnClick="AddUnidad" />

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
                                                            DataKeyNames="IDUSERUNID">
                                                            <Columns>
                                                                <asp:BoundField DataField="IDUSERUNID" HeaderText="Id" ReadOnly="True">
                                                                    <ItemStyle CssClass="TextoCenter" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESCRIPCION" HeaderText="Unidad">
                                                                    <ItemStyle CssClass="TextoCenter" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="Valida" HeaderText="Valida">
                                                                    <ItemStyle CssClass="TextoLeft" />
                                                                </asp:BoundField>
                                                                <asp:TemplateField HeaderText="Val.">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btn_Valida" runat="server" ImageUrl="~/imagenes/check.png"
                                                                            OnClick="ValidaUnidad" />

                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="50px" />
                                                                    <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />

                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Elim">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="btn_Elim" runat="server" ImageUrl="~/imagenes/close.png"
                                                                            OnClick="ElimUnidad" />

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