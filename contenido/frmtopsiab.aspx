<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmtopsiab.aspx.cs" Inherits="contenido_frmtopsiab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sistema Farmacia</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <link href="../css/layout.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
        /* Toggle del menú lateral – anima el ícono */
        function fnMenu() {
            var lfr = window.top.document.getElementById('theframe');
            if (lfr == null) { return; }
            var btn = document.getElementById('btnToggleMenu');
            if (lfr.cols !== "240,*") {
                lfr.cols = "240,*";
                if (btn) { btn.classList.add('is-open'); }
            } else {
                lfr.cols = "1,*";
                if (btn) { btn.classList.remove('is-open'); }
            }
        }
    </script>

    <script language="javascript" type="text/javascript">
        var sessionTimeoutWarning = "330";
        var sessionTimeout        = "<%= Session.Timeout %>";
        var timeOnPageLoad        = new Date();

        setTimeout('SessionWarning()',        parseInt(sessionTimeoutWarning) * 60 * 1000);
        setTimeout('RedirectToWelcomePage()', parseInt(sessionTimeout)        * 60 * 1000);

        function SessionWarning() {
            var mins = parseInt(sessionTimeout) - parseInt(sessionTimeoutWarning);
            alert("Su sesión expirará en otros " + mins + " minutos. Guarde su trabajo.");
        }
        function RedirectToWelcomePage() {
            alert("Sesión expirada. Será redireccionado al Login.");
            self.parent.location = "Login.aspx?out=2";
        }
    </script>
</head>
<body class="topbar-body">
    <form id="form1" runat="server">

        <div class="topbar-bar">

            <%-- ── IZQUIERDA: toggle + logo ── --%>
            <div class="topbar-left">
                <button id="btnToggleMenu" type="button"
                        class="topbar-toggle is-open"
                        onclick="fnMenu();"
                        title="Mostrar / Ocultar menú lateral">
                    <i class="fas fa-bars"></i>
                </button>
                <div class="topbar-logo-wrap">
                    <img src="../imagenes/logo-HCSBA.jpg" alt="Logo HCSBA" class="topbar-logo-img" />
                </div>
            </div>

            <%-- ── CENTRO: título del sistema ── --%>
            <div class="topbar-center">
                <i class="fas fa-pills topbar-title-icon"></i>
                <asp:Label ID="lblTit" runat="server" CssClass="topbar-title"></asp:Label>
            </div>

            <%-- ── DERECHA: usuario + sede + cerrar sesión ── --%>
            <div class="topbar-right">

                <div class="topbar-user-info">
                    <i class="fas fa-user-circle topbar-user-icon"></i>
                    <div class="topbar-user-details">
                        <asp:Label ID="lblUsr" runat="server" CssClass="topbar-user-name"></asp:Label>
                        <span class="topbar-user-sede">
                            <i class="fas fa-hospital-alt"></i>&nbsp;<asp:Label ID="lblSed" runat="server" CssClass="topbar-sede-text"></asp:Label>
                        </span>
                    </div>
                </div>

                <div runat="server" id="cierra" class="topbar-logout-wrap">
                    <asp:LinkButton ID="cierrasesion" runat="server"
                                    OnClick="cierrasesion_Click"
                                    ToolTip="Cerrar Sesión"
                                    CssClass="topbar-logout-btn">
                        <i class="fas fa-sign-out-alt"></i><span class="topbar-logout-txt">Salir</span>
                    </asp:LinkButton>
                </div>

            </div>
        </div>

    </form>
</body>
</html>

