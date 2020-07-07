﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaVariables.aspx.vb" Inherits="academico_PredictorDiserccion_frmListaVariables" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
<link rel="stylesheet" href='../../assets/css/sweet-alerts/sweetalert.css'/>
    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../../assets/js/app.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src='../../assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../../assets/js/core.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.countTo.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css' />
    <%--<script type="text/javascript" src='../assets/js/funciones.js'></script>

    <script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=2'></script>--%>

    <script type="text/javascript" src='../../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../../assets/js/select2.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../../assets/js/bootstrap-colorpicker.js'></script>

    <script src='../../assets/js/bootstrap-datepicker.js'></script>
   <script type="text/javascript" src="../../assets/js/sweet-alert/sweetalert.min.js?x=3"></script>

    <script type="text/javascript" src="js/Variables.js"></script>

    <title></title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
        }
        .content .main-content
        {
            padding-right: 15px;
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
            height: 30px;
            font-weight: 300; /* line-height: 40px; */
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
        .sweet-alert button.cancel
        {
            color: #fff;
            background-color: #c9302c;
            border-color: #ac2925;
        }
        .sweet-alert button 
        {
                background-color: #6fd64b;
    
            
            color: white;
            box-shadow: none;
            font-size: 17px;
            font-weight: 500;
            cursor: pointer;
            border-width: initial;
            border-style: none;
            border-color: initial;
            border-image: initial;
            border-radius: 5px;
            padding: 10px 32px;
            margin: 26px 5px 0px;
        }
        .sweet-alert .sa-icon.sa-info
        {
            display:none;
        }
        
        .noty_message
            z-index:100;
        {
        }
        </style>
</head>
<body class="">
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
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Variables de Análisis Predictivo</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a> <a class="btn btn-green" id="btnAgregar" href="#" data-toggle="modal"
                                                        data-target="#mdRegistro"><i class="ion-android-add"></i>&nbsp;Agregar</a>
                                            </div>
                                        </div>                                         
                                    </div>
                                </div>
                                <%--<div class="col-md-3 col-sm-12  col-lg-3">
                                    <div class="buttons-list">
                                        <div class="right pull-right">
                                            <ul class="right_bar">
                                                <a href="javascript:get_loc();">Mostrar localización</a>
                                                 <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Consultar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Registrar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Modificar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Eliminar</li>
                                                <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Imprimir</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Variables <span class="panel-options"><a class="panel-refresh" href="#">
                                    <i class="icon ti-reload" onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div id="tConvocatoria_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tConvocatoria" name="tConvocatoria" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 54px; " class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="38%" style="font-weight: bold; width: 89px; " class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Nombre Variable Descrip.
                                                </td>
                                                <td width="15%" style="font-weight: bold;  " class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Nombre Variable
                                                </td>
                                                <td width="20%" style="font-weight: bold; " class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Descripción
                                                </td>
                                                <td width="10%" style="font-weight: bold;   text-align: center;" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                    Origen
                                                </td>
                                                <td width="10%" style="font-weight: bold; " class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Fin: activate to sort column ascending">
                                                    Signo
                                                </td>
                                                <td width="7%" style="font-weight: bold;   " class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                    Estado
                                                </td>
                                                <td width="10%" style="font-weight: bold;  text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                    Opciones
                                                </td>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th colspan="10" rowspan="1">
                                                </th>
                                            </tr>
                                        </tfoot>
                                        <tbody id="tbListVariable">
                                            <%--  <tr class="odd">
                                                <td valign="top" colspan="7" class="dataTables_empty">
                                                    No se ha encontrado informacion
                                                </td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /panel -->
                    <div class="row">
                        <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel" style='z-index:0'
                            aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog" id="modalReg">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="myModalLabel3">
                                            Mantenimiento de Variables</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                          <input type="hidden" id="Funcion" name="Funcion" value="Grabar" />
                                          <input type="hidden" id="op" name="op" value="1" />
                                          <input type="hidden" id="txtCodigoVar" name="txtCodigoVar" value="0" />
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Origen:</label>
                                                <div class="col-sm-4">
                                                    <select class="form-control" id="cboOrigen" name="cboOrigen">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                        <option value="1">Alumnos</option>
                                                        <option value="2">Tutoria</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Nombre Desc.:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtnombreDesc" name="txtnombreDesc" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                         <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Nombre Variable:</label>
                                                <div class="col-sm-4">
                                                    <input type="text" id="txtnombre" name="txtnombre" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Descripción:</label>
                                                <div class="col-sm-8">
                                                    <textarea id="txtDescripcion" name="txtDescripcion" rows="5" style="width: 100%"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Fecha :</label>
                                                <div class="col-sm-5" id="date-popup-group">
                                                    <div class="input-group date" id="FechaInicio">
                                                        <input name="txtFecha" class="form-control" id="txtFecha" style="text-align: right;"
                                                            type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="FecInicial">
                                                        </i></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Signo:</label>
                                                <div class="col-sm-5" id="date-popup-group">
                                                    <select class="form-control" id="cboSigno" name="cboSigno">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                         <option value="+">(+) </option>
                                                          <option value="-" >(-)</option>
                                                           
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Estado:</label>
                                                <div class="col-sm-4">
                                                    <input type="checkbox" id="chkestado" name="chkestado"   checked="checked" />
                                                </div>
                                            </div>
                                        </div>
                                        <%--                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Adjunto:</label>
                                                <div class="col-sm-8">
                                                    <input name="txtfile" type="file" id="txtfile" class="form-control">
                                                </div>
                                            </div>
                                        </div>--%>
                                        </form>
                                    </div>
                                    <div class="modal-footer">
                                        <center>
                                            <button type="button" id="btnA" class="btn btn-primary" onclick="confirmGuardar();">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                    <div class="row">
                        <div class="modal fade" id="mdEliminar" role="dialog" aria-labelledby="myModalLabel"
                            aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="H1">
                                            Eliminar Convocatoria</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmEliminar" name="frmEliminar" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                        <h5 class="text-danger">
                                        ¿Esta Seguro que desea Eliminar Convocatoria?</h4>
                                        </form>
                                    </div>
                                    <div class="modal-footer">
                                        <center>
                                            <button type="button" id="Button1" class="btn btn-primary" onclick="fnEliminar();">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="Button2" data-dismiss="modal">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                </div>
                <!-- row -->
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
</body>
</html>
--%> 