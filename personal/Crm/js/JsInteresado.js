var filtroOrigen = [];
var origen = [];
var tipodocumento = [];
var Region = [];
var seleccionandoCarrera = false;
var requestId = '';

$(document).ready(function () {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tInteresados', 0, 'asc', 100);
    var dt = fnCreateDataTableBasic('tEMail', 0, 'asc', 2);

    ope = fnOperacion(1);
    //    console.log(ope)
    rpta = fnvalidaSession();

    //    alert(rpta)
    if (rpta == true) {
        var filtros = window.location.href.slice(window.location.href.indexOf('?') + 1);
        filtros = filtros.split('|');

        var codigoTestDefault = '' // POR DEFECTO PREGRADO

        if (filtros.length > 1) {
            codigoTestDefault = filtros[0];
        }

        // fnSitInt(1);
        // fnEstadoComunicacion();

        // fnSelectFiltroOrigen();

        // CALLBACK HELL!!!
        fnTipoEst(1, codigoTestDefault, function () {
            fnConvocatoria(function () {
                fnSelectEvento(function () {
                    fnSelectFiltroOrigen(function () {
                        fnFiltroGrados(function () {
                            fnSelectFiltroColegio(function () {
                                fnRequisitoAdmision(function () {
                                    fnSitInt(1, function () {
                                        fnEstadoComunicacion(function () {
                                            fnCarreraProfesional(function () {
                                                fnCentroCosto(function () {
                                                    if (filtros.length > 1) {
                                                        fnCargaFiltros();
                                                    } else {
                                                        fnLoading(false);
                                                    }
                                                });
                                            })
                                        });
                                    });
                                })
                            });
                        });
                    });
                })
            });
        });
    } else {
        window.location.href = rpta
    }
    // fnLoading(false);

    $('#cboTipoEstudio').change(function () {
        fnLoading(true);
        fnOnTipoEstudioChanged();
    });

    $('#cboConvocatoria').change(function () {
        // var codigoTest = $('#cboTipoEstudio').val();
        // var codigoCon = $(this).val();
        fnSelectEvento();
        fnSelectFiltroColegio();
    });

    $('#cboAcuerdo').on('change', function (e) {
        if ($(this).val() == '1') {
            $('#txtFechaAcuerdo').prop('disabled', false);
            $('#txtFechaAcuerdo').focus();
            $('#chkMisAcuerdos').prop('disabled', false);
        } else {
            $('#txtFechaAcuerdo').val('');
            $('#txtFechaAcuerdo').prop('disabled', true);
            $('#chkMisAcuerdos').prop('checked', false);
            $('#chkMisAcuerdos').prop('disabled', true);
        }
    });
    $('#cboAcuerdo').trigger('change'); // Para que se actualice el estado del control al cargar la página

    $('#cboRegionD').change(function () {
        fnProv();
    });

    $('#cboProvinciaD').change(function () {
        fnDis();
    });

    $("#btnAgregar").click(function () {
        $("#hdtip").val("N");
        fnSelectOrigen();
        fnSelectTipoDoc();
        fnGrados();

        fnReg();
        $("#btnInscribir").text("Inscribir y Cerrar")
        $("#btnInscribirYRedirigir").show()
        $('#hdcod').remove();
        $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
        Limpiar(1);
        $('#datos').find('input, text,textarea, button, select').not('#txtAnioEgreso').attr('disabled', 'disabled');
        $("#cboTipoDocumento").removeAttr("disabled");
        $("#txtnum_doc").removeAttr("readonly")
        $("#Coincidencias").removeAttr("style");
        $("#Coincidencias").attr("style", "display:none")
        $("#detalle_int").removeAttr("style");
        $("#detalle_int").attr("style", "display:block")
        $("#btnCancelarReg").removeAttr("disabled");
        $("#btnCoincidencias").attr("style", "display:none;");
        $('#cboTipoDocumento option:contains("DOC. NACIONAL DE IDENTIDAD")').attr('selected', 'selected');
        $('#cboTipoDocumento').trigger("change");
        $('#btnEliminarInteresado').hide();
        $('#mdRegistro').modal("show");
        $('#txtnum_doc').focus();
    });

    $('#btnExportar').on('click', function () {
        var $form = $('<form id="frmExportar" style="display: none;" action="../DataJson/crm/Interesado.aspx" method="post" target="_self">'
            + '<input type="hidden" id="action" name="action" value="' + ope.exp + '" />'
            + '<input type="hidden" id="requestId" name="requestId" value="' + requestId + '" />'
            + '</form>').appendTo(document.body);

        $form.submit();
        $form.remove();
    });

    $('#mdRegistro').on('shown.bs.modal', function () {
        $('#txtnum_doc').focus();
    })

    $('#mdInstitucionEd').on('shown.bs.modal', function () {
        $('input:text[aria-controls=tInstitucionEducativa]').focus();
    })

    $('#mdDireccion').on('shown.bs.modal', function () {
        $("#cboRegionD").focus();
    })

    $('#mdTelefono').on('shown.bs.modal', function () {
        $("#cboTipoTelefono").focus();
    })

    $('#mdEmail').on('shown.bs.modal', function () {
        $("#cboTipoEMail").focus();
    })

    $('#mdCarrera').on('shown.bs.modal', function () {
        $("#cboCarrera").focus();
    })

    $('#mdCarrera').on('hide.bs.modal', function () {
        seleccionandoCarrera = false;
    })

    $('#txtnum_doc').keyup(function (e) {
        if (e.keyCode == 13) {
            if (fnValidaBuscaxTipoyNumDoc() == true) {
                fnBuscaxTipoyNumDoc();
            }
        }
    })


    $('#txtfecnac').keyup(function (e) {
        if (e.keyCode == 13) {
            fnBuscaxApeyNom();
        }
    })

    $("#btnie").click(function () {
        if ($("#tbInstitucionEducativa").html().trim() == "") {
            fnBuscaIE();
        }
        $("#mdInstitucionEd").modal("show");
    });



    //    $('#mdRegistro').on('show.bs.modal', function(event) {

    //        var button = $(event.relatedTarget) // Botón que activó el modal
    //        fnSelectTipoDoc();
    //        fnReg();

    //        //        alert('--')
    //        if (button.attr("id") == "btnAgregar") {

    //        } else if (button.attr("id") == "btnE") {
    //            $("#btnInscribir").text("Guardar")
    //            $('#hdcod').remove();
    //            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
    //        }

    //    })

    $('#cboTipoDocumento').change(function () {
        $("#txtnum_doc").removeAttr("readonly");
        Limpiar(2);
        $("#btnCoincidencias").removeAttr("disabled");
        $("#btnCoincidencias").removeAttr("style")
        $("#btnCoincidencias").attr("disabled", "disabled");
        $("#btnCoincidencias").attr("style", "display:none;");
        $("#txtnum_doc").removeAttr("maxlength");
        $("#cboGradoEstudios").attr("disabled", "disabled");
        $("#btnie").attr("disabled", "disabled");
        $("#btndir").attr("disabled", "disabled");
        $("#btntel").attr("disabled", "disabled");
        $("#btnema").attr("disabled", "disabled");
        $("#btncp").attr("disabled", "disabled");
        $("#btnCuestionario").attr("disabled", "disabled");
        $("#btnInscribir").attr("disabled", "disabled");
        $("#btnInscribirYRedirigir").attr("disabled", "disabled");
        var opcion = $("#cboTipoDocumento option:selected").text()
        // Para DNI
        if (opcion == "DOC. NACIONAL DE IDENTIDAD") {
            $("#txtnum_doc").attr("maxlength", "8");
        } else {
            if (opcion == "REG. ÚNICO DE CONTRIBUYENTES") {
                $("#txtnum_doc").attr("maxlength", "11");
            } else {
                if (opcion == "PASAPORTE" || opcion == "CARNET DE EXTRANJERÍA") {
                    $("#txtnum_doc").attr("maxlength", "12");
                } else {
                    $("#txtnum_doc").attr("maxlength", "15");
                }
            }
        }
    });

    $('#cboRegionIE').change(function () {
        fnBuscaIE();
    });

    $("#btndir").click(function () {
        LimpiarDireccion(1)
        if ($("#hdcod_i").val() == 0) {
            RegistrarInteresado()
            if ($("#hdcod_i").val() == 0) {
            } else {
                fnListarDirecciones()

                $('#mdDireccion').modal("show")
            }
        } else {
            fnListarDirecciones()
            $('#mdDireccion').modal("show")
        }

    })


    $("#btntel").click(function () {
        LimpiarTelefono(1)
        if ($("#hdcod_i").val() == 0) {
            RegistrarInteresado()
            if ($("#hdcod_i").val() == 0) {
            } else {
                fnListarTelefonos()
                $('#mdTelefono').modal("show")
            }
        } else {
            fnListarTelefonos()
            $('#mdTelefono').modal("show")
        }
    })

    $("#btnema").click(function () {
        LimpiarEMail(1)
        if ($("#hdcod_i").val() == 0) {
            RegistrarInteresado()
            if ($("#hdcod_i").val() == 0) {
            } else {
                fnListarEmail()
                $('#mdEmail').modal("show")
            }
        } else {
            fnListarEmail()
            $('#mdEmail').modal("show")
        }
    })

    $("#btncp").click(function () {
        seleccionandoCarrera = true;

        LimpiarCarrera(1)
        if ($("#hdcod_i").val() == 0) {
            RegistrarInteresado()
            seleccionandoCarrera = false;

            if ($("#hdcod_i").val() == 0) {
            } else {
                fnCpf($("#codeve").val(), 0)
                fnListaPrioridad(0);
                fnListarCarreras();
                $('#mdCarrera').modal("show")
            }
        } else {
            fnCpf($("#codeve").val(), 0)
            fnListaPrioridad(0);
            fnListarCarreras();
            $('#mdCarrera').modal("show")
        }
    })

    $("#btnCuestionario").click(function () {
        if ($("#hdcod_i").val() == 0) {
            RegistrarInteresado()
            if ($("#hdcod_i").val() == 0) {
            } else {
                fnListarPreguntas();
                $("#mdCuestionario").modal("show");
            }
        } else {
            fnListarPreguntas();
            $("#mdCuestionario").modal("show");

        }
    })

    $('#bntResetEvento').on('click', function () {
        resetCboEvento();
    });

    $('#bntResetCarreraProfesional').on('click', function () {
        resetCboCarreraProfesional();
    });

    $('#bntResetCentroCosto').on('click', function () {
        resetCboCentroCosto();
    });

    $('#bntResetRequisitoAdmision').on('click', function () {
        resetCboRequisitoAdmision();
    });

    $('#bntResetFiltroColegio').on('click', function () {
        resetCboFiltroColegio();
    });

    $('#btnAsignarAnexo').on('click', function (e) {
        fnLoading(true);

        $.ajax({
            type: "POST",
            url: "../DataJson/crm/PersonalAnexo.aspx",
            data: { "action": ope.lst },
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                console.log(data);
                for (var i = 0; i < data.length; i++) {
                    $('#mdAnexo #txtNumeroAnexo').val(data[i].cNumero);
                }
                $('#mdAnexo').modal('show');
            },
            error: function (result) {
                //                console.log(result)
                fnMensaje("error", result)
            },
            complete: function () {
                fnLoading(false);
            }
        });
    });

    $('#mdAnexo').on('shown.bs.modal', function () {
        $('#mdAnexo #txtNumeroAnexo').focus();
    });

    $('#btnGuardarAnexo').on('click', function (e) {
        var numeroAnexo = $('#txtNumeroAnexo').val();
        guardarAnexo(numeroAnexo);
    });

    var callbackConfirmar;
    $('#btnEliminarInteresado').on('click', function (e) {
        callbackConfirmar = function () {
            var codigoInt = $("#hdcod_i").val();
            fnLoading(true);

            $.ajax({
                type: "POST",
                url: "../DataJson/crm/Interesado.aspx",
                data: {
                    action: ope.eli,
                    codigoInt: codigoInt
                },
                dataType: "json",
                cache: false,
                async: false,
                success: function (data) {
                    if (data.rpta == 1) {
                        var $fila = $('#tbInteresados tr[data-cod="' + codigoInt + '"]');
                        $fila.remove();
                        fnLoading(false);
                        $('#mdRegistro').modal('hide');
                        fnMensaje('success', 'Interesado eliminado correctamente');
                    } else {
                        fnMensaje("error", "No se ha podido eliminar al interesado: " + data.msje);
                    }
                },
                error: function (result) {
                    console.log(result);
                    fnLoading(false);
                },
            });
        }
        $('#mdlConfirmar').modal('show');
    });

    $('#mdlConfirmar #btnConfirmar').on('click', function (e) {
        $('#mdlConfirmar').modal('hide');
        if (callbackConfirmar != undefined) {
            callbackConfirmar();
        }
    });
});

