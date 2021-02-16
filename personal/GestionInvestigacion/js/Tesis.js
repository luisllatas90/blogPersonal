var Autores = [];
var objetivos = [];
var Asesores = [];
var Jurado = [];
var JuradoInforme = [];
var tipoautor = "";
codalu = "";
coduniver = "";
nombre = "";
var codobj = "0";
$(document).ready(function() {
    var dt = fnCreateDataTableBasic('tAlumnos', 0, 'asc', 300);
    var dt = fnCreateDataTableBasic('tObjetivos', 1, 'desc', 300);
    var dt = fnCreateDataTableBasic('tAsesor', 0, 'asc', 300);
    var dt = fnCreateDataTableBasic('tObservacionesDocente', 0, 'asc', 300);
    //ope = fnOperacion(1);
    fnLoading(true);
    rpta = fnvalidaSession();
    if (rpta == true) {
        fnListarSemestre();
        fnListarTipoInvestigacion();
        cLineas();
        fnListarTipoParticipante(1, 'A', 'cboTipoAutor');
        fnListarTipoParticipante(1, 'S', 'cboRolAsesor');
        fnListarTipoParticipante(1, 'J', 'cboRolJurado');
        fnListarTipoParticipante(1, 'J', 'cboRolJuradoInforme');
        fnListarDepartamentoAcademico();
        //fnListarOCDE(0, 'AR');
        fnListarPersonalDepartamentoAcademico('%', 'cboAsesor');
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $("#cboSemestre").change(function() {
        $("#RegistroTabs").attr("style", "display:none");
        $("#Lista").attr("style", "display:block")
        fnDestroyDataTableDetalle('tAlumnos');
        $('#tbAlumnos').html("");
        fnResetDataTableBasic('tAlumnos', 0, 'asc', 300);
        fnListarCarrera();
        $("#cboCurso").html('<option value="">-- Seleccione --</option>');
    })

    $("#cboEtapa").change(function() {
        $("#RegistroTabs").attr("style", "display:none");
        $("#Lista").attr("style", "display:block")
        fnDestroyDataTableDetalle('tAlumnos');
        $('#tbAlumnos').html("");
        fnResetDataTableBasic('tAlumnos', 0, 'asc', 300);
        fnListarCarrera();
        $("#cboCurso").html('<option value="">-- Seleccione --</option>');
    })

    $("#cboCarrera").change(function() {
        $("#RegistroTabs").attr("style", "display:none");
        $("#Lista").attr("style", "display:block")
        fnDestroyDataTableDetalle('tAlumnos');
        $('#tbAlumnos').html("");
        fnResetDataTableBasic('tAlumnos', 0, 'asc', 300);
        fnListarCursos();
    })

    $("#cboCurso").change(function() {
        $("#RegistroTabs").attr("style", "display:none");
        $("#Lista").attr("style", "display:block")
        fnDestroyDataTableDetalle('tAlumnos');
        $('#tbAlumnos').html("");
        fnResetDataTableBasic('tAlumnos', 0, 'asc', 300);
    })

    $("#cboDepartamento").change(function() {
        fnListarPersonalDepartamentoAcademico($(this).val(), "cboDocenteAsesor");
    })

    $("#btnConsultar").click(function() {
        if ($("#cboEtapa").val() != "") {
            if ($("#cboCarrera").val() != "") {
                if ($("#cboCurso").val() != "") {
                    $("#RegistroTabs").attr("style", "display:none");
                    $("#Lista").attr("style", "display:block")
                    fnListarAlumnos();
                } else {
                    fnMensaje("error", "Debe seleccionar Curso/Grupo")
                }
            } else {
                fnMensaje("error", "Debe seleccionar Carrera Profesional")
            }
        } else {
            fnMensaje("error", "Debe seleccionar Etapa")
        }
    })


    $("#cbotipo").change(function() {
        if ($("#cbotipo").val() == 1) {

            $("#ActaAprobacion").attr("style", "display:block");
            $("#JuradoInforme").attr("style", "display:block");

        } else {
            $("#ActaAprobacion").attr("style", "display:none");
            $("#JuradoInforme").attr("style", "display:none");
        }
    });

    $("#btnObjetivos").click(function() {
        $("#txtobjetivo").val("");
        $("#cboTipoObjetivo").val("");
        $("#mdObjetivos").modal("show");
    })

    $("#btnAgregarObjetivo").click(function() {
        fnAgregarObjetivo();
    })

    $("#btnCancelarObj").click(function() {
        $("#mdObjetivos").modal("hide");
    })

    $("#btnAutor").click(function() {
        $("#txtAutor").val("");
        $("#cboTipoAutor").val("");
        $("#mdAutor").modal("show");
    })

    $("#btnAgregarAutor").click(function() {
        fnAgregarAutor();
    })

    $("#btnCancelarAutor").click(function() {
        $("#mdAutor").modal("hide");
    })

    $('#chkUsat').click(function() {
        $("#txtusat").val("");
        if ($(this).is(':checked')) {
            $("#txtusat").attr("style", "display:block");
        } else {
            $("#txtusat").attr("style", "display:none");
        }
    });

    $('#chkExterno').click(function() {
        $("#txtexterno").val("");
        if ($(this).is(':checked')) {
            $("#txtexterno").attr("style", "display:block");
        } else {
            $("#txtexterno").attr("style", "display:none");
        }
    });


    $("#btnAsignarAsesor").click(function() {
        fnLimpiarAsesor();
        $("#mdAsignarAsesor").modal("show");
    })

    $("#btnCancelarAsesor").click(function() {
        $("#mdAsignarAsesor").modal("hide");
    })

    $("#btnAgregarAsesor").click(function() {
        fnAgregarAsesor();
        fnLimpiarAsesor();
    })


    $("#btnAsignarJurado").click(function() {
        fnLimpiarJurado();
        $("#mdAsignarJurado").modal("show");
    })

    $("#btnCancelarJurado").click(function() {
        $("#mdAsignarJurado").modal("hide");
    })

    $("#cboDepartamentoJurado").change(function() {
        fnListarPersonalDepartamentoAcademico($(this).val(), "cboDocenteJurado");
    })

    $("#btnAgregarJurado").click(function() {
        fnAgregarJurado();
        fnLimpiarJurado();
    })


    $("#btnJuradoInforme").click(function() {
        fnLimpiarJuradoInforme();
        $("#mdAsignarJuradoInforme").modal("show");
    })

    $("#btnCancelarJuradoInforme").click(function() {
        $("#mdAsignarJuradoInforme").modal("hide");
    })

    $("#cboDepartamentoJuradoInforme").change(function() {
        fnListarPersonalDepartamentoAcademico($(this).val(), "cboDocenteJuradoInforme");
    })

    $("#btnAgregarJuradoInforme").click(function() {
        fnAgregarJuradoInforme();
        fnLimpiarJuradoInforme();
    })

    $("#btnGuardarTesis").click(function() {
        fnConfirmarGuardarTesis();
        //fnGuardarTesis();
    })

    $("#btnGuardarPreinforme").click(function() {
        fnConfirmarGuardarPreInforme();
        //fnGuardarPreinforme();
    })

    $("#btnGuardarInforme").click(function() {
        fnConfirmarGuardarInforme();
        //fnGuardarInforme();
    })


    $('#chkOCDE').click(function() {
        //$("#cboArea").val("0");
        fnListarAreaOCDE($("#cboLinea").val());
        $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
        $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
        if ($(this).is(':checked')) {
            $("#ocde").attr("style", "display:block");
        } else {
            $("#ocde").removeAttr("style");
            $("#ocde").attr("style", "display:none");
        }
    });

    $("#cboArea").change(function() {
        if ($("#cboArea").val() != 0) {
            fnListarOCDE($("#cboArea").val(), 'SA');
        } else {
            $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
        }
        $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
    })

    $("#cboSubArea").change(function() {
        if ($("#cboSubArea").val() != 0) {
            fnListarOCDE($("#cboSubArea").val(), 'DI');
        } else {
            $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
        }
    })

    $("#btnAgregarObservacion").click(function() {
        fnConfirmarRegistroObservacion();
    })

    $("#cboLinea").change(function() {
        fnListarAreaOCDE($("#cboLinea").val());
    })

});


var aDataR = [];
function fnConfirmarGuardarTesis() {
    if (fnValidarTesis() == true) {
        aDataR = {
            mensaje: '¿Desea guardar los cambios?'
        }
        fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnGuardarTesis');
    }
}

function fnConfirmarGuardarPreInforme() {
    if (fnValidarPreinforme() == true) {
        aDataR = {
            mensaje: '¿Desea guardar los cambios?'
        }
        fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnGuardarPreinforme');
    }
}

function fnConfirmarGuardarInforme() {
    if (fnValidarInforme() == true) {
        aDataR = {
            mensaje: '¿Desea guardar los cambios?'
        }
        fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnGuardarInforme');
    }
}


function fnListarSemestre() {
    //fnLoading(true)
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lSemestre" />');
    $('form#frm').append('<input type="hidden" id="tipo" name="tipo" value="TO" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    if (data[i].vig == 1) {
                        tb += '<option selected="selected" value="' + data[i].cod + '">' + data[i].des + '</option>';
                    } else {
                        tb += '<option value="' + data[i].cod + '">' + data[i].des + '</option>';
                    }
                }
                $("#cboSemestre").html(tb);
            }
            //fnLoading(false);
        },
        error: function(result) {
            //        fnMensaje("error", result)
            fnMensaje("error", "No se pudo cargar lista de semestres")
            //fnLoading(false);
        }
    });
    //    fnLoading(false);
}

function fnListarCarrera() {
    //fnLoading(true)
    $("#cboCarrera").html('<option value="">-- Seleccione --</option>');
    if ($("#cboSemestre").val() != '' && $("#cboEtapa").val() != '') {
        fnLoading(true)
        $('body').append('<form id="frm"></form>');
        $('form#frm').append('<input type="hidden" id="action" name="action" value="lCarrera" />');
        $('form#frm').append('<input type="hidden" id="cac" name="cac" value="' + $("#cboSemestre").val() + '" />');
        $('form#frm').append('<input type="hidden" id="etapa" name="etapa" value="' + $("#cboEtapa").val() + '" />');
        $('form#frm').append('<input type="hidden" id="id" name="id" value="' + ObtenerValorGET("id") + '" />');
        $('form#frm').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        var form = $("#frm").serializeArray();
        $("form#frm").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var filas = data.length;
                tb += '<option value="">-- Seleccione --</option>'
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<option value="' + data[i].cod + '">' + data[i].des + '</option>';
                    }
                    $("#cboCarrera").html(tb);
                }
                fnLoading(false);
            },
            error: function(result) {
                //        fnMensaje("error", result)
                fnMensaje("error", "No se pudo cargar lista de Carrera Profesional")
                fnLoading(false);
            }
        });
    } else {
        $("#cboCurso").html('<option value="">-- Seleccione --</option>');
    }
    //    fnLoading(false);
}

function fnListarTipoInvestigacion() {
    //fnLoading(true)
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lTipoInvestigacion" />');
    $('form#frm').append('<input type="hidden" id="tipo" name="tipo" value="C" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            tb += '<option value="" selected="selected">-- Seleccione --</option>';
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<option  value="' + data[i].cod + '">' + data[i].des + '</option>';
                }
            }
            $("#cbotipoInvestigacion").html(tb);
            //fnLoading(false);
        },
        error: function(result) {
            //fnMensaje("error", result)
            fnMensaje("error", "No se pudo cargar lista de tipos de investigación")
            //fnLoading(false);
        }
    });
    //    fnLoading(false);
}


function cLineas() {
    var arr = fnLineas('%');
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboLinea').html(str);
}

function fnListarDepartamentoAcademico() {
    //fnLoading(true)
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lDepartamentoAcademico" />');
    $('form#frm').append('<input type="hidden" id="tipo" name="tipo" value="1" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="" />');
    $('form#frm').append('<input type="hidden" id="param2" name="param2" value="" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            tb += '<option value="" selected="selected">-- Seleccione --</option>';
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<option  value="' + data[i].cod + '">' + data[i].des + '</option>';
                }
            }
            $("#cboDepartamento").html(tb);
            $("#cboDepartamentoJurado").html(tb);
            $("#cboDepartamentoJuradoInforme").html(tb);

            //fnLoading(false);
        },
        error: function(result) {
            //fnMensaje("error", result)
            fnMensaje("error", "No se pudo cargar lista de departamentos académicos")
            //fnLoading(false);
        }
    });
    //    fnLoading(false);
}

function fnListarPersonalDepartamentoAcademico(codigo_dac, ctrl) {
    //fnLoading(true)
    if (codigo_dac == "") {
        tb = '<option value="" selected="selected">-- Seleccione --</option>';
        $("#cboDocenteAsesor").html(tb);
    } else {
        $('body').append('<form id="frm"></form>');
        $('form#frm').append('<input type="hidden" id="action" name="action" value="lPersonalDepartamentoAcademico" />');
        $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + codigo_dac + '" />');
        var form = $("#frm").serializeArray();
        $("form#frm").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var filas = data.length;
                tb += '<option value="" selected="selected">-- Seleccione --</option>';
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<option  value="' + data[i].cod + '">' + data[i].des + '</option>';
                    }
                }
                $("#" + ctrl).html(tb);
                //fnLoading(false);
            },
            error: function(result) {
                //fnMensaje("error", result)
                //fnLoading(false);
            }
        });
        //    fnLoading(false);
    }
}


