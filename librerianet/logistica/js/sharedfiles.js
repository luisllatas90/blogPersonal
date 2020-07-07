
function ModalAdjuntar2(cod_dpe, cod_ped) {
    $("#cod_dpe").val(cod_dpe)
    $("#cod_ped").val(cod_ped)
    $("#txtfile").val("");
    fnVer(cod_dpe)
    $('.AdjuntoProy').attr("data-toggle", "modal");
    $('.AdjuntoProy').attr("data-target", "#mdRegistro");
    //alert('a');
}

function fnVer(c) {
    $.ajax({
        type: "POST",
        url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
        data: { "action": "Ver", "cod_dpe": c },
        dataType: "json",
        cache: false,
        success: function(data) {
            console.log(data);
            var tb = '';
            var i = 0;
            var filas = data.length;
            for (i = 0; i < filas; i++) {
                tb += '<tr>';
                tb += '<td>' + (i + 1) + "" + '</td>';
                tb += '<td><i  class="' + data[i].nExtension + '"></i> ' + data[i].nArchivo + '</td>';
                tb += '<td>';
                tb += '<button onclick="fnDownload(' + data[i].cCod + ');" class="btn btn-primary"><i  class=" ion-android-download"><span></span></i></button>';
                tb += '</td>';
                tb += '</tr>';
            }
            if (filas > 0) $('#mdFiles').modal('toggle');
            $('#tbFiles').html(tb);
        },
        error: function(result) {
        }
    });
}
function fnDownload(id_ar) {
    var flag = false;
    var form = new FormData();
    form.append("action", "Download");
    form.append("cod_Arch", id_ar);
    // alert();
    console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
        data: form,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        async: false,
        success: function(data) {
            flag = true;
            console.log(data);
            var file = 'data:application/octet-stream;base64,' + data[0].File;
            var link = document.createElement("a");
            link.download = data[0].Nombre;
            link.href = file;
            link.click();
            fnMensaje(data[1].tipo, data[1].msje);
        },
        error: function(result) {
            console.log(result);
            fnMensaje(data[1].tipo, data[1].msje);
            flag = false;
        }
    });
    return flag;

}
function fnGuardar() {
    if ($("#txtfile").val() == "") {
        $("#divMessage").html("<p>Debe Selecionar un Archivo.</p>")
    } else {
        $("#btnGuardar").attr("disabled", true);
        fnLoadingDiv("divLoading", true);
        SubirArchivo($("#cod_dpe").val(), $("#cod_dpe").val());
        fnLoadingDiv("divLoading", false);
        //            $('#mdRegistro').modal('hide');
        //            $('#mdRegistro').modal('hide');
        //            fnMensaje('success', data[0].msje);
        //            fnConsultar();

        // fnMensaje('warning', data.msje);
        //            if ($("#" + data[0].obj)) {
        //                $("#" + data[0].obj).focus();
        //                $("#msje").addClass("alert alert-warning");
        //            } else {
        //                $("#msje").addClass("alert alert-danger");
        //            }
        $("#btnGuardar").removeAttr("disabled");
        fnVer($("#cod_dpe").val())
    }
}
function SubirArchivo(c, n) {
    var flag = false;
    var form = new FormData();
    var files = $("#txtfile").get(0).files;
    console.log(files);
    // Add the uploaded image content to the form data collection
    if (files.length > 0) {
        form.append("action", "Upload")
        form.append("cod_dpe", $("#cod_dpe").val())
        form.append("cod_ped", $("#cod_ped").val())
        form.append("UploadedImage", files[0]);
    }
    console.log(form);
    $.ajax({
        type: "POST",
        url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
        data: form,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function(data) {
            flag = true;
            console.log(data);
            $("#txtfile").val("");
            //		              fnMensaje('warning', 'Subiendo Archivo');
            //		              $('#divMessage').addClass('alert alert-success alert-dismissable');
            //		              $fileupload = $('#fileData');
            //		              $fileupload.replaceWith($fileupload.clone(true));

        },
        error: function(result) {
            console.log(result);
            $("#divMessage").html("<p>" & data[0].msje & "</p>");
            flag = false;
        }
    });
    return flag;
}