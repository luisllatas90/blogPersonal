<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaTutorado.aspx.vb"
    Inherits="tutoria_FrmListaTutorado" %>

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
    <link rel="stylesheet" href='../assets/css/sweet-alerts/sweetalert.css'/>    

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

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>                                          

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <%-- <script type="text/javascript" src="../assets/js/bootstrap-editable.min.js"></script>--%>

    <script type="text/javascript" src="../assets/js/jquery.listarea.js"></script>

    <%--
    <script type="text/javascript" src="../assets/js/editable-tables.js"></script>--%>

    <script src='../assets/js/bootstrap-datepicker.js'></script>

    <script type="text/javascript" src="../assets/js/sweet-alert/sweetalert.min.js?x=3"></script>

    <script type="text/javascript" src="js/tutoria.js?t=4"></script>

    <script type="text/javascript" src="js/JsTutorado.js?o=e"></script>

    <title></title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
        }
        .content .main-content
        {
            padding-left: 0px;
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
        {
            z-index: 1500;
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
            text-align: right;
        }
        span#select2-cboPersonal-container
        {
            font-weight: normal;
        }
        table.dataTable tbody tr.selected
        {
            background-color: #B0BED9;
            color: #000000;
        }
        body, html
        {
            font-size: 13px;
        }
        .sa-icon.sa-success.animate
        {
            display: none !important;
        }
        .sa-icon.sa-info
        {
            display: none !important;
        }
        input.chkP,input#chkAsistir
        {
            display:none ;    
        }
        #noty_top_layout_container
        {
            z-index: 100 !important;
        }
        #mdRegistro,#mdNueva, .noty_modal
        {
            z-index: 0 !important;
        }
        #tAsistencias tbody th, #tAsistencias tbody td {
            padding: 0px 0px; 
        }
        #mdAsistencias .modal-body {
            font-size :12px; 
        }
        #btnListar {
            margin-top:5px;
        }
        img{
            height:200px;
            width: 187px;
        }
        .liDatos{
            width:93%;
        }
        ul.list-inline.list-unstyled
        {
            margin :0px;
        }
        .liDatos p{
            margin:0px;
            display:inline-block;
            font-weight:bold;
        }
        ul.list-inline.list-unstyled i {
            font-size: 18px;
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
                <div class="row">
                    <div class="manage_buttons" id="divOpc">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-user page_header_icon"></i><span class="main-text">Mis tutorados</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <%--<a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a> <a class="btn btn-green" id="btnAgregar2" href="#" data-toggle="modal"
                                                        data-target="#mdRegistro"><i class="ion-android-add"></i>&nbsp;Agregar</a>--%>
                                            </div>
                                        </div>
                                        <br />
                                    </div>
                                    
                                        <input type="hidden" id="ct" name="ct" runat="server" />
                                </div>
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
                            </div>
                        </div>
                        <div class="row">
                            <form class="form form-horizontal" id="frmCiclo" onsubmit="return false;">
                            <div class="col-md-4">
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
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-sm-12 col-md-4 control-label">
                                        Carrera Prof.</label>
                                    <div class="col-sm-12 col-md-8">
                                        <select name="cboCarreraP" class="form-control" id="cboCarreraP">
                                            <option value="" selected="">-- Seleccione -- </option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a>
                            </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Tutorados<span class="panel-options"><a class="panel-refresh" href="#"> <i
                                    class="icon ti-reload" onclick="fnBuscarTutorados(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <%--<form class="form form-horizontal" id="frmCiclo" onsubmit="return false;"--%>
                            <div class="row">
                                <div class="table-responsive">
                                    <div id="tTutorado_wrapper" class="dataTables_wrapper" role="grid">
                                        <table id="tTutorado" name="tTutorado" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="38%" style="font-weight: bold; width: 70px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Tutor: activate to sort column descending">
                                                        Tutor
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        Código
                                                    </td>
                                                    <td width="38%" style="font-weight: bold; width: 70px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Estudiante
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                        Carrera Prof.
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                        Categoría
                                                    </td>
                                                    <%--<td width="7%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                        Estado
                                                    </td>--%>
                                                    <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Eval. Entrada: activate to sort column ascending">
                                                        Eval. Entrada
                                                    </td>
                                                     <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label=Asistencias: activate to sort column ascending">
                                                        Asistencias
                                                    </td>
                                                     <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label=Notas: activate to sort column ascending">
                                                        Notas
                                                    </td>
                                                     <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label=Sesiones: activate to sort column ascending">
                                                        Sesiones
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                        Opciones
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="11" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbTutorado">
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
                                    <div class="modal-dialog modal-lg" id="modalReg">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" aria-label="Close" style="float: right;" id="btnCloseEval">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="myModalLabel3">
                                                    Registrar Evaluación de Entrada
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row" style="float: right;" id="divEditar">
                                                    <div class="buttons-list">
                                                        <div class="pull-right-btn">
                                                            <button id="btnEditar" class="btn btn-info btn-icon-info btn-icon-block btn-icon-blockleft" onclick="fnEditar()">
                                                                <i class="ion ion-edit"></i><span>Editar</span>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <%--<div class="col-md-12">--%>
                                                    <div class="col-md-4 nopad-right" style="padding-left: 0;">
                                                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                                        onsubmit="return false;" action="#" method="post">
                                                        <%--<div class="row">--%>
                                                        <%--<div class="col-md-12">--%>
                                                        <div class="panel panel-piluku" id="divEvaluar">
                                                            <div class="panel-heading" style="background-color: #FFFFFF;">
                                                                <h3 class="panel-title" style="color: #000000;">
                                                                    Ítems de evaluación
                                                                </h3>
                                                            </div>
                                                            <div class="panel-body">
                                                                <div class="row form-group">
                                                                    <label class="col-md-4 control-label">
                                                                        Evaluación:</label>
                                                                    <div class="col-md-8">
                                                                        <select class="form-control" id="cboEval" name="cboEval">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="row form-group">
                                                                    <label class="col-md-4 control-label">
                                                                        Ítem:</label>
                                                                    <div class="col-md-8">
                                                                        <select class="form-control" id="cboItemEv" name="cboItemEv">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="row form-group">
                                                                    <label class="col-md-4 control-label">
                                                                        Resultado:</label>
                                                                    <div class="col-md-8">
                                                                        <input type="number" min="0" max="100" id="puntaje" />
                                                                        <select class="form-control" id="cboPuntaje" name="cboPuntaje" style="display: none">
                                                                            <option value="" selected="">-- Seleccione -- </option>
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                                <div class="row form-group" id="txtRango">
                                                                    <div class="col-md-4">
                                                                    </div>
                                                                    <label class="col-md-8 control-label" style="text-align :left;color:#E44D4D">*Rango 1-100</label>
                                                                </div> 
                                                                <br />
                                                                <div class="row form-group" style="text-align: center;">
                                                                    <button type="button" class="btn btn-primary" id="btnAgregar" onclick="fnAgregarItem()">
                                                                        <i class="ion ion-plus-circled"></i><span>Agregar</span></button>
                                                                </div>
                                                                <!-- /row -->
                                                            </div>
                                                        </div>
                                                        <div class="panel panel-piluku" id="divTotales">
                                                            <div class="panel-heading" style="background-color: #FFFFFF;">
                                                                <h3 class="panel-title" style="color: #000000;">
                                                                    Información General
                                                                </h3>
                                                            </div>
                                                            <div class="panel-body" id="divRiesgoF">
                                                                <div class="row form-group" style="text-align: center">
                                                                </div>
                                                                <div class="row form-group" style="text-align: center">
                                                                    <%--<button class="btn btn-success" type="button">SocioAfectiva <span class="badge">ALTO</span>--%>
                                                                    </button>
                                                                    <%--<h5>
                                                                                                <span class="col-md-6">Eval. SocioAfectiva</span>
                                                                                                <span class="col-md-4 label" id="Span1">MEDIO</span
                                                                                            </h5>--%>
                                                                </div>
                                                                <!-- /row -->
                                                            </div>
                                                        </div>
                                                        <%--</div>
							                                                       <%-- </div> --%>
                                                        <%--</div>--%>
                                                        </form>
                                                    </div>
                                                    <div class="col-md-8 nopad-right" id="divTablaEvaluar">
                                                        <div class="table-responsive">
                                                            <div id="tEval_wrapper" class="dataTables_wrapper" role="grid">
                                                                <table id="tEval" name="tEval" class="display dataTable" width="100%">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                                N°
                                                                            </td>
                                                                            <td width="20%" style="font-weight: bold; width: 100px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Evaluación: activate to sort column ascending">
                                                                                Evaluación
                                                                            </td>
                                                                            <td width="20%" style="font-weight: bold; width: 100px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Ítem: activate to sort column ascending">
                                                                                Ítem
                                                                                <td width="10%" style="font-weight: bold; width: 70px; text-align: center" class="sorting"
                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Puntaje: activate to sort column ascending">
                                                                                    Puntaje
                                                                                </td>
                                                                                <%--<td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nivel de Riesgo: activate to sort column ascending">
                                                                                    Nivel de Riesgo
                                                                                </td>--%>
                                                                                <%--<td width="7%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                                                    Estado--%>
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 20px; text-align: center" class="sorting"
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
                                                                    <tbody id="tbEval">
                                                                        <%--<tr class="odd">
                                                                                <td valign="top" colspan="7" class="dataTables_empty">
                                                                                    No se ha encontrado informacion
                                                                                </td>
                                                                            </tr>--%>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%-- </div>--%>
                                                </div>
                                            </div>
                                            <div class="modal-footer" id="divFooter">
                                                <center>
                                                    <button type="button" id="btnGuardarEval" class="btn btn-primary" onclick="fnConfirmar(this)">
                                                        Guardar</button>
                                                    <button type="button" class="btn btn-danger" id="btnCancelarReg" onclick="fnCancelar(this);">
                                                        Cancelar</button>
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
            <%-- </form>
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
            <!--                        *** /Tabs Justified Without Border ***-->
            <!-- /panel -->
        </div>
    </div>
    <!-- /panel -->
    <!-- row -->
    <div class="row">
        <div class="modal fade" id="mdNueva" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" id="Div2">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" id="mtRegistro">
                            Nueva Sesión
                        </h4>
                    </div>
                    <div class="modal-body">
                        <form id="frmNueva" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                        onsubmit="return false;" action="#" method="post">
                        <div class="row">
                            <%--<div class="row" style="float:right;" id="divEditar">
                                                                <div class="buttons-list">
                                                                    <div class="pull-right-btn">
                                                                        <button class="btn btn-info btn-icon-info btn-icon-block btn-icon-blockleft" onclick ="fnEditar()">
							                                                <i class="ion ion-edit"></i>
							                                                <span>Editar</span>
						                                                </button>           
						                                            </div>
                                                                </div>
                                                            </div>--%>
                            <div id="pnlNueva">
                                <div class="row" id="">
                                    <div class="row">
                                    <div class="col-md-12" id="div14">
                                        <div class="form-group">
                                            <%--<label class="col-sm-2 control-label"></label>--%>
                                            <%-- <div class="row">--%>
                                            <div class="col-sm-2">
                                                <label class="control-label align-rt">
                                                    Estudiante</label>
                                            </div>
                                            <div class="col-sm-10">
                                                <label class="control-label" id="lblIndividual" >
                                                    Nombre de Estudiante</label>
                                            </div>
                                            <%--   </div>--%>
                                        </div>
                                        <%--<div id="divAsistir">
                                        </div>--%>
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Actividad</label>
                                            </div>
                                            <div class="col-sm-5" style="font-size: smaller;">
                                                <select class="form-control " id="cboActividad" name="cboActividad" >
                                                    <option value="" selected="" style="padding: 0px;"></option>
                                                </select>
                                            </div> 
                                            <%-- <label class="col-sm-2 control-label">
                                                Asistió</label>--%>
                                           <%-- <div class="col-sm-3">
                                               <ul class="list-inline checkboxes-radio">
                                                    <li class="ms-hover">
                                                        <input type="checkbox" id="chkAsistir" class="chkAsistir" dy="1" name="chkAsistir">
                                                        <label for="chkAsistir">
                                                            <span></span></label>
                                                    </li>
                                                </ul> 
                                            </div>   --%>                                        
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Inicidencia</label>
                                            </div>
                                            <div class="col-sm-10" style="font-size: smaller;">
                                                <input id="txtIncidencia" name="txtIncidencia" type="text" class="form-control" placeholder="Descripción">
                                            </div>                                            
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Comentario</label>
                                            </div>
                                            <div class="col-sm-10" style="font-size: smaller;">
                                                 <textarea name="txtComentario" class="form-control text-area" cols="30" rows="10"
                                                                        id="txtComentario" placeholder="Comentario"></textarea>
                                            </div>                                            
                                        </div>
                                        <hr />
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Acción Futura</label>
                                            </div>
                                            <div class="col-md-10" style="font-size: smaller;">
                                                     <textarea name="txtAccion" class="form-control text-area" cols="30" rows="10"
                                                                        id="txtAccion" placeholder="Acciones Futuras"></textarea>
                                            </div> 
                                            
                                        </div> 
                                        <div class="form-group" id="Div1" style="font-size: smaller;">
                                                <div class="col-md-2">
                                                        <label class="control-label align-rt">
                                                            Fecha</label>
                                                </div>
                                                <div class="col-md-4">
                                                   <div class="input-group date">
                                                        <input type="text" class="form-control" id="dtpFechaF" name="dtpFechaF" placeholder="__/__/____"
                                                            data-provide="datepicker">
                                                        <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                        <%--<input name="txtfecnac" class="form-control" id="txtfecnac" style="text-align: right;" type="text" placeholder="__/__/____" data-provide="datepicker">--%>
                                                    </div>
                                                    </div> 
                                                <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Estado</label>
                                            </div>
                                            <div class="col-sm-4" style="font-size: smaller;">
                                                <select class="form-control " id="cboEstado" name="cboEstado" >
                                                    <option value="" selected="" style="padding: 0px;"></option>
                                                </select>
                                            </div> 
                                            </div>
                                            
                                       <%-- <div class="form-group">
                                            
                                        </div>--%>
                                        <hr />
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Problemas</label>
                                            </div>
                                            <div class="col-sm-10" style="font-size: smaller;">
                                                <ul class="list-inline checkboxes-radio" id="lstProblemas">
                                                   <%-- <li class="ms-hover">
                                                        <input type="checkbox" id="chkP1" class="chkP" d="1" name="chkP1">
                                                        <label for="chkP1">
                                                            <span></span>Familiar</label>
                                                    </li>--%>
                                                    
                                                </ul> 
                                            </div> 
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Resultado</label>
                                            </div>
                                            <div class="col-sm-5" style="font-size: smaller;">
                                                <select class="form-control " id="cboResultado" name="cboResultado" >
                                                    <option value="" selected="" style="padding: 0px;"></option>
                                                </select>
                                            </div> 
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Riesgo</label>
                                            </div>
                                            <div class="col-sm-3" style="font-size: smaller;">
                                                <select class="form-control " id="cboRiesgo" name="cboRiesgo" >
                                                    <option value="" selected="" style="padding: 0px;"></option>
                                                </select>
                                            </div> 
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <%--<div class="row">
                                                            <button class="btn btn-green" onclick="fnSiguiente()" style="float: right;">
                                                                <i class="ion ion-checkmark"></i><span>Siguiente</span>
                                                            </button>
                                                        </div>--%>
                            </div>
                            <div class="row" id="divPrimero" style="display: none;">
                                <div class="col-md-12">
                                    <div class="row">
                                        <%--<div class="row">
                                                                    <button class="btn btn-green" onclick="fnRegresar()">
                                                                        <i class=" ion-arrow-left-c"></i><span>Regresar</span>
                                                                    </button>
                                                                </div>--%>
                                        <%--                        <div class="row">
                                                                    <div class="col-md-12">--%>
                                        <div class="form-group">
                                                                    
                                                                    <div class="row">
                                                                        <div class="col-sm-2">
                                                                            <label class="control-label align-rt">
                                                                                Tipo</label>
                                                                        </div>
                                                                        <div class="col-sm-4">
                                                                            <select class="form-control" id="cboTipo" name="cboTipo">
                                                                            </select>
                                                                        </div>
                                                                       
                                                                    </div>
                                                                   
                                                                </div>
                                        <div class="form-group">
                                            <div class="col-md-2">
                                                <label class="control-label align-rt">
                                                    Fecha</label>
                                            </div>
                                            <div class="col-sm-3" id="date-popup-group" style="font-size: smaller;">
                                                <div class="input-group date" style="padding-top: 0px;">
                                                    <input type="text" class="form-control" id="dtpFecha" name="dtpFecha" placeholder="__/__/____"
                                                        data-provide="datepicker" style="font-size: 13px;">
                                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                </div>
                                            </div>
                                            <div class="col-md-7" style="padding-left: 0px;">
                                                <div class="input-group" style="padding-top: 0px;">
                                                    <label class="col-sm-2 control-label" style="width: 15%; padding-left: 0px;">
                                                        Hora</label>
                                                    <select class="form-control " id="cboHoraD" name="cboHoraD" style="width: 18%;">
                                                        <option value="" selected="" style="padding: 0px;">00 </option>
                                                    </select>
                                                    <select class="form-control" id="cboMinutoD" name="cboMinutoD" style="width: 18%;">
                                                        <option value="" selected="">00 </option>
                                                    </select>
                                                    <label class="col-sm-1 control-label" style="width: 13%; text-align: center;">
                                                        a</label>
                                                    <select class="form-control" id="cboHoraA" name="cboHoraA" style="width: 18%;">
                                                        <option value="" selected="" style="padding: 0px;">00 </option>
                                                    </select>
                                                    <select class="form-control" id="cboMinutoA" name="cboMinutoA" style="width: 18%;">
                                                        <option value="" selected="">00 </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group" style ="margin-bottom:0px;" >
                                            <label class="col-sm-2 control-label">
                                                Descripción</label>
                                            <div class="col-sm-10">
                                                <textarea name="lblDescripcion" class="form-control text-area" cols="30" rows="10" maxlength="500"
                                                    id="lblDescripcion" placeholder="Descripción"></textarea>
                                            </div>
                                        </div>
                                        <div style ="margin-top:0px;" class="form-group">
                                             <label style ="float:right;padding-top:0px;" class="col-sm-2 control-label" id="contador">
                                                    </label>
                                         </div> 
                                    </div>
                                   <%-- <div class="row">
                                        <button class="btn btn-green" onclick="fnSiguiente()" style="float: right;">
                                            <i class="ion ion-checkmark"></i><span>Crear Sesión</span>
                                        </button>
                                    </div>--%>
                                </div>
                            </div>
                        </form>
                    </div>
                    <%--</div>--%>
                    <div class="modal-footer" id="div5">
                        <center>
                            <button type="button" id="btnGuardarSesion" class="btn btn-primary" onclick="fnConfirmar(this)">
                                Crear Sesión</button>
                            <button type="button" class="btn btn-danger" id="btnCancelarSesion" onclick="fnCancelar(this);">
                                Cancelar</button>
                        </center>
                    </div>
                    <%--</div>--%>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
         <div class="modal fade" id="mdAsistencias" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog modal-lg" id="Div4">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="titAsistencias">
                                                    Asistencias Moodle
                                                </h4>
                                            </div>
                    <div class="modal-body">
                        <form id="frmAsistencias" name="frmAsistencias" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                       
                         <div class="row">
                                <div class="table-responsive">
                                    <div id="tAsistencias_wrapper" class="dataTables_wrapper" role="grid">
                                        <table id="tAsistencias" name="tAsistencias" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Curso: activate to sort column descending">
                                                        Curso
                                                    </td>
                                                    <td width="30%" style="font-weight: bold; width: 70px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Docente: activate to sort column descending">
                                                        Docente
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 30px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Presente: activate to sort column ascending" id="tdP">
                                                        Presente
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 30px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Falta: activate to sort column ascending" id="tdF">
                                                        Falta
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Total: activate to sort column ascending">
                                                        Total
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Porcentaje: activate to sort column ascending">
                                                        Porcentaje
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Veces Desapr.: activate to sort column ascending">
                                                        Veces Desapr.
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Semáforo: activate to sort column ascending">
                                                        Semáforo
                                                    </td>
                                                    
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="9" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbAsistencias">
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
                        <div class="form-group" style ="color:Black">
                            <%--<div class="alert alert-info alert-dismissable">--%>
									<p style ="margin-bottom :3px"><button type="button" class="btn btn-xs btn-info" style="background-color:#d9534f;width:3%;">R</button> 0% - 50%</p>
									<p style ="margin-bottom :3px"><button type="button" class="btn btn-xs btn-info" style="background-color:#f1e019;width:3%;">A</button> 51% - 74%</p>
									<p style ="margin-bottom :3px"><button type="button" class="btn btn-xs btn-info" style="background-color:#6fd64b;width:3%;">V</button> 75% - 100%</p>
									<p style ="margin-bottom :3px"><button type="button" class="btn btn-xs btn-info" style="width:3%;">N</button> Sin registro</p>
									
						    <%--</div>--%>
                        </div>
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
     <div class="row">
         <div class="modal fade" id="mdDetalle" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog modal-lg" id="Div3">
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
                       
                         <div class="row" id="divTodas">
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
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Hora: activate to sort column ascending" id="td1">
                                                        Hora
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 120px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Descripción: activate to sort column ascending" id="td2">
                                                        Descripción
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="N° de tutorados: activate to sort column ascending">
                                                        Asistencia
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
                          <div class="row">
                            <a href="#"  onclick ="fnVerIndividual()"><label class="control-label" id="lblInd" style="font-size:13px;cursor:pointer;color: #337ab7;">Ver Tutoría Individual</label></a>
                          </div>
                          <div class="row" id="divIndividual" style ="display:none;visibility:hidden ">
                                <div class="table-responsive">
                                    <div id="tIndividual_wrapper" class="dataTables_wrapper" role="grid">
                                        <table id="tIndividual" name="tIndividual" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td> 
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Fecha: activate to sort column descending">
                                                        Fecha
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Actividad: activate to sort column descending">
                                                        Actividad
                                                    </td>
                                                    <td width="38%" style="font-weight: bold; width: 70px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Incidencia: activate to sort column ascending" id="td3">
                                                        Incidencia
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 120px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Comentario: activate to sort column ascending" id="td4">
                                                        Comentario
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Acción Futura: activate to sort column ascending">
                                                        Acción Futura
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                        Estado
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Ejecución: activate to sort column ascending">
                                                        Fecha Ejecución
                                                    </td>  
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Resultado: activate to sort column ascending">
                                                        Resultado
                                                    </td>  
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nivel Riesgo: activate to sort column ascending">
                                                        Nivel Riesgo
                                                    </td>  
                                                    <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Problemas: activate to sort column ascending">
                                                        Problemas
                                                    </td>   
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="11" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbIndividual">
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
     <div class="row">
         <div class="modal fade" id="mdAlumno" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog modal-lg" id="Div7">
                                        <div class="modal-content">
                                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="H2">
                                                    Datos del estudiante 
                                                </h4>
                                            </div>
                    <div class="modal-body">
                        <form id="frmAlumno" name="frmAlumno" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                       
                            <div class="row">
                                <div class="col-md-3">
                                    <%--<label class="control-label align-rt">
                                        Tipo</label>--%>
                                        
                                    <img id="imgFoto" alt="Sin Foto" src="">
                                </div>
                                <div class="col-md-9">                         								
										
                                    <ul class="list-inline list-unstyled">
						                <li><i class="ion-android-bookmark red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Código</p>
						                    <span class="col-md-9" id="liCodigo"></span>
						                </li>
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-android-contact red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Estudiante</p>
						                    <span class="col-md-9" id="liEstudiante"></span>
						                </li>
					                </ul>					                
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-university red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Carrera Prof.</p>
						                    <span class="col-md-9" id="liEscuela" style ="font-size:13px"></span>
						                </li>
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-android-list red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Semestre Ing</p>
						                    <span class="col-md-2" id="liIngreso"></span>
						                    <p class="col-md-3">Modalidad</p>
						                    <span class="col-md-4" id="liModalidad"></span>
						                </li>						              
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-android-list red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Doc. Identidad</p>
						                    <span class="col-md-3" id="liDoc"></span>
						                    <p class="col-md-3">Fecha Nac.</p>
						                    <span class="col-md-3" id="liNac"></span>
						                </li>						              
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-android-list red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Sexo</p>
						                    <span class="col-md-3" id="liSexo"></span>
						                    <p class="col-md-3">Estado Civil</p>
						                    <span class="col-md-3" id="liCivil"></span>
						                </li>						              
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-android-home red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Dirección</p>
						                    <span class="col-md-9" id="liDireccion"></span>
						                </li>						              
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class=" ion-at red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Email</p>
						                    <span class="col-md-9" id="liEmail"></span>
						                </li>						              
					                </ul>
					                <ul class="list-inline list-unstyled">
						                <li><i class="ion-ios-telephone red-info"></i></li>
						                <li class="liDatos">
						                    <p class="col-md-3">Teléfono</p>
						                    <span class="col-md-9" id="liTelf"></span>
						                </li>						              
					                </ul>
					                
                                    <%--<div class="row">
                                        <div class="col-md-6">
                                        </div>
                                    </div>--%>
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
    </div> </div>
    <div class="hiddendiv common">
    </div>

    <link rel="stylesheet" href='../assets/css/profile.css?x=3'/>
    <script src="js/alert.js?x=2" type="text/javascript"></script>

</body>
</html>
