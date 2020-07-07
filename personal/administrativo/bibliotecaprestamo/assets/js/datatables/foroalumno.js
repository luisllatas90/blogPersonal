/*$(document).ready(function(e) {
   // $("#btnGinc").click(uploadFile);
    $("#flArchivo").change(function(e) {
        var t = $("#flArchivo").val();
        $("#lblflArchivo").html(t);
    });
   
});*/
function uploadFile() {
    var event = jQuery.Event("preventDefault");
    $(document).trigger(event);

    var asunto = $("#txtincasunto").val();
    var msje = $("#txtincmsje").val();
    var input = $("#flArchivo")[0];

    /*var frm = new FormData();
    console.log(frm);*/

    var myForm = document.querySelector('frmForo');
    frm = new FormData(myForm);
    
    frm.append('asunto', asunto);
    frm.append('mensaje', msje);
    frm.append('archivo', input.files[0]);
    console.log(frm);
    
    //console.log(input.files[0]);
    /*
    var form = new FormData();
    form.append('asunto', asunto);
    form.append('mensaje', msje);
    form.append('file', input.files[0]);
    console.log(input.files[0]);
    console.log(form);*/    
    //$('.piluku-preloader').removeClass('hidden');
    //console.log(input.files[0]);
    //var form = $.parseJSON($("#frmForo").serializeArray());
    //form.append('file', input.files[0]);
   
    /* 
   $.ajax({
        type: "GET",
        //headers: { "Cache-Control":"no-cache", "Content-Type":"multipart/form-data" },
        //headers: { 'Cache-Control': 'no-cache' },
        //  contentType: "application/json; charset=utf-8",
        url: "procesar.aspx",
        //data: { "param0": "regAluf", asunto: asunto, msje: msje, file: input.files[0] },
        data: { },
        //processData: false,
        //contentType: false,
        dataType: "json",
        success: function(data) {
            console.log(data);
            //$('.piluku-preloader').addClass('hidden');
            fnMensaje(data[0].alert, data[0].msje)
            if (data[0].r) {
                $('#mdForoReg').modal('hide');
                fnIncidentes();
                $("#PanelInc").focus();
            }
        },
        error: function(result) {
            console.log(result);
            // f_Menu("nivelacioncurso.aspx");
        }
    });*/
    event.preventDefault();
}

function fnIncidentes() {
       
        $("#divContentForo").html("<br><br><br>");
        //if (page.length > 0) {
        /*  $("#divleft").removeClass("left-bar menu_appear").addClass("left-bar");
        $("#divleft").css(["overflow", "hidden", "outline", "none"]);
        $("#divOverlay").removeClass("overlay show").addClass("overlay");
        $('.piluku-preloader').removeClass('hidden');*/
        fnDivRefresh('divContentForo',1000);
        $.post("incidente.aspx", {}, function(data, status) {
            if (status == 'success') {
                // $("#divContent").html(data);
                $("#divContentForo").empty()
                $("#divContentForo").html(data);
                $("#PanelInc").focus();


            } else {
                $("#divContentForo").html("");
            }

        });
        //}
    }
