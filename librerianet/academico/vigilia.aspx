<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vigilia.aspx.vb" Inherits="personal_academico_estudiante_historial" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>VIGILIA </title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../private/funciones.js"></script>    
    <style type="text/css">
        .D
        {
            color: #FF0000;
        }
        .A
        {
            color: #0000FF;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    
        <asp:Panel ID="Panel1" runat="server" style="text-align: center">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" 
    Font-Size="Large" ForeColor="#3333FF" style="text-align: center" 
    Text="Confirma tu participación en la Vigilia de beatificación de JUAN PABLO II"></asp:Label>
            <br />
            <br />
            <asp:Button ID="cmdParticipar" runat="server" 
    BackColor="#FFFF99" Font-Bold="True" Font-Size="Large" ForeColor="#CC3300" 
    Height="53px" Text="Si participaré" Width="222px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="cmdnoParicipo" runat="server" 
    BackColor="#FFFF99" Font-Bold="True" Font-Size="Large" ForeColor="#CC3300" 
    Height="53px" Text="No participaré" Width="222px" />
            <br />
            <br />
            <asp:Image ID="Image1" runat="server" Height="75%" 
    ImageUrl="~/academico/elige/vigilia.jpg" style="text-align: center" />
        </asp:Panel>
    <br />
    </form>
</body>
</html>
