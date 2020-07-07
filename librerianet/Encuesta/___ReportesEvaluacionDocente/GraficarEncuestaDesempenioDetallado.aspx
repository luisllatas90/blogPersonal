<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficarEncuestaDesempenioDetallado.aspx.vb" Inherits="Encuesta_ReportesEvaluacionDocente_GraficarEncuestaDesempenioDetallado" %>

<%@ Register assembly="DundasWebChart" namespace="Dundas.Charting.WebControl" tagprefix="DCWC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td align="justify">
                    &nbsp;<asp:Button ID="cmdRetornar" runat="server" Text="Regresar" BackColor="#FFFFE1" 
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006666" 
                        Width="85px" />
                    &nbsp;<asp:Button ID="cmdDetallado" runat="server" Text="Ver General" 
                        BackColor="#FFFFE1" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" 
                        ForeColor="#006666" Width="85px" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Profesor:</b>&nbsp;
                    <asp:Label ID="lblProfesor" runat="server"></asp:Label>
&nbsp;&nbsp;
                </td>
                <td align="left">
                    <b>Escuela: </b>
                    <asp:Label ID="lblEscuela" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>Curso:</b>&nbsp;
                    <asp:Label ID="lblCurso" runat="server"></asp:Label>
&nbsp;
                </td>
                <td align="left">
                    <b>Grupo horario: </b>
                    <asp:Label ID="lblGrupoHor" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    <table align="center">
        <tr>
            <td>
                <DCWC:ChartToolbar ID="ChartToolbar1" runat="server" ChartID="Chart1" 
                    Height="40px" style="overflow: hidden; text-align: left;" 
                    BorderSkinStyle="Sunken" FrameBorderColor="ControlDarkDark" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <DCWC:Chart ID="Chart1" runat="server" BackGradientEndColor="Tan" BorderLineColor="LightSlateGray" 
                    BorderLineStyle="Solid" DataSourceID="SqlDataSource2" Height="538px" 
                    Palette="Tan" Width="802px">
                    <legends>
                        <DCWC:Legend Alignment="Center" BackColor="50, 255, 255, 255" 
                            BorderColor="LightSlateGray" Docking="Bottom" LegendStyle="Row" Name="Default">
                        </DCWC:Legend>
                    </legends>
                    <ui>
                        <toolbar backcolor="Transparent" borderstyle="NotSet">
                            <borderskin framebackcolor="SteelBlue" framebackgradientendcolor="LightBlue" 
                                pagecolor="Transparent" />
                        </toolbar>
                    </ui>
                    <titles>
                        <DCWC:Title Name="Title1" Style="Frame" 
                            Text="REPORTE DETALLADO DE EVALUACIÓN DOCENTE POR PREGUNTA">
                        </DCWC:Title>
                    </titles>
                    <series>
                        <DCWC:Series BackGradientEndColor="50, 191, 201, 224" BackGradientType="TopBottom" 
                            BorderColor="64, 64, 64" ChartType="Line" Font="Microsoft Sans Serif, 8.25pt" 
                            MarkerStyle="Circle" Name="Autoevaluación Docente" ValueMembersY="PuntajeDoc" 
                            ValueMemberX="numero_eva" MarkerSize="3" ShadowOffset="1">
                            <smartlabels enabled="True" />
                        </DCWC:Series>
                        <DCWC:Series BackGradientEndColor="50, 151, 209, 248" BackGradientType="TopBottom" 
                            BorderColor="64, 64, 64" ChartType="Line" Font="Microsoft Sans Serif, 8.25pt" 
                            MarkerStyle="Circle" Name="Promedio Evaluacion de Estudiantes" 
                            ValueMembersY="PuntajeEst" MarkerSize="3" ShadowOffset="1">
                            <smartlabels enabled="True" />
                        </DCWC:Series>
                    </series>
                    <chartareas>
                        <DCWC:ChartArea BackColor="White" BackGradientEndColor="White" 
                            BorderColor="LightSlateGray" BorderStyle="Solid" Name="Default">
                            <axisy>
                                <majorgrid linecolor="65, 0, 0, 0" />
                                <minorgrid linecolor="30, 0, 0, 0" />
                                <majortickmark style="None" />
                                <minortickmark size="2" />
                            </axisy>
                            <axisx labelsautofit="False" title="Número de Preguntas">
                                <majorgrid linecolor="65, 0, 0, 0" />
                                <minorgrid linecolor="30, 0, 0, 0" />
                                <minortickmark size="2" />
                                <LabelStyle Format="G0" />
                            </axisx>
                            <area3dstyle enable3d="True" wallwidth="0" />
                        </DCWC:ChartArea>
                    </chartareas>
                    <borderskin framebackcolor="SteelBlue" framebackgradientendcolor="LightBlue" 
                        framebordercolor="100, 0, 0, 0" frameborderwidth="2" pagecolor="AliceBlue" />
                </DCWC:Chart>
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="EAD_ConsultarPromedioObtenidoXCursoXDocenteXPregunta" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter Name="codigo_per" QueryStringField="per" 
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="codigo_cup" 
                QueryStringField="cup" Type="Int32" />
            <asp:QueryStringParameter Name="codigo_cev" QueryStringField="cev" 
                Type="Int32" />
            <asp:QueryStringParameter Name="codigo_cac" QueryStringField="cac" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
