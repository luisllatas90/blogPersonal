var detalles = [];
var Op = 0;
var Index = 0;
$(document).ready(function() {
   // fnNumeros('txtIntercep');
   // fnNumeros('txtCoeficiente');
    // $("#op").val(1);
    fnCicloAcademico(2);
    fnListarVarible();
    $("#btnListar").on("click", function() {
    ListarFormula();
    });
    
});

function confirmGuardar() {
    if (fnValidar()) {
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
                  function() {
                     // $('#mdRegistro').modal('hide');
                      fnGuardar();
                  });
    }
}
function fnDeleteVariable(codigo) {
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
                  function() {
                      // $('#mdRegistro').modal('hide');
    fnAnularItemFormula(codigo);
                  });
              }

  function fnAnularItemFormula(codigo) {
      $.ajax({
          type: "POST",
          dataType: "html",
          url: "../../DataJson/PredictorDesercion/FormulasDesercion.aspx",
          data: { "Funcion": 'AnulaFormula', 'codigo_fml': codigo },
          dataType: "json",
          success: function(data) {
              var flag = false;
              console.log(data);
              jQuery.each(data, function(i, val) {
                  console.log(data);
                  if (val.Status == "OK") {
                      fnMensaje("success", val.Message)                                           
                      /// ListarVarible();
                  }
                  if (val.Status == "ERROR") {
                      fnMensaje("warning", val.Message)
                  }
              });


          },
          error: function(result) {
              console.log(result);
              fnMensaje("warning", result)

          }
      });
  }
function fnConfirmaEliminarItem( cod, op) {
if  (op>0)
{
    swal(
            {
                title: "¿Desea continuar?",
                text: "El item seleccionado se encuentra registrado ,¿Esta seguro de Inactivar el registro?.",
                type: "info",
                showCancelButton: true,
                confirmButtonColor: '#6fd64b',
                confirmButtonClass: 'btn-success',
                confirmButtonText: 'Ok',
                closeOnConfirm: true,
                animation: "slide-from-top"
                //closeOnCancel: false
            },
          function() {
              // $('#mdRegistro').modal('hide');
            fnDesactivarItem(op, cod);
              // fnDeleteItem(cod);
          }
    );
}
     
     if(op==0){
         swal({
        title: "¿Desea continuar?",
        text: "¿Esta seguro de quitar el registro?",
        type: "info",
        showCancelButton: true,
        confirmButtonColor: '#6fd64b',
        confirmButtonClass: 'btn-success',
        confirmButtonText: 'Ok',
        closeOnConfirm: true,
        animation: "slide-from-top"
        //closeOnCancel: false
    },
                  function() {
                      // $('#mdRegistro').modal('hide');
                      fnDeleteItem(cod);
                  });
     }
}

function fnGuardar() {
    //var formData = $('#frmRegistro').serializeArray();
    var x = $("#txtFecha").val().split('/');
   
  var  fecha = x[1] + '/' + x[0] + '/' + x[2];
    
    var variables = JSON.stringify(detalles);
    var params = {
        'Funcion':'Grabar',
        'op':$("#op").val(),
        "txtCodigoFml":$("#txtCodigoFml").val(),
        "cboCicloAcademicoR":$("#cboCicloAcademicoR").val(),
        "txtNombreFormula":$("#txtNombreFormula").val(),
        "txtIntercep":$("#txtIntercep").val(),
        "txtFecha": fecha,
        "chkestado":$("#chkestado").val(),
        "cboVariable":$("#cboVariable").val(),
        "txtCoeficiente": $("#optxtCoeficiente").val(),
        "Session":$("#txtSession").val(),
        "variables": variables
};
$.ajax({
    type: "POST",
    dataType: "html",
    url: "../../DataJson/PredictorDesercion/FormulasDesercion.aspx",
    data: params,
    dataType: "json",
    success: function(data) {
        var flag = false;
        console.log(data);
        jQuery.each(data, function(i, val) {
            console.log(data);
            if (val.Status == "OK") {
                fnMensaje("success", val.Message)
                $('#mdRegistro').modal('hide');
                LimpiarVar();
                var detalles = [];
               /// ListarVarible();
            }
            if (val.Status == "ERROR") {
                fnMensaje("warning", val.Message)
            }


        });


    },
    error: function(result) {
        console.log(result);
        fnMensaje("warning", result)

    }
});
}

function fnValidar() {
    var res = true;
    if ($("#cboCicloAcademicoR").val() == '') {
        fnMensaje("warning", 'Seleccione ciclo Acádemico')
        res = false;
    }
    if ($("#txtNombreFormula").val() == '') {
        fnMensaje("warning", 'Ingrese un nombre a la fórmula')
        res = false;
    }
    if ($("#txtIntercep").val() == '') {
        fnMensaje("warning", 'Ingrese el valor Intercep')
        res = false;
    }
    if ($("#txtFecha").val() == '') {
        fnMensaje("warning", 'Ingrese la Fecha')
        res = false;
    }
    return res;
}
function LimpiarVar() {
    $('#cboCicloAcademicoR').val('');
    $('#txtNombreFormula').val('');
    $('#txtIntercep').val('0');
    $('#txtFecha').val('');
    $('#txtCoeficiente').val(0);
    $('#txtCodigoFml').val(0);
    $("#op").val(1);
    $('#TbodyVariables').html("");
}


