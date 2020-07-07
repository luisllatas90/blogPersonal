<%@ Page Language="VB" AutoEventWireup="false" CodeFile="prueba report view.aspx.vb" Inherits="indicadores_frmTableroPrincipal" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
 <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
     
     <style type="text/css">
        .usatFormatoCampo
        {
        	float:left; 
        	font-weight:bold;        	     	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:10px;
        	/*width:200px;*/
        	width:20%        	
        }
        
        .usatFormatoCampoAncho
        {
        	float:left; 
        	font-weight:bold;        	       	
        	padding-left:30px;
        	color:#27333c;
        	font-family: Arial;
        	height:20px;
        	width:65%;
        	/*border:1px solid red;*/
        }
        
        .usatTituloAzul {
	        font-family: Arial;
	        font-size: 12pt;
	        font-weight: bold;
	        color: #3063c5;            
            width: 831px;
            height: 20px;
        }
               
        .usatPanel
        {        
            border: 1px solid #C0C0C0;	        
	        height:100px;	        
	        -moz-border-radius: 15px; 
	        /*padding-top:10px;*/	        
        }
        
        .usatPanelConsulta
        {        
            border: 1px solid #C0C0C0;	        	        
	        /*-moz-border-radius: 15px; */
	        padding-top:10px;
	        margin-top:10px; 
	        padding-bottom:10px;	    	                  
        }
        
        #lblSubtitulo
        {
        	position:relative;
        	top: -10px;
        	left: 10px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;        	        
        	text-align:center;
        }
                
         #lblConsulta
        {
        	position:relative;
        	top: -20px;
        	left: 10px;
        	font-size:10pt;
        	color:#3063c5;
        	font-weight:bold;
        	background-color:White;        	      
        	text-align:center;
        }

        
        .borde-redondeado {
	        border-radius:20px;
	        -moz-boder-radius:20px; 
	        -webkit-border-radius:20px;
	        behavior: url(../css/border-radius.htc);	        	        
	        width:1200px;
	        height:780px;
	        padding-top:5px;
	        text-align:center;	        
        }
        
        .menu 
        {
        	width:230px;
        	min-height: 50px;
        	background-color:#23588c;
        	padding-top:30px;
        	color: White;
        	font-size:medium;
        	text-align: left;
        	padding-left: 20px;        	
        }
         A:link {color:#FFFFFF;}         
         A:visited { text-decoration: none; color:#487ec0;}
         A:hover { text-decoration: underline; color:#666666; }
                
        .degradado   
        {     
        	    width: 200px;
        	    height: 70px;
        	    color: White;
        	    text-align: left;
        	    padding-top: 25px;
        	    font-size:medium;
        	    padding-left: 20px;
        	    font-weight:bold;   
        	   
        	   	   
              /* Color alternativo para versiones que no soporten degradados */  
              background-color:#2B93D2;  
              
              /* Safari 4+ y Chrome 1+ */  
              background-image:-o-linear-gradient(top, #2B93D2, #77BCE6);  
              
              /* Safari 5.1+ y Chrome 10+ */  
              /* Firefox 3.6+ */  
              /* Opera 11.10+ */
         }  
        
        /********************************/   
        
        #cuerpo 
        {
        	 padding:8px; 
                background-color: #f5f5f5; 
                width: 76%; 
                border-bottom: 1px solid #999999; 
                border-right: 1px solid #999999;
        }
        
        #titulo 
        {
        	font-size:x-large;
        	text-align: center;
        	color:#576e7e;
        	height:60px;
        }
        
        #nombreperspectiva
        {
        	font-size:large;
        	text-align: center;
        	height: 50px;
        }
        
         #titulopagina 
        {
        	font-size:x-large;
        	text-align: right;
        	color:#01A9DB;          		
        }
        
        #seleccion 
        {
        	float:left;
        	width: 40%;
        	text-align:left;
        	font-size:medium;
        }
        
        #display
        {
        	float:left;
        	width: 60%
        }
        
        #grafico 
        {
        	height:300px
        }
        
        #reporte
        {
        	height:300px        	        
        }
        
        .ocultar
        {
        	display:none        	
        }
        
         .combos
        {
        	width:400px
        }
                        
    </style>
    
    
    <script language = "javascript" type="text/javascript">
         
        function CargarCuerpoPerspectiva(codigo_pers, nombre_pers) {

//            $("#divobjetivo div").each(function(index) {
//                $(this).removeClass();
//                $(this).addClass("ocultar");

//                alert($(this).id);


            //            })
            
            //Ocultar todos los divs que esten dentro del panel
            capas = document.getElementById("panelComboObjetivo").getElementsByTagName("div");

            for (i = 0; i < capas.length; i++) {
                
                capas[i].style.display = 'none';
                //document.getElementById('div' + codigo_pers).style.display = 'inline';
                //document.location.href = '#div' + codigo_pers;
            }
            
            //Mostrar solo el div del combo que corresponde a la perspectiva seleccionada
            div = document.getElementById("div" + codigo_pers);
            div.style.display = 'block';

            //Mostrar como titulo el nombre de la perspectiva seleccionada
            nombreperspectiva.innerHTML = nombre_pers;

        }
        
        var ddlIndicadores;
        function GetCountries() {
            ddlIndicadores = document.getElementById("<%=ddlIndicadores.ClientID %>");
            ddlIndicadores.options.length == 0;
            AddOption("Loading", "0");
            PageMethods.GetCountries(OnSuccess);
        }
//        window.onload = GetCountries;

        function OnSuccess(response) {            
            ddlIndicadores.options.length = 0;
            //AddOption("Please select", "0");
            for (var i in response) {
                AddOption(response[i].Name, response[i].value);
            }
        }
        function AddOption(text, value) {
            var option = document.createElement('<option value="' + value + '">');            
            ddlIndicadores.options.add(option);
            option.innerText = text;

        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                
    <div style="background-color:#255c95; padding-top:20px; padding-left:20px; padding-right:20px; padding-bottom:20px; position:relative">
    <div class="borde-redondeado" style="background-color:#FFFFFF">
        <div style="height:50px">
         
            <asp:Button ID="Button1" runat="server" Text="Button" />
            
            <div id="titulopagina" style="float:left; width: 75%">
                INFORMES POR PERSPECTIVA
            </div>
        </div>
        
        <div>
        
            <div id="menuperspectivas" style="width:20%; float:left" runat="server">
                <div class="degradado">INICIO</div>
            </div>
            
            <%--<div id="cuerpo" style="width:76%; border: 6px inherit #cbccbe; padding-right:20px">--%>
            <div id="cuerpo" runat="server">
                <div id="titulo">
                    INFORME ANUAL POR PERSPECTIVA
                </div>
                
                <div id="nombreperspectiva">
                    PERSPECTIVA X
                </div>
                
                <div id="seleccion">
                    <div style="height:30px">OBJETIVO ESTRATÉGICO</div>
                    <div style="height:30px" id="divobjetivo" runat="server">
                        <asp:Panel ID="panelComboObjetivo" runat="server">                            
                        </asp:Panel>                                        
                    </div>
                    
                    <div style="height:30px">INDICADOR</div>
                    <div style="height:30px">
                        <asp:DropDownList ID="ddlIndicadores" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                
                <div id="display">
                    <div id="grafico">grafico
                        <rsweb:ReportViewer ID="rptGrafico" runat="server">
                        </rsweb:ReportViewer>
                    </div>
                    
                    <div id="reporte">reporte</div>
                </div>
               
                
            
            </div>            
        </div>
    </div>
    </div>
    
    </form>
</body>
</html>
