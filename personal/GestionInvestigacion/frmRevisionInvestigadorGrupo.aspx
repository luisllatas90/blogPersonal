<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRevisionInvestigadorGrupo.aspx.vb"
    Inherits="GestionInvestigacion_frmRevisionInvestigadorGrupo" %>

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
        var aDataI = [];
        var aDataGI = [];
        $(document).ready(function() {
            //document.execCommand('ClearAuthenticationCache');
            window.setInterval("RecargarAvisos()", 600000);
            //window.setInterval("RecargarAvisos()", 10000);

            rpta = fnvalidaSession();
            if (rpta == false) {
                window.location.href = rpta;
            }
            fnLineas();
            //fnResetDataTableTramite('tInvestigador', 0, 'desc');
            //fnResetDataTableTramite('tGrupoInvestigador', 0, 'desc');
            var dt = fnCreateDataTableBasic('tInvestigador', 1, 'asc');
            var dt = fnCreateDataTableBasic('tGrupoInvestigador', 1, 'asc');
            $("#labelrps").html("Colaboradores <FONT COLOR='red'><b>(0)</b></FONT>");
            $("#labelrpsG").html("Grupos de Investigación <FONT COLOR='red'><b>(0)</b></FONT>");
            ListaInvestigadoresEstado(0, 'ES', 'X');
            ListaInvestigadoresEstado(0, 'ES', 'T');
            $("#btnAprobar").click(fnEstadoInvA);
            $("#btnRechazar").click(fnEstadoInvR);
            $("#btnAprobarG").click(fnEstadoGrupoA);
            $("#btnRechazarG").click(fnEstadoGrupoR);
            $("#btnAceptar").click(fnCambiarEstado);
            $("#btnAceptarG").click(fnCambiarEstadoG);

            $("#btnReactivar").click(fnReactivarEstadoG);

            ListaGrupoInvestigadores("T");
            ListaGrupoInvestigadores("X");

            $("#btnObservar").click(function() {
                $("#mdObservar").modal("show");
                $("#txtDescripcionObservacion").val("");
            });

            $("#btnObservarInv").click(function() {
                $("#mdObservarInv").modal("show");
                $("#txtDescripcionObservacionInv").val("");
            });

            $("#btnGuardarObservacion").click(function() {
                fnEnviar($("#hdRegG").val(), 0);
            });

            $("#btnGuardarObservacionInv").click(function() {
                $("#btnRechazar").attr('disabled', 'disabled');
                $("#btnAprobar").attr('disabled', 'disabled');
                fnEnviarInv($("#hdReg").val(), 0);
            });

            //RecargarAvisos();
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

        function fnListarObservacionesINV(cod, inv) {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "gVisualizarHistorialGI", "param1": cod, "param2": inv },
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
                    $("#DivObservacionesInv").html(div);

                },
                error: function(result) {
                    fnMensaje("warning", result);
                }

            });
            return false;
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
                                if (data[i].c_obs == 2 || data[i].c_obs == 3) {
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
                    fnMensaje("warning", result)
                }

            });
            return false;
        }

        function fnEnviarInv(cod, vered) {
            var param2 = $("#txtDescripcionObservacionInv").val();
            var param3 = "INV";
            var param4 = $("#hdCtf").val();
            var param5 = "OBSERVADO";
            var param6 = 0;
            //alert(cod + "-" + param2 + "-" + param3 + "-" + param4 + "-" + param5 + "-" + param6);

            rpta = fnvalidaSession();
            if (rpta == true) {

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
                        //document.execCommand('ClearAuthenticationCache');
                        if (data[0].Status == "success") {
                            fnListarObservacionesINV($("#hdReg").val(), 'INV');
                            if (vered == 1) {
                                $("#mdEdicion").modal("hide");
                            } else {
                                $("#mdEdicion").modal("show");
                                $("#mdObservarInv").modal("hide");
                            }
                            RecargarAvisos();
                            fnMensaje("success", data[0].Message);
                            EnviarEmail(cod, param3, "OBSERVACIÓN", param2);

                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');

                        } else {
                            RecargarAvisos();

                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');

                            fnMensaje("error", data[0].Message);
                        }
                    },
                    error: function(result) {
                        RecargarAvisos();
                        $("#DivGuardar").attr("style", "display:block");
                        $("#MensajeGuardar").attr("style", "display:none");
                        $("#MensajeGuardar").html('');

                        fnMensaje("warning", result);
                    }

                });
                //alert("observado");

                return false;

            } else {
                window.location.href = rpta;
            }
        }

        function fnEnviar(cod, vered) {

            rpta = fnvalidaSession();
            if (rpta == true) {

                var param2 = $("#txtDescripcionObservacion").val();
                var param3 = "GRU";
                var param4 = $("#hdCtf").val();
                var param5 = "OBSERVADO";
                var param6 = 0;
                //alert(cod + "-" + param2 + "-" + param3 + "-" + param4 + "-" + param5 + "-" + param6);
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "gHistorialGI", "param1": cod, "param2": param2, "param3": param3, "param4": param4, "param5": param5, "param6": param6, "hdUser": $("#hdUser").val() },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //document.execCommand('ClearAuthenticationCache');
                        if (data[0].Status == "success") {
                            fnListarObservaciones($("#hdRegG").val(), 'GRU');
                            RecargarAvisos();
                            if (vered == 1) {
                                $("#mdEdicionG").modal("hide");
                            } else {
                                $("#mdEdicionG").modal("show");
                                $("#mdObservar").modal("hide");
                            }
                            fnMensaje("success", data[0].Message);
                            EnviarEmail(cod, param3, "OBSERVACIÓN", param2);
                        } else {
                            RecargarAvisos();
                            $("#mdEdicionG").modal("hide");
                            $("#mdObservar").modal("hide");
                            fnMensaje("error", data[0].Message);
                        }
                    },
                    error: function(result) {
                        fnMensaje("warning", result);
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

        function RecargarAvisos() {
            //alert("avisos");
            ListaInvestigadoresEstado(0, 'ES', 'X');
            fnDestroyDataTableDetalle('tInvestigador');
            $('#tbInvestigador').html('');

            ListaInvestigadoresEstado(0, 'ES', 'T');

            fnDestroyDataTableDetalle('tGrupoInvestigador');
            $('#tbGrupoInvestigador').html('');

            ListaGrupoInvestigadores("T");
            ListaGrupoInvestigadores("X");
            //alert("Alerta");
        }

        function ListaGrupoInvestigadores(tipo) {
            var nombreGrupo = "";
            $("#hdCantRegInvRev").val("0");
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lGrupoInvestigadores", "param1": 0 },
                dataType: "json",
                cache: false,
                async: true,
                success: function(data) {
                    var tb = '';
                    var i = 0;
                    var nro = 0;
                    var nroRow = 0;
                    aDataGI = data;
                    var filas = aDataGI.length;
                    var mostrar = "";
                    if (filas > 0) {
                        for (i = 0; i < aDataGI.length; i++) {
                            if (nombreGrupo != aDataGI[i].d_gru) {
                                if (tipo == "X") {
                                    //$("#hdCantRegInvRev").val(nro);
                                    $("#labelrpsG").html("Grupos de Investigación <FONT COLOR='red'><b>(" + $("#hdCantRegInvRev").val() + ")</b></FONT>");

                                } else {
                                    if (aDataGI[i].d_iea != "REGISTRO") {
                                        nroRow = nroRow + 1;
                                        if (aDataGI[i].d_iea == "EN EVALUACIÓN") {
                                            nro = nro + 1;
                                            mostrar = "";
                                            $("#hdCantRegInvRev").val(nro);
                                            tb += '<tr style="color: #FF0000">';
                                        } else {
                                            if (aDataGI[i].d_iea == "APROBADO" || aDataGI[i].d_iea == "RECHAZADO" || aDataGI[i].d_iea == "OBSERVADO") {
                                                mostrar = "disabled";
                                            } else {
                                                mostrar = "";
                                            }
                                            tb += '<tr>';
                                        }
                                        tb += '<td style="text-align:center">' + (nroRow) + "" + '</td>';
                                        tb += '<td style="text-align:left">' + aDataGI[i].d_gru + '</td>';
                                        tb += '<td style="text-align:left">' + aDataGI[i].d_lin + '</td>';
                                        tb += '<td style="text-align:center">' + aDataGI[i].d_iea + '</td>';
                                        tb += '<td style="text-align:center">';
                                        tb += '<button type="button" id="btnEG" name="btnEG" class="btn btn-sm btn-info" onclick="fnEGrupo(\'' + aDataGI[i].c_gru + '\')" title="Editar" ><i class="ion-eye"></i></button>';
                                        tb += '<button type="button" id="btnEnviarG" name="btnEnviarG" class="btn btn-sm btn-green" onclick="CambiarEstadoGrupo(' + aDataGI[i].c_gru + ',\'' + 'A' + '\')" title="Aprobar Revisión" ' + mostrar + '><i class=" ion-checkmark"></i></button>';
                                        tb += '<button type="button" id="btnDG" name="btnDG" class="btn btn-sm btn-red" onclick="CambiarEstadoGrupo(' + aDataGI[i].c_gru + ',\'' + 'R' + '\')" title="Rechazar Registro" ' + mostrar + '><i class="ion-close"></i></button>';
                                        tb += '</td>';
                                        tb += '</tr>';
                                        nombreGrupo = aDataGI[i].d_gru;
                                    }
                                }
                            }
                        }
                        //fnDestroyDataTableDetalle('tBonos');
                        if (tipo == "T") {
                            fnDestroyDataTableDetalle('tGrupoInvestigador');
                            $('#tbGrupoInvestigador').append(tb);
                            //fnResetDataTableTramite('tGrupoInvestigador', 0, 'asc');
                            fnResetDataTableBasic('tGrupoInvestigador', 0, 'asc');
                        }
                    }
                },
                error: function(result) {
                    //alert("a");
                    fnMensaje("warning", result)
                }
            });

        }

        function ListaInvestigadoresEstado(param1, param2, param3) {
            $.ajax({
                type: "GET",
                //contentType: "application/json; charset=utf-8",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lInvestigadoresEstado", "param1": param1, "param2": param2, "param3": param3 },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var i = 0;
                    if (param3 == "X") {
                        var filas = data.length;
                        $("#hdCantInvRev").val(filas);
                        $("#labelrps").html("Colaboradores <FONT COLOR='red'><b>(" + filas + ")</b></FONT>");
                        //alert(filas);
                    } else {
                        aDataI = data;
                        var filas = aDataI.length;
                        var mostrar = '';
                        if (filas > 0) {
                            for (i = 0; i < aDataI.length; i++) {
                                if (aDataI[i].d_rev == "EN EVALUACIÓN") {
                                    tb += '<tr style="color: #FF0000">';
                                } else {
                                    if (aDataI[i].d_rev == "APROBADO" || aDataI[i].d_rev == "RECHAZADO" || aDataI[i].d_rev == "OBSERVADO") {
                                        mostrar = "disabled";
                                    } else {
                                        mostrar = "";
                                    }
                                    tb += '<tr>';
                                }
                                tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                                tb += '<td style="text-align:left">' + aDataI[i].d_per + '</td>';
                                tb += '<td>' + aDataI[i].d_tpe + '</td>';
                                tb += '<td style="text-align:center">' + aDataI[i].d_rev + '</td>';
                                tb += '<td style="text-align:center">';
                                tb += '<button type="button" id="btnEditar" name="btnEditar" class="btn btn-sm btn-info" onclick="fnEditar(\'' + aDataI[i].c_inv + '\')" title="Editar" ><i class="ion-eye"></i></button>';
                                tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-green" onclick="CambiarEstadoInvestigador(' + aDataI[i].c_inv + ',\'' + 'A' + '\')" title="Aprobar Revisión" ' + mostrar + '><i class=" ion-checkmark"></i></button>';
                                tb += '<button type="button" id="btnDG" name="btnDG" class="btn btn-sm btn-red" onclick="CambiarEstadoInvestigador(' + aDataI[i].c_inv + ',\'' + 'R' + '\')" title="Rechazar Registro" ' + mostrar + '><i class="ion-close"></i></button>';
                                tb += '</td>';
                                tb += '</tr>';
                            }
                        }
                        if (param3 == "T") {
                            fnDestroyDataTableDetalle('tInvestigador');
                            $('#tbInvestigador').append(tb);
                            fnResetDataTableBasic('tInvestigador', 0, 'asc');
                        }
                    }
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });


        }
        function limpia() {
            $("#txtNombre").val("");
            $("#cboLinea").val("");
            $("#txtURLDina").val("");
            $("#txtOrcid").val("");
            $("#txtEspecialidad").val("");
            $("#txtTrabajador").val("");
            $("#txtDNI").val("");
        }

        function fnEGrupo(reg) {

            //EnviarEmail();
            //alert("mensaje");

            var y = fnBuscarG(reg);
            $("#hdRegG").val(reg);
            $("#txtGrupo").val(aDataGI[y].d_gru);
            var t = '';
            t = '<option value="' + aDataGI[y].c_lin + '" selected="' + '">' + aDataGI[y].d_lin + '</option>';
            $("#cboLineasUSAT").html(t);
            var boton = document.getElementById("btnAprobarG");
            var boton1 = document.getElementById("btnObservar");
            var boton2 = document.getElementById("btnRechazarG");
            if (aDataGI[y].d_iea == "APROBADO" || aDataGI[y].d_iea == "RECHAZADO" || aDataGI[y].d_iea == "OBSERVADO") {
                boton.disabled = true;
                boton1.disabled = true;
                boton2.disabled = true;
                $('#btnReactivar').show();
                if (aDataGI[y].d_iea == "OBSERVADO") {
                    boton1.disabled = false;
                    $('#btnReactivar').hide();
                }
            } else {
                boton.disabled = false;
                boton1.disabled = false;
                boton2.disabled = false;
                $('#btnReactivar').hide();
            }
            //            if (aDataGI[y].c_tipo == 1) {
            //                $("#c6").prop("checked", true);
            //            }
            //            if (aDataGI[y].c_tipo == 2) {
            //                $("#c5").prop("checked", true);
            //            }    
            if (aDataGI[y].dis == 0) {
                $('#divLineasOCDE').hide();
            } else {
                $('#divLineasOCDE').show();
                t = '<option value="' + aDataGI[y].c_are + '">' + aDataGI[y].d_are + '</option>';
                $("#cboArea").html(t);
                t = '<option value="' + aDataGI[y].c_sub + '">' + aDataGI[y].d_sar + '</option>';
                $("#cboSubArea").html(t);
                t = '<option value="' + aDataGI[y].dis + '">' + aDataGI[y].d_dis + '</option>';
                $("#cboDisciplina").html(t);
            }
            t = '<option value="' + aDataGI[y].c_reg + '">' + aDataGI[y].d_reg + '</option>';

            fnListarProvincia(aDataGI[y].c_reg);
            fnListarDistrito(aDataGI[y].prov_gru);
            $("#cboRegion").html(t);
            $("#cboProvincia").val(aDataGI[y].prov_gru);
            $("#cboDistrito").val(aDataGI[y].dist_gru);
            $("#txtLugar").val(aDataGI[y].lug_gru);
            //$("#propuesta").html("<a href='" + aDataGI[y].prop_gru + "' target='_blank' style='font-weight:bold'>Descargar Plan</a>");
            $("#propuesta").html('<a onclick="fnDownload(\'' + aDataGI[y].prop_gru + '\')" >Descargar Plan</a>')

            var tb = '';
            var nro = 0;
            var nombreGrupo = aDataGI[y].d_gru;
            var filas = aDataGI.length;
            if (filas > 0) {
                for (i = 0; i < aDataGI.length; i++) {
                    if (nombreGrupo == aDataGI[i].d_gru) {
                        nro = nro + 1;
                        tb += '<tr>';
                        tb += '<td width="5%" style="text-align:center">' + (nro) + "" + '</td>';
                        tb += '<td width="50%" style="text-align:center">' + aDataGI[i].d_nom + '</td>';
                        if (aDataGI[i].coord_dgi == 1) {
                            tb += '<td width="10%" style="text-align:center">' + "RESPONSABLE" + '</td>';
                        } else {
                            tb += '<td width="10%" style="text-align:center">' + "-" + '</td>';
                        }
                        tb += '<td width="10%" style="text-align:center">' + aDataGI[i].c_dni + '</td>';
                        //tb += '<td style="text-align:center">' + aDataGI[i].d_ded + '</td>';
                        //tb += '<td style="text-align:center">' + aDataGI[i].d_rin + '</td>';
                        tb += '</tr>';
                    }
                }
                $('#pGrupInvestigador').html('');
                $('#pGrupInvestigador').append(tb);
            }

            fnListarObservaciones($("#hdRegG").val(), 'GRU');

            $('div#mdEdicionG').modal('show');
        }
        /*
        function fnDownload(id_ar) {
        var flag = false;
        var form = new FormData();
        form.append("action", "Download2");
        form.append("IdArchivo", id_ar);
        // alert();
        //            console.log(form);
        $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
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
        }*/

        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }

        function fnListarProvincia(cod) {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="lProvincia" /><input type="hidden" id="codRegion" name="codRegion" value="' + cod + '" /></form>');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var filas = data.length;
                    tb += '<option value="0" selected="selected">-- Seleccione --</option>';
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<option value="' + data[i].cod + '">' + data[i].nombre + '</option>';
                        }
                        $("#cboProvincia").html(tb);
                    }
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }

        function fnListarDistrito(cod) {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="lDistrito" /><input type="hidden" id="codProvincia" name="codProvincia" value="' + cod + '" /></form>');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var filas = data.length;
                    tb += '<option value="0" selected="selected">-- Seleccione --</option>';
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<option value="' + data[i].cod + '">' + data[i].nombre + '</option>';
                        }
                        $("#cboDistrito").html(tb);
                    }
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }

        function fnEditar(reg) {
            var x = fnBuscar(reg);
            $("#hdReg").val(reg);
            $("#txtNombre").val(aDataI[x].d_per);
            $("#cboLinea").val(aDataI[x].cod_linea);
            $("#txtRenacyt").val(aDataI[x].cod_renacyt)
            $("#txtURLDina").val(aDataI[x].d_url);
            $("#txtOrcid").val(aDataI[x].orcid);
            $("#txtEspecialidad").val(aDataI[x].d_cgo);
            $("#txtTrabajador").val(aDataI[x].d_tpe);
            $("#txtDNI").val(aDataI[x].dni_per);

            var boton = document.getElementById("btnAprobar");
            var boton1 = document.getElementById("btnRechazar");
            var boton2 = document.getElementById("btnObservarInv");
            if (aDataI[x].d_iea == "APROBADO" || aDataI[x].d_iea == "RECHAZADO" || aDataI[x].d_iea == "OBSERVADO") {
                boton.disabled = true;
                boton1.disabled = true;
                boton2.disabled = true;
            } else {
                boton.disabled = false;
                boton1.disabled = false;
                boton2.disabled = false;
            }
            //alert(aDataI[x].dina);
            if (aDataI[x].dina == "1") {
                document.getElementById('chkDINA').checked = true;
            } else {
                //alert("0");
                document.getElementById('chkDINA').checked = false;
            }
            if (aDataI[x].regina == "1") {
                document.getElementById('chkREGINA').checked = true;
            } else {
                document.getElementById('chkREGINA').checked = false;
            }
            fnListarObservacionesINV($("#hdReg").val(), 'INV');

            $('div#mdEdicion').modal('show');

        }

        function fnEstadoGrupoA() {
            //alert("AAAA");
            $("#hdTipo").val("A");
            //alert($("#hdRegG").val() + "-" + $("#hdTipo").val());
            var y = fnBuscarG($("#hdRegG").val());
            fnMensajeConfirmarEliminar('top', "¿Desea Aprobar Grupo Investigador " + aDataGI[y].d_gru + "?", 'CambiarEstadoGrupo1', $("#hdRegG").val(), $("#hdTipo").val());
        }
        function fnEstadoGrupoR() {
            //alert("BBBB");
            $("#hdTipo").val("R");
            //alert($("#hdRegG").val() + "-" + $("#hdTipo").val());
            //CambiarEstadoGrupo1($("#hdRegG").val(), $("#hdTipo").val());
            CambiarEstadoGrupo($("#hdRegG").val(), $("#hdTipo").val());
        }


        function fnEstadoInvA() {
            $("#hdTipo").val("A");
            //alert($("#hdReg").val() + "-" + $("#hdTipo").val());
            fnMensajeConfirmarEliminar('top', "¿Desea Aprobar al colaborador: " + $("#txtNombre").val() + "?", 'CambiarEstadoInvestigador1', $("#hdReg").val(), $("#hdTipo").val());
            //CambiarEstadoInvestigador1($("#hdReg").val(), $("#hdTipo").val());
        }
        function fnEstadoInvR() {
            $("#hdTipo").val("R");
            //alert($("#hdReg").val() + "-" + $("#hdTipo").val());
            fnMensajeConfirmarEliminar('top', "¿Desea Rechazar al colaborador: " + $("#txtNombre").val() + "?", 'CambiarEstadoInvestigador1', $("#hdReg").val(), $("#hdTipo").val());
            //CambiarEstadoInvestigador1($("#hdReg").val(), $("#hdTipo").val());
        }

        function fnCambiarEstado() {
            CambiarEstadoInvestigador1($("#hdReg").val(), $("#hdTipo").val());
        }

        function fnCambiarEstadoG() {
            CambiarEstadoGrupo1($("#hdRegG").val(), $("#hdTipo").val());
        }

        function fnReactivarEstadoG() {
            var y = fnBuscarG($("#hdRegG").val());
            fnMensajeConfirmarEliminar('top', "¿Desea Reactivar el grupo: " + aDataGI[y].d_gru + "?", 'CambiarEstadoGrupo1', $("#hdRegG").val(), "V");
        }

        function CambiarEstadoGrupo(reg, tip) {
            $("#txtRechazo").val("");
            var y = fnBuscarG(reg);
            $("#hdRegG").val(reg);
            $("#hdTipo").val(tip);
            if (tip == "A") {
                document.getElementById("divTitleG").innerHTML = "Aprobar Grupo Investigador";
                document.getElementById("divMensajeG").innerHTML = "¿Desea Aprobar Grupo Investigador: " + aDataGI[y].d_gru + "?";
                $('#divMotivoRechazo').hide();
                fnMensajeConfirmarEliminar('top', "¿Desea Aprobar Grupo Investigador " + aDataGI[y].d_gru + "?", 'CambiarEstadoGrupo1', reg, tip);
                //$('div#mdMensajeG').modal('show');
            } else {
                document.getElementById("divTitleG").innerHTML = "Rechazar Grupo Investigador";
                document.getElementById("divMensajeG").innerHTML = "¿Desea Rechazar Grupo Investigador " + aDataGI[y].d_gru + "?";
                $('#divMotivoRechazo').show();
                $('div#mdMensajeG').modal('show');
            }

        }

        function CambiarEstadoInvestigador(reg, tip) {
            var x = fnBuscar(reg);

            $("#hdReg").val(reg);
            $("#hdTipo").val(tip);
            $("#txtNombre").val(aDataI[x].d_per);
            if (tip == "A") {
                document.getElementById("divTitle").innerHTML = "Aprobar Investigador";
                document.getElementById("divMensaje").innerHTML = "¿Desea Aprobar al Investigador: " + aDataI[x].d_per + "?";

                fnMensajeConfirmarEliminar('top', "¿Desea Aprobar al colaborador: " + aDataI[x].d_per + "?", 'CambiarEstadoInvestigador1', reg, tip);
                $('div#mdMensaje').modal('hide');
            } else {
                document.getElementById("divTitle").innerHTML = "Rechazar Investigador";
                document.getElementById("divMensaje").innerHTML = "¿Desea Rechazar al Investigador: " + aDataI[x].d_per + "?";

                fnMensajeConfirmarEliminar('top', "¿Desea Rechazar al colaborador: " + aDataI[x].d_per + "?", 'CambiarEstadoInvestigador1', reg, tip);
                $('div#mdMensaje').modal('hide');
            }

        }

        function CambiarEstadoGrupo1(reg, tip) {
            //alert("bbbbbbbbb");
            //alert(reg + "-" + tip);
            rpta = fnvalidaSession();
            if (rpta == true) {

                var estado = "";
                var sw = 0;
                if (tip == "A" || tip == "V") {
                    sw = 0;
                    if (tip == "A") {
                        estado = "APROBACIÓN";
                    } else {
                        estado = "REACTIVADO";
                    }
                } else {
                    if ($("#txtRechazo").val() == '') {
                        sw = 1;
                    } else {
                        sw = 0;
                        estado = "RECHAZADO";
                    }
                }
                if (sw == 1) {
                    fnMensaje("warning", "Ingrese Motivo de Rechazo");
                } else {
                    $.ajax({
                        type: "GET",
                        //contentType: "application/json; charset=utf-8",
                        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                        data: { "action": "actualizarGrupoInvestigador", "param1": reg, "param2": tip, "param3": $("#hdUser").val(), "param4": $("#txtRechazo").val() },
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            //document.execCommand('ClearAuthenticationCache');
                            RecargarAvisos();
                            $('div#mdMensajeG').modal('hide');
                            $('div#mdEdicionG').modal('hide');
                            $('div#mdMensaje').modal('hide');
                            fnMensaje(data[0].Status, data[0].Message);
                            if (data[0].Status == "success") {
                                //alert("E");
                                EnviarEmail(reg, 'GRU', estado, $("#txtRechazo").val());
                            }

                        },
                        error: function(result) {
                            //alert(result);
                            fnMensaje("warning", result);
                            //console.log(result);

                        }
                    });

                    return false;
                }
            } else {
                window.location.href = rpta;
            }

        }

        function CambiarEstadoInvestigador1(reg, tip) {
            //alert(reg + "-" + tip);
            rpta = fnvalidaSession();
            if (rpta == true) {

                var estado = "";
                if (tip == "A") {
                    estado = "APROBACIÓN";
                } else {
                    if (tip == "R") {
                        estado = "RECHAZADO";
                    }
                }
                $.ajax({
                    type: "GET",
                    //contentType: "application/json; charset=utf-8",
                    url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                    data: { "action": "actualizarInvestigador", "param1": reg, "param2": tip, "param3": $("#hdUser").val() },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //document.execCommand('ClearAuthenticationCache');

                        ListaInvestigadoresEstado(0, 'ES', 'X');

                        fnDestroyDataTableDetalle('tInvestigador');
                        $('#tbInvestigador').html('');

                        ListaInvestigadoresEstado(0, 'ES', 'T');

                        fnMensaje("success", data[0].Message);

                        if (data[0].Status == "success") {
                            EnviarEmail(reg, 'INV', estado, "");
                        }


                    },
                    error: function(result) {
                        console.log(result);
                    }
                });
                //RecargarAvisos();
                $('div#mdEdicion').modal('hide');
                $('div#mdMensaje').modal('hide');

            } else {
                window.location.href = rpta;
            }
        }

        function fnBuscar(c) {
            var i;
            var j = -1;
            var l;
            l = aDataI.length;
            for (i = 0; i < l; i++) {
                if (aDataI[i].c_inv == c) {
                    j = i;
                    return j;
                }
            }
        }
        function fnBuscarG(c) {
            var i;
            var j = -1;
            var l;
            l = aDataGI.length;
            for (i = 0; i < l; i++) {
                if (aDataGI[i].c_gru == c) {
                    j = i;
                    return j;
                }
            }
        }


        function fnLineas() {
            var arr;
            $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarLineas" /></form>');
            $('#frmOpe').append('<input type="hidden" id="cpf" name="cpf" value="%" />');
            var form = $("#frmOpe").serializeArray();
            $("#frmOpe").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    var valor = $("#cboLinea").val();
                    tb = ""
                    tb = "<option value=''>-- Seleccione --</option>"
                    if (data.length > 0) {
                        for (i = 0; i < data.length; i++) {
                            if (valor == data[i].cod) {
                                tb += "<option value='" + data[i].cod + "' selected='selected'>" + data[i].nombre + "</option>";
                            } else {
                                tb += "<option value='" + data[i].cod + "'>" + data[i].nombre + "</option>";
                            }
                        }
                    }
                    $("#cboLinea").html(tb);
                },
                error: function(result) {
                    //console.log(result)
                    arr = result;
                }
            });

            return arr;
        }

        
    </script>

