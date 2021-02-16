<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistroTesis.aspx.vb"
    Inherits="GestionInvestigacion_FrmRegistroTesis" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Cargamos css -->
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <!-- Cargamos JS -->

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <%--<script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>--%>

    <script type="text/javascript" src='../assets/js/materialize.js'></script>

    <%--<script type="text/javascript" src='../assets/js/form-elements.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/select2.js'></script>--%>

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js?X=2'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>

    <script src="js/_General.js?x=2" type="text/javascript"></script>

    <script src="js/Tesis.js?m=23n" type="text/javascript"></script>

    <title>Registro de Tesis</title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .content .main-content
        {
            padding-right: 18px;
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
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
            text-align: left;
        }
        .modal
        {
            overflow: auto !important;
        }
        .input-group .form-control
        {
            z-index: 0;
        }
        #tbObservaciones tbody tr td, #tbObservacionesInforme tbody tr td
        {
            padding: 0px;
            margin: 0px;
            font-size: 11px;
        }
        #tbObservaciones tbody tr th, #tbObservacionesInforme tbody tr th
        {
            padding: 2px;
            margin: 0px;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
            color: White;
            background-color: #E33439;
        }
    </style>
</head>
<body class="">
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="overlay">
            </div>
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Tesis</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <%--          <button class="btn btn-green" id="btnAgregar" value="Agregar" onclick="fnNuevoConcurso();">
                                                    Agregar</button>--%>
                                                <div class="row">
                                                </div>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscarAlumno" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                            <label id="lblSemestre" class="control-label col-sm-1 col-md-1">
                                                Semestre</label>
                                            <div class="col-sm-2 col-md-2">
                                                <select id="cboSemestre" name="cboSemestre" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                            <label id="Label1" class="control-label col-sm-1 col-md-1">
                                                Etapa</label>
                                            <div class="col-sm-2 col-md-2">
                                                <select name="cboEtapa" class="form-control" id="cboEtapa">
                                                    <option value="">-- Seleccione -- </option>
                                                    <option value="P">PROYECTO</option>
                                                    <option value="E">EN EJECUCIÓN</option>
                                                    <option value="I">INFORME</option>
                                                </select>
                                            </div>
                                            <label id="Label3" class="control-label col-sm-2 col-md-2">
                                                Carrera Profesional</label>
                                            <div class="col-sm-4 col-md-4">
                                                <select name="cboCarrera" class="form-control" id="cboCarrera">
                                                    <option value="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label id="Label2" class="control-label col-sm-1 col-md-1">
                                                Curso/Grupo
                                            </label>
                                            <div class="col-sm-8 col-md-9">
                                                <select id="cboCurso" name="cboCurso" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                            <div class="col-sm-3 col-md-2">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar">
                                                    Consultar</button>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="Lista">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Alumnos
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form class="form form-horizontal" id="frmLista" onsubmit="return false;" action="#"
                            method="post">
                            </form>
                            <div class="table-responsive">
                                <div id="tAlumnos_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tAlumnos" name="tAlumnos" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="4%" style="font-weight: bold; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <%-- <td width="27%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Curso
                                                </td>
                                                <td width="6%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Grupo
                                                </td>--%>
                                                <td width="12%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Código Universitario
                                                </td>
                                                <td width="45%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Apellidos y Nombres
                                                </td>
                                                <td width="14%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending"
                                                    id="colActa">
                                                    Nota Acta Sustentación
                                                </td>
                                                <td width="12%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending"
                                                    id="colAsesor">
                                                </td>
                                                <%-- <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Estado
                                                </td>--%>
                                                <td width="13%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
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
                                        <tbody id="tbAlumnos">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="RegistroTabs" style="display: none;">
                    <div class="panel panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title" id="TituloPanel">
                                ## - ####
                                <%--             <span class="panel-options"><a href="#" class="panel-refresh">
                                    <i class="icon ti-reload"></i></a><a href="#" class="panel-minimize"><i class="icon ti-angle-up">
                                    </i></a><a href="#" class="panel-close"><i class="icon ti-close"></i></a>
                                    </span>--%>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div role="tabpanel">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist">
                                    <li role="presentation" class="active" id="TabProyecto"><a href="#hometabnb" aria-controls="home"
                                        role="tab" data-toggle="tab" id="Tab1">Proyecto</a></li>
                                    <li role="presentation" id="TabEjecucion"><a href="#profiletabnb" aria-controls="profile"
                                        role="tab" data-toggle="tab" id="Tab2">Ejecución</a></li>
                                    <li role="presentation" id="TabInforme"><a href="#messagestabnb" aria-controls="messages"
                                        role="tab" data-toggle="tab" id="Tab3">Informe</a></li>
                                    <li role="presentation" id="TabSustentacion"><a href="#settingstabnb" aria-controls="settings"
                                        role="tab" data-toggle="tab" id="Tab4">Sustentación</a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content piluku-tab-content">
                                    <div role="tabpanel" class="tab-pane active" id="hometabnb">
                                        <form enctype="multipart/form-data" id="frmRegistro" name="frmRegistro">
                                        <input type="hidden" id="hdcodA" name="hdcodA" value="0" />
                                        <input type="hidden" id="hdValidaJur" name="hdValidaJur" value="0" />
                                        <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label" for="txttituloPos">
                                                    Etapa de Tesis:</label>
                                                <div class="col-sm-6">
                                                    <select name="cbotipoInvestigacion" class="form-control" id="cbotipoInvestigacion">
                                                        <option value="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                                <%--<div class="col-md-3"><input type="button" class="btn btn-green" id="btnObjetivos" name="btnObjetivos" value="Objetivos" /></div>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label" for="txttituloPos">
                                                    Título:</label>
                                                <div class="col-sm-9">
                                                    <%--<input type="" id="txtTitulo" name="txtTitulo" class="form-control" />--%>
                                                    <textarea id="txtTitulo" name="txtTitulo" class="form-control" rows="3"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                </label>
                                                <div class="col-md-1">
                                                    <%-- Se oculta Boton pero funcionalidad Sigue utilizandose, ahora solo se guarda el estudiante como unico autor--%>
                                                    <button type="button" id="btnAutor" name="btnAutor" class="btn btn-primary" style="display: none">
                                                        Autor(es) &nbsp;&nbsp;<i class="ion-android-people"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Facultad:</label>
                                                <label class="col-sm-2 control-label" style="font-weight: bold;" id="lblFacultad">
                                                </label>
                                                <label class="col-sm-2 control-label">
                                                    Escuela Profesional:</label>
                                                <label class="col-sm-5 control-label" style="font-weight: bold;" id="lblCarrera">
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Línea de Investigación USAT:</label>
                                                <%--<label class="col-sm-9 control-label" style="font-weight: bold;" id="lblLinea">
                                                </label>--%>
                                                <div class="col-sm-9">
                                                    <select name="cboLinea" class="form-control" id="cboLinea">
                                                        <option value="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-xs-4 col-sm-3 control-label">
                                                    Asignar Línea OCDE</label>
                                                <div class="col-xs-8 col-md-8">
                                                    <input type="checkbox" value="0" id="chkOCDE" name="chkOCDE" />
                                                </div>
                                            </div>
                                        </div>
                                        <div id="ocde" style="display: none">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-xs-3 col-sm-3 control-label">
                                                        Área Temática:</label>
                                                    <div class="col-xs-8 col-md-8">
                                                        <select id="cboArea" name="cboArea" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-xs-3 col-sm-3 control-label">
                                                        Sub Área:</label>
                                                    <div class="col-xs-8 col-md-8">
                                                        <select id="cboSubArea" name="cboSubArea" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-xs-3 col-sm-3 control-label">
                                                        Disciplina:</label>
                                                    <div class="col-xs-8 col-md-8">
                                                        <select id="cboDisciplina" name="cboDisciplina" class="form-control">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                </label>
                                                <div class="col-md-1">
                                                    <button type="button" id="btnObjetivos" name="btnObjetivos" class="btn btn-success">
                                                        Objetivos &nbsp;&nbsp;<i class="ion-ios-compose"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Fecha Inicio:</label>
                                                <div class="col-sm-3" id="date-popup-group">
                                                    <div class="input-group date" id="FechaInicio">
                                                        <input name="txtfeciniTes" class="form-control" id="txtfeciniTes" style="text-align: right;"
                                                            type="text" placeholder="__/__/____" data-provide="datepicker" disabled="disabled">
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                        </i></span>
                                                    </div>
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Fecha Fin:</label>
                                                <div class="col-sm-3" id="date-popup-group">
                                                    <div class="input-group date" id="FechaFin">
                                                        <input name="txtfecfinTes" class="form-control" id="txtfecfinTes" style="text-align: right;"
                                                            type="text" placeholder="__/__/____" data-provide="datepicker">
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                        </i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-md-3 control-label" for="cboFinanciamiento">
                                                    Financiamiento:</label>
                                                <div class="col-xs-2 col-md-2">
                                                    <input type="checkbox" name="chkPropio" id="chkPropio" style="display: inline-block">
                                                    <label for="chkPropio" style="color: Black; font-size: 13px">
                                                        &nbsp; Propio</label>
                                                </div>
                                                <div class="col-xs-2 col-md-2">
                                                    <input type="checkbox" name="chkUsat" id="chkUsat" style="display: inline-block">
                                                    <label for="chkUsat" style="color: Black; font-size: 13px">
                                                        &nbsp; USAT</label>
                                                </div>
                                                <div class="col-xs-2 col-md-2">
                                                    <input type="checkbox" name="chkExterno" id="chkExterno" style="display: inline-block">
                                                    <label for="chkExterno" style="color: Black; font-size: 13px">
                                                        &nbsp; Externo</label>
                                                </div>
                                                <div class="col-xs-3 col-sm-3">
                                                    <input type="text" id="txtusat" name="txtusat" class="form-control" maxlength="200"
                                                        placeholder="Ingrese Financiamiento USAT">
                                                    <input type="text" id="txtexterno" name="txtexterno" class="form-control" maxlength="200"
                                                        placeholder="Ingrese Financiamiento Externo">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Presupuesto:</label>
                                                <div class="col-sm-2">
                                                    <input type="text" id="txtpresupuesto" name="txtpresupuesto" class="form-control">
                                                </div>
                                                <label class="col-sm-1 control-label">
                                                    Avance:</label>
                                                <div class="col-sm-1">
                                                    <input type="text" id="txtavance" name="txtavance" class="form-control" value="40"
                                                        readonly="readonly" />
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Proyecto de Tesis Final (PDF):</label>
                                                <div class="col-sm-3">
                                                    <input type="file" id="fileproyecto" name="file_proyecto">
                                                </div>
                                                <div id="file_proyecto" class="col-sm-2">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <%--<label class="col-sm-3 control-label">
                                                    Adjuntar Informe de Similitud (Sotfware Antiplagio):</label>--%>
                                                <label class="col-sm-3 control-label">
                                                    Reporte de Similitud (PDF):</label>
                                                <div class="col-sm-5">
                                                    <input type="file" id="filesimilitudProyecto" name="filesimilitudProyecto">
                                                    <div id="file_similitudProyecto">
                                                    </div>
                                                </div>
                                                <label class="col-sm-3 control-label">
                                                    Nota Final de la asignatura:</label>
                                                <div class="col-sm-1">
                                                    <input type="text" id="txtNotaFinal" name="txtNotaFinal" class="form-control" value=""
                                                        readonly="readonly" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-12 label-danger" style="color: White; font-weight: 400">
                                                    Acta de sustentación y Observacíones</label>
                                            </div>
                                        </div>
                                        <div class="row" id="filaAsignarJurado">
                                            <div class="form-group">
                                                <label class="col-xs-2 col-sm-3 control-label">
                                                </label>
                                                <%--  <div class="col-md-2">
                                                    <button type="button" id="btnAsignarAsesor" name="btnAsignarAsesor" class="btn btn-orange">
                                                        Asignar Asesor(es)&nbsp;&nbsp;<i class="ion-man"></i>
                                                    </button>
                                                </div>
                                                <div class="col-md-1">
                                                </div>--%>
                                                <div class="col-xs-3 col-md-2">
                                                    <button type="button" id="btnAsignarJurado" name="btnAsignarJurado" class="btn btn-info">
                                                        Asignar Jurados&nbsp;&nbsp;<i class="ion-man"></i>
                                                    </button>
                                                </div>
                                                <label class="col-xs-2 col-sm-2 control-label">
                                                    Fecha de Sustentación:</label>
                                                <div class="col-xs-3 col-sm-3" id="Div15">
                                                    <div class="input-group date" id="Div16">
                                                        <input name="txtFechaSustentacionP" class="form-control" id="txtFechaSustentacionP"
                                                            style="text-align: right;" type="text" placeholder="__/__/____" data-provide="datepicker">
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I2"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="ActaAprobacion">
                                            <div class="form-group">
                                                <label class="col-xs-2 col-sm-3 control-label">
                                                    Acta de Aprobación:</label>
                                                <div class="col-xs-3 col-md-2">
                                                    <a class="btn btn-green" onclick="GenerarActa('P')">Generar Acta</a></div>
                                                <div id="SubirActa">
                                                    <label class="col-xs-3 col-sm-3 control-label">
                                                        Adjuntar Acta de Aprobación (PDF):</label>
                                                    <div class="col-xs-3 col-sm-3">
                                                        <input type="file" id="fileacta" name="file_acta">
                                                    </div>
                                                    <div id="file_acta" class="col-sm-2">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="filaNotaJurado">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Nota de sustentación:</label>
                                                <div class="col-sm-1">
                                                    <input type="text" id="txtNotaSustentacionP" name="txtNotaSustentacionP" class="form-control">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <button class="btn btn-danger collapsed" type="button" data-toggle="collapse" data-target="#collapseProyecto"
                                                    aria-expanded="false" aria-controls="collapseProyecto" id="btnObservacionesProyecto"
                                                    style="display: none">
                                                    Observaciones de docente
                                                </button>
                                                <div class="collapse" id="collapseProyecto" aria-expanded="false" style="height: 0px;">
                                                    <div>
                                                        <table id="tbObservaciones" class="table table-striped" style="width: 100%; padding: 0;
                                                            color: Black">
                                                            <tbody id="ObservacionesProyecto">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="DivGuardar">
                                            <center>
                                                <button type="button" id="btnGuardarTesis" class="btn btn-primary">
                                                    Guardar</button>
                                                <button type="button" id="btnCancelarProyecto" class="btn btn-danger" onclick="OcultarRegistro()">
                                                    Regresar</button>
                                            </center>
                                        </div>
                                        </form>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="profiletabnb">
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Fecha aprobación acta:</label>
                                                <div class="col-sm-3" id="Div10">
                                                    <div class="input-group date" id="FecActa">
                                                        <input name="txtFecActa" class="form-control" id="txtFecActa" style="text-align: right;"
                                                            type="text" placeholder="__/__/____" data-provide="datepicker">
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I1"></i>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="row">
                                            <label class="col-xs-3 col-md-3 control-label">
                                                Asesor
                                            </label>
                                            <div class="col-xs-6 col-md-6">
                                                <select id="cboAsesor" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                            <div class="col-xs-1 col-md-1">
                                                <label class="label medium label-primary" id="lblNotaEjecucion" style="color: White;
                                                    font-weight: 400; font-size: 12px">
                                                </label>
                                            </div>
                                            <div class="col-xs-2 col-md-2">
                                                <label class="label medium label-success" id="lblPorcentajeEjecucion" style="color: White;
                                                    font-weight: 400; font-size: 12px">
                                                </label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Avance de ejecución de tesis (PDF):</label>
                                                <div class="col-sm-4">
                                                    <input type="file" id="filepreinforme" name="filepreinforme" />
                                                    <div id="file_preinforme">
                                                    </div>
                                                </div>
                                                <label class="col-sm-2 control-label">
                                                    Nota Final</label>
                                                <div class="col-sm-1">
                                                    <input type="text" id="txtNotaFinalPreInforme" name="txtNotaFinalPreInforme" class="form-control"
                                                        value="" readonly="readonly">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="Div1">
                                            <center>
                                                <button type="button" id="btnGuardarPreinforme" class="btn btn-primary" style="display: inline-block">
                                                    Guardar</button>
                                                <button type="button" id="btnCancelarPreinforme" class="btn btn-danger" onclick="OcultarRegistro()">
                                                    Regresar</button>
                                            </center>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="messagestabnb">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-xs-3 col-sm-3 control-label">
                                                    Informe Final (PDF,RAR):<br />
                                                    Tamaño máximo: 45MB
                                                </label>
                                                <div class="col-xs-4 col-sm-4">
                                                    <input type="file" id="fileinforme" name="fileinforme" />
                                                    <div id="file_informe">
                                                    </div>
                                                </div>
                                                <label class="col-xs-2 col-sm-1 control-label">
                                                    Nota Final</label>
                                                <div class="col-xs-2 col-sm-1">
                                                    <input type="text" id="Text2" name="txtNotaFinal" class="form-control" value="" readonly="readonly">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Link Informe Final :
                                                </label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="txtLinkInforme" name="txtLinkInforme" class="form-control"
                                                        runat="server" maxlength="500" />
                                                    <span style="color: Red">Longitud máxima 500 carácteres </span>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="JuradoInforme">
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-xs-3 col-sm-3 control-label">
                                                        Adjuntar Informe de Similitud (Sotfware Antiplagio)(PDF,RAR):<br />
                                                        Tamaño máximo: 25MB
                                                    </label>
                                                    <div class="col-xs-3 col-sm-3">
                                                        <input type="file" id="filesimilitud" name="filesimilitud" />
                                                        <div id="file_similitud">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-xs-12 col-sm-12 label-danger" style="color: White; font-weight: 400">
                                                        Acta de sustentación y Observaciones</label>
                                                </div>
                                            </div>
                                            <div class="row" id="filaAsignarJuradoInforme">
                                                <div class="form-group">
                                                    <label class="col-xs-3 col-md-3 control-label">
                                                    </label>
                                                    <div class="col-xs-2 col-md-2">
                                                        <button type="button" id="btnJuradoInforme" name="btnJuradoInforme" class="btn btn-orange">
                                                            Asignar Jurado &nbsp;&nbsp;<i class="ion-man"></i>
                                                        </button>
                                                    </div>
                                                    <label class="col-xs-2 col-md-2 control-label">
                                                        Fecha de Sustentación:</label>
                                                    <div class="col-xs-3 col-md-3" id="Div17">
                                                        <div class="input-group date" id="Div18">
                                                            <input name="txtFechaSustentacionI" class="form-control" id="txtFechaSustentacionI"
                                                                style="text-align: right;" type="text" placeholder="__/__/____" data-provide="datepicker">
                                                            <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I3"></i>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <label class="col-xs-3 col-md-3 control-label">
                                                        Acta de Aprobación:</label>
                                                    <div class="col-sm-3">
                                                        <a class="btn btn-green" id="btnGenerarActaI" onclick=" GenerarActa('I');">Generar Acta</a>
                                                    </div>
                                                    <div id="SubirActaInforme">
                                                        <label class="col-xs-3 col-sm-3 control-label">
                                                            Adjuntar Acta de Aprobación(PDF):</label>
                                                        <div class="col-xs-3 col-sm-3">
                                                            <input type="file" id="fileactainforme" name="fileactainforme" />
                                                            <div id="file_actainforme">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="filaNotaJuradoInforme">
                                                <div class="form-group">
                                                    <label class="col-sm-3 control-label">
                                                        Nota de sustentación:</label>
                                                    <div class="col-sm-1">
                                                        <input type="text" id="txtNotaSustentacionI" name="txtNotaSustentacionI" class="form-control">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <button class="btn btn-danger collapsed" type="button" data-toggle="collapse" data-target="#collapseInforme"
                                                    aria-expanded="false" aria-controls="collapseInforme" id="btnObservacionesInforme"
                                                    style="display: none">
                                                    Observaciones
                                                </button>
                                                <div class="collapse" id="collapseInforme" aria-expanded="false" style="height: 0px;">
                                                    <div>
                                                        <table id="tbObservacionesInforme" class="table table-striped" style="width: 100%;
                                                            padding: 0; color: Black">
                                                            <tbody id="ObservacionesInforme">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="Div2">
                                            <center>
                                                <button type="button" id="btnGuardarInforme" class="btn btn-primary" style="display: inline-block">
                                                    Guardar</button>
                                                <button type="button" id="btnCancelarInforme" class="btn btn-danger" onclick="OcultarRegistro()">
                                                    Regresar</button>
                                            </center>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane" id="settingstabnb">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Grado de Bachiller:</label>
                                                <div class="col-sm-3">
                                                    <input type="file" id="file4" name="file_acta" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Acta de Sustentación:</label>
                                                <div class="col-sm-3">
                                                    <input type="file" id="file5" name="file_acta" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Permiso de Publicación:</label>
                                                <div class="col-sm-3">
                                                    <input type="file" id="file6" name="file_acta" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Tesis(PDF):</label>
                                                <div class="col-sm-3">
                                                    <input type="file" id="file7" name="file_acta" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                </label>
                                                <div class="col-md-2">
                                                    <button type="button" id="btnJuradoSustentacion" name="btnJuradoSustentacion" class="btn btn-orange">
                                                        Asignar Jurado de Sustentación&nbsp;&nbsp;<i class="ion-man"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row" id="Div3">
                                            <center>
                                                <button type="button" id="Button9" class="btn btn-primary" style="display: inline-block">
                                                    Guardar</button>
                                                <button type="button" id="Button10" class="btn btn-danger" onclick="OcultarRegistro()">
                                                    Regresar</button>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- =================================== MODAL AUTOR(ES) ===============================================--%>
                <div class="row">
                    <div class="modal fade" id="mdAutor" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div4">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H1">
                                        Autor(es)</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmEquipo" name="frmEquipo" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Alumno:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtAutor" name="txtAutor" class="form-control ui-autocomplete-input"
                                                    autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Tipo Autor:</label>
                                            <div class="col-sm-5">
                                                <select id="cboTipoAutor" name="cboTipoAutor" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarAutor" name="btnAgregarAutor" class="btn btn-success">
                                                Agregar
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;
                                                    width: 100%">
                                                    <div id="Div7" class="dataTables_wrapper" role="grid">
                                                        <table id="tAutor" name="tAutor" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td style="font-weight: bold; text-align: center; width: 5%" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        N°
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 15%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Codigo Universitario
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 50%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Alumno
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 20%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Rol
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="5" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbAutor">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarAutor">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- ================================= FIN MODAL AUTOR(ES) ===============================================--%>
                <%-- =================================== MODAL OBJETIVO(S) ===============================================--%>
                <div class="row">
                    <div class="modal fade" id="mdObjetivos" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div5">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H2">
                                        Objetivos de Proyecto</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmObjetivos" name="frmObjetivos" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtobjetivo">
                                                Descripción:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtobjetivo" name="txtobjetivo" class="form-control ui-autocomplete-input"
                                                    autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="cboTipoObjetivo">
                                                Tipo:</label>
                                            <div class="col-sm-5">
                                                <select id="cboTipoObjetivo" name="cboTipoObjetivo" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                    <option value="1">OBJETIVO GENERAL</option>
                                                    <option value="2">OBJETIVO ESPECÍFICO</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarObjetivo" name="btnAgregarObjetivo" class="btn btn-success">
                                                Agregar Objetivo
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                    <div id="Div6" class="dataTables_wrapper" role="grid">
                                                        <table id="tObjetivos" name="tObjetivos" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <%--          <td width="5%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        N°--%>
                                                                    </td>
                                                                    <td width="70%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Objetivo
                                                                    </td>
                                                                    <td width="20%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Tipo
                                                                    </td>
                                                                    <td width="10%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="4" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbObjetivos">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarObj">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- =================================== FIN MODAL OBJETIVO(S) ===============================================--%>
                <%-- =================================== MODAL ASESOR(ES) ===========================================--%>
                <div class="row">
                    <div class="modal fade" id="mdAsignarAsesor" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div9">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H3">
                                        Asignar Asesor(es)</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="Form1" name="frmEquipo" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Departamento:</label>
                                            <div class="col-sm-7">
                                                <select id="cboDepartamento" name="cboDepartamento" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Docente:</label>
                                            <div class="col-sm-7">
                                                <select id="cboDocenteAsesor" name="cboDocenteAsesor" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Rol:</label>
                                            <div class="col-sm-5">
                                                <select id="cboRolAsesor" name="cboRolAsesor" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarAsesor" name="btnAgregarAsesor" class="btn btn-success">
                                                Agregar Asesor
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                    <div id="Div8" class="dataTables_wrapper" role="grid">
                                                        <table id="tAsesor" name="tAsesor" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td style="font-weight: bold; text-align: center; width: 5%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        N°
                                                                    </td>
                                                                    <td style="font-weight: bold; width: 65%" class="sorting" tabindex="0" rowspan="1"
                                                                        colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Docente
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 20%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Rol
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="4" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbAsesor">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarAsesor">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- =================================== FIN MODAL ASESOR(ES) ===========================================--%>
                <%-- =================================== MODAL JURADO(S) ===========================================--%>
                <div class="row">
                    <div class="modal fade" id="mdAsignarJurado" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div11">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H4">
                                        Asignar Jurado(s)</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="Form2" name="frmEquipo" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Departamento:</label>
                                            <div class="col-sm-7">
                                                <select id="cboDepartamentoJurado" name="cboDepartamentoJurado" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Docente:</label>
                                            <div class="col-sm-7">
                                                <select id="cboDocenteJurado" name="cboDocenteJurado" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Rol:</label>
                                            <div class="col-sm-5">
                                                <select id="cboRolJurado" name="cboRolJurado" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarJurado" name="btnAgregarJurado" class="btn btn-success">
                                                Agregar Jurado
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                    <div id="Div12" class="dataTables_wrapper" role="grid">
                                                        <table id="tJurado" name="tJurado" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td style="font-weight: bold; text-align: center; width: 5%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        N°
                                                                    </td>
                                                                    <td style="font-weight: bold; width: 60%" class="sorting" tabindex="0" rowspan="1"
                                                                        colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Docente
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 15%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Rol
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Aprueba Director
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="5" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbJurado">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarJurado">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- -->
                <%-- =================================== MODAL JURADO(S) ===========================================--%>
                <div class="row">
                    <div class="modal fade" id="mdAsignarJuradoInforme" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div13">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H5">
                                        Asignar Jurado(s)</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="Form3" name="frmEquipo" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="cboDepartamentoJuradoInforme">
                                                Departamento:</label>
                                            <div class="col-sm-7">
                                                <select id="cboDepartamentoJuradoInforme" name="cboDepartamentoJuradoInforme" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="cboDocenteJuradoInforme">
                                                Docente:</label>
                                            <div class="col-sm-7">
                                                <select id="cboDocenteJuradoInforme" name="cboDocenteJuradoInforme" class="form-control">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="cboRolJuradoInforme">
                                                Rol:</label>
                                            <div class="col-sm-5">
                                                <select id="cboRolJuradoInforme" name="cboRolJuradoInforme" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarJuradoInforme" name="btnAgregarJuradoInforme"
                                                class="btn btn-success">
                                                Agregar Jurado
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                    <div id="Div14" class="dataTables_wrapper" role="grid">
                                                        <table id="tJuradoInforme" name="tJuradoInforme" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td style="font-weight: bold; text-align: center; width: 5%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        N°
                                                                    </td>
                                                                    <td style="font-weight: bold; width: 60%" class="sorting" tabindex="0" rowspan="1"
                                                                        colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Docente
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 15%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Rol
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Aprueba Director
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="4" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbJuradoInforme">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarJuradoInforme">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- -->
                <%-- =================================== MODAL OBSERVACION(ES) ===============================================--%>
                <div class="row">
                    <div class="modal fade" id="mdObservacion" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div19">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H6">
                                        Observaciones</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmObservaciones" name="frmObservaciones" enctype="multipart/form-data"
                                    class="form-horizontal" method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Observación:</label>
                                            <div class="col-sm-5">
                                                <input type="hidden" value="0" id="hdoTes" name="hdoTes" />
                                                <input type="hidden" value="0" id="hdoAlu" name="hdoAlu" />
                                                <input type="hidden" value="0" id="hdoEtapa" name="hdoEtapa" />
                                                <textarea id="txtObservacion" name="txtObservacion" rows="4" cols="80"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarObservacion" name="btnAgregarObservacion" class="btn btn-success">
                                                Agregar
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;
                                                    width: 100%">
                                                    <div id="Div20" class="dataTables_wrapper" role="grid">
                                                        <table id="tObservacionesDocente" name="tObservacionesDocente" class="display dataTable"
                                                            width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td style="font-weight: bold; text-align: center; width: 5%" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        N°
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 65%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Observación
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Fecha Registro
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Resuelto
                                                                    </td>
                                                                    <td style="font-weight: bold; text-align: center; width: 10%" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="5" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="tbObservacionesDocente">
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="btnCancelarObservaciones" data-dismiss="modal"
                                            aria-label="Close">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- ================================= FIN MODAL OBSERVACION(ES) ===============================================--%>
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
