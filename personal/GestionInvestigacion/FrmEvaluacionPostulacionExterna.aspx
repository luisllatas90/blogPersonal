<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEvaluacionPostulacionExterna.aspx.vb"
    Inherits="GestionInvestigacion_FrmEvaluacionPostulacionExterna" %>

<!DOCTYPE html>
<html>
<head>
    <title>Evaluación de Postulación Externa</title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <!-- Cargamos css -->
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- Cargamos JS -->

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

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

    <script src="js/_General.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            var dt = fnCreateDataTableBasic('tConcursos', 0, 'asc', 100);
            var dt2 = fnCreateDataTableBasic('tPostulacion', 0, 'asc', 100);
            var dt2 = fnCreateDataTableBasic('tGrupo', 0, 'asc', 100);
            ope = fnOperacion(1);
            rpta = fnvalidaSession();
            if (rpta == true) {
                fnListarConcurso();
            } else {
                window.location.href = rpta
            }
            $("#btnRegresar").click(function() {
                $("#VerConcurso").attr("style", "display:none");
                $("#ListaConcursos").attr("style", "display:block");
                $("#PanelBusqueda").attr("style", "display:block");
            })

            $("#btnCerrar").click(function() {
                $("#mdRegistro").modal("hide");
            })

        });


        function fnLimpiarPostulacion() {
            equipo = [];
            fnDestroyDataTableDetalle('tGrupo');
            $('#tbGrupo').html('');
            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
            $("#tab2").trigger("click");
            $("#cboGrupo").removeAttr('disabled');
        }


        function fnListarConcurso() {
            if ($("#cboEstado").val() !== "") {
                rpta = fnvalidaSession()
                if (rpta == true) {
                    fnLoading(true)
                    $("form#frmbuscar input[id=action]").remove();
                    $("form#frmbuscar input[id=ctf]").remove();
                    $("form#frmbuscar input[id=ambito]").remove();
                    $("form#frmbuscar input[id=dir]").remove();
                    $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
                    $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
                    $('form#frmbuscar').append('<input type="hidden" id="ambito" name="ambito" value="1" />');
                    $('form#frmbuscar').append('<input type="hidden" id="dir" name="dir" value="2" />');
                    var form = $("#frmbuscar").serializeArray();
                    $("form#frmbuscar input[id=action]").remove();
                    $("form#frmbuscar input[id=ctf]").remove();
                    $("form#frmbuscar input[id=ambito]").remove();
                    $("form#frmbuscar input[id=dir]").remove();
                    
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/GestionInvestigacion/Concurso.aspx",
                        data: form,
                        dataType: "json",
                        cache: false,
                        //async: false,
                        success: function(data) {
                            var tb = '';
                            var i = 0;
                            var filas = data.length;
                            var clase_icono = "ion-edit";
                            if (filas > 0) {
                                for (i = 0; i < filas; i++) {
                                    tb += '<tr>';
                                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                                    tb += '<td>' + data[i].titulo + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].fecini + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].fecfin + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].tipo + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].nro_pos + '</td>';
                                    tb += '<td style="text-align:center">';
                                    tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnVer(\'' + data[i].cod + '\')" title="Ver" ><i class="ion-eye"></i></button>';
                                    tb += '</td>';
                                    tb += '</tr>';
                                }
                            }
                            fnDestroyDataTableDetalle('tConcursos');
                            $('#tbConcursos').html(tb);
                            fnResetDataTableBasic('tConcursos', 0, 'asc', 100);
                            fnLoading(false);
                        },
                        error: function(result) {
                            fnMensaje("warning", result)
                            fnLoading(false);
                        }
                    });


                } else {
                    window.location.href = rpta
                }
            }
        }


        function fnVer(cod) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                $("#hdcod").val(cod)
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                $('#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
                $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
                var form = $("#frmbuscar").serializeArray();
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/Concurso.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    //            async: false,
                    success: function(data) {
                        //console.log(data);
                        $("#hdcodCon").val(data[0].cod)
                        $("#txttitulo").val(data[0].titulo);
                        $('#txtdescripcion').val(data[0].des);
                        $("#cboAmbito").val(data[0].ambito);
                        $("#txtfecini").val(data[0].fecini);
                        $("#txtfecfin").val(data[0].fecfin);
                        $("#txtfecfineva").val(data[0].fecfineva);
                        $("#txtfecres").val(data[0].fecres);
                        $("#cbotipo").val(data[0].tipo);
                        if (data[0].rutabases != "") {
                            //$("#file_Bases").html("<a href='" + data[0].rutabases + "' target='_blank' style='font-weight:bold'>Descargar Bases</a>")
                            //                    $("#file_Bases").html("<a href='" + data[0].rutabases + "' target='_blank' style='font-weight:bold'>Descargar Bases</a>")
                            $("#file_Bases").html('<a onclick="fnDownload(\'' + data[0].rutabases + '\')" style="font-weight:bold">Descargar Bases</a>')
                        } else {
                            $("#file_Bases").html("");
                        }
                        $("#mensaje").html("");


                        /*if (data[0].cerrado == '1') {
                        $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) el Plazo para Postular al Concurso Culminó</label>')
                        $("#btnPostular").attr("style", "display:none");
                        } else {
                        if (data[0].iniciado == '0') {
                        $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) Ud. Podra postular a partir de la Fecha de Inicio del Concurso : ' + data[0].fecini + '</label>')

                            } else {
                        $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) Abstenerse de Postular, si no cuenta con los Requerimientos mínimos establecidos en las Bases del Concurso</label>')
                        $("#btnPostular").attr("style", "display:inline-block");
                        }

                        }*/
                        $("#VerConcurso").attr("style", "display:block");
                        $("#PanelBusqueda").attr("style", "display:none");
                        $("#ListaConcursos").attr("style", "display:none");
                        fnListarPostulacion(data[0].cod, data[0].cerrado);
                        //                        if (data[0].iniciado == '0') {
                        //                            $("#btnPostular").attr("style", "display:none");
                        //                        }
                        fnLoading(false);
                    },
                    error: function(result) {
                        fnLoading(false)
                        //console.log(result)
                    }
                });
            } else {
                window.location.href = rpta
            }
        }


        function fnListarPostulacion(codigo_con, cerrado) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lpo + '" />');
                $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
                var form = $("#frmbuscar").serializeArray();
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        var tb = '';
                        var i = 0;
                        var filas = data.length;
                        var clase_icono = "ion-edit";
                        if (filas > 0) {
                            for (i = 0; i < filas; i++) {
                                tb += '<tr>';
                                tb += '<td style="text-align:center;width:5%">' + (i + 1) + "" + '</td>';
                                tb += '<td style="width:30%">' + data[i].coord + '</td>';
                                tb += '<td style="width:35%">' + data[i].titulo + '</td>';
                                tb += '<td style="text-align:center;width:10%">' + data[i].fechareg + '</td>';
                                tb += '<td style="text-align:center;width:5%">';
                                if (data[i].tipo == "INDIVIDUAL") {
                                    tb += data[i].tipo;
                                } else {
                                    tb += '<b><a onclick="fnListarEquipo(\'' + data[i].cod + '\')" >' + data[i].tipo + '</a></b>';
                                }

                                tb += '</td>';
                                tb += '<td style="text-align:center;width:15%"><a onclick="fnDownload(\'' + data[i].archivo + '\')" >Propuesta</a></td>';
                                tb += '</tr>';
                            }
                            $("#mensaje").html("");
                            $("#divPostulacion").removeAttr("style")
                            $("#divPostulacion").attr("style", "display:block")
                            //                    $("#btnPostular").attr("style", "display:none");
                        } else {
                            if (cerrado == 1) {
                                if (filas == 0) {
                                    $("#divPostulacion").removeAttr("style")
                                    $("#divPostulacion").attr("style", "display:none")
                                } else {
                                    $("#divPostulacion").removeAttr("style")
                                    $("#divPostulacion").attr("style", "display:block")
                                }
                                //                        $("#btnPostular").attr("style", "display:none");
                            } else {
                                $("#divPostulacion").removeAttr("style")
                                $("#divPostulacion").attr("style", "display:none")
                                //                        $("#btnPostular").attr("style", "display:inline-block");
                            }
                        }
                        fnDestroyDataTableDetalle('tPostulacion');
                        $('#tbPostulacion').html(tb);
                        fnResetDataTableBasic('tPostulacion', 0, 'asc', 100);
                    },
                    error: function(result) {
                        fnMensaje("warning", result)
                    }
                });
                fnLoading(false);

            } else {
                window.location.href = rpta
            }
        }


        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }

        function fnListarEquipo(cod) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                fnDestroyDataTableDetalle('tGrupo');
                $('#tbGrupo').html('');
                fnResetDataTableBasic('tGrupo', 0, 'asc', 10);

                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=hdcodPost]").remove();
                $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lep + '" />');
                $('form#frmbuscar').append('<input type="hidden" id="hdcodPost" name="hdcodPost" value="' + cod + '" />');
                var form = $("#frmbuscar").serializeArray();
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=hdcodPost]").remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //console.log(data);
                        var tb = '';
                        var i = 0;
                        var filas = data.length;

                        if (filas > 0) {
                            for (i = 0; i < filas; i++) {
                                tb += '<tr>';
                                tb += '<td style="text-align:center;width:5%">' + (i + 1) + '</td>';
                                tb += '<td style="width:50%">' + data[i].nombre + '</td>';
                                if (data[i].dina != '-') {
                                    tb += '<td style="width:45%;"><a  href="' + data[i].dina + '" target="_blank">' + data[i].dina.substr(0, 80) + '...</a></td>';
                                } else {
                                    tb += '<td style="width:45%;">' + data[i].dina.substr(0, 50) + '</td>';
                                }

                                tb += '</tr>';
                            }
                        }
                        //console.log(objetivos);
                        fnDestroyDataTableDetalle('tGrupo');
                        $('#tbGrupo').html(tb);
                        fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
                        $("#mdRegistro").modal("show");
                    },
                    error: function(result) {
                        fnMensaje("warning", result)
                    }
                });
                fnLoading(false);
            } else {
                window.location.href = rpta
            }
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
    </style>
