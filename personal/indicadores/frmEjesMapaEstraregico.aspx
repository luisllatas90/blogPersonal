<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEjesMapaEstraregico.aspx.vb" Inherits="indicadores_frmEjesMapaEstraregico" %>

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
        <td width="15%">&nbsp;<asp:Label ID="Label1" runat="server" Text="Plan Estrategico"></asp:Label>
        </td>
        <td width="85%"> 
                <asp:DropDownList ID="ddlPlan" runat="server" 
                Width="600px" AutoPostBack="True">
                </asp:DropDownList>
                <asp:Label ID="Label3" runat="server" Text="Año"></asp:Label>
                <asp:DropDownList ID="ddlPeriodo" runat="server" Width="120px" 
                    AutoPostBack="True">
                </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td width="15%">&nbsp;<asp:Label ID="Label2" runat="server" Text="Centro Costo"></asp:Label>
        </td>
        <td width="85%"> 
                <asp:DropDownList ID="ddlCentroCostoPlan" runat="server" AutoPostBack="True" 
                SkinID="ComboObligatorio" Width="600px">
                </asp:DropDownList>
                <asp:HiddenField ID="hfEje" runat="server" />
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
    
           <div id="tabs" runat="server" visible="true">
	        <table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
		<tr>
		    <td class="pestanabloqueada" id="Td2" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('0','','');">
                    <asp:LinkButton ID="lnkPers1" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
                    <asp:Label ID="lblPers1" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                    <asp:LinkButton ID="lnkPers2" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
                    <asp:Label ID="lblPers2" runat="server" Visible="False"></asp:Label>
            </td>
			<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPers3" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
                <asp:Label ID="lblPers3" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPers4" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
                <asp:Label ID="lblPers4" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPers5" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
                <asp:Label ID="lblPers5" runat="server" Visible="False"></asp:Label>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			    <td class="pestanaresaltada" id="Td1" align="center" style="height:25px;width:15%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPers6" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue"></asp:LinkButton>
                    <asp:Label ID="lblPers6" runat="server" Visible="False"></asp:Label>
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
