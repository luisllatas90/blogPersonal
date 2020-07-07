/*
================================================================================================
	CVUSAT
	Fecha de Modificación: 28/02/2007
	Fecha de Creación: 30/05/2005
	Creador: Gerardo Chunga Chinguel
	Obs: Realiza las validaciones y procedimientos para el módulo de Matrícula del Alumno
================================================================================================
*/

function LimpiarError() {
 window.status="Se ha producido un error. Contáctese con el Administrador del Sistema"
 return true;
}

//window.onerror = LimpiarError

//document.oncontextmenu="return false"


//Declarar array de mensajes de alerta en matrícula
var ArrMensajes= new Array(10);
ArrMensajes[0] = "Debe elegir las asignaturas que Ud. se matriculará";
ArrMensajes[1] = "Ud. sólo puede elegir un Grupo de Horario de la asignatura Programada";
ArrMensajes[4] ="¿Está completamente seguro que desea GUARDAR la matricula?\n\n Recuerde que para cualquier modificación de Agregado o Retiro de Asignaturas\n lo puede hacer después de 48 horas de realizado este proceso"
ArrMensajes[5] ="¿Está seguro que desea Retirarse de la asignatura seleccionada?"
ArrMensajes[6] ="¿Está seguro que desea Restablecer los retiros de asignaturas seleccionadas?"
ArrMensajes[7] ="RECUERDE:\n- ES OBLIGATORIO matricularse en al menos una ASIGNATURA COMPLEMENTARIA\n     (Hacer clic en la pestaña Cursos Complementarios)\n- Después de realizada la Matrícula, NO PUEDE RETIRARSE de una asignatura complementaria"
ArrMensajes[9] ="Ud. NO PUEDE MATRICULARSE EN MÁS DE 3 ASIGNATURAS PARA EL CICLO DE VERANO."
/*
================================================================================================
Creado por	: Gerardo Chunga Chinguel
Observación	: Permite sumar creditaje de cursos programados, para guardar en BD 
================================================================================================
*/

function AbrirCurso(codigo_cur)
{
	var fila=document.all.item("codigo_cur" + codigo_cur)
	var img="../images/mas.gif"
			
	if (fila.length==undefined){
		if (fila.style.display=="none"){
			fila.style.display=""
			img="../images/menos.gif"
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
				img="../images/menos.gif"
			}
			else{
				item.style.display="none"
			}
		}
	}

	document.getElementById("img" + codigo_cur).src=img
}

function pintafilamarcada(idcheck)
{
    var fila=""
    var curso= ""
		if(document.all){
			fila = event.srcElement.parentElement.parentElement
			curso =document.all.item("curso_padre" + idcheck.cc)
		}else{
			var idcurso = "curso_padre" + idcheck.attributes["cc"].value
			curso = document.getElementById(idcurso)
			fila = curso.parentElement.parentElement
		}
	var claseAnterior=curso.clase
      
    if(idcheck.checked==true){
        fila.className="SelOn"
        curso.className="SelOn"
	}
    else{
	    fila.className=""
	    curso.className=claseAnterior
    }

    CalcularcursosElegidos()
   
}




/*Ocurre cuando ya se ha validado la marca del check analizado*/

