<%@ Page Language="VB" AutoEventWireup="false" CodeFile="buscarestudiante.aspx.vb" Inherits="buscarestudiante" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar estudiante autor de la investigación</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 187px;
        }
        .style2
        {
            width: 66%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="style1">
                <b>Ingrese apellidos o nombres</b></td>
            <td class="style2">
                <asp:TextBox ID="TextBox1" runat="server" Width="50%"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:ListBox ID="ListBox1" runat="server" Width="100%">
                    <asp:ListItem Value="1">REYES HERNÁNDEZ, HEYDI</asp:ListItem>
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="cmdAnadir" runat="server" Text="Añadir" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