function fnListarTipoParticipante(tipo, param, control) {
    //fnLoading(true)
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lTipoParticipante" />');
    $('form#frm').append('<input type="hidden" id="tipo" name="tipo" value="' + tipo + '" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + param + '" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            tb += '<option value="" selected="selected">-- Seleccione --</option>';
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    if (param == "J" && control == "cboRolJurado") {
                        if (data[i].des.replace("JURADO-", "") == "PRESIDENTE") {
                            descripcion_jurado = 'JURADO 1';
                        }
                        if (data[i].des.replace("JURADO-", "") == "SECRETARIO") {
                            descripcion_jurado = 'JURADO 2';
                        }
                        if (data[i].des.replace("JURADO-", "") == "ASESOR") {
                            descripcion_jurado = 'JURADO 3';
                        }
                        if (data[i].des.replace("JURADO-", "") == "VOCAL") {
                            descripcion_jurado = 'JURADO 4';
                        }

                    } else {
                        descripcion_jurado = data[i].des
                    }

                    tb += '<option  value="' + data[i].cod + '">' + descripcion_jurado + '</option>';
                    if (tipo == 1 && param == 'A' && data[i].des == 'AUTOR') {
                        tipopart = data[i].cod;
                        despart = descripcion_jurado;
                    }
                }
            }
            $("#" + control).html(tb);

            var select = $('#cboRolJurado');
            select.html(select.find('option').sort(function(x, y) {
                // to change to descending order switch "<" for ">"
                return $(x).text() > $(y).text() ? 1 : -1;

            }));
            //fnLoading(false);
        },
        error: function(result) {
            //fnMensaje("error", result)
            fnMensaje("error", "No se pudo cargar lista de tipos de participante")
            //fnLoading(false);
        }
    });
    //    fnLoading(false);
}


function fnListarCursos() {

    if ($("#cboSemestre").val() != '' && $("#cboEtapa").val() != '' && $("#cboCarrera").val() != '') {
        fnLoading(true)
        $("form#frmbuscarAlumno input[id=action]").remove();
        $("form#frmbuscarAlumno input[id=id]").remove();
        $("form#frmbuscarAlumno input[id=ctf]").remove();
        $('form#frmbuscarAlumno').append('<input type="hidden" id="action" name="action" value="lCursosxDocente" />');
        $('form#frmbuscarAlumno').append('<input type="hidden" id="id" name="id" value="' + ObtenerValorGET("id") + '" />');
        $('form#frmbuscarAlumno').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        var form = $("#frmbuscarAlumno").serializeArray();
        $("form#frmbuscarAlumno input[id=action]").remove();
        $("form#frmbuscarAlumno input[id=id]").remove();
        $("form#frmbuscarAlumno input[id=ctf]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var filas = data.length;
                tb += '<option value="">-- Seleccione --</option>'
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        //                    if (data[i].vig == 1) {
                        //                        tb += '<option selected="selected" value="' + data[i].cod + '">' + data[i].des + '</option>';
                        //                    } else {
                        tb += '<option value="' + data[i].cod + '">' + data[i].des + '</option>';
                        //                    }
                    }
                }
                $("#cboCurso").html(tb);
                fnLoading(false);
            },
            error: function(result) {
                //fnMensaje("error", result)
                fnLoading(false);
            }
        });
    } else {
        $("#cboCurso").html('<option value="">-- Seleccione --</option>');
    }
    //    fnLoading(false);
}

function fnListarAlumnos() {
    if ($("#cboCurso").val() != '') {
        fnLoading(true)
        $("form#frmbuscarAlumno input[id=action]").remove();
        $('form#frmbuscarAlumno').append('<input type="hidden" id="action" name="action" value="lAlumnosxCurso" />');
        var form = $("#frmbuscarAlumno").serializeArray();
        $("form#frmbuscarAlumno input[id=action]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        //                        tb += '<td>' + data[i].nom + '</td>';
                        //                        tb += '<td style="text-align:center">' + data[i].grupo + '</td>';
                        tb += '<td style="text-align:center">' + data[i].coduniver + '</td>';
                        tb += '<td>' + data[i].alumno + '</td>';

                        var estilo = ""

                        $("#colActa").html("Nota Acta Sustentación");
                        if ($("#cboEtapa").val() == "P") {
                            if (data[i].notaP > 0) {
                                estilo = "style='font-weight:bold;color:green;text-align:center'";
                                tb += '<td ' + estilo + '>' + data[i].notaP + '</td>';
                            } else {
                                estilo = "style='text-align:center'"
                                tb += '<td ' + estilo + '>-</td>';
                            }
                        } else if ($("#cboEtapa").val() == "I") {
                            if (data[i].notaI > 0) {
                                estilo = "style='font-weight:bold;color:green;text-align:center'";
                                tb += '<td ' + estilo + '>' + data[i].notaI + '</td>';
                            } else {
                                estilo = "style='text-align:center'"
                                tb += '<td ' + estilo + '>-</td>';
                            }
                        } else {
                            tb += '<td style="text-align:center"></td>';
                            $("#colActa").html("");
                        }

                        $("#colAsesor").html("Nota Asesor");
                        estilo = ""
                        if ($("#cboEtapa").val() == "E") {
                            if (data[i].notaEjecucion > 0) { estilo = "style='font-weight:bold;color:green;text-align:center'" } else { estilo = "style='text-align:center'" }
                            tb += '<td ' + estilo + '>' + data[i].notaEjecucion + '</td>';
                        } else if ($("#cboEtapa").val() == "I") {
                            if (data[i].notaInforme > 0) { estilo = "style='font-weight:bold;color:green;text-align:center'" } else { estilo = "style='text-align:center'" }
                            tb += '<td ' + estilo + '>' + data[i].notaInforme + '</td>';
                        } else {
                            tb += '<td style="text-align:center"></td>';
                            $("#colAsesor").html("");
                        }

                        tb += '<td style="text-align:center">';
                        if (data[i].cod_tes == "0") {
                            tb += '<button type="button" id="btnNuevo" class="btn btn-sm btn-success" onclick="Nuevo(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',\'' + data[i].etapa + '\')" title="Ver" ><i class="ion ion-android-add-circle"></i></button>';
                        } else {
                            tb += '<button type="button" id="btnEditar" class="btn btn-sm btn-info" onclick="Editar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',\'' + data[i].etapa + '\',\'' + $("#cboCurso").val() + '\')" title="Ver" ><i class="ion-edit"></i></button>';
                            if ($("#cboEtapa").val() == "P") {
                                if (data[i].fecBloqueoP == '0') {
                                    tb += '<button type="button" id="btnEnviar" class="btn btn-sm btn-orange" onclick="fnConfirmarEnviar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',0,\'P\')" title="Colocar Candado" ><i class="ion-locked"></i></button>';
                                    tb += '<button type="button" id="btnObservarP" class="btn btn-sm btn-gray" onclick="fnObservar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',\'P\')" title="Observar" ><i class="ion-eye"></i></button>';
                                } else {
                                    tb += '<button type="button" id="btnQuitarEnvio" class="btn btn-sm btn-danger" onclick="fnConfirmarEnviar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',1,\'P\')" title="Quitar Candado" ><i class="ion-unlocked"></i></button>';

                                }
                            }
                            if ($("#cboEtapa").val() == "I") {
                                if (data[i].fecBloqueoI == '0') {
                                    tb += '<button type="button" id="btnEnviar" class="btn btn-sm btn-orange" onclick="fnConfirmarEnviar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',0,\'I\')" title="Colocar Candado" ><i class="ion-locked"></i></button>';
                                    tb += '<button type="button" id="btnObservarI" class="btn btn-sm btn-gray" onclick="fnObservar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',\'I\')" title="Observar" ><i class="ion-eye"></i></button>';
                                } else {
                                    tb += '<button type="button" id="btnQuitarEnvio" class="btn btn-sm btn-danger" onclick="fnConfirmarEnviar(\'' + data[i].cod_tes + '\',\'' + data[i].cod_alu + '\',1,\'I\')" title="Quitar Candado" ><i class="ion-unlocked"></i></button>';
                                }
                            }
                            if (ObtenerValorGET("ctf") == "1" || ObtenerValorGET("ctf") == "144") {
                                if ($("#cboEtapa").val() == "E") {
                                    if (data[i].notaEjecucion > 0) {
                                        tb += '<button type="button" id="btnAnularNotaAsesor" class="btn btn-sm btn-primary" onclick="AnularNotaAsesor(\'' + data[i].cod_tes + '\',\'' + data[i].cac + '\',\'E\')" title="Restablecer nota y porcentaje de Asesor" ><i class="ion ion-close"></i></button>';
                                    }
                                }
                                if ($("#cboEtapa").val() == "I") {
                                    if (data[i].notaInforme > 0) {
                                        tb += '<button type="button" id="btnAnularNotaAsesor" class="btn btn-sm btn-primary" onclick="AnularNotaAsesor(\'' + data[i].cod_tes + '\',\'' + data[i].cac + '\',\'I\')" title="Restablecer nota y porcentaje de Asesor" ><i class="ion ion-close"></i></button>';
                                    }
                                }
                            }
                        }

                        //                        tb += '<button type="button" id="btnEnviar" class="btn btn-sm btn-orange" onclick="Enviar(\'' + data[i].cod_tes + '\')" title="Enviar" ><i class="ion-arrow-right-a"></i></button>';

                        //                        tb += '<button type="button" id="btnEliminar" class="btn btn-sm btn-red" onclick="fnConfirmarEliminar(\'' + data[i].cod + '\')" title="Eliminar" ><i class="ion-close"></i></button>';
                        //}
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tAlumnos');
                $('#tbAlumnos').html(tb);
                fnResetDataTableBasic('tAlumnos', 0, 'asc', 300);

                /* AUTOCOMPLETAR ALUMNO */

                $('#txtAutor').autocomplete({
                    source: $.map(data, function(item) {
                        return item.alumno;
                    }),
                    select: function(event, ui) {
                        var selectecItem = data.filter(function(value) {
                            return value.alumno == ui.item.value;
                        });
                        codalu = selectecItem[0].cod_alu;
                        coduniver = selectecItem[0].coduniver;
                        nombre = selectecItem[0].alumno;

                        $('#PanelEvento').hide("fade");
                    },
                    minLength: 1,
                    delay: 500
                });

                $('#txtAutor').keyup(function() {
                    var l = parseInt($(this).val().length);
                    if (l == 0) {
                        codalu = "";
                        coduniver = "";
                        nombre = "";
                    }
                });

                /* FIN AUTOCOMPLETAR */
                fnLoading(false);
            },
            error: function(result) {
                //fnMensaje("error", result)
                fnLoading(false);
            }
        });
    } else {
        fnDestroyDataTableDetalle('tAlumnos');
        $('#tbAlumnos').html('');
        fnResetDataTableBasic('tAlumnos', 0, 'asc', 300);
    }
    //    fnLoading(false);
}


function VerTabxEtapa(etapa) {
    if (etapa == "P") {
        $("#TabProyecto").attr("style", "display:table-cell");
        $("#Tab1").trigger("click");
        $("#TabEjecucion").attr("style", "display:none");
        $("#TabInforme").attr("style", "display:none");
        $("#TabSustentacion").attr("style", "display:none");
        $("#btnGuardarTesis").attr("style", "display:inline-block");
    }
    if (etapa == "E") {
        $("#TabProyecto").attr("style", "display:table-cell");
        $("#TabProyecto").attr("class", "");
        $("#TabEjecucion").attr("style", "display:table-cell");
        $("#Tab2").trigger("click");
        $("#TabInforme").attr("style", "display:none");
        $("#TabSustentacion").attr("style", "display:none");
        $("#btnGuardarPreinforme").attr("style", "display:inline-block");
        $("#filepreinforme").removeAttr('disabled');
        $("#cboAsesor").removeAttr('disabled');
        if ($("#hdcod").val() == 0) {
            $("#btnGuardarTesis").attr("style", "display:inline-block");
            $("#hometabnb input").removeAttr('disabled');
            $("#hometabnb select").removeAttr('disabled');
        } else {
            $("#btnGuardarTesis").attr("style", "display:none");
            $("#hometabnb input").attr('disabled', 'disabled');
            $("#hometabnb select").attr('disabled', 'disabled');
        }
    }
    if (etapa == "I") {
        $("#TabProyecto").attr("style", "display:table-cell");
        $("#TabProyecto").attr("class", "");
        $("#TabEjecucion").attr("style", "display:table-cell");
        $("#TabEjecucion").attr("class", "");
        $("#TabInforme").attr("style", "display:table-cell");
        $("#Tab3").trigger("click");
        $("#TabSustentacion").attr("style", "display:none");

        if ($("#hdcod").val() == 0) {
            $("#btnGuardarTesis").attr("style", "display:inline-block");
            $("#hometabnb input").removeAttr('disabled');
            $("#hometabnb select").removeAttr('disabled');
            $("#btnGuardarPreinforme").attr("style", "display:inline-block");
            $("#filepreinforme").removeAttr('disabled');
            $("#cboAsesor").removeAttr('disabled');
        } else {
            $("#btnGuardarTesis").attr("style", "display:none");
            $("#hometabnb input").attr('disabled', 'disabled');
            $("#hometabnb select").attr('disabled', 'disabled');
            $("#btnGuardarPreinforme").attr("style", "display:none");
            $("#filepreinforme").attr('disabled', 'disabled');
            $("#cboAsesor").attr('disabled', 'disabled');
        }
    }
    $("#hdcod").removeAttr("disabled");
    $("#hdcoA").removeAttr("disabled");
    VisualizacionxPerfil(etapa);
}

