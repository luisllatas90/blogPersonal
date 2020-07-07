<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdocumentoingreso.aspx.vb" Inherits="frmdocumentoingreso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="funciones.js"></script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            font-family: "Courier New";
        }
        .style2
        {
            font-family: "Courier New";
            font-size: 8pt;
            width: 130px;
            height: 21px;
        }
        .style3
        {
            font-family: "Courier New";
            font-style: normal;
            font-size: small;
        }
        .style4
        {
            font-size: x-small;
            font-family: "Courier New";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div>
        <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" id="TABLE1" language="javascript" onclick="return TABLE1_onclick()">
            <tr>
                <td colspan="6" style="color: white; background-color: #993333" class="style3">
                    Documento de Ingreso</td>
            </tr>
            <tr>
                <td class="style2">
                                        Tipo Documento :</td>
                <td style="font-size: 8pt; width: 132px; font-family: Arial; height: 21px">
                    <asp:Label ID="lbltipodocumento" runat="server" Text="Label" Width="416px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td style="width: 130px; height: 21px" class="style4">
                    Nº :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblnumerodocumento" runat="server" Text="Label" Width="104px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style1">
                    Fecha :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblfecha" runat="server" Text="Label" Width="104px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style1">
                    Cliente :</td>
                <td style="font-size: 8pt; width: 132px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblcliente" runat="server" Text="Label" Width="416px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td style="width: 130px; height: 21px" class="style4">
                    Tipo Cliente :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lbltipocliente" runat="server" Text="Label" Width="104px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style1">
                    Fecha Reg :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblfechareg" runat="server" Text="Label" Width="104px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
            </tr>
            <tr>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style1">
                    Operador :</td>
                <td style="font-size: 8pt; width: 132px; font-family: Arial; height: 21px">
                    <asp:Label ID="lbloperador" runat="server" Text="Label" Width="280px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td style="width: 130px; height: 21px" class="style4">
                    Terminal :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblterminal" runat="server" Text="Label" Width="176px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
                <td style="font-size: 8pt; width: 130px; height: 21px" class="style1">
                    Estado :</td>
                <td style="font-size: 8pt; width: 130px; font-family: Arial; height: 21px">
                    <asp:Label ID="lblestado" runat="server" Text="Label" Width="104px" 
                        style="font-family: 'Courier New'"></asp:Label></td>
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
                <div  style="overflow :scroll; height : 500px">
                    <asp:GridView ID="lstinformacioncargos" runat="server" 
                        AutoGenerateColumns="False" Font-Names="Arial" Font-Size="8pt" Height="1px" 
                        Width="1086px" AllowSorting="True" CellPadding="2">
                        <Columns>
                            <asp:BoundField DataField="codigo_din" HeaderText="codigo_din" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_din">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_rub" HeaderText="descripcion_rub" 
                                SortExpression="descripcion_rub">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cantidad_din" HeaderText="cantidad_din" 
                                SortExpression="cantidad_din">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="importe_din" HeaderText="importe_din" 
                                SortExpression="importe_din">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="montotransferido_din" 
                                HeaderText="montotransferido_din" SortExpression="montotransferido_din">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_cac" HeaderText="descripcion_cac" 
                                SortExpression="descripcion_cac">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_din" HeaderText="descripcion_din" 
                                SortExpression="descripcion_din">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 50px">
                    <asp:Label ID="lblobservacion" runat="server" Height="48px" Text="Label" Width="848px" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                <td colspan="2" style="height: 50px">
                    <asp:Button ID="cmdcerrar" runat="server" Text="Cerrar" Width="248px" /></td>
            </tr>
        </table>
    
    </div>
        <span style="font-size: 8pt; color: #993333; font-family: Arial; text-decoration: underline">
            Sistema de Tesorería v1.0</span>
    </form>
</body>
</html>
