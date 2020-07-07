$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    fnLoading(false);
    if (rpta == true) {
        cTipoEstudio();
        fnListaSesion();
        cTipoDenominacion();
    } else {
        window.location.href = rpta
    }


    $("#cboTipoEstudio").change(function() {
        cCarreraProfesional($("#cboTipoEstudio").val(), 1);
    })
    $("#btnConsultar").click(function() {
        ConsultarEgresado();
        $("#RegistroExpediente").hide();
        $("#Lista").show();
        $("#jqxgrid").jqxGrid("clearselection");
    })
    $("#btnGenerar").click(function() {
        fnGenerarCorrelativos();
    })
    $("#btnQuitar").click(function() {
        fnQuitarCorrelativos();
    })




    $("#txtbusqueda").keyup(function(e) {
        if (e.keyCode == 13) {
            ConsultarEgresado();
            $("#RegistroExpediente").hide();
            $("#Lista").show();
            $("#jqxgrid").jqxGrid("clearselection");
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

function ObtenerCorrelativos() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.ocd + '" /></form>');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/Egresado.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            //            console.log(data);
            $("#nroDiploma").val(data[0].nro_dip);
            $("#libro_b").val(data[0].lib_b);
            $("#libro_t").val(data[0].lib_t);
            $("#libro_m").val(data[0].lib_m);
            $("#libro_d").val(data[0].lib_d);
            $("#libro_s").val(data[0].lib_s);
            $("#folio_b").val(data[0].fol_b);
            $("#folio_t").val(data[0].fol_t);
            $("#folio_m").val(data[0].fol_m);
            $("#folio_d").val(data[0].fol_d);
            $("#folio_s").val(data[0].fol_s);
            $("#cBachiller").html(data[0].nro_dipb);
            $("#cTitulo").html(data[0].nro_dipt);
            $("#cSegunda").html(data[0].nro_dips);
            $("#cMaestro").html(data[0].nro_dipm);
            $("#cDoctor").html(data[0].nro_dipd);
            //            arr = data;
        },
        error: function(result) {
            //console.log(result)
            //            arr = result;
        }
    });

    return arr;
}

function ConsultarEgresado() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        if (fnValidarBusqueda() == true) {
            fnLoading(true);
            var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod', type: 'string' },
                    { name: 'nro_exp', type: 'string' },
                    { name: 'nro_dip', type: 'string' },
                    { name: 'cod_alu', type: 'string' },
                    { name: 'cod_univer', type: 'string' },
                    { name: 'alu', type: 'string' },
                    { name: 'nom_cpf', type: 'string' },
                    { name: 'est', type: 'string' },
                    { name: 'tipo_dip', type: 'string' },
                    { name: 'abrev_dip', type: 'string' },
                    { name: 'nro_lib', type: 'string' },
                    { name: 'nro_folio', type: 'string' },
                    { name: 'fec_reg', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.lec, hdscu: $("#cboSesion").val(), hdTest: $("#cboTipoEstudio").val(), hdCarrera: $("#cboCarrera").val(), txtbuscar: $("#txtbusqueda").val(), hdTipo: $("#cboTipoDenominacion").val() }

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
                height: 400,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                //columnsresize: true,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                selectionmode: 'checkbox',
                altrows: true,
                columns: [
                //{text: 'codigo', datafield: 'cod', width: '8%' },
                {text: 'Dip.', datafield: 'abrev_dip', width: '3%', cellsalign: 'center', align: 'center' },
                { text: 'Carrera Profesional', datafield: 'nom_cpf', width: '27%' },
                { text: 'Código Univ.', datafield: 'cod_univer', width: '9%' },
                { text: 'Alumno', datafield: 'alu', width: '31%' },
                { text: 'N° Diploma', datafield: 'nro_exp', width: '10%', cellsalign: 'center', align: 'center' },
                { text: 'Libro', datafield: 'nro_lib', width: '5%', cellsalign: 'center', align: 'center' },
                { text: 'Folio', datafield: 'nro_folio', width: '4%', cellsalign: 'center', align: 'center' },
                { text: 'Fecha', datafield: 'fec_reg', width: '8%', cellsalign: 'center', align: 'center' },

                ]
            });
            ObtenerCorrelativos();
            fnLoading(false);
        }
    } else {
        window.location.href = rpta
    }
}