function fnOnTipoEstudioChanged() {
    //CALLBACK HELL!
    fnConvocatoria(function () {
        fnSelectEvento(function () {
            fnSitInt(1, function () {
                fnCarreraProfesional(function () {
                    fnFiltroGrados(function () {
                        fnCentroCosto(function () {
                            fnLimpiarTabla();
                            fnLoading(false);
                        });
                    });
                });
            });
        });
    });
}

function fnLimpiarTabla() {  
    $('#tbInteresados').empty();
}

function fnSitInt(op, callback) {
    var codigoTest = $('#cboTipoEstudio').val();
    var arr = fnSituacionInteresado(1, "XTE", op, codigoTest);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    if (op == 1) {
        $('#cboTipoPersona').html(str);
    }

    if (callback != undefined) {
        callback();
    }
}

function fnTipoEst(op, codigoTest, callback) {

    var arr = fnTipoEstudio(1, "TO", op);
    var n = arr.length;
    var str = "";
    str += '<option value=""> -- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        if (arr[i].nombre == 'TODOS') {
            continue;
        }
        if ((codigoTest == '' && arr[i].nombre == 'PRE GRADO') || arr[i].cod == codigoTest) {
            str += '<option value="' + arr[i].cod + '" selected>' + arr[i].nombre + '</option>';
        } else {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }
        // if (arr[i].nombre == 'PRE GRADO') {
        //     str += '<option value="' + arr[i].cod + '" selected>' + arr[i].nombre + '</option>';
        // } else {
        //     str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        // }
    }
    if (op == 1) {
        $('#cboTipoEstudio').html(str);
        // $('#cboTipoEstudio').trigger('change');
    }
    if (op == 2) {
        $('#cboTipoEstudioR').html(str);
    }

    if (callback != undefined) {
        callback();
    }
}

function fnConvocatoria(callback) {
    var codigoTest = $('#cboTipoEstudio').val();

    fConvocatoria(1, "C", codigoTest, function (arr) {
        var n = arr.length;
        var str = "";
        str += '<option value="" >-- Seleccione -- </option>';
        $('#cboEvento').html(str);
        for (i = 0; i < n; i++) {
            if (arr[i].nombre == 'TODOS') {
                continue;
            }
            if (i == 0) {
                str += '<option value="' + arr[i].cod + '" selected>' + arr[i].nombre + '</option>';
            } else {
                str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
            }
        }
        $('#cboConvocatoria').html(str);
        // initCboEvento();

        fnSelectFiltroColegio();

        if (callback != undefined) {
            callback();
        }
    });
}

function fnCentroCosto(callback) {
    var codigoTest = $('#cboTipoEstudio').val();

    fCentroCosto(1, codigoTest, function (arr) {
        var n = arr.length;
        var str = "";
        // str += '<option value=""> -- Seleccione -- </option>';
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }
        // str += '<option value="" selected>TODOS</option>';
        $('#cboCentroCosto').html(str);
        initCboCentroCosto();

        if (callback != undefined) {
            // fnLoading(false);
            callback();
        }
    });
}

function fnFiltroGrados(callback) {
    var codigoTest = $('#cboTipoEstudio').val();
    fGradosPorTipoEstudio(1, codigoTest, function (arr) {
        var n = arr.length;
        var str = "";
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }
        $('#cboGrados').html(str);
        initCboFiltroGrados();

        if (callback != undefined) {
            callback();
        }
    });
}

function fnGrados(callback) {  
    var codigoTest = $('#cboTipoEstudio').val();
    fGradosPorTipoEstudio(1, codigoTest, function (arr) {
        var n = arr.length;
        var str = "";
        str += '<option value=""> -- Seleccione -- </option>';
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }
        $('#cboGradoEstudios').html(str);
        initCboFiltroGrados();

        if (callback != undefined) {
            callback();
        }
    });
}

function fnRequisitoAdmision(callback) {
    var codigoTest = $('#cboTipoEstudio').val();
    var codigoMin = '';

    fRequisitoAdmision(1, codigoTest, codigoMin, function (arr) {
        var n = arr.length;
        var str = "";
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cod + '">' + arr[i].des + '</option>';
        }
        $('#cboRequisitoAdmision').html(str);
        initCboRequisitoAdmision();

        if (callback != undefined) {
            callback();
        }
    });
}

function fnEstadoComunicacion(callback) {
    var arr = fEstadoComunicacionBusqueda(1, "CI", '0');
    //    console.log(arr)
    var n = arr.length;
    var str = "";
    str += '<option value="-1" selected>TODOS</option>';
    $('#cboComunicacion').html(str);
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        // if (arr[i].nombre == 'SIN COMUNICACIÓN') {
        //     str += '<option value="' + arr[i].cod + '" selected>' + arr[i].nombre + '</option>';
        // } else {
        // }
    }
    $('#cboComunicacion').html(str);

    if (callback != undefined) {
        callback();
    }
}


function fnSelectEvento(callback) {
    var codigoTest = $('#cboTipoEstudio').val();
    var codigoCon = $('#cboConvocatoria').val();

    var arr = fnEvento(1, "C", codigoTest, codigoCon);
    //    console.log(arr)
    var n = arr.length;
    var str = "";
    // str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        if (arr[i].nombre == 'TODOS') {
            continue;
        }
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboEvento').html(str);
    initCboEvento();

    if (callback != undefined) {
        callback();
    }
}

function fnCarreraProfesional(callback) {
    var cod_test = $('#cboTipoEstudio').val();
    var cod_eve = $('#cboEvento').val();

    if (cod_eve == null) {
        cod_eve = 0;
    }

    fnCarrera(1, 'T', cod_eve, cod_test, function (arr) {
        var n = arr.length;
        var str = "";
        // str += '<option value="" >TODOS</option>';
        $('#cboCarreraProfesional').html(str);
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
        }
        $('#cboCarreraProfesional').html(str);
        initCboCarreraProfesional();

        if (callback != undefined) {
            callback();
        }
    });
}

function fnSelectFiltroOrigen(callback) {
    if (filtroOrigen.length == 0) {
        var arr = fnOrigen(1);
        for (i = 0; i < arr.length; i++) {
            filtroOrigen.push({
                cod: arr[i].cod,
                nombre: arr[i].nombre
            });
        }
    }

    var n = filtroOrigen.length;
    var str = "";
    str += '<option value="" selected>TODOS</option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + filtroOrigen[i].cod + '">' + filtroOrigen[i].nombre + '</option>';
    }
    $('#cboFiltroOrigen').html(str);

    if (callback != undefined) {
        callback();
    }
}

function fnSelectFiltroColegio(callback) {
    var codigoCon = $('#cboConvocatoria').val();
    fnInstitucionEducativaPorConvocatoria(1, 'DIS', 0, false, codigoCon, function (arr) {
        var n = arr.length;
        var str = "";
        $('#cboFiltroColegio').html(str);
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cod + '">' + arr[i].nom + ' | ' + arr[i].dis + ' - ' + arr[i].pro + ' - ' + arr[i].dep + '</option>';
        }
        $('#cboFiltroColegio').html(str);
        initCboFiltroColegio();
        setFiltroColegio();

        if (callback != undefined) {
            callback();
        }
    });
}

function setFiltroColegio() {
    if (window.location.href.indexOf('?') == -1) {
        return;
    }

    var filtros = window.location.href.slice(window.location.href.indexOf('?') + 1);
    filtros = filtros.split('|');

    if (filtros[18] != undefined) {
        $.each(filtros[18].split(','), function (i, e) {
            $("#cboFiltroColegio option[value='" + e + "']").prop('selected', true)
        });
    }
    $("#cboFiltroColegio").selectpicker('refresh');
}

function fnSelectOrigen() {

    if (origen.length == 0) {
        var arr = fnOrigen(1);
        for (i = 0; i < arr.length; i++) {
            origen.push({
                cod: arr[i].cod,
                nombre: arr[i].nombre
            });
        }
    }

    var n = origen.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + origen[i].cod + '">' + origen[i].nombre + '</option>';
    }
    $('#cboOrigen').html(str);
}


function fnSelectTipoDoc() {

    if (tipodocumento.length == 0) {
        var arr = fnTipoDocumento(1, "", "");
        for (i = 0; i < arr.length; i++) {
            tipodocumento.push({
                cod: arr[i].cod,
                nombre: arr[i].nombre
            });
        }
    }

    var n = tipodocumento.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + tipodocumento[i].cod + '">' + tipodocumento[i].nombre + '</option>';
    }
    $('#cboTipoDocumento').html(str);
}


function fnReg() {
    if (Region.length == 0) {
        var arr = fnRegion(1, "TO", '');
        for (i = 0; i < arr.length; i++) {
            Region.push({
                cod: arr[i].cod,
                nombre: arr[i].nombre
            });
        }
    }

    var n = Region.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        if (Region[i].nombre == "Lambayeque") {
            str += '<option value="' + Region[i].cod + '" selected="selected">' + Region[i].nombre + '</option>';
        } else {
            str += '<option value="' + Region[i].cod + '">' + Region[i].nombre + '</option>';
        }

    }
    $('#cboRegionD').html(str);
    $('#cboRegionIE').html(str);
}

function fnProv() {

    var arr = fnProvincia(1, "ES", $('#cboRegionD').val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    $('#cboDistritoD').html(str);
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboProvinciaD').html(str);
}

function fnDis() {
    var arr = fnDistrito(1, "ES", $('#cboProvinciaD').val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboDistritoD').html(str);
}

function fnCpf(cod_eve, cod_test) {

    var arr = fnCarrera(1, 'LXE', cod_eve, cod_test);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboCarrera').html(str);
}

function fnCargaFiltros() {
    var vars = [];
    var filtros = window.location.href.slice(window.location.href.indexOf('?') + 1);
    //    console.log(filtros)
    filtros = filtros.split('|');
    if (filtros.length > 1) {
        $("#cboConvocatoria").val(filtros[1]);
        $('#cboConvocatoria').trigger('change');

        var codigoCon = $('#cboConvocatoria').val();
        // fnSelectEvento(codigoTest, codigoCon);

        $.each(filtros[2].split(','), function (i, e) {
            $("#cboEvento option[value='" + e + "']").prop('selected', true)
        });
        $("#cboEvento").selectpicker('refresh');

        $("#txttexto").val(filtros[3])
        $("#cboFiltroOrigen").val(filtros[4])

        $.each(filtros[5].split(','), function (i, e) {
            $("#cboGrados option[value='" + e + "']").prop('selected', true)
        });
        $("#cboGrados").selectpicker('refresh');

        $("#cboTipoPersona").val(filtros[6])
        $("#cboComunicacion").val(filtros[7])
        $("#cboAcuerdo").val(filtros[8])
        $("#txtFechaAcuerdo").val(filtros[9])
        $("#chkMisAcuerdos").prop('checked', filtros[10] == 'on')

        $.each(filtros[11].split(','), function (i, e) {
            $("#cboCarreraProfesional option[value='" + e + "']").prop('selected', true)
        });
        $("#cboCarreraProfesional").selectpicker('refresh');

        $("#cboFiltroPrioridad").val(filtros[12])

        $.each(filtros[13].split(','), function (i, e) {
            $("#cboCentroCosto option[value='" + e + "']").prop('selected', true)
        });
        $("#cboCentroCosto").selectpicker('refresh');

        $("#txtFechaDesde").val(filtros[14])
        $("#txtFechaHasta").val(filtros[15])
        $("#txtletraini").val(filtros[16])
        $("#txtletrafin").val(filtros[17])

        $.each(filtros[19].split(','), function (i, e) {
            $("#cboRequisitoAdmision option[value='" + e + "']").prop('selected', true)
        });
        $("#cboRequisitoAdmision").selectpicker('refresh');

        fnListar(0);
    } else {
        fnLoading(false);
    }

    //    console.log(vars);
}


function fnListar(op) {
    if (op == 1) {
        rpta = fnvalidaSession()
    } else {
        rpta = true;
    }
    if (rpta == true) {
        if (ValidaBusqueda() == true) {
            fnLoading(true);

            var totalResult = [];
            var listarRecursivo = function () {
                $('#frmbuscar').
                    append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />')
                    .append('<input type="hidden" id="requestId" name="requestId" value="' + requestId + '" />');

                var form = $("#frmbuscar").serializeArray();
                $("form#frmbuscar input[id=action]").remove();
                $("form#frmbuscar input[id=requestId]").remove();

                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Interesado.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (response) {
                        totalResult = totalResult.concat(response.result)
                        requestId = response.requestId;

                        if (response.continuar === true) {
                            listarRecursivo()
                        } else {
                            cargarListaInteresados(totalResult)
                        }
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                console.log(result)
                    }
                });
            }
            listarRecursivo();
        }
    } else {
        window.location.href = rpta
    }
}

