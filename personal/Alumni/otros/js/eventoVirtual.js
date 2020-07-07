var flagDocumentReady = false;
var $mdlMensajeServidor = undefined;
var $mdlMensajeClienteConfirmar = undefined;
var $tabLista = undefined;
var $tabMantenimiento = undefined;
var callbackConfirmar = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    $loadingGif = $('#loadingGif');
    $tabLista = $('#nav-lista-tab');
    $tabMantenimiento = $('#nav-mantenimiento-tab');

    $.fn.datetimepicker.Constructor.Default = $.extend({}, $.fn.datetimepicker.Constructor.Default, {
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

    $('body').on('click', 'button[id*="btnFakeEliminar"]', function (e) {  
        atenuarBoton($(this).attr('id'));
        var $realButton = $(this).closest('td').find('button[id*="btnEliminar"]');
        callbackConfirmar = function () {
            $realButton.trigger('click');
        }
        mostrarModalConfirmar(0, '¿Realmente desea realizar esta operación?');
    });

    $mdlMensajeClienteConfirmar.find('#btnConfContinuar').on('click', function (e) {
        atenuarBoton($(this).attr('id'));
        callbackConfirmar();
    });

    $mdlMensajeClienteConfirmar.on('hide.bs.modal', function (e) {
        callbackConfirmar = undefined;
        $('button[id*="btnFakeEliminar"]').prop('disabled', false);
    });

    $('body').on('click', '#btnFakeManGuardar', function (e) {
        if ($('#txtNombre').val().trim() == '') {
            mostrarToastr('0', 'Debe ingresar un nombre');
            $('#txtNombre').focus();
            return false;
        }

        if ($('#dtpFecha').val().trim() == '') {
            mostrarToastr('0', 'Debe ingresar una fecha');
            $('#dtpFecha').focus();
            return false;
        }

        if ($('#dtpHoraInicio').val().trim() == '') {
            mostrarToastr('0', 'Debe ingresar una hora de inicio');
            $('#dtpHoraInicio').focus();
            return false;
        }

        if ($('#txtUrl').val().trim() == '') {
            mostrarToastr('0', 'Debe ingresar un enlace');
            $('#txtUrl').focus();
            return false;
        }

        if ($('#cmbTipo').val().length == 0) {
            mostrarToastr('0', 'Debe seleccionar al menos un tipo de evento');
            $('#cmbTipo').focus();
            return false;
        }

        __doPostBack('btnManGuardar', '');
    });

    initFormPlugins();
    verificarToastrServer();
});

function initFormPlugins() {
    initDatetimePicker('mrkFecha', {
        format: 'DD/MM/YYYY'
    });

    initDatetimePicker('mrkHoraInicio', {
        format: 'HH:mm'
    });

    initDatetimePicker('mrkHoraFin', {
        format: 'HH:mm'
    });

    initSelectPicker('cmbTipo', {
        liveSearch: true,
        size: 2
    });
}

function initDatetimePicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.datetimepicker(opciones);
}

function initSelectPicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.selectpicker(opciones);
    $control.selectpicker('refresh');
}

function verificarParametros(tipoConsulta) {
    switch (tipoConsulta) {
        case 'TAB':
            var tipoVista = $('#hddTipoVista').val();
            seleccionarTab(tipoVista)
            initFormPlugins();
            break;
    }
}

function mostrarModalConfirmar(tipo, msg) {
    var $divMensajeConfirmar = $mdlMensajeClienteConfirmar.find('#divMensajeConfirmar');
    $divMensajeConfirmar.removeClass('alert alert-danger alert-warning alert-success');
    $divMensajeConfirmar.html(msg);

    switch (tipo) {
        case -1:
            $divMensajeConfirmar.addClass('alert alert-danger');
            break;
        case 0:
            $divMensajeConfirmar.addClass('alert alert-warning');
            break;
        case 1:
            $divMensajeConfirmar.addClass('alert alert-success');
            break;
    }
    $mdlMensajeClienteConfirmar.modal('show');
}

function eliminarProcesado() {
    atenuarBoton('btnConfContinuar', true);
    $mdlMensajeClienteConfirmar.modal('hide');
}

function seleccionarTab(tipoVista) {
    switch (tipoVista) {
        case 'L':
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

function verificarToastrServer() {
    var paramsToastr = $('#hddParamsToastr').val();
    if (paramsToastr != '') {
        var rpta, msg;

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
            }
        }
        mostrarToastr(rpta, msg);
    }
}

function mostrarToastr(tipo, mensaje) {
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
}