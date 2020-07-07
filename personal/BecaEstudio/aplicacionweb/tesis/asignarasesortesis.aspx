<%@ Page Language="VB" AutoEventWireup="false" CodeFile="asignarasesortesis.aspx.vb" Inherits="personal_academico_tesis_asignarasesortesis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Asignar Asesores / Jurado a Trabajos de Investigación para 
        Titulación</p>
    <table class="style1">
        <tr>
            <td>
                Escuela:</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Profesor</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Rol</td>
            <td>
                <asp:DropDownList ID="cboCodigo_tpi" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Fecha Reg</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="Fecha Reg." />
            <asp:BoundField HeaderText="Rol" />
            <asp:BoundField HeaderText="Apellidos y Nombres" />
            <asp:BoundField HeaderText="Estado actual" />
        </Columns>
    </asp:GridView>
    
    </form>
</body>
</html>