</head>
<body>
    <input type="hidden" id="hdUser" name="hdUser" value="" runat="server" />
    <input type="hidden" id="hdCantInvRev" name="hdCantInvRev" value="" runat="server" />
    <input type="hidden" id="hdReg" name="hdReg" value="" runat="server" />
    <input type="hidden" id="hdTipo" name="hdTipo" value="" runat="server" />
    <input type="hidden" id="hdCantRegInvRev" name="hdCantRegInvRev" value="" runat="server" />
    <input type="hidden" id="hdRegG" name="hdRegG" value="" runat="server" />
    <input type="hidden" id="hdCtf" name="hdCtf" value="" runat="server" />
    <input type="hidden" id="hdCod" name="hdCod" value="" runat="server" />
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
                                                de Colaboradores y Grupos de Investigaci&oacute;n</span>
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
                                    Revisi&oacute;n de Colaboradores y Grupos de Investigación
                                </h3>
                            </div>
                            <div class="panel-body">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div role="tabpanel" style="text-align: center;">
                                                <ul class="nav nav-tabs piluku-tabs" role="tablist">
                                                    <li role="presentationINV" class="active" id="Investigador" runat="server" style="width: 49%;">
                                                        <a href="#Investigador_tab" aria-controls="home" role="tab" data-toggle="tab">
                                                            <div id="labelrps">
                                                                Colaboradores
                                                            </div>
                                                        </a></li>
                                                    <li role="presentationINV" id="GrupoInvestigador" runat="server" style="width: 49%;">
                                                        <a href="#GrupoInvestigador_tab" aria-controls="profile" role="tab" data-toggle="tab">
                                                            <div id="labelrpsG">
                                                                Grupos de Investigación</div>
                                                        </a></li>
                                                </ul>
                                                <div class="tab-content piluku-tab-content">
                                                    <div role="tabpanel" class="tab-pane active" id="Investigador_tab" runat="server">
                                                        <div class='table-responsive'>
                                                            <div class='panel-body'>
                                                                <div class='table-responsive'>
                                                                    <!--Default Form-->
                                                                    <div id="tBonos_wrapper" class="dataTables_wrapper" role="grid">
                                                                        <table id="tInvestigador" name="tInvestigador" class="display dataTable" width="100%">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                                        N°
                                                                                    </td>
                                                                                    <td width="40%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                                        Colaborador
                                                                                    </td>
                                                                                    <td width="15%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                                        Tipo
                                                                                    </td>
                                                                                    <td width="10%" style="font-weight: bold; width: 169px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                        Estado
                                                                                    </td>
                                                                                    <td width="15%" style="font-weight: bold; width: 111px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                                            <tbody id="tbInvestigador">
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <!--Default Form-->
                                                                </div>
                                                            </div>
                                                            <br />
                                                        </div>
                                                    </div>
                                                    <div role="tabpanel" class="tab-pane " id="GrupoInvestigador_tab" runat="server">
                                                        <div class='table-responsive'>
                                                            <div class='panel-body'>
                                                                <div class='table-responsive'>
                                                                    <!--Default Form-->
                                                                    <div id="Div1" class="dataTables_wrapper" role="grid">
                                                                        <table id="tGrupoInvestigador" name="tGrupoInvestigador" class="display dataTable"
                                                                            width="100%">
                                                                            <thead>
                                                                                <tr role="row">
                                                                                    <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                                        N°
                                                                                    </td>
                                                                                    <td width="30%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                                        Grupo
                                                                                    </td>
                                                                                    <td width="30%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                                        Area
                                                                                    </td>
                                                                                    <td width="10%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                        Estado
                                                                                    </td>
                                                                                    <td width="15%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                                        tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                                            <tbody id="tbGrupoInvestigador">
                                                                            </tbody>
                                                                        </table>
                                                                    </div>
                                                                    <!--Default Form-->
                                                                </div>
                                                            </div>
                                                            <br />
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
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="modalRegI">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabel3">
                                        Investigador</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="frmRegistroG" name="frmRegistroG" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div id="DivObservacionesInv">
                                    </div>
                                    <div class="row" id="Evaluacion1" style="text-align: right;">
                                        <button type="button" class="btn btn-success" id="btnAprobar">
                                            Aprobar</button>
                                        <button type="button" id="btnObservarInv" class="btn btn-warning">
                                            Observar</button>
                                        <button type="button" id="btnRechazar" class="btn btn-red">
                                            Rechazar</button>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtNombre">
                                                Nombre:</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtNombre" name="txtNombre" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtDNI">
                                                DNI:</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtDNI" name="txtDNI" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtTrabajador">
                                                Trabajador:</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtTrabajador" name="txtTrabajador" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtEspecialidad">
                                                Especialidad:</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtEspecialidad" name="txtEspecialidad" class="form-control"
                                                    readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtURLDina">
                                                Linea de Investigación:</label>
                                            <div class="col-sm-8">
                                                <select id="cboLinea" runat="server" class="form-control" disabled="disabled">
                                                    <option value="">-- Seleccione --</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-3 col-md-3 control-label ">
                                            Identificadores</label>
                                        <label class="col-sm-2 col-md-2 control-label ">
                                            RENACYT(REGINA)</label>
                                        <div class="col-sm-1 col-md-1">
                                            <input type="checkbox" id="chkREGINA" name="chkREGINA" value="0" data-validation="checkbox_group"
                                                data-validation-qty="1-2" runat="server">
                                        </div>
                                        <label class="col-sm-2 col-md-2 control-label ">
                                            CTI VITAE(DINA)</label>
                                        <div class="col-sm-1 col-md-1">
                                            <input type="checkbox" id="chkDINA" name="chkDINA" value="0" data-validation="checkbox_group"
                                                data-validation-qty="1-2" runat="server">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtRenacyt">
                                                Código Renacyt(Regina):</label>
                                            <div class="col-sm-3">
                                                <input type="text" id="txtRenacyt" name="txtRenacyt" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtURLDina">
                                                URL CTI VITAE(DINA):</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtURLDina" name="txtURLDina" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtOrcid">
                                                ORCID:</label>
                                            <div class="col-sm-8">
                                                <input type="text" id="txtOrcid" name="txtOrcid" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <!--<button type="button" id="btnA" class="btn btn-success" onclick="fnGuardarProyecto();">
                                            Aprobar</button>-->
                                        <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal fade" id="mdMensaje" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 0;">
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
                <div class="modal fade" id="mdMensajeG" tabindex="5" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true" data-backdrop="static" data-keyboard="false" style="z-index: 5;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                </button>
                                <h4 class="modal-title" style="color: White">
                                    <div id="divTitleG">
                                    </div>
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12" id="divMensajeG">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" id="divMotivoRechazo">
                                        <label class="col-sm-2 control-label" for="txtRechazo">
                                            Motivo:</label>
                                        <div class="col-sm-10">
                                            <textarea id="txtRechazo" name="txtRechazo" cols="20" rows="3" style="width: 100%"></textarea>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <center>
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-primary" id="btnAceptarG">
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
                    <div class="modal fade" id="mdEdicionG" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="modalRegG">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="myModalLabelG">
                                        Grupo de Investigación</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="Form1" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div id="DivObservaciones">
                                    </div>
                                    <div class="row" id="Div2" style="text-align: right;">
                                        <button type="button" class="btn btn-primary" id="btnReactivar">
                                            Reactivar</button>
                                        <button type="button" class="btn btn-success" id="btnAprobarG">
                                            Aprobar</button>
                                        <button type="button" id="btnObservar" class="btn btn-warning">
                                            Observar</button>
                                        <button type="button" id="btnRechazarG" class="btn btn-red">
                                            Rechazar</button>
                                    </div>
                                    <div class="row">
                                        <label class="col-md-2 control-label ">
                                            Nombre Grupo</label>
                                        <div class="col-md-7">
                                            <input name="txtGrupo" type="text" id="txtGrupo" value="" class="form-control" runat="server"
                                                placeholder="Digitar nombre de grupo" readonly="true" />
                                        </div>
                                    </div>
                                    <!--<div class="row">
                                        <label class="col-md-2 control-label ">
                                            Tipo</label>
                                        <ul class="list-inline checkboxes-radio">
                                            <li class="ms-hover">
									            <input type="radio" name="active" id="c6" checked="" readonly=true>
									            <label for="c6"><span></span>Unidisciplinario</label>
								            </li>
								            <li class="ms-hover">
									            <input type="radio" name="active" id="c5" readonly=true>
									            <label for="c5"><span></span>Multidisciplinario</label>
								            </li>
							            </ul>
                                    </div>-->
                                    <div class="row">
                                        <label class="col-md-2 control-label ">
                                            Líneas USAT</label>
                                        <div class="col-md-7">
                                            <select name="cboLineasUSAT" class="form-control" id="cboLineasUSAT" readonly="true">
                                            </select>
                                        </div>
                                    </div>
                                    <div id="divLineasOCDE">
                                        <div class="row">
                                            <label class="col-md-2 control-label ">
                                                &Aacute;rea Tem&aacute;tica</label>
                                            <div class="col-md-7">
                                                <select name="cboArea" class="form-control" id="cboArea" readonly="true">
                                                </select>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-2 control-label ">
                                                Sub &Aacute;rea</label>
                                            <div class="col-md-7">
                                                <select name="cboSubArea" class="form-control" id="cboSubArea" readonly="true">
                                                    <option value="0" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label class="col-md-2 control-label ">
                                                Disciplina</label>
                                            <div class="col-md-7">
                                                <select name="cboDisciplina" class="form-control" id="cboDisciplina" readonly="true">
                                                    <option value="0" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">
                                                Aplicación:</label>
                                            <div class="col-md-3">
                                                <select id="cboRegion" name="cboRegion" class="form-control" readonly="true">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                            <label class="col-md-1 control-label">
                                                Provincia:</label>
                                            <div class="col-md-3">
                                                <select id="cboProvincia" name="cboProvincia" class="form-control" readonly="true">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">
                                                Distrito:</label>
                                            <div class="col-md-3">
                                                <select id="cboDistrito" name="cboDistrito" class="form-control" readonly="true">
                                                    <option value="" selected="">-- Seleccione -- </option>
                                                </select>
                                            </div>
                                            <label class="col-md-1 control-label">
                                                Lugar:</label>
                                            <div class="col-md-3">
                                                <input type="text" id="txtLugar" name="txtLugar" class="form-control" readonly="true" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">
                                                Plan de Acción(PDF)</label>
                                            <div class="col-sm-7">
                                                <div id="propuesta">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="table-responsive" style="font-size: 10px; font-weight: 300; line-height: 18px;">
                                                <div id="Div4" class="dataTables_wrapper" role="grid">
                                                    <table id="Table1" name="tbGrupInvestigador" class="display dataTable" width="100%">
                                                        <thead>
                                                            <tr role="row">
                                                                <td width="5%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                    NRO
                                                                </td>
                                                                <td width="60%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                    Colaborador
                                                                </td>
                                                                <td width="10%" style="font-weight: bold; width: 54px; text-align: center" class="sorting_asc"
                                                                    tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N°: activate to sort column descending">
                                                                    Coord.
                                                                </td>
                                                                <td width="10%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                    tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                    DNI
                                                                </td>
                                                                <!--
                                                            <td width="5%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Dedicaci&oacute;n(h/s)
                                                            </td>
                                                            <td width="5%" style="font-weight: bold; width: 89px; text-align: center" class="sorting"
                                                                tabindex="0" rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Rol Investigador
                                                            </td>
                                                            -->
                                                            </tr>
                                                        </thead>
                                                        <tfoot>
                                                            <tr>
                                                                <th colspan="4" rowspan="1">
                                                                </th>
                                                            </tr>
                                                        </tfoot>
                                                        <tbody id="pGrupInvestigador">
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <button type="button" class="btn btn-danger" id="Button3" data-dismiss="modal">
                                            Cancelar</button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="modal fade" id="mdObservarInv" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog modal-lg" id="Div6">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H1">
                                        Observar Colaborador</h4>
                                </div>
                                <div class="modal-body">
                                    <form id="Form2" name="frmObservar" enctype="multipart/form-data" class="form-horizontal"
                                    method="post" onsubmit="return false;" action="#">
                                    <div class="row">
                                        <div class="form-group">
                                            <label class="col-sm-3 control-label" for="txtDescripcionObservacionInv">
                                                Observación:</label>
                                            <div class="col-sm-7">
                                                <textarea id="txtDescripcionObservacionInv" name="txtDescripcionObservacionInv" cols="20"
                                                    rows="3" style="width: 100%"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    </form>
                                </div>
                                <div class="modal-footer">
                                    <div id="DivGuardar">
                                        <center>
                                            <button type="button" class="btn btn-primary" id="btnGuardarObservacionInv">
                                                Guardar</button>
                                            <button type="button" class="btn btn-danger" id="btnCancelarObsInv" data-dismiss="modal">
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
                <div class="row">
                    <div class="modal fade" id="mdObservar" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
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
                                    <center>
                                        <button type="button" class="btn btn-primary" id="btnGuardarObservacion">
                                            Guardar</button>
                                        <button type="button" class="btn btn-danger" id="btnCancelarObs" data-dismiss="modal">
                                            Cancelar</button>
                                    </center>
                                </div>
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
</body>
</html>
