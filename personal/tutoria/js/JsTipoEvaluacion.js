$(document).ready(function() {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tTipoEvaluacion', 2, 'asc');
    var dt1 = fnCreateDataTableBasicDetalle('tVariable', 0, 'asc');
    ope = fnOperacion(1);
    //////console.log(ope)
    fnBuscarTipoEvaluacion();
    rpta = fnvalidaSession()
    //alert(rpta)
    if (rpta == true) {
        $('#btnListar').click(fnBuscarTipoEvaluacion);
        $('#btnGuardar').click(fnGuardar);
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $('#mdRegistro').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        //alert('--')
        if (button.attr("id") == "btnAgregar") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
            Limpiar();
            $("#divVariables").css('display', 'none');
            $("#divVariables").css('visibility', 'hidden');
        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            $("#divVariables").css('display', 'block');
            $("#divVariables").css('visibility', 'visible');
            Edit();
            fnVariables();
        }
    })
    //document.getElementById("puntaje").maxLength = "2";
    $("#puntaje").keydown(function(e) {

        //////console.log('s');
        //            if ($("#puntaje").val().length==2){
        //                ////console.log('sss');
        //                e.preventDefault();
        //                }
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
                (e.keyCode == 65 & e.ctrlKey === true) ||
                (e.keyCode >= 35 & e.keyCode <= 39)) {

            return;
        }

        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) & (e.keyCode < 96 || e.keyCode > 105)) {

            e.preventDefault();
        }
    });

    $("#puntaje").keyup(function(e) {
        //            if(parseInt($("#puntaje").val()) < 0 || isNaN(parseInt($("#puntaje").val()))) 
        //                $("#puntaje").val('0') ; 
        //            else 
        if (parseInt($("#puntaje").val()) > 10)
            $("#puntaje").val('10');

        if (parseInt($("#puntaje").val().length) > 4) {

            $("#puntaje").val($("#puntaje").val().substring(0, 4));
        }
    });

    $("#cboAplica").change(function() {
        if ($("#cboAplica").val() !== "peso") {
            $("#puntaje").val('1');
            $("#puntaje").prop('disabled', true);
        } else {
            $("#puntaje").val('');
            $("#puntaje").prop('disabled', false);
        }
    });
})

function fnVariables() {

    var arr = fVariables(1, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cCod + '">' + arr[i].cDescripcion + '</option>';
    }

    $("#cboVariable").html(str);
}
function fnBuscarTipoEvaluacion(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        //fnLoading(true)
        $("form#frmBuscarTipoEvaluacion input[id=action]").remove();
        $('#frmBuscarTipoEvaluacion').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        var form = $("#frmBuscarTipoEvaluacion").serializeArray();
        $("form#frmBuscarTipoEvaluacion input[id=action]").remove();
        //        ////console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/TipoEvaluacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //$("form#frmBuscarTipoComunicacion input[id=action]").remove();
                //////console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                    tb += '<td>' + data[i].cDescripcion + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';
                    tb += '<button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tTipoEvaluacion');
                $('#tbTipoEvaluacion').html(tb);
                fnResetDataTableBasic('tTipoEvaluacion', 2, 'asc');
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function(result) {
                //////console.log(result)
                if (sw) { fnLoading(false); }
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnBuscarVariableTipoEvaluacion(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        //fnLoading(true)
        $("form#frmRegistro input[id=action]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        //        ////console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/VariableTipoEvaluacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //$("form#frmBuscarTipoComunicacion input[id=action]").remove();
                //////console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                    tb += '<td>' + data[i].cVar + '</td>';
                    tb += '<td style="text-align:center">' + data[i].cPeso + '</td>';
//                    tb += '<td>' + data[i].cTotal + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDeleteVar(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tVariable');
                $('#tbVariable').html(tb);
                fnResetDataTableBasicDetalle('tVariable', 0, 'asc');
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function(result) {
                //////console.log(result)
                if (sw) { fnLoading(false); }
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (ValidaGuardar() == true) {
            fnLoading(true)
            if ($("#hdcod").val() == 0) {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //$('#hdcod').remove();
                //////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/TipoEvaluacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //////console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscarTipoEvaluacion(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //////console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            } else {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $('#hdcod').val(0);
                ////////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/TipoEvaluacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        ////console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscarTipoEvaluacion(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //////console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            }
            fnLoading(false)
        }        
    } else {
        window.location.href = rpta
    }
}


function Edit() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmRegistro input[id=action]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/TipoEvaluacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                ////console.log(data);
                $("#txtDescripcion").val(data[0].cDescripcion);
                if (data[0].cEstado == 1) {
                    $("#chkestado").prop("checked", true);
                } else {
                    $("#chkestado").prop("checked", false);
                }
                $("#cboAplica").val(data[0].cOpe);
                $("#cboAplica").trigger('change');
                //if (sw) { fnLoading(false); }
                fnBuscarVariableTipoEvaluacion(false);
                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
                //////console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnEliminar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/TipoEvaluacion.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //////console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarTipoEvaluacion(false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function(result) {
                //////console.log(result)
                fnMensaje("warning", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}
function fnEliminarVar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/VariableTipoEvaluacion.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //////console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarVariableTipoEvaluacion (false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function(result) {
                //////console.log(result)
                fnMensaje("warning", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}


var aDataR = [];
function fnDelete(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar el Tipo de Evaluación?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}

function fnDeleteVar(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar esta variable?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminarVar', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}
function Limpiar() {
    $("#txtDescripcion").val("");
    $("#chkestado").prop("checked", true);
    $("#cboVariable").val("");
    $("#cboAplica").val("");
    $("#puntaje").val("");
    fnDestroyDataTableDetalle('tVariable');
    $('#tbVariable').html('');
    fnResetDataTableBasicDetalle('tVariable', 0, 'asc');
}

function fnLoading(sw) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
}

function fnLoadingDiv(div, sw) {
    if (sw) {
        $("#" + div).removeClass('hidden');
    } else {
        $("#" + div).addClass('hidden');
    }
}
function fnAgregar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (Valida() == true) {
            if ($("#hdcod").val() !== 0) {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $("#puntaje").prop('disabled', false);
                var form = $("#frmRegistro").serializeArray();
                $("#cboAplica").trigger('change');
                $("form#frmRegistro input[id=action]").remove();
                //$('#hdcod').remove();
                //////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/VariableTipoEvaluacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //////console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            fnBuscarVariableTipoEvaluacion(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //////console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            } 
        }
        
    } else {
        window.location.href = rpta
    }
}
function Valida() {
    if ($("#cboVariable").val() == '') {
        fnMensaje("warning", 'Seleccione una variable')
        return false
    }
    if ($("#cboAplica").val() == 'peso') {
        if ($("#puntaje").val() == '' || $("#puntaje").val() == '0') {
            fnMensaje("warning", 'Escriba el peso de la variable correspondiente')
            return false
        }
    }
    
    
    return true
}
function ValidaGuardar() {
    if ($("#txtDescripcion").val().replace(/ /g, '') == '') {
        fnMensaje("warning", 'Se necesita una descripción')
        return false
    }
    if ($("#cboAplica").val() == '') {
        fnMensaje("warning", 'Seleccione una operación')
        return false
    }
    return true
}

