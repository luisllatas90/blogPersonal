var edit;
$(document).ready(function() {
    $("#op").val(1);
    
    ListarVarible();
});
function confirmGuardar(){
    if (fnValidar()){
         swal({
		            title: "¿Desea continuar?",
		            text: "Esta seguro de realizar este proceso.",
		            type: "info",
		            showCancelButton: true,
		            confirmButtonColor: '#6fd64b',
		            confirmButtonClass: 'btn-success',
		            confirmButtonText: 'Ok',
		            closeOnConfirm: true,
		            animation: "slide-from-top"
                      //closeOnCancel: false
                  },
                  function(){
                   $('#mdRegistro').modal('hide');
      	                fnGuardar();
                  });
     }
}


function fnGuardar() {
    var formData = $('#frmRegistro').serializeArray();



    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/Variables.aspx",
        data: formData,
        dataType: "json",
        success: function(data) {
            var flag = false;
            console.log(data);
            jQuery.each(data, function(i, val) {

            if (val.Status == "OK") {
                fnMensaje("success", val.Message)
                    LimpiarVar();
                    ListarVarible();
                }
                if (val.Status == "ERROR") {
                }


            });


        },
        error: function(result) {
                fnMensaje("warning", result)

        }
    });
}

function fnValidar() {
    var res =true;
    if ($("#cboOrigen").val() == '') {
        fnMensaje("warning", 'Seleccione El origen de la variable')
        res= false;
    }
    if ($("#cboSigno").val() == '') {
        fnMensaje("warning", 'Seleccione a el signo de la variable')
        res= false;
    }
    if ($("#txtnombre").val() == '') {
        fnMensaje("warning", 'Ingrese Nombre de la variable')
        res= false;
    }
    if ($("#txtnombreDesc").val() == '') {
        fnMensaje("warning", 'Ingrese Nombre descriptivo de la variable')
        res= false;
    }     
    return res ;
}
function LimpiarVar() {
    $('#cboOrigen').val('');
    $('#txtnombre').val('');
    $('#txtDescripcion').val('');
    $('#txtnombreDesc').val('');
    $('#txtCodigoVar').val(0);
    $("#op").val(1);
}
function ListarVarible() {

    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/Variables.aspx",
        data: { "Funcion": "Listar" },
        dataType: "json",
        cache: false,
        success: function(data) {
            var row = 0;
            //  alert(data.length);
            var sOut = '';
           // console.log(data);
            $("#tbListVariable").html("");
            edit = data;
            jQuery.each(data, function(i, val) {
                var docente = '';


                sOut += '<tr>';
                sOut += '<td>' + row + 1 + ' ' + '</td>';
                sOut += '<td>' + val.NombreDescVar + ' ' + '</td>';
                sOut += '<td>' + val.NombreVar + ' ' + '</td>';
                sOut += '<td>' + val.DescripcionVar + ' ' + '</td>';
                sOut += '<td>' + val.OrigenVar + ' ' + '</td>';
                sOut += '<td>' + val.SignoVar + '</td>';
                sOut += '<td>' + val.EstadoText + ' ' + '</td>';
                sOut += '<td style="text-align:center">';
                sOut += '<button type="button" id="btnDI" name="btnDI" class="btn  btn-success btn-xs" title="Eliminar" onclick="fnEditarVarible(' + val.CodigoVar + ',' + row + ')" ><i class=" ion-android-create"></i></button>';
              //  sOut += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnDeleteVariable(' + val.CodigoVar + ')" ><i class="ion-android-delete"></i></button>';
                sOut += '</td>';
                sOut += '</tr>';


                row++;
            });

            $("#tbListVariable").html(sOut);
            ///  oTable.fnOpen(nTr, sOut, 'details');

        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}
function fnEditarVarible(codigoVar, row) {
    $('#mdRegistro').css("z-index","0");
    $("#mdRegistro").modal("show");
    $("#txtnombre").val(edit[row].NombreVar);
    $("#txtnombreDesc").val(edit[row].NombreDescVar);
    $("#txtDescripcion").val(edit[row].DescripcionVar);
    $("#cboSigno").val(edit[row].SignoVar);
    $("#cboOrigen").val(edit[row].OrigenVar);
 
    $("#txtFecha").val(edit[row].FechaVar);
    $("#txtCodigoVar").val(edit[row].CodigoVar);
    $("#op").val(2);
}
function fnDeleteVariable(codigoVar) {
}

function fnConfirmar(){

          $('#mdRegistro').css("z-index","0");
        swal({
		title: "Confirmación",
		text: "¿Desea guardar esta Variable?",
		type: "success",
		showCancelButton: true,
		confirmButtonClass: 'btn-success',
		confirmButtonText: 'OK',
		closeOnConfirm: true,
          //closeOnCancel: false
      },
      function(){
        $('#mdRegistro').modal('hide');
        fnGuardarDetalle();
      	//swal("Thanks!", "We are glad you clicked welcome!", "success");
      });
}

function fnNumeros(input){
$("#"+input).keydown(function(e) {
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 39)) {
        return;
    }

        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}