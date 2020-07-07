<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRevisarBonos.aspx.vb"
    Inherits="GestionInvestigacion_frmRevisarBonos" %>

<!DOCTYPE html>
<html>
<head>
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

    <%-- ======================= Fin Notificaciones =============================================--%>
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
        }
    </style>

    <script type="text/javascript">
        var aDataBP = [];
        $(document).ready(function() {
            //document.execCommand('ClearAuthenticationCache');
            var dt = fnCreateDataTableBasic('tBonos', 1, 'asc');
            //fnResetDataTableTramite('tBonos', 0, 'asc');

            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }

            fnListarBonos(1);
            $("#btnRechazar").click(fnEstadoBono);
            $("#btnAceptarB").click(fnCambiarEstadoB);
            $("#btnConsultar").click(fnConsultarB);
            $("#btnAprobar").click(fnAprobarB);


            $("#btnObservar").click(function() {
                $("#mdObservar").modal("show");
                $("#txtDescripcionObservacion").val("");
            });

            $("#btnGuardarObservacion").click(function() {
                $("#btnRechazar").attr('disabled', 'disabled');
                $("#btnAprobar").attr('disabled', 'disabled');
                fnEnviar($("#hdBono").val(), 0);
            });
            fnListarBDRevistas();
            $("#cboMotivoRechazo").change(function() {
                if ($(this).val() == '4') {
                    $("#divMotivoRechazo").show();
                    $("#txtRechazo").val('');
                } else {
                    $("#divMotivoRechazo").hide();
                }
            })
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

        function fnConsultarB() {
            if ($("#cboEstado").val() == "0") {
                fnMensaje("warning", "Seleccionar Estado");
                return false;
            } else {
                if ($("#cboEstado").val() == "TO") {
                    fnListarBonos(1);
                    return false;
                } else {
                    fnDestroyDataTableDetalle('tBonos');
                    $('#tbBonos').html('');
                    $.ajax({
                        type: "GET",
                        //contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: { "action": "lBonosPublicacionEstado", "param1": $("#cboEstado").val() },
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            aDataBP = data;
                            var tb = '';
                            var i = 0;
                            var filas = aDataBP.length;
                            var mostrar = "";
                            if (filas > 0) {
                                for (i = 0; i < aDataBP.length; i++) {
                                    tb += '<tr>';
                                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                                    tb += '<td>' + aDataBP[i].d_tit + '</td>';
                                    tb += '<td>' + aDataBP[i].n_inv + '</td>';
                                    tb += '<td style="text-align:center">' + aDataBP[i].f_tit + '</td>';
                                    tb += '<td style="text-align:center">' + aDataBP[i].d_iea + '</td>';
                                    if (aDataBP[i].d_iea == "APROBADO" || aDataBP[i].d_iea == "RECHAZADO" || aDataBP[i].d_iea == "OBSERVADO" || aDataBP[i].d_iea == "REGISTRO") {
                                        mostrar = "disabled";
                                    } else {
                                        mostrar = "";
                                    }
                                    tb += '<td style="text-align:center">';
                                    tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(\'' + aDataBP[i].c_bon + '\')" title="Editar" ><i class="ion-eye"></i></button>';
                                    tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-green" onclick="CambiarEstadoBon(' + aDataBP[i].c_bon + ',\'' + 'A' + '\')" title="Aprobar Revisión" ' + mostrar + ' ><i class="ion-checkmark"></i></button>';
                                    tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="CambiarEstadoBon(' + aDataBP[i].c_bon + ',\'' + 'R' + '\')" title="Rechazar Registro" ' + mostrar + '><i class="ion-close"></i></button>';
                                    tb += '</td>';
                                    tb += '</tr>';
                                }
                            }

                            fnDestroyDataTableDetalle('tBonos');
                            $('#tbBonos').append(tb);
                            //fnResetDataTableTramite('tBonos', 0, 'asc');
                            fnResetDataTableBasic('tBonos', 0, 'asc');
                            return false;
                        },
                        error: function(result) {
                            //console.log("...................");
                            //console.log(result);
                            fnMensaje("warning", result)
                        }
                    });
                }
                return false;
            }

        }

        function fnEnviar(cod, vered) {
            rpta = fnvalidaSession();
            if (rpta == true) {
                var param2 = $("#txtDescripcionObservacion").val();
                var param3 = "BON";
                var param4 = $("#hdCtf").val();
                var param5 = "OBSERVADO";
                var param6 = 0;
                //alert(cod + "-" + param2 + "-" + param3 + "-" + param4 + "-" + param5 + "-" + param6);

                $("#DivGuardar").attr("style", "display:none");
                $("#MensajeGuardar").attr("style", "display:block");
                $("#MensajeGuardar").html('<b>Guardando Postulación...</b>');

                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "gHistorialGI", "param1": cod, "param2": param2, "param3": param3, "param4": param4, "param5": param5, "param6": param6, "hdUser": $("#hdUser").val() },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        if (data[0].Status == "success") {
                            $("#btnGuardarObservacion").removeAttr('disabled');
                            //document.execCommand('ClearAuthenticationCache');
                            fnListarObservaciones($("#hdBono").val(), 'BON');
                            fnListarBonos(1);
                            EnviarEmail(cod, param3, "OBSERVACIÓN", param2);
                            fnMensaje("success", data[0].Message);
                            if (vered == 1) {
                                $("#mdEdicionG").modal("hide");
                                $("#mdObservar").modal("hide");
                            } else {
                                $("#mdEdicionG").modal("show");
                                $("#mdObservar").modal("hide");
                            }
                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');
                        } else {
                            //document.execCommand('ClearAuthenticationCache');
                            $("#mdObservar").modal("hide");
                            fnListarBonos(1);
                            fnMensaje("error", data[0].Message);

                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');
                        }

                    },
                    error: function(result) {
                        fnListarBonos(1);
                        $("#DivGuardar").attr("style", "display:block");
                        $("#MensajeGuardar").attr("style", "display:none");
                        $("#MensajeGuardar").html('');
                    }

                });
                //alert("observado");

                return false;
            } else {
                window.location.href = rpta;
            }
        }

        function EnviarEmail(cod, tipo, estado, descripcion) {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "SubmitEMail", "param1": cod, "param2": tipo, "param3": estado, "param4": descripcion },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    if (data[0].Status == "success") {
                        alert("Mensaje de correo enviado");
                    }
                },
                error: function(result) {
                    console.log(result); //--para errores                      
                }

            });
        }


        function fnCambiarEstadoB() {
            CambiarEstadoBono1($("#hdBono").val(), $("#hdTipo").val());
        }

        function CambiarEstadoBono1(reg, tip) {
            rpta = fnvalidaSession();
            if (rpta == true) {

                var sw = 0;
                var estado = "";
                var descripcioncorreo = ""
                var detalle = '';
                if (tip == "R") {
                    estado = "RECHAZADO";
                    if ($("#cboMotivoRechazo").val() == '0') {
                        sw = 1;
                    } else if ($("#cboMotivoRechazo").val() == '4' && $("#txtRechazo").val() == '') {
                        sw = 2;
                    } else {
                        sw = 0;
                    }
                    descripcioncorreo = $("#txtRechazo").val()
                } else {
                    estado = "APROBACIÓN";
                    descripcioncorreo = ""
                    //$("#txtRechazo").val("*");
                    detalle = '*';
                    sw = 0;
                }
                if (sw == 1) {
                    fnMensaje("warning", "Seleccione Motivo de Rechazo");
                    return false;
                } else if (sw == 2) {
                    fnMensaje("warning", "Ingrese detalle de Motivo de Rechazo");
                    return false;
                } else {
                    if ($("#cboMotivoRechazo").val() == 4) {
                        detalle = $("#txtRechazo").val();
                    }
                    $.ajax({
                        type: "GET",
                        //contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: { "action": "actualizarBonoPublicacion", "param1": reg, "param2": tip, "param3": $("#hdUser").val(), "param4": detalle, "param5": $("#cboMotivoRechazo").val() },
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            //console.log(data);
                            //document.execCommand('ClearAuthenticationCache');
                            fnListarBonos(1);
                            fnMensaje(data[0].Status, data[0].Message);
                            EnviarEmail(reg, 'BON', estado, descripcioncorreo);
                        },
                        error: function(result) {
                            //alert(result);
                            fnMensaje("warning", result);

                        }
                    });
                    $('div#mdEdicion').modal('hide');
                    $('div#mdMensaje').modal('hide');
                }
            } else {
                window.location.href = rpta;
            }
        }

        function fnAprobarB() {
            $("#hdTipo").val("A");
            CambiarEstadoBono($("#hdBono").val(), $("#hdTipo").val());
        }

        function fnEstadoBono() {
            $("#hdTipo").val("R");
            CambiarEstadoBono($("#hdBono").val(), $("#hdTipo").val());
        }


        function CambiarEstadoBono(reg, tip) {
            rpta = fnvalidaSession();
            if (rpta == true) {

                var y = fnBuscar(reg);
                $("#hdBono").val(reg);
                $("#hdTipo").val(tip);
                if (tip == "A") {
                    document.getElementById("divTitle").innerHTML = "Aprobar para Bono";
                    document.getElementById("divMensaje").innerHTML = "¿Desea Aprobar para Bono: " + aDataBP[y].d_tit + "?";

                    fnMensajeConfirmarEliminar('top', "¿Desea Aprobar para Bono: " + aDataBP[y].d_tit + "?", 'CambiarEstadoBono1', reg, tip);
                    //$('#divMotivoRechazo').hide();
                    $('div#mdMensaje').modal('hide');
                } else {
                    document.getElementById("divTitle").innerHTML = "Rechazar Grupo Investigador";
                    document.getElementById("divMensaje").innerHTML = "¿Desea Rechazar para Bono: " + aDataBP[y].d_tit + "?";
                    $("#cboMotivoRechazo").val("0");
                    $('div#mdMensaje').modal('show');
                    $("#divMotivoRechazo").hide();
                }
            } else {
                window.location.href = rpta;
            }
        }

        function fnListarBonos(cbo) {
            var cboVal = "";
            if (cbo == "1") {
                cboVal = "TO";
            } else {
                cboVal = $("#cboEstado").val();
            }
            if (cboVal == "0") {
                fnMensaje("warning", "Elegir Estado de Bono");
                return false;
            }
            else {
                fnDestroyDataTableDetalle('tBonos');
                $('#tbBonos').html('');
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "lBonosPublicacionTO", "param1": 0, "param2": cboVal },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //console.log ("------------");
                        //console.log(data);
                        aDataBP = data;
                        var tb = '';
                        var i = 0;
                        var filas = aDataBP.length;
                        var mostrar = "";
                        if (filas > 0) {
                            for (i = 0; i < aDataBP.length; i++) {
                                tb += '<tr>';
                                tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                                tb += '<td>' + aDataBP[i].d_tit + '</td>';
                                tb += '<td>' + aDataBP[i].n_inv + '</td>';
                                tb += '<td style="text-align:center">' + aDataBP[i].f_tit + '</td>';
                                tb += '<td style="text-align:center">' + aDataBP[i].d_iea + '</td>';
                                if (aDataBP[i].d_iea == "APROBADO" || aDataBP[i].d_iea == "RECHAZADO" || aDataBP[i].d_iea == "OBSERVADO") {
                                    mostrar = "disabled";
                                } else {
                                    mostrar = "";
                                }
                                tb += '<td style="text-align:center">';
                                tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(\'' + aDataBP[i].c_bon + '\')" title="Editar" ><i class="ion-eye"></i></button>';
                                tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-green" onclick="CambiarEstadoBon(' + aDataBP[i].c_bon + ',\'' + 'A' + '\')" title="Aprobar Revisión" ' + mostrar + ' ><i class="ion-checkmark"></i></button>';
                                tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="CambiarEstadoBon(' + aDataBP[i].c_bon + ',\'' + 'R' + '\')" title="Rechazar Registro" ' + mostrar + '><i class="ion-close"></i></button>';
                                tb += '</td>';
                                tb += '</tr>';
                            }
                        }

                        fnDestroyDataTableDetalle('tBonos');
                        $('#tbBonos').append(tb);
                        //fnResetDataTableTramite('tBonos', 0, 'asc');
                        fnResetDataTableBasic('tBonos', 0, 'asc');
                        return false;
                    },
                    error: function(result) {
                        //console.log("...................");
                        console.log(result);
                        //fnMensaje("warning", result)
                    }
                });

                return false;
            }

        }

        function CambiarEstadoBon(reg, tip) {
            rpta = fnvalidaSession();
            if (rpta == true) {

                var y = fnBuscar(reg);
                $("#hdBono").val(reg);
                $("#hdTipo").val(tip);
                if (tip == "A") {
                    document.getElementById("divTitle").innerHTML = "Aprobar Publicación";
                    document.getElementById("divMensaje").innerHTML = "¿Desea Aprobar Publicación: " + aDataBP[y].d_tit + "?";
                    $('#divMotivoRechazo').hide();
                    fnMensajeConfirmarEliminar('top', "¿Desea Aprobar Publicación: " + aDataBP[y].d_tit + "?", 'CambiarEstadoBono1', reg, tip);
                } else {
                    document.getElementById("divTitle").innerHTML = "Rechazar Publicación";
                    document.getElementById("divMensaje").innerHTML = "¿Desea Rechazar Publicación: " + aDataBP[y].d_tit + "?";
                    $('#divMotivoRechazo').hide();
                    $('#txtRechazo').val('');
                    $('#cboMotivoRechazo').val('0');
                    $("#mdMensaje").modal("show");
                }
            } else {
                window.location.href = rpta;
            }
        }

        function limpiar() {
            $('#txtTituloE').val("");
            $('#txtFechaE').val("");
            $("#cboBaseDatos").val("");
            $("#cboParticipacion").val("");
            $('#txtRevistaE').val("");
            $('#txtURLPubliE').val("");
        }

        function fnEditar(bon) {
            limpiar();
            $('#hdAccion').val("A");
            var y = fnBuscar(bon);
            $('#hdBono').val(aDataBP[y].c_bon);
            $('#txtTituloE').val(aDataBP[y].d_tit);
            $('#txtFechaE').val(aDataBP[y].f_tit);
            $('#cboParticipacion').val(aDataBP[y].c_tipopart);
            $('#txtRevistaE').val(aDataBP[y].r_bon);
            $('#txtURLPubliE').val(aDataBP[y].u_bon);
            $("#cboBaseDatos").val(aDataBP[y].c_bdrev);
            var boton = document.getElementById("btnAprobar");
            var boton1 = document.getElementById("btnObservar");
            var boton2 = document.getElementById("btnRechazar");

            if (aDataBP[y].d_iea == "APROBADO" || aDataBP[y].d_iea == "RECHAZADO" || aDataBP[y].d_iea == "OBSERVADO" || aDataBP[y].d_iea == "REGISTRO") {
                boton.disabled = true;
                boton1.disabled = true;
                boton2.disabled = true;
            } else {
                boton.disabled = false;
                boton1.disabled = false;
                boton2.disabled = false;
            }
            fnListarObservaciones($("#hdBono").val(), 'BON');
            $("#mdEdicion").modal("show");
        }

        function fnListarBDRevistas() {
            $.ajax({
                type: "GET",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "ListarBaseDatosRevista" },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var filas = data.length;
                    var div = "";
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            div = div + "<option value='" + data[i].cod + "'>" + data[i].nombre + "</option>";
                        }
                    }
                    $("#cboBaseDatos").append(div);

                },
                error: function(result) {
                    return false;
                }

            });

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
                                    div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:blue'> .: " + data[i].d_fech + " - " + data[i].d_eia + " - " + data[i].d_obs + "</label></br>";
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
                    return false;
                }

            });
            return false;
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
        
        
        
        
    </script>

