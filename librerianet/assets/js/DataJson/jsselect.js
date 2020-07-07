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