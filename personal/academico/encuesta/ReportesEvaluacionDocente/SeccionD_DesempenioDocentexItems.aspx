<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SeccionD_DesempenioDocentexItems.aspx.vb" Inherits="Encuesta_ReportesEvaluacionDocente_SeccionD_DesempenioDocentexItems" %>
<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;" cellpadding="3" cellspacing="0">
            <tr>
                <td align="justify" class="titulocel" colspan="2">
                                Reporte de evaluación de desempeño docente </td>
            </tr>
            <tr>
                <td align="justify" colspan="2">
                                Departamento Académico:
                                <asp:DropDownList ID="cboDepartamentoAcad" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
&nbsp;</td>
            </tr>
            <tr>
                <td align="justify" colspan="2">
                                Docente&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                :
                                <asp:DropDownList ID="cboPersona" runat="server">
                                </asp:DropDownList>
&nbsp;</td>
            </tr>
            <tr>
                <td align="justify" colspan="2">
                                Semestre Académico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                :&nbsp;
                                <asp:DropDownList ID="cboCicloAcad" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
&nbsp;Nro de Evaluación
                                <asp:DropDownList ID="cboNroEvaluacion" runat="server">
                                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdConsultar" runat="server" BackColor="#FFFFE1" BorderColor="Gray" BorderStyle="Solid" 
                                    BorderWidth="1px" ForeColor="#006666" Text="Consultar" />
                </td>
            </tr>
            <tr>
                <td align="justify" colspan="2" style="font-size: xx-small; color: #808080;">
                                <span style="border:solid; border-width:1px; background-color: #FFFFE1">
                                <img alt="" src="../../../../images/help.gif" style="width: 18px" /> Para visualizar 
                                el puntaje obtenido en cada asignatura dar clic en cada uno de los elementos de 
                                la lista siguiente</span></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                                <asp:GridView ID="gvDesempenio" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_cup" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="Escuela" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_Cur" HeaderText="Asignaturas" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="modalidad" HeaderText="Modalidad" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo &lt;br&gt; horario " 
                                            HtmlEncode="False" />
                                        <asp:BoundField DataField="totalHoras_Car" HeaderText="Horas/&lt;br&gt;Semana" 
                                            HtmlEncode="False" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:CommandField ShowSelectButton="True" SelectText="" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                            Text="Usted no tiene asignado cursos para el presente ciclo académico"></asp:Label>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#E8FFFF" />
                                    <HeaderStyle CssClass="TituloTabla" />
                                </asp:GridView>
                            </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;<asp:Label ID="lblNroEstudiantes" runat="server" ForeColor="Blue"></asp:Label>
                    &nbsp;</td>
                <td align="right">
                    
                    <asp:ImageButton ID="imgExcel" runat="server" ImageUrl="../../../images/Excel.gif" />
                    <asp:ImageButton ID="ImgImprimir" runat="server" 
                        ImageUrl="../../../../images/impresora.gif" onclientclick="javascript:print();" />
                    
                    <asp:ImageButton ID="imgGraficar" runat="server" BorderStyle="Solid" 
                        ImageUrl="../../../../images/stats.gif" ToolTip="Graficar" />
                    
                    &nbsp; </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" 
                        HorizontalAlign="Center" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pregunta_eva" HeaderText="Preguntas de Evaluación" >
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Puntaje &lt;br&gt; Autoevaluación" 
                                DataField="Prom_Docente" DataFormatString="{0:#,###,##0.00}" 
                                HtmlEncode="False" >
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Prom_Estudiante" DataFormatString="{0:#,###,##0.00}" 
                                HeaderText="Promedio de&lt;br&gt;Evaluación de&lt;br&gt; Estudiantes" 
                                HtmlEncode="False">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" runat="server" BorderStyle="None" ForeColor="Red" 
                                Text="No se encontraron registros de evaluación para el ciclo académico elegido"></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#9FBEFF" CssClass="TituloTabla" Height="20px" />
                    </asp:GridView>
                    </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;<asp:Label ID="lblPveinte" runat="server" Font-Bold="True" 
                        
                        Text="20. Señale algún aspecto que debe mejorar el profesor(a) - [E: Opinión del estudiante, D: Opinión del docente ]"></asp:Label>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataList ID="dtlPveinte" runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <asp:Label ID="evaluador_eedLabel" runat="server" 
                                Text='<%# Eval("evaluador_eed") %>'  />
                            :
                            <asp:Label ID="respuestaTexto_eddLabel" runat="server" 
                                Text='<%# Eval("respuestaTexto_edd") %>' />
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="EAD_ConsultarObservacionesEncuesta" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboNroEvaluacion" Name="codigo_cev" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="cboPersona" Name="codigo_per" 
                                PropertyName="SelectedValue" Type="Int32" />
                            <asp:ControlParameter ControlID="gvDesempenio" Name="codigo_cup" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
