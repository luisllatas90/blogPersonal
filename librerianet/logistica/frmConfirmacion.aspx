<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfirmacion.aspx.vb" Inherits="logistica_frmConfirmacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>    
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <TABLE width="50%" align="center">
    <tr><td align="center" style="font-size: large; font-family: Arial">
    
        Confirmación de entrega de pedidos</td></tr>
    <tr><td>
        <br />
        Nro. Salida&nbsp;&nbsp; :
        <asp:Label ID="lblSalida" runat="server"></asp:Label>
        <br />
        <br />
        Referencia&nbsp;&nbsp;&nbsp; :
        <asp:Label ID="lblObservacion" runat="server"></asp:Label>
        <br />
        <br />
        Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :         <asp:Label ID="lblUsuario" runat="server"></asp:Label>
        <br />
    </td></tr>
    <tr><td align="center">
        <asp:Button ID="cmdConfirmar" runat="server" BorderStyle="Solid" 
            Text="Confirmar Entrega" CssClass="guardar" Height="32px" />
    
    </td></tr>
    </TABLE>
    </div>
    </form>
</body>
</html>
