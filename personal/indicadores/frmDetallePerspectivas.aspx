<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDetallePerspectivas.aspx.vb" Inherits="indicadores_frmDetallePerspectivas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link rel="stylesheet" type="text/css" href="../../private/estilo.css">
        <script language="JavaScript" src="../../private/funciones.js"></script>  
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
    <tr>
        <td>       
        </td>
    </tr>
    <tr>
        <td width="15%">&nbsp;<asp:Label ID="Label1" runat="server" Text="Objetivos"></asp:Label>
        </td>
        <td width="85%"> 
                <asp:DropDownList ID="ddlObjetivos" runat="server" AutoPostBack="True" 
                SkinID="ComboObligatorio" Width="100%">
                </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="15%">&nbsp;<asp:Label ID="Label2" runat="server" Text="Indicadores"></asp:Label>
        </td>
        <td width="85%"> 
                <asp:DropDownList ID="ddlIndicadores" runat="server" AutoPostBack="True" 
                SkinID="ComboObligatorio" Width="100%">
                </asp:DropDownList>
        </td>
    </tr>
    <tr id="Tr1" runat="server">
    <td width="15%">&nbsp;</td>
                <td width="85%">
                    <asp:Panel ID="trResultados" runat="server" Visible="false" Height="300px" ScrollBars="Vertical">
                     </asp:Panel>                  
                </td>
    </tr>
    </table>
    <br />
    <div id="tabs" runat="server" visible="false">
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
		<tr>
			<td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkDatosEvento" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Gráfico Nº1</asp:LinkButton>
            </td>
			<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPreInscripcion" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Gráfico Nº 2</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkInscripciones" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">-</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkRegisMateriales" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">-</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			    <td class="pestanaresaltada" id="Td1" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkprogActividades" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">-</asp:LinkButton>
            </td>                        
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			    <td class="pestanaresaltada" id="Td2" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">-</asp:LinkButton>
            </td>
		</tr>
		<tr>
    	<td style="height:600px;width:100%" valign="top" colspan="12" class="pestanarevez">
			<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" runat="server">
			</iframe>
		</td>
	  </tr>
	</table>
    </div>
    </div>
    </form>
</body>
</html>
