var flagDocumentReady = false;

var $mdlMensajesServidor;
var $mdlConfirmarInscripcion;
var callbackHideMsgServidor;

var $mdlMensajeConfirmacion;
var callbackHideMsgConfirmacion;

$(document).ready(function () {
    $.fn.selectpicker.Constructor.BootstrapVersion = '4';

    toastr.options = {
        positionClass: 'toast-top-full-width',
        timeOut: 2000,
    }

    InicializarControles();

    RevisarDescargaFichaInscripcion();

    $('body').on('click', '.modalidad', function (e) {
        $('.modalidad').not(this).each(function () {
            $(this).removeClass('active');
        });

        if (!$(this).hasClass('active')) {
            $(this).addClass('active');
        }
    });

    $('body').on('change', '#txtNroDocIdentidad', function () {
        __doPostBack('txtNroDocIdentidad');
    });

    $('#rbtNacionalidadPeruana').on('click', function () {
        var checked = $(this).data('checked');
        var $cmbPaisNacimiento = $('#cmbPaisNacimiento');

        if (checked === true) {
            $(this).prop('checked', false);
            $(this).data('checked', false);
            $cmbPaisNacimiento.prop('disabled', false);
            $('#btnFakeNacionalidadExtranjera').trigger('click');
        } else {
            $(this).data('checked', true);
            $cmbPaisNacimiento.prop('disabled', true);
            $('#btnFakeNacionalidadPeruana').trigger('click');
        }

        $cmbPaisNacimiento.selectpicker('refresh');
        $('.error-validation').empty();
    });

    $('#txtCelular').on('change', function (e) {
        $('#txtCelularToken').val($(this).val());
    });

    $('body').on('change keyup', '#txtEmail', function () {
        $('#ulValidacionEmail').html('');
    });

    $('body').on('change', '#chkCambiarCelular', function (e) {
        $('#txtCelularToken').prop('disabled', !$(this).is(':checked'));
    });

    $('.pregunta-discapacidad').on('click', function (e) {
        if (!$(this).hasClass('active')) {
            var $otroBoton = $(this).siblings('.pregunta-discapacidad');
            $(this).addClass('active');
            $otroBoton.removeClass('active');

            var respuesta = $(this).data('discapacidad');
            if (respuesta) {
                var mensaje = '<p>Si presenta alguna discapacidad, debe acercarse a nuestra Oficina de Admisión para su inscripción, presentar los siguientes requisitos: </p>';
                mensaje += '<ul>';
                mensaje += '<li>Certificado de estudios</li>';
                mensaje += '<li>Copia de DNI y de recibo de a agua o luz</li>';
                mensaje += '<li>Certificado que acredite la discapacidad emitido por  el CONADIS</li>';
                mensaje += '</ul>';
                MostrarMensajeModal(0, mensaje);
                $('#btnDatosPersonalesNext').hide();
                DesactivarTab('datos-academicos');
                DesactivarTab('datos-padres');
            } else {
                $('#btnDatosPersonalesNext').show();
            }
        }
    });

    $('#btnDatosPersonalesPrev').on('click', function (e) {
        var controlId = $(this).attr('id');
        HabilitarDeshabilitarBoton(controlId, true);

        SeleccionarTab('modalidad-carrera', function () {
            HabilitarDeshabilitarBoton(controlId, false);
        });
    });

    $('#btnDatosPersonalesNext').on('click', function (e) {
        var validator = $("#frmInscripcionAlumno").validate();
        var controlId = $(this).attr('id');

        if (validator.subset('#datos-personales')) {
            HabilitarDeshabilitarBoton(controlId, true);
            SeleccionarTab('datos-academicos', function () {
                HabilitarDeshabilitarBoton(controlId, false);
            });
        }
    });

    $('#btnDatosAcademicosPrev').on('click', function (e) {
        var controlId = $(this).attr('id');
        HabilitarDeshabilitarBoton(controlId, true);

        SeleccionarTab('datos-personales', function () {
            HabilitarDeshabilitarBoton(controlId, false);
        });
    });

    $('#btnDatosAcademicosNext').on('click', function (e) {
        var validator = $("#frmInscripcionAlumno").validate();
        var controlId = $(this).attr('id');

        if (validator.subset('#datos-academicos')) {
            SeleccionarTab('datos-padres', function () {
                HabilitarDeshabilitarBoton(controlId, false);
            });
        }
    });

    $('#btnDatosPadresPrev').on('click', function (e) {
        var controlId = $(this).attr('id');
        HabilitarDeshabilitarBoton(controlId, true);

        SeleccionarTab('datos-academicos', function () {
            HabilitarDeshabilitarBoton(controlId, false);
        });
    });

    $('#datos-padres .custom-control-input[type="checkbox"]').on('change', function () {
        var thisChecked = $(this).is(':checked');
        var $checkboxs = $('#datos-padres .custom-control-input[type="checkbox"]');

        var padreMadreChecked = false;
        $checkboxs.each(function () {
            if ($(this).attr('id') != 'chkRespPagoApoderado' && $(this).is(':checked')) {
                padreMadreChecked = true;
                return false;
            }
        });

        if (!padreMadreChecked) {
            $('#datos-padres .collapse').collapse('show');

            var idChk = $(this).attr('id');
            if (idChk != 'chkRespPagoApoderado') {
                $('#chkRespPagoApoderado').prop('checked', true);
            }

        } else {
            if (thisChecked) {
                $('#datos-padres .collapse').collapse('hide');
                $('#chkRespPagoApoderado').prop('checked', false);
            }
        }

        ResetearValidacion('datos-padres');
    });

    $('body').on('change', '#cmbOrdenMerito', function (e) {
        if ($(this).val() != "-1") {
            $('#txtOtroMerito').val('');
            $('#txtOtroMerito').prop('disabled', true);
        } else {
            $('#txtOtroMerito').prop('disabled', false);
        }
    });

    // Validaciones
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

    jQuery.validator.methods.email = function (value, element) {
        return this.optional(element) || /^([^@\s]+)@((?:[-a-z0-9]+\.)+[a-z]{2,})$/i.test(value);
    }

    $('#frmInscripcionAlumno').validate({
        ignore: ".ignore",
        onchange: function (element) {
            $(element).valid();
        },
        errorElement: "li",
        errorLabelContainer: '.error-validation',
        highlight: function (element, errorClass, validClass) {
            switch ($(element).prop('type')) {
                case 'select-one':
                    $(element).closest('.dropdown').addClass("is-invalid").removeClass("is-valid");
                    break;

                default:
                    $(element).addClass("is-invalid").removeClass("is-valid");
                    break;
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            switch ($(element).prop('type')) {
                case 'select-one':
                    $(element).closest('.dropdown').addClass("is-valid").removeClass("is-invalid");
                    break;

                default:
                    $(element).addClass("is-valid").removeClass("is-invalid");
                    break;
            }
        },
        rules: {
            cmbCarreraProfesional: { cmbRequired: true },
            txtNroDocIdentidad: {
                required: true,
                digits: {
                    depends: function () {
                        return $('#cmbTipoDocIdentidad').val() == '1'; //DNI
                    },
                },
                minlength: function () {
                    if ($('#cmbTipoDocIdentidad').val() == '1') {
                        return 8;
                    } else {
                        return 0;
                    }
                },
                maxlength: function () {
                    if ($('#cmbTipoDocIdentidad').val() == '1') {
                        return 8;
                    } else {
                        return 100;
                    }
                }
            },
            dtpFecNacimiento: { required: true },
            txtNombres: { required: true },
            txtApellidoPaterno: { required: true },
            txtApellidoMaterno: { required: true },
            cmbDepNacimiento: { cmbRequired: true },
            cmbProNacimiento: { cmbRequired: true },
            cmbDisNacimiento: { cmbRequired: true },
            txtDireccionActual: { required: true },
            cmbDepActual: { cmbRequired: true },
            cmbProActual: { cmbRequired: true },
            cmbDisActual: { cmbRequired: true },
            txtTelefono: { minlength: 6 },
            txtCelular: {
                required: true,
                minlength: 9,
                maxlength: 9,
            },
            txtEmail: {
                required: true,
                email: true,
            },
            cmbDepInstitucionEducativa: { cmbRequired: true },
            cmbInstitucionEducativa: { cmbRequired: true },
            cmbCondicionEstudiante: { cmbRequired: true },
            txtAnioEgreso: {
                digits: true,
                minlength: 4,
                maxlength: 4,
            },
            txtDniPadre: {
                minlength: 8,
                maxlength: 8,
            },
            txtNombresPadre: { required: true, },
            txtApellidoMaternoPadre: { required: true, },
            txtApellidoPaternoPadre: { required: true, },
            // dtpFecNacPadre: {
            //     required: {
            //         depends: function () { return $('#chkRespPagoPadre').is(':checked'); },
            //     },
            // },
            txtDireccionPadre: {
                required: {
                    depends: function () { return $('#chkRespPagoPadre').is(':checked'); },
                },
            },
            cmbDepPadre: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoPadre').is(':checked'); },
                },
            },
            cmbProPadre: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoPadre').is(':checked'); },
                },
            },
            cmbDisPadre: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoPadre').is(':checked'); },
                },
            },
            txtTelefonoPadre: { minlength: 6 },
            txtCelularPadre: {
                required: {
                    depends: function () { return $('#chkRespPagoPadre').is(':checked'); },
                },
                minlength: 9,
                maxlength: 9,
            },
            txtEmailPadre: {
                email: true,
            },
            chkRespPagoPadre: {
                required: {
                    depends: function () {
                        return !$('#chkRespPagoPadre').is(':checked') && !$('#chkRespPagoMadre').is(':checked') && !$('#chkRespPagoApoderado').is(':checked');
                    }
                }
            },
            txtDniMadre: {
                minlength: 8,
                maxlength: 8,
            },
            txtNombresMadre: { required: true, },
            txtApellidoMaternoMadre: { required: true, },
            txtApellidoPaternoMadre: { required: true, },
            // dtpFecNacMadre: {
            //     required: {
            //         depends: function () { return $('#chkRespPagoMadre').is(':checked'); },
            //     },
            // },
            txtDireccionMadre: {
                required: {
                    depends: function () { return $('#chkRespPagoMadre').is(':checked'); },
                },
            },
            cmbDepMadre: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoMadre').is(':checked'); },
                },
            },
            cmbProMadre: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoMadre').is(':checked'); },
                },
            },
            cmbDisMadre: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoMadre').is(':checked'); },
                },
            },
            txtTelefonoMadre: { minlength: 6 },
            txtCelularMadre: {
                required: {
                    depends: function () { return $('#chkRespPagoMadre').is(':checked'); },
                },
                minlength: 9,
                maxlength: 9,
            },
            txtEmailMadre: {
                email: true,
            },
            chkRespPagoMadre: {
                required: {
                    depends: function () {
                        return !$('#chkRespPagoPadre').is(':checked') && !$('#chkRespPagoMadre').is(':checked') && !$('#chkRespPagoApoderado').is(':checked');
                    }
                }
            },
            txtDniApoderado: {
                minlength: 8,
                maxlength: 8,
            },
            txtNombresApoderado: {
                required: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            txtApellidoMaternoApoderado: {
                required: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            txtApellidoPaternoApoderado: {
                required: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            // dtpFecNacApoderado: {
            //     required: {
            //         depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
            //     },
            // },
            txtDireccionApoderado: {
                required: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            cmbDepApoderado: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            cmbProApoderado: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            cmbDisApoderado: {
                cmbRequired: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
            },
            txtTelefonoApoderado: { minlength: 6 },
            txtCelularApoderado: {
                required: {
                    depends: function () { return $('#chkRespPagoApoderado').is(':checked'); },
                },
                minlength: 9,
                maxlength: 9,
            },
            txtEmailApoderado: {
                email: true,
            },
            chkRespPagoApoderado: {
                required: {
                    depends: function () {
                        return !$('#chkRespPagoPadre').is(':checked') && !$('#chkRespPagoMadre').is(':checked') && !$('#chkRespPagoApoderado').is(':checked');
                    }
                }
            },
            txtCelularToken: {
                required: true,
                minlength: 9,
                maxlength: 9,
            },
        },
        messages: {
            cmbCarreraProfesional: { cmbRequired: 'Debe seleccionar una carrera profesional', },
            txtNroDocIdentidad: {
                required: 'Debe ingresar su DNI',
                digits: 'El DNI solo debe contener dígitos',
                minlength: 'El DNI debe tener 8 caracteres',
                maxlength: 'El DNI debe tener 8 caracteres',
            },
            dtpFecNacimiento: { required: 'Debe ingresar su fecha de nacimiento', },
            txtNombres: { required: 'Debe ingresar sus nombres', },
            txtApellidoPaterno: { required: 'Debe ingresar su apellido paterno', },
            txtApellidoMaterno: { required: 'Debe ingresar su apellido materno', },
            cmbDepNacimiento: { cmbRequired: 'Debe seleccionar un departamento de nacimiento', },
            cmbProNacimiento: { cmbRequired: 'Debe seleccionar una provincia de nacimiento', },
            cmbDisNacimiento: { cmbRequired: 'Debe seleccionar un distrito de nacimiento', },
            txtDireccionActual: { required: 'Debe ingresar su dirección actual', },
            cmbDepActual: { cmbRequired: 'Debe seleccionar su departamento actual', },
            cmbProActual: { cmbRequired: 'Debe seleccionar su provincia actual', },
            cmbDisActual: { cmbRequired: 'Debe seleccionar su distrito actual', },
            txtTelefono: { minlength: 'Su número de teléfono debe contener al menos 6 caracteres', },
            txtCelular: {
                required: 'Debe ingresar su número celular',
                minlength: 'Su número de celular debe contener 9 caracteres',
                maxlength: 'Su número de celular debe contener 9 caracteres',
            },
            txtEmail: {
                required: 'Debe ingresar su correo electrónico',
                email: 'El formato del email no es correcto',
            },
            cmbDepInstitucionEducativa: { cmbRequired: 'Debe seleccionar el departamento de su colegio', },
            cmbInstitucionEducativa: { cmbRequired: 'Debe seleccionar su colegio', },
            cmbCondicionEstudiante: { cmbRequired: 'Debe seleccionar su condición', },
            txtAnioEgreso: {
                digits: 'El año de egreso solo debe contener dígitos',
                minlength: 'El año de egreso debe contener solo 4 caracteres',
                maxlength: 'El año de egreso debe contener solo 4 caracteres',
            },
            txtDniPadre: {
                minlength: 'El DNI debe del padre tener 8 caracteres',
                maxlength: 'El DNI debe del padre tener 8 caracteres',
            },
            txtNombresPadre: { required: 'Debe ingresar el nombre de su padre', },
            txtApellidoPaternoPadre: { required: 'Debe ingresar el apellido paterno de su padre', },
            txtApellidoMaternoPadre: { required: 'Debe ingresar el apellido materno de su padre', },
            dtpFecNacPadre: { required: 'Debe ingresar la fecha de nacimiento de su padre', },
            txtDireccionPadre: { required: 'Debe ingresar la dirección de su padre', },
            cmbDepPadre: { cmbRequired: 'Debe seleccionar el departamento donde vive su padre', },
            cmbProPadre: { cmbRequired: 'Debe seleccionar la provincia donde vive su padre', },
            cmbDisPadre: { cmbRequired: 'Debe seleccionar el distrito donde vive su padre', },
            txtTelefonoPadre: { minlength: 'El número de teléfono de su padre debe contener al menos 6 caracteres', },
            txtCelularPadre: {
                required: 'Debe ingresar el número celular de su padre',
                minlength: 'El número celular de su padre debe contener 9 caracteres',
                maxlength: 'El número celular de su padre debe contener 9 caracteres',
            },
            txtEmailPadre: {
                required: 'Debe ingresar el email de su padre',
                email: 'El formato del email de su padre no es correcto',
            },
            chkRespPagoPadre: {
                required: 'Debe seleccionar al menos un responsable de pago',
            },
            txtDniMadre: {
                minlength: 'El DNI debe de la madre tener 8 caracteres',
                maxlength: 'El DNI debe de la madre tener 8 caracteres',
            },
            txtNombresMadre: { required: 'Debe ingresar el nombre de su madre', },
            txtApellidoPaternoMadre: { required: 'Debe ingresar el apellido paterno de su madre', },
            txtApellidoMaternoMadre: { required: 'Debe ingresar el apellido materno de su madre', },
            dtpFecNacMadre: { required: 'Debe ingresar la fecha de nacimiento de su madre', },
            txtDireccionMadre: { required: 'Debe ingresar la dirección de su madre', },
            cmbDepMadre: { cmbRequired: 'Debe seleccionar el departamento donde vive su madre', },
            cmbProMadre: { cmbRequired: 'Debe seleccionar la provincia donde vive su madre', },
            cmbDisMadre: { cmbRequired: 'Debe seleccionar el distrito donde vive su madre', },
            txtTelefonoMadre: { minlength: 'El número de teléfono de su madre debe contener al menos 6 caracteres', },
            txtCelularMadre: {
                required: 'Debe ingresar el número celular de su madre',
                minlength: 'El número celular de su madre debe contener 9 caracteres',
                maxlength: 'El número celular de su madre debe contener 9 caracteres',
            },
            txtEmailMadre: {
                required: 'Debe ingresar el email de su madre',
                email: 'El formato del email de su madre no es correcto',
            },
            chkRespPagoMadre: {
                required: 'Debe seleccionar al menos un responsable de pago',
            },
            txtDniApoderado: {
                minlength: 'El DNI debe del apoderado tener 8 caracteres',
                maxlength: 'El DNI debe del apoderado tener 8 caracteres',
            },
            txtNombresApoderado: { required: 'Debe ingresar el nombre de su apoderado', },
            txtApellidoPaternoApoderado: { required: 'Debe ingresar el apellido paterno de su apoderado', },
            txtApellidoMaternoApoderado: { required: 'Debe ingresar el apellido materno de su apoderado', },
            dtpFecNacApoderado: { required: 'Debe ingresar la fecha de nacimiento de su apoderado', },
            txtDireccionApoderado: { required: 'Debe ingresar la dirección de su apoderado', },
            cmbDepApoderado: { cmbRequired: 'Debe seleccionar el departamento donde vive su apoderado', },
            cmbProApoderado: { cmbRequired: 'Debe seleccionar la provincia donde vive su apoderado', },
            cmbDisApoderado: { cmbRequired: 'Debe seleccionar el distrito donde vive su apoderado', },
            txtTelefonoApoderado: { minlength: 'El número de teléfono de su apoderado debe contener al menos 6 caracteres', },
            txtCelularApoderado: {
                required: 'Debe ingresar el número celular de su apoderado',
                minlength: 'El número celular de su apoderado debe contener 9 caracteres',
                maxlength: 'El número celular de su apoderado debe contener 9 caracteres',
            },
            txtEmailApoderado: {
                required: 'Debe ingresar el email de su apoderado',
                email: 'El formato del email de su apoderado no es correcto',
            },
            chkRespPagoApoderado: {
                required: 'Debe seleccionar al menos un responsable de pago',
            },
        }
    });

    $('#btnFinalizarInscripcion').on('click', function (e) {
        var validator = $("#frmInscripcionAlumno").validate();

        if (validator.subset('#datos-padres')) {
            // ValidarEmail();
            $mdlConfirmarInscripcion.modal('show');
        }
    });

    // Validaciones manuales
    $('#cmbCarreraProfesional').on('change', function (e) {
        $(this).valid();
    });

    $('body').on('change', '#cmbDepNacimiento', function (e) {
        ResetearValidacion('datos-personales');
        $(this).valid();
    });

    $('body').on('change', '#cmbProNacimiento', function (e) {
        ResetearValidacion('datos-personales');
        $(this).valid();
    });

    $('body').on('change', '#cmbDisNacimiento', function (e) {
        ResetearValidacion('datos-personales');
        $(this).valid();
    });

    $('body').on('change', '#cmbDepActual', function (e) {
        ResetearValidacion('datos-personales');
        $(this).valid();
    });

    $('body').on('change', '#cmbProActual', function (e) {
        ResetearValidacion('datos-personales');
        $(this).valid();
    });

    $('body').on('change', '#cmbDisActual', function (e) {
        ResetearValidacion('datos-personales');
        $(this).valid();
    });

    $('body').on('change', '#cmbDepInstitucionEducativa', function (e) {
        ResetearValidacion('datos-academicos');
        $(this).valid();
    });

    $('body').on('change', '#cmbInstitucionEducativa', function (e) {
        ResetearValidacion('datos-academicos');
        $(this).valid();
    });

    $('body').on('change', '#cmbCondicionEstudiante', function (e) {
        ResetearValidacion('datos-academicos');
        $(this).valid();
    });

    $('body').on('change', '#cmbDepPadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbProPadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbDisPadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbDepMadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbProMadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbDisMadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbDepApoderado', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbProApoderado', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#cmbDisApoderado', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#chkRespPagoPadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#chkRespPagoMadre', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('change', '#chkRespPagoApoderado', function (e) {
        ResetearValidacion('datos-padres');
        $(this).valid();
    });

    $('body').on('keypress', 'input.only-digits', function (e) {
        return onlyDigits(e);
    });

    $('body').on('keypress', 'input.only-letters', function (e) {
        return onlyLetters(e);
    });

    $('body').on('click', '#btnVerificarDatos', function (e) {
        if (!$(this).hasClass('disabled')) {
            $mdlConfirmarInscripcion.modal('hide');
            $('a[href="#datos-personales"]').trigger('click');
        }
    });

    $('#btnDescargarFicha').on('click', function (e) {
        var $this = $(this);
        // $this.prop('disabled', true);
        DescargarFichaInscripcion(function () {
            $this.prop('disabled', false);
        });
    });

    $('#btnEnviarFichaCorreo').on('click', function (e) {
        var nombreCompleto = $('#txtApellidoPaterno').val().toUpperCase() + ' '
            + $('#txtApellidoMaterno').val().toUpperCase() + ' '
            + $('#txtNombres').val().toUpperCase();
        var email = 'informes.admision@usat.edu.pe';
        var subject = 'Ficha de inscripción ' + nombreCompleto;
        var emailBody = '';
        window.location = 'mailto:' + email + '?subject=' + subject + '&body=' + emailBody;
    });

    flagDocumentReady = true;
});

