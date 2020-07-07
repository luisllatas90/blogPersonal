<%@ Page Language="VB" AutoEventWireup="false" CodeFile="presentacion_intro.aspx.vb" Inherits="secretarias_presentacion_intro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link href="../estilo.css"rel="stylesheet" type="text/css">
<script type="text/javascript" src="../funciones.js"> </script>
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblReunion" runat="server" Font-Bold="True" Font-Size="Large"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblFacultad" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <img alt="" src="../images/LogoOficial.jpg" 
                        style="width: 305px; height: 138px" /></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblTipo" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblFecha" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" class="style2">
                    </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                        Font-Size="Medium" ForeColor="#3333FF">Continuar</asp:LinkButton>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