function cargarListaInteresados(data) {
    fnLimpiarTabla();

    var tb = '';
    var i = 0;
    var filas = data.length;
    for (i = 0; i < filas; i++) {
        tb += '<tr data-cod="' + data[i].cod + '">';
        tb += '<td style="text-align:center">' + (i + 1) + '</td>';
        // tb += '<td style="text-align:center">' + data[i].tdoc + '</td>';
        tb += '<td>' + data[i].ndoc + '</td>';
        tb += '<td >' + data[i].apepat + '</td>';
        tb += '<td >' + data[i].apemat + '</td>';
        tb += '<td >' + data[i].nom + '</td>';
        tb += '<td >' + data[i].situ + '</td>';
        tb += '<td >' + data[i].carr + '</td>';
        tb += '<td>';

        var barClass = '';
        var porcentaje = data[i].porcen * 100;

        switch (true) {
            case (porcentaje <= 25):
                barClass = 'progress-bar-danger'
                break;
            case (porcentaje <= 50):
                barClass = 'progress-bar-warning'
                break;
            case (porcentaje <= 75):
                barClass = 'progress-bar-info'
                break;
            default:
                barClass = 'progress-bar-success'
                break;
        }

        tb += '<div class="progress-bar ' + barClass + '" role="progressbar" aria-valuenow="' + porcentaje + '" aria-valuemin="0" aria-valuemax="100" style="width: ' + porcentaje + '%;">' + porcentaje + '%</div>';
        tb += '<td >' + data[i].ecom + '</td>';
        tb += '<td >' + data[i].feccom + '</td>';
        tb += '<td >' + data[i].acrd + '</td>';
        tb += '<td >' + data[i].reqf + '</td>';
        tb += '<td >' + data[i].fecreg + '</td>';
        tb += '<td style="text-align:center">';
        tb += '<button type="button" id="btnEditar" name="btnEditar" class="btn btn-sm btn-info"  title="Editar" onclick="fnEditarInteresado(\'' + data[i].ctdoc + '\',\'' + data[i].ndoc + '\',\'' + data[i].codeve + '\')" ><i class="ion-edit"></i></button>';
        tb += '<button type="button" id="btnProfile" name="btnProfile" class="btn btn-sm btn-orange"  title="Perfil Interesado" onclick="fnPerfil(\'' + data[i].cod + '\')" ><i class="ion-ios-person"></i></button>';
        tb += '</td>';
        tb += '</tr>';
    }

    var eventosSeleccionados = $("#cboEvento option:selected").length;
    if (eventosSeleccionados == 1) {
        $("#btnAgregar").attr("style", "display:initial");
        $("#codeve").val($("#cboEvento").val());
    } else {
        $("#btnAgregar").attr("style", "display:none");
        $("#codeve").val("0");
    }
    // if ($("#cboEvento option:selected").text() == 'TODOS' || $("#cboEvento option:selected").val() == '') {
    //     $("#btnAgregar").attr("style", "display:none");
    //     $("#codeve").val("0");
    // } else {
    //     $("#btnAgregar").attr("style", "display:initial");
    //     $("#codeve").val($("#cboEvento").val());
    // }

    if (filas > 0) {
        $("#btnExportar").attr("style", "display:initial");
    } else {
        $("#btnExportar").attr("style", "display:none");
    }

    fnDestroyDataTableDetalle('tInteresados');
    $('#tbInteresados').html(tb);

    var sDom = '<"top"iflp>rt<"bottom"ip><"clear">';
    fnResetDataTableBasic('tInteresados', 0, 'asc', 100, sDom, function () {
        fnLoading(false);
    });
}

function ValidaBusqueda() {
    if ($("#cboTipoEstudio").val() == "") {
        fnMensaje("error", "Selecione Tipo de Estudio");
        return false
    }
    if ($("#cboConvocatoria").val() == "") {
        fnMensaje("error", "Selecione Una Convocatoria");
        return false
    }
    return true
}


function fnValidaBuscaxTipoyNumDoc() {

    if ($("#cboTipoDocumento").val() == "") {
        //        fnMensajeDiv("FormMensaje", "danger", "Selecione Tipo de Documento");
        fnMensaje("error", "Selecione Tipo de Documento");
        $("#cboTipoDocumento").focus();
        return false;
    } else {
        if ($("#txtnum_doc").val() == "") {
            //            fnMensajeDiv("FormMensaje", "danger", "Ingrese el N° de Documento.");
            fnMensaje("error", "Ingrese el N° de Documento.");
            $("#txtnum_doc").focus();
            return false;
        } else {
            var numero = $("#txtnum_doc").val();
            var opcion = $("#cboTipoDocumento option:selected").text();
            // Para DNI
            if (opcion == "DOC. NACIONAL DE IDENTIDAD") {
                if (isNaN(numero) || numero % 1 != 0 || numero <= 0) {
                    //                    fnMensajeDiv("FormMensaje", "danger", "Solo se Aceptan Numeros Enteros.");
                    fnMensaje("error", "Solo se Aceptan Numeros Enteros.");
                    $("#txtnum_doc").focus();
                    return false;
                } else {
                    if (numero.length != 8) {
                        //                        fnMensajeDiv("FormMensaje", "danger", "Longitud de Documento es de 8 Digitos.");
                        fnMensaje("error", "Longitud de Documento es de 8 Digitos.");
                        $("#txtnum_doc").focus();
                        return false;
                    } else {
                        return true;
                    }
                }
            }
            // Para RUC
            if (opcion == "REG. ÚNICO DE CONTRIBUYENTES") {
                if (isNaN(numero) && numero % 1 != 0 || numero <= 0) {
                    //                    fnMensajeDiv("FormMensaje", "danger", "Solo se Aceptan Numeros Enteros.");
                    fnMensaje("error", "Solo se Aceptan Numeros Enteros.");
                    $("#txtnum_doc").focus();
                    return false;
                } else {
                    if (numero.length != 11) {
                        //fnMensajeDiv("FormMensaje", "danger", "Longitud de Documento es de 11 Digitos.");
                        fnMensaje("error", "Longitud de Documento es de 11 Digitos.");
                        $("#txtnum_doc").focus();
                        return false;
                    } else {
                        return true;
                    }
                }
            }
            // Para PASAPORTE O CARNET
            if (opcion == "PASAPORTE" || opcion == "CARNET DE EXTRANJERÍA") {
                if (numero.length > 12) {
                    //                    fnMensajeDiv("FormMensaje", "danger", "Longitud Máxima del Documento es de 12 Digitos.");
                    fnMensaje("error", "Longitud Máxima del Documento es de 12 Digitos.");
                    $("#txtnum_doc").focus();
                    return false;
                } else {
                    return true;
                }
            }
            // Para PASAPORTE O CARNET
            if (opcion == "PARTIDA DE NACIMIENTO") {
                if (numero.length > 15) {
                    //                    fnMensajeDiv("FormMensaje", "danger", "Longitud Máxima del Documento es de 15 Digitos.");
                    fnMensaje("error", "Longitud Máxima del Documento es de 15 Digitos.");
                    $("#txtnum_doc").focus();
                    return false;
                } else {
                    return true;
                }
            }
            //
        }
    }
}

function fnBuscaxTipoyNumDoc() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        // $("#mdRegistro").hide();
        $("#txtapepat").removeAttr("disabled")
        $("#txtapemat").removeAttr("disabled")
        $("#txtnombre").removeAttr("disabled")
        //$("#txtfecnac").removeAttr("disabled")
        $("input#date").removeAttr("disabled")
        $("#cboTipoDocumento").removeAttr("disabled");
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.btnd + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        //            console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Interesado.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                // console.log(data);
                if (data.length > 0) {
                    $("#hdcod_i").val(data[0].cod);

                    if (!$("#btnInscribir").text().includes("Inscribir")) {
                        $("#cboOrigen").val(data[0].cOri);
                    }

                    $("#chkConfirmado").prop("checked", data[0].cNdiConf);
                    $("#txtapepat").val(data[0].cApi);
                    $("#txtapemat").val(data[0].cAmi);
                    $("#txtnombre").val(data[0].cNi);
                    $("input#date").val(data[0].cFn)
                    $("#cboGradoEstudios").val(data[0].grado);
                    $("#txtAnioEgreso").val(data[0].anioEgre);
                    $("#codie").val(data[0].cied);
                    $("#txtie").val(data[0].ied)
                    $("#txtdir").val(data[0].dir)
                    $("#txttel").val(data[0].tlv)
                    $("#txtema").val(data[0].ema)
                    $("#codcpf").val(data[0].ccp)
                    $("#txtcp").val(data[0].cp)
                    NuevoInteresado()
                    //$("#cboTipoDocumento").attr("disabled", "disabled");
                    if ($("#hdtip").val() == 'N') {
                        $("#txtnum_doc").attr("readonly", "readonly");
                    }
                } else {
                    //                        fnMensajeDiv("FormMensajeBusq", "danger", "No se Encontrarón Coincidencias.");
                    fnMensaje("error", "No se Encontrarón Coincidencias.");
                    $("#chkConfirmado").prop("checked", true);
                    $("#btnCoincidencias").removeAttr("disabled");
                    $("#btnCoincidencias").removeAttr("style")
                    $("#cboGradoEstudios").attr("disabled", "disabled");
                    $("#btnie").attr("disabled", "disabled");
                    $("#btndir").attr("disabled", "disabled");
                    $("#btntel").attr("disabled", "disabled");
                    $("#btnema").attr("disabled", "disabled");
                    $("#btncp").attr("disabled", "disabled");
                    $("#btnCuestionario").attr("disabled", "disabled");
                    $("#btnInscribir").attr("disabled", "disabled");
                    $("#btnInscribirYRedirigir").attr("disabled", "disabled");
                    $("#cboGradoEstudios").val("");
                    $("#codie").val("0");
                    $("#txtapepat").val("");
                    $("#txtapemat").val("");
                    $("#txtnombre").val("");
                    $("#date").val("");
                    $("#txtie").val("");
                    $("#txtdir").val("");
                    $("#txttel").val("");
                    $("#txtema").val("");
                    $("#codcpf").val("0");
                    $("#txtcp").val("");

                    fnDestroyDataTableDetalle('tCoincidencias');
                    $('#TbCoincidencias').html('');
                    fnResetDataTableBasic('tCoincidencias', 2, 'asc', 5);

                    $("#txtapepat").focus();
                }
                fnLoading(false);
                //$("#mdRegistro").show();
            },
            error: function (result) {
                fnLoading(false);
                $("#frmRegistro").show();
                //                    console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }


}