function InicializarControles() {
    $mdlMensajesServidor = $('#mdlMensajesServidor');
    $mdlMensajesServidor.modal({
        backdrop: 'static',
        show: false,
    });
    $mdlMensajesServidor.on('hide.bs.modal', function () {
        if (callbackHideMsgServidor != undefined) {
            callbackHideMsgServidor();
        }
        callbackHideMsgServidor = undefined;
    });

    $mdlConfirmarInscripcion = $('#mdlConfirmarInscripcion');
    $mdlConfirmarInscripcion.modal({
        backdrop: 'static',
        keyboard: false,
        show: false,
    });
    $mdlConfirmarInscripcion.on('show.bs.modal', function () {
        $('#datos-padres').addClass('largo');
    })
    $mdlConfirmarInscripcion.on('hidden.bs.modal', function () {
        $('#datos-padres').removeClass('largo');
    });

    $mdlMensajeConfirmacion = $('#mdlMensajeConfirmacion');
    $mdlMensajeConfirmacion.modal({
        backdrop: 'static',
        show: false,
    });
    $mdlMensajeConfirmacion.on('hide.bs.modal', function () {
        if (callbackHideMsgConfirmacion != undefined) {
            callbackHideMsgConfirmacion();
        }
        callbackHideMsgConfirmacion = undefined;
    });

    CargarCombo('cmbCarreraProfesional', { liveSearch: true });
    CargarCombo('cmbTipoDocIdentidad');
    CargarCombo('cmbPaisNacimiento', { liveSearch: true });
    CargarDatepicker('dtpFecNacimiento', { language: 'es' });
    CargarCombo('cmbInstitucionEducativa', {
        liveSearch: true,
        size: 6,
    });

    CargarDatepicker('dtpFecNacPadre', { language: 'es' });
    CargarDatepicker('dtpFecNacMadre', { language: 'es' });
    CargarDatepicker('dtpFecNacApoderado', { language: 'es' });

    $('#txtOtroMerito').prop('disabled', true);
}

