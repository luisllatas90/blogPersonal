<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datospropuesta_POA.aspx.vb"
    Inherits="proponente_datospropuesta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css">

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
                    <table class="style1" style="width: 100%">
                        <tr>
                            <td width="20%">
                                Nº de Versión
                            </td>
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
                                                        Estado de la Propuesta
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" class="style3" style="font-weight: bold; color: #000000;" width="18%">
                                                        C. Facultad:
                                                    </td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoFacultad" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                                    </td>
                                                    <td align="right" class="style3" style="font-weight: bold; color: #000000" width="18%">
                                                        Rectorado:
                                                    </td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoRectorado" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                                    </td>
                                                    <%--<td align="right" class="style3" style="color: #000000; font-weight: bold" width="24%">C. Universitario:</td>
                                                    <td align="center" class="style3" width="13%">
                                                        <asp:Label ID="lblEstadoConsejo" runat="server" Font-Bold="True" ForeColor="#3333FF"></asp:Label>
                                                    </td>--%>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblPropuesta" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Blue"></asp:Label>
                                <asp:HiddenField ID="HdCodigo_acp" runat="server" />
                                <asp:HiddenField ID="HdCodigo_tpr" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Proponente:
                            </td>
                            <td>
                                <asp:Label ID="lblProponente" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Tipo Propuesta:
                            </td>
                            <td>
                                <asp:Label ID="lblTipoPropuesta" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Área:
                            </td>
                            <td>
                                <asp:Label ID="lblArea" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="20%">
                                Instancia:
                            </td>
                            <td>
                                <asp:Label ID="lblInstancia" runat="server" Font-Bold="True" ForeColor="#FF6600"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TDMargen" runat="server">
                            <td width="20%">
                                Margen:
                            </td>
                            <td>
                                <asp:Label ID="lblMargen" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TDRentabilidad" runat="server">
                            <td width="20%">
                                Rentabilidad (%):
                            </td>
                            <td style="font-weight: bold; color: #000000;">
                                <asp:Label ID="lblRentabilidad" runat="server" Font-Bold="True" Style="font-size: medium"></asp:Label>
                                <asp:Label ID="lblMsgRentabilidad"  
                                    runat="server" Font-Bold="True" style="font-size: x-small" ForeColor="Red" 
                                    Visible="False" ></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td width="20%">
                                Tipo Cambio:
                            </td>
                            <td>
                                <asp:Label ID="lblSimbolo" runat="server"></asp:Label>
                                &nbsp;(S/.
                                <asp:Label ID="lblCambio" runat="server"></asp:Label>
                                )
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td colspan="2">
                                <table class="style1">
                                    <tr>
                                        <td width="16%">
                                            Ingreso
                                        </td>
                                        <td width="16%">
                                            <asp:Label ID="lblSim0" runat="server"></asp:Label>
                                            <asp:Label ID="lblIngreso" runat="server"></asp:Label>
                                        </td>
                                        <td width="16%">
                                            Egreso
                                        </td>
                                        <td width="16%">
                                            <asp:Label ID="lblSim1" runat="server"></asp:Label>
                                            <asp:Label ID="lblEgreso" runat="server"></asp:Label>
                                        </td>
                                        <td width="16%">
                                            Utilidad
                                        </td>
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
                                    <tr id="TDImportes" runat="server">
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            Ingreso
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            S/.
                                            <asp:Label ID="lblIngresoMN" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            Egreso
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            S/.
                                            <asp:Label ID="lblEgresoMN" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="16%">
                                            Utilidad
                                        </td>
                                        <td style="font-weight: bold; color: #000000;" width="20%">
                                            S/.
                                            <asp:Label ID="lblUtilidadMN" runat="server"></asp:Label>
                                            <asp:Label ID="lblMsgUtilidad"  
                                    runat="server" Font-Bold="True" style="font-size: x-small" ForeColor="Red" 
                                    Visible="False" ></asp:Label>
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
                                Archivos Adjuntos
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" style="font-weight: bold">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:ImageButton ID="imgPresupuesto" runat="server" ImageUrl="~/administrativo/propuestas2/images/ext/xls.gif"
                                                Style="width: 16px" />
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btn_MostrarPresupuesto" runat="server" Text="PRESUPUESTO POA" CssClass="agregar2"
                                                Width="160px" Height="22px" BackColor="White" BorderColor="White" 
                                                Font-Bold="True" Font-Strikeout="False" Font-Underline="False" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:GridView ID="GridView1" runat="server" Width="100%" AutoGenerateColumns="False"
                                    ShowHeader="False" DataKeyNames="nombre_apr" GridLines="Horizontal">
                                    <RowStyle Height="30px" BorderStyle="None" />
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_apr" HeaderText="Codigo_apr" Visible="False">
                                            <HeaderStyle Width="0%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_apr" HeaderText="Archivo" Visible="False">
                                            <HeaderStyle Width="0%" />
                                            <ItemStyle Width="50%" />
                                        </asp:BoundField>
                                        <asp:ImageField DataImageUrlField="extension" DataImageUrlFormatString="../images/ext/{0}.gif"
                                            HeaderText="imagen" ConvertEmptyStringToNull="False">
                                            <HeaderStyle Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20px" />
                                        </asp:ImageField>
                                        <asp:HyperLinkField DataNavigateUrlFields="archivo" DataNavigateUrlFormatString="{0}"
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
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Black" Text="Resumen:"></asp:Label>
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
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Black" Text="Importancia:"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblImportancia" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table align="center">
                        <tr id="TDPresupuesto" runat="server">
                            <td colspan="7" style="font-weight: 700; font-size: medium; text-align: left">
                                PRESUPUESTO
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:GridView ID="dgv_PresupuestoIngreso" runat="server" Width="100%" AutoGenerateColumns="False"
                                    Visible="False" HorizontalAlign="Center" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad" />
                                        <asp:BoundField DataField="item" HeaderText="Item">
                                            <HeaderStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C2}">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="SubTotal" DataFormatString="{0:C2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        INGRESOS: NO SE HA REGISTRADO NINGUN ITEM EN PRESUPUESTO
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" />
                                    <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                                <asp:GridView ID="dgv_Presupuesto" runat="server" Width="100%" AutoGenerateColumns="False"
                                    Visible="False" HorizontalAlign="Center" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="tipo" HeaderText="Tipo" Visible="False">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="actividad" HeaderText="Actividad" />
                                        <asp:BoundField DataField="item" HeaderText="Item">
                                            <HeaderStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                                            <HeaderStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad">
                                            <HeaderStyle Width="70px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="precio" HeaderText="Precio" DataFormatString="{0:C2}">
                                            <HeaderStyle Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="subtotal" HeaderText="SubTotal" DataFormatString="{0:C2}">
                                            <HeaderStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="porcentaje" HeaderText="%">
                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_dap" HeaderText="codigo_dap" Visible="False" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        EGRESOS: NO SE HA REGISTRADO NINGUN ITEM EN PRESUPUESTO
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" />
                                    <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
