$(document).ready(function() {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tTipoSesion', 2, 'asc');
    ope = fnOperacion(1);
    ////console.log(ope)
    fnBuscarTipoSesion();
    rpta = fnvalidaSession()
    //alert(rpta)
    if (rpta == true) {
        $('#btnListar').click(fnBuscarTipoSesion);
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
        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            Edit()
        }
    })
})


function fnBuscarTipoSesion(sw) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (sw) { fnLoading(true); }
        //fnLoading(true)
        $("form#frmBuscarTipoSesion input[id=action]").remove();
        $('#frmBuscarTipoSesion').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        var form = $("#frmBuscarTipoSesion").serializeArray();
        $("form#frmBuscarTipoSesion input[id=action]").remove();
        //        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/TipoSesion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //$("form#frmBuscarTipoSesion input[id=action]").remove();
                ////console.log(data);
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
                fnDestroyDataTableDetalle('tTipoSesion');
                $('#tbTipoSesion').html(tb);
                fnResetDataTableBasic('tTipoSesion', 2, 'asc');
                if (sw) { fnLoading(false); }
                //fnLoading(false);
            },
            error: function(result) {
                ////console.log(result)
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
                ////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/TipoSesion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        ////console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscarTipoSesion(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        ////console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            } else {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $('#hdcod').val(0);
                //////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/TipoSesion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            fnBuscarTipoSesion(false);
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        ////console.log(result)
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
            url: "../DataJson/tutoria/TipoSesion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                //console.log(data);
                $("#txtDescripcion").val(data[0].cDescripcion);
                if (data[0].cEstado == 1) {
                    $("#chkestado").prop("checked", true);
                } else {
                    $("#chkestado").prop("checked", false);
                }
                //if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
                ////console.log(result)
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
            url: "../DataJson/tutoria/TipoSesion.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                ////console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarTipoSesion(false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function(result) {
                ////console.log(result)
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
        mensaje: '¿Desea Eliminar el Tipo de Sesión?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}


function Limpiar() {
    $("#txtDescripcion").val("");
    $("#chkestado").prop("checked", true);
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

function ValidaGuardar() {
    if ($("#txtDescripcion").val().replace(/ /g, '') == '') {
        fnMensaje("warning", 'Se necesita una descripción')
        return false
    }

    return true
}