const g_Des_Titulo_Confirmacion_Defecto = "¿Esta seguro que desea realizar esta acción?";
const g_Des_Mensaje_Confirmacion_Defecto = "Deseo realizar esta operacion";

let Modal_Confirmacion = function (
    p_Des_Titulo = ""
    , p_Des_Mensaje = ""
    , p_Obj_Parametros = null
    , p_Obj_Funcion_Ok = null
    , p_Obj_Funcion_Cancel = null) {

    if (p_Des_Titulo == "" || p_Des_Titulo == null) p_Des_Titulo = g_Des_Titulo_Confirmacion_Defecto;
    if (p_Des_Mensaje == "" || p_Des_Mensaje == null) p_Des_Mensaje = g_Des_Mensaje_Confirmacion_Defecto;

    let dialog = bootbox.dialog({
        title: p_Des_Titulo,
        message: p_Des_Mensaje,
        size: 'large',
        buttons: {
            cancel: {
                label: "Cancelar",
                className: 'btn-danger',
                callback: function () {
                    if (p_Obj_Funcion_Cancel != null) p_Obj_Funcion_Cancel(p_Obj_Parametros);
                }
            },
            ok: {
                label: "Confirmar",
                className: 'btn-info',
                callback: function () {
                    if (p_Obj_Funcion_Ok != null) p_Obj_Funcion_Ok(p_Obj_Parametros);
                }
            }
        }
    });
}

let Modal_Error = function(p_Des_Mensaje = "ERROR",p_Flg_BtnLog = false,p_Obj_Parametros=null,p_Obj_Funcion_Ok = null) {
    let l_Obj_Error = {
        title: '<h4 style="color: red;">! HA OCURRIDO UN ERROR !</h4>',
        message: '<div>'+p_Des_Mensaje+'</div>',
        size: 'large',
        buttons: {
            ok: {
                label: "OK",
                className: 'btn-info',
                callback: function () {
                    if (p_Obj_Funcion_Ok != null) p_Obj_Funcion_Ok(p_Obj_Parametros);
                }
            }
        }
    }

    if( p_Flg_BtnLog == true )
    {
        l_Obj_Error.buttons.Log = {
            label: "Descargar Log Error",
            className: 'btn-danger',
            callback: function () {
                Crear_Log_Error(p_Obj_Parametros);
            }
        }
    }

    let dialog = bootbox.dialog(l_Obj_Error);
}

let Mostrar_Modal_Confirmacion = function (Flg_Respuesta = false) {

    if (Flg_Respuesta == true) {
        $(".loader").css("position", "fixed");
        $(".loader").css("left", "0px");
        $(".loader").css("top", "0px");
        $(".loader").css("width", "100%");
        $(".loader").css("height", "100%");
        $(".loader").css("z-index", "9999");
        $(".loader").css("background", "url('assest/images/bien.gif') 50% 50% no-repeat rgb(209, 211, 211)");
        $(".loader").css("opacity", ".8");
    }

}

