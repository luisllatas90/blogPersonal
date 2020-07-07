<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteasistencia.aspx.vb" Inherits="medicina_reportes_reporteasistencia" %>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 108px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
   
    <table style="width:100%" cellpadding="3" cellspacing="0">
        <tr>
            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </ajaxToolkit:ToolkitScriptManager>
            <td  >
            </td>
        </tr>
        <tr >
            <td class="style1" >
                Curso</td>
            <td >
                <asp:Label ID="lblcurso" runat="server" CssClass="usatCeldaMenuSubTitulo"></asp:Label>
            </td>
        </tr>
        <tr >
            <td class="style1">
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
                Reporte de Asistencias</td>
        </tr>
        <tr >
            <td colspan="2">
                &nbsp;<asp:Button ID="CmdRegresar" runat="server" CssClass="salir" 
                    Text="      Regresar" 
                    onclientclick="javascript:history.back(); return false;" Width="78px" />
            &nbsp;<asp:Button ID="CmdExportar" runat="server" CssClass="Exportar" 
                    Text="   Exportar" Width="81px" />
                <asp:Button ID="CmdLeyenda" runat="server" 
                    Text="Ver Actividades" BackColor="White" BorderStyle="None" 
                    ForeColor="#0000CC" />
            </td>
        </tr>
        <tr >
            <td colspan="2">
                <asp:Table ID="TblAsistencia" runat="server" BorderStyle="None" 
                    BorderWidth="1px" CellPadding="2" CellSpacing="0" GridLines="Both">
                </asp:Table>
                <br />
            </td>
        </tr>
        <tr >
            <td colspan="2">
                &nbsp;</td>
        </tr>
        </table>

    <asp:Panel ID="Panel1" runat="server" BackColor="White">
        <asp:Table ID="TblLeyenda" runat="server" 
    CellPadding="2" CellSpacing="0" GridLines="Both">
            <asp:TableRow runat="server" HorizontalAlign="Center" Font-Bold="True" 
                CssClass="selected">
                <asp:TableCell runat="server" ColumnSpan="3">Actividades</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server" CssClass="selected">
                <asp:TableCell runat="server" HorizontalAlign="Center" 
                            Width="70px" CssClass="selected">Actividad</asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Center" 
                            Width="180px" CssClass="selected">Fecha</asp:TableCell>
                <asp:TableCell runat="server" HorizontalAlign="Center" CssClass="selected">Descripcion</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>

    <ajaxToolkit:PopupControlExtender ID="PopupControlExtender1" runat="server" 
        PopupControlID="Panel1" TargetControlID="CmdLeyenda">
    </ajaxToolkit:PopupControlExtender>

    </form>
</body>
</html>
