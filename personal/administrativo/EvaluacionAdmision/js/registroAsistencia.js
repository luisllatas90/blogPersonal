var flagDocumentReady = false;
var $mdlMensaje = undefined;
var $mdlMensajeClienteConfirmar = undefined;
var $mdlCerrarAsistencias = undefined;
var $mdlRegistrarIncidencia = undefined;
var $mdlVisualizarIncidencias = undefined;
var $tabLista = undefined;
var $tabMantenimiento = undefined;
var $btnAccionPorConfirmar = undefined;
var callbackConfirmar = undefined;
var tblAsistencia = undefined;

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

    $mdlCerrarAsistencias = $('#mdlCerrarAsistencias');
    $mdlCerrarAsistencias.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlRegistrarIncidencia = $('#mdlRegistrarIncidencia');
    $mdlRegistrarIncidencia.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlVisualizarIncidencias = $('#mdlVisualizarIncidencias');
    $mdlVisualizarIncidencias.modal({
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

    $('body').on('click', '#btnFakeGuardar', function (e) {
        $btnAccionPorConfirmar = $(this);
        $btnAccionPorConfirmar.prop('disabled', true);

        var seleccionados = $('#grvAsistencia tr .o-switch input[name*="rbtAsistencia"]:checked').length;
        var filas = $('#grvAsistencia tr .o-switch').length;

        if (seleccionados < filas) {
            mostrarModalConfirmar(0, 'Ha marcado ' + seleccionados + ' asistencias de ' + filas + ', ¿Desea continuar?');
            callbackConfirmar = function () {
                $('#btnGuardar').trigger('click');
            }
        } else {
            $('#btnGuardar').trigger('click');
        }
    });

    $('body').on('click', '.o-switch label.btn.disabled', function (e) {
        return false;
    });

    $('body').on('click', '#chkActivarCierre', function (e) {
        if ($(this).prop('checked')) {
            $('#grvList input[id*="chkSeleccionarGrupo"]').parent().removeClass('d-none');
            $('#btnCerrarAsistencia').removeClass('invisible');
            $('#grvList #divChkHeader').removeClass('d-none');
        } else {
            $('#grvList input[id*="chkSeleccionarGrupo"]').prop('checked', false);
            $('#grvList input[id*="chkHeader"]').prop('checked', false);

            $('#grvList input[id*="chkSeleccionarGrupo"]').parent().addClass('d-none');
            $('#btnCerrarAsistencia').addClass('invisible');
            $('#grvList #divChkHeader').addClass('d-none');
        }
    });

    $('body').on('click', '#grvList th input[id*="chkHeader"]', function (e) {
        $('#grvList input[id*="chkSeleccionarGrupo"]').prop('checked', $(this).prop('checked'));
    });

    $('body').on('change', 'input[id*="chkActivarEdicion"]', function (e) {
        if ($(this).prop('checked')) {
            $(this).closest('td').find('.btn.disabled').removeClass('disabled');
        } else {
            $(this).closest('td').find('input[type="radio"]').prop('checked', false);
            $(this).closest('td').find('.btn').removeClass('active');
            $(this).closest('td').find('.btn').addClass('disabled');
        }
    });

    configPlugins();
    initFilterPlugins();
    initCerrarPlugins();
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

    $.extend(true, $.fn.DataTable.defaults, {
        dom:
            "<'row'<'col-sm-48 col-md-24'l><'col-sm-48 col-md-24'f>>" +
            "<'row'<'col-sm-48'tr>>" +
            "<'row'<'col-sm-48 col-md-20'i><'col-sm-48 col-md-28'p>>",
        renderer: 'bootstrap'
    });
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

function initCerrarPlugins() {
    initDatepicker('txtFechaCierreAsistencias', {
        language: 'es',
        format: 'dd/mm/yyyy'
    })
}

function initFormPlugins() {
    if (tblAsistencia != undefined) {
        tblAsistencia.destroy();
    }
    tblAsistencia = $('#grvAsistencia').DataTable({
        'paging': false,
        'info': false,
        'autoWidth': false,
        'language': {
            'sSearch': 'Buscar',
            'zeroRecords': 'No se ha encontrado ninguna coincidencia',
        }
    });

    $('.custom-checkbox').each(function (i, e) {
        $(this).find('.custom-control-label').attr('for', $(this).find('[id*="chkActivarEdicion"]').attr('id'));
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
            case 'ACC_ADIC':
                var $modal;
                var tipoModal = $('#hddTipoModalAccion').val();
                switch (tipoModal) {
                    case 'CA':
                        $modal = $mdlCerrarAsistencias;
                        break;
                    case 'RI':
                        $modal = $mdlRegistrarIncidencia;
                        break;
                    case 'VI':
                        $modal = $mdlVisualizarIncidencias;
                        break;
                }

                var mostrar = $('#hddMostrarModalAccion').val();
                if (mostrar === 'true') {
                    $modal.modal('show');
                }

                var ocultar = $('#hddOcultarModalAccion').val();
                if (ocultar === 'true') {
                    $modal.modal('hide');
                }
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
    if ($boton.is('button')) {
        $boton.prop('disabled', !retorno);
    }

    if ($boton.is('a')) {
        if (retorno) {
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
            mostrarMensajeModal(rpta, msg);
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
        case '2':
            toastr.info(mensaje);
            break;
    }
}