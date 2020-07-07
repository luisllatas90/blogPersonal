<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmVerAdjuntos.aspx.vb" Inherits="logistica_frmVerAdjuntos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"> 
    
 <link href="../private/estilo.css"rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
    
    <script type="text/javascript" src='../assets/js/jquery.js'></script>
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css' />
    <script type="text/javascript" src='../assets/js/app.js'></script>
    <%--<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>--%>
    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>
    <script type="text/javascript" src='../assets/js/wow.min.js'></script>
    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>
    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>
    <%--    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../../assets/js/core.js'></script>--%>
    <script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>
    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>
    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>
    <script type="text/javascript" src='../assets/js/funciones.js'></script>

    <%--    <script type="text/javascript" src="../../assets/js/DataJson/jsselect.js?x=10"></script>

    <script type="text/javascript" src='../../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../../assets/js/select2.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.multi-select.js'></script>--%>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>
    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
    
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    
    <script type="text/javascript">
        $(document).ready(function() {
            //alert('a');
            fnVer($("#cod_rco").val());
        });
        
        function ModalAdjuntar2(cod_dpe, cod_ped) {
            $("#cod_dpe").val(cod_dpe)
            $("#cod_ped").val(cod_ped)
            $("#txtfile").val("");
            fnVer(cod_dpe)
            $('.AdjuntoProy').attr("data-toggle", "modal");
            $('.AdjuntoProy').attr("data-target", "#mdRegistro");
            //alert('a');
        }

        function fnVer(c) {
            $.ajax({
                type: "POST",
                url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                data: { "action": "Ver", "cod1": c , "idTabla": 6},
                dataType: "json",
                cache: false,
                success: function(data) {
                    //console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td>' + (i + 1) + "" + '</td>';
                        tb += '<td><i  class="' + data[i].nExtension + '"></i> ' + data[i].nArchivo + '</td>';
                        tb += '<td>';
                        tb += '<button onclick="fnDownload(' + data[i].cCod + ');" class="btn btn-primary"><i  class=" ion-android-download"><span></span></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                    if (filas > 0) $('#mdFiles').modal('toggle');
                    $('#tbFiles').html(tb);
                },
                error: function(result) {
                }
            });
        }
        function fnDownload(id_ar) {
            var flag = false;
            var form = new FormData();
            form.append("action", "Download");
            form.append("cod_Arch", id_ar);
            // alert();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                data: form,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                async: false,
                success: function(data) {
                    flag = true;
                    //console.log(data);
                    var file = 'data:application/octet-stream;base64,' + data[0].File;
                    var link = document.createElement("a");
                    link.download = data[0].Nombre;
                    link.href = file;
                    link.click();
                    //fnMensaje(data[1].tipo, data[1].msje);
                    if (navigator.userAgent.indexOf("NET") > -1) {

                        var param = { 'Id': id_ar };
                        // OpenWindowWithPost("DataJson/DescargarArchivo.aspx", "", "NewFile", param);
                        window.open("../DataJson/Logistica/DescargarArchivo.aspx?Id=" + id_ar, 'ta', "");

                    }
                },
                error: function(result) {
                    //console.log(result);
                    fnMensaje(data[1].tipo, data[1].msje);
                    flag = false;
                }
            });
            return flag;

        }
        function fnGuardar() {
            if ($("#txtfile").val() == "") {
                $("#divMessage").html("<p>Debe Selecionar un Archivo.</p>")
            } else {
                $("#btnGuardar").attr("disabled", true);
                fnLoadingDiv("divLoading", true);
                SubirArchivo($("#cod_rco").val(), $("#cod_rco").val());
                fnLoadingDiv("divLoading", false);
                $("#btnGuardar").removeAttr("disabled");
                fnVer($("#cod_rco").val())
            }
        }
        function SubirArchivo(c, n) {
            var flag = false;
            var form = new FormData();
            var files = $("#txtfile").get(0).files;
            //console.log(files);
            // Add the uploaded image content to the form data collection
            if (files.length > 0) {
//                
                form.append("action", "Upload")
                form.append("cod1", $("#cod_rco").val())
                form.append("cod2", $("#cod_rco").val())
                form.append("idTabla", 6)
                form.append("UploadedImage", files[0]);
            }
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                data: form,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                success: function(data) {
                    flag = true;
                    $("#txtfile").val("");                

                },
                error: function(result) {
                    //console.log(result);
                    $("#divMessage").html("<p>" & data[0].msje & "</p>");
                    flag = false;
                }
            });
            return flag;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="row" id="divPrincipal">
        <div class="" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #3871B0; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel3">
                            Adjuntar Archivos</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divMessage">
                        </div>
                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                        <div class="row">
                            <div id="msje">
                            </div>
                        </div>
                        <div class="row">
                            <input type="hidden" id="cod_rco" value="" runat="server" />
                            <input type="hidden" id="action" value="" runat="server" />
                        </div>
                        <div class="row">
                            <table style="width: 100%;" class="display dataTable">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">
                                        </th>
                                        <th style="width: 80%">
                                            Archivo
                                        </th>
                                        <th style="width: 10%">
                                            Opci&oacute;n
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbFiles">
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan="3">
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Adjunto:</label>
                                <div class="col-sm-8">
                                    <input type="file" id="txtfile" name="txtfile" class="form-control" runat="server" />
                                </div>
                                <div style="float: left;" id="divLoading" class="hidden">
                                    <img id="imgload" src="../assets/images/loading.GIF"></div>
                            </div>
                        </div>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" id="btnGuardar" class="btn btn-primary" onclick="fnGuardar();">
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
    </form>
</body>
</html>