</head>
<body>
    <div class="piluku-preloader text-center hidden">
        <!--<div class="progress">
                <div class="indeterminate"></div>
                </div>-->
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="overlay">
            </div>
            <div class="main-content">
                <div class="row">
                    <div class="manage_buttons">
                        <div class="row">
                            <div id="PanelBusqueda">
                                <div class="col-md-12 col-sm-12 col-lg-12">
                                    <div class="page_header">
                                        <div class="pull-left">
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Evaluar
                                                Postulaciones</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar" onclick="fnListarConcurso()">
                                                    Consultar</button>
                                            </div>
                                        </div>
                                        <form class="form form-horizontal" id="frmbuscar" onsubmit="return false;" action="#"
                                        method="post">
                                        <div class="form-group">
                                            <div class="col-md-7">
                                                <label class="col-md-3 control-label ">
                                                    Titulo de Concurso</label>
                                                <div class="col-md-9">
                                                    <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                                    <input type="text" class="form-control" id="txtBusqueda" name="txtBusqueda" />
                                                </div>
                                            </div>
                                            <div class="col-md-5">
                                                <label class="col-md-4 control-label ">
                                                    Estado
                                                </label>
                                                <div class="col-md-8">
                                                    <select name="cboEstado" class="form-control" id="cboEstado">
                                                        <option value="">-- Seleccione -- </option>
                                                        <option value="1" selected="selected">EN PROCESO</option>
                                                        <option value="2">CULMINADO</option>
                                                        <option value="T">TODOS</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="ListaConcursos">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Concursos
                            </h3>
                        </div>
                        <div class="panel-body">
                            <form class="form form-horizontal" id="frmLista" onsubmit="return false;" action="#"
                            method="post">
                            </form>
                            <div class="table-responsive">
                                <div id="tProyectos_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tConcursos" name="tConcursos" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="35%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Titulo
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Fecha Inicio
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Fecha Fin
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Tipo
                                                </td>
                                                <td width="10%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Postulaciones
                                                </td>
                                                <td width="5%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                    Ver
                                                </td>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th colspan="7" rowspan="1">
                                                </th>
                                            </tr>
                                        </tfoot>
                                        <tbody id="tbConcursos">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" id="VerConcurso" style="display: none;">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Detalle de Concurso
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="txttitulo">
                                        Título:</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txttitulo" name="txttitulo" class="form-control" disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Breve Descripción:</label>
                                    <div class="col-md-8">
                                        <textarea class="form-control" cols="50" rows="3" id="txtdescripcion" name="txtdescripcion"
                                            disabled="disabled"></textarea>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Fecha Inicio Postulación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecini" class="form-control" id="txtfecini" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Fecha Fin Evaluación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecfineva" class="form-control" id="txtfecfineva" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Fecha Fin Postulación:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecfin" class="form-control" id="txtfecfin" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <label class="col-sm-3 control-label">
                                        Fecha Publicación de Resultados:</label>
                                    <div class="col-sm-2">
                                        <input name="txtfecres" class="form-control" id="txtfecres" style="text-align: right;"
                                            disabled="disabled" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Tipo:</label>
                                    <div class="col-md-3">
                                        <select id="cbotipo" name="cbotipo" class="form-control" disabled="disabled">
                                            <option value="">--Seleccione--</option>
                                            <option value="0">INDIVIDUAL</option>
                                            <option value="1">UNIDISCIPLINARIO</option>
                                            <option value="2">GRUPAL</option>
                                            <option value="3">INDIVIDUAL/GRUPAL</option>
                                        </select>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Bases(Pdf)</label>
                                    <div class="col-sm-8">
                                        <div id="file_Bases">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <div id="mensaje">
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divPostulacion" style="display: none">
                                <h3>
                                    Listado de Postulación</h3>
                                <div class="table-responsive">
                                    <div id="Div2" class="dataTables_wrapper" role="grid">
                                        <table id="tPostulacion" name="tPostulacion" class="display dataTable" width="100%">
                                            <thead>
                                                <tr role="row">
                                                    <td style="font-weight: bold; width: 5%; text-align: center" class="sorting_asc"
                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                        N°
                                                    </td>
                                                    <td style="font-weight: bold; width: 30%; text-align: center" class="sorting" tabindex="0"
                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Coordinador
                                                    </td>
                                                    <td style="font-weight: bold; width: 35%; text-align: center" class="sorting" tabindex="0"
                                                        rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                        Titulo
                                                    </td>
                                                    <td style="font-weight: bold; width: 10%; text-align: center" class="sorting" tabindex="0"
                                                        rowspan="1" colspan="1" aria-label="Etapa: activate to sort column ascending">
                                                        Fecha Postulación
                                                    </td>
                                                    <td style="font-weight: bold; width: 10%; text-align: center" class="sorting" tabindex="0"
                                                        rowspan="1" colspan="1" aria-label="Etapa: activate to sort column ascending">
                                                        Tipo
                                                    </td>
                                                    <td style="font-weight: bold; width: 10%; text-align: center" class="sorting" tabindex="0"
                                                        rowspan="1" colspan="1" aria-label="Etapa: activate to sort column ascending">
                                                        Propuesta
                                                    </td>
                                                    <%--   <td style="font-weight: bold; width: 5%; text-align: center" class="sorting" tabindex="0"
                                                        rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                        Opciones
                                                    </td>--%>
                                                </tr>
                                            </thead>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="6" rowspan="1">
                                                    </th>
                                                </tr>
                                            </tfoot>
                                            <tbody id="tbPostulacion">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <center>
                                        <%--<button id="btnPostular" name="btnPostular" class="btn btn-success">
                                            Postular</button>--%>
                                        <button id="btnRegresar" name="btnRegresar" class="btn btn-danger">
                                            Regresar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="modalReg">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Equipo</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="table-responsive" style="font-size: 12px; font-weight: 300; line-height: 18px;">
                                            <div id="Div1" class="dataTables_wrapper" role="grid">
                                                <table id="tGrupo" name="tGrupo" class="display dataTable" width="100%">
                                                    <thead>
                                                        <tr role="row">
                                                            <th style="font-weight: bold; text-align: center;" class="sorting_asc" tabindex="0"
                                                                rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                N°
                                                            </th>
                                                            <th style="font-weight: bold; text-align: center;" class="sorting" tabindex="0" rowspan="1"
                                                                colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Nombre
                                                            </th>
                                                            <th style="font-weight: bold; text-align: center;" class="sorting" tabindex="0" rowspan="1"
                                                                colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                Url DINA
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                        <tr>
                                                            <th colspan="5" rowspan="1">
                                                            </th>
                                                        </tr>
                                                    </tfoot>
                                                    <tbody id="tbGrupo">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <center>
                                                <%--<button id="btnPostular" name="btnPostular" class="btn btn-success">
                                            Postular</button>--%>
                                                <button id="btnCerrar" name="btnCerrar" class="btn btn-danger">
                                                    Regresar</button>
                                            </center>
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
    <%--    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>--%>
</body>
</html>
