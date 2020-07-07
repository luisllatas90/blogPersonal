<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalletareausuario.aspx.vb" Inherits="aulavirtual_detalletareausuario" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle de trabajo enviado</title>
    <link rel="STYLESHEET"  href="../../private/estilo.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulo">Detalle / Comentarios de trabajo enviado<p />
    
    <asp:DataList ID="DataList1" runat="server" CellPadding="4" ForeColor="#333333" 
        RepeatColumns="1" ShowHeader="False" Width="100%">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <AlternatingItemTemplate>
            <table style="width:100%;">
                <tr>
                    <td style=" width:20%">
                        Fecha de Registro:</td>
                    <td style=" width:80%">
                        <asp:Label ID="Label1" runat="server" Text='<%# eval("fechareg") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style=" width:20%">
                        Registrado por:</td>
                    <td style=" width:80%">
                        <asp:Label ID="Label2" runat="server" Text='<%# eval("nombreusuario") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style=" width:20%">
                        Estado</td>
                    <td style=" width:80%">
                        <asp:Label ID="Label4" runat="server" 
                            Text='<%# eval("estadorevisionalumno") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style=" width:100%">
                        Comentario</td>
                </tr>
                <tr>
                    <td colspan="2" style=" width:100%">
                        <asp:Label ID="Label3" runat="server" Text='<%# eval("obs") %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </AlternatingItemTemplate>
        <AlternatingItemStyle BackColor="White" BorderColor="#999999" BorderStyle="Solid" 
            BorderWidth="1px" />
        <ItemStyle BackColor="#E3EAEB" BorderColor="#999999" BorderStyle="Solid" 
            BorderWidth="1px" />
        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>
            <table style="width:100%;">
                <tr>
                    <td style=" width:20%">
                        Fecha de Registro:</td>
                    <td style=" width:80%">
                        <asp:Label ID="Label1" runat="server" Text='<%# eval("fechareg") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style=" width:20%">
                        Registrado por:</td>
                    <td style=" width:80%">
                        <asp:Label ID="Label2" runat="server" Text='<%# eval("nombreusuario") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style=" width:20%">
                        Estado</td>
                    <td style=" width:80%">
                        <asp:Label ID="Label4" runat="server" 
                            Text='<%# eval("estadorevisionalumno") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style=" width:100%">
                        Comentario</td>
                </tr>
                <tr>
                    <td colspan="2" style=" width:100%">
                        <asp:Label ID="Label3" runat="server" Text='<%# eval("obs") %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    </form>
</body>
</html>
