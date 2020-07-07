$(document).ready(function() {
    //fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    //fnLoading(false);
    if (rpta == true) {
        cTipoEstudio();
        cTipoDenominacion();
        //cActoAcademico();
        // Create a jqxDateTimeInput
        //$("#txtFechaActoAcad").jqxDateTimeInput({ width: '100%', height:'30px'});
        //fnLoading(false);
    } else {
        window.location.href = rpta
    }
    /*

    $("#btnRegistrar").click(function() {
    alert($(this).text())
    if ($(this).text() == "Registrar") {
    $("#ListaSesion").hide();
    $("#RegistraSesion").show();
    $(this).text("Cancelar");
    $(this).removeAttr("class");
    $(this).attr("class", "btn btn-danger");
    fnColumnaSesion(2)
    fnColumnaExpedientes(0, 2)

        } else {
    $("#ListaSesion").show();
    $("#RegistraSesion").hide();
    $(this).text("Registrar");
    $(this).removeAttr("class");
    $(this).attr("class", "btn btn-primary");
    fnColumnaSesion(1)
    $("#jqxgrid").jqxGrid("clear");
    }

    })

    $("#CboSesiones").change(function() {
    fnColumnaExpedientes($(this).val(), 2)
    fnColumnaExpedientes($(this).val(), 3)
    })

    $("#txtbusqueda").keyup(function(e) {
    if (e.keyCode == 13) {
    if (ValidaBusqueda() == true) {
    ConsultarAlumno();
    $("#mdCoincidencias").modal("show");
    }
    }

    })*/
    $("#cboTipoEstudio").change(function() {
        cCarreraProfesional($("#cboTipoEstudio").val(), 1);
    })
    $("#cboTipoEstudioR").change(function() {
        cCarreraProfesional($("#cboTipoEstudioR").val(), 2);
    })


    $("#btnConsultar").click(function() {
        if (fnValidaConsultar() == true) {
            ConsultarDenominacion();
        }
    })

    $("#btnAgregar").click(function() {
        Limpiar();
        $("#hdcod").val("0")
        $("#mdRegistro").modal("show");
    });

});

function ConsultarDenominacion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true);
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod_cp', type: 'string' },
                    { name: 'nom_cp', type: 'string' },
                    { name: 'cod_gt', type: 'string' },
                    { name: 'nom_gt', type: 'string' },
                    { name: 'vig', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Denominacion.aspx",
                data: { action: ope.lst, test: $("#cboTipoEstudio").val(), cpf: $("#cboCarrera").val(), vig: $("#cboVigenciaL").val() }

            };
        //        console.log(source);
            var dataAdapter = new $.jqx.dataAdapter(source);
            var cellsrenderer = function(row, column, value) {
                // alert(value);
                var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
                return '<button class="btn btn-success" title="Editar" onclick="CargaDatos(\'' + dataRecord.cod_gt + '\');"><i class="ion-android-hand"></i></button>';
            }

        
        $("#jqxgrid").jqxGrid(
            {
                width: "100%",
                height: 500,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                columnsresize: true,
                //                showdefaultloadelement: false,
                //                autoshowloadelement: false,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                //selectionmode: 'checkbox',
                altrows: true,
                columns: [
                //{text: 'codigo', datafield: 'cod', width: '8%' },
                {text: 'Carrera Profesional', datafield: 'nom_cp', width: '40%' },
                { text: 'Denominación', datafield: 'nom_gt', width: '49%' },
                { text: 'Vigencia', columntype: 'checkbox', datafield: 'vig', width: '7%' },
                { text: ' ', datafield: 'cod_gt', width: '4%', cellsrenderer: cellsrenderer
                    /*createwidget: function(rowindex, column, value, htmlElement) {
                        var button = $("<button class='btn btn-success' title='Editar'><i class='ion-android-hand'></i></button>");
                        $(htmlElement).append(button);
                        button.click(function(event) {
                            var cod = rowindex.bounddata.cod_gt;
                            CargaDatos(cod)
                        });
                    },
                    initwidget: function(row, column, value, htmlElement) {
                    }*/
                }
                ]
            });
        //fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function CargaDatos(cod) {
    if (cod != "") {
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Denominacion.aspx",
            data: { action: ope.edi, cod: cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                $("#cboTipoEstudioR").val(data[0].test)
                cCarreraProfesional($("#cboTipoEstudioR").val(), 2);
                $("#cboCarreraR").val(data[0].cod_cp)
                $("#txtdenominacion").val(data[0].nom_gt)
                $("#cboTipoDenominacion").val(data[0].tip)
                if (data[0].vig == 1) {
                    $("#chkvigencia").prop("checked", true);
                } else {
                    $("#chkvigencia").prop("checked", false);
                }
                $("#hdcod").val(data[0].cod_gt)
                $("#mdRegistro").modal("show");
            },
            error: function(result) {
                console.log(result)
                fnMensaje("error", result)
            }
        });

    } else {
        fnMensaje("error", "Debe Seleccionar una Fila")

    }

}

