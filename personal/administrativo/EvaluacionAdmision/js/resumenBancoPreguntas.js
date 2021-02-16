var flagDocumentReady = false;
var $mdlMensajeServidor = undefined;
var $mdlMensajeClienteConfirmar = undefined;
var $mdlVistaPreviaPreguntaSimple = undefined;
var $mdlVistaPreviaPreguntaCompuesta = undefined;
var $tabResumen = undefined;
var $tabSimple = undefined;
var $tabCompuesta = undefined;
var $btnAccionPorConfirmar = undefined;
var callbackConfirmar = undefined;

$(document).ready(function () {
    flagDocumentReady = true;

    // Inicializo controles genéricos
    $loadingGif = $('#loadingGif');

    $tabResumen = $('#nav-resumen-tab');
    $tabSimple = $('#nav-simple-tab');
    $tabCompuesta = $('#nav-compuesta-tab');

    // Inicializo modales
    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlMensajeClienteConfirmar = $('#mdlMensajeClienteConfirmar');
    $mdlMensajeClienteConfirmar.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlVistaPreviaPreguntaSimple = $('#mdlVistaPreviaPreguntaSimple');
    $mdlVistaPreviaPreguntaSimple.modal({
        backdrop: 'static',
        show: false,
    });

    $mdlVistaPreviaPreguntaCompuesta = $('#mdlVistaPreviaPreguntaCompuesta');
    $mdlVistaPreviaPreguntaCompuesta.modal({
        backdrop: 'static',
        show: false,
    });

    // Eventos
    $mdlMensajeClienteConfirmar.find('#btnConfContinuar').on('click', function (e) {
        atenuarBoton($(this).attr('id'));
        callbackConfirmar();
    });
    $mdlMensajeClienteConfirmar.on('hide.bs.modal', function (e) {
        callbackConfirmar = undefined;
        $btnAccionPorConfirmar.prop('disabled', false);
    });
    initFakeListener('btnFakeCancelarSimple', 'btnCancelarSimple');
    initFakeListener('btnFakeCancelarCompuesta', 'btnCancelarCompuesta');

    $mdlVistaPreviaPreguntaSimple.find('#btnVistaPrevContinuarSimple').on('click', function (e) {
        if ($('#chkContinuarSimple').prop('checked')) {
            atenuarBoton($(this).attr('id'));
            callbackConfirmar();
        } else {
            mostrarToastr('2', "Debe marcar el check de confirmación para proceder con el registro");
            $('#chkContinuarSimple').focus();
        }
    });

    $mdlVistaPreviaPreguntaCompuesta.find('#btnVistaPrevContinuarCompuesta').on('click', function (e) {
        if ($('#chkContinuarCompuesta').prop('checked')) {
            atenuarBoton($(this).attr('id'));
            callbackConfirmar();
        } else {
            mostrarToastr('2', "Debe marcar el check de confirmación para proceder con el registro");
            $('#chkContinuarCompuesta').focus();
        }
    });

    $('body').on('keypress', 'input.only-digits', function (e) {
        return onlyDigits(e);
    });

    // PREGUNTA SIMPLE
    // Guardar
    $('body').on('click', '#btnFakeGuardarSimple', function (e) {
        var $realButton = $('#btnGuardarSimple');
        if (validateFormSimple()) {
            var pregunta = $('#txtPreguntaSimple').val();
            $mdlVistaPreviaPreguntaSimple.find('.texto').html(pregunta);

            var alternativas = '';
            $('#udpAlternativasSimple textarea[id*="txtAlternativaSimple"]').each(function (i, e) {
                var correcta = $(this).closest('.tab-pane').find('input[id*="rbtRespuestaSimple"]').prop('checked');
                var clsCorrecta = correcta ? ' list-group-item-success' : '';

                alternativas += '<li class="list-group-item d-flex ' + clsCorrecta + '">';
                alternativas += '<span class="mr-1">' + (+i + 1) + '. ' + '</span>';
                alternativas += $(this).val() + '</li>';
                alternativas += '</li>';
            });
            $mdlVistaPreviaPreguntaSimple.find('.alternativas').html(alternativas);
            $mdlVistaPreviaPreguntaSimple.find('#chkContinuarSimple').prop('checked', false);

            $mdlVistaPreviaPreguntaSimple.modal('show');
            callbackConfirmar = function () {
                $realButton.trigger('click');
            }
        }
    });

    // PREGUNTA COMPUESTA
    $('body').on('click', '#grvPreguntasCompuesta button[id$="btnEditar"]', function (e) {
        var $tr = $(this).closest('tr');
        initPluginsModalCompuesta($tr);
    });

    $('body').on('click', '#grvPreguntasCompuesta .btn.aceptar', function (e) {
        var $tr = $(this).closest('tr');
        var valCompetencia = $tr.find('select[id*="cmbCompetenciaCompuesta"]').val();
        if (valCompetencia != -1) {
            var textCompetencia = $tr.find('select[id*="cmbCompetenciaCompuesta"] option:selected').text();
            $tr.find('.competencia').html(textCompetencia);
        } else {
            $tr.find('.competencia').html('--');
        }

        var valSubCompetencia = $tr.find('select[id*="cmbSubCompetenciaCompuesta"]').val();
        if (valSubCompetencia != -1) {
            var textSubCompetencia = $tr.find('select[id*="cmbSubCompetenciaCompuesta"] option:selected').text();
            $tr.find('.sub-competencia').html(textSubCompetencia);
        } else {
            $tr.find('.sub-competencia').html('--');
        }

        var valIndicador = $tr.find('select[id*="cmbIndicadorCompuesta"]').val();
        if (valIndicador != -1) {
            var textIndicador = $tr.find('select[id*="cmbIndicadorCompuesta"] option:selected').text();
            $tr.find('.indicador').html(textIndicador);
        } else {
            $tr.find('.indicador').html('--');
        }

        var valComplejidad = $tr.find('select[id*="cmbComplejidadCompuesta"]').val();
        if (valComplejidad != -1) {
            var textComplejidad = $tr.find('select[id*="cmbComplejidadCompuesta"] option:selected').text();
            $tr.find('.complejidad').html(textComplejidad);
        } else {
            $tr.find('.complejidad').html('--');
        }
    });

    // Guardar
    $('body').on('click', '#btnFakeGuardarCompuesta', function (e) {
        var $realButton = $('#btnGuardarCompuesta');
        if (validateFormCompuesta()) {
            var enunciado = $('#txtEnunciadoCompuesta').val();
            $mdlVistaPreviaPreguntaCompuesta.find('.enunciado').html(enunciado);

            $mdlVistaPreviaPreguntaCompuesta.find('.preguntas').html('');
            var $templatePregunta = $mdlVistaPreviaPreguntaCompuesta.find('.template-pregunta').html();

            $('#grvPreguntasCompuesta textarea[id*="txtPreguntaCompuesta"]').each(function (i, el) {
                var $modal = $(this).closest('.modal');
                var $pregunta = $($.parseHTML($templatePregunta));
                $pregunta.find('.title').html('Pregunta N° ' + (+i + 1));

                // Pregunta
                $pregunta.find('.texto').html($(this).val());

                // Alternativas
                var alternativas = '';
                $modal.find('textarea[id*="txtAlternativaCompuesta"]').each(function (i, el) {
                    var correcta = $(this).closest('.tab-pane').find('input[id*="rbtRespuestaCompuesta"]').prop('checked');
                    var clsCorrecta = correcta ? ' list-group-item-success' : '';

                    alternativas += '<li class="list-group-item d-flex ' + clsCorrecta + '">';
                    alternativas += '<span class="mr-1">' + (+i + 1) + '. ' + '</span>';
                    alternativas += $(this).val() + '</li>';
                    alternativas += '</li>';
                });
                $pregunta.find('.alternativas').html(alternativas);

                $mdlVistaPreviaPreguntaCompuesta.find('.preguntas').append($pregunta);
            });
            $mdlVistaPreviaPreguntaCompuesta.find('#chkContinuarCompuesta').prop('checked', false);

            $mdlVistaPreviaPreguntaCompuesta.modal('show');
            callbackConfirmar = function () {
                $realButton.trigger('click');
            }
        }
    });

    // ---------------------------------------------------------------------------------------------------------------
    // Corrección de summernote cuando está dentro de un modal
    $(document).on("show.bs.modal", '.modal', function (event) {
        var zIndex = 100000 + (10 * $(".modal:visible").length);
        $(this).css("z-index", zIndex);
        setTimeout(function () {
            $(".modal-backdrop").not(".modal-stack").first().css("z-index", zIndex - 1).addClass("modal-stack");
        }, 0);
    }).on("hidden.bs.modal", '.modal', function (event) {
        $(".modal:visible").length && $("body").addClass("modal-open");
    });

    $(document).on('inserted.bs.tooltip', function (event) {
        var zIndex = 100000 + (10 * $(".modal:visible").length);
        var tooltipId = $(event.target).attr("aria-describedby");
        $("#" + tooltipId).css("z-index", zIndex);
    });

    $(document).on('inserted.bs.popover', function (event) {
        var zIndex = 100000 + (10 * $(".modal:visible").length);
        var popoverId = $(event.target).attr("aria-describedby");
        $("#" + popoverId).css("z-index", zIndex);
    });
    // ---------------------------------------------------------------------------------------------------------------

    // Auto cursor cuando selecciono una pestaña de alternativas
    $('body').on('shown.bs.tab', 'div[id*="udpAlternativas"] .nav', function (e) {
        var href = $(e.target).attr('href');
        var $control = $(href).find('textarea[id*="txtAlternativa"]');
        if ($control.summernote('isEmpty')) {
            $control.summernote('focus');
        }
    });

    initPlugins();
});

