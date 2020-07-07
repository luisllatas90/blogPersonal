<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmsubirarchivo.aspx.vb" Inherits="frmsubirarchivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link  rel ="stylesheet" href="estilo.css"/>
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    
        <table style="width: 576px; height: 128px" border="1" bordercolor="lemonchiffon">
            <tr>
                <td colspan="2" style="height: 17px" bgcolor=LemonChiffon >
                    <span style="font-size: 10pt; font-family: Courier New">
                    Adjuntar archivo a la rendición</span></td>
            </tr>
            <tr class="usatCeldaTitulo">
                <td colspan="2" style="height: 21px">
                    <span style="font-family: Courier New">Adjuntar Archivo :</span></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <span style="font-size: 9pt; font-family: Courier New">Archivo :</span></td>
                <td style="width: 337px">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="464px" /></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <span style="font-family: Courier New">
                    Descripción :</span></td>
                <td style="width: 337px">
                    <asp:TextBox ID="txtdescripcion" runat="server" Height="56px" Width="456px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align=center>
                    &nbsp;<asp:Button ID="cmdaceptar" runat="server" Text="Aceptar" Width="80px" Font-Names="Courier New" Font-Size="9pt" /><asp:Button ID="cmdcancelar"
                        runat="server" Text="Cancelar" Font-Names="Courier New" Font-Size="9pt" /></td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 15px">
                    <asp:Label ID="lblmensaje" runat="server" Width="560px" ForeColor="Maroon"></asp:Label></td>
            </tr>
        </table>
    
    
        <asp:HiddenField ID="HiddenField1" runat="server" />
    </form>
</body>
</html>
