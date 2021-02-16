<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAprobacionSolicitud.aspx.vb" Inherits="administrativo_Tesoreria_Rendiciones_AppRendiciones_FrmAprobacionSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->

    <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../../../scripts/bootstrap-4.6.0/fontawesome-5.15.2/css/all.css"  rel="stylesheet" type="text/css">
    <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap-toogle.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="col-lg-12">
                <div class="card">
                    <div class="card-header bg-danger text-white">
                        Compromiso de rendición
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-striped table-sm">
                                <thead>
                                    <tr>
                                        <th>Respuesta</th>
                                        <th>N° de pedido</th>
                                        <th>Solicitido por</th>
                                        <th>Atendido a</th>
                                        <th>Compromiso</th>
                                        <th>Responsable de la rendición</th>
                                        <th>Monto asignado (S/)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th scope="row">
                                            <a href="#" data-toggle="modal" data-target="#exampleModalAprobar">
                                                <i class='fa-lg fa fa-check-circle' data-toggle="tooltip" data-placement="top" title="Aprobar"></i>
                                            </a>&nbsp;
                                            <a href="#" data-toggle="modal" data-target="#exampleModalRechazar">
                                                <i class='fa-lg fa fa-times' style="color: red;" data-toggle="tooltip" data-placement="top" title="Rechazar"></i>
                                            </a>
                                        </th>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td>
                                            <button type="button" class="btn btn-secondary" data-toggle="tooltip" data-placement="top" title="Ver compromiso">
                                                Ver
                                            </button>
                                        </td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                    </tr>
                                    <tr>
                                        <th scope="row">
                                            <a href="#" data-toggle="tooltip" data-placement="top" title="Aprobar">
                                                <i class='fa-lg fa fa-check-circle'></i>
                                            </a>&nbsp;
                                            <a href="#" data-toggle="tooltip" data-placement="top" title="Rechazar">
                                                <i class='fa-lg fa fa-times' style="color: red;"></i>
                                            </a>
                                        </th>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td>
                                            <button type="button" class="btn btn-secondary" data-toggle="tooltip" data-placement="top" title="Ver compromiso">
                                                Ver
                                            </button>
                                        </td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                        <td><input type="text" class="form-control form-control-sm" readonly="true"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Modal Aprobar-->
    <div class="modal fade" id="exampleModalAprobar" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Registrar fechas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form class="form-horizontal">
                        <div class="card">
                            <div class="card-body">
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <label class="col-sm-4">Entrega de dinero</label>
                                            <div class="col-sm-8">
                                                <input type="date" class="form-control form-control-sm">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <label class="col-sm-4">Limite de rendición</label>
                                            <div class="col-sm-8">
                                                <input type="date" class="form-control form-control-sm">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-success">Guardar</button>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal Rechazar-->
    <div class="modal fade" id="exampleModalRechazar" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Registrar rechazo</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <label for="staticEmail" class="col-sm-3 col-form-label">Motivo:</label>
                        <div class="col-sm-9">
                            <textarea name="textarea" class="form-control form-control-sm" rows="2"
                                cols="50"></textarea>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-success">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    
    <script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.slim.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/popper.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap-toogle.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function(){
          $('[data-toggle="tooltip"]').tooltip();   
        });
    </script>
</body>
</html>
