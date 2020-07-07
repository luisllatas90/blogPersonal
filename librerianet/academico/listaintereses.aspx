<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listaintereses.aspx.vb" Inherits="librerianet_academico_listaintereses" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Lista de intereses generados</p>
    <input id="Regresar" type="button" value="Regresar" onclick="history.back(-1)" />
    <br />
    <asp:GridView ID="grwListaIntereses" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" ShowFooter="True">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="codigo" HeaderText="#" />
            <asp:BoundField DataField="saldo" HeaderText="Saldo" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="fecha" HeaderText="Fecha" DataFormatString="{0:d}" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="dias" HeaderText="Días" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="interes" HeaderText="Interes" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="deudaalafecha" HeaderText="Deuda a la fecha" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="pago" HeaderText="Pago" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    
    </form>
</body>
</html>
