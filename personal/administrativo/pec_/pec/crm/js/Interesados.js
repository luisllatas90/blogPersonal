
$(document).ready(function() {

    // Carga Lista y Combos

    fnLoading(true);
    load(1);
    Departamentos(0);
    TipoDocumento(0);
    TipoComunicacion(0);
    Arbol();
    fnLoading(false);
    // Fin Carga Combos

    /* var oTable = $('#TablaInteresados').DataTable({
    "bPaginate": false,
    "bFilter": true,
    "bLengthChange": false,
    "bInfo": false
    });

    var oTable1 = $('#TablaComunicacion').DataTable({
    "bPaginate": false,
    "bFilter": false,
    "bLengthChange": false,
    "bInfo": false,
    "scrollY": "200px",
    "scrollCollapse": true,
    "paging": false
    });*/

    // Modal Eliminar 
    $('#dataDelete').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        var id = button.data('id') // Extraer la información de atributos de datos
        var modal = $(this)
        modal.find('#id_int').val(id)
    })
    // Fin Modal Eliminar

    // Modal Eliminar Comunicacion
    $('#DataDeleteCom').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        var id = button.data('id') // Extraer la información de atributos de datos
        var modal = $(this)
        modal.find('#id_com').val(id)
    })
    // Fin Modal Eliminar

    $("#cbodepartamento").change(function() {
        Provincias($("#cbodepartamento").val(), 0);
        $("#cbodistrito").html("<select id='cbodistrito' name='cbodistrito' class=' form-control input-sm' runat='server'><option value='0'>--Seleccione--</option></select>");
    });
    $("#cboprovincia").change(function() {
        Distritos($("#cboprovincia").val(), 0);
    });
    $('.piluku-tabs li a').click(function() {
        //    alert($(this).attr("href"));
        //alert($("#Paso").val());
        if ($(this).attr("href") == "#tabs1") {
            //            $('.piluku-tabs > .active').next('li').find('a').trigger('click');
            if ($("#Paso").val() == '2') {
                $("#Paso").val(1);
                $("#btnCancelar").show();
                $("#btnAtras").hide();
            }

        } else if ($(this).attr("href") == "#tabs2") {
            //            $('.piluku-tabs > .active').next('li').find('a').trigger('click');
            if ($("#Paso").val() == '1') {
                $("#Paso").val(2);
                $("#btnCancelar").hide();
                $("#btnAtras").show();
            }
        }
    })

    $("#btnAtras").click(function() {
        $('.piluku-tabs > .active').prev('li').find('a').trigger('click');
        $("#Paso").val(1);
        $("#btnCancelar").show();
        $("#btnAtras").hide();
    });

    // Modal Registro / Actualizacion

    $('#dataRegister').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        var id = button.data('id') // Extraer la información de atributos de datos
        var modal = $(this)
        Limpiar();
        if (id == undefined) {
            modal.find('#codigo_int').val("0");
            //            $('.piluku-tabs > .active').next('li').find('a').css('display', 'none');
            $('.piluku-tabs > .active').next('li').find('a').attr('style', 'display:none')
        } else {
            modal.find('#codigo_int').val(id);
            //            $('.piluku-tabs > .active').next('li').find('a').css('display', 'block');
            $('.piluku-tabs > .active').next('li').find('a').removeAttr('style')
            $.ajax({
                type: 'GET',
                contentType: "application/json; charset=utf-8",
                url: 'Interesados_ajax.aspx',
                data: { "action": "CargaDatos", "id": id },
                //                async: false,
                dataType: "json",
                cache: false,
                success: function(data) {
                    //                    console.log(data);
                    $("#cboTipoDocumento").val(data.tdo);
                    $("#numdoc").val(data.ndo);
                    $("#apepat").val(data.ap);
                    $("#apemat").val(data.am);
                    $("#nombres").val(data.nm);
                    $("#cbodepartamento").val(data.dp);
                    Provincias($("#cbodepartamento").val(), data.prov);
                    Distritos(data.prov, data.dis);
                    $("#direccion").val(data.dr);
                    $("#telefono").val(data.tl);
                    $("#email").val(data.em);
                    $("#celular").val(data.ce);
                    $("#procedencia").val(data.pro);
                    $("#grado").val(data.gra);
                    $("#carrera").val(data.car);
                }
    ,
                error: function(result) {
                    //console.log(result);
                }
            });

            Comunicacion(id)
        }
    });
    // Fin Modal Registro/Actualizacion
});


