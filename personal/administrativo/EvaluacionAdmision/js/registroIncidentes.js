var flagDocumentReady = false;
var $mdlMensaje = undefined;
var $mdlMensajeClienteConfirmar = undefined;
var $tabLista = undefined;
var $tabMantenimiento = undefined;
var $btnAccionPorConfirmar = undefined;
var callbackConfirmar = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    // Inicializo controles genéricos
    $loadingGif = $('#loadingGif');
    $tabLista = $('#nav-lista-tab');
    $tabMantenimiento = $('#nav-mantenimiento-tab');

    // Inicializo modales
    $mdlMensaje = $('#mdlMensaje');
    $mdlMensaje.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajeClienteConfirmar = $('#mdlMensajeClienteConfirmar');
    $mdlMensajeClienteConfirmar.modal({
        backdrop: 'static',
        show: false,
    });

    // Eventos
    $mdlMensajeClienteConfirmar.find('#btnConfContinuar').on('click', function (e) {
        atenuarBoton($(this).attr('id'));
        callbackConfirmar();
    });

    $mdlMensajeClienteConfirmar.on('hide.bs.modal', function (e) {
        callbackConfirmar = undefined;
        $btnAccionPorConfirmar.prop('disabled', false);
    });
    initFakeListener('btnFakeEliminar', 'btnEliminar');

    configPlugins();
    initFilterPlugins();
});

function configPlugins() {
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

function initFilterPlugins() {
    initSelectPicker('cmbFiltroCicloAcademico', {
        liveSearch: true,
        size: 10
    });

    initSelectPicker('cmbFiltroCentroCosto', {
        liveSearch: true,
        size: 10
    });
}

function initFormPlugins() {
    initSelectPicker('cmbCicloAcademico', {
        liveSearch: true,
        size: 10
    });

    initSelectPicker('cmbCentroCosto', {
        liveSearch: true,
        size: 10
    });

    initSelectPicker('cmbGrupoAdmision', {
        liveSearch: true,
        size: 10
    });
}

function initDatepicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.datepicker(opciones);
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
    var tipos = tipoConsulta.split('|');

    for (var tipo of tipos) {
        switch (tipo) {
            case 'MEN_SERV':
                verificarMensajeServer();
                break;
            case 'TOASTR':
                verificarToastrServer();
                break;
            case 'TAB':
                var tipoVista = $('#hddTipoVista').val();
                seleccionarTab(tipoVista)
                initFormPlugins();
                break;
        }
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

function initFakeListener(fakeId, id) {
    $('body').on('click', '[id*="' + fakeId + '"]', function (e) {
        $btnAccionPorConfirmar = $(this);

        hideGrvTooltips();
        atenuarBoton($(this).attr('id'));
        
        var $realButton = $(this).closest('td').find('[id*="' + id + '"]');
        callbackConfirmar = function () {
            $realButton[0].click();
        }

        var msg = '';
        var tipo = 0;
        switch (id) {
            case 'btnActivar':
                msg = '¿Realmente desea realizar la operación: <b>Activar</b>?';
                break;
            case 'btnDesactivar':
                msg = '¿Realmente desea realizar la operación: <b>Desactivar</b>?';
                break;
            case 'btnEliminar':
                msg = '¿Realmente desea realizar la operación: <b>Eliminar</b>?. Tenga en cuenta que esta operación no es reversible.';
                tipo = -1;
                break;
        }
        mostrarModalConfirmar(tipo, msg);
    });
}

function accionConfirmadaFinalizada() {
    atenuarBoton('btnConfContinuar', true);
    $mdlMensajeClienteConfirmar.modal('hide');
}

function hideGrvTooltips() {
    $('#grvList [data-toggle="tooltip"]').tooltip('hide');
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
    if($boton.is('button')) {
        $boton.prop('disabled', !retorno);
    }

    if($boton.is('a')){
        if(retorno) {
            $boton.removeClass('disabled');
        } else {
            $boton.addClass('disabled');
        }
    }
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
        var mostrar = $('#hddMenServMostrar').val();
        var rpta = parseInt($('#hddMenServRpta').val());

        if (mostrar == 'true') {
            var msg = $('#hddMenServMensaje').val();
            mostrarMensajeModal(rpta,msg);
        }
    }
}

function mostrarMensajeModal(rpta, msg) {
    if (flagDocumentReady) {

        var $divMdlMensaje = $mdlMensaje.find('#divMdlMensaje');
        $divMdlMensaje.removeClass('alert alert-danger alert-warning alert-success');
        $divMdlMensaje.html(msg);

        switch (rpta) {
            case -1:
                $divMdlMensaje.addClass('alert alert-danger');
                break;
            case 0:
                $divMdlMensaje.addClass('alert alert-warning');
                break;
            case 1:
                $divMdlMensaje.addClass('alert alert-success');
                break;
        }
        $mdlMensaje.modal('show');
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