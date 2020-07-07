<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmReglamento.aspx.vb" Inherits="reglamentos_FrmReglamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 140px;
        }
        .style2
        {
            width: 140px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
            ForeColor="Red"></asp:Label>
    
        <table style="width:100%;">
            <tr>
                <td class="style2">
                    Nombre:</td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Elaborado por:</td>
                <td>
                    <asp:TextBox ID="txtElaborado" runat="server" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Fecha Publicación:</td>
                <td>
                    <asp:Calendar ID="calPublicacion" runat="server"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Enlace:</td>
                <td>
                    <asp:FileUpload ID="flpArchivo" runat="server" Width="300px"/>                                        
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Mostrar:</td>
                <td>
                    <asp:CheckBox ID="chkMostrar" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    Prioridad:</td>
                <td>
                    <asp:CheckBox ID="chkPrioridad" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