function CalcularcursosElegidos()
{
	var chk=frmFicha.chkcursoshabiles
	var cursosMarcados=0
	var PagoCursos=0
	var creditosMarcados=0
	var cursosobligatorios=0;
	var eleccompeleccionado=0;
	var cantCursosMat = 0;
	var credMat = 0;
	var valor = 0;
	lblPrecioCurso.innerHTML=PagoCursos
	cantCursosMat = frmFicha.cantCursos.value
	credMat = frmFicha.credMat.value
	cursosMarcados = cantCursosMat
	creditosMarcados = eval(credMat)
	
	if (chk.length==undefined){
		if (chk.checked==true){
			cursosMarcados=1
			PagoCursos=eval(chk.preciocurso)
		}
	}
	else{
		for (i=0; i<chk.length;i++){
            		var Control=chk[i]
			/*sumar los cursos que tengan creditaje mayor a 0*/
			if (Control.checked==true && eval(Control.value)>0){
				var precioCurso = 0
				if(document.all)
					precioCurso = Control.preciocurso
				else
					precioCurso = Control.attributes["preciocurso"].value
					
			    PagoCursos+= Control.value * eval(precioCurso)
			    /* PagoCursos += eval(Control.preciocalculadocurso); */
			    /*alert(PagoCursos)*/
				/*agregado por jmanay*/
				/*alert (Control.preciocalculadocurso);*/
				cursosMarcados=eval(cursosMarcados)+1
				creditosMarcados+=(eval(Control.value))
			}


			/*modificacion*/
			/*se ha marcado electivo o complementario*/
			if (Control.checked==true && (( (Control.tc=='CC' || Control.tc=='CO') && Control.value==0 ) || eval(Control.electivo)==1 ))

				{
					eleccompeleccionado++;
					
				}
			//alert ('jmanay dice :' + Control.tc + '   values : ' + Control.value + '  electivo : ' + Control.electivo);
			/*validar que si solo existen complementarios y lectivos dejarlo grabar sobre de complementarios*/			
			if (( ( Control.tc!='CC' && Control.tc!='CO') ||  Control.value!=0) &&  eval(Control.electivo)!=1)
				{
					cursosobligatorios++;
				}
			

		}
	}


	/*Mostrar total de Cursos Marcados*/
	tdCursos.innerHTML= cursosMarcados
	
	/*Mostrar total de créditos Marcados*/
	tdTotal.innerHTML= creditosMarcados

	/*Mostrar Pago total de 1era pensión, según Marcados
	OJO: Para Medicina siempre es 1200 en ciclo regular
	
	if (frmFicha.txtcodigo_cpf.value==31 && frmFicha.txttipo_cac.value=="N"){
		PagoCursos=600
	}*/
	
	/*04/01/2008 : Para que multiplique por 5 en verano*/
	/*Comentado por hreyes ahora todo calcula en base a 5 e*/
	if (frmFicha.txttipo_cac.value == "E") {
	    PagoCursos = PagoCursos * 5
	    //Calcular pago por inscripción
	    lblInscripcion.innerHTML = redondear(((cursosMarcados - cantCursosMat) * 30), 2)
	}
	else {
	    PagoCursos = (PagoCursos * 5)
	}

	//----------------------------------------------------------------
	
	lblPrecioCurso.innerHTML=redondear(PagoCursos,2)
	valor = frmFicha.NroCuotas.value
	if (valor>0 && frmFicha.TieneMatricula.value == 0){
		lblCuota.innerHTML = redondear(parseFloat(PagoCursos / valor),2)
	}
	
	/*Bloquear siempre GUARDAR*/
	activarControles(0)
	/*determinar si se activa o no*/
	if (cursosMarcados>0){activarControles(1)}
	if (eleccompeleccionado>0){activarControles(1)}
	//activarControles(cursosMarcados);
		
}

