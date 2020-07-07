<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoInidicadorActividad.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
        .style1
        {
            width: 218px;
        }
        .style4
        {
            width: 444px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
            Text="REGISTRO INDICADORES - ACTIVIDAD"></asp:Label>
    </p>
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    OBJETIVO</td>
                <td class="style4">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    INDICADOR</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox4" runat="server" Width="196px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    FORMULA</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox5" runat="server" Width="285px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Guardar" />
    <asp:Button ID="Button2" runat="server" Text="Cancelar" />
    
    </form>
</body>
</html>
