$(document).ready(function () {
    $('#mdlImportar').modal({
        backdrop: 'static',
        show: false,
    });

    $('body').on('click', '#btnImportar', function (e) {  
        if ($('#cmbCicloAcademico').val() == '-1') {
            //alert("Seleccione el Semestre");
            ShowMessage('\u00A1 Seleccione el Semestre !','Warning'); 
            $('#cmbCicloAcademico').focus();
            return false;
        }
        $('#mdlImportar').modal('show');
        //var cbo = document.getElementById('<%=cmbCicloAcademico.ClientID%>');
        var cbotext = $('#cmbCicloAcademico option:selected').text();
        $('#txtHaciaCicloAcademico').val(cbotext);
    });
});

function ShowMessage(message, messagetype) {
    var cssclss;
    switch (messagetype) {
        case 'Success':
            cssclss = 'alert-success'
            break;
        case 'Error':
            cssclss = 'alert-danger'
            break;
        case 'Warning':
            cssclss = 'alert-warning'
            break;
        default:
            cssclss = 'alert-info'
    }
    $('#divMensaje').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert ' + cssclss + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
}