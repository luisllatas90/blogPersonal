<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Blanco.aspx.vb" Inherits="SisSolicitudes_Blanco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        Recordar Solicitud:<br />
        Cod. Solicitud:</div>
    <asp:TextBox ID="txtCod" runat="server"></asp:TextBox>
    &nbsp;<asp:Button ID="Button1" runat="server" Text="Button" />
    <br />
    2. Evaluación y Registro
    <br />
    <asp:TextBox ID="txtCod0" runat="server"></asp:TextBox>
    &nbsp;<asp:Button ID="Button2" runat="server" Text="Button" />
    </form>
</body>
</html>
