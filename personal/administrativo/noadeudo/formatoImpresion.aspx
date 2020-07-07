<%@ Page Language="VB" AutoEventWireup="false" CodeFile="formatoImpresion.aspx.vb" Inherits="noadeudo_formatoImpresion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css"rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloctrles.css" rel="stylesheet" type="text/css" />     
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 7px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    Solicitud</td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblSolicitud" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Estudiante</td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblEstudiante" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Escuela</td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblEscuela" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Fecha</td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblFecha" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Revisiones</td>
                <td class="style2">
                    :</td>
                <td>
                    <asp:Label ID="lblRevisiones" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:GridView ID="gvRevisiones" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td class="style2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1" colspan="3">
                    <asp:Button ID="cmdImprimir" runat="server" CssClass="imprimir2" Height="28px" 
                        onclientclick="window.print()" Text="Imprimir" Width="102px" />
&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="cmdCerrar" runat="server" CssClass="imprimir2" Height="28px" 
                        Text="Cancelar" Width="102px" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
