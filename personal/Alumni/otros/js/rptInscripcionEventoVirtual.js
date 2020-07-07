var flagDocumentReady = false;
var $mdlMensajeServidor = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    $loadingGif = $('#loadingGif');

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $('body').on('click', '#btnFakeListar', function (e) {
        $('#grvInscripcionEvento').animate({
            'opacity': 0.5
        }, 150);
        __doPostBack('btnListar', '');
    });

    $('body').on('click', '#btnExportar', function (e) {
        if ($('#grvInscripcionEvento tr').length > 0) {
            atenuarBoton('btnExportar', false);
            alternarLoadingGif('global', false);

            setTimeout(function () {
                exportar('grvInscripcionEvento', 'xlsx');
                atenuarBoton('btnExportar', true);
                alternarLoadingGif('global', true);
            }, 500);

        } else {
            toastr.warning('No hay datos para exportar');
        }
    });

    initDatepicker('dtpFechaDesde', {
        language: 'es'
    });

    initDatepicker('dtpFechaHasta', {
        language: 'es'
    });
});

function exportar(id, type, fn, dl) {
    var elt = document.getElementById(id);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "Sheet JS" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || ('INSCRIPCIONES.' + (type || 'xlsx')));
}

function alternarLoadingGif(tipo, retorno) {
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

function atenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function initDatepicker(controlId, opciones) {
    $('#' + controlId).datepicker(opciones);
}

function verificarCambiosAjax() {
    verificarToastrServer();
    verificarMensajeServer();
}

function verificarToastrServer() {
    var paramsToastr = $('#hddParamsToastr').val();
    if (paramsToastr != '') {
        var rpta, msg, control;

        var datos = paramsToastr.split('|');
        for (var i = 0; i < datos.length; i++) {
            var valores = datos[i].split('=');

            switch (valores[0]) {
                case 'rpta':
                    rpta = valores[1];
                    break;
                case 'msg':
                    msg = valores[1];
                    break;
                case 'control':
                    control = valores[1];
                    break;
            }
        }
        mostrarToastr(rpta, msg, control);
    }
}

function mostrarToastr(tipo, mensaje, control) {
    switch (tipo) {
        case '1':
            toastr.success(mensaje);
            break;
        case '0':
            toastr.warning(mensaje);
            break;
        case '-1':
            toastr.error(mensaje);
            break;
    }

    if (control) {
        var selector = '#' + control;
        var $control = $(selector);
        $control.focus();
    }
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