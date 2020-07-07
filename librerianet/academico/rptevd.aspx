<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptevd.aspx.vb" Inherits="rptevd" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rendimiento por Veces Desaprobadas</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblMenu" runat="server" CssClass="usatTitulo"></asp:Label>
    
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td colspan="2" style="width:100%">
                &nbsp;&nbsp;&nbsp; Escuela Profesional: <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;Ciclo 
                <asp:DropDownList ID="dpciclo" runat="server">
                </asp:DropDownList>
&nbsp;<asp:DropDownList ID="dpTipo" runat="server">
                    <asp:ListItem Value="1">Desaprobaron &gt;2 veces una asignatura</asp:ListItem>
                    <asp:ListItem Value="2">Desaprobaron &gt;2 asignaturas en ciclos anteriores</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="cmdBuscar" runat="server" Font-Bold="False" Font-Overline="False" 
        Font-Underline="False" 
        Text="Buscar" CssClass="buscar2" />
        <asp:Button ID="cmdExportar" runat="server" Text="Exportar" CssClass="excel2" 
                    Visible="False" />
            </td>
            
        </tr>
        <tr bgcolor="#C8D9EE" style="height:20px">
            <td style="width:100%" class="azul">
                &nbsp;</td>
            
        </tr>
        <tr>
            <td style="margin-left: 80px" valign="top" colspan="2">
    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Código" 
        CellPadding="2" GridLines="Horizontal" BorderStyle="None" 
                    Width="100%" 
                    >
        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        <EmptyDataRowStyle CssClass="sugerencia" />
        <EmptyDataTemplate>
            No se encontrarios estudiantes según los criterios seleccionados
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    
            </td>
        </tr>
        </table>
    
    </form>
</body>
</html>
