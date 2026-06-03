<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaArticulos.aspx.cs" Inherits="contenido_Administracion_ListaArticulos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>Listado Artículos</title>
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

        .auto-style4 {
            width: 154px;
        }

        .auto-style5 {
            width: 64px;
        }

        .auto-style6 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 32px;
            width: 64px;
        }

        .auto-style7 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 32px;
        }

        .auto-style8 {
            height: 32px;
        }

        .auto-style9 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 28px;
            width: 64px;
        }

        .auto-style10 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 28px;
        }

        .auto-style11 {
            height: 28px;
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
                    <td class="TextoLeft">Gestión Artículos
                    </td>
                </tr>
            </table>

            <table width="90%" border="1">
                <tr>
                    <td>
                        <table width="100%" border="0">
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
                                <td width="195"></td>
                                <td width="451">&nbsp;</td>
                            </tr>
                        </table>
                        <table border="0" style="width: 100%">
                            <tr>
                                <td width="170">&nbsp;</td>
                                <td width="147">&nbsp;</td>
                                <td width="75">&nbsp;</td>
                                <td width="144">&nbsp;</td>
                                <td width="93">&nbsp;</td>
                                <td width="235">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TextoRigth">Nombre:</td>
                                <td colspan="5" class="TextoLeft">
                                    <asp:TextBox ID="bTxtNom" runat="server" Height="24px" Width="690px"></asp:TextBox>
                                </td>
                                <td width="126">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="TextoRigth">Codigo:</td>
                                <td class="TextoLeft">
                                    <asp:TextBox ID="btxtCod" runat="server" Width="129px"></asp:TextBox>
                                </td>

                                <td class="TextoRigth">&nbsp;</td>
                                <td class="TextoLeft">
                                    <asp:ImageButton runat="server" ImageUrl="~/imagenes/excel_exportar.png"
                                        Height="30px" Width="45px" ID="ImBtPrint0" OnClick="btn_Excel_Click"></asp:ImageButton>

                                </td>
                                <td class="style3">
                                    <label></label>
                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

            <hr />
            <p>
                <asp:GridView ID="dgData" runat="server" AutoGenerateColumns="False" DataKeyNames="IDARTICULO"
                    GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged"
                    OnRowDataBound="dgData_RowDataBound" Width="100%" AllowPaging="True"
                    OnPageIndexChanging="gbExp_PageIndexChanging" AllowSorting="True" OnSorting="dgData_Sorting" PageSize="50">
                    <Columns>
                        <asp:BoundField DataField="IDARTICULO" HeaderText="Id" ShowHeader="False">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle CssClass="TextoCenter" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CODARTICULO" HeaderText="Código" SortExpression="CODARTICULO">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle Width="70px" CssClass="TextoCenter" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DESCRIPCION_LARGA" HeaderText="Nombre" SortExpression="DESCRIPCION_LARGA">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle Width="130px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TIPO_RIESGO" HeaderText="MORBIDO" SortExpression="TIPO_RIESGO">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle Width="50px" CssClass="TextoCenter" />
                        </asp:BoundField>

                        <asp:BoundField DataField="UNI_MIN" HeaderText="UN. MINIMA" SortExpression="UNI_MIN">
                            <HeaderStyle Width="10px" CssClass="Titulo2" />
                            <ItemStyle Font-Size="8pt" CssClass="TextoCenter" HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="VIA" HeaderText="VIA" SortExpression="VIA">
                            <HeaderStyle Width="10px" CssClass="Titulo2" />
                            <ItemStyle Font-Size="8pt" CssClass="TextoCenter" HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="CALCULAR" HeaderText="CALCULAR" SortExpression="CALCULAR">
                            <HeaderStyle Width="10px" CssClass="Titulo2" />
                            <ItemStyle Font-Size="8pt" CssClass="TextoCenter" HorizontalAlign="Center" />
                        </asp:BoundField>

                        <asp:BoundField DataField="bodega" HeaderText="Bodega" SortExpression="bodega">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle Width="100px" CssClass="TextoCenter" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo"
                            DataFormatString="{0:###,###,###,##0.00}" HeaderText="Existencia" SortExpression="saldo">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle Width="100px" CssClass="TextoCenter" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRECIO_UNITARIO"
                            DataFormatString="{0:$###,###,###,##0.00}" HeaderText="Precio Neto" SortExpression="PRECIO_UNITARIO">
                            <HeaderStyle CssClass="Titulo2" />
                            <ItemStyle Width="100px" CssClass="TextoCenter" />
                        </asp:BoundField>
                        <asp:CommandField ShowSelectButton="true" ButtonType="Link" Visible="false" SelectText="Enroll" />
                    </Columns>
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="black" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

            </p>
            <br />

            <asp:GridView ID="dgData0" runat="server"
                GridLines="None"
                Width="100%"
                AutoGenerateColumns="False" Visible="False">
                <Columns>

                    <asp:BoundField DataField="CODARTICULO" HeaderText="Código" SortExpression="CODARTICULO">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="70px" CssClass="TextoCenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DESCRIPCION_LARGA" HeaderText="Nombre" SortExpression="DESCRIPCION_LARGA">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TIPO_RIESGO" HeaderText="MORBIDO" SortExpression="TIPO_RIESGO">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="50px" CssClass="TextoCenter" />
                    </asp:BoundField>

                    <asp:BoundField DataField="UNI_MIN" HeaderText="UN. MINIMA" SortExpression="UNI_MIN">
                        <HeaderStyle Width="10px" CssClass="Titulo2" />
                        <ItemStyle Font-Size="8pt" CssClass="TextoCenter" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="VIA" HeaderText="VIA" SortExpression="VIA">
                        <HeaderStyle Width="10px" CssClass="Titulo2" />
                        <ItemStyle Font-Size="8pt" CssClass="TextoCenter" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="CALCULAR" HeaderText="CALCULAR" SortExpression="CALCULAR">
                        <HeaderStyle Width="10px" CssClass="Titulo2" />
                        <ItemStyle Font-Size="8pt" CssClass="TextoCenter" HorizontalAlign="Center" />
                    </asp:BoundField>

                    <asp:BoundField DataField="bodega" HeaderText="Bodega" SortExpression="bodega">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="100px" CssClass="TextoCenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="saldo"
                        HeaderText="Existencia" SortExpression="saldo">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="100px" CssClass="TextoCenter" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRECIO_UNITARIO"
                        HeaderText="Precio Neto" SortExpression="PRECIO_UNITARIO">
                        <HeaderStyle CssClass="Titulo2" />
                        <ItemStyle Width="100px" CssClass="TextoCenter" />
                    </asp:BoundField>

                </Columns>
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="black" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>

        </div>

    </form>
</body>
</html>
