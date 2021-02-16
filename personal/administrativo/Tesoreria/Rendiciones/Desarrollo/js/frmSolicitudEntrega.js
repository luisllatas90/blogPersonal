$(document).ready(function() {
    listarPersonaDatos();
    $('[data-toggle="tooltip"]').tooltip();   
    //agregarAtendido();
});

let g_ListPedidoSeleccionados = [];
let g_ListDetallePedido = [];
let g_ListPersonaSeleccionados = [];
let g_ListTemporalPersona = [];
let g_IndiceModalPersona = 0;

let listarPersonaDatos = function () {

    let data = {
        nPerCodigo: 0
    }

    $.ajax({
        type: "POST",
        url: "DataJson/SolicitudEntregaJson.aspx",
        data: { Funcion: "listarPersonaDatos", data: JSON.stringify(data) },
        dataType: "json",
        cache: false,
        success: function (response) {
            var foo = response;
            if (foo != null && $.isArray(foo)) {
                $.each(foo, function (index, value) {
                    $('#txtSolicitante').val(value.cPerApePaterno + ' ' + value.cPerApeMaterno + ' ' + value.cPerNombres);
                    $('#txtEmail').val(value.cPersonaEmail);
                });
            }
        },
        error: function (result) {

        }
    });
}

/* ========================  Detalle Pedido  ========================== */

let listarDetallePedido = function(l_nPedidoCodigo){
    let data = {
        nPedLogCodigo: l_nPedidoCodigo
    }

    $.ajax({

        type: "POST",
        url: "DataJson/SolicitudEntregaJson.aspx",
        data: { Funcion: "listarDetallePedidoLogistica", data: JSON.stringify(data) },
        dataType: "json",
        cache: false,

        success: function (response) {
            setTimeout(() => {
                llenarArregloDetallePedido(response);
            }, 1200); 
        },
        error: function (result) {

        }
    });
}

let llenarArregloDetallePedido = function(response){
    let l_flagexiste = false;
    var foo = response;
    if (foo != null && $.isArray(foo)) {
        $.each(foo, function (index, value) {
            l_flagexiste = g_ListDetallePedido.some( elemento => elemento.nPedLogDetalleCodigo == value.nPedLogDetalleCodigo );
            if(l_flagexiste != true){
                g_ListDetallePedido.push({ 
                    nPedLogDetalleCodigo: value.nPedLogDetalleCodigo,
                    nPedLogCodigo: value.nPedLogCodigo,
                    fPedLogImporte: value.fPedLogImporte,
                    nArticuloCodigo: value.nArticuloCodigo,
                    cArticuloDescripcion: value.cArticuloDescripcion,
                    fPedLogDetalleImporte: value.fPedLogDetalleImporte
                 }
                );
            }
        });
    }  
    llenarTablaDetallePedido();
}

let llenarTablaDetallePedido = function(){
    let l_arrayFilas = [];
    let l_arrayTabla = [];
    if (g_ListDetallePedido != null && $.isArray(g_ListDetallePedido)) {
        $.each(g_ListDetallePedido, function (index, value) {
                l_arrayFilas = ['<a href="#" data-toggle="tooltip" data-placement="top" title="Quitar Pedido" class="btnQuitarDetallePedido" id="'+ value.nPedLogCodigo +'"><i style="color:red" class="fa-lg fa fa-trash"></i></a>',
                value.nPedLogCodigo, 
                value.cArticuloDescripcion,
                value.fPedLogDetalleImporte];
                l_arrayTabla.push(l_arrayFilas);             
        });
        $('#tbDetallePedidos thead').empty();
        llenarTablaCabeceraPedidoDetalle();
    }
    CrearHtml_Tabla('tbDetallePedidos', l_arrayTabla);
    //$('[data-toggle="tooltip"]').tooltip();
}

let llenarTablaCabeceraPedidoDetalle = function(){
    let l_Html = '';
    l_Html +='<tr>';
    l_Html +=   '<th>Eliminar</th>';
    l_Html +=   '<th>Nro Pedido</th>';
    l_Html +=   '<th>Artículo</th>';
    l_Html +=   '<th>Importe</th>';
    l_Html += '</tr>'
    $('#tbDetallePedidos thead').append(l_Html);
}

$(document).on('click', '.btnSeleccionarPedido', function (e) {       
    let l_nPedidoCodigo = $(this).attr("id");
    g_ListPedidoSeleccionados.push({ nPedLogCodigo: l_nPedidoCodigo});
    listarDetallePedido(l_nPedidoCodigo);
    vaciarTablaPedidos();
    listarPedidoLogistica();
});

