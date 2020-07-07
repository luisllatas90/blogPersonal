var objetivos = [];
var codobj = "0";
$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    if (rpta == true) {
        var dt = fnCreateDataTableBasic('tConcursos', 0, 'asc', 100);
        var dt = fnCreateDataTableBasic('tGrupo', 0, 'asc', 10);
        var dtO = fnCreateDataTableBasic('tObjetivos', 1, 'asc', 10);
        var dt = fnCreateDataTableBasic('tPostulacion', 0, 'asc', 100);
        fnListarConcurso();
        cLineas();
        fnListarOCDE(0, 'AR');
        fnListarRegion();
    } else {
        window.location.href = rpta
    }
    fnLoading(false);
    $("#btnRegresar").click(function() {
        $("#VerConcurso").attr("style", "display:none");
        $("#ListaConcursos").attr("style", "display:block");
        $("#PanelBusqueda").attr("style", "display:block");
    })

    //    $("#cboGrupo").change(function() {
    //        if ($("#cboGrupo").val() == "") {
    //            fnDestroyDataTableDetalle('tGrupo');
    //            $('#tbGrupo').html('');
    //            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
    //        } else {
    //            fnListarInvestigadorGrupos('GRU');
    //        }
    //    })
    $("#btnObjetivos").click(function() {
        fnLoading(true);
        $("#txtobjetivo").val("");
        $("#cboTipoObjetivo").val("");
        $("#mdObjetivos").modal("show");
        fnLoading(false);
    });

    $("#btnAgregarObjetivo").click(function() {
        fnLoading(true);
        fnAgregarObjetivo();
        codper = "";
        nombre = "";
        $("#txtPersonal").val("");
        $("#cboRol").val("");
        fnLoading(false);
    });


});

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

function fnListarRegion() {
    fnLoading(true)
    $("form#frmbuscar input[id=action]").remove();
    $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="lRegion" />');
    var form = $("#frmbuscar").serializeArray();
    $("form#frmbuscar input[id=action]").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        //            async: false,
        success: function(data) {
            //console.log(data);
            var tb = '';
            var filas = data.length;
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<option value="' + data[i].c_reg + '">' + data[i].d_reg + '</option>';
                }
                $("#cboRegion").html(tb);
            }
        },
        error: function(result) {
            fnMensaje("warning", result)
        }
    });
    fnLoading(false);
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

/*
function fnListarInvestigadorGrupos(tipo) {
fnLoading(true)
$("form#frmbuscar input[id=action]").remove();
$("form#frmbuscar input[id=ctf]").remove();
$("form#frmbuscar input[id=tipo]").remove();
$("form#frmbuscar input[id=codbusq]").remove();
$('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="ListarInvestigadorGrupos" />');
$('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
$('form#frmbuscar').append('<input type="hidden" id="tipo" name="tipo" value="' + tipo + '" />');
if (tipo == "GRU") {
$('form#frmbuscar').append('<input type="hidden" id="codbusq" name="codbusq" value="' + $("#cboGrupo").val() + '" />');
} else {
$('form#frmbuscar').append('<input type="hidden" id="codbusq" name="codbusq" value="' + ObtenerValorGET("ctf") + '" />');
}
var form = $("#frmbuscar").serializeArray();
$("form#frmbuscar input[id=action]").remove();
$("form#frmbuscar input[id=ctf]").remove();
$("form#frmbuscar input[id=tipo]").remove();
$("form#frmbuscar input[id=codbusq]").remove();
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
if (tipo == 'IU' || tipo == 'IM') {
tb += '<option value="" selected>-- Seleccione -- </option>';
for (i = 0; i < filas; i++) {
tb += '<option value="' + data[i].cod + '">' + data[i].nombre + '</option>';
}
$("#cboGrupo").html(tb);
}
}
},
error: function(result) {
fnMensaje("warning", result)
}
});
fnLoading(false);

}*/


