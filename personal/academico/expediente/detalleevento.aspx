<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleevento.aspx.vb" Inherits="detalleevento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Detalle de Evento</title>
    <link rel="STYLESHEET" href="private/estilo.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>     
        <table style="width: 100%">
            <tr>
                <td colspan="3" rowspan="2" align="center">
        <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="100%" DataSourceID="Eventos" AutoGenerateRows="False" BackColor="White" BorderColor="#CC9966" BorderWidth="1px" CellPadding="4" BorderStyle="None" GridLines="Horizontal">
            <Fields>
                <asp:BoundField DataField="Descripcion_eve" HeaderText="Nombre" >
                    <HeaderStyle Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="Organizado" HeaderText="Organizado por" />
                <asp:BoundField DataField="tipo" HeaderText="Tipo" />
                <asp:BoundField DataField="Inicio" DataFormatString="{0:dd-MM-yyyy}" HeaderText="F. Inicio"
                    HtmlEncode="False" />
                <asp:BoundField DataField="Fin" DataFormatString="{0:dd-MM-yyyy}" HeaderText="F. Fin"
                    HtmlEncode="False" />
            </Fields>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <EditRowStyle BackColor="#FFCC66" ForeColor="#663399" Font-Bold="True" />
            <RowStyle Font-Names="Arial" Font-Size="Small" HorizontalAlign="Left" Width="300px" BackColor="White" ForeColor="#330099" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <FieldHeaderStyle Font-Names="Arial" Font-Size="Small"
                Width="120px" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" Width="80px" ForeColor="#FFFFCC" />
            <EmptyDataTemplate>
                <em style="font-weight: normal; font-size: 9pt; color: maroon; font-style: normal;
                    font-family: verdana">No se encontro ninguna coincidencia del evento seleccionado.
                    Intentelo nuevamente.</em>
            </EmptyDataTemplate>
            <HeaderTemplate>
                Detalle de Evento
            </HeaderTemplate>
        </asp:DetailsView>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td align="right" colspan="3">
                    <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript:window.close();return false;"
                        Text="Salir" Width="63px" />
                    &nbsp;
                </td>
            </tr>
        </table>
    
    </div>
        <asp:ObjectDataSource ID="Eventos" runat="server" SelectMethod="ObtieneDetalleEvento"
            TypeName="Combos">
            <SelectParameters>
                <asp:QueryStringParameter Name="tipo" QueryStringField="tipo" Type="Int32" />
                <asp:QueryStringParameter Name="param1" QueryStringField="id" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
