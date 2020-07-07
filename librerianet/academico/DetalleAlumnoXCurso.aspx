<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DetalleAlumnoXCurso.aspx.vb" Inherits="librerianet_academico_DetalleAlumnoXCurso" %>
<%@ Register Src="controles/CtrlFotoAlumno.ascx" TagName="FotoAlumno" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
     <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <script src="../../private/tooltip.js" language="javascript"></script>
    <style type="text/css">
        #Button2
        {
            width: 90px;
        }
    TBODY    { display: table-row-group }
    </style>
</head>
<body>
    <form id="form1" runat="server">
  
    <table style="width:100%" cellpadding="3" cellspacing="0">
        <tr >
            <td align="center" bgcolor="#6699FF" 
                
                style="font-family: Arial, Helvetica, sans-serif; font-size: 16px; font-weight: bold; color: #FFFFFF;">
                &nbsp;Asistencias y Notas por Curso</td>
        </tr>
        <tr >
            <td>
            &nbsp;<input id="CmdImprimir" class="boton" type="button" value="Imprimir" onclick="javascript:print();" />
                <asp:Button ID="cmdExportar" runat="server" CssClass="boton" Text="Exportar" />
            </td>
        </tr>
        <tr >
            <td>
                <asp:Label ID="lblAlumno" runat="server"></asp:Label>
&nbsp;<asp:Label ID="lblCicloAcad" runat="server"></asp:Label>
            </td>
        </tr>
        <tr >
            <td>
                Leyenda:
                <asp:Label ID="Label4" runat="server" BackColor="#FFFF66" BorderStyle="Solid" 
                    BorderWidth="1px" Text="C.A."></asp:Label>
&nbsp;= Actividad que considera asistencia&nbsp;
                <asp:Label ID="Label5" runat="server" BackColor="#FF9900" BorderStyle="Solid" 
                    BorderWidth="1px" Text="C.N."></asp:Label>
&nbsp;= Actividad que considera nota</td>
        </tr>
        <tr >
            <td align="center">
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
            <td align="justify">
                <b>FILTRAR POR:</b></td>
        </tr>
        <tr >
            <td align="justify">
                <asp:Table ID="tblCursos" runat="server" Width="100%" HorizontalAlign="Center">
                    <asp:TableRow ID="rowCursos" runat="server">
                        <asp:TableCell ID="cellCursos" runat="server"></asp:TableCell>
                    </asp:TableRow>  
                    <asp:TableRow ID="rowDetalleNotas" runat="server">
                        <asp:TableCell ID="cellDetalleNotas" runat="server" HorizontalAlign="Justify">
                         <asp:GridView ID="gvConsolidado" runat="server" 
                             AllowSorting="True" AutoGenerateColumns="False" 
                             DataSourceID="SqlDSAsistenciayNotas" GridLines="Horizontal" PageSize="25" 
                             Width="100%" DataKeyNames="alumno,cicloAcademico">
                             <RowStyle Height="22px" />
                                <Columns>
                                    <asp:BoundField DataField="codigo_act" HeaderText="N°" >
                                        <ItemStyle Width="28px" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
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
                                    <asp:BoundField HeaderText="C.A">
                                        <HeaderStyle BackColor="#FFFF66" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="considerarnota_act" HeaderText="C.N">
                                        <HeaderStyle BackColor="#FF9900" />
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
                        </asp:TableCell>
                    </asp:TableRow>  
                </asp:Table>
            </td>
        </tr>
        <tr >
            <td align="center">
               
            </td>
        </tr>
        <tr >
            <td align="justify">

    <asp:SqlDataSource ID="SqlDSAsistenciayNotas" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="MED_ConsultarNotasyAsistenciaConsolidado_alumno" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
        
            <asp:SessionParameter Name="codigo_cac" SessionField="codigo_cac"
                Type="Int32" />
             <asp:SessionParameter Name="codigo_alu" SessionField="codigo_alu"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

            </td>
        </tr>
        </table>
    <div>
    
    <asp:SqlDataSource ID="SqlDSAsistenciayNotas0" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="MED_ConsultarNotasyAsistenciaXCurso" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
        
            <asp:SessionParameter Name="codigo_cac" SessionField="codigo_cac"
                Type="Int32" />
             <asp:SessionParameter Name="codigo_alu" SessionField="codigo_alu"
                Type="Int32" />
            <asp:QueryStringParameter Name="codigo_syl" QueryStringField="syl" 
                Type="Int32" />                
        </SelectParameters>
    </asp:SqlDataSource>

    </div>
    </form>
</body>
</html>
