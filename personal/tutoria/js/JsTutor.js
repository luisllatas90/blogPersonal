var lstPer;
var codper;
var titulo;
var codperM;
var tituloM;
$(document).ready(function() {
    //fnLoading(true);
    var dt = fnCreateDataTableBasic('tConvocatoria', 1, 'asc');
    var dtP = fnCreateDataTableBasic('tPorAsignar', 3, 'asc', 20);
    var dtA = fnCreateDataTableBasic('tAsignados', 2, 'asc', 20);
    var dtD = fnCreateDataTableBasic('tDetalle', 0, 'asc');

    ope = fnOperacion(1);
    //console.log(ope);
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnCicloAcad('#cboCicloAcad')
        $('#tabTutor').click(fnBuscarTutor);
        $('#cboCategoria').change(function() {
            //fnCantAlumnos();
            $("#cboRiesgo").prop('selectedIndex', 0);
            fnEscuela();
        });

        $('#lblCantAlumnos').click(function() {
            $('#lblchkAlumnos').css("display", "block");
            $('#lblchkAlumnos').css("visibility", "visible");
        });

        $('#cboEscuela').change(function() {
            $("#cboRiesgo").prop('selectedIndex', 0);
            fnListarCiclos();
        });
        $('#cboIng').change(function() {
            ////console.log('here');
            $("#cboRiesgo").prop('selectedIndex', 0);
            fnCantAlumnos();
        });
        $('#cboRiesgo').change(function() {
            ////console.log('here');
            fnCantAlumnos();
        });
        $("#cboCicloAcad").change(function() {
            var l = document.getElementById('tabTutor');
            l.click();
            fnEscuela();
            fnListarCiclos();
            fNRiesgoSeparacion();
            //fnBuscarAlumnos(); 
        });
        fnAutoCPersonal();
        //        fnPer();
        fnCategoria('#cboCategoria');
        //fnListarCiclos();
        //        $('#tbTutorado1').on( 'click', 'tr', function () {
        //            $(this).toggleClass('selected');
        //        });
        $("#chkFiltros").change(function() {
            if (this.checked) {
                //console.log('check');
                //Do stuff
                $('#divFiltros').css("visibility", "visible");
                $('#divFiltros').css("display", "block");
                $('#divAlumnos').css("visibility", "hidden");
                $('#divAlumnos').css("display", "none");
                $('#lblchkAlumnos').css("display", "none");
                $('#lblchkAlumnos').css("visibility", "hidden");
                fnCantAlumnos();
            }

        });
        $("#chkAlumnos").change(function() {
            if (this.checked) {
                ////console.log('check');
                //Do stuff
                $('#divFiltros').css("display", "none");
                $('#divFiltros').css("visibility", "hidden");
                $('#divAlumnos').css("visibility", "visible");
                $('#divAlumnos').css("display", "block");
            }

        });
    } else {
        window.location.href = rpta
    }
    //fnLoading(false);

    $('#mdRegistro').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget) // Botón que activó el modal
        //        alert('--')
        if (button.attr("id") == "btnAgregar") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
            Limpiar();
        } else if (button.attr("id") == "btnE") {
            $('#hdcod').remove();
            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            fnCicloAcad('#cboCicloAcadM')
            Edit()
        }
    })
    $('#mdDetalle').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget)
        ////console.log(button.attr("id"));
        if (button.attr("id") == "aDetalle") {
            fnSesiones(button.attr("hdc"));
        }
    })
    //    $('#mdEliminar').on('show.bs.modal', function(event) {
    //        var button = $(event.relatedTarget) // Botón que activó el modal
    //        $('#hdcod').remove();
    //        $('#frmEliminar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
    //    })
})
function fNRiesgoSeparacion(){
    var arr = fRiesgoSeparacion(1,"RS");
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $("#cboRiesgo").html(str);
}
function fnCargaPersonal() {
    var f = $('#hdTF').val();
    return fnListarPersonal(1, 0, 0);
}
function fnTipoEst(op) {

    var arr = fnTipoEstudio(1, "TO", op);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    if (op == 1) {
        $('#cboTipoEstudio').html(str);
    }
    if (op == 2) {
        $('#cboTipoEstudioR').html(str);
    }
}

