<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Detallepresupuesto.aspx.vb" Inherits="presupuesto_areas_detallePresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
                    <table width="100%" align="center" border="0" cellspacing="0">
                        <tr class="CeldaImagen">
                            <td colspan="2" style="height: 25px; font-weight: 700;">
                                REPORTE DE PRESUPUESTO POR CENTRO DE COSTO</td>
                        </tr>
                        <tr bgcolor="#999999" style="height:1px;">
                            <td>
                            </td>
                             <td>
                            </td>
                         </tr>
                        <tr class="titulocel">
                            <td>
                                Proceso&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                <asp:DropDownList ID="cboProceso" runat="server" AutoPostBack="true"> 
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                
                            </td>
                        </tr>
                        <tr class="titulocel">
                            <td>
                                Centro de costo:
                                <asp:DropDownList ID="cboCentroCostos" runat="server">
                                </asp:DropDownList>
                                </td>
                            <td align="right">
                                
                                </td>
                        </tr>
                        <tr>
                            <td height="20" colspan="2" class="titulocel">
                                <asp:Button ID="cmdConsultar" runat="server" CssClass="Buscar" Text="    Consultar" 
                                    Width="80px" Height="22px" /> 
				<asp:Button ID="cmdExportar" runat="server" BackColor="#FEFFE1" 
                                    CssClass="ExportarAExcel" Text="    Exportar" Width="80px" Height="22px" />
			    </td>
                        </tr>
                        <tr>
                            <td height="1px" colspan="2" bgcolor="#999999">
                                </td>
                        </tr>
                        <tr>
                            <td height="20" colspan="2">
                                <table style="width:100%; background-color: #FFFFEA;" border="0" 
                                    cellpadding="3" cellspacing="0" id="tblDetalle">
                                    <tr>
                                        <td>
                                            Periodo presupuestal:
                                            <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Centro de costos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                                            <asp:Label ID="lblCecos" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Estado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;:
                                            <asp:Label ID="lblEstado" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="1px" bgcolor="#999999">
                                             </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr >
                            <td colspan="2" style="font-weight: 700">
                                <asp:Label ID="lblIngresos" runat="server" Text="INGRESOS" Visible="False" 
                                    Font-Underline="True" ></asp:Label>
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTechoIng" runat="server" Text="- Techo: " Visible="False" 
                                    ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTotalIng" runat="server" Text="- Techo: " Visible="False" 
                                    ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblDiferenciaIng" runat="server" Text="- Techo: " 
                                    Visible="False" ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvDetalle" runat="server" 
                                    Width="100%" ShowFooter="True" Font-Size="8pt">
                                    <RowStyle Height="20px" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                            Text="No se encontraron registros"></asp:Label>
                                    </EmptyDataTemplate>
                                    <FooterStyle HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                                    <HeaderStyle CssClass="TituloTabla" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="font-weight: 700">
                                <asp:Label ID="lblEgresos" runat="server" Text="EGRESOS" Visible="False" 
                                    Font-Underline="True"></asp:Label>
                            &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTechoEgr" runat="server" Text="- Techo: " Visible="False" 
                                    ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblTotalEgr" runat="server" Text="- Techo: " Visible="False" 
                                    ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblDiferenciaEgr" runat="server" Text="- Techo: " 
                                    Visible="False" ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvEgresos" runat="server" ShowFooter="True" Width="100%" 
                                    Font-Size="8pt">
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                            Text="No se encontraron registros"></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="TituloTabla" />
                                <FooterStyle HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td  style="font-weight: bold" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td  style="font-weight: bold" colspan="2">
                                <asp:Label ID="lblEgresos0" runat="server" Text="DIFERENCIA" Visible="False" 
                                    Font-Underline="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  style="font-weight: bold" colspan="2">
                                <asp:Label ID="lblDiferencia" runat="server"></asp:Label>
                            </td>
                        </tr>
                        </table>
    
    </div>
    <asp:HiddenField ID="hddTotalIng" runat="server" Value="0" />
    <asp:HiddenField ID="hddTotalEgr" runat="server" Value="0" />
    </form>
</body>
</html>
