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
    var fila=event.srcElement.parentElement.parentElement
    var curso=document.all.item("curso_padre" + idcheck.cc)
	var claseAnterior=curso.clase
      
    if(idcheck.checked==true){
        fila.className="SelOn"
        curso.className="SelOn"
	}
    else{
	    fila.className=""
	    curso.className=claseAnterior
    }	
}

function Actualizar(idCheck, maxCreditos)
{
	var cursos=0
	var creditos=0
	var totalcur=0;
	var totalcrd;
	var chkcursos = frmFicha.chkcursoshabiles
	var cantCursosMat = 0;
	var credMat = 0;
	var CreditosPension = 0;
    //var sw = 0;
    
	cantCursosMat = frmFicha.cantCursos.value
	credMat = frmFicha.credMat.value	

	if (chkcursos.length==undefined && idCheck.checked==true){
		totalcrd=idCheck.value
		totalcur=1
	}
	else {
	    creditos = eval(credMat)
	    
		for (i=0; i<chkcursos.length;i++){
		    var Control=chkcursos[i]
			//Desactivar los otros grupos del curso
			if (Control.cc==idCheck.cc && Control.id!=idCheck.id){
				Control.disabled=idCheck.checked==true
			}

			if (Control.checked == true) {
			    creditos += eval(Control.value)
			    cursos = eval(cursos) + 1
			    //sw = 1
			    switch (parseInt(Control.vd)) {
			        case 1: CreditosPension += redondear(parseFloat(Control.value) * 1.3, 2)//parseFloat(Control.vd)
			            break;
			        case 0: CreditosPension += redondear(parseFloat(Control.value), 2)
			            break;
			        default: CreditosPension += redondear(parseFloat(Control.value) * 1.5, 2)//parseFloat(Control.vd)
			            break;
			    }
			}         }
        //creditos = eval(document.all.creditosMat.innerHTML) + eval(creditos)
	}

	if (cursos>0 || totalcur>0){
		document.getElementById("cmdAgregar").disabled=false
	}
	else{
		document.getElementById("cmdAgregar").disabled=true
    }
    //alert("Creditos: " + eval(credMat) + " / " + eval(creditos))
	if (creditos > maxCreditos) {
	    alert("No puede exceder el máximo de créditos permitidos según reglamento académico")
		idCheck.checked=false
    } else {
        var codigo_cac = 0
	    document.all.totalcrd.innerHTML = creditos - credMat
	    document.all.creditosMat.innerHTML =  eval(creditos)
//	    document.all.creditosMat.innerHTML = eval(document.all.creditosmatriculados.value) + eval(creditos)
	    document.all.totalcur.innerHTML = cursos
	    codigo_cac = document.all.lblCicloAcademico.value
		pintafilamarcada(idCheck)
		switch (eval(frmFicha.txtcodigo_cpf.value)) {
		    case 24:
		        if ((codigo_cac == "2007-I") || (codigo_cac == "2007-II") ||
		                (codigo_cac == "2008-I") || (codigo_cac == "2008-II")) {
		        if (creditos <= 13) // medicina
		            document.all.lblPrecioCiclo.innerHTML = redondear(parseFloat(frmFicha.precioCredito.value) * CreditosPension * 5, 2)
		        else
		            document.all.lblPrecioCiclo.innerHTML = redondear(1300 * 5,2)		        
		    } else{
		        if (creditos <= 13) // medicina
		            document.all.lblPrecioCiclo.innerHTML = redondear(parseFloat(frmFicha.precioCredito.value) * CreditosPension * 5, 2)
		        else
		            document.all.lblPrecioCiclo.innerHTML = redondear(1500 * 5,2)
		    }		  
		    break    
		    case 31:
		        if (creditos <= 13) // odontologia
		            document.all.lblPrecioCiclo.innerHTML = redondear(parseFloat(frmFicha.precioCredito.value) * CreditosPension * 5, 2)
		        else
		            document.all.lblPrecioCiclo.innerHTML = 700 * 5
		        break
		    default:
		        document.all.lblPrecioCiclo.innerHTML = redondear( parseFloat(frmFicha.precioCredito.value) * CreditosPension * 5, 2)
		}		
	}
	
}

function modificarmatricula(modo,ID)
{
	var pagina=""
	if (modo=='N'){ //Para nueva matrícula
		pagina="../academico/matricula/administrar/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + ID
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
		
	if (modo=='A'){ //Para agregado de matrícula
		pagina="../academico/matricula/administrar/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + ID
		parent.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
	
	if (modo=='R'){ //Para retiro de asinaturas
  		if (confirm(arrMensajes[3])==true){
			location.href="procesarmatricula.asp?accion=retirarcursomatricula&codigo_dma=" + ID + "&estado_dma=" + modo
		}
	}
}

function BuscarCursosProgramados(codigo_pes)
{
	pagina="../academico/matricula/administrar/frmagregarcurso.asp?accion=agregarcursomatricula&codigo_pes=" + codigo_pes
	window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}
function ActualizaPreRequisito(param1, param2, param3, param4) {
    pagina = "../administrar/calculaPreRequisito.aspx?param1=" + param1 + "&param2=" + param2 + "&param3=" + param3 + "&param4=" + param4
    window.location.href = pagina
}
function EnviarFichaMatricula() {
    var cuotas = 0
	//agregado por hreyes
    //Validar que haya marcado la cantidad de cuotas
	/*
	if (document.frmFicha.grupoCuotas[0].checked == true) {
	    cuotas = frmFicha.grupoCuotas[0].value
	}
	else {
	    if (document.frmFicha.grupoCuotas[1].checked == true) {
	        cuotas = frmFicha.grupoCuotas[1].value
	    }
	}*/

    cuotas = frmFicha.NroCuotas.value
    
	if (cuotas > 0) { 
	//Mostrar arrMensajes de confirmación
 	  if (confirm("Está seguro que desea agregar los cursos seleccionados a la matrícula")==true){
		//Declarar array de propiedades del check marcado
		var arrCP="" //Array de codigo de curso programado
		var arrVD="" //Array de codigo de curso programado
		var chk1=frmFicha.chkcursoshabiles
		var totalmarcados=0
		


        
		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
		if (chk1.length==undefined){
			if (chk1.checked==true){
				arrCP+=chk1.cp + ","
				totalmarcados=1
				arrVD += chk1.vd + ","
			}
		}
		else{
	        for(var i=0;i<chk1.length;i++){
				Control=chk1[i]
				if(Control.checked==true){
					arrCP+=Control.cp + ","
					arrVD+=Control.vd + ","
					totalmarcados=eval(totalmarcados)+1
				}
		   }
		}
		if (totalmarcados==0){
			alert(arrMensajes[0])
		}
		else{
		    
			    //Mostrar arrMensajes de confirmación y enviar datos
			    procesararrMensajes()
			    frmFicha.CursosProgramados.value=arrCP
			    frmFicha.VecesDesprobados.value = arrVD
			    //alert("ok")
			    frmFicha.submit()
			
		}
	  }
    }
    else {
        alert("Debe indicar el número de cuotas que desea pagar")
        return (false)
    }
}

function procesararrMensajes()
{
	tblFicha.style.display="none"
	tblmensaje.style.display=""
}

function CalcularCuota(valor, precioCredito) {
    lblCuota.innerHTML = redondear(parseFloat(parseFloat(precioCredito) / valor), 2)
    frmFicha.NroCuotas.value = valor
}