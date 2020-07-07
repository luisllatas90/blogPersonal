<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerBoletaPago.aspx.vb" Inherits="administrativo_BoletasPago_VerBoletaPago" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="csrf-param" content="_csrf">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
        <meta http-equiv='X-UA-Compatible' content='IE=10' />   
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7, IE=EmulateIE9, IE=EDGE" /> 
        <meta http-equiv='cache-control' content='no-cache'/>
        <meta http-equiv='expires' content='0'/>
        <meta http-equiv='pragma' content='no-cache'/>
        <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
       
        <link rel='stylesheet' href='../../academico/assets/css/bootstrap.min.css'/>
        <link rel='stylesheet' href='../../academico/assets/css/material.css'/>
        <link rel='stylesheet' href='../../academico/assets/css/style.css?x=1'/>
        <link rel='stylesheet' href='../../academico/assets/css/jquery.dataTables.min.css'/>

       
        <script src="../../academico/assets/js/jquery.js" type="text/javascript"></script>
 
 
        <script type="text/javascript" src='../../academico/assets/js/jquery.dataTables.min.js'></script>
        <script src="../../academico/assets/js/bootstrap.min.js" type="text/javascript"></script>

        <script src="../../academico/assets/js/noty/jquery.noty.js" type="text/javascript"></script>
        <script src="../../academico/assets/js/noty/layouts/top.js" type="text/javascript"></script>
        <script src="../../academico/assets/js/noty/layouts/default.js" type="text/javascript"></script>

        <script src="../../academico/assets/js/noty/jquery.desknoty.js" type="text/javascript"></script>
        <script src="../../academico/assets/js/noty/notifications-custom.js" type="text/javascript"></script>

        <script src="../../academico/assets/js/jquery.PrintArea.js" type="text/javascript"></script>

    <script src="../../academico/assets/js/PDFObject/pdfobject.min.js" type="text/javascript"></script>
    
          <script type="text/javascript">
              $(document).ready(function() {

                  //jQuery(window).load(function() {
                      $('.piluku-preloader').addClass('hidden');
                  //});
                  Periodo();
                  Persona();
                  $('#btnConsultar').on('click', function() {
                      ListarBoletas();
                  });
                  $("#Persona").change(function() {
                 // alert(this.value);
                    $("#tbPlanilla").html("");
                  });
              });
              function Periodo() {
                  $.ajax({
                      type: "POST",
                      // contentType: "application/json; charset=utf-8",
                      url: "../../DataJson/Boletas/SelectBoletasPago_ajax.aspx",
                      data: { "Funcion": "Periodo" },
                      dataType: "json",
                      cache: false,
                      success: function(data) {
                      var row = 1;
                         
                          var sOut = '';
                          $("#Periodo").html("");
                          jQuery.each(data, function(i, val) {
                          sOut += ' <option value=' + val.Label + ' >' + val.Label + ' </option>'
                          });

                          $("#Periodo").html(sOut);


                      },
                      error: function(result) {
                   
                      }
                  });
              }
              function Persona() {
                  $.ajax({
                      type: "POST",
                      // contentType: "application/json; charset=utf-8",
                      url: "../../DataJson/Boletas/SelectBoletasPago_ajax.aspx",
                      data: { "Funcion": "DatosPersona" },
                      dataType: "json",
                      cache: false,
                      success: function(data) {
                          var row = 1;
                          
                          var sOut = '';
                          $("#Persona").html("");
                          jQuery.each(data, function(i, val) {
                          sOut += ' <option value="' + val.Value + '">' + val.Label + ' </option>'
                          });

                          $("#Persona").html(sOut);


                      },
                      error: function(result) {
                      console.log('Error Persona');
                          
                      }
                  });
              }
              function ListarBoletas() {
               
                $('.piluku-preloader').removeClass('hidden');
                $.ajax({
                    type: "POST",
                    // contentType: "application/json; charset=utf-8",
                    url: "../../DataJson/Boletas/SelectBoletasPago_ajax.aspx",
                    data: { "Periodo": $("#Periodo").val(), "Funcion": "Planilla", "PersonaId": $("#Persona").val() },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        var row = 1;
                        console.log(data);
                        var sOut = '';
                        $("#tbPlanilla").html("");
                        jQuery.each(data, function(i, val) {
                            var docente = '';

                            var generado = val.BolGenerado;
                            var btn = "";
                            if (generado == "1") {
                                btn = '<a href="#" id=' + val.IdArchivosCompartidos + '   name=' + val.IdArchivosCompartidos + ' onclick="DescargarArchivo(\'' + val.IdArchivosCompartidos + '\');" class="btn btn-primary btn-icon-primary btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#ModalPrint" data-whatever="@getbootstrap"><i  class="ion  ion-ios-printer-outline"></i><span>Descargar Boleta de Remuneraciones Digital</span></a>';
                            } else {
                                if (val.CodigoTplla == 1) {
                                    btn = '<a href="#"  id=' + val.Codigo + ' name=' + val.Codigo + ' onclick="GenerarBoleta(\'' + val.Codigo + '\',this);" class="btn btn-green btn-icon-green btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#exampleModal" data-whatever="@getbootstrap"><i  class="ion ion-checkmark"></i><span>Aceptar Boleta de Remuneraciones Digital &nbsp;&nbsp;&nbsp;</span></a>';
                                } else if (val.CodigoTplla == 5) {
                                    btn = '<a href="#"  id=' + val.Codigo + ' name=' + val.Codigo + ' onclick="GenerarCTS(\'' + val.Codigo + '\',this);" class="btn btn-green btn-icon-green btn-icon-block btn-icon-blockleft" data-toggle="modal" data-target="#exampleModal" data-whatever="@getbootstrap"><i  class="ion ion-checkmark"></i><span>Aceptar Boleta de Remuneraciones Digital &nbsp;&nbsp;&nbsp;</span></a>';
                                }
                            }

                            sOut += '<tr>';
                            sOut += '<td>' + val.Periodo + ' ' + '</td>';
                            sOut += '<td>' + val.Mes + ' ' + '</td>';
                            sOut += '<td>' + val.TipoPlanilla + ' ' + '</td>';
                            sOut += '<td>';
                            sOut += '<div class="btn-group">';
                            sOut += btn;
                            sOut += '</td>';


                            sOut += '</tr>';


                            row++;
                        });

                        $("#tbPlanilla").html(sOut);
                        $('.piluku-preloader').addClass('hidden');


                    },
                    error: function(result) {
                        console.log('ff');

                    }
                });
            }
            function GenerarBoleta(codigo, name) {
                $('.piluku-preloader').removeClass('hidden');
                var flag = false;
              
                $.ajax({
                    type: "POST",
                    url: "../../DataJson/Boletas/SelectBoletasPago_ajax.aspx",
                    data: { "Funcion": "Generar", "Param1": codigo, "PersonaId": $("#Persona").val() },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        console.log(data)
                        if (data.Status == "OK") {                            
                            fnMensaje('success', data.StatusBody.Message);
                        } else {
                            fnMensaje('warning', data.StatusBody.Message);
                        }
                        $('.piluku-preloader').addClass('hidden');
                        ListarBoletas();
 
                    },
                    error: function(result) {
                        console.log(result);
                    }
                });

            }

            function GenerarCTS(codigo, name) {
                $('.piluku-preloader').removeClass('hidden');
                var flag = false;

                $.ajax({
                    type: "POST",
                    url: "../../DataJson/Boletas/SelectBoletasPago_ajax.aspx",
                    data: { "Funcion": "GenerarCTS", "Param1": codigo, "PersonaId": $("#Persona").val() },
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        console.log(data)
                        if (data.Status == "OK") {
                            fnMensaje('success', data.StatusBody.Message);
                        } else {
                            fnMensaje('warning', data.StatusBody.Message);
                        }
                        $('.piluku-preloader').addClass('hidden');
                        ListarBoletas();

                    },
                    error: function(result) {
                        console.log(result);
                    }
                });

            }
            
            function DescargarArchivo(IdArchivo) {
                var flag = false;
             
                $.ajax({
                    type: "POST",
                    url: "../../DataJson/Boletas/SelectBoletasPago_ajax.aspx",
                    data: { "Funcion": "DescargarArchivo", "Param1": IdArchivo },
                    dataType: "json",
                    cache: false,
                   
                    success: function(data) {
                        
                        $('#vista-previa-comprobante').html('');
                       
                        if (data[0].Status == "OK") {
                            var file = 'data:application/pdf;base64,' + data[0].File;

                            if (navigator.userAgent.indexOf("NET") > -1) {
                                  window.open("../../DataJson/Boletas/DescargarArchivo.aspx?Id=" + IdArchivo, 'ta', "");
                                                          } else {
                               
                                $('#vista-previa-comprobante').html('<iframe src=' + file + ' type="application/pdf" width="100%" height="430" frameborder="0"></iframe>');
                               
                                $('#modal-vista-previa').modal('show');
                                $('#btnPdf').hide();
                               
                            }
                        } else {

                        fnMensaje('warning', "Status :AVISO" + "<br/>" + "" + data[0].Message);
                        }

                    },
                    error: function(result) {
                      
                    }
                });

            }
            function downloadWithName(uri, name) {
                var link = document.createElement("a");
                link.download = name;
                link.href = uri;
                link.click();
                
            }
            function changeTemplate(t) {
                var mytemplate = document.getElementById("vista-previa-comprobante");
                mytemplate.innerHTML = '<object type="text/html" data=' + t + '></object>';
            }
            function btnConsultar_onclick() {

            }

          </script>