function fnListarVarible() {
    var str = '';
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/Variables.aspx",
        data: { "Funcion": "Listar" },
        dataType: "json",
        cache: false,
        success: function(data) {
            $("#cboVariable").html("");
           
            jQuery.each(data, function(i, val) {
            str += '<option value="' + val.CodigoVar + '">' + val.NombreDescVar + "(<b>" + val.NombreVar + "</b>)" + '</option>';
            });
            $("#cboVariable").html(str);
           
            
        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });
}
function fnCicloAcademico(root) { // k:codigo_cac 
  //  try {
        var n = parseInt(root);
        var sroot = "";
        var parametros = { "Funcion": "CicloAcademico" };
        var i = 0;
        var arr;
        for (i = 0; i < n; i++) {
            sroot += "../";
        }
        var str = "";
        sroot = sroot + 'DataJson/PredictorDesercion/FormulasDesercion.aspx',
        $.ajax({
            type: 'POST',
            url: sroot,
            data: parametros,
           // async: false,
            cache: false,
            dataType: "json",
            success: function(data) {
                console.log(data);
                $("#cboCicloAcademico").html("");
                $("#cboCicloAcademicoR").html("");
                jQuery.each(data, function(i, val) {
                    str += '<option value="' + val.Value + '">' + val.Label + '</option>';
                });
                $("#cboCicloAcademico").html(str);
                $("#cboCicloAcademicoR").html(str);

            },
            error: function(result) {
                arr = null;

            }
        });
     
///        return arr;
   // }
   /// catch (err) {
   ////     alert(err.message);
   // }
}
function fnAgregarVariable() {
    var value;
    var tb = '';
    var tb2 = '';
    var rowCount = $('#TbodyVariables tr').length;
    var repite = false;
    if (fnValidarItem() == true) {
      //alert(1);
        for (i = 0; i < detalles.length; i++) {
            if (detalles[i].variableId == $('#cboVariable').val()) {
                repite = true;
            }
        }
        if (repite == false) {
            $('#TbodyVariables tr').each(function() {
                value = $(this).find("td:first").html();

            });
            if (!($.isNumeric(value))) { rowCount = 0 }

            var row = (rowCount + 1);
            var variableId = $('#cboVariable').val();
            var variableText = $('#cboVariable option:selected').text();
            var coeficiente = $('#txtCoeficiente').val();
            detalles.push(
            {
                variableId: variableId,
                nombre: variableText,
                coeficiente: coeficiente,
                CodigoVarFml:0
            });
           /// console.log(detalles);
            $('#txtCoeficiente').val('');
            console.log(detalles);
            for (i = 0; i < detalles.length; i++) {
                tb += '<tr>';
                // tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                tb += '<td>' + detalles[i].variableId + '</td>';
                tb += '<td>' + detalles[i].nombre + '</td>';
                tb += '<td style="text-align:center">' + detalles[i].coeficiente + '</td>';
                //tb += '<td style="text-align:center"></td>';
                tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnConfirmaEliminarItem(' + (i + 1) + ','+detalles[i].CodigoVarFml+')" ><i class="ion-android-delete"></i></button></td>';
                tb += '</tr>';
            }
            $('#TbodyVariables').html(tb);

        } else {
            fnMensaje("warning", 'Ya existe la variable en la lista')
        }
    }
}
function fnDeleteItem(cod) {
    var tb = '';
  //  alert(cod);
    console.log('==' + detalles[cod - 1].CodigoVarFml);
  //  if (detalles[cod - 1].CodigoVarFml == 0) {
        detalles.splice(cod - 1, 1);
        for (i = 0; i < detalles.length; i++) {
            tb += '<tr>';
            // tb += '<td style="text-align:center">' + (i + 1) + '</td>';
            tb += '<td>' + detalles[i].variableId + '</td>';
            tb += '<td>' + detalles[i].nombre + '</td>';
            tb += '<td style="text-align:center">' + detalles[i].coeficiente + '</td>';
            //tb += '<td style="text-align:center"></td>';
            tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnConfirmaEliminarItem(' + (i + 1) + ','+detalles[i].CodigoVarFml+')" ><i class="ion-android-delete"></i></button></td>';
            tb += '</tr>';
        }
        $('#TbodyVariables').html(tb);
   // }  
}
function fnValidarItem() {
    if ($("#cboVariable").val() == '') {
        fnMensaje("warning", 'Seleccione un tipo de Variable')
        return false
    }
    if ($("#txtCoeficiente").val() == '') {
        fnMensaje("warning", 'Ingrese el coeficiente')
        return false
    }

     

    return true
}
function fnNumeros(input) {
    $("#" + input).keydown(function(e) {
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        (e.keyCode == 65 && e.ctrlKey === true) ||
        (e.keyCode >= 35 && e.keyCode <= 39 && e.keyCode == 45)) {
            return;
        }

        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });
}

