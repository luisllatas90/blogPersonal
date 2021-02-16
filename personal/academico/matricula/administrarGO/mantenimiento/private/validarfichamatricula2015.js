/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 17/08/2005
  Fecha de Modificación: 08/01/2006
  Creado por	: Gerardo Chunga Chinguel
  Modificado por: Helen Reyes Hernández 25/02/2011
  Observación	: Validar Curso Programado para GRABAR en la tabla detalle matricula
==========================================================================================
*/

var arrMensajes= new Array(10);
arrMensajes[0] = "Debe elegir los cursos que Ud. se matriculará";
arrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario del Curso a Matricularse";
arrMensajes[2] = "¿Está completamente seguro que desea matricular en los cursos elegidos?"
arrMensajes[3]="¿Está completamente seguro que desea retirar la asignatura seleccionada?"
arrMensajes[4] = "¿ACCIÓN IRREVERSIBLE.\nEstá completamente seguro que desea ELIMINAR la asignatura seleccionada?"
arrMensajes[5] = "¿ACCIÓN IRREVERSIBLE.\nEstá completamente seguro que desea restablecer la matrícula del estudiante?"

function AbrirCurso(codigo_cur)
{
	var fila=document.all.item("codigo_cur" + codigo_cur)
	var img="../../../../images/mas.gif"
			
	if (fila.length==undefined){
		if (fila.style.display=="none"){
			fila.style.display=""
			img="../../../../images/menos.gif"
		}
		else{
			fila.style.display="none"
		}
	}
	else{
		for (var i=0;i<fila.length;i++){
			var item=fila[i]
			
			if (item.style.display=="none"){
				item.style.display=""
				img="../../../../images/menos.gif"
			}
			else{
				item.style.display="none"
			}
		}
	}

	document.getElementById("img" + codigo_cur).src=img
}
function redondear(cantidad, decimales) {
/*    var cantidad = parseFloat(cantidad);
    var decimales = parseFloat(decimales);
    decimales = (!decimales ? 2 : decimales);
    return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);*/
    var cantidad = parseFloat(cantidad);
    var decimales = parseFloat(decimales);
    //alert(cantidad+' -- '+decimales);
    //decimales = (!decimales ? 2 : decimales);
    //return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
    //alert(cantidad.toFixed(decimales));
    return cantidad.toFixed(decimales);
}
function pintafilamarcada(idcheck)
{  
    var _cc= $('#'+idcheck.id).attr('cc');
	var fila=event.srcElement.parentElement.parentElement;

    var curso=document.all.item("curso_padre" + _cc);
	var claseAnterior=curso.clase;
      
    if(idcheck.checked==true){
        fila.className="SelOn";
        curso.className="SelOn";
	}
    else{
	    fila.className="";
	    curso.className=claseAnterior;
	}
}

