<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Identificacion.aspx.vb" Inherits="academico_Identificacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="top: 40%; left: 40%; position: fixed;" class="contornotabla_azul">
            <tr>
                <td>
                    Código universitario</td>
                <td>
                    <asp:TextBox ID="txtCodUniversitario" runat="server" MaxLength="10" 
                        Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Clave</td>
                <td>
                    <asp:TextBox ID="txtClave" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="cmdAcceder" runat="server" Text="Acceder" Width="75px" />
                </td>
                <td>
                    <asp:Button ID="cmdCerrar" runat="server" Text="Cerrar" Width="75px" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
