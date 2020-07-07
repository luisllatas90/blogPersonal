<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdocumentologistica.aspx.vb" Inherits="frmdocumentologistica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 17px;
            width: 68px;
            font-family: "Courier New";
            font-size: x-small;
        }
        .style2
        {
            width: 68px;
            font-family: "Courier New";
            font-size: x-small;
        }
        .style3
        {
            height: 19px;
            width: 68px;
        }
        .style4
        {
            font-family: Arial;
            font-size: x-small;
            font-weight: bold;
            color: #0000FF;
        }
        .style5
        {
            border-left: 1px none black;
            border-right: 1px none black;
            border-top: 1px none black;
            height: 19px;
                width: 68px;
                font-family: Arial;
                font-size: x-small;
            color: #0000FF;
            font-weight: bold;
            border-bottom-style: none;
        }
        .style6
        {
            font-size: x-small;
        }
        .style7
        {
            color: #FFFFFF;
            font-family: "Courier New";
            font-size: small;
        }
        .style8
        {
            height: 17px;
            width: 349px;
        }
        .style9
        {
            width: 349px;
        }
        .style10
        {
            font-family: Arial;
            font-size: x-small;
            height: 19px;
            width: 349px;
        }
        .style11
        {
            height: 19px;
            width: 349px;
        }
        .style12
        {
            color: #0000FF;
            font-weight: bold;
        }
        .style13
        {
            height: 19px;
            width: 268435440px;
        }
        .style14
        {
            height: 221px;
        }
        .style15
        {
            height: 16px;
        }
        .style16
        {
            height: 17px;
            width: 7px;
        }
        .style17
        {
            width: 7px;
        }
        .style18
        {
            height: 19px;
            width: 7px;
        }
        .style19
        {
            font-size: x-small;
            height: 17px;
            width: 50px;
            font-family: "Courier New";
        }
        .style20
        {
            font-size: x-small;
            width: 50px;
            font-family: "Courier New";
        }
        .style21
        {
            height: 19px;
            width: 50px;
        }
        .style22
        {
            font-size: x-small;
            height: 19px;
            width: 50px;
        }
        .style23
        {
            font-family: "Courier New";
            font-size: x-small;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
   
        <table style="border: 1px solid black; width: 999px; height: 446px;">
            <tr>
                <td colspan="8" style="background-color : #993300;" class="style7" >
                    Documento de Compra</td>
            </tr>
            <tr>
                <td class="style1">
                    Documento :</td>
                <td style="font-size: 12px; font-family: Arial; " class="style8">
                    <asp:Label ID="lbldocumento" runat="server" Text="Label" Width="332px" 
                        style="font-size: x-small"></asp:Label></td>
                <td style="width: 58px; height: 17px;" class="style23">
                    <strong>
                    Número :</strong></td>
                <td style="font-size: 12px; font-family: Arial; " class="style16">
                    <asp:Label ID="lblnumero" runat="server" Text="Label" Width="176px" 
                        style="font-size: x-small"></asp:Label></td>
                <td class="style19">
                    <strong>Fecha :</strong></td>
                <td colspan="3" style="font-size: 12px; font-family: Arial; height: 17px;">
                    <asp:Label ID="lblfecha" runat="server" Text="Label" Width="127px" 
                        style="font-size: x-small"></asp:Label></td>
            </tr>
            <tr>
                <td class="style2">
                    Proveedor :</td>
                <td style="font-size: 12px; font-family: Arial;" class="style9">
                    <asp:Label ID="lblproveedor" runat="server" Text="Label" Width="318px" 
                        style="font-size: x-small"></asp:Label></td>
                <td style="width: 58px; " class="style23">
                    <strong class="style6">
                    Tipo :</strong></td>
                <td style="font-size: 12px; font-family: Arial;" class="style17">
                    <asp:Label ID="lbltipo" runat="server" Text="Label" Width="136px" 
                        style="font-size: x-small"></asp:Label></td>
                <td class="style20">
                    Equipo <strong>
                                        :</strong></td>
                <td colspan="3" style="font-size: 12px; font-family: Arial">
                    <asp:Label ID="lblequipo" runat="server" Text="Label" Width="116px" 
                        style="font-size: x-small"></asp:Label></td>
            </tr>
            <tr>
                <td class="style2">
                    Fecha reg :</td>
                <td colspan="7" style="font-size: 12px; font-family: Arial">
                    <asp:Label ID="lblfechareg" runat="server" Text="Label" Width="320px" 
                        style="font-size: x-small"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="8" style="font-size: 12px; font-family: Arial">
                </td>
            </tr>
            <tr>
            
                <td colspan="8" class="style14"  style="width :98%" >
                <div style="overflow:scroll; width : 98%; height: 233px;">
                    <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False"
                        Width="97%" Height="16px">
                        <Columns>
                            <asp:BoundField DataField="descripcion_Art" HeaderText="descripcion_Art" ReadOnly="True"
                                SortExpression="descripcion_Art">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcion_Uni" HeaderText="descripcion_Uni" SortExpression="descripcion_Uni">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Precio_Dco" HeaderText="Precio_Dco" SortExpression="Precio_Dco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cantidad_Dco" HeaderText="cantidad_Dco" SortExpression="cantidad_Dco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="subtotal_Dco" HeaderText="subtotal_Dco" SortExpression="subtotal_Dco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="descuento_Dco" HeaderText="descuento_Dco" SortExpression="descuento_Dco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:BoundField DataField="recargo_Dco" HeaderText="recargo_Dco" SortExpression="recargo_Dco">
                            <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue" />
                                <ItemStyle Font-Names="Arial" Font-Size="X-Small" />
                            </asp:BoundField>
                            <asp:CheckBoxField DataField="exoneradoIgv_Dco" HeaderText="exoneradoIgv_Dco" SortExpression="exoneradoIgv_Dco">
                                <HeaderStyle BackColor="#F0F0F0" Font-Names="Arial" Font-Size="X-Small" ForeColor="Blue"  Width=100px/>
                            </asp:CheckBoxField>                            
                        </Columns>
                    </asp:GridView>
                    </div>                    </td>
            </tr>
            <tr>
                <td colspan="8" class="style15">
                    <asp:Label ID="lblobservacion" runat="server" Height="26px" Text="Label" 
                        Width="998px" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
            </tr>
            <tr>
                <td 
                    class="style5">
                    Desc :</td>
                <td style="border: 1px none black; " class="style10">
                    <asp:Label ID="lbldescuento" runat="server" Text="Label" Width="136px"></asp:Label>
                    </td>
                <td style="border: 1px none black; width: 50px; height: 19px; " class="style4">
                    Moneda :
                    </td>
                <td style="border: 1px none black; font-size: 10pt; font-family: Arial; " 
                    class="style18">
                    <asp:Label ID="lblmoneda" runat="server" Text="Label" Width="136px"></asp:Label></td>
                <td style="border: 1px none black; font-size: 10pt; font-family: Arial; " 
                    class="style21">
                    Igv :</td>
                <td colspan="3" style="border-right: black 1px solid; border-top: black 1px solid;
                    font-size: 10pt; border-left: black 1px solid; border-bottom: black 1px solid;
                    font-family: Arial; height: 19px; border-style: none;">
                    <asp:Label ID="Label7" runat="server" Text="Label" Width="136px"></asp:Label></td>
            </tr>
            <tr>
                <td style="border: 1px none black; font-size: 10pt; font-family: Arial; " 
                    class="style3">
                </td>
                <td style="border: 1px none black; font-size: 10pt; font-family: Arial; " 
                    class="style11">
                </td>
                <td colspan="2" 
                    style="border: 1px none black; font-family: Arial; height: 19px; " 
                    class="style6"><b>T</b><span class="style12">otal de la comra</span></td>
                <td style="border: 1px none black; font-family: Arial; " 
                    class="style22">
                    <asp:Label ID="lbltotalcompra" runat="server" Text="Label" Width="107px" 
                        
                        style="color: #0000FF; text-align: left; font-weight: 700; font-size: small"></asp:Label></td>
                <td colspan="2" 
                    style="border: 1px none black; font-size: x-small; font-family: Arial; height: 19px; width: 50px; ">
                    T<span class="style12">otal Físico :</span></td>
                <td style="border: 1px none black; font-size: 10pt; font-family: Arial; " 
                    class="style13">
                    <asp:Label ID="lbltotalfisico" runat="server" Text="Label" Width="65px" 
                        style="color: #0000FF; text-align: right; font-weight: 700"></asp:Label></td>
            </tr>
        </table>
    
    
    </form>
</body>
</html>
