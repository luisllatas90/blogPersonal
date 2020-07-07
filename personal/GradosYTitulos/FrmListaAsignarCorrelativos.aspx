<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAsignarCorrelativos.aspx.vb"
    Inherits="GradosYTitulos_FrmListaAsignarCorrelativos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
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

    <script src="js/_General.js?x=1" type="text/javascript"></script>

    <script src="js/AsignaCorrelativos.js?x=5" type="text/javascript"></script>

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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Generar
                                                Correlativos</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar">
                                                    Consultar</button>
                                                <button class="btn btn-green" id="btnGenerar" value="Generar">
                                                    Generar</button>
                                                <button class="btn btn-danger" id="btnQuitar" value="Quitar">
                                                    Quitar</button>
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-5">
                                                <label class="col-md-3 control-label ">
                                                    Sesión de Consejo</label>
                                                <div class="col-md-9">
                                                    <select name="cboSesion" class="form-control" id="cboSesion">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
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
                                            <div class="col-md-7">
                                                <label class="col-md-3 control-label ">
                                                    Tipo Denominación</label>
                                                <div class="col-md-9">
                                                    <select name="cboTipoDenominacion" class="form-control" id="cboTipoDenominacion">
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
                        <div class="row">
                            <div class="col-md-2">
                                <b>N° DIPLOMA</b>
                            </div>
                            <div class="col-md-2">
                                <b>BACHILLER</b>
                            </div>
                            <div class="col-md-2">
                                <b>TITULO</b>
                            </div>
                            <div class="col-md-2">
                                <b>SEGUNDA ESPECIALIDAD</b>
                            </div>
                            <div class="col-md-2">
                                <b>MAESTRO</b>
                            </div>
                            <div class="col-md-2">
                                <b>DOCTOR</b>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                            </div>
                            <div class="col-md-2">
                                <span style="color: Green; font-weight:bold; font-size:13px" id="cBachiller"></span>
                            </div>
                            <div class="col-md-2">
                                <span style="color: Blue; font-weight:bold; font-size:13px" id="cTitulo"></span>
                            </div>
                            <div class="col-md-2">
                                <span style="color: Red; font-weight:bold; font-size:13px" id="cSegunda"></span>
                            </div>
                            <div class="col-md-2">
                                <span style="color:black; font-weight:bold; font-size:13px" id="cMaestro"></span>
                            </div>
                            <div class="col-md-2">
                                <span style="color:Maroon; font-weight:bold; font-size:13px" id="cDoctor"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <input type="text" name="nroDiploma" id="nroDiploma" class="form-control" maxlength="11" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="libro_b" id="libro_b" class="form-control" maxlength="6" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="folio_b" id="folio_b" class="form-control" maxlength="3" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="libro_t" id="libro_t" class="form-control" maxlength="6" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="folio_t" id="folio_t" class="form-control" maxlength="3" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="libro_s" id="libro_s" class="form-control" maxlength="6" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="folio_s" id="folio_s" class="form-control" maxlength="3" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="libro_m" id="libro_m" class="form-control" maxlength="6" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="folio_m" id="folio_m" class="form-control" maxlength="3" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="libro_d" id="libro_d" class="form-control" maxlength="6" />
                            </div>
                            <div class="col-md-1">
                                <input type="text" name="folio_d" id="folio_d" class="form-control" maxlength="3" />
                            </div>
                        </div>
                        <div class="row">
                            <div id="jqxgrid">
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
