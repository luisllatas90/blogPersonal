<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegularizarArchivos.aspx.vb" Inherits="logistica_RegularizarArchivos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="UTF-8">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="Buscar" />
    
        <br />
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1">
        </asp:GridView>
    
    </div>
    <asp:Button ID="Button2" runat="server" Text="Subir" />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="log_ArchivosAntiguos" SelectCommandType="StoredProcedure">
        
    </asp:SqlDataSource>
    </form>
</body>
</html>
