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