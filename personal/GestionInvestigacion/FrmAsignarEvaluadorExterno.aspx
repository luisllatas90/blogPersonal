<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAsignarEvaluadorExterno.aspx.vb"
    Inherits="GestionInvestigacion_FrmAsignarEvaluadorExterno" %>

<!DOCTYPE html>
<html>
<head>
    <title>Asignar Evaluador Externo</title>
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

    <script src="js/_General.js?x=3" type="text/javascript"></script>

    <script src="js/AsignarEvaluadorExterno.js?x=3" type="text/javascript"></script>

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
    </style>
</head>
<body>
    <input type="hidden" id="hdRowsDT" name="hdRowsDT" value="" />
    <input type="hidden" id="hdCON" name="hdCON" value="" />
    <input type="hidden" id="hdEVE" name="hdEVE" value="" />
    <input type="hidden" id="hdCorreo" name="hdCorreo" value="" />
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Asignar
                                                Evaluadores Externos</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <%-- <button class="btn btn-primary" id="btnConsultar" value="Consultar" onclick="fnListarPostulaciones()">
                                                    Consultar</button>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="row">
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-12">
                                                <label class="col-md-1 control-label ">
                                                    Concurso</label>
                                                <div class="col-md-10">
                                                    <select id="cboConcurso" name="cboConcurso" class="form-control">
                                                        <option value="">--Seleccione--</option>
                                                    </select>
                                                    <input type="hidden" id="txtBusqueda" name="txtBusqueda" value="%" />
                                                </div>
                                            </div>
                                            <div class="col-md-4" style="display: none">
                                                <label class="col-md-4 control-label ">
                                                    Estado
                                                </label>
                                                <div class="col-md-8">
                                                    <input type="hidden" name="cboEstado" id="cboEstado" value="T">
                                                    <select name="cboAsignado" class="form-control" id="cboAsignado">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                        <option value="1">PENDIENTES</option>
                                                        <option value="2">ASIGNADOS</option>
                                                        <option value="T">TODOS</option>
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
                <div class="row" id="DivConcurso" style="display: none;">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Datos del Concurso
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="txttitulo">
                                        Título:</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txttitulo" name="txttitulo" class="form-control" readonly="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Breve Descripción:</label>
                                    <div class="col-md-8">
                                        <textarea class="form-control" cols="50" rows="3" id="txtdescripcion" name="txtdescripcion"
                                            readonly="true"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Fecha Inicio Postulación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecini" class="form-control" id="txtfecini" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Fecha Fin Evaluación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecfineva" class="form-control" id="txtfecfineva" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Fecha Fin Postulación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecfin" class="form-control" id="txtfecfin" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Fecha Publicación de Resultados:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecres" class="form-control" id="txtfecres" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Tipo:</label>
                                    <div class="col-md-3">
                                        <select id="cbotipo" name="cbotipo" class="form-control" disabled="disabled">
                                            <option value="">--Seleccione--</option>
                                            <option value="0">INDIVIDUAL</option>
                                            <option value="1">UNIDISCIPLINARIO</option>
                                            <option value="2">MULTIDISCIPLINARIO</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Bases</label>
                                    <div class="col-sm-3">
                                        <div id="bases">
                                        </div>
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Estructura de la Propuesta</label>
                                    <div class="col-sm-3">
                                        <a href='Archivos/Concursos/Estructura/FORMATO PROPUESTA - FONDO CONCURSABLE DOCENTE.docx' target='_blank'
                                            style='font-weight: bold;'>Estructura de La Propuesta</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="divPostulacion"  style="display: none;">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Lista de Postulaciones
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="buttons-list">
                                    <div class="pull-right-btn" id="divBtnEnviarEE">
                                        <button class="btn btn-green" id="btnEnviarEE" value="Agregar" onclick="fnEnviarEvaluadorExterno();">
                                            Enviar Evaluador Externo</button>
                                            <button class="btn btn-info" id="Button1" value="Agregar" onclick="fnEnviarNotificacionEvaluador();">
                                            Enviar Notificación</button>
                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <div id="Div2" class="dataTables_wrapper" role="grid">
                                        <table id="tPostulacion" name="tPostulacion" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td width="3%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    </td>
                                                    <td width="4%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td width="40%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Titulo
                                                    </td>
                                                    <td width="22%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Responsable
                                                    </td>
                                                    <td width="16%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Etapa
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Eval.
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                        Email
                                                    </td>
                                                    <td width="5%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                            <tbody id="tbPostulacion">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdEvaluadores" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div5">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H2">
                                        Evaluadores Externos</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmEvaluadores" name="frmEvaluadores" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Linea de Investigación USAT:</label>
                                            <div class="col-md-9">
                                                <select id="cboLinea" name="cboLinea" class="form-control">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtobjetivo">
                                                Evaluador Externo:</label>
                                            <div class="col-sm-7">
                                                <select id="cboEvaluador" name="cboEvaluador" class="form-control">
                                                    <option value="">--Seleccione--</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <center>
                                            <button type="button" id="btnAgregar" name="btnAgregar" class="btn btn-success" onclick="AsignarEvaluador()">
                                                Agregar Evaluador
                                            </button>
                                        </center>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <div class="col-sm-12">
                                                <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                                    <div id="Div6" class="dataTables_wrapper" role="grid">
                                                        <table id="tEvaluadores" name="tEvaluadores" class="display dataTable" width="100%">
                                                            <thead>
                                                                <tr role="row">
                                                                    <td width="5%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        N°
                                                                    </td>
                                                                    <td width="35%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                        Nombre
                                                                    </td>
                                                                    <td width="50%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                                        rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                                        URL Investigador
                                                                    </td>
                                                                    <td width="7%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
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
                                                            <tbody id="tbEvaluadores">
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
                                        <button type="button" class="btn btn-danger" id="Button7" data-dismiss="modal">
                                            Regresar</button>
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
    <div class="modal fade" id="mdMensaje" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 0;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                    </button>
                    <h4 class="modal-title" style="color: White">
                        <div id="divTitle">
                            Confirmación de Envio a Evaluadores Externos</div>
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12" id="divMensaje">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        <div class="btn-group" id="divOcultarEnvio">
                            <button type="button" class="btn btn-primary" id="btnAceptarEnvioEmail" name="btnAceptarEnvioEmail"
                                onclick="fnAceptarEnvioEmail()">
                                <i class="ion-android-done"></i>&nbsp;Aceptar</button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">
                                <i class="ion-android-cancel"></i>&nbsp;Cancelar</button>
                        </div>
                    </center>
                    <center>
                        <div id="divProcesando" style="display: none">
                            Procesando Información...
                        </div>
                    </center>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
