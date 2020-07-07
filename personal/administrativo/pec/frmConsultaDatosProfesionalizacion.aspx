<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaDatosProfesionalizacion.aspx.vb" Inherits="administrativo_pec_frmConsultaDatosProfesionalizacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
        <script language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tabs" runat="server" visible="true">
	    <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
		<tr>
			<td class="pestanabloqueada" id="tab" align="center" style="height:50px;width:12%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkDatosEvento" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Historial Académico</asp:LinkButton>
            </td>
			<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:50px;width:12%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPreInscripcion" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Estado de Cuenta</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
		</tr>
		
		<tr>
		    <td style="height:600px;width:100%" valign="top" colspan="14" class="pestanarevez">
			    <iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" runat="server">
			    </iframe>
		    </td>
	    </tr>
	    
	</table>
    </div>
    </form>
</body>
</html>
