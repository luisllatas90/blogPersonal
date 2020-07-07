var $ifrmActive = undefined;
var $mdlActive = undefined;
var $mdlMensajes = undefined;
var $mdlMensajeServer = undefined;
var callbackHideMdlMensajes = undefined;
var callbackMdlMensajeServer = undefined;

$(document).ready(function() {
    $mdlMensajes = $('#mdlMensajes');
    $mdlMensajes.modal({
        backdrop: 'static',
        show: false
    });
    $mdlMensajes.on('hide.bs.modal', function() {
        if (callbackHideMdlMensajes != undefined) {
            callbackHideMdlMensajes();
        }
        callbackHideMdlMensajes = undefined;
    });

    $mdlMensajeServer = $('#mdlMensajeServer');
    $mdlMensajeServer.modal({
        backdrop: 'static',
        show: false
    });
    $mdlMensajeServer.on('hide.bs.modal', function() {
        if (callbackMdlMensajeServer != undefined) {
            callbackMdlMensajeServer();
        }
        callbackMdlMensajeServer = undefined;
    });

    $('body').on('formSubmiting', function(e) {
        AtenuarModalActivo(false);
    });

    $('body').on('formSubmited', function(e) {
        OnPostBack();
        AtenuarModalActivo(true);
    });

});

function FormularioRegistroComite() {
    var mostrar = $mdlMensajeServer.find('#mensajeServer').data('mostrar');
    if (mostrar) {
        callbackMdlMensajeServer = function() {
            $('#btnRegistrar').prop('disabled', false);
        }
        $mdlMensajeServer.modal('show');
    } else {
        $('#btnRegistrar').prop('disabled', false);
        CargarModal(controlId, 'btnRegistrar');
    }
}

function CargarModal(btnCallerId, btnIdForTriggerClick) {
    $mdlActive = $('#modalComite');
    var modalProperties = {
        backdrop: 'static',
        show: false
    };

    LoadModal($mdlActive, modalProperties, btnCallerId, btnIdForTriggerClick);
}

function LoadModal($modal, modalProperties, btnIdParentCaller, btnIdForTriggerClick) {
    $mdlActive.modal(modalProperties);

    var $btnSubmit = $modal.find('.submit');
    $ifrmActive = $modal.find('iframe');
    //$ifrmActive.length = 0;

    var routine = function() {
        $modal.modal('show');

        if (btnIdParentCaller != undefined) {
            AtenuarBoton(btnIdParentCaller, true);
        }

        if (btnIdForTriggerClick != undefined) {
            $btnSubmit.on('click', function() {
                $(this).prop('disabled', true);
                AtenuarModalActivo(false);
                $ifrmActive.contents().find('#' + btnIdForTriggerClick).trigger('click');
            });
        }
    }

    if ($ifrmActive.length == 0) {
        routine();
    } else {
        $ifrmActive.iFrameResize({
            // heightCalculationMethod: 'lowestElement',
            minHeight: 320,
            //maxHeight: 450,
            autoResize: true
        });

        $ifrmActive.on('load', function() {
            routine();
        });
    }
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function AtenuarModalActivo(retorno) {
    if (retorno) {
        $ifrmActive.animate({
            opacity: 1
        }, 50, function() { })
        $mdlActive.find('.submit').prop('disabled', false);
    } else {
        $ifrmActive.animate({
            opacity: 0.5
        }, 50, function() { });
    }
}

function MostrarMensajeServidor(rpta, msg) {
    try {
        var $mensajePostBack = $mdlMensajes.find('#mensajePostBack');
        $mensajePostBack.removeClass(function(index, className) {
            return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
        });

        switch (rpta) {
            case -1:
                $mensajePostBack.addClass('alert-danger');
                break;
            case 0:
                $mensajePostBack.addClass('alert-warning');
                break;
            case 1:
                $mensajePostBack.addClass('alert-success');
                break;
        }

        $mensajePostBack.html(msg);
        $mdlMensajes.modal('show');
    } catch (err) {
        console.log(err);
        throw err;
    }
}

function OnPostBack() {
    $ifrmActive.animate({
        opacity: 1
    }, 125, function() { });
    var $respuestaPostback = $ifrmActive.contents().find('#respuestaPostback');

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');

    if (rpta == 1) {
        $mdlActive.modal('hide');
        MostrarMensajeServidor(rpta, msg);

        var postbackListar = $mdlActive.data('postback-listar');
        if (postbackListar === true || postbackListar == undefined) {
            callbackHideMdlMensajes = function() {
                //__doPostBack('btnRefrescar', '');
            }
        }
    }
}
