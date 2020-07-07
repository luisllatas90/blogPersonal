var flagDocumentReady = false;
var $smartWizard = undefined;
var $mdlMensajesCliente = undefined;
var tempStep = {
    stepNumber: 0,
};
var validator = undefined;

// CÁLCULOS
var total = 0;

$(document).ready(function () {
    flagDocumentReady = true;
    InicializarControles();

    $mdlMensajesCliente = $('#mdlMensajesCliente');
    $mdlMensajesCliente.modal({
        backdrop: 'static',
        show: false,
    });

    /* -----------------------------------WIZARD----------------------------------- */
    $smartWizard = $('#smartwizard');
    var cantPasos = $smartWizard.find("> ul > li").length
    $smartWizard.on('leaveStep', function (e, anchorObject, stepNumber, stepDirection) {
        var codigoSco = $('#cmbServicio').val();
        if (codigoSco == '-1') {
            $('#cmbServicio').valid();
        }
        return codigoSco != '-1';
    });
    $smartWizard.on('showStep', function (e, anchorObject, stepNumber, stepDirection) {
        if ((cantPasos - 1) != stepNumber) {
            $('body').find("#btnFakeSubmit").attr("disabled", true);
        } else {
            $('body').find("#btnFakeSubmit").attr("disabled", false);
        }
    });
    $smartWizard.on('click', 'a.nav-link', function (e) {
        tempStep.stepNumber = $(this).attr('href').replace('#step-', '') - 1;
    });
    $smartWizard.on('click', '.sw-btn-prev:not(.disabled)', function () {
        tempStep.stepNumber -= 1;
    });
    $smartWizard.on('click', '.sw-btn-next:not(.disabled)', function () {
        tempStep.stepNumber += 1;
    });
    /* -----------------------------------FIN WIZARD----------------------------------- */

    /* -----------------------------------VALIDATE----------------------------------- */
    validator = $('#frmFinanciamiento').validate({
        ignore: ".ignore",
        onchange: false,
        onfocusout: false,
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");
            switch (element.prop("type")) {
                case 'checkbox':
                    error.insertAfter(element.next("label"));
                    break;
                case 'select-one':
                    if (element.parent().hasClass('dropdown')) {
                        error.insertAfter(element.parent().find('.dropdown-toggle'));
                    } else {
                        error.insertAfter(element);
                    }
                    break;
                default:
                    error.insertAfter(element);
                    break;
            }
        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass("is-invalid").removeClass("is-valid");
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).addClass("is-valid").removeClass("is-invalid");
        },
        rules: {
            cmbServicio: 'cmbRequired',
        }
    });
    jQuery.validator.addMethod('cmbRequired', function (value, element) {
        return this.optional(element) || (value != '-1');
    }, 'Este campo es obligatorio.');
    /* -----------------------------------FIN VALIDATE----------------------------------- */

    $('#mdlMensajeServidor').modal({
        backdrop: 'static',
        show: false,
    });

    $('#mdlMensajeServidor').on('hidden.bs.modal', function () {
        if (callbackMdlMensajeServidor != undefined) {
            callbackMdlMensajeServidor();
        }
        callbackMdlMensajeServidor = undefined;

        var nombreControl = $('#respuestaPostback').data('control');
        if (nombreControl != undefined && nombreControl != '') {
            $('#' + nombreControl).focus();
        }
    });

    /* ------------------------------CÁLCULOS------------------------------ */
    $('body').on('click', '#chkUnaCuota', function (e) {
        CambioCuotas();
    });

    $('body').on('keyup', '#txtRecargo', function (e) {
        CalcularTotal();
    });

    $('body').on('keyup', '#txtDescuento', function (e) {
        CalcularTotal();
    });

    $('body').on('keyup', '#txtCuotaInicial', function (e) {
        CalcularSaldo();
    });
    /* -------------------------------------------------------------------- */

    $('body').on('keypress', '.decimal', function (e) {
        var digitos = $(this).data('decimales');
        digitos = (digitos == undefined ? 0 : digitos);
        var result = floatNumber(e, this, digitos);
        if (!result) {
            e.preventDefault();
        }
    });

    $('body').on('click', '#btnFakeSubmit', function (e) {
        e.preventDefault();

        $(this).prop('disabled', true);
        var validator = $("#frmFinanciamiento").validate();
        if (validator.form()) {
            parent.$('body').trigger('formSubmiting');
            $('#btnRegistrar').trigger('click');
        } else {
            $(this).prop('disabled', false);
            MostrarMensajeCliente(0, 'Se han encontrado errores de validación, verifique los datos ingresados.');
        }
    });

    CambioCuotas();
});