function fnBuscaxApeyNom() {
    if ($("#txtapepat").val() == "" && $("#txtapemat").val() == "" && $("#txtnombre").val() == "") {
        //        fnMensajeDiv("FormMensajeBusq", "danger", "Ingrese Al menos uno de los campos de Búsqueda.");
        fnMensaje("error", "Ingrese Al menos uno de los campos de Búsqueda.");
    } else {
        fnLoading(true);
        //$("#mdRegistro").hide();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.bcon + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        //        console.log(form)
        $("#Coincidencias").removeAttr("style");
        $("#Coincidencias").attr("style", "display:block")
        $("#detalle_int").removeAttr("style");
        $("#detalle_int").attr("style", "display:none")
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Interesado.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                if (filas > 0) {
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + data[i].tdoc + '</td>';
                        tb += '<td>' + data[i].ndoc + '</td>';
                        tb += '<td >' + data[i].apepat + '</td>';
                        tb += '<td >' + data[i].apemat + '</td>';
                        tb += '<td >' + data[i].nom + '</td>';
                        tb += '<td style="text-align:center"><button type="button" id="btns" name="btns" class="btn btn-sm btn-info" onclick="fnSeleccionCoincidencia(\'' + data[i].cod + '\')" title="Seleccionar" ><i class="ion-edit"></i></button></td>';
                        tb += '</tr>';
                    }
                } else {
                    fnMensaje("information", "No se Encontrarón Coincidencias, Se Registrará como nuevo.");
                    NuevoInteresado();
                }
                fnDestroyDataTableDetalle('tCoincidencias');
                $('#TbCoincidencias').html(tb);
                fnResetDataTableBasic('tCoincidencias', 2, 'asc', 5);
                fnLoading(false);
                //$("#mdRegistro").show();
            },
            error: function (result) {
                fnLoading(false);
                $("#mdRegistro").show();
                //                console.log(result)
            }
        });
    }
}


function fnSeleccionCoincidencia(val) {
    fnLoading(true);
    $("#mdRegistro").hide();
    $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.scon + '" />');
    $('#frmRegistro').append('<input type="hidden" id="cod" name="cod" value="' + val + '" />');
    var form = $("#frmRegistro").serializeArray();
    $("form#frmRegistro input[id=action]").remove();
    $("form#frmRegistro input[id=cod]").remove();
    //    console.log(form)
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/Interesado.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function (data) {
            //            console.log(data);
            if (data.length > 0) {
                $("#hdcod_i").val(data[0].cod);
                $("#cboTipoDocumento").val(data[0].cTdi);
                $("#txtnum_doc").val(data[0].cNdi);
                $("#chkConfirmado").prop("checked", data[0].cNdiConf);
                $("#txtapepat").val(data[0].cApi);
                $("#txtapemat").val(data[0].cAmi);
                $("#txtnombre").val(data[0].cNi);
                $("input#date").val(data[0].cFn)
                $("#cboGradoEstudios").val(data[0].grado);
                $("#codie").val(data[0].cied);
                $("#txtie").val(data[0].ied);
                $("#txtdir").val(data[0].dir);
                $("#txttel").val(data[0].tel);
                $("#txtema").val(data[0].ema);
                $("#codcpf").val(data[0].ccp)
                $("#txtcp").val(data[0].cp);
                $("#btnCoincidencias").attr("disabled", "disabled");
                $("#btnCoincidencias").attr("style", "display:none;");
                //$("#cboTipoDocumento").attr("disabled", "disabled");
                $("#txtnum_doc").attr("readonly", "readonly");
                $("#Coincidencias").removeAttr("style");
                $("#Coincidencias").attr("style", "display:none")
                $("#detalle_int").removeAttr("style");
                $("#detalle_int").attr("style", "display:block")
                $("#date").removeAttr("disabled");
                $("#cboGradoEstudios").removeAttr("disabled");
                $("#btnie").removeAttr("disabled");
                $("#btndir").removeAttr("disabled");
                $("#btntel").removeAttr("disabled");
                $("#btnema").removeAttr("disabled");
                $("#btncp").removeAttr("disabled");
                $("#btnCuestionario").removeAttr("disabled");
                $("#btnInscribir").removeAttr("disabled");
                $("#btnInscribirYRedirigir").removeAttr("disabled");
            } else {
                //fnMensajeDiv("FormMensaje", "danger", "No se Encontrarón Coincidencias.");
                //                $("#btnCoincidencias").removeAttr("disabled");
                //                $("#btnCoincidencias").removeAttr("style")
            }
            fnLoading(false);
            $("#mdRegistro").show();
        },
        error: function (result) {
            fnLoading(false);
            $("#mdRegistro").show();
            //            console.log(result)
        }
    });
}

function fnBuscaIE() {
    if ($("#cboRegionIE").val !== "") {
        fnLoading(true);
        //        $("#mdRegistro").hide();
        $('#frmRegistroIE').append('<input type="hidden" id="action" name="action" value="' + ope.bie + '" />');
        var form = $("#frmRegistroIE").serializeArray();
        $("form#frmRegistroIE input[id=action]").remove();
        //        console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Interesado.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td>' + data[i].nom + '</td>';
                    tb += '<td >' + data[i].dir + '</td>';
                    tb += '<td >' + data[i].dis + ' / ' + data[i].prov + ' / ' + data[i].reg + '</td>';
                    //                    tb += '<td >' +  + '</td>';
                    //                    tb += '<td >' +  + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnsIE" name="btnsIE" class="btn btn-sm btn-info" onclick="fnSeleccionIE(\'' + data[i].cod + '\',\'' + data[i].nom + '\')" data-dismiss="modal" title="Seleccionar" ><i class="ion-edit"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tInstitucionEducativa');
                $('#tbInstitucionEducativa').html(tb);
                fnResetDataTableBasic('tInstitucionEducativa', 0, 'asc', 10);

                fnLoading(false);
                //                $("#mdRegistro").show();
            },
            error: function (result) {
                fnLoading(false);
                //                $("#mdRegistro").show();
                //                console.log(result)
            }
        });
    }
}
function fnSeleccionIE(cod_ie, nom_ie) {
    $("#txtie").val(nom_ie);
    $("#codie").val(cod_ie);
    $("#cboGradoEstudios").focus();
    $("#mdInstitucionEd").modal("hide");
}

function validaregistro() {
    if (fnValidaBuscaxTipoyNumDoc() == true) {
        if ($('#cboOrigen').val().trim() == '') {
            fnMensaje("error", 'Debe seleccionar un origen');
            $('#cboOrigen').focus();
            return false;
        }
        if ($("#txtapepat").val() == "") {
            //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Apellido Paterno de Interesado.");
            fnMensaje("error", "Ingrese Apellido Paterno de Interesado.");
            $("#txtapepat").focus();
            return false;
        }
        if ($("#txtapemat").val() == "") {
            //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Apellido Materno de Interesado.");
            fnMensaje("error", "Ingrese Apellido Materno de Interesado.");
            $("#txtapemat").focus();
            return false;
        }
        if ($("#txtnombre").val() == "") {
            //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Nombres de Interesado.");
            $("#txtnombre").focus();
            fnMensaje("error", "Ingrese Nombres de Interesado.");
            return false;
        }

        /*
        if ($("input#date").val() == "") {
        //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Fecha de Nacimiento Interesado.");
        fnMensaje("error", "Ingrese Fecha de Nacimiento Interesado.");
        return false;
        }
        */
        if ($("input#date").val() != "") {
            var fecha = $("input#date").val()
            var arrFecha = fecha.split("/")
            if (arrFecha.length == 3) {
                if (arrFecha[0].length != 2) {
                    //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Correctamente Fecha de Nacimiento.");
                    fnMensaje("error", "Ingrese Correctamente Fecha de Nacimiento.");
                    $("#date").focus();
                    return false;
                }
                if (arrFecha[1].length != 2) {
                    //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Correctamente Fecha de Nacimiento.");
                    fnMensaje("error", "Ingrese Correctamente Fecha de Nacimiento.");
                    $("#date").focus();
                    return false;
                }
                if (arrFecha[2].length != 4) {
                    //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Correctamente Fecha de Nacimiento.");
                    $("#date").focus();
                    fnMensaje("error", "Ingrese Correctamente Fecha de Nacimiento.");
                    return false;
                }
            } else {
                $("#date").focus();
                fnMensaje("error", "Ingrese Correctamente Fecha de Nacimiento.");
                return false;
            }
        }


        if ($("#cboGradoEstudios").val() == "") {
            //            fnMensajeDiv("FormMensaje", "danger", "Ingrese Nombres de Interesado.");
            fnMensaje("error", "Seleccione el Grado de Estudios.");
            $("#cboGradoEstudios").focus();
            return false;
        }

        if (!seleccionandoCarrera && $('#txtcp').val().trim() == '') {
            fnMensaje("error", 'Debe seleccionar al menos una carrera profesional');
            $('#btncp').focus();
            return false;
        }

        return true;
    } else {
        return false;
    }
}

function validaDuplicados() {
    if (validaregistro() == true) {
        $("form#frmRegistro input[id=action]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.vdup + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        //        console.log(form);
        var res = false;
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Interesado.aspx",
            data: form,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                //                console.log(data);
                if (data[0].cont == 0) {
                    res = true;
                } else {
                    //                fnMensajeDiv("FormMensaje", "danger", "Interesado ya se encuentra Registrado.");
                    fnMensaje("error", "Interesado ya se encuentra Registrado.");
                    res = false;
                }
                //                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
        return res;
    }
}


function RegistrarInteresado() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (validaDuplicados() == true) {
            fnLoading(true);
            $("form#frmRegistro input[id=action]").remove();
            $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
            var form = $("#frmRegistro").serializeArray();
            $("form#frmRegistro input[id=action]").remove();
            //            console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/crm/Interesado.aspx",
                data: form,
                dataType: "json",
                async: false,
                cache: false,
                success: function (data) {
                    //                    console.log(data);
                    if (data[0].rpta == 1) {
                        $("#hdcod_i").val(data[0].cod)
                        fnMensaje("success", data[0].msje)
                        //                    Limpiar()
                        //                    fnListar();
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                    fnLoading(false);
                },
                error: function (result) {
                    fnLoading(false);
                    //                    console.log(result)
                }
            });
        }
    } else {
        window.location.href = rpta
    }
}

function NuevoInteresado() {
    $("#btnCoincidencias").removeAttr("style");
    $("#btnCoincidencias").attr("style", "display:none;");
    $("#Coincidencias").removeAttr("style");
    $("#Coincidencias").attr("style", "display:none")
    $("#detalle_int").removeAttr("style");
    $("#detalle_int").attr("style", "display:block")
    $("#date").removeAttr("disabled");
    $("#cboGradoEstudios").removeAttr("disabled");
    $("#btnie").removeAttr("disabled");
    $("#btndir").removeAttr("disabled");
    $("#btntel").removeAttr("disabled");
    $("#btnema").removeAttr("disabled");
    $("#btncp").removeAttr("disabled");
    $("#btnCuestionario").removeAttr("disabled");
    $("#btnInscribir").removeAttr("disabled");
    $("#btnInscribirYRedirigir").removeAttr("disabled");
    $("#btnie").focus();
}

