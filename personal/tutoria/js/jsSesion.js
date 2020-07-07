var lstTut;
var codTut;
var titulo;
var lstTut1;
var codTut1;
var titulo1;
var table,table2,InfoM;
var rows_selected = [];
var chk_dias = [];
var alumnos = [];
var dias = [];
var chk_presente = [];
var presente = [];
var sesiones=[];
var problemas = [];
var chk_problemas=[];
var max_descripcion = 500;
$(document).ready(function() {
    //fnLoading(true);
        var dt = fnCreateDataTableBasic('tSesiones', 0, 'asc');
        var dt2 = fnCreateDataTableBasic('tTutoradosA', 0, 'asc');
        var dt3 = fnCreateDataTableBasic('tTutoradosM', 0, 'asc');
        var dtT = fnCreateDataTableBasic('tTutorados', 2, 'asc');
        
        
        table2 = $('#tTutoradosA').DataTable();
        table = $('#tTutorados').DataTable();

    ope = fnOperacion(1);
    //////console.log(ope);
    rpta = fnvalidaSession()
    if (rpta == true) {
        var d = new Date();
        $("#cboMes").val(d.getMonth()+1);
        fnCicloAcad('#cboCicloAcad');
        $("#chkUna").change(function() {
            chkSesiones();
        });
        $("#chkVarias").change(function() {
            chkSesiones();
        });
        $("#chkPara").change(function() {
            chkTutorado();
        });
        $("#chkElegir").change(function() {
            chkTutorado();
        });
        $("#cboCicloAcad").change(function() {
            fnTutor();
            fnEscuela('#cboCarreraLista');  // HCano 14-05-18 
        });
        
        /* // HCano 14-05-18 */
        $("#cboCarreraLista").change(function() {
            fnBuscarTutorados();
        });
        // HCano 14-05-18 
        
        $("#cboTutor").change(function() {
            fnCategoria ();
            $("#cboCategoria").trigger('change');
        });
        $("#cboCategoria").change(function() {
           fnEscuela('#cboCarrera');
            $("#cboCarrera").trigger('change');
        });
        
         $("#cboTutorB").change(function() {
            fnBuscarTutorados();
        });
        
        $("#cboCarrera").change(function() {
            fnCicloIng();
            $("#cboIng").trigger('change');        
            if ($("#cboTipo").find('option:selected').attr("opc") == "2" ){                
                fnCursos();
            }   
        });
        $("#cboCarreraM").change(function() {
            fnCicloIng();
            $("#cboIng").trigger('change');        
            if ($("#cboTipoM").find('option:selected').attr("opc") == "2" ){      
                fnCursos2();
            }   
        });     
        $("#cboIng").change(function() {
             fnBuscarAlumnos();
        });
        
        $("#cboTipo").change(function() {
            if ($("#cboTipo").find('option:selected').attr("opc") == "2") {
                $('#divCurso').css("visibility", "visible");
                $('#divCurso').css("display", "block");
                $('#lichkVarias').css("visibility", "visible");
                $('#lichkVarias').css("display", "block");     
                fnCursos();
            } else if ($("#cboTipo").find('option:selected').attr("opc") == "1"){
                $('#divPara').css("visibility", "visible");
                $('#divPara').css("display", "block"); 
                $('#divTutorados').css("visibility", "hidden");
                $('#divTutorados').css("display", "none");     
                $('#lichkVarias').css("visibility", "hidden");
                $('#lichkVarias').css("display", "none");   
                $('#divCurso').css("visibility", "hidden");
                $('#divCurso').css("display", "none");   
                         
                fnAutoCTutores();
            }else{
                $('#divPara').css("visibility", "hidden");
                $('#divPara').css("display", "none");
                $('#divTutorados').css("visibility", "visible");
                $('#divTutorados').css("display", "block");
                $('#divCurso').css("visibility", "hidden");
                $('#divCurso').css("display", "none"); 
                $('#lichkVarias').css("visibility", "visible");
                $('#lichkVarias').css("display", "block");      
                $('#cboCurso').val('');        
            }
           
        });
        $("#cboRango").change(function() {
            fnRango();
        });
        $("#chkPara").prop('checked', true)
        $("#chkUna").trigger("change");
        $("#chkPara").trigger("change");
        fnTipoSesion();
        fnHora();
        fnMinuto();
        fnSemana();
        fnRango();
        $('#mdRegistro').on('show.bs.modal', function(event) {
            //        var button = $(event.relatedTarget) // Botón que activó el modal
            //        //        alert('--')
            //        if (button.attr("id") == "btnAgregar") {
                        $('#hdcod').remove();
                        $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="0" />');
            //            Limpiar();
            //        } else if (button.attr("id") == "btnE") {
            //            $('#hdcod').remove();
            //            $('#frmRegistro').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
            //            fnCicloAcad('#cboCicloAcadM')
            //            Edit()
            //        }
            //    })
            //    $('#mdEliminar').on('show.bs.modal', function(event) {
            //        var button = $(event.relatedTarget) // Botón que activó el modal
            //        $('#hdcod').remove();
            //        $('#frmEliminar').append('<input type="hidden" id="hdcod" name="hdcod" value="' + button.attr("hdc") + '" />');
        })
        $('#mdAsistencia').on('show.bs.modal', function(event) {
            var button = $(event.relatedTarget) // Botón que activó el modal
            $('#hdcodS').remove();
            $('#frmAsistencia').append('<input type="hidden" id="hdcodS" name="hdcodS" value="' + button.attr("hdc") +'" />');
            fnEdit($('#hdcodS').val());
            chk_presente=[];
            presente =[];
        })
//         $('#mdIndividual').on('show.bs.modal', function(event) {
//            var button = $(event.relatedTarget) // Botón que activó el modal
//            $('#hdcodS').remove();
//            $('#frmIndividual').append('<input type="hidden" id="hdcodS" name="hdcodS" value="' + button.attr("hdc") +'" />');
//            fnEdit($('#hdcodS').val());
//            chk_presente=[];
//            presente =[];
//        })

        $('#tbTutorados').on('click', 'input[type="checkbox"]', function(e) {
            var $row = $(this).closest('tr');
            var data = table.row($row).data();
            var rowId = data[0];
//            var index = $.inArray(rowId, rows_selected);
            var hdcSelect =$(this).attr("hdc");
            var index2 = $.inArray(hdcSelect, rows_selected );
//            console.log(rows_selected);
//            console.log(hdcSelect);
//            console.log(index2);
//            console.log(data);
//            console.log(this.checked);
            if (this.checked && index2 === -1) {
                //rows_selected.push(rowId);
                rows_selected.push(hdcSelect);
                var hdc = $(this).attr("hdc");
                alumnos.push({
                    hdc: hdc
                });
            } else if (!this.checked && index2 !== -1) {
                rows_selected.splice(index2, 1);
                alumnos.splice(index2, 1);
            }

            if (this.checked) {
                $row.addClass('selected');
            } else {
                $row.removeClass('selected');
            }
            updateDataTableSelectAllCtrl(table);
            e.stopPropagation();
//            console.log(alumnos);
//            console.log(rows_selected);
        });

        // Handle click on table cells with checkboxes
        $('#tTutorados').on('click', 'tbody td, thead th:first-child', function(e) {
            $(this).parent().find('input[type="checkbox"]').trigger('click');
        });

        // Handle click on "Select all" control
        $('thead input[id="chkSelectAll"]', table.table().container()).on('click', function(e) {
            if (this.checked) {
                $('#tTutorados tbody input[type="checkbox"]:not(:checked)').trigger('click');
            } else {
                $('#tTutorados tbody input[type="checkbox"]:checked').trigger('click');
            }

            // Prevent click event from propagating to parent
            e.stopPropagation();
        });

        // Handle table draw event
        table.on('draw', function() {
            // Update state of "Select all" control
            updateDataTableSelectAllCtrl(table);
        });
        
        $('#divPrimero').on('click', 'input[type="checkbox"]', function(e) {
 
            var id = $(this).attr("dy");

            var index = $.inArray(id, dias);

            if (this.checked && index === -1) {
                var dy = $(this).attr("dy");
                dias.push(dy);
                chk_dias.push({
                    dy: dy
                });
            } else if (!this.checked && index !== -1) {
                dias.splice(index, 1);                
                chk_dias.splice(index, 1);
            }
            

        });
          $('#mdIndividual').on('click', 'input[class="chkP"]', function(e) {
 
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
        $('#tbTutoradosA').on('click', 'input[type="checkbox"]', function(e) {
            var $row = $(this).closest('tr');
            var data = table2.row($row).data();
            var rowId = data[0];
            var index = $.inArray(rowId, chk_presente );
            if (this.checked && index === -1) {
                chk_presente.push(rowId);
                var hdc = $(this).attr("hdc");
                presente.push({
                    sa: hdc
                });
            } else if (!this.checked && index !== -1) {
                chk_presente.splice(index, 1);
                presente.splice(index, 1);
            }
            ////console.log(chk_presente );
            ////console.log(presente );
//            if (this.checked) {
//                $row.addClass('selected');
//            } else {
//                $row.removeClass('selected');
//            }
            updateDataTableSelectAllCtrl2(table2);
            e.stopPropagation();
        });
        // Handle click on table cells with checkboxes
        $('#tTutoradosA').on('click', 'tbody td, thead input[type="checkbox"]', function(e) {
            $(this).parent().find('input[type="checkbox"]').trigger('click');
        });

        // Handle click on "Select all" control
        $('thead input[id="chkPall"]', table2.table().container()).on('click', function(e) {
            if (this.checked) {
                $('#tTutoradosA tbody input[type="checkbox"]:not(:checked)').trigger('click');
            } else {
                $('#tTutoradosA tbody input[type="checkbox"]:checked').trigger('click');
            }

            // Prevent click event from propagating to parent
            e.stopPropagation();
        });

         table2.on('draw', function() {
            // Update state of "Select all" control
            updateDataTableSelectAllCtrl2(table2);
        });
        
        $('#contador').html(max_descripcion);

        $('#lblDescripcion').keyup(function() {
            var chars = $(this).val().length;
            var diff = max_descripcion - chars;
            $('#contador').html(diff);   
        });
        $('#lblDescripcionM').keyup(function() {
            var chars = $(this).val().length;
            var diff = max_descripcion - chars;
            $('#contadorM').html(diff);   
        });

        

    } else {
        window.location.href = rpta
    }
    //fnLoading(false);

})
function fnModal() {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")

    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            $('#mdRegistro').modal('show');            
            //fnAutoCTutores();
            InicioAgregar();
            //LimpiarTutorado();
        }else{
            window.location.href = rpta
        }
    }
}
function fnModalModificar(cod,c) {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")

    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            sesiones =[];
            $('#hdcodS').remove();
            $('#c').remove();
            $('#frmModificar').append('<input type="hidden" id="hdcodS" name="hdcodS" value="' + cod+'" />');
            $('#frmModificar').append('<input type="hidden" id="c" name="c" value="' + c+'" />');
            $('#mdModificar').modal('show');           
            $('#divInfo').css("visibility", "hidden");
            fnVer();
            LimpiarModificar()           
            //////console.log(cod);
            //fnAutoCTutores();
            Edit(cod);
            fnAutoCPBuscarTutorados();
            //////console.log($('#divInfo').is(':visible'));
            //LimpiarTutorado();
           
        }else{
            window.location.href = rpta
        }
    }
}
function fnModalIndividual(cod){
     if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")

    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
                fnActividad ();
                fnEstado ();
                fnResultado ();
                fnNivelRiesgo ();
                fnTipoProblema ();
                $('#hdcodS').remove();
                $('#frmIndividual').append('<input type="hidden" id="hdcodS" name="hdcodS" value="' + cod +'" />');
                $('#mdIndividual').modal('show'); 
                fnEditI(cod);  
                ////console.log(cod);
        }else{
            window.location.href = rpta
        }
    }
}
function fnVer(){

if ($('#divInfo').is(':visible')){
    $('#divTutoradosM').css("visibility", "visible");
    $('#divTutoradosM').css("display", "block");
    $('#divInfo').css("visibility", "hidden");
    $('#divInfo').css("display", "none");
    document.getElementById("lblVerAlumnos").textContent = 'Ocultar';    
}else{
    $('#divTutoradosM').css("visibility", "hidden");
    $('#divTutoradosM').css("display", "none");
    $('#divInfo').css("visibility", "visible");
    $('#divInfo').css("display", "block");
    document.getElementById("lblVerAlumnos").textContent = 'Ver alumnos';  
}

    
}
function chkTutorado() {

    var chk = $('#chkPara');
    if (chk.is(':checked')) {
        $('#inputPara').prop("disabled", false)
        $('#divElegir').css("visibility", "hidden");
        $('#divElegir').css("display", "none");
    } else {
        $('#inputPara').prop("disabled", true)
        $('#divElegir').css("visibility", "visible");
        $('#divElegir').css("display", "block");
    }
    $('#inputPara').val('');
    codTut='';
    titulo = '';
    rows_selected = [];
    alumnos = [];
    $('#tTutorados tbody input[type="checkbox"]:checked').trigger('click');
}
function fnRango() {

    var val = $('#cboRango').val();
    if (val=="M") {    
        $('#divMes').css("visibility", "visible");
        $('#divMes').css("display", "block");
        $('div[name=divDia]').css("visibility", "hidden");
        $('div[name=divDia]').css("display", "none");
    } else if (val=="D") {
        $('#divMes').css("visibility", "hidden");
        $('#divMes').css("display", "none");
        $('div[name=divDia]').css("visibility", "visible");
        $('div[name=divDia]').css("display", "block");
    }else{
        $('#divMes').css("visibility", "hidden");
        $('#divMes').css("display", "none");
        $('div[name=divDia]').css("visibility", "hidden");
        $('div[name=divDia]').css("display", "none");
    }
   
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
    f.selectedIndex =1;
}
function fnEscuela(cboId) {

    var arr = fAux(1, "ES1",$("#cboCicloAcad").val() ,$("#cboTutor").val(),'',$("#cboIng").val(),$("#cboCategoria").val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $(cboId).html(str);   
    var f = document.getElementById('cboCarrera');
    f.selectedIndex = 0; 
}
function fnEscuela2() {

    var arr = fAux(1, "ES1",$("#cboCicloAcad").val() ,$("#cboTutorB").val(),'','','');
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboCarreraM").html(str);   
}
function LimpiarIndividual() {
    
    document.getElementById("lblIndividual").textContent = '';
    $('#cboActividad').val('');
    $('#cboEstado').val('');
    $('#cboResultado').val('');
    $('#cboRiesgo').val('');
    $('#txtIncidencia').val('');
    $('#txtComentario').val('');
    $('#txtAccionF').val('');
    $('#dtpFecha').val('');    
    $('#dtpFecha').datepicker('setDate','');
    $('#hdcodS').val('');
        
}
function LimpiarTutorado() {
    
    $('#cboTipo').val('');
    $('#tTutorados tbody input[type="checkbox"]:checked').trigger('click');
    $('#divPrimero').css("visibility", "visible");
    $('#divPrimero').css("display", "block");
    $('#pnlNueva').css("visibility", "hidden");
    $('#pnlNueva').css("display", "none");
    $('#inputPara').val('');
    codTut ="";
    titulo ="";
        
}
function LimpiarSesion() {
    
    $('#dtpFecha').val('');
    $('#dtpDesde').val('');
    $('#dtpHasta').val('');
    $('#cboHoraD').val('0');
    $('#cboMinutoD').val('0');
    $('#cboHoraA').val('0');
    $('#cboMinutoA').val('0');
    $('#cboHoraDV').val('0');
    $('#cboMinutoDV').val('0');
    $('#cboHoraAV').val('0');
    $('#cboMinutoAV').val('0');
    $('#cboSemana').val('1');
    $('#lblDescripcion').val('');
    $("#chkUna").prop('checked', true);
    $("#chkUna").trigger('change');    
    $('input[name=chkDia]').prop('checked',false);
    fnBuscarTutor();     
    fnCategoria('#cboCategoria');
    fnEscuela('#cboCarrera');
    fnCicloIng();
//    fnBuscarAlumnos();

}
function LimpiarModificar() {
    
    $('#dtpFecha').val('');
    $('#cboHoraD').val('0');
    $('#cboMinutoD').val('0');
    $('#cboHoraA').val('0');
    $('#cboMinutoA').val('0');
    $('#lblDescripcion').val('');
    $('#cboTipoM').val('');      
    $('#busPoint').val('');
    fnDestroyDataTableDetalle10('tTutoradosM');
    $('#tbTutoradosM').html('');
    fnResetDataTableBasic10('tTutoradosM', 0, 'asc');
        var lstTut1='';
        var codTut1='';
        var titulo1='';
        

}
function chkSesiones() {

//    ////console.log('chkTutorado');
    var chk = $('#chkUna');
    if (chk.is(':checked')) {
        $('#divUna').css("visibility", "visible");
        $('#divUna').css("display", "block");
        $('#divVarias').css("visibility", "hidden");
        $('#divVarias').css("display", "none");
    } else {
        $('#divUna').css("display", "none");
        $('#divUna').css("visibility", "hidden");
        $('#divVarias').css("visibility", "visible");
        $('#divVarias').css("display", "block");
        
    }
    dias=[];
    chk_dias =[];
    var now = new Date();
    var today = now.getDate()  + '/' + (now.getMonth() + 1) + '/' + now.getFullYear();
    $('#dtpFecha').val(today);    
    $('#dtpFecha').datepicker('setDate',today);
    $('#dtpDesde').val(today);    
    $('#dtpDesde').datepicker('setDate',today);
    $('#dtpHasta').val('');
    $('#cboHoraD').val('0');
    $('#cboMinutoD').val('0');
    $('#cboHoraA').val('0');
    $('#cboMinutoA').val('0');
    $('#cboHoraDV').val('0');
    $('#cboMinutoDV').val('0');
    $('#cboHoraAV').val('0');
    $('#cboMinutoAV').val('0');
    $('#cboSemana').val('1');    
    $('input[name=chkDia]').prop('checked',false);
}
function InicioAgregar() {
    sesiones =[];
    $('#divFooter').css("visibility", "hidden");
    $('#divFooter').css("display", "none");
    $('#divCurso').css("visibility", "hidden");
    $('#divCurso').css("display", "none");  
    $('#divPara').css("visibility", "hidden");
    $('#divPara').css("display", "none");  
    
    $('#mtRegistro').html("Nueva Sesión");
    LimpiarTutorado();
    LimpiarSesion();
}
function ValidarSesionM(){
      if ($("#cboTipoM").val() == '') {
          fnMensaje("warning", 'Seleccione un tipo de Sesión')
          return false
      }
      if ($("#dtpFechaM").val() == '') {
          fnMensaje("warning", 'Seleccione una fecha')
          return false
      }
      if ($("#cboHoraDM").val() == '0') {
          fnMensaje("warning", 'Seleccione la hora de inicio de la sesión')
          return false
      }
      if ($("#cboHoraAM").val() == '0') {
          fnMensaje("warning", 'Seleccione la hora fin de la sesión ')
          return false
      }
      if (parseInt ($("#cboHoraAM").val()) < parseInt( $("#cboHoraDM").val()) ) {
          fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
          return false
      }else if ((parseInt ($("#cboHoraAM").val()) == parseInt( $("#cboHoraDM").val())) && (parseInt ($("#cboMinutoAM").val()) < parseInt( $("#cboMinutoDM").val()) )){
       fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
          return false
      }
      if ($("#lblDescripcionM").val().replace(/ /g, '') == '') {
          fnMensaje("warning", 'Es necesario una breve descripción')
          return false
      }
      return true
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
//      if ($("#dtpFechaF").val() !== '' && $("#dtpFechaF").val() < $("#dtpFecha").val()) {
//          fnMensaje("warning", 'La fecha de la próxima cita debe ser mayor a la fecha de esta Sesión')
//          return false
//      }
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
function ValidarPrimero(){
      if ($("#cboTipo").val() == '') {
          fnMensaje("warning", 'Seleccione un tipo de Sesión')
          return false
      }
      if (alumnos.length ==0) {
          fnMensaje("warning", 'Seleccione al menos un alumno')
          return false
      }
     
      return true
}
function ValidarSegundo(){
 var chk = $('#chkUna');
    if ($("#cboTipo").val() == '') {
          fnMensaje("warning", 'Seleccione un tipo de Sesión')
          return false
    }
    if (chk.is(':checked')) {
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
    
      
    }else{
    
        var x = $("#dtpDesde").val().split('/');
        var y = $("#dtpHasta").val().split('/');
        fecha_desde = x[1] + '/' + x[0] + '/' + x[2];
        fecha_hasta = y[1] + '/' + y[0] + '/' + y[2];
        
         if ($("#dtpDesde").val() == '') {
          fnMensaje("warning", 'Seleccione una fecha desde la que se creerán las Sesiones')
              return false
          } 
          if ($("#dtpHasta").val() == '') {
              fnMensaje("warning", 'Seleccione una fecha hasta la que se creerán las Sesiones')
              return false
          }
          if (new Date(fecha_desde) > new Date(fecha_hasta ) ){
              fnMensaje("warning", 'La fecha desde que se crearán las sesiones debe ser menor a la Fecha Límite')
              return false
          }
           if ($("#cboHoraDV").val() == '0') {
              fnMensaje("warning", 'Seleccione la hora de inicio de la sesión')
              return false
          }
          if ($("#cboHoraAV").val() == '0') {
              fnMensaje("warning", 'Seleccione la hora fin de la sesión ')
              return false
          }
           if (parseInt ($("#cboHoraAV").val()) < parseInt( $("#cboHoraDV").val()) ) {
              fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
              return false
          }else if ((parseInt ($("#cboHoraAV").val()) == parseInt( $("#cboHoraDV").val())) && (parseInt ($("#cboMinutoAV").val()) < parseInt( $("#cboMinutoDV").val()) )){
           fnMensaje("warning", 'La hora de inicio debe ser menor a la hora fin ')
              return false
          }
          if (dias.length ==0) {
              fnMensaje("warning", 'Seleccione al menos un día')
              return false
          }
    }
    //var replaced = str.replace(/ /g, '+');
     if ($("#lblDescripcion").val().replace(/ /g, '') == '') {
          fnMensaje("warning", 'Es necesario una breve descripción')
          return false
      }
      
//      if ($("#cboTipo").find('option:selected').attr("opc") == "1"){
//          if (alumnos.length ==0) {
//              fnMensaje("warning", 'Seleccione al menos un alumno')
//              return false
//          }
//      }
      
     if ($("#cboTipo").find('option:selected').attr("opc") == "2"){
          if ($("#cboCurso").val() == '') {
              fnMensaje("warning", 'Seleccione un Curso')
              return false
            }
      }
     if ($("#cboCarrera").val() == '') {
          fnMensaje("warning", 'Seleccione una Carrera Profesional')
          return false
     }
     
      return true
}
function fnSiguiente() {
    if (ValidarSegundo() == true) {
        fnGuardar();
//        $('#divPrimero').css("visibility", "hidden");
//        $('#divPrimero').css("display", "none");
//        $('#pnlNueva').css("visibility", "visible");
//        $('#pnlNueva').css("display", "block");
//        $('#divFooter').css("visibility", "visible");
//        $('#divFooter').css("display", "block");
//        $("#chkUna").prop('checked', true);
//        $("#chkUna").trigger('change');
        //////console.log(rows_selected);
        ////console.log(alumnos);
        ////console.log(chk_dias);
    }
    
}
function fnRegresar() {
    $('#divPrimero').css("visibility", "visible");
    $('#divPrimero').css("display", "block");
    $('#pnlNueva').css("visibility", "hidden");
    $('#pnlNueva').css("display", "none");
    $('#divFooter').css("visibility", "hidden");
    $('#divFooter').css("display", "none");
    //////console.log(rows_selected);
}
function fnBuscarTutorados(){
    var rpta= fnvalidaSession()
    if (rpta == true) {
        
        //var arr = fTutorados(1, $("#cboCicloAcad").val(), "","LT",$("#cboTutorB").val(),$("#cboCarrera").val()); // HCano 14-05-18
        var arr = fTutorados(1, $("#cboCicloAcad").val(), "","LT",$("#cboTutorB").val(),$("#cboCarreraLista").val()); // HCano 14-05-18
        var n = arr.length;
        var str = "";
        str += '<option value="" selected>-- Seleccione -- </option>';
        for (i = 0; i < n; i++) {
            str += '<option value="' + arr[i].cTA + '">' + arr[i].cAlumno + '</option>';
        }
        $("#cboAlumno").html(str);
    }else {
            window.location.href = rpta
    }
}
function fnAutoCPBuscarTutorados(){
    var rpta= fnvalidaSession()
    if (rpta == true) {
         var arr = fTutorados(1, $("#cboCicloAcad").val(), "","LT",$("#cboTutorB").val(),$("#cboCarreraM").val());
         var jsonString = arr;
            $('#busPoint').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.cAlumno;
                }),
                select: function(event, ui) {
                    var selectecItem = jsonString.filter(function(value) {
                        return value.cAlumno == ui.item.value;
                    });
                    codTut1 = selectecItem[0].cTA;
                    titulo1 = selectecItem[0].cAlumno;
                    
                    $('#PanelEvento').hide("fade");
                    //alert("cod: " + selectecItem[0].cod + ", nombre: " + selectecItem[0].nombre);
                },
                minLength: 2,
                delay: 600
            });

            $('#busPoint').keyup(function() {
                var l = parseInt($(this).val().length);
                //if (l == 0) {
                codTut1 = "";
                titulo1 = "";
                //}
            });
    }else {
            window.location.href = rpta
    }
}
function fnEditI(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
//        fnLoading(true)
        $("form#frmIndividual input[id=action]").remove();
        $("form#frmIndividual input[id=hdcodSE]").remove();
        $("form#frmIndividual input[id=cboCiclo]").remove();
        $('#frmIndividual').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmIndividual').append('<input type="hidden" id="hdcodSE" name="hdcodSE" value="' + $("#hdcodS").val() + '" />');
        $('#frmIndividual').append('<input type="hidden" id="cboCiclo" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');
        var form = $("#frmIndividual").serializeArray();
        $("form#frmIndividual input[id=action]").remove();
        $("form#frmIndividual input[id=hdcodSE]").remove();
        $("form#frmIndividual input[id=cboCiclo]").remove();
        //        ////console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionAlumno.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
            //                ////console.log(data);
                //codperM = data[0].cPer
                var filas = data.length;
                if (filas>0){
                    if (data[0].cPro !==''){
                        var prob = data[1].cPro.split("/");
                    
                        for (i = 0; i < prob.length ; i++) {
                            $("#mdIndividual").find("input[d='" + prob[i] + "']").trigger('click');
                        }
                       // ////console.log(prob);
                    }
                       //////console.log(prob);              
                    document.getElementById("lblIndividual").textContent = data[1].cAlumno;
                    $('#cboActividad').val(data[1].cAct);
                    $('#cboEstado').val(data[1].cEtu);
                    $('#cboResultado').val(data[1].cRes);
                    $('#cboRiesgo').val(data[1].cNri);
                    $('#txtIncidencia').val(data[1].cIncidencia);
                    $('#txtComentario').val(data[1].cComent);
                    $('#txtAccion').val(data[1].cAcc);
                    $('#dtpFechaF').val(data[1].cFechaEj);
                    
                    $('#hdcodS').remove();
                    $('#frmIndividual').append('<input type="hidden" id="hdcodS" name="hdcodS" value="' + data[1].cSA +'" />');
                }
                               
            },
            error: function(result) {
            }
        });
    } else {
        window.location.href = rpta
    }
}
function fnEdit(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        fnLoading(true)
        $("form#frmCiclo input[id=action]").remove();
        $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmCiclo').append('<input type="hidden" id="hdcodSE" name="hdcodSE" value="' + $("#hdcodS").val() + '" />');
        var form = $("#frmCiclo").serializeArray();
        $("form#frmCiclo input[id=action]").remove();
        $("form#frmCiclo input[id=hdcodSE]").remove();
        //        ////console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionAlumno.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
            //                ////console.log(data);
                //codperM = data[0].cPer
                var desc=data[0].cDstu;
                chk_presente =[];
                presente =[];
                
                if (data[0].cCur!=""){
                    desc =data[0].cDstu + ' - ' + data[0].cCur;
                }
                document.getElementById("lblFecha_A").textContent = data[0].cFecha;
                document.getElementById("lblDescripcion_A").textContent = desc;
                document.getElementById("lblCarrera_A").textContent = data[0].cCarr;
                
                var tb = '';
                var i = 0;
                var filas = data.length;
                for (i = 1; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + (i) + "" + '</td>';
                    tb += '<td>' + data[i].cAlumno + '</td>';
                    if (data[i].cTiene_asistencia==1 && data[i].cAsistencia_stu=='P'){
                        tb += ' <td><input type="checkbox" id="chkSelect" hdc="' + data[i].cSA + '" checked></td>';
                        
                        chk_presente.push(i.toString() );
                        presente.push({sa:data[i].cSA});
                    }else{
                        tb += ' <td><input type="checkbox" id="chkSelect" hdc="' + data[i].cSA + '" ></td>';
                    }

                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle10('tTutoradosA');
                $('#tbTutoradosA').html(tb);
                fnResetDataTableBasic10('tTutoradosA', 0, 'asc');
 
                table2 = $('#tTutoradosA').DataTable();
                tabla = $('#tbTutoradosA').html();
//                ////console.log(chk_presente);
//                ////console.log(presente);
                
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
function Edit(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //fnLoading(true)
        $("form#frmCiclo input[id=action]").remove();
        $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
        $('#frmCiclo').append('<input type="hidden" id="hdcodSE" name="hdcodSE" value="' + $("#hdcodS").val() + '" />');
        var form = $("#frmCiclo").serializeArray();
        $("form#frmCiclo input[id=action]").remove();
        $("form#frmCiclo input[id=hdcodSE]").remove();
        //        ////console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionAlumno.aspx",
            data: form,
            dataType: "json",
            cache: false,
            success: function(data) {
            //                ////console.log(data);
                //codperM = data[0].cPer
                
            fnEscuela2();
            $("#dtpFechaM").val(data[0].cFecha);
            $("#lblDescripcionM").val(data[0].cDstu);            
            var diff = max_descripcion - $("#lblDescripcionM").val().length;
            $('#contadorM').html(diff);  
            
            $("#cboTipoM").val(data[0].cTis);                   
            $("#cboCarreraM").val(data[0].ccCarr);            
            fnCursos2();
            if ($("#cboTipoM").find('option:selected').attr("opc") == "2" ){
                $("#cboCursoM").val(data[0].ccCur);     
                $("#divCursoM").css('display','block') ;     
            }else{ 
                $("#divCursoM").css('display','none') ;             
            }
            var Hini = parseInt(data[0].cHini.split(":")[0]);
            var Mini = parseInt(data[0].cHini.split(":")[1]);
            var Hfin = parseInt(data[0].cHfin.split(":")[0]);
            var Mfin = parseInt(data[0].cHfin.split(":")[1]);
            $("#cboHoraDM").val(Hini);
            $("#cboMinutoDM").val(Mini);
            $("#cboHoraAM").val(Hfin);
            $("#cboMinutoAM").val(Mfin);
            
            InfoM =$("#divInfo").html();
            
            //////console.log(InfoM);
                var tb = '';
                var i = 0;
                var filas = data.length;
                if (filas >1){
                    for (i = 1; i < filas; i++) {
                    tb += '<tr>';
                    tb += '<td style="text-align:center">' + i  + '</td>';
                    tb += '<td>' + data[i].cAlumno + '</td>';
                    tb += '<td style="text-align:center"><button type="button" id="btnDI" name="btnDI" class="btn btn-red btn-xs" title="Eliminar" onclick="fnDeleteItem(\'' + data[i].cSA + '\')" ><i class="ion-android-delete"></i></button></td>';
                    tb += '</tr>';
                }
                fnDestroyDataTableDetalle10('tTutoradosM');
                $('#tbTutoradosM').html(tb);
                fnResetDataTableBasic10('tTutoradosM', 0, 'asc');
                }
                
 
               // fnLoading(false);
            },
            error: function(result) {
                //fnLoading(false)
            }
        });
    } else {
        window.location.href = rpta
    }
}
function fnDeleteItem(cod) {
    var msje ='';
    if (parseInt($("#c").val())==1){
        msje ='¿Desea Eliminar este alumno de la Sesión?, al eliminarlo se eliminará toda la Sesión.';
    }else{
        msje ='¿Desea Eliminar este alumno de la Sesión?';
    }
    aDataR = {
        cod: cod,
        mensaje: msje
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminarItem', aDataR.cod);
    //    fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}
function fnEliminarItem(cod){
     rpta = fnvalidaSession()
    if (rpta == true) {
        //        fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionAlumno.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
//                ////console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    Edit($("hdcodS").val());
                    var c =parseInt($("#c").val())
                    $("#c").val((c-1).toString());
                    fnBuscarSesiones (false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function(result) {
                //            ////console.log(result)
                fnMensaje("warning", result)
            }
        });
        //        fnLoading(false)
    } else {
        window.location.href = rpta
    }
}
//function Edit(cod) {
//    rpta = fnvalidaSession()
//    if (rpta == true) {
//        fnLoading(true)
//        $("form#frmModificar input[id=action]").remove();
//        $("form#frmModificar input[id=hdcodSE]").remove();
//        $('#frmModificar').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
//        $('#frmModificar').append('<input type="hidden" id="hdcodSE" name="hdcodSE" value="' + $('#hdcodS').val() + '" />');
//        var form = $("#frmModificar").serializeArray();
////        $("form#frmModificar input[id=action]").remove();
////        $("form#frmModificar input[id=hdcodSE]").remove();
//        //        ////console.log(form);
//        $.ajax({
//            type: "POST",
//            url: "../DataJson/tutoria/SesionAlumno.aspx",
//            data: form,
//            dataType: "json",
//            cache: false,
//            success: function(data) {
//            $("#dtpFechaM").val(data[0].cFecha);
//            $("#lblDescripcionM").val(data[0].cDstu);
//            $("#cboTipoM").val(data[0].cTis);
//            var Hini = parseInt(data[0].cHini.split(":")[0]);
//            var Mini = parseInt(data[0].cHini.split(":")[1]);
//            var Hfin = parseInt(data[0].cHfin.split(":")[0]);
//            var Mfin = parseInt(data[0].cHfin.split(":")[1]);
//            $("#cboHoraDM").val(Hini);
//            $("#cboMinutoDM").val(Mini);
//            $("#cboHoraAM").val(Hfin);
//            $("#cboMinutoAM").val(Mfin);
//                              
//                fnLoading(false);
//            },
//            error: function(result) {
//                fnLoading(false)
//            }
//        });
//    } else {
//        window.location.href = rpta
//    }
//}
function fnGuardarAsistencia(tipo){
if ($("#hdcodS").val() == "") {
        fnMensaje("warning", "Debe Seleccionar una Sesión")
}else{

    rpta = fnvalidaSession()
        if (rpta == true) {
               fnLoading(true)
         if (tipo==1){
             $('#action').remove();
               $('#tipoA').remove();
              $('#frmAsistencia').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
              $('#frmAsistencia').append('<input type="hidden" id="tipoA" name="tipo" value="'+tipo +'" />');
              var form = $("#frmAsistencia").serializeArray();              
              var array = JSON.stringify(presente); 
              form.push({"name":"array","value":array});
//              ////console.log(presente);
//              ////console.log(chk_presente);
              
              $('#action').remove();
               $('#tipoA').remove();
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/SesionAlumno.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        ////console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcod').val(0);
                          //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                          //LimpiarEval()
                          fnBuscarSesiones(false);
                          fnMensaje("success", data[0].msje)

//                          fnDestroyDataTableDetalle('tEval');
//                          $('#tbEval').html('');
//                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                          $('#mdAsistencia').modal('hide');
                          
                          
                      } else {
                          fnMensaje("warning", data[0].msje)
                      }
                  },
                  error: function(result) {
                      //            ////console.log(result)
                      fnMensaje("warning", result)
                  }
              });
         }else{
              $('#action').remove();
              $('#tipoA').remove();
              $('#frmIndividual').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
              $('#frmIndividual').append('<input type="hidden" id="tipoA" name="tipo" value="'+tipo +'" />');
              var form = $("#frmIndividual").serializeArray();              
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
                      //                        ////console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcod').val(0);
                           LimpiarIndividual();
                          $('#mdIndividual').modal('hide');                          
                          fnBuscarSesiones(false);
                          problemas =[];
                          chk_problemas =[];
                          fnMensaje("success", data[0].msje)
                          
                      } else {
                          fnMensaje("warning", data[0].msje)
                      }
                  },
                  error: function(result) {
                      //            ////console.log(result)
                      fnMensaje("warning", result)
                  }
              });
         }
                
               
          
          fnLoading(false)
        }else {
                window.location.href = rpta
        }
}
}

