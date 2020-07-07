var callbackMensajeClose = undefined;

$(document).ready(function () {
    $('#mdlMensajeServidor').on('hidden.bs.modal', function () {
        if (callbackMensajeClose != undefined) {
            callbackMensajeClose();
        }
    });
});

function InitComboCentroCosto() {  
    $('#cmbCentroCosto').selectpicker({
        'size': 10,
    });
}

function InitComboCarreraProfesional() {  
    $('#cmbCarreraProfesional').selectpicker({
        'size': 10,
    });
}

function InitComboModalidadIngreso() {  
    $('#cmbModalidadIngreso').selectpicker({
        'size': 10,
    });
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

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    var nodeName = $boton.prop('nodeName');
    if (nodeName != undefined) {
        switch (nodeName){
            case 'A':
                if (retorno) {
                    $boton.removeClass('disabled');
                } else {
                    $boton.addClass('disabled');
                }
                break;
            case 'BUTTON':
                $boton.prop('disabled', !retorno);
                break;
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