function PanelOrdenMeritoCargado() {
    CargarCombo('cmbOrdenMerito');
    $('#cmbOrdenMerito').trigger('change');
}

function ValidarPaso1(controlId) {
    var validacion = ValidarClickLinkButton(controlId);
    if (!validacion) {
        return false;
    }

    validacion = ($('#cmbCarreraProfesional').val() != '-1');
    if (!validacion) {
        $('#cmbCarreraProfesional').focus();
    } else {
        HabilitarDeshabilitarBoton(controlId, true);
    }
    return validacion;
}

function ValidarChecksInscripcion() {
    if (!$('#chkCondicion1').is(':checked')) {
        $('#chkCondicion1').focus();
        toastr.error('Debe aceptar todas las condiciones', 'Error', { positionClass: 'toast-bottom-right' });
        return false;
    }

    if (!$('#chkCondicion2').is(':checked')) {
        $('#chkCondicion2').focus();
        toastr.error('Debe aceptar todas las condiciones', 'Error', { positionClass: 'toast-bottom-right' });
        return false;
    }

    if (!$('#chkCondicion3').is(':checked')) {
        $('#chkCondicion3').focus();
        toastr.error('Debe aceptar todas las condiciones', 'Error', { positionClass: 'toast-bottom-right' });
        return false;
    }

    // if (!$('#chkCondicion4').is(':checked')) {
    //     $('#chkCondicion4').focus();
    //     toastr.error('Debe aceptar todas las condiciones', 'Error', { positionClass: 'toast-bottom-right' });
    //     return false;
    // }

    return true;
}

