var objetivos = [];
var equipo = [];
var codobj = "0";
$(document).ready(function() {
    var dt = fnCreateDataTableBasic('tConcursos', 0, 'asc', 100);
    var dt1 = fnCreateDataTableBasic('tGrupo', 0, 'asc', 10);
    var dtO = fnCreateDataTableBasic('tObjetivos', 1, 'asc', 10);
    var dt2 = fnCreateDataTableBasic('tPostulacion', 0, 'asc', 100);
    //    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    if (rpta == true) {
        fnListarConcurso();
        cLineas();
        fnListarOCDE(0, 'AR');
        fnListarRegion();
        fnAutoCDocente();
        //fnAutoCAlumno();
        //        fnLoading(false);
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
        if ($("#cboAmbito option:selected").text() == "INTERNO") {

            $("#archivo2").attr("style", "display:block");
            $("#archivo3").attr("style", "display:block");
            $("#archivo4").attr("style", "display:block");
            $("#archivo5").attr("style", "display:block");
        } else {
            $("#archivo2").attr("style", "display:none");
            $("#archivo3").attr("style", "display:none");
            $("#archivo4").attr("style", "display:none");
            $("#archivo5").attr("style", "display:none");
        }
        if ($("#cbotipo option:selected").text() == "INDIVIDUAL") {
            //$("#cboGrupo").val("");
            $("#rbGrupo").attr("style", "display:none")
            $("#rbIndividual").attr("style", "display:block")
            $("#rbtipoparticipante1").prop("checked", true)
            $("#rbtipoparticipante2").prop("checked", false)
            $("#rbtipoparticipante3").prop("checked", false)
            //$("#divalumno").attr("style", "display:none")
            $("#divDocente").attr("style", "display:none")

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
            //$("#divalumno").attr("style", "display:none")
            $("#divDocente").attr("style", "display:none")
            fnDestroyDataTableDetalle('tGrupo');
            $('#tbGrupo').html('');
            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
            //fnListarInvestigadorGrupos('IU');
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
            // $("#divalumno").attr("style", "display:block")
            $("#divDocente").attr("style", "display:block")
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
            //$("#divalumno").attr("style", "display:block")
            $("#divDocente").attr("style", "display:none")
        }
        fnConsultarDatosDocente();
        fnLoading(false);
        $("#mdRegistro").modal("show");
    })
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
    });
    $('#chkOCDE').click(function() {
        $("#cboArea").val("0");
        $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
        $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
        if ($(this).is(':checked')) {
            $("#ocde").attr("style", "display:block");
        } else {
            $("#ocde").removeAttr("style");
            $("#ocde").attr("style", "display:none");
        }
    });
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
    })*/
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
        $("#txtobjetivo").val("");
        $("#cboRol").val("");
        fnLoading(false);
    });

    $("#cboRegion").change(function() {
        if ($(this).val() != 0) {
            fnListarProvincia($(this).val());
        } else {
            $("#cboProvincia").html("<option value='0'>-- Seleccione --</option>");
        }
        $("#cboDistrito").html("<option value='0'>-- Seleccione --</option>");
    })

    $("#cboProvincia").change(function() {
        if ($(this).val() != 0) {
            fnListarDistrito($(this).val());
        } else {
            $("#cboDistrito").html("<option value='0'>-- Seleccione --</option>");
        }
    });


    $("#rbtipoparticipante1").change(function() {
        if ($(this).is(':checked')) {
            equipo = [];
            fnConsultarDatosDocente();
            $("#divDocente").hide();
        }
    })

    $("#rbtipoparticipante3").change(function() {
        if ($(this).is(':checked')) {
            equipo = [];
            $("#divDocente").show();
            fnConsultarDatosDocente();
        }

    })
});

/*
var cod = ""
var nombre = ""
function fnAutoCAlumno() {
jsonString = fnAlumnosTesis('%');
$('#txtalumno').autocomplete({
source: $.map(jsonString, function(item) {
return item.nombre;
}),
select: function(event, ui) {
var selectecItem = jsonString.filter(function(value) {
return value.nombre == ui.item.value;
});
cod = selectecItem[0].cod;
nombre = selectecItem[0].nombre;
$('#PanelEvento').hide("fade");
},
minLength: 1,
delay: 500
});

$('#txtalumno').keyup(function() {
var l = parseInt($(this).val().length);
if (l == 0) {
cod = "";
nombre = "";
}
});
}
*/

