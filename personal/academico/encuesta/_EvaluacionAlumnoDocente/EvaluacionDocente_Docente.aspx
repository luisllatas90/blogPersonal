<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EvaluacionDocente_Docente.aspx.vb" Inherits="_EvaluacionDocente_Docente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>USAT - CUESTIONARIO DE AUTOEVALUACION SOBRE LA ACTIVIDAD DOCENTE DE LOS PROFESORES</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
        .style3
        {
            color: #0000CC;
        }
        .style4
        {
            color: #003366;
        }
        .style5
        {
            color: #003366;
            font-weight: bold;
        }
        .style6
        {
            height: 31px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    
    <div style="text-align:center">
    
        <table style="width:70%;">
             <tr>
             <td align="left" class="style4">
                <asp:Image ID="Image11" runat="server" 
                     ImageUrl="https://intranet.usat.edu.pe/usat/files/2011/02/Logo-USAT-300x150.png" 
                     Width="153px" Height="72px" />
            </td>
            <td align="LEFT">
                <b style="mso-bidi-font-weight:normal">
                <span lang="ES-MX" style=" font-size :12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"
                    class="style6">CUESTIONARIO DE EVALUACIÓN SOBRE LA ACTIVIDAD DOCENTE DE LOS PROFESORES 
                </span>
                <span lang="ES-MX" style="font-size:12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                color:#292929;text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"> </BR> VICERRECTORADO ACADÉMICO 
                <br /><asp:Label ID="lblSemestre" runat="server" Text="" 
                    style="color: #336699; font-family: Arial; font-size: x-small"></asp:Label>
                </span></b>
            </td>
        </tr>
            <tr>
                <td colspan="2" valign="top">
                    &nbsp;</td>
            </tr>
            <tr style="text-align:justify;">
                <td colspan="2" valign="top">
                    Estimado profesor:
                    <asp:Label ID="txtProfesor" runat="server" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#003366" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr style="text-align:justify;">
                <td colspan="2" class="style6">
                    Estamos interesados en saber cuál es su percepción sobre <b>su actividad docente</b>, 
                    con el objetivo de mejorar el servicio brindado por ustedes.&nbsp; </td>
            </tr>
            <tr>
            <td style="text-align: center" colspan="2">
                   <!-- <asp:LinkButton ID="LinkButton1" runat="server" 
                    style="color: #FF0000; font-size: small ;"></asp:LinkButton>-->

                    <span class="style5">Sirvase elegir 
                    la asignatura. (Se evaluará una asignatura al día)</span><span class="style1"> </span><b>de la lista</b></td>
            
            </tr>
            <tr>
               <!-- <td colspan="2" style="color: #FF0000;">
                    <b>Encuestas pendientes:
                    <asp:Label ID="lblPendientes" runat="server" Text="0"></asp:Label>
                    </b></td>-->
            </tr>

</tr>
            <tr>
                <td colspan="2" align="justify">
                                <asp:GridView ID="gvDesempenio" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_cup,codigo_cac" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="nombre_Cpf" HeaderText="Escuela" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_Cur" HeaderText="Asignaturas" >
                                            <ItemStyle HorizontalAlign="Left" />
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
                                        <asp:BoundField HeaderText="Cumplimiento de obligaciones" />
                                        <asp:BoundField HeaderText="Nivel Académico" />
                                        <asp:BoundField HeaderText="Metodología" />
                                        <asp:BoundField HeaderText="Materiales" />
                                        <asp:BoundField HeaderText="Actitud del profesor" />
                                        <asp:BoundField HeaderText="Evaluación" />
                                        <asp:BoundField HeaderText="Valoración &lt;/br&gt;global" HtmlEncode="False" />
                                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                            Text="La evaluación de cursos ha finalizado "></asp:Label>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <HeaderStyle CssClass="TituloTabla" />
                                </asp:GridView>
                            </td>
            </tr>
               <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr style="text-align:justify;">
            <td colspan="2"><br />Exprese <b>su valoración para cada item</b>, seleccionando el círculo que mejor corresponda, de acuerdo a la escala que se muestra a continuación:<br /><br /></td>
            </tr>
         
            <tr>
                <td colspan="2" align="center">
                    <b>Nunca</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Algunas veces&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Casi siempre&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Siempre&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No 
                    Sabe<br />
                    <span class="style2No sabe</span></td>
            </tr>
            <tr>
                <td colspan="2" align="left" class="style4">
                    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ----1--------------------------------2---------------------------------3---------------------------4-----------------------NS/NA----</tr>
            <tr>
                <td colspan="2" align="center">
                    <b>Total desacuerdo</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Desacuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; 
                    De acuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    Total acuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
                        class="style3">&nbsp; 
                    No aplica</span></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;</td>
            </tr>
  	<tr style="text-align:center;">
            <td colspan="2"><div id="txtObligatorio" runat="server" style="border:1px solid #800000;padding:4px; width:250px;"></div></td>
        </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="cmdGuardarArriba" runat="server" Text="   Guardar" 
                        ValidationGroup="Guardar" CssClass="guardar" />
                &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_eva,conrespuesta_eva,orden_eva" 
                        HorizontalAlign="Center" Width="55%" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                                <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pregunta_eva" HeaderText="Items " >
                                <ItemStyle HorizontalAlign="Left" Width="90%" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="" />
                            <asp:BoundField HeaderText="" />
                            <asp:BoundField HeaderText="" />
                            <asp:BoundField HeaderText="" />
                            <asp:BoundField HeaderText="" HtmlEncode="False"></asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#0066CC" CssClass="TituloTabla" Height="20px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <!--<td colspan="2" style="text-align:left;">
                    &nbsp;<asp:Label ID="lblComentarioPVeinte" runat="server" 
                        
                        Text="La siguiente pregunta servirá para conocer que aspecto usted considera que debería mejorar, sirvase contestarla para finalizar la evaluación" 
                        style="text-align: justify"></asp:Label></td>-->
            </tr>
            <tr>
                <td colspan="2" align="left">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPVeinte" runat="server" 
                        Text="20. Señale algún aspecto que Ud. debe mejorar como profesor(a)" 
                        style="font-weight: 700"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtPVeinte" ErrorMessage="La pregunta 20 es obligatoria" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    &nbsp;&nbsp;
                    <asp:TextBox ID="txtPVeinte" runat="server" Width="68%" TextMode="MultiLine" 
                        BorderStyle="Solid" Font-Names="Arial"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:HiddenField ID="HddPVeinte" runat="server" />
                        <asp:Label ID="lblEncuesta" runat="server" Font-Size="Large" ForeColor="Blue" 
                            Text="Encuesta no disponible"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button ID="cmdGuardarAbajo" runat="server" Text="   Guardar" 
                        ValidationGroup="Guardar" CssClass="guardar" />
                &nbsp;&nbsp;
                </td>
            </tr>
        </table>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ValidationGroup="Guardar" ShowMessageBox="True" ShowSummary="False" />
    <asp:HiddenField ID="hddCodigo_cev" runat="server" />
    <asp:HiddenField ID="hddcodigo_cac" runat="server" />
    </form>
</body>
</html>
