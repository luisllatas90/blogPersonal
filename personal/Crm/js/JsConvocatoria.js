$(document).ready(function() {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tConvocatoria', 2, 'asc');
    ope = fnOperacion(1);
    rpta = fnvalidaSession()

    //alert(rpta)
    if (rpta == true) {
        fnTipoEst(1);
        fnTipoEst(2);
        fnCicloAcad();
        fnBuscarConvocatoria(false);
        $('#btnListar').click(fnBuscarConvocatoria);

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
            if ($("#cboTipoEstudio option:selected").text() == "TODOS") {
                $("#cboTipoEstudioR").prop('selectedIndex', 0);
            } else {
                $("#cboTipoEstudioR").val($("#cboTipoEstudio").val());
            }

        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            Edit()
        }
    })
    //    $('#mdEliminar').on('show.bs.modal', function(event) {
    //        var button = $(event.relatedTarget) // Botón que activó el modal
    //        $('#hdcod').remove();
    //        $('#frmEliminar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
    //    })
})

function fnTipoEst(op) {

    var arr = fnTipoEstudio(1, "TO", op);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        //str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        if (arr[i].nombre == 'TODOS') {
            str += '<option value="' + arr[i].cod + '" selected="selected">' + arr[i].nombre + '</option>';
        } else {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }
    }
    if (op == 1) {
        $('#cboTipoEstudio').html(str);
    }
    if (op == 2) {
        $('#cboTipoEstudioR').html(str);
    }
}

function fnCicloAcad() {

    var arr = fnCicloAcademico(1, "LCV", "");
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboCicloAcademicoR').html(str);
}

function fnBuscarConvocatoria(sw) {
    if ($("#cboTipoEstudio").val() == "") {
        fnMensaje("error", "Debe Seleccionar un tipo de estudio.")
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {

            if (sw) { fnLoading(true); }
            //fnLoading(true)
            $('#frmBuscarConvoc').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
            var form = $("#frmBuscarConvoc").serializeArray();
            //console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/crm/Convocatoria.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    $("form#frmBuscarConvoc input[id=action]").remove();
                    //console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        //if (i == 0 && !data[i].sw) {
                        //fnMensaje('warning', data[i].msje);
                        //break;
                        //}

                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td>' + data[i].cNombre + '</td>';
                        tb += '<td>' + data[i].cTest + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cFecini + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cFecFin + '</td>';
                        //                        tb += '<td align="center">';
                        //                        if (data[i].cEstado == 1) {
                        //                            tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                        //                        } else {
                        //                            tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                        //                        }
                        tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';

                        tb += '<button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';

                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tConvocatoria');
                    $('#tbConvocatoria').html(tb);
                    fnResetDataTableBasic('tConvocatoria', 2, 'asc');
                    if (sw) { fnLoading(false); }
                    //fnLoading(false);
                },
                error: function(result) {
                    //console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
    }

}



function fnGuardar() {
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
                    url: "../DataJson/crm/Convocatoria.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            $("#cboTipoEstudio").val($("#cboTipoEstudioR").val())
                            Limpiar()
                            fnBuscarConvocatoria(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //console.log(result)
                        fnMensaje("error", result)
                    }
                });
            } else {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Convocatoria.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            $('#hdcod').val(0);
                            $("#cboTipoEstudio").val($("#cboTipoEstudioR").val())
                            Limpiar()
                            fnBuscarConvocatoria(false);
                            fnMensaje("success", data[0].msje)
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //console.log(result)
                        fnMensaje("error", result)
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
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Convocatoria.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //console.log(data);
                $("#cboTipoEstudioR").val(data[0].cTes);
                $('#cboCicloAcademicoR').val(data[0].cCac);
                $("#txtnombre").val(data[0].cNombre);
                $("#txtdetalle").val(data[0].cDetalle);
                $("#txtfecini").val(data[0].cFecini);
                $("#txtfecfin").val(data[0].cFecFin);
                $("#txtfecini").datepicker().datepicker("setDate", data[0].cFecini);
                $("#txtfecfin").datepicker().datepicker("setDate", data[0].cFecFin);
                //                if (data[0].cEstado == 1) {
                //                    $("#chkestado").prop("checked", true);
                //                } else {
                //                    $("#chkestado").prop("checked", false);
                //                }
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
            url: "../DataJson/crm/Convocatoria.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarConvocatoria(false);
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                //console.log(result)
                fnMensaje("error", result)
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
        mensaje: '¿Desea Eliminar Convocatoria?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}

function Limpiar() {
    $("#cboTipoEstudioR").prop('selectedIndex', 0);
    $('#cboCicloAcademicoR').prop('selectedIndex', 0);
    $("#txtnombre").val("");
    $("#txtdetalle").val("");
    // $("#chkestado").prop("checked", true);
    var hoy = new Date();
    var dd = hoy.getDate();
    var mm = hoy.getMonth() + 1; //hoy es 0!
    var yyyy = hoy.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    hoy = dd + '/' + mm + '/' + yyyy;
    //    $("#txtfecini").val(hoy);
    $("#txtfecini").datepicker().datepicker("setDate", hoy);
    //    $("#txtfecfin").val(hoy);
    $("#txtfecfin").datepicker().datepicker("setDate", hoy);
    $("#txtfecini").val("");
    $("#txtfecfin").val("");
}


function fnValidar() {
    if ($("#cboTipoEstudioR").val() == '') {
        fnMensaje("error", 'Seleccione un Tipo de Estudio')
        return false
    }
    if ($("#cboCicloAcademicoR").val() == '') {
        fnMensaje("error", 'Seleccione El Ciclo Académico')
        return false
    }
    if ($("#txtnombre").val() == '') {
        fnMensaje("error", 'Ingrese Nombre')
        return false
    }
    if ($("#txtfecini").val() == '') {
        fnMensaje("error", 'Ingrese Fecha de Inicio')
        return false
    }
    if ($("#txtfecfin").val() == '') {
        fnMensaje("error", 'Ingrese Fecha de Finalización')
        return false
    }
    return true
}



function fnCreateDataTableBasic(table, col, ord) {
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": 10,
        "aaSorting": [[col, ord]]
    });
    return dt;
}


function fnResetDataTableBasic(table, col, ord) {
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
            "aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": 10,
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

