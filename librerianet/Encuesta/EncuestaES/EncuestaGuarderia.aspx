﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaGuarderia.aspx.vb" Inherits="EvaluacionAlumnoDocente_EncuestaGuarderiaInfantil" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">
        function o_O()
        {
            location.href = 'EncuestaGuarderiaSalir.aspx?rpta=' + getRadioButtonSelectedValue(document.form1.Respuesta_750) ;
        }
        
        
    function getRadioButtonSelectedValue(ctrl)
    {
            for(i=0;i<ctrl.length;i++)
                    if(ctrl[i].checked) return ctrl[i].value;
    }

    /*function validar() {
        var sAux = "";
        var frm = document.getElementById("form1");
        var tipo = "";
        var opciones ="";
        for (i = 0; i < frm.elements.length; i++) {
            //sAux += "NOMBRE: " + frm.elements[i].name + " ";
            sAux += "TIPO :  " + frm.elements[i].type + " ";
            tipo = frm.elements[i].type + " ";
            if (tipo = "radio") {
              
                }
             }   
                                   
    }*/
     /*   formulario = document.getElementById("frm1");
        for (var i = 0; i < formulario.elements.length; i++) {
            var elemento = formulario.elements[i];
            if (elemento.type == "checkbox") 
                { if (!elemento.checked) { return false; } } }*/
        
    
    </script>
    <style type="text/css">
    table {
	font-family: Trebuchet MS;
	font-size: 8pt;
	margin-right: 0px;}
TBODY {
	display: table-row-group;
}
tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	color: #2F4F4F;
}
        .style4
        {
            width: 338px;
        }
        
.guardar  	{border:1px solid #C0C0C0; background:#FEFFE1 url('../images/guardar.gif') no-repeat 0% 50%; width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; }
.guardar {
	border: 1px solid #C0C0C0;
	background: #FEFFE1 url('../images/guardar.gif') no-repeat 0% 80%;
	width: 100;
	font-family: Tahoma;
	font-size: 8pt;
	font-weight: bold;
	height: 25;
}
        .style6
        {
            color: #800000;
        }
        
        .style7
        {
            text-align: justify;
            font-size: small;
        }
        .style12
        {
            text-align: justify;
            font-size: small;
            font-weight: bold;
            color: #292929;
        }
             .style8
        {
            text-align: justify;
            font-size: small;
            font-weight:bold;
            height: 16px;
            padding:4px 0px 4px 30px;
        }
        .btn 
        { cursor:hand;
          font-size: 12px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server" method="POST" action="EncuestaGuarderia.aspx">
<center>
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
                    class="style6">
                <br />
                Plan de Negocios para la creación de una guardería infantil en la Universidad.<br />
&nbsp;</span><span lang="ES-MX" style="font-size:12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                color:#292929;text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"></BR> 
                encuesta al estudiante 
                <br /><asp:Label ID="lblSemestre" runat="server" Text="" 
                    style="color: #336699; font-family: Arial; font-size: x-small"></asp:Label>
                </span></b>
            </td>
        </tr>
        <tr><td class="style4"><br /><br />
            </td></tr>
        <tr>
            <td colspan="2" class="style7">
                <b>Estimada Estudiante:<br />
                <br />
                </b>Sabemos que más del 50% de estudiantes de nuestra universidad son mujeres, 
                pero además que muchas de ustedes ya son madres o algunas que ya están por 
                serlo; por lo que estamos analizando la posibilidad de implementar una guardería 
                infantil en el campus universitario o cerca a él, como una ayuda para que pueda 
                realizar sus actividades y obtengan un mejor desempeño. Agradecemos de antemano 
                su sinceridad y colaboración. <br />
            </td>
        </tr>
        <tr>
            <td class="style12" colspan="2" style="text-align:center;">
                <label id="txtObligatorio" runat="server">
                
                </label>           
                </td>
        </tr>
        <tr>
            <td class="style12" colspan="2">
            <asp:Button ID="Button1" runat="server" Text="Guardar Respuestas" 
                    class="guardar btn"  /><br />
            <div id="TablaPreguntas" runat="server">                                
                
            </div>
            <br />
             <asp:Button ID="Button2" runat="server" Text="Guardar Respuestas" class="guardar" />
            </td>
           
        </tr>
        </table>
</center>
    </form>
</body>
</html>
