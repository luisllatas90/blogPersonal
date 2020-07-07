<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datosreunion.aspx.vb" Inherits="secretarias_datosreunion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../funciones.js"> </script>
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="dgvDetalle" runat="server" DataSourceID="SqlDataSource1" 
        AutoGenerateColumns="False" CellPadding="4" 
        GridLines="None" ForeColor="#333333" Width="769px">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="codigo_prp" HeaderText="Cod" 
                SortExpression="codigo_prp" >
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_Prp" HeaderText="Propuesta" 
                SortExpression="nombre_Prp" >
                <ItemStyle Width="90%" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="PRP_ConsultarSesionesConsejos" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="AG" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="codigo_per" 
                QueryStringField="id_rec" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