function Actualizar(idCheck,limitecrd,tipo)
{
	var contadorGH=0
	var total=0
	var chkcursos=frmFicha.chkcursoshabiles
	var contadoridioma=0
	var cantCursosMat = 0;
	var credMat = 0;
	
	cantCursosMat = frmFicha.cantCursos.value
	credMat = frmFicha.credMat.value

	if (chkcursos.length==undefined){
		if (chkcursos.checked){
		    total=eval(chkcursos.value)
		}
	}
	else{
		total = eval(credMat)
		for (i=0; i<chkcursos.length;i++){
            var Control=chkcursos[i]
			if (Control.type=="checkbox" && Control.checked){
				if (Control.cc==idCheck.cc){
				    contadorGH=contadorGH+1
				}
				if (Control.tcomp=="I" && idCheck.tcomp=="I"){
				    contadoridioma=contadoridioma+1
				}

				total+=eval(Control.value)
			}
		}
	}
	
	/*1. Validar que sólo Marque un sólo grupo horario del curso*/
	if (contadorGH>1){
	   alert (ArrMensajes[1])
	   total=total-eval(idCheck.value)
	   idCheck.checked=false
	   return 0
	}

	/*2. Validar Máximo de Créditos permitidos vs Créditos Marcados, caso de medicina no valida*/
	if (eval(total)>eval(limitecrd) && frmFicha.txtcodigo_cpf.value!=24){
	   alert("Ud. solo se puede matricularse hasta en " + limitecrd + " créditos\n\n Si aún no ha cumplido el límite de créditos\n Se le sugiere elija otro curso de menor creditaje.")
	   idCheck.checked=false
	   return 0
	}

	if (frmFicha.txttipo_cac.value=="N"){
		/*3. Validar adelanto de cursos. Debe matricularse en ciclo inferiores*/
		if (eval(ValidarCicloCursoInferior(idCheck))==0){
			if (idCheck.checked==true)
				{idCheck.checked=false}
			else
				{idCheck.checked=true}
		}

		/*4. Validar que sólo Marque un Idioma en ciclo REGULAR*/
		if (contadoridioma>1){
		   alert ("Sólo debe matricularse en un Idioma")
		   total=total-eval(idCheck.value)
		   idCheck.checked=false
		   return 0
		}
	}

	/*5. Validar que en verano sólo deben matricularse en 3 cursos curriculares 17-12-09
	if (idCheck.checked && frmFicha.txttipo_cac.value=="E"){
	    if ((idCheck.tc!="CO" && idCheck.tc!="CC") || (idCheck.value>0)){
	       	total=eval(tdCursos.innerText)+1
		    if (total>3){
        	        idCheck.checked=false
	                alert(ArrMensajes[9])
		     }
	     }
	 }
	*/


	/*6. Validar Horario en Escuelas NO MODULARES MARCADAS
	if (idCheck.checked && frmFicha.txtcodigo_cpf.value!=20 && frmFicha.txtcodigo_cpf.value!=22 && frmFicha.txtcodigo_cpf.value!=23 && && frmFicha.txtcodigo_cpf.value!=30 && frmFicha.txtcodigo_cpf.value!=31 && frmFicha.txtcodigo_cpf.value!=32 && frmFicha.txtcodigo_cpf.value!=33){
	    ValidarHorarioCurso(idCheck)
	}*/
	

	/*Pintar la fila del Check marcado*/
	pintafilamarcada(idCheck)

	
	/*Marcar/Desmarcar el check del curso padre, dependiendo de los GH*/
	document.all.item("chkcursoUnico" + idCheck.cc).checked=idCheck.checked
}

function redondear(cantidad, decimales)
{
	var cantidad = parseFloat(cantidad);
	var decimales = parseFloat(decimales);
	decimales = (!decimales ? 2 : decimales);
	return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
}
function ActualizaPreRequisito() {
    pagina = "procesarmatricula2.asp"
    //alert(pagina)
    window.location.href = pagina
}
function activarControles(valor)
{
	if (valor==0){
		frmFicha.cmdGuardar.disabled=true
		frmFicha.cmdHorario.disabled=true
	}
	else{
		frmFicha.cmdGuardar.disabled=false
		frmFicha.cmdHorario.disabled=false
	}
}




