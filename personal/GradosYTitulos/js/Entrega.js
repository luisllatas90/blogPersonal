$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    if (rpta == true) {
        fnListaSesion()
        fnColumnaExpedientes($("#cboSesion").val())
        /*$("#txtFechaResolucion").jqxDateTimeInput({ width: '100%', height: '30px' });
        var f = new Date();
        f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
        $("#txtFechaResolucion").jqxDateTimeInput('setDate', f);*/
    } else {
        window.location.href = rpta
    }
    fnLoading(false);

    $("#cboSesion").change(function() {
        $("#jqxgrid").jqxGrid("clearselection");
        fnColumnaExpedientes($(this).val())
    })
    /*
    $("#cboDenominacion").change(function() {
    $("#jqxgrid").jqxGrid("clearselection");
    fnColumnaExpedientes($("#cboSesion").val())
    })
    */

    $("#cboEstado").change(function() {
        $("#jqxgrid").jqxGrid("clearselection");
        fnColumnaExpedientes($("#cboSesion").val())
    })

    $("#txtBusqueda").keyup(function(e) {
        if (e.keyCode == 13) {
            fnColumnaExpedientes($("#cboSesion").val())
        }

    })
    /*
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
    */
});

function fnListaSesion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $('#FrmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#FrmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="%" />');
        var form = $("#FrmRegistro").serializeArray();
        //        console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: form,
            dataType: "json",
            cache: false,
            asyn:false,
            success: function(data) {
            $("form#FrmRegistro input[id=action]").remove();
            $("form#FrmRegistro input[id=hdcod]").remove();
                //console.log(data);
                var tb = '';
                var i = 0;
                var filas = data.length;
                //                tb += '<option value="">--Seleccione--</option>'
                tb += '<option value="T" selected="selected">TODOS</option>'
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

function fnColumnaExpedientes() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        var opcion = ope.lee
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod', type: 'string' },
                    { name: 'cod_univer', type: 'string' },
                    { name: 'alu', type: 'string' },
                    { name: 'deno', type: 'string' },
                    { name: 'tipo_dip', type: 'string' },
                    { name: 'entregado', type: 'string' },
                    { name: 'cod_dta', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/Egresado.aspx",
                data: { "action": opcion, "hdcod": $("#cboSesion").val(), "txtBusqueda": $("#txtBusqueda").val(), "estado": $("#cboEstado").val() }

            };
        //        console.log(source);

        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
            if (dataRecord.entregado == 1) {
                return '<button class="btn btn-success" title="Click para Retirar Entrega" onclick="ActualizarEntrega(\'' + dataRecord.cod + '\',0,\'' + dataRecord.cod_dta + '\');"><i class="ion-close-round"></i></button>';
            }
            else {
                return '<button class="btn btn-warning" title="Click para Entregar" onclick="ActualizarEntrega(\'' + dataRecord.cod + '\',1,\'' + dataRecord.cod_dta + '\');"><i class="ion-checkmark"></i></button>';
            }

        }
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
                //                selectionmode: 'checkbox',
                altrows: true,
                columns: [
                /*{ text: 'Ship Name', datafield: 'ShipName', width: 250 },
                { text: 'Shipped Date', datafield: 'ShippedDate', width: 100, cellsformat: 'yyyy-MM-dd' },
                { text: 'Freight', datafield: 'Freight', width: 150, cellsformat: 'F2', cellsalign: 'right' },
                { text: 'Ship City', datafield: 'ShipCity', width: 150 },*/
                //                {text: 'cod', datafield: 'cod', visible:'false' },
                {text: 'Código Univer.', datafield: 'cod_univer', width: '10%' },
                { text: 'Alumno', datafield: 'alu', width: '40%' },
                { text: 'Diploma', datafield: 'deno', width: '40%' },
                { text: 'Tipo', datafield: 'tipo_dip', width: '5%', align: 'center' },
                //                { text: 'Entregado', datafield: 'entregado', width: '5%' },
                {text: ' ', datafield: 'cod', width: '5%', cellsrenderer: cellsrenderer }
                ]
            });
            $("#jqxgrid").jqxGrid("clearselection");
        var datainfo = $("#jqxgrid").jqxGrid('getdatainformation');
        $("#num_filas").html(datainfo.rowscount + " Registro(s)")
        fnLoading(false);

    } else {
        window.location.href = rpta
    }
}

function ActualizarEntrega(cod, entregado,cod_dta) {
    $("form#FrmRegistro input[id=action]").remove();
    $("form#FrmRegistro input[id=hdcod]").remove();
    $("form#FrmRegistro input[id=entregado]").remove();
    $("form#FrmRegistro input[id=cod_dta]").remove();
    $("form#FrmRegistro input[id=cod_tfu]").remove();
    $('#FrmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.edip + '" />');
    $('#FrmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
    $('#FrmRegistro').append('<input type="hidden" id="entregado" name="entregado" value="' + entregado + '" />');
    $('#FrmRegistro').append('<input type="hidden" id="cod_dta" name="cod_dta" value="' + cod_dta + '" />');
    $('#FrmRegistro').append('<input type="hidden" id="cod_tfu" name="cod_tfu" value="0" />');
    var form = $("#FrmRegistro").serializeArray();
    console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosyTitulos/Egresado.aspx",
        data: form,
        dataType: "json",
        cache: false,
        success: function(data) {
            $("form#FrmRegistro input[id=action]").remove();
            $("form#FrmRegistro input[id=hdcod]").remove();
            $("form#FrmRegistro input[id=entregado]").remove();
            $("form#FrmRegistro input[id=cod_dta]").remove();
            $("form#FrmRegistro input[id=cod_tfu]").remove();
           console.log(data);
            //                        fnLimpiar()
            if (data[0].rpta == 1) {
                fnMensaje("success", data[0].msje)
                fnColumnaExpedientes($("#cboSesion").val())
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




/*
function fnLimpiar() {
var f = new Date();
f = f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear()
$("#txtFechaResolucion").jqxDateTimeInput('setDate', f);
$("#txtNroResolucion").val("");
}
*/