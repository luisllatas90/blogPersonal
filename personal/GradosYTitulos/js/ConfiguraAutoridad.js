$(document).ready(function() {
    //fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    //fnLoading(false);
    if (rpta == true) {
        cFacultad();
        cPersonal();
        cCargo();
        cPrefijo()
        $("#cboPrefijo").val("MAA=")
        $("#cboFacultad").val("MAA=")
        //cTipoEstudio();
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
    /*$("#cboTipoEstudio").change(function() {
    cCarreraProfesional($("#cboTipoEstudio").val(), 1);
    })
    $("#cboTipoEstudioR").change(function() {
    cCarreraProfesional($("#cboTipoEstudioR").val(), 2);
    })

*/
    $("#btnConsultar").click(function() {
        //if (fnValidaConsultar() == true) {
        ConsultarConfiguracion();
        //}
    })

    $("#btnAgregar").click(function() {
        Limpiar();
        //fnHabilitar()
        $("#hdcod").val("0")
        $("#mdRegistro").modal("show");
    });



});

function cFacultad() {
    var arr = fnFacultad();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboFacultad').html(str);
}

function cPersonal() {
    var arr = fnPersonal();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboPersonalR').html(str);
}

function cCargo() {
    var arr = fnCargo();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboCargo').html(str);
    $('#cboCargoR').html(str);

}

function cPrefijo() {
    var arr = fnPrefijo();
    console.log(arr);
    var n = arr.length;
    var str = "";
    //    str += "<option value='0' selected='selected'>-- Seleccione -- </option>";
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboPrefijo').html(str);

}



function ConsultarConfiguracion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true);
        var source = {
            datatype: "json",
            type: "POST",
            async: false,
            datafields: [
                { name: 'cod_cc', type: 'string' },
                { name: 'cod_cg', type: 'string' },
                { name: 'nom_cg', type: 'string' },
                { name: 'cod_pre', type: 'string' },
                { name: 'cod_pe', type: 'string' },
                { name: 'nom_pe', type: 'string' },
                { name: 'cod_fac', type: 'string' },
                { name: 'nom_fac', type: 'string' },
                { name: 'encar', type: 'string' },
                { name: 'vig', type: 'string' },
                { name: 'ord', type: 'string' }
                ],
            root: 'rows',
            url: "../DataJson/GradosyTitulos/ConfigurarAutoridad.aspx",
            data: { action: ope.lst, cod: $("#cboCargo").val(), vig: $("#cboVigenciaL").val() }

        };
//        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
//        console.log(dataAdapter);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Editar" onclick="CargaDatos(\'' + dataRecord.cod_cc + '\');"><i class="ion-android-hand"></i></button>';
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
            ready: function() {
                // called when the Grid is loaded. Call methods or set properties here.         
            },
            //selectionmode: 'checkbox',
            altrows: true,
            columns: [
            //{text: 'codigo', datafield: 'cod', width: '8%' },
                {text: 'Cargo', datafield: 'nom_cg', width: '20%' },
                { text: 'Personal', datafield: 'nom_pe', width: '35%' },
                { text: 'Facultad', datafield: 'nom_fac', width: '20%' },
                { text: 'Orden', datafield: 'ord', width: '5%' },
                { text: 'Encargado', columntype: 'checkbox', datafield: 'encar', width: '9%' },
                { text: 'Vigencia', columntype: 'checkbox', datafield: 'vig', width: '7%' },
                { text: ' ', datafield: 'cod_cc', width: '4%', cellsrenderer: cellsrenderer
                    /*createwidget: function(rowindex, column, value, htmlElement) {
                    var button = $("<button class='btn btn-success' valor="+ value +"><i class='ion-android-hand'></i></button>");
                    $(htmlElement).append(button);
                    button.click(function(event) {
                    console.log(rowindex);
                    var cod = $(this).attr("valor")
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

function ver(cod) {

    alert(cod)
}

function CargaDatos(cod) {
    if (cod != "") {
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/ConfigurarAutoridad.aspx",
            data: { action: ope.edi, cod: cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
//                console.log(data);
                $("#cboCargoR").val(data[0].cod_cg)
                $("#cboPersonalR").val(data[0].cod_pe)
                $("#cboFacultad").val(data[0].cod_fac)
                $("#cboOrden").val(data[0].ord)
                $("#cboPrefijo").val(data[0].cod_pre)
                if (data[0].vig == '1') {
                    $("#chkvigencia").prop("checked", true);
                } else {
                    $("#chkvigencia").prop("checked", false);
                }
                if (data[0].encar == '1') {
                    $("#chkEncargado").prop("checked", true);
                } else {
                    $("#chkEncargado").prop("checked", false);
                }
                $("#hdcod").val(data[0].cod_cc)
//                fnDeshabilitar()
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


function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (fnValidar() == true) {
            //fnLoading(true)
            if ($("#hdcod").val() == 0) {
                //fnHabilitar()
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //$('#hdcod').remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GradosyTitulos/ConfigurarAutoridad.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            Limpiar()
                            ConsultarConfiguracion()
                            $("#mdRegistro").modal("hide");

                        }
                        else {
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //console.log(result)
                        fnMensaje("error", result)
                    }
                });
            } else {
                //fnHabilitar()
                $("form#frmRegistro input[id=action]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GradosyTitulos/ConfigurarAutoridad.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            $('#hdcod').val(0);
                            Limpiar()
                            fnMensaje("success", data[0].msje)
                            ConsultarConfiguracion()
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
    $("#cboCargoR").val("");
    $('#cboPersonalR').val("");
    $("#cboPrefijo").val("MAA=")
    $("#cboFacultad").val("MAA=")
    $("#cboOrden").val("");
    $("#chkvigencia").prop("checked", true);
    $("#chkEncargado").prop("checked", false);
}

function fnValidar() {
    if ($("#cboCargoR").val() == "") {
        fnMensaje("error", "Seleccione el Cargo.")
        return false;
    }
    if ($("#cboPersonalR").val() == "") {
        fnMensaje("error", "Seleccione el Prefijo de Personal.")
        return false;
    }
    if ($("#cboPersonalR").val() == "") {
        fnMensaje("error", "Seleccione el Personal.")
        return false;
    }
    if ($("#cboOrden").val() == "0") {
        fnMensaje("error", "Seleccione el orden.")
        return false;
    }
    return true;

}
/*
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

}*/
/*
function fnDeshabilitar() {
    $("#cboCargoR").attr("disabled", "disabled");
    $("#cboPersonalR").attr("disabled", "disabled");
    $("#cboFacultad").attr("disabled", "disabled");
    $("#chkEncargado").attr("disabled", "disabled");

}

function fnHabilitar() {
    $("#cboCargoR").removeAttr("disabled");
    $("#cboPersonalR").removeAttr("disabled");
    $("#cboFacultad").removeAttr("disabled");
    $("#chkEncargado").removeAttr("disabled");
}*/