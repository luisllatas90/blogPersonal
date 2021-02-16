<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConformidadRendicion.aspx.vb" Inherits="administrativo_Tesoreria_Rendiciones_AppRendiciones_FrmConformidadRendicion" %>

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
                            <table class="table table-striped table-sm" id="tbl_Conformidad_Rendicion">
                                <thead>
                                    <tr>
                                        <th>N° de solicitud</th>
                                        <th>Detalle de solicitud</th>
                                        <th>N° pedido</th>
                                        <th>Solicitante</th>
                                        <th>Actividad</th>
                                        <th>Compromiso</th>
                                        <th>Responsable a rendir</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td>                                            
                                            <button type="button" class="btn btn-secondary">
                                                    Ver
                                            </button>
                                        </td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td>
                                            <button type="button" class="btn btn-secondary" data-toggle="modal" data-target="#exampleModal">
                                                Ver
                                            </button>
                                        </td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                    </tr>
                                    <tr>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td>
                                            <button type="button" class="btn btn-secondary">
                                                Ver
                                            </button>
                                        </td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                        <td>
                                            <button type="button" class="btn btn-secondary" data-toggle="modal" data-target="#exampleModal">
                                                Ver
                                            </button>
                                        </td>
                                        <td><input type="text" class="form-control form-control-sm"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Registrar observación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <div class="col-sm-12">
                            <span>Texto</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-success">Dar Conformidad</button>
                </div>
            </div>
        </div>
    </div>
    
    <script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.slim.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/popper.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap-toogle.min.js" type="text/javascript"></script>
    <script src="../../../../scripts/bootboxjs/bootbox.all.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="js/usat.recursos.js"></script>
    <script src="js/FrmConformidadRendicion.js"></script>
</body>
</html>
