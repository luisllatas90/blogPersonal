var flagDocumentReady = false;
var $mdlMensajeServidor = undefined;
var $mdlMensajeClienteConfirmar = undefined;
var $tabLista = undefined;
var $tabMantenimiento = undefined;
var $ifrmConfigCargos = undefined;
var $ifrmFiltrosAlumno = undefined;
var callbackConfirmar = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    initPlugins();

    $loadingGif = $('#loadingGif');
    $tabLista = $('#nav-lista-tab');
    $tabMantenimiento = $('#nav-mantenimiento-tab');

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajeClienteConfirmar = $('#mdlMensajeClienteConfirmar');
    $mdlMensajeClienteConfirmar.modal({
        backdrop: 'static',
        show: false,
    });

    verificarCambiosAjax();
});

function initPlugins() {
    $.fn.datetimepicker.Constructor.Default = $.extend({}, $.fn.datetimepicker.Constructor.Default, {
        locale: 'es',
        icons: {
            time: 'fa fa-clock',
            date: 'fa fa-calendar',
            up: 'fa fa-arrow-up',
            down: 'fa fa-arrow-down',
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-calendar-check-o',
            clear: 'fa fa-trash',
            close: 'fa fa-times'
        }
    });

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function initFormDataPlugins() {
    initFrameFiltrosAlumno();
    initSelectPicker('cmbCicloAcademico', {
        size: 6
    });
    initSelectPicker('cmbTipoProceso');
}

function initFrameFiltrosAlumno() {
    $ifrmFiltrosAlumno = $('#ifrmFiltrosAlumno');
    $ifrmFiltrosAlumno.iFrameResize();
}

function initFrameConfigCargos() {
    $ifrmConfigCargos = $('#ifrmConfigCargos');
    $ifrmConfigCargos.iFrameResize({
        // autoResize: false
        // minHeight: 200
    });
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

function initSelectPicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.selectpicker(opciones);
    $control.selectpicker('refresh');
}

function verificarCambiosAjax() {
    if (flagDocumentReady) {
        verificarParametros('TAB');
        verificarToastrServer();
        verificarMensajeServer();
    }
}

function verificarParametros(tipoConsulta) {
    switch (tipoConsulta) {
        case 'TAB':
            var tipoVista = $('#hddTipoVista').val();
            seleccionarTab(tipoVista)
            initFormDataPlugins();
            break;
    }
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

function seleccionarTab(tipoVista) {
    switch (tipoVista) {
        case 'L':
            if ($tabLista == undefined) {
                alert('NO DEFINIDO!');
            }
            $tabLista.removeClass('disabled');
            $tabLista.tab('show');
            $tabMantenimiento.addClass('disabled');
            break;
        case 'M':
            $tabMantenimiento.removeClass('disabled');
            $tabMantenimiento.tab('show');
            $tabLista.addClass('disabled');
            break;
    }
}