function initPlugins() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function initFakeListener(fakeId, id) {
    $('body').on('click', '[id*="' + fakeId + '"]', function (e) {
        $btnAccionPorConfirmar = $(this);

        hideGrvTooltips();
        atenuarBoton($(this).attr('id'));

        var $realButton;
        var msg = '';
        var tipo = 0;
        switch (id) {
            case 'btnCancelarSimple':
                msg = 'Al cancelar perderá cualquier dato ingresado en el formulario, <b>¿Desea continuar?</b>';
                $realButton = $('#btnCancelarSimple')
                break;
            case 'btnCancelarCompuesta':
                msg = 'Al cancelar perderá cualquier dato ingresado en el formulario, <b>¿Desea continuar?</b>';
                $realButton = $('#btnCancelarCompuesta')
                break;
        }
        callbackConfirmar = function () {
            $realButton[0].click();
        }

        mostrarModalConfirmar(tipo, msg);
    });
}

function accionConfirmadaFinalizada() {
    atenuarBoton('btnConfContinuar', true);
    $mdlMensajeClienteConfirmar.modal('hide');
}

// PREGUNTA SIMPLE
function checkRptaGuardar(tipo) {
    var $modal;
    if (tipo == 'S') {
        $modal = $mdlVistaPreviaPreguntaSimple;
    }
    if (tipo == 'C') {
        $modal = $mdlVistaPreviaPreguntaCompuesta;
    }

    var paramsToastr = $('#hddParamsToastr').val();
    if (paramsToastr != '') {
        var rpta, msg;
        var datos = paramsToastr.split('|');

        for (var i = 0; i < datos.length; i++) {
            var valores = datos[i].split('=');

            if (valores[0] == 'rpta' && valores[1] == '1') {
                $modal.modal('hide');
            }
        }
    }
}

