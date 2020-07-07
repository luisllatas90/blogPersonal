<%@ Page Language="VB" AutoEventWireup="false" CodeFile="incidencia.aspx.vb" Inherits="academico_docente_incidencia"  %>
<!DOCTYPE html>
<html lang="en">
   <head runat="server" >
      <meta charset="utf-8" />      
      <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
      <meta http-equiv='X-UA-Compatible' content='IE=8' />
      <meta http-equiv='X-UA-Compatible' content='IE=10' />
      <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/>
      <META HTTP-EQUIV="Pragma" CONTENT="no-cache"/>
      
      <link rel='stylesheet' href='../../assets/css/bootstrap.min.css'/>
      <link rel='stylesheet' href='../../assets/css/material.css'/>
      <link rel='stylesheet' href='../../assets/css/style.css'/>
      
      <script type="text/javascript" src='../../assets/js/jquery.js'></script>
      <script type="text/javascript" src='../../assets/js/app.js'></script>
      <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
      <script type="text/javascript" src='../../assets/js/bootstrap.min.js'></script>
      <script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js'></script>
      <script type="text/javascript" src='../../assets/js/wow.min.js'></script>
      <script type="text/javascript" src="../../assets/js/jquery.nicescroll.min.js"></script>
      <script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js'></script>
      <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>
      <script type="text/javascript" src='../../assets/js/materialize.js'></script>
      <script type="text/javascript" src='../../assets/js/bic_calendar.js'></script>
      <script type="text/javascript" src='../../assets/js/core.js'></script>
      <script type="text/javascript" src='../../assets/js/jquery.countTo.js'></script>
      <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>
      <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>
      <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>
      <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>
      <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>
      <script type="text/javascript" src='../../assets/js/funciones.js'></script>
      <script type="text/javascript" src="../../assets/js/DataJson/jsselect.js?x=10"></script>
      <script type="text/javascript" src='../../assets/js/form-elements.js'></script>
      <script type="text/javascript" src='../../assets/js/select2.js'></script>
      <script type="text/javascript" src='../../assets/js/jquery.multi-select.js'></script>
      <script type="text/javascript" src='../../assets/js/bootstrap-colorpicker.js'></script>
      <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css'/>
      <title></title>
      <script type="text/javascript">

          $(document).ready(function() {
              $("#btnListar").click(fnConsultar);
              $("#cboDacad").change(fnCargaDocente);
              $("#cboDacadR").change(fnCargaDocenteR);
              fnLoading(true);
              fnCargaSelect();
              fnLoading(false);
              $('#tIncidencia').DataTable({
                  "bPaginate": false,
                  "bFilter": true,
                  "bLengthChange": false,
                  "bInfo": false
              });
              ope = fnOperacion(2);
              //console.log(ope);
              $('#btnAgregar').attr("data-toggle", "modal");
              $('#btnAgregar').attr("data-target", "#mdRegistro");
          });

        
          
          function fnCargaDocente() {
			  fnLoadingDiv('divLoading',true);
              var dpto = parseInt($('#cboDacad').val());
              var arr = fnDepAcadDocente(2, 0, dpto);
              
              var n = arr.length;
              var str = "";
              str += '<option value="" selected>-- Seleccione -- </option>';
              for (i = 0; i < n; i++) {
                  str += '<option value="' + arr[i].cPer + '">' + arr[i].nPer + '</option>';
              }
              arr = null;
              $("#cboDocente").html(str);
			  fnLoadingDiv('divLoading',false);
          }
          
          function fnCargaDocenteR() {
              fnLoadingDiv('divLoading', true);
              var dpto = parseInt($('#cboDacadR').val());
              var arr = fnDepAcadDocente(2, 0, dpto);

              var n = arr.length;
              var str = "";
              str += '<option value="" selected>-- Seleccione -- </option>';
              for (i = 0; i < n; i++) {
                  str += '<option value="' + arr[i].cPer + '">' + arr[i].nPer + '</option>';
              }
              arr = null;
              $("#cboDocenteR").html(str);
              fnLoadingDiv('divLoading', false);
          }
          function fnCargaSelect() {
              var arr = fnSemestre(2, 0);
              var n = arr.length;
              var str = "";
              str += '<option value="" selected>-- Seleccione -- </option>';
              for (i = 0; i < n; i++) {
                  str += '<option value="' + arr[i].cCiclo + '">' + arr[i].nCiclo + '</option>';
              }

              $("#cboSemestre").html(str);
              $("#cboSemestreR").html(str);

              arr = fnDepAcad(2, 0);
              var n = arr.length;
              var str = "";
              str += '<option value="" selected>-- Seleccione -- </option>';
              for (i = 0; i < n; i++) {
                  str += '<option value="' + arr[i].cDac + '">' + arr[i].nDac + '</option>';
              }
              $("#cboDacad").html(str);
              $("#cboDacadR").html(str);  
          }

          function fnConsultar(sw=true) {
           
              var cod = $("#hdc").val();
              $("#process").val(ope.lst);
             if (sw){  fnLoading(true);}

               var form = $("#frmbuscar").serializeArray();
               console.log(form);
                  
                  $.ajax({
                      type: "POST",
                      url: "../../DataJson/docente/incidente_docente.aspx",
                      data: form,
                      dataType: "json",
                      cache: false,
                      success: function(data) {
                          console.log(data);
                          var tb = '';
                          var i = 0;
                          var filas = data.length;
                          for (i = 0; i < filas; i++) {
                              if (i == 0 && !data[i].sw) {
                                  fnMensaje('warning', data[i].msje);
                                  $("#" + data[i].obj).focus();
                                  break;
                              }
                              tb += '<tr>';
                              tb += '<td>' + (i + 1) + "" + '</td>';
                              tb += '<td>' + data[i].nCiclo + "" + '</td>';
                              tb += '<td>' + data[i].nDpto + '</td>';
                              tb += '<td>' + data[i].nPer + '</td>';
                              tb += '<td>' + data[i].nAsunto + '</td>';
                              tb += '<td>' + truncate(data[i].nDetalle, 10) + '</td>';
                              tb += '<td>';
                              tb += '<button onclick="fnVer(' + data[i].cCod + ');" class="btn btn-primary"><i  class=" ion-android-download"><span>(' + data[i].nFiles + ')</span></i></button>';
                              tb += '<button onclick="fnEdit(' + data[i].cCod + ');" class="btn btn-orange"><i  class=" ion-edit"><span></span></i></button>';
                              tb += '</td>';
                              tb += '</tr>';
                          }
                          fnDestroyDataTableDetalle('tIncidencia');
                          $('#tbIncidencia').html(tb);
                          fnResetDataTable('tIncidencia');
                          if (sw) {fnLoading(false);}
                      },
                      error: function(result) {
                      }
                  });
            
              $("#process").val("")
          }

          function fnGuardar() {
              console.log(ope);
		      //fnLoading(true);
		      $("#hdo").val(ope.reg);
		      var form = $("#frmRegistro").serializeArray();
		      console.log(form);
		      $.ajax({
		          type: "POST",
		          url: "../../DataJson/docente/incidente_docente.aspx",
		          data: form,
		          dataType: "json",
		          cache: false,
		          success: function(data) {
		              console.log(data);
		              var tb = '';
		              var i = 0;
		              if (data[0].sw) {
		                  SubirArchivo(data[0].cCod, data[0].sNum); 
		                  $('#mdRegistro').modal('hide');
		                  fnMensaje('success', data[0].msje);
		                  fnConsultar();
		                 // $('#mdRegistro').modal('hide');
		              } else {
		                  // fnMensaje('warning', data.msje);
		                  if ($("#" + data[0].obj)) {
		                      $("#" + data[0].obj).focus();
		                      $("#msje").addClass("alert alert-warning");
		                  } else {
		                      $("#msje").addClass("alert alert-danger");
		                  }
		                  $("#msje").html(data[0].msje + ' <i class="ion-alert"></i>');
		              }

		             // $('#mdRegistro').modal('hide');

		              fnLoading(false);
		          },
		          error: function(result) {
		          }
		      });
		  }
            
          function fnVer (c){
            //fnLoading(true);

              $.ajax({
                  type: "POST",
                  url: "../../DataJson/docente/incidente_docente.aspx",
                  data: { "hdo": ope.lstFl, "hdc": c },
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      console.log(data);
                      var tb = '';
                      var i = 0;
                      var filas = data.length;
                      var filas = data.length;
                      for (i = 0; i < filas; i++) {
                          tb += '<tr>';
                          tb += '<td>' + (i + 1) + "" + '</td>';
                          tb += '<td><i  class="' + data[i].nExtension + '"></i> ' + data[i].nArchivo + '</td>'; 
                          tb += '<td>';
                          tb += '<button onclick="fnDownload(' + data[i].cCod + ');" class="btn btn-primary"><i  class=" ion-android-download"><span></span></i></button>';                          
                          tb += '</td>';
                          tb += '</tr>';
                      }
                      if (filas>0) $('#mdFiles').modal('toggle');

                      $('#tbFiles').html(tb);
                      fnLoading(false);
                  },
                  error: function(result) {
                  }
              });
          }
          
		  function fnEdit(c) {
		      
		     
		      fnLoading(true);

		      $.ajax({
		          type: "POST",
		          url: "../../DataJson/docente/incidente_docente.aspx",
		          data: { "hdo": ope.mod, "hdc": c },
		          dataType: "json",
		          cache: false,
		          success: function(data) {
		              console.log(data);
		              var tb = '';
		              var i = 0;
		              var filas = data.length;
		              if (filas == 1) {
		                  $('#cboSemestreR').val(data[0].cCiclo);
		                  $('#cboDacadR').val(data[0].cDpto);
		                  fnCargaDocenteR();
		                  $('#cboDocenteR').val(data[0].cPer);
		                  $('#txtasunto').val(data[0].nAsunto);
		                  $('#txtdetalle').val(data[0].nDetalle);
		                  $('#hdc').val(data[0].cCod);
		                  $('#mdRegistro').modal('toggle');

		              }
		              fnLoading(false);
		          },
		          error: function(result) {
		          }
		      });

		  }

		  function SubirArchivo(c, n) {
		      var flag = false;
		      var form = new FormData();

		      var files = $("#txtfile").get(0).files;
		      console.log(files);
		      // Add the uploaded image content to the form data collection
		      if (files.length > 0) {
		          form.append("hdo", ope.upl);
		          form.append("hdc", c);
		          form.append("hdn", n);
		          form.append("UploadedImage", files[0]);		          
		      }
		     
		      // alert();

		      console.log(form);
		      $.ajax({
		          type: "POST",
		          url: "../../DataJson/docente/incidente_docente.aspx",
		          data: form,
		          dataType: "json",
		          cache: false,
		          contentType: false,
		          processData: false,
		          success: function(data) {
		              flag = true;
		              console.log(data);
		              //		              fnMensaje('warning', 'Subiendo Archivo');
		              //		              $('#divMessage').addClass('alert alert-success alert-dismissable');
		              //		              $fileupload = $('#fileData');
		              //		              $fileupload.replaceWith($fileupload.clone(true));

		          },
		          error: function(result) {
		              console.log(result);
		              //$("#divMessage").html(result);
		              flag = false;
		          }
		      });
		      return flag;
		  }

		  function fnDownload(c) {
		      var flag = false;
		      var form = new FormData(); 
		          form.append("hdo", ope.dwl);
		          form.append("hdc", c);
		      

		      // alert();

		      console.log(form);
		      $.ajax({
		          type: "POST",
		          url: "../../DataJson/docente/incidente_docente.aspx",
		          data: form,
		          dataType: "json",
		          cache: false,
		          contentType: false,
		          processData: false,
		          success: function(data) {
		              flag = true;
		              console.log(data);
		              var file = 'data:application/octet-stream;base64,' + data[0].File;
		              var link = document.createElement("a");
		              link.download = data[0].Nombre;
		              link.href = file;
		              link.click();
		          },
		          error: function(result) {
		              console.log(result);
		              //$("#divMessage").html(result);
		              flag = false;
		          }
		      });
		      return flag;
		  }
      </script>
   </head>
   <body class="" >
      <div class="piluku-preloader text-center">
         <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
         <div class="loader">Loading...</div>
      </div>
      <div class="wrapper">
         <div class="content">
            <div class="overlay"></div>
            <div class="main-content">
               <div class="row">
                  <div class="manage_buttons" id="divOpc">
                     <div class="row">
                        <div id="PanelBusqueda" >
                           <div class="col-md-9 col-sm-12 col-lg-9 search">
                              <div class="page_header">
                                 <div class="pull-left">
                                    <i class="icon ti-layout-media-left page_header_icon"></i>
                                    <span class="main-text">Registro Incidencias de Docentes</span>
                                 </div>
                                 <div class="buttons-list">
                                    <div class="pull-right-btn">
                                       <a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>&nbsp;Listar</a>
                                       <a class="btn btn-green" id="btnAgregar" href="#"><i class="ion-android-add"></i>&nbsp;Agregar</a>
                                    </div>
                                 </div>
                              </div>
                              <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#" method="post">
                                 <input type="hidden" id="process" value="" runat="server" />
                                 <div class="row">
                                    <div class="col-md-3">
                                    <div class="form-group">
                                          <label class="col-sm-12 col-md-4 control-label">Semestre</label>
                                          <div class="col-sm-12 col-md-8" >                                                    
                                             <select name="cboSemestre" class="form-control" id="cboSemestre" ></select>
                                          </div>
                                       </div>
                                      
                                    </div>
                                    <div class="col-md-9">
                                     <div class="form-group">
                                       
                                          <label class="col-sm-12 col-md-3 control-label">Dep. Acad.</label>
                                          <div class="col-sm-12 col-md-9">                                                    
                                             <select name="cboDacad" class="form-control" id="cboDacad" onchange="fnCargaDocente()" ></select>
                                          </div>
                                       </div>
                                      
                                    </div>
                                    </div>
                                    <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                          <label class="col-sm-12 col-md-3 control-label">Docente</label>
                                          <div class="col-sm-12 col-md-9"> 
										  <div style="float:left;" id="divLoading" class="hidden"><img id="imgload" src="../../assets/images/loading.GIF" ></div>
                                          <div class="input-group">										  
							                    <span class="input-group-addon bg">							                    
								                    <i class="ion-university"></i>
							                    </span>                                                   
                                             <select name="cboDocente" class="form-control" id="cboDocente" >
                                             <option value="" selected="">-- Seleccione -- </option>
                                             </select>
                                          </div>
                                          </div>
                                       </div>
                                    </div>
                                 </div>
                              </form>
                           </div>
                           <div class="col-md-3 col-sm-12  col-lg-3" >
                              <div class="buttons-list">
                                 <div class="right pull-right">
                                    <ul class="right_bar">
                                       <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;Consultar</li>
                                       <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;Registrar</li>
                                       <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;Modificar</li>
                                       <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;Eliminar</li>
                                       <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i> &nbsp;Imprimir</li>
                                    </ul>
                                 </div>
                              </div>
                           </div>
                        </div>
                       
                     </div>
                  </div>
               </div>               
               <div class="row">
                 
                     <!-- panel -->
                     <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                           <h3 class="panel-title">
                              <i class="ion-android-list"></i> Listado
                              <span class="panel-options">
                              <a class="panel-refresh" href="#">
                              <i class="icon ti-reload" onclick="fnConsultar(false)"></i> 
                              </a>
                              <a class="panel-minimize" href="#">
                              <i class="icon ti-angle-up"></i> 
                              </a>
                              </span>
                           </h3>
                        </div>
                        <div class="panel-body">
                           <div class="table-responsive">
                              <table id="tIncidencia" class="display dataTable cell-border" width="100%">
							  <thead>
								  <tr>
									<td width="5%" style="font-weight:bold;">N.</td>
									<td width="8%" style="font-weight:bold;">Semestre</td>
									<td width="15%" style="font-weight:bold;">Dep. Acad&eacute;mico</td>
									<td width="24%" style="font-weight:bold;">Docente</td>
									<td width="20%" style="font-weight:bold;">Asunto</td>
									<td width="18%" style="font-weight:bold;">Detalle</td>
									<td width="10%" style="font-weight:bold;">Opciones</td>
								  </tr>							  
							  </thead>
							  <tbody id="tbIncidencia">
							  </tbody>
							  <tfoot>
							  <tr>
							  <th colspan=7></th>
							  </tr>							  
							  </tfoot>
							  </table>
                           </div>
                        </div>
                     </div>
                     <!-- /panel -->         
                 
                 
                <div class="row">
                <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" role="dialog" data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;color:White;font-weight:bold;">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float:right;" ><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title" id="myModalLabel3">Registrar Incidencia</h4>
				</div>
				<div class="modal-body">
				<form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data"  class="form-horizontal"  method="post"  onsubmit="return false;" action="#">
				    <div class="row">
				    <div id="msje"></div>
				    </div>
				    <div class="row">
				     <input type="hidden" id="hdo" value="" runat="server" />
				    <%-- <input type="text" id="hdr" value="" runat="server" />
				     <input type="text" id="hdm" value="" runat="server" />
				     <input type="text" id="hde" value="" runat="server" />--%>
				     <input type="hidden" id="hdc" value="" runat="server" />
                     <div class="form-group">
						<label class="col-sm-3 control-label">Semestre:</label>
						<div class="col-sm-8">
							<select class="form-control" id="cboSemestreR" name="cboSemestreR" >
							</select>
						</div>
					</div>		
					</div>		
					<div class="row">
					<div class="form-group">
						<label class="col-sm-3 control-label">Departamento:</label>
						<div class="col-sm-8">
							<select class="form-control" id="cboDacadR" name="cboDacadR" >
							</select>
						</div>
					</div>	
					</div>
					<div class="row">
					<div class="form-group">
						<label class="col-sm-3 control-label">Docente:</label>
						<div class="col-sm-8">
							<select class="form-control" id="cboDocenteR" name="cboDocenteR" >
							</select>
						</div>
					</div>	
					</div>
					<div class="row">
					<div class="form-group">
						<label class="col-sm-3 control-label">Asunto:</label>
						<div class="col-sm-8">
							<input type="text" id="txtasunto" name="txtasunto" class="form-control" />
						</div>
					</div>	
					</div>
					<div class="row">
					<div class="form-group">
						<label class="col-sm-3 control-label">Detalle:</label>
						<div class="col-sm-8">
							<textarea id="txtdetalle" name="txtdetalle" style="width:100%"></textarea>
						</div>
					</div>	
					</div>
					<div class="row">
					<div class="form-group">
						<label class="col-sm-3 control-label">Adjunto:</label>
						<div class="col-sm-8">
							<input type="file" id="txtfile" name="txtfile" class="form-control" runat="server" />
						</div>
					</div>	
					</div>
				</form> 
				</div>
				<div class="modal-footer">	
				<center>
					<button type="button" id="btnA" class="btn btn-primary"  onclick="fnGuardar();">Guardar</button>					
				    <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal" >Cancelar</button>
				 </center>
				</div>
			</div>
		</div>
	</div>
               </div>
               <!-- row -->
               <div class="row">
                <div class="modal fade" id="mdFiles" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" role="dialog" data-backdrop="static" data-keyboard="false">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header" style="background-color:#E33439;color:White;font-weight:bold;">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float:right;" ><span aria-hidden="true" class="ti-close" style="color:White;"></span></button>
					<h4 class="modal-title" id="tituloFile">Archivos Incidencia</h4>
				</div>
				<div class="modal-body">
				<form id="frmFiles" name="frmFiles" enctype="multipart/form-data"  class="form-horizontal"  method="post"  onsubmit="return false;" action="#">				   
				   	 <div class="row">
				   	 <table style="width:100%;" class="display dataTable" >
				   	 <thead>
				   	 <tr>
				   	 <th style="width:10%"></th>
				   	 <th style="width:80%">Archivo</th>
				   	 <th style="width:10%">Opci&oacute;n</th>
				   	 </tr>
				   	 </thead>
				   	 <tbody id="tbFiles">				   	 
				   	 </tbody>
				   	 <tfoot>
				   	 <tr>
				   	 <th colspan="3"></th>
				   	 </tr>
				   	 </tfoot>
				   	 </table>
				     </div>					
				</form> 
				</div>
				<div class="modal-footer">	
				<center>			
				    <button type="button" class="btn btn-danger" id="Button2" data-dismiss="modal" >Cancelar</button>
				 </center>
				</div>
			</div>
		</div>
	</div>
               </div>
               <!-- row -->
               
               
               
               
               </div>
               <!-- row -->
              
            </div>
         </div>
      </div>
      
   </body>
</html>