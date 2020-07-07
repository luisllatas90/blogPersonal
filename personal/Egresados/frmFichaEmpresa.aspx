<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFichaEmpresa.aspx.vb" Inherits="Egresado_frmFichaEmpresa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
        <tr>
            <td colspan="4" align="center" style=" font-size:small"><b>DATOS DE EMPRESA</b></td>
        </tr>
        <tr style="height:22px">
            <td style="width:15%"><b>Nombre:</b></td>
            <td>
                <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label>
            </td>
            <td style="width:15%"><b>Dirección:</b></td>
            <td>
                <asp:Label ID="lblDireccion" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="height:22px">
            <td><b>RUC:</b></td>
            <td>
                <asp:Label ID="lblRUC" runat="server" Text=""></asp:Label>
            </td>
            <td><b>Teléfono:</b></td>
            <td>
                <asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="height:22px">
            <td><b>Fax:</b></td>
            <td>
                <asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
            </td>
            <td><b>Correo:</b></td>
            <td>
                <asp:Label ID="lblCorreo" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="height:22px">
            <td colspan="4" align="right">
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="salir" Height="22px" Width="100px" /></td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdCodigo_pro" runat="server" />
    <asp:HiddenField ID="HdCodigo_ofe" runat="server" />
    </form>
</body>
</html>
