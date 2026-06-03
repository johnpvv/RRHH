<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaRoles.aspx.cs" Inherits="contenido_SysAdmin_ListaRoles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE"/>
    <title>Listado Artículos</title>
    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body, html
        {
            font-family: Tahoma;
            font-size: small;
        }
        .Normal
        {
            background-color: #EFF3FB;
            cursor: hand;
        }
        .Normal:Hover, .Alternate:Hover
        {
            background-color: #D1DDF1;
            cursor: hand;
        }
        .Alternate
        {
            background-color: White;
            cursor: hand;
        }
        .style3
        {
            height: 20px;
        }
        .auto-style1
        {
            width: 70px;
            height: 20px;
        }
        .auto-style2
        {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 20px;
        }
        .auto-style3 {
            width: 1010px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <table border="0" style="width: 1194px">
            <tr>
            <td class="auto-style2"><label>
                Gestión Roles --&gt;</label></td>
            <td class="auto-style1">
                </td>
            </tr>
        </table>
         <table width="90%" border="1">
            <tr>
            <td class="auto-style3">
                    <table border="0" style="width: 771px">
                      <tr>
                        <td width="132"><label>
                            &nbsp;</label><asp:Button ID="btn_Buscar" runat="server" onclick="btn_Buscar_Click" 
                                Text="Buscar" />
                          </td>
                        <td width="214">
                            <asp:Button ID="btnNuevo" runat="server" onclick="btnNuevo_Click" 
                                Text="Nuevo" />
                          </td>
                        <td width="195">
                            &nbsp;</td>
                        <td width="451">&nbsp;</td>
                      </tr>
                    </table>
                    <table border="0" style="width: 809px" >
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
                        <td class="TextoRigth">Roles:</td>
                        <td class="TextoLeft" colspan="4">
                            <asp:TextBox ID="txtBNom" runat="server" Width="455px"></asp:TextBox>
                          </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td class="TextoRigth">&nbsp;</td>
                        <td class="TextoLeft">
                            <asp:CheckBox ID="bchkEli" runat="server" Text="Eliminado" 
                                ToolTip="Eliminado" />
                          </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      </tr>
                    </table>
          
            </td>
            </tr>
        </table>
        
        <hr />
                        <asp:GridView ID="dgData" runat="server" 
             AutoGenerateColumns="False" DataKeyNames="idrol" 
                        GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged" 
                            OnRowDataBound="dgData_RowDataBound" Width="1127px"  onsorting="dgData_Sorting"
             AllowPaging="True" onpageindexchanging="dgData_PageIndexChanging" AllowSorting="True" PageSize="30">
                            <Columns>
                                <asp:BoundField DataField="idrol" HeaderText="Id" ShowHeader="False" 
                                    ReadOnly="True" Visible="False">
                                <HeaderStyle CssClass="Titulo2" Width="50px" />
                                <ItemStyle CssClass="TextoCenter" Width="10px" />
                                </asp:BoundField>

                                <asp:BoundField DataField="codigo" HeaderText="N°" SortExpression="codigo">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle Width="70px" CssClass="TextoCenter" />
                                </asp:BoundField>

                                <asp:BoundField DataField="codigo" HeaderText="Codigo" SortExpression="codigo">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle Width="70px" CssClass="TextoCenter" />
                                </asp:BoundField>

                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion" SortExpression="descripcion">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle Width="70px" CssClass="TextoCenter" />
                                </asp:BoundField>

                                <asp:BoundField DataField="num_usr" HeaderText="Nº Usr." SortExpression="num_usr">
                                <HeaderStyle CssClass="Titulo2" />
                                <ItemStyle Width="70px" CssClass="TextoCenter" />
                                </asp:BoundField>
                                

                                <asp:BoundField DataField="num_rol" HeaderText="Nº App." SortExpression="num_rol">
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