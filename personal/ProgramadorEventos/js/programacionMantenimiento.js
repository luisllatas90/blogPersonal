var flagDocumentReady = false;
var flagReadyFinished = false;
var $mdlMensajesServidor;
var callbackHideMsgServidor;
var $smartWizard = undefined;
var flagEditForm = false;
var procesandoStep = false;
var tempStep = {
    stepNumber: 0,
    stepDirection: '',
    stepMode: 'B' // Botones, T: Tabs
};

$(document).ready(function () {
    flagReadyFinished = false;
    flagDocumentReady = true;
    InicializarControles();

    $smartWizard = $('#smartwizard');
    flagEditForm = ($('#frmProgramacion').data('edit') === "True");

    var programacionManual = ($('#liSeleccionarInteresados').length > 0);

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
            showPreviousButton: programacionManual,
            showNextButton: programacionManual,
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
        if (!flagEditForm && (stepNumber < 1)) {
            $('body').find("#btnFakeSubmit").attr("disabled", true);
        } else {
            $('body').find("#btnFakeSubmit").attr("disabled", false);
        }

        if (stepNumber > 0) {
            document.getElementById("ifrmSeleccionarInteresados").contentWindow.InicializarControles();
        }
    });

    // $smartWizard.on('leaveStep', function (e, anchorObject, stepNumber, stepDirection) {
    //     tempStep.anchorObject = anchorObject;
    //     tempStep.stepDirection = stepDirection;

    //     if (stepDirection == 'forward') {
    //         if (!AsignarFiltros()) {
    //             toastr.error('No ha seleccionado a ningún interesado');
    //             return false;
    //         }
    //     }
    //     return true;
    // });

    $smartWizard.on('click', '.nav-link', function (e) {
        tempStep.stepNumber = parseInt($(this).data('step')) - 1;
        tempStep.stepMode = 'T';
    });

    $smartWizard.on('click', '.sw-btn-group button', function (e) {
        tempStep.stepMode = 'B';
    });

    if ($smartWizard.find('.nav-item.active').is(':last-child')) {
        $smartWizard.find("#btnFakeSubmit").attr("disabled", false);
    }

    $('body').on('click', '#btnFakeSubmit', function (e) {
        e.preventDefault();

        var programacionManual = ($('#liSeleccionarInteresados').length > 0);
        // if (programacionManual && !AsignarFiltros()) {
        //     toastr.error('No ha seleccionado a ningún interesado');
        //     return false;
        // }
        
        var $ifrmSeleccionarInteresados = $('#ifrmSeleccionarInteresados');
        var $ddlConvocatoria = $('#ddlConvocatoria', $ifrmSeleccionarInteresados.contents());
        var convocatoria = $ddlConvocatoria.val();
        
        if (programacionManual && (convocatoria == '0' || convocatoria == '')) {
            toastr.error('Debe seleccionar una convocatoria');
            $ddlConvocatoria.focus();
            return false;
        }

        AsignarFiltros();
        parent.$('body').trigger('formSubmiting');
        $('#btnRegistrar').trigger('click');
    });

    ///////////////////////////////

    $mdlMensajesServidor = $('#mdlMensajesServidor');

    $mdlMensajesServidor.on('hide.bs.modal', function () {
        if (callbackHideMsgServidor != undefined) {
            callbackHideMsgServidor();
        }
        callbackHideMsgServidor = undefined;
    });


    $('#ifrmSeleccionarInteresados').iFrameResize({});

    flagReadyFinished = true;
});