function fnBuscarSesiones(){
if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        if ($("#cboTutorB").val() == "") {
            fnMensaje("warning", "Debe Seleccionar un tutor.")
        } else {
    
            rpta = fnvalidaSession()
            if (rpta == true) {
                //if (sw) { fnLoading(true); }
                //    fnLoading(true)
                $('#frmCiclo').append('<input type="hidden" id="action" name="action" value="' + ope.lst + '" />');
                $('#frmCiclo').append('<input type="hidden" id="tipo" name="tipo" value="LS" />');
                $('#frmCiclo').append('<input type="hidden" id="k" name="k" value="' +$("#cboTutorB").val() + '" />');
                var form = $("#frmCiclo").serializeArray();
                
                $("form#frmCiclo input[id=action]").remove();
                $("form#frmCiclo input[id=tipo]").remove();
                $("form#frmCiclo input[id=k]").remove();
                ////console.log(form);

                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/SesionTutor.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                    $("form#frmCiclo input[id=action]").remove();
                    $("form#frmCiclo input[id=tipo]").remove();
                    $("form#frmCiclo input[id=k]").remove();
                        //////console.log(data);
                        
                        var tb = '';
                        var i = 0;
                        var filas = data.length;
                        for (i = 0; i < filas; i++) {
                            var m='';
                            tb += '<tr>';
                            tb += '<td style="text-align:center">' + (i+1)+ "" + '</td>';
                            tb += '<td style="text-align:center">' + data[i].cFecha + "" + '</td>';
                            tb += '<td>' + data[i].cDTis + '</td>';
                            tb += '<td>' + data[i].cDstu + '</td>';
                            tb += '<td style="text-align:center">' + data[i].cHini + ' - '+ data[i].cHfin + '</td>';
                            tb += '<td style="text-align:center">' + data[i].cTotal + '</td>';
                            if (data[i].cInd ==1){
                                m=' onclick="fnModalIndividual(\''+data[i].cStu +'\')"';
                            }else{
                                m='data-toggle="modal" data-target="#mdAsistencia"';
                            
                            }
                            if (data[i].cMod>0){
                                tb += '<td style="text-align:center"><button type="button" id="btnA" name="btnA" class="btn btn-sm btn-green"' +m +'" hdc="' + data[i].cStu  +'" title="Modificar Asistencia" ><i class="ion-android-hand"></i></button>';
                            }else{
                                 tb += '<td style="text-align:center"><button type="button" id="btnA" name="btnA" class="btn btn-sm btn-primary"' +m +'" hdc="' + data[i].cStu +  '" title="Iniciar Asistencia" ><i class="ion-android-hand"></i></button>';
                            }
                            tb += '<button type="button" id="btnE" name="btnE" class="btn btn-sm btn-info" onclick="fnModalModificar(\'' + data[i].cStu +'\',\'' + data[i].cTotal  +'\')"  hdc="' + data[i].cStu + '" title="Editar Sesión" ><i class="ion-edit"></i></button>';

                            tb += '<button type="button" id="btnD" name="btnD" class="btn btn-sm btn-red" onclick="fnDelete(\'' + data[i].cStu + '\')" hdc="' + data[i].cStu + '" title="Eliminar" ><i class="ion-android-delete"></i></button></td>';

                            tb += '</tr>';
                        }
                        fnDestroyDataTableDetalle10('tSesiones');
                        $('#tbSesiones').html(tb);
                        fnResetDataTableBasic10('tSesiones', 0, 'asc');
                        //if (sw) { fnLoading(false); }
                        //                    fnLoading(false);
                    },
                    error: function(result) {
                        //                    ////console.log(result)
                    }
                });
            } else {
                window.location.href = rpta
            }
        }
      }
}
function fnDelete(cod) {
    aDataR = {
        cod: cod,
        mensaje: '¿Desea Eliminar esta Sesión?'
    }
    fnMensajeConfirmarEliminar('top', aDataR.mensaje, 'fnEliminar', aDataR.cod);
    //    fnMensajeConfirmarEliminar('top', aDataR.mensaje + '[' + aDataR.registro + ']?', 'fnRetiro', aDataR.cdm, aDataR.cu);
}
function fnEliminar(cod) {
    rpta = fnvalidaSession()
    if (rpta == true) {
        //        fnLoading(true)
        $.ajax({
            type: "POST",
            url: "../DataJson/tutoria/SesionTutor.aspx",
            data: { "action": ope.eli, "hdcod": cod },
            dataType: "json",
            cache: false,
            async: false,
            success: function(data) {
//                ////console.log(data);
                if (data[0].rpta == 1) {
                    fnMensaje("success", data[0].msje)
                    fnBuscarSesiones(false);
                } else {
                    fnMensaje("warning", data[0].msje)
                }
            },
            error: function(result) {
                //            ////console.log(result)
                fnMensaje("warning", result)
            }
        });
        //        fnLoading(false)
    } else {
        window.location.href = rpta
    }
}
function fnAutoCTutores() {
    if ($("#cboTipo").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Tipo de Sesión.")
        LimpiarTutorado();
    } else{
        lstTut = fTutorados(1, $("#cboCicloAcad").val(), "","TF",$("#cboTutorB").val(),$("#cboCarrera").val());
    var jsonString = lstTut;
    $('#inputPara').autocomplete({
        source: $.map(jsonString, function(item) {
            return item.cAlumno;
        }),
        select: function(event, ui) {
            var selectecItem = jsonString.filter(function(value) {
                return value.cAlumno == ui.item.value;
            });
            codTut = selectecItem[0].cTA;
            titulo = selectecItem[0].cAlumno;
            alumnos.push({
                hdc:codTut
            });
            ////console.log(alumnos);
            $('#PanelEvento').hide("fade");
            //alert("cod: " + selectecItem[0].cod + ", nombre: " + selectecItem[0].nombre);
        },
        minLength: 2,
        delay: 600
    });

    $('#inputPara').keyup(function() {
        var l = parseInt($(this).val().length);
        //if (l == 0) {
        codTut = "";
        titulo = "";
        alumnos = [];
        //}
    });
    var tb = '';
    var i = 0;
    var filas = jsonString.length;
    for (i = 0; i < filas; i++) {
        tb += '<tr>';
        //tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
        tb += ' <td><input type="checkbox" id="chkSelect" style="text-align:center" hdc="' + jsonString[i].cTA + '"></td>';
        tb += '<td style="text-align:center">' + jsonString[i].cCodU + '</td>';
        tb += '<td>' + jsonString[i].cAlumno + '</td>';
        tb += '<td style="text-align:center">' + jsonString[i].cAbrev + '</td>';
        tb += '<td style="text-align:center">' + jsonString[i].cCat + '</td>';
        //tb += '<td style="text-align:center"><button type="button" id="btnIz" name="btnIz" class="btn btn-sm btn-green btn-icon-green" onclick="fnIzquierda(\'' + data[i].alu + '\',\'' + data[i].categoria + '\')" title="Asignar" ><i class="ti-angle-double-right"></i></button></td>';

        tb += '</tr>';
    }
    //fnDestroyDataTableDetalle('tTutorados');
    fnDestroyDataTableDetalle10('tTutorados');
    $('#tbTutorados').html(tb);
    fnResetDataTableBasic10('tTutorados', 2, 'asc');
    table = $('#tTutorados').DataTable();
    //fnResetDataTableBasic('tTutorados', 1, 'asc');
    }
    

}
function updateDataTableSelectAllCtrl(table) {
    var $table = table.table().node();
    var $chkbox_all = $('tbody input[type="checkbox"]', $table);
    var $chkbox_checked = $('tbody input[type="checkbox"]:checked', $table);
    var chkbox_select_all = $('thead input[id="chkSelectAll"]', $table).get(0);

    // If none of the checkboxes are checked
    if ($chkbox_checked.length === 0) {
        chkbox_select_all.checked = false;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = false;
        }

        // If all of the checkboxes are checked
    } else if ($chkbox_checked.length === $chkbox_all.length) {
        chkbox_select_all.checked = true;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = false;
        }

        // If some of the checkboxes are checked
    } else {
        chkbox_select_all.checked = true;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = true;
        }
    }
}
function updateDataTableSelectAllCtrl2(table) {

    var $table = table.table().node();
    var $chkbox_all = $('tbody input[type="checkbox"]', $table);
    var $chkbox_checked = $('tbody input[type="checkbox"]:checked', $table);
    var chkbox_select_all = $('thead input[id="chkPall"]', $table).get(0);

    // If none of the checkboxes are checked
    if ($chkbox_checked.length === 0) {
        chkbox_select_all.checked = false;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = false;
        }

        // If all of the checkboxes are checked
    } else if ($chkbox_checked.length === $chkbox_all.length) {
        chkbox_select_all.checked = true;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = false;
        }

        // If some of the checkboxes are checked
    } else {
        chkbox_select_all.checked = true;
        if ('indeterminate' in chkbox_select_all) {
            chkbox_select_all.indeterminate = true;
        }
    }
}

