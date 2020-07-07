var detalles = [];
var tabla = '';
var problemas = [];
var chk_problemas=[];
var max_descripcion = 500;

$(document).ready(function() {
    fnLoading(true);
    var dt = fnCreateDataTableBasic('tTutorado', 1, 'asc');
    var dtE = fnCreateDataTableBasicDetalle('tEval', 0, 'asc');
    var dtA = fnCreateDataTableBasic('tAsistencias', 0, 'asc');
    var dtD = fnCreateDataTableBasic('tDetalle', 0, 'asc');
    var dti = fnCreateDataTableBasicDetalle('tIndividual', 0, 'asc');
    
    //    var dtP = fnCreateDataTableBasic('tPorAsignar', 3, 'asc', 20);
    //    var dtA = fnCreateDataTableBasic('tAsignados', 2, 'asc', 20);

    ope = fnOperacion(1);
    //console.log(ope);
    rpta = fnvalidaSession()
    if (rpta == true) {
        
        fnCicloAcad('#cboCicloAcad')
        fnRiesgosEval();
        //        $('#tabTutor').click(fnBuscarTutor);
        //        $('#cboCategoria').change(fnCantAlumnos);
        //        $('#cboEscuela').change(fnCantAlumnos);
        //        $('#cboIng').change(fnCantAlumnos);
        $("#cboCicloAcad").change(function() {
            fnEscuela();
            limpiarLista();
        });
        $("#btnListar").click(function() {
            fnBuscarTutorados(false);
        });
        $("#cboEval").change(function() {
            fnVariableTEval();
        });
        $("#cboItemEv").change(function() {
            if ($("#cboItemEv").find('option:selected').attr("tipo") == "logico") {
                //console.log('1');
                $('#cboPuntaje').css("display", "block");
                $('#puntaje').css("display", "none");
                $('#txtRango').css("display", "none");
                fnOpcionVariable();
            } else {
                //console.log('2');
                $('#cboPuntaje').css("display", "none");
                $('#puntaje').css("display", "block");
                $('#txtRango').css("display", "block");
            }
        });

        document.getElementById("puntaje").maxLength = "3";
        $("#puntaje").keydown(function(e) {
        
                //console.log('s');
//            if ($("#puntaje").val().length==2){
//                //console.log('sss');
//                e.preventDefault();
//                }
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13]) !== -1 ||
                (e.keyCode == 65 & e.ctrlKey === true) ||
                (e.keyCode >= 35 & e.keyCode <= 39)) {
               
                //console.log('ss');
                return;
            }

            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) & (e.keyCode < 96 || e.keyCode > 105)) {
                //console.log('sss');
                e.preventDefault();
            }
        });
        
        $("#puntaje").keyup(function(e) {
//            if(parseInt($("#puntaje").val()) < 0 || isNaN(parseInt($("#puntaje").val()))) 
//                $("#puntaje").val('0') ; 
//            else 
            if(parseInt($("#puntaje").val()) > 100) 
                $("#puntaje").val('100') ; 
        });
             
        fnHora ();
        fnMinuto ();
        fnTipoSesion();
        
    } else {
        window.location.href = rpta
    }
    fnLoading(false);
    $('#mdRegistro').on('hide.bs.modal', function(event) {

        var col = $("#tbEval > tr:first > td").length;
        //console.log(col);
        $('#tEval td:nth-child(' + col + '),#tEval th:nth-child(' + col + ')').show();
    
    });
    $('#mdRegistro').on('show.bs.modal', function(event) {
        detalles=[];
        var button = $(event.relatedTarget) // Botón que activó el modal
        $('#cod').remove();
        $('#codE').remove();
        $('#frmRegistro').append('<input type="hidden" id="cod" name="cod" value="' + button.attr("hdc") + '" />');
        $('#frmRegistro').append('<input type="hidden" id="codE" name="codE" value="' + button.attr("hde") + '" />');
//        if ( button.attr("h")!=='ING ') {
//            $("#divEditar").css("display", "none");
//            $("#divEditar").css("visible", "hidden");
//            $("#divEvaluar").css("display", "none");
//            $("#divFooter").css("display", "none");
//            $("#divTotales").css("display", "block");
//        }else{
            if ($('#codE').val() != "0"  ) {
                fnListarDetEvaluacion();
                fnListarTotEvaluacion();
                $("#divEvaluar").css("display", "none");
                $("#divFooter").css("display", "none");
                $("#divEditar").css("display", "block");
                $("#divTotales").css("display", "block");
                if ( button.attr("h")!=='ING') {
                    $("#divEditar").css("display", "none");
                }
                
            }
            if ($('#codE').val() == "0") {
                fnDestroyDataTableDetalle('tEval');
                $('#tbEval').html('');
                fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                $("#divEditar").css("display", "none");
                $("#divEvaluar").css("display", "block");
                $("#divFooter").css("display", "block");
                $("#divFooter").css("visibility", "visible");
                $("#divTotales").css("display", "none");
                
            }
//        }
            tabla=$("#tbEval").html();
    })
     $('#mdAsistencias').on('show.bs.modal', function(event) {
            var button = $(event.relatedTarget) 
            //console.log(button.closest('tr').attr("name"));
            if (button.attr("id")=="btnA"){
                fnAsistencias(button.attr("hdc"));
                $('#titAsistencias').html('Asistencias Moodle: ' + button.closest('tr').attr("name"));
            }else if (button.attr("id")=="btnN"){
                fnNotas(button.attr("hdc"));
                $('#titAsistencias').html('Notas Moodle: ' + button.closest('tr').attr("name"));
            }              
    });
     $('#mdAlumno').on('show.bs.modal', function(event) {
            LimpiarDatos();
            var button = $(event.relatedTarget) 
            //console.log(button.closest('tr').attr("name"));            
                fnDatos(button.attr("hdc"));            
    });
     $('#mdDetalle').on('show.bs.modal', function(event) {
            var button = $(event.relatedTarget) 
            if (button.attr("id")=="btnSe"){
                 fnSesiones(button.attr("hdc"));
                 $('#titDetalle').html('Sesiones: ' + button.closest('tr').attr("name")); 
                 $('#hdcod').remove();
                 $('#frmDetalle').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
                 
                $('#divTodas').css("display", "block");
                $('#divTodas').css("visibility", "visible");   
                $('#divIndividual').css("display", "none");
                $('#divIndividual').css("visibility", "hidden");  
                document.getElementById("lblInd").textContent = "Ver Tutoría Individual"; 
           }    
    });
     $('#mdNueva').on('click', 'input[class="chkP"]', function(e) {
 
            var id = $(this).attr("d");

            var index = $.inArray(id, chk_problemas );

            if (this.checked && index === -1) {
                var d = $(this).attr("d");
                chk_problemas .push(d);
                problemas.push({
                    d: d
                });
            } else if (!this.checked && index !== -1) {
                chk_problemas.splice(index, 1);                
                problemas.splice(index, 1);
            }
            ////console.log(problemas );

        });
      
      $("#btnCloseEval").click( function(){
         if (tabla!=$("#tbEval").html() && $("#divEvaluar").is(":visible")){
              $('#mdRegistro').css("z-index","0");
                swal({
		            title: "¿Desea continuar?",
		            text: "Los datos han sido modificados, si cierra esta ventana perderá los cambios.",
		            type: "info",
		            showCancelButton: true,
		            confirmButtonClass: 'btn-info',
		            confirmButtonText: 'Ok',
		            closeOnConfirm: true,
                      //closeOnCancel: false
                  },
                  function(){
                   $('#mdRegistro').modal('hide');
      	            //swal("Good!", "Thanks for clicking!", "success");
      	            //alert('s');
                  });
           }else{
                   $('#mdRegistro').modal('hide');
                    LimpiarEval();
           }
      });
      
      $('#contador').html(max_descripcion);

        $('#lblDescripcion').keyup(function() {
            var chars = $(this).val().length;
            var diff = max_descripcion - chars;
            $('#contador').html(diff);   
        });
      

})
function LimpiarDatos(){
        $("#imgFoto").attr('src','');
        $("#liCodigo").html('');     
        $("#liEstudiante").html('');         
        $("#liIngreso").html('')         
        $("#liModalidad").html('')      
        $("#liEscuela").html('')     
        $("#liDoc").html('');      
        $("#liNac").html('');      
        $("#liSexo").html('');      
        $("#liCivil").html('');      
        $("#liDireccion").html('');      
        $("#liEmail").html('');      
        $("#liTelf").html(''); 
}
function fnDatos(cod) {

    var arr = fDatos(1, cod);
    var n = arr.length;
    var str = "";
    
    if (arr.length > 0){
        if (arr[0].s='F'){            
            var sexo = 'Femenino';
        }else{    
            var sexo = 'Masculino';
        }
        if (arr[0].em2!=''){            
            var email = '<a href="#" onclick="fnEmail(this)" hd="' +arr[0].em  +'">' + arr[0].em + '</a> / <a href="#" onclick="fnEmail(this)" hd="'+ arr[0].em2+'">'+ arr[0].em2 + '</a>';
        }else{    
            var email = '<a href="#" onclick="fnEmail(this)" hd="' +arr[0].em  +'">' + arr[0].em + '</a>';
        }
        if (arr[0].tm!=''){            
            var telf = arr[0].tc + ' / '+ arr[0].tm ;
        }else{    
            var telf = arr[0].tc;
        }
        $("#imgFoto").attr('src',arr[0].f);
        $("#liCodigo").append(arr[0].codU);     
        $("#liEstudiante").append(arr[0].nombre);         
        $("#liIngreso").append(arr[0].si);         
        $("#liModalidad").append(arr[0].min);      
        $("#liEscuela").append(arr[0].esc);      
        $("#liDoc").append(arr[0].ti + ' ' + arr[0].ni );      
        $("#liNac").append(arr[0].nac);          
        $("#liSexo").append(sexo);          
        $("#liCivil").append(arr[0].ec);          
        $("#liDireccion").append(arr[0].dir + ' '+ arr[0].urb + ' - '+ arr[0].dis + ' - '+ arr[0].pro );          
        $("#liEmail").append(email  );          
        $("#liTelf").append(telf );      
    }
    

//    str += '<option value="" selected>-- Seleccione -- </option>';
//    for (i = 0; i < n; i++) {        
//            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
//    }
//    
//    $("#cboCarrera").html(str);   
//    var f = document.getElementById('cboCarrera');
//    f.selectedIndex =0; 
}
 function fnEmail(event){
 
 var button = $(event) // Botón que activó el modal
            //        //        alert('--')
    //if (button.attr("hd") == "btnGuardarEval") {
     //event.preventDefault();
    //alert(button.attr("hd"));
    var email = button.attr("hd");
    var subject = 'Tutoría';
    var emailBody = '';
    window.location = 'mailto:' + email + '?subject=' + subject + '&body=' +   emailBody;
 }