function Nuevo(cod, cod_alu, etapa) {
    fnLoading(true)
    fnLimpiarProyecto();
    $("#ObservacionesProyecto").html("");
    $("#btnObservacionesProyecto").attr("style", "display:none");
    $("#ObservacionesInforme").html("");
    $("#btnObservacionesInforme").attr("style", "display:none");
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lDatosAlumno" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
    $('form#frm').append('<input type="hidden" id="param2" name="param2" value="' + cod_alu + '" />');
    $('form#frm').append('<input type="hidden" id="param3" name="param3" value="' + $("#cboSemestre").val() + '" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $("#hdcod").val(cod);
    $("#hdcodA").val(cod_alu);
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log(data);
            $("#TituloPanel").html("<b>" + data[0].coduniver + ' - ' + data[0].nom_alu + "</b>");
            $("#lblFacultad").html(data[0].facultad);
            $("#lblCarrera").html(data[0].carrera);
            $("#txtfeciniTes").val(data[0].fecini_cac);

            $("#Lista").attr("style", "display:none")
            $("#RegistroTabs").attr("style", "display:block")
            VerTabxEtapa(etapa)
            //fnLoading(false);

            Autores.push({
                cod_aut: 0,
                cod: cod_alu,
                coduniver: data[0].coduniver,
                nombre: data[0].nom_alu,
                codtipo: tipopart,
                destipo: despart,
                estado: 1
            });

            var tb;
            for (i = 0; i < Autores.length; i++) {
                if (Autores[i].estado == 1) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + Autores[i].coduniver + '</td>';
                    tb += '<td>' + Autores[i].nombre + '</td>';
                    tb += '<td>' + Autores[i].destipo + '</td>';
                    //tb += '<td style="text-align:center"></td>';
                    tb += '<td style="text-align:center">';
                    tb += '<button type="button" id="btnDeleteAutor" name="btnDeleteAutor" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarAutor(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    tb += '</td>';
                    tb += '</tr>';
                }
            }

            fnDestroyDataTableDetalle('tAutor');
            $('#tbAutor').html(tb);
            fnResetDataTableBasic('tAutor', 0, 'asc', 10);
            $("#cbotipoInvestigacion").focus();
            $("#chkOCDE").prop('checked', false);


            if (data[0].matTesis > 0) {
                $("#hdValidaJur").val(etapa);
            }
        },
        error: function(result) {
            //fnMensaje("error", result)
            //fnLoading(false);
        }
    });
    fnLoading(false);
}

function Editar(cod, cod_alu, etapa, cup) {
    fnLoading(true)
    fnLimpiarProyecto();
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lDatosTesis" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
    $('form#frm').append('<input type="hidden" id="param2" name="param2" value="' + cod_alu + '" />');
    $('form#frm').append('<input type="hidden" id="param3" name="param3" value="' + cup + '" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $("#hdcod").val(cod);
    $("#hdcodA").val(cod_alu);
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        async: false,
        success: function(data) {
            //console.log(data);
            $("#cbotipoInvestigacion").val(data[0].cod_tin);
            $("#txtTitulo").val(data[0].titulo);
            $("#TituloPanel").html("<b>" + data[0].coduniver + ' - ' + data[0].nom_alu + "</b>");
            $("#lblFacultad").html(data[0].facultad);
            $("#lblCarrera").html(data[0].carrera);
            //$("#lblLinea").html(data[0].linea);
            $("#cboLinea").val(data[0].cod_linea);
            $("#txtfeciniTes").val(data[0].fec_ini);
            $("#txtfecfinTes").val(data[0].fec_fin);
            $("#txtFecActa").val(data[0].fec_acta);
            $("#cboAsesor").val(data[0].asesor)

            if (data[0].financ != null) {

                var array = data[0].financ.split(',');
                for (var i = 0; i < array.length; i++) {
                    if (array[i] == 'P') {
                        $("#chkPropio").prop("checked", true)
                    }
                    if (array[i] == 'U') {
                        $("#chkUsat").prop("checked", true)
                        $("#txtusat").attr("style", "display:block")
                        $("#txtusat").val(data[0].financusat)
                    }
                    if (array[i] == 'E') {
                        $("#chkExterno").prop("checked", true)
                        $("#txtexterno").attr("style", "display:block")
                        $("#txtexterno").val(data[0].financexterno)
                    }
                }
            }
            $("#txtpresupuesto").val(data[0].presu);
            // $("#txtavance").val(data[0].avance);


            $("#txtNotaSustentacionP").val(data[0].nota_susP);
            $("#txtFechaSustentacionP").val(data[0].fec_susP);
            $("#txtNotaSustentacionI").val(data[0].nota_susI);
            $("#txtFechaSustentacionI").val(data[0].fec_susI);

            //console.log(data);
            if (data[0].nota_E != "0" && data[0].porcentaje_E != "0") {
                $("#lblNotaEjecucion").html("Nota: " + data[0].nota_E)
                $("#lblPorcentajeEjecucion").html("Porcentaje: " + data[0].porcentaje_E)
            } else {
                $("#lblNotaEjecucion").html("")
                $("#lblPorcentajeEjecucion").html("")
            }

            $("#Lista").attr("style", "display:none")
            $("#RegistroTabs").attr("style", "display:block")
            VerTabxEtapa(etapa)
            $("#cbotipoInvestigacion").focus();
            $("#cboDepartamento").val(data[0].cod_dac)
            fnListarPersonalDepartamentoAcademico(data[0].cod_dac);
            //fnLoading(false);
            fnListarAutor(data[0].cod_tes)
            fnListarObjetivos(data[0].cod_tes)
            fnListarAsesor(data[0].cod_tes)
            fnListarJurado(data[0].cod_tes)
            $("#ObservacionesProyecto").html("");
            $("#btnObservacionesProyecto").attr("style", "display:none");

            if (Jurado.length > 0) {
                for (k = 0; k < Jurado.length; k++) {
                    fnListarObservaciones(data[0].cod_tes, Jurado[k].cod_jur, Jurado[k].nombre, "ObservacionesProyecto");
                }
            }

            fnListarJuradoInforme(data[0].cod_tes)
            $("#ObservacionesInforme").html("");
            $("#btnObservacionesInforme").attr("style", "display:none");
            if (JuradoInforme.length > 0) {
                for (j = 0; j < JuradoInforme.length; j++) {
                    fnListarObservaciones(data[0].cod_tes, JuradoInforme[j].cod_jur, JuradoInforme[j].nombre, "ObservacionesInforme");
                }
            }

            //Agregar el mismo jurado si no tiene jurado para informe registrado y va a editar
            if (JuradoInforme.length == 0 && Jurado.length > 0) {
                var tb = "";
                for (i = 0; i < Jurado.length; i++) {
                    if (Jurado[i].estado == 1) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
                        tb += '<td style="width:60%">' + Jurado[i].nombre + '</td>';
                        tb += '<td style="text-align:center; width:15%">' + Jurado[i].destipo + '</td>';
                        tb += '<td style="text-align:center; width:10%">PENDIENTE</td>';
                        tb += '<td style="text-align:center; width:10%">';
                        tb += '<button type="button" id="btnDeleteJuradoInforme" name="btnDeleteJuradoInforme" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarJuradoInforme(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                        tb += '</td>';
                        tb += '</tr>';

                        JuradoInforme.push({
                            cod_jur: 0,
                            cod: Jurado[i].cod,
                            nombre: Jurado[i].nombre,
                            codtipo: Jurado[i].codtipo,
                            destipo: Jurado[i].destipo,
                            estado: 1
                        });

                    }
                }
                fnDestroyDataTableDetalle('tJuradoInforme');
                $('#tbJuradoInforme').html(tb);
                fnResetDataTableBasic('tJuradoInforme', 0, 'asc', 300);
            }


            if (data[0].proyecto != "") {
                $("#file_proyecto").html('<a onclick="fnDownload(\'' + data[0].proyecto + '\')" >Descargar Proyecto</a>')
            } else {
                $("#file_proyecto").html("");
            }

            if (data[0].actaproyecto != "") {
                $("#file_acta").html('<a onclick="fnDownload(\'' + data[0].actaproyecto + '\')" >Descargar Acta</a>')
            } else {
                $("#file_acta").html("");
            }

            if (data[0].similitudproyecto != "") {
                $("#file_similitudProyecto").html('<a onclick="fnDownload(\'' + data[0].similitudproyecto + '\')" >Descargar Informe de Similitud</a>')
            } else {
                $("#file_similitudProyecto").html("");
            }

            if (data[0].preinforme != "") {
                $("#file_preinforme").html('<a onclick="fnDownload(\'' + data[0].preinforme + '\')" >Descargar avance</a>')
            } else {
                $("#file_preinforme").html("");
            }
            if (data[0].informe != "") {
                $("#file_informe").html('<a onclick="fnDownload(\'' + data[0].informe + '\')" >Descargar Informe</a>')
            } else {
                $("#file_informe").html("");
            }
            if (data[0].linkinforme != "") {
                $("#txtLinkInforme").val(data[0].linkinforme)
            } else {
                $("#txtLinkInforme").val("");
            }
            if (data[0].actainforme != "") {
                $("#file_actainforme").html('<a onclick="fnDownload(\'' + data[0].actainforme + '\')" >Descargar Acta de Informe</a>')
            } else {
                $("#file_actainforme").html("");
            }
            if (data[0].similitudinforme != "") {
                $("#file_similitud").html('<a onclick="fnDownload(\'' + data[0].similitudinforme + '\')" >Descargar Informe de Similitud Antiplagio</a>')
            } else {
                $("#file_similitud").html("");
            }
            if (data[0].cod_dis != "0") {
                $("#chkOCDE").prop('checked', true);
                $("#ocde").attr("style", "display:block");
                fnListarAreaOCDE($("#cboLinea").val());
                $("#cboArea").val(data[0].cod_area)
                fnListarOCDE($("#cboArea").val(), 'SA');
                $("#cboSubArea").val(data[0].cod_sub)
                fnListarOCDE($("#cboSubArea").val(), 'DI');
                $("#cboDisciplina").val(data[0].cod_dis)
            } else {
                $("#chkOCDE").prop('checked', false);
                $("#ocde").removeAttr("style");
                $("#ocde").attr("style", "display:none");
            }

            if (data[0].matTesis > 0) {
                $("#hdValidaJur").val(etapa);
            }
        },
        error: function(result) {
            //fnMensaje("error", result)
            //fnLoading(false);
        }
    });
    fnLoading(false);

}

/*========================================================================================================================================*/
/*======================================================== AUTORES =====================================================================*/
/*========================================================================================================================================*/


function fnListarAutor(codigo_Tes) {
    Autores = [];
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lAutor" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + codigo_Tes + '" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';

            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + "" + '</td>';
                    tb += '<td style="text-align:center; width:15%">' + data[i].coduni + '</td>';
                    tb += '<td style="width:50%">' + data[i].nom + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + data[i].destipo + '</td>';
                    tb += '<td style="text-align:center; with:10%">';
                    tb += '<button type="button" id="btnQuitarAutor" class="btn btn-red btn-xs" onclick="fnQuitarAutor(\'' + (i + 1) + '\')" title="Quitar" ><i class="ion-android-delete"></i></button>';
                    //                        tb += '<button type="button" id="btnEliminar" class="btn btn-sm btn-red" onclick="fnConfirmarEliminar(\'' + data[i].cod + '\')" title="Eliminar" ><i class="ion-close"></i></button>';
                    //}
                    tb += '</td>';
                    tb += '</tr>';

                    Autores.push({
                        cod_aut: data[i].cod_rtes,
                        cod: data[i].cod_alu,
                        coduniver: data[i].coduni,
                        nombre: data[i].nom,
                        codtipo: data[i].codtipo,
                        destipo: data[i].destipo,
                        estado: 1
                    });
                }
            }
            fnDestroyDataTableDetalle('tAutor');
            $('#tbAutor').html(tb);
            fnResetDataTableBasic('tAutor', 0, 'asc', 300);

        },
        error: function(result) {
            //fnMensaje("error", result)
            fnLoading(false);
        }
    });
}

