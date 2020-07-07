<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmconsultaplanilla.aspx.vb" Inherits="frmconsultaplanilla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="funciones.js"></script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            font-family: "Courier New";
            font-size: small;
            color: #0000FF;
            font-weight: bold;
        }
        .style2
        {
            font-family: "Courier New";
            font-size: small;
            color: #FFFFFF;
            background-color: #993300;
        }
        .style3
        {
            font-family: "Courier New";
            font-size: small;
            color: #FFFFFF;
        }
        .style5
        {
            width: 140px;
            height: 24px;
            font-size: 8pt;
            font-family: "Courier New";
        }
        .style6
        {
            height: 21px;
            width: 140px;
        }
        .style7
        {
            width: 140px;
        }
        .style9
        {
            width: 54px;
            height: 24px;
            font-size: small;
            font-family: "Courier New";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 18px; width: 1234px; background-color: #993300">
        <span class="style1">S</span><span class="style3">istema de Tesorería V1.0 &gt;&gt; </span><span class="style2">
        Módulode Consultas de Planillas de Remuneraciones</span></div>
        <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; width: 65%; border-bottom: black 1px solid; height: 94%">
            <tr>
                <td class="style6">
                </td>
                <td style="width: 30px; height: 21px">
                </td>
                <td style="width: 46px; height: 21px">
                </td>
                <td style="width: 32px; height: 21px">
                </td>
                <td style="width: 54px; height: 21px">
                </td>
                <td style="height: 21px">
                </td>
            </tr>
            <tr>
                <td class="style5">
                    Tipo Planilla :</td>
                <td style="width: 30px; height: 24px;">
                    <asp:DropDownList ID="cbotipoplanilla" runat="server" Width="240px" 
                        Font-Names="Arial" Font-Size="8pt" style="font-family: 'Courier New'">
                    </asp:DropDownList></td>
                <td style="width: 46px; height: 24px;">
                    <span style="font-size: 8pt; font-family: 'Courier New'">
                    Año :</span></td>
                <td style="width: 32px; height: 24px;">
                    <asp:DropDownList ID="cboaño" runat="server" Width="112px" Font-Names="Arial" 
                        Font-Size="8pt">
                    </asp:DropDownList></td>
                <td class="style9">
                    Mes :</td>
                <td style="height: 24px">
                    <asp:DropDownList ID="cbomes" runat="server" Width="86px" Font-Names="Arial" 
                        Font-Size="8pt" Height="16px">
                    </asp:DropDownList>
                    <asp:Button ID="cmdbuscar" runat="server" BorderStyle="Ridge" Text="Buscar" 
                        Width="56px" style="height: 22px" BackColor="#66CCFF" BorderColor="White" 
                        ForeColor="White" /></td>
            </tr>
            <tr>
                <td class="style7">
                </td>
                <td style="width: 30px">
                </td>
                <td style="width: 46px">
                </td>
                <td style="width: 32px">
                </td>
                <td style="width: 54px">
                </td>
                <td>
                </td>
            </tr>
            <tr height ="80%">
                <td colspan="6" style="height: 154px">
                   <div style="overflow :scroll ; height : 804px; width: 880px;">
                    <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" Height="104px" Width="856px">
                        <Columns>
                            <asp:BoundField DataField="codigo_dplla" HeaderText="Id">
                                <ItemStyle Font-Size="X-Small"  Width="10%"/>
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_per" HeaderText="codigo_per" 
                                Visible="False" />
                            <asp:BoundField HeaderText="Nombres" DataField="nombres">
                                <ItemStyle Font-Size="X-Small" Width="40%"/>
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Moneda" DataField="descripcion_tip">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Size="X-Small" Width="20%"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Importe" DataField="monto">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Size="X-Small" Width="20%"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Importe Cancelado">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Size="X-Small" Width="10%"/>
                            </asp:BoundField>
                            <asp:CommandField />
                            <asp:ImageField DataImageUrlField="codigo_ddp" DataImageUrlFormatString="~/iconos/buscar.gif">
                            </asp:ImageField>
                            <asp:ImageField AlternateText="Visualizar Boleta de Pago" 
                                DataImageUrlField="codigo_per" DataImageUrlFormatString="~/iconos/buscar.gif">
                            </asp:ImageField>
                        </Columns>
                    </asp:GridView>
                    </div> 
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
