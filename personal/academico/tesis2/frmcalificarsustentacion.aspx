<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmcalificarsustentacion.aspx.vb" Inherits="frmcalificarsustentacion" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Sustentación</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <p class="usatTitulo">Calificativo de Sustentación<p/>
<table cellpadding="3" cellspacing="0" width="100%" class="contornotabla">
        <tr>
            <td>
                Nro Expediente</td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Fecha registro</td>
            <td>
                </asp:TextBox>
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td valign="top">
                Calificativo</td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem>Sobresaliente</asp:ListItem>
                    <asp:ListItem>Buena</asp:ListItem>
                    <asp:ListItem>Aprobado</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        </table>
       
        <p style="text-align:center">
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Font-Bold="True" />&nbsp;
            <asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
        </p>
    </form>
</body>
</html>
