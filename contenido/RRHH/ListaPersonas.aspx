<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListaPersonas.aspx.cs" Inherits="contenido_RRHH_ListaPersonas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=11; IE=9; IE=8; IE=7; IE=EDGE" />
    <title>RRHH_ListaPersonas</title>
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
    <link href="~/css/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />

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
            /* width: 156px; */
            width: 156px;
        }

        .auto-style3 {
            width: 986px;
        }

        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            height: 24px !important;
        }

        .success {
            background-color: #4CAF50;
            color: #FDFEFE;
        }
        /* Green */
        .info {
            background-color: #2196F3;
            color: #FDFEFE;
        }
        /* Blue */
        .labelBlue {
            background-color: #4682B4;
            color: #FDFEFE;
        }
        /* Blue */
        .warning {
            background-color: #ff9800;
            color: #FDFEFE;
        }
        /* Orange */
        .warningDark {
            background-color: darkmagenta;
            color: white;
        }
        /*  */
        .goldBlack {
            background-color: gold;
            color: black;
        }
        /*  */
        .danger {
            background-color: #f44336;
            color: #FDFEFE;
        }
        /* Red */
        .other {
            background-color: #e7e7e7;
            color: black;
        }
        /* Gray */

        .boton-lavanda {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font: bold;
            text-align: center;
            background-color: #7f7fbf;
            color: #FDFEFE;
            font-size: 13px;
            Width: 150px;
            height: 35px;
        }

        .auto-style4 {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px; /* width: 156px; */
            ;
            width: 156px;
            height: 24px;
        }

        .auto-style5 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 24px;
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
                        Gestión Personas --&gt;</label></td>
                <td></td>
            </tr>
        </table>
        <table width="90%" border="0" class="table table-hover table-bordered">
            <tr>
                <td>
                    <asp:Button ID="btn_Buscar" Height="35px" CssClass="labelBlue" runat="server" OnClick="btn_Buscar_Click" Text="Buscar" Width="120px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnNuevo" Height="35px" class="success" runat="server" OnClick="btnNuevo_Click" Text="Nuevo" Width="120px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <table border="0" style="width: 932px">
                        <tr>
                            <td class="auto-style4">Nombre:</td>
                            <td class="auto-style5">
                                <p>
                                    <asp:TextBox ID="TxtNombre" runat="server" placeholder="Nombre" onkeypress="KeyEnter(event)" Height="20px" MaxLength="80" Width="162px"></asp:TextBox>
                                    <asp:TextBox ID="TxtPaterno" runat="server" placeholder="Paterno" onkeypress="KeyEnter(event)" Height="20px" MaxLength="80" Width="189px"></asp:TextBox>
                                    <asp:TextBox ID="TxtMaterno" runat="server" placeholder="Materno" onkeypress="KeyEnter(event)" Height="20px" MaxLength="80" Width="210px"></asp:TextBox>
                                </p>
                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style2">Rut:</td>
                            <td class="TextoLeft">
                                <asp:TextBox ID="TxtRut" runat="server" onkeypress="KeyEnter(event)" MaxLength="9" Height="20" onblur="IsInteger(this);"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:CheckBox ID="bchkEli" runat="server" Text="Eliminado" ToolTip="Eliminado" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <hr style="width: 100%" />
        <asp:GridView ID="dgData" runat="server"
            AutoGenerateColumns="False" DataKeyNames="rut"
            GridLines="None" OnSelectedIndexChanged="dgData_SelectedIndexChanged"
            OnRowDataBound="dgData_RowDataBound"
            Width="100%" OnSorting="dgData_Sorting"
            AllowPaging="True"
            OnPageIndexChanging="dgData_PageIndexChanging"
            Style="margin-right: 0px;" AllowSorting="True"
            class="table table-hover table-bordered">
            <Columns>
                <asp:BoundField DataField="rut" HeaderText="Rut" SortExpression="rut">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" height="25px" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>
                <asp:BoundField DataField="ap_paterno" HeaderText="Paterno" SortExpression="paterno">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>
                <asp:BoundField DataField="ap_materno" HeaderText="Materno" SortExpression="materno">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>
                <asp:BoundField DataField="direccion" HeaderText="Direccion" SortExpression="direccion">
                    <HeaderStyle CssClass="Titulo2" />
                    <ItemStyle Width="70px" CssClass="TextoCenter" />
                </asp:BoundField>
                <asp:CommandField ShowSelectButton="true" ButtonType="Link" Visible="false" SelectText="Enroll" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
    </form>
</body>
</html>
