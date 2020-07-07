<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaBibliotecaPersonal.aspx.vb" Inherits="EvaluacionDocente_Estudiante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>USAT - Encuesta sobre la Calidad de Servicio de Biblioteca</title> 
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>    
    <style type="text/css">
        p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:10.0pt;
	font-family:"Times New Roman","serif";
	        margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        
        .style1
        {
            text-align: justify;
        }
        
       
        p.MsoBodyText
	{margin-bottom:.0001pt;
	text-align:justify;
	font-size:11.0pt;
	font-family:"Times New Roman","serif";
	        margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        
       
        </style>
        <script type="text/javascript">
            function selecciono(i) {

                if (i == 1) {

                   
                    document.getElementById("capa").style.borderColor = "#76b7f5";
                    document.getElementById("txtPreguntaAbierta").focus();

                }
                else {

                    document.getElementById("capa").style.borderColor = "white";
                }
                
           }
        </script>
</head>
<body>
<center>
    <form id="form1" runat="server" name="formulario">
    <table style="width:50%;">
        <tr>
             <td align="left">
             <!--
                '---------------------------------------------------------------------------------------------------------------
                'Fecha: 29.10.2012
                'Usuario: dguevara
                'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                '----------------------
             -->
                <asp:Image ID="Image1" runat="server" 
                     ImageUrl="https://intranet.usat.edu.pe/usat/files/2011/02/Logo-USAT-300x150.png" 
                     Width="153px" Height="72px" />
            </td>
            <td align="right">
            &nbsp; <b>Biblioteca P. DIONISIO QUESQUÉN</b>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">   
                  <b style="mso-bidi-font-weight:normal"><span lang="ES-MX" style="font-size:12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                color:#292929;text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX">LIBQUAL+: VERSIÓN EN ESPAÑOL</span></b>
            </td> 
        </tr>
        <tr>
            <td align="center" colspan="2">   
              <b style="mso-bidi-font-weight:normal"><span lang="ES-MX" style="font-size:12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                color:#292929;text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX">calidad del  servicio de la biblioteca<o:p></o:p></span></b>
            </td>            
        </tr>
        <tr>         
         <td colspan="2" class="style1">
                <b>Estimado :</b>
                <br />
                <br/>
                Tu participación en esta encuesta nos permitirá determinar el nivel de satisfacción con el servicio que ofrece la Biblioteca de la Universidad Católica “Santo Toribio de Mogrovejo”. Te agradeceremos respondas todas las preguntas.
                <br/><br/>
                </td>
         </tr>
         <tr>
            <td style="color:Red; border:1px solid red;">
            <asp:LinkButton ID="LinkButton1" runat="server" 
                    style="color: #FF0000; font-size: medium"><i>Clic aquí  para responder la encuesta más tarde</i></asp:LinkButton>
            </td>
        </tr>
         <tr>
         <td align="left">
                <b>Indicaciones:</b>
                <br/><br/>
                En esta encuesta se han considerado tres niveles de servicio:
                <br /><br/>
                <b>Mínimo:</b> representa el nivel mínimo de servicio que encontrarías aceptable.<br/>
                <b>Deseado:</b> representa el nivel de servicio que deseas encontrar en la biblioteca.<br/>
                <b>Percibido:</b> representa el nivel de servicio que consideras ofrece actualmente la biblioteca.<br/>
                <br />
                Por favor, evalúa los siguientes enunciados y selecciona la alternativa que identifique el nivel del servicio. 
                Deberás evaluar las 3 filas de la izquierda y elegir el valor que creas conveniente, entre <b>1 y 5.</b>
            </td>
        </tr>
        
        <tr>
        <td>       <br /><br />
            <img src="leyendaencuestabib.png" style="width: auto; height: auto" /></td>
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
                    HorizontalAlign="Center" GridLines="Horizontal" Width="98%">
                    <Columns>
                        <asp:BoundField DataField="numero_eva" HeaderText="Nº" >
                            <ItemStyle Width="15px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="pregunta_eva" HeaderText="Items de evaluación" >
                            <ItemStyle HorizontalAlign="Left" Width="90%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="1" />
                        <asp:BoundField HeaderText="2" />
                        <asp:BoundField HeaderText="3" />
                        <asp:BoundField HeaderText="4" />
                        <asp:BoundField HeaderText="5" />
                    </Columns>
                    <HeaderStyle Height="20px" BackColor="#0066CC" ForeColor="White" />
                    <AlternatingRowStyle BorderColor="#3366CC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: left">
                 <br />
                 17. ¿Cómo te enteras de los nuevos servicios de la Biblioteca?:<br />
                    <input name="radioservicio" id="Radio1" type="radio" value = "1" runat="server" onclick="selecciono(0);"/>Capacitaciones&nbsp;
                    <input name="radioservicio" id="Radio2" type="radio" value = "2" runat="server" onclick="selecciono(0);"/>Página web de Biblioteca&nbsp;
                    <input name="radioservicio" id="Radio3" type="radio" value = "3" runat="server" onclick="selecciono(0);" />Correo Electrónico&nbsp;
                    <input name="radioservicio" id="Radio4" type="radio" value = "4" runat="server" onclick="selecciono(0);" />Profesores&nbsp;
                    <input name="radioservicio" id="Radio5" type="radio" value = "5" runat="server" onclick="selecciono(1);" />Otro&nbsp;
                 <br /><br />
                 <div id="capa" style="border:1px solid white;">
                 Si seleccionó [5]Otro, favor de especificar cuál:
                 <br /><br />
                 <input type="text" id="txtPreguntaAbierta" runat="server" size="80"/>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
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
