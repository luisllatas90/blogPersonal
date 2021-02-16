var flagEditForm = false;

function inputEventHandler(e){
    if(e.target.matches(".radio-prv")){
       document.getElementById("hfcodigoprv").value = e.target.dataset.codigoPrv;
       document.getElementById("hfidentificadorprv").value = e.target.dataset.idePrv;
       document.getElementById("hfnivel").value = e.target.dataset.ncpPrv;
       document.getElementById("txtFiltrPregunta").value = e.target.dataset.codigoPrv;
       flagEditForm = true;
    }
}

$(document).ready(function () {
    $smartWizard = $('#smartwizard');

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
            showPreviousButton: true,
            showNextButton: true,
            toolbarExtraButtons: [
                $('<button id="btnFakeSubmit" type="button" ' + (flagEditForm ? '' : 'disabled') + '></button>').text('Finalizar')
                    .addClass('btn btn-primary')
            ]
        },
    });

    $('#mdlPreguntas').modal({
        backdrop: 'static',
        show: false
    });
    
    $('#mdlVerPregunta').modal({
        backdrop: 'static',
        show: false
    });

    $('body').on('click', 'button[id$="btnSeleccionar"]', function (e) {  
        $('#mdlPreguntas').modal('show');
    });
    
    $('#btnAltGuardar').click(function() {
        if(flagEditForm != true){
            alert("¡ Seleccione una pregunta para continuar !");
            return false;
        }
    });
//    
//    $('body').on('change', '#rbtTipoPregunta1', function (e) {  
//        __doPostBack('rbtTipoPregunta1','');
//        document.getElementById("divPreguntas").addEventListener("input", inputEventHandler);
//    });
//    
//    $('body').on('change', '#rbtTipoPregunta2', function (e) {  
//        __doPostBack('rbtTipoPregunta2','');
//        document.getElementById("divPreguntas").addEventListener("input", inputEventHandler);
//    });
//    
//   $('body').on('click', 'a[id$="btnSeleccionar"]', function (e) {  
//       $('#mdlPreguntas').modal('show');
//       document.getElementById("divPreguntas").addEventListener("input", inputEventHandler);
//   });
    
});

function openModal(modal) {
    switch (modal){
        case "selecccion":
            $('#mdlPreguntas').modal('show');
            document.getElementById("divPreguntas").addEventListener("input", inputEventHandler);
            break;
        case "pregunta":
            $('#mdlVerPregunta').modal('show');
            break;
    }
}


