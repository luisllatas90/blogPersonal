<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rpteevaluacionparticipante.aspx.vb" Inherits="personal_aulavirtual_lebir_eva_rpteevaluacionparticipante" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../../private/estiloaulavirtual.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label1" runat="server" 
        Text="Reporte de evaluación del participante" Font-Bold="True" 
        Font-Size="Medium"></asp:Label>
    <p class="e1">
        <asp:Button ID="cmdRegresar" runat="server" CssClass="atras" 
            Text="    Regresar" />  
        &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="excel" Text="Exportar" />
    &nbsp;</p>
    <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>

    </form>
</body>
</html>
