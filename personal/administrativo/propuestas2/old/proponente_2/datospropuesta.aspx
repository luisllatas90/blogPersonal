<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datospropuesta.aspx.vb" Inherits="proponente_datospropuesta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>
<script type="text/javascript"> 
    function HabilitarBoton(tipo,fila)
  	{
    	if (document.form1.cmdModificar!=undefined){
            document.form1.cmdModificar.disabled=false     
    	}

    	SeleccionarFila();
    	AbrirPestana(0,'datospropuesta.aspx');
	}
	
    function AbrirPestana(tab,pagina)
    {
        if (document.form1.txtelegido.value>0){
	        ResaltarPestana(tab,'','')
	        
	        fradetalle.location.href=pagina + "?codigo_prp=" + document.form1.txtelegido.value + "&codigo_per=<%=request.querystring("id")%>"
	    }
    }


</script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 17px;
        }
        .style3
        {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style2" width="75%">
                    <table class="style1">
                        <tr>
                            <td width="20%">
                                Nº de Versión</td>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td width="16%">
                                <asp:DropDownList ID="ddlversiones" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                                        </td>
                                        <td width="84%">
                                            <table cellpadding="0" cellspacing="0" class="contornotabla">
                                                <tr>
                                                    <td align="center" colspan="6" style="font-weight: bold; color: #000000;">
                                                        Estado de la Propuesta</td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="style3" style="font-weight: bold; color: #000000;" 
                                                        width="18%">
                                                        C. Facultad:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoFacultad" runat="server" Font-Bold="True" 
                                                            ForeColor="#3333FF"></asp:Label>
                                                    </td>
                                                    <td align="right" class="style3" style="font-weight: bold; color: #000000" 
                                                        width="18%">
                                                        Rectorado:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoRectorado" runat="server" Font-Bold="True" 
                                                            ForeColor="#3333FF"></asp:Label>
                                                    </td>
                                                    <td align="right" class="style3" style="color: #000000; font-weight: bold" 
                                                        width="24%">
                                                        C. Universitario:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoConsejo" runat="server" Font-Bold="True" 
                                                            ForeColor="#3333FF"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblPropuesta" runat="server" Font-Bold="True" Font-Size="Small" 
                                    ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Proponente:</td>
                            <td>
                                <asp:Label ID="lblProponente" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo Propuesta:
                            </td>
                            <td>
                                <asp:Label ID="lblTipoPropuesta" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Área:</td>
                            <td>
                                <asp:Label ID="lblArea" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Instancia:
                            </td>
                            <td>
                                <asp:Label ID="lblInstancia" runat="server" Font-Bold="True" 
                                    ForeColor="#FF6600"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Tipo Cambio:</td>
                            <td>
                                <asp:Label ID="lblSimbolo" runat="server"></asp:Label>
&nbsp;(S/.
                                <asp:Label ID="lblCambio" runat="server"></asp:Label>
                                )</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="style1">
                                    <tr>
                                        <td width="16%">
                                            Ingreso
                                        </td>
                                        <td width="16%">
                                            <asp:Label ID="lblSim0" runat="server"></asp:Label>
                                            <asp:Label ID="lblIngreso" runat="server"></asp:Label>
&nbsp;</td>
                                        <td width="16%">
                                            Egreso</td>
                                        <td width="16%">
                                            <asp:Label ID="lblSim1" runat="server"></asp:Label>
                                            <asp:Label ID="lblEgreso" runat="server"></asp:Label>
                                        </td>
                                        <td width="16%">
                                            Utilidad</td>
                                        <td width="20%">
                                            <asp:Label ID="lblSim2" runat="server"></asp:Label>
                                            <asp:Label ID="lblUtilidad" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="style1">
                                    <tr>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            Ingreso</td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            S/.
                                            <asp:Label ID="lblIngresoMN" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            Egreso</td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            S/.
                                            <asp:Label ID="lblEgresoMN" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            Utilidad</td>
                                        <td style="font-weight: bold; color: #000000;" width="20%">
                                            S/.
                                            <asp:Label ID="lblUtilidadMN" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="contornotabla" width="25%" valign="top">
                    <table class="style1">
                        <tr>
                            <td style="font-size: small; font-weight: bold; color: #000080">
&nbsp;Archivos Adjuntos</td>
                        </tr>
                        <tr>
                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" ShowHeader="False" DataKeyNames="nombre_apr" GridLines="Horizontal">
                                                    <RowStyle Height="30px" BorderStyle="None" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Codigo_apr" HeaderText="Codigo_apr" 
                                                            Visible="False" >
                                                            <HeaderStyle Width="0%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre_apr" HeaderText="Archivo" Visible="False" >
                                                            <HeaderStyle Width="0%" />
                                                            <ItemStyle Width="50%" />
                                                        </asp:BoundField>
                                                        <asp:ImageField DataImageUrlField="extension" DataImageUrlFormatString="../images/ext/{0}.gif"
                                HeaderText="imagen" ConvertEmptyStringToNull="False">
                                                            <HeaderStyle Width="10%" />
                                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                                        </asp:ImageField>
                                                        <asp:HyperLinkField DataNavigateUrlFields="archivo" 
                                                            DataNavigateUrlFormatString="{0}"
                                                            DataTextField="descripcion_apr" HeaderText="Descripción" Target="_blank" />
                                                    </Columns>
                                                </asp:GridView>
                                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" 
                        Text="Resumen:"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblResumen" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black" 
                        Text="Importancia:"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblImportancia" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
