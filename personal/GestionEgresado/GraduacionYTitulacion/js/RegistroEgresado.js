$(document).ready(function() {
    fnLoading(true);
    setTimeout(fnLoading, 1200);

    ope = fnOperacion(2);
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
        $("#txtFechaResolucionFac").jqxDateTimeInput({ width: '100%', height: '30px', disabled: true })
        $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', '');
        $("#RegistroExpediente").hide();
        //        fnLoading(false);
        ConsultarTramites('P', '%')

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
    //11.08.2020 JBANDA
    $("#cboCargo4").change(function() {
        cAutoridad($(this).val(), 'MAA=', '%', 4) // MAA= : 0
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

    $("#btnObservar").click(function() {
        $("#jqxGridRequisitos").jqxGrid("clearselection");
        CargaRequisitos($("#txtNroExp").val());
        $("#mdObservaciones").modal("show");
    })
    $("#btnGuardarObservacion").click(function() {
        var seleccionados = ""
        var nombre = " "
        var detalle = ""
        var rows = $("#jqxGridRequisitos").jqxGrid('selectedrowindexes');
        rows = rows.sort(rows[i]);

        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                nombre = $('#jqxGridRequisitos').jqxGrid('getcellvalue', rows[i], "nombre")
                detalle = $('#jqxGridRequisitos').jqxGrid('getcellvalue', rows[i], "detalle")
                if (nombre != null) {
                    if (i == 0) {
                        seleccionados += '|:|' + nombre + ": " + detalle;
                    } else {
                        if ($('#jqxGridRequisitos').jqxGrid('getcellvalue', rows[i - 1], "nombre") == nombre) {
                            seleccionados += ', ' + detalle;
                        } else {
                            seleccionados += "|:||:|" + nombre + ": " + detalle;
                        }
                    }
                }
                if (i == (rows.length - 1)) { seleccionados += ".|:||:|" }
            }
            if ($("#txtObservacion").val().trim() != "") {
                ObservarTramite($("#hddta").val(), $("#txtObservacion").val(), seleccionados)
            } else {
                fnMensaje("error", "Ingrese observación");
            }
        } else {
            fnMensaje("error", "Debe seleccionar un requisito incumplido");
        }
    })
    $("#btnBuscar").click(function() {
        $("#RegistroExpediente").hide();
        $("#Lista").show();

        if ($("#cboEstado").val() == "R") {
            ConsultarEgresado($("#cboEstado").val(), $("#txtbusqueda").val())
        } else {
            ConsultarTramites($("#cboEstado").val(), $("#txtbusqueda").val())

        }
    })
    $("#btnVer").click(function() {
        $("#mdArchivos").modal("show");
    })
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
    //11.08.2020 JBANDA
    $('#cboCargo4').html(str);
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
    //11.08.2020 JBANDA
    if (cbo == 4) {
        $('#cboAutoridad4').html(str);
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
                    { name: 'dta', type: 'string' },
                    { name: 'trl', type: 'string' },
                    { name: 'cpf', type: 'string' }
                ],
                root: 'rows',
                url: "../../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.ba, txtbuscar: $("#txtbusqueda").val() }

            };
        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxgridModal").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Editar" onclick="CargaDatosAlumno(\'' + dataRecord.cod + '\',\'' + dataRecord.tip + '\',\'' + dataRecord.emi + '\',0,\'' + dataRecord.trl + '\',\'' + dataRecord.dta + '\');"><i class="ion-android-hand"></i></button>';
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


function CargaDatosAlumno(cod, tipo, emision, archivo, expediente, dta, estado, trl) {
    console.log(trl);
    console.log(dta);
    if (cod != "") {
        fnLimpiar();
        fnLoading(true);
        setTimeout(fnLoading, 1000);
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosyTitulos/Egresado.aspx",
            data: { action: ope.ca, cod: cod, tipo: tipo },
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
                    tb1 += '<li>' + data[i].coduniver + '<p>Código Univer.</p></li></ul>'
                    $("#lblcodigo").html(data[i].coduniver)

                    tb2 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                    tb2 += '<li>' + data[i].tipodoc + '<p>Tipo Documento</p></li></ul>'

                    tb3 += '<ul class="list-inline list-unstyled"><li><i class="fa  fa-credit-card primary-info"></i></li>'
                    tb3 += '<li>' + data[i].nrodoc + '<p>N° Documento</p></li></ul>'
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
                    cGradoRegistro(data[i].cod_cp, tipo);
                    $("#verificaBachiller").html(data[i].ver_bach)
                    $("#hdCodigoAlu").val(data[i].cod)
                    $("#hdCodigoAluME").val(data[i].cod)
                    var arr = fnCargosxTest(data[i].cod_test)

                    if (data[i].fecha_acto != "") {
                        $("#txtFechaActoAcad").jqxDateTimeInput('setDate', data[i].fec_acto);
                    } else {
                        $("#txtFechaActoAcad").jqxDateTimeInput('setDate', '');
                    }
                    if (data[i].tipo_acto != "") {
                        $("#cboActoAcad").val(data[i].tipo_acto);

                    } else {
                        $("#cboActoAcad").val("MAA=");
                    }
                    if (data[i].fechareso != "") {
                        $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', data[i].fechareso);
                    } else {
                        $("#txtFechaResolucionFac").jqxDateTimeInput('setDate', '');
                    }
                    $("#txtNroResolucionFac").val(data[i].nroreso)
                    $("#ArchivoResolucion").html("");
                    if (data[i].archivoresofac != "") {
                        $("#ArchivoResolucion").html("<button input type='button' class='btn btn-sm btn-success btn-radius' title='Descargar resolución de facultad' onclick='fnDescargar(\"" + data[i].archivoresofac + "\")' ><i class='ion-android-download'></i></button>")
                    }
                }

                cCargo();
                $("#cboCargo1").val(arr[0].cod)
                $("#cboCargo2").val(arr[1].cod)
                $("#cboCargo3").val(arr[2].cod)
                //11.08.2020 JBANDA
                $("#cboCargo4").val(arr[3].cod)

                //$('#datos1').html(tb);
                cAutoridad($("#cboCargo1").val(), 'MAA=', 1, 1) // MAA= : 0
                cAutoridad($("#cboCargo2").val(), 'MAA=', 1, 2) // MAA= : 0
                cAutoridad($("#cboCargo3").val(), $("#cboFacultad").val(), 1, 3)
                //11.08.2020 JBANDA
                cAutoridad($("#cboCargo4").val(), 'MAA=', '%', 4)

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

                /*$("#txtNroExp").val('-')*/
                $("#txtNroExp").val(expediente);
                $("#RegistroExpediente").show();
                $("#Lista").hide();
                $("#mdCoincidencias").modal("hide");

                $("#cboEmisionDiploma").val(emision);
                //$("#btnVer").removeAttr("onclick");
                $("#DivArchivos").hide();
                if (archivo != "") {
                    //$("#btnVer").attr("onclick", "fnDescargar('" + archivo + "')");
                    $("#DivArchivos").show();
                }
                if (estado == "O") {
                    $("#btnGuardar").hide();
                    $("#btnObservar").hide();
                } else {
                    $("#btnGuardar").show();
                    $("#btnObservar").show();
                }
                $("#hddta").val(dta);
                $("#cboModEstudio").val("P")
                ListarArchivos("1", trl, dta)
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

function ListarArchivos(op, trl, dta) {
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.lat + '" /></form>');
    $('#frmOpe').append('<input type="hidden" id="op" name="op" value="' + op + '" />');
    $('#frmOpe').append('<input type="hidden" id="trl" name="trl" value="' + trl + '" />');
    $('#frmOpe').append('<input type="hidden" id="dta" name="dta" value="' + dta + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    console.log(form);
    $.ajax({
        type: "POST",
        url: "../../DataJson/GradosYTitulos/Egresado.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            console.log(data);
            var tb = ''
            if (data.length > 0) {
                tb += "<table class='table table-responsive table-condensed' width='70%'>"
                tb += "<thead>"
                tb += "<tr style='background-color:#E33439;'>"
                tb += "<th style='width:20%;color:white; text-align:center;'>Tipo</th>"
                tb += "<th style='width:20%'></th>"
                tb += "</tr>"
                tb += "</thead>"
                for (i = 0; i < data.length; i++) {
                    tb += "<tr>"
                    tb += "<td style='width:20%;' >" + data[i].tabla + "</td>"
                    tb += "<td style='width:20%;' ><button class='btn btn-sm btn-primary btn-radius' onclick='fnDescargar(\"" + data[i].valorcampo + "\")'>Descargar</button></td>"
                    //tb += "<td>" + data[i].observacion + "</td>"
                    tb += "</tr>"
                }
                tb += "</table>"
            }
            $("#tbarchivos").html(tb)
        },
        error: function(result) {
            //console.log(result)
        }
    });
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
    var seleccion = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '" >' + arr[i].nombre + '</option>';
    }
    $('#CboGrado').html(str);
}
function cGradoRegistro(cod_cpf, vigencia) {
    ///var arr = fnGrado(cod_cpf, vigencia); // HCANO 19/08/2020
    var arr = fnGradoxTipo(cod_cpf, vigencia); // HCANO 19/08/2020

    //console.log(arr);
    var n = arr.length;
    var str = "";
    var seleccion = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        /* HCANO 19/08/2020 */
        if (n == 2 && (n - 1) == i) {
            seleccion = "selected";
        }
        /* HCANO 19/08/2020 */
        str += '<option value="' + arr[i].cod + '" ' + seleccion + ' >' + arr[i].nombre + '</option>';

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
                    url: "../../DataJson/GradosyTitulos/Egresado.aspx",
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
                            $("#Lista").show();
                            $("#txtbusqueda").focus();
                            var texto = $("#txtbusqueda").val();
                            if (texto == "")
                            { texto = "%"; }
                            ConsultarTramites($("#cboEstado").val(), texto)
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
    /*if ($("#inputtxtFechaActoAcad").val() == "") {
    $("#inputtxtFechaActoAcad").focus();
    fnMensaje("error", "Ingrese Correctamente la Fecha del Acto Académico.")
    return false;
    }*/
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
    //11.08.2020 JBANDA    
    if ($("#cboCargo4").val() == "") {
        $("#cboCargo4").focus();
        fnMensaje("error", "Seleccione El Cargo de La Cuarta Autoridad.")
        return false;
    }
    //11.08.2020 JBANDA    
    if ($("#cboAutoridad4").val() == "") {
        $("#cboAutoridad4").focus();
        fnMensaje("error", "Seleccione El Personal de la Cuarta Autoridad.")
        return false;
    }
    if (!confirm("¿Está seguro que desea recepcionar el expediente?")) {
        return false;
    }
    return true;

}


