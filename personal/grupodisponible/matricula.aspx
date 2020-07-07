<%@ Page Language="VB" AutoEventWireup="false" CodeFile="matricula.aspx.vb" Inherits="matricula" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link rel='stylesheet' href='assets/css/fullcalendar.css'/>
<link rel='stylesheet' href='assets/css/fullcalendar-custom.css'/>
<script type="text/javascript" src='assets/js/jquery.dataTables.min.js'></script>
<link rel='stylesheet' href='assets/css/jquery.dataTables.min.css'/>
<script type="text/javascript"  src='assets/js/moment.min.js'></script>
<script type="text/javascript" src='assets/js/fullcalendar.js?x=1'></script>

<script src='assets/js/noty/jquery.noty.js'></script>
<script src='assets/js/noty/layouts/top.js'></script>
<script src='assets/js/noty/layouts/default.js'></script>

<script src='assets/js/noty/jquery.desknoty.js'></script>
<script src='assets/js/noty/notifications-custom.js?x=1'></script>


 <!--<script type="text/javascript" src='assets/js/fullcalendar-custom.js'></script>--> 
 
<style type="text/css">
td.details-control {
	background: url('assets/img/details_open.png') no-repeat center center;
	width:10%;
	cursor: pointer; 
	font-size:small;
}

/*td.delete {
	background: url('assets/img/details_delete.png') no-repeat center center;
	width:10%;
	cursor: pointer; 
	font-size:small;
}*/
tr.shown td.details-control {
	background: url('assets/img/details_close.png') no-repeat center center;
}

