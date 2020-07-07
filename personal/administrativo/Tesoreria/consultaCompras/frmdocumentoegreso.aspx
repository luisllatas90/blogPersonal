<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdocumentoegreso.aspx.vb" Inherits="frmdocumentoegreso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <script src="funciones.js">function TABLE1_onclick() {

}

</script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            font-family: "Courier New";
            font-size: small;
        }
        .style3
        {
            font-family: "Courier New";
            font-size: x-small;
            width: 130px;
            height: 21px;
            color: #0000FF;
        }
        .style4
        {
            font-family: "Courier New";
            font-size: x-small;
            color: #0000FF;
        }
        .style5
        {
            color: #0000FF;
            font-family: "Courier New";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" id="TABLE1" ">
            <tr>
                <td colspan="6" style="color: white; background-color:  #993300" class="style1">
                    <span style="background-color: #993300">Documento de Egreso</span></td>
            </tr>
            <tr>
                <td class="style3">
                    Tipo Documento :</td>
                <td style="font-size: 8pt; width: 132px; font-family: Arial; height: 21px">
                    <asp:Label ID="lbltipodocumento" runat="server" Text="Label" Width="416px"></asp:Label></td>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style5">
                    Nº :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblnumerodocumento" runat="server" Text="Label" Width="104px"></asp:Label></td>
                <td style="width: 130px; height: 21px" class="style4">
                    Fecha :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblfecha" runat="server" Text="Label" Width="104px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 130px; height: 21px" class="style4">
                    Cliente :</td>
                <td style="font-size: 8pt; width: 132px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblcliente" runat="server" Text="Label" Width="416px"></asp:Label></td>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style5">
                    Tipo Cliente :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lbltipocliente" runat="server" Text="Label" Width="104px"></asp:Label></td>
                <td style="width: 130px; height: 21px" class="style4">
                    Fecha Reg :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblfechareg" runat="server" Text="Label" Width="104px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 130px; height: 21px" class="style4">
                    Operador :</td>
                <td style="font-size: 8pt; width: 132px; font-family: Arial; height: 21px">
                    <asp:Label ID="lbloperador" runat="server" Text="Label" Width="280px"></asp:Label></td>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style5">
                    Terminal :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblterminal" runat="server" Text="Label" Width="176px"></asp:Label></td>
                <td style="width: 130px; height: 21px" class="style4">
                    Estado :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblestado" runat="server" Text="Label" Width="104px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 132px; border-bottom: 1px solid;">
                </td>
                <td style="width: 132px; border-bottom: 1px solid;">
                </td>
                <td style="width: 132px; border-bottom: 1px solid;">
                </td>
                <td style="width: 132px; border-bottom: 1px solid;">
                </td>
                <td style="width: 132px; border-bottom: 1px solid;">
                </td>
                <td style="width: 132px; border-bottom: 1px solid;">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                <div  style="overflow : auto; height : 361px">
                    <asp:GridView ID="lstinformacioncargos" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="codigo_deg" Font-Names="Arial" Font-Size="8pt" Height="1px" Width="1096px" AllowSorting="True" CellPadding="2">
                        <Columns>
                            <asp:BoundField DataField="codigo_deg" HeaderText="Id" InsertVisible="False"
                                ReadOnly="True" SortExpression="codigo_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small"  Width ="5%"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_rub" HeaderText="Rubro" SortExpression="descripcion_rub">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="25%"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cantidad_deg" HeaderText="Cantidad" SortExpression="cantidad_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="5%" HorizontalAlign="Center"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="importe_deg" HeaderText="Importe" SortExpression="importe_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="5%" HorizontalAlign="Right"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_cco" HeaderText="Centro Costos" SortExpression="descripcion_cco">
                                <ItemStyle Font-Names="Arial" Width ="25%" Font-Size="X-Small"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Importetransferido_deg" HeaderText="Imp. Transf."
                                SortExpression="Importetransferido_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="5%" HorizontalAlign="Right"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="exigirrendicion_deg" HeaderText="Ex. rendicion"
                                SortExpression="exigirrendicion_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="2%"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="montorendido_deg" HeaderText="Imp. Rendido" SortExpression="montorendido_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="5%" HorizontalAlign="Right"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="montodevuelto_deg" HeaderText="Imp. Devuelto" SortExpression="montodevuelto_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="5%" HorizontalAlign="Right"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="saldorendir_deg" HeaderText="saldorendir_deg" SortExpression="saldorendir_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="5%" HorizontalAlign="Right"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observacion_deg" HeaderText="observacion_deg" SortExpression="observacion_deg">
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" Width ="30%"/>
                                <HeaderStyle Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" BackColor="#F0F0F0" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 50px">
                    <asp:Label ID="lblobservacion" runat="server" Height="48px" Text="Label" 
                        Width="848px" Font-Names="Arial" Font-Size="8pt" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td colspan="2" style="height: 50px">
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
        <span style="font-size: 8pt; color: #993333; font-family: Arial; text-decoration: underline">
            Sistema de Tesorería v1.0</span>
    </form>
</body>
</html>