function fnListarOCDE(cod, tipo) {
    fnLoading(true)
    $("form#frmbuscar input[id=action]").remove();
    $("form#frmbuscar input[id=param1]").remove();
    $("form#frmbuscar input[id=param2]").remove();
    $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="lAreaConocimientosOCDE" />');
    $('form#frmbuscar').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
    $('form#frmbuscar').append('<input type="hidden" id="param2" name="param2" value="' + tipo + '" />');
    var form = $("#frmbuscar").serializeArray();
    $("form#frmbuscar input[id=action]").remove();
    $("form#frmbuscar input[id=param1]").remove();
    $("form#frmbuscar input[id=param2]").remove();
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
}


function fnListarConcurso() {
    if ($("#cboEstado").val() !== "") {
        //        rpta = fnvalidaSession()
        //        if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=ambito]").remove();
        $("form#frmbuscar input[id=dir]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="ambito" name="ambito" value="0" />');
        $('form#frmbuscar').append('<input type="hidden" id="dir" name="dir" value="%" />');
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
            async: false,
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
                        //if ((data[i].estado == 'REGISTRO' || data[i].estado == 'OBSERVADO') && (ObtenerValorGET("ctf") == 1 || ObtenerValorGET("ctf") == 13 || ObtenerValorGET("ctf") == 65)) {
                        // EN ETAPA DE REGISTRO//OBSERVACION FACULTAD//OBSERVACION DPTO//OBSERVACION COORD. GENERAL APARECE PARA ENVIAR
                        // tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-orange" onclick="fnEnviar(\'' + data[i].cod + '\',99)" title="Enviar a Evaluación" ><i class="ion-arrow-right-a"></i></button>';
                        //}
                        //if (data[i].estado == 'REGISTRO') {
                        //tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="fnEliminar(\'' + data[i].cod + '\')" title="Eliminar" ><i class="ion-close"></i></button>';
                        //}
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tConcursos');
                $('#tbConcursos').html(tb);
                fnResetDataTableBasic('tConcursos', 0, 'asc', 100);
            },
            error: function(result) {
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);

        //        } else {
        //            window.location.href = rpta
        //        }
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
            async: false,
            success: function(data) {
                //console.log(data);
                $("#hdcodCon").val(data[0].cod)
                $("#txttitulo").val(data[0].titulo);
                $('#txtdescripcion').val(data[0].des);
                $("#txtfecini").val(data[0].fecini);
                $("#txtfecfin").val(data[0].fecfin);
                $("#txtfecfineva").val(data[0].fecfineva);
                $("#txtfecres").val(data[0].fecres);
                $("#cbotipo").val(data[0].tipo);
                if (data[0].rutabases != "") {
                    //                    $("#file_Bases").html("<a href='" + data[0].rutabases + "' target='_blank' style='font-weight:bold'>Descargar Bases</a>")
                    $("#file_Bases").html('<a onclick="fnDownload(\'' + data[0].rutabases + '\')" style="font-weight:bold">Descargar Bases</a>')
                } else {
                    $("#file_Bases").html("");
                }
                $("#mensaje").html("");
                if (data[0].cerrado == '1') {
                    $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) el Plazo para Postular al Concurso Culminó.</label>')
                    //                    $("#btnPostular").attr("style", "display:none");
                } else {
                    $("#mensaje").html('<label class="col-sm-12 control-label text-danger" style="font-weight: bold">(*) El Concurso se Encuentra Abierto para Postulación.</label>')
                    //                    $("#btnPostular").attr("style", "display:inline-block");
                }
                $("#VerConcurso").attr("style", "display:block");
                $("#PanelBusqueda").attr("style", "display:none");
                $("#ListaConcursos").attr("style", "display:none");
                fnListarPostulacion(data[0].cod, data[0].cerrado);

                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
                //console.log(result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}

function fnCancelar() {
    $("#mdRegistro").modal("hide");
    //fnMensajeConfirmarEliminar('top', '¿Desea Salir Sin Guardar Cambios.?', 'fnCerrarModal', '');
}

function fnAgregarObjetivo() {
    var value;
    var tb = '';
    var rowCount = $('#tbObjetivos tr').length;
    var repite = false;
    var msje = '';
    if (fnValidarObjetivo() == true) {
        for (i = 0; i < objetivos.length; i++) {
            if (objetivos[i].descripcion == $("#txtobjetivo").val()) {
                repite = true;
                msje = "El Objetivo ya se encuentra Registrado.";
            }
        }
        if ($("#cboTipoObjetivo option:selected").text() == "GENERAL") {
            for (i = 0; i < objetivos.length; i++) {
                if (objetivos[i].tipo == "GENERAL") {
                    repite = true;
                    msje = "El Proyecto solo puede contener un Objetivo General.";
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
                tipo: $("#cboTipoObjetivo option:selected").text()
            });

            //console.log(detalles);

            for (i = 0; i < objetivos.length; i++) {
                if (objetivos[i].tipo == "GENERAL") {
                    tb += '<tr style="font-weight:bold;color:green;">';
                } else {
                    tb += '<tr>';
                }
                //                tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                tb += '<td>' + objetivos[i].descripcion + '</td>';
                tb += '<td>' + objetivos[i].tipo + '</td>';
                //tb += '<td style="text-align:center"></td>';
                tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
                tb += '</tr>';
            }
            fnDestroyDataTableDetalle('tObjetivos');

            $('#tbObjetivos').html(tb);
            fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);

        } else {
            fnMensaje("warning", msje)
        }

    }
}

function fnValidarObjetivo() {
    if ($("#txtobjetivo").val() == "") {
        fnMensaje("warning", "Ingrese Descripción de Objetivo");
        return false;
    }
    if ($("#cboTipoObjetivo").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Tipo de Objetivo");
        return false;
    }
    return true;
}


function fnQuitarObjetivo(cod) {
    var tb = '';
    //console.log(cod);
    objetivos.splice(cod - 1, 1);
    for (i = 0; i < objetivos.length; i++) {

        if (objetivos[i].tipo == "GENERAL") {
            tb += '<tr style="font-weight:bold;color:green;">';
        } else {
            tb += '<tr>';
        }
        tb += '<td>' + objetivos[i].descripcion + '</td>';
        tb += '<td>' + objetivos[i].tipo + '</td>';
        tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
        tb += '</tr>';
    }
    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html(tb);
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);
}



