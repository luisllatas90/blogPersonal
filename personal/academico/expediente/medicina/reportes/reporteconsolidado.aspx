<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteconsolidado.aspx.vb" Inherits="medicina_reportes_reporteconsolidado" %>
<%@ Register Src="~/academico/expediente/medicina/controles/CtrlFotoAlumno.ascx" TagName="FotoAlumno" TagPrefix="uc2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
 
    <table style="width:100%" cellpadding="3" cellspacing="0">
        <tr>
           
            <td class="style2"  >
            </td>
            <td >
                &nbsp;</td>
        </tr>
        <tr >
            <td class="style2" >
                Curso</td>
            <td >
                <asp:Label ID="lblcurso" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
            </td>
        </tr>
        <tr >
            <td class="style2">
                Cronograma</td>
            <td>
                Fecha Inicio:
                <asp:Label ID="lblInicio" runat="server" CssClass="usatCeldaMenuSubTitulo" ></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp; Fecha Fin:
                <asp:Label ID="lblFin" runat="server" CssClass="usatCeldaMenuSubTitulo" ></asp:Label>
            </td>
        </tr>
        <tr >
            <td colspan="2">
                <hr class="usatTablaInfo"  />
            </td>
        </tr>
        <tr >
            <td colspan="2" align="center" bgcolor="#E9E4C7" 
                style="font-family: Arial, Helvetica, sans-serif; font-size: 16px; font-weight: bold">
                Consolidado de Asistencias y Notas por Alumno</td>
        </tr>
        <tr >
            <td colspan="2">
                <asp:Button ID="CmdRegresar" runat="server" CssClass="salir" 
                    Text="      Regresar" 
                    onclientclick="javascript:history.back(); return false;" Width="78px" />
            &nbsp;</td>
        </tr>
        <tr >
            <td colspan="2" align="center">
                <asp:Table ID="TblAsistencia" runat="server" BorderStyle="Solid" 
                    BorderWidth="1px" CellPadding="4" CellSpacing="0" GridLines="Both" 
                    BorderColor="Black">
                </asp:Table>
           
            </td>
        </tr>
        <tr >
            <td colspan="2">
                &nbsp;</td>
        </tr>
        </table>

    </form>
</body>
</html>