.info {
  border : 1px solid;
  margin: 10px 0px;
  padding:10px 5px 10px 30px;
  background-repeat: no-repeat;
  background-position: 10px center;
  max-width:380px;
  color: #9F6000;
  background-color: #FEEFB3;
}
</style>
<script type="text/javascript">

    var PI = {

        onReady: function() {
            $('#btnInscripcionA').click(fnGeneraInscripcion);
            $('#btnInscripcionC').click(fnCancelarInscripcion);
            $('#btnCartaA').click(fnGenerarC);
            $('#btnCartaC').click(fnCancelarC);
            $('#calendar').on(function(event) { event.preventDefault(); });
            $("#mdForoReg").css({ 'top': _H });
            //fnVerificarDeuda();


            $("#flArchivo").change(function() {
                var maxSize = 500000;
                var val = $(this).val();
                var file = $("#flArchivo")[0].files[0];
                var fileName = file.name;
                var fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1);
                if (!isImage(fileExtension)) {
                    fnMensaje('error', 'Solo se permiten subir im&aacute;genes');
                    $(this).val('');
                    $(this).focus();
                    return false;
                }
                var fileSize = ($(flArchivo)[0].files[0].size);
                if (fileSize > maxSize) {
                    fnMensaje('error', 'Solo se permiten archivos con tamaño maximo de 500Kb. Ingrese otro archivo');
                    $(this).val('');
                    $(this).focus();
                }

            });
            $('.panel-minimize').on('click', function(e) {
                e.preventDefault();
                var $target = $(this).parent().parent().parent().next('.panel-body');
                if ($target.is(':visible')) {
                    $('i', $(this)).removeClass('ti-angle-up').addClass('ti-angle-down');
                } else {
                    $('i', $(this)).removeClass('ti-angle-down').addClass('ti-angle-up');
                }
                $target.slideToggle();
            });

        }
    }

    $(document).ready(PI.onReady);
    function isImage(extension) {
        switch (extension.toLowerCase()) {
            case 'jpg': case 'gif': case 'png': case 'jpeg': case 'xlsx': case 'docx': case 'pdf':
                return true;
                break;
            default:
                return false;
                break;
        }
    }  
    function fnCalendar() {
        var date = new Date();
        var d = date.getDate();
        var m = date.getMonth();
        var y = date.getFullYear();
        $('#calendario').empty();
        var calendar = $('#calendar').fullCalendar({
            height: 900,
            contentHeight: 400,
            aspectRatio: 64,
            theme: true,
            /*header: {
                left: '',
                center: '',
                right: ''
            },*/
            header:false,
            defaultView: 'agendaWeek',
            defaultEventMinutes: 60,
            //selectable: true, //Deshabilita la seleccion en los dias

            selectable: false,
            selectHelper: true,
            disableDragging: true,
            firstDay: 1,
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNameShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
            editable: false, //Deshabilita que se modifique el evento
            allDaySlot: false,
            allDayText: 'Todo el dia',
            axisFormat: 'HH:mm',
            timeFormat: 'HH:mm',
            eventDrop: function(event, delta) {
                return false;
                //alert(event.title + ' was moved ' + delta + ' days\n' +'(should probably update your database)');
            }, eventRender: function(event, element) {
                element.attr('title', event.tip);
            }

        });


    }

    function CalendarAgregaEvento(cup, titulo, inicio, fin, allday, cr) {
        fnCalendar();
        var bg = '#337ab7';
        var filabg = '#2196f3';
        //console.log(cup + ' - ' + titulo + ' - ' + inicio + ' - ' + fin + ' - ' + allday + ' - ' + cr);
        if (cr) {
            bg = '#f00';
            $("#btnRegistrar").removeClass("btn btn-primary");
            $("#btnRegistrar").addClass("btn btn-primary disabled");
            //fnMensaje('error', 'se ha generado cruce de horarios con el curso: ' + titulo)
        } else {
            $("#btnRegistrar").removeClass("btn btn-primary disabled");
            $("#btnRegistrar").addClass("btn btn-primary");

        };

        $('#calendar').fullCalendar('addEventSource', [{ id: cup, title: titulo, start: inicio, end: fin, allDay: allday, color: bg, tip: titulo}]);
        $('#calendar').fullCalendar('refetchEvents');
        $('#calendar').fullCalendar('rerenderEvents');
    }
    function CalendarEliminarEvento() {
        // console.log('eliminar');
        $('#calendar').fullCalendar('removeEvents', function(event) {
            return true;
        });
    }
    function fnGeneraInscripcion() {        
        $('.piluku-preloader').removeClass('hidden');
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processventa.aspx",
            data: { "param0": "gInscVera" },
            dataType: "json",
            success: function(data) {
                console.log(data);
                $('.piluku-preloader').addClass('hidden');

                if (data.sw) {
                    // f_Menu('matricula.aspx');
                    fnCargoConfirmado();

                } else {
                    fnMensaje(data.alert, data.msje);
                }
            },
            error: function(result) {
                console.log('erro');
                console.log(result);
                $('.piluku-preloader').addClass('hidden');
            }
        });
    }

    function fnGenerarC() {
        $('.piluku-preloader').removeClass('hidden');
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "procesar.aspx",
            data: { "param0": "gCC" },
            dataType: "json",
            success: function(data) {
                console.log(data);
                $('.piluku-preloader').addClass('hidden');

                if (data.r) {
                    f_Menu('matricula.aspx');
                }
                fnMensaje(data.alert, data.msj);
            },
            error: function(result) {
                console.log('erro');
                console.log(result);
                $('.piluku-preloader').addClass('hidden');
            }
        });
    }
    
    function fnCancelarInscripcion() {
        $('#divInfo').html('');
    }
    function fnCancelarC() {
        $('#divCarta').html('');
    }
    function fnVerificarDeuda() {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "processventa.aspx",
            data: { "param0": "dv" },
            dataType: "json",
            success: function(data) {
                console.log(data);
                if (data.r) {
                    fnMostrarDeuda();
                }
            },
            error: function(result) {
                sOut = '';
            }
        });
        $('#mdPageDeuda').modal('show');
    }
    function fnMostrarDeuda() {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "detalledeuda.aspx",
            data: { "param1": 5 },
            dataType: "json",
            success: function(data) {
                //console.log(data);
                var sOut = '';
                jQuery.each(data, function(i, val) {
                    sOut += '<tr>';
                    sOut += '<td>' + val.FECHA + ' ' + '</td>';
                    sOut += '<td>' + val.SERVICIO + ' ' + '</td>';
                    sOut += '<td>' + val.estado_deu + ' ' + '</td>';
                    sOut += '<td align="right">' + val.CARGO + ' ' + '</td>';
                    sOut += '<td align="right">' + val.PAGOS + ' ' + '</td>';
                    sOut += '<td align="right">' + val.SALDO + ' ' + '</td>';
                    sOut += '<td align="right">' + val.MORA_DEU + ' ' + '</td>';
                    sOut += '<td align="right">' + val.TOTAL + ' ' + '</td>';
                    sOut += '<td>' + val.OBSERVACION + ' ' + '</td>';
                    sOut += '</tr>';
                });
                $("#tbodyPago").html(sOut);
            },
            error: function(result) {
                sOut = '';
            }
        });
        $('#mdPageDeuda').modal('show');
    }

    function fnCargoConfirmado() { 
       var t = '';
                    t += "<div class='row'>";
                    t += "<div class='col-md-12'>"
                    t += "<div class='panel panel-piluku'>";

                    t += "<div class='panel-heading'>";
                    t += "<h3 class='panel-title'>Inscripci&oacute;n de Cursos de Verano</h3>";
                    t += "</div>";

                    t += "<div class='table-responsive'>";

                    t += "<div class='panel-body'>";

                    t += "<div class='row'>"

                    t += "<div class='form-group'>";

                    t += "<div class='col-sm-12'  style='text-align:justify'>";
                    t += "<center>";
                    t += "<img src='assets/images/AVISO_ACTIVACION_VERANO.png?x=1' style='width:50%;height:50%;' />";
                    t += "</center>";
                    t += "</div>";
                    
                    t += "</div>";

                    t += "</div>";

                    t += "</div>";

                    t += "</div>";

                    t += "</div>";
                    t += "</div>";
                    t += "</div>";
                    $("#divInfo").hide();
                    $('#divInfo').html(t);
                    $("#divInfo").fadeIn(3000);
    }
