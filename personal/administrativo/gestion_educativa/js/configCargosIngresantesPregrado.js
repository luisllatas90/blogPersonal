var flagDocumentReady = false;
var $mdlMensajeServidor = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $('body').on('click', '#btnAgregarFila', function (e) {
        agregarFilaDinamica();
    });

    var jsonString = $('#hddJsonConfig').val();
    cargarFormDinamico(JSON.parse(jsonString));

    verificarCambiosAjax();
});

function verificarCambiosAjax() {
    if (flagDocumentReady) {
        verificarToastrServer();
        verificarMensajeServer();
    }
}

function initSelectPicker($control, opciones) {
    $control.selectpicker(opciones);
    $control.selectpicker('refresh');
}

function initDatepicker($control, opciones) {
    $control.datepicker(opciones);
}

// GENERACIÓN DINÁMICA DE FORMULARIO
// ------------------------------------------------------------------------
function cargarFormDinamico(configs) {
    for (var i = 0; i < configs.length; i++) {
        var config = configs[i];
        agregarFilaDinamica(config);
    }
}

function agregarFilaDinamica(config) {
    var $container = $('#form-dinamico');
    var $newForm = $('#row-template').clone();

    var index = $('#form-dinamico .form-group').length;
    $newForm.attr('id', 'form-' + index);
    $newForm.removeClass('d-none');

    var $dtpFechaPeriodo = $newForm.find('#dtpFechaPeriodo');
    generarAtributosForm($dtpFechaPeriodo, index);
    initDatepicker($dtpFechaPeriodo, {
        language: 'es',
        orientation: 'top'
    });

    var $txtImporte = $newForm.find('#txtImporte');
    generarAtributosForm($txtImporte, index);

    var $dtpFechaVencimiento = $newForm.find('#dtpFechaVencimiento');
    generarAtributosForm($dtpFechaVencimiento, index);
    initDatepicker($dtpFechaVencimiento, {
        language: 'es',
        orientation: 'top'
    });

    var $cmbCategoriaEscuela = $newForm.find('#cmbCategoriaEscuela');
    generarAtributosForm($cmbCategoriaEscuela, index);
    initSelectPicker($cmbCategoriaEscuela);

    if (config != undefined) {
        $dtpFechaPeriodo.val(config.fechaInicioPeriodo);
        $txtImporte.val(config.importe);
        $dtpFechaVencimiento.val(config.fechaVencimiento);
    }

    $container.append($newForm);
}

function generarAtributosForm($control, index) {
    $control.attr('id', $control.attr('id') + '_' + index);
    $control.attr('name', $control.attr('name') + '_' + index);
}

// ------------------------------------------------------------------------

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

function atenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function alternarLoadingGif(tipo, retorno) {
    if (flagDocumentReady) {
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