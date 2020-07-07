<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaEgresado.aspx.vb"
    Inherits="GradosYTitulos_FrmListaEgresado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="es">
<head>
    <title id='Description'>Egresado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <link rel="stylesheet" href="../assets/jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../assets/jqwidgets/jqwidgets/styles/jqx.light.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcore.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdata.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxbuttons.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxscrollbar.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxlistbox.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdropdownlist.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxmenu.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxinput.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.filter.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.sort.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.selection.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxpanel.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/globalization/globalize.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcalendar.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdatetimeinput.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcheckbox.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.pager.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdata.export.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.export.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxtooltip.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.columnsresize.js"></script>

    <%--  <script type="text/javascript" src="../assets/jqwidgets/scripts/demos.js"></script>--%>
    <%-- <script type="text/javascript" src="js/generatedata.js"></script>--%>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src='../assets/js/bootstrap-datepicker.js'></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>

    <script src="js/_General.js?x=2" type="text/javascript"></script>

    <script src="js/Egresado.js?x=0" type="text/javascript"></script>

    <%--<script src="js/ExportarCVS.js" type="text/javascript"></script>--%>
    <%--<script src="js/JqxTable.js" type="text/javascript"></script>--%>
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
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }
        #columntablejqxgrid
        {
            z-index: 10;
        }
        #DatosPersonales1, #DatosPersonales2, #DatosPersonales3, #DatosPersonales4, #DatosPersonales5, #DatosPersonales6, #DatosPersonales7, #ApellidosNombres
        {
            font-size: 13px;
        }
        .list-inline > li
        {
            font-weight: bold;
        }
        .list-inline > li p
        {
            font-weight: normal;
        }
    </style>
