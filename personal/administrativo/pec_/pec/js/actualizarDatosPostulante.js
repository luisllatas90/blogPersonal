var flagDocumentReady = false;
var flagEditForm = false;

$(document).ready(function () {
    flagDocumentReady = true;
    var codigoPaiPeru = $('#codigo-pai-peru').data('value')
    InicializarControles()

    var $smartWizard = $('#smartwizard')
    var cantPasos = $smartWizard.find("> ul > li").length
    $smartWizard.on('showStep', function (e, step, index) {
        console.log(flagEditForm);
        if (!flagEditForm && ((cantPasos - 1) != index)) {
            $('body').find("#btnFakeSubmit").attr("disabled", true);
        } else {
            $('body').find("#btnFakeSubmit").attr("disabled", false);
        }
    });

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

    $('body').on('click', '#btnFakeSubmit', function (e) {
        parent.$('body').trigger('formSubmiting');
        $('#btnSubmit').trigger('click');
    });

    $('#centroCosto').on('change', function (e) {
        var codCentroCosto = this.value;
        var $subgrupoCentroCosto = $('#subgrupo-centro-costo')
        if (codCentroCosto != '') {
            $subgrupoCentroCosto.slideDown();
        } else {
            $subgrupoCentroCosto.slideUp();
        }
    })

    $('form').on('change', 'input[name="chkInfApoderado"]', function () {
        $('input[name="chkInfApoderado"]').attr('enabled', !this.checked)
        var $divApoderado = $("#datos-apoderado");
        if ($divApoderado.hasClass('d-none')) {
            $divApoderado.hide();
            $divApoderado.removeClass('d-none')
            $divApoderado.slideDown();
        } else {
            if (this.checked) {
                $divApoderado.slideDown();
            } else {
                $divApoderado.slideUp();
            }
        }
    })
});

function InicializarControles() {
    if (flagDocumentReady) {
        var $smartWizard = $('#smartwizard');
        var $btnSubmit = $('#btnSubmit');
        flagEditForm = ($('#frmDatosPostulante').data('edit') === "True");

        $smartWizard.smartWizard({
            autoAdjustHeight: false,
            anchorSettings: {
                enableAllAnchors: flagEditForm
            },
            lang: {
                next: 'Siguiente',
                previous: 'Anterior'
            },
            toolbarSettings: {
                showPreviousButton: !flagEditForm,
                showNextButton: !flagEditForm,
                toolbarExtraButtons: [
                    $('<button id="btnFakeSubmit" type="button" ' + (flagEditForm ? '' : 'disabled') + '></button>').text('Guardar')
                        .addClass('btn btn-primary')
                ]
            },
        });

        $('#dtpFecNacimiento').datepicker({
            language: 'es'
        });

        $('#cmbPaisNacimiento').selectpicker({
            size: 6,
        });
        $('#cmbCentroCosto').selectpicker({
            size: 10,
        });
        $('#cmbCategoriaPostulacion').selectpicker({
            size: 5,
        });
        $('#cmbBenificioPostulacion').selectpicker({
            size: 5,
        });

        $('#dtpFecNacPadre').datepicker({
            language: 'es'
        });
        $('#dtpFecNacMadre').datepicker({
            language: 'es'
        });
        $('#dtpFecNacApoderado').datepicker({
            language: 'es'
        });

        if ($smartWizard.find('.nav-item.active').is(':last-child')) {
            $smartWizard.find("#btnFakeSubmit").attr("disabled", false);
        }
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
            callbackHideMdlMensajes = function () { $('#' + control).focus() }
            $mensajePostBack.addClass('alert-warning');
            $mensajePostBack.html(msg);
            $mdlMensajes.modal('show');
            break;

    }
    parent.$('body').trigger('formSubmited');
}