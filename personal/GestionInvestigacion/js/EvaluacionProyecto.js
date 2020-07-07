var equipo = [];
var objetivos = [];
var codper = "";
var nombre = "";
var codobj = "0";
$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    if (rpta == true) {
        var dtP = fnCreateDataTableBasic('tProyectos', 0, 'asc', 15);
        var dtE = fnCreateDataTableBasic('tEquipo', 0, 'asc', 10);
        var dtO = fnCreateDataTableBasic('tObjetivos', 1, 'asc', 10);
        cPersonal(ObtenerValorGET("ctf"));
        cLineas();
        cAutor();
        fnAutoCPersonal();
        cFiltro();
        fnListarProyecto();
        fnListarOCDE(0, 'AR');
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $("#btnEquipo").click(function() {
        fnLoading(true);
        $("#txtPersonal").val("");
        $("#cboRol").val("");
        $("#mdEquipo").modal("show");
        fnLoading(false);
    });

    $("#btnAgregarEquipo").click(function() {
        fnLoading(true);
        fnAgregarIntegrante();
        codper = "";
        nombre = "";
        $("#txtPersonal").val("");
        $("#cboRol").val("");
        fnLoading(false);
    });

    $("#btnObjetivos").click(function() {
        fnLoading(true);
        $("#txtPersonal").val("");
        $("#cboRol").val("");
        $("#mdObjetivos").modal("show");
        fnLoading(false);
    });

    $("#btnAgregarObjetivo").click(function() {
        fnLoading(true);
        fnAgregarObjetivo();
        codper = "";
        nombre = "";
        $("#txtobjetivo").val("");
        $("#cboRol").val("");
        fnLoading(false);
    });
    $("#cboArea").change(function() {
        if ($("#cboArea").val() != 0) {
            fnListarOCDE($("#cboArea").val(), 'SA');
        } else {
            $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
        }
        $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
    })
    $("#cboSubArea").change(function() {
        if ($("#cboSubArea").val() != 0) {
            fnListarOCDE($("#cboSubArea").val(), 'DI');
        } else {
            $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
        }
    })

    $('#chkOCDE').click(function() {
        $("#cboArea").val("0");
        $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
        $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
        if ($(this).is(':checked')) {
            $("#ocde").attr("style", "display:block");
        } else {
            $("#ocde").removeAttr("style");
            $("#ocde").attr("style", "display:none");
        }
    });

    $("#btnObservar").click(function() {
        $("#mdObservar").modal("show");
        $("#txtDescripcionObservacion").val("");
    });
    $("#btnGuardarObservacion").click(function() {
        if ($("#txtDescripcionObservacion").val() == "") {
            fnMensaje("danger", "Debe Ingresar la Observación.")
        } else {
            fnConfirmarEnviar($("#hdcod").val(), 0, '¿Desea Observar el Proyecto.?');
        }
    });

    $('#chkExterno').click(function() {
        $("#txtexterno").val("");
        if ($(this).is(':checked')) {
            $("#txtexterno").attr("style", "display:block");
        } else {

            $("#txtexterno").attr("style", "display:none");
        }
    });

});

function cPersonal(ctf) {
    var arr = fnPersonal(ctf, '%');
    var n = arr.length;
    var str = "";
    str += '<option value="" >-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        if (arr[i].nombre == 'TODOS') {
            str += '<option value="' + arr[i].cod + '" selected>' + arr[i].nombre + '</option>';
        } else {
            str += '<option value="' + arr[i].cod + '" >' + arr[i].nombre + '</option>';
        }
    }
    $('#cboPersonal').html(str);
}

function cLineas() {
    var arr = fnLineas('%');
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboLinea').html(str);
}

function cAutor() {
    var arr = fnRolInvestigador('D');
    //console.log(arr)
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboRol').html(str);
}

function cFiltro() {

    $('form#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.cfe + '" />');
    $('form#frmRegistro').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
    var form = $("#frmRegistro").serializeArray();
    $("form#frmRegistro input[id=ctf]").remove();
    $("form#frmRegistro input[id=action]").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            str = ''
            selec = ''
            if (data.length > 0) {
                for (i = 0; i < data.length; i++) {
                    if (i == 0) { "selected='selected'" }
                    str += "<option value='" + data[i].cod + "' " + selec + ">" + data[i].nombre + "</option>";
                }
                $("#cboEstado").html(str);
            }

            fnLoading(false);
        },
        error: function(result) {
            fnMensaje("warning", result)
        }
    });

}

