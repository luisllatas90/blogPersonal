<%@ Page Language="VB" AutoEventWireup="false" CodeFile="nuevocomentario.aspx.vb" Inherits="proponente_nuevocomentario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            height: 13px;
        }
        .style2
        {
            height: 43px;
        }
        .style3
        {
            width: 12%;
        }
        .style4
        {
            height: 13px;
            width: 12%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="font-weight: bold; font-size: medium">
    
        Registro de Comentario<br />
        <br />
        <table class="contornotabla" width="70%">
            <tr>
                <td class="style3">
                    &nbsp;</td>
                <td width="2%">
                    &nbsp;</td>
                <td width="70%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    Asunto</td>
                <td>
                    &nbsp;</td>
                <td align="left">
                    <asp:TextBox ID="txtAsunto" runat="server" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" class="style3">
                    Contenido</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:TextBox ID="txtComentario" runat="server" Height="109px" Rows="4" 
                        Width="96%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                </td>
                <td class="style1">
                </td>
                <td class="style1">
                </td>
            </tr>
            <tr>
                <td bgcolor="#EAEDF1" class="style2" colspan="3" 
                    style="border-width: 1px; border-color: #000000; border-top-style: solid;">
                    <asp:Button ID="cmdAceptar" runat="server" CssClass="guardar_prp" Height="32px" 
                        Text="          Guardar" Width="100px" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" 
                        Height="32px" Text="          Cancelar" Width="100px" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
