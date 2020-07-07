var flagDocumentReady = false;
var flagEditForm = false;
var $smartWizard = undefined;
var $mdlMensajesCliente = undefined;
var $mdlMensajesServidor = undefined;
var $mdlCoincidencias = undefined;
var procesandoStep = false;
var tempStep = {
    stepNumber: 0,
    stepDirection: '',
    stepMode: 'B' // Botones, T: Tabs
};

window.onload = function () {
    $('#loading-gif').fadeOut(150);
    $('#frmDatosPostulante').css('display', 'block');
    $('#frmDatosPostulante').animate({
        opacity: 1
    }, 50)
}

$(document).ready(function () {
    flagDocumentReady = true;

    // Agrego el metodo "subset" al plugin para poder validar por regiones
    jQuery.validator.prototype.subset = function (container) {
        var ok = true;
        var self = this;
        $(container).find(':input').each(function () {
            if (!self.element($(this))) ok = false;
        });
        return ok;
    }

    jQuery.validator.addMethod('cmbRequired', function (value, element) {
        return this.optional(element) || (value != '-1' && value != '');
    }, 'Este campo es obligatorio.');

    $('#frmDatosPostulante').validate({
        ignore: ".ignore",
        onchange: function (element) {
            $(element).valid();
        },
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
            cmbCicloIngreso: { cmbRequired: true },
            cmbCarreraProfesional: { cmbRequired: true },
            cmbModalidadIngreso: { cmbRequired: true },
            txtNroDocIdentidad: {
                required: true,
                digits: {
                    depends: function () {
                        return $('#cmbTipoDocIdentidad').val() == 'DNI';
                    },
                },
                minlength: function () {
                    if ($('#cmbTipoDocIdentidad').val() == 'DNI') {
                        return 8;
                    } else {
                        return 0;
                    }
                },
                maxlength: function () {
                    if ($('#cmbTipoDocIdentidad').val() == 'DNI') {
                        return 8;
                    } else {
                        return 100;
                    }
                }
            },
            txtApellidoPaterno: {
                required: true,
            },
            txtApellidoMaterno: {
                required: true,
            },
            txtNombres: {
                required: true,
            },
            dtpFecNacimiento: {
                required: true,
            },
            cmbGenero: {
                cmbRequired: true,
            },
            txtEmail: {
                email: true,
            },
            txtEmailAlternativo: {
                email: true,
            },
            txtAnioPromocion: {
                digits: true,
                minlength: 4,
                maxlength: 4,
            },
            cmbDptoNacimiento: { cmbRequired: true },
            cmbPrvNacimiento: { cmbRequired: true },
            cmbDstNacimiento: { cmbRequired: true },
            cmbDptoActual: { cmbRequired: true },
            cmbPrvActual: { cmbRequired: true },
            cmbDstActual: { cmbRequired: true },
            cmbPaisInstitucion: { cmbRequired: true },
            // cmbDptoInstitucion: { cmbRequired: true },
            // cmbPrvInstitucion: { cmbRequired: true },
            // cmbDstInstitucion: { cmbRequired: true },
            cmbInstitucionEducativa: { cmbRequired: true },
            // Datos del padre
            txtDocIdentPadre: {
                digits: true,
                minlength: 8,
                maxlength: 8,
            },
            txtEmailPadre: {
                email: true,
            },
            txtDocIdentMadre: {
                digits: true,
                minlength: 8,
                maxlength: 8,
            },
            txtEmailMadre: {
                email: true,
            },
            txtDocIdentApoderado: {
                digits: true,
                minlength: 8,
                maxlength: 8,
            },
            txtEmailApoderado: {
                email: true,
            },
        },
    });

    $('body').on('change', '#cmbCarreraProfesional', function (e) {
        $(this).valid();
    });

    $('body').on('change', '#cmbPaisInstitucion', function (e) {
        $(this).valid();
    });

    $('body').on('change', '#cmbInstitucionEducativa', function (e) {
        $(this).valid();
    });

    $('body').on('keyup', '#txtNroDocIdentidad', function (e) {
        if (!$(this).valid()) {
            if (!$('#lnkObtenerDatos').hasClass('disabled')) {
                $('#lnkObtenerDatos').addClass('disabled');
            }
        } else {
            $('#lnkObtenerDatos').removeClass('disabled');
        }
    });

    $('body').on('keypress', '#txtApellidoPaterno', function (e) {
        if (e.keyCode == 13) {
            // __doPostBack('lnkObtenerDatosPorApellidos', '');
        }
    });

    $('body').on('keypress', '#txtApellidoMaterno', function (e) {
        if (e.keyCode == 13) {
            // __doPostBack('lnkObtenerDatosPorApellidos', '');
        }
    });

    // $('body').on('keyup', '#txtApellidoPaterno, #txtApellidoMaterno', function (e) {
    //     if (!$(this).valid()) {
    //         if (!$('#lnkObtenerDatosPorApellidos').hasClass('disabled')) {
    //             $('#lnkObtenerDatosPorApellidos').addClass('disabled');
    //         }
    //     } else {
    //         $('#lnkObtenerDatosPorApellidos').removeClass('disabled');
    //     }
    // });

    $('body').on('keypress', '#txtNroDocIdentidad', function (e) {
        if (e.keyCode == 13 && $(this).valid()) {
            __doPostBack('lnkObtenerDatos', '');
        }
    });

    $smartWizard = $('#smartwizard');
    flagEditForm = ($('#frmDatosPostulante').data('edit') === "True");

    $smartWizard.smartWizard({
        showStepURLhash: false,
        autoAdjustHeight: false,
        anchorSettings: {
            enableAllAnchors: flagEditForm
        },
        lang: {
            next: 'Siguiente',
            previous: 'Anterior'
        },
        keyNavigation: false,
        toolbarSettings: {
            showPreviousButton: true,
            showNextButton: true,
            toolbarExtraButtons: [
                $('<button id="btnFakeSubmit" type="button" ' + (flagEditForm ? '' : 'disabled') + '></button>').text('Guardar')
                    .addClass('btn btn-primary')
            ]
        },
    });

    var cantPasos = $smartWizard.find("> ul > li").length
    $smartWizard.on('showStep', function (e, anchorObject, stepNumber, stepDirection) {
        tempStep.stepNumber = stepNumber;
        tempStep.stepDirection = stepDirection;
        if (!flagEditForm && ((cantPasos - 1) != stepNumber)) {
            $('body').find("#btnFakeSubmit").attr("disabled", true);
        } else {
            $('body').find("#btnFakeSubmit").attr("disabled", false);
        }
    });

    $smartWizard.on('click', '.nav-link', function (e) {
        tempStep.stepNumber = parseInt($(this).data('step')) - 1;
        tempStep.stepMode = 'T';
    });

    $smartWizard.on('click', '.sw-btn-group button', function (e) {
        tempStep.stepMode = 'B';
    });


    $smartWizard.on('leaveStep', function (e, anchorObject, stepNumber, stepDirection) {
        tempStep.anchorObject = anchorObject;
        tempStep.stepDirection = stepDirection;

        if (!procesandoStep && stepDirection == 'forward') {
            var stepId = anchorObject.get(0).hash;
            var validator = $("#frmDatosPostulante").validate();

            if (validator.subset(stepId)) {
                var codigoAlu = $('#frmDatosPostulante').data('alu');
                if (codigoAlu == '0') {
                    if (stepNumber > 1 && stepDirection == 'forward') {
                        procesandoStep = true;
                        var $stepContent = $(anchorObject.attr('href'));
                        $stepContent.css({ opacity: 0.5 });
                        __doPostBack('btnSubmitAndContinue', '');
                    } else {
                        return true;
                    }
                } else {
                    return true;
                }
            }

            return false;
        }
        return true;
    });

    if ($smartWizard.find('.nav-item.active').is(':last-child')) {
        $smartWizard.find("#btnFakeSubmit").attr("disabled", false);
    }

    $mdlMensajesCliente = $('#mdlMensajesCliente');
    $mdlMensajesCliente.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajesServidor = $('#mdlMensajesServidor');
    $mdlMensajesServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajesServidor.on('hide.bs.modal', function () {
        if (callbackHideMdlMensajes != undefined) {
            callbackHideMdlMensajes();
        }
        callbackHideMdlMensajes = undefined;
    });

    $mdlCoincidencias = $('#mdlCoincidencias');
    $mdlCoincidencias.modal({
        backdrop: 'static',
        show: false,
    });

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
    });

    $('body').on('click', '#btnFakeSubmit', function (e) {
        e.preventDefault();

        $(this).prop('disabled', true);
        var validator = $("#frmDatosPostulante").validate();

        // Valido cada pestaña visible
        var validacion = true;
        $('#smartwizard .nav-link').each(function () {
            var stepId = $(this).attr('href');
            validacion = validator.subset(stepId);
            if (!validacion) {
                return false;
            }
        });

        if (validacion) {
            parent.$('body').trigger('formSubmiting');
            $('#btnSubmit').trigger('click');
        } else {
            $(this).prop('disabled', false);
            MostrarMensajeCliente(0, 'Se han encontrado errores de validación, verifique los datos ingresados.');
        }
    });

    $('body').on('keypress', 'input.only-digits', function (e) {
        return onlyDigits(e);
    });

    $('body').on('keypress', 'input.only-letters', function (e) {
        return onlyLetters(e);
    });

    InicializarControles();
});

