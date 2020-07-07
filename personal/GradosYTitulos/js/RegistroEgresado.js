$(document).ready(function() {
    fnLoading(true);
    setTimeout(fnLoading, 1200);

    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    if (rpta == true) {
        cFacultad();
        cActoAcademico();
        cGrupoEgresado();
        // Create a jqxDateTimeInput
        $("#txtFechaActoAcad").jqxDateTimeInput({ width: '100%', height: '30px' });
        $("#txtFechaActoAcad").jqxDateTimeInput('setDate', '');
        //        var f = new Date();
        //        f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
        //        $("#txtFechaActoAcad").jqxDateTimeInput('setDate', f);
        $("#txtFechaResolucionFac").jqxDateTimeInput({ width: '100%', height: '30px' })
        $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', '');
        $("#RegistroExpediente").hide();
        //        fnLoading(false);

    } else {
        window.location.href = rpta
    }

    $("#btnGuardar").click(function() {
        fnGuardar();
    })

    $("#btnCancelarReg").click(function() {
        $("#RegistroExpediente").hide();
        $("#Lista").show();
    })


    $("#cboCargo1").change(function() {
        cAutoridad($(this).val(), 'MAA=', 1, 1) // MAA= : 0
    })
    $("#cboCargo2").change(function() {
        cAutoridad($(this).val(), 'MAA=', 1, 2) // MAA= : 0
    })
    $("#cboCargo3").change(function() {
        cAutoridad($(this).val(), $("#cboFacultad").val(), 1, 3)
    })

    $("#cboFacultad").change(function() {
        cAutoridad($("#cboCargo3").val(), $(this).val(), 1, 3) // MAA= : 0
    })

    $("#txtbusqueda").keyup(function(e) {
        if (e.keyCode == 13) {
            if (ValidaBusqueda() == true) {
                ConsultarAlumno();
                $("#mdCoincidencias").modal("show");
            }
        }

    })

    $("#btnEditarDatos").click(function() {
        $("#mdEditarDatos").modal("show");
    })

    $("#btnGuardarDatos").click(function() {
        fnGuardarDatosContacto();
    })
    $("#cboEmisionDiploma").change(function() {
        BuscaFechaActo();
    });
    $("#CboGrado").change(function() {
        BuscaFechaActo();
    });
    //  fnLoading(false);
});


function cCargo() {
    var arr = fnCargo();
    //console.log(arr);
    var n = arr.length;
    var str = "";
    str += '<option value=""s selected="selected" >-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboCargo1').html(str);
    $('#cboCargo2').html(str);
    $('#cboCargo3').html(str);
}

function cAutoridad(cod_cgo, cod_fac, vig, cbo) {
    var arr = fnAutoridad(cod_cgo, cod_fac, vig);
    //console.log(arr);
    var n = arr.length;
    var str = "";
    //    str += '<option value="">-- Seleccione -- </option>';
    if (n == 1) {
        selec = "selected='selected'";
        str += '<option value="">-- Seleccione -- </option>';
    } else {
        selec = ""
        str += '<option value="" selected="selected">-- Seleccione -- </option>';
    }
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '" ' + selec + '>' + arr[i].nombre + '</option>';
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
}

