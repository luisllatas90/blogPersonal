<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmregistroingresoporquinta.aspx.vb" Inherits="frmregistroingresoporquinta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style5
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
        }
        .style6
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
        }
        .style9
        {
            font-family: Arial;
            font-size: small;
            color: #FFFFFF;
            background-color: #71B5E2;
            font-weight: bold;
        }
        .style10
        {
            width: 175px;
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
        }
        .style13
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
            width: 75px;
        }
        .style19
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
            width: 159px;
        }
        .style22
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
            width: 47px;
        }
        .style25
        {
            width: 90px;
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
        }
        .style28
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
            width: 387px;
        }
        .style30
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            text-align: center;
        }
        .style31
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
            width: 67px;
        }
        .style32
        {
            font-family: "Courier New", Courier, "espacio sencillo";
            font-size: small;
            height: 37px;
            width: 96px;
        }
        .style33
        {
            color: #FFFFFF;
            font-weight: bold;
            font-family: Arial;
            font-size: small;
        }
    </style>
    <script language="javascript" type ="text/javascript">
        
    </script>
</head>
<body bgcolor="#d2d2d2" style="background-color: #ffffff">
    <form id="form1" runat="server">    
    <table   style="width: 77%; height: 168px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"  >
        <tr>
            <td colspan="8" style="background-color: #993333" class="style33" bgcolor="#993333">
                <span style="font-family: Courier New">
                Datos del Informe</span></td>
        </tr>
        <tr>
            <td colspan="8" >
                <hr />
            </td>
        </tr>
        <tr>
            <td class="style5">
                Programa&nbsp;&nbsp;&nbsp;:&nbsp; </td>
            <td class="style6" colspan="7">
                <asp:DropDownList ID="cboprograma" runat="server" Height="50px" 
                    Width="613px" Font-Names="Courier New">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style5">
                Descripción:</td>
            <td class="style6" colspan="7">
                <asp:TextBox ID="txtdescripcion" runat="server" Width="611px" Height="54px" 
                    MaxLength="200" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style9" colspan="8" style="background-color: #993333">
                <span style="font-family: Courier New">
                Datos de la planilla&nbsp; en la que desea que se procese:</span></td>
        </tr>
        <tr>
            <td class="style10">
                Tipo de Planilla&nbsp;&nbsp;&nbsp;:</td>
            <td class="style19">
                <asp:DropDownList ID="cbotipoplanilla" runat="server" Height="30px" 
                    Width="240px" style="font-size: x-small" 
                    ToolTip="Tipo de Planilla a agregar">
                </asp:DropDownList>
            </td>
            <td class="style22">
                Año:&nbsp;&nbsp; </td>
            <td class="style25">
                <asp:DropDownList ID="cboaño" runat="server" 
                    style="font-size: x-small; margin-left: 0px;" Height="16px" Width="49px">
                </asp:DropDownList>
            </td>
            <td class="style13">
                Mes:&nbsp;&nbsp;&nbsp; </td>
            <td class="style32">
                <asp:DropDownList ID="cbomes" runat="server" style="font-size: x-small">
                </asp:DropDownList>
            </td>
            <td class="style31">
                Moneda:&nbsp; </td>
            <td class="style28">
                <asp:DropDownList ID="cboMoneda" runat="server" style="font-size: x-small">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style30" colspan="8">
                <asp:Button ID="cmdgrabar" runat="server" BackColor="#185597" 
                    BorderColor="#185597" BorderStyle="None" ForeColor="White" Text="Grabar" 
                    Height="22px" onclientclick="return (confirm('Desea registrar'))" 
                    Width="72px" style="background-color: #84BFE7" />
                <asp:Button ID="cmdcancelar" runat="server" BackColor="#185597" 
                    BorderStyle="None" ForeColor="White" Text="Cancelar" 
                    style="background-color: #77B8E2" />
            </td>
                
        </tr>
    </table>
    <asp:Label ID="lblmensaje" runat="server" ForeColor="#CC3300" Text="Mensaje" BackColor="LemonChiffon" Height="16px" Width="1072px"></asp:Label>
    </form>
</body>
</html>
