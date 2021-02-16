<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaExpedientesSesionConsejo.aspx.vb"
    Inherits="GraduacionYTitulacion_FrmListaExpedientesSesionConsejo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <title id='Description'>Sesion de Consejo Universitario </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
    <link rel="stylesheet" href="../../assets/jqwidgets/jqwidgets/styles/jqx.base.css"
        type="text/css" />
    <link href="../../assets/jqwidgets/jqwidgets/styles/jqx.light.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="../../assets/jqwidgets/scripts/jquery-1.11.1.min.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxcore.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdata.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxbuttons.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxscrollbar.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxlistbox.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdropdownlist.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxmenu.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxinput.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.filter.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.sort.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.selection.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxpanel.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/globalization/globalize.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxcalendar.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdatetimeinput.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxcheckbox.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.pager.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxdata.export.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxgrid.export.js"></script>

    <script type="text/javascript" src="../../assets/jqwidgets/jqwidgets/jqxtooltip.js"></script>

    <%--  <script type="text/javascript" src="../../assets/jqwidgets/scripts/demos.js"></script>--%>
    <%-- <script type="text/javascript" src="js/generatedata.js"></script>--%>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script src='../../assets/js/bootstrap-datepicker.js'></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>
    <%-- ======================= Incio Exportar =============================================--%>
    <%--<script src="../../assets/jqwidgets/jqwidgets/jqxdata.js" type="text/javascript"></script>

    <script src="../../assets/jqwidgets/jqwidgets/jqxdata.export.js" type="text/javascript"></script>

    <script src="../../assets/jqwidgets/jqwidgets/jqxgrid.export.js" type="text/javascript"></script>--%>
    <%-- ======================= Fin Exportar =============================================--%>

    <script src="js/_General.js?x=2" type="text/javascript"></script>

    <script src="js/Sesion.js?x=9" type="text/javascript"></script>

    <%--<script src="js/jquery.callback.js" type="text/javascript"></script>--%>
    <%--<script src="js/ExportHTMLTable.js" type="text/javascript"></script>--%>
    <%--<script src="js/ExportarCVS.js" type="text/javascript"></script>--%>
    <%--<script src="js/JqxTable.js" type="text/javascript"></script>--%>
    <%--    <script type="text/javascript">
        var exportTable1 = new ExportHTMLTable('jqxgrid');
    </script>--%>
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
        #columntablejqxgrid1
        {
            z-index: 10;
        }
        .jqx-grid-cell
        {
            font-size: 11px;
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
                                            <i class="icon ti-layers page_header_icon"></i><span class="main-text">Sesiones de Consejo
                                                Universitario</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnRegistrar" value="Registrar" style="display: none">
                                                    Asignar</button>
                                                <%-- <button class="btn btn-primary" id="btnExportar">
                                                    Exportar</button>
                                                <input type="button" onclick="exportTable1.exportToCSV()" value="Export to-CSV" />--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="ListaSesion">
                    <div class="panel panel-piluku" id="PanelLista" style="display: none">
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
                                <div class="col-md-2">
                                    <div id="jqxlistbox">
                                    </div>
                                </div>
                                <div class="col-md-10">
                                    <div id="jqxgrid">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label class="form-control" id="num_filas" name="num_filas" style='font-size: 11px;
                                    margin: 2px 3px; font-weight: bold; text-align: right;'>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="RegistraSesion">
                    <div class="panel panel-piluku" id="Div1">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Gestionar Sesiones de Consejo Universitario
                                <%--                                <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload"
                                    onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize" href="#"><i
                                        class="icon ti-angle-up"></i></a></span>--%>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form class="form form-horizontal" id="Form1" onsubmit="return false;" action="#"
                            method="post">
                            </form>
                            <div class="row">
                                <div class="col-md-6 col-sm-6">
                                    <%-- <div class="row">
                                        <label class="col-md-12 control-label" style="font-weight: bold; color: black">
                                            I. Crear una Nueva Sesión de Consejo</label>
                                    </div>--%>
                                    <div class="row">
                                        <form id="frmRegistroSesion" name="frmRegistroSesion" enctype="multipart/form-data"
                                        class="form-horizontal" method="post" onsubmit="return false;" action="#">
                                        <%-- <label class="col-md-3 control-label">
                                            Nueva Sesión</label>
                                        <div class="col-md-6">
                                            <div class="input-group date" id="FechaSesion">
                                                <input name="txtFecha" class="form-control" id="txtFecha" style="text-align: right;"
                                                    type="text" placeholder="__/__/____" data-provide="datepicker">
                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="Fec">
                                                </i></span>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <button id="btnAgregarSesion" class="btn btn-primary">
                                                Crear Sesión</button>
                                        </div>--%>
                                        </form>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-12 control-label" style="font-weight: bold; color: black">
                                            Expedientes pendientes de asignar :</label>
                                    </div>
                                    <div id="jqxgrid1">
                                    </div>
                                    <div class="row">
                                        <label class="form-control" id="num_filas1" name="num_filas1" style='font-size: 11px;
                                            margin: 2px 3px; font-weight: bold; text-align: right;'>
                                        </label>
                                    </div>
                                    <div class="row" style="text-align: center">
                                        <button id="btnAsignar" class="btn btn-orange">
                                            Asignar Seleccionados >></button>
                                    </div>
                                </div>
                                <div class="col-md-6 col-sm-6">
                                    <div class="row">
                                        <label class="col-sm-4 col-md-3 control-label">
                                            Elegir Sesión</label>
                                        <div class="col-sm-8 col-md-6">
                                            <select id="CboSesiones" name="CboSesiones" class="form-control">
                                                <option value="">--Seleccione--</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-12" style="font-weight: bold; color: black" id="lblsesion">
                                        </label>
                                    </div>
                                    <div id="jqxgrid2">
                                    </div>
                                    <div class="row">
                                        <label class="form-control" id="num_filas2" name="num_filas2" style='font-size: 11px;
                                            margin: 2px 3px; font-weight: bold; text-align: right;'>
                                        </label>
                                    </div>
                                    <div class="row" style="text-align: center">
                                        <button id="btnEnviar" class="btn btn-success" style="display: none">
                                            Enviar a sesión de consejo Universitario>></button>
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
</body>
</html>
