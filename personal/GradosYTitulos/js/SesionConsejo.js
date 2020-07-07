$(document).ready(function() {
    fnLoading(true);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();

    if (rpta == true) {
        $("#txtFechaSesion").jqxDateTimeInput({ width: '100%', height: '30px' });
        ConsultarSesion();
    } else {
        window.location.href = rpta
    }
    fnLoading(false)

    $("#btnAgregar").click(function() {
        //        Limpiar();
        $("#hdcod").val("0")
        $("#txtFechaSesion").val("")
        $("#mdRegistro").modal("show");
    });

    $("#btnConsultar").click(function() {

        ConsultarSesion();
    })

    //    $("#jqxlistbox").on('checkChange', function(event) {
    //        fnColumnaExpedientes($(this).val(), 1)

    //    })
    //    $("#btnAsignar").click(function() {
    //        var codigos = ""
    //        var rows = $("#jqxgrid1").jqxGrid('selectedrowindexes');
    //        if (rows.length > 0) {
    //            if ($("#CboSesiones").val() != "") {

    //                for (var i = 0; i < rows.length; i++) {
    //                    if (i == rows.length - 1) {
    //                        codigos += $('#jqxgrid1').jqxGrid('getcellvalue', rows[i], "cod");
    //                    } else {
    //                        codigos += $('#jqxgrid1').jqxGrid('getcellvalue', rows[i], "cod") + ',';
    //                    }
    //                }

    //                MoverAlumnoSesion(codigos, $("#CboSesiones").val(), 1);
    //                $("#jqxgrid1").jqxGrid("clearselection");
    //                $("#jqxgrid2").jqxGrid("clearselection");
    //                fnColumnaExpedientes($("#CboSesiones").val(), 2)
    //                fnColumnaExpedientes($("#CboSesiones").val(), 3)
    //            } else {
    //                fnMensaje("error", "Debe Seleccionar una Sesión para Asignar")
    //            }
    //        } else {
    //            fnMensaje("error", "Debe Seleccionar al Menos Un Egresado para Asignar a la Sesión.")
    //        }

    //    })
    //    $("#btnRegistrar").click(function() {
    //        //        alert($(this).text())
    //        if ($(this).text() == "Registrar") {
    //            $("#ListaSesion").hide();
    //            $("#RegistraSesion").show();
    //            $(this).text("Volver");
    //            $(this).removeAttr("class");
    //            $(this).attr("class", "btn btn-danger");
    //            fnColumnaSesion(2)
    ////            fnColumnaExpedientes(0, 2)
    //            //            $("#btnExportar").hide();
    //        } else {
    //            $("#ListaSesion").show();
    //            $("#RegistraSesion").hide();
    //            $(this).text("Registrar");
    //            $(this).removeAttr("class");
    //            $(this).attr("class", "btn btn-primary");
    //            fnColumnaSesion(1)
    //            $("#jqxgrid").jqxGrid("clear");
    //            //            $("#btnExportar").show();
    //        }

    //    })

    //    //    $("#btnExportar").click(function() {
    //    //        //    $("#jqxgrid").jqxGrid('exportdata', 'xls', 'Expedientes');
    //    //        //        console.log($("#jqxgrid").jqxGrid('exportdata', 'json'));
    //    //        JSONToCSVConvertor("1", true)
    //    //    })

    //    $("#CboSesiones").change(function() {
    //        
    //        if ($("#CboSesiones").val() == "") {
    //            $("#lblsesion").html("")
    //            $("#btnAsignar").attr("style","display:none")
    //            $("#jqxgrid1").jqxGrid("clear");
    //            $("#jqxgrid2").jqxGrid("clear");
    //        } else {
    //        var sesion = $("#CboSesiones option:selected").text()
    //        $("#lblsesion").html("Lista de Egresados de la SESIÓN " + sesion.substring(6, sesion.length))
    //        fnColumnaExpedientes($(this).val(), 2)
    //        fnColumnaExpedientes($(this).val(), 3)
    //        $("#btnAsignar").removeAttr("style")
    //        }

    //    })
    //    $("#btnAgregarSesion").click(function() {
    //        Registrar_Sesion();
    //    })

});


function ConsultarSesion() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true);
        var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'cod', type: 'string' },
                    { name: 'nom', type: 'string' },
                    { name: 'fec', type: 'string' }
                ],
                root: 'rows',
                url: "../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
                data: { action: ope.lst, hdcod: '%' }

            };
        //        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgrid").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Editar" onclick="CargaDatos(\'' + dataRecord.cod + '\');"><i class="ion-android-hand"></i></button>';
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
                {text: 'Descripción', datafield: 'nom', width: '65%' },
                { text: 'Fecha de Sesión', datafield: 'fec', width: '30%' },
                { text: ' ', datafield: 'cod', width: '5%', cellsrenderer: cellsrenderer
                }
                ]
            });
    } else {
        window.location.href = rpta
    }
}

function CargaDatos(cod) {
//    console.log(cod);
    if (cod != "") {
        $.ajax({
            type: "POST",
            url: "../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
            data: { action: ope.lst, hdcod: cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
//                console.log(data);
                $("#txtFechaSesion").jqxDateTimeInput('setDate', data[0].fec);
                //                if (data[0].vig == 1) {
                //                    $("#chkvigencia").prop("checked", true);
                //                } else {
                //                    $("#chkvigencia").prop("checked", false);
                //                }
                $("#hdcod").val(data[0].cod)
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

    if ($("#txtFechaSesion").val() != "") {
        if ($("#hdcod").val() == "0") {
            $("form#frmRegistro input[id=action]").remove();
            $("form#frmRegistro input[id=txtfecha]").remove();
            $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
            $('#frmRegistro').append('<input type="hidden" id="txtfecha" name="txtfecha" value="' + $("#inputtxtFechaSesion").val() + '" />');
            var form = $("#frmRegistro").serializeArray();
            $("form#frmRegistro input[id=action]").remove();
            $("form#frmRegistro input[id=txtfecha]").remove();
            //$('#hdcod').remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    //                console.log(data);
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        $("#txtFechaSesion").val("")
                        ConsultarSesion();
                        $("#mdRegistro").modal("hide");
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                },
                error: function(result) {
                    console.log(result)
                    fnMensaje("error", result)
                }
            });
        } else {
            $("form#frmRegistro input[id=action]").remove();
            $("form#frmRegistro input[id=txtfecha]").remove();
            $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
            $('#frmRegistro').append('<input type="hidden" id="txtfecha" name="txtfecha" value="' + $("#inputtxtFechaSesion").val() + '" />');
            var form = $("#frmRegistro").serializeArray();
            $("form#frmRegistro input[id=action]").remove();
            $("form#frmRegistro input[id=txtfecha]").remove();
            //$('#hdcod').remove();
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GradosyTitulos/SesionConsejoUniversitario.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    //                console.log(data);
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        $("#txtFechaSesion").val("")
                        ConsultarSesion();
                        $("#mdRegistro").modal("hide");
                    } else {
                        fnMensaje("error", data[0].msje)
                    }
                },
                error: function(result) {
                    console.log(result)
                    fnMensaje("error", result)
                }
            });

        }
    } else {
        fnMensaje("error", "Debe Seleccionar una Fecha de Sesión.")

    }
}

