<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstdocumentos.aspx.vb" Inherits="librerianet_aulavirtual_lstdocumentos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    
    Documentos publicados<br />
    <br />
    <br />
    <table class="style1">
        <tr>
            <td>
                Carpetas</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
            <asp:TreeView ID="trw" runat="server" Font-Names="Verdana" Font-Size="8pt">
            </asp:TreeView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <asp:HiddenField ID="hdIdUsuario" runat="server" />
    <asp:HiddenField ID="hdidcursovirtual" runat="server" />
    <asp:HiddenField ID="hdcodigo_tfu" runat="server" />
    
    </form>
</body>
</html>
