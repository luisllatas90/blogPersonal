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
        if ($('#cmbCicloAcademico').val() == '-1') {
            $('#cmbCicloAcademico').focus();
            toastr.warning('Debe seleccionar un ciclo académico');
            return false;
        }

        if ($('#dtpFechaDesde').val() == '') {
            $('#dtpFechaDesde').focus();
            toastr.warning('Debe indicar la fecha de "desde"');
            return false;
        }

        if ($('#dtpFechaHasta').val() == '') {
            $('#dtpFechaHasta').focus();
            toastr.warning('Debe indicar la fecha de "hasta"');
            return false;
        }

        if (!$('#cmbDepartamentoAcademico').val()) {
            $('#cmbDepartamentoAcademico').focus();
            toastr.warning('Debe seleccionar un departamento académico');
            return false;
        }

        __doPostBack('btnListar', '');
    });

    $('body').on('click', '#btnExportar', function (e) {
        if ($('#grvAsistencia tr').length > 0) {
            atenuarBoton('btnExportar', false);
            alternarLoadingGif('global', false);

            setTimeout(function () {
                exportar('grvAsistencia', 'xlsx');
                atenuarBoton('btnExportar', true);
                alternarLoadingGif('global', true);
            }, 500);

        } else {
            toastr.warning('No hay datos para exportar');
        }
    });

    initFiltros();
    verificarMensajeServer();
});


function exportar(id, type, fn, dl) {
    var elt = document.getElementById(id);
    var wb = XLSX.utils.table_to_book(elt, { sheet: "Sheet JS" });
    return dl ?
        XLSX.write(wb, { bookType: type, bookSST: true, type: 'base64' }) :
        XLSX.writeFile(wb, fn || ('ASISTENCIA.' + (type || 'xlsx')));
}

function initFiltros() {
    initSelectPicker('cmbCicloAcademico', {
        liveSearch: true,
        size: 6
    });

    initSelectPicker('cmbCarreraProfesional', {
        liveSearch: true,
        size: 6
    });

    initSelectPicker('cmbDepartamentoAcademico', {
        liveSearch: true,
        size: 6,
        dropdownAlignRight: true
    });

    initDatepicker('dtpFechaDesde', {
        language: 'es'
    });

    initDatepicker('dtpFechaHasta', {
        language: 'es'
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

function initDatepicker(controlId, opciones) {
    $('#' + controlId).datepicker(opciones);
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