var flagDocumentReady = false;
var flagCaptchaLoaded = false;
var flagCaptchaValidated = false;
var $mdlMensajeServidor = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $('body').on('change', '#cmbTipoParticipante', function (e) {
        $(this).valid();
    });

    jQuery.validator.addMethod("", function (value, element) {
        return this.optional(element) || value != '-1';
    }, 'Campo Obligatorio');

    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[A-zÀ-ú\s]+$/i.test(value);
    }, "Solo se admiten letras");

    $("#frmEventoVirtual").validate({
        ignore: ".ignore",
        onChange: function (element) {
            console.log(element)
            $(element).valid();
        },
        errorElement: "em",
        errorPlacement: function (error, element) {
            error.addClass("invalid-feedback");

            switch (element.prop("type")) {
                case 'checkbox':
                    error.insertAfter(element.next("label"));
                    break;
                case 'radio':
                    error.insertAfter(element.closest('.form-group').find('.custom-radio:last-child'));
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
            // $(element).addClass("is-valid").removeClass("is-invalid");
            $(element).removeClass("is-invalid");
        },
        rules: {
            txtNombreCompleto: {
                required: true,
                lettersonly: true,
            },
            txtDocIdentidad: {
                required: true,
                digits: true,
                rangelength: [8, 8]
            },
            txtEmail: {
                required: true,
                email: true,
            },
            txtCelular: {
                required: true,
                digits: true,
            },
            cmbTipoParticipante: '',
            rbtConstancia: 'required',
            rbtTrabajando: 'required',
            txtEmpresa: {
                required: {
                    depends: function () {
                        return $('#rbtTrabajandoSi').prop('checked');
                    }
                }
            },
            txtCargo: {
                required: {
                    depends: function () {
                        return $('#rbtTrabajandoSi').prop('checked');
                    }
                }
            },
            rbtOfertaLaboral: {
                required: {
                    depends: function () {
                        return $('#rbtTrabajandoSi').prop('checked');
                    },
                }
            },
            chkTerminosCondiciones: 'required',
        },
        messages: {
            txtNombreCompleto: {
                required: 'Campo obligatorio',
                lettersonly: 'Solo se admiten letras',
            },
            txtDocIdentidad: {
                required: 'Campo Obligatorio',
                digits: 'DNI no válido',
                rangelength: 'DNI no válido',
            },
            txtEmail: {
                required: 'Campo Obligatorio',
                email: 'Email no válido',
            },
            txtCelular: {
                required: 'Campo Obligatorio',
                digits: 'Solo se aceptan dígitos',
            },
            cmbTipoParticipante: 'Campo Obligatorio',
            rbtConstancia: 'Campo Obligatorio',
            rbtTrabajando: 'Campo Obligatorio',
            txtEmpresa: 'Campo Obligatorio',
            txtCargo: 'Campo Obligatorio',
            rbtOfertaLaboral: 'Campo Obligatorio',
            chkTerminosCondiciones: 'Campo Obligatorio',
        }
    });

    $('#datosLaborales').collapse({ toggle: false });

    $('body').on('change', 'input[name="rbtTrabajando"]', function () {
        if ($('#rbtTrabajandoSi').prop('checked')) {
            $('#datosLaborales').collapse('show');
        } else {
            $('#datosLaborales').collapse('hide');
        }
    });

    $('body').on('click', '#chkTerminosCondiciones', function (e) {
        activarSubmit();
    });

    $('body').on('click', '#btnFakeEnviar', function (e) {
        var validator = $("#frmEventoVirtual").validate();
        if (validator.form()) {
            $('#btnEnviar').trigger('click');
        }
    });

    initCmbTipoParticipante();
});

function alternarLoadingGif(tipo, retorno) {
    var $control = undefined;
    switch (tipo) {
        case 'global':
            $control = $('#loadingGif');
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

function initCmbTipoParticipante() {
    $('#cmbTipoParticipante').selectpicker({
        // liveSearch: true,
    });
}

function imNotRobot() {
    flagCaptchaValidated = true;
    activarSubmit();
}

function onloadCallback() {
    flagCaptchaLoaded = true;
    cargarCaptcha();
}

function cargarCaptcha() {
    if (flagCaptchaLoaded) {
        grecaptcha.render('validacionCaptcha', {
            // 'sitekey': '6LcYUmoUAAAAAMoNG382w_R4kquhM45M6I0Zerus',
            'sitekey': '6LemTGAUAAAAAC4qhRnTPNDqY1XyNS35KSG6WxJo',
            'callback': imNotRobot,
            'expired-callback': expCallback,
        });
        var expCallback = function () {
            flagCaptchaValidated = false;
            activarSubmit();
            grecaptcha.reset();
        };

        var $captcha = $('#validacionCaptcha');
        $captcha.show();
    }
    flagCaptchaValidated = false;

    verificarMensajeServer();
}

function verificarCambiosAjax() {
    verificarToastrServer();
    verificarMensajeServer();
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

function activarSubmit() {
    var validacion = ($('#chkTerminosCondiciones').is(':checked') && flagCaptchaValidated);
    $('#btnFakeEnviar').prop('disabled', !validacion);
}

function enableDisableControl(controlId, deshabilitar) {
    var $control = $('#' + controlId);
    $control.prop('disabled', deshabilitar);
}