</head>
<body>
 <div class="piluku-preloader text-center">
  <!-- <div class="progress">
      <div class="indeterminate"></div>
  </div> -->
  <div class="loader">Loading...</div>
</div>
   
        <div class="panel panel-piluku">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Boletas de Pago
                   
                </h3>
             </div>
                 <div class="panel-body">
                     <div class="row">
                        <form  method="post" >
                      
                            <div class="col-md-10 col-sm-10 col-xs-10"  style="float:left">
                                <div class="form-group">
                                    <label class="col-sm-2 col-xs-2 control-label"><b>Periodo :</b></label>
                                    <div class="col-sm-2 col-xs-2"> 
                                        <select class="name_search  form-control valid" name="Periodo" id="Periodo" data-validation="required" data-validation-error-msg="Please make a choice">                                          
                                                          					        
                                        </select>
                                    </div>
                                    <div class="col-sm-6 col-xs-6"> 
                                        <select class="name_search  form-control valid" name="Persona" id="Persona" data-validation="required" data-validation-error-msg="Please make a choice">                                                                                      
                                        </select>
                                    </div>             
                                </div>  
                            </div>
                            <div class="col-md-1 col-sm-2 col-xs-2" >
                                <div class="form-group">                           
                                    <div class="col-sm-3"><button type="button" name="btnConsultar" id="btnConsultar"  Text="Consultar Cursos" class="btn btn-primary" value="Consultar" onclick="return btnConsultar_onclick()">Consultar</button></div>  
                                </div>  
                            </div>
                         </form>
                        </div> 						 
                        <!-- /row -->
                        <div class="row">
                            <div class="col-md-12">
                                <div id="PnlList">
                                    <table class="display dataTable cell-border" id="tablaRendiciones">
                                        <thead>
                                            <tr role="row">		
                                                
                                                <th style="width:8%">Año</th>
                                                <th style="width:8%">Mes</th>
                                                <th style="width:8%">Tipo Planilla</th>                                                 								
                                                <th style="width:30%"></th>
                                                                                                                                      
                                            </tr>
                                        </thead>
                                        <tbody id="tbPlanilla" runat="server">
                                        </tbody>									 
                                    </table>	
                                </div>
                            </div>
                        </div>
                 </div>
        </div>
<div id="modal-vista-previa" class="fade modal" role="dialog" data-backdrop="static" data-keyboard="false">
<div class="modal-dialog modal-lg">
<div class="modal-content">
<div class="modal-header">
<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
<h4><i class="fa fa-file"></i>  Vista previa</h4>
</div>
<div class="modal-body">
	 
		<div id="vista-previa-comprobante">

		</div>
	
		<hr>
		
		<div class="text-right">
			<img class="modal-logo pull-left"  alt="" style="display:none">		
				<a id="btnPdf" class="btn btn-primary" href="#" target="_blank"><i class="fa fa-file-pdf-o"></i>  Descargar en PDF</a>						<div class="clearfix"></div>
		</div>
</div>

</div>
</div>
</div>   
</body>
</html>
