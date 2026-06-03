<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Constantes.aspx.cs" Inherits="contenido_SysAdmin_Constantes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Firmas</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>


    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />
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

        .style5 {
            width: 162px;
        }

        .style6 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 162px;
        }

        .auto-style2 {
            width: 1020px;
        }

        .style8 {
            width: 344px;
        }

        .auto-style3 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 26px;
        }

        .auto-style4 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 291px;
            height: 26px;
        }

        .auto-style5 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 162px;
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">

            <asp:ScriptManager ID="ToolkitScriptManager1" EnableScriptGlobalization="True" runat="server">
            </asp:ScriptManager>

            <table style="width: 80%;">
                <tr>
                    <td class="TextoLeft">Gestión Firmas</td>
                </tr>
            </table>

            <table width="80%" border="1">
                <tr>
                    <td class="auto-style2">
                        <table width="100%" border="0">
                            <tr>
                                <td class="style8">
                                    <label>
                                        &nbsp;</label><asp:Button ID="btn_Buscar" runat="server" OnClick="btn_Buscar_Click" CssClass="boton2"
                                            Text="Buscar" Height="30px" Width="162px" />
                                </td>
                                <td class="TextoRigth">Eliminados:</td>
                                <td class="TextoLeft">
                                    <asp:CheckBox ID="ckElim" runat="server" />
                                </td>
                                <td width="451">&nbsp;</td>
                                <td width="214">
                                &nbsp;
                                <asp:HiddenField ID="hddIdart" runat="server" />
                            </td>
                            </tr>
                        </table>
                        <table border="0" style="width: 100%">
                            <tr>
                                <td width="170">&nbsp;</td>
                                <td width="147">&nbsp;</td>
                                <td class="style5">&nbsp;</td>
                                <td width="144">&nbsp;</td>
                                <td width="93">&nbsp;</td>
                                <td width="235">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style3">CODIGO:</td>
                                <td class="auto-style4">
                                    <asp:TextBox ID="Tcodigo" runat="server"></asp:TextBox>

                                </td>
                                <td class="auto-style5"></td>
                                <td class="auto-style4"></td>
                                <td class="auto-style3"></td>
                                <td class="auto-style4"></td>
                            </tr>

                            <tr>
                                <td class="TextoRigth">Encargado:</td>
                                <td class="TextoLeft">
                                    <asp:TextBox ID="Tencarado" runat="server"></asp:TextBox>
                                    <asp:ImageButton ID="ImBtIngresar" runat="server" Height="30px"
                                        ImageUrl="~/imagenes/check.png"
                                        Width="45px" OnClick="ImBtIngresar_Click" />

                                </td>
                                <td class="style6">&nbsp;</td>
                                <td class="TextoLeft">&nbsp;
                                </td>
                                <td class="TextoRigth">&nbsp;</td>
                                <td class="TextoLeft">&nbsp;</td>
                            </tr>

                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="4">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                        </table>

                    </td>
                </tr>
            </table>

            <hr />

            <asp:GridView ID="dgData" runat="server"
                AutoGenerateColumns="False" DataKeyNames="IDCONSTANTE"
                GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                OnRowDataBound="dgData_RowDataBound" Width="50%"
                AllowPaging="True" OnPageIndexChanging="dgData_PageIndexChanging"
                PageSize="30">

                <Columns>

                    <asp:BoundField DataField="IDCONSTANTE" HeaderText="ID">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="50px" CssClass="TextoCenter" Font-Size="8pt" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CODIGO" HeaderText="CODIGO">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="50px" CssClass="TextoCenter" Font-Size="8pt" />
                    </asp:BoundField>

                    <asp:BoundField DataField="ENCARGADO" HeaderText="ENCARGADO">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="50px" CssClass="TextoCenter" Font-Size="8pt" />
                    </asp:BoundField>



                    <asp:TemplateField HeaderText="Eliminar.">
                        <ItemTemplate>
                            <asp:ImageButton ID="btn_EliCla" runat="server" ImageUrl="~/imagenes/close.png"
                                OnClick="btn_EliCla_Click" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="Titulo2" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Rehabilitar">
                        <ItemTemplate>
                            <asp:ImageButton ID="btn_RehaCla" runat="server" ImageUrl="~/imagenes/check.png"
                                OnClick="btn_RehaCla_Click" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="Titulo2" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:ImageButton ID="btn_Edit" runat="server" ImageUrl="~/imagenes/edit.png"
                                OnClick="btn_Edit_Click" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="Titulo2" Width="50px" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:CommandField ShowSelectButton="true" ButtonType="Link" Visible="false" SelectText="Enroll" />
                </Columns>
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="black" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />

            </asp:GridView>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="BtnPopup" runat="server" Style="display: none" Text="Button" />
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender_Popup" runat="server"
                BackgroundCssClass="modalbackgbround" PopupControlID="PanelPopup"
                TargetControlID="BtnPopup"
                Enabled="True" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelPopup" runat="server" Style="background: #E2E7F3;" ScrollBars="Vertical" Width="90%" Height="70%">

                <hr />
                <table border="0" style="width: 100%">
                    <tr>
                        <td class="TextoCenter">
                            <asp:Button ID="BtnEditarPop" runat="server" OnClick="BtnEditarPop_Click" Text="Editar" Width="296px" />
                        </td>

                    </tr>
                </table>

                <asp:UpdatePanel ID="UpdatePanelCantidad" runat="server" Visible="False">
                    <ContentTemplate>

                        <table border="0" style="width: 100%">
                            <tr>
                                <td class="TextoCenter">
                                    <asp:Button ID="BtnCerrarPopup" runat="server" OnClick="BtnCerrarPopup_Click" Text="Cerrar" Height="24px" Width="297px" />
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <table border="0" style="width: 100%">

                            <tr class="Titulo2">
                                <td class="auto-style54">ID</td>
                                <td class="auto-style63">Codigo</td>
                                <td class="auto-style58">Encargado</td>
                            </tr>
                            <td class="auto-style53">
                                <asp:TextBox ID="IdPop" runat="server" CssClass="TextoLeft" Height="42px" MaxLength="900" MaxLines="5" TextMode="MultiLine" Width="233px" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="auto-style53">
                                <asp:TextBox ID="TextCodigoPop" runat="server" CssClass="TextoLeft" Height="42px" MaxLength="900" MaxLines="5" TextMode="MultiLine" Width="233px"></asp:TextBox>
                            </td>
                            <td class="auto-style53">
                                 <asp:TextBox ID="TextEncargadoPop" runat="server" CssClass="TextoLeft" Height="42px" MaxLength="900" MaxLines="5" TextMode="MultiLine" Width="233px"></asp:TextBox>
                            </td>

                        </table>

                        <hr />

                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>

        </div>
    </form>
</body>
</html>
