<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAlineamientoActividades.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

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
            Text="ALINEAMIENTO DE ACTIVIDADES"></asp:Label>
    </p>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    DATOS POA</td>
                <td class="style8">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
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
                    ACTIVIDAD POA</td>
                <td class="style8">
                    <asp:DropDownList ID="DropDownList6" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
    
    <br />
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    DATOS PEI</td>
                <td class="style8">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style5">
                    PLAN (SOLO LECTURA)</td>
                <td class="style6">
                    <asp:DropDownList ID="DropDownList4" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td class="style7">
                    CARGA AL SELECCIONAR POA </td>
            </tr>
            <tr>
                <td class="style1">
                    PERSPECTIVA </td>
                <td class="style8">
                    <asp:DropDownList ID="DropDownList5" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    CARGA AL SELECCIONAR POA</td>
            </tr>
            <tr>
                <td class="style1">
                    OBJETIVOS ESTRATEGICO</td>
                <td class="style8">
                    <asp:DropDownList ID="DropDownList2" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    CARGA AL SELECCIONAR PERSPECTIVA</td>
            </tr>
            <tr>
                <td class="style1">
                    INDICADOR ESTRATEGICO</td>
                <td class="style8">
                    <asp:DropDownList ID="DropDownList3" runat="server" Width="353px">
                    </asp:DropDownList>
                </td>
                <td>
                    CARGA AL SELECCIONAR OBJETIVO</td>
            </tr>
            </table>
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Guardar" />
    <asp:Button ID="Button2" runat="server" Text="Cancelar" />
    
    </form>
</body>
</html>
