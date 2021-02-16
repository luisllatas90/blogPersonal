<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistrarExpediente.aspx.vb"
    Inherits="GradosYTitulos_FrmRegistrarExpediente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Expediente</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <link rel="stylesheet" href="../assets/jqwidgets/jqwidgets/styles/jqx.base.css" type="text/css" />
    <link href="../assets/jqwidgets/jqwidgets/styles/jqx.light.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="../assets/jqwidgets/scripts/jquery-1.11.1.min.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcore.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdata.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxbuttons.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxscrollbar.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxlistbox.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdropdownlist.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxmenu.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxinput.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.js?x=1"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.filter.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.sort.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.selection.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxpanel.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/globalization/globalize.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcalendar.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdatetimeinput.js?x=2"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcheckbox.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.pager.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdata.export.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.export.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxtooltip.js"></script>

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

    <script src="js/_General.js?a=2" type="text/javascript"></script>

    <script src="js/RegistroEgresado.js?x=3" type="text/javascript"></script>

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
        .jqx-grid-column-header, .jqx-grid-cell
        {
            font-size: 11px;
        }
        #modalCoincidencias
        {
            width: 90%;
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
<body class="">

    <div class="piluku-preloader">
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
            <%--<div class="main-content" style="background-color: White">--%>
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons" id="divOpc">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark page_header_icon"></i><span class="main-text">Registro de
                                                Expediente</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <label class="control-label">
                                                    Buscar:</label>
                                                <input type="text" id="txtbusqueda" name="txtbusqueda" style="width: 250px" placeholder="DNI/Apellidos y Nombres" />
                                            </div>
                                        </div>
                                        <%--
                                        <form class="form form-horizontal" id="frmBuscarConvoc" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Tipo de Estudio</label>
                                                    <div class="col-sm-12 col-md-8">
                                                        <select name="cboTipoEstudio" class="form-control" id="cboTipoEstudio">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        </form>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="RegistroExpediente">
                    <form class="form form-horizontal" id="frmRegistro" onsubmit="return false;" action="#"
                    method="post">
                    <div class="row">
                        <div class="col-md-3" style="padding: 0px">
                            <!-- panel -->
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
                                        <label class="col-md-12 control-label" style="text-align: left">
                                            N° de Diploma:</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <input type="text" id="txtNroExp" name="txtNroExp" class="form-control" />
                                        </div>
                                    </div>
                                    <!--Agregado por OLLUEN-->
                                    <div class="row">
                                        <label class="col-md-12 control-label" style="text-align: left">
                                            Descargar Archivos de trámite:</label>
                                    </div>
                                    <div class="row">
                                       <div class="col-md-12">
                                           <button type="button" id="Button1" name="btnEditarDatos" class="btn btn-primary">
                                                    DESCARGAR</button>
                                        </div>
                                           
                                        
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="height: 50px">
                                        </div>
                                    </div>
                                    <%-- <div class="row">
                                    <label class="col-md-12 control-label">
                                        Fecha de Sesion de Consejo:</label>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-12 control-label">
                                        Fecha de Resolución:</label>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-md-12 control-label">
                                        N° de Resolución:</label>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <input type="text" class="form-control" />
                                    </div>
                                </div>--%>
                                </div>
                            </div>
                            <!-- /panel -->
                        </div>
                        <div class="col-md-9" style="padding: 0px">
                            <div class="panel panel-piluku">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Datos Personales <span class="panel-options"><a class="panel-refresh" href="#">
                                            <%--<i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i>--%></a>
                                            <%--<a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>--%></span>
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                    <input type="hidden" id="hdCodigoAlu" name="hdCodigoAlu" value="0" />
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
                        <div class="col-md-12" style="padding: 0px">
                            <!-- panel -->
                            <div class="panel panel-piluku" id="Div1">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Datos Académicos <span class="panel-options"><a class="panel-refresh" href="#">
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
                                            <select id="CboCarrera" name="CboCarrera" class="form-control">
                                                <option value="0">-- SELECCIONE --</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label">
                                            Especialidad:</label>
                                        <div class="col-md-9">
                                            <select id="CboEspecialidad" name="CboEspecialidad" class="form-control">
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
                                    <div id="verificaBachiller" name="verificaBachiller" class="text-left" style="padding-left: 27%">
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
                                            Titulo de Tesis:</label>
                                        <div class="col-md-9">
                                            <textarea id="txtTituloTesis" name="txtTituloTesis" class="form-control" rows="3"></textarea>
                                        </div>
                                    </div>
                                    <%--                                    <div class="row">
                                        <label class="col-md-3 control-label">
                                            Grupo:</label>
                                        <div class="col-md-3">
                                            <select id="cboGrupo" name="cboGrupo" class="form-control">
                                                <option value="0">-- SELECCIONE --</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label">
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
                                    </div>--%>
                                    <div class="row">
                                        <label class="col-md-3 control-label">
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
                                <!-- 11.08.2020 JBANDA Cuarta Autoridad -->
                                <div class="row">
                                    <label class="col-md-1 control-label">
                                        Cargo:</label>
                                    <div class="col-md-4">
                                        <select id="cboCargo4" name="cboCargo4" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                        </select>
                                    </div>
                                    <label class="col-md-1 control-label">
                                        Autoridad:</label>
                                    <div class="col-md-6">
                                        <select id="cboAutoridad4" name="cboAutoridad4" class="form-control">
                                            <option value="0">-- SELECCIONE --</option>
                                        </select>
                                    </div>    
                                </div>
                                <!-- 11.08.2020 JBANDA Cuarta Autoridad -->                                
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
                            <button type="button" id="Button2" name="btnObservar" class="btn btn-warning">
                                Observar</button>
                            <button type="button" id="Button3" name="btnConformidad" class="btn btn-success">
                                Conformidad</button>
                            <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                Cancelar</button>
                        </center>
                    </div>
                </div>
                <div class="modal fade" id="mdCoincidencias" role="dialog" aria-labelledby="myModalLabel"
                    style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg" id="modalCoincidencias">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                </button>
                                <h4 class="modal-title" id="myModalLabel3">
                                    Coincidencias Búsqueda Alumno</h4>
                            </div>
                            <div class="modal-body">
                                <form id="frmCoincidencias" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                method="post" onsubmit="return false;" action="#">
                                <div id="jqxgridModal">
                                </div>
                                <p class="text-right">
                                    Se Muestran las 200 Primeras Coincidencias</p>
                                </form>
                            </div>
                            <%--                                <div class="modal-footer">
                                </div>--%>
                        </div>
                    </div>
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
                                            <label class="col-sm-4 control-label" style="text-align:left; font-weight:bold" id="lbldocumento">
                                            </label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Código Univ.:</label>
                                            <label class="col-sm-4 control-label" style="text-align:left; font-weight:bold" id="lblcodigo">
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
    <div class="hiddendiv common">
    </div>
</body>
</html>
