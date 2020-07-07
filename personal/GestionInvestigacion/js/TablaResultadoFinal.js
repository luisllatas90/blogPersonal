$(document).ready(function() {

    var oTable = $('#tPostulacion').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });

    var nCloneTh = document.createElement('th');
    var nCloneTd = document.createElement('td');
    nCloneTd.className = "details-control";

    $('#tPostulacion thead tr').each(function() {
        this.insertBefore(nCloneTh, this.childNodes[0]);
    });

    $('#tPostulacion tbody tr').each(function() {
        this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
    });

    $('#tPostulacion tbody td.details-control').on('click', function() {
        var nTr = $(this).parents('tr')[0];
        //alert(nTr);
        var cadenatr = nTr.id;

        var arregloTr = cadenatr.split(",");
        var cant_det = arregloTr[0];
        var c_det = arregloTr[1];

        //alert(cant_det + "-" + c_det);

        //alert(cant_det + "-" + c_det);

        if (oTable.fnIsOpen(nTr)) {
            $(nTr).removeClass('shown');
            $(nTr).css("font-weight", "");
            oTable.fnClose(nTr);
        }
        else {
            $(nTr).addClass('shown');
            $(nTr).css("font-weight", "Bold");

            var sOut = '';
            var nroDP = 0;
            var i = 0;
            var j = 0;
            var contador = 0;
            var colore = "";
            ////////////////////////////////

            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/operaciones.aspx",
                data: { "action": "lDetallePostulacionesFinal", "param1": cant_det },
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {

                    // data es un listado JSON
                    var mostrar = '';
                    var mostrarck = '';
                    var chk = '';
                    sOut = '<table border="0" style="width:100%;font-size:10px; id="tbDetallePost">';
                    if (data.length > 0) {

                        for (i = 0; i < data.length; i++) {
                            if (data[i].calif_eva == "-1") {
                                mostrar = 'disabled';
                            }
                        }

                        for (i = 0; i < data.length; i++) {
                            contador++;
                            sOut += '<tr id=' + contador + "," + cant_det + '>';

                            if (mostrar == "") {
                                if ($('#txtNotaGlobal\\[' + cant_det + '\\]').val() != "") {
                                    if ($('#txtNotaGlobal\\[' + cant_det + '\\]').val() != "0") {
                                        mostrar = 'disabled';
                                    } else {
                                        mostrar = '';
                                    }
                                } else {
                                    mostrar = '';
                                }
                            }

                            if (data[i].calif_eva == "-1") {
                                sOut += '<td style="text-align:center;margin:0 auto" width="5%"><input type="checkbox" id="chkOCDE' + c_det + '[' + contador + ']" name="chkOCDE' + c_det + '[' + contador + ']" onclick="AsginarNotaFinal(this);" value="' + cant_det + ',0,' + data.length + ',' + contador + ',' + c_det + '" ' + mostrar + ' ' + mostrarck + '></td>';
                                sOut += '<td width="15%">' + data[i].nombre_eve + '</td>';
                                sOut += '<td style="text-align:center" width="5%">Falta Calificar</td>';
                            } else {
                                if (data[i].calif_eva <= 70) {
                                    colore = '#FF0006';
                                } else {
                                    colore = '#337ab7';
                                }
                                sOut += '<td style="text-align:center;margin:0 auto" width="5%"><input type="checkbox"  id="chkOCDE' + c_det + '[' + contador + ']"  name="chkOCDE' + c_det + '[' + contador + ']" onclick="AsginarNotaFinal(this);" value="' + cant_det + ',' + data[i].calif_eva + ',' + data.length + ',' + contador + ',' + c_det + '" ' + mostrar + ' ' + mostrarck + '></td>';
                                sOut += '<td width="15%"><label style="font-weight: bold; color:' + colore + ';">' + data[i].nombre_eve + '</label></td>';
                                sOut += '<td style="text-align:center" width="5%"><label style="font-weight: bold; color:' + colore + ';">' + data[i].calif_eva + '</label></td>';
                            }
                            if (data[i].rubri_eva == "../GestionInvestigacion") {
                                sOut += '<td style="text-align:center" width="5%">Sin Rúbrica</td>';
                            } else {
                                //sOut += '<td style="text-align:center" width="5%"><a href="' + data[i].rubri_eva + '" target="_blank" style="font-weight: bold; font-style: oblique; color:' + colore + ';">Rúbrica</a></td>';
                                sOut += '<td style="text-align:center" width="5%"><a onclick="fnDownload(\'' + data[i].rubri_eva + '\')" target="_blank" style="font-weight: bold; font-style: oblique; color:' + colore + ';">Rúbrica</a></td>';
                            }
                            sOut += '</tr>';
                        }

                        sOut += '</table>';
                        oTable.fnOpen(nTr, sOut, 'details');
                    }

                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });

            /////////////////////

        }
    });
    $('#tPostulacion  thead  tr th:eq(0)').html('DETALLE');

});





