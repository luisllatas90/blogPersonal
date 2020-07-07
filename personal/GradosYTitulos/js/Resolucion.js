$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    if (rpta == true) {
        fnListaSesion()
        $("#txtFechaResolucion").jqxDateTimeInput({ width: '100%', height: '30px' });
        var f = new Date();
        f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
        $("#txtFechaResolucion").jqxDateTimeInput('setDate', f);
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $("#cboSesion").change(function() {
    fnListaDenominaciones();
        $("#jqxgrid").jqxGrid("clearselection");
        fnColumnaExpedientes($(this).val())
    })

    $("#cboDenominacion").change(function() {
        $("#jqxgrid").jqxGrid("clearselection");
        fnColumnaExpedientes($("#cboSesion").val())
    })


    $("#cboEstado").change(function() {
        $("#jqxgrid").jqxGrid("clearselection");
        fnColumnaExpedientes($("#cboSesion").val())
    })


    $("#btnAsignar").click(function() {
        var codigos = ""
        var rows = $("#jqxgrid").jqxGrid('selectedrowindexes');
        if (rows.length > 0) {
            if ($("#txtNroResolucion").val() != "") {
                for (var i = 0; i < rows.length; i++) {
                    if (i == rows.length - 1) {
                        codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod");
                    } else {
                        codigos += $('#jqxgrid').jqxGrid('getcellvalue', rows[i], "cod") + ',';
                    }
                }
                //console.log(codigos)
                $("form#FrmResolucion input[id=action]").remove();
                $("form#FrmResolucion input[id=hdcod]").remove();
                $("form#FrmResolucion input[id=txtFechaResol]").remove();
                $('#FrmResolucion').append('<input type="hidden" id="action" name="action" value="' + ope.acr + '" />');
                $('#FrmResolucion').append('<input type="hidden" id="hdcod" name="hdcod" value="' + codigos + '" />');
                $('#FrmResolucion').append('<input type="hidden" id="txtFechaResol" name="txtFechaResol" value="' + $("#inputtxtFechaResolucion").val() + '" />');
                var form = $("#FrmResolucion").serializeArray();
                //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/GradosyTitulos/Egresado.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        $("form#FrmResolucion input[id=action]").remove();
                        $("form#FrmResolucion input[id=hdcod]").remove();
                        $("form#FrmResolucion input[id=txtFechaResol]").remove();
                        //console.log(data);
                        fnLimpiar()
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            $("#jqxgrid").jqxGrid("clearselection");
                            fnColumnaExpedientes($("#cboSesion").val());
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
            } else { fnMensaje("error", "Ingrese Número de Resolución") }
        } else {

            fnMensaje("error", "Seleccione al Menos un Alumno para Asignar")
        }
    });

});

function fnListaSesion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
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

function fnListaDenominaciones() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $('#frmLista').append('<input type="hidden" id="action" name="action" value="' + ope.lstden + '" />');
        $('#frmLista').append('<input type="hidden" id="hdcod" name="hdcod" value="' + $("#cboSesion").val() + '" />');
        var form = $("#frmLista").serializeArray();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/Denominacion.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                $("form#frmLista input[id=action]").remove();
                $("form#frmLista input[id=hdcod]").remove();
                //console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                tb += '<option value="0" selected="selected">TODOS</option>'
                for (i = 0; i < filas; i++) {
                    tb += '<option value="' + data[i].cod_gt + '">' + data[i].nom_gt + '</option>'
                }
                $("#cboDenominacion").html(tb);
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


function fnColumnaExpedientes(codigos_sesion) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        var opcion = ope.cer
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod', type: 'string' },
                    { name: 'Alumno', type: 'string' },
                    { name: 'deno', type: 'string' },
                    { name: 'nro_res', type: 'string' },
                    { name: 'fec_res', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: { "action": opcion, "hdcod": codigos_sesion, "cod_den": $("#cboDenominacion").val(), "estado": $("#cboEstado").val() }

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
                /*{ text: 'Ship Name', datafield: 'ShipName', width: 250 },
                { text: 'Shipped Date', datafield: 'ShippedDate', width: 100, cellsformat: 'yyyy-MM-dd' },
                { text: 'Freight', datafield: 'Freight', width: 150, cellsformat: 'F2', cellsalign: 'right' },
                { text: 'Ship City', datafield: 'ShipCity', width: 150 },*/
                //                {text: 'cod', datafield: 'cod', visible:'false' },
                {text: 'Denominación', datafield: 'deno', width: '24%' },
                { text: 'Alumno', datafield: 'Alumno', width: '40%' },
                { text: 'N° Resolución', datafield: 'nro_res', width: '20%' },
                { text: 'Fecha', datafield: 'fec_res', width: '12%' },
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
    var f = new Date();
    f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
    $("#txtFechaResolucion").jqxDateTimeInput('setDate', f);
    $("#txtNroResolucion").val("");
}
