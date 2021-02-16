var flagDocumentReady = false;

$(document).ready(function () {
    flagDocumentReady = true;

    InicializarControles();

    $('.checkAll').on('click', function () {
        $(this).closest('table').find('tbody :checkbox')
            .prop('checked', this.checked)
            .closest('tr').toggleClass('selected', this.checked);
    });

    $('tbody :checkbox').on('click', function () {
        $(this).closest('tr').toggleClass('selected', this.checked); //Clase de selección en la fila

        $(this).closest('table').find('.checkAll').prop('checked', ($(this).closest('table').find('tbody :checkbox:checked').length == $(this).closest('table').find('tbody :checkbox').length)); //Poner la selección en .checkAll
    });

    $("#txtFiltro").on("keyup", function () {
        FiltrarGrid();
    });

    $('#ifrmFiltrarInteresados').iFrameResize({
        heightCalculationMethod: 'documentElementOffset',
        // minHeight: 230
    });

    $('body').on('click', '#fakeBtnRegistrar', function (e) {
        e.preventDefault();
        AtenuarBoton('fakeBtnRegistrar', false);

        var filtrosSel = $('#ifrmFiltrarInteresados').contents().find('#hddFiltrosSel').val();
        $('#hddFiltros').val(filtrosSel);
        __doPostBack('btnRegistrar', '');
    });
});

function InicializarControles() {
    if (flagDocumentReady) {
        $('#ddlCentroCosto').selectpicker({
            size: 10
        });
        $('#ddlCentroCosto').selectpicker("refresh");

        $('#ddlGrados').selectpicker({
            size: 10,
            noneSelectedText: '--Seleccione--'
        });
        $('#ddlGrados').selectpicker("refresh");
    }
}

function FiltrarGrid() {
    var value = $("#txtFiltro").val().toLowerCase();
    $('#grwInteresado').find("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

function seleccionarTipo(opt) {
    if (opt.value == "A") {
        $("#divCentroCosto").removeClass("d-none");

        $("#ddlGrados").val("");
        $("#divGrados").addClass("d-none");
    } else {
        $("#divGrados").removeClass("d-none");

        $("#ddlCentroCosto").val("");
        $("#divCentroCosto").addClass("d-none");
    }

    $('#ddlCentroCosto').selectpicker("refresh");
    $('#divGrados').selectpicker("refresh");
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function Buscando() {
    AtenuarBoton(controlId, false);
    $('#containerBusqueda').addClass('loading');
}

function BusquedaFinalizada() {
    AtenuarBoton(controlId, true);
    $('#containerBusqueda').removeClass('loading');
}

function EnvioFinalizado(controlId) {
    AtenuarBoton(controlId, true);
}