function InicializarControles() {
    if (flagDocumentReady) {
        var $smartWizard = $('#smartwizard');

        $smartWizard.smartWizard({
            showStepURLhash: false,
            autoAdjustHeight: false,
            lang: {
                next: 'Siguiente',
                previous: 'Anterior'
            },
            keyNavigation: false,
            toolbarSettings: {
                showPreviousButton: true,
                showNextButton: true,
                toolbarExtraButtons: [
                    $('<button id="btnFakeSubmit" type="button" disabled="disabled"></button>')
                        .text('Guardar')
                        .addClass('btn btn-primary')
                ]
            },
        });

        $('#dtpFecVenc').datepicker({
            language: 'es',
            format: 'dd/mm/yyyy'
        });

        $('#dtpFecVencInicial').datepicker({
            language: 'es',
            format: 'dd/mm/yyyy'
        });

        if (!($('#rowCostos').data('oculto'))) {
            $('#rowCostos').fadeIn(0);
        } else {
            $('#rowCostos').fadeOut(0);
        }
    }
}

function CambioCuotas() {
    if ($('#chkUnaCuota').prop("checked")) {
        $('#divMasCuotas').hide();
        $('#divUnaCuota').show();
    } else {
        $('#divMasCuotas').show();
        $('#divUnaCuota').hide();
    }
}

function ValidarNumeros(event) {
    if (event.shiftKey) {
        event.preventDefault();
    }

    if (event.keyCode == 46 || event.keyCode == 8) {
    }
    else {
        if (event.keyCode < 95) {
            if (event.keyCode < 48 || event.keyCode > 57) {
                event.preventDefault();
            }
        }
        else {
            if (event.keyCode < 96 || event.keyCode > 105) {
                event.preventDefault();
            }
        }
    }
}

function CalcularTotal() {
    var descuento = 0.0;
    var recargo = 0.0;

    var precio = parseFloat(($('#txtPrecio').val()) == '' ? 0 : $('#txtPrecio').val());
    var descuento = parseFloat(($('#txtDescuento').val()) == '' ? 0 : $('#txtDescuento').val());
    var recargo = parseFloat(($('#txtRecargo').val()) == '' ? 0 : $('#txtRecargo').val());

    total = (precio + recargo - descuento).toFixed(2);
    $('#txtTotal').val(total);
    $('#txtCuota').val(total);

    CalcularSaldo();
}

function CalcularSaldo() {
    var cuotaInicial = parseFloat(($('#txtCuotaInicial').val()) == '' ? 0 : $('#txtCuotaInicial').val());
    var saldo = (total - cuotaInicial).toFixed(2);
    $('#txtSaldo').val(saldo)
}


var callbackMdlMensajeServidor = undefined;

function SubmitPostBack() {
    var $mdlMensajeServidor = $('#mdlMensajeServidor');
    var $respuestaPostback = $mdlMensajeServidor.find('#respuestaPostback')

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');
    var control = $respuestaPostback.data('control');

    $respuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $respuestaPostback.addClass('alert-danger');
            $respuestaPostback.html(msg);
            $mdlMensajeServidor.modal('show');
            break;
        case 0:
            callbackMdlMensajeServidor = function () { $('#' + control).focus() }
            $respuestaPostback.addClass('alert-warning');
            $respuestaPostback.html(msg);
            $mdlMensajeServidor.modal('show');
            break;
        case 1:
            break;
    }
    parent.$('body').trigger('formSubmited');
}

function MostrarMensajeCliente(rpta, msg) {
    var $mensaje = $mdlMensajesCliente.find('#mensaje');
    $mensaje.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    var $modalTitle = $mdlMensajesCliente.find('.modal-header .modal-title');
    switch (rpta) {
        case -1:
            $modalTitle.html('Error');
            $mensaje.addClass('alert-danger');
            break;
        case 0:
            $modalTitle.html('Alerta');
            $mensaje.addClass('alert-warning');
            break;
        case 1:
            $modalTitle.html('Respuesta');
            $mensaje.addClass('alert-success');
            break;
    }
    $mensaje.html(msg);
    $mdlMensajesCliente.modal('show');
}