$(document).on('click', '.btnQuitarDetallePedido', function (e) {       
    let l_nPedidoCodigo = $(this).attr("id");
    eliminarDetallePedido(l_nPedidoCodigo);
    eliminarPedido(l_nPedidoCodigo);
    if(g_ListDetallePedido.length == 0){
        $('#tbDetallePedidos thead').empty();
    }
});

let eliminarPedido = function(l_nPedidoCodigo){
    let g_ListPedido_Temporal = []; 
    if (g_ListPedidoSeleccionados != null && $.isArray(g_ListPedidoSeleccionados)) {
        $.each(g_ListPedidoSeleccionados, function (index, value) {
            if(value.nPedLogCodigo != l_nPedidoCodigo){
                g_ListPedido_Temporal.push({ 
                    nPedLogCodigo: value.nPedLogCodigo
                    }
                );
            }
        });
    }

    g_ListPedidoSeleccionados = g_ListPedido_Temporal;
    llenarTablaDetallePedido();
}

let eliminarDetallePedido = function (l_nPedidoCodigo){
    /*let l_ListDetallePedido_Temporal = g_ListDetallePedido; 

    if (l_ListDetallePedido_Temporal != null && $.isArray(l_ListDetallePedido_Temporal)) {
        $.each(l_ListDetallePedido_Temporal, function (index, value) {
            
            if(value.nPedLogCodigo == l_nPedidoCodigo){
                const obj = l_ListDetallePedido_Temporal[index];
                const valor = g_ListDetallePedido.indexOf(obj);
                console.log(valor);
                const g_ListDetalle = g_ListDetallePedido.splice(valor, 1);
                console.log(g_ListDetalle);
           }
       });
    }*/

    let g_ListDetallePedido_Temporal = []; 
    if (g_ListDetallePedido != null && $.isArray(g_ListDetallePedido)) {
        $.each(g_ListDetallePedido, function (index, value) {
            if(value.nPedLogCodigo != l_nPedidoCodigo){
                g_ListDetallePedido_Temporal.push({ 
                    nPedLogDetalleCodigo: value.nPedLogDetalleCodigo,
                    nPedLogCodigo: value.nPedLogCodigo,
                    fPedLogImporte: value.fPedLogImporte,
                    nArticuloCodigo: value.nArticuloCodigo,
                    cArticuloDescripcion: value.cArticuloDescripcion,
                    fPedLogDetalleImporte: value.fPedLogDetalleImporte
                    }
                );
            }
        });
    }

    g_ListDetallePedido = g_ListDetallePedido_Temporal;
    llenarTablaDetallePedido();
}

/* ========================  Modal Pedido  ========================== */
let listarPedidoLogistica = function(){

    let l_nPedidoCodigo = document.getElementById("txtPedidoCodigo").value;
    
    if(l_nPedidoCodigo == ''){
        l_nPedidoCodigo = 0;
    }

    let data = {
        nPedLogCodigo: l_nPedidoCodigo
    }

    $.ajax({
        type: "POST",
        url: "DataJson/SolicitudEntregaJson.aspx",
        data: { Funcion: "listarPedidoLogistica", data: JSON.stringify(data) },
        dataType: "json",
        cache: false,
        success: function (response) {
            $('#tbPedidos').DataTable().destroy();
            var foo = response;
            let l_arrayFilas = [];
            let l_arrayTabla = [];
            let l_flagexiste = false;
            if (foo != null && $.isArray(foo)) {
                $.each(foo, function (index, value) {
                    l_flagexiste = g_ListPedidoSeleccionados.some( elemento => elemento.nPedLogCodigo == value.nPedLogCodigo );
                    if(l_flagexiste != true){
                        l_arrayFilas = ['<a href="#" data-toggle="tooltip" data-placement="top" title="Seleccionar Pedido '+ value.nPedLogCodigo +'" class="btnSeleccionarPedido" id="'+ value.nPedLogCodigo +'"><i  class="fa-lg fa fa-check-circle"></i></a>',
                        value.nPedLogCodigo, 
                        value.fPedLogImporte, 
                        value.dPedLogFecha, 
                        value.cPerApePaterno + ' ' + value.cPerApeMaterno + ' ' + value.cPerNombres,
                        value.cCcDescripcion];
                        l_arrayTabla.push(l_arrayFilas);
                    }
                    l_flagexiste = false;
                });

                $('#tbPedidos thead').empty();
                llenarTablaCabeceraPedido();
            }
            CrearHtml_Tabla('tbPedidos', l_arrayTabla);
            $('#tbPedidos').DataTable();
            //$('[data-toggle="tooltip"]').tooltip();
        },
        error: function (result) {

        }
    });
}

