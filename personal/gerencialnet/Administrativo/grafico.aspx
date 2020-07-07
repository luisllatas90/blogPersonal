<%@ Page Language="VB" AutoEventWireup="false" CodeFile="grafico.aspx.vb" Inherits="Grafico" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<style type="text/css" >
#textovertical {
writing-mode: tb-rl;
filter: flipv fliph;
} 
</style>
   <title>:: Reportes Academicos :: Gráficos</title>
   <link rel="STYLESHEET" href="css/estilos.css"/>
   <link rel="stylesheet" type="text/css" href="css/estiloimpresion.css" media="print"/>
</head>
<body topmargin="0" leftmargin="0" rightmargin="0" >
<center>
    <form id="form1" runat="server">
        <asp:Panel ID="PanelLeyenda" runat="server" Height="50px" Style="left: 568px;
            top: 228px; padding-right: 10px; padding-left: 10px; padding-bottom: 10px; margin: 10px; padding-top: 10px; text-align: left;" Width="125px" BorderColor="#E0E0E0" BorderWidth="1px" HorizontalAlign="Left" Wrap="False">
        </asp:Panel>
        </form>
    </center>
</body>
</html>