</head>
<body class=''>
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Listado
                                                de Egresados</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar">
                                                    Consultar</button>
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar" onclick="window.location.href='FrmRegistrarExpediente.aspx'">
                                                    Agregar</button>
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-5">
                                                <label class="col-md-3 control-label ">
                                                    Tipo Estudio</label>
                                                <div class="col-md-9">
                                                    <select name="cboTipoEstudio" class="form-control" id="cboTipoEstudio">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-7">
                                                <label class="col-md-3 control-label ">
                                                    Carrera Profesional</label>
                                                <div class="col-md-9">
                                                    <select name="cboCarrera" class="form-control" id="cboCarrera">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-5">
                                                <label class="col-md-3 control-label ">
                                                    Buscar</label>
                                                <div class="col-md-9">
                                                    <input type="text" id="txtbusqueda" name="txtbusqueda" class="form-control" placeholder="DNI/Cod. Univ/N° Exp./Apellidos y Nombres" />
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
            <div class="row" id="Lista">
                <div class="panel-piluku" id="PanelLista">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Listado
                            <%--                                <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload"
                                    onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize" href="#"><i
                                        class="icon ti-angle-up"></i></a></span>--%>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <form class="form form-horizontal" id="frmLista" onsubmit="return false;" action="#"
                        method="post">
                        </form>
                        <div class="row">
                            <div id="jqxgrid">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%--                <div class="row">
                    <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 100;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Registro de Denominación</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Tipo de Estudio:</label>
                                            <div class="col-sm-5">
                                                <select class="form-control" id="cboTipoEstudioR" name="cboTipoEstudioR">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Carrera Profesional:</label>
                                            <div class="col-sm-8">
                                                <select class="form-control" id="cboCarreraR" name="cboCarreraR">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Tipo de Denominación:</label>
                                            <div class="col-sm-5">
                                                <select class="form-control" id="cboTipoDenominacion" name="cboTipoDenominacion">
                                                    <option value="" selected="selected">-- Seleccione -- </option>
                                                    <option value="1">GRADO</option>
                                                    <option value="2">TITULO</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Denominación:</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtdenominacion" name="txtdenominacion" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Vigencia:</label>
                                            <div class="col-sm-4">
                                                <input type="checkbox" id="chkvigencia" name="chkvigencia" style="display: block;"
                                                    checked="checked" />
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" id="btnGuardar" class="btn btn-primary" onclick="fnGuardar();">
                                            Guardar</button>
                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            <div id="RegistroExpediente">
                <form class="form form-horizontal" id="frmRegistro" onsubmit="return false;" action="#"
                method="post">
                <div class="row">
                    <div class="col-md-12" style="padding: 0px">
                        <div class="panel panel-piluku">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Datos Personales <span class="panel-options"><a class="panel-refresh" href="#">
                                        <%--<i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i>--%></a>
                                        <%--<a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>--%></span>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <input type="hidden" id="hdCodigoAlu" name="hdCodigoAlu" value="0" />
                                <input type="hidden" id="hdCodEgr" name="hdCodEgr" value="0" />
                                <input type="hidden" id="hdCodigoTes" name="hdCodigoTes" value="0" />
                                <div class="row">
                                    <div class="col-md-2">
                                        <div id="foto" style="text-align: center; vertical-align: middle">
                                        </div>
                                        <br />
                                        <center>
                                            <button type="button" id="btnEditarDatos" name="btnEditarDatos" class="btn btn-orange">
                                                EDITAR</button>
                                        </center>
                                    </div>
                                    <div class="col-md-10">
                                        <div class="short-div">
                                            <div class="col-md-4">
                                                <div id="DatosPersonales1">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div id="DatosPersonales2">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div id="DatosPersonales3">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="short-div">
                                            <div class="col-md-12">
                                                <div id="ApellidosNombres">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="short-div">
                                            <div class="col-md-4">
                                                <div id="DatosPersonales5">
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div id="DatosPersonales6">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="short-div">
                                            <div class="col-md-12">
                                                <div id="DatosPersonales4">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- /panel -->
                        </div>
                    </div>
                </div>
                <!--/row-->
                <div class="row">
                    <div class="panel panel-piluku" id="Div2">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Datos de Trámite <span class="panel-options"><a class="panel-refresh" href="#">
                                    <%--<i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i>--%></a>
                                    <%--<a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>--%></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <label class="col-md-2 control-label">
                                    N° de Diploma:</label>
                                <div class="col-md-3">
                                    <input type="text" id="txtNroExp" name="txtNroExp" class="form-control" />
                                </div>
                                <label class="col-md-3 control-label">
                                    Fecha de Sesión de Consejo:</label>
                                <div class="col-md-3">
                                    <input type="text" id="txtFechaConsejo" name="txtFechaConsejo" class="form-control"
                                        disabled="disabled" />
                                    <%--<div id="txtFechaConsejo">
                                    </div>--%>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-2 control-label">
                                    N° de Resolución:</label>
                                <div class="col-md-3">
                                    <input type="text" class="form-control" id="txtNroResolucion" name="txtNroResolucion"
                                        disabled="disabled" />
                                </div>
                                <label class="col-md-3 control-label">
                                    Fecha de Resolución:</label>
                                <div class="col-md-3">
                                    <input type="text" id="txtFechaResolucion" name="txtFechaResolucion" class="form-control"
                                        disabled="disabled" />
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-2 control-label">
                                    N° de Resolución de Facultad:</label>
                                <div class="col-md-3">
                                    <input type="text" class="form-control" id="txtNroResolucionFac" name="txtNroResolucionFac" />
                                </div>
                                <label class="col-md-3 control-label">
                                    Fecha de Resolución de Facultad:</label>
                                <div class="col-md-3">
                                    <div id="txtFechaResolucionFac">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-2 control-label">
                                    Grupo:</label>
                                <div class="col-md-3">
                                    <select id="cboGrupo" name="cboGrupo" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-2 control-label">
                                    Libro:</label>
                                <div class="col-md-2">
                                    <input type="text" id="txtNroLibro" name="txtNroLibro" class="form-control" />
                                </div>
                                <label class="col-md-1 control-label">
                                    Folio:</label>
                                <div class="col-md-2">
                                    <input type="text" id="txtNroFolio" name="txtNroFolio" class="form-control" />
                                </div>
                                <label class="col-md-1 control-label">
                                    Registro:</label>
                                <div class="col-md-2">
                                    <input type="text" id="txtRegistro" name="txtRegistro" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /panel -->
                </div>
                <div class="row">
                    <div class="col-md-12" style="padding: 0px">
                        <!-- panel -->
                        <div class="panel panel-piluku" id="Div1">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Datos Academicos <span class="panel-options"><a class="panel-refresh" href="#">
                                        <%--<i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i>--%></a>
                                        <%--<a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>--%></span>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Facultad:</label>
                                    <div class="col-md-9">
                                        <select id="cboFacultad" name="cboFacultad" class="form-control">
                                            <option value="0">-- NO DEFINIDA -- </option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Carrera Profesional:</label>
                                    <div class="col-md-9">
                                        <select id="CboCarreraR" name="CboCarreraR" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Especialidad:</label>
                                    <div class="col-md-9">
                                        <select id="cboEspecialidad" name="cboEspecialidad" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Diploma Obtenido:</label>
                                    <div class="col-md-9">
                                        <select id="CboGrado" name="CboGrado" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Acto Académico:</label>
                                    <div class="col-md-3">
                                        <div id="txtFechaActoAcad">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <select id="cboActoAcad" name="cboActoAcad" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Título de Tesis:</label>
                                    <div class="col-md-9">
                                        <textarea id="txtTituloTesis" name="txtTituloTesis" class="form-control" rows="3"></textarea>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Modalidad Estudio:</label>
                                    <div class="col-md-3">
                                        <select id="cboModEstudio" name="cboModEstudio" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                            <option value="P" selected="selected">PRESENCIAL</option>
                                            <option value="S">SEMIPRESENCIAL</option>
                                            <option value="D">A DISTANCIA</option>
                                        </select>
                                    </div>
                                    <label class="col-md-3 control-label">
                                        Emisión de Diploma:</label>
                                    <div class="col-md-3">
                                        <select id="cboEmisionDiploma" name="cboEmisionDiploma" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                            <option value="O" selected="selected">ORIGINAL</option>
                                            <option value="D">DUPLICADO</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-3 control-label">
                                        Observaciones:</label>
                                    <div class="col-md-9">
                                        <textarea id="txtObservaciones" name="txtObservaciones" class="form-control" rows="3"></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- /panel -->
                    </div>
                </div>
                <!--/row-->
                <div class="row">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Cargos y Autoridades<span class="panel-options"><a class="panel-refresh" href="#">
                                    <%--<i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i>--%></a>
                                    <%--<a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>--%></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <label class="col-md-1 control-label">
                                    Cargo:</label>
                                <div class="col-md-4">
                                    <select id="cboCargo1" name="cboCargo1" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                                <label class="col-md-1 control-label">
                                    Autoridad:</label>
                                <div class="col-md-6">
                                    <select id="cboAutoridad1" name="cboAutoridad1" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-1 control-label">
                                    Cargo:</label>
                                <div class="col-md-4">
                                    <select id="cboCargo2" name="cboCargo2" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                                <label class="col-md-1 control-label">
                                    Autoridad:</label>
                                <div class="col-md-6">
                                    <select id="cboAutoridad2" name="cboAutoridad2" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-1 control-label">
                                    Cargo:</label>
                                <div class="col-md-4">
                                    <select id="cboCargo3" name="cboCargo3" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                                <label class="col-md-1 control-label">
                                    Autoridad:</label>
                                <div class="col-md-6">
                                    <select id="cboAutoridad3" name="cboAutoridad3" class="form-control">
                                        <option value="0">-- SELECCIONE --</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                        <!-- /panel -->
                    </div>
                </div>
                <!-- /row -->
                </form>
                <div class="row">
                    <center>
                        <button type="button" id="btnGuardar" name="btnGuardar" class="btn btn-primary">
                            Guardar</button>
                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                            Cancelar</button>
                    </center>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdEditarDatos" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 100;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H1">
                                        Editar Datos de Egresado</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmEditarDatos" name="frmEditarDatos" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <input type="hidden" id="hdCodigoAluME" name="hdCodigoAluME" value="0" />
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                N° Documento:</label>
                                            <label class="col-sm-4 control-label" style="text-align: left; font-weight: bold"
                                                id="lbldocumento">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Código Univ.:</label>
                                            <label class="col-sm-4 control-label" style="text-align: left; font-weight: bold"
                                                id="lblcodigo">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Apellido Paterno:</label>
                                            <div class="col-sm-8">
                                                <input type="text" name="txtapepat" id="txtapepat" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Apellido Paterno:</label>
                                            <div class="col-sm-8">
                                                <input type="text" name="txtapemat" id="txtapemat" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Nombres:</label>
                                            <div class="col-sm-8">
                                                <input type="text" name="txtnombres" id="txtnombres" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Email:</label>
                                            <div class="col-sm-8">
                                                <input type="text" name="txtemail" id="txtemail" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Teléfono Movil:</label>
                                            <div class="col-sm-5">
                                                <input type="text" name="txttelmov" id="txttelmov" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Teléfono Fijo:</label>
                                            <div class="col-sm-5">
                                                <input type="text" name="txttelfijo" id="txttelfijo" class="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" id="btnGuardarDatos" class="btn btn-primary">
                                            Guardar</button>
                                        <button type="button" class="btn btn-danger" id="btnCancelarEdicion" data-dismiss="modal">
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
    <div class="hiddendiv common">
    </div>
</body>
</html>
