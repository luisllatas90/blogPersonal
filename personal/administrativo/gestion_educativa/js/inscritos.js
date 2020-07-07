var flagDocumentReady = false;
var $ifrmActive = undefined;
var $mdlActive = undefined;
var $mdlMensajes = undefined;
var $mdlMensajeServidor = undefined;

$(document).ready(function () {
    flagDocumentReady = true;
    InicializarControles();

    $('body').on('keypress', '#txtFiltroDNI', function (e) {
        if (e.keyCode == 13) {
            __doPostBack('btnListarPostulante', '');
            e.preventDefault();
        }
    });

    $('body').on('keyup', '#txtFiltroPostulantes', function (e) {
        FiltrarGrillaPostulantes();
    });

    $('body').on('keyup', '#txtFiltroIngresantes', function (e) {
        FiltrarGrillaIngresantes();
    });

    $mdlMensajes = $('#mdlMensajes');
    $mdlMensajes.modal({
        backdrop: 'static',
        show: false,
    });
    $mdlMensajes.on('hide.bs.modal', function () {
        if (callbackHideMdlMensajes != undefined) {
            callbackHideMdlMensajes();
        }
        callbackHideMdlMensajes = undefined;
    });

    $mdlMensajeServidor = $('#mdlMensajeServidor');
    $mdlMensajeServidor.modal({
        backdrop: 'static',
        show: false,
    });
    $mdlMensajeServidor.on('hide.bs.modal', function () {
        if (callbackMdlMensajeServer != undefined) {
            callbackMdlMensajeServer();
        }
        callbackMdlMensajeServer = undefined;
    });

    $('body').on('formSubmiting', function (e) {
        AtenuarModalActivo(false);
    });

    $('body').on('formSubmited', function (e) {
        OnPostBack();
        AtenuarModalActivo(true);
    });

    $('body').on('formSubmitedWithErrors', function (e) {
        AtenuarModalActivo(true);
    });

    $('body').on('formCanceled', function (e) {
        $mdlActive.modal('hide');
    });

    $('body').on('scrollTop', function (e) {
        $mdlActive.scrollTop(0);
    });

    $('body').on('click', '#btnExportarInteresados', function (e) {
        var path = $(this).data('path');
        window.open(path);
    });

    $('body').on('click', '#btnExportarIngresantes', function (e) {
        var path = $(this).data('path');
        window.open(path);
    });

    $('body').on('click', '[id*="btnImprimirFichaIngresante"]', function (e) {
        var btnId = $(this).attr('id');
        AtenuarBoton(btnId, false);
        AlternarLoadingGif('interno', false);

        var objData = {
            alu: $(this).data('alu'),
        }
        var params = $.param(objData);
        var url = 'frmFichaInscripcionPDF.aspx';
        $.fileDownload(url, {
            httpMethod: 'POST',
            data: params,
            successCallback: function (url) {
                AtenuarBoton(btnId, true);
                AlternarLoadingGif('interno', true);
            },
            failCallback: function (html, url) {
                AtenuarBoton(btnId, true);
                AlternarLoadingGif('interno', true);
            }
        });
    });

    $('body').on('click', '[id*="btnImprimirFichaPecIngresante"]', function (e) {
        var btnId = $(this).attr('id');
        AtenuarBoton(btnId, false);
        AlternarLoadingGif('interno', false);

        var objData = {
            alu: $(this).data('alu'),
        }
        var params = $.param(objData);
        var url = 'frmFichaInscripcionPecPDF.aspx';
        $.fileDownload(url, {
            httpMethod: 'POST',
            data: params,
            successCallback: function (url) {
                AtenuarBoton(btnId, true);
                AlternarLoadingGif('interno', true);
            },
            failCallback: function (html, url) {
                AtenuarBoton(btnId, true);
                AlternarLoadingGif('interno', true);
            }
        });
    });
});

var callbackHideMdlMensajes = undefined;
var callbackMdlMensajeServer = undefined;

function InicializarControles() {
    if (flagDocumentReady) {
        $('#cmbCentroCosto').selectpicker({
            size: 10,
        });

        $('#cmbCicloAcademico').selectpicker({
            size: 6,
        });

        $('#cmbModalidadIngreso').selectpicker({
            size: 6,
        });

        $('#cmbCarreraProfesional').selectpicker({
            size: 6,
        });
    }
}