// PREGUNTA COMPUESTA
function initPluginsModalCompuesta($tr) {
    var $modal = $tr.find('.modal');

    $modal.each(function (i, e) {
        $(this).modal({
            backdrop: 'static',
            show: false,
        });
    });

    $tr.find('.selectpicker').each(function (i, e) {
        initSelectPicker($(this).attr('id'), {
            size: 5
        })
    });

    $tr.find('[id*="txtPreguntaCompuesta"]').each(function (i, e) {
        initFullEditor($(this).attr('id'), 200);
    });

    $tr.find('[id*="txtAlternativaCompuesta"]').each(function (i, e) {
        initMinEditor($(this).attr('id'));
    });

    var index = $modal.attr('data-index');
    var total = $('#grvPreguntasCompuesta tr').find('.modal').length;

    $modal.find('.index').html(index);
    $modal.find('.total').html(total);

    configRadioNames('C', $modal);
    $modal.modal('show');
}

function initSelectPickersContainer(id) {
    $('#' + id + ' .selectpicker').each(function (i, e) {
        initSelectPicker($(this).attr('id'), {
            size: 5
        })
    });
}


//
function validateFormSimple() {
    if ($('#cmbCompetenciaSimple').val() == '-1') {
        mostrarToastr('0', 'Debe seleccionar una competencia');
        $('#cmbCompetenciaSimple').focus();
        return false;
    }

    if ($('#cmbSubCompetenciaSimple').val() == '-1') {
        mostrarToastr('0', 'Debe seleccionar una sub competencia');
        $('#cmbSubCompetenciaSimple').focus();
        return false;
    }

    if ($('#cmbIndicadorSimple').val() == '-1') {
        mostrarToastr('0', 'Debe seleccionar un indicador');
        $('#cmbIndicadorSimple').focus();
        return false;
    }

    if ($('#cmbComplejidadSimple').val() == '-1') {
        mostrarToastr('0', 'Debe seleccionar un nivel de complejidad');
        $('#cmbComplejidadSimple').focus();
        return false;
    }

    if ($('#txtPreguntaSimple').summernote('isEmpty')) {
        mostrarToastr('0', 'Debe asignar el contenido a la pregunta');
        $('#txtPreguntaSimple').summernote('focus');
        return false;
    }

    var errorAlternativas = false;
    $('[id*="txtAlternativaSimple"]').each(function (i, e) {
        if ($(this).summernote('isEmpty')) {
            mostrarToastr('0', 'Debe asignar el contenido de la alternativa N° ' + (i + 1));
            errorAlternativas = true;
            return false;
        }
    });
    if (errorAlternativas) {
        return false;
    }

    if ($('[id*="rbtRespuestaSimple"]:checked').length == 0) {
        mostrarToastr('0', 'No ha marcado ninguna alternativa como respuesta correcta');
        return false;
    }

    return true;
}

