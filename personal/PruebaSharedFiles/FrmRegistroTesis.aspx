<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistroTesis.aspx.vb"
    Inherits="GestionInvestigacion_FrmRegistroTesis" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Cargamos css -->
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <!-- Cargamos JS -->

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script src="../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/wow.min.js'></script>

    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <%--<script type="text/javascript" src='../assets/js/jquery.accordion.js'></script>--%>

    <script type="text/javascript" src='../assets/js/materialize.js'></script>

    <%--<script type="text/javascript" src='../assets/js/form-elements.js'></script>--%>
    <%--<script type="text/javascript" src='../assets/js/select2.js'></script>--%>

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>
    <title>Registro de Tesis</title>

    <script type="text/javascript">
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            console.log(sw);
        }
        function SubirArchivo() {
            //fnLoading(true)
            //var flag = false;

            try {

                var data = new FormData();
                data.append("action", "SurbirArchivo");
                data.append("codigo", 'OAA2ADkANQA='); //ABAD GALLARDO - ADMINISTRACIÓN DE EMPRESAS
                data.append("tipo", 'INFORME');
                var files = $("#fileinforme").get(0).files;
                if (files.length > 0) {
                    data.append("ArchivoASubir", files[0]);
                }
                $("#lblRespuesta").val("");
                console.log(data);
                if (files.length > 0) {
                    $.ajax({
                        type: "POST",
                        url: "DataJson/Tesis.aspx",
                        data: data,
                        //dataType: "json",
                        //cache: false,
                        contentType: false,
                        processData: false,
                        //async: false,
                        success: function(data) {
                            $("#lblRespuesta").val(data);
                            console.log(data);
                            //fnLoading(false)
                        },
                        error: function(result) {
                            console.log(result);
                            $("#lblRespuesta").val(result.toString());

                            //flag = false;
                            //console.log(result);
                        }
                    });
                }
                //return flag;
                //fnLoading(false);
            }
            catch (err) {
                //return false;
                console.log(err)
                $("#lblRespuesta").val(err.toString());

                //fnLoading(false);
            }
            //fnLoading(false);
        }
    </script>

    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
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
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
            text-align: left;
        }
        .modal
        {
            overflow: auto !important;
        }
        .input-group .form-control
        {
            z-index: 0;
        }
        #tbObservaciones tbody tr td, #tbObservacionesInforme tbody tr td
        {
            padding: 0px;
            margin: 0px;
            font-size: 11px;
        }
        #tbObservaciones tbody tr th, #tbObservacionesInforme tbody tr th
        {
            padding: 2px;
            margin: 0px;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
            color: White;
            background-color: #E33439;
        }
    </style>
</head>
<body class="">
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="panel-piluku">
                <div class="panel-body">
                    <div class="row">
                        <div class="form-group">
                            <label class="col-xs-3 col-sm-3 control-label">
                                Informe Final (PDF,RAR):<br />
                                Tamaño máximo: 45MB
                            </label>
                            <div class="col-xs-4 col-sm-4">
                                <input type="file" id="fileinforme" name="fileinforme" />
                                <div id="file_informe">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-xs-3 col-sm-3 control-label">
                                Mensaje
                            </label>
                            <div class="col-md-8">
                                <textarea id="lblRespuesta" class="form-control" rows="10"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="Div2">
                        <center>
                            <button type="button" id="btnGuardarInforme" class="btn btn-primary" style="display: inline-block"
                                onclick="SubirArchivo()">
                                Guardar</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
