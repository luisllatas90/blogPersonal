<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ObservarSolicitud.aspx.vb" Inherits="SisSolicitudes_ObservarSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Solicitud como Observada</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">

    <table style="width:100%;">
        <tr>
            <td>
                Instancia a Derivar:
                <asp:DropDownList ID="CboInstancia" runat="server">
                    <asp:ListItem Value="1">Director de Escuela</asp:ListItem>
                    <asp:ListItem Value="2">Director Académico</asp:ListItem>
                    <asp:ListItem Value="3">Administrador General</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Observación:</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TxtObservacion" runat="server" Rows="5" TextMode="MultiLine" 
                    Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" Width="60px" 
                    CssClass="boton" />
&nbsp;<asp:Button ID="CmdCerrar" runat="server" Text="Cerrar" Width="60px" CssClass="boton" />
            &nbsp;</td>
        </tr>
    </table>

    </form>
</body>
</html>