function fnInscribir(redirigir) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //        fnLoading(true);
        if ($("#btnInscribir").text().includes("Inscribir")) {
            if ($("#hdcod_i").val() == 0) {
                if (validaDuplicados() == true) {
                    $("form#frmRegistro input[id=action]").remove();
                    $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                    var form = $("#frmRegistro").serializeArray();
                    $("form#frmRegistro input[id=action]").remove();
                    //                    console.log(form);

                    $.ajax({
                        type: "POST",
                        url: "../DataJson/crm/Interesado.aspx",
                        data: form,
                        dataType: "json",
                        cache: false,
                        success: function (data) {
                            //                            console.log(data);
                            if (data[0].rpta == 1) {
                                $("#hdcod_i").val(data[0].cod)
                                //                            fnMensaje("success", data[0].msje)
                                $("form#frmRegistro input[id=action]").remove();
                                //                            $("form#frmRegistro input[id=hdcod_i]").remove();
                                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.ins + '" />');
                                //                            $('#frmRegistro').append('<input type="hidden" id="hdcod_i" name="hdcod_i" value="' + data[0].cod + '" />');
                                var form1 = $("#frmRegistro").serializeArray();
                                $("form#frmRegistro input[id=action]").remove();
                                //                                console.log(form1);
                                $.ajax({
                                    type: "POST",
                                    url: "../DataJson/crm/Interesado.aspx",
                                    data: form1,
                                    dataType: "json",
                                    cache: false,
                                    success: function (data1) {
                                        //                                        console.log(data1);
                                        if (data1[0].rpta == 1) {
                                            $.ajax({
                                                type: "POST",
                                                url: "../DataJson/crm/Interesado.aspx",
                                                data: {
                                                    action: ope.mrk,
                                                    codigoInt: data[0].cod,
                                                    nombreOri: $('#cboOrigen option:selected').text()
                                                },
                                                dataType: "json",
                                                cache: false,
                                                success: function (data2) {
                                                    if (data2[0].rpta == 1) {
                                                        if (redirigir) {
                                                            fnPerfil($("#hdcod_i").val());
                                                        } else {
                                                            $("#mdRegistro").modal("hide");
                                                            fnMensaje("success", data1[0].msje)
                                                            fnListar(0);
                                                        }
                                                    } else {
                                                        fnMensaje("error", data[0].msje + " / " + data1[0].msje + " / " + data2[0].msje);
                                                    }
                                                },
                                                error: function (err) {
                                                    fnLoading(false);
                                                }
                                            });

                                        } else {
                                            fnMensaje("error", data[0].msje + " / " + data1[0].msje);
                                        }

                                        fnLoading(false);
                                    },
                                    error: function (result) {
                                        fnLoading(false);
                                        //                                        console.log(result)
                                    }
                                });

                            } else {
                                fnMensaje("error", data[0].msje)
                            }
                            fnLoading(false);
                        },
                        error: function (result) {
                            fnLoading(false);
                            //                            console.log(result)
                        }
                    });
                }
                fnLoading(false);
            } else {
                if (!validaregistro()) {
                    return false;
                }

                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Interesado.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_i").val(data[0].cod)
                            //                        fnMensaje("success", data[0].msje)
                            $("form#frmRegistro input[id=action]").remove();
                            $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.ins + '" />');
                            var form1 = $("#frmRegistro").serializeArray();
                            $("form#frmRegistro input[id=action]").remove();
                            //                            console.log(form1);
                            $.ajax({
                                type: "POST",
                                url: "../DataJson/crm/Interesado.aspx",
                                data: form1,
                                dataType: "json",
                                cache: false,
                                success: function (data1) {
                                    //                                    console.log(data1);
                                    if (data1[0].rpta == 1) {

                                        $.ajax({
                                            type: "POST",
                                            url: "../DataJson/crm/Interesado.aspx",
                                            data: {
                                                action: ope.mrk,
                                                codigoInt: data[0].cod,
                                                nombreOri: $('#cboOrigen option:selected').text()
                                            },
                                            dataType: "json",
                                            cache: false,
                                            success: function (data2) {
                                                if (data2[0].rpta == 1) {
                                                    if (redirigir) {
                                                        fnPerfil($("#hdcod_i").val());
                                                    } else {
                                                        $("#mdRegistro").modal("hide");
                                                        fnMensaje("success", data1[0].msje)
                                                        fnListar(0);
                                                    }
                                                } else {
                                                    fnMensaje("error", data[0].msje + " / " + data1[0].msje + " / " + data2[0].msje);
                                                }
                                            },
                                            error: function (err) {
                                                fnLoading(false);
                                            }
                                        });


                                        // fnMensaje("success", data1[0].msje)
                                        // $("#mdRegistro").modal("hide");
                                        // fnListar(0);
                                    } else {
                                        //                                    fnMensajeDiv("FormMensaje", "danger", data1[0].msje);
                                        fnMensaje("error", data1[0].msje);
                                    }
                                    fnLoading(false);
                                },
                                error: function (result) {
                                    fnLoading(false);
                                    //                                    console.log(result)
                                }
                            });

                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
                fnLoading(false);
            }
        } else {  // Editar
            if (validaregistro() == true) {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Interesado.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_i").val(data[0].cod)
                            fnMensaje("success", data[0].msje)
                            $("#mdRegistro").modal("hide")
                            fnListar(0);
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            }
            fnLoading(false);
        }
    } else {
        window.location.href = rpta
    }
}

function Limpiar(tipo) {
    if (tipo == 1) {
        $("#cboTipoDocumento").prop('selectedIndex', 0);
    }
    $("#txtnum_doc").val("");
    $("#chkConfirmado").prop("checked", true);
    $("#txtnum_doc").removeAttr("maxlength");
    $("#txtapepat").val("");
    $("#txtapepat").attr("disabled", "disabled");
    $("#txtapemat").val("");
    $("#txtapemat").attr("disabled", "disabled");
    $("#txtnombre").val("");
    $("#txtnombre").attr("disabled", "disabled");
    $("#cboGradoEstudios").val("");
    $("#cboGradoEstudios").attr("disabled", "disabled");
    $("#txtAnioEgreso").val("");
    $("#date").val("");
    $("#date").attr("disabled", "disabled");
    $("#txtie").val("");
    $("#txtdir").val("");
    $("#txttel").val("");
    $("#txtema").val("");
    $("#codcpf").val("0");
    $("#txtcp").val("");
    $("#hdcod_i").val("0");
}

function fnPerfil2(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=cod_int]").remove();
        $("#frmbuscar").append('<input type="hidden" id="cod_int"  name="cod_int" value="' + cod + '" />')
        $("#frmbuscar").append('<input type="hidden" id="action"  name="action" value="' + ope.pint + '" />')
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=cod_int]").remove();
        $("form#frmbuscar input[id=action]").remove();
        //        console.log(form);

        $.post('FrmListaInformacionInteresado.aspx', form, function (data, status) {
            if (status == 'success') {

                // $("#divContent").html(data);
                $("#pagina").empty()
                $("#pagina").html(data);
                $("#mdPerfilInteresado").modal("show")
            }
            //else {
            //            $("#divContent").html("");
            //            location.reload();
            //        }
            fnLoading(false);
        });

    } else {
        window.location.href = rpta
    }
}


function fnPerfil(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmbuscar input[id=action]").remove();
        $("form#frmbuscar input[id=cod_int]").remove();
        $("#frmbuscar").append('<input type="hidden" id="cod_int"  name="cod_int" value="' + cod + '" />')
        $("#frmbuscar").append('<input type="hidden" id="action"  name="action" value="' + ope.pint + '" />')
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=cod_int]").remove();
        $("form#frmbuscar input[id=action]").remove();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Interesado.aspx",
            data: form,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                //                console.log(data);
                if (data[0].msje == true) {
                    window.location.href = data[0].link
                    //$("#pagina")
                } else {
                    //                    fnMensaje("error", data[0].msje)
                }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


function fnEditarInteresado(tdoc, numdoc, cod_eve) {
    $("#hdtip").val("E");
    fnSelectOrigen();
    fnSelectTipoDoc();
    fnReg();
    $("#btnInscribir").text("Guardar")
    $("#btnInscribirYRedirigir").hide();
    $('#btnEliminarInteresado').show();
    $('#mdRegistro').modal("show");
    $("#cboTipoDocumento").val(tdoc);
    $("#txtnum_doc").val(numdoc);
    $("#codeve").val(cod_eve)
    fnBuscaxTipoyNumDoc();
    $("#btnInscribir").text("Guardar")
    $("#detalle_int").find("input, text").not('#txtAnioEgreso').attr("disabled", "disabled")
    $("#date").removeAttr("disabled")
    $("#txtnum_doc").removeAttr("readonly")
    var opcion = $("#cboTipoDocumento option:selected").text()
    // Para DNI
    if (opcion == "DOC. NACIONAL DE IDENTIDAD") {
        $("#txtnum_doc").attr("maxlength", "8");
    } else {
        if (opcion == "REG. ÚNICO DE CONTRIBUYENTES") {
            $("#txtnum_doc").attr("maxlength", "11");
        } else {
            if (opcion == "PASAPORTE" || opcion == "CARNET DE EXTRANJERÍA") {
                $("#txtnum_doc").attr("maxlength", "12");
            } else {
                $("#txtnum_doc").attr("maxlength", "15");
            }
        }
    }

}

/*============= DIRECCION ===================*/

function validaRegistroDireccion() {
    if ($("#cboRegionD").val() == "") {
        //        fnMensajeDiv("FormMensajeD", "danger", "Seleccione Región.");
        fnMensaje("error", "Seleccione Región.");
        $("#cboRegionD").focus();
        return false;
    }
    if ($("#cboProvinciaD").val() == "") {
        //        fnMensajeDiv("FormMensajeD", "danger", "Seleccione Provincia.");
        fnMensaje("error", "Seleccione Provincia.");
        $("#cboProvinciaD").focus();
        return false;

    }
    if ($("#cboDistritoD").val() == "") {
        //        fnMensajeDiv("FormMensajeD", "danger", "Seleccione Distrito.");
        fnMensaje("error", "Seleccione Distrito.");
        $("#cboDistritoD").focus();
        return false;
    }
    if ($("#txtDireccion").val() == "") {
        //        fnMensajeDiv("FormMensajeD", "danger", "Ingrese Dirección.");
        fnMensaje("error", "Ingrese Dirección.");
        $("#txtDireccion").focus();
        return false;
    }
    return true;
}

function fnListarDirecciones() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmRegistroD input[id=hdcodiD]").remove();
        $("form#frmRegistroD input[id=action]").remove();
        $('#frmRegistroD').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmRegistroD').append('<input type="hidden" id="hdcodiD" name="hdcodiD" value="' + $("#hdcod_i").val() + '" />');
        var form = $("#frmRegistroD").serializeArray();
        $("form#frmRegistroD input[id=action]").remove();
        $("form#frmRegistroD input[id=hdcodiD]").remove();
        //        console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Direccion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + data[i].dir + '</td>';
                    tb += '<td >' + data[i].dep + '</td>';
                    tb += '<td >' + data[i].pro + '</td>';
                    tb += '<td >' + data[i].dis + '</td>';
                    tb += '<td >' + data[i].fec + '</td>';
                    tb += '<td align="center">';
                    if (data[i].vig == 1) {
                        tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    } else {
                        tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    }
                    tb += '<td style="text-align:center"><button type="button" id="btnED" name="btnED" onclick="EditarDireccion(\'' + data[i].cod + '\')" class="btn btn-sm btn-info"  title="Editar" ><i class="ion-edit"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tDirecciones');
                $('#tbDirecciones').html(tb);
                fnResetDataTableBasic('tDirecciones', 0, 'asc', 5);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


function fnGuardarDireccion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (validaRegistroDireccion() == true) {
            fnLoading(true);
            if ($("#hdcod_D").val() == 0) {
                $("form#frmRegistroD input[id=hdcodiD]").remove();
                $("form#frmRegistroD input[id=action]").remove();
                $('#frmRegistroD').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmRegistroD').append('<input type="hidden" id="hdcodiD" name="hdcodiD" value="' + $("#hdcod_i").val() + '" />');
                var form = $("#frmRegistroD").serializeArray();
                $("form#frmRegistroD input[id=action]").remove();
                $("form#frmRegistroD input[id=hdcodiD]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Direccion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_D").val(data[0].cod)
                            //                            fnMensajeDiv("FormMensajeD", "success", data[0].msje)
                            fnMensaje("success", data[0].msje)
                            fnListarDirecciones()
                            if ($("#chkVigenciaD").prop("checked") == true) {
                                $("#txtdir").val($("#txtDireccion").val());
                            }
                            LimpiarDireccion()
                            $("#mdDireccion").modal("hide");
                            $("#btntel").focus();
                            //                    fnListar();
                        } else {
                            //                        fnMensajeDiv("FormMensajeD", "warning", data[0].msje)
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            } else {
                $("form#frmRegistroD input[id=action]").remove();
                $("form#frmRegistroD input[id=hdcodiD]").remove();
                $('#frmRegistroD').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistroD').append('<input type="hidden" id="hdcodiD" name="hdcodiD" value="' + $("#hdcod_i").val() + '" />');
                var form = $("#frmRegistroD").serializeArray();
                $("form#frmRegistroD input[id=action]").remove();
                $("form#frmRegistroD input[id=hdcodiD]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Direccion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $('#hdcod_D').val(0);
                            //                            fnMensajeDiv("FormMensajeD", "success", data[0].msje)
                            fnMensaje("success", data[0].msje)
                            fnListarDirecciones()
                            if ($("#chkVigenciaD").prop("checked") == true) {
                                $("#txtdir").val($("#txtDireccion").val());
                            }
                            LimpiarDireccion()
                        } else {
                            //                        fnMensajeDiv("FormMensajeD", "warning", data[0].msje)
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function (result) {
                        //            console.log(result)
                        //                    fnMensajeDiv("FormMensajeD", "warning", result);
                        fnMensaje("error", result);
                    }
                });
            }
        }
    } else {
        window.location.href = rpta
    }
}