function fnCicloAcad(cboId) {

    var arr = fnCicloAcademico(1, "LCV", "");
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    
    $(cboId).html(str);
}
function fnCategoria(cboId) {

    var lstCat = fnListarCategoria(1, 0, 0);
    var arr = JSON.parse(lstCat);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $(cboId).html(str);    
    
    var f = document.getElementById('cboCategoria');
    f.selectedIndex =0;
}
function fnListarCiclos() {

    var arr = fnSemestre(1, $('#cboCicloAcad').val(), $('#cboCategoria').val(),$('#cboEscuela').val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 1; i < n; i++) {        
            str += '<option value="' + arr[i].cCod + '">' + arr[i].cNombre + '</option>';        
    }

    $('#cboIng').html(str);
    document.getElementById("lblCantAlumnos").textContent = arr[0].cCant;
    //$("#cboIng").trigger("change");
}
function fnPer() {
    var jsonString = fnListarPersonal(1, 0, 0);
    var arr = JSON.parse(jsonString);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboPersonal').html(str);
}

function fnBuscarTutor() {
    ////console.log('aqui');
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
        $('#tutores').css("visibility", "hidden");
        $('#tutorados').css("visibility", "hidden");
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {

            //if (sw) { fnLoading(true); }
            // fnLoading(true)
            $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
            var form = $("#frmCiclo").serializeArray();
            ////console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/Tutor.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                $("form#frmCiclo input[id=action]").remove();
                    $('#tutores').css("visibility", "visible");
                    $('#tutorados').css("visibility", "visible");
                    ////console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        //if (i == 0 && !data[i].sw) {
                        // fnMensaje('warning', data[i].msje);
                        // break;
                        //}

                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td>' + data[i].cNombre + '</td>';
                        tb += '<td align="center">';
                        if (data[i].cEstado == "1") {
                            tb += '<input type="checkbox" checked="checked" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                        } else {
                            tb += '<input type="checkbox" class="editor-active" id="' + i + '" style="display:block;" disabled="disabled" /></td>';
                        }

                        // tb += '<td style="text-align:center">' + data[i].cFecini + '</td>';
                        // tb += '<td style="text-align:center">' + data[i].cFecFin + '</td>';
                        tb += '<td style="text-align:center"> <a href="#" id="aDetalle" hdc="' + data[i].cCod + '" data-toggle="modal" data-target="#mdDetalle"><label class="control-label" style="color: #337ab7;font-size:13px;cursor:pointer;">' + data[i].cSesion + '</label></a></td>';
                        tb += '<td style="text-align:center">' + data[i].cTutorados + '</td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" data-toggle="modal" data-target="#mdRegistro" hdc="' + data[i].cCod + '" title="Editar" ><i class="ion-edit"></i></button>';
                        tb += '<button type="button" id="btnA" name="btnA" class="btn btn-sm btn-green btn-icon-green" onclick="fnAsignar(\'' + data[i].cCod + '\')" title="Asignar tutorados" ><i class="ion-android-people"></i></button>';
                        tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red btn-icon-red" onclick="fnDelete(\'' + data[i].cCod + '\')" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';
                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tConvocatoria');
                    $('#tbConvocatoria').html(tb);
                    fnResetDataTableBasic('tConvocatoria', 1, 'asc');
                    //if (sw) { fnLoading(false); }
                    // fnLoading(false);
                },
                error: function(result) {
                    ////console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
    }

}
function fnAsignar(cod){
    fnCargarDatos(cod);
    fnAsignados();
    LimpiarFiltros();
    document.getElementById("lblCantAlumnos").textContent = "0 alumnos encontrados.";
    //fnCantAlumnos();
}
function LimpiarFiltros() {

    $("#cboCategoria").val('');
    $("#cboEscuela").val('');
    $("#cboIng").val('');
}
function fnCargarDatos(cod){
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            // fnLoading(true)

            $('#liTutorados').remove();
            $('#tabs').append('<li role="presentation" class="" id="liTutorados"><a href="#tutorados" aria-controls="profile" id="tabTutorado" role="tab" data-toggle="tab" aria-expanded="false">Tutorados</a></li>');
            var l = document.getElementById('tabTutorado');
            l.click();

//            $('#tutores').css("visibility", "hidden");
//            $('#tutores').css("display", "none");
//            $('#tutorados').css("visibility", "visible");
//            $('#tutorados').css("display", "block");
            $('#hdcod').remove();
            $('#action').remove();
            $('#frmTutor').append('<input type="hidden" id="action" name="action" value="'+ ope.lst + '" />');            
            $('#frmTutor').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
            var form = $("#frmTutor").serializeArray();
            ////console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/Tutor.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    $("form#frmTutor input[id=action]").remove();
                    ////console.log(data);
                    
                    document.getElementById("lblTutor").textContent= "Tutor: " + data[0].cNombre;
                    //if (sw) { fnLoading(false); }
                    // fnLoading(false);
                },
                error: function(result) {
                    ////console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
      }
}
function fnAsignados(){
if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)            
            $("form#frmTutor input[id=action]").remove();
            $("form#frmTutor input[id=cboCicloAcad]").remove();
            $("form#frmTutor input[id=tipo]").remove();
            $('#frmTutor').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
            $('#frmTutor').append('<input type="hidden" id="tipo" name="tipo" value="LT" />');              
            $('#frmTutor').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');         
            //$('#frmTutor').append('<input type="hidden" id="hdcod" name="hdcod" value="' + cod + '" />');
            var form = $("#frmTutor").serializeArray();
            //console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/TutorAlumno.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    $("form#frmTutor input[id=action]").remove();
                    $("form#frmTutor input[id=cboCicloAcad]").remove();
                    $("form#frmTutor input[id=tipo]").remove();
                   // console.log(data);

                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center"><button type="button" id="btnDe" name="btnDe" class="btn btn-sm btn-green btn-icon-green" onclick="fnDerecha(\'' + data[i].cTA + '\')" title="Desasignar" ><i class="ti-angle-double-left"></i></button></td>';
                        tb += '<td style="text-align:center">' + data[i].cCodU + "" + '</td>';
                        tb += '<td>' + data[i].cAlumno + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cAbrev + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cCat + '</td>';
                        tb += '<td style="text-align:center"><input type="checkbox" class="form-control" id="chkatender[' + i + ']" name="chkatender[' + i + ']" onclick="fnConfirmarAtencion(\'' + data[i].cAlumno + '\',\'' + data[i].cTA + '\',' + i + ')" ';
                        if (data[i].cAtendido)
                            tb += 'checked="checked" ';
                        tb += '></td>';
                        tb += '</tr>';
                    }
                    
                    fnDestroyDataTableDetalle('tAsignados');
                    $('#tbTutorado2').html(tb);
                    fnResetDataTableBasic('tAsignados', 3, 'asc', 20);
                    //fnAsignados();
                    //if (sw) { fnLoading(false); }
                    //                    fnLoading(false);
                },
                error: function(result) {
                    //                    //console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
      }
  }

  function fnConfirmarAtencion(alu, cod, j) {
      fnBloquear('PanelLista', true);
      var est = 0;
      if ($('#chkatender\\[' + j + '\\]').is(':checked')) {
          est = 1;
      }
      var msje;
      
      if (est == 1) {
          msje = '¿Desea atender al alumno: ' + alu + ' ?';
      } else {
          msje = '¿Desea quitar atención al alumno: ' + alu + ' ?';
      }
      
      var aParam = {
          alu: alu,
          cod: cod,
          i:j,
          mensaje: msje
      }
      var r = fnMensajeConfirmarEliminarAtencion('top', aParam.mensaje, 'fnAtender', 'fnCancelarAtencion', aParam.cod, aParam.i);

  }


  function fnBloquear(Panel,sw) {
      if (sw) {
          $('#' + Panel).css("display", "none");
          
      } else {
        $('#' + Panel).css("display", "block");
            
      }
    
  } 
  
  function fnCancelarAtencion(j) {
      var est = 0;
      if ($('#chkatender\\[' + j + '\\]').is(':checked')) {
          est = 1;
      }

      if (est == 0) {
        $('#chkatender\\[' + j + '\\]').prop('checked', true);
      } else {       
        $('#chkatender\\[' + j + '\\]').prop('checked', false);
      }

      fnBloquear('PanelLista',false);
  }

  function fnAtender(cod, i) {

    var est = 0;
    if($('#chkatender\\[' + i + '\\]').is(':checked')){
        est=1;
    }

      try {
          $.ajax({
              type: "POST",
              url: "../DataJson/tutoria/TutorAlumno.aspx",
              data: {'action':ope.atT,'c':cod,'est':est },
              dataType: "json",
              cache: false,
              success: function(data) {
                  console.log(data);

              },
              error: function(result) {
                  console.log(result)
              }
          });

      } catch (e) {
        console.log(e.message);
      }
      fnBloquear('PanelLista', false);
  
  }
