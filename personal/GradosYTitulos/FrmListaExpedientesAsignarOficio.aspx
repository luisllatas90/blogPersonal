<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaExpedientesAsignarOficio.aspx.vb" Inherits="GradosYTitulos_FrmListaExpedientesAsignarOficio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <title id='Description'>Asignar Oficio a Expedientes de Egresados</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
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

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.filter.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.sort.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.selection.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxpanel.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/globalization/globalize.js"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxcalendar.js?x=5"></script>

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxdatetimeinput.js?x=4"></script>

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

    <script src="js/_General.js?x=31" type="text/javascript"></script>

    <script src="js/Oficio.js?x=51" type="text/javascript"></script>

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
        #columntablejqxgrid1
        {
            z-index: 10;
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
                                            <i class="icon ti-folder page_header_icon"></i><span class="main-text">Asignar Oficio</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <%-- <button class="btn btn-primary" id="btnExportar">
                                                    Exportar</button>--%>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-5">
                                                    <label class="col-md-5 control-label ">
                                                        Grupo de Egresados</label>
                                                    <div class="col-md-7">
                                                        <select name="cboGrupo" class="form-control" id="cboGrupo">
                                                            <option value="" selected="">-- Seleccione -- </option>
                                                        </select>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <label class="col-md-5 control-label ">
                                                        Estado</label>
                                                    <div class="col-md-7">
                                                        <select name="cboEstado" class="form-control" id="cboEstado">
                                                            <option value="P" selected="selected">PENDIENTES</option>
                                                            <option value="A">ASIGNADOS</option>
                                                            <option value="T">TODOS</option>
                                                        </select>
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
                <div class="row" id="ListaSesion">
                    <div class="col-md-9">
                        <div class="panel panel-piluku" id="PanelLista">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Listado
                                    <%--                          <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload"
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
                                <div class="row">
                                    <label class="form-control" id="num_filas" name="num_filas" style='font-size: 11px;
                                        margin: 2px 3px; font-weight: bold; text-align: right;'>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="panel panel-piluku" id="Div1">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Oficio
                                    <%--                                    <span class="panel-options">
                                    <a class="panel-refresh" href="#"><i class="icon ti-reload"
                                        onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize" href="#"><i
                                            class="icon ti-angle-up"></i></a></span>--%>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <form class="form form-horizontal" id="FrmRegistro" onsubmit="return false;" action="#"
                                method="post">
                                <div class="row">
                                    <label class="col-md-12 control-label">
                                        N° de Oficio:</label>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <input type="text" class="form-control" name="txtNroOficio" id="txtNroOficio" />
                                    </div>
                                </div>
                                <div class="row">
                                    <center>
                                        <button class="btn btn-primary" id="btnAsignar" value="Registrar">
                                            Asignar</button>
                                    </center>
                                </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div> </div>
</body>
</html>
