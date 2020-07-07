var flagDocumentReady = false;
var callbackPostback = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    $('#mdlMensajeServidor').modal({
        backdrop: 'static',
        show: false
    });

    $('#mdlMensajeServidor').on('hidden.bs.modal', function () {
        if (callbackMensajeClose != undefined) {
            callbackMensajeClose();
        }
    });

    $("input[type=file]").change(function () {
        var fieldVal = $(this).val();
        var $customFileLabel = $(this).next(".custom-file-label");

        // Change the node's value by removing the fake path (Chrome)
        fieldVal = fieldVal.replace("C:\\fakepath\\", "");

        if (fieldVal != undefined && fieldVal != "") {
            $customFileLabel.attr('data-content', fieldVal);
        } else {
            $customFileLabel.attr('data-content', fieldVal);
            fieldVal = 'Seleccione un archivo';
        }
        $customFileLabel.text(fieldVal);
    });

    InicializarCombos();
    InicializarProcesos();
});

var callbackMensajeClose = undefined;

function InicializarCombos() {  
    $('#cmbTipoEstudio').selectpicker({
        'size': 10,
    });

    $('#cmbCentroCosto').selectpicker({
        'size': 10,
    });
}

function InicializarProcesos() {
    if (flagDocumentReady) {
        var $respuestaPostback = $('#mdlMensajeServidor #respuestaPostback');
        if ($respuestaPostback.data('enviado')) {
            PostBack(callbackPostback);
        }
    }
}

function Validando() {
    AtenuarBoton('btnValidar', false);
    AlternarLoadingGif('global', false);
    callbackMensajeClose = function () {
        AtenuarBoton('btnValidar', true);
        callbackMensajeClose = undefined;
    }
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function AlternarLoadingGif(tipo, retorno) {
    var $loadingGif = undefined;
    switch (tipo) {
        case 'global':
            $loadingGif = $('#loading-gif');
            break;
        case 'interno':
            $loadingGif = $('#contentTabs .tab-pane.active .loading-gif');
            break;
    }

    if ($loadingGif != undefined) {
        if (!retorno) {
            $loadingGif.fadeIn(150);
        } else {
            $loadingGif.fadeOut(150);
        }
    }
}

function CallbackValidacion() {
    callbackPostback = function () {
        __doPostBack('btnEnviar', '');
    };
    PostBack(callbackPostback);
}

function PostBack(callbackSuccess) {
    AlternarLoadingGif('global', true);

    var $mdlMensajeServidor = $('#mdlMensajeServidor');
    var $respuestaPostback = $mdlMensajeServidor.find('#respuestaPostback');

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');
    var control = $respuestaPostback.data('control');

    $respuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $respuestaPostback.addClass('alert-danger');
            $respuestaPostback.html(msg);
            $mdlMensajeServidor.modal('show');
            break;
        case 0:
            callbackMdlMensajeServidor = function () { $('#' + control).focus() }
            DecorarControles();
            $respuestaPostback.addClass('alert-warning');
            $respuestaPostback.html(msg);
            $mdlMensajeServidor.modal('show');
            break;
        case 1:
            $respuestaPostback.addClass('alert-success');
            if (callbackSuccess != undefined) {
                callbackSuccess();
            } else {
                $respuestaPostback.html(msg);
                $mdlMensajeServidor.modal('show');
            }
            break;
    }
}

function DecorarControles() {
    $('#frmInscripcion input').each(function (index, value) {
        var dataError = $(this).data('error');
        if (dataError != undefined) {
            if (dataError) {
                $(this).removeClass('is-valid');
                $(this).addClass('is-invalid');
            } else {
                $(this).removeClass('is-invalid');
                $(this).addClass('is-valid');
            }
        }
    });
}