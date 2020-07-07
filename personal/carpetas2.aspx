<%@ Page Language="VB" AutoEventWireup="false" CodeFile="carpetas2.aspx.vb" Inherits="carpetas2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Menú de Opciones</title>
<link href="../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
<script type="text/javascript">
function OcultarFrame()
{
	var NombreFrame=top.parent.document.getElementById('fraGrupo')
	var TamanioActual=NombreFrame.cols

	if (TamanioActual=="3%,*"){
		NombreFrame.cols="24%,*"
		document.all.tblMnuPrincipal.style.display=""
		document.all.tblTituloMnu.style.display="none"
	}
	else{
		document.all.tblMnuPrincipal.style.display="none"
		document.all.tblTituloMnu.style.display=""
		NombreFrame.cols="3%,*"
	}
}

if(top.location==self.location){
    location.href='../tiempofinalizado.asp'
} //El ../ depende de la ruta de la página
</script>
<style type="text/css">
<!--
body 
{
	background-color: #F0F0F0}
.Menu {color: #000000; font-weight: bold; font-size: 7pt;}

    .menuporelegir
    {
        border: 1px solid #808080;
        background-color: #FFCC66;
    }

    .menuseleccionado
    {
        background-color: #FFCC66;
        border: 1px solid #808080;
    }

    .mnuPadreElegido
    {
        background-color: #FFFFFF;
        border: 2px solid #808000;
    }

-->
</style>
</head>
<body topmargin="0" leftmargin="0" rightmargin="0">
<form id="form1" runat="server">
<asp:HiddenField ID="hdid" runat="server" />
<asp:HiddenField ID="hdctf" runat="server" />
<asp:HiddenField ID="hdcapl" runat="server" />
        <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="12pt" ForeColor="Red" 
            Text="Ha finalizado el tiempo de acceso al sistema. Cierre la ventana de su navegador e ingrese denuevo." 
            Visible="False"></asp:Label>
		<div id="listadiv" style="width:100%;height:90%;>
            <asp:TreeView ID="trwsubMenus" runat="server" Font-Names="Verdana" Font-Size="8pt">
                <HoverNodeStyle CssClass="menuporelegir" />
                <SelectedNodeStyle CssClass="menuseleccionado" />
            </asp:TreeView>
         </div>
<table style="width: 100%;display:none;height:100%;cursor:hand" id="tblTituloMnu" bgcolor="#CCCCCC">
	<tr><td align="left" valign="top">
		<img src="../images/menus/barramenu.gif" onClick="OcultarFrame()"></td>
	</tr>
</table> 
</form>

</body>
</html>
