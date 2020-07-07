<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoDetalleActividadesPOA.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

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
        .style8
        {
            width: 218px;
            height: 19px;
        }
        .style9
        {
            width: 444px;
            height: 19px;
        }
        .style10
        {
            height: 19px;
        }
        .style11
        {
            width: 218px;
            height: 23px;
        }
        .style12
        {
            width: 444px;
            height: 23px;
        }
        .style13
        {
            height: 23px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
            Text="REGISTRO DETALLE DE ACTIVIDAD"></asp:Label>
        <br />
    
        <table style="width:100%;">
            <tr>
                <td class="style11">
                    ACTIVIDAD</td>
                <td class="style12">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td class="style13">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    DETALLE ACTIVIDAD</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox4" runat="server" Width="334px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    META (%)</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox2" runat="server" Width="81px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    AVANCE</td>
                <td class="style9">
                    <asp:TextBox ID="TextBox3" runat="server" Width="75px"></asp:TextBox>
                </td>
                <td class="style10">
                    </td>
            </tr>
            <tr>
                <td class="style8">
                    FECHA INICIO</td>
                <td class="style9">
                    <asp:TextBox ID="TextBox5" runat="server" Width="101px" Text='dd/mm/aaaa'>dd/mm/aaaa</asp:TextBox>
                </td>
                <td class="style10">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style8">
                    FECHA FIN</td>
                <td class="style9">
                    <asp:TextBox ID="TextBox6" runat="server" Width="101px" Text='dd/mm/aaaa'>dd/mm/aaaa</asp:TextBox>
                </td>
                <td class="style10">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    RESPONSABLE</td>
                <td class="style4">
                    <asp:DropDownList ID="DropDownList4" runat="server" Width="354px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    PRESUPUESTO</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox7" runat="server" Width="81px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    FECHA DE CONTROL (HITO)</td>
                <td class="style4">
                    <asp:TextBox ID="TextBox8" runat="server" Width="101px" Text='dd/mm/aaaa'>dd/mm/aaaa</asp:TextBox>
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
