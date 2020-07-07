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
            width: 100%;
        }
        .style3
        {
            height: 22px;
        }
        .style4
        {
            width: 100%;
            border: 3px solid #000080;
        }
        .style5
        {
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style2" width="75%" colspan="2">
                    <table bgcolor="#F0F0F0" cellpadding="0" cellspacing="0" class="style4">
                        <tr>
                            <td class="style5" width="16%">
                                <asp:Button ID="cmdCalificaciones" runat="server" CssClass="enviarpropuesta" 
                                    Height="41px" Text="Ver Calificaciones" Width="174px" />
                            </td>
                            <td class="style5" width="16%">
                                <asp:Button ID="cmdComentarios" runat="server" CssClass="grabacion_prp" 
                                    Height="41px" Text="Ver Comentarios" Width="174px" />
                            </td>
                            <td class="style5" width="16%">
                                <asp:Button ID="cmdAcuerdos" runat="server" CssClass="conforme1" Height="41px" 
                                    Text="Acuerdos" Width="131px" />
                            </td>
                            <td class="style5" width="16%">
                                <asp:Button ID="cmdDiscusion" runat="server" CssClass="modificar_1" 
                                    Height="41px" Text="Discusión" Width="124px" />
                            </td>
                            <td class="style5" width="16%">
                            </td>
                            <td align="right" class="style5" width="20%">
                                <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                                    Font-Size="Medium" ForeColor="Maroon">Volver a Agenda</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style2" width="75%" colspan="2">
                    <asp:Label ID="lblinstancia" runat="server" ForeColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2" width="75%" colspan="2">
                                <asp:Label ID="lblPropuesta" runat="server" Font-Bold="True" Font-Size="X-Large" 
                                    ForeColor="Blue"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2" width="75%">
                    &nbsp;</td>
                <td width="25%" valign="top" align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style2" width="75%" valign="top">
                    <table class="style1">
                        <tr>
                            <td width="20%">
                                <asp:Label ID="Label4" runat="server" Font-Size="Medium" ForeColor="Black" 
                                    Text="Nº de Versión:"></asp:Label>
                            </td>
                            <td>
                                <table cellpadding="0" cellspacing="0" class="style1">
                                    <tr>
                                        <td width="16%">
                                <asp:DropDownList ID="ddlversiones" runat="server" AutoPostBack="True" Font-Size="Medium">
                                </asp:DropDownList>
                                        </td>
                                        <td width="84%">
                                            <table cellpadding="0" cellspacing="0" class="contornotabla">
                                                <tr>
                                                    <td align="center" colspan="6" 
                                                        style="font-weight: bold; color: #000000; font-size: medium;">
                                                        Estado de la Propuesta</td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="style3" style="font-weight: bold; color: #000000; font-size: medium;" 
                                                        width="18%">
                                                        C. Facultad:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoFacultad" runat="server" Font-Bold="True" 
                                                            ForeColor="#3333FF" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td align="right" class="style3" style="font-weight: bold; color: #000000; font-size: medium;" 
                                                        width="18%">
                                                        Rectorado:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoRectorado" runat="server" Font-Bold="True" 
                                                            ForeColor="#3333FF" Font-Size="Medium"></asp:Label>
                                                    </td>
                                                    <td align="right" class="style3" style="color: #000000; font-weight: bold; font-size: medium;" 
                                                        width="24%">
                                                        C. Universitario:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoConsejo" runat="server" Font-Bold="True" 
                                                            ForeColor="#3333FF" Font-Size="Medium"></asp:Label>
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
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Size="Medium" ForeColor="Black" 
                                    Text="Proponente:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProponente" runat="server" Font-Bold="True" 
                                    Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Font-Size="Medium" ForeColor="Black" 
                                    Text="Tipo Propuesta:"></asp:Label>
&nbsp;</td>
                            <td>
                                <asp:Label ID="lblTipoPropuesta" runat="server" ForeColor="Black" 
                                    Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Size="Medium" ForeColor="Black" 
                                    Text="Área:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblArea" runat="server" Font-Bold="True" ForeColor="Black" 
                                    Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Size="Medium" ForeColor="Black" 
                                    Text="Tipo Cambio:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSimbolo" runat="server" Font-Size="Medium"></asp:Label>
&nbsp;(S/.
                                <asp:Label ID="lblCambio" runat="server" Font-Size="Medium"></asp:Label>
                                )</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table class="style1">
                                    <tr>
                                        <td width="16%">
                                            &nbsp;<asp:Label ID="Label8" runat="server" Font-Size="Medium" 
                                                ForeColor="Black" Text="Ingreso"></asp:Label>
                                        </td>
                                        <td width="16%">
                                            <asp:Label ID="lblSim0" runat="server" Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="lblIngreso" runat="server" Font-Size="Medium"></asp:Label>
&nbsp;</td>
                                        <td width="16%">
                                            <asp:Label ID="Label10" runat="server" Font-Size="Medium" ForeColor="Black" 
                                                Text="Egreso"></asp:Label>
                                        </td>
                                        <td width="16%">
                                            <asp:Label ID="lblSim1" runat="server" Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="lblEgreso" runat="server" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td width="16%">
                                            <asp:Label ID="Label12" runat="server" Font-Size="Medium" ForeColor="Black" 
                                                Text="Utilidad"></asp:Label>
                                        </td>
                                        <td width="20%">
                                            <asp:Label ID="lblSim2" runat="server" Font-Size="Medium"></asp:Label>
                                            <asp:Label ID="lblUtilidad" runat="server" Font-Size="Medium"></asp:Label>
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
                                            <asp:Label ID="Label9" runat="server" Font-Size="Medium" ForeColor="Black" 
                                                Text="Ingreso"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            S/.
                                            <asp:Label ID="lblIngresoMN" runat="server" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            <asp:Label ID="Label11" runat="server" Font-Size="Medium" ForeColor="Black" 
                                                Text="Egreso"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            S/.
                                            <asp:Label ID="lblEgresoMN" runat="server" Font-Size="Medium"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            <asp:Label ID="Label13" runat="server" Font-Size="Medium" ForeColor="Black" 
                                                Text="Utilidad"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="20%">
                                            S/.
                                            <asp:Label ID="lblUtilidadMN" runat="server" Font-Size="Medium"></asp:Label>
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
                            <td style="font-size: medium; font-weight: bold; color: #000080">
&nbsp;Archivos Adjuntos</td>
                        </tr>
                        <tr>
                            <td>
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" 
                        AutoGenerateColumns="False" ShowHeader="False" DataKeyNames="nombre_apr" GridLines="Horizontal">
                                                    <RowStyle Height="30px" BorderStyle="None" Font-Size="Medium" />
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
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" 
                        Text="Resumen:" Font-Size="Large"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblResumen" runat="server" Font-Size="X-Large" 
                        ForeColor="#003366"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black" 
                        Text="Importancia:" Font-Size="Large"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblImportancia" runat="server" Font-Size="X-Large" 
                        ForeColor="#003366"></asp:Label>
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
