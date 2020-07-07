var aDtNiv = [];
$(document).ready(function() {
    var tb = "";
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lsCMINV" },
        dataType: "json",
        async: false,
        success: function(data) {

        if (data) {
            aDtNiv = data;
           // console.log(data);
                for (var i = 0; i < data.length; i++) {
                    tb += '<tr role="row" style="font-size:11px">';
                    tb += '<td>' + data[i].gr + '</td>';
                    tb += '<td>' + data[i].curso + '</td>';
                    tb += '<td style="text-align:right;">' + data[i].ciclo + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].cred + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td>' + data[i].doc + '</td>';
                    tb += '<td style="text-align:right;">' + data[i].fecini + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].fecfin + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td style="text-align:right;">' + data[i].costo + '&nbsp;&nbsp;&nbsp;</td>';
                    tb += '<td>';
                    if (parseInt(data[i].candeu) >= 2)
                        tb += '<font style="color:red;">REGULARIZAR DEUDAS PENDIENTES</font>';
                    if (data[i].estado == "N")
                        tb += '<font style="color:red;">CURSO FINALIZADO</font>';
                    if (data[i].estado == "C")
                        tb += '<font style="color:red;">CURSO INICIADO</font>';                            
                    if (data[i].estado == "M") {
                        tb += '<a href="#" class="btn btn-primary" onclick="fnGuardar(1,' + data[i].cup + ')"><i class="ion-checkmark-round"></i></a>';
                        tb += '<a href="#" class="btn btn-red" onclick="fnGuardar(0,' + data[i].cup + ')"><i class="ion-close-round"></i></a>';
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


    var oTable = $('#tablacursosnivelacion').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });

});
function fnGuardar(opc, c) {
    $('.piluku-preloader').removeClass('hidden');
    var obj = {};
    var i = 0;
    for (var idx in aDtNiv) {
        key = aDtNiv[idx];
        if (key.cup == c)
            obj[i] = key;
        i++;
    }
    // console.log(aDtNiv);

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "regNivMat", "param1": obj, "param2": opc },
        dataType: "json",
        success: function(data) {
            $('.piluku-preloader').addClass('hidden');
            if (data[0].R == "OK") {
                fnMensaje("success", data[0].Mensaje);
                fnMensaje("warning", data[0].aviso + 'email: ' + data[0].email);
            } else {
                fnMensaje("danger", data[0].Mensaje);
            }

            f_Menu("nivelacioncurso.aspx");
           // console.log(data);
        },
        error: function(result) {
            f_Menu("nivelacioncurso.aspx");
        }
    });

    
}