let CrearHtml_Opciones_Tabla = function (p_Obj_Config) {

    // p_Obj_Config = {
    //     Btn_Mostrar: {
    //         Clase : '',
    //         Clase_Btn : '',
    //         Id_Opciones : '',
    //         Id: 1,
    //         IdAux1: 2,
    //         IdAux2: 3,
    //         Descripcion: "boton",
    //         Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>',
    //         Link: ''
    //     },
    //     Btn_Ocultar: {
    //         Clase : '',
    //         Clase_Btn : '',
    //         Id_Opciones : '',
    //         Id: 1,
    //         IdAux1: 2,
    //         IdAux2: 3,
    //         Descripcion: "boton",
    //         Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>',
    //         Link: '',
    //         List_Opciones: [
    //             {
    //                 Clase_Btn : '',
    //                 Id: 1,
    //                 IdAux1: 2,
    //                 IdAux2: 3,
    //                 Descripcion: "boton",
    //                 Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>',
    //                 Link: ''
    //             }
    //         ]
    //     }
    // }

    let l_Html = "";

    if (p_Obj_Config.Btn_Mostrar) {

        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Clase') == false) p_Obj_Config.Btn_Mostrar.Clase = "";
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Clase_Btn') == false) p_Obj_Config.Btn_Mostrar.Clase_Btn = "btn_mostrar_opciones";
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Id_Opciones') == false) p_Obj_Config.Btn_Mostrar.Id_Opciones = 0;
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Id') == false) p_Obj_Config.Btn_Mostrar.Id = 0;
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('IdAux1') == false) p_Obj_Config.Btn_Mostrar.IdAux1 = 0;
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('IdAux2') == false) p_Obj_Config.Btn_Mostrar.IdAux2 = 0;
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Descripcion') == false) p_Obj_Config.Btn_Mostrar.Descripcion = "";
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Icono') == false) p_Obj_Config.Btn_Mostrar.Icono = "";
        if (p_Obj_Config.Btn_Mostrar.hasOwnProperty('Link') == false) p_Obj_Config.Btn_Mostrar.Link = "#";

        l_Html += '<div class="list-group ' + p_Obj_Config.Btn_Mostrar.Clase + '" id="'+p_Obj_Config.Btn_Mostrar.Id_Opciones+'">';
        l_Html += '<a href="' + p_Obj_Config.Btn_Mostrar.Link + '"';
        l_Html += 'class="list-group-item list-group-item-action active ' + p_Obj_Config.Btn_Mostrar.Clase_Btn + '"';
        l_Html += 'id="' + p_Obj_Config.Btn_Mostrar.Id + '"  IdAux1="' + p_Obj_Config.Btn_Mostrar.IdAux1 + '"  IdAux2="' + p_Obj_Config.Btn_Mostrar.IdAux2 + '">';
        l_Html += p_Obj_Config.Btn_Mostrar.Descripcion;
        l_Html += '</a>';
        l_Html += '</div>';

    }

    if (p_Obj_Config.Btn_Ocultar) {

        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Clase') == false) p_Obj_Config.Btn_Ocultar.Clase = "";
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Clase_Btn') == false) p_Obj_Config.Btn_Ocultar.Clase_Btn = "btn_ocultar_opciones";
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Id_Opciones') == false) p_Obj_Config.Btn_Ocultar.Id_Opciones = 0;
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Id') == false) p_Obj_Config.Btn_Ocultar.Id = 0;
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('IdAux1') == false) p_Obj_Config.Btn_Ocultar.IdAux1 = 0;
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('IdAux2') == false) p_Obj_Config.Btn_Ocultar.IdAux2 = 0;
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Descripcion') == false) p_Obj_Config.Btn_Ocultar.Descripcion = "";
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Icono') == false) p_Obj_Config.Btn_Ocultar.Icono = "";
        if (p_Obj_Config.Btn_Ocultar.hasOwnProperty('Link') == false) p_Obj_Config.Btn_Ocultar.Link = "#";

        l_Html += '<div class="list-group ' + p_Obj_Config.Btn_Ocultar.Clase + '" id="'+p_Obj_Config.Btn_Ocultar.Id_Opciones+'" style="display: none;">';
        l_Html += '    <a href="#"';
        l_Html += '        class="list-group-item list-group-item-action active ' + p_Obj_Config.Btn_Ocultar.Clase_Btn + '"';
        l_Html += '        id="' + p_Obj_Config.Btn_Ocultar.Id + '" IdAux1="' + p_Obj_Config.Btn_Ocultar.IdAux1 + '"  IdAux2="' + p_Obj_Config.Btn_Ocultar.IdAux2 + '">';
        l_Html += '        Ocultar opciones';
        l_Html += '    </a>';

        $.each(p_Obj_Config.Btn_Ocultar.List_Opciones, function (i, item) {

            if (item.hasOwnProperty('Clase_Btn') == false) item.Clase_Btn = "";
            if (item.hasOwnProperty('Id') == false) item.Id = 0;
            if (item.hasOwnProperty('IdAux1') == false) item.IdAux1 = 0;
            if (item.hasOwnProperty('IdAux2') == false) item.IdAux2 = 0;
            if (item.hasOwnProperty('Descripcion') == false) item.Descripcion = "";
            if (item.hasOwnProperty('Icono') == false) item.Icono = "";
            if (item.hasOwnProperty('Link') == false) item.Link = "#";            

            l_Html += '<a href="' + item.Link + '" class="list-group-item list-group-item-action '+item.Clase_Btn+'"';
            l_Html += ' id="'+item.Id+'" ';
            l_Html += ' IdAux1="'+item.IdAux1+'" ';
            l_Html += ' IdAux2="'+item.IdAux2+'" ';
            l_Html += '>';
            l_Html += '     ' + item.Icono + '';
            l_Html += '     ' + item.Descripcion + '';
            l_Html += '</a>';
        });

        l_Html += '</div>';

    }

    return l_Html;

}

