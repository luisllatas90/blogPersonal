<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ayudaemail.aspx.vb" Inherits="librerianet_outlookusat_ayudaemail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body oncontextmenu="return event.ctrlKey">
    <form id="form1" runat="server">
    <h2 style="text-decoration: underline">
        Pasos para activar tu cuenta de correo USAT</h2>
    <asp:Panel ID="Panel1" runat="server">
    <p>
        1. Debes hacer clic en la siguiente dirección web:
        <a target="_blank" href="https://www.outlook.com/">
        https://www.outlook.com/</a></p>
    <p>
        2. Ingresar los siguientes datos de acceso:</p>
    <p>
        Windows Live ID (correo electrónico):&nbsp;         <asp:Label ID="lblusuario" runat="server" Font-Bold="True" Font-Size="14pt" 
            ForeColor="Red"></asp:Label>
        <br />
        Contraseña&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        :&nbsp;&nbsp;
        <asp:Label ID="lblClave" runat="server" Font-Bold="True" Font-Size="14pt" 
            ForeColor="Red"></asp:Label>
    </p>
    <p>
        3. Finalmente completar tus datos personales y hacer clic en el botón &quot;ACEPTO&quot;, tal como se muestra en la siguiente figura:</p>
    <p>
        <img alt="" src="paso1.jpg" style="width: 955px; height: 1172px" /></p>
    <h2>
        Para cualquier consulta o reporte de problemas, pueden realizarlo al siguiente 
        email: <a href="mailto:computo@usat.edu.pe">computo@usat.edu.pe</a>
    </h2>
    </asp:Panel>
    
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="14pt" 
        ForeColor="Red"></asp:Label>
    
    </form>
</body>
</html>