function fnEscuela() {

    var f = $('option:selected', $("#cboCicloAcad")).attr('hd');
    var arr = fAux(1, "ESC",$("#cboCicloAcad").val() ,f,'','','');
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboCarreraP").html(str);   
//    var f = document.getElementById('cboCarrera');
//    f.selectedIndex =0; 
}
function fnEditar() {
    $("#divEditar").css("display", "none");
    $("#divEvaluar").css("display", "block");
    $("#divFooter").css("display", "block");
    $("#divTotales").css("display", "none");
    
    var tb = '';
    for (i = 0; i < detalles.length; i++) {
        tb += '<tr>';
        tb += '<td style="text-align:center">' + (i + 1) + '</td>';
        tb += '<td>' + detalles[i].evalmostrar + '</td>';
        tb += '<td>' + detalles[i].itemmostrar + '</td>';
        tb += '<td style="text-align:center">' + detalles[i].resultadomostrar + '</td>';
        //tb += '<td style="text-align:center"></td>';
        tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnDeleteItem(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
        tb += '</tr>';
    }

    fnDestroyDataTableDetalle('tEval');
    $('#tbEval').html(tb);
    fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);

    var col = $("#tbEval > tr:first > td").length;
    //console.log(col);
    $('#tEval td:nth-child(' + col + '),#tEval th:nth-child(' + col + ')').show();
    tabla=$("#tbEval").html();
}

