/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 17/08/2005
  Fecha de Modificación: 13/12/2010
  Creado por	: Gerardo Chunga Chinguel, modificado por hreyes
  Observación	: Validar Curso Programado para GRABAR en la tabla detalle matricula
==========================================================================================
*/

var arrMensajes= new Array(10);
arrMensajes[0] = "Debe elegir los cursos que Ud. se matriculará";
arrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario del Curso a Matricularse";
arrMensajes[2] = "¿Está completamente seguro que desea matricular en los cursos elegidos?"
arrMensajes[3]="¿Está completamente seguro que desea retirar la asignatura seleccionada?"

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
    var cantidad = parseFloat(cantidad);
    var decimales = parseFloat(decimales);
    decimales = (!decimales ? 2 : decimales);
    return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
}

function pintafilamarcada(idcheck)
{
/*    var fila=event.srcElement.parentElement.parentElement
    var curso=document.all.item("curso_padre" + idcheck.cc)
	var claseAnterior=curso.clase
      
    if(idcheck.checked==true){
        fila.className="SelOn"
        curso.className="SelOn"
	}
    else{
	    fila.className=""
	    curso.className=claseAnterior
    }	*/
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

function Actualizar(idCheck, maxCreditos)
{
 	var cursos=0
	var creditos=0
	var totalcur=0;
	var totalcrd;
	var chkcursos ;
	var cantCursosMat = 0;
	var credMat = 0;
	var CreditosPension = 0;
	var sw=true;

	cantCursosMat = $("#cantCursos").val();
	//credMat = frmFicha.credMat.value
	credMat = $("#credMat").val();	
	var size=parseInt($("input:checkbox[name="+idCheck.name+"]").size()) ;


    if (size== 0 && $(idCheck.id).is(':checked') == true) {
			//alert("1");
	            totalcur = 1
	            totalcrd = $(idCheck).val()	   
		//totalcrd=idCheck.value
		//totalcur=1
	}
	else {
	   		//alert(2);
	    //creditos = eval(credMat)
	    creditos = parseInt(credMat)
		//alert(creditos);
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
	                //sw = 1
	                //alert("vd: "+parseInt($(this).attr("vd")));
	                sw=false;
	                   // alert("val: "+$(this).val());
	                
	        
	             
	                     CreditosPension = parseFloat(CreditosPension) + parseFloat(redondear(parseFloat($(this).val()), 2));//parseFloat(Control.vd)
	                      
	             
	               // alert("creditospension "+parseFloat(CreditosPension));
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
    //alert("Creditos: " + creditos+ " > " + maxCreditos)
	
 		var codigo_cac = 0;
        //document.all.totalcrd.innerHTML = creditos - credMat        
        $("#totalcrd").html(parseInt(creditos) - parseInt(credMat));
        //document.all.creditosMat.innerHTML = creditos
        $("#creditosMat").html(creditos)
	    //document.all.creditosMat.innerHTML = eval(document.all.creditosmatriculados.value) + eval(creditos)
        //document.all.totalcur.innerHTML = cursos
        $("#totalcur").html(cursos)
	    codigo_cac = $("#lblCicloAcademico").val()
		pintafilamarcada(idCheck)
		//alert("CPF: "+parseInt($("#txtcodigo_cpf").val()));
		//alert("creditos acum : "+CreditosPension);
		//alert('sw'+sw);
	           if (sw)
	     		$("#lblPrecioCiclo").html("0.00");   	     		
	           else		 		
		 		$("#lblPrecioCiclo").html((parseFloat(CreditosPension)).toFixed(2));   
            
	
	
	
}

function modificarmatricula(modo,ID) {
    
    var codigo_mar = parseInt(document.getElementById("cbocodigo_mar").value);
	var pagina=""
	if (modo=='N'){ //Para nueva matrícula
	    pagina = "../academico/matricula/administrarcomplementario/frmagregarcurso.asp?accion=matriculasegura&codigo_pes=" + ID
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
		
	if (modo=='A'){ //Para agregado de matrícula
	    pagina = "../academico/matricula/administrarcomplementario/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + ID
		parent.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}

	if (modo == 'R') { //Para retiro de asinaturas
	    //if (parseInt($("#cbocodigo_mar").val()) > 0) {
	    if (codigo_mar > 0) {
            if (confirm(arrMensajes[3])==true){
                location.href = "procesarmatricula.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo + "&codigo_mar=" + codigo_mar
		    }
	    } else {
	        alert("Debe seleccionar un motivo para retirar al alumno.");
	    }
	}
}

function BuscarCursosProgramados(codigo_pes)
{
    pagina = "../academico/matricula/administrarcomplementario/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + codigo_pes
	window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}
function ActualizaPreRequisito(param1, param2, param3, param4) {
    pagina = "../administrarcomplementario/calculaPreRequisito.aspx?param1=" + param1 + "&param2=" + param2 + "&param3=" + param3 + "&param4=" + param4
    window.location.href = pagina
}
function EnviarFichaMatricula(_codigo_alu,_codigo_pes,_codigo_cac) {
    var cuotas = 0
    cuotas = 5;

    if ($("#txtAccionMat").val() == "agregarcursomatricula") {
        if (parseInt($("#cbocodigo_mar").val()) < 0) {
            alert("Debe seleccionar un motivo para agregar al alumno.");
            return false;
        }
    }
        
	if (cuotas > 0) { 
	//Mostrar arrMensajes de confirmación
 	  if (confirm("Está seguro que desea agregar los cursos seleccionados a la matrícula")==true){
		//Declarar array de propiedades del check marcado
		var arrCP="" //Array de codigo de curso programado
		var arrVD="" //Array de codigo de curso programado
		var chk1=frmFicha.chkcursoshabiles
		var totalmarcados=0
		


        
		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
 		$(document).ready(function() {
	        $("input:checkbox[name=chkcursoshabiles]").each(function() {	           
	          //$(this).attr("cc") == $(idCheck).attr("cc") && $(this).attr("id") != $(idCheck).attr("id")) {	          	

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
			    //procesararrMensajes()
			    $('#CursosProgramados').val(arrCP);
			    $('#VecesDesprobados').val(arrVD);
				var sw=0;

				$.post("frmcruces.asp", {
						codigo_alu: 		_codigo_alu,
						codigo_pes: 		_codigo_pes,
						codigo_cac: 		_codigo_cac,
						cursosprogramados: 	arrCP
					}, function(data,status) {		            			            
			            if (status=='success'){			            	
			            	$("#divCruces").hide();
			            	$("#divCruces").html(data+'<input type="button" id="btnCerrar" value="Regresar" onclick="cerrarCruce()" class="regresar2">' );		
			            	sw=$('#sw').val();
			            	//alert(sw);

				            if(sw==1){
				            		$("#divCruces").show();				            		
				            		$("#frmFicha").hide();
				            		return false;
			            	}
			            	else{
			            		//alert("submit");
			            		$("#divCruces").hide();	
			            		frmFicha.submit();
			            	}
			            }								            

			        });
			
		}
	  }
    }
    else {
        alert("Debe indicar el número de cuotas que desea pagar")
        return (false)
    }
}

function cerrarCruce(){
	$("#divCruces").html("");
	$("#divCruces").hide();
	$("#frmFicha").show();
}

function procesararrMensajes()
{
	tblFicha.style.display="none"
	tblmensaje.style.display=""
}

function CalcularCuota(valor, precioCredito) {
	/*precioCredito=$('#lblPrecioCurso').val();
	alert(valor+' '+ precioCredito);
	//alert('Precio Ciclo: ' +redondear(parseFloat(parseFloat(precioCredito) / parseFloat(valor)), 2));
    lblPrecioCiclo.innerHTML = redondear(parseFloat(parseFloat(precioCredito) / valor), 2);
    //alert((parseFloat(precioCredito)/parseFloat(valor)).toFixed(2));
    frmFicha.NroCuotas.value = valor;*/
}