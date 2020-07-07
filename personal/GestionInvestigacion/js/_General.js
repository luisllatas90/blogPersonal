var ope = [];
function fnOperacion(root) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "ope" };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/GestionInvestigacion/Operaciones.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            //cache: false,
            success: function(data) {
                arr = $.parseJSON(data);
                //console.log(arr);
            },
            error: function(result) {
                arr = null;
            }
        })
        return arr;
    }
    catch (err) {
        alert(err.message);
    }
}

function fnvalidaSession() {
    var rpta = false
    $('body').append('<form id="frm"><input type="hidden" id="action" name="action" value="ValidaSession" /></form>');
    var form = $("#frm").serializeArray();
    $("#frm").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            //console.log(data);
            if (data[0].msje == true) {
                rpta = data[0].msje
            } else {
                rpta = data[0].link
            }
        },
        error: function(result) {
            console.log(result)
        }
    });
    return rpta;
}

function fnPersonal(ctf, texto) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarPersonal" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="0" />');
    $('#frmOpe').append('<input type="hidden" id="ctf" name="ctf" value="' + ctf + '" />');
    $('#frmOpe').append('<input type="hidden" id="texto" name="texto" value="' + texto + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
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

function fnDocentesxDepartamento(codigo_dac) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="lPersxDep" /></form>');
    $('#frmOpe').append('<input type="hidden" id="codigo_dac" name="cod_dac" value="' + codigo_dac + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
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


function fnAlumnosTesis(texto) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarAlumnosTesis" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="0" />');
    $('#frmOpe').append('<input type="hidden" id="texto" name="texto" value="' + texto + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
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

function fnLineas(cpf) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarLineas" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cpf" name="cpf" value="' + cpf + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
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



function fnTipoAutor(cod) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ListaTipoAutorProyecto" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
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



function fnRolInvestigador(tipo) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ListaRolInvestigador" /></form>');
    $('#frmOpe').append('<input type="hidden" id="tipo" name="tipo" value="' + tipo + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GestionInvestigacion/operaciones.aspx",
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

/**
* Funcion que captura las variables pasados por GET
* Devuelve un array de clave=>valor
*/
function ObtenerValorGET(valor) {
    var valoraDevolver = "";
    // capturamos la url
    var loc = document.location.href;
    // si existe el interrogante
    if (loc.indexOf('?') > 0) {
        // cogemos la parte de la url que hay despues del interrogante
        var getString = loc.split('?')[1];
        // obtenemos un array con cada clave=valor
        var GET = getString.split('&');
        var get = {};
        // recorremos todo el array de valores
        for (var i = 0, l = GET.length; i < l; i++) {
            var tmp = GET[i].split('=');
            if (tmp[0] == valor) {
                valoraDevolver = tmp[1]
            }
            //get[tmp[0]] = unescape(decodeURI(tmp[1]));
        }
        return valoraDevolver;
    }
}


function fnLoading(sw) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
    //console.log(sw);
}

function fnLoadingDiv(div, sw) {
    if (sw) {
        $("#" + div).removeClass('hidden');
    } else {
        $("#" + div).addClass('hidden');
    }

}

function fnCreateDataTableBasic(table, col, ord, nro_filas) {
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        //"aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": nro_filas,
        "aaSorting": [[col, ord]]
    });
    return dt;
}

function fnResetDataTableBasic(table, col, ord, nro_filas) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().fnDestroy();
        dt = $('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            //"aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": nro_filas,
            "aaSorting": [[col, ord]]
        });

        return dt;
    }
}

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}