function fnCicloAcad(cboId) {

    var arr = fnCicloAcademico(1, "LT", "");
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '" hd="'+arr[i].t + '">' + arr[i].nombre + '</option>';
    }

    $(cboId).html(str);
}
function fnCargarDatos(cod) {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)

            $('#action').remove();
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
                    $("form#frmTutor input[id=action]").remove();
                    ////console.log(data);

                    document.getElementById("lblTutor").textContent = "Tutor: " + data[0].cNombre;
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
function fnBuscarTutorados(){
if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)
            $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
            $('#frmCiclo').append('<input type="hidden" id="tipo" name="tipo" value="LP" />');
            var form = $("#frmCiclo").serializeArray();
            
            $("form#frmCiclo input[id=action]").remove();
            $("form#frmCiclo input[id=tipo]").remove();
            //console.log(form);

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/TutorAlumno.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                $("form#frmCiclo input[id=action]").remove();
                $("form#frmCiclo input[id=tipo]").remove();
                    console.log(data);
                    
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                    for (i = 0; i < filas; i++) {
                        var colorA='';
                        var colorN='';
                        var eval='';                        
                        var a;
                        var modal='data-toggle="modal" data-target="#mdRegistro"';
                        if (data[i].cAsistM =="V"){
                            colorA="#6fd64b";
                        }else if (data[i].cAsistM =="R"){
                            colorA="#d9534f";
                        }else if (data[i].cAsistM =="A"){
                            colorA="#f1e019";
                        }else {
                            colorA="";
                        }
                        
                        if (data[i].cNotasM =="V"){
                            colorN="#6fd64b";
                        }else if (data[i].cNotasM =="R"){
                            colorN="#d9534f";
                        }else if (data[i].cNotasM =="A"){
                            colorN="#f1e019";
                        }
                        
                        tb += '<tr name="'+ data[i].cAlumno+'">';
                        tb += '<td>' + data[i].cTutor + "" + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cCodU + "" + '</td>';
                        tb += '<td style="cursor:pointer;color: #337ab7;font-weight:500;" data-toggle="modal" data-target="#mdAlumno" hdc="' + data[i].cTA +'" >' + data[i].cAlumno + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cAbrev + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cCat + '</td>';
                        if(data[i].cCac == data[i].cCaI){
                            a="ING";
                        }else{
                            a=""
                        }
                        if( data[i].cCac !== data[i].cCaI && data[i].cEva ==0 ){
                            modal="";
                        }
                        if (  data[i].cREval!=='' ||   data[i].cCac !== data[i].cCaI  ){
                            if(data[i].cREval==''){
                                eval='No registrada';
                            }
                            else{
                                eval='R. ' + data[i].cREval;
                            }
                            tb += '<td style="text-align:center"><a  href="#" id="btnD" name="btnD" '+ modal + ' h="'+ a+'" hdc="' + data[i].cAlu + '"  hde="' + data[i].cEva + '" ><label style="font-weight:100;">' +eval +'</label></a></td>';
                        }else {
                            tb += '<td style="text-align:center"><button type="button" id="btnD" name="btnD" class="btn btn-sm btn-info btn-radius"' +modal +' h="'+ a +'" hdc="' + data[i].cAlu + '"  hde="' + data[i].cEva + '" title="Registrar" >Registrar</button></td>';
                        }
                        
                        tb += '<td style="text-align:center"><button type="button" id="btnA" name="btnA" class="btn btn-md btn-info" data-toggle="modal" data-target="#mdAsistencias"  hdc="' + data[i].cTA + '" title="Asistencias Moodle" style="background-color:'+ colorA + ';" >' + data[i].cAsistM + '</button></td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnN" name="btnN" class="btn btn-md btn-info" data-toggle="modal" data-target="#mdAsistencias" hdc="' + data[i].cTA + '" title="Notas Moodle" style="background-color:'+ colorN + ';" >' + data[i].cNotasM + '</button></td>';
                        tb += '<td style="text-align:center"> <a href="#" id="btnSe"  hdc="' + data[i].cTA + '" data-toggle="modal" data-target="#mdDetalle"><label class="control-label" style="font-size:13px;cursor:pointer;color: #337ab7;">'+ data[i].cSes +'</label></a></td>';
                        tb += '<td style="text-align:center"><button type="button" id="btnS" name="btnS" class="btn btn-md btn-info" onclick="fnModal(\''+ data[i].cTA +'\',\''+ data[i].cCpf + '\')" hdc="' + data[i].cTA + '" title="Nueva Sesión" ><i class="ion-android-clipboard"></i></button></td>';
// <a href="#" onclick="fnBuscarAlumnos()"><label id="lblCantAlumnos" style="float: right;cursor:pointer;"></label></a>
                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tTutorado');
                    $('#tbTutorado').html(tb);
                    fnResetDataTableBasic('tTutorado', 3, 'asc',20);
                    //if (sw) { fnLoading(false); }
                    //                    fnLoading(false);
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
  
  function fnModal(cod,cpf) {
          
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")

    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            $("#hdcodS").remove();
            $("#hdcodSE").remove();
            $("#cboCarrera").remove();
            $('#frmNueva').append('<input type="hidden" id="hdcodS" name="hdcodS" value="'+ cod+'" />');
            $('#frmNueva').append('<input type="hidden" id="hdcodSE" name="hdcodSE" value="0" />');   
            $('#frmNueva').append('<input type="hidden" id="cboCarrera" name="cboCarrera" value="' + cpf +'" />');   
            $('#mdNueva').modal('show');            
            //fnAutoCTutores();
            InicioAgregar();
            fnActividad ();
            fnEstado ();
            fnResultado ();
            fnNivelRiesgo ();
            fnTipoProblema ();
            //LimpiarTutorado();
        }else{
            window.location.href = rpta
        }
    }
}
function InicioAgregar() {
    
    $('#divFooter').css("visibility", "hidden");
    $('#divFooter').css("display", "none");
    
    LimpiarTutorado();
    LimpiarSesion();
}
function LimpiarTutorado() {
    
    
    var f = document.getElementById('cboTipo');
    f.selectedIndex = 1;
    
    var now = new Date();
    var today = zeroPad(now.getDate(), 2)   + '/' + zeroPad(now.getMonth() + 1,2) + '/' + now.getFullYear();
    ////console.log(today);
    $('#dtpFecha').val(today);
    $('#dtpFecha').datepicker('setDate',today);
    
    $('#divPrimero').css("visibility", "visible");
    $('#divPrimero').css("display", "block");
    $('#pnlNueva').css("visibility", "hidden");
    $('#pnlNueva').css("display", "none");
    $('#cboHoraD').val('0');
    $('#cboMinutoD').val('0');
    $('#cboHoraA').val('0');
    $('#cboMinutoA').val('0');
    
    $('#lblDescripcion').val('');
        
}
function LimpiarSesion() {
    $('#txtIncidencia').val('');
    $('#txtComentario').val('');
    $('#txtAccion').val('');   
    $('#cboResultado').val('');  
    $('#cboActividad').val(''); 
    $('#cboRiesgo').val('');    
}
  function fnListarDetEvaluacion() {
      if ($("#cboCicloAcad").val() == "") {
          fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
      } else {
          if (rpta == true) {
              //if (sw) { fnLoading(true); }
              //    fnLoading(true)
              $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
              var form = $("#frmRegistro").serializeArray();

              $("form#frmRegistro input[id=action]").remove();

              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/EvaluacionEntrada.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      $("form#frmRegistro input[id=action]").remove();
                      ////console.log(data);
                      detalles = [];
                      var tb = '';
                      var i = 0;
                      var filas = data.length;
                      for (i = 0; i < filas; i++) {
                          detalles.push({
                              eval: data[i].cTeva,
                              evalmostrar: data[i].cDeva,
                              item: data[i].cVt,
                              itemmostrar: data[i].cDVar,
                              resultado: data[i].cPuntaje,
                              resultadomostrar: data[i].cResultado,
                              opcion: data[i].cOpcion
                          });
                            
                          tb += '<tr>';
                          tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                          tb += '<td>' + data[i].cDeva + '</td>';
                          tb += '<td>' + data[i].cDVar + '</td>';
                          tb += '<td style="text-align:center">' + data[i].cResultado + '</td>';
                          //tb += '<td style="text-align:center"></td>';
                          tb += '<td style="text-align:center"></td>';
                          tb += '</tr>';
                      }
                      
                      //console.log(data);
                      fnDestroyDataTableDetalle('tEval');
                      $('#tbEval').html(tb);
                      fnResetDataTableBasicDetalle('tEval', 1, 'asc');
                      var col = $("#tbEval > tr:first > td").length;
                      ////console.log(detalles);
                      $('#tEval td:nth-child(' +col +'),#tEval th:nth-child(' +col +')').hide();
                      //#tbEval th:nth-child(2)
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
  function fnListarTotEvaluacion() {
      if ($("#cboCicloAcad").val() == "") {
          fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
      } else {
          if (rpta == true) {
              //if (sw) { fnLoading(true); }
              //    fnLoading(true)
              $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.lstT + '" />');
              var form = $("#frmRegistro").serializeArray();

              $("form#frmRegistro input[id=action]").remove();
              //console.log(form);

              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/EvaluacionEntrada.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      $("form#frmRegistro input[id=action]").remove();
                      ////console.log(data);
                      var div = '';
                      var tb = '';
                      var i = 0;
                      var filas = data.length;

                      div += '<div class="alert alert-danger">';
                      div += 'Alerta: Hay ítems obligatorios por registrar.';
                      div += '</div>';

                      $('#divRiesgoF').html(div);

                      if (data[i].cRiesgoF == "") {

                      } else {
                          div = '';
                          div += '<div class="row form-group" style="text-align:center" >'
                          div += '<h4>';
                          div += '<button class="btn btn-danger" type="button" style="width:80%;" >Riesgo Final <span class="badge">' + data[i].cRiesgoF + '</span></button>';
                          div += '</h4>';
                          div += '</div>';



                          for (i = 0; i < filas; i++) {
                              div += '<div class="" style="text-align:center" >'
                              div += '<h4>';
                              div += '<button class="btn btn-success" type="button" style="width:60%;">' + data[i].cDeva + ' <span class="badge">' + data[i].cRiesgo + '</span>';
                              div += '</h4>';
                              div += '</div>';
                          }

                          $('#divRiesgoF').html(div);
                      }
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
  function fnRiesgosEval() {

      var arr = fRiesgosEval(1, 0, 0);
      var n = arr.length;
      var str = "";
      str += '<option value="" selected>-- Seleccione -- </option>';
      for (i = 0; i < n; i++) {
          str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
      }

      $('#cboEval').html(str);

  }
  function fnVariableTEval() {

      var arr = fVariableTEval(1,$("#cboEval").val());
      var n = arr.length;
      var str = "";
      str += '<option value="" selected>-- Seleccione -- </option>';
      for (i = 0; i < n; i++) {
          str += '<option value="' + arr[i].cod + '" hdcod="' + arr[i].codV + '" tipo="' + arr[i].tvar + '">' + arr[i].nombre + '</option>';
      }

      $('#cboItemEv').html(str);

  }
  function fnOpcionVariable() {
      var option = $('option:selected', $("#cboItemEv")).attr('hdcod');
      //console.log(option );
      var arr = fOpcionVariables(1,option);
      var n = arr.length;
      var str = "";
      str += '<option value="" selected>-- Seleccione -- </option>';
      for (i = 0; i < n; i++) {
          str += '<option value="' + arr[i].cCod + '" >' + arr[i].cDescripcion + '</option>';
      }

      $('#cboPuntaje').html(str);
  }
  
  
  function fnValidarItem() {
      if ($("#cboEval").val() == '') {
          fnMensaje("warning", 'Seleccione un tipo de Evaluación')
          return false
      }
      if ($("#cboItemEv").val() == '') {
          fnMensaje("warning", 'Seleccione un Ítem')
          return false
      }
     
      if ($("#cboItemEv").find('option:selected').attr("tipo") == "logico") {
          if ($("#cboPuntaje").val() == '') {
              fnMensaje("warning", 'Seleccione el Resultado')
              return false
          }
      } else {
          if ($("#puntaje").val() == '') {
              fnMensaje("warning", 'Ingrese un resultado')
              return false
          }
      }
     
      return true
  }
  function fnAgregarItem() {
      ////console.log('ff');
      
      var value;
      var tb = '';
      var tb2 = '';
      var rowCount = $('#tbEval tr').length;
      var repite=false;

      if (fnValidarItem() == true) {
//          $.grep(detalles, function(e) { return e.item == id; });
          for (i = 0; i < detalles.length; i++) {
              if (detalles[i].item == $('#cboItemEv').val()) {
                  repite = true;
              }
          }
          ////console.log(repite);
          if (repite == false) {
              $('#tbEval tr').each(function() {
                  value = $(this).find("td:first").html();

              });
              if (!($.isNumeric(value))) { rowCount = 0 }

              var row = (rowCount + 1);
              var eval = $('#cboEval').val();
              var evalmostrar = $('#cboEval option:selected').text()
              var item = $('#cboItemEv').val();
              var itemmostrar = $('#cboItemEv option:selected').text();
              if ($("#cboItemEv").find('option:selected').attr("tipo") == "logico") {
                  var resultadomostrar = $('#cboPuntaje option:selected').text();
                  var resultado = 0;
                  var opcion = $('#cboPuntaje').val();
              } else {
                  var resultadomostrar = $('#puntaje').val();
                  var resultado = $('#puntaje').val();
                  var opcion = 0;
              }

              detalles.push({
                  eval: eval,
                  evalmostrar: evalmostrar,
                  item: item,
                  itemmostrar: itemmostrar,
                  resultado: resultado,
                  resultadomostrar: resultadomostrar,
                  opcion: opcion
              });

              //console.log(detalles);
              for (i = 0; i < detalles.length; i++) {
                  tb += '<tr>';
                  tb += '<td style="text-align:center">' + (i + 1) + '</td>';
                  tb += '<td>' + detalles[i].evalmostrar + '</td>';
                  tb += '<td>' + detalles[i].itemmostrar + '</td>';
                  tb += '<td style="text-align:center">' + detalles[i].resultadomostrar + '</td>';
                  //tb += '<td style="text-align:center"></td>';
                  tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnDeleteItem(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
                  tb += '</tr>';
              }

              fnDestroyDataTableDetalle('tEval');

              ////console.log(value);
              //          if (!($.isNumeric(value))) {
              //              $('#tbEval').html(tb);
              //          } else {
              //              tb2 = $('#tbEval').html();
              //              $('#tbEval').html(tb2 + tb);
              //          }

              $('#tbEval').html(tb);
              fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
              LimpiarEval();
          } else {

                fnMensaje("warning", "El ítem ya ha sido ingresado")
          }
          
      }
}
function LimpiarEval() {
        $('#puntaje').val('');
        $('#cboItemEv').val('');
        $('#cboPuntaje').val('');
}
function limpiarLista(){
        
        fnDestroyDataTableDetalle('tTutorado');
        $('#tbTutorado').html('');
        fnResetDataTableBasic('tTutorado', 3, 'asc',20);
        
}
function fnDeleteItem(cod) {
        var tb = '';
      //console.log(cod);
//      document.getElementById("tEval").deleteRow(cod);
      detalles.splice(cod-1, 1);
      for (i = 0; i < detalles.length; i++) {
          tb += '<tr>';
          tb += '<td style="text-align:center">' + (i + 1) + '</td>';
          tb += '<td>' + detalles[i].evalmostrar + '</td>';
          tb += '<td>' + detalles[i].itemmostrar + '</td>';
          tb += '<td style="text-align:center">' + detalles[i].resultadomostrar + '</td>';
          //tb += '<td style="text-align:center"></td>';
          tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnDeleteItem(' + (i + 1) + ')" ><i class="ion-android-delete"></i></button></td>';
          tb += '</tr>';
      }

      fnDestroyDataTableDetalle('tEval');
      $('#tbEval').html(tb);
      fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
}
function fnGuardarDetalleEval() {
      rpta = fnvalidaSession()
      if (rpta == true) {

          fnLoading(true)
         
          if ($('#codE').val() == "0") {
              var form = JSON.stringify(detalles);
              //console.log(form);
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/EvaluacionEntrada.aspx",
                  data: { "hdcod": $("#cod").val(), "action": ope.reg, "array": form },
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        //console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcod').val(0);
                          //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                          LimpiarEval()
                          fnBuscarTutorados(false);
                          fnMensaje("success", data[0].msje)

                          fnDestroyDataTableDetalle('tEval');
                          $('#tbEval').html('');
                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                          $('#mdRegistro').modal('hide');
                          detalles = [];
                          
                      } else {
                          fnMensaje("warning", data[0].msje)
                      }
                  },
                  error: function(result) {
                      //            //console.log(result)
                      fnMensaje("warning", result)
                  }
              });
          } else {
              var form = JSON.stringify(detalles);
              //console.log(form);
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/EvaluacionEntrada.aspx",
                  data: { "hdcod": $("#cod").val(), "action": ope.mod, "array": form },
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        //console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcod').val(0);
                          //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                          LimpiarEval()
                          fnBuscarTutorados(false);
                          fnMensaje("success", data[0].msje)

                          fnDestroyDataTableDetalle('tEval');
                          $('#tbEval').html('');
                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                          $('#mdRegistro').modal('hide');
                          var detalles = [];
                          
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
          
          fnLoading(false)

      } else {
          window.location.href = rpta
      }
  }
  function fnCancelar(event) { 
//  //console.log("a + " + tabla);
//  //console.log($("#tbEval").html());
    var button = $(event) // Botón que activó el modal
            //        //        alert('--')
        if (button.attr("id") == "btnCancelarSesion") {               
            $('#mdNueva').modal('hide');      
        }
        else if(button.attr("id") == "btnCancelarReg"){
            if (tabla!=$("#tbEval").html()){
              $('#mdRegistro').css("z-index","0");
                swal({
		            title: "¿Desea continuar?",
		            text: "Los datos han sido modificados, si cierra esta ventana perderá los cambios.",
		            type: "info",
		            showCancelButton: true,
		            confirmButtonClass: 'btn-info',
		            confirmButtonText: 'Ok',
		            closeOnConfirm: true,
                      //closeOnCancel: false
                  },
                  function(){
                   $('#mdRegistro').modal('hide');
      	            //swal("Good!", "Thanks for clicking!", "success");
      	            //alert('s');
                  });
           }else{
                   $('#mdRegistro').modal('hide');
                    LimpiarEval();
           }
        }
      
    
  }
  
  function fnHora() {

    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < 24; i++) {
        str += '<option value="' + i + '">' + zeroPad(i, 2) + '</option>';
    }
    $('#cboHoraA').html(str);
    $('#cboHoraD').html(str);
    //$(".cboHora").html(str);
}
function fnMinuto() {

    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < 60; i+=5) {
        str += '<option value="' + i + '">' + zeroPad(i, 2) + '</option>';
    }
    $('#cboMinutoA').html(str);
    $('#cboMinutoD').html(str);
    //$(".cboHora").html(str);
}
function fnTipoSesion() {

    var arr = fTipoSesion(1,"LI",0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option opc="' + arr[i].opc +'" value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $("select[name=cboTipo]").html(str);
    $("#cboTipo").html(str);
    
}
function ValidaSesion(){
     
      if ($("#dtpFecha").val() == '') {
          fnMensaje("warning", 'Seleccione una fecha')
          return false
      }
      if ($("#cboHoraD").val() == '0') {
          fnMensaje("warning", 'Seleccione la hora de inicio de la sesión')
          return false
      }
      if ($("#cboHoraA").val() == '0') {
          fnMensaje("warning", 'Seleccione la hora fin de la sesión ')
          return false
      }
      if (parseInt ($("#cboHoraA").val()) < parseInt( $("#cboHoraD").val()) ) {
          fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
          return false
      }else if ((parseInt ($("#cboHoraA").val()) == parseInt( $("#cboHoraD").val())) && (parseInt ($("#cboMinutoA").val()) < parseInt( $("#cboMinutoD").val()) )){
       fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
          return false
      }
      if ($("#lblDescripcion").val().replace(/ /g, '') == '') {
          fnMensaje("warning", 'Es necesario una breve descripción')
          return false
      }
      return true
}
function fnConfirmar(event){

 var button = $(event) // Botón que activó el modal
            //        //        alert('--')
    if (button.attr("id") == "btnGuardarEval") {
         swal({
		title: "Confirmación",
		text: "¿Desea guardar esta evaluación?",
		type: "success",
		showCancelButton: true,
		confirmButtonClass: 'btn-success',
		confirmButtonText: 'OK',
		closeOnConfirm: true,
          //closeOnCancel: false
      },
      function(){
        $('#mdRegistro').modal('hide');
        fnGuardarDetalleEval();
      	//swal("Thanks!", "We are glad you clicked welcome!", "success");
      });
    }else if(button.attr("id") == "btnGuardarSesion"){   
        var valida;
        if ($('#hdcodSE').val() == "0"){
            valida =ValidarSesion()
        } else{            
            valida =ValidarSesionI()
        }
         if (valida==true){
                swal({
		                title: "Confirmación",
		                text: "¿Desea guardar la sesión?",
		                type: "success",
		                showCancelButton: true,
		                confirmButtonClass: 'btn-success',
		                confirmButtonText: 'OK',
		                closeOnConfirm: true,
                  //closeOnCancel: false
                },
              function(){
                //$('#mdRegistro').modal('hide');
                fnGuardar();
      	        //swal("Thanks!", "We are glad you clicked welcome!", "success");
              });
        }
    }
          //$('#mdRegistro').css("z-index","0");
   
}
function fnGuardar() {
      rpta = fnvalidaSession();
      if (rpta == true) {
       
            fnLoading(true)
          if ($('#hdcodSE').val() == "0") {
           // //console.log(sesiones.length );
       //     if ( sesiones.length ==0) {
          
                
                var tipo = 1;  
                var alumnos=[];
                alumnos.push({hdc: $('#hdcodS').val() });              
                //sesiones =[];
              $('#action').remove();
              $("form#frmNueva input[id=cboCicloAcad]").remove();
              $('#frmNueva').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
              $('#frmNueva').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');
              var form = $("#frmNueva").serializeArray();              
              var array = JSON.stringify(alumnos); 
              //var array1 = JSON.stringify(chk_dias);
              form.push({"name":"array","value":array},{"name":"tipo","value":tipo});
              //console.log(form);
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/SesionTutor.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        //console.log(data);
                      if (data[0].rpta == 1) {
                            $('#hdcod').val();
                            $('#divPrimero').css("visibility", "hidden");
                            $('#divPrimero').css("display", "none");
                            $('#pnlNueva').css("visibility", "visible");
                            $('#pnlNueva').css("display", "block");
                            
                            if  ( data[0].cod !="" ){
                                $('#hdcodSE').val(data[0].cod);
                                $("#btnGuardarSesion").html('Guardar');
                                
                                fnGuardarDetalle();
                                fnEditI(data[0].cod);
                                fnBuscarTutorados(false);
                            }
                                 
                          
                      } else if (data[0].rpta == -1){
                          fnMensaje("warning", "Usted no se ha identificado como tutor.");
                      } else {
                          fnMensaje("warning", data[0].msje);
                      }
                  },
                  error: function(result) {
                      //            //console.log(result)
                      fnMensaje("warning", result)
                  }
              });
              
          } else{
              if (ValidarSesionI()==true){
                    $('#action').remove();
                  $('#tipoA').remove();
                  $('#frmNueva').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
                  $('#frmNueva').append('<input type="hidden" id="tipoA" name="tipo" value="2" />');
                  var form = $("#frmNueva").serializeArray();              
                  var array = JSON.stringify(problemas); 
                  form.push({"name":"array","value":array});
                  $('#action').remove();
                  $('#tipoA').remove();
                  
                  $.ajax({
                      type: "POST",
                      url: "../DataJson/tutoria/SesionAlumno.aspx",
                      data: form,
                      dataType: "json",
                      cache: false,
                      success: function(data) {
                          //                        //console.log(data);
                          if (data[0].rpta == 1) {
                              $('#hdcodSE').val(0);
                               LimpiarTutorado();
                              $('#mdNueva').modal('hide');                          
                              fnBuscarTutorados(false);
                              problemas =[];
                              chk_problemas =[];
                              fnMensaje("success", data[0].msje)
                              
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
    function fnActividad() {
   
    var arr = fActividad(1, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboActividad").html(str);    

}

 function fnEstado() {
   
    var arr = fEstado(1, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboEstado").html(str);    

}
 function fnResultado() {
   
    var arr = fResultado(1, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboResultado").html(str);    

}
 function fnNivelRiesgo() {
   
    var arr = fNivelRiesgo(1, 0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboRiesgo").html(str);    

}
 function fnTipoProblema() {
   
    var arr = fTipoProblema(1, 0);
    var n = arr.length;
    var str = "";
    for (i = 0; i < n; i++) {        
            str +='<li class="ms-hover">';
            str += '<input type="checkbox" id="chkP'+ (i+1) +'" class="chkP" d="'+ arr[i].cod +'" name="chkP'+(i+1) + '">';
            str +='<label for="chkP'+ (i+1) +'">';
            str +='<span></span>'+ arr[i].nombre +'</label>';
            str +='</li>';
    }
    
    $("#lstProblemas").html(str);    

}
function fnEditI(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmNueva input[id=action]").remove();
        $('#frmNueva').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        //$('#frmIndividual').append('<input type="hidden" id="hdcodSE" name="hdcodSE" value="' + $("#hdcodS").val() + '" />');
        var form = $("#frmNueva").serializeArray();
        $("form#frmNueva input[id=action]").remove();
        //$("form#frmNueva input[id=hdcodSE]").remove();
        //        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionAlumno.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
            //                //console.log(data);
                //codperM = data[0].cPer
//                var prob = data[0].cPro.split("/");
//                
//                for (i = 0; i < prob.length ; i++) {
//                    $("#mdIndividual").find("input[d='" + prob[i] + "']").trigger('click');
//                }
//                //console.log(prob);
                
                document.getElementById("lblIndividual").textContent = data[1].cAlumno;
//                $('#cboActividad').val(data[0].cAct);
//                $('#cboEstado').val(data[0].cEtu);
//                $('#cboResultado').val(data[0].cRes);
//                $('#cboRiesgo').val(data[0].cNri);
//                $('#txtIncidencia').val(data[0].cIncidencia);
//                $('#txtComentario').val(data[0].cComent);
//                $('#txtAccion').val(data[0].cAcc);
//                $('#dtpFechaF').val(data[0].cFechaEj);
                
                $('#hdcodS').remove();
                $('#frmNueva').append('<input type="hidden" id="hdcodS" name="hdcodS" value="' + data[1].cSA +'" />');
                fnLoading(false);
            },
            error: function(result) {
                fnLoading(false)
            }
        });
    } else {
        window.location.href = rpta
    }
}
function fnGuardarDetalle() {
      rpta = fnvalidaSession();
      if (rpta == true) {
           
                
            fnLoading(true)
          if ($('#hdcodSE').val() !== "0") {      
 //           if (sesiones.length  > 0) {
//              var chk = $('#chkUna');
//                if (chk.is(':checked')) {
//                    var tipo = 1;
//                }else{
//                    var tipo = 2;                
//                }
                 var alumnos=[];
                 var sesiones=[];
                alumnos.push({hdc: $('#hdcodS').val() });  
                sesiones.push({stu: $('#hdcodSE').val() });    
                   
              $('#action').remove();
              $("form#frmNueva input[id=cboCicloAcad]").remove();
              $('#frmNueva').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
              $('#frmNueva').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');
              var form = $("#frmNueva").serializeArray();              
              var array = JSON.stringify(alumnos); 
              var array1 = JSON.stringify(sesiones );
              form.push({"name":"array","value":array},{"name":"array1","value":array1});
              //console.log(form);
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/SesionAlumno.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        //console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcod').val(0);
                          //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                          //LimpiarEval()
                          //fnBuscarTutorados(false);
                          fnMensaje("success", data[0].msje)

//                          fnDestroyDataTableDetalle('tEval');
//                          $('#tbEval').html('');
//                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                          //$('#mdRegistro').modal('hide');
                         // fnBuscarSesiones(false);
                         // alumnos = [];
                          
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
          
            fnLoading(false )
          
      }else{
            
            window.location.href = rpta
      }
}
function ValidarSesionI(){
      if ($("#cboActividad").val() == '') {
          fnMensaje("warning", 'Seleccione una actividad')
          return false
      }
      if ($("#txtIncidencia").val() == '') {
          fnMensaje("warning", 'Es necesario descripbir la incidencia')
          return false
      }
      if ($("#txtComentario").val() == '') {
          fnMensaje("warning", 'Es necesario un comentario del tutor')
          return false
      }
      if ($("#txtAccion").val() == '') {
          fnMensaje("warning", 'Es necesario describir la acción futura')
          return false
      }
      if ($("#cboEstado").val() == '') {
          fnMensaje("warning", 'Seleccione un Estado')
          return false
      }
      
      if ($("#dtpFechaF").val() !== '' && $("#dtpFechaF").datepicker('getDate') < $("#dtpFecha").datepicker('getDate')) {        
          fnMensaje("warning", 'La fecha de la próxima cita debe ser mayor a la fecha de esta Sesión')
          return false
      }
      if ($("#cboResultado").val() == '') {
          fnMensaje("warning", 'Seleccione un Resultado')
          return false
      }
      if ($("#cboRiesgo").val() == '') {
          fnMensaje("warning", 'Seleccione un Nivel de Riesgo')
          return false
      }
      
      return true
}
function ValidarSesion(){
    if ($("#cboTipo").val() == '') {
          fnMensaje("warning", 'Seleccione un tipo de Sesión')
          return false
    }
  
         if ($("#dtpFecha").val() == '') {
              fnMensaje("warning", 'Seleccione una fecha')
              return false
          }
           if ($("#cboHoraD").val() == '0') {
              fnMensaje("warning", 'Seleccione la hora de inicio de la sesión')
              return false
          }
          if ($("#cboHoraA").val() == '0') {
              fnMensaje("warning", 'Seleccione la hora fin de la sesión ')
              return false
          }
           if (parseInt ($("#cboHoraA").val()) < parseInt( $("#cboHoraD").val()) ) {
              fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
              return false
          }else if ((parseInt ($("#cboHoraA").val()) == parseInt( $("#cboHoraD").val())) && (parseInt ($("#cboMinutoA").val()) < parseInt( $("#cboMinutoD").val()) )){
           fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
              return false
          }
    
     
      
    //var replaced = str.replace(/ /g, '+');
     if ($("#lblDescripcion").val().replace(/ /g, '') == '') {
          fnMensaje("warning", 'Es necesario una breve descripción')
          return false
      }
    
    
     
      return true
}
 function fnAsistencias(tua) {
   
   $("#tdP").html("Presente");
   $("#tdF").html("Falta");
    var arr = fAsistencias(1, tua,'0','ALU',$('#cboCicloAcad').val());
    var n = arr.length;
    var tb = "";
    for (i = 0; i < n; i++) {  
        var colorA='';
       
        if (arr[i].sem =="V"){
            colorA="#6fd64b";
        }else if (arr[i].sem =="R"){
            colorA="#d9534f";
        }else if (arr[i].sem =="A"){
            colorA="#f1e019";
        }
          
          tb += '<tr>';
          tb += '<td style="text-align:center">' + (i + 1) + '</td>';
          tb += '<td>' + arr[i].nombre + '</td>';
          tb += '<td>' + arr[i].docente + '</td>';
          tb += '<td style="text-align:center">' + arr[i].p + '</td>';
          tb += '<td style="text-align:center">' + arr[i].f + '</td>';
          tb += '<td style="text-align:center">' + arr[i].t + '</td>';
          tb += '<td style="text-align:center">' + arr[i].por + '% </td>';
          tb += '<td style="text-align:center">' + arr[i].v + '</td>';
          tb += '<td style="text-align:center"><button type="button" id="btnN" name="btnN" class="btn btn-md btn-info" style="background-color:'+ colorA + ';" >' + arr[i].sem + '</button></td>';
                        
          //tb += '<td style="text-align:center">' + arr[i].sem + '</td>';
          tb += '</tr>';
      }

      fnDestroyDataTableDetalle('tAsistencias');
      $('#tbAsistencias').html(tb);
      fnResetDataTableBasicDetalle('tAsistencias', 0, 'asc');  

}
function fnNotas(tua) {
   
   $("#tdP").html("Aprobado");
   $("#tdF").html("Desaprobado");
    var arr = fNotas(1, tua,'0','ALU',$('#cboCicloAcad').val());
    var n = arr.length;
    var tb = "";
    for (i = 0; i < n; i++) {  
        var colorA='';
       
        if (arr[i].sem =="V"){
            colorA="#6fd64b";
        }else if (arr[i].sem =="R"){
            colorA="#d9534f";
        }else if (arr[i].sem =="A"){
            colorA="#f1e019";
        }
          
          tb += '<tr>';
          tb += '<td style="text-align:center">' + (i + 1) + '</td>';
          tb += '<td>' + arr[i].nombre + '</td>';
          tb += '<td>' + arr[i].docente + '</td>';
          tb += '<td style="text-align:center">' + arr[i].a + '</td>';
          tb += '<td style="text-align:center">' + arr[i].d + '</td>';
          tb += '<td style="text-align:center">' + arr[i].t + '</td>';
          tb += '<td style="text-align:center">' + arr[i].por + '% </td>';
          tb += '<td style="text-align:center">' + arr[i].v + '</td>';
          tb += '<td style="text-align:center"><button type="button" id="btnN" name="btnN" class="btn btn-md btn-info" style="background-color:'+ colorA + ';" >' + arr[i].sem + '</button></td>';
                        
          //tb += '<td style="text-align:center">' + arr[i].sem + '</td>';
          tb += '</tr>';
      }

      fnDestroyDataTableDetalle('tAsistencias');
      $('#tbAsistencias').html(tb);
      fnResetDataTableBasicDetalle('tAsistencias', 0, 'asc');  

}
function fnSesiones(cod) {
 rpta = fnvalidaSession()
 if (rpta == true) {
     var arr = fSesiones(1, '', $('#cboCicloAcad').val(), 'LTUA',cod);
     var n = arr.length;
     var tb = "";
     
     for (i = 1; i < n; i++) {

         tb += '<tr>';
         tb += '<td style="text-align:center">' + (i) + '</td>';
         tb += '<td>' + arr[i].cDTis + '</td>';
         tb += '<td style="text-align:center">' + arr[i].cFecha + '</td>';
         tb += '<td style="text-align:center">' + arr[i].cHini + ' - ' + arr[i].cHfin + '</td>';
         tb += '<td>' + arr[i].cDstu + '</td>';
         tb += '<td style="text-align:center">' + arr[i].cAsis + '</td>';

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

function fnVerIndividual(){
    if ($("#divTodas").is(':visible')){
        
        fnSesionIndividual();
    }else{
        $('#divTodas').css("display", "block");
        $('#divTodas').css("visibility", "visible");   
        $('#divIndividual').css("display", "none");
        $('#divIndividual').css("visibility", "hidden"); 
        document.getElementById("lblInd").textContent = "Ver Tutoría Individual";
    }
}

function fnSesionIndividual() {
    rpta = fnvalidaSession()
    if (rpta == true) {
        $("form#frmDetalle input[id=action]").remove();
        $('#frmDetalle').append('<input type="hidden" id="action" name="action" value="' + ope.ind + '" />');
        var form = $("#frmDetalle").serializeArray();
        $("form#frmDetalle input[id=action]").remove();
        //        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionAlumno.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
            //                //console.log(data);
//                document.getElementById("lblFecha_A").textContent = data[0].cFecha;
//                document.getElementById("lblDescripcion_A").textContent = data[0].cDstu;
                
                var tb = '';
                var i = 0;
                var filas = data.length;
                //console.log(filas);
                if (filas >0){
                    for (i = 0; i < filas; i++) {
                        tb += '<tr>';
                        tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cFecha + '</td>';
                        tb += '<td>' + data[i].cActividad  + '</td>';
                        tb += '<td>' + data[i].cIncidencia  + '</td>';
                        tb += '<td>' + data[i].cComent  + '</td>';
                        tb += '<td>' + data[i].cAcc  + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cEstado  + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cFechaEj  + '</td>';
                        tb += '<td>' + data[i].cResultado  + '</td>';
                        tb += '<td style="text-align:center">' + data[i].cRiesgo  + '</td>';     
                        tb += '<td>' + data[i].cPros  + '</td>';                  
                        tb += '</tr>';
                    }
                    fnDestroyDataTableDetalle('tIndividual');
                    $('#tbIndividual').html(tb);
                    fnResetDataTableBasicDetalle('tIndividual', 0, 'asc');
                    
                    $('#divIndividual').css("display", "block");
                    $('#divIndividual').css("visibility", "visible");
                    $('#divTodas').css("display", "none");
                    $('#divTodas').css("visibility", "hidden"); 
                    
                    document.getElementById("lblInd").textContent = "Ver todas";
                }
                
 
                
            },
            error: function(result) {
            }
        });
    } else {
        window.location.href = rpta
    }
}