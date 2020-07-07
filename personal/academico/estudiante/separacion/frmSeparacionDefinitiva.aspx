<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSeparacionDefinitiva.aspx.vb" Inherits="academico_estudiante_separacion_frmSeparacionDefinitiva" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../../private/estilo.css" rel="Stylesheet" type ="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
    <style type="text/css">
        .style1
        {
            font-size: medium;
            font-weight: bold;
        }
        .style2
        {
            font-size: x-small;
            text-align: justify;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" Width="100px" Height="22px" CssClass="imprimir2" /> 
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" class="style1">RESOLUCIÓN DE
                    <asp:Label ID="lblTitulo" runat="server" Text=""></asp:Label>
                </td>
            </tr>  
            <tr>
                <td align="center" colspan="2">&nbsp;<asp:Label ID="lblResolucion" runat="server" 
                        Text="" style="font-weight: 700; font-size: small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:25%" align="right"> 
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">                    
                    <asp:Label ID="lblFecha" runat="server" Font-Bold="True" 
                        style="font-size: x-small"></asp:Label>                    
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>                
            </tr>
            <tr>
                <td colspan="2">
                    <b><span class="style2">VISTOS; </span> </b> <span class="style2"> <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; El historial académico del estudiante <asp:Label ID="lblEstudiante" runat="server" Font-Bold="True" 
                        Font-Underline="True"></asp:Label> &nbsp;identificado con código de matrícula 
                    <asp:Label ID="lblmatricula" runat="server" Font-Bold="True" 
                        Font-Underline="True"></asp:Label>, y el Reglamento de Estudios de Pregrado de la Universidad Católica Santo Toribio de Mogrovejo y;
                    <br />
                    </span>
                    <br />
                    <b><span class="style2">CONSIDERANDO: </span> </b> <span class="style2"> <br />
                    <p style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Que, la Escuela de
                    <asp:Label ID="lblEscuela" runat="server" Font-Bold="True" 
                        Font-Underline="True"></asp:Label>&nbsp;es el órgano administrativo encargado de velar 
                        por la formación profesional de los 
                    estudiantes y supervisar el desempeño académico de los mismos, hasta la obtención del grado académico. <br />
                    Que en cumplimiento de dicha función, se ha realizado la  supervisión del desempeño académico de los estudiantes de la carrera 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                    </p>
                    <p style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Que, de acuerdo a la verificación fecha en su 
                        historial académico, el estudiante&nbsp;<asp:Label 
                        ID="lblEstudiante1" runat="server" Font-Bold="True" Font-Underline="True"></asp:Label>,&nbsp;desaprobó las 
                    siguientes asignaturas:</p></span></td>
            </tr>
            <tr>
                <td colspan="2" align="center">                    
                    <asp:GridView ID="gvCursos" runat="server" Width="70%" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="identificador_Cur" HeaderText="Identificador">
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_Cur" HeaderText="Curso">
                                <ItemStyle Width="60%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VecesDesaprobadas" HeaderText="Veces Desaprobadas">
                                <ItemStyle Width="20%" HorizontalAlign="Center"/>
                            </asp:BoundField>                            
                        </Columns>
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                        <RowStyle Height="22px" />
                    </asp:GridView>                    
                </td>
            </tr>
            <tr>
                <td colspan="2">                    
                    <span class="style2">
                    <div id="DivDefinitivo" runat="server">
                    <p style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Que, el artículo 
                        102 de la Ley Universitaria 30220 y el  
                        <asp:Label ID="lblArticulo" runat="server" Text=""
                        style="font-weight: 700; text-decoration: underline"></asp:Label>
&nbsp;del reglamento de Estudios de Pregrado de la USAT, establece 
                    que es causal de 
                        <asp:Label ID="lblTipoSeparacion2" runat="server" Text=""></asp:Label>&nbsp;haber desaprobado por 
                        <asp:Label ID="lblVeces" runat="server" Text=""></asp:Label>
                        &nbsp;una asignatura, supuesto en el cual se encuentra el(la) estudiante
                    <asp:Label ID="lblEstudiante2" runat="server" Font-Bold="True" 
                        Font-Underline="True"></asp:Label>
                    </p>
                    </div>
                    <div id="DivTemporal" runat="server">
                    <p style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Que, el artículo 
                        102 de la Ley Universitaria 30220 y el  
                        <asp:Label ID="lblArticulo2" runat="server" Text=""
                        style="font-weight: 700; text-decoration: underline"></asp:Label>
&nbsp;del reglamento de Estudios de Pregrado de la USAT, establece 
                    que la desaprobación de un mismo curso por tres veces da lugar a que el estudiante 
                        sea separado temporalmente por un año de la universidad, supuesto en el cual se encuentra el(la) estudiante
                    <asp:Label ID="lblEstudiante4" runat="server" Font-Bold="True" 
                        Font-Underline="True"></asp:Label>
                    </p>
                    </div>                    
                    <p style="text-align: justify">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Que, en 
                        concordancia con lo establecido en el Reglamento de Estudios de Pregrado de la 
                        Universidad Católica Santo Toribio de Mogrovejo:</p>
                    <b>SE RESUELVE</b><br />
                    <p><b>PRIMERO:</b> 
                        <asp:Label ID="lblTipoSeparacion" runat="server" Text=""></asp:Label>&nbsp;al estudiante
                    <asp:Label ID="lblEstudiante3" runat="server" Font-Bold="True" 
                        Font-Underline="True"></asp:Label>
                    &nbsp;por haber desaprobado por
                        <asp:Label ID="lblVeces2" runat="server" Text=""></asp:Label>
                    </p>
                    <p style="text-align: justify"><b>SEGUNDO:</b> Poner en conocimiento de la presente Resolución al 
                    Vicerrector Académico y al Director General de asuntos estudiantiles, así como la Administración General 
                    de la Universidad para  los fines pertinentes.</p>
                    </span>                                        
                    <center><span class="style2">Regístrese, comuníquese y archívese <br /></span><br />
                        <span class="style2">Mgtr / Dr</span> <br /><br /><br /><br /><br /> 
                    <br />
                    <br />
                        <span class="style2">
                        <asp:Label ID="lblDirectorEscuela" runat="server" Font-Bold="True"></asp:Label>
                        <br />
                        </span>
                        <b><span class="style2">Director de Escuela</span></b>
                    </center> 
                    </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