function LimpiarDireccion(tipo) {
    fnReg();
    fnProv();
    $("#txtDireccion").val("");
    $("#chkVigenciaD").prop("checked", true);
    $("#hdcod_D").val("0")
}

function EditarDireccion(cd) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("#hdcod_D").val(cd)
        $("form#frmRegistroD input[id=action]").remove();
        $('#frmRegistroD').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistroD").serializeArray();
        $("form#frmRegistroD input[id=action]").remove();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Direccion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                $("#cboRegionD").val(data[0].cdep);
                fnProv();
                $('#cboProvinciaD').val(data[0].cpro);
                fnDis();
                $("#cboDistritoD").val(data[0].cdis);
                $("#txtDireccion").val(data[0].dir);
                if (data[0].vig == 1) {
                    $("#chkVigenciaD").prop("checked", true);
                } else {
                    $("#chkVigenciaD").prop("checked", false);
                }
                //                if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
                //            console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}



//======================================== TELEFONO =====================================
function fnListarTelefonos() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmRegistroT input[id=hdcodiT]").remove();
        $("form#frmRegistroT input[id=action]").remove();
        $('#frmRegistroT').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmRegistroT').append('<input type="hidden" id="hdcodiT" name="hdcodiT" value="' + $("#hdcod_i").val() + '" />');
        var form = $("#frmRegistroT").serializeArray();
        $("form#frmRegistroT input[id=action]").remove();
        $("form#frmRegistroT input[id=hdcodiT]").remove();
        //        console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Telefono.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + data[i].tip + '</td>';
                    tb += '<td >' + data[i].nro + '</td>';
                    //tb += '<td >' + data[i].det + '</td>';
                    tb += '<td >' + data[i].fec + '</td>';
                    tb += '<td >' + data[i].nprt + '</td>';
                    tb += '<td align="center">';
                    if (data[i].vig == 1) {
                        tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    } else {
                        tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    }
                    tb += '<td style="text-align:center"><button type="button" id="btnET" name="btnET" onclick="EditarTelefono(\'' + data[i].cod + '\')" class="btn btn-sm btn-info"  title="Editar" ><i class="ion-edit"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tTelefonos');
                $('#tbTelefonos').html(tb);
                fnResetDataTableBasic('tTelefonos', 0, 'asc', 5);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function validaRegistroTelefono() {
    if ($("#cboTipoTelefono").val() == "") {
        //        fnMensaje( "danger", "Seleccione Tipo de Teléfono.");
        fnMensaje("error", "Seleccione Tipo de Teléfono.");
        $("#cboTipoTelefono").focus();
        return false;
    }
    if ($("#txtnumeroTel").val() == "") {
        //        fnMensaje( "danger", "Ingrese número telefónico.");
        $("#txtnumeroTel").focus()
        fnMensaje("error", "Ingrese número telefónico.");
        return false;
    }
    return true;
}

function LimpiarTelefono(tipo) {
    $("#cboTipoTelefono").prop('selectedIndex', 0);
    $("#txtnumeroTel").val("");
    $("#txtdetalleTel").val("");
    $("#chkVigenciaT").prop("checked", true);
    $('#rbtPertenenciaInteresado').prop("checked", true);
    $("#hdcod_T").val("0")
}


function fnGuardarTelefono() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (validaRegistroTelefono() == true) {
            fnLoading(true);
            if ($("#hdcod_T").val() == 0) {
                $("form#frmRegistroT input[id=hdcodiT]").remove();
                $("form#frmRegistroT input[id=action]").remove();
                $('#frmRegistroT').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmRegistroT').append('<input type="hidden" id="hdcodiT" name="hdcodiT" value="' + $("#hdcod_i").val() + '" />');
                var form = $("#frmRegistroT").serializeArray();
                $("form#frmRegistroT input[id=action]").remove();
                $("form#frmRegistroT input[id=hdcodiT]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Telefono.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_T").val(data[0].cod)
                            //                            fnMensaje( "success", data[0].msje)
                            fnMensaje("success", data[0].msje)
                            if ($("#chkVigenciaT").prop("checked") == true) {
                                $("#txttel").val($("#txtnumeroTel").val());
                            }
                            fnListarTelefonos()
                            LimpiarTelefono()
                            $("#mdTelefono").modal("hide");
                            $("#btnema").focus();
                            //                    fnListar();
                        } else {
                            //                        fnMensaje( "warning", data[0].msje)
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            } else {
                $("form#frmRegistroT input[id=hdcodiT]").remove();
                $("form#frmRegistroT input[id=action]").remove();
                $('#frmRegistroT').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistroT').append('<input type="hidden" id="hdcodiT" name="hdcodiT" value="' + $("#hdcod_i").val() + '" />');
                var form = $("#frmRegistroT").serializeArray();
                $("form#frmRegistroT input[id=action]").remove();
                $("form#frmRegistroT input[id=hdcodiT]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/Telefono.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_T").val(data[0].cod)
                            //                            fnMensaje( "success", data[0].msje)
                            fnMensaje("success", data[0].msje)
                            if ($("#chkVigenciaT").prop("checked") == true) {
                                $("#txttel").val($("#txtnumeroTel").val());
                            }
                            fnListarTelefonos()
                            LimpiarTelefono()
                            //                    fnListar();
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            }
        }
    } else {
        window.location.href = rpta
    }
}


function EditarTelefono(cd) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("#hdcod_T").val(cd)
        $("form#frmRegistroT input[id=action]").remove();
        $('#frmRegistroT').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistroT").serializeArray();
        $("form#frmRegistroT input[id=action]").remove();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Telefono.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                $("#cboTipoTelefono").val(data[0].tip);
                $("#txtnumeroTel").val(data[0].nro);
                $("#txtdetalleTel").val(data[0].det);

                switch (data[0].prt) {
                    case 'I':
                        $('#rbtPertenenciaInteresado').prop('checked', true);
                        break;
                    case 'P':
                        $('#rbtPertenenciaPadre').prop('checked', true);
                        break;
                    case 'M':
                        $('#rbtPertenenciaMadre').prop('checked', true);
                        break;
                    case 'A':
                        $('#rbtPertenenciaApoderado').prop('checked', true);
                        break;
                }

                if (data[0].vig == 1) {
                    $("#chkVigenciaT").prop("checked", true);
                } else {
                    $("#chkVigenciaT").prop("checked", false);
                }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
            }
        });
    } else {
        window.location.href = rpta
    }
}



//======================================== EMAIL =====================================
function fnListarEmail() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmRegistroEmail input[id=hdcodiEMail]").remove();
        $("form#frmRegistroEmail input[id=action]").remove();
        $('#frmRegistroEmail').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmRegistroEmail').append('<input type="hidden" id="hdcodiEMail" name="hdcodiEMail" value="' + $("#hdcod_i").val() + '" />');
        var form = $("#frmRegistroEmail").serializeArray();
        $("form#frmRegistroEmail input[id=action]").remove();
        $("form#frmRegistroEmail input[id=hdcodiEMail]").remove();
        //        console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/EMail.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + data[i].tip + '</td>';
                    tb += '<td >' + data[i].des + '</td>';
                    tb += '<td >' + data[i].fec + '</td>';
                    tb += '<td align="center">';
                    if (data[i].vig == 1) {
                        tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    } else {
                        tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    }
                    tb += '<td align="center">';
                    if (data[i].vrf) {
                        tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    } else {
                        tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    }
                    tb += '<td style="text-align:center"><button type="button" id="btnET" name="btnET" onclick="EditarEmail(\'' + data[i].cod + '\')" class="btn btn-sm btn-info"  title="Editar" ><i class="ion-edit"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle('tEMail');
                $('#tbEMail').html(tb);
                fnResetDataTableBasic('tEMail', 0, 'asc', 5);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


function validaRegistroEMail() {
    if ($("#cboTipoEMail").val() == "") {
        fnMensaje("error", "Seleccione Tipo de E-mail.");
        $("#cboTipoEMail").focus();
        return false;
    }
    if ($("#txtDescripcionEMail").val() == "") {
        fnMensaje("error", "Ingrese Correo Electrónico");
        $("#txtDescripcionEMail").focus();
        return false;
    }

    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/i;
    //Se muestra un texto a modo de ejemplo, luego va a ser un icono
    if (emailRegex.test($("#txtDescripcionEMail").val())) {
        return true;
    } else {
        fnMensaje("error", "Ingrese Correctamente Correo Electrónico");
        $("#txtDescripcionEMail").focus();
        return false;

    }
    return true;
}


function LimpiarEMail(tipo) {
    $("#cboTipoEMail").prop('selectedIndex', 0);
    $("#txtDescripcionEMail").val("");
    $("#txtDetalleEMail").val("");
    $("#hdcod_EMail").val("0");
    $("#chkVerificadoEMail").prop("checked", false);
}

function fnValidarEmail() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        var email = $('#txtDescripcionEMail').val().trim();

        if (email == '') {
            fnMensaje("warning", "Debe ingresar un email a verificar");
            $('#txtDescripcionEMail').focus();
            return false;
        }

        var path = "https://apps.emaillistverify.com/api/verifEmail?secret=iofafn3rsgczdpvz1isr3&email=" + email;

        $('#btnValidarEmail').prop('disabled', true);

        $.ajax({
            type: "GET",
            url: path,
            cache: false,
            dataType: 'text',
            success: function (response) {
                if (response == "ok") {
                    fnMensaje("success", "Email válido");
                    $('#chkVerificadoEMail').prop("checked", true);
                } else {
                    fnMensaje("error", "No se ha podido validar el email, mensaje: " + response);
                    $('#chkVerificadoEMail').prop("checked", false);
                }
            },
            error: function (error) {
                console.log(error);
            },
            complete: function () {
                $('#btnValidarEmail').prop('disabled', false);
            }
        });
    }
}


function fnGuardarEMail() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (validaRegistroEMail() == true) {
            fnLoading(true);
            if ($("#hdcod_EMail").val() == 0) {
                $("form#frmRegistroEmail input[id=hdcodiEMail]").remove();
                $("form#frmRegistroEmail input[id=action]").remove();
                $('#frmRegistroEmail').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmRegistroEmail').append('<input type="hidden" id="hdcodiEMail" name="hdcodiEMail" value="' + $("#hdcod_i").val() + '" />');
                $("#chkVerificadoEMail").prop("disabled", false);
                var form = $("#frmRegistroEmail").serializeArray();
                $("form#frmRegistroEmail input[id=action]").remove();
                $("form#frmRegistroEmail input[id=hdcodiEMail]").remove();
                $("#chkVerificadoEMail").prop("disabled", true);
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/EMail.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_EMail").val(data[0].cod)
                            fnMensaje("success", data[0].msje)
                            if ($("#chkVigenciaEMail").prop("checked") == true) {
                                $("#txtema").val($("#txtDescripcionEMail").val());
                            }
                            fnListarEmail()
                            LimpiarEMail()
                            $("#mdEmail").modal("hide");
                            $("#btncp").focus();

                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            } else {
                $("form#frmRegistroEmail input[id=hdcodiEMail]").remove();
                $("form#frmRegistroEmail input[id=action]").remove();
                $('#frmRegistroEmail').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistroEmail').append('<input type="hidden" id="hdcodiEMail" name="hdcodiEMail" value="' + $("#hdcod_i").val() + '" />');
                $("#chkVerificadoEMail").prop("disabled", false);
                var form = $("#frmRegistroEmail").serializeArray();
                $("form#frmRegistroEmail input[id=action]").remove();
                $("form#frmRegistroEmail input[id=hdcodiEMail]").remove();
                $("#chkVerificadoEMail").prop("disabled", true);
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/EMail.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_EMail").val(data[0].cod)
                            fnMensaje("success", data[0].msje)
                            if ($("#chkVigenciaEMail").prop("checked") == true) {
                                $("#txtema").val($("#txtDescripcionEMail").val());
                            }
                            fnListarEmail()
                            LimpiarEMail()
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            }
        }
    } else {
        window.location.href = rpta
    }
}


function EditarEmail(cd) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("#hdcod_EMail").val(cd)
        $("form#frmRegistroEmail input[id=action]").remove();
        $('#frmRegistroEmail').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistroEmail").serializeArray();
        $("form#frmRegistroEmail input[id=action]").remove();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/EMail.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function (data) {
                //                console.log(data);
                $("#cboTipoEMail").val(data[0].tip);
                $("#txtDescripcionEMail").val(data[0].des);
                $("#txtDetalleEMail").val(data[0].det);
                if (data[0].vig == 1) {
                    $("#chkVigenciaEMail").prop("checked", true);
                } else {
                    $("#chkVigenciaEMail").prop("checked", false);
                }
                $("#chkVerificadoEMail").prop("checked", data[0].vrf);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
            }
        });
    } else {
        window.location.href = rpta
    }
}


