<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPostularBono.aspx.vb"
    Inherits="GestionInvestigacion_frmPostularBono" %>

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

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src='../assets/js/noty/jquery.noty.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src='../assets/js/noty/notifications-custom.js'></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <!-- Manejo de tablas -->

    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>

    <link href="../assets/css/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />

    <script src="../assets/js/funcionesDataTable.js?y=1" type="text/javascript"></script>

    <!-- Piluku -->
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/material.css?x=1" />
    <link rel="stylesheet" type="text/css" href="../assets/css/style.css?y=4" />

    <script src='../assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>
    <!-- <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>-->
    <%-- ======================= Fin Notificaciones =============================================--%>
    <!--<script src="js/_General.js?x=1" type="text/javascript"></script>-->
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
            padding-top: 3px;
        }
    </style>

    <script type="text/javascript">
        var aDataBP = [];
        $(document).ready(function() {
            //document.execCommand('ClearAuthenticationCache');
            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }

            limpiar();
            $('#divLimpiar').hide();
            $('#hdAccion').val("R");
            //fnResetDataTableTramite('tbBonoPublicacion', 0, 'desc');
            var dt = fnCreateDataTableBasic('tbBonoPublicacion', 1, 'asc');
            //listaGrupoCoordinador($("#hdUser").val(), 'CO'); -- NO VA
            listaBonos();
            $("#btnPostularBono").click(fnPostularBono);
            $("#btnLimpiar").click(fnLimpiaDatos);
            $("#btnGuardar").click(fnGuardarBono);
            $("#btnAceptar").click(fnAceptar);

        });

        function fnvalidaSession() {
            var rpta = false
            $('body').append('<form id="frm"><input type="hidden" id="action" name="action" value="ValidaSession" /></form>');
            var form = $("#frm").serializeArray();
            $("#frm").remove();
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    if (data[0].msje == true) {
                        rpta = data[0].msje
                    } else {
                        rpta = data[0].link
                    }
                },
                error: function(result) {
                    console.log(result)
                }
            });
            return rpta;
        }

        function limpiar() {
            $("#txtTitulo").val("");
            $("#txtFecha").val("");
            $("#cboBDRevista").val("0");
            $("#txtRevista").val("");
            $("#txtURLPubli").val("");
            $("#cboParticipacion").val("0");
        }


        function listaBonos() {
            fnValidaInvestigador();
            if ($("#hdValida").val() == "1") {
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "lBonosPublicacion", "param1": $("#hdUser").val(), "param2": "PE" },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        var tb = '';
                        var i = 0;
                        var mostrar = '';
                        aDataBP = data;
                        var contador = 0;
                        var filas = aDataBP.length;
                        if (filas > 0) {
                            for (i = 0; i < aDataBP.length; i++) {
                                tb += '<tr>';
                                tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                                tb += '<td style="text-align:center">' + aDataBP[i].d_tit + '</td>';
                                tb += '<td style="text-align:center">' + aDataBP[i].f_tit + '</td>';
                                tb += '<td style="text-align:center">' + aDataBP[i].d_iea + '</td>';
                                if (aDataBP[i].d_iea == "EN EVALUACIÓN" || aDataBP[i].d_iea == "APROBADO" || aDataBP[i].d_iea == "RECHAZADO") {
                                    mostrar = 'disabled'
                                } else {
                                    mostrar = '';
                                }
                                tb += '<td style="text-align:center">';
                                tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(' + aDataBP[i].c_bon + ')" title="Editar" ><i class="ion-eye"></i></button>';
                                tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-orange" onclick="CambiarBonoPublicacion(' + aDataBP[i].c_bon + ',\'' + 'E' + '\')" title="Aprobar Revisión" ' + mostrar + ' ><i class="ion-arrow-right-a"></i></button>';
                                tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="CambiarBonoPublicacion(' + aDataBP[i].c_bon + ',\'' + 'X' + '\')" title="Eliminar Bono" ' + mostrar + '><i class="ion-close"></i></button>';
                                tb += '</td>';
                                tb += '</tr>';
                            }
                        }
                        fnDestroyDataTableDetalle('tbBonoPublicacion');
                        $('#pBonoPublicacion').append(tb);
                        //fnResetDataTableTramite('tbBonoPublicacion', 0, 'asc');
                        fnResetDataTableBasic('tbBonoPublicacion', 0, 'asc');
                    },
                    error: function(result) {
                        //alert(result);
                        fnMensaje("warning", result)
                    }
                });
            }
            else {
                var contenido = '';
                contenido = contenido + "<center>Debe registrarse como colaborador(a) con actividad de investigación</center><br>";
                contenido = contenido + "<center><a href='../GestionInvestigacion/frmRegistroInvestigadores.aspx?id=" + $("#hdId").val() + "&ctf=" + $("#hdCtf").val() + "'>Registro de colaborador(a) con actividad de investigación</a></center>"
                $('#ContenidoMensajeValidaInv').html(contenido);
                return false;
            }

        }

        function fnValidaInvestigador() {
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "validaInvestigador", "param1": $("#hdUser").val(), "param2": "VA" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var filas = data.length;
                    if (filas > 0) {
                        $("#hdValida").val("1");
                    } else {
                        $("#hdValida").val("0");
                    }
                },
                error: function(result) {
                    $("#hdValida").val("0");
                }
            });
        }

        function fnAceptar() {
            ajaxCambiarBonoPublicacion($("#hdBono").val(), $("#hdTipo").val());
        }

        function CambiarBonoPublicacion(reg, tip) {
            rpta = fnvalidaSession();
            if (rpta == true) {
                var x = fnBuscar(reg);

                $("#hdBono").val(reg);
                $("#hdTipo").val(tip);

                if (tip == "X") {
                    document.getElementById("divTitle").innerHTML = "Eliminar Bono";
                    document.getElementById("divMensaje").innerHTML = "¿Desea Eliminar el bono: " + aDataBP[x].d_tit + "?";

                    fnMensajeConfirmarEliminar('top', "¿Desea eliminar el bono: " + aDataBP[x].d_tit + "?", 'ajaxCambiarBonoPublicacion', reg, tip);
                    //ajaxCambiarBonoPublicacion($("#hdBono").val(), $("#hdTipo").val());
                    //$('div#mdMensaje').modal('show'); // MODAL NO
                } else {
                    document.getElementById("divTitle").innerHTML = "Enviar Bono";
                    document.getElementById("divMensaje").innerHTML = "¿Desea Aprobar al Investigador: " + aDataBP[x].d_tit + "?";

                    fnMensajeConfirmarEliminar('top', "¿Desea enviar a revisión: " + aDataBP[x].d_tit + "?", 'ajaxCambiarBonoPublicacion', reg, tip);

                }
            } else {
                window.location.href = rpta;
            }

        }

        function ajaxCambiarBonoPublicacion(reg, tip) {
            rpta = fnvalidaSession();
            if (rpta == true) {
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "actualizarBonoPublicacion", "param1": reg, "param2": tip, "param3": $("#hdUser").val(), "param4": "","param5":"0" },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //document.execCommand('ClearAuthenticationCache');
                        fnMensaje(data[0].Status, data[0].Message);

                        fnDestroyDataTableDetalle('tbBonoPublicacion');
                        $('#pBonoPublicacion').html('');

                        listaBonos();

                        if (tip == "X") {
                            $('div#mdMensaje').modal('hide');
                        }
                    },
                    error: function(result) {
                        console.log(result);
                    }
                });
            } else {
                window.location.href = rpta;
            }
        }

        function fnLimpiaDatos() {
            limpiar();
            var boton = document.getElementById("btnPostularBono");
            boton.innerHTML = "Postular";
            $('#divLimpiar').hide();
            return false;
        }

        function fnEditar(bon) {
            rpta = fnvalidaSession();
            if (rpta == true) {
                limpiar();
                $('#hdAccion').val("A");
                var y = fnBuscar(bon);
                $('#hdBono').val(aDataBP[y].c_bon);
                $('#txtTituloE').val(aDataBP[y].d_tit);
                $('#txtFechaE').val(aDataBP[y].f_tit);
                $('#cboParticipacionE').val(aDataBP[y].c_tipopart);
                $('#cboBDRevistaE').val(aDataBP[y].c_bdrev);
                $('#txtRevistaE').val(aDataBP[y].r_bon);
                $('#txtURLPubliE').val(aDataBP[y].u_bon);

                var boton = document.getElementById("btnGuardar");
                if (aDataBP[y].d_iea == "EN EVALUACIÓN" || aDataBP[y].d_iea == "APROBADO" || aDataBP[y].d_iea == "RECHAZADO") {
                    boton.disabled = true;
                } else {
                    boton.disabled = false;
                }
                fnListarObservaciones($("#hdBono").val(), 'BON');

                $("#mdEdicion").modal("show");
            } else {
                window.location.href = rpta;
            }
        }

        function listaGrupoCoordinador(param1, param2) {
            $("#action").val("lGrupoComoCoordinador");
            var form = $('#frmRegistroBonoPublicacion').serialize();
            //alert($("#action").val());
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": $("#action").val(), "param1": param1, "param2": param2 },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    var i = 0;
                    var t = '';
                    if (data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            t += '<option value="' + data[i].c_gru + '" ' + data[i].selected + '>' + data[i].n_gru + '</option>';
                        }
                    }
                    $('#cboGrupo').html(t);
                },
                error: function(result) {
                    sOut = '';
                    //alert(result);
                }
            });
        }

        function fnGuardarBono() {
            var sw = 0;
            var arrayvalida = new Array();
            var mensaje = "";
            if ($("#txtFechaE").val() == "") {
                sw = 1;
                mensaje = "Seleccione Fecha";
            }
            if ($("#txtRevistaE").val() == "") {
                sw = 1;
                mensaje = "Ingrese Revista";
            }
            if ($("#cboParticipacionE").val() == 0) {
                sw = 1;
                mensaje = "seleccione el tipo de participación";
            }
            if ($("#txtURLPubliE").val() == "") {
                sw = 1;
                mensaje = "Ingrese URL de Publicación";
            }
            if ($("#txtTituloE").val() == "") {
                sw = 1;
                mensaje = "Ingrese Titulo de Publicación";
            }
            if (sw == 1) {
                fnMensaje("warning", mensaje);
                return false;
            } else {
                $("#action").val("gBonoPublicacion");
                var form = $('#frmRegistroBonoPublicacion').serialize();
                //alert(form);
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //console.log("--------------");
                        //console.log(data);
                        //document.execCommand('ClearAuthenticationCache');
                        fnMensaje(data[0].Status, data[0].Message);

                        //fnDestroyDataTableDetalle('tbBonoPublicacion');
                        //$('#pBonoPublicacion').html('');

                        //listaBonos();

                        //$("#mdEdicion").modal("hide");
                        window.location.reload();
                    },
                    error: function(result) {
                        console.log(result); //--para errores
                    }

                });

                return false; //--para errores   
            }
        }

        function fnPostularBono() {
            rpta = fnvalidaSession();
            if (rpta == true) {

                var sw = 0;
                var arrayvalida = new Array();
                var mensaje = "";
                if ($("#txtURLPubli").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese URL de Publicación";
                }
                if ($("#txtRevista").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese Revista";
                }
                if ($("#cboBDRevista").val() == 0) {
                    sw = 1;
                    mensaje = "Seleccione Base Datos de Revista";
                }
                if ($("#cboParticipacion").val() == 0) {
                    sw = 1;
                    mensaje = "seleccione el tipo de participación";
                }
                if ($("#txtFecha").val() == "") {
                    sw = 1;
                    mensaje = "Seleccione Fecha de Publicación";
                }
                if ($("#txtTitulo").val() == "") {
                    sw = 1;
                    mensaje = "Ingrese Titulo de Publicación";
                }
                if (sw == 1) {
                    fnMensaje("warning", mensaje);
                    return false;
                } else {
                    $("#action").val("gBonoPublicacion");
                    var form = $('#frmRegistroBonoPublicacion').serialize();
                    //alert(form);
                    $("#btnPostularBono").attr('disabled', 'disabled');
                    $.ajax({
                        type: "GET",
                        //contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: form,
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            $("#btnPostularBono").removeAttr('disabled');
                            //document.execCommand('ClearAuthenticationCache');

                            fnMensaje(data[0].Status, data[0].Message);

                            fnDestroyDataTableDetalle('tbBonoPublicacion');
                            $('#pBonoPublicacion').html('');

                            limpiar();
                            listaBonos();

                        },
                        error: function(result) {
                            console.log(result); //--para errores
                        }

                    });

                    return false; //--para errores     

                }
            } else {
                window.location.href = rpta;
            }

        }

        function fnBuscar(c) {
            var i;
            var j = -1;
            var l;
            l = aDataBP.length;
            for (i = 0; i < l; i++) {
                if (aDataBP[i].c_bon == c) {
                    j = i;
                    return j;
                }
            }
        }

        function fnListarObservaciones(cod, gru) {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "gVisualizarHistorialGI", "param1": cod, "param2": gru },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log("*---------");
                    //console.log(data);
                    var filas = data.length;
                    var div = "";
                    var obs = 0;
                    var res = 0;
                    if (filas > 0) {
                        div += "<div class='alert alert-danger'>";
                        div += "<label style='font-size:12px;text-align:center;margin-botton:2px;color:black'>OBSERVACIONES</label></br>";
                        for (i = 0; i < filas; i++) {
                            if (data[i].c_obs == 0) {
                                obs = obs + 1;
                                div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:red'>" + obs + ".- " + data[i].d_fech + " - " + data[i].d_eia + " - " + data[i].d_obs + "</label></br>";
                            } else {
                                if (data[i].c_obs == 2) {
                                    div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:blue'> .: " + data[i].d_fech + " - " + data[i].d_obs + "</label></br>";
                                } else {
                                    res = res + 1;
                                    div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:green'>" + res + ".- " + data[i].d_fech + " - " + data[i].d_eia + " - " + data[i].d_obs + "</label></br>";
                                }
                            }
                        }
                        div += "</div>";
                    } else {
                        div = ""
                    }
                    $("#DivObservaciones").html(div);

                },
                error: function(result) {
                    console.log(result); //--para errores                      
                }

            });
            return false;
        }

        function validateMail(idMail) {
            object = document.getElementById(idMail);
            valueForm = object.value;

            var patron = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,4})+$/;
            if (valueForm.search(patron) == 0) {
                object.style.color = "#a1a5ac";
                document.getElementById("hdValidaUrl").value = "1";
                return;
            }
            object.style.color = "#f00";
            document.getElementById("hdValidaUrl").value = "0";
        }
        
    </script>

