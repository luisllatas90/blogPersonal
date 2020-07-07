var flagDocumentReady = false;
var $mdlMensajesCliente = undefined;
var $mdlMensajeServidor = undefined;
var $mdlCoincidencias = undefined;

$(document).ready(function () {
    flagDocumentReady = true;
    InicializarControles();

    $mdlMensajesCliente = $('#mdlMensajesCliente');
    $mdlMensajesCliente.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlCoincidencias = $('#mdlCoincidencias');
    $mdlCoincidencias.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajeServidor.on('hidden.bs.modal', function () {
        if (callbackMdlMensajeServidor != undefined) {
            callbackMdlMensajeServidor();
        }
        callbackMdlMensajeServidor = undefined;

        var nombreControl = $('#respuestaPostback').data('control');
        if (nombreControl != undefined && nombreControl != '') {
            $('#' + nombreControl).focus();
        }
    });

    $('body').on('keypress', '#txtDNI', function (e) {
        if (e.keyCode == 13 && $(this).valid()) {
            __doPostBack('lnkObtenerDatos', '');
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

    $('body').on('click', '#btnMensajeCancelarNuevaPersona', function (e) {
        parent.$('body').trigger('formCanceled');
    });

    $('body').on('keypress', 'input.only-digits', function (e) {
        return onlyDigits(e);
    });

    $('body').on('keypress', 'input.only-letters', function (e) {
        return onlyLetters(e);
    });
});

function InicializarControles() {
    if (flagDocumentReady) {
        $('#cmbInstitucionEducativa').selectpicker({
            size: 6,
            liveSearch: true,
        });

        $('#cmbCarreraProfesional').selectpicker({
            liveSearch: true,
        });

        $('#dtpFecNacimiento').datepicker({
            language: 'es',
            format: 'dd/mm/yyyy'
        });

        if (!($('#rowCostos').data('oculto'))) {
            $('#rowCostos').fadeIn(0);
        } else {
            $('#rowCostos').fadeOut(0);
        }

        jQuery.validator.addMethod('cmbRequired', function (value, element) {
            return this.optional(element) || (value != '-1');
        }, 'Este campo es obligatorio.');
        jQuery.validator.addMethod("lettersonly", function (value, element) {
            return this.optional(element) || /^[a-z\s]+$/i.test(value);
        }, "Only alphabetical characters");
        $('#frmInscripcion').validate({
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
                txtDNI: {
                    required: true,
                    digits: true,
                    minlength: 8,
                    maxlength: 8,
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
                cmbSexo: {
                    cmbRequired: true,
                },
                cmbDepartamento: {
                    cmbRequired: true,
                },
                cmbProvincia: {
                    cmbRequired: true,
                },
                cmbDistrito: {
                    cmbRequired: true,
                },
                txtDireccion: {
                    required: true,
                },
                txtEmail: {
                    email: true,
                    required: true,
                },
                cmbEstadoCivil: {
                    cmbRequired: true,
                },
                cmbDepartamentoInstEduc: {
                    cmbRequired: true,
                },
                cmbInstitucionEducativa: {
                    cmbRequired: true,
                },
                cmbAnioEstudio: {
                    cmbRequired: true,
                },
                cmbCicloIngreso: {
                    cmbRequired: true,
                },
                cmbEventoCRM: {
                    cmbRequired: true,
                },
                cmbCarreraProfesional: {
                    cmbRequired: true,
                },
                cmbModalidadIngreso: {
                    cmbRequired: true,
                },
                txtCentroLabores: {
                    required: true,
                },
                txtCargoActual: {
                    required: true,
                },
                cmbTipoParticipante: {
                    cmbRequired: true,
                },
                cmbModalidadIngresoEC: {
                    cmbRequired: true,
                },
            },
            messages: {
                txtApellidoPaterno: 'Solo letras',
                txtApellidoMaterno: 'Solo letras',
                txtNombres: 'Solo se permiten letras',
            },
        });

        VerificarDeudas();
    }
}

var callbackMdlMensajeServidor = undefined;

function ValidarForm() {
    $(this).prop('disabled', true);
    var validator = $("#frmInscripcion").validate();
    if (validator.form()) {
        __doPostBack('btnRegistrar', '');
    } else {
        MostrarMensajeCliente(0, 'Se han encontrado errores de validación, verifique los datos ingresados.');
        parent.$('body').trigger('formSubmitedWithErrors');
    }
    return false;
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
    parent.$('body').trigger('scrollTop');
}

function SubmitPostBack() {
    var $divMdlMenServParametros = $('#mdlMensajeServidor #divMdlMenServParametros');
    var codigoAlu = $divMdlMenServParametros.data('codigoAlu');
    var codigoPso = $divMdlMenServParametros.data('codigoPso');
    parent.$('body').trigger('formSubmited', {
        codigoAlu: codigoAlu,
        codigoPso: codigoPso,
    });
}

function RevisarMensajePostback() {
    var $mdlMensajeServidor = $('#mdlMensajeServidor');
    var $divMdlMenServParametros = $mdlMensajeServidor.find('#divMdlMenServParametros');

    var mostrar = $divMdlMenServParametros.data('mostrar');
    if (mostrar) {
        var $respuestaPostback = $mdlMensajeServidor.find('#respuestaPostback');
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
                $mdlMensajeServidor.modal('show');
                break;
            case 0:
                $respuestaPostback.addClass('alert-warning');
                $respuestaPostback.html(msg);
                $mdlMensajeServidor.modal('show');
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

function DecorarControles() {
    $('#frmInscripcion input').each(function (index, value) {
        var dataError = $(this).data('error');
        if (dataError != undefined) {
            if (dataError) {
                $(this).removeClass('is-valid');
                $(this).addClass('is-invalid');
            } else {
                $(this).removeClass('is-invalid');
                $(this).addClass('is-valid');
            }
        }
    });
}

function LlamarParentWithErrors() {
    parent.$('body').trigger('formSubmitedWithErrors');
}

function BuscandoDatos() {
    $('#txtDNI').prop('disabled', true);
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