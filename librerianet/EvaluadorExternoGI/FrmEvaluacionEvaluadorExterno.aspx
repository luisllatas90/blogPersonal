<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEvaluacionEvaluadorExterno.aspx.vb"
    Inherits="GestionInvestigacion_FrmEvaluacionEvaluadorExterno" %>

<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Lista de Proyectos</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <%--Compatibilidad con IE--%>
    <!--<script type="text/javascript" src="../assets/js/jquery.js"></script>-->

    <script src="js/jquery.js" type="text/javascript"></script>

    <!--<script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>	-->

    <script src="js/bootstrap.min.js" type="text/javascript"></script>

    <!--<script type="text/javascript" src='../assets/js/noty/jquery.noty.js'></script>-->

    <script src="js/jquery.noty.js" type="text/javascript"></script>

    <!--<script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>-->

    <script src="js/top.js" type="text/javascript"></script>

    <!--<script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>-->

    <script src="js/default.js" type="text/javascript"></script>

    <!--<script type="text/javascript" src='../assets/js/noty/notifications-custom.js'></script>-->

    <script src="js/notifications-custom.js" type="text/javascript"></script>

    <!--<script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>-->

    <script src="js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>

    <!-- Manejo de tablas -->
    <!--<script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>-->

    <script src="js/jquery.dataTables.min.js" type="text/javascript"></script>

    <!--<link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />-->
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />
    <!--<script src="../assets/js/funcionesDataTable.js?y=1" type="text/javascript"></script>-->

    <script src="js/funcionesDataTable.js" type="text/javascript"></script>

    <!-- Piluku -->
    <!--<link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css"/>-->
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!--<link rel="stylesheet" href="../assets/css/material.css?x=1"/>-->
    <link href="css/material.css" rel="stylesheet" type="text/css" />
    <!--<link rel="stylesheet" type="text/css" href="../assets/css/style.css?y=4"/>-->
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <%-- ======================= Inicio Notificaciones =============================================--%>
    <!-- <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>-->
    <%-- ======================= Fin Notificaciones =============================================--%>
    <!--<script src="js/_General.js?x=1" type="text/javascript"></script>-->
    <style type="text/css">
        input[type="file"]
        {
            display: none;
        }
        .custom-file-upload
        {
            border: 1px solid #ccc;
            background-color: white;
            padding: 6px 12px;
            cursor: pointer;
        }
        input[type="number"]
        {
            width: 4em;
        }
        .CambiaInputs
        {
            /*border: 2px solid green;*/
            border: 1px solid #ccc;
            background-color: red;
            padding: 6px 12px;
            cursor: pointer;
        }
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
            padding-top: 3px;
        }
    </style>

    <script type="text/javascript">
        var aDataPE = [];
        $(document).ready(function() {
            //fnResetDataTableTramite('tPostulacionesEvaluar', 0, 'asc');
            //fnResetDataTableTramite('tObjetivos', 0, 'asc');
            var dt = fnCreateDataTableBasic('tPostulacionesEvaluar', 1, 'asc');
            var dt1 = fnCreateDataTableBasic('tObjetivos', 1, 'asc');
            listarPostulacionesEvaluar();
            $("#btnObjetivos").click(function() {
                $("#mdObjetivos").modal("show");
            });
        });

        function listarPostulacionesEvaluar() {
            //alert("a");
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "DataJson/operaciones.aspx",
                data: { "action": "lPostulacionesPorEvaluar", "param1": $("#hdEVE").val(), "param2": $("#hdCON").val() },
                dataType: "json",
                success: function(data) {
                    console.log("-----------");
                    console.log(data);
                    var tb = '';
                    var i = 0;
                    var mostrar = '';
                    aDataPE = data;
                    var contador = 0;
                    var filas = aDataPE.length;
                    if (filas > 0) {
                        for (i = 0; i < aDataPE.length; i++) {
                            //alert(aDataPE[i].cal_eva + "-" + aDataPE[i].rub_eva);
                            if (aDataPE[i].cal_eva != null && aDataPE[i].rub_eva != null) {
                                mostrar = "disabled";
                            } else {
                                mostrar = "";
                            }
                            tb += '<tr>';
                            tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                            tb += '<td style="text-align:center">' + aDataPE[i].tit_pos + '</td>';
                            tb += '<td style="text-align:center;color:red;font-wiegth:bold;">' + aDataPE[i].fecha_eva + '</td>';
                            //tb += '<td style="text-align:center"><a href="' + "Archivos/Postulacion/" + aDataPE[i].cod_pos +  aDataPE[i].prd_pos + '" target="_blank" style="font-weight: bold; font-style: oblique;">Información</a></td>';
                            tb += '<td style="text-align:center"><button type="button" id="btnPostulacion" name="btnPostulacion" class="btn btn-sm btn-info" onclick="InformacionPostulacion(' + aDataPE[i].cod_pos + ')" title="Información"><i class="ion-eye"></i></button></td>';
                            if (aDataPE[i].cal_eva != "" || aDataPE[i].cal_eva != null) {
                                tb += '<td style="text-align:center"><input type="number" id="txtNE[' + (i + 1) + ']" name="txtNE[' + (i + 1) + ']" min="0" max="100" size="3" oninput="maxLengthCheck(this)" maxlength="3" onKeyPress="return soloNumeros(event)" onblur="compruebaValidoEntero(' + (i + 1) + ')" ' + mostrar + ' value="' + aDataPE[i].cal_eva + '"></td>';
                            } else {
                                tb += '<td style="text-align:center"><input type="number" id="txtNE[' + (i + 1) + ']" name="txtNE[' + (i + 1) + ']" min="0" max="100" size="3" oninput="maxLengthCheck(this)" maxlength="3" onKeyPress="return soloNumeros(event)" onblur="compruebaValidoEntero(' + (i + 1) + ')" ' + mostrar + '></td>';
                            }
                            //tb += '<td style="text-align:center"><input type="file" multiple="multiple" id="filePE[' + (i + 1) + ']" name="filePE[' + (i + 1) + ']" /><i class="ion-android-upload"></i></td>';
                            //alert(aDataPE[i].rub_eva);
                            if (aDataPE[i].rub_eva != 0) {
                                //tb += '<td style="text-align:center"><a href="' + aDataPE[i].rub_eva + '" target="_blank">Rúbrica</a></td>';
                                //tb += '<td style="text-align:center"><a onclick="fnDownload(\'' + aDataPE[i].rub_eva + '\')" >Rúbrica</a></td>';
                                tb += '<td style="text-align:center"><a onclick="fnDownload(\'' + aDataPE[i].rub_eva + '\')" >Rúbrica</a></td>'
                           } else {
                                tb += '<td style="text-align:center;margin:0 auto;"><div style="float:left;width:50%"><label class="custom-file-upload"><input type="file" id="filePE[' + (i + 1) + ']" name="filePE[' + (i + 1) + ']" onchange="if(!this.value.length)ocultarColor(' + (i + 1) + ');; cambiarColor(' + (i + 1) + ');" ' + mostrar + '/><i class="fa fa-cloud-upload"></i></label></div><div style="float:right;width:50%"><div id="divFile[' + (i + 1) + ']" style="display:none;width:50%;border:1px solid green" class="btn btn-sm"><i class="ion-android-clipboard" style="color: green;"></i></div></div></td>';
                            }
                            tb += '<td style="text-align:center"><button type="button" id="btnEnviarE[' + (i + 1) + ']" name="btnEnviarE[' + (i + 1) + ']" class="btn btn-sm btn-orange" onclick="EnviarEvaluacionExterno(' + (i + 1) + ',' + aDataPE[i].cod_pos + ')" title="Enviar Revisión" ' + mostrar + '><i class="ion-arrow-right-a"></i></button></td>';
                            tb += '</tr>';
                        }
                    }
                    fnDestroyDataTableDetalle('tPostulacionesEvaluar');
                    $('#tbPostulacionesEvaluar').append(tb);
                    //fnResetDataTableTramite('tPostulacionesEvaluar', 0, 'asc');
                    fnResetDataTableBasic('tPostulacionesEvaluar', 0, 'desc');
                },
                error: function(result) {
                    alert("aa");
                    fnMensaje("warning", result)
                }
            });
        }

        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }

        /*function fnDownload(id_ar) {
        var flag = false;
        var form = new FormData();
        form.append("action", "Download3");
        form.append("IdArchivo", id_ar);
        $.ajax({
        type: "POST",
        url: "DataJson/Operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function(data) {
        console.log(data);
        flag = true;

                    var file = 'data:application/octet-stream;base64,' + data[0].File;
        var link = document.createElement("a");
        link.download = data[0].Nombre;
        link.href = file;
        link.click();
        },
        error: function(result) {
        console.log(result);
        flag = false;
        }
        });
        return flag;
        }
        */

        function cambiarColor(index) {
            if ($('#filePE\\[' + index + '\\]').val() == "") {
                $('#divFile\\[' + index + '\\]').hide();
            }
            else {
                $('#divFile\\[' + index + '\\]').show();
            }
        }

        function ocultarColor(index) {
            $('#divFile\\[' + index + '\\]').hide();
            fnMensaje("warning", "Debe seleccionar la rubrica a cargar");
        }

        function compruebaValidoEntero(index) {
            if ($('#txtNE\\[' + index + '\\]').val() > 100) {
                fnMensaje("warning", "Calificación máxima 100");
                $('#txtNE\\[' + index + '\\]').val("");
                $('#txtNE\\[' + index + '\\]').focus();
                $('#txtNE\\[' + index + '\\]').select();
            }
        }

        function EnviarEvaluacionExterno(index, cod) {
            var sw = 0;
            var mensaje = "";
            var fic = $('#filePE\\[' + index + '\\]').val().split('\\');
            //alert(fic[fic.length - 1]);
            var archivo_extension = fic[fic.length - 1];
            if ($('#filePE\\[' + index + '\\]').val() == "") {
                sw = 1;
                mensaje = "Ingrese archivo de Rubrica";
                $('#divFile\\[' + index + '\\]').hide();
            }
            if ($('#txtNE\\[' + index + '\\]').val() == "") {
                sw = 1;
                mensaje = "Ingrese calificación en Postulación";
                $('#txtNE\\[' + index + '\\]').focus();
                $('#txtNE\\[' + index + '\\]').select();
            }
            if (sw == 1) {
                fnMensaje("warning", mensaje);
                return false;
            } else {

                $("#action").val("aEvaluarPostulacionExterno");
                var form = $('#frmRegistroInvestigadores').serialize();
                //alert($("#action").val());
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "DataJson/operaciones.aspx",
                    data: { "action": "aEvaluarPostulacionExterno", "param1": $('#txtNE\\[' + index + '\\]').val(), "param2": cod, "param3": $("#hdEVE").val(), "param4": archivo_extension },
                    //data: form,
                    dataType: "json",
                    success: function(data) {
                        if (data[0].Status == "success") {
                            fnCargarRubrica(data[0].Code, index, "RB");


                            fnDestroyDataTableDetalle('tPostulacionesEvaluar');
                            $('#tbPostulacionesEvaluar').html('');

                            fnMensaje(data[0].Status, data[0].Message);
                            document.execCommand('ClearAuthenticationCache');

                            listarPostulacionesEvaluar();
                        }


                    },
                    error: function(result) {
                        alert("b");
                        console.log(result);
                    }
                });

            }

        }

        function fnCargarRubrica(cod, index, tipo) {
            var flag = false;
            try {

                var data = new FormData();
                data.append("action", "SubirRubricaNew");
                data.append("codigo", cod);
                data.append("tipo", tipo);

                if (tipo == "RB") {
                    //var files = $("#file_cv").get(0).files;
                    var files = $('#filePE\\[' + index + '\\]').get(0).files;
                    if (files.length > 0) {
                        data.append("ArchivoASubir", files[0]);
                    }
                }
                //console.log(data);
                if (files.length > 0) {
                    $.ajax({
                        type: "POST",
                        url: "DataJson/operaciones.aspx",
                        data: data,
                        dataType: "json",
                        cache: false,
                        contentType: false,
                        processData: false,
                        async: false,
                        success: function(data) {
                            flag = true;
                        },
                        error: function(result) {
                            flag = false;
                        }
                    });
                }

                return flag;

            }
            catch (err) {
                return false;
            }
        }

        function InformacionPostulacion(cod) {
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "DataJson/operaciones.aspx",
                data: { "action": "lPostulacionesXCodigo", "param1": cod },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log("---------");
                    //console.log(data);
                    if (data.length > 0) {
                        $("#txtresumen").val(data[0].resumen);
                        $("#txtpalabras").val(data[0].palabras);
                        $("#txtjustificacion").val(data[0].justificacion);
                        if (data[0].propuesta != "") {
                            //$("#divPro").html("<a href='" + data[0].propuesta + "' target='_blank' style='font-weight:bold'>Descargar Propuesta</a>")
                            //$("#divPro").html("<a onclick='fnDownload(\'' + data[0].propuesta + '\')" target='_blank' style='font-weight:bold'>Descargar Propuesta</a>")
                            $("#divPro").html('<a onclick="fnDownload(\'' + data[0].propuesta + '\')"  style="font-weight:bold">Descargar Propuesta</a>')
                        } else {
                            $("#divPro").html("");
                        }
                        /*
                        if (data[0].presupuesto != "") {
                            //$("#divPre").html("<a href='" + data[0].presupuesto + "' target='_blank' style='font-weight:bold'>Descargar Presupuesto</a>")
                            $("#divPre").html("<a onclick='fnDownload(" + data[0].presupuesto + ")' target='_blank' style='font-weight:bold'>Descargar Presupuesto</a>")
                        } else {
                            $("#divPre").html("");
                        }
                        if (data[0].cronograma != "") {
                            //$("#divCrono").html("<a href='" + data[0].cronograma + "' target='_blank' style='font-weight:bold'>Descargar Cronograma</a>")
                            $("#divCrono").html("<a onclick='fnDownload(" + data[0].cronograma + ")' target='_blank' style='font-weight:bold'>Descargar Cronograma</a>")
                        } else {
                            $("#divCrono").html("");
                        }
                        if (data[0].resultado != "") {
                            //$("#divResult").html("<a href='" + data[0].resultado + "' target='_blank' style='font-weight:bold'>Descargar Resultados Esperados</a>")
                            $("#divResult").html("<a onclick='fnDownload(" + data[0].resultado + ")' target='_blank' style='font-weight:bold'>Descargar Resultados Esperados</a>")
                        } else {
                            $("#divResult").html("");
                        }*/
                        fnListarObjetivos(cod);
                    }

                },
                error: function(result) {
                    //fnLoading(false)
                    alert("ccc");
                    console.log(result)
                }
            });

            $("#mdRegistro").modal("show");
            return false;
        }



        function fnListarObjetivos(cod) {
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "DataJson/operaciones.aspx",
                data: { "action": "lObjetivosPos", "param1": cod },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log("----------");
                    //console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    objetivos = [];
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            if (data[i].tipo == "GENERAL") {
                                tb += '<tr style="font-weight:bold;color:green;">';
                            } else {
                                tb += '<tr>';
                            }
                            tb += '<td>' + (i + 1) + '</td>';
                            tb += '<td>' + data[i].des + '</td>';
                            tb += '<td>' + data[i].tipo + '</td>';
                            tb += '</tr>';
                        }
                    }
                    //console.log(objetivos);
                    fnDestroyDataTableDetalle('tObjetivos');
                    $('#tbObjetivos').html(tb);
                    fnCreateDataTableBasic('tObjetivos', 0, 'desc', 0);

                },
                error: function(result) {
                    alert("ddd");
                    fnMensaje("warning", result)
                }
            });
        }

        function fnCancelar() {
            $("#mdRegistro").modal("hide");
        }

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
        function maxLengthCheck(object) {
            if (object.value.length > object.maxLength)
                object.value = object.value.slice(0, object.maxLength);
            //fnMensaje("warning", "Calificación menor que 100");
        }
        
    </script>