//======================================== CARRERA PROFESIONAL =====================================
function fnListarCarreras() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmRegistroCP input[id=hdcodiCP]").remove();
        $("form#frmRegistroCP input[id=action]").remove();
        $('#frmRegistroCP').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmRegistroCP').append('<input type="hidden" id="hdcodiCP" name="hdcodiCP" value="' + $("#hdcod_i").val() + '" />');
        var form = $("#frmRegistroCP").serializeArray();
        $("form#frmRegistroCP input[id=action]").remove();
        $("form#frmRegistroCP input[id=hdcodiCP]").remove();
        //        console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/CarreraProfesional.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //                console.log(data);
                $("#txtcp").val("");
                $("#codcpf").val(0);
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                    tb += '<td>' + data[i].ncpf + '</td>';
                    tb += '<td >' + data[i].test + '</td>';
                    tb += '<td >' + data[i].eve + '</td>';
                    tb += '<td >' + data[i].fec + '</td>';
                    tb += '<td align="center">' + data[i].pri + '</td>';
                    //                    if (data[i].vig == 1) {
                    //                    tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" />' + data[i].vig + '</td>';
                    //                    } else {
                    //                        tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                    //                    }
                    tb += '<td style="text-align:center"><button type="button" id="btnCP" name="btnCP" onclick="EditarCarrera(\'' + data[i].cod + '\')" class="btn btn-xs btn-info"  title="Editar" ><i class="ion-edit"></i></button>'
                    tb += '<button type="button" id="btnECP" name="btnECP" onclick="fnDeleteCarrera(\'' + data[i].cod + '\')" class="btn btn-xs btn-red" title="Eliminar" ><i class="ion-android-delete"></i></button>'
                    tb += '</td>';
                    tb += '</tr>';
                    if (i == 0) {
                        $("#txtcp").val(data[i].ncpf)
                        $("#codcpf").val(data[i].cpf)
                    }
                }
                fnDestroyDataTableDetalle('tCarrera');
                $('#tbCarrera').html(tb);
                fnResetDataTableBasic('tCarrera', 0, 'asc', 5);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}


function fnListaPrioridad(hdcod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmRegistroCP input[id=hdcodiCP]").remove();
        $("form#frmRegistroCP input[id=action]").remove();
        $("form#frmRegistroCP input[id=hdcod]").remove();
        $('#frmRegistroCP').append('<input type="hidden" id="action" name="action" value="' + ope.lpr + '" />');
        $('#frmRegistroCP').append('<input type="hidden" id="hdcodiCP" name="hdcodiCP" value="' + $("#hdcod_i").val() + '" />');
        $('#frmRegistroCP').append('<input type="hidden" id="hdcod" name="hdcod" value="' + hdcod + '" />');
        var form = $("#frmRegistroCP").serializeArray();
        $("form#frmRegistroCP input[id=action]").remove();
        $("form#frmRegistroCP input[id=hdcodiCP]").remove();
        $("form#frmRegistroCP input[id=hdcod]").remove();
        //        console.log(form)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/CarreraProfesional.aspx",
            data: form,
            dataType: "json",
            async: false,
            cache: false,
            success: function (data) {
                //                console.log(data);
                var str = '';
                var i = 0;
                var filas = data.length;
                str += '<option value="" selected>-- Seleccione -- </option>';
                for (i = 0; i < filas; i++) {
                    str += '<option value="' + data[i].cod + '">' + data[i].nom + '</option>';
                }
                $('#cboPrioridad').html(str);
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false);
                //                console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function validaRegistroCpf() {
    if ($("#cboCarrera").val() == "") {
        fnMensaje("error", "Seleccione Carrera Profesional.");
        $("#cboCarrera").focus();
        return false;
    }
    if ($("#cboPrioridad").val() == "") {
        fnMensaje("error", "Seleccione Prioridad.");
        $("#cboPrioridad").focus();
        return false;
    }
    //    if ($("#txtdetalleCpf").val() == "") {
    //        fnMensaje("error", "Ingrese Correo Electrónico");
    //        return false;
    //    }
    return true;
}

function LimpiarCarrera(tipo) {
    $("#cboCarrera").prop('selectedIndex', 0);
    $("#txtdetalleCpf").val("");
    $("#hdcod_CP").val("0")
    $("#cboPrioridad").prop('selectedIndex', 0);
}


function fnGuardarCpf() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (validaRegistroCpf() == true) {
            fnLoading(true);
            if ($("#hdcod_CP").val() == 0) {
                $("form#frmRegistroCP input[id=hdcodiCP]").remove();
                $("form#frmRegistroCP input[id=action]").remove();
                $("form#frmRegistroCP input[id=hdcodeve]").remove();
                $('#frmRegistroCP').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmRegistroCP').append('<input type="hidden" id="hdcodiCP" name="hdcodiCP" value="' + $("#hdcod_i").val() + '" />');
                $('#frmRegistroCP').append('<input type="hidden" id="hdcodeve" name="hdcodeve" value="' + $("#codeve").val() + '" />');
                var form = $("#frmRegistroCP").serializeArray();
                $("form#frmRegistroCP input[id=action]").remove();
                $("form#frmRegistroCP input[id=hdcodiCP]").remove();
                $("form#frmRegistroCP input[id=hdcodeve]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/CarreraProfesional.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_CP").val(data[0].cod)
                            fnMensaje("success", data[0].msje)
                            if ($("#cboPrioridad option:selected").text() == "1") {
                                $("#codcpf").val($("#cboCarrera option:selected").val())
                                $("#txtcp").val($("#cboCarrera option:selected").text());
                            }
                            fnListarCarreras()
                            LimpiarCarrera()
                            $("#mdCarrera").modal("hide");
                            $("#btnCuestionario").focus();
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            } else {
                $("form#frmRegistroCP input[id=hdcodiCP]").remove();
                $("form#frmRegistroCP input[id=action]").remove();
                $("form#frmRegistroCP input[id=hdcodeve]").remove();
                $('#frmRegistroCP').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistroCP').append('<input type="hidden" id="hdcodiCP" name="hdcodiCP" value="' + $("#hdcod_i").val() + '" />');
                $('#frmRegistroCP').append('<input type="hidden" id="hdcodeve" name="hdcodeve" value="' + $("#codeve").val() + '" />');
                var form = $("#frmRegistroCP").serializeArray();
                $("form#frmRegistroCP input[id=action]").remove();
                $("form#frmRegistroCP input[id=hdcodiCP]").remove();
                $("form#frmRegistroCP input[id=hdcodeve]").remove();
                //                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/crm/CarreraProfesional.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    async: false,
                    success: function (data) {
                        //                        console.log(data);
                        if (data[0].rpta == 1) {
                            $("#hdcod_CP").val(data[0].cod)
                            fnMensaje("success", data[0].msje)
                            if ($("#cboPrioridad option:selected").text() == "1") {
                                $("#codcpf").val($("#cboCarrera option:selected").val());
                                $("#txtcp").val($("#cboCarrera option:selected").text());
                            }
                            fnListarCarreras()
                            LimpiarCarrera()
                            $("#mdCarrera").modal("hide");
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                        fnLoading(false);
                    },
                    error: function (result) {
                        fnLoading(false);
                        //                        console.log(result)
                    }
                });
            }
        }
    } else {
        window.location.href = rpta
    }
}