function fnListarObjetivos(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lob + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
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
                        tb += '<td>' + data[i].des + '</td>';
                        tb += '<td>' + data[i].tipo + '</td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
                        tb += '</tr>';
                        objetivos.push({
                            cod: data[i].cod,
                            descripcion: data[i].des,
                            codtipo: data[i].codtipo,
                            tipo: data[i].tipo
                        });

                    }
                }
                //console.log(objetivos);
                fnDestroyDataTableDetalle('tObjetivos');
                $('#tbObjetivos').html(tb);
                fnCreateDataTableBasic('tObjetivos', 1, 'desc', 10);

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


function fnValidarPostulacionTab1() {
    if ($("#txttituloPos").val() == "") {
        fnMensaje("warning", "Ingrese Titulo de Postulación");
        return false;
    }
    if ($("#cboLinea").val() == "") {
        fnMensaje("warning", "Debe Seleccionar la Linea de Investigación");
        return false;
    }
    if ($("#chkOCDE").is(':checked')) {
        if ($("#cboArea").val() == "0") {
            fnMensaje("warning", "Debe Seleccionar Una Área de Investigación OCDE");
            return false;
        }
        if ($("#cboSubArea").val() == "0") {
            fnMensaje("warning", "Debe Seleccionar una Sub Área de Investigación OCDE");
            return false;
        }
        if ($("#cboDisciplina").val() == "0") {
            fnMensaje("warning", "Debe Seleccionar una Disciplina de Investigación OCDE");
            return false;
        }
    }
    if ($("#cboRegion").val() == "0") {
        fnMensaje("warning", "Debe Seleccionar la Región");
        return false;
    }
    return true;
}