function validateFormCompuesta() {
    if ($('#txtEnunciadoCompuesta').summernote('isEmpty')) {
        mostrarToastr('0', 'Debe asignar el texto del enunciado');
        $('#txtEnunciadoCompuesta').summernote('focus');
        return false;
    }

    if ($('#txtCantidadPreguntas').val().trim() == '') {
        mostrarToastr('0', 'Debe indicar la cantidad de preguntas');
        $('#txtCantidadPreguntas').focus();
        return false;
    }

    var errorPreguntas = false;
    var $modal;
    $('#grvPreguntasCompuesta [id*="txtPreguntaCompuesta"]').each(function (i, el) {
        $modal = $(this).closest('.modal');

        var $cmbCompetenciaCompuesta = $modal.find('select[id*="cmbCompetenciaCompuesta"]');
        if ($cmbCompetenciaCompuesta.val() == '-1') {
            mostrarToastr('0', 'Debe seleccionar una competencia para la pregunta N° ' + (+i + 1));
            $cmbCompetenciaCompuesta.focus();
            errorPreguntas = true;
            return false;
        }

        var $cmbSubCompetenciaCompuesta = $modal.find('select[id*="cmbSubCompetenciaCompuesta"]');
        if ($cmbSubCompetenciaCompuesta.val() == '-1') {
            mostrarToastr('0', 'Debe seleccionar una sub competencia para la pregunta N° ' + (+i + 1));
            $cmbSubCompetenciaCompuesta.focus();
            errorPreguntas = true;
            return false;
        }

        var $cmbIndicarorCompuesta = $modal.find('select[id*="cmbIndicarorCompuesta"]');
        if ($cmbIndicarorCompuesta.val() == '-1') {
            mostrarToastr('0', 'Debe seleccionar un indicador para la pregunta N° ' + (+i + 1));
            $cmbIndicarorCompuesta.focus();
            errorPreguntas = true;
            return false;
        }

        var $cmbComplejidadCompuesta = $modal.find('select[id*="cmbComplejidadCompuesta"]');
        if ($cmbComplejidadCompuesta.val() == '-1') {
            mostrarToastr('0', 'Debe seleccionar una complejidad para la pregunta N° ' + (+i + 1));
            $cmbComplejidadCompuesta.focus();
            errorPreguntas = true;
            return false;
        }

        var $txtPreguntaCompuesta = $modal.find('[id*="txtPreguntaCompuesta"]');
        if ($txtPreguntaCompuesta.summernote('isEmpty')) {
            mostrarToastr('0', 'Debe asignar el contenido a la pregunta N° ' + (+i + 1));
            $txtPreguntaCompuesta.focus();
            errorPreguntas = true;
            return false;
        }

        var errorAlternativas = false;
        $modal.find('[id*="txtAlternativaCompuesta"]').each(function (j, e) {
            if ($(this).summernote('isEmpty')) {
                mostrarToastr('0', 'Debe asignar el contenido de la alternativa N° ' + (j + 1) + ', pregunta N° ' + (+i + 1));
                errorAlternativas = true;
                return false;
            }
        });
        if (errorAlternativas) {
            errorPreguntas = true;
            return false;
        }

        if ($modal.find('[id*="rbtRespuestaCompuesta"]:checked').length == 0) {
            mostrarToastr('0', 'No ha marcado ninguna alternativa de la pregunta N° ' + (+i + 1) + ' como respuesta correcta');
            errorPreguntas = true;
            return false;
        }
    });
    if (errorPreguntas) {
        $modal.modal('show');
        return false;
    }

    var codigoCom = 0;
    $('#grvPreguntasCompuesta [id*="cmbCompetenciaCompuesta"]').each(function (i, el) {
        var thisCodigoCom = parseInt($(this).val());
        if (codigoCom != 0 && codigoCom != thisCodigoCom) {
            mostrarToastr('0', 'Las competencias de todas las preguntas deben ser las mismas');
            errorPreguntas = true;
            return false;
        }
        codigoCom = thisCodigoCom;
    })
    if (errorPreguntas) {
        return false;
    }

    return true;
}

