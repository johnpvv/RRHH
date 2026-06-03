<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestUniOperativa.aspx.cs" Inherits="contenido_Administracion_GestUniOperativa" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Selecting GridView Row</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">

        function leaveChange() {
            __doPostBack('DDLFAMILIA', '')
        }
        function leaveChange2() {
            __doPostBack('DDLSFAMILIA', '')
        }


    </script>

    <script type="text/javascript">
        function LimpiarTexto(obj) {
            document.getElementById(obj).value = "";
        }

        function SaltoTexto(e, obj) {

            if (e.keyCode == 13) {


                var ObjX;
                ObjX = document.getElementById(obj);

                if (ObjX == null) {
                    return;
                }

                var NomFor1;
                NomFor1 = ObjX.form.id;

                var NumObj;
                NumObj = document.forms[NomFor1].elements.length;


                for (i = 0; i < NumObj; i++) {

                    if (document.forms[NomFor1].elements[i].id == obj) {

                        for (j = i + 1; j < NumObj; j++) {

                            if (document.forms[NomFor1].elements[j].disabled == true) {
                                j = j + 1;
                                if (j > (document.forms[NomFor1].elements.length - 1)) {
                                    break;
                                }
                            }

                            if (document.forms[NomFor1].elements[j].type == "text") {

                                document.forms[NomFor1].elements[j].value = "";
                                document.forms[NomFor1].elements[j].focus();
                                break;
                            }
                        }
                    }
                }

            }
            else {
                false
            }

        }

        function ValidarTexto(obj) {
            var strNumero
            if (document.getElementById(obj).value == "") {
                document.getElementById(obj).value = 0;

            }
            else {
                strNumero = document.getElementById(obj).value
                strNumero = strNumero.replace(",", ".");
                if (isNaN(strNumero) == true) {
                    alert("El valor " + document.getElementById(obj).value + " no es un número");
                    document.getElementById(obj).value = 0;
                    document.getElementById(obj).focus();
                }
                else {
                    document.getElementById(obj).value = strNumero;
                }

            }


        }

        document.onkeypress = function (e) {
            var esIE = (document.all);
            var esNS = (document.layers);
            var tecla;
            tecla = (esIE) ? event.keyCode : e.which;
            if (tecla == 13) {
                return false;
            }
        }


    </script>

    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />


    <style type="text/css">
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
            width: 85%;
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

        .style1 {
            width: 229px;
        }

        .style2 {
            width: 285px;
        }

        .style3 {
            width: 164px;
        }

        .style4 {
            width: 106px;
        }

        .style6 {
            width: 323px;
        }

        .style7 {
            width: 295px;
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

        .style15 {
            width: 300px;
        }

        .style16 {
            width: 712px;
        }

        .style17 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            width: 712px;
        }

        .style18 {
            width: 744px;
        }

        .style19 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 10px;
            width: 744px;
        }

        .style20 {
            width: 872px;
        }

        .style21 {
            width: 877px;
        }

        .style22 {
            width: 228px;
        }

        .style23 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 228px;
        }

        .auto-style1 {
            width: 503px;
        }

        .auto-style3 {
            width: 226px;
        }

        .auto-style5 {
            width: 538px;
        }

        .auto-style6 {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server"></asp:ScriptManager>
        <table style="width: 80%">
            <tr>
                <td class="auto-style1">
                    <label>
                        Gestion 
                        Unidad Operativa</label></td>
                <td style="text-align: right" class="auto-style63">

                    <asp:ImageButton ID="ImgBtnBack" runat="server" ImageUrl="~/imagenes/back.png"
                        Style="text-align: right; margin-left: 0px;" OnClick="ImgBtnBack_Click" Width="24px" Height="26px" />

                </td>
                <td style="text-align: right" class="auto-style68"></td>
            </tr>
        </table>
        <div style="margin-left: 40px">
            <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="981px" Width="1100px"
                Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto"
                ActiveTabIndex="0">
                <ajaxToolkit:TabPanel runat="server" ID="TP_1" HeaderText="Prueba2" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                    <HeaderTemplate>
                        Detalle
                    
                    </HeaderTemplate>

                    <ContentTemplate>
                        <table width="947" border="0">
                            <tr>
                                <td class="style1">
                                    <label>
                                        &#160;</label></td>
                                <td class="style2">
                                    <asp:Button ID="BtnAgregar" runat="server" OnClick="BtnAgregar_Click" OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')"
                                        Text="Agregar/Modificar" />
                                </td>
                                <td width="195">&nbsp;</td>
                                <td width="378">&nbsp;</td>
                                <td width="378">&nbsp;</td>
                            </tr>
                        </table>
                        <table border="0" style="width: 945px">

                            <tr>
                                <td width="60" class="TextoRigth">Nombre:</td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtNombre" runat="server" Height="24px"
                                        Width="710px" CssClass="TextoLeft" MaxLength="80" Required="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="TextoRigth">Codigo:
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtCodigo" runat="server" CssClass="TextoLeft" Width="155px"
                                        MaxLength="8"></asp:TextBox>
                                </td>
                                <td class="style23">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td class="TextoRigth">Extiende a 360: </td>
                                <td>
                                    <asp:CheckBox ID="chk360" runat="server" />

                                </td>
                                <td class="style23">&nbsp;</td>
                                <td>&nbsp;</td>
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
                                            <td class="TextoRigth" width="170">Codigo:</td>
                                            <td width="147">
                                                <asp:TextBox ID="TCodigo" runat="server" Height="24px" MaxLength="80" Width="105px"></asp:TextBox>
                                            </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td class="auto-style3">
                                                <asp:RadioButtonList ID="rbLista" runat="server" CssClass="TextoCheck" RepeatDirection="Horizontal" Width="258px">
                                                    <asp:ListItem Selected="True" Value="1">Unidades</asp:ListItem>
                                                    <asp:ListItem Value="2">Asignados</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td width="93">&nbsp;</td>
                                            <td width="235">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="TextoRigth">Descripcion:</td>
                                            <td colspan="5" class="TextoLeft">
                                                <asp:TextBox ID="TDesc" runat="server" Height="24px" Width="307px" MaxLength="80"></asp:TextBox></td>
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
                                                                DataKeyNames="IDARTICULO">
                                                                <Columns>
                                                                    <asp:BoundField DataField="IDARTICULO" HeaderText="Id" ReadOnly="True">
                                                                        <ItemStyle CssClass="TextoCenter" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CODARTICULO" HeaderText="CODARTICULO">
                                                                        <ItemStyle CssClass="TextoCenterutr" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DESCRIPCION_LARGA" HeaderText="DESCRIPCION_LARGA">
                                                                        <ItemStyle CssClass="TextoCenterutr" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Add">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btn_AddVia" runat="server" ImageUrl="~/imagenes/check.png"
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
                                                                DataKeyNames="IDARTUNID">
                                                                <Columns>
                                                                    <asp:BoundField DataField="IDARTUNID" HeaderText="Id" ReadOnly="True">
                                                                        <ItemStyle CssClass="TextoCenter" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CODARTICULO" HeaderText="CODARTICULO">
                                                                        <ItemStyle CssClass="TextoCenter" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="DESCRIPCION_LARGA" HeaderText="DESCRIPCION_LARGA">
                                                                        <ItemStyle CssClass="TextoCenter" />
                                                                    </asp:BoundField>

                                                                    <asp:TemplateField HeaderText="Elim">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="btn_ElimVIa" runat="server" ImageUrl="~/imagenes/close.png"
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
        </div>
    </form>
    <script type="text/javascript">
        function pageLoad() {
            document.getElementById("<%=TxtNombre.ClientID %>").focus();
        }        
    </script>
</body>
</html>

