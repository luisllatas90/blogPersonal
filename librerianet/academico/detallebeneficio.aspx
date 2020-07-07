<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detallebeneficio.aspx.vb" Inherits="librerianet_academico_detallebeneficio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Beneficios de Beca por Matrícula</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table class="style1">
        <tr>
            <td width="30%">
                Código Universitario</td>
            <td width="70%">:&nbsp;
                <asp:Label ID="lblCodigo" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">Apellidos y Nombres</td>
            <td width="70%">:&nbsp;
                <asp:Label ID="lblapellidos" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="30%">&nbsp;Escuela Profesional</td>
            <td width="70%">:&nbsp;
                <asp:Label ID="lblescuela" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333">
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="descripcion_cac" HeaderText="Ciclo Académico">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="preciocredito_mat" HeaderText="Precio por Crédito">
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="Descripcion_tbe" HeaderText="Beneficio">
                <ItemStyle Width="20%" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
