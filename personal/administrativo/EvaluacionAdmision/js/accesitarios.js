
$(document).ready(function () {
    $('#mdlNotificar').modal({
        backdrop: 'static',
        show: false
    });

    $('#mdlGenerarCargoMatricula').modal({
        backdrop: 'static',
        show: false
    });

    $('body').on('click', 'button[id$="btnNotificar"]', function (e) {  
        $('#mdlNotificar').modal('show');
    });

    $('body').on('click', 'button[id$="btnGenerarCargoMatricula"]', function (e) {  
        $('#mdlGenerarCargoMatricula').modal('show');
    });
});

function openModal(modal) {
    switch (modal){
        case "notificar":
            $('#mdlNotificar').modal('show');
            break;
        case "generar":
            $('#mdlGenerarCargoMatricula').modal('show');
            break;
     }
}