<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistroProyecto.aspx.vb"
    Inherits="GestionInvestigacion_FrmRegistroProyecto" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>Registro de Proyectos</title>
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

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>

    <script src="js/_General.js?x=2" type="text/javascript"></script>

    <script src="js/Proyecto.js?x=2" type="text/javascript"></script>

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
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
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
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar">
                                                    Consultar</button>
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar">
                                                    Agregar</button>
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <label class="col-md-2 control-label ">
                                                    Curso</label>
                                                <div class="col-md-10">
                                                    <select name="cboCurso" class="form-control" id="cboCurso">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <label class="col-md-3 control-label ">
                                                    Estado</label>
                                                <div class="col-md-9">
                                                    <select name="cboEstado" class="form-control" id="cboEstado">
                                                        <option value="" selected="">-- Seleccione -- </option>
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
                                Alumnos Matriculados
                                <%--                                <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload"
                                    onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize" href="#"><i
                                        class="icon ti-angle-up"></i></a></span>--%>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form class="form form-horizontal" id="frmLista" onsubmit="return false;" action="#"
                            method="post">
                            </form>
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