</script>
<script src='assets/js/datatables/foroalumno.js'></script>
</head>
<body oncontextmenu="return false" onkeydown="return false">

<div id="divCarta" runat="server"></div> 
<div id="divInfo" runat="server"></div>

    <div id="divFrameMat" runat="server">
    <div id="divReglamento">
    <div class="row">
 	    <div class="col-md-12">
				<!-- *** Responsive Tables *** -->
				<!-- panel -->
				<div class='panel panel-piluku'>
					<div class='panel-heading'>
						<h3 class='panel-title'>Reglamento de Pensiones</h3>						
					</div>
					
					<div class='table-responsive'>		
					<div class='panel-body'>
					
					<div class="row">
					        <div class="form-group">
						        <div class="col-sm-12">
							        <center>
							         <img src="assets/images/reglamento2019-0.png?yA=3" style="width:379px;height:101px;" />
							         </center>
                                   <br /><br />
                                    
						        </div>
        					
					        </div>	
					  </div>
					  <div class="row">
					  
                            <div class="form-group">
							         <div class="col-sm-12" >		
							                    <p><center>
										[<input type='checkbox' class='mark-complete' id='toggle-switcha' name='toggle-switcha' checked="checked">
                                        <label for='toggle-switcha'><span></span></label>]<font style="font-weight:bold;color:Black;">He leído la <a href="pdf/CartilladePensiones2019-I.pdf" target="_blank">Cartilla Informativa 2019</a></font>
														</center>	</p>
								                    </div>
								                    
								             
					                </div>   
                         </div><hr />
                         <div class="row">
                            <div class="form-group">
				                    <div class="col-sm-12">
				                    <center>
                                        <a href="#bodyPrincipal" id ="btnacepto" class="btn btn-success">Acepto</a>
                                        <a href="#bodyPrincipal" id ="btnnoacepto" class="btn btn-danger">No Acepto</a> 				                  
                                        </center>
			                    </div>
			            </div>
                     </div>
                     </div>
                     </div>
               </div>
        </div>
       </div>
    </div> 
       
    <div id="divMatricula">
    
          <div class="row">
          <div class="col-md-12">
            <div class="col-md-3 col-sm-6 col-xs-12 nopad-left">
            <div class="dashboard-stats">
                <div class="left">
                    <center>
                    <h6 class="flatBluec counter">Cursos Seleccionados</h6>
                    <h6 id="h6CursosSeleccionados">0</h6>
                    <h6 class="flatBluec counter">Cr&eacute;ditos Seleccionados</h6>
                    <h6 id="h6CreditosSeleccionados">0</h6>
                    </center>
                </div>
                <div class="right flatBlue">
                    <h6>Resumen de Cursos</h6>
                </div>
            </div>
        </div>
            <div class="col-md-3 col-sm-6 col-xs-12 nopad-left">
                    <div class="dashboard-stats">
					    <div class="left">
						    <center>
						    <h6 class="flatBluec counter" id="h6CostoSemestre" runat="server">Costo del Semestre</h6>
                            <h6 id="h6CostoCiclo">0</h6>
                            <h6 id="h6CostoCiclo2">0</h6>
                            <h6 class="flatBluec counter" id="h6Cuotas" runat="server" >Costo Cuota</h6>
                            <h6 id="h6CostoCuota">0</h6>
                            <h6 id="h6CostoCuota2">0</h6>
                            </center>
					    </div>
					    <div class="right flatGreen">
						    <h6>Resumen de Pensiones</h6>
					    </div>
				    </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 nopad-right">
                    <div class="dashboard-stats">
					    <div class="left">
						    <center>
						    <h6 class="flatBluec counter" style="margin-top:5px;">
                                <p style="width:80%; height: 19px;">Situaci&oacute;n actual del alumno:</p></h6>
                            <h6 id="h6SituacionAlumno" runat="server" style="margin-top:0px; padding-top:0px;">Normal</h6>
                            <h6 class="flatBluec counter" id="h6Promes" runat="server" style="margin-top:0px;"> Ciclo de Referencia</h6>
                            <h6 id="h6Prom" runat="server" style="margin-top:0px; font-weight:bold"></h6>
                            </center>
					    </div>
					    <div class="right flatRed">
						    <h6>Situaci&oacute;n Acad&eacute;mica</h6>
					    </div>
				    </div>
            </div>
            <div class="col-md-3 col-sm-6 col-xs-12 nopad-right">
                    <div class="dashboard-stats">
					    <div class="center">	
						   <center>
						   <p style="margin-top:10px;">
						    <button class="btn btn-primary" id="btnGuardar" onclick="fnGuardar()">
							    <i class="ion ion-checkmark"></i>
							    <span id="spGuardar" runat="server">Guardar Matricula</span>
						    </button>
						    </p>
						   <p id="pSolInc" runat="server">
                            
                            </p>
                            </center>                           
                          
					    </div>
					    <%--<div class="right flatOrange">
						    <h6>Matr&iacute;cula</h6>
					    </div>--%>
				    </div>
            </div>
            </div>
            </div>
          <div class="row">
          <div class="col-md-12">
            <div class="col-md-6 nopad-left">
              <!-- panel -->
				<div class="panel panel-piluku">
					<div class="panel-heading">
						<h3 class="panel-title">
							<i class="icon ti-book"></i>&nbsp;Grupos Disponibles							
							<span class="panel-options">
								<a href="#" class="panel-minimize">
									<i class="icon ti-angle-up"></i>
								</a>
							</span>
						</h3>
					</div>
					<div class="panel-body" id="divGrupoDisponible">
					
						<div class="row">
							<div class="col-md-12">
							
							    <div class="table-responsive">
							    <div id="divIdioma" runat="server"></div>
							          <script type="text/javascript" src='assets/js/datatables/grupodisponibles.js?X=2'></script>
							          <form class="form form-horizontal"  id="frmGrupos" method="post" enctype="multipart/form-data">
								        
								           <table class="display dataTable" id="tablagruposdisponibles">
									        <thead id="thGrupoDisponible" style="font-size:small">
										        <tr role="row" id="thtrGrupoDisponible">										            
											        <th style="width:50%">Descripci&oacute;n</th>
											        <th style="width:20%">Cr&eacute;d</th>
											        <th style="width:20%">Ciclo</th>
											        <th style="width:5%;text-align:center;" id="opcion"></th>
										        </tr>
									        </thead>
									        <tbody id="tbCursos" runat="server">
									        </tbody>
									        <tfoot>
									        <tr>
									        <td colspan="5">
									        Condici&oacute;n de Estudiante:
									          <span id="lblsituacion"  class=""></span>
									        </td>
									        </tr>
									        <tr>
									        <td colspan="5">
									        <b>Leyenda:</b> <br />
									        <ul  style="text-align:justify; font-size:xx-small">
									         <li id="liElec" runat="server"></li>
									         <li>Veces desaprobado: <sub style="color:red;font-weight:bold;" title="Numero de veces desaprobado">n</sub></li>
									         <li>Curso electivo: <sub style="color:blue;font-weight:bold;" title="Curso Electivo">E</sub></li>
									         <li><span style="text-decoration: underline;">Promedio ponderado</span> : El aquel obtenido en el &uacute;ltimo semestre acad&eacute;mico regular.</li>
									         <li>Puedes realizar retiro de asignatura hasta un d&iacute;a despu&eacute;s de realizada tu matr&iacute;cula de cursos.</li>
									         <li>
									         <ul>
									         <li>Se considera estudiante regular a aquel que se encuentra matriculado en un mínimo de 11 créditos por semestre.</li>
									         <li>El estudiante que haya desaprobado dos veces una misma asignatura , solo podrá matricularse hasta en 15 créditos, incluyendo la o las asignaturas desaprobadas.</li>
									         <li>Para que una asignatura sea dictada se necesita un número no menor a  quince (15) estudiantes matriculados.</li>
									         </ul>

