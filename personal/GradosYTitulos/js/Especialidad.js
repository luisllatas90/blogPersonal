$(document).ready(function() {
    //fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    //fnLoading(false);
    if (rpta == true) {
        cTipoEstudio();
    } else {
        window.location.href = rpta
    }

    $("#cboTipoEstudio").change(function() {
        cCarreraProfesional($("#cboTipoEstudio").val(), 1);
    })
    $("#cboTipoEstudioR").change(function() {
        cCarreraProfesional($("#cboTipoEstudioR").val(), 2);
    })


    $("#cboCarreraR").change(function() {
        cPlanEstudios($("#cboCarreraR").val());
    })


    $("#btnConsultar").click(function() {
        if (fnValidaConsultar() == true) {
            ConsultarEspecialidad();
        }
    })

    $("#btnAgregar").click(function() {
        Limpiar();
        $("#hdcod").val("0")
        $("#mdRegistro").modal("show");
    });

});

function ConsultarEspecialidad() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true);
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod_es', type: 'string' },
                    { name: 'nom_es', type: 'string' },
                    { name: 'nom_pes', type: 'string' },
                    { name: 'vig', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Especialidad.aspx",
                data: { action: ope.lst, test: $("#cboTipoEstudio").val(), cpf: $("#cboCarrera").val(), vig: $("#cboVigenciaL").val() }

            };

        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Editar" onclick="CargaDatos(\'' + dataRecord.cod_es + '\');"><i class="ion-android-hand"></i></button>';
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
                altrows: true,
                columns: [
                { text: 'Especialidad', datafield: 'nom_es', width: '40%' },
                { text: 'Plan Estudio', datafield: 'nom_pes', width: '49%' },
                { text: 'Vigencia', columntype: 'checkbox', datafield: 'vig', width: '7%' },
                { text: ' ', datafield: 'cod_es', width: '4%', cellsrenderer: cellsrenderer

                }
                ]
            });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function CargaDatos(cod) {
    if (cod != "") {
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Especialidad.aspx",
            data: { action: ope.edi, cod: cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
//                console.log(data);
                $("#cboTipoEstudioR").val(data[0].test)
                cCarreraProfesional($("#cboTipoEstudioR").val(), 2);
                $("#cboCarreraR").val(data[0].cod_cp)
                cPlanEstudios($("#cboCarreraR").val());
                $("#cboPlanEstudios").val(data[0].cod_pes)
                $("#txtespecialidad").val(data[0].nom_es)
                $("#txtabreviatura").val(data[0].abr_es)
                if (data[0].vig == 1) {
                    $("#chkvigencia").prop("checked", true);
                } else {
                    $("#chkvigencia").prop("checked", false);
                }
                $("#hdcod").val(data[0].cod_es)
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


function cPlanEstudios(cod_cpf) {
    var arr = fnPlanEstudios(cod_cpf);
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboPlanEstudios').html(str);
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
                    url: "../DataJson/GradosyTitulos/Especialidad.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            if ($("#cboTipoEstudio").val() != "" && $("#cboCarrera").val() != "") { ConsultarEspecialidad() }
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
                    url: "../DataJson/GradosyTitulos/Especialidad.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            $('#hdcod').val(0);
                            Limpiar()
                            fnMensaje("success", data[0].msje)
                            if ($("#cboTipoEstudio").val() != "" && $("#cboCarrera").val() != "") { ConsultarEspecialidad() }
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
    $('#cboPlanEstudios').html(str);
    $("#txtespecialidad").val("");
    $("#txtabreviatura").val("");
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
    if ($("#cboPlanEstudios").val() == "") {
        fnMensaje("error", "Seleccione el Plan de Estudios.")
        return false;
    }
    if ($("#txtespecialidad").val() == "") {
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