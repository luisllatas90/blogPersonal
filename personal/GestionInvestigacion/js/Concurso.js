/*var equipo = [];
var objetivos = [];
var codper = "";
var nombre = "";
var codobj = "0";*/
$(document).ready(function() {
    var dt = fnCreateDataTableBasic('tConcursos', 0, 'asc', 100);
    ope = fnOperacion(1);
    rpta = fnvalidaSession();
    fnLoading(true);
    if (rpta == true) {
        fnListarConcurso();
    } else {
        window.location.href = rpta
    }
    fnLoading(false);
});



function fnListarConcurso() {
    if ($("#cboEstado").val() !== "") {
        rpta = fnvalidaSession()
        if (rpta == true) {
            fnLoading(true)
            $("form#frmbuscar input[id=action]").remove();
            $("form#frmbuscar input[id=ctf]").remove();
            $("form#frmbuscar input[id=dir]").remove();            
            $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
            $('form#frmbuscar').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
            $('form#frmbuscar').append('<input type="hidden" id="dir" name="dir" value="%" />');
            var form = $("#frmbuscar").serializeArray();
            $("form#frmbuscar input[id=action]").remove();
            $("form#frmbuscar input[id=ctf]").remove();
            $("form#frmbuscar input[id=dir]").remove();
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Concurso.aspx",
                data: form,
                dataType: "json",
                cache: false,
                //                async: false,
                success: function(data) {
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    var clase_icono = "ion-edit";
                    if (filas > 0) {
                        for (i = 0; i < filas; i++) {
                            tb += '<tr>';
                            tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                            tb += '<td>' + data[i].titulo + '</td>';
                            tb += '<td style="text-align:center">' + data[i].fecini + '</td>';
                            tb += '<td style="text-align:center">' + data[i].fecfin + '</td>';
                            tb += '<td style="text-align:center">' + data[i].ambito + '</td>';
                            tb += '<td style="text-align:center">' + data[i].tipo + '</td>';
                            tb += '<td style="text-align:center">' + data[i].estado + '</td>';
                            tb += '<td style="text-align:center">';
                            tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnEditar(\'' + data[i].cod + '\')" title="Editar" ><i class="ion-edit"></i></button>';
                            //if ((data[i].estado == 'REGISTRO' || data[i].estado == 'OBSERVADO') && (ObtenerValorGET("ctf") == 1 || ObtenerValorGET("ctf") == 13 || ObtenerValorGET("ctf") == 65)) {
                            // EN ETAPA DE REGISTRO//OBSERVACION FACULTAD//OBSERVACION DPTO//OBSERVACION COORD. GENERAL APARECE PARA ENVIAR
                            // tb += '<button type="button" id="btnEnviar" name="btnEnviar" class="btn btn-sm btn-orange" onclick="fnEnviar(\'' + data[i].cod + '\',99)" title="Enviar a Evaluación" ><i class="ion-arrow-right-a"></i></button>';
                            //}
                            //if (data[i].estado == 'REGISTRO') {
                            tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="fnConfirmarEliminar(\'' + data[i].cod + '\')" title="Eliminar" ><i class="ion-close"></i></button>';
                            //}
                            tb += '</td>';
                            tb += '</tr>';
                        }
                    }
                    fnDestroyDataTableDetalle('tConcursos');
                    $('#tbConcursos').html(tb);
                    fnResetDataTableBasic('tConcursos', 0, 'asc', 100);
                    fnLoading(false);
                },
                error: function(result) {
                    fnMensaje("warning", result)
                    fnLoading(false);
                }
            });


        } else {
            window.location.href = rpta
        }
    }
}

function fnNuevoConcurso() {
    fnLimpiarConcurso();
    $("#file_Bases").html("");
    $("#mdRegistro").modal("show");
}

function fnLimpiarConcurso() {
    $("#hdcod").val("0");
    $("#txttitulo").val("");
    $("#txtdescripcion").val("");
    $("#cbotipo").val("");
    $("#cboAmbito").val("");
    $("#txtfecini").val("");
    $("#txtfecfin").val("");
    $("#txtfecfineva").val("");
    $("#txtfecres").val("");
    $("#fileBases").val("");
    $("#txtfecfineva").val("");
    $("#txtfecres").val("");
}

function fnCancelar() {
    $("#mdRegistro").modal("hide");
    //fnMensajeConfirmarEliminar('top', '¿Desea Salir Sin Guardar Cambios.?', 'fnCerrarModal', '');
}