// Cargar Lista de Interesados 
function load(page) {
    var parametros = { "action": "load", "page": page };
    //    $("#loader").fadeIn('slow');
    $.ajax({

        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        cache: false,
        data: parametros,
        //        beforeSend: function(objeto) {
        //            $("#loader").html("<img src='img/loader.gif'>");
        //        },
        dataType: "json",
        success: function(data) {
            //            console.log(data)
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<tr role="row">';
                rpta += '<td width="15%">' + data[i].numdoc + '</td>';
                rpta += '<td width="50%">' + data[i].nombre + '</td>';
                rpta += '<td width="25%">' + data[i].email + '</td>';
                rpta += '<td width="5%" align="center"><button type="button" class="btn btn-sm btn-info" data-toggle="modal" data-target="#dataRegister" data-id="' + data[i].cod + '" title="Editar" ><i class="ion-edit"></i></button></td>';
                rpta += '<td width="5%" align="center"><button type="button" class="btn btn-sm btn-danger" data-toggle="modal" data-target="#dataDelete" data-id="' + data[i].cod + '" title="Eliminar" ><i class="ion-close"></i></button></td>';
                rpta += '</tr>';
            }
            //            alert(rpta);
            fnDestroyDataTableDetalle('TablaInteresados');
            $("#TbInteresados").html(rpta);
            fnResetDataTable('TablaInteresados');

            //            $("#loader").html("");
        }
        //        ,
        //        error: function(result) {
        //            console.log(result);
        //        }
    });
}
// Fin Cargar Lista de Interesados


// Cargar Lista de Interesados 
function Comunicacion(cod_int) {
    var parametros = { "action": "Com", "page": cod_int };
    //    $("#loader").fadeIn('slow');
    $.ajax({

        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        cache: false,
        data: parametros,
        //        beforeSend: function(objeto) {
        //            $("#loader").html("<img src='img/loader.gif'>");
        //        },
        dataType: "json",
        success: function(data) {
            //            console.log(data)
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<tr >';
                rpta += '<td style="width:5%">' + data[i].nro + '</td>';
                rpta += '<td style="width:20%">' + data[i].carrera + '</td>';
                rpta += '<td style="width:20%">' + data[i].tipocom + '</td>';
                rpta += '<td style="width:40%">' + data[i].detalle + '</td>';
                rpta += '<td style="width:10%" align="center">' + data[i].fecha + '</td>';
                rpta += '<td style="width:5%" align="center"><button type="button" class="btn btn-xs btn-danger" data-toggle="modal" data-target=".modal3" data-id="' + data[i].cod + '" title="Eliminar" ><i class="ion-close"></i></button></td>';
                rpta += '</tr>';
            }
            //            alert(rpta);
            fnDestroyDataTableDetalle('TablaComunicacion');
            $("#TbComunicacion").html(rpta);
            fnResetDataTableSinFiltros('TablaComunicacion');
            //            $("#txtcomunicacion").val($("#TablaComunicacion").html());
            //            $("#loader").html("");
        }
        //        ,
        //        error: function(result) {
        //            console.log(result);
        //        }
    });
}
// Fin Cargar Lista de Interesados


function Departamentos(cod_dep) {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        data: { "action": "dep" },
        //        async: false,
        dataType: "json",
        success: function(data) {
            //console.log(data);
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<option value="' + data[i].cod + '"';
                if (data[i].cod == cod_dep) {
                    rpta += ' selected="selected" ';
                }
                rpta += '>' + data[i].des + '</option>';
            }
            $("#cbodepartamento").append(rpta);
        },
        error: function(result) {
            //console.log(result);
        }

    });
}


function Provincias(cod_dep, cod_prov) {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        data: { "action": "prov", "page": cod_dep },
        //        async: false,
        dataType: "json",
        success: function(data) {
            //console.log(data);
            //            alert(data);
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<option value="' + data[i].cod + '"';
                if (data[i].cod == cod_prov) {
                    rpta += ' selected="selected" ';
                }
                rpta += '>' + data[i].des + '</option>';
            }
            //            alert(rpta);
            $("#cboprovincia").html("<select id='cboprovincia' name='cboprovincia' class='form-control input-sm' runat='server'><option value='0'>--Seleccione--</option></select>");
            $("#cboprovincia").append(rpta);
        },
        error: function(result) {
            //console.log(result);
        }

    });
}

