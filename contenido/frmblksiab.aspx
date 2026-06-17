<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmblksiab.aspx.cs" Inherits="contenido_frmblksiab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Menu Principal</title>
    <link href="../css/css.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .PortalContainer {
            width: 95%;
            margin: 20px auto;
            font-family: Arial, Helvetica, sans-serif;
        }

        .PortalTitulo {
            font-size: 28px;
            font-weight: bold;
            color: #2C3E50;
            margin-bottom: 5px;
        }

        .PortalSubTitulo {
            color: #666666;
            margin-bottom: 25px;
            font-size: 13px;
        }

        .MenuGrid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .MenuCard {
            display: block;
            width: 180px;
            height: 140px;
            background-color: #18578C;
            color: White !important;
            text-decoration: none !important;
            border-radius: 10px;
            text-align: center;
            box-shadow: 0px 2px 6px #999999;
            transition: all 0.2s ease;
        }

            .MenuCard:hover {
                background-color: #1F6FB2;
                transform: translateY(-3px);
                text-decoration: none !important;
                box-shadow: 0px 4px 10px #666666;
            }

        .MenuCardIcon {
            font-size: 48px;
            margin-top: 25px;
        }

        .MenuCardText {
            font-size: 15px;
            font-weight: bold;
            margin-top: 10px;
        }
    </style>
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
                <asp:LinkButton ID="btnMarcaciones" runat="server" CssClass="MenuCard">
                    <div class="MenuCardIcon">🕒</div>
                    <div class="MenuCardText">Marcaciones</div>
                </asp:LinkButton>
                <asp:LinkButton ID="btnPermisos" runat="server" CssClass="MenuCard">
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
                <asp:LinkButton ID="btnDocumentos" runat="server" CssClass="MenuCard">
                    <div class="MenuCardIcon">📁</div>
                    <div class="MenuCardText">Documentos</div>
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