<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmactualizardptocurso.aspx.vb" Inherits="frmactualizardptocurso" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrar cursos y departamentos académicos</title>
    
    <script type="text/javascript" src="../../private/funciones.js"></script>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css">

</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulopagina">Lista de asignaturas por Departamento Académico</p>
    <table width="100%" class="contornotabla">
        <tr>
            <td style="width:10%">Buscar</td>
            <td style="width:80%">
                <asp:TextBox ID="txtTermino" runat="server" CssClass="cajas2"></asp:TextBox>
            </td>
            <td style="width:10%">
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Width="100%" BorderStyle="Solid" CellPadding="3" GridLines="Horizontal" 
        BorderColor="Silver" BorderWidth="1px">
        <RowStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" />
        <Columns>
            <asp:BoundField DataField="codigo_cur" HeaderText="ID">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="identificador_cur" HeaderText="Código">
                <ItemStyle Width="15%" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_cur" HeaderText="Descripción">
                <ItemStyle Width="40%" />
            </asp:BoundField>
            <asp:BoundField DataField="codigo_dac" HeaderText="Departamento Académico">
                <ItemStyle Width="30%" />
            </asp:BoundField>
        </Columns>
        <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" 
            CssClass="eTabla" />
    </asp:GridView>
    <p>
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
    </p>
    </form>
</body>
</html>
