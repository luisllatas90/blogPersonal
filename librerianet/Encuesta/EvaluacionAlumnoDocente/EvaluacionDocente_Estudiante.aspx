<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EvaluacionDocente_Estudiante.aspx.vb" Inherits="EvaluacionDocente_Estudiante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>EVALUACIÓN VIRTUAL DE LA FUNCIÓN DOCENTE  - VICERRECTORADO ACADÉMICO - USAT</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
        .style2
        {
            height: 35px;
        }
        .style3
        {
            height: 24px;
        }
        .style4
        {
            width: 440px;
        }
        .style5
        {
            font-size: small;
        }
        .style6
        {
            color: #800000;
        }
        
    </style>
</head>
<body>
<center>
    <form id="form1" runat="server">
    <table style="width:70%;">
        <tr>
             <td align="left" class="style4">
                <asp:Image ID="Image11" runat="server" 
                     ImageUrl="http://www.usat.edu.pe/web/wp-content/uploads/2015/04/logousat.png" 
                     Width="153px" Height="72px" />
            </td>
            <td align="LEFT">
                <b style="mso-bidi-font-weight:normal">
                <span lang="ES-MX" style=" font-size :12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"
                    class="style6">EVALUACIÓN VIRTUAL DE LA FUNCIÓN DOCENTE 
                </span>
                <span lang="ES-MX" style="font-size:12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                color:#292929;text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"> </BR> VICERRECTORADO ACADÉMICO 
                <br /><asp:Label ID="lblSemestre" runat="server" Text="" 
                    style="color: #336699; font-family: Arial; font-size: x-small"></asp:Label>
                </span></b>
            </td>
        </tr>
        <tr><td class="style4"><br /><br />
            </td></tr>
        <tr>
            <td colspan="2" style="text-align:justify">
                <span class="style5">Estimado Estudiante: <br />
                Estamos interesados en saber cuál es su percepción sobre la actividad docente 
                del profesor, con el objetivo de mejorar el servicio brindado.<br />
                Sírvase tener en cuenta las siguientes recomendaciones para evaluar el curso: </span> 
                <asp:Label ID="lblCurso" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="justify">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="justify" style="color: blue">
                Seleccione el profesor que dicta el siguiente curso:</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <b><asp:GridView ID="gvDesempenio" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="codigo_cup,codigo_cac,codigo_per" 
                    Width="98%">
                                    <Columns>
                                    <asp:BoundField DataField="docente" HeaderText="Profesor" 
                                            HtmlEncode="False" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" >
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Autoevaluación" Visible="false" />
                                        <asp:BoundField HeaderText="Cumplimiento de obligaciones" Visible="false" />
                                        <asp:BoundField HeaderText="Nivel Académico" Visible="false" />
                                        <asp:BoundField HeaderText="Metodología" Visible="false" />
                                        <asp:BoundField HeaderText="Materiales" Visible="false" />
                                        <asp:BoundField HeaderText="Actitud" Visible="false" />
                                        <asp:BoundField HeaderText="Evaluación" Visible="false" />
                                        <asp:BoundField HeaderText="Valoración global" Visible="false"/>
                                        <asp:CommandField SelectText="" ShowSelectButton="True" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                            Text="No se encontraron profesores a evaluar"></asp:Label>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle BackColor="#FFFFCC" />
                                    <HeaderStyle CssClass="TituloTabla" />
                                </asp:GridView>
                            </b></td>
        </tr>
        <tr>
            <td colspan="2" align="justify">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="justify">
                Expresa tu valoración para cada ítem, seleccionando el círculo que mejor 
                corresponda, de acuerdo a la escala que se muestra a continuación:</tr>
        <tr>
            <td colspan="2" style="font-weight: 700" class="style1">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Nunca&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Algunas veces&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Casi siempre&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 
                Siempre&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                No Sabe&nbsp;&nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td colspan="2" style="font-weight: 700" class="style1">
                -1----------------------2------------------------3------------------------4------------------------NS/NA</td>
        </tr>
        <tr>
            <td colspan="2" class="style1">
                Total desacuerdo &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Desacuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                De acuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Total acuerdo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                No Aplica</td>
        </tr>
        <tr>
            <td colspan="2" class="style1"><hr /></td>
        </tr>
        <tr>
            <td colspan="2"><div id="txtObligatorio" runat="server" style="border:1px solid #800000; width:250px;"></div></td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:Button ID="cmdGuardarArriba" runat="server" Text="   Guardar" 
                    ValidationGroup="Guardar" CssClass="guardar" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvPreguntas" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="codigo_eva,conrespuesta_eva,orden_eva" 
                    HorizontalAlign="Center" Width="65%" CellPadding="4" 
                    ForeColor="#333333" BorderColor="Maroon" GridLines="None">
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" BorderStyle="Solid" 
                        VerticalAlign="Middle" Wrap="False" />
                    <Columns>
                        <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                            <ItemStyle Width="15px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pregunta_eva" HeaderText="Items de evaluación" >
                            <ItemStyle HorizontalAlign="Left" Width="90%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="">
<ControlStyle Width="350px"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="" >
<ControlStyle Width="350px"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="">
<ControlStyle Width="350px"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="">
<ControlStyle Width="350px"></ControlStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="">
<ControlStyle Width="250px"></ControlStyle>
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <HeaderStyle Height="20px" BackColor="#990000" ForeColor="White" 
                        Font-Bold="True" BorderStyle="Dotted" />
                    <AlternatingRowStyle BorderColor="#3366CC" BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style=" text-align: justify">
                &nbsp;<asp:Label ID="lblComentarioPVeinte" runat="server" 
                    Text="La siguiente pregunta servirá como apoyo a que el profesor mejore, sirvase contestarla para finalizar la evaluación"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style=" text-align: justify">
                &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblPVeinte" runat="server" 
                        Text="20. Señale algún aspecto que debe mejorar el profesor(a)"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtPVeinte" ErrorMessage="La pregunta veinte es obligatoria" 
                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="txtPVeinte" runat="server" Width="97%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="style2">
                    <asp:HiddenField ID="HddPVeinte" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right" class="style3">
                <asp:Button ID="cmdGuardarAbajo" runat="server" Text="   Guardar" 
                    ValidationGroup="Guardar" CssClass="guardar" />
            </td>
        </tr>
    </table>
    <div>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ValidationGroup="Guardar" ShowMessageBox="True" ShowSummary="False" />
    
    </div>
    <asp:HiddenField ID="hddCodigo_cev" runat="server" />
    <asp:HiddenField ID="hddcodigo_cac" runat="server" />
    </form>
 </center>
</body>
</html>
