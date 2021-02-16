<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaEstadoComunicacion.aspx.vb"
    Inherits="Crm_FrmListaEstadoComunicacion" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <%--    <meta http-equiv="X-UA-Compatible" content="IE=edge" />--%>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />--%>
    <meta http-equiv="Pragma" content="no-cache" />
    <%-- Necesarios --%>
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <%--<link rel='stylesheet' href='../assets/css/material.css' />--%>
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <%--<script type="text/javascript" src='../assets/js/app.js'></script>--%>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>

    <%-- Necesarios--%>
    <%-- Para Tabla--%>

    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <%-- Para Tabla--%>
    <%-- Loading de Tabla--%>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../assets/js/core.js'></script>

    <%-- Loading de Tabla--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/materialize.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/bic_calendar.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>--%>
    <%-- Notificaciones--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- Notificaciones--%>
    <%-- Tabla--%>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%-- Tabla--%>
    <%--<script type="text/javascript" src='../assets/js/funciones.js'></script>

    <script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=2'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/form-elements.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/select2.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>--%>
    <%--<script src='../assets/js/bootstrap-datepicker.js'></script>--%>

    <script type="text/javascript" src="js/crm.js?x=1"></script>

    <script type="text/javascript" src="js/JsEstadoComunicacion.js?x=1"></script>

    <title>Lista de Estado de Comunicación</title>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
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
       .i-am-new
        {
            z-index:100;
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
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Estado de Comunicación</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" href="#"><i class="ion-android-search"></i>
                                                    &nbsp;Listar</a> <a class="btn btn-green" id="btnAgregar" href="#" data-toggle="modal"
                                                        data-target="#mdRegistro"><i class="ion-android-add"></i>&nbsp;Agregar</a>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmBuscarEstadoComunicacion" onsubmit="return false;"
                                        action="#" method="post">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
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
                    <!-- panel -->
                    <div class="panel panel-piluku" id="PanelLista">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Listado de Estado de Comunicación<span class="panel-options"><a class="panel-refresh" href="#">
                                    <i class="icon ti-reload" onclick="fnBuscarEstadoComunicacion(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div id="tEstadoComunicacion_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tEstadoComunicacion" name="tEstadoComunicacion" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="85%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Nombre
                                                </td>
                                                <%--                                      
                                                <td width="7%" style="font-weight: bold; width: 203px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Estado: activate to sort column ascending">
                                                    Estado
                                                </td>--%>
                                                <td width="10%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                    rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                    Opciones
                                                </td>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th colspan="7" rowspan="1">
                                                </th>
                                            </tr>
                                        </tfoot>
                                        <tbody id="tbEstadoComunicacion">
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
                        <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                            style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog" id="modalReg">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="myModalLabel3">
                                            Mantenimiento de Estado de Comunicación</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Descripción:</label>
                                                <div class="col-sm-8">
                                                    <input id="txtDescripcion" name="txtDescripcion" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <%-- 
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Estado:</label>
                                                <div class="col-sm-4">
                                                    <input type="checkbox" id="chkestado" name="chkestado" style="display: block;" checked="checked" />
                                                </div>
                                            </div>
                                        </div>
                                                                    <div class="row">
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
                                            <button type="button" id="btnA" class="btn btn-primary" onclick="fnGuardar();">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                                Cancelar</button>
                                        </center>
                                        <input  type="hidden" name="hdValida" id="hdValida" value="0" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- row -->
                    <div class="row">
                        <div class="modal fade" id="mdEliminar" role="dialog" aria-labelledby="myModalLabel" style="z-index:0;"
                            aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-sm">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="H1">
                                            Eliminar EstadoComunicacion</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmEliminar" name="frmEliminar" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                        <h5 class="text-danger">
                                        ¿Esta Seguro que desea Eliminar EstadoComunicacion?</h4>
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