function Actualizar(idCheck)
{
    var cursos = 0;
	var creditos=0
	var totalcur=0;
	var totalcrd;
	var cantCursosMat = 0;
	var credMat = 0;
	var CreditosPension = 0;    
	cantCursosMat = $("#cantCursos").val();	
	credMat = $("#credMat").val();		
	var size=parseInt($("input:checkbox[name="+idCheck.name+"]").size()) ;
		
    if (size== 0 && $(idCheck.id).is(':checked') == true) {		
	            totalcur = 1
	            totalcrd = $(idCheck).val()
	        }	
	else {		
	    creditos = parseInt(credMat)
	    $(document).ready(function() {
	        $("input:checkbox[name=chkcursoshabiles]").each(function() {
	           
	            if ($(this).attr("cc") == $(idCheck).attr("cc") && $(this).attr("id") != $(idCheck).attr("id")) {	            	
	                //$(this).attr('checked', true)
	                $(this).attr('disabled', true)
	            }
	            //agregado por edgard 24/07/2015: no descheckaba y no calculaba al deseleccionar el curso
				if ($(this).attr("cc") == $(idCheck).attr("cc") && $(idCheck).is(':checked')==false ) {	
					
					 $(this).attr('checked', false)  
					 $(this).removeAttr("disabled");

	            }
	            if ($(this).is(':checked')) {
	                creditos = parseInt(creditos) + parseInt($(this).val())
	                cursos = parseInt(cursos) + 1
	                
	                switch (parseInt($(this).attr("vd"))) {
	                    case 1: CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()) * 1.3, 2));//parseFloat(Control.vd)
	                        break;
	                    case 0: CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()), 2));
	                        break;
	                    default: CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()) * 1.5, 2));//parseFloat(Control.vd)
	                        break;
	                }
	            }	             
	        });
	    });
}


	if (cursos > 0 || totalcur > 0) {
	    $("#cmdAgregar").attr('disabled', false)
	}
	else{

		$("#cmdAgregar").attr('disabled', true)

    }
		var codigo_cac = 0  
        $("#totalcrd").html(parseInt(creditos) - parseInt(credMat));
        $("#creditosMat").html(creditos);	    
        $("#totalcur").html(cursos);
	    codigo_cac = $("#lblCicloAcademico").val();
		pintafilamarcada(idCheck);		
		switch (parseInt($("#txtcodigo_cpf").val())) {

	  		case 24:
		        if ((codigo_cac == "2007-I") || (codigo_cac == "2007-II") ||
		                (codigo_cac == "2008-I") || (codigo_cac == "2008-II")) {
		            if (creditos <= 13) // medicina
		                $("#lblPrecioCiclo").html((parseFloat($("#precioCredito").val()) * parseInt(CreditosPension) * 5).toFixed(2));
		            else
		                $("#lblPrecioCiclo").html(parseFloat(1300 * 5).toFixed(2));
		        } else{
    		        if (creditos <= 13) // medicina
    		            $("#lblPrecioCiclo").html((parseFloat($("#precioCredito").val()) * parseInt(CreditosPension) * 5).toFixed(2));
		            else
    		            $("#lblPrecioCiclo").html((1600 * 5).toFixed(2));
		        }		  
		        break    
            case 31:
    		    if (creditos <= 13) // odontologia
    		        $("#lblPrecioCiclo").html((parseFloat($("#precioCredito").val()) * parseInt(CreditosPension) * 5).toFixed(2));
                else
    		        $("#lblPrecioCiclo").html((750 * 5).toFixed(2));
                break
            default:    
              $("#lblPrecioCiclo").html((parseFloat($("#precioCredito").val())*parseFloat(CreditosPension) *5).toFixed(2));
	} 	
	CalcularCuota(5,$('#lblPrecioCiclo').html());
}

function modificarmotivo(codigo_dma,tipo,  motivoactual , obsactual)
{
	var pagina='';

	// mostrar ventana con informacion respectiva
	pagina	=	"../matricula/administrarGO/mantenimiento";
	window.open ("../frmmotivoagregadoretiro2015.asp?ruta="+ pagina + "&accion=modificarmotivo&codigo_dma=" + codigo_dma + "&tipo_mar=" + tipo + "&motivoactual=" + motivoactual + "&obsactual=" + obsactual,"","height=400,width=450") ;
}

