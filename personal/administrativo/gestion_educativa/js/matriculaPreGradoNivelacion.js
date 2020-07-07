var flagDocumentReady = false;
var $mdlMensajesServidor;
var callbackHideMsgServidor;

var $ifrmActive = undefined;
var $mdlActive = undefined;

$(document).ready(function () {
    InitElementos();
    InitCombos();

    $('body').animate({
        opacity: 1
    }, 150);

    $mdlMensajesServidor.on('hide.bs.modal', function () {
        if (callbackHideMsgServidor != undefined) {
            callbackHideMsgServidor();
        }
        callbackHideMsgServidor = undefined;
    });

    var qryChecks = '#grwAlumnos tr:not(.thead-dark) input[id$="chkElegir"]'; // Query para obtener los checks de la grilla de alumnos
    $('body').on('click', '#grwAlumnos .thead-dark input[id$="chkHeader"]', function (e) {
        var checked = $(this).is(':checked');
        if (!checked) {
            $(qryChecks).each(function (e) {
                $(this).prop('disabled', false);
            });
        }

        var checkeados = 0;
        var vacantes = $('#spnVacantes').data('vacantes');
        if (vacantes == 0) {
            $(this).prop('disabled', true);
            e.preventDefault();
            e.stopPropagation();
        } else {
            $(this).closest('#grwAlumnos').find('tr:not(.thead-dark)').each(function () {
                var $chkElegir = $(this).find('input[id$="chkElegir"]');
                if (checked) {
                    if ($chkElegir.length > 0) {
                        if (checkeados < vacantes) {
                            $chkElegir.prop('checked', checked);
                            checkeados += 1
                        } else {
                            $chkElegir.prop('disabled', true);
                        }
                    }
                } else {
                    $chkElegir.prop('checked', checked);
                }
            });
        }
    });

    $('body').on('click', qryChecks, function (e) {
        var vacantes = $('#spnVacantes').data('vacantes');
        var checkeados = $(qryChecks + ':checked').length;

        var $chkHeader = $('#grwAlumnos .thead-dark input[id$="chkHeader"]');

        if (checkeados < vacantes) {
            $chkHeader.prop('checked', false);
            $(qryChecks).each(function (e) {
                $(this).prop('disabled', false);
            })
        } else {
            if (vacantes == 0) {
                $chkHeader.prop('disabled', true);
                $(qryChecks).each(function (e) {
                    $(this).prop('disabled', true);
                });
            } else {
                $chkHeader.prop('checked', true);
                $(qryChecks + ':not(:checked)').each(function (e) {
                    $(this).prop('disabled', true);
                });
            }
        }

        if (checkeados > vacantes) {
            e.preventDefault();
            e.stopPropagation();
        }
    });

    flagDocumentReady = true;
});

function InitElementos() {
    $mdlMensajesServidor = $('#mdlMensajesServidor');
}

function InitCombos() {
    InitComboCentroCosto();
    InitComboPlanEstudios();
    InitComboCicloAcademico();
    InitComboCarreraProfesional();
    InitComboGrupoHorario();
    InitComboCursosProgramados();
    InitComboEstadoPagoMatricula();
}

function InitComboCentroCosto() {
    $('#cmbCentroCosto').selectpicker({
        liveSearch: true,
        size: 10,
        noneSelectedText: '-- Seleccione --',
    });
}

function InitComboPlanEstudios() {
    $('#cmbPlanEstudios').selectpicker({
        liveSearch: true,
        size: 10,
        noneSelectedText: '-- Seleccione --',
    });
}

function InitComboCicloAcademico() {
    $('#cmbCicloAcademico').selectpicker({
        size: 10,
        noneSelectedText: '-- Seleccione --',
    });
}

function InitComboCarreraProfesional() {
    $('#cmbCarreraProfesional').selectpicker({
        liveSearch: true,
        size: 10,
        noneSelectedText: '-- Seleccione --',
    });
};

