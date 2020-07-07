<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaTutor.aspx.vb"
    Inherits="Crm_FrmListaTutor" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <script type="text/javascript" src='../assets/js/jquery.js'></script>
    <script type="text/javascript" src='../assets/js/app.js'></script>
    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>
    <script type="text/javascript" src='../assets/js/wow.min.js'></script>
    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>
    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../assets/js/materialize.js'></script>
    <script type="text/javascript" src='../assets/js/bic_calendar.js'></script>
    <script type="text/javascript" src='../assets/js/core.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>
    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js?x=1"></script>
    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js?x=e'></script>
    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />    
    <script type="text/javascript" src='../assets/js/form-elements.js'></script>
    <script type="text/javascript" src='../assets/js/select2.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>
    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>
    <script type="text/javascript" src='../assets/js/bootstrap-datepicker.js'></script>    
    <script type="text/javascript" src="js/tutoria.js?t=7"></script>
    <script type="text/javascript" src="js/JsTutor.js?x=3"></script>

    <title></title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
        }
        .content .main-content
        {
            padding-right: 15px;
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
            height: 30px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .form-horizontal .input-group
        {
            padding-top: 5px;
        }
        .form-control
        {
            height: 32.9px;
        }
        .ui-autocomplete
        { z-index:1500;
        }
        .select2-selection__arrow
        {            
            height: 32px !important;
            border-bottom: 1px solid #e4e4e4;
        }
        .select2-selection__rendered
        {            
            height: 32.9px !important;
        }
        span.select2-selection.select2-selection--single
        {            
            height: 32.9px !important;
        }
       label.col-sm-12.col-md-4.control-label
       {
           text-align :left
       }
       span#select2-cboPersonal-container 
       {
            font-weight: normal;
       }
       table.dataTable tbody tr.selected {
            background-color: #B0BED9;
            color: #000000;
        }
        .control-label {
            font-size: 12px;
        }
        table#tPorAsignar  {
            font-size: 12px;
        }
        table#tAsignados  {
            font-size: 12px;
        }
        .piluku-tabs.nav-justified li a 
        {
            background-color:#e0e5ec;
        }
        #noty_top_layout_container
        {
            z-index: 100 !important;
        }
        #mdRegistro, .noty_modal,#busPoint
        {
            z-index: 0 !important;
        }
        </style>
