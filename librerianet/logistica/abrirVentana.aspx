<%@ Page Language="VB" AutoEventWireup="false" CodeFile="abrirVentana.aspx.vb" Inherits="logistica_abrirVentana" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<style type="text/css">
.titulofuncion {
	font-size: 11px;
	font-family: verdana;
	font-weight: bold;
	color: #000080;
}
.descripcion {
	font-size: 11px;
	font-family: verdana;
	color: #000080;
	padding-left : 5%;
}
</style>
</head>

<body bgcolor="White" style="margin: 5px;vertical-align:middle; ">
<div style ="line-height :12pt;">
    <form id="form1" runat="server">
    
        <asp:Label ID="pNumero" runat="server" BackColor="#E33439" Font-Names="Verdana" 
            Font-Size="Small" ForeColor="White" Text="Orden de " Width="100%" 
            style="text-align :center;" Font-Bold="True" ></asp:Label>
    
    </form>
    
   <label class="titulofuncion">Proveedor:</label>
<p class="descripcion" id ="pProveedor" runat ="server" ></p>
   <label class="titulofuncion">Observación:</label>
<p class="descripcion" id ="pObs" runat ="server" ><label id ="lblReferencia" runat ="server" title ="Ninguna" ></label></p>
</div>

</body>
</html>
