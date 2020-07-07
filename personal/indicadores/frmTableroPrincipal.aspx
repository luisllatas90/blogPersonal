<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTableroPrincipal.aspx.vb" Inherits="indicadores_frmTableroPrincipal" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
 <link href="../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />
 <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>     
     
     <style type="text/css">
          
        .borde-redondeado {
	        border-radius:20px;
	        -moz-boder-radius:20px; 
	        -webkit-border-radius:20px;
	        behavior: url(../css/border-radius.htc);	        	        
	        width:1200px;
	        height:830px;
	        padding-top:5px;
	        text-align:center;	        
        }
        
        .borde-redondeado-objetivo {
	        border-radius:20px;
	        -moz-boder-radius:20px; 
	        -webkit-border-radius:20px;
	        behavior: url(../css/border-radius.htc);	        	        
	        width:40px;
	        height:50px;
	        padding-top:5px;
	        padding-left:10px;
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
         A:visited { text-decoration: none; color:#FFFFFF;}
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
        	width: 340px        	 		
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
        	width:300px
        }
                        
    </style>
    
    
    <script language = "javascript" type="text/javascript">
        var ddlIndicadores;
        function CargarCuerpoPerspectiva(codigo_pers, nombre_pers) {

//            $("#divobjetivo div").each(function(index) {
//                $(this).removeClass();
//                $(this).addClass("ocultar");

//                alert($(this).id);


            //            })
            
            //Ocultar todos los divs que esten dentro del panel
            //x = document.getElementById("panelcontenedorperspectivas").getElementsByTagName("div");
            //x.style.visibility = 'visible';

            d = document.getElementById("contenedorperspectivas");
            d.style.display = 'block';

            d = document.getElementById("contenedor");
            d.style.display = 'none';
            
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
            titulopagina.innerHTML = "INFORMES POR PERSPECTIVA";

            ddlIndicadores = document.getElementById("<%=ddlIndicadores.ClientID %>");
            ddlIndicadores.options.length = 0
        }
        //var ddlIndicadores;        
        function GetCountries(sender, e) {
            var num = sender.options[sender.selectedIndex].value;
            //sender.disabled = true;            
            ddlIndicadores = document.getElementById("<%=ddlIndicadores.ClientID %>");
            ddlIndicadores.options.length == 0;
            AddOption("Loading", "0");
            PageMethods.GetCountries(num, OnSuccess, OnFailed, sender);
            
        }
//        window.onload = GetCountries;

        function OnSuccess(response, userContext, methodName) {            
            ddlIndicadores.options.length = 0;
            //AddOption("Please select", "0");
            for (var i in response) {
                AddOption(response[i].Name, response[i].Id);
            }
        }

        function AddOption(text, value) {
            
            var option = document.createElement('<option value="' + value + '">');            
            ddlIndicadores.options.add(option);
            option.innerText = text;            
        }

        function OnFailed(error, userContext, methodName) {
            if (error != null) {
                alert(error.get_message());
            }
            userContext.disabled = false;
        }

        function MostrarContenedorSemaforos() {

            titulopagina.innerHTML = "TABLERO DE CONTROL";           
             
            d = document.getElementById("contenedorperspectivas");
            d.style.display = 'none';

            d = document.getElementById("contenedor");
            d.style.display = 'block';
        }

        function enviarDatosAFrame(frameNombre) {
                       
            var formulario = document.getElementById('form1');
            formulario.target = frameNombre;

            setTimeout('formularioInicializar(\'' + formulario.id + '\');', 2000);
        }

    function formularioInicializar(formularioNombre) {
        var form = document.getElementById(formularioNombre);
        form.target ='';
    }      

       
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">     
     </asp:ScriptManager>
                                    
    <asp:Button ID="boton" runat="server" Text="aqui" />
    <div style="background-color:#255c95; padding-top:20px; padding-left:20px; padding-right:20px; padding-bottom:20px; position:relative">
    <div class="borde-redondeado" style="background-color:#FFFFFF">
        <div style="height:50px">
         
            <div style="float:left; width:20%; padding-left:13px">
                <div style="float:left">
                    <asp:Label ID="Label1" runat="server" Text="Plan estratégico"></asp:Label>
                    <asp:DropDownList ID="ddlPlan" runat="server" Width="140px" AutoPostBack="true">
                    </asp:DropDownList>
                </div>                
            </div>
            
            <div style="float:left; width: 78%">
                <div style="float:left" >
                    <asp:Label ID="Label2" runat="server" Text="Área"></asp:Label> 
                    <asp:DropDownList ID="ddlCeCo" runat="server" Width="400px" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                
                <div style="width:10px; float:left"></div>
                <div style="float:left">
                    <asp:Label ID="Label3" runat="server" Text="Periodo"></asp:Label> 
                    <asp:DropDownList ID="ddlPeriodo" runat="server" Width="110px" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
                
                <div id="titulopagina" style="float:left" runat="server">INFORMES POR PERSPECTIVA</div>                
            </div>
        </div>
        
        <div>
        
            <div id="menuperspectivas" style="width:20%; float:left" runat="server">
                <div class="degradado">TODOS<div style="height:30px">INDICADOR</div>
                    </div>
            </div>
            
            <%--<div id="cuerpo" style="width:76%; border: 6px inherit #cbccbe; padding-right:20px">--%>
            <div id="cuerpo" runat="server">
                <asp:Panel ID="panelCuerpo" runat="server">
                </asp:Panel>
                
             <div id="contenedor" runat="server"></div>
             
             <asp:Panel ID="panelcontenedorperspectivas" runat="server">
                
             <div id="contenedorperspectivas" runat="server" style="display:none"> 
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
                    
                    <div style="height:30px">
                        <asp:DropDownList ID="ddlIndicadores" runat="server" Width="220px">
                        </asp:DropDownList>
                        
                        <%--<asp:TextBox ID="txtNombre" runat="server" ClientIDMode="Static"></asp:TextBox>
                        PostBackUrl="frmReporteIndicador.aspx"
                        
                        --%>                                               
                        
                        <!--
                            '---------------------------------------------------------------------------------------------------------------
                            'Fecha: 29.10.2012
                            'Usuario: dguevara
                            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                            '---------------------------------------------------------------------------------------------------------------
                        -->
                        
                         <asp:Button ID="btnAceptar" runat="server" Text="Mostrar"                         
                            OnClientClick="enviarDatosAFrame('iframePaginaHija');" 
                            PostBackUrl="JavaScript:window.location('//intranet.usat.edu.pe/rptusat/?/PRIVADOS/ACADEMICO/IND_GraficoBarrasUnIndicador')"
                            
                            />
                        
                    </div>
                </div>
                
                <div id="display">
                    <div id="grafico">
                                       
                    <iframe id="iframePaginaHija" name="iframePaginaHija" 
                            width="100%" height="380px">
                    </iframe>

                   <%-- <iframe id="fragrafico" width="100%" border="0" frameborder="0" runat="server">
                    </iframe> src="frmReporteIndicador.aspx"  --%>
	                                                            
                    </div>
                                        
                    <div id="reporte">
                        <iframe id="iframeTabla" name="frmReporteTablaIndicador.aspx"
                        width="100%" border="0" frameborder="0">
	                    </iframe>
	                    
	                    <%-- src="frmReporteTablaIndicador.aspx"  --%>
                    
                    </div>
                </div>
               
              </div>  
              </asp:Panel>
            </div>            
        </div>
    </div>
    </div>         
    
    </form>
</body>
</html>
