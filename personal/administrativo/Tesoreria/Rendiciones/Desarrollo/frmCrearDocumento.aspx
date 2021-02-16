<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCrearDocumento.aspx.vb"
    Inherits="administrativo_Tesoreria_Rendiciones_nuevavista_frmCrearDocumento" %>
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
                    <h5 style="color: white;">Registro de Documento a Rendir</h5>
                </div>
                <div class="card-header" style="background-color:#007bff;">
                    <div class="form-group row" style="color: white;">
                        <label for="staticEmail" class="col-sm-3 col-form-label">Imp. Entregado : <a>1200 SOLES</a></label>
                        <label for="staticEmail" class="col-sm-3 col-form-label">Imp. Rendido : <a></a></label>
                        <label for="staticEmail" class="col-sm-3 col-form-label">Imp. Devuelto : <a></a> </label>
                        <label for="staticEmail" class="col-sm-3 col-form-label">Saldo : <a></a></label>                        
                    </div>
                </div>
                <div class="card-body">

                    <form>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Nro. Ruc :</label>
                            <div class="col-sm-3">
                                <input type="text" class="form-control form-control-sm" id="staticEmail" value="">
                            </div>
                            <div class="col-sm-3">
                                <a type="submit" class="btn btn-primary" style="width: 100%;">Consultar RUC Sunat</a>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Empresa/Institución/
                                Persona Independiente:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control form-control-sm" id="staticEmail" value="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Dirección:</label>
                            <div class="col-sm-9">
                                <input type="text" class="form-control form-control-sm" id="staticEmail" value="">
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Tipo de Documento: </label>
                            <div class="col-sm-3">
                                <select class="form-control form-control-sm">
                                    <option value="1">Factura de compra</option>
                                    <option value="1">Boleta de compra</option>
                                </select>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Fecha:</label>
                            <div class="col-sm-3">
                                <input type="date" class="form-control form-control-sm" id="staticEmail" value="">
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Nro. Operación::</label>
                            <div class="col-sm-3">
                                <input type="text" class="form-control form-control-sm" id="staticEmail" value="">
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Importe:</label>
                            <div class="col-sm-3">
                                <input type="text" class="form-control form-control-sm" id="staticEmail" value="">
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Archivo adjunto:</label>
                            <div class="col-sm-6">
                                <input type="file" class="form-control form-control-sm" id="staticEmail">
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label">Observación:</label>
                            <div class="col-sm-9">
                                <textarea name="textarea" class="form-control form-control-sm" rows="2" cols="50"></textarea>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="staticEmail" class="col-sm-3 col-form-label"></label>
                            <div class="col-sm-3">
                                <a type="submit" class="btn btn-success" style="width: 100%;">
                                    <i class="fa fa-save"></i>
                                    Grabar
                                </a>
                            </div>
                            <div class="col-sm-3">
                                <a href="frmCrearRendicion.aspx" class="btn btn-danger" style="width: 100%;">Cancelar</a>
                            </div>
                        </div>

                    </form>
                </div>
            </div>
        </div>

    </body>

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