function fnValidarPostulacionTab2() {
    /*  if ($("#rbtipoparticipante2").is(':checked')) {
    if ($("#cboGrupo").val() == "") {
    fnMensaje("warning", "Debe Seleccionar un Grupo de Investigación");
    return false;
    }
    }*/
    return true;
}

function fnValidarPostulacionTab3() {
    if ($("#txtresumen").val() == "") {
        fnMensaje("warning", "Ingrese Resumen de Propuesta");
        return false;
    }
    // Verificar Objetivos, un general como nínimo
    var banderaG = 0;
    for (i = 0; i < objetivos.length; i++) {
        if (objetivos[i].tipo == 'GENERAL') {
            banderaG = banderaG + 1;
        }
    }
    if (banderaG == 0) {
        fnMensaje("warning", "Postulación Debe Contar con un Objetivo General como mínimo.");
        return false;
    }

    if ($("#txtpalabras").val() == "") {
        fnMensaje("warning", 'Ingrese Palabras clave, Separadas por coma(",")');
        return false;
    }
    /*
    if ($("#file_pto").val() == '') {
    fnMensaje("warning", 'Adjuntar Archivo de Presupuesto en Formato Excel(.xls o .xlsx)');
    return false;
    }
    if ($("#file_pto").val() != '') {
    archivo = $("#file_pto").val();
    //Extensiones Permitidas
    //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
    extensiones_permitidas = new Array(".xls", ".xlsx");
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
    fnMensaje("warning", "Solo puede Adjuntar Archivos de Presupuesto en Formato de Excel(.xls o .xlsx)");
    return false;
    }
    }
    if ($("#file_cronograma").val() == '') {
    fnMensaje("warning", 'Adjuntar Archivo de Cronograma en Formato de Excel(.xls o .xlsx)');
    return false;
    }
    if ($("#file_cronograma").val() != '') {
    archivo = $("#file_cronograma").val();
    //Extensiones Permitidas
    //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
    extensiones_permitidas = new Array(".xls", ".xlsx");
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
    fnMensaje("warning", "Solo puede Adjuntar Archivos de Cronograma en Formato de Excel(.xls o .xlsx)");
    return false;
    }
    }*/
    if ($("#file_producto").val() == '') {
        fnMensaje("warning", 'Adjuntar Archivo de Informe Informe en Formato de PDF');
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
            fnMensaje("warning", "Solo puede Adjuntar Archivo de Informe en Formato de PDF");
            return false;
        }
    }
    /*
    if ($("#file_declaracion").val() == '') {
    fnMensaje("warning", 'Adjuntar Archivo de Declaracion Jurada en Formato de PD');
    return false;
    }
    if ($("#file_declaracion").val() != '') {
    archivo = $("#file_declaracion").val();
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
    fnMensaje("warning", "Solo puede Adjuntar Declaracion Jurada en Formato de PDF");
    return false;
    }
    }*/
    return true;
}

function fnvalidarTabs() {
    if (fnValidarPostulacionTab1() == false) {
        $("#tab1").trigger("click");
        return false;
    }
    if (fnValidarPostulacionTab2() == false) {
        $("#tab2").trigger("click");
        return false;
    }
    if (fnValidarPostulacionTab3() == false) {
        $("#tab3").trigger("click");
        return false;
    }
    return true
}



