<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVistaTestdeHorario.aspx.vb" Inherits="personal_frmVistaTestdeHorario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Horario</title>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <style type="text/css">

        .style7
        {
        }
 
        .style1
        {
        }
        .style4
        {
            width: 123px;
            height: 22px;
        }
        .style8
        {
            height: 20px;
        }
        .style22
        {
            width: 50%;
        }
        .style45
        {
            width: 590px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: Verdana; font-size: 11px">
    <div>
        <asp:Panel ID="pnlCabecera" runat="server">
            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
            <tr>
                <td bgcolor="#D1DDEF" height="30px">
                <b>
                    <asp:Label ID="Label1" runat="server" Text="REGISTRO HORARIO PERSONAL" ></asp:Label></b></td>
            </tr>
            <tr>
                <td>
                        <asp:Panel ID="pnlMensaje" Visible="false" runat="server">
                            <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                                <tr>
                                    <td bgcolor="#FEFFC2" height="30px">
                                    <b>
                                        <asp:Label ID="Label4" runat="server" Text="No se encontro ningún horario registrado para el ciclo académico seleccionado." 
                                        ForeColor="#FF3300"></asp:Label></b></td>
                                </tr>
                            </table>
                        </asp:Panel>
                </td>
            </tr>
                
           </table>
            <asp:Panel ID="pnlHorario" runat="server">
            <table width="100%">
                <tr>
        <td rowspan="3" valign="top">
            <table>
                <tr>                    
                    <td align="left" class="style8" valign="top">
                        Vigencia del Horario:<asp:DropDownList ID="ddlSemana" runat="server" 
                        AutoPostBack="True" Height="20px" Width="100px">
                         <asp:ListItem Value="0">Semestre</asp:ListItem>                                                            
                          </asp:DropDownList>
                           <br />
                            <asp:Label ID="lblFechas" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                </tr>
                <tr>
                    
                    <td align="left" style="font-size: small" valign="top">
                                                        Total Horas Semanales:
                                                        <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                </tr>
                <tr>
                <td align="left" style="font-size: small" valign="top">
                                                        Total Horas Mensuales:
                                                        <asp:Label ID="lblHorasMensuales" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>                    
                </tr>
                <tr>
                    <td>
                                                       <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                        CellPadding="1" ForeColor="#333333" Font-Size="Smaller" 
                                                           HorizontalAlign="Center">
                                                        <RowStyle BackColor="#EFF3FB" HorizontalAlign=center/>
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
                                                                    <asp:Label ID="lblGA" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblG" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                <asp:Label ID="lblF" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>
                                                                        
                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGP" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblE" runat="server" Text="Label" Height="35px" Width="100px" Visible="False"></asp:Label>                                                                        
                                                                </td>
                                                            </tr>
                                                                                                                            
                                                        </table>
                    </td>
                </tr>
                
            </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria" Width="522px" 
                                                AutoGenerateColumns="False" Font-Size="Smaller">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="Cod" HeaderText="Cod" />
                                                    <asp:BoundField DataField="DIA" HeaderText="DIA" />
                                                    <asp:BoundField DataField="HInicio" HeaderText="HInicio" />
                                                    <asp:BoundField DataField="HFin" HeaderText="HFin" />
                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                                                    <asp:BoundField DataField="CCostos" HeaderText="CCostos" />
                                                </Columns>
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
                        <td align="top"><hr /></td>
                    </tr>
                    <tr>
                        <td valign="top"> 
                                                        <asp:GridView ID="gvListaCambios" runat="server" CellPadding="4" 
                                                            ForeColor="#333333" GridLines="None" 
                                                            Caption="Histórico de envíos de horarios" AutoGenerateColumns="False" 
                                                            Font-Size="Smaller">
                                                            <RowStyle BackColor="#EFF3FB" />
                                                            <Columns>
                                                                <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                                                <asp:BoundField DataField="MesVigente" HeaderText="MesVigente" />
                                                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                                                <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                                                <asp:BoundField DataField="RegistradorPor" HeaderText="RegistradoPor" />
                                                                <asp:CommandField ShowSelectButton="True" />
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
            </td>            
        </tr>        
            </table>
        </asp:Panel>
        </asp:Panel>
        
    </div>
    </form>
</body>
</html>