function InitComboGrupoHorario() {
    $('#cmbGrupoHorario').selectpicker({
        noneSelectedText: '-- Seleccione --',
    });
}

function InitComboCursosProgramados() {
    $('#cmbCursosProgramados').selectpicker({
        noneSelectedText: '-- Seleccione --',
    });
}

function InitComboEstadoPagoMatricula() {
    $('#cmbEstadoPagoMatricula').selectpicker({
        noneSelectedText: '-- Seleccione --',
    });
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function AtenuarLoadingGif(retorno) {
    var $loadingGif = $('#loading-gif');

    if ($loadingGif != undefined) {
        if (!retorno) {
            $loadingGif.fadeIn(150);
        } else {
            $loadingGif.fadeOut(150);
        }
    }
}

function AtenuarElemento(elementId, retorno) {
    var $element = $('#' + elementId);

    if ($element != undefined) {
        if (!retorno) {
            $element.animate({
                opacity: 0.7
            }, 150);
        } else {
            $element.animate({
                opacity: 1
            }, 150);
        }
    }
}

function VerificarLoadingGif() {
    var codigoPes = $('#cmbPlanEstudios').val();
    var codigoCac = $('#cmbCicloAcademico').val();
    var codigosCur = $('#cmbCursosProgramados').val();
    var valueGrupoHorario = $('#cmbGrupoHorario').val();

    if (codigoPes != '-1' && codigoCac != '-1' && codigosCur.length > 0 && valueGrupoHorario != '-1') {
        AtenuarLoadingGif(false);
    }
}

function CargarModalRetiro(btnCallerId) {
    $mdlActive = $('#mdlRetirarMatricula');
    var modalProperties = {
        backdrop: 'static',
        show: false,
        keyboard: false,
    };
    LoadModal(modalProperties, btnCallerId);
}

function LoadModal(modalProperties, btnIdParentCaller, btnIdForTriggerClick) {
    $mdlActive.modal('hide');
    $mdlActive.modal(modalProperties);

    var $btnSubmit = $mdlActive.find('.submit');
    $ifrmActive = $mdlActive.find('iframe');

    var routine = function () {
        AlternarLoadingLayer(true);
        AlternarLoadingGif('interno', true);
        $mdlActive.modal('show');

        if (btnIdParentCaller != undefined) {
            AtenuarBoton(btnIdParentCaller, true)
        }

        if (btnIdForTriggerClick != undefined) {
            $btnSubmit.on('click', function () {
                $(this).prop('disabled', true);
                AtenuarModalActivo(false);
                $ifrmActive.contents().find('#' + btnIdForTriggerClick).trigger('click');
            });
        }
    }

    if ($ifrmActive.length == 0) {
        routine()
    } else {
        $ifrmActive.iFrameResize({
            // autoResize: false
            // minHeight: 200
        });
        $ifrmActive.on('load', function () {
            routine();
        });
    }
}

function AlternarLoadingLayer(retorno) {
    var $loadingLayer = $('#loading-layer');

    if ($loadingLayer != undefined) {
        if (!retorno) {
            $loadingLayer.css({ zIndex: 9999 });
            $loadingLayer.animate({
                opacity: 1
            }, 150);
        } else {
            $loadingLayer.css({
                zIndex: -1,
                opacity: 0,
            });
        }
    }
}

function AlternarLoadingGif(tipo, retorno) {
    var $loadingGif = undefined;
    switch (tipo) {
        case 'global':
            $loadingGif = $('#loading-gif');
            break;
        case 'interno':
            $loadingGif = $('#contentTabs .tab-pane.active .loading-gif');
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

function AtenuarModalActivo(retorno) {
    if (retorno) {
        $ifrmActive.animate({
            opacity: 1
        }, 50, function () { })
        $mdlActive.find('.submit').prop('disabled', false);
    } else {
        $ifrmActive.animate({
            opacity: 0.5
        }, 50, function () { });
    }
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