function fnBuscarAlumnos() {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)    ;
            $("form#frmPorAsignar input[id=action]").remove();
            $("form#frmPorAsignar input[id=cboCicloAcad]").remove();
            $("form#frmPorAsignar input[id=tipo]").remove();
            $('#frmPorAsignar').append('<input type="hidden" id="action" name="action" value="POB" />');   
            $('#frmPorAsignar').append('<input type="hidden" id="tipo" name="tipo" value="1" />');                 
            $('#frmPorAsignar').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />'); 
            var form = $("#frmPorAsignar").serializeArray();
            //console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/Operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    $("form#frmPorAsignar input[id=action]").remove();
                    $("form#frmPorAsignar input[id=cboCicloAcad]").remove();
                    $("form#frmPorAsignar input[id=tipo]").remove();
                    ////console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        //tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td style="text-align:center">' + data[i].codU + '</td>';
                        tb += '<td>' + data[i].nombre + '</td>';
                        tb += '<td style="text-align:center">' + data[i].carrera + '</td>';
                        tb += '<td style="text-align:center">' + data[i].categoria + '</td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnIz" name="btnIz" class="btn btn-sm btn-green btn-icon-green" onclick="fnIzquierda(\'' + data[i].alu + '\',\'' + data[i].categoria + '\')" title="Asignar" ><i class="ti-angle-double-right"></i></button></td>';

                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tPorAsignar');
                    $('#tbTutorado1').html(tb);
                    fnResetDataTableBasic('tPorAsignar', 3, 'asc', 20);
                    $("#chkAlumnos").prop('checked', true);
                    $("#chkAlumnos").trigger("change");
                    //if (sw) { fnLoading(false); }
                    //                    fnLoading(false);
                },
                error: function(result) {
                    //                    //console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
    }

}
function fnCantAlumnos() {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)    ;  
            $("form#frmPorAsignar input[id=action]").remove();
            $('#frmPorAsignar').append('<input type="hidden" id="action" name="action" value="POB" />');     
            $('#frmPorAsignar').append('<input type="hidden" id="tipo" name="tipo" value="2" />');           
            $('#frmPorAsignar').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />'); 
            var form = $("#frmPorAsignar").serializeArray();
            //console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/Operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    $("form#frmPorAsignar input[id=action]").remove();
                    $("form#frmPorAsignar input[id=cboCicloAcad]").remove();
                    $("form#frmPorAsignar input[id=tipo]").remove();
                    //console.log(data);

                    if (data[0].msje !== undefined) {
                        document.getElementById("lblCantAlumnos").textContent = data[0].msje;
                    }
                    
                    //if (sw) { fnLoading(false); }
                    //fnLoading(false);
                },
                error: function(result) {
                    //console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
    }

}
function fnEscuela() {
    ////console.log('aqui');
    
        rpta = fnvalidaSession()
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)

            $("form#frmCiclo input[id=action]").remove();
            $("form#frmCiclo input[id=cboCategoria]").remove();
            $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.esc + '" />');
            $('#frmCiclo').append('<input type="hidden" id="cboCategoria" name="cboCategoria" value="' + $('#cboCategoria').val() + '" />');
            var form = $("#frmCiclo").serializeArray();

            ////console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/Tutor.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                $("form#frmCiclo input[id=action]").remove();
                $("form#frmCiclo input[id=cboCategoria]").remove();
                    
                    var str = "";
                    var i = 0;
                    var filas = data.length;                    
                    str += '<option value="" selected>-- Seleccione -- </option>';
                    for (i = 1; i < filas; i++) {
                        //                if (i == 0 && !data[i].sw) {
                        //                    fnMensaje('warning', data[i].msje);
                        //                    break;
                        //                }
                        str += '<option value="' + data[i].cCod + '">' + data[i].cNombre + '</option>';
                        
                            
                    }
                    
                    $('#cboEscuela').html(str);

                    document.getElementById("lblCantAlumnos").textContent = data[0].cCant;
