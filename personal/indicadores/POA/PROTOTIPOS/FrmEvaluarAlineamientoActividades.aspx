<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEvaluarAlineamientoActividades.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
            Text="EVALUAR ALINEAMIENTO DE ACTIVIDADES"></asp:Label>
    </p>
    
        <table style="width:100%; margin-bottom: 6px;">
            <tr>
                <td class="style1">
                    POA</td>
                <td class="style8">
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    ACTIVIDAD</td>
                <td class="style8">
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    PERSPECTIVA</td>
                <td class="style8">
                    <asp:TextBox ID="TextBox1" runat="server" Width="352px"></asp:TextBox>
                </td>
                <td>
                    CARGADO AL SELECCIONAR ACTIVIDAD - SOLO LECTURA</td>
            </tr>
            <tr>
                <td class="style1">
                    OBJETIVO ESTRATEGICO</td>
                <td class="style8">
                    <asp:TextBox ID="TextBox2" runat="server" Width="352px"></asp:TextBox>
                </td>
                <td>
                    CARGADO AL SELECCIONAR ACTIVIDAD - SOLO LECTURA</td>
            </tr>
            <tr>
                <td class="style9">
                    ALINEADO</td>
                <td class="style10">
                    &nbsp;<asp:RadioButton ID="RadioButton1" runat="server" Text="SI  " />
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="NO" />
                </td>
                <td class="style11">
                    </td>
            </tr>
            <tr>
                <td class="style1">
                    OBSERVACION</td>
                <td class="style8">
                    <asp:TextBox ID="TextBox3" runat="server" Height="94px" Width="309px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    <br />
    <div>
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Guardar" />
    <asp:Button ID="Button2" runat="server" Text="Cancelar" />
    
    </form>
</body>
</html>
