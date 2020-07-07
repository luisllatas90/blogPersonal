<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVerHorarios.aspx.vb" Inherits="personal_frmVerHorarios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 6px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        <tr>
            <td>
                <asp:Label ID="lblfechaenviox" runat="server" Text="Fecha de envío: " 
                    Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblfechaenvio" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblregistradoporx" runat="server" Text="Registrado por: " 
                    Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblregistradopor" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3"><hr /></td>
        </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Vigencia del Horario"></asp:Label>
                </td>                
                <td>
                    <asp:DropDownList ID="ddlSemana" 
                        runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                                                        </td>
                <td>                    
                    <asp:Button ID="btnCancelar" runat="server" Text="Regresar" Width="100px" Height="22px" CssClass="salir" 
                    onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
                    
                </td>                                
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Intervalo de fechas"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblFechas" runat="server" Text=""></asp:Label></td>
                <td></td>

            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Total Horas Semanales:"></asp:Label>
                </td>
                <td>
                                                        <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                 <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Total Horas Mensuales:"></asp:Label>
                </td>
                <td>
                                                        <asp:Label ID="lblHorasMensuales" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                <td></td>
            </tr>
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Observación"></asp:Label></td>
                <td colspan=3>
                    <asp:Label ID="lblObservacionHorario" runat="server" ForeColor="Red"></asp:Label>
                </td>
              
            </tr>
            <tr>
                <td valign="top" colspan="2">
                                        <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria" 
                        Width="450px" Font-Size="XX-Small">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="#AEC9FF" />
                                            </asp:GridView>                                        
                </td>
                <td valign="top">
                                                       <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                        CellPadding="1" ForeColor="#333333" Font-Size="XX-Small" Width="200px">
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView> 
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                     <table align="center" bgcolor="White" style="width: 200px;">
                                                            <tr>
                                                            <td class="style4"  style="font-weight: bold">
                                                                    Leyenda:</td>
                                                            </tr>
                                                            <tr>                                                         
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblA" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label> 
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblU" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblD" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                 </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblP" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align = "center" valign="middle">
                                                                    <asp:Label ID="lblCP" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblH" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblT" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblC" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblI" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGR" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblCA" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td >
                                                                <td class="style4" align = "center" valign="middle">
                                                                <asp:Label ID="lblF" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                <asp:Label ID="lblE" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>    
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGA" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGP" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblG" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>
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