//                    var f = document.getElementById('cboEscuela');
//                    f.selectedIndex = 0;
//                    $("#cboIng").trigger("change");

//                    var f = document.getElementById('cboIng');
//                    f.selectedIndex =0;
                    //if (sw) { fnLoading(false); }
                    //                    fnLoading(false);
                },
                error: function(result) {
//                    //console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
    

}
function fnAgregar() {
    $('#hdcod').remove();
    $('#frmBuscarTuto').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
    fnGuardar();
}
function fnAsignarTodos(){
    rpta = fnvalidaSession()
    if (rpta == true) {
        
            fnLoading(true)

                
                
                $('#frmPorAsignar').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmPorAsignar').append('<input type="hidden" id="tipo" name="tipo" value="2" />');
                $('#frmPorAsignar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + $("#hdcod").val() + '" />'); 
                $('#frmPorAsignar').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />'); 
          
                var form = $("#frmPorAsignar").serializeArray();
                //console.log( form);
                $("form#frmPorAsignar input[id=action]").remove();
                $("form#frmPorAsignar input[id=tipo]").remove();
                $("form#frmPorAsignar input[id=cboCicloAcad]").remove();
                $("form#frmPorAsignar input[id=hdcod]").remove();
                //            $('#hdcod').remove();
                            ////console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/TutorAlumno.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
//                        //console.log(data);
                        if (data[0].rpta == 1) {
                            //fnMensaje("success", data[0].msje)
                            Limpiar()
//                            fnBuscarAlumnos(false);
                            fnAsignados(false);
                            fnCantAlumnos(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //            //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            
            fnLoading(false)
        
    } else {
        window.location.href = rpta
    }
}

function fnIzquierda(cod,cat){
     rpta = fnvalidaSession()
    if (rpta == true) {
       
            fnLoading(true)
            
                $("form#frmTutor input[id=action]").remove();
                $("form#frmTutor input[id=cod]").remove();
                $("form#frmTutor input[id=cat]").remove();
                $("form#frmTutor input[id=tipo]").remove();
                $('#frmTutor').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmTutor').append('<input type="hidden" id="tipo" name="tipo" value="1" />');
                $('#frmTutor').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
                $('#frmTutor').append('<input type="hidden" id="cat" name="cat" value="' + cat + '" />');
                var form = $("#frmTutor").serializeArray();
                $("form#frmTutor input[id=action]").remove();
                //            $('#hdcod').remove();
                            //console.log(form);
                            $.ajax({
                                type: "POST",
                                url: "../DataJson/tutoria/TutorAlumno.aspx",
                                data: form,
                                dataType: "json",
                                cache: false,
                                success: function(data) {
                                    //                        //console.log(data);
                                    if (data[0].rpta == 1) {
                                        //fnMensaje("success", data[0].msje)
                                        //$("#cboTipoEstudio").val($("#cboTipoEstudioR").val())
                                        Limpiar()
                                        fnBuscarAlumnos(false);
                                        fnAsignados(false);
                                    } else {
                                        fnMensaje("warning", data[0].msje)
                                    }
                                },
                                error: function(result) {
                                    //            //console.log(result)
                                    fnMensaje("warning", result)
                                }
                            });
            
            fnLoading(false)
        
    } else {
        window.location.href = rpta
    }
}
function fnDerecha(cod){
     rpta = fnvalidaSession()
    if (rpta == true) {
        
            fnLoading(true)
            
                $("form#frmTutor input[id=action]").remove();
                $("form#frmTutor input[id=tipo]").remove();
                $("form#frmTutor input[id=cod]").remove();
                $('#frmTutor').append('<input type="hidden" id="action" name="action" value="' + ope.eli+ '" />');
                $('#frmTutor').append('<input type="hidden" id="tipo" name="tipo" value="1" />');
                $('#frmTutor').append('<input type="hidden" id="cod" name="cod" value="' + cod + '" />');
                var form = $("#frmTutor").serializeArray();
                $("form#frmTutor input[id=action]").remove();
                //            $('#hdcod').remove();
                            //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/TutorAlumno.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
//                        //console.log(data);
                        if (data[0].rpta == 1) {
                            //fnMensaje("success", data[0].msje)
                            Limpiar()
                            if ($("#chkAlumnos").is(':checked'))
                                fnBuscarAlumnos(false);
                            else
                                fnCantAlumnos(false);
                            
                            fnAsignados(false);
                            
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //            //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            
            fnLoading(false)
        
    } else {
        window.location.href = rpta
    }
}
function fnGuardar() {
    rpta = fnvalidaSession()
    if (rpta == true) {

        fnLoading(true)
        if ($('#hdcod').val() != 0) {
            if (fnValidar() == true) {
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=cboCicloAcad]").remove();
                $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                $('#frmRegistro').append('<input type="hidden" id="cboCicloAcadM" name="cboCicloAcadM" value="' + $("#cboCicloAcad").val() + '" />');
                $('#chkEstado').prop('disabled', false);
                var form = $("#frmRegistro").serializeArray();
                $('#chkEstado').prop('disabled', true);
                $("form#frmRegistro input[id=action]").remove();
                $("form#frmRegistro input[id=cboCicloAcadM]").remove();
                //            //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/Tutor.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //                        //console.log(data);
                        if (data[0].rpta == 1) {
                            $('#hdcod').val(0);
                            //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                            Limpiar()
                            fnBuscarTutor(false);
                            fnMensaje("success", data[0].msje)
                            $('#mdRegistro').modal('hide');
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //            //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            }
        } else {
            if ($("#busPoint").val() == "") {
                fnMensaje("warning", "Debe Seleccionar un trabajador.");
            } else {
                $("form#frmCiclo input[id=action]").remove();
                $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
                $('#frmCiclo').append('<input type="hidden" id="cod" name="cod" value="' + codper + '" />');
                var form = $("#frmCiclo").serializeArray();
                $("form#frmCiclo input[id=action]").remove();
                $("form#frmCiclo input[id=cod]").remove();
                //            $('#hdcod').remove();
                //            //console.log(form);
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/Tutor.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //                        //console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            //$("#cboTipoEstudio").val($("#cboTipoEstudioR").val())
                            Limpiar()
                            fnBuscarTutor(false);
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //            //console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            }
            
        }

        fnLoading(false)
        
       
    } else {
        window.location.href = rpta
    }
}


function Edit() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmRegistro input[id=action]").remove();
        $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.edi + '" />');
        var form = $("#frmRegistro").serializeArray();
        $("form#frmRegistro input[id=action]").remove();
        //        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/Tutor.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
                            //console.log(data);
                codperM = data[0].cPer
//                $('#cboCicloAcadM').val(data[0].cCac);
                $('#cboPersonal').val(codperM);
                //document.getElementById("select2-cboPersonal-container").textContent=data[0].cNombre;
//                $("#txtfecini").val(data[0].cFecini);
//                $("#txtfecfin").val(data[0].cFecFin);
                if (data[0].cEstado == 1) {
                    $("#chkestado").prop("checked", true);
                } else {
                    $("#chkestado").prop("checked", false);
                }
                //                if (sw) { fnLoading(false); }
                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
                //            //console.log(result)
            }
        });
    } else {
        window.location.href = rpta
    }
}

