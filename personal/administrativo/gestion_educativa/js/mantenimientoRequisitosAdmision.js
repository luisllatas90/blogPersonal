var flagDocumentReady = false;
var callbackHideMsgServidor;
var $mdlMensajesServidor;
var $mdlConfirm;

var $tabLista, $tabMantenimiento;

$(document).ready(function () {
    initCombos();

    $tabLista = $('#lista-tab');
    $tabMantenimiento = $('#mantenimiento-tab');

    $mdlConfirm = $('#mdlConfirm');
    $mdlConfirm.modal({
        show: false
    });

    $('body').on('click', '#btnCancelar', function (e) {
        $tabLista.removeClass('disabled');
        $tabLista.tab('show');
        $tabMantenimiento.addClass('disabled');
    });

    $('#btnConfirm').on('click', function (e) {
        var action = $(this).data('action');
        eval(action);
        $mdlConfirm.modal('hide');
    });

    $('body').on('keyup', '#txtFiltroNombre', function (e) {
        filterGridRequisitos();
    });
});

function filterGridRequisitos() {
    var value = $("#txtFiltroNombre").val().toLowerCase();
    $('#grwRequisitosAdmision').find("tbody tr").filter(function () {
        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
    });
}

function reformatOnClick() {
    $('#grwRequisitosAdmision button[id*="btnEliminar"]').each(function (i, elem) {
        var message = '<b>¿Realmente desea realizar esta operación?</b><br>Tenga en cuenta que al eliminar este requisito <u><i>se elminará para todas las modalidades en las que esté seleccionado</i></u>';
        var type = 'danger';
        var action = $(elem).attr('onclick');
        $(this).attr('onclick', 'confirmAction("' + message + '", "' + type + '", "' + action + '")');
    });
}

function clearFilter() { 
    $('#txtFiltroNombre').val('');
}

function confirmAction(message, type, action) {
    var $messageConfirm = $mdlConfirm.find('#messageConfirm');
    $messageConfirm.html(message);
    $messageConfirm.attr('class', 'alert alert-' + type);
    $mdlConfirm.find('#btnConfirm').data('action', action);
    $mdlConfirm.modal('show');
}

function toggleTab(onForm) {
    if (onForm) {
        $tabMantenimiento.removeClass('disabled');
        $tabMantenimiento.tab('show');
        $tabLista.addClass('disabled');
    } else {
        $tabLista.removeClass('disabled');
        $tabLista.tab('show');
        $tabMantenimiento.addClass('disabled');
    }
}

function initCombos() {
    initCombo('cbmFiltroModalidad');
}

function initCombo(id) {
    var options = {
        noneSelectedText: '-- Seleccione --',
    };

    $('#' + id).selectpicker(options);
}

function stateButton(id) {
    var $button = $('#' + id);
    var disabled = $button.prop('disabled');
    $button.prop('disabled', !disabled);

    if (disabled) {
        $button.removeClass('disabled');
    } else {
        $button.addClass('disabled');
    }
}

function checkResponseForm() {
    var rpta = $('#hddRpta').val().trim();

    if (rpta == '1') {
        toggleTab(false)
    }
}

function checkResponsePostback() {
    var rpta = $('#hddRpta').val().trim();
    var msg = $('#hddMsg').val().trim();

    if (rpta != '') {
        showMessage(rpta, msg);
    }
}

function showMessage(rpta, msg) {
    switch (rpta) {
        case '0':
            toastr.warning(msg)
            break;
        case '1':
            toastr.success(msg)
            break;
        case '-1':
            toastr.error(msg)
            break;
    }
}

function stateLoadingGif(type, state) {
    var $loadingGif = undefined;
    switch (type) {
        case 'lista':
            $loadingGif = $('#loading-gif');
            break;
        case 'mantenimiento':
            $loadingGif = $('#loading-gif-mantenimiento');
            break;
    }

    if ($loadingGif != undefined) {
        if (!state) {
            $loadingGif.fadeIn(150);
        } else {
            $loadingGif.fadeOut(150);
        }
    }
}