let vaciarTablaPedidos = function (){
    $('#tbPedidos thead').empty();
    $('#tbPedidos tbody').empty();
    llenarTablaCabeceraPedido();
}

let llenarTablaCabeceraPedido = function(){
    let l_Html = '';
    l_Html +='<tr>';
    l_Html +=   '<th>Seleccione</th>';
    l_Html +=   '<th>Nro Pedido</th>';
    l_Html +=   '<th>Importe</th>';
    l_Html +=   '<th>Fecha Pedido</th>';
    l_Html +=   '<th>Solicita</th>';
    l_Html +=   '<th>Centro Costo</th>';
    l_Html += '</tr>'
    $('#tbPedidos thead').append(l_Html);
}

$(document).on('click', '#btnListarPedido', function (e) {   
    vaciarTablaPedidos();  
    $('#tbPedidos').DataTable().destroy();
    listarPedidoLogistica();
});

$(document).on('click', '#mostrarModalPedido', function (e) {    
    vaciarTablaPedidos();
});

$(document).on('click', '#btnCerraModalPedido', function (e) {       
    $('#tbPedidos').DataTable().destroy();
});

$(document).on('click', '#btnCerraModalPedido2', function (e) {       
    $('#tbPedidos').DataTable().destroy();
});

/* ========================  Atendido ========================== */
//Agregar fila nueva.
$('#tbAtendido .btnAgregarAtendido').click(function(){
    agregarAtendido();
});

let agregarAtendido = function(){
    vaciarTablaAtendido();

	var tbody = $('#tbAtendido tbody');
	//var fila_contenido = tbody.find('tr').first().html();
    var sOut = '';

    g_ListPersonaSeleccionados.push(
        { 
            nPerCodigo: '',
            cPerNombres: '',
            cPerCtaBancaria: '',
            cPerMail: ''
        }
    );

    $.each(g_ListPersonaSeleccionados, function (index, value) {
        sOut = '<th scope="row">' +
                    '<button type="button" class="btn btn-danger btnEliminarAtendido">' +
                        '<i class="fa fa-times"></i>' +
                    '</button>'+
                '</th>'+
                '<td>'+
                    '<div class="input-group">'+
                        '<input type="text" id="txtNombre'+ index +'" class="form-control form-control-sm" readonly="true">'+
                        '<div class="input-group-prepend">'+
                            '<button type="button" onclick="obtenerIndexAtendido(' + index +')" class="btn btn-primary" data-toggle="modal" data-target="#modalPersona">'+
                                '<i class="fa fa-search" data-toggle="tooltip" data-placement="top" title="Buscar Personal"></i>'+
                            '</button>'+
                        '</div>'+
                    '</div>'+
                '</td>'+
                '<td>'+
                    '<input type="text" id="txtCtaBancariaAtendido'+ index +'" class="form-control form-control-sm" readonly="true">'+
                '</td>' +
                '<td>' +
                    '<input type="text" id="txtBancoAtendido'+ index +'" class="form-control form-control-sm" readonly="true">' +
                '</td>'+
                '<td>'+
                    '<input type="number" id="txtMontoAsignadoAtendido'+ index +'" class="form-control form-control-sm"></td>' +
                '</td>'+
                '<td>' +
                    '<input type="text" id="txtxEmailAtendido'+ index +'" class="form-control form-control-sm" readonly="true">' +
                '</td>';        
    });

    var fila_contenido = sOut;
    var fila_nueva = $('<tr></tr>');
	fila_nueva.append(fila_contenido);
	tbody.append(fila_nueva);
}

//Eliminar fila.
$('#tbAtendido').on('click', '.btnEliminarAtendido', function(){	
    g_ListPersonaSeleccionados.splice(g_IndiceModalPersona, 1);
    console.log(g_ListPersonaSeleccionados);
    $(this).parents('tr').eq(0).remove();
});

let llenarValoresTablaAtendido = function (l_nPerCodigo){
    console.log(l_nPerCodigo);
    $.each(g_ListTemporalPersona, function (index, value) {
        if(l_nPerCodigo == value.nPerCodigo){
            $("#txtNombre" + g_IndiceModalPersona + "").val(value.cPerApePaterno + ' ' + value.cPerApeMaterno + ' ' + value.cPerNombres);
            $("#txtxEmailAtendido" + g_IndiceModalPersona + "").val(value.cPerEmail);
        }
    });
    g_ListTemporalPersona = [];
}