function modificarmatricula(modo,ID,dc,cp,accion)
{
	//alert("admin validar ficha mat 2015 modo:"+modo +' ID: '+ID);

	var pagina="";
	if (modo=='N'){ //Para nueva matrícula
	    //pagina="../academico/matricula/mantenimiento/frmagregarcurso2015.asp?esnuevamatricula=S&accion=agregarcursomatricula&codigo_pes=" + ID
	    pagina = "frmagregarcurso2015.asp?esnuevamatricula=S&accion=matriculasegura&codigo_pes=" + ID		
		window.location.href = pagina
	}
		
	if (modo=='A'){ //Para agregado de matrícula
		pagina="../academico/matricula/administrarGO/mantenimiento/frmagregarcurso2015.asp?accion=" + accion + "&codigo_cac=" + ID + "&descripcion_cac=" + dc + "&codigo_pes=" + cp
		parent.location.href="../../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}

	if (modo == 'R') { //Para retiro de asinaturas
	    if (parseInt($("#cbocodigo_mar").val()) > 0) {
	        if (parseInt($("#cbocodigo_mar").val()) == 36) {
	            var texto = $("#txtMotAyR").val();
	            texto = $.trim(texto);
	            if (texto.length > 0) {
	                if (confirm(arrMensajes[3]) == true) {
	                    // mostrar informacion del retiro de la asignatura
	                    pagina = "../matricula/administrarGO/mantenimiento/";
	                    location.href = "procesarmatricula2015.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_cac=" + dc + "&codigo_mar=" + $("#cbocodigo_mar").val() + "&obs=" + $("#txtMotAyR").val()
	                } 	
	            } else {
	                alert("Debe ingresar un motivo de retiro");
	            }
	        } else {
	            if (confirm(arrMensajes[3]) == true) {
	                // mostrar informacion del retiro de la asignatura
	                pagina = "../matricula/administrarGO/mantenimiento/";
	                //window.open("../frmmotivoagregadoretiro2015.asp?ruta=" + pagina + "&accion=retirarcursomatricula&codigo_dma=" + ID + "&tipo_mar=R&motivoactual=" + "&codigo_mar=" + $("#cbocodigo_mar").val(), "", "height=400,width=450");
	                location.href = "procesarmatricula2015.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_cac=" + dc + "&codigo_mar=" + $("#cbocodigo_mar").val() + "&obs=" + $("#txtMotAyR").val()
	            } 		
	        }	       
	    } else {
	        alert("Debe seleccionar un motivo para retirar al alumno.");
	    }		
  		
	}
	
	if (modo=='E'){ //Para ELIMINAR asinaturas
  		if (confirm(arrMensajes[4])==true){
			location.href="procesarmatricula2015.asp?accion=eliminarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_cac=" + dc
		}
	}

}

function restablecermatricula(codigo_mat) {
    if (confirm(arrMensajes[5]) == true) {
        var page = "procesarmatricula2015.asp?accion=restablecermatricula&codigo_mat=" + codigo_mat
        
    location.href = page 
    }
}

function BuscarCursosProgramados(ca,dc)
{
    pagina = "../academico/matricula/administrarGO/mantenimiento/frmagregarcurso2015.asp?accion=agregarcursomatricula&codigo_pes=" + document.all.cbocodigo_pes.value + "&codigo_cac=" + ca + "&descripcion_cac=" + dc
	window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina;
}
function ActualizaPreRequisito(param1, param2, param3, param4) {
    pagina = "../mantenimiento/calculaPreRequisito2015.aspx?param1=" + param1 + "&param2=" + param2 + "&param3=" + param3 + "&param4=" + param4
    window.location.href = pagina;
}
function EnviarFichaMatricula() {
     var cuotas = 0
	//agregado por hreyes
    //Validar que haya marcado la cantidad de cuotas

     // cuotas = frmFicha.NroCuotas.value
     if ($("#txtAccionMat").val() == "agregarcursomatricula") {
         if (parseInt($("#cbocodigo_mar").val()) < 0) {
             alert("Debe seleccionar un motivo para agregar al alumno.");
             return false;
         } else {
             if (parseInt($("#cbocodigo_mar").val()) == 40) {
                 var texto = $("#txtMotAyR").val();
                 texto = $.trim(texto);
                 if (texto.length == 0) {
                     alert("Debe ingresar un motivo de agregado");
                     return false;
                 }
             }     
         }
     }
     
    cuotas=$('#NroCuotas').val();    
	if (cuotas > 0) { 
	//Mostrar arrMensajes de confirmación
 	  if (confirm("Está seguro que desea agregar los cursos seleccionados a la matrícula")==true){
		//Declarar array de propiedades del check marcado
		var arrCP="" //Array de codigo de curso programado
		var arrVD="" //Array de codigo de curso programado
		var totalmarcados=0;
		   
		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
 		$(document).ready(function() {
	        $("input:checkbox[name=chkcursoshabiles]").each(function() {	           	          
	        	  if ($(this).is(':checked')) {		        	  	
	        	  	    arrCP+=$(this).attr("cp") + ",";
	        	  	    arrVD+=$(this).attr("vd") + ",";
	        	  	    totalmarcados=eval(totalmarcados)+1;
	        	  }
	        });
	    });
		

		if (totalmarcados==0){
			alert(arrMensajes[0])
		}
		else{		    
			    //Mostrar arrMensajes de confirmación y enviar datos
			    procesararrMensajes()			    
			    $('#CursosProgramados').val(arrCP);
			    $('#VecesDesprobados').val(arrVD);			    
			    $('#frmFicha').submit();
		}
	  }
    }
    else {
        alert("Debe indicar el número de cuotas que desea pagar");
        return (false);
    }
}

function procesararrMensajes()
{
	tblFicha.style.display="none";
	tblmensaje.style.display="";
}

function CalcularCuota(valor, precioCredito) {
    $('#lblCuota').html(redondear(parseFloat(parseFloat(precioCredito) / valor), 2));
    frmFicha.NroCuotas.value = valor;
}

