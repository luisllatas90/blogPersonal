var flagDocumentReady = false;

var $mdlMensajeServidor = undefined;
var $loadingGif = undefined;
var outerCallback = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    $loadingGif = $('#loadingGif');

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $('body').on('generarCadenaFiltros', function (e, callback) {
        outerCallback = callback;
        __doPostBack('btnGenerarCadenaFiltros', '');
    });

    initControls();
    visibilidadFiltros();
    verificarMensajeServer();
});

function initControls() {  
    initSelectPicker('cmbTipoEstudio', {
        size: 5
    });
}

function visibilidadFiltros() {
    $('.row-filtros').each(function (i, el) {
        if ($(this).find('.col-filtro:not(.d-none)').length > 0) {
            $(this).removeClass('d-none');
        } else {
            $(this).addClass('d-none');
        }
    });
}

function devolverCadenaFiltros() {
    if (verificarParametrosMensaje(false)) {
        if (outerCallback != undefined) {
            outerCallback({
                cadenaFiltros: $('#hddCadenaFiltros').val(),
            });
        }
    };
}

function devolverErrorFiltros(rpta, msg, control) {
    parent.$('body').trigger('errorFiltrosEnviado', {
        rpta: rpta,
        msg: msg,
        control: control,
    });
}

function atenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function initSelectPicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.selectpicker(opciones);
    $control.selectpicker('refresh');
}

function verificarParametrosMensaje(lanzarEvento) {
    if (flagDocumentReady) {
        var rpta = $('#hddRpta').val();

        if (rpta != "") {
            var msg = $('#hddMsg').val();
            var control = $('#hddControl').val();
            if (lanzarEvento) {
                devolverErrorFiltros(rpta, msg, control);
            }
            return false;
        } else {
            return true;
        }
    }
    return false;
}

function verificarMensajeServer() {
    if (flagDocumentReady) {
        var $divMenServParametros = $mdlMensajeServidor.find('#divMenServParametros');
        var rpta = $divMenServParametros.data('rpta');
        var mostrar = $divMenServParametros.data('mostrar');

        if (mostrar === true) {
            var $divMenServMensaje = $mdlMensajeServidor.find('#divMenServMensaje');
            $divMenServMensaje.removeClass('alert alert-danger alert-warning alert-success');

            switch (rpta) {
                case -1:
                    $divMenServMensaje.addClass('alert alert-danger');
                    break;
                case 0:
                    $divMenServMensaje.addClass('alert alert-warning');
                    break;
                case 1:
                    $divMenServMensaje.addClass('alert alert-success');
                    break;
            }
            $mdlMensajeServidor.modal('show');
        } else {
            $mdlMensajeServidor.modal('hide');
        }
    }
}

function alternarLoadingGif(tipo, retorno) {
    var tipoForm = $('#hddTipoForm').val();

    if (tipoForm == 'N') {
        var $control = undefined;

        switch (tipo) {
            case 'global':
                $control = $loadingGif;
                break;
        }

        if ($control != undefined) {
            if (!retorno) {
                $control.fadeIn(150);
            } else {
                $control.fadeOut(150);
            }
        }
    }
}