<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmblksiab.aspx.cs" Inherits="contenido_frmblksiab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Menu Principal</title>
    <link href="../css/EstiloRRHH.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="PortalContainer">
            <div class="PortalTitulo">
                Portal del Trabajador
            </div>

            <div class="PortalSubTitulo">
                Bienvenido al sistema de autoservicio institucional
            </div>

            <div class="MenuGrid">
                <asp:LinkButton ID="btnPerfil" runat="server" CssClass="MenuCard" OnClick="btnPerfil_Click">
                    <div class="MenuCardIcon">👤</div>
                    <div class="MenuCardText">Mis Datos</div>
                </asp:LinkButton>
                <asp:LinkButton ID="btnMarcaciones" runat="server" CssClass="MenuCard" OnClick="btnMarcaciones_Click">
                    <div class="MenuCardIcon">🕒</div>
                    <div class="MenuCardText">Marcaciones</div>
                </asp:LinkButton>
                <asp:LinkButton ID="btnTurnos" runat="server" CssClass="MenuCard" OnClick="btnTurnos_Click">
                    <div class="MenuCardIcon">📁</div>
                    <div class="MenuCardText">Turnos</div>
                </asp:LinkButton>
                <asp:LinkButton ID="btnPermisos" runat="server" CssClass="MenuCard" OnClick="btnPermisos_Click">
                    <div class="MenuCardIcon">📋</div>
                    <div class="MenuCardText">Permisos</div>
                </asp:LinkButton>                
                <asp:LinkButton ID="btnVacaciones" runat="server" CssClass="MenuCard">
                    <div class="MenuCardIcon">🏖</div>
                    <div class="MenuCardText">Vacaciones</div>
                </asp:LinkButton>
                <asp:LinkButton ID="btnLiquidaciones" runat="server" CssClass="MenuCard">
                    <div class="MenuCardIcon">💰</div>
                    <div class="MenuCardText">Liquidaciones</div>
                </asp:LinkButton>                
                <asp:LinkButton ID="btnDirectorio" runat="server" CssClass="MenuCard">
                    <div class="MenuCardIcon">📞</div>
                    <div class="MenuCardText">Directorio</div>
                </asp:LinkButton>
                <asp:LinkButton ID="btnNoticias" runat="server" CssClass="MenuCard">
                    <div class="MenuCardIcon">📢</div>
                    <div class="MenuCardText">Noticias</div>
                </asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>