var flagDocumentReady = false;
$(document).ready(function () {
    flagDocumentReady = true;
    var codigoPaiPeru = $('#codigo-pai-peru').data('value')
    InicializarControles()

    var $smartWizard = $('#smartwizard')
    var cantPasos = $smartWizard.find("> ul > li").length
    $smartWizard.on('showStep', function (e, step, index) {
        if ((cantPasos - 1) == index) {
            $('body').find("#btnFakeSubmit").attr("disabled", false);
        } else {
            $('body').find("#btnFakeSubmit").attr("disabled", true);
        }
    });

    $('body').on('click', '#btnFakeSubmit', function (e) {
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
        console.log("INICIALIZANDO!!")
        var $smartWizard = $('#smartwizard')
        var $btnSubmit = $('#btnSubmit')
        $smartWizard.smartWizard({
            lang: {
                next: 'Siguiente',
                previous: 'Anterior'
            },
            autoAdjustHeight: false,
            toolbarSettings: {
                toolbarExtraButtons: [
                    $('<button id="btnFakeSubmit" type="button" disabled></button>').text('Guardar')
                        .addClass('btn btn-success')
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
