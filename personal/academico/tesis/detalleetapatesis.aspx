<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleetapatesis.aspx.vb" Inherits="detalleetapatesis" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de asesorías</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
            <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="cmdNuevo" runat="server" CssClass="usatModificar" Height="25px" 
        Text="  Cambiar estado" Width="130px" UseSubmitBehavior="False" />
    &nbsp;<input id="cmdCancelar" type="button" value="Cerrar" 
                onclick="self.parent.tb_remove()" class="salir" />
    <asp:Label ID="lblEstado" runat="server" Font-Bold="True" Font-Size="12pt" 
        ForeColor="Red" Text="La tesis ha finalizado su proceso." Visible="False"></asp:Label>
    <br /><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="codigo_Etes,codigo_Eti" ForeColor="#333333" Width="100%">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField HeaderText="#" >
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="fechaAprobacion_Etes" 
                HeaderText="Fecha Aprobación" >
                <ItemStyle Font-Size="8pt" Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_Eti" HeaderText="Fase" >
                <ItemStyle Font-Size="8pt" Width="25%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Registrado por">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("OpRegistro") %>'></asp:Label>
                    <br />
                    <asp:Label ID="Label2" runat="server" Text='<%# eval("obs_Etes") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" />
                <ItemStyle Font-Size="8pt" Width="60%" />
            </asp:TemplateField>
            <asp:CommandField ShowDeleteButton="True" >
                <ControlStyle Font-Underline="True" ForeColor="Blue" />
                <ItemStyle Width="5%" />
            </asp:CommandField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