function ValidarGenerarToken() {
    if (!ValidarChecksInscripcion()) {
        return false;
    }

    if (!ValidarClickLinkButton('btnGenerarToken')) {
        return false;
    }

    return true;
}

function ValidarConfirmarInscripcion() {
    if (!ValidarClickLinkButton('btnConfirmarInscripcion')) {
        return false;
    }

    if (!ValidarChecksInscripcion()) {
        return false;
    }

    $('#btnVerificarDatos').removeClass('disabled');
    $('#btnVerificarDatos').addClass('disabled');

    $('#btnConfirmarInscripcion').removeClass('disabled');
    $('#btnConfirmarInscripcion').addClass('disabled');

    return true;
}

function ValidarEmail() {
    var email = $('#txtEmail').val().trim();
    // var noEncontrado = ($('#hddEmailCoincidente').val().trim() == '0');
    var noVerificado = ($('#hddEmailVerificado').val().trim() == '0');

    var validator = $("#frmInscripcionAlumno").validate();
    var emailValido = validator.element('#txtEmail');

    if (emailValido && noVerificado) {
        loadingValidacionEmail(true);

        var path = "https://apps.emaillistverify.com/api/verifEmail?secret=iofafn3rsgczdpvz1isr3&email=" + email;
        $.ajax({
            type: "GET",
            url: path,
            cache: false,
            // async: false,
            dataType: 'text',
            success: function (response) {
                if (response == "ok") {
                    $('#hddEmailVerificado').val('1');
                    $('#ulValidacionEmail').html('');
                } else {
                    $('#hddEmailVerificado').val('0');
                    $('#ulValidacionEmail').html('<li>El formato de correo ingresado es correcto, aunque no se ha podido determinar su validez. Puede continuar o probar con otro correo.</li>');
                }
            },
            error: function (a, b, c) {
                console.log(a, b, c)
            },
            complete: function () {  
                loadingValidacionEmail(false);
            }
        });
    } else {
        $('#ulValidacionEmail').html('');
    }
}

