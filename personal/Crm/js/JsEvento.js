$(document).ready(function () {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tEvento', 2, 'asc');
    ope = fnOperacion(1);
    //    console.log(ope)
    rpta = fnvalidaSession()

    $('#cboTipoEstudio').on('change', function (e) {
        var codigoTest = $(this).val();
        fnConvocatoria(codigoTest, 'L');
    });

    $('#mdRegistro').on('show.bs.modal', function (event) {
        var codigoTest = $('#cboTipoEstudio').val();
        fnConvocatoria(codigoTest, 'R');

        var button = $(event.relatedTarget) // Botón que activó el modal
        //        alert('--')
        if (button.attr("id") == "btnAgregar") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
            Limpiar();
            if ($("#cboConvocatoria option:selected").text() == "TODOS") {
                $("#cboConvocatoriaR").prop('selectedIndex', 0);
            } else {
                $("#cboConvocatoriaR").val($("#cboConvocatoria").val());
            }
        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            Edit()
        }
    })

    //alert(rpta)
    if (rpta == true) {
        fnTipoEst(1);
        // fnConvocatoria("L");    //Listar
        // fnConvocatoria("R");    //Listar en Ventana de Registro
        fnBuscarEvento(false);
        fnActividadPOA("L");    //Listar

        $('#btnListar').click(fnBuscarEvento);
        $('#btnGuardar').click(fnGuardar);
    } else {
        window.location.href = rpta
    }
    fnLoading(false);
})

function fnTipoEst(op) {
    var arr = fnTipoEstudio(1, "TO", op);
    var n = arr.length;
    var str = "";
    str += '<option value=""> -- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        if (arr[i].nombre == 'TODOS') {
            continue;
        }
        if (arr[i].nombre == 'PRE GRADO') {
            str += '<option value="' + arr[i].cod + '" selected>' + arr[i].nombre + '</option>';
        } else {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }

    }
    if (op == 1) {
        $('#cboTipoEstudio').html(str);
        $('#cboTipoEstudio').trigger('change');
    }
    if (op == 2) {
        $('#cboTipoEstudioR').html(str);
    }
}

function fnConvocatoria(codigoTest, op) {
    var arr = fConvocatoria(1, "C", codigoTest);

    var n = arr.length;
    var str = "";

    if (n == 0) {
        str += '<option value="" selected>-- Seleccione -- </option>';
    } else {
        for (i = 0; i < n; i++) {
            if (arr[i].nombre == 'TODOS') {
                continue;
                // str += '<option value="' + arr[i].cod + '" selected="selected">' + arr[i].nombre + '</option>';
            } else {
                str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
            }
        }
    }

    if (op == 'L') {
        $('#tbEvento').html('');
        $('#cboConvocatoria').html(str);
    }
    if (op == 'R') {
        $('#cboConvocatoriaR').html(str);
    }
}


function fnActividadPOA(op) {
    var arr = fActividadPOA(1, "C", op);

    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    if (op == 'L') {
        $('#cboActividadPOAR').html(str);
    }
}


function fnBuscarEvento(sw) {
    if ($("#cboConvocatoria").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Convocatoria.")
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            if (sw) { fnLoading(true); }
            //    fnLoading(true)
            $('#frmBuscarEvento').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');

            var form = $("#frmBuscarEvento").serializeArray();
            console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/crm/Evento.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function (data) {
                    $("form#frmBuscarEvento input[id=action]").remove();
                    console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td>' + data[i].cEvento + '</td>';
                        tb += '<td>' + data[i].cConvocatoria + '</td>';
                        tb += '<td>' + data[i].cActividad + '</td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';
                        tb += '<button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tEvento');
                    $('#tbEvento').html(tb);
                    fnResetDataTableBasic('tEvento', 2, 'asc');
                    if (sw) { fnLoading(false); }
                    //            fnLoading(false);
                },
                error: function (result) {
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
                //            $('#hdcod').remove();
                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Evento.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            $("#cboConvocatoria").val($("#cboConvocatoriaR").val())

                            Limpiar()
                            fnBuscarEvento(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function (result) {
                        console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            } else {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $('#hdcod').val(0);
                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Evento.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscarEvento(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function (result) {
                        //            console.log(result)
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
            url: "../DataJson/crm/Evento.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                console.log(data);
                $("#cboConvocatoriaR").val(data[0].cCon);
                $('#cboActividadPOAR').val(data[0].cAcp);
                $("#txtnombre").val(data[0].cEvento);
                $("#txtdetalle").val(data[0].cDescripcion);
                $("#txtfecini").val(data[0].cFecini);
                $("#txtfecfin").val(data[0].cFecfin);
                $("#txtfecini").datepicker().datepicker("setDate", data[0].cFecini);
                $("#txtfecfin").datepicker().datepicker("setDate", data[0].cFecfin);
                if (data[0].cEstado == 1) {
                    $("#chkestado").prop("checked", true);
                } else {
                    $("#chkestado").prop("checked", false);
                }

                //if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
                //            console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnEliminar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //        fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Evento.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarEvento(false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function (result) {
                //            console.log(result)
                fnMensaje("warning", result)
            }
        });
        //        fnLoading(false)
    } else {
        window.location.href = rpta
    }
}

var aDataR = [];
function fnDelete(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar Evento?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //    fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}


function Limpiar() {
    $("#cboConvocatoriaR").prop('selectedIndex', 0);
    $('#cboActividadPOAR').prop('selectedIndex', 0);
    $("#txtnombre").val("");
    $("#txtdetalle").val("");
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
    $("#chkestado").prop("checked", true);
}

function fnValidar() {
    if ($("#cboConvocatoriaR").val() == '') {
        fnMensaje("warning", 'Seleccione una Convocatoria')
        return false
    }
    if ($("#cboActividadPOAR").val() == '') {
        fnMensaje("warning", 'Seleccione una Actividad POA')
        return false
    }
    if ($("#txtnombre").val() == '') {
        fnMensaje("warning", 'Ingrese Nombre')
        return false
    }
    if ($("#txtdetalle").val() == '') {
        fnMensaje("warning", 'Ingrese Detalle')
        return false
    }

    if ($("#txtfecini").val() == '') {
        fnMensaje("warning", 'Ingrese Fecha de Inicio')
        return false
    }
    if ($("#txtfecfin").val() == '') {
        fnMensaje("warning", 'Ingrese Fecha de Finalización')
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