function InicializarControles() {
    $("#txtDescripcion").focus();

    if (flagDocumentReady) {
        InitComboVariablesProgramacion();

        $('#cboVariablesProgramacion').on('changed.bs.select', function (e, clickedIndex, isSelected, previousValue) {
            var tipoMensaje = $('#cboTipoMensaje').val();
            if (tipoMensaje != 'S') { return; }

            var options = [];
            $('#cboVariablesProgramacion option').each(function (e) {
                options.push($(this).val());
            });

            var variable = options[clickedIndex];
            var strVariable = '{{' + variable + '}}';
            var resultado = '';
            if (isSelected) {
                resultado = $('#txtID').val() + strVariable
            } else {
                resultado = $('#txtID').val().replace(strVariable, '')
            }

            $('#txtID').val(resultado);
        });

        if ($("#cboTipoFrecuencia").val() !== "S") {
            var $select;

            $("#txtFrecuenciaDiaSemana").val("");
            $select = $("#cboFrecuenciaDiaSemana").multipleSelect({
                placeholder: "Seleccione la semana",
                position: 'top',
                width: 200,
                multiple: true,
                multipleWidth: 80
            });

            $select.multipleSelect("setSelects", []);
            $select.multipleSelect("refresh");
            $select.multipleSelect("enable");
        }

        var min;
        $("#dtpFechaInicio").datepicker({
            format: "dd/mm/yyyy",
            maxViewMode: 2,
            language: "es",
            daysOfWeekHighlighted: "0",
            autoclose: true,
            todayHighlight: true
        }).on("changeDate", function (e) {
            min = new Date(getDate(this) + 1);

            // Si la fecha Fin está habilitado y está en check, pasar el enfoque
            if ($("#chkFechaFin").is(':checked') && !$("#dtpFechaFin").is(':disabled')) {
                $("#dtpFechaFin").datepicker({ minDate: min });
                $("#dtpFechaFin").datepicker({ startDate: min });
                $("#dtpFechaFin").focus();
            } else if ($("#dtpFechaFin").is(':disabled')) {
                $("#dtpHoraInicio").focus();
            }
        });

        $("#dtpFechaFin").datepicker({
            format: "dd/mm/yyyy",
            maxViewMode: 2,
            language: "es",
            daysOfWeekHighlighted: "0",
            autoclose: true,
            todayHighlight: true
        });

        if ($("#txtID").val() == null || $("#txtID").val() == "") {
            elegirTipoFrecuencia($('#cboTipoFrecuencia').val("D"));
            elegirTipoFrecuencia($('#cboTipoFrecuencia').trigger("change"));

            $("#dtpFechaFin").attr("disabled", "true");
            $("#dtpHoraInicio").attr("disabled", "true");

            $("#rbtFrecuenciaDiaMes").trigger("click");
            $("#rbtFrecuenciaA1").trigger("click");
            $("#txtDescripcion").focus();
        }
    }
}

function InitComboVariablesProgramacion() {
    $('#cboVariablesProgramacion').selectpicker({
        noneSelectedText: '-- Seleccione --'
    });
}

function getDate(element) {
    var date;
    try {
        dpg = $.fn.datepicker.DPGlobal;
        date = dpg.parseDate(element.value, dpg.parseFormat("dd/mm/yyyy"));
    } catch (error) {
        date = null;
    }

    return date;
}

function elegirTipoMensaje(opt) {
    if (flagReadyFinished) {
        $('#txtID').val('');
        $('#cboVariablesProgramacion').val('');
        $('#cboVariablesProgramacion').selectpicker('refresh');
    }

    if (opt.value == "S") {
        $("#lblID").text("Mensaje");
        $("#lblDestinatarioPrueba").text("Número Celular");
    } else {
        $("#lblID").text("ID del Template");
        $("#lblDestinatarioPrueba").text("Correo Electrónico");
    }
}

function elegirTipoProgramacion(opt) {
    if (opt.value == "P") {
        $("#chkFechaFin").removeAttr("disabled");

        if ($('#txtCategoria').val() != 'I') {
            $("#divFrecuenciaGeneral").removeAttr("disabled"); // Nunca se habilita la frecuencia cuando la categoría es I: Programación por fecha de registro de interesado
        }
        $("#divFrecuenciaDiaria").removeAttr("disabled");
        $("#dtpHoraInicio").val("");
        $("#dtpHoraInicio").attr("disabled", "true");

        $("#lblFechaInicio").text("Fecha de inicio");
    } else {
        $("#chkFechaFin").attr("disabled", "true");
        $("#dtpFechaFin").attr("disabled", "true");

        $("#divFrecuenciaGeneral").attr("disabled", "true");
        $("#divFrecuenciaDiaria").attr("disabled", "true");
        $("#dtpHoraInicio").removeAttr("disabled");

        //Valores por defecto
        $("#chkFechaFin").prop("checked", false);
        $("#dtpFechaFin").val("");
        $("#lblFechaInicio").text("Fecha de ejecución");
    }

    $("#dtpFechaFin").attr("disabled", "true");

    $("#rbtOrdinal").trigger("click");
    $("#rbtFrecuenciaDiaMes").trigger("click");
    $("#rbtFrecuenciaA2").trigger("click");
    $("#rbtFrecuenciaA1").trigger("click");

    if (opt.value == "P") {
        $("#cboTipoFrecuencia").focus();
    } else {
        $("#dtpFechaInicio").focus();
    }
}

