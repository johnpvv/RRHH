<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="contenido_Index" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Frameset//EN"
    "http://www.w3.org/TR/html4/frameset.dtd">

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sistema RR.HH.</title>
</head>

<frameset rows="76,*" border="0" frameborder="0">
    <frame name="banner" src="frmtopsiab.aspx" scrolling="no" noresize="noresize" />

    <frameset id="theframe" runat="server" cols="240,*" frameborder="0" border="0">
        <frame id="frmMenu" runat="server" name="contents"
            src="frmleftsiab.aspx" noresize="noresize" />

        <frame id="frmBlk" runat="server" name="main"
            src="frmblksiab.aspx" />
    </frameset>

    <noframes>
        <body>
            <p>Este sistema requiere compatibilidad con frames.</p>
        </body>
    </noframes>
</frameset>
</html>