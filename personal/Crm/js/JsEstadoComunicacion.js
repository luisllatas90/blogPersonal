$(document).ready(function() {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tEstadoComunicacion', 2, 'asc', 10);
    ope = fnOperacion(1);
    fnBuscarEstadoComunicacion(true);
    rpta = fnvalidaSession()
    if (rpta == true) {
        $('#btnListar').click(fnBuscarEstadoComunicacion);
        $('#btnGuardar').click(fnGuardar);
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $('#mdRegistro').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        $("#hdValida").val("0")
        if (button.attr("id") == "btnAgregar") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
            Limpiar();
        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            Edit()
        }
    })
})


function fnBuscarEstadoComunicacion(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        //fnLoading(true)
        $("form#frmBuscarEstadoComunicacion input[id=action]").remove();
        $('#frmBuscarEstadoComunicacion').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        var form = $("#frmBuscarEstadoComunicacion").serializeArray();
        $("form#frmBuscarEstadoComunicacion input[id=action]").remove();
//                console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/EstadoComunicacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //$("form#frmBuscarEstadoComunicacion input[id=action]").remove();
//                                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                    tb += '<td>' + data[i].nom + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cod + '" title="Editar" ><i class="ion-edit"></i></button>';
                    tb += '<button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tEstadoComunicacion');
                $('#tbEstadoComunicacion').html(tb);
                fnResetDataTableBasic('tEstadoComunicacion', 2, 'asc', 10);
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function(result) {
                //console.log(result)
                if (sw) { fnLoading(false); }
            }
        });
    } else {
        window.location.href = rpta
    }
}


function fnGuardar() {
    if ($("#hdValida").val() == "0") {
        $("#hdValida").val("1")
        rpta = fnvalidaSession()
        if (rpta == true) {
            if (fnValidar() == true) {
                fnLoading(true)
                if ($("#hdcod").val() == 0) {
                    $("form#frmRegistro input[id=action]").remove();
                    $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                    var form = $("#frmRegistro").serializeArray();
                    $("form#frmRegistro input[id=action]").remove();
                    //$('#hdcod').remove();
                    //console.log(form);
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/crm/EstadoComunicacion.aspx",
                        data: form,
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            //console.log(data);
                            if (data[0].rpta == 1) {
                                fnMensaje("success", data[0].msje)
                                Limpiar()
                                fnBuscarEstadoComunicacion(false);

                                $("#mdRegistro").modal("hide");
                            } else {
                                fnMensaje("warning", data[0].msje)
                            }
                            $("#hdValida").val("0")
                        },
                        error: function(result) {
                            //console.log(result)
                            fnMensaje("warning", result)
                        }
                    });
                } else {
                    $("form#frmRegistro input[id=action]").remove();
                    $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                    var form = $("#frmRegistro").serializeArray();
                    $("form#frmRegistro input[id=action]").remove();
                    $('#hdcod').val(0);
                    ////console.log(form);
                    $.ajax({
                        type: "POST",
                        url: "../DataJson/crm/EstadoComunicacion.aspx",
                        data: form,
                        dataType: "json",
                        cache: false,
                        async: false,
                        success: function(data) {
                            //                        console.log(data);
                            if (data[0].rpta == 1) {
                                fnMensaje("success", data[0].msje)
                                Limpiar()
                                fnBuscarEstadoComunicacion(false);
                                $("#mdRegistro").modal("hide");
                            } else {
                                fnMensaje("warning", data[0].msje)
                            }
                            $("#hdValida").val("0")
                        },
                        error: function(result) {
                            //console.log(result)
                            fnMensaje("warning", result)
                        }
                    });
                }
                fnLoading(false)
            }
        } else {
            window.location.href = rpta
        }
    } else {
        fnMensaje("warning", "Solo se puede Registrar uno a la Vez")
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
            url: "../DataJson/crm/EstadoComunicacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //                console.log(data);
                $("#txtDescripcion").val(data[0].nom);
                if (data[0].est == 1) {
                    $("#chkestado").prop("checked", true);
                } else {
                    $("#chkestado").prop("checked", false);
                }
                //if (sw) { fnLoading(false); }
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

function fnEliminar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/EstadoComunicacion.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarEstadoComunicacion(false);
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                //console.log(result)
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
        mensaje: '¿Desea Eliminar Estado de Comunicación?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}


function Limpiar() {
    $("#txtDescripcion").val("");
    $("#chkestado").prop("checked", true);
}


function fnValidar() {
    if ($("#txtDescripcion").val() == '') {
        fnMensaje("error", 'Ingrese Descripción')
        return false
    }
    return true
}


function fnCreateDataTableBasic(table, col, ord, nro_filas) {
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        //"aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": nro_filas,
        "aaSorting": [[col, ord]]
    });
    return dt;
}

function fnResetDataTableBasic(table, col, ord, nro_filas) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().fnDestroy();
        dt = $('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            //"aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": nro_filas,
            "aaSorting": [[col, ord]]
        });

        return dt;
    }
}

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
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