function elegirTipoFrecuencia(opt) {
    if (opt.value == "D") { //Diaria
        $("#divFrecuenciaDiaSemana").hide();
        $("#divFrecuenciaA").show();
        $("#divFrecuenciaB").hide();
        $("#lblFrecuencia").text("día(s)");
    } else if (opt.value == "S") { //Semanal
        $("#divFrecuenciaDiaSemana").show();
        $("#divFrecuenciaA").show();
        $("#divFrecuenciaB").hide();
        $("#lblFrecuencia").text("semana(s)");
    } else if (opt.value == "M") { //Mensual
        $("#divFrecuenciaDiaSemana").hide();
        $("#divFrecuenciaA").hide();
        $("#divFrecuenciaB").show();
    }
}

function elegirFrecuenciaDiaSemana(opt) {
    if (opt == "S") {
        $("#txtFrecuenciaDiaSemana").val($("#cboFrecuenciaDiaSemana").multipleSelect("getSelects"));
    }
}

function elegirFrecuenciaDiaMes(opt) {
    if (opt.value == "rbtFrecuenciaDiaMes") {
        $("#spnFrecuenciaDiaMes").removeAttr("disabled");
        $("#spnFrecuenciaMes").removeAttr("disabled");

        $("#cboOrdinal").attr("disabled", "true");
        $("#cboDiaSemana").attr("disabled", "true");
        $("#spnMes").attr("disabled", "true");

        //valores por defecto
        $("#cboOrdinal").val("1");
        $("#cboDiaSemana").val("1");
        $("#spnMes").val("1");
        $("#spnFrecuenciaDiaMes").focus();
    } else if (opt.value == "rbtOrdinal") {
        $("#spnFrecuenciaDiaMes").attr("disabled", "true");
        $("#spnFrecuenciaMes").attr("disabled", "true");

        $("#cboOrdinal").removeAttr("disabled");
        $("#cboDiaSemana").removeAttr("disabled");
        $("#spnMes").removeAttr("disabled");

        //valores por defecto
        $("#spnFrecuenciaDiaMes").val("1");
        $("#spnFrecuenciaMes").val("1");
        $("#cboOrdinal").focus();
    }
}

function elegirFrecuenciaDiaria(opt) {
    if (opt.value == "rbtFrecuenciaA1") {
        $("#dtpHoraFrecuencia").removeAttr("disabled");

        $("#spnFrecuenciaHora").attr("disabled", "true");
        $("#cboFrecuenciaTiempo").attr("disabled", "true");
        $("#dtpHoraIniDia").attr("disabled", "true");
        $("#dtpHoraFinDia").attr("disabled", "true");

        //valores por defecto
        $("#spnFrecuenciaHora").val("1");
        $("#cboFrecuenciaTiempo").val("Hrs");
        $("#dtpHoraIniDia").val("");
        $("#dtpHoraFinDia").val("");
        $("#dtpHoraFrecuencia").focus();
    } else if (opt.value == "rbtFrecuenciaA2") {
        $("#dtpHoraFrecuencia").attr("disabled", "true");

        $("#spnFrecuenciaHora").removeAttr("disabled");
        $("#cboFrecuenciaTiempo").removeAttr("disabled");
        $("#dtpHoraIniDia").removeAttr("disabled");
        $("#dtpHoraFinDia").removeAttr("disabled");

        //valores por defecto
        $("#dtpHoraFrecuencia").val("");
        $("#spnFrecuenciaHora").focus();
    }
}

function elegirFechaFinalización() {
    if ($("#chkFechaFin").is(':checked')) {
        $("#dtpFechaFin").removeAttr("disabled");
        $("#dtpFechaFin").focus();
    } else {
        $("#dtpFechaFin").attr("disabled", "true");
        $("#dtpFechaFin").val("");
    }
}

