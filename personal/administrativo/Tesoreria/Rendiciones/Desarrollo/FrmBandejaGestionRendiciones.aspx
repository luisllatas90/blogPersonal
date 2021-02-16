<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmBandejaGestionRendiciones.aspx.vb"
    Inherits="administrativo_Tesoreria_Rendiciones_nuevavista_frmListarRendiciones" %>
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

        <div class="container-fluid" style="margin-top: 1%;">
            <div class="card">
                <div class="card-header" style="background-color: #e33439;">
                    <h5 style="color: white;">Lista de rendiciones</h5>
                </div>
                <div class="card-body">

                    <form>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-2 col-form-label">Estado:</label>
                            <div class="col-sm-6">
                                <select class="form-control form-control-sm" id="cbx_EstadoRendicion">
                                    <option value="P">Pendientes</option>
                                    <option value="F">Finalizadas</option>
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
                                <a id="btn_ObtenerListaRendicion" class="btn btn-primary"
                                    style="width: 100%;">Consultar</a>
                            </div>
                        </div>

                        <div class="form-group row">
                            <div class="col-sm-12">
                                <table class="table table-striped table-hover" id="tbl_Rendiciones">
                                    <thead>
                                        <tr>
                                            <th scope="col">#</th>
                                            <th scope="col">Fecha</th>
                                            <th scope="col">Atendido</th>
                                            <th scope="col">Observacion</th>
                                            <th scope="col">Monto</th>
                                            <th scope="col">Estado</th>
                                            <th scope="col" style="width: 20%;">Opciones</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
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
    <script src="../../../../academico/assets/js/jquery.js" type="text/javascript"></script>
    <script src="js/usat.recursos.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            const g_List_Opciones_Tbl = [
                {
                    Clase_Btn: "btn_ver_detalle",
                    Descripcion: " Ver registro de rendición",
                    Link: "#",
                    Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>'
                },
                {
                    Clase_Btn: "btn_realizar_rendicion",
                    Descripcion: " Realizar rendición",
                    Link: "frmCrearRendicion.aspx",
                    Icono: '<i class="fa fa-file fa-3" aria-hidden="true"></i>'
                },
                {
                    Clase_Btn: "btn_solicitar_devolucion",
                    Descripcion: " Solicitar devolución",
                    Link: "#",
                    Icono: '<i class="fa fa-envelope-open fa-3" aria-hidden="true"></i>'
                },
                {
                    Clase_Btn: "btn_Imprimir",
                    Descripcion: " Imprimir",
                    Link: "#",
                    Icono: '<i class="fa fa-print fa-3" aria-hidden="true"></i>'
                }
            ];

            $(".btn-solicitar-devolucion").click(function (e) {
                Modal_Confirmacion(
                    "¿Esta seguro que desea realizar esta acción?"
                    , "Se generara un correo al Director(a) de finanzas, solicitando la devolución del excedente gastado"
                    + " por un valor de S/.500 soles."
                );
            });


            let ObtenerListaRendicion = function (p_nTipEstado = 0, p_bFlgEstado = "") {
                let l_arrayTabla = [];

                let l_Data = {
                    nTipEstado: p_nTipEstado,
                    bFlgEstado: p_bFlgEstado
                }

                $.ajax({

                    type: "POST",
                    url: "controller/BandejaGestionRendicionesController.aspx",
                    data: { Funcion: "ObtenerListaRendicion", Data: JSON.stringify(l_Data) },
                    dataType: "json",
                    cache: false,

                    success: function (data) {

                        if (data.LogError.bFlag == false) {
                            $.each(data.Resultado, function (index, item) {
                                l_arrayTabla.push(
                                    [
                                        (index + 1)
                                        , item.dFechaEgreso
                                        , item.cDesAtendido
                                        , item.cDescripcion
                                        , item.nDesImporte
                                        , "Finalizados"
                                        , CrearHtml_Opciones_Tabla_Array(g_List_Opciones_Tbl, item.nCodigo, 0, 0)
                                    ]
                                );
                            });
                            CrearHtml_Tabla("tbl_Rendiciones", l_arrayTabla);
                        } else {
                            Modal_Error(data.LogError.cMensaje);
                        }

                    },
                    error: function (result) {

                    }
                });
            }


            $("#btn_ObtenerListaRendicion").click(function (e) {
                ObtenerListaRendicion(
                    1, $("#cbx_EstadoRendicion").val()
                );
            })
        });

    </script>