function EditarCarrera(cd) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("#hdcod_CP").val(cd)
        $("form#frmRegistroCP input[id=action]").remove();
        $('#frmRegistroCP').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistroCP").serializeArray();
        $("form#frmRegistroCP input[id=action]").remove();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/CarreraProfesional.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //                console.log(data);
                $("#cboCarrera").val(data[0].cpf);
                $("#txtdetalleCpf").val(data[0].det);
                //                var contador = 0;
                //                $("#cboPrioridad option").each(function() {
                //                    if ($(this).text() == data[0].pri) {
                //                        contador = contador + 1;
                //                    }
                //                });
                fnListaPrioridad($("#hdcod_CP").val());
                //                if (contador == 0) {
                //                    $("#cboPrioridad").append('<option value="' + data[0].pri + '">' + data[0].pri + '</option>');
                //                }
                $("#cboPrioridad").val(data[0].pri);
                //                if (data[0].vig == 1) {
                //                    $("#chkVigenciaEMail").prop("checked", true);
                //                } else {
                //                    $("#chkVigenciaEMail").prop("checked", false);
                //                }
                fnLoading(false);
            },
            error: function (result) {
                fnLoading(false)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnEliminarCarrera(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/CarreraProfesional.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function (data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarCarreras();
                    fnListaPrioridad(0);
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function (result) {
                //                console.log(result)
                fnMensaje("error", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}


var aDataR = [];
function fnDeleteCarrera(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar Carrera Profesional de Interesado?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminarCarrera', aDataR.cod);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}



/*============================================================== PREGUNTAS CRM =============================================================*/

function fnListarPreguntas() {
    fnLoading(true);
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.lst + '" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cod" name="cod" value="0" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/Preguntas.aspx",
        data: form,
        dataType: "json",
        //        cache: false,
        //        async:false,
        success: function (data) {
            var tb = '';
            var i = 0;
            var filas = data.length;
            if (filas > 0) {
                tb += '<div class="panel-group piluku-accordion piluku-accordion-two" id="accordionOne" role="tablist" aria-multiselectable="true">'
                for (i = 0; i < filas; i++) {
                    tb += '<div class="panel panel-default">';
                    tb += '<div class="panel-heading" role="tab" id="headingModal' + (i + 1) + '">'
                    tb += '<h4 class="panel-title">'
                    //tb += '<a class="" data-toggle="collapse" data-parent="#accordionOne" href="#Pregunta' + (i + 1) + '"'
                    tb += '<a class="" data-toggle="collapse" href="#Pregunta' + (i + 1) + '"'
                    //tb += 'aria-expanded="true" aria-controls="collapseOne" onclick="fnListarRespuestaxPregunta(\'' + data[i].codigo + '\',\'' + (i + 1) + '\')" >' + (i + 1) + '. ' + data[i].descripcion + '</a>'
                    tb += 'aria-expanded="true" aria-controls="collapseOne">' + (i + 1) + '. ' + data[i].descripcion + '</a>'
                    tb += '</h4>'
                    tb += '</div>'
                    tb += '<div id="Pregunta' + (i + 1) + '" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne" aria-expanded="true">'
                    tb += '<div class="panel-body">'
                    tb += '<input type="hidden" id="hdPregunta' + (i + 1) + '" name ="hdPregunta' + (i + 1) + '" value=\'' + data[i].codigo + '\' />'
                    tb += '<div id="BodyRespuestas' + (i + 1) + '">'
                    tb += '</div>'
                    tb += '</div>'
                    tb += '</div>'
                    tb += '</div>'
                }
                tb += '</div>'
                $("#ModalPreguntas").html(tb);
                i = 0;
                for (i = 0; i < filas; i++) {
                    fnListarRespuestaxPregunta(data[i].codigo, (i + 1))
                }
            }
            fnLoading(false);
        },
        error: function (result) {
            fnLoading(false);
        }
    });
}




function fnListarRespuestaxPregunta(cod, nro) {
    //if ($("#Pregunta" + nro).attr("class") == 'panel-collapse collapse') { //Verificar si se encuentra Colapsado o contraido
    fnLoading(true);
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.lrp + '" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/Preguntas.aspx",
        data: form,
        dataType: "json",
        //        cache: false,
        //        async:false,
        success: function (data) {
            var tb = '';
            var i = 0;
            var filas = data.length;
            if (filas > 0) {

                tb += '<div class="form-group">';
                tb += '<div class="row"><div class="col-md-12">&nbsp;</div></div>';
                tb += '<div class="row">';
                tb += '<div class="col-md-9">';
                tb += '<select class="form-control" id="cboRespuestas' + nro + '">';
                tb += '<option value ="">-- Seleccione --</option>';
                for (i = 0; i < filas; i++) {
                    tb += '<option value ="' + data[i].codigo + '">' + data[i].descripcion + '</option>';
                }
                tb += '</select>';
                tb += '</div>';
                tb += '<div class="col-md-3">';
                tb += '<input type="button" id="btnRespuestas"' + nro + '" class="btn btn-success" value="Agregar" onclick="fnAgregarRespuesta(' + nro + ',\'' + $("#codeve").val() + '\',\'' + $("#hdcod_i").val() + '\')"/>';
                tb += '</div>';
                tb += '</div>';
                tb += '<div class="row">'
                tb += '<div class="col-md-9"><input type="text" name="txtOtro" id="txtOtro' + nro + '" class="form-control hidden" placeholder="Descripción"></div>';
                tb += '</div>';
                tb += '</div>';

                /* Creo Tabla con las Respuestas ya Seleccionadas*/
                tb += '<div class="form-group">';
                tb += '<div class="row">';
                tb += '<div class="table-responsive" style="font-size:12px;color:black;padding:3px;">'
                tb += '<div id="tEvento_wrapper" class="dataTables_wrapper" role="grid">'
                tb += '<table id="tRespuestas' + nro + '" class="display dataTable cell-border" width="100%" style="width: 100%;">'
                tb += '<thead>'
                tb += '<tr role="row">'
                tb += '<td width="10%" style="font-weight: bold;" class="sorting_asc" tabindex="0" rowspan="1" colspan="1" aria-sort="ascending" aria-label="N.: activate to sort column descending">N.</td>'
                tb += '<td width="80%" style="font-weight: bold;" class="sorting" tabindex="0" rowspan="1" colspan="1" aria-label="Respuesta: activate to sort column ascending">Respuesta</td>'
                tb += '<td width="10%" style="font-weight: bold;" class="sorting" tabindex="0" rowspan="1" colspan="1" aria-label="Opciones: activate to sort column ascending">Opciones</td>'
                tb += '</tr>'
                tb += '</thead>'
                tb += '<tfoot><tr><th colspan="3" rowspan="1"></th></tr></tfoot>'
                tb += '<tbody id="tbRespuestas' + nro + '"></tbody>'
                tb += '</table>'
                tb += '</div>'
                tb += '</div>'
                tb += '</div>'
                tb += '</div>'
                /* Fin Tabla*/

                var idBodyRespuestas = "#BodyRespuestas" + nro
                $(idBodyRespuestas).html(tb);

                // El usuario selecciona "OTRO"
                $(idBodyRespuestas).find('select[id^="cboRespuestas"]').on('change', function (e) {
                    var $txtOtro = $(idBodyRespuestas).find('input[id^="txtOtro"]');
                    var respuesta = $(this).find('option:selected').text();

                    if (respuesta.trim() == 'OTRO') {
                        $txtOtro.removeClass('hidden');
                        $txtOtro.focus();
                    } else {
                        $txtOtro.val('');
                        if (!$txtOtro.hasClass('hidden')) {
                            $txtOtro.addClass('hidden');
                        }
                    }
                });

                var dt = fnCreateDataTableBasic('tRespuestas' + nro, 0, 'asc', 10);


                fnListarRespuestasxPregunta(nro, $("#codeve").val(), $("#hdcod_i").val(), cod)

            }
            fnLoading(false);
        },
        error: function (result) {
            fnLoading(false);
        }
    });
    //}
}


function fnAgregarRespuesta(nro, cod_eve, cod_int) {
    if ($("#cboRespuestas" + nro).val() != "") {
        rpta = fnvalidaSession()
        //    alert(rpta)
        if (rpta == true) {
            $.ajax({
                type: "POST",
                url: "../DataJson/crm/Preguntas.aspx",
                data: { "action": ope.reg, "codeve": cod_eve, "codint": cod_int, "codresp": $("#cboRespuestas" + nro).val(), "nombreresp": $("#cboRespuestas" + nro).find('option:selected').text(), "rptaotro": $("#txtOtro" + nro).val() },
                dataType: "json",
                //cache: false,
                //async: false,
                success: function (data) {
                    //                console.log(data);
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        $("#cboRespuestas" + nro).val("");
                        $("#cboRespuestas" + nro).trigger('change');
                        fnListarRespuestasxPregunta(nro, cod_eve, cod_int, $("#hdPregunta" + nro).val())
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                },
                error: function (result) {
                    //                console.log(result)
                    fnMensaje("error", result)
                }
            });
        } else {
            window.location.href = rpta
        }
    } else {
        fnMensaje("error", "Debe seleccionar una Respuesta para Agregar");
    }
}

function fnListarRespuestasxPregunta(nro, cod_eve, cod_int, cod_pre) {
    fnLoading(true);
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/Preguntas.aspx",
        data: { "action": ope.rxp, "codeve": cod_eve, "codint": cod_int, "codpre": cod_pre },
        dataType: "json",
        //cache: false,
        //async: false,
        success: function (data) {
            var tb = '';
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                tb += '<tr>';
                tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                tb += '<td>' + data[i].descripcion + '</td>';
                tb += '<td>';
                tb += '<button type="button" id="btnEliminarRespuesta" name="btnEliminarRespuesta" onclick="fnQuitarRespuesta(\'' + data[i].codigoipr + '\',' + nro + ')" class="btn btn-xs btn-red" title="Eliminar" alt="Eliminar" ><i class="ion-android-delete"></i></button>'
                if (filas > 1) {
                    if (i + 1 != filas) {
                        tb += '<button type="button" id="btnBajar" name="btnBajar" onclick="fnMoverRespuesta(\'' + data[i].codigoipr + '\',' + nro + ',\'B\')" class="btn btn-xs btn-orange" title="Bajar" alt="Bajar" ><i class="ion-arrow-down-b"></i></button>'
                    }
                    if (i != 0) {
                        tb += '<button type="button" id="btnSubir" name="btnSubir" onclick="fnMoverRespuesta(\'' + data[i].codigoipr + '\',' + nro + ',\'S\')" class="btn btn-xs btn-primary" title="Subir" alt="Subir" ><i class="ion-arrow-up-b"></i></button>'
                    }
                }
                tb += '</td>';
                tb += '</tr>';
            }
            fnDestroyDataTableDetalle('tRespuestas' + nro);
            $('#tbRespuestas' + nro).html(tb);
            fnResetDataTableBasic('tRespuestas' + nro, 0, 'asc', 10);
            fnLoading(false);
        },
        error: function (result) {
            fnLoading(false);
            //                console.log(result)
        }
    });

}


var aDataRes = [];
function fnQuitarRespuesta(cod, nro) {
    aDataRes = {
        cod: cod,
        mensaje: '¿Desea Eliminar Respuesta Seleccionada?',
        nro: nro
    }
    fnMensajeConfirmarEliminar('top', aDataRes.mensaje, 'fnEliminarRespuesta', aDataRes.cod, aDataRes.nro);
    //fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}


function fnEliminarRespuesta(cod, nro) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Preguntas.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function (data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarRespuestasxPregunta(nro, $("#codeve").val(), $("#hdcod_i").val(), $("#hdPregunta" + nro).val())
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function (result) {
                //                console.log(result)
                fnMensaje("error", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}


function fnMoverRespuesta(cod, nro, dir) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/crm/Preguntas.aspx",
            data: { "action": ope.mrp, "hdcod": cod, "dir": dir },
            dataType: "json",
            //cache: false,
            //async: false,
            success: function (data) {
                //                console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnListarRespuestasxPregunta(nro, $("#codeve").val(), $("#hdcod_i").val(), $("#hdPregunta" + nro).val())
                } else {
                    fnMensaje("error", data[0].msje)
                }
            },
            error: function (result) {
                //                console.log(result)
                fnMensaje("error", result)
            }
        });
        //fnLoading(false)
    } else {
        window.location.href = rpta
    }
}

/*============================================================== FIN PREGUNTAS CRM =============================================================*/












function fnCreateDataTableBasic(table, col, ord, nfilas) {
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": nfilas,
        "aaSorting": [[col, ord]]
    });
    return dt;
}

function fnResetDataTableBasic(table, col, ord, nfilas, sDom, callback) {
    // console.log("DATATABLE: " + getTiempo());
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        // var dt = $('#' + table).DataTable({
        //     "sContentPadding": false
        // });
        // dt = $('#' + table).DataTable().fnDestroy();

        var options = {
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            "aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": nfilas,
            "aaSorting": [[col, ord]],
            //            ,
            // "sDom": 'BfrTip',
            //            "oTableTools": {
            //                "sSwfPath": "../assets/swf/copy_csv_xls_pdf.swf",
            //                "aButtons": ["pdf"]
            //            }
        }

        if (sDom != undefined) {
            options.sDom = sDom
        }

        var dt = $('#' + table).DataTable(options);
        // console.log("FIN: " + getTiempo());

        if (typeof callback === 'function') {
            callback();
        }

        return dt;
    }
}



function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}

function fnLoading(sw, callback) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }

    if (callback != undefined) {
        callback();
    }
}

function fnLoadingDiv(div, sw) {
    if (sw) {
        $("#" + div).removeClass('hidden');
    } else {
        $("#" + div).addClass('hidden');
    }
}

function initCboEvento() {
    $('#cboEvento').selectpicker({
        noneSelectedText: 'Ningún evento seleccionado',
        liveSearch: true,
        size: 10
    });
    $('#cboEvento').selectpicker('refresh');
}

function initCboCarreraProfesional() {
    $('#cboCarreraProfesional').selectpicker({
        noneSelectedText: 'Ninguna carrera seleccionada',
        liveSearch: true,
        // width: 320
    });
    $('#cboCarreraProfesional').selectpicker('refresh');
}

function initCboCentroCosto() {
    $('#cboCentroCosto').selectpicker({
        noneSelectedText: 'Ningún centro de costo seleccionados',
        liveSearch: true,
        // width: 320
    });
    $('#cboCentroCosto').selectpicker('refresh');
}

function initCboFiltroGrados() {
    $('#cboGrados').selectpicker({
        'noneSelectedText': 'Ningún grado seleccionado'
    });
    $('#cboGrados').selectpicker('refresh');
}

function initCboRequisitoAdmision() {
    $('#cboRequisitoAdmision').selectpicker({
        noneSelectedText: 'Ningún requisito seleccionado',
        liveSearch: true,
        // width: 320
    });
    $('#cboRequisitoAdmision').selectpicker('refresh');
}

function initCboFiltroColegio() {
    $('#cboFiltroColegio').selectpicker('destroy');
    $('#cboFiltroColegio').selectpicker({
        noneSelectedText: 'Ningún colegio seleccionado',
        liveSearch: true,
    });
    $('#cboFiltroColegio').selectpicker('refresh');
}

function resetCboEvento() {
    $('#cboEvento').val('');
    $('#cboEvento').selectpicker('refresh');
}

function resetCboFiltroColegio() {
    $('#cboFiltroColegio').val('');
    $('#cboFiltroColegio').selectpicker('refresh');
}

function resetCboCarreraProfesional() {
    $('#cboCarreraProfesional').val('');
    $('#cboCarreraProfesional').selectpicker('refresh');
}

function resetCboCentroCosto() {
    $('#cboCentroCosto').val('');
    $('#cboCentroCosto').selectpicker('refresh');
}

function resetCboRequisitoAdmision() {
    $('#cboRequisitoAdmision').val('');
    $('#cboRequisitoAdmision').selectpicker('refresh');
}

function getTiempo() {
    m = new Date();
    dateString = m.getUTCFullYear() + "/" + (m.getUTCMonth() + 1) + "/" + m.getUTCDate() + " " + m.getUTCHours() + ":" + m.getUTCMinutes() + ":" + m.getUTCSeconds();
    return dateString;
}

function guardarAnexo(numeroAnexo) {
    $.ajax({
        type: "POST",
        url: "../DataJson/crm/PersonalAnexo.aspx",
        data: { "action": ope.reg, "txtNumeroAnexo": numeroAnexo },
        dataType: "json",
        cache: false,
        async: false,
        success: function (data) {
            console.log(data);
            if (data[0].rpta == 1) {
                $("#mdAnexo").modal("hide");
                fnMensaje("success", data[0].msje)
            } else {
                fnMensaje("error", data[0].msje)
            }
        },
        error: function (result) {
            //                console.log(result)
            fnMensaje("error", result)
        }
    });
}