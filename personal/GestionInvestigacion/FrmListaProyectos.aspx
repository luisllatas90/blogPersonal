<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaProyectos.aspx.vb"
    Inherits="GestionInvestigacion_FrmListaProyectos" %>

<!DOCTYPE html>
<html>
<head>
    <title>Lista de Proyectos</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <!-- Cargamos css -->
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Cargamos JS -->

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>

    <script src="js/_General.js?x=5" type="text/javascript"></script>

    <script src="js/Proyecto.js?x=6" type="text/javascript"></script>

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
    </style>
</head>
<body>
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
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Proyectos
                                                de Investigación</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar" onclick="fnListarProyecto()">
                                                    Consultar</button>
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar" onclick="NuevoProyecto();">
                                                    Agregar</button>
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <label class="col-md-4 control-label ">
                                                    Docente / Administrativo</label>
                                                <div class="col-md-8">
                                                    <select name="cboPersonal" class="form-control" id="cboPersonal">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <label class="col-md-4 control-label ">
                                                    Estado de Proyecto</label>
                                                <div class="col-md-8">
                                                    <select name="cboEstado" class="form-control" id="cboEstado">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                        <option value="1">REGISTRO</option>
                                                        <option value="2">EN EVALUACIÓN</option>
                                                        <option value="3">OBSERVADOS</option>
                                                        <option value="4">RECHAZADOS</option>
                                                        <option value="5">APROBADOS</option>
                                                        <option value="6">TODOS</option>
                                                    </select>
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
                <div class="row">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Proyectos de Investigación
                                <%--                                <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload"
                                    onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize" href="#"><i
                                        class="icon ti-angle-up"></i></a></span>--%>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form class="form form-horizontal" id="frmLista" onsubmit="return false;" action="#"
                            method="post">
                            </form>
                            <div class="table-responsive">
                                <div id="tProyectos_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tProyectos" name="tProyectos" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="50%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Titulo
                                                </td>
                                                <td width="11%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Fecha Inicio
                                                </td>
                                                <td width="11%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Fecha Fin
                                                </td>
                                                <td width="12%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Estado
                                                </td>
                                                <%--                                                <td width="10%" style="font-weight: bold; width: 226px; text-align: center;" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                    Fecha Fin
                                                </td>--%>
                                                <%--                                                <td width="7%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                    Estado
                                                </td>--%>
                                                <td width="11%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                        <tbody id="tbProyectos">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="modalReg">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Proyecto de Investigación</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div id="DivObservaciones">
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Título:</label>
                                            <div class="col-sm-8">
                                                <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                                <input type="hidden" id="hdcodTin" name="hdcodTin" value="4" />
                                                <input type="text" id="txttitulo" name="txttitulo" class="form-control" maxlength="800" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                            </label>
                                            <div class="col-md-1">
                                                <button type="button" id="btnObjetivos" name="btnObjetivos" class="btn btn-green">
                                                    Objetivos &nbsp;&nbsp;<i class="ion-ios-compose"></i>
                                                </button>
                                            </div>
                                            <div class="col-md-1">
                                            </div>
                                            <div class="col-md-1">
                                                <button type="button" id="btnEquipo" name="btnEquipo" class="btn btn-info">
                                                    Grupo de Investigación &nbsp;&nbsp;<i class="ion-university"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Linea de Investigación USAT:</label>
                                            <div class="col-md-8">
                                                <select id="cboLinea" name="cboLinea" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Asignar Linea OCDE</label>
                                            <div class="col-md-8">
                                                <input type="checkbox" value="0" id="chkOCDE" name="chkOCDE" />
                                            </div>
                                        </div>
                                    </div>
                                    <div id="ocde" style="display: none">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Área Temática:</label>
                                                <div class="col-md-8">
                                                    <select id="cboArea" name="cboArea" class="form-control">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Sub Área:</label>
                                                <div class="col-md-8">
                                                    <select id="cboSubArea" name="cboSubArea" class="form-control">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Disciplina:</label>
                                                <div class="col-md-8">
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
                                                Fecha Inicio:</label>
                                            <div class="col-sm-3" id="date-popup-group">
                              
                                                <div class="input-group date" id="FechaInicio">
                                                    <input name="txtfecini" class="form-control" id="txtfecini" style="text-align: right;" autocomplete="off"
                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                    </i></span>
                                                </div>
                                            </div>
                                            <label class="col-sm-2 control-label">
                                                Fecha Fin:</label>
                                            <div class="col-sm-3" id="date-popup-group">
                                                <div class="input-group date" id="FechaFin">
                                                    <input name="txtfecfin" class="form-control" id="txtfecfin" style="text-align: right;" autocomplete="off"
                                                        type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                    <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecFinal">
                                                    </i></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <%--                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="cboFinanciamiento">
                                                Financiamiento:</label>
                                            <div class="col-sm-3">
                                                <select class="form-control" id="cboFinanciamiento" name="cboFinanciamiento">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                    <option value="P">PROPIO</option>
                                                    <option value="U">USAT</option>
                                                    <option value="E">EXTERNO</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label" for="cboFinanciamiento">
                                                Financiamiento:</label>
                                            <div class="col-md-2">
                                                <input type="checkbox" name="chkPropio" id="chkPropio" style="display: inline-block">
                                                <label for="chkPropio" style="color: Black; font-size: 13px" style="display: inline-block">
                                                    &nbsp; Propio</label>
                                            </div>
                                            <div class="col-md-2">
                                                <input type="checkbox" name="chkUsat" id="chkUsat" style="display: inline-block">
                                                <label for="chkUsat" style="color: Black; font-size: 13px" style="display: inline-block">
                                                    &nbsp; USAT</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-md-3">
                                            </div>
                                            <div class="col-md-2">
                                                <input type="checkbox" name="chkExterno" id="chkExterno" style="display: inline-block">
                                                <label for="chkExterno" style="color: Black; font-size: 13px" style="display: inline-block">
                                                    &nbsp; Externo</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <input type="text" id="txtexterno" name="txtexterno" class="form-control" maxlength="200" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Presupuesto:</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtpresupuesto" name="txtpresupuesto" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <%--                                   <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Presupuesto(Excel):</label>
                                            <div class="col-sm-8">
                                                <input type="file" id="filepto" name="filepto" />
                                                <div id="file_pto">
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtavance">
                                                Avance(%):</label>
                                            <div class="col-sm-2">
                                                <input type="text" id="txtavance" name="txtavance" class="form-control" maxlength="6" />
                                            </div>
                                            <label class="col-sm-3 control-label" for="txtavance">
                                                Estado de Avance:</label>
                                            <div class="col-sm-3">
                                                <select id="cboAvance" name="cboAvance" class="form-control">
                                                    <option value="" selected="selected">-- Seleccione --</option>
                                                    <option value="0">EN EJECUCIÓN</option>
                                                    <option value="1">CULMINADO</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Informe(PDF)</label>
                                            <div class="col-sm-8">
                                                <input type="file" id="fileinforme" name="fileinforme" />
                                                <div id="file_informe">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <div id="DivGuardar">
                                        <center>
                                            <button type="button" id="btnA" class="btn btn-primary" onclick="fnGuardarProyecto();">
                                                Guardar</button>
                                            <button type="button" id="btnCancelarReg" class="btn btn-danger" onclick="fnCancelar();">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                    <center>
                                        <div class="alert alert-success" id="MensajeGuardar" style="display: none;">
                                        </div>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdEquipo" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div3">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H1">
                                        Grupo de Investigación</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmEquipo" name="frmEquipo" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Docente/Administrativo:</label>
                                            <div class="col-sm-7">
                                                <input type="text" id="txtPersonal" name="txtPersonal" class="form-control ui-autocomplete-input"
                                                    autocomplete="off" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txttitulo">
                                                Rol:</label>
                                            <div class="col-sm-5">
                                                <select id="cboRol" name="cboRol" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregarEquipo" name="btnAgregarEquipo" class="btn btn-success">
                                                Agregar Integrante
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;
                                                    width: 100%">
                                                    <div id="Div2" class="dataTables_wrapper" role="grid">
                                                        <table id="tEquipo" name="tEquipo" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="5%" style="font-weight: bold; text-align: center" class="sorting_asc"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                        N°
                                                                    </td>
                                                                    <td width="50%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Integrante
                                                                    </td>
                                                                    <td width="35%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                        Rol
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
                                                            <tbody id="tbEquipo">
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
                                        <button type="button" class="btn btn-danger" id="btnCancelarEq">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
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
                                                    autocomplete="off" maxlength="1600" />
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
                                                    <option value="1">GENERAL</option>
                                                    <option value="2">ESPECÍFICO</option>
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
                                                                    <th colspan="3" rowspan="1">
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
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