function fnAgregarAutor() {
    var value;
    var tb = '';
    var rowCount = $('#tbAutor tr').length;
    var repite = false;
    //console.log(equipo);
    if (fnValidarAutor() == true) {
        //          $.grep(detalles, function(e) { return e.item == id; });
        for (i = 0; i < Autores.length; i++) {
            if (Autores[i].cod == codalu && Autores[i].estado == 1) {
                repite = true;
            }
        }
        //console.log(repite);
        if (repite == false) {
            $('#tbAutor tr').each(function() {
                value = $(this).find("td:first").html()
            });
            if (!($.isNumeric(value))) { rowCount = 0 }
            var row = (rowCount + 1);
            Autores.push({
                cod_aut: 0,
                cod: codalu,
                coduniver: coduniver,
                nombre: nombre,
                codtipo: $("#cboTipoAutor").val(),
                destipo: $("#cboTipoAutor option:selected").text(),
                estado: 1
            });
            console.log(Autores);
            for (i = 0; i < Autores.length; i++) {
                if (Autores[i].estado == 1) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
                    tb += '<td style="text-align:center; width:15%">' + Autores[i].coduniver + '</td>';
                    tb += '<td style="width:50%">' + Autores[i].nombre + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + Autores[i].destipo + '</td>';
                    //tb += '<td style="text-align:center"></td>';
                    tb += '<td style="text-align:center; width:10%">';
                    //                    if (Autores[i].codtipo == tipopart) {
                    //                        tb += '';
                    //                    } else {
                    tb += '<button type="button" id="btnDeleteAutor" name="btnDeleteAutor" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarAutor(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    //                    }
                    tb += '</td>';
                    tb += '</tr>';
                }
            }
            fnDestroyDataTableDetalle('tAutor');
            $('#tbAutor').html(tb);
            fnResetDataTableBasic('tAutor', 0, 'asc', 10);
            $("#txtAutor").val("");
            $("#cboTipoAutor").val("");
        } else {
            fnMensaje("error", "El Alumno ya ha sido ingresado")
        }

    }
}

function fnValidarAutor() {
    if ($("#txtAutor").val() == "" || codalu == "") {
        fnMensaje("error", "Debe seleccionar un alumno");
        return false;
    }
    if ($("#cboTipoAutor").val() == "") {
        fnMensaje("error", "Debe seleccionar un tipo de autor");
        return false;
    }
    /*
    if ($("#cboTipoAutor option:selected").text() == "AUTOR") {
    var cont = 0;
    for (i = 0; i < Autores.length; i++) {
    if (Autores[i].destipo== "AUTOR" && Autores[i].estado == 1) {
    cont = cont + 1;
    }
    }
    if (cont > 0) {
    fnMensaje("error", "El Grupo Solo puede tener un Coordinador General.");
    return false;
    }
    }*/
    return true;
}

function fnQuitarAutor(cod) {
    var tb = '';
    //console.log(cod);
    //      document.getElementById("tEval").deleteRow(cod);
    if (Autores[cod - 1].cod_aut == 0) {
        Autores.splice(cod - 1, 1);
    } else {
        Autores[cod - 1].estado = 0;
    }
    for (i = 0; i < Autores.length; i++) {
        if (Autores[i].estado == 1) {
            tb += '<tr>';
            tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
            tb += '<td style="text-align:center;width:15%">' + Autores[i].coduniver + '</td>';
            tb += '<td style="width:50%">' + Autores[i].nombre + '</td>';
            tb += '<td style="text-align:center; width:20%">' + Autores[i].destipo + '</td>';
            tb += '<td style="text-align:center; width:10%">';
            tb += '<button type="button" id="btnDeleteAutor" name="btnDeleteAutor" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarAutor(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
            tb += '</td>';
            tb += '</tr>';
        }
    }
    //console.log(Autores);
    fnDestroyDataTableDetalle('tAutor');
    $('#tbAutor').html(tb);
    fnResetDataTableBasic('tAutor', 0, 'asc', 10);
}

function fnGuardarAutor(cod) {
    fnLoading(true)
    //if ($('#codE').val() == "0") {
    var form = JSON.stringify(Autores);
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: { "hdcod": cod, "action": "RegAutor", "array": form },
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log("OK");
            filas = data.length
            for (i = 0; i < filas; i++) {
                if (data[i].rpta == 0) {
                    fnMensaje("error", data[i].msje)
                }
            }
        },
        error: function(result) {
            //console.log(result)
        }
    });
    fnLoading(false);
}

/*========================================================================================================================================*/
/*======================================================== OBJETIVOS =====================================================================*/
/*========================================================================================================================================*/

function fnListarObjetivos(codigo_Tes) {
    objetivos = [];
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lObjetivos" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + codigo_Tes + '" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            var tb = ''
            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    if (data[i].tipo_obj == "OBJETIVO GENERAL") {
                        tb += '<tr style="font-weight:bold;color:green;">';
                    } else {
                        tb += '<tr>';
                    }
                    tb += '<td style="width:70%">' + data[i].des_obj + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + data[i].tipo_obj + '</td>';
                    tb += '<td style="text-align:center; width:10%">';
                    tb += '<button type="button" id="btnQuitarObjetivo" class="btn btn-red btn-xs" onclick="fnQuitarObjetivo(\'' + (i + 1) + '\')" title="Quitar" ><i class="ion-android-delete"></i></button>';
                    tb += '</td>';
                    tb += '</tr>';

                    objetivos.push({
                        cod: data[i].cod_obj,
                        descripcion: data[i].des_obj,
                        codtipo: data[i].codtipo_obj,
                        tipo: data[i].tipo_obj,
                        estado: 1
                    });
                }
            }
            fnDestroyDataTableDetalle('tObjetivos');
            $('#tbObjetivos').html(tb);
            fnResetDataTableBasic('tObjetivos', 1, 'desc', 300);

        },
        error: function(result) {
            fnLoading(false);
        }
    });
}



function fnAgregarObjetivo() {
    var value;
    var tb = '';
    var rowCount = $('#tbObjetivos tr').length;
    var repite = false;
    var msje = '';
    if (fnValidarObjetivo() == true) {
        for (i = 0; i < objetivos.length; i++) {
            if (objetivos[i].descripcion == $("#txtobjetivo").val() && objetivos[i].estado == 1) {
                repite = true;
                msje = "El objetivo ya se encuentra registrado.";
            }
        }
        if ($("#cboTipoObjetivo option:selected").text() == "OBJETIVO GENERAL") {
            for (i = 0; i < objetivos.length; i++) {
                if (objetivos[i].tipo == "OBJETIVO GENERAL" && objetivos[i].estado == 1) {
                    repite = true;
                    msje = "El proyecto solo puede contener un objetivo general.";
                }
            }
        }
        if (repite == false) {
            $('#tbObjetivos tr').each(function() {
                value = $(this).find("td:first").html();

            });
            if (!($.isNumeric(value))) { rowCount = 0 }

            var row = (rowCount + 1);

            objetivos.push({
                cod: codobj,
                descripcion: $("#txtobjetivo").val(),
                codtipo: $("#cboTipoObjetivo").val(),
                tipo: $("#cboTipoObjetivo option:selected").text(),
                estado: 1
            });

            //console.log(objetivos);

            for (i = 0; i < objetivos.length; i++) {
                if (objetivos[i].estado == 1) {
                    if (objetivos[i].tipo == "OBJETIVO GENERAL") {
                        tb += '<tr style="font-weight:bold;color:green;">';
                    } else {
                        tb += '<tr>';
                    }
                    tb += '<td style="width:70%">' + objetivos[i].descripcion + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + objetivos[i].tipo + '</td>';
                    tb += '<td style="text-align:center; width:10%">';
                    tb += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    tb += '</td></tr>';
                }
            }
            //console.log(tb)
            fnDestroyDataTableDetalle('tObjetivos');
            $('#tbObjetivos').html(tb);
            fnResetDataTableBasic('tObjetivos', 1, 'desc', 300);
            $("#txtobjetivo").val("");
            $("#cboTipoObjetivo").val("");
        } else {
            fnMensaje("error", msje)
        }

    }
}

function fnValidarObjetivo() {
    if ($("#txtobjetivo").val() == "") {
        fnMensaje("error", "Ingrese descripción de objetivo");
        return false;
    }
    if ($("#cboTipoObjetivo").val() == "") {
        fnMensaje("error", "Debe seleccionar un tipo de objetivo");
        return false;
    }
    return true;
}

function fnQuitarObjetivo(cod) {
    var tb = '';
    //console.log(cod);
    if (objetivos[cod - 1].cod == 0) {
        objetivos.splice(cod - 1, 1);
    } else {
        objetivos[cod - 1].estado = 0;
    }
    for (i = 0; i < objetivos.length; i++) {
        if (objetivos[i].estado == 1) {
            if (objetivos[i].tipo == "OBJETIVO GENERAL") {
                tb += '<tr style="font-weight:bold;color:green;">';
            } else {
                tb += '<tr>';
            }
            tb += '<td style="width:70%">' + objetivos[i].descripcion + '</td>';
            tb += '<td style="text-align:center; width:20%">' + objetivos[i].tipo + '</td>';
            tb += '<td style="text-align:center; width:10%">';
            tb += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
            tb += '</td></tr>';
        }
    }

    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html(tb);
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 300);
}


function fnGuardarObjetivos(cod) {
    fnLoading(true)
    var form = JSON.stringify(objetivos);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: { "hdcod": cod, "action": "RegObjetivos", "array": form },
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            //console.log("OK");
        },
        error: function(result) {
            //console.log(result)

        }
    });
    fnLoading(false);
    //    } else {
    //        window.location.href = rpta
    //    }
}


function OcultarRegistro() {
    $("#RegistroTabs").attr("style", "display:none");
    $("#Lista").attr("style", "display:block")
    $('#collapseProyecto').collapse('hide')
    $('#collapseInforme').collapse('hide')
}