</head>
<body>
    <form id="frmRegistroBonoPublicacion" name="frmRegistroBonoPublicacion">
    <input type="hidden" id="action" name="action" value="" />
    <input type="hidden" id="hdUser" name="hdUser" value="" runat="server" />
    <input type="hidden" id="hdAccion" name="hdAccion" value="" runat="server" />
    <input type="hidden" id="hdBono" name="hdBono" value="" runat="server" />
    <input type="hidden" id="hdTipo" name="hdTipo" value="" runat="server" />
    <input type="hidden" id="hdValidaUrl" name="hdValidaUrl" value="" />
    <input type="hidden" id="hdCtf" name="hdCtf" value="" runat="server" />
    <input type="hidden" id="hdId" name="hdId" value="" runat="server" />
    <input type="hidden" id="hdValida" name="hdValida" value="" runat="server" />
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">POSTULACIÓN
                                                A BONO </span>
                                        </div>
                                        <%--                                        <div class="buttons-list" id="limpiarTextos">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-green" id="btnLimpiar" value="Limpiar">
                                                    Limpiar</button>
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Registro Publicaci&oacute;n para Bono
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-md-12" id="ContenidoMensajeValidaInv" runat="server">
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Titulo Publicaci&oacute;n</label>
                                        <div class="col-md-7">
                                            <input name="txtTitulo" type="text" id="txtTitulo" value="" class="form-control" />
                                        </div>
                                        <div class="col-md-1">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnPostularBono" value="Postular">
                                                    Postular</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Fecha de Publicaci&oacute;n de artículo</label></label>
                                        <div class="col-md-3">
                                            <div class="input-group date" id="FechaInicio">
                                                <input name="txtFecha" class="form-control" id="txtFecha" style="text-align: right;"
                                                    type="text" placeholder="__/__/____" data-provide="datepicker" />
                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="txtFecha">
                                                </i></span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Participación</label>
                                        <div class="col-md-3">
                                            <select name="cboParticipacion" class="form-control" id="cboParticipacion" runat="server">
                                                <option value="0" selected>-- Seleccione --</option>
                                                <option value="1">Primer autor</option>
                                                <option value="2">Segundo autor</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Base Datos Revista</label>
                                        <div class="col-md-3">
                                            <select name="cboBDRevista" class="form-control" id="cboBDRevista" runat="server">
                                                <option value="0" selected>-- Seleccione --</option>
                                                <%--                                                <option value="1" >Web Of Science </option>
                                                <option value="2" >Scopus</option>
                                                <option value="3" >Scielo</option>
                                                <option value="4" >ACC CIETNA</option>
                                                <option value="5" >IUS</option>
                                                <option value="6" >EDUCARE ET COMUNICARE</option>
                                                <option value="7" >Apuntes de Bioética</option>--%>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Revista</label>
                                        <div class="col-md-7">
                                            <input name="txtRevista" type="text" id="txtRevista" value="" class="form-control" />
                                        </div>
                                        <div class="col-md-1" id="divLimpiar">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-green" id="btnLimpiar" value="Limpiar">
                                                    Limpiar</button>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            URL Publicaci&oacute;n
                                        </label>
                                        <div class="col-md-7">
                                            <input name="txtURLPubli" type="text" id="txtURLPubli" value="" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="panel-piluku">
                                            <div class="panel-heading">
                                                <h3 class="panel-title">
                                                    Listado Postulaciones para Bono
                                                </h3>
                                            </div>
                                            <div class="panel-body">
                                                <div class="table-responsive">
                                                    <div id="tProyectos_wrapper" class="dataTables_wrapper" role="grid">
                                                        <table id="tbBonoPublicacion" name="tbBonoPublicacion" class="display dataTable"
                                                            width="100%">
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
                                                                        Fecha
                                                                    </td>
                                                                    <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                        Estado
                                                                    </td>
                                                                    <td width="15%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                        Opciones
                                                                    </td>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th colspan="5" rowspan="1">
                                                                    </th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody id="pBonoPublicacion" runat="server">
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
        <div class="row">
            <div class="modal fade" id="mdEdicion" role="dialog" aria-labelledby="myModalLabel"
                style="z-index: 5;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-lg" id="modalRegI">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                            </button>
                            <h4 class="modal-title" id="myModalLabel3">
                                Actualización de Bono</h4>
                        </div>
                        <div class="modal-body">
                            <div id="DivObservaciones">
                            </div>
                            <div class="row">
                                <label class="col-md-3 control-label ">
                                    Titulo Publicaci&oacute;n</label>
                                <div class="col-md-7">
                                    <input name="txtTituloE" type="text" id="txtTituloE" value="" class="form-control" />
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-3 control-label ">
                                    Fecha Publicaci&oacute;n</label></label>
                                <div class="col-md-3">
                                    <div class="input-group date" id="Div1">
                                        <input name="txtFechaE" class="form-control" id="txtFechaE" style="text-align: right;"
                                            type="text" placeholder="__/__/____" data-provide="datepicker" />
                                        <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I1"></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-3 control-label ">
                                    Participación</label>
                                <div class="col-md-3">
                                    <select name="cboParticipacionE" class="form-control" id="cboParticipacionE" runat="server">
                                        <option value="0" selected>-- Seleccione --</option>
                                        <option value="1">Primer autor</option>
                                        <option value="2">Segundo autor</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-3 control-label ">
                                    Base Datos Revista</label>
                                <div class="col-md-4">
                                    <select name="cboBDRevistaE" class="form-control" id="cboBDRevistaE" runat="server">
                                        <option value="0" selected>-- Seleccione --</option>
                                        <%--    <option value="1" >Web Of Sience </option>
                                                <option value="2" >Scopus</option>
                                                <option value="3" >SciELO</option>--%>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-3 control-label ">
                                    Revista</label>
                                <div class="col-md-7">
                                    <input name="txtRevistaE" type="text" id="txtRevistaE" value="" class="form-control" />
                                </div>
                            </div>
                            <div class="row">
                                <label class="col-md-3 control-label ">
                                    URL Publicaci&oacute;n
                                </label>
                                <div class="col-md-7">
                                    <input name="txtURLPubliE" type="text" id="txtURLPubliE" value="" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <center>
                                <button type="button" id="btnGuardar" class="btn btn-success">
                                    Guardar</button>
                                <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                    Cancelar</button>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="mdMensaje" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 5;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" style="color: White">
                            <div id="divTitle">
                            </div>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12" id="divMensaje">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <div class="btn-group">
                                <button type="button" class="btn btn-primary" id="btnAceptar">
                                    <i class="ion-android-done"></i>&nbsp;Aceptar</button>
                                <button type="button" class="btn btn-danger" data-dismiss="modal">
                                    <i class="ion-android-cancel"></i>&nbsp;Cancelar</button>
                            </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div> </div>
    </form>
</body>
</html>