function configRadioNames(tipo, $modal) {
    if (tipo == 'S') {
        $('#udpAlternativasSimple').find('input[type="radio"]').each(function (i, e) {
            $(this).attr('name', 'rbtRespuestaSimple');
            $(this).closest('.custom-control').find('label').attr('for', $(this).attr('id'));
        });
    }

    if (tipo == 'C') {
        var index = $modal.attr('data-index');
        $modal.find('input[type="radio"]').each(function (i, e) {
            $(this).attr('name', 'rbtRespuestaCompuesta' + (+index));
            $(this).closest('.custom-control').find('label').attr('for', $(this).attr('id'));
        });
    }
}

function initGrvTooltips() {
    $('#grvList [data-toggle="tooltip"]').tooltip({
        trigger: 'hover'
    });
}

function initFormPlugins(tipoVista) {
    initFormSelectPickers(tipoVista);

    switch (tipoVista) {
        case 'S':
            initFullEditor('txtPreguntaSimple', 250);

            $('[id*="txtAlternativaSimple"]').each(function (i, e) {
                initMinEditor($(this).attr('id'));
            });

            configRadioNames('S');
            break;
        case 'C':
            initFullEditor('txtEnunciadoCompuesta', 250);
            break;
    }
}

function initFullEditor(id, height) {
    $('#' + id).summernote({
        height: height,
        lang: 'es-ES',
        dialogsInBody: true,
        toolbar: [
            ['style', ['style']],
            ['font', ['bold', 'underline', 'clear']],
            ['fontname', ['fontname']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['table', ['table']],
            ['insert', ['picture']],
            ['view', ['fullscreen', 'codeview', 'help']],
        ]
    });
}

function initMinEditor(id) {
    $('#' + id).summernote({
        lang: 'es-ES',
        height: 100,
        dialogsInBody: true,
        toolbar: [
            ['style', ['bold', 'italic', 'underline', 'clear']],
            ['font', ['strikethrough', 'superscript', 'subscript']],
            ['fontsize', ['fontsize']],
            ['color', ['color']],
            ['para', ['ul', 'ol', 'paragraph']],
            ['insert', ['picture']],
            ['view', ['codeview']],
        ]
    });
}

function initFormSelectPickers(tipoVista) {
    switch (tipoVista) {
        case 'S':
            initSelectPicker('cmbCompetenciaSimple', {});
            initSelectPicker('cmbSubCompetenciaSimple', {
                size: 5
            });
            initSelectPicker('cmbIndicadorSimple', {
                size: 5,
                dropdownAlignRight: true,
            });
            initSelectPicker('cmbComplejidadSimple', {
                size: 5
            });
            break;
        case 'C':
            initSelectPicker('cmbCompetenciaCompuesta', {});
            initSelectPicker('cmbSubCompetenciaCompuesta', {
                size: 5
            });
            initSelectPicker('cmbIndicadorCompuesta', {
                size: 5,
                dropdownAlignRight: true,
            });
            initSelectPicker('cmbComplejidadCompuesta', {
                size: 5
            });
            break;
    }
}

function initDatetimePicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.datetimepicker(opciones);
}