function fnListaSesion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $("form#frmLista input[id=action]").remove();
        $("form#frmLista input[id=hdcod]").remove();
        $('#frmLista').append('<input type="hidden" id="action" name="action" value="' + ope.lsc + '" />');
        $('#frmLista').append('<input type="hidden" id="hdcod" name="hdcod" value="%" />');
        var form = $("#frmLista").serializeArray();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                $("form#frmLista input[id=action]").remove();
                $("form#frmLista input[id=hdcod]").remove();
                //console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                tb += '<option value="" selected="selected">--Seleccione--</option>'
                for (i = 0; i < filas; i++) {
                    tb += '<option value="' + data[i].cod + '">' + data[i].nom + '</option>'
                }
                $("#cboSesion").html(tb);
                fnLoading(false);
                //fnLoading(false);
            },
            error: function(result) {
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }

}


function fnGenerarCorrelativos() {

    var codigos = ""

    var rows = ""
    rows = $("#jqxgrid").jqxGrid('selectedrowindexes');
    if (rows == undefined) {
        rows = 0;
    }
    if (rows.length > 0) {
        if (valida_Generar() == true) {
            for (var i = 0; i < rows.length; i++) {
                if (i == rows.length - 1) {
                    codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod");
                } else {
                    codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod") + ',';
                }
            }
            //console.log(codigos)
            $("form#frmLista input[id=action]").remove();
            $("form#frmLista input[id=hdcod]").remove();
            $('#frmLista').append('<input type="hidden" id="action" name="action" value="' + ope.gce + '" />');
            $('#frmLista').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigos + '" />');
            var form = $("#frmLista").serializeArray();
            $("form#frmLista input[id=action]").remove();
            $("form#frmLista input[id=hdcod]").remove();
            //        console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //                console.log(data);
                    //                fnLimpiar()
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        ObtenerCorrelativos()
                        ConsultarEgresado();
                        $("#jqxgrid").jqxGrid("clearselection");

                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                    fnLoading(false);
                    //fnLoading(false);
                },
                error: function(result) {
                    //console.log(result)
                }
            });
        }
    } else {
        fnMensaje("error", "Debe Seleccionar al menos un Egresado para Asignar Correlativos.")
    }

}


function fnQuitarCorrelativos() {

    var codigos = ""

    var rows = ""
    rows = $("#jqxgrid").jqxGrid('selectedrowindexes');
    if (rows == undefined) {
        rows = 0;
    }
    if (rows.length > 0) {
        if (valida_Generar() == true) {
            for (var i = 0; i < rows.length; i++) {
                if (i == rows.length - 1) {
                    codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod");
                } else {
                    codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod") + ',';
                }
            }
            //console.log(codigos)
            $("form#frmLista input[id=action]").remove();
            $("form#frmLista input[id=hdcod]").remove();
            $('#frmLista').append('<input type="hidden" id="action" name="action" value="' + ope.qce + '" />');
            $('#frmLista').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigos + '" />');
            var form = $("#frmLista").serializeArray();
            $("form#frmLista input[id=action]").remove();
            $("form#frmLista input[id=hdcod]").remove();
            //        console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: form,
                dataType: "json",
                cache: false,
                async: false,
                success: function(data) {
                    //                console.log(data);
                    //                fnLimpiar()
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        ObtenerCorrelativos()
                        ConsultarEgresado();
                        $("#jqxgrid").jqxGrid("clearselection");

                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                    fnLoading(false);
                    //fnLoading(false);
                },
                error: function(result) {
                    //console.log(result)
                }
            });
        }
    } else {
        fnMensaje("error", "Debe Seleccionar al menos un Egresado para Quitar Correlativos.")
    }

}


function fnValidarBusqueda() {
    if ($("#cboSesion").val() == "") {
        $("#cboSesion").focus();
        fnMensaje("error", "Seleccione Sesión de Consejo Universitario.")
        return false;
    }

    return true;
}

