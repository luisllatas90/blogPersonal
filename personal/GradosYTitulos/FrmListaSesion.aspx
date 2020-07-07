<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaSesion.aspx.vb"
    Inherits="GradosYTitulos_FrmListaSesion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <title id='Description'>Sesión de Consejo</title>
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

    <script type="text/javascript" src="../assets/jqwidgets/jqwidgets/jqxgrid.columnsresize.js" ></script>

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

    <script src="js/SesionConsejo.js?x=4" type="text/javascript"></script>

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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Sesiones de Consejo Universitario</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar">
                                                    Consultar</button>
                                                <button class="btn btn-green" id="btnAgregar" value="Agregar" style="display:none" >
                                                    Agregar</button>
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
<%--                                        <div class="form-group">
                                            <div class="col-md-5">
                                                <label class="col-md-3 control-label ">
                                                    Sesión</label>
                                                <div class="col-md-9">
                                                    <select name="cboVigenciaL" class="form-control" id="cboVigenciaL">
                                                        <option selected="selected" value="T">TODOS </option>
                                                        <option value="1">VIGENTE</option>
                                                        <option value="0">NO VIGENTE</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>--%>
                                        </form>
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
                <div class="row">
                    <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 100;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Registro de Sesión</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Fecha de Sesión:</label>
                                            <div class="col-sm-5">
                                                <div id="txtFechaSesion"></div>
                                            </div>
                                        </div>
                                    </div>
<%--                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Vigencia:</label>
                                            <div class="col-sm-4">
                                                <input type="checkbox" id="chkvigencia" name="chkvigencia" style="display: block;"
                                                    checked="checked" />
                                            </div>
                                        </div>
                                    </div>--%>
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
                </div>
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