function ConsultarAlumno() {
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
                    { name: 'tipodoc', type: 'string' },
                    { name: 'nrodoc', type: 'string' },
                    { name: 'coduniver', type: 'string' },
                    { name: 'alu', type: 'string' },
                    { name: 'test', type: 'string' },
                    { name: 'cpf', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.ba, txtbuscar: $("#txtbusqueda").val() }

            };
        //        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgridModal").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Editar" onclick="CargaDatosAlumno(\'' + dataRecord.cod + '\');"><i class="ion-android-hand"></i></button>';
        }

        $("#jqxgridModal").jqxGrid(
            {
                width: "100%",
                height: 500,
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
                {text: 'Tipo Doc', datafield: 'tipodoc', width: '8%' },
                { text: 'N° Doc', datafield: 'nrodoc', width: '10%' },
                { text: 'Cod. Univer.', datafield: 'coduniver', width: '10%' },
                { text: 'Alumno', datafield: 'alu', width: '40%' },
                { text: 'Carrera Profesional', datafield: 'cpf', width: '28%' },
                { text: ' ', datafield: 'cod', width: '4%', cellsrenderer: cellsrenderer }
                ]
            });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function CargaDatosAlumno(cod) {
    if (cod != "") {
        fnLoading(true);
        setTimeout(fnLoading, 1000);
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Egresado.aspx",
            data: { action: ope.ca, cod: cod },
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
                    /*tb += '<li>DNI: <span>' + data[i].cNumDoc + '</span></li>';
                    tb += '<li>Apellido Paterno: <span>' + data[i].cApePat + '</span></li>';
                    tb += '<li>Apellido Materno: <span>' + data[i].cApeMat + '</span></li>';
                    tb += '<li>Nombres: <span>' + data[i].cNombres + '</span></li>';
                    tb += '<li>Fecha Nacimiento: <span>' + data[i].cFecNac + '</span></li>';*/
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
                    tb4 += '<img border="1" src="//intranet.usat.edu.pe/imgestudiantes/' + data[i].foto + '" width="100" heigth="118" style="width:98px;height:115px" alt="Sin Foto">'
                    //                    tb4 += '<button type="button" id="btnEditarDatos" name="btnEditarDatos" class="btn btn-orange">EDITAR</button>'

                    tb5 += '<ul class="list-inline list-unstyled"><li><i class="fa fa-at primary-info"></i></li>'
                    tb5 += '<li>' + data[i].correo + '<p>Correo Electrónico</p></li></ul>'
                    $("#txtemail").val(data[i].correo)

                    tb6 += '<ul class="list-inline list-unstyled"><li><i class="fa fa-mobile primary-info"></i></li>'
                    tb6 += '<li>' + data[i].telmov + '<p>Teléfono Movil</p></li></ul>'
                    $("#txttelmov").val(data[i].telmov)

                    tb7 += '<ul class="list-inline list-unstyled"><li><i class="fa fa-phone primary-info"></i></li>'
                    tb7 += '<li>' + data[i].telfijo + '<p>Teléfono Fijo</p></li></ul>'
                    $("#txttelfijo").val(data[i].telfijo)

                    $("#txtTituloTesis").val(data[i].nom_tes)
                    $("#hdCodigoTes").val(data[i].cod_tes)
                    $("#cboFacultad").val(data[i].cod_fac)

                    //-- Llenar y seleccionar Combo Carrera Profesional
                    cbo_Cpf += '<option value="' + data[i].cod_cp + '">' + data[i].nom_cp + '</option>';
                    $("#CboCarrera").html(cbo_Cpf)
                    $("#CboCarrera").val(data[i].cod_cp)

                    cEspecialidad(data[i].cod_pes);
                    cGrado(data[i].cod_cp, 1);
                    $("#verificaBachiller").html(data[i].ver_bach)
                    $("#hdCodigoAlu").val(data[i].cod)
                    $("#hdCodigoAluME").val(data[i].cod)
                    var arr = fnCargosxTest(data[i].cod_test)
                }

                cCargo();
                $("#cboCargo1").val(arr[0].cod)
                $("#cboCargo2").val(arr[1].cod)
                $("#cboCargo3").val(arr[2].cod)

                //$('#datos1').html(tb);
                cAutoridad($("#cboCargo1").val(), 'MAA=', 1, 1) // MAA= : 0


                cAutoridad($("#cboCargo2").val(), 'MAA=', 1, 2) // MAA= : 0
                cAutoridad($("#cboCargo3").val(), $("#cboFacultad").val(), 1, 3)
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

                $("#txtNroExp").val('-')
                $("#RegistroExpediente").show();
                $("#mdCoincidencias").modal("hide");

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
    var arr = fnEspecialidad(cod_pes, '1'); // Solo Filtro Vigentes para El Registro
    //console.log(arr);
    var n = arr.length;
    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#CboEspecialidad').html(str);
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


function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //        fnLoading(true);
        if (fnValidar() == true) {
            fnLoading(true)
            setTimeout(fnLoading, 1000);
            if ($("#hdcod").val() == 0) {
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=txtFechaActo]").remove();
                $("form#frmRegistro input[name=cboGrupo]").remove();
                $("form#frmRegistro input[id=txtNroLibro]").remove();
                $("form#frmRegistro input[id=txtNroFolio]").remove();
                $("form#frmRegistro input[id=txtRegistro]").remove();
                $("form#frmRegistro input[id=txtFechaResolucionF]").remove();

                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmRegistro').append('<input type="hidden" id="txtFechaActo" name="txtFechaActo" value="' + $("#inputtxtFechaActoAcad").val() + '" />');
                $('#frmRegistro').append('<input type="hidden" id="txtFechaResolucionF" name="txtFechaResolucionF" value="' + $("#inputtxtFechaResolucionFac").val() + '" />');

                $('#frmRegistro').append('<input type="hidden" id="cboGrupo" name="cboGrupo" value="" />');
                $('#frmRegistro').append('<input type="hidden" id="txtNroLibro" name="txtNroLibro" value="" />');
                $('#frmRegistro').append('<input type="hidden" id="txtNroFolio" name="txtNroFolio" value="" />');
                $('#frmRegistro').append('<input type="hidden" id="txtRegistro" name="txtRegistro" value="" />');
                var form = $("#frmRegistro").serializeArray();
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=txtFechaActo]").remove();
                $("form#frmRegistro input[id=cboGrupo]").remove();
                $("form#frmRegistro input[id=txtNroLibro]").remove();
                $("form#frmRegistro input[id=txtNroFolio]").remove();
                $("form#frmRegistro input[id=txtRegistro]").remove();
                $("form#frmRegistro input[id=txtFechaResolucionF]").remove();
                //$('#hdcod').remove();
                //                console.log(form);
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
                            fnLimpiar()
                            $("#RegistroExpediente").hide();
                            $("#txtbusqueda").focus();
                            //if ($("#cboTipoEstudio").val() != "" && $("#cboCarrera").val() != "") { ConsultarDenominacion() }
                            //$("#mdRegistro").modal("hide");
                            $("#txtbusqueda").val("");
                            //                            fnLoading(false);
                        } else {
                            fnMensaje("error", data[0].msje)
                            //                            fnLoading(false);
                        }

                    },
                    error: function(result) {
                        //console.log(result)
                        //                    fnMensaje("error", result)
                        //                        fnLoading(false);
                    }
                });

            }
            //fnLoading(false)
        } else {
            //            fnLoading(false);
        }
        //        fnLoading(false);
    } else {
        window.location.href = rpta
    }

}

