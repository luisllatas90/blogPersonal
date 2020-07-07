var flagDocumentReady = false;

var $mdlMensajeServidor = undefined;
var $tabLista = undefined;
var $tabMantenimiento = undefined;
var $mdlAlumnos = undefined;
var $mdlMensajeClienteConfirmar = undefined;
var $divConfigManual = undefined;
var $ifrmFiltrosAlumno = undefined;
var $loadingGif = undefined;
var accionFiltros = undefined;
var callbackConfirmar = undefined;
var keyCSV = 'CODIGO_UNIV';

$(document).ready(function () {
    flagDocumentReady = true;

    configPlugins();

    $loadingGif = $('#loadingGif');
    $tabLista = $('#nav-lista-tab');
    $tabMantenimiento = $('#nav-mantenimiento-tab');

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlAlumnos = $('#mdlAlumnos');
    $mdlAlumnos.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajeClienteConfirmar = $('#mdlMensajeClienteConfirmar');
    $mdlMensajeClienteConfirmar.modal({
        backdrop: 'static',
        show: false,
    });

    $('body').on('click', 'button[id*="btnFakeEjecutar"]', function (e) {
        atenuarBoton($(this).attr('id'));
        var $realButton = $(this).closest('td').find('button[id*="btnEjecutar"]');
        callbackConfirmar = function () {
            $realButton.trigger('click');
        }
        mostrarModalConfirmar(0, '¿Realmente desea realizar esta operación?');
    });

    $mdlMensajeClienteConfirmar.find('#btnConfirmarContinuar').on('click', function (e) {
        atenuarBoton($(this).attr('id'));
        callbackConfirmar();
    });

    $mdlMensajeClienteConfirmar.on('hide.bs.modal', function (e) {
        callbackConfirmar = undefined;
        $('button[id*="btnFakeEjecutar"]').prop('disabled', false);
    });

    $divConfigManual = $('#divConfigManual');

    $('body').on('change', '#rbtConfiguracionPredefinida', function (e) {
        __doPostBack('rbtConfiguracionPredefinida', '');
    });

    $('body').on('change', '#rbtConfiguracionManual', function (e) {
        __doPostBack('rbtConfiguracionManual', '');
    });

    $('body').on('change', '#fluArchivoAlumnos', function (e) {
        var file = e.target.files[0];

        var extension = file.name.split('.').pop();
        if (extension != 'csv') {
            mostrarToastr('0', 'Debe subir un archivo con extensión CSV', 'fluArchivoAlumnos');
            return false;
        }

        if (!file) {
            $('#hddCodUnivAlumnos').val('');
            __doPostBack('hddCodUnivAlumnos', '');
            return;
        }

        var r = new FileReader();
        r.onload = function (e) {
            var results = processCSV(e.target.result, true, true);
            console.log(results);
            var codigos = '';
            for (var i = 0; i < results.length; i++) {
                var codigo = results[i][keyCSV];
                if (codigo != null) {
                    if (codigos != '') {
                        codigos += ','
                    }
                    codigos += codigo;
                }
            }
            $('#hddCodUnivAlumnos').val(codigos);
            __doPostBack('hddCodUnivAlumnos', '');
        }
        r.readAsBinaryString(file);

        // Papa.parse(file, {
        //     header: false,
        //     dynamicTyping: true,
        //     complete: function (results) {
        //         var codigos = '';

        //         for (var i = 0; i < results.data.length; i++) {
        //             var codigo = results.data[i][0];
        //             if (codigo != null) {
        //                 if (codigos != '') {
        //                     codigos += ','
        //                 }
        //                 codigos += codigo;
        //             }
        //         }
        //         $('#hddCodUnivAlumnos').val(codigos);
        //         __doPostBack('hddCodUnivAlumnos', '');
        //     }
        // });
    });

    $('body').on('click', '#lnkDescargarPlantilla', function (e) {
        e.preventDefault();
        descargarPlantilla();
    })

    $('body').on('click', '#nav-manual-tab', function (e) {
        $('#hddTipoSeleccionAlumnos').val('M');
        __doPostBack('hddTipoSeleccionAlumnos', '');
    });

    $('body').on('click', '#nav-importar-tab', function (e) {
        $('#hddTipoSeleccionAlumnos').val('I');
        __doPostBack('hddTipoSeleccionAlumnos', '');
    });

    $('body').on('click', '#btnExportar', function (e) {
        if ($('#grvAlumnos tr').length > 0) {
            atenuarBoton('btnExportar', false);
            alternarLoadingGif('global', false);

            setTimeout(function () {
                exportar('grvAlumnos', 'xlsx');
                atenuarBoton('btnExportar', true);
                alternarLoadingGif('global', true);
            }, 500);

        } else {
            toastr.warning('No hay datos para exportar');
        }
    });

    $('body').on('click', '#btnFakeMostrarAlumnos', function (e) {
        var configuracionManual = ($('#rbtConfiguracionManual').is(':checked'));
        var porFiltros = ($('#hddTipoSeleccionAlumnos').val() == 'M');

        if (configuracionManual && porFiltros) {
            $ifrmFiltrosAlumno[0].contentWindow.$('body').trigger('generarCadenaFiltros', function (data) {
                $('#hddFiltrosAlumno').val(data.cadenaFiltros);
                llamadaServerMostrarAlumnos();
            });
        } else {
            llamadaServerMostrarAlumnos();
        }
    });

    $('body').on('click', '#btnFakeGuardar', function (e) {
        var configuracionManual = ($('#rbtConfiguracionManual').is(':checked'));
        var porFiltros = ($('#hddTipoSeleccionAlumnos').val() == 'M');

        if (configuracionManual && porFiltros) {
            $ifrmFiltrosAlumno[0].contentWindow.$('body').trigger('generarCadenaFiltros', function (data) {
                $('#hddFiltrosAlumno').val(data.cadenaFiltros);
                __doPostBack('btnGuardar', '');
            });
        } else {
            __doPostBack('btnGuardar', '');
        }
    });

    $('body').on('errorFiltrosEnviado', function (e, data) {
        mostrarToastr(data.rpta, data.msg, data.control, true);
    });

    $('body').on('change', '#rbtEjecManual', function (e) {
        __doPostBack('rbtEjecManual', '');
    });

    $('body').on('change', '#rbtProgUnaVez', function (e) {
        __doPostBack('rbtProgUnaVez', '');
    });

    $('body').on('change', '#rbtProgPeriodico', function (e) {
        __doPostBack('rbtProgPeriodico', '');
    });

    $('body').on('change', '#rbtTipoConfigurado', function (e) {
        __doPostBack('rbtTipoConfigurado', '');
    });

    $('body').on('change', '#rbtTipoManual', function (e) {
        __doPostBack('rbtTipoManual', '');
    });

    verificarMensajeServer();
});

