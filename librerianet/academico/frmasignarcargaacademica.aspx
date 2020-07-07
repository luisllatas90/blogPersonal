<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmasignarcargaacademica.aspx.vb" Inherits="academico_frmasignarcargaacademica" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignación de Carga Académica por Departamento Académico</title>
        <link href="../../private/estilo.css" rel="stylesheet" type="text/css">

</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="0" style="width:100%; height:100%">
        <tr>
            <td colspan="2" height="5%" width="100%">
            Asignación de Carga Académica por Departamento Académico
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">Ciclo Académico:</td>
            <td height="5%" width="80%">
                <asp:DropDownList ID="dpCiclo" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">Dpto. Académico</td>
            <td height="5%" width="80%">
                <asp:DropDownList ID="dpDpto" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="100%" colspan="2">
                <asp:DropDownList ID="dpCurso" runat="server" CssClass="Cajas2">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="100%" colspan="2" align="right">
                <asp:Button ID="cmdBuscar" runat="server" Text="Consultar" 
                    UseSubmitBehavior="False" 
                    onclientclick="fradetalle.location.href='frmasignarprofesor.aspx?codigo_cur=' + document.all.dpCurso.value + '&codigo_cac=' + document.all.dpCiclo.value;return(false)" />
            </td>
        </tr>
        <tr>
            <td height="5%" width="100%" colspan="2">
            <iframe id="fradetalle" style="height:350px; width:100%" border="0" frameborder="0" 
                    name="fradetalle" class="contornotabla">
					</iframe>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">
                &nbsp;</td>
            <td height="5%" width="80%" align="right">
                &nbsp;</td>
        </tr>
        </table>
    </form>
</body>
</html>
