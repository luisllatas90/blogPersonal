var flagDocumentReady = false;
var flagCaptchaLoaded = false;
var flagCaptchaValidated = false;
var registroDesdeCampus = false;

$(document).ready(function () {
    $.fn.selectpicker.Constructor.BootstrapVersion = '4';

    flagDocumentReady = true;
    InicializarControles();

    $('body').animate({
        opacity: 1
    }, 150);

    $('#mdlGenerico').modal({
        backdrop: 'static',
        show: false
    });

    var callBackConfAceptar;
    var callBackConfHidden;
    $('#mdlConfirmacion').modal({
        backdrop: 'static',
        show: false
    });
    $('#mdlConfirmacion').on('hidden.bs.modal', function () {
        if (callBackConfHidden != undefined) {
            callBackConfHidden();
            callBackConfHidden = undefined;
        }
    });
    $('#mdlConfirmacion #btnModalConfirmAceptar').on('click', function (e) {
        $(this).prop('disabled', true);
        $('#btnModalConfirmCancelar').prop('disabled', true);
        if (callBackConfAceptar != undefined) {
            callBackConfAceptar();
            callBackConfAceptar = undefined;
        };
    });

    $('body').on('click', '#btnModalAceptar', function (e) {
        var $mensaje = $('#mensajeServer');
        var nombreControl = $mensaje.data('control');
        if (nombreControl != undefined && nombreControl != '') {
            $('#' + nombreControl).focus();
        }
    });

    $('body').on('click', '#chkTerminosCondiciones', function (e) {
        activacionBotonesSubmit();
    });

    $('body').on('click', '#chkConsideracionesAdmision', function (e) {
        activacionBotonesSubmit();
    });

    $('body').on('click', '#lnkLeerTerminosCondiciones', function (e) {
        MostrarTerminosCondiciones();
        e.preventDefault();
    });

    $('body').on('click', '#lnkLeerConsideracionesAdmision', function (e) {
        MostrarConsideracionesAdmision();
        e.preventDefault();
    });

    $('body').on('focusout', '.dropdown-toggle', function (e) {
        $(this).parent().find('.selectpicker').valid();
    })
    $('body').on('change', 'select', function (e) { $(this).valid(); });

    jQuery.validator.addMethod("comboRequired", function (value, element) {
        return this.optional(element) || value != '-1';
    }, 'Campo Obligatorio');

    jQuery.validator.addMethod('fecha', function (value, element) {  
        var dateRegex = /^(?=\d)(?:(?:31(?!.(?:0?[2469]|11))|(?:30|29)(?!.0?2)|29(?=.0?2.(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00)))(?:\x20|$))|(?:2[0-8]|1\d|0?[1-9]))([-.\/])(?:1[012]|0?[1-9])\1(?:1[6-9]|[2-9]\d)?\d\d(?:(?=\x20\d)\x20|$))?(((0?[1-9]|1[012])(:[0-5]\d){0,2}(\x20[AP]M))|([01]\d|2[0-3])(:[0-5]\d){1,2})?$/;
        return dateRegex.test(value);
    }, 'Ingrese una fecha válida');

    $('body').on('click', '#btnFakeInformarme', function (e) {
        e.preventDefault();

        $(this).prop('disabled', true);
        $('#btnFakeInscribirme').prop('disabled', true);
        var validator = $("#frmInscripcionInteresado").validate();
        if (validator.form()) {
            // Antes de realizar el registro valido el email
            var email = $('#txtEmail').val().trim();
            var yaExiste = $('#hddEmailCoincidente').val().trim();
            if (email != '' && yaExiste == '0') {
                var path = "https://apps.emaillistverify.com/api/verifEmail?secret=iofafn3rsgczdpvz1isr3&email=" + email;

                $.ajax({
                    type: "GET",
                    url: path,
                    cache: false,
                    async: false,
                    dataType: 'text',
                    success: function (response) {
                        if (response == "ok") {
                            $('#hddEmailVerificado').val('1');
                        } else {
                            $('#hddEmailVerificado').val('0');
                        }
                    },
                    error: function (a, b, c) {
                        console.log(a, b, c)
                    }
                });
            }
            $('#btnInformarme').trigger('click');
        } else {
            $(this).prop('disabled', false);
            $('#btnFakeInscribirme').prop('disabled', false);
        }
    });

    $('body').on('click', '#btnFakeInscribirme', function (e) {
        e.preventDefault();

        $(this).prop('disabled', true);
        $('#btnFakeInformarme').prop('disabled', true);
        var validator = $("#frmInscripcionInteresado").validate();
        if (validator.form()) {
            callBackConfHidden = function () {
                $('#btnFakeInscribirme').prop('disabled', false);
                $('#btnFakeInformarme').prop('disabled', false);
                $('#btnModalConfirmAceptar').prop('disabled', false);
                $('#btnModalConfirmCancelar').prop('disabled', false);
            };
            callBackConfAceptar = function () { $('#btnInscribirme').trigger('click'); }
            $('#mdlConfirmacion').modal('show');
        } else {
            $(this).prop('disabled', false);
            $('#btnFakeInformarme').prop('disabled', false);
        }
    });
});