function EnviarFichaMatricula()
{
			//Declarar array de propiedades del check marcado
		//se puede usar para conctenar array.concat.
		var arrCP="" //Array de codigo de curso programado
		var arrVD="" //Array de veces que lleva el curso
		var chk1=frmFicha.chkcursoshabiles
		var totalmarcados=0
		var HayComplementarioMarcados=0
		var HayComplementarioLlevar=0
		var Bandera=0 //Falso
		var HayComplementarioMatriculado=frmFicha.txtExigirCompl.value //Permite validar si se exige o no el complementario
		var cicloalumno=frmFicha.txtcicloalumno.value 
		var cuotas
		cuotas=0
		
		//agregado por hreyes
		//Validar que haya marcado la cantidad de cuotas
		/*if (document.frmFicha.grupoCuotas[0].checked == true) {
			cuotas = frmFicha.grupoCuotas[0].value
		}
		else
		{ 	if (document.frmFicha.grupoCuotas[1].checked == true)
			{	cuotas = frmFicha.grupoCuotas[1].value
			}
		}
		*/
		cuotas = frmFicha.NroCuotas.value
		//Recorriendo 1er Iframe Con cursos curriculares/complementarios de plan antiguo o nuevo
		if (chk1.length==undefined){
			if (chk1.checked==true){
				arrCP+=chk1.cp + ","
				arrVD+=chk1.vd + ","
				totalmarcados=1
				HayComplementarioMarcados=1
				HayComplementarioLlevar=1
			}
		}
		else{
			for(var I=0;I<chk1.length;I++){
				var Control=chk1[I]
				if ((Control.tc=="CO" || Control.tc=="CC") && Control.value=="0" && eval(Control.ciclo)<= eval(cicloalumno))//Es complementario pago aparte
					{HayComplementarioLlevar=eval(HayComplementarioLlevar)+1}

				if(Control.checked==true){
					arrCP+=Control.cp + ","
					arrVD+=Control.vd + ","
					totalmarcados=eval(totalmarcados)+1

					if ((Control.tc=="CO" || Control.tc=="CC") && Control.value=="0")//El usuario ha marcado más complementarios
						{HayComplementarioMarcados=eval(HayComplementarioMarcados)+1}

				}
			}
		}
		
		//Si no ha elegido cursos -->NO MATRICULAR
		if (totalmarcados=="0"){
			alert(ArrMensajes[0])
			return(false)
		}
		else{
			/* Verificar si ha marcado al menos un complementario */
			//alert(HayComplementarioLlevar + '--' + (HayComplementarioMarcados==0) + '--' + (HayComplementarioMatriculado==0))
			if (HayComplementarioLlevar>0 && HayComplementarioMarcados==0 && HayComplementarioMatriculado==0){
				alert(ArrMensajes[7])
				return(false)				
			}
			else{
				if (cuotas > 0 )
				{ //Mostrar mensaje de confirmación y enviar datos
					if (confirm(ArrMensajes[4])==true){
						procesarMensaje()
						frmFicha.CursosProgramados.value=arrCP
						frmFicha.VecesDesprobados.value=arrVD					
						frmFicha.submit()
					}
				}else
				{ 	alert("Debe indicar el número de cuotas que desea pagar")
					return(false)
				}
			}
		}
}


function modificarmatricula(modo,ID)
{
	
	if (modo=='A'){
		location.href="cargando2015.asp?pagina=agregarcursomatricula&codigo_mat=" + ID
	}
	else{
		//Validar que esté validado el ciclo inferior del curso
 	  	if (ValidarRetiroCursoInferior(ID)==1){
 	  		if (confirm(ArrMensajes[5])==true){
				location.href="procesarmatricula2015.asp?accion=retirarcursomatricula&codigo_dma=" + ID 
			}
		}
	}
}

function MostrarTablaCursos(modo)
{
	var comp=document.all.cursocomplementario
	var curr=document.all.cursocurricular

	if (modo=="R"){
		curr.style.display=""
		if (comp!=undefined){comp.style.display="none"}
	}
	else{
		comp.style.display=""
		if (curr!=undefined){curr.style.display="none"}
	}
}


function VistaHorario(modo)
{
	var pagina="vistahorario.asp?estadomatricula=previo"
	if (modo=="M"){
		pagina="vistahorario.asp?estadomatricula=matriculados"
	}
	
	window.open(pagina,"vsthorario","height=400,width=700,statusbar=no,scrollbars=yes,top=100,left=200,resizable=yes,toolbar=no,menubar=no")	
}

function procesarMensaje()
{
	tblFicha.style.display="none"
	tblmensaje.style.display=""
}

function CalcularCuota(valor, precioCredito) {
    lblCuota.innerHTML = parseFloat((precioCredito / valor))
	frmFicha.NroCuotas.value = valor
}