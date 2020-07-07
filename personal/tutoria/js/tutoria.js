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
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            // cache: false,
            success: function(data) {
                arr = $.parseJSON(data);
                //                //console.log(arr);
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
        url: "../DataJson/tutoria/operaciones.aspx",
        data: form,
        dataType: "json",
        cache: false,
        async: false,
        success: function(data) {
            //            //console.log(data);
            if (data[0].msje == true) {
                rpta = data[0].msje
            } else {
                rpta = data[0].link
            }
        },
        error: function(result) {
            //console.log(result)
        }
    });
    return rpta;
}
function fnSemestre(root, f, k, r) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "CicloAcademicoTO", "f": f ,"k":k,"cboEscuela":r};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fnListarPersonal(root, k, f) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "PER", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                ////console.log(data);
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
function fnListarCategoria(root, k, f) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "CAT", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                ////console.log(data);
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
function fnListarAlumnos(root, k, f) {
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "POB", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
            async: false,
            cache: false,
            success: function(data) {
                ////console.log(data);
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
function fnTipoEstudio(root, k, f) { // k:codigo_test 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TipoEstudio", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
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


function fnCicloAcademico(root, k, f) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "CicloAcademico", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fTipoSesion(root, k,f) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TSES", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fTutores(root, t,k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": ope.lst, "tipo": t ,"cboCicloAcad":k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Tutor.aspx',
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
function fAux(root, t,k,f,cpf,cai,cat) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "AUX", "tipo": t ,"k":k,"f":f,"codigo_cpf":cpf,"codigo_cai":cai,"cat":cat};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fDatos(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "ALU", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fActividad(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TACT", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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

function fEstado(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TEST", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fRiesgoSeparacion(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fResultado(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TRES", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fNivelRiesgo(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TNRI", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fTipoProblema(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TPRO", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fVariables(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": ope.lst, "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/VariableEvaluacion.aspx',
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
function fOpcionVariables(root, k) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": ope.lst, "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/OpcionVariableEvaluacion.aspx',
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
function fCursos(root, k,f) { // k:codigo_cac 
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "CUR", "k": k , "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
//root:1, k:"C", f:'R'
function fRiesgosEval(root, k, f) { // k:codigo_con // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TEVAL", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fVariableTEval(root, k) { // k:codigo_con // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "VTEVAL", "k": k};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fTutorados(root, k,f,t,cod,cpf) { // k:codigo_con // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": ope.lst, "cboCicloAcad": k, "cboTipo": f ,"tipo":t,"hdcod":cod,"cboCarrera":cpf};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/TutorAlumno.aspx',
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

function fnTipoDocumento(root, k, f) { // k:codigo_eve // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "TipoDocumento", "k": k, "f": f };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/crm/Operaciones.aspx',
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
function fAsistencias(root, k, f,t,c) { // k:codigo_eve // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "AM", "k": k, "f": f ,"tipo":t,"c":c};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fNotas(root, k, f,t,c) { // k:codigo_eve // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": "NM", "k": k, "f": f ,"tipo":t,"c":c};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/Operaciones.aspx',
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
function fSesiones(root, k,f, t,a) { // k:codigo_eve // f:codigo_test
    try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "action": ope.lst, "k": k,"cboCicloAcad": f, "tipo":t,"cboAlumno": a};
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        sroot = sroot + 'DataJson/tutoria/SesionTutor.aspx',
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
    }, 3000);
}

function fnCreateDataTableBasic(table, col, ord,len) {

    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": len,
        "aaSorting": [[col, ord]]
    });
    return dt;
};
function fnCreateDataTableBasicSelect(table, col, ord, len) {
    //console.log('s');
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": len,
        "aaSorting": [[col, ord]],
        'columnDefs': [{
            'targets': 0,
            'searchable': false,
            'orderable': false,
            'width': '1%',
            'className': 'dt-body-center',
            'render': function(data, type, full, meta) {
                return '<input type="checkbox">';
            }
}]
    });
    return dt;
};
function fnCreateDataTableBasicDetalle(table, col, ord, len) {
 
    var dt = $('#' + table).DataTable({
        "bPaginate": false,
        "bFilter": false,
        "bLengthChange": false,
        "bInfo": true,
        "bSort": false,
        "aaSorting": [[col, ord]]
    });
    return dt;
};

function fnResetDataTableBasic(table, col, ord,len) {
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
            "aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": len,
            "aaSorting": [[col, ord]]
        });

        return dt;
    }
}
function fnResetDataTableBasicDetalle(table, col, ord, len) {
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
            "bPaginate": false,
            "bFilter": false,
            "bLengthChange": false,
            "bInfo": true,
            "aaSorting": [[col, ord]],
            "bSort": false
        });

        return dt;
    }
}
function fnResetDataTableBasicDetalle10(table, col, ord, len) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().destroy();
        dt = $('#' + table).DataTable({
            "bPaginate": false,
            "bFilter": false,
            "bLengthChange": false,
            "bInfo": true,
            "aaSorting": [[col, ord]],
            "bSort": false
        });

        return dt;
    }
}
function fnResetDataTableBasic10(table, col, ord, len) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().destroy();
        dt = $('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": len,
            "aaSorting": [[col, ord]],
            
        });

        return dt;
    }
}

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}
function fnDestroyDataTableDetalle10(table) {
    var dt = $('#' + table).DataTable().destroy();;
    return dt;
}
function fnLoading(sw) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
}
function zeroPad(num, places) {
    var zero = places - num.toString().length + 1;
    return Array(+(zero > 0 && zero)).join("0") + num;
}