function fnGuardarConcurso() {
    if (fnValidarConcurso() == true) {
        rpta = fnvalidaSession()
        if (rpta == true) {
            fnLoading(true)
            $("form#frmRegistro input[id=ctf]").remove();
            $("form#frmRegistro input[id=action]").remove();
            $('form#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
            $('form#frmRegistro').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
            var form = $("#frmRegistro").serializeArray();
            $("form#frmRegistro input[id=ctf]").remove();
            $("form#frmRegistro input[id=action]").remove();
            console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Concurso.aspx",
                data: form,
                dataType: "json",
                cache: false,
                //async: false,
                success: function(data) {
                    if (data[0].rpta == 1) {
                        fnMensaje("success", data[0].msje)
                        if ($("#fileBases").val() != "") {
                            SubirArchivo(data[0].cod);
                        }
                        $("#mdRegistro").modal("hide");
                        fnListarConcurso();
                        fnLoading(false);
                    } else {
                        fnMensaje("error", data[0].msje)
                        fnLoading(false);
                    }

                },
                error: function(result) {
                    fnMensaje("warning", result)
                    fnLoading(false);
                }
            });
        } else {
            window.location.href = rpta
        }
    }
}

function fnValidarConcurso() {
    if ($("#txttitulo").val() == "") {
        fnMensaje("warning", "Ingrese Titulo de Concurso");
        return false;
    }
    if ($("#txtdescripcion").val() == "") {
        fnMensaje("warning", "Ingrese una Breve Descripción del Concurso");
        return false;
    }
    
    if ($("#cboAmbito").val() == "") {
        fnMensaje("warning", "Debe Seleccionar el Ámbito de Concurso");
        return false;
    }

    if ($("#txtfecini").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Inicio");
        return false;
    }
    if ($("#txtfecfin").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Fin");
        return false;
    }
    if ($("#txtfecfineva").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Fin de Evaluación de Postulaciones");
        return false;
    }
    if ($("#txtfecres").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Fecha de Publicación de Resultados");
        return false;
    }


    var fecini = new Date($("#txtfecini").val().substr(3, 2) + '/' + $("#txtfecini").val().substr(0, 2) + '/' + $("#txtfecini").val().substr(6, 4));
    var fecfin = new Date($("#txtfecfin").val().substr(3, 2) + '/' + $("#txtfecfin").val().substr(0, 2) + '/' + $("#txtfecfin").val().substr(6, 4));
    var fecfineva = new Date($("#txtfecfineva").val().substr(3, 2) + '/' + $("#txtfecfineva").val().substr(0, 2) + '/' + $("#txtfecfineva").val().substr(6, 4));
    var fecres = new Date($("#txtfecres").val().substr(3, 2) + '/' + $("#txtfecres").val().substr(0, 2) + '/' + $("#txtfecres").val().substr(6, 4));

    if (fecini > fecfin) {
        fnMensaje("warning", "Fecha de Fin de Concurso no Puede ser Menor a la Fecha de Inicio.");
        return false;
    }
    if (fecini > fecfineva || fecfin > fecfineva) {
        fnMensaje("warning", "Fecha Fin de Evaluación de Postulaciones debe ser Mayor que las Fechas de Inicio y Fin de Postulaciones.");
        return false;
    }

    if (fecini > fecres || fecfin > fecres || fecfineva > fecres) {
        fnMensaje("warning", "Fecha de Publicación de Resultados debe ser Mayor a las Fechas de Postulaciones y Evaluación.");
        return false;
    }

    if ($("#cbotipo").val() == "") {
        fnMensaje("warning", "Debe Seleccionar el Tipo de Concurso");
        return false;
    }

    if ($("#hdcod").val() == 0) {
        if ($("#fileBases").val() == '') {
            fnMensaje("warning", "Adjuntar Archivo de Bases de Concurso en Formato de PDF.");
            return false;
        }
    }

    //    if ($("#fileBases").val() != '') {
    //        archivo = $("#fileBases").val();
    //        //Extensiones Permitidas
    //        //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
    //        extensiones_permitidas = new Array(".pdf");
    //        //recupero la extensión de este nombre de archivo
    //        // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
    //        archivo = archivo.substring(archivo.length - 5, archivo.length);
    //        //despues del punto de nombre recortado
    //        extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
    //        //compruebo si la extensión está entre las permitidas 
    //        permitida = false;
    //        for (var i = 0; i < extensiones_permitidas.length; i++) {
    //            if (extensiones_permitidas[i] == extension) {
    //                permitida = true;
    //                break;
    //            }
    //        }
    //        if (permitida == false) {
    //            //            fnMensaje("warning", "Solo puede Adjuntar Archivos de Informe en Formato de Word(.doc,.docx) o PDF");
    //            fnMensaje("warning", "Solo puede Adjuntar Archivo de Bases de Concurso en Formato de PDF");
    //            return false;
    //        }
    //    }
    return true;
}



