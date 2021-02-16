<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCrearRendicion.aspx.vb"
    Inherits="administrativo_Tesoreria_Rendiciones_nuevavista_frmCrearRendicion" %>
    <!doctype html>
    <html lang="en">

    <head>
        <!-- Required meta tags -->
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">

        <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap.css" rel="stylesheet" type="text/css" />
        <script src="../../../../scripts/bootstrap-4.6.0/js/jquery-3.5.1.slim.min.js" type="text/javascript"></script>
        <script src="../../../../scripts/bootstrap-4.6.0/js/bootstrap.min.js" type="text/javascript"></script>

        <link href="../../../../scripts/bootstrap-4.6.0/fontawesome-5.15.2/css/all.css" rel="stylesheet"
            type="text/css">
        <link href="../../../../scripts/bootstrap-4.6.0/css/bootstrap-toogle.min.css" rel="stylesheet"
            type="text/css" />


        <title>Hello, world!</title>
    </head>

    <body>
        <div style="margin: 1%;">
            <div class="card">
                <div class="card-header" style="background-color: #e33439;">
                    <h5 style="color: white;">Rendición de cuentas</h5>
                </div>
                <div class="card-body">

                    <form>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Responsable de la
                                rendición:</label>
                            <div class="col-sm-9">
                                <input type="text" readonly class="form-control form-control-sm" id="staticEmail"
                                    value="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">N° Pedido:</label>
                            <div class="col-sm-3">
                                <input type="text" readonly class="form-control form-control-sm" id="staticEmail"
                                    value="">
                            </div>
                            <label for="staticEmail" class="col-sm-3 col-form-label">Fecha:</label>
                            <div class="col-sm-3">
                                <input type="text" readonly class="form-control form-control-sm" id="staticEmail"
                                    value="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Atendido:</label>
                            <div class="col-sm-3">
                                <input type="text" readonly class="form-control form-control-sm" id="staticEmail"
                                    value="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-9 col-form-label"></label>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <a type="submit" class="btn btn-info" style="width: 100%;">Documento de
                                        egreso</a>
                                </div>
                                <div class="form-group">
                                    <a href="frmCrearDocumento.aspx" class="btn btn-warning"
                                        style="width: 100%;">Agregar
                                        registro</a>
                                </div>
                                <div class="form-group">
                                    <a href="#" class="btn btn-success" data-toggle="modal" data-target="#exampleModal"
                                        style="width: 100%;">Agregar
                                        planilla de movilidad</a>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-12">
                                <table class="table table-striped table-hover">
                                    <thead>
                                        <tr>
                                            <th scope="col">#</th>
                                            <th scope="col">Tipo</th>
                                            <th scope="col">Serie-Número</th>
                                            <th scope="col">Fecha</th>
                                            <th scope="col">Institución /Empresa /Persona independiente</th>
                                            <th scope="col">Importe</th>
                                            <th scope="col">Observación</th>
                                            <th scope="col" style="width: 20%;">Opciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <th scope="row">1</th>
                                            <td>Factura</td>
                                            <td>f001-56536</td>
                                            <td>21/01/2020</td>
                                            <td>Cevichera Jhon SAC</td>
                                            <td>15 soles</td>
                                            <td>Ninguna</td>
                                            <td>
                                                <div class="list-group mostrar-opciones">
                                                    <a href="#"
                                                        class="list-group-item list-group-item-action active btn_mostrar_opciones"
                                                        id="mostra_1">
                                                        Ver opciones
                                                    </a>
                                                </div>

                                                <div class="list-group ocultar-opciones" style="display: none;">
                                                    <a href="#"
                                                        class="list-group-item list-group-item-action active btn_ocultar_opciones"
                                                        id="ocular_1">
                                                        Ocultar opciones
                                                    </a>
                                                    <a href="#" class="list-group-item list-group-item-action">
                                                        <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                    </a>
                                                    <a href="frmCrearDocumento.aspx"
                                                        class="list-group-item list-group-item-action">
                                                        <i class="fa fa-edit fa-3" aria-hidden="true"></i> - Editar
                                                    </a>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

    </body>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #007bff;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Agregar Planilla</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <label for="staticEmail" class="col-sm-3 col-form-label">Documento :</label>
                        <div class="col-sm-9">
                            <input type="file" class="form-control form-control-sm" id="staticEmail">
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-primary">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    </html>
    <script type="text/javascript">

        $(document).ready(function () {

            $(".btn_mostrar_opciones").click(function (e) {

                $(".ocultar-opciones").show();
                $(".mostrar-opciones").hide();

            });

            $(".btn_ocultar_opciones").click(function (e) {

                $(".ocultar-opciones").hide();
                $(".mostrar-opciones").show();

            });

        });

    </script>