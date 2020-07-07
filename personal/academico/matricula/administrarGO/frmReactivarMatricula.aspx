<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReactivarMatricula.aspx.vb" Inherits="academico_matricula_administrar_frmReactivarMatricula" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" /> 
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td height="5%" colspan="4" valign="top" width="100%"><span class="usatTitulo">
                Búsqueda de estudiantes de Pregrado</span>
                </td>
            </tr>
            <tr>
                <td style="width:15%">
                    <asp:Label ID="Label1" runat="server" Text="Buscar por:"></asp:Label>
                </td>
                <td style="width:17%">
                    <asp:DropDownList ID="ddpTipo" runat="server">
                        <asp:ListItem Value="N">Apellidos y Nombres</asp:ListItem>
                        <asp:ListItem Value="C">Codigo Universitario</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:40%">
                    <asp:TextBox ID="txtBuscar" runat="server" Width="80%"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" Height="22px" CssClass="buscar2" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height:5%">
                    <asp:Label ID="lblAviso" runat="server" Font-Bold="True" Font-Size="Small" 
                        ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                <asp:GridView ID="gvAlumnos" runat="server" Width="100%" GridLines="Horizontal" 
                        HeaderStyle-BorderColor="Red" HeaderStyle-BorderStyle="Solid"
                        HeaderStyle-BorderWidth="1px"
                        BorderStyle="Solid" BorderColor="Black"                        
                        AutoGenerateColumns="False" AllowPaging="True">
                        <Columns>
                            <asp:BoundField DataField="codigo_Alu" HeaderText="ID" >
                                <ItemStyle HorizontalAlign="Center" Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Codigo">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Nombres" HeaderText="Apellidos y Nombres">
                                <ItemStyle Width="34%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional">
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="Estado" >
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="deuda" HeaderText="Deuda">
                                <ItemStyle HorizontalAlign="Center" Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_Mat" HeaderText="codigo_Mat" 
                                Visible="False" />
                            <asp:CommandField ButtonType="Image" HeaderText="Ver" 
                                SelectImageUrl="../../../../images/credito.gif" SelectText="Ver" 
                                ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                            </asp:CommandField>
                        </Columns>                        
                        <HeaderStyle BackColor="#D4CEA0" ForeColor="Black" Height="30px" 
                            BorderColor="Red"/>
                        <AlternatingRowStyle BackColor ="#E5EEF7"/>
                        <SelectedRowStyle ForeColor ="White" Font-Bold ="True" BackColor ="#008A8C" />            
                        <RowStyle Height="17px" VerticalAlign="Middle" ForeColor ="#2D5580" BackColor="White"/>                        
                        <EmptyDataTemplate>                        
                        <table width="100%" style="background:#D4CEA0; color:Black; font-weight:bold;"> 
                                <tr style="height:30px">
                                    <td style="width:6%"><b>ID</b></td>
                                    <td style="width:10%"><b>Codigo</b></td>
                                    <td style="width:34%"><b>Apellidos y Nombres</b></td>
                                    <td style="width:30%"><b>Carrera Profesional</b></td>
                                    <td style="width:8%"><b>Estado</b></td>
                                    <td style="width:7%"><b>Deuda</b></td>
                                    <td style="width:5%"><b>Ver</b></td>                                    
                                </tr>
                         </table>
                        </EmptyDataTemplate>                        
                        
                    </asp:GridView></td>
            </tr>
        </table>    
        <br />
        <div id="tbDetalle" runat="server">
        
        <table cellspacing="0" cellpadding="0" style="height:500px; border-collapse: collapse" bordercolor="#111111" width="100%">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblMatricula" runat="server" Font-Bold="True" Font-Size="Medium" 
                        ForeColor="#006666"></asp:Label><br />
                </td>
                <td colspan="3" align="right">
                    <asp:Button ID="btnReactivar" runat="server" Text="Reactivar Matricula" CssClass="guardar2" Height="22px" Width="130px" />
                </td>                
            </tr>
			<tr style="height:8%">
					<td class="pestanaresaltada" align="center" width="18%">
                        <asp:LinkButton ID="lnkDatos" runat="server">Datos del Estudiante</asp:LinkButton>
                    </td>					
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			        <td class="pestanabloqueada" align="center" width="18%" onclick="ResaltarPestana2('0','','');">
                        <asp:LinkButton ID="lnkEstadoCuenta" runat="server">Estado de Cuenta</asp:LinkButton></td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			        <td class="pestanabloqueada" align="center" width="18%" onclick="ResaltarPestana2('0','','');">
                        <asp:LinkButton ID="lnkMovimientos" runat="server">Mov. Académicos</asp:LinkButton>  </td>                    
                    <td width="10%" height="10%" class="bordeinf" align="right" onclick="ResaltarPestana2('0','','');">
		                <img border="0" src="../../../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()" />
		                <%if session("codigo_tfu")=1 or session("codigo_tfu")=16 or session("codigo_tfu")=33 or session("codigo_tfu")=7 then%>
                                <img border="0" src="../../../images/editar.gif" style="cursor:hand" ALT="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('frmactualizardatos.asp',this)" />
		                <%end if%>
                                <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')" />
                    </td>
				</tr>
	  			<tr style="height:92%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">					
					<iframe id="fradetalle" runat="server" height="95%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			</tr>
        </table>
        </div>    
    </div>
    <asp:HiddenField ID="HdCicloActual" runat="server" />
    <asp:HiddenField ID="HdCodigoPer" runat="server" />
    </form>
</body>
</html>