function configPlugins() {
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

function verificarCambiosAjax() {
    verificarParametros('TAB');
    verificarToastrServer();
    verificarMensajeServer();
}

function descargarPlantilla() {
    var json = {};
    json[keyCSV] = '';
    var csv = generateCSV([json]);
    saveAs(new Blob([s2ab(csv)], { type: "application/octet-stream" }), "IMPORTAR ALUMNOS.csv");
}

function confirmarEjecutado() {
    atenuarBoton('btnConfirmarContinuar', true);
    $mdlMensajeClienteConfirmar.modal('hide');
}

function initIframe() {
    $ifrmFiltrosAlumno = $('#ifrmFiltrosAlumno');
    $ifrmFiltrosAlumno.iFrameResize({
        // autoResize: false
        // minHeight: 200
    });
}

function initFormMantenimiento() {
    initIframe();
    initFechaHoraInicioProg();
    initFechaHoraFinProg();
    initFechaVencimiento();
    initSelectPicker('cmbTipoConfiguracion');
    initSelectPicker('cmbCicloAcademico', {
        'size': 6
    });
    initSelectPicker('cmbEjecutarCada');
    initSelectPicker('cmbTipoDeuda');
    initSelectPicker('cmbServicioConcepto');
    initSelectPicker('cmbCentroCosto');
}

function verificarParametros(tipoConsulta) {
    switch (tipoConsulta) {
        case 'TAB':
            var tipoVista = $('#hddTipoVista').val();
            seleccionarTab('GENERAL', tipoVista);

            var tipoSeleccionAlumnos = $('#hddTipoSeleccionAlumnos').val();
            seleccionarTab('ALUMNOS', tipoSeleccionAlumnos);
            break;
    }
}

function seleccionarTab(tipo, valor) {
    switch (tipo) {
        case 'GENERAL':
            switch (valor) {
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
            break;
        case 'ALUMNOS':
            switch (valor) {
                case 'M':
                    $('#nav-manual-tab').tab('show');
                    break;
                case 'I':
                    $('#nav-importar-tab').tab('show');
                    break;
            }
            break;
    }
}

function initFechaHoraInicioProg() {
    initDatetimePicker('mrkFechaHoraInicioProg', {
        format: 'DD/MM/YYYY HH:mm',
    });
}

function initFechaHoraFinProg() {
    initDatetimePicker('mrkFechaHoraFinProg', {
        format: 'DD/MM/YYYY HH:mm',
    });
}

function initFechaVencimiento() {
    initDatetimePicker('mrkFechaVencimiento', {
        format: 'DD/MM/YYYY',
    });
}

function initDatetimePicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.datetimepicker(opciones);
}

function llamadaServerMostrarAlumnos() {
    __doPostBack('btnMostrarAlumnos', '');
}

function mostrarToastr(tipo, mensaje, control, enIframe) {
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
        if (enIframe) {
            $control = $ifrmFiltrosAlumno[0].contentWindow.$(selector);
        }
        $control.focus();
    }
}

function exportar(id, type, fn, dl) {
    var elt = document.getElementById(id);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "Sheet JS" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || ('ASISTENCIA.' + (type || 'xlsx')));
}

// function verificarTipoConfiguracion() {
//     var valor = $('input[name="rbtTipoConfiguracion"]:checked').val();
//     if (valor == 'M') {
//         $divConfigManual.collapse('show');
//         $('#cmbTipoConfiguracion').val(-1);
//         $('#cmbTipoConfiguracion').prop('disabled', true);
//     } else {
//         $divConfigManual.collapse('hide');
//         $('#cmbTipoConfiguracion').prop('disabled', false);
//     }
//     initSelectPicker('cmbTipoConfiguracion');
// }

function initSelectPicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.selectpicker(opciones);
    $control.selectpicker('refresh');
}

function atenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
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

function verificarMostrarAlumnos() {
    var $divAlumnoParametros = $mdlAlumnos.find('#divAlumnoParametros');
    var mostrar = $divAlumnoParametros.data('mostrar');

    if (mostrar === true) {
        $mdlAlumnos.modal('show');
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