function Distritos(cod_prov, cod_dis) {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        data: { "action": "dis", "page": cod_prov },
        //        async: false,
        dataType: "json",
        success: function(data) {
            //console.log(data);
            //            alert(data);
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<option value="' + data[i].cod + '"';
                if (data[i].cod == cod_dis) {
                    rpta += ' selected="selected" ';
                }
                rpta += '>' + data[i].des + '</option>';
            }
            //            alert(rpta);
            $("#cbodistrito").html("<select id='cbodistrito' name='cbodistrito' class='form-control input-sm' runat='server'><option value='0'>--Seleccione--</option></select>");
            $("#cbodistrito").append(rpta);
        },
        error: function(result) {
            //console.log(result);
        }
    });
}

function TipoDocumento(cod_td) {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        data: { "action": "TipoDoc", "page": cod_td },
        //        async: false,
        dataType: "json",
        success: function(data) {
            //console.log(data);
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<option value="' + data[i].cod + '"';
                if (data[i].cod == cod_td) {
                    rpta += ' selected="selected" ';
                }
                rpta += '>' + data[i].des + '</option>';
            }
            //                        alert(rpta);
            $("#cboTipoDocumento").html("<select id='cboTipoDocumento' name='cboTipoDocumento' class='form-control input-sm' runat='server'><option value='0'>--Seleccione--</option></select>");
            $("#cboTipoDocumento").append(rpta);
        },
        error: function(result) {
            //console.log(result);
        }
    });
}

function TipoComunicacion(cod_com) {
    $.ajax({
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        url: 'Interesados_ajax.aspx',
        data: { "action": "TipoCom", "page": cod_com },
        //        async: false,
        dataType: "json",
        success: function(data) {
            //            console.log(data);
            var rpta = "";
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                rpta += '<option value="' + data[i].cod + '"';
                if (data[i].cod == cod_com) {
                    rpta += ' selected="selected" ';
                }
                rpta += '>' + data[i].des + '</option>';
            }
            $("#cboTipoComunicacion").html("<select id='cboTipoComunicacion' name='cboTipoComunicacion' class='form-control input-sm' runat='server'></select>");
            $("#cboTipoComunicacion").append(rpta);
        },
        error: function(result) {
            //console.log(result);
        }
    });
}

function Quitar() {
    var parametros = $("#eliminarDatos").serialize() + "&action=Eliminar";
    //    alert(parametros)
    $.ajax({
        type: "POST",
        url: "Interesados_ajax.aspx",
        data: parametros,
        beforeSend: function(objeto) {
            $("#rpta_delete").html("<div id='rpta_delete'>Procesando...</div>");
            $("#rpta_delete").attr("class", "alert alert-info");
        },
        success: function(datos) {
            //        alert(datos)
            if (datos == '1') {

                fnMensaje('success', 'Se Eliminó Correctamente.');
                $("#rpta_delete").html("<div id='rpta_delete'>Se Eliminó Correctamente.</div>");
                $("#rpta_delete").attr("class", "alert alert-success");
            } else {
                fnMensaje('warning', 'No se Pudo Eliminar.');
                $("#rpta_delete").html("<div id='rpta_delete'>No se Pudo Eliminar.</div>");
                $("#rpta_delete").attr("class", "alert alert-danger");
            }
            load(1);
            $('#dataDelete').modal('hide');
            //            location.reload();
        }
    });
}

function QuitarCom() {
    fnLoadingDiv('divLoading', true);
    var parametros = $("#FormEliminaCom").serialize() + "&action=Eliminar_Com";
    //    alert(parametros)
    $.ajax({
        type: "GET",
        url: "Interesados_ajax.aspx",
        data: parametros,
        beforeSend: function(objeto) {
            $("#rpta_comunicacion").html("<div id='rpta_delete'>Procesando...</div>");
            $("#rpta_comunicacion").attr("class", "alert alert-info");
        },
        success: function(datos) {
            //                    alert(datos)
            if (datos == '1') {
                fnMensaje('success', 'Se Eliminó Correctamente.');
                $("#rpta_comunicacion").html("<div id='rpta_delete'>Se Eliminó Correctamente.</div>");
                $("#rpta_comunicacion").attr("class", "alert alert-success");
            } else {
                fnMensaje('warning', 'No se Pudo Eliminar.');
                $("#rpta_comunicacion").html("<div id='rpta_delete'>No se Pudo Eliminar.</div>");
                $("#rpta_comunicacion").attr("class", "alert alert-danger");
            }

            $('#DataDeleteCom').modal('hide');
            Comunicacion($('#codigo_int').val());
            //                alert($("#cod_int").val());
            //            location.reload();
        }
    });
    fnLoadingDiv('divLoading', false);
}

