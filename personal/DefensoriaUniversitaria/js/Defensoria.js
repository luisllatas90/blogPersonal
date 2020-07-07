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
        sroot = sroot + 'DataJson/Defensoria/Operaciones.aspx',
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
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ValidaSession" /></form>');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/Defensoria/operaciones.aspx",
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


//function fnTipoEstudio(root, k, f) { // k:codigo_test 
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "TipoEstudio", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}


//function fnSituacionInteresado(root, k, f) { // k:codigo_test 
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "SituacionInteresado", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnCicloAcademico(root, k, f) { // k:codigo_cac 
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "CicloAcademico", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

////root:1, k:"C", f:'R'
//function fConvocatoria(root, k, f) { // k:codigo_con // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "Convocatoria", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}



//function fActividadPOA(root, k, f) { // k:codigo_con // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "ActividadPOA", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnEvento(root, k, f, cod_con) { // k:codigo_eve // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "Evento", "k": k, "f": f, "cod_con": cod_con };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnTipoDocumento(root, k, f) { // k:codigo_eve // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "TipoDocumento", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnRegion(root, k, f) { // k:codigo_eve // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "Region", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnProvincia(root, k, f) { // k:codigo_eve // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "Provincia", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnDistrito(root, k, f) { // k:codigo_eve // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "Distrito", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fnCarrera(root, k, f) { // k:codigo_eve // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "CarreraProfesional", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}



//function fMotivo(root, k, f) { // k:codigo_con // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "Motivo", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        //console.log(parametros)
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}

//function fTipoComunicacion(root, k, f) { // k:codigo_con // f:codigo_test
//    try {
//        var n = parseInt(root);
//        var sroot = "";
//        var parametros = { "action": "TipoComunicacion", "k": k, "f": f };
//        var i = 0;
//        var arr;
//        for (i = 0; i < n; i++) {
//            sroot += "../";
//        }
//        //console.log(parametros)
//        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
//        $.ajax({
//            type: 'POST',
//            url: sroot,
//            data: parametros,
//            async: false,
//            cache: false,
//            success: function(data) {
//                arr = $.parseJSON(data);
//            },
//            error: function(result) {
//                arr = null;
//            }
//        })
//        return arr;
//    }
//    catch (err) {
//        alert(err.message);
//    }
//}


function fnMensajeDiv(div, tipo, mensaje) {
    $("#" + div).removeAttr("class");
    $("#" + div).text("")
    $("#" + div).hide();
    if (tipo == "info") {
        $("#" + div).attr("class", "alert alert-info");
    }
    if (tipo == "danger") {
        $("#" + div).attr("class", "alert alert-danger");
    }
    if (tipo == "warning") {
        $("#" + div).attr("class", "alert alert-warning");
    }
    if (tipo == "success") {
        $("#" + div).attr("class", "alert alert-success")
    }
    $("#" + div).text(mensaje)
    $("#" + div).show();
    setTimeout(function() {
        $("#" + div).fadeOut(500)
    }, 4000);
}