function fnEliminar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //        fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/Tutor.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
//                //console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarTutor(false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function(result) {
                //            //console.log(result)
                fnMensaje("warning", result)
            }
        });
        //        fnLoading(false)
    } else {
        window.location.href = rpta
    }
}


var aDataR = [];
function fnDelete(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar este Tutor?'
    }
    var r=fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //    fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);

   
}

function Limpiar() {
    $("#cboTipoEstudioR").prop('selectedIndex', 0);
    $('#cboCicloAcademicoR').prop('selectedIndex', 0);
    $("#txtnombre").val("");
    $("#txtdetalle").val(""); 
    $("#txtfecini").val("");
    $("#txtfecfin").val("");
    $("#busPoint").val("");
    $("#chkestado").prop("checked", true);
}


function fnValidar() {
//    if ($("#cboTipoEstudioR").val() == '') {
//        fnMensaje("warning", 'Seleccione un Tipo de Estudio')
//        return false
//    }
//    if ($("#cboCicloAcadM").val() == '') {
//        fnMensaje("warning", 'Seleccione El Ciclo Académico')
//        return false
//    }
    if ($("#cboPersonal").val() == '') {
        fnMensaje("warning", 'Seleccione a un trabajador')
        return false
    }
//    if ($("#txtnombre").val() == '') {
//        fnMensaje("warning", 'Ingrese Nombre')
//        return false
//    }
//    if ($("#txtfecini").val() == '') {
//        fnMensaje("warning", 'Ingrese Fecha de Inicio')
//        return false
//    }
//    if ($("#txtfecfin").val() == '') {
//        fnMensaje("warning", 'Ingrese Fecha de Finalización')
//        return false
//    }
    return true
}