</li>
									         <div id="divLeyenda" runat="server">
									         <li><p  style="text-align:justify">
									         <u><b>Ciclo de Referencia:</b></u> <b>Es el ciclo en la cual el estudiante debería encontrarse de acuerdo a su plan de estudios </b>y se calcula contando el número de semestres transcurridos desde la primera matrícula regular del estudiante en la escuela actual hasta la última matrícula regular.
                                             </p>
                                             </li>
									            <ul>
									            <li>
									            <p style="text-align:justify">
									            <u><b>Nivelar:</b></u> 
                                                    <span>Sólo en aquellas asignaturas cuyo ciclo es inferior o igual al ciclo de referencia. </span></p>
									            </li>
									            <li id="liAdelantar" runat="server">
									            <p style="text-align:justify">
									            <u><b>Adelantar:</b></u>
                                                    <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,sans-serif;
mso-fareast-font-family:Calibri;mso-fareast-theme-font:minor-latin;mso-bidi-font-family:
&quot;Times New Roman&quot;;mso-ansi-language:ES-PE;mso-fareast-language:EN-US;
mso-bidi-language:AR-SA">Aquellos alumnos invictos en todos los semestres matriculados. &nbsp;(SÓLO 1 asignatura)</span></p>
									            </li>
									            </div>
									            </ul>
									         </ul>
									        </td>
									        </tr>
									        </tfoot>
								        </table>		
								       
								        </form>
			        <!-- /form-->
								</div>
							</div>
						</div>
						<!-- /row -->
			        
					</div>
				</div>
				<!-- /panel -->
             </div>             
 

            <div class="col-md-6 nopad-right">       	    
				<!-- panel -->
				<div class="panel panel-piluku">
					<div class="panel-heading">
						<h3 class="panel-title">
							<i class="ion-calendar"></i>&nbsp;Horario
							<span class="panel-options">
								<a href="#" class="panel-minimize">
									<i class="icon ti-angle-up"></i>
								</a>
							</span>
						</h3>
					</div>
					<div class="panel-body" id="divHorario">						
							<div class="col-md-12">
							
							<div class="table-responsive">
							    <div id='calendar'>
							    
							    </div>
							    <div id='calendario'>
							   <div id="Div1" class="fc fc-ltr ui-widget"><div class="fc-view-container"><div class="fc-view fc-agendaWeek-view fc-agenda-view"><table><thead class="fc-head"><tr><td class="ui-widget-header"><div class="fc-row ui-widget-header"><table><thead><tr><th class="fc-axis ui-widget-header" style="width: 72px;"></th><th class="fc-day-header ui-widget-header fc-mon">Lun </th><th class="fc-day-header ui-widget-header fc-tue">Mar </th><th class="fc-day-header ui-widget-header fc-wed">Mie </th><th class="fc-day-header ui-widget-header fc-thu">Jue </th><th class="fc-day-header ui-widget-header fc-fri">Vie </th><th class="fc-day-header ui-widget-header fc-sat">Sab </th><th class="fc-day-header ui-widget-header fc-sun">Dom </th></tr></thead></table></div></td></tr></thead><tbody class="fc-body"><tr><td class="ui-widget-content"><div class="fc-time-grid-container" style="height: 377px; overflow: hidden;"><div class="fc-time-grid"><div class="fc-bg"><table><tbody><tr><td class="fc-axis ui-widget-content" style="width: 72px;"></td><td class="fc-day ui-widget-content fc-mon fc-past" data-date="2016-01-11"></td><td class="fc-day ui-widget-content fc-tue fc-today ui-state-highlight" data-date="2016-01-12"></td><td class="fc-day ui-widget-content fc-wed fc-future" data-date="2016-01-13"></td><td class="fc-day ui-widget-content fc-thu fc-future" data-date="2016-01-14"></td><td class="fc-day ui-widget-content fc-fri fc-future" data-date="2016-01-15"></td><td class="fc-day ui-widget-content fc-sat fc-future" data-date="2016-01-16"></td><td class="fc-day ui-widget-content fc-sun fc-future" data-date="2016-01-17"></td></tr></tbody></table></div><div class="fc-slats"><table><tbody><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>07:00-08:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>08:00-09:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>09:00-10:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>10:00-11:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>11:00-12:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>12:00-13:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>13:00-14:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>14:00-15:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>15:00-16:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>16:00-17:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>17:00-18:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>18:00-19:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>19:00-20:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>20:00-21:00</span></td><td class="ui-widget-content"></td></tr><tr><td class="fc-axis fc-time ui-widget-content" style="width: 72px;"><span>21:00-22:00</span></td><td class="ui-widget-content"></td></tr></tbody></table></div><hr class="fc-divider ui-widget-header" style="display: block;"><div class="fc-content-skeleton"><table><tbody><tr><td class="fc-axis" style="width:72px"></td><td><div class="fc-event-container"></div></td><td><div class="fc-event-container"></div></td><td><div class="fc-event-container"></div></td><td><div class="fc-event-container"></div></td><td><div class="fc-event-container"></div></td><td><div class="fc-event-container"></div></td><td><div class="fc-event-container"></div></td></tr></tbody></table></div></div></div></td></tr></tbody></table></div></div>
							    
							    </div>
							    </div>
						    </div>
						    
							</div>	
							<div class="row">
							<span class="label label-primary">Horario Disponible</span>
							<span class="label label-danger">Cruce de Horario</span>
							
							</div>					
						<!-- /row -->
					</div>
				</div>
				<!-- /panel -->
            </div>
            
            </div>
            
           </div>                    
            
		 
	      <!-- panel -->
   <!-- /divmatricula -->
   <input type="hidden" id="txtponderado"  runat="server" value=""/>
   <input type="hidden" id="txtnotaMinima"  runat="server" value=""/>
   <input type="hidden" id="txtcredMaxMat"  runat="server" value=""/>
   <input type="hidden" id="txttotalCredAprob"  runat="server" value=""/>
   <input type="hidden" id="txtprecioCredito"  runat="server" value=""/>
   
   <input type="hidden" id="txtveces1"  runat="server" value=""/>
   <input type="hidden" id="txtvecesn"  runat="server" value=""/>
   <input type="hidden" id="txtmaxCred"  runat="server" value=""/>
   <input type="hidden" id="txtmontoCredPen"  runat="server" value=""/>
   <input type="hidden" id="txtcuotas"  runat="server" value=""/>
   <input type="hidden" id="txtmat"  runat="server" value=""/>
   <input type="hidden" id="txtcond"  runat="server" value=""/>
   <input type="hidden" id="txtcpf"  runat="server" value=""/>
   </div>   
    
    <div class="modal fade" id="continuemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
		<div class="modal-dialog" style="padding-top:50px">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true" class="ti-close"></span></button>
					<h4 class="modal-title" id="myModalLabel3">Motivo de Agregado/Retiro</h4>
				</div>
				<div class="modal-body">
				<div class="row">
                    <div class="form-group">
						<label class="col-sm-4 control-label">Seleccione:</label>
						<div class="col-sm-8">
							<select class="form-control" id="cboR" name="cboR" >
							</select>
							<select class="form-control" id="cboA" name="cboA" >
							</select>
						</div>
					</div>		
					</div>		
					<div class="row" id="divmR">
					<div class="form-group">
						<label class="col-sm-4 control-label">Otro Motivo Ret.</label>
						<div class="col-sm-8">
							<textarea id="txtmR" name="txtmR" style="width:100%"></textarea>
						</div>
					</div>	
					</div>
					<div class="row">
					<div class="form-group" id="divmA">
						<label class="col-sm-4 control-label">Otro Motivo Agr.</label>
						<div class="col-sm-8">
							<textarea id="txtmA" name="txtmA" style="width:100%"></textarea>
						</div>
					</div>	
					</div>
				</div>
				<div class="modal-footer">
					<button type="button" id="btnR" class="btn btn-primary" data-dismiss="modal" onclick="fnRet();">Continuar</button>
					<button type="button" id="btnA" class="btn btn-primary" data-dismiss="modal" onclick="fnGuardar();">Continuar</button>					
				    <button type="button" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
				</div>
			</div>
		</div>
	</div>
	</div>
  
    <div class="row">
      <div class="col-md-12">
      <div id="divContentForo"></div>
      </div>
      </div>  
    <div class="modal fade" id="mdForoReg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" runat="server">
	<div class="modal-dialog">
		<div class="modal-content">
		
		<form id="frmForo" name="frmForo" runat="server"> 
			<div class="modal-header">
				<button type="button" class="btn btn-danger" data-dismiss="modal" aria-label="Close" style="float:right"><span aria-hidden="true" class="ti-close"></span></button>
				<h4 class="modal-title" id="H1" style="color:#E33439">Registra </h4>
				<h4 style="color:Gray">Si tienes alg&uacute;n incoveniente con tu matr&iacute;cula.<br />Preg&uacute;ntanos aqu&iacute;</h4>
			</div>
			<div class="modal-body">				    
                <div class="row">	
		        <div class="col-md-12" > 	
		        <div class="row">
					    <div class="form-group col-md-12">
							<label class="col-sm-2 control-label">Asunto:</label>
							<div class="col-md-9">
							<input type="text" id="txtincasunto" name="txtincasunto" value="" style="color:Black;  margin-top:7px;" class="form-control" runat="server"  /> 
							</div>
							
						</div> 
				</div>		
				<div class="row">
					    <div class="form-group col-md-12">
							<label class="col-md-2 control-label">Mensaje:</label>
							<div class="col-md-9">
							<textarea id="txtincmsje" name="txtincmsje" style="color:Black;  margin-top:7px;" class="form-control" runat="server"></textarea>
							</div>
							
						</div> 
				</div>
				<div class="row col-md-12">
					    <div class="form-group col-md-12">															
							<label class="col-md-2 control-label">Archivo</label>
							 <div class="col-md-9">
							     <div class="input-group btn-primary">
							      <span class="input-group-addon bg">
							      <i class="ion-upload"></i>
							      </span>
							      <asp:FileUpload ID="flArchivo" runat="server" class="form-control" maxlength="3" accept="gif|jpg|png" Width="100%" />                                  								      
							     </div>
							     <div>
							      <center><font style="color:Red;">(Tamaño m&aacute;ximo: 500KB)<br />Se permiten archivos: jpg,gif,png,jpeg,xlsx,docx,pdf</font></center>
							     </div>
                                    
                            </div>
						</div> 
				</div>	    	
			   </div>
			</div>	               			
			</div>
			<div class="modal-footer">
                <center>
               <asp:Button ID="btnGinc" runat="server" Text="Guardar"  CssClass="btn btn-primary"/>
				<%--<button type="button" class="btn btn-primary" id="btnGinc"><i class="ion-done"></i>Guardar</button>--%>
			    <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>					
				</center>
			</div>
		 </form>
		</div>
	</div>