function fnCicloAcad(cboId) {

    var arr = fnCicloAcademico(1, "LT", "");
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $(cboId).html(str);
}
function fnTipoSesion() {

    var arr = fTipoSesion(1,"L",0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option opc="' + arr[i].opc +'" value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    $("select[name=cboTipo]").html(str);
    $("#cboTipoS").html(str);
    
     var arr = fTipoSesion(1,"L",0);
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option opc="' + arr[i].opc +'" value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';
    }

    //$("select[name=cboTipo]").html(str);
    $("#cboTipoM").html(str);
}

function fnCursos() {

    var arr = fCursos(1,$("#cboCicloAcad").val(),$("#cboCarrera").val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option opc="' + arr[i].opc +'" value="' + arr[i].cod + '">' +  arr[i].nombre + '</option>';
    }

    $("#cboCurso").html(str);
}
function fnCursos2() {

    var arr = fCursos(1,$("#cboCicloAcad").val(),$("#cboCarreraM").val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {
        str += '<option opc="' + arr[i].opc +'" value="' + arr[i].cod + '">' +  arr[i].nombre + '</option>';
    }

    $("#cboCursoM").html(str);
}
function fnHora() {

    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < 24; i++) {
        str += '<option value="' + i + '">' + zeroPad(i, 2) + '</option>';
    }
    $('#cboHoraA').html(str);
    $('#cboHoraD').html(str);
    $('#cboHoraAV').html(str);
    $('#cboHoraDV').html(str);
    $('#cboHoraAM').html(str);
    $('#cboHoraDM').html(str);
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
    $('#cboMinutoAV').html(str);
    $('#cboMinutoDV').html(str);
    $('#cboMinutoAM').html(str);
    $('#cboMinutoDM').html(str);
    //$(".cboHora").html(str);
}
function fnSemana() {

    var str = "";
    //str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 1; i < 11; i++) {
        str += '<option value="' + i + '">' + i + '</option>';
    }
    $('#cboSemana').html(str);
    //$(".cboHora").html(str);
}