function loadingValidacionEmail(cargando) {
    if (cargando) {
        $('#spLoadingValidacionEmail').html('<img src="img/loading.gif">Se está verificando la existencia del email ingresado, espere un momento por favor.');
    } else {
        $('#spLoadingValidacionEmail').html('');
    }
}

function ResetearValidacion(step_id) {
    $('.error-validation').empty();

    var validado = ($('#' + step_id).find('.is-invalid').length > 0);
    var validator = $("#frmInscripcionAlumno").validate();
    validator.resetForm();

    if (validado) {
        validator.subset('#' + step_id);
    }
}

function SeleccionarTab(tabId, callback) {
    var $tab = $('#pasos-tabs a[href="#' + tabId + '"]');
    $tab.removeClass('disabled');
    $tab.tab('show');

    $('html, body').animate({ scrollTop: 0 }, 350);

    if (callback != undefined) {
        callback();
    }
}

function DesactivarTab(tabId) {
    var $navItem = $('.nav-item[data-toggle="tab"][href="#' + tabId + '"]');
    $navItem.removeClass('show active');
    $navItem.addClass('disabled');
}

function CargarCombo(comboId, options) {
    $('#' + comboId).selectpicker(options);
}

function CargarDatepicker(datepickerId, options) {
    $('#' + datepickerId).datepicker(options);
}

