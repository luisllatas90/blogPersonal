$(document).ready(function() {
    // $('#divInfoDetails').hide();
    // var maxCred, montoCredPen, desaprobado1v, desaprobado2v;

    var oTable = $('#tablagruposhorariotodos').DataTable({
        "bPaginate": false,
        "bFilter": true,
        "bLengthChange": false,
        "bInfo": true,
        "aaSorting": [[2, 'asc']],
        "aoColumnDefs": [{
            "bSortable": false,
            "aTargets": [0, 1, 2]
}]
        });

        var nCloneTh = document.createElement('th');
        var nCloneTd = document.createElement('td');
        nCloneTd.className = "details-control";

        $('#tablagruposhorariotodos thead tr').each(function() {
            this.insertBefore(nCloneTh, this.childNodes[0]);
        });

        $('#tablagruposhorariotodos tbody tr').each(function() {
            this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
        });

        $('#tablagruposhorariotodos tbody td.details-control').on('click', function() {

            var nTr = $(this).parents('tr')[0];
            var cur = nTr.id;
            var selCurso = $('#selCurso' + cur).val();


            if (oTable.fnIsOpen(nTr)) {
                $(nTr).removeClass('shown');
                $(nTr).css("font-weight", "");
                oTable.fnClose(nTr);
            }
            else {

                $(nTr).addClass('shown');
                $(nTr).css("font-weight", "Bold");
                //console.log(cur);
                var sOut = '';
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "detallegruposhorariotodos.aspx",
                    data: { "cur": cur },
                    dataType: "json",
                    success: function(data) {

                        sOut = '<table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                        var c;
                        if (data.length > 0) {

                            for (var i = 0; i < data.length - 1; i++) {
                                if (i < (data.length - 1)) {
                                    if (data[i].codigo_cup != data[i + 1].codigo_cup) {
                                        sOut += '<tr>';
                                        sOut += '<td>[' + data[i].grupohor_cup + ']</td>';

                                        if (data[i].docente) {
                                            sOut += '<td>[' + data[i].docente + ']</td>';
                                        }
                                        else {
                                            sOut += '<td>[No hay información para mostrar]</td>';
                                        }


                                        // console.log(Boolean(data[i].estado_cup));
                                        c = 0;
                                        sOut += '<td><table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                                        jQuery.each(data, function(j, hor) {
                                            if (hor.codigo_cup == data[i].codigo_cup) {
                                                sOut += '<tr>';
                                                sOut += '<td align="left" width="40%"><input type="hidden" id="txtdia' + hor.codigo_cup + '[' + c + ']" name="txtdia' + hor.codigo_cup + '[' + c + ']" value="' + hor.dia_Lho + '"/><input type="hidden" id="txtfecIni' + hor.codigo_cup + '[' + c + ']" name="txtfecIni' + hor.codigo_cup + '[' + c + ']" value="' + hor.nombre_Hor + '"/><input type="hidden" id="txtfecFin' + hor.codigo_cup + '[' + c + ']" name="txtfecFin' + hor.codigo_cup + '[' + c + ']" value="' + hor.horaFin_Lho + '"/>' + hor.dia_Lho + ' ' + hor.nombre_Hor + ' - ' + hor.horaFin_Lho + '</td>';
                                                sOut += '<td align="left" width="60%">' + hor.ambiente + '</td>';
                                                // sOut += '<td align="left" width="45%">' + hor.docente + '</td>';
                                                sOut += '</tr>';
                                                // console.log(hor.ambiente);
                                                c++;
                                            }
                                        });
                                        sOut += '</table><input type="hidden" id="txthornum' + data[i].codigo_cup + '" value="' + c + '"/></td>';

                                        if (data[i].estado_cup)
                                            sOut += '<td align="center"><b>' + (parseInt(data[i].vacantes_cup) - parseInt(data[i].nroMatriculados)) + '</b><br>Vacante</td>';
                                        else
                                            sOut += '<td>[Cerrado]<i class="ion-alert" title="Grupo Cerrado"></td>';
                                        sOut += '<input type="hidden" name="txtst' + data[i].codigo_cur + '" value="' + data[i].estado_cup + '"/></tr>';
                                    }
                                }

                            }

                            sOut += '<tr>';
                            sOut += '<td>[' + data[data.length - 1].grupohor_cup + ']</td>';

                            if (data[data.length - 1].docente) {
                                sOut += '<td>[' + data[data.length - 1].docente + ']</td>';
                            }
                            else {
                                sOut += '<td>[No hay información para mostrar]</td>';
                            }

                            sOut += '<td><table cellpadding="1" cellspacing="1" border="0" style="width:100%;font-size:11px;">';
                            c = 0;
                            jQuery.each(data, function(j, hor) {
                                if (hor.codigo_cup == data[data.length - 1].codigo_cup) {
                                    sOut += '<tr>';
                                    sOut += '<td align="left" width="40%"><input type="hidden" id="txtdia' + hor.codigo_cup + '[' + c + ']" name="txtdia' + hor.codigo_cup + '[' + c + ']"value="' + hor.dia_Lho + '"/><input type="hidden" id="txtfecIni' + hor.codigo_cup + '[' + c + ']" name="txtfecIni' + hor.codigo_cup + '[' + c + ']" value="' + hor.nombre_Hor + '"/><input type="hidden" id="txtfecFin' + hor.codigo_cup + '[' + c + ']" name="txtfecFin' + hor.codigo_cup + '[' + c + ']" value="' + hor.horaFin_Lho + '"/>' + hor.dia_Lho + ' ' + hor.nombre_Hor + ' - ' + hor.horaFin_Lho + '</td>';
                                    sOut += '<td align="left" width="60%">' + hor.ambiente + '</td>';
                                    // sOut += '<td align="left" width="45%">' + hor.docente + '</td>';
                                    sOut += '</tr>';
                                    // console.log(hor.ambiente);
                                    c++;
                                }
                            });
                            sOut += '</table><input type="hidden" id="txthornum' + data[data.length - 1].codigo_cup + '" value="' + c + '"/></td>';

                            if (data[data.length - 1].estado_cup)

                                sOut += '<td align="center"><b>' + (parseInt(data[data.length - 1].vacantes_cup) - parseInt(data[data.length - 1].nroMatriculados)) + '</b><br>Vacante</td>';
                            else
                                sOut += '<td>[Cerrado]<i class="ion-alert" title="Grupo Cerrado"></i></td>';

                            sOut += '<input type="hidden" name="txtst' + data[data.length - 1].codigo_cur + '" value="' + data[data.length - 1].estado_cup + '"/></tr>';
                        }


                        sOut += '</table>';

                        oTable.fnOpen(nTr, sOut, 'details' + cur);
                        $('.details' + cur).val()
                    },
                    error: function(result) {
                        //console.log(result);
                        sOut = '...';
                        oTable.fnOpen(nTr, sOut, 'details');
                        f_Menu('gruposhorario.aspx');
                    }
                });
            }
        });

    });


    