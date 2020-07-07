$(document).ready(function() {
    fnNotificacion();
    
    
});

function fnNotificaciones(c) {
    $("#divContent").html("");
    //if (page.length > 0) {
      /*  $("#divleft").removeClass("left-bar menu_appear").addClass("left-bar");
        $("#divleft").css(["overflow", "hidden", "outline", "none"]);
        $("#divOverlay").removeClass("overlay show").addClass("overlay");
        $('.piluku-preloader').removeClass('hidden');*/
    $('.piluku-preloader').addClass('hidden');
        $.post("notificaciones.aspx", { c: c
        }, function(data, status) {
            if (status == 'success') {
                // $("#divContent").html(data);
                $("#divNotificacion").empty()
                $("#divNotificacion").html(data);
                $("#divNotificacion").reload();
                //var link = $('#liNoty').attr("href");
                //$("#divNotificacion").load(link);
                //event.preventDefault();
            } else {
                $("#divNotificacion").html("");

            }
            $('.piluku-preloader').addClass('hidden');
            
        });
    //}
    }

function fnDetNoty(c) {
    $('#spNumberMessage').html("");
    var li = "";
    var r = 0;
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "processnoty.aspx",
        data: { "param0": "lstDetNoty", "param2": c },
        dataType: "json",
        async: false,
        success: function(data) {
            console.log(data);
            if (data.length > 0) {
                fnLeer(data[0].codna);
                $("#h1Cab").html(data[0].texto + '<span>Publicado ' + data[0].fecha + '</span>');
                $('#pDesc').html(data[0].desc);
                $('#pImgDesc').html(data[0].imgDesc);
                if (data[0].imgFile != "") { $('#imgFile').attr("src", data[0].imgFileUrl); } else { $('#imgFile').attr("src", ""); }
                $('#pDescFile').html(data[0].fileDesc);
                if (data[0].file != "") { $('#liFile').html('<a href="' + data[0].fileUrl + '"><i class="ti-cloud-down flatBluec"></i> ' + data[0].file + ' <span><i class="ti-download"></i></span></a>'); }
                else { $('#liFile').html(''); }
            }
        },
        error: function(result) {
            // console.log('error');
            console.log(result);
            //location.reload();
        }

    });
}

function fnLeer(i) {
    $('#i'+i).hide();
    
}


function fnNotificacion() {
   
    var li = "";
    var r = 0;
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "processnoty.aspx",
        data: { "param0": "lstNoty" },
        dataType: "json",
        async: false,
        success: function(data) {
            // console.log(data);
            if (data) {
                //aDtNiv = data;

                for (var i = 0; i < data.length; i++) {
                    li += '<li>';
                    li += '<a id="liNoty" href="#divInfoNoty" onclick="fnNotificaciones(' + data[i].codna + ')">';
                    //li += '<a href="#" )">';
                    li += '<div class="' + data[i].hexagon + '">';
                    li += '<span><i class="' + data[i].icon + '"></i></span>'
                    li += '</div>';
                    if (data[i].r == 1) {

                        li += '<span class="text_info">' + data[i].texto + '</span>';
                        li += '<span class="time_info">Publicado ' + data[i].fecha + '</span><br>';
                        li += '<span class="time_info">' + data[i].msj + '</span>';
                    } else {
                        r++;
                        li += '<span class="text_info" style="font-weight:bold;"><i class="ion ion-record flatBluec status"></i>' + data[i].texto + '</span>';
                        li += '<span class="time_info" style="font-weight:bold;">Publicado ' + data[i].fecha + '</span><br>';
                        li += '<span class="time_info" style="font-weight:bold;">' + data[i].msj + '</span>';
                    }

                    li += '</a>';
                    li += '</li>';
                    if (i == 4) {
                        li += '<li>';
                        li += '<a href="#" class="last_info" onclick="fnNotificaciones()">Ver todas las notificaciones</a>';
                        li += '</li>';
                        i = data.length;
                    }

                }
            }
            if (r > 0) { $('#spNumberMessage').html(r); }
            $('#ulNoty').html(li);
            
        },
        error: function(result) {
            // console.log('error');
            console.log(result);
            //location.reload();
        }
    });
}