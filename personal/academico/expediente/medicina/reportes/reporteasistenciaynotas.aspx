<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteasistenciaynotas.aspx.vb" Inherits="medicina_reportes_reporteasistenciaynotas" %>
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
            &nbsp;<input id="CmdImprimir" class="boton" type="button" value="Imprimir" onclick="javascript:print();" /></td>
        </tr>
        <tr >
            <td colspan="2" align="center">
                <asp:Table ID="TblDatos" runat="server">
                    <asp:TableRow runat="server">
                        <asp:TableCell ID="TblCelFoto" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TblCelResumen" runat="server"> <table border="1" cellpadding="0" cellspacing="0" style="width: 265px; height: 81px">
                                    <tr>
                                        <td align="center" style="background:#FFEECC"colspan="2">
                                            Resumen Asistencias</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;<asp:Label ID="Label1" runat="server" Text="Asistencias"></asp:Label></td>
                                        <td align="center">
                                            <asp:Label ID="LblAsistencia" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;<asp:Label ID="Label2" runat="server" Text="Tardanzas"></asp:Label></td>
                                        <td align="center">
                                            <asp:Label ID="LblTardanzas" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;<asp:Label ID="Label3" runat="server" Text="Inasistencias"></asp:Label></td>
                                        <td align="center">
                                            <asp:Label ID="LblFaltas" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                </table></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </td>
        </tr>
        <tr >
            <td colspan="2" align="center">
                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" 
                    DataSourceID="SqlDSAsistenciayNotas" GridLines="Horizontal" PageSize="25" 
                    Width="95%">
                 <RowStyle Height="22px" />
                    <Columns>
                        <asp:BoundField DataField="codigo_act" HeaderText="N°" >
                            <ItemStyle Width="28px" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaini_act" DataFormatString="{0:dd-MM-yyyy}" 
                            HeaderText="Fecha" HtmlEncode="False" >
                            <ItemStyle HorizontalAlign="Center" Width="85px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaini_act" DataFormatString="{0:HH:mm}" 
                            HeaderText="H.I." HtmlEncode="False" >
                            <ControlStyle BackColor="#3366FF" />
                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechafin_act" DataFormatString="{0:HH:mm}" 
                            HeaderText="H.F." >
                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Act" HeaderText="Actividad" >
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="C.A" />
                        <asp:BoundField DataField="considerarnota_act" HeaderText="C.N">
                            <ItemStyle HorizontalAlign="Center" Width="25px" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoasistencia_act" HeaderText="Asistencia">
                            <ItemStyle HorizontalAlign="Center" Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HoraIngreso_dact" DataFormatString="{0:HH:mm:ss}" 
                            HeaderText="Hora Ingreso" HtmlEncode="False" Visible="False">
                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CalifNum_dact" DataFormatString="{0:00.00}" 
                            HeaderText="Nota" HtmlEncode="False">
                            <ItemStyle HorizontalAlign="Center" Width="40px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="observacion_dact" HeaderText="Observación" />
                    </Columns>
                
                    <HeaderStyle BackColor="Beige" Height="25px" />
                
                </asp:GridView>
            </td>
        </tr>
        </table>

    <asp:SqlDataSource ID="SqlDSAsistenciayNotas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="MED_ConsultarNotasyAsistenciaAlumno" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="codigo_syl" QueryStringField="codigo_syl" 
                Type="Int32" />
            <asp:QueryStringParameter Name="codigo_dma" QueryStringField="codigo_dma" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    </form>
</body>
</html>