let CrearHtml_Opciones_Tabla_Array = function (p_Array_Datos,p_Id = 0, p_Id_Aux1=0,p_Id_Aux2=0, p_Des_Clase = "opciones") {

    let l_Obj_Config = {};

    if (p_Array_Datos != null) {

        l_Obj_Config = {
            Btn_Mostrar: {
                Clase: p_Des_Clase+"_mostrar",
                Clase_Btn: "btn_"+p_Des_Clase+"_mostrar",
                Id_Opciones : "div_mostrar_"+p_Id,
                Id: p_Id,
                IdAux1: p_Id_Aux1,
                IdAux2: p_Id_Aux2,
                Descripcion: "Mostrar Opciones",
                Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>',
                Link: '#'
            },
            Btn_Ocultar: {
                Clase: p_Des_Clase+"_ocultar",
                Clase_Btn: "btn_"+p_Des_Clase+"_ocultar",
                Id_Opciones : "div_ocultar_"+p_Id,
                Id: p_Id,
                IdAux1: p_Id_Aux1,
                IdAux2: p_Id_Aux2,
                Descripcion: "Ocultar Opciones",
                Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>',
                Link: '#',
                List_Opciones: []
            }
        }

        for (let i = 0; i < p_Array_Datos.length; i++) {

            l_Obj_Config.Btn_Ocultar.List_Opciones.push(
                {
                    Clase_Btn : p_Array_Datos[i].Clase_Btn,
                    Id: p_Id,
                    IdAux1: p_Id_Aux1,
                    IdAux2: p_Id_Aux2,
                    Descripcion: p_Array_Datos[i].Descripcion,
                    Icono: p_Array_Datos[i].Icono,
                    Link: p_Array_Datos[i].Link
                }
            );

        }
    }

    return CrearHtml_Opciones_Tabla(l_Obj_Config);

}

let CrearHtml_Tabla = function( p_Id_Tabla = "", p_Datos_Tabla = [])
{
    let l_Html = "";

    $('#' + p_Id_Tabla + ' tbody').empty();

    if( p_Datos_Tabla != null && p_Datos_Tabla.length > 0 )
    {
        
        for(let y = 0; y < p_Datos_Tabla.length; y++)
        {
            l_Html += '<tr>';
            for(let x = 0; x < p_Datos_Tabla[y].length; x++)
            {
                l_Html += '<td>' +p_Datos_Tabla[y][x] + '</td>';
            }
            l_Html += '</tr>';
        }
        
    }
    
    $('#' + p_Id_Tabla).append(l_Html);
}

let Crear_Log_Error = function(params) {
    
}


$(document).on('click', '.btn_opciones_mostrar', function (e) {       
    let l_Id = $(this).attr("id");
    $("#div_mostrar_" + l_Id).hide();
    $("#div_ocultar_" + l_Id).show();
});

$(document).on('click', '.btn_opciones_ocultar', function (e) {
    let l_Id = $(this).attr("id");
    $("#div_mostrar_" + l_Id).show();
    $("#div_ocultar_" + l_Id).hide();
});