function fnGuardarTesis() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("#txtfeciniTes").removeAttr("disabled");
        formData = new FormData(document.forms.namedItem("frmRegistro"));
        formData.append("action", "Registrar");
        $("#txtfeciniTes").attr("disabled", "disabled");

        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: formData,
            dataType: "json",
            contentType: false,
            processData: false,
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnGuardarAutor(data[0].cod);
                    fnGuardarObjetivos(data[0].cod);
                    //fnGuardarAsesor(data[0].cod);
                    fnGuardarJurado(data[0].cod, "P");
                    if ($("#fileproyecto").val() != "") {
                        SubirArchivo(data[0].cod, "PROYECTO");
                    }
                    if ($("#fileacta").val() != "") {
                        SubirArchivo(data[0].cod, "ACTA");
                    }
                    if ($("#filesimilitudProyecto").val() != "") {
                        SubirArchivo(data[0].cod, "SIMILITUDPROYECTO");
                    }

                    $("#hdcod").val(data[0].cod);
                    $('#collapseProyecto').collapse({
                        toggle: true
                    })
                    $('#collapseInforme').collapse({
                        toggle: true
                    })
                    OcultarRegistro();
                    fnListarAlumnos();
                    fnLoading(false);
                } else {
                    fnLoading(false);
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                fnMensaje("error", "No se Pudo registrar proyecto de tesis")
                //fnMensaje("error", result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}

function fnValidarTesis() {
    if ($("#cbotipoInvestigacion").val() == "") {
        fnMensaje("error", "Debe seleccionar la etapa de tesis.");
        $("#cbotipoInvestigacion").focus();
        return false;
    }
    if ($("#txtTitulo").val() == "") {
        fnMensaje("error", "Ingrese título de investigación.");
        $("#txtTitulo").focus();
        return false;
    }
    if ($("#cboLinea").val() == "" || $("#cboLinea").val() == undefined) {
        fnMensaje("error", "Debe seleccionar una linea de investigación.");
        $("#cboLinea").focus();
        return false;
    }
    if (!$("#chkOCDE").is(':checked')) {
        fnMensaje("error", "Debe seleccionar una linea de investigación OCDE.");
        return false;
    }

    if ($("#chkOCDE").is(':checked')) {
        if ($("#cboArea").val() == "0") {
            fnMensaje("error", "Debe Seleccionar Una Área de investigación OCDE.");
            return false;
        }
        if ($("#cboSubArea").val() == "0") {
            fnMensaje("error", "Debe Seleccionar una Sub Área de investigación OCDE.");
            return false;
        }
        if ($("#cboDisciplina").val() == "0") {
            fnMensaje("error", "Debe Seleccionar una Disciplina de investigación OCDE.");
            return false;
        }
    }
    // Verificar Autores, como mínimo 1
    //console.log(Autores);
    var bandera = 0;
    for (i = 0; i < Autores.length; i++) {
        if (Autores[i].estado == 1 && Autores[i].destipo == 'AUTOR') {
            bandera = bandera + 1;
        }
    }
    if (bandera == 0) {
        fnMensaje("error", "Proyecto debe contar con un autor como mínimo.");
        return false;
    }

    // Verificar Objetivos, un general como nínimo
    var banderaG = 0;
    for (i = 0; i < objetivos.length; i++) {
        if (objetivos[i].estado == 1 && objetivos[i].tipo == 'OBJETIVO GENERAL') {
            banderaG = banderaG + 1;
        }
    }
    if (banderaG == 0) {
        fnMensaje("error", "Proyecto debe contar con un objetivo general.");
        return false;
    }

    if ($("#txtfeciniTes").val() == "") {
        fnMensaje("error", "Debe Seleccionar una fecha de inicio.");
        $("#txtfeciniTes").focus();
        return false;
    }
    if ($("#txtfecfinTes").val() == "") {
        fnMensaje("error", "Debe seleccionar una fecha de fin.");
        $("#txtfecfinTes").focus();
        return false;
    }

    var fecini = new Date($("#txtfeciniTes").val().substr(3, 2) + '/' + $("#txtfeciniTes").val().substr(0, 2) + '/' + $("#txtfeciniTes").val().substr(6, 4));
    var fecfin = new Date($("#txtfecfinTes").val().substr(3, 2) + '/' + $("#txtfecfinTes").val().substr(0, 2) + '/' + $("#txtfecfinTes").val().substr(6, 4));

    if (fecini > fecfin) {
        fnMensaje("error", "Fecha de fin de proyecto no puede ser menor a la fecha de inicio.");
        return false;
    }

    if ($("#chkPropio").is(':checked') == false && $("#chkUsat").is(':checked') == false && $("#chkExterno").is(':checked') == false) {
        fnMensaje("error", "Debe seleccionar el financiamiento del Proyecto.");
        return false;
    }

    if ($("#chkUsat").is(':checked') == true && $("#txtusat").val() == "") {
        fnMensaje("error", "Debe indicar el financiamiento de USAT.");
        $("#txtusat").focus()
        return false;
    }


    if ($("#chkExterno").is(':checked') == true && $("#txtexterno").val() == "") {
        fnMensaje("error", "Debe indicar el financiamiento externo.");
        $("#txtexterno").focus()
        return false;
    }

    if ($("#txtpresupuesto").val() == "") {
        fnMensaje("error", "Ingrese el presupuesto del Proyecto.");
        $("#txtpresupuesto").focus()
        return false;
    }
    var RE = /^\d*(\.\d{1})?\d{0,1}$/;
    if (!RE.test($("#txtpresupuesto").val())) {
        fnMensaje("error", "Ingrese correctanente el presupuesto, puede colocar hasta 2 decimales después del punto decimal.");
        $("#txtpresupuesto").focus()
        return false;
    }

    if ($("#fileacta").val() != "" || $("#file_acta").html() != "") {
        if ($("#txtNotaSustentacionP").val() == "") {
            fnMensaje("error", "Ingrese nota de sustentación del Proyecto.");
            $("#txtNotaSustentacionP").focus()
            return false;
        }
        if ($("#txtNotaSustentacionP").val() != "") {
            if (!RE.test($("#txtNotaSustentacionP").val())) {
                fnMensaje("error", "Ingrese correctanente la nota de Sustentación, puede colocar hasta 2 decimales después del punto decimal.");
                $("#txtNotaSustentacionP").focus()
                return false;
            }
            if ($("#txtNotaSustentacionP").val() > 20) {
                fnMensaje("error", "Ingrese correctanente la nota de Sustentación, Nota máxima de 20.");
                $("#txtNotaSustentacionP").focus()
                return false;
            }
        }
    }
    /*
    if ($("#txtFechaSustentacionP").val() == "" && Jurado.length == 3) {
    fnMensaje("error", "Debe seleccionar una fecha de Sustentación de Proyecto.");
    $("#txtFechaSustentacionP").focus();
    return false;
    }
    */
    if ($("#txtNotaSustentacionP").val() != "" && ($("#txtFechaSustentacionP").val() == "" || ($("#fileacta").val() == "" && $("#file_acta").html() == ""))) {
        fnMensaje("error", "Para colocar nota de sustentación debe colocar fecha de sustentación y adjuntar el acta de Sustentación.");
        return false;
    }

    if ($("#txtFechaSustentacionP").val() != "" && Jurado.length < 3 && $("#hdValidaJur").val() == "P") {
        fnMensaje("error", "Para colocar fecha de sustentación debe asignar los 3 Jurados de sustentación.");
        return false;
    }


    /*
    if ($("#txtavance").val() == "") {
    fnMensaje("error", "Ingrese el Avance del Proyecto");
    $("#txtavance").focus()
    return false;
    }

    if (!RE.test($("#txtavance").val())) {
    fnMensaje("error", "Ingrese Correctanente el valor de Avance, puede colocar hasta 2 decimales después del punto decimal.");
    $("#txtavance").focus()
    return false;
    }

    if (parseFloat($("#txtavance").val()) > 100) {
    fnMensaje("error", "Ingrese Correctamente el valor de Avance, No puede ser mayor al 100%.");
    $("#txtavance").focus()
    return false;
    }*/

    if ($("#fileproyecto").val() != '') {

        if ($("#fileproyecto")[0].files[0].size >= 20971520) {

            fnMensaje("error", "solo se pueden adjuntar archivos de maximo 20MB");
            return false;
        }

        archivo = $("#fileproyecto").val();
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de proyecto en formato de PDF");
            return false;
        }
    }

    if ($("#fileacta").val() != '') {

        if ($("#fileacta")[0].files[0].size >= 20971520) {
            fnMensaje("error", "solo se pueden adjuntar archivos de máximo 20MB");
            return false;
        }

        archivo = $("#fileacta").val();
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de acta en formato de PDF");
            return false;
        }
    }

    if ($("#filesimilitudProyecto").val() != '') {

        if ($("#filesimilitudProyecto")[0].files[0].size >= 20971520) {
            fnMensaje("error", "solo se pueden adjuntar archivos de máximo 20MB");
            return false;
        }

        archivo = $("#filesimilitudProyecto").val();
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de Reporte de Similitud en formato de PDF");
            return false;
        }
    }
    return true
}

function fnLimpiarProyecto() {
    $("#cbotipoInvestigacion").val("");
    $("#txtTitulo").val("");
    $("#TituloPanel").html("## - ####");
    $("#lblFacultad").html("");
    $("#lblCarrera").html("");
    $("#cboLinea").val("");
    $("#cboArea").val("0");
    $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
    $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
    $("#ocde").removeAttr("style");
    $("#ocde").attr("style", "display:none");
    //$("#lblLinea").html("");
    $("#txtfeciniTes").val("");
    $("#txtfecfinTes").val("");
    $("#chkPropio").prop("checked", false)
    $("#chkUsat").prop("checked", false)
    $("#chkExterno").prop("checked", false)
    $("#txtusat").val("")
    $("#txtusat").attr("style", "display:none");
    $("#txtexterno").val("")
    $("#txtexterno").attr("style", "display:none");
    $("#txtpresupuesto").val("");
    $("#txtNotaSustentacionP").val("");
    $("#txtFechaSustentacionP").val("");
    $("#txtNotaSustentacionI").val("");
    $("#txtFechaSustentacionI").val("");
    $("#lblNotaEjecucion").html("")
    $("#lblPorcentajeEjecucion").html("")
    //$("#txtavance").val("25");
    $("#fileproyecto").val("");
    $("#file_proyecto").html("");
    $("#fileacta").val("");
    $("#file_acta").html("");
    $("#filesimilitudProyecto").val("");
    $("#file_similitudProyecto").html("")
    $("#txtFecActa").val("")
    $("#cboAsesor").val("");
    $("#filepreinforme").val("");
    $("#file_preinforme").html("");
    $("#fileinforme").val("");
    $("#file_informe").html("");
    $("#fileactainforme").val("");
    $("#file_actainforme").html("");
    $("#filesimilitud").val("");
    $("#file_similitud").html("");
    $("#hdValidaJur").val("0");

    Autores = [];
    fnDestroyDataTableDetalle('tAutor');
    $('#tbAutor').html('');
    fnResetDataTableBasic('tAutor', 0, 'asc', 10);
    objetivos = [];
    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html('');
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 300);
    $("#cboDepartamento").val("");
    Asesores = [];
    fnDestroyDataTableDetalle('tAsesor');
    $('#tbAsesor').html('');
    fnResetDataTableBasic('tAsesor', 0, 'asc', 300);
    $("#cboDepartamentoJurado").val("");
    Jurado = [];
    fnDestroyDataTableDetalle('tJurado');
    $('#tbJurado').html('');
    fnResetDataTableBasic('tJurado', 0, 'asc', 300);
    JuradoInforme = [];
    fnDestroyDataTableDetalle('tJuradoInforme');
    $('#tbJuradoInforme').html('');
    fnResetDataTableBasic('tJuradoInforme', 0, 'asc', 300);

}


/*========================================================================================================================================*/
/*======================================================== ASESOR =====================================================================*/
/*========================================================================================================================================*/

function fnListarAsesor(codigo_Tes) {
    Asesores = [];
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lParticipante" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + codigo_Tes + '" />');
    $('form#frm').append('<input type="hidden" id="param2" name="param2" value="S" />');
    $('form#frm').append('<input type="hidden" id="param3" name="param3" value="P" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + "" + '</td>';
                    tb += '<td style="width:60%">' + data[i].nom + '</td>';
                    tb += '<td style="text-align:center; width:15%">' + data[i].destipo + '</td>';
                    tb += '<td style="text-align:center; width:10%"><b>' + data[i].aprueba_dir + '</b></td>';
                    tb += '<td style="text-align:center; with:10%">';
                    tb += '<button type="button" id="btnQuitarAutor" class="btn btn-red btn-xs" onclick="fnQuitarAsesor(\'' + (i + 1) + '\')" title="Quitar" ><i class="ion-android-delete"></i></button>';
                    tb += '</td>';
                    tb += '</tr>';

                    Asesores.push({
                        cod_jur: data[i].cod_jur,
                        cod: data[i].cod_per,
                        nombre: data[i].nom,
                        codtipo: data[i].codtipo,
                        destipo: data[i].destipo,
                        estado: 1
                    });
                }
            }
            fnDestroyDataTableDetalle('tAsesor');
            $('#tbAsesor').html(tb);
            fnResetDataTableBasic('tAsesor', 0, 'asc', 300);

        },
        error: function(result) {
            //fnMensaje("error", result)
            fnLoading(false);
        }
    });
}


function fnAgregarAsesor() {
    var value;
    var tb = '';
    var rowCount = $('#tbAsesor tr').length;
    var repite = false;
    //console.log(equipo);
    if (fnValidarAsesor() == true) {
        //          $.grep(detalles, function(e) { return e.item == id; });
        for (i = 0; i < Asesores.length; i++) {
            if (Asesores[i].cod == $("#cboDocenteAsesor").val() && Asesores[i].estado == 1) {
                repite = true;
            }
        }
        //console.log(repite);
        if (repite == false) {
            $('#tbAsesor tr').each(function() {
                value = $(this).find("td:first").html()
            });
            if (!($.isNumeric(value))) { rowCount = 0 }
            var row = (rowCount + 1);
            Asesores.push({
                cod_jur: 0,
                cod: $("#cboDocenteAsesor").val(),
                nombre: $("#cboDocenteAsesor option:selected").text(),
                codtipo: $("#cboRolAsesor").val(),
                destipo: $("#cboRolAsesor option:selected").text(),
                estado: 1
            });
            //console.log(Asesores);
            for (i = 0; i < Asesores.length; i++) {
                if (Asesores[i].estado == 1) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
                    tb += '<td style="width:65%">' + Asesores[i].nombre + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + Asesores[i].destipo + '</td>';
                    tb += '<td style="text-align:center; width:10%">';
                    tb += '<button type="button" id="btnDeleteAsesor" name="btnDeleteAsesor" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarAsesor(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    tb += '</td>';
                    tb += '</tr>';
                }
            }
            fnDestroyDataTableDetalle('tAsesor');
            $('#tbAsesor').html(tb);
            fnResetDataTableBasic('tAsesor', 0, 'asc', 300);
            fnLimpiarAsesor()
        } else {
            fnMensaje("error", "El docente ya ha sido ingresado")
        }

    }
}


function fnValidarAsesor() {
    if ($("#cboDepartamento").val() == "") {
        fnMensaje("error", "Debe seleccionar un Departamento Académico");
        return false;
    }
    if ($("#cboDocenteAsesor").val() == "") {
        fnMensaje("error", "Debe seleccionar un docente");
        return false;
    }
    if ($("#cboRolAsesor").val() == "") {
        fnMensaje("error", "Debe seleccionar un rol para el docente");
        return false;
    }
    return true;
}

function fnQuitarAsesor(cod) {
    var tb = '';
    if (Asesores[cod - 1].cod_jur == 0) {
        Asesores.splice(cod - 1, 1);
    } else {
        Asesores[cod - 1].estado = 0;
    }
    for (i = 0; i < Asesores.length; i++) {
        if (Asesores[i].estado == 1) {
            tb += '<tr>';
            tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
            tb += '<td style="width:65%">' + Asesores[i].nombre + '</td>';
            tb += '<td style="text-align:center; width:20%">' + Asesores[i].destipo + '</td>';
            tb += '<td style="text-align:center; width:10%">';
            tb += '<button type="button" id="btnDeleteAsesor" name="btnDeleteAsesor" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarAsesor(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
            tb += '</td>';
            tb += '</tr>';
        }
    }
    //console.log(Asesores);
    fnDestroyDataTableDetalle('tAsesor');
    $('#tbAsesor').html(tb);
    fnResetDataTableBasic('tAsesor', 0, 'asc', 10);
}

function fnLimpiarAsesor() {
    //$("#cboDepartamento").val("");
    $("#cboDocenteAsesor").val("");
    $("#cboRolAsesor").val("");
}

function fnGuardarAsesor(cod) {
    if (Asesores.length > 0) {
        fnLoading(true)
        //if ($('#codE').val() == "0") {
        var form = JSON.stringify(Asesores);
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: { "hdcod": cod, "abreviatura_eta": "P", "action": "RegAsesor", "array": form },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log("OK");
            },
            error: function(result) {
                //console.log(result)

            }
        });
        fnLoading(false);
    }
}