function valida_Generar() {
    if ($("#nroDiploma").val() == "") {
        $("#nroDiploma").focus();
        fnMensaje("error", "Ingrese el Número de Diploma Inicial.")
        return false;
    }
    //    console.log($("#nroDiploma").val().slice(0, 5))
    if ($("#nroDiploma").val().slice(0, 5) != "PE069") {
        $("#nroDiploma").focus();
        fnMensaje("error", "Ingrese correctamente el Número de Diploma debe Iniciar con PE069.")
        return false;
    }
    if ($("#nroDiploma").val().length != 11) {
        $("#nroDiploma").focus();
        fnMensaje("error", "Longitud de Diploma debe ser de 11 Caracteres de la Forma 'PE069######'")
        return false;
    }
    
    if (($("#libro_b").val()).length < 2) {
        $("#libro_b").focus();
        fnMensaje("error", "Ingrese correctamente el Libro de Bachiller")
        return false;
    }
    if ($("#libro_b").val().slice(0, 1) != 'B') {
        $("#libro_b").focus();
        fnMensaje("error", "El Libro de Bachiller debe Tener como primer caracter 'B'")
        return false;
    }

    if ($("#folio_b").val() == "") {
        $("#folio_b").focus();
        fnMensaje("error", "Ingrese correctamente el Folio de Bachiller")
        return false;
    }
    if ($("#folio_b").val() > 100 ) {
        $("#folio_b").focus();
        fnMensaje("error", "El N° de Folio de Bachiller no puede ser Mayor a 100")
        return false;
    }
    
    if ($("#libro_t").val().length < 2) {
        $("#libro_t").focus();
        fnMensaje("error", "Ingrese correctamente el Libro de Titulo")
        return false;
    }
    if ($("#libro_t").val().slice(0, 1) != 'T') {
        $("#libro_t").focus();
        fnMensaje("error", "El Libro de Titulo debe Tener como primer caracter 'T'")
        return false;
    }
    if ($("#folio_t").val() == "") {
        $("#folio_t").focus();
        fnMensaje("error", "Ingrese correctamente el Folio de Titulo")
        return false;
    }
    if ($("#folio_t").val() > 100) {
        $("#folio_t").focus();
        fnMensaje("error", "El N° de Folio de Titulo no puede ser Mayor a 100")
        return false;
    }

    if ($("#libro_s").val().length < 2) {
        $("#libro_s").focus();
        fnMensaje("error", "Ingrese correctamente el Libro de Segunda Especialidad")
        return false;
    }
    if ($("#libro_s").val().slice(0, 1) != 'S') {
        $("#libro_s").focus();
        fnMensaje("error", "El Libro de Segunda especialidad debe Tener como primer caracter 'S'")
        return false;
    }
    if ($("#folio_s").val() == "") {
        $("#folio_s").focus();
        fnMensaje("error", "Ingrese correctamente el Folio de Segunda Especialidad")
        return false;
    }
    if ($("#folio_s").val() > 100) {
        $("#folio_s").focus();
        fnMensaje("error", "El N° de Folio de Segunda Especialidadno puede ser Mayor a 100")
        return false;
    }
    
    if ($("#libro_m").val().length < 2) {
        $("#libro_m").focus();
        fnMensaje("error", "Ingrese correctamente el Libro de Maestro")
        return false;
    }
    if ($("#libro_m").val().slice(0, 1) != 'M') {
        $("#libro_m").focus();
        fnMensaje("error", "El Libro de Maestro debe Tener como primer caracter 'M'")
        return false;
    }
    if ($("#folio_m").val() == "") {
        $("#folio_m").focus();
        fnMensaje("error", "Ingrese correctamente el Folio de Maestro")
        return false;
    }
    if ($("#folio_m").val() > 100) {
        $("#folio_m").focus();
        fnMensaje("error", "El N° de Folio de Maestro no puede ser Mayor a 100")
        return false;
    }
    
    if ($("#libro_d").val().length < 2) {
        $("#libro_d").focus();
        fnMensaje("error", "Ingrese correctamente el Libro de Doctor")
        return false;
    }
    if ($("#libro_d").val().slice(0, 1) != 'D') {
        $("#libro_d").focus();
        fnMensaje("error", "El Libro de Doctor debe Tener como primer caracter 'D'")
        return false;
    }
    if ($("#folio_d").val() == "") {
        $("#folio_d").focus();
        fnMensaje("error", "Ingrese correctamente el Folio de Doctor")
        return false;
    }
    if ($("#folio_d").val() > 100) {
        $("#folio_d").focus();
        fnMensaje("error", "El N° de Folio de Doctor no puede ser Mayor a 100")
        return false;
    }
    
    return true;
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