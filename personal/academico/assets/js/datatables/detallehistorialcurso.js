$(document).ready(function() {
    $("a[name='verDet']").on("click", function() {

        var codigoDma = $(this).data("id");
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
    });
});