function ValidaBusqueda() {
    if (($("#txtbusqueda").val()).length <= 2) {
        fnMensaje("error", "Ingrese al menos 3 Caracteres.")
        return false;
    }
    return true;

}

function fnValidar() {
    if ($("#txtNroExp").val() == "") {
        $("#txtNroExp").focus();
        fnMensaje("error", "Ingrese al Número de Expediente.")
        return false;
    }
    if ($("#CboCarrera").val() == "0") {
        $("#CboCarrera").focus();
        fnMensaje("error", "Seleccione la Carrera Profesional.")
        return false;
    }
    if ($("#CboGrado option:selected").text() == "-- SELECCIONE --") {
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
    if ($("#cboActoAcad option:selected").text() == "-- SELECCIONE --") {
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
    //    if ($("#cboGrupo").val() == "") {
    //        $("#cboGrupo").focus();
    //        fnMensaje("error", "Seleccione el Grupo.")
    //        return false;
    //    }
    //    if ($("#txtNroLibro").val() == "") {
    //        $("#txtNroLibro").focus();
    //        fnMensaje("error", "Ingrese el N° de Libro.")
    //        return false;
    //    }
    //    if ($("#txtNroFolio").val() == "") {
    //        $("#txtNroFolio").focus();
    //        fnMensaje("error", "Ingrese el N° de Folio.")
    //        return false;
    //    }
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
    return true;

}


function fnLimpiar() {
    $("#txtNroExp").val("")
    $("#hdCodigoAlu").val("0")
    $("#foto").html("")
    $("#DatosPersonales1").html("")
    $("#DatosPersonales2").html("")
    $("#DatosPersonales3").html("")
    $("#ApellidosNombres").html("")
    $("#cboFacultad").val("MAA=")
    $("#CboCarrera").val("0")
    $("#CboEspecialidad").val("MAA=")
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

    $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', '');
    $("#txtNroResolucionFac").val("")
    $("#txtObservaciones").val("")
    
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
    $("#lbldocumento").html("")
    $("#lblcodigo").html("")

}



function BuscaFechaActo() {
    if ($("#cboEmisionDiploma").val() == "D" && $("#CboGrado option:selected").text() != "-- SELECCIONE --") {
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Egresado.aspx",
            data: { action: ope.cfa, cod: $("#hdCodigoAlu").val(), cod_dgt: $("#CboGrado").val() },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                $("#txtFechaActoAcad").jqxDateTimeInput('setDate', data[0].fecha);
                $("#cboActoAcad").val(data[0].tipo)
            },
            error: function(result) {
                console.log(result)
                //fnMensaje("error", result)
            }
        });


    }

}