/*========================================================================================================================================*/
/*======================================================== JURADO =====================================================================*/
/*========================================================================================================================================*/


function fnListarJurado(codigo_Tes) {
    Jurado = [];
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lParticipante" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + codigo_Tes + '" />');
    $('form#frm').append('<input type="hidden" id="param2" name="param2" value="J" />');
    $('form#frm').append('<input type="hidden" id="param3" name="param3" value="P" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + "" + '</td>';
                    tb += '<td style="width:60%">' + data[i].nom + '</td>';
                    tb += '<td style="text-align:center; width:15%">' + data[i].destipo + '</td>';
                    tb += '<td style="text-align:center; width:10%"><b>' + data[i].aprueba_dir + '</b></td>';
                    tb += '<td style="text-align:center; with:10%">';
                    //if (data[i].aprueba_dir != "APROBADO") {
                    tb += '<button type="button" id="btnQuitarJurado" class="btn btn-red btn-xs" onclick="fnQuitarJurado(\'' + (i + 1) + '\')" title="Quitar" ><i class="ion-android-delete"></i></button>';
                    //}
                    tb += '</td>';
                    tb += '</tr>';

                    Jurado.push({
                        cod_jur: data[i].cod_jur,
                        cod: data[i].cod_per,
                        nombre: data[i].nom,
                        codtipo: data[i].codtipo,
                        destipo: data[i].destipo,
                        aprueba_dir: data[i].aprueba_dir,
                        estado: 1
                    });
                }
            }
            fnDestroyDataTableDetalle('tJurado');
            $('#tbJurado').html(tb);
            fnResetDataTableBasic('tJurado', 0, 'asc', 300);
            //fnLoading(false);
        },
        error: function(result) {
            fnMensaje("error", result)
            //fnLoading(false);
        }
    });
}

function fnListarObservaciones(codigo_Tes, cod_jur, nom_jur, nombre_div) {
    $('body').append('<form id="frm1"></form>');
    $('form#frm1').append('<input type="hidden" id="action" name="action" value="ListarObservacionesJurado" />');
    $('form#frm1').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigo_Tes + '" />');
    $('form#frm1').append('<input type="hidden" id="codigo_jur" name="codigo_jur" value="' + cod_jur + '" />');
    var form1 = $("#frm1").serializeArray();
    $("form#frm1").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form1,
        dataType: "json",
        //cache: false,
        async: false,
        success: function(data1) {
            //console.log(data1);
            var tb1 = $("#" + nombre_div).html();
            var filas1 = data1.length;
            if (filas1 > 0) {
                if (tb1 == "") {
                    //tb1 = tb1 + "<tr style='font-size:12px;color:red;font-weight:bold'><td colspan='4'>Observaciones</td></tr>";
                    //tb1 = tb1 + "<tr style='font-size:11px;color:red;font-weight:bold'><td>Jurado</td><td>Fecha</td><td>Tipo</td><td>Observación</td></tr>";
                    tb1 = tb1 + "<tr><th style='width:25%'>Jurado</th><th style='width:10%'>Fecha</th><th style='width:10%'>Tipo</th><th style='width:55%'>Observación</th></tr>";
                }
                for (m = 0; m < filas1; m++) {
                    //tb1 = tb1 + "<tr style='font-size:11px;color:red;'><td style='width:26%'>" + nom_jur + "</td><td style='width:7%'>" + data1[m].fecha + "</td><td style='width:7%'>" + data1[m].tipo + "</td><td style='width:60%'>" + data1[m].descripcion + "</td></tr>";
                    tb1 = tb1 + "<tr><td style='width:26%'>" + nom_jur + "</td><td style='width:7%'>" + data1[m].fecha + "</td><td style='width:7%'>" + data1[m].tipo + "</td><td style='width:60%'>" + data1[m].descripcion + "</td></tr>";
                }
                $("#btn" + nombre_div).attr("style", "display:block");
            }
            $("#" + nombre_div).html(tb1);
            //console.log(tb1);
        },
        error: function(result) {
            console.log(result);
            fnMensaje("error", result)
            //fnLoading(false);
        }
    });
}


function fnAgregarJurado() {
    var value;
    var tb = '';
    var rowCount = $('#tbJurado tr').length;
    var repite = false;
    //console.log(equipo);
    if (fnValidarJurado() == true) {
        //          $.grep(detalles, function(e) { return e.item == id; });
        for (i = 0; i < Jurado.length; i++) {
            if (Jurado[i].cod == $("#cboDocenteJurado").val() && Jurado[i].estado == 1) {
                repite = true;
            }
            if (Jurado[i].codtipo == $("#cboRolJurado").val() && Jurado[i].estado == 1) {
                repite = true;
            }
        }
        //console.log(repite);
        if (repite == false) {
            $('#tbJurado tr').each(function() {
                value = $(this).find("td:first").html()
            });
            if (!($.isNumeric(value))) { rowCount = 0 }
            var row = (rowCount + 1);
            Jurado.push({
                cod_jur: 0,
                cod: $("#cboDocenteJurado").val(),
                nombre: $("#cboDocenteJurado option:selected").text(),
                codtipo: $("#cboRolJurado").val(),
                destipo: $("#cboRolJurado option:selected").text(),
                aprueba_dir: 'PENDIENTE',
                estado: 1
            });
            //console.log(Jurado);
            for (i = 0; i < Jurado.length; i++) {
                if (Jurado[i].estado == 1) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
                    tb += '<td style="width:60%">' + Jurado[i].nombre + '</td>';
                    tb += '<td style="text-align:center; width:15%">' + Jurado[i].destipo + '</td>';
                    tb += '<td style="text-align:center; width:10%">' + Jurado[i].aprueba_dir + '</td>';
                    tb += '<td style="text-align:center; width:10%">';
                    tb += '<button type="button" id="btnDeleteJurado" name="btnDeleteJurado" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarJurado(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    tb += '</td>';
                    tb += '</tr>';
                }
            }
            fnDestroyDataTableDetalle('tJurado');
            $('#tbJurado').html(tb);
            fnResetDataTableBasic('tJurado', 0, 'asc', 300);
            fnLimpiarJurado()
        } else {
            fnMensaje("error", "El docente o el rol ya ha sido asignado, Debe seleccionar un docente o rol diferente")
        }

    }
}


function fnValidarJurado() {
    if ($("#cboDepartamentoJurado").val() == "") {
        fnMensaje("error", "Debe seleccionar un Departamento Académico");
        return false;
    }
    if ($("#cboDocenteJurado").val() == "") {
        fnMensaje("error", "Debe seleccionar un docente");
        return false;
    }
    if ($("#cboRolJurado").val() == "") {
        fnMensaje("error", "Debe seleccionar un rol para el docente");
        return false;
    }
    return true;
}

function fnQuitarJurado(cod) {
    var tb = '';
    if (Jurado[cod - 1].cod_jur == 0) {
        Jurado.splice(cod - 1, 1);
    } else {
        Jurado[cod - 1].estado = 0;
    }
    for (i = 0; i < Jurado.length; i++) {
        if (Jurado[i].estado == 1) {
            tb += '<tr>';
            tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
            tb += '<td style="width:60%">' + Jurado[i].nombre + '</td>';
            tb += '<td style="text-align:center; width:15%">' + Jurado[i].destipo + '</td>';
            tb += '<td style="text-align:center; width:10%">' + Jurado[i].aprueba_dir + '</td>';
            tb += '<td style="text-align:center; width:10%">';
            //if (data[i].aprueba_dir != "APROBADO") {
            tb += '<button type="button" id="btnDeleteJurado" name="btnDeleteJurado" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarJurado(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
            //}
            tb += '</td>';
            tb += '</tr>';
        }
    }
    //console.log(Asesores);
    fnDestroyDataTableDetalle('tJurado');
    $('#tbJurado').html(tb);
    fnResetDataTableBasic('tJurado', 0, 'asc', 10);
}

function fnLimpiarJurado() {
    //$("#cboDepartamento").val("");
    $("#cboDocenteJurado").val("");
    $("#cboRolJurado").val("");
}
function fnLimpiarJuradoInforme() {
    //$("#cboDepartamento").val("");
    $("#cboDocenteJuradoInforme").val("");
    $("#cboRolJuradoInforme").val("");
}

function fnGuardarJurado(cod, abrev_etapa) {
    if ((Jurado.length > 0 && abrev_etapa == "P") || (JuradoInforme.length > 0 && abrev_etapa == "I")) {
        fnLoading(true)
        //if ($('#codE').val() == "0") {
        if (abrev_etapa == "P") {
            var form = JSON.stringify(Jurado);
        } else if (abrev_etapa = "I") {
            var form = JSON.stringify(JuradoInforme);
        }
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: { "hdcod": cod, "abreviatura_eta": abrev_etapa, "action": "RegAsesor", "array": form },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log("OK");
            },
            error: function(result) {
                //console.log(result)

            }
        });
        fnLoading(false);
    }
}


function fnValidarPreinforme() {
    if ($("#hdcod").val() == "0") {
        fnMensaje("error", "Debe registrar un proyecto.");
        $("#Tab1").trigger("click");
        return false;
    }

    //    if ($("#filepreinforme").val() == "") {
    //        fnMensaje("error", "Debe seleccionar un archivo")
    //        return false;
    //    }

    if ($("#filepreinforme").val() != '') {
        if ($("#filepreinforme")[0].files[0].size >= 20971520) {
            fnMensaje("error", "solo se pueden adjuntar archivos de máximo 20MB");
            return false;
        }

        archivo = $("#filepreinforme").val();
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de preInforme en formato de PDF");
            return false;
        }
    }

    return true

}

function fnGuardarPreinforme() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        SubirArchivo($("#hdcod").val(), "PREINFORME");
        fnLoading(false)

        //        if ($("#txtFecActa").val() != "") {
        //            ActualizarDatosTesis($("#hdcod").val(), $("#txtFecActa").val())
        //        }

        if ($("#cboAsesor").val() != "") {
            GuardarAsesor($("#hdcod").val(), 'E', $("#cboAsesor").val());
        }
        fnMensaje("success", "Registro de preinforme guardado correctamente");
        $('#collapseProyecto').collapse({
            toggle: true
        })
        $('#collapseInforme').collapse({
            toggle: true
        })
        fnListarAlumnos();
        OcultarRegistro();
    } else {
        window.location.href = rpta
    }
}


function fnListarJuradoInforme(codigo_Tes) {
    JuradoInforme = [];
    $('body').append('<form id="frm"></form>');
    $('form#frm').append('<input type="hidden" id="action" name="action" value="lParticipante" />');
    $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + codigo_Tes + '" />');
    $('form#frm').append('<input type="hidden" id="param2" name="param2" value="J" />');
    $('form#frm').append('<input type="hidden" id="param3" name="param3" value="I" />');
    var form = $("#frm").serializeArray();
    $("form#frm").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: form,
        dataType: "json",
        //cache: false,
        async: false,
        success: function(data) {

            var tb = '';
            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + "" + '</td>';
                    tb += '<td style="width:60%">' + data[i].nom + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + data[i].destipo + '</td>';
                    tb += '<td style="text-align:center; width:10%"><b>' + data[i].aprueba_dir + '</b></td>';
                    tb += '<td style="text-align:center; with:10%">';
                    //                    if (data[i].aprueba_dir != "APROBADO") {
                    tb += '<button type="button" id="btnQuitarJuradoInforme" class="btn btn-red btn-xs" onclick="fnQuitarJuradoInforme(\'' + (i + 1) + '\')" title="Quitar" ><i class="ion-android-delete"></i></button>';
                    //                    }
                    tb += '</td>';
                    tb += '</tr>';

                    JuradoInforme.push({
                        cod_jur: data[i].cod_jur,
                        cod: data[i].cod_per,
                        nombre: data[i].nom,
                        codtipo: data[i].codtipo,
                        destipo: data[i].destipo,
                        aprueba_dir: data[i].aprueba_dir,
                        estado: 1
                    });
                }
            }
            fnDestroyDataTableDetalle('tJuradoInforme');
            $('#tbJuradoInforme').html(tb);
            fnResetDataTableBasic('tJuradoInforme', 0, 'asc', 300);
            //fnLoading(false);
        },
        error: function(result) {
            fnMensaje("error", result)
            //fnLoading(false);
        }
    });
}


