<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPostularConvocatoriaExterna.aspx.vb"
    Inherits="GestionInvestigacion_FrmPostularConvocatoriaExterna" %>

<!DOCTYPE html>
<html>
<head>
    <title>Postular a Convocatoria Externa</title>
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

    <script type="text/javascript" src='../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../assets/js/select2.js'></script>

    <script src="js/_General.js" type="text/javascript"></script>

    <script type="text/javascript">
        var equipo = [];
        var codper = "";
        var nombre = "";
        var codinv = 0;
        var urldina = "";
        $(document).ready(function() {
            var dt = fnCreateDataTableBasic('tConcursos', 0, 'asc', 100);
            var dt2 = fnCreateDataTableBasic('tPostulacion', 0, 'asc', 100);
            var dt1 = fnCreateDataTableBasic('tGrupo', 0, 'asc', 10);

            ope = fnOperacion(1);
            rpta = fnvalidaSession();
            if (rpta == true) {
                fnListarConcurso();
                fnAutoCPersonal();
            } else {
                window.location.href = rpta
            }

            $("#btnRegresar").click(function() {
                $("#VerConcurso").attr("style", "display:none");
                $("#ListaConcursos").attr("style", "display:block");
                $("#PanelBusqueda").attr("style", "display:block");
            })

            $("#btnPostular").click(function() {
                //        alert($("#cbotipo").val());
                fnLoading(true);
                fnLimpiarPostulacion();

                if ($("#cbotipo option:selected").text() == "INDIVIDUAL") {
                    //$("#cboGrupo").val("");
                    $("#rbGrupo").attr("style", "display:none")
                    $("#rbIndividual").attr("style", "display:block")
                    $("#rbtipoparticipante1").prop("checked", true)
                    $("#rbtipoparticipante2").prop("checked", false)
                    $("#rbtipoparticipante3").prop("checked", false)
                    $("#divalumno").attr("style", "display:none")
                    //fnListarInvestigadorGrupos('INV');
                }

                if ($("#cbotipo option:selected").text() == "UNIDISCIPLINARIO") {
                    $("#rbIndividual").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:block")
                    $("#rbGrupoM").attr("style", "display:none")
                    $("#rbtipoparticipante2").prop("checked", true)
                    $("#rbtipoparticipante1").prop("checked", false)
                    $("#rbtipoparticipante3").prop("checked", false)
                    $("#divalumno").attr("style", "display:none")
                    fnDestroyDataTableDetalle('tGrupo');
                    $('#tbGrupo').html('');
                    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
                    //fnListarInvestigadorGrupos('IU');
                    $("#rbIndividual").removeAttr("disabled")
                }
                if ($("#cbotipo option:selected").text() == "GRUPAL") {
                    $("#rbIndividual").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:none")
                    $("#rbGrupoM").attr("style", "display:block")
                    $("#rbtipoparticipante3").prop("checked", true)
                    $("#rbtipoparticipante1").prop("checked", false)
                    $("#rbtipoparticipante2").prop("checked", false)
                    fnDestroyDataTableDetalle('tGrupo');
                    $('#tbGrupo').html('');
                    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
                    //fnListarInvestigadorGrupos('IM');
                    //$("#divalumno").attr("style", "display:block")
                    $("#AgregarParticipante").attr("style", "display:block");
                    $("#txtPersonal").val("");
                    $("#txtPersonal").focus();
                    codper = ""
                    nombre = ""
                    codinv = 0
                    urldina = ""
                    $("#rbtipoparticipante3").removeAttr("disabled")
                }
                if ($("#cbotipo option:selected").text() == "INDIVIDUAL/GRUPAL") {
                    $("#rbIndividual").attr("style", "display:block")
                    $("#rbGrupo").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:none")
                    $("#rbGrupoM").attr("style", "display:block")
                    $("#rbtipoparticipante3").prop("checked", false)
                    $("#rbtipoparticipante1").prop("checked", true)
                    $("#rbtipoparticipante2").prop("checked", false)
                    fnDestroyDataTableDetalle('tGrupo');
                    $('#tbGrupo').html('');
                    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
                    //fnListarInvestigadorGrupos('INV');
                    //fnListarInvestigadorGrupos('IM');
                    $("#divalumno").attr("style", "display:block")
                    $("#rbIndividual").removeAttr("disabled")
                    $("#rbtipoparticipante3").removeAttr("disabled")
                }
                cDatospersonal();
                fnLoading(false);
                $("#mdRegistro").modal("show");
            })

            /*
            $("#cboGrupo").change(function() {
            equipo = [];
            if ($("#cboGrupo").val() == "") {
            fnDestroyDataTableDetalle('tGrupo');
            $('#tbGrupo').html('');
            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
            } else {

                    fnListarInvestigadorGrupos('GRU');
            }
            })
            
            $("#rbtipoparticipante1").click(function() {
            if (this.checked) {
            equipo = [];
            fnListarInvestigadorGrupos('INV');
            }

            })
            */

            $("#rbtipoparticipante3").change(function() {
                if (this.checked) {
                    equipo = [];
                    $("#AgregarParticipante").attr("style", "display:block");
                    cDatospersonal()
                    $("#txtPersonal").val("");
                    $("#txtPersonal").focus();
                    codper = ""
                    nombre = ""
                    codinv = 0
                    urldina = ""

                }
            })

            $("#rbtipoparticipante1").change(function() {
                if (this.checked) {
                    equipo = [];
                    $("#AgregarParticipante").attr("style", "display:none");
                    cDatospersonal()
                }
            })
            $("#btnAgregarParticipante").click(function() {
                fnAgregarParticipante();
                $("#txtPersonal").val("");
                codper = ""
                nombre = ""
                codinv = 0
                urldina = ""
            })
        });

        function cDatospersonal() {
            var data = fnPersonal('-1', '');
            var n = data.length;
            var tb = "";

            for (i = 0; i < n; i++) {
                tb += '<tr>';
                tb += '<td style="text-align:center;width:5%">' + (i + 1) + "" + '</td>';
                tb += '<td style="width:50%">' + data[i].nombre + '</td>';
                tb += '<td style="width:40%">' + data[i].dina + '</td>';
                tb += '<td style="width:5%"></td>';
                //tb += '<td style="width:28%"><select id="cboRol' + i + '" name="cboRol' + i + '" class="form-control" style="font-size:13px;" onchange="fnCambiaRol(' + i + ')" >' + str + '</select></td>';
                //tb += '<td style="width:4%"><input type="text" id="txtdedicacion' + i + '" name="txtdedicacion' + i + '" class="form-control" style="text-align:right" value="" /></td>';
                tb += '</tr>';

                equipo.push({
                    cod_inv: data[i].inv,
                    cod_per: data[i].cod,
                    cod_alu: 0,
                    nom_inv: data[i].nombre,
                    dina: data[i].dina,
                    cod_rol: 0,
                    dedicacion: 0,
                    estado:1
                });
            }
            fnDestroyDataTableDetalle('tGrupo');
            $('#tbGrupo').html(tb);
            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
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


                        if (data[0].cerrado == '1') {
                            $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) el Plazo para Postular al Concurso Culminó</label>')
                            $("#btnPostular").attr("style", "display:none");
                        } else {
                            if (data[0].iniciado == '0') {
                                $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) Ud. Podra postular a partir de la Fecha de Inicio del Concurso : ' + data[0].fecini + '</label>')

                            } else {
                                $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) Abstenerse de Postular, si no cuenta con los Requerimientos mínimos establecidos en las Bases del Concurso</label>')
                                $("#btnPostular").attr("style", "display:inline-block");
                            }

                        }
                        $("#VerConcurso").attr("style", "display:block");
                        $("#PanelBusqueda").attr("style", "display:none");
                        $("#ListaConcursos").attr("style", "display:none");
                        fnListarPostulacion(data[0].cod, data[0].cerrado);
                        if (data[0].iniciado == '0') {
                            $("#btnPostular").attr("style", "display:none");
                        }
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

        function fnCancelar() {
            $("#mdRegistro").modal("hide");
            //fnMensajeConfirmarEliminar('top', '¿Desea Salir Sin Guardar Cambios.?', 'fnCerrarModal', '');
        }

        function fnDownload(id_ar) {
            window.open("DescargarArchivo.aspx?Id=" + id_ar);
        }


        function SubirArchivo(cod, tipo) {
            fnLoading(true)
            var flag = false;
            try {

                var data = new FormData();
                data.append("action", ope.Up);
                data.append("codigo", cod);
                data.append("tipo", tipo);

                if (tipo == "PROPUESTA") {
                    var files = $("#file_producto").get(0).files;
                    if (files.length > 0) {
                        data.append("ArchivoASubir", files[0]);
                    }
                }
                //console.log(data);
                if (files.length > 0) {
                    // fnMensaje('primary', 'Subiendo Archivo');
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                        data: data,
                        dataType: "json",
                        cache: false,
                        contentType: false,
                        processData: false,
                        async: false,
                        success: function(data) {
                            flag = true;
                            console.log('ARCHIVO SUBIDO');
                            //console.log(data);

                        },
                        error: function(result) {
                            //console.log('falseee');
                            flag = false;
                            //console.log(result);
                        }
                    });
                }

                return flag;
                fnLoading(false);
            }
            catch (err) {
                return false;
                fnLoading(false);
            }
            fnLoading(false);
        }


        function fnGuardarPostulacion() {

            $("#DivGuardar").attr("style", "display:none");
            $("#MensajeGuardar").attr("style", "display:block");
            $("#MensajeGuardar").html('<b>Guardando Postulación...</b>');
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                $("form#frmRegistro input[id=ctf]").remove();
                $("form#frmRegistro input[id=action]").remove();
                $('form#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.rpe + '" />');
                $('form#frmRegistro').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=ctf]").remove();
                $("form#frmRegistro input[id=action]").remove();
                //console.log(form);

                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    //                async: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            fnGuardarEquipo(data[0].cod)

                            if ($("#file_producto").val() != "") {
                                //console.log("INGRESA ARCHIVO INFORME");
                                SubirArchivo(data[0].cod, "PROPUESTA");
                            }

                            fnListarPostulacion($("#hdcod").val(), 0)// codigo concurso
                            $("#mdRegistro").modal("hide");

                            fnLoading(false);
                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');
                        } else {
                            fnMensaje("error", data[0].msje)
                            $("#DivGuardar").attr("style", "display:block");
                            $("#MensajeGuardar").attr("style", "display:none");
                            $("#MensajeGuardar").html('');
                        }
                        fnLoading(false);
                    },
                    error: function(result) {
                        fnMensaje("warning", result)
                        $("#DivGuardar").attr("style", "display:block");
                        $("#MensajeGuardar").attr("style", "display:none");
                        $("#MensajeGuardar").html('');
                    }
                });
                fnLoading(false);
            } else {
                window.location.href = rpta
            }
        }



        function fnValidarPostulacion() {
            if (fnValidarPostulacionTab2() == false) {
                $("#tab2").trigger("click");
                return false;
            }
            if (fnValidarPostulacionTab4() == false) {
                $("#tab4").trigger("click");
                return false;

            }

            return true;
        }





        function fnConfirmar() {
            if (fnValidarPostulacion() == true) {
                fnMensajeConfirmarEliminar('top', '¿Desea Registrar Postulación, una vez Guardado no se podran Editar los Datos de Postulación.? ', 'fnGuardarPostulacion');
            }
        }



        function fnListarPostulacion(codigo_con, cerrado) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lstex + '" />');
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
                                tb += '<td style="width:30%">' + data[i].personal + '</td>';
                                tb += '<td style="width:50%">' + data[i].titulo + '</td>';
                                tb += '<td style="text-align:center;width:10%">' + data[i].fechareg + '</td>';
                                //                                tb += '<td style="text-align:center;width:20%">' + data[i].des_etapa + '</td>';
                                tb += '<td style="text-align:center;width:5%">';
                                tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnVerPostulacion(\'' + data[i].cod + '\')" title="Ver" ><i class="ion-eye"></i></button>';
                                tb += '</td>';
                                tb += '</tr>';
                            }
                            $("#mensaje").html("");
                            $("#divPostulacion").removeAttr("style")
                            $("#divPostulacion").attr("style", "display:block")
                            $("#btnPostular").attr("style", "display:none");
                        } else {
                            if (cerrado == 1) {
                                if (filas == 0) {
                                    $("#divPostulacion").removeAttr("style")
                                    $("#divPostulacion").attr("style", "display:none")
                                } else {
                                    $("#divPostulacion").removeAttr("style")
                                    $("#divPostulacion").attr("style", "display:block")
                                }
                                $("#btnPostular").attr("style", "display:none");
                            } else {
                                $("#divPostulacion").removeAttr("style")
                                $("#divPostulacion").attr("style", "display:none")
                                $("#btnPostular").attr("style", "display:inline-block");
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



        function fnVerPostulacion(cod) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                fnLimpiarPostulacion();
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                $("form#frmbuscar input[id=hdcodPost]").remove();
                $('#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />'); // PARA VER
                $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
                $('form#frmbuscar').append('<input type="hidden" id="hdcodPost" name="hdcodPost" value="' + cod + '" />');
                var form = $("#frmbuscar").serializeArray();
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=ctf]").remove();
                $("form#frmbuscar input[id=hdcodPost]").remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    //async: false,
                    success: function(data) {
                        console.log(data);
                        //                $("#hdcodCon").val(data[0].cod)
                        fnDestroyDataTableDetalle('tGrupo');
                        $('#tbGrupo').html('');
                        fnResetDataTableBasic('tGrupo', 0, 'asc', 10);

                        $("#btnA").attr("style", "display:none");
                        $("#file_producto").attr('disabled','disabled');
                        /*if (data[0].inv != 0) {
                        //$("#cboGrupo").val("");
                        $("#rbGrupo").attr("style", "display:none")
                        $("#rbIndividual").attr("style", "display:block")
                        $("#rbtipoparticipante1").prop("checked", true)
                        $("#rbtipoparticipante2").prop("checked", false)
                        $("#rbtipoparticipante3").prop("checked", false)
                        $("#rbIndividual").attr("disabled", "disabled")
                        }

                        if (data[0].gru != 0) {
                        $("#rbIndividual").attr("style", "display:none")
                        $("#rbGrupo").attr("style", "display:block")
                        $("#rbGrupoU").attr("style", "display:none")
                        $("#rbGrupoM").attr("style", "display:block")
                        $("#rbtipoparticipante3").prop("checked", true)
                        $("#rbtipoparticipante1").prop("checked", false)
                        $("#rbtipoparticipante2").prop("checked", false)
                        //$("#cboGrupo").val(data[0].gru);
                        $("#cboGrupo").attr("style", "display:none");
                        $("#rbGrupo").attr("disabled", "disabled")
                        }*/

                        fnListarEquipo(cod)

                        if (data.length > 0) {
                            if (data[0].rutainforme != "") {
                                $("#producto").html("<a onclick='fnDownload(\"" + data[0].rutainforme + "\")' style='font-weight:bold'>Descargar Propuesta</a>")
                            } else {
                                $("#producto").html("");
                            }

                            $("#mdRegistro").modal("show");
                        }


                        fnLoading(false);
                    },
                    error: function(result) {
                        fnLoading(false)
                        console.log(result)
                    }
                });
            } else {
                window.location.href = rpta
            }
        }


        function fnLimpiarPostulacion() {
            $("#hdcodPos").val("0");
            $("#file_producto").val("");
            $('#producto').html("");
            $("#tab2").trigger("click");
            equipo = [];
            $("#btnA").attr("style", "display:inline-block")
            $("#file_producto").removeAttr('disabled');
        }

        function fnValidarPostulacionTab2() {

            var nro = equipo.length
            if (nro == 0) {
                fnMensaje("warning", "Para Postular debe contar con al menos un Participante.");
                return false;
            }
            if (nro == 1 && $("#rbtipoparticipante3").is(':checked')) {
                fnMensaje("warning", "El Equipo de Postulación debe tener al menos Dos Integrantes.");
                return false;
            }
            return true;
        }



        function fnValidarPostulacionTab4() {
            if ($("#file_producto").val() == '') {
                fnMensaje("warning", 'Adjuntar Archivo de Propuesta en Formato de PDF');
                return false;
            }
            if ($("#file_producto").val() != '') {
                archivo = $("#file_producto").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".pdf");
                //recupero la extensión de este nombre de archivo
                // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
                archivo = archivo.substring(archivo.length - 5, archivo.length);
                //despues del punto de nombre recortado
                extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
                //compruebo si la extensión está entre las permitidas 
                permitida = false;
                for (var i = 0; i < extensiones_permitidas.length; i++) {
                    if (extensiones_permitidas[i] == extension) {
                        permitida = true;
                        break;
                    }
                }
                if (permitida == false) {
                    //            fnMensaje("warning", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
                    fnMensaje("warning", "Solo puede Adjuntar Archivo de Propuesta en Formato de PDF");
                    return false;
                }
            }
            return true;
        }


        function fnGuardarEquipo(cod) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
                var cont = equipo.length
                for (i = 0; i < cont; i++) {
                    equipo[i].dedicacion = 0
                }
                console.log(equipo);
                var form = JSON.stringify(equipo);
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                    data: { "hdcodP": cod, "action": ope.req, "array": form },
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function(data) {
                        //console.log("OK");
                    },
                    error: function(result) {
                        //            //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
                fnLoading(false);
            } else {
                window.location.href = rpta
            }
        }


        function fnListarEquipo(cod) {
            rpta = fnvalidaSession()
            if (rpta == true) {
                fnLoading(true)
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
                        equipo = [];
                        // Si solo es una fila quiere decir que postulacion es individual
                        $("#AgregarParticipante").attr("style","display:none")
                        if (filas == 1) {
                            $("#rbGrupo").attr("style", "display:none")
                            $("#rbIndividual").attr("style", "display:block")
                            $("#rbtipoparticipante1").prop("checked", true)
                            $("#rbtipoparticipante2").prop("checked", false)
                            $("#rbtipoparticipante3").prop("checked", false)
                            $("#rbIndividual").attr("disabled", "disabled")
                        } else {
                            $("#rbIndividual").attr("style", "display:none")
                            $("#rbGrupo").attr("style", "display:block")
                            $("#rbGrupoU").attr("style", "display:none")
                            $("#rbGrupoM").attr("style", "display:block")
                            $("#rbtipoparticipante3").prop("checked", true)
                            $("#rbtipoparticipante1").prop("checked", false)
                            $("#rbtipoparticipante2").prop("checked", false)
                            $("#rbtipoparticipante3").attr("disabled", "disabled")
                        }

                        if (filas > 0) {
                            for (i = 0; i < filas; i++) {
                                tb += '<tr>';
                                tb += '<td style="text-align:center;width:3%">' + (i + 1) + '</td>';
                                tb += '<td style="width:37%">' + data[i].nombre + '</td>';
                                tb += '<td style="width:28%">' + data[i].dina + '</td>';
                                //                                tb += '<td style="width:28%">' + data[i].rol + '</td>';
                                //                                tb += '<td style="width:4%">' + data[i].dedicacion + '</td>';
                                tb += '<td style="width:4%"></td>';
                                tb += '</tr>';
                            }
                        }
                        //console.log(objetivos);
                        fnDestroyDataTableDetalle('tGrupo');
                        $('#tbGrupo').html(tb);
                        fnResetDataTableBasic('tGrupo', 0, 'asc', 10);

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

        function fnAutoCPersonal() {
            jsonString = fnPersonal(1, '-1');
            $('#txtPersonal').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.nombre;
                }),
                select: function(event, ui) {
                    var selectecItem = jsonString.filter(function(value) {
                        return value.nombre == ui.item.value;
                    });
                    codper = selectecItem[0].cod;
                    nombre = selectecItem[0].nombre;
                    urldina = selectecItem[0].dina;
                    codinv = selectecItem[0].inv;
                    $('#PanelEvento').hide("fade");
                },
                minLength: 1,
                delay: 500
            });

            $('#txtPersonal').keyup(function() {
                var l = parseInt($(this).val().length);
                if (l == 0) {
                    codper = "";
                    nombre = "";
                }
            });
        }


        function fnAgregarParticipante() {
            var value;
            var tb = '';
            var rowCount = $('#tbGrupo tr').length;
            var repite = false;
            //console.log(equipo);
            if (fnValidarParticipante() == true) {
                //          $.grep(detalles, function(e) { return e.item == id; });
                for (i = 0; i < equipo.length; i++) {
                    if (equipo[i].cod_per == codper) {
                        repite = true;
                    }
                }
                //console.log(repite);
                if (repite == false) {
                    $('#tbGrupo tr').each(function() {
                        value = $(this).find("td:first").html();

                    });
                    if (!($.isNumeric(value))) { rowCount = 0 }
                    var row = (rowCount + 1);
                    equipo.push({
                        cod_inv: codinv,
                        cod_per: codper,
                        cod_alu: 0,
                        nom_inv: nombre,
                        dina: urldina,
                        cod_rol: 0,
                        dedicacion: 0,
                        estado:1
                    });
                    //console.log(detalles);
                    for (i = 0; i < equipo.length; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center;width:5%">' + (i + 1) + '</td>'
                        tb += '<td style="width:50%">' + equipo[i].nom_inv + '</td>';
                        tb += '<td style="width:40%">' + equipo[i].dina + '</td>';
                        tb += '<td style="text-align:center;width:5%">'
                        if (i > 0) {
                            tb += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarIntegrante(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                        }
                        tb += '</td>';
                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tGrupo');
                    $('#tbGrupo').html(tb);
                    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
                } else {
                    fnMensaje("warning", "El Participante ya ha sido ingresado")
                }

            }
        }

        function fnValidarParticipante() {
            if ($("#txtPersonal").val() == "" || codper == "") {
                fnMensaje("warning", "Debe Seleccionar un Personal Docente/Administrativo");
                return false;
            }
            return true;
        }


        function fnQuitarIntegrante(cod) {
            var tb = '';
            equipo.splice(cod - 1, 1);
            for (i = 0; i < equipo.length; i++) {
                tb += '<tr>';
                tb += '<td style="text-align:center;width:5%">' + (i + 1) + '</td>'
                tb += '<td style="width:50%">' + equipo[i].nom_inv + '</td>';
                tb += '<td style="width:40%">' + equipo[i].dina + '</td>';
                tb += '<td style="text-align:center;width:5%">'
                if (i > 0) {
                    tb += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarIntegrante(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                }
                tb += '</td>';
                tb += '</tr>';
            }
            fnDestroyDataTableDetalle('tGrupo');
            $('#tbGrupo').html(tb);
            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
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
                                            <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Postular
                                                a Convocatoria Externa</span>
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
                                                <label class="col-md-4 control-label ">
                                                    Titulo de Convocatoria</label>
                                                <div class="col-md-8">
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
                            <%--   <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-3 control-label" for="txttitulo">
                                        Título:</label>
                                    <div class="col-sm-8">
                                        <input type="text" id="txttitulo" name="txttitulo" class="form-control" disabled="disabled" />
                                    </div>
                                </div>
                            </div>--%>
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
                                        Ámbito:</label>
                                    <div class="col-md-3">
                                        <select id="cboAmbito" name="cboAmbito" class="form-control" disabled="disabled">
                                            <option value="">--Seleccione--</option>
                                            <option value="0">INTERNO</option>
                                            <option value="1">EXTERNO</option>
                                        </select>
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
                                                                Autor
                                                            </td>
                                                            <td style="font-weight: bold; width: 50%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Nombre: activate to sort column ascending">
                                                                Titulo
                                                            </td>
                                                            <td style="font-weight: bold; width: 10%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                Fecha Postulación
                                                            </td>
                                                            <%--           <td style="font-weight: bold; width: 20%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                Etapa
                                                            </td>--%>
                                                            <td style="font-weight: bold; width: 5%; text-align: center" class="sorting" tabindex="0"
                                                                rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">
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
                                                    <tbody id="tbPostulacion">
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group">
                                            <center>
                                                <button id="btnPostular" name="btnPostular" class="btn btn-success">
                                                    Postular</button>
                                                <button id="btnRegresar" name="btnRegresar" class="btn btn-danger">
                                                    Regresar</button>
                                            </center>
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
                                                    Registrar Postulación</h4>
                                            </div>
                                            <div class="modal-body">
                                                <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                                                method="post" onsubmit="return false;" action="#">
                                                <div role="tabpanel">
                                                    <!-- Nav tabs -->
                                                    <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist">
                                                        <li role="presentation" class=""><a href="#profiletabnb" aria-controls="profile"
                                                            role="tab" data-toggle="tab" aria-expanded="false" id="tab2">Datos Postulación</a></li>
                                                        <li role="presentation" class=""><a href="#settingstabnb" aria-controls="settings"
                                                            role="tab" data-toggle="tab" aria-expanded="false" id="tab4">Archivos de Propuesta</a></li>
                                                    </ul>
                                                    <!-- Tab panes -->
                                                    <div class="tab-content piluku-tab-content">
                                                        <div role="tabpanel" class="tab-pane active" id="profiletabnb">
                                                            <div class="row">
                                                                <input type="hidden" id="hdcodPos" name="hdcodPos" value="0" />
                                                                <input type="hidden" id="hdcodCon" name="hdcodCon" value="0" />
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">
                                                                        Participante(s):</label>
                                                                    <div class="col-md-2" id="rbIndividual">
                                                                        <input type="radio" name="rbtipo" id="rbtipoparticipante1" value="0" readonly="true">
                                                                        <label for="rbtipoparticipante1" style="color: Black; font-size: 13px">
                                                                            <span></span>&nbsp; Individual</label></div>
                                                                    <div id="rbGrupo">
                                                                        <div class="col-md-2" id="rbGrupoU">
                                                                            <input type="radio" name="rbtipo" id="rbtipoparticipante2" value="1">
                                                                            <label for="rbtipoparticipante2" style="color: Black; font-size: 13px" id="nomGrupo">
                                                                                <span></span>&nbsp; Unidisciplinario</label></div>
                                                                        <div class="col-md-3" id="rbGrupoM">
                                                                            <input type="radio" name="rbtipo" id="rbtipoparticipante3" value="2">
                                                                            <label for="rbtipoparticipante3" style="color: Black; font-size: 13px" id="Label1">
                                                                                <span></span>&nbsp; Grupal</label></div>
                                                                        <div class="col-md-5">
                                                                            <%--<select id="cboGrupo" name="cboGrupo" class="form-control">
                                                                                <option value="" selected="">-- Seleccione -- </option>
                                                                            </select>--%>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="AgregarParticipante" style="display: none">
                                                                <div class="form-group">
                                                                    <label class="col-sm-2 control-label">
                                                                        Docente/Administrativo:</label>
                                                                    <div class="col-md-6">
                                                                        <input type="text" id="txtPersonal" name="txtPersonal" class="form-control ui-autocomplete-input"
                                                                            autocomplete="off" />
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <button id="btnAgregarParticipante" class="btn btn-success">
                                                                            Agregar</button>
                                                                    </div>
                                                                </div>
                                                            </div>
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
                                                                                    <th style="font-weight: bold; text-align: center;" class="sorting" tabindex="0" rowspan="1"
                                                                                        colspan="1" aria-label="Fecha Inicio: activate to sort column ascending">
                                                                                        
                                                                                    </th>
                                                                                    <%--<th style="font-weight: bold; width: 28%; text-align: center;" class="sorting" tabindex="0"
                                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                        Rol
                                                                                    </th>
                                                                                    <th style="font-weight: bold; width: 4%; text-align: center;" class="sorting" tabindex="0"
                                                                                        rowspan="1" colspan="1" aria-label="Tipo de Estudio: activate to sort column ascending">
                                                                                        Dedicación
                                                                                    </th>--%>
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
                                                        </div>
                                                        <div role="tabpanel" class="tab-pane" id="settingstabnb">
                                                            <div class="row">
                                                                <div class="form-group">
                                                                    <label class="col-sm-3 control-label">
                                                                        Propuesta(PDF)</label>
                                                                    <div class="col-sm-8">
                                                                        <input type="file" id="file_producto" name="file_producto" />
                                                                        <div id="producto">
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </form>
                                            </div>
                                            <div class="modal-footer" id="Footer_Modal">
                                                <div id="DivGuardar">
                                                    <center>
                                                        <button type="button" id="btnA" class="btn btn-primary" onclick="fnConfirmar();">
                                                            Guardar</button>
                                                        <button type="button" id="btnCancelarReg" class="btn btn-danger" onclick="fnCancelar();">
                                                            Regresar</button>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
