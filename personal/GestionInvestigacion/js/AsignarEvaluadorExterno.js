
$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    if (rpta == true) {

        var dt = fnCreateDataTableBasic('tPostulacion', 1, 'asc', 100);
        var dtO = fnCreateDataTableBasic('tEvaluadores', 0, 'asc', 5);

        fnListarConcurso();

        cLineas();
        listarEvaluadoresExternos('C', $("#cboLinea").val());

        $("#divBtnEnviarEE").hide();
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $("#cboConcurso").change(function() {
        if ($(this).val() != '') {
            fnVerConcurso($(this).val());
            $("#DivConcurso").attr("style", "display:block")
            $("#divPostulacion").attr("style", "display:block")
            fnListarPostulacion($(this).val());
        } else {
            fnDestroyDataTableDetalle('tPostulacion');
            $('#tbPostulacion').html("");
            fnResetDataTableBasic('tPostulacion', 1, 'asc', 100);

            //            document.getElementById('tPostulacion').innerHTML = '';
            //            fnDestroyDataTableDetalle('tPostulacion');
            //            $('#tbPostulacion').html('');

            $("#DivConcurso").attr("style", "display:none")
            $("#divPostulacion").attr("style", "display:none")

        }
    })
    $("#cboLinea").change(function() {
        listarEvaluadoresExternos('C', $(this).val());
    })
});

function fnListarConcurso() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=dir]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lcp + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="dir" name="dir" value="%" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=dir]").remove();        
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Concurso.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data)
                var tb = '';
                tb += "<option value=''>--Seleccione--</option>";
                var i = 0;
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<option value="' + data[i].cod + '">' + data[i].titulo + '</option>';
                    }
                    $("#cboConcurso").html(tb)
                }
            },
            error: function(result) {
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}

function listarEvaluadoresExternos(tipo, parametro) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
        data: { "action": "lEvaluadoresExt", "tipo": tipo, "parametro": parametro },
        dataType: "json",
        success: function(data) {
            var tb = ''
            tb += "<option value=''>--Seleccione--</option>"
            if (data.length) {
                for (i = 0; i < data.length; i++) {
                    if (data[i].est_eve == 1) {
                        tb += "<option value='" + data[i].cod_eve + "'>" + data[i].nom_eve + "</option>"
                    }
                }
            }
            $("#cboEvaluador").html(tb)
        },
        error: function(result) {
            sOut = '';
            //console.log(result);
        }
    });
}


function fnVerConcurso(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        fnLimpiarConcurso();
        $("form#frmbuscar input[id=hdcod]").remove();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $('#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Concurso.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                $("#hdCON").val(data[0].cod);
                $("#txttitulo").val(data[0].titulo);
                $('#txtdescripcion').val(data[0].des);
                $("#txtfecini").val(data[0].fecini);
                $("#txtfecfin").val(data[0].fecfin);
                $("#txtfecfineva").val(data[0].fecfineva);
                $("#txtfecres").val(data[0].fecres);
                $("#cbotipo").val(data[0].tipo);
                if (data[0].rutabases != "") {
                    //                    $("#bases").html("<a href='" + data[0].rutabases + "' target='_blank' style='font-weight:bold'>Descargar Bases</a>")
                    $("#bases").html('<a onclick="fnDownload(\'' + data[0].rutabases + '\')" style="font-weight:bold">Descargar Bases</a>')
                } else {
                    $("#bases").html("");
                }
                $("#VerConcurso").attr("style", "display:block");
                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
                //console.log(result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}

function fnLimpiarConcurso() {
    $("#txttitulo").val('');
    $('#txtdescripcion').val('');
    $("#txtfecini").val('');
    $("#txtfecfin").val('');
    $("#cbotipo").val('');
    $("#bases").html("");

}

function fnEnviarEvaluadorExterno() {
    contadorTablaDetalle();
    var swVacio = 0;
    var swVacio1 = 0;
    var valorCelda = '';
    var valores = '';
    var primero = 0;

    $('#hdCorreo').val("");
    //alert("Filas: " + $('#hdRowsDT').val());
    for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
        if ($('#chkPos\\[' + i + '\\]').is(':checked')) {
            swVacio++;
            if (primero == 1) {
                valorCelda = valorCelda + ",";
            }
            valorCelda = valorCelda + $('#chkPos\\[' + i + '\\]').val();
            primero = 1;
        }
    }
    $('#hdCorreo').val(valorCelda);

    document.getElementById("divMensaje").innerHTML = "Se enviarán la(s) postulacion(es) para su revisión ";  // $('#cboConcurso').val() + " - " + valorCelda + "";
    //alert($('#cboConcurso').val() + "-" + $('#hdCorreo').val());
    $("#btnAceptarEnvioEmail").attr("onclick", "fnAceptarEnvioEmail()")
    $('div#mdMensaje').modal('show');
}

function fnAceptarEnvioEmail() {
    $("#divOcultarEnvio").attr("style", "display:none");
    $("div#divOcultarEnvio").modal('hide');
    $("#divProcesando").attr("style", "display:show");
    $("div#divProcesando").modal('show');
    //alert($('#cboConcurso').val() + "-" + $('#hdCorreo').val());
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
        data: { "action": "envioEmailEvaluadorExterno", "param1": $('#cboConcurso').val(), "param2": $('#hdCorreo').val() },
        dataType: "json",
        success: function(data) {
            if (data[0].Status == "success") {
                fnMensaje("success", data[0].Message);
                fnVerConcurso($('#cboConcurso').val());
                fnListarPostulacion($('#cboConcurso').val());
                //$('div#DivConcurso').modal('show');
                $("#DivConcurso").attr("style", "display:block");
                $('div#mdMensaje').modal('hide');
            } else {
                fnMensaje("error", data[0].Message);
                //$('div#DivConcurso').modal('show');
                $("#DivConcurso").attr("style", "display:block");
                $('div#mdMensaje').modal('hide');
            }
            $("#divOcultarEnvio").attr("style", "display:block");
            $("#divProcesando").attr("style", "display:none");
        },
        error: function(result) {
            //console.log(result); //--para errores                      
        }

    });
}