function HabilitarDeshabilitarBoton(controlId, deshabilitar) {
    var $control = $('#' + controlId);

    if (deshabilitar) {
        $control.addClass('disabled');
    } else {
        $control.removeClass('disabled');
    }
}

function ValidarClickLinkButton(controlId) {
    var $element = $('#' + controlId);
    return !$element.hasClass('disabled');
}

function RevisarMensajePostback() {
    if (flagDocumentReady) {
        var $divMdlMenServParametros = $mdlMensajesServidor.find('#divMdlMenServParametros');

        var mostrar = $divMdlMenServParametros.data('mostrar');
        if (mostrar) {
            var $divRespuestaPostback = $mdlMensajesServidor.find('#divRespuestaPostback');

            var rpta = $divRespuestaPostback.data('rpta');
            var msg = $divRespuestaPostback.data('msg');
            var control = $divRespuestaPostback.data('control');

            $mdlConfirmarInscripcion.modal('hide');
            $('#btnVerificarDatos').removeClass('disabled');
            $('#btnConfirmarInscripcion').removeClass('disabled');

            switch (rpta) {
                case 0:
                    if (control != '') {
                        callbackHideMsgServidor = function () { $('#' + control).focus(); }
                    }
                    break;
            }

            MostrarMensajeModal(rpta, msg);
        }
    }
}

