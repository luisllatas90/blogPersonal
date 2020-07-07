/*
function fnOperacion(root)
function fnSemestre(root,k)
function fnDepAcad(root, k)
function fnDepAcadDocente(root, k,f)
function fnCentroCosto(root, k, f)
function fnEventos(root, k, f)
function fnConvertirJsonTOArray(JsonObj)
function fnSelectTipoDocIdent(cbo, sel)
function fnSelectSexo(cbo, sel)
function fnSelectEstadoCivil(cbo, sel)
*/

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
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            // cache: false,
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


function fnSemestre(root,k) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "cicloacad", "k": k };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                arr = $.parseJSON(data);
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

function fnDepAcad(root, k) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "depacad", "k": k };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                arr = $.parseJSON(data);
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

function fnDepAcadDocente(root, k,f) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "depdoc", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                arr = $.parseJSON(data);
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

function fnCentroCosto(root, k, f) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "CCOxPxV", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                //console.log(data);
                arr = data;
               // arr = $.parseJSON(data);
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

function fnModalidadIngreso(root, k, f,mod) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "moding", "k": k, "f": f, "mod": mod };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                //console.log(data);
                //arr = data;
                arr = $.parseJSON(data);
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

function fnEventos(root, k, f) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "CCOevent", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                //console.log(data);
                arr = data;
                // arr = $.parseJSON(data);
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


function fnConvertirJsonTOArray(JsonObj) {
    var array = [];
    for (var i in JsonObj) {
        if (JsonObj.hasOwnProperty(i) && !isNaN(+i)) {
            array[+i] = JsonObj[i];
        }
    }

    return array;
}


function fnSelectTipoDocIdent(cbo, sel) {
    var scbo = '';
    if (sel == 'DNI') {
        scbo += '<option value="DNI" selected="selected">';
    } else {
        scbo += '<option value="DNI">';
    }
    scbo += 'DNI</option>';
    if (sel == 'CARNÉ DE EXTRANJERÍA') {
        scbo += '<option value="CARNÉ DE EXTRANJERÍA" selected="selected">';
    } else {
    scbo += '<option value="CARNÉ DE EXTRANJERÍA">';
    }
    scbo += 'CARNÉ DE EXTRANJERÍA</option>';
    $('#' + cbo).html(scbo);
}

function fnSelectSexo(cbo, sel) {
    var scbo = '';

    if (sel == '') {
        scbo += '<option value="" selected="selected">';
    } else {
        scbo += '<option value="">';
    }
    scbo += 'SELECCIONE</option>';
    
    if (sel == 'M') {
        scbo += '<option value="M" selected="selected">';
    } else {
        scbo += '<option value="M">';
    }
    scbo += 'MASCULINO</option>';
    if (sel == 'F') {
        scbo += '<option value="F" selected="selected">';
    } else {
        scbo += '<option value="F">';
    }
    scbo += 'FEMENINO</option>';
    $('#' + cbo).html(scbo);
}

function fnSelectEstadoCivil(cbo, sel) {
    var scbo = '';

    if (sel == '') {
        scbo += '<option value="" selected="selected">';
    } else {
        scbo += '<option value="">';
    }
    scbo += 'SELECCIONE</option>';

    if (sel == 'SOLTERO') {
        scbo += '<option value="SOLTERO" selected="selected">';
    } else {
    scbo += '<option value="SOLTERO">';
    }
    scbo += 'SOLTERO</option>';
    if (sel == 'CASADO') {
        scbo += '<option value="CASADO" selected="selected">';
    } else {
    scbo += '<option value="CASADO">';
    }
    scbo += 'CASADO</option>';
    if (sel == 'VIUDO') {
        scbo += '<option value="VIUDO" selected="selected">';
    } else {
    scbo += '<option value="VIUDO">';
    }
    scbo += 'VIUDO</option>';
    if (sel == 'DIVORCIADO') {
        scbo += '<option value="DIVORCIADO" selected="selected">';
    } else {
    scbo += '<option value="DIVORCIADO">';
    }
    scbo += 'DIVORCIADO</option>';
    $('#' + cbo).html(scbo);
}

function fnUbigeo(root, p1,p2,p3) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "ubi", "p1": p1, "p2": p2, "p3": p3 };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/select_ajax.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
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

function fnSelectChange(cbo) {
    document.getElementById(cbo).selectedIndex = document.getElementById(cbo).defaultIndex;
}
function fnSelectFocus(cbo) {
    document.getElementById(cbo).defaultIndex = document.getElementById(cbo).selectedIndex;
}

function fnSelectReset(cbo) {
    $('#' + cbo).removeAttr("onchange");
    $('#' + cbo).removeAttr("onfocus");
}