function VerificarInscripcionCampus() {
    registroDesdeCampus = ($('#hddInscripcionCampus').val() == '1');

    if (registroDesdeCampus) {
        $('.captcha-y-terminos').css({ 'display': 'none' });
        $('#chkTerminosCondiciones').prop('checked', true);
        activacionBotonesSubmit();
    }
}

function InicializarControles() {
    if (flagDocumentReady) {
        $("#frmInscripcionInteresado").validate({
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
                txtDNIApoderado: {
                    required: true,
                    digits: true,
                    rangelength: [8, 8]
                },
                txtApePatApoderado: 'required',
                txtApeMatApoderado: 'required',
                txtNombresApoderado: 'required',
                txtNumCelApoderado: 'required',
                txtEmailApoderado: 'email',
                txtDNI: {
                    required: true,
                    digits: true,
                    rangelength: [8, 8]
                },
                txtApellidoPaterno: 'required',
                txtApellidoMaterno: 'required',
                txtNombres: 'required',
                txtNumCelular: {
                    required: true,
                    digits: true,
                },
                txtEmail: {
                    required: true,
                    email: true,
                },
                cmbDepartamento: 'comboRequired',
                cmbProvincia: 'comboRequired',
                cmbDistrito: 'comboRequired',
                txtDireccion: 'required',
                // dtpFecNacimiento: 'required',
                dtpFecNacimiento: {
                    required: true,
                    fecha: true
                },
                cmbSexo: 'comboRequired',
                cmbAnioEstudio: 'comboRequired',
                cmbDepartamentoInstEduc: 'comboRequired',
                cmbInstitucionEducativa: 'comboRequired',
                cmbCarreraProfesional: 'comboRequired',
            },
            messages: {
                txtDNI: {
                    required: 'Campo Obligatorio',
                    digits: 'DNI no válido',
                    rangelength: 'DNI no válido',
                },
                txtApellidoPaterno: 'Campo Obligatorio',
                txtApellidoMaterno: 'Campo Obligatorio',
                txtNombres: 'Campo Obligatorio',
                txtNumCelular: {
                    required: 'Campo Obligatorio',
                    digits: 'Solo se aceptan dígitos',
                },
                txtEmail: {
                    required: 'Campo Obligatorio',
                    email: 'Email no válido',
                },
                cmbDepartamento: 'Campo Obligatorio',
                cmbProvincia: 'Campo Obligatorio',
                cmbDistrito: 'Campo Obligatorio',
                txtDireccion: 'Campo Obligatorio',
                // dtpFecNacimiento: 'Campo Obligatorio',
                dtpFecNacimiento: {
                    required: 'Campo Obligatorio',
                    fecha: 'Ingrese una fecha válida'
                },
                cmbSexo: 'Campo Obligatorio',
                cmbAnioEstudio: 'Campo Obligatorio',
                cmbDepartamentoInstEduc: 'Campo Obligatorio',
                cmbInstitucionEducativa: 'Campo Obligatorio',
                cmbCarreraProfesional: 'Campo Obligatorio',
            }
        });

        // $('#dtpFecNacimiento').datepicker({
        //     language: 'es',
        //     format: 'dd/mm/yyyy'
        // });

        $('#dtpFecNacimiento').mask('00/00/0000');

        $('#cmbInstitucionEducativa').selectpicker({
            liveSearch: true,
            // liveSearchStyle: 'startsWith',
        });

        $('#cmbCarreraProfesional').selectpicker({
            liveSearch: true,
            // liveSearchStyle: 'startsWith',
        });

        if (!($('#rowCostos').data('oculto'))) {
            $('#rowCostos').fadeIn(0);
        } else {
            $('#rowCostos').fadeOut(0);
        }

        var $mdlRespuestaServer = $('#mdlRespuestaServer');
        $mdlRespuestaServer.modal({
            'backdrop': 'static',
            'show': false
        });
        var $mensaje = $mdlRespuestaServer.find('#mensajeServer');
        if ($mensaje.data('operacion-realizada')) {
            // $mensaje.addClass($mensaje.data('clase-rpta'));
            $('#mdlConfirmacion').modal('hide');
            $mdlRespuestaServer.modal('show');
            $('body, html').scrollTop(0);
        }

        var errorMensaje = $('#errorMensaje').html();
        if (errorMensaje.trim() != '') {
            console.log(errorMensaje)
        }

        VerificarInscripcionCampus();
        activacionBotonesSubmit();
    }
}