function fnEnviarNotificacionEvaluador() {
    contadorTablaDetalle();
    var swVacio = 0;
    var swVacio1 = 0;
    var valorCelda = '';
    var valores = '';
    var primero = 0;

    $('#hdCorreo').val("");
    //alert("Filas: " + $('#hdRowsDT').val());
    for (var i = 1; i <= $('#hdRowsDT').val(); i++) {
        if ($('#chkPos\\[' + i + '\\]').is(':checked')) {
            swVacio++;
            if (primero == 1) {
                valorCelda = valorCelda + ",";
            }
            valorCelda = valorCelda + $('#chkPos\\[' + i + '\\]').val();
            primero = 1;
        }
    }
    $('#hdCorreo').val(valorCelda);

    document.getElementById("divMensaje").innerHTML = "Se enviarán Notificaciones a Evaluadores Que no hayan colocado Nota y Rúbrica.";  // $('#cboConcurso').val() + " - " + valorCelda + "";
    $("#btnAceptarEnvioEmail").attr("onclick", "fnAceptarEnvioNotificacion()")
    //alert($('#cboConcurso').val() + "-" + $('#hdCorreo').val());
    $('div#mdMensaje').modal('show');
}


function fnAceptarEnvioNotificacion() {
    $("#divOcultarEnvio").attr("style", "display:none");
    $("div#divOcultarEnvio").modal('hide');
    $("#divProcesando").attr("style", "display:show");
    $("div#divProcesando").modal('show');
    //alert($('#cboConcurso').val() + "-" + $('#hdCorreo').val());
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
        data: { "action": "envioNotificacionEvaluadorExterno", "param1": $('#cboConcurso').val(), "param2": $('#hdCorreo').val() },
        dataType: "json",
        success: function(data) {
            if (data[0].Status == "success") {
                fnMensaje("success", data[0].Message);
                fnVerConcurso($('#cboConcurso').val());
                fnListarPostulacion($('#cboConcurso').val());
                //$('div#DivConcurso').modal('show');
                $("#DivConcurso").attr("style", "display:block");
                $('div#mdMensaje').modal('hide');
            } else {
                fnMensaje("error", data[0].Message);
                //$('div#DivConcurso').modal('show');
                $("#DivConcurso").attr("style", "display:block");
                $('div#mdMensaje').modal('hide');
            }
            $("#divOcultarEnvio").attr("style", "display:block");
            $("#divProcesando").attr("style", "display:none");
        },
        error: function(result) {
            //console.log(result); //--para errores                      
        }

    });
}


function contadorTablaDetalle() {
    var tableReg = document.getElementById('tPostulacion');
    var searchText = 'No se ha encontrado informacion';
    var cellsOfRow = "";
    var compareWith = "";
    var contawors = "";
    for (var i = 1; i < tableReg.rows.length; i++) {
        cellsOfRow = tableReg.rows[i].getElementsByTagName('td');
        for (var j = 0; j < cellsOfRow.length; j++) {
            compareWith = cellsOfRow[j].innerHTML;
            if (searchText.length == 0 || (compareWith.indexOf(searchText) > -1)) {
                contawors = 0;
                $("#hdRowsDT").val(contawors);
            } else {
                $("#hdRowsDT").val(i);
            }
        }
    }
}