function cTipoEstudio() {
    var arr = fnTipoEstudio();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboTipoEstudio').html(str);
    $('#cboTipoEstudioR').html(str);
}


function cTipoDenominacion() {
    var arr = fnTipoDenominacion();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected="selected" >-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboTipoDenominacion').html(str);
}

function cCarreraProfesional(cod_test, nro_cbo) {
    var arr = fnCarreraProfesional(cod_test);
    //    console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    if (nro_cbo == 1) {
        $('#cboCarrera').html(str);
    }
    if (nro_cbo == 2) {
        $('#cboCarreraR').html(str);
    }
}

function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (fnValidar() == true) {
            //fnLoading(true)
            if ($("#hdcod").val() == 0) {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //$('#hdcod').remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GradosyTitulos/Denominacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            if ($("#cboTipoEstudio").val() != "" && $("#cboCarrera").val() != "") { ConsultarDenominacion() }
                            $("#mdRegistro").modal("hide");

                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //console.log(result)
                        fnMensaje("error", result)
                    }
                });
            } else {
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GradosyTitulos/Denominacion.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            $('#hdcod').val(0);
                            Limpiar()
                            fnMensaje("success", data[0].msje)
                            if ($("#cboTipoEstudio").val() != "" && $("#cboCarrera").val() != "") { ConsultarDenominacion() }
                            $("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //console.log(result)
                        fnMensaje("error", result)
                    }
                });
            }
            //fnLoading(false)
        }
    } else {
        window.location.href = rpta
    }
}

function Limpiar() {
    $("#cboTipoEstudioR").val("");
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    $('#cboCarreraR').html(str);
    $("#cboTipoDenominacion").val("");
    $("#txtdenominacion").val("");
    $("#chkvigencia").prop("checked", true);
}

function fnValidar() {
    if ($("#cboTipoEstudioR").val() == "") {
        fnMensaje("error", "Seleccione el Tipo de Estudio.")
        return false;
    }
    if ($("#cboCarreraR").val() == "") {
        fnMensaje("error", "Seleccione la Carrera Profesional.")
        return false;
    }
    if ($("#cboTipoDenominacion").val() == "") {
        fnMensaje("error", "Seleccione el Tipo de Denominación.")
        return false;
    }
    if ($("#txtdenominacion").val() == "") {
        fnMensaje("error", "Ingrese Denominación.")
        return false;
    }
    return true;

}

function fnValidaConsultar() {
    if ($("#cboTipoEstudio").val() == "") {
        fnMensaje("error", "Seleccione el Tipo de Estudio.")
        return false;
    }
    if ($("#cboCarrera").val() == "") {
        fnMensaje("error", "Seleccione la Carrera Profesional.")
        return false;
    }
    return true;

}