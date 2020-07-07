<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaReclamosDefensoria.aspx.vb"
    Inherits="Crm_FrmListaTipoComunicacion" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=2' />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/app.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../assets/js/core.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%--<script type="text/javascript" src='../assets/js/funciones.js'></script>

    <script type="text/javascript" src='../assets/js/funcionesDataTable.js?x=2'></script>--%>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script src='../assets/js/bootstrap-datepicker.js'></script>

    <script type="text/javascript" src="js/Defensoria.js?x=19"></script>

    <script type="text/javascript" src="js/JsDefensoria.js?x=17"></script>

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
        .i-am-new
        {
            z-index: 100;
        }
    </style>

    <script language="javascript" type="text/javascript">
      //  function btnGuardar_onclick() {
        //}

    </script>

</head>
<body class="">
    <div class="piluku-preloader text-center hidden">
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
                                            <i class="icon ti-volume page_header_icon"></i><span class="main-text">Defensoría Universitaria</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <a class="btn btn-primary" id="btnListar" name="btnListar" href="#"><i class="ion-android-search">
                                                </i>&nbsp;Listar</a> <a class="btn btn-green" id="btnAgregar" href="#" data-toggle="modal"
                                                    data-target="#mdRegistro"><i class="ion-android-add"></i>&nbsp;Agregar</a>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmBuscarDefensoria" onsubmit="return false;"
                                        action="#" method="post">
                                        <input name="process" type="hidden" id="process">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-sm-12 col-md-4 control-label">
                                                        Tipo:</label>
                                                    <div class="col-sm-12 col-md-4">
                                                        <select name="cboTipo" class="form-control" id="cboTipo">
                                                            <option value="T" selected="">-- Seleccione -- </option>
                                                            <option value="C">CONSULTA </option>
                                                            <option value="R">RECLAMO</option>
                                                            <option value="D">DENUNCIA</option>
                                                        </select>
                                                    </div>
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
                                Registro Sobre Defensoría Universitaria <span class="panel-options"><a class="panel-refresh"
                                    href="#"><i class="icon ti-reload" onclick="fnBuscar(false)"></i></a><a class="panel-minimize"
                                        href="#"><i class="icon ti-angle-up"></i></a></span>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div id="tDefensoria_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tDefensoria" name="tDefensoria" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 5px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="7%" style="font-weight: bold; width: 7px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Fecha
                                                </td>
                                                <td width="8%" style="font-weight: bold; width: 8px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Teléfono
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 10px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Mail
                                                </td>
                                                <td width="30%" style="font-weight: bold; width: 30px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Detalle
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 10px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Tipo
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 10px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    N° Res
                                                </td>
                                                <td width="20%" style="font-weight: bold; width: 20px; text-align: center" class="sorting"
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
                                        <tbody id="tbDefensoria">
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
                                            Mantenimiento de Defensoría Universitaria</h4>
                                    </div>
                                    <div class="modal-body">
                                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                        method="post" onsubmit="return false;" action="#">
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Tipo:</label>
                                                <div class="col-sm-8">
                                                    <select class="form-control" id="cboTipoR" name="cboTipoR">
                                                        <option value="T" selected="true">-- Seleccione -- </option>
                                                        <option value="C">CONSULTA </option>
                                                        <option value="R">RECLAMO</option>
                                                        <option value="D">DENUNCIA</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Telefono:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtTelefono" name="txtTelefono" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Email:</label>
                                                <div class="col-sm-8">
                                                    <input type="text" id="txtEmail" name="txtEmail" class="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <label class="col-sm-3 control-label">
                                                    Detalle:</label>
                                                <div class="col-sm-8">
                                                    <textarea id="txtDetalle" name="txtDetalle" style="width: 100%"></textarea>
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                    <div class="modal-footer">
                                        <center>
                                            <button type="button" id="btnGuardar" name="btnGuardar" class="btn btn-primary">
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
                <!-- row -->
                <!-- /panel -->
                <div class="row">
                    <div class="modal fade" id="mdRespuesta" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog" id="modalResp">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H1">
                                        Agregar Respuesta de Defensoría Universitaria</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegRespuesta" name="frmRegRespuesta" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label">
                                                Respuesta:</label>
                                            <div class="col-sm-8">
                                                <textarea id="txtRespuesta" name="txtRespuesta" style="width: 100%"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" id="btnGuardarResp" name="btnGuardarResp" class="btn btn-primary"
                                            onclick='fnResponder();'>
                                            Guardar</button>
                                        <button type="button" class="btn btn-danger" id="btnCancelarResp" data-dismiss="modal">
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
