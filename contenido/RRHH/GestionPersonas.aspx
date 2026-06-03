<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GestionPersonas.aspx.cs" Inherits="contenido_RRHH_GestionPersonas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RRHH_GestionPersonas</title>
    <script language="text/javascript" src="../../js/common.js" type="text/javascript"></script>

    <link runat="server" href="~/css/Estilos1.css" rel="stylesheet" type="text/css" id="Link1" />
    <link href="~/css/css.css" rel="stylesheet" type="text/css" />
    <%--<link href="~/css/bootstrap/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>

    <script type="text/javascript">
        function ExisteRut() {
            __doPostBack('ExisteRutPostBack', '')
        }
        function ConfirmarGuardar() {
            if (confirm('¿Desea guardar los Cambios?')) {
                document.getElementById('divCargando').style.display = 'block';
                return true;
            }
            return false;
        }
    </script>
    <style type="text/css">
        .success {
            background-color: #4CAF50;
            color: #FDFEFE;
        }

        .info {
            background-color: #2196F3;
            color: #FDFEFE;
        }

        .labelBlue {
            background-color: #4682B4;
            color: #FDFEFE;
        }

        .warning {
            background-color: #ff9800;
            color: #FDFEFE;
        }

        .warningDark {
            background-color: darkmagenta;
            color: white;
        }

        .goldBlack {
            background-color: gold;
            color: black;
        }

        .danger {
            background-color: #f44336;
            color: #FDFEFE;
        }

        .other {
            background-color: #e7e7e7;
            color: black;
        }

        .auto-style4 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 353px;
        }

        .auto-style13 {
            height: 29px;
        }

        .auto-style15 {
            text-align: left;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            width: 353px;
            height: 29px;
        }

        .auto-style18 {
            width: 787px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="TS_1" runat="server" EnableScriptGlobalization="True"></asp:ScriptManager>
        <table class="auto-style18">
            <tr>
                <td class="TextoLeft">Gestión
                        <label>
                            Personas --&gt; Antecedentes --&gt;
                        <asp:Label ID="LbTitulo" runat="server" Text="Label"></asp:Label>
                        </label>
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImgBtnBack" runat="server" ImageUrl="~/imagenes/back.png"
                        OnClick="ImgBtnBack_Click" Style="width: 24px; text-align: right" />
                </td>
            </tr>
        </table>
        <ajaxToolkit:TabContainer runat="server" ID="TC_1" Height="700px" Width="800px"
            Font-Names="Tahoma" Font-Size="13px" ForeColor="#666666" ScrollBars="Auto"
            ActiveTabIndex="0">

            <ajaxToolkit:TabPanel runat="server" ID="TabPanel4" HeaderText="Prueba4" Font-Names="Tahoma" ForeColor="#666666" Font-Size="13px">
                <HeaderTemplate>Información Personal</HeaderTemplate>
                <ContentTemplate>
                    <table border="0" style="width: 700px" class="table table-hover table-bordered">
                        <tr>
                            <td class="auto-style52">
                                <table border="0" style="width: 600px">
                                    <tr>
                                        <td class="auto-style57">
                                            <asp:Button ID="btn_Agregar" Height="35px" Text="Grabar" Width="150px"
                                                class="success" runat="server"
                                                OnClick="btn_Agregar_Click"
                                                OnClientClick="return ConfirmarGuardar();" />
                                        </td>

                                        <td class="auto-style58">
                                            <asp:Button ID="btn_habilitar"
                                                runat="server" class="goldBlack"
                                                Text="Habilitar" Width="150px" Height="35px"
                                                OnClick="btn_habilitar_Click"
                                                OnClientClick="javascript:Confirm('Desea realizar la actualización de los Datos..')" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0">
                                    <tr>
                                        <td style="width: 200px;"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth"></td>
                                        <td class="TextoLeft"></td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Estado:</td>
                                        <td class="TextoLeft">
                                            <asp:Label ID="lbEstado" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">RUT:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtRut" runat="server" MaxLength="9"
                                                onblur="IsInteger(this);ExisteRut();"
                                                Width="141px" Required="true" Font-Bold="True"></asp:TextBox>
                                            &nbsp;&nbsp;-&nbsp;&nbsp;
                                                <asp:TextBox ID="TxtDv" runat="server" MaxLength="1" Width="23px" Required="true" Font-Bold="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Nombres:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtNombre" runat="server" Width="350px" MaxLength="80" Required="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">Nombre Social:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtNombreSocial" runat="server" MaxLength="80" Width="350px"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">Apellido Paterno:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtPaterno" runat="server" Width="350px" MaxLength="80" Required="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">Apellido Materno:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtMaterno" runat="server" Width="350px" MaxLength="80" Required="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">Dirección:</td>
                                        <td class="TextoLeft">
                                            <asp:TextBox ID="TxtDire" runat="server" Width="400px" Required="true"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="TextoRigth">Prevision:</td>
                                        <td class="TextoLeft">

                                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender2" runat="server" PromptCssClass="PromptCSS" QueryPattern="Contains" TargetControlID="ddlPrevision" BehaviorID="_content_ListSearchExtender2">
                                            </ajaxToolkit:ListSearchExtender>

                                            <asp:DropDownList ID="ddlPrevision" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Región:</td>

                                        <td class="TextoLeft">
                                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                                TargetControlID="ddlRegion"
                                                PromptCssClass="PromptCSS" QueryPattern="Contains" BehaviorID="_content_ListSearchExtender1" />

                                            <asp:DropDownList ID="ddlRegion" runat="server" Width="250px"
                                                OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged"
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Comuna:</td>
                                        <td class="TextoLeft">
                                            <ajaxToolkit:ListSearchExtender ID="ListSearchExtender3" runat="server"
                                                TargetControlID="ddlComuna"
                                                PromptCssClass="PromptCSS" QueryPattern="Contains" BehaviorID="_content_ListSearchExtender3" />

                                            <asp:DropDownList ID="ddlComuna" runat="server" Width="250px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">E-Mail:</td>
                                        <td>
                                            <asp:TextBox ID="TMail" runat="server" Height="21px" MaxLength="100" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 200px"></td>
                                        <td></td>
                                        <td style="width: 250px"></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Fono 1 (+56):</td>
                                        <td class="auto-style63">
                                            <asp:TextBox ID="TFono1" runat="server" Height="21px" MaxLength="9" Width="181px" onblur="IsInteger(this)"></asp:TextBox>
                                        </td>
                                        <td class="TextoRigth">Obs Fono 1:</td>
                                        <td class="auto-style4">
                                            <asp:TextBox ID="TObsFono1" runat="server" Height="21px" MaxLength="99" Width="242px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Fono 2 (+56):</td>
                                        <td class="auto-style13">
                                            <asp:TextBox ID="TFono2" runat="server" Height="21px" MaxLength="9" Width="180px" onblur="IsInteger(this)"></asp:TextBox>
                                        </td>
                                        <td class="TextoRigth">Obs Fono 2:</td>
                                        <td class="auto-style15">
                                            <asp:TextBox ID="TObsFono2" runat="server" Height="21px" MaxLength="99" Width="242px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth"></td>
                                        <td class="auto-style63" colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td class="TextoRigth">Observaciones:</td>
                                        <td class="TextoLeft" colspan="3">
                                            <asp:TextBox ID="observacion" runat="server" CssClass="TextoLeft" Height="80px" MaxLength="500" MaxLines="5" onkeypress="return HandleEnter(event)" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                            <asp:HiddenField ID="hdIdentificador" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="divCargando" style="display: none; position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(0,0,0,0.3); z-index: 9999;">

                        <div style="position: absolute; top: 50%; left: 50%; transform: translate(-50%,-50%); width: 300px; height: 180px; background: white; border-radius: 10px; text-align: center; padding-top: 30px; box-shadow: 0 0 15px #666;">

                            <img src="../../imagenes/ajax-loader.gif" style="width: 120px; height: 120px;" alt="Cargando..." />
                            <br />
                            <br />
                            <span style="font-size: 16px; font-weight: bold;">Guardando información...
                            </span>

                        </div>
                    </div>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>

            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel2">
                <HeaderTemplate>Acciones</HeaderTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </form>
</body>
</html>
