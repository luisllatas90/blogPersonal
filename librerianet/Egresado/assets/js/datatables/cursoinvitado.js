//var aDtNiv = [];
$(document).ready(function() {
var tb = "";
var tb2 = "";
var tb3 = "";
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lstCRECU" },
        dataType: "json",
        async: false,
        success: function(data) {

        if (data) {
            //aDtNiv = data;
            //console.log(data);
                for (var i = 0; i < data.length; i++) {
                    tb += '<tr role="row" style="font-size:11px">';
                    tb += '<td>' + data[i].gr + '</td>';
                    tb += '<td>' + data[i].curso + '</td>';
                    tb += '<td style="text-align:right;">' + data[i].ciclo + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].cred + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td>';

                    tb += '<a href="#" class="btn btn-primary" onclick="fnGuardar(1,' + data[i].cdma + ')"><i class="ion-checkmark-round"></i></a>';
                    tb += '<a href="#" class="btn btn-red" onclick="fnGuardar(0,' + data[i].cdma + ')"><i class="ion-close-round"></i></a>';
                    
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

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lstCEXAM" },
        dataType: "json",
        async: false,
        success: function(data) {

            if (data) {
                //aDtNiv = data;
                //console.log(data);
                for (var i = 0; i < data.length; i++) {
                    tb2 += '<tr role="row" style="font-size:11px">';
                    tb2 += '<td>' + data[i].curso + '</td>';
                    tb2 += '<td>' + data[i].gr + '</td>';
                    tb2 += '<td>' + data[i].est + '</td>';
                   

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

            f_Menu("cursoinvitado.aspx");
            // console.log(data);
        },
        error: function(result) {
            console.log(result);
            // f_Menu("nivelacioncurso.aspx");
        }
    });

    
}