let vaciarTablaAtendido = function (){
    $('#tbAtendido thead').empty();
    llenarCabeceraAtendido();
}

let llenarCabeceraAtendido = function (){
    let l_Html = '';
    l_Html +='<tr>';
    l_Html +=   '<th></th>';
    l_Html +=   '<th>Nombre Completo</th>';
    l_Html +=   '<th>(***) N° de cuenta bancaria</th>';
    l_Html +=   '<th>(***) Banco</th>';
    l_Html +=   '<th>Monto asignado (S/)</th>';
    l_Html +=   '<th>Correo</th>';
    //l_Html +=   '<th>(****) Responsable</th>';
    l_Html += '</tr>'
    $('#tbAtendido thead').append(l_Html);
}

/* ========================  Modal Persona  ========================== */
let obtenerIndexAtendido = function(nIndex){
    g_IndiceModalPersona = nIndex;
}

let listarPersona = function(){
    let l_cPersonaApellido;
    let l_cPersonaDocumento;
    let l_nOpcion = 1;
    
    if(l_cPersonaApellido != ''){
        l_nOpcion = 1;
        l_cPersonaApellido = document.getElementById("txtBuscaPersonaApellido").value;
    }else if(l_cPersonaDocumento != ''){
        l_cPersonaDocumento = document.getElementById("txtBuscaPersonaDocumento").value;
        l_nOpcion = 2;
    }

    let data = {
        nOpcion: l_nOpcion,
        cPerApellidos: l_cPersonaApellido,
        cPersonaDocumento: l_cPersonaDocumento
    }

    $.ajax({
        type: "POST",
        url: "DataJson/SolicitudEntregaJson.aspx",
        data: { Funcion: "listarPersona", data: JSON.stringify(data) },
        dataType: "json",
        cache: false,
        success: function (response) {
            $('#tbPersona').DataTable().destroy();
            var foo = response;
            g_ListTemporalPersona = response;
            let l_arrayFilas = [];
            let l_arrayTabla = [];
            let l_flagexiste = false;
            if (foo != null && $.isArray(foo)) {
                $.each(foo, function (index, value) {
                    l_flagexiste = g_ListPersonaSeleccionados.some( elemento => elemento.nPerCodigo == value.nPerCodigo );
                    if(l_flagexiste != true){
                        l_arrayFilas = ['<a href="#" data-toggle="tooltip" data-placement="top" title="Seleccionar Persona '+ value.nPerCodigo +'" class="btnSeleccionarPersona" id="'+ value.nPerCodigo +'"><i  class="fa-lg fa fa-check-circle"></i></a>',
                        value.cPerApePaterno + ' ' + value.cPerApeMaterno + ' ' + value.cPerNombres,
                        value.cPerEmail];
                        l_arrayTabla.push(l_arrayFilas);
                    }
                    l_flagexiste = false;
                });

                $('#tbPersona thead').empty();
                llenarTablaCabeceraPersona();
            }
            CrearHtml_Tabla('tbPersona', l_arrayTabla);
            $('#tbPersona').DataTable();
        },
        error: function (result) {

        }
    });
};

$(document).on('click', '#btnBuscaPersonaApellido', function (e) {   
    vaciarTablaPersona();  
    $('#tbPersona').DataTable().destroy();
    listarPersona();
});

$(document).on('click', '#btnBuscaPersonaDocumento', function (e) {   
    vaciarTablaPersona();  
    $('#tbPersona').DataTable().destroy();
    listarPersona();
});

$(document).on('click', '.btnSeleccionarPersona', function (e) {       
    let l_nPerCodigo = $(this).attr("id");    
    g_ListPersonaSeleccionados.push({ nPerCodigo: l_nPerCodigo});
    $('#tbPersona').DataTable().destroy();
    vaciarTablaPersona();
    llenarValoresTablaAtendido(l_nPerCodigo);
});

let vaciarTablaPersona = function (){
    $('#tbPersona thead').empty();
    $('#tbPersona tbody').empty();
    llenarTablaCabeceraPersona();
}

let llenarTablaCabeceraPersona = function(){
    let l_Html = '';
    l_Html +='<tr>';
    l_Html +=   '<th>Seleccione</th>';
    l_Html +=   '<th>Apellidos y Nombres</th>';
    l_Html +=   '<th>Email</th>';
    l_Html += '</tr>'
    $('#tbPersona thead').append(l_Html);
}