</head>
<body>
    <form id="frmRevisarBonoPublicacion" name="frmRevisarBonoPublicacion">
    <input type="hidden" id="action" name="action" value="" />
    <input type="hidden" id="hdUser" name="hdUser" value="" runat="server" />
    <input type="hidden" id="hdAccion" name="hdAccion" value="" runat="server" />
    <input type="hidden" id="hdBono" name="hdBono" value="" runat="server" />
    <input type="hidden" id="hdTipo" name="hdTipo" value="" runat="server" />
    <input type="hidden" id="hdReg" name="hdReg" value="" runat="server" />
    <input type="hidden" id="hdCtf" name="hdCtf" value="" runat="server" />
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Revisión
                                                de Bonos de Publicaci&oacute;n</span>
                                        </div>
                                        <div class="buttons-list">
                                            <div class="pull-right-btn">
                                                <button class="btn btn-primary" id="btnConsultar" value="Consultar">
                                                    Consultar</button>
                                                <!--<button class="btn btn-green" id="btnAgregar" value="Agregar" data-toggle="modal"
                                                    data-target="#mdRegistro">
                                                    Agregar</button>-->
                                                <%--<button class="btn btn-primary" id="btnExportar">Exportar</button>--%>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <!--<div class="col-md-7">
                                                <label class="col-md-4 control-label ">
                                                    Docente / Administrativo</label>
                                                <div class="col-md-8">
                                                    <select name="cboPersonal" class="form-control" id="cboPersonal">
                                                        <option value="" selected="">-- Seleccione -- </option>
                                                    </select>
                                                </div>
                                            </div>-->
                                            <div class="col-md-5">
                                                <label class="col-md-4 control-label ">
                                                    Estado de Bonos</label>
                                                <div class="col-md-8">
                                                    <select name="cboEstado" class="form-control" id="cboEstado">
                                                        <option value="0" selected="">-- Seleccione -- </option>
                                                        <option value="REGISTRO">REGISTRO</option>
                                                        <option value="EN EVALUACIÓN">EN EVALUACIÓN</option>
                                                        <option value="OBSERVADO">OBSERVADOS</option>
                                                        <option value="RECHAZADO">RECHAZADOS</option>
                                                        <option value="APROBADO">APROBADOS</option>
                                                        <option value="TO">TODOS</option>
                                                    </select>
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
                    <div class="panel-piluku">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Registro de Publicaciones para Bono
                                <%--                                <span class="panel-options"><a class="panel-refresh" href="#"><i class="icon ti-reload"
                                    onclick="fnBuscarConvocatoria(false)"></i></a><a class="panel-minimize" href="#"><i
                                        class="icon ti-angle-up"></i></a></span>--%>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div id="tBonos_wrapper" class="dataTables_wrapper" role="grid">
                                    <table id="tBonos" name="tBonos" class="display dataTable" width="100%">
                                        <thead>
                                            <tr role="row">
                                                <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                    N°
                                                </td>
                                                <td width="30%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Titulo
                                                </td>
                                                <td width="20%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                    Investigador(a)
                                                </td>
                                                <td width="8%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                    Fecha
                                                </td>
                                                <td width="12%" style="font-weight: bold; width: 272px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                    Estado Bono
                                                </td>
                                                <td width="15%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
                                                    Opciones
                                                </td>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <th colspan="6" rowspan="1">
                                                </th>
                                            </tr>
                                        </tfoot>
                                        <tbody id="tbBonos">
                                        </tbody>
                                    </table>
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
                                    <div class="row" id="Div2" style="text-align: right;">
                                        <button type="button" class="btn btn-success" id="btnAprobar">
                                            Aprobar</button>
                                        <button type="button" id="btnObservar" class="btn btn-warning">
                                            Observar</button>
                                        <button type="button" id="btnRechazar" class="btn btn-red">
                                            Rechazar</button>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Titulo Publicaci&oacute;n</label>
                                        <div class="col-md-7">
                                            <input name="txtTituloE" type="text" id="txtTituloE" value="" class="form-control"
                                                readonly="true" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Fecha Publicaci&oacute;n de artículo</label></label>
                                        <div class="col-md-3">
                                            <div class="input-group date" id="Div1">
                                                <input name="txtFechaE" class="form-control" id="txtFechaE" style="text-align: right;"
                                                    type="text" placeholder="__/__/____" readonly="true" />
                                                <span class="input-group-addon sm"><i class="ion ion-ios-calendar-outline" id="I1"></i>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Base De Datos</label>
                                        <div class="col-md-7">
                                            <select id="cboBaseDatos" class="form-control" disabled="disabled">
                                                <option value="">--Seleccione--</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Participación</label>
                                        <div class="col-md-3">
                                            <select name="cboParticipacion" class="form-control" id="cboParticipacion" runat="server"
                                                disabled="disabled">
                                                <option value="0" selected>-- Seleccione --</option>
                                                <option value="1">Primer autor</option>
                                                <option value="2">Segundo autor</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            Revista</label>
                                        <div class="col-md-7">
                                            <input name="txtRevistaE" type="text" id="txtRevistaE" value="" class="form-control"
                                                readonly="true" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-3 control-label ">
                                            URL Publicaci&oacute;n
                                        </label>
                                        <div class="col-md-7">
                                            <input name="txtURLPubliE" type="text" id="txtURLPubliE" value="" class="form-control"
                                                readonly="true" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <!--<button type="button" id="btnGuardar" class="btn btn-success">
                                            Guardar</button>-->
                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdObservar" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 10;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div3">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H3">
                                        Observar Grupo de Investigación</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmObservar" name="frmObservar" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtobjetivo">
                                                Observación:</label>
                                            <div class="col-sm-7">
                                                <textarea id="txtDescripcionObservacion" name="txtDescripcionObservacion" cols="20"
                                                    rows="3" style="width: 100%"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <div id="DivGuardar">
                                        <center>
                                            <button type="button" class="btn btn-primary" id="btnGuardarObservacion">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="btnCancelarObs" data-dismiss="modal">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                    <center>
                                        <div class="alert alert-success" id="MensajeGuardar" style="display: none;">
                                        </div>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="mdMensaje" tabindex="5" role="dialog" aria-labelledby="myModalLabel"
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
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="col-sm-2 control-label" for="cboMotivoRechazo">
                                            Motivo:</label>
                                        <div class="col-sm-10">
                                            <select name="cboMotivoRechazo" class="form-control" id="cboMotivoRechazo" runat="server">
                                                <option value="0" selected>-- Seleccione --</option>
                                                <option value="1">Artículo no indizado a Scopus, Scielo, ISI, Revistas USAT</option>
                                                <option value="2">Docente sin filiación USAT</option>
                                                <option value="3">Docente no es primer ni segundo autor</option>
                                                <option value="4">Otros</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="divMotivoRechazo">
                                        <label class="col-sm-2 control-label" for="txtRechazo">
                                            detalle:</label>
                                        <div class="col-sm-10">
                                            <textarea id="txtRechazo" name="txtRechazo" cols="20" rows="3" style="width: 100%"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-primary" id="btnAceptarB">
                                            <i class="ion-android-done"></i>&nbsp;Aceptar</button>
                                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                                            <i class="ion-android-cancel"></i>&nbsp;Cancelar</button>
                                    </div>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                </div>
            </div>
        </div>
    </div>
    <div class="hiddendiv common">
    </div>
    </form>
</body>
</html>