function MostrarMensajeModal(rpta, msg) {
    var $divRespuestaPostback = $mdlMensajesServidor.find('#divRespuestaPostback');
    $divRespuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $divRespuestaPostback.addClass('alert-danger');
            break;
        case 0:
            $divRespuestaPostback.addClass('alert-warning');
            break;
        case 1:
            $divRespuestaPostback.addClass('alert-success');
            break;
    }

    $divRespuestaPostback.html(msg);
    $mdlMensajesServidor.modal('show');
}

function RevisarMensajeConfirmacion() {
    if (flagDocumentReady) {
        var $divMdlMenConfParametros = $mdlMensajeConfirmacion.find('#divMdlMenConfParametros');

        var mostrar = $divMdlMenConfParametros.data('mostrar');
        if (mostrar) {
            $mdlConfirmarInscripcion.modal('hide');

            var $divRespuestaConfirmacion = $mdlMensajeConfirmacion.find('#divRespuestaConfirmacion');
            var msg = $divRespuestaConfirmacion.data('msg');
            $divRespuestaConfirmacion.html(msg);

            callbackHideMsgConfirmacion = function () { location.reload(); }
            $mdlMensajeConfirmacion.modal('show');
        }
    }
}

function RevisarMensajePostbackToastr() {
    var $divMensajesToastr = $('#divMensajesToastr');
    var mostrar = $divMensajesToastr.data('mostrar');

    if (mostrar) {
        var rpta = $divMensajesToastr.data('rpta');
        var msg = $divMensajesToastr.data('msg');
        MostrarToastr(rpta, msg);
    }
}

