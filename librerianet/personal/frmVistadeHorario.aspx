<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVistadeHorario.aspx.vb" Inherits="librerianet_personal_frmVistadeHorario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <script src="../../private/funciones.js" type="text/javascript"></script>
  
    <title>Página sin título</title>
    <style type="text/css">

        .style4
        {
            width: 123px;
            height: 22px;
        }
        </style>
</head>
<body>
    <form id="form2" runat="server" style="font-family: Verdana; font-size: 11px">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td class="style7" valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td valign="top" style="font-size: small" align="left">
                                            Vigencia del Horario:<asp:DropDownList ID="cboSemana" runat="server" 
                                                AutoPostBack="True" Height="20px" Width="100px">
                                                <asp:ListItem Value="0">Semestre</asp:ListItem>
                                                <asp:ListItem Value="1">Semana 1</asp:ListItem>
                                                <asp:ListItem Value="2">Semana 2</asp:ListItem>
                                                <asp:ListItem Value="3">Semana 3</asp:ListItem>
                                                <asp:ListItem Value="4">Semana 4</asp:ListItem>
                                                <asp:ListItem Value="5">Semana 5</asp:ListItem>
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="lblFechas" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: small" align="left">
                                Total Horas Semanales:                                 <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" style="font-size: small">
                                            <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                CellPadding="1" ForeColor="#333333" Font-Size="XX-Small">
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
                                    </table>
                            </td>
                            <td valign="top">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="CmdReporteHorasTesis" runat="server" CausesValidation="False" 
                                              Text="Hrs. Asesoria Tesis" UseSubmitBehavior="False" />
                                            <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="#AEC9FF" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvListaCambios" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Histórico de envíos de horarios">
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
                                        <td>
                    <hr />
                                        </td>
                                    </tr>
                                    <tr>
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
                                                                    <asp:Label ID="lblG" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
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
                                                                    <asp:Label ID="lblF" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblI" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblE" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblC" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGR" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGA" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGP" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblCP" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                            </tr>
                                                        </table>
                                        </td>
                                    </tr>
                                    </table>
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
