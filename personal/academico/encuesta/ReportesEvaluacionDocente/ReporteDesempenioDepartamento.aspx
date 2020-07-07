<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteDesempenioDepartamento.aspx.vb" Inherits="Encuesta_ReportesEvaluacionDocente_ReporteDesempenioDepartamento" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">
    .ExportarAWord{border:1px solid #C0C0C0; background:#FEFFE1 url('../../images/exportaraword.gif') no-repeat 0% 80%; width:70; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>
                    <b>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    RESULTADOS DE ENCUESTA- EVALUACION DOCENTE</b>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Departamento Académico:                     <asp:DropDownList ID="cboDepartamento" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Carrera Profesional&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :
                    <asp:DropDownList ID="cboEscuela" runat="server">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="left">
                    Semestre Académico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    :
                    <asp:DropDownList ID="cboCicloAcad" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    Número de Evaluación&nbsp;&nbsp;&nbsp;&nbsp; :                     <asp:DropDownList ID="cboNroEvaluacion" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="cmdBuscar" runat="server" CssClass="Buscar" Height="20px" 
                        Text="    Buscar" Width="80px" />
                    &nbsp;<asp:Button ID="CmdExportar" runat="server" Text="   Exportar" 
                    CssClass="ExportarAExcel" Width="80px" BackColor="#FEFFE1" Height="20px" />
                </td>
            </tr>
            <tr>
                <td>
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: 700">
                    <b>Cantidad de Encuestas Registradas: 
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                <asp:Table ID="TblEncuestas" runat="server" BorderColor="Black" 
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="0" CellSpacing="0" 
                    GridLines="Horizontal" Width="100%" BackColor="White">
                </asp:Table>
                </td>
            </tr>
        </table>
    
    </div>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
    </form>
</body>
</html>