function Guardar() {
    if ($("#Paso").val() == 1) {
        if (Validar() == '1') {
            var parametros = $("#guardarDatos").serialize() + "&action=Guardar";
            $.ajax({
                type: "POST",
                url: "Interesados_ajax.aspx",
                data: parametros,
                beforeSend: function(objeto) {
                    $("#rpta_registro").html("Procesando...");
                    $("#rpta_registro").attr("class", "alert alert-info text-left");
                },
                success: function(datos) {
                    if (datos == 'R') { // Registro
                        Limpiar();
                        fnMensaje('success', 'Se Registró Correctamente.');
                        $("#rpta_registro").html("Se Registró Correctamente.");
                        $("#rpta_registro").attr("class", "alert alert-success text-left");
                        /*$("#Paso1").hide();
                        $("#Paso2").show();*/

                        $('.piluku-tabs > .active').next('li').find('a').removeAttr('style')
                        $('.piluku-tabs > .active').next('li').find('a').trigger('click');
                        $("#Paso").val(2);
                        $("#btnAtras").show();
                        $("#btnCancelar").hide();
                    } else if (datos == 'A') { // Actualizo
                        //Limpiar();
                        fnMensaje('success', 'Se Actualizó Correctamente.');
                        $("#rpta_registro").html("Se Actualizó Correctamente.");
                        $("#rpta_registro").attr("class", "alert alert-success text-left");
                        /*$("#Paso1").hide();
                        $("#Paso2").show();*/
                        $('.piluku-tabs > .active').next('li').find('a').trigger('click');
                        $("#Paso").val(2);
                        $("#btnAtras").show();
                        $("#btnCancelar").hide();
                    } else { // Error
                        fnMensaje('warning', 'No se Pudo Registrar');
                        $("#rpta_registro").html("No se Pudo Registrar.");
                        $("#rpta_registro").attr("class", "alert alert-danger text-left");
                    }
                    load(1);
                    //            $('#dataRegister').modal('hide');
                },
                error: function(result) {
                    //            console.log(result);
                }
            });
        }

    } else if ($("#Paso").val() == 2) {
        Limpiar();
        $('#dataRegister').modal('hide');
    }
}

function Guardar_Com() {
    if (Validar_com() == '1') {
        var parametros = $("#FormPaso2").serialize() + "&action=Guardar_Com" + "&codigo_int=" + $("#codigo_int").val();
        //        alert(parametros);
        $.ajax({
            type: "POST",
            url: "Interesados_ajax.aspx",
            data: parametros,
            beforeSend: function(objeto) {
                $("#rpta_comunicacion").html("Procesando...");
                $("#rpta_comunicacion").attr("class", "alert alert-info text-left");
            },
            success: function(datos) {
                if (datos == '1') { // Registro
                    fnMensaje('success', 'Se Registró Correctamente.');
                    $("#rpta_comunicacion").html("Se Registró Correctamente.");
                    $("#rpta_comunicacion").attr("class", "alert alert-success text-left");
                    Comunicacion($("#codigo_int").val());
                } else { // Error
                    fnMensaje('warning', 'No se Pudo Registrar');
                    $("#rpta_comunicacion").html("No se Pudo Registrar.");
                    $("#rpta_comunicacion").attr("class", "alert alert-danger text-left");
                }

                //            $('#dataRegister').modal('hide');
            },
            error: function(result) {
                //            console.log(result);
            }
        });
    }
}

function Limpiar() {
    $("#cboTipoDocumento").val("0")
    $("#cbodepartamento").val("0")
    $("#cboTipocomunicacion").val("0")
    $("#cbodistrito").html("<select id='cbodistrito' class=' form-control input-sm'><option value='0'>--Seleccione--</option></select>");
    $("#cboprovincia").html("<select id='cboprovincia' class='form-control input-sm'><option value='0'>--Seleccione--</option></select>");
    $("#codigo_int").val("0");
    $("#numdoc").val("");
    $("#apepat").val("");
    $("#apemat").val("");
    $("#nombres").val("");
    $("#direccion").val("");
    $("#telefono").val("");
    $("#email").val("");
    $("#celular").val("");
    $("#direccion").val("");
    $("#procedencia").val("");
    $("#grado").val("");
    $("#carrera").val("");
    $("#cboTipoComunicacion").val("0")
    $("#txtcomunicacion").val("");
    $("#rpta_registro").html("");
    $("#rpta_registro").removeClass("alert alert-success text-left")
    $("#rpta_delete").html("");
    $("#rpta_delete").removeClass("alert alert-success")
    $('.piluku-tabs > .active').prev('li').find('a').trigger('click');
    //$("#Paso2").hide();
    //$("#Paso1").show();
}