</head>
<body>
    <form id="frmRegistroInvestigadores" name="frmRegistroInvestigadores">
    <input type="hidden" id="action" name="action" value="" />
    <input type="hidden" id="hdEVE" name="hdEVE" value="" runat="server" />
    <input type="hidden" id="hdCON" name="hdCON" value="" runat="server" />
    <!--
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>-->
    <div class="wrapper">
        <div class="content">
            <!--<div class="overlay">
            </div>-->
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">GESTIÓN
                                                DE INVESTIGACIÓN </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="panel-piluku">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Evaluación de Postulaciones
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="form-group">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Evaluador Externo</label>
                                                <div class="col-md-8">
                                                    <input name="txtEvaluadorExterno" type="text" id="txtEvaluadorExterno" value="" class="form-control"
                                                        readonly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Concurso</label>
                                                <div class="col-md-8">
                                                    <input name="txtConcurso" type="text" id="txtConcurso" value="" class="form-control"
                                                        readonly="true" runat="server" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-md-2 control-label ">
                                                    Rúbrica</label>
                                                <div class="col-md-8">
                                                    <div id="txtRubrica" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="panel-piluku">
                                                    <div class="panel-heading">
                                                        <h3 class="panel-title">
                                                            Listado de Postulaciones
                                                        </h3>
                                                    </div>
                                                    <div class="panel-body">
                                                        <div class="table-responsive">
                                                            <div id="tBonos_wrapper" class="dataTables_wrapper" role="grid">
                                                                <table id="tPostulacionesEvaluar" name="tPostulacionesEvaluar" class="display dataTable"
                                                                    width="100%">
                                                                    <thead>
                                                                        <tr role="row">
                                                                            <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                                N°
                                                                            </td>
                                                                            <td width="35%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Postulación: activate to sort column ascending">
                                                                                Postulación
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 169px; text-align: center;" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Fin Evaluación: activate to sort column ascending">
                                                                                Fin Evaluación
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Información: activate to sort column ascending">
                                                                                Información
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Nota: activate to sort column ascending">
                                                                                Nota
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Rubrica: activate to sort column ascending">
                                                                                Rúbrica
                                                                            </td>
                                                                            <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                                Enviar
                                                                            </td>
                                                                        </tr>
                                                                    </thead>
                                                                    <tfoot>
                                                                        <tr>
                                                                            <th colspan="7" rowspan="1">
                                                                            </th>
                                                                        </tr>
                                                                    </tfoot>
                                                                    <tbody id="tbPostulacionesEvaluar" runat="server">
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
            style="z-index: 2;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" id="modalReg">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel3">
                            Información de Postulación</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <div id="divPro">
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div id="divPre">
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div id="divCrono">
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div id="divResult">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Resumen:</label>
                                <div class="col-sm-8">
                                    <textarea id="txtresumen" name="txtresumen" class="form-control" cols="100%" rows="3"
                                        maxlength="8000"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                </label>
                                <div class="col-md-1">
                                    <button type="button" id="btnObjetivos" name="btnObjetivos" class="btn btn-green">
                                        Objetivos &nbsp;&nbsp;<i class="ion-ios-compose"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Palabras Clave:</label>
                                <div class="col-sm-8">
                                    <textarea id="txtpalabras" name="txtpalabras" class="form-control" cols="100%" rows="2"
                                        maxlength="1000"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Justificación:</label>
                                <div class="col-sm-8">
                                    <textarea id="txtjustificacion" name="txtjustificacion" class="form-control" cols="100%"
                                        rows="3" maxlength="8000"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" id="btnCancelarReg" class="btn btn-danger" onclick="fnCancelar();">
                                Cancelar</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="modal fade" id="mdObjetivos" role="dialog" aria-labelledby="myModalLabel"
            style="z-index: 5;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" id="Div5">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" id="H2">
                            Objetivos de Proyecto</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                        <div id="Div6" class="dataTables_wrapper" role="grid">
                                            <table id="tObjetivos" name="tObjetivos" class="display dataTable" width="100%">
                                                <thead>
                                                    <tr role="row">
                                                        <td width="70%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                            rowspan="1" colspan="1" aria-label="Nro: activate to sort column ascending">
                                                            Nro
                                                        </td>
                                                        <td width="70%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                            rowspan="1" colspan="1" aria-label="Objetivo: activate to sort column ascending">
                                                            Objetivo
                                                        </td>
                                                        <td width="5%" style="font-weight: bold; text-align: center" class="sorting" tabindex="0"
                                                            rowspan="1" colspan="1" aria-label="Tipo: activate to sort column ascending">
                                                            Tipo
                                                        </td>
                                                    </tr>
                                                </thead>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="3" rowspan="1">
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                                <tbody id="tbObjetivos">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <button type="button" class="btn btn-danger" id="Button7" data-dismiss="modal">
                                Volver</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