function SubirArchivo(cod) {
    //    fnLoading(true)
    var flag = false;
    try {
        var data = new FormData();
        data.append("action", ope.Up);
        data.append("codigo", cod);
        var files = $("#fileBases").get(0).files;
        if (files.length > 0) {
            data.append("ArchivoASubir", files[0]);
        }
        //console.log(data);
        if (files.length > 0) {
            // fnMensaje('primary', 'Subiendo Archivo');
            $.ajax({
                type: "POST",
                url: "../DataJson/GestionInvestigacion/Concurso.aspx",
                data: data,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                async: false,
                success: function(data) {
                    //console.log(data);
                    flag = true;
                    console.log('ARCHIVO SUBIDO');

                },
                error: function(result) {
                    //console.log('falseee');
                    flag = false;
                    //console.log(result);
                }
            });
        }
        return flag;
        //        fnLoading(false);
    }
    catch (err) {
        return false;
        //        fnLoading(false);
    }
    //    fnLoading(false);
}


function fnEditar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true);
        fnLimpiarConcurso();
        $("#hdcod").val(cod)
        $("form#frmRegistro input[id=action]").remove();
        $("form#frmRegistro input[id=ctf]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        $('form#frmRegistro').append('<input type="hidden" id="ctf" name="ctf" value="' + ObtenerValorGET("ctf") + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        $("form#frmRegistro input[id=ctf]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Concurso.aspx",
            data: form,
            dataType: "json",
            cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                $("#txttitulo").val(data[0].titulo);
                $('#txtdescripcion').val(data[0].des);
                $("#cboAmbito").val(data[0].ambito);                
                $("#txtfecini").val(data[0].fecini);
                $("#txtfecfin").val(data[0].fecfin);
                $("#txtfecfineva").val(data[0].fecfineva);
                $("#txtfecres").val(data[0].fecres);
                $("#cbotipo").val(data[0].tipo);
                if (data[0].rutabases != "") {
                    $("#file_Bases").html('<a onclick="fnDownload(\'' + data[0].rutabases + '\')" >Bases</a>')
                } else {
                    $("#file_Bases").html("");
                }

                fnLoading(false);
                $("#mdRegistro").modal("show");
            },
            error: function(result) {
                fnLoading(false)
                //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnConfirmarEliminar(cod) {
    fnMensajeConfirmarEliminar('top', '¿Desea Eliminar el Concurso.?', 'fnEliminar', cod);
}

function fnEliminar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmbuscar input[id=cod]").remove();
        $("form#frmbuscar input[id=action]").remove();
        $('form#frmbuscar').append('<input type="hidden" id="action" name="action" value="' + ope.eli + '" />');
        $('form#frmbuscar').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
        var form = $("#frmbuscar").serializeArray();
        $("form#frmbuscar input[id=cod]").remove();
        $("form#frmbuscar input[id=action]").remove();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/GestionInvestigacion/Concurso.aspx",
            data: form,
            dataType: "json",
            cache: false,
            //async: false,
            success: function(data) {
                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje);
                    fnListarConcurso();
                } else {
                    fnMensaje("error", data[0].msje)
                }
                fnLoading(false);
            },
            error: function(result) {
                fnMensaje("warning", result)
                fnLoading(false);
            }
        });
    } else {
        window.location.href = rpta
    }
}

/*
function fnDownload(id_ar) {
    var flag = false;
    var form = new FormData();
    form.append("action", "Download");
    form.append("IdArchivo", id_ar);
    // alert();
    //            console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/Operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function(data) {
            console.log(data);
            flag = true;

            var file = 'data:application/octet-stream;base64,' + data[0].File;
            var link = document.createElement("a");
            link.download = data[0].Nombre;
            link.href = file;
            link.click();
        },
        error: function(result) {
            console.log(result);
            flag = false;
        }
    });
    return flag;
}
*/


function fnDownload(id_ar) {
    window.open("DescargarArchivo.aspx?Id=" + id_ar);
}



function downloadWithName(uri, name) {
    var link = document.createElement("a");
    link.download = name;
    link.href = uri;
    link.click();
    // alert(link);
}