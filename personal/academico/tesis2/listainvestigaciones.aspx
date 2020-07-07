<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listainvestigaciones.aspx.vb" Inherits="listainvestigaciones" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buscar investigaciones</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Buscar investigaciones registradas<p/>
    <table width="100%">
        <tr>
            <td style="width:10%">
                Buscar</td>
            <td style="width:45%">
                <asp:TextBox ID="txtParametro" runat="server" CssClass="cajas2"></asp:TextBox>
                    </td>
            <td style="width:15%">
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">Código de Investigación</asp:ListItem>
                    <asp:ListItem Value="1">Estudiante</asp:ListItem>
                </asp:DropDownList>
                    </td>
            <td style="width:30%">
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />
                    </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="nro" HeaderText="#" />
                        <asp:BoundField DataField="fechareg" HeaderText="Fecha Registro" />
                        <asp:BoundField DataField="codigo" HeaderText="Código Inv." />
                        <asp:BoundField DataField="titulo" HeaderText="Titulo de investigación" />
                        <asp:BoundField DataField="autor" HeaderText="Autor" />
                        <asp:BoundField DataField="asesor" HeaderText="Asesor (es)" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="fase" HeaderText="Fase" />
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <br />
                <br />
                <table style="width:100%; border-collapse:collapse" cellpadding="3" cellspacing="0" border="1">
                    <tr style="background-color:#5D7B9D;color:White">
                        <th >#</th>
                        <th >
                            Fecha de registro</th>
                        <th >
                            Código Inv</th>
                        <th >
                            Título de Investigación</th>
                        <th >
                            Autor</th>
                        <th >
                            Asesor</th>
                        <th >
                            Estado</th>
                        <th >
                            Fase</th>
                    </tr>
                    <tr>
                        <td>
                           1</td>
                        <td>
                            <%=Date.Today%></td>
                        <td>
                            <%=Session("codigo")%>&nbsp;</td>
                        <td>
                            <%=Session("titulo")%>&nbsp;</td>
                        <td>
                            <%=Session("autor")%>&nbsp;</td>
                        <td>
                            <%=Session("asesor")%>&nbsp;</td>
                        <td>
                            <%=Session("estado")%>&nbsp;</td>
                        <td>
                            <%=Session("fase")%>&nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="cmdModificar" runat="server" Text="Modificar" />
&nbsp;
                <asp:Button ID="cmdEliminar" runat="server" Text="Eliminar" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
