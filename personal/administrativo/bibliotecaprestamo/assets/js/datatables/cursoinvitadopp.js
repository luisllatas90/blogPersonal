//var aDtNiv = [];
$(document).ready(function() {
  
    var tb2 = "";
    var tb3 = "";
 
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lstCEXAM" },
        dataType: "json",
        async: false,
        success: function(data2) {
			console.log(data2);
            if (data2) {
                //aDtNiv = data;
                //console.log(data);
                for (var i = 0; i < data2.length; i++) {
                    tb2 += '<tr role="row" style="font-size:11px">';
                    tb2 += '<td>' + data2[i].curso + '</td>';
                    tb2 += '<td>' + data2[i].gr + '</td>';
                    tb2 += '<td>' + data2[i].est + '</td>';
                    tb2 += '<td>' + data2[i].ambiente + '</td>';
                    tb2 += '<td>' + data2[i].horario + '</td>';


                    tb2 += '</tr>';
                }
            }
            $('#tbCursos2').html(tb2);
        },
        error: function(result) {
            location.reload();
        }
    });
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lstEXANOD" },
        dataType: "json",
        async: false,
        success: function(data) {

            if (data) {
                //aDtNiv = data;
                //console.log(data);
                for (var i = 0; i < data.length; i++) {
                    tb3 += '<tr role="row" style="font-size:11px">';
                    tb3 += '<td>' + data[i].curso + '</td>';
                    tb3 += '<td>' + data[i].gr + '</td>';
                    //tb2 += '<td>' + data[i].est + '</td>';


                    tb3 += '</tr>';
                }
            }
            $('#tbCursos3').html(tb3);
        },
        error: function(result) {
            location.reload();
        }
    });

    var oTable = $('#tablacursoinvitado').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });
    var oTable2 = $('#tablacursoinvitado2').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });
    var oTable3 = $('#tablacursoinvitado3').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });

});

function fnListar() {
    var tb = "";
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lstCRECU" },
        dataType: "json",
        async: false,
        success: function(data) {
            console.log(data);
            if (data) {
                //aDtNiv = data;
                //console.log(data);
                for (var i = 0; i < data.length; i++) {
                    tb += '<tr role="row" style="font-size:11px">';
                    tb += '<td>' + data[i].gr + '</td>';
                    tb += '<td>' + data[i].curso + '</td>';
                    tb += '<td style="text-align:right;">' + data[i].ciclo + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].cred + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].ambiente + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].horario_rec + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td>';
                    if (data[i].horario) {

                        tb += '<a href="#" class="btn btn-primary" onclick="fnConfirmar(1,' + data[i].cdma + ',\'' + data[i].curso + '\')"><i class="ion-checkmark-round"></i></a>';
                        tb += '<a href="#" class="btn btn-red" onclick="fnConfirmar(0,' + data[i].cdma + ',\'' + data[i].curso + '\')"><i class="ion-close-round"></i></a>';

                    }
                    else {
                        tb += 'Sin horario registrado. Visitar Escuela';
                    }
                    tb += '</td>';
                    tb += '</tr>';
                }
            }
            $('#tbCursos').html(tb);
        },
        error: function(result) {
            location.reload();
        }
    });

}

function fnConfirmar(opc, cdma,cur) {
    if (opc == 1) {
        fnMensajeConfirmarEliminar('top', 'Estas Seguro que deseas aceptar la solicitud de recuperaci&oacute;n [' + cur + ']?', 'fnGuardar', opc, cdma);
    }
    else {
        fnMensajeConfirmarEliminar('top', 'Estas Seguro que deseas rechazar la solicitud de recuperaci&oacute;n [' + cur + ']?', 'fnGuardar', opc, cdma);
    }
}

function fnGuardar(opc, c) {
    $('.piluku-preloader').removeClass('hidden');


    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "regCurRec", "param3": c, "param2": opc },
        dataType: "json",
        success: function(data) {
           // console.log(data);
            $('.piluku-preloader').addClass('hidden');
            if (data[0].R == "OK") {
                fnMensaje("success", data[0].Mensaje);
               // fnMensaje("warning", data[0].aviso + 'email: ' + data[0].email);
            } else {
                fnMensaje("danger", data[0].Mensaje);
            }

            f_Menu("cursoinvitadopp.aspx");
            // console.log(data);
        },
        error: function(result) {
            console.log(result);
            // f_Menu("nivelacioncurso.aspx");
        }
    });

    
}