function fnLimpiar() {
    $("#hdCodEgr").val("0")
    $("#hddta").val("0")
    $("#txtNroExp").val("-")
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
    //31.07.2020 JBANDA
    $("#cboCargo4").val("")
    $("#cboAutoridad4").val("")

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
                url: "../../DataJson/GradosyTitulos/Egresado.aspx",
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
                            url: "../../DataJson/GradosyTitulos/Egresado.aspx",
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
            url: "../../DataJson/GradosyTitulos/Egresado.aspx",
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

function ConsultarTramites(estado, texto) {
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
                    { name: 'cpf', type: 'string' },
                    { name: 'tip', type: 'string' },
                    { name: 'emi', type: 'string' },
                    { name: 'archivo', type: 'string' },
                    { name: 'nro_expediente', type: 'string' },
                    { name: 'estado', type: 'string' },
                    { name: 'fecha_reg', type: 'string' },
                    { name: 'trl', type: 'string' },
                    { name: 'dta', type: 'string' }
                ],
                root: 'rows',
                url: "../../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.ctr, estado: estado, txtbuscar: texto }

            };
        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxGridLista").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" title="Recepcionar" onclick="CargaDatosAlumno(\'' + dataRecord.cod + '\',\'' + dataRecord.tip + '\',\'' + dataRecord.emi + '\',\'' + dataRecord.archivo + '\',\'' + dataRecord.nro_expediente + '\',\'' + dataRecord.dta + '\',\'' + estado + '\',\'' + dataRecord.trl + '\');"><i class="ion-android-hand"></i></button>';
        }

        $("#jqxGridLista").jqxGrid(
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
                {text: 'Tipo Doc', datafield: 'tipodoc', width: '7%' },
                { text: 'N° Doc', datafield: 'nrodoc', width: '8%' },
                { text: 'Cod. Univer.', datafield: 'coduniver', width: '9%' },
                { text: 'Alumno', datafield: 'alu', width: '30%' },
                { text: 'Carrera Profesional', datafield: 'cpf', width: '24%' },
                { text: 'Tipo', datafield: 'tip', width: '5%' },
                { text: 'Emisión', datafield: 'emi', width: '7%' },
                { text: 'Fec Recepción', datafield: 'fecha_reg', width: '6%' },
                { text: ' ', datafield: 'cod', width: '4%', cellsrenderer: cellsrenderer }
                ]
            });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}