function FiltrarGrillaPostulantes() {
    var value = $("#txtFiltroPostulantes").val().toLowerCase();
    $('#grwPostulantes').find("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

function LimpiarFiltroPostulantes() {
    $("#txtFiltroPostulantes").val('');
    FiltrarGrillaPostulantes();
}

function FiltrarGrillaIngresantes() {
    var value = $("#txtFiltroIngresantes").val().toLowerCase();
    $('#grwIngresantes').find("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

function LimpiarFiltroIngresantes() {
    $("#txtFiltroIngresantes").val('');
    FiltrarGrillaIngresantes();
}

function CargandoPanelPostulantes() {
    AlternarLoadingGif('interno', false);
    AlternarDataGridView('grwPostulantes', false);
}

function CargandoPanelIngresantes() {
    AlternarDataGridView('grwIngresantes', false);
    AlternarLoadingGif('interno', false);
}

function PanelPostulantesCargado() {
    var mostrar = $mdlMensajeServidor.find('#divMdlMenServParametros').data('mostrar');
    if (mostrar) {
        callbackMdlMensajeServer = function () {
            $('#btnListarPostulante').prop('disabled', false);
            AlternarLoadingGif('interno', true);
        }
        $mdlMensajeServidor.modal('show');
    } else {
        AlternarLoadingGif('interno', true);
        AlternarDataGridView('grwPostulantes', true);
        LimpiarFiltroPostulantes();
    }
}

function PanelIngresantesCargado() {
    var mostrar = $mdlMensajeServidor.find('#divMdlMenServParametros').data('mostrar');
    if (mostrar) {
        callbackMdlMensajeServer = function () {
            $('#btnListarIngresante').prop('disabled', false);
            AlternarLoadingGif('interno', true);
        }
        $mdlMensajeServidor.modal('show');
    } else {
        AlternarDataGridView('grwIngresantes', true);
        AlternarLoadingGif('interno', true);
        LimpiarFiltroIngresantes();
    }
}

function FormularioRegistroInscripcionCargado() {
    var mostrar = $mdlMensajeServidor.find('#divMdlMenServParametros').data('mostrar');
    if (mostrar === true) {
        callbackMdlMensajeServer = function () {
            $('#btnRegistrarPostulante').prop('disabled', false);
            AlternarLoadingGif('interno', true);
        }
        $mdlMensajeServidor.modal('show');
    } else {
        CargarModalInscripcionPostulante(controlId, 'btnRegistrar');
    }
}

// -------------------------------MODALES-------------------------------
function CargarModalMovimientos(btnCallerId) {
    $mdlActive = $('#mdlMovimientosPostulante');
    var modalProperties = {
        backdrop: 'static',
        show: false,
    };
    LoadModal(modalProperties, btnCallerId);
}

function CargarModalInscripcionPostulante(btnCallerId, btnIdForTriggerClick) {
    $mdlActive = $('#mdlInscripcionPostulante');
    var modalProperties = {
        backdrop: 'static',
        show: false,
    };
    LoadModal(modalProperties, btnCallerId, btnIdForTriggerClick);
}

function CargarModalMantenimientoPostulante(btnCallerId, btnIdForTriggerClick) {
    $mdlActive = $('#mdlMantenimientoPostulante');
    var modalProperties = {
        backdrop: 'static',
        show: false,
    };
    LoadModal(modalProperties, btnCallerId, btnIdForTriggerClick);
}

function OperacionFinanciarCargada(btnCallerId, btnIdForTriggerClick) {
    $mdlActive = $('#mdlFinanciamiento');
    var modalProperties = {
        backdrop: 'static',
        show: false,
    };

    var mostrar = $mdlMensajeServidor.find('#divMdlMenServParametros').data('mostrar');
    if (mostrar) {
        callbackMdlMensajeServer = function () {
            $('#' + btnCallerId).prop('disabled', false);
            AlternarLoadingGif('interno', true);
        }
        $mdlMensajeServidor.modal('show');
    } else {
        AlternarLoadingGif('interno', true);
        LoadModal(modalProperties, btnCallerId, btnIdForTriggerClick);
    }
}

function CargarModalGenerarConvenio(btnCallerId, btnIdForTriggerClick) {
    $mdlActive = $('#mdlGenerarConvenio');
    var modalProperties = {
        backdrop: 'static',
        show: false,
    };
    LoadModal(modalProperties, btnCallerId, btnIdForTriggerClick);
}

function LoadModal(modalProperties, btnIdParentCaller, btnIdForTriggerClick) {
    $mdlActive.modal(modalProperties);

    var $btnSubmit = $mdlActive.find('.submit');
    $ifrmActive = $mdlActive.find('iframe');

    var routine = function () {
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
// ---------------------------------------------------------------------

// -------------------------------GLOBALES-------------------------------
function MostrarMensajeClient(rpta, msg) {
    var $mensajePostBack = $mdlMensajes.find('#mensajePostBack');
    $mensajePostBack.removeClass(function (index, className) {
        return (className.match(/(^|\s)alert-\S+/g) || []).join(' ');
    });

    switch (rpta) {
        case -1:
            $mensajePostBack.addClass('alert-danger');
            break;
        case 0:
            $mensajePostBack.addClass('alert-warning');
            break;
        case 1:
            $mensajePostBack.addClass('alert-success');
            break;
    }
    $mensajePostBack.html(msg);
    $mdlMensajes.modal('show');
}

function MostrarMensajeServer() {
    var $mdlMenServParametros = $mdlMensajeServidor.find('#divMdlMenServParametros');
    var rpta = $mdlMenServParametros.data('rpta');

    var $mensajeServer = $mdlMensajeServidor.find('#mensajeServer');
    $mensajeServer.removeClass('alert alert-danger alert-warning alert-success');

    switch (rpta) {
        case -1:
            $mensajeServer.addClass('alert alert-danger');
            break;
        case 0:
            $mensajeServer.addClass('alert alert-warning');
            break;
        case 1:
            $mensajeServer.addClass('alert alert-success');
            break;
    }
    $mdlMensajeServidor.modal('show');
}

function OnPostBack() {
    $ifrmActive.animate({
        opacity: 1
    }, 125, function () { });
    var $respuestaPostback = $ifrmActive.contents().find('#respuestaPostback');

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');

    if (rpta == 1) {
        $mdlActive.modal('hide');
        MostrarMensajeClient(rpta, msg)

        var postbackListar = $mdlActive.data('postback-listar');
        if (postbackListar === true) {
            callbackHideMdlMensajes = function () {
                __doPostBack('btnListarPostulante', '');
            }
        }
    }
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
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

function AlternarDataGridView(gridViewId, retorno) {
    var $tbody = $('#' + gridViewId).find('tbody');
    if (!retorno) {
        $tbody.css({
            opacity: 0.5,
        });
    } else {
        $tbody.animate({
            opacity: 1,
        }, 50);
    }
}

function CargandoPostulantesPorPagina(controlId) {
    var $boton = $('#' + controlId);
    var $pager = $boton.closest('.pagination');
    $pager.find('.page-item').removeClass('active')
    $boton.parent().addClass('active');

    AtenuarBoton(controlId, false);
    AlternarLoadingGif('interno', false);
    AlternarDataGridView('grwPostulantes', false);
}

function PostulantesCargadosPorPagina(controlId) {
    AtenuarBoton(controlId, true);
    AlternarLoadingGif('interno', true);
    AlternarDataGridView('grwPostulantes', true);
    LimpiarFiltroPostulantes();
}

function FichaInscripcionCargada(btnCallerId) {
    AtenuarBoton(btnCallerId, true);
    AlternarLoadingGif('interno', true);
}

function VerificarRespuestaFichaInscripcion() {
    var $iframe = $('#ifrmFichaInscripcion');
    $iframe.on('load', function (e) {
        var $respuestaPostback = $(this).contents().find('#respuestaPostback');
        var rpta = $respuestaPostback.data('rpta');
        var msg = $respuestaPostback.data('msg');
        MostrarMensajeClient(rpta, msg);
    });
}

function ExportarGrilla(btnId, metodo) {
    AtenuarBoton(btnId, false);
    AlternarLoadingGif('interno', false);

    var getData = {
        mod: $('#urlMod').val(),
        id: $('#urlId').val(),
        ctf: $('#urlCtf').val(),
    }
    var getParams = $.param(getData);

    var postData = {};
    $('#frmPostulantes').serializeArray().map(function (x) { postData[x.name] = x.value; });
    postData = $.extend({}, postData, {
        method: metodo,
    });
    var postParams = $.param(postData);

    var url = 'frmPostulantes.aspx?' + getParams;
    $.fileDownload(url, {
        httpMethod: 'POST',
        data: postParams,
        successCallback: function (url) {
            AtenuarBoton(btnId, true);
            AlternarLoadingGif('interno', true);
        },
        failCallback: function (responseHtml, url, error) {
            AtenuarBoton(btnId, true);
            AlternarLoadingGif('interno', true);
        },
    });
}