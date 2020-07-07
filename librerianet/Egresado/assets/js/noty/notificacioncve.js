$(document).ready(function() {
    fnNotificacion();
    
    
});

function fnNotificaciones(c) {

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

            } else {
                $("#divNotificacion").html("");

            }
            $('.piluku-preloader').addClass('hidden');
            
        });
    //}
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
                    li += '<a href="#" onclick="fnNotificaciones(' + data[i].codna + ')">';
                    //li += '<a href="#" )">';
                    li += '<div class="' + data[i].hexagon + '">';
                    li += '<span><i class="' + data[i].icon + '"></i></span>'
                    li += '</div>';
                    if (data[i].r == 1) {

                        li += '<span class="text_info">' + data[i].texto + '</span>';
                        li += '<span class="time_info">' + data[i].fecha + '</span>';
                    } else {
                        r++;
                        li += '<span class="text_info" style="font-weight:bold;">' + data[i].texto + '</span>';
                        li += '<span class="time_info" style="font-weight:bold;">' + data[i].fecha + '</span>';
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
            $('#spNumberMessage').html(r);
            $('#ulNoty').html(li);
        },
        error: function(result) {
            // console.log('error');
            console.log(result);
            //location.reload();
        }
    });
}