<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSolicitudEntrega.aspx.vb" Inherits="administrativo_Tesoreria_Rendiciones_AppRendiciones_FrmSolicitudEntrega" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solicitud Entrega a Rendir</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/>
    <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../../scripts/bootstrap-4.6.0/fontawesome-5.15.2/css/all.css"  rel="stylesheet" type="text/css">
    <link href="../../../../scripts/bootstrap-4.6.0/css/datatables/jquery.dataTables.min.css"  rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-header bg-danger text-white">
                        Solicitud de entrega a rendir
                    </div>
                    <div class="card-body">
                        <form class="form-horizontal">
                            <div class="card">
                                <div class="card-body">
                                    <!--<div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-3"><strong>Centro de Costo</strong></label>
                                                <div class="col-sm-9">
                                                    <div class="input-group">
                                                        <input type="text" class="form-control">
                                                        <div class="input-group-append">
                                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCco">
                                                                <i class="fa fa-search" data-toggle="tooltip" data-placement="top" title="Buscar Centro de Costo"></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>-->
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-3"><strong>N° de pedido</strong></label>
                                                <div class="col-sm-9">
                                                    <div class="input-group">
                                                        <input type="text" class="form-control" readonly="true">
                                                        <div class="input-group-append" data-toggle="tooltip" data-placement="top" title="Buscar Pedido">
                                                            <button type="button" class="btn btn-primary" id="mostrarModalPedido" data-toggle="modal" data-target="#modalPedido">
                                                                <i class="fa fa-search" ></i>
                                                            </button>
                                                        </div>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <table id="tbDetallePedidos" class="table table-striped table-sm">
                                                            <thead>
                                                            </thead>
                                                            <tbody>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-3"><strong>(*) Solicitante</strong></label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="txtSolicitante" class="form-control form-control-sm" readonly="true">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-3">Correo del Solicitante</label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="txtEmail" class="form-control form-control-sm" readonly="true">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-12"><strong>Justificación de la solicitud</strong></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-3" style="text-align: center;">Actividad a desarrollar</label>
                                                <div class="col-sm-9">
                                                    <input type="text" class="form-control form-control-sm">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-3" style="text-align: center;">En beneficio de</label>
                                                <div class="col-sm-9">
                                                    <input type="text" class="form-control form-control-sm">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <label class="col-sm-6" style="text-align: center;">Fecha Inicio de la actividad</label>
                                                <div class="col-sm-6">
                                                    <input type="date" class="form-control form-control-sm">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="row">
                                                <label class="col-sm-6" style="text-align: center;">Fecha Fin de la actividad</label>
                                                <div class="col-sm-6">
                                                    <input type="date" class="form-control form-control-sm">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-12"><strong>(**) Atendido</strong></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table id="tbAtendido" class="table table-striped table-sm">
                                            <thead>
                                            </thead>
                                            <tbody>
                                                <!--<tr>
                                                    <th scope="row">
                                                        <button type="button" class="btn btn-danger btnEliminarAtendido">
                                                            <i class="fa fa-times"></i>
                                                        </button>
                                                    </th>
                                                    <td>
                                                        <div class="input-group">
                                                            <input type="text" name="txtNombreAtendido[]" class="form-control form-control-sm" readonly="true">
                                                            <div class="input-group-prepend">
                                                                <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalPersona">
                                                                    <i class="fa fa-search" data-toggle="tooltip" data-placement="top" title="Buscar Personal"></i>
                                                                </button>
                                                            </div>
                                                        </div>
                                                    </td>
                                                    <td><input type="text" name="txtCtaBancariaAtendido[]" class="form-control form-control-sm" readonly="true"></td>
                                                    <td><input type="text" name="txtBancoAtendido" class="form-control form-control-sm" readonly="true"></td>
                                                    <td><input type="number" name="txtMontoAsignadoAtendido[]" class="form-control form-control-sm"></td>
                                                    <td><input type="text" name="txtxEmailAtendido[]" class="form-control form-control-sm" readonly="true"></td>
                                                    <!--<td>
                                                        <select name="account" class="form-control">
                                                            <option>Solicitante 1</option>
                                                            <option>Atendido 1</option>
                                                          </select>
                                                    </td>
                                                </tr>-->
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <th scope="row">
                                                        <button type="button" class="btn btn-success btnAgregarAtendido" title="Agregar Atendido">
                                                            <i class="fa fa-plus"></i>
                                                        </button>
                                                    </th>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                    <!--<div class="form-group row">
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <label class="col-sm-12"><strong>(****) Responsable de la rendición</strong></label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <table class="table table-striped table-sm">
                                            <thead>
                                                <tr>
                                                    <th></th>
                                                    <th>Nombre Completo</th>
                                                    <th>Monto asignado (S/)</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <th scope="row">
                                                        <button type="button" class="btn btn-danger"><i class="fa fa-times"></i></button>
                                                    </th>
                                                    <td><input type="text" class="form-control form-control-sm"></td>
                                                    <td><input type="number" class="form-control form-control-sm"></td>
                                                </tr>
                                                <tr>
                                                    <th scope="row">
                                                        <button type="button" class="btn btn-danger"><i class="fa fa-times"></i></button>
                                                    </th>
                                                    <td><input type="text" class="form-control form-control-sm"></td>
                                                    <td><input type="number" class="form-control form-control-sm"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">Total</td>
                                                    <td><input type="number" class="form-control form-control-sm" readonly="true"></td>
                                                </tr>
                                                <tr>
                                                    <th scope="row">
                                                        <button type="button" class="btn btn-success"><i class="fa fa-plus"></i></button>
                                                    </th>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>-->
                                </div>
                            </div>
                            <p>(*) Persona que realiza el pedido a través del módulo del campus virtual correspondiente.</p>
                            <p>(**) Persona que recibe el dinero y responsable de firmar el compromiso de rendición de cuentas y autorización de descuento por planilla.</p>
                            <p>(***) En caso se trate de un trabajador, la información aparecerá llena de forma automática.</p>
                            <p>(****) Una o más personas responsables de tramitar y sustentar la rendición. Puede ser el solicitante, uno o más atendidos.</p>
                            <p><strong>Todos los campos de la solicitud son obligatorios.</strong></p>
                            <div class="form-group row">
                                <label for="staticEmail" class="col-sm-9 col-form-label"></label>
                                <div class="col-sm-3">
                                    <button type="button" class="btn btn-success" style="width: 100%;">
                                        <i class="fa fa-save"></i>&nbsp;&nbsp;Registrar
                                    </button>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Modal Centro de Costo -->
    <div class="modal fade" id="exampleModalCco" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Pedido</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <label class="col-sm-4">Descripción</label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <input type="text" class="form-control form-control-sm">
                                                    <div class="input-group-prepend">
                                                        <button type="button" class="btn btn-info" data-toggle="tooltip" data-placement="top" title="Buscar Pedido">
                                                            <i class="fa fa-search"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr/>
                                <div class="table-responsive">
                                    <table class="table table-striped table-sm">
                                        <thead>
                                            <tr>
                                                <th>Seleccione</th>
                                                <th>Descripción</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <a href="#" data-toggle="tooltip" data-placement="top" title="Seleccionar">
                                                        <i class='fa-lg fa fa-check-circle'></i>
                                                    </a>
                                                </td>
                                                <td>TI-DS-JEFATURA DE DESARROLLO DE SISTEMAS - 2020</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <a href="#" data-toggle="tooltip" data-placement="top" title="Seleccionar">
                                                        <i class='fa-lg fa fa-check-circle'></i>
                                                    </a>
                                                </td>
                                                <td>DV_SEGURIDAD, SALUD OCUPACIONAL Y MEDIO AMBIENTE 2021 - V02</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Pedido -->
    <div class="modal fade" id="modalPedido" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Pedido</h5>
                    <button type="button" id="btnCerraModalPedido2" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <label class="col-sm-4">Número de Pedido</label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <input type="number" id="txtPedidoCodigo" name="txtPedidoCodigo" class="form-control form-control-sm">
                                                    <div class="input-group-prepend">
                                                        <button type="button" id="btnListarPedido" class="btn btn-info" data-toggle="tooltip" data-placement="top" title="Buscar Pedido">
                                                            <i class="fa fa-search"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr/>
                                <div class="table-responsive">
                                    <table id="tbPedidos" class="display table table-striped table-sm">
                                        <thead>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" id="btnCerraModalPedido">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal Persona -->
    <div class="modal fade" id="modalPersona" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Persona</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <label class="col-sm-4">Apellidos</label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <input type="text" id="txtBuscaPersonaApellido" class="form-control form-control-sm">
                                                    <div class="input-group-prepend">
                                                        <button type="button" id="btnBuscaPersonaApellido" class="btn btn-info" data-toggle="tooltip" data-placement="top" title="Buscar Persona">
                                                            <i class="fa fa-search"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row">
                                            <label class="col-sm-4">DNI</label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <input type="text" id="txtBuscaPersonaDocumento" class="form-control form-control-sm">
                                                    <div class="input-group-prepend">
                                                        <button type="button" id="btnBuscaPersonaDocumento" class="btn btn-info" data-toggle="tooltip" data-placement="top" title="Buscar Persona">
                                                            <i class="fa fa-search"></i>
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr/>
                                <div class="table-responsive">
                                    <table id="tbPersona" class="display table table-sm">
                                        <thead>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/popper.min.js" type="text/javascript"></script>
    <!--<script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.slim.min.js" type="text/javascript"></script>-->
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/datatables/jquery.dataTables.min.js"></script>    
    <script src="../../../../scripts/bootstrap-4.6.0/js/sweetalert2.js" type="text/javascript"></script>
    <script src="js/usat.recursos.js" type="text/javascript"></script>
    <script src="js/frmSolicitudEntrega.js" type="text/javascript"></script>
</body>
</html>