var cod = ""
var nombre = ""
function fnAutoCDocente() {
    jsonString = fnDocentesxDepartamento('%');
    $('#txtDocente').autocomplete({
        source: $.map(jsonString, function(item) {
            return item.nombre;
        }),
        select: function(event, ui) {
            var selectecItem = jsonString.filter(function(value) {
                return value.nombre == ui.item.value;
            });
            cod = selectecItem[0].cod;
            nombre = selectecItem[0].nombre;
            $('#PanelEvento').hide("fade");
        },
        minLength: 1,
        delay: 500
    });

    $('#txtDocente').keyup(function() {
        var l = parseInt($(this).val().length);
        if (l == 0) {
            cod = "";
            nombre = "";
        }
    });
}

function fnAgregarDocente() {
    var value;
    var tb = '';
    var rowCount = $('#tbGrupo tr').length;
    //    console.log(rowCount);
    var repite = false;
    //console.log(equipo);
    if (fnValidarIntegrante() == true) {
        for (i = 0; i < equipo.length; i++) {
            if (equipo[i].cod_per == cod && equipo[i].estado == 1) {
                repite = true;
            }
        }
        //console.log(repite);
        if (repite == false) {
            if (rowCount > 0) {
                tb = $("#tbGrupo").html();
                if (rowCount == 1) {
                    if ($('#tbGrupo tr td').html() == 'No se ha encontrado informacion') {
                        tb = ''
                    }
                }
            } else {
                tb = '';
            }
            var arr = fnRolInvestigador('D');
            var n = arr.length;
            var str = "";
            str += '<option value="">-- Seleccione -- </option>';
            for (j = 0; j < n; j++) {
                str += '<option value="' + arr[j].cod + '">' + arr[j].nombre.toUpperCase() + '</option>';
            }

            var num = equipo.length
            tb += '<tr id="row' + i + '">';
            tb += '<td style="text-align:center;width:3%">' + (num + 1) + '</td>';
            tb += '<td style="width:39%">' + nombre + '</td>';
            tb += '<td style="width:28%">-</td>';
            tb += '<td style="width:30%"><select id="cboRol' + num + '" name="cboRol' + num + '" class="form-control" style="font-size:13px;" onchange="fnCambiaRol(' + num + ')" >' + str + '</select></td>';
            //tb += '<td style="width:4%"><input type="text" id="txtdedicacion' + i + '" name="txtdedicacion' + i + '" class="form-control" style="text-align:right" value="" /></td>';
            tb += '<td style="text-align:center">';
            tb += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarIntegrante(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
            tb += '</td>';
            tb += '</tr>';
            fnDestroyDataTableDetalle('tGrupo');
            $('#tbGrupo').html(tb);
            fnResetDataTableBasic('tGrupo', 0, 'asc', 10);

            equipo.push({
                cod_inv: 0,
                cod_per: cod,
                cod_alu: 0,
                nom_inv: nombre,
                dina_inv: '',
                cod_rol: $("#cboRol" + num + " option:selected").val(),
                dedicacion: '0',
                estado: 1
            });

            cod = "";
            nombre = "";
            $("#txtDocente").val("");

        } else {
            fnMensaje("warning", "El Docente ya ha sido ingresado")
        }

    }
}

function fnValidarIntegrante() {
    if ($("#txtDocente").val() == "" || cod == "") {
        fnMensaje("warning", "Debe Seleccionar un Docente");
        return false;
    }
    return true;
}



function fnQuitarIntegrante(cod) {
    var tb = '';
    equipo[cod - 1].estado = 0;
    $("#row" + (cod - 1)).remove();
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



function fnListarRegion() {
    //    fnLoading(true)
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
        //async: false,
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
    //    fnLoading(false);
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
rpta = fnvalidaSession();
if (rpta == true) {
fnLoading(true)
$("form#frmbuscar input[id=action]").remove();
$("form#frmbuscar input[id=ctf]").remove();
$("form#frmbuscar input[id=tipo]").remove();
$("form#frmbuscar input[id=codbusq]").remove();
$('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="ListarInvestigadorGrupos" />');
$('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
$('form#frmbuscar').append('<input type="hidden" id="tipo" name="tipo" value="' + tipo + '" />');
if (tipo == "GRU") {
//$('form#frmbuscar').append('<input type="hidden" id="codbusq" name="codbusq" value="' + $("#cboGrupo").val() + '" />');
$('form#frmbuscar').append('<input type="hidden" id="codbusq" name="codbusq" value="0" />');
} else {
$('form#frmbuscar').append('<input type="hidden" id="codbusq" name="codbusq" value="0" />');
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

var arr = fnRolInvestigador();
if (tipo == 'INV') {
//console.log(arr)
var n = arr.length;
var str = "";
str += '<option value="" selected>-- Seleccione -- </option>';
for (j = 0; j < n; j++) {
str += '<option value="' + arr[j].cod + '">' + arr[j].nombre.toUpperCase() + '</option>';
}
for (i = 0; i < filas; i++) {
tb += '<tr>';
tb += '<td style="text-align:center;width:3%">' + (i + 1) + "" + '</td>';
tb += '<td style="width:37%">' + data[i].nombre + '</td>';
tb += '<td style="width:28%">' + data[i].dina + '</td>';
tb += '<td style="width:28%"><select id="cboRol' + i + '" name="cboRol' + i + '" class="form-control" style="font-size:13px;" onchange="fnCambiaRol(' + i + ')" >' + str + '</select></td>';
tb += '<td style="width:4%"><input type="text" id="txtdedicacion' + i + '" name="txtdedicacion' + i + '" class="form-control" style="text-align:right" value="" /></td>';
tb += '</tr>';

equipo.push({
cod_inv: data[i].cod,
cod_alu: 0,
nom_inv: data[i].nombre,
dina: data[i].dina,
cod_rol: '',
dedicacion: ''
});
}

fnDestroyDataTableDetalle('tGrupo');
$('#tbGrupo').html(tb);
fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
}
if (tipo == 'GRU') {

//console.log(arr)
var n = arr.length;
var str = "";
str += '<option value="" selected>-- Seleccione -- </option>';
for (j = 0; j < n; j++) {
str += '<option value="' + arr[j].cod + '">' + arr[j].nombre.toUpperCase() + '</option>';
}
for (i = 0; i < filas; i++) {
tb += '<tr>';
tb += '<td style="text-align:center;width:4%">' + (i + 1) + "" + '</td>';
tb += '<td style="width:37%">' + data[i].nombre + '</td>';
tb += '<td style="width:28%">' + data[i].dina + '</td>';
tb += '<td style="width:28%"><select id="cboRol' + i + '" name="cboRol' + i + '" class="form-control" style="font-size:13px;" onchange="fnCambiaRol(' + i + ')" >' + str + '</select></td>';
tb += '<td style="width:4%"><input type="text" id="txtdedicacion' + i + '" name="txtdedicacion' + i + '" class="form-control" style="text-align:right" value="" /></td>';
tb += '</tr>';

equipo.push({
cod_inv: data[i].cod,
cod_alu: 0,
nom_inv: data[i].nombre,
dina: data[i].dina,
cod_rol: '',
dedicacion: ''
});
}
fnDestroyDataTableDetalle('tGrupo');
$('#tbGrupo').html(tb);
fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
}
// console.log(tb)
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
*/

function fnCambiaRol(pos) {
    equipo[pos].cod_rol = $("#cboRol" + pos).val();
    $("#cboRol" + pos + " option").each(function() {
        $(this).removeAttr("selected");
    });
    $("#cboRol" + pos + " option[value=\'" + equipo[pos].cod_rol + "\']").attr("selected", true);
    $("#cboRol" + pos).val(equipo[pos].cod_rol);
}

function fnListarOCDE(cod, tipo) {
    //    fnLoading(true)
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
            //            fnLoading(false);
        },
        error: function(result) {
            fnMensaje("warning", result)
            //            fnLoading(false);
        }
    });

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
            $('form#frmbuscar').append('<input type="hidden" id="ambito" name="ambito" value="0" />');
            $('form#frmbuscar').append('<input type="hidden" id="dir" name="dir" value="2" />');
            var form = $("#frmbuscar").serializeArray();
            $("form#frmbuscar input[id=action]").remove();
            $("form#frmbuscar input[id=ctf]").remove();
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
                tb += '<td style="width:70%">' + objetivos[i].descripcion + '</td>';
                tb += '<td style="width:20%">' + objetivos[i].tipo + '</td>';
                //tb += '<td style="text-align:center"></td>';
                tb += '<td style="text-align:center;width:10%"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
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
        tb += '<td style="width:70%">' + objetivos[i].descripcion + '</td>';
        tb += '<td style="width:20%">' + objetivos[i].tipo + '</td>';
        tb += '<td style="text-align:center;width:10%"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
        tb += '</tr>';
    }
    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html(tb);
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);
}

function fnConsultarDatosDocente() {
    rpta = fnvalidaSession();
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="ldatosDocInv" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                var arr = fnRolInvestigador('D');
                var n = arr.length;
                var str = "";
                var seleccion = 0;
                str += '<option value="" selected>-- Seleccione -- </option>';
                for (j = 0; j < n; j++) {
                    if (arr[j].nombre.toUpperCase() == "INVESTIGADOR PRINCIPAL") {
                        seleccion = arr[j].cod;
                    }
                    str += '<option value="' + arr[j].cod + '">' + arr[j].nombre.toUpperCase() + '</option>';

                }

                var tb = '';
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr id="row' + i + '">';
                        tb += '<td style="text-align:center;width:3%">' + (i + 1) + "" + '</td>';
                        tb += '<td style="width:39%">' + data[i].nombre_per + '</td>';
                        tb += '<td style="width:28%">' + data[i].urldina_inv + '</td>';
                        tb += '<td style="width:30%"><select id="cboRol' + i + '" name="cboRol' + i + '" class="form-control" style="font-size:13px;" onchange="fnCambiaRol(' + i + ')" disabled="disabled" >' + str + '</select></td>';
                        //                        tb += '<td style="width:4%"><input type="text" id="txtdedicacion' + i + '" name="txtdedicacion' + i + '" class="form-control" style="text-align:right" value="" /></td>';
                        tb += '<td></td>'
                        tb += '</tr>';

                        equipo.push({
                            cod_inv: data[i].cod_inv,
                            cod_per: data[i].cod_per,
                            cod_alu: 0,
                            nom_inv: data[i].nombre_per,
                            dina: data[i].urldina_inv,
                            cod_rol: seleccion,
                            dedicacion: '0',
                            estado: 1
                        });
                    }

                    fnDestroyDataTableDetalle('tGrupo');
                    $('#tbGrupo').html(tb);
                    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
                }

                $("#cboRol0 option[value=\'" + seleccion + "\']").attr("selected", true);
                $("#cboRol0").val(seleccion);
            },
            error: function(result) {
                console.log(result);
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function fnGuardarObjetivos(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        //if ($('#codE').val() == "0") {
        var form = JSON.stringify(objetivos);
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Concurso.aspx",
            data: { "hdcodP": cod, "action": ope.rob, "array": form },
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
                        tb += '<td style="width:70%">' + data[i].des + '</td>';
                        tb += '<td style="width:20%">' + data[i].tipo + '</td>';
                        tb += '<td style="text-align:center;width:10%"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
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
        $("#txttituloPos").focus();
        fnMensaje("warning", "Ingrese Titulo de Postulación");
        return false;
    }
    if ($("#cboLinea").val() == "") {
        fnMensaje("warning", "Debe Seleccionar la Linea de Investigación");
        return false;
    }
    if (!$("#chkOCDE").is(':checked')) {
        fnMensaje("warning", "Debe Seleccionar Una Área de Investigación OCDE")
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
    if ($("#cboProvincia").val() == "0") {
        fnMensaje("warning", "Debe Seleccionar la Provincia");
        return false;
    }
    if ($("#cboDistrito").val() == "0") {
        fnMensaje("warning", "Debe Seleccionar el Distrito");
        return false;
    }
    if ($("#txtLugar").val() == "") {
        $("#txtLugar").focus();
        fnMensaje("warning", "Ingrese Lugar");
        return false;
    }
    return true;
}


function fnValidarPostulacionTab2() {
    /*if ($("#rbtipoparticipante2").is(':checked') || $("#rbtipoparticipante3").is(':checked')) {
    if ($("#cboGrupo").val() == "") {
    fnMensaje("warning", "Debe Seleccionar un Grupo de Investigación");
    return false;
    }
    }*/
    var nro = equipo.length
    var cont = 0;
    for (i = 0; i < nro; i++) {
        if ($("#cboRol" + i).val() == "") {
            $("#cboRol" + i).focus();
            fnMensaje("warning", "Debe Seleccionar el Rol de Participante")
            return false;
        }

        if ($("#cboRol" + i + " option:selected").text() == "INVESTIGADOR PRINCIPAL") {
            cont = cont + 1;
            if (cont > 1) {
                fnMensaje("warning", "El Grupo Solo puede tener un Investigador Principal.");
                return false;
            }
        }
        /*
        if (equipo[i].cod_alu == 0) {
        if ($("#txtdedicacion" + i).val() == "") {
        $("#txtdedicacion" + i).focus();
        fnMensaje("warning", "Debe Ingresar Horas semanales de dedicación")
        return false;
        }
        if (!/^([0-9])*$/.test($("#txtdedicacion" + i).val())) {
        $("#txtdedicacion" + i).focus();
        //alert($("#txtdedicacion" + i).val());
        fnMensaje("warning", "Ingrese Correctamente Dedicación(Números Enteros)")
        return false;
        }
        }*/
    }

    if (cont == 0) {
        fnMensaje("warning", "El Grupo debe tener al menos un Integrante con Rol de Investigador Principal.");
        return false;
    }
    if ($("#rbtipoparticipante3").is(':checked')) {
        if (equipo.length < 2) {
            fnMensaje("warning", "El Grupo debe contar con al menos dos integrantes")
            return false;
        }
    }
    return true;
}

function fnValidarPostulacionTab3() {
    if ($("#txtresumen").val() == "") {
        $("#txtresumen").focus();
        fnMensaje("warning", "Ingrese Resumen de Propuesta");
        $("#txtresumen").focus();
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
        $("#txtpalabras").focus();
        fnMensaje("warning", "Ingrese palabras Clave de Propuesta");
        $("#txtpalabras").focus();
        return false;
    }
    if ($("#txtjustificacion").val() == "") {
        $("#txtjustificacion").focus();
        fnMensaje("warning", "Ingrese Justificación de Propuesta");
        $("#txtjustificacion").focus();
        return false;
    }

    if ($("#txtfeciniPos").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Inicio de Propuesta");
        return false;
    }
    if ($("#txtfecfinPos").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Fin de Propuesta");
        return false;
    }
    var fecini = new Date($("#txtfeciniPos").val().substr(3, 2) + '/' + $("#txtfeciniPos").val().substr(0, 2) + '/' + $("#txtfeciniPos").val().substr(6, 4));
    var fecfin = new Date($("#txtfecfinPos").val().substr(3, 2) + '/' + $("#txtfecfinPos").val().substr(0, 2) + '/' + $("#txtfecfinPos").val().substr(6, 4));

    if (fecini > fecfin) {
        fnMensaje("warning", "Fecha de Fin de Propuesta no Puede ser Menor a la Fecha de Inicio.");
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
    }

    if ($("#file_resultados").val() == '') {
    fnMensaje("warning", 'Adjuntar Archivo de Resultados Esperados en Formato de PDF');
    return false;
    }

    if ($("#file_resultados").val() != '') {
    archivo = $("#file_resultados").val();
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
    fnMensaje("warning", "Solo puede Adjuntar Archivo de Resultados Esperados en Formato de PDF");
    return false;
    }
    }


    if ($("#file_declaracion").val() == '') {
    fnMensaje("warning", 'Adjuntar Archivo de Declaracion Jurada en Formato de PDF');
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
    if (fnValidarPostulacionTab4() == false) {
        $("#tab4").trigger("click");
        return false;
    }
    return true
}


function fnConfirmar() {
    if (fnvalidarTabs() == true) {
        fnMensajeConfirmarEliminar('top', '¿Desea Registrar Postulación, una vez Guardado no se podran Editar los Datos de Postulación.? ', 'fnGuardarPostulacion');
    }
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
        $('form#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
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
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    //console.log(data[0].cod)
                    fnGuardarEquipo(data[0].cod)
                    fnGuardarObjetivos(data[0].cod)

                    if ($("#file_producto").val() != "") {
                        //console.log("INGRESA ARCHIVO INFORME");
                        SubirArchivo(data[0].cod, "PROPUESTA");
                    }
                    /*if ($("#file_pto").val() != "") {
                    //console.log("INGRESA ARCHIVO PTO");
                    SubirArchivo(data[0].cod, "PRESUPUESTO");
                    }
                    if ($("#file_cronograma").val() != "") {
                    //console.log("INGRESA ARCHIVO CRONOGRAMA");
                    SubirArchivo(data[0].cod, "CRONOGRAMA");
                    }
                    if ($("#file_resultados").val() != "") {
                    //console.log("INGRESA ARCHIVO PTO");
                    SubirArchivo(data[0].cod, "RESULTADOSESPERADOS");
                    }
                    if ($("#file_declaracion").val() != "") {
                    //console.log("INGRESA ARCHIVO DECLARACION");
                    SubirArchivo(data[0].cod, "DECLARACION");
                    }*/

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


function fnGuardarEquipo(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        //            if ($('#codE').val() == "0") {
        /*var cont = equipo.length
        for (i = 0; i < cont; i++) {
        if (equipo[i].cod_alu == 0) {
        equipo[i].dedicacion = $("#txtdedicacion" + i).val();
        } else {
        equipo[i].dedicacion = 0
        }
        }*/
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

function fnGuardarObjetivos(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        //if ($('#codE').val() == "0") {
        var form = JSON.stringify(objetivos);
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
            data: { "hdcodP": cod, "action": ope.rob, "array": form },
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
        if (tipo == "PRESUPUESTO") {
            var files = $("#file_pto").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }
        if (tipo == "CRONOGRAMA") {
            var files = $("#file_cronograma").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }
        if (tipo == "RESULTADOSESPERADOS") {
            var files = $("#file_resultados").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }
        if (tipo == "DECLARACION") {
            var files = $("#file_declaracion").get(0).files;
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
            //flag= true;
        }

        //        } else {
        //            alert("Pf. Verificar extensión de archivo");
        //        }
        return flag;
        fnLoading(false);
    }
    catch (err) {
        return false;
        fnLoading(false);
    }
    fnLoading(false);
}
/*

function SubirArchivos2(cod) {
fnLoading(true)
var flag = false;
try {

var data = new FormData();
data.append("action", ope.Up2);
data.append("codigo", cod);
var files = ""

var archivo = ""
files = $("#file_producto").get(0).files;
if (files.length > 0) {
archivo = files[0];
}
data.append("propuesta", archivo);

archivo = "";
files = $("#file_pto").get(0).files;
if (files.length > 0) {
archivo = files[0];
}
data.append("presupuesto", archivo);

archivo = "";
files = $("#file_cronograma").get(0).files;
if (files.length > 0) {
archivo = files[0];
}
data.append("cronograma", files[0]);

archivo = "";
files = $("#file_resultados").get(0).files;
if (files.length > 0) {
archivo = files[0];
}
data.append("resultadoesperado", files[0]);

archivo = "";
files = $("#file_declaracion").get(0).files;
if (files.length > 0) {
archivo = files[0];
}
data.append("declaracion", files[0]);

//console.log(data);
//        if (files.length > 0) {
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
console.log('ARCHIVOS SUBIDO');
//console.log(data);

},
error: function(result) {
//console.log('falseee');
flag = false;
//console.log(result);
}
});
//flag= true;
}

//        } else {
//            alert("Pf. Verificar extensión de archivo");
//        }
return flag;
fnLoading(false);
//    }
catch (err) {
return false;
fnLoading(false);
}
fnLoading(false);
}

*/


function fnLimpiarPostulacion() {
    $("#hdcodPos").val("0");
    //    $("#hdcodCon").val("0");
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
    equipo = [];
    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html('');
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);
    fnDestroyDataTableDetalle('tGrupo');
    $('#tbGrupo').html('');
    fnResetDataTableBasic('tGrupo', 0, 'asc', 10);
    /* $("#file_resultados").val("");
    $("#file_pto").val("");
    $("#file_cronograma").val("");*/
    $("#file_producto").val("");
    //$("#file_declaracion").val("");
    $("#tab1").trigger("click");
    $("#btnA").removeAttr("style")
    $("#btnA").attr("style", "display:inline-block")
    $("#chkOCDE").removeAttr('disabled');

    $("#cboArea").removeAttr('disabled');
    $("#cboSubArea").removeAttr('disabled');
    $("#cboDisciplina").removeAttr('disabled');

    $("#btnAgregarObjetivo").removeAttr('disabled');
    //$("#cboGrupo").removeAttr('disabled');
    $('#resultados').html("");
    $('#pto').html("");
    $('#cronograma').html("");
    $('#producto').html("");
    $('#declaracion').html("");
    //$("#divalumno").attr("style", "display:none")
    $("#divDocente").attr("style", "display:none")
    $("#cboRegion").val("0");
    $("#cboRegion").removeAttr('disabled');
    $("#cboProvincia").html("<option value='0'>-- Seleccione --</option>");
    $("#cboProvincia").removeAttr('disabled');
    $("#cboDistrito").html("<option value='0'>-- Seleccione --</option>");
    $("#cboDistrito").removeAttr('disabled');
    $("#txtLugar").val("");
    $("#txtLugar").removeAttr('disabled');
    $("#txttituloPos").removeAttr('disabled');
    $("#cboLinea").removeAttr('disabled');
    $("#txtresumen").removeAttr('disabled');
    $("#txtpalabras").removeAttr('disabled');
    $("#txtjustificacion").removeAttr('disabled');
    $("#txtfeciniPos").removeAttr('disabled');
    $("#txtfecfinPos").removeAttr('disabled');
    $("#file_producto").removeAttr('disabled');
}

function fnListarPostulacion(codigo_con, cerrado) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
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
                        tb += '<td style="width:60%">' + data[i].titulo + '</td>';
                        tb += '<td style="text-align:center;width:10%">' + data[i].fechareg + '</td>';
                        tb += '<td style="text-align:center;width:20%">' + data[i].des_etapa + '</td>';
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
                if (data.length > 0) {

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
                    $("#txttituloPos").attr('disabled', 'disabled');
                    $("#cboLinea").attr('disabled', 'disabled');
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
                    //fnListarInvestigadorGrupos('IU');
                    $("#cboGrupo").attr("style:display:block");
                    $("#cboGrupo").val(data[0].gru)
                    $("#rbGrupo").attr("disabled", "disabled")
                    }
                    if (data[0].tipo_gru == 2) {
                    $("#rbIndividual").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:block")
                    $("#rbGrupoU").attr("style", "display:none")
                    $("#rbGrupoM").attr("style", "display:block")
                    $("#rbtipoparticipante3").prop("checked", true);
                    $("#rbtipoparticipante1").prop("checked", false);
                    $("#rbtipoparticipante2").prop("checked", false);
                    //fnListarInvestigadorGrupos('IM');
                    $("#cboGrupo").attr("style:display:block")
                    $("#cboGrupo").val(data[0].gru)
                    $("#rbtipoparticipante3").attr("disabled", "disabled")
                    }
                    */

                    $("#rbIndividual").attr("style", "display:none")
                    $("#rbGrupo").attr("style", "display:none")
                    $("#rbGrupoU").attr("style", "display:none")
                    $("#rbGrupoM").attr("style", "display:none")
                    $("#divDocente").attr("style", "display:none")

                    fnListarEquipo(cod)

                    $("#txtresumen").val(data[0].resumen);
                    $("#txtpalabras").val(data[0].palabras);
                    $("#txtjustificacion").val(data[0].justificacion);
                    $("#txtfeciniPos").val(data[0].fechaini);
                    $("#txtfecfinPos").val(data[0].fechafin);


                    $("#txtresumen").attr('disabled', 'disabled');
                    $("#txtpalabras").attr('disabled', 'disabled');
                    $("#txtjustificacion").attr('disabled', 'disabled');
                    $("#txtfeciniPos").attr('disabled', 'disabled');
                    $("#txtfecfinPos").attr('disabled', 'disabled');

                    //                $("#cbotipo").val(data[0].tipo);
                    /*if (data[0].rutaresultados != "") {
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
                    }*/
                    if (data[0].rutainforme != "") {
                        $("#producto").html("<a onclick='fnDownload(\"" + data[0].rutainforme + "\")' style='font-weight:bold'>Descargar Propuesta</a>")
                    } else {
                        $("#producto").html("");
                    }
                    /*if (data[0].rutadeclaracion != "") {
                    $("#declaracion").html("<a onclick='fnDownload(\"" + data[0].rutadeclaracion + "\")' style='font-weight:bold'>Descargar Declaración</a>")
                    } else {
                    $("#declaracion").html("");
                    }
                    */
                    $("#btnA").attr("style", "display:none")
                    $("#tab1").trigger("click");

                    $("#chkOCDE").attr('disabled', 'disabled');

                    fnListarObjetivos(cod)
                    $("#btnAgregarObjetivo").attr('disabled', 'disabled');
                    //$("#cboGrupo").attr('disabled', 'disabled');
                    //$("#divalumno").attr("style", "display:none")
                    $("#divDocente").attr("style", "display:none")
                    $("#file_producto").attr('disabled', 'disabled');
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
                        tb += '<td style="width:39%">' + data[i].nombre + '</td>';
                        if (data[i].dina != '-') {
                            tb += '<td style="width:28%"><a href="' + data[i].dina + '" target="_blank">' + data[i].dina.substr(0, 70) + '</a></td>';
                        } else {
                            tb += '<td style="width:28%">-</td>';
                        }

                        tb += '<td style="width:30%">' + data[i].rol + '</td>';
                        //tb += '<td style="width:4%">' + data[i].dedicacion + '</td>';
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

/*
function fnAgregarAlumno() {
var value;
var tb = '';
var rowCount = $('#tbGrupo tr').length;
//    console.log(rowCount);
var repite = false;
//console.log(equipo);
if (fnValidarIntegrante() == true) {
for (i = 0; i < equipo.length; i++) {
if (equipo[i].cod_alu == cod) {
repite = true;
}
}
//console.log(repite);
if (repite == false) {
if (rowCount > 0) {
tb = $("#tbGrupo").html();
if (rowCount == 1) {
if ($('#tbGrupo tr td').html() == 'No se ha encontrado informacion') {
tb = ''
}
}
} else {
tb = '';
}
var arr = fnRolInvestigador();
var n = arr.length;
var str = "";
str += '<option value="">-- Seleccione -- </option>';
for (j = 0; j < n; j++) {
if (arr[j].nombre.toUpperCase() == 'TESISTA') {
str += '<option selected="selected" value="' + arr[j].cod + '">' + arr[j].nombre.toUpperCase() + '</option>';
} else {
str += '<option value="' + arr[j].cod + '">' + arr[j].nombre.toUpperCase() + '</option>';
}
}

var num = equipo.length
tb += '<tr>';
tb += '<td style="text-align:center;width:3%">' + (num + 1) + '</td>';
tb += '<td  style="width:37%">' + nombre + '</td>';
tb += '<td  style="width:28%">-</td>';
tb += '<td  style="width:28%"><select id="cboRol' + num + '" name="cboRol' + num + '" class="form-control" style="font-size:13px;" onchange="fnCambiaRol(' + num + ')" >' + str + '</select></td>';
tb += '<td  style="width:4%">-</td>';
tb += '</tr>';
//console.log(tb);
fnDestroyDataTableDetalle('tGrupo');
$('#tbGrupo').html(tb);
fnResetDataTableBasic('tGrupo', 0, 'asc', 10);

//            console.log($("#cboRol" + num + " option:selected").val());

equipo.push({
cod_inv: 0,
cod_alu: cod,
nom_inv: nombre,
dina: '',
cod_rol: $("#cboRol" + num + " option:selected").val(),
dedicacion: '0'
});



cod = "";
nombre = "";
$("#txtalumno").val("");

} else {
fnMensaje("warning", "El Alumno ya ha sido ingresado")
}

}
}

function fnValidarIntegrante() {
if ($("#txtalumno").val() == "" || cod == "") {
fnMensaje("warning", "Debe Seleccionar un Alumno");
return false;
}
return true;
}
*/

//function fnDownload(id_ar) {
//    var flag = false;
//    var form = new FormData();
//    form.append("action", "Download2");
//    form.append("IdArchivo", id_ar);
//    // alert();
//    //            console.log(form);
//    $.ajax({
//        type: "POST",
//        url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
//        data: form,
//        dataType: "json",
//        cache: false,
//        contentType: false,
//        processData: false,
//        success: function(data) {
//            console.log(data);
//            flag = true;

//            var file = 'data:application/octet-stream;base64,' + data[0].File;
//            var link = document.createElement("a");
//            link.download = data[0].Nombre;
//            link.href = file;
//            link.click();
//        },
//        error: function(result) {
//            console.log(result);
//            flag = false;
//        }
//    });
//    return flag;
//}


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