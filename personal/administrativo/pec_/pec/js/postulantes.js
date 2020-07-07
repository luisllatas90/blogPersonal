var flagDocumentReady = false;
var $elemSubmited = undefined;
var $activeModal = undefined;

$(document).ready(function () {
    flagDocumentReady = true;
    InicializarControles();

    $("#txtFiltroPostulantes").on("keyup", function () {
        FiltrarGrillaPostulantes();
    });

    $('#mdlMensajes').modal({
        backdrop: 'static',
        show: false,
    });

    $('#mdlMensajes').on('hide.bs.modal', function () {
        if (callbackHideMdlMensajes != undefined) {
            callbackHideMdlMensajes();
        }
        callbackHideMdlMensajes = undefined;
    });

    $('body').on('formSubmited', function (e) {
        MostrarMensajePostback();
        $('#btnRegistrarPostulanteModal').prop('disabled', false);
    })

    $('body').on('formSubmiting', function (e) {
        $elemSubmited.animate({
            opacity: 0.5
        }, 50, function () { });
    })

    $('#mdlInscripcionPostulante').on('click', '#btnRegistrarPostulanteModal', function (e) {
        $(this).prop('disabled', true);
        $elemSubmited = $('#ifrmInscripcionPostulante');
        $elemSubmited.contents().find('#btnRegistrar').trigger('click');
        $elemSubmited.animate({
            opacity: 0.5
        }, 50, function () { });
    });
});

var callbackHideMdlMensajes = undefined;

function FiltrarGrillaPostulantes() {
    var value = $("#txtFiltroPostulantes").val().toLowerCase();
    $('#grwPostulantes').find("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

function InicializarControles() {
    if (flagDocumentReady) {
        $('#cmbCentroCosto').selectpicker({
            size: 10,
        });
    }
}

function CargandoPanelPostulantes() {
    $('#grwPostulantes').fadeOut(150);
    $('#loading-gif').fadeIn(150);
}

function PanelPostulantesCargado() {
    $('#grwPostulantes').hide();
    $('#loading-gif').fadeOut(150);
    $('#grwPostulantes').fadeIn(150);
    FiltrarGrillaPostulantes();
}

function InitModalMovimientos() {
    $('#mdlMovimientos').modal({
        backdrop: 'static',
        show: false,
    });
}

function InitModalInscripcionPostulante() {
    $('#mdlInscripcionPostulante').modal({
        backdrop: 'static',
        show: false,
    });
}

function InitModalMantenimientoPostulante() {
    $('#mdlMantenimientoPostulante').modal({
        backdrop: 'static',
        show: false,
    });
}

function CargarModalMovimientos() {
    $activeModal = $('#mdlMovimientos');
    $('#mdlMovimientos').modal('show');
}

function CargarModalInscripcionPostulante() {
    $('#ifrmInscripcionPostulante').iFrameResize();
    $activeModal = $('#mdlInscripcionPostulante');
    $('#mdlInscripcionPostulante').modal('show');
}

function CargarModalMantenimientoPostulante() {
    var $iframe = $('#ifrmMantenimientoPostulante');
    $iframe.iFrameResize();
    $elemSubmited = $iframe;
    $activeModal = $('#mdlMantenimientoPostulante');
    $('#mdlMantenimientoPostulante').modal('show');
}

function MostrarMensajePostback() {
    $elemSubmited.animate({
        opacity: 1
    }, 125, function () { });
    var $respuestaPostback = $elemSubmited.contents().find('#respuestaPostback');

    var rpta = $respuestaPostback.data('rpta');
    var msg = $respuestaPostback.data('msg');
    console.log(rpta);
    console.log(msg);

    if (rpta == 1) {
        $mdlMensajes.find('#mensajePostBack').html(msg);
        $activeModal.modal('hide');
        $mdlMensajes.modal('show');
        callbackHideMdlMensajes = function () {
            __doPostBack('btnRefrescarGrillaPostulantes', '');
        }
    }
}