function fnAgregarJuradoInforme() {
    var value;
    var tb = '';
    var rowCount = $('#tbJuradoInforme tr').length;
    var repite = false;
    //console.log(equipo);
    if (fnValidarJuradoInforme() == true) {
        //          $.grep(detalles, function(e) { return e.item == id; });
        for (i = 0; i < JuradoInforme.length; i++) {
            if (JuradoInforme[i].cod == $("#cboDocenteJuradoInforme").val() && JuradoInforme[i].estado == 1) {
                repite = true;
            }
            if (JuradoInforme[i].codtipo == $("#cboRolJuradoInforme").val() && JuradoInforme[i].estado == 1) {
                repite = true;
            }
        }

        //console.log(repite);
        if (repite == false) {
            $('#tbJuradoInforme tr').each(function() {
                value = $(this).find("td:first").html()
            });
            if (!($.isNumeric(value))) { rowCount = 0 }
            var row = (rowCount + 1);
            JuradoInforme.push({
                cod_jur: 0,
                cod: $("#cboDocenteJuradoInforme").val(),
                nombre: $("#cboDocenteJuradoInforme option:selected").text(),
                codtipo: $("#cboRolJuradoInforme").val(),
                destipo: $("#cboRolJuradoInforme option:selected").text(),
                aprueba_dir: 'PENDIENTE',
                estado: 1
            });
            console.log(JuradoInforme);
            for (i = 0; i < JuradoInforme.length; i++) {
                if (JuradoInforme[i].estado == 1) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
                    tb += '<td style="width:55%">' + JuradoInforme[i].nombre + '</td>';
                    tb += '<td style="text-align:center; width:20%">' + JuradoInforme[i].destipo + '</td>';
                    tb += '<td style="text-align:center; width:10%"><b>' + JuradoInforme[i].aprueba_dir + '</b></td>';
                    tb += '<td style="text-align:center; width:10%">';
                    tb += '<button type="button" id="btnDeleteJuradoInforme" name="btnDeleteJuradoInforme" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarJuradoInforme(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    tb += '</td>';
                    tb += '</tr>';
                }
            }
            fnDestroyDataTableDetalle('tJuradoInforme');
            $('#tbJuradoInforme').html(tb);
            fnResetDataTableBasic('tJuradoInforme', 0, 'asc', 300);
            fnLimpiarJuradoInforme()
        } else {
            fnMensaje("error", "El docente o tipo de jurado ya ha sido asignado")
        }

    }
}


function fnValidarJuradoInforme() {
    if ($("#cboDepartamentoJuradoInforme").val() == "") {
        fnMensaje("error", "Debe Seleccionar un Departamento Académico");
        return false;
    }
    if ($("#cboDocenteJuradoInforme").val() == "") {
        fnMensaje("error", "Debe Seleccionar un Docente");
        return false;
    }
    if ($("#cboRolJuradoInforme").val() == "") {
        fnMensaje("error", "Debe Seleccionar un Rol para el docente");
        return false;
    }
    return true;
}

function fnQuitarJuradoInforme(cod) {
    var tb = '';
    if (JuradoInforme[cod - 1].cod_jur == 0) {
        JuradoInforme.splice(cod - 1, 1);
    } else {
        JuradoInforme[cod - 1].estado = 0;
    }
    for (i = 0; i < JuradoInforme.length; i++) {
        if (JuradoInforme[i].estado == 1) {
            tb += '<tr>';
            tb += '<td style="text-align:center; width:5%">' + (i + 1) + '</td>';
            tb += '<td style="width:55%">' + JuradoInforme[i].nombre + '</td>';
            tb += '<td style="text-align:center; width:20%">' + JuradoInforme[i].destipo + '</td>';
            tb += '<td style="text-align:center; width:10%">' + JuradoInforme[i].aprueba_dir + '</td>';
            tb += '<td style="text-align:center; width:10%">';
            tb += '<button type="button" id="btnDeleteJuradoInforme" name="btnDeleteJuradoInforme" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarJuradoInforme(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
            tb += '</td>';
            tb += '</tr>';
        }
    }
    //console.log(Asesores);
    fnDestroyDataTableDetalle('tJuradoInforme');
    $('#tbJuradoInforme').html(tb);
    fnResetDataTableBasic('tJuradoInforme', 0, 'asc', 10);
}


function fnValidarInforme() {
    if ($("#hdcod").val() == "0") {
        fnMensaje("error", "Debe registrar un proyecto y preinforme.");
        $("#Tab1").trigger("click");
        return false;
    }
    if ($("#txtLinkInforme").val() != "") {
        if ($("#txtLinkInforme").val().indexOf("http://") == -1 && $("#txtLinkInforme").val().indexOf("https://") == -1) {
            fnMensaje("error", "Ingrese una Url correcta por ejemplo: http://www.onedrive.com ó https://www.onedrive.com");
            return false;
        }
    }
    if ($("#file_informe").html() == "" && $("#hdValidaJur").val() != "0") {
        if ($("#fileinforme").val() == '' && $("#txtLinkInforme").val() == '') {
            fnMensaje("error", "Debe Seleccionar un archivo de informe o colocar el link de informe.");
            return false;
        }
    }
    if ($("#fileinforme").val() != "") {
        if ($("#fileinforme")[0].files[0].size > 47185920) {
            fnMensaje("error", "solo se pueden adjuntar archivos de máximo 45MB");
            return false;
        }
    }


    if ($("#fileinforme").val() != '') {
        archivo = $("#fileinforme").val();

        //Extensiones Permitidas
        //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
        extensiones_permitidas = new Array(".pdf", ".rar");
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de informe en formato de PDF y .rar");
            return false;
        }
    }

    /*if (JuradoInforme.length < 3) {
    fnMensaje("error", "Debe seleccionar al menos 3 Jurados de Informe.");
    return false;
    }
    */

    if ($("#filesimilitud").val() != '') {
        if ($("#filesimilitud")[0].files[0].size > 26214400) {
            fnMensaje("error", "solo se pueden adjuntar archivos de máximo 25MB");
            return false;
        }
    }
    if ($("#filesimilitud").val() != '') {
        archivo = $("#filesimilitud").val();
        //Extensiones Permitidas
        //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
        extensiones_permitidas = new Array(".pdf", ".rar");
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de acta en formato de PDF y .rar");
            return false;
        }
    }

    if ($("#fileactainforme").val() != '') {
        if ($("#fileactainforme")[0].files[0].size > 20971520) {
            fnMensaje("error", "solo se pueden adjuntar archivos de máximo 20MB");
            return false;
        }
    }

    if ($("#fileactainforme").val() != '') {
        archivo = $("#fileactainforme").val();
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
            //            fnMensaje("error", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("error", "Solo puede adjuntar archivos de acta en formato de PDF");
            return false;
        }
    }

    var RE = /^\d*(\.\d{1})?\d{0,1}$/;

    if ($("#fileactainforme").val() != "" || $("#file_actainforme").html() != "") {
        if ($("#txtNotaSustentacionI").val() == "") {
            fnMensaje("error", "Ingrese nota de sustentación del Informe.");
            $("#txtNotaSustentacionI").focus()
            return false;
        }
        if ($("#txtNotaSustentacionI").val() != "") {
            if (!RE.test($("#txtNotaSustentacionI").val())) {
                fnMensaje("error", "Ingrese correctanente la nota de Sustentación, puede colocar hasta 2 decimales después del punto decimal.");
                $("#txtNotaSustentacionI").focus()
                return false;
            }
        }
        if ($("#txtNotaSustentacionI").val() > 20) {
            fnMensaje("error", "Ingrese correctamente la nota de Sustentación, Nota máxima de 20.");
            $("#txtNotaSustentacionI").focus()
            return false;
        }
    }
    /*
    if ($("#txtFechaSustentacionI").val() == "" && JuradoInforme.length == 3) {
    fnMensaje("error", "Debe seleccionar una fecha de Sustentación de Informe.");
    $("#txtFechaSustentacionI").focus();
    return false;
    }
    */
    if ($("#hdValidaJur").val() != "0") {
        if ($("#txtNotaSustentacionI").val() != "" && ($("#txtFechaSustentacionI").val() == "" || ($("#fileactainforme").val() == "" && $("#file_actainforme").html() == ""))) {
            fnMensaje("error", "Para colocar nota de sustentación debe colocar fecha de sustentación y adjuntar el acta de Sustentación.");
            return false;
        }

        if ($("#txtFechaSustentacionI").val() != "" && JuradoInforme.length < 2 /*&& $("#hdValidaJur").val()=="I"*/) {
            fnMensaje("error", "Para colocar fecha de sustentación debe asignar los Jurados.");
            return false;
        }
    }
    return true
}

function fnGuardarInforme() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnGuardarJurado($("#hdcod").val(), "I")
        SubirArchivo($("#hdcod").val(), "INFORME");
        SubirArchivo($("#hdcod").val(), "ACTAINFORME");
        SubirArchivo($("#hdcod").val(), "SIMILITUDINFORME");
        fnGuardarLinkInforme($("#hdcod").val())

        if ($("#txtFechaSustentacionI").val() != '' || $("#txtNotaSustentacionI").val() != '') {
            fnAsignarDatosSustentacion($("#hdcod").val(), $("#txtNotaSustentacionI").val(), $("#txtFechaSustentacionI").val());
        }
        if ($("#txtFechaSustentacionI").val() == '') {
            if ($("#txtNotaSustentacionI").val() != '') {
                fnAsignarDatosSustentacion($("#hdcod").val(), $("#txtNotaSustentacionI").val(), $("#txtFechaSustentacionI").val());
            } else {
                fnAsignarDatosSustentacion($("#hdcod").val(), '', $("#txtFechaSustentacionI").val());

            }
        }
        fnLoading(false)
        fnMensaje("success", "Archivo de informe guardado correctamente");
        fnListarAlumnos();
        OcultarRegistro();
        $('#collapseProyecto').collapse({
            toggle: true
        })
        $('#collapseInforme').collapse({
            toggle: true
        })
    } else {
        window.location.href = rpta
    }
}


/* Inicio HCano  30-06-2020 */

function fnGuardarLinkInforme(cod) {
    fnLoading(true)
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: { "hdcod": cod, "action": "ActualizarLinkInforme", "link": $("#txtLinkInforme").val() },
        dataType: "json",
        async: false,
        success: function(data) {
            console.log(data);
            if (data[0].rpta == 1) {
                fnMensaje("success", data[0].msje);
                fnLoading(false);
            } else {
                fnLoading(false);
                fnMensaje("error", "No se pudo actualizar link de informe.")
            }
        },
        error: function(result) {
            fnMensaje("error", "No se pudo actualizar link de informe.")
            fnLoading(false)
        }
    });
    fnLoading(false);
}
/* Fin HCano  30-06-2020 */


function fnAsignarDatosSustentacion(cod, no, fecSustentacion) {
    fnLoading(true)
    rpta = fnvalidaSession()
    if (rpta == true) {
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: { "hdcod": cod, "fec": fecSustentacion, "nota": no, "action": "AsignarDatosSustentacionInforme" },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log("OK");
                fnLoading(false)
            },
            error: function(result) {
                //console.log(result)
                fnLoading(false)

            }
        });
    } else {
        fnLoading(false);
        window.location.href = rpta
    }
}



function SubirArchivo(cod, tipo) {
    fnLoading(true)
    var flag = false;
    try {

        var data = new FormData();
        //data.append("action", ope.Up);
        data.append("action", "SurbirArchivo");
        data.append("codigo", cod);
        data.append("tipo", tipo);

        if (tipo == "PROYECTO") {
            var files = $("#fileproyecto").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }

        if (tipo == "ACTA") {
            var files = $("#fileacta").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }
        if (tipo == "SIMILITUDPROYECTO") {
            var files = $("#filesimilitudProyecto").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }

        if (tipo == "PREINFORME") {
            var files = $("#filepreinforme").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }

        if (tipo == "INFORME") {
            var files = $("#fileinforme").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }

        if (tipo == "ACTAINFORME") {
            var files = $("#fileactainforme").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }

        if (tipo == "SIMILITUDINFORME") {
            var files = $("#filesimilitud").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }

        if (files.length > 0) {
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Tesis.aspx",
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


function fnDownload(id_ar) {
    window.open("DescargarArchivoTesis.aspx?Id=" + id_ar);
}

function GenerarActa(etapa) {

    var bandera = 0;
    var contador = 0;
    if (etapa == 'P') {
        if (Jurado.length < 3) {
            fnMensaje("error", "Debe contar con 3 jurados para generar acta sustentación de proyecto")
            bandera = 1;
        } else {

            for (i = 0; i < Jurado.length; i++) {
                if (Jurado[i].aprueba_dir == 'PENDIENTE') {
                    contador = contador + 1;
                }
            }
            if (contador != 0) {
                fnMensaje("error", "Todos los Jurados deben estar aprobados por director de Escuela para poder generar acta")
                bandera = 1;
            } else {

                for (i = 0; i < Jurado.length; i++) {
                    if (Jurado[i].cod_jur == '0') {
                        contador = contador + 1;
                    }
                }
                if (contador != 0) {
                    fnMensaje("error", "Debe guardar jurados para generar acta sustentación de proyecto")
                    bandera = 1;
                }
            }
        }

    }

    if (etapa == 'I') {
        contador = 0;
        if (JuradoInforme.length < 3) {
            cuenta = 0;
            for (i = 0; i < JuradoInforme.length; i++) {
                if (JuradoInforme[i].destipo == 'JURADO-PRESIDENTE' || JuradoInforme[i].destipo == 'JURADO-SECRETARIO') {
                    cuenta = cuenta + 1;
                }
            }
            if (cuenta < 2) {
                fnMensaje("error", "Debe contar con 3 jurados para generar acta sustentación de Informe o al menos Presidente y Secretario Asignado")
                bandera = 1;
            } else {
                for (i = 0; i < JuradoInforme.length; i++) {
                    if (JuradoInforme[i].aprueba_dir == 'PENDIENTE' && JuradoInforme[i].estado == 1) {
                        contador = contador + 1;
                    }
                }
                if (contador != 0) {
                    fnMensaje("error", "Todos los Jurados deben estar aprobados por director de Escuela para poder generar acta")
                    bandera = 1;
                }
            }
        } else {
            for (i = 0; i < JuradoInforme.length; i++) {
                if (JuradoInforme[i].cod_jur == '0') {
                    contador = contador + 1;
                }
            }
            if (contador != 0) {
                fnMensaje("error", "Debe guardar jurados para generar acta sustentación de proyecto")
                bandera = 1;
            } else {
                for (i = 0; i < JuradoInforme.length; i++) {
                    if (JuradoInforme[i].aprueba_dir == 'PENDIENTE' && JuradoInforme[i].estado == 1) {
                        contador = contador + 1;
                    }
                }
                if (contador != 0) {
                    fnMensaje("error", "Todos los Jurados deben estar aprobados por director de Escuela para poder generar acta")
                    bandera = 1;
                }

            }
        }

    }
    if (bandera == 0) {
        fnLoading(true);
        $('body').append('<form id="frm"></form>');
        $('form#frm').append('<input type="hidden" id="action" name="action" value="GenerarActa" />');
        $('form#frm').append('<input type="hidden" id="param1" name="param1" value="' + $("#hdcod").val() + '" />');
        $('form#frm').append('<input type="hidden" id="param2" name="param2" value="' + $("#hdcodA").val() + '" />');
        $('form#frm').append('<input type="hidden" id="etapa" name="etapa" value="' + etapa + '" />');
        var form = $("#frm").serializeArray();
        $("form#frm").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            //cache: false,
            async: false,
            success: function(data) {
                if (data[0].ok == true) {
                    window.open(data[0].link, "_blank");
                } else {
                    fnMensaje("error", "No se pudo generar acta")
                }
                fnLoading(false);
            },
            error: function(result) {
                //fnMensaje("error", result)
                fnLoading(false);
            }
        });
    }
}

function fnListarOCDE(cod, tipo) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscarAlumno input[id=action]").remove();
        $("form#frmbuscarAlumno input[id=param1]").remove();
        $("form#frmbuscarAlumno input[id=param2]").remove();
        $('form#frmbuscarAlumno').append('<input type="hidden" id="action" name="action" value="lAreaConocimientosOCDE" />');
        $('form#frmbuscarAlumno').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
        $('form#frmbuscarAlumno').append('<input type="hidden" id="param2" name="param2" value="' + tipo + '" />');
        var form = $("#frmbuscarAlumno").serializeArray();
        $("form#frmbuscarAlumno input[id=action]").remove();
        $("form#frmbuscarAlumno input[id=param1]").remove();
        $("form#frmbuscarAlumno input[id=param2]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<option value="' + data[i].codigo + '">' + data[i].descripcion + '</option>';
                    }
                }
                if (tipo == "AR") {
                    $("#cboArea").html(tb);
                }
                if (tipo == "SA") {
                    $("#cboSubArea").html(tb);
                }
                if (tipo == "DI") {
                    $("#cboDisciplina").html(tb);
                }
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