function MostrarToastr(rpta, msg) {
    switch (rpta) {
        case -1:
            toastr.error(msg, 'Error', { positionClass: 'toast-bottom-full-width' });
            break;

        case 0:
            toastr.warning(msg, 'Alerta', { positionClass: 'toast-bottom-full-width' });
            break;

        case 1:
            toastr.info(msg, 'Finalizado', { positionClass: 'toast-bottom-full-width' });
            break;
    }
}

function RevisarDescargaFichaInscripcion() {
    var $divDataFichaInscripcion = $('#divDataFichaInscripcion');

    var mostrar = $divDataFichaInscripcion.data('mostrar');
    if (mostrar) {
        var alu = $divDataFichaInscripcion.data('alu');

        var objData = {
            alu: alu,
        }
        var params = $.param(objData);
        var url = $('#hddPathFicha').val();
        $.fileDownload(url, {
            httpMethod: 'POST',
            data: params,
            successCallback: function (url) {
                console.log(url);
            },
            failCallback: function (html, url) {
                MostrarMensajeModal(-1, "Se produjo un error al generar la ficha de inscripción");
                console.log(html);
                console.log(url);
            }
        });
    }
}

function DescargarFichaInscripcion(callback) {
    var $divDataFichaInscripcion = $('#divDataFichaInscripcion');

    var alu = $divDataFichaInscripcion.data('alu');
    var objData = { alu: alu }
    var params = $.param(objData);
    var url = $('#hddPathFicha').val();

    $.fileDownload(url, {
        httpMethod: 'POST',
        data: params,
        successCallback: function (url) {
            if (callback != undefined) { callback() };
        },
        failCallback: function (html, url) {
            MostrarMensajeModal(-1, "Se produjo un error al generar la ficha de inscripción");
            if (callback != undefined) { callback() };
            console.log(html);
            console.log(url);
        }
    });
}

function DeshabilitarCelularToken() {
    $('#chkCambiarCelular').prop('checked', false);
    $('#chkCambiarCelular').trigger('change');
}

function RevisarValidacionDocIdentidad() {
    var $divRespuestaPostback = $('#divRespuestaPostback');
    var rpta = $divRespuestaPostback.data('rpta');

    // La validación lanza un error, entonces bloqueo el botón "siguiente"
    $('#btnDatosPersonalesNext').prop('disabled', rpta == -1);
    DesactivarTab('datos-academicos');
    DesactivarTab('datos-padres');
}