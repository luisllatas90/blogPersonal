<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdescuentosporplanilla.aspx.vb" Inherits="frmdescuentosporplanilla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="funciones.js"></script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {            height: 737px;
        }
        .style16
        {
            width: 131px;
        }
        .style17
        {
            width: 802px;
        }
        .style18
        {
            color: #993300;
            font-family: Arial;
            font-size: x-small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 26px">
    
        <span class="style18">Sistema de Tesorería&gt; Módulo Consultas Descuentos por 
        Planilla</span><br />
        <br />
        <br />
    
    </div>
    <table style="width: 1203px">
    <tr>
        <td class="style16">
                        Seleccione personal :</td>
        <td class="style17">
            <asp:DropDownList ID="cbocliente" runat="server" Height="22px" 
                    style="font-size: x-small; font-family: Arial" Width="658px">
                </asp:DropDownList>
                <asp:CheckBox ID="chkincluircancelados" runat="server" Font-Names="Arial" 
                    Font-Size="Small" style="font-size: small" Text="Incluir cancelados" />
        </td>
        <td>
                <asp:Button ID="cmdconsultar" runat="server" Text="Consultar" Height="20px" 
                    Width="132px" />
        </td>
        <td>
        </td>
    </tr>
    </table>
    <table class="style1"         
        style="border: 1px solid #000000; width : 98%; border-spacing: 0px; height: 695px;" 
        cellpadding="0" cellspacing="0">
        <tr>
            <td class="style2" style="vertical-align :top" >
                    <div style ="overflow : scroll; height : 731px">
                    <asp:GridView ID="lstinformacioncargos" runat="server" 
                        AutoGenerateColumns="False" Font-Names="Arial" Font-Size="Small" Height="16px" 
                        Width="98%" CellPadding="2" 
                    DataKeyNames="codigo_ddc">
                        <Columns>
                            <asp:BoundField DataField="codigo_ddc" HeaderText="Id" 
                                InsertVisible="False" ReadOnly="True" SortExpression="Id">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"/>
                                <ItemStyle Width="2%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_rub" HeaderText="Rubro" 
                                SortExpression="descripcion_rub">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"/>
                                <ItemStyle Width="25%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre" 
                                SortExpression="descripcion_cac">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small"   ForeColor="Blue" />
                                <ItemStyle Width="2%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tplla" HeaderText="Tipo Planilla">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                    <ItemStyle Width="20%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="año_ddc" HeaderText="Año" >
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                    <ItemStyle Width="2%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_mes" HeaderText="Mes" >
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                    <ItemStyle Width="5%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_cco" HeaderText="Centro Costos" 
                                SortExpression="descripcion_cco">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"/>
                                
                                <ItemStyle Width="36%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_tip" 
                                HeaderText="Moneda" SortExpression="Moneda">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"/>                                
                                <ItemStyle Width="5%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="importe_ddc" HeaderText="Importe" 
                                SortExpression="importe_ddc">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"/>
                                <ItemStyle HorizontalAlign="Right" Font-Names="Arial" Font-Size="X-Small"/>
                                <ItemStyle Width="5%"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="importecancelado_ddc" HeaderText="Imp. cancelado" 
                                SortExpression="importecancelado_ddc">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"/>
                                <ItemStyle HorizontalAlign="Right"  Width ="5%" Font-Names="Arial" Font-Size="X-Small"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="saldo" HeaderText="Saldo">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" 
                                    ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    </div>
                    </td>
        </tr>
        </table>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    </form>
</body>
</html>