function guardar() {
    var fechaIni = "";
    var fechaFin = "";

    $("#txtFrecuencia").val($("#spnFrecuencia").val());
    $("#txtFrecuenciaDiaSemana").val($("#cboFrecuenciaDiaSemana").val());
    $("#txtFrecuenciaDiaMes").val($("#spnFrecuenciaDiaMes").val());
    $("#txtFrecuenciaMes").val($("#spnFrecuenciaMes").val());
    $("#txtFrecuenciaHora").val($("#spnFrecuenciaHora").val());
    $("#txtMes").val($("#spnMes").val());
    $("#txtNumeroDias").val($("#spnNumeroDias").val());

    $("#txtHoraFrecuencia").val($("#dtpHoraFrecuencia").val());
    $("#txtHoraIniDia").val($("#dtpHoraIniDia").val());
    $("#txtHoraFinDia").val($("#dtpHoraFinDia").val());
    $("#txtHoraInicio").val($("#dtpHoraInicio").val());

    if ($("#dtpFechaInicio").datepicker("getDate") != null) {
        fechaIni = $("#dtpFechaInicio").datepicker("getDate").format("dd/MM/yyyy").valueOf();
    }

    if ($("#dtpFechaFin").is(":enabled")) {
        if ($("#dtpFechaFin").datepicker("getDate") != null) {
            fechaFin = $("#dtpFechaFin").datepicker("getDate").format("dd/MM/yyyy").valueOf();
        }
    }

    $("#txtFechaInicio").val(fechaIni);
    $("#txtFechaFin").val(fechaFin);
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function SubmitPostBack() {
    var $mdlMensajesServidor = $('#mdlMensajesServidor');
    var $divRespuestaPostback = $mdlMensajesServidor.find('#divRespuestaPostback');

    var rpta = $divRespuestaPostback.data('rpta');
    var msg = $divRespuestaPostback.data('msg');
    var control = $divRespuestaPostback.data('control');

    $divRespuestaPostback.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $divRespuestaPostback.addClass('alert-danger');
            $divRespuestaPostback.html(msg);
            $mdlMensajesServidor.modal('show');
            break;
        case 0:
            $divRespuestaPostback.addClass('alert-warning');
            $divRespuestaPostback.html(msg);
            $mdlMensajesServidor.modal('show');
            break;
        case 1:
            break;
    }
    parent.$('body').trigger('formSubmited');
}

function RevisarMensajePostback() {
    if (flagDocumentReady) {
        var $divMdlMenServParametros = $mdlMensajesServidor.find('#divMdlMenServParametros');

        var mostrar = $divMdlMenServParametros.data('mostrar');
        if (mostrar) {
            var $divRespuestaPostback = $mdlMensajesServidor.find('#divRespuestaPostback');
            $divRespuestaPostback.removeClass(function (index, className) {
                return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
            });

            var rpta = $divRespuestaPostback.data('rpta');
            var msg = $divRespuestaPostback.data('msg');
            var control = $divRespuestaPostback.data('control');

            switch (rpta) {
                case -1:
                    $divRespuestaPostback.addClass('alert-danger');
                    $divRespuestaPostback.html(msg);
                    $mdlMensajesServidor.modal('show');
                    break;
                case 0:
                    $divRespuestaPostback.addClass('alert-warning');
                    $divRespuestaPostback.html(msg);
                    $mdlMensajesServidor.modal('show');
                    if (control != "") {
                        callbackHideMsgServidor = function () { $('#' + control).focus() };
                    }
                    break;
                case 1:
                    $divRespuestaPostback.addClass('alert-success');
                    $divRespuestaPostback.html(msg);
                    $mdlMensajesServidor.modal('show');
                    break;
            }
            parent.$('body').trigger('scrollTop');
        }
    }
}

function AsignarFiltros() {
    var $ifrmSeleccionarInteresados = $('#ifrmSeleccionarInteresados');
    var $hddFiltrosSel = $('#hddFiltrosSel', $ifrmSeleccionarInteresados.contents());
    $('#hddFiltros').val($hddFiltrosSel.val());

    return $('#hddFiltros').val() != '';
}