function fnLimpiarPostulacion() {
    $("#hdcodPos").val("0");
    //    $("#hdcodCon").val("0");
    $("#txtalumno").val("");
    $("#txttituloPos").val("");
    $("#cboLinea").val("");
    $("#chkOCDE").prop("checked", false);
    $("#ocde").removeAttr("style");
    $("#ocde").attr("style", "display:none");
    $("#cboArea").val("0");
    $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
    $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
    $("#txtresumen").val("");
    $("#txtpalabras").val("");
    $("#txtjustificacion").val("");
    $("#txtfeciniPos").val("");
    $("#txtfecfinPos").val("");
    objetivos = [];
    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html("");
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);
    equipo = [];
    fnDestroyDataTableDetalle('tGrupo');
    $('#tbGrupo').html('');
    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
    //$("#file_resultados").val("");
    //$("#file_pto").val("");
    //$("#file_cronograma").val("");
    $("#file_producto").val("");
    //$("#file_declaracion").val("");
    $("#tab1").trigger("click");
    //    $("#btnA").removeAttr("style")
    //    $("#btnA").attr("style", "display:inline-block")
    $("#chkOCDE").removeAttr('disabled');
    /*
    $("#rbtipoparticipante1").removeAttr('disabled');
    $("#rbtipoparticipante2").removeAttr('disabled');
    $("#cboGrupo").removeAttr('disabled');
    */
    $("#cboArea").removeAttr('disabled');
    $("#cboSubArea").removeAttr('disabled');
    $("#cboDisciplina").removeAttr('disabled');
    $("#cboRegion").removeAttr('disabled');
    $("#btnAgregarObjetivo").removeAttr('disabled');
    $("#divalumno").attr("style", "display:none")
    $("#cboRegion").val("0");
    $("#cboProvincia").html("<option value='0'>-- Seleccione --</option>");
    $("#cboDistrito").html("<option value='0'>-- Seleccione --</option>");
    $("#txtLugar").val();
    $("#txtfeciniPos").removeAttr('disabled');
    $("#txtfecfinPos").removeAttr('disabled');
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
                        tb += '<td style="text-align:center;width:15%">' + data[i].des_etapa + '</td>';
                        tb += '<td style="text-align:center;width:5%">';
                        tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnVerPostulacion(\'' + data[i].cod + '\')" title="Ver" ><i class="ion-eye"></i></button>';
                        tb += '</td>';
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

function fnVerPostulacion(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
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
            async: false,
            success: function(data) {
                //                console.log(data);
                $("#hdcodPos").val(cod)
                if (data.length > 0) {
                    if (data[0].es_alumno == 1) {
                        $("#txttituloPosA").val(data[0].titulo);
                        $("#txtAsesor").val(data[0].asesor);
                        $("#txtFacultad").val(data[0].facultad);
                        $("#txtEscuela").val(data[0].cpf);
                        $("#txtDNI").val(data[0].nrodoc);
                        $("#txtFechaNac").val(data[0].fechanac);
                        $("#txtEmail").val(data[0].email);
                        $("#txtFijo").val(data[0].telfijo);
                        $("#tabEstudiante").attr("style", "display:table-cell");
                        $("#tabLI1").attr("style", "display:none");
                        //$("#cboGrupo").attr("style", "display:none")
                        $("#A1").trigger("click");
                    } else {
                        $("#tabEstudiante").attr("style", "display:none");
                        $("#tabLI1").attr("style", "display:table-cell");
                        $("#tab1").trigger("click");

                        $("#txttituloPos").val(data[0].titulo);
                        $('#cboLinea').val(data[0].linea);
                        if (data[0].cod_dis != "0") {
                            $("#chkOCDE").prop('checked', true);
                            $("#ocde").attr("style", "display:block");
                            $("#cboArea").val(data[0].cod_area)
                            fnListarOCDE($("#cboArea").val(), 'SA');
                            $("#cboSubArea").val(data[0].cod_sub)
                            fnListarOCDE($("#cboSubArea").val(), 'DI');
                            $("#cboDisciplina").val(data[0].cod_dis)
                            $("#cboArea").attr('disabled', 'disabled');
                            $("#cboSubArea").attr('disabled', 'disabled');
                            $("#cboDisciplina").attr('disabled', 'disabled');
                        } else {
                            $("#chkOCDE").prop('checked', false);
                            $("#ocde").removeAttr("style");
                            $("#ocde").attr("style", "display:none");
                        }
                        $("#cboRegion").val(data[0].region);
                        $("#cboRegion").attr('disabled', 'disabled');
                        fnListarProvincia(data[0].region)
                        $("#cboProvincia").val(data[0].provincia);
                        $("#cboProvincia").attr('disabled', 'disabled');
                        fnListarDistrito(data[0].provincia)
                        $("#cboDistrito").val(data[0].distrito);
                        $("#cboDistrito").attr('disabled', 'disabled');
                        $("#txtLugar").val(data[0].lugar)
                        $("#txtLugar").attr('disabled', 'disabled');

                    }
                    /*
                    if (data[0].tipo_gru == 0) {
                    $("#rbIndividual").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:none")
                    $("#rbGrupoM").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:none")
                    $("#rbtipoparticipante1").prop("checked", true);
                    $("#rbtipoparticipante2").prop("checked", false);
                    $("#rbtipoparticipante3").prop("checked", false);

                    }
                    if (data[0].tipo_gru == 1) {
                    $("#rbIndividual").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:block")
                    $("#rbGrupoM").attr("style", "display:none")
                    $("#rbtipoparticipante2").prop("checked", true);
                    $("#rbtipoparticipante1").prop("checked", false);
                    $("#rbtipoparticipante3").prop("checked", false);
                    fnListarInvestigadorGrupos('IU');
                    $("#cboGrupo").attr("style:display:block");
                    $("#cboGrupo").val(data[0].gru)
                    }
                    if (data[0].tipo_gru == 2) {
                    $("#rbIndividual").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:none")
                    $("#rbGrupoM").attr("style", "display:block")
                    $("#rbtipoparticipante3").prop("checked", true);
                    $("#rbtipoparticipante1").prop("checked", false);
                    $("#rbtipoparticipante2").prop("checked", false);
                    fnListarInvestigadorGrupos('IM');
                    $("#cboGrupo").attr("style:display:block")
                    $("#cboGrupo").val(data[0].gru)
                    }*/
                    fnListarEquipo(cod)

                    $("#txtresumen").val(data[0].resumen);
                    $("#txtpalabras").val(data[0].palabras);
                    $("#txtjustificacion").val(data[0].justificacion);
                    $("#txtfeciniPos").val(data[0].fechaini);
                    $("#txtfecfinPos").val(data[0].fechafin);
                    /*
                    if (data[0].rutaresultados != "") {
                    $("#resultados").html("<a onclick='fnDownload(\"" + data[0].rutaresultados + "\")' style='font-weight:bold'>Descargar Resultados Esperados</a>")
                    } else {
                    $("#resultados").html("");
                    }
                    if (data[0].rutapto != "") {
                    $("#pto").html("<a onclick='fnDownload(\"" + data[0].rutapto + "\")' style='font-weight:bold'>Descargar Presupuesto</a>")
                    } else {
                    $("#pto").html("");
                    }
                    if (data[0].rutacronograma != "") {
                    $("#cronograma").html("<a onclick='fnDownload(\"" + data[0].rutacronograma + "\")' style='font-weight:bold'>Descargar Cronograma</a>")
                    } else {
                    $("#cronograma").html("");
                    }
                    */
                    if (data[0].rutainforme != "") {
                        $("#producto").html("<a onclick='fnDownload(\"" + data[0].rutainforme + "\")' style='font-weight:bold'>Descargar Propuesta</a>")
                    } else {
                        $("#producto").html("");
                    }
                    /*
                    if (data[0].rutadeclaracion != "") {
                    $("#declaracion").html("<a onclick='fnDownload(\"" + data[0].rutadeclaracion + "\")' style='font-weight:bold'>Descargar Declaración</a>")
                    } else {
                    $("#declaracion").html("");
                    }*/

                    $("#btnA").attr("style", "display:none")

                    $("#chkOCDE").attr('disabled', 'disabled');
                    /*$("#rbtipoparticipante1").attr('disabled', 'disabled');
                    $("#rbtipoparticipante2").attr('disabled', 'disabled');
                    $("#cboGrupo").attr('disabled', 'disabled');
                    */
                    fnListarObjetivos(cod)
                    $("#btnAgregarObjetivo").attr('disabled', 'disabled');
                    $("#txtfeciniPos").attr('disabled', 'disabled');
                    $("#txtfecfinPos").attr('disabled', 'disabled');
                    
                    $("#mdRegistro").modal("show");
                }
                if (data[0].etapa == 1) {
                    $("#Evaluacion").attr("style", "display:block;text-align:right")
                } else {
                    $("#Evaluacion").attr("style", "display:none")
                }

                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
                //console.log(result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}

function fnListarObjetivos(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcodPost]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lob + '" />');
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
                objetivos = [];
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        if (data[i].tipo == "GENERAL") {
                            tb += '<tr style="font-weight:bold;color:green;">';
                        } else {
                            tb += '<tr>';
                        }
                        tb += '<td style="width:70%">' + data[i].des + '</td>';
                        tb += '<td style="width:20%">' + data[i].tipo + '</td>';
                        tb += '<td style="text-align:center;width:10%"></td>';
                        tb += '</tr>';
                        objetivos.push({
                            cod: data[i].cod,
                            descripcion: data[i].des,
                            codtipo: data[i].codtipo,
                            tipo: data[i].tipo
                        });

                    }
                }
                //console.log(objetivos);
                fnDestroyDataTableDetalle('tObjetivos');
                $('#tbObjetivos').html(tb);
                fnCreateDataTableBasic('tObjetivos', 1, 'desc', 10);

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