function fnCancelar(event) { 
    var button = $(event); // Botón que activó el modal
            //        //        alert('--')
            ////console.log(button);
    if (button.attr("id") == "btnCancelarSesion") {
        $('#mdRegistro').modal('hide');
        LimpiarTutorado();
        LimpiarSesion();
    }else if (button.attr("id") == "btnCancelarAs"){
               $('#mdAsistencia').modal('hide');
    }else if (button.attr("id") == "btnCancelarM"){
    
        ////console.log(InfoM);
        if (InfoM != $("#divInfo").html()){
             swal({
		                title: "¿Desea continuar?",
		                text: "Los datos han sido modificados, si cierra esta ventana perderá los cambios.",
		                type: "info",
		                showCancelButton: true,
		                confirmButtonClass: 'btn-success',
		                confirmButtonText: 'OK',
		                closeOnConfirm: true,
                  //closeOnCancel: false
                },
              function(){
                fnGuardarDetalle();
      	        //swal("Thanks!", "We are glad you clicked welcome!", "success");
              });
        }else{
            $('#mdModificar').modal('hide');
        }
    }
    
  }

function fnConfirmar(event){
    var button = $(event) // Botón que activó el modal
            //        //        alert('--')
    if (button.attr("id") == "btnGuardarAsistencia") {
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
                fnGuardarAsistencia(1);
      	        //swal("Thanks!", "We are glad you clicked welcome!", "success");
              });
    
    }else if (button.attr("id") == "btnGuardarSesion"){
        if (ValidarSegundo()==true){
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
                fnGuardarDetalle();
      	        //swal("Thanks!", "We are glad you clicked welcome!", "success");
              });
        }
    }else if (button.attr("id") == "btnGuardarM"){
        if (ValidarSesionM()==true){
                swal({
		                title: "Confirmación",
		                text: "¿Desea actualizar la sesión?",
		                type: "success",
		                showCancelButton: true,
		                confirmButtonClass: 'btn-success',
		                confirmButtonText: 'OK',
		                closeOnConfirm: true,
                  //closeOnCancel: false
                },
              function(){
                //$('#mdRegistro').modal('hide');
                fnGuardarDetalle();
      	        //swal("Thanks!", "We are glad you clicked welcome!", "success");
              });
       }
    }else if (button.attr("id") == "btnGuardaIndividual"){
        if (ValidarSesionI()==true){
                swal({
		                title: "Confirmación",
		                text: "¿Desea actualizar la sesión?",
		                type: "success",
		                showCancelButton: true,
		                confirmButtonClass: 'btn-success',
		                confirmButtonText: 'OK',
		                closeOnConfirm: true,
                  //closeOnCancel: false
                },
              function(){
                //$('#mdRegistro').modal('hide');
                fnGuardarAsistencia(2);
      	        //swal("Thanks!", "We are glad you clicked welcome!", "success");
              });
       }
    }
            

 
}
function fnGuardarDetalle() {
      rpta = fnvalidaSession();
      if (rpta == true) {
       
            fnLoading(true)
//          if ($('#hdcod').val() == "0") {      
            if (sesiones.length  > 0) {
//              var chk = $('#chkUna');
//                if (chk.is(':checked')) {
//                    var tipo = 1;
//                }else{
//                    var tipo = 2;                
//                }
                
              $('#action').remove();
              $("form#frmRegistro input[id=cboCicloAcad]").remove();
              $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
              $('#frmRegistro').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');
              var form = $("#frmRegistro").serializeArray();              
              var array = JSON.stringify(alumnos); 
              var array1 = JSON.stringify(sesiones );
              form.push({"name":"array","value":array},{"name":"array1","value":array1});
//              console.log(form);
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/SesionAlumno.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        ////console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcod').val(0);
                          //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                          //LimpiarEval()
                          //fnBuscarTutorados(false);
                          fnMensaje("success", data[0].msje)

//                          fnDestroyDataTableDetalle('tEval');
//                          $('#tbEval').html('');
//                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                          $('#mdRegistro').modal('hide');
                          fnBuscarSesiones(false);
                          alumnos = [];
                          
                          
                      } else {
                          fnMensaje("warning", data[0].msje)
                      }
                  },
                  error: function(result) {
                      //            ////console.log(result)
                      fnMensaje("warning", result)
                  }
              });
          } else if ($('#hdcodS').val !=0){
                $('#action').remove();
                $("form#frmModificar input[id=cboCicloAcadM]").remove();
              $('#frmModificar').append('<input type="hidden" id="action" name="action" value="' + ope.mod + '" />');
              $('#frmModificar').append('<input type="hidden" id="cboCicloAcadM" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');
              $('#cboTipoM').prop('disabled', false);
              var form = $("#frmModificar").serializeArray();  
              $('#cboTipoM').prop('disabled', true);
              ////console.log($("#cod").val());
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/SesionTutor.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        ////console.log(data);
                      if (data[0].rpta == 1) {
                          $('#hdcodS').val(0);
                          //$("#cbocicloAcad").val($("#cboTipoEstudioR").val())
                          //LimpiarEval()
                          fnBuscarSesiones(false);
                          fnMensaje("success", data[0].msje)

//                          fnDestroyDataTableDetalle('tEval');
//                          $('#tbEval').html('');
//                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                          $('#mdModificar').modal('hide');
                          
                      } else {
                          fnMensaje("warning", data[0].msje)
                      }
                  },
                  error: function(result) {
                      //            ////console.log(result)
                      fnMensaje("warning", result)
                  }
              });
          }    
          
          fnLoading(false)
        
      } else {
          window.location.href = rpta
      }
  }
  function fnGuardar() {
      rpta = fnvalidaSession();
      if (rpta == true) {
       
            fnLoading(true)
//          if ($('#hdcod').val() == "0") {
           // ////console.log(sesiones.length );
            if ( sesiones.length ==0) {
          
                var chk = $('#chkUna');
                if (chk.is(':checked')) {
                    var tipo = 1;
                }else{
                    var tipo = 2;                
                }
                //sesiones =[];
              $('#action').remove();
              $("form#frmRegistro input[id=cboCicloAcad]").remove();
              $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="' + ope.reg + '" />');
              $('#frmRegistro').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />');
              var form = $("#frmRegistro").serializeArray();              
              var array = JSON.stringify(alumnos); 
              var array1 = JSON.stringify(chk_dias);
              form.push({"name":"array","value":array},{"name":"tipo","value":tipo},{"name":"array1","value":array1});
              ////console.log(form);
              $.ajax({
                  type: "POST",
                  url: "../DataJson/tutoria/SesionTutor.aspx",
                  data: form,
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      //                        ////console.log(data);
                      if (data[0].rpta == 1) {
                            $('#hdcod').val();
                            $('#divPrimero').css("visibility", "hidden");
                            $('#divPrimero').css("display", "none");
                            $('#pnlNueva').css("visibility", "visible");
                            $('#pnlNueva').css("display", "block");
                            $('#divFooter').css("visibility", "visible");
                            $('#divFooter').css("display", "block");
                            
                            var i = 0;
                            var filas = data.length;
                            for (i = 0; i < filas; i++) {
                            
                                //////console.log(data[i].cod );
                                sesiones.push({stu:data[i].cod});
                                
                            }
                            ////console.log(sesiones );
                                         
//                            fnBuscarTutor();     
//                            fnCategoria('#cboCategoria');
//                            fnEscuela('#cboCarrera');
//                            fnCicloIng();
                            if ($("#cboTipo").find('option:selected').attr("opc") == "2" || $("#cboTipo").find('option:selected').attr("opc") == "3"){
                                $("#cboIng").val($("#cboCicloAcad").val())
                            }
                            fnBuscarAlumnos();
//                          fnDestroyDataTableDetalle('tEval');
//                          $('#tbEval').html('');
//                          fnResetDataTableBasicDetalle('tEval', 0, 'asc', 10);
                            $('#mtRegistro').html("Elegir tutorados");
                            alumnos = [];
                            
                            fnMensaje("success", data[0].msje)
                          
                      } else if (data[0].rpta == -1) {
                          fnMensaje("warning", "Usted no se identifica como Tutor")
                      }else {
                          fnMensaje("warning", data[0].msje)
                      }
                  },
                  error: function(result) {
                      //            ////console.log(result)
                      fnMensaje("warning", result)
                  }
              });
          } 
          fnLoading(false)
        
      } else {
          window.location.href = rpta
      }
  }
  function fnTutor() {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            var arr = fTutores(1, "LCTF",$("#cboCicloAcad").val() );
            var n = arr.length;
            var str = "";
            str += '<option value="" selected>-- Seleccione -- </option>';
            for (i = 0; i < n; i++) {        
                    str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
            }
            
            $("#cboTutorB").html(str); 
            $("#cboTutorB").prop("selectedIndex", 1);   
            $("#cboTutorB").trigger('change');   
        }else{        
            window.location.href = rpta
        }
    }
   

}
  function fnBuscarTutor() {
   
    var arr = fTutores(1, "LCTF",$("#cboCicloAcad").val() ,'','','','');
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboTutor").html(str);  
    $("#cboTutor").prop("selectedIndex", 1);    

}
  function fnCicloIng() {
   
    var arr = fAux(1, "ING",$("#cboCicloAcad").val() ,$("#cboTutor").val(),$("#cboCarrera").val(),'',$("#cboCategoria").val());
    var n = arr.length;
    var str = "";
    str += '<option value="" selected>-- Seleccione -- </option>';
    for (i = 0; i < n; i++) {        
            str += '<option value="' + arr[i].cod + '">' + arr[i].nombre + '</option>';        
    }
    
    $("#cboIng").html(str); 
    $("#cboIng").prop("selectedIndex", 1);      

}
function fnBuscarAlumnos() {
    if ($("#cboCicloAcad").val() == "") {
        fnMensaje("warning", "Debe Seleccionar un Ciclo Académico.")
    } else {
        rpta = fnvalidaSession()
        if (rpta == true) {
            //if (sw) { fnLoading(true); }
            //    fnLoading(true)    ;  
            $("form#frmRegistro input[id=action]").remove();
            $("form#frmRegistro input[id=cboCicloAcad]").remove();
            $('#frmRegistro').append('<input type="hidden" id="action" name="action" value="TUT" />');             
            $('#frmRegistro').append('<input type="hidden" id="cboCicloAcad" name="cboCicloAcad" value="' + $("#cboCicloAcad").val() + '" />'); 
            var form = $("#frmRegistro").serializeArray();
            alumnos =[];
            rows_selected =[];

            $.ajax({
                type: "POST",
                url: "../DataJson/tutoria/Operaciones.aspx",
                data: form,
                dataType: "json",
                cache: false,
                success: function(data) {
                    $("form#frmRegistro input[id=action]").remove();
                    $("form#frmRegistro input[id=cboCicloAcad]").remove();
                    //////console.log(data);
                    var tb = '';
                    var i = 0;
                    var filas = data.length;
                     if ($("#cboTipo").find('option:selected').attr("opc") == "1" ){ 
                        
                            //console.log(data);
                            var jsonString = data;
                            $('#inputPara').autocomplete({
                                source: $.map(jsonString, function(item) {
                                    return item.nombre;
                                }),
                                select: function(event, ui) {
                                    var selectecItem = jsonString.filter(function(value) {
                                        return value.nombre == ui.item.value;
                                    });
                                    codTut = selectecItem[0].cTA;
                                    titulo = selectecItem[0].nombre;
                                    alumnos.push({
                                        hdc:codTut
                                    });
                                    $('#PanelEvento').hide("fade");
                                    //alert("cod: " + selectecItem[0].cod + ", nombre: " + selectecItem[0].nombre);
                                },
                                minLength: 2,
                                delay: 600
                            });

                            $('#inputPara').keyup(function() {
                                var l = parseInt($(this).val().length);
                                //if (l == 0) {
                                codTut = "";
                                titulo = "";
                                alumnos = [];
                                //}
                            });
                        }   else{
                        
                            for (i = 0; i < filas; i++) {
                       
                                    tb += '<tr>';
                                    //tb += '<td style="text-align:center">' + (i + 1) + "" + '</td>';
                                    tb += ' <td><input type="checkbox" id="chkSelect" style="text-align:center" hdc="' + data[i].cTA + '"></td>';
                                    tb += '<td style="text-align:center">' + data[i].codU + '</td>';
                                    tb += '<td>' + data[i].nombre + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].abrev + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].categoria + '</td>';
                                    tb += '<td style="text-align:center">' + data[i].rg + '</td>';
                                    //tb += '<td style="text-align:center"><button type="button" id="btnIz" name="btnIz" class="btn btn-sm btn-green btn-icon-green" onclick="fnIzquierda(\'' + data[i].alu + '\',\'' + data[i].categoria + '\')" title="Asignar" ><i class="ti-angle-double-right"></i></button></td>';

                                    tb += '</tr>';
                                    
                                    rows_selected.push(data[i].cTA);
                                    alumnos.push ({hdc: data[i].cTA });
                                                  
                            }
                        }
                    
                    fnDestroyDataTableDetalle10('tTutorados');
                    $('#tbTutorados').html(tb);
                    fnResetDataTableBasic10('tTutorados', 0, 'asc');
                    
                    table = $('#tTutorados').DataTable();
                    //$('thead input[id="chkSelectAll"]').trigger('click');
//                    $('#tTutorados').change(function(){
                        var cells = table.cells().nodes();
                        $( cells ).find(':checkbox').prop('checked', true);
                        
                       // $( cells ).find(':checkbox').trigger('click');
//                    });
                    //if (sw) { fnLoading(false); }
                    //                    fnLoading(false);
                },
                error: function(result) {
                    //                    ////console.log(result)
                }
            });
        } else {
            window.location.href = rpta
        }
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
function fnAgregar() {
    rpta = fnvalidaSession()
    if (rpta == true) {
      
            if ($("#busPoint").val() == "") {
                fnMensaje("warning", "Debe Seleccionar un alumno.");
            } else {
                var al=[];
                var ses=[];
                al.push({ hdc: codTut1 });
                ses.push({ stu:$("#hdcodS").val()});
                                
                  var array = JSON.stringify(al); 
                  var array1 = JSON.stringify(ses );
                  
                $.ajax({
                    type: "POST",
                    url: "../DataJson/tutoria/SesionAlumno.aspx",
                    data: {"action":ope.reg,"array":array,"array1":array1},
                    dataType: "json",
                    cache: false,
                    success: function(data) {
                        //                        ////console.log(data);
                        if (data[0].rpta == 1) {
                            fnMensaje("success", data[0].msje)
                            //$("#cboTipoEstudio").val($("#cboTipoEstudioR").val())
                            $("#c").val(data[0].c);
                            LimpiarModificar();
                            Edit($("#hdcodS").val());
                        } else {
                            fnMensaje("warning", data[0].msje)
                        }
                    },
                    error: function(result) {
                        //            ////console.log(result)
                        fnMensaje("warning", result)
                    }
                });
            }
       
    } else {
        window.location.href = rpta
    }
}