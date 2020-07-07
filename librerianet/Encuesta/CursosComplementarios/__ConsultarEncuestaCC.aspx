<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultarEncuestaCC.aspx.vb" Inherits="Encuesta_CursosComplementarios_ConsultarEncuestaCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%;">
        <tr>
            <td colspan="2">
                <b>Consulta de cursos complementarios</b><br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotal" runat="server" ForeColor="#0033CC"></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="cmdExportar" runat="server" CssClass="ExportarAExcel" 
                    Text="     Exportar" Width="80px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
    <asp:GridView ID="gvCursosComplementarios" runat="server" Width="100%">
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                Text="No se encontraron registros"></asp:Label>
        </EmptyDataTemplate>
        <HeaderStyle CssClass="TituloTabla" Height="20px" />
    </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