function initSelectPicker(controlId, opciones) {
    var $control = $('#' + controlId);
    $control.selectpicker(opciones);
    $control.selectpicker('refresh');
}

function verificarParametros(tipoConsulta) {
    var tipos = tipoConsulta.split('|');

    for (var tipo of tipos) {
        switch (tipo) {
            case 'MEN_SERV':
                verificarMensajeServer();
                break;
            case 'TOASTR':
                verificarToastrServer();
                break;
            case 'TAB':
                var tipoVista = $('#hddTipoVista').val();
                seleccionarTab(tipoVista);
                break;
        }
    }
}

function mostrarModalConfirmar(tipo, msg) {
    var $divMensajeConfirmar = $mdlMensajeClienteConfirmar.find('#divMensajeConfirmar');
    $divMensajeConfirmar.removeClass('alert alert-danger alert-warning alert-success');
    $divMensajeConfirmar.html(msg);

    switch (tipo) {
        case -1:
            $divMensajeConfirmar.addClass('alert alert-danger');
            break;
        case 0:
            $divMensajeConfirmar.addClass('alert alert-warning');
            break;
        case 1:
            $divMensajeConfirmar.addClass('alert alert-success');
            break;
    }
    $mdlMensajeClienteConfirmar.modal('show');
}

function accionConfirmadaFinalizada() {
    atenuarBoton('btnConfContinuar', true);
    $mdlMensajeClienteConfirmar.modal('hide');
}

function hideGrvTooltips() {
    $('#grvList [data-toggle="tooltip"]').tooltip('hide');
}

function seleccionarTab(tipoVista) {
    switch (tipoVista) {
        case 'R':
            $tabResumen.removeClass('disabled');
            $tabResumen.tab('show');
            $tabSimple.addClass('disabled');
            $tabCompuesta.addClass('disabled');
            break;
        case 'S':
            $tabSimple.removeClass('disabled');
            $tabSimple.tab('show');
            $tabResumen.addClass('disabled');
            $tabCompuesta.addClass('disabled');
            break;
        case 'C':
            $tabCompuesta.removeClass('disabled');
            $tabCompuesta.tab('show');
            $tabResumen.addClass('disabled');
            $tabSimple.addClass('disabled');
            break;
    }
    initFormPlugins(tipoVista);
}


function atenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    if ($boton.is('button')) {
        $boton.prop('disabled', !retorno);
    }

    if ($boton.is('a')) {
        if (retorno) {
            $boton.removeClass('disabled');
        } else {
            $boton.addClass('disabled');
        }
    }
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

function verificarMensajeServer() {
    if (flagDocumentReady) {
        var mostrar = $('#hddMenServMostrar').val();
        var rpta = parseInt($('#hddMenServRpta').val());

        if (mostrar == 'true') {
            var $divMenServMensaje = $mdlMensajeServidor.find('#divMenServMensaje');
            $divMenServMensaje.removeClass('alert alert-danger alert-warning alert-success');
            $divMenServMensaje.html($('#hddMenServMensaje').val());

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

function mostrarMensajeModal(rpta, msg) {
    if (flagDocumentReady) {

        var $divMenServMensaje = $mdlMensajeServidor.find('#divMenServMensaje');
        $divMenServMensaje.removeClass('alert alert-danger alert-warning alert-success');
        $divMenServMensaje.html(msg);

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
    }
}

function verificarToastrServer() {
    var paramsToastr = $('#hddParamsToastr').val();
    if (paramsToastr != '') {
        var rpta, msg;

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
            }
        }
        mostrarToastr(rpta, msg);
    }
}

function mostrarToastr(tipo, mensaje) {
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
        case '2':
            toastr.info(mensaje);
            break;
    }
}