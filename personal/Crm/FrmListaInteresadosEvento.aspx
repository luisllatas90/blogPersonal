<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaInteresadosEvento.aspx.vb"
    Inherits="crm_FrmListaInteresadosEvento" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/app.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <%--<script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/materialize.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/bic_calendar.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/core.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js?x=1'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%--<script src="../assets/js/dataTables.buttons.min.js" type="text/javascript"></script>
    <link href="../assets/css/dataTables.tableTools.css" rel="stylesheet" type="text/css" />
    <script src="../assets/js/dataTables.tableTools.js" type="text/javascript"></script>
    <script type="text/javascript" src='../assets/js/funciones.js'></script>
    <script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=1'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>
    <script src="../assets/js/jquery.maskedinput.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>--%>

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>

    <script type="text/javascript" src="js/crm.js?x=90"></script>

    <script type="text/javascript" src="js/JsInteresado.js?x=600"></script>

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
            height: 28px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .help_block
        {
            margin-bottom: 0px;
            margin-top: 0px;
            vertical-align: middle;
        }
        .style1
        {
            color: #FF0000;
        }
        .style2
        {
            text-align: left;
        }
        .piluku-accordion-two .panel .panel-heading
        {
            background-color: #fb5d5d;
        }
        .piluku-accordion .panel .panel-heading .panel-title a
        {
            color: White;
        }
        .panel-body input:focus
        {
            background-color: #FFE6E8;
        }
        .panel-body select:focus
        {
            background-color: #FFE6E8;
        }
        .panel-body .btn:focus
        {
            border-style: double;
            border-width: medium;
            border-color: Red;
        }
        .progress-bar
        {
            font-size: 10px;    
        }

        .btn.dropdown-toggle 
        {
            padding: 3px 10px;
        }

        button[data-id="cboCarreraProfesional"] + .dropdown-menu.open {
            min-width: 400px !important;
            max-width: 460px !important;
        }

        button[data-id="cboCarreraProfesional"] + .dropdown-menu.open .inner.open {
            overflow-x: hidden
        }

        button[data-id="cboCentroCosto"] + .dropdown-menu.open {
            min-width: 400px !important;
            max-width: 460px !important;
        }

        button[data-id="cboCentroCosto"] + .dropdown-menu.open .inner.open {
            overflow-x: hidden
        }

        .dropdown-menu.inner {
            font-size: 13px;
        }

        input[name="chkMisAcuerdos"] {
            margin: 1px 0 0;
        }

        #btnAgregar {
            margin-right: 10px;
        }

        #btnAsignarAnexo {
            margin-right: 15px;;
        }

        #divBotonesAccion {
            padding-bottom: 0px;
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
                                            <i class="icon ti-pencil-alt page_header_icon"></i><span class="main-text">Inscripción
                                                Interesados</span>
                                        </div>
                                        <div class="buttons-list" id="divBotonesAccion">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-info" id="btnAsignarAnexo" name="btnAsignarAnexo"
                                                    href="#" data-toggle="modal"><i class="ion-android-call">
                                                    </i>&nbsp;Asignar Anexo</a>
                                                <a class="btn btn-primary" id="btnListar" onclick="event.preventDefault();fnListar()" href="#"><i class="ion-android-search">
                                                    </i>&nbsp;Listar</a>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                            <div class="form-group">
                                                <div class="col-md-12">
                                                    <label class="col-md-0 control-label ">
                                                        <span class="style1">(*) Campos obligatorios a filtrar</span></label>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">
                                                        Tipo Estudio<span class="style1">(*)</span></label></label>
                                                    <div class="col-md-9">
                                                        <select name="cboTipoEstudio" class="form-control" id="cboTipoEstudio">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">
                                                        Situación</label>
                                                    <div class="col-md-9">
                                                        <select name="cboTipoPersona" class="form-control" id="cboTipoPersona">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">
                                                        Convocatoria<span class="style1">(*)</span></label>
                                                    <div class="col-md-9">
                                                        <select name="cboConvocatoria" class="form-control" id="cboConvocatoria">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">
                                                        Comunicación</label>
                                                    <div class="col-md-9">
                                                        <select name="cboComunicacion" class="form-control" id="cboComunicacion">
                                                            <%--<option value="t" selected="selected">todos </option>
                                                            <option value="1">con comunicación</option>
                                                            <option value="0">sin comunicación</option>--%>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">
                                                        Evento<span class="style1">(*)</span></label>
                                                    <div class="col-md-9" style="display: flex;">
                                                        <select name="cboEvento" class="form-control selectpicker" id="cboEvento" multiple>
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                        <button type="button" id="bntResetEvento" name="bntResetEvento" 
                                                            class="btn btn-sm btn-warning" title="Limpiar Filtro" style="font-size: 12px; padding: 4px 10px;">
                                                            <i class="ion-close"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">
                                                        Acuerdo</label>
                                                    <div class="col-md-3">
                                                        <select name="cboAcuerdo" class="form-control" id="cboAcuerdo">
                                                            <option value="-1" selected="selected">TODOS </option>
                                                            <option value="1">Con Acuerdo</option>
                                                            <option value="0">Sin Acuerdo</option>
                                                        </select>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="input-group date-block">
                                                            <div class="input-group date">
                                                                <input type="text" class="form-control" id="txtFechaAcuerdo" name="txtFechaAcuerdo" data-provide="datepicker"
                                                                    placeholder="dd/mm/aaaa" style="z-index: 1" disabled="disabled" />
                                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline"></i></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="checkbox">
                                                            <label>
                                                              <input type="checkbox" name="chkMisAcuerdos" id="chkMisAcuerdos"/>Mis acuerdos
                                                            </label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 col-sm-12">
                                                    <label class="col-md-3 control-label ">
                                                        DNI/Nombres</label>
                                                    <div class="col-md-9">
                                                        <input type="text" id="txttexto" name="txttexto" class="form-control" placeholder="DNI / Apellidos Y Nombres"
                                                            style="border: 1px solid #d9d9d9; vertical-align: middle" />
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-12">
                                                    <label class="col-md-3 control-label ">
                                                        Carrera Profesional</label>
                                                    <div class="col-md-7" style="display: flex">
                                                        <select name="cboCarreraProfesional" class="form-control selectpicker" id="cboCarreraProfesional" multiple>
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                        <button type="button" id="bntResetCarreraProfesional" name="bntResetCarreraProfesional" 
                                                            class="btn btn-sm btn-warning" title="Limpiar Filtro" style="font-size: 12px; padding: 4px 10px;">
                                                            <i class="ion-close"></i>
                                                        </button>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <select name="cboFiltroPrioridad" class="form-control" id="cboFiltroPrioridad">
                                                            <option value="1" selected="">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                            <option value="5">5</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 col-sm-12">
                                                    <label class="col-md-3 control-label ">Origen</label>
                                                    <div class="col-md-9">
                                                        <select id="cboFiltroOrigen" name="cboFiltroOrigen" class="form-control">
                                                            <option value="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 col-sm-12">
                                                    <label class="col-md-3 control-label ">Centro de costo</label>
                                                    <div class="col-md-9" style="display: flex;">
                                                        <select name="cboCentroCosto" class="form-control selectpicker" id="cboCentroCosto" multiple>
                                                            <option value="-1" selected>-- Seleccione -- </option>
                                                        </select>
                                                        <button type="button" id="bntResetCentroCosto" name="bntResetCentroCosto" 
                                                            class="btn btn-sm btn-warning" title="Limpiar Filtro" style="font-size: 12px; padding: 4px 10px;">
                                                            <i class="ion-close"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label ">Grado</label>
                                                    <div class="col-md-9">
                                                        <select id="cboGrados" name="cboGrados" multiple class="form-control selectpicker">
                                                            <option value="T">TERCERO</option>
                                                            <option value="C">CUARTO</option>
                                                            <option value="Q">QUINTO</option>
                                                            <option value="E">EGRESADO</option>
                                                            <option value="U">UNIVERSITARIO</option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="row">
                                                        <label class="col-md-3 control-label">Origen Admisión</label>
                                                        <div class="col-md-9">
                                                            <select name="cboOrigenAdmision" class="form-control" id="cboOrigenAdmision">
                                                                <option value="" selected="selected">TODOS</option>
                                                                <option value="W">WEB</option>
                                                                <option value="P">PRESENCIAL</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 col-sm-12">
                                                    <label class="col-md-3 control-label ">Colegio</label>
                                                    <div class="col-md-9" style="display: flex">
                                                        <select name="cboFiltroColegio" class="form-control selectpicker" id="cboFiltroColegio" multiple>
                                                        </select>
                                                        <button type="button" id="bntResetFiltroColegio" name="bntResetFiltroColegio" 
                                                            class="btn btn-sm btn-warning" title="Limpiar Filtro" style="font-size: 12px; padding: 4px 10px;">
                                                            <i class="ion-close"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label">Desde</label>
                                                    <div class="col-md-3">
                                                        <div class="input-group date-block">
                                                            <div class="input-group date">
                                                                <input type="text" class="form-control" id="txtFechaDesde" name="txtFechaDesde" data-provide="datepicker"
                                                                    placeholder="dd/mm/aaaa" style="z-index: 1" />
                                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline"></i></span>
                                                            </div>
                                                            <%--<span class="help-block">Formato: dd/mm/aaaa</span>--%>
                                                        </div>
                                                    </div>
                                                    <label class="col-md-2 control-label">Hasta</label>
                                                    <div class="col-md-3">
                                                        <div class="input-group date-block">
                                                            <div class="input-group date">
                                                                <input type="text" class="form-control" id="txtFechaHasta" name="txtFechaHasta" data-provide="datepicker"
                                                                    placeholder="dd/mm/aaaa" style="z-index: 1" />
                                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline"></i></span>
                                                            </div>
                                                            <%--<span class="help-block">Formato: dd/mm/aaaa</span>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label">Requisitos Faltantes</label>
                                                    <div class="col-md-9" style="display: flex;">
                                                        <select name="cboRequisitoAdmision" class="form-control selectpicker" id="cboRequisitoAdmision" multiple>
                                                            <option value="-1" selected>-- Seleccione -- </option>
                                                        </select>
                                                        <button type="button" id="bntResetRequisitoAdmision" name="bntResetRequisitoAdmision" 
                                                            class="btn btn-sm btn-warning" title="Limpiar Filtro" style="font-size: 12px; padding: 4px 10px;">
                                                            <i class="ion-close"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <label class="col-md-3 control-label">
                                                        Rango de Letras</label>
                                                    <div class="col-md-2">
                                                        <input type="text" id="txtletraini" name="txtletraini" class="form-control" placeholder="A"
                                                            maxlength="1" style="border: 1px solid #d9d9d9; text-align: center" />
                                                    </div>
                                                    <label class="col-md-1 control-label">
                                                        -</label>
                                                    <div class="col-md-2">
                                                        <input type="text" id="txtletrafin" name="txtletrafin" class="form-control" placeholder="Z"
                                                            maxlength="1" style="border: 1px solid #d9d9d9; text-align: center" />
                                                    </div>
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="modal fade" id="mdPerfilInteresado" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg" style="width: 98%">
                        <div class="modal-content">
                            <div id="pagina">
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
                            <%--<i class="ion-android-list"></i>--%>
                            Listado de Interesados <span class="panel-options"><a class="panel-refresh" href="#">
                                <i class="icon ti-reload" onclick="fnListar(false)"></i></a><a class="panel-minimize"
                                    href="#"><i class="icon ti-angle-up"></i></a></span>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm-12" style="display: flex;">
                                <a class="btn btn-green" id="btnAgregar" style="display: none"><i class="ion-android-add"></i>&nbsp;Agregar</a>
                                <a class="btn btn-orange" id="btnExportar" style="display: none"><i class="ion-ios-arrow-thin-down"></i>&nbsp;Exportar</a>
                            </div>
                            <br>
                        </div>
                        <div class="table-responsive">
                            <div id="tEvento_wrapper" class="dataTables_wrapper" role="grid">
                                <table id="tInteresados" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                    <thead>
                                        <tr role="row">
                                            <td width="3%" style="font-weight: bold; width: 50px;" class="sorting_asc" tabindex="0"
                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                N.
                                            </td>
                                            <!-- <td width="6%" style="font-weight: bold; width: 90px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                Tipo Doc.
                                            </td> -->
                                            <td width="6%" style="font-weight: bold; width: 90px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                N° Doc.
                                            </td>
                                            <td width="10%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                Apellido Paterno
                                            </td>
                                            <td width="10%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                Apellidos Materno
                                            </td>
                                            <td width="10%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                Nombres
                                            </td>
                                            <td width="8%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                Situación
                                            </td>
                                            <td width="10%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                Carrera
                                            </td>
                                            <td width="12%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Interes: activate to sort column ascending">
                                                Interés
                                            </td>
                                            <td width="5%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Últ. Comun.: activate to sort column ascending">
                                                Últ. Comun.
                                            </td>
                                            <td width="5%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Fec. Últ. Comun.: activate to sort column ascending">
                                                Fec. Últ. Comun.
                                            </td>
                                            <td width="5%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Últ. Comun.: activate to sort column ascending">
                                                Últ. Acuerdo.
                                            </td>
                                            <td width="5%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Req. Faltantes: activate to sort column ascending">
                                                Req. Faltantes
                                            </td>
                                            <td width="3%" style="font-weight: bold; width: 100px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Fecha Reg.: activate to sort column ascending">
                                                Fecha Reg.
                                            </td>
                                            <td width="7%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                Opciones
                                            </td>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th colspan="14" rowspan="1">
                                            </th>
                                        </tr>
                                    </tfoot>
                                    <tbody id="tbInteresados">
                                        <%--  <tr class="odd">
                                            <td valign="top" colspan="6" class="dataTables_empty">
                                                No se ha encontrado informacion
                                            </td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <!-- /panel -->
                        <div class="row">
                            <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                                aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 1;">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="myModalLabel3">
                                                Registrar Interesado</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">Origen:</label>
                                                    <div class="col-sm-4">
                                                        <select class="form-control" id="cboOrigen" name="cboOrigen">
                                                            <option value="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <button type="button" id="btnEliminarInteresado" class="btn btn-danger">Eliminar</button>
                                                    </div>
                                                </div>
                                                <hr style="margin: 7px 0px;">
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-2 control-label">
                                                        Tipo Documento:</label>
                                                    <div class="col-sm-4">
                                                        <select class="form-control" id="cboTipoDocumento" name="cboTipoDocumento">
                                                            <option value="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        N° Documento:</label>
                                                    <div class="col-sm-3">
                                                        <input type="text" id="txtnum_doc" name="txtnum_doc" class="form-control" />
                                                        <div class="checkbox">
                                                            <label>
                                                                <input type="checkbox" id="chkConfirmado" name="chkConfirmado" checked>¿Confirmado?
                                                            </label>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <a class="btn btn-primary" id="btnBuscaInt" href="#" onclick="fnBuscaxTipoyNumDoc()">
                                                            <i class="ion-android-search"></i></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="datos">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Apellido Paterno:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtapepat" name="txtapepat" class="form-control" />
                                                        </div>
                                                        <label class="col-sm-2 control-label">
                                                            Apellido Materno:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtapemat" name="txtapemat" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-2 control-label">
                                                            Nombres:</label>
                                                        <div class="col-sm-4">
                                                            <input type="text" id="txtnombre" name="txtnombre" class="form-control" />
                                                        </div>
                                                        <label class="col-sm-2 control-label">
                                                            Fecha Nacimiento</label>
                                                        <div class="col-sm-4" id="date-popup-group">
                                                            <div class="input-group date-block">
                                                                <div class="input-group date" id="FecNac">
                                                                    <input type="text" class="form-control" id="date" name="txtfecnac" data-provide="datepicker"
                                                                        placeholder="dd/mm/aaaa" />
                                                                    <span class="input-group-addon sm" id="btnfecha"><i class="ion ion-ios-calendar-outline"
                                                                        id="FechaNacimiento"></i></span>
                                                                </div>
                                                                <%--<span class="help-block">Formato: dd/mm/aaaa</span>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Nombres:</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtnombre" name="txtnombre" class="form-control" />
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div id="FormMensajeBusq">
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-md-12">
                                                            <center>
                                                                <button id="btnCoincidencias" name="btnCoincidencias" class="btn btn-orange" onclick="fnBuscaxApeyNom()"
                                                                    style="display: none">
                                                                    <span>Buscar Coincidencias</span> <i class="ion-android-search"></i>
                                                                </button>
                                                            </center>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div id="detalle_int">
                                                    <%--<div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Fecha Nacimiento:</label>
                                                            <div class="col-sm-4" id="date-popup-group">
                                                                <div class="input-group date-block">
                                                                    <div class="input-group date" id="FechaInicio">
                                                                        <input name="txtfecnac" class="form-control" id="txtfecnac" style="text-align: right;"
                                                                            type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                                            <span class="input-group-addon sm" id="btnfecha"><i class="ion ion-ios-calendar-outline"
                                                                            id="FechaNacimiento"></i></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <%--                              <label class="col-sm-3 control-label">
                                                                Fecha Nacimiento</label>
                                                            <div class="col-sm-5" id="date-popup-group">
                                                                <div class="input-group date-block">
                                                                    <div class="input-group date" id="FecNac">
                                                                        <input type="text" class="form-control" id="date" name="txtfecnac" data-provide="datepicker" />
                                                                        <span class="input-group-addon sm" id="btnfecha"><i class="ion ion-ios-calendar-outline"
                                                                            id="FechaNacimiento"></i></span>
                                                                    </div>
                                                                    <span class="help-block">Formato: dd/mm/aaaa</span>
                                                                </div>
                                                            </div>--%>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">
                                                                Colegio:</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="txtie" name="txtie" class="form-control" onclick="return txtie_onclick()" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" id="btnie" class="btn btn-warning">
                                                                    <i class="ion-android-search"></i>
                                                                </button>
                                                            </div>
                                                            <label class="col-sm-2 control-label">
                                                                Grado de Estudios:</label>
                                                            <div class="col-sm-3">
                                                                <select id="cboGradoEstudios" name="cboGradoEstudios" class="form-control">
                                                                    <option value="" selected="selected">-- Seleccione --</option>
                                                                    <option value="T">TERCERO</option>
                                                                    <option value="C">CUARTO</option>
                                                                    <option value="Q">QUINTO</option>
                                                                    <option value="E">EGRESADO</option>
                                                                    <option value="U">UNIVERSITARIO</option>
                                                                </select>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">
                                                                Dirección:</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="txtdir" name="txtdir" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" id="btndir" class="btn btn-primary">
                                                                    <i class="ion-ios-home"></i>
                                                                </button>
                                                            </div>
                                                            <label class="col-sm-2 control-label">
                                                                Año de egreso:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id="txtAnioEgreso" name="txtAnioEgreso" class="form-control" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <%--                                        <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">
                                                                Teléfono:</label>
                                                            <div class="col-sm-7">
                                                                <input type="text" id="txttel" name="txttel" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                                <button type="button" id="btntel" class="btn btn-primary" >
                                                                    <i class="ion-ios-telephone"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">
                                                                Correo Electrónico:</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="txtema" name="txtema" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" id="btnema" class="btn btn-primary">
                                                                    <i class="ion-ios-email-outline"></i>
                                                                </button>
                                                            </div>
                                                            <label class="col-sm-2 control-label">
                                                                Teléfono:</label>
                                                            <div class="col-sm-2">
                                                                <input type="text" id="txttel" name="txttel" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" id="btntel" class="btn btn-primary">
                                                                    <i class="ion-ios-telephone"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">
                                                                Carrera Profesional USAT:</label>
                                                            <div class="col-sm-4">
                                                                <input type="text" id="txtcp" name="txtcp" class="form-control" />
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" id="btncp" class="btn btn-primary">
                                                                    <i class="ion-university"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <center>
                                                        <button type="button" id="btnCuestionario" name="btnCuestionario" class="btn btn-warning">
                                                            <i class="ion-clipboard"></i>&nbsp;&nbsp;Cuestionario
                                                        </button>
                                                        <button type="button" id="btnInscribir" class="btn btn-primary" onclick="fnInscribir(false);">
                                                            Inscribir y Cerrar</button>
                                                        <button type="button" id="btnInscribirYRedirigir" class="btn btn-success" onclick="fnInscribir(true);">
                                                            Inscribir y Revisar</button>
                                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                            Cancelar</button>
                                                    </center>
                                                </div>
                                            </div>
                                            <input type="hidden" id="hdcod_i" name="hdcod_i" value="0" />
                                            <input type="hidden" id="codie" name="codie" value="0" />
                                            <input type="hidden" id="codcpf" name="codcpf" value="0" />
                                            <input type="hidden" id="codeve" name="codeve" value="0" />
                                            <input type="hidden" id="hdtip" name="hdtip" value="N" />
                                            </form>
                                            <div id="Coincidencias" style="display: none">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <div class="col-sm-12">
                                                            <div class="table-responsive" style="font-size: 11px; font-weight: 300; line-height: 18px;">
                                                                <div id="Div2" class="dataTables_wrapper" role="grid">
                                                                    <table id="tCoincidencias" class="display dataTable cell-border" width="99%">
                                                                        <thead>
                                                                            <tr role="row">
                                                                                <td width="5%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                    Doc.
                                                                                </td>
                                                                                <td width="10%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                    Numero
                                                                                </td>
                                                                                <td width="25%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                    Apellido Paterno
                                                                                </td>
                                                                                <td width="25%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                    Apellido Materno
                                                                                </td>
                                                                                <td width="30%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                    Apellido Nombres
                                                                                </td>
                                                                                <td width="5%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                    rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                    Seleccionar
                                                                                </td>
                                                                            </tr>
                                                                        </thead>
                                                                        <tfoot>
                                                                            <tr>
                                                                                <th colspan="6" rowspan="1">
                                                                                </th>
                                                                            </tr>
                                                                        </tfoot>
                                                                        <tbody id="TbCoincidencias">
                                                                        </tbody>
                                                                    </table>
                                                                    <br />
                                                                    <center>
                                                                        <button id="btnNuevoInt" name="btnNuevoInt" class="btn btn-green" onclick="NuevoInteresado()">
                                                                            <span>Nuevo Interesado</span> <i class="ion-android-person-add"></i>
                                                                        </button>
                                                                    </center>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensaje">
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
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdDireccion" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H3">
                                                Registrar Dirección</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroD" name="frmRegistroD" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Región:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboRegionD" name="cboRegionD">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Provincia:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboProvinciaD" name="cboProvinciaD">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Distrito:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboDistritoD" name="cboDistritoD">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Dirección:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtDireccion" name="txtDireccion" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Domicilio Actual:</label>
                                                    <div class="col-sm-8">
                                                        <input type="checkbox" id="chkVigenciaD" name="chkVigenciaD" style="display: block;"
                                                            checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeD">
                                            </div>
                                            <input type="hidden" id="hdcod_D" name="hdcod_D" value="0" />
                                            <center>
                                                <button type="button" id="Button10" class="btn btn-primary" onclick="fnGuardarDireccion();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="Button11" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div1" class="dataTables_wrapper" role="grid">
                                                                <table id="tDirecciones" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Direccion
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Región
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Provincia
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 203px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                                                Distrito
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Fecha Registro
                                                                            </td>
                                                                            <td width="5%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Vigencia
                                                                            </td>
                                                                            <td width="5%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Opciones
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="8" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbDirecciones">
                                                                        <%--<tr class="odd">
                                                                            <td valign="top" colspan="6" class="dataTables_empty">
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
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdTelefono" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H4">
                                                Registrar Teléfono</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroT" name="frmRegistroT" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Tipo:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboTipoTelefono" name="cboTipoTelefono">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                            <option value="FIJO">FIJO</option>
                                                            <option value="CELULAR">CELULAR </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Numero:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtnumeroTel" name="txtnumeroTel" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Detalle:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtdetalleTel" name="txtdetalleTel" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">Pertenencia:</label>
                                                    <div class="col-sm-8">
                                                        <label class="radio-inline">
                                                            <input type="radio" name="rbtPertenencia" id="rbtPertenenciaInteresado" value="I" style="display: block;"> Interesado
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="rbtPertenencia" id="rbtPertenenciaPadre" value="P" style="display: block;"> Padre
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="rbtPertenencia" id="rbtPertenenciaMadre" value="M" style="display: block;"> Madre
                                                        </label>
                                                        <label class="radio-inline">
                                                            <input type="radio" name="rbtPertenencia" id="rbtPertenenciaApoderado" value="M" style="display: block;"> Apoderado
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Vigencia:</label>
                                                    <div class="col-sm-8">
                                                        <input type="checkbox" id="chkVigenciaT" name="chkVigenciaT" style="display: block;"
                                                            checked="checked" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeT">
                                            </div>
                                            <input type="hidden" id="hdcod_T" name="hdcod_T" value="0" />
                                            <center>
                                                <button type="button" id="Button12" class="btn btn-primary" onclick="fnGuardarTelefono();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="Button13" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div8" class="dataTables_wrapper" role="grid">
                                                                <table id="tTelefonos" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="10%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                                                Tipo
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Número: activate to sort column ascending">
                                                                                Número
                                                                            </td>
                                                                            <%--                                                                 <td width="35%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Detalle: activate to sort column ascending">
                                                                                Detalle
                                                                            </td>--%>
                                                                            <td width="15%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Fecha: activate to sort column ascending">
                                                                                Fecha
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Pertenencia: activate to sort column ascending">
                                                                                Pertenencia
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Vigencia: activate to sort column ascending">
                                                                                Vigencia
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                                    <tbody id="tbTelefonos">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
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
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdEmail" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H5">
                                                Registrar Email</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroEmail" name="frmRegistroEmail" enctype="multipart/form-data"
                                            class="form-horizontal" method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Tipo:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboTipoEMail" name="cboTipoEMail">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                            <option value="PERSONAL">Personal </option>
                                                            <option value="INSTITUCIONAL">Institucional </option>
                                                            <option value="OTRO">Otro </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Email:</label>
                                                    <div class="col-sm-7">
                                                        <input type="text" id="txtDescripcionEMail" name="txtDescripcionEMail" class="form-control" />
                                                    </div>
                                                    <button type="button" id="btnValidarEmail" class="btn btn-sm btn-warning" onClick="fnValidarEmail();" style="padding: 3px 10px;">
                                                        <i class="ion-android-checkmark-circle"></i>
                                                    </button>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Detalle:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtDetalleEMail" name="txtDetalleEMail" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Vigencia:</label>
                                                    <div class="col-sm-1">
                                                        <input type="checkbox" id="chkVigenciaEMail" name="chkVigenciaEMail" style="display: block;margin-top: 10px;"
                                                            checked="checked" />
                                                    </div>
                                                    <label class="col-sm-2 control-label">
                                                        Verificado:</label>
                                                    <div class="col-sm-1">
                                                        <input type="checkbox" id="chkVerificadoEMail" name="chkVerificadoEMail" disabled style="display: block;margin-top: 10px;" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeEmail">
                                            </div>
                                            <input type="hidden" id="hdcod_EMail" name="hdcod_EMail" value="0" />
                                            <center>
                                                <button type="button" id="BtnGuardarEMail" name="BtnGuardarEMail" class="btn btn-primary"
                                                    onclick="fnGuardarEMail();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="BtnCancelarEMail" name="BtnCancelarEMail"
                                                    data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div11" class="dataTables_wrapper" role="grid">
                                                                <table id="tEMail" class="display dataTable cell-border" width="100%" style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 50px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="20%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                                                Tipo
                                                                            </td>
                                                                            <td width="25%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Email: activate to sort column ascending">
                                                                                Email
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 130px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                F. Registro
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 50px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Vigencia: activate to sort column ascending">
                                                                                Vigente
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 50px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Verificado: activate to sort column ascending">
                                                                                Verificado
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 80px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Opciones
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="6" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbEMail">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdInstitucionEd" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H6">
                                                Buscar Institución Educativa</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroIE" name="frmRegistroIE" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Región:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboRegionIE" name="cboRegionIE">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Nombre:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtnombreIE" name="txtnombreIE" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>--%>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div10" class="dataTables_wrapper" role="grid">
                                                                <table id="tInstitucionEducativa" class="display dataTable cell-border" width="100%"
                                                                    style="width: 100%;">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="25%" style="font-weight: bold;" class="sorting_asc" tabindex="0" rowspan="1"
                                                                                colspan="1" aria-sort="ascending" aria-label="Institución Educativa : activate to sort column descending">
                                                                                Institución Educativa
                                                                            </td>
                                                                            <td width="30%" style="font-weight: bold;" class="sorting" tabindex="0" rowspan="1"
                                                                                colspan="1" aria-label="Dirección: activate to sort column ascending">
                                                                                Dirección
                                                                            </td>
                                                                            <td width="40%" style="font-weight: bold;" class="sorting" tabindex="0" style="text-align: center"
                                                                                rowspan="1" colspan="1" aria-label="Distrito / Provincia / Región : activate to sort column ascending">
                                                                                Distrito / Provincia / Región
                                                                            </td>
                                                                            <td width="3%" style="font-weight: bold;" class="sorting" tabindex="0" rowspan="1"
                                                                                colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                                Seleccionar
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="4" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbInstitucionEducativa">
                                                                        <%--<tr class="odd">
                                                                            <td valign="top" colspan="6" class="dataTables_empty">
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
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="modal-footer">
                                            <center>
                                                <button type="button" class="btn btn-danger" id="Button17" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <div class="row">
                            <div class="modal fade" id="mdCarrera" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H7">
                                                Carrera Profesional</h4>
                                        </div>
                                        <div class="modal-body">
                                            <form id="frmRegistroCP" name="frmRegistroCP" enctype="multipart/form-data" class="form-horizontal"
                                            method="post" onsubmit="return false;" action="#">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Carrera Profesional:</label>
                                                    <div class="col-sm-8">
                                                        <select class="form-control" id="cboCarrera" name="cboCarrera">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Obervación:</label>
                                                    <div class="col-sm-8">
                                                        <input type="text" id="txtdetalleCpf" name="txtdetalleCpf" class="form-control" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Prioridad:</label>
                                                    <div class="col-sm-4">
                                                        <%--<input type="checkbox" id="chkVigenciaCpf" name="chkVigenciaCpf" style="display: block;"
                                                            checked="checked" />--%>
                                                        <select id="cboPrioridad" name="cboPrioridad" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="FormMensajeCP">
                                            </div>
                                            <input type="hidden" id="hdcod_CP" name="hdcod_CP" value="0" />
                                            <center>
                                                <button type="button" id="Button16" class="btn btn-primary" onclick="fnGuardarCpf();">
                                                    Guardar</button>
                                                <button type="button" class="btn btn-danger" id="Button17" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                            </form>
                                            <br />
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-12">
                                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                            <div id="Div14" class="dataTables_wrapper" role="grid">
                                                                <table id="tCarrera" class="display dataTable cell-border" width="100%">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 54px;" class="sorting_asc" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">
                                                                                N.
                                                                            </td>
                                                                            <td width="31%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Carrera Prof.
                                                                            </td>
                                                                            <td width="15%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Tipo Estudio
                                                                            </td>
                                                                            <td width="35%" style="font-weight: bold; width: 89px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Semestre: activate to sort column ascending">
                                                                                Evento
                                                                            </td>
                                                                            <td width="8%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                Fecha Reg.
                                                                            </td>
                                                                            <td width="3%" style="font-weight: bold; width: 169px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Dep. Académico: activate to sort column ascending">
                                                                                Prioridad
                                                                            </td>
                                                                            <td width="3%" style="font-weight: bold; width: 111px;" class="sorting" tabindex="0"
                                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                                    <tbody id="tbCarrera">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
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
                                        <div class="modal-footer">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                        <!-- ********************************** CUESTIONARIO **************************************************-->
                        <div class="row">
                            <div class="modal fade" id="mdCuestionario" role="dialog" aria-labelledby="myModalLabel"
                                style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                <div class="modal-dialog modal-lg">
                                    <div class="modal-content">
                                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                            </button>
                                            <h4 class="modal-title" id="H1">
                                                Cuestionario</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div id="ModalPreguntas">
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <center>
                                                <button type="button" class="btn btn-danger" id="Button1" data-dismiss="modal">
                                                    Cancelar</button>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- row -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
    <div class="modal fade" id="mdAnexo" role="dialog" aria-labelledby="myModalLabel" style="z-index: 1"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                    </button>
                    <h5 class="modal-title" id="H1">
                        Asignar Número de Anexo</h5>
                </div>
                <div class="modal-body">
                    <form id="frmAnexo" name="frmAnexo" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Número:</label>
                                <div class="col-sm-8">
                                    <input type="text" name="txtNumeroAnexo" id="txtNumeroAnexo">
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <center>
                        <button type="button" id="btnGuardarAnexo" name="btnGuardarAnexo" class="btn btn-primary"">
                                            Guardar</button>
                                        <button type=" button" class="btn btn-danger" id="BtnCancelar" name="BtnCancelar"
                            data-dismiss="modal">
                            Cancelar</button>
                    </center>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" id="mdlConfirmar">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span
                            aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Confirmar operación</h4>
                </div>
                <div class="modal-body">
                    <div class="alert alert-danger" style="margin-bottom: 0;">
                        <p><strong>¿Realmente desea eliminar al interesado?</strong></p>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="btnConfirmar">Confirmar</button>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