function GuardarAsesor(cod, etapa, cod_per) {
    fnLoading(true)
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: { "hdcod": cod, "abreviatura_eta": etapa, "action": "ActualizarAsesor", "codigo_per": cod_per },
        dataType: "json",
        //cache: false,
        //async: false,
        success: function(data) {
            //console.log("OK");
            fnLoading(false)
        },
        error: function(result) {
            //console.log(result)
            fnLoading(false)

        }
    });
    fnLoading(false);
}


function ActualizarDatosTesis(cod, fecActa) {
    fnLoading(true)
    rpta = fnvalidaSession()
    if (rpta == true) {
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: { "hdcod": cod, "fec": fecActa, "action": "ActualizarDatosTesis" },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log("OK");
                fnLoading(false)
            },
            error: function(result) {
                //console.log(result)
                fnLoading(false)

            }
        });
    } else {
        fnLoading(false);
        window.location.href = rpta
    }
}

function Enviar(cod, alu, tipo, etapa) {
    //tipo:
    //0 ENVIAR
    //1 QUITAR ENVIO
    //ETAPA
    //P: PROYECTO
    //I: INFORME
    fnLoading(true)
    rpta = fnvalidaSession()
    if (rpta == true) {
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: { "hdcod": cod, "hdalu": alu, "tipo": tipo, "etapa": etapa, "action": "EnviarTesis" },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                //console.log("OK");
                fnLoading(false)
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarAlumnos();
                } else {
                    fnLoading(false);
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                //console.log(result)
                fnLoading(false)
            }
        });
    } else {
        fnLoading(false);
        window.location.href = rpta
    }
}

function fnConfirmarEnviar(cod, alu, tipo, etapa) {
    if (tipo == "0") {
        fnMensajeConfirmarEliminar('top', '¿Desea Bloquear edición de Tesis? El alumno no podrá editar la Tesis', 'Enviar', cod, alu, tipo, etapa);
    } else {
        fnMensajeConfirmarEliminar('top', '¿Desea quitar bloqueo de edición de tesis? El alumno podrá editar la Tesis', 'Enviar', cod, alu, tipo, etapa);
    }
}

function fnObservar(cod_tes, cod_alu, etapa) {
    $("#hdoTes").val(cod_tes);
    $("#hdoAlu").val(cod_alu);
    $("#hdoEtapa").val(etapa);
    $("#txtObservacion").val("");
    fnListarObservacionesDocente(cod_tes, etapa)
    $("#mdObservacion").modal("show");
}

function fnRegistrarObservacion() {

    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmObservaciones input[id=action]").remove();
        $("#frmObservaciones").append('<input type="hidden" id="action" name="action" value="RegObservacionDocente" />');
        var form = $("#frmObservaciones").serializeArray();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                fnLoading(false)
                if (data[0].rpta == 1) {
                    fnLoading(false);
                    fnMensaje("success", data[0].msje)
                    $("#txtObservacion").val("");
                    fnListarObservacionesDocente();
                } else {
                    fnLoading(false);
                    fnMensaje("error", data[0].msje)
                }
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

function fnConfirmarRegistroObservacion() {
    if ($("#txtObservacion").val() == "") {
        fnMensaje("error", "Debe ingresar detalle de la observación")
    } else {
        fnMensajeConfirmarEliminar('top', '¿Está seguro que desea guardar la observación?, Se enviará un correo al alumno indicando la observación', 'fnRegistrarObservacion');
    }
}


function fnListarObservacionesDocente() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmObservaciones input[id=action]").remove();
        $("#frmObservaciones").append('<input type="hidden" id="action" name="action" value="lstObservacionDocente" />');
        var form = $("#frmObservaciones").serializeArray();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Tesis.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                fnLoading(false);
                var tb = '';
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center" width="5%">' + (i + 1) + '</td>';
                        tb += '<td width="65%">' + data[i].descripcion + '</td>';
                        tb += '<td style="text-align:center" width="10%">' + data[i].fecha + '</td>';
                        tb += '<td style="text-align:center" width="10%">' + data[i].resuelto + '</td>';
                        //tb += '<td style="text-align:center"></td>';
                        tb += '<td style="text-align:center" width="10%">';
                        tb += '<button type="button" id="btnDeleteObservacion" name="btnDeleteObservacion" class="btn btn-red btn-xs" title="Eliminar" onclick="fnConfirmarEliminarObservacion(\'' + data[i].cod + '\')" ><i class="ion-android-delete"></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tObservacionesDocente');
                $('#tbObservacionesDocente').html(tb);
                fnResetDataTableBasic('tObservacionesDocente', 0, 'asc', 10);
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


function fnConfirmarEliminarObservacion(cod) {
    fnMensajeConfirmarEliminar('top', '¿Desea eliminar observación?', 'fnEliminarObservacion', cod);
}

function fnEliminarObservacion(cod) {
    fnLoading(true)
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Tesis.aspx",
        data: { "hdcod": cod, "action": "EliminarObservacionDocente" },
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            fnLoading(false)
            if (data[0].rpta == 1) {
                fnLoading(false);
                fnMensaje("success", data[0].msje)
                fnListarObservacionesDocente()
            } else {
                fnLoading(false);
                fnMensaje("error", data[0].msje)
            }
        },
        error: function(result) {
        }
    });
    fnLoading(false);

}

function VisualizacionxPerfil(etapa) {
    /* Si es adm o docente de apoyo a tesis, asigna jurado*/
    if (ObtenerValorGET("ctf") == "1" || ObtenerValorGET("ctf") == "144") {
        $("#filaAsignarJurado").show();
        $("#SubirActa").show();
        $("#filaNotaJurado").show();
        $("#filaAsignarJuradoInforme").show();
        $("#SubirActaInforme").show();
        $("#filaNotaJuradoInforme").show()
        $("#btnGuardarTesis").show();
        $("#btnGuardarPreinforme").show();
        $("#btnGuardarInforme").show();
        $("#cboAsesor").removeAttr("disabled");
        $("#filepreinforme").removeAttr("disabled");
        $("#fileinforme").removeAttr("disabled"); ;
        $("#filesimilitud").removeAttr("disabled");

        //if (etapa == "P") {
        $('#frmRegistro').find('input, textarea, button, select').removeAttr('disabled');
        $('#frmObjetivos').find('input, textarea, button, select, table').removeAttr('disabled');
        //}

        //        if (etapa == "E") {
        //            if ($("#hdcod").val() == 0) {
        $("#btnGuardarTesis").attr("style", "display:inline-block");
        /* } else {
        $("#btnGuardarTesis").attr("style", "display:none");
        }
        }*/
        //        if (etapa == "I") {
        //            if ($("#hdcod").val() == 0) {
        $("#btnGuardarTesis").attr("style", "display:inline-block");
        $("#btnGuardarPreinforme").attr("style", "display:inline-block");
        $("#filepreinforme").removeAttr('disabled');
        $("#cboAsesor").removeAttr('disabled');
        /* } else {
        $("#btnGuardarTesis").attr("style", "display:none");
        $("#btnGuardarPreinforme").attr("style", "display:none");
        $("#filepreinforme").attr('disabled', 'disabled');
        $("#cboAsesor").attr('disabled', 'disabled');
        }
        }*/

    } else {
        $("#filaAsignarJurado").hide();
        $("#SubirActa").hide();
        $("#filaNotaJurado").hide();
        $("#filaAsignarJuradoInforme").hide();
        $("#SubirActaInforme").hide();
        $("#filaNotaJuradoInforme").hide();
        $("#btnGuardarTesis").hide();
        $("#btnGuardarPreinforme").hide();
        $("#btnGuardarInforme").hide();
        $('#frmRegistro').find('input, textarea, button, select').attr('disabled', 'disabled');
        $('#frmObjetivos').find('input, textarea, button, select, table').attr('disabled', 'disabled');
        $("#btnObjetivos").removeAttr("disabled");
        $("#btnObservacionesProyecto").removeAttr("disabled");
        $("#btnCancelarProyecto").removeAttr("disabled");
        $("#cboAsesor").attr("disabled", "disabled");
        $("#filepreinforme").attr("disabled", "disabled");
        $("#fileinforme").attr("disabled", "disabled");
        $("#filesimilitud").attr("disabled", "disabled");
    }

}


function fnListarAreaOCDE(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
        $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
        var tb = '<option value="0">--seleccione--</option>';
        if ($("#cboLinea").val() != "") {
            $("form#frmbuscarAlumno input[id=action]").remove();
            $("form#frmbuscarAlumno input[id=cdlinea]").remove();
            $('form#frmbuscarAlumno').append('<input type="hidden" id="action" name="action" value="lstAreaOCDExLineaUSAT" />');
            $('form#frmbuscarAlumno').append('<input type="hidden" id="cdlinea" name="cdlinea" value="' + cod + '" />');
            var form = $("#frmbuscarAlumno").serializeArray();
            $("form#frmbuscarAlumno input[id=action]").remove();
            $("form#frmbuscarAlumno input[id=cdlinea]").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/tesis.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);

                    var filas = data.length;
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<option value="' + data[i].cod + '">' + data[i].des + '</option>';
                        }
                    }
                    $("#cboArea").html(tb);
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });
        } else {
            $("#cboArea").html(tb);
        }
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}

function AnularNotaAsesor(cod_tes, cac,etapa) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (confirm("¿Está seguro que desea anular la nota colocada por el asesor?")) {
            fnLoading(true);
            $('body').append('<form id="frm"></form>');
            $('form#frm').append('<input type="hidden" id="action" name="action" value="AnularNotaAsesor" />');
            $('form#frm').append('<input type="hidden" id="tes" name="tes" value="' + cod_tes + '" />');
            $('form#frm').append('<input type="hidden" id="cac" name="cac" value="' + cac + '" />');
            $('form#frm').append('<input type="hidden" id="etapa" name="etapa" value="' + etapa + '" />');
            var form = $("#frm").serializeArray();
            $("form#frm").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Tesis.aspx",
                data: form,
                dataType: "json",
                //cache: false,
                async: false,
                success: function(data) {
                    if (data[0].rpta == "1") {
                        fnMensaje("success", data[0].msje)
                        fnListarAlumnos();
                    } else {
                        fnMensaje("error", "No se pudo realizar la operación")
                    }
                    fnLoading(false);
                },
                error: function(result) {
                    //fnMensaje("error", result)
                    fnLoading(false);
                }
            });
        }
    } else {
        window.location.href = rpta
    }
}