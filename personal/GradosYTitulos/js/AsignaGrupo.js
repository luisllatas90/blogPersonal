$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    fnLoading(false);
    if (rpta == true) {
        cTipoEstudio();
        fnListaSesion();
        cGrupoEgresado();
    } else {
        window.location.href = rpta
    }


    $("#cboTipoEstudio").change(function() {
        cCarreraProfesional($("#cboTipoEstudio").val(), 1);
    })
    $("#btnConsultar").click(function() {
        ConsultarEgresado();
        $("#Lista").show();
        $("#jqxgrid").jqxGrid("clearselection");
    })
    $("#btnAsignar").click(function() {
        fnAsignarGrupo();
        cGrupoEgresado();
    });
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
                    { name: 'fec_reg', type: 'string' },
                    { name: 'grupo', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.leg, hdscu: $("#cboSesion").val(), hdTest: $("#cboTipoEstudio").val(), hdCarrera: $("#cboCarrera").val(), txtbuscar: $("#txtbusqueda").val() }

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
                {text: 'Dip.', datafield: 'abrev_dip', width: '4%', cellsalign: 'center', align: 'center' },
                { text: 'Carrera Profesional', datafield: 'nom_cpf', width: '30%' },
                //                { text: 'Código Univ.', datafield: 'cod_univer', width: '9%' },
                {text: 'Alumno', datafield: 'alu', width: '35%' },
                { text: 'N° Diploma', datafield: 'nro_exp', width: '14%', cellsalign: 'center', align: 'center' },
                //                { text: 'Libro', datafield: 'nro_lib', width: '5%', cellsalign: 'center', align: 'center' },
                //                { text: 'Folio', datafield: 'nro_folio', width: '4%', cellsalign: 'center', align: 'center' },
                {text: 'Grupo', datafield: 'grupo', width: '12%', cellsalign: 'center', align: 'center' },

                ]
            });
            var datainfo = $("#jqxgrid").jqxGrid('getdatainformation');
            $("#num_filas").html(datainfo.rowscount + " Registro(s)")
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
        $('#frmLista').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
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


function fnAsignarGrupo() {
    var codigos = ""
    var rows = $("#jqxgrid").jqxGrid('selectedrowindexes');
    if (rows.length > 0) {
        for (var i = 0; i < rows.length; i++) {
            if (i == rows.length - 1) {
                codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod");
            } else {
                codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod") + ',';
            }
        }
        //        console.log(codigos)
        $("form#FrmAsignar input[id=action]").remove();
        $("form#FrmAsignar input[id=hdcod]").remove();
        $('#FrmAsignar').append('<input type="hidden" id="action" name="action" value="' + ope.age + '" />');
        $('#FrmAsignar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigos + '" />');
        var form = $("#FrmAsignar").serializeArray();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Egresado.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                $("form#FrmAsignar input[id=action]").remove();
                $("form#FrmAsignar input[id=hdcod]").remove();
                //                console.log(data);
                $("#cboGrupo").val("");
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    $("#jqxgrid").jqxGrid("clearselection");
                    ConsultarEgresado()
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
    } else {
        fnMensaje("error", "Seleccione al Menos un Egresado para Asignar")
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
