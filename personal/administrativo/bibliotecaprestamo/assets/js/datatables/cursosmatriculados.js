$(document).ready(function() {
    // $('#divInfoDetails').hide();
    var oTable = $('#tablacursosmatriculados').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });

    var nCloneTh = document.createElement('th');
    var nCloneTd = document.createElement('td');
    nCloneTd.className = "details-control";

    $('#tablacursosmatriculados thead tr').each(function() {
        this.insertBefore(nCloneTh, this.childNodes[0]);
    });

    $('#tablacursosmatriculados tbody tr').each(function() {
        this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
    });

    $('#tablacursosmatriculados tbody td.details-control').on('click', function() {
        var nTr = $(this).parents('tr')[0];
        var cup = nTr.id;

        if (oTable.fnIsOpen(nTr)) {
            /* This row is already open - close it */
            //this.src = "../examples_support/details_open.png";
            $(nTr).removeClass('shown');
            $(nTr).css("font-weight", "");


            oTable.fnClose(nTr);
        }
        else {
            /* Open this row */
            //this.src = "../examples_support/details_close.png";
            $(nTr).addClass('shown');
            $(nTr).css("font-weight", "Bold");

            var sOut = '';
            // alert(_cup);
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "detallecursosmatriculados.aspx",
                data: { "cup": cup },
                dataType: "json",
                success: function(data) {
                    sOut = '<table cellpadding="5" cellspacing="2" border="0" style="width:80%;font-size:12px;">';
                    jQuery.each(data, function(i, val) {
                        var mod = $('#hdmod').val();
                        
                        var docente = '';
                        sOut += '<tr>';


                        if (mod == '2') {
                            sOut += '<td>' + val.dia_Lho + ' ' + val.nombre_Hor + '-' + val.horaFin_Lho + '</td>';
                        }
                        else {
                            sOut += '<td>' + val.dia_Lho2 + ' ' + val.nombre_Hor + '-' + val.horaFin_Lho + '</td>';
                        }


                        //sOut += '<td>' + val.docente + ' ' + '<br>Inicio: ' + val.fechainicio_cup.slice(0, 10) + ' Fecha Fin: ' + val.fechafin_cup.slice(0, 10) + '</td>';
                        sOut += '<td>' + val.ambiente + ' ' + '</td>';
                        sOut += '</tr>';
                    });
                    sOut += '</table>';
                    oTable.fnOpen(nTr, sOut, 'details');

                },
                error: function(result) {

                    //console.log(result);
                    sOut = '';
                    oTable.fnOpen(nTr, sOut, 'details');
                    location.reload();
                }
            });







        }
    });
    $('#tablacursosmatriculados  thead  tr th:eq(0)').html('Horario');
});