</head>
<body class="">
    <div class="piluku-preloader text-center hidden">
        <!--<div class="progress">
            <div class="indeterminate"></div>
            </div>-->
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="overlay">
            </div>
            <div class="main-content">
               <%-- <div class="row">
                    <div class="manage_buttons" id="divOpc">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-user page_header_icon"></i><span class="main-text">Tutores</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">--%>
                                                <%--<a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a> <a class="btn btn-green" id="btnAgregar2" href="#" data-toggle="modal"
                                                        data-target="#mdRegistro"><i class="ion-android-add"></i>&nbsp;Agregar</a>--%>
                                        <%--    </div>
                                        </div>
                                        <br />
                                        
                                        
                                    </div>
                                </div>--%>
                                <%--<div class="col-md-3 col-sm-12  col-lg-3">
                                    <div class="buttons-list">
                                        <div class="right pull-right">
                                            <ul class="right_bar">
                                                <a href="javascript:get_loc();">Mostrar localización</a>
                                                 <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Consultar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Registrar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Modificar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Eliminar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Imprimir</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>--%>
                           <%-- </div>
                        </div>
                    </div>
                </div>--%>
                
                <div class="row">
                
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                       <%-- <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Tutores<span class="panel-options"><a class="panel-refresh" href="#">
                                    <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>--%>
                        <div class="panel-body">
                                    <%--<form class="form form-horizontal" id="frmCiclo" onsubmit="return false;"--%>
                                        <div class="row">
                                        <form class="form form-horizontal" id="frmCiclo" onsubmit="return false;">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Semestre Académico</label>
                                                    <div class="col-sm-12 col-md-8">
                                                        <select name="cboCicloAcad" class="form-control" id="cboCicloAcad">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            </form>
                                        </div>
                                  <%-- </form>--%>
				        <!--                        *** Tabs Justified Without Border ***-->
        				
					        <%--<div class="panel-heading">
						        <h3 class="panel-title">
							        Tabs Justified without Border
							        <span class="panel-options">
								        <a href="#" class="panel-refresh">
									        <i class="icon ti-reload"></i>
								        </a>
								        <a href="#" class="panel-minimize">
									        <i class="icon ti-angle-up"></i>
								        </a>
								        <a href="#" class="panel-close">
									        <i class="icon ti-close"></i>
								        </a>
							        </span>
						        </h3>
					        </div>--%>
						        <div role="tabpanel">
							        <!-- Nav tabs -->
							        <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist" id="tabs">
								        <li role="presentation" class=""><a href="#tutores" aria-controls="home" id="tabTutor" role="tab" data-toggle="tab" aria-expanded="false">Tutores</a></li>
								       <%-- <li role="presentation" class=""><a href="#tutorados" aria-controls="profile" id="tabTutorado" role="tab" data-toggle="tab" aria-expanded="false">Tutorados</a></li>--%>
								        <%--<li role="presentation" class="active"><a href="#messagestabnb" aria-controls="messages" role="tab" data-toggle="tab" aria-expanded="true">Messages</a></li>
								        <li role="presentation" class=""><a href="#settingstabnb" aria-controls="settings" role="tab" data-toggle="tab" aria-expanded="false">Settings</a></li>--%>
							        </ul>
							        <!-- Tab panes -->
							        <div class="tab-content piluku-tab-content">
								        <div role="tabpanel" class="tab-pane" id="tutores">
								            <div class="row">
								                <div class="col-md-12 col-sm-12 col-lg-12">
                                                    <form class="form form-horizontal" id="frmBuscarTuto" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                           <%-- <div class="col-md-5">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Ciclo Académico</label>
                                                    <div class="col-sm-12 col-md-8">
                                                        <select name="cboCicloAcad" class="form-control" id="cboCicloAcad">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <div class="col-md-12">     
                                            <div class="input-group">
									            <input type="text" class="form-control" name ="busPoint" id="busPoint" placeholder="Buscar Persona">
									            <span class="input-group-btn">
										            <button class="btn btn-primary btn-md" type="button" id="btnAgregar"  onclick="fnAgregar();">Agregar Tutor</button>
									            </span>
								            </div>
								        </div>
                                        </div>
                                        
                                        </form>
                                                </div>
								            </div>
								            <br>
								            <div class="row">
								                <div class="table-responsive">								            
                                                    <div id="tConvocatoria_wrapper" class="dataTables_wrapper" role="grid">
                                                        <table id="tConvocatoria" name="tConvocatoria" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td width="38%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Nombre
                                                    </td>
                                                   
                                                    <td width="7%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                        Estado
                                                    </td>
                                                     <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                        N° Sesiones
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                        N° Tutorados
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                        Opciones
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="7" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbConvocatoria">
                                                <%--  <tr class="odd">
                                                    <td valign="top" colspan="7" class="dataTables_empty">
                                                        No se ha encontrado informacion
                                                    </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                                    </div>
                                                </div> 
                                            </div> 
                                            <div class="row">
                                                <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                                        aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                        <div class="modal-dialog" id="modalReg">
                                            <div class="modal-content">
                                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                    </button>
                                                    <h4 class="modal-title" id="myModalLabel3">
                                                        Mantenimiento de Tutor</h4>
                                                </div>
                                                <div class="modal-body">
                                                    <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                                    method="post" onsubmit="return false;" action="#">
                                                    
                                                    <%--<div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Ciclo Académico:</label>
                                                            <div class="col-sm-4">
                                                                <select class="form-control" id="cboCicloAcadM" name="cboCicloAcadM">
                                                                    <option value="" selected="">-- Seleccione -- </option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <%--<div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Trabajador:</label>
                                                            <div class="col-sm-8">  
                                                                <input type="text" class="form-control" name ="busPoint1" id="busPoint1" placeholder="Buscar Persona">
									                        </div>
                                                            
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                       <%-- <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Detalle:</label>
                                                            <div class="col-sm-8">
                                                                <textarea id="txtdetalle" name="txtdetalle" rows="5" style="width: 100%"></textarea>
                                                            </div>
                                                        </div>--%>
                                                         <div class="row">
                                                        <!-- xselect form with input group   -->
					                                    <div class="form-group">
						                                    <label class="col-sm-3 control-label">Persona:</label>
						                                    <div class="col-sm-8">
							                                    <div class="input-group">
								                                    <span class="input-group-addon bg">
									                                    <i class=" ion-ios-contact"></i>
								                                    </span>
								                                    <select class="form-control" id ="cboPersonal" name ="cboPersonal">
									                                    <%--<option value="" selected="">-- Seleccione -- </option>--%>
								                                    </select>
							                                    </div>
							                                    <!-- /input-group -->
						                                    </div>
					                                    </div>
					            <!-- xselect form with input group   -->

					                                </div>
                                                    </div>
                                                   <%-- <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Fecha Inicio:</label>
                                                            <div class="col-sm-5" id="date-popup-group">
                                                                <div class="input-group date" id="FechaInicio">
                                                                    <input name="txtfecini" class="form-control" id="txtfecini" style="text-align: right;"
                                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                                    </i></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                   <%-- <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Fecha Fin:</label>
                                                            <div class="col-sm-5" id="date-popup-group">
                                                                <div class="input-group date" id="FechaFin">
                                                                    <input name="txtfecfin" class="form-control" id="txtfecfin" style="text-align: right;"
                                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                                    </i></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row" style="display:none;">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Estado:</label>
                                                            <div class="col-sm-4">
                                                            <input type="checkbox" checked="checked" id="chkEstado" name="chkEstado" style="display:block;" disabled ="disabled"/>
                                                               <%-- <input type="checkbox" id="chkestado" name="chkestado" style="display: block;" checked="checked" />--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--                                        <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Adjunto:</label>
                                                            <div class="col-sm-8">
                                                                <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    </form>
                                                </div>
                                                <div class="modal-footer">
                                                    <center>
                                                        <button type="button" id="btnA" class="btn btn-primary" onclick ="fnGuardar()" >
                                                            Guardar</button>
                                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                            Cancelar</button>
                                                    </center>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                            </div>
								        </div>
								        <div role="tabpanel" class="tab-pane" id="tutorados">
								           <div class="col-md-6 nopad-right">
                                                <!-- panel -->
                                                <div class="panel panel-piluku">
                                                    <div class="panel-heading panelH"  >
                                                        <h3 class="panel-title">
                                                            Por asignar
                                                            <span class="panel-options">
                                                               <a href="#" class="panel-refresh">
                                                                  <i class="icon ti-reload"></i> 
                                                              </a>
                                                             <%-- <a href="#" class="panel-minimize">
                                                                  <i class="icon ti-angle-up"></i> 
                                                              </a>
                                                              <a href="#" class="panel-close">
                                                                  <i class="icon ti-close"></i> 
                                                              </a>--%>
                                                                </span>
                                                        </h3>
                                                    </div>
                                                  <div class="panel-body">
                                                     <div class="row">
                                                        <div class="col-sm-4">
                                                            <label>Asignar por: </label>
                                                        </div>
                                                        <div class="col-sm-8">
							                                <ul class="list-inline checkboxes-radio">
								                                <li class="ms-hover">
									                                <input type="radio" name="active" id="chkFiltros" checked="">
									                                <label for="chkFiltros"><span></span>Filtros</label>
								                                </li>
								                                <li class="ms-hover">
									                                <input type="radio" name="active" id="chkAlumnos" >
									                                <label id="lblchkAlumnos"  for="chkAlumnos" style ="display:none;visibility:hidden "><span></span>Alumnos</label>
								                                </li>
								                                
							                                </ul>
						                                </div>
                                                     </div>
                                                     <div class="row" id="divFiltros">
                                                         <form class="form form-horizontal" id="frmPorAsignar" onsubmit="return false;">
                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-3 control-label" style="padding-top: 5px;padding-left: 0px;">
                                                                        <label>Categoría</label>					                                        
							                                        </div>
                                                                    <div class="col-sm-12 col-md-9" style="padding-right: 0px;">
                                                                            <select class="form-control" id="cboCategoria" name="cboCategoria" tabindex="-1" >
							                                                </select>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-12 col-md-3 control-label" style="padding-top: 5px;">
                                                                            <label>Escuela</label>					                                        
							                                        </div>
                                                                    <div class="col-sm-12 col-md-9" style="padding-right: 0px;">
                                                                            <select class="form-control" id="cboEscuela" name="cboEscuela"  tabindex="-1" >
							                                                </select>
                                                                    </div>
                                                                </div>
                                                                <div class ="row">
                                                                    <div class="col-sm-12 col-md-3 control-label" style="padding-top: 5px;">
                                                                        <label>Semestre Ing.</label>					                                        
							                                        </div>
                                                                    <div class="col-sm-12 col-md-9" style="padding-right: 0px;">
                                                                            <select class="form-control" id="cboIng" name="cboIng"  tabindex="-1" >
							                                                </select>
                                                                    </div>
                                                                </div>
                                                                <div class ="row">
                                                                    <div class="col-sm-12 col-md-3 control-label" style="padding-top: 5px;">
                                                                        <label>Riesgo de Separación</label>					                                        
							                                        </div>
                                                                    <div class="col-sm-12 col-md-9" style="padding-right: 0px;">
                                                                            <select class="form-control" id="cboRiesgo" name="cboRiesgo"  tabindex="-1" >
                                                                                <%--<option value="">--Seleccionar--</option>
                                                                                <option value="">TODOS</option>
                                                                                <option value="1">CON RIESGO DE SEPARACIÓN</option>
                                                                                <option value="0">SIN RIESGO DE SEPARACIÓN</option>--%>
							                                                </select>
                                                                    </div>
                                                                </div>
                                                                <br />
                                                                <div class ="row">
                                                                    <a href="#" onclick="fnBuscarAlumnos()"><label id="lblCantAlumnos" style="float: right;cursor:pointer;"></label></a>
                                                                    <%--<label id="lblCantAlumnos" style="float: right;"></label>--%>
                                                                </div>
                                                                <div class="col-sm-12 col-md-12" style="padding-right: 0px;">
                                                                <button class="btn btn-green btn-block" onclick ="fnAsignarTodos()">Asignar Todos</button>
                                                                      <%--<button type="button" id="btnI" name="btnI" class="btn btn-sm btn-green btn-icon-green" onclick="fnAsignarTodos()" title="Asignar" ><i class="ion-ios-fastforward-outline"></i></button>  --%>
                                                                </div>
                                                          </form>     
							                          </div>
							                          <br />
                                                     <div class="row" id="divAlumnos" style ="display:none ;">
								                <div class="table-responsive">								            
                                                    <div id="tPorAsignar_wrapper" class="dataTables_wrapper" role="grid">
                                                         <form class="form form-horizontal" id="frmtbPorAsignar" onsubmit="return false;">
                                                        <table id="tPorAsignar" name="tPorAsignar" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        Código
                                                                    </td>
                                                                    <td width="38%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Nombre
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                        Escuela
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                                        Categoría
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="7" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbTutorado1">
                                                                <%--  <tr class="odd">
                                                                    <td valign="top" colspan="7" class="dataTables_empty">
                                                                        No se ha encontrado informacion
                                                                    </td>
                                                                </tr>--%>
                                                            </tbody>
                                                        </table>
                                                        </form>
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
                                                    <div class="panel-heading panelH">
                                                        <h3 class="panel-title">
                                                            Asignados
                                                            <span class="panel-options">
                                                               <a href="#" class="panel-refresh">
                                                                  <i class="icon ti-reload"></i> 
                                                              </a>
                                                            <%--  <a href="#" class="panel-minimize">
                                                                  <i class="icon ti-angle-up"></i> 
                                                              </a>
                                                              <a href="#" class="panel-close">
                                                                  <i class="icon ti-close"></i> 
                                                              </a>--%>
                                                                </span>
                                                        </h3>
                                                    </div>
                                                  <div class="panel-body">
                                                  <div class="row" >
                                                    <form class="form form-horizontal" id="frmTutor" name="frmTutor" onsubmit="return false;">
                                                            <div class="col-sm-12 control-label" style="padding-top: 5px;padding-left: 0px;text-align: center">
                                                                    <label id="lblTutor" name="lblTutor"></label>				                                        
							                                </div>		
                                                    </form>					                                        					                                        
							                      </div>
							                          
							                          <br />
                                                     <div class="row">
                                                         <div class="table-responsive">
							                               <table id="tAsignados" name="tAsignados" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        Código
                                                                    </td>
                                                                    <td width="28%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Nombre
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                        Escuela
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                                        Categoría
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Atendido: activate to sort column ascending">
                                                                        Atención
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="8" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbTutorado2">
                                                                <%--  <tr class="odd">
                                                                    <td valign="top" colspan="7" class="dataTables_empty">
                                                                        No se ha encontrado informacion
                                                                    </td>
                                                                </tr>--%>
                                                            </tbody>
                                                        </table>
						                                </div>
						                            </div>
                                                    <!-- /row -->
                                                  </div>
                                                </div>
                                            <!-- /panel -->
                                            </div>
								        </div>
								        <%--<div role="tabpanel" class="tab-pane active" id="messagestabnb">Aliquam erat volutpat. Nullam sed dui nulla. Quisque a cursus lectus, id luctus lorem. Quisque in pharetra sem. Sed placerat nisi sit amet tincidunt fringilla. Cras leo erat, posuere eu nunc sit amet, egestas maximus diam. Cras eget libero lacinia, commodo turpis vel, maximus nunc. Nullam ac metus facilisis, efficitur libero ut, sollicitudin leo. Integer rhoncus scelerisque purus. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Nulla bibendum ex eget lorem porta placerat. Vestibulum eros mauris, congue quis leo sit amet, placerat molestie leo.</div>
								        <div role="tabpanel" class="tab-pane" id="settingstabnb">Integer a lobortis ex. Praesent nibh metus, efficitur in gravida at, feugiat at velit. Vivamus tincidunt nisi eget pulvinar sodales. Praesent dignissim arcu quis velit fermentum dictum. Donec mattis elit sit amet sapien lacinia, at mattis dui pharetra. Mauris ac dolor at metus lobortis tristique ac ut mi. Phasellus vel lacinia ex. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eu pretium turpis. Duis nulla dolor, rhoncus ac sagittis eget, ultricies ac tortor. Sed semper metus ut arcu laoreet feugiat. Nunc ac ante ultricies, ultrices leo quis, pellentesque libero.</div>--%>
							        </div>
						        </div>
				        <!--                        *** /Tabs Justified Without Border ***-->
				        <!-- /panel -->
                            
                        </div>
                    </div>
                    <!-- /panel -->
                    
                    <!-- row -->
                    <div class="row">
                        <div class="modal fade" id="mdEliminar" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="H1">
                                            Eliminar Convocatoria</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmEliminar" name="frmEliminar" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                        <h5 class="text-danger">
                                        ¿Esta Seguro que desea Eliminar Convocatoria?</h4>
                                        </form>
                                    </div>
                                    <div class="modal-footer">
                                        <center>
                                            <button type="button" id="Button1" class="btn btn-primary" onclick="fnEliminar();">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="Button2" data-dismiss="modal">
                                                Cancelar</button>
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
     <div class="row">
         <div class="modal fade" id="mdDetalle" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog modal-lg" id="Div4">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="titDetalle">
                                                    Detalle
                                                </h4>
                                            </div>
                    <div class="modal-body">
                        <form id="frmDetalle" name="frmDetalle" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                       
                         <div class="row">
                                <div class="table-responsive">
                                    <div id="tDetalle_wrapper" class="dataTables_wrapper" role="grid">
                                        <table id="tDetalle" name="tDetalle" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td> 
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Tipo: activate to sort column descending">
                                                        Tipo
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Fecha: activate to sort column descending">
                                                        Fecha
                                                    </td>
                                                    <td width="38%" style="font-weight: bold; width: 70px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Hora: activate to sort column ascending" id="tdP">
                                                        Hora
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 120px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Descripción: activate to sort column ascending" id="tdF">
                                                        Descripción
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="N° de tutorados: activate to sort column ascending">
                                                        Asistentes
                                                    </td>
                                                                                                        
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="6" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbDetalle">
                                                <%--  <tr class="odd">
                                                    <td valign="top" colspan="7" class="dataTables_empty">
                                                        No se ha encontrado informacion
                                                    </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <center>
                           <%-- <button type="button" id="Button3" class="btn btn-primary" onclick="fnEliminar();">
                                Guardar</button>
                            <button type="button" class="btn btn-danger" id="Button4" data-dismiss="modal">
                                Cancelar</button>--%>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>