function fnEvaluar(codigo_Eta, tipo) {
    var mensaje;
    if (tipo == 'R') {
        mensaje = '¿Desea Rechazar Postulación.?';
    }
    if (tipo == 'A') {
        mensaje = '¿Desea Aprobar Postulación.?';
    }
    fnMensajeConfirmarEliminar('top', mensaje, 'fnActualizarEtapa', codigo_Eta);
}


function fnActualizarEtapa(val) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmRegistro input[id=ctf]").remove();
        $("form#frmRegistro input[id=action]").remove();
        $("form#frmRegistro input[id=cod_etapa]").remove();
        $('form#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.ace + '" />');
        $('form#frmRegistro').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        $('form#frmRegistro').append('<input type="hidden" id="cod_etapa" name="cod_etapa" value="' + val + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=ctf]").remove();
        $("form#frmRegistro input[id=action]").remove();
        $("form#frmRegistro input[id=cod_etapa]").remove();
        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarPostulacion(data[0].cod, data[0].cerrado);
                    $("#mdRegistro").modal("hide");
                    fnLoading(false);
                } else {
                    fnMensaje("error", data[0].msje)
                }
                fnLoading(false);
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
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center;width:3%">' + (i + 1) + '</td>';
                        tb += '<td style="width:37%">' + data[i].nombre + '</td>';
                        if (data[i].dina != '-') {
                            tb += '<td style="width:28%"><a href="' + data[i].dina + '" target="_blank">' + data[i].dina.substr(0, 70) + '</a></td>';
                        } else {
                            tb += '<td style="width:28%">-</td>';
                        }
                        //tb += '<td style="width:28%">' + data[i].dina + '</td>';
                        tb += '<td style="width:28%">' + data[i].rol + '</td>';
                        tb += '<td style="width:4%">' + data[i].dedicacion + '</td>';
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
}
*/

function fnDownload(id_ar) {
    window.open("DescargarArchivo.aspx?Id=" + id_ar);
}


function downloadWithName(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
    // alert(link);
}