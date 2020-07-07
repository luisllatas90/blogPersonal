$(document).ready(function () {
    
});

function SubmitPostBack() {
    debugger;
    parent.$('body').trigger('formSubmited', {mostrarRespuesta});
}