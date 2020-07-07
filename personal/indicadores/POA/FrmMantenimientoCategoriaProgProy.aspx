<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoCategoriaProgProy.aspx.vb"
    Inherits="indicadores_POA_FrmMantenimientoCategoriaProgProy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            width: 168px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="foco" />

    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Registro de Categorías"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td class="style1">
                    <asp:HiddenField ID="hdcodigo" runat="server" Value="0" />
                    <asp:HiddenField ID="hdvigencia" runat="server" Value="0" />
                </td>
               
            </tr>
            <tr>
                <td class="style1">
                    <span lang="es-pe">Descripción:</span>
                </td>
                <td>
                    <asp:TextBox ID="txtNombrePoa" runat="server" Width="550px" MaxLength="200" CssClass="caja_poa"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblmensaje" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="cmdGuardarPoa" runat="server" CssClass="btnGuardar" Text="   Guardar"
                        ValidationGroup="Grupo1" />
                    &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="btnCancelar" Text="  Cancelar" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
