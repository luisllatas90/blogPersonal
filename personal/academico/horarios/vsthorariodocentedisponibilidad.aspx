<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vsthorariodocentedisponibilidad.aspx.vb" Inherits="academico_horarios_vsthorariodocentedisponibilidad" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
   <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
  <meta http-equiv='X-UA-Compatible' content='IE=8' />
  <meta http-equiv='X-UA-Compatible' content='IE=10' />
  <meta http-equiv='X-UA-Compatible' content='IE=11' />
 <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
<link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1'/>
<link rel='stylesheet' href='../../assets/css/material.css?x=1'/>
<link rel='stylesheet' href='../../assets/css/style.css?y=1'/>
<script type="text/javascript" src='../../assets/js/jquery.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/app.js?x=1'></script>

<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/bootstrap.min.js?x=1'></script>

<script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/wow.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/jquery.accordion.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/materialize.js?x=1'></script>
<script type="text/javascript" src='../../assets/js/funciones.js?a=1'></script>
<script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
<script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
<script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

<script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
<link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>


<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css?z=m">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js?b=m"></script>
<script type="text/javascript" language="JavaScript" src="private/validarhorarios.js"></script>
    <title>Reporte disponibilidad horarios de personal</title>
         <script type="text/javascript">
     var event = jQuery.Event("DefaultPrevented");
     $(document).trigger(event);
     $(document).ready(function() {
         var v = msieversion();

         if (v > 0) {
           //  $('div#TituloForm').html('<h2>Restricciones horario de docente</h2>');
         } else {
             $('div#divFila').remove();
         }
         
         $('#hdtv').val(v);



     });

 
     function msieversion() {

         var ua = window.navigator.userAgent;
         var msie = ua.indexOf("MSIE ");
         var version = 0;
         if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))  // If Internet Explorer, return version number
         {
             //alert(parseInt(ua.substring(msie + 5, ua.indexOf(".", msie))));
             version = parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
         }
         else  // If another browser, return 0
         {
             version = 0;
         }

         return version;
     }

     function fnFoco(input) {
         $('#' + input).focus();
     }
     
     function fnDivLoad(div, time) {

         var $target = $('#' + div);
         $target.mask('<i class="fa fa-refresh fa-spin"></i> Cargando...');
         setTimeout(function() {
             $target.unmask();
         }, time);
     }
     
     function PintarCeldasHorarioDisponibleDocente(ocultar)
	{
		var i=0
		var ArrFilas=document.all.tblHorario.getElementsByTagName('tr')
		var total=0
		for (var f=0;f<ArrFilas.length;f++){
			/*Excluir a cabezeras*/
			if (ArrFilas[f].className==""){
				var ArrCeldas=ArrFilas[f].cells
				i=0
				
				for (var c=0;c<ArrCeldas.length; c++){
					var Celda=ArrCeldas[c]			
					if(Celda.className=="" && Celda.innerText!=""){
						
						//**********************************************************************************************************************
						//dguevara 12.09.2013
						//Este bloque va a sombrear los horarios que tienen (*)
						//Los cuales indican que estan registrados en su horario personal, el (*) es actualizado en el procedimiento.
						var hor=Celda.innerText;
						if(hor.indexOf('*')!=-1){
							Celda.className="CU"
						}else{
						    Celda.className = "DE2";
						}
						//**********************************************************************************************************************
						
						i=i+1
						total=total+1
					}
				}
				/*Oculta filas vacías*/
				
				if (i==0 && ocultar=="S"){
					ArrFilas[f].style.display="none"
				}
				
			}
		}
		if (document.all.totalhrs!=undefined){
			totalhrs.innerHTML=total + " horas ocupadas"
		}
	}
     </script>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .content .main-content
        {
            padding-right: 0px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            font-weight: 300;  line-height: 40px;
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 0px;
        }
        .form-horizontal .control-label
        {
            padding-top: 0px;
        }
    </style>
</head>
<body class="">
<div id="loading" class="piluku-preloader text-center" runat="server">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
<div class="wrapper">
<div class="content">


<div class="main-content">
 <form class="form form-horizontal" id="frmRestriccion"  runat="server">  
 <div  id="report">
 <div id="PanelLista" runat="server"> 
                <div class="row">                   
                    <div class="manage_buttons">
                        <div class="row">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left" id="TituloForm">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Consulta: Disponibilidad de horario docente</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-primary" Text="Consultar"><span class="fa fa-search"></span>&nbsp;Consultar</asp:LinkButton>                                                
                                            </div>
                                        </div>                                        
                                        <div class="row">
                                        <div class="col-md-12 col-sm-12 col-lg-12">
                                            <div class="col-md-6" style="float:left;width:50%;">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label ">
                                                    Semestre acad&eacute;mico</label>
                                                <div class="col-md-8">
                                                    <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="col-md-6" style="float:left;width:50%;">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label ">
                                                    Departamento Acad&eacute;mico
                                                </label>
                                                <div class="col-md-8">
                                                     <asp:DropDownList ID="ddlDepartamento" runat="server"  CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
                                            </div>
                                       </div>                                     
                                        <div class="row">
                                       <div class="col-md-12 col-sm-12 col-lg-12">
                                            <div class="col-md-12" >                                            
                                             <div class="form-group">
                                                <label class="col-md-2 control-label ">
                                                    Docente</label>
                                                <div class="col-md-10">
                                                     <asp:DropDownList ID="ddlDocente" runat="server" AutoPostBack=true  CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
                                            
                                            </div>
                                        </div>                                   
                                  
                                    </div>
                                </div>
                       
                        </div>
                    </div>                   
                </div>
                <div class="row" id="divFila"></div>
                
               
                
                <div class="row">                    
                    <div class="panel-piluku">
                        <div class="panel-heading" style="background-color: #E33439; color:White;">
                            <h3 class="panel-title">
                                Horario de disponibilidad registrada
                            </h3>
                        </div>
                        <div class="panel-body">
                           
                            <div class="row">
                            <div id="tablacalendario" runat="server"></div>
                            </div>
                               
                        </div>
                    </div>                    
                </div>
          </div>
          
 </form>    
 </div>
</div>
</div>
</body>
</html>