</div>
	
	<div class="modal fade modal-full-pad" id="mdPageDeuda" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-full">
        <div class="modal-content">
            <div class="modal-header" style="background-color: #E33439;">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                </button>
                <h4 class="modal-title" style="color: White">
                    Estado de Cuenta - Deudas Vencidas</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">	
                        <font style="color:Red;"><b>(*) Debes estar al d&iacute;a en tus deudas para poder realizar tus tr&aacute;mites</b></font>
                        
                        <table class="display dataTable cell-border" id="tbPagos" >
						            <thead>
							            <tr role="row">
								            <th>Fecha Venc.</th>
								            <th>Concepto</th>
								            <th>Estado</th>
								            <th style="text-align:right;">Cargo (S/.)</th>
								            <th style="text-align:right;">Pago (S/.)</th>
								            <th style="text-align:right;">Saldo (S/.)</th>
								            <th style="text-align:right;">Mora (S/.)</th>
								            <th style="text-align:right;">SubTotal (S/.)</th>
								            <th>Observaci&oacute;n</th>								            
							            </tr>
						            </thead>
						            <tbody id="tbodyPago" runat="server" >
						            </tbody>
						            <tfoot>
						            <tr>
						            <th colspan="9"></th>
						            </tr>
						            </tfoot>
					            </table>	
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <center>
                    <div class="btn-group">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar&nbsp;<i class="ion-arrow-right-a"></i></button>
                    </div>
                </center>
            </div>
        </div>
    </div>
</div>
</body>
</html>
