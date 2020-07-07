<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaGeneral.aspx.vb" Inherits="frmConsultaGeneral" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server" style="font-family: verdana; font-size: 11px;">
    <div style="font-size: medium; font-weight: bold; color: #003399; text-decoration: blink;">
    
        Consulta General de Horarios 2010-I<br />
        <br />
    
    </div>
    <asp:GridView ID="gvConsultaGeneral" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
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
