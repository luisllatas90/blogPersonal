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
        sroot = sroot + 'DataJson/GradosYTitulos/Operaciones.aspx',
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
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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


function fnFacultad() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarFacultad" /></form>');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnActoAcademico() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarActoAcad" /></form>');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnGrupoEgresado() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarGrupoEgresado" /></form>');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnEspecialidad(cod_pes,vig) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarEspecialidad" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cod" name="cod" value="' + cod_pes + '" />');
    $('#frmOpe').append('<input type="hidden" id="vig" name="vig" value="' + vig + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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


function fnGrado(cod_cp,vig) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarGrado" /></form>');
    $('#frmOpe').append('<input type="hidden" id="cod" name="cod" value="' + cod_cp + '" />');
    $('#frmOpe').append('<input type="hidden" id="vig" name="vig" value="' + vig + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnTipoEstudio() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarTipoEstudio" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="0" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnPlanEstudios(codigo_cpf) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarPlanEstudio" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="' + codigo_cpf + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnCarreraProfesional(cod_test) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarCarreraProf" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="' + cod_test + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnPersonal() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarPersonal" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnCargo() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarCargo" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnPrefijo() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarPrefijo" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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



function fnAutoridad(cod_cgo,cod_fac,vig) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarAutoridad" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param1" name="param1" value="' + cod_cgo + '" />');
    $('#frmOpe').append('<input type="hidden" id="param2" name="param2" value="' + cod_fac + '" />');
    $('#frmOpe').append('<input type="hidden" id="param3" name="param3" value="' + vig + '" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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



function fnTipoDenominacion() {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarTipoDenominacion" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="0" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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

function fnCargosxTest(cod_Test) {
    var arr;
    $('body').append('<form id="frmOpe"><input type="hidden" id="action" name="action" value="ConsultarCargosxTest" /></form>');
    $('#frmOpe').append('<input type="hidden" id="param" name="param" value="'+ cod_Test +'" />');
    var form = $("#frmOpe").serializeArray();
    $("#frmOpe").remove();
    //console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/GradosYTitulos/operaciones.aspx",
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



function fnLoading(sw) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
}

function fnLoadingDiv(div, sw) {
    if (sw) {
        $("#" + div).removeClass('hidden');
    } else {
        $("#" + div).addClass('hidden');
    }
}