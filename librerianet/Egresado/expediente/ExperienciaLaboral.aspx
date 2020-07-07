<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExperienciaLaboral.aspx.vb" Inherits="ExperienciaLaboral" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Experiencia Profesional</title>	      
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
            document.getElementById("cmdGuardar0").setAttribute("href", "ExperienciaLaboralRegistrar.aspx");
        
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
        .guardar  	{padding: 5px 10px;
            border: 1px solid #C0C0C0;
            background: #FEFFE1;            
            width:146px; 
            font-family:Tahoma;     font-size:8pt;     font-weight:bold;     height:25; text-decoration:none;
        }
        a , img { text-decoration:none; color:Black; border:none; cursor:hand; outline:none; width:16px; height:16px; }
        .centrar{ text-align:center;}
        
       .tabla { text-align:center;border-right: 1px solid #2F4F4F;  font-size: 8px;}
        .tabla th
        { 
          background-color:#F3F3F3;
          color:#002B56;/*#003366;*/
          padding:4px;
          border-top: 1px solid #003366 ;
          border-bottom: 1px solid #003366 ;
          border-left: 1px solid #003366 ;
        }
        .tabla td
        { 
          padding:3px; border-left: 1px solid #003366;  border-bottom: 1px solid #003366;
        }
        .fila1
        { 
          background-color:#F7F4F4;
          color:black;         
          padding:3px;    
         
        }
        .fila2
        { background-color:white;
          color:black;
          padding:3px;         
        }
        
        .style1
        {
            width: 800px;
        }
        
    </style>
  </head>
  <body>	
	<form id="form1" runat="server">	
	<table cellpadding="0" cellspacing="0" class="" style="width: 100%;" >
	  <tr><td style="color:White; background:#e33439; font-size:14px;padding:3px;">&nbsp;Experiencia Laboral</td></tr>
	</table><br />
	
     <div style="float:right;padding-bottom:3px;">
        <a runat="server" id='cmdGuardar0' class='ifancybox guardar' style="text-decoration:none; color:Black;" href="ExperienciaLaboralRegistrar.aspx">Agregar Experiencia</a>
     </div>   
     
     
	  <br /> <br />
       <div id="GridExperiencia" runat="server">  </div>  
       <p align="right">
       <br />
       <asp:Button ID="Button2" runat="server" CssClass="guardar" Enabled="true" 
               Text="Anterior" SkinID="BotonGuardar" Width="120px" />&nbsp;&nbsp;&nbsp;
       <asp:Button ID="Button1" runat="server" CssClass="guardar" Enabled="true" Text="Continuar" SkinID="BotonGuardar" Width="120px"  />
       </p>
	      
   
  </form>
 </body>
 </html>

