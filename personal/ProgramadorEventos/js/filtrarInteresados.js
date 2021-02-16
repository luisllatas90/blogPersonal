var flagDocumentReady = false;

$(document).ready(function () {
    flagDocumentReady = true;

    InicializarControles();

    $('body').on('click', '.btn-limpiar', function (e) {
        var id = e.target.id.replace('btnLimpiar', 'ddl');
        LimpiarDropdown(id);
    });

    $('#cboFiltro').on('change', function (e) {  
        seleccionarTipo($('#cboFiltro').val());
    });
    seleccionarTipo($('#cboFiltro').val());

    $('body').on('change', '#txtFechaDesde', function (e) {  
        __doPostBack('txtFechaDesde', '');
    });

    $('body').on('change', '#txtFechaHasta', function (e) {  
        __doPostBack('txtFechaHasta', '');
    });
});

function InicializarControles() {
    if (flagDocumentReady) {
        InitDdlEvento();
        InitDdlInstitucionEducativa();
        InitDdlCarreraProfesional();
        InitDdlCentroCosto();
        IniteDdlGrados();
        InitFechas();
    }
}

function InitFechas() {  
    $('#txtFechaDesde').datepicker({
        language: 'es',
    });

    $('#txtFechaHasta').datepicker({
        language: 'es',
    });
}

function InitDdlEvento() {
    $('#ddlEvento').selectpicker({
        size: 6,
        noneSelectedText: '--TODOS--',
        liveSearch: true
    });
    if ($('#ddlEvento').find('option').length > 0) {
        $('#ddlEvento').selectpicker("refresh");
    }
}

function InitDdlInstitucionEducativa() {
    $('#ddlInstitucionEducativa').selectpicker({
        size: 6,
        noneSelectedText: '--TODOS--',
        liveSearch: true
    });
    if ($('#ddlInstitucionEducativa').find('option').length > 0) {
        $('#ddlInstitucionEducativa').selectpicker("refresh");
    }
}

function InitDdlCarreraProfesional() {
    $('#ddlCarreraProfesional').selectpicker({
        size: 6,
        noneSelectedText: '--TODOS--',
        liveSearch: true
    });
    if ($('#ddlCarreraProfesional').find('option').length > 0) {
        $('#ddlCarreraProfesional').selectpicker("refresh");
    }
}

function InitDdlCentroCosto() {
    $('#ddlCentroCosto').selectpicker({
        size: 6,
        noneSelectedText: '--TODOS--',
        liveSearch: true,
        width: '350px'
    });
    if ($('#ddlCentroCosto').find('option').length > 0) {
        $('#ddlCentroCosto').selectpicker("refresh");
    }
}

function IniteDdlGrados() {
    $('#ddlGrados').selectpicker({
        noneSelectedText: '--TODOS--'
    });
    if ($('#ddlGrados').find('option').length > 0) {
        $('#ddlGrados').selectpicker("refresh");
    }
}

function seleccionarTipo(opt) {
    if (opt == "A") {
        $("#divCentroCosto").removeClass("d-none");

        $("#ddlGrados").val("");
        $("#divGrados").addClass("d-none");
    } else {
        $("#divGrados").removeClass("d-none");

        $("#ddlCentroCosto").val("");
        $("#divCentroCosto").addClass("d-none");
    }

    InitDdlCentroCosto();
    IniteDdlGrados();

    __doPostBack('ddlCentroCosto', '');
    __doPostBack('ddlGrados', '');
}

function AtenuarBoton(botonId, retorno) {
    var $boton = $('#' + botonId);
    $boton.prop('disabled', !retorno);
}

function LimpiarDropdown(id) {
    $('#' + id).val('');
    $('#' + id).selectpicker('refresh');
    __doPostBack(id, '');
}

function FormatRbtPreferente() {  
    $('input[name="rbtPreferente"]').addClass('custom-control-input');
    $('input[name="rbtPreferente"] + label').addClass('custom-control-label');
}

function loading(start) {  
    if (start) {
        $('.container-filtros').addClass('disabled-div');
        $('#totalInteresados').animate({
            opacity: 0.4
        }, 250);
        $('.loading-gif').animate({
            opacity: 1
        }, 250)
    } else {
        $('.container-filtros').removeClass('disabled-div');
        $('#totalInteresados').animate({
            opacity: 1
        }, 100);
        $('.loading-gif').animate({
            opacity: 0
        }, 100)
    }
}