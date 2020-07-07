<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficarEncuestaDesempenio.aspx.vb" Inherits="Encuesta_ReportesEvaluacionDocente_GraficarEncuestaDesempenio" %>

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
    <div align="center">
    
        <table style="width:100%;">
            <tr>
                <td align="left" colspan="2">
                    &nbsp;<asp:Button ID="cmdRetornar" runat="server" Text="Regresar" BackColor="#FFFFE1" 
                        BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" ForeColor="#006666" 
                        Width="85px" />
                    &nbsp;<asp:Button ID="cmdDetallado" runat="server" Text="Ver Detallado" 
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
        <table>
            <tr>
                <td>
        <DCWC:ChartToolbar ID="ChartToolbar1" runat="server" 
                        style="overflow: hidden; text-align: left;" BorderSkinStyle="Sunken" 
                        ChartID="Chart1" FrameBorderColor="ControlDarkDark" />
    
                </td>
            </tr>
            <tr>
                <td>
    
        <DCWC:Chart ID="Chart1" runat="server" 
            BorderLineColor="LightSlateGray" DataSourceID="SqlDataSource2" Height="513px"  
            Palette="Tan" Width="773px" BackGradientEndColor="Tan" BorderLineStyle="Solid">
            <legends>
                <DCWC:Legend Alignment="Center" BackColor="Lavender" BorderColor="Gray" 
                    Docking="Bottom" LegendStyle="Row" Name="Default" ShadowOffset="2">
                </DCWC:Legend>
            </legends>
            <ui>
                <toolbar>
                    <borderskin framebackcolor="SteelBlue" framebackgradientendcolor="LightBlue" 
                        pagecolor="Transparent" />
                </toolbar>
            </ui>
            <titles>
                <DCWC:Title Name="Title1" Style="Frame" Text="PERFIL DE DESEMPEÑO DOCENTE">
                </DCWC:Title>
            </titles>
            <series>
                <DCWC:Series BorderColor="64, 64, 64" ChartType="Line" 
                    CustomAttributes="LabelStyle=Center, DrawingStyle=LightToDark" 
                    Name="Puntaje de Autoevaluación" ShadowOffset="2" ShowLabelAsValue="True" 
                    ValueMembersY="Prom_Docente" ValueMemberX="numero_eva" 
                    BackGradientEndColor="200, 195, 205, 220">
                    <smartlabels enabled="True" />
                </DCWC:Series>
                <DCWC:Series BorderColor="64, 64, 64" ChartType="Line" 
                    CustomAttributes="LabelStyle=Center, DrawingStyle=LightToDark" 
                    Name="Promedio de Evaluación de Estudiantes" ShadowOffset="2" 
                    ShowLabelAsValue="True" ValueMembersY="Prom_Estudiante" 
                    BackGradientEndColor="200, 145, 175, 205">
                    <smartlabels enabled="True" />
                </DCWC:Series>
            </series>
            <chartareas>
                <DCWC:ChartArea BackColor="White" BorderColor="120, 64, 64, 64" 
                    BorderStyle="Solid" Name="Default" ShadowOffset="2">
                    <axisy linecolor="120, 64, 64, 64">
                        <majorgrid linecolor="LightSteelBlue" linestyle="Dash" />
                        <minortickmark size="2" />
                    </axisy>
                    <axisx linecolor="120, 64, 64, 64" margin="true" title="Seccion de Preguntas">
                        <majorgrid linecolor="LightSteelBlue" linestyle="Dash" />
                        <majortickmark style="Cross" />
                        <minortickmark size="2" />
                        <LabelStyle Format="G" />
                    </axisx>
                    <axisx2 linecolor="120, 64, 64, 64">
                    </axisx2>
                    <axisy2 linecolor="120, 64, 64, 64">
                    </axisy2>
                    <area3dstyle enable3d="true" light="Realistic" rightangleaxes="False" 
                        wallwidth="4" xangle="20" yangle="15" />
                </DCWC:ChartArea>
            </chartareas>
            <borderskin framebackcolor="SteelBlue" framebackgradientendcolor="LightBlue" 
                framebordercolor="100, 0, 0, 0" frameborderwidth="2" pagecolor="Transparent" />
        </DCWC:Chart>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
            SelectCommand="EAD_ConsultarPromedioObtenidoXCursoXDocenteXSeccion" 
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="codigo_per" QueryStringField="per" 
                    Type="Int32" />
                <asp:QueryStringParameter Name="codigo_cup" QueryStringField="cup" 
                    Type="Int32" />
                <asp:QueryStringParameter Name="codigo_cev" QueryStringField="cev" 
                    Type="Int32" />
                <asp:QueryStringParameter Name="codigo_cac" QueryStringField="cac" 
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