function fnAutoCPersonal() {
    jsonString = fnPersonal(1, '%');
    $('#txtPersonal').autocomplete({
        source: $.map(jsonString, function(item) {
            return item.nombre;
        }),
        select: function(event, ui) {
            var selectecItem = jsonString.filter(function(value) {
                return value.nombre == ui.item.value;
            });
            codper = selectecItem[0].cod;
            nombre = selectecItem[0].nombre;
            $('#PanelEvento').hide("fade");
        },
        minLength: 1,
        delay: 500
    });

    $('#txtPersonal').keyup(function() {
        var l = parseInt($(this).val().length);
        if (l == 0) {
            codper = "";
            nombre = "";
        }
    });
}

function fnAgregarIntegrante() {
    var value;
    var tb = '';
    var rowCount = $('#tbEquipo tr').length;
    var repite = false;

    if (fnValidarIntegrante() == true) {
        //          $.grep(detalles, function(e) { return e.item == id; });
        for (i = 0; i < equipo.length; i++) {
            if (equipo[i].cod == codper && equipo[i].estado == 1) {
                repite = true;
            }
        }
        ////console.log(repite);
        if (repite == false) {
            $('#tbEquipo tr').each(function() {
                value = $(this).find("td:first").html();

            });
            if (!($.isNumeric(value))) { rowCount = 0 }
            var row = (rowCount + 1);
            equipo.push({
                cod_aut: 0,
                cod: codper,
                nombre: nombre,
                codtipo: $("#cboRol").val(),
                rol: $("#cboRol option:selected").text(),
                estado: 1
            });
            //console.log(detalles);
            for (i = 0; i < equipo.length; i++) {
                if (equipo[i].estado == 1) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + equipo[i].nombre + '</td>';
                    tb += '<td>' + equipo[i].rol + '</td>';
                    //tb += '<td style="text-align:center"></td>';
                    tb += '<td style="text-align:center">';
                    if (equipo[i].rol == "COORDINADOR GENERAL") {
                        tb += '';
                    } else {
                        tb += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarIntegrante(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button>';
                    }
                    tb += '</td>';
                    tb += '</tr>';
                }
            }
            fnDestroyDataTableDetalle('tEquipo');
            $('#tbEquipo').html(tb);
            fnResetDataTableBasic('tEquipo', 0, 'asc', 10);
        } else {
            fnMensaje("warning", "El Personal ya ha sido ingresado")
        }

    }
}

function fnValidarIntegrante() {
    if ($("#txtPersonal").val() == "" || codper == "") {
        fnMensaje("warning", "Debe Seleccionar un Personal Docente/Administrativo");
        return false;
    }
    if ($("#cboRol").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Rol para el Personal");
        return false;
    }
    if ($("#cboRol  option:selected").text() == "COORDINADOR GENERAL") {
        var cont = 0;
        for (i = 0; i < equipo.length; i++) {
            if (equipo[i].rol == "COORDINADOR GENERAL" && equipo[i].estado == 1) {
                cont = cont + 1;
            }
        }
        if (cont > 0) {
            fnMensaje("warning", "El Grupo Solo puede tener un Coordinador General.");
            return false;
        }
    }
    return true;
}



