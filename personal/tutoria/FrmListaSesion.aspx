<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaSesion.aspx.vb"
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
    <link rel="stylesheet" href='../assets/css/sweet-alerts/sweetalert.css'>
    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <%--
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />--%>

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

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src="../assets/js/jquery.dataTables1.10.min.js?x=a"></script>

    <%--<script type="text/javascript" src='../assets/js/jquery.dataTables.min.js?x=dd'></script>--%>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.listarea.js"></script>

    <script src='../assets/js/bootstrap-datepicker.js'></script>

    <script type="text/javascript" src="../assets/js/sweet-alert/sweetalert.min.js?x=3"></script>

    <script type="text/javascript" src="js/tutoria.js?f=4"></script>

    <script type="text/javascript" src="js/jsSesion.js?s=12"></script>

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
            padding-top: 8px;
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
            z-index: 10000000000;
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
        .input-group
        {
            width: 100%;
        }
        input[type="checkbox"]
        {
            display: none;
        }
        #tTutorados
        {
            font-size: smaller;
        }
        input#chkSelect, input#chkSelectAll
        {
            display: block;
        }
        #tTutorados
        {
            font-size: 12px;
            table-layout: fixed;
        }
        hr
        {
            margin-top: 0px;
            margin-bottom: 0px;
        }
        #noty_top_layout_container
        {
            z-index: 100 !important;
        }
        #mdRegistro, #mdAsistencia, #mdModificar, #mdIndividual, .noty_modal
        {
            z-index: 0 !important;
        }
        .input-group.date
        {
            padding-top: 0px;
        }
        #chkPall
        {
            display: inline-block;
        }
        #td1
        {
            width: 30px;
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
                                            <i class=" ion-android-calendar page_header_icon"></i><span class="main-text">Mis Sesiones</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" href="#" onclick="fnBuscarSesiones()"><i
                                                    class="ion-android-search"></i>&nbsp;Listar</a> <a class="btn btn-green" id="btnAgregar"
                                                        href="#" onclick="fnModal()"><i class="ion-android-add"></i>&nbsp;Nueva Sesión</a>
                                            </div>
                                        </div>
                                        <input type="hidden" id="ct" name="ct" runat="server" />
                                        <form class="form form-horizontal" id="frmCiclo" onsubmit="return false;">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-12 col-md-3 control-label">
                                                    Semestre Académico</label>
                                                <div class="col-sm-12 col-md-4">
                                                    <select name="cboCicloAcad" class="form-control" id="cboCicloAcad">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                                <%--</div>
                                            <div class="form-group">--%>
                                                <label class="col-sm-12 col-md-1 control-label">
                                                    Tipo
                                                </label>
                                                <div class="col-sm-12 col-md-4">
                                                    <select name="cboTipoS" class="form-control" id="cboTipoS">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-12 col-md-3 control-label">
                                                    Tutor</label>
                                                <div class="col-sm-12 col-md-9">
                                                    <select name="cboTutor" class="form-control" id="cboTutorB">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-12 col-md-3 control-label">
                                                    Tutorado</label>
                                                <div class="col-sm-12 col-md-9">
                                                    <select name="cboAlumno" class="form-control" id="cboAlumno">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-12 col-md-4 control-label">
                                                    Rango de Fecha</label>
                                                <div class="col-sm-12 col-md-4">
                                                    <select name="cboRango" class="form-control" id="cboRango">
                                                        <option value="">-- Seleccione -- </option>
                                                        <option value="">Todos</option>
                                                        <option value="M" selected="">Mensual</option>
                                                        <%--<option value="2">Semanal</option>--%>
                                                        <option value="D">Diario</option>
                                                    </select>
                                                </div>
                                                <div class="col-sm-12 col-md-4" id="divMes" style="display: none">
                                                    <select name="cboMes" class="form-control" id="cboMes">
                                                        <option value="1">Enero</option>
                                                        <option value="2">Febrero</option>
                                                        <option value="3">Marzo</option>
                                                        <option value="4">Abril</option>
                                                        <option value="5">Mayo</option>
                                                        <option value="6">Junio</option>
                                                        <option value="7">Julio</option>
                                                        <option value="8">Agosto</option>
                                                        <option value="9">Setiembre</option>
                                                        <option value="10">Octubre</option>
                                                        <option value="11">Noviembre</option>
                                                        <option value="12">Diciembre</option>
                                                    </select>
                                                </div>
                                                <div class="col-sm-12 col-md-4" id="date-popup-group" name="divDia">
                                                    <div class="input-group date">
                                                        <input type="text" class="form-control" id="dtpDia" name="dtpDia" placeholder="__/__/____"
                                                            data-provide="datepicker">
                                                        <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                        <%--<input name="txtfecnac" class="form-control" id="txtfecnac" style="text-align: right;" type="text" placeholder="__/__/____" data-provide="datepicker">--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-sm-12 col-md-4 control-label">
                                                    Carrera Profesional</label>
                                                <div class="col-sm-12 col-md-8">
                                                    <select name="cboCarreraLista" class="form-control" id="cboCarreraLista">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                        <br />
                                    </div>
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
                    </div>
                </div>
                <div class="row" id="pnlListar">
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Sesiones<span class="panel-options"><a class="panel-refresh" href="#"> <i
                                    class="icon ti-reload" onclick="fnBuscarTutorados(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <%--<form class="form form-horizontal" id="frmCiclo" onsubmit="return false;"--%>
                            <div class="row">
                                <div class="table-responsive">
                                    <div id="tSesiones_wrapper" class="dataTables_wrapper" role="grid">
                                        <table id="tSesiones" name="tSesiones" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="5%" style="font-weight: bold; width: 5%; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 10%; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Fecha: activate to sort column descending">
                                                        Fecha
                                                    </td>
                                                    <td width="18%" style="font-weight: bold; width: 18%; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                        Tipo
                                                    </td>
                                                    <td width="30%" style="font-weight: bold; width: 30%; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Descripción: activate to sort column ascending">
                                                        Descripción
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 10%; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Hora: activate to sort column ascending">
                                                        Hora
                                                    </td>
                                                    <td width="7%" style="font-weight: bold; width: 7%; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Cant. Tutorados: activate to sort column ascending">
                                                        Cant. Tutorados
                                                    </td>
                                                    <td width="10%" style="font-weight: bold; width: 10%; text-align: center" class="sorting"
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
                                            <tbody id="tbSesiones">
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
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                                </button>
                                                <h4 class="modal-title" id="mtRegistro">
                                                    Nueva Sesión
                                                </h4>
                                            </div>
                                            <div class="modal-body">
                                                <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
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
                                                                <div class="col-md-12">
                                                                    <%--                                                                    <div class="row">
                                                                        <button class="btn btn-green" onclick="fnRegresar()">
                                                                            <i class=" ion-arrow-left-c"></i><span>Regresar</span>
                                                                        </button>
                                                                    </div>--%>
                                                                    <div class="form-group" id="divPara">
                                                                        <%-- <div class="col-sm-2" style="padding-left: 0px;">--%>
                                                                        <%--  <ul class="list-inline checkboxes-radio">
                                                                                            <li class="ms-hover">
                                                                                                <input type="radio" name="active" id="chkPara" checked="">--%>
                                                                        <label class="col-sm-2 control-label" id="lblPara">
                                                                            Para</label>
                                                                        <%--   </li>
                                                                                        </ul>--%>
                                                                        <%-- </div>--%>
                                                                        <div class="col-sm-10">
                                                                            <input id="inputPara" type="text" class="form-control" placeholder="Nombre de Alumno">
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group check-radio" id="divTutorados">
                                                                        <%--<label class="col-sm-2 control-label"></label>--%>
                                                                        <%--<div class="row">
                                                                            <div class="col-sm-2" style="padding-left: 0px;">
                                                                                <ul class="list-inline checkboxes-radio">
                                                                                    <li class="ms-hover">
                                                                                        <input type="radio" name="active" id="chkPara" checked="">
                                                                                        <label id="lblPara" for="chkPara">
                                                                                            <span></span>Para</label>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                            <div class="col-sm-10">
                                                                                <input id="inputPara" type="text" class="form-control" placeholder="Nombre de Alumno">
                                                                            </div>
                                                                        </div>--%>
                                                                        <%--<div class="row">
                                                                            <ul class="list-inline checkboxes-radio">
                                                                                <li class="ms-hover">
                                                                                    <input type="radio" name="active" id="chkElegir">
                                                                                    <label for="chkElegir">
                                                                                        <span></span>Elegir tutorados</label>
                                                                                </li>
                                                                            </ul>
                                                                        </div>--%>
                                                                        <div class="alert alert-warning">
                                                                            La selección de los tutorados es por página.
                                                                        </div>
                                                                        <div class="row">
                                                                            <%--<div class="col-md-12">--%>
                                                                            <div class="table-responsive" id="divElegir">
                                                                                <div id="tTutorados_wrapper" class="dataTables_wrapper" role="grid">
                                                                                    <table id="tTutorados" name="tTutorados" class="display dataTable" width="100%">
                                                                                        <thead>
                                                                                            <tr role="row">
                                                                                                <td width="5%" style="font-weight: bold; width: 5%; text-align: center" tabindex="0"
                                                                                                    rowspan="1" colspan="1" aria-label="Código: activate to sort column descending">
                                                                                                    <input type="checkbox" id="chkSelectAll" style="text-align: center">
                                                                                                </td>
                                                                                                <td width="5%" style="font-weight: bold; width: 5%; text-align: center" class="sorting_asc"
                                                                                                    style="width: 20px" tabindex="0" rowspan="1" colspan="1" aria-sort="ascending"
                                                                                                    aria-label="Código: activate to sort column descending">
                                                                                                    Código
                                                                                                </td>
                                                                                                <td width="38%" style="font-weight: bold; width: 38%; text-align: center" class="sorting"
                                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                                                    Nombre
                                                                                                </td>
                                                                                                <td width="10%" style="font-weight: bold; width: 10%; text-align: center" class="sorting"
                                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Escuela: activate to sort column ascending">
                                                                                                    Carrera Prof.
                                                                                                </td>
                                                                                                <td width="10%" style="font-weight: bold; width: 10%; text-align: center" class="sorting"
                                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Categoría: activate to sort column ascending">
                                                                                                    Categoría
                                                                                                </td>
                                                                                                <td width="10%" style="font-weight: bold; width: 10%; text-align: center" class="sorting"
                                                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Riesgo: activate to sort column ascending">
                                                                                                    Riesgo
                                                                                                </td>
                                                                                            </tr>
                                                                                        </thead>
                                                                                        <tfoot>
                                                                                            <tr>
                                                                                                <th colspan="6" rowspan="1">
                                                                                                </th>
                                                                                            </tr>
                                                                                        </tfoot>
                                                                                        <tbody id="tbTutorados">
                                                                                            <%--<tr class="odd">
                                                                                            <td valign="top" colspan="7" class="dataTables_empty">
                                                                                                No se ha encontrado informacion
                                                                                            </td>
                                                                                        </tr>--%>
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                            <%-- </div>--%>
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
                                                                    <%--<label class="col-sm-2 control-label"></label>--%>
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
                                                                    <%--                                                       </div>
                                                                    </div>--%>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div class="col-md-2">
                                                                        <label class="control-label align-rt">
                                                                            Modo</label>
                                                                    </div>
                                                                    <div class="col-md-10" id="Div2">
                                                                        <ul class="list-inline checkboxes-radio">
                                                                            <li class="col-sm-6 ms-hover">
                                                                                <input type="radio" name="active2" id="chkUna" checked="">
                                                                                <label class="label-radio" for="chkUna">
                                                                                    <span></span>Una Sesión</label>
                                                                            </li>
                                                                            <li class="col-sm-6 ms-hover" id="lichkVarias">
                                                                                <input type="radio" name="active2" id="chkVarias">
                                                                                <label class="label-radio" for="chkVarias">
                                                                                    <span></span>Varias Sesiones</label>
                                                                            </li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                                <hr />
                                                                <div class="row" id="divUna">
                                                                    <%--<div class="form-group">--%>
                                                                    <div class="form-group">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label align-rt">
                                                                                Fecha</label>
                                                                        </div>
                                                                        <div class="col-md-10" id="date-popup-group">
                                                                            <div class="input-group date">
                                                                                <input type="text" class="form-control" id="dtpFecha" name="dtpFecha" placeholder="__/__/____"
                                                                                    data-provide="datepicker">
                                                                                <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                                                <%--<input name="txtfecnac" class="form-control" id="txtfecnac" style="text-align: right;" type="text" placeholder="__/__/____" data-provide="datepicker">--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-md-2">
                                                                            <label class="control-label align-rt">
                                                                                Hora</label>
                                                                        </div>
                                                                        <div class="col-md-10" id="Div1">
                                                                            <div class="col-md-1" style="padding: 0px;">
                                                                                <label class="control-label align-rt" style="float: left;">
                                                                                    desde</label>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <div class="input-group">
                                                                                    <select class="form-control" id="cboHoraD" name="cboHoraD" style="width: 48%; margin-right: 3px;">
                                                                                        <option value="" selected="">00 </option>
                                                                                    </select>
                                                                                    <select class="form-control" id="cboMinutoD" name="cboMinutoD" style="width: 48%;">
                                                                                        <option value="" selected="">00 </option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-1" style="padding: 0px;">
                                                                                <label class="control-label align-rt" style="float: left;">
                                                                                    a</label>
                                                                            </div>
                                                                            <div class="col-md-5">
                                                                                <div class="input-group">
                                                                                    <select class="form-control" id="cboHoraA" name="cboHoraA" style="width: 48%; margin-right: 3px;">
                                                                                        <option value="" selected="">00 </option>
                                                                                    </select>
                                                                                    <select class="form-control" id="cboMinutoA" name="cboMinutoA" style="width: 48%;">
                                                                                        <option value="" selected="">00 </option>
                                                                                    </select>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <%-- </div>--%>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="divVarias">
                                                                <div class="col-md-12">
                                                                    <div class="form-group">
                                                                        <div class="form-group">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label align-rt">
                                                                                    Desde</label>
                                                                            </div>
                                                                            <div class="col-md-10" id="date-popup-group">
                                                                                <div class="input-group date ">
                                                                                    <input type="text" class="form-control" data-provide="datepicker" id="dtpDesde" name="dtpDesde"
                                                                                        placeholder="__/__/____">
                                                                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group ">
                                                                            <label class="col-md-2 control-label">
                                                                                Días:</label>
                                                                            <div class="col-md-10">
                                                                                <ul class="list-inline checkboxes-radio">
                                                                                    <li class="ms-hover">
                                                                                        <input type="checkbox" id="c1a" class="chkDia" dy="1" name="chkDia">
                                                                                        <label for="c1a">
                                                                                            <span></span>Lunes</label>
                                                                                    </li>
                                                                                    <li class="ms-hover">
                                                                                        <input type="checkbox" id="c2a" class="chkDia" dy="2" name="chkDia">
                                                                                        <label for="c2a">
                                                                                            <span></span>Martes</label>
                                                                                    </li>
                                                                                    <li class="ms-hover">
                                                                                        <input type="checkbox" id="c3a" class="chkDia" dy="3" name="chkDia">
                                                                                        <label for="c3a">
                                                                                            <span></span>Miércoles</label>
                                                                                    </li>
                                                                                    <li class="ms-hover">
                                                                                        <input type="checkbox" id="c4a" class="chkDia" dy="4" name="chkDia">
                                                                                        <label for="c4a">
                                                                                            <span></span>Jueves</label>
                                                                                    </li>
                                                                                    <li class="ms-hover">
                                                                                        <input type="checkbox" id="c5a" class="chkDia" dy="5" name="chkDia">
                                                                                        <label for="c5a">
                                                                                            <span></span>Viernes</label>
                                                                                    </li>
                                                                                    <li class="ms-hover">
                                                                                        <input type="checkbox" id="c6a" class="chkDia" dy="6" name="chkDia">
                                                                                        <label for="c6a">
                                                                                            <span></span>Sábado</label>
                                                                                    </li>
                                                                                </ul>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label align-rt">
                                                                                    Hora</label>
                                                                            </div>
                                                                            <div class="col-md-10" id="Div7">
                                                                                <div class="col-md-1" style="padding: 0px;">
                                                                                    <label class="control-label align-rt" style="float: left;">
                                                                                        desde</label>
                                                                                </div>
                                                                                <div class="col-md-5">
                                                                                    <div class="input-group">
                                                                                        <select class="form-control" id="cboHoraDV" name="cboHoraDV" style="width: 48%; margin-right: 3px;">
                                                                                            <option value="" selected="">00 </option>
                                                                                        </select>
                                                                                        <select class="form-control" id="cboMinutoDV" name="cboMinutoDV" style="width: 48%;">
                                                                                            <option value="" selected="">00 </option>
                                                                                        </select>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-1" style="padding: 0px;">
                                                                                    <label class="control-label align-rt" style="float: left;">
                                                                                        a</label>
                                                                                </div>
                                                                                <div class="col-md-5">
                                                                                    <div class="input-group">
                                                                                        <select class="form-control" id="cboHoraAV" name="cboHoraAV" style="width: 48%; margin-right: 3px;">
                                                                                            <option value="" selected="">00 </option>
                                                                                        </select>
                                                                                        <select class="form-control" id="cboMinutoAV" name="cboMinutoAV" style="width: 48%;">
                                                                                            <option value="" selected="">00 </option>
                                                                                        </select>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label align-rt">
                                                                                    Cada</label>
                                                                            </div>
                                                                            <div class="col-md-10" id="Div6">
                                                                                <select class="col-md-6 form-control" id="cboSemana" name="cboSemana" style="width: 20%;
                                                                                    /* margin-right: 3px; */">
                                                                                    <option value="" selected="">1 </option>
                                                                                </select>
                                                                                <label class="col-md-2 control-label align-rt" style="float: left;">
                                                                                    semana(s)</label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                            <div class="col-md-2">
                                                                                <label class="control-label align-rt">
                                                                                    Hasta</label>
                                                                            </div>
                                                                            <div class="col-md-10" id="date-popup-group">
                                                                                <div class="input-group date">
                                                                                    <input type="text" class="form-control" data-provide="datepicker" id="dtpHasta" name="dtpHasta"
                                                                                        placeholder="__/__/____">
                                                                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="margin-bottom: 0px">
                                                                <label class="col-sm-2 control-label">
                                                                    Descripción</label>
                                                                <div class="col-sm-10">
                                                                    <textarea name="lblDescripcion" class="form-control text-area" cols="30" rows="10"
                                                                        maxlength="500" id="lblDescripcion" placeholder="Descripción"></textarea>
                                                                </div>
                                                            </div>
                                                            <div style="margin-top: 0px;" class="form-group">
                                                                <label style="float: right; padding-top: 0px;" class="col-sm-2 control-label" id="contador">
                                                                    0/500</label>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <div class="col-sm-2" style="display: none;">
                                                                        <label class="control-label align-rt">
                                                                            Tutor</label>
                                                                    </div>
                                                                    <div class="col-sm-5" style="display: none;">
                                                                        <select class="form-control" id="cboTutor" name="cboTutor">
                                                                        </select>
                                                                    </div>
                                                                    <div class="col-sm-1" style="display: none">
                                                                        <label class="control-label align-rt">
                                                                            Categoría</label>
                                                                    </div>
                                                                    <div class="col-sm-4" style="display: none">
                                                                        <select class="form-control" id="cboCategoria" name="cboCategoria">
                                                                        </select>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <div class="col-sm-2">
                                                                    <label class="control-label align-rt">
                                                                        Carrera Prof.</label>
                                                                </div>
                                                                <div class="col-sm-10">
                                                                    <select class="form-control" id="cboCarrera" name="cboCarrera">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" style="display: none">
                                                                <div class="col-sm-2">
                                                                    <label class="control-label align-rt">
                                                                        Semestre Ing.</label>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <select class="form-control" id="cboIng" name="cboIng">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="divCurso">
                                                                <div class="col-sm-2">
                                                                    <label class="control-label align-rt">
                                                                        Curso</label>
                                                                </div>
                                                                <div class="col-sm-10">
                                                                    <select class="form-control" id="cboCurso" name="cboCurso">
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <button class="btn btn-green" onclick="fnSiguiente()" style="float: right;">
                                                                <i class="ion ion-checkmark"></i><span>Crear Sesión</span>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                                </form>
                                            </div>
                                            <%--</div>--%>
                                            <div class="modal-footer" id="divFooter">
                                                <center>
                                                    <button type="button" id="btnGuardarSesion" class="btn btn-primary" onclick="fnConfirmar(this)">
                                                        Agregar a Sesión</button>
                                                    <button type="button" class="btn btn-danger" id="btnCancelarSesion" onclick="fnCancelar(this);">
                                                        Cancelar</button>
                                                </center>
                                            </div>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--  </div>
                    </div>--%>
        <!-- /panel -->
        <!-- row -->
        <div class="row">
            <div class="modal fade" id="mdAsistencia" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog" id="Div4">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                            </button>
                            <h4 class="modal-title" id="H2">
                                Registrar Asistencia
                            </h4>
                        </div>
                        <div class="modal-body">
                            <form id="frmAsistencia" name="frmAsistencia" enctype="multipart/form-data" class="form-horizontal"
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
                                <div id="div5">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <%--<label class="col-sm-2 control-label"></label>--%>
                                                <div class="row">
                                                    <div class="col-sm-2" style="padding-left: 0px;">
                                                        <label class="control-label" style="font-weight: bold">
                                                            Fecha</label>
                                                    </div>
                                                    <div class="col-sm-2" style="padding-left: 0px;">
                                                        <label class="control-label" id="lblFecha_A">
                                                            Fecha</label>
                                                    </div>
                                                    <div class="col-sm-2" style="padding-left: 0px;">
                                                        <label class="control-label" style="font-weight: bold">
                                                            Carrera Prof.</label>
                                                    </div>
                                                    <div class="col-sm-6" style="padding-left: 0px;">
                                                        <label class="control-label" id="lblCarrera_A">
                                                            Fecha</label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-2" style="padding-left: 0px;">
                                                        <label class="control-label" style="font-weight: bold">
                                                            Descripción</label>
                                                    </div>
                                                    <div class="col-sm-10" style="padding-left: 0px;">
                                                        <label class="control-label" id="lblDescripcion_A">
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="div8">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group check-radio">
                                                    <%--<label class="col-sm-2 control-label"></label>--%>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <%--<div class="col-md-12">--%>
                                            <div class="table-responsive" id="div9">
                                                <div id="tTutoradosA_wrapper" class="dataTables_wrapper" role="grid">
                                                    <table id="tTutoradosA" name="tTutoradosA" class="display dataTable" width="100%">
                                                        <thead>
                                                            <tr role="row">
                                                                <td width="5%" style="font-weight: bold; width: 5%; text-align: center" class="sorting"
                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="N°: activate to sort column ascending">
                                                                    N°
                                                                </td>
                                                                <td width="38%" style="font-weight: bold; width: 38%; text-align: center" class="sorting"
                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Alumno: activate to sort column ascending">
                                                                    Estudiante
                                                                </td>
                                                                <td width="5%" style="font-weight: bold; width: 5%;" tabindex="0" rowspan="1" colspan="1"
                                                                    aria-label="Asistencia: activate to sort column descending">
                                                                    <input type="checkbox" id="chkPall">
                                                                    <%--                                          <input type="checkbox" id="chkSelectAll" class="chkSelectAll" name="chkSelectAll" display="none">
									                                                            <label for="chkSelectAll"><span>sd</span></label>--%>
                                                                    <%-- <input type="checkbox" id="chkPall" name="chkPall">
									                                                                <label name="chkPall" for="chkPall"><span></span>Lunes</label>--%>
                                                                    Asistencia
                                                                </td>
                                                            </tr>
                                                        </thead>
                                                        <tfoot>
                                                            <tr>
                                                                <th colspan="3" rowspan="1">
                                                                </th>
                                                            </tr>
                                                        </tfoot>
                                                        <tbody id="tbTutoradosA">
                                                            <%--<tr class="odd">
                                                                                            <td valign="top" colspan="7" class="dataTables_empty">
                                                                                                No se ha encontrado informacion
                                                                                            </td>
                                                                                        </tr>--%>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <%-- </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" id="btnGuardarAsistencia" class="btn btn-primary" onclick="fnConfirmar(this)">
                                Guardar</button>
                            <button type="button" class="btn btn-danger" id="btnCancelarAs" onclick="fnCancelar(this);">
                                Cancelar</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="modal fade" id="mdModificar" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog" id="Div10">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                            </button>
                            <h4 class="modal-title" id="H1">
                                Editar Sesión
                            </h4>
                        </div>
                        <div class="modal-body">
                            <form id="frmModificar" name="frmModificar" enctype="multipart/form-data" class="form-horizontal"
                            onsubmit="return false;" action="#" method="post">
                            <div class="row">
                                <div id="div11">
                                    <div class="row">
                                        <div class="col-md-12" id="divInfo">
                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <label class="control-label align-rt">
                                                        Tipo</label>
                                                </div>
                                                <div class="col-sm-10">
                                                    <select class="form-control" id="cboTipoM" name="cboTipo" disabled="disabled">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group" style="margin-bottom: 0px;">
                                                <label class="col-sm-2 control-label">
                                                    Descripción</label>
                                                <div class="col-sm-10">
                                                    <textarea name="lblDescripcion" class="form-control text-area" cols="30" rows="3"
                                                        maxlength="500" id="lblDescripcionM" placeholder="Descripción"></textarea>
                                                </div>
                                            </div>
                                            <div style="margin-top: 0px;" class="form-group">
                                                <label style="float: right; padding-top: 0px;" class="col-sm-2 control-label" id="contadorM">
                                                </label>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-2">
                                                    <label class="control-label align-rt">
                                                        Carrera</label>
                                                </div>
                                                <div class="col-sm-10">
                                                    <select class="form-control" id="cboCarreraM" name="cboCarrera">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divCursoM">
                                                <div class="col-sm-2">
                                                    <label class="control-label align-rt">
                                                        Curso</label>
                                                </div>
                                                <div class="col-sm-10">
                                                    <select class="form-control" id="cboCursoM" name="cboCurso">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-2">
                                                    <label class="control-label align-rt">
                                                        Fecha</label>
                                                </div>
                                                <div class="col-sm-3" id="date-popup-group" style="font-size: smaller;">
                                                    <div class="input-group date">
                                                        <input type="text" class="form-control" id="dtpFechaM" name="dtpFecha" placeholder="__/__/____"
                                                            data-provide="datepicker" style="font-size: 13px;">
                                                        <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                                    </div>
                                                </div>
                                                <div class="col-md-7" style="padding-left: 0px;">
                                                    <div class="input-group" style="padding-top: 0px;">
                                                        <label class="col-sm-2 control-label" style="width: 15%; padding-left: 0px;">
                                                            Hora</label>
                                                        <select class="form-control " id="cboHoraDM" name="cboHoraDM" style="width: 18%;">
                                                            <option value="" selected="" style="padding: 0px;">00 </option>
                                                        </select>
                                                        <select class="form-control" id="cboMinutoDM" name="cboMinutoDM" style="width: 18%;">
                                                            <option value="" selected="">00 </option>
                                                        </select>
                                                        <label class="col-sm-1 control-label" style="width: 13%; text-align: center;">
                                                            a</label>
                                                        <select class="form-control" id="cboHoraAM" name="cboHoraAM" style="width: 18%;">
                                                            <option value="" selected="" style="padding: 0px;">00 </option>
                                                        </select>
                                                        <select class="form-control" id="cboMinutoAM" name="cboMinutoAM" style="width: 18%;">
                                                            <option value="" selected="">00 </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <a onclick="fnVer()" style="cursor: pointer">
                                            <label id="lblVerAlumnos">
                                                Ver estudiantes</label></a>
                                        <div class="row" id="divTutoradosM">
                                            <div class="col-md-12">
                                                <div class="row">
                                                    <div class="input-group">
                                                        <input type="text" class="form-control" name="busPoint" id="busPoint" placeholder="Buscar Estudiante"
                                                            style="height: 2.5em;">
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-primary btn-sm" type="button" id="Button1" onclick="fnAgregar();"
                                                                style="height: 2.5em;">
                                                                Agregar Estudiante</button>
                                                        </span>
                                                    </div>
                                                </div>
                                                <br />
                                                <div class="table-responsive">
                                                    <div id="tTutoradosM_wrapper" class="dataTables_wrapper" role="grid">
                                                        <table id="tTutoradosM" name="tTutoradosM" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="5%" style="font-weight: bold; width: 5%; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="N°: activate to sort column ascending">
                                                                        N°
                                                                    </td>
                                                                    <td width="38%" style="font-weight: bold; width: 38%; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Alumno: activate to sort column ascending">
                                                                        Estudiante
                                                                    </td>
                                                                    <td width="5%" style="font-weight: bold; width: 5%;" tabindex="0" rowspan="1" colspan="1"
                                                                        aria-label="Asistencia: activate to sort column descending">
                                                                        <input type="checkbox" id="Checkbox1">
                                                                        <%--                                          <input type="checkbox" id="chkSelectAll" class="chkSelectAll" name="chkSelectAll" display="none">
									                                                            <label for="chkSelectAll"><span>sd</span></label>
                                                                <%-- <input type="checkbox" id="chkPall" name="chkPall">
									                                                                <label name="chkPall" for="chkPall"><span></span>Lunes</label>--%>
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="3" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbTutoradosM">
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
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" id="btnGuardarM" class="btn btn-primary" onclick="fnConfirmar(this)">
                                Guardar</button>
                            <button type="button" class="btn btn-danger" id="btnCancelarM" onclick="fnCancelar(this)">
                                Cancelar</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="modal fade" id="mdIndividual" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog" id="Div12">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                            </button>
                            <h4 class="modal-title" id="H3">
                                Tutoría Individual
                            </h4>
                        </div>
                        <div class="modal-body">
                            <form id="frmIndividual" name="frmIndividual" enctype="multipart/form-data" class="form-horizontal"
                            onsubmit="return false;" action="#" method="post">
                            <div class="row">
                                <div id="div13">
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
                                                    <label class="control-label" id="lblIndividual">
                                                        Nombre de Estudiante</label>
                                                </div>
                                                <%--   </div>--%>
                                            </div>
                                            <div id="divAsistir">
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-2">
                                                    <label class="control-label align-rt">
                                                        Actividad</label>
                                                </div>
                                                <div class="col-sm-5" style="font-size: smaller;">
                                                    <select class="form-control " id="cboActividad" name="cboActividad">
                                                        <option value="" selected="" style="padding: 0px;"></option>
                                                    </select>
                                                </div>
                                                <%--<label class="col-sm-2 control-label">
                                                Asistió</label>
                                            <div class="col-sm-3">
                                               <ul class="list-inline checkboxes-radio">
                                                    <li class="ms-hover">
                                                        <input type="checkbox" id="chkAsistir" class="chkAsistir" dy="1" name="chkAsistir">
                                                        <label for="chkAsistir">
                                                            <span></span></label>
                                                    </li>
                                                </ul> 
                                            </div> --%>
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
                                                    <textarea name="txtAccion" class="form-control text-area" cols="30" rows="10" id="txtAccion"
                                                        placeholder="Acciones Futuras"></textarea>
                                                </div>
                                            </div>
                                            <div class="form-group" id="date-popup-group" style="font-size: smaller;">
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
                                                    <select class="form-control " id="cboEstado" name="cboEstado">
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
                                                    <select class="form-control " id="cboResultado" name="cboResultado">
                                                        <option value="" selected="" style="padding: 0px;"></option>
                                                    </select>
                                                </div>
                                                <div class="col-md-2">
                                                    <label class="control-label align-rt">
                                                        Riesgo</label>
                                                </div>
                                                <div class="col-sm-3" style="font-size: smaller;">
                                                    <select class="form-control " id="cboRiesgo" name="cboRiesgo">
                                                        <option value="" selected="" style="padding: 0px;"></option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" id="btnGuardaIndividual" class="btn btn-primary" onclick="fnConfirmar(this)">
                                Guardar</button>
                            <button type="button" class="btn btn-danger" id="Button2" onclick="fnCancelar(this)">
                                Cancelar</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <%--<div class="modal fade" id="mdEliminar" role="dialog" aria-labelledby="myModalLabel"
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
                                            <button type="button" class="btn btn-danger" id="Button2" data-dismiss="modal" >
                                                Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
        </div>
        <!-- row -->
    </div>
    <!-- row -->
    </div> </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
