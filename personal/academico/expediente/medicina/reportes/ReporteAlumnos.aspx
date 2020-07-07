<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReporteAlumnos.aspx.vb" Inherits="medicina_reportes_ReporteAlumnos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">

TBODY    { display: table-row-group }

tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif; font-size: 8pt; color: #2F4F4F
	}

        .style1
        {
            width: 108px;
        }
    .usatCeldaMenuSubTitulo {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
	font-weight: bold;
	color: #800000;
}
.usatTablaInfo {background:#FFFFCC; border:1px solid #96965E; padding:5px;margin-top:12px;margin-botton:12px; color:#0000FF}
.salir  	{border:1px solid #C0C0C0; background:#FEFFE1 url('../../images/salir.gif') no-repeat 0% 80%; 
width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25
        }
.Exportar  	{border:1px solid #C0C0C0; background:#FEFFE1 url('../../images/xls.gif') no-repeat 0% 80%; 
width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25
        }

.Selected
{
	CURSOR: hand;
	background : #395ACC;
	color : #FFFFFF;
}

body{ font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: Black;font-size:8pt;
	font: normal;
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
                Reporte de alumnos matriculados</td>
        </tr>
        <tr >
            <td colspan="2">
                &nbsp;<asp:Button ID="CmdRegresar" runat="server" CssClass="salir" 
                    Text="      Regresar" 
                    onclientclick="javascript:history.back(); return false;" Width="78px" />
            &nbsp;<asp:Button ID="CmdExportar" runat="server" CssClass="Exportar" 
                    Text="   Exportar" Width="81px" />
            </td>
        </tr>
        <tr >
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr >
            <td colspan="2">
                <asp:GridView ID="gvAlumnos" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <RowStyle BackColor="#EFF3FB" Height="25px" />
                    <Columns>
                        <asp:BoundField HeaderText="Nro" />
                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Codigo Universitario">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Alumno" HeaderText="Alumno">
                            <ItemStyle Width="500px" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        </table>

    </form>
</body>
</html>
