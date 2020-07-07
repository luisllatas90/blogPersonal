//var aDtNiv = [];
$(document).ready(function() {
    /*var oTable = $('#tbHistorial').DataTable({
    "bPaginate": false,
    "bFilter": false,
    "bLengthChange": false,
    "bInfo": false
    });*/

    $('#btnConsultar').click(fnCargaNotas);

    fnCargaNotas();
});

function fnDetalle(id) {
    var codigoDma = id;
        var tipoMatricula = null;
        var estadoAct = null;
        var sTabla = null;
        $(".modal-body #valor").val(codigoDma);
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "detallehistorialcurso.aspx",
            data: { "codigoDma": codigoDma },
            dataType: "json",
            success: function(data) {
                jQuery.each(data, function(i, val) {
                    $("#titulo").text("Detalles del Curso [" + val.nombre_Cur + " .::. Grupo Horario: " + val.grupoHor_Cup + "]")

                    sTabla += "<tr><td class='active' style ='line-height:1'>Estudiante</td>";
                    sTabla += "<td style ='line-height:1'>" + val.NombresApellidos + "</td>";
                    sTabla += "<td class='active' style ='line-height:1'>Código Universitario</td>";
                    sTabla += "<td colspan=3 style ='line-height:1'>" + val.codigoUniver_Alu + "</td></tr>";
                    sTabla += "<tr><td class='active' style ='line-height:1'>Carrera Profesional</td>"
                    sTabla += "<td colspan=5 style ='line-height:1'>" + val.nombre_Cpf + "</td></tr>"
                    sTabla += "<tr><td class='active' style ='line-height:1'>Fecha de Registro</td>"
                    sTabla += "<td style ='line-height:1'>" + val.fechaReg_Dma + "</td>"
                    sTabla += "<td class='active' style ='line-height:1'>Registrado por</td>"
                    sTabla += "<td colspan=3 style ='line-height:1'>" + val.OperadorReg_Dma + "</td></tr>"
                    sTabla += "<tr><td class='active' style ='line-height:1'>Tipo de Matrícula</td>"

                    switch (val.tipoMatricula_Dma) {
                        case "A":
                            tipoMatricula = "Agregado"
                            break;
                        case "N":
                            tipoMatricula = "Normal"
                            break;
                        case "U":
                            tipoMatricula = "Examen de ubicación"
                            break;
                        case "C":
                            tipoMatricula = "Convalidación"
                            break;
                        case "S":
                            tipoMatricula = "Examen de suficiencia"
                            break;
                    }
                    sTabla += "<td colspan=5 style ='line-height:1'>" + tipoMatricula + "</td></tr>"

                    sTabla += "<tr><td class='active' style ='line-height:1'>Estado Actual</td>"

                    if (val.estado_Dma == "R") {
                        estadoAct = "Retirado";
                    } else {
                        estadoAct = "Matriculado";
                    }
                    sTabla += "<td colspan=5 style ='line-height:1'>" + estadoAct + "</td></tr>"

                    if (val.tipoMatricula_Dma == "A") {
                        sTabla += "<tr><td class='active' style ='line-height:1'>Motivo de Agregado</td>"
                        sTabla += "<td style ='line-height:1'>" + val.agregado + "</td>"
                        sTabla += "<td class='active' style ='line-height:1'>Operador de Agregado</td>"
                        sTabla += "<td style ='line-height:1'>" + val.OperadorReg_Dma + "</td>"
                        sTabla += "<td class='active' style ='line-height:1'>Fecha de Agregado</td>"
                        sTabla += "<td style ='line-height:1'>" + val.fechaReg_Dma + "</td></tr>"
                        sTabla += "<tr><td class='active' style ='line-height:1'>Obs. Agregado</td>"
                        sTabla += "<td colspan=5 style ='line-height:1'>" + val.obsagregado_dma + "</td>>/tr>"
                    }

                    if (val.estado_Dma == "R") {
                        sTabla += "<tr><td class='active' style ='line-height:1'>Motivo de Retiro</td>"
                        sTabla += "<td style ='line-height:1'>" + val.retiro + "</td>"
                        sTabla += "<td class='active' style ='line-height:1'>Operador de Retiro</td>"
                        sTabla += "<td style ='line-height:1'>" + val.OperadorMod_Dma + "</td>"
                        sTabla += "<td class='active' style ='line-height:1'>Fecha de Retiro</td>"
                        sTabla += "<td style ='line-height:1'>" + val.fechamod_Dma + "</td></tr>"
                        sTabla += "<tr><td class='active' style ='line-height:1'>Obs. Retiro</td>"
                        sTabla += "<td colspan=5 style ='line-height:1'>" + val.obsretiro_dma + "</td>>/tr>"
                    }

                    $("#tablaDetalleCurso").html(sTabla)

                });
            },
            error: function(result) {
                $("#tablaDetalleCurso").html("< tr><td></td></tr>");
                f_Menu('historialacademico.aspx');
            }
        });

}
function fnCargaNotas() {
    var tb = "";
    var cpf = $('#cbocpf').val();

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        data: { "param0": "lsHis", "param2": cpf },
        dataType: "json",
        //async: false,
        success: function(data) {
            console.log(data)

            if (data) {
                var CAR = 0;
                var CAC = 0;
                var AAR = 0;
                var AAC = 0;
                var pps = "";

                var cnv = 0;
                var det = 0;
                // aDtNiv = data;
              
                for (var i = 0; i < data.length; i++) {

                    if (data[i].swprom) {
                        pps = data[i - 1].desc_cac;
                        pps = pps.substr(-1);
                        console.log(pps);
                        tb += '<tr role="row" style="font-size:12px;font-weight:bold">';
                        tb += '<td></td>';
                        tb += '<td></td>';
                        tb += '<td></td>';
                        tb += '<td style="text-align:right;">Total</td>';
                        tb += '<td style="text-align:right;">' + data[i].sumcred + '&nbsp;&nbsp;&nbsp;</td>';
                        if (pps == '0') {
                            tb += '<td style="text-align:right;" colspan="2">Promedio</td>';
                        }
                        else {
                            if (data[i].prom > 0) {
                                tb += '<td style="text-align:right;" colspan="2">Promedio Ponderado Semestral:::::::::</td>';
                            } else {
                                tb += '<td style="text-align:right;" colspan="2">&nbsp;</td>';
                            }

                        }
                        // tb += '<td style="text-align:right;"></td>';

                        if (cnv == det) {
                            tb += '<td style="text-align:right;">-&nbsp;&nbsp;&nbsp;</td>';
                        } else {

                            if (data[i].prom > 0)
                                tb += '<td style="text-align:right;">' + (data[i].prom).toFixed(2) + '&nbsp;&nbsp;&nbsp;</td>';
                            else
                                tb += '<td style="text-align:right;">&nbsp;&nbsp;&nbsp;</td>';

                        }



                        tb += '<td></td>';
                        tb += '<td style="text-align:center;"></td>';
                        tb += '<td></td>';
                        tb += '</tr>';
                        cnv = 0;
                        det=0
                    } else {
                        if (data[i].cnvtr) {
                            cnv++;
                        }
                        det++;
                        tb += '<tr role="row" style="font-size:11px; color:' + data[i].color + '">';
                        tb += '<td>' + data[i].ciclo + '</td>';
                        tb += '<td>' + data[i].desc_cac + '</td>';
                        tb += '<td>' + data[i].tcurdma + '</td>';
                        tb += '<td>' + data[i].curso + '</td>';
                        tb += '<td style="text-align:right;">' + data[i].cred + '&nbsp;&nbsp;&nbsp;</td>';
                        tb += '<td>' + data[i].grupo + '</td>';
                        tb += '<td style="text-align:right;">' + data[i].vcsdma + '&nbsp;&nbsp;&nbsp;</td>';
                        tb += '<td style="text-align:right;">' + data[i].nota + '&nbsp;&nbsp;&nbsp;</td>';
                        tb += '<td>' + data[i].condicion + '</td>';
                        tb += '<td style="text-align:center;"><a name="verDet"  href="#" class="active" data-toggle="modal" data-target="#largemodal" data-id="' + data[i].dma + '" onclick="fnDetalle(' + data[i].dma + ')";>Ver</a></td>';
                        tb += '<td>' + data[i].escuela + '</td>';
                        tb += '</tr>';
                        if (data[i].condicion == "A") {
                            if (data[i].tipo != "C") {
                                CAR += parseInt(data[i].cred);
                                AAR++;
                            } else {
                                CAC += parseInt(data[i].cred);
                                AAC++;
                            }
                        }
                    }


                }
            }
            $('#tbdHistorial').html(tb);
            $('#CAR').html(CAR);
            $('#CAC').html(CAC);
            $('#CARCAC').html(CAR + CAC);
            $('#AAR').html(AAR);
            $('#AAC').html(AAC);
            $('#AARAAC').html(AAR + AAC);



        },
        error: function(result) {
            console.log(result)
            //location.reload();
        }
    })


}
