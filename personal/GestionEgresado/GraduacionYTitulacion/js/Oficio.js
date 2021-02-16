$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(2);
    rpta = fnvalidaSession();

    if (rpta == true) {
        cGrupoEgresado();
        fnTablaExpedientes();
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $("#cboGrupo").change(function() {
        $("#jqxgrid").jqxGrid("clearselection");
        fnTablaExpedientes()
    })
    $("#cboEstado").change(function() {
        $("#jqxgrid").jqxGrid("clearselection");
        fnTablaExpedientes()
    })

    $("#btnAsignar").click(function() {
        var codigos = ""
        var rows = $("#jqxgrid").jqxGrid('selectedrowindexes');
        if (rows.length > 0) {
            if (($("#txtNroOficio").val()).trim() != "") {
                for (var i = 0; i < rows.length; i++) {
                    if (i == rows.length - 1) {
                        codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod");
                    } else {
                        codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod") + ',';
                    }
                }
                //console.log(codigos)
                $("form#FrmRegistro input[id=action]").remove();
                $("form#FrmRegistro input[id=hdcod]").remove();
                //                $("form#FrmRegistro input[id=txtFechaResol]").remove();
                $('#FrmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.aco + '" />');
                $('#FrmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigos + '" />');
                //                $('#FrmResolucion').append('<input type="hidden" id="txtFechaResol" name="txtFechaResol" value="' + $("#inputtxtFechaResolucion").val() + '" />');
                var form = $("#FrmRegistro").serializeArray();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../../DataJson/GradosyTitulos/Egresado.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        $("form#FrmRegistro input[id=action]").remove();
                        $("form#FrmRegistro input[id=hdcod]").remove();
                        //                        $("form#FrmRegistro input[id=txtFechaResol]").remove();
                        //console.log(data);
                        fnLimpiar()
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            $("#jqxgrid").jqxGrid("clearselection");
                            fnTablaExpedientes();
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
            } else { fnMensaje("error", "Ingrese Número de Oficio") }
        } else {
            fnLimpiar();
            fnMensaje("error", "Seleccione al Menos un Alumno para Asignar")
        }
    });

});

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

function fnTablaExpedientes() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        var opcion = ope.ceo
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod', type: 'string' },
                    { name: 'alu', type: 'string' },
                    { name: 'nro_exp', type: 'string' },
                    { name: 'num_of', type: 'string' }
                ],
                root: 'rows',
                url: "../../DataJson/GradosyTitulos/Egresado.aspx",
                data: { "action": opcion, "hdcod": $("#cboGrupo").val(), "estado": $("#cboEstado").val() }

            };
        //        console.log(source);

        var dataAdapter = new $.jqx.dataAdapter(source);
        // create jqxgrid.
        $("#jqxgrid").jqxGrid(
            {
                width: "100%",
                height: 450,
                source: dataAdapter,
                sortable: true,
                filterable: true,
                //pageable: true,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                selectionmode: 'checkbox',
                altrows: true,
                columns: [
                { text: 'N° Expediente', datafield: 'nro_exp', width: '15%' },
                { text: 'Alumno', datafield: 'alu', width: '56%' },
                { text: 'N° Oficio', datafield: 'num_of', width: '25%' },
                ]
            });
        var datainfo = $("#jqxgrid").jqxGrid('getdatainformation');
        $("#num_filas").html(datainfo.rowscount + " Registro(s)")
        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}

function fnLimpiar() {
    //var f = new Date();
    //f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
    //$("#txtFechaResolucion").jqxDateTimeInput('setDate', f);
    $("#txtNroOficio").val("");
}
