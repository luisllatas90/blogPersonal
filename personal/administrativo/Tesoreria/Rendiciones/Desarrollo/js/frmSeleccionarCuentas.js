$(document).ready(function() {
    listarCuentasBancarias();
});

let listarCuentasBancarias = function (){

    $.ajax({
        type: "POST",
        url: "DataJson/SeleccionarCuentasJson.aspx",
        data: { Funcion: "listarCuentaBancaria" },
        dataType: "json",
        cache: false,
        success: function (response) {
            var foo = response;
            var sOut = '';

            if (foo != null && $.isArray(foo)) {
                $.each(foo, function (index, value) {
                    if (index == 0){
                        sOut += '<tr>';
                        sOut += '<td colspan="3" style"background-color: rgba(0, 0, 0, 0.03);"><h3 class="h4">' + value.cBanNombre + '</h3></td>';
                        sOut += '</tr>';
                    }
                    if (index > 0){
                        if (value.nBanCodigo != foo[index - 1].nBanCodigo) {
                            sOut += '<tr>';
                            sOut += '<td colspan="3" style"background-color: rgba(0, 0, 0, 0.03);"><h3 class="h4">' + value.cBanNombre + '</h3></td>';
                            sOut += '</tr>';
                        }
                    }

                    sOut += '<tr>';
                    sOut += '<td>' + value.cBanNombre + '</td>';
                    sOut += '<td>' + value.nCtaBancariaNumCuenta + '</td>';
                    if (value.cCtaBancariaEstado == 'A') {
                        sOut += '<td><input type="checkboxtoggle" id="' + value.nCtaBancariaCodigo +'" checked data-toggle="toggle"  data-on="&#10003;" data-onstyle="success" class="btnDesactivar"></td>';
                        //sOut += '<td><div class="custom-control custom-switch"><input type="checkbox" checked class="custom-control-input" id="' + value.nCtaBancariaCodigo +'" onchange="Desactivar(' + value.nCtaBancariaCodigo + ')"><label class="custom-control-label" for="' + value.nCtaBancariaCodigo  +'"></label></div></td>';
                    }else{
                        //sOut += '<td><div class="custom-control custom-switch"><input type="checkbox" class="custom-control-input" id="' + value.nCtaBancariaCodigo +'" onchange="Activar(' + value.nCtaBancariaCodigo + ')"><label class="custom-control-label" for="' + value.nCtaBancariaCodigo  +'"></label></div></td>';
                        sOut += '<td><input type="checkboxtoggle" id="' + value.nCtaBancariaCodigo +'" data-toggle="toggle"  data-off="✕" data-offstyle="danger" class="btnActivar"></td>';
                       
                    }
                    sOut += '</tr>';
                });
            }
            $("#tbCtasBancarias").html(sOut);

            if (foo != null && $.isArray(foo)) {
                $.each(foo, function (index, value) {
                    $('#' + value.nCtaBancariaCodigo).bootstrapToggle();
                });
            }
            
        },
        error: function (result) {

        }
    });
}

let activar = function(nCtaBancariaCodigo) { 
    //alert(objCtasBancarias.attr('id'));
    //var l_nCtaBancariaCodigo = objCtasBancarias.attr('id'); 
    swal({
        title: 'Estas seguro de activar esta cuenta bancaria?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Activar!',
        cancelButtonText: 'No, cancelar!'
    }).then((result) => {
        if (result.value) {
            
            var l_nCtaBancariaCodigo = nCtaBancariaCodigo;

            let data = {
                nCtaBancariaCodigo: l_nCtaBancariaCodigo
            }

            $.ajax({
                type: "POST",
                url: "DataJson/SeleccionarCuentasJson.aspx",
                data: { Funcion: "actualizarCuentaBancaria", data: JSON.stringify(data) },
                dataType: "json",
                cache: false,
                success: function (response) {
                    console.log({ response });

                    var foo = response;
                    var sOut = '';

                    if (foo != null && $.isArray(foo)) {
                        $.each(foo, function (index, value) {
                            swal('La cuenta bancaria se activó correctamente');
                        });
                    }
                    setTimeout(listarCuentasBancarias,2000);
                },
                error: function (result) {

                }
            });
        } else if (result.dismiss === swal.DismissReason.cancel){listarCuentasBancarias()}
    })
}

let desactivar = function(nCtaBancariaCodigo) { 
    //alert(objCtasBancarias.attr('id'));
    //var l_nCtaBancariaCodigo = objCtasBancarias.attr('id'); 
    swal({
        title: 'Estas seguro de desactivar esta cuenta bancaria?',
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, Desactivar!',
        cancelButtonText: 'No, cancelar!'
    }).then((result) => {
        if (result.value) {
            
            var l_nCtaBancariaCodigo = nCtaBancariaCodigo;

            let data = {
                nCtaBancariaCodigo: l_nCtaBancariaCodigo
            }

            $.ajax({
                type: "POST",
                url: "DataJson/SeleccionarCuentasJson.aspx",
                data: { Funcion: "actualizarCuentaBancaria", data: JSON.stringify(data) },
                dataType: "json",
                cache: false,
                success: function (response) {
                    //console.log({ response });
                    var foo = response;
                    if (foo != null && $.isArray(foo)) {
                        $.each(foo, function (index, value) {
                            swal('La cuenta bancaria se desactivó correctamente');
                        });
                    }
                    setTimeout(listarCuentasBancarias,2000);
                },
                error: function (result) {

                }
            });
        } else if (result.dismiss === swal.DismissReason.cancel){listarCuentasBancarias()}
    })
}

$(document).on('change', '.btnActivar', function (e) {       
    let l_nCtaBancariaCodigo = $(this).attr("id");
    activar(l_nCtaBancariaCodigo);
});

$(document).on('change', '.btnDesactivar', function (e) {       
    let l_nCtaBancariaCodigo = $(this).attr("id");
    desactivar(l_nCtaBancariaCodigo);
});