function Validar() {
    $("#rpta_registro").html("");
    $("#rpta_registro").removeClass("alert alert-success text-left")
    if ($("#apepat").val() == "") {
        $("#rpta_registro").html("Ingrese Apellido Paterno.");
        $("#rpta_registro").attr("class", "alert alert-danger text-left");
        $("#apepat").focus();
        return 0;
    } else if ($("#apemat").val() == "") {
        $("#rpta_registro").html("Ingrese Apellido Materno.");
        $("#rpta_registro").attr("class", "alert alert-danger text-left");
        $("#apemat").focus();
        return 0;
    } else if ($("#nombres").val() == "") {
        $("#rpta_registro").html("Ingrese Nombres.");
        $("#rpta_registro").attr("class", "alert alert-danger text-left");
        $("#nombres").focus();
        return 0;
    } else {
        return 1;
    }
}

function Validar_com() {
    $("#rpta_comunicacion").html("");
    $("#rpta_comunicacion").removeClass("alert alert-success text-left")
    if ($("#cboTipoComunicacion").val() == "0") {
        $("#rpta_comunicacion").html("Seleccione Tipo de Comunicación.");
        $("#rpta_comunicacion").attr("class", "alert alert-danger text-left");
        return 0;
    } else {
        return 1;
    }
}


function Arbol() {
    var rpta = "";
    rpta += '<ul>'
    rpta += '<li class="parent_li"><span title="collapse this branch" class="parent"><i class="fa fa-folder-open"></i>' + 1 + '</span><a href=""></a>';
    rpta += '<ul>'
    rpta += '<li><span class="child"><i class="fa fa-file"></i>' + 2 + '</span> <a href=""></a></li>'
    rpta += '</ul>'
    rpta += '</li>'
    rpta += '</ul>'
    $("#arbol").html(rpta);
}



//---------------------------------- Funciones.Js------------------------------------------

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}
function fnResetDataTable(table) {


    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var oTable = $('#' + table).DataTable({
            "sContentPadding": false
        });
        oTable = $('#' + table).DataTable().fnDestroy();
        oTable = $('#' + table).DataTable({
            "bPaginate": false,
            "bFilter": true,
            "bLengthChange": false,
            "bInfo": false,
            "bProcessing": true


        });

        return oTable;
    }
}
function fnResetDataTableSinFiltros(table) {

    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var oTable = $('#' + table).DataTable({
            "sContentPadding": false
        });
        oTable = $('#' + table).DataTable().fnDestroy();

        oTable = $('#' + table).DataTable({
            "bPaginate": false,
            "bFilter": false,
            "bLengthChange": false,
            "bInfo": false
            /*,
            "sScrollY": "220",
            "fnInitComplete": function() {
            jQuery('.dataTable').removeAttr('style');
            jQuery('.dataTables_scrollHeadInner').removeAttr('style');
            jQuery('.dataTables_scrollHeadInner').attr('style', 'width:100%;padding-right: 0px;');
            jQuery('.dataTables_scrollHeadInner').removeAttr('style'); //changing the width
            jQuery('.dataTables_scrollHeadInner').attr('style', 'width:100%;padding-right: 0px;'); //changing the width
            jQuery('.dataTable').removeAttr('style');
            jQuery('.dataTable').attr('style', 'width:100%');
            //                jQuery('#TablaComunicacion').removeAttr('style');
            //                jQuery('#TablaComunicacion').attr('style', 'width:100%');
            } ,
            "Columns": [
            { "sWidth": '5%' },
            { "sWidth": '25%' },
            { "sWidth": '15%' },
            { "sWidth": '40%' },
            { "sWidth": '10%' },
            { "sWidth": '5%'}]
            */
            //            "bScrollCollapse": true
            //            "sScrollXInner": '851',
            //            "bProcessing": true
        });

        return oTable;
    }
}
//