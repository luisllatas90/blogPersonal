$(document).ready(function() {
    ope = fnOperacion(1);
    fnListarConcurso();

    $("#btnPostular").click(function() {
        fnLimpiarPostulacion();
        $("#mdRegistro").modal("show");
    });

    $("#cboConcurso").change(function() {
        fnListarPostulacion($('#cboConcurso').val());
    });

    $("#cboTipo").change(function() {
        if ($(this).val() == 'RUBRICA' && $("#hdcodPos").val() != "") {
            fnEvaluadores($("#hdcodPos").val())
        } else {
            $("#cboEvaluador").html("<option value=''>-- Seleccione --</option>")
        }
    });

    $("#hdcodPos").change(function() {
        if ($("#cboTipo").val() == 'RUBRICA' && $(this).val() != "") {
            fnEvaluadores($(this).val())
        } else {
            $("#cboEvaluador").html("<option value=''>-- Seleccione --</option>")
        }
    });


    $("#btnSubir").click(function() {
        if ($("#hdcodPos").val() != "") {
            if ($("#cboTipo").val() != "") {
                if ($("#file").val() != "") {
                    if ($("#cboTipo").val() == "RUBRICA" && $("#cboEvaluador").val() == "") {
                        fnMensaje("warning", "Seleccionar Evaluador para Subir Rubrica.");
                    } else {
                        cod = $("#hdcodPos").val();
                        tipo = $("#cboTipo").val();
                        evaluador = $("#cboEvaluador").val();
                        SubirArchivo(cod, tipo, evaluador);
                        fnLimpiarPostulacion();
                        $("#mdRegistro").modal("hide");
                    }
                } else {
                    fnMensaje("warning", "Seleccionar Archivo");
                }
            } else {
                fnMensaje("warning", "Seleccionar tipo de archivo a subir");
            }
        } else {
            fnMensaje("warning", "Seleccionar Postulación");
        }
    });
});


function fnLimpiarPostulacion() {
    $('#cboConcurso').val("");
    $('#hdcodPos').val("");
    $('#file').val("");
}


function SubirArchivo(cod, tipo, evaluador) {
    fnLoading(true)
    try {

        var data = new FormData();
        data.append("referencia", "1");
        data.append("action", ope.Up);
        data.append("codigo", cod);
        data.append("tipo", tipo);
        data.append("cod_evaluador", evaluador);

        var files = $("#file").get(0).files;
        if (files.length > 0) {
            data.append("ArchivoASubir", files[0]);
        }

        //console.log(data);
        if (files.length > 0) {
            // fnMensaje('primary', 'Subiendo Archivo');
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
                data: data,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                async: false,
                success: function(data) {
                    //flag = true;
                    console.log(data);
                    //console.log('ARCHIVO SUBIDO');
                    //console.log(data);
                    fnLoading(false);
                },
                error: function(result) {
                    //console.log('falseee');
                    //  flag = false;
                    console.log(result);
                    fnLoading(false);
                }
            });
        } else {
            fnLoading(false);
        }
        fnLoading(false);
    }
    catch (err) {
        return false;
        fnLoading(false);
    }
    fnLoading(false);
}


function fnListarConcurso() {
    fnLoading(true);
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.lst + '" /></form>');
    $('form#frmOpe').append('<input type="hidden" id="ctf" name="ctf" value="1" />');
    $('form#frmOpe').append('<input type="hidden" id="txtBusqueda" name="txtBusqueda" value="" />');
    $('form#frmOpe').append('<input type="hidden" id="cboEstado" name="cboEstado" value="T" />');
    $('form#frmOpe').append('<input type="hidden" id="ambito" name="ambito" value="0" />');
    $('form#frmOpe').append('<input type="hidden" id="dir" name="dir" value="%" />');
    
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Concurso.aspx",
        data: form,
        dataType: "json",
        cache: false,
        //                async: false,
        success: function(data) {
            var tb = '';
            var filas = data.length;
            tb += '<option value="">-- Seleccione --</option>';
            $("#cboConcurso").html('');
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<option value="' + data[i].cod + '">' + data[i].titulo + '</option>';
                }
            }
            $("#cboConcurso").html(tb);
            fnLoading(false);
        },
        error: function(result) {
            fnMensaje("warning", result)
            fnLoading(false);
        }
    });
}


function fnListarPostulacion(codigo_con) {
    if ($("#cboConcurso").val() != "") {
        fnLoading(true)
        $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.lpo + '" /></form>');
        $('form#frmOpe').append('<input type="hidden" id="ctf" name="ctf" value="1" />');
        $('form#frmOpe').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigo_con + '" />');
        var form = $("#frmOpe").serializeArray();
        $("#frmOpe").remove();
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
            data: form,
            dataType: "json",
            //cache: false,
            //async: false,
            success: function(data) {
                var tb = '';
                tb += '<option value="">-- Seleccione --</option>';
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<option value="' + data[i].cod + '">' + data[i].titulo + '</option>';
                    }
                }
                $("#hdcodPos").html(tb)
                fnLoading(false);
            },
            error: function(result) {
                fnMensaje("warning", result)
                fnLoading(false);
            }
        });
    }
}


function fnEvaluadores(cod) {
    fnLoading(true)
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.lev + '" /></form>');
    $('form#frmOpe').append('<input type="hidden" id="hdcodPos" name="hdcodPos" value="' + cod + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Postulacion.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            var tb = '';
            var filas = data.length;
            tb += '<option value="">-- Seleccione --</option>';
            if (filas > 0) {
                for (i = 0; i < filas; i++) {
                    tb += '<option value="' + data[i].cod + '">' + data[i].nombre + '</option>';
                }
            }
            $("#cboEvaluador").html(tb)
            fnLoading(false);
        },
        error: function(result) {
            fnMensaje("warning", result)
            fnLoading(false);
        }
    });

}