function ListarFormula() {

    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/FormulasDesercion.aspx",
        data: { "Funcion": "Listar", 'CodigoCac': $("#cboCicloAcademico").val(), "op": 1 },
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

                if (val.Estado == 'Inactivo') {
                    sOut += '<tr style="    background-color: #fb5d5d;color:#fff">';
                } else {
                     sOut += '<tr>';
                }
              //  sOut += '<tr>';
                sOut += '<td>' + row + 1 + ' ' + '</td>';
                sOut += '<td>' + val.NombreFml + ' ' + '</td>';
                sOut += '<td>' + val.VarIndependienteFml + ' ' + '</td>';
                sOut += '<td>' + val.CicloAcademico + ' ' + '</td>';
                sOut += '<td>' + val.Fecha + ' ' + '</td>';
                sOut += '<td>' + val.Estado + ' ' + '</td>';
                sOut += '<td style="text-align:center">';
                sOut += '<button type="button" id="btnDI" name="btnDI" class="btn  btn-success btn-xs" title="Editar" onclick="fnEditarFormula(' + val.CodigoFml + ',' + row + ')" ><i class=" ion-android-create"></i></button>';
                sOut += '<button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnDeleteVariable(' + val.CodigoFml + ')" ><i class="ion-android-delete"></i></button>';
                sOut += '</td>';
                sOut += '</tr>';


                row++;
            });

            $("#tbFormula").html(sOut);
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
var Codigo_fml = 0;
function fnEditarFormula(codigo) {
    detalles = [];
    Codigo_fml = codigo;
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/FormulasDesercion.aspx",
        data: { "Funcion": "Editar", 'CodigoFml': codigo },
        dataType: "json",
        cache: false,
        success: function(data) {

            //  alert(data.length);
            var sOut = '';
            // console.log(data);
            $("#TbodyVariables").html("");
            edit = data;
            jQuery.each(data, function(i, val) {
                $("#txtCodigoFml").val(val.CodigoFml);
                $("#op").val(2);
                $("#txtNombreFormula").val(val.NombreFml);
                $("#txtIntercep").val(val.VarIndependienteFml);                
                $("#txtFecha").val(val.FechaFml);
                $("#cboCicloAcademicoR").val($("#cboCicloAcademico").val());
                detalles.push(
                    {
                        variableId: val.CodigoVar,
                        nombre: val.NombreDescVar,
                        coeficiente: val.Coeficiente,
                        CodigoVarFml: val.CodigoVarFml
                    });

                var docente = '';
                sOut += '<tr>';
                // tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                sOut += '<td>' + val.CodigoVar + '</td>';
                sOut += '<td>' + val.NombreDescVar + '</td>';
                sOut += '<td style="text-align:center">' + val.Coeficiente + '</td>';
                //tb += '<td style="text-align:center"></td>';
                sOut += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnConfirmaEliminarItem(' + (i + 1) + ',' + val.CodigoVarFml + ')" ><i class="ion-android-delete"></i></button></td>';
                sOut += '</tr>';

            });

            $("#TbodyVariables").html(sOut);
            ///  oTable.fnOpen(nTr, sOut, 'details');

        },
        error: function(result) {
            sOut = '';
            console.log(result);
            // oTable.fnOpen(nTr, sOut, 'details');
            // location.reload();
        }
    });

    $('#mdRegistro').css("z-index", "0");
    $("#mdRegistro").modal("show");
}

function fnDesactivarItem(codigo,index) {
    //var formData = $('#frmRegistro').serializeArray();
   
    var params = {
        'Funcion': 'Desactivar',                 
        "Session": $("#txtSession").val(),
        "CodigoVarFml": codigo
    };
    $.ajax({
        type: "POST",
        dataType: "html",
        url: "../../DataJson/PredictorDesercion/FormulasDesercion.aspx",
        data: params,
        dataType: "json",
        success: function(data) {
            var flag = false;
            console.log(data);
            jQuery.each(data, function(i, val) {
                console.log(data);
                if (val.Status == "OK") {
                    fnMensaje("success", val.Message)
                    fnDeleteItem(index);
                   // fnEditarFormula(Codigo_fml);
                }
                if (val.Status == "ERROR") {
                    fnMensaje("warning", val.Message)
                }
            });
        },
        error: function(result) {
            console.log(result);
            fnMensaje("warning", result)

        }
    });
}