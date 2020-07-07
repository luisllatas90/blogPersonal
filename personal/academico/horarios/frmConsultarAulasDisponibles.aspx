<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultarAulasDisponibles.aspx.vb" Inherits="academico_horarios_frmConsultarAulasDisponibles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        { 
            color: #2F4F4F;            
            Background-color:#F0F0F0; font-family:Trebuchet MS; font-size:12px;
            
        }
        .style1
        {
          font-weight:bold; font-size:13px;
        }
        .izq
        { border-left:2px solid;
            }
              .der
        { border-right:2px solid;
            }
            .baj
            { border-bottom:2px solid;
                
                }     
                
                .arr
            { border-top:2px solid;
                
                }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table width="70%" cellpadding="0" cellspacing="0">
            <tr>
                <td class="style1" colspan="4">
                                        Consultar Ambientes Disponibles&nbsp;</td>
            </tr>
            <tr><td><br /></td></tr>
            
            <tr>
                <td class="izq arr">
                    Semestre Académico</td>
                <td class="arr">
                    <asp:DropDownList ID="comboCiclo" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="arr">
                    Tipo Ambiente</td>
                <td class="arr der">
                    <asp:DropDownList ID="comboAmbiente" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="izq">
                    Ubicacion</td>
                <td>
                    <asp:DropDownList ID="comboUbicacion" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    Capacidad &gt;=</td>
                <td class="der">
                    <asp:TextBox ID="txtCapacidad" runat="server" Text="0" Width="20%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="izq baj">
                    Día</td>
                <td  class="baj">
                    <asp:DropDownList ID="comboDia" runat="server">
                    </asp:DropDownList>
                </td>
                <td colspan="2" class="baj der">
                    Desde
                    <asp:DropDownList ID="comboDesde" runat="server">
                    </asp:DropDownList>
                    :00h&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Hasta&nbsp;<asp:DropDownList ID="comboHasta" runat="server">
                    </asp:DropDownList>
                    
                    :00h</td>
            </tr>
            <tr>
                <td><br />
                  <asp:Button ID="Button1" runat="server" Text="Buscar" /></td>
                <td>
                    <br /></td>
                <td colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td colspan="4"> 
                    <asp:Label ID="lblmensaje" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:GridView ID="gridAulas" runat="server" BackColor="White" 
                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
                        CellPadding="4" AutoGenerateColumns="False">
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                            <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" />
                            <asp:BoundField DataField="Abreviatura" HeaderText="Abreviatura" 
                                Visible="False" />
                            <asp:BoundField DataField="Ubicación" HeaderText="Ubicación" />
                            <asp:BoundField DataField="Capacidad" HeaderText="Capacidad" />
                            <asp:BoundField HeaderText="Horario" />
                            <asp:BoundField DataField="nroCruces" HeaderText="Nro Cruces" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
