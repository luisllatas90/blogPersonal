var $form;
var $mdlMensajesServidor;

$(document).ready(function () {
    InitJqueryVars();
    InitJqueryPlugins();
    InitComboTipoParticipante();

    $('body').on('keypress', '.decimal', function (e) {
        var digitos = $(this).data('decimales');
        digitos = (digitos == undefined ? 0 : digitos);
        var result = floatNumber(e, this, digitos);
        if (!result) {
            e.preventDefault();
        }
    });

    $('body').on('change', '#cmbTipoParticipante', function (e) {
        $(this).valid();
    });
});

function InitJqueryVars() {  
    $form = $('#frmDetalleConvenio');
    $mdlMensajesServidor = $('#mdlMensajesServidor');
}

function InitJqueryPlugins() { 
    $('#txtFechaPrimeraCuota').datepicker({
        language: 'es'
    });
    
    jQuery.validator.addMethod('cmbRequired', function (value, element) {
        return this.optional(element) || (value != '-1' && value != '');
    }, 'Este campo es obligatorio.');

    $form.validate({
        ignore: ".ignore",
        onfocusout: function (element) {
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
            cmbTipoParticipante: {
                cmbRequired: true,
            },
            txtOperacion: {
                required: true,
            },
        },
    });
}

function ValidarForm(e) {  
    var validator = $("#frmDetalleConvenio").validate();
    if (validator.form()) {
        parent.$('body').trigger('formProcessing');
        return true;
    } else {
        ErrorPostback();
        return false;
    }
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    var nodeName = $boton.prop('nodeName');
    if (nodeName != undefined) {
        switch (nodeName){
            case 'A':
                if (retorno) {
                    $boton.removeClass('disabled');
                } else {
                    $boton.addClass('disabled');
                }
                break;
            case 'BUTTON':
                $boton.prop('disabled', !retorno);
                break;
        }
    }
}

function AlternarLoadingGif(tipo, retorno) {
    var $loadingGif;
    switch (tipo) {
        case 'global':
            $loadingGif = $('.main .loading-gif');
            break;
        case 'cuotas':
            $loadingGif = $('#loading-gif-cuotas');
            break;
    }

    if ($loadingGif != undefined) {
        if (!retorno) {
            $loadingGif.fadeIn(150);
        } else {
            $loadingGif.fadeOut(150);
        }
    }
}

function InitComboTipoParticipante() {
    $('#cmbTipoParticipante').selectpicker();
}

function InitControlesCuotas() {  
    $('#grwCuotas').find('input[id$="txtFechaVencimiento"]').each(function (e) {  
        $(this).datepicker({
            language: 'es'
        });
    });
};

function SubmitPostBack() {
    RevisarRespuestaServidor();

    var $divRespuestaPostback = $mdlMensajesServidor.find('#divRespuestaPostback');
    var rpta = $divRespuestaPostback.data('rpta');
    var msg = $divRespuestaPostback.data('msg');

    parent.$('body').trigger('formSubmited', {
        rpta: rpta,
        msg: msg,
    });
}

function ErrorPostback() {  
    parent.$('body').trigger('formSubmited', {
        rpta: '-1',
        msg: '',
    });
}

function RevisarRespuestaServidor() {
    var $mdlMenServparametros = $mdlMensajesServidor.find('#divMdlMenServParametros');
    if ($mdlMenServparametros.data('mostrar')) {
        MostrarMensajeServidor();
    }
}

function MostrarMensajeServidor() {
    var $divRespuestaPostback = $mdlMensajesServidor.find('#divRespuestaPostback');
    $divRespuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    var rpta = $divRespuestaPostback.data('rpta');
    var msg = $divRespuestaPostback.data('msg');

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