function fnQuitarIntegrante(cod) {
    var tb = '';
    //console.log(cod);
    //      document.getElementById("tEval").deleteRow(cod);
    if (equipo[cod - 1].cod_aut == 0) {
        equipo.splice(cod - 1, 1);
    } else {
        equipo[cod - 1].estado = 0;
    }
    for (i = 0; i < equipo.length; i++) {
        if (equipo[i].estado == 1) {
            tb += '<tr>';
            tb += '<td style="text-align:center">' + (i + 1) + '</td>';
            tb += '<td>' + equipo[i].nombre + '</td>';
            tb += '<td>' + equipo[i].rol + '</td>';
            //tb += '<td style="text-align:center">' + detalles[i].resultadomostrar + '</td>';
            //tb += '<td style="text-align:center"></td>';
            tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarIntegrante(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
            tb += '</tr>';
        }
    }

    fnDestroyDataTableDetalle('tEquipo');
    $('#tbEquipo').html(tb);
    fnResetDataTableBasic('tEquipo', 0, 'asc', 10);
}


function fnAgregarObjetivo() {
    var value;
    var tb = '';
    var rowCount = $('#tbObjetivos tr').length;
    var repite = false;
    var msje = '';
    if (fnValidarObjetivo() == true) {
        for (i = 0; i < objetivos.length; i++) {
            if (objetivos[i].descripcion == $("#txtobjetivo").val() && objetivos[i].estado == 1) {
                repite = true;
                msje = "El Objetivo ya se encuentra Registrado.";

            }
        }
        if ($("#cboTipoObjetivo option:selected").text() == "GENERAL") {
            for (i = 0; i < objetivos.length; i++) {
                if (objetivos[i].tipo == "GENERAL" && objetivos[i].estado == 1) {
                    repite = true;
                    msje = "El Proyecto solo puede contener un Objetivo General.";
                }
            }
        }
        if (repite == false) {
            $('#tbObjetivos tr').each(function() {
                value = $(this).find("td:first").html();

            });
            if (!($.isNumeric(value))) { rowCount = 0 }

            var row = (rowCount + 1);

            objetivos.push({
                cod: codobj,
                descripcion: $("#txtobjetivo").val(),
                codtipo: $("#cboTipoObjetivo").val(),
                tipo: $("#cboTipoObjetivo option:selected").text(),
                estado: 1
            });

            //console.log(detalles);

            for (i = 0; i < objetivos.length; i++) {
                if (objetivos[i].estado == 1) {
                    if (objetivos[i].tipo == "GENERAL") {
                        tb += '<tr style="font-weight:bold;color:green;">';
                    } else {
                        tb += '<tr>';
                    }
                    //                tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + objetivos[i].descripcion + '</td>';
                    tb += '<td>' + objetivos[i].tipo + '</td>';
                    //tb += '<td style="text-align:center"></td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
            }
            fnDestroyDataTableDetalle('tObjetivos');

            $('#tbObjetivos').html(tb);
            fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);

        } else {
            fnMensaje("warning", msje)
        }

    }
}

function fnValidarObjetivo() {
    if ($("#txtobjetivo").val() == "") {
        fnMensaje("warning", "Ingrese Descripción de Objetivo");
        return false;
    }
    if ($("#cboTipoObjetivo").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Tipo de Objetivo");
        return false;
    }
    return true;
}

function fnQuitarObjetivo(cod) {
    var tb = '';
    //console.log(cod);
    if (objetivos[cod - 1].cod == 0) {
        objetivos.splice(cod - 1, 1);
    } else {
        objetivos[cod - 1].estado = 0;
    }
    for (i = 0; i < objetivos.length; i++) {
        if (objetivos[i].estado == 1) {
            if (objetivos[i].tipo == "GENERAL") {
                tb += '<tr style="font-weight:bold;color:green;">';
            } else {
                tb += '<tr>';
            }
            tb += '<td>' + objetivos[i].descripcion + '</td>';
            tb += '<td>' + objetivos[i].tipo + '</td>';
            tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
            tb += '</tr>';
        }
    }

    fnDestroyDataTableDetalle('tObjetivos');
    $('#tbObjetivos').html(tb);
    fnResetDataTableBasic('tObjetivos', 1, 'desc', 10);
}


function fnGuardarProyecto() {
    if (fnValidarProyecto() == true) {
        rpta = fnvalidaSession()
        if (rpta == true) {
            fnLoading(true)
            $('form#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
            $('form#frmRegistro').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
            var form = $("#frmRegistro").serializeArray();
            $("form#frmRegistro input[id=ctf]").remove();
            $("form#frmRegistro input[id=action]").remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        fnGuardarObjetivos(data[0].cod);
                        fnGuardarEquipo(data[0].cod);
                        //fnLimpiar()
                        /*if ($("#filepto").val() != "") {
                        //console.log("INGRESA ARCHIVO PTO");
                        SubirArchivo(data[0].cod, "PRESUPUESTO");
                        }*/
                        if ($("#fileinforme").val() != "") {
                            //console.log("INGRESA ARCHIVO INFORME");
                            SubirArchivo(data[0].cod, "INFORME");
                        }
                        $("#mdRegistro").modal("hide");
                        fnListarProyecto();
                        /*ConsultarEgresado();
                        $("#Lista").show();*/
                        fnLoading(false);
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                    fnLoading(false);
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
}


function fnGuardarObjetivos(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        //if ($('#codE').val() == "0") {
        var form = JSON.stringify(objetivos);
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: { "hdcodP": cod, "action": ope.rob, "array": form },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log("OK");
            },
            error: function(result) {
                //            //console.log(result)
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function fnGuardarEquipo(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        //if ($('#codE').val() == "0") {
        var form = JSON.stringify(equipo);
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: { "hdcodP": cod, "action": ope.req, "array": form },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
            },
            error: function(result) {
                //            //console.log(result)
                fnMensaje("warning", result)
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function SubirArchivo(cod, tipo) {
    fnLoading(true)
    var flag = false;
    try {

        var data = new FormData();
        data.append("action", ope.Up);
        data.append("codigo", cod);
        data.append("tipo", tipo);

        if (tipo == "PRESUPUESTO") {
            var files = $("#filepto").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }
        if (tipo == "INFORME") {
            var files = $("#fileinforme").get(0).files;
            if (files.length > 0) {
                data.append("ArchivoASubir", files[0]);
            }
        }
        //console.log(data);
        if (files.length > 0) {
            // fnMensaje('primary', 'Subiendo Archivo');
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
                data: data,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                async: false,
                success: function(data) {
                    flag = true;
                    console.log('ARCHIVO SUBIDO');
                    //console.log(data);

                },
                error: function(result) {
                    //console.log('falseee');
                    flag = false;
                    //console.log(result);
                }
            });
            //flag= true;
        }

        //        } else {
        //            alert("Pf. Verificar extensión de archivo");
        //        }
        return flag;
        fnLoading(false);
    }
    catch (err) {
        return false;
        fnLoading(false);
    }
    fnLoading(false);
}

function fnListarProyecto() {
    if ($("#cboPersonal").val() !== "" && $("#cboEstado").val() !== "") {
        rpta = fnvalidaSession()
        if (rpta == true) {
            fnLoading(true)
            $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
            $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
            var form = $("#frmbuscar").serializeArray();
            $("form#frmbuscar input[id=action]").remove();
            $("form#frmbuscar input[id=ctf]").remove();
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    var clase_icono = "";
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<tr>';
                            tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                            tb += '<td>' + data[i].titulo + '</td>';
                            tb += '<td>' + data[i].coordinador + '</td>';
                            //tb += '<td style="text-align:center">' + data[i].fecini + '</td>';
                            //tb += '<td style="text-align:center">' + data[i].fecfin + '</td>';
                            tb += '<td style="text-align:center">' + data[i].estado + '</td>';
                            tb += '<td style="text-align:center">';
                            if (data[i].estado == "REGISTRO") {
                                clase_icono = "ion-edit";
                            } else {
                                clase_icono = "ion-eye";
                            }
                            tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(\'' + data[i].cod + '\')" title="Editar" ><i class="' + clase_icono + '"></i></button>';
                            if ((data[i].estado == 'REGISTRO' || data[i].estado == 'OBSERVADO') && (ObtenerValorGET("ctf") == 1 || ObtenerValorGET("ctf") == 13 || ObtenerValorGET("ctf") == 65)) {
                                // EN ETAPA DE REGISTRO//OBSERVACION FACULTAD//OBSERVACION DPTO//OBSERVACION COORD. GENERAL APARECE PARA ENVIAR
                                tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-orange" onclick="fnConfirmarEnviar(\'' + data[i].cod + '\',99,\'¿Desea Enviar el Proyecto.?\')" title="Enviar a Evaluación" ><i class="ion-arrow-right-a"></i></button>';
                            }
                            //tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="fnEliminar(\'' + data[i].cod + '\')" title="Eliminar" ><i class="ion-close"></i></button>';
                            tb += '</td>';
                            tb += '</tr>';
                        }
                    }
                    fnDestroyDataTableDetalle('tProyectos');
                    $('#tbProyectos').html(tb);
                    fnResetDataTableBasic('tProyectos', 0, 'asc', 15);

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
}


function fnEditar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        fnLimpiarProyecto();
        $("#hdcod").val(cod)
        $("form#frmRegistro input[id=action]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //                console.log(data);
                $("#txttitulo").val(data[0].titulo);
                
                cLineas();
                var cuenta = 0;
                $("#cboLinea option").each(function() {
                    //console.log($(this).val() + " - " + data[0].linea);
                    if ($(this).val() == data[0].linea) {
                        cuenta = cuenta + 1;
                    }
                });

                //console.log(cuenta);
                if (cuenta > 0) {
                    $('#cboLinea').val(data[0].linea);
                } else {
                    $('#cboLinea').append("<option value='" + data[0].linea + "'>" + data[0].nombrelinea + "</option>");
                    $('#cboLinea').val(data[0].linea);
                }
                
                $("#txtfecini").val(data[0].fecini);
                $("#txtfecfin").val(data[0].fecfin);
                //                $("#cboFinanciamiento").val(data[0].financiamiento);
                var array = data[0].financiamiento.split(',');
                for (var i = 0; i < array.length; i++) {
                    if (array[i] == 'P') {
                        $("#chkPropio").prop("checked", true)
                    }
                    if (array[i] == 'U') {
                        $("#chkUsat").prop("checked", true)
                    }
                    if (array[i] == 'E') {
                        $("#chkExterno").prop("checked", true)
                        $("#txtexterno").attr("style", "display:block")
                        $("#txtexterno").val(data[0].financia_ext)
                    }
                }

                $("#txtavance").val(data[0].avance);
                $("#cboAvance").val(data[0].estadoavance);
                $("#txtpresupuesto").val(data[0].presupuesto);
                if (data[0].rutapto != "") {
                    $("#file_pto").html("<a href='" + data[0].rutapto + "' target='_blank'>Presupuesto</a>")
                } else {
                    $("#file_pto").html("");
                }
                /*if (data[0].rutainforme != "") {
                $("#file_informe").html("<a href='" + data[0].rutainforme + "' target='_blank'>Informe</a>")*/
                if (data[0].rutainforme != "") {
                    $("#file_informe").html('<a onclick="fnDownload(\'' + data[0].rutainforme + '\')" >Informe</a>')

                } else {
                    $("#file_informe").html("");
                }
                $("#mdRegistro").modal("show");
                fnListarObjetivos(cod);
                fnListarEquipo(cod);
                fnListarObservaciones(cod);
                if ((data[0].estado == 'EN EVALUACIÓN' || data[0].estado == 'OBSERVADO' || data[0].estado == 'APROBADO') && (ObtenerValorGET("ctf") == 1 || data[0].instancia == 'DIRECTOR DE DEPARTAMENTO' || data[0].instancia == 'COORDINADOR DE INVESTIGACIÓN DE FACULTAD' || data[0].instancia == 'COORDINADOR GENERAL DE INVESTIGACIÓN')) {
                    $("#Evaluacion").attr("style", "text-align: right;display:block");
                    //$("#btnObservar").attr("onclick", "fnEnviar(\'" + cod + "\',0)");
                    $("#btnAprobar").attr("onclick", "fnConfirmarEnviar(\'" + cod + "\',1,'¿Desea Aprobar el Proyecto.?')");
                    if (data[0].estado == 'APROBADO') {
                        $("#btnAprobar").attr("style", "display:none");
                    } else {
                        $("#btnAprobar").removeAttr("style");
                    }
                } else {
                    $("#Evaluacion").removeAttr("style");
                    $("#Evaluacion").attr("style", "text-align: right;display:none");
                    //$("#btnObservar").removeAttr("onclick");
                    $("#btnAprobar").removeAttr("onclick");
                }
                if (data[0].cod_dis != "0") {
                    $("#chkOCDE").prop('checked', true);
                    $("#ocde").attr("style", "display:block");
                    $("#cboArea").val(data[0].cod_area)
                    fnListarOCDE($("#cboArea").val(), 'SA');
                    $("#cboSubArea").val(data[0].cod_sub)
                    fnListarOCDE($("#cboSubArea").val(), 'DI');
                    $("#cboDisciplina").val(data[0].cod_dis)
                } else {
                    $("#chkOCDE").prop('checked', false);
                    $("#ocde").removeAttr("style");
                    $("#ocde").attr("style", "display:none");
                }
                if (ObtenerValorGET("ctf") == 1) {
                    $("#btnA").attr("style", "display:inline-block");
                } else {
                    $("#btnA").attr("style", "display:none");
                }
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

function fnConfirmarEnviar(cod, veredicto, mensaje) {
    fnMensajeConfirmarEliminar('top', mensaje, 'fnEnviar', cod, veredicto);
}

// VEREDICTO 0 : OBSERVAR , 1 : APROBAR
function fnEnviar(cod, vered) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=cod]").remove();
        $("form#frmbuscar input[id=veredicto]").remove();
        $("form#frmbuscar input[id=txtobservacion]").remove();
        $('#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.aie + '" />');
        $('#frmbuscar').append('<input type="hidden" id="veredicto" name="veredicto" value="' + vered + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="txtobservacion" name="txtobservacion" value="' + $("#txtDescripcionObservacion").val() + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=ctf]").remove();
        $("form#frmbuscar input[id=cod]").remove();
        $("form#frmbuscar input[id=veredicto]").remove();
        $("form#frmbuscar input[id=txtobservacion]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje);
                    if (vered == 1) {
                        $("#mdRegistro").modal("hide");
                    } else {
                        $("#mdObservar").modal("hide");
                        fnListarObservaciones(cod);
                    }
                    if (data[0].EnviaCorreo == 1) {
                        EnviarEmail(cod, vered)
                    }
                    fnListarProyecto();
                } else {
                    fnMensaje("error", data[0].msje)
                }
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

function EnviarEmail(cod, veredicto) {

    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
        data: { "action": ope.email, "param1": cod, "param2": veredicto },
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            if (data[0].rpta == 1) {
                fnMensaje("success", data[0].msje);
            } else {
                fnMensaje("error", data[0].msje);
            }
        },
        error: function(result) {
            console.log(result); //--para errores                      
        }

    });
}



function fnListarObservaciones(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=cod]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lop + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=cod]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                var filas = data.length;
                var div = ""
                if (filas > 0) {
                    div += "<div class='alert alert-danger'>";
                    for (i = 0; i < filas; i++) {
                        if (data[i].sol == 0) {
                            div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:red'> - " + data[i].des + "</label></br>";
                        } else {
                            div += "<label style='font-size:12px;line-height:10px;margin-botton:2px;color:green'> - " + data[i].des + "</label></br>";
                        }
                    }
                    div += "</div>";
                } else {
                    div = ""
                }
                $("#DivObservaciones").html(div);
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


function fnListarObjetivos(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lob + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                objetivos = [];
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        if (data[i].tipo == "GENERAL") {
                            tb += '<tr style="font-weight:bold;color:green;">';
                        } else {
                            tb += '<tr>';
                        }
                        tb += '<td>' + data[i].des + '</td>';
                        tb += '<td>' + data[i].tipo + '</td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarObjetivo(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
                        tb += '</tr>';
                        objetivos.push({
                            cod: data[i].cod,
                            descripcion: data[i].des,
                            codtipo: data[i].codtipo,
                            tipo: data[i].tipo,
                            estado: data[i].estado
                        });

                    }
                }
                //console.log(objetivos);
                fnDestroyDataTableDetalle('tObjetivos');
                $('#tbObjetivos').html(tb);
                fnCreateDataTableBasic('tObjetivos', 1, 'desc', 10);

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

function fnListarEquipo(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lap + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=hdcod]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Proyecto.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                equipo = [];
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {

                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                        tb += '<td>' + data[i].autor + '</td>';
                        tb += '<td>' + data[i].tipo + '</td>';
                        //tb += '<td style="text-align:center"></td>';
                        if (data[i].tipo == 'COORDINADOR GENERAL') {
                            tb += '<td style="text-align:center"></td>';
                        } else {
                            tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnQuitarIntegrante(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
                        }
                        tb += '</tr>';

                        equipo.push({
                            cod_aut: data[i].cod,
                            cod: data[i].codper,
                            nombre: data[i].autor,
                            codtipo: data[i].codtipo,
                            rol: data[i].tipo,
                            estado: data[i].estado
                        });

                    }
                }
                //console.log(equipo);
                fnDestroyDataTableDetalle('tEquipo');
                $('#tbEquipo').html(tb);
                fnCreateDataTableBasic('tEquipo', 0, 'asc', 10);

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

function fnLimpiarProyecto() {
    $("#hdcod").val("0");
    $("#txttitulo").val("");
    $("#cboLinea").val("");
    $("#cboArea").val("0");
    $("#cboSubArea").html("<option value='0'>-- Seleccione --</option>");
    $("#cboDisciplina").html("<option value='0'>-- Seleccione --</option>");
    $("#txtfecini").val("");
    $("#txtfecfin").val("");
    $("#cboFinanciamiento").val("");
    //    $("#filepto").val("");
    $("#txtpresupuesto").val("");
    $("#txtavance").val("");
    $("#cboAvance").val("");
    $("#fileinforme").val("");
    $("#ocde").removeAttr("style");
    $("#ocde").attr("style", "display:none");
    $("#chkOCDE").prop('checked', false);

    $("#chkPropio").prop('checked', false);
    $("#chkUsat").prop('checked', false);
    $("#chkExterno").prop('checked', false);
    $("#txtexterno").attr("style", "display:none");
    $("#txtexterno").val('');

}

function fnValidarProyecto() {
    if ($("#txttitulo").val() == "") {
        fnMensaje("warning", "Ingrese Titulo de Proyecto");
        return false;
    }

    // Verificar Objetivos, un general 
    var banderaG = 0;
    //var banderaE = 0;
    for (i = 0; i < objetivos.length; i++) {
        if (objetivos[i].estado == 1 && objetivos[i].tipo == 'GENERAL') {
            banderaG = banderaG + 1;
        }
        //        if (objetivos[i].estado == 1 && objetivos[i].tipo != 'GENERAL') {
        //            banderaE = banderaE + 1;
        //        }
    }
    if (banderaG == 0) {
        fnMensaje("warning", "Proyecto Debe Contar con un Objetivo General como mínimo.");
        return false;
    }
    // Verificar Equipo, como mínimo 1
    var bandera = 0;
    for (i = 0; i < equipo.length; i++) {
        if (equipo[i].estado == 1) {
            bandera = bandera + 1;
        }
    }
    if (bandera == 0) {
        fnMensaje("warning", "Proyecto Debe Contar con Un Autor como mínimo.");
        return false;
    }

    if ($("#cboLinea").val() == "") {
        fnMensaje("warning", "Debe Seleccionar la Linea de Investigación");
        return false;
    }
    if ($("#chkOCDE").is(':checked')) {
        if ($("#cboArea").val() == "0") {
            fnMensaje("warning", "Debe Seleccionar Una Área de Investigación OCDE");
            return false;
        }
        if ($("#cboSubArea").val() == "0") {
            fnMensaje("warning", "Debe Seleccionar una Sub Área de Investigación OCDE");
            return false;
        }
        if ($("#cboDisciplina").val() == "0") {
            fnMensaje("warning", "Debe Seleccionar una Disciplina de Investigación OCDE");
            return false;
        }
    }

    if ($("#txtfecini").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Inicio");
        return false;
    }
    if ($("#txtfecfin").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Fin");
        return false;
    }
    var fecini = new Date($("#txtfecini").val().substr(3, 2) + '/' + $("#txtfecini").val().substr(0, 2) + '/' + $("#txtfecini").val().substr(6, 4));
    var fecfin = new Date($("#txtfecfin").val().substr(3, 2) + '/' + $("#txtfecfin").val().substr(0, 2) + '/' + $("#txtfecfin").val().substr(6, 4));

    if (fecini > fecfin) {
        fnMensaje("warning", "Fecha de Fin de Proyecto no Puede ser MKenor a la Fecha de Inicio.");
        return false;
    }
    //    if ($("#cboFinanciamiento").val() == "") {
    //        fnMensaje("warning", "Debe Seleccionar el Financiamiento del Proyecto");
    //        return false;
    //    }

    if ($("#chkPropio").is(':checked') == false && $("#chkUsat").is(':checked') == false && $("#chkExterno").is(':checked') == false) {
        fnMensaje("warning", "Debe Seleccionar el Financiamiento del Proyecto");
        return false;
    }

    if ($("#chkExterno").is(':checked') == true && $("#txtexterno").val() == "") {
        fnMensaje("warning", "Debe indicar el Financiamiento Externo");
        $("#txtexterno").focus()
        return false;
    }


    if ($("#txtavance").val() == "") {
        fnMensaje("warning", "Ingrese el Avance del Proyecto");
        return false;
    }
    var RE = /^\d*(\.\d{1})?\d{0,1}$/;
    if (!RE.test($("#txtavance").val())) {
        fnMensaje("warning", "Ingrese Correctanente el valor de Avance, puede colocar hasta 2 decimales después del punto decimal");
        return false;
    }
    if (parseFloat($("#txtavance").val()) > 100) {
        fnMensaje("warning", "Ingrese Correctanente el valor de Avance, No puede ser mayor al 100%");
        return false;
    }
    //    if ($("#filepto").val() != '') {
    //        archivo = $("#filepto").val();
    //        //Extensiones Permitidas
    //        extensiones_permitidas = new Array(".xls", ".xlsx");
    //        //recupero la extensión de este nombre de archivo
    //        // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
    //        archivo = archivo.substring(archivo.length - 5, archivo.length);
    //        //despues del punto de nombre recortado
    //        extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
    //        //compruebo si la extensión está entre las permitidas 
    //        permitida = false;
    //        for (var i = 0; i < extensiones_permitidas.length; i++) {
    //            if (extensiones_permitidas[i] == extension) {
    //                permitida = true;
    //                break;
    //            }
    //        }
    //        if (permitida == false) {

    //            fnMensaje("warning", "Solo puede Adjuntar Archivos de Presupuesto en Formato de Excel(.xls o.xlsx)");
    //            return false;
    //        }
    //    }

    if ($("#hdcod").val() == 0) {
        if ($("#fileinforme").val() == '') {
            fnMensaje("warning", "Seleccione el Archivo de Informe.");
            return false;
        }
    }
    if ($("#fileinforme").val() != '') {
        archivo = $("#fileinforme").val();
        //Extensiones Permitidas
        //extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
        extensiones_permitidas = new Array(".pdf");
        //recupero la extensión de este nombre de archivo
        // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
        archivo = archivo.substring(archivo.length - 5, archivo.length);
        //despues del punto de nombre recortado
        extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
        //compruebo si la extensión está entre las permitidas 
        permitida = false;
        for (var i = 0; i < extensiones_permitidas.length; i++) {
            if (extensiones_permitidas[i] == extension) {
                permitida = true;
                break;
            }
        }
        if (permitida == false) {
            //fnMensaje("warning", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
            fnMensaje("warning", "Solo puede Adjuntar Archivos de Informe en Formato PDF");
            return false;
        }
    }



    return true;
}

function fnCancelar() {
    fnCerrarModal();
    //fnMensajeConfirmarEliminar('top', '¿Desea Salir Sin Guardar Cambios.?', 'fnCerrarModal', '');
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}

function fnCerrarModal() {
    $("#mdRegistro").modal("hide");
}


function fnListarOCDE(cod, tipo) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=param1]").remove();
        $("form#frmbuscar input[id=param2]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="lAreaConocimientosOCDE" />');
        $('form#frmbuscar').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="param2" name="param2" value="' + tipo + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=param1]").remove();
        $("form#frmbuscar input[id=param2]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                var tb = '';
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<option value="' + data[i].codigo + '">' + data[i].descripcion + '</option>';
                    }
                }
                if (tipo == "AR") {
                    $("#cboArea").html(tb);
                }
                if (tipo == "SA") {
                    $("#cboSubArea").html(tb);
                }
                if (tipo == "DI") {
                    $("#cboDisciplina").html(tb);
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
/*
function fnDownload(id_ar) {
    var flag = false;
    var form = new FormData();
    form.append("action", "Download2");
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
}*/

function fnDownload(id_ar) {
    window.open("DescargarArchivo.aspx?Id=" + id_ar);
}



function downloadWithName(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
    // alert(link);
}
