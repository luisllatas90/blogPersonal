var flagDocumentReady = false;
$(document).ready(function () {
    flagDocumentReady = true;
    InicializarControles();

    $('#mdlMensajes').modal({
        backdrop: 'static',
        show: false,
    });

    $('#mdlMensajes').on('hide.bs.modal', function () {
        if (callbackHideMdlMensajes != undefined) {
            callbackHideMdlMensajes();
        }
        callbackHideMdlMensajes = undefined;
    });

    $('#dtpFecNacimiento').datepicker({
        language: 'es'
    });
});

function InicializarControles() {
    if (flagDocumentReady) {
        $('#cmbInstitucionEducativa').selectpicker({
            size: 6,
            liveSearch: true,
        });

        $('#cmbCarreraProfesional').selectpicker({
            size: 6,
            liveSearch: true,
        });
    }
}

var callbackHideMdlMensajes = undefined;

function SubmitPostBack() {
    var $respuestaPostback = $('#respuestaPostback');
    var $mdlMensajes = $('#mdlMensajes');
    var $mensajePostBack = $mdlMensajes.find('#mensajePostBack')

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');
    var control = $respuestaPostback.data('control');

    $mensajePostBack.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $mensajePostBack.addClass('alert-danger');
            $mensajePostBack.html(msg);
            $mdlMensajes.modal('show');
            break;
        case 0:
            callbackHideMdlMensajes = function(){$('#' + control).focus()}
            $mensajePostBack.addClass('alert-warning');
            $mensajePostBack.html(msg);
            $mdlMensajes.modal('show');
            break;
        case 1:
    }
    parent.$('body').trigger('formSubmited');
}