function ConsultarEgresado(estado, texto) {
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
                    { name: 'abrev_dip', type: 'string' },
                    { name: 'fecha_reg', type: 'string' }

                ],
                root: 'rows',
                url: "../../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.lst, hdTest: '', hdCarrera: '', txtbuscar: texto }

            };
        //        console.log(source);
        var dataAdapter = new $.jqx.dataAdapter(source);
        var cellsrenderer = function(row, column, value) {
            // alert(value);
            var dataRecord = $("#jqxGridLista").jqxGrid('getrowdata', row);
            return '<button class="btn btn-success" onclick="CargaDatosEgresado(\'' + dataRecord.cod + '\');"><i class="ion-android-hand"></i></button>';
        }

        $("#jqxGridLista").jqxGrid(
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
                { text: 'Alumno', datafield: 'alu', width: '30%' },
                { text: 'Plan Estudios', datafield: 'pes', width: '20%' },
                { text: 'Tipo', datafield: 'tipo_dip', width: '4%', cellsalign: 'center', align: 'center' },
                { text: 'Diploma', datafield: 'abrev_dip', width: '6%', cellsalign: 'center', align: 'center' },
                { text: 'Estado', columntype: 'checkbox', datafield: 'est', width: '6%' },
                { text: 'Fec Recepción', datafield: 'fecha_reg', width: '10%', cellsalign: 'center', align: 'center' },
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
        fnLimpiar();
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosyTitulos/Egresado.aspx",
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
                    $("#CboCarrera").html(cbo_Cpf)
                    $("#CboCarrera").val(data[i].cod_cp)

                    cEspecialidad(data[i].cod_pes);
                    $("#cboEspecialidad").val(data[i].cod_esp)
                    cGrado(data[i].cod_cp, '%');
                    $("#CboGrado").val(data[i].cod_dgt)


                    $("#hdCodEgr").val(data[i].cod)
                    $("#hdCodigoAlu").val(data[i].cod_alu)
                    $("#hdCodigoAluME").val(data[i].cod_alu)
                    //--
                    ListarArchivos("1", data[i].trl, data[i].dta)
                    $("#DivArchivos").hide();
                    if (data[i].archivo != "") {
                        //$("#btnVer").attr("onclick", "fnDescargar('" + data[i].archivo + "')");
                        $("#DivArchivos").show();
                    }
                    $("#txtNroResolucionFac").val(data[i].nroreso)
                    $("#ArchivoResolucion").html("");
                    if (data[i].archivoresofac != "") {
                        $("#ArchivoResolucion").html("<button input type='button' class='btn btn-sm btn-success btn-radius'  title='Descargar resolución de facultad' onclick='fnDescargar(\"" + data[i].archivoresofac + "\")' ><i class='ion-android-download'></i></button>")
                    }
                }
                cCargo();
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
                $("#btnGuardar").hide();
                $("#btnObservar").hide();


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

