<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="contenido_Index" %>


    <frameset rows="76,*" border="0" frameborder="0">
		<frame name="banner" src="frmtopsiab.aspx"scrolling="no" noresize="no" />
		<frameset id="theframe" runat="server" frameborder="0" border="3" bordercolor="red" cols="240,*">
			<frame id="frmMenu" runat="server" name="contents" src="frmleftsiab.aspx" noresize="no"/>
			<frame id="frmBlk" runat="server" name="main" src="frmblksiab.aspx"/>
		</frameset>
		<noframes>
		</noframes>
	</frameset>