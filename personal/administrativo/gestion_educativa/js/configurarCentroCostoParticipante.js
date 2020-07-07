var $mdlActive;
var $mdlMensajesCliente;

$(document).ready(function () {
    InitCombosSelectpicker();

    $('body').animate({
        opacity: 1
    }, 150);

    var selChkServicioConcepto = 'input[type="checkbox"][id$="chkServicioConcepto"]';
    $('body').on('change', selChkServicioConcepto, function (e) {
        $(this).closest('#grwServicioConcepto').find(selChkServicioConcepto).prop('disabled', true);
    });

    $mdlMensajesCliente = $('#mdlMensajesCliente');
    $mdlMensajesCliente.modal({
        backdrop: 'static',
        show: false,
    });
    $mdlMensajesCliente.on('hide.bs.modal', function () {
        if (callbackHideMdlMensajes != undefined) {
            callbackHideMdlMensajes();
        }
        callbackHideMdlMensajes = undefined;
    });

    $('body').on('formProcessing', function (e, data) {
        AtenuarModalActivo(false);
    });

    $('body').on('formSubmited', function (e, data) {
        OnPostBack(data);
        AtenuarModalActivo(true);
    });
});

function OnPostBack(data) {
    $ifrmActive.animate({
        opacity: 1
    }, 125, function () { });
    var $respuestaPostback = $ifrmActive.contents().find('#respuestaPostback');

    if (data.rpta == 1) {
        $mdlActive.modal('hide');
        MostrarMensajesCliente(data.rpta, data.msg);

        var postbackListar = $mdlActive.data('postback-listar');
        if (postbackListar === true) {
            callbackHideMdlMensajes = function () {
                $('#btnListarServicios').trigger('click');
            }
        }
    }
}

function InitCombosSelectpicker() {  
    InitComboTipoEstudio();
    InitComboCentroCosto();
}

function InitComboTipoEstudio() {  
    $('#cmbTipoEstudio').selectpicker();
}

function InitComboCentroCosto() {
    $('#cmbCentroCosto').selectpicker({
        liveSearch: true,
    });
}

function AlternarLoadingGif(tipo, retorno) {
    var $loadingGif = $('#loading-gif');
    switch (tipo) {
        case 'global':
            $loadingGif = $('.main .loading-gif');
            break;
        case 'interno':
            $loadingGif = $('.servicios .card-header .loading-gif');
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

function MostrarMensajesCliente(rpta, msg) {
    var $mensajePostBack = $mdlMensajesCliente.find('#mensajePostBack');
    $mensajePostBack.removeClass(function (index, className) {
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
    $mdlMensajesCliente.modal('show');
}

function MostrarModalDetalleConvenio(btnCallerId, btnIdForTriggerClick) {
    $mdlActive = $('#mdlDetalleConvenio');
    var modalProperties = {
        backdrop: 'static',
        show: false
    }
    LoadModal(modalProperties, btnCallerId, btnIdForTriggerClick)
}

function LoadModal(modalProperties, btnIdParentCaller, btnIdForTriggerClick) {
    $mdlActive.modal(modalProperties);

    var $btnSubmit = $mdlActive.find('.submit');
    $ifrmActive = $mdlActive.find('iframe');

    var routine = function () {
        AlternarLoadingGif('interno', true);
        $mdlActive.modal('show');

        if (btnIdParentCaller != undefined) {
            AtenuarBoton(btnIdParentCaller, true)
        }

        if (btnIdForTriggerClick != undefined) {
            $btnSubmit.on('click', function () {
                $(this).prop('disabled', true);
                $ifrmActive.contents().find('#' + btnIdForTriggerClick).trigger('click');
            });
        }
    }

    if ($ifrmActive.length == 0) {
        routine()
    } else {
        $ifrmActive.iFrameResize();
        $ifrmActive.on('load', function () {
            routine();
        });
    }
}

function AtenuarModalActivo(retorno) {
    if (retorno) {
        $ifrmActive.animate({
            opacity: 1
        }, 50, function () { })
        $mdlActive.find('.submit').prop('disabled', false);
    } else {
        $ifrmActive.animate({
            opacity: 0.5
        }, 50, function () { });
    }
}