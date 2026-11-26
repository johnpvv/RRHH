<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmleftsiab.aspx.cs" Inherits="contenido_frmleftsiab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TreeView ID="TreeView2" runat="server" Height="47px" ImageSet="Inbox" Style="z-index: 100; left: 0px; position: absolute; top: 0px"
                Width="200px">
                <ParentNodeStyle Font-Bold="True" />
                <HoverNodeStyle Font-Underline="True" Font-Bold="true" />
                <SelectedNodeStyle Font-Bold="true" VerticalPadding="0px" />
                <NodeStyle Font-Bold="false" />
                <Nodes>
                    <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/home.png" Text="RR.HH." Value="0">
                        <asp:TreeNode Expanded="True" SelectAction="Expand" ImageUrl="~/imagenes/menu/ajustes.png" Text="Gestión RR.HH." Value="Gestión" ToolTip="Gestion RRHH">
                            <asp:TreeNode NavigateUrl="~/contenido/GestionRRHH/ListaTurnos.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesroles.png" Text="Gestión Turnos" ToolTip="Turnos" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/GestionRRHH/GestionTurnosExcel.aspx" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Cargar Turnos <br> desde Excel" ToolTip="Turnos" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/GestionRRHH/ListaHorarios.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Gestión Horarios" ToolTip="Horarios" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/GestionRRHH/GestionMarcaciones.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesroles.png" Text="Gestión Marcaciones" ToolTip="Marcaciones" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/GestionRRHH/GestionEquivalenciaReloj.aspx" Target="main" ImageUrl="~/imagenes/menu/archivos.png" Text="Equivalencia Relojes" ToolTip="Equivalencia" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/GestionRRHH/GestionSincronizacion.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesusuario.png" Text="Sincronización" ToolTip="Sincronización" Value="MANT_ROLES"></asp:TreeNode>
                        </asp:TreeNode>

                        <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Reportes" Value="Reportes" ToolTip="Reportes">
                            <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/compras.png" Text="Marcaciones" Value="Marcas" ToolTip="Marcas">
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepMarcaCentro.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Marcas <br> Por Centro" ToolTip="Marcaciones" Value="RPT_MARCA"></asp:TreeNode>
                                <asp:TreeNode NavigateUrl="~/contenido/Reportes/RepMarcaTrabajadores.aspx" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Detalle Marcas <br> Trabajadores" ToolTip="Marcaciones" Value="RPT_MARCA"></asp:TreeNode>
                            </asp:TreeNode>
                        </asp:TreeNode>

                        <asp:TreeNode Expanded="False" SelectAction="Expand" ImageUrl="~/imagenes/menu/ajustesb.png" Text="Administracion" Value="Administracion" ToolTip="Administracion">
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListaPersonas.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesusuario.png" Text="Gestión Usuarios" ToolTip="Usuarios" Value="MANT_USUARIO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaAccesos.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesaccesos.png" Text="Accesos" ToolTip="Accesos" Value="MANT_ACCESO"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/ListaRoles.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesroles.png" Text="Roles" ToolTip="Roles" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListaUniOperativa.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/ajustesunidadoperativa.png" Text="Gestión Centros" ToolTip="Centros o Unidades" Value="MANT_UNIDAD"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/ListarRelojes.aspx" Target="main" ImageUrl="~/imagenes/menu/calculos.png" Text="Gestión Relojes" ToolTip="Relojes" Value="MANT_ROLES"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/Constantes.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Const. Config" ToolTip="Const. de Configuracion" Value="MANT_CONST"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/SysAdmin/Feriados.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/firmas.png" Text="Feriados" ToolTip="Feriados" Value="MANT_CONST"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="~/contenido/Administracion/GestionEstadistica.aspx?id=1" Target="main" ImageUrl="~/imagenes/menu/compras.png" Text="Estadistica" ToolTip="Estadistica" Value="ADM_ESTAD"></asp:TreeNode>
                        </asp:TreeNode>
                    </asp:TreeNode>
                </Nodes>
                <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                    NodeSpacing="0px" VerticalPadding="0px" />
            </asp:TreeView>
        </div>
    </form>
</body>
</html>