function activacionBotonesSubmit() {
    // var validacion = $('#chkTerminosCondiciones').is(':checked') && flagCaptchaValidated; //DESARROLLO
    var validacion = ($('#chkTerminosCondiciones').is(':checked') && (registroDesdeCampus || flagCaptchaValidated));
    $('#btnFakeInformarme').prop('disabled', !validacion);

    var consideracionesAdmision = ($('#chkConsideracionesAdmision').length == 1);
    if (consideracionesAdmision) {
        validacion = validacion && $('#chkConsideracionesAdmision').is(':checked');
    }
    $('#btnFakeInscribirme').prop('disabled', !validacion);

    if (validacion) {
        // Solo para los alumnos que quinto y egresados activo el botón de inscripción
        validacion = ['Q', 'E'].includes($('#cmbAnioEstudio').val());
        $('#btnFakeInscribirme').prop('disabled', !validacion);
    }
}

function imNotRobot() {
    flagCaptchaValidated = true;
    activacionBotonesSubmit();
}

function onloadCallback() {
    flagCaptchaLoaded = true;
    cargarCaptcha();
}

function cargarCaptcha() {
    if (flagCaptchaLoaded) {
        grecaptcha.render('validacionCaptcha', {
            'sitekey': '6LcYUmoUAAAAAMoNG382w_R4kquhM45M6I0Zerus',
            'callback': imNotRobot,
            'expired-callback': expCallback,
        });
        var expCallback = function () {
            flagCaptchaValidated = false;
            activacionBotonesSubmit();
            grecaptcha.reset();
        };

        var $captcha = $('#validacionCaptcha');
        $captcha.show();
    }
}

function MostrarTerminosCondiciones() {
    var $mdlGenerico = $('#mdlGenerico');
    $mdlGenerico.find('#mensajeGenerico').html($('#terminosCondiciones').html());
    $mdlGenerico.modal('show');
}

function MostrarConsideracionesAdmision() {
    var $mdlGenerico = $('#mdlGenerico');
    $mdlGenerico.find('#mensajeGenerico').html($('#consideracionesAdmision').html());
    $mdlGenerico.modal('show');
}