function fnListarPostulacion(codigo_con) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lpe + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigo_con + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                var tb = '';
                var i = 0;
                var cant = 0;
                var filas = data.length;
                var mostrar = "";
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        if (data[i].orden == 1) {
                            mostrar = "disabled";
                        }
                    }
                    if (mostrar == "disabled") {
                        $("#divBtnEnviarEE").hide();
                    } else {
                        $("#divBtnEnviarEE").show();
                    }
                    //                    mostrar = "";
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text:center;"> <input type="checkbox" id="chkPos[' + (i + 1) + ']" name="chkPos[' + (i + 1) + ']" value="' + data[i].codSE + '" class="mark-complete" ' + mostrar + '/> </td>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td class="titulo">' + data[i].titulo + '</td>';
                        tb += '<td class="titulo">' + data[i].responsable + '</td>';
                        tb += '<td>' + data[i].des_etapa + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cantidad + '</td>';
                        tb += '<td style="text-align:center">' + data[i].email + '/' + data[i].cantidad + '</td>';
                        tb += '<td style="text-align:center">';
                        tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-warning" onclick="fnEvaluadores(\'' + data[i].cod + '\')" title="Ver Evaluadores" ' + mostrar + '><i class="ion-android-person-add"></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tPostulacion');
                $('#tbPostulacion').html(tb);
                fnResetDataTableBasic('tPostulacion', 0, 'asc', 100);
            },
            error: function(result) {
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}



function fnEvaluadores(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcodPos]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lev + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcodPos" name="hdcodPos" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                var tb = '';
                var i = 0;
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center" width="5%">' + (i + 1) + "" + '</td>';
                        tb += '<td width="35%" >' + data[i].nombre + '</td>';
                        tb += '<td width="40%" >' + data[i].dina + '</td>';
                        tb += '<td style="text-align:center" width="7%">';
                        tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-danger" onclick="fnQuitar(\'' + data[i].cod + '\')" title="Ver Evaluadores" ><i class="ion-android-delete"></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tEvaluadores');
                $('#tbEvaluadores').html(tb);
                fnResetDataTableBasic('tEvaluadores', 0, 'asc', 5);
                $("#cboEvaluador").val("");
                $("#cboLinea").val("");
                $("#mdEvaluadores").modal("show");
            },
            error: function(result) {
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}


function fnQuitar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.qev + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje);
                    fnEvaluadores($("#hdcodPos").val());
                    fnListarPostulacion($("#cboConcurso").val());
                    fnLoading(false);
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function(result) {
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}

function AsignarEvaluador() {
    if ($("#cboEvaluador").val() != "") {
        rpta = fnvalidaSession();
        if (rpta == true) {
            fnLoading(true);
            $("form#frmEvaluadores input[id=action]").remove();
            $("form#frmEvaluadores input[id=hdcod]").remove();
            $('form#frmEvaluadores').append('<input type="hidden" id="action" name="action" value="' + ope.aev + '" />');
            $('form#frmEvaluadores').append('<input type="hidden" id="hdcod" name="hdcod" value="' + $("#hdcodPos").val() + '" />');
            var form = $("#frmEvaluadores").serializeArray();
            $("form#frmEvaluadores input[id=action]").remove();
            $("form#frmEvaluadores input[id=hdcod]").remove();
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje);
                        fnEvaluadores($("#hdcodPos").val());
                        fnListarPostulacion($("#cboConcurso").val());
                        fnLoading(false);
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                },
                error: function(result) {
                    fnMensaje("warning", result)
                }
            });
            fnLoading(false);

        } else {
            window.location.href = rpta
        }
    } else {
        fnMensaje("warning", 'Seleccione un Evaluador Externo.');
    }
}


function cLineas() {
    var arr = fnLineas('%');
    var n = arr.length;
    var str = "";
    str += '<option value="">-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    str += '<option value="%" selected="selected">TODOS</option>';
    $('#cboLinea').html(str);
}

function fnLineas(cpf) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarLineas" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cpf" name="cpf" value="' + cpf + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            //console.log(data);
            arr = data;
        },
        error: function(result) {
            //console.log(result)
            arr = result;
        }
    });

    return arr;
}


function fnDownload(id_ar) {
    var flag = false;
    var form = new FormData();
    form.append("action", "Download");
    form.append("IdArchivo", id_ar);
    // alert();
    //            console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function(data) {
            console.log(data);
            flag = true;

            var file = 'data:application/octet-stream;base64,' + data[0].File;
            var link = document.createElement("a");
            link.download = data[0].Nombre;
            link.href = file;
            link.click();
        },
        error: function(result) {
            console.log(result);
            flag = false;
        }
    });
    return flag;
}



function downloadWithName(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
    // alert(link);
}