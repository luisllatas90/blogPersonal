//var objetivos = [];
//var codobj = "0";
$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    if (rpta == true) {
        var dt = fnCreateDataTableBasic('tPostulacion', 1, 'asc', 100);
        var dtO = fnCreateDataTableBasic('tEvaluadores', 0, 'asc', 5);
        fnListarConcurso();
        listarEvaluadoresExternos('C', $("#cboLinea").val());
    } else {
        window.location.href = rpta
    }
    fnLoading(false);
    /*$("#btnRegresar").click(function() {
    $("#VerConcurso").attr("style", "display:none");
    $("#ListaConcursos").attr("style", "display:block");
    $("#PanelBusqueda").attr("style", "display:block");
    })

    $("#btnObjetivos").click(function() {
    fnLoading(true);
    $("#mdObjetivos").modal("show");
    fnLoading(false);
    });*/
    $("#cboConcurso").change(function() {
        if ($(this).val() != '') {
            fnVerConcurso($(this).val());
            $("#DivConcurso").attr("style", "display:block")
            fnListarPostulacion($(this).val());
        } else {
            $("#DivConcurso").attr("style", "display:none")
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
                console.log(data)
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

function listarEvaluadoresExternos() {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
        data: { "action": "lEvaluadoresExt" },
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
                $("#txttitulo").val(data[0].titulo);
                $('#txtdescripcion').val(data[0].des);
                $("#txtfecini").val(data[0].fecini);
                $("#txtfecfin").val(data[0].fecfin);
                $("#cbotipo").val(data[0].tipo);
                if (data[0].rutabases != "") {
                    $("#bases").html("<a href='" + data[0].rutabases + "' target='_blank' style='font-weight:bold'>Descargar Bases</a>")
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
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td>' + data[i].titulo + '</td>';
                        tb += '<td>' + data[i].des_etapa + '</td>';
                        tb += '<td style="text-align:center">';
                        tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-warning" onclick="fnEvaluadores(\'' + data[i].cod + '\')" title="Ver Evaluadores" ><i class="ion-android-person-add"></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tPostulacion');
                $('#tbPostulacion').html(tb);
                fnResetDataTableBasic('tPostulacion', 1, 'asc', 100);
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
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td>' + data[i].nombre + '</td>';
                        tb += '<td>' + data[i].dina + '</td>';
                        tb += '<td style="text-align:center">';
                        tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-danger" onclick="fnQuitar(\'' + data[i].cod + '\')" title="Ver Evaluadores" ><i class="ion-android-delete"></i></button>';
                        tb += '</td>';
                        tb += '</tr>';
                    }
                }
                fnDestroyDataTableDetalle('tEvaluadores');
                $('#tbEvaluadores').html(tb);
                fnResetDataTableBasic('tEvaluadores', 0, 'asc', 5);
                $("#cboEvaluador").val("");
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
    if ($("#cboEvaluador").val != "") {
        rpta = fnvalidaSession()
        if (rpta == true) {
            fnLoading(true)
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
    fnMensaje("danger", 'Seleccione un Evaluador Externo.');
    }
}


