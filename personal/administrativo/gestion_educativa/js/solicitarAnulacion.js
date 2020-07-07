var flagDocumentReady = false;
var $mdlMensajesServidor;
var $mdlMensajesCliente;
var callbackMensajeCliente;
var callbackMensajeServidor;

$(document).ready(function () {
    $mdlMensajesServidor = $('#mdlMensajesServidor');
    $mdlMensajesServidor.modal({
        backdrop: 'static',
        show: false,
        keyboard: false,
    });

    $mdlMensajesCliente = $('#mdlMensajesCliente');
    $mdlMensajesCliente.modal({
        backdrop: 'static',
        show: false,
    });
    $mdlMensajesCliente.on('hide.bs.modal', function () {
        if (callbackMensajeCliente != undefined) {
            callbackMensajeCliente();
        }
        callbackMensajeCliente = undefined;
    });

    $('body').on('click', '#btnCancelar', function () {
        $(this).prop('disabled', true);
        parent.$('body').trigger('formCanceled');
    });

    $('body').on('keypress', '.decimal', function (e) {
        var digitos = $(this).data('decimales');
        digitos = (digitos == undefined ? 0 : digitos);
        var result = floatNumber(e, this, digitos);
        if (!result) {
            e.preventDefault();
        }
    });

    $('body').on('focusin', 'input[id$="txtCantidad"]', function () {
        $(this).data('prev-value', $(this).val());
    });

    $('body').on('keyup', 'input[id$="txtCantidad"]', function () {
        if (ValidarMontos($(this))) {
            CalcularTotal();
        }
    });

    flagDocumentReady = true;
});

function ValidarMontos($element) {
    var saldo = parseFloat($('#txtSaldo').val());
    $('body').find('#grwDeudas input[id$="txtCantidad"]').each(function () {
        var total = 0.0;
        $('body').find('#grwDeudas input[id$="txtCantidad"]').each(function () {
            if ($(this).attr('id') != $element.attr('id')) {
                total += parseFloat($(this).val());
            }
        });

        var $txtCantidad = $(this);
        var cantidad = parseFloat($txtCantidad.val());
        if (cantidad == 0) {
            MostrarMensajesCliente(0, 'Debe ingresar un monto mayor a 0');
            callbackMensajeCliente = function () {
                $txtCantidad.val($txtCantidad.data('prev-value'));
                CalcularTotal();
            }
            return false;
        }

        total += cantidad;
        if (total > saldo) {
            MostrarMensajesCliente(0, 'El monto no puede ser mayor al saldo: ' + saldo.toFixed(2));
            callbackMensajeCliente = function () {
                $txtCantidad.val($txtCantidad.data('prev-value'));
                CalcularTotal();
            }
            return false;
        }
    });
    return true;
}

function CalcularTotal() {
    var total = 0.0;
    $('body').find('#grwDeudas input[id$="txtCantidad"]').each(function () {
        var cantidad = $(this).val();
        if (cantidad != '') {
            cantidad = parseFloat(cantidad)
            total += cantidad;
        }
    });
    $('body').find('#grwDeudas input[id$="txtTotal"]').val(total.toFixed(2));
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function RevisarRespuestaServidor() {
    if (!flagDocumentReady) {
        return false;
    }
    var respuestaServidor = {
        rpta: '',
        msg: '',
    }
    var $mdlMenServparametros = $mdlMensajesServidor.find('#divMdlMenServParametros');
    if ($mdlMenServparametros.data('mostrar')) {
        var $respuestaPostback = $mdlMensajesServidor.find('#respuestaPostback');
        respuestaServidor = {
            rpta: $respuestaPostback.data('rpta'),
            msg: $respuestaPostback.data('msg'),
        }
        MostrarMensajeServidor(respuestaServidor.rpta, respuestaServidor.msg);
    }
}

function MostrarMensajeServidor(rpta, msg, control) {
    var $respuestaPostback = $mdlMensajesServidor.find('#respuestaPostback');
    $respuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $respuestaPostback.addClass('alert-danger');
            break;
        case 0:
            if (control != '') {
                callbackMensajeServidor = function () { $('#' + control).focus() }
            }
            $respuestaPostback.addClass('alert-warning');
            break;
        case 1:
            $respuestaPostback.addClass('alert-success');
            break;
    }
    $respuestaPostback.html(msg);
    $mdlMensajesServidor.modal('show');
}

function MostrarMensajesCliente(rpta, msg) {
    var $textoMensaje = $mdlMensajesCliente.find('#textoMensaje');
    $textoMensaje.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $textoMensaje.addClass('alert-danger');
            break;
        case 0:
            $textoMensaje.addClass('alert-warning');
            break;
        case 1:
            $textoMensaje.addClass('alert-success');
            break;
    }
    $textoMensaje.html(msg);
    $mdlMensajesCliente.modal('show');
}

function SubmitPostBack() {
    parent.$('body').trigger('formSubmited');
}