<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContingenciaFrmPostulacionConcurso.aspx.vb"
    Inherits="GestionInvestigacion_FrmPostulacionConcurso" %>

<!DOCTYPE html>
<html>
<head>
    <title>Subir Archivos Postulación - SharedFiles</title>
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

    <script src="js/Contingencia.js?x=11" type="text/javascript"></script>

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
                    <div class="form-group">
                        <center>
                            <button id="btnPostular" name="btnPostular" class="btn btn-success">
                                Reemplazar Archivos de Postulación</button>
                        </center>
                    </div>
                </div>
                <%--                </div>
                            </div>
                        </div>--%>
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
                                        Registrar Postulación</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div role="tabpanel">
                                        <!-- Nav tabs -->
                                        <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist">
                                            <%--                                                    <li role="presentation" class="active"><a href="#hometabnb" aria-controls="home"
                                                        role="tab" data-toggle="tab" aria-expanded="true" id="tab1">Información General</a></li>
                                                    <li role="presentation" class=""><a href="#profiletabnb" aria-controls="profile"
                                                        role="tab" data-toggle="tab" aria-expanded="false" id="tab2">Grupo de Investigación</a></li>
                                                    <li role="presentation" class=""><a href="#messagestabnb" aria-controls="messages"
                                                        role="tab" data-toggle="tab" aria-expanded="false" id="tab3">Propuesta</a></li>--%>
                                            <li role="presentation" class="active"><a href="#settingstabnb" aria-controls="settings"
                                                role="tab" data-toggle="tab" aria-expanded="false" id="tab4">Archivos de Propuesta</a></li>
                                        </ul>
                                        <!-- Tab panes -->
                                        <div class="tab-content piluku-tab-content">
                                            <div role="tabpanel" class="tab-pane active" id="settingstabnb">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Concurso</label>
                                                        <div class="col-sm-8">
                                                            <select id="cboConcurso" name="cboConcurso" class="form-control">
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Postulación</label>
                                                        <div class="col-sm-8">
                                                            <select id="hdcodPos" name="hdcodPos" class="form-control">
                                                                <option value="">-- Seleccione --</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--                                 <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            CODIGO POSTULACION</label>
                                                        <div class="col-sm-8">
                                                            <input type="text" id="hdcodPos" name="hdcodPos" value="0" />
                                                            <div id="Div2">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            TIPO ARCHIVO</label>
                                                        <div class="col-sm-8">
                                                            <select id="cboTipo" name="cboTipo" class="form-control">
                                                                <option value="" selected="selected">-- Seleccione --</option>
                                                                <option value="PROPUESTA">PROPUESTA</option>
                                                                <option value="PRESUPUESTO">PRESUPUESTO</option>
                                                                <option value="CRONOGRAMA">CRONOGRAMA</option>
                                                                <option value="RESULTADOSESPERADOS">RESULTADOS ESPERADOS</option>
                                                                <option value="DECLARACION">DECLARACION</option>
                                                                <option value="RUBRICA">RUBRICA</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Evaluador Externo</label>
                                                        <div class="col-sm-8">
                                                            <select id="cboEvaluador" name="cboEvaluador" class="form-control">
                                                                <option value="">-- Seleccione --</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-sm-3 control-label">
                                                            Archivo</label>
                                                        <div class="col-sm-8">
                                                            <input type="file" id="file" name="file" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer" id="Footer_Modal">
                                    <div id="DivGuardar">
                                        <center>
                                            <button type="button" id="btnSubir" class="btn btn-primary">
                                                Subir Archivo</button>
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
            </div>
        </div>
    </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