function CargaAutoridad(cod) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.caut + '" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param1" name="param1" value="' + cod + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../../DataJson/GradosYTitulos/Egresado.aspx",
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

function fnDescargar(id) {
    var d = new Date();
    window.open("../../Descargar.aspx?id=" + id + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
}

function CargaRequisitos(glosa) {

    fnLoading(true);
    var source =
            {
                datatype: "json",
                type: "POST",
                async: false,
                datafields: [
                    { name: 'nombre', type: 'string' },
                    { name: 'detalle', type: 'string' }
                ],
                root: 'rows',
                url: "../../DataJson/GradosyTitulos/Egresado.aspx",
                data: { action: ope.crt, glosa: glosa }

            };
    //        console.log(source);
    var dataAdapter = new $.jqx.dataAdapter(source);
    /* var cellsrenderer = function(row, column, value) {
    // alert(value);
    var dataRecord = $("#jqxGridRequisitos").jqxGrid('getrowdata', row);
    return '<button class="btn btn-success" title="Editar" onclick="CargaDatosAlumno(\'' + dataRecord.cod + '\',\'' + dataRecord.tip + '\',\'' + dataRecord.emi + '\',\'' + dataRecord.archivo + '\',\'' + dataRecord.nro_expediente + '\');"><i class="ion-android-hand"></i></button>';
    }*/

    $("#jqxGridRequisitos").jqxGrid(
            {
                width: "100%",
                source: dataAdapter,
                sortable: true,
                //pageable: true,
                columnsresize: true,
                groupable: true,
                selectionmode: 'checkbox',
                showgroupsheader: false,
                groupsexpandedbydefault: true,
                autoheight: true,
                autorowheight: true,
                ready: function() {
                    // called when the Grid is loaded. Call methods or set properties here.         
                },
                //selectionmode: 'checkbox',
                altrows: true,
                columns: [
                //{text: 'codigo', datafield: 'cod', width: '8%' },
                {text: 'Requisito', datafield: 'nombre', hidden: true },
                { text: 'SELECCIONE REQUISITOS INCUMPLIDOS', datafield: 'detalle'}/*,
                { text: ' ', datafield: 'cod', width: '4%', cellsrenderer: cellsrenderer }*/
                ],
                groups: ['nombre']
            });

    fnLoading(false);

}


function ObservarTramite(cod_dta, observacion, requisitos) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="' + ope.otr + '" /></form>');
        $('#frmOpe').append('<input type="hidden" id="param1" name="param1" value="' + cod_dta + '" />');
        $('#frmOpe').append('<input type="hidden" id="param2" name="param2" value="' + observacion + '" />');
        $('#frmOpe').append('<input type="hidden" id="param3" name="param3" value="' + requisitos + '" />');
        var form = $("#frmOpe").serializeArray();
        $("#frmOpe").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../../DataJson/GradosYTitulos/Egresado.aspx",
            data: form,
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
                console.log(data);
                if (data[0].evaluacion == true) {
                    fnLimpiar()
                    $("#mdObservaciones").modal("hide");
                    $("#RegistroExpediente").hide();
                    $("#Lista").show();
                    var texto = $("#txtbusqueda").val();
                    if (texto == "")
                    { texto = "%"; }
                    ConsultarTramites($("#cboEstado").val(), texto)
                    $("#txtbusqueda").val("");
                    fnMensaje("success", "Observación registrada correctamente")
                } else {
                    fnMensaje("error", "No se pudo registrar observación")
                }
            },
            error: function(result) {
                //console.log(result)
                fnMensaje("error", "No se pudo registrar observación")
            }
        });
        fnLoading(false);
    } else {
        window.location.href = rpta
    }
}