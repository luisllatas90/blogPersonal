$(document).ready(function() {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tDefensoria', 0, 'asc');
    var dt = fnCreateDataTableBasic('tConsultaDefensoria', 0, 'asc');

    ope = fnOperacion(1);
    rpta = fnvalidaSession()
    fnBuscar(false);
    fnBuscarConsulta(false);
    //alert(rpta)
    if (rpta == true) {
        $('#btnListar').click(fnBuscar);
        $('#btnGuardar').click(fnGuardar);

        $('#btnListarConsulta').click(fnBuscarConsulta);
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $('#mdRegistro').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        //alert('--')
        $("#btnGuardar").removeAttr("disabled");
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


function fnBuscar(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        //fnLoading(true)

        $("form#frmBuscarDefensoria input[id=action]").remove();
        $('#frmBuscarDefensoria').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        var form = $("#frmBuscarDefensoria").serializeArray();
        $("form#frmBuscarDefensoria input[id=action]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/Defensoria/Defensoria.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                    tb += '<td>' + data[i].cFecha + '</td>';
                    tb += '<td>' + data[i].cTelefono + '</td>';
                    tb += '<td>' + data[i].cMail + '</td>';
                    tb += '<td>' + data[i].cDetalle + '</td>';
                    tb += '<td>' + data[i].cTipoNombre + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';
                    tb += '<button type="button" id="btnD" name="btnD" class="btn btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tDefensoria');
                $('#tbDefensoria').html(tb);
                fnResetDataTableBasic('tDefensoria', 0, 'asc');
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function(result) {
                console.log(result)
                if (sw) { fnLoading(false); }
            }
        });
    } else {
        window.location.href = rpta
    }
}


function validate() {
    if ($("#cboTipoR").val() == "") {
        fnMensaje("error", "Seleccione Tipo");
        return false;
    }

    if ($("#txtTelefono").val() == "") {
        fnMensaje("error", "Ingrese Teléfono");
        return false;
    }

    if ($("#txtEmail").val() == "") {
        fnMensaje("error", "Ingrese Correo Electrónico");
        return false;
    }

    var numero = $("#txtTelefono").val()
    if (isNaN(numero) || numero % 1 != 0 || numero <= 0) {
        fnMensaje("error", "Solo se Aceptan Números");
        $("#txtTelefono").focus();
        return false;
    }

    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    if (emailRegex.test($("#txtEmail").val())) {
        return true;
    } else {
        fnMensaje("error", "Ingrese Correctamente Correo Electrónico");
        return false;
    }

    return true;
}


function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (validate() == true) {
            fnLoading(true);
            $("#btnGuardar").removeAttr("disabled");
            $("#btnGuardar").attr("disabled", "disabled");
            if ($("#hdcod").val() == 0) {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();

                //$('#hdcod').remove();
                $.ajax({
                    type: "POST",
                    url: "../DataJson/Defensoria/Defensoria.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
//                        console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscar(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        console.log(result)
                        fnMensaje("warning", result)
                    }
                });
//                $("#btnGuardar").removeAttr("disabled");
            } else {

                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $('#hdcod').val(0);
                ////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/Defensoria/Defensoria.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscar(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                        $("#btnGuardar").removeAttr("disabled");
                        fnLoading(false)
                    },
                    error: function(result) {
                        //console.log(result)
                    fnMensaje("warning", result)
                    $("#btnGuardar").removeAttr("disabled");
                    }
                });
                fnLoading(false)
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
            url: "../DataJson/Defensoria/Defensoria.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //                console.log(data);
                $("#txtTelefono").val(data[0].cTelefono);
                $("#txtEmail").val(data[0].cMail);
                $("#txtDetalle").val(data[0].cDetalle);
                $("#cboTipoR").val(data[0].cTipo);

                //if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)

                //                console.log(result)
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
            url: "../DataJson/Defensoria/Defensoria.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscar(false);
                } else {
                    fnMensaje("warning", data[0].msje)
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


//******************************************************************************************************


function fnBuscarConsulta(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        //fnLoading(true)

        $("form#frmBuscarConsultaDefensoria input[id=action]").remove();
        $('#frmBuscarConsultaDefensoria').append('<input type="hidden" id="action" name="action" value="' + ope.cons + '" />');
        var form = $("#frmBuscarConsultaDefensoria").serializeArray();
        $("form#frmBuscarConsultaDefensoria input[id=action]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/Defensoria/Defensoria.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                    tb += '<td>' + data[i].cFecha + '</td>';
                    tb += '<td>' + data[i].cTelefono + '</td>';
                    tb += '<td>' + data[i].cMail + '</td>';
                    tb += '<td>' + data[i].cDetalle + '</td>';
                    tb += '<td>' + data[i].cTipo + '</td>';
                    tb += '<td>' + data[i].cPersona + '</td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tConsultaDefensoria');
                $('#tbConsultaDefensoria').html(tb);
                fnResetDataTableBasic('tConsultaDefensoria', 0, 'asc');
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function(result) {
                console.log(result)
                if (sw) { fnLoading(false); }
            }
        });
    } else {
        window.location.href = rpta
    }
}


//******************************************************************************************************

var aDataR = [];
function fnDelete(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar la Defensoría?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}


function Limpiar() {
    $("#txtTelefono").val("");
    $("#txtEmail").val("");
    $("#txtDetalle").val("");
}


function fnCreateDataTableBasic(table, col, ord) {
    var dt = $('#' + table).DataTable({
    //"sPaginationType": "full_numbers",
        "bPaginate": false,
        "bLengthChange": false,
        "bAutoWidth": true,
        //"aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
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
        //"sPaginationType": "full_numbers",
            "bPaginate": false,
            "bLengthChange": false,
            "bAutoWidth": true,
            //"aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
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

