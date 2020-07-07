<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultasVarias.aspx.vb" Inherits="presupuesto_consultas_ConsultasVarias" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" border="0" cellspacing="0">
            <tr>
                <td class="CeldaImagen" colspan="2" height="25px" 
                    style="height: 25px; font-weight: 700;">
                    &nbsp;REPORTES DE PRESUPUESTO POR CENTRO DE COSTOS O CUENTA CONTABLE</td>
            </tr>
            <tr bgcolor="#999999" style="height:1px;">
                <td>
                    </td>
                <td align="right">
                     </td>
            </tr>
            <tr class="titulocel">
                <td>
                    &nbsp;Proceso&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :                                 <asp:DropDownList ID="cboProceso" runat="server">
                                </asp:DropDownList>
                            </td>
                <td align="right">
                               
                            &nbsp;</td>
            </tr>
            <tr class="titulocel">
                <td>
                                &nbsp;Centro de costo:
                                <asp:DropDownList ID="cboCentroCostos" runat="server">
                                </asp:DropDownList>

                                </td>
                <td align="right">
                                
                                &nbsp;</td>
            </tr>
            <tr class="titulocel">
                <td>
                                &nbsp;Opción&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                :
                                <asp:DropDownList ID="cboOpcion" runat="server">
                                    <asp:ListItem Value="1">Resumen total por programa presupuestal</asp:ListItem>
                                    <asp:ListItem Value="2">Resumen mensual por programa presupuestal</asp:ListItem>
                                    <asp:ListItem Value="3">Resumen total por cuenta contable</asp:ListItem>
                                    <asp:ListItem Value="4">Resumen mensual por cuenta contable</asp:ListItem>
                                    <asp:ListItem Value="5">Detallado</asp:ListItem>
                                </asp:DropDownList>
 				<asp:Button ID="cmdConsultar" runat="server" CssClass="Buscar" Text="    Consultar" 
                                    Width="80px" Height="22px" BorderStyle="Outset" /> 
				<asp:Button ID="cmdExportar" runat="server" BackColor="#FEFFE1" 
                                    CssClass="ExportarAExcel" Text="    Exportar" Width="80px"  Height="22px" BorderStyle="Outset" />
                                </td>
                <td align="right">
                                &nbsp;</td>
            </tr>
            <tr bgcolor="#999999" style="height:1px;">
                <td>
                    </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvDetalle" runat="server" ShowFooter="True">
                        <FooterStyle BackColor="#E8EEF7" ForeColor="#3366CC" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" Text="No se encontraron registros" 
                                ForeColor="Red"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="TituloTabla" Height="20px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
