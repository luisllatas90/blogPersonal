<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmBandejaValidacionRendiciones.aspx.vb"
    Inherits="administrativo_Tesoreria_Rendiciones_nuevavista_frmBandejaRendiciones" %>
    <!doctype html>
    <html lang="en">

    <head>
        <!-- Required meta tags -->
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <meta http-equiv="Expires" content="0">
        <meta http-equiv="Last-Modified" content="0">
        <meta http-equiv="Cache-Control" content="no-cache, mustrevalidate">
        <meta http-equiv="Pragma" content="no-cache">

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
        <div class="loader"></div>
        <div class="container-fluid" style="margin-top: 1%;">
            <div class="card">
                <div class="card-header" style="background-color: #e33439;">
                    <h5 class="vista_jefe_inmediato" style="color: white;">Consultar rendición de cuentas de
                        trabajadores</h5>
                    <h5 class="vista_asistente_contable" style="color: white;">Rendiciones de cuentas por revisar
                        contabilidad</h5>
                    <h5 class="vista_jefe_tesoreria" style="color: white;">Finalización de rendiciones</h5>
                    <h5 class="vista_jefe_contabilidad" style="color: white;">Dar Seguimiento a la rendición</h5>
                </div>
                <div class="card-body">

                    <form>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-2 col-form-label">Estado:</label>
                            <div class="col-sm-6">
                                <select class="form-control form-control-sm" aria-label="Default select example">
                                    <option value="1">Pendientes</option>
                                    <option value="1">Finalizadas</option>
                                </select>
                            </div>
                            <label for="staticEmail" class="col-sm-1 col-form-label">Filtro:</label>
                            <div class="col-sm-3">
                                <input type="email" class="form-control form-control-sm" id="exampleInputEmail1"
                                    aria-describedby="emailHelp">
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-9"></div>
                            <div class="col-sm-3">
                                <a type="submit" class="btn btn-primary" style="width: 100%;">Consultar</a>
                            </div>
                        </div>

                        <div class="vista_jefe_inmediato">
                            <div class="form-group row">
                                <label for="staticEmail" class="col-sm-2 col-form-label">Dependencia:</label>
                                <div class="col-sm-6">
                                    <input type="text" readonly class="form-control form-control-sm" id="staticEmail"
                                        value="">
                                </div>
                                <label for="staticEmail" class="col-sm-1 col-form-label">Año:</label>
                                <div class="col-sm-3">
                                    <input type="text" readonly class="form-control form-control-sm" id="staticEmail"
                                        value="">
                                </div>
                            </div>

                            <div class="form-group row" id="div_tbl_jefe_inmediato">
                                <div class="col-sm-12">
                                    <table class="table table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">Responsable de la rendición</th>
                                                <th scope="col">Monto a rendir (S/)</th>
                                                <th scope="col">N° de solicitud</th>
                                                <th scope="col">N° de pedido</th>
                                                <th scope="col">Solicitado por</th>
                                                <th scope="col">Atendido</th>
                                                <th scope="col">Estado de la rendición</th>
                                                <th scope="col" style="width: 18%;">Opciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Juan Perez</td>
                                                <td>1200 SOLES</td>
                                                <td>40045</td>
                                                <td>40045</td>
                                                <td>Jose Gonzales</td>
                                                <td>Juan Jose</td>
                                                <td><a class="badge badge-danger">Pendiente</a></td>
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
                                                        <a href="#"
                                                            class="list-group-item list-group-item-action btn-aprobar-jefe-inmediato">
                                                            <i class="fa fa-check fa-3" aria-hidden="true"></i> -
                                                            Aprobar
                                                        </a>
                                                        <a href="#"
                                                            class="list-group-item list-group-item-action btn-rechazar"
                                                            data-toggle="modal" data-target="#exampleModal">
                                                            <i class="fa fa-times fa-3" aria-hidden="true"></i> -
                                                            Rechazar
                                                        </a>
                                                        <a href="#"
                                                            class="list-group-item list-group-item-action btn-ver-registro-rendicion"
                                                            data-toggle="modal" data-target="#modal-detalle-rendicion">

                                                            <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                            registro de la rendición
                                                        </a>
                                                        <a href="#" class="list-group-item list-group-item-action">
                                                            <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                            comprobante de rendición
                                                        </a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="vista_asistente_contable">
                            <div class="form-group row" id="div_tbl_asistente_contable">
                                <div class="col-sm-12">
                                    <table class="table table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">N° de solicitud</th>
                                                <th scope="col">N° pedido</th>
                                                <th scope="col">Responsable a rendir</th>
                                                <th scope="col">Imp. Entregado</th>
                                                <th scope="col">Imp. Rendido (S/.)</th>
                                                <th scope="col">Saldo (S/.)</th>
                                                <th scope="col">¿Tiene documentos de emisión física en la rendición?
                                                </th>
                                                <th scope="col">Validación de jefe inmediato</th>
                                                <th scope="col" style="width: 18%;">Opciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th scope="row">1</th>
                                                <td>Juan Perez</td>
                                                <td>1200 SOLES</td>
                                                <td>40045</td>
                                                <td>40045</td>
                                                <td>Jose Gonzales</td>
                                                <td>Juan Jose</td>
                                                <td>Pendiente</td>
                                                <td>Pendiente</td>
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
                                                            detalle
                                                        </a>
                                                        <a href="#"
                                                            class="list-group-item list-group-item-action btn-ver-registro-rendicion"
                                                            data-toggle="modal" data-target="#modal-detalle-rendicion">
                                                            <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                            registro de rendición
                                                        </a>
                                                        <a href="#" class="list-group-item list-group-item-action">
                                                            <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                            hoja de rendición
                                                        </a>
                                                        <a href="#"
                                                            class="list-group-item list-group-item-action btn-aprobar-asistente-contable">
                                                            <i class="fa fa-check fa-3" aria-hidden="true"></i> -
                                                            Aprobar
                                                        </a>
                                                        <a href="#"
                                                            class="list-group-item list-group-item-action btn-rechazar"
                                                            data-toggle="modal" data-target="#exampleModal">
                                                            <i class="fa fa-times fa-3" aria-hidden="true"></i> -
                                                            Observar
                                                        </a>


                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="vista_jefe_tesoreria">

                            <div class="form-group row">
                                <div class="col-sm-3">
                                    <a type="submit" class="btn btn-outline-info" style="width: 100%;">
                                        <i class="fa fa-plus-square fa-3" aria-hidden="true"></i>
                                        Seleccionar todas
                                    </a>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-3">
                                    <a type="submit" class="btn btn-success" style="width: 100%;">
                                        <i class="fa fa-check fa-3" aria-hidden="true"></i>
                                        Finalizar
                                    </a>
                                </div>
                            </div>

                            <div class="form-group row" id="div_tbl_asistente_contable">
                                <div class="col-sm-12">
                                    <table class="table table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">N° de solicitud</th>
                                                <th scope="col">N° pedido</th>
                                                <th scope="col">Tipo de entrega</th>
                                                <th scope="col">Fecha de entrega</th>
                                                <th scope="col">Responsable a rendir</th>
                                                <th scope="col">Importes Rendicion</th>
                                                <th scope="col">Estado</th>
                                                <th scope="col" style="width: 20%;">Opciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    <input type="checkbox" id="exampleCheck1">
                                                </th>
                                                <td>40045</td>
                                                <td>40045</td>
                                                <td>Fisica</td>
                                                <td>22/01/2021</td>
                                                <td>Jose Gonzales</td>
                                                <td>
                                                    <div>Entregado(S/.): 1000</div>
                                                    <div>Rendido(S/.): 1000</div>
                                                    <div>Devuelto(S/.): 1000</div>
                                                    <div>Saldo (S/.): 1000</div>
                                                </td>
                                                <td>Finalizada</td>
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
                                                            detalle
                                                        </a>
                                                        <a href="#" class="list-group-item list-group-item-action">
                                                            <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                            hoja de rendición
                                                        </a>
                                                        <a href="#" class="list-group-item list-group-item-action">
                                                            <i class="fa fa-search fa-3" aria-hidden="true"></i> - Ver
                                                            Documento de egreso por descuento (Planilla / liquidación) o
                                                            cargo de pensión
                                                        </a>
                                                        <a href="#" class="list-group-item list-group-item-action">
                                                            <i class="fa fa-check fa-3" aria-hidden="true"></i> -
                                                            Finalizar
                                                        </a>

                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="vista_jefe_contabilidad">

                            <div class="form-group row" id="div_tbl_asistente_contable">
                                <div class="col-sm-12">
                                    <table class="table table-striped table-hover">
                                        <thead>
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">DNI</th>
                                                <th scope="col">Nombre completo</th>
                                                <th scope="col">Puesto de trabajo</th>
                                                <th scope="col">Fuente</th>
                                                <th scope="col">Código CeCo</th>
                                                <th scope="col">Centro de costo</th>
                                                <th scope="col">Monto (S/)</th>
                                                <th scope="col" style="width: 20%;">Opciones</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th scope="row">
                                                    1
                                                </th>
                                                <td>742851236</td>
                                                <td>Claudia Lozano Alarcon</td>
                                                <td>Analista de calidad</td>
                                                <td>Indefinida</td>
                                                <td>3542345</td>
                                                <td>
                                                    Indefinido
                                                </td>
                                                <td>S/.2500</td>
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
                                                            detalle
                                                        </a>
                                                        <a href="#" class="list-group-item list-group-item-action">
                                                            <i class="fas fa-arrow-alt-circle-right fa-3"
                                                                aria-hidden="true"></i>
                                                            - Enviar registro de rendiciones pendientes
                                                        </a>
                                                    </div>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
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
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Registrar observación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <label for="staticEmail" class="col-sm-3 col-form-label">Observación:</label>
                        <div class="col-sm-9">
                            <textarea name="textarea" class="form-control form-control-sm" rows="2"
                                cols="50"></textarea>
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
    <div class="modal fade" id="modal-detalle-rendicion" tabindex="-1" aria-labelledby="exampleModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #e33439;">
                    <h5 class="modal-title" id="exampleModalLabel" style="color: white;">Rendición de cuentas</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
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
                                        </tr>
                                    </tbody>
                                </table>
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

    </html>

    <script src="../../../../scripts/bootboxjs/bootbox.all.min.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js" type="text/javascript"></script>
    <script src="js/usat.recursos.js" type="text/javascript"></script>



    <script type="text/javascript">

        $(document).ready(function () {

            $(".vista_jefe_inmediato").show();
            $(".vista_asistente_contable").hide();
            $(".vista_jefe_tesoreria").hide();
            $(".vista_jefe_contabilidad").hide();

            $(".btn_mostrar_opciones").click(function (e) {

                $(".ocultar-opciones").show();
                $(".mostrar-opciones").hide();

            });

            $(".btn_ocultar_opciones").click(function (e) {

                $(".ocultar-opciones").hide();
                $(".mostrar-opciones").show();

            });

            $(".btn-rechazar").click(function (e) {

            });

            $(".btn-aprobar-jefe-inmediato").click(function (e) {
                let dialog = bootbox.dialog({
                    title: g_Des_Titulo_Confirmacion_Defecto,
                    message: "Realizar confirmación",
                    size: 'large',
                    buttons: {
                        cancel: {
                            label: "Cancelar",
                            className: 'btn-danger',
                            callback: function () {

                            }
                        },
                        ok: {
                            label: "Confirmar",
                            className: 'btn-info',
                            callback: function () {

                            }
                        }
                    }
                });
            });


            $(".btn-aprobar-asistente-contable").click(function (e) {
                console.log(Mostrar_Modal_Confirmacion);
                let dialog = bootbox.dialog({
                    title: g_Des_Titulo_Confirmacion_Defecto,
                    message: "Realizar confirmación",
                    size: 'large',
                    buttons: {
                        cancel: {
                            label: "Cancelar",
                            className: 'btn-danger',
                            callback: function () {

                            }
                        },
                        ok: {
                            label: "Confirmar",
                            className: 'btn-info',
                            callback: function () {

                            }
                        }
                    }
                });
            });



            let PostDataFun = function() {

                let dataobj = {
                    ObjData: {
                        Obj_Conexion: {
                            Key_Origen: "APP_WEB",
                            Key_Aplicacion: "APP_WEB_MASTER_1",
                            Ip_Terminal: "192.168.0.6"
                        }
                    }
                }

                $.ajax({

                    type: "POST",
                    url: "LogicaWS.aspx",
                    data: { Funcion: "PostDataFun", dataobj : JSON.stringify(dataobj) },
                    dataType: "json",
                    cache: false,

                    success: function (data) {
                        console.log({ data });
                    },
                    error: function (result) {

                    }
                });
            }

            PostDataFun();

        });

    </script>