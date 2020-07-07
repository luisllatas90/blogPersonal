<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRevisionPersonal.aspx.vb" Inherits="personal_frmRevisionPersonal" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .0
        {
            background-color: #E6E6FA;
        }
        .1
        {
            background-color: #FFFCBF;
        }
        .2
        {
            background-color: #D9ECFF;
        }
        .3
        {
            background-color: #C7E0CE;
        }
        
        .5
        {
            background-color: #FFCC00;
        }
        .6
        {
            background-color: #F8C076;
        }
        .4
        {
            background-color: #CCFF66;
        }
        </style>

        <!--
        <link rel="stylesheet" type="text/css" href="../Scripts/css/bootstrap.min.css" />
        -->
        
        <script src="script/jquery-1.12.3.min.js" type="text/javascript"></script>
        
        <script type="text/javascript">
            $(document).ready(function() {
                $("#txtPersonal").focus();

                $("#txtPersonal").keypress(function(event) {
                    var keycode = (event.keyCode ? event.keyCode : event.which);
                    if (keycode == '13') {                        
                        $('#btnBuscar').trigger('click');
                    }
                });
            });
            
        </script>        
    <title>Busqueda de Personal</title>
</head>
    <body>
    <form id="form1" runat="server">        

    <table width="100%" >
        <tr>
            <td colspan="2" valign="middle" 
                    style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF; background-color: #D2D2FF;" height="40px">
                <asp:Label ID="Label2" runat="server" Text="Búsqueda de Personal" 
                    Font-Bold="False" Font-Size="Large" ForeColor="#3333CC"></asp:Label>
             </td>            
        </tr>
        <tr>
              <td>                              
                <asp:Label ID="Label1" runat="server" Text="Personal: "></asp:Label>
                <asp:TextBox ID="txtPersonal" runat="server" Width="40%" Height="22px" ></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="120px" Height="22px" OnClick="btnBuscar_Click" />                                   
                <hr style="border-right: darkred 1px solid; border-top: darkred 1px solid; border-left: darkred 1px solid; border-bottom: darkred 1px solid; height: 1px" />
             </td>
         </tr>         
        <tr>
            <td align="center">            
            <asp:GridView ID="gvListaPersonal" runat="server" CellPadding="4" 
                                ForeColor="#333333"  Width="100%"
                            HorizontalAlign="Center" AutoGenerateColumns="False" BorderStyle="Solid" 
                            DataKeyNames="codigo_per" Font-Size="X-Small" AllowPaging="True" 
                    PageSize="5">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField DataField="codigo_Per" HeaderText="Código" />
                                    <asp:BoundField DataField="personal" HeaderText="Nombre" />
                                    <asp:BoundField DataField="CentroCostos" HeaderText="CentroCostos" />
                                    <asp:BoundField DataField="TipoPersona" HeaderText="TipoPersona" />
                                    <asp:BoundField DataField="Dedicacion" HeaderText="Dedicación" />
                                    <asp:BoundField DataField="EstadoPlanilla" HeaderText="Estado" />
                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                                                        SelectImageUrl="../../images/resultados.gif" />
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
            </td>
        </tr>        
    </table>
    <br />
    <div id="tabs" runat="server" visible="false">
	<table cellspacing="0" cellpadding="0" style="border-collapse:collapse; border-color:#111111; width:100%">
		<tr>
			<td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:16%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkDatosPersona" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Datos Personales</asp:LinkButton>
            </td>			
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="Td1" align="center" style="height:25px;width:16%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkHorario" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Horario</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="Td2" align="center" style="height:25px;width:16%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkHorarioAdm" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Horario Académico</asp:LinkButton>
            </td>
			<td style="height:25px;width:55%" class="bordeinf">&nbsp;</td>
		</tr>
		<tr>
    	<td style="height:600px;width:100%" valign="top" colspan="10" class="pestanarevez">
			<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" runat="server">
			</iframe>
		</td>
	  </tr>
	</table>
    </div>
    <asp:Label ID="lblMensaje" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblcodigo_per" runat="server" Visible="False"></asp:Label>
    </form>
</body>
</html>
