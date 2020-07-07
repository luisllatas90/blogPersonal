$(document).ready(function() {

    var oTable = $('#tbActivoFijo').DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": false
    });

    var nCloneTh = document.createElement('th');
    var nCloneTd = document.createElement('td');
    nCloneTd.className = "details-control";

    $('#tbActivoFijo thead tr').each(function() {
        this.insertBefore(nCloneTh, this.childNodes[0]);
    });

    $('#tbActivoFijo tbody tr').each(function() {
        this.insertBefore(nCloneTd.cloneNode(true), this.childNodes[0]);
    });

    $('#tbActivoFijo tbody td.details-control').on('click', function() {
        var nTr = $(this).parents('tr')[0];
        var cadenatr = nTr.id;

        var arregloTr = cadenatr.split(",");
        var cant_det = arregloTr[0];
        var c_det = arregloTr[1];
        var idEgreso = 0;
        idEgreso = arregloTr[2];
        //alert("ntr: "+nTr);

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

            //alert("cant_det: "+cant_det);
            //alert("c_det: " + c_det);
            egresos = $('#hdIEgreso').val().split(",");
            cantidades = $('#param3').val().split(",");
            //alert(aDataDP[c_det].d_art);
            //alert(aDataDP[c_det].c_art);
            //alert($('#txtNroPedido').val());
            var nrop = $('#txtNroPedido').val();

            var i = 0;
            var j = 0;
            var contador = 0;
            //var conteo = parseInt(cant_det) + parseInt(c_det)
            //alert(conteo);
            var swDF = 0;


            $("input#param0").val("LstDetalleRegistroAF");
            var form = $('#frmRegistroActivo').serialize();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "../../DataJson/activofijo/processactivofijo.aspx",
                data: { "param0": "LstDetalleRegistroAF", "param1": nrop, "param2": aDataDP[c_det].c_art, "hdIEgreso": egresos[c_det] },
                //data: { "param0": "LstDetalleRegistroAF", "param1": nrop, "param2": $('#param2').val(), "hdIEgreso": $('#hdIEgreso').val() },
                dataType: "json",
                success: function(data) {

                    aDataDR = data;

                    if (data.length > 0) { i = data.length; }
                    else { i = 0; }

                    if (cant_det > 1) {
                        swDF = 1;
                    }

                    $('#hdCClonar').val(data.length);

                    sOut = '<table border="0" style="width:100%;font-size:10px; id="tbDetalleAF">';
                    for (j = 0; j < cant_det; j++) {
                        contador++;
                        sOut += '<tr>';
                        if (j < i) {
                            sOut += '<td width="5%">' + aDataDR[j].d_nes + '</td>';
                            sOut += '<td width="70%">' + aDataDP[c_det].d_art;
                            sOut += '<input type="hidden" id="txtCSerie[' + contador + ']" name="txtCSerie[' + contador + ']" value="' + aDataDR[j].d_ser + '" />';
                            sOut += '<input type="hidden" id="txtCEgre[' + contador + ']" name="txtCEgre[' + contador + ']" value="' + aDataDR[j].c_egr + '" />';
                            sOut += '<input type="hidden" id="txtNroComp[' + contador + ']" name="txtNroComp[' + contador + ']" value="' + aDataDR[j].d_nro + '" /></td>';
                            sOut += '<td width="15%">' + aDataDR[j].d_ser + '</td>';
                            sOut += '<td width="5%">' + aDataDR[j].d_nro + '</td>';
                            sOut += '<td width="5%"><a href="#" class="btn btn-orange btn-xs" onclick="fnRegistrarSerie(' + j + ',' + c_det + ',' + 1 + ',\'' + aDataDR[j].d_ser + '\')" ><i class="ion-android-create"></i></a></td>';
                            
                        } else {
                            sOut += '<td width="5%">' + contador + '</td>';
                            sOut += '<td width="70%">' + aDataDP[c_det].d_art;
                            sOut += '<input type="hidden" id="txtCSerie[' + contador + ']" name="txtCSerie[' + contador + ']" value="-" />';
                            sOut += '<input type="hidden" id="txtCEgre[' + contador + ']" name="txtCEgre[' + contador + ']" value="-" />';
                            sOut += '<input type="hidden" id="txtNroComp[' + contador + ']" name="txtNroComp[' + contador + ']" value="-" /></td>';
                            sOut += '<td width="5%"> - </td>';
                            sOut += '<td width="5%"> - </td>';
                            sOut += '<td width="5%"><a href="#" class="btn btn-orange btn-xs" onclick="fnRegistrarSerie(' + j + ',' + c_det + ',' + 0 + ',' + 0 + ')" ><i class="ion-android-create"></i></a></td>';
                        }
                        if (j == 0) {
                            if (swDF == 1) {
                                

                                sOut += '<td width="5%"><a href="#" class="btn btn-gray btn-xs" onclick="fnComponentes(' + j + ',' + c_det + ',' + idEgreso + ')" ><i class="ion-android-done-all"></i></a></td>';
                                sOut += '<td width="5%"></td>';
                            }
                            else {
                                sOut += '<td width="5%"></td>';
                            }
                        } else {
                            sOut += '<td width="5%"></td>';
                            //sOut += '<td width="5%"><a href="#" class="btn btn-gray btn-xs" onclick="fnComponentes(' + j + ',' + c_det + ')" ><i class="ion-android-done-all"></i></a></td>';

                        }
                        sOut += '</tr>';
                    }

                    sOut += '</table>';
                    oTable.fnOpen(nTr, sOut, 'details');
                },
                error: function(result) {
                    console.log(result);
                    sOut = '';
                    oTable.fnOpen(nTr, sOut, 'details');
                    //location.reload();
                }
            });

        }
    });
    $('#tbActivoFijo  thead  tr th:eq(0)').html('DETALLE');

});