function InicializarControles() {
    if (flagDocumentReady) {
        $('#dtpFecNacimiento').datepicker({
            language: 'es'
        });

        $('#cmbCarreraProfesional').selectpicker({
            // size: 6,
        });

        $('#cmbPaisInstitucion').selectpicker({
            size: 6,
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
        $('#cmbInstitucionEducativa').selectpicker({
            // size: 8,
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

        InitDtpFechaPrimeraMatricula();
    }
}

function InitDtpFechaPrimeraMatricula() {
    $('#dtpFechaPrimeraMatricula').datepicker({
        language: 'es'
    });
}

function ContinuarStep() {
    var $stepContent = $(tempStep.anchorObject.attr('href'));
    var $respuestaPostback = $('#respuestaPostback');

    $stepContent.css({ opacity: 1 });
    if (tempStep.stepMode == 'B') {
        if ($respuestaPostback.data('rpta') == '1') {
            if (tempStep.stepDirection == 'forward') {
                tempStep.stepNumber += 1;
            } else {
                tempStep.stepNumber -= 1;
            }
        }
    } else {
        tempStep.stepMode = 'B'
    }
    $smartWizard.smartWizard('showStep', tempStep.stepNumber);
    procesandoStep = false;
}

function RevisarRespuestaServidor() {
    var respuestaServidor = {
        rpta: '',
        msg: '',
    }
    var $mdlMenServparametros = $mdlMensajesServidor.find('#divMdlMenServParametros');
    if ($mdlMenServparametros.data('mostrar')) {
        var $respuestaPostback = $mdlMensajesServidor.find('#respuestaPostback');
        respuestaServidor = {
            rpta: $respuestaPostback.data('rpta'),
            msg: $respuestaPostback.data('msg'),
        }
        MostrarMensajeServidor(respuestaServidor.rpta, respuestaServidor.msg);
    }
    return respuestaServidor;
}

var callbackHideMdlMensajes = undefined;
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

function MostrarMensajeServidor(rpta, msg, control) {
    var $respuestaPostback = $mdlMensajesServidor.find('#respuestaPostback');
    $respuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $respuestaPostback.addClass('alert-danger');
            break;
        case 0:
            if (control != '') {
                callbackHideMdlMensajes = function () { $('#' + control).focus() }
            }
            $respuestaPostback.addClass('alert-warning');
            break;
        case 1:
            $respuestaPostback.addClass('alert-success');
            break;
    }
    $respuestaPostback.html(msg);
    $mdlMensajesServidor.modal('show');
}

function SubmitPostBack() {
    var $respuestaPostback = $mdlMensajesServidor.find('#respuestaPostback');
    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');
    var control = $respuestaPostback.data('control');

    MostrarMensajeServidor(rpta, msg, control);
    $('#btnFakeSubmit').prop('disabled', false);

    var $divMdlMenServParametros = $mdlMensajesServidor.find('#divMdlMenServParametros');
    var codigoAlu = $divMdlMenServParametros.data('codigoAlu');
    var codigoPso = $divMdlMenServParametros.data('codigoPso');
    parent.$('body').trigger('formSubmited', {
        codigoAlu: codigoAlu,
        codigoPso: codigoPso,
    });
}

function RevisarMensajePostback() {
    var $mdlMensajesServidor = $('#mdlMensajesServidor');
    var $divMdlMenServParametros = $mdlMensajesServidor.find('#divMdlMenServParametros');

    var mostrar = $divMdlMenServParametros.data('mostrar');
    if (mostrar) {
        var $respuestaPostback = $mdlMensajesServidor.find('#respuestaPostback');
        $respuestaPostback.removeClass(function (index, className) {
            return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
        });

        var rpta = $respuestaPostback.data('rpta');
        var msg = $respuestaPostback.data('msg');
        var control = $respuestaPostback.data('control');

        switch (rpta) {
            case -1:
                $respuestaPostback.addClass('alert-danger');
                $respuestaPostback.html(msg);
                $mdlMensajesServidor.modal('show');
                break;
            case 0:
                $respuestaPostback.addClass('alert-warning');
                $respuestaPostback.html(msg);
                $mdlMensajesServidor.modal('show');
                if (control != "") {
                    callbackMdlMensajeServidor = function () { $('#' + control).focus() }
                    // DecorarControles();
                }
                break;
            case 1:
                break;
        }
        parent.$('body').trigger('scrollTop');
    }
}

function BuscandoDatos() {
    $('#txtNroDocIdentidad').prop('disabled', true);
    $('#txtApellidoPaterno').prop('disabled', true);
    $('#txtApellidoMaterno').prop('disabled', true);
    $('#lnkObtenerDatos').addClass('disabled');
    // $('#lnkObtenerDatosPorApellidos').addClass('disabled');
}

function DatosBuscados() {
    RevisarMensajePostback();

    $('#lnkObtenerDatos').removeClass('disabled');
    // $('#lnkObtenerDatosPorApellidos').removeClass('disabled');

    if ($('#grwCoincidencias').data('mostrar')) {
        $mdlCoincidencias.modal('show');
    }
}

function SeleccionandoPersona(controlId) {
    $('#' + controlId).prop('disabled', true);
}

function PersonaSeleccionada() {
    $mdlCoincidencias.modal('hide');
    $('#lnkObtenerDatos').removeClass('disabled');
    // $('#lnkObtenerDatosPorApellidos').removeClass('disabled');
}

var deudasMostradas = false;
function VerificarDeudas() {
    // Grilla de deudas
    var $divDeudas = $('#divDeudas');
    var mostrarDeudas = $divDeudas.data('mostrar');

    if (!deudasMostradas) {
        if (mostrarDeudas) {
            $divDeudas.collapse('show');
            deudasMostradas = true;
        } else {
            $divDeudas.collapse('hide');
            deudasMostradas = false;
        }
    } else {
        if (mostrarDeudas) {
            $('#divDeudas').show();
        } else {
            $('#divDeudas').hide();
        }
    }
}

function VerificarEmail() {
    var email = $('#txtEmail').val().trim();
    var path = "https://apps.emaillistverify.com/api/verifEmail?secret=iofafn3rsgczdpvz1isr3&email=" + email;

    var yaExiste = $('#hddEmailCoincidente').val().trim();
    if (email != '' && email != 'ninguno@usat.edu.pe' && yaExiste == '0') { 
        $.ajax({
            type: "GET",
            url: path,
            cache: false,
            dataType: 'text',
            success: function (response) {
                if (response == "ok") {
                    $('#hddEmailVerificado').val("1");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
}