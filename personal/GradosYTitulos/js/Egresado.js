$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    fnLoading(false);
    if (rpta == true) {
        cTipoEstudio();
        cFacultad();
        cActoAcademico();
        cCargo();
        cGrupoEgresado();
        // Create a jqxDateTimeInput
        $("#txtFechaActoAcad").jqxDateTimeInput({ width: '100%', height: '30px' });
        var f = new Date();
        f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
        $("#txtFechaActoAcad").jqxDateTimeInput('setDate', f);
        $("#txtFechaResolucionFac").jqxDateTimeInput({ width: '100%', height: '30px' })
        $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', '');
        $("#RegistroExpediente").hide();
        //        $("#txtFechaConsejo").jqxDateTimeInput({ width: '100%', height: '30px' });
        //        $("#txtFechaResolucion").jqxDateTimeInput({ width: '100%', height: '30px' });
    } else {
        window.location.href = rpta
    }

    $("#cboCargo1").change(function() {
        cAutoridad($(this).val(), 'MAA=', '%', 1) // MAA= : 0
    })
    $("#cboCargo2").change(function() {
        cAutoridad($(this).val(), 'MAA=', '%', 2) // MAA= : 0
    })
    $("#cboCargo3").change(function() {
        cAutoridad($(this).val(), $("#cboFacultad").val(), '%', 3)
    })
    $("#cboFacultad").change(function() {
        cAutoridad($("#cboCargo3").val(), $(this).val(), 1, 3) // MAA= : 0
    })
    //31.07.2020 JBANDA
    $("#cboCargo4").change(function() {
        cAutoridad($(this).val(), 'MAA=', '%', 4) // MAA= : 0
    })
    
    /*
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
    $("#btnConsultar").click(function() {
        ConsultarEgresado();
        $("#RegistroExpediente").hide();
        $("#Lista").show();
    })

    $("#btnGuardar").click(function() {
        fnGuardar();
    })

    $("#btnCancelarReg").click(function() {
        $("#RegistroExpediente").hide();
        $("#Lista").show();
    })

    $("#btnEditarDatos").click(function() {
        $("#mdEditarDatos").modal("show");
    })

    $("#btnGuardarDatos").click(function() {
        fnGuardarDatosContacto();
    })

    $("#txtbusqueda").keyup(function(e) {
        if (e.keyCode == 13) {
            ConsultarEgresado();
            $("#RegistroExpediente").hide();
            $("#Lista").show();
        }

    })

});

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
}

function cCargo() {
    var arr = fnCargo();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" >-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboCargo1').html(str);
    $('#cboCargo2').html(str);
    $('#cboCargo3').html(str);
    //31.07.2020 JBANDA
    $('#cboCargo4').html(str);
}


function cAutoridad(cod_cgo, cod_fac, vig, cbo) {
    var arr = fnAutoridad(cod_cgo, cod_fac, vig);
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    if (cbo == 1) {
        $('#cboAutoridad1').html(str);
    }
    if (cbo == 2) {
        $('#cboAutoridad2').html(str);
    }
    if (cbo == 3) {
        $('#cboAutoridad3').html(str);
    }
    //31.07.2020 JBANDA
    if (cbo == 4) {
        $('#cboAutoridad4').html(str);
    }
}

function ConsultarEgresado() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod', type: 'string' },
                    { name: 'nro_exp', type: 'string' },
                    { name: 'cod_alu', type: 'string' },
                    { name: 'cod_univer', type: 'string' },
                    { name: 'alu', type: 'string' },
                    { name: 'pes', type: 'string' },
                    { name: 'est', type: 'string' },
                    { name: 'tipo_dip', type: 'string' },
                    { name: 'abrev_dip', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.lst, hdTest: $("#cboTipoEstudio").val(), hdCarrera: $("#cboCarrera").val(), txtbuscar: $("#txtbusqueda").val() }

            };
        //        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" onclick="CargaDatosEgresado(\'' + dataRecord.cod + '\');"><i class="ion-android-hand"></i></button>';
        }

        $("#jqxgrid").jqxGrid(
            {
                width: "100%",
                height: 450,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                //columnsresize: true,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                //selectionmode: 'checkbox',
                altrows: true,
                columns: [
                //{text: 'codigo', datafield: 'cod', width: '8%' },
                {text: 'Nro. Expediente', datafield: 'nro_exp', width: '10%' },
                { text: 'Código Univ.', datafield: 'cod_univer', width: '10%' },
                { text: 'Alumno', datafield: 'alu', width: '38%' },
                { text: 'Plan Estudios', datafield: 'pes', width: '22%' },
                { text: 'Tipo', datafield: 'tipo_dip', width: '4%', cellsalign: 'center', align: 'center' },
                { text: 'Diploma', datafield: 'abrev_dip', width: '6%', cellsalign: 'center', align: 'center' },
                { text: 'Estado', columntype: 'checkbox', datafield: 'est', width: '6%' },
                { text: ' ', datafield: 'cod', width: '4%', cellsrenderer: cellsrenderer }
                ]
            });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function CargaDatosEgresado(cod) {
    if (cod != "") {
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Egresado.aspx",
            data: { action: ope.edi, hdcod: cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                //console.log(data);
                var tb1 = '';
                var tb2 = '';
                var tb3 = '';
                var tb4 = '';
                var tb5 = '';
                var tb6 = '';
                var tb7 = '';
                var cbo_Cpf = '';
                var apnom = '';
                cbo_Cpf += '<option value="0">-- SELECCIONE --</option>';
                var i = 0;
                var filas = data.length;
                for (i = 0; i < filas; i++) {

                    tb1 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                    tb1 += '<li>' + data[i].cod_univer + '<p>Código Universitario</p></li></ul>'
                    $("#lblcodigo").html(data[i].cod_univer)

                    tb2 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                    tb2 += '<li>' + data[i].tipodoc + '<p>Tipo de Documento</p></li></ul>'

                    tb3 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                    tb3 += '<li>' + data[i].nrodoc + '<p>N° de Documento</p></li></ul>'
                    $("#lbldocumento").html(data[i].nrodoc)

                    apnom += '<ul class="list-inline list-unstyled"><li><i class="fa fa-user primary-info"></i></li>'
                    apnom += '<li>' + data[i].alu + '<p>Apellidos y Nombres</p></li></ul>'
                    $("#txtapepat").val(data[i].apepat)
                    $("#txtapemat").val(data[i].apemat)
                    $("#txtnombres").val(data[i].nom)
                    tb4 += '<img border="1" src="//intranet.usat.edu.pe/imgestudiantes/' + data[i].foto + '" width="100" heigth="118" style="width:98px;height:115px" alt="Sin Foto">'

                    tb5 += '<ul class="list-inline list-unstyled"><li><i class="fa fa-at primary-info"></i></li>'
                    tb5 += '<li>' + data[i].correo + '<p>Correo Electrónico</p></li></ul>'
                    $("#txtemail").val(data[i].correo)

                    tb6 += '<ul class="list-inline list-unstyled"><li><i class="fa fa-mobile primary-info"></i></li>'
                    tb6 += '<li>' + data[i].telmov + '<p>Teléfono Movil</p></li></ul>'
                    $("#txttelmov").val(data[i].telmov)

                    tb7 += '<ul class="list-inline list-unstyled"><li><i class="fa fa-phone primary-info"></i></li>'
                    tb7 += '<li>' + data[i].telfijo + '<p>Teléfono Fijo</p></li></ul>'
                    $("#txttelfijo").val(data[i].telfijo)

                    $("#txtNroExp").val(data[i].nro_exp)
                    //                    $("#txtFechaConsejo").jqxDateTimeInput('setDate', data[i].fec_cons);
                    $("#txtFechaConsejo").val(data[i].fec_cons);
                    $("#txtNroResolucion").val(data[i].nro_res)
                    //                    $("#txtFechaResolucion").jqxDateTimeInput('setDate', data[i].fec_res);
                    $("#txtFechaResolucion").val(data[i].fec_res);
                    $("#txtFechaActoAcad").jqxDateTimeInput('setDate', data[i].fec_acto);
                    $("#cboActoAcad").val(data[i].cod_acto)
                    $("#txtTituloTesis").val(data[i].nom_tes)
                    $("#hdCodigoTes").val(data[i].cod_tes)
                    $("#cboFacultad").val(data[i].cod_fac)
                    $("#cboModEstudio").val(data[i].mod_est)
                    $("#cboEmisionDiploma").val(data[i].tipo_dip)
                    $("#cboGrupo").val(data[i].cod_gru)
                    $("#txtNroLibro").val(data[i].nro_lib)
                    $("#txtNroFolio").val(data[i].nro_fol)
                    $("#txtRegistro").val(data[i].nro_reg)
                    $("#txtObservaciones").val(data[i].obs)

                    $("#txtNroResolucionFac").val(data[i].nrores_Fac)
                    $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', data[i].fecres_Fac);

                    //-- Llenar y seleccionar Combo Carrera Profesional
                    cbo_Cpf += '<option value="' + data[i].cod_cp + '">' + data[i].nom_cp + '</option>';
                    $("#CboCarreraR").html(cbo_Cpf)
                    $("#CboCarreraR").val(data[i].cod_cp)

                    cEspecialidad(data[i].cod_pes);
                    $("#cboEspecialidad").val(data[i].cod_esp)
                    cGrado(data[i].cod_cp, '%');
                    $("#CboGrado").val(data[i].cod_dgt)


                    $("#hdCodEgr").val(data[i].cod)
                    $("#hdCodigoAlu").val(data[i].cod_alu)
                    $("#hdCodigoAluME").val(data[i].cod_alu)
                    //--
                }

                var arr = CargaAutoridad($("#hdCodEgr").val())
                $("#cboCargo1").val(arr[0].cod_cgo)
                $("#cboCargo2").val(arr[1].cod_cgo)
                $("#cboCargo3").val(arr[2].cod_cgo)
                //31.07.2020 JBANDA
                if (arr.length > 3) {
                    $("#cboCargo4").val(arr[3].cod_cgo)
                } else {
                    $("#cboCargo4").val("")
                }

                cAutoridad($("#cboCargo1").val(), 'MAA=', '%', 1) // MAA= : 0
                cAutoridad($("#cboCargo2").val(), 'MAA=', '%', 2) // MAA= : 0
                cAutoridad($("#cboCargo3").val(), $("#cboFacultad").val(), '%', 3);
                //31.07.2020 JBANDA
                cAutoridad($("#cboCargo4").val(), 'MAA=', '%', 4)

                $("#cboAutoridad1").val(arr[0].cod_ccp)
                $("#cboAutoridad2").val(arr[1].cod_ccp)
                $("#cboAutoridad3").val(arr[2].cod_ccp)
                //31.07.2020 JBANDA
                if (arr.length > 3) {
                    $("#cboAutoridad4").val(arr[3].cod_ccp)                    
                } else {
                    $("#cboAutoridad4").val("")
                }

                //$('#datos1').html(tb);
                $('#DatosPersonales1').html(tb1);
                $('#DatosPersonales2').html(tb2);
                $('#DatosPersonales3').html(tb3);
                $('#DatosPersonales4').html(tb5);
                $('#DatosPersonales5').html(tb6);
                $('#DatosPersonales6').html(tb7);
                $('#ApellidosNombres').html(apnom);
                $('#foto').html(tb4);
                /*if (data[0].rpta == 1) {
                fnMensaje("success", data[0].msje)
                $("#txtFecha").val("")
                fnColumnaSesion(2)
                } else {
                fnMensaje("error", data[0].msje)
                }*/
                $("#RegistroExpediente").show();
                $("#Lista").hide();

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


function cActoAcademico() {
    var arr = fnActoAcademico();
    //    console.log(arr);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboActoAcad').html(str);
}


function cGrupoEgresado() {
    var arr = fnGrupoEgresado();
    //    console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $('#cboGrupo').html(str);
}


function cEspecialidad(cod_pes) {
    var arr = fnEspecialidad(cod_pes, '%'); // Lista Todos
    //console.log(arr);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboEspecialidad').html(str);
}

function cGrado(cod_cpf, vigencia) {
    var arr = fnGrado(cod_cpf, vigencia);
    //console.log(arr);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#CboGrado').html(str);
}

function CargaAutoridad(cod) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.caut + '" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/Egresado.aspx",
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


function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (fnValidar() == true) {
            fnLoading(true)
            if ($("#hdCodEgr").val() != 0) {
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=txtFechaActo]").remove();
                $("form#frmRegistro input[id=txtFechaResolucionF]").remove();
                //                $("form#frmRegistro input[id=txtFechaConsejo]").remove();
                //                $("form#frmRegistro input[id=txtFechaResolucion]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistro').append('<input type="hidden" id="txtFechaActo" name="txtFechaActo" value="' + $("#inputtxtFechaActoAcad").val() + '" />');
                $('#frmRegistro').append('<input type="hidden" id="txtFechaResolucionF" name="txtFechaResolucionF" value="' + $("#inputtxtFechaResolucionFac").val() + '" />');

                //                $('#frmRegistro').append('<input type="hidden" id="txtFechaConsejo" name="txtFechaConsejo" value="' + $("#inputtxtFechaConsejo").val() + '" />');
                //                $('#frmRegistro').append('<input type="hidden" id="txtFechaResolucion" name="txtFechaResolucion" value="' + $("#inputtxtFechaResolucion").val() + '" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=txtFechaActo]").remove();
                $("form#frmRegistro input[id=txtFechaResolucionF]").remove();
                //                $("form#frmRegistro input[id=txtFechaConsejo]").remove();
                //                $("form#frmRegistro input[id=txtFechaResolucion]").remove();

                console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GradosyTitulos/Egresado.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            fnLimpiar()
                            $("#RegistroExpediente").hide();
                            ConsultarEgresado();
                            $("#Lista").show();
                            $("#txtbusqueda").focus();
                            $("#txtbusqueda").val("");
                            //if ($("#cboTipoEstudio").val() != "" && $("#cboCarrera").val() != "") { ConsultarDenominacion() }
                            //$("#mdRegistro").modal("hide");
                        } else {
                            fnMensaje("error", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //console.log(result)
                        //                    fnMensaje("error", result)
                    }
                });
                /*} else {
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
                });*/
            }
            fnLoading(false)
        }
    } else {
        window.location.href = rpta
    }
}

//function ValidaBusqueda() {
//    if (($("#txtbusqueda").val()).length <= 2) {
//        fnMensaje("error", "Ingrese al menos 3 Caracteres.")
//        return false;
//    }
//    return true;

//}

function fnValidar() {
    if ($("#txtNroExp").val() == "") {
        $("#txtNroExp").focus();
        fnMensaje("error", "Ingrese al Número de Expediente.")
        return false;
    }
    //    if ($("#txtNroResolucionFac").val() == "") {
    //        $("#txtNroResolucionFac").focus();
    //        fnMensaje("error", "Ingrese al Número de Resolución de Facultad.")
    //        return false;
    //    }
    //    if ($("#inputtxtFechaResolucionFac").val() == "") {
    //        $("#inputtxtFechaResolucionFac").focus();
    //        fnMensaje("error", "Ingrese Correctamente la Fecha de Resolución de Facultad.")
    //        return false;
    //    }
    if ($("#cboGrupo").val() == "") {
        $("#cboGrupo").focus();
        fnMensaje("error", "Seleccione el Grupo.")
        return false;
    }
    if ($("#txtNroLibro").val() == "") {
        $("#txtNroLibro").focus();
        fnMensaje("error", "Ingrese el N° de Libro.")
        return false;
    }
    if ($("#txtNroFolio").val() == "") {
        $("#txtNroFolio").focus();
        fnMensaje("error", "Ingrese el N° de Folio.")
        return false;
    }
    if ($("#CboCarreraR").val() == "0" || $("#CboCarreraR option:selected").text() == "") {
        $("#CboCarreraR").focus();
        fnMensaje("error", "Seleccione la Carrera Profesional.")
        return false;
    }
    if ($("#CboGrado option:selected").text() == "-- SELECCIONE --" || $("#CboGrado option:selected").text() == "") {
        $("#CboGrado").focus();
        fnMensaje("error", "Seleccione el Grado/Titulo Obtenido.")
        return false;
    }
    //    var f1 = new Date();
    //    f1 = f1.getDate() + "/" + (f1.getMonth() + 1) + "/" + f1.getFullYear()
    //    if ($("#inputtxtFechaActoAcad").val() == "" || $("#inputtxtFechaActoAcad").val() > f1) {
    if ($("#inputtxtFechaActoAcad").val() == "") {
        $("#inputtxtFechaActoAcad").focus();
        fnMensaje("error", "Ingrese Correctamente la Fecha del Acto Académico.")
        return false;
    }
    if ($("#cboActoAcad option:selected").text() == "-- SELECCIONE --" || $("#cboActoAcad option:selected").text() == "") {
        $("#cboActoAcad").focus();
        fnMensaje("error", "Seleccione el Acto Académico.")
        return false;
    }
    if ($("#txtTituloTesis").val() == "" && $("#cboActoAcad option:selected").text() == "SUSTENTACIÓN DE TESIS") {
        $("#txtTituloTesis").focus();
        fnMensaje("error", "Ingrese Correctamente el Titulo de la Tesis.")
        return false;
    }
    if ($("#cboModEstudio").val() == "0") {
        $("#cboModEstudio").focus();
        fnMensaje("error", "Seleccione Modalidad de Estudio.")
        return false;
    }
    if ($("#cboEmisionDiploma").val() == "0") {
        $("#cboEmisionDiploma").focus();
        fnMensaje("error", "Seleccione el Tipo de Emision del Diploma.")
        return false;
    }

    if ($("#cboCargo1").val() == "") {
        $("#cboCargo1").focus();
        fnMensaje("error", "Seleccione El Cargo de La Primera Autoridad.")
        return false;
    }
    if ($("#cboAutoridad1").val() == "") {
        $("#cboAutoridad1").focus();
        fnMensaje("error", "Seleccione El Personal de la Primera Autoridad.")
        return false;
    }
    if ($("#cboCargo2").val() == "") {
        $("#cboCargo2").focus();
        fnMensaje("error", "Seleccione El Cargo de La Segunda Autoridad.")
        return false;
    }
    if ($("#cboAutoridad2").val() == "") {
        $("#cboAutoridad2").focus();
        fnMensaje("error", "Seleccione El Personal de la Segunda Autoridad.")
        return false;
    }
    if ($("#cboCargo3").val() == "") {
        $("#cboCargo3").focus();
        fnMensaje("error", "Seleccione El Cargo de La Tercera Autoridad.")
        return false;
    }
    if ($("#cboAutoridad3").val() == "") {
        $("#cboAutoridad3").focus();
        fnMensaje("error", "Seleccione El Personal de la Tercera Autoridad.")
        return false;
    }
    //31.07.2020 JBANDA
    /*
    if ($("#cboCargo4").val() == "") {
        $("#cboCargo4").focus();
        fnMensaje("error", "Seleccione El Cargo de La Cuarta Autoridad.")
        return false;
    }
    */
    //31.07.2020 JBANDA
    /*
    if ($("#cboAutoridad4").val() == "") {
        $("#cboAutoridad4").focus();
        fnMensaje("error", "Seleccione El Personal de la Cuarta Autoridad.")
        return false;
    }
    */
    return true;

}

function fnLimpiar() {
    $("#txtNroExp").val("")
    $("#txtNroResolucion").val("");
    $("#hdCodigoAlu").val("0")
    $("#foto").html("")
    $("#DatosPersonales1").html("")
    $("#DatosPersonales2").html("")
    $("#DatosPersonales3").html("")
    $("#ApellidosNombres").html("")
    $("#cboFacultad").val("MAA=")
    $("#CboCarreraR").val("0")
    $("#cboEspecialidad").val("MAA=")
    $("#CboGrado").val("MAA=")
    var f = new Date();
    f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
    $("#txtFechaActoAcad").jqxDateTimeInput('setDate', f);
    //    $("#ApellidosNombres").val("")
    $("#cboActoAcad").val("MAA=")
    $("#txtTituloTesis").val("")
    $("#cboModEstudio").val("0")
    $("#cboEmisionDiploma").val("0")
    $("#cboGrupo").val("");
    $("#txtNroLibro").val("");
    $("#txtNroFolio").val("");
    $("#txtRegistro").val("");
    $("#cboCargo1").val("")
    $("#cboAutoridad1").val("")
    $("#cboCargo2").val("")
    $("#cboAutoridad2").val("")
    $("#cboCargo3").val("")
    $("#cboAutoridad3").val("")
    //31.07.2020 JBANDA
    $("#cboCargo4").val("")
    $("#cboAutoridad4").val("")

    $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', '');
    $("#txtNroResolucionFac").val("")
}

function fnGuardarDatosContacto() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //        if (fnValidar() == true) {
        //fnLoading(true)
        if ($("#hdCodigoAluME").val() != 0) {
            $("form#frmEditarDatos input[id=action]").remove();
            $('#frmEditarDatos').append('<input type="hidden" id="action" name="action" value="' + ope.adc + '" />');
            var form = $("#frmEditarDatos").serializeArray();
            $("form#frmEditarDatos input[id=action]").remove();
            //$('#hdcod').remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        fnLimpiarDatosContacto()
                        //Refrescamos Datos
                        $.ajax({
                            type: "POST",
                            url: "../DataJson/GradosyTitulos/Egresado.aspx",
                            data: { action: ope.ca, cod: $("#hdCodigoAluME").val() },
                            dataType: "json",
                            cache: false,
                            async: false,
                            success: function(data) {
                                //console.log(data);
                                var tb1 = '';
                                var tb2 = '';
                                var tb3 = '';
                                var tb4 = '';
                                var tb5 = '';
                                var tb6 = '';
                                var tb7 = '';
                                var cbo_Cpf = '';
                                var apnom = '';
                                var i = 0;
                                var filas = data.length;
                                for (i = 0; i < filas; i++) {

                                    tb1 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                                    tb1 += '<li>' + data[i].coduniver + '<p>Código Universitario</p></li></ul>'
                                    $("#lblcodigo").html(data[i].coduniver)

                                    tb2 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                                    tb2 += '<li>' + data[i].tipodoc + '<p>Tipo de Documento</p></li></ul>'

                                    tb3 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                                    tb3 += '<li>' + data[i].nrodoc + '<p>N° de Documento</p></li></ul>'
                                    $("#lbldocumento").html(data[i].nrodoc)

                                    apnom += '<ul class="list-inline list-unstyled"><li><i class="fa fa-user primary-info"></i></li>'
                                    apnom += '<li>' + data[i].apepat + ' ' + data[i].apemat + ' ' + data[i].nom + '<p>Apellidos y Nombres</p></li></ul>'
                                    $("#txtapepat").val(data[i].apepat)
                                    $("#txtapemat").val(data[i].apemat)
                                    $("#txtnombres").val(data[i].nom)

                                    tb5 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                                    tb5 += '<li>' + data[i].correo + '<p>Correo Electrónico</p></li></ul>'
                                    $("#txtemail").val(data[i].correo)

                                    tb6 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                                    tb6 += '<li>' + data[i].telmov + '<p>Teléfono Movil</p></li></ul>'
                                    $("#txttelmov").val(data[i].telmov)

                                    tb7 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                                    tb7 += '<li>' + data[i].telfijo + '<p>Teléfono Fijo</p></li></ul>'
                                    $("#txttelfijo").val(data[i].telfijo)
                                }
                                $('#DatosPersonales1').html(tb1);
                                $('#DatosPersonales2').html(tb2);
                                $('#DatosPersonales3').html(tb3);
                                $('#DatosPersonales4').html(tb5);
                                $('#DatosPersonales5').html(tb6);
                                $('#DatosPersonales6').html(tb7);
                                $('#ApellidosNombres').html(apnom);
                            },
                            error: function(result) {
                                console.log(result)
                                fnMensaje("error", result)
                            }
                        });

                        $("#mdEditarDatos").modal("hide");

                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                },
                error: function(result) {
                    //console.log(result)
                    //                    fnMensaje("error", result)
                }
            });
        } else {
            fnMensaje("error", "No se Pudo Editar Información de Contacto")
        }
        //        }
    } else {
        window.location.href = rpta
    }
}


function fnLimpiarDatosContacto() {
    $("#txtemail").val("")
    $("#txttelmov").val("")
    $("#txttelfijo").val("")
    $("#txtapepat").val("")
    $("#txtapemat").val("")
    $("#txtnombres").val("")
}

