$(document).ready(function(){
    ObtenerListaRendicion(1,"F");
});

const g_List_Opciones_Tbl = [
    {
        Clase_Btn: "btn_ver_detalle",
        Descripcion: " Ver detalle solicitud",
        Link: "#",
        Icono: '<i class="fa fa-search fa-3" aria-hidden="true"></i>'
    },
    {
        Clase_Btn: "btn_dar_conformidad",
        Descripcion: " Dar conformidad",
        Link: "#",
        Icono: '<i class="fa fa-file fa-3" aria-hidden="true"></i>'
    }
];

let ObtenerListaRendicion = function (p_nTipEstado = 0, p_bFlgEstado = "") {
    let l_arrayTabla = [];

    let l_Data = {
        nTipEstado: p_nTipEstado,
        bFlgEstado: p_bFlgEstado
    }

    $.ajax({

        type: "POST",
        url: "controller/BandejaGestionRendicionesController.aspx",
        data: { Funcion: "ObtenerListaRendicion", Data: JSON.stringify(l_Data) },
        dataType: "json",
        cache: false,

        success: function (data) {

            if (data.LogError.bFlag == false) {
                $.each(data.Resultado, function (index, item) {
                    l_arrayTabla.push(
                        [
                            (index + 1)
                            , item.dFechaEgreso
                            , item.cDesAtendido
                            , item.cDescripcion
                            , item.nDesImporte
                            , "Finalizados"
                            , CrearHtml_Opciones_Tabla_Array(g_List_Opciones_Tbl, item.nCodigo, 0, 0)
                        ]
                    );
                });
                // CrearHtml_Tabla("tbl_Conformidad_Rendicion", l_arrayTabla);
            } else {
                Modal_Error(data.LogError.cMensaje);
            }

        },
        error: function (result) {

        }
    });
}