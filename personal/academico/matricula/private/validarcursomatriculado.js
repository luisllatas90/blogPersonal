/*
==========================================================================================
  CV-USAT
  Fecha de Creación: 17/08/2005
  Fecha de Modificación: 08/01/2006
  Creado por	: Gerardo Chunga Chinguel
  Observación	: Validar Curso Programado para GRABAR en la tabla detalle matricula
==========================================================================================
*/

var Mensaje= new Array(10);
Mensaje[0] = "Debe elegir los cursos que Ud. se matriculará";
Mensaje[1] = "Ud. sólo puede elegir un Grupo de Horario del Curso a Matricularse";
Mensaje[2] = "No se puede matricular en este Curso, porque existe un cruce de Horario con el Curso:\n\n"
Mensaje[3] = "No se puede matricular en este Curso, porque existen cruzes de horarios\n con los siguientes Cursos:\n\n"
Mensaje[4] = "¿Está completamente seguro que desea matricularse en los cursos elegidos?"
Mensaje[5] = "Está completamente seguro que desea eliminar el curso seleccionado\n Recuerde que esta acción no podrá deshacerla"
Mensaje[6] = "Debe seleccionar la matrícula que desea " //Para cambiar de estado a la matrícula
Mensaje[7] = "La matrícula seleccionada no se puede Cambiar de Estado,\n ya que es una Pre-Matrícula que aún no ha sido concretada"
Mensaje[8] = "No se puede cambiar de estado a la matrícula seleccionada, ya que no coincide con el ciclo Actual vigente"
Mensaje[9] = "Está completamente seguro que desea " //Para cambiar de estado a la matrícula
Mensaje[10]="Solamente se puede eliminar una pre-matrícula realizada en el ciclo actual vigente"
Mensaje[11]="El estudiante tiene una Deuda pendiente. Por favor consulte el Estado de Cuenta en el menú Matrícula"
Mensaje[12]="¿Está completamente seguro que desea retirar la asignatura seleccionada?"

function pintafilamarcada(fila,idCheck)
{
   var Estado=true
   var FilaSeleccionada=fila;
   var Celdas = FilaSeleccionada.getElementsByTagName('td');
   var ArrCeldas  = Celdas.length;
	for (var c = 0; c < ArrCeldas; c++){
		if(idCheck.checked==true){
			Celdas[c].style.backgroundColor = '#DFEFFF';
		}
		else {
			Celdas[c].style.backgroundColor = ''; // #d6e7ef
			Estado=false
		}
	}
   return true;
}

function enviarCursosProgramados(codigo_mat,codigo_alu,codigo_cac,accion,tipoSP)
{
	//Mostrar mensaje de confirmación
 	  if (confirm("Está seguro que desea agregar los cursos seleccionados a la matrícula")==true){
		//Declarar array de propiedades del check marcado
		var arrCP="" //Array de codigo de curso programado
		var arrTC="" //Array de tipo de curso
		var arrCR="" //Array de creditos del curso
		var arrVD="" //Array de veces que lleva el curso
		var chk1=fracursoprogramado.frmFicha.chkcursoshabiles
		var totalmarcados=0

		//Recorriendo 1er Iframe Con cursos curriculares/complementarios
		if (chk1.length==undefined){
			if (chk1.checked==true){
				arrCP+=chk1.cp + ","
				arrTC+=chk1.tc + ","
				arrCR+=chk1.cr + ","
				arrVD+=chk1.vd + ","
				totalmarcados=1
			}
		}
		else{
	           for(var i=0;i<chk1.length;i++){
			Control=chk1[i]
			if(Control.checked==true){
				arrCP+=Control.cp + ","
				arrTC+=Control.tc + ","
				arrCR+=Control.cr + ","
				arrVD+=Control.vd + ","
				totalmarcados=eval(totalmarcados)+1
			}
		   }
		}
		if (totalmarcados==0){
			alert(Mensaje[0])
		}
		else{
			MensajeProcesando('../../../../')
			location.href="procesarmatricula.asp?accion=" + accion + "&tipoSP=" + tipoSP + "&CursosProgramados=" + arrCP + "&TipoCursos=" + arrTC + "&CreditoCursos=" + arrCR + "&VecesDesprobados=" + arrVD + "&codigo_mat=" + codigo_mat + "&codigo_alu=" + codigo_alu + "&codigo_cac=" + codigo_cac
		}
	}
}
	
function verificaeleccion(idCheck,codCur)
	{
		var valctrl=0;var contadorGH=0
		var totalcrd=0;var totalcur=0
		var fila=idCheck.value
		fila=document.getElementById("fila" + fila)
		var chkcursos=frmFicha.chkcursoshabiles

		if (chkcursos.length==undefined){
			if (chkcursos.checked==true){
				var Creditos=chkcursos.cr
				totalcrd+=eval(Creditos)
				totalcur=eval(totalcur)+1
			}
		}
		else{
			for (i=0; i<chkcursos.length;i++){
			    var Control=chkcursos[i]
				if (Control.type=="checkbox" && Control.checked){
					if (Control.cc==codCur)
						{contadorGH=contadorGH+1}
					var Creditos=Control.cr
					totalcrd+=eval(Creditos)
					totalcur=eval(totalcur)+1
			 	}
			}
		}
	
		if (contadorGH>1){
	   		alert (Mensaje[1])
		   	totalcrd=totalcrd-eval(idCheck.cr)
		   	totalcur=totalcur-1
		   	idCheck.checked=false
		}
		
		if (totalcur>0){
			parent.cmdAgregar.disabled=false
		}
		else{
			parent.cmdAgregar.disabled=true
		}
		pintafilamarcada(fila,idCheck)
		parent.totalcrd.innerHTML=totalcrd
		parent.totalcur.innerHTML=totalcur
}


function MostrarTotalCursosProgramados(numtotal)
{
	parent.totalprog.innerHTML=numtotal

}

function VerificarEstadoMatricula(cicloactual)
{
	if (txtelegido.value!=""){
		var fila=document.getElementById(txtelegido.value)
		var celda=fila.getElementsByTagName('td')
		var cm=eval(celda[1].innerText)
		var dca=celda[3].innerText
		var em=celda[5].innerText
		if (dca==cicloactual){
			if (document.all.cmdEliminar!=undefined){
				cmdEliminar.disabled=true
				cmdAnular.disabled=true
				cmdNormal.disabled=true
				switch(em){
					case "P":
						cmdEliminar.disabled=false
						break
					case "A":
						cmdNormal.disabled=false
						break
					case "N":
						cmdAnular.disabled=false
						break
				}
			}
		}
	}
}

function CambiarEstadoMatricula(modo,estado,cicloactual)
{
	if (txtelegido.value=="")
		{alert(Mensaje[6])}
	else{
		var fila=document.getElementById(txtelegido.value)
		var celda=fila.getElementsByTagName('td')
		var cm=eval(celda[1].innerText)
		var dca=celda[3].innerText
		var em=celda[5].innerText

		if (cm=="" || cm==undefined)
			{alert(Mensaje[6] + estado)}
		else{
			//Verificar que la matrícula se haya concretado; estado=N
			if (em=="P")
				{alert(Mensaje[7])}
			else{
				//Verificar que solo se pueda Anular la última Matrícula
				if (dca!=cicloactual)
					{alert(Mensaje[8])}
				else{
					if (modo=="A" && em=="N" || modo=="N" && em=="A"){
					   if (confirm(Mensaje[9] + estado + " la matrícula " + dca)==true)
						{location.href="procesar.asp?accion=cambiarestadocurso&optestado=" + modo + "&codigo_cup=0&codigo_mat=" + cm}
					}
				}
			}
		}
	}
}


function AccionRegMatricula(modo,cicloactual)
{
	var ciclo_mat=txtciclo_mat.value
	var estado_mat=txtestado_mat.value
	var cm=cbocodigo_mat.value
	
	if (modo=="E"){
		//Verificar que la matrícula se haya concretado; estado=N
		if (estado_mat=="N" && ciclo_mat==cicloactual){
			if (confirm(Mensaje[9] + " eliminar la matrícula seleccionada \Recuerde que el Cargo generado en Caja no eliminará hasta enviar al Banco de Crédito")==true){
				location.href="procesar.asp?accion=cambiarestadocurso&optestado=X&codigo_cup=0&codigo_cac=" + ciclo_mat + "&codigo_mat=" + cm
			}
		}
	}
	
	//Restablecer / Anular matrícula concretada
	if (modo=="C"){
		//Verificar que solo se pueda Anular la última Matrícula
		if (estado_mat=="N" && ciclo_mat==cicloactual){
			if (confirm(Mensaje[9] + " Anular la matrícula seleccionada?")==true){
				location.href="procesar.asp?accion=cambiarestadocurso&optestado=A&codigo_cup=0&codigo_cac=" + ciclo_mat + "&codigo_mat=" + cm
			}
		}

		//Verificar que solo se pueda restablecer a matriculas anuladas
		if (estado_mat=="A" && ciclo_mat==cicloactual){
			 if (confirm(Mensaje[9] + " Restablecer la matrícula seleccionada?")==true){
				location.href="procesar.asp?accion=cambiarestadocurso&optestado=N&codigo_cup=0&codigo_cac=" + ciclo_mat + "&codigo_mat=" + cm
			}
		}	
	}
	
	//Verificar que botón activará, según el estado
	if (modo=="V"){
		if (document.all.cmdCambiar!=undefined){
			cmdCambiar.disabled=true
			cmdEliminar.disabled=true
			if (estado_mat=="N"){
				cmdCambiar.className="marcado2"
				cmdCambiar.value="    Anular"
			}
			if (estado_mat=="A"){
				cmdCambiar.className="regresar2"
				cmdCambiar.value="     Restablecer"
			}
	
			if (ciclo_mat==cicloactual && estado_mat!="P"){
				cmdCambiar.disabled=false
				cmdEliminar.disabled=false
			}
		
			//alert(ciclo_mat + '***' + cicloactual + '**' + estado_mat)
		}
	}
}

function Abrircursomatriculado(modo,cm,ca)
{
	var cup=0
	if (modo=="H"){
		var fila=window.event.srcElement.parentElement;
		cmdRetirar.disabled=true
		if (fila.tagName == "TR"){
			var ecup=fila.getElementsByTagName('td')[8].innerText
			if (ecup!="R"){
				cmdRetirar.disabled=false
				if (document.all.cmdEliminar!=undefined){
					cmdEliminar.disabled=false
				}
				
				txtelegido.value=fila.id
			}
		}
	}
	
	if (modo=="R"){
		if (confirm(Mensaje[12])==true){
			cup=txtelegido.value.substring(2,txtelegido.value.length)
			location.href="procesar.asp?accion=cambiarestadocurso&optestado=R&codigo_cup=" + cup + "&codigo_mat=" + cm + "&codigo_cac=" + ca
		}
	}

	if (modo=="E"){
		if (confirm(Mensaje[5])==true){
			cup=txtelegido.value.substring(2,txtelegido.value.length)
			location.href="procesar.asp?accion=cambiarestadocurso&optestado=E&codigo_cup=" + cup + "&codigo_mat=" + cm + "&codigo_cac=" + ca
		}
	}	

}

function Agregarcursomatriculado(al,cm,ca,dca,ce,cpl,accion,deuda)
{
	var curmat=document.all.txtCurMat
	if (curmat!=undefined)
		{curmat=curmat.value}
	else{
		curmat="MAT"
	}

	if (deuda=="0"){
		AbrirPopUp("frmagregarcurso.asp?accion=" + accion + "&curmat=" + curmat + "&codigo_alu=" + al + "&codigo_mat=" + cm + "&codigo_cac=" + ca + "&descripcion_cac=" + dca + "&codigo_cpf=" + ce + "&codigo_pes=" + cpl,"500","750","yes")
	}
	else{
		alert(Mensaje[11])
	}
}

//Términos para realizar las búsqueda de documentos
//1. Abrir ventana
//2. Evaluar criterios de búsqueda
//3. Recuperar criterios de búsqueda y realizarla

function BuscarCursosProgramados(cp,ca)
	{
	   var termino 	= null;
	   var campo	= null;
	   showModalDialog("frmbuscarcursoprogramado.asp?codigo_pes=" + cp + "&codigo_cac=" + ca,window,"dialogWidth:400px;dialogHeight:250px;status:no;help:no;center:yes");
	}

function EvaluarCriteriosbusqueda(cp,ca)
{
   	var controlbusqueda=document.all.texto
   	var terminobusqueda=controlbusqueda.value
   	var campobusqueda=document.all.cbxcampo
	
	if (terminobusqueda.length<3 && campobusqueda.selectedIndex!=2){
		alert("Por favor escriba el término de búsqueda")
		controlbusqueda.value=""
		controlbusqueda.focus()
		return(false)
	}
 	else{
		var Argumentos = window.dialogArguments;
		var codigo_pes=Argumentos.codigo_pes
		var codigo_cac=Argumentos.codigo_cac
	   	Argumentos.termino=terminobusqueda
   		Argumentos.campo=campobusqueda.value

	   	Argumentos.RecuperarCondicionBusqueda(cp,ca);
		window.close();
	}
}

function desactivarCamposBusqueda()
{
	if (document.all.cbxcampo.selectedIndex==2){
		document.all.texto.disabled=true
	}
	else{
		document.all.texto.disabled=false
		document.all.texto.focus()
	}
}


function RecuperarCondicionBusqueda(codigo_pes,codigo_cac)
{
	document.fracursoprogramado.location.href="listacursosprogramados.asp?modalidad=busqueda&termino=" + termino + "&campo=" + campo + "&codigo_pes=" + codigo_pes + "&codigo_cac=" + codigo_cac 
}

function listaCursosProgramados(cm,cac,dac,cpf,cpes,accion)
{
	location.href="frmagregarcurso.asp?codigo_mat=" + cm + "&codigo_cac=" + cac + "&descripcion_cac=" + dac + "&codigo_cpf=" + cpf + "&codigo_pes=" + cpes + "&accion=" + accion
}


function ConsultarPonderadoAcumulado()
{
	var nombre_cpf=cboescuela.options[cboescuela.selectedIndex].text

	if (cboescuela.value=="-2"){
		alert("Especifique la Escuela Profesional")
		cboescuela.focus()
	}

	if (cbocicloingreso.value=="-2"){
		alert("Especifique el ciclo de Ingreso del Estudiante")
		cbocicloingreso.focus()
	}

	if (cbociclo.value=="-2"){
		alert("Especifique el ciclo académico")
		cbociclo.focus()
	}

	
	location.href="pondacumulado.asp?codigo_cpf=" + cboescuela.value + "&cicloIng_Alu=" + cbocicloingreso.value + "&codigo_cac=" + cbociclo.value + "&nombre_cpf=" + nombre_cpf
}

function ConsultarPonderadoEscuela2()
{
	var nombre_cpf=cbocodigo_cpf.options[cbocodigo_cpf.selectedIndex].text

	if (cbocodigo_cpf.value=="-2"){
		alert("Especifique la Escuela Profesional")
		cbocodigo_cpf.focus()
	}
	
	location.href="pondescuelaciclo.asp?codigo_cpf=" + cbocodigo_cpf.value + "&codigo_cac=" + cbocodigo_cac.value + "&nombre_cpf=" + nombre_cpf
}

function MostrarHistorial(fila)
{
	var celdas=fila.getElementsByTagName('td');
	var codigouniver_alu=celdas[2].innerText

	AbrirPopUp("../../clsbuscaralumno.asp?codigouniver_alu=" + codigouniver_alu + "&pagina=matricula/consultapublica/historial.asp","450","750","no","no","yes")
}

function MensajeProcesando(ruta)
{
	var texto="<h1>&nbsp;</h1><h3 align='center'>Procesando matrícula<br>Por favor. Espere un momento...<br><br>"
	    texto+="<img border='0' src='" + ruta + "/images/cargando.gif' width=\"300\" height=\"25\"></h3><h1>&nbsp;</h1>"

	mensaje.className="contornotabla"
	mensaje.innerHTML=texto
}