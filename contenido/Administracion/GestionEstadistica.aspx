<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionEstadistica.aspx.cs" Inherits="contenido_Administracion_GestionEstadistica" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Artículos</title>

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
        .ControlsCenter {
            margin-left: auto;
            margin-right: auto;
            width: 50cm;
            text-align: center;
        }

        .ModalBackground {
            background-color: gray;
            filter: alpha(opacity=50);
            opacity: 0.50;
        }

        #UpdatePanel1 {
            width: 300px;
            height: 100px;
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

        .style6 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 162px;
        }

        .style8 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 135px;
        }

        .auto-style1 {
            height: 20px;
        }

        .auto-style2 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 122px;
        }

        .auto-style3 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 670px;
        }

        .auto-style5 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 184px;
        }

        .auto-style6 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 181px;
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
                    <td class="TextoLeft">
                        <label>
                            &nbsp;Reportes --&gt; Gastos Policlinicos
                        </label>
                    </td>
                    <td style="text-align: right" class="style42">&nbsp;</td>
                    <td style="text-align: right" class="auto-style68"></td>
                </tr>
            </table>

            <table width="80%" border="1">
                <tr>
                    <td>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                            <ContentTemplate>
                                <fieldset>

                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td width="132" class="TextoLeft">
                                                <div>
                                                    <asp:Button ID="BtnDespachar0" runat="server" CssClass="boton2" Height="30px"
                                                        OnClick="btn_Buscar_Click" Text="Buscar" Width="162px" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel3" runat="server">

                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/ajax-loader.gif" />
                                        <asp:Label ID="Label2" runat="server" Text="Ejecutando....."></asp:Label>
                                    </asp:Panel>

                                    <asp:Button ID="btnUpdateProgress1" runat="server" Style="display:none;" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="5" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                    <ajaxToolkit:ModalPopupExtender ID="UpdateProgress1_ModalPopupExtender" runat="server" BackgroundCssClass="ModalBackground"
                                        DynamicServicePath="" Enabled="True" TargetControlID="btnUpdateProgress1" PopupControlID="Panel3">
                                    </ajaxToolkit:ModalPopupExtender>

                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="UpdatePanelChk" runat="server">

                            <ContentTemplate>
                                <fieldset>
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:CheckBox ID="chk1" runat="server" AutoPostBack="true" OnCheckedChanged="chk1_CheckedChanged" />
                                                1
                            <asp:CheckBox ID="chk2" runat="server" AutoPostBack="true" OnCheckedChanged="chk2_CheckedChanged" />
                                                2
                            <asp:CheckBox ID="chk3" runat="server" AutoPostBack="true" OnCheckedChanged="chk3_CheckedChanged" />
                                                3
                            <asp:CheckBox ID="chk4" runat="server" AutoPostBack="true" OnCheckedChanged="chk4_CheckedChanged" />
                                                4
                            <asp:CheckBox ID="chk5" runat="server" AutoPostBack="true" OnCheckedChanged="chk5_CheckedChanged" />
                                                5
                            <asp:CheckBox ID="chk6" runat="server" AutoPostBack="true" OnCheckedChanged="chk6_CheckedChanged" />
                                                6 
                            <asp:CheckBox ID="chk7" runat="server" AutoPostBack="true" OnCheckedChanged="chk7_CheckedChanged" />
                                                7
                            <asp:CheckBox ID="chk8" runat="server" AutoPostBack="true" OnCheckedChanged="chk8_CheckedChanged" />
                                                8
                            <asp:CheckBox ID="chk9" runat="server" AutoPostBack="true" OnCheckedChanged="chk9_CheckedChanged" />
                                                9
                            <asp:CheckBox ID="chk10" runat="server" AutoPostBack="true" OnCheckedChanged="chk10_CheckedChanged" />
                                                10
                            <asp:CheckBox ID="chk11" runat="server" AutoPostBack="true" OnCheckedChanged="chk11_CheckedChanged" />
                                                11
                            <asp:CheckBox ID="chk12" runat="server" AutoPostBack="true" OnCheckedChanged="chk12_CheckedChanged" />
                                                12</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: left" class="auto-style1">
                                                <asp:HiddenField ID="hdchk" runat="server" />
                                            </td>
                                        </tr>
                                    </table>

                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table border="0" style="width: 100%">
                            <tr>
                                <td class="auto-style5">Año:</td>
                                <td class="auto-style2">
                                    <asp:DropDownList ID="ddlAnio" runat="server">
                                    </asp:DropDownList>

                                </td>
                                <td class="auto-style6">&nbsp;</td>
                                <td class="TextoLeft">
                                    <asp:Button ID="BtnCertificar" runat="server" CssClass="boton2" Height="30px"
                                        OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')"
                                        Text="Certificar" Width="162px" OnClick="BtnCertificar_Click" />
                                </td>
                                <td class="TextoLeft">
                                    <asp:Button ID="BtnLimpiar" runat="server" CssClass="boton2" Height="30px" Visible="false"
                                        OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')"
                                        Text="Limpiar" Width="162px" OnClick="BtnLimpiar_Click" />
                                </td>
                                <td class="TextoLeft">&nbsp;</td>
                            </tr>
                        </table>
                        <table border="0" style="width: 100%">
                            <tr>
                                <td class="TextoRigth">Bodega:</td>
                                <td class="TextoLeft">
                                    <asp:DropDownList runat="server" CssClass="listado" ID="ddlBod">
                                        <asp:ListItem Selected="True" Value="0">Seleccionar Estado</asp:ListItem>
                                        <asp:ListItem Value="1">Vigente</asp:ListItem>
                                        <asp:ListItem Value="3">No Vigente</asp:ListItem>
                                    </asp:DropDownList>

                                </td>
                                <td class="TextoRigth">
                                    <asp:ImageButton runat="server" ImageUrl="~/imagenes/excel_exportar.png"
                                        Height="30px" Width="45px" ID="ImBtPrint0" OnClick="btn_Excel_Click"></asp:ImageButton>

                                </td>
                                <td class="TextoLeft">

                                    <asp:ImageButton runat="server" ImageUrl="~/imagenes/pdf.png" Visible="false"
                                        Height="30px" Width="45px" ID="pdf" OnClick="pdfBtn"></asp:ImageButton>

                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">&nbsp;</td>
                                <td class="auto-style3">&nbsp;</td>
                                <td class="auto-style1">&nbsp;</td>
                                <td class="TextoLeft">&nbsp;</td>
                            </tr>

                        </table>



                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">

                            <ContentTemplate>
                                <fieldset>
                                    <table border="0" style="width: 100%">
                                        <tr>
                                            <td class="TextoLeft">
                                                <asp:Label ID="lbmensaje" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
            </table>
            <hr />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <asp:GridView ID="dgData" runat="server"
                            GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                            OnRowDataBound="dgData_RowDataBound" Width="100%"
                            AllowPaging="True" OnPageIndexChanging="dgData_PageIndexChanging"
                            PageSize="30" AutoGenerateColumns="False" AllowSorting="True" OnSorting="dgData_Sorting">
                            <Columns>

                                <asp:BoundField DataField="TIPO" HeaderText="Tipo" SortExpression="TIPO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>


                                <asp:BoundField DataField="UNIDAD" HeaderText="Unidad" SortExpression="UNIDAD">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_ENERO" HeaderText="Crónico Enero" SortExpression="CRONICO_ENERO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_ENERO" HeaderText="Mórbido Enero" SortExpression="MORBIDO_ENERO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_ENERO" HeaderText="Total Enero" SortExpression="TOTAL_ENERO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_FEBRERO" HeaderText="Crónico Febrero" SortExpression="CRONICO_FEBRERO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_FEBRERO" HeaderText="Mórbido Febrero" SortExpression="MORBIDO_FEBRERO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_FEBRERO" HeaderText="Total Febrero" SortExpression="TOTAL_FEBRERO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_MARZO" HeaderText="Crónico Marzo" SortExpression="CRONICO_MARZO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_MARZO" HeaderText="Mórbido Marzo" SortExpression="MORBIDO_MARZO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_MARZO" HeaderText="Total Marzo" SortExpression="TOTAL_MARZO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_ABRIL" HeaderText="Crónico Abril" SortExpression="CRONICO_ABRIL">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_ABRIL" HeaderText="Mórbido Abril" SortExpression="MORBIDO_ABRIL">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_ABRIL" HeaderText="Total Abril" SortExpression="TOTAL_ABRIL">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_MAYO" HeaderText="Crónico Mayo" SortExpression="CRONICO_MAYO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_MAYO" HeaderText="Mórbido Mayo" SortExpression="MORBIDO_MAYO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_MAYO" HeaderText="Total Mayo" SortExpression="TOTAL_MAYO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_JUNIO" HeaderText="Crónico Junio" SortExpression="CRONICO_JUNIO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_JUNIO" HeaderText="Mórbido Junio" SortExpression="MORBIDO_JUNIO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_JUNIO" HeaderText="Total Junio" SortExpression="TOTAL_JUNIO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_JULIO" HeaderText="Crónico Julio" SortExpression="CRONICO_JULIO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_JULIO" HeaderText="Mórbido Julio" SortExpression="MORBIDO_JULIO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_JULIO" HeaderText="Total Julio" SortExpression="TOTAL_JULIO">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_AGOST" HeaderText="Crónico Agosto" SortExpression="CRONICO_AGOST">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_AGOST" HeaderText="Mórbido Agosto" SortExpression="MORBIDO_AGOST">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_AGOST" HeaderText="Total Agosto" SortExpression="TOTAL_AGOST">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_SEPT" HeaderText="Crónico Septiembre" SortExpression="CRONICO_SEPT">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_SEPT" HeaderText="Mórbido Septiembre" SortExpression="MORBIDO_SEPT">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_SEPT" HeaderText="Total Septiembre" SortExpression="TOTAL_SEPT">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_OCT" HeaderText="Crónico Octubre" SortExpression="CRONICO_OCT">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_OCT" HeaderText="Mórbido Octubre" SortExpression="MORBIDO_OCT">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_OCT" HeaderText="Total Octubre" SortExpression="TOTAL_OCT">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_NOV" HeaderText="Crónico Noviembre" SortExpression="CRONICO_NOV">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_NOV" HeaderText="Mórbido Noviembre" SortExpression="MORBIDO_NOV">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_NOV" HeaderText="Total Noviembre" SortExpression="TOTAL_NOV">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="CRONICO_DIC" HeaderText="Crónico Diciembre" SortExpression="CRONICO_DIC">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="MORBIDO_DIC" HeaderText="Mórbido Diciembre" SortExpression="MORBIDO_DIC">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>

                                <asp:BoundField DataField="TOTAL_DIC" HeaderText="Total Diciembre" SortExpression="TOTAL_DIC">
                                    <HeaderStyle CssClass="Titulo2" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>


                            </Columns>
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="black" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <br />

        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <fieldset>
                    <asp:GridView ID="dgData0" runat="server"
                        GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                        OnRowDataBound="dgData_RowDataBound" Width="100%" OnPageIndexChanging="dgData_PageIndexChanging"
                        PageSize="30" AutoGenerateColumns="False" Visible="False">
                        <Columns>
                            <asp:BoundField DataField="TIPO" HeaderText="TIPO" SortExpression="TIPO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>


                            <asp:BoundField DataField="UNIDAD" HeaderText="UNIDAD" SortExpression="UNIDAD">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_ENERO" HeaderText="CRONICO_ENERO" SortExpression="CRONICO_ENERO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_ENERO" HeaderText="MORBIDO_ENERO" SortExpression="MORBIDO_ENERO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_ENERO" HeaderText="TOTAL_ENERO" SortExpression="TOTAL_ENERO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_FEBRERO" HeaderText="CRONICO_FEBRERO" SortExpression="CRONICO_FEBRERO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_FEBRERO" HeaderText="MORBIDO_FEBRERO" SortExpression="MORBIDO_FEBRERO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_FEBRERO" HeaderText="TOTAL_FEBRERO" SortExpression="TOTAL_FEBRERO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_MARZO" HeaderText="CRONICO_MARZO" SortExpression="CRONICO_MARZO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_MARZO" HeaderText="MORBIDO_MARZO" SortExpression="MORBIDO_MARZO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_MARZO" HeaderText="TOTAL_MARZO" SortExpression="TOTAL_MARZO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_ABRIL" HeaderText="CRONICO_ABRIL" SortExpression="CRONICO_ABRIL">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_ABRIL" HeaderText="MORBIDO_ABRIL" SortExpression="MORBIDO_ABRIL">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_ABRIL" HeaderText="TOTAL_ABRIL" SortExpression="TOTAL_ABRIL">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_MAYO" HeaderText="CRONICO_MAYO" SortExpression="CRONICO_MAYO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_MAYO" HeaderText="MORBIDO_MAYO" SortExpression="MORBIDO_MAYO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_MAYO" HeaderText="TOTAL_MAYO" SortExpression="TOTAL_MAYO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_JUNIO" HeaderText="CRONICO_JUNIO" SortExpression="CRONICO_JUNIO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_JUNIO" HeaderText="MORBIDO_JUNIO" SortExpression="MORBIDO_JUNIO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_JUNIO" HeaderText="TOTAL_JUNIO" SortExpression="TOTAL_JUNIO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_JULIO" HeaderText="CRONICO_JULIO" SortExpression="CRONICO_JULIO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_JULIO" HeaderText="MORBIDO_JULIO" SortExpression="MORBIDO_JULIO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_JULIO" HeaderText="TOTAL_JULIO" SortExpression="TOTAL_JULIO">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_AGOST" HeaderText="CRONICO_AGOSTO" SortExpression="CRONICO_AGOST">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_AGOST" HeaderText="MORBIDO_AGOSTO" SortExpression="MORBIDO_AGOST">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_AGOST" HeaderText="TOTAL_AGOST" SortExpression="TOTAL_AGOST">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_SEPT" HeaderText="CRONICO_SEPT" SortExpression="CRONICO_SEPT">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_SEPT" HeaderText="MORBIDO_SEPT" SortExpression="MORBIDO_SEPT">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_SEPT" HeaderText="TOTAL_SEPT" SortExpression="TOTAL_SEPT">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_OCT" HeaderText="CRONICO_OCT" SortExpression="CRONICO_OCT">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_OCT" HeaderText="MORBIDO_OCT" SortExpression="MORBIDO_OCT">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_OCT" HeaderText="TOTAL_OCT" SortExpression="TOTAL_OCT">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_NOV" HeaderText="CRONICO_NOV" SortExpression="CRONICO_NOV">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_NOV" HeaderText="MORBIDO_NOV" SortExpression="MORBIDO_NOV">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_NOV" HeaderText="TOTAL_NOV" SortExpression="TOTAL_NOV">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CRONICO_DIC" HeaderText="CRONICO_DIC" SortExpression="CRONICO_DIC">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="MORBIDO_DIC" HeaderText="MORBIDO_DIC" SortExpression="MORBIDO_DIC">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TOTAL_DIC" HeaderText="TOTAL_DIC" SortExpression="TOTAL_DIC">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>

                        </Columns>
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="black" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
<script type="text/javascript">
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(showPopup);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(hidPopup);

    function showPopup(sender, args) {
        var c = '<%= UpdateProgress1_ModalPopupExtender.ClientID %>';
        $find(c).show();
    }

    function hidPopup(sender, args) {
        var c = '<%= UpdateProgress1_ModalPopupExtender.ClientID %>';
        $find(c).hide();



    }

</script>

