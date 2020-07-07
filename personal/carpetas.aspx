<%@ Page Language="VB" AutoEventWireup="false" CodeFile="carpetas.aspx.vb" Inherits="carpetas" %>
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
<table width="100%" height="100%" border="0" cellpadding="4" cellspacing="0" id="tblMnuPrincipal">
  <tr>
    <td width="97%" class="bordeinf" valign="middle" height="8%" valign="top">
    <b> <font size="2" color="Blue" face="Tahoma"><asp:Label ID="lblModulo" 
            runat="server"></asp:Label></font></b>
        
	</td>
	<td width="3%"align="right" height="8%" style="cursor:hand" class="bordeinf" onClick="OcultarFrame()">
	<img src="../images/menus/contraer.gif">
	</td>
	</tr>
  <tr>
    <td valign="top" align="left" colspan="2" height="32%" width="100%">
        <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" Font-Names="Arial" 
            Font-Size="12pt" ForeColor="Red" 
            Text="Ha finalizado el tiempo de acceso al sistema. Cierre la ventana de su navegador e ingrese denuevo." 
            Visible="False"></asp:Label>
        <asp:DataList ID="dlMenuPadre" runat="server" RepeatColumns="3" 
            DataKeyField="codigo_men" Width="100%" HorizontalAlign="Center">
            <ItemStyle Height="60px" HorizontalAlign="Center" VerticalAlign="Middle" 
                Width="33.3%" />
            <ItemTemplate>
            <asp:Image ID="imgMenu" runat="server" 
                    ImageUrl='<%# "../images/menus/" & eval("icono_men") %>' Height="35px" 
                    Width="35px" />
		    <br />
            <asp:LinkButton ID="lnkMenuPadre" runat="server" 
                    Text='<%# eval("descripcion_men") %>' Font-Overline="False" 
                    Font-Underline="True" ForeColor="#0066FF"></asp:LinkButton>
            </ItemTemplate>
        </asp:DataList>
    </td>
	</tr>
  <tr>
    <td colspan="2" height="60%" width="100%" valign="top" style="background-color:White" class="contornotabla">
		<!--<div id="listadiv" style="width:100%;height:100%;>-->
            <asp:TreeView ID="trwsubMenus" runat="server" Font-Names="Verdana" Font-Size="8pt">
                <HoverNodeStyle CssClass="menuporelegir" />
                <SelectedNodeStyle CssClass="menuseleccionado" />
            </asp:TreeView>
         <!--</div>-->
	</td>
   </tr>
</table>
<table style="width: 100%;display:none;height:100%;cursor:hand" id="tblTituloMnu" bgcolor="#CCCCCC">
	<tr><td align="left" valign="top">
		<img src="../images/menus/barramenu.gif" onClick="OcultarFrame()"></td>
	</tr>
</table> 
<asp:HiddenField ID="hdRutas" runat="server" />
</form>

</body>
</html>
