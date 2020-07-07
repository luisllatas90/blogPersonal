<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaPlan.aspx.vb" Inherits="PlanProyecto_frmListaPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">        
       <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td style="width: 20%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                    height="40px" bgcolor="#E6E6FA">
                <asp:Label ID="lblTitulo" runat="server" Text="Planes" 
                    Font-Bold="True" Font-Size="11pt"></asp:Label></td>
                <td style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" 
                    height="40px" bgcolor="#E6E6FA"><asp:DropDownList ID="dpProyectos" runat="server" Width="50%" 
                        AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>            
            <tr>
                <td style="width:15%">
                    &nbsp;&nbsp;</td>
                <td>                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" 
                        Text=""></asp:Label>
                    </td>                
            </tr>
        </table>
        <br />
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Grupo" 
           CssClass="agregar2" Width="100px" Height="22px" />
        <br /><br />
        <table cellspacing="0" cellpadding="0" style="border-collapse: collapse; bordercolor: #111111;width:100%">
            <tr>
            <td class="pestanabloqueada" id="tab" align="center" style="height:25px; onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkResumen" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue" Font-Size="Small">Resumen</asp:LinkButton></td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
            <td class="pestanabloqueada" id="tab" align="center" style="height:25px; onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkConfiguracion" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue" Font-Size="Small">Configuración</asp:LinkButton></td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
            <td class="pestanabloqueada" id="Td1" align="center" style="height:25px; onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkGrupo" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue" Font-Size="Small">Grupo</asp:LinkButton></td>            
        </tr>
        <tr>
            <td colspan="5" class="pestanarevez" style="height: 500px; width: 100%">
                <iframe id="fradetalle" width="100%" height="100%" border="0" frameborder="0" runat="server">
	            </iframe>  
            </td>
        </tr>
        </table>
    </form>
    <p>
&nbsp;</p>
</body>
</html>
