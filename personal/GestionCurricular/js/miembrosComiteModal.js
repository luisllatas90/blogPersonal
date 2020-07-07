var flagDocumentReady = false;
var callbackMdlMensajeServidor = undefined;

$(document).ready(function() {
    flagDocumentReady = true;
    
    $('#mdlMensajeServidor').modal({
        backdrop: 'static',
        show: false
    });

    $('#mdlMensajeServidor').on('hidden.bs.modal', function() {
        if (callbackMdlMensajeServidor != undefined) {
            callbackMdlMensajeServidor();
        }
        callbackMdlMensajeServidor = undefined;

        var nombreControl = $('#respuestaPostback').data('control');
        if (nombreControl != undefined && nombreControl != '') {
            $('#' + nombreControl).focus();
        }
    });
});

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function SubmitPostBack() {
    var $mdlMensajeServidor = $('#mdlMensajeServidor');
    var $respuestaPostback = $mdlMensajeServidor.find('#respuestaPostback')

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');
    var control = $respuestaPostback.data('control');

    $respuestaPostback.removeClass(function(index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $respuestaPostback.addClass('alert-danger');
            $respuestaPostback.html(msg);
            $mdlMensajeServidor.modal('show');
            break;
        case 0:
            callbackMdlMensajeServidor = function() { $('#' + control).focus() }
            $respuestaPostback.addClass('alert-warning');
            $respuestaPostback.html(msg);
            $mdlMensajeServidor.modal('show');
            break;
        case 1:
            break;
    }
    parent.$('body').trigger('formSubmited');
}