function fnCreateDataTableBasic(table, col, ord,len) {
    ////console.log(len);
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 30, 20, 10, 10, -1], [10, 30, 20, 10, 10, "All"]],
        "iDisplayLength": len,
        "aaSorting": [[col, ord]]
    });
    return dt;
};

function fnResetDataTableBasic(table, col, ord,len) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().fnDestroy();
        dt = $('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            "aLengthMenu": [[10, 30, 20, 10, 10, 10], [10, 30, 20, 10, 10, "All"]],
            "iDisplayLength": len,
            "aaSorting": [[col, ord]]
        });

        return dt;
    }
}

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}

function fnLoading(sw) {
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
}

function fnLoadingDiv(div, sw) {
    if (sw) {
        $("#" + div).removeClass('hidden');
    } else {
        $("#" + div).addClass('hidden');
    }
}
function fnAutoCPersonal() {
    lstPer = fnCargaPersonal();
    var jsonString = JSON.parse(lstPer);
    $('#busPoint').autocomplete({
        source: $.map(jsonString, function(item) {
            return item.nombre;
        }),
        select: function(event, ui) {
            var selectecItem = jsonString.filter(function(value) {
                return value.nombre == ui.item.value;
            });
            codper = selectecItem[0].cod;
            titulo = selectecItem[0].nombre;
            $('#PanelEvento').hide("fade");
            //alert("cod: " + selectecItem[0].cod + ", nombre: " + selectecItem[0].nombre);
        },
        minLength: 2,
        delay: 600
    });

    $('#busPoint').keyup(function() {
        var l = parseInt($(this).val().length);
        if (l == 0) {
            codper = "";
            titulo = "";
        }
    });

//    var jsonString = fnListarPersonal(1, 0, 0);
    var arr = JSON.parse(lstPer);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }
    $('#cboPersonal').html(str);
}

function fnSesiones(cod) {
 rpta = fnvalidaSession()
 if (rpta == true) {
     var arr = fSesiones(1, cod, $('#cboCicloAcad').val(), 'LTC','');
     var n = arr.length;
     var tb = "";
     $('#titDetalle').html('Sesiones: ' + arr[0].cDtc);

     for (i = 1; i < n; i++) {

         tb += '<tr>';
         tb += '<td style="text-align:center">' + (i) + '</td>';
         tb += '<td>' + arr[i].cDTis + '</td>';
         tb += '<td style="text-align:center">' + arr[i].cFecha + '</td>';
         tb += '<td style="text-align:center">' + arr[i].cHini + ' - ' + arr[i].cHfin + '</td>';
         tb += '<td>' + arr[i].cDstu + '</td>';
         tb += '<td style="text-align:center">' + arr[i].cPre + '/' + arr[i].cTotal + '</td>';

         //tb += '<td style="text-align:center">' + arr[i].sem + '</td>';
         tb += '</tr>';
     }

     fnDestroyDataTableDetalle('tDetalle');
     $('#tbDetalle').html(tb);
     fnResetDataTableBasic('tDetalle', 0, 'asc');
 } else {
    window.location.href = rpta
 }
}