<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormacionAcademica.aspx.vb" Inherits="FormacionAcademica" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Formación Académica</title>	      
      <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../jquery/fancy/jquery.js" type="text/javascript"></script>
    <script src="../jquery/fancy/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../jquery/fancy/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />

    <script type="text/javascript">
     $(document).ready(function() {
         $(".ifancybox").fancybox({
                'width': '90%',
                'height': '90%',
                'autoScale': false,
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'type': 'iframe'
            });
        });
        function funcion() {
            document.getElementById("cmdGuardar0").setAttribute("href", "FormacionAcademicaRegistrar.aspx");
        }
      
    function confirm_delete() {
        return confirm('¿Desea borrar este registro?');
    }
</script>
<style type="text/css">
    body {
        color: #2F4F4F;
        font-family: "Trebuchet MS","Lucida Console",Arial,san-serif;
        font-size: 8px;
        padding-left:5px;
        padding-top:10px;
        }
        .guardar{
            padding: 5px 10px;
            border: 1px solid #C0C0C0;
            background: #FEFFE1;
            width:112px; 
            font-family:Tahoma;     font-size:8pt;     font-weight:bold;     height:25
    }
        a , img { text-decoration:none; color:Black; border:none; cursor:hand; outline:none; width:16px; height:16px; }
        .centrar{ text-align:center;}
        
        .tabla { text-align:center;border-right: 1px solid #2F4F4F;  font-size: 8px;}
        .tabla th
        { 
          background-color:#2F4F4F;
          color:White;
          padding:4px;
          border: 1px solid #2F4F4F;
        }
        .tabla td
        { 
          padding:3px; border-left: 1px solid #2F4F4F;  border-bottom: 1px solid #2F4F4F
        }
        .fila1
        { 
          background-color:#F9F9DE;
          color:black;         
          padding:3px;    
         
        }
        .fila2
        { background-color:white;
          color:black;
          padding:3px;         
        }
        
        .CabeceraFA
        {
            color: #0000FF;
            font-weight: bold;
            width: 750px;
        }
        
    </style>
  </head>
  <body>	
	<form id="form1" runat="server">	
	<table cellpadding="0" cellspacing="0" class="" style="width: 100%;">
	  <tr><td style="color:White; background:#e33439; font-size:14px;padding:3px;">&nbsp;Formación Académica</td></tr>
	</table>&nbsp;
     <table>
        <td class="CabeceraFA">GRADOS ACADÉMICOS<hr /></td>
        <td><a runat="server" id='cmdGuardar0' class='ifancybox guardar' href="FormacionAcademicaRegistrar.aspx">Agregar Grado</a></td>
     </table>
     <div id="GridFormacion" runat="server"></div>
     <br /><br />
     <table>
        <td class="CabeceraFA">TÍTULOS PROFESIONALES<hr /></td>
        <td><a runat="server" id='A1' class='ifancybox guardar' href="FrmRegistraTitulo.aspx">Agregar Título</a></td>
     </table>
     <div id="GridTitulo" runat="server"></div>
     <br /><br />
     <table>
        <td class="CabeceraFA">IDIOMAS<hr /></td>
        <td><a runat="server" id='A2' class='ifancybox guardar' href="FrmRegistraIdioma.aspx">Agregar Idioma</a></td>
     </table>
     <div id="GridIdioma" runat="server"></div>
     <br /><br />
     <table>
        <td class="CabeceraFA">OTROS ESTUDIOS<hr style="width: 750px" /></td>
        <td><a runat="server" id='A3' class='ifancybox guardar' href="FrmRegistraOtros.aspx">Agregar Estudio</a></td>
     </table>
     <div id="GridOtros" runat="